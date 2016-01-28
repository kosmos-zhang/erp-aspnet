<%@ WebHandler Language="C#" Class="SellSendDetailsList" %>

using System;
using System.Web;
using System.Data;
using XBase.Business.Office.SellManager;

public class SellSendDetailsList : BaseHandler
{
    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "getinfolist":
                GetSellSendDetailsList();//获取销售发货明细列表
                break;
            default:
                DefaultAction(action);
                break;
        }
    }

    //获取销售发货明细列表
    private void GetSellSendDetailsList()
    {
        //设置行为参数
        string orderString = (GetParam("orderby").Trim());//排序
        string order = "desc";//排序：降序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "SendDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：升序
        }
        int pageCount = int.Parse(GetParam("pageCount").Trim());//每页显示记录数
        int pageIndex = int.Parse(GetParam("pageIndex").Trim());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        int totalCount = 0;
        string ord = orderBy + " " + order;

        //查询条件参数Start
        string productID = GetParam("ProductID").Trim().Length == 0 ? null : GetParam("ProductID").Trim();//物品
        string custID = GetParam("CustID").Trim().Length == 0 ? null : GetParam("CustID").Trim();//客户
        string beginDate = GetParam("BeginDate").Trim().Length == 0 ? null : GetParam("BeginDate").Trim();//开始日期
        string endDate = GetParam("EndDate").Trim().Length == 0 ? null : GetParam("EndDate").Trim();//结束日期
        string isOpenBill = GetParam("IsOpenBill").Trim().Length == 0 ? null : GetParam("IsOpenBill").Trim();//是否已开票
        //查询条件参数End 
        XBase.Model.Office.SellManager.SellSendDetailsListModel model = new XBase.Model.Office.SellManager.SellSendDetailsListModel();
        model.ProductID = productID;
        model.CustID = custID;
        model.BeginDate = beginDate;
        model.EndDate = endDate;
        model.IsOpenBill = isOpenBill;
        model.CompanyCD = UserInfo.CompanyCD;
        model.SelPointLen = UserInfo.SelPoint;
        model.IsMoreUnit=UserInfo.IsMoreUnit;

        DataTable dt =SellSendDetailsListBus.GetSellSendDetailListData(model,pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        Output(sb.ToString());
    }

}
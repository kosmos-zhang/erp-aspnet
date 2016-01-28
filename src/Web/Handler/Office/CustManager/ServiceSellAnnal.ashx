<%@ WebHandler Language="C#" Class="ServiceSellAnnal" %>

using System;
using System.Web;
using XBase.Business.Office.CustManager;
using System.Data;
using XBase.Common;
using System.IO;
using System.Web.Script.Serialization;

public class ServiceSellAnnal : BaseHandler
{
    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "getlist":
                GetListData();//获取产品销售记录列表
                break;
            default:
                DefaultAction(action);
                break;
        }
    }

    //获取产品销售记录列表
    public void GetListData()
    {
        //设置行为参数
        string orderString = (GetParam("orderby").Trim());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "orderDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(GetParam("pageCount").Trim());//每页显示记录数
        int pageIndex = int.Parse(GetParam("pageIndex").Trim());//当前页
        int totalCount = 0;
        string ord = orderBy + " " + order;

        string CustID = GetParam("CustID").Trim();
        string ProductID = GetParam("ProductID").Trim();
        string DateBegin = GetParam("DateBegin").Trim();//开始时间;
        string DateEnd = GetParam("DateEnd").Trim();//结束时间;

        DataTable dt = ServiceBus.GetSellAnnalList(UserInfo, CustID, ProductID, DateBegin, DateEnd, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
        {
            sb.Append("[{\"ID\":\"\"}]");
        }
        else
        {
            sb.Append(JsonClass.DataTable2Json(dt));
        }
        sb.Append("}");

        Output(sb.ToString());
    }
}
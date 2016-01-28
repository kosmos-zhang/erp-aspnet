<%@ WebHandler Language="C#" Class="SellProductReport" %>

using System;
using System.Web;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using XBase.Business.Office.SellReport;
using XBase.Model.Office.SellReport;

public class SellProductReport : BaseHandler
{
    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "save":
                SaveSellRpt();//添加销售汇报
                break;
            case "update":
                UpDateSellRpt();//修改
                break;
            case "del":
                DeleteSellRpt();//删除
                break;
            case "getdatabyid":
                GetSellProductRptData();//获取销售汇报详细信息
                break;
            case "getdatalist":
                GetDataList();//根据检索条件获取销售汇报列表
                break;
            case "getprodinfo":
                GetProductInfoByID();//根据ID获取产品信息
                break;
            default:
                DefaultAction(action);
                break;
        }
    }

    //添加
    private void SaveSellRpt()
    {
        string strMsg = "";
        int billID = 0;
        bool isSucc = false;
        SellReportModel sellRptModel = GetSellRptModel();
        List<SellReportDetailModel> sellRptDetailsModelList = GetSellRptDetailsModelList();
        isSucc = SellProductReportBus.Insert(sellRptModel, sellRptDetailsModelList, out strMsg);//ExpensesBus.SaveExpensesApply(expApplyModel, expDetailsModelList, out strMsg);
        
        JsonClass jc;
        if (isSucc)
        {
            //expApplyID = ExpensesBus.GetExpApplyID(strCode, strCompanyCD);
            billID = Convert.ToInt32(strMsg.Split('|')[1]);
            jc = new JsonClass(billID, "", "", strMsg.Split('|')[0].ToString(), 1);
        }
        else
        {
            jc = new JsonClass(billID, "", "", strMsg, 0);
        }
        Output(jc.ToJosnString());
    }

    //修改
    private void UpDateSellRpt()
    {
        string strMsg = "";
        bool isSucc = false;
        SellReportModel sellRptModel = GetSellRptModel();
        List<SellReportDetailModel> sellRptDetailsModelList = GetSellRptDetailsModelList();
        string strBillID = GetParam("BillID").Trim();//ID
        int billID = strBillID.Length == 0 ? -1 : Convert.ToInt32(strBillID);
        sellRptModel.ID = billID;//附加到model中
        isSucc = SellProductReportBus.UpdateSellReport(sellRptModel, sellRptDetailsModelList, out strMsg);//ExpensesBus.UpdateExpensesApply(expApplyModel, expDetailsModelList, out strMsg);
        
        JsonClass jc;
        if (isSucc)
        {
            jc = new JsonClass(billID, "", "", strMsg, 1);
        }
        else
        {
            jc = new JsonClass(billID, "", "", strMsg, 0);
        }
        Output(jc.ToJosnString());
    }
    
    //删除
    private void DeleteSellRpt()
    {
        string billID = GetParam("BillIDs").Trim();
        billID = billID.Remove(billID.Length - 1, 1);

        string strMsg = string.Empty;
        string strFieldText = string.Empty;
        JsonClass JC;
        if (SellProductReportBus.DelSellRpt(billID, UserInfo.CompanyCD, out strMsg, out strFieldText))
            JC = new JsonClass(0, strFieldText, "", strMsg, 1);
        else
            JC = new JsonClass(0, strFieldText, "", strMsg, 0);

        Output(JC.ToJosnString());
    }
    
    //获取销售汇报详细信息
    private void GetSellProductRptData()
    {
        string strBillID = GetParam("billID").Trim();//费用申请单ID
        int BillID = strBillID.Length == 0 ? -1 : Convert.ToInt32(strBillID);

        DataTable dt = SellProductReportBus.GetSellReportMain(BillID, UserInfo.CompanyCD,UserInfo.SelPoint);
        DataTable dtDetail = SellProductReportBus.GetSellReportDetail(BillID);
        
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("data:");
        if (dt.Rows.Count == 0)
        {
            sb.Append("[{\"ID\":\"\"}]");
            sb.Append("}");
        }
        else
        {
            sb.Append(JsonClass.DataTable2Json(dt));
            if (dtDetail.Rows.Count > 0)
            {
                sb.Append(",");
                sb.Append("dataDetail:");
                sb.Append(JsonClass.DataTable2Json(dtDetail));
            }
            sb.Append("}");
        }

        Output(sb.ToString());
    }

    //获取销售汇报列表
    private void GetDataList()
    {
        //设置行为参数
        string orderString =GetParam("orderby").Trim();//排序
        string order = "desc";//排序：降序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "createdate";//要排序的字段，如果为空，默认为"createdate"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：升序
        }
        int pageCount = int.Parse(GetParam("pageCount").Trim());//每页显示记录数
        int pageIndex = int.Parse(GetParam("pageIndex").Trim());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        int totalCount = 0;
        string ord = orderBy + " " + order;

        DataTable hdt = new DataTable();
        SellReportModel sellRpt = new SellReportModel();
        sellRpt.CompanyCD = UserInfo.CompanyCD;
        sellRpt.SelPointLen = UserInfo.SelPoint;
        sellRpt.CreateDate = GetParam("createdate").Trim().Length == 0 ? null : (DateTime?)Convert.ToDateTime(GetParam("createdate").Trim());
        sellRpt.SellDept = GetParam("SellDept").Trim().Length == 0 ? null : (int?)Convert.ToInt32(GetParam("SellDept").Trim());
        
        DateTime? CreateDate1 = GetParam("createdate1").Trim().Length == 0 ? null : (DateTime?)Convert.ToDateTime(GetParam("createdate1").Trim());
        hdt = SellProductReportBus.GetSellRptList(sellRpt,CreateDate1, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (hdt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(hdt));
        sb.Append("}");

        Output(sb.ToString());
    }

    //获取销售汇报实体信息
    private SellReportModel GetSellRptModel()
    {
        string strSellDept = GetParam("SellDept").Trim();
        string strProductID = GetParam("productID").Trim();
        string strProductName = GetParam("productName").Trim();
        string strPrice = GetParam("price").Trim();
        string strSellNum = GetParam("sellNum").Trim();
        string strSellPrice = GetParam("sellPrice").Trim();
        string strCreatedate = GetParam("createdate").Trim();
        string strMemo = GetParam("memo").Trim();

        SellReportModel model = new SellReportModel();
        model.SellDept = strSellDept.Length == 0 ? null : (int?)Convert.ToInt32(strSellDept);
        model.ProductID = strProductID.Length == 0 ? null : (int?)Convert.ToInt32(strProductID);
        model.ProductName = strProductName.Length == 0 ? null : strProductName;
        model.Price = strPrice.Length == 0 ? null : (decimal?)Convert.ToDecimal(strPrice);
        model.SellNum = strSellNum.Length == 0 ? null : (decimal?)Convert.ToDecimal(strSellNum);
        model.SellPrice = strSellPrice.Length == 0 ? null : (decimal?)Convert.ToDecimal(strSellPrice);
        model.CreateDate = strCreatedate.Length == 0 ? null : (DateTime?)Convert.ToDateTime(strCreatedate);
        model.Memo = strMemo.Length == 0 ? null : strMemo;
        model.CompanyCD = UserInfo.CompanyCD;
        model.SelPointLen = UserInfo.SelPoint;

        return model;
    }

    //获取提成明细
    private List<SellReportDetailModel> GetSellRptDetailsModelList()
    {
        string[] strarray = null;
        string recorditems = "";
        string[] inseritems = null;
        string strfitinfo = GetParam("strfitinfo").Trim();

        SellReportDetailModel sellRptDModel;
        List<SellReportDetailModel> sellRptDModelList = new List<SellReportDetailModel>();

        strarray = strfitinfo.Split('|');
        string[] sqlarray = new string[strarray.Length];

        for (int i = 0; i < strarray.Length; i++)
        {
            StringBuilder fitsql = new StringBuilder();
            recorditems = strarray[i];
            inseritems = recorditems.Split(',');
            if (recorditems.Length != 0)
            {
                sellRptDModel = new SellReportDetailModel();
                //sellRptDModel.SortNo = Convert.ToInt32(inseritems[0].ToString());//序号
                sellRptDModel.SellerID = Convert.ToInt32(inseritems[1].ToString().Trim());//业务员
                sellRptDModel.Rate = Convert.ToDecimal(inseritems[2].ToString());//提成比例
                sellRptDModel.CompanyCD = UserInfo.CompanyCD;
                sellRptDModelList.Add(sellRptDModel);
            }
        }
        return sellRptDModelList;
    }

    //根据产品ID获取产品信息
    private void GetProductInfoByID()
    {
        DataTable dt = null;
        int productid = GetParam("productID").Trim().Length == 0 ? -1 :Convert.ToInt32(GetParam("productID").Trim());
        if (productid != -1)
        {
            dt = SellProductReportBus.GetProductInfoByID(productid);
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("data:");
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
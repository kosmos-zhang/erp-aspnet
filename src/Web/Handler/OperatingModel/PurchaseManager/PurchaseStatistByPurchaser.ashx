<%@ WebHandler Language="C#" Class="PurchaseStatistByPurchaser" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using XBase.Business.Common;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Data;


public class PurchaseStatistByPurchaser : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string Action = context.Request.Form["Action"];
            switch (Action)
            {
                case "Select":
                    GetPurByPurchaser(context);
                    break;
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    private void GetPurByPurchaser(HttpContext context)
    {
        string orderBy = (context.Request.Form["orderBy"]);//排序
        //string order = "asc";//排序：升序
        //string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "Purchaser";//要排序的字段，如果为空，默认为"ID"
        //if (orderString.EndsWith("_d"))
        //{
        //    order = "desc";//排序：降序
        //}
        string CompanyCD = string.Empty;
        try
        {
            CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        }
        catch
        {
            CompanyCD = "1001";
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        string Purchaser = context.Request.Form["Purchaser"];
        string StartDate = context.Request.Form["StartDate"];
        string EndDate = context.Request.Form["EndDate"];
        EndDate = Convert.ToDateTime(EndDate).AddDays(1).ToString("yyyy-MM-dd");
        int totalCount = 0;

        DataTable dt = PurchaseOrderBus.GetPurStatByPurchaser(CompanyCD,Purchaser,StartDate,EndDate,pageIndex, pageCount, orderBy, ref totalCount);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
        {
            sb.Append("\"\"");
        }
        else
        {
            sb.Append(JsonClass.DataTable2Json(dt));
        }
        sb.Append("}");
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
}
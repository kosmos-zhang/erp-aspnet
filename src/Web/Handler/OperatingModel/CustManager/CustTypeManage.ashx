<%@ WebHandler Language="C#" Class="CustTypeManage" %>

using System;
using System.Web;
using XBase.Model.Office.CustManager;
using System.IO;
using System.Data;
using XBase.Business.Office.CustManager;
using XBase.Common;

public class CustTypeManage : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "num";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        //int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        //int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        //int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        string CustTypeManage = context.Request.Form["CustTypeManage"].ToString();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        string ord = orderBy + " " + order;

        DataTable dt = null;
        switch (CustTypeManage)
        {
            case "1":
                dt = CustInfoBus.GetCustListByTypeManage(CompanyCD,ord);
                break;
            case "2":
                dt = CustInfoBus.GetCustListByTypeSell(CompanyCD, ord);
                break;
            case "3":
                dt = CustInfoBus.GetCustListByTypeTime(CompanyCD, ord);
                break;
            default:
                dt = CustInfoBus.GetCustListByTypeManage(CompanyCD, ord);
                break;
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        //sb.Append("totalCount:");
        //sb.Append(totalCount.ToString());
        sb.Append("data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"CustTypeName\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
        
    }

    public class DataSourceModel
    {
        public string CustTypeName { get; set; }
        public string num { get; set; }        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
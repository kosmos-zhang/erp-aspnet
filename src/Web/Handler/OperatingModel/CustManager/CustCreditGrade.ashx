<%@ WebHandler Language="C#" Class="CustCreditGrade" %>

using System;
using System.Web;
using XBase.Model.Office.CustManager;
using System.IO;
using System.Data;
using XBase.Business.Office.CustManager;
using XBase.Common;

public class CustCreditGrade : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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

        string ord = orderBy + " " + order;

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        DataTable dt = CustInfoBus.GetCustListByCreditGrade(CompanyCD, ord);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"num\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
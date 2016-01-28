<%@ WebHandler Language="C#" Class="EmployeeSel" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.CustManager;

public class EmployeeSel : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
         string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//用户公司代码           
        //XElement dsXML = ConvertDataTableToXML();

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("data:");
        sb.Append(JsonClass.DataTable2Json(CustInfoBus.GetCustManager(CompanyCD)));
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
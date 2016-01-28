<%@ WebHandler Language="C#" Class="CustLink" %>

using System;
using System.Web;
using XBase.Business.Office.CustManager;
using System.Data;
using XBase.Common;

public class CustLink : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string LinkName = context.Request.Params["LinkName"].ToString().Trim();
        string CustNo = context.Request.Params["CustNo"].ToString().Trim(); //客户ID
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码

        DataTable dt = CustInfoBus.GetCustLinkByID(CompanyCD, LinkName, CustNo);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("data:");
        
        if (dt.Rows.Count == 0)
            sb.Append("[{\"id\":\"\"}]");
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
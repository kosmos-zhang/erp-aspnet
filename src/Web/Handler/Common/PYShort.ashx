<%@ WebHandler Language="C#" Class="PYShort" %>

using System;
using System.Web;

public class PYShort : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string strPYShort = XBase.Common.PYShortUtil.GetPYString(context.Request.QueryString["Text"].ToString());
        JsonClass jc;
        jc = new JsonClass(strPYShort, "", 1);
        context.Response.Write(jc);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
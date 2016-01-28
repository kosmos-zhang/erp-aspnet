<%@ WebHandler Language="C#" Class="ComplainDel" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.CustManager;

public class ComplainDel : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string allcustid = context.Request.Params["allcustid"].ToString().Trim(); //客户联络ID
        string[] al = allcustid.Split(',');

        JsonClass jc;

        if (ComplainBus.DelComplainInfo(al) >= 0)
            jc = new JsonClass("success", "", 1);
        else
            jc = new JsonClass("faile", "", 0);
        context.Response.Write(jc);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
<%@ WebHandler Language="C#" Class="ContactInfoDel" %>

using System;
using System.Web;
using System.Collections;
using XBase.Business.Office.CustManager;

public class ContactInfoDel : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string allcustid = context.Request.Params["allcustid"].ToString().Trim(); //客户联络ID
        string[] al = allcustid.Split(',');

        JsonClass jc;

        if (ContactHistoryBus.DelContactInfo(al) >= 0)
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
<%@ WebHandler Language="C#" Class="TalkInfoDel" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.CustManager;

public class TalkInfoDel : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string allcustid = context.Request.Params["alltalkid"].ToString().Trim(); //客户联络ID
        string[] al = allcustid.Split(',');

        JsonClass jc;

        if (TalkBus.DelTalkInfo(al) >= 0)
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
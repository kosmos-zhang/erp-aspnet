<%@ WebHandler Language="C#" Class="CustInfoDel" %>

using System;
using System.Web;
using System.Collections;
using XBase.Business.Office.CustManager;
using System.Data;

public class CustInfoDel : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        
        string allcustid = context.Request.Params["allcustid"].ToString().Trim(); //客户编号
        string AllCustNO = context.Request.Params["AllCustNO"].ToString().Trim(); //客户编号
        string[] al = allcustid.Split(',');
        string[] AllNo = AllCustNO.Split(',');

        JsonClass jc;
        
        //执行删除
        //CustInfoBus.DelCustInfo(al);

        bool LinkManHas = CustInfoBus.GetLinkManByCustNo(AllNo,al);
        if (LinkManHas == true)
        {
            jc = new JsonClass("success", "", 2);
        }
        else
        {
            if (CustInfoBus.DelCustInfo(al) > 0)
                jc = new JsonClass("success", "", 1);
            else
                jc = new JsonClass("faile", "", 0);            
        }
        context.Response.Write(jc);
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
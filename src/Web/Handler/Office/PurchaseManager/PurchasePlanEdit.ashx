<%@ WebHandler Language="C#" Class="PurchasePlanEdit" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;
using System.Web.SessionState;

public class PurchasePlanEdit : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context) 
    {
        if (context.Request.RequestType == "POST")
        {
            string ID = context.Request.Form["ID"];
            string sign = context.Request.Form["sign"];
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            DataTable dtp = PurchasePlanBus.GetPurchasePlanPrimary(ID);
            DataTable dt = PurchasePlanBus.GetPurchasePlanSource(ID);
            DataTable dt2 = PurchasePlanBus.GetPurchasePlanDetail(ID);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("datap:");
            sb.Append(JsonClass.DataTable2Json(dtp));
            sb.Append(",data:");
            sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append(",data2:");
            sb.Append(JsonClass.DataTable2Json(dt2));
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();


        }
    }  
 
    public bool IsReusable 
    {
        get 
        {
            return false;
        }
    }

}
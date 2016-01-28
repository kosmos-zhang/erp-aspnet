<%@ WebHandler Language="C#" Class="CompanyModule" %>
using System;
using System.Web;
using XBase.Common;
using XBase.Business.SystemManager;
using XBase.Model.SystemManager;

public class CompanyModule : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string action = context.Request.QueryString["action"];
        string CompanyModule = context.Request.QueryString["CompanyModule"];
        string CompanyCD = context.Request.QueryString["CompanyCD"];
        string[] module = CompanyModule.Split(',');
        string result = "";
        if (action == "Edit")
        {
            result = CompanyModBus.AddCompanyModule(CompanyCD, module).ToString();
        }
        context.Response.Write(result);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
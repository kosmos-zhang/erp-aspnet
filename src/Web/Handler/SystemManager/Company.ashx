<%@ WebHandler Language="C#" Class="Company" %>

using System;
using System.Web;
using XBase.Business.SystemManager;
public class Company : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string CompanyCD = context.Request.QueryString["txtCompanyCD"];
        bool result = false;
        result = CompanyBus.IsExist(CompanyCD);
        if (result)
        {
            context.Response.Write("1");
        }
        else
        {
            context.Response.Write("0");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
<%@ WebHandler Language="C#" Class="CompanyOpenServInfo" %>

using System;
using System.Web;
using XBase.Business.SystemManager;
using XBase.Model.SystemManager;

public class CompanyOpenServInfo : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //获取公司代码
        string companyCD = context.Request.QueryString["txtCompanyCD"];
        //获取客户公司信息
        CompanyOpenServModel model = CompanyOpenServBus.GetCompanyOpenServInfo(companyCD);
        if (model == null)
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
<%@ WebHandler Language="C#" Class="ContractNoCheck" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.SystemManager;
using XBase.Business.Office.AdminManager;
using XBase.Business.Common;

public class ContractNoCheck : IHttpHandler, System.Web.SessionState.IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        string Code = context.Request.QueryString["strcode"];//获取序列号
        string CommonTable = context.Request.QueryString["TableName"];
        string ColName = context.Request.QueryString["colname"];
        bool result = PrimekeyVerifyBus.CheckCodeUniq(CommonTable, ColName, Code);
        //唯一性字段是否存在存在
        JsonClass jc;
        if (!result)
        {
            jc = new JsonClass("faile", "合同编号已经存在，请重新输入！", 0);
        }
        else
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            jc = new JsonClass("success", "", 1);
        }
        context.Response.Write(jc);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
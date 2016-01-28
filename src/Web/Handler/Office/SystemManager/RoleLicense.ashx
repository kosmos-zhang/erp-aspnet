<%@ WebHandler Language="C#" Class="RoleLicense" %>

using System;
using System.Web;
using XBase.Business.Office.SystemManager;
using XBase.Common;
public class RoleLicense : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        JsonClass jc=null;
        string CompanCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Id = context.Request.QueryString["str"];
        bool isDelete = RoleFunctionBus.DelRoleFun(CompanCD,Id);
        //删除成功时
        if (isDelete)
        {
            jc = new JsonClass("success", "删除成功！", 1);
        }
        else
        {
            jc = new JsonClass("faile", "删除失败，请联系管理员！", 0);
        }
            context.Response.Write(jc);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
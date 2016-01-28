<%@ WebHandler Language="C#" Class="RoleCount" %>

using System;
using System.Web;
using XBase.Business .Common;
using XBase.Common;
using System.Data;
using XBase.Business.Office.SystemManager;
public class RoleCount : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string Code = context.Request.QueryString["strcode"];//获取序列号
        string CommonTable = context.Request.QueryString["TableName"];
        string ColName = context.Request.QueryString["colname"];
        bool result = PrimekeyVerifyBus.CheckCodeUniq(CommonTable,ColName,Code);
        //唯一性字段是否存在存在
        JsonClass jc;
        if (!result)
        {
            jc = new JsonClass("faile", "角色名称已经存在，请重新输入！", 3);
        }
        else
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            DataTable dt = RoleInfoBus.GetMaxRoleCount(CompanyCD);
            if (dt.Rows[0][0].ToString() == "1")
            {
                jc = new JsonClass("faile", "无法新增角色，已达到最大值！", 3);
            }
            else
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
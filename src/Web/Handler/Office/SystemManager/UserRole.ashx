<%@ WebHandler Language="C#" Class="UserRole" %>
using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.SystemManager;
using XBase.Model.Office.SystemManager;
using System.Collections;

public class UserRole : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        JsonClass jc;
        string RoleID = context.Request.QueryString["RoleIDArray"];
        string UserID = context.Request.QueryString["UserID"];
        string action = context.Request.QueryString["action"];
        object obj = context.Request.QueryString["flag"];
        bool isDelete = false;
        if (obj != null)
        {
            string userid = context.Request.QueryString["userid"].Trim();
            string roleid = context.Request.QueryString["roleid"].Trim ();
            roleid = roleid.Substring(1, roleid.Length-1);
            userid = userid.Substring(1, userid.Length-1);
            for (int j = 0; j < roleid.Length; j++)
            {
                if (roleid[j] == '0')
                {
                    jc = new JsonClass("没有给用户分配的角色无法删除！", "", 0);
                    context.Response.Write(jc);
                    return;
                } 
                
            }
                isDelete = UserRoleBus.DeleteUserRole(userid, roleid);
            //删除成功时
            if (isDelete)
            {
                jc = new JsonClass("删除成功", "", 1);
            }
            else
            {
                jc = new JsonClass("删除失败", "", 0);
            }
            context.Response.Write(jc);
           return;
        }
        UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string CompanyCD = UserInfo.CompanyCD;
        string[] RoleIDArray = RoleID.Split(',');
        if (action == "Edit")
        {
            string result_ = "false";
            if (action == "Edit")
            {
                result_ = UserRoleBus.InsertUserRoleWithList(UserID, CompanyCD, RoleIDArray).ToString();
            }
            context.Response.Write(result_);
            return;
        }
      
       bool result = UserRoleBus.InsertUserRoleWithList(UserID, CompanyCD, RoleIDArray);
        if (result)
         {
             jc = new JsonClass("保存成功", "", 1);
         }
         else
         {
             jc = new JsonClass("保存失败", "", 0);
         }
         context.Response.Write(jc);

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
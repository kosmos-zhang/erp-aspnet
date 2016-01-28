<%@ WebHandler Language="C#" Class="ChangePsd" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.SystemManager;
public class ChangePsd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        JsonClass jc;
        string userID = context.Request.QueryString["UserID"];
        //string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //获得页面输入的密码
        string Password = context.Request.QueryString["Password"];
        string OldPassword = StringUtil.EncryptPasswordWitdhMD5(context.Request.QueryString["OldPassword"].ToString().Trim());
        if (context.Request.QueryString["flag"] != null)
        {
            if (!Password.Equals(OldPassword))
            {
                jc = new JsonClass("原密码不正确！", "", 3);
                context.Response.Write(jc);
                return;
            }
            else
            {
                jc = new JsonClass("原密码正确！", "", 4);
                context.Response.Write(jc);
                return;
            }
        }
        else
        {
            string NewPassword = StringUtil.EncryptPasswordWitdhMD5(context.Request.QueryString["NewPassword"].ToString().Trim());
            string CommanCD = context.Request.QueryString["CommanyCD"];
            if (UserInfoBus.ModifyUserInfoPwd(userID, NewPassword, CommanCD,userID))
            {
                UserInfoBus.ModifyUserInfoPwdLog(userID, NewPassword,CommanCD,userID);
                jc = new JsonClass("保存成功", NewPassword, 1);
            }
            else
            {
                jc = new JsonClass("保存失败", "", 0);
            }
            context.Response.Write(jc);
        }
         
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
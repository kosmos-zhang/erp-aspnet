<%@ WebHandler Language="C#" Class="ChangePsd" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.SystemManager;
using System.Data;
public class ChangePsd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        JsonClass jc;
        string userID = context.Request.QueryString["UserID"];
        string CommanCD = context.Request.QueryString["CommanyCD"];
       
        
        //string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //获得页面输入的密码
        string Password = context.Request.QueryString["NewPassword"].ToString().Trim();
        string NewPassword = StringUtil.EncryptPasswordWitdhMD5(context.Request.QueryString["NewPassword"].ToString().Trim());
        DataTable dt = UserInfoBus.GetUserInfoByID(userID, CommanCD);
        if (dt.Rows.Count > 0)
        {
            if (NewPassword == dt.Rows[0]["password"].ToString())
            {
                jc = new JsonClass("新密码不能与原密码相同", Password, 4);
                context.Response.Write(jc);
                return;
            }
        }
       
        string hfuserid = context.Request.QueryString["hfuserid"];
        if (UserInfoBus.ModifyUserInfoPwd(userID, NewPassword, CommanCD,hfuserid))
        {
            UserInfoBus.ModifyUserInfoPwdLog(userID, NewPassword,CommanCD,hfuserid);
            jc = new JsonClass("保存成功", Password, 1);
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
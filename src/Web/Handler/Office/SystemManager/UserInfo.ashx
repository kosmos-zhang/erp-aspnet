<%@ WebHandler Language="C#" Class="UserInfo" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.SystemManager;
using XBase.Model.Office.SystemManager;

public class UserInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
       string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
       string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        JsonClass jc;
        UserInfoModel Model = new UserInfoModel();
        Model.CompanyCD = companyCD;
        string UserID = context.Request.QueryString["UserId"].ToString().Trim();
        if (context.Request.QueryString["flag"] != null)
        {
            string strID = context.Request.QueryString["UserId"].ToString().Trim();
            string Fno = "";

            string[] ProductsNo = strID.Split(',');
            for (int i = 0; i < ProductsNo.Length; i++)
            {
                string ID = ProductsNo[i].ToString();
                string[] No = ID.Split('|');
                string Fsta = No[1].ToString();
                string CanDelFlag = No[2].ToString();
                if (CanDelFlag == "1900-1-1") CanDelFlag = "";
                if (!string.IsNullOrEmpty(CanDelFlag))
                {
                    jc = new JsonClass("已使用的用户不能删除！", "", 0);
                    context.Response.Write(jc);
                    return;
                }
                if (Fsta == "1")
                {
                    jc = new JsonClass("系统管理员不能被删除！", "", 0);
                    context.Response.Write(jc);
                    return;
                }
                else
                {
                    string flowflag = No[0].ToString();
                    Fno += flowflag + ",";
                }
            }



            //string userID_ = Fno.Substring(0, Fno.Length-1);
            //string[] No = strID.Split(',');
            string[] userID_ = Fno.Split(',');
            for (int i = 0; i < userID_.Length; i++)
            {
                if (userID_[i].ToString()==userid)
                {
                    jc = new JsonClass("正在使用的用户不能删除！", "", 0);
                    context.Response.Write(jc);
                    return;
                }
                
            }
            string tuserid = Fno.Substring(0, Fno.Length - 1);
            bool isDelete = UserInfoBus.DeleteUserInfo(tuserid); 
            //bool isDelete = true;
           // 删除成功时
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
        Model.UserID = UserID;
        Model.UserName = context.Request.QueryString["UserName"].ToString().Trim();
        if (context.Request.QueryString["EmployeeID"].ToString().Trim() != "0")
        {
            Model.EmployeeID = int.Parse(context.Request.QueryString["EmployeeID"].ToString());
        }
        if (!string.IsNullOrEmpty(context.Request.QueryString["txtOpenDate"].ToString().Trim()))
            Model.OpenDate = DateTime.Parse(context.Request.QueryString["txtOpenDate"].ToString().Trim());
        if (!string.IsNullOrEmpty(context.Request.QueryString["txtCloseDate"].ToString().Trim()))
            Model.CloseDate = DateTime.Parse(context.Request.QueryString["txtCloseDate"].ToString().Trim());
        Model.Remark = context.Request.QueryString["Remark"].ToString().Trim();
        Model.LockFlag = context.Request.QueryString["LockFlag"].ToString().Trim();
        Model.UsedStatus = context.Request.QueryString["UsedStatus"].ToString().Trim();
        Model.IsHardValidate = context.Request.QueryString["IsHardValidate"].ToString().Trim();
        if (UserInfoBus.GetUserCount(companyCD, Model.UserID) > 0)
        {
            #region 修改用户信息
            Model.IsInsert = false;
            if (UserInfoBus.ModifyUserInfo(Model))
            {
                jc = new JsonClass("保存成功", "", 1);
            }
            else
            {
                jc = new JsonClass("保存失败", "", 0);
            }
            context.Response.Write(jc);
            #endregion
        }
        else
        {
            //密码加密
            string password = StringUtil.EncryptPasswordWitdhMD5(context.Request.QueryString["Password"].ToString().Trim());
            Model.Password = password;
            #region 添加用户信息
            int isMaxUser = UserInfoBus.IsMaxUserCount(companyCD);
            Model.IsInsert = true;
            if (UserInfoBus.ModifyUserInfo(Model))
            {
                jc = new JsonClass("保存成功", "", 1);
            }
            else
            {
                jc = new JsonClass("保存失败", "", 0);
            }
            context.Response.Write(jc);

            #endregion
        }
        
   
    }

   
    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
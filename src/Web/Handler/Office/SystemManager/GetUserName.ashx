<%@ WebHandler Language="C#" Class="GetUserName" %>

using System;
using System.Web;
using System.Data;
using XBase.Business.Office.SystemManager;
using XBase.Common;
public class GetUserName : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        try
        {
        string UserID = context.Request.QueryString["userid"];
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string checkboxtext = null;
        DataTable dt = UserInfoBus.GetUserInfo(CompanyCD, UserID);
        if (UserID != "")
        {
            //执行sql语句
            DataTable DataTable = UserRoleBus.GetUserRoleInfo(UserID);
            //列出查询数据
            string[] RoleChecked = new string[DataTable.Rows.Count];
            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                DataRow row = DataTable.Rows[i];
                RoleChecked[i] = row["RoleID"].ToString();
            }
            //获取该用户所属企业下所有角色信息
            //UserInfoUtil UserInfo = "admin ";
            DataTable RoleInfo = RoleInfoBus.GetRoleInfo(CompanyCD);
            string[] RoleID = new string[RoleInfo.Rows.Count];
            string[] RoleName = new string[RoleInfo.Rows.Count];
            for (int i = 0; i < RoleInfo.Rows.Count; i++)
            {
                DataRow row = RoleInfo.Rows[i];
                RoleID[i] = row["RoleID"].ToString();
                RoleName[i] = row["RoleName"].ToString();
            }

            //显示角色信息
            string RoleIDList = "";
            for (int i = 0; i < RoleID.Length; i++)
            {
                RoleIDList += "<input type='checkbox' id='RoleID' value='" + RoleID[i] + "'";
                if (isChecked(RoleID[i], RoleChecked))
                {
                    RoleIDList += " checked";
                }
                RoleIDList += ">" + RoleName[i];
            }
           checkboxtext = RoleIDList.Trim();
           string result = dt.Rows[0]["UserID"].ToString();
           result = result + "|" + checkboxtext;
           context.Response.Write(result);
        }
     
        }
        catch
        {
        }
    }







    private static bool isChecked(string RoleID, string[] RoleIDArray)
    {
        for (int i = 0; i < RoleIDArray.Length; i++)
        {
            if (RoleIDArray[i] == RoleID)
            {
                return true;
            }
        }
        return false;
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}
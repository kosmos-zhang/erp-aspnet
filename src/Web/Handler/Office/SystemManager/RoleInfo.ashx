<%@ WebHandler Language="C#" Class="RoleInfo" %>
using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.SystemManager;
using XBase.Model.Office.SystemManager;
using System.Data;
using XBase.Business;
using XBase.Business.Common;
public class RoleInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        JsonClass jc;
        int roleid =0;
        object obj = context.Request.QueryString["flag"];
        if (obj!=null)
        {
            string role_id = context.Request.QueryString["RoleID"].Trim();
            string CompanyID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string userID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            int[] role = RoleInfoBus.GetRoleInfoArray(userID, CompanyID);
            string[] No = role_id.Split(',');
            for (int i = 0; i < No.Length; i++)
            {
                string no = No[i];
                for (int j = 0; j < role.Length; j++)
                {
                    if (role[j] == int.Parse(No[i]))
                    {

                        jc = new JsonClass("此角色正在使用！无法删除", "", 0);
                        context.Response.Write(jc);
                        return;
                    } 
                }
            }
            bool isDelete = RoleInfoBus.DeleteRoleInfo(role_id, CompanyID);
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
        string RoleName = context.Request.QueryString["RoleName"].Trim();
        string CompanyCD = context.Request.QueryString["CompanyCD"].Trim();
        string Remark = context.Request.QueryString["Remark"].Trim();
        string action = context.Request.QueryString["action"].Trim();
        string actionedit = context.Request.QueryString["actionedit"].Trim();
        if (actionedit == "edit")
            roleid = int.Parse(context.Request.QueryString["roleid"].Trim());
        else roleid = int.Parse(context.Request.QueryString["id"].Trim());
        RoleInfoModel model = new RoleInfoModel();
        model.CompanyCD = CompanyCD;
        model.RoleName = RoleName;
        model.Remark = Remark;
        RoleInfoBus RoleInfo = new RoleInfoBus();
       if (action == "Edit"||actionedit=="edit")
        {
            #region 修改角色信息
            if (RoleInfoBus.UpdateRoleInfo(model, roleid))
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
        else if (action == "Add")
        {
            #region 添加角色信息
            string tempID = "0";
            string CommonTable = context.Request.QueryString["TableName"].Trim();
            string ColName = context.Request.QueryString["columname"].Trim();
            bool result = PrimekeyVerifyBus.CheckCodeUniq(CommonTable, ColName, RoleName);
            //唯一性字段是否存在存在
            if (!result)
            {
                jc = new JsonClass("角色名称已经存在，请重新输入！", "", 3);
                context.Response.Write(jc);
                return;
            }
            else
            {
                DataTable dt = RoleInfoBus.GetMaxRoleCount(CompanyCD);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    jc = new JsonClass("无法新增角色，已达到最大值！", "", 3);
                    context.Response.Write(jc);
                    return;
                }
                else
                    jc = new JsonClass("success", "", 1);

            }
            
            if (RoleInfoBus.InsertRoleInfo(model,out tempID))
            {
                jc = new JsonClass("保存成功", "1",int.Parse(tempID));
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
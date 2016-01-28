using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using XBase.Common;
using XBase.Business.Office.SystemManager;

public partial class Pages_Office_SystemManager_UserRole_Modify : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//待修改
        //    string requestParam = Request.QueryString.ToString();
        //    hidModuleID.Value = ConstUtil.Menu_SerchUserInfo;
        //    int firstIndex = requestParam.IndexOf("&");
        //    //返回回来时
        //    if (firstIndex > 0)
        //    {
        //        string searchCondition = requestParam.Substring(firstIndex);
        //        //去除参数
        //        searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.Menu_AddUserRole, string.Empty);
        //        //设置检索条件
        //        hidSearchCondition.Value = searchCondition;
        //        InitPage(Request.QueryString["UserIDFlag"]);
        //    }
        //    this.txtUserID.Value = Request.QueryString["UserIDFlag"];
        //}
        string ActionValue ="Edit";
        if (ActionValue == "" || ActionValue == null)
        {
            ActionValue = ActionUtil.Add.ToString();

        }
        action.Value = ActionValue;
        //修改时，列出数据
        if (ActionValue == ActionUtil.Edit.ToString())
        {
            string UserID = Request.QueryString["UserIDFlag"];
            if (UserID != "")
            {
                //执行sql语句
                DataTable DataTable = UserRoleBus.GetUserRoleInfo(UserID);
                //列出查询数据
                string[] RoleChecked = new string[DataTable.Rows.Count];
                for (int i = 0; i < DataTable.Rows.Count;i++ )
                {
                    DataRow row = DataTable.Rows[i];
                    //UserName = row["UserName"].ToString();
                    RoleChecked[i] = row["RoleID"].ToString();
                }
                string requestParam = Request.QueryString.ToString();
                hidModuleID.Value = ConstUtil.Menu_SerchUserRole;
                int firstIndex = requestParam.IndexOf("&");
                //返回回来时
                if (firstIndex > 0)
                {
                    string searchCondition = requestParam.Substring(firstIndex);
                    //去除参数
                    searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.Menu_AddUserRole, string.Empty);
                    //设置检索条件
                    hidSearchCondition.Value = searchCondition;
                }
                lbUserID.Text = UserID;
                //lbUserName.Text = UserName;

                //获取该用户所属企业下所有角色信息
                UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                string CompanyCD = UserInfo.CompanyCD;
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
                for (int i = 0; i < RoleID.Length;i++ )
                {
                    RoleIDList += "<input type='checkbox' id='RoleID' value='" + RoleID[i] + "'";
                    if (isChecked(RoleID[i], RoleChecked))
                    {
                        RoleIDList += " checked";
                    }
                    RoleIDList += ">" + RoleName[i];
                }
                LblRoleID.Text = RoleIDList;

            }
            hfUserID.Value = UserID;
        }

    }

    //判断是否被选中
    private static bool isChecked(string RoleID,string[] RoleIDArray)
    {
        for (int i = 0; i < RoleIDArray.Length;i++ )
        {
            if(RoleIDArray[i]==RoleID)
            {
                return true;
            }
        }
        return false;
    }

}

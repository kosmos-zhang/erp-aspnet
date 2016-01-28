using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using XBase.Common;
using XBase.Business.Office.SystemManager;

public partial class Pages_Office_SystemManager_RoleInfo_Add :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//待修改
            string requestParam = Request.QueryString.ToString();
            hidModuleID.Value = ConstUtil.Menu_SerchUserInfo;
            int firstIndex = requestParam.IndexOf("&");
            //返回回来时
            if (firstIndex > 0)
            {
                string searchCondition = requestParam.Substring(firstIndex);
                //去除参数
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.Menu_AddRoleInfo, string.Empty);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
                string RoleID = Request.QueryString["RoleIDFlag"];
                this.txtCompanyCD.Value = companyCD;
                this.txtCompanyCD.Disabled = true;
                hfRoleID.Value = RoleID;
                int Roleid = int.Parse(RoleID);
                InitPage(Roleid);
            }
        }
    }
    private void InitPage(int RoleID )
    {
        DataTable DataTable = RoleInfoBus.GetRoleInfoByID(RoleID);
        if (DataTable.Rows.Count > 0)
        {
            DataRow row = DataTable.Rows[0];
            txtRoleID.Value = row["RoleName"].ToString();
            txtRemark.Value= row["remark"].ToString();
        }
    }
}

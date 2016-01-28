using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using XBase.Common;
using XBase.Business.Office.SystemManager;

public partial class Pages_Office_SystemManager_RoleInfo_Edit : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtCompanyCD.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (!Page.IsPostBack)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            hidModuleID.Value = ConstUtil.Menu_SerchRoleInfo;
            string requestParam = Request.QueryString.ToString();
            //通过参数个数来判断是否从菜单过来
            int firstIndex = requestParam.IndexOf("&");
            //从列表过来时
            if (firstIndex > 0)
            {
                //获取列表的查询条件
                string searchCondition = requestParam.Substring(firstIndex);
                //去除参数
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.Menu_AddRoleInfo, string.Empty);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
                //迁移页面
                btnback.Visible = true;
            }
            else
            {
                btnback.Visible = false;
            }
        }
    }
  
}

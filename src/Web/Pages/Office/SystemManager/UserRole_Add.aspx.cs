using System;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XBase.Business.Office.SystemManager;
using XBase.Common;
public partial class Pages_Office_SystemManager_UserRole_Add : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          BindDplUsreInfo();
          if (!Page.IsPostBack)
          {
              string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
              hidModuleID.Value = ConstUtil.Menu_SerchUserRole;
              string requestParam = Request.QueryString.ToString();
              //通过参数个数来判断是否从菜单过来
              int firstIndex = requestParam.IndexOf("&");
              //从列表过来时
              if (firstIndex > 0)
              {
                  //获取列表的查询条件
                  string searchCondition = requestParam.Substring(firstIndex);
                  //去除参数
                  searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.Menu_AddUserRole, string.Empty);
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
    //判断是否被选中
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
    private void BindDplUsreInfo()
    {
        UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string CompanyCD = UserInfo.CompanyCD;
        DataTable dt = UserInfoBus.GetUserInfo(CompanyCD,"");
        if (dt.Rows.Count > 0)
        {
            Drp_UserInfo.DataTextField = "UserID";
            Drp_UserInfo.DataValueField = "UserID";
            Drp_UserInfo.DataSource = dt;
            Drp_UserInfo.DataBind();
            ListItem Item = new ListItem();
        }
    }

 
}

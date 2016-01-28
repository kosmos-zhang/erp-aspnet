/***********************************************************************
 * 
 * Module Name:Web.UserControl.RoleDropDownList.ascx
 * Current Version: 1.0 
 * Creator: jiangym
 * Auditor:2009-01-15
 * End Date:
 * Description:获取企业角色
 * Version History:
 ***********************************************************************/
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XBase.Business.Office.SystemManager;
using XBase.Common;
public partial class UserControl_RoleDrpControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindRoleInfo();
        }
    }

    #region 根据企业编码获取企业角色
    private void BindRoleInfo()
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        DataTable dt = RoleInfoBus.GetRoleInfo(CompanyCD);
        if (dt.Rows.Count > 0)
        {
            Drp_RoleInfo.DataTextField = "RoleName";
            Drp_RoleInfo.DataValueField = "RoleID";
            Drp_RoleInfo.DataSource = dt;
            Drp_RoleInfo.DataBind();
            ListItem Item = new ListItem();
            Item.Value = "0";
            Item.Text = "--请选择--";
            Drp_RoleInfo.Items.Insert(0, Item);
        }
    }
    #endregion
}

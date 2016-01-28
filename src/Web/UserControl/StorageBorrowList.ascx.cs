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

public partial class UserControl_StorageBorrowList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string companyCD = GetCurrentUserInfo().CompanyCD;
            DataTable dt = XBase.Business.Office.StorageManager.StorageBorrowBus.GetDepot(companyCD);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlDepot.DataSource = dt;
                ddlDepot.DataTextField = "StorageName";
                ddlDepot.DataValueField = "ID";
                ddlDepot.DataBind();
                ddlDepot.Items.Insert(0, new ListItem("--请选择--", "-1"));
            }
            else
                ddlDepot.Items.Insert(0, new ListItem("--暂时无仓库--", "-1"));
        }
    }

    #region 读取当前用户信息
    protected UserInfoUtil GetCurrentUserInfo()
    {
        if (SessionUtil.Session["UserInfo"] != null)
        {
            UserInfoUtil user = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            return user;
        }
        else
            return new UserInfoUtil();
    }
    #endregion

}

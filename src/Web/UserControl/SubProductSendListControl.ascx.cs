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
public partial class UserControl_SubProductSendListControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSubStore();
        }
    }
    protected void BindSubStore()
    {
        XBase.Model.Office.HumanManager.DeptModel model = new XBase.Model.Office.HumanManager.DeptModel();
        model.CompanyCD = UserInfo.CompanyCD;
        DataTable dtSource = XBase.Business.Office.LogisticsDistributionManager.SubProductSendPriceBus.GetSubStore(model);
        ddlSubStore.DataSource = dtSource;
        ddlSubStore.DataTextField = "DeptName";
        ddlSubStore.DataValueField = "ID";
        ddlSubStore.DataBind();

        ddlSubStore.Items.Insert(0,new ListItem("--请选择--", "-1"));
    }
    #region 读取用户信息
    protected UserInfoUtil UserInfo
    {
        get
        {
            if (SessionUtil.Session["UserInfo"] != null)
                return (UserInfoUtil)SessionUtil.Session["UserInfo"];
            else
                return new UserInfoUtil();
        }
    }
    #endregion
}

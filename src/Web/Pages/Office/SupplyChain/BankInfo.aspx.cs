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
public partial class Pages_Office_SupplyChain_BankInfo :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.UserPrincipal.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            this.txtPrincipal.Value = Convert.ToString(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
            txt_CreateDate.Value = Convert.ToString(DateTime.Now.ToShortDateString());
        }
    }
}

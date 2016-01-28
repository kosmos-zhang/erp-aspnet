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

using XBase.Business.Office.SellManager;

public partial class UserControl_SelectSellContractUC : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = XBase.Business.Office.SellManager.SellModuleSelectCurrencyBus.GetCurrencyTypeSetting();
            ConCurrencyType.DataSource = dt;
            ConCurrencyType.DataTextField = "CurrencyName";
            ConCurrencyType.DataValueField = "ID";
            ConCurrencyType.DataBind();
        }
    }
}

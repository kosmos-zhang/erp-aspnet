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
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;

public partial class Pages_Office_PurchaseManager_ProviderProductAdd : BasePage
{
    #region Master Arrive ID
    public int intMasterProviderProductID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["intMasterProviderProductID"], out tempID);
            return tempID;
        }
    }
    #endregion

    string CompanyCD = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        this.txtIndentityID.Value = this.intMasterProviderProductID.ToString();
        if (!Page.IsPostBack)
        {
            hidModuleID.Value = ConstUtil.MODULE_ID_PROVIDERPRODUCTINFO;
        }
    }
}

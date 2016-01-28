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

public partial class Pages_Office_PurchaseManager_ProviderLinkMan_Add : BasePage
{
    #region Master Arrive ID
    public int intMasterProviderLinkManID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["intMasterProviderLinkManID"], out tempID);
            return tempID;
        }
    }
    #endregion

    string CompanyCD = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        this.txtIndentityID.Value = this.intMasterProviderLinkManID.ToString();
        if (!Page.IsPostBack)
        {
            hidModuleID.Value = ConstUtil.MODULE_ID_PurchaseContractInfo;
            BinddrpLinkType();//绑定采购供应商联系人类型

            UserInfoUtil userInfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
            usernametemp.Value = userInfo.UserName;
            datetemp.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUserID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            txtModifiedUserIDReal.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            txtModifiedDate2.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUserID2.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        }
    }

    #region 绑定采购供应商联系人类型
    private void BinddrpLinkType()
    {
        DataTable dt = ProviderLinkManBus.GetdrpLinkType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpLinkType.DataSource = dt;
            drpLinkType.DataTextField = "TypeName";
            drpLinkType.DataValueField = "ID";
            drpLinkType.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpLinkType.Items.Insert(0, Item);
    }
    #endregion

}

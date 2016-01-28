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

public partial class Pages_Office_PurchaseManager_ProviderContactHistory_Add : BasePage
{
    #region Master Arrive ID
    public int intMasterProviderContactHistoryID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["intMasterProviderContactHistoryID"], out tempID);
            return tempID;
        }
    }
    #endregion

    string CompanyCD = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        this.txtIndentityID.Value = this.intMasterProviderContactHistoryID.ToString();
        if (!Page.IsPostBack)
        {
            hidModuleID.Value = ConstUtil.MODULE_ID_PROVIDERCONTRACTHISTORYINFO;
            BinddrpLinkReasonID();//绑定采购供应商联络事由
            //BinddrpLinkMode();//绑定采购供应商联络方式

            #region 采购供应商联络单编号规则
            CodingRuleControl1.CodingType = ConstUtil.CODING_RULE_PURCHASE;
            CodingRuleControl1.ItemTypeID = ConstUtil.CODING_RULE_PURCHASE_PROVIDERLINKMAN;
            //CodingRuleControl1.TableName = "ProviderContactHistory";
            //CodingRuleControl1.ColumnName = "ContactNo";
            #endregion
        }
    }

    #region 绑定采购供应商联络事由
    private void BinddrpLinkReasonID()
    {
        DataTable dt = ProviderContactHistoryBus.GetdrpLinkReasonID();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpLinkReasonID.DataSource = dt;
            drpLinkReasonID.DataTextField = "TypeName";
            drpLinkReasonID.DataValueField = "ID";
            drpLinkReasonID.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpLinkReasonID.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定采购供应商联络方式
    private void BinddrpLinkMode()
    {
        DataTable dt = ProviderContactHistoryBus.GetdrpLinkMode();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpLinkMode.DataSource = dt;
            drpLinkMode.DataTextField = "TypeName";
            drpLinkMode.DataValueField = "ID";
            drpLinkMode.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpLinkMode.Items.Insert(0, Item);
    }
    #endregion
}

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

public partial class Pages_Office_PurchaseManager_PurchaseContract_Add : BasePage
{
    #region Master Arrive ID
    public int intMasterPurchaseContractID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["intMasterPurchaseContractID"], out tempID);
            return tempID;
        }
    }
    #endregion
    string CompanyCD = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
        HiddenMoreUnit.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString();//是否启用多计量单位
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsDisplayPrice)
            IsDisplayPrice.Value = "true";
        else
            IsDisplayPrice.Value = "false";


        this.txtIndentityID.Value = this.intMasterPurchaseContractID.ToString();
        CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsBarCode)
        {
            btnGetGoods.Visible = true;
        }
        else
        {
            btnGetGoods.Visible = false;
        }
        if (!Page.IsPostBack)
        {
            //新建修改采购合同模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_PurchaseContractInfo;
            //if (this.hidIsliebiao.Value != "")
            //{
            //    this.chkIsZzs.Checked = true;
            //}
            txtCreateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            BindDrpTypeID();
            BindDrpTakeType();
            BindDrpCarryType();
            BindDrpPayType();
            bindDrpMoneyType();
            bingdrpApplyReason();//绑定原因
            binddrpCurrencyType();//绑定币种

            UserInfoUtil userInfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
            UserID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            UserName.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            SystemTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtCreateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            usernametemp.Value = userInfo.UserName;
            txtCreatorReal.Value = userInfo.EmployeeName;
            datetemp.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtCreateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedDate2.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUserID2.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            txtModifiedUserID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            //txtModifiedUserIDReal.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserName;
            txtModifiedUserIDReal.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();

            #region 采购合同单据规则
            CodingRuleControl1.CodingType = ConstUtil.CODING_RULE_PURCHASE;
            CodingRuleControl1.ItemTypeID = ConstUtil.CODING_RULE_PURCHASE_CONTRACT;
            //CodingRuleControl1.TableName = "PurchaseContract";
            //CodingRuleControl1.ColumnName = "ContractNo";
            usernametemp.Value = userInfo.UserName;
            datetemp.Value = DateTime.Now.ToString("yyyy-MM-dd");
            #endregion

            #region 提交审批调用方法
            FlowApply1.BillTypeFlag = ConstUtil.BILL_TYPEFLAG_PURCHASE;
            FlowApply1.BillTypeCode = ConstUtil.BILL_TYPEFLAG_PURCHASE_CONTRACT;
            #endregion
        }
    }

    #region 绑定列表采购类别
    private void BindDrpTypeID()
    {
        DataTable dt = PurchaseContractBus.GetddlTypeID();
        if (dt != null && dt.Rows.Count > 0)
        {
            DrpTypeID.DataSource = dt;
            DrpTypeID.DataTextField = "TypeName";
            DrpTypeID.DataValueField = "ID";
            DrpTypeID.DataBind();
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        DrpTypeID.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定列表交货方式
    private void BindDrpTakeType()
    {
        DataTable dt = PurchaseContractBus.GetDrpTakeType();
        if (dt != null && dt.Rows.Count > 0)
        {
            DrpTakeType.DataSource = dt;
            DrpTakeType.DataTextField = "TypeName";
            DrpTakeType.DataValueField = "ID";
            DrpTakeType.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        DrpTakeType.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定列表运送方式
    private void BindDrpCarryType()
    {
        DataTable dt = PurchaseContractBus.GetDrpCarryType();
        if (dt != null && dt.Rows.Count > 0)
        {
            DrpCarryType.DataSource = dt;
            DrpCarryType.DataTextField = "TypeName";
            DrpCarryType.DataValueField = "ID";
            DrpCarryType.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        DrpCarryType.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定列表结算方式
    private void BindDrpPayType()
    {
        DataTable dt = PurchaseContractBus.GetDrpPayType();
        if (dt != null && dt.Rows.Count > 0)
        {
            DrpPayType.DataSource = dt;
            DrpPayType.DataTextField = "TypeName";
            DrpPayType.DataValueField = "ID";
            DrpPayType.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        DrpPayType.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定列表支付方式
    private void bindDrpMoneyType()
    {
        DataTable dt = PurchaseContractBus.GetDrpMoneyType();
        if (dt != null && dt.Rows.Count > 0)
        {
            DrpMoneyType.DataSource = dt;
            DrpMoneyType.DataTextField = "TypeName";
            DrpMoneyType.DataValueField = "ID";
            DrpMoneyType.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        DrpMoneyType.Items.Insert(0, Item);
    }
    #endregion


    #region 绑定原因
    private void bingdrpApplyReason()
    {
        DataTable dt = PurchaseContractBus.GetDrpApplyReason();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpApplyReason.DataSource = dt;
            drpApplyReason.DataTextField = "CodeName";
            drpApplyReason.DataValueField = "ID";
            drpApplyReason.DataBind();
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpApplyReason.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定币种
    private void binddrpCurrencyType()
    {
        DataTable dt = PurchaseContractBus.GetCurrenyType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpCurrencyType.DataSource = dt;
            drpCurrencyType.DataTextField = "CurrencyName";
            drpCurrencyType.DataValueField = "hhh";
            drpCurrencyType.DataBind();

            string aaa = drpCurrencyType.Value;
            CurrencyTypeID.Value = aaa.Split('_')[0];
            txtRate.Text = aaa.Split('_')[1];
        }
    }
    #endregion
}

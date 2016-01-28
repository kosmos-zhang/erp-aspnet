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
using XBase.Business.Office.CustManager;
using XBase.Business.Common;

public partial class Pages_Office_CustManager_Cust_Edit : BasePage
{
    string CompanyCD = "";
    //string CompanyCD = "AAAAAA";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            BindCustType();
            BindCustCreditGrade();
            BindCustLinkCycle();
            BindCustCountry();
            BindCustTakeType();
            BindCustCarryType();
            BindCustPayType();
            BindCustCurrencyType();
            BindMoneyType();
            BindCustArea();
        }
    }

    #region 绑定客户类别的方法
    private void BindCustType()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_INFO_CUSTTYPE);
        if (dt.Rows.Count > 0)
        {
            ddlCustType.DataTextField = "TypeName";
            ddlCustType.DataValueField = "ID";
            ddlCustType.DataSource = dt;
            ddlCustType.DataBind();
           
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlCustType.Items.Insert(0, Item);
    }
    #endregion    

    #region 绑定客户地区的方法
    private void BindCustArea()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_INFO_AREAID);
        if (dt.Rows.Count > 0)
        {
            ddlArea.DataTextField = "TypeName";
            ddlArea.DataValueField = "ID";
            ddlArea.DataSource = dt;
            ddlArea.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlArea.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定客户优质级别的方法
    private void BindCustCreditGrade()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_INFO_CREDITGRADE);
        if (dt.Rows.Count > 0)
        {
            ddlCreditGrade.DataTextField = "TypeName";
            ddlCreditGrade.DataValueField = "ID";
            ddlCreditGrade.DataSource = dt;
            ddlCreditGrade.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlCreditGrade.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定客户联络期限的方法
    private void BindCustLinkCycle()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_INFO_LINKCYCLE);
        if (dt.Rows.Count > 0)
        {
            ddlLinkCycle.DataTextField = "TypeName";
            ddlLinkCycle.DataValueField = "ID";
            ddlLinkCycle.DataSource = dt;
            ddlLinkCycle.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlLinkCycle.Items.Insert(0, Item);
    }
    #endregion

    #region  绑定国家地区的方法
    private void BindCustCountry()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CODE_TYPE_HUMAN, ConstUtil.CODE_TYPE_COUNTRY);
        if (dt.Rows.Count > 0)
        {
            ddlCountry.DataTextField = "TypeName";
            ddlCountry.DataValueField = "ID";
            ddlCountry.DataSource = dt;
            ddlCountry.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlCountry.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定交货方式的方法
    private void BindCustTakeType()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.SELL_TYPE_SELL, ConstUtil.SELL_TYPE_TAKETYPE);
        if (dt.Rows.Count > 0)
        {
            ddlTakeType.DataTextField = "TypeName";
            ddlTakeType.DataValueField = "ID";
            ddlTakeType.DataSource = dt;
            ddlTakeType.DataBind();
           
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlTakeType.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定运货方式的方法
    private void BindCustCarryType()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.SELL_TYPE_SELL, ConstUtil.SELL_TYPE_CARRYTYPE);
        if (dt.Rows.Count > 0)
        {
            ddlCarryType.DataTextField = "TypeName";
            ddlCarryType.DataValueField = "ID";
            ddlCarryType.DataSource = dt;
            ddlCarryType.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlCarryType.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定结算方式的方法
    private void BindCustPayType()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_INFO_PAYTYPE);
        if (dt.Rows.Count > 0)
        {
            ddlPayType.DataTextField = "TypeName";
            ddlPayType.DataValueField = "ID";
            ddlPayType.DataSource = dt;
            ddlPayType.DataBind();
           
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlPayType.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定支付方式的方法
    private void BindMoneyType()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_INFO_MONEYTYPE);
        if (dt.Rows.Count > 0)
        {
            ddlMoneyType.DataTextField = "TypeName";
            ddlMoneyType.DataValueField = "ID";
            ddlMoneyType.DataSource = dt;
            ddlMoneyType.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlMoneyType.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定币种的方法
    private void BindCustCurrencyType()
    {
        DataTable dt = CustInfoBus.GetCustCurrencyType(CompanyCD);
        if (dt.Rows.Count > 0)
        {
            ddlCurrencyType.DataTextField = "CurrencyName";
            ddlCurrencyType.DataValueField = "ID";
            ddlCurrencyType.DataSource = dt;
            ddlCurrencyType.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlCurrencyType.Items.Insert(0, Item);
    }
    #endregion
}

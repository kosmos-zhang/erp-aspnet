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

public partial class Pages_Office_CustManager_Cust_Add :BasePage
{
    string CompanyCD = "";//((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
    //string CompanyCD = "AAAAAA";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ddlCustNo.CodingType = ConstUtil.CUST_CODINGTYPE;
            ddlCustNo.ItemTypeID = ConstUtil.CUST_ITEMTYPEID;

            txtCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;//建单人姓名
            txtCreatedDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUser.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改人
            txtModifiedDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUser0.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改人
            txtModifiedDate0.Value = DateTime.Now.ToString("yyyy-MM-dd");

            BindCustType();//绑定客户类别
            BindCustCreditGrade();//绑定客户优质级别
            BindCustLinkCycle();//绑定客户联络期限
            BindCustCountry();//绑定国家地区
            BindCustTakeType();//绑定交货方式
            BindCustCarryType();//绑定运货方式
            BindCustPayType();//绑定结算方式
            BindCustCurrencyType();//绑定币种
            BindMoneyType();//支付方式
            BindCustArea();//客户地区
            BindLinkManLinkType();//联系人类型

            //民族
            CodeTypeDrpControl1.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            CodeTypeDrpControl1.TypeCode = ConstUtil.CODE_TYPE_NATIONAL;
            CodeTypeDrpControl1.IsInsertSelect = true;

            //学历
            CodeTypeDrpControl2.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            CodeTypeDrpControl2.TypeCode = ConstUtil.CODE_TYPE_CULTURE;
            CodeTypeDrpControl2.IsInsertSelect = true;

            //专业
            CodeTypeDrpControl3.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            CodeTypeDrpControl3.TypeCode = ConstUtil.CODE_TYPE_PROFESSIONAL;
            CodeTypeDrpControl3.IsInsertSelect = true;
        }
    }

    #region 绑定联系人类型
    private void BindLinkManLinkType()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_LINK_LINKTYPE);
        if (dt.Rows.Count > 0)
        {
            ddlLinkType.DataTextField = "TypeName";
            ddlLinkType.DataValueField = "ID";
            ddlLinkType.DataSource = dt;
            ddlLinkType.DataBind();

        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlLinkType.Items.Insert(0, Item);
    }
    #endregion

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

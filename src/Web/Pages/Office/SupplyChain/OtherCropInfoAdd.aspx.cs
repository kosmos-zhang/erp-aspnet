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
using XBase.Business.Office.SystemManager;
using System.Xml.Linq;
using XBase.Business.Office.FinanceManager;
using XBase.Model.Office.SupplyChain;
using XBase.Common;
public partial class Pages_Office_SupplyChain_OtherCropInfoAdd : BasePage
{
    private string TypeFlag = "";//大类定义
    private string TypeCode = "";//小类定义
    #region Master Product ID
    public int intOtherCorpInfoID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["intOtherCorpInfoID"], out tempID);
            return tempID;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        BindSel();
        BindArea();
        BindPayType();
        BindMoney();
        BindCurrencyType();
        if (!IsPostBack)
        {
            hidModuleID.Value = ConstUtil.Menu_OtherCorpInfo;
            txt_CreateDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
            this.UserPrincipal.Text =((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            this.txtPrincipal.Value =Convert.ToString(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
            string requestParam = Request.QueryString.ToString();

            int firstIndex = requestParam.IndexOf("&");
            //从列表过来时
            if (firstIndex > 0)
            {
                //获取列表的查询条件
                string searchCondition = requestParam.Substring(firstIndex);
                //去除参数
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.Menu_OtherCorpInfoAdd, string.Empty);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
                //迁移页面
            }
            if (Request["intOtherCorpInfoID"] != "" && Request["intOtherCorpInfoID"]!=null)
            {
                //通过参数个数来判断是否从菜单过来
                OtherCorpInfoModel model=new OtherCorpInfoModel ();
                model = XBase.Business.Office.SupplyChain.OtherCorpInfoBus.GetOtherCorpById(int.Parse(Request["intOtherCorpInfoID"]));
                sel_BigType.Value= model.BigType  ;        
                txt_CustNo.Value   =model.CustNo;
                txt_CustName.Text = model.CustName;
                txt_CorpNam.Value = model.CorpNam;
                txt_PYShort.Value = model.PYShort;
                txt_CustNote.Value = model.CustNote;
                sel_AreaID.SelectedValue = model.AreaID;
                txt_CompanyType.Value = model.CompanyType;
                txt_StaffCount.Text = model.StaffCount;   
               txt_SetupDate.Value      =model.SetupDate;
               txt_ArtiPerson.Value = model.ArtiPerson;
               txt_SetupMoney.Text = model.SetupMoney;
               txt_SetupAddress.Value = model.SetupAddress;
               txt_CapitalScale.Value = model.CapitalScale;
               txt_SaleroomY.Text = model.SaleroomY;
               txt_ProfitY.Text = model.ProfitY;
               txt_TaxCD.Value = model.TaxCD;
               txt_BusiNumber.Text = model.BusiNumber;
               txt_SellArea.Value = model.SellArea;
               sel_CountryID.SelectedValue = model.CountryID;
               txt_Province.Text = model.Province;
               txt_City.Value = model.City;
               txt_Post.Value = model.Post;
               txt_ContactName.Text = model.ContactName;
               txt_Tel.Text = model.Tel;
               txt_Fax.Value = model.Fax;
               txt_Mobile.Text = model.Mobile;
               txt_email.Text = model.email;
               txt_Addr.Value = model.Addr;
               sel_BillType.Value = model.BillType;
               sel_PayType.SelectedValue = model.PayType;
               sel_MoneyType.SelectedValue = model.MoneyType;
               sel_CurrencyType.SelectedValue = model.CurrencyType;
               txt_Remark.Text = model.Remark;
               sel_UsedStatus.Value = model.UsedStatus;
                txtPrincipal.Value = model.Creator;
                txt_CreateDate.Text = model.CreateDate;
                this.UserPrincipal.Text = model.EmployeeName;
                chk_isTax.Checked = model.isTax == "1" ? true : false;
            }
            else 
            {
               
            }
           
        }
    }
    /// <summary>
    /// 国家地区
    /// </summary>
    private void BindArea()
    {
         TypeFlag = "2";//人事
         TypeCode = "3";//国家地区
         DataTable dt = CodePublicTypeBus.GetCodePublicByCode(TypeFlag, TypeCode);
        if (dt.Rows.Count > 0)
        {
            this.sel_CountryID.DataTextField = "TypeName";
            sel_CountryID.DataValueField = "ID";
            sel_CountryID.DataSource = dt;
            sel_CountryID.DataBind();
        }

    }
    /// <summary>
    /// 所在区域
    /// </summary>
    private void BindSel()
    {
     TypeFlag = "4";//客户
     TypeCode = "12";//区域
        DataTable dt = CodePublicTypeBus.GetCodePublicByCode(TypeFlag, TypeCode);
        if (dt.Rows.Count > 0)
        {
            this.sel_AreaID.DataTextField = "TypeName";
            sel_AreaID.DataValueField = "ID";
            sel_AreaID.DataSource = dt;
            sel_AreaID.DataBind();
        }

    }
    private void BindPayType()
    {
        TypeCode = "11";
        TypeFlag = "4";
        DataTable dt = CodePublicTypeBus.GetCodePublicByCode(TypeFlag, TypeCode);
        if (dt.Rows.Count > 0)
        {
            this.sel_PayType.DataTextField = "TypeName";
            sel_PayType.DataValueField = "ID";
            sel_PayType.DataSource = dt;
            sel_PayType.DataBind();
           
        }
        ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
        sel_PayType.Items.Insert(0, Item);
    }
    /// <summary>
    /// 支付方式
    /// </summary>
    private void BindMoney()
    {
        TypeFlag = "4";
        TypeCode = "14";
        DataTable dt = CodePublicTypeBus.GetCodePublicByCode(TypeFlag, TypeCode);
        if (dt.Rows.Count > 0)
        {
            this.sel_MoneyType.DataTextField = "TypeName";
            sel_MoneyType.DataValueField = "ID";
            sel_MoneyType.DataSource = dt;
            sel_MoneyType.DataBind();
            
        }
        ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
        sel_MoneyType.Items.Insert(0, Item);
    }
    /// <summary>
    /// 币种
    /// </summary>
    private void BindCurrencyType()
    {
        DataTable dt = CurrTypeSettingBus.GetCurrTypeByCompanyCD();
        if (dt.Rows.Count > 0)
        {
            this.sel_CurrencyType.DataTextField = "CurrencyName";
            sel_CurrencyType.DataValueField = "ID";
            sel_CurrencyType.DataSource = dt;
            sel_CurrencyType.DataBind();
        }
    }
}

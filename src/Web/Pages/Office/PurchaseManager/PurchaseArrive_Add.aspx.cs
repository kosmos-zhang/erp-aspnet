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

/// <summary>
/// 采购到货通知单
/// </summary>
public partial class Pages_Office_PurchaseManager_PurchaseArrive_Add : BasePage
{

    #region 变量

    /// <summary>
    /// 是否启用多计量单位(true：启用；false：不启用)
    /// </summary>
    private bool _isMoreUnit = false;

    /// <summary>
    /// 是否启用超订单发货(true：启用；false：不启用)
    /// </summary>
    private bool _isOverOrder = false;

    /// <summary>
    /// 小数位数
    /// </summary>
    private int _selPoint = 2;
    #endregion

    #region 属性
    /// <summary>
    /// Master Arrive ID
    /// </summary>
    public int intMasterArriveID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["intMasterArriveID"], out tempID);
            return tempID;
        }
    }

    /// <summary>
    /// 是否启用多计量单位(ture：启用；false：不启用)
    /// </summary>
    public bool IsMoreUnit
    {
        get
        {
            return _isMoreUnit;
        }
        set
        {
            _isMoreUnit = value;
        }
    }

    /// <summary>
    /// 是否启用超订单发货(ture：启用；false：不启用)
    /// </summary>
    public bool IsOverOrder
    {
        get
        {
            return _isOverOrder;
        }
        set
        {
            _isOverOrder = value;
        }
    }

    /// <summary>
    /// 小数位数
    /// </summary>
    public int SelPoint
    {
        get
        {
            return _selPoint;
        }
        set
        {
            _selPoint = value;
        }
    }
    #endregion

    #region 事件、方法

    #region 页面

    #region 事件

    /// <summary>
    /// 加载窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo = SessionUtil.Session["UserInfo"] as UserInfoUtil;
        // 多计量单位控制
        _isMoreUnit = UserInfo.IsMoreUnit;
        // 超订单发货
        _isOverOrder = UserInfo.IsOverOrder;
        // 小数位数
        _selPoint = int.Parse(UserInfo.SelPoint);
        this.txtIndentityID.Value = this.intMasterArriveID.ToString();
        if (UserInfo.IsBarCode)
        {
            btnGetGoods.Visible = true;
        }
        else
        {
            btnGetGoods.Visible = false;
        }
        if (!Page.IsPostBack)
        {
            hiddBillingAddModuleid.Value = ConstUtil.MODULE_ID_BILLING_ADD;//开票编辑页面MoudleID
            hidModuleID.Value = ConstUtil.MODULE_ID_PurchaseArriveInfo;
            UserID.Value = UserInfo.EmployeeID.ToString();
            UserName.Value = UserInfo.EmployeeName;
            SystemTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtCreateDate.Value = SystemTime.Value;
            BinddrpTypeID();//绑定采购类别
            BinddrpTakeType();//绑定交货方式
            BinddrpCarryType();//绑定运送方式
            BinddrpPayType();//绑定结算方式
            binddrpMoneyType();//绑定支付方式
            binddrpCurrencyType();//绑定币种
            this.UserPurchaser.Text = UserInfo.EmployeeName;
            this.HidPurchaser.Value = UserInfo.EmployeeID.ToString();
            this.txtArriveDate.Text = SystemTime.Value;

            txtCreator.Value = UserInfo.EmployeeID.ToString();

            #region 采购同货通知单据规则
            CodingRuleControl1.CodingType = ConstUtil.CODING_RULE_PURCHASE;
            CodingRuleControl1.ItemTypeID = ConstUtil.CODING_RULE_PURCHASE_ARRIVE;
            usernametemp.Value = UserInfo.UserName;
            txtCreatorReal.Value = UserInfo.EmployeeName;
            datetemp.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtCreateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUserID.Value = UserInfo.UserID.ToString();
            txtModifiedUserIDReal.Value = UserInfo.UserID.ToString();
            txtModifiedDate2.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUserID2.Value = UserInfo.UserID.ToString();
            #endregion

            #region 提交审批调用方法
            FlowApply1.BillTypeFlag = ConstUtil.BILL_TYPEFLAG_PURCHASE;
            FlowApply1.BillTypeCode = ConstUtil.BILL_TYPEFLAG_PURCHASE_ARRIVE;
            #endregion
        }
    }

    #endregion

    #region 方法
    /// <summary>
    /// 绑定列表采购类别
    /// </summary>
    private void BinddrpTypeID()
    {
        DataTable dt = PurchaseArriveBus.GetddlTypeID();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpTypeID.DataSource = dt;
            drpTypeID.DataTextField = "TypeName";
            drpTypeID.DataValueField = "ID";
            drpTypeID.DataBind();
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpTypeID.Items.Insert(0, Item);
    }

    /// <summary>
    /// 绑定列表交货方式
    /// </summary>
    private void BinddrpTakeType()
    {
        DataTable dt = PurchaseArriveBus.GetDrpTakeType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpTakeType.DataSource = dt;
            drpTakeType.DataTextField = "TypeName";
            drpTakeType.DataValueField = "ID";
            drpTakeType.DataBind();

        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpTakeType.Items.Insert(0, Item);
    }

    /// <summary>
    /// 绑定列表运送方式
    /// </summary>
    private void BinddrpCarryType()
    {
        DataTable dt = PurchaseArriveBus.GetDrpCarryType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpCarryType.DataSource = dt;
            drpCarryType.DataTextField = "TypeName";
            drpCarryType.DataValueField = "ID";
            drpCarryType.DataBind();

        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpCarryType.Items.Insert(0, Item);
    }

    /// <summary>
    /// 绑定列表结算方式
    /// </summary>
    private void BinddrpPayType()
    {
        DataTable dt = PurchaseArriveBus.GetDrpPayType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpPayType.DataSource = dt;
            drpPayType.DataTextField = "TypeName";
            drpPayType.DataValueField = "ID";
            drpPayType.DataBind();

        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpPayType.Items.Insert(0, Item);
    }

    /// <summary>
    /// 绑定列表支付方式
    /// </summary>
    private void binddrpMoneyType()
    {
        DataTable dt = PurchaseArriveBus.GetDrpMoneyType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpMoneyType.DataSource = dt;
            drpMoneyType.DataTextField = "TypeName";
            drpMoneyType.DataValueField = "ID";
            drpMoneyType.DataBind();

        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        drpMoneyType.Items.Insert(0, Item);
    }


    /// <summary>
    /// 绑定币种
    /// </summary>
    private void binddrpCurrencyType()
    {
        DataTable dt = CurrTypeSettingDBHelper.GetCurrenyType();
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

    #endregion

    #endregion

}

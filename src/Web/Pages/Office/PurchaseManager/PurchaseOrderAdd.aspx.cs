using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using XBase.Common;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using System.Web.UI.WebControls;

public partial class Handler_Office_PurchaseManager_PurchaseOrderAdd : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
        HiddenMoreUnit.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString();//是否启用多计量单位
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsDisplayPrice)
            IsDisplayPrice.Value = "true";
        else
            IsDisplayPrice.Value = "false";

        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsBarCode)
        {
            btnGetGoods.Visible = true;
        }
        else
        {
            btnGetGoods.Visible = false;
        }

        if (!IsPostBack)
        {
            #region 单据规则
            PurOrderNo.CodingType = ConstUtil.CODING_RULE_PURCHASE;
            PurOrderNo.ItemTypeID = ConstUtil.CODING_RULE_PURCHASE_ORDER;
            //PurOrderNo.TableName = "PurchaseOrder";
            //PurOrderNo.ColumnName = "OrderNo";
            #endregion

            FlowApply1.BillTypeFlag = ConstUtil.CODING_RULE_PURCHASE;
            FlowApply1.BillTypeCode = ConstUtil.CODING_RULE_PURCHASE_ORDER;

            #region 绑定采购类别
            ddlTypeID.TypeFlag = ConstUtil.PURCHASE_TYPE_PURCHASE;
            ddlTypeID.TypeCode = ConstUtil.PURCHASE_TYPE_PURCHASETYPE;
            ddlTypeID.IsInsertSelect = true;
            #endregion

            #region 绑定交货方式
            ddlTakeType.TypeFlag = ConstUtil.SELL_TYPE_SELL;
            ddlTakeType.TypeCode = ConstUtil.SELL_TYPE_TAKETYPE;
            ddlTakeType.IsInsertSelect = true;
            #endregion

            #region 绑定运送方式
            ddlCarryType.TypeFlag = ConstUtil.SELL_TYPE_SELL;
            ddlCarryType.TypeCode = ConstUtil.SELL_TYPE_CARRYTYPE;
            ddlCarryType.IsInsertSelect = true;
            #endregion

            #region 绑定结算方式
            ddlPayType.TypeFlag = ConstUtil.CUST_TYPE_CUST;
            ddlPayType.TypeCode = ConstUtil.CUST_INFO_PAYTYPE;
            ddlPayType.IsInsertSelect = true;
            #endregion

            #region 绑定支付方式
            ddlMoneyType.TypeFlag = ConstUtil.CUST_TYPE_CUST;
            ddlMoneyType.TypeCode = ConstUtil.CUST_INFO_MONEYTYPE;
            ddlMoneyType.IsInsertSelect = true;
            #endregion

            #region 绑定币种
            BindCurrencyType();
            #endregion

            #region 新增页面时给制单人等赋值
            string ID = Request.QueryString["ID"];
            if (ID == null)
            {
                UserInfoUtil userInfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
                txtCreatorName.Value = userInfo.EmployeeName;
                txtCreatorID.Value = userInfo.EmployeeID.ToString();
                txtCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtModifiedUserID.Value = userInfo.UserID;
                txtModifiedUserName.Value = userInfo.UserID;
            }
            #endregion
        }
    }
    #region 绑定币种
    private void BindCurrencyType()
    {
        DataTable dt = PurchaseOrderDBHelper.GetCurrenyType();
        if (dt != null && dt.Rows.Count > 0)
        {            
            ddlCurrencyType.DataSource = dt;
            ddlCurrencyType.DataTextField  = "CurrencyName";
            ddlCurrencyType.DataValueField = "hhh";
            ddlCurrencyType.DataBind();

            string aaa = ddlCurrencyType.Value;
            CurrencyTypeID.Value = aaa.Split('_')[0];
            //txtExchangeRate.Text = aaa.Split('_')[1].Substring(0, aaa.Split('_')[1].Length - 2);
            txtExchangeRate.Text = aaa.Split('_')[1];
        }
    }
    #endregion
}

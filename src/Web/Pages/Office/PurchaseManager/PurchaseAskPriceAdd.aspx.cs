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
public partial class Pages_Office_PurchaseManager_PurchaseAskPriceAdd : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsBarCode)
        {
            btnGetGoods.Visible = true;
        }
        else
        {
            btnGetGoods.Visible = false;
        }
        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
        HiddenMoreUnit.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString();//是否启用多计量单位
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsDisplayPrice)
            IsDisplayPrice.Value = "true";
        else
            IsDisplayPrice.Value = "false";

        if (!IsPostBack)
        {
            BindBillNo();
            BindCurrencyType();
            BindCreator();
            BindTypeID();
            BindApplyReason();
            BindFlowApply();
            
        }
    }
    #region 单据规则
    private void BindBillNo()
    {
        PurAskPriceNo.CodingType = ConstUtil.CODING_RULE_PURCHASE;
        PurAskPriceNo.ItemTypeID = ConstUtil.CODING_RULE_PURCHASE_ASKPRICE; ;
        //PurAskPriceNo.TableName = "PurchaseAskPrice";
        //PurAskPriceNo.ColumnName = "AskNo";
    }
    #endregion

    #region 绑定币种
    private void BindCurrencyType()
    {
        DataTable dt = PurchaseOrderDBHelper.GetCurrenyType();
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlCurrencyType.DataSource = dt;
            ddlCurrencyType.DataTextField = "CurrencyName";
            ddlCurrencyType.DataValueField = "hhh";
            ddlCurrencyType.DataBind();

            string aaa = ddlCurrencyType.Value;
            CurrencyTypeID.Value = aaa.Split('_')[0];
            txtExchangeRate.Text = aaa.Split('_')[1];
        }
    }
    #endregion

    #region 新增页面时给制单人等赋值
    private void BindCreator()
    {
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
    }
    #endregion

    #region 绑定采购类别
    private void BindTypeID()
    {
        ddlTypeID.TypeFlag = ConstUtil.PURCHASE_TYPE_PURCHASE;
        ddlTypeID.TypeCode = ConstUtil.PURCHASE_TYPE_PURCHASETYPE;
        ddlTypeID.IsInsertSelect = true;
    }
    #endregion

    #region 绑定原因
    private void BindApplyReason()
    {
        DataTable dt = PurchaseApplyDBHelper.GetApplyReason();
        if (dt != null && dt.Rows.Count > 0)
        {
            HidApplyReason.DataSource = dt;
            HidApplyReason.DataTextField = "CodeName";
            HidApplyReason.DataValueField = "ID";
            HidApplyReason.DataBind();
        }
        HidApplyReason.Items.Insert(0, new ListItem("--请选择--", ""));
    }
    #endregion

    #region 绑定审批流程
    void BindFlowApply()
    {
        FlowApply1.BillTypeFlag = ConstUtil.CODING_RULE_PURCHASE;
        FlowApply1.BillTypeCode = ConstUtil.CODING_RULE_PURCHASE_ASKPRICE;
    }
    #endregion
}

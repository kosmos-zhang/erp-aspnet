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
using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;

public partial class Pages_Office_StoreManager_SubSellOrderAdd : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hidCompanyCD.Value = UserInfo.CompanyCD;
        if (!IsPostBack)
        {
            string ID = Request.QueryString["ID"];
            if ((ID == "0") || (ID == "") || (ID == null))
            {//新建页面
                
                BindOrderNo();
                #region 绑定销售分店
                DataRow dt = SubStorageDBHelper.GetSubDeptFromDeptID(UserInfo.DeptID.ToString());

                if (dt != null)
                {
                    hidDeptID.Value = dt["ID"].ToString();
                    txtDeptName.Value = dt["DeptName"].ToString();
                }
                else
                {
                    hidDeptID.Value = "";
                    txtDeptName.Value = "";
                }
                #endregion

                BindUserInfo();

                //下单日期缺省为当前日期
                txtOrderDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                btnGetGoods.Visible = UserInfo.IsBarCode;
                unbtnGetGoods.Visible = UserInfo.IsBarCode;
            }

            if (UserInfo.IsMoreUnit)
            {
                txtIsMoreUnit.Value = "1";
                BasicUnitTd.Visible = true;
                BasicNumTd.Visible = true;
            }
            else
            {
                txtIsMoreUnit.Value = "0";
                BasicUnitTd.Visible = false;
                BasicNumTd.Visible = false;
            }

            BindCurrencyType();
            BindOrderMethod();
            BindTakeType();
            BindPayType();
            BindMoneyType();
            BindCarrayType();
            BindStorage();

            //小数点位数
            hidSelPoint.Value = UserInfo.SelPoint;
        }
    }
    #region 绑定单据规则
    private void BindOrderNo()
    {
        OrderNo.CodingType = ConstUtil.CODING_RULE_SUBSTORE;
        OrderNo.ItemTypeID = ConstUtil.CODING_RULE_SUBSTORE_SUBSELLORDER;
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
            txtRate.Value = aaa.Split('_')[1];
            hiddenRate.Value = aaa.Split('_')[1];
        }
    }
    #endregion

    #region 绑定订货方式
    private void BindOrderMethod()
    {
        ddlOrderMethod.TypeFlag = ConstUtil.SELL_TYPE_SELL;
        ddlOrderMethod.TypeCode = ConstUtil.SELL_TYPE_ORDERMETHOD;
        ddlOrderMethod.IsInsertSelect = true;
    }
    #endregion

    #region 绑定交付方式
    private void BindTakeType()
    {
        ddlTakeType.TypeFlag = ConstUtil.SELL_TYPE_SELL;
        ddlTakeType.TypeCode = ConstUtil.SELL_TYPE_TAKETYPE;
        ddlTakeType.IsInsertSelect = true;
    }
    #endregion

    #region 绑定结算方式
    private void BindPayType()
    {
        ddlPayType.TypeFlag = ConstUtil.CUST_TYPE_CUST;
        ddlPayType.TypeCode = ConstUtil.CUST_INFO_PAYTYPE;
        ddlPayType.IsInsertSelect = true;
    }
    #endregion

    #region 绑定支付方式
    private void BindMoneyType()
    {
        ddlMoneyType.TypeFlag = ConstUtil.CUST_TYPE_CUST;
        ddlMoneyType.TypeCode = ConstUtil.CUST_INFO_MONEYTYPE;
        ddlMoneyType.IsInsertSelect = true;
    }
    #endregion

    #region 绑定运送方式
    private void BindCarrayType()
    {
        ddlCarryType.TypeFlag = ConstUtil.SELL_TYPE_SELL;
        ddlCarryType.TypeCode = ConstUtil.SELL_TYPE_CARRYTYPE;
        ddlCarryType.IsInsertSelect = true;
    }
    #endregion

    #region 绑定仓库
    private void BindStorage()
    {
        StorageModel StorageM = new StorageModel();
        StorageM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        StorageM.UsedStatus = "1";
        StorageHid.DataSource = StorageBus.GetStorageListBycondition(StorageM);
        StorageHid.DataValueField = "ID";
        StorageHid.DataTextField = "StorageName";
        StorageHid.DataBind();
        StorageHid.Items.Insert(0,new ListItem("--请选择--",""));
    }
    #endregion

    #region 绑定制单人等信息
    private void BindUserInfo()
    {
        txtCreatorName.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
        txtCreateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        txtModifiedUserName.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        txtModifiedDate.Value = txtCreateDate.Value;
    }
    #endregion
}

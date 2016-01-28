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
using XBase.Model.Office.SubStoreManager;
using XBase.Business.Office.SubStoreManager;

public partial class Pages_Office_SubStoreManager_SubSellBackAdd : BasePage
{
    #region Master SellBack ID
    public int intMasterSubSellBackID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ID"], out tempID);
            return tempID;
        }
    }
    #endregion
    string CompanyCD = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtIndentityID.Value = this.intMasterSubSellBackID.ToString();
        CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        if (!Page.IsPostBack)
        {
            hidModuleID.Value = ConstUtil.MODULE_ID_SUBSTOREMANAGER_SUBSELLBACKLIST;
            BinddrpCurrencyType();//绑定币种
            BinddrpStorageID();//绑定仓库

            UserInfoUtil userInfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
            hidCompanyCD.Value = userInfo.CompanyCD;
            UserID.Value = userInfo.EmployeeID.ToString();
            UserName.Value = userInfo.EmployeeName;
            SystemTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
            SystemTime2.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtCreateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtCreator.Value = userInfo.EmployeeID.ToString();
            usernametemp.Value = userInfo.UserName;
            txtCreatorReal.Value = userInfo.EmployeeName;
            datetemp.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtCreateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedDate2.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUserID2.Value = userInfo.UserID.ToString();
            txtModifiedUserID.Value = userInfo.UserID.ToString();
            txtModifiedUserIDReal.Value = userInfo.UserID.ToString();
            DataRow dt = SubStorageDBHelper.GetSubDeptFromDeptID(userInfo.DeptID.ToString());

            if (dt != null)
            {
                HidDeptID.Value = dt["ID"].ToString();
                HidDeptID2.Value = dt["ID"].ToString();
                txtDeptName.Text = dt["DeptName"].ToString();

            }
            //计量单位
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


            btnGetGoods.Visible = userInfo.IsBarCode;
            unbtnGetGoods.Visible = userInfo.IsBarCode;

            //小数点位数
            hidSelPoint.Value = UserInfo.SelPoint;

            #region 门店销售退货单据规则
            CodingRuleControl1.CodingType = ConstUtil.CODING_RULE_SUBSTORE;
            CodingRuleControl1.ItemTypeID = ConstUtil.CODING_RULE_SUBSTORE_SUBSELLBACK;

            usernametemp.Value = userInfo.UserName;
            datetemp.Value = DateTime.Now.ToString("yyyy-MM-dd");
            #endregion

        }
    }

    #region 绑定币种
    private void BinddrpCurrencyType()
    {
        DataTable dt = SubSellBackBus.GetCurrenyType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpCurrencyType.DataSource = dt;
            drpCurrencyType.DataTextField = "CurrencyName";
            drpCurrencyType.DataValueField = "hhh";
            drpCurrencyType.DataBind();

            string aaa = drpCurrencyType.Value;
            CurrencyTypeID.Value = aaa.Split('_')[0];
            txtRate.Text = aaa.Split('_')[1];
            hiddenRate.Value = aaa.Split('_')[1];
        }
    }
    #endregion

    #region 绑定仓库名称
    private void BinddrpStorageID()
    {
        DataTable dt = SubSellBackBus.GetdrpStorageID();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpStorageID.DataSource = dt;
            drpStorageID.DataTextField = "StorageName";
            drpStorageID.DataValueField = "ID";
            drpStorageID.DataBind();
            ListItem Item = new ListItem("--请选择--", "");
            drpStorageID.Items.Insert(0, Item);
        }
    }
    #endregion
}

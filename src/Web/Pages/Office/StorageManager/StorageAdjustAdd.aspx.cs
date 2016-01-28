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
using XBase.Business.Office.StorageManager;
public partial class Pages_Office_StorageManager_StorageAdjustAdd : BasePage
{
    #region  AdjustID
    public int AdjustID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request.QueryString["ID"], out tempID);
            return tempID;
        }
    }
    public int intMasterProductID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ID"], out tempID);
            return tempID;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        FlowApply1.BillTypeFlag = ConstUtil.CODING_RULE_Storage_NO;
        FlowApply1.BillTypeCode = ConstUtil.CODING_RULE_StoAdjust_NO;
        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
        HiddenMoreUnit.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString();//是否启用多计量单位
        if (!IsPostBack)
        {
            ddlInStorage.Attributes.Add("onchange", "DoChange()");
            checkNo.CodingType = ConstUtil.CODING_RULE_Storage_NO;
            checkNo.ItemTypeID = ConstUtil.CODING_RULE_StoAdjust_NO;

            //控制条码扫描
            if (!((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsBarCode)
            {
                btnGetGoods.Visible = false;
            }
            #region 初始化

            int EmployeeId = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            string Company = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            txtCloseDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtCloser.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            txtCloserReal.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            txtCloser.Value = EmployeeId.ToString();
            txtConfirmor.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            txtConfirmDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            tbCreater.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            txtCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUserID.Text = UserID.ToString();
            txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            ddlInStorage.DataSource = StorageAdjustBus.GetStorageInfo();
            ddlInStorage.DataTextField = "StorageName";
            ddlInStorage.DataValueField = "ID";
            ddlInStorage.DataBind();
            //ddlInStorage.Items.Insert(0, new ListItem("--请选择--", "0"));

            ddlReasonType.DataSource = StorageAdjustBus.GetReason("3");
            ddlReasonType.DataTextField = "CodeName";
            ddlReasonType.DataValueField = "ID";
            ddlReasonType.DataBind();
            ddlReasonType.Items.Insert(0, new ListItem("--请选择--", "0"));


            #endregion
        }
    }
}

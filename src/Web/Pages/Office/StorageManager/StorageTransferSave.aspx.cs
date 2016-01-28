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

using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;
using XBase.Common;

public partial class Pages_Office_StorageManager_StorageTransferSave :  BasePage
{

    #region Master Product ID
    public int intMasterProductID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["TransferID"], out tempID);
            return tempID;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtRuleCodeNo.CodingType = ConstUtil.CODING_RULE_Storage_NO;
            txtRuleCodeNo.ItemTypeID = ConstUtil.CODING_RULE_StorageAdjust_NO;
            tboxCreator.Value = GetCurrentUserInfo().EmployeeName;
            BindReason();
            BindStorageInfo();
            hidIsBatchNo.Value = UserInfo.IsBatch ? "1" : "0";
        }
        if (Request.QueryString["action"] != null)
            txtAction.Value = Request.QueryString["action"].ToString();
        if (Request.QueryString["TransferID"] != null)
            txtTransferID.Value = Request.QueryString["TransferID"].ToString();
        if (Request.QueryString["TransferNo"] != null)
            txtNo.Value = Request.QueryString["TransferNo"].ToString();
        FlowApply1.BillTypeFlag = ConstUtil.BILL_TYPEFLAG_STORAGE;
        FlowApply1.BillTypeCode = ConstUtil.BILL_TYPECODE_STORAGE_TRANSFER;

        txtCurrentDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        txtCurrentUserID.Value = GetCurrentUserInfo().UserID;
        txtCurrentUserName.Value = GetCurrentUserInfo().EmployeeName;
        txtFlowStatus.Value = Request.QueryString["FlowStatus"] == null ? "0" : Request.QueryString["FlowStatus"].ToString();
        tboxModifiedDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        tbxoModifiedUser.Value = GetCurrentUserInfo().UserID;
        txtIsBack.Value = Request.QueryString["IsBack"] != null ? "1" : string.Empty;

        //设置隐藏域条码的值
        if (GetCurrentUserInfo().IsBarCode)
        {
            hidCodeBar.Value = "1";
        }
        else
        {
            hidCodeBar.Value = "0";
        }

        //计量单位
        if (GetCurrentUserInfo().IsMoreUnit)
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
        hidSelPoint.Value = UserInfo.SelPoint;
    }

    #region 绑定仓库数据
    protected void BindStorageInfo()
    {
        StorageModel model = new StorageModel();
        model.UsedStatus = "1";
        model.CompanyCD = GetCurrentUserInfo().CompanyCD;
        DataTable dt = StorageBus.GetStorageListBycondition(model);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlInStorageID.DataSource = dt;
            ddlInStorageID.DataTextField = "StorageName";
            ddlInStorageID.DataValueField = "ID";
            ddlInStorageID.DataBind();

            ddlOutStorageID.DataSource=dt;
            ddlOutStorageID.DataTextField="StorageName";
            ddlOutStorageID.DataValueField="ID";
            ddlOutStorageID.DataBind();
        }
    }
    #endregion

    #region 读取当前用户信息
    protected UserInfoUtil GetCurrentUserInfo()
    {
        if (SessionUtil.Session["UserInfo"] != null)
            return (UserInfoUtil)SessionUtil.Session["UserInfo"];
        else
            return new UserInfoUtil();
    }
    #endregion


    #region 绑定原因
    protected void BindReason()
    {
        DataTable dt = XBase.Business.Office.StorageManager.StorageBorrowBus.GetBorrowReason(GetCurrentUserInfo().CompanyCD, 4);
        if (dt != null && dt.Rows.Count > 0)
        {

            ddlReasonType.DataSource = dt;
            ddlReasonType.DataTextField = "CodeName";
            ddlReasonType.DataValueField = "ID";
            ddlReasonType.DataBind();

            ddlReasonType.Items.Insert(0, new ListItem("--请选择--", "-1"));
        }
        else
            ddlReasonType.Items.Insert(0, new ListItem("--暂时无调拨原因--", "-1"));
    }
    #endregion
}

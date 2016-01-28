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

public partial class Pages_Office_StorageManager_StorageCheckSave : BasePage
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
            HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
            HiddenMoreUnit.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString();//是否启用多计量单位
            btnGetGoods.Visible = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsBarCode;
            txtRuleCodeNo.CodingType = ConstUtil.CODING_RULE_Storage_NO;
            txtRuleCodeNo.ItemTypeID = ConstUtil.CODING_RULE_STORAGE_CHECK;
            BindStorageInfo();
            BindCheckType();//绑定盘点类型
            tboxCreator.Value = UserInfo.EmployeeName;
            FlowApply1.BillTypeFlag = ConstUtil.BILL_TYPEFLAG_STORAGE;
            FlowApply1.BillTypeCode = ConstUtil.BILL_TYPECODE_STORAGE_CHECK;

            if (Request.QueryString["CheckID"] != null)
            {
                txtAction.Value = "EDIT";
                txtCheckID.Value = Request.QueryString["CheckID"].ToString();
            }
        }
        txtCurrentDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        txtCurrentUserID.Value = GetCurrentUserInfo().UserID;
        txtCurrentUserName.Value = GetCurrentUserInfo().EmployeeName;
        txtFlowStatus.Value = Request.QueryString["FlowStatus"] == null ? "0" : Request.QueryString["FlowStatus"].ToString();
        tboxModifiedDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        tboxModifiedUser.Value = GetCurrentUserInfo().UserID;
        txtIsBack.Value = Request.QueryString["IsBack"] != null ? "1" : string.Empty;
    }
    #region 绑定仓库数据
    protected void BindStorageInfo()
    {
        StorageModel model = new StorageModel();
        model.CompanyCD = UserInfo.CompanyCD;
        model.UsedStatus = "1";
        DataTable dt = StorageBus.GetStorageListBycondition(model);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlStorageID.DataSource = dt;
            ddlStorageID.DataTextField = "StorageName";
            ddlStorageID.DataValueField = "ID";
            ddlStorageID.DataBind();
        }
    }
    #endregion

    #region 绑定盘点类型
    private void BindCheckType()
    {
        DataTable dt = StorageCheckBus.GetCheckType(UserInfo.CompanyCD);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlCheckType.DataSource = dt;
            ddlCheckType.DataTextField = "TypeName";
            ddlCheckType.DataValueField = "ID";
            ddlCheckType.DataBind();

        }
        ddlCheckType.Items.Insert(0, new ListItem("--请选择--", "0"));
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

    #region 读取当前用户信息
    protected new UserInfoUtil UserInfo
    {
        get
        {
            if (SessionUtil.Session["UserInfo"] == null)
                return new UserInfoUtil();
            else
                return (UserInfoUtil)SessionUtil.Session["UserInfo"];
        }
    }
    #endregion
}

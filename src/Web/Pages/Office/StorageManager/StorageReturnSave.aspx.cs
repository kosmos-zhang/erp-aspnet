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

public partial class Pages_Office_StorageManager_StorageReturnSave :  BasePage
{

    #region Master Product ID
    public int intMasterProductID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ReturnID"], out tempID);
            return tempID;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDepot();
            txtRuleCodeNo.CodingType = ConstUtil.CODING_RULE_Storage_NO;
            txtRuleCodeNo.ItemTypeID = ConstUtil.CODING_RULE_STORAGERETURN_NO;
            txtCurrentDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtCurrentUserID.Value = GetCurrentUserInfo().UserID;
            txtCurrentUserName.Value = GetCurrentUserInfo().EmployeeName;
            tboxModifiedDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            tbxoModifiedUser.Value = GetCurrentUserInfo().UserID;
            txtIsBack.Value = Request.QueryString["IsBack"] != null ? "1" : string.Empty;
            if (Request.QueryString["action"] != null)
            {
                action.Value = Request.QueryString["action"].ToString();
                txtReturnID.Value = Request.QueryString["ReturnID"].ToString();
                txtRerunNo.Value = Request.QueryString["ReturnNo"].ToString();
                FlowApply1.BillTypeFlag = ConstUtil.BILL_TYPEFLAG_STORAGE;
                FlowApply1.BillTypeCode = ConstUtil.BILL_TYPECODE_STORAGE_RETURN;
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
            hidSelPoint.Value = UserInfo.SelPoint;
        }
    }


    #region 读取仓库
    protected void LoadDepot()
    {
        string companyCD = GetCurrentUserInfo().CompanyCD;
        DataTable dt = XBase.Business.Office.StorageManager.StorageBorrowBus.GetDepot(companyCD);
        if (dt != null && dt.Rows.Count > 0)
        {

            ddlStorage.DataSource = dt;
            ddlStorage.DataTextField = "StorageName";
            ddlStorage.DataValueField = "ID";
            ddlStorage.DataBind();
        }
        else
            ddlStorage.Items.Insert(0, new ListItem("--暂无仓库--", "-1"));

    }

    #endregion

    #region 读取当前用户信息
    protected UserInfoUtil GetCurrentUserInfo()
    {
        if (SessionUtil.Session["UserInfo"] != null)
        {
            UserInfoUtil user = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            tboxCreator.Value = user.EmployeeName;
            return user;
        }
        else
            return new UserInfoUtil();
    }
    #endregion
}

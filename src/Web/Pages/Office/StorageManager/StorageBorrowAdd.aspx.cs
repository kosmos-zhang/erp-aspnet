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


public partial class Pages_Office_StorageManager_StorageBorrowAdd :BasePage
{
    #region Master Product ID
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
        if (!IsPostBack)
        {
            LoadDepot();
            LoadBorrowReason();
            hidIsBatchNo.Value = UserInfo.IsBatch ? "1" : "0";
            
        }
        action.Value = Request.QueryString["action"] == null ? "ADD" : Request.QueryString["action"].ToString();
        borrowid.Value = Request.QueryString["ID"] == null ? "0" : Request.QueryString["ID"].ToString();
        borrowno.Value = Request.QueryString["BorrowRealNo"] == null ? "0" : Request.QueryString["BorrowRealNo"].ToString();
        FlowApply1.BillTypeFlag = ConstUtil.BILL_TYPEFLAG_STORAGE;
        FlowApply1.BillTypeCode = ConstUtil.BILL_TYPECODE_STORAGE_BORROW;
        txtRuleCodeNo.CodingType = ConstUtil.CODING_RULE_Storage_NO;
        txtRuleCodeNo.ItemTypeID = ConstUtil.CODING_RULE_STORAGE_BORROW;
        UserInfoUtil user = GetCurrentUserInfo();
        txtCurrentDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        txtCurrentUserName.Value = user.EmployeeName;
        tbxoModifiedUser.Value = user.UserID;
        tboxModifiedDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        txtIsBack.Value = GetSafeRequest("IsBack");
        //设置隐藏域条码的值
        if (user.IsBarCode)
        {
            hidCodeBar.Value = "1";
        }
        else
        {
            hidCodeBar.Value = "0";
        }
        if (user.IsMoreUnit)
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
        //小数点位数
        hidSelPoint.Value = user.SelPoint;
    }


    #region 载入仓库列表
    protected void LoadDepot()
    {
        string companyCD = GetCurrentUserInfo().CompanyCD;
        DataTable dt = XBase.Business.Office.StorageManager.StorageBorrowBus.GetDepot(companyCD);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlDepot.DataSource = dt;
            ddlDepot.DataTextField = "StorageName";
            ddlDepot.DataValueField = "ID";
            ddlDepot.DataBind();
        }
        else
            ddlDepot.Items.Insert(0, new ListItem("--暂时无仓库--", "-1"));

    }
    #endregion

    #region 读取当前用户信息
    protected UserInfoUtil GetCurrentUserInfo()
    {
        if (SessionUtil.Session["UserInfo"] != null)
        {
            UserInfoUtil user = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            tboxCreator.Value = user.UserID;
            return user;
        }
        else
            return new UserInfoUtil();
    }
    #endregion

    #region 载入借货原因
    public  void LoadBorrowReason()
    {
        UserInfoUtil user = GetCurrentUserInfo();
        DataTable dt = XBase.Business.Office.StorageManager.StorageBorrowBus.GetBorrowReason(user.CompanyCD, 2);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlBorrowReason.DataSource = dt;
            ddlBorrowReason.DataTextField = "CodeName";
            ddlBorrowReason.DataValueField = "ID";
            ddlBorrowReason.DataBind();

            ddlBorrowReason.Items.Insert(0, new ListItem("--请选择--", "-1"));
        }
        else
            ddlBorrowReason.Items.Insert(0, new ListItem("--暂时无借货原因--", "-1"));
        
    }
    #endregion

    #region 接受参数
    protected string GetSafeRequest(string key)
    {
        if (Request.QueryString[key] != null)
            return Request.QueryString[key].ToString();
        else
            return string.Empty;
    }
    #endregion

}

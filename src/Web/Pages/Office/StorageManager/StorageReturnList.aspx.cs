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
using XBase.Business.Office.StorageManager;
using XBase.Common;

public partial class Pages_Office_StorageManager_StorageReturnList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDepot();
            GetBillExAttrControl1.TableName = "officedba.StorageReturn";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            GetBillExAttrControl1.ExtIndex = EFIndex;
            GetBillExAttrControl1.ExtValue = EFDesc;
            GetBillExAttrControl1.SetExtControlValue();
        }
        MoudleID.Value = ConstUtil.MODULE_ID_STORAGE_RETURN_SAVE;
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
            ddlStorage.Items.Insert(0, new ListItem("--请选择--", ""));
        }
        else
            ddlStorage.Items.Insert(0, new ListItem("--暂无仓库--", ""));

    }

    #endregion

    #region 读取当前用户信息
    protected UserInfoUtil GetCurrentUserInfo()
    {
        if (SessionUtil.Session["UserInfo"] != null)
        {
            UserInfoUtil user = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //  tboxCreator.Value = user.UserID;
            return user;
        }
        else
            return new UserInfoUtil();
    }
    #endregion

    protected void imgOutput_Click(object sender, ImageClickEventArgs e)
    {

        UserInfoUtil userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        Hashtable htParas = new Hashtable();
        string ListID = StorageBus.GetStorageIDStr(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID, userinfo.CompanyCD);
        string BillStatus = FormatString(ddlBillStatus.SelectedItem.Value);
        if (!string.IsNullOrEmpty(BillStatus))
            htParas.Add("@BillStatus", BillStatus);
        string ReturnNo = FormatString(txtReturnNo.Value.Trim());
        if (!string.IsNullOrEmpty(ReturnNo))
            htParas.Add("@ReturnNo", "%" + ReturnNo + "%");
        string ReturnerID = FormatString(txtReturnerID.Value);
        if (!string.IsNullOrEmpty(ReturnerID))
            htParas.Add("@ReturnPerson", Convert.ToInt32(ReturnerID));

        string StorageID = FormatString(ddlStorage.SelectedItem.Value);
        if (!string.IsNullOrEmpty(StorageID))
            htParas.Add("@StorageID", Convert.ToInt32(StorageID));
        string ReturnTitle =FormatString( txtReturnTitle.Value.Trim());
        if (!string.IsNullOrEmpty(txtReturnTitle.Value))
            htParas.Add("@ReturnTitle", "%" + ReturnTitle + "%");

        string BorrowDeptID = FormatString(txtBorrowDeptID.Value);
        if (!string.IsNullOrEmpty(BorrowDeptID))
            htParas.Add("@BorrowDepptID", Convert.ToInt32(BorrowDeptID));

        string OutDeptID = FormatString(txtOutDeptID.Value);
        if (!string.IsNullOrEmpty(OutDeptID))
            htParas.Add("@OutDeptID", Convert.ToInt32(OutDeptID));

        string FromBillID = FormatString(txtFromBillID.Value);
        if (!string.IsNullOrEmpty(FromBillID))
            htParas.Add("@FromBillID", Convert.ToInt32(FromBillID));

        string tempStart = FormatString(txtStartDate.Value);
        DateTime StartDate = Convert.ToDateTime(string.IsNullOrEmpty(tempStart) ? DateTime.MinValue.ToString() : tempStart);
        string tempEnd = FormatString(txtEndDate.Value);
        DateTime EndDate = Convert.ToDateTime(string.IsNullOrEmpty(tempEnd) ? DateTime.MinValue.ToString() : tempEnd);
        if (StartDate > DateTime.MinValue)
            htParas.Add("@StartDate", StartDate);
        if (EndDate > DateTime.MinValue)
            htParas.Add("@EndDate", EndDate.AddDays(1));

        string orderString =FormatString(txtOrderBy.Value);//排序
        string order = "DESC";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"


        if (orderString.EndsWith("_a"))
        {
            order = "ASC";//排序：降序
        }

        orderBy = orderBy + " " + order;

        DataTable dt = XBase.Business.Office.StorageManager.StorageReturnBus.GetStorageReturnList(htParas, userinfo.CompanyCD, orderBy);

        OutputToExecl.ExportToTableFormat(this, dt,
            new string[] { "借货返还单编号", "借货返还单主题", "返还仓库", "返还人", "返还日期", "对应借货单" },
            new string[] { "ReturnNo", "Title", "StorageName", "ReturnName", "ReturnDate", "BorrowNo" },"借货返还单");
    }


    protected string FormatString(string Value)
    {

        if (string.IsNullOrEmpty(Value) || Value == "-1")
            return string.Empty;
        else
            return Value;
    }


}

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

public partial class Pages_Office_StorageManager_StorageTransferList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindStorageInfo();
            //扩展属性
            GetBillExAttrControl1.TableName = "officedba.StorageTransfer";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            GetBillExAttrControl1.ExtIndex = EFIndex;
            GetBillExAttrControl1.ExtValue = EFDesc;
            GetBillExAttrControl1.SetExtControlValue();
            hidSelPoint.Value = UserInfo.SelPoint;
        }
        MoudleID.Value = ConstUtil.MODULE_ID_STORAGE_TRANSFER_SAVE;
        ModuleInOutID.Value = ConstUtil.MODULE_ID_STORAGE_TRANSFER_LIST;
    }
    #region 绑定仓库数据
    protected void BindStorageInfo()
    {
        StorageModel model = new StorageModel();
        model.CompanyCD = GetCurrentUserInfo().CompanyCD;
        model.UsedStatus = "1";
        DataTable dt = StorageBus.GetStorageListBycondition(model);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlInStorageID.DataSource = dt;
            ddlInStorageID.DataTextField = "StorageName";
            ddlInStorageID.DataValueField = "ID";
            ddlInStorageID.DataBind();
            ddlInStorageID.Items.Insert(0, new ListItem("--请选择--", "-1"));

            ddlOutStorageID.DataSource = dt;
            ddlOutStorageID.DataTextField = "StorageName";
            ddlOutStorageID.DataValueField = "ID";
            ddlOutStorageID.DataBind();
            ddlOutStorageID.Items.Insert(0, new ListItem("--请选择--", "-1"));
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


    protected void imgOutput_Click(object sender, ImageClickEventArgs e)
    {
        Hashtable htPara = new Hashtable();
        string ListID = StorageBus.GetStorageIDStr(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID, ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
        string TransferNo = FormatString(txtTransferNo.Value.Trim());
        if (!string.IsNullOrEmpty(TransferNo))
            htPara.Add("TransferNo", "%" + TransferNo + "%");
        string Title = FormatString(txtTitle.Value.Trim());
        if (!string.IsNullOrEmpty(Title))
            htPara.Add("Title", "%" + Title + "%");
        string ApplyUserID = FormatString(txtApplyUserID.Value);
        if (!string.IsNullOrEmpty(ApplyUserID))
            htPara.Add("ApplyUserID", Convert.ToInt32(ApplyUserID));
        string ApplyDeptID = FormatString(txtApplyDeptID.Value);
        if (!string.IsNullOrEmpty(ApplyDeptID))
            htPara.Add("ApplyDeptID", Convert.ToInt32(ApplyDeptID));
        string InStorageID = FormatString(ddlInStorageID.SelectedItem.Value);
        if (!string.IsNullOrEmpty(InStorageID))
            htPara.Add("InStorageID", Convert.ToInt32(InStorageID));
        string RequireInDate = FormatString(txtRequireInDate.Value);
        if (!string.IsNullOrEmpty(RequireInDate))
            htPara.Add("RequireInDate", Convert.ToDateTime(RequireInDate));
        string OutDeptID = FormatString(txtOutDeptID.Value);
        if (!string.IsNullOrEmpty(OutDeptID))
            htPara.Add("OutDeptID", Convert.ToInt32(OutDeptID));
        string OutStorageID = FormatString(ddlOutStorageID.SelectedItem.Value);
        if (!string.IsNullOrEmpty(OutStorageID))
            htPara.Add("OutStorageID", Convert.ToInt32(OutStorageID));
        string ConfirmStatus = FormatString(ddlConfirmStatus.SelectedItem.Value);
        if (!string.IsNullOrEmpty(ConfirmStatus))
            htPara.Add("ConfirmStatus", ConfirmStatus);
        string BusiStatus = FormatString(ddlBusiStatus.SelectedItem.Value);
        if (!string.IsNullOrEmpty(BusiStatus))
            htPara.Add("BusiStatus", BusiStatus);
        string BillStatus = FormatString(ddlBillStatus.SelectedItem.Value);
        if (!string.IsNullOrEmpty(BillStatus))
            htPara.Add("BillStatus", BillStatus);
        htPara.Add("CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);

        string orderString = FormatString(txtOrderBy.Value); ;//排序
        string order = "DESC";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ASC";//排序：降序
        }
        orderBy = orderBy + " " + order;
        DataTable dt=XBase.Business.Office.StorageManager.StorageTransferBus.GetStorageTransferList(htPara,orderBy);
        foreach (DataRow row in dt.Rows)
        {
            if (row["FlowStatus"] == null || row["FlowStatus"].ToString() == "")
                row["FlowStatus"] = "";
            else if (row["FlowStatus"].ToString() == "1")
                row["FlowStatus"] = "待审批";
            else if (row["FlowStatus"].ToString() == "2")
                row["FlowStatus"] = "审批中";
            else if (row["FlowStatus"].ToString() == "3")
                row["FlowStatus"] = "审批通过";
            else if (row["FlowStatus"].ToString() == "4")
                row["FlowStatus"] = "审批不通过";
            else if (row["FlowStatus"].ToString() == "5")
                row["FlowStatus"] = "撤销审批";

            if (row["BusiStatus"].ToString() == "1")
                row["BusiStatus"] = "调拨申请";
            else if (row["BusiStatus"].ToString() == "2")
                row["BusiStatus"] = "调拨出库";
            else if (row["BusiStatus"].ToString() == "3")
                row["BusiStatus"] = "调拨入库";
            else if (row["BusiStatus"].ToString() == "4")
                row["BusiStatus"] = "调拨完成";


        }


        OutputToExecl.ExportToTableFormat(this, dt,
            new string[] { "调拨单编号", "调拨单主题", "调拨申请人", "要货部门", "调入仓库", "要求到货日期", "调货部门", "调出仓库", "调拨数量", "调拨金额","单据状态","审批状态","业务状态" },
            new string[] { "TransferNo", "Title", "ApplyUserID", "ApplyDeptID", "InStorageID", "RequireInDate", "OutDeptID", "OutStorageID", "TransferCount", "TransferPrice", "BillStatus", "FlowStatus", "BusiStatus" },
            "调拨单");
    }

    protected string FormatString(string Value)
    {

        if (string.IsNullOrEmpty(Value) || Value == "-1")
            return string.Empty;
        else
            return Value;
    }
}

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

public partial class Pages_Office_StorageManager_StorageBorrowList :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDepot();
            //扩展属性
            GetBillExAttrControl1.TableName = "officedba.StorageBorrow";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            GetBillExAttrControl1.ExtIndex = EFIndex;
            GetBillExAttrControl1.ExtValue = EFDesc;
            GetBillExAttrControl1.SetExtControlValue();

            hidSelPoint.Value = UserInfo.SelPoint;
        }
        MoudleID.Value = ConstUtil.MODULE_ID_STORAGE_BORROW_SAVE;
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
            ddlDepot.Items.Insert(0, new ListItem("--请选择--", "-1"));
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
            return user;
        }
        else
            return new UserInfoUtil();
    }
    #endregion


    #region 分析返回查询参数
    protected void GetSearchPara()
    {
        //txtSearchPara.Value = "pageIndex=" + Request.QueryString["pageIndex"].ToString() +
        //                           "pageCount=" + Request.QueryString["pageCount"].ToString() +
        //                           "orderBy=" + Request.QueryString["orderBy"].ToString() +
        //                           "BorrowNo=" + Request.QueryString["BorrowNo"].ToString() +
        //                           "Title=" + Request.QueryString["Title"].ToString() +
        //                           "Borrower=" + Request.QueryString["Borrower"].ToString() +
        //                           "DeptID=" + Request.QueryString["DeptID"].ToString() +
        //                           "OutDeptID=" + Request.QueryString["OutDeptID"].ToString() +
        //                           "StorageID=" + Request.QueryString["StorageID"].ToString() +
        //                           "Transactor=" + Request.QueryString["Transactor"].ToString() +
        //                           "StartTime=" + Request.QueryString["StartTime"].ToString() +
        //                           "EndTime=" + Request.QueryString["EndTime"].ToString() +
        //                           "BillStatus=" + Request.QueryString["BillStatus"].ToString() +
        //                           "SubmitStatus=" + Request.QueryString["SubmitStauts"].ToString();

        //txtPageIndex.Value = GetSafeRequest("pageIndex");
        //txtPageCount.Value = GetSafeRequest("pageCount");
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
    #region 导出到Excel
    protected void imgOutput_Click(object sender, ImageClickEventArgs e)
    {
        StorageBorrow borrow = new StorageBorrow();

        borrow.BorrowNo = string.IsNullOrEmpty(tboxBorrowNo.Value.Trim())?string.Empty:("%"+tboxBorrowNo.Value.Trim()+"%");
        borrow.Title = string.IsNullOrEmpty(tboxTitle.Value.Trim()) ? string.Empty : ("%" +tboxTitle.Value.Trim()+ "%"); 
        borrow.Borrower = Convert.ToInt32(string.IsNullOrEmpty(txtBorrower.Value.Trim())?"-1":txtBorrower.Value.Trim());
        borrow.DeptID = Convert.ToInt32(string.IsNullOrEmpty(txtBorrowDeptID.Value.Trim())?"-1":txtBorrowDeptID.Value.Trim());
        borrow.OutDeptID = Convert.ToInt32(string.IsNullOrEmpty(txtOutDept.Value.Trim())?"-1":txtOutDept.Value.Trim());
        borrow.StorageID = Convert.ToInt32(string.IsNullOrEmpty(ddlDepot.SelectedItem.Value)?"-1":ddlDepot.SelectedItem.Value);
        borrow.Transactor = Convert.ToInt32(string.IsNullOrEmpty(txtTransactor.Value.Trim())?"-1":txtTransactor.Value.Trim());
        borrow.BillStatus = string.IsNullOrEmpty(ddlBillStatus.SelectedItem.Value)?string.Empty:ddlBillStatus.SelectedItem.Value;
        borrow.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(tboxBorrowStartTime.Text.Trim()) ? DateTime.MinValue.ToString() : tboxBorrowStartTime.Text.Trim());
        DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(tboxBorrowEndTime.Text.Trim()) ? DateTime.MinValue.ToString() : tboxBorrowEndTime.Text.Trim());
        int SubmitStatus = Convert.ToInt32(string.IsNullOrEmpty(ddlSubmitStatus.SelectedItem.Value)?"-1":ddlSubmitStatus.SelectedItem.Value);
        string orderString =(string.IsNullOrEmpty(txtOrderBy.Value.Trim()) ? string.Empty : txtOrderBy.Value.Trim());//排序
        string order = "DESC";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ASC";//排序：降序
        }
        //扩展属性
        string EFIndex = Request.QueryString["EFIndex"];
        string EFDesc = Request.QueryString["EFDesc"];
        GetBillExAttrControl1.ExtIndex = EFIndex;
        GetBillExAttrControl1.ExtValue = EFDesc;
        GetBillExAttrControl1.SetExtControlValue();
        orderBy = orderBy + " " + order;

        DataTable dt = XBase.Business.Office.StorageManager.StorageBorrowBus.GetStorageList(EFIndex,EFDesc,borrow, orderBy, start, end, SubmitStatus);
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
        }

        OutputToExecl.ExportToTableFormat(this,dt,
            new string[]{"借货单编号","借货单主题","借货部门","借货人","被借部门","被借仓库","借货数量","借货金额","出库人","出库时间","单据状态","审批状态"},
            new string[] { "BorrowNo", "Title", "DeptID", "Borrower", "OutDeptID", "StorageID", "CountTotal", "TotalPrice", "Transactor", "OutDate", "BillStatus", "FlowStatus" },
            "借货单列表");
       

    }
    #endregion
}

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

public partial class Pages_Office_StorageManager_StorageCheckList :  BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindStorageInfo();
            BindCheckType();//绑定盘点类型
            HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
            GetBillExAttrControl1.TableName = "officedba.StorageCheck";
        }
        MoudleID.Value = ConstUtil.MODULE_ID_STORAGE_CEHCK_SAVE;
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
            ddlStorageID.Items.Insert(0, new ListItem("--请选择--", "-1"));
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

    protected void imgOutput_Click(object sender, ImageClickEventArgs e)
    {
        Hashtable htPara = new Hashtable();
        string CheckNo = FormatString(txtCheckNo.Value.Trim());
        if (!string.IsNullOrEmpty(CheckNo))
            htPara.Add("CheckNo", "%" + CheckNo + "%");
        string Title = FormatString(txtTitle.Value.Trim());
        if (!string.IsNullOrEmpty(Title))
            htPara.Add("Title", "%" + Title + "%");

        string Transactor = FormatString(txtTransactor.Value.Trim());
        if (!string.IsNullOrEmpty(Transactor))
            htPara.Add("Transactor", Convert.ToInt32(Transactor));

        string DeptID = FormatString(txtDeptID.Value);
        if (!string.IsNullOrEmpty(DeptID))
            htPara.Add("DeptID", Convert.ToInt32(DeptID));

        string StorageID = FormatString(ddlStorageID.SelectedItem.Value);
        if (!string.IsNullOrEmpty(StorageID))
            htPara.Add("StorageID", Convert.ToInt32(StorageID));
        string CheckType = FormatString(ddlCheckType.SelectedItem.Value);
        if (!string.IsNullOrEmpty(CheckType))
            htPara.Add("CheckType", Convert.ToInt32(CheckType));

        string CheckStartDate = FormatString(txtStartDate.Value);
        if (!string.IsNullOrEmpty(CheckStartDate))
            htPara.Add("CheckStartDate", Convert.ToDateTime(CheckStartDate));


        string DiffCountStart = FormatString(txtDiffCountStart.Value);
        if (!string.IsNullOrEmpty(DiffCountStart))
            htPara.Add("DiffCountStart", Convert.ToDecimal(DiffCountStart));

        string DiffCountEnd = FormatString(txtDiffCountEnd.Value);
        if (!string.IsNullOrEmpty(DiffCountEnd))
            htPara.Add("DiffCountEnd", Convert.ToDecimal(DiffCountEnd));


        string FlowStatus = FormatString(ddlConfirmStatus.SelectedItem.Value);
        if (!string.IsNullOrEmpty(FlowStatus))
            htPara.Add("FlowStatus", FlowStatus);

        string BillStatus = FormatString(ddlBillStatus.SelectedItem.Value);
        if (!string.IsNullOrEmpty(BillStatus))
            htPara.Add("BillStatus", BillStatus);

        htPara.Add("CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
        string orderString = FormatString(txtOrderBy.Value); //排序
        string order = "DESC";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ASC";//排序：降序
        }
        orderBy = orderBy + " " + order;

        string IndexValue = GetBillExAttrControl1.GetExtIndexValue;
        string TxtValue = GetBillExAttrControl1.GetExtTxtValue;
        string BatchNo = this.txtBatchNo.Value.ToString();

        DataTable dt = XBase.Business.Office.StorageManager.StorageCheckBus.GetStorageCheckList(htPara, IndexValue, TxtValue, BatchNo,orderBy);
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

        OutputToExecl.ExportToTableFormat(this, dt,
            new string[] { "盘点单编号", "盘点单主题", "部门", "仓库", "经办人", "盘点开始日期", "盘点类型", "差异金额","单据状态","审批状态" },
            new string[] { "CheckNo", "Title", "DeptName", "StorageName", "TransactorName", "CheckStartDate", "CheckType", "DiffCount" ,"BillStatus","FlowStatus"},
            "盘点单");
    }


    protected string FormatString(string Value)
    {

        if (string.IsNullOrEmpty(Value.Trim()) || Value == "-1")
            return string.Empty;
        else
            return Value.Trim();
    }


}

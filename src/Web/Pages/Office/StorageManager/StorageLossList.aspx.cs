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

public partial class Pages_Office_StorageManager_StorageLossList : BasePage
{
    private string companyCD = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
        if (!IsPostBack)
        {
            DataTable dt = CodeReasonTypeBus.GetReasonTypeByFlag(companyCD, "5");
            if (dt.Rows.Count > 0)
            {
                ddlReason.DataSource = dt;
                ddlReason.DataTextField = "CodeName";
                ddlReason.DataValueField = "ID";
                ddlReason.DataBind();
                ddlReason.Items.Insert(0, new ListItem("--请选择--", ""));
            }
            StorageModel model = new StorageModel();
            model.CompanyCD = companyCD;
            model.UsedStatus = "1";
            DataTable dt1 = StorageBus.GetStorageListBycondition(model);
            if (dt1.Rows.Count > 0)
            {
                ddlStorage.DataSource = dt1;
                ddlStorage.DataTextField = "StorageName";
                ddlStorage.DataValueField = "ID";
                ddlStorage.DataBind();
                ddlStorage.Items.Insert(0, new ListItem("--请选择--", ""));
            }

            //新建模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_STORAGE_STORAGELOSS_ADD;
            ListModuleID.Value = ConstUtil.MODULE_ID_STORAGE_STORAGELOSS_LIST;
            GetBillExAttrControl1.TableName = "officedba.StorageLoss";

            //返回处理

            string requestParam = Request.QueryString.ToString();
            //从列表过来时
            int firstIndex = requestParam.IndexOf("&");
            //返回回来时
            if (firstIndex > 0)
            {
                //获取是否查询的标识
                string flag = Request.QueryString["Flag"];
                //点击查询时，设置查询的条件，并执行查询
                if ("1".Equals(flag))
                {

                    txtLossNo.Value = Request.QueryString["LossNo"];
                    txtTitle.Value = Request.QueryString["Title"];
                    txtDeptID.Value = Request.QueryString["Dept"];
                    ddlStorage.SelectedValue = Request.QueryString["StorageID"];
                    txtExecutorID.Value = Request.QueryString["Executor"];
                    sltFlowStatus.Value = Request.QueryString["FlowStatus"];
                    ddlReason.SelectedValue = Request.QueryString["ReasonType"];
                    sltBillStatus.Value = Request.QueryString["BillStatus"];
                    txtLossDateStart.Value = Request.QueryString["LossDateStart"];
                    txtLossDateEnd.Value = Request.QueryString["LossDateEnd"];

                    txtTotalPriceStart.Value = Request.QueryString["TotalPriceStart"];
                    txtTotalPriceEnd.Value = Request.QueryString["TotalPriceEnd"];
                    DeptName.Value = Request.QueryString["DeptName"];
                    UserExecutor.Value = Request.QueryString["UserExecutor"];
                    txtBatchNo.Value = Request.QueryString["BatchNo"];

                    //获取当前页
                    string pageIndex = Request.QueryString["pageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["pageCount"];

                    string EFIndex = Request.QueryString["EFIndex"];
                    string EFDesc = Request.QueryString["EFDesc"];

                    GetBillExAttrControl1.ExtIndex = EFIndex;
                    GetBillExAttrControl1.ExtValue = EFDesc;
                    GetBillExAttrControl1.SetExtControlValue();
                    //执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");DoSearch('" + pageIndex + "');</script>");
                }
            }
        }

    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        StorageLossModel model = new StorageLossModel();
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string LossDateStart = string.Empty;
        string LossDateEnd = string.Empty;
        string TotalPriceStart = string.Empty;
        string TotalPriceEnd = string.Empty;
        string FlowStatus = string.Empty;
        model.LossNo = txtLossNo.Value;
        model.Title = txtTitle.Value;
        model.DeptID = txtDeptID.Value;
        model.StorageID = ddlStorage.SelectedValue;
        model.Executor = txtExecutorID.Value;
        model.ReasonType = ddlReason.SelectedValue;
        model.BillStatus = sltBillStatus.Value;
        FlowStatus = sltFlowStatus.Value;
        LossDateStart = txtLossDateStart.Value;
        LossDateEnd = txtLossDateEnd.Value;
        TotalPriceStart = txtTotalPriceStart.Value;
        TotalPriceEnd = txtTotalPriceEnd.Value;

        string orderBy = txtorderBy.Value;
        if (!string.IsNullOrEmpty(orderBy))
        {
            if (orderBy.Split('_')[1] == "a")
            {
                orderBy = orderBy.Split('_')[0] + " asc";
            }
            else
            {
                orderBy = orderBy.Split('_')[0] + " desc";
            }
        }
        string IndexValue = GetBillExAttrControl1.GetExtIndexValue;
        string TxtValue = GetBillExAttrControl1.GetExtTxtValue;
        string BatchNo = this.txtBatchNo.Value.ToString();

        DataTable dt = StorageLossBus.GetStorageLossTableBycondition(model, IndexValue, TxtValue, LossDateStart, LossDateEnd, TotalPriceStart, TotalPriceEnd, FlowStatus,BatchNo, orderBy);

        OutputToExecl.ExportToTableFormat(this, dt,
            new string[] { "报损单编号", "报损单主题", "经办人", "报损部门", "报损仓库", "报损时间", "报损原因", "成本金额合计", "单据状态", "审批状态" },
            new string[] { "LossNo", "Title", "Executor", "DeptName", "StorageName", "LossDate", "ReasonTypeName", "TotalPrice", "BillStatusName", "FlowStatus" },
            "库存报损列表");
    }
}

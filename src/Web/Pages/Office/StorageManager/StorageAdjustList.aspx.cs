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
using XBase.Model.Office.StorageManager;
public partial class Pages_Office_StorageManager_StorageAdjustList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //扩展属性
            GetBillExAttrControl2.TableName = "officedba.StorageAdjust";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            GetBillExAttrControl2.ExtIndex = EFIndex;
            GetBillExAttrControl2.ExtValue = EFDesc;
            GetBillExAttrControl2.SetExtControlValue();
            ddlReason.DataSource = StorageAdjustBus.GetReason("3");
            ddlReason.DataTextField = "CodeName";
            ddlReason.DataValueField = "ID";
            ddlReason.DataBind();
            ddlReason.Items.Insert(0, new ListItem("--请选择--", "0"));
            hiddenStorageID.DataSource = StorageAdjustBus.GetStorageInfo();
            hiddenStorageID.DataTextField = "StorageName";
            hiddenStorageID.DataValueField = "ID";
            hiddenStorageID.DataBind();
            hiddenStorageID.Items.Insert(0, new ListItem("--请选择--", "0"));
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StorageAdjustModel model = new StorageAdjustModel();
            model.CompanyCD = companyCD;
            model.Title = this.txtSubject.Text.Trim();
            model.AdjustNo = this.txtReportNo.Text.Trim();
            model.StorageID = int.Parse(this.hiddenStorageID.SelectedValue);
            model.BillStatus = BillStatus.Value;
            string theBeginTime = this.BeginTime.Value;
            string theEndTime = this.EndTime.Value;
            model.Executor = int.Parse(hiddenExecutor.Value);
            model.DeptID = int.Parse(hiddenDeptID.Value);
            model.ReasonType = int.Parse(ddlReason.SelectedValue);
            string theFlowStatus = FlowStatus.Value;
            int TotalCount = 0;
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            GetBillExAttrControl2.ExtIndex = EFIndex;
            GetBillExAttrControl2.ExtValue = EFDesc;
            GetBillExAttrControl2.SetExtControlValue();
            model.Attachment = " ID Desc";
            if (this.hiddenOrder.Value != "0")
            {
                string[] myOrder = this.hiddenOrder.Value.Split('_');
                if (myOrder[1] == "a")
                {
                    model.Attachment = myOrder[0] + " asc ";
                }
                else
                {
                    model.Attachment = myOrder[0] + " desc ";
                }
            }
            model.Creator = -100;
            model.Confirmor = -100;
            string BatchNo = this.txtBatchNo.Value.ToString();
            model.BatchNo = BatchNo;

            DataTable dt = StorageAdjustBus.GetAllAdjust(model,EFIndex,EFDesc, theBeginTime, theEndTime, theFlowStatus, ref TotalCount);

            //导出标题
            string headerTitle = "单据编号|单据主题|仓库|经办人|部门|调整时间|调整原因|单据状态|审批状态";
            string[] header = headerTitle.Split('|');

            //导出标题所对应的列字段名称
            string columnFiled = "AdjustNo|Title|StorageName|EmployeeName|DeptName|AdjustDate|CodeName|BillStatus|FlowStatus";
            string[] field = columnFiled.Split('|');

            XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "日常调整单列表");
        }
        catch
        { }
    }
}

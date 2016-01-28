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
public partial class Pages_Office_StorageManager_StorageCheckReportList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GetBillExAttrControl1.TableName = "officedba.QualityCheckReport";
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        StorageQualityCheckReportModel model = new StorageQualityCheckReportModel();
        model.CompanyCD = companyCD;
        model.Title = this.txtSubject.Text.Trim();
        model.ReportNo = this.txtReportNo.Text.Trim();
        model.FromType = sltFromType.Value.Trim();
        model.FromReportNo = this.tbReportNo.Value;
        model.CheckType = sltCheckType.Value.Trim();
        model.CheckMode = sltCheckMode.Value.Trim();
        model.Checker = int.Parse(hiddenChecker.Value);
        model.ApplyDeptID = int.Parse(hiddenCheckDept.Value);
        string FlowStatus = ddlFlowStatus.Value;
        string theBeginTime = BeginTime.Value;
        string theEndTime = EndTime.Value;
        model.BillStatus = this.sltBillStatus.Value;
        int TotalCount = 0;

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

        string EFIndex = GetBillExAttrControl1.GetExtIndexValue;
        string EFDesc = GetBillExAttrControl1.GetExtTxtValue;

        DataTable dt = CheckReportBus.SearchReport(model, theBeginTime, theEndTime, FlowStatus, EFIndex, EFDesc, ref TotalCount);

        //导出标题
        string headerTitle = "单据编号|单据主题|源单类型|往来单位|往来单位类别|质检类别|检验方式|报检人员|报检部门|报检日期|单据状态|审批状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "ReportNo|Title|FromTypeName|OtherCorpName|BigTypeName|CheckTypeName|CheckModeName|EmployeeName|DeptName|CheckDate|BillStatus|FlowStatus";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "质检报告单列表");
    }
}

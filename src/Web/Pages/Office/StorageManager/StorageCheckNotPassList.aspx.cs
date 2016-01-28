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

public partial class Pages_Office_StorageManager_StorageCheckNotPassList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GetBillExAttrControl1.TableName = "officedba.CheckNotPass";

    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CheckNotPassModel model = new CheckNotPassModel();
        model.CompanyCD = companyCD;
        string ProductID = this.hiddenProductID.Value;
        model.Title = this.txtSubject.Text.Trim();
        model.ProcessNo = txtReportNo.Text.Trim();
        model.ReportID = int.Parse(hiddenFromReportID.Value);
        model.BillStatus = sltBillStatus.Value;
        string theBeginTme = BeginTime.Value;
        string theEndTime = EndTime.Value;
        string FlowStatus = ddlFlowStatus.Value;
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

        DataTable dt = CheckNotPassBus.SearchNoPass(model, ProductID, theBeginTme, theEndTime, FlowStatus, EFIndex, EFDesc, ref TotalCount);

        //导出标题
        string headerTitle = "单据编号|单据主题|源单类型|质检报告单|物品编号|物品名称|处理负责人|处理日期|单据状态|审批状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "ProcessNo|Title|FromTypeName|ReportNo|ProNo|ProductName|EmployeeName|ProcessDate|BillStatus|FlowStatus";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "不合格品处置单列表");
    }
}

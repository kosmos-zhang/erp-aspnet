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

public partial class Pages_Office_StorageManager_StorageQualityCheckList : BasePage
{
    /// <summary>
    /// 界面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GetBillExAttrControl1.TableName = "officedba.QualityCheckApplay";
    }

    #region 导出Excel
    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StorageQualityCheckApplay model = new StorageQualityCheckApplay();
            model.ApplyNO = this.txtInNo.Value.Trim();
            model.Title = this.txtTitle.Value.Trim();
            model.Checker = this.HiddenUser.Value.Trim();
            model.CustID = int.Parse(this.hiddenCustID.Value);
            model.CheckDeptID = this.HiddenDept.Value;
            model.CheckDate = Convert.ToDateTime("1800-1-1");
            if (this.BeginCheckDate.Value != "")
            {
                model.CheckDate = Convert.ToDateTime(this.BeginCheckDate.Value);
            }
            string EndDate = "9999-2-3";
            if (this.EndCheckDate.Value != "")
            {
                EndDate = this.EndCheckDate.Value;
            }
            model.CompanyCD = companyCD;
            model.FromType = this.FromType.Value;
            model.CheckType = this.txtCheckType.Value;
            model.CheckMode = this.txtCheckMode.Value;
            model.BillStatus = this.BillStatus.Value;

            string EFIndex = GetBillExAttrControl1.GetExtIndexValue;
            string EFDesc = GetBillExAttrControl1.GetExtTxtValue;
            string FlowStatus = this.ddlFlowStatus.Value;
            model.Attachment = " ID Desc";
            if (this.HiddenOrderID.Value != "0")
            {
                string[] myOrder = this.HiddenOrderID.Value.Split('_');
                if (myOrder[1] == "a")
                {
                    model.Attachment = myOrder[0] + " asc ";
                }
                else
                {
                    model.Attachment = myOrder[0] + " desc ";
                }
            }
            model.Creater = -100;
            model.Confirmor = -100;
            int TotalCount = 0;
            DataTable dt = StorageQualityCheckPro.GetQualityList(model, Convert.ToDateTime(EndDate), FlowStatus, EFIndex, EFDesc, ref TotalCount);

            //导出标题
            string headerTitle = "单据编号|单据主题|源单类型|往来单位|往来单位大类|检验方式|报检人员|报检部门|报检日期|单据状态|审批状态";
            string[] header = headerTitle.Split('|');

            //导出标题所对应的列字段名称
            string columnFiled = "ApplyNo|Title|FromType|CustName|CustBigType|CheckMode|EmployeeName|DeptName|CheckDate|BillStatus|FlowStatus";
            string[] field = columnFiled.Split('|');

            XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "质检申请列表");
        }
        catch
        { }
    }
    #endregion
}

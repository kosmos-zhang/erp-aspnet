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
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;

public partial class Pages_Office_HumanManager_PerformanceT : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        PerformanceTemplateEmpModel searchModel = new PerformanceTemplateEmpModel();
        searchModel.EmployeeID = txtSearchEmployeeID.Value ;
        searchModel.ScoreEmployee = txtSearchScoreEmployee.Value ;
        //设置查询条件
        //要素名称
        /// searchModel.ElemName = context.Request.QueryString["ElemName"];
        //启用状态
        /// searchModel.UsedStatus = context.Request.QueryString["UsedStatus"];

        //查询数据
        DataTable dtData = PerformanceTemplateEmpBus.SearchFlowInfo(searchModel);

        if (!string.IsNullOrEmpty(txtSearchTemplateName.Value .Trim ()))
        {

            dtData = GetNewDataTable(dtData, "TemplateName like '%" + txtSearchTemplateName.Value .Trim () + "%'");

        } 

        //导出标题
        string headerTitle = "被考评人员|模板名称|考核步骤|考评人";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "EmployeeName|TemplateName|StepName|ScoreEmployee";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dtData, header, field, "人员考核流程设置");




    }
    private DataTable GetNewDataTable(DataTable dt, string condition)
    {
        DataTable newdt = new DataTable();
        newdt = dt.Clone();
        DataRow[] dr = dt.Select(condition);
        for (int i = 0; i < dr.Length; i++)
        {
            newdt.ImportRow((DataRow)dr[i]);
        }
        return newdt;//返回的查询结果
    }
}

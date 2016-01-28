using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using XBase.Common;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Pages_Office_HumanManager_EmployeeExaminationReport :  BasePage 
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtEndDate.Value = DateTime.Now.ToString("yyyy-MM-01");
            this.txtEndToDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            this.txtStartDate.Value = DateTime.Now.ToString("yyyy-MM-01");
            this.txtStartToDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            //DataBindToDept();
        }
        if (IsPostBack)
        {
            Search();

        }
    }
    //public void DataBindToDept()
    //{
    //    DataTable dt = PerformanceQueryBus.SearchDeptInfo();

    //    ddlDeptName.DataSource = dt;
    //    ddlDeptName.DataTextField = "DeptName";
    //    ddlDeptName.DataValueField = "ID";
    //    ddlDeptName.DataBind();
    //    ddlDeptName.Items.Add(new System.Web.UI.WebControls.ListItem("--请选择--", "0"));
    //    ddlDeptName.SelectedValue = "0";
    //}
 
    
     
   
    public void Search()
    {



        ReportDocument oRpt = new ReportDocument();
        CrystalReportSource1.ReportDocument.Load(Server.MapPath(@"~/OperatingModel/CrystalReport/HumanManager/EmployeeExamination.rpt"));
        // SetDatabaseLogon 拉模式中必须用这个方法来设置登录信息，参数一：用户名；参数二：密码；参数三：服务器；参数四：数据库名
        CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.PerformanceSummary"));
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        EmployeeTestSearchModel searchModel = new EmployeeTestSearchModel();
        searchModel.Addr = Request.Form["hidDeptID"] == null ? "" : Request.Form["hidDeptID"].ToString();
        searchModel.StatusName  = Request.Form["txtSearchEmployeeID"] == null ? "" : Request.Form["txtSearchEmployeeID"].ToString();
        searchModel.StartDate = txtStartDate.Value;
        searchModel.StartToDate = txtStartToDate.Value;
        searchModel.EndDate = txtEndDate.Value;
        searchModel.EndToDate = txtEndToDate.Value;
        searchModel.CompanyCD = userInfo.CompanyCD;
        //查询数据
   
        CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["Creator"].Text = "\"" + "制表人:" + userInfo.EmployeeName + "\"";
        CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["DeptName"].Text = "\"" + "开始日期:" + txtStartDate.Value + " 至 " + txtStartToDate.Value + "\"";
        CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["PerformanceType"].Text = "\"" + "结束日期:" + txtEndDate.Value + " 至 " + txtEndToDate.Value + "\"";
        DataTable dtData = EmployeeTestBus.EmployeeExaminationReportInfo(searchModel);

        DataTable dt = new DataTable();
        DataColumn newCol = new DataColumn("LevelType");
        DataColumn newCol2 = new DataColumn("ID");
        dt.Columns.Add(newCol);
        dt.Columns.Add(newCol2);
        if (dtData.Rows.Count > 0)
        {
            

            for (int i=0;i<dtData .Rows.Count ;i++)
            {DataRow sk = dt.NewRow();
                string deptName =  EmployeeTestBus.getDept (  dtData.Rows[i]["LevelType"] == null ? "" : dtData.Rows[i]["LevelType"].ToString());
                sk["LevelType"] = deptName;
                sk["ID"] = dtData.Rows[i]["ID"] == null ? "" : dtData.Rows[i]["ID"].ToString();
            dt.Rows.Add (sk);
            }

        }


        CrystalReportSource1.ReportDocument.SetDataSource(dt);
        CrystalReportSource1.DataBind();
        // CrystalReportViewer1是水晶报表浏览器，下面是给该浏览器赋上对像
        CrystalReportViewer1.ReportSource = CrystalReportSource1;
        // CrystalReportViewer1.DataBind(); 
    }

    protected void btnQuery_Click1(object sender, EventArgs e)
    {
        //实例ReportDocument对象来显示对应的报表文件
        Search();
        //报表文件路径


    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        //CrystalDecisions.Shared.DiskFileDestinationOptions DiskOpts = new CrystalDecisions.Shared.DiskFileDestinationOptions();
        //oRpt.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
        //switch (ddlFormat.SelectedItem.Text)
        //{
        //    case "Rich Text (RTF)":
        //        oRpt.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.RichText;//
        //        DiskOpts.DiskFileName = "c:\\Output.rtf";//
        //        break;
        //    case "Portable Document (PDF)":
        //        oRpt.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;//
        //        DiskOpts.DiskFileName = "c:\\Output.pdf";//
        //        break;
        //    case "MS Word (DOC)":
        //        oRpt.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.WordForWindows;//
        //        DiskOpts.DiskFileName = "c:\\Output.doc";//
        //        break;
        //    case "MS Excel (XLS)":
        //        oRpt.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.Excel;//
        //        DiskOpts.DiskFileName = "c:\\Output.xls";//
        //        break;
        //    default:
        //        break;
        //}
        //oRpt.ExportOptions.DestinationOptions = DiskOpts;
        //oRpt.Export();
    }

    protected void btnQuery_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Search();
    }
}

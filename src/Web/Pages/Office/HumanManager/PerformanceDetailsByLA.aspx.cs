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

public partial class Pages_Office_HumanManager_PerformanceDetailsByLA : BasePage
{
     
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataBindToDept();
            DataBindToPerType();
            DataBindToTaskYear();
            DataBindToTaskNo();
            DataBindToTaskNum();
        }

        if (IsPostBack)
        {  
      
            Search();

        }
    }
    public void DataBindToDept()
    {
        DataTable dt = PerformanceQueryBus.SearchDeptInfo();

        ddlDeptName.DataSource = dt;
        ddlDeptName.DataTextField = "DeptName";
        ddlDeptName.DataValueField = "ID";
        ddlDeptName.DataBind();
        ddlDeptName.Items.Add(new System.Web.UI.WebControls.ListItem("--请选择--", "0"));
        ddlDeptName.SelectedValue = "0";
    }
    public void DataBindToPerType()
    {
        PerformanceTypeModel searchModel = new PerformanceTypeModel();
        DataTable dtData = PerformanceQueryBus.SearchPerTypeInfo(searchModel);


        this.ddlPerType.DataSource = dtData;
        ddlPerType.DataTextField = "TypeName";
        ddlPerType.DataValueField = "ID";
        ddlPerType.DataBind();
        ddlPerType.Items.Add(new System.Web.UI.WebControls.ListItem("--请选择--", "0"));
        ddlPerType.SelectedValue = "0";
    }
    public void DataBindToTaskYear()
    {
        int i = 2007;
        ddlTaskYear.Items.Add(new System.Web.UI.WebControls.ListItem("--请选择--", "0"));
        for (int temp = i ; temp < i + 14; temp++)
        {
            ddlTaskYear.Items.Add(new System.Web.UI.WebControls.ListItem(temp.ToString(), temp.ToString()));
        }
    }
    public void DataBindToTaskNo()
    {
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        DataTable dtData = PerformanceQueryBus.SearchRectCheckElemInfo(searchModel);

        this.ddlTestNo.DataSource = dtData;
        ddlTestNo.DataTextField = "Title";
        ddlTestNo.DataValueField = "TaskNo";
        ddlTestNo.DataBind();
        ddlTestNo.Items.Add(new System.Web.UI.WebControls.ListItem("--请选择--", "0"));
        ddlTestNo.SelectedValue = "0";
    }
    public void DataBindToTaskNum()
    {
        if (ddlTaskFlag.SelectedIndex == 0)
        {
            ddlTaskNum.Items.Clear();
            ddlTaskNum.Items.Add(new System.Web.UI.WebControls.ListItem("--请选择--", "0"));
        }
        else if (ddlTaskFlag.SelectedIndex == 1)//月考核
        {
            ddlTaskYear.Visible = true;
            ddlTaskNum.Visible = true;
            ddlTaskNum.Items.Clear();

            ddlTaskNum.Items.Add(new System.Web.UI.WebControls.ListItem("--请选择--", "0"));
            for (int temp = 1; temp < 13; temp++)
            {
                ddlTaskNum.Items.Add(new System.Web.UI.WebControls.ListItem(temp.ToString() + "月", temp.ToString()));
            }
        }
        else if (ddlTaskFlag.SelectedIndex == 2)//季度考核
        {
            ddlTaskNum.Visible = true;
            ddlTaskYear.Visible = true;
            ddlTaskNum.Items.Clear();

            ddlTaskNum.Items.Add(new System.Web.UI.WebControls.ListItem("--请选择--", "0"));
            for (int temp = 1; temp < 5; temp++)
            {
                ddlTaskNum.Items.Add(new System.Web.UI.WebControls.ListItem("第" + temp.ToString() + "季度", temp.ToString()));
            }
        }
        else if (ddlTaskFlag.SelectedIndex == 3)//半年考核
        {
            ddlTaskNum.Visible = true;
            ddlTaskYear.Visible = true;
            ddlTaskNum.Items.Clear();

            ddlTaskNum.Items.Add(new System.Web.UI.WebControls.ListItem("--请选择--", "0"));
            ddlTaskNum.Items.Add(new System.Web.UI.WebControls.ListItem("上半年", "1"));
            ddlTaskNum.Items.Add(new System.Web.UI.WebControls.ListItem("下半年", "2"));
        }
        else if (ddlTaskFlag.SelectedIndex == 4)//年考核
        {
            ddlTaskNum.Items.Clear();
            ddlTaskNum.Visible = false;
            ddlTaskYear.Visible = true;
        }
        else if (ddlTaskFlag.SelectedIndex == 5)//年考核
        {
            ddlTaskNum.Items.Clear();
            ddlTaskYear.Visible = false;
            ddlTaskNum.Visible = false;
        }
    }
    public void Search()
    {



        ReportDocument oRpt = new ReportDocument();
        if (chkType1.Checked)
        {
            CrystalReportSource1.ReportDocument.Load(Server.MapPath(@"~/OperatingModel/CrystalReport/HumanManager/PerformanceStaticByLA.rpt"));
        }
        if (chkType2.Checked)
        {
            CrystalReportSource1.ReportDocument.Load(Server.MapPath(@"~/OperatingModel/CrystalReport/HumanManager/PerformanceStaticByLABing.rpt"));
        }
        if (chkType3.Checked)
        {
            CrystalReportSource1.ReportDocument.Load(Server.MapPath(@"~/OperatingModel/CrystalReport/HumanManager/PerformanceStaticByLAZhe.rpt"));
        }
        

        // SetDatabaseLogon 拉模式中必须用这个方法来设置登录信息，参数一：用户名；参数二：密码；参数三：服务器；参数四：数据库名
        CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.PerformanceSummary"));
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        if (ddlDeptName.SelectedValue != "0" && ddlDeptName.SelectedIndex != -1)//部门
        {
            searchModel.SummaryDate = ddlDeptName.SelectedValue;
        }

        if (ddlTaskFlag.SelectedIndex != 0)
            searchModel.TaskFlag = ddlTaskFlag.SelectedValue;//考核期间类型



        if (ddlTestNo.SelectedValue != "0" && ddlTestNo.SelectedIndex != -1)
            searchModel.TaskNo = ddlTestNo.SelectedValue; //考核任务编号


        if (this.ddlPerType.SelectedValue != "0" && ddlPerType.SelectedIndex != -1)
            searchModel.CompleteDate = this.ddlPerType.SelectedValue;//考核类型

        if (ddlTaskFlag.SelectedValue != "4" && ddlTaskFlag.SelectedValue != "5")
        {

            if (this.ddlTaskNum.SelectedValue != "0" && ddlTaskNum.SelectedIndex != -1)
                searchModel.TaskNum = ddlTaskNum.SelectedValue;//考核期间 
        }


        if (this.ddlLevelType.SelectedIndex != 0)
            searchModel.Summaryer = ddlLevelType.SelectedValue;//考核等级
        //   if (Request.QueryString["AdviceType"] != "0")
        //   searchModel.Title = Request.QueryString["AdviceType"];//考核建议
        if (this.ddlTaskYear.SelectedIndex != 0 && this.ddlTaskYear.SelectedIndex != -1)
            searchModel.TaskDate = ddlTaskYear.SelectedValue;//考核建议
        //  if (!string.IsNullOrEmpty(Request.QueryString["EmployeeID"]))
        // searchModel.EditFlag = Request.QueryString["EmployeeID"];//被考核人
        searchModel.CompanyCD = userInfo.CompanyCD;
        //查询数据
        CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["DeptName"].Text = "\"" + "" + "\"";
        CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["PerformanceType"].Text = "\"" + "" + "\"";
        CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["taskFlag"].Text = "\"" + "" + "\"";
        CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["taskNum"].Text = "\"" + "考核期间:" + "全部" + "\"";
        CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["Creator"].Text = "\"" + "制表人:" + userInfo.EmployeeName + "\"";
        DataTable dtData = PerformanceQueryBus.SearchDetailsInfoByLA (searchModel);

        if (ddlDeptName.SelectedValue != "0" && ddlDeptName.SelectedIndex != -1)
        {
            CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["DeptName"].Text = "\"" + "部门:" + ddlDeptName.Items[ddlDeptName.SelectedIndex].Text + "\"";
        }
        else
        {
            CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["DeptName"].Text = "\"" + "部门:" + "全部" + "\"";
        }
        if (this.ddlPerType.SelectedValue != "0" && ddlPerType.SelectedIndex != -1)
        {
            CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["PerformanceType"].Text = "\"" + "考核类型:" + ddlPerType.Items[ddlPerType.SelectedIndex].Text + "\"";
        }
        else
        {
            CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["PerformanceType"].Text = "\"" + "考核类型:" + "全部" + "\"";
        }
        if (ddlTaskFlag.SelectedIndex != 0)
        {
            CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["taskFlag"].Text = "\"" + "考核期间类型:" + ddlTaskFlag.Items[ddlTaskFlag.SelectedIndex].Text + "\"";
        }
        else
        {
            CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["taskFlag"].Text = "\"" + "考核期间类型:" + "全部" + "\"";
        }
        if (ddlTaskFlag.SelectedValue != "4" && ddlTaskFlag.SelectedValue != "5")
        {

            if (this.ddlTaskNum.SelectedValue != "0" && ddlTaskNum.SelectedIndex != -1)
            {
                if (this.ddlTaskYear.SelectedIndex != 0 && this.ddlTaskYear.SelectedIndex != -1)
                {
                    CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["taskNum"].Text = "\"" + "考核期间:" + ddlTaskYear.Items[ddlTaskYear.SelectedIndex].Text + ddlTaskNum.Items[ddlTaskNum.SelectedIndex].Text + "\"";
                }
                else
                {
                    CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["taskNum"].Text = "\"" + "考核期间:" + "所有年份" + ddlTaskNum.Items[ddlTaskNum.SelectedIndex].Text + "\"";
                }
            }
        }
        else
        {
            if (this.ddlTaskYear.SelectedIndex != 0 && this.ddlTaskYear.SelectedIndex != -1)
            {
                CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["taskNum"].Text = "\"" + ddlTaskYear.Items[ddlTaskYear.SelectedIndex].Text + "\"";
            }
        }



        CrystalReportSource1.ReportDocument.SetDataSource(dtData);
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
    protected void ddlTaskFlag_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataBindToTaskNum();
    }
    protected void btnQuery_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
    
        Search();
    }
    protected void ddlTaskFlag_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DataBindToTaskNum();
    }
    protected void chkType1_CheckedChanged(object sender, EventArgs e)
    {
        //chkType2.Checked = false;
        //chkType3.Checked = false;

        btnQuery_Click(this, null);

    }
    protected void chkType2_CheckedChanged(object sender, EventArgs e)
    {
        //chkType1.Checked = false;
        //chkType3.Checked = false;
        btnQuery_Click(this, null);
    }
    protected void chkType3_CheckedChanged(object sender, EventArgs e)
    {
        //chkType2.Checked = false;
        //chkType1.Checked = false;
        btnQuery_Click(this, null);
    }
}

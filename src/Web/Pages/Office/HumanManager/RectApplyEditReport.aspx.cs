
using System;
using System.Data;
using XBase.Common;
using XBase.Business.Common;
using System.Web.UI.WebControls;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Pages_Office_HumanManager_RectApplyEditReport : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //string rectApplyID;
        //if (!Page.IsPostBack)
        //{
        //    //DataBindToDept();
        //    //DataBindToYear();
        //    //DataBindToMonth();
        //    if (Request.QueryString["rectApplyID"] != null)
        //    {
        //        string rectApplyID = Request.QueryString["rectApplyID"].ToString();
        //        Search(rectApplyID);
        //    }
        //}
        //if (IsPostBack)
        //{
        //    Search();
        //}
        if (Request.QueryString["rectApplyID"] != null)
        {
            string rectApplyID = Request.QueryString["rectApplyID"].ToString();
            Search(rectApplyID);
        }
    }
    //public void DataBindToYear()
    //{
    //    string year = string.Empty;
    //    int yearTemp = DateTime.Now.Year - 10;
    //    for (int i = 0; i <= 30; i++)
    //    {
    //        ddlYear.Items.Add(new ListItem((yearTemp + i).ToString() + "年度", (yearTemp + i).ToString()));
    //    }
    //    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    //}
    //public void DataBindToMonth()
    //{
    //    string month = string.Empty;
    //    int monthTemp = DateTime.Now.Month;
    //    if (monthTemp < 10)
    //    {
    //        month = "0" + monthTemp.ToString();
    //    }
    //    ddlEndMonth.SelectedValue = month;
    //    this.ddlStartMonth.SelectedValue = month;
    //}
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
    private void Search(string rectApplyID)
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //查询数据
        DataTable dtData = RectApplyBus.GetRectApplyInfoWithIDByReport (rectApplyID);
        DataTable dtSub = RectApplyBus.GetRectApplyDetailsInfoByReport(rectApplyID);
        ReportDocument oRpt = new ReportDocument();
        CrystalReportSource1.ReportDocument.Load(Server.MapPath(@"~/OperatingModel/CrystalReport/HumanManager/RectApplyEdit.rpt"));
        // SetDatabaseLogon 拉模式中必须用这个方法来设置登录信息，参数一：用户名；参数二：密码；参数三：服务器；参数四：数据库名
        CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.SalaryReportSummary"));

        ReportDocument rdDetail = CrystalReportSource1.ReportDocument.Subreports["RectApplyDetailReport.rpt"];
        rdDetail.SetDataSource(dtSub);
        //查询数据
   CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["Creator"].Text = "\"" + "制表人:" + userInfo.EmployeeName + "\"";
        CrystalReportSource1.ReportDocument.SetDataSource(dtData);
        CrystalReportSource1.DataBind();
        // CrystalReportViewer1是水晶报表浏览器，下面是给该浏览器赋上对像
        CrystalReportViewer1.ReportSource = CrystalReportSource1;
       
        //CrystalReportViewer1.DataBind();


    }
    //protected string getDeptName(string DeptId)
    //{

    //    return SalaryStandardBus.GetNameByDeptID(DeptId);


    //}
    //protected void imgSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    //{
    //    Search();
    //}
}

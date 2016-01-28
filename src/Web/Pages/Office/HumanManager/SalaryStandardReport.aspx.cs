/**********************************************
 * 作    者： 王保军
 * 创建日期： 2009/06/10
 * 描    述：工资标准报表
 * 修改日期： 2009/06/10
 * 版    本： 0.1.0
 ***********************************************/


using System;
using System.Data;
using XBase.Common;
using XBase.Business.Common;
using System.Web.UI.WebControls;
using XBase.Model.Office.HumanManager; 
using XBase.Business.Office.HumanManager;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
public partial class Pages_Office_HumanManager_SalaryStandardReport : BasePage 
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCodeType();
            BinddQuarters();
        }
        if (IsPostBack)
        {
            Search();
        }
    }
    /// <summary>
    /// 绑定岗位职等
    /// </summary>
    private void BindCodeType()
    {
        //   查询分类标识信息
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo("2", "14");
        //分类标识存在时绑定数据
        if (dt != null && dt.Rows.Count > 0)
        {
            //设置列表项的文本内容的数据源字段
            ddlSearchQuaterAdmin.DataTextField = "TypeName";
            //设置列表项的提供值的数据源字段。
            ddlSearchQuaterAdmin.DataValueField = "ID";
            //设置列表项的数据源
            ddlSearchQuaterAdmin.DataSource = dt;
            //绑定列表的数据源
            ddlSearchQuaterAdmin.DataBind();
            ddlSearchQuaterAdmin.Items.Add(new System.Web.UI.WebControls.ListItem("--请选择--", "0"));
            ddlSearchQuaterAdmin.SelectedValue = "0";
            //如果需要添加一空选项时
            //if (this.IsInsertSelect)
            //{
            //    //添加一请选择选项
            //    ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
            //    ddlCodeType.Items.Insert(0, Item);
            //}
            ////设置选中项
            //if (!string.IsNullOrEmpty(_selectedValue))
            //{
            //    ddlCodeType.SelectedValue = _selectedValue;
            //}
            ////
            //ddlCodeType.Enabled = this.Enabled;
        }
    }
  
    /// <summary>
    /// 绑定岗位
    /// </summary>
    private void BinddQuarters()
    {
        DataTable dtQuarter = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
        ddlSearchQuarter.DataSource = dtQuarter;
        ddlSearchQuarter.DataValueField = "ID";
        ddlSearchQuarter.DataTextField = "QuarterName";
        ddlSearchQuarter.DataBind();
        ddlSearchQuarter.Items.Add(new System.Web.UI.WebControls.ListItem("--请选择--", "0"));
        ddlSearchQuarter.SelectedValue = "0";
    }
    private void Search()
    {
        SalaryStandardModel searchModel = new SalaryStandardModel();
        //设置查询条件
        //岗位
        if (ddlSearchQuarter.SelectedValue!="0")
        searchModel.QuarterID = ddlSearchQuarter.SelectedValue;
        //岗位职等
        if (ddlSearchQuaterAdmin.SelectedValue != "0")
        searchModel.AdminLevel = ddlSearchQuaterAdmin.SelectedValue;
        //启用状态·
        searchModel.UsedStatus = "1";
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //查询数据
        DataTable dtData = SalaryStandardBus.SearchSalaryStandardReport(searchModel);

        ReportDocument oRpt = new ReportDocument();
        CrystalReportSource1.ReportDocument.Load(Server.MapPath(@"~/OperatingModel/CrystalReport/HumanManager/SalaryStandardReport.rpt"));
        // SetDatabaseLogon 拉模式中必须用这个方法来设置登录信息，参数一：用户名；参数二：密码；参数三：服务器；参数四：数据库名
        CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.SalaryStandard"));

        //查询数据
        CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["DeptName"].Text = "\"" + "" + "\"";
        CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["PerformanceType"].Text = "\"" + "" + "\"";
        CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["Creator"].Text = "\"" + "制表人:" + userInfo.EmployeeName + "\"";

        if (ddlSearchQuarter.SelectedValue != "0" && ddlSearchQuarter.SelectedIndex != -1)
        {
            CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["DeptName"].Text = "\"" + "岗位:" + ddlSearchQuarter.Items[ddlSearchQuarter.SelectedIndex].Text + "\"";
        }
        else
        {
            CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["DeptName"].Text = "\"" + "岗位:" + "全部" + "\"";
        }
        if (this.ddlSearchQuaterAdmin.SelectedValue != "0" && ddlSearchQuaterAdmin.SelectedIndex != -1)
        {
            CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["PerformanceType"].Text = "\"" + "职等:" + ddlSearchQuaterAdmin.Items[ddlSearchQuaterAdmin.SelectedIndex].Text + "\"";
        }
        else
        {
            CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["PerformanceType"].Text = "\"" + "职等:" + "全部" + "\"";
        }



        CrystalReportSource1.ReportDocument.SetDataSource(dtData);
        CrystalReportSource1.DataBind();
        // CrystalReportViewer1是水晶报表浏览器，下面是给该浏览器赋上对像
        CrystalReportViewer1.ReportSource = CrystalReportSource1;
       // CrystalReportViewer1.DataBind();


    }

    protected void imgSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Search();
    }
}
  

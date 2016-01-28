/**********************************************
 * 类作用：   工资报表列表
 * 建立人：   吴志强
 * 建立时间： 2009/05/23
 ***********************************************/
using System;
using XBase.Common;
using System.Text;
using System.Data;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
using System.Web.UI.WebControls;

public partial class Pages_Office_HumanManager_SalaryReport_Info : BasePage
{
    /// <summary>
    /// 类名：SalaryReport_Info
    /// 描述：工资报表列表
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/23
    /// 最后修改时间：2009/05/23
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //年下拉列表
            DateTime.Now.ToString("yyyy");

            for (int i = -10; i < 4; i++)
            {
                //获取年
                string year = Convert .ToString (2017+i);
                //添加下拉列表选项
                ddlYear.Items.Insert(i + 10, new ListItem(year, year));
            }
            //年份
            ddlYear.Items.Insert(0, new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE));

            string lastYear = DateTime.Now.ToString("yyyy");
            //获取前一月的月份
            string lastMonth = DateTime.Now.ToString("MM");
            ddlYear.SelectedValue = lastYear;
            ddlMonth.SelectedValue = lastMonth;
            //新建模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_SALARY_REPORT_NEW;

            //获取请求参数
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
                    //报表编号
                    txtReportNo.Text = Request.QueryString["ReportNo"];
                    //报表主题
                    txtReportName.Text = Request.QueryString["ReportName"];
                    //所属月份
                    string reportMonth = Request.QueryString["ReportMonth"];
                    //所属年月不为空时
                    if (!string.IsNullOrEmpty(reportMonth))
                    {
                        //所属年月输入时
                        if (reportMonth.Length == 6)
                        {
                            //
                            ddlYear.SelectedValue = reportMonth.Substring(0, 4);
                            ddlMonth.SelectedValue = reportMonth.Substring(4, 2);
                        }
                        //只输入年时
                        else if (reportMonth.Length == 4)
                        {
                            ddlYear.SelectedValue = reportMonth;
                        }
                        //只输入月时
                        else
                        {
                            ddlMonth.SelectedValue = reportMonth;
                        }
                    }
                    //编制状态
                    ddlStatus.SelectedValue = Request.QueryString["Status"];
                    //审批状态
                    ddlFlowStatus.SelectedValue = Request.QueryString["FlowStatus"];

                    //获取当前页
                    string pageIndex = Request.QueryString["PageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["PageCount"];
                    //获取排序
                    string orderBy = Request.QueryString["OrderBy"];
                    //执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");this.orderBy = \""
                                + orderBy + "\";DoSearch('" + pageIndex + "');</script>");
                }
            }
        }
    }
 
    protected void btnImport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        SalaryReportModel searchModel = new SalaryReportModel();
        //设置查询条件
        //报表编号
        searchModel.ReprotNo = txtReportNo.Text.Trim();
        //报表主题
        searchModel.ReportName = txtReportName.Text.Trim();
        //所属年月
        searchModel.ReportMonth = Request.Form["ddlYear"].ToString() + Request.Form["ddlMonth"].ToString();
        //编制状态
        searchModel.Status = Request.Form["ddlStatus"].ToString();
        //审批状态   
        searchModel.FlowStatus = Request.Form["ddlFlowStatus"].ToString();

        //查询数据
        DataTable dtReport = SalaryReportBus.SearchReportInfo(searchModel);

        //导出标题
        string headerTitle = "工资报表编号|工资报表主题|所属月份|开始日期|结束日期|编制人|编制日期|报表状态|审批状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "ReprotNo|ReportName|ReportMonth|StartDate|EndDate|Creator|CreateDate|StatusName|FlowStatus";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dtReport, header, field, "工资报表列表");
    }
}

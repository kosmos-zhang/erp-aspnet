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

public partial class Pages_Office_HumanManager_PerformanceBetter_Edit : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            //新建招聘申请的模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_PERFORMANCEBETTER;
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
                    //   //申请编号
                    //   txtRectApplyNo.Text = Request.QueryString["RectApplyNo"];
                    //   //申请部门
                    //   hidDeptID.Value = Request.QueryString["ApplyDeptID"];
                    //   DeptApply.Text = Request.QueryString["ApplyDeptName"];
                    //   //申请日期
                    ////   txtApplyDate.Text = Request.QueryString["ApplyDate"];
                    //   //职位名称
                    //   DeptQuarter.Value  = Request.QueryString["JobName"];
                    //   //审批状态
                    //   ddlFlowStatus.SelectedValue = Request.QueryString["FlowStatus"];

                    //   //获取当前页
                    //   string pageIndex = Request.QueryString["PageIndex"];
                    //   //获取每页显示记录数 
                    //   string pageCount = Request.QueryString["PageCount"];
                    //   //排序 
                    //   string OrderBy = Request.QueryString["OrderBy"];
                    //   //执行查询
                    //   ClientScript.RegisterStartupScript(this.GetType(), "SearchRectApply"
                    //           , "<script language=javascript>this.pageCount = parseInt(" + pageCount
                    //               + ");this.OrderBy = \"" + OrderBy + "\";DoSearch('" + pageIndex + "');</script>");
                }
            }
        }

    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        PerformanceBetterModel searchModel = new PerformanceBetterModel();
        searchModel.PlanNo = txtSearchTask.Value.Trim();
        //启用状态
        searchModel.Title = inptSearchTitle.Value.Trim();
        searchModel.CreateDate = txtStartDate.Text.Trim();
        searchModel.EndDate = txtEndDate.Text.Trim();
        searchModel.EmployeeId = txtSearchScoreEmployee.Value;
        //启用状态
        //查询数据

        DataTable dtData = PerformanceBetterBus.SearchPlanInfo(searchModel);

        //导出标题
        string headerTitle = "计划编号|计划主题|创建人|创建时间" ;
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "PlanNo|Title|Creator|CreateDate";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dtData, header, field, "绩效改进列表");




    }
}

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

public partial class Pages_Office_HumanManager_PerformanceGrade : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            //新建招聘申请的模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_PERFORMANCEGRADELIST;
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
        PerformanceScoreModel searchModel = new PerformanceScoreModel();


        searchModel.TaskNo = txtSearchTask.Value.Trim();
        //启用状态
        searchModel.TaskTitle = inptSearchTitle.Value.Trim();
        string selSearchTaskFlag = Request.Form["selSearchTaskFlag"].ToString();
        if (selSearchTaskFlag != "0")
            searchModel.TaskFlag = selSearchTaskFlag;

        //启用状态
        searchModel.EmployeeID = txtSearchScoreEmployee.Value;
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        searchModel.ScoreEmployee = userInfo.EmployeeID.ToString();

        //查询数据

        DataTable dtData = PerformanceGradeBus.SearchTaskInfo(searchModel);

        //导出标题
        string headerTitle = "任务编号|任务主题|考核期间类型|考核模板|被考评人|任务创建人|任务创建日期";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "TaskNo|TaskTitle|TaskFlag|TemplateName|EmployeeName|CreateEmployeeName|CreateDate";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dtData, header, field, "待评分列表");




    }
}

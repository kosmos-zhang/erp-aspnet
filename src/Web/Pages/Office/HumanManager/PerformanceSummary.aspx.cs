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
public partial class Pages_Office_HumanManager_PerformanceSummary : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        searchModel.TaskNo = txtSearchTask.Value.Trim();
        //启用状态
        searchModel.Title = inptSearchTitle.Value.Trim();
        string selSearchTaskFlag = Request.Form["selSearchTaskFlag"].ToString();
        if (selSearchTaskFlag != "0")
            searchModel.TaskFlag = selSearchTaskFlag;
        string selSearchTaskNum = Request.Form["selSearchTaskNum"].ToString();
        //启用状态
        if (selSearchTaskNum != "0")
            searchModel.TaskNum = selSearchTaskNum;
        string selSearchTaskYear = Request.Form["selSearchTaskYear"].ToString();
        if (selSearchTaskYear != "0")
            searchModel.TaskDate = selSearchTaskYear;

        //查询数据
        DataTable dtData = PerformanceSummaryBus.SearchTaskInfo(searchModel);



        //导出标题
        string headerTitle = "任务编号|任务主题|考核期间类型|考核期间|考核开始日期|考核结束日期|任务状态|创建人|创建时间|汇总人|汇总日期";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "TaskNo|Title|TaskFlag|TaskNum|StartDate|EndDate|Status|Creator|CreateDate|Summaryer|SummaryDate";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dtData, header, field, "评分汇总");




    }
}

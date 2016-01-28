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


public partial class Pages_Office_HumanManager_PerformanceTask_Edit :BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //绩效考核指标编号初期处理
            AimNum.CodingType = ConstUtil.CODING_RULE_TYPE_PERFORMANCETASK;
            AimNum.ItemTypeID = ConstUtil.CODING_RULE_HUMEN_PERFORMANCETASK;
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        searchModel.TaskNo = txtSearchTask.Text.Trim();
        //启用状态
        searchModel.Title = inptSearchTitle.Value.Trim();
        if (selSearchTaskFlag .Value != "0")
            searchModel.TaskFlag = selSearchTaskFlag.Value;
        //启用状态]
        string TaskNum = Request.Form["selSearchTaskNum"].ToString();
        if (TaskNum != "0")
            searchModel.TaskNum = TaskNum;
        string TaskYear = Request.Form["selSearchTaskYear"].ToString();
        if (TaskYear != "0")
            searchModel.TaskDate = TaskYear;
        //查询数据
        DataTable dtData = PerformanceTaskWorkBus.SearchTaskInfo(searchModel);

        //导出标题
        string headerTitle = "任务编号|任务主题|考核期间类型|考核周期|开始日期|结束日期";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "TaskNo|Title|TaskFlag|TaskNum|StartDate|EndDate";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dtData, header, field, "考核任务列表");




    }
}

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

public partial class Pages_Office_HumanManager_PerformanceStatistics : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        string selSearchTaskFlag = Request.Form["selSearchTaskFlag"].ToString();
        if (selSearchTaskFlag != "0")
            searchModel.TaskFlag = selSearchTaskFlag;//考核期间类型
        string taskNo = txtTaskTitle.Attributes["title"];
        if (!string.IsNullOrEmpty(taskNo))
            searchModel.TaskNo = taskNo;//考核任务编号
        string SelPerType = Request.Form["SelPerType"].ToString();
        if (SelPerType != "0")
            searchModel.CompleteDate = SelPerType;//考核类型

        if (selSearchTaskFlag != "4" && selSearchTaskFlag != "5")
        {
            string selSearchTaskNum = Request.Form["selSearchTaskNum"].ToString();
            if (selSearchTaskNum != "0")
                searchModel.TaskNum = selSearchTaskNum;//考核期间 
        }
        string SelLevelType = Request.Form["SelLevelType"].ToString();
        if (SelLevelType != "0")
            searchModel.Summaryer = SelLevelType;//考核等级
        string SelAdviceType = Request.Form["SelAdviceType"].ToString();
        if (SelAdviceType != "0")
            searchModel.Title = SelAdviceType;//考核建议

        string selSearchTaskYear = Request.Form["selSearchTaskYear"].ToString();
        if (selSearchTaskYear != "0")
            searchModel.TaskDate = selSearchTaskYear;//考核建议

        if (!string.IsNullOrEmpty(txtDeptID.Value ))
        {
            searchModel.EditFlag = txtDeptID.Value ;//部门
        }

        searchModel.CompanyCD = userInfo.CompanyCD;

        //查询数据
        DataTable dtData = PerformanceQueryBus.SearchStaticsInfo(searchModel);

        //导出标题
        string headerTitle = "部门|任务主题|考核期间类型|考核期间|考核等级|考核建议|人数";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "DeptName|Title|TaskFlag|TaskNum|LevelType|AdviceType|CountPerson";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dtData, header, field, "考核统计列表");




    }
}

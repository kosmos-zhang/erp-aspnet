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

public partial class Pages_Office_HumanManager_PerformanceTaskCheck : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        searchModel.TaskFlag = Request.Form["selSearchTaskFlag"].ToString();
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        searchModel.EditFlag = userInfo.EmployeeID.ToString();
        searchModel.CompanyCD = userInfo.CompanyCD;
        //查询数据
        DataTable dtData = PerformanceSummaryBus.SearchSurmmaryCheckInfo(searchModel);
        //导出标题
        string headerTitle = "被考核人|任务主题|考核期间类型|考核期间|考核模板|考核总得分|累计扣分|累计加分|实际得分|考核等级|考核建议|总评人|总评时间|确认时间";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "passEmployeeName|Title|TaskFlag|TaskNum|templateName|TotalScore|KillScore|AddScore|RealScore|LevelType|AdviceType|EvaluaterName|EvaluateDate|SignDate";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dtData, header, field, "员工确认");




    }
}

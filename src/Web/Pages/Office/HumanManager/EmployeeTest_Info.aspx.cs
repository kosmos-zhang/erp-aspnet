/**********************************************
 * 类作用：   考试记录列表
 * 建立人：   吴志强
 * 建立时间： 2009/04/08
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Web.UI.WebControls;
public partial class Pages_Office_HumanManager_EmployeeTest_Info : BasePage
{
    /// <summary>
    /// 类名：EmployeeTest_Info
    /// 描述：考试记录列表
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/08
    /// 最后修改时间：2009/04/08
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            //新建考试记录的模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_TEST_EDIT;
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
                    //考试编号
                    txtTestNo.Value = Request.QueryString["TestNo"];
                    //主题
                    txtTitle.Value = Request.QueryString["Title"];
                    //考试负责人 
                    txtTeacherID.Value = Request.QueryString["TeacherID"];
                    UserTeacher.Value = Request.QueryString["TeacherName"];
                    //开始时间
                    txtStartDate.Value = Request.QueryString["StartDate"];
                    txtStartToDate.Value = Request.QueryString["StartToDate"];
                    //结束时间
                    txtEndDate.Value = Request.QueryString["EndDate"];
                    txtEndToDate.Value = Request.QueryString["EndToDate"];
                    //考试地点
                    txtAddr.Value = Request.QueryString["Addr"];
                    //考试地点
                    ddlStatus.SelectedValue = Request.QueryString["StatusID"];
                    //获取当前页
                    string pageIndex = Request.QueryString["PageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["PageCount"];
                    //执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "SearchTrainingAsse"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");DoSearchInfo('" + pageIndex + "');</script>");
                }
            }
        }
    }
    protected void btnImport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {
            XBase.Model.Office.HumanManager.EmployeeTestSearchModel searchModel = new XBase.Model.Office.HumanManager.EmployeeTestSearchModel();//获取数据
            //考核编号
            searchModel.TestNo = txtTestNo.Value;
            //主题
            searchModel.Title = txtTitle.Value;
            //获取考试负责人
            string teacherID = UserTeacher.Value;
            //考试负责人输入时，解析考试负责人
            //if(!string.IsNullOrEmpty(teacherID))
            //{
            //    //解析考试负责人
            //    teacherID = teacherID.Substring(5);
            //}
            searchModel.TeacherID = teacherID;
            //开始时间
            searchModel.StartDate = txtStartDate.Value;
            searchModel.StartToDate = txtStartToDate.Value;
            //结束时间
            searchModel.EndDate = txtEndDate.Value;
            searchModel.EndToDate = txtEndToDate.Value;
            //考试地点
            searchModel.Addr = txtAddr.Value;
            //考试状态
            searchModel.StatusID = ddlStatus.SelectedValue;

            //查询数据
            DataTable dt = XBase.Business.Office.HumanManager.EmployeeTestBus.SearchTestInfo(searchModel);//查询数据

            //导出标题
            string headerTitle = "考试编号|主题|考试负责人|开始时间|结束时间|考试地点|考试状态|缺考人数";
            string[] header = headerTitle.Split('|');

            //导出标题所对应的列字段名称
            string columnFiled = "TestNo|Title|TeacherName|StartDate|EndDate|Addr|StatusName|AbsenceCount";
            string[] field = columnFiled.Split('|');

            XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "考试记录列表");
        }
        catch { }
    }
}

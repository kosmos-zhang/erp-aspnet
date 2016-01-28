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

public partial class Pages_Office_HumanManager_RectInterview_Info : BasePage
{
    /// <summary>
    /// 类名：RectInterview_Info
    /// 描述：面试记录列表处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/20
    /// 最后修改时间：2009/04/20
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            ddlRectPlan.Items.Clear();

            ddlRectPlan.DataSource = RectInterviewBus.GetRectPlanInfo();
            ddlRectPlan.DataValueField = "PlanNo";
            ddlRectPlan.DataTextField = "Title";
            ddlRectPlan.DataBind();
            ddlRectPlan.Items.Insert(0, new ListItem("--请选择--", ""));
            ddlRectPlan.SelectedIndex = 0;

            //获取应聘岗位列表数据
            ddlQuarter.DataSource = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
            ddlQuarter.DataValueField = "ID";
            ddlQuarter.DataTextField = "QuarterName";
            ddlQuarter.DataBind();
            ddlQuarter.Items.Insert(0, new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE));
            //新建模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_RECTINTERVIEW_EDIT;
            //获取请求参数
            string requestParam = Request.QueryString.ToString();
            //从列表过来时
            int firstIndex = requestParam.IndexOf("&");
            //返回回来时
            if (firstIndex > 0)
            {
                ////获取是否查询的标识
                //string flag = Request.QueryString["Flag"];
                ////点击查询时，设置查询的条件，并执行查询
                //if ("1".Equals(flag))
                //{
                //    //面试记录编号
                //    txtRectInterviewNo.Text = Request.QueryString["InterviewNo"];
                //    //初试面试日期
                //    txtInterviewDate.Text = Request.QueryString["InterviewDate"];
                //    txtInterviewToDate.Text = Request.QueryString["InterviewToDate"];

                //    //复试面试日期
                //    txtCheckStartDate.Text = Request.QueryString["CheckStartDate"];
                //    txtCheckEndDate.Text = Request.QueryString["CheckEndDate"];
                //    //应聘岗位
                //    ddlQuarter.SelectedValue = Request.QueryString["QuarterID"];
                //    //姓名
                //    UserTxtStaffName.Text = Request.QueryString["StaffNameDisPlay"];
                //    //hidStaffName.Value = Request.QueryString["StaffName"];

                //    //招聘方式
                //    ddlRectType.SelectedValue = Request.QueryString["RectType"];
                //    //初试结果
                //    ddlInterviewResult.SelectedValue = Request.QueryString["InterviewResult"];
                //    //复试结果
                //    ddlFinalResult.SelectedValue = Request.QueryString["FinalResult"];

                //    //获取当前页
                //    string pageIndex = Request.QueryString["PageIndex"];
                //    //获取每页显示记录数 
                //    string pageCount = Request.QueryString["PageCount"];
                //    //排序 
                //    string OrderBy = Request.QueryString["OrderBy"];
                //    //执行查询
                //    ClientScript.RegisterStartupScript(this.GetType(), "SearchRectInterview"
                //            , "<script language=javascript>this.pageCount = parseInt(" + pageCount
                //                + ");this.OrderBy = \"" + OrderBy + "\";DoSearch('" + pageIndex + "');</script>");
                //}
            }
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        //从请求中获取排序列
        string orderString = hidOrderBy.Value.Trim();

        //排序：默认为升序
        string orderBy = "asc";
        //要排序的字段，如果为空，默认为"RectApplyNo"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "InterviewNo";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "desc";
        }
        //从请求中获取当前页

        //int pageIndex = Convert.ToInt32(txtToPage.Value);
        ////从请求中获取每页显示记录数
        //int pageCount = Convert.ToInt32(txtShowPageCount.Value);
        ////跳过记录数
        //int skipRecord = (pageIndex - 1) * pageCount;


        //获取数据
        RectInterviewModel searchModel = new RectInterviewModel();
        //设置查询条件
        //编号
        searchModel.InterviewNo = txtRectInterviewNo.Text.Trim();
        //面试日期
        searchModel.InterviewDate = txtInterviewDate.Text.Trim();
        searchModel.Attachment = txtInterviewToDate.Text.Trim();

        searchModel.InterviewResult = Request.Form["ddlInterviewResult"].ToString();
        //应聘岗位
        searchModel.QuarterID = Request.Form["ddlQuarter"].ToString();
        //姓名
        searchModel.StaffName = UserTxtStaffName.Text.Trim();
        //人力资源
        searchModel.CheckDate = txtCheckStartDate.Text.Trim();
        ////部门主管
        searchModel.CheckNote = txtCheckEndDate.Text.Trim();
        //最终结果
        searchModel.FinalResult = Request.Form["ddlFinalResult"].ToString();
        searchModel.PlanID = Request.Form["ddlRectPlan"].ToString();
        searchModel.RectType = Request.Form["ddlRectType"].ToString();

        int aaa = searchModel.InterviewNo.IndexOf('%');///过滤字符串
        if (aaa != -1)
        {
            searchModel.InterviewNo = searchModel.InterviewNo.Replace('%', ' ');
        }
        if (searchModel.StaffName != null)
        {
            int bbb = searchModel.StaffName.IndexOf('%');///过滤字符串
            if (bbb != -1)
            {
                searchModel.StaffName = searchModel.StaffName.Replace('%', ' ');
            }
        }
        string ord = orderByCol + " " + orderBy;
        int TotalCount = 0;

       
        //查询数据
        DataTable dtRectApply = new DataTable();
        if (!string.IsNullOrEmpty(txtToPage.Value))
        {
            dtRectApply = RectInterviewBus.SearchInterviewCSInfo(searchModel, 1, 10000, ord, ref TotalCount);
        }
        string[,] ht = { 
                            { "面试编号", "InterviewNo"}, 
                            { "对应招聘计划", "PlanName"}, 
                            { "招聘方式", "RectType" },
                            { "姓名", "StaffName" },
                            { "应聘岗位", "QuarterName"},
                            { "初试日期", "InterviewDate"},
                            { "初试结果","InterviewResult"},
                            { "复试日期","CheckDate"},
                            { "复试结果","FinalResult"}
                        };
        ExportExcel(dtRectApply, ht, "", "面试记录列表");




    }
}

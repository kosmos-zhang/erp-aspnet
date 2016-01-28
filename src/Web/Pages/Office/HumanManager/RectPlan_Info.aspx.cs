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
public partial class Pages_Office_HumanManager_RectPlan_Info : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //设置模块ID
            hfModuleID.Value = ConstUtil.MODULE_ID_HUMAN_RECTPLAN_EDIT;

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
                    //活动编号
                    txtRectPlanNo.Value = Request.QueryString["RectPlanNo"];
                    //主题
                    txtTitle.Value = Request.QueryString["Title"];
                    //负责人
                    txtPrincipalID.Value = Request.QueryString["Principal"];
                    UserPrincipalName.Value = Request.QueryString["PrincipalName"];
                    //开始时间
                    txtStartDate.Text = Request.QueryString["StartDate"];
                    txtStartToDate.Text = Request.QueryString["StartToDate"];
                    //招聘人数
                    txtPersonCount.Value = Request.QueryString["PersonCount"];
                    //活动状态
                    ddlStatus.SelectedValue = Request.QueryString["Status"];

                    //获取当前页
                    string pageIndex = Request.QueryString["PageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["PageCount"];
                    //排序 
                    string orderBy = Request.QueryString["OrderBy"];
                    //执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");"
                                    + "this.orderBy = \"" + orderBy + "\";SearchRectPlanInfo('" + pageIndex + "');</script>");
                }
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
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "RectApplyNo";
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
        RectPlanSearchModel searchModel = new RectPlanSearchModel();
        //设置查询条件
        //活动编号
        searchModel.RectPlanNo = txtRectPlanNo.Value.Trim();
        //主题
        searchModel.Title = txtTitle.Value.Trim();
        //开始时间
        searchModel.StartDate = txtStartDate.Text.Trim();
        searchModel.StartToDate = txtStartToDate.Text.Trim();
        //负责人
        string selectPrincipal = txtPrincipalID.Value;
        searchModel.PrincipalID = selectPrincipal;

        //招聘人数
        searchModel.PersonCount = txtPersonCount.Value .Trim ();
        //活动状态
        searchModel.StatusID = Request.Form["ddlStatus"].ToString();
        string ord = orderByCol + " " + orderBy;
        int TotalCount = 0;
        //查询数据
        DataTable dt = new DataTable();

        if (!string.IsNullOrEmpty(txtToPage.Value))
        {
            dt = RectPlanBus.SearchSpecialExport(searchModel, 1, 10000, ord, ref TotalCount);//查询数据 
        }

        string[,] ht = { 
                            { "招聘计划编号", "RectPlanNo "}, 
                            { "主题", "Title"}, 
                            { "开始时间", "StartDate" },
                            { "结束时间", "EndDate" },
                            { "负责人", "PrincipalName"},
                            { "招聘人数", "PersonCount"},
                            { "已面试","ReviewStatus"},
                            { "已录用","EmployStatus"},
                            { "计划状态","StatusName"}
                        };
        ExportExcel(dt, ht, "", "招聘计划列表");




    }
}

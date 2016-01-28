/**********************************************
 * 类作用：   申请招聘列表
 * 建立人：   吴志强
 * 建立时间： 2009/04/17
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using XBase.Business.Office.HumanManager;
using System.Web.UI.WebControls;
using XBase.Model.Office.HumanManager;
public partial class Pages_Office_HumanManager_RectApply_Info : BasePage
{
    /// <summary>
    /// 类名：RectApply_Info
    /// 描述：申请招聘列表
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/17
    /// 最后修改时间：2009/04/17
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            //新建招聘申请的模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_RECTAPPLY_EDIT;
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
  
    protected void btnImport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        //从请求中获取排序列
        string orderString = hidOrderBy.Value .Trim ();

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
        
        //int pageIndex =Convert .ToInt32 ( txtToPage.Value);
        ////从请求中获取每页显示记录数
        //int pageCount = Convert.ToInt32(txtShowPageCount.Value);
        ////跳过记录数
        //int skipRecord = (pageIndex - 1) * pageCount;

        //获取数据
        RectApplyModel searchModel = new RectApplyModel();
        //设置查询条件

        searchModel.RectApplyNo = txtRectApplyNo.Text.Trim();
        searchModel.DeptID = hidDeptID.Value;//申请部门
        //申请日期
        //searchModel.UsedDate  = request.QueryString["ApplyDate"];
        //searchModel.JobName = request.QueryString["JobName"];//职位名称
        searchModel.FlowStatusID = Request.Form["ddlFlowStatus"].ToString();
        searchModel.BillStatus = Request.Form["DropDownList1"].ToString();

        string ord = orderByCol + " " + orderBy;
        int TotalCount = 0;


        //查询数据
        DataTable dtRectApply = new DataTable();
        if (!string.IsNullOrEmpty(txtToPage.Value))
        {
            dtRectApply = RectApplyBus.SearchRectApplyInfo(searchModel, 1, 10000, ord, ref TotalCount);//查询数据
        }
        string[,] ht = { 
                            { "申请编号", "RectApplyNo"}, 
                            { "制单时间 ", "CreateDate"}, 
                            { "申请部门 ", "DeptName" },
                            { "编制定额 ", "MaxNum" },
                            { "现有人数 ", "NowNum"},
                            { "总需求人数", "RequireNum"},
                            { "审批状态","FlowStatusName"}
                          
                        };
        ExportExcel(dtRectApply, ht, "", "招聘申请列表");
    }
}

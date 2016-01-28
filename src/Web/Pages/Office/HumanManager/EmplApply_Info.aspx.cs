/**********************************************
 * 类作用：   调职申请单列表
 * 建立人：   吴志强
 * 建立时间： 2009/04/22
 ***********************************************/
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
using XBase.Common;

public partial class Pages_Office_HumanManager_EmplApply_Info : BasePage
{
    /// <summary>
    /// 类名：EmplApply_Info
    /// 描述：调职申请单列表
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/22
    /// 最后修改时间：2009/04/22
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            //新建模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLAPPLY_EDIT;
            //获取请求参数
            string requestParam = Request.QueryString.ToString();
            //从列表过来时
            int firstIndex = requestParam.IndexOf("&");
            //返回回来时
            if (firstIndex > 0)
            {
                //获取是否查询的标识
                //string flag = Request.QueryString["Flag"];
                ////点击查询时，设置查询的条件，并执行查询
                //if ("1".Equals(flag))
                //{
                //    //编号
                //    txtEmplApplyNo.Text = Request.QueryString["EmplApplyNo"];
                //    //申请人
                //    UserApply.Text = Request.QueryString["ApplyName"];
                //    hidApplyID.Value = Request.QueryString["ApplyID"];
                //    //申请日期
                //    txtApplyDate.Text = Request.QueryString["ApplyDate"];
                //    txtApplyToDate.Text = Request.QueryString["ApplyToDate"];
                //    //审批状态
                //    ddlFlowStatus.SelectedValue = Request.QueryString["FlowStatus"];

                //    //获取当前页
                //    string pageIndex = Request.QueryString["PageIndex"];
                //    //获取每页显示记录数 
                //    string pageCount = Request.QueryString["PageCount"];
                //    //执行查询
                //    ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                //            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");DoSearch('" + pageIndex + "');</script>");
                //}
            }
        }
    }
    protected void btnImport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {
            int TotalCount = 0;


            XBase.Model.Office.HumanManager.EmplApplyModel searchModel = new XBase.Model.Office.HumanManager.EmplApplyModel();

            searchModel.EmplApplyNo = txtEmplApplyNo.Text;//申请编号
            searchModel.EmployeeID = hidApplyID.Value;//申请人
            searchModel.ApplyDate = txtApplyDate.Text;
            searchModel.ApplyToDate = txtApplyToDate.Text;//申请日期
            searchModel.FlowStatusID = ddlFlowStatus.SelectedValue;//审批状态

            DataTable dt = XBase.Business.Office.HumanManager.EmplApplyBus.SearchEmplApplyInfo(searchModel, 1, 1000000, "EmplApplyNo", ref TotalCount);

            //导出标题
            string headerTitle = "申请编号|申请人|申请时间|目前部门|目前岗位|目前岗位职等|希望日期|调至部门|调至岗位|调至岗位职等|审批状态";
            string[] header = headerTitle.Split('|');

            //导出标题所对应的列字段名称
            string columnFiled = "EmplApplyNo|EmployeeName|ApplyDate|NowDeptName|NowQuarterName|NowAdminLevelName|HopeDate|NewDeptName|NewQuarterName|NewAdminLevelName|FlowStatusName";
            string[] field = columnFiled.Split('|');

            XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "调职申请列表");
        }
        catch { }
    }
}

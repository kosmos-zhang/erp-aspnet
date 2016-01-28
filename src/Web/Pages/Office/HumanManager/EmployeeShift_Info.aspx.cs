/**********************************************
 * 类作用：   调职单列表
 * 建立人：   吴志强
 * 建立时间： 2009/04/25
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using XBase.Business.Office.HumanManager;
using System.Web.UI.WebControls;

public partial class Pages_Office_HumanManager_EmployeeShift_Info : BasePage
{
    /// <summary>
    /// 类名：EmployeeShift_Info
    /// 描述：调职单列表
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/25
    /// 最后修改时间：2009/04/25
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            //新建模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_SHIFT_EDIT;
            //对应调职单
            DataTable dtApply = EmplApplyNotifyBus.GetApplyInfo(true);
            ddlApply.DataSource = dtApply;
            ddlApply.DataValueField = "EmplApplyNo";
            ddlApply.DataTextField = "Title";
            ddlApply.DataBind();
            ListItem item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
            ddlApply.Items.Insert(0, item);
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
                //    //调职单编号
                //    txtNotifyNo.Text = Request.QueryString["NotifyNo"];
                //    //调职单主题
                //    txtTitle.Text = Request.QueryString["Title"];
                //    //对应申请单
                //    ddlApply.SelectedValue = Request.QueryString["EmplApplyNo"];
                //    //员工
                //    hidEmployeeID.Value = Request.QueryString["EmployeeID"];
                //    UserEmployeeName.Text = Request.QueryString["EmployeeName"];

                //    //获取当前页
                //    string pageIndex = Request.QueryString["PageIndex"];
                //    //获取每页显示记录数 
                //    string pageCount = Request.QueryString["PageCount"];
                //    //排序 
                //    string OrderBy = Request.QueryString["OrderBy"];
                //    //执行查询
                //    ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                //            , "<script language=javascript>this.pageCount = parseInt(" + pageCount
                //            + ");this.OrderBy = \"" + OrderBy + "\";DoSearch('" + pageIndex + "');</script>");
                //}
            }
        }
    }
    protected void btnImport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {
            XBase.Model.Office.HumanManager.EmplApplyNotifyModel searchModel = new XBase.Model.Office.HumanManager.EmplApplyNotifyModel();//获取数据
            //设置查询条件
            //调职单编号
            searchModel.NotifyNo = txtNotifyNo.Text.Trim();
            //调职单主题
            searchModel.Title = txtTitle.Text.Trim();
            //对应申请单
            searchModel.EmplApplyNo = ddlApply.SelectedValue.Trim();
            //员工
            searchModel.EmployeeID = hidEmployeeID.Value.Trim();

            DataTable dt = XBase.Business.Office.HumanManager.EmplApplyNotifyBus.SearchEmplApplyNotifyInfo(searchModel);//查询数据
            for (var i = 0; i < dt.Rows.Count; i++) 
            {
                if (dt.Rows[i]["BillStatus"].ToString() == "1")
                {
                    dt.Rows[i]["BillStatus"] = "未确认";
                }
                else 
                {
                    dt.Rows[i]["BillStatus"] = "已确认";
                }
            }
            //导出标题
            string headerTitle = "调职单编号|调职单主题|对应申请单|员工编号|员工姓名|原部门|原岗位|调出时间|调入部门|调入岗位|单据状态";
            string[] header = headerTitle.Split('|');

            //导出标题所对应的列字段名称
            string columnFiled = "NotifyNo|Title|EmplApplyNo|EmployeeNo|EmployeeName|NowDeptName|NowQuarterName|OutDate|NewDeptName|NewQuarterName|BillStatus";
            string[] field = columnFiled.Split('|');

            XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "调职通知单列表");
        }
        catch { }
    }
}

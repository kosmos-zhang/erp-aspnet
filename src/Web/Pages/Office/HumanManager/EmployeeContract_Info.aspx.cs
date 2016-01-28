/**********************************************
 * 类作用：   合同列表
 * 建立人：   吴志强
 * 建立时间： 2009/04/28
 ***********************************************/
using System;
using XBase.Common;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Data;

public partial class Pages_Office_HumanManager_EmployeeContract_Info : BasePage
{
    /// <summary>
    /// 类名：EmployeeContract_Info
    /// 描述：合同列表
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/28
    /// 最后修改时间：2009/04/28
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        btnImport.Attributes["onclick"] = "return IfExp();";

        //页面初期表示
        if (!IsPostBack)
        {
            //新建模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_CONTRACT_EDIT;
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
                    //编号
                    txtContractNo.Text = Request.QueryString["ContractNo"];
                    //申请日期
                    txtTitle.Text = Request.QueryString["Title"];
                    //员工
                    UserEmployeeName.Text = Request.QueryString["EmployeeName"];
                    hidEmployeeID.Value = Request.QueryString["EmployeeID"];

                    //获取当前页
                    string pageIndex = Request.QueryString["PageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["PageCount"];
                    //排序 
                    //string OrderBy = Request.QueryString["OrderBy"];
                    //执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount
                            + ");DoSearch('" + pageIndex + "');</script>");
                }
            }
        }
    }

    protected void btnImport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {
            string orderString = hiddExpOrder.Value.Trim();//排序
            string order = "asc";//排序：降序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"

            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            string ord = " ORDER BY " + orderBy + " " + order;

            //获取数据
            EmployeeContractModel searchModel = new EmployeeContractModel();
            //设置查询条件
            //编号
            searchModel.ContractNo = txtContractNo.Text.Trim();
            //主题
            searchModel.Title = txtTitle.Text.Trim();
            //员工
            searchModel.EmployeeID = hidEmployeeID.Value;

            //查询数据
            DataTable dt = EmployeeContractBus.SearchEmployeeContractInfo(searchModel);

            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "合同编号", "合同名称", "员工编号", "员工姓名", "签约时间", "生效时间", "失效时间" },
                new string[] { "ContractNo", "Title", "EmployeeNo", "EmployeeName", "SigningDate", "StartDate", "EndDate" },
                "合同列表");
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Exp", "<script language=javascript>showPopup('../../../Images/Pic/Close.gif','../../../Images/Pic/note.gif','导出发生异常');</script>");
        }
    }
}

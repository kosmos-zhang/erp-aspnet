/**********************************************
 * 类作用：   快速调职通道
 * 建立人：   吴志强
 * 建立时间： 2009/04/24
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Web.UI.WebControls;
using XBase.Business.Office.HumanManager;

public partial class Pages_Office_HumanManager_FastShift_Info : BasePage
{
    /// <summary>
    /// 类名：FastShift_Info
    /// 描述：快速调职通道
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/24
    /// 最后修改时间：2009/04/24
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
       
        //页面初期表示
        if (!IsPostBack)
        {

            #region 下拉列表初期设置
            //行政等级 分类标识
            //ddlAdminLevel.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //ddlAdminLevel.TypeCode = ConstUtil.CODE_TYPE_QUARTER_LEVEL;
            ////添加请选择选项
            //ddlAdminLevel.IsInsertSelect = true;
            hidSysteDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

            //岗位 分类标识
            DataTable dtQuarter = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
            ddlQuarter.DataSource = dtQuarter;
            ddlQuarter.DataValueField = "ID";
            ddlQuarter.DataTextField = "QuarterName";
            ddlQuarter.DataBind();
            ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
            ddlQuarter.Items.Insert(0, Item);

            //职称 分类标识
            ddlPosition.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlPosition.TypeCode = ConstUtil.CODE_TYPE_POSITION;
            //添加请选择选项
            ddlPosition.IsInsertSelect = true;
            #endregion
            
            //模块ID设置
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_SHIFT_EDIT;

            #region 初始查询设置
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
                    txtEmployeeNo.Text = Request.QueryString["EmployeeNo"];
                    //工号
                    txtEmployeeNum.Text = Request.QueryString["EmployeeNum"];
                    //姓名
                    txtEmployeeName.Text = Request.QueryString["EmployeeName"];
                    //工种
                   // ddlContractKind.SelectedValue = Request.QueryString["ContractKind"];
                    //行政等级
                  //  ddlAdminLevel.SelectedValue = Request.QueryString["AdminLevel"];
                    //岗位
                    ddlQuarter.SelectedValue = Request.QueryString["QuarterID"];
                    //职称
                    ddlPosition.SelectedValue = Request.QueryString["PositionID"];
                    //入职时间
                    txtStartDate.Text = Request.QueryString["StartDate"];

                    //获取当前页
                    string pageIndex = Request.QueryString["PageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["PageCount"];
                    //执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");DoSearch('" + pageIndex + "');</script>");
                }
            }
            #endregion
        }
    }

    protected void btnImport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {
            int TotalCount = 0;
            XBase.Model.Office.HumanManager.EmployeeSearchModel model = new XBase.Model.Office.HumanManager.EmployeeSearchModel();//获取数据
            model.EmployeeNo = txtEmployeeNo.Text.Trim();//编号
            model.EmployeeNum = txtEmployeeNum.Text.Trim();//工号
            model.PYShort = "";//拼音缩写
            model.EmployeeName = txtEmployeeName.Text.Trim();//姓名
            model.ContractKind = "";//request.QueryString["ContractKind"].Trim();//工种
            model.AdminLevelID = "";// request.QueryString["AdminLevel"].Trim(); //行政等级
            model.QuarterID = ddlQuarter.SelectedValue; //岗位
            model.PositionID = ((DropDownList)ddlPosition.Controls[0]).SelectedValue; //职称
            model.EnterDate = txtStartDate.Text.Trim();//入职时间
            DataTable dt = XBase.Business.Office.HumanManager.EmployeeInfoBus.SearchEmployeeWorkInfo(model, 1, 1000000, "EmployeeNo", ref TotalCount);//查询数据
            dt.Columns.Add("EntryAge", typeof(String));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["EntryDate"].ToString() != "")
                    dt.Rows[i]["EntryAge"] = Convert.ToInt32(DateTime.Now.ToString("yyyy")) - Convert.ToInt32(Convert.ToDateTime(dt.Rows[i]["EntryDate"]).ToString("yyyy")) + 1;
            }

            //导出标题
            string headerTitle = "编号|工号|姓名|部门|岗位|职称|入职时间|在公司工龄";
            string[] header = headerTitle.Split('|');

            //导出标题所对应的列字段名称
            string columnFiled = "EmployeeNo|EmployeeNum|EmployeeName|DeptName|QuarterName|PositionName|EntryDate|EntryAge";
            string[] field = columnFiled.Split('|');

            XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "快速调职通道");
        }
        catch { }
    }
}

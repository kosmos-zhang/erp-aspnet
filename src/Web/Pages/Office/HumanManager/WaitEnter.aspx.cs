/**********************************************
 * 类作用：   待入职
 * 建立人：   吴志强
 * 建立时间： 2009/04/29
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using XBase.Business.Office.HumanManager;
using System.Web.UI.WebControls;

public partial class Pages_Office_HumanManager_WaitEnter : BasePage
{
    /// <summary>
    /// 类名：HumanManager_WaitEnter
    /// 描述：待入职
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/29
    /// 最后修改时间：2009/04/29
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {

        //页面初期表示
        if (!IsPostBack)
        {
            #region 下拉列表初期设置
            //学历
            ddlCulture.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlCulture.TypeCode = ConstUtil.CODE_TYPE_CULTURE;
            //添加请选择选项
            ddlCulture.IsInsertSelect = true;

            //岗位 分类标识
            DataTable dtQuarter = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
            ddlQuarter.DataSource = dtQuarter;
            ddlQuarter.DataValueField = "ID";
            ddlQuarter.DataTextField = "QuarterName";
            ddlQuarter.DataBind();
            ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
            ddlQuarter.Items.Insert(0, Item);
            //入职岗位 分类标识
            ddlEnterQuarter.DataSource = dtQuarter;
            ddlEnterQuarter.DataValueField = "ID";
            ddlEnterQuarter.DataTextField = "QuarterName";
            ddlEnterQuarter.DataBind();
            //行政等级
            //ddlEnterAdminLevel.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //ddlEnterAdminLevel.TypeCode = ConstUtil.CODE_TYPE_ADMIN_LEVEL;
            //ddlEnterAdminLevel.IsInsertSelect = true;
            //岗位职等
            ddlEnterQuarterLevel.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlEnterQuarterLevel.TypeCode = ConstUtil.CODE_TYPE_QUARTER_ADMIN;
            //ddlEnterQuarterLevel.IsInsertSelect = true;
            //职称
            ddlEnterPosition.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlEnterPosition.TypeCode = ConstUtil.CODE_TYPE_POSITION;
            ddlEnterPosition.IsInsertSelect = true;
            
            //ddlEnterPosition.IsInsertSelect = true;

            //设置系统时间
            txtSystemDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            #endregion

            //模块ID设置
            hidContractModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_CONTRACT_EDIT;
            //新建人员
            hidEmplyModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_ADD;

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
                    //姓名
                    txtEmployeeName.Text = Request.QueryString["EmployeeName"];
                    //性别
                    ddlSex.SelectedValue = Request.QueryString["Sex"];
                    //学历
                    ddlCulture.SelectedValue = Request.QueryString["CultureID"];
                    //毕业院校
                    txtSchoolName.Text = Request.QueryString["SchoolName"];
                    //岗位
                    ddlQuarter.SelectedValue = Request.QueryString["QuarterID"];

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

        XBase.Model.Office.HumanManager.EmployeeSearchModel searchModel = new XBase.Model.Office.HumanManager.EmployeeSearchModel();//获取数据
        searchModel.EmployeeNo = txtEmployeeNo.Text;//编号
        searchModel.EmployeeName = txtEmployeeName.Text;//姓名
        searchModel.SexID = ddlSex.SelectedValue.Trim();//性别
        searchModel.CultureLevel =((DropDownList)ddlCulture.Controls[0]).SelectedValue.Trim();//学历
        searchModel.SchoolName =txtSchoolName.Text;//毕业院校
        searchModel.QuarterID = ddlQuarter.SelectedValue.Trim();//应聘岗位
        searchModel.Flag = DDLFlag.SelectedValue.Trim();//应聘岗位
        int TotalCount = 0;

        DataTable dt = XBase.Business.Office.HumanManager.EmployeeInfoBus.SearchWaitEnterInfo(searchModel, 1, 10000000, "EmployeeNo", ref TotalCount);//查询数据

        //导出标题
        string headerTitle = "人员编号|姓名|性别|应聘岗位|联系方式|学历|毕业院校|专业|人员类型|复试结果";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "EmployeeNo|EmployeeName|SexName|QuarterName|Contact|CultureLevelName|SchoolName|ProfessionalName|Flag|FinalResult";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "未入职人员列表");
    }
}

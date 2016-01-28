/**********************************************
 * 类作用：   人才储备列表
 * 建立人：   吴志强
 * 建立时间： 2009/03/23
 ***********************************************/
using System;
using System.Collections;
using XBase.Common;
using XBase.Model.Office.HumanManager;
using System.Data;
using XBase.Business.Office.HumanManager;

public partial class Pages_Office_HumanManager_EmployeeReserve_Info : BasePage
{
    /// <summary>
    /// 类名：EmployeeReserve_Info
    /// 描述：人才储备列表
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/23
    /// 最后修改时间：2009/03/23
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnImport.Attributes["onclick"] = "return IfExp();";

            //文化程度 分类标识
            ddlCulture.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlCulture.TypeCode = ConstUtil.CODE_TYPE_CULTURE;
            //添加请选择选项
            ddlCulture.IsInsertSelect = true;
            //模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_ADD;
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
                    //txtEmplApplyNo.Text = Request.QueryString["EmplApplyNo"];
                    //编号
                    txtEmployeeNo.Value = Request.QueryString["EmployeeNo"];
                    //身份证
                    txtPYShort.Value = Request.QueryString["PYShort"];
                    //姓名
                    txtEmployeeName.Value = Request.QueryString["EmployeeName"];
                    //性别
                    ddlSex.Value = Request.QueryString["Sex"];
                    //文化程度
                    ddlCulture.SelectedValue = Request.QueryString["CultureID"];
                    //毕业院校
                    txtSchoolName.Value = Request.QueryString["SchoolName"];
                    //应聘岗位
                    txtPositionTitle.Value = Request.QueryString["PositionTitle"];
                    //工龄
                    txtTotalSeniority.Value = Request.QueryString["TotalSeniority"];

                    //获取当前页
                    string pageIndex = Request.QueryString["PageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["PageCount"];
                    //执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");SearchEmployeeReserve('" + pageIndex + "');</script>");
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
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"

            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            string ord = " ORDER BY " + orderBy + " " + order;

            //获取数据
            EmployeeSearchModel searchModel = new EmployeeSearchModel();
            //设置查询条件
            //编号
            searchModel.EmployeeNo = txtEmployeeNo.Value.Trim();
            //拼音缩写
            searchModel.PYShort = txtPYShort.Value.Trim();
            //姓名
            searchModel.EmployeeName = txtEmployeeName.Value.Trim();
            //性别
            //string sex = ddlSex.Value;
            searchModel.SexID = ddlSex.Value;
            //文化程度 
            searchModel.CultureLevel = ddlCulture.SelectedValue;
            //searchModel.ProfessionalID = request.QueryString["ProfessionalID"];
            //毕业院校
            searchModel.SchoolName = txtSchoolName.Value.Trim();
            //应聘岗位
            searchModel.PositionTitle = txtPositionTitle.Value.Trim();
            //工龄
            searchModel.TotalSeniority = txtTotalSeniority.Value.Trim();

            DataTable dt = EmployeeInfoBus.ExportEmployeeReserve(searchModel);

            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "编号", "工号", "姓名", "拼音缩写", "曾用名","英文名", "人员分类",
                    "证件类型","证件号码","社保卡号","应聘职务","岗位","职称","岗位职等",
                    "所在部门","入职时间","性别","出生日期","联系电话","手机号码","电子邮件","其他联系方式",
                    "籍贯","婚姻状况","学历","毕业院校","专业","政治面貌","宗教信仰",
                    "民族","户口","户口性质","国家地区","身高(厘米)","体重","视力",
                    "最高学位","健康状况","特长","计算机水平","参加工作时间","家庭住址","外语语种1",
                    "外语语种2","外语语种3","外语水平1","外语水平2","外语水平3",
                    "专业描述" },

                new string[] { "EmployeeNo", "EmployeeNum", "EmployeeName", "PYShort", "UsedName","NameEn", "Flag",
                    "DocuType","CardID","SafeguardCard","PositionTitle","QuarterName","PositionName","AdminLevelName",
                    "DeptName","EnterDate","SexName","Birth","Telephone","Mobile","EMail","OtherContact",
                    "Origin","MarriageStatus","CultureLevel","GraduateSchool","ProfessionalName","Landscape","Religion",
                    "NationalName","Account","AccountNature","CountryName","Height","Weight","Sight",
                    "Degree","HealthStatus","Features","ComputerLevel","WorkTime","HomeAddress","ForeignLanguage11",
                    "ForeignLanguage12","ForeignLanguage13","ForeignLanguageLevel1","ForeignLanguageLevel2","ForeignLanguageLevel3",
                    "ProfessionalDes" },

                "人才储备列表");
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Exp", "<script language=javascript>showPopup('../../../Images/Pic/Close.gif','../../../Images/Pic/note.gif','导出发生异常');</script>");
        }
    }
}

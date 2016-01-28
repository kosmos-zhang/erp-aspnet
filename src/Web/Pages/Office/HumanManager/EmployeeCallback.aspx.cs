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
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;

public partial class Pages_Office_HumanManager_EmployeeCallback : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //文化程度 分类标识
            ddlCulture.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlCulture.TypeCode = ConstUtil.CODE_TYPE_CULTURE;
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

    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {

    }
}

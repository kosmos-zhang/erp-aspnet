/**********************************************
 * 类作用：   用户管理
 * 建立人：   陶春  
 * 建立时间： 2009/01/10
 ***********************************************/

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using XBase.Common;
using XBase.Business.Office.SystemManager;

public partial class Pages_Office_SystemManager_UserInfo_Query : BasePage
{
    /// <summary>
    /// 类名：UserInfo_Query
    /// 描述：用户管理处理
    /// 
    /// 作者：陶春
    /// 创建时间：2009/01/10
    /// 最后修改时间：2009/01/10
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //判断公司是否启用USBKEY
            IsCompanyOpen.Value =UserInfoBus.IsOpenValidateByCompany(UserInfo.CompanyCD) ? "1" : "0";

            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            BindDrpList(companyCD);
            this.hidModuleID.Value = ConstUtil.Menu_AddUserInfo;

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

                    this.txtUserID.Value = Request.QueryString["UserID"];

                    chklockflag.Value = Request.QueryString["LockFlag"];
                    EmployeeID.Value = Request.QueryString["UserName"];
                    txtOpenDate.Value = Request.QueryString["OpenDate"];
                    txtCloseDate.Value = Request.QueryString["CloseDate"];
                    //获取当前页
                    string pageIndex = Request.QueryString["PageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["PageCount"];
                    //执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");Fun_Search_UserInfo('" + pageIndex + "');</script>");
                }
            }
        }
    }
    /// <summary>
    /// 绑定员工姓名
    /// </summary>
    private void BindDrpList(string companyCD)
    {
        DataTable dt_employee = UserInfoBus.GetEmployeeInfo(companyCD);
        if (dt_employee != null && dt_employee.Rows.Count > 0)
        {
            EmployeeID.Items.Clear();
            foreach (DataRow Row in dt_employee.Rows)
            {
                ListItem Employee = new ListItem();
                Employee.Text = Row["EmployeeName"].ToString() + "_" + Row["EmployeeNo"].ToString();
                Employee.Value = Row["ID"].ToString();
                EmployeeID.Items.Add(Employee);
            }
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        EmployeeID.Items.Insert(0, Item);
    }
 
}

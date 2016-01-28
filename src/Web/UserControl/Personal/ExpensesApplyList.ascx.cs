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
using XBase.Business.Personal.Expenses;

public partial class UserControl_Personal_ExpensesApplyList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //经办人
            string strCompanyCD = string.Empty;//单位编号
            int strDeptID = 0;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            strDeptID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID;

            DataTable dtTra = ExpensesBus.GetEmployeeName(strCompanyCD);
            UserTransactor.DataSource = dtTra;
            UserTransactor.DataTextField = "EmployeeName";
            UserTransactor.DataValueField = "ID";
            UserTransactor.DataBind();

            //申请人
            DataTable dtApp = ExpensesBus.GetEmployeeName(strCompanyCD);
            UserApplyor.DataSource = dtApp;
            UserApplyor.DataTextField = "EmployeeName";
            UserApplyor.DataValueField = "ID";
            UserApplyor.DataBind();
        }
    }
}

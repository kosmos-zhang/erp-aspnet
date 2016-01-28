/**********************************************
 * 类作用：   用户管理
 * 建立人：  陶春
 * 建立时间： 2009/01/10
 ***********************************************/

using System;
using System.Web;
using XBase.Common;
using XBase.Model.Office.SystemManager;
using XBase.Business.Office.SystemManager;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
public partial class Pages_Office_SystemManager_UserInfo_Modify :BasePage
{
    /// <summary>
    /// 类名：UserInfo_Modify
    /// 描述：用户管理处理
    /// 
    /// 作者：陶春
    /// 创建时间：2009/01/10
    /// 最后修改时间：2009/01/10
    /// </summary>
    ///

    protected void Page_Load(object sender, EventArgs e)
    {
        //第一次初期化页面
        if (!IsPostBack)
        {

            //判断公司是否启用USBKEY
            IsCompanyOpen.Value = UserInfoBus.IsOpenValidateByCompany(UserInfo.CompanyCD) ? "1" : "0";

            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//待修改
            ////获取参数用户名ID
            //string userID = Request.QueryString["UserID"];
            ////绑定员工信息
            //BindDrpList(companyCD);
            ////页面初期设置
            BindDrpList(companyCD);
            string requestParam = Request.QueryString.ToString();
            hidModuleID.Value = ConstUtil.Menu_SerchUserInfo;
             int firstIndex = requestParam.IndexOf("&");
            //返回回来时
             if (firstIndex > 0)
             {
                 string searchCondition = requestParam.Substring(firstIndex);
                 //去除参数
                 searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.Menu_AddUserInfo, string.Empty);
                 //设置检索条件
                 hidSearchCondition.Value = searchCondition;
                 InitPage(Request.QueryString["UserIDFlag"]);
             }
             this.txtUserID.Value = Request.QueryString["UserIDFlag"];

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
            ListItem select = new ListItem();
            select.Text = "－选择员工姓名－";
            select.Value = "0";
            EmployeeID.Items.Insert(0, select);
        }
    }
    #region 初期化页面
    /// <summary>
    /// 初期化页面
    /// </summary>
    /// <param name="userID">用户ID</param>
    private void InitPage(string userID)
    {
        //获得公司代码
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;                         ///待修改
        //没有参数时，新规追加用户
        if (string.IsNullOrEmpty(userID))
        {
            return;
        }
        //修改用户
        //获取用户信息
        DataTable dt = new DataTable();
        dt = UserInfoBus.GetUserInfoByID(userID, companyCD);
        txtUserID.Value = dt.Rows[0]["UserID"].ToString();
        txtUserName.Value = dt.Rows[0]["UserName"].ToString();
        EmployeeID.Value = dt.Rows[0]["EmployeeID"].ToString();
        string IsRoot = dt.Rows[0]["IsRoot"].ToString();
        if (IsRoot.Equals("1"))
        {
            txtOpenDate.Disabled = true;
            txtCloseDate.Disabled = true;
            chkLockFlag.Disabled = true;
        }
        if (!string.IsNullOrEmpty(dt.Rows[0]["OpenDate"].ToString()))
        {
          DateTime OpenDate = DateTime.Parse(dt.Rows[0]["OpenDate"].ToString());
           txtOpenDate.Value = OpenDate.ToString("yyyy-MM-dd");
        }
        if (!string.IsNullOrEmpty(dt.Rows[0]["CloseDate"].ToString()))
        {
            DateTime CloseDate = DateTime.Parse(dt.Rows[0]["CloseDate"].ToString());
            txtCloseDate.Value = CloseDate.ToString("yyyy-MM-dd");
        }
        chkLockFlag.Checked = ConstUtil.LOCK_FLAG_LOCKED.Equals(dt.Rows[0]["LockFlag"].ToString()) ? true : false;
        UsedStatus.Value = dt.Rows[0]["UsedStatus"].ToString();
        txtRemark.Value = dt.Rows[0]["remark"].ToString();
        chkIsHardValidate.Checked = dt.Rows[0]["IsHardValidate"].ToString() == "1" ? true : false;
    }
    #endregion
}

/**********************************************
 * 类作用：   用户/部门选择
 * 建立人：   江贻明
 * 建立时间： 2009/03/30
 ***********************************************/
using System;
using System.Data;
using XBase.Business.Common;
using System.Text;
using XBase.Common;
public partial class Pages_Common_UserOrDeptSelectNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string TypeID = Request.QueryString["TypeID"].Trim().ToString();
            txtTypeID.Value = TypeID;
            InitUserDept(null,TypeID);
        }
    }

    #region 初期表示部门员工数据
    /// <summary>
    /// 初期表示部门员工数据
    /// </summary>
    /// <param name="flag"></param>
    private void InitUserDept(string flag,string TypeID)
    {
        StringBuilder deptUserInfo = new StringBuilder();
        //获取部门信息
        DataTable dtDept = UserDeptSelectBus.GetDeptInfo(TypeID);
        //部门信息存在的时候
        if (dtDept != null && dtDept.Rows.Count > 0)
        {
            //获取员工信息
            DataTable dtUser = UserDeptSelectBus.GetUserInfo();
            deptUserInfo.AppendLine("<table id='dtDeptUser'>");
            //遍历部门，设置部门对应人员
            for (int i = 0; i < dtDept.Rows.Count; i++)
            {
                //获取部门名称
                string deptName = (string)dtDept.Rows[i]["DeptName"];
                //获取部门ID
                int deptID = (int)dtDept.Rows[i]["ID"];
                //显示部门
                deptUserInfo.AppendLine("<tr><td>" + deptName + "</td><td>");
                //员工信息
                StringBuilder deptUser = new StringBuilder(string.Empty);
                //获取部门下的员工


                string Expression = "DeptID='" + deptID.ToString() + "'";

                DataRow[] drDeptUser = dtUser.Select(Expression);
                //遍历部门的所有员工
                for (int j = 0; j < drDeptUser.Length; j++)
                {
                    //获取员工数据
                    DataRow drUserTemp = (DataRow)drDeptUser[j];
                    //获取用户ID
                    int employeesID = (int)drUserTemp["ID"];
                    //获取用户名
                    string employeesName = (string)drUserTemp["EmployeesName"];
                    if (!string.IsNullOrEmpty(employeesName))
                    {
                        if (TypeID ==ConstUtil. TYPE_DANX_CODE)
                        {
                            deptUser.AppendLine("<input type='radio' name='select' id='chk_" + deptID.ToString() + "_" + employeesID.ToString() + "'  value='" + ConstUtil.DEPT_EMPLOY_SELECT_EMPLOY
                                                   + employeesID.ToString() + "|" + employeesName + "'>" + employeesName);
                        }
                        else if (TypeID ==ConstUtil.TYPE_DUOX_CODE)
                        {
                            deptUser.AppendLine("<input type='checkbox' id='chk_" + deptID.ToString() + "_" + employeesID.ToString() + "'  value='" + ConstUtil.DEPT_EMPLOY_SELECT_EMPLOY
                            + employeesID.ToString() + "|" + employeesName + "'>" + employeesName);
                        }
                    }
       
                }
                //设置部门下的员工信息
                deptUserInfo.AppendLine(deptUser.ToString());
                deptUserInfo.AppendLine("</td></tr>");
            }
            deptUserInfo.AppendLine("</table>");
        }
        //设置员工部门信息
        divDeptUser.InnerHtml = deptUserInfo.ToString() + divDeptUser.InnerHtml;
    }
    #endregion
}

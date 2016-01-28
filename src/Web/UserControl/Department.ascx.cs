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
using XBase.Business.Common;
using System.Text;

public partial class UserControl_Department : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitUserDept();
    }

    /// <summary>
    /// 初期表示部门员工数据
    /// </summary>
    /// <param name="flag"></param>
    private void InitUserDept()
    {
        StringBuilder deptUserInfo = new StringBuilder();
        //获取部门信息
        DataTable dtDept = UserDeptSelectBus.GetDepartmentInfo();
        //部门信息存在的时候，
        if (dtDept != null && dtDept.Rows.Count > 0)
        {
            //获取员工信息
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

                deptUserInfo.AppendLine("</td></tr>");

            }
            deptUserInfo.AppendLine("</table>");
            //deptUserInfo.AppendLine("<p><input id='btnOk' type='button' value='确认'   onclick='GetUserOrDept();'> </p>");
            //deptUserInfo.AppendLine("<p><input id='btnNO' type='button' value='取消'   onclick='test();'> </p>");
        }
        //设置员工部门信息
        divDeptUser.InnerHtml = deptUserInfo.ToString() + divDeptUser.InnerHtml;
    }
}

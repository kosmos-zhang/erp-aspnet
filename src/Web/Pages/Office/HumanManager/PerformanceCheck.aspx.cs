using System;
using XBase.Common;
using System.Data;
using System.Collections;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Business.Common;

public partial class Pages_Office_HumanManager_PerformanceCheck : BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_PERFORMANCEGRADELIST;
            string requestParam = Request.QueryString.ToString();
            //通过参数个数来判断是否从菜单过来
            int firstIndex = requestParam.IndexOf("&");
            //从列表过来时
            if (firstIndex > 0)
            {
                //返回按钮可见

                //获取列表的查询条件
                string searchCondition = requestParam.Substring(firstIndex);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
            }
            else
            {
                //返回按钮不可见

            }
            if (Request.QueryString["hidIsliebiao"] != null)
            {
                hidIsliebiao.Value = "1";
            }



            if (Request.QueryString["TaskNo"] != null && Request.QueryString["EmployeeID"] != null && Request.QueryString["TemplateNo"] != null)
            {
                hidElemID.Value = Request.QueryString["TaskNo"].ToString();
                hiEmpl.Value = Request.QueryString["EmployeeID"].ToString();
                hidTemplateNo.Value = Request.QueryString["TemplateNo"].ToString();
                DataTable dt = DeptInfoDBHelper.GetEmployeeByID(Request.QueryString["EmployeeID"].ToString());
                string s=string .Empty ;
                if (dt.Rows.Count > 0)
                {
                   s="被考核人："  + dt.Rows[0]["EmployeeName"].ToString ()+"   所在部门:"+ dt.Rows[0]["DeptName"].ToString ();
                        
                }
                Emp.InnerHtml = s;
            }
                 


        }

    }
}

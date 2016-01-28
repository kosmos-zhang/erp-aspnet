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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using XBase.Common;
using XBase.Business.Office.HumanManager;

public partial class Pages_PrinttingModel_HumanManager_EmployeeTrainingPrint : System.Web.UI.Page
{
    ReportDocument rd = new ReportDocument();
    protected void Page_Init(object sender, EventArgs e)
    {
        BindMainInfo();
    }

    /*绑定主表信息*/
    protected void BindMainInfo()
    {

        /*接受参数*/

        string OrderNo = Request.QueryString["no"].ToString();


        /*读取数据*/
        DataTable dt = TrainingBus.GetRepOrder(OrderNo);
        string strEmployee = string.Empty;//参与培训的人员
        DataTable dtEmployee = TrainingBus.GetRepTrainingUser(OrderNo);
        for (int i = 0; i < dtEmployee.Rows.Count; i++)
        {
            strEmployee += dtEmployee.Rows[i]["EmployeeName"].ToString()+",";
        }
        if (strEmployee.Length > 0)
        {
           strEmployee= strEmployee.Remove(strEmployee.Length - 1,1);
        }
        if (dt.Rows.Count > 0)
        {
            dt.Rows[0]["ModifiedUserID"] = strEmployee;
        }
        DataTable dtDetail = TrainingBus.GetRepOrderDetail(OrderNo);

        /*绑定RPT*/
        if (dt != null)
        {
            /*加载主报表*/
            rd.Load(Server.MapPath(@"~/PrinttingModel/HumanManager/EmployeeTraining.rpt"));
            crViewer.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.EmployeeTraining"));

            /*加载子报表*/
            ReportDocument rdDetail = rd.Subreports["TrainingSchedule.rpt"];
            rdDetail.SetDataSource(dtDetail);
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //绑定数据
            rd.SetDataSource(dt);
            rd.Refresh();
            this.crViewer.ReportSource = rd;
            rd.SetParameterValue("Today", "制表人：" + userInfo.EmployeeName);

        }
    }
}

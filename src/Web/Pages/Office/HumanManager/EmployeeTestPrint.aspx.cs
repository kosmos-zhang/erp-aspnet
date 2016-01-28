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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
public partial class Pages_Office_HumanManager_EmployeeTestPrint : System.Web.UI.Page
{
    ReportDocument rd = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadDataBind();
    }

    void LoadDataBind()
    {
        try
        {
        string Id =Request.QueryString["Id"].ToString();
            DataSet ds = XBase.Business.Office.HumanManager.EmployeeTestBus.GetTestInfoByID(Id);
            DataTable dt = ds.Tables[0];
            DataTable dt1 = ds.Tables[1];
            if (dt != null)
            {
               // Response.Write(dt1.Rows[0][0].ToString() + dt1.Rows[0][1].ToString() + dt1.Rows[0][2].ToString());
                rd.Load(Server.MapPath(@"~/OperatingModel/CrystalReport/HumanManager/EmployeeTestReport.rpt"));
                CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.CustContact"));
                //绑定数据
                ReportDocument sub = rd.Subreports[0];
                sub.SetDataSource(dt1);

                rd.SetDataSource(dt);
                this.CrystalReportViewer1.ReportSource = rd;
                rd.SetParameterValue("creator", "制表人：" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName);
            }
        }
        catch { }
    }
}

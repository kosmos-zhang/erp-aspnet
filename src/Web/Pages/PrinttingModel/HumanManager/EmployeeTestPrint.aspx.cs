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
using System.Text;
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
            string Id = Request.QueryString["Id"].ToString();
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            DataSet ds = XBase.Business.Office.HumanManager.EmployeeTestBus.GetTestInfoByNO(Id, CompanyCD);
            DataTable dt = ds.Tables[0];
            DataTable dt1 = ds.Tables[1];
            StringBuilder sbUserNameInfo = new StringBuilder();
            if (dt != null)
            {
                
                rd.Load(Server.MapPath(@"~/OperatingModel/CrystalReport/HumanManager/EmployeeTestReport.rpt"));
                CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.CustContact"));
                //绑定数据
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        sbUserNameInfo.Append(dt1.Rows[i]["EmployeeName"] + ",");
                    }
                }
                ReportDocument sub = rd.Subreports[0];
                sub.SetDataSource(dt1);

                rd.SetDataSource(dt);
                this.CrystalReportViewer1.ReportSource = rd;
                rd.SetParameterValue("testPeople",sbUserNameInfo.ToString().TrimEnd(','));
            }
        }
        catch { }
    }
}

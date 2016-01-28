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
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
using System.Data.SqlClient;
using XBase.Common;

public partial class Pages_Office_HumanManager_PrintPerformanceDetailsByLT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {



    }


    //protected void btnQuery_Click(object sender, ImageClickEventArgs e)
    //{
    //    //实例ReportDocument对象来显示对应的报表文件
    //    ReportDocument oRpt = new ReportDocument();
    //    //报表文件路径
    //    string path = Server.MapPath(@"~/DecisionAnalysis/CrystalReport/FinanceManager/demo.rpt");
    //    oRpt.Load(path);
    //    //oRpt.SetDatabaseLogon(Reportconnstring.LoginName, Reportconnstring.LoginPwd, Reportconnstring.Server, Reportconnstring.DataBase);



    //    SqlConnection conn = new SqlConnection(connsql);
    //    conn.Open();
    //    SqlDataAdapter dp = new SqlDataAdapter("select top 5 * from dbo.report", conn);
    //    DataSet ds = new DataSet();
    //    dp.Fill(ds);
    //    conn.Close();


    //    DataTable dt = ds.Tables[0];
    //    //数据源赋值对应好报表文件配置好的数据源格式
    //    oRpt.SetDataSource(dt);//设置报表显示的数据源，支持Table及DataSet
    //    CrystalReportViewer1.ReportSource = oRpt;
    //    CrystalReportViewer1.DataBind();
    //}
}

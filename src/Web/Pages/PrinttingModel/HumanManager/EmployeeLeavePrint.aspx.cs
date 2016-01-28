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
public partial class Pages_Office_HumanManager_EmployeeLeavePrint : System.Web.UI.Page
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
            DataTable dt = XBase.Business.Office.HumanManager.MoveNotifyBus.GetMoveNotifyInfoByID(Id);
            if (dt != null)
            {

                rd.Load(Server.MapPath(@"~/OperatingModel/CrystalReport/HumanManager/MoveApplyNotifyNoReport.rpt"));
                CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.CustContact"));
                //绑定数据
                rd.SetDataSource(dt);
                this.CrystalReportViewer1.ReportSource = rd;
            }
        }
        catch { }
    }
}

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
public partial class Pages_PrinttingModel_StorageManager_PrintStorageCheckReport : System.Web.UI.Page
{
    ReportDocument rd = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.BindInfo();
    }
    protected void BindInfo()
    {
        UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        /*接受参数*/
        int ID = Convert.ToInt32(Request.QueryString["ID"].ToString());
        string ReprotNo = Request.QueryString["ReportNo"].ToString();

        string CompanyCD = UserInfo.CompanyCD;
        /*读取数据*/
        DataTable dtDetail = new DataTable();
        DataTable dt = XBase.Business.Office.StorageManager.CheckReportBus.GetReportInfo(ID);
        dtDetail = XBase.Business.Office.StorageManager.CheckReportBus.GetDetailInfo(ID);

        /*绑定RPT*/
        if (dt != null)
        {
            /*加载主报表*/          
            this.CrystalReportSource1.ReportDocument.Load(Server.MapPath(@"~/PrinttingModel/StorageManager/StorageCheckReport.rpt"));
            CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.QualityCheckReport"));
            /*加载子报表*/
            ReportDocument rdDetail = new ReportDocument();
            if (dtDetail.Rows.Count > 0)
            {

                rdDetail = CrystalReportSource1.ReportDocument.Subreports["StorageCheckReportDetail.rpt"];
                rdDetail.SetDataSource(dtDetail);              

            }
            
            //绑定数据
            CrystalReportSource1.ReportDocument.SetDataSource(dt);
            CrystalReportSource1.DataBind();
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            
            this.CrystalReportSource1.ReportDocument.SetParameterValue("Creator", userInfo.UserName);
            this.CrystalReportViewer1.ReportSource = CrystalReportSource1;
            
        }
    }
}

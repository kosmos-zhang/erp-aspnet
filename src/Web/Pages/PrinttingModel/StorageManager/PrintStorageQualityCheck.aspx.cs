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
public partial class Pages_PrinttingModel_StorageManager_PrintStorageQualityCheck : System.Web.UI.Page
{
    ReportDocument rd = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        BindInfo();
    }
    /*绑定信息*/
    protected void BindInfo()
    {
        UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        /*接受参数*/
        int ID = Convert.ToInt32(Request.QueryString["ID"].ToString());
        string FromType = Request.QueryString["FromType"].ToString();
        string ApplyNo = Request.QueryString["ApplyNo"].ToString();

        string CompanyCD = UserInfo.CompanyCD;
        /*读取数据*/
        DataTable dtDetail = new DataTable();
        DataTable dt = XBase.Business.Office.StorageManager.StorageQualityCheckPro.GetPrintQualityDB(ID);
        if (FromType.Trim() == "0")
        {
            dtDetail = XBase.Business.Office.StorageManager.StorageQualityCheckPro.GetPrintQualityDetail(ApplyNo, CompanyCD, "0");
        }
        if (FromType == "1")
        {
            dtDetail = XBase.Business.Office.StorageManager.StorageQualityCheckPro.GetPrintQualityDetail(ApplyNo, CompanyCD, "1");
        }
        if (FromType == "2")
        {
            dtDetail = XBase.Business.Office.StorageManager.StorageQualityCheckPro.GetPrintQualityDetail(ApplyNo, CompanyCD, "2");
        }

        /*绑定RPT*/
        if (dt != null)
        {
            /*加载主报表*/
            if (FromType == "0")
            {
                CrystalReportSource1.ReportDocument.Load(Server.MapPath(@"~/PrinttingModel/StorageManager/StorageQualityCheck.rpt"));
            }
            if (FromType == "1")
            {
                CrystalReportSource1.ReportDocument.Load(Server.MapPath(@"~/PrinttingModel/StorageManager/StorageQualityCheckPur.rpt"));
            }
            if (FromType == "2")
            {

                CrystalReportSource1.ReportDocument.Load(Server.MapPath(@"~/PrinttingModel/StorageManager/StorageQualityCheckMan.rpt"));
            }
          
            /*加载子报表*/
            ReportDocument rdDetail = new ReportDocument();
            if (dtDetail.Rows.Count > 0)
            {
                if (FromType == "0")
                {
                    rdDetail = CrystalReportSource1.ReportDocument.Subreports["StorageQualityCheckDetail.rpt"];
                    rdDetail.SetDataSource(dtDetail);
                }
                if (FromType == "1")
                {
                    rdDetail = CrystalReportSource1.ReportDocument.Subreports["StorageQualityCheckDetail3.rpt"];
                    rdDetail.SetDataSource(dtDetail);
                }
                if (FromType == "2")
                {
                    rdDetail = CrystalReportSource1.ReportDocument.Subreports["StorageQualityCheckDetail2.rpt"];
                    rdDetail.SetDataSource(dtDetail);
                }
            }
            CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.QualityCheckApplay"));
            //绑定数据

            CrystalReportSource1.ReportDocument.SetDataSource(dt);
            CrystalReportSource1.DataBind();
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            CrystalReportSource1.ReportDocument.SetParameterValue("Creator", userInfo.UserName);
            this.CrystalReportViewer1.ReportSource = CrystalReportSource1;
        }

    }
}

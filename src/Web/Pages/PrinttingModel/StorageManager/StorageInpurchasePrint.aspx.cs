﻿using System;
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
using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;
using System.Text;
using XBase.Common;
using CrystalDecisions.CrystalReports.Engine;


public partial class Pages_PrinttingModel_StorageManager_StorageInpurchasePrint : System.Web.UI.Page
{
    ReportDocument rd = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetData();
    }

    protected void GetData()
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string ID = Request.QueryString["ID"].Trim();
        string CompanyCD = userInfo.CompanyCD;
        DataTable dt = StorageInPurchaseBus.GetStorageInPurchaseInfo(ID, CompanyCD);
        DataTable dt1 = StorageInPurchaseBus.GetStorageInPurchaseDetailInfo(ID, CompanyCD);

        rd.Load(Server.MapPath(@"~/PrinttingModel/StorageManager/C_StorageInPurchase.rpt"));
        CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.V_StorageInPurchase"));
        ReportDocument rd1 = rd.Subreports["C_StorageInPurchaseDetail.rpt"];
        rd1.SetDataSource(dt1);
        //绑定数据
        rd.SetDataSource(dt);
        rd.Refresh();
        this.CrystalReportViewer1.ReportSource = rd;
        rd.SetParameterValue("Name", userInfo.EmployeeName);

    }
}

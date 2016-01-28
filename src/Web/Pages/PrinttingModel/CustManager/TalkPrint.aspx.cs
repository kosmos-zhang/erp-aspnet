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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using XBase.Business.Office.CustManager;
using XBase.Common;
using XBase.Business.Common;

public partial class Pages_PrinttingModel_CustManager_TalkPrint : System.Web.UI.Page
{
    ReportDocument rd = new ReportDocument();
    protected void Page_Init(object sender, EventArgs e)
    {
        LoadDataBind();
    }

    void LoadDataBind()
    {
        string id = Request.QueryString["id"].ToString();//客户编号

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        DataTable dt = TalkBus.PrintTalk(CompanyCD, id);

        if (dt != null)
        {
            rd.Load(Server.MapPath(@"~/PrinttingModel/CustManager/TalkPrint.rpt"));
            CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.CustTalk"));
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //绑定数据
            rd.SetDataSource(dt);
            rd.Refresh();
            this.CrystalReportViewer1.ReportSource = rd;
            rd.SetParameterValue("Today", "制表人：" + userInfo.EmployeeName);
        }
    }
}

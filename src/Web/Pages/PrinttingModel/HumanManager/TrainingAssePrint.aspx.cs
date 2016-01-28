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
using XBase.Business.Office.HumanManager;
using XBase.Common;
using XBase.Business.Common;


public partial class Pages_PrinttingModel_HumanManager_TrainingAssePrint : System.Web.UI.Page
{
    ReportDocument rd = new ReportDocument();
    protected void Page_Init(object sender, EventArgs e)
    {
        LoadDataBind();
    }

    void LoadDataBind()
    {
        string No = Request.QueryString["id"].ToString();//培训编号
        //string TrainingID = Request.QueryString["TrainingID"].ToString();

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                
        //获取培训基本信息
        DataTable dsTrainingAsseInfo = TrainingAsseBus.PrintTrainingAsse(CompanyCD, No);
        //设置考核结果
        DataTable dtResultInfo = TrainingAsseBus.PrintTrainingDetail(CompanyCD, No);

        if (dsTrainingAsseInfo != null)
        {
            //主报表
            rd.Load(Server.MapPath(@"~/PrinttingModel/HumanManager/TrainingAsse.rpt"));
            CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.TrainingAsse"));
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //子报表
            if (dtResultInfo != null)
            {
                ReportDocument rdResultInfo = rd.Subreports["TrainingAsseResult.rpt"];
                rdResultInfo.SetDataSource(dtResultInfo);
            }
           
            //绑定数据
            rd.SetDataSource(dsTrainingAsseInfo);

            this.CrystalReportViewer1.ReportSource = rd;
            rd.SetParameterValue("Today", "制表人：" + userInfo.EmployeeName);
        }
    }
}

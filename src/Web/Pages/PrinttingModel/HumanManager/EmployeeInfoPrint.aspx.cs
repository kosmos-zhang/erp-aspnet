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

public partial class Pages_PrinttingModel_HumanManager_EmployeeInfoPrint : System.Web.UI.Page
{
    ReportDocument rd = new ReportDocument();
    protected void Page_Init(object sender, EventArgs e)
    {
        LoadDataBind();
    }

    void LoadDataBind()
    {
        string No = Request.QueryString["No"].ToString();//编号
        string ID = Request.QueryString["ID"].ToString();//ID

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        DataTable dt = EmployeeInfoBus.PrintEmployee(CompanyCD, No);
        DataTable dtHis = EmployeeInfoBus.PrintHistory(CompanyCD, No);
        DataTable dtHisWork = new DataTable();
        dtHisWork = dtHis.Clone();
        DataTable dtHisStudy = new DataTable();
        dtHisStudy = dtHis.Clone();

        DataRow[] rows = dtHis.Select("Flag = 1");
        foreach(DataRow row in rows)
        {
            dtHisWork.Rows.Add(row.ItemArray);
        }

        DataRow[] rows2 = dtHis.Select("Flag = 2");
        foreach (DataRow row in rows2)
        {
            dtHisStudy.Rows.Add(row.ItemArray);
        }

        DataTable dtSkill = EmployeeInfoBus.PrintSkill(CompanyCD, No);
        DataTable dtContract = EmployeeContractBus.PrintContract(CompanyCD, ID);

        if (dt != null)
        {
            //主报表
            rd.Load(Server.MapPath(@"~/PrinttingModel/HumanManager/EmployeeInfoPrint.rpt"));
            CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.EmployeeInfo"));            

            //子报表
            if (dtHisWork != null)
            {
                ReportDocument rdHisWork = rd.Subreports["EmployeeHistoryWork.rpt"];
                rdHisWork.SetDataSource(dtHisWork);
            }
            if (dtHisStudy != null)
            {
                ReportDocument rdHisStudy = rd.Subreports["EmployeeHistoryStudy.rpt"];
                rdHisStudy.SetDataSource(dtHisStudy);
            }

            if (dtSkill != null)
            {
                ReportDocument rdSkill = rd.Subreports["EmployeeSkill.rpt"];
                rdSkill.SetDataSource(dtSkill);
            }

            if (dtContract != null)
            {
                ReportDocument rdContract = rd.Subreports["EmoloyeeContract.rpt"];
                rdContract.SetDataSource(dtContract);
            }
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //绑定数据
            rd.SetDataSource(dt);
            rd.SetParameterValue("Today", "制表人：" + userInfo.EmployeeName);
            this.CrystalReportViewer1.ReportSource = rd;
            
        }
    }
}

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
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using XBase.Common;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Pages_OperatingModel_PurchaseManager_PurchaseArriveCollectPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportDocument rd = new ReportDocument();
        //if (!this.Page.IsPostBack)
        //{
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //string orderBy = Request.QueryString["orderby"].ToString();//排序

        string orderString = Request.QueryString["orderby"].ToString();//排序
        string order = "DESC";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProviderNo";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ASC";//排序：降序
        }
        orderBy = orderBy + " " + order;

        string ProviderID = Request.QueryString["ProviderID"];
        string ProductID = Request.QueryString["ProductID"];
        string StartConfirmDate = Request.QueryString["StartConfirmDate"];
        string EndConfirmDate = Request.QueryString["EndConfirmDate"];

        DataTable dt = PurchaseArriveBus.PurchaseArriveCollectPrint(ProviderID, ProductID, StartConfirmDate, EndConfirmDate, CompanyCD, orderBy);

        rd.Load(Server.MapPath(@"~/OperatingModel/CrystalReport/PurchaseManager/PurchaseArriveCollect.rpt"));
        //绑定数据
        rd.SetDataSource(dt);
        rd.Refresh();
        this.CrystalReportViewer1.ReportSource = rd;
        rd.SetParameterValue("creator", "制表人：" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName);
        string DateParam = string.Empty;
        if (!string.IsNullOrEmpty(StartConfirmDate))
        {
            DateParam += "起始日期：" + StartConfirmDate;
        }
        if (!string.IsNullOrEmpty(EndConfirmDate))
        {
            DateParam += "  终止日期：" + EndConfirmDate;
        }
        rd.SetParameterValue("StartEndDate", DateParam);

        //}
    }
}

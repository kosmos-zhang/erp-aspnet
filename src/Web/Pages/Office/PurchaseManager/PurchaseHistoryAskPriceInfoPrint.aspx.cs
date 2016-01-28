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
using XBase.Common;
using XBase.Business.Office.PurchaseManager;

public partial class Pages_Office_PurchaseManager_PurchaseHistoryAskPriceInfoPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportDocument rd = new ReportDocument();


        int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;

        //设置行为参数
        string orderString = Request.Params["orderby"];//排序
        string order = "DESC";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProductID";//要排序的字段，如果为空，默认为"ProductID"
        if (orderString.EndsWith("_a"))
        {
            order = "ASC";//排序：降序
        }


        string ProductID = Request.Params["ProductID"];
        string StartPurchaseDate = Request.Params["StartPurchaseDate"];
        string EndPurchaseDate = Request.Params["EndPurchaseDate"];


        orderBy = orderBy + " " + order;
        //string temp = JsonClass.DataTable2Json();
        DataTable dt = PurchaseOrderBus.SelectPurchaseHistoryAskPricePrint(orderBy, ProductID, StartPurchaseDate, EndPurchaseDate);


        rd.Load(Server.MapPath(@"~/OperatingModel/CrystalReport/PurchaseManager/PurchaseHistoryAskPriceInfo.rpt"));
        CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.V_PurchaseHistoryAskPriceInfo"));
        
        //UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //rd.DataDefinition.FormulaFields["Creator"].Text = "\"" + "制表人:" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName + "\"";
        //绑定数据
        rd.SetDataSource(dt);
        rd.Refresh();
        this.CrystalReportViewer1.ReportSource = rd;
        string DateParam = string.Empty;
        if (!string.IsNullOrEmpty(StartPurchaseDate))
        {
            DateParam += "起始日期：" + StartPurchaseDate;
        }
        if (!string.IsNullOrEmpty(EndPurchaseDate))
        {
            DateParam += "  终止日期：" + EndPurchaseDate;
        }
        rd.SetParameterValue("StartEndDate", DateParam);
        rd.SetParameterValue("Creator", "" + "制表人:" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName + "");
    }
}

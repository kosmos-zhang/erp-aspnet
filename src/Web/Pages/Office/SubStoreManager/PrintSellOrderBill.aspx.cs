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
using XBase.Business.Office.SubStoreManager;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
public partial class Pages_Office_SubStoreManager_PrintSellOrderBill : System.Web.UI.Page
{
    ReportDocument rd = new ReportDocument();
    protected void Page_Init(object sender, EventArgs e)
    {
        Print();
    }
    /// <summary>
    /// 绑定报表
    /// </summary>
    private void Print()
    {
        UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        /*接受参数*/
        string BillID = Request.QueryString["BillID"].ToString();
        string CompanyCD = UserInfo.CompanyCD;
        /*读取数据*/
        DataTable SubSellOrderPri = SubSellOrderBus.GetSubSellOrderPrint(BillID);
        DataTable SubSellOrderDetail = SubSellOrderBus.GetSubSellOrderDetailPrint(BillID);
        /*绑定RPT*/
        if (SubSellOrderPri != null)
        {
            /*加载主报表*/
            rd.Load(Server.MapPath(@"~/PrinttingModel/SubStoreManager/PrintSellBill.rpt"));
            CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.View_PrintSubSellOrderBill"));

            /*加载子报表*/
            ReportDocument rdDetail = rd.Subreports["PrintSellBillDetail.rpt"];
            rdDetail.SetDataSource(SubSellOrderDetail);

            //绑定数据
            rd.SetDataSource(SubSellOrderPri);
            rd.Refresh();
            this.CrystalReportViewer1.ReportSource = rd;
            rd.SetParameterValue("Creator",UserInfo.UserName);
        }
    }
}

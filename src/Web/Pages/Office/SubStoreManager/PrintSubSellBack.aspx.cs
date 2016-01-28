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
public partial class Pages_Office_SubStoreManager_PrintSubSellBack : System.Web.UI.Page
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
        int BillID = int.Parse(Request.QueryString["BillID"].ToString());
        /*读取数据*/
        DataTable SubSellBackPri = SubSellBackBus.SubSellBack(BillID);
        if (SubSellBackPri != null && SubSellBackPri.Rows.Count > 0)
        {
            foreach (DataRow row in SubSellBackPri.Rows)
            {
                if (row["FromType"].ToString() == "0")
                    row["FromType"] = "无来源";
                if (row["FromType"].ToString() == "1")
                    row["FromType"] = "销售订单";
                if (row["isAddTax"].ToString() == "0")
                    row["isAddTax"] = "否";
                if (row["isAddTax"].ToString() == "1")
                    row["isAddTax"] = "是";
            }
        }
        DataTable SubSellBackDetail = SubSellBackBus.Details(BillID);
        /*绑定RPT*/
        if (SubSellBackPri != null)
        {
            /*加载主报表*/
            rd.Load(Server.MapPath(@"~/PrinttingModel/SubStoreManager/PrintSellBack.rpt"));
            CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.V_SubStoreSubSellBack"));

            /*加载子报表*/
            ReportDocument rdDetail = rd.Subreports["PrintSellBackDetail.rpt"];
            rdDetail.SetDataSource(SubSellBackDetail);

            //绑定数据
            rd.SetDataSource(SubSellBackPri);
            rd.Refresh();
            this.CrystalReportViewer1.ReportSource = rd;
            rd.SetParameterValue("Creator",UserInfo.UserName);
        }
    }
}

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
public partial class Pages_PrinttingModel_SellManager_PrintAdversaryInfo : System.Web.UI.Page
{
    ReportDocument rd = new ReportDocument();
    protected void Page_Init (object sender, EventArgs e)
    {
        BindMainInfo();
    }

    /*绑定主表信息*/
    protected void BindMainInfo()
    {
       
        /*接受参数*/

        string OrderNo = Request.QueryString["no"].ToString();

      
        /*读取数据*/
        DataTable dt = XBase.Business.Office.SellManager.AdversaryInfoBus.GetRepOrder(OrderNo);
        DataTable dtDetail = XBase.Business.Office.SellManager.AdversaryInfoBus.GetRepOrderDetail(OrderNo);

        /*绑定RPT*/
        if (dt != null)
        {
            /*加载主报表*/
            rd.Load(Server.MapPath(@"~/PrinttingModel/SellManager/AdversaryInfo.rpt"));
            crViewer.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.sellmodule_report_adversaryinfo"));

            /*加载子报表*/
            ReportDocument rdDetail = rd.Subreports["AdversaryInfoDetail.rpt"];
            rdDetail.SetDataSource(dtDetail);

            //绑定数据
            rd.SetDataSource(dt);
            rd.Refresh();
            this.crViewer.ReportSource = rd;
 UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        rd.SetParameterValue("PrintName","制表人："+ userInfo.UserName);
        }

       
    }
}

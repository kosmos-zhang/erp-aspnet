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
using XBase.Business.Office.SupplyChain;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
public partial class Pages_Office_SupplyChain_PrintProductPrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            Print();
    }
    private void Print()
    {
        string ChangeNo = Request["ChangeNo"].ToString();
        DataTable dt = ProductPriceChangeBus.GetProductPriceInfoByID(int.Parse(Request["ChangeNo"]));
        if (dt!=null&&dt.Rows.Count > 0)
        {
            ReportDocument oRpt = new ReportDocument();
            //SubreportObject chlidobj = new SubreportObject();//子报表对象
            //报表文件路径
            string path = Server.MapPath(@"~/OperatingModel/CrystalReport/SubStoreManager/PrintProductPrice.rpt");
            oRpt.Load(path);


            CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.View_PrintProductPrice"));

            //数据源赋值对应好报表文件配置好的数据源格式
            oRpt.SetDataSource(dt);//设置报表显示的数据源，支持Table及DataSet
            oRpt.Refresh();

            CrystalReportViewer1.ReportSource = oRpt;
            oRpt.SetParameterValue("creator", "制表人：" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName);
        }
      
    }

}

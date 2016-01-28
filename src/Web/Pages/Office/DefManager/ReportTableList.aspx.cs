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

using XBase.Business.Office.DefManager;
using XBase.Common;
using XBase.Model.Office.DefManager;
/// <summary>
/// 业务表列表界面
/// </summary>
public partial class ReportTableList : BasePage
{
    /// <summary>
    /// 界面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //绑定时间
            this.txtBeginDate.Value = System.DateTime.Now.ToString("yyyy-MM-01");
            this.txtEndDate.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
            if (Request.QueryString["reportid"] != null)
            {
                //获取报表头
                DataSet ds = TableReportBus.GetRePortByID(Request.QueryString["reportid"].ToString());
                report_title.Text = ds.Tables[0].Rows[0]["Menuname"].ToString();

                this.Reportlist.DataSource =  TableReportBus.GetReportTableByID(Request.QueryString["reportid"].ToString(), txtBeginDate.Value, this.txtEndDate.Value);
                this.Reportlist.DataBind();
            }
        }

        lbl_time.Text = System.DateTime.Now.ToShortDateString();
        lbl_person.Text = UserInfo.EmployeeName.ToString();
    }
    protected void btn_query_Click(object sender, ImageClickEventArgs e)
    {
        this.Reportlist.DataSource = TableReportBus.GetReportTableByID(Request.QueryString["reportid"].ToString(), txtBeginDate.Value, this.txtEndDate.Value);
        this.Reportlist.DataBind();
    }
    protected void btn_excel_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = TableReportBus.GetReportTableByID(Request.QueryString["reportid"].ToString(), txtBeginDate.Value, this.txtEndDate.Value);
        DataSet ds = TableReportBus.GetRePortByID(Request.QueryString["reportid"].ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            string headstr = string.Empty;
            foreach (DataColumn c in dt.Columns)
            {
                headstr = headstr + "," + c.ColumnName;
            }
            headstr = headstr.TrimStart(',');
            try
            {
                ExportExcelReport(dt, headstr, "", ds.Tables[0].Rows[0]["Menuname"].ToString());
            }
            catch { }
        }
    }
}

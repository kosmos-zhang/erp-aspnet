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

using XBase.Business.DefManager;
using XBase.Model.Office.DefManager;
using XBase.Business.Office.DefManager;

public partial class Pages_DefManager_CreateTableReport :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindTableList();
            if (Request.QueryString["getid"] != null)
            {
                DataSet ds = TableReportBus.GetRePortByID(Request.QueryString["getid"].ToString());

                this.txt_menu.Text = ds.Tables[0].Rows[0]["Menuname"].ToString();
                this.ddl_timeflag.SelectedValue = ds.Tables[0].Rows[0]["timeFlag"].ToString();
                this.txt_sql.Text = ds.Tables[0].Rows[0]["SqlStr"].ToString();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    hiduserid.Value = ds.Tables[1].Rows[0]["userdUserList"].ToString();
                    Hidname.Value = TableReportBus.GetUserNameList(hiduserid.Value);
                }
                for (int i = 0; i < this.tablelist.Items.Count; i++)
                {
                    foreach (string ii in ds.Tables[0].Rows[0]["tablelist"].ToString().Split(','))
                    {
                        if(this.tablelist.Items[i].Value == ii)
                        {
                            this.tablelist.Items[i].Selected = true;
                        }
                    }
                }
            }
        }
    }

    private void BindTableList()
    {
        tablelist.DataSource = TableReportBus.GetTableList();
        tablelist.DataTextField = "buildTableName";
        tablelist.DataValueField = "ID";
        tablelist.DataBind();
    }
    protected void btn_save_Click(object sender, ImageClickEventArgs e)
    {
        ReportTableModel report = new ReportTableModel();
        string tablestr = string.Empty;
        for (int i = 0; i < this.tablelist.Items.Count; i++)
        {
            if (tablelist.Items[i].Selected)
            {
                tablestr += "," + tablelist.Items[i].Value;
            }
        }
        report.Tablelist = tablestr.TrimStart(',');
        string id = "0";
        if (Request.QueryString["getid"] != null)
        {
            id = Request.QueryString["getid"].ToString();
        }
        else
        {
            id = HidControl.Value;
        }
        
        report.Menuname = this.txt_menu.Text;
        report.SqlStr = this.txt_sql.Text;
        report.timeFlag = Convert.ToInt32(this.ddl_timeflag.SelectedValue);
        report.Excelhead = "";
        HidControl.Value = TableReportBus.InsertReport(report, UserInfo, id, hiduserid.Value).ToString();
    }
}

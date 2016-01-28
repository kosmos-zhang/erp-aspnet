using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XBase.Business.Office.SellReport;
using XBase.Common;
public partial class Pages_Office_SellReport_SellRank : BasePage
{
    public int index = 1;
    public UserInfoUtil userInfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        if (!Page.IsPostBack)
        {
            this.txt_begintime.Text = DateTime.Now.ToString("yyyy-MM-01");
            this.txt_endtime.Text = DateTime.Now.ToString("yyyy-MM-dd");

            string flag = this.summaryType.Value;
            switch (flag)
            {
                case "1":
                    lbl_title.Text = "部门/分店名称";
                    break;
                case "2":
                    lbl_title.Text = "业务员姓名";
                    break;
                case "3":
                    lbl_title.Text = "产品名称";
                    break;
            }
            rpt_result1.DataSource = UserProjectInfoBus.GetSummaryData(this.txt_begintime.Text, this.txt_endtime.Text, userInfo, typeorder.Value, int.Parse(this.summaryType.Value),userInfo.SelPoint);
            rpt_result1.DataBind();
        }
    }
    protected void btn_query_Click(object sender, ImageClickEventArgs e)
    {
        this.txt_begintime.Text = HidBeginTime.Value;
        this.txt_endtime.Text = HidEndTime.Value;
        
        string flag = this.summaryType.Value;
        switch (flag)
        {
            case "1":
                lbl_title.Text = "部门/分店名称";
                break;
            case "2":
                lbl_title.Text = "业务员姓名";
                break;
            case "3":
                lbl_title.Text = "产品名称";
                break;
        }
        rpt_result1.DataSource = UserProjectInfoBus.GetSummaryData(this.txt_begintime.Text, this.txt_endtime.Text, userInfo, typeorder.Value,int.Parse(this.summaryType.Value),userInfo.SelPoint);
        rpt_result1.DataBind();
    }
}

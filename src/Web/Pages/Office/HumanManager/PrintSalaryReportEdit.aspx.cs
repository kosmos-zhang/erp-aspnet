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
using XBase.Business.Office.HumanManager ;
using System.Text;
using XBase.Common;

public partial class Pages_Office_HumanManager_PrintSalaryReportEdit : System.Web.UI.Page
{
    protected string Printclass = "onlyPrint";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
           string ss= Request.QueryString.ToString();
            this.Label1.Text = this.GetSource();
            this.HiddenCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
        }
    }

    protected string GetSource()
    {
        string ID = Request.QueryString["ID"].Trim ().ToString ();
        DataTable dtReportInfo = SalaryReportBus.GetReportInfoByNo(ID); 
        


        int defaultCount = Convert.ToInt32(this.linesPerPage.Value);
        //设置查询条件
        StringBuilder htmlStr = new StringBuilder();
        htmlStr.Append("\r\t  <table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
        //表头开始（在打印预览中每页都有）****************************************************************
        htmlStr.Append("\r\t   <thead  style=\"display:table-header-group;font-weight:bold\">");
        htmlStr.Append("\r\t   <tr><td colspan=\"18\">");


        htmlStr.Append("\r\t  <table width=\"98%\" height=\"40\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"     =\"center\">");
        htmlStr.Append("\r\t  <tr>");
        htmlStr.Append("\r\t  <td align=\"center\" class=\"pS\" valign=\"middle\"><strong> 工资报表 </strong></td>");
        htmlStr.Append("\r\t  </tr></table>");

        htmlStr.Append("\r\t  <table width=\"98%\" height=\"25px\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

        htmlStr.Append("\r\t  <tr><td align=\"left\" style=\"height: 25px;\" class=\"td\" colspan=\"2\">编制单位：" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyName + "&nbsp;</td>");
      //  htmlStr.Append("\r\t  <td align=\"center\" style=\"height: 25px;\" class=\"td\" colspan=\"3\">会计期间：" + AccountDate + "&nbsp;</td>");
        htmlStr.Append("\r\t  <td align=\"right\" style=\"height: 25px;\" class=\"td\" colspan=\"3\"> </td>");
        htmlStr.Append("\r\t  </tr></table>");

        htmlStr.Append("\r\t  <table width=\"98%\" height=\"25px\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

        htmlStr.Append("\r\t  <tr><td align=\"center\" style=\"height: 25px;\"     >  工资报表编号</td>");
 
        htmlStr.Append("\r\t  <td align=\"left\" style=\"height: 25px;\"   >  " + GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "ReprotNo") + "&nbsp;</td>");
        htmlStr.Append("\r\t   <td align=\"center\" style=\"height: 25px;\"    >  工资报表主题</td>");
        htmlStr.Append("\r\t  <td align=\"left\" style=\"height: 25px;\"    >  " + GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "ReportName") + "&nbsp;</td>");
        htmlStr.Append("\r\t   <td align=\"center\" style=\"height: 25px;\"     >  所属月份</td>");
        htmlStr.Append("\r\t  <td align=\"left\" style=\"height: 25px;\"   >  " + GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "ReportYear") + GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "ReportMonth") + "&nbsp;</td>");
        htmlStr.Append("\r\t  </tr><tr>");

        htmlStr.Append("\r\t   <td align=\"center\" style=\"height: 25px;\"      >  开始时间</td>");
        htmlStr.Append("\r\t  <td align=\"left\" style=\"height: 25px;\"   >  " + GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "StartDate") + "&nbsp;</td>");
        htmlStr.Append("\r\t   <td align=\"center\" style=\"height: 25px;\"     >  结束时间</td>");
        htmlStr.Append("\r\t  <td align=\"left\" style=\"height: 25px;\"  >  " + GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "EndDate") + "&nbsp;</td>");
        htmlStr.Append("\r\t   <td align=\"center\" style=\"height: 25px;\"    >  编制人</td>");
        htmlStr.Append("\r\t  <td align=\"left\" style=\"height: 25px;\"    >  " + GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "Creator") + "&nbsp;</td>");
        htmlStr.Append("\r\t  </tr><tr>");
        htmlStr.Append("\r\t   <td align=\"center\" style=\"height: 25px;\"    >  报表状态</td>");
        htmlStr.Append("\r\t  <td align=\"left\" style=\"height: 25px;\"   >  " + GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "Status") + "&nbsp;</td>");
        htmlStr.Append("\r\t   <td align=\"center\" style=\"height: 25px;\"    >  编制日期</td>");
        htmlStr.Append("\r\t  <td align=\"left\" style=\"height: 25px;\"   >  " + GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "CreateDate") + "&nbsp;</td>");
        //htmlStr.Append("\r\t  <td align=\"center\" style=\"height: 25px;\" class=\"td\" colspan=\"3\">会计期间：" + AccountDate + "&nbsp;</td>");
        //htmlStr.Append("\r\t  <td align=\"right\" style=\"height: 25px;\" class=\"td\" colspan=\"3\">单位：元&nbsp;</td>");
        htmlStr.Append("\r\t  </tr> ");
 
 
  
 
        htmlStr.Append("\r\t   </table><br/>");
        
        htmlStr.Append("\r\t  </td></tr>");

        string resul = SalaryReportBus.PrintInitSalaryReportInfo(GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "ReprotNo"), defaultCount, Printclass);
        htmlStr.Append(resul);
        //表尾开始（在打印预览中每页都有）****************************************************************


        htmlStr.Append("\r\t   <tfoot class='noprint2'   style=\"display:table-footer-group;font-weight:bold\">");
        htmlStr.Append("\r\t     <tr><td align=\"left\"   class=\"noprint2\"  colspan=\"7\"> 制表：&nbsp;" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName + "</td>");
        htmlStr.Append("\r\t     <td align=\"center\" style=\"height: 23px\" class=\"noprint2\" colspan=\"10\"  >打印日期：" + DateTime.Now.ToString("yyyy-MM-dd") + "");
        htmlStr.Append("\r\t      &nbsp;</td>");
        htmlStr.Append("\r\t     </tr></tfoot>");
        //表尾结束（在打印预览中每页都有）****************************************************************


        htmlStr.Append("\r\t     </table>");



        return htmlStr.ToString();
    }

    protected void btnOutWord_Click(object sender, EventArgs e)
    {
        //HttpResponse resp;
        //resp = Page.Response;
        //resp.ContentEncoding = System.Text.Encoding.Default;
        ////设置查询条件
        //string AccountDate = "";

        //AccountDate = Request.QueryString["AccountDate"].Trim().ToString();


        //DataTable dt = VoucherBus.BalanceSheetSource(AccountDate);

        //string OutFile = "资产负债表" + ".xls";
        //resp.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(OutFile)));
        //string ls_item = "";
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "资产负债表" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(13);
        //resp.Write(ls_item);

        //ls_item = "";
        //ls_item += "会计期间:" + Convert.ToChar(9);
        //ls_item += AccountDate + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(13);
        //resp.Write(ls_item);

        //ls_item = "";
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(13);
        //resp.Write(ls_item);

        //ls_item = "";
        //int i = 0;
        //ls_item += "资产" + Convert.ToChar(9);
        //ls_item += "行次" + Convert.ToChar(9);
        //ls_item += "期末数" + Convert.ToChar(9);
        //ls_item += "期初数" + Convert.ToChar(9);
        //ls_item += "负债和所有者（或股东）权益" + Convert.ToChar(9);
        //ls_item += "行次" + Convert.ToChar(9);
        //ls_item += "期末数" + Convert.ToChar(9);
        //ls_item += "期初数" + Convert.ToChar(13);
        //resp.Write(ls_item);

        //ls_item = "";
        //for (i = 0; i < dt.Rows.Count; i++)
        //{
        //    ls_item += dt.Rows[i]["Asset"].ToString() + Convert.ToChar(9);
        //    ls_item += dt.Rows[i]["ALine"].ToString() + Convert.ToChar(9);
        //    ls_item += dt.Rows[i]["AEndAmount"].ToString() + Convert.ToChar(9);
        //    ls_item += dt.Rows[i]["AYearBeginAmount"].ToString() + Convert.ToChar(9);
        //    ls_item += dt.Rows[i]["Debt"].ToString() + Convert.ToChar(9);
        //    ls_item += dt.Rows[i]["DLine"].ToString() + Convert.ToChar(9);
        //    ls_item += dt.Rows[i]["DEndAmount"].ToString() + Convert.ToChar(9);
        //    ls_item += dt.Rows[i]["DYearBeginAmount"].ToString() + Convert.ToChar(13);
        //    resp.Write(ls_item);
        //    ls_item = "";
        //}


        //ls_item = "";
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(13);
        //resp.Write(ls_item);

        //ls_item = "";
        //ls_item += "制表：" + Convert.ToChar(9);
        //ls_item += ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "打印日期：" + Convert.ToChar(9);
        //ls_item += DateTime.Now.ToString("yyyy-MM-dd") + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(9);
        //ls_item += "" + Convert.ToChar(13);
        //resp.Write(ls_item);
        //resp.End();
    }
}

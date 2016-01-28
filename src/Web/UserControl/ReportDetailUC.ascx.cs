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
using System.Text;

public partial class UserControl_ReportDetailUC : System.Web.UI.UserControl
{
    private string[,] _listarr;
    private string[] _requestarr;
    private string _title;
    private string _ajaxurl;
    private DataTable _importdt;
    /// <summary>
    /// 显示的列表字段
    /// </summary>
    public string[,] ListArr
    {
        set { _listarr = value; }
        get { return _listarr; }
    }

    /// <summary>
    /// 请求集合
    /// </summary>
    public string[] RequestArr
    {
        set { _requestarr = value; }
        get { return _requestarr; }
    }
    
    /// <summary>
    /// 列表标题
    /// </summary>
    public string DetailTitle
    {
        set { _title = value; }
        get { return _title; }
    }

    /// <summary>
    /// ajax网页地址
    /// </summary>
    public string AjaxUrl
    {
        set { _ajaxurl = value; }
        get { return _ajaxurl; }
    }

    /// <summary>
    /// 导出表
    /// </summary>
    public DataTable ImportDT
    {
        set { _importdt = value; }
        get { return _importdt; }
    }

    public string Condition = "";
    public XBase.Common.UserInfoUtil UserInfo=(XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
    protected void Page_Load(object sender, EventArgs e)
    {
        for (int i = 0; i < RequestArr.Length;i++)
        {
            Condition += "&" + RequestArr[i] + "=" + System.Web.HttpUtility.UrlEncode(Request.QueryString[RequestArr[i]],System.Text.Encoding.GetEncoding("gb2312"));

        }
    }

    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
      
        ExportExcel(ImportDT, ListArr, "", DetailTitle);
    }


    /// <summary>
    /// 明细表Execl导出
    /// </summary>
    /// <param name="DetailDT">明细数据源</param>
    /// <param name="ht">excel列表</param>
    /// <param name="subhead">子表头，如果不加字表头，可以赋""值</param>
    /// <param name="FileName">主表头，以及excel文件名</param>
    protected void ExportExcel(DataTable DetailDT, string[,] ht, string subhead, string FileName)
    {
        StringBuilder OutTable = new StringBuilder();
        OutTable.Append("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
        OutTable.Append("<tr style=\"height:50px; font-weight:bold;font-size:20pt\" align=\"center\">");
        OutTable.Append("<td colspan='" + Convert.ToString(ht.Length / 2) + "'>");
        OutTable.Append(FileName);
        OutTable.Append("</td>");
        OutTable.Append("</tr>");
        if (subhead.Length > 0)
        {
            OutTable.Append("<tr style=\"height:30px;\" align=\"right\">");
            OutTable.Append("<td colspan='" + Convert.ToString(ht.Length / 2) + "'>");
            OutTable.Append(subhead);
            OutTable.Append("</td>");
            OutTable.Append("</tr>");
        }
        OutTable.Append("<tr style=\"height:30px;\">");
        for (int i = 0; i < ht.Length / 2; i++)
        {
            OutTable.Append("<td>");
            OutTable.Append(ht[i, 0]);
            OutTable.Append("</td>");
        }

        OutTable.Append("</tr>");
        for (int n = 0; n < DetailDT.Rows.Count; n++)
        {
            OutTable.Append("<tr style=\"height:30px;\">");
            for (int ii = 0; ii < ht.Length / 2; ii++)
            {
                OutTable.Append("<td>");
                OutTable.Append(DetailDT.Rows[n][ht[ii, 1]].ToString());
                OutTable.Append("</td>");
            }
            OutTable.Append("</tr>");
        }
        OutTable.Append("<tr style=\"height:30px;\">");
        OutTable.Append("<td colspan='" + Convert.ToString(ht.Length / 2) + "'><span style=\"width:50%\" align=\"left\">");
        OutTable.Append("制表人:");
        OutTable.Append(UserInfo.EmployeeName);
        OutTable.Append("\t</span><span align=\"right\">");
        OutTable.Append("制表时间" + DateTime.Now.ToShortDateString());
        OutTable.Append("</span></td>");
        OutTable.Append("</tr>");
        OutTable.Append("</table>");
        ExportDsToXls( FileName, OutTable.ToString());
    }

    /// <summary>
    /// 导出EXCEL方法
    /// </summary>
    /// <param name="page"></param>
    /// <param name="fileName"></param>
    /// <param name="html"></param>
    protected void ExportDsToXls(string fileName, string html)
    {

        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        Response.Clear();
        Response.Charset = "gb2312";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName) + ".xls");
        Response.Write("<html><head><META http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\"></head><body>");

        Response.Write(html);

        Response.Write(tw.ToString());
        Response.Write("</body></html>");
        Response.End();
        hw.Close();
        hw.Flush();
        tw.Close();
        tw.Flush();


    }


}


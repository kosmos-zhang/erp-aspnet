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
//using Dundas.Charting.WebControl;

public partial class UserControl_ReportGraphsUC : System.Web.UI.UserControl
{
    
    public XBase.Common.UserInfoUtil UserInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Chart1.ChartAreas["Default"].CursorX.UserEnabled =
           this.AxisList.SelectedIndex == 0 || this.AxisList.SelectedIndex == 2;

        this.Chart1.ChartAreas["Default"].CursorY.UserEnabled =
            this.AxisList.SelectedIndex == 1 || this.AxisList.SelectedIndex == 2;
        this.Chart1.ChartAreas["Default"].AxisX.View.MinSize = 10;
        this.Chart1.ChartAreas["Default"].AxisY.View.MinSize = 50;
        Chart1.AJAXZoomEnabled = true;
        ChartHandle(false, ChartColorPalette.SemiTransparent, SeriesChartType.Spline, MarkerStyle.Circle, true);
        try
        {
            DetailUrl += "?" + BindIndex + "='+IndeId+'";
            for (int i = 0; i < ReqArr.Length / 2; i++)
            {
                DetailUrl += "&" + ReqArr[i, 0] + "=" + ReqArr[i, 1];
            }
        }
        catch
        {}

    }

    #region Button Events




    /// <summary>
    /// 生成检索图表事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btn_search_Click(object sender, EventArgs e)
    {
        ChartHandle(false, ChartColorPalette.SemiTransparent, SeriesChartType.Spline, MarkerStyle.Circle, true);
        foreach (Series series in Chart1.Series)
        {
            series.CustomAttributes = "LabelStyle=Top";
        }
    }

    /// <summary>
    /// 生成折线图事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_line_Click(object sender, EventArgs e)
    {
        ChartHandle(false, ChartColorPalette.SemiTransparent, SeriesChartType.Spline, MarkerStyle.Circle, true);
        foreach (Series series in Chart1.Series)
        {
            series.CustomAttributes = "LabelStyle=Top";
        }
    }

    /// <summary>
    /// 生成柱状图事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_column_Click(object sender, EventArgs e)
    {

        ChartHandle(true, ChartColorPalette.SemiTransparent, SeriesChartType.Column, MarkerStyle.None, true);
        Chart1.Series["Series1"]["PointWidth"] = "0.2";
    }
    /// <summary>
    /// 生成饼状图事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_pie_Click(object sender, EventArgs e)
    {

        ChartHandle(true, ChartColorPalette.SemiTransparent, SeriesChartType.Pie, MarkerStyle.None, false);
        Chart1.Series["Series1"]["PieLabelStyle"] = "Outside";
        Chart1.Series["Series1"]["PieLineArrowType"] = "Triangle";
        Chart1.Series["Series1"]["PieLineArrowSize"] = "1";
    }

    /// <summary>
    /// 导出EXCEL事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_exportExcel_Click(object sender, ImageClickEventArgs e)
    {
        ExportExcel(BindDT, BindSonDT, RelationExp, ListArr,Title, "", RelationVal,BindY);
    }
    #endregion

    #region 属性

    private string _bindinex, _bindx, _bindy, _titlex, _titley, _legendtext, _title, _relationexp, _relationval, _detailurl, _unit;
    private string[,] _reqarr;
    private DataTable _binddt;
    private DataTable _bindsondt;
    private string[,] _listarr;
    public string[,] ListArr
    {
        set { _listarr = value; }
        get { return _listarr; }
    }
    public string[,] ReqArr
    {
        set { _reqarr = value; }
        get { return _reqarr; }
    }
    public string RelationVal
    {
        set { _relationval = value; }
        get { return _relationval; }
    }
    public string RelationExp
    {
        set { _relationexp = value; }
        get { return _relationexp; }
    }
    public string Title
    {
        set { _title = value; }
        get { return _title; }
    }
    public string BindIndex
    {
        set { _bindinex = value; }
        get { return _bindinex; }
    }
    public string BindX
    {
        set { _bindx = value; }
        get { return _bindx; }
    }
    public string BindY
    {
        set { _bindy = value; }
        get { return _bindy; }
    }
    public string TitleX
    {
        set { _titlex = value; }
        get { return _titlex; }
    }
    public string TitleY
    {
        set { _titley = value; }
        get { return _titley; }
    }
    public string LegendText
    {
        set { _legendtext = value; }
        get { return _legendtext; }
    }
    public string DetailUrl
    {
        set { _detailurl = value; }
        get { return _detailurl; }
    }
    public DataTable BindDT
    {
        set { _binddt = value; }
        get { return _binddt; }
    }
    public DataTable BindSonDT
    {
        set { _bindsondt = value; }
        get { return _bindsondt; }
    }
    public string Unit
    {
        set { _unit = value; }
        get { return _unit; }
    }
    #endregion

    #region Methods
    /// <summary>
    /// 生成共用方法
    /// </summary>
    /// <param name="bools"></param>
    /// <param name="chartp"></param>
    /// <param name="col"></param>
    /// <param name="msa"></param>
    private void ChartHandle(bool bools, ChartColorPalette chartp, SeriesChartType col, MarkerStyle msa, bool legend)
    {
        FillDeptIdList(BindDT);
        if (BindDT.Rows.Count > 0)
        {

            Chart1.Series["Series1"].Points.DataBindXY(BindDT.DefaultView, BindX, BindDT.DefaultView, BindY);
            this.Chart1.ChartAreas["Default"].AxisY.Title =TitleY;
            Chart1.Series["Series1"].Palette = chartp;
            if (BindIndex != null)
            {
                Chart1.Series["Series1"].Href = "javascript:LinkDetail('#INDEX')";

            }
            else 
            {
                ImageButton1.Visible = false;
            }
            this.Chart1.ChartAreas["Default"].AxisX.Title =TitleX;

            if (legend)
            {
                Chart1.Series["Series1"].LegendText =LegendText;
            }
            else
            {
                Chart1.Series["Series1"].LegendText = "";
            }
            Chart1.ChartAreas["Default"].Area3DStyle.Enable3D = bools;
            foreach (Series series in Chart1.Series)
            {
                series.Type = col;
                series.MarkerStyle = msa;
                series.MarkerSize = 15;
                series.ToolTip = "点击查看明细";
                series.ShowLabelAsValue = true;
            }
            Chart1.Visible = true;
            nodata.Visible = false;
        }
        else
        {
            Chart1.Visible = false;
            nodata.Visible = true;
        }
    }

    /// <summary>
    /// 填充部门Hidden
    /// </summary>
    /// <param name="dt"></param>
    private void FillDeptIdList(DataTable dt)
    {
        try
        {
            string DeptIdStr = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DeptIdStr += "," + dt.Rows[i][BindIndex].ToString();
            }
            DeptIdList.Value = DeptIdStr.TrimStart(',');
        }
        catch
        {}
    }

    #endregion


    /// <summary>
    /// 关联excel导出 
    /// </summary>
    /// <param name="TotalDT">主数据</param>
    /// <param name="DetailDT">明细数据</param>
    /// <param name="Relation">主子关联</param>
    /// <param name="ht">excel列表</param>
    /// <param name="FileName">文件名称，以及主标题名称</param>
    /// <param name="subhead">子标题名称，如果不设置，赋“”值</param>
    /// <param name="cell">关联字段</param>
    protected void ExportExcel(DataTable TotalDT, DataTable DetailDT, string Relation, string[,] ht, string FileName, string subhead, string cell, string cellNum)
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
        OutTable.Append("<tr style=\"height:30px; font-weight:bold\">");
        for (int i = 0; i < ht.Length / 2; i++)
        {
            OutTable.Append("<td>");
            OutTable.Append(ht[i, 0]);
            OutTable.Append("</td>");
        }
        OutTable.Append("</tr>");
        for (int i = 0; i < TotalDT.Rows.Count; i++)
        {
            DataRow[] drlist = DetailDT.Select(Relation.Replace("#Relation#", TotalDT.Rows[i][cell].ToString()));

            for (int n = 0; n < drlist.Length; n++)
            {
                OutTable.Append("<tr style=\"height:30px\">");
                for (int ii = 0; ii < ht.Length / 2; ii++)
                {
                    OutTable.Append("<td>");
                    OutTable.Append(drlist[n][ht[ii, 1]].ToString());
                    OutTable.Append("</td>");
                }
                OutTable.Append("</tr>");
            }

            OutTable.Append("<tr style=\"height:30px; font-weight:bold\">");
            OutTable.Append("<td colspan='" + Convert.ToString(ht.Length / 2) + "'>");
            if (string.IsNullOrEmpty(Unit))
            {
                OutTable.Append(TotalDT.Rows[i][0].ToString() + "&nbsp;&nbsp;数量小计：" + TotalDT.Rows[i][cellNum].ToString() + "&nbsp;");
            }
            else
            {
                OutTable.Append(TotalDT.Rows[i][0].ToString() + "&nbsp;&nbsp;金额小计：" + TotalDT.Rows[i][cellNum].ToString() + "&nbsp;" + Unit);
            }
            OutTable.Append("</td>");
            OutTable.Append("</tr>");
        }
        OutTable.Append("<tr style=\"height:30px;\">");
        OutTable.Append("<td colspan='" + Convert.ToString(ht.Length / 2) + "'><span style=\"width:50%\" align=\"left\">");
        OutTable.Append("制表人:" + UserInfo.EmployeeName + "\t");
        OutTable.Append("</span><span align=\"right\">");
        OutTable.Append("制表时间" + DateTime.Now.ToShortDateString());
        OutTable.Append("</span></td>");
        OutTable.Append("</tr>");
        OutTable.Append("</table>");
        ExportDsToXls(FileName, OutTable.ToString());
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

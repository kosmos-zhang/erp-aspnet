/**********************************************
 * 类作用：   页面基类
 * 建立人：   吴志强
 * 建立时间： 2008/12/30
 ***********************************************/

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using XBase.Business.Common;
using XBase.Common;


public class BasePage : System.Web.UI.Page
{

    protected UserInfoUtil UserInfo;

    private int selPoint = 2;

    protected void Page_PreLoad(object sender, EventArgs e)
    {
        //获得用户页面控制权限
        UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        //XBase.Common.CRCer.GetValidateCode(this.Page);


        //获得工程路径
        String currentDomainPath = System.AppDomain.CurrentDomain.BaseDirectory;
        //Session时间过期
        if (UserInfo == null)
        {
            Response.Redirect("~/Pages/SystemErrorPage/TimeOutPage.aspx");
            return;
        }

        //获得ModuleID
        string moduleID = (string)Request.QueryString["ModuleID"];
        //ModuleID为空时，默认为不对页面进行权限控制
        if (string.IsNullOrEmpty(moduleID))
        {
            moduleID = (string)Session["curpage_ModuleID"];
            if (string.IsNullOrEmpty(moduleID))
                return;
        }
        else
        {
            Session["curpage_ModuleID"] = moduleID;
        }

        //ModuleID类型判断，如果不为数字，则输出Error
        if (!ValidateUtil.IsInt(moduleID))
        {
            // ModuleID不为数字时，为错误ID，页面跳转去没有权限的页面
            //Response.Redirect("~/Pages/SystemErrorPage/NoAuthorityPage.aspx");
            return;
        }
        //获得页面控制权限
        string[] AuthInfo = SafeUtil.GetPageAuthority(moduleID, UserInfo);
        //有权限操作页面
        if (AuthInfo != null && AuthInfo.Length > 0)
        {
            //可操作的控件显示
            for (int i = 0; i < AuthInfo.Length; i++)
            {
                try
                {
                    //设置可见
                    this.FindControl(AuthInfo[i].Trim()).Visible = true;
                }
                catch //页面没有此控件时
                {
                    //TODO
                    continue;
                }
            }
        }
        //没有权限操作页面，页面跳转
        else
        {
            // Response.Redirect("~/Pages/SystemErrorPage/NoAuthorityPage.aspx");
        }
    }
    #region 导出excel
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
    protected void ExportExcel(DataTable TotalDT, DataTable DetailDT, string Relation, string[,] ht, string FileName, string subhead, string cell, int cellNum)
    {
        try
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
                OutTable.Append(TotalDT.Rows[i][0].ToString() + "&nbsp;&nbsp;小计：" + TotalDT.Rows[i][cellNum].ToString() + "&nbsp;");
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
            ExportDsToXls(this, FileName, OutTable.ToString());
        }
        catch { }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="TotalDT">总表dt</param>
    /// <param name="DetailDT">明细dt</param>
    /// <param name="Relation">总dt-明细dt对应关系</param>
    /// <param name="ht">列表数组</param>
    /// <param name="FileName">文件名</param>
    /// <param name="subhead">自文件名</param>
    /// <param name="cell">总dt-明细dt对应子段</param>
    /// <param name="cellNum">小计列序号</param>
    /// <param name="unit">单位</param>
    protected void ExportExcel(DataTable TotalDT, DataTable DetailDT, string Relation, string[,] ht, string FileName, string subhead, string cell, int cellNum, string unit)
    {
        try
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
                if (!string.IsNullOrEmpty(unit))
                {
                    OutTable.Append(TotalDT.Rows[i][0].ToString() + "&nbsp;&nbsp;金额小计：" + TotalDT.Rows[i][cellNum].ToString() + "&nbsp;" + unit);
                }
                else
                {
                    OutTable.Append(TotalDT.Rows[i][0].ToString() + "&nbsp;&nbsp;数量小计：" + TotalDT.Rows[i][cellNum].ToString() + "&nbsp;");
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
            ExportDsToXls(this, FileName, OutTable.ToString());
        }
        catch { }
    }


    protected void ExportExcel(DataTable TotalDT, DataTable TotalDT1, DataTable DetailDT, DataTable DetailDT1, string Relation, string Relation1, string[,] ht, string[,] ht1, string FileName, string FileName1, string subhead, string cell, int cellNum, string unit)
    {
        try
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
                if (string.IsNullOrEmpty(unit))
                {
                    OutTable.Append(TotalDT.Rows[i][0].ToString() + "&nbsp;&nbsp;数量小计：" + TotalDT.Rows[i][cellNum].ToString() + "&nbsp;");
                }
                else
                {
                    OutTable.Append(TotalDT.Rows[i][0].ToString() + "&nbsp;&nbsp;金额小计：" + TotalDT.Rows[i][cellNum].ToString() + "&nbsp;" + unit);
                }
                OutTable.Append("</td>");
                OutTable.Append("</tr>");
            }

            //添加第二个表
            OutTable.Append("<tr style=\"height:50px; font-weight:bold;font-size:20pt\" align=\"center\">");
            OutTable.Append("<td colspan='" + Convert.ToString(ht1.Length / 2) + "'>");
            OutTable.Append(FileName1);
            OutTable.Append("</td>");
            OutTable.Append("</tr>");
            if (subhead.Length > 0)
            {
                OutTable.Append("<tr style=\"height:30px;\" align=\"right\">");
                OutTable.Append("<td colspan='" + Convert.ToString(ht1.Length / 2) + "'>");
                OutTable.Append(subhead);
                OutTable.Append("</td>");
                OutTable.Append("</tr>");
            }
            OutTable.Append("<tr style=\"height:30px; font-weight:bold\">");
            for (int i = 0; i < ht1.Length / 2; i++)
            {
                OutTable.Append("<td>");
                OutTable.Append(ht1[i, 0]);
                OutTable.Append("</td>");
            }
            OutTable.Append("</tr>");
            for (int i = 0; i < TotalDT1.Rows.Count; i++)
            {
                DataRow[] drlist1 = DetailDT1.Select(Relation1.Replace("#Relation#", TotalDT1.Rows[i][cell].ToString()));

                for (int n = 0; n < drlist1.Length; n++)
                {
                    OutTable.Append("<tr style=\"height:30px\">");
                    for (int ii = 0; ii < ht1.Length / 2; ii++)
                    {
                        OutTable.Append("<td>");
                        OutTable.Append(drlist1[n][ht1[ii, 1]].ToString());
                        OutTable.Append("</td>");
                    }
                    OutTable.Append("</tr>");
                }

                OutTable.Append("<tr style=\"height:30px; font-weight:bold\">");
                OutTable.Append("<td colspan='" + Convert.ToString(ht1.Length / 2) + "'>");
                if (string.IsNullOrEmpty(unit))
                {
                    OutTable.Append(TotalDT1.Rows[i][0].ToString() + "&nbsp;&nbsp;数量小计：" + TotalDT1.Rows[i][cellNum].ToString() + "&nbsp;");
                }
                else
                {
                    OutTable.Append(TotalDT1.Rows[i][0].ToString() + "&nbsp;&nbsp;金额小计：" + TotalDT1.Rows[i][cellNum].ToString() + "&nbsp;" + unit);
                }
                OutTable.Append("</td>");
                OutTable.Append("</tr>");
            }
            //第二个表结束


            OutTable.Append("<tr style=\"height:30px;\">");
            OutTable.Append("<td colspan='" + Convert.ToString(ht.Length / 2) + "'><span style=\"width:50%\" align=\"left\">");
            OutTable.Append("制表人:" + UserInfo.EmployeeName + "\t");
            OutTable.Append("</span><span align=\"right\">");
            OutTable.Append("制表时间" + DateTime.Now.ToShortDateString());
            OutTable.Append("</span></td>");
            OutTable.Append("</tr>");
            OutTable.Append("</table>");

            ExportDsToXls(this, FileName, OutTable.ToString());

        }
        catch { }
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
        try
        {
            if (DetailDT != null)
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
                        OutTable.Append(DetailDT.Rows[n][ht[ii, 1]].ToString() + Convert.ToChar(127));
                        OutTable.Append("</td>");
                    }
                    OutTable.Append("</tr>");
                }
                OutTable.Append("<tr style=\"height:30px;\">");
                OutTable.Append("<td colspan='" + Convert.ToString(ht.Length / 2) + "'><span style=\"width:50%\" align=\"left\">");
                OutTable.Append("制表人：" + UserInfo.EmployeeName + "\t");
                OutTable.Append("</span><span align=\"right\">");
                OutTable.Append("制表时间：" + DateTime.Now.ToShortDateString());
                OutTable.Append("</span></td>");
                OutTable.Append("</tr>");
                OutTable.Append("</table>");
                ExportDsToXls(this, FileName, OutTable.ToString());
            }
        }
        catch{ }
    }


    /// <summary>
    /// 明细表Execl导出 (带decimal自动格式化)
    /// </summary>
    /// <param name="DetailDT">明细数据源</param>
    /// <param name="ht">excel列表</param>
    /// <param name="subhead">子表头，如果不加字表头，可以赋""值</param>
    /// <param name="formatType">例如：0.00</param>
    /// <param name="FileName">主表头，以及excel文件名</param>
    protected void ExportExcel(DataTable DetailDT, string[,] ht, string subhead, string FileName,string formatType)
    {
        try
        {
            if (DetailDT != null)
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

                        if (DetailDT.Columns[ht[ii, 1]].DataType.Name.ToLower() == "decimal")
                        {
                            string tmp = string.Empty;
                            if (DetailDT.Rows[n][ht[ii, 1]] != null && DetailDT.Rows[n][ht[ii, 1]].ToString() != "")
                                tmp = Convert.ToDecimal(DetailDT.Rows[n][ht[ii, 1]].ToString()).ToString(formatType);
                            else
                                tmp = Convert.ToDecimal("0").ToString(formatType);

                            OutTable.Append(tmp + Convert.ToChar(127));
                        }
                        else
                        {
                            OutTable.Append(DetailDT.Rows[n][ht[ii, 1]].ToString() + Convert.ToChar(127));
                        }


                     //   OutTable.Append(DetailDT.Rows[n][ht[ii, 1]].ToString() + Convert.ToChar(127));
                        OutTable.Append("</td>");
                    }
                    OutTable.Append("</tr>");
                }
                OutTable.Append("<tr style=\"height:30px;\">");
                OutTable.Append("<td colspan='" + Convert.ToString(ht.Length / 2) + "'><span style=\"width:50%\" align=\"left\">");
                OutTable.Append("制表人：" + UserInfo.EmployeeName + "\t");
                OutTable.Append("</span><span align=\"right\">");
                OutTable.Append("制表时间：" + DateTime.Now.ToShortDateString());
                OutTable.Append("</span></td>");
                OutTable.Append("</tr>");
                OutTable.Append("</table>");
                ExportDsToXls(this, FileName, OutTable.ToString());
            }
        }
        catch  { }
    }








    protected void ExportExcelReport(DataTable DetailDT, string ht, string subhead, string FileName)
    {
        string[] arr = ht.Split(',');
        try
        {
            if (DetailDT != null)
            {
                StringBuilder OutTable = new StringBuilder();
                OutTable.Append("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                OutTable.Append("<tr style=\"height:50px; font-weight:bold;font-size:20pt\" align=\"center\">");
                OutTable.Append("<td colspan='" + Convert.ToString(arr.Length) + "'>");
                OutTable.Append(FileName);
                OutTable.Append("</td>");
                OutTable.Append("</tr>");
                if (subhead.Length > 0)
                {
                    OutTable.Append("<tr style=\"height:30px;\" align=\"right\">");
                    OutTable.Append("<td colspan='" + Convert.ToString(arr.Length) + "'>");
                    OutTable.Append(subhead);
                    OutTable.Append("</td>");
                    OutTable.Append("</tr>");
                }
                OutTable.Append("<tr style=\"height:30px;\">");
                for (int i = 0; i < arr.Length; i++)
                {
                    OutTable.Append("<td>");
                    OutTable.Append(arr[i]);
                    OutTable.Append("</td>");
                }

                OutTable.Append("</tr>");
                for (int n = 0; n < DetailDT.Rows.Count; n++)
                {
                    OutTable.Append("<tr style=\"height:30px;\">");
                    for (int ii = 0; ii < arr.Length; ii++)
                    {
                        OutTable.Append("<td>");
                        OutTable.Append(DetailDT.Rows[n][arr[ii]].ToString());
                        OutTable.Append("</td>");
                    }
                    OutTable.Append("</tr>");
                }
                OutTable.Append("<tr style=\"height:30px;\">");
                OutTable.Append("<td colspan='" + Convert.ToString(arr.Length) + "'><span style=\"width:50%\" align=\"left\">");
                OutTable.Append("制表人：" + UserInfo.EmployeeName + "\t");
                OutTable.Append("</span><span align=\"right\">");
                OutTable.Append("制表时间：" + DateTime.Now.ToShortDateString());
                OutTable.Append("</span></td>");
                OutTable.Append("</tr>");
                OutTable.Append("</table>");
                ExportDsToXls(this, FileName, OutTable.ToString());
            }
        }
        catch { }
    }



    /// <summary>
    /// 明细表Execl导出
    /// </summary>
    /// <param name="DetailDT">明细数据源</param>
    /// <param name="ht">excel列表</param>
    /// <param name="subhead">子表头，如果不加字表头，可以赋""值</param>
    /// <param name="FileName">主表头，以及excel文件名</param>
    protected void ExportExcel(DataTable DetailDT, string[,] ht, string subhead, string FileName, int Count)
    {
        try
        {
            if (DetailDT != null)
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
                OutTable.Append("<tr style=\"height:30px; font-weight:bold\">");
                OutTable.Append("<td colspan='" + Convert.ToString(ht.Length / 2) + "'>");

                OutTable.Append("小计：" + Count + "&nbsp;条");

                OutTable.Append("</td>");
                OutTable.Append("</tr>");
                OutTable.Append("<tr style=\"height:30px;\">");
                OutTable.Append("<td colspan='" + Convert.ToString(ht.Length / 2) + "'><span style=\"width:50%\" align=\"left\">");
                OutTable.Append("制表人:" + UserInfo.EmployeeName + "\t");
                OutTable.Append("</span><span align=\"right\">");
                OutTable.Append("制表时间" + DateTime.Now.ToShortDateString());
                OutTable.Append("</span></td>");
                OutTable.Append("</tr>");
                OutTable.Append("</table>");
                ExportDsToXls(this, FileName, OutTable.ToString());
            }
        }
        catch { }
    }

    #region 带有收入部分和支出部分的报表导出
    /// <summary>
    /// 带有收入部分和支出部分的报表导出
    /// </summary>
    /// <param name="DetailDT">列表table</param>
    /// <param name="SumDT">总计table</param>
    /// <param name="ht">列表字段及标题名称</param>
    /// <param name="htSum">总计字段及名称</param>
    /// <param name="prefixCount">单列：不在收入和支出的列的数量(直接从ht里取前几个)</param>
    /// <param name="InCount">在收入部分的字段(从ht中减去单列的开始计起,除了单列和收入部分的，剩下的即为支出部分)</param>
    /// <param name="FileName">名件名称</param>
    protected void ExportExcelByInOut(DataTable DetailDT,DataTable SumDT, string[,] ht,string[,] htSum,int prefixCount,int InCount, string FileName)
    {
        StringBuilder OutTable = new StringBuilder();
        OutTable.Append("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");

        OutTable.Append("<tr style=\"height:50px; font-weight:bold;font-size:20pt\" align=\"left\">");
        OutTable.Append("<td colspan='" + Convert.ToString(ht.Length / 2) + "'>&nbsp;" + FileName + "</td></tr>");
        OutTable.Append("<tr style=\"height:30px;\">");
        for (int i = 0; i < prefixCount; i++)
        {
            OutTable.Append("<td rowspan=\"2\">");
            OutTable.Append(ht[i, 0]);
            OutTable.Append("</td>");
        }
        OutTable.Append("  <td colspan=\"" + InCount + "\">收入部分</td>");
        OutTable.Append("  <td colspan=\"" + Convert.ToString((ht.Length / 2) - prefixCount -InCount) + "\">支出部分</td>");
        OutTable.Append("  </tr>");
        OutTable.Append("<tr style=\"height:30px;\">");
        for (int i = prefixCount; i < ht.Length / 2; i++)
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
                OutTable.Append(DetailDT.Rows[n][ht[ii, 1]].ToString() + Convert.ToChar(127));
                OutTable.Append("</td>");
            }
            OutTable.Append("</tr>");
        }
        OutTable.Append("<tr style=\"height:30px;\">");
        OutTable.Append("<td colspan='" + Convert.ToString(ht.Length / 2) + "'>合计： ");
        if (SumDT.Rows.Count > 0)
        {
            for(int m=0;m<htSum.Length/2;m++)
            {
                OutTable.Append(htSum[m, 0] + "：" + SumDT.Rows[0][htSum[m, 1]].ToString() + "&nbsp;&nbsp;");
            }
            
        }
        OutTable.Append("</td>");
        OutTable.Append("</tr>");
        OutTable.Append("<tr style=\"height:30px;\">");
        OutTable.Append("<td colspan='" + Convert.ToString(ht.Length / 2) + "'><span style=\"width:50%\" align=\"left\">");
        OutTable.Append("制表人:" + UserInfo.EmployeeName + "\t");
        OutTable.Append("</span><span align=\"right\">");
        OutTable.Append("制表时间" + DateTime.Now.ToShortDateString());
        OutTable.Append("</span></td>");
        OutTable.Append("</tr>");
        OutTable.Append("</table>");

        ExportDsToXls(this, FileName, OutTable.ToString());

    }
    #endregion

    /// <summary>
    /// 判断字符串是否为数字 add by Moshenlin 2009 12 19 
    /// </summary>
    /// <param name="numberString"></param>
    /// <returns></returns>
    public bool ISNumber(string numberString)
    {
        try
        {
            double num;
            num = double.Parse(numberString);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 明细表Execl导出--add Moshenlin 2009 12 19
    /// </summary>
    /// <param name="DetailDT">明细数据源</param>
    /// <param name="ht">excel列表</param>
    /// <param name="subhead">子表头，如果不加字表头，可以赋""值</param>
    /// <param name="FileName">主表头，以及excel文件名</param>
    protected void ExportExcelByAbs(DataTable DetailDT, string[,] ht, string subhead, string FileName)
    {
        try
        {
            if (DetailDT != null)
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
                        string value = DetailDT.Rows[n][ht[ii, 1]].ToString();
                        if (ISNumber(value))
                        {
                            value = Math.Abs(Convert.ToDecimal(value)).ToString();
                        }
                        OutTable.Append(value);
                        OutTable.Append("</td>");
                    }
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
                ExportDsToXls(this, FileName, OutTable.ToString());
            }
        }
        catch { }
    }


    /// <summary>
    /// 导出EXCEL方法
    /// </summary>
    /// <param name="page"></param>
    /// <param name="fileName"></param>
    /// <param name="html"></param>
    protected void ExportDsToXls(Page page, string fileName, string html)
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
    #endregion


    #region 获取显示和隐藏价格和条码 Add by hm 2010/01/26
    protected string GetIsDisplayPrice()
    {
        string IsDisplayPrice = "";
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsDisplayPrice)
        {
            IsDisplayPrice = "";
        }
        else
        {
            IsDisplayPrice = "none";
        }
        return IsDisplayPrice;
    }

    protected string GetIsBarCode()
    {
        string IsBarCode = "";
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsBarCode)
        {
            IsBarCode = "";
        }
        else
        {
            IsBarCode = "none";
        }
        return IsBarCode;
    }
    #endregion
    /// <summary>
    /// 获取金额精度位数
    /// 调用方法：1、确保页面继承BasePage,页面加载时调用GetPoint()方法，返回系统设置的金额小数位数赋值给全局变量或隐藏域 HiddenPoint.Value = GetPoint();
    /// 2、页面控件输入时调用：onchange='Number_round(this,$("#HiddenPoint").val())'
    /// 3、计算金额页面赋值时这样用：$("#Money")val(parseFloat(item.Money).toFixed($("#HiddenPoint").val()));
    /// </summary>
    /// <returns></returns>
    //protected string GetPoint()
    //{
    //    DataTable dt = XBase.Business.Office.SystemManager.ParameterSettingBus.GetPoint(UserInfo.CompanyCD, "5");
    //    if (dt != null)
    //    {
    //        if (dt.Rows.Count > 0) return dt.Rows[0]["SelPoint"].ToString();
    //        else
    //        return "2";//默认 
    //    }
    //    else
    //        return "2";//默认 
    //}
    #region 输出单据打印页面,根据打印模板设置项动态显示
    /// <summary>
    /// 根据打印模板设置项动态显示
    /// </summary>
    /// <param name="billTitle">打印页面正标题</param>
    /// <param name="strBaseFields">设置的主表字段值,用竖线分割的字符串。 例:MRPNo|DeptName</param>
    /// <param name="strDetailFields">设置的明细表字段值,用竖线分割的字符串。 例:ProdNo|ProductName|GrossCount</param>
    /// <param name="aBase">取主表返回的DataTable里的字段，以及对应页面显示出来的标题,用二维数组形式。例:在dbBaseTable里取出来的MRPNo对应的是单据编号一栏,取出来的DeptName对应的是部门一栏</param>
    /// <param name="aDetail">取明细表返回的DataTable里的字段,以及对应页面显示出来的标题,用二维数组形式。例：在dtDetailTable里取出来的ProdNo对应的是物品编号一栏，取出来的ProductName是物品名称一栏</param>
    /// <param name="dbBaseTable">取主表返回的DataTable</param>
    /// <param name="dbDetailTable">取主表返回的DataTable</param>
    /// <param name="isBaseInfo">是否是主表信息(用来区分是主表还是明细，从而输出不同的html标签)</param>
    /// <returns></returns>
    protected string WritePrintPageTable(string billTitle, string strBaseFields, string strDetailFields, string[,] aBase, string[,] aDetail, DataTable dbBaseTable, DataTable dbDetailTable, bool isBaseInfo)
    {

        //获得用户页面控制权限
        UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        selPoint = int.Parse(UserInfo.SelPoint);
        string notContains = "";
        if (isBaseInfo)
        {
            string[] arrBaseKey = strBaseFields.Split('|');
            for (int m = 0; m < arrBaseKey.Length; m++)
            {
                if (!dbBaseTable.Columns.Contains(arrBaseKey[m].ToString()))
                {
                    notContains = notContains + arrBaseKey[m].ToString() + ",";
                }
            }
        }
        else
        {
            string[] arrDetailKey = strDetailFields.Split('|');
            for (int n = 0; n < arrDetailKey.Length; n++)
            {
                if (!dbDetailTable.Columns.Contains(arrDetailKey[n].ToString()))
                {
                    notContains = notContains + arrDetailKey[n].ToString() + ",";
                }
            }
        }
        if (!string.IsNullOrEmpty(notContains))
        {
            return notContains;
        }

        if (isBaseInfo)
        {
            StringBuilder sbBase = new StringBuilder();
            string[] arrBaseKey = strBaseFields.Split('|');

            if (arrBaseKey.Length > 0)
            {
                sbBase.AppendLine("<tr><td colspan=\"10\" align=\"center\"><font size=\"5\"><b>" + billTitle + "</b></font></td></tr><tr height=\"20\"><td colspan=\"10\" style=\"color: #cccccc\"></td></tr><tr height=\"20\">");
                for (int m = 0; m < arrBaseKey.Length; m++)
                {
                    for (int x = 0; x < aBase.Length / 2; x++)
                    {
                        if (aBase[x, 1].ToString().Equals(arrBaseKey[m].ToString()))
                        {
                            if (!aBase[x, 0].ToString().EndsWith("}"))
                            {
                                sbBase.AppendLine("<td align=\"left\" width=\"15%\">" + aBase[x, 0].ToString() + "：</td><td colspan=\"4\" align=\"left\" width=\"35%\">" + FormatValue(dbBaseTable, 0, arrBaseKey[m].ToString()) + "</td>");
                            }
                        }
                    }
                    if (m % 2 == 1)
                    {
                        if (m == (arrBaseKey.Length - 1))
                        {
                            sbBase.AppendLine("</tr>");
                        }
                        else
                        {
                            sbBase.AppendLine("</tr><tr height=\"20\">");
                        }
                    }

                }
                if (arrBaseKey.Length % 2 == 1)
                {
                    sbBase.AppendLine("<td align=\"left\" width=\"15%\"></td><td colspan=\"4\" align=\"left\" width=\"35%\"></td></tr>");
                }
            }
            return sbBase.ToString();

        }
        else
        {
            StringBuilder sbDetailTitle = new StringBuilder();
            StringBuilder sbDetailData = new StringBuilder();
            string[] arrDetailKey = strDetailFields.Split('|');
            if (arrDetailKey.Length > 0)
            {
                sbDetailData.AppendLine("<tr>");
                for (int i = 0; i < dbDetailTable.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        sbDetailTitle.AppendLine("<tr>");
                    }
                    for (int n = 0; n < arrDetailKey.Length; n++)
                    {
                        if (!dbDetailTable.Columns.Contains(arrDetailKey[n].ToString()))
                        {
                            notContains = notContains + arrDetailKey[n].ToString() + ",";
                        }
                        for (int y = 0; y < aDetail.Length / 2; y++)
                        {
                            if (aDetail[y, 1].ToString().Equals(arrDetailKey[n].ToString()))
                            {
                                if (n == 0)
                                {
                                    if (i == 0)
                                    {
                                        sbDetailTitle.AppendLine("<td  class=\"tdFirstTitleMyLove\">" + aDetail[y, 0].ToString() + "</td>");
                                    }
                                    sbDetailData.AppendLine("<td class=\"trDetailFirst\">" + FormatValue(dbDetailTable, i, arrDetailKey[n].ToString()) + "&nbsp;</td>");
                                }
                                else
                                {
                                    if (i == 0)
                                    {
                                        sbDetailTitle.AppendLine("<td align=\"left\" class=\"tdContent\">" + aDetail[y, 0].ToString() + "</td>");
                                    }
                                    sbDetailData.AppendLine("<td class=\"trDetail\">" + FormatValue(dbDetailTable, i, arrDetailKey[n].ToString()) + "&nbsp;</td>");
                                }
                            }
                        }

                    }
                    if (i == 0)
                    {
                        sbDetailTitle.AppendLine("</tr>");
                    }
                    sbDetailData.AppendLine("</tr>");
                }
                return sbDetailTitle.ToString() + sbDetailData.ToString();

            }
        }
        return "";
    }

    /// <summary>
    /// 格式化数据
    /// </summary>
    /// <param name="dt">表</param>
    /// <param name="iRow">行</param>
    /// <param name="sColumn">列</param>
    /// <returns></returns>
    private string FormatValue(DataTable dt, int iRow, string sColumn)
    {
        string returnValue = "";
        if (dt == null || dt.Rows.Count <= iRow || !dt.Columns.Contains(sColumn))
        {// 表不存在，列不存在，行不存在
            returnValue = "";
        }
        else if (dt.Columns[sColumn].DataType == typeof(decimal))
        {
            string tmp2 = new string('0', selPoint);
            tmp2 = "{0:0." + tmp2 + "}";
            returnValue = String.Format(tmp2, dt.Rows[iRow][sColumn]) + Convert.ToChar(127);
        }
        else
        {
            return dt.Rows[iRow][sColumn].ToString();
        }
        return returnValue;
    }

    #endregion


    #region 门店进销存日报表导出带合计_张玉圆
    /// <summary>
    /// 明细表Execl导出
    /// </summary>
    /// <param name="DetailDT">明细数据源</param>
    /// <param name="ht">excel列表</param>
    /// <param name="subhead">子表头，如果不加字表头，可以赋""值</param>
    /// <param name="FileName">主表头，以及excel文件名</param>
    protected void ExportExcel_Sum(DataTable SumDT, string[,] htSum, DataTable DetailDT, string[,] ht, string subhead, string FileName)
    {
        try
        {
            if (DetailDT != null)
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
                        OutTable.Append(DetailDT.Rows[n][ht[ii, 1]].ToString() + Convert.ToChar(127));
                        OutTable.Append("</td>");
                    }
                    OutTable.Append("</tr>");
                }
//-----------------
                OutTable.Append("<tr style=\"height:30px;\">");
                OutTable.Append("<td colspan='" + Convert.ToString(ht.Length / 2) + "'>合计： ");
                if (SumDT.Rows.Count > 0)
                {
                    for (int m = 0; m < htSum.Length / 2; m++)
                    {
                        OutTable.Append(htSum[m, 0] + "：" + SumDT.Rows[0][htSum[m, 1]].ToString() + "&nbsp;&nbsp;");
                    }
                }
                OutTable.Append("</td>");
                OutTable.Append("</tr>");
//-----------------

                OutTable.Append("<tr style=\"height:30px;\">");
                OutTable.Append("<td colspan='" + Convert.ToString(ht.Length / 2) + "'><span style=\"width:50%\" align=\"left\">");
                OutTable.Append("制表人：" + UserInfo.EmployeeName + "\t");
                OutTable.Append("</span><span align=\"right\">");
                OutTable.Append("制表时间：" + DateTime.Now.ToShortDateString());
                OutTable.Append("</span></td>");
                OutTable.Append("</tr>");
                OutTable.Append("</table>");
                ExportDsToXls(this, FileName, OutTable.ToString());
            }
        }
        catch { }
    }
    #endregion
}

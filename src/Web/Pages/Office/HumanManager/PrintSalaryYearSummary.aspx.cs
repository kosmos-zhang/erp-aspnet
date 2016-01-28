/**********************************************
 * 作    者： 王保军
 * 创建日期： 2009/06/10
 * 描    述：工资汇总报表
 * 修改日期： 2009/06/10
 * 版    本： 0.1.0
 ***********************************************/
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using XBase.Common;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;

public partial class Pages_Office_HumanManager_PrintSalaryYearSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
        //    DataBindToDept();

        //}
        //if (IsPostBack)
        //{
        //    Search();
        //}
        //if (!IsPostBack)
        //{
            string deptIDGet = Request.QueryString["DeptID"] == null ? "" : Request.QueryString["DeptID"].ToString();

            string year = Request.QueryString["year"] == null ? "" : Request.QueryString["year"].ToString();
            Label1.Text = getSalaryInfoByDept(deptIDGet ,year );
            this.HiddenCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
        //}
        ///search .InnerHtml =  
    }
    /// <summary>
    /// 绑定部门
    /// </summary>
    public void DataBindToDept()
    {
        //DataTable dt = PerformanceQueryBus.SearchDeptInfo();

        //ddlDeptName.DataSource = dt;
        //ddlDeptName.DataTextField = "DeptName";
        //ddlDeptName.DataValueField = "ID";
        //ddlDeptName.DataBind();
        //ddlDeptName.Items.Add(new System.Web.UI.WebControls.ListItem("--请选择--", "0"));
        //ddlDeptName.SelectedValue = "0";
    }
    private void Search()
    {
        //SalaryStandardModel searchModel = new SalaryStandardModel();
        ////设置查询条件
        ////岗位

        //if (ddlDeptName.SelectedValue != "0")
        //    searchModel.QuarterID = ddlDeptName.SelectedValue;
        //UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        ////查询数据
        //DataTable dtNewTable = new Table();
        //DataTable dtData = new DataTable();
        //dtData.Columns.Add("Remark");
        //dtData.Columns.Add("itemNo");
        //dtData.Columns.Add("CompanyCD");
        //dtData.Columns.Add("UnitPrice");
        //for (int i = 0; i < dtNewTable.Rows.Count; i++)
        //{
        //    DataRow newRow = dtData.NewRow();
        //    newRow["Remark"] = getDeptName(dtNewTable.Rows[i]["Remark"] == null ? "" : dtNewTable.Rows[i]["Remark"].ToString());
        //    newRow["itemNo"] = dtNewTable.Rows[i]["itemNo"] == null ? "" : dtNewTable.Rows[i]["itemNo"].ToString();
        //    newRow["CompanyCD"] = dtNewTable.Rows[i]["CompanyCD"] == null ? "" : dtNewTable.Rows[i]["CompanyCD"].ToString();
        //    newRow["UnitPrice"] = dtNewTable.Rows[i]["UnitPrice"] == null ? "" : dtNewTable.Rows[i]["UnitPrice"].ToString();
        //    dtData.Rows.Add(newRow);
        //}


        //ReportDocument oRpt = new ReportDocument();
        //CrystalReportSource1.ReportDocument.Load(Server.MapPath(@"~/OperatingModel/CrystalReport/HumanManager/SalarySummeryReport.rpt"));
        //// SetDatabaseLogon 拉模式中必须用这个方法来设置登录信息，参数一：用户名；参数二：密码；参数三：服务器；参数四：数据库名
        //CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.SalaryReportSummary"));
        ////查询数据
        //CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["Creator"].Text = "\"" + "制表人:" + userInfo.EmployeeName + "\"";
        //CrystalReportSource1.ReportDocument.SetDataSource(dtData);
        //CrystalReportSource1.DataBind();
        //// CrystalReportViewer1是水晶报表浏览器，下面是给该浏览器赋上对像
        //CrystalReportViewer1.ReportSource = CrystalReportSource1;
        //CrystalReportViewer1.DataBind();


    }
    protected string getDeptName(string DeptId)
    {

        return SalaryStandardBus.GetNameByDeptID(DeptId);


    }
    protected void imgSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Search();
    }
    public string getSalaryInfoByDept(string deptIDGet,string yearID)
    {
        string year = yearID;
        DataTable dt = SalaryStandardBus.GetDeptInfo();
        StringBuilder sb = new StringBuilder();
        sb.Append("\r\t  <table width=\"95%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"  valign=\"top\" align=\"center\"><tr align=\"center\"><td width=\"95%\" align=\"center\" valign=\"top\">");

        sb.Append("\r\t  <table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
        sb.Append("\r\t   <thead  style=\"display:table-header-group;font-weight:bold\">");
        sb.Append("\r\t    <tr><td colspan=\"26\"> <table width=\"98%\" height=\"40\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\"> <tr><td align=\"center\" class=\"pS\" valign=\"middle\"><strong> " + year + "年度工资分析表 </strong></td></tr></table></td></tr>");
        sb.Append(" \r\t <tr ><td align=\"center\" class=\"td1\" style=\"height: 25px\">部门</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">1月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">2月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">3月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">4月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">5月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">6月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">7月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">8月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">9月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">10月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">11月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">12月</td><td align=\"center\" class=\"td4\" style=\"height: 25px\">" + "&nbsp;" + "</td></tr>");
        sb.Append("\r\t  <tr><td align=\"center\" class=\"td1\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">金额</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">金额</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">金额</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">金额</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">金额</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">金额</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">金额</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">金额</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">金额</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">金额</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">金额</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">金额</td><td align=\"center\" class=\"td4\" style=\"height: 25px\">月平均金额</td></tr>");
        sb.Append("\r\t </thead>");

        sb.Append(" <tbody bgcolor=\"white\" id=\"show\"");


      
        if (!string.IsNullOrEmpty(deptIDGet))
        {
            decimal sum = 0;

            string DeptName = SalaryStandardBus.GetNameByDeptID(deptIDGet);
            if (string.IsNullOrEmpty(DeptName))
            {
                DeptName = "&nbsp;";
            }
            sb.Append("\r\t  <tr><td align=\"center\" class=td1 style=\"height: 25px\">" + DeptName + "</td>");
            for (int month = 1; month < 13; month++)
            {
                string monthTemp;
                if (month < 10)
                {
                    monthTemp = "0" + month.ToString();
                }
                else
                {
                    monthTemp = month.ToString();
                }

                DataTable dtNew = SalaryStandardBus.GetMonthlyInfo(year, deptIDGet, monthTemp);
                if (dtNew.Rows.Count > 0)
                {
                    string count = dtNew.Rows[0]["CompanyCD"] == null ? "" : dtNew.Rows[0]["CompanyCD"].ToString();
                    string UnitPrice = dtNew.Rows[0]["UnitPrice"] == null ? "" : dtNew.Rows[0]["UnitPrice"].ToString();
                    sb.Append("\r\t  <td align=\"center\" class=td1 style=\"height: 25px\">" + count + "</td>" + "<td align=\"center\" class=td1 style=\"height: 25px\">" + UnitPrice + "</td>");

                    sum = sum + Convert.ToDecimal(UnitPrice);
                }
                else
                {
                    sb.Append("\r\t  <td align=\"center\" class=td1 style=\"height: 25px\">" + "&nbsp;" + "</td>" + "<td align=\"center\" class=td1 style=\"height: 25px\">" + "&nbsp;" + "</td>");
                }
            }
            decimal dd = Math.Round(sum / 12, 4);

            sb.Append("\r\t  <td align=\"center\" class=td4 style=\"height: 25px\">" + Convert.ToString(dd) + "</td>");
            sb.Append("\r\t  </tr>");
        }
        else
        {


            for (int a = 0; a < dt.Rows.Count; a++)
            {
                decimal sum = 0;
                string deptID = dt.Rows[a]["DeptID"] == null ? "" : dt.Rows[a]["DeptID"].ToString();
                string DeptName = SalaryStandardBus.GetNameByDeptID(deptID);
                if (string.IsNullOrEmpty(DeptName))
                {
                    DeptName = "&nbsp;";
                }
                sb.Append("\r\t  <tr><td align=\"center\" class=td1 style=\"height: 25px\">" + DeptName + "</td>");
                for (int month = 1; month < 13; month++)
                {
                    string monthTemp;
                    if (month < 10)
                    {
                        monthTemp = "0" + month.ToString();
                    }
                    else
                    {
                        monthTemp = month.ToString();
                    }

                    DataTable dtNew = SalaryStandardBus.GetMonthlyInfo(year, deptID, monthTemp);
                    if (dtNew.Rows.Count > 0)
                    {
                        string count = dtNew.Rows[0]["CompanyCD"] == null ? "" : dtNew.Rows[0]["CompanyCD"].ToString();
                        string UnitPrice = dtNew.Rows[0]["UnitPrice"] == null ? "" : dtNew.Rows[0]["UnitPrice"].ToString();
                        sb.Append("\r\t  <td align=\"center\" class=td1 style=\"height: 25px\">" + count + "</td>" + "<td align=\"center\" class=td1 style=\"height: 25px\">" + UnitPrice + "</td>");

                        sum = sum + Convert.ToDecimal(UnitPrice);
                    }
                    else
                    {
                        sb.Append("\r\t  <td align=\"center\" class=td1 style=\"height: 25px\">" + "&nbsp;" + "</td>" + "<td align=\"center\" class=td1 style=\"height: 25px\">" + "&nbsp;" + "</td>");
                    }
                }
                decimal dd = Math.Round(sum / 12, 4);

                sb.Append("\r\t  <td align=\"center\" class=td4 style=\"height: 25px\">" + Convert.ToString(dd) + "</td>");
                sb.Append("\r\t  </tr>");
            }
        }

        sb.Append("\r\t <tr><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td5\" style=\"height: 25px\">" + "&nbsp;" + "</td></tr>");











        sb.Append("\r\t    </tbody>");
        sb.Append("\r\t   <tfoot  class='noprint2' style=\"display:table-footer-group;font-weight:bold\"> <tr><td align=\"left\"   class=\"noprint2\" colspan=\"8\"  > 制表人：&nbsp;" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName + "</td>  <td align=\"right\" style=\"height: 23px\" class=\"noprint2\" colspan=\"22\"  >打印日期：" + DateTime.Now.ToString("yyyy-MM-dd") + "&nbsp;</td>   </tr></tfoot>");

        sb.Append("\r\t   </table>");
        sb.Append("\r\t    </td>  </tr>  </table>");
        return sb.ToString();






    }
    protected void btnOutWord_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        string deptIDGet = Request.QueryString["DeptID"] == null ? "" : Request.QueryString["DeptID"].ToString();
        string year = Request.QueryString["year"] == null ? "" : Request.QueryString["year"].ToString();


        string titleNamee = year +"年度工资统计表";
        HttpResponse resp;
        resp = Page.Response;
        resp.ContentEncoding = System.Text.Encoding.Default;
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];


        DataTable dtData = new DataTable();
        dtData.Columns.Add("deptName");//部门
        dtData.Columns.Add("MonthCount1");//人数
        dtData.Columns.Add("MonthMoney1");//金额
        dtData.Columns.Add("MonthCount2");//人数
        dtData.Columns.Add("MonthMoney2");//金额
        dtData.Columns.Add("MonthCount3");//人数
        dtData.Columns.Add("MonthMoney3");//金额
        dtData.Columns.Add("MonthCount4");//人数
        dtData.Columns.Add("MonthMoney4");//金额
        dtData.Columns.Add("MonthCount5");//人数
        dtData.Columns.Add("MonthMoney5");//金额
        dtData.Columns.Add("MonthCount6");//人数
        dtData.Columns.Add("MonthMoney6");//金额
        dtData.Columns.Add("MonthCount7");//人数
        dtData.Columns.Add("MonthMoney7");//金额
        dtData.Columns.Add("MonthCount8");//人数
        dtData.Columns.Add("MonthMoney8");//金额
        dtData.Columns.Add("MonthCount9");//人数
        dtData.Columns.Add("MonthMoney9");//金额
        dtData.Columns.Add("MonthCount10");//人数
        dtData.Columns.Add("MonthMoney10");//金额
        dtData.Columns.Add("MonthCount11");//人数
        dtData.Columns.Add("MonthMoney11");//金额
        dtData.Columns.Add("MonthCount12");//人数
        dtData.Columns.Add("MonthMoney12");//金额
        dtData.Columns.Add("summary");//月平均金额


        if (!string.IsNullOrEmpty(deptIDGet))
        {
            decimal sum = 0;
            DataRow newRow = dtData.NewRow();



            string DeptName = SalaryStandardBus.GetNameByDeptID(deptIDGet);
            if (string.IsNullOrEmpty(DeptName))
            {
                DeptName = " ";
            }

            newRow["deptName"] = DeptName;
            for (int month = 1; month < 13; month++)
            {
                string monthTemp;
                if (month < 10)
                {
                    monthTemp = "0" + month.ToString();
                }
                else
                {
                    monthTemp = month.ToString();
                }

                DataTable dtNew = SalaryStandardBus.GetMonthlyInfo(year, deptIDGet, monthTemp);
                if (dtNew.Rows.Count > 0)
                {
                    newRow["MonthCount" + month.ToString()] = dtNew.Rows[0]["CompanyCD"] == null ? "" : dtNew.Rows[0]["CompanyCD"].ToString();
                    string UnitPrice = dtNew.Rows[0]["UnitPrice"] == null ? "" : dtNew.Rows[0]["UnitPrice"].ToString();
                    newRow["MonthMoney" + month.ToString()] = UnitPrice;

                    sum = sum + Convert.ToDecimal(UnitPrice);
                }
                else
                {
                    newRow["MonthCount" + month.ToString()] = " ";
                    newRow["MonthMoney" + month.ToString()] = " ";
                    sum = sum + 0;
                }
            }
            decimal dd = Math.Round(sum / 12, 4);
            newRow["summary"] = Convert.ToString(dd);
            dtData.Rows.Add(newRow);
        }
        else
        {
            DataTable dt = SalaryStandardBus.GetDeptInfo();
            for (int a = 0; a < dt.Rows.Count; a++)
            {
                decimal sum = 0;
                DataRow newRow = dtData.NewRow();


                string deptID = dt.Rows[a]["DeptID"] == null ? "" : dt.Rows[a]["DeptID"].ToString();
                string DeptName = SalaryStandardBus.GetNameByDeptID(deptID);
                if (string.IsNullOrEmpty(DeptName))
                {
                    DeptName = " ";
                }

                newRow["deptName"] = DeptName;
                for (int month = 1; month < 13; month++)
                {
                    string monthTemp;
                    if (month < 10)
                    {
                        monthTemp = "0" + month.ToString();
                    }
                    else
                    {
                        monthTemp = month.ToString();
                    }

                    DataTable dtNew = SalaryStandardBus.GetMonthlyInfo(year, deptID, monthTemp);
                    if (dtNew.Rows.Count > 0)
                    {
                        newRow["MonthCount" + month.ToString()] = dtNew.Rows[0]["CompanyCD"] == null ? "" : dtNew.Rows[0]["CompanyCD"].ToString();
                        string UnitPrice = dtNew.Rows[0]["UnitPrice"] == null ? "" : dtNew.Rows[0]["UnitPrice"].ToString();
                        newRow["MonthMoney" + month.ToString()] = UnitPrice;

                        sum = sum + Convert.ToDecimal(UnitPrice);
                    }
                    else
                    {
                        newRow["MonthCount" + month.ToString()] = " ";
                        newRow["MonthMoney" + month.ToString()] = " ";
                        sum = sum + 0;
                    }
                }
                decimal dd = Math.Round(sum / 12, 4);
                newRow["summary"] = Convert.ToString(dd);
                dtData.Rows.Add(newRow);
            }
        }



        string OutFile = titleNamee + ".xls";
        resp.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(OutFile)));
        string ls_item = "";
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += titleNamee + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(13);
        resp.Write(ls_item);

        ls_item = "";
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(13);
        resp.Write(ls_item);

        ls_item = "";
        ls_item += "  " + Convert.ToChar(9);
        ls_item += "一" + Convert.ToChar(9);
        ls_item += "月" + Convert.ToChar(9);
        ls_item += "二" + Convert.ToChar(9);
        ls_item += "月" + Convert.ToChar(9);
        ls_item += "三" + Convert.ToChar(9);
        ls_item += "月" + Convert.ToChar(9);
        ls_item += "四" + Convert.ToChar(9);
        ls_item += "月" + Convert.ToChar(9);
        ls_item += "五" + Convert.ToChar(9);
        ls_item += "月" + Convert.ToChar(9);
        ls_item += "六" + Convert.ToChar(9);
        ls_item += "月" + Convert.ToChar(9);
        ls_item += "七" + Convert.ToChar(9);
        ls_item += "月" + Convert.ToChar(9);
        ls_item += "八" + Convert.ToChar(9);
        ls_item += "月" + Convert.ToChar(9);
        ls_item += "九" + Convert.ToChar(9);
        ls_item += "月" + Convert.ToChar(9);
        ls_item += "十" + Convert.ToChar(9);
        ls_item += "月" + Convert.ToChar(9);
        ls_item += "十一" + Convert.ToChar(9);
        ls_item += "月" + Convert.ToChar(9);
        ls_item += "十二" + Convert.ToChar(9);
        ls_item += "月" + Convert.ToChar(9);
        ls_item += " " + Convert.ToChar(13);
        resp.Write(ls_item);

        ls_item = "";
        int i = 0;
        ls_item += "部门  " + Convert.ToChar(9);
        ls_item += "人数" + Convert.ToChar(9);
        ls_item += "金额" + Convert.ToChar(9);
        ls_item += "人数" + Convert.ToChar(9);
        ls_item += "金额" + Convert.ToChar(9);
        ls_item += "人数" + Convert.ToChar(9);
        ls_item += "金额" + Convert.ToChar(9);
        ls_item += "人数" + Convert.ToChar(9);
        ls_item += "金额" + Convert.ToChar(9);
        ls_item += "人数" + Convert.ToChar(9);
        ls_item += "金额" + Convert.ToChar(9);
        ls_item += "人数" + Convert.ToChar(9);
        ls_item += "金额" + Convert.ToChar(9);
        ls_item += "人数" + Convert.ToChar(9);
        ls_item += "金额" + Convert.ToChar(9);
        ls_item += "人数" + Convert.ToChar(9);
        ls_item += "金额" + Convert.ToChar(9);
        ls_item += "人数" + Convert.ToChar(9);
        ls_item += "金额" + Convert.ToChar(9);
        ls_item += "人数" + Convert.ToChar(9);
        ls_item += "金额" + Convert.ToChar(9);
        ls_item += "人数" + Convert.ToChar(9);
        ls_item += "金额" + Convert.ToChar(9);
        ls_item += "人数" + Convert.ToChar(9);
        ls_item += "金额" + Convert.ToChar(9);
        ls_item += "月平均金额" + Convert.ToChar(9);
        ls_item += " " + Convert.ToChar(13);
        resp.Write(ls_item);


         
        ls_item = "";
        for (i = 0; i < dtData.Rows.Count; i++)
        {
            ls_item += dtData.Rows[i]["deptName"].ToString() + Convert.ToChar(9);

            ls_item += dtData.Rows[i]["MonthCount1"].ToString() + Convert.ToChar(9);
            ls_item += dtData.Rows[i]["MonthMoney1"].ToString() + Convert.ToChar(9);

            ls_item += dtData.Rows[i]["MonthCount2"].ToString() + Convert.ToChar(9);
            ls_item += dtData.Rows[i]["MonthMoney2"].ToString() + Convert.ToChar(9);

            ls_item += dtData.Rows[i]["MonthCount3"].ToString() + Convert.ToChar(9);
            ls_item += dtData.Rows[i]["MonthMoney3"].ToString() + Convert.ToChar(9);

            ls_item += dtData.Rows[i]["MonthCount4"].ToString() + Convert.ToChar(9);
            ls_item += dtData.Rows[i]["MonthMoney4"].ToString() + Convert.ToChar(9);

            ls_item += dtData.Rows[i]["MonthCount5"].ToString() + Convert.ToChar(9);
            ls_item += dtData.Rows[i]["MonthMoney5"].ToString() + Convert.ToChar(9);

            ls_item += dtData.Rows[i]["MonthCount6"].ToString() + Convert.ToChar(9);
            ls_item += dtData.Rows[i]["MonthMoney6"].ToString() + Convert.ToChar(9);

            ls_item += dtData.Rows[i]["MonthCount7"].ToString() + Convert.ToChar(9);
            ls_item += dtData.Rows[i]["MonthMoney7"].ToString() + Convert.ToChar(9);

            ls_item += dtData.Rows[i]["MonthCount8"].ToString() + Convert.ToChar(9);
            ls_item += dtData.Rows[i]["MonthMoney8"].ToString() + Convert.ToChar(9);

            ls_item += dtData.Rows[i]["MonthCount9"].ToString() + Convert.ToChar(9);
            ls_item += dtData.Rows[i]["MonthMoney9"].ToString() + Convert.ToChar(9);

            ls_item += dtData.Rows[i]["MonthCount10"].ToString() + Convert.ToChar(9);
            ls_item += dtData.Rows[i]["MonthMoney10"].ToString() + Convert.ToChar(9);

            ls_item += dtData.Rows[i]["MonthCount11"].ToString() + Convert.ToChar(9);
            ls_item += dtData.Rows[i]["MonthMoney11"].ToString() + Convert.ToChar(9);

            ls_item += dtData.Rows[i]["MonthCount12"].ToString() + Convert.ToChar(9);
            ls_item += dtData.Rows[i]["MonthMoney12"].ToString() + Convert.ToChar(9);

            ls_item += dtData.Rows[i]["summary"].ToString() + Convert.ToChar(13);

            resp.Write(ls_item);
            ls_item = "";
        }


        ls_item = "";
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(13);
        resp.Write(ls_item);

        ls_item = "";
        ls_item += "制表：" + Convert.ToChar(9);
        ls_item += ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "打印日期：" + Convert.ToChar(9);
        ls_item += DateTime.Now.ToString("yyyy-MM-dd") + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(13);
        resp.Write(ls_item);
        resp.End();
    }
}

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

//using System;
//using System.Data;
//using XBase.Common;
//using XBase.Business.Common;
//using System.Web.UI.WebControls;
//using XBase.Model.Office.HumanManager;
//using XBase.Business.Office.HumanManager;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
//using System.Text;
//using System.Collections.Generic;
//using System.Collections;
public partial class Pages_Office_HumanManager_SalaryYearSummary : BasePage 
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
        //Label1.Text =getSalaryInfoByDept();
      ///search .InnerHtml =  
    }
    /// <summary>
    /// 绑定部门
    /// </summary>
    //public void DataBindToDept()
    //{
    //    DataTable dt = PerformanceQueryBus.SearchDeptInfo();

    //    ddlDeptName.DataSource = dt;
    //    ddlDeptName.DataTextField = "DeptName";
    //    ddlDeptName.DataValueField = "ID";
    //    ddlDeptName.DataBind();
    //    ddlDeptName.Items.Add(new System.Web.UI.WebControls.ListItem("--请选择--", "0"));
    //    ddlDeptName.SelectedValue = "0";
    //}
    //private void Search()
    //{
    //    //SalaryStandardModel searchModel = new SalaryStandardModel();
    //    ////设置查询条件
    //    ////岗位

    //    //if (ddlDeptName.SelectedValue != "0")
    //    //    searchModel.QuarterID = ddlDeptName.SelectedValue;
    //    //UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
    //    ////查询数据
    //    //DataTable dtNewTable = new Table();
    //    //DataTable dtData = new DataTable();
    //    //dtData.Columns.Add("Remark");
    //    //dtData.Columns.Add("itemNo");
    //    //dtData.Columns.Add("CompanyCD");
    //    //dtData.Columns.Add("UnitPrice");
    //    //for (int i = 0; i < dtNewTable.Rows.Count; i++)
    //    //{
    //    //    DataRow newRow = dtData.NewRow();
    //    //    newRow["Remark"] = getDeptName(dtNewTable.Rows[i]["Remark"] == null ? "" : dtNewTable.Rows[i]["Remark"].ToString());
    //    //    newRow["itemNo"] = dtNewTable.Rows[i]["itemNo"] == null ? "" : dtNewTable.Rows[i]["itemNo"].ToString();
    //    //    newRow["CompanyCD"] = dtNewTable.Rows[i]["CompanyCD"] == null ? "" : dtNewTable.Rows[i]["CompanyCD"].ToString();
    //    //    newRow["UnitPrice"] = dtNewTable.Rows[i]["UnitPrice"] == null ? "" : dtNewTable.Rows[i]["UnitPrice"].ToString();
    //    //    dtData.Rows.Add(newRow);
    //    //}


    //    //ReportDocument oRpt = new ReportDocument();
    //    //CrystalReportSource1.ReportDocument.Load(Server.MapPath(@"~/OperatingModel/CrystalReport/HumanManager/SalarySummeryReport.rpt"));
    //    //// SetDatabaseLogon 拉模式中必须用这个方法来设置登录信息，参数一：用户名；参数二：密码；参数三：服务器；参数四：数据库名
    //    //CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.SalaryReportSummary"));
    //    ////查询数据
    //    //CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["Creator"].Text = "\"" + "制表人:" + userInfo.EmployeeName + "\"";
    //    //CrystalReportSource1.ReportDocument.SetDataSource(dtData);
    //    //CrystalReportSource1.DataBind();
    //    //// CrystalReportViewer1是水晶报表浏览器，下面是给该浏览器赋上对像
    //    //CrystalReportViewer1.ReportSource = CrystalReportSource1;
    //    //CrystalReportViewer1.DataBind();


    //}
    //protected string getDeptName(string DeptId)
    //{

    //    return SalaryStandardBus.GetNameByDeptID(DeptId);


    //}
    //protected void imgSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    //{
    //    Search();
    //}
    ////public string  getSalaryInfoByDept()
    ////{
    ////  DataTable dt=  SalaryStandardBus.GetDeptInfo();
    ////  StringBuilder sb = new StringBuilder();
    ////  sb.Append("\r\t  <table width=\"95%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"  valign=\"top\" align=\"center\"><tr align=\"center\"><td width=\"95%\" align=\"center\" valign=\"top\">");

    ////  sb.Append("\r\t  <table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
    ////  sb.Append("\r\t   <thead  style=\"display:table-header-group;font-weight:bold\">");
    ////  sb.Append("\r\t    <tr><td colspan=\"26\"> <table width=\"98%\" height=\"40\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\"> <tr><td align=\"center\" class=\"pS\" valign=\"middle\"><strong><u>年度工资统计表</u></strong></td></tr></table></td></tr>");
    ////  sb.Append(" \r\t <tr ><td align=\"center\" class=\"td1\" style=\"height: 25px\">部门</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">1月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">2月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">3月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">4月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">5月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">6月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">7月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">8月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">9月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">10月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">11月</td><td colspan='2' align=\"center\" class=\"td1\" style=\"height: 25px\">12月</td><td align=\"center\" class=\"td4\" style=\"height: 25px\">" + "&nbsp;" + "</td></tr>");
    ////  sb.Append("\r\t  <tr><td align=\"center\" class=\"td1\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">合计</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">合计</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">合计</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">合计</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">合计</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">合计</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">合计</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">合计</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">合计</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">合计</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">合计</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">人数</td><td align=\"center\" class=\"td1\" style=\"height: 25px\">合计</td><td align=\"center\" class=\"td4\" style=\"height: 25px\">平均额</td></tr>");
    ////  sb.Append("\r\t </thead>");

    ////  sb.Append(" <tbody bgcolor=\"white\" id=\"show\"");


    ////  string year = "2009";
    ////  decimal sum = 0;
    ////    for (int a=0;a<dt.Rows.Count ;a++)
    ////    {   
    ////        string deptID=dt.Rows[a]["DeptID"] == null ? "" : dt.Rows[a]["DeptID"].ToString();
    ////        string DeptName = SalaryStandardBus.GetNameByDeptID(deptID);
    ////        if (string.IsNullOrEmpty(DeptName))
    ////        {
    ////            DeptName = "&nbsp;";
    ////        }
    ////        sb.Append("\r\t  <tr><td align=\"center\" class=td1 style=\"height: 25px\">" + DeptName + "</td>");
    ////    for (int month = 1; month < 13; month++)
    ////    {
    ////        string monthTemp;
    ////        if (month < 10)
    ////        {
    ////            monthTemp = "0" + month.ToString ();
    ////        }
    ////        else
    ////        {
    ////            monthTemp =  month.ToString();
    ////        }
         
    ////        DataTable dtNew = SalaryStandardBus.GetMonthlyInfo(year,deptID  , monthTemp);
    ////        if (dtNew.Rows.Count > 0)
    ////        {
    ////            string count = dtNew.Rows[0]["CompanyCD"] == null ? "" : dtNew.Rows[0]["CompanyCD"].ToString();
    ////            string UnitPrice=dtNew.Rows[0]["UnitPrice"] == null ? "" : dtNew.Rows[0]["UnitPrice"].ToString() ;
    ////            sb.Append("\r\t  <td align=\"center\" class=td1 style=\"height: 25px\">" + count + "</td>" + "<td align=\"center\" class=td1 style=\"height: 25px\">" + UnitPrice + "</td>");

    ////            sum = sum + Convert.ToDecimal(UnitPrice);
    ////        }
    ////        else
    ////        {
    ////            sb.Append("\r\t  <td align=\"center\" class=td1 style=\"height: 25px\">" + "&nbsp;" + "</td>" + "<td align=\"center\" class=td1 style=\"height: 25px\">" + "&nbsp;" + "</td>");
    ////        }
    ////    }
    ////    decimal dd = Math.Round(sum / 12, 4);

    ////    sb.Append("\r\t  <td align=\"center\" class=td4 style=\"height: 25px\">" + Convert.ToString(dd) + "</td>");
    ////    sb.Append("\r\t  </tr>");
    ////    }

    ////    sb.Append("\r\t <tr><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td3\" style=\"height: 25px\">" + "&nbsp;" + "</td><td align=\"center\" class=\"td5\" style=\"height: 25px\">" + "&nbsp;" + "</td></tr>");











    ////    sb.Append("\r\t    </tbody>");
    ////    sb.Append("\r\t   <tfoot style=\"display:table-footer-group;font-weight:bold\"> <tr><td align=\"left\"   class=\"td\" colspan=\"5\"  > 制表：&nbsp;" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName + "</td>  <td align=\"right\" style=\"height: 23px\" class=\"td\" colspan=\"22\"  >打印日期：" + DateTime.Now.ToString("yyyy-MM-dd") + "&nbsp;</td>   </tr></tfoot>");

    ////    sb.Append("\r\t   </table>");
    ////    sb.Append("\r\t    </td>  </tr>  </table>");
    ////      return sb.ToString ();
       





    ////}
    //protected void btnOutWord_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    //{
    //    string titleNamee = "考核明细表";
    //    HttpResponse resp;
    //    resp = Page.Response;
    //    resp.ContentEncoding = System.Text.Encoding.Default;
    //    UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
    //    PerformanceTaskModel searchModel = new PerformanceTaskModel();
    //    if (Request.QueryString["TaskFlag"] != "0")
    //        searchModel.TaskFlag = Request.QueryString["TaskFlag"];//考核期间类型
    //    if (!string.IsNullOrEmpty(Request.QueryString["TaskNo"]))
    //        searchModel.TaskNo = Request.QueryString["TaskNo"];//考核任务编号
    //    if (Request.QueryString["PerTypeID"] != "0")
    //        searchModel.CompleteDate = Request.QueryString["PerTypeID"];//考核类型
    //    if (Request.QueryString["TaskFlag"] != "4" && Request.QueryString["TaskFlag"] != "5")
    //    {
    //        if (Request.QueryString["TaskNum"] != "0")
    //            searchModel.TaskNum = Request.QueryString["TaskNum"];//考核期间 
    //    }
    //    if (Request.QueryString["LevelType"] != "0")
    //        searchModel.Summaryer = Request.QueryString["LevelType"];//考核等级
    //    if (Request.QueryString["AdviceType"] != "0")
    //        searchModel.Title = Request.QueryString["AdviceType"];//考核建议
    //    if (Request.QueryString["TaskDate"] != "0")
    //        searchModel.TaskDate = Request.QueryString["TaskDate"];//考核建议

    //    if (!string.IsNullOrEmpty(Request.QueryString["EmployeeID"]))
    //        searchModel.EditFlag = Request.QueryString["EmployeeID"];//被考核人
    //    searchModel.CompanyCD = userInfo.CompanyCD;
    //    //查询数据
    //    DataTable dt = PerformanceQueryBus.SearchScoreInfo(searchModel);
    //    string OutFile = titleNamee + ".xls";
    //    resp.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(OutFile)));
    //    string ls_item = "";
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += titleNamee + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(13);
    //    resp.Write(ls_item);

    //    ls_item = "";
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(13);
    //    resp.Write(ls_item);

    //    ls_item = "";
    //    int i = 0;
    //    ls_item += "部门" + Convert.ToChar(9);
    //    ls_item += "人员" + Convert.ToChar(9);
    //    ls_item += "考核类型" + Convert.ToChar(9);
    //    ls_item += "考核总得分" + Convert.ToChar(9);
    //    ls_item += "累计扣分" + Convert.ToChar(9);
    //    ls_item += "累计加分" + Convert.ToChar(9);
    //    ls_item += "实际得分" + Convert.ToChar(9);
    //    ls_item += "考核等级" + Convert.ToChar(9);
    //    ls_item += "考核建议" + Convert.ToChar(13);
    //    resp.Write(ls_item);

    //    ls_item = "";
    //    for (i = 0; i < dt.Rows.Count; i++)
    //    {

    //        ls_item += dt.Rows[i]["DeptName"].ToString() + Convert.ToChar(9);
    //        ls_item += dt.Rows[i]["passEmployeeName"].ToString() + Convert.ToChar(9);
    //        ls_item += dt.Rows[i]["TypeName"].ToString() + Convert.ToChar(9);
    //        ls_item += dt.Rows[i]["TotalScore"].ToString() + Convert.ToChar(9);
    //        ls_item += dt.Rows[i]["KillScore"].ToString() + Convert.ToChar(9);
    //        ls_item += dt.Rows[i]["AddScore"].ToString() + Convert.ToChar(9);
    //        ls_item += dt.Rows[i]["RealScore"].ToString() + Convert.ToChar(9);
    //        ls_item += dt.Rows[i]["AdviceType"].ToString() + Convert.ToChar(9);
    //        ls_item += dt.Rows[i]["LevelType"].ToString() + Convert.ToChar(13);
    //        resp.Write(ls_item);
    //        ls_item = "";
    //    }


    //    ls_item = "";
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(13);
    //    resp.Write(ls_item);

    //    ls_item = "";
    //    ls_item += "制表：" + Convert.ToChar(9);
    //    ls_item += ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "打印日期：" + Convert.ToChar(9);
    //    ls_item += DateTime.Now.ToString("yyyy-MM-dd") + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(9);
    //    ls_item += "" + Convert.ToChar(13);
    //    resp.Write(ls_item);
    //    resp.End();
    //}
}

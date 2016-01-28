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


public partial class Pages_Office_HumanManager_PrintPerformanceQuery :BasePage 
{
    protected string titleName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!this.Page.IsPostBack)
        //{

        //DataTable CurrTypeDT = CurrTypeSettingBus.GetMasterCurrency();
        //if (CurrTypeDT.Rows.Count > 0)
        //{
        //    this.HiddenCurryTypeID.Value = CurrTypeDT.Rows[0]["ID"].ToString();
        //}

        //      titleName = Request.QueryString["type"].ToString() == "1" ? "应收帐款汇总表" : "应付帐款汇总表";
        this.Label1.Text = this.GetSource();
        this.HiddenCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;

    }

    protected string GetSource()
    {
        int defaultCount = Convert.ToInt32(this.linesPerPage.Value);
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        if (Request.QueryString["TaskFlag"] != "0")
            searchModel.TaskFlag = Request.QueryString["TaskFlag"];//考核期间类型
        if (!string.IsNullOrEmpty(Request.QueryString["TaskNo"]))
            searchModel.TaskNo = Request.QueryString["TaskNo"];//考核任务编号
        if (Request.QueryString["PerTypeID"] != "0")
            searchModel.CompleteDate = Request.QueryString["PerTypeID"];//考核类型
        if (Request.QueryString["TaskFlag"] != "4" && Request.QueryString["TaskFlag"] != "5")
        {
            if (Request.QueryString["TaskNum"] != "0")
                searchModel.TaskNum = Request.QueryString["TaskNum"];//考核期间 
        }
        if (Request.QueryString["LevelType"] != "0")
            searchModel.Summaryer = Request.QueryString["LevelType"];//考核等级
        if (Request.QueryString["AdviceType"] != "0")
            searchModel.Title = Request.QueryString["AdviceType"];//考核建议
        if (Request.QueryString["TaskDate"] != "0")
            searchModel.TaskDate = Request.QueryString["TaskDate"];//考核建议

        if (!string.IsNullOrEmpty(Request.QueryString["EmployeeID"]))
            searchModel.EditFlag = Request.QueryString["EmployeeID"];//被考核人
        searchModel.CompanyCD = userInfo.CompanyCD;
        //查询数据
        DataTable dt = PerformanceQueryBus.SearchScoreInfo(searchModel);


        StringBuilder htmlStr = new StringBuilder();
        htmlStr.Append("\r\t  <table width=\"95%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"  valign=\"top\" align=\"center\"><tr align=\"center\"><td width=\"95%\" align=\"center\" valign=\"top\"><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
        //表头开始（在打印预览中每页都有）****************************************************************
        htmlStr.Append("\r\t   <thead  style=\"display:table-header-group;font-weight:bold\">");
        htmlStr.Append("\r\t   <tr><td colspan=\"15\">");


        htmlStr.Append("\r\t  <table width=\"98%\" height=\"40\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\">");
        htmlStr.Append("\r\t  <tr>");
        htmlStr.Append("\r\t  <td align=\"center\" class=\"pS\" valign=\"middle\"><strong> " + "考核查询" + " </strong></td>");
        htmlStr.Append("\r\t  </tr></table>");

        //htmlStr.Append("\r\t  <table width=\"98%\" height=\"25px\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

        //htmlStr.Append("\r\t  <tr><td align=\"left\" style=\"height: 25px;\" class=\"td\" colspan=\"2\">部门");
        //htmlStr.Append("\r\t  &nbsp;</td></a>");

        //htmlStr.Append("\r\t  <tr><td align=\"left\" style=\"height: 25px;\" class=\"td\" colspan=\"2\">");
        //htmlStr.Append("\r\t  &nbsp;</td><td align=\"right\" style=\"height: 25px;\" class=\"td\" colspan=\"4\">币种：" + curryName + "");
        //htmlStr.Append("\r\t  &nbsp;</td>");
        //htmlStr.Append("\r\t  </tr></table>");

        //htmlStr.Append("\r\t  </td></tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">被考核人</td>");
        htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">任务主题</td>");
        htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">考核期间类型</td>");
        htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">考核期间</td>");
        htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">考核模板</td>");
        htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">考核总得分</td>");
        htmlStr.Append("\r\t     <td width=\"4%\" align=\"center\" class=\"td1\" style=\"height: 25px\">累计扣分</td>");
        htmlStr.Append("\r\t     <td width=\"4%\" align=\"center\" class=\"td1\" style=\"height: 25px\">累计加分</td>");
        htmlStr.Append("\r\t     <td width=\"4%\" align=\"center\" class=\"td1\" style=\"height: 25px\">实际得分</td>");
        htmlStr.Append("\r\t     <td width=\"4%\" align=\"center\" class=\"td4\" style=\"height: 25px\">考核等级</td>");
        htmlStr.Append("\r\t     <td width=\"4%\" align=\"center\" class=\"td4\" style=\"height: 25px\">考核建议</td>");
        htmlStr.Append("\r\t     <td width=\"3%\" align=\"center\" class=\"td4\" style=\"height: 25px\">总评人</td>");
        htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=\"td4\" style=\"height: 25px\">总评时间</td>");
        htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=\"td4\" style=\"height: 25px\">确认时间</td>");
        htmlStr.Append("\r\t     </tr></thead>");
        //表头结束（在打印预览中每页都有）****************************************************************
        //循环列表开始（<input id="linesPerPage" type="hidden"  value="4" /> value值控制每输出value行打印预览自动分页）******************************
        htmlStr.Append("\r\t     <tbody bgcolor=\"white\" id=\"show\"");
        if (dt.Rows.Count >= defaultCount)
        {
            int count = dt.Rows.Count / defaultCount;
            int count1 = dt.Rows.Count % defaultCount;
            int falcount = count * defaultCount;
            if (count1 != 0)
            {
                falcount = (count + 1) * defaultCount;
            }



            for (int i = 0; i < falcount; i++)
            {
                //控制表格的样式
                string className = (i + 1) % defaultCount == 0 ? "class=td5" : "class=td4";
                string className1 = (i + 1) % defaultCount == 0 ? "class=td3" : "class=td1";
                if (i < dt.Rows.Count)
                {
                    htmlStr.Append("\r\t     <tr >");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;" + dt.Rows[i]["passEmployeeName"].ToString() + "</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;" + dt.Rows[i]["Title"].ToString() + "</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;" + dt.Rows[i]["TaskFlag"].ToString() + "</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"left\" " + className1 + " style=\"height: 25px\">&nbsp;" + dt.Rows[i]["TaskNum"].ToString() + "</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"left\" " + className1 + " style=\"height: 25px\">&nbsp;" + dt.Rows[i]["templateName"].ToString() + "</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"right\" " + className1 + " style=\"height: 25px\">&nbsp;" + dt.Rows[i]["TotalScore"].ToString() + "</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"right\" " + className1 + " style=\"height: 25px\">&nbsp;" + dt.Rows[i]["KillScore"].ToString() + "</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"right\" " + className1 + " style=\"height: 25px\">&nbsp;" + dt.Rows[i]["AddScore"].ToString() + "</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"right\" " + className1 + " style=\"height: 25px\">&nbsp;" + dt.Rows[i]["RealScore"].ToString() + "</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;" + dt.Rows[i]["LevelType"].ToString() + "</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;" + dt.Rows[i]["AdviceType"].ToString() + "</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;" + dt.Rows[i]["EvaluaterName"].ToString() + "</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;" + dt.Rows[i]["EvaluateDate"].ToString() + "</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;" + dt.Rows[i]["SignDate"].ToString()+ "</td>");
                    htmlStr.Append("\r\t     <td width=\"15%\" align=\"left\" ");

                    htmlStr.Append("\r\t    " + className + "");
                    //htmlStr.Append("\r\t     style=\"height: 25px\">&nbsp;" + dt.Rows[i]["EndAmount"].ToString() + "</td>");
                    htmlStr.Append("\r\t     </tr>");
                }
                else
                {
                    htmlStr.Append("\r\t     <tr >");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" " + className1 + " style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" ");
                    htmlStr.Append("\r\t    " + className + "");
                    htmlStr.Append("\r\t     style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     </tr>");
                }
            }
        }
        else
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                htmlStr.Append("\r\t     <tr >");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td1 style=\"height: 25px\">&nbsp;" + dt.Rows[j]["passEmployeeName"].ToString() + "</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\"class=td1  style=\"height: 25px\">&nbsp;" + dt.Rows[j]["Title"].ToString() + "</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td1 style=\"height: 25px\">&nbsp;" + dt.Rows[j]["TaskFlag"].ToString() + "</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"left\" class=td1 style=\"height: 25px\">&nbsp;" + dt.Rows[j]["TaskNum"].ToString() + "</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"left\" class=td1 style=\"height: 25px\">&nbsp;" + dt.Rows[j]["templateName"].ToString() + "</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"right\" class=td1 style=\"height: 25px\">&nbsp;" + dt.Rows[j]["TotalScore"].ToString() + "</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"right\" class=td1 style=\"height: 25px\">&nbsp;" + dt.Rows[j]["KillScore"].ToString() + "</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"right\" class=td1 style=\"height: 25px\">&nbsp;" + dt.Rows[j]["AddScore"].ToString() + "</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"right\" class=td1 style=\"height: 25px\">&nbsp;" + dt.Rows[j]["RealScore"].ToString() + "</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td1 style=\"height: 25px\">&nbsp;" + dt.Rows[j]["LevelType"].ToString() + "</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td1 style=\"height: 25px\">&nbsp;" + dt.Rows[j]["AdviceType"].ToString() + "</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td1 style=\"height: 25px\">&nbsp;" + dt.Rows[j]["EvaluaterName"].ToString() + "</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td1 style=\"height: 25px\">&nbsp;" + dt.Rows[j]["EvaluateDate"].ToString() + "</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td5 style=\"height: 25px\">&nbsp;" + dt.Rows[j]["SignDate"].ToString() + "</td>");
                //htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" ");

                //htmlStr.Append("\r\t    class=td4");
                //htmlStr.Append("\r\t     style=\"height: 25px\">&nbsp;" + dt.Rows[j]["SignDate"].ToString() + "</td>");
                htmlStr.Append("\r\t     </tr>");
            }
            for (int k = 0; k < defaultCount - dt.Rows.Count; k++)
            {
                htmlStr.Append("\r\t     <tr>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td1 style=\"height: 25px\">&nbsp;</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td1 style=\"height: 25px\">&nbsp;</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td1 style=\"height: 25px\">&nbsp;</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td1 style=\"height: 25px\">&nbsp;</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td1 style=\"height: 25px\">&nbsp;</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td1 style=\"height: 25px\">&nbsp;</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td1 style=\"height: 25px\">&nbsp;</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td4 style=\"height: 25px\">&nbsp;</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td5 style=\"height: 25px\">&nbsp;</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td5 style=\"height: 25px\">&nbsp;</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td5 style=\"height: 25px\">&nbsp;</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td5 style=\"height: 25px\">&nbsp;</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td5 style=\"height: 25px\">&nbsp;</td>");
                htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td5 style=\"height: 25px\">&nbsp;</td>");
                htmlStr.Append("\r\t     </tr>");
                if (k == (defaultCount - dt.Rows.Count - 1))
                {
                    htmlStr.Append("\r\t     <tr>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td3 style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td3 style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td3 style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td3 style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td3 style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td3 style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td3 style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td5 style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td5 style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td5 style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td5 style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td5 style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td5 style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     <td width=\"5%\" align=\"center\" class=td5 style=\"height: 25px\">&nbsp;</td>");
                    htmlStr.Append("\r\t     </tr>");
                }
            }
        }


        //循环列表结束（<input id="linesPerPage" type="hidden"  value="4" /> value值控制每输出value行打印预览自动分页）******************************


        //表尾开始（在打印预览中每页都有）****************************************************************
        htmlStr.Append("\r\t     </tbody>");

        htmlStr.Append("\r\t   <tfoot  class='noprint2'  style=\"display:table-footer-group;font-weight:bold\">");
        htmlStr.Append("\r\t     <tr><td align=\"left\"   class=\"noprint2\" style=\"float:left\">制表:" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName + "</td>");
        htmlStr.Append("\r\t     <td align=\"right\" style=\"height: 23px\" class=\"noprint2\" colspan=\"11\"  >打印日期：" + DateTime.Now.ToString("yyyy-MM-dd") + "");
        htmlStr.Append("\r\t      </td>");
        htmlStr.Append("\r\t     </td></tr></tfoot>");
        //表尾结束（在打印预览中每页都有）****************************************************************


        htmlStr.Append("\r\t     </table></td></tr></table>");
        return htmlStr.ToString();
    }

    protected void btnOutWord_Click(object sender, EventArgs e)
    {
        string titleNamee = "考核查询";
        HttpResponse resp;
        resp = Page.Response;
        resp.ContentEncoding = System.Text.Encoding.Default;
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        if (Request.QueryString["TaskFlag"] != "0")
            searchModel.TaskFlag = Request.QueryString["TaskFlag"];//考核期间类型
        if (!string.IsNullOrEmpty(Request.QueryString["TaskNo"]))
            searchModel.TaskNo = Request.QueryString["TaskNo"];//考核任务编号
        if (Request.QueryString["PerTypeID"] != "0")
            searchModel.CompleteDate = Request.QueryString["PerTypeID"];//考核类型
        if (Request.QueryString["TaskFlag"] != "4" && Request.QueryString["TaskFlag"] != "5")
        {
            if (Request.QueryString["TaskNum"] != "0")
                searchModel.TaskNum = Request.QueryString["TaskNum"];//考核期间 
        }
        if (Request.QueryString["LevelType"] != "0")
            searchModel.Summaryer = Request.QueryString["LevelType"];//考核等级
        if (Request.QueryString["AdviceType"] != "0")
            searchModel.Title = Request.QueryString["AdviceType"];//考核建议
        if (Request.QueryString["TaskDate"] != "0")
            searchModel.TaskDate = Request.QueryString["TaskDate"];//考核建议

        if (!string.IsNullOrEmpty(Request.QueryString["EmployeeID"]))
            searchModel.EditFlag = Request.QueryString["EmployeeID"];//被考核人
        searchModel.CompanyCD = userInfo.CompanyCD;
        //查询数据
        DataTable dt = PerformanceQueryBus.SearchScoreInfo(searchModel);
        string OutFile = titleNamee + ".xls";
        resp.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(OutFile)));
        string ls_item = "";
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
        ls_item += "" + Convert.ToChar(13);
        resp.Write(ls_item);

        ls_item = "";
        int i = 0;
        ls_item += "被考核人" + Convert.ToChar(9);
        ls_item += "任务主题" + Convert.ToChar(9);
        ls_item += "考核期间类型" + Convert.ToChar(9);
        ls_item += "考核期间" + Convert.ToChar(9);
        ls_item += "考核模板" + Convert.ToChar(9);
        ls_item += "考核总得分" + Convert.ToChar(9);
        ls_item += "累计扣分" + Convert.ToChar(9);
        ls_item += "累计加分" + Convert.ToChar(9);
        ls_item += "实际得分" + Convert.ToChar(9);
        ls_item += "考核等级" + Convert.ToChar(9);
        ls_item += "考核建议" + Convert.ToChar(9);
        ls_item += "总评人" + Convert.ToChar(9);
        ls_item += "总评时间" + Convert.ToChar(9);
        ls_item += "确认时间" + Convert.ToChar(13);
        resp.Write(ls_item);

        ls_item = "";
        for (i = 0; i < dt.Rows.Count; i++)
        {

            ls_item += dt.Rows[i]["passEmployeeName"].ToString() + Convert.ToChar(9);
            ls_item += dt.Rows[i]["Title"].ToString() + Convert.ToChar(9);
            ls_item += dt.Rows[i]["TaskFlag"].ToString() + Convert.ToChar(9);
            ls_item += dt.Rows[i]["TaskNum"].ToString() + Convert.ToChar(9);
            ls_item += dt.Rows[i]["templateName"].ToString() + Convert.ToChar(9);
            ls_item += dt.Rows[i]["TotalScore"].ToString() + Convert.ToChar(9);
            ls_item += dt.Rows[i]["KillScore"].ToString() + Convert.ToChar(9);
            ls_item += dt.Rows[i]["AddScore"].ToString() + Convert.ToChar(9);
            ls_item += dt.Rows[i]["RealScore"].ToString() + Convert.ToChar(9);
            ls_item += dt.Rows[i]["LevelType"].ToString() + Convert.ToChar(9);
            ls_item += dt.Rows[i]["AdviceType"].ToString() + Convert.ToChar(9);
            ls_item += dt.Rows[i]["EvaluaterName"].ToString() + Convert.ToChar(9);
            ls_item += dt.Rows[i]["EvaluateDate"].ToString() + Convert.ToChar(9);
            ls_item += dt.Rows[i]["SignDate"].ToString() + Convert.ToChar(13);
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
        ls_item += "" + Convert.ToChar(13);
        resp.Write(ls_item);

        ls_item = "";
        ls_item += "制表人:" + Convert.ToChar(9);
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
        ls_item += "打印日期：" + Convert.ToChar(9);
        ls_item += DateTime.Now.ToString("yyyy-MM-dd") + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(9);
        ls_item += "" + Convert.ToChar(13);
        resp.Write(ls_item);
        resp.End();


    }
}

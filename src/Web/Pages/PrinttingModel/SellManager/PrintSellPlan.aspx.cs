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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using XBase.Business.Office.SellManager;
using XBase.Common;
using System.Text;

public partial class Pages_PrinttingModel_SellManager_PrintSellPlan : System.Web.UI.Page
{
    ReportDocument rd = new ReportDocument();
    private DataTable dtDetail = new DataTable();
    private DataTable dt1 = new DataTable();
    string strTemp1 = "    ";
    string strTemp2 = "        ";
    string strTemp3 = "            ";
    string strTemp4 = "                ";
    string strTemp5 = "                    ";
    string strTemp6 = "                        ";
    string strTemp7 = "                            ";
    string strTemp8 = "                                ";
    string strTemp9 = "                                    ";
    protected void Page_Init(object sender, EventArgs e)
    {
        BindMainInfo();
    }

    /*绑定主表信息*/
    protected void BindMainInfo()
    {

        /*接受参数*/

        string OrderNo = Request.QueryString["no"].ToString();


        /*读取数据*/
        DataTable dt = SellPlanBus.GetRepOrder(OrderNo);


        dt1.Columns.Add("PlanNo");
        DataRow dr = dt1.NewRow();
        dr[0] = "销售计划明细:";
        dt1.Rows.Add(dr);
        LoadDetailQuarter();
        /*绑定RPT*/
        if (dt != null)
        {
            /*加载主报表*/
            rd.Load(Server.MapPath(@"~/PrinttingModel/SellManager/SellPlan.rpt"));
            crViewer.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.SellPlan"));

            /*加载子报表*/
            ReportDocument rdDetail = rd.Subreports["SellPlanDetail.rpt"];
            rdDetail.SetDataSource(dt1);

            //绑定数据
            rd.SetDataSource(dt);
            rd.Refresh();
            this.crViewer.ReportSource = rd;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            rd.SetParameterValue("PrintName", "制表人：" + userInfo.UserName);

        }
    }


    private void LoadDetailQuarter()
    {
        string OrderNo = Request.QueryString["no"].ToString();
        dtDetail = SellPlanBus.GetOrderDetail(OrderNo);
        DataRow[] rows = dtDetail.Select("ParentID = 0");//赛选父节点为0的行

        foreach (DataRow row in rows)//循环每节点里的子节点
        {
            LoadQuarter(row);
        }

    }

    private int nodeQuarter = 1;
    private void LoadQuarter(DataRow p)
    {

        string str = string.Empty;
        DataRow[] rows = dtDetail.Select("ParentID=" + p["id"].ToString());
        string nodeType = nodeQuarter.ToString();
        if (rows.Length == 0)
        {
            nodeType = "-1";
        }


        if (p["IsSummarize"].ToString() == "0")
        {
            str += p["DetailTypeName"].ToString() + "：" + p["DetailName"].ToString() + "  " + "最低目标额(元)：" + p["MinDetailotal"].ToString()
                 + "  " + "目标额(元)：" + p["DetailTotal"].ToString();
        }
        else if (p["IsSummarize"].ToString() == "1")
        {
            str += p["DetailTypeName"].ToString() + "：" + p["DetailName"].ToString() + "  " + "最低目标额(元)：" + p["MinDetailotal"].ToString()
                    + "  " + "目标额(元)：" + p["DetailTotal"].ToString() + "  " + p["AddOrCutText"].ToString()
                    + "  目标达成率：" + p["CompletePercent"].ToString().Trim() + "%";
        }
        switch (nodeQuarter)
        {
            case 1:
                str = strTemp1 + str;
                break;
            case 2:
                str = strTemp2 + str;
                break;
            case 3:
                str = strTemp3 + str;
                break;
            case 4:
                str = strTemp4 + str;
                break;
            case 5:
                str = strTemp5 + str;
                break;
            case 6:
                str = strTemp6 + str;
                break;
            case 7:
                str = strTemp7 + str;
                break;
            case 8:
                str = strTemp8 + str;
                break;
            case 9:
                str = strTemp9 + str;
                break;
            default:
                break;
        }
        DataRow dr = dt1.NewRow();
        dr[0] = str;

        dt1.Rows.Add(dr);

        nodeQuarter++;

        foreach (DataRow row in rows)
        {
            LoadQuarter(row);
        }
        nodeQuarter--;


    }
}

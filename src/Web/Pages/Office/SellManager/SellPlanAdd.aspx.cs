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
using XBase.Common;
using XBase.Business.Office.SellManager;
public partial class Pages_Office_SellManager_SellPlanAdd : BasePage
{
    //小数精度
    private int _selPoint = 2;
    public int SelPoint
    {
        get
        {
            return _selPoint;
        }
        set
        {
            _selPoint = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PlanDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        FlowApply1.BillTypeFlag = ConstUtil.CODING_RULE_SELL;
        FlowApply1.BillTypeCode = ConstUtil.CODING_RULE_SELLPLAN_NO;
        hiddSummarizeDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        hiddSummarizer.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
        SellPlanUC.CodingType = ConstUtil.CODING_RULE_SELL;
        SellPlanUC.ItemTypeID = ConstUtil.CODING_RULE_SELLPLAN_NO;
        Summarizer.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
        SummarizeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        ////临时注释
        Creator.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
        ////临时注释
        ModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();

        CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        ModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        for (int i = -2; i < 6; i++)
        {
            ListItem item = new ListItem(DateTime.Now.AddYears(i).Year.ToString(), DateTime.Now.AddYears(i).Year.ToString());
            ddlYear.Items.Add(item);
        }
        ddlYear.SelectedValue = DateTime.Now.Year.ToString();

        int MaxWeek = 0;
        int Week = weekNumber(ref MaxWeek);
        for (int i = 1; i <= MaxWeek; i++)
        {
            ListItem item = new ListItem("第" + i.ToString() + "周", i.ToString());
            ddlWeek.Items.Add(item);
        }
        ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        ddlWeek.SelectedValue = Week.ToString();
        if (DateTime.Now.Month <= 6)
        {
            Half.SelectedValue = "1";
        }
        else
        {
            Half.SelectedValue = "2";
        }

        switch (DateTime.Now.Month)
        {
            case 1:    
            case 2: 
            case 3:
                ddlSeason.SelectedIndex = 0;
                break;
            case 4:
            case 5:
            case 6:
                ddlSeason.SelectedIndex =1;
                break;
            case 7:
            case 8:
            case 9:
                ddlSeason.SelectedIndex = 2;
                break;
            case 10:
            case 11:
            case 12:
                ddlSeason.SelectedIndex = 3;
                break;
            default:
                break;
        }
        // 小数位数
        _selPoint = int.Parse(UserInfo.SelPoint);
    }

    /// <summary>
    /// 计算当前日期为本年多少周 
    /// </summary>
    /// <returns></returns>
    private int weekNumber(ref int MaxWeek)
    {
        //取日期的方法很多（我这里是用控件）就不啰嗦了。
        string firstDateText = DateTime.Now.Year.ToString() + "-1-1";
        DateTime firstDay = Convert.ToDateTime(firstDateText);
        int theday;

        if (firstDay.DayOfWeek == DayOfWeek.Monday)
        {
            theday = 0;
        }
        else if (firstDay.DayOfWeek == DayOfWeek.Tuesday)
        {
            theday = 1;
        }
        else if (firstDay.DayOfWeek == DayOfWeek.Wednesday)
        {
            theday = 2;
        }
        else if (firstDay.DayOfWeek == DayOfWeek.Thursday)
        {
            theday = 3;
        }
        else if (firstDay.DayOfWeek == DayOfWeek.Friday)
        {
            theday = 4;
        }
        else if (firstDay.DayOfWeek == DayOfWeek.Saturday)
        {
            theday = 5;
        }
        else
        {
            theday = 6;
        }

        DateTime nowDate = DateTime.Now;
        int temp = (nowDate.DayOfYear + theday) % 7;
        int weekNum = 0;
        if (temp != 0)
        {
            weekNum = (nowDate.DayOfYear + theday) / 7 + 1;
        }
        else
        {
            weekNum = (nowDate.DayOfYear + theday) / 7;
        }
        string lastDateText = DateTime.Now.Year.ToString() + "-12-31";
        DateTime lastDate = Convert.ToDateTime(lastDateText);
        int temp1 = (nowDate.DayOfYear + theday) % 7;

        if (temp1 != 0)
        {
            MaxWeek = (lastDate.DayOfYear + theday) / 7 + 1;
        }
        else
        {
            MaxWeek = (lastDate.DayOfYear + theday) / 7;
        }
        return weekNum;

    }
}

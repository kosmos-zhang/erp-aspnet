<%@ WebHandler Language="C#" Class="StorageCost" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
public class StorageCost : SubBaseHandler
{

    public override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "get":
                GetList();
                break;
            case "getpreyearmonth":
                GetStartAndEndDate();
                break;
            case "calculation":
                CalculationStorageCost();
                break;
            case "edit":
                EditPeriodCost();
                break;
        }
    }


    /* 修改库存成本 */
    protected void EditPeriodCost()
    {
        Decimal cost = Convert.ToDecimal(GetRequestNum("cost"));
        int id = Convert.ToInt32(GetRequestNum("id"));

        bool res = XBase.Business.Office.StorageManager.StorageCostBus.EditPeriodEndCost(cost, id);
        if (res)
            OutputResult(true, "调整期末成本成功");
        else
            OutputResult(false, "调整期末成本失败");
    }
    

    /* 计算存货成本 */
    protected void CalculationStorageCost()
    {
        string yearMonth = GetRequest("YearMonth");
        string startDate = GetRequest("StartDate");
        string endDate = GetRequest("EndDate");
        string preYearMonth = GetPreYearMonth(yearMonth);
        string result = XBase.Business.Office.StorageManager.StorageCostBus.CalculationStorageCost(UserInfo.CompanyCD, yearMonth, preYearMonth, startDate, endDate, UserInfo.EmployeeID);
        string[] res = result.Split('|');
        if (res[0] == "0")
            OutputResult(true, res[1]);
        else
            OutputResult(false, res[1]);
    }


    //根据选择的年月 获取开始日期和结束日期
    protected void GetStartAndEndDate()
    {
        string yearMonth = GetRequest("yearMonth");
        string preYearMonth = GetPreYearMonth(yearMonth);
        string preDate = XBase.Business.Office.StorageManager.StorageCostBus.GetLastCalculationDate(preYearMonth, UserInfo.CompanyCD);

        string startDate = string.Empty, endDate = string.Empty;
        if (!string.IsNullOrEmpty(preDate))
        {
            string preStartDate = preDate.Split('|')[0];
            string preEndDate = preDate.Split('|')[1];
            if (string.IsNullOrEmpty(preEndDate))
            {
                endDate = GetDate(yearMonth, false);
            }
            else
            {
                DateTime last = Convert.ToDateTime(preEndDate);
                endDate = last.AddDays(1).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }

            if (string.IsNullOrEmpty(preStartDate))
            {
                startDate = GetDate(yearMonth, true);
            }
            else
            {
                startDate = Convert.ToDateTime(preStartDate).AddDays(1).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
        }
        else
        {
            startDate =  GetDate(yearMonth, true);
            endDate = GetDate(yearMonth, false);
        }

        OutputResult(true, startDate + "|" + endDate);

    }



    /* 获取对应年月的日期 */
    protected string GetDate(string yearMonth, bool start)
    {
        int year = Convert.ToInt32(yearMonth.Substring(0, 4));
        int month = Convert.ToInt32(yearMonth.Substring(4, 2));
        if (start)
        {
            return year.ToString() + "-" + month.ToString() + "-01";
        }
        else
        {
            int days = DateTime.DaysInMonth(year, month);
            return year.ToString() + "-" + month.ToString() + "-" + (days < 10 ? ("0" + days.ToString()) : days.ToString());
        }

    }


    //获取上期年月
    protected string GetPreYearMonth(string yearMonth)
    {
        int year = Convert.ToInt32(yearMonth.Substring(0, 4));
        int month = Convert.ToInt32(yearMonth.Substring(4, 2));

        if (month == 1)
        {
            month = 12;
            year--;
        }
        else
            month--;
        if (month <= 9)
            return year.ToString() + "0" + month.ToString();
        else
            return year.ToString() + month.ToString();

    }




    public void GetList()
    {
        Hashtable htParas = new Hashtable();
        AddToHashtable(ref htParas, "ProductID");//编号
        AddToHashtable(ref htParas, "StartYearMonth");//下单日期 开始
        AddToHashtable(ref htParas, "EndYearMonth");//下单日期 结束
        htParas.Add("CompanyCD", UserInfo.CompanyCD);

        int PageIndex = Convert.ToInt32(GetRequestNum("PageIndex"));//页码
        int PageSize = Convert.ToInt32(GetRequestNum("PageCount"));//页大小
        string OrderBy = GetRequest("OrderBy");//排序字段

        int TotalCount = 0;

        DataTable dt = XBase.Business.Office.StorageManager.StorageCostBus.GetStorageCostList(htParas, PageIndex, PageSize, OrderBy, ref TotalCount);

        OutputDataTable(dt, TotalCount);
    }

}
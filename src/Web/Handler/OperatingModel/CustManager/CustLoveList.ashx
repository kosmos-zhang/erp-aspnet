<%@ WebHandler Language="C#" Class="CustLoveList" %>


using System;
using System.Web;
using System.Data;
using System.Text;
using XBase.Common;
public class CustLoveList : BaseHandler
{
    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "loaddata":
                LoadData();//加载数据
                break;
            case "lovecount":
                LoadLoveCount();
                break;
            case "lovebytype":
                LoadLoveByType();
                break;
            case "lovebyman":
                LoadLoveByMan();
                break;
            case "lovebydays":
                LoadLoveByDays();
                break;
            default:
                DefaultAction(action);
                break;
        }

    }


    /// <summary>
    /// 零投诉客户统计
    /// </summary>
    private void LoadLoveByDays()
    {
        string Days = GetParam("Days");
        string CompanyCD = UserInfo.CompanyCD;

        /* 页面参数 */
        string pageIndex = GetParam("PageIndex");
        if (pageIndex == string.Empty)
            pageIndex = "1";
        string pageSize = GetParam("PageSize");
        if (pageSize == string.Empty)
            pageSize = "10";

        string orderExp = "";

        string orderString = GetParam("orderby");
        if (orderString == string.Empty)
        {
            orderExp = "CustNO ASC";
        }
        else
        {
            string order = "asc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CustNo";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            orderExp = orderBy + " " + order;
        }

        DataTable dataList = new DataTable();
        int TotalCount = 0;
        dataList = XBase.Business.Office.CustManager.LoveBus.GetLoveByDays(Days, CompanyCD, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
       SessionUtil.Session.Add("CurrentListTable", XBase.Business.Office.CustManager.LoveBus.GetLoveByDays(Days, CompanyCD, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount));
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());

    }

    /// <summary>
    /// 被投诉人统计
    /// </summary>
    private void LoadLoveByMan()
    {
        string Linker = GetParam("Linker");
        string StartDate = GetParam("StartDate");
        string EndDate = GetParam("EndDate");
        string CompanyCD = UserInfo.CompanyCD;

        /* 页面参数 */
        string pageIndex = GetParam("PageIndex");
        if (pageIndex == string.Empty)
            pageIndex = "1";
        string pageSize = GetParam("PageSize");
        if (pageSize == string.Empty)
            pageSize = "10";

        string orderExp = "";

        string orderString = GetParam("orderby");
        if (orderString == string.Empty)
        {
            orderExp = "CustNO ASC";
        }
        else
        {
            string order = "asc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CustNO";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            orderExp = orderBy + " " + order;
        }

        DataTable dataList = new DataTable();
        int TotalCount = 0;
        dataList = XBase.Business.Office.CustManager.LoveBus.GetLoveByMan(Linker, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
         SessionUtil.Session.Add("CurrentListTable", XBase.Business.Office.CustManager.LoveBus.GetLoveByMan(Linker, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount));
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());

    }


    /// <summary>
    /// 投诉分类统计
    /// </summary>
    private void LoadLoveByType()
    {
        string LoveType = GetParam("LoveType");
        string StartDate = GetParam("StartDate");
        string EndDate = GetParam("EndDate");
        string CompanyCD = UserInfo.CompanyCD;

        /* 页面参数 */
        string pageIndex = GetParam("PageIndex");
        if (pageIndex == string.Empty)
            pageIndex = "1";
        string pageSize = GetParam("PageSize");
        if (pageSize == string.Empty)
            pageSize = "10";

        string orderExp = "";

        string orderString = GetParam("orderby");
        if (orderString == string.Empty)
        {
            orderExp = "TypeName ASC";
        }
        else
        {
            string order = "asc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "TypeName";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            orderExp = orderBy + " " + order;
        }
        // Output(LoveType + CompanyCD);
        DataTable dataList = new DataTable();
        int TotalCount = 0;
        dataList = XBase.Business.Office.CustManager.LoveBus.GetLoveByType(LoveType, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
        SessionUtil.Session.Add("CurrentListTable",XBase.Business.Office.CustManager.LoveBus.GetLoveByType(LoveType, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount));
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());

    }


    /// <summary>
    /// 投诉次数统计
    /// </summary>
    private void LoadLoveCount()
    {
        string CustName = GetParam("CustName");
        string LoveType = GetParam("LoveType");
        string StartDate = GetParam("StartDate");
        string EndDate = GetParam("EndDate");
        string CompanyCD = UserInfo.CompanyCD;

        /* 页面参数 */
        string pageIndex = GetParam("PageIndex");
        if (pageIndex == string.Empty)
            pageIndex = "1";
        string pageSize = GetParam("PageSize");
        if (pageSize == string.Empty)
            pageSize = "10";

        string orderExp = "";

        string orderString = GetParam("orderby");
        if (orderString == string.Empty)
        {
            orderExp = "CustNO ASC";
        }
        else
        {
            string order = "asc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CustNo";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            orderExp = orderBy + " " + order;
        }

        DataTable dataList = new DataTable();
        int TotalCount = 0;
        dataList = XBase.Business.Office.CustManager.LoveBus.GetCustLoveCount(CustName, LoveType, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
       
         SessionUtil.Session.Add("CurrentListTable",XBase.Business.Office.CustManager.LoveBus.GetCustLoveCount(CustName, LoveType, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount));
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());

    }

    /// <summary>
    /// 投诉一览表
    /// </summary>
    private void LoadData()
    {
        string CustName = GetParam("CustName");
        string LoveType = GetParam("LoveType");
        string StartDate = GetParam("StartDate");
        string EndDate = GetParam("EndDate");
        string CompanyCD = UserInfo.CompanyCD;

        /* 页面参数 */
        string pageIndex = GetParam("PageIndex");
        if (pageIndex == string.Empty)
            pageIndex = "1";
        string pageSize = GetParam("PageSize");
        if (pageSize == string.Empty)
            pageSize = "10";

        string orderExp = "";

        string orderString = GetParam("orderby");
        if (orderString == string.Empty)
        {
            orderExp = "CustNO ASC";
        }
        else
        {
            string order = "asc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CustNO";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            orderExp = orderBy + " " + order;
        }
        //Output(LoveType);
        DataTable dataList = new DataTable();
        int TotalCount = 0;
        dataList = XBase.Business.Office.CustManager.LoveBus.GetLoveList(CustName, LoveType, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
        
         SessionUtil.Session.Add("CurrentListTable", XBase.Business.Office.CustManager.LoveBus.GetLoveList(CustName, LoveType, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount));
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());
    } 

}
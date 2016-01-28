<%@ WebHandler Language="C#" Class="CustTalkList" %>

using System;
using System.Web;
using System.Data;
using System.Text;

public class CustTalkList :BaseHandler {
    
    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "loaddata":
                LoadData();//加载数据
                break;
            case "talkcount":
                LoadTalkCount();
                break;
            case "talkbytype":
                LoadTalkByType();
                break;
            case "talkbypriority":
                LoadTalkByPriority();
                break;
            case "talkbyman":
                LoadTalkByMan();
                break;
            case "talkbydays":
                LoadTalkByDays();
                break;
            default:
                DefaultAction(action);
                break;
        }

    }

    private void LoadTalkByPriority() 
    {
        string Priority = GetParam("Priority");
        string CustName = GetParam("CustName");
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
        // Output(TalkType + CompanyCD);
        DataTable dataList = new DataTable();
        int TotalCount = 0;
        dataList = XBase.Business.Office.CustManager.TalkBus.GetCustTalkByPriority(CustName,Priority, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString()); 
    }
    
    /// <summary>
    /// 未洽谈客户统计
    /// </summary>
    private void LoadTalkByDays()
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
        dataList = XBase.Business.Office.CustManager.TalkBus.GetTalkByDays(Days, CompanyCD, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());

    }

    /// <summary>
    /// 按洽谈人统计
    /// </summary>
    private void LoadTalkByMan()
    {
        string Linker = GetParam("Linker");
        string CustName = GetParam("CustName");
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
        dataList = XBase.Business.Office.CustManager.TalkBus.GetTalkByMan(CustName,Linker, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());

    }


    /// <summary>
    /// 按洽谈方式统计
    /// </summary>
    private void LoadTalkByType()
    {
        string TalkType = GetParam("TalkType");
        string CustName = GetParam("CustName");
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
        // Output(TalkType + CompanyCD);
        DataTable dataList = new DataTable();
        int TotalCount = 0;
        dataList = XBase.Business.Office.CustManager.TalkBus.GetTalkByType(CustName, TalkType, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
        StringBuilder sb = new StringBuilder();
        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());

    }


    /// <summary>
    /// 按洽谈次数统计
    /// </summary>
    private void LoadTalkCount()
    {
        string CustName = GetParam("CustName");
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
        dataList = XBase.Business.Office.CustManager.TalkBus.GetCustTalkCount(CustName,CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());

    }

    /// <summary>
    /// 洽谈一览表
    /// </summary>
    private void LoadData()
    {
        string CustName = GetParam("CustName");
        string Priority = GetParam("Priority");
        string Status = GetParam("Status");
        string TalkType = GetParam("TalkType");
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
        //Output(TalkType);
        DataTable dataList = new DataTable();
        int TotalCount = 0;
        dataList = XBase.Business.Office.CustManager.TalkBus.GetTalkList(CustName,TalkType,Priority,Status,CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
        StringBuilder sb = new StringBuilder();
        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());
    } 

}
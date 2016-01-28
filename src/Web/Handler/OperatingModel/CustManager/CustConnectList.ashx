<%@ WebHandler Language="C#" Class="CustConnectList" %>

using System;
using System.Web;
using System.Data;
using System.Text;
using XBase.Common;
public class CustConnectList:BaseHandler {

    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "loaddata":
                LoadData();//加载数据
                break;
            case "contactcount":
                LoadCountCount();
                break;
            case "contactreason":
                LoadCountReason();
                break;
            case "contactlinkmode":
                LoadContactLinkMode();
                break;
            case "contactlinker":
                LoadContactLinker();
                break;
            case "contactdays":
                LoadContactDays();
                break;
            default:
                DefaultAction(action);
                break;
        }

    }
    /// <summary>
    /// 按联络人统计
    /// </summary>
    private void LoadContactDays() 
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
            orderExp = "CustNo ASC";
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
        dataList = XBase.Business.Office.CustManager.ContactHistoryBus.GetStatCustByDays(CompanyCD,Convert.ToInt32(Days),Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);

        SessionUtil.Session.Add("CurrentListTable", XBase.Business.Office.CustManager.ContactHistoryBus.GetStatCustByDays(CompanyCD, Convert.ToInt32(Days),1, 99999, orderExp, ref TotalCount));
        //Output(orderExp);
        
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());
    }
    
    /// <summary>
    /// 未联络客户统计 
    /// </summary>
    private void LoadContactLinker() 
    {
        string CustName = GetParam("CustName");
        string LinkerId = GetParam("LinkerId");
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
            orderExp = "CustNo ASC";
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
        dataList = XBase.Business.Office.CustManager.ContactHistoryBus.GetStatContactNumByCustAndLinkMan(CustName, LinkerId, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);

        SessionUtil.Session.Add("CurrentListTable", XBase.Business.Office.CustManager.ContactHistoryBus.GetStatContactNumByCustAndLinkMan(CustName, LinkerId, CompanyCD, StartDate, EndDate, 1, 99999, orderExp, ref TotalCount));
        //Output(orderExp);
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());
         
    }
    
    /// <summary>
    /// 按联络方式统计 
    /// </summary>
    private void LoadContactLinkMode() 
    {
        string CustName = GetParam("CustName");
        string LinkReasonId = GetParam("ReasonId");
        string LinkModeId = GetParam("LinkModeId");
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
            orderExp = "CustNo ASC";
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
        dataList = XBase.Business.Office.CustManager.ContactHistoryBus.GetStatContactNumByCustAndLinkMode(CustName, LinkReasonId, LinkModeId, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);

        SessionUtil.Session.Add("CurrentListTable", XBase.Business.Office.CustManager.ContactHistoryBus.GetStatContactNumByCustAndLinkMode(CustName, LinkReasonId, LinkModeId, CompanyCD, StartDate, EndDate, 1, 99999, orderExp, ref TotalCount));
        //Output(orderExp);
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());
        
    }
    
    /// <summary>
    /// 按联络事由统计 
    /// </summary>
    private void LoadCountReason() 
    {
        string CustName = GetParam("CustName");
        string LinkReasonId = GetParam("ReasonId");
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
            orderExp = "CustNo ASC";
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
        dataList = XBase.Business.Office.CustManager.ContactHistoryBus.GetStatContactNumByCustAndReason(CustName,LinkReasonId,CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
        SessionUtil.Session.Add("CurrentListTable", XBase.Business.Office.CustManager.ContactHistoryBus.GetStatContactNumByCustAndReason(CustName, LinkReasonId, CompanyCD, StartDate, EndDate, 1,99999, orderExp, ref TotalCount));
        //Output(orderExp);
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());
    }
    
    /// <summary>
    /// 联络次数统计
    /// </summary>
    private void LoadCountCount() 
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
            orderExp = "CustNo ASC";
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
        dataList = XBase.Business.Office.CustManager.ContactHistoryBus.GetStatContactNumByCust(CustName, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
        SessionUtil.Session.Add("CurrentListTable", XBase.Business.Office.CustManager.ContactHistoryBus.GetStatContactNumByCust(CustName, CompanyCD, StartDate, EndDate,1,99999, orderExp, ref TotalCount));
        
        //Output(orderExp);
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());

    }
    
    /// <summary>
    /// 联络一览表
    /// </summary>
    private void LoadData() 
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
            orderExp = "ID ASC";
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
        dataList = XBase.Business.Office.CustManager.ContactHistoryBus.GetContactInfoBycondition(CustName, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);

        SessionUtil.Session.Add("CurrentListTable", XBase.Business.Office.CustManager.ContactHistoryBus.GetContactInfoBycondition(CustName, CompanyCD, StartDate, EndDate, 1,99999, orderExp, ref TotalCount));
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());

    }
}
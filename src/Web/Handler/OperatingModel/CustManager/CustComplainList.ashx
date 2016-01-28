<%@ WebHandler Language="C#" Class="CustComplainList" %>

using System;
using System.Web;
using System.Data;
using System.Text;
using XBase.Common;
public class CustComplainList:BaseHandler {

    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "loaddata":
                LoadData();//加载数据
                break;
            case "complaincount":
                LoadComplainCount();
                break;
            case "complainbytype":
                LoadComplainByType();
                break;
            case "complainbyman":
                LoadComplainByMan();
                break;
            case "complaindays":
                LoadComplainDays();
                break;
            default:
                DefaultAction(action);
                break;
        }

    }


    /// <summary>
    /// 零投诉客户统计
    /// </summary>
    private void LoadComplainDays()
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
        dataList = XBase.Business.Office.CustManager.ComplainBus.GetComplainByDays(Days,CompanyCD,Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);

        SessionUtil.Session.Add("CurrentListTable", XBase.Business.Office.CustManager.ComplainBus.GetComplainByDays(Days, CompanyCD, 1, 99999, orderExp, ref TotalCount));
        
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
    private void LoadComplainByMan()
    {
        string ComplainMan = GetParam("ComplainMan");
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
        dataList = XBase.Business.Office.CustManager.ComplainBus.GetComplainByMan(ComplainMan,CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);

        SessionUtil.Session.Add("CurrentListTable", XBase.Business.Office.CustManager.ComplainBus.GetComplainByMan(ComplainMan, CompanyCD, StartDate, EndDate, 1,99999, orderExp, ref TotalCount));
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
    private void LoadComplainByType()
    {
        string ComplainType = GetParam("ComplainType");
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
       // Output(ComplainType + CompanyCD);
        DataTable dataList = new DataTable();
        int TotalCount = 0;
        dataList = XBase.Business.Office.CustManager.ComplainBus.GetComplainByType(ComplainType, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);

        SessionUtil.Session.Add("CurrentListTable", XBase.Business.Office.CustManager.ComplainBus.GetComplainByType(ComplainType, CompanyCD, StartDate, EndDate, 1, 99999, orderExp, ref TotalCount));
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
    private void LoadComplainCount() 
    {
        string CustName = GetParam("CustName");
        string ComplainType = GetParam("ComplainType");
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
        dataList = XBase.Business.Office.CustManager.ComplainBus.GetComplainCount(CustName, ComplainType, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);

        SessionUtil.Session.Add("CurrentListTable", XBase.Business.Office.CustManager.ComplainBus.GetComplainCount(CustName, ComplainType, CompanyCD, StartDate, EndDate, 1, 99999, orderExp, ref TotalCount));
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
        string ComplainType = GetParam("ComplainType");
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
        dataList = XBase.Business.Office.CustManager.ComplainBus.GetComplainList(CustName, ComplainType, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);

        SessionUtil.Session.Add("CurrentListTable", XBase.Business.Office.CustManager.ComplainBus.GetComplainList(CustName, ComplainType, CompanyCD, StartDate, EndDate, 1, 99999, orderExp, ref TotalCount));
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + TotalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());
    } 
    
}
<%@ WebHandler Language="C#" Class="CustOrderList" %>

using System;
using System.Web;
using System.Data;
using System.Text;

public class CustOrderList : BaseHandler
{
    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "loadcustorderbyproduct":
                LoadCustOrderByProduct();//加载数据
                break;
            case "loadcustorderbydate":
                LoadCustOrderByDate();
                break;
            case "loadcustorderbydays":
                LoadCustOrderByDays();
                break;
  
        }

    }


    /// <summary>
    /// 零投诉客户统计
    /// </summary>
    private void LoadCustOrderByDays()
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
        dataList = XBase.Business.Office.CustManager.CustInfoBus.GetStatCustBuyByDays(CompanyCD,Convert.ToInt32(Days),Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
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
    private void LoadCustOrderByDate()
    {
        string ProductName = GetParam("ProductName");
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
        dataList = XBase.Business.Office.CustManager.CustInfoBus.GetStatCustBuyByDate(ProductName,CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
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
    private void LoadCustOrderByProduct() 
    {
        string ProductName = GetParam("ProductName");
        string CustID = GetParam("CustID");

        string MinCount = GetParam("MinCount");
        string MaxCount = GetParam("MaxCount");
        string MinPrice = GetParam("MinPrice");
        string MaxPrice = GetParam("MaxPrice");
        
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
            orderExp = "ProdNo ASC";
        }
        else
        {
            string order = "asc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProdNo";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            orderExp = orderBy + " " + order;
        }
        // Output(LoveType + CompanyCD);
        DataTable dataList = new DataTable();
        int TotalCount = 0;
        dataList = XBase.Business.Office.CustManager.CustInfoBus.GetStatCustBuyByProduct(ProductName,CustID, MinCount,MaxCount,MinPrice,MaxPrice, CompanyCD, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref TotalCount);
        StringBuilder sb = new StringBuilder();

         if (dataList.Rows.Count == 0)
         {
             sb.Append("{result:true,data:");
             sb.Append("{count:" + TotalCount.ToString() + ",");
             sb.Append("list:" + "[{\"ProdNo\":\"\"}]" + "}");
             sb.Append("}");
             
         }
         else
         {
             sb.Append("{result:true,data:");
             sb.Append("{count:" + TotalCount.ToString() + ",");
             sb.Append("list:" + DataTable2Json(dataList) + "}");
             sb.Append("}");
         }
        
        Output(sb.ToString());
    }

}
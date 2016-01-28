<%@ WebHandler Language="C#" Class="StorageLossAnalysisDetails" %>
using System;
using System.Web;
using System.Data;
using System.Text;
using System.Collections.Generic;
using XBase.Business.Office.StorageManager;

public class StorageLossAnalysisDetails : BaseHandler
{
    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "storage":
            case "deptprice":
                EquipInfo();
                break;
            default:
                DefaultAction(action);
                break;
        }
    }
  
    private void EquipInfo()
    {
        string productID = GetParam("productid");
        string beginDate = GetParam("begindate");
        string endDate = GetParam("enddate");
        int ByTimeType =Convert.ToInt32(GetParam("ByTimeType"));
                        
        string pageIndex = GetParam("PageIndex");
        if (pageIndex == string.Empty)
            pageIndex = "1";
        string pageSize = GetParam("PageSize");
        if (pageSize == string.Empty)
            pageSize = "10";

        
        string orderString = GetParam("orderby");
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        string orderExp = orderBy + " " + order;

        DataTable dataList = null;
        int recCount = 0;

        dataList = StorageBus.GetStorageLossAnalysisDetails(Convert.ToInt32(productID), beginDate, endDate,ByTimeType, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), ref recCount);
        StringBuilder sb = new StringBuilder();
        sb.Append("{totalCount:" + recCount + ",data:");
        sb.Append(DataTable2Json(dataList));
        sb.Append("}");

        Output(sb.ToString());
    }
    
   
    
}
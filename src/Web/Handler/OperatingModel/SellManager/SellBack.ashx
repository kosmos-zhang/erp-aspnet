<%@ WebHandler Language="C#" Class="SellContract" %>
using System;
using System.Web;
using System.Data;
using System.Text;
using System.Collections.Generic;

public class SellContract :BaseHandler
{
    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "deptnum":
            case "personnum":
            case "areanum":
            case "trendnum":
            case "deptprice":
            case "personprice":
            case "areaprice":
            case "trendprice":
                DeptAndPerson();
                break;
            default:
                DefaultAction(action);
                break;
        }
    }
  
    private void DeptAndPerson()
    {
        string GroupType = GetParam("GroupType");
        string Status =GetParam("Status");
        string Type = GetParam("Type");
        string StartDate = GetParam("StartDate");
        string EndDate = GetParam("EndDate");
        string DeptOrEmployeeId = GetParam("DeptOrEmployeeId");
        string AreaId = GetParam("AreaId");
        string DateValue = GetParam("DateValue");
        string DateType = GetParam("DateType");
        string pageIndex = GetParam("PageIndex");
        if (pageIndex == string.Empty)
            pageIndex = "1";
        string pageSize = GetParam("PageSize"); 
        if (pageSize == string.Empty)
            pageSize = "10";

        string orderString = GetParam("orderby");
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "BackNo";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        string orderExp = orderBy + " " + order;

        DataTable dataList = null;
        int recCount = 0;

        dataList =XBase.Business.Office.SellManager.SellBackBus.GetSellBackDetail(GroupType, DeptOrEmployeeId, DateType, DateValue, Status, Type, StartDate, EndDate, AreaId, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref recCount);

        StringBuilder sb = new StringBuilder();
        sb.Append("{totalCount:" + recCount + ",data:");
        sb.Append(DataTable2Json(dataList));
        sb.Append("}");

        Output(sb.ToString());
    }
    
   
    
}
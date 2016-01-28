<%@ WebHandler Language="C#" Class="CustTalkByPriorityDetails" %>
using System;
using System.Web;
using System.Data;
using System.Text;
using System.Collections.Generic;
using XBase.Business.Office.CustManager;

public class CustTalkByPriorityDetails : BaseHandler
{
    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "countnum":
                EquipInfo();
                break;
            case "custnum":
                EquipInfo1();
                break;
            default:
                DefaultAction(action);
                break;
        }
    }

    private void EquipInfo1()
    {
        string begindate = GetParam("begindate");
        string enddate = GetParam("enddate");
        string custname = GetParam("custname");
        string custID = GetParam("custID");
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

        dataList = CustInfoBus.GetCustTalkByCountDetails(UserInfo.CompanyCD,begindate, enddate, custname, custID, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref recCount);

        StringBuilder sb = new StringBuilder();
        sb.Append("{totalCount:" + recCount + ",data:");
        sb.Append(DataTable2Json(dataList));
        sb.Append("}");

        Output(sb.ToString());
    }
    
    private void EquipInfo()
    {
        string begindate = GetParam("begindate");
        string enddate = GetParam("enddate");
        string custname = GetParam("custname");
        string Priority = GetParam("Priority");        
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

        dataList = CustInfoBus.GetCustTalkByPriorityDetails(begindate, enddate, custname, Priority, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref recCount);
        
        StringBuilder sb = new StringBuilder();
        sb.Append("{totalCount:" + recCount + ",data:");
        sb.Append(DataTable2Json(dataList));
        sb.Append("}");

        Output(sb.ToString());
    }
    
   
    
}
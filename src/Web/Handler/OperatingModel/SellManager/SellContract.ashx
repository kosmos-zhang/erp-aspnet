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
                DeptAndPerson();//加载数据
                break;
            case "statenum":
            case "typenum":
                StateAndType();
                break;
            case "trendnum":
                TrendNum();
                break;
            default:
                DefaultAction(action);
                break;
        }
    }
    private void TrendNum()
    {
        string Status = GetParam("Status");
        string Type = GetParam("Type");
        string StartDate = GetParam("StartDate");
        string EndDate = GetParam("EndDate");
        string Name = GetParam("Name");
        string DataType1 = GetParam("DataType");
        string GroupType = GetParam("GroupType");
        string DeptOrEmployeeId = GetParam("DeptOrEmployeeId");
        
        string pageIndex = GetParam("PageIndex");
        if (pageIndex == string.Empty)
            pageIndex = "1";
        string pageSize = GetParam("PageSize");
        if (pageSize == string.Empty)
            pageSize = "10";

        string orderString = GetParam("orderby");
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ContractNo";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        string orderExp = orderBy + " " + order;

        DataTable dataList = null;
        int recCount = 0;
        dataList = XBase.Business.Office.SellManager.SellContractBus.GetSellContractDetail(Convert.ToInt32(Status),Convert.ToInt32(Type),Name,"", StartDate, EndDate,DataType1,GroupType,DeptOrEmployeeId, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref recCount);

        StringBuilder sb = new StringBuilder();
        sb.Append("{totalCount:" + recCount + ",data:");
        sb.Append(DataTable2Json(dataList));
        sb.Append("}");

        Output(sb.ToString());
    }
    private void StateAndType()
    {
        string Status = GetParam("Status");
        string Type = GetParam("Type");
        string StartDate = GetParam("StartDate");
        string EndDate = GetParam("EndDate");
        string Name = GetParam("Name");

        string pageIndex = GetParam("PageIndex");
        if (pageIndex == string.Empty)
            pageIndex = "1";
        string pageSize = GetParam("PageSize");
        if (pageSize == string.Empty)
            pageSize = "10";

        string orderString = GetParam("orderby");
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ContractNo";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        string orderExp = orderBy + " " + order;

        DataTable dataList = null;
        int recCount = 0;
        if (GetParam("action").IndexOf("State") != -1)
        {
            dataList = XBase.Business.Office.SellManager.SellContractBus.GetSellContractDetail(Convert.ToInt32(Name), 0, Status, Type, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref recCount);
        }
        else
        {
            dataList = XBase.Business.Office.SellManager.SellContractBus.GetSellContractDetail(0, Convert.ToInt32(Name), Status, Type, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref recCount);
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("{totalCount:" + recCount + ",data:");
        sb.Append(DataTable2Json(dataList));
        sb.Append("}");

        Output(sb.ToString());
    }
    
    private void DeptAndPerson()
    {
        int Status = Convert.ToInt32(GetParam("Status"));
        int Type = Convert.ToInt32(GetParam("Type"));
        string StartDate = GetParam("StartDate");
        string EndDate = GetParam("EndDate");
        string Name = GetParam("Name");

        string pageIndex = GetParam("PageIndex");
        if (pageIndex == string.Empty)
            pageIndex = "1";
        string pageSize = GetParam("PageSize");
        if (pageSize == string.Empty)
            pageSize = "10";

        string orderString = GetParam("orderby");
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ContractNo";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        string orderExp = orderBy + " " + order;

        DataTable dataList = null;
        int recCount = 0;
        string DeptOrPerson = "";
        if (GetParam("action").IndexOf("Dept")!=-1)
        {
            DeptOrPerson = "Dept";
        }
        else
        {
            DeptOrPerson = "Person";
        }
        
        dataList = XBase.Business.Office.SellManager.SellContractBus.GetSellContractDetail(Status, Type, Name, DeptOrPerson, StartDate, EndDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref recCount);

        StringBuilder sb = new StringBuilder();
        sb.Append("{totalCount:" + recCount + ",data:");
        sb.Append(DataTable2Json(dataList));
        sb.Append("}");

        Output(sb.ToString());
    }
    
   
    
}
<%@ WebHandler Language="C#" Class="PurchaseOrderProduct" %>
using System;
using System.Web;
using System.Data;
using System.Text;
using System.Collections.Generic;

public class PurchaseOrderProduct : BaseHandler
{
    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "productnum":
            case "productprice":
            case "productnumtrend":
            case "productpricetrend":
                DeptAndPerson();
                break;
            default:
                DefaultAction(action);
                break;
        }
    }
  
    private void DeptAndPerson()
    {
        string DateType = GetParam("DateType");
        string ProductId = GetParam("ProductId");
        string StartDate = GetParam("StartDate");
        string EndDate = GetParam("EndDate");
        string DeptId = GetParam("DeptId");
        string ProviderId = GetParam("ProviderId");
        string DateValue = GetParam("DateValue");
        string pageIndex = GetParam("PageIndex");
        if (pageIndex == string.Empty)
            pageIndex = "1";
        string pageSize = GetParam("PageSize");
        if (pageSize == string.Empty)
            pageSize = "10";

        string orderString = GetParam("orderby");
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "OrderNO";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        string orderExp = orderBy + " " + order;

        DataTable dataList = null;
        int recCount = 0;

        dataList = XBase.Business.Office.PurchaseManager.PurchaseOrderBus.GetPurchaseOrderProductDetail(ProviderId, DeptId, StartDate, EndDate, DateType, DateValue, ProductId, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), orderExp, ref recCount);

        StringBuilder sb = new StringBuilder();
        sb.Append("{totalCount:" + recCount + ",data:");
        sb.Append(DataTable2Json(dataList));
        sb.Append("}");

        Output(sb.ToString());
    }
    
   
    
}
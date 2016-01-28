<%@ WebHandler Language="C#" Class="SelectSellOrderDetail" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Office.SellManager;
using XBase.Model.Office.SellManager;

public class SelectSellOrderDetail : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string action = (context.Request.Form["actionDet"].ToString());//操作
            if (action == "detail")
            {
                GetList(context);
            }
            else if (action == "getLsit")
            {
                GetListByDetailID(context);
            }
            else if (action == "order")
            {
                GetOrderList( context);
            }
            else if (action == "orderList")
            {
                 GetOrderAndDetail( context);
            }

        }
    }

    /// <summary>
    /// 获取订单信息以及其明细
    /// </summary>
    /// <param name="context"></param>
    private void GetOrderAndDetail(HttpContext context)
    {
        string strOrderID = string.Empty;//订单编号
        string strOrderJson = string.Empty;//订单基本信息
        string strOrderListJson = string.Empty;//订单明细信息
        string strJson = string.Empty;//订单信息

        strOrderID = (context.Request.Form["OrderID"].ToString());//订单号

        strJson = "{";
        //判断单据信息是否仍然存在
        if (SelectSellOrderDetailBus.GetSellOrderInfo(strOrderID).Rows.Count == 1)
        {
            strOrderJson = JsonClass.DataTable2Json(SelectSellOrderDetailBus.GetSellOrderInfo(strOrderID));
            strJson += "\"ord\":" + strOrderJson;
            //判断单据是否存在明细信息
            if (SelectSellOrderDetailBus.GetSellOrderDetailByCustID(null, null, strOrderID).Rows.Count > 0)
            {
                strOrderListJson = JsonClass.DataTable2Json(SelectSellOrderDetailBus.GetSellOrderDetailByCustID(null, null, strOrderID));
                strJson += ",\"ordList\":" + strOrderListJson;
            }
        }
        strJson += "}";
        context.Response.Write(strJson);
    }

    /// <summary>
    /// 获取订单类表
    /// </summary>
    /// <param name="context"></param>
    private void GetOrderList(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderByOrder"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "OrderDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageOrderCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

        string Title = context.Request.Form["Title"].ToString().Trim().Length == 0 ? null : context.Request.Form["Title"].ToString().Trim();
        string OrderNo = context.Request.Form["orderNo"].ToString().Trim().Length == 0 ? null : context.Request.Form["orderNo"].ToString().Trim();
        string model = context.Request.Form["model"].ToString().Trim().Length == 0 ? "all" : context.Request.Form["model"].ToString().Trim();
        string CustName = context.Request.Form["CustName"].ToString().Trim().Length == 0 ? null : context.Request.Form["CustName"].ToString().Trim();
        int? CurrencyType = context.Request.Form["CurrencyType"].ToString().Trim().Length == 0 ? null : (int?)Convert.ToInt32(context.Request.Form["CurrencyType"].ToString().Trim());
        int totalCount = 0;
        string ord = orderBy + " " + order;

        DataTable dt = SelectSellOrderDetailBus.GetSellOrderList(OrderNo, Title, CustName, CurrencyType, model, pageIndex, pageCount, ord, ref totalCount);
       
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

    /// <summary>
    /// 根据订单明细ID获取明细信息
    /// </summary>
    private void GetListByDetailID(HttpContext context)
    {
        int? iCustID = null;
        string strDetailID = context.Request.Form["DetailID"].ToString().Trim();
        string strOrderID = null;
        string strJson = string.Empty;//订单信息
        strDetailID = strDetailID.Remove(strDetailID.Length - 1);
        DataTable dt = SelectSellOrderDetailBus.GetSellOrderDetailByCustID(iCustID, strDetailID, strOrderID);
        strJson = "{";
        if (dt.Rows.Count > 0)
        {
            strJson += "\"data\":" + JsonClass.DataTable2Json(dt);
        }
        strJson += "}";
        context.Response.Write(strJson);
    }

    /// <summary>
    /// 获取订单明细
    /// </summary>
    /// <param name="context"></param>
    private void GetList(HttpContext context)
    {
        int? iCustID = null;
        string strDetailID = null;
        string strOrderID = null;

        string orderString = (context.Request.Form["orderByDet"].ToString());//排序
        string order = "desc";//排序：升序

        if (context.Request.Form["CustID"].ToString().Trim().Length != 0)
        {
            iCustID = Convert.ToInt32(context.Request.Form["CustID"].ToString().Trim());
        }
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "OrderNo";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageDetCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        string Title = context.Request.Form["Title"].ToString().Trim().Length == 0 ? null : context.Request.Form["Title"].ToString().Trim();
        string OrderNo = context.Request.Form["orderNo"].ToString().Trim().Length == 0 ? null : context.Request.Form["orderNo"].ToString().Trim();
        //string model = context.Request.Form["model"].ToString().Trim().Length == 0 ? "all" : context.Request.Form["model"].ToString().Trim();
        string CurrencyType = context.Request.Form["CurrencyType"].ToString().Trim().Length == 0 ? null : context.Request.Form["CurrencyType"].ToString().Trim();
        string OrderID = context.Request.Form["OrderID"].ToString().Trim().Length == 0 ? null : context.Request.Form["OrderID"].ToString().Trim();
        string Rate = context.Request.Form["Rate"].ToString().Trim().Length == 0 ? null : context.Request.Form["Rate"].ToString().Trim();
        
        int totalCount = 0;
        string ord = orderBy + " " + order;

        DataTable dt = SelectSellOrderDetailBus.GetSellOrderDetailByCustID(iCustID, strDetailID, strOrderID, CurrencyType,Rate, OrderID, OrderNo,
            Title, pageIndex, pageCount, ord, ref totalCount);
        
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"OrderID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }


    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
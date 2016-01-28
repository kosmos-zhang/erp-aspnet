<%@ WebHandler Language="C#" Class="SelectSellOfferUC" %>

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

public class SelectSellOfferUC : IHttpHandler, System.Web.SessionState.IRequiresSessionState 
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string action = (context.Request.Form["actionOff"].ToString());//操作
            if (action == "UC")
            {
                GetLsit(context);
            }
            else if (action == "info")
            {
                GetSellOffer(context);
            }
            
        }
    }

    /// <summary>
    /// 获取报价单信息
    /// </summary>
    /// <param name="context"></param>
    private void GetSellOffer(HttpContext context)
    {
        string strOffNo = string.Empty;//报价单编号
        string strOffJson = string.Empty;//报价单基本信息
        string strOffListJson = string.Empty;//报价单明细信息
        string strJson = string.Empty;//报价单信息
        
        strOffNo = (context.Request.Form["offerNo"].ToString());//报价单号

        strJson = "{";
        //判断单据信息是否仍然存在
        if (SelectSellOfferUCBus.GetSellOffer(strOffNo).Rows.Count == 1)
        {
            strOffJson = JsonClass.DataTable2Json(SelectSellOfferUCBus.GetSellOffer(strOffNo));
            strJson += "\"off\":" + strOffJson;
            //判断单据是否存在明细信息
            if (SelectSellOfferUCBus.GetSellOfferInfo(strOffNo).Rows.Count > 0)
            {
                strOffListJson = JsonClass.DataTable2Json(SelectSellOfferUCBus.GetSellOfferInfo(strOffNo));
                strJson += ",\"offList\":" + strOffListJson;
            }
        }            
        strJson += "}";
        context.Response.Write(strJson);
    }

    /// <summary>
    /// 获取数据列表
    /// </summary>
    private void GetLsit(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderByOff"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "OfferNo";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageOffCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        string Title = context.Request.Form["Title"].ToString().Trim().Length == 0 ? null : context.Request.Form["Title"].ToString().Trim();
        string OrderNo = context.Request.Form["orderNo"].ToString().Trim().Length == 0 ? null : context.Request.Form["orderNo"].ToString().Trim();
        string model = context.Request.Form["model"].ToString().Trim().Length == 0 ? "all" : context.Request.Form["model"].ToString().Trim();
        string CustName = context.Request.Form["CustName"].ToString().Trim().Length == 0 ? null : context.Request.Form["CustName"].ToString().Trim();
        int? CurrencyType = context.Request.Form["CurrencyType"].ToString().Trim().Length == 0 ? null : (int?)Convert.ToInt32(context.Request.Form["CurrencyType"].ToString().Trim());
        int totalCount = 0;
        string ord = orderBy + " " + order;
        DataTable dt = SelectSellOfferUCBus.GetSellOfferList(OrderNo, Title, CustName, CurrencyType, model, pageIndex, pageCount, ord, ref totalCount);
       
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

   

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
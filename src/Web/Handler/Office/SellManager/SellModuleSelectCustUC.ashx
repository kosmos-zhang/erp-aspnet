<%@ WebHandler Language="C#" Class="SellModuleSelectCustUC" %>
using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Text;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Office.SellManager;
using XBase.Model.Office.SellManager;


public class SellModuleSelectCustUC : IHttpHandler, System.Web.SessionState.IRequiresSessionState 
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string action = (context.Request.Form["actionSellCust"].ToString());//操作
            if (action == "getinfo")
            {
                GetLsit(context);
            }
            else if (action == "info")
            {
                string strID = (context.Request.Form["id"].ToString());//客户id
                GetCustInfo(strID,context);
            }
        }
    }

    /// <summary>
    /// 获取客户信息
    /// </summary>
    /// <param name="strID"></param>
    private void GetCustInfo(string strID, HttpContext context)
    {
        StringBuilder sb = new StringBuilder();
        DataTable dt = SellModuleSelectCustBus.GetCustInfo(strID);
        if (dt.Rows.Count == 1)
        {
            sb.Append("{\"CustType\":\"");
            sb.Append(dt.Rows[0]["CustType"]);
            sb.Append("\",\"CurrencyType\":\"");
            sb.Append(dt.Rows[0]["CurrencyType"]);
            sb.Append("\",\"TakeType\":\"");
            sb.Append(dt.Rows[0]["TakeType"]);
            sb.Append("\",\"PayType\":\"");
            sb.Append(dt.Rows[0]["PayType"]);
            sb.Append("\",\"BusiType\":\"");
            sb.Append(dt.Rows[0]["BusiType"]);
            sb.Append("\",\"CarryType\":\"");
            sb.Append(dt.Rows[0]["CarryType"]);
            sb.Append("\",\"CustName\":\"");
            sb.Append(dt.Rows[0]["CustName"]);
            sb.Append("\",\"TypeName\":\"");
            sb.Append(dt.Rows[0]["TypeName"]);
            sb.Append("\",\"Tel\":\"");
            sb.Append(dt.Rows[0]["Tel"]);
            sb.Append("\",\"ExchangeRate\":\"");
            sb.Append(dt.Rows[0]["ExchangeRate"]);
            sb.Append("\",\"MoneyType\":\"");
            sb.Append(dt.Rows[0]["MoneyType"]);
            sb.Append("\",\"CurrencyName\":\"");
            sb.Append(dt.Rows[0]["CurrencyName"]);
            sb.Append("\"}");            
        }

        context.Response.Write(sb.ToString());
    }


    /// <summary>
    /// 获取数据列表
    /// </summary>
    private void GetLsit(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageSellCustCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        
        string Title = context.Request.Form["Title"].ToString().Trim().Length == 0 ? null : context.Request.Form["Title"].ToString().Trim();
        string OrderNo = context.Request.Form["orderNo"].ToString().Trim().Length == 0 ? null : context.Request.Form["orderNo"].ToString().Trim();
        string model = context.Request.Form["model"].ToString().Trim().Length == 0 ? "all" : context.Request.Form["model"].ToString().Trim();

        int totalCount = 0;
        string ord = orderBy + " " + order;

        DataTable dt = SellModuleSelectCustBus.GetCustList(OrderNo, Title, model, pageIndex, pageCount, ord, ref totalCount);
       
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
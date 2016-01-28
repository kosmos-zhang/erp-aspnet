<%@ WebHandler Language="C#" Class="CustOrders" %>

using System;
using System.Web;
using XBase.Common;
using System.Data;
using XBase.Business.Office.CustManager;

public class CustOrders : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CustNo";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据
        string ProductID = context.Request.Form["ProductID"].ToString();
        string CustID = context.Request.Form["CustID"].ToString();
        
        string NumBegin = context.Request.Form["NumBegin"].ToString();
        string NumEnd = context.Request.Form["NumEnd"].ToString();

        string PriceBegin = context.Request.Form["PriceBegin"].ToString();
        string PriceEnd = context.Request.Form["PriceEnd"].ToString();

        string DateBegin = context.Request.Form["DateBegin"].ToString();
        string DateEnd = context.Request.Form["DateEnd"].ToString();

       
        string CompanyCD =  ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        string ord = orderBy + " " + order;
        int totalCount = 0;

        DataTable dt = CustInfoBus.GetCustOrders(CompanyCD,ProductID,CustID, NumBegin, NumEnd, PriceBegin, PriceEnd, DateBegin, DateEnd, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        
        //if (dt.Rows.Count == 0)
        if (dt == null)
            sb.Append("[{\"custno\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));            
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
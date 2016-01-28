<%@ WebHandler Language="C#" Class="SellModuleSelectTransporter" %>

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
public class SellModuleSelectTransporter : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageSellTranCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

        string Title = context.Request.Form["Title"].ToString().Trim().Length == 0 ? null : context.Request.Form["Title"].ToString().Trim();
        string OrderNo = context.Request.Form["orderNo"].ToString().Trim().Length == 0 ? null : context.Request.Form["orderNo"].ToString().Trim();
       

        int totalCount = 0;
        string ord = orderBy + " " + order;

        DataTable dt = SellModuleSelectTransporterBus.GetTransporter(OrderNo, Title,  pageIndex, pageCount, ord, ref totalCount);
       
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
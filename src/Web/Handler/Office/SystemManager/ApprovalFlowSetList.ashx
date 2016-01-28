<%@ WebHandler Language="C#" Class="ApprovalFlowSetList" %>

using System;
using System.Web;
using XBase.Model.Office.SystemManager;
using System.IO;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Serialization;
using XBase.Common;
using System.Linq;
using System.Data.SqlTypes;
using System.Data;
using System.Xml.Linq;
using XBase.Business.Office.SystemManager;
public class ApprovalFlowSetList : IHttpHandler, System.Web.SessionState.IRequiresSessionState {

    public void ProcessRequest(HttpContext context)
    {
        string TypeCode = context.Request.QueryString["TypeCode"].ToString();
        string TypeFlag = context.Request.QueryString["TypeFlag"].ToString();
        string orderString = context.Request.QueryString["orderby"].ToString();//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "FlowNo";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        int totalCount = 0;
        string ord = orderBy + " " + order;
        string UseStatus = context.Request.QueryString["UseStatus"].ToString();
        //获取数据
        DataTable dt = ApprovalFlowSetBus.GetFlowInfo(TypeFlag, TypeCode, UseStatus, pageIndex, pageCount, ord, ref totalCount);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"FlowNo\":\"\"}]");
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
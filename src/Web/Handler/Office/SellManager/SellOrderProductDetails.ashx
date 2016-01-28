<%@ WebHandler Language="C#" Class="SellOrderProductDetails" %>

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

public class SellOrderProductDetails : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string action = (context.Request.Form["action"].ToString());//操作
            if (action == "action")
            {
                GetLsit(context);
            }
        }
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
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
       
     
        int totalCount = 0;
        string ord = orderBy + " " + order;
        DataTable dt = GetDataTable(context, pageIndex, pageCount, ord,pageIndex,pageCount, ref totalCount);
       
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

    private DataTable GetDataTable(HttpContext context, int pageIndex, int pageCount, string ord,int pageindex,int pagesize, ref int TotalCount)
    {
        DataTable dt = new DataTable();
        string hidvalue = context.Request.Params["hidvalue"].ToString().Trim();
        string begintime = context.Request.Params["begintime"].ToString().Trim();
        string endtime = context.Request.Params["endtime"].ToString().Trim();
        string productid = context.Request.Params["productid"].ToString().Trim();
        string timeType = context.Request.Params["timeType"].ToString().Trim();
        
        dt = SellOrderActiveMoneyBus.SellOrderProductBySetUpDetials(timeType, hidvalue, Convert.ToInt32(productid), begintime, endtime, ord, pageindex, pagesize, ref TotalCount);
        return dt;
    }    

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
<%@ WebHandler Language="C#" Class="SelectAdversary" %>

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

public class SelectAdversary : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string action = (context.Request.Form["actionAd"].ToString());//操作
            if (action == "getinfo")
            {
                GetLsit(context);
            }
            else if (action == "info")
            {
                GetInfo(context);
            }

        }
    }

    /// <summary>
    /// 获取报价单信息
    /// </summary>
    /// <param name="context"></param>
    private void GetInfo(HttpContext context)
    {
        string strID = string.Empty;//报价单编号
        string strAdJson = string.Empty;//报价单基本信息
        string strJson = string.Empty;//报价单信息
        
        strID = context.Request.Form["id"].ToString().Trim();//报价单号
        DataTable dt = AdversarySellBus.GetAdversaryInfo(Convert.ToInt32(strID));
        strJson = "{";
        //判断单据信息是否仍然存在
        if (dt.Rows.Count == 1)
        {
            strAdJson = JsonClass.DataTable2Json(dt);
            strJson += "\"data\":" + strAdJson;
            
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
        string orderString = (context.Request.Form["orderByAd"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CreatDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageAdCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        string Title = context.Request.Form["Title"].ToString().Trim().Length == 0 ? null : context.Request.Form["Title"].ToString().Trim();
        string OrderNo = context.Request.Form["orderNo"].ToString().Trim().Length == 0 ? null : context.Request.Form["orderNo"].ToString().Trim();

        int totalCount = 0;
        string ord = orderBy + " " + order;
        DataTable dt = AdversarySellBus.GetAdversaryInfo(Title, OrderNo, pageIndex, pageCount, ord, ref totalCount);
       
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
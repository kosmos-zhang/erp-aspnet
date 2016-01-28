<%@ WebHandler Language="C#" Class="PurchaseRoadQry" %>

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
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;
using System.Web.SessionState;
using System.Text;

public class PurchaseRoadQry : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    private UserInfoUtil userInfo = null;

    public void ProcessRequest(HttpContext context)
    {
        userInfo = SessionUtil.Session["UserInfo"] as UserInfoUtil;
        if (context.Request.RequestType == "POST")
        {
            string action = context.Request.Form["action"].ToLower();
            switch (action)
            {
                case "select":
                    GetPurchaseRoadQry(context);
                    break;
                case "getpurchaseorder":
                    GetPurchaseOrder(context);
                    break;
            }
        }
    }

    /// <summary>
    /// 获得订单编号
    /// </summary>
    /// <param name="context"></param>
    private void GetPurchaseOrder(HttpContext context)
    {
        //设置行为参数
        string orderBy = (context.Request.Form["orderBy"].ToString());//排序
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int TotalCount = 0;
        DataTable dt = PurchaseOrderBus.SelectPurchaseOrder(pageIndex
                , pageCount, orderBy, ref TotalCount, userInfo);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(TotalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
        {
            sb.Append("\"\"");
        }
        else
        {
            sb.Append(JsonClass.DataTable2Json(dt));
        }
        sb.Append("}");
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

    /// <summary>
    /// 获得在途量
    /// </summary>
    /// <param name="context"></param>
    private void GetPurchaseRoadQry(HttpContext context)
    {
        string CompanyCD = userInfo.CompanyCD;//公司代码

        //设置行为参数
        string orderBy = (context.Request.Form["orderBy"].ToString());//排序
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

        string ProviderID = context.Request.Form["ProviderID"];
        string ProductID = context.Request.Form["ProductID"];
        string StartDate = context.Request.Form["StartDate"];
        string EndDate = context.Request.Form["EndDate"];
        string OrderNo = context.Request.Form["OrderNo"];
        int TotalCount = 0;

        context.Response.ContentType = "text/plain";
        DataTable dt = PurchaseOrderBus.ProdOnRoadQry(CompanyCD, ProductID, ProviderID, StartDate
            , EndDate, OrderNo, pageIndex, pageCount, orderBy, ref TotalCount);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(TotalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
        {
            sb.Append("\"\"");
        }
        else
        {
            sb.Append(JsonClass.DataTable2Json(dt));
        }
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
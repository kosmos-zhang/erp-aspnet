<%@ WebHandler Language="C#" Class="SubSellTotal" %>

using System;
using System.Web;
using System.Data;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using System.Linq;
using XBase.Business.Office.SubStoreManager;
public class SubSellTotal : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        GetSellProductLsit(context);
    }

    /// <summary>
    /// 获取数据列表
    /// </summary>
    private void GetSellProductLsit(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "DeptName";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        //SellGatheringModel sellGatheringModel = new SellGatheringModel();
        int totalCount = 0;
        string ord = orderBy + " " + order;
        try
        {
            DataTable dt = new DataTable();
            string SellDate_ = context.Request.Params["SellDate"].ToString().Trim();
            string SellendDate_ = context.Request.Params["SellEndDate"].ToString().Trim();
            string DeptID = context.Request.Params["DeptID"].ToString().Trim();
            DateTime SellDate = Convert.ToDateTime(SellDate_);
            DateTime sellEndDate = Convert.ToDateTime(SellendDate_);
            //DateTime SellEmdDate = '1900-06-03';
            dt = SubStorageBus.GetSubProductSellInfoByDept(SellDate, sellEndDate, DeptID, "DEPT", pageIndex, pageCount, ord, ref totalCount);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(totalCount.ToString());
            sb.Append(",data:");
            if (dt.Rows.Count == 0)
                sb.Append("[{\"DeptName\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append("}");
            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
        catch
        { }
    }



  
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
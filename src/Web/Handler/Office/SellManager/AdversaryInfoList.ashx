<%@ WebHandler Language="C#" Class="AdversaryInfoList" %>

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

public class AdversaryInfoList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string action = (context.Request.Form["action"].ToString());//操作
            if (action == "getinfo")
            {
                GetLsit(context);
            }
            else if (action == "del")
            {
                DelOrder(context);
            }
            else if (action == "orderInfo")//获取销售机会详细信息
            {
                int orderID = Convert.ToInt32(context.Request.Params["orderID"].ToString().Trim());
                string strJson = string.Empty;
                DataTable dt = AdversaryInfoBus.GetOrderInfo(orderID);
                strJson = "{";
                if (dt.Rows.Count > 0)
                {
                    strJson += "\"data\":" + JsonClass.DataTable2Json(dt);
                }
                strJson += "}";
                context.Response.Write(strJson);
            }
            if (action == "detail")
            {
                string orderNo = context.Request.Params["orderNo"].ToString().Trim();
                string strJson = string.Empty;
                DataTable dt = AdversaryInfoBus.GetOrderDetail(orderNo);
                strJson = "{";
                if (dt.Rows.Count > 0)
                {
                    strJson += "\"data\":" + JsonClass.DataTable2Json(dt);
                }
                strJson += "}";
                context.Response.Write(strJson);
            }
        }
    }

    /// <summary>
    /// 删除单据
    /// </summary>
    private void DelOrder(HttpContext context)
    {
        string orderNos = context.Request.Params["orderNos"].ToString().Trim();
        orderNos = orderNos.Remove(orderNos.Length - 1, 1);
        JsonClass JC;
        if (AdversaryInfoBus.DelOrder(orderNos))
            JC = new JsonClass("success", "", 1);
        else
            JC = new JsonClass("faile", "", 0);
        context.Response.Write(JC);
    }

    /// <summary>
    /// 获取数据列表
    /// </summary>
    private void GetLsit(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        int totalCount = 0;
        string ord = orderBy + " " + order;


        DataTable dt = GetDataTable(context, pageIndex, pageCount, ord, ref totalCount);
       
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

    private DataTable GetDataTable(HttpContext context, int pageIndex, int pageCount, string ord, ref int TotalCount)
    {
        DataTable dt = new DataTable();
        AdversaryInfoModel adversaryInfoModel = new AdversaryInfoModel();
        string strCustNo = context.Request.Params["CustNo"].ToString().Trim();
        string strCustType = context.Request.Params["CustType"].ToString().Trim();
        string strCustName = context.Request.Params["CustName"].ToString().Trim();
        string strPYShort = context.Request.Params["PYShort"].ToString().Trim();


        string CustNo = strCustNo.Length == 0 ? null : strCustNo;
        int? CustType = strCustType.Length == 0 ? null : (int?)Convert.ToInt32(strCustType);

        string CustName = strCustName.Length == 0 ? null : strCustName;
        string PYShort = strPYShort.Length == 0 ? null : strPYShort;

        adversaryInfoModel.CustNo = CustNo;
        adversaryInfoModel.CustType = CustType;
        adversaryInfoModel.CustName = CustName;
        adversaryInfoModel.PYShort = PYShort;

        dt = AdversaryInfoBus.GetOrderList(adversaryInfoModel, pageIndex, pageCount, ord, ref TotalCount);
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
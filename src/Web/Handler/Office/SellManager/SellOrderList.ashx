<%@ WebHandler Language="C#" Class="SellOrderList" %>

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

public class SellOrderList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            else if (action == "orderInfo")//获取单据详细信息
            {
                int orderID = Convert.ToInt32(context.Request.Params["orderID"].ToString().Trim());
                string strJson = string.Empty;
                DataTable dt = SellOrderBus.GetOrderInfo(orderID);
                strJson = "{";
                if (dt.Rows.Count > 0)
                {
                    strJson += "\"data\":" + JsonClass.DataTable2Json(dt);
                }
                strJson += "}";
                context.Response.Write(strJson);
            }
            else if (action == "detail")//获取单据明细信息
            {
                string orderNo = context.Request.Params["orderNo"].ToString().Trim();
                string strJson = string.Empty;
                DataTable dt = SellOrderBus.GetOrderDetail(orderNo);
                strJson = "{";
                if (dt.Rows.Count > 0)
                {
                    strJson += "\"data\":" + JsonClass.DataTable2Json(dt);
                }
                strJson += "}";
                context.Response.Write(strJson);
            }
            else if (action == "fee")//获取单据明细信息
            {
                string orderNo = context.Request.Params["orderNo"].ToString().Trim();
                string strJson = string.Empty;
                DataTable dt = SellOrderBus.GetFee(orderNo);
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
      
        
        string strMsg = string.Empty;
        string strFieldText = string.Empty;
        JsonClass JC;
        if (SellOrderBus.DelOrder(orderNos, out strMsg, out strFieldText))
            JC = new JsonClass(0, strFieldText, "", strMsg, 1);
        else
            JC = new JsonClass(0, strFieldText, "", strMsg, 0);

        context.Response.Write(JC.ToJosnString());
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
        string strorderNo = context.Request.Params["orderNo"].ToString().Trim();
        string strTitle = context.Request.Params["Title"].ToString().Trim();
        string strTotalPrice = context.Request.Params["TotalPrice"].ToString().Trim();
        string strTotalPrice1 = context.Request.Params["TotalPrice1"].ToString().Trim();
        string strFromType = context.Request.Params["FromType"].ToString().Trim();
        string strBillStatus = context.Request.Params["BillStatus"].ToString().Trim();
        string strCustID = context.Request.Params["CustID"].ToString().Trim();
        string strSeller = context.Request.Params["Seller"].ToString().Trim();
        string strisOpenbill = context.Request.Params["isOpenbill"].ToString().Trim();
        string strFlowStatus = context.Request.Params["FlowStatus"].ToString().Trim();
        string strFromBillID = context.Request.Params["FromBillID"].ToString().Trim();
        string strSendPro = context.Request.Params["SendPro"].ToString().Trim();
        //扩展属性条件
        string EFIndex = context.Request.Params["EFIndex"].ToString();
        string EFDesc = context.Request.Params["EFDesc"].ToString();
        string strProjectID = context.Request.Params["ProjectID"].ToString().Trim();

        string orderNo = strorderNo.Length == 0 ? null : strorderNo;
        string Title = strTitle.Length == 0 ? null : strTitle;
        decimal? TotalPrice = strTotalPrice.Length == 0 ? null : (decimal?)Convert.ToDecimal(strTotalPrice);
        decimal? TotalPrice1 = strTotalPrice1.Length == 0 ? null : (decimal?)Convert.ToDecimal(strTotalPrice1);
        string FromType = strFromType.Length == 0 ? null : strFromType;
        string BillStatus = strBillStatus.Length == 0 ? null : strBillStatus;
        int? CustID = strCustID.Length == 0 ? null : (int?)Convert.ToInt32(strCustID);
        int? Seller = strSeller.Length == 0 ? null : (int?)Convert.ToInt32(strSeller);
        string isOpenbill = strisOpenbill.Length == 0 ? null : strisOpenbill;
        int? FlowStatus = strFlowStatus.Length == 0 ? null : (int?)Convert.ToInt32(strFlowStatus);
        int? FromBillID = strFromBillID.Length == 0 ? null : (int?)Convert.ToInt32(strFromBillID);
        string SendPro = strSendPro.Length == 0 ? null : strSendPro;
        int? ProjectID = strProjectID.Length == 0 ? null : (int?)Convert.ToInt32(strProjectID);

        SellOrderModel model = new SellOrderModel();
        model.OrderNo = orderNo;
        model.Title = Title;
        model.TotalPrice = TotalPrice;
        model.FromType = FromType;
        model.FromBillID = FromBillID;
        model.BillStatus = BillStatus;
        model.CustID = CustID;
        model.Seller = Seller;
        model.isOpenbill = isOpenbill;
        model.ProjectID = ProjectID;

        dt = SellOrderBus.GetOrderList(model, TotalPrice1, SendPro, FlowStatus,EFIndex,EFDesc, pageIndex, pageCount, ord, ref TotalCount);
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
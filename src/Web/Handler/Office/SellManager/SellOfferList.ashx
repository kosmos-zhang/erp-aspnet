<%@ WebHandler Language="C#" Class="SellOfferList" %>

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

public class SellOfferList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                DataTable dt = SellOfferBus.GetOrderInfo(orderID);
                strJson = "{";
                if (dt.Rows.Count > 0)
                {
                    strJson += "\"data\":" + JsonClass.DataTable2Json(dt);
                }
                strJson += "}";
                context.Response.Write(strJson);
            }
            else if (action == "detail")//获取销售机会详细信息
            {
                string orderNo = context.Request.Params["orderNo"].ToString().Trim();
                string strJson = string.Empty;
                DataTable dt = SellOfferBus.GetOrderDetail(orderNo);
                strJson = "{";
                if (dt.Rows.Count > 0)
                {
                    strJson += "\"data\":" + JsonClass.DataTable2Json(dt);
                }
                strJson += "}";
                context.Response.Write(strJson);
            }

            else if (action == "his")//获取报价记录
            {
                string orderNo = context.Request.Params["orderNo"].ToString().Trim();
                string strJson = string.Empty;
                DataTable dt = SellOfferBus.GetSellOfferHistory(orderNo);
                strJson = "{";
                if (dt.Rows.Count > 0)
                {
                    strJson += "\"data\":" + JsonClass.DataTable2Json(dt);
                }
                strJson += "}";
                context.Response.Write(strJson);
            }
            else if (action == "hisDetail")//获取报价记录
            {
                string orderNo = context.Request.Params["orderNo"].ToString().Trim();
                int OfferTime = Convert.ToInt32( context.Request.Params["OfferTime"].ToString().Trim());
                string strJson = string.Empty;
                DataTable dt = SellOfferBus.GetSellOfferHistoryDetail(orderNo,OfferTime);
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
        if (SellOfferBus.DelOrder(orderNos,out strMsg,out strFieldText))
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
        SellGatheringModel sellGatheringModel = new SellGatheringModel();
        string ord = orderBy + " " + order;
        int totalCount = 0;

        //扩展属性条件
        string EFIndex = context.Request.Form["EFIndex"].ToString();
        string EFDesc = context.Request.Form["EFDesc"].ToString();

        DataTable dt = GetDataTable(EFIndex, EFDesc,context, pageIndex, pageCount, ord, ref totalCount);
        
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

    private DataTable GetDataTable(string EFIndex, string EFDesc, HttpContext context, int pageIndex, int pageCount, string ord, ref int TotalCount)
    {
        DataTable dt = new DataTable();
        string strOfferNo = context.Request.Params["OfferNo"].ToString().Trim();
        string strTitle = context.Request.Params["Title"].ToString().Trim();
        string strCustID = context.Request.Params["CustID"].ToString().Trim();
        string strSeller = context.Request.Params["Seller"].ToString().Trim();
        string strFromType = context.Request.Params["FromType"].ToString().Trim();
        string strBillStatus = context.Request.Params["BillStatus"].ToString().Trim();
        string strOfferDate = context.Request.Params["OfferDate"].ToString().Trim();
        string strOfferDate1 = context.Request.Params["OfferDate1"].ToString().Trim();
        string strFlowStatus = context.Request.Params["FlowStatus"].ToString().Trim();
        string strFromBillID = context.Request.Params["FromBillID"].ToString().Trim();


        string OfferNo = strOfferNo.Length == 0 ? null : strOfferNo; 
        string Title = strTitle.Length == 0 ? null : strTitle; 
        int? CustID = strCustID.Length == 0 ? null : (int?)Convert.ToInt32(strCustID);
        int? Seller = strSeller.Length == 0 ? null : (int?)Convert.ToInt32(strSeller);
        string FromType = strFromType.Length == 0 ? null : strFromType;
        string BillStatus = strBillStatus.Length == 0 ? null : strBillStatus;
        DateTime? OfferDate = strOfferDate.Length == 0 ? null : (DateTime?)Convert.ToDateTime(strOfferDate);
        DateTime? OfferDate1 = strOfferDate1.Length == 0 ? null : (DateTime?)Convert.ToDateTime(strOfferDate1);
        int? FlowStatus = strFlowStatus.Length == 0 ? null : (int?)Convert.ToInt32(strFlowStatus);
        int? FromBillID = strFromBillID.Length == 0 ? null : (int?)Convert.ToInt32(strFromBillID);
        
        SellOfferModel model = new SellOfferModel();
        model.BillStatus = BillStatus;
        model.CustID = CustID;
        model.FromBillID = FromBillID;
        model.FromType = FromType;
        model.OfferDate = OfferDate;
        model.OfferNo = OfferNo;
        model.Seller = Seller;
        model.Title = Title;
        model.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//临时存储当前登录人ID
        dt = SellOfferBus.GetOrderList(EFIndex, EFDesc,model, FlowStatus, OfferDate1, pageIndex, pageCount, ord, ref TotalCount);
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
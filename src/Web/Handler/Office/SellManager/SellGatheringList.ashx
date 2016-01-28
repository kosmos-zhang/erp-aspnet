<%@ WebHandler Language="C#" Class="SellGatheringList" %>

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

public class SellGatheringList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                DelGathering(context);
            }
            else if (action == "orderInfo")//获取单据详细信息
            {
                int orderID = Convert.ToInt32(context.Request.Params["orderID"].ToString().Trim());
                string strJson = string.Empty;
                DataTable dt = SellGatheringBus.GetOrderInfo(orderID);
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
    /// 删除报价单
    /// </summary>
    private void DelGathering(HttpContext context)
    {
        string strIds = context.Request.Form["GatheringNos"].ToString();//要删除的设备ID
        strIds = strIds.Remove(strIds.Length-1, 1);
        string strMsg = string.Empty;
        JsonClass JC;
        if (SellGatheringBus.DelSellGathering(strIds,out strMsg))
            JC = new JsonClass(0, "", "", strMsg, 1);
        else
            JC = new JsonClass(0, "", "", strMsg, 0);
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
        GetSellGatheringModel(sellGatheringModel,context);
        string PlanPrice0 = context.Request.Params["PlanPrice0"].ToString().Trim();      //计划回款金额  
        PlanPrice0 = PlanPrice0.Length == 0 ? null : PlanPrice0;
        int totalCount = 0;
        string ord = orderBy + " " + order;
        //扩展属性条件
        string EFIndex = context.Request.Params["EFIndex"].ToString();
        string EFDesc = context.Request.Params["EFDesc"].ToString();
        
        DataTable dt = SellGatheringBus.GetSellGathering(sellGatheringModel, PlanPrice0, EFIndex, EFDesc, pageIndex, pageCount, ord, ref totalCount);
      
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

    /// <summary>
    /// 获取实体
    /// </summary>
    /// <param name="strCode"></param>
    /// <param name="context"></param>
    private void GetSellGatheringModel(SellGatheringModel sellGatheringModel, HttpContext context)
    {
        string GatheringNo = context.Request.Params["OfferNo"].ToString().Trim();
        string Title = context.Request.Params["Title"].ToString().Trim();         //主题                      
        string CustID = context.Request.Params["CustID"].ToString().Trim();         //客户ID（关联客户信息表）                    
        string FromType = context.Request.Params["FromType"].ToString().Trim();       //源单类型（0无来源，1发货通知单，2销售订单） 
        string FromBillID = context.Request.Params["FromBillID"].ToString().Trim();     //源单ID                                                            
        string PlanPrice = context.Request.Params["PlanPrice"].ToString().Trim();      //计划回款金额   
                                   
        string GatheringTime = context.Request.Params["GatheringTime"].ToString().Trim();  //期次                                                                            
        string Seller = context.Request.Params["Seller"].ToString().Trim();         //业务员(对应员工表ID)                                                                         

        if (Title.Length != 0)
        {
            sellGatheringModel.Title = Title;//主题
        }
        if (GatheringNo.Length != 0)
        {
            sellGatheringModel.GatheringNo = GatheringNo;    //回款计划编号       
        }
        if (CustID.Length != 0)
        {
            sellGatheringModel.CustID = Convert.ToInt32(CustID);         //客户ID（关联客户信息表） 
        }
        if (FromType.Length != 0)
        {
            sellGatheringModel.FromType = FromType;       //源单类型（0无来源，1发货通知单，2销售订单）
        }
        if (FromBillID.Length != 0)
        {
            sellGatheringModel.FromBillID = Convert.ToInt32(FromBillID);     //源单ID
        }
        if (PlanPrice.Length != 0)
        {
            sellGatheringModel.PlanPrice = Convert.ToDecimal(PlanPrice);      //计划回款金额 
        }
        if (GatheringTime.Length != 0)
        {
            sellGatheringModel.GatheringTime = GatheringTime;  //期次  
        }
        if (Seller.Length != 0)
        {
            sellGatheringModel.Seller = Convert.ToInt32(Seller);         //业务员(对应员工表ID)  
        }                       
        
            sellGatheringModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码  
       
    }


    

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
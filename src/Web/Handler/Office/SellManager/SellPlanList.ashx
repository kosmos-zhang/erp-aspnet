<%@ WebHandler Language="C#" Class="SellPlanList" %>

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
using System.Text;

public class SellPlanList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                DataTable dt = SellPlanBus.GetOrderInfo(orderID);
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
                LoadDetailQuarter(context);
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
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

        //扩展属性条件
        string EFIndex = context.Request.Form["EFIndex"].ToString();
        string EFDesc = context.Request.Form["EFDesc"].ToString(); 
        
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        SellGatheringModel sellGatheringModel = new SellGatheringModel();
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = GetDataTable(EFIndex,EFDesc, context, pageIndex, pageCount, ord, ref totalCount);

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

    private DataTable GetDataTable(string EFIndex,string EFDesc,HttpContext context, int pageIndex, int pageCount, string ord, ref int TotalCount)
    {
        DataTable dt = new DataTable();
        string strPlanNo = context.Request.Params["PlanNo"].ToString().Trim();
        string strTitle = context.Request.Params["Title"].ToString().Trim();
        string strPlanType = context.Request.Params["PlanType"].ToString().Trim();
        string strBillStatus = context.Request.Params["BillStatus"].ToString().Trim();
        string strFlowStatus = context.Request.Params["FlowStatus"].ToString().Trim();

        string OfferNo = strPlanNo.Length == 0 ? null : strPlanNo;
        string Title = strTitle.Length == 0 ? null : strTitle;
        string PlanType = strPlanType.Length == 0 ? null : strPlanType;
        string BillStatus = strBillStatus.Length == 0 ? null : strBillStatus;
        int? FlowStatus = strFlowStatus.Length == 0 ? null : (int?)Convert.ToInt32(strFlowStatus);

        SellPlanModel model = new SellPlanModel();
        model.BillStatus = BillStatus;
        model.PlanType = PlanType;
        model.PlanNo = strPlanNo;
        model.Title = Title;
        dt = SellPlanBus.GetOrderList(EFIndex, EFDesc, model, FlowStatus, pageIndex, pageCount, ord, ref TotalCount);
        return dt;
    }

    private DataTable dtDetail = new DataTable();
    private void LoadDetailQuarter(HttpContext context)
    {
        string orderNo = context.Request.Params["orderNo"].ToString().Trim();

        dtDetail = SellPlanBus.GetOrderDetail(orderNo);

        StringBuilder sb = new StringBuilder();
        DataRow[] rows = dtDetail.Select("ParentID = 0");//赛选父节点为0的行

        sb.Append("[");
        foreach (DataRow row in rows)//循环每节点里的子节点
        {
            LoadQuarter(row, sb);
        }
        sb.Append("]");

        if (dtDetail.Rows.Count != 0)
        {
            context.Response.Write("{result:true,data:[{nodeType:1,text:\"销售计划明细\",value:\"0\",subNodes:" + sb.ToString() + "}]}");
        }
        else
        {
            context.Response.Write("{result:true,data:" + sb.ToString() + "}");
        }
    }

    private int nodeQuarter = 1;
    private void LoadQuarter(DataRow p, StringBuilder sb)
    {
        if (sb[sb.Length - 1] == '}')
            sb.Append(",");

        DataRow[] rows = dtDetail.Select("ParentID=" + p["id"].ToString());
        string nodeType = nodeQuarter.ToString();
        if (rows.Length == 0)
        {
            nodeType = "-1";
        }

        sb.Append("{");
        sb.Append("nodeType: 2,");
        if (p["IsSummarize"].ToString() == "0")
        {
            sb.Append("text:\"" + p["DetailTypeName"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "：" + p["DetailName"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "&nbsp;&nbsp;&nbsp;" + "最低目标额(元)：" + p["MinDetailotal"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n")
                + "&nbsp;&nbsp;&nbsp;" + "目标额(元)：" + p["DetailTotal"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "\"");
        }
        else if (p["IsSummarize"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") == "1")
        {
            sb.Append("text:\"" 
                    + p["DetailTypeName"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "：" + p["DetailName"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "&nbsp;&nbsp;&nbsp;" + "最低目标额(元)：" + p["MinDetailotal"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n")
                    + "&nbsp;&nbsp;&nbsp;" + "目标额(元)：" + p["DetailTotal"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "&nbsp;&nbsp;&nbsp;" + p["AddOrCutText"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n")
                    + "&nbsp;&nbsp;&nbsp;目标达成率：" + p["CompletePercent"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n").Trim()+"%"
                    + "\"");
        }
        sb.Append(",value:\"" 
                + p["id"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "|" + p["ParentID"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "|" + p["DetailType"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n")
                + "|" + p["DetailID"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "|" + p["DetailTotal"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "|" + p["MinDetailotal"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") 
                + "|" + p["DetailName"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "|" + nodeQuarter.ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "|" + p["IsSummarize"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n")
                + "|" + p["SummarizeDate"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "|" + p["SummarizeNote"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "|" + p["AimRealResult"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n")
                + "|" + p["AddOrCut"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "|" + p["Difference"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n") + "|" + p["CompletePercent"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n")
                + "|" + p["SummarizerName"].ToString().Replace("\"","\\\"").Replace("\n","\\r\\n")
                + "\"");
        sb.Append(",subNodes:[");


        nodeQuarter++;

        foreach (DataRow row in rows)
        {
            LoadQuarter(row, sb);
        }
        nodeQuarter--;

        sb.Append("]}");
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
        if (SellPlanBus.DelOrder(orderNos, out strMsg, out strFieldText))
            JC = new JsonClass(0, strFieldText, "", strMsg, 1);
        else
            JC = new JsonClass(0, strFieldText, "", strMsg, 0);

        context.Response.Write(JC.ToJosnString());
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
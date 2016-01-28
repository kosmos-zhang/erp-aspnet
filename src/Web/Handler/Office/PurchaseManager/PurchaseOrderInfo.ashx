<%@ WebHandler Language="C#" Class="PurchaseOrderInfo" %>

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

public class PurchaseOrderInfo : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        
        
        if (context.Request.RequestType == "POST")
        {
            string ActionOrder = context.Request.QueryString["ActionOrder"];
            if (ActionOrder == "Delete")
            {
                JsonClass jc;
                string IDs = context.Request.QueryString["IDs"];
                string OrderNos = context.Request.QueryString["OrderNos"];

                if (PurchaseOrderBus.DeletePurchaseOrder(IDs,OrderNos) == true)
                {
                    jc = new JsonClass("success", "", 1);
                    context.Response.Write(jc);
                    return;
                }
                else
                {

                }
            }
            else if (ActionOrder == "Select")
            {
                string orderString = (context.Request.QueryString["orderBy"]);//排序
                string order = "asc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ConfirmDate";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                {
                    order = "desc";//排序：降序
                }
                orderBy = orderBy + " " + order;
                int pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
                int totalCount = 0;

                PurchaseOrderModel PurchaseOrderM = GetPurchaseOrderMM(context.Request);
                DataTable dt = PurchaseOrderBus.GetPurchaseOrderAnaylise(PurchaseOrderM, pageIndex, pageCount, orderBy, ref totalCount);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt.Rows.Count >0)
                {
                  sb.Append(JsonClass.DataTable2Json(dt));
                }
                else
                {
                  sb.Append("\"\"");    
                }
                sb.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
            }
            else
            {
                string orderString = (context.Request.QueryString["orderBy"]);//排序
                string order = "asc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                {
                    order = "desc";//排序：降序
                }
                orderBy = orderBy + " " + order;
                int pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
                int totalCount = 0;
                
                PurchaseOrderModel PurchaseOrderM = GetPurchaseOrderM(context.Request);
                DataTable dt =PurchaseOrderBus.GetPurchaseOrder(PurchaseOrderM,pageIndex,pageCount,orderBy,ref totalCount);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt.Rows.Count >0)
                {
                  sb.Append(JsonClass.DataTable2Json(dt));
                }
                else
                {
                  sb.Append("\"\"");    
                }
                sb.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
            }
        }
    }
 
    public bool IsReusable 
    {
        get 
        {
            return false;
        }
    }
    /// <summary>
    /// datatabletoxml
    /// </summary>
    /// <param name="xmlDS"></param>
    /// <returns></returns>
    private XElement ConvertDataTableToXML(DataTable xmlDS)
    {
        StringWriter sr = new StringWriter();
        xmlDS.TableName = "Data";
        xmlDS.WriteXml(sr, System.Data.XmlWriteMode.IgnoreSchema, true);
        string contents = sr.ToString();
        return XElement.Parse(contents);
    }

    public static string ToJSON(object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(obj);
    }
    //数据源结构
    public class DataSourceModel
    {
        public string ID { get; set; }
        public string OrderNo { get; set; }
        public string OrderTitle { get; set; }
        public string TypeID { get; set; }
        public string TypeName { get; set; }
        public string PurchaserID { get; set; }
        public string PurchaserName { get; set; }
        public string ProviderID { get; set; }
        public string ProviderName { get; set; }
        public string TotalPrice { get; set; }
        public string TotalTax { get; set; }
        public string TotalFee { get; set; }
        public string BillStatus { get; set; }
        public string BillStatusName { get; set; }
        public string FlowStatusName { get; set; }
        //public string IsCite { get; set; }
    }
    private PurchaseOrderModel GetPurchaseOrderM(HttpRequest request)
    {
        PurchaseOrderModel PurchaseOrderM = new PurchaseOrderModel();
        PurchaseOrderM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        PurchaseOrderM.OrderNo = request.QueryString["No"];
        PurchaseOrderM.Title = request.QueryString["Title"]; 
        PurchaseOrderM.TypeID = request.QueryString["TypeID"];
        PurchaseOrderM.DeptID = request.QueryString["DeptID"];
        PurchaseOrderM.Purchaser = request.QueryString["PurchaseID"];
        PurchaseOrderM.FromType = request.QueryString["FromType"];
        PurchaseOrderM.ProviderID = request.QueryString["ProviderID"];
        PurchaseOrderM.BillStatus = request.QueryString["BillStatus"];
        PurchaseOrderM.FlowStatus = request.QueryString["FlowStatus"];
        PurchaseOrderM.EFIndex = request.QueryString["EFIndex"];
        PurchaseOrderM.EFDesc = request.QueryString["EFDesc"];
        PurchaseOrderM.ProjectID = request.QueryString["ProjectID"];

        return PurchaseOrderM;
    }

    private PurchaseOrderModel GetPurchaseOrderMM(HttpRequest request)
    {
        PurchaseOrderModel PurchaseOrderM = new PurchaseOrderModel();
        PurchaseOrderM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
         PurchaseOrderM.ProviderID = request.QueryString["ProviderID"];
    
         PurchaseOrderM.OrderNo = request.QueryString["ProductNo"]; 
        PurchaseOrderM.Title = request.QueryString["StartDate"];
        PurchaseOrderM.TypeID = request.QueryString["EndDate"]; 
        PurchaseOrderM.DeptID = request.QueryString["ProductName"]; 
   
        return PurchaseOrderM;
    }
}
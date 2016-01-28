<%@ WebHandler Language="C#" Class="PurchaseAskPriceInfo" %>

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

public class PurchaseAskPriceInfo : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        if (context.Request.RequestType == "POST")
        {
            string ActionAsk = context.Request.Params["ActionAsk"];
            if (ActionAsk == "Select")
            {//检索
                string orderString = (context.Request.Params["orderBy"]);//排序
                string order = "asc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                {
                    order = "desc";//排序：降序
                }
                orderBy = orderBy + " " + order;
                int pageCount = int.Parse(context.Request.Params["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Params["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
                
                int totalCount = 0;
                
                PurchaseAskPriceModel PurchaseAskPriceM = GetPurchaseAskPriceM(context.Request);
                DataTable dt = PurchaseAskPriceBus.GetPurchaseAskPrice(PurchaseAskPriceM, pageIndex, pageCount, orderBy, ref totalCount);
                //XElement dsXML = ConvertDataTableToXML(PurchaseAskPriceBus.GetPurchaseAskPrice(PurchaseAskPriceM));
                ////linq排序
                //var dsLinq =
                //    (order == "ascending") ?
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value ascending
                //     select new DataSourceModel()
                //     {
                //         ID = x.Element("ID").Value,
                //         AskNo = x.Element("AskNo").Value,
                //         AskTitle = x.Element("AskTitle").Value,
                //         ProviderID = x.Element("ProviderID").Value,
                //         ProviderName = x.Element("ProviderName").Value,
                //         AskDate = x.Element("AskDate").Value,
                //         AskUserID = x.Element("AskUserID").Value,
                //         AskUserName = x.Element("AskUserName").Value,
                //         AskOrder = x.Element("AskOrder").Value,
                //         TotalPrice = x.Element("TotalPrice").Value,
                //         TotalTax = x.Element("TotalTax").Value,
                //         TotalFee = x.Element("TotalFee").Value,
                //         BillStatus = x.Element("BillStatus").Value,
                //         BillStatusName = x.Element("BillStatusName").Value,
                //         FlowStatus = x.Element("FlowStatus").Value,
                //         FlowStatusName = x.Element("FlowStatusName").Value,
                //         //IsCite = x.Element("IsCite").Value,
                //     })
                //              :
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value descending
                //     select new DataSourceModel()
                //     {
                //         ID = x.Element("ID").Value,
                //         AskNo = x.Element("AskNo").Value,
                //         AskTitle = x.Element("AskTitle").Value,
                //         ProviderID = x.Element("ProviderID").Value,
                //         ProviderName = x.Element("ProviderName").Value,
                //         AskDate = x.Element("AskDate").Value,
                //         AskUserID = x.Element("AskUserID").Value,
                //         AskUserName = x.Element("AskUserName").Value,
                //         AskOrder = x.Element("AskOrder").Value,
                //         TotalPrice = x.Element("TotalPrice").Value,
                //         TotalTax = x.Element("TotalTax").Value,
                //         TotalFee = x.Element("TotalFee").Value,
                //         BillStatus = x.Element("BillStatus").Value,
                //         BillStatusName = x.Element("BillStatusName").Value,
                //         FlowStatus = x.Element("FlowStatus").Value,
                //         FlowStatusName = x.Element("FlowStatusName").Value,
                //         //IsCite = x.Element("IsCite").Value,
                //     });
                //int totalCount = dsLinq.Count();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt.Rows.Count > 0)
                {
                    sb.Append(JsonClass.DataTable2Json(dt));
                }
                else
                {
                    sb.Append("\"\"");
                }
                //sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
                sb.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
            }
            //else if (ActionAsk == "History")
            //{
            //    string orderString = (context.Request.Params["orderBy"]);//排序
            //    string order = "ascending";//排序：升序
            //    string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            //    if (orderString.EndsWith("_d"))
            //    {
            //        order = "descending";//排序：降序
            //    }
            //    int pageCount = int.Parse(context.Request.Params["pageCount"].ToString());//每页显示记录数
            //    int pageIndex = int.Parse(context.Request.Params["pageIndex"].ToString());//当前页
            //    int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            //    string ID = context.Request.Params["ID"];
            //    string AskNo = context.Request.Params["AskNo"];
            //    string AskOrder = context.Request.Params["AskOrder"];
                
            //    XElement dsXML = ConvertDataTableToXML(PurchaseAskPriceBus.GetPurAskPriceHistory(ID,AskNo,AskOrder));
            //    //linq排序
            //    var dsLinq =
            //        (order == "ascending") ?
            //        (from x in dsXML.Descendants("Data")
            //         orderby x.Element(orderBy).Value ascending
            //         select new DataSourceHistory()
            //         {
            //             ID = x.Element("ID").Value,
            //             AskNo = x.Element("AskNo").Value,
            //             AskOrder = x.Element("AskOrder").Value,
            //             AskDate = x.Element("AskDate").Value,
            //             AskUserID = x.Element("AskUserID").Value,
            //             CountTotal = x.Element("CountTotal").Value,
            //             TotalPrice = x.Element("TotalPrice").Value,
            //             TotalTax = x.Element("TotalTax").Value,
            //             TotalFee = x.Element("TotalFee").Value,
            //             Discount = x.Element("Discount").Value,
            //             DiscountTotal = x.Element("DiscountTotal").Value,
            //             RealTotal = x.Element("RealTotal").Value,
            //             isAddTax = x.Element("isAddTax").Value,
            //             ProductID = x.Element("ProductID").Value,
            //             ProductNo = x.Element("ProductNo").Value,
            //             ProductName = x.Element("ProductName").Value,
            //             Specification = x.Element("Specification").Value,
            //             ProductCount = x.Element("ProductCount").Value,
            //             UnitID = x.Element("UnitID").Value,
            //             UnitName = x.Element("UnitName").Value,
            //             DiscountDetail = x.Element("DiscountDetail").Value,
            //             TaxRate = x.Element("TaxRate").Value,
            //             TaxPrice = x.Element("TaxPrice").Value,
            //             TotalFeeDetail = x.Element("TotalFeeDetail").Value,
            //             TotalPriceDetail = x.Element("TotalPriceDetail").Value,
            //             TotalTaxDetail = x.Element("TotalTaxDetail").Value,
            //             RequireDate = x.Element("RequireDate").Value,
            //             UnitPrice = x.Element("UnitPrice").Value,
            //             Remark = x.Element("Remark").Value,

            //         })
            //                  :
            //        (from x in dsXML.Descendants("Data")
            //         orderby x.Element(orderBy).Value descending
            //         select new DataSourceHistory()
            //         {
            //             ID = x.Element("ID").Value,
            //             AskNo = x.Element("AskNo").Value,
            //             AskOrder = x.Element("AskOrder").Value,
            //             AskDate = x.Element("AskDate").Value,
            //             AskUserID = x.Element("AskUserID").Value,
            //             CountTotal = x.Element("CountTotal").Value,
            //             TotalPrice = x.Element("TotalPrice").Value,
            //             TotalTax = x.Element("TotalTax").Value,
            //             TotalFee = x.Element("TotalFee").Value,
            //             Discount = x.Element("Discount").Value,
            //             DiscountTotal = x.Element("DiscountTotal").Value,
            //             RealTotal = x.Element("RealTotal").Value,
            //             isAddTax = x.Element("isAddTax").Value,
            //             ProductID = x.Element("ProductID").Value,
            //             ProductNo = x.Element("ProductNo").Value,
            //             ProductName = x.Element("ProductName").Value,
            //             Specification = x.Element("Specification").Value,
            //             ProductCount = x.Element("ProductCount").Value,
            //             UnitID = x.Element("UnitID").Value,
            //             UnitName = x.Element("UnitName").Value,
            //             DiscountDetail = x.Element("DiscountDetail").Value,
            //             TaxRate = x.Element("TaxRate").Value,
            //             TaxPrice = x.Element("TaxPrice").Value,
            //             TotalFeeDetail = x.Element("TotalFeeDetail").Value,
            //             TotalPriceDetail = x.Element("TotalPriceDetail").Value,
            //             TotalTaxDetail = x.Element("TotalTaxDetail").Value,
            //             RequireDate = x.Element("RequireDate").Value,
            //             UnitPrice = x.Element("UnitPrice").Value,
            //             Remark = x.Element("Remark").Value,

            //         });
            //    int totalCount = dsLinq.Count();
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("{");
            //    sb.Append("totalCount:");
            //    sb.Append(totalCount.ToString());
            //    sb.Append(",data:");
            //    sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
            //    sb.Append("}");

            //    context.Response.ContentType = "text/plain";
            //    context.Response.Write(sb.ToString());
            //    context.Response.End();
            //}
            else if (ActionAsk == "Delete")
            {
                JsonClass jc;
                string IDs = context.Request.Params["IDs"];
                string AskNos = context.Request.Params["AskNos"];

                if (PurchaseAskPriceBus.DeletePurAsk(IDs,AskNos) == true)
                {
                    jc = new JsonClass("success", "", 1);
                    context.Response.Write(jc);
                }
                else
                {

                }
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
        public string AskNo { get; set; }
        public string AskTitle { get; set; }
        public string ProviderID { get; set; }
        public string ProviderName { get; set; }
        public string AskDate { get; set; }
        public string AskUserID { get; set; }
        public string AskUserName { get; set; }
        public string AskOrder { get; set; }
        public string TotalPrice { get; set; }
        public string TotalTax { get; set; }
        public string TotalFee { get; set; }
        public string BillStatus { get; set; }
        public string BillStatusName { get; set; }
        public string FlowStatus { get; set; }
        public string FlowStatusName { get; set; }
        //public string IsCite { get; set; }
        
    }
    public class DataSourceHistory
    {
        public string ID { get; set; }
        public string AskNo { get; set; }
        public string AskOrder { get; set; }
        public string AskDate { get; set; }
        public string AskUserID { get; set; }
        public string CountTotal { get; set; }
        public string TotalPrice { get; set; }
        public string TotalTax { get; set; }
        public string TotalFee { get; set; }
        public string Discount { get; set; }
        public string DiscountTotal { get; set; }
        public string RealTotal { get; set; }
        public string isAddTax { get; set; }
        public string ProductID { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string Specification { get; set; }
        public string ProductCount { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string DiscountDetail { get; set; }
        public string TaxRate { get; set; }
        public string TaxPrice { get; set; }
        public string TotalFeeDetail { get; set; }
        public string TotalPriceDetail { get; set; }
        public string TotalTaxDetail { get; set; }
        public string RequireDate { get; set; }
        public string UnitPrice { get; set; }
        public string Remark { get; set; }

    }
    private PurchaseAskPriceModel GetPurchaseAskPriceM(HttpRequest request)
    {
        PurchaseAskPriceModel PurchaseAskPriceM = new PurchaseAskPriceModel();
        PurchaseAskPriceM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        PurchaseAskPriceM.AskNo = request.Params["No"];
        PurchaseAskPriceM.AskTitle = request.Params["Title"];
        PurchaseAskPriceM.FromType = request.Params["FromType"];
        PurchaseAskPriceM.DeptID = request.Params["AskDeptID"];
        PurchaseAskPriceM.AskUserID = request.Params["AskUserID"];
        PurchaseAskPriceM.BillStatus = request.Params["BillStatus"];
        PurchaseAskPriceM.ProviderID = request.Params["ProviderID"];
        PurchaseAskPriceM.FlowStatus = request.Params["FlowStatus"];
        PurchaseAskPriceM.AskDate = request.Params["StartAskDate"];
        PurchaseAskPriceM.EndAskDate = request.Params["EndAskDate"];

        PurchaseAskPriceM.EFDesc = request.Params["EFDesc"];
        PurchaseAskPriceM.EFIndex = request.Params["EFIndex"];
        

        return PurchaseAskPriceM;
    }
}
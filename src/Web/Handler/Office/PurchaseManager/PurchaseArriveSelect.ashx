<%@ WebHandler Language="C#" Class="PurchaseArriveSelect" %>

using System;
using System.Web;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using System.Web.SessionState;

public class PurchaseArriveSelect : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string orderString = (context.Request.Form["orderbyPurchaseArrive"].ToString());//排序
            string order = "ascending";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCountPurchaseOrder"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndexPurchaseOrder"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

            int Provider00ID = int.Parse(context.Request.Form["Provider00ID"].ToString());
            int CurrencyTypeID = int.Parse(context.Request.Form["CurrencyTypeID"].ToString());
            string Rate = context.Request.Form["Rate"].ToString();
            string ProductNo = context.Request.Form["ProductNo"].ToString();
            string ProductName = context.Request.Form["ProductName"].ToString();
            string FromBillNo = context.Request.Form["FromBillNo"].ToString();
            string PurchaseArriveEFDesc = context.Request.Form["PurchaseArriveEFDesc"].ToString();
            string PurchaseArriveEFIndex = context.Request.Form["PurchaseArriveEFIndex"].ToString();
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码

            DataTable dt = PurchaseRejectBus.GetPurchaseRejectDetail(ProductNo, ProductName, FromBillNo, CompanyCD, Provider00ID, CurrencyTypeID, Rate, PurchaseArriveEFIndex, PurchaseArriveEFDesc);
            XElement dsXML = ConvertDataTableToXML(dt);
            //linq排序
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     ID = x.Element("ID") == null ? string.Empty : x.Element("ID").Value,
                     ProductID = x.Element("ProductID") == null ? string.Empty : x.Element("ProductID").Value,
                     ProductNo = x.Element("ProductNo") == null ? string.Empty : x.Element("ProductNo").Value,
                     ProductName = x.Element("ProductName") == null ? string.Empty : x.Element("ProductName").Value,
                     standard = x.Element("standard") == null ? string.Empty : x.Element("standard").Value,
                     ColorName = x.Element("ColorName") == null ? string.Empty : x.Element("ColorName").Value,
                     UnitID = x.Element("UnitID") == null ? string.Empty : x.Element("UnitID").Value,
                     UnitName = x.Element("UnitName") == null ? string.Empty : x.Element("UnitName").Value,
                     ProductCount = x.Element("ProductCount") == null ? string.Empty : x.Element("ProductCount").Value,
                     UnitPrice = x.Element("UnitPrice") == null ? string.Empty : x.Element("UnitPrice").Value,
                     TaxPrice = x.Element("TaxPrice") == null ? string.Empty : x.Element("TaxPrice").Value,
                     Discount = x.Element("Discount") == null ? string.Empty : x.Element("Discount").Value,
                     TaxRate = x.Element("TaxRate") == null ? string.Empty : x.Element("TaxRate").Value,
                     TotalPrice = x.Element("TotalPrice") == null ? string.Empty : x.Element("TotalPrice").Value,
                     TotalFee = x.Element("TotalFee") == null ? string.Empty : x.Element("TotalFee").Value,
                     TotalTax = x.Element("TotalTax") == null ? string.Empty : x.Element("TotalTax").Value,
                     Remark = x.Element("Remark") == null ? string.Empty : x.Element("Remark").Value,
                     FromBillID = x.Element("FromBillID") == null ? string.Empty : x.Element("FromBillID").Value,
                     FromBillNo = x.Element("FromBillNo") == null ? string.Empty : x.Element("FromBillNo").Value,
                     FromLineNo = x.Element("FromLineNo") == null ? string.Empty : x.Element("FromLineNo").Value,
                     BackCount = x.Element("BackCount") == null ? string.Empty : x.Element("BackCount").Value,
                     ProviderID = x.Element("ProviderID") == null ? string.Empty : x.Element("ProviderID").Value,
                     ProviderName = x.Element("ProviderName") == null ? string.Empty : x.Element("ProviderName").Value,
                     CurrencyType = x.Element("CurrencyType") == null ? string.Empty : x.Element("CurrencyType").Value,
                     CurrencyTypeName = x.Element("CurrencyTypeName") == null ? string.Empty : x.Element("CurrencyTypeName").Value,
                     Rate = x.Element("Rate") == null ? string.Empty : x.Element("Rate").Value,
                     InCount = x.Element("InCount") == null ? string.Empty : x.Element("InCount").Value,
                     UsedUnitID = x.Element("UsedUnitID") == null ? "" : x.Element("UsedUnitID").Value,
                     UsedUnitCount = x.Element("UsedUnitCount") == null ? "" : x.Element("UsedUnitCount").Value,
                     UsedUnitName = x.Element("UsedUnitName") == null ? "" : x.Element("UsedUnitName").Value,
                     UsedPrice = x.Element("UsedPrice") == null ? "" : x.Element("UsedPrice").Value,
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     ID = x.Element("ID") == null ? string.Empty : x.Element("ID").Value,
                     ProductID = x.Element("ProductID") == null ? string.Empty : x.Element("ProductID").Value,
                     ProductNo = x.Element("ProductNo") == null ? string.Empty : x.Element("ProductNo").Value,
                     ProductName = x.Element("ProductName") == null ? string.Empty : x.Element("ProductName").Value,
                     standard = x.Element("standard") == null ? string.Empty : x.Element("standard").Value,
                     ColorName = x.Element("ColorName") == null ? string.Empty : x.Element("ColorName").Value,
                     UnitID = x.Element("UnitID") == null ? string.Empty : x.Element("UnitID").Value,
                     UnitName = x.Element("UnitName") == null ? string.Empty : x.Element("UnitName").Value,
                     ProductCount = x.Element("ProductCount") == null ? string.Empty : x.Element("ProductCount").Value,
                     UnitPrice = x.Element("UnitPrice") == null ? string.Empty : x.Element("UnitPrice").Value,
                     TaxPrice = x.Element("TaxPrice") == null ? string.Empty : x.Element("TaxPrice").Value,
                     Discount = x.Element("Discount") == null ? string.Empty : x.Element("Discount").Value,
                     TaxRate = x.Element("TaxRate") == null ? string.Empty : x.Element("TaxRate").Value,
                     TotalPrice = x.Element("TotalPrice") == null ? string.Empty : x.Element("TotalPrice").Value,
                     TotalFee = x.Element("TotalFee") == null ? string.Empty : x.Element("TotalFee").Value,
                     TotalTax = x.Element("TotalTax") == null ? string.Empty : x.Element("TotalTax").Value,
                     Remark = x.Element("Remark") == null ? string.Empty : x.Element("Remark").Value,
                     FromBillID = x.Element("FromBillID") == null ? string.Empty : x.Element("FromBillID").Value,
                     FromBillNo = x.Element("FromBillNo") == null ? string.Empty : x.Element("FromBillNo").Value,
                     FromLineNo = x.Element("FromLineNo") == null ? string.Empty : x.Element("FromLineNo").Value,
                     BackCount = x.Element("BackCount") == null ? string.Empty : x.Element("BackCount").Value,
                     ProviderID = x.Element("ProviderID") == null ? string.Empty : x.Element("ProviderID").Value,
                     ProviderName = x.Element("ProviderName") == null ? string.Empty : x.Element("ProviderName").Value,
                     CurrencyType = x.Element("CurrencyType") == null ? string.Empty : x.Element("CurrencyType").Value,
                     CurrencyTypeName = x.Element("CurrencyTypeName") == null ? string.Empty : x.Element("CurrencyTypeName").Value,
                     Rate = x.Element("Rate") == null ? string.Empty : x.Element("Rate").Value,
                     InCount = x.Element("InCount") == null ? string.Empty : x.Element("InCount").Value,
                     UsedUnitID = x.Element("UsedUnitID") == null ? "" : x.Element("UsedUnitID").Value,
                     UsedUnitCount = x.Element("UsedUnitCount") == null ? "" : x.Element("UsedUnitCount").Value,
                     UsedUnitName = x.Element("UsedUnitName") == null ? "" : x.Element("UsedUnitName").Value,
                     UsedPrice = x.Element("UsedPrice") == null ? "" : x.Element("UsedPrice").Value,
                 });
            int totalCount = dsLinq.Count();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(totalCount.ToString());
            sb.Append(",data:");
            sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
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

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    //数据源结构
    public class DataSourceModel
    {
        public string ID { get; set; }
        public string ProductID { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string standard { get; set; }
        public string ColorName { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string ProductCount { get; set; }
        public string UnitPrice { get; set; }
        public string TaxPrice { get; set; }
        public string Discount { get; set; }
        public string TaxRate { get; set; }
        public string TotalPrice { get; set; }
        public string TotalFee { get; set; }
        public string TotalTax { get; set; }
        public string Remark { get; set; }
        public string FromBillID { get; set; }
        public string FromBillNo { get; set; }
        public string FromLineNo { get; set; }
        public string BackCount { get; set; }
        public string ProviderID { get; set; }
        public string ProviderName { get; set; }
        public string CurrencyType { get; set; }
        public string CurrencyTypeName { get; set; }
        public string Rate { get; set; }
        public string InCount { get; set; }
        public string UsedUnitID { get; set; }
        public string UsedUnitName { get; set; }
        public string UsedUnitCount { get; set; }
        public string UsedPrice { get; set; }
    }

}
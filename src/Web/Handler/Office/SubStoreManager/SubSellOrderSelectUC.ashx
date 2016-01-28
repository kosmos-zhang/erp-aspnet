<%@ WebHandler Language="C#" Class="SubSellOrderSelectUC" %>

using System;
using System.Web;
using XBase.Model.Office.SubStoreManager;
using XBase.Business.Office.SubStoreManager;
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

public class SubSellOrderSelectUC : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string orderString = (context.Request.Form["orderbySubSellOrder"].ToString());//排序
            string order = "ascending";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCountSubSellBack"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndexSubSellBack"].ToString());//当前页
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

            string OrderNo = context.Request.Form["OrderNo"].ToString();
            int CurrencyTypeID = int.Parse(context.Request.Form["CurrencyTypeID"].ToString());
            string Rate = context.Request.Form["Rate"].ToString();
            DataTable dt = SubSellBackDBHelper.GetSubSellBackDetailUC(OrderNo, CurrencyTypeID, Rate, CompanyCD);
            XBase.Common.StringUtil.DecimalFormatPoint(int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint), dt);
            XElement dsXML = ConvertDataTableToXML(dt);
            //linq排序
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     ID = x.Element("ID").Value,
                     DeptID = x.Element("DeptID").Value,
                     DeptName = x.Element("DeptName").Value,
                     OrderNo = x.Element("OrderNo").Value,
                     FromLineNo = x.Element("FromLineNo").Value,
                     FromBillNo = x.Element("FromBillNo").Value,
                     FromBillID = x.Element("FromBillID").Value,
                     ProductID = x.Element("ProductID").Value,
                     ProductNo = x.Element("ProductNo").Value,
                     ProductName = x.Element("ProductName").Value,
                     standard = x.Element("standard").Value,
                     UnitID = x.Element("UnitID").Value,
                     UnitName = x.Element("UnitName").Value,
                     UnitPrice = x.Element("UnitPrice").Value,
                     TaxPrice = x.Element("TaxPrice").Value,
                     Discount = x.Element("Discount").Value,
                     TaxRate = x.Element("TaxRate").Value,
                     StorageID = x.Element("StorageID").Value,
                     StorageName = x.Element("StorageName").Value,
                     ProductCount = x.Element("ProductCount").Value,
                     Remark = x.Element("Remark").Value,
                     YBackCount = x.Element("YBackCount").Value,
                     UsedUnitID = x.Element("UsedUnitID") == null ? "" : x.Element("UsedUnitID").Value,
                     UsedUnitName = x.Element("UsedUnitName") == null ? "" : x.Element("UsedUnitName").Value,
                     UsedUnitCount = x.Element("UsedUnitCount") == null ? "" : x.Element("UsedUnitCount").Value,
                     UsedPrice = x.Element("UsedPrice") == null ? "" : x.Element("UsedPrice").Value,
                     ExRate = x.Element("ExRate") == null ? "" : x.Element("ExRate").Value,
                     BatchNo = x.Element("BatchNo") == null ? "" : x.Element("BatchNo").Value
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     ID = x.Element("ID").Value,
                     DeptID = x.Element("DeptID").Value,
                     DeptName = x.Element("DeptName").Value,
                     OrderNo = x.Element("OrderNo").Value,
                     FromLineNo = x.Element("FromLineNo").Value,
                     FromBillNo = x.Element("FromBillNo").Value,
                     FromBillID = x.Element("FromBillID").Value,
                     ProductID = x.Element("ProductID").Value,
                     ProductNo = x.Element("ProductNo").Value,
                     ProductName = x.Element("ProductName").Value,
                     standard = x.Element("standard").Value,
                     UnitID = x.Element("UnitID").Value,
                     UnitName = x.Element("UnitName").Value,
                     UnitPrice = x.Element("UnitPrice").Value,
                     TaxPrice = x.Element("TaxPrice").Value,
                     Discount = x.Element("Discount").Value,
                     TaxRate = x.Element("TaxRate").Value,
                     StorageID = x.Element("StorageID").Value,
                     StorageName = x.Element("StorageName").Value,
                     ProductCount = x.Element("ProductCount").Value,
                     Remark = x.Element("Remark").Value,
                     YBackCount = x.Element("YBackCount").Value,
                     UsedUnitID = x.Element("UsedUnitID") == null ? "" : x.Element("UsedUnitID").Value,
                     UsedUnitName = x.Element("UsedUnitName") == null ? "" : x.Element("UsedUnitName").Value,
                     UsedUnitCount = x.Element("UsedUnitCount") == null ? "" : x.Element("UsedUnitCount").Value,
                     UsedPrice = x.Element("UsedPrice") == null ? "" : x.Element("UsedPrice").Value,
                     ExRate = x.Element("ExRate") == null ? "" : x.Element("ExRate").Value,
                     BatchNo = x.Element("BatchNo") == null ? "" : x.Element("BatchNo").Value
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
        public string DeptID { get; set; }
        public string DeptName { get; set; }
        public string OrderNo { get; set; }
        public string FromLineNo { get; set; }
        public string FromBillNo { get; set; }
        public string FromBillID { get; set; }
        public string ProductID { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string standard { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string UnitPrice { get; set; }
        public string TaxPrice { get; set; }
        public string Discount { get; set; }
        public string TaxRate { get; set; }
        public string StorageID { get; set; }
        public string StorageName { get; set; }
        public string ProductCount { get; set; }
        public string Remark { get; set; }
        public string YBackCount { get; set; }
        public string UsedUnitID { get; set; }
        public string UsedUnitName { get; set; }
        public string UsedUnitCount { get; set; }
        public string UsedPrice { get; set; }
        public string ExRate { get; set; }
        public string BatchNo { get; set; }
    }

}
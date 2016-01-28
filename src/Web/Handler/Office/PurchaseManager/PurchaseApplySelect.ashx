<%@ WebHandler Language="C#" Class="PurchaseApplySelect" %>

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
using XBase.Model.Office.SellManager;
using System.Web.SessionState;

public class PurchaseApplySelect : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string orderString = (context.Request.Form["orderbyBillChoose"].ToString());//排序
            string order = "ascending";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCountBillChoose"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndexBillChoose"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

            int Provider00ID = int.Parse(context.Request.Form["Provider00ID"].ToString());

            string ProductNo = context.Request.Form["ProductNo"].ToString();
            string ProductName = context.Request.Form["ProductName"].ToString();
            string FromBillNo = context.Request.Form["FromBillNo"].ToString();
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码

            DataTable sd = PurchaseContractBus.GetApplySelectDetail(Provider00ID, ProductNo, ProductName, FromBillNo, CompanyCD);
            XElement dsXML = ConvertDataTableToXML(sd);
            //linq排序
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     ID = x.Element("ID").Value,
                     ProductID = x.Element("ProductID").Value,
                     ProductNo = x.Element("ProductNo").Value,
                     ProductName = x.Element("ProductName").Value,
                     standard = x.Element("standard").Value,
                     UnitID = x.Element("UnitID").Value,
                     Unit = x.Element("Unit").Value,
                     ProductCount = x.Element("ProductCount").Value,
                     UnitPrice = x.Element("UnitPrice").Value,
                     TaxPrice = x.Element("TaxPrice").Value,
                     Discount = x.Element("Discount").Value,
                     TaxRate = x.Element("TaxRate").Value,
                     RequireDate = x.Element("RequireDate").Value,
                     ApplyReasonID = x.Element("ApplyReasonID").Value,
                     ApplyReason = x.Element("ApplyReason").Value,
                     FromBillID = x.Element("FromBillID").Value,
                     FromBillNo = x.Element("FromBillNo").Value,
                     FromLineNo = x.Element("FromLineNo").Value,
                     TypeID = x.Element("TypeID").Value,
                     Seller = x.Element("Seller").Value,
                     SellerName = x.Element("SellerName").Value,
                     DeptID = x.Element("DeptID").Value,
                     DeptIDName = x.Element("DeptIDName").Value,
                     UsedUnitCount = x.Element("UsedUnitCount").Value,
                     UsedUnitID = x.Element("UsedUnitID") == null ? "" : x.Element("UsedUnitID").Value,
                     UsedUnitName = x.Element("UsedUnitName").Value,
                     ColorName = x.Element("ColorName").Value,
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     ID = x.Element("ID").Value,
                     ProductID = x.Element("ProductID").Value,
                     ProductNo = x.Element("ProductNo").Value,
                     ProductName = x.Element("ProductName").Value,
                     standard = x.Element("standard").Value,
                     UnitID = x.Element("UnitID").Value,
                     Unit = x.Element("Unit").Value,
                     ProductCount = x.Element("ProductCount").Value,
                     UnitPrice = x.Element("UnitPrice").Value,
                     TaxPrice = x.Element("TaxPrice").Value,
                     Discount = x.Element("Discount").Value,
                     TaxRate = x.Element("TaxRate").Value,
                     RequireDate = x.Element("RequireDate").Value,
                     ApplyReasonID = x.Element("ApplyReasonID").Value,
                     ApplyReason = x.Element("ApplyReason").Value,
                     FromBillID = x.Element("FromBillID").Value,
                     FromBillNo = x.Element("FromBillNo").Value,
                     FromLineNo = x.Element("FromLineNo").Value,
                     TypeID = x.Element("TypeID").Value,
                     Seller = x.Element("Seller").Value,
                     SellerName = x.Element("SellerName").Value,
                     DeptID = x.Element("DeptID").Value,
                     DeptIDName = x.Element("DeptIDName").Value,
                     UsedUnitCount = x.Element("UsedUnitCount").Value,
                     UsedUnitID = x.Element("UsedUnitID") == null ? "" : x.Element("UsedUnitID").Value,
                     UsedUnitName = x.Element("UsedUnitName").Value,
                     ColorName = x.Element("ColorName").Value,
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
        public string UnitID { get; set; }
        public string Unit { get; set; }
        public string ProductCount { get; set; }
        public string UnitPrice { get; set; }
        public string TaxPrice { get; set; }
        public string Discount { get; set; }
        public string TaxRate { get; set; }
        public string RequireDate { get; set; }
        public string ApplyReasonID { get; set; }
        public string ApplyReason { get; set; }
        public string FromBillID { get; set; }
        public string FromBillNo { get; set; }
        public string FromLineNo { get; set; }
        public string TypeID { get; set; }
        public string Seller { get; set; }
        public string SellerName { get; set; }
        public string DeptID { get; set; }
        public string DeptIDName { get; set; }
        public string UsedUnitName { get; set; }
        public string UsedUnitCount { get; set; }
        public string UsedUnitID { get; set; }
        public string ColorName { get; set; }
    }

}
<%@ WebHandler Language="C#" Class="PurchaseHistoryAskPriceShow" %>

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

public class PurchaseHistoryAskPriceShow : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        if (context.Request.RequestType == "POST")
        {
            int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;

            //设置行为参数
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "DESC";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "TaxPrice";//要排序的字段，如果为空，默认为"ProductID"
            if (orderString.EndsWith("_a"))
            {
                order = "ASC";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数


            string ProductID = context.Request.Form["ProductID"];
            int TotalCount = 0;


            orderBy = orderBy + " " + order;
            

            ////获取数据
            //string[] str = new string[1];
            //if (context.Request.Params["ProductID"] != null && context.Request.Params["ProductID"] != "")
            //{
            //    str[0] = context.Request.Params["ProductID"].ToString();
            //}
            //else
            //{
            //    str[0] = "";
            //}


            //XElement dsXML = ConvertDataTableToXML(PurchaseOrderBus.SelectPurchaseHistoryAskPriceShow(str));
            ////linq排序
            //var dsLinq =
            //    (order == "ascending") ?
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value ascending
            //     select new DataSourceModel()
            //     {
            //         ID = x.Element("ID").Value,
            //         ProductID = x.Element("ProductID").Value,
            //         ProductNo = x.Element("ProductNo").Value,
            //         ProductName = x.Element("ProductName").Value,
            //         Specification = x.Element("Specification").Value,
            //         UnitID = x.Element("UnitID").Value,
            //         UnitName = x.Element("UnitName").Value,
            //         OrderNo = x.Element("OrderNo").Value,
            //         PurchaseDate = x.Element("PurchaseDate").Value,
            //         Purchaser = x.Element("Purchaser").Value,
            //         PurchaserName = x.Element("PurchaserName").Value,
            //         ProviderID = x.Element("ProviderID").Value,
            //         ProviderName = x.Element("ProviderName").Value,
            //         UnitPrice = x.Element("UnitPrice").Value,
            //         TaxRate = x.Element("TaxRate").Value,
            //         TaxPrice = x.Element("TaxPrice").Value,
            //         ProductCount = x.Element("ProductCount").Value,
            //         TotalFee = x.Element("TotalFee").Value,
            //     })
            //              :
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value descending
            //     select new DataSourceModel()
            //     {
            //         ID = x.Element("ID").Value,
            //         ProductID = x.Element("ProductID").Value,
            //         ProductNo = x.Element("ProductNo").Value,
            //         ProductName = x.Element("ProductName").Value,
            //         Specification = x.Element("Specification").Value,
            //         UnitID = x.Element("UnitID").Value,
            //         UnitName = x.Element("UnitName").Value,
            //         OrderNo = x.Element("OrderNo").Value,
            //         PurchaseDate = x.Element("PurchaseDate").Value,
            //         Purchaser = x.Element("Purchaser").Value,
            //         PurchaserName = x.Element("PurchaserName").Value,
            //         ProviderID = x.Element("ProviderID").Value,
            //         ProviderName = x.Element("ProviderName").Value,
            //         UnitPrice = x.Element("UnitPrice").Value,
            //         TaxRate = x.Element("TaxRate").Value,
            //         TaxPrice = x.Element("TaxPrice").Value,
            //         ProductCount = x.Element("ProductCount").Value,
            //         TotalFee = x.Element("TotalFee").Value,
            //     });
            //int totalCount = dsLinq.Count();
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("{");
            //sb.Append("totalCount:");
            //sb.Append(totalCount.ToString());
            //sb.Append(",data:");
            //sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
            //sb.Append("}");

            //context.Response.ContentType = "text/plain";
            //context.Response.Write(sb.ToString());
            //context.Response.End();

            context.Response.ContentType = "text/plain";
            //string temp = JsonClass.DataTable2Json();

            string temp = JsonClass.FormatDataTableToJson(PurchaseOrderBus.SelectPurchaseHistoryAskPriceShow(pageIndex, pageCount, orderBy, ref TotalCount, ProductID), TotalCount);
            context.Response.Write(temp);
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
 
    public bool IsReusable {
        get {
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
        public string Specification { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string OrderNo { get; set; }
        public string PurchaseDate { get; set; }
        public string Purchaser { get; set; }
        public string PurchaserName { get; set; }
        public string ProviderID { get; set; }
        public string ProviderName { get; set; }
        public string UnitPrice { get; set; }
        public string TaxRate { get; set; }
        public string TaxPrice { get; set; }
        public string ProductCount { get; set; }
        public string TotalFee { get; set; }
    }
}
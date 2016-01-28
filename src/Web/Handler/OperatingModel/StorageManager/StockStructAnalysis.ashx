<%@ WebHandler Language="C#" Class="StockStructAnalysis" %>

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
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;

public class StockStructAnalysis : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string companyCD = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        GetList(context);
    }

    private void GetList(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "ascending";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProductCount";//要排序的字段，如果为空，默认为"ProductNo"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

            //获取数据
            StockAccountModel model = new StockAccountModel();
            model.CompanyCD = companyCD;
            model.StartDate = context.Request.Form["txtStartDate"].Trim();
            //DataTable dt = StockAccountBus.GetStockStructAnalysis(model, "", "");
            //DataView dv = dt.DefaultView;
            //dv.Sort = "ProductCount asc ";
            
            XElement dsXML = ConvertDataTableToXML(StockAccountBus.GetStockStructAnalysis(model, "", ""));
            string[] OrderList = new string[6];
            OrderList[0] = "ProductCount";
            OrderList[1] = "TaxTotalPrice";
            OrderList[2] = "StockBizhong";
            OrderList[3] = "ZanYaTotalPrice";
            OrderList[4] = "OutCountPerDay";
            OrderList[5] = "OutSellCountPerDay";
            if (StorageCommonDBHelper.IsOrderbyNum(orderBy, OrderList))//表示是需要数字排序的
            {
                //linq排序
                var dsLinq =
                   (order == "ascending") ?
                   (from x in dsXML.Descendants("Data")
                    orderby (decimal)x.Element(orderBy) ascending
                    //let price = (decimal)x.Element("Price")

                    select new DataSourceModel()
                    {

                        ProductNo = x.Element("ProductNo").Value,
                        ProductName = x.Element("ProductName").Value,
                        Specification = x.Element("Specification").Value,
                        UnitID = x.Element("UnitID").Value,
                        ProductCount = x.Element("ProductCount").Value,
                        TaxTotalPrice = x.Element("TaxTotalPrice").Value,
                        StockBizhong = x.Element("StockBizhong").Value,
                        ZanYaTotalPrice = x.Element("ZanYaTotalPrice").Value,
                        OutCountPerDay = x.Element("OutCountPerDay").Value,
                        OutSellCountPerDay = x.Element("OutSellCountPerDay").Value,

                    })
                             :
                   (from x in dsXML.Descendants("Data")
                    orderby (decimal)x.Element(orderBy) descending
                    select new DataSourceModel()
                    {
                        ProductNo = x.Element("ProductNo").Value,
                        ProductName = x.Element("ProductName").Value,
                        Specification = x.Element("Specification").Value,
                        UnitID = x.Element("UnitID").Value,
                        ProductCount = x.Element("ProductCount").Value,
                        TaxTotalPrice = x.Element("TaxTotalPrice").Value,
                        StockBizhong = x.Element("StockBizhong").Value,
                        ZanYaTotalPrice = x.Element("ZanYaTotalPrice").Value,
                        OutCountPerDay = x.Element("OutCountPerDay").Value,
                        OutSellCountPerDay = x.Element("OutSellCountPerDay").Value,

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
            else//文字排序
            {
                //linq排序
                var dsLinq =
                   (order == "ascending") ?
                   (from x in dsXML.Descendants("Data")
                    orderby x.Element(orderBy).Value ascending
                    //let price = (decimal)x.Element("Price")

                    select new DataSourceModel()
                    {

                        ProductNo = x.Element("ProductNo").Value,
                        ProductName = x.Element("ProductName").Value,
                        Specification = x.Element("Specification").Value,
                        UnitID = x.Element("UnitID").Value,
                        ProductCount = x.Element("ProductCount").Value,
                        TaxTotalPrice = x.Element("TaxTotalPrice").Value,
                        StockBizhong = x.Element("StockBizhong").Value,
                        ZanYaTotalPrice = x.Element("ZanYaTotalPrice").Value,
                        OutCountPerDay = x.Element("OutCountPerDay").Value,
                        OutSellCountPerDay = x.Element("OutSellCountPerDay").Value,

                    })
                             :
                   (from x in dsXML.Descendants("Data")
                    orderby x.Element(orderBy).Value descending
                    select new DataSourceModel()
                    {
                        ProductNo = x.Element("ProductNo").Value,
                        ProductName = x.Element("ProductName").Value,
                        Specification = x.Element("Specification").Value,
                        UnitID = x.Element("UnitID").Value,
                        ProductCount = x.Element("ProductCount").Value,
                        TaxTotalPrice = x.Element("TaxTotalPrice").Value,
                        StockBizhong = x.Element("StockBizhong").Value,
                        ZanYaTotalPrice = x.Element("ZanYaTotalPrice").Value,
                        OutCountPerDay = x.Element("OutCountPerDay").Value,
                        OutSellCountPerDay = x.Element("OutSellCountPerDay").Value,

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
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string Specification { get; set; }
        public string UnitID { get; set; }
        public string StorageName { get; set; }
        public string ProductCount { get; set; }
        public string TaxTotalPrice { get; set; }
        public string StockBizhong { get; set; }
        public string ZanYaTotalPrice { get; set; }
        public string OutCountPerDay { get; set; }
        public string OutSellCountPerDay { get; set; }
    }
}
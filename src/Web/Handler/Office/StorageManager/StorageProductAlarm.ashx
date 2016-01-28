<%@ WebHandler Language="C#" Class="StorageProductAlarm" %>

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

public class StorageProductAlarm : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string companyCD = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //设置行为参数
            string orderString = (context.Request.QueryString["orderby"].ToString());//排序
            string order = "desc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProductNo";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_a"))
            {
                order = "asc";//排序：降序
            }
            int pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数


            //获取数据
            StockAccountModel model = new StockAccountModel();
            model.CompanyCD = companyCD;
            if (context.Request.QueryString["txtProductNo"] != null)
            {
                model.ProductNo = context.Request.QueryString["txtProductNo"].ToString();
            }
            if (context.Request.QueryString["txtProductName"] != null)
            {
                model.ProductName = context.Request.QueryString["txtProductName"].ToString();
            }
            string sltAlarmType = context.Request.QueryString["sltAlarmType"].ToString();
            string BarCode = context.Request.QueryString["BarCode"].ToString();

            string ord = orderBy + " " + order;
            int TotalCount = 0;
            DataTable dt = StorageProductAlarmBus.GetStorageProductAlarm(sltAlarmType, model, BarCode, pageIndex, pageCount, ord, ref TotalCount);

            //XElement dsXML = ConvertDataTableToXML(StorageProductAlarmBus.GetStorageProductAlarm(sltAlarmType, model));
            ////linq排序
            //var dsLinq =
            //    (order == "ascending") ?
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value ascending
            //     select new DataSourceModel()
            //     {
            //         ProductNo = x.Element("ProductNo").Value,
            //         ProductName = x.Element("ProductName").Value,
            //         TypeID = x.Element("TypeID").Value,
            //         Specification = x.Element("Specification").Value,
            //         UnitID = x.Element("UnitID").Value,
            //         MinStockNum = x.Element("MinStockNum").Value,
            //         MaxStockNum = x.Element("MaxStockNum").Value,
            //         SafeStockNum = x.Element("SafeStockNum").Value,
            //         ProductCount = x.Element("ProductCount").Value,
            //         AlarmType = x.Element("AlarmType").Value,

            //     })
            //              :
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value descending
            //     select new DataSourceModel()
            //     {
            //         ProductNo = x.Element("ProductNo").Value,
            //         ProductName = x.Element("ProductName").Value,
            //         TypeID = x.Element("TypeID").Value,
            //         Specification = x.Element("Specification").Value,
            //         UnitID = x.Element("UnitID").Value,
            //         MinStockNum = x.Element("MinStockNum").Value,
            //         MaxStockNum = x.Element("MaxStockNum").Value,
            //         SafeStockNum = x.Element("SafeStockNum").Value,
            //         ProductCount = x.Element("ProductCount").Value,
            //         AlarmType = x.Element("AlarmType").Value,
            //     });
            //int totalCount = dsLinq.Count();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(TotalCount.ToString());
            sb.Append(",data:");
            if (dt.Rows.Count == 0)
                sb.Append("[{\"ProductNo\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
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

        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string TypeID { get; set; }
        public string Specification { get; set; }
        public string UnitID { get; set; }
        public string MinStockNum { get; set; }
        public string MaxStockNum { get; set; }
        public string SafeStockNum { get; set; }
        public string ProductCount { get; set; }
        public string AlarmType { get; set; }
    }


}
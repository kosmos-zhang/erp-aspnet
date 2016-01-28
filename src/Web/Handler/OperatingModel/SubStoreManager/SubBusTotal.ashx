<%@ WebHandler Language="C#" Class="SubBusTotal" %>

using System;
using System.Web;
using System.Data;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using System.Linq;
using XBase.Business.Office.SubStoreManager;
public class SubBusTotal : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        GetSellProductLsit(context);
    }

    /// <summary>
    /// 获取数据列表
    /// </summary>
    private void GetSellProductLsit(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "ascending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProductName";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "descending";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        //SellGatheringModel sellGatheringModel = new SellGatheringModel();

        DataTable dt = GetDataTable(context);
        XElement dsXML = ConvertDataTableToXML(dt);
        //linq排序
        var dsLinq =
            (order == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderBy).Value ascending
             select new DataSourceModel()
             {
                 DeptName = x.Element("DeptName").Value,
                 ProductName = x.Element("ProductName").Value,
                 Specification = x.Element("Specification").Value,
                  UnitName = x.Element("UnitName").Value,
                 SellCount = x.Element("SellCount").Value,
                 SellTotalFee = x.Element("SellTotalFee").Value,
                 BackCount = x.Element("BackCount").Value,
             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderBy).Value descending
             select new DataSourceModel()
             {
                 DeptName = x.Element("DeptName").Value,
                 ProductName = x.Element("ProductName").Value,
                 Specification = x.Element("Specification").Value,
                 UnitName = x.Element("UnitName").Value,
                 SellCount = x.Element("SellCount").Value,
                 SellTotalFee = x.Element("SellTotalFee").Value,
                 BackCount = x.Element("BackCount").Value,
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
    private DataTable GetDataTable(HttpContext context)
    {
        DataTable dt = new DataTable();
        string SellDate_ = context.Request.Params["SellDate"].ToString().Trim();
        string DeptID = context.Request.Params["DeptID"].ToString().Trim();
        DateTime SellDate = Convert.ToDateTime(SellDate_);
        DateTime SellEndDate = Convert.ToDateTime(context.Request.Params["SellEndDate"].ToString().Trim());
        //dt = SubStorageBus.GetSubProductSellInfo(SellDate, SellEndDate, DeptID);
        dt = null;
        return dt;
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
        public string DeptName { get; set; }
        public string ProductName { get; set; }
        public string Specification { get; set; }
        public string UnitName { get; set; }
        public string SellCount { get; set; }
        public string SellTotalFee { get; set; }
        public string BackCount { get; set; }
        public string BackTotalFee { get; set; }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
<%@ WebHandler Language="C#" Class="ReportPurDetail" %>



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
using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;
public class ReportPurDetail : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.RequestType == "POST")
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "ascending";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            string ArriveNo = context.Request.Form["ArriveNo"].ToString();
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            string Productname = context.Request.Form["MyProductName1"].ToString();
            string ProductNo = context.Request.Form["MyProNo1"].ToString();
            string ReportStr = ProductNo + "?" + Productname;
            DataTable mydt = CheckReportBus.GetReprotPurDetail(ArriveNo, CompanyCD, ReportStr);
            
            XElement dsXML = ConvertDataTableToXML(mydt);
            
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     ID = x.Element("ID").Value,
                     ProductName = x.Element("ProductName").Value,
                     CodeName = x.Element("CodeName").Value,
                     ProNo = x.Element("ProNo").Value,
                     ProductCount = x.Element("ProductCount").Value,
                     ApplyCheckCount = x.Element("ApplyCheckCount").Value,
                     CheckedCount = x.Element("CheckedCount").Value,
                     ProductID = x.Element("ProductID").Value,
                     Specification = x.Element("Specification").Value,
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     ID = x.Element("ID").Value,
                     ProductName = x.Element("ProductName").Value,
                     CodeName = x.Element("CodeName").Value,
                     ProNo = x.Element("ProNo").Value,
                     ProductCount = x.Element("ProductCount").Value,
                     ApplyCheckCount = x.Element("ApplyCheckCount").Value,
                     CheckedCount = x.Element("CheckedCount").Value,
                     ProductID = x.Element("ProductID").Value,
                     Specification = x.Element("Specification").Value,

                     
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
    //数据源结构
    public class DataSourceModel
    {
        public string ID { get; set; }         //
        public string ProductName { get; set; }
        public string CodeName { get; set; }      //
        public string ProNo { get; set; }  //
        public string ProductCount { get; set; }  //
        public string ApplyCheckCount { get; set; }  //
        public string CheckedCount { get; set; }//
        public string ProductID { get; set; }
        public string Specification { get; set; }

        
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

}
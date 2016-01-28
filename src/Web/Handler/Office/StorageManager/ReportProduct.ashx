<%@ WebHandler Language="C#" Class="ReportProduct" %>

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

public class ReportProduct : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "descending";//排序：
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "ascending";//排序：
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            string ProNo = context.Request.Form["TheProNo"].ToString();
            string ProductName = context.Request.Form["TheProductName"].ToString();
            string ReportStr = ProNo + "?" + ProductName;
                 
            string ApplyNo = context.Request.Form["ApplyNo"].ToString();
            XElement dsXML = ConvertDataTableToXML(CheckReportBus.GetCheckDetailBy(ApplyNo, CompanyCD,ReportStr));

            var dsLinq =
    (order == "ascending") ?
    (from x in dsXML.Descendants("Data")
     orderby x.Element(orderBy).Value ascending
     select new DataSourceModel()
     {
         CodeName = x.Element("CodeName").Value,
         ID = x.Element("ID").Value,
         ProductCount = x.Element("ProductCount").Value,
         ProductID = x.Element("ProductID").Value,
         ProductName = x.Element("ProductName").Value,
         ProNo = x.Element("ProNo").Value,
         UnitID = x.Element("UnitID").Value,
         FromLineNo = x.Element("FromLineNo").Value,
         CheckedCount = x.Element("CheckedCount").Value,
         CheckMode = x.Element("CheckMode").Value,
         CheckModeID = x.Element("CheckModeID").Value,
         Specification = x.Element("Specification").Value,

     })
              :
    (from x in dsXML.Descendants("Data")
     orderby x.Element(orderBy).Value descending
     select new DataSourceModel()
     {
         CodeName = x.Element("CodeName").Value,
         ID = x.Element("ID").Value,
         ProductCount = x.Element("ProductCount").Value,
         ProductID = x.Element("ProductID").Value,
         ProductName = x.Element("ProductName").Value,
         ProNo = x.Element("ProNo").Value,
         UnitID = x.Element("UnitID").Value,
         FromLineNo = x.Element("FromLineNo").Value,
         CheckedCount = x.Element("CheckedCount").Value,
         CheckMode = x.Element("CheckMode").Value,
         CheckModeID = x.Element("CheckModeID").Value,
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
        public string ID { get; set; }         //ID
        public string UnitID { get; set; }      //
        public string ProductCount { get; set; }      //
        public string ProductName { get; set; }    //
        public string ProductID { get; set; }  //ID
        public string ProNo { get; set; }  //
        public string CodeName { get; set; }  //ID
        public string FromLineNo { get; set; }
        public string CheckedCount { get; set; }
        public string CheckMode { get; set; }
        public string CheckModeID { get; set; }
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
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
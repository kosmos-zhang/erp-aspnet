<%@ WebHandler Language="C#" Class="LinkManSele" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.CustManager;
using System.Xml.Linq;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Linq;

public class LinkManSele : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
         if (context.Request.RequestType == "POST")
        {
           //设置行为参数
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "ascending";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "LinkManName";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

            //获取数据           
            string LinkMan = context.Request.Form["LinkMan"].ToString().Trim();
            string Appellation = context.Request.Form["Appellation"].ToString().Trim();
            string Department = context.Request.Form["Department"].ToString().Trim();
            string CustNo = context.Request.Form["CustNo"].ToString().Trim(); 
            
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//用户公司代码 

            XElement dsXML = ConvertDataTableToXML(LinkManBus.GetLinkManByCustNo(LinkMan, Appellation, Department, CompanyCD, CustNo));

            //linq排序
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     id = x.Element("id").Value,
                     LinkManName = x.Element("LinkManName").Value,
                     Appellation = x.Element("Appellation").Value,
                     Department = x.Element("Department").Value
                 }):
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     id = x.Element("id").Value,
                     LinkManName = x.Element("LinkManName").Value,
                     Appellation = x.Element("Appellation").Value,
                     Department = x.Element("Department").Value
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
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    //把DataTable转换为XML流
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

    public class DataSourceModel
    {
        public string id { get; set; }
        public string LinkManName { get; set; }
        public string Appellation { get; set; }
        public string Department { get; set; }
    }

}
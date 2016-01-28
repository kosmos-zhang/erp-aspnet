<%@ WebHandler Language="C#" Class="ApprovalFlowSet" %>

using System;
using System.Web;
using XBase.Model.Office.SystemManager;
using XBase.Business.SystemManager;
using System.IO;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Serialization;
using XBase.Common;
using System.Linq;
using System.Data.SqlTypes;
using System.Data;
using System.Xml.Linq;
public class ApprovalFlowSet : IHttpHandler, System.Web.SessionState.IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) 
    {
        string ProCode = context.Request.QueryString["BillTypeName"].ToString();
        string orderString = context.Request.QueryString["orderby"].ToString();//排序
        string order = "ascending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "TypeCode";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "descending";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据
        XElement dsXML = ConvertDataTableToXML(ApprovalFlowSetBus.GetBillTypeByTypeCode(ProCode));
        //linq排序
        var dsLinq =
            (order == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderBy).Value ascending
             select new DataSourceModel()
             {
                 TypeCode = x.Element("TypeCode").Value,
                 TypeName = x.Element("TypeName").Value,
                 ID = x.Element("ID").Value
             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderBy).Value descending
             select new DataSourceModel()
             {
                 TypeCode = x.Element("TypeCode").Value,
                 TypeName = x.Element("TypeName").Value,
                 ID = x.Element("ID").Value
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
 
    public bool IsReusable {
        get {
            return false;
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
    //数据源结构
    public class DataSourceModel
    {
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string ID{ get; set; }
    }
}
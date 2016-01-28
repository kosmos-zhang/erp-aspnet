<%@ WebHandler Language="C#" Class="StorageReportPur" %>



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
public class StorageReportPur : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            string method = context.Request.Form["method"].ToString();
            string BillTitle = context.Request.Form["TheBillTitle"].ToString();
            string BillNo = context.Request.Form["TheBillNo"].ToString();
            string ReportStr = BillNo + "?" + BillTitle;
                

            XElement dsXML = ConvertDataTableToXML(CheckReportBus.GetReportPur(CompanyCD,method,ReportStr));
            
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     ID = x.Element("ID").Value,
                     ArriveDate = x.Element("ArriveDate").Value,
                     CustBigTypeID = x.Element("CustBigTypeID").Value,
                     CustBigTypeName = x.Element("CustBigTypeName").Value,
                     CustID = x.Element("CustID").Value,
                     CustName = x.Element("CustName").Value,
                     CustNo = x.Element("CustNo").Value,
                     DeptName = x.Element("DeptName").Value,
                     EmployeeName = x.Element("EmployeeName").Value,
                     Title = x.Element("Title").Value,
                     ArriveNo = x.Element("ArriveNo").Value,
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     ID = x.Element("ID").Value,
                     ArriveDate=x.Element("ArriveDate").Value,
                     CustBigTypeID=x.Element("CustBigTypeID").Value,
                     CustBigTypeName=x.Element("CustBigTypeName").Value,
                     CustID=x.Element("CustID").Value,
                     CustName=x.Element("CustName").Value,
                     CustNo=x.Element("CustNo").Value,
                     DeptName=x.Element("DeptName").Value,
                     EmployeeName=x.Element("EmployeeName").Value,
                     Title=x.Element("Title").Value,
                     ArriveNo = x.Element("ArriveNo").Value,

                     
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
        public string Title { get; set; }      
        public string CustID { get; set; }      //
        public string CustName { get; set; }    //
        public string CustBigTypeName { get; set; }  //
        public string CustBigTypeID { get; set; }  //
        public string CustNo { get; set; }  //
        public string EmployeeName { get; set; }//
        public string DeptName { get; set; }
        public string ArriveDate { get; set; }
        public string ArriveNo { get; set; }

        
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
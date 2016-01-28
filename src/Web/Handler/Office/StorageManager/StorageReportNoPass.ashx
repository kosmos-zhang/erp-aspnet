<%@ WebHandler Language="C#" Class="StorageReportNoPass" %>

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
public class StorageReportNoPass : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "descending";
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "ascending";
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            string method = context.Request.Form["method"].ToString();
            string BillNo = context.Request.Form["myBillNo"].ToString();
            string BillTitle = context.Request.Form["myBillTitle"].ToString();
            string NoPassStr = BillNo + "?" + BillTitle;
            DataTable dt = CheckNotPassBus.GetReportInfo(method, NoPassStr);
            XBase.Common.StringUtil.DecimalFormatPoint(int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint), dt);
            XElement dsXML = ConvertDataTableToXML(dt);

            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     ID = x.Element("ID").Value,
                     NoPass = x.Element("NoPass").Value,
                     ProductID = x.Element("ProductID").Value,
                     ProductName = x.Element("ProductName").Value,
                     ProdNo = x.Element("ProdNo").Value,
                     ReportNo = x.Element("ReportNo").Value,
                     UnitID = x.Element("UnitID").Value,
                     UnitName = x.Element("UnitName").Value,
                     EmployeeName = x.Element("EmployeeName").Value,
                     DeptName = x.Element("DeptName").Value,
                     Specification = x.Element("Specification").Value,
                     Title = x.Element("Title").Value,
                     NotPassNum = x.Element("NotPassNum").Value,

                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     ID = x.Element("ID").Value,
                     NoPass = x.Element("NoPass").Value,
                     ProductID = x.Element("ProductID").Value,
                     ProductName = x.Element("ProductName").Value,
                     ProdNo = x.Element("ProdNo").Value,
                     ReportNo = x.Element("ReportNo").Value,
                     UnitID = x.Element("UnitID").Value,
                     UnitName = x.Element("UnitName").Value,
                     EmployeeName = x.Element("EmployeeName").Value,
                     DeptName = x.Element("DeptName").Value,
                     Specification = x.Element("Specification").Value,
                     Title = x.Element("Title").Value,
                     NotPassNum = x.Element("NotPassNum").Value,


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
        public string ID { get; set; }         //报告单ID
        public string ReportNo { get; set; }   //报告单编号
        public string Title { get; set; }      //报告单标题
        public string ProductID { get; set; }      //
        public string ProductName { get; set; }    //
        public string ProdNo { get; set; }  //
        public string UnitID { get; set; }  //
        public string UnitName { get; set; }  //
        public string NoPass { get; set; }//
        public string EmployeeName { get; set; }
        public string DeptName { get; set; }
        public string Specification { get; set; }
        public string NotPassNum { get; set; }//处置数量


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
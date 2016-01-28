<%@ WebHandler Language="C#" Class="CheckReportFrom" %>

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
public class CheckReportFrom : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.RequestType == "POST")
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "ascending";//排序：升序
            int pageCount = 10;
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
            try
            {
                pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            }
            catch
            { }
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            string method = context.Request.Form["method"].ToString();
            string BillNo = context.Request.Form["TheBillNo"].ToString();
            string BillTitle = context.Request.Form["TheBillTitle"].ToString();
            string ReportStr = BillNo + "?" + BillTitle;
            XElement dsXML = ConvertDataTableToXML(CheckReportBus.GetReportInfo(method,ReportStr));
            
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     CheckerID = x.Element("CheckerID").Value,
                     CheckerName = x.Element("CheckerName").Value,
                     CheckMode = x.Element("CheckMode").Value,
                     CheckType=x.Element("CheckType").Value,
                     CodeName = x.Element("CodeName").Value,
                     DeptID = x.Element("DeptID").Value,
                     DeptName = x.Element("DeptName").Value,
                     ID = x.Element("ID").Value,
                     ProductID = x.Element("ProductID").Value,
                     ProductName = x.Element("ProductName").Value,
                     ProNo = x.Element("ProNo").Value,
                     UnitID = x.Element("UnitID").Value,
                     ReportNo = x.Element("ReportNo").Value,
                     Title = x.Element("Title").Value,
                     CheckModeName = x.Element("CheckModeName").Value,
                     CheckTypeName = x.Element("CheckTypeName").Value,
                     CheckNum = x.Element("CheckNum").Value,
                     FromLineNo = x.Element("FromLineNo").Value,
                     CheckContent = x.Element("CheckContent").Value,
                     CheckedCount = x.Element("CheckedCount").Value,
                     CheckStandard = x.Element("CheckStandard").Value,
                     Specification = x.Element("Specification").Value,
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     CheckerID = x.Element("CheckerID").Value,
                     CheckerName = x.Element("CheckerName").Value,
                     CheckMode = x.Element("CheckMode").Value,
                     CheckType = x.Element("CheckType").Value,
                     CodeName = x.Element("CodeName").Value,
                     DeptID = x.Element("DeptID").Value,
                     DeptName = x.Element("DeptName").Value,
                     ID = x.Element("ID").Value,
                     ProductID = x.Element("ProductID").Value,
                     ProductName = x.Element("ProductName").Value,
                     ProNo = x.Element("ProNo").Value,
                     UnitID = x.Element("UnitID").Value,
                     ReportNo=x.Element("ReportNo").Value,
                     Title=x.Element("Title").Value,
                     CheckModeName = x.Element("CheckModeName").Value,
                     CheckTypeName = x.Element("CheckTypeName").Value,
                     CheckNum = x.Element("CheckNum").Value,
                     FromLineNo = x.Element("FromLineNo").Value,
                     CheckContent = x.Element("CheckContent").Value,
                     CheckedCount = x.Element("CheckedCount").Value,
                     CheckStandard = x.Element("CheckStandard").Value,
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
        public string ID { get; set; }         //申请单ID
        public string CheckType { get; set; }      //申请单标题
        public string CheckMode { get; set; }      //往来单位ID
        public string CheckerID { get; set; }    //往来单位名称
        public string CheckerName { get; set; }  //往来单位大类ID
        public string DeptName { get; set; }  //往来单位大类名称
        public string DeptID { get; set; }  //生产负责人ID
        public string ProductID { get; set; }//生产负责人名称
        public string ProductName { get; set; }
        public string ProNo { get; set; }
        public string UnitID { get; set; }
        public string CodeName { get; set; }
        public string ReportNo { get; set; }
        public string Title { get; set; }
        public string CheckModeName { get; set; }
        public string CheckTypeName { get; set; }
        public string CheckNum {get; set;}
        public string FromLineNo { get; set; }
        public string CheckContent { get; set; }
        public string CheckedCount { get; set; }
        public string CheckStandard { get; set; }
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
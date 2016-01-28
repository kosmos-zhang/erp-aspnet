<%@ WebHandler Language="C#" Class="StorageInitailList" %>

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

public class StorageInitailList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string companyCD = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            
            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //设置行为参数
            string orderString = (context.Request.QueryString["orderBy"].ToString());//排序
            string order = "desc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_a"))
            {
                order = "asc";//排序：降序
            }
            int pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

            //获取数据
            StorageInitailModel model = new StorageInitailModel();
            string EnterDateStart = string.Empty;
            string EnterDateEnd = string.Empty;
            model.CompanyCD = companyCD;
            model.InNo = context.Request.QueryString["InNo"].Trim();
            string BatchNo = context.Request.QueryString["BatchNo"].Trim();
            model.Title = context.Request.QueryString["Title"];
            model.DeptID = context.Request.QueryString["Dept"];
            model.StorageID = context.Request.QueryString["StorageID"];
            model.Executor = context.Request.QueryString["txtExecutor"];
            model.BillStatus = context.Request.QueryString["sltBillStatus"];
            EnterDateStart = context.Request.QueryString["EnterDateStart"].ToString();
            EnterDateEnd = context.Request.QueryString["EnterDateEnd"].ToString();
            string EFIndex = context.Request.QueryString["EFIndex"].ToString();
            string EFDesc = context.Request.QueryString["EFDesc"].ToString();
            string ord = orderBy + " " + order;
            int TotalCount = 0;
            DataTable dt = StorageInitailBus.GetStorageInitailTableBycondition(BatchNo,EFIndex, EFDesc, model, EnterDateStart, EnterDateEnd, pageIndex, pageCount, ord, ref TotalCount);

            //XElement dsXML = ConvertDataTableToXML(StorageInitailBus.GetStorageInitailTableBycondition(model,EnterDateStart,EnterDateEnd));
            ////linq排序
            //var dsLinq =
            //    (order == "ascending") ?
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value ascending
            //     select new DataSourceModel()
            //     {

            //         ID = x.Element("ID").Value,
            //         InNo = x.Element("InNo").Value,
            //         Title = x.Element("Title").Value,
            //         StorageName = x.Element("StorageName").Value,
            //         DeptName = x.Element("DeptName").Value,
            //         Executor = x.Element("Executor").Value,
            //         EnterDate = x.Element("EnterDate").Value,
            //         CountTotal = x.Element("CountTotal").Value,
            //         TotalPrice = x.Element("TotalPrice").Value,
            //         BillStatus = x.Element("BillStatus").Value,
            //     })
            //              :
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value descending
            //     select new DataSourceModel()
            //     {
            //         ID = x.Element("ID").Value,
            //         InNo = x.Element("InNo").Value,
            //         Title = x.Element("Title").Value,
            //         StorageName = x.Element("StorageName").Value,
            //         DeptName = x.Element("DeptName").Value,
            //         Executor = x.Element("Executor").Value,
            //         EnterDate = x.Element("EnterDate").Value,
            //         CountTotal = x.Element("CountTotal").Value,
            //         TotalPrice = x.Element("TotalPrice").Value,
            //         BillStatus = x.Element("BillStatus").Value,
            //     });
            //int totalCount = dsLinq.Count();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(TotalCount.ToString());
            sb.Append(",data:");
            if (dt.Rows.Count == 0)
                sb.Append("[{\"ID\":\"\"}]");
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
        public string ID { get; set; }
        public string InNo { get; set; }
        public string Title { get; set; }
        public string StorageName { get; set; }
        public string DeptName { get; set; }
        public string Executor { get; set; }
        public string EnterDate { get; set; }
        public string CountTotal { get; set; }
        public string TotalPrice { get; set; }
        public string BillStatus { get; set; }
    }

}
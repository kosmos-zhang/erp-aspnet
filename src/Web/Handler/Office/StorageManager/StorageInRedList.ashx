<%@ WebHandler Language="C#" Class="StorageInRedList" %>

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
public class StorageInRedList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string companyCD = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string action = (context.Request.QueryString["action"].ToString());//操作
            if (action == "getStorageInList")//根据类型弹出源单列表
            {
                string orderString = (context.Request.QueryString["orderByOff"].ToString());//排序
                string order = "descending";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CreateDate";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_a"))
                {
                    order = "ascending";//排序：降序
                }
                int pageCount = int.Parse(context.Request.QueryString["pageOffCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
                string InType = (context.Request.QueryString["InType"]);

                StorageInRedModel model = new StorageInRedModel();
                model.CompanyCD = companyCD;
                model.InNo = context.Request.QueryString["txtNo_UC"];
                model.Title = context.Request.QueryString["txtTitle_UC"];
                
                DataTable dt = StorageInRedBus.GetStorageInList(model, InType);
                XElement dsXML = ConvertDataTableToXML(dt);
                //linq排序
                var dsLinq =
                    (order == "ascending") ?
                    (from x in dsXML.Descendants("Data")
                     orderby x.Element(orderBy).Value ascending
                     select new DataSourceStorageInModle()
                     {
                         ID = x.Element("ID").Value,
                         InNo = x.Element("InNo").Value,
                         FromType = x.Element("FromType").Value,
                         Title = x.Element("Title").Value,
                         CreatDate = x.Element("CreateDate").Value,
                     })
                              :
                    (from x in dsXML.Descendants("Data")
                     orderby x.Element(orderBy).Value descending
                     select new DataSourceStorageInModle()
                     {
                         ID = x.Element("ID").Value,
                         InNo = x.Element("InNo").Value,
                         FromType = x.Element("FromType").Value,
                         Title = x.Element("Title").Value,
                         CreatDate = x.Element("CreateDate").Value,
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
            else
            {

                string orderString = (context.Request.QueryString["orderby"].ToString());//排序
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
                StorageInRedModel model = new StorageInRedModel();
                model.CompanyCD = companyCD;
                string EnterDateStart = string.Empty;
                string EnterDateEnd = string.Empty;
                model.InNo = context.Request.QueryString["InNo"].Trim();
                model.Title = context.Request.QueryString["Title"];
                model.ReasonType = context.Request.QueryString["ddlReasonType"];
                if (context.Request.QueryString["FromBillID"].Trim() != "undefined")
                {
                    model.FromBillID = context.Request.QueryString["FromBillID"].ToString();
                }
                if (context.Request.QueryString["InPutDept"].Trim() != "undefined")
                {
                    model.DeptID = context.Request.QueryString["InPutDept"].ToString();
                }
                model.BillStatus = context.Request.QueryString["BillStatus"].ToString();
                if (context.Request.QueryString["Executor"].Trim() != "undefined")
                {
                    model.Executor = context.Request.QueryString["Executor"].ToString();
                }
                if (context.Request.QueryString["FromType"].Trim() != "undefined")
                {
                    model.FromType = context.Request.QueryString["FromType"].ToString();
                }
                model.EFIndex = context.Request.QueryString["EFIndex"].ToString();
                model.EFDesc = context.Request.QueryString["EFDesc"].ToString();

                EnterDateStart = context.Request.QueryString["EnterDateStart"].ToString();
                EnterDateEnd = context.Request.QueryString["EnterDateEnd"].ToString();

                string BatchNo = context.Request.QueryString["BatchNo"];
                
                string ord = orderBy + " " + order;
                int TotalCount = 0;
                DataTable dt = StorageInRedBus.GetStorageInRedTableBycondition(BatchNo,model, EnterDateStart, EnterDateEnd, pageIndex, pageCount, ord, ref TotalCount);

                //XElement dsXML = ConvertDataTableToXML(StorageInRedBus.GetStorageInRedTableBycondition(model, EnterDateStart, EnterDateEnd));
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
                //         FromType = x.Element("FromType").Value,
                //         FromBillNO = x.Element("FromInNo").Value,
                //         ReasonType=x.Element("CodeName").Value,
                //         InputDept = x.Element("DeptName").Value,
                //         ExecutorName = x.Element("ExecutorName").Value,
                //         EnterDate = x.Element("EnterDate").Value,
                //         CountTotal = x.Element("CountTotal").Value,
                //         TotalPrice = x.Element("TotalPrice").Value,
                //         BillStatusName = x.Element("BillStatusName").Value,
                //         Summary = x.Element("Summary").Value,

                //     })
                //              :
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value descending
                //     select new DataSourceModel()
                //     {
                //         ID = x.Element("ID").Value,
                //         InNo = x.Element("InNo").Value,
                //         Title = x.Element("Title").Value,
                //         FromType = x.Element("FromType").Value,
                //         FromBillNO = x.Element("FromInNo").Value,
                //         ReasonType = x.Element("CodeName").Value,
                //         InputDept = x.Element("DeptName").Value,
                //         ExecutorName = x.Element("ExecutorName").Value,
                //         EnterDate = x.Element("EnterDate").Value,
                //         CountTotal = x.Element("CountTotal").Value,
                //         TotalPrice = x.Element("TotalPrice").Value,
                //         BillStatusName = x.Element("BillStatusName").Value,
                //         Summary = x.Element("Summary").Value,
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
        //入库单编号、入库单主题、源单类型、原始入库单、入库部门、
        //人库人、入库时间、红冲数量、红冲金额、摘要、单据状态

        public string ID { get; set; }
        public string InNo { get; set; }
        public string Title { get; set; }
        public string FromType { get; set; }
        public string FromBillNO { get; set; }
        public string ReasonType { get; set; }
        public string InputDept { get; set; }
        public string ExecutorName { get; set; }
        public string EnterDate { get; set; }
        public string CountTotal { get; set; }
        public string TotalPrice { get; set; }
        public string Summary { get; set; }
        public string BillStatusName { get; set; }
    }

    public class DataSourceStorageInModle
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string FromType { get; set; }
        public string InNo { get; set; }
        public string CreatDate { get; set; }
    }

}
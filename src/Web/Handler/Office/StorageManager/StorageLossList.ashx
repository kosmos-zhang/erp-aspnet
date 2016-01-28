<%@ WebHandler Language="C#" Class="StorageLossList" %>

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

public class StorageLossList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string companyCD = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //设置行为参数
            string orderString = (context.Request.QueryString["orderby"].ToString());//排序
            string order = "desc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "LossNo";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_a"))
            {
                order = "asc";//排序：降序
            }
            int pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数


            //获取数据
            StorageLossModel model = new StorageLossModel();
            model.CompanyCD = companyCD;
            string LossDateStart = string.Empty;
            string LossDateEnd = string.Empty;
            string TotalPriceStart = string.Empty;
            string TotalPriceEnd = string.Empty;
            string FlowStatus = string.Empty;
            model.LossNo = context.Request.QueryString["LossNo"].Trim();
            model.Title = context.Request.QueryString["Title"];
            model.BatchNo = context.Request.QueryString["BatchNo"];//批次
            if (context.Request.QueryString["Dept"].Trim() != "undefined")
            {
                model.DeptID = context.Request.QueryString["Dept"].ToString();
            }
            model.StorageID = context.Request.QueryString["StorageID"].ToString();
            if (context.Request.QueryString["Executor"].Trim() != "undefined")
            {
                model.Executor = context.Request.QueryString["Executor"].ToString();
            }
            model.ReasonType = context.Request.QueryString["ReasonType"].ToString();
            model.BillStatus = context.Request.QueryString["BillStatus"].ToString();
            FlowStatus = context.Request.QueryString["FlowStatus"].ToString();
            LossDateStart = context.Request.QueryString["LossDateStart"].ToString();
            LossDateEnd = context.Request.QueryString["LossDateEnd"].ToString();
            TotalPriceStart = context.Request.QueryString["TotalPriceStart"].ToString();
            TotalPriceEnd = context.Request.QueryString["TotalPriceEnd"].ToString();

            //扩展属性条件
            string EFIndex = context.Request.QueryString["EFIndex"].ToString();
            string EFDesc = context.Request.QueryString["EFDesc"].ToString();

            string ord = orderBy + " " + order;
            int TotalCount = 0;
            DataTable dt = StorageLossBus.GetStorageLossTableBycondition(model, LossDateStart, LossDateEnd, TotalPriceStart, TotalPriceEnd, FlowStatus, EFIndex, EFDesc, pageIndex, pageCount, ord, ref TotalCount);

            //XElement dsXML = ConvertDataTableToXML(StorageLossBus.GetStorageLossTableBycondition(model, LossDateStart, LossDateEnd, TotalPriceStart, TotalPriceEnd, FlowStatus));
            ////linq排序
            //var dsLinq =
            //    (order == "ascending") ?
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value ascending
            //     select new DataSourceModel()
            //     {
            //         ID = x.Element("ID").Value,
            //         LossNo = x.Element("LossNo").Value,
            //         Title = x.Element("Title").Value,
            //         Executor = x.Element("Executor").Value,
            //         DeptName = x.Element("DeptName").Value,
            //         StorageName = x.Element("StorageName").Value,
            //         LossDate = x.Element("LossDate").Value,
            //         ReasonTypeName = x.Element("ReasonTypeName").Value,
            //         TotalPrice = x.Element("TotalPrice").Value,
            //         BillStatusName = x.Element("BillStatusName").Value,
            //         FlowStatus = x.Element("FlowStatus").Value,

            //     })
            //              :
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value descending
            //     select new DataSourceModel()
            //     {
            //         ID = x.Element("ID").Value,
            //         LossNo = x.Element("LossNo").Value,
            //         Title = x.Element("Title").Value,
            //         Executor = x.Element("Executor").Value,
            //         DeptName = x.Element("DeptName").Value,
            //         StorageName = x.Element("StorageName").Value,
            //         LossDate = x.Element("LossDate").Value,
            //         ReasonTypeName = x.Element("ReasonTypeName").Value,
            //         TotalPrice = x.Element("TotalPrice").Value,
            //         BillStatusName = x.Element("BillStatusName").Value,
            //         FlowStatus = x.Element("FlowStatus").Value,
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
        public string LossNo { get; set; }
        public string Title { get; set; }
        public string Executor { get; set; }
        public string DeptName { get; set; }
        public string StorageName { get; set; }
        public string LossDate { get; set; }
        public string ReasonTypeName { get; set; }
        public string TotalPrice { get; set; }
        public string BillStatusName { get; set; }
        public string FlowStatus { get; set; }

    }

}
<%@ WebHandler Language="C#" Class="StorageOutSellList" %>

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

public class StorageOutSellList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string companyCD = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string action = (context.Request.QueryString["action"].ToString());//操作
            if (action == "getSellList")//弹出销售发货通知单
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

                XBase.Model.Office.SellManager.SellSendModel model = new XBase.Model.Office.SellManager.SellSendModel();
                model.CompanyCD = companyCD;
                model.SendNo = context.Request.QueryString["txtNo_UC"];
                model.Title = context.Request.QueryString["txtTitle_UC"];

                DataTable dt = StorageOutSellBus.GetSellSendList(model);
                XElement dsXML = ConvertDataTableToXML(dt);
                //linq排序
                var dsLinq =
                    (order == "ascending") ?
                    (from x in dsXML.Descendants("Data")
                     orderby x.Element(orderBy).Value ascending
                     select new DataSourceSellSendModle()
                     {
                         ID = x.Element("ID").Value,
                         SendNo = x.Element("SendNo").Value,
                         Title = x.Element("Title").Value,
                         CreatDate = x.Element("CreateDate").Value,
                     })
                              :
                    (from x in dsXML.Descendants("Data")
                     orderby x.Element(orderBy).Value descending
                     select new DataSourceSellSendModle()
                     {
                         ID = x.Element("ID").Value,
                         SendNo = x.Element("SendNo").Value,
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
                //设置行为参数
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
                //扩展属性条件
                string EFIndex=context.Request.QueryString["EFIndex"].ToString();
                string EFDesc=context.Request.QueryString["EFDesc"].ToString();

                string BatchNo = context.Request.QueryString["BatchNo"].ToString();

                //获取数据
                StorageOutSellModel model = new StorageOutSellModel();
                string OutDateStart = string.Empty;
                string OutDateEnd = string.Empty;
                string SendNo = string.Empty;
                model.CompanyCD = companyCD;
                model.OutNo = context.Request.QueryString["OutNo"].Trim();
                model.Title = context.Request.QueryString["Title"];
                model.BillStatus = context.Request.QueryString["BillStatus"];
                model.BatchNo = context.Request.QueryString["BatchNo"];//批次（生产版改造增加批次2010.4.19）
                
                if (context.Request.QueryString["InOutDept"].Trim() != "undefined")
                {
                    model.DeptID = context.Request.QueryString["InOutDept"].ToString();
                }
                if (context.Request.QueryString["Transactor"].Trim() != "undefined")
                {
                    model.Transactor = context.Request.QueryString["Transactor"].ToString();
                }
                OutDateStart = context.Request.QueryString["OutDateStart"].ToString();
                OutDateEnd = context.Request.QueryString["OutDateEnd"].ToString();
                SendNo = context.Request.QueryString["SendNo"].ToString();
                string ord = orderBy + " " + order;
                int TotalCount = 0;
                DataTable dt = StorageOutSellBus.GetStorageOutSellTableBycondition(model, OutDateStart, OutDateEnd, SendNo,EFIndex,EFDesc, pageIndex, pageCount, ord, ref TotalCount);


                //XElement dsXML = ConvertDataTableToXML(StorageOutSellBus.GetStorageOutSellTableBycondition(model, OutDateStart, OutDateEnd, SendNo));
                ////linq排序
                //var dsLinq =
                //    (order == "ascending") ?
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value ascending
                //     select new DataSourceModel()
                //     {

                //         ID = x.Element("ID").Value,
                //         OutNo = x.Element("OutNo").Value,
                //         Title = x.Element("Title").Value,
                //         SendNo = x.Element("SendNo").Value,
                //         CountTotal = x.Element("CountTotal").Value,
                //         OutDate = x.Element("OutDate").Value,
                //         TotalPrice = x.Element("TotalPrice").Value,
                //         Summary = x.Element("Summary").Value,
                //         BillStatusName = x.Element("BillStatusName").Value,
                //         Transactor = x.Element("Transactor").Value,
                //         DeptName = x.Element("DeptName").Value,

                //     })
                //              :
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value descending
                //     select new DataSourceModel()
                //     {
                //         ID = x.Element("ID").Value,
                //         OutNo = x.Element("OutNo").Value,
                //         Title = x.Element("Title").Value,
                //         SendNo = x.Element("SendNo").Value,
                //         CountTotal = x.Element("CountTotal").Value,
                //         OutDate = x.Element("OutDate").Value,
                //         TotalPrice = x.Element("TotalPrice").Value,
                //         Summary = x.Element("Summary").Value,
                //         BillStatusName = x.Element("BillStatusName").Value,
                //         Transactor = x.Element("Transactor").Value,
                //         DeptName = x.Element("DeptName").Value,

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
        public string ID { get; set; }
        public string OutNo { get; set; }
        public string Title { get; set; }
        public string SendNo { get; set; }//销售发货通知单编号
        public string CountTotal { get; set; }
        public string OutDate { get; set; }
        public string TotalPrice { get; set; }
        public string Summary { get; set; }
        public string BillStatusName { get; set; }
        public string Transactor { get; set; }
        public string DeptName { get; set; }
    }

    public class DataSourceSellSendModle
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string SendNo { get; set; }
        public string CreatDate { get; set; }
    }

}
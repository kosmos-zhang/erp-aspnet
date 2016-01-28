<%@ WebHandler Language="C#" Class="StorageInProcessList" %>

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

public class StorageInProcessList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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

                XBase.Model.Office.ProductionManager.ManufactureTaskModel model = new XBase.Model.Office.ProductionManager.ManufactureTaskModel();
                model.CompanyCD = companyCD;
                model.TaskNo = context.Request.QueryString["txtNo_UC"];
                model.Subject = context.Request.QueryString["txtTitle_UC"];

                DataTable dt = StorageInProcessBus.GetMTList(model);
                XElement dsXML = ConvertDataTableToXML(dt);
                //linq排序
                var dsLinq =
                    (order == "ascending") ?
                    (from x in dsXML.Descendants("Data")
                     orderby x.Element(orderBy).Value ascending
                     select new DataSourceProcessModle()
                     {
                         ID = x.Element("ID").Value,
                         TaskNo = x.Element("TaskNo").Value,
                         Title = x.Element("Title").Value,
                         CreatDate = x.Element("CreateDate").Value,
                     })
                              :
                    (from x in dsXML.Descendants("Data")
                     orderby x.Element(orderBy).Value descending
                     select new DataSourceProcessModle()
                     {
                         ID = x.Element("ID").Value,
                         TaskNo = x.Element("TaskNo").Value,
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

                //获取数据
                StorageInProcessModel model = new StorageInProcessModel();
                string EnterDateStart = string.Empty;
                string EnterDateEnd = string.Empty;
                string StorageID = string.Empty;
                string FromBillNo = string.Empty;
                model.CompanyCD = companyCD;
                model.InNo = context.Request.QueryString["InNo"].Trim();
                model.Title = context.Request.QueryString["Title"].Trim();
                if (context.Request.QueryString["ProcessDept"].Trim() != "undefined")
                {
                    model.ProcessDept = context.Request.QueryString["ProcessDept"].ToString().Trim();
                }
                if (context.Request.QueryString["InPutDept"].Trim() != "undefined")
                {
                    model.DeptID = context.Request.QueryString["InPutDept"].ToString().Trim();
                }
                model.BillStatus = context.Request.QueryString["BillStatus"].ToString().Trim();
                if (context.Request.QueryString["Executor"].Trim() != "undefined")
                {
                    model.Executor = context.Request.QueryString["Executor"].ToString().Trim();
                }
                model.EFIndex = context.Request.QueryString["EFIndex"].ToString();
                model.EFDesc = context.Request.QueryString["EFDesc"].ToString();

                model.Processor = context.Request.QueryString["Processor"].ToString().Trim();
                EnterDateStart = context.Request.QueryString["EnterDateStart"].ToString().Trim();
                EnterDateEnd = context.Request.QueryString["EnterDateEnd"].ToString().Trim();
                StorageID = context.Request.QueryString["StorageID"].ToString().Trim();
                FromBillNo = context.Request.QueryString["FromBillNo"].ToString().Trim();
                string BatchNo = context.Request.QueryString["BatchNo"].ToString().Trim();

                string ord = orderBy + " " + order;
                int TotalCount = 0;
                DataTable dt = StorageInProcessBus.GetStorageInProcessTableBycondition(BatchNo,model, EnterDateStart, EnterDateEnd, StorageID, FromBillNo, pageIndex, pageCount, ord, ref TotalCount);

                //XElement dsXML = ConvertDataTableToXML(StorageInProcessBus.GetStorageInProcessTableBycondition(model, EnterDateStart, EnterDateEnd, StorageID, FromBillNo));
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
                //         TaskNo = x.Element("TaskNo").Value,
                //         ProcessDept = x.Element("ProcessDeptName").Value,
                //         ProcessType = x.Element("ProcessType").Value,
                //         Processor = x.Element("Processor").Value,
                //         InPutDept = x.Element("InPutDeptName").Value,
                //         Executor = x.Element("Executor").Value,
                //         EnterDate = x.Element("EnterDate").Value,
                //         CountTotal = x.Element("CountTotal").Value,
                //         TotalPrice = x.Element("TotalPrice").Value,
                //         BillStatus = x.Element("BillStatusName").Value,
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
                //         TaskNo = x.Element("TaskNo").Value,
                //         ProcessDept = x.Element("ProcessDeptName").Value,
                //         ProcessType = x.Element("ProcessType").Value,
                //         Processor = x.Element("Processor").Value,
                //         InPutDept = x.Element("InPutDeptName").Value,
                //         Executor = x.Element("Executor").Value,
                //         EnterDate = x.Element("EnterDate").Value,
                //         CountTotal = x.Element("CountTotal").Value,
                //         TotalPrice = x.Element("TotalPrice").Value,
                //         BillStatus = x.Element("BillStatusName").Value,
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
        //入库单编号、入库单主题、生产任务单、加工类别、加工单位、
        //生产负责人、部门、人库人、入库时间、入库数量、入库金额、摘要、单据状态

        public string ID { get; set; }
        public string InNo { get; set; }
        public string Title { get; set; }
        public string TaskNo { get; set; }
        public string ProcessType { get; set; }//加工类别
        public string ProcessDept { get; set; }//加工部门
        public string Processor { get; set; }
        public string InPutDept { get; set; }
        public string Executor { get; set; }
        public string EnterDate { get; set; }
        public string CountTotal { get; set; }
        public string TotalPrice { get; set; }
        public string Summary { get; set; }
        public string BillStatus { get; set; }
    }

    public class DataSourceProcessModle
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string TaskNo { get; set; }
        public string CreatDate { get; set; }
    }

}
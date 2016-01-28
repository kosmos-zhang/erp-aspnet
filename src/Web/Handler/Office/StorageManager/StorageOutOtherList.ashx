<%@ WebHandler Language="C#" Class="StorageOutOtherList" %>

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

public class StorageOutOtherList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "OutDate";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_a"))
            {
                order = "asc";//排序：降序
            }
            int pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

            //获取数据
            StorageOutOtherModel model = new StorageOutOtherModel();
            string OutDateStart = string.Empty;
            string OutDateEnd = string.Empty;
            string SendNo = string.Empty;
            model.CompanyCD = companyCD;
            model.OutNo = context.Request.QueryString["OutNo"].Trim();
            model.Title = context.Request.QueryString["Title"].Trim();
            model.FromType = context.Request.QueryString["FromType"].Trim();
            model.ReasonType = context.Request.QueryString["ReasonType"];
            model.BillStatus = context.Request.QueryString["BillStatus"];
            model.BatchNo = context.Request.QueryString["BatchNo"];//批次
            model.ProjectID = context.Request.QueryString["ProjectID"];//批次
            if (context.Request.QueryString["Transactor"].Trim() != "undefined")
            {
                model.Transactor = context.Request.QueryString["Transactor"].ToString();
            }
            OutDateStart = context.Request.QueryString["OutDateStart"].ToString();
            OutDateEnd = context.Request.QueryString["OutDateEnd"].ToString();

            //扩展属性条件
            string EFIndex = context.Request.QueryString["EFIndex"].ToString();
            string EFDesc = context.Request.QueryString["EFDesc"].ToString();
            
            string ord = orderBy + " " + order;
            int TotalCount = 0;
            DataTable dt = StorageOutOtherBus.GetStorageOutOtherTableBycondition(model, OutDateStart, OutDateEnd,EFIndex,EFDesc, pageIndex, pageCount, ord, ref TotalCount);

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
        public string OutNo { get; set; }
        public string Title { get; set; }
        public string FromTypeName { get; set; }
        public string CountTotal { get; set; }
        public string OutDate { get; set; }
        public string TotalPrice { get; set; }
        public string Summary { get; set; }
        public string BillStatusName { get; set; }
        public string Transactor { get; set; }
        public string ReasonTypeName { get; set; }
    }
}
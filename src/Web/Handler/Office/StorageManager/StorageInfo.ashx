<%@ WebHandler Language="C#" Class="WorkCenterList" %>
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

public class WorkCenterList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //[待修改]
        //private string companyCD = "AAAAAA";
        if (context.Request.RequestType == "POST")
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
            StorageModel model = new StorageModel();
            model.StorageNo = context.Request.QueryString["StorageNo"].ToString();
            model.StorageName = context.Request.QueryString["StorageName"].ToString();
            model.StorageType = context.Request.QueryString["StorageType"].ToString();
            model.UsedStatus = context.Request.QueryString["UsedStatus"].ToString();
            model.CompanyCD = companyCD;

            string ord = orderBy + " " + order;
            int TotalCount = 0;

            DataTable dt = StorageBus.GetLitBycondition(model, pageIndex, pageCount, ord, ref TotalCount);
            //XElement dsXML = ConvertDataTableToXML(StorageBus.GetStorageListBycondition(model));
            //linq排序
            //var dsLinq =
            //    (order == "ascending") ?
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value ascending
            //     select new DataSourceModel()
            //     {

            //         ID = x.Element("ID").Value,
            //         StorageNo = x.Element("StorageNo").Value,
            //         StorageName = x.Element("StorageName").Value,
            //         StorageType = x.Element("StorageType").Value,
            //         UsedStatus = x.Element("UsedStatus").Value,
            //         Remark = x.Element("Remark").Value
            //     })

            //              :
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value descending
            //     select new DataSourceModel()
            //     {

            //         ID = x.Element("ID").Value,
            //         StorageNo = x.Element("StorageNo").Value,
            //         StorageName = x.Element("StorageName").Value,
            //         StorageType = x.Element("StorageType").Value,
            //         UsedStatus = x.Element("UsedStatus").Value,
            //         Remark = x.Element("Remark").Value
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
        public string StorageNo { get; set; }
        public string StorageName { get; set; }
        public string StorageType { get; set; }
        public string UsedStatus { get; set; }
        public string Remark { get; set; }
    }

}
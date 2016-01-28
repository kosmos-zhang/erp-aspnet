<%@ WebHandler Language="C#" Class="SubStorageList" %>

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
using XBase.Business.Office.SubStoreManager;
using XBase.Model.Office.SubStoreManager;
using System.Web.SessionState;
using System.Text;

public class SubStorageList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        if (context.Request.RequestType == "POST")
        {
            int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            string Action = context.Request.Params["action"];
            //设置行为参数
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "DESC";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProductID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_a"))
            {
                order = "ASC";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

            string DeptID = context.Request.Form["DeptID"];
            string ProductID = context.Request.Form["ProductID"];
            string ProductName = context.Request.Form["ProductName"];
            string BatchNo = context.Request.Form["BatchNo"];
            int TotalCount = 0;

            orderBy = orderBy + " " + order;
            context.Response.ContentType = "text/plain";
            DataTable dt = SubStorageBus.SelectSubStorageProduct(pageIndex, pageCount, orderBy, ref TotalCount, DeptID, ProductID, ProductName, BatchNo);

            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count == 0)
            {
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(TotalCount.ToString());
                sb.Append(",data:");
                sb.Append("0");
                sb.Append("}");
            }
            else
                sb.Append(JsonClass.FormatDataTableToJson(dt, TotalCount));
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
        public string ProductID { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string Specification { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string DeptID { get; set; }
        public string DeptName { get; set; }
        public string ProductCount { get; set; }
    }

}
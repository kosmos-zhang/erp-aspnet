<%@ WebHandler Language="C#" Class="PurchaseApplyNoPass" %>

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
using System.Web.SessionState;

public class PurchaseApplyNoPass : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            int pageCount = 10;
            string orderString = context.Request.QueryString["orderby"].ToString();//排序
            string order = "asc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "NotPassNum";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
                order = "desc";//排序：降序
            pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
            string myOrder = orderBy + " " + order;

            ////设置行为参数
            //string orderString = (context.Request.QueryString["orderBy"]);//排序
            //string order = "ascending";//排序：升序
            //string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "NotPassNum";//要排序的字段，如果为空，默认为"ID"
            //if (orderString.EndsWith("_d"))
            //{
            //    order = "descending";//排序：降序
            //}
            //int pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
            //int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
            //int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数


            DateTime EndCheckDate = Convert.ToDateTime("9999-2-3");
            string BeginDate = context.Request.QueryString["BeginDate"].ToString().Trim();
            string EndDate = context.Request.QueryString["EndDate"].ToString().Trim();
            string CustID = context.Request.QueryString["CustID"].ToString();
            string TotalProductCount = "0";
            string TotalNotPassCount = "0";
            int TotalCount = 0;
            DataTable dt = PurchaseApplyNoPassBus.SearchPurNoPass("0",BeginDate, EndDate, CustID, myOrder, pageIndex, pageCount, ref TotalProductCount, ref TotalNotPassCount, ref TotalCount);

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
            //  }
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

        public string CustName { get; set; }
        public string ProductName { get; set; }
        public string Specification { get; set; }
        public string UnitName { get; set; }
        public string ProductCount { get; set; }
        public string NotPassNum { get; set; }

    }
    

}
<%@ WebHandler Language="C#" Class="PurchaseRejectOverview" %>

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
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;
using System.Web.SessionState;
using System.Text;

public class PurchaseRejectOverview : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        if (context.Request.RequestType == "POST")
        {
            //int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;

            //设置行为参数
            string orderBy = (context.Request.Form["orderby"].ToString());//排序
            //string order = "DESC";//排序：升序
            //string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            //if (orderString.EndsWith("_a"))
            //{
            //    order = "ASC";//排序：降序
            //}
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

            string ProductID = context.Request.Form["ProductID"];
            string Reason = context.Request.Form["Reason"];
            string StartRejectDate = context.Request.Form["StartRejectDate"];
            string EndRejectDate = context.Request.Form["EndRejectDate"];
            int TotalCount = 0;


            //orderBy = orderBy + " " + order;


            ////获取数据
            //string[] str = new string[4];
            //if (context.Request.Params["ProductID"] != null && context.Request.Params["ProductID"] != "")
            //{
            //    str[0] = context.Request.Params["ProductID"].ToString();
            //}
            //else
            //{
            //    str[0] = "";
            //}
            //if (context.Request.Params["Reason"] != null && context.Request.Params["Reason"] != "")
            //{
            //    str[1] = context.Request.Params["Reason"].ToString();
            //}
            //else
            //{
            //    str[1] = "";
            //}
            //if (context.Request.Params["StartRejectDate"].Trim() != null && context.Request.Params["StartRejectDate"].Trim() != "")
            //{
            //    str[2] = context.Request.Params["StartRejectDate"].ToString();
            //}
            //else
            //{
            //    str[2] = "";
            //}
            //if (context.Request.Params["EndRejectDate"].Trim() != null && context.Request.Params["EndRejectDate"].Trim() != "")
            //{
            //    str[3] = context.Request.Params["EndRejectDate"].ToString();
            //}
            //else
            //{
            //    str[3] = "";
            //}


            //XElement dsXML = ConvertDataTableToXML(PurchaseRejectBus.SelectPurchaseRejectOverview(str));
            ////linq排序
            //var dsLinq =
            //    (order == "ascending") ?
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value ascending
            //     select new DataSourceModel()
            //     {
            //         ID = x.Element("ID").Value,
            //         ProductID = x.Element("ProductID").Value,
            //         ProductNo = x.Element("ProductNo").Value,
            //         ProductName = x.Element("ProductName").Value,
            //         BackCount = x.Element("BackCount").Value,
            //         OutedTotal = x.Element("OutedTotal").Value,
            //         RejectDate = x.Element("RejectDate").Value,
            //         TotalFee = x.Element("TotalFee").Value,
            //     })
            //              :
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value descending
            //     select new DataSourceModel()
            //     {
            //         ID = x.Element("ID").Value,
            //         ProductID = x.Element("ProductID").Value,
            //         ProductNo = x.Element("ProductNo").Value,
            //         ProductName = x.Element("ProductName").Value,
            //         BackCount = x.Element("BackCount").Value,
            //         OutedTotal = x.Element("OutedTotal").Value,
            //         RejectDate = x.Element("RejectDate").Value,
            //         TotalFee = x.Element("TotalFee").Value,
            //     });
            //int totalCount = dsLinq.Count();
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("{");
            //sb.Append("totalCount:");
            //sb.Append(totalCount.ToString());
            //sb.Append(",data:");
            //sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
            //sb.Append("}");

            //context.Response.ContentType = "text/plain";
            //context.Response.Write(sb.ToString());
            //context.Response.End();

            context.Response.ContentType = "text/plain";
            DataTable dt = PurchaseRejectBus.SelectPurchaseRejectOverview(pageIndex, pageCount, orderBy, ref TotalCount, ProductID, Reason, StartRejectDate, EndRejectDate);
            //string temp = JsonClass.FormatDataTableToJson(dt, TotalCount);
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
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    ////数据源结构
    //public class DataSourceModel
    //{
    //    public string ID { get; set; }
    //    public string ProductID { get; set; }
    //    public string ProductNo { get; set; }
    //    public string ProductName { get; set; }
    //    public string BackCount { get; set; }
    //    public string OutedTotal { get; set; }
    //    public string RejectDate { get; set; }
    //    public string TotalFee { get; set; }
    //}

}
<%@ WebHandler Language="C#" Class="ProviderContactHistoryWarning" %>

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

public class ProviderContactHistoryWarning : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        if (context.Request.RequestType == "POST")
        {
            int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            string action = context.Request.Form["action"].ToString();
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            //设置行为参数
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "DESC";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_a"))
            {
                order = "ASC";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            int TotalCount = 0;
            orderBy = orderBy + " " + order;


            ////获取数据
            //DataTable dt = ProviderContactHistoryBus.SelectProviderContactDelay(CompanyCD);

            //XElement dsXML = ConvertDataTableToXML(dt);
            ////linq排序
            //var dsLinq =
            //    (order == "ascending") ?
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value ascending
            //     select new DataSourceModel()
            //     {
            //         ID = x.Element("ID").Value,
            //         CustName = x.Element("CustName").Value,
            //         LinkCycleName = x.Element("LinkCycleName").Value,
            //         DelayDays = x.Element("DelayDays").Value,
            //         LinkDate = x.Element("LinkDate").Value,
            //         LinkerName = x.Element("LinkerName").Value,
            //     })
            //              :
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value descending
            //     select new DataSourceModel()
            //     {
            //         ID = x.Element("ID").Value,
            //         CustName = x.Element("CustName").Value,
            //         LinkCycleName = x.Element("LinkCycleName").Value,
            //         DelayDays = x.Element("DelayDays").Value,
            //         LinkDate = x.Element("LinkDate").Value,
            //         LinkerName = x.Element("LinkerName").Value,
            //     });
            //int totalCount = dsLinq.Count();
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //if (dt.Rows.Count == 0)
            //{
            //    //sb.Append("{");
            //    //sb.Append("data:");
            //    //sb.Append(JsonClass.DataTable2Json(dtp));
            //    ////sb.Append("}");
            //    //sb.Append(",data2:[{");
            //    //sb.Append(" ID:null}]");
            //    //sb.Append(",IsCite:[{");
            //    //sb.Append("IsCite:" + str);
            //    //sb.Append("}]}");
                
            //    sb.Append("{");
            //    sb.Append("totalCount:");
            //    sb.Append(totalCount.ToString());
            //    sb.Append(",data:[{");
            //    sb.Append("ID:null}]");
            //    sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
            //    sb.Append("}"); 
            //}
            //else
            //{
            //    sb.Append("{");
            //    sb.Append("totalCount:");
            //    sb.Append(totalCount.ToString());
            //    sb.Append(",data:");
            //    sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
            //    sb.Append("}");
            //}

            //context.Response.ContentType = "text/plain";
            //context.Response.Write(sb.ToString());
            //context.Response.End();

            context.Response.ContentType = "text/plain";
            //string temp = JsonClass.DataTable2Json();
            DataTable dt = ProviderContactHistoryBus.SelectProviderContactDelay(pageIndex, pageCount, orderBy, ref TotalCount, CompanyCD);
            
        
            StringBuilder sb = new StringBuilder();
            
            if (dt!=null && dt.Rows.Count ==0)
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

    //数据源结构
    public class DataSourceModel
    {
        public string ID { get; set; }
        public string CustName { get; set; }
        public string LinkCycleName { get; set; }
        public string DelayDays { get; set; }
        public string LinkDate { get; set; }
        public string LinkerName { get; set; }
    }

}
<%@ WebHandler Language="C#" Class="ContactDeferInfo" %>

using System;
using System.Web;
using XBase.Business.Office.CustManager;
using System.Data;
using System.Xml.Linq;
using XBase.Common;
using System.IO;
using System.Web.Script.Serialization;
using System.Linq;

public class ContactDeferInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "linkdate";//要排序的字段，如果为空，默认为"linkmanname"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //int Manager = Convert.ToInt32(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);//当前用户ID

        string ord = orderBy + " " + order;
        int totalCount = 0;

        DataTable dt = ContactHistoryBus.GetContactDefer(CompanyCD, pageIndex, pageCount, ord, ref totalCount);

        //linq排序
        //var dsLinq =
        //    (order == "ascending") ?
        //    (from x in dsXML.Descendants("Data")
        //     orderby x.Element(orderBy).Value ascending
        //     select new DataSourceModel()
        //     {
        //         id = x.Element("id").Value,
        //         custno = x.Element("custno").Value,
        //         custnam = x.Element("custnam").Value,
        //         custtype = x.Element("custtype").Value,
        //         linkcycle = x.Element("linkcycle").Value,
        //         days = x.Element("days").Value,
        //         linkdate = x.Element("linkdate").Value,
        //         EmployeeName = x.Element("EmployeeName").Value
        //     }):
        //    (from x in dsXML.Descendants("Data")
        //     orderby x.Element(orderBy).Value descending
        //     select new DataSourceModel()
        //     {
        //         id = x.Element("id").Value,
        //         custno = x.Element("custno").Value,
        //         custnam = x.Element("custnam").Value,
        //         custtype = x.Element("custtype").Value,
        //         linkcycle = x.Element("linkcycle").Value,
        //         days = x.Element("days").Value,
        //         linkdate = x.Element("linkdate").Value,
        //         EmployeeName = x.Element("EmployeeName").Value
        //     });

        //int totalCount = dsLinq.Count();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt == null)
            sb.Append("[{\"id\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt)); 
            //sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    //把DataTable转换为XML流
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

    public class DataSourceModel
    {
        public string id { get; set; }
        public string custno { get; set; }
        public string custnam { get; set; }
        public string custtype { get; set; }
        public string linkcycle { get; set; }
        public string days { get; set; }
        public string linkdate { get; set; }
        public string EmployeeName { get; set; }       
    }

}
<%@ WebHandler Language="C#" Class="ProviderSelect" %>

using System;
using System.Web;
using System.Data;
using System.Xml.Linq;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using System.IO;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Serialization;
using XBase.Common;
using System.Linq;
public class ProviderSelect : IHttpHandler, System.Web.SessionState.IRequiresSessionState {

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string orderString = (context.Request.Form["orderbyProvider"].ToString());//排序
            string order = "ascending";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProviderID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCountProvider"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndexProvider"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

            //获取数据
            ProviderInfoModel model = new ProviderInfoModel();
            if (context.Request.Form["ProviderID"].ToString() != "")
            {
                model.ID = Convert.ToInt32(context.Request.Form["ProviderID"].ToString().Trim());
            }
            if (!string.IsNullOrEmpty(context.Request.Form["ProviderNo"].ToString().Trim()))
            {
                model.CustNo = context.Request.Form["ProviderNo"].ToString();
            }
            if (!string.IsNullOrEmpty(context.Request.Form["ProviderName"].ToString().Trim()))
            {
                model.CustName = context.Request.Form["ProviderName"].ToString();
            }


            XElement dsXML = ConvertDataTableToXML(ProviderInfoBus.GetProviderSelect(model));
            //linq排序
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     ProviderID = x.Element("ProviderID").Value,
                     ProviderNo = x.Element("ProviderNo").Value,
                     ProviderName = x.Element("ProviderName").Value,
                     TakeType=x.Element("TakeType").Value,
                     TakeTypeName = x.Element("TakeTypeName").Value,
                     CarryType = x.Element("CarryType").Value,
                     CarryTypeName = x.Element("CarryTypeName").Value,
                     PayType = x.Element("PayType").Value,
                     PayTypeName = x.Element("PayTypeName").Value,
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     ProviderID = x.Element("ProviderID").Value,
                     ProviderNo = x.Element("ProviderNo").Value,
                     ProviderName = x.Element("ProviderName").Value,
                     TakeType = x.Element("TakeType").Value,
                     TakeTypeName = x.Element("TakeTypeName").Value,
                     CarryType = x.Element("CarryType").Value,
                     CarryTypeName = x.Element("CarryTypeName").Value,
                     PayType = x.Element("PayType").Value,
                     PayTypeName = x.Element("PayTypeName").Value,
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
        public string ProviderID { get; set; }
        public string ProviderNo { get; set; }
        public string ProviderName { get; set; }
        public string TakeType { get; set; }
        public string TakeTypeName { get; set; }
        public string CarryType { get; set; }
        public string CarryTypeName { get; set; }
        public string PayType { get; set; }
        public string PayTypeName { get; set; }

    }

}
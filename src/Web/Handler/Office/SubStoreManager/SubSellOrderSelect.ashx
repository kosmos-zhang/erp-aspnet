<%@ WebHandler Language="C#" Class="SubSellOrderSelect" %>

using System;
using System.Web;
using XBase.Model.Office.SubStoreManager;
using XBase.Business.Office.SubStoreManager;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using System.Web.SessionState;

public class SubSellOrderSelect : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            int id = 0;
            //设置行为参数
            string orderString = (context.Request.Form["orderbySubSellOrder"]);//排序
            string order = "ascending";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCountSubSellBack"]);//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndexSubSellBack"]);//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            int DeptID = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID.ToString());//分店ID
            DataRow dr = SubStorageDBHelper.GetSubDeptFromDeptID(DeptID.ToString());
            if (dr != null)
            {
                DeptID = int.Parse(dr["ID"].ToString());
            }
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码

            string OrderNo = context.Request.Form["OrderNo"];
            int CurrencyTypeID = 0;
            if (int.TryParse(context.Request.Form["CurrencyTypeID"], out id))
            {
                CurrencyTypeID = id;
            }
            string Rate = context.Request.Form["Rate"];
            string SendMode = context.Request.Form["SendMode"];

            XElement dsXML = ConvertDataTableToXML(SubSellBackBus.GetSubSellBackDetail(DeptID, OrderNo, SendMode, CompanyCD, CurrencyTypeID, Rate));
            //linq排序
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     ID = x.Element("ID").Value,
                     OrderNo = x.Element("OrderNo").Value,
                     SendMode = x.Element("SendMode").Value,
                     SendModeName = x.Element("SendModeName").Value,
                     OutDate = x.Element("OutDate").Value,
                     OutUserID = x.Element("OutUserID").Value,
                     OutUserName = x.Element("OutUserName").Value,
                     CustName = x.Element("CustName").Value,
                     CustTel = x.Element("CustTel").Value,
                     CustMobile = x.Element("CustMobile").Value,
                     CustAddr = x.Element("CustAddr").Value,
                     CurrencyType = x.Element("CurrencyType").Value,
                     CurrencyTypeName = x.Element("CurrencyTypeName").Value,
                     Rate = x.Element("Rate").Value,
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     ID = x.Element("ID").Value,
                     OrderNo = x.Element("OrderNo").Value,
                     SendMode = x.Element("SendMode").Value,
                     SendModeName = x.Element("SendModeName").Value,
                     OutDate = x.Element("OutDate").Value,
                     OutUserID = x.Element("OutUserID").Value,
                     OutUserName = x.Element("OutUserName").Value,
                     CustName = x.Element("CustName").Value,
                     CustTel = x.Element("CustTel").Value,
                     CustMobile = x.Element("CustMobile").Value,
                     CustAddr = x.Element("CustAddr").Value,
                     CurrencyType = x.Element("CurrencyType").Value,
                     CurrencyTypeName = x.Element("CurrencyTypeName").Value,
                     Rate = x.Element("Rate").Value,
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
        public string ID { get; set; }
        public string OrderNo { get; set; }
        public string SendMode { get; set; }
        public string SendModeName { get; set; }
        public string OutDate { get; set; }
        public string OutUserID { get; set; }
        public string OutUserName { get; set; }
        public string CustName { get; set; }
        public string CustTel { get; set; }
        public string CustMobile { get; set; }
        public string CustAddr { get; set; }
        public string CurrencyType { get; set; }
        public string CurrencyTypeName { get; set; }
        public string Rate { get; set; }
    }

}
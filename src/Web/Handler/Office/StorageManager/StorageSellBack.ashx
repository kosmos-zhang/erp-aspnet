<%@ WebHandler Language="C#" Class="StorageSellBack" %>

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

public class StorageSellBack : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string companyCD = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//[待修改]
        string action = (context.Request.Form["action"].ToString());//操作
        if (action == "getinfo")//弹出层信息
        {
            GetLsit(context);
        }
        else if (action == "list")//获取基本信息和根据传过来的DetailID数组获取明细
        {
            GetDetailList(context);
        }
    }


    private void GetDetailList(HttpContext context)
    {
        string strDetailIDList = string.Empty;
        strDetailIDList = (context.Request.Form["DetailIDList"].ToString());//明细信息的ID组合字符串
        //string strMTNo = context.Request.Form["strSBNo"].ToString();
        string strJson = string.Empty;
        //string strMTJson = string.Empty;//采购单基本信息
        string strSBListJson = string.Empty;//采购单单明细信息
        strJson = "{";
        if (StorageSellBackBus.GetInfoByDetalIDList(strDetailIDList, companyCD).Rows.Count > 0)
        {
            //strMTJson = JsonClass.DataTable2Json(ManufactureTaskInfoBus.GetMTInfo(strMTNo, companyCD));
            //strJson += "\"MT\":" + strMTJson;
            strSBListJson += "\"SBList\":" + JsonClass.DataTable2Json(StorageSellBackBus.GetInfoByDetalIDList(strDetailIDList, companyCD));
            strJson += strSBListJson;
        }
        strJson += "}";
        context.Response.Write(strJson);
    }

    /// <summary>
    /// 获取数据列表
    /// </summary>
    private void GetLsit(HttpContext context)//弹出层信息
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderByOff"].ToString());//排序
        string order = "descending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CreateDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ascending";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageOffCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        string txtBackNo = context.Request.Form["txtBackNo"].ToString();
        string txtTitle = context.Request.Form["txtTitle"].ToString();

        DataTable dt = StorageSellBackBus.GetSBDetailInfo(companyCD, txtBackNo, txtTitle);
        XElement dsXML = ConvertDataTableToXML(dt);
        //linq排序
        var dsLinq =
            (order == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderBy).Value ascending
             select new DataSourceModel()
             {

                 ID = x.Element("ID").Value,
                 BackNo = x.Element("BackNo").Value,
                 BackCargoTheme = x.Element("BackCargoTheme").Value,
                 CreateDate = x.Element("CreateDate").Value,
                 ProductNo = x.Element("ProdNo").Value,
                 ProductName = x.Element("ProductName").Value,
                 ColorName = x.Element("ColorName").Value,
                 BackNumber = x.Element("BackNumber").Value,
                 InNumber = x.Element("InNumber").Value,
                 NotInCount = x.Element("NotInCount").Value,
                 DetailID = x.Element("DetailID").Value,
                 
                 UnitName = x.Element("UnitName").Value,
                 JiBenCount = x.Element("JiBenCount").Value,
                 UnitPrice = x.Element("UnitPrice").Value

             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderBy).Value descending
             select new DataSourceModel()
             {
                 ID = x.Element("ID").Value,
                 BackNo = x.Element("BackNo").Value,
                 BackCargoTheme = x.Element("BackCargoTheme").Value,
                 CreateDate = x.Element("CreateDate").Value,
                 ProductNo = x.Element("ProdNo").Value,
                 ProductName = x.Element("ProductName").Value,
                 ColorName = x.Element("ColorName").Value,
                 BackNumber = x.Element("BackNumber").Value,
                 InNumber = x.Element("InNumber").Value,
                 NotInCount = x.Element("NotInCount").Value,
                 DetailID = x.Element("DetailID").Value,

                 UnitName = x.Element("UnitName").Value,
                 JiBenCount = x.Element("JiBenCount").Value,
                 UnitPrice = x.Element("UnitPrice").Value
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

    //数据源结构
    public class DataSourceModel
    {
        public string ID { get; set; }
        public string BackNo { get; set; }
        public string BackCargoTheme { get; set; }
        public string Creator { get; set; }
        public string CreateDate { get; set; }

        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        public string BackNumber { get; set; }
        public string InNumber { get; set; }
        public string NotInCount { get; set; }
        public string DetailID { get; set; }

        public string UnitPrice { get; set; }
        public string UnitName { get; set; }
        public string JiBenCount { get; set; }  
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
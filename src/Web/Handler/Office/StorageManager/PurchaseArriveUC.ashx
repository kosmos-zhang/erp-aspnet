<%@ WebHandler Language="C#" Class="PurchaseArriveUC" %>

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

public class PurchaseArriveUC : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    string companyCD = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//[待修改]
        if (context.Request.RequestType == "POST")
        {
            string action = (context.Request.Form["actionOff"].ToString());//操作
            if (action == "getinfo")//弹出层信息
            {
                GetLsit(context);
            }
            else if (action == "info")
            {
                GetPurchaseArrive(context);
            }
            else if (action == "list")
            {
                GetArriveList(context);
            }

        }
    }


    private void GetArriveList(HttpContext context)
    {
        string strDetailIDList = string.Empty;
        strDetailIDList = (context.Request.Form["DetailIDList"].ToString());//明细信息的ID组合字符串
        string strPANo=context.Request.Form["strPANo"].ToString();
        string strJson = string.Empty;
        string strPAJson = string.Empty;//采购单基本信息
        string strPAListJson = string.Empty;//采购单单明细信息
        strJson = "{";
        if (PurchaseArriveInfoBus.GetPAInfo(strPANo, companyCD).Rows.Count >0)
        {
            strPAJson = JsonClass.DataTable2Json(PurchaseArriveInfoBus.GetPAInfo(strPANo, companyCD));
            strJson += "\"PA\":" + strPAJson;
            strPAListJson += ",\"PAList\":" + JsonClass.DataTable2Json(PurchaseArriveInfoBus.GetInfoByArriveList(strDetailIDList, companyCD));
            strJson += strPAListJson;
        }
        strJson += "}";
        context.Response.Write(strJson);
    }
    /// <summary>
    /// 获取报价单信息
    /// </summary>
    /// <param name="context"></param>
    private void GetPurchaseArrive(HttpContext context)
    {
        string strPANo = string.Empty;//采购到货单编号
        string strPAJson = string.Empty;//采购单基本信息
        string strPAListJson = string.Empty;//采购单单明细信息
        string strJson = string.Empty;//采购单单信息

        strPANo = (context.Request.Form["strPANo"].ToString());//采购单编号

        strJson = "{";
        //判断单据信息是否仍然存在
        if (PurchaseArriveInfoBus.GetPAInfo(strPANo, companyCD).Rows.Count == 1)
        {
            strPAJson = JsonClass.DataTable2Json(PurchaseArriveInfoBus.GetPAInfo(strPANo, companyCD));
            strJson += "\"PA\":" + strPAJson;
            //判断单据是否存在明细信息
            if (PurchaseArriveInfoBus.GetPADetailInfo(strPANo, companyCD).Rows.Count > 0)
            {
                strPAListJson = JsonClass.DataTable2Json(PurchaseArriveInfoBus.GetPADetailInfo(strPANo, companyCD));
                strJson += ",\"PAList\":" + strPAListJson;
            }
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
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ascending";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageOffCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        string txtArriveNo = string.Empty;
        string txtTitle = string.Empty;
        txtArriveNo = context.Request.Form["txtArriveNo"].ToString();
        txtTitle = context.Request.Form["txtTitle"].ToString();
        
        DataTable dt = PurchaseArriveInfoBus.GetPurchaseArriveInfo(companyCD,txtArriveNo,txtTitle);
        XElement dsXML = ConvertDataTableToXML(dt);
        //linq排序
        var dsLinq =
            (order == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderBy).Value ascending
             select new DataSourceModel()
             {
                 ID = x.Element("ID").Value,
                 ArriveNo = x.Element("ArriveNo").Value,
                 Title = x.Element("Title").Value,
                 ProviderID = x.Element("ProviderName").Value,
                 Purchaser = x.Element("Purchaser").Value,
                 ProductNo = x.Element("ProductNo").Value,
                 ProductName = x.Element("ProductName").Value,
                 ColorName = x.Element("ColorName").Value,
                 
                 TotalPrice = x.Element("TotalPrice").Value,
                 ProductCount = x.Element("ProductCount").Value,
                 InCount = x.Element("InCount").Value,
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
                 ArriveNo = x.Element("ArriveNo").Value,
                 Title = x.Element("Title").Value,
                 ProviderID = x.Element("ProviderName").Value,
                 Purchaser = x.Element("Purchaser").Value,
                 ProductNo = x.Element("ProductNo").Value,
                 ProductName = x.Element("ProductName").Value,
                 ColorName = x.Element("ColorName").Value,
                 
                 TotalPrice = x.Element("TotalPrice").Value,
                 ProductCount = x.Element("ProductCount").Value,
                 InCount = x.Element("InCount").Value,
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
        public string ArriveNo { get; set; }
        public string Title { get; set; }
        public string ProviderID { get; set; }
        public string Purchaser { get; set; }
        public string Creator { get; set; }
        public string CreateDate { get; set; }

        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        
        public string ProductCount { get; set; }
        public string UnitPrice { get; set; }
        public string TotalPrice { get; set; }
        public string InCount { get; set; }
        public string DetailID { get; set; }

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
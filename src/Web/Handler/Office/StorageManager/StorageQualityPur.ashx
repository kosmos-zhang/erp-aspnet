<%@ WebHandler Language="C#" Class="StorageQualityPur" %>

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
using XBase.Business.Office.SellManager;
using XBase.Model.Office.SellManager;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using XBase.Business.Office.StorageManager;

public class StorageQualityPur : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    //需修改
    string companyCD = "";
    public void ProcessRequest(HttpContext context)
    {
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (context.Request.RequestType == "POST")
        {
            if (context.Request.QueryString["Action"] != null && context.Request.QueryString["Action"].ToString() == "Pur")
            {
                GetPurList(context);
            }
            else
            {
                //设置行为参数
                string orderString = (context.Request.Form["orderbyPur"].ToString());//排序
                string order = "descending";//排序：
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                {
                    order = "ascending";//排序：
                }
                int pageCount = int.Parse(context.Request.Form["pageCountPur"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["currentPageIndexPur"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
                string FromType = context.Request.Form["FromType"].ToString();

                string ProductName = context.Request.Form["myProductName1"].ToString();
                string ProNO = context.Request.Form["myProNo1"].ToString();
                string QuaStr = ProNO + "?" + ProductName;

                XElement dsXML = ConvertDataTableToXML(StorageQualityCheckPro.GetStorageQualityCheckPro("1", QuaStr));   //参数1 是页面源单类型的标识      
                //linq排序
                var dsLinq =
                    (order == "ascending") ?
                    (from x in dsXML.Descendants("Data")
                     orderby x.Element(orderBy).Value ascending
                     select new DataSourceModel()
                     {
                         ProID = x.Element("ProID").Value,
                         ProductID = x.Element("ProductID").Value,
                         UnitID = x.Element("UnitID").Value,
                         ProductName = x.Element("ProductName").Value,
                         ProductNo = x.Element("ProductNo").Value,
                         CodeName = x.Element("CodeName").Value,
                         FromBillID = x.Element("FromBillID").Value,
                         FromLineNo = x.Element("FromLineNo").Value,
                         Remark = x.Element("Remark").Value,
                         QuaCheckedCount = x.Element("QuaCheckedCount").Value,
                         CompanyCD = x.Element("CompanyCD").Value,
                         SortNo = x.Element("SortNo").Value,
                         ApplyCheckCount = x.Element("ApplyCheckCount").Value,
                         FromTypeName = x.Element("FromTypeName").Value,
                         ProductCount = x.Element("ProductCount").Value,
                         CheckedCount = x.Element("CheckedCount").Value,
                         PurID = x.Element("PurID").Value,
                         FromType = x.Element("FromType").Value,
                         ID = x.Element("PurID").Value,
                         FromBillNo = x.Element("FromBillNo").Value,
                         UsedUnitID = x.Element("UsedUnitID").Value,
                         UsedUnitCount = x.Element("UsedUnitCount").Value,
                         UsedUnitName = x.Element("UsedUnitName").Value,

                     })
                              :
                    (from x in dsXML.Descendants("Data")
                     orderby x.Element(orderBy).Value descending
                     select new DataSourceModel()
                     {
                         ProID = x.Element("ProID").Value,
                         ProductID = x.Element("ProductID").Value,
                         UnitID = x.Element("UnitID").Value,
                         ProductName = x.Element("ProductName").Value,
                         ProductNo = x.Element("ProductNo").Value,
                         CodeName = x.Element("CodeName").Value,
                         FromBillID = x.Element("FromBillID").Value,
                         FromLineNo = x.Element("FromLineNo").Value,
                         Remark = x.Element("Remark").Value,
                         QuaCheckedCount = x.Element("QuaCheckedCount").Value,
                         CompanyCD = x.Element("CompanyCD").Value,
                         SortNo = x.Element("SortNo").Value,
                         ApplyCheckCount = x.Element("ApplyCheckCount").Value,
                         FromTypeName = x.Element("FromTypeName").Value,
                         ProductCount = x.Element("ProductCount").Value,
                         CheckedCount = x.Element("CheckedCount").Value,
                         PurID = x.Element("PurID").Value,
                         FromType = x.Element("FromType").Value,
                         ID = x.Element("PurID").Value,
                         FromBillNo = x.Element("FromBillNo").Value,
                         UsedUnitID = x.Element("UsedUnitID").Value,
                         UsedUnitCount = x.Element("UsedUnitCount").Value,
                         UsedUnitName = x.Element("UsedUnitName").Value,
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
    }
    /// <summary>
    /// 填充采购相应的明细
    /// </summary>
    /// <param name="context"></param>
    private void GetPurList(HttpContext context)
    {
        string strDetailIDList = string.Empty;
        strDetailIDList = (context.Request.QueryString["DetailIDList"].ToString());//明细信息的ID组合字符串
        string strJson = string.Empty;
        string strPurListJson = string.Empty;//采购单单明细信息
        strJson = "{";

        strJson += "\"PA\":" + JsonClass.DataTable2Json(StorageQualityCheckPro.GetPurDetail(strDetailIDList, companyCD));

        strJson += "}";
        context.Response.Write(strJson);
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
        public string ProID { get; set; }
        public string CompanyCD { get; set; }
        public string SortNo { get; set; }
        public string ProductID { get; set; }
        public string UnitID { get; set; }
        public string ProductName { get; set; }
        public string QuaCheckedCount { get; set; }
        public string Remark { get; set; }
        public string FromBillID { get; set; }
        public string FromLineNo { get; set; }
        public string ProductNo { get; set; }
        public string CodeName { get; set; }
        public string ApplyCheckCount { get; set; }
        public string FromTypeName { get; set; }
        public string ProductCount { get; set; }
        public string CheckedCount { get; set; }
        public string PurID { get; set; }
        public string FromType { get; set; }
        public string ID { get; set; }
        public string FromBillNo { get; set; }
        public string UsedUnitID { get; set; }
        public string UsedUnitCount { get; set; }
        public string UsedUnitName { get; set; }

    }


}
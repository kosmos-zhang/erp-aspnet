<%@ WebHandler Language="C#" Class="StorageQualityManufacture" %>

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

public class StorageQualityManufacture : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (context.Request.RequestType == "POST")
        {
            if (context.Request.QueryString["Action"] != null && context.Request.QueryString["Action"].ToString() == "Man")
            {
                GetManList(context);
            }
            else
            {
                //设置行为参数
                string orderString = (context.Request.Form["orderbyMan"].ToString());//排序
                string order = "descending";//排序：
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                {
                    order = "ascending";//排序：
                }
                int pageCount = int.Parse(context.Request.Form["pageCountMan"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["currentPageIndexMan"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
                string ProductName = context.Request.Form["ProductName"].ToString();
                string ProNO = context.Request.Form["ProductNo"].ToString();
                string QuaStr = ProNO + "?" + ProductName;
                DataTable dt = StorageQualityCheckPro.GetStorageQualityCheckPro("2", QuaStr);
                XBase.Common.StringUtil.DecimalFormatPoint(int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint), dt);
                XElement dsXML = ConvertDataTableToXML(dt);     //参数2为获取生产相关信息  
                //linq排序
                var dsLinq =
                    (order == "ascending") ?
                    (from x in dsXML.Descendants("Data")
                     orderby x.Element(orderBy).Value ascending
                     select new DataSourceModel()
                     {
                         ProductName = x.Element("ProductName").Value,
                         ProdNo = x.Element("ProdNo").Value,
                         CodeName = x.Element("CodeName").Value,
                         ApplyCheckCount = x.Element("ApplyCheckCount").Value,
                         FromBillID = x.Element("FromBillID").Value,
                         ManID = x.Element("ManID").Value,
                         FromLineNo = x.Element("FromLineNo").Value,
                         ProductID = x.Element("ProductID").Value,
                         UnitID = x.Element("UnitID").Value,
                         Principal = x.Element("Principal").Value,
                         EmployeeName = x.Element("EmployeeName").Value,
                         DeptID = x.Element("DeptID").Value,
                         DeptName = x.Element("DeptName").Value,
                         CheckedCount = x.Element("CheckedCount").Value,
                         InCount = x.Element("InCount").Value,
                         FromBillNo = x.Element("FromBillNo").Value,
                         ManCheckCount = x.Element("ManCheckCount").Value,
                         UsedUnitID = x.Element("UsedUnitID").Value,
                         UsedUnitCount = x.Element("UsedUnitCount").Value,
                         UsedUnitName = x.Element("UsedUnitName").Value,

                     })
                              :
                    (from x in dsXML.Descendants("Data")
                     orderby x.Element(orderBy).Value descending
                     select new DataSourceModel()
                     {
                         ProductName = x.Element("ProductName").Value,
                         ProdNo = x.Element("ProdNo").Value,
                         CodeName = x.Element("CodeName").Value,
                         ApplyCheckCount = x.Element("ApplyCheckCount").Value,
                         FromBillID = x.Element("FromBillID").Value,
                         ManID = x.Element("ManID").Value,
                         FromLineNo = x.Element("FromLineNo").Value,
                         ProductID = x.Element("ProductID").Value,
                         UnitID = x.Element("UnitID").Value,
                         Principal = x.Element("Principal").Value,
                         EmployeeName = x.Element("EmployeeName").Value,
                         DeptID = x.Element("DeptID").Value,
                         DeptName = x.Element("DeptName").Value,
                         CheckedCount = x.Element("CheckedCount").Value,
                         InCount = x.Element("InCount").Value,
                         FromBillNo = x.Element("FromBillNo").Value,
                         ManCheckCount = x.Element("ManCheckCount").Value,
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

    /// <summary>
    /// 填充采购相应的明细
    /// </summary>
    /// <param name="context"></param>
    private void GetManList(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string strDetailIDList = string.Empty;
        strDetailIDList = (context.Request.QueryString["DetailIDList"].ToString());//明细信息的ID组合字符串
        string strJson = string.Empty;
        string strPurListJson = string.Empty;//采购单单明细信息
        strJson = "{";

        strJson += "\"PA\":" + JsonClass.DataTable2Json(StorageQualityCheckPro.GetManDetail(strDetailIDList, companyCD));

        strJson += "}";
        context.Response.Write(strJson);
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
        public string ProductName { get; set; }
        public string ProdNo { get; set; }
        public string CodeName { get; set; }
        public string ApplyCheckCount { get; set; }
        public string FromBillID { get; set; }
        public string ManID { get; set; }
        public string FromLineNo { get; set; }
        public string ProductID { get; set; }
        public string UnitID { get; set; }
        public string EmployeeName { get; set; }
        public string Principal { get; set; }
        public string DeptName { get; set; }
        public string DeptID { get; set; }
        public string CheckedCount { get; set; }
        public string InCount { get; set; }
        public string FromBillNo { get; set; }
        public string ManCheckCount { get; set; }
        public string UsedUnitID { get; set; }
        public string UsedUnitCount { get; set; }

        public string UsedUnitName { get; set; }

    }


}
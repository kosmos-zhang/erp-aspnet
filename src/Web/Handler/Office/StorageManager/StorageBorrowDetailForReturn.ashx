<%@ WebHandler Language="C#" Class="StorageBorrowDetailForReturn" %>

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
public class StorageBorrowDetailForReturn : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string BorrowNo = context.Request.Form["BorrowNo"] == null ? string.Empty : context.Request.Form["BorrowNo"].ToString();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        string orderString = (context.Request.Form["orderby"] == null ? string.Empty : context.Request.Form["orderby"]);//排序
        string order = "ascending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "SortNo";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ascending";//排序：降序
        }
        DataTable dt = XBase.Business.Office.StorageManager.StorageReturnBus.GetStroageBorrowDetail(CompanyCD, BorrowNo);
        XElement dsXML = ConvertDataTableToXML(dt);
        var dsLinq =
         (order == "ascending") ?
         (from x in dsXML.Descendants("Data")
          orderby x.Element(orderBy).Value ascending
          select new DataSourceModel()
          {
              ID = GetElementValue(x, "ID", false),
              ProductName = GetElementValue(x, "ProductName", false),
              ProductNo = GetElementValue(x, "ProdNo", false),
              ProductSpec = GetElementValue(x, "Specification", false),
              ProductUnit = GetElementValue(x, "CodeName", false),
              ReturnCount = GetElementValue(x, "ReturnCount", false),
              SortNo = GetElementValue(x, "SortNo", false),
              UnitPrice = GetElementValue(x, "UnitPrice", false),
              ProductUnitID = GetElementValue(x, "UnitID", false),
              ProductID = GetElementValue(x, "ProductID", false),
              RealReturnCount = GetElementValue(x, "RealReturnCount", false),
              ProductCount = GetElementValue(x, "ProductCount", false),
              BorrowNo = GetElementValue(x, "BorrowNo", false),
              UsedUnitID = GetElementValue(x, "UsedUnitID", false),
              UsedUnitCount = GetElementValue(x, "UsedUnitCount", false),
              UsedPrice = GetElementValue(x, "UsedPrice", false),
              ExRate = GetElementValue(x, "ExRate", false),
              BatchNo=GetElementValue(x,"BatchNo",false)
          })

                   :
         (from x in dsXML.Descendants("Data")
          orderby x.Element(orderBy).Value descending
          select new DataSourceModel()
          {
              ID = GetElementValue(x, "ID", false),
              ProductName = GetElementValue(x, "ProductName", false),
              ProductNo = GetElementValue(x, "ProdNo", false),
              ProductSpec = GetElementValue(x, "Specification", false),
              ProductUnit = GetElementValue(x, "CodeName", false),
              ReturnCount = GetElementValue(x, "ReturnCount", false),
              SortNo = GetElementValue(x, "SortNo", false),
              UnitPrice = GetElementValue(x, "UnitPrice", false),
              ProductUnitID = GetElementValue(x, "UnitID", false),
              ProductID = GetElementValue(x, "ProductID", false),
              RealReturnCount = GetElementValue(x, "RealReturnCount",false),
              ProductCount=GetElementValue(x,"ProductCount",false),
              BorrowNo = GetElementValue(x, "BorrowNo", false),
              UsedUnitID = GetElementValue(x, "UsedUnitID", false),
              UsedUnitCount = GetElementValue(x, "UsedUnitCount", false),
              UsedPrice = GetElementValue(x, "UsedPrice", false),
              ExRate = GetElementValue(x, "ExRate", false),
              BatchNo = GetElementValue(x, "BatchNo", false)
          });
        int totalCount = dsLinq.Count();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        sb.Append(ToJSON(dsLinq.ToList()));
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


    public class DataSourceModel
    {
        public string ID { get; set; }
        public string ProductID { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ProductSpec { get; set; }
        public string ProductUnit { get; set; }
        public string ProductUnitID { get; set; }
        public string ReturnCount { get; set; }
        public string UnitPrice { get; set; }
        public string SortNo { get; set; }
        public string RealReturnCount { get; set; }
        public string ProductCount { get; set; }
        public string BorrowNo { get; set; }
        public string UsedUnitID { get; set; }
        public string UsedUnitCount { get; set; }
        public string UsedPrice { get; set; }
        public string ExRate { get; set; }
        public string BatchNo { get; set; }
    }

    //判断节点是否存在 及格式化日期
    protected string GetElementValue(XElement x, string Key, bool IsDate)
    {
        if (x.Element(Key) != null)
        {
            string tempValue = x.Element(Key).Value;
            if (IsDate)
            {
                if (string.IsNullOrEmpty(tempValue))
                {
                    return string.Empty;
                }
                else
                    return Convert.ToDateTime(tempValue).ToString("yyyy-MM-dd");
            }
            else
                return x.Element(Key).Value;
        }
        else
            return string.Empty;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
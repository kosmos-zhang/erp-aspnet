<%@ WebHandler Language="C#" Class="StorageBorrowDetailList" %>

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

public class StorageBorrowDetailList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        UserInfoUtil userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string CompanyCD = userinfo.CompanyCD;
        string BorrowNo = context.Request.Form["BorrowNo"].ToString();

        DataTable dt = XBase.Business.Office.StorageManager.StorageBorrowBus.GetStorageBorrowDetail(CompanyCD, BorrowNo);
        XElement dsXML = ConvertDataTableToXML(dt);
        //设置行为参数&
        string orderString = (context.Request.Form["orderby"] == null ? string.Empty : context.Request.Form["orderby"]);//排序
        string order = "descending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ascending";//排序：降序
        }
        
        var dsLinq =
               (order == "ascending") ?
               (from x in dsXML.Descendants("Data")
                orderby x.Element(orderBy).Value ascending
                select new DataSourceModel()
                {

                    ID = GetElementValue(x, "ID", false),
                    ProductID = GetElementValue(x, "ProductID", false),
                    CodeName = GetElementValue(x, "CodeName", false),
                    ProdNo = GetElementValue(x, "ProdNo", false),
                    ProductCount = GetElementValue(x, "ProductCount", false),
                    ProductName = GetElementValue(x, "ProductName", false),
                    Specification = GetElementValue(x, "Specification", false),
                    StandardCost = GetElementValue(x, "UnitPrice", false),
                    UnitID = GetElementValue(x, "UnitID", false),
                    BorrowCount = GetElementValue(x, "ProductCount", false),
                    BorrowPrice = GetElementValue(x, "TotalPrice", false),
                    ReturnCount = GetElementValue(x, "ReturnCount", false),
                    ReturnDate = GetElementValue(x, "ReturnDate", true),
                    SortNo = GetElementValue(x, "SortNo", false),
                    UseCount = GetElementValue(x, "UseCount", false),
                    Remark = GetElementValue(x, "Remark", false),
                    MinusIs = GetElementValue(x, "MinusIs", false),
                    FlowStatus = GetElementValue(x, "FlowStatus", false),
                    RealReturnCount = GetElementValue(x, "RealReturnCount", false),
                    UsedUnitID = GetElementValue(x, "UsedUnitID", false),
                    UsedUnitCount = GetElementValue(x, "UsedUnitCount", false),
                    UsedPrice = GetElementValue(x, "UsedPrice", false),
                    ExRate = GetElementValue(x, "ExRate", false),
                    BatchNo = GetElementValue(x, "BatchNo", false)
                })

                         :
               (from x in dsXML.Descendants("Data")
                orderby x.Element(orderBy).Value descending
                select new DataSourceModel()
                {

                    ID = GetElementValue(x,"ID",false),
                    ProductID =GetElementValue(x,"ProductID",false),
                    CodeName = GetElementValue(x,"CodeName",false),
                    ProdNo = GetElementValue(x,"ProdNo",false),
                    ProductCount =GetElementValue(x,"ProductCount",false),
                    ProductName = GetElementValue(x,"ProductName",false),
                    Specification =GetElementValue(x,"Specification",false),
                    StandardCost =GetElementValue(x,"UnitPrice",false),
                    UnitID =GetElementValue(x,"UnitID",false),
                    BorrowCount = GetElementValue(x,"ProductCount",false),
                    BorrowPrice = GetElementValue(x,"TotalPrice",false),
                    ReturnCount =GetElementValue(x,"ReturnCount",false),
                    ReturnDate = GetElementValue(x,"ReturnDate",true),
                    SortNo =GetElementValue(x,"SortNo",false),
                    UseCount=GetElementValue(x,"UseCount",false),
                    Remark=GetElementValue(x,"Remark",false),
                    MinusIs =GetElementValue(x,"MinusIs",false),
                    FlowStatus = GetElementValue(x,"FlowStatus",false),
                    RealReturnCount = GetElementValue(x, "RealReturnCount",false),
                    UsedUnitID = GetElementValue(x, "UsedUnitID", false),
                    UsedUnitCount = GetElementValue(x, "UsedUnitCount", false),
                    UsedPrice = GetElementValue(x, "UsedPrice", false),
                    ExRate = GetElementValue(x, "ExRate", false),
                    BatchNo=GetElementValue(x,"BatchNo",false)
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
        public string ProdNo { get; set; }
        public string ProductName { get; set; }
        public string CodeName { get; set; }
        public string ProductCount { get; set; }
        public string StandardCost { get; set; }
        public string Specification { get; set; }
        public string UnitID { get; set; }
        public string BorrowCount { get; set; }
        public string BorrowPrice { get; set; }
        public string ReturnDate { get; set; }
        public string ReturnCount { get; set; }
        public string SortNo { get; set; }
        public string UseCount { get; set; }
        public string Remark { get; set; }
        public string MinusIs { get; set; }
        public string FlowStatus { get; set; }
        public string RealReturnCount { get; set; }
        public string UsedUnitID { get; set; }
        public string UsedUnitCount { get; set; }
        public string UsedPrice { get; set; }
        public string ExRate { get; set; }
        public string BatchNo { get; set; }
        
    }



    public bool IsReusable
    {
        get
        {
            return false;
        }
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

}
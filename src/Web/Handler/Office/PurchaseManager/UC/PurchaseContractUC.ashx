<%@ WebHandler Language="C#" Class="PurchaseContractUC" %>

using System;
using System.Web;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Model.Office.ProductionManager;
using System.Web.SessionState;

public class PurchaseContractUC : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string action = context.Request.Params["action"];
            if (action == "GetDetail")
            {
                string orderString = context.Request.Params["orderby"];//排序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length) : "ID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                {
                }
                int pageCount = int.Parse(context.Request.Params["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Params["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

                string ContractNo = context.Request.Params["ContractNo"].ToString();
                XElement dsXML = ConvertDataTableToXML(PurchaseOrderBus.GetPurOrderDetailByContractNo(ContractNo));
                var dsLinq =
                    (from x in dsXML.Descendants("Data")
                     orderby x.Element(orderBy).Value ascending
                     select new DataSourceModel()
                     {
                         ProductID = x.Element("ProductID").Value,
                         ProductNo = x.Element("ProductNo").Value,
                         ProductName = x.Element("ProductName").Value,
                         standard = x.Element("standard").Value,
                         ProductCount = x.Element("ProductCount").Value,
                         OrderCount = x.Element("OrderCount").Value,
                         UnitID = x.Element("UnitID").Value,
                         UnitName = x.Element("UnitName").Value,
                         UnitPrice = x.Element("UnitPrice").Value,
                         TotalPrice = x.Element("TotalPrice").Value,
                         RequireDate = x.Element("RequireDate").Value,
                         TaxPrice = x.Element("TaxPrice").Value,
                         Discount = x.Element("Discount").Value,
                         TaxRate = x.Element("TaxRate").Value,
                         TotalFee = x.Element("TotalFee").Value,
                         TotalTax = x.Element("TotalTax").Value,
                         FromBillID = x.Element("FromBillID").Value,
                         FromBillNo = x.Element("FromBillNo").Value,
                         FromLineNo = x.Element("FromLineNo").Value,
                         Remark = x.Element("Remark").Value,

                         Purchaser = x.Element("Purchaser").Value,
                         PurchaserName = x.Element("PurchaserName").Value,
                         DeptID = x.Element("DeptID").Value,
                         DeptName = x.Element("DeptName").Value,
                         
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

            else if (action == null)
            {
                string OrderBy = context.Request.Form["OrderBy"].ToString().Trim();
                string OrderByType = context.Request.Form["OrderByType"].ToString().Trim();
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

                string ContactNo = context.Request.Form["ContactNo"];
                string Title = context.Request.Form["Title"];
                string Provider = context.Request.Form["Provider"];
                string StartDate = context.Request.Form["StartDate"];
                string EndDate = context.Request.Form["EndDate"];
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                int totalCount = 0;

                DataTable dt = PurchaseOrderBus.GetPurContract(CompanyCD,Provider, ContactNo, Title, StartDate, EndDate, pageIndex, pageCount, OrderBy, OrderByType, ref totalCount);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                sb.Append(JsonClass.DataTable2Json(dt));
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
        public string ContractNo { get; set; }
        public string ProviderID { get; set; }
        public string Title { get; set; }

        public string TypeID { get; set; }
        public string TakeType { get; set; }
        public string CarryType { get; set; }
        public string PayType { get; set; }
        public string MoneyType { get; set; }
        public string isAddTax { get; set; }
        
        public string ProductID { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string standard { get; set; }
        public string ProductCount { get; set; }
        public string OrderCount { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string UnitPrice { get; set; }
        public string TotalPrice { get; set; }
        public string RequireDate { get; set; }
        public string TaxPrice { get; set; }
        public string Discount { get; set; }
        public string TaxRate { get; set; }
        public string TotalFee { get; set; }
        public string TotalTax { get; set; }
        public string FromBillID { get; set; }
        public string FromBillNo { get; set; }
        public string FromLineNo { get; set; }
        public string Remark { get; set; }
        
        public string Purchaser{ get; set; }
        public string PurchaserName{ get; set; }
        public string DeptID{ get; set; }
        public string DeptName { get; set; }
        
    }
    

}
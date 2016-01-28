<%@ WebHandler Language="C#" Class="PurchasePlanUC" %>

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

public class PurchasePlanUC : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            
            int ProviderID = Convert.ToInt32(context.Request.Form["ProviderID"]);
            string OrderBy = context.Request.Form["OrderBy"].ToString().Trim();
            string OrderByType = context.Request.Form["OrderByType"];
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

            string ProductNo = context.Request.Form["ProductNo"];
            string ProductName = context.Request.Form["ProductName"];
            string StartDate = context.Request.Form["StartDate"];
            string EndDate = context.Request.Form["EndDate"];
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            int totalCount = 0;



            DataTable dtdata = PurchaseOrderDBHelper.GetPurPlanByProvider(CompanyCD, ProviderID, ProductNo, ProductName, StartDate, EndDate, pageIndex, pageCount, OrderBy, OrderByType, ref totalCount);
            DataTable dt = GetNewDataTable(dtdata, string.Empty, OrderBy + " " + OrderByType);
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
            ////linq排序
            //var dsLinq =
            //    (order == "ascending") ?
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value ascending
            //     select 
            //     new DataSourceModel()
            //     {
            //         ProductID = x.Element("ProductID").Value,
            //         ProductNo = x.Element("ProductNo").Value,
            //         ProductName = x.Element("ProductName").Value,
            //         Specification = x.Element("Specification").Value,
            //         ProductCount = x.Element("ProductCount").Value,
            //         OrderCount = x.Element("OrderCount").Value,
            //         UnitID = x.Element("UnitID").Value,
            //         UnitName = x.Element("UnitName").Value,
            //         UnitPrice = x.Element("UnitPrice").Value,
            //         InTaxRate = x.Element("InTaxRate").Value,
            //         TaxPrice = x.Element("TaxPrice").Value,
            //         TotalPrice = x.Element("TotalPrice").Value,
            //         RequireDate = x.Element("RequireDate").Value,
            //         FromBillID = x.Element("FromBillID").Value,
            //         FromBillNo = x.Element("FromBillNo").Value,
            //         FromLineNo = x.Element("FromLineNo").Value,

            //         TypeID = x.Element("TypeID").Value,
            //         Purchaser = x.Element("Purchaser").Value,
            //         PurchaserName = x.Element("PurchaserName").Value,
            //         PlanDeptID = x.Element("PlanDeptID").Value,
            //         PlanDeptName = x.Element("PlanDeptName").Value,                                       
            //     })
            //              :
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value descending
            //     select new DataSourceModel()
            //     {
            //         ProductID = x.Element("ProductID").Value,
            //         ProductNo = x.Element("ProductNo").Value,
            //         ProductName = x.Element("ProductName").Value,
            //         Specification = x.Element("Specification").Value,
            //         ProductCount = x.Element("ProductCount").Value,
            //         OrderCount = x.Element("OrderCount").Value,
            //         UnitID = x.Element("UnitID").Value,
            //         UnitName = x.Element("UnitName").Value,
            //         UnitPrice = x.Element("UnitPrice").Value,
            //         InTaxRate = x.Element("InTaxRate").Value,
            //         TaxPrice = x.Element("TaxPrice").Value,
            //         TotalPrice = x.Element("TotalPrice").Value,
            //         RequireDate = x.Element("RequireDate").Value,                     
            //         FromBillID = x.Element("FromBillID").Value,
            //         FromBillNo = x.Element("FromBillNo").Value,
            //         FromLineNo = x.Element("FromLineNo").Value,

            //         TypeID = x.Element("TypeID").Value,
            //         Purchaser = x.Element("Purchaser").Value,
            //         PurchaserName = x.Element("PurchaserName").Value,
            //         PlanDeptID = x.Element("PlanDeptID").Value,
            //         PlanDeptName = x.Element("PlanDeptName").Value,
            //     });
            //int totalCount = dsLinq.Count();
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("{");
            //sb.Append("totalCount:");
            //sb.Append(totalCount.ToString());
            //sb.Append(",data:");
            //sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
            //sb.Append("}");

            //context.Response.ContentType = "text/plain";
            //context.Response.Write(sb.ToString());
            //context.Response.End();
        }
    }
    private DataTable GetNewDataTable(DataTable dt, string condition, string Order)
    {
        DataTable newdt = new DataTable();
        newdt = dt.Clone();
        DataRow[] dr = dt.Select(condition, Order);
        for (int i = 0; i < dr.Length; i++)
        {
            newdt.ImportRow((DataRow)dr[i]);
        }
        return newdt;//返回的查询结果
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
        public string ProductID {get;set;}
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string Specification { get; set; }
        public string ProductCount { get; set; }
        public string OrderCount { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string UnitPrice { get; set; }
        public string TaxPrice { get; set; }
        public string InTaxRate { get; set; }
        public string TotalPrice { get; set; }
        public string RequireDate { get; set; }
        public string FromBillID { get; set; }
        public string FromBillNo { get; set; }
        public string FromLineNo { get; set; }

        public string TypeID { get; set; }
        public string Purchaser { get; set; }
        public string PurchaserName { get; set; }
        public string PlanDeptID { get; set; }
        public string PlanDeptName { get; set; }
    }

}
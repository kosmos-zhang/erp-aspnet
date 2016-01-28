<%@ WebHandler Language="C#" Class="PurchaseHistoryAskPrice" %>

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
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;
using System.Web.SessionState;
using System.Text;

public class PurchaseHistoryAskPrice : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        if (context.Request.RequestType == "POST")
        {
            int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;

            //设置行为参数
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "DESC";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProductID";//要排序的字段，如果为空，默认为"ProductID"
            if (orderString.EndsWith("_a"))
            {
                order = "ASC";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数


            string ProductID = context.Request.Form["ProductID"];
            string StartPurchaseDate = context.Request.Form["StartPurchaseDate"];
            string EndPurchaseDate = context.Request.Form["EndPurchaseDate"];
            int TotalCount = 0;


            orderBy = orderBy + " " + order;
            
            

            ////获取数据
            //string[] str = new string[3];
            //if (context.Request.Params["ProductID"] != null && context.Request.Params["ProductID"] != "")
            //{
            //    str[0] = context.Request.Params["ProductID"].ToString();
            //}
            //else
            //{
            //    str[0] = "";
            //}
            //if (context.Request.Params["StartPurchaseDate"].Trim() != null && context.Request.Params["StartPurchaseDate"].Trim() != "")
            //{
            //    str[1] = context.Request.Params["StartPurchaseDate"].ToString();
            //}
            //else
            //{
            //    str[1] = "";
            //}
            //if (context.Request.Params["EndPurchaseDate"].Trim() != null && context.Request.Params["EndPurchaseDate"].Trim() != "")
            //{
            //    str[2] = context.Request.Params["EndPurchaseDate"].ToString();
            //}
            //else
            //{
            //    str[2] = "";
            //}


            //XElement dsXML = ConvertDataTableToXML(PurchaseOrderBus.SelectPurchaseHistoryAskPrice(str));
            ////linq排序
            //var dsLinq =
            //    (order == "ascending") ?
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value ascending
            //     select new DataSourceModel()
            //     {
            //         ProductID = x.Element("ProductID").Value,
            //         ProductNo = x.Element("ProductNo").Value,
            //         ProductName = x.Element("ProductName").Value,
            //         Specification = x.Element("Specification").Value,
            //         UnitID = x.Element("UnitID").Value,
            //         UnitName = x.Element("UnitName").Value,
            //         LargeTaxPrice = x.Element("LargeTaxPrice").Value,
            //         SmallTaxPrice = x.Element("SmallTaxPrice").Value,
            //         AverageTaxPrice = x.Element("AverageTaxPrice").Value,
            //         NewTaxPrice = x.Element("NewTaxPrice").Value,
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
            //         UnitID = x.Element("UnitID").Value,
            //         UnitName = x.Element("UnitName").Value,
            //         LargeTaxPrice = x.Element("LargeTaxPrice").Value,
            //         SmallTaxPrice = x.Element("SmallTaxPrice").Value,
            //         AverageTaxPrice = x.Element("AverageTaxPrice").Value,
            //         NewTaxPrice = x.Element("NewTaxPrice").Value,
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

            context.Response.ContentType = "text/plain";
            //string temp = JsonClass.DataTable2Json();
            DataTable dt = PurchaseOrderBus.SelectPurchaseHistoryAskPrice(pageIndex, pageCount, orderBy, ref TotalCount, ProductID, StartPurchaseDate, EndPurchaseDate);

            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                sb.Append(JsonClass.FormatDataTableToJson(dt, TotalCount));
            }
            else
            {
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(TotalCount.ToString());
                sb.Append(",data:");
                sb.Append("0");
                sb.Append("}"); 
            }
          
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
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    ////数据源结构
    //public class DataSourceModel
    //{
    //    public string ProductID { get; set; }
    //    public string ProductNo { get; set; }
    //    public string ProductName { get; set; }
    //    public string Specification { get; set; }
    //    public string UnitID { get; set; }
    //    public string UnitName { get; set; }
    //    public string LargeTaxPrice { get; set; }
    //    public string SmallTaxPrice { get; set; }
    //    public string AverageTaxPrice { get; set; }
    //    public string NewTaxPrice { get; set; }
    //}

}
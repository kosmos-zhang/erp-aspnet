<%@ WebHandler Language="C#" Class="SellProductReport" %>


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
using System.Web.SessionState;

public class SellProductReport : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //设置行为参数
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "asc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
                order = "desc";//排序：降序
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            string myOrder = orderBy + " " + order;
            int TotalCount = 0;
            string BeginTime = context.Request.Form["BeginTime"].ToString();
            string EndTime = context.Request.Form["EndTime"].ToString();
            string CurrencyType = context.Request.Form["CurrencyType"].ToString();
            string TotalProductCount = "0", TotalTaxBuy = "0", TotalStandardBuy = "0";
            DataTable dt=SellProductReportBus.GetSellProductReport("0",pageIndex,pageCount,myOrder,BeginTime, EndTime,CompanyCD,CurrencyType,ref TotalProductCount,ref TotalTaxBuy,ref TotalStandardBuy,ref TotalCount);
            ////linq排序
            //var dsLinq =
            //    (order == "ascending") ?
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value ascending
            //     select new DataSourceModel()
            //     {
            //         ProNo = x.Element("ProNo").Value,
            //         ProductName = x.Element("ProductName").Value,
            //         StandardSell = x.Element("StandardSell").Value,
            //         SellTax = x.Element("SellTax").Value,
            //         ProductCount = x.Element("ProductCount").Value,
            //         TaxBuy = x.Element("TaxBuy").Value,
            //         StandardBuy = x.Element("StandardBuy").Value,
            //         Specification = x.Element("Specification").Value,
            //         TypeName = x.Element("TypeName").Value,
            //         CodeName = x.Element("CodeName").Value,
            //         Flag = x.Element("Flag").Value,
            //         ID = x.Element("ID").Value,
            //         Precent = x.Element("Precent").Value,
            //         Tot = x.Element("Tot").Value, 

            //     })
            //              :
            //    (from x in dsXML.Descendants("Data")
            //     orderby x.Element(orderBy).Value descending
            //     select new DataSourceModel()
            //     {
            //         ProNo = x.Element("ProNo").Value,
            //         ProductName = x.Element("ProductName").Value,
            //         StandardSell = x.Element("StandardSell").Value,
            //         SellTax = x.Element("SellTax").Value,
            //         ProductCount = x.Element("ProductCount").Value,
            //         TaxBuy = x.Element("TaxBuy").Value,
            //         StandardBuy = x.Element("StandardBuy").Value,
            //         Specification = x.Element("Specification").Value,
            //         TypeName = x.Element("TypeName").Value,
            //         CodeName = x.Element("CodeName").Value,
            //         Flag = x.Element("Flag").Value,
            //         ID = x.Element("ID").Value,
            //         Precent = x.Element("Precent").Value,
            //         Tot = x.Element("Tot").Value, 
                    


            //     });
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(TotalCount.ToString());
            sb.Append(",data:");
            if (dt.Rows.Count == 0)
                sb.Append("[{\"ID\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append("}");
            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();

        }
    }
    ////数据源结构
    //public class DataSourceModel
    //{
    //    public string ID { get; set; }
    //    public string ProNo { get; set; }
    //    public string ProductName { get; set; }
    //    public string StandardSell { get; set; }
    //    public string SellTax { get; set; }
    //    public string ProductCount { get; set; }
    //    public string TaxBuy { get; set; }
    //    public string StandardBuy { get; set; }
    //    public string Specification { get; set; }
    //    public string TypeName { get; set; }
    //    public string CodeName { get; set; }
    //    public string Flag { get; set; }
    //    public string Precent { get; set; }
    //    public string Tot { get; set; }

    //}
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


    
}
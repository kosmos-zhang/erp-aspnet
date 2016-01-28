<%@ WebHandler Language="C#" Class="StorageBorrow_Add" %>
/*
*库存借货异步页面
*/
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


public class StorageBorrow_Add : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "descending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ascending";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageProductInfocount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        string ProductName = context.Request.Form["ProductName"].ToString();
        string ProdNo = context.Request.Form["ProdNo"].ToString();

       //设置查询参数
        string CompanyCD =((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        int BorrowDeptID = 0; //Convert.ToInt32(context.Request.Form["BorrowDeptID"].ToString());
        int StorageID = Convert.ToInt32(context.Request.Form["StorageID"].ToString());
        
        ///*解析自定义属性参数*/.

        string EFIndex = "";
        string EFDesc = "";
        if (context.Request.Form["EFIndex"] != null && context.Request.Form["EFDesc"] != null)
        {
            EFIndex = context.Request.Form["EFIndex"].ToString().Trim();
            EFDesc = context.Request.Form["EFDesc"].ToString().Trim();
        }

        DataTable dt = XBase.Business.Office.StorageManager.StorageBorrowBus.GetProductInfo(CompanyCD, BorrowDeptID, StorageID,ProductName,ProdNo,EFIndex,EFDesc);
        XElement dsXML = ConvertDataTableToXML(dt);

        var dsLinq =
               (order == "ascending") ?
               (from x in dsXML.Descendants("Data")
                orderby x.Element(orderBy) == null ? string.Empty : x.Element(orderBy).Value ascending
                select new DataSourceModel()
                {

                    ID = GetElementValue(x, "ID", false),
                    ProductID = GetElementValue(x, "ProductID", false),
                    CodeName = GetElementValue(x, "CodeName", false),
                    ProdNo = GetElementValue(x, "ProdNo", false),
                    ProductCount = GetElementValue(x, "ProductCount", false),
                    ProductName = GetElementValue(x, "ProductName", false),
                    Specification = GetElementValue(x, "Specification", false),
                    StandardCost = GetElementValue(x, "StandardCost", false),
                    IsBatchNo = GetElementValue(x, "IsBatchNo", false),//批次
                    BatchNo = GetElementValue(x, "BatchNo", false),//批次
                    UnitID = GetElementValue(x, "UnitID", false)
                })

                         :
               (from x in dsXML.Descendants("Data")
                orderby x.Element(orderBy)==null?string.Empty:x.Element(orderBy).Value descending
                select new DataSourceModel()
                {

                    ID =GetElementValue(x,"ID",false),
                    ProductID = GetElementValue(x,"ProductID",false),
                    CodeName = GetElementValue(x,"CodeName",false),
                    ProdNo =GetElementValue(x,"ProdNo",false),
                    ProductCount = GetElementValue(x,"ProductCount",false),
                    ProductName = GetElementValue(x,"ProductName",false),
                    Specification = GetElementValue(x,"Specification",false),
                    StandardCost =GetElementValue(x,"StandardCost",false),
                    IsBatchNo =GetElementValue(x,"IsBatchNo",false),//批次
                    BatchNo = GetElementValue(x, "BatchNo", false),//批次
                    UnitID =GetElementValue(x,"UnitID",false)
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
        public string IsBatchNo { get; set; }//批次
        public string BatchNo { get; set; }//批次
        
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
<%@ WebHandler Language="C#" Class="StorageBorrowListForReturn" %>

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

public class StorageBorrowListForReturn : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        string orderString = (context.Request.Form["orderby"] == "" ? string.Empty : context.Request.Form["orderby"].ToString());//排序
        string order = "DESC";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ASC";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        
        Hashtable htPara = new Hashtable();
        string BorrowNo=FormatRequest(context,"BorrowNo",false);
        int BorrowDeptID = Convert.ToInt32(FormatRequest(context, "BorrowDeptID", true));
        int OutDeptID = Convert.ToInt32(FormatRequest(context, "OutDeptID", true));
        int OutStorageID = Convert.ToInt32(FormatRequest(context, "OutStorageID", true));
        if (!string.IsNullOrEmpty(BorrowNo))
            htPara.Add("BorrowNo", "%"+BorrowNo+"%");
        if (BorrowDeptID != -1)
            htPara.Add("BorrowDeptID", BorrowDeptID);
        if (OutDeptID != -1)
            htPara.Add("OutDeptID", OutDeptID);
        if (OutStorageID != -1)
            htPara.Add("OutStorageID", OutStorageID);
        htPara.Add("CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);

        orderBy = orderBy + " " + order;
        int TotalCount = 0;
        
       
        
        
        DataTable dt = XBase.Business.Office.StorageManager.StorageReturnBus.GetSorageBorrow(htPara,pageIndex,pageCount,orderBy,ref TotalCount);

        context.Response.ContentType = "text/plain";
        context.Response.Write(JsonClass.DataTableToJson(dt, TotalCount));
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
        public string BorrowNo { get; set; }
        public string DeptID { get; set; }
        public string DeptIDText { get; set; }
        public string Borrower { get; set; }
        public string BorrowerText { get; set; }
        public string BorrowDate { get; set; }
        public string OutDeptID { get; set; }
        public string OutDeptIDText { get; set; }
        public string StorageID { get; set; }
        public string StorageIDText { get; set; }
        public string TotalCount { get; set; }
        public string TotalPrice { get; set; }
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

    protected string FormatRequest(HttpContext context, string key, bool IsNum)
    {
        if (context.Request.Form[key] == null)
        {
            if (IsNum)
                return "-1";
            else
                return string.Empty;
        }
        else
        {
            if (string.IsNullOrEmpty(context.Request.Form[key].ToString()))
                if (IsNum)
                    return "-1";
                else
                    return string.Empty;
            else
                return context.Request.Form[key].ToString();
        }
    }
    
    
}
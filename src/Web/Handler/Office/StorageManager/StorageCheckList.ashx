<%@ WebHandler Language="C#" Class="StorageCheckList" %>

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

public class StorageCheckList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{


    public void ProcessRequest(HttpContext context)
    {
        string action = context.Request.Form["action"].ToString();
         if (action == "GET")
            Get(context);
        else if (action == "DEL")
            Del(context);
    }

    protected void Get(HttpContext context)
    {

        Hashtable htPara = new Hashtable();
        string CheckNo = FormatRequest(context, "CheckNo", false);
        if (!string.IsNullOrEmpty(CheckNo))
            htPara.Add("CheckNo", "%" + CheckNo + "%");
        
        
        string Title = FormatRequest(context, "Title", false);
        if (!string.IsNullOrEmpty(Title))
            htPara.Add("Title", "%" + Title + "%");

        string Transactor = FormatRequest(context, "Transactor", true);
        if (Transactor != "-1")
            htPara.Add("Transactor", Convert.ToInt32(Transactor));

        string DeptID = FormatRequest(context, "DeptID", true);
        if (DeptID != "-1")
            htPara.Add("DeptID", Convert.ToInt32(DeptID));

        string StorageID = FormatRequest(context, "StorageID", true);
        if (StorageID != "-1")
            htPara.Add("StorageID", Convert.ToInt32(StorageID));
        string CheckType = FormatRequest(context, "CheckType", true);
        if (CheckType != "-1" && CheckType.Trim()!="0")
            htPara.Add("CheckType", Convert.ToInt32(CheckType));

        string CheckStartDate = FormatRequest(context, "CheckStartDate", false);
        if (!string.IsNullOrEmpty(CheckStartDate))
            htPara.Add("CheckStartDate", Convert.ToDateTime(CheckStartDate));


        string DiffCountStart = FormatRequest(context, "DiffCountStart", true);
        if (DiffCountStart != "-1")
            htPara.Add("DiffCountStart", Convert.ToDecimal(DiffCountStart));

        string DiffCountEnd = FormatRequest(context, "DiffCountEnd", true);
        if (DiffCountEnd != "-1")
            htPara.Add("DiffCountEnd", Convert.ToDecimal(DiffCountEnd));


        string FlowStatus = FormatRequest(context, "FlowStatus", false);
        if(FlowStatus!="-1")
            htPara.Add("FlowStatus", FlowStatus);
        
        string BillStatus = FormatRequest(context, "BillStatus", false);
        if (BillStatus != "-1")
            htPara.Add("BillStatus", BillStatus);

        string EFIndex = FormatRequest(context, "EFIndex", false);
        string EFDesc = FormatRequest(context, "EFDesc", false);
        string BatchNo = FormatRequest(context, "BatchNo", false);//批次

        htPara.Add("CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
        string orderString = (context.Request.Form["orderby"] == "" ? string.Empty : context.Request.Form["orderby"].ToString());//排序
        string order = "DESC";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ASC";//排序：降序
        }
        int pageCount = Convert.ToInt32(FormatRequest(context, "pageCount", true));// int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = Convert.ToInt32(FormatRequest(context, "pageIndex", true));// int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

        int TotalCount = 0;

        orderBy = orderBy + " " + order;
        DataTable dt = StorageCheckBus.GetStorageCheckList(htPara,EFIndex,EFDesc,BatchNo,pageIndex, pageCount, orderBy, ref TotalCount);
        
       
        context.Response.ContentType = "text/plain";
        context.Response.Write(JsonClass.DataTableToJson(dt, TotalCount));
        context.Response.End();

    }

    protected void Del(HttpContext context)
    {
        string[] IDList = context.Request.Form["ID"].ToString().Split(',');
        context.Response.ContentType = "text/plain";
        context.Response.Write(XBase.Business.Office.StorageManager.StorageCheckBus.DelStorageCheck(IDList));
        context.Response.End();

    }







    public class DataSourceModel
    {
        public string ID { get; set; }
        public string CheckNo { get; set; }
        public string Title { get; set; }
        public string DeptID { get; set; }
        public string Transactor { get; set; }
        public string TransactorName { get; set; }
        public string DeptName { get; set; }
        public string StorageID { get; set; }
        public string StorageName { get; set; }
        public string CheckStartDate { get; set; }
        public string CheckType { get; set; }
        public string DiffCount { get; set; }
        public string BillStatus { get; set; }
        public string FlowStatus { get; set; }
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
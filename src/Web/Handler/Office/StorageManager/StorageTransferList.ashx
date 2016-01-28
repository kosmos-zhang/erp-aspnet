<%@ WebHandler Language="C#" Class="StorageTransferList" %>

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


public class StorageTransferList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
        UserInfoUtil userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        Hashtable htPara = new Hashtable();
        string TransferNo = FormatRequest(context, "TransferNo", false);
        if (!string.IsNullOrEmpty(TransferNo))
            htPara.Add("TransferNo", "%"+TransferNo+"%");
        string Title = FormatRequest(context, "TransferTitle", false);
        if (!string.IsNullOrEmpty(Title))
            htPara.Add("Title", "%" + Title + "%");
        string ApplyUserID = FormatRequest(context, "ApplyUserID", true);
        if (ApplyUserID != "-1")
            htPara.Add("ApplyUserID", Convert.ToInt32(ApplyUserID));
        string ApplyDeptID = FormatRequest(context, "ApplyDeptID", true);
        if (ApplyDeptID != "-1")
            htPara.Add("ApplyDeptID", Convert.ToInt32(ApplyDeptID));
        string InStorageID = FormatRequest(context, "InStorageID", true);
        if (InStorageID != "-1")
            htPara.Add("InStorageID", Convert.ToInt32(InStorageID));
        string RequireInDate = FormatRequest(context, "RequireInDate", false);
        if (!string.IsNullOrEmpty(RequireInDate))
            htPara.Add("RequireInDate", Convert.ToDateTime(RequireInDate));
        string OutDeptID = FormatRequest(context, "OutDeptID", true);
        if (OutDeptID != "-1")
            htPara.Add("OutDeptID", Convert.ToInt32(OutDeptID));
        string OutStorageID = FormatRequest(context, "OutStorageID", true);
        if (OutStorageID != "-1")
            htPara.Add("OutStorageID", Convert.ToInt32(OutStorageID));
        string ConfirmStatus = FormatRequest(context, "ConfirmStatus", false);
        if (ConfirmStatus!="-1")
            htPara.Add("ConfirmStatus", ConfirmStatus);
        string BusiStatus = FormatRequest(context, "BusiStatus", false);
        if (BusiStatus!="-1")
            htPara.Add("BusiStatus", BusiStatus);
        string BillStatus = FormatRequest(context, "BillStatus", false);
        if (BillStatus!="-1")
            htPara.Add("BillStatus", BillStatus);
        htPara.Add("CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
        string EFIndex = FormatRequest(context, "EFIndex", false);
        string EFDesc = FormatRequest(context, "EFDesc", false);
        string orderString = (context.Request.Form["orderby"] == "" ? string.Empty : context.Request.Form["orderby"].ToString());//排序
        string order = "DESC";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ASC";//排序：降序
        }
        orderBy = orderBy + " " + order;

        int pageCount = Convert.ToInt32(FormatRequest(context, "pageCount", true));// int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = Convert.ToInt32(FormatRequest(context, "pageIndex", true));// int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

        int TotalCount = 0;
       

        DataTable dt = XBase.Business.Office.StorageManager.StorageTransferBus.GetStorageTransferList( EFIndex, EFDesc,htPara,pageIndex,pageCount,orderBy,ref TotalCount);
       
        context.Response.ContentType = "text/plain";
        context.Response.Write(JsonClass.DataTableToJson(dt,TotalCount));
        context.Response.End();

    }

    protected void Del(HttpContext context)
    {
        string[] IDList = context.Request.Form["ID"].ToString().Split(',');
        context.Response.ContentType = "text/plain";
        context.Response.Write(XBase.Business.Office.StorageManager.StorageTransferBus.DelStorageTransfer(IDList));
        context.Response.End();

    }







    public class DataSourceModel
    {
        public string ID { get; set; }
        public string TransferNo { get; set; }
        public string TransferTitle { get; set; }
        public string ApplyUserID { get; set; }
        public string ApplyDeptID { get; set; }
        public string InStorageID { get; set; }
        public string RequireInDate { get; set; }
        public string OutDeptID { get; set; }
        public string OutStorageID { get; set; }
        public string OutDate { get; set; }
        public string TransferCount { get; set; }
        public string TransferPrice { get; set; }
        public string BillStatus { get; set; }
        public string FlowStatus { get; set; }
        public string BusiStatus { get; set; }
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
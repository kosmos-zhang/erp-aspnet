<%@ WebHandler Language="C#" Class="StorageReturnList" %>

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

public class StorageReturnList :IHttpHandler,System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string action = context.Request.Form["action"].ToString();
        if (action == "GET")
            GetList(context);
        else if (action == "DEL")
            DelReturnList(context);
    }


    protected void DelReturnList(HttpContext context)
    {
        string[] IDList = context.Request.Form["ID"].ToString().Split(',');
        context.Response.ContentType = "text/plain";
        context.Response.Write(XBase.Business.Office.StorageManager.StorageReturnBus.DelStorageReturn(IDList));
        context.Response.End();
    }


    protected void GetList(HttpContext context)
    {
      
        UserInfoUtil userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        Hashtable htParas = new Hashtable();
        string EFIndex =FormatRequest(context, "EFIndex", false);
        string EFDesc = FormatRequest(context, "EFDesc", false);
        if (!string.IsNullOrEmpty(FormatRequest(context, "BillStatus", false)))
            htParas.Add("@BillStatus", FormatRequest(context, "BillStatus", false));
        if (!string.IsNullOrEmpty(FormatRequest(context, "ReturnNo", false)))
            htParas.Add("@ReturnNo", "%"+FormatRequest(context, "ReturnNo", false)+"%");
        if (Convert.ToInt32(FormatRequest(context, "ReturnPerson", true)) > 0)
            htParas.Add("@ReturnPerson", Convert.ToInt32(FormatRequest(context, "ReturnPerson", true)));
        if (Convert.ToInt32(FormatRequest(context, "StorageID", true)) > 0)
            htParas.Add("@StorageID", Convert.ToInt32(FormatRequest(context, "StorageID", true)));
        if (!string.IsNullOrEmpty(FormatRequest(context, "ReturnTitle", false)))
            htParas.Add("@ReturnTitle", "%" + FormatRequest(context, "ReturnTitle", false) + "%");
        if (Convert.ToInt32(FormatRequest(context, "BorrowDeptID", true)) > 0)
            htParas.Add("@BorrowDeptID", Convert.ToInt32(FormatRequest(context, "BorrowDeptID", true)));
        if (Convert.ToInt32(FormatRequest(context, "OutDeptID", true)) > 0)
            htParas.Add("@OutDeptID", Convert.ToInt32(FormatRequest(context, "OutDeptID", true)));
        if (Convert.ToInt32(FormatRequest(context, "FromBillID", true)) > 0)
            htParas.Add("@FromBillID", Convert.ToInt32(FormatRequest(context, "FromBillID", true)));
        string tempStart = FormatRequest(context, "StartDate", false);
        DateTime StartDate = Convert.ToDateTime(string.IsNullOrEmpty(tempStart) ? DateTime.MinValue.ToString() : tempStart);
        string tempEnd = FormatRequest(context, "EndDate", false);
        DateTime EndDate = Convert.ToDateTime(string.IsNullOrEmpty(tempEnd) ? DateTime.MinValue.ToString() : tempEnd);
        if (StartDate > DateTime.MinValue)
            htParas.Add("@StartDate", StartDate);
        if (EndDate > DateTime.MinValue)
            htParas.Add("@EndDate", EndDate.AddDays(1));

        string orderString = (context.Request.Form["orderby"] == "" ? string.Empty : context.Request.Form["orderby"].ToString());//排序
        string order = "DESC";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"


        if (orderString.EndsWith("_a"))
        {
            order = "ASC";//排序：降序
        }

        orderBy = orderBy + " " + order;

        int pageCount = Convert.ToInt32(FormatRequest(context,"pageCount", true));// int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = Convert.ToInt32(FormatRequest(context,"pageIndex", true));// int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

        int TotalCount = 0;

        DataTable dt = XBase.Business.Office.StorageManager.StorageReturnBus.GetStorageReturnList(EFIndex,EFDesc,htParas, userinfo.CompanyCD, pageCount, pageIndex, orderBy, ref TotalCount);
        context.Response.ContentType = "text/plain";
        context.Response.Write(JsonClass.DataTableToJson(dt,TotalCount));
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
        public string ReturnNo { get; set; }
        public string ReturnTitle { get; set; }
        public string StorageName { get; set; }
        public string ReturnPerson { get; set; }
        public string ReturnDate { get; set; }
        public string BillStatus { get; set; }
        public string BorrowNo { get; set; }
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

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
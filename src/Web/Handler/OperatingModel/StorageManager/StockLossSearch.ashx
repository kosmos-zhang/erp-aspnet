<%@ WebHandler Language="C#" Class="StockLossSearch" %>

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


public class StockLossSearch : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string companyCD = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //设置行为参数
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "asc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "LossNo";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数


            //获取数据
            StorageLossModel model = new StorageLossModel();
            model.CompanyCD = companyCD;
            string LossDateStart = string.Empty;
            string LossDateEnd = string.Empty;
            string TotalPriceStart = string.Empty;
            string TotalPriceEnd = string.Empty;
            string FlowStatus = string.Empty;
            model.LossNo = context.Request.Form["LossNo"].Trim();
            model.Title = context.Request.Form["Title"];
            if (context.Request.Form["Dept"].Trim() != "undefined")
            {
                model.DeptID = context.Request.Form["Dept"].ToString();
            }
            model.StorageID = context.Request.Form["StorageID"].ToString();
            if (context.Request.Form["Executor"].Trim() != "undefined")
            {
                model.Executor = context.Request.Form["Executor"].ToString();
            }
            model.ReasonType = context.Request.Form["ReasonType"].ToString();
            model.BillStatus = context.Request.Form["BillStatus"].ToString();
            FlowStatus = context.Request.Form["FlowStatus"].ToString();
            LossDateStart = context.Request.Form["LossDateStart"].ToString();
            LossDateEnd = context.Request.Form["LossDateEnd"].ToString();
            TotalPriceStart = context.Request.Form["TotalPriceStart"].ToString();
            TotalPriceEnd = context.Request.Form["TotalPriceEnd"].ToString();
            //扩展属性条件
            string EFIndex = "";
            string EFDesc = "";
            try
            {
                EFIndex = context.Request.QueryString["EFIndex"].ToString();
                EFDesc = context.Request.QueryString["EFDesc"].ToString();
            }catch (Exception){}
            string ord = orderBy + " " + order;
            int TotalCount = 0;
            DataTable dt = StorageLossBus.GetStorageLossTableBycondition(model, LossDateStart, LossDateEnd, TotalPriceStart, TotalPriceEnd, FlowStatus, EFIndex, EFDesc, pageIndex, pageCount, ord, ref TotalCount);

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
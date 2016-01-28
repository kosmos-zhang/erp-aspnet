<%@ WebHandler Language="C#" Class="PurchaseArriveCollect" %>

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

public class PurchaseArriveCollect : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        if (context.Request.RequestType == "POST")
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码

            //设置行为参数
            string orderBy = (context.Request.Form["orderby"].ToString());//排序
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

            string ProviderID = context.Request.Form["ProviderID"];
            string ProductID = context.Request.Form["ProductID"];
            string StartConfirmDate = context.Request.Form["StartConfirmDate"];
            string EndConfirmDate = context.Request.Form["EndConfirmDate"];
            bool IsDate = false;
            bool.TryParse(context.Request.Form["IsDate"], out IsDate);

            int TotalCount = 0;

            context.Response.ContentType = "text/plain";
            DataTable dt = PurchaseArriveBus.PurchaseArriveCollect(pageIndex, pageCount, orderBy
                , ref TotalCount, ProviderID, ProductID, StartConfirmDate, EndConfirmDate, CompanyCD, IsDate, false);
            StringBuilder sb = new StringBuilder();

            if (dt.Rows.Count == 0)
            {
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(TotalCount.ToString());
                sb.Append(",data:");
                sb.Append("0");
                sb.Append("}");
            }
            else
                sb.Append(JsonClass.FormatDataTableToJson(dt, TotalCount));
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
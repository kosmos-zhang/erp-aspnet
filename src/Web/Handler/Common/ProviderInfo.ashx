<%@ WebHandler Language="C#" Class="ProviderInfo" %>

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

public class ProviderInfo : IHttpHandler,System.Web.SessionState.IRequiresSessionState 
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string orderBy = (context.Request.Form["orderbyProvider"].ToString());//排序
            int pageCount = int.Parse(context.Request.Form["pageCountProvider"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndexProvider"].ToString());//当前页

            //获取数据
            ProviderInfoModel model = new ProviderInfoModel();
            if (context.Request.Form["ProviderID"].ToString() != "")
            {
                model.ID = Convert.ToInt32(context.Request.Form["ProviderID"].ToString().Trim());
            }
            if (!string.IsNullOrEmpty(context.Request.Form["ProviderNo"].ToString().Trim()))
            {
                model.CustNo = context.Request.Form["ProviderNo"].ToString();
            }
            if (!string.IsNullOrEmpty(context.Request.Form["ProviderName"].ToString().Trim()))
            {
                model.CustName = context.Request.Form["ProviderName"].ToString();
            }
            try
            {
                model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                model.CompanyCD = "T0004";
            }
            
            model.UsedStatus = "1";
            int totalCount = 0;
            DataTable dt = ProviderInfoBus.GetProviderInfo(model, pageIndex, pageCount, orderBy, out totalCount);
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(totalCount.ToString());
            sb.Append(",data:");
            if (dt == null || dt.Rows.Count == 0)
            {//dt为空，拼装让字符串符合json格式
                sb.Append("\"\"");
            }
            else
            {
                sb.Append(JsonClass.DataTable2Json(dt));
            }
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
    //数据源结构
    public class DataSourceModel
    {
        public string ProviderID { get; set; }
        public string ProviderNo { get; set; }
        public string ProviderName { get; set; }

    }

}
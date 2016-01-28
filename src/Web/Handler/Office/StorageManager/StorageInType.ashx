<%@ WebHandler Language="C#" Class="StorageInType" %>

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

public class StorageInType : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string companyCD = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//[待修改]
        string action = (context.Request.Form["action"].ToString());//操作
        if (action == "getinfo")//弹出层信息
        {
            GetLsit(context);
        }
        else if (action == "list")//获取基本信息和根据传过来的InNo获取明细
        {
            GetDetailList(context);
        }
    }
    private void GetDetailList(HttpContext context)
    {
        string InType = context.Request.Form["InType"].ToString();
        string InNo = context.Request.Form["InNo"].ToString();
        string strJson = string.Empty;
        //string strMTJson = string.Empty;//采购单基本信息
        string strListJson = string.Empty;//入库单明细信息
        strJson = "{";
        if (InTypeBus.GetDetailInfo(companyCD, InType, InNo).Rows.Count > 0)
        {
            //strMTJson = JsonClass.DataTable2Json(ManufactureTaskInfoBus.GetMTInfo(strMTNo, companyCD));
            //strJson += "\"MT\":" + strMTJson;
            strListJson += "\"InList\":" + JsonClass.DataTable2Json(InTypeBus.GetDetailInfo(companyCD, InType, InNo));
            strJson += strListJson;
        }
        strJson += "}";
        context.Response.Write(strJson);
    }

    /// <summary>
    /// 获取数据列表
    /// </summary>
    private void GetLsit(HttpContext context)//弹出层信息
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderByOff"].ToString());//排序
        string order = "descending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ascending";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageOffCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        string Intype = context.Request.Form["InType"].ToString();
        string txtInNo = string.Empty;
        string txtTitle = string.Empty;
        txtInNo = context.Request.Form["txtInNo"].ToString();
        txtTitle = context.Request.Form["txtTitle"].ToString();
        DataTable dt = InTypeBus.GetInTypeInfo(companyCD, Intype,txtInNo,txtTitle);
        XElement dsXML = ConvertDataTableToXML(dt);
        //linq排序
        var dsLinq =
            (order == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderBy).Value ascending
             select new DataSourceModel()
             {

                 ID = x.Element("ID").Value,
                 InNo = x.Element("InNo").Value,
                 InType = x.Element("InType").Value,
                 Title = x.Element("Title").Value,
                 InNumber = x.Element("CountTotal").Value,

             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderBy).Value descending
             select new DataSourceModel()
             {
                 ID = x.Element("ID").Value,
                 InNo = x.Element("InNo").Value,
                 InType = x.Element("InType").Value,
                 Title = x.Element("Title").Value,
                 InNumber = x.Element("CountTotal").Value,
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

    //数据源结构
    public class DataSourceModel
    {
        public string ID { get; set; }
        public string InNo { get; set; }
        public string InType { get; set; }
        public string Title { get; set; }
        public string InNumber { get; set; }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
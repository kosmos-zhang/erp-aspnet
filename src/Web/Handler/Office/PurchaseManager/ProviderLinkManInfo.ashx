<%@ WebHandler Language="C#" Class="ProviderLinkManInfo" %>

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

public class ProviderLinkManInfo : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        if (context.Request.RequestType == "POST")
        {
            int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            //string action = context.Request.Params["action"];
            string action = context.Request.Form["action"].ToString();
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            if (action == "Delete")
            {
                string ID = context.Request.Params["str"].ToString();
                JsonClass jc;
                bool isDelete = ProviderLinkManBus.DeleteProviderLinkMan(ID,CompanyCD);
                //删除成功时
                if (isDelete)
                {
                    jc = new JsonClass("删除成功", "", 1);
                }
                else
                {
                    jc = new JsonClass("删除失败", "", 0);
                }
                context.Response.Write(jc);
                return;
                
            }
            else
            {

                //设置行为参数
                string orderString = (context.Request.Form["orderby"].ToString());//排序
                string order = "DESC";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_a"))
                {
                    order = "ASC";//排序：降序
                }
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

                string CustNo = context.Request.Form["CustNo"];
                string LinkManName = context.Request.Form["LinkManName"];
                string Handset = context.Request.Form["Handset"];
                string Important = context.Request.Form["Important"];
                string LinkType = context.Request.Form["LinkType"];
                string StartBirthday = context.Request.Form["StartBirthday"];
                string EndBirthday = context.Request.Form["EndBirthday"];
                int TotalCount = 0;


                orderBy = orderBy + " " + order;

                ////获取数据
                //string[] str = new string[7];
                //if (context.Request.Params["CustNo"] != null && context.Request.Params["CustNo"] != "")
                //{
                //    str[0] = context.Request.Params["CustNo"].ToString();
                //}
                //else
                //{
                //    str[0] = "";
                //}
                //if (context.Request.Params["LinkManName"] != null && context.Request.Params["LinkManName"] != "")
                //{
                //    str[1] = context.Request.Params["LinkManName"].ToString();
                //}
                //else
                //{
                //    str[1] = "";
                //}
                //if (context.Request.Params["Handset"] != null && context.Request.Params["Handset"] != "")
                //{
                //    str[2] = context.Request.Params["Handset"].ToString();
                //}
                //else
                //{
                //    str[2] = "";
                //}
                //if (context.Request.Params["Important"] != null && context.Request.Params["Important"] != "")
                //{
                //    str[3] = context.Request.Params["Important"].ToString();
                //}
                //else
                //{
                //    str[3] = "";
                //}
                //if (context.Request.Params["LinkType"] != null && context.Request.Params["LinkType"] != "")
                //{
                //    str[4] = context.Request.Params["LinkType"].ToString();
                //}
                //else
                //{
                //    str[4] = ""; 
                //}
                //if (context.Request.Params["StartBirthday"].Trim() != null && context.Request.Params["StartBirthday"].Trim() != "")
                //{
                //    str[5] = context.Request.Params["StartBirthday"].ToString();
                //}
                //else
                //{
                //    str[5] = "";
                //}
                //if (context.Request.Params["EndBirthday"].Trim() != null && context.Request.Params["EndBirthday"].Trim() != "")
                //{
                //    str[6] = context.Request.Params["EndBirthday"].ToString();
                //}
                //else
                //{
                //    str[6] = "";
                //}


                //XElement dsXML = ConvertDataTableToXML(ProviderLinkManBus.SelectProviderLinkMan(str));
                ////linq排序
                //var dsLinq =
                //    (order == "ascending") ?
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value ascending
                //     select new DataSourceModel()
                //     {
                //         ID = x.Element("ID").Value,
                //         CustNo = x.Element("CustNo").Value,
                //         CustName = x.Element("CustName").Value,
                //         LinkManName = x.Element("LinkManName").Value,
                //         Important = x.Element("Important").Value,
                //         Appellation = x.Element("Appellation").Value,
                //         WorkTel = x.Element("WorkTel").Value,
                //         Handset = x.Element("Handset").Value,
                //         MailAddress = x.Element("MailAddress").Value,
                //         MSN = x.Element("MSN").Value,
                //         QQ = x.Element("QQ").Value,
                //         LinkType = x.Element("LinkType").Value,
                //         LinkTypeName = x.Element("LinkTypeName").Value,
                //         Birthday = x.Element("Birthday").Value,
                //     })
                //              :
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value descending
                //     select new DataSourceModel()
                //     {
                //         ID = x.Element("ID").Value,
                //         CustNo = x.Element("CustNo").Value,
                //         CustName = x.Element("CustName").Value,
                //         LinkManName = x.Element("LinkManName").Value,
                //         Important = x.Element("Important").Value,
                //         Appellation = x.Element("Appellation").Value,
                //         WorkTel = x.Element("WorkTel").Value,
                //         Handset = x.Element("Handset").Value,
                //         MailAddress = x.Element("MailAddress").Value,
                //         MSN = x.Element("MSN").Value,
                //         QQ = x.Element("QQ").Value,
                //         LinkType = x.Element("LinkType").Value,
                //         LinkTypeName = x.Element("LinkTypeName").Value,
                //         Birthday = x.Element("Birthday").Value,
                //     });
                //int totalCount = dsLinq.Count();
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("{");
                //sb.Append("totalCount:");
                //sb.Append(totalCount.ToString());
                //sb.Append(",data:");
                //sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
                //sb.Append("}");

                //context.Response.ContentType = "text/plain";
                //context.Response.Write(sb.ToString());
                //context.Response.End();

                context.Response.ContentType = "text/plain";
                DataTable dt = ProviderLinkManBus.SelectProviderLinkMan(pageIndex, pageCount, orderBy, ref TotalCount, CustNo, LinkManName, Handset, Important, LinkType, StartBirthday, EndBirthday);

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
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    ////数据源结构
    //public class DataSourceModel
    //{
    //    public string ID { get; set; }
    //    public string CustNo { get; set; }
    //    public string CustName { get; set; }
    //    public string LinkManName { get; set; }
    //    public string Important { get; set; }
    //    public string Appellation { get; set; }
    //    public string WorkTel { get; set; }
    //    public string Handset { get; set; }
    //    public string MailAddress { get; set; }
    //    public string MSN { get; set; }
    //    public string QQ { get; set; }
    //    public string LinkType { get; set; }
    //    public string LinkTypeName { get; set; }
    //    public string Birthday { get; set; }
    //}

}
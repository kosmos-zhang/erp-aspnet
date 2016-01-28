<%@ WebHandler Language="C#" Class="ProviderFileInfo" %>

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

public class ProviderFileInfo : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        if (context.Request.RequestType == "POST")
        {
            int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string Action = context.Request.Params["Action"];
            if (Action == "Delete")
            {
                JsonClass jc;
                int Length = Convert.ToInt32(context.Request.Params["Length"]);
                bool Flag = true;
                for (int i = 0; i < Length; ++i)
                {

                    string CustNo = context.Request.Params["CustNo" + i];
                    try
                    {
                        Flag = Flag && (ProviderInfoBus.DeleteProviderInfoAll(CustNo, CompanyCD));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    if (Flag == false)
                    {
                        jc = new JsonClass("faile", "", 0);
                        context.Response.Write(jc);
                        return;
                    }
                }
                jc = new JsonClass("success", "", 1);
                context.Response.Write(jc);
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
                string CustName = context.Request.Form["CustName"];
                string CustNam = context.Request.Form["CustNam"];
                string PYShort = context.Request.Form["PYShort"];
                string CustType = context.Request.Form["CustType"];
                string CustClass = context.Request.Form["CustClass"];
                string AreaID = context.Request.Form["AreaID"];
                string CreditGrade = context.Request.Form["CreditGrade"];
                string Manager = context.Request.Form["Manager"];
                string StartCreateDate = context.Request.Form["StartCreateDate"];
                string EndCreateDate = context.Request.Form["EndCreateDate"];
                int TotalCount = 0;


                orderBy = orderBy + " " + order;
                

                ////获取数据
                //string[] str = new string[11];
                //if (context.Request.Params["CustNo"] != null && context.Request.Params["CustNo"] != "")
                //{
                //    str[0] = context.Request.Params["CustNo"].ToString();
                //}
                //else
                //{
                //    str[0] = "";
                //}
                //if (context.Request.Params["CustName"] != null && context.Request.Params["CustName"] != "")
                //{
                //    str[1] = context.Request.Params["CustName"].ToString();
                //}
                //else
                //{
                //    str[1] = "";
                //}
                //if (context.Request.Params["CustNam"] != null && context.Request.Params["CustNam"] != "")
                //{
                //    str[2] = context.Request.Params["CustNam"].ToString();
                //}
                //else
                //{
                //    str[2] = "";
                //}
                //if (context.Request.Params["PYShort"] != null && context.Request.Params["PYShort"] != "")
                //{
                //    str[3] = context.Request.Params["PYShort"].ToString();
                //}
                //else
                //{
                //    str[3] = "";
                //}
                //if (context.Request.Params["CustType"] != null && context.Request.Params["CustType"] != "")
                //{
                //    str[4] = context.Request.Params["CustType"].ToString();
                //}
                //else
                //{
                //    str[4] = "";
                //}
                //if (context.Request.Params["CustClass"] != null && context.Request.Params["CustClass"] != "")
                //{
                //    str[5] = context.Request.Params["CustClass"].ToString();
                //}
                //else
                //{
                //    str[5] = "";
                //}
                //if (context.Request.Params["AreaID"] != null && context.Request.Params["AreaID"] != "")
                //{
                //    str[6] = context.Request.Params["AreaID"].ToString();
                //}
                //else
                //{
                //    str[6] = "";
                //}
                //if (context.Request.Params["CreditGrade"] != null && context.Request.Params["CreditGrade"] != "")
                //{
                //    str[7] = context.Request.Params["CreditGrade"].ToString();
                //}
                //else
                //{
                //    str[7] = "";
                //}
                //if (context.Request.Params["Manager"] != null && context.Request.Params["Manager"] != "")
                //{
                //    str[8] = context.Request.Params["Manager"].ToString();
                //}
                //else
                //{
                //    str[8] = "";
                //}
                //if (context.Request.Params["StartCreateDate"].Trim() != null && context.Request.Params["StartCreateDate"].Trim() != "")
                //{
                //    str[9] = context.Request.Params["StartCreateDate"].ToString();
                //}
                //else
                //{
                //    str[9] = "";
                //}
                //if (context.Request.Params["EndCreateDate"].Trim() != null && context.Request.Params["EndCreateDate"].Trim() != "")
                //{
                //    str[10] = context.Request.Params["EndCreateDate"].ToString();
                //}
                //else
                //{
                //    str[10] = "";
                //}


                //XElement dsXML = ConvertDataTableToXML(ProviderInfoBus.SelectProviderInfo(str));
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
                //         CustNam = x.Element("CustNam").Value,
                //         CustType = x.Element("CustType").Value,
                //         CustTypeName = x.Element("CustTypeName").Value,
                //         PYShort = x.Element("PYShort").Value,
                //         CreditGrade = x.Element("CreditGrade").Value,
                //         CreditGradeName = x.Element("CreditGradeName").Value,
                //         Manager = x.Element("Manager").Value,
                //         ManagerName = x.Element("ManagerName").Value,
                //         AreaID = x.Element("AreaID").Value,
                //         AreaName = x.Element("AreaName").Value,
                //         Creator = x.Element("Creator").Value,
                //         CreatorName = x.Element("CreatorName").Value,
                //         CreateDate = x.Element("CreateDate").Value,
                //         CustClass = x.Element("CustClass").Value,
                //         CustClassName = x.Element("CustClassName").Value,
                //     })
                //              :
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value descending
                //     select new DataSourceModel()
                //     {
                //         ID = x.Element("ID").Value,
                //         CustNo = x.Element("CustNo").Value,
                //         CustName = x.Element("CustName").Value,
                //         CustNam = x.Element("CustNam").Value,
                //         CustType = x.Element("CustType").Value,
                //         CustTypeName = x.Element("CustTypeName").Value,
                //         PYShort = x.Element("PYShort").Value,
                //         CreditGrade = x.Element("CreditGrade").Value,
                //         CreditGradeName = x.Element("CreditGradeName").Value,
                //         Manager = x.Element("Manager").Value,
                //         ManagerName = x.Element("ManagerName").Value,
                //         AreaID = x.Element("AreaID").Value,
                //         AreaName = x.Element("AreaName").Value,
                //         Creator = x.Element("Creator").Value,
                //         CreatorName = x.Element("CreatorName").Value,
                //         CreateDate = x.Element("CreateDate").Value,
                //         CustClass = x.Element("CustClass").Value,
                //         CustClassName = x.Element("CustClassName").Value,
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
                DataTable dt = ProviderInfoBus.SelectProviderInfo(pageIndex, pageCount, orderBy, ref TotalCount, CustNo, CustName, CustNam, PYShort, CustType, CustClass, AreaID, CreditGrade, Manager, StartCreateDate, EndCreateDate);

                //string temp = JsonClass.FormatDataTableToJson(dt, TotalCount);
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
    //    public string CustNam { get; set; }
    //    public string CustType { get; set; }
    //    public string CustTypeName { get; set; }
    //    public string PYShort { get; set; }
    //    public string CreditGrade { get; set; }
    //    public string CreditGradeName { get; set; }
    //    public string Manager { get; set; }
    //    public string ManagerName { get; set; }
    //    public string AreaID { get; set; }
    //    public string AreaName { get; set; }
    //    public string Creator { get; set; }
    //    public string CreatorName { get; set; }
    //    public string CreateDate { get; set; }
    //    public string CustClass { get; set; }
    //    public string CustClassName { get; set; }
    //}

}
<%@ WebHandler Language="C#" Class="CustAdviceList" %>

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
using XBase.Business.Office.CustManager;
using XBase.Model.Office.CustManager;

public class CustAdviceList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //string companyCD = "AAAAAA";//[待修改]

        if (context.Request.RequestType == "POST")
        {
            CustAdviceModel model = new CustAdviceModel();
            int pageCount = 10;
               int TotalCount = 0;
            //设置行为参数
            string orderString = context.Request.Form["orderby"].ToString();//排序
            string order = "desc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "AdviceDate";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_a"))
                order = "asc";//排序：降序
            try
            {
                pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            }
            catch
            {
                pageCount = int.Parse(context.Request.Form["MaxCount"].ToString());
            }
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            string myOrder = orderBy + " " + order;
         

            if (!string.IsNullOrEmpty(context.Request.Form["txtAdvicetNo"].ToString().Trim()))
            {
                model.AdviceNo = context.Request.Form["txtAdvicetNo"].ToString().Trim();
            }
            if (!string.IsNullOrEmpty(context.Request.Form["txtTitle"].ToString().Trim()))
            {
                model.Title = context.Request.Form["txtTitle"].ToString().Trim();
            }
            if (!string.IsNullOrEmpty(context.Request.Form["CustID"]) && context.Request.Form["CustID"].ToString() != "0")
            {
                model.CustID = context.Request.Form["CustID"];

            }
            if (!string.IsNullOrEmpty(context.Request.Form["DestClerk"]) && context.Request.Form["DestClerk"] != "0")
            {
                model.DestClerk = context.Request.Form["DestClerk"];
            }

            model.State = context.Request.Form["txtState"];

            model.AdviceType = context.Request.Form["txtAdviceType"];
            string BeginTime = context.Request.Form["BeginTime"].ToString();
            string EndTime = context.Request.Form["EndTime"].ToString();
            string Manager = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();

            DataTable dt = CustAdviceBus.GetAllCustAdvice(Manager,model, pageIndex, pageCount, myOrder, BeginTime, EndTime, companyCD, ref TotalCount);
          
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
    //数据源结构
    public class DataSourceModel
    {
        public string ID { get; set; }
        public string AdjustNo { get; set; }
        public string Title { get; set; }
        public string StorageName { get; set; }
        public string DeptName { get; set; }
        public string AdjustDate { get; set; }
        public string EmployeeName { get; set; }
        public string CodeName { get; set; }
        public string BillStatusName { get; set; }
        public string BillStatus { get; set; }
        public string FlowStatus { get; set; }
        public string FlowStatusID { get; set; }
    }

}
<%@ WebHandler Language="C#" Class="StorageNoPassList" %>

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

public class StorageNoPassList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;       
        if (context.Request.RequestType == "POST")
        {
            try
            {
                CheckNotPassModel model = new CheckNotPassModel();

                //设置行为参数
                int pageCount = 10;
                string orderString = context.Request.Form["orderby"].ToString();//排序
                string order = "desc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                    order = "asc";//排序：降序
                pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                string myOrder = orderBy + " " + order;

                int TotalCount = 0;
                model.Creator = pageIndex;
                model.Confirmor = pageCount;
                model.Attachment = myOrder;


                model.CompanyCD = companyCD;
                if (!string.IsNullOrEmpty(context.Request.Form["txtNoPassNo"].ToString().Trim()))
                {
                    model.ProcessNo = context.Request.Form["txtNoPassNo"].ToString().Trim();
                }
                if (!string.IsNullOrEmpty(context.Request.Form["txtTitle"].ToString().Trim()))
                {
                    model.Title = context.Request.Form["txtTitle"].ToString().Trim();
                }
                //if (context.Request.QueryString["ReportID"].ToString() != "0")
                //{
                model.ReportID = int.Parse(context.Request.Form["ReportID"].ToString().Trim());
                //}
                //if (context.Request.QueryString["ProductID"].ToString()!="0")
                //{
                string ProductID = context.Request.Form["ProductID"].ToString();
                //}
                //if (context.Request.QueryString["BillStatus"].ToString())
                //{
                model.BillStatus = context.Request.Form["BillStatus"].ToString();
                //}
                string FlowStatus = context.Request.Form["FlowStatus"].ToString();
                string BeginTime = context.Request.Form["BeginTime"].ToString();
                string EndTime = context.Request.Form["EndTime"].ToString();

                //扩展属性条件
                string EFIndex = context.Request.Form["EFIndex"].ToString();
                string EFDesc = context.Request.Form["EFDesc"].ToString();

                DataTable dt = CheckNotPassBus.SearchNoPass(model, ProductID, BeginTime, EndTime, FlowStatus, EFIndex, EFDesc, ref TotalCount);
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
            catch
            { }
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
        public string ProcessNo { get; set; }
        public string Title { get; set; }
        public string FromTypeName { get; set; }
        public string ReportName { get; set; }
        public string ProductName { get; set; }
        public string ProNo { get; set; }
        public string EmployeeName { get; set; }
        public string ProcessDate { get; set; }
        public string BillStatus { get; set; }
        public string BillStatusID { get; set; }
        public string FlowStatus { get; set; }
        public string ReportNo { get; set; }
    }

}
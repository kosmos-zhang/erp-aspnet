<%@ WebHandler Language="C#" Class="StorageReportList" %>

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

public class StorageReportList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //string companyCD = "AAAAAA";//[待修改]

        if (context.Request.RequestType == "POST")
        {
            try
            {
                StorageQualityCheckReportModel model = new StorageQualityCheckReportModel();
                int pageCount = 10;
                //设置行为参数
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
                string BeginTime = "", EndTime = "";
                if (!string.IsNullOrEmpty(context.Request.Form["txtReportNo"].ToString().Trim()))
                {
                    model.ReportNo = context.Request.Form["txtReportNo"].ToString().Trim();
                }
                if (!string.IsNullOrEmpty(context.Request.Form["txtTitle"].ToString().Trim()))
                {
                    model.Title = context.Request.Form["txtTitle"].ToString().Trim();
                }
                model.FromReportNo = context.Request.Form["ReportID"].ToString().Trim();
                if (context.Request.Form["Checker"].ToString() != "0" && context.Request.Form["Checker"].ToString() != "" && context.Request.Form["Checker"].ToString() != null)
                {
                    model.ApplyUserID = int.Parse(context.Request.Form["Checker"].ToString().Trim());
                }
                if (context.Request.Form["CheckDept"].ToString() != "0" && context.Request.Form["CheckDept"].ToString() != "" && context.Request.Form["CheckDept"].ToString() != null)
                {
                    model.ApplyDeptID = int.Parse(context.Request.Form["CheckDept"].ToString().Trim());
                }

                if (!string.IsNullOrEmpty(context.Request.Form["BeginTime"].ToString().Trim()))
                {
                    BeginTime = context.Request.Form["BeginTime"].ToString().Trim();
                }
                if (!string.IsNullOrEmpty(context.Request.Form["EndTime"].ToString().Trim()))
                {
                    EndTime = context.Request.Form["EndTime"].ToString().Trim();
                }
                model.CheckMode = context.Request.Form["sltCheckMode"].ToString();
                model.FromType = context.Request.Form["sltFromType"].ToString();
                model.CheckType = context.Request.Form["sltCheckType"].ToString();
                model.BillStatus = context.Request.Form["BillStatus"].ToString();
                //扩展属性条件
                string EFIndex = context.Request.Form["EFIndex"].ToString();
                string EFDesc = context.Request.Form["EFDesc"].ToString();

                string FlowStatus = context.Request.Form["FlowStatus"].ToString();
                DataTable dt = CheckReportBus.SearchReport(model, BeginTime, EndTime, FlowStatus, EFIndex, EFDesc, ref TotalCount);
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
        public string ReportNo { get; set; }
        public string Title { get; set; }
        public string OtherCorpName { get; set; }
        public string DeptName { get; set; }
        public string BigTypeName { get; set; }
        public string FromTypeName { get; set; }
        public string CheckModeName { get; set; }
        public string CheckTypeName { get; set; }
        public string EmployeeName { get; set; }
        public string FlowStatus { get; set; }
        public string BillStatus { get; set; }
        public string CheckDate { get; set; }
        public string BillStatusID { get; set; }
        public string FlowStatusID { get; set; }
    }

}
<%@ WebHandler Language="C#" Class="StorageQualityCheckList" %>

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
using System.Web.SessionState;

public class StorageQualityCheckList : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            try
            {
                StorageQualityCheckApplay model = new StorageQualityCheckApplay();
                int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                int pageCount = 10;
                string ActionPlan = context.Request.Form["ActionPlan"];

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
                model.Creater = pageIndex;
                model.Confirmor = pageCount;
                model.Attachment = myOrder;
                model.CompanyCD = CompanyCD;
                DateTime EndCheckDate = Convert.ToDateTime("9999-2-3");
                if (context.Request.Form["txtInNo"] != "")
                {
                    model.ApplyNO = context.Request.Form["txtInNo"].ToString().Trim();
                }
                if (context.Request.Form["txtTitle"].ToString() != "")
                {
                    model.Title = context.Request.Form["txtTitle"].ToString().Trim();
                }
                if (context.Request.Form["txtChecker"].ToString() != "0")
                {
                    model.Checker = context.Request.Form["txtChecker"].ToString().Trim();
                }
                if (!string.IsNullOrEmpty(context.Request.Form["hiddenCustID"].ToString()) && context.Request.Form["hiddenCustID"].ToString() != "0")
                {
                    model.CustName = context.Request.Form["hiddenCustID"].ToString().Trim();
                }
                if (context.Request.Form["txtCheckDept"].ToString() != "0")
                {
                    model.CheckDeptID = context.Request.Form["txtCheckDept"].ToString().Trim();
                }
                model.CheckDate = Convert.ToDateTime("1800-1-1");
                if (context.Request.Form["BeginCheckDate"].ToString() != "")
                {
                    model.CheckDate = Convert.ToDateTime(context.Request.Form["BeginCheckDate"].ToString().Trim());
                }
                if (context.Request.Form["EndCheckDate"].ToString() != "" && context.Request.Form["EndCheckDate"] != null)
                {
                    EndCheckDate = Convert.ToDateTime(context.Request.Form["EndCheckDate"].ToString());
                }
                if (context.Request.Form["BillType"].ToString() != "00")
                {
                    model.BillStatus = context.Request.Form["BillType"].ToString().Trim();
                }

                if (context.Request.Form["txtCheckType"].ToString() != "00")
                {
                    model.CheckType = context.Request.Form["txtCheckType"].ToString().Trim();
                }
                if (context.Request.Form["txtCheckMode"].ToString() != "00")
                {
                    model.CheckMode = context.Request.Form["txtCheckMode"].ToString().Trim();
                }
                if (context.Request.Form["FromType"].ToString() != "00")
                {
                    model.FromType = context.Request.Form["FromType"].ToString().Trim();
                }
                string FlowStatus = context.Request.Form["FlowStatus"].ToString().Trim();

                //扩展属性条件
                string EFIndex = context.Request.Form["EFIndex"].ToString();
                string EFDesc = context.Request.Form["EFDesc"].ToString();
                
                DataTable dt = StorageQualityCheckPro.GetQualityList(model, EndCheckDate, FlowStatus, EFIndex, EFDesc, ref TotalCount);
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
        public string ApplyNo { get; set; }
        public string Title { get; set; }
        public string CustName { get; set; }
        public string CheckDate { get; set; }
        public string DeptName { get; set; }
        public string EmployeeName { get; set; }
        public string FromType { get; set; }
        public string CheckType { get; set; }
        public string CheckMode { get; set; }
        public string BillStatus { get; set; }
        public string FlowStatus { get; set; }
        public string CustBigType { get; set; }
        public string FromTypeID { get; set; }
        public string BillStatusID { get; set; }
        public string FlowStatusID { get; set; }

    }
    

}
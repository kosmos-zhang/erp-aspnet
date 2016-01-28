<%@ WebHandler Language="C#" Class="SubStorageInitList" %>

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
using XBase.Business.Office.SubStoreManager;
using XBase.Model.Office.SubStoreManager;
using System.Web.SessionState;
using System.Text;

public class SubStorageInitList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        if (context.Request.RequestType == "POST")
        {
            int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            string Action = context.Request.Params["action"];
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码

            if (Action == "Delete")
            {
                JsonClass jc;
                string IDs = context.Request.Params["IDs"];
                string InNos = context.Request.Params["InNos"];

                if (true == SubStorageBus.DeleteSubStorageIn(IDs, InNos, CompanyCD))
                {
                    jc = new JsonClass("success", "", 1);
                    context.Response.Write(jc);
                    context.Response.End();
                }
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

                string DeptID = context.Request.Form["DeptID"];
                string ProductID = context.Request.Form["ProductID"];
                string ProductName = context.Request.Form["ProductName"];
                string BatchNo = context.Request.Form["BatchNo"];

                //扩展属性条件
                string EFIndex = context.Request.Form["EFIndex"].ToString();
                string EFDesc = context.Request.Form["EFDesc"].ToString();

                int TotalCount = 0;
                orderBy = orderBy + " " + order;
                context.Response.ContentType = "text/plain";
                DataTable dt = SubStorageBus.SelectSubStorageInitList(pageIndex, pageCount, orderBy, ref TotalCount, DeptID, ProductID, ProductName, EFIndex, EFDesc, BatchNo);

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
        public string DeptID { get; set; }
        public string DeptName { get; set; }
        public string InNo { get; set; }
        public string Title { get; set; }
        public string CountTotal { get; set; }
        public string Confirmor { get; set; }
        public string ConfirmorName { get; set; }
        public string ConfirmDate { get; set; }
        public string BillStatus { get; set; }
        public string BillStatusName { get; set; }
    }

}
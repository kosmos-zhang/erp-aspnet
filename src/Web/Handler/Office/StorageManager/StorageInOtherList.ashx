<%@ WebHandler Language="C#" Class="StorageInOtherList" %>

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
public class StorageInOtherList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string companyCD = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //设置行为参数
            string orderString = (context.Request.QueryString["orderby"].ToString());//排序
            string order = "desc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_a"))
            {
                order = "asc";//排序：降序
            }
            int pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            //扩展属性条件
            string EFIndex = context.Request.QueryString["EFIndex"].ToString();
            string EFDesc = context.Request.QueryString["EFDesc"].ToString();

            //获取数据
            StorageInOtherModel model = new StorageInOtherModel();
            model.CompanyCD = companyCD;
            string EnterDateStart = string.Empty;
            string EnterDateEnd = string.Empty;
            model.InNo = context.Request.QueryString["InNo"].Trim();
            model.Title = context.Request.QueryString["Title"];
            if (context.Request.QueryString["InPutDept"].Trim() != "undefined")
            {
                model.DeptID = context.Request.QueryString["InPutDept"].ToString();
            }
            model.BillStatus = context.Request.QueryString["BillStatus"].ToString();
            if (context.Request.QueryString["Executor"].Trim() != "undefined")
            {
                model.Executor = context.Request.QueryString["Executor"].ToString();
            }
            if (context.Request.QueryString["Taker"].Trim() != "undefined")
            {
                model.Taker = context.Request.QueryString["Taker"].ToString();
            }
            if (context.Request.QueryString["Checker"].Trim() != "undefined")
            {
                model.Checker = context.Request.QueryString["Checker"].ToString();
            }
            model.EFDesc = EFDesc;
            model.EFIndex = EFIndex;
            EnterDateStart = context.Request.QueryString["EnterDateStart"].ToString();
            EnterDateEnd = context.Request.QueryString["EnterDateEnd"].ToString();
            string StorageID = context.Request.QueryString["StorageID"];
            string BatchNo = context.Request.QueryString["BatchNo"];
            //string ProjectID = context.Request.QueryString["ProjectID"];
            model.ProjectID = context.Request.QueryString["ProjectID"];

            string ord = orderBy + " " + order;
            int TotalCount = 0;
            DataTable dt = StorageInOtherBus.GetStorageInOtherTableBycondition(BatchNo,model, StorageID, EnterDateStart, EnterDateEnd, pageIndex, pageCount, ord, ref TotalCount);

            
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
        //入库单编号、入库单主题、交货人、验收人、仓库、入库部门、人库人、入库时间、入库原因、入库数量、入库金额、摘要、单据状态

        public string ID { get; set; }
        public string InNo { get; set; }
        public string Title { get; set; }
        public string TakerName { get; set; }
        public string CheckerName { get; set; }
        public string ReasonTypeName { get; set; }

        public string ExecutorName { get; set; }

        public string InPutDept { get; set; }

        public string EnterDate { get; set; }
        public string CountTotal { get; set; }
        public string TotalPrice { get; set; }
        public string Summary { get; set; }
        public string BillStatusName { get; set; }
    }

}
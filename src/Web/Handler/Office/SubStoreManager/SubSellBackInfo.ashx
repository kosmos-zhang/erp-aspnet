<%@ WebHandler Language="C#" Class="SubSellBackInfo" %>

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

public class SubSellBackInfo : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        if (context.Request.RequestType == "POST")
        {
            int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            string Action = context.Request.Params["Action"];
            if (Action == "Delete")
            {
                JsonClass jc;
                string IDs = context.Request.Params["IDs"];
                string BackNos = context.Request.Params["BackNos"];
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码

                if (true == SubSellBackBus.DeleteSubSellBack(IDs, BackNos, CompanyCD))
                {
                    jc = new JsonClass("success", "", 1);
                    context.Response.Write(jc);
                    context.Response.End();
                }
            }
            else
            {

                //设置行为参数
                string orderBy = (context.Request.Form["orderby"].ToString());//排序
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

                string BackNo = context.Request.Form["BackNo"];
                string Title = context.Request.Form["Title"];
                string OrderID = context.Request.Form["OrderID"];
                string CustName = context.Request.Form["CustName"];
                string CustTel = context.Request.Form["CustTel"];
                string DeptID = context.Request.Form["DeptID"];
                string Seller = context.Request.Form["Seller"];
                string BusiStatus = context.Request.Form["BusiStatus"];
                string BillStatus = context.Request.Form["BillStatus"];
                string CustAddr = context.Request.Form["CustAddr"];

                //扩展属性条件
                string EFIndex = context.Request.Form["EFIndex"];
                string EFDesc = context.Request.Form["EFDesc"];

                int TotalCount = 0;
                context.Response.ContentType = "text/plain";
                DataTable dt = SubSellBackBus.SelectSubSellBack(pageIndex, pageCount, orderBy, ref TotalCount, BackNo, Title, OrderID, CustName, CustTel, DeptID, Seller, BusiStatus, BillStatus, CustAddr, EFIndex, EFDesc);

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
        public string BackNo { get; set; }
        public string Title { get; set; }
        public string OrderID { get; set; }
        public string OrderNo { get; set; }
        public string CustName { get; set; }
        public string CustTel { get; set; }
        public string CustAddr { get; set; }
        public string DeptID { get; set; }
        public string DeptName { get; set; }
        public string Seller { get; set; }
        public string SellerName { get; set; }
        public string BusiStatus { get; set; }
        public string BillStatus { get; set; }
        public string BusiStatusName { get; set; }
        public string BillStatusName { get; set; }
        public string SendMode { get; set; }
        public string FromType { get; set; }
        public string CurrencyType { get; set; }
    }

}
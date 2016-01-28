<%@ WebHandler Language="C#" Class="SubSellOrderList" %>

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


public class SubSellOrderList : IHttpHandler, IRequiresSessionState 
{  
    public void ProcessRequest (HttpContext context) 
    {
        if (context.Request.RequestType == "POST")
        {
            string Action = context.Request.QueryString["Action"];
            if (Action == "Select")
            {
                string orderString = (context.Request.QueryString["orderBy"]);//排序
                string order = "asc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                {
                    order = "desc";//排序：降序
                }
                int pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
                int totalCount = 0;
                string ord = orderBy + " " + order;
                SubSellOrderModel SubSellOrderM = GetSubSellOrderM(context.Request);

                //扩展属性条件
                string EFIndex = context.Request.QueryString["EFIndex"].ToString();
                string EFDesc = context.Request.QueryString["EFDesc"].ToString();

                DataTable dt = SubSellOrderBus.SelectSubSellOrder(SubSellOrderM, pageIndex, pageCount, ord, EFIndex, EFDesc, ref totalCount);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt.Rows.Count == 0)
                    sb.Append("[{\"ID\":\"\"}]");
                else
                    sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");
                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
                return;
            }
            else if (Action == "Delete")
            {
                JsonClass jc;
                string IDs = context.Request.QueryString["IDs"];
                string OrderNos = context.Request.QueryString["OrderNos"];

                if (true == SubSellOrderBus.DeleteSubSellOrder(IDs,OrderNos))
                {
                    jc = new JsonClass("success", "", 1);
                    context.Response.Write(jc);
                    context.Response.End();
                }
            }
        }
    }
 
    public bool IsReusable 
    {
        get 
        {
            return false;
        }
    }
    ///// <summary>
    ///// datatabletoxml
    ///// </summary>
    ///// <param name="xmlDS"></param>
    ///// <returns></returns>
    //private XElement ConvertDataTableToXML(DataTable xmlDS)
    //{
    //    StringWriter sr = new StringWriter();
    //    xmlDS.TableName = "Data";
    //    xmlDS.WriteXml(sr, System.Data.XmlWriteMode.IgnoreSchema, true);
    //    string contents = sr.ToString();
    //    return XElement.Parse(contents);
    //}

    //public static string ToJSON(object obj)
    //{
    //    JavaScriptSerializer serializer = new JavaScriptSerializer();
    //    return serializer.Serialize(obj);
    //}
    //数据源结构
    //public class DataSourceModel
    //{
    //    public string ID { get; set; }
    //    public string OrderNo { get; set; }
    //    public string Title { get; set; }
    //    public string SendMode { get; set; }
    //    public string SendModeName { get; set; }
    //    public string CustName { get; set; }
    //    public string CustTel { get; set; }
    //    public string CustMobile { get; set; }
    //    public string CustAddr { get; set; }
    //    public string DeptID { get; set; }
    //    public string DeptName { get; set; }
    //    public string Seller { get; set; }
    //    public string SellerName { get; set; }
    //    public string BusiStatus { get; set; }
    //    public string BusiStatusName { get; set; }
    //    public string BillStatus { get; set; }
    //    public string BillStatusName { get; set; }
    //}
    private SubSellOrderModel GetSubSellOrderM(HttpRequest request)
    {
        SubSellOrderModel SubSellOrderM = new SubSellOrderModel();
        SubSellOrderM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        SubSellOrderM.OrderNo = request.QueryString["OrderNo"];
        SubSellOrderM.Title = request.QueryString["Title"];
        SubSellOrderM.SendMode = request.QueryString["SendMode"];
        SubSellOrderM.CustName = request.QueryString["CustName"];
        SubSellOrderM.CustTel = request.QueryString["CustTel"];
        SubSellOrderM.CustMobile = request.QueryString["CustMobile"];
        SubSellOrderM.CustAddr = request.QueryString["CustAddr"];
        SubSellOrderM.DeptID = request.QueryString["DeptID"];
        SubSellOrderM.Seller = request.QueryString["Seller"];
        SubSellOrderM.BusiStatus = request.QueryString["BusiStatus"];
        SubSellOrderM.BillStatus = request.QueryString["BillStatus"];
        return SubSellOrderM;
    }
}
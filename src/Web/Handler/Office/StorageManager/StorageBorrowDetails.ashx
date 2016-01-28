<%@ WebHandler Language="C#" Class="StorageBorrowDetails" %>

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

public class StorageBorrowDetails : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        int id = Convert.ToInt32(context.Request.Form["id"] == null ? "0" : context.Request.Form["id"].ToString());
        DataTable dt = XBase.Business.Office.StorageManager.StorageBorrowBus.GetStorageBorrowByID(id);
        XElement dsXML = ConvertDataTableToXML(dt);
        //设置行为参数
        string orderString = (context.Request.Form["orderby"] == null ? string.Empty : context.Request.Form["orderby"]);//排序
        string order = "ascending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ascending";//排序：降序
        }
        //int pageCount = int.Parse(context.Request.Form["pageProductInfocount"].ToString());//每页显示记录数
        //  int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        //int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        var dsLinq =
               (order == "ascending") ?
               (from x in dsXML.Descendants("Data")
                orderby x.Element(orderBy).Value ascending
                select new DataSourceModel()
                {

                    BillStatus = x.Element("BillStatus") == null ? string.Empty : x.Element("BillStatus").Value,
                    BorrowDate = string.IsNullOrEmpty(x.Element("BorrowDate").Value) ? string.Empty : (Convert.ToDateTime(x.Element("BorrowDate").Value)).ToString("yyyy-MM-dd"),
                    Borrower = x.Element("Borrower") == null ? string.Empty : x.Element("Borrower").Value,
                    BorrowNo = x.Element("BorrowNo") == null ? string.Empty : x.Element("BorrowNo").Value,
                    CloseDate = x.Element("CloseDate") == null ? string.Empty : (Convert.ToDateTime(x.Element("CloseDate").Value)).ToString("yyyy-MM-dd"),
                    Closer = x.Element("Closer") == null ? string.Empty : x.Element("Closer").Value,


                    CompanyCD = x.Element("CompanyCD") == null ? string.Empty : x.Element("CompanyCD").Value,
                    ConfirmDate = x.Element("ConfirmDate") == null ? string.Empty : (Convert.ToDateTime(x.Element("ConfirmDate").Value)).ToString("yyyy-MM-dd"),
                    Confirmor = x.Element("Confirmor") == null ? string.Empty : x.Element("Confirmor").Value,
                    CountTotal = x.Element("CountTotal") == null ? string.Empty : x.Element("CountTotal").Value,

                    CreateDate = x.Element("CreateDate") == null ? string.Empty : (Convert.ToDateTime(x.Element("CreateDate").Value)).ToString("yyyy-MM-dd"),
                    Creator = x.Element("Creator") == null ? string.Empty : x.Element("Creator").Value,
                    DeptID = x.Element("DeptID") == null ? string.Empty : x.Element("DeptID").Value,
                    ModifiedDate = x.Element("ModifiedDate") == null ? string.Empty : (Convert.ToDateTime(x.Element("ModifiedDate").Value)).ToString("yyyy-MM-dd"),



                    ModifiedUserID = x.Element("ModifiedUserID") == null ? string.Empty : x.Element("ModifiedUserID").Value,
                    OutDate = x.Element("OutDate") == null ? string.Empty : (Convert.ToDateTime(x.Element("OutDate").Value)).ToString("yyyy-MM-dd"),
                    OutDeptID = x.Element("OutDeptID") == null ? string.Empty : x.Element("OutDeptID").Value,
                    ReasonType = x.Element("ReasonType") == null ? string.Empty : x.Element("ReasonType").Value,
                    Remark = x.Element("Remark") == null ? string.Empty : x.Element("Remark").Value,
                    StorageID = x.Element("StorageID") == null ? string.Empty : x.Element("StorageID").Value,
                    Summary = x.Element("Summary") == null ? string.Empty : x.Element("Summary").Value,
                    Title = x.Element("Title") == null ? string.Empty : x.Element("Title").Value,
                    TotalPrice = x.Element("TotalPrice") == null ? string.Empty : x.Element("TotalPrice").Value,
                    Transactor = x.Element("Transactor") == null ? string.Empty : x.Element("Transactor").Value,
                    BorrowerText = x.Element("BorrowerText") == null ? string.Empty : x.Element("BorrowerText").Value,
                    TransactorText = x.Element("OuterText") == null ? string.Empty : x.Element("OuterText").Value,
                    CloserText = x.Element("CloserText") == null ? string.Empty : x.Element("CloserText").Value,
                    ConfirmorText = x.Element("ConfirmorText") == null ? string.Empty : x.Element("ConfirmorText").Value,
                    CreatorText = x.Element("CreatorText") == null ? string.Empty : x.Element("CreatorText").Value,
                    ExtField1 = x.Element("ExtField1") == null ? string.Empty : x.Element("ExtField1").Value,
                    ExtField2 = x.Element("ExtField2") == null ? string.Empty : x.Element("ExtField2").Value,
                    ExtField3 = x.Element("ExtField3") == null ? string.Empty : x.Element("ExtField3").Value,
                    ExtField4 = x.Element("ExtField4") == null ? string.Empty : x.Element("ExtField4").Value,
                    ExtField5 = x.Element("ExtField5") == null ? string.Empty : x.Element("ExtField5").Value,
                    ExtField6 = x.Element("ExtField6") == null ? string.Empty : x.Element("ExtField6").Value,
                    ExtField7 = x.Element("ExtField7") == null ? string.Empty : x.Element("ExtField7").Value,
                    ExtField8 = x.Element("ExtField8") == null ? string.Empty : x.Element("ExtField8").Value,
                    ExtField9 = x.Element("ExtField9") == null ? string.Empty : x.Element("ExtField9").Value,
                    ExtField10 = x.Element("ExtField10") == null ? string.Empty : x.Element("ExtField10").Value,
                    DeptName = GetElementValue(x, "DeptName", false),
                    OutDeptName = GetElementValue(x, "OutDeptName", false)
                })

                         :
               (from x in dsXML.Descendants("Data")
                orderby x.Element(orderBy).Value descending
                select new DataSourceModel()
                {

                    BillStatus = x.Element("BillStatus") == null ? string.Empty : x.Element("BillStatus").Value,
                    BorrowDate = string.IsNullOrEmpty(x.Element("BorrowDate").Value) ? string.Empty : (Convert.ToDateTime(x.Element("BorrowDate").Value)).ToString("yyyy-MM-dd"),
                    Borrower = x.Element("Borrower") == null ? string.Empty : x.Element("Borrower").Value,
                    BorrowNo = x.Element("BorrowNo") == null ? string.Empty : x.Element("BorrowNo").Value,
                    CloseDate = x.Element("CloseDate") == null ? string.Empty : (Convert.ToDateTime(x.Element("CloseDate").Value)).ToString("yyyy-MM-dd"),
                    Closer = x.Element("Closer") == null ? string.Empty : x.Element("Closer").Value,


                    CompanyCD = x.Element("CompanyCD") == null ? string.Empty : x.Element("CompanyCD").Value,
                    ConfirmDate = x.Element("ConfirmDate") == null ? string.Empty : (Convert.ToDateTime(x.Element("ConfirmDate").Value)).ToString("yyyy-MM-dd"),
                    Confirmor = x.Element("Confirmor") == null ? string.Empty : x.Element("Confirmor").Value,
                    CountTotal = x.Element("CountTotal") == null ? string.Empty : x.Element("CountTotal").Value,

                    CreateDate = x.Element("CreateDate") == null ? string.Empty : (Convert.ToDateTime(x.Element("CreateDate").Value)).ToString("yyyy-MM-dd"),
                    Creator = x.Element("Creator") == null ? string.Empty : x.Element("Creator").Value,
                    DeptID = x.Element("DeptID") == null ? string.Empty : x.Element("DeptID").Value,
                    ModifiedDate = x.Element("ModifiedDate") == null ? string.Empty : (Convert.ToDateTime(x.Element("ModifiedDate").Value)).ToString("yyyy-MM-dd"),



                    ModifiedUserID = x.Element("ModifiedUserID") == null ? string.Empty : x.Element("ModifiedUserID").Value,
                    OutDate = x.Element("OutDate") == null ? string.Empty : (Convert.ToDateTime(x.Element("OutDate").Value)).ToString("yyyy-MM-dd"),
                    OutDeptID = x.Element("OutDeptID") == null ? string.Empty : x.Element("OutDeptID").Value,
                    ReasonType = x.Element("ReasonType") == null ? string.Empty : x.Element("ReasonType").Value,
                    Remark = x.Element("Remark") == null ? string.Empty : x.Element("Remark").Value,
                    StorageID = x.Element("StorageID") == null ? string.Empty : x.Element("StorageID").Value,
                    Summary = x.Element("Summary") == null ? string.Empty : x.Element("Summary").Value,
                    Title = x.Element("Title") == null ? string.Empty : x.Element("Title").Value,
                    TotalPrice = x.Element("TotalPrice") == null ? string.Empty : x.Element("TotalPrice").Value,
                    Transactor = x.Element("Transactor") == null ? string.Empty : x.Element("Transactor").Value,
                    BorrowerText = x.Element("BorrowerText") == null ? string.Empty : x.Element("BorrowerText").Value,
                    TransactorText = x.Element("OuterText") == null ? string.Empty : x.Element("OuterText").Value,
                    CloserText = x.Element("CloserText") == null ? string.Empty : x.Element("CloserText").Value,
                    ConfirmorText = x.Element("ConfirmorText") == null ? string.Empty : x.Element("ConfirmorText").Value,
                    CreatorText = x.Element("CreatorText") == null ? string.Empty : x.Element("CreatorText").Value,
                    ExtField1 = x.Element("ExtField1") == null ? string.Empty : x.Element("ExtField1").Value,
                    ExtField2 = x.Element("ExtField2") == null ? string.Empty : x.Element("ExtField2").Value,
                    ExtField3 = x.Element("ExtField3") == null ? string.Empty : x.Element("ExtField3").Value,
                    ExtField4 = x.Element("ExtField4") == null ? string.Empty : x.Element("ExtField4").Value,
                    ExtField5 = x.Element("ExtField5") == null ? string.Empty : x.Element("ExtField5").Value,
                    ExtField6 = x.Element("ExtField6") == null ? string.Empty : x.Element("ExtField6").Value,
                    ExtField7 = x.Element("ExtField7") == null ? string.Empty : x.Element("ExtField7").Value,
                    ExtField8 = x.Element("ExtField8") == null ? string.Empty : x.Element("ExtField8").Value,
                    ExtField9 = x.Element("ExtField9") == null ? string.Empty : x.Element("ExtField9").Value,
                    ExtField10 = x.Element("ExtField10") == null ? string.Empty : x.Element("ExtField10").Value,
                    DeptName = GetElementValue(x, "DeptName", false),
                    OutDeptName = GetElementValue(x, "OutDeptName", false)

                });
        int totalCount = dsLinq.Count();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        sb.Append(ToJSON(dsLinq.ToList()));
        sb.Append("}");
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
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

    public class DataSourceModel
    {
        public string CompanyCD { get; set; }
        public string BorrowNo { get; set; }
        public string Title { get; set; }
        public string Borrower { get; set; }
        public string DeptID { get; set; }
        public string BorrowDate { get; set; }
        public string ReasonType { get; set; }
        public string OutDeptID { get; set; }
        public string StorageID { get; set; }
        public string OutDate { get; set; }
        public string Transactor { get; set; }
        public string TotalPrice { get; set; }
        public string CountTotal { get; set; }
        public string Summary { get; set; }
        public string Remark { get; set; }
        public string Creator { get; set; }
        public string CreateDate { get; set; }
        public string BillStatus { get; set; }
        public string Confirmor { get; set; }
        public string ConfirmDate { get; set; }
        public string Closer { get; set; }
        public string CloseDate { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedUserID { get; set; }
        public string BorrowerText { get; set; }
        public string TransactorText { get; set; }
        public string ConfirmorText { get; set; }
        public string CloserText { get; set; }
        public string CreatorText { get; set; }
        public string DeptName { get; set; }
        public string OutDeptName { get; set; }
        public string ExtField1 { get; set; }
        public string ExtField2 { get; set; }
        public string ExtField3 { get; set; }
        public string ExtField4 { get; set; }
        public string ExtField5 { get; set; }
        public string ExtField6 { get; set; }
        public string ExtField7 { get; set; }
        public string ExtField8 { get; set; }
        public string ExtField9 { get; set; }
         public string ExtField10 { get; set; }

    }
    //判断节点是否存在 及格式化日期
    protected string GetElementValue(XElement x, string Key, bool IsDate)
    {
        if (x.Element(Key) != null)
        {
            string tempValue = x.Element(Key).Value;
            if (IsDate)
            {
                if (string.IsNullOrEmpty(tempValue))
                {
                    return string.Empty;
                }
                else
                    return Convert.ToDateTime(tempValue).ToString("yyyy-MM-dd");
            }
            else
                return x.Element(Key).Value;
        }
        else
            return string.Empty;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
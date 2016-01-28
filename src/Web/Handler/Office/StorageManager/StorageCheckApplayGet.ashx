<%@ WebHandler Language="C#" Class="StorageCheckApplayGet" %>

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
using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;
public class StorageCheckApplayGet : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.RequestType == "POST")
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "ascending";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
            string method = context.Request.Form["method"].ToString();
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            string BillNo = context.Request.Form["TheBillNO"].ToString();
            string BillTitle = context.Request.Form["TheBillTitle"].ToString();
            string ReprotStr = BillNo + "?" + BillTitle;
                
            
            XElement dsXML = ConvertDataTableToXML(CheckReportBus.GetCheckApplay(method,ReprotStr));
            
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     ApplyNo = x.Element("ApplyNo").Value,
                     ApplyUserID = x.Element("ApplyUserID").Value,
                     ApplyUserName = x.Element("ApplyUserName").Value,
                     CheckDeptId = x.Element("CheckDeptId").Value,
                     CheckDeptName = x.Element("CheckDeptName").Value,
                     CheckMode = x.Element("CheckMode").Value,
                     CheckModeName = x.Element("CheckModeName").Value,
                     CheckType = x.Element("CheckType").Value,
                     CheckTypeName = x.Element("CheckTypeName").Value,
                     CustBigTypeID = x.Element("CustBigTypeID").Value,
                     CustBigTypeName = x.Element("CustBigTypeName").Value,
                     CustID = x.Element("CustID").Value,
                     CustName = x.Element("CustName").Value,
                     DeptID = x.Element("DeptID").Value,
                     DeptName = x.Element("DeptName").Value,
                     ID = x.Element("ID").Value,
                     PrincipalID = x.Element("PrincipalID").Value,
                     PrincipalName = x.Element("PrincipalName").Value,
                     Title = x.Element("Title").Value,
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                    ApplyNo=x.Element("ApplyNo").Value,
                    ApplyUserID=x.Element("ApplyUserID").Value,
                    ApplyUserName=x.Element("ApplyUserName").Value,
                    CheckDeptId=x.Element("CheckDeptId").Value,
                    CheckDeptName=x.Element("CheckDeptName").Value,
                    CheckMode=x.Element("CheckMode").Value,
                    CheckModeName=x.Element("CheckModeName").Value,
                    CheckType=x.Element("CheckType").Value,
                    CheckTypeName=x.Element("CheckTypeName").Value,
                    CustBigTypeID=x.Element("CustBigTypeID").Value,
                    CustBigTypeName=x.Element("CustBigTypeName").Value,
                    CustID=x.Element("CustID").Value,
                    CustName=x.Element("CustName").Value,
                    DeptID=x.Element("DeptID").Value,
                    DeptName=x.Element("DeptName").Value,
                    ID=x.Element("ID").Value,
                    PrincipalID=x.Element("PrincipalID").Value,
                    PrincipalName=x.Element("PrincipalName").Value,
                    Title=x.Element("Title").Value,
                     
                 });
            int totalCount = dsLinq.Count();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(totalCount.ToString());
            sb.Append(",data:");
            sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
    }
    //数据源结构
    public class DataSourceModel
    {
        public string ID { get; set; }         //申请单ID
        public string Title { get; set; }      //申请单标题
        public string CustID { get; set; }      //往来单位ID
        public string CustName { get; set; }    //往来单位名称
        public string CustBigTypeID { get; set; }  //往来单位大类ID
        public string CustBigTypeName { get; set; }  //往来单位大类名称
        public string PrincipalID { get; set; }  //生产负责人ID
        public string PrincipalName { get; set; }//生产负责人名称
        public string DeptID { get; set; }     //生产部门ID
        public string DeptName { get; set; }   //生产部门名称
        public string CheckType { get; set; } //质检类别
        public string CheckTypeName { get; set; }
        public string CheckMode { get; set; }  //检验方式
        public string CheckModeName { get; set; }
        public string CheckDeptId { get; set; } //报检部门
        public string CheckDeptName { get; set; } //报检部门
        public string ApplyUserID { get; set; }   //报检人
        public string ApplyUserName { get; set; }
        public string ApplyNo { get; set; }
        
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

}
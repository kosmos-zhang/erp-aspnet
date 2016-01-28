<%@ WebHandler Language="C#" Class="EquipmentInfo" %>
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
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;
using System.Web.SessionState;

public class EquipmentInfo : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context) 
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            
            var ActionApply = context.Request.Params["ActionApply"];
            if (ActionApply == "Select"||ActionApply == null)
            {
                string orderString = (context.Request.Params["orderBy"].ToString());//排序
                string order = "asc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                {
                    order = "desc";//排序：降序
                }
                orderBy = orderBy + " " + order;
                int pageCount = int.Parse(context.Request.Params["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Params["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
                int totalCount = 0;
                
                //获取数据
                PurchaseApplyModel PurchaseApplyM = new PurchaseApplyModel();
                PurchaseApplyM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                PurchaseApplyM.ApplyNo = context.Request.Params["ApplyNo"];
                PurchaseApplyM.Title = context.Request.Params["Title"];
                PurchaseApplyM.ApplyUserID = context.Request.Params["ApplyUserID"];
                PurchaseApplyM.ApplyDeptID = context.Request.Params["ApplyDeptID"];
                PurchaseApplyM.TypeID = context.Request.Params["TypeID"];
                PurchaseApplyM.FromType = context.Request.Params["FromType"];
                PurchaseApplyM.StartApplyDate = context.Request.Params["StartApplyDate"];
                PurchaseApplyM.EndApplyDate = context.Request.Params["EndApplyDate"];
                PurchaseApplyM.BillStatus = context.Request.Params["BillStatus"];
                PurchaseApplyM.FlowStatus = context.Request.Params["FlowStatus"];
                PurchaseApplyM.EFDesc = context.Request.Params["EFDesc"];
                PurchaseApplyM.EFIndex = context.Request.Params["EFIndex"];
                DataTable dt = PurchaseApplyBus.SelectPurchaseApply(PurchaseApplyM, pageIndex, pageCount, orderBy, ref totalCount);
                //XElement dsXML = ConvertDataTableToXML(PurchaseApplyBus.SelectPurchaseApply(PurchaseApplyM));
                ////linq排序
                //var dsLinq =
                //    (order == "ascending") ?
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value ascending
                //     select new DataSourceModel()
                //     {
                //         ID = x.Element("ID").Value,
                //         ApplyNo = x.Element("ApplyNo").Value,
                //         Title = x.Element("Title").Value,
                //         FromType = x.Element("FromType").Value,
                //         FromTypeName = x.Element("FromTypeName").Value,
                //         ApplyUserID = x.Element("ApplyUserID").Value,
                //         ApplyUserName = x.Element("ApplyUserName").Value,
                //         ApplyDeptID = x.Element("ApplyDeptID").Value,
                //         ApplyDeptName = x.Element("ApplyDeptName").Value,
                //         TypeID = x.Element("TypeID").Value,
                //         TypeName = x.Element("TypeName").Value,
                //         ApplyDate = x.Element("ApplyDate").Value,
                //         BillStatus = x.Element("BillStatus").Value,
                //         BillStatusName = x.Element("BillStatusName").Value,
                //         FlowStatus = x.Element("FlowStatus").Value,
                //         FlowStatusName = x.Element("FlowStatusName").Value,
                //         //IsCite = x.Element("IsCite").Value,
                //         //IsCiteName = x.Element("IsCiteName").Value,
                //         //Auditing = x.Element("Auditing").Value,
                //         //AuditingName = x.Element("AuditingName").Value,
                //     })
                //              :
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value descending
                //     select new DataSourceModel()
                //     {
                //         ID = x.Element("ID").Value,
                //         ApplyNo = x.Element("ApplyNo").Value,
                //         Title = x.Element("Title").Value,
                //         FromType = x.Element("FromType").Value,
                //         FromTypeName = x.Element("FromTypeName").Value,
                //         ApplyUserID = x.Element("ApplyUserID").Value,
                //         ApplyUserName = x.Element("ApplyUserName").Value,
                //         ApplyDeptID = x.Element("ApplyDeptID").Value,
                //         ApplyDeptName = x.Element("ApplyDeptName").Value,
                //         TypeID = x.Element("TypeID").Value,
                //         TypeName = x.Element("TypeName").Value,
                //         ApplyDate = x.Element("ApplyDate").Value,
                //         BillStatus = x.Element("BillStatus").Value,
                //         BillStatusName = x.Element("BillStatusName").Value,
                //         FlowStatus = x.Element("FlowStatus").Value,
                //         FlowStatusName = x.Element("FlowStatusName").Value,
                //         //IsCite = x.Element("IsCite").Value,
                //         //IsCiteName = x.Element("IsCiteName").Value,
                //         //Auditing = x.Element("Auditing").Value,
                //         //AuditingName = x.Element("AuditingName").Value,
                //     });
                //int totalCount = dsLinq.Count();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt.Rows.Count == 0)
                {
                    sb.Append("\"\"");
                }
                else
                {
                    sb.Append(JsonClass.DataTable2Json(dt));
                }
                //sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
                sb.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
            }
            else if (ActionApply == "Delete")
            {
                JsonClass jc;
                string IDs = context.Request.Params["IDs"];
                string ApplyNos = context.Request.Params["ApplyNos"];

                if (PurchaseApplyBus.DeleteApply(ApplyNos, IDs) == true)
                {
                    jc = new JsonClass("success", "", 1);
                    context.Response.Write(jc);
                }
                else
                {
                    
                }
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

 
    public bool IsReusable {
        get {
            return false;
        }
    }
    //数据源结构
    public class DataSourceModel
    {
        public string ID { get; set; }
        public string ApplyNo { get; set; }
        public string Title { get; set; }
        public string FromType { get; set; }
        public string FromTypeName { get; set; }
        public string ApplyUserID { get; set; }
        public string ApplyUserName { get; set; }
        public string ApplyDeptID { get; set; }
        public string ApplyDeptName { get; set; }
        public string TypeID { get; set; }
        public string TypeName { get; set; }
        public string ApplyDate { get; set; }
        public string BillStatus { get; set; }
        public string BillStatusName { get; set; }
        public string FlowStatus { get; set; }
        public string FlowStatusName { get; set; }
        //public string IsCite { get; set; }
        //public string IsCiteName { get; set; }
        //public string Auditing { get; set; }
        //public string AuditingName { get; set; } 

    }
}
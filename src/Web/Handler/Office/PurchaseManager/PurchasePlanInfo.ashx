<%@ WebHandler Language="C#" Class="PurchasePlanInfo" %>

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

public class PurchasePlanInfo : IHttpHandler,IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        if (context.Request.RequestType == "POST")
        {
            int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            string ActionPlan = context.Request.QueryString["ActionPlan"];
            if (ActionPlan == "Delete")
            {
                JsonClass jc;
                string IDs = context.Request.QueryString["IDs"];
                string PlanNos = context.Request.QueryString["PlanNos"];

                if (true == PurchasePlanBus.DeletePurchasePlanAll(IDs, PlanNos))
                {
                    jc = new JsonClass("success", "", 1);
                    context.Response.Write(jc);
                }
            }
            else if(ActionPlan == "Select")
            {
                //设置行为参数
                string orderString = (context.Request.QueryString["orderBy"]);//排序
                string order = "asc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                {
                    order = "desc";//排序：降序
                }
                orderBy = orderBy + " " + order;
                int pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
                int totalCount =0;
                PurchasePlanModel PurchasePlanM = new PurchasePlanModel();
                PurchasePlanM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                PurchasePlanM.PlanNo = context.Request.QueryString["No"];
                PurchasePlanM.Title = context.Request.QueryString["Title"];
                
                PurchasePlanM.PlanUserID = context.Request.QueryString["PlanUser"];
                PurchasePlanM.PlanMoney = context.Request.QueryString["TotalMoneyMin"];
                PurchasePlanM.TotalMoneyMax = context.Request.QueryString["TotalMoneyMax"];
                PurchasePlanM.PlanDeptID = context.Request.QueryString["DeptID"];
                PurchasePlanM.PlanDate = context.Request.QueryString["StartPlanDate"];
                PurchasePlanM.EndPlanDate = context.Request.QueryString["EndPlanDate"];
                PurchasePlanM.BillStatus = context.Request.QueryString["BillStatus"];
                PurchasePlanM.FlowStatus = context.Request.QueryString["FlowStatus"];
                PurchasePlanM.EFDesc = context.Request.QueryString["EFDesc"];
                PurchasePlanM.EFIndex = context.Request.QueryString["EFIndex"];
                //XElement dsXML = ConvertDataTableToXML(PurchasePlanBus.SelectPurchasePlan(PurchasePlanM));
                DataTable dt = PurchasePlanBus.SelectPurchasePlan(PurchasePlanM, pageIndex, pageCount, orderBy, ref totalCount);
                //linq排序
                //var dsLinq =
                //    (order == "ascending") ?
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value ascending
                //     select new DataSourceModel()
                //     {
                //         ID = x.Element("ID").Value,
                //         PlanNo = x.Element("PlanNo").Value,
                //         PlanTitle = x.Element("PlanTitle").Value,
                //         PlanUserID = x.Element("PlanUserID").Value,
                //         PlanUserName = x.Element("PlanUserName").Value,
                //         PlanDate = x.Element("PlanDate").Value,
                //         PlanDeptID = x.Element("PlanDeptID").Value,
                //         PlanDeptName = x.Element("PlanDeptName").Value,
                //         PlanMoney = x.Element("PlanMoney").Value,
                //         BillStatus = x.Element("BillStatus").Value,
                //         BillStatusName = x.Element("BillStatusName").Value,
                //         FlowStatus = x.Element("FlowStatus").Value,
                //         FlowStatusName = x.Element("FlowStatusName").Value,
                //         //IsCite = x.Element("IsCite").Value,
                //     })
                //              :
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value descending
                //     select new DataSourceModel()
                //     {
                //         ID = x.Element("ID").Value,
                //         PlanNo = x.Element("PlanNo").Value,
                //         PlanTitle = x.Element("PlanTitle").Value,
                //         PlanUserID = x.Element("PlanUserID").Value,
                //         PlanUserName = x.Element("PlanUserName").Value,
                //         PlanDate = x.Element("PlanDate").Value,
                //         PlanDeptID = x.Element("PlanDeptID").Value,
                //         PlanDeptName = x.Element("PlanDeptName").Value,
                //         PlanMoney = x.Element("PlanMoney").Value,
                //         BillStatus = x.Element("BillStatus").Value,
                //         BillStatusName = x.Element("BillStatusName").Value,
                //         FlowStatus = x.Element("FlowStatus").Value,
                //         FlowStatusName = x.Element("FlowStatusName").Value,
                //         //IsCite = x.Element("IsCite").Value,
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
        public string PlanNo { get; set; }
        public string PlanTitle { get; set; }
        public string PlanUserID { get; set; }
        public string PlanUserName { get; set; }
        public string PlanDate { get; set; }
        public string PlanDeptID { get; set; }
        public string PlanDeptName { get; set; }
        public string PlanMoney { get; set; }
        public string BillStatus { get; set; }
        public string BillStatusName { get; set; }
        public string FlowStatus { get; set; }
        public string FlowStatusName { get; set; }
        //public string IsCite { get; set; }
    }
    

}
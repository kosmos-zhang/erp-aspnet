<%@ WebHandler Language="C#" Class="PurchaseRequireInfo" %>

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


public class PurchaseRequireInfo : IHttpHandler, IRequiresSessionState 
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string action = context.Request.QueryString["action"];
            if (action == "Delete")
            {//删除
                JsonClass jc;
                int Length = Convert.ToInt32(context.Request.QueryString["Length"]);
                int[] IDS = new int[Length];
                for (int i = 0; i < Length; ++i)
                {
                    IDS[i] = Convert.ToInt32(context.Request.QueryString["ID" + i]);
                }
                if (PurchaseRequireBus.DeletePurchaseRequireInfo(IDS) == true)
                {
                    jc = new JsonClass("success", "", 1);
                }
                else
                {
                    jc = new JsonClass("faile", "", 0);
                }
                context.Response.Write(jc);
            }
            else if (action == "Generate")
            {//生成采购计划

            }
            else if (action == null)
            {
                //设置行为参数
                string orderString = context.Request.QueryString["orderby"];//排序
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
                int totalCount = 0;

                PurchaseRequireModel PurchaseRequireM = new PurchaseRequireModel();
                PurchaseRequireM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                PurchaseRequireM.ProdTypeID = context.Request.Params["ProductTypeID"];
                PurchaseRequireM.ProdTypeName = context.Request.Form["ProductTypeName"];
                PurchaseRequireM.ProdID = context.Request.Params["ProductID"];
                PurchaseRequireM.ProdName = context.Request.Params["ProductName"];
                PurchaseRequireM.CreateCondition = context.Request.Params["CreateCondition"];
                PurchaseRequireM.RequireDate = context.Request.Params["StartDate"];
                PurchaseRequireM.EndRequireDate = context.Request.Params["EndDate"];

                DataTable dt = PurchaseRequireBus.GetPurchaseRequireInfo(PurchaseRequireM, pageIndex, pageCount, orderBy, ref totalCount);
                //XElement dsXML = ConvertDataTableToXML(PurchaseRequireBus.GetPurchaseRequireInfo(PurchaseRequireM));
                ////linq排序
                //var dsLinq =
                //    (order == "ascending") ?
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value ascending
                //     select new DataSourceModel()
                //     {
                //         ID = x.Element("ID").Value,
                //         MRPNo = x.Element("MRPNo").Value,
                //         ProdID = x.Element("ProdID").Value,
                //         ProdNo = x.Element("ProdNo").Value,
                //         ProductName = x.Element("ProductName").Value,
                //         ProdTypeID = x.Element("ProdTypeID").Value,
                //         ProdTypeName = x.Element("ProductTypeName").Value,
                //         Specification = x.Element("Specification").Value,
                //         UnitID = x.Element("UnitID").Value,
                //         UnitName = x.Element("UnitName").Value,
                //         NeedCount = x.Element("NeedCount").Value,
                //         HasNum = x.Element("HasNum").Value,
                //         WantingNum = x.Element("WantingNum").Value,
                //         WaitingDays = x.Element("WaitingDays").Value,
                //         RequireDate = x.Element("RequireDate").Value,
                //         OrderCount = x.Element("OrderCount").Value,
                //     })
                //              :
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value descending
                //     select new DataSourceModel()
                //     {
                //         ID = x.Element("ID").Value,
                //         MRPNo = x.Element("MRPNo").Value,
                //         ProdID=x.Element("ProdID").Value,
                //         ProdNo = x.Element("ProdNo").Value,
                //         ProductName = x.Element("ProductName").Value,
                //         ProdTypeID = x.Element("ProdTypeID").Value,
                //         ProdTypeName = x.Element("ProductTypeName").Value,
                //         Specification = x.Element("Specification").Value,
                //         UnitID = x.Element("UnitID").Value,
                //         UnitName = x.Element("UnitName").Value,
                //         NeedCount = x.Element("NeedCount").Value,
                //         HasNum = x.Element("HasNum").Value,
                //         WantingNum = x.Element("WantingNum").Value,
                //         WaitingDays = x.Element("WaitingDays").Value,
                //         RequireDate = x.Element("RequireDate").Value,
                //         OrderCount = x.Element("OrderCount").Value,
                //     });
                //int totalCount = dsLinq.Count();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt.Rows.Count >0)
                {
                  sb.Append(JsonClass.DataTable2Json(dt));  
                }
                else
                {
                    sb.Append("\"\"");
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
        public string MRPID { get; set; }
        public string MRPNo { get; set; }
        public string ProdID { get; set; }
        public string ProdNo { get; set; }
        public string ProductName { get; set; }
        public string ProdTypeID { get; set; }
        public string ProdTypeName { get; set; }
        public string Specification { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string NeedCount { get; set; }
        public string HasNum { get; set; }
        public string WantingNum { get; set; }
        public string WaitingDays { get; set; }
        public string RequireDate { get; set; }
        public string Remark { get; set; }
        public string OrderCount { get; set; }
    }
    

}
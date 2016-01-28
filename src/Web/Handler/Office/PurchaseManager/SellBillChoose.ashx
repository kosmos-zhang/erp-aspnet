<%@ WebHandler Language="C#" Class="SellBillChoose" %>

using System;
using System.Web;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Model.Office.SellManager;
using System.Web.SessionState;


public class SellBillChoose : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context) 
    {
       if (context.Request.RequestType == "POST")
        {
            //设置行为参数
           
            string OrderBy = context.Request.Form["OrderBy"];//排序
            string OrderByType = context.Request.Form["OrderByType"];//排序类型
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

            string ProductNo = context.Request.Form["ProductNo"];
            string ProductName = context.Request.Form["ProductName"];
            string StartDate = context.Request.Form["StartDate"];
            string EndDate = context.Request.Form["EndDate"];
            ///*解析自定义属性参数*/
            //string EFIndex = context.Request.Form["EFIndex"].ToString().Trim();
            //string EFDesc = context.Request.Form["EFDesc"].ToString().Trim();
            //if (EFIndex != "-1" && EFIndex != "" && EFDesc != "")
            //{
            //    EFIndex = "ExtField" + EFIndex;
            //    EFDesc = "%" + EFDesc + "%";
            //}
           
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            int totalCount = 0;


           // DataTable dtdata = PurchaseApplyBus.GetSellOrderDetail(CompanyCD, EFIndex, EFDesc, ProductNo, ProductName, StartDate, EndDate, pageIndex, pageCount, OrderBy, OrderByType, ref totalCount);
            DataTable dtdata = PurchaseApplyBus.GetSellOrderDetail(CompanyCD, ProductNo, ProductName, StartDate, EndDate, pageIndex, pageCount, OrderBy, OrderByType, ref totalCount);
            DataTable dt = GetNewDataTable(dtdata, string.Empty, OrderBy + " " + OrderByType);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(totalCount.ToString());
            sb.Append(",data:");
            if (dt.Rows.Count > 0)
            {
                sb.Append(JsonClass.DataTable2Json(dt));
            }
            else
            {
                sb.Append("\"\"");
            }
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
    }
    private DataTable GetNewDataTable(DataTable dt, string condition, string Order)
    {
        DataTable newdt = new DataTable();
        newdt = dt.Clone();
        DataRow[] dr = dt.Select(condition, Order);
        for (int i = 0; i < dr.Length; i++)
        {
            newdt.ImportRow((DataRow)dr[i]);
        }
        return newdt;//返回的查询结果
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
        public string OrderNo { get; set; }
        public string SortNo { get; set; }
        public string ProductID { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ProductCount { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string Specification { get; set; }
        public string SendDate { get; set; }

        //public string FromDeptID { get; set; }
        //public string FromDeptName { get; set; }


    }

}
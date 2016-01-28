<%@ WebHandler Language="C#" Class="CustInfoList" %>

using System;
using System.Web;
using XBase.Model.Office.CustManager;
using System.IO;
using System.Data;
using XBase.Business.Office.CustManager;
using XBase.Common;

public class CustInfoList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{    
    public void ProcessRequest (HttpContext context) {

        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CustNo";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据
        CustInfoModel CustInfoM = new CustInfoModel();

        CustInfoM.CustTypeManage = context.Request.Form["CustTypeManage"].ToString();
        CustInfoM.CustTypeSell = context.Request.Form["CustTypeSell"].ToString();
        CustInfoM.CustTypeTime = context.Request.Form["CustTypeTime"].ToString();
        CustInfoM.CustType = context.Request.Form["CustType"].ToString() == "" ? 0 : Convert.ToInt32(context.Request.Form["CustType"].ToString());        
        CustInfoM.CustClass = (context.Request.Form["CustClass"].ToString() == "") ? 0 : Convert.ToInt32(context.Request.Form["CustClass"].ToString());
        CustInfoM.CreditGrade = context.Request.Form["CreditGrade"].ToString() == "" ? 0 : Convert.ToInt32(context.Request.Form["CreditGrade"].ToString());
        CustInfoM.RelaGrade = context.Request.Form["RelaGrade"].ToString();
        CustInfoM.AreaID = context.Request.Form["AreaID"].ToString() == "0" ? 0 : Convert.ToInt32(context.Request.Form["AreaID"].ToString());
        CustInfoM.Seller = context.Request.Form["ManagerID"].ToString() == "" ? 0 : Convert.ToInt32(context.Request.Form["ManagerID"].ToString());

        CustInfoM.CompanyCD =  ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();

        string ord = orderBy + " " + order;
        int totalCount = 0;

        DataTable dt = CustInfoBus.GetCustListByCondition(CustInfoM,CanUserID, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        //sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));       
        //sb.Append("}");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));            
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

    //把DataTable转换为XML流
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

    public class DataSourceModel
    {
        public string ID { get; set; }
        
        public string CustNo { get; set; }        
        public string CustName { get; set; }
        public string CustTypeManage { get; set; }
        public string CustTypeSell { get; set; }
        public string CustTypeTime { get; set; }

        public string CustTypeName { get; set; }
        public string CustClassName { get; set; }
        public string CreditGradeName { get; set; }
        public string RelaGrade { get; set; }
        public string Area { get; set; }

        public string Province { get; set; }
        public string City { get; set; }
        public string ManagerName { get; set; }
        public string ContactName { get; set; }
        public string Tel { get; set; }
        
        public string CreatorName { get; set; }
        public string CreatedDate { get; set; }
    }

 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
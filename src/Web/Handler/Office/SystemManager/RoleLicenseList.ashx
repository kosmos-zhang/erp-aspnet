<%@ WebHandler Language="C#" Class="RoleLicenseList" %>

using System;
using System.Web;
using XBase.Business.Office.SystemManager;
using System.Data;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Web.Script.Serialization;
using XBase.Common;
public class RoleLicenseList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModuleName";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        int totalCount = 0;
        string ord = orderBy + " " + order;
        //获取数据
        string ModName = context.Request.Form["ModName"].ToString();
        string RoleID = context.Request.Form["RoleID"].ToString();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        DataTable dt = RoleInfoBus.GetRoleFunction(CompanyCD, RoleID, ModName, pageIndex, pageCount, ord, ref totalCount);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"id\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    public static string ToJSON(object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(obj);
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
    public class DataSourceModel
    {
        public string ModuleID { get; set; }
        public string FunctionCD { get; set; }

        public string FunctionName { get; set; }
        public string CompanyCD { get; set; }
        public string ModuleName { get; set; }

        public string RoleName { get; set; }

        public string ModifiedDate { get; set; }
        public string ModifiedUserID { get; set; }
        public string RoleID { get; set; }

        public string ID { get; set; }
    }
}
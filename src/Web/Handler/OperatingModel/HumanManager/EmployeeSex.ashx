<%@ WebHandler Language="C#" Class="EmployeeSex" %>

using System;
using System.Web;
using XBase.Common;
using System.Data;
using XBase.Business.Office.HumanManager;

public class EmployeeSex : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
       string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "num";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据       
        string DeptID = context.Request.Form["DeptID"].ToString();       
        string CompanyCD =  ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        string ord = orderBy + " " + order;
        int totalCount = 0;

        DataTable dt = EmployeeInfoBus.GetEmployeeSex(CompanyCD, DeptID, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"DeptID\":\"\"}]");
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
}
<%@ WebHandler Language="C#" Class="UserRoleList" %>

using System;
using System.Web;
using XBase.Business.Office.SystemManager;
using System.Data;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Web.Script.Serialization;
public class UserRoleList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        string orderString = (context.Request.Form["orderby"] == null ? " " : context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "UserID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"] == null ? "10" : context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"] == null ? "1" : context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        int totalCount = 0;
        string ord = orderBy + " " + order;
        //获取数据
        try
        {
            string UserID = context.Request.Form["UserID"] == null ? "" : context.Request.Form["UserID"].ToString();
            string RoleID = context.Request.Form["RoleID"] == null ? "" : context.Request.Form["RoleID"].ToString();
            string UserName = context.Request.Form["UserName"] == null ? "" : context.Request.Form["UserName"].ToString();
            //linq排序
            DataTable dt = UserRoleBus.GetUserRoleByConditions(RoleID, UserID, UserName, pageIndex, pageCount, ord, ref totalCount);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append( Convert .ToString ( totalCount));
            sb.Append(",data:");
            if (dt.Rows.Count > 0)
            {
                      sb.Append(JsonClass.DataTable2Json(dt));
            }
            else
            {
                     sb.Append("[{\"UserID\":\"\"}]");
            }
           
          
            sb.Append("}");
            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
        catch
        {}
    }



    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


}
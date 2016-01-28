<%@ WebHandler Language="C#" Class="UserInfoList" %>

using System;
using System.Web;
using System.Data;
using System.Xml.Linq;
using XBase.Model.Office.SystemManager;
using XBase.Business.Office.SystemManager;
using System.IO;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Serialization;
using XBase.Common;
using System.Linq;
public class UserInfoList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "asc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "UserID";//要排序的字段，如果为空，默认为"ID"
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
            UserInfoModel model = new UserInfoModel();
            if (!string.IsNullOrEmpty(context.Request.Form["UserID"].ToString()))
            model.UserID = context.Request.Form["UserID"].ToString();
            model.UserName = "";
            if (!string.IsNullOrEmpty(context.Request.Form["LockFlag"].ToString()))
            model.LockFlag = context.Request.Form["LockFlag"].ToString();
            if (!string.IsNullOrEmpty(context.Request.Form["OpenDate"].ToString()))
            model.OpenDate = DateTime.Parse( context.Request.Form["OpenDate"].ToString());
            if (!string.IsNullOrEmpty(context.Request.Form["CloseDate"].ToString()))
            model.CloseDate = DateTime.Parse(context.Request.Form["CloseDate"].ToString());
            model.EmployeeID = int.Parse(context.Request.Form["UserName"].ToString());
            if (!string.IsNullOrEmpty(context.Request.Form["IsHardValidate"].ToString()))
                model.IsHardValidate = context.Request.Form["IsHardValidate"].ToString();
            //DataTable dt=UserInfoBus.GetUserInfo(model);
            DataTable dt = UserInfoBus.GetUserInfo(model, pageIndex, pageCount, ord, ref totalCount);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt.Rows.Count == 0)
                    sb.Append("[{\"UserID\":\"\"}]");
                else
                    sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");
                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
        }
    }
  

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
<%@ WebHandler Language="C#" Class="LoveInfo" %>

using System;
using System.Web;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;
using System.Data;
using System.Xml.Linq;
using XBase.Common;
using System.IO;
using System.Web.Script.Serialization;
using System.Linq;

public class LoveInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{    
    public void ProcessRequest (HttpContext context) {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "LoveDate";//要排序的字段，如果为空，默认为"linkmanname"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据
        CustLoveModel CustLoveM = new CustLoveModel();
        string CustName = context.Request.Form["CustName"].ToString().Trim();//客户名称        
        CustLoveM.LoveType = Convert.ToInt32(context.Request.Form["LoveType"].ToString().Trim());//投诉类型
        string LoveBegin = context.Request.Form["LoveBegin"].ToString().Trim();// == "" ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(context.Request.Form["LoveBegin"].ToString());//开始时间
        string LoveEnd = context.Request.Form["LoveEnd"].ToString().Trim();//== "" ? Convert.ToDateTime("9999-12-31") : Convert.ToDateTime(context.Request.Form["LoveEnd"].ToString() + " 23:59:59.000");//结束时间            
        string CustLinkMan = context.Request.Form["CustLinkMan"].ToString().Trim();//客户联系人
        CustLoveM.Title = context.Request.Form["Title"].ToString().Trim();//主题

        CustLoveM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Manager = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID

        string ord = orderBy + " " + order;
        int totalCount = 0;

        DataTable dt = LoveBus.GetLoveInfoBycondition(Manager,CustName, CustLoveM, LoveBegin, LoveEnd, CustLinkMan, pageIndex, pageCount, ord, ref totalCount);
        
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt == null)
            sb.Append("[{\"id\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt)); 
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
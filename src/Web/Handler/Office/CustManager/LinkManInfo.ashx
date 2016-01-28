<%@ WebHandler Language="C#" Class="LinkManInfo" %>

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

using System.Collections;
using System.Text;

public class LinkManInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{  
    public void ProcessRequest (HttpContext context) 
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "id";//要排序的字段，如果为空，默认为"linkmanname"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据
        LinkManModel LinkManM = new LinkManModel();
        string CustNam = context.Request.Form["CustNam"].ToString().Trim();
        LinkManM.LinkManName = context.Request.Form["LinkManName"].ToString().Trim();
        LinkManM.Handset = context.Request.Form["Handset"].ToString().Trim();
        LinkManM.Important = context.Request.Form["Important"].ToString().Trim();//(context.Request.Form["Important"].ToString().Trim() == "0") ? "0" : context.Request.Form["Important"].ToString();
        LinkManM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        LinkManM.LinkType = context.Request.Form["LinkType"].ToString().Trim() == "0" ? 0 : Convert.ToInt32(context.Request.Form["LinkType"].ToString());
        //DateTime DateBegin = context.Request.Form["BeginDate"].ToString().Trim() == "" ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(context.Request.Form["BeginDate"].ToString());//开始时间
        //DateTime DateEnd = context.Request.Form["EndDate"].ToString().Trim() == "" ? Convert.ToDateTime("9999-12-31") : Convert.ToDateTime(context.Request.Form["EndDate"].ToString());//结束时间            
        string DateBegin = context.Request.Form["BeginDate"].ToString().Trim();
        string DateEnd = context.Request.Form["EndDate"].ToString().Trim();
        LinkManM.WorkTel = context.Request.Form["WorkTel"].ToString().Trim();

        LinkManM.CanViewUser = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID
        
        string ord=orderBy+" "+order;
        int totalCount = 0;
        
        DataTable dt = LinkManBus.GetLinkManInfoBycondition(CustNam, LinkManM,DateBegin,DateEnd, pageIndex, pageCount, ord,ref totalCount);
        
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt == null)
            sb.Append("[{\"ID\":\"\"}]");
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
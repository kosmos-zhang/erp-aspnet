<%@ WebHandler Language="C#" Class="ContactInfo" %>

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

public class ContactInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "LinkDate";//要排序的字段，如果为空，默认为"linkmanname"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据
        ContactHistoryModel ContactM = new ContactHistoryModel();
        string CustName = context.Request.Form["CustName"].ToString().Trim();//客户名称        
        string CustLinkMan = context.Request.Form["CustLinkMan"].ToString().Trim();//被联络人
        string LinkDateBegin = context.Request.Form["LinkDateBegin"].ToString().Trim();// == "" ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(context.Request.Form["LinkDateBegin"].ToString());//联络开始时间
        string LinkDateEnd = context.Request.Form["LinkDateEnd"].ToString().Trim();// == "" ? Convert.ToDateTime("9999-12-31") : Convert.ToDateTime(context.Request.Form["LinkDateEnd"].ToString() + " 23:59:59.000");//联络结束时间
        string ReasonId = (context.Request.Form["ReasonId"]+"").Trim();//报表--联络原因
        ContactM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();        

        int Manager = Convert.ToInt32(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);//当前用户ID

        string ord = orderBy + " " + order;
        int totalCount = 0;

        DataTable dt = new DataTable();
        //查出表结果集放入Session
        if (ReasonId != "")//报表页面
        {
            dt = ContactHistoryBus.GetContactInfoBycondition(CustName, CustLinkMan, ContactM, LinkDateBegin, LinkDateEnd, ReasonId, pageIndex, pageCount, ord, ref totalCount);
            SessionUtil.Session.Add("CurrentListTable", ContactHistoryBus.GetContactInfoBycondition(CustName, CustLinkMan, ContactM, LinkDateBegin, LinkDateEnd, ReasonId, 1, 99999, ord, ref totalCount));
        }
        else
        {
            dt = ContactHistoryBus.GetContactInfoBycondition(CanUserID, CustName, CustLinkMan, ContactM, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref totalCount);
            SessionUtil.Session.Add("CurrentListTable", ContactHistoryBus.GetContactInfoBycondition(CanUserID,CustName, CustLinkMan, ContactM, LinkDateBegin, LinkDateEnd, 1, 99999, ord, ref totalCount));
        }
        
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
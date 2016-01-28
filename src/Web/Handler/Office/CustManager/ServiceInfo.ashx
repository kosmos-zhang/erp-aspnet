<%@ WebHandler Language="C#" Class="ServiceInfo" %>

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

public class ServiceInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "BeginDate";//要排序的字段，如果为空，默认为"linkmanname"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据
        CustServiceModel CustServiceM = new CustServiceModel();
        string CustName = context.Request.Form["CustName"].ToString().Trim(); ;//客户名称        
        CustServiceM.ServeType = Convert.ToInt32(context.Request.Form["ServeType"].ToString().Trim());//服务类型        
        CustServiceM.Fashion = Convert.ToInt32(context.Request.Form["Fashion"].ToString().Trim());//服务方式        
        string ServiceDateBegin = context.Request.Form["ServiceDateBegin"].ToString().Trim();// == "" ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(context.Request.Form["ServiceDateBegin"].ToString());//服务开始时间
        string ServiceDateEnd = context.Request.Form["ServiceDateEnd"].ToString().Trim();// == "" ? Convert.ToDateTime("9999-12-31") : Convert.ToDateTime(context.Request.Form["ServiceDateEnd"].ToString() + " 23:59:59.000");//服务结束时间            
        CustServiceM.Title = context.Request.Form["Title"].ToString().Trim();//客户服务主题
        string Executant = context.Request.Form["Executant"].ToString().Trim();//执行人
        string CustLinkMan = context.Request.Form["CustLinkMan"].ToString().Trim();//客户联系人       
        string ServerTypeW = (context.Request.Form["ServerTypeW"] + "").Trim();
        string ServerPerson = (context.Request.Form["ServerPerson"] + "").Trim(); 
        CustServiceM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

       string Manager = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID

        string ord = orderBy + " " + order;
        int totalCount = 0;

        //XElement dsXML = ConvertDataTableToXML(ServiceBus.GetServiceInfoBycondition(CustName, CustServiceM, ServiceDateBegin, ServiceDateEnd, Executant, CustLinkMan));
        DataTable dt = new DataTable();

        if (ServerTypeW != "")
        {
            dt = ServiceBus.GetServiceInfoServerType(ServerTypeW, CustName, CustServiceM, ServiceDateBegin, ServiceDateEnd, Executant, CustLinkMan, pageIndex, pageCount, ord, ref totalCount);
            SessionUtil.Session.Add("CurrentListTable", ServiceBus.GetServiceInfoServerType(ServerTypeW, CustName, CustServiceM, ServiceDateBegin, ServiceDateEnd, Executant, CustLinkMan, 1, 99999, ord, ref totalCount));
        }
        else if (ServerPerson != "")
        {
            dt = ServiceBus.GetServiceInfoByServerPerson(ServerPerson, CustName, CustServiceM, ServiceDateBegin, ServiceDateEnd, Executant, CustLinkMan, pageIndex, pageCount, ord, ref totalCount);
          SessionUtil.Session.Add("CurrentListTable",ServiceBus.GetServiceInfoByServerPerson(ServerPerson, CustName, CustServiceM, ServiceDateBegin, ServiceDateEnd, Executant, CustLinkMan, 1, 99999, ord, ref totalCount));
        }
        else
        {
            dt = ServiceBus.GetServiceInfoBycondition(Manager,CustName, CustServiceM, ServiceDateBegin, ServiceDateEnd, Executant, CustLinkMan, pageIndex, pageCount, ord, ref totalCount);
            SessionUtil.Session.Add("CurrentListTable", ServiceBus.GetServiceInfoBycondition(Manager,CustName, CustServiceM, ServiceDateBegin, ServiceDateEnd, Executant, CustLinkMan, 1, 99999, ord, ref totalCount));
        }
        
        
        
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
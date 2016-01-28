<%@ WebHandler Language="C#" Class="ComplainInfo" %>

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

public class ComplainInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest (HttpContext context) {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ComplainDate";//要排序的字段，如果为空，默认为"linkmanname"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据
        CustComplainModel CustComplainM = new CustComplainModel();
        string CustName = context.Request.Form["CustName"].ToString().Trim();//客户名称        
        CustComplainM.ComplainType = Convert.ToInt32(context.Request.Form["ComplainType"].ToString().Trim());//投诉类型
        CustComplainM.Critical = context.Request.Form["Critical"].ToString().Trim();//紧急程度
        string ComplainBegin = context.Request.Form["ComplainBegin"].ToString().Trim();// == "" ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(context.Request.Form["ComplainBegin"].ToString());//投诉开始时间
        string ComplainEnd = context.Request.Form["ComplainEnd"].ToString().Trim();// == "" ? Convert.ToDateTime("9999-12-31") : Convert.ToDateTime(context.Request.Form["ComplainEnd"].ToString() + " 23:59:59.000");//结束时间            
        CustComplainM.Title = context.Request.Form["Title"].ToString().Trim();//客户投诉主题
        string CustLinkMan = context.Request.Form["CustLinkMan"].ToString().Trim();//客户联系人
        string DestClerk = context.Request.Form["DestClerk"].ToString().Trim();//接待人
        string ComplainTypeW = (context.Request.Form["ComplainTypeW"] + "").Trim();
        string ComplainPerson = (context.Request.Form["ComplainPerson"] + "").Trim();
        string GroupBy = (context.Request.Form["GroupBy"] + "").Trim();
        string TimeIndex = (context.Request.Form["TimeIndex"] + "").Trim();
        string CustNameW = (context.Request.Form["CustNameW"] + "").Trim();
        
        CustComplainM.State = context.Request.Form["State"].ToString().Trim();//状态

        CustComplainM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Manager = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID

        string ord = orderBy + " " + order;
        int totalCount = 0;

        DataTable  dt  = new DataTable();

        if (ComplainTypeW != "")
        {
            dt = ComplainBus.GetComplainInfoComplainType(ComplainTypeW, CustName, CustComplainM, ComplainBegin, ComplainEnd, CustLinkMan, DestClerk, pageIndex, pageCount, ord, ref totalCount);
            SessionUtil.Session.Add("CurrentListTable", ComplainBus.GetComplainInfoComplainType(ComplainTypeW, CustName, CustComplainM, ComplainBegin, ComplainEnd, CustLinkMan, DestClerk, 1, 99999, ord, ref totalCount));
        }
        else if (ComplainPerson != "")
        {
            dt = ComplainBus.GetComplainInfoByComplainPerson(ComplainPerson, CustName, CustComplainM, ComplainBegin, ComplainEnd, CustLinkMan, DestClerk, pageIndex, pageCount, ord, ref totalCount);
            SessionUtil.Session.Add("CurrentListTable",ComplainBus.GetComplainInfoByComplainPerson(ComplainPerson, CustName, CustComplainM, ComplainBegin, ComplainEnd, CustLinkMan, DestClerk, 1, 99999, ord, ref totalCount) );
        }
        else if (TimeIndex != "")
        {
            dt = ComplainBus.GetComplainByTimeBehaviour(CustNameW, ComplainPerson, GroupBy, TimeIndex, CustComplainM.CompanyCD, ComplainBegin, ComplainEnd, pageIndex, pageCount, ord, ref totalCount);
            SessionUtil.Session.Add("CurrentListTable", ComplainBus.GetComplainByTimeBehaviour(CustNameW, ComplainPerson, GroupBy, TimeIndex, CustComplainM.CompanyCD, ComplainBegin, ComplainEnd, 1, 99999, ord, ref totalCount));
        }
        else {
            dt = ComplainBus.GetComplainInfoBycondition(Manager,CustName, CustComplainM, ComplainBegin, ComplainEnd, CustLinkMan, DestClerk, pageIndex, pageCount, ord, ref totalCount);
            SessionUtil.Session.Add("CurrentListTable", ComplainBus.GetComplainInfoBycondition(Manager,CustName, CustComplainM, ComplainBegin, ComplainEnd, CustLinkMan, DestClerk, 1, 99999, ord, ref totalCount));
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
<%@ WebHandler Language="C#" Class="CustTypeManageNewList" %>

using System;
using System.Web;
using System.Data;
using XBase.Business.Office.CustManager;
using XBase.Common;

public class CustTypeManageNewList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
            
    public void ProcessRequest (HttpContext context) {

        string action = context.Request.Form["action"].ToString();//排序

        switch (action)
        {
            case "ReportCustSeleType":
                ReportCustSeleType(context);
                break;
            case "ReportCustType":
                ReportCustType(context);
                break;
            case "ReportCustClass":
                ReportCustClass(context);
                break;
            case "ReportCustArea":
                ReportCustArea(context);
                break;
            case "ReportRelaGrade":
                ReportRelaGrade(context);
                break;
            case "ReportCreditGrade":
                ReportCreditGrade(context);
                break;
            case "ReportCustTime":
                ReportCustTime(context);
                break;
                
                
                
        }
    }

    //按
    private void ReportCustTime(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

        string ord = orderBy + " " + order;
        int totalCount = 0;

        string DateTimeID = context.Request.Form["DateTimeID"].ToString().Trim();
        string DateSele = context.Request.Form["DateSele"].ToString().Trim();
        string CustTypeManage = context.Request.Form["CustTypeManage"].ToString().Trim();
        string CustTypeSell = context.Request.Form["CustTypeSell"].ToString().Trim();
        string CustTypeTime = context.Request.Form["CustTypeTime"].ToString().Trim();

        string CustType = context.Request.Form["CustType"].ToString().Trim();
        string CustClass = context.Request.Form["CustClass"].ToString().Trim();
        string Area = context.Request.Form["Area"].ToString().Trim();
        string RelaGrade = context.Request.Form["RelaGrade"].ToString().Trim();
        string CreditGrade = context.Request.Form["CreditGrade"].ToString().Trim();
        string BeginDate = context.Request.Form["BeginDate"].ToString().Trim();
        string EndDate = context.Request.Form["EndDate"].ToString().Trim();

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        DataTable dt = CustInfoBus.GetCustListByTimeNewList(CompanyCD, DateSele, DateTimeID, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass,
                Area, RelaGrade, CreditGrade, BeginDate, EndDate, pageIndex, pageCount, ord, ref totalCount);

        if (dt != null)
        {
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
    }

    //按客户优质级别对比
    private void ReportCreditGrade(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

        string ord = orderBy + " " + order;
        int totalCount = 0;

        string CreditGradeID = context.Request.Form["CustTypeID"].ToString().Trim();
        string CustTypeManage = context.Request.Form["CustTypeManage"].ToString().Trim();
        string CustTypeSell = context.Request.Form["CustTypeSell"].ToString().Trim();
        string CustTypeTime = context.Request.Form["CustTypeTime"].ToString().Trim();

        string CustType = context.Request.Form["CustType"].ToString().Trim();
        string CustClass = context.Request.Form["CustClass"].ToString().Trim();
        string Area = context.Request.Form["Area"].ToString().Trim();
        string RelaGrade = context.Request.Form["RelaGrade"].ToString().Trim();
        string BeginDate = context.Request.Form["BeginDate"].ToString().Trim();
        string EndDate = context.Request.Form["EndDate"].ToString().Trim();

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        DataTable dt = CustInfoBus.GetCustListByCreditGradeNewList(CompanyCD, CreditGradeID, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass,
                    RelaGrade, Area, BeginDate, EndDate, pageIndex, pageCount, ord, ref totalCount);

        if (dt != null)
        {
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
    }

    //按客户关系等级对比
    private void ReportRelaGrade(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

        string ord = orderBy + " " + order;
        int totalCount = 0;

        string RelaGrade = context.Request.Form["CustTypeID"].ToString().Trim();
        string CustTypeManage = context.Request.Form["CustTypeManage"].ToString().Trim();
        string CustTypeSell = context.Request.Form["CustTypeSell"].ToString().Trim();
        string CustTypeTime = context.Request.Form["CustTypeTime"].ToString().Trim();

        string CustType = context.Request.Form["CustType"].ToString().Trim();
        string CustClass = context.Request.Form["CustClass"].ToString().Trim();
        string Area = context.Request.Form["Area"].ToString().Trim();
        string CreditGrade = context.Request.Form["CreditGrade"].ToString().Trim();
        string BeginDate = context.Request.Form["BeginDate"].ToString().Trim();
        string EndDate = context.Request.Form["EndDate"].ToString().Trim();

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        DataTable dt = CustInfoBus.GetCustListByRelaGradeNewList(CompanyCD, RelaGrade, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass,
            Area, CreditGrade, BeginDate, EndDate, pageIndex, pageCount, ord, ref totalCount);

        if (dt != null)
        {
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
    }

    //按客户所在区域对比
    private void ReportCustArea(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

        string ord = orderBy + " " + order;
        int totalCount = 0;

        string AreaID = context.Request.Form["CustTypeID"].ToString().Trim();
        string CustTypeManage = context.Request.Form["CustTypeManage"].ToString().Trim();
        string CustTypeSell = context.Request.Form["CustTypeSell"].ToString().Trim();
        string CustTypeTime = context.Request.Form["CustTypeTime"].ToString().Trim();

        string CustType = context.Request.Form["CustType"].ToString().Trim();
        string CustClass = context.Request.Form["CustClass"].ToString().Trim();
        string RelaGrade = context.Request.Form["RelaGrade"].ToString().Trim();
        string CreditGrade = context.Request.Form["CreditGrade"].ToString().Trim();
        string BeginDate = context.Request.Form["BeginDate"].ToString().Trim();
        string EndDate = context.Request.Form["EndDate"].ToString().Trim();

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        DataTable dt = CustInfoBus.GetCustListByAreaNewList(CompanyCD, AreaID, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass, RelaGrade, CreditGrade, BeginDate,
            EndDate, pageIndex, pageCount, ord, ref totalCount);

        if (dt != null)
        {
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
    }

    //按客户细分对比
    private void ReportCustClass(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

        string ord = orderBy + " " + order;
        int totalCount = 0;

        string CustTypeID = context.Request.Form["CustTypeID"].ToString().Trim();
        string CustTypeManage = context.Request.Form["CustTypeManage"].ToString().Trim();
        string CustTypeSell = context.Request.Form["CustTypeSell"].ToString().Trim();
        string CustTypeTime = context.Request.Form["CustTypeTime"].ToString().Trim();

        string CustType = context.Request.Form["CustType"].ToString().Trim();
        string Area = context.Request.Form["Area"].ToString().Trim();
        string RelaGrade = context.Request.Form["RelaGrade"].ToString().Trim();
        string CreditGrade = context.Request.Form["CreditGrade"].ToString().Trim();
        string BeginDate = context.Request.Form["BeginDate"].ToString().Trim();
        string EndDate = context.Request.Form["EndDate"].ToString().Trim();

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        DataTable dt = CustInfoBus.GetCustListByClassNewList(CompanyCD, CustTypeID, CustTypeManage, CustTypeSell, CustTypeTime, CustType, Area, RelaGrade, CreditGrade, BeginDate,
            EndDate, pageIndex, pageCount, ord, ref totalCount);

        if (dt != null)
        {
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
    }
    
    //按客户类型对比
    private void ReportCustType(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

        string ord = orderBy + " " + order;
        int totalCount = 0;

        string CustTypeID = context.Request.Form["CustTypeID"].ToString().Trim();
        string CustTypeManage = context.Request.Form["CustTypeManage"].ToString().Trim();
        string CustTypeSell = context.Request.Form["CustTypeSell"].ToString().Trim();
        string CustTypeTime = context.Request.Form["CustTypeTime"].ToString().Trim();

        string CustClass = context.Request.Form["CustClass"].ToString().Trim();
        string Area = context.Request.Form["Area"].ToString().Trim();
        string RelaGrade = context.Request.Form["RelaGrade"].ToString().Trim();
        string CreditGrade = context.Request.Form["CreditGrade"].ToString().Trim();
        string BeginDate = context.Request.Form["BeginDate"].ToString().Trim();
        string EndDate = context.Request.Form["EndDate"].ToString().Trim();

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        
        DataTable dt = CustInfoBus.GetCustListByTypeNewList(CompanyCD, CustTypeID, CustTypeManage, CustTypeSell, CustTypeTime, CustClass, Area, RelaGrade, CreditGrade, BeginDate,
            EndDate, pageIndex, pageCount, ord, ref totalCount);

        if (dt != null)
        {
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
    }
    
    //按客户分类对比
    private void ReportCustSeleType(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

        string ord = orderBy + " " + order;
        int totalCount = 0;
        
        string CustSeleTypeID = context.Request.Form["CustSeleTypeID"].ToString().Trim();
        string CustSeleType = context.Request.Form["CustSeleType"].ToString().Trim();
        string CustType = context.Request.Form["CustType"].ToString().Trim();
        string CustClass = context.Request.Form["CustClass"].ToString().Trim();
        string Area = context.Request.Form["Area"].ToString().Trim();
        string RelaGrade = context.Request.Form["RelaGrade"].ToString().Trim();
        string CreditGrade = context.Request.Form["CreditGrade"].ToString().Trim();
        string BeginDate = context.Request.Form["BeginDate"].ToString().Trim();
        string EndDate = context.Request.Form["EndDate"].ToString().Trim();

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        DataTable dt = CustInfoBus.GetCustListByTypeManageNewList(CompanyCD,CustSeleTypeID, CustSeleType, CustType, CustClass, Area, RelaGrade, CreditGrade, BeginDate,
            EndDate, pageIndex, pageCount, ord, ref totalCount);
        
        if(dt != null)
        {
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
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
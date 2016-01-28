<%@ WebHandler Language="C#" Class="HumanReport" %>

using System;
using System.Web;
using XBase.Common;
using System.Data;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
public class HumanReport : IHttpHandler , System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string action = string.Empty;
        if (context.Request.Params["Action"] != null)
        {
            action = context.Request.Params["Action"].ToString();
        } 

        string page = action.Split('_')[0];
        string acTemp = action.Split('_')[1];

        if (page == "EmployeeExaminationReport")//人员状况明细月报
        {
            if (acTemp == "Select")
            {
                EmployeeExaminationSelect(context);
            } 
        }
        else if (page == "SalaryStandardReport")// 工资标准明细
        { 
            if (acTemp == "Select")
            {
                SalaryStandardSelect(context);
            }
        }
        else if (page == "SalarySummeryReport")// 部门工资明细统计
        { 
            if (acTemp == "Select")
            {
                SalarySummerySelect(context); 
            }
        }  
        else if (page == "DeptPieceReportPrint")// 计件工资走势分析明细  
        { 
            if (acTemp == "Select")
            {
                DeptPieceReportSelect(context);
            }
        }
        else if (page == "DeptPieceDetails")// 计件工资明细   DeptTimeDetails
        { 
            if (acTemp == "Select")
            {
                DeptPieceDetails(context);
            }
        }
        else if (page == "DeptTimeDetails")// 计时工资明细   
        {
            if (acTemp == "Select")
            {
                DeptTimeDetails(context);
            }
        }
        else if (page == "DeptRealMoneyReportPrint")// 工资月份走势分析明细  
        { 
            if (acTemp == "Select")
            {
                DeptRealMoneyReportPrintSelect(context);
            }
        }
        else if (page == "DeptTimeReportPrint")// 计时工资走势分析明细
        { 
            if (acTemp == "Select")
            {
                DeptTimeReportPrintSelect(context);
            }
        }
        else if (page == "PerformanceDetailsByLTPrint")// 绩效考核按等级分布明细  
        {
            if (acTemp == "Select")
            {
                PerformanceDetailsByLTPrintSelect(context);
            }
        }
        else if (page == "PerformanceDetailsByLAPrint")// 绩效考核按建议分布明细  
        {
            if (acTemp == "Select")
            {
                PerformanceDetailsByLAPrintSelect(context);
            } 
        }
        else if (page == "EmployeeTestCountPrint")// 员工考试次数分析明细  
        {
            if (acTemp == "Select")
            {
                EmployeeTestCountPrintSelect(context);
            }
        }
        else if (page == "TrainningCountAnalysePrint")// 员工培训次数分析明细  
        {
            if (acTemp == "Select")
            {
                TrainningCountAnalysePrintSelect(context);
            }
        }
        else if (page == "EmployeeConditionByMonthPrint")// 员工状况按月走势明细  
        {
            if (acTemp == "Select")
            { 
                string type=context.Request.Form["type"] == null ? "" : context.Request.Form["type"].ToString();
                if (type.Equals("1")) //招聘人数明细
                {
                    EmployeeConditionByZhaoPing(context);
                }
                else if (type.Equals("2")) //面试人数明细
                {
                    EmployeeConditionByMianShi(context);
                }
                else if (type.Equals("3")) //报到人数明细
                {
               EmployeeConditionByBaoDao(context);
                }
                else if (type.Equals("4")) //迟到次数明细
                {
                  EmployeeConditionByChiDao(context);
                }
                else if (type.Equals("5")) //早退次数明细
                {
                   EmployeeConditionByZaoTui(context);
                }
                else if (type.Equals("6")) //矿工人数明细
                {
                EmployeeConditionByKuangGong(context);
                }
                else if (type.Equals("7")) //请假人数明细
                {
                EmployeeConditionByQingjia(context);
                }
                else if (type.Equals("8")) //迁出人数明细
                {
                 EmployeeConditionByQianchu(context);
                }
                else if (type.Equals("9")) //迁入人数明细
                {
                 EmployeeConditionByQianRu(context);
                }
                else if (type.Equals("10")) //离职人数明细
                {
                   EmployeeConditionByLiZhi(context);
                }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("{");
                    sb.Append("totalCount:");
                    sb.Append("0");
                    sb.Append(",data:");
                    sb.Append("[{\"DeptID\":\"\"}]");
                    sb.Append("}"); 
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(sb.ToString());
                    context.Response.End();
                }
                
                
          
            }
        } 
        
 
    }


    public   void DeptTimeDetails(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "EmployeeID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据       
        string DeptID = context.Request.Form["DeptID"].ToString();
        string year = context.Request.Form["year"].ToString();

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        SalaryStandardModel searchModel = new SalaryStandardModel();
        //设置查询条件
        //岗位

        if (DeptID != "0")//部门
            searchModel.QuarterID = DeptID;
        if (year != "0")//年度
            searchModel.UnitPrice = year;
        searchModel.CompanyCD = CompanyCD;
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtData = HumanReportBus.DeptTimeReportPrintSelect(searchModel, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
        {
            sb.Append("[{\"EmployeeID\":\"\"}]");
        }
        else
        {
            //DataTable dtTemp = CommonYearSelectControl.GetNewDataTable(dtData, "WorkMoney>'" + 0.00 + "'");
            sb.Append(JsonClass.DataTable2Json(dtData));
        }
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    public void DeptPieceDetails(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "EmployeeID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据       
        string DeptID = context.Request.Form["DeptID"].ToString();
        string year = context.Request.Form["year"].ToString();

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        SalaryStandardModel searchModel = new SalaryStandardModel();
        //设置查询条件
        //岗位

        if (DeptID != "0")//部门
            searchModel.QuarterID = DeptID;
        if (year != "0")//年度
            searchModel.UnitPrice = year;
        searchModel.CompanyCD = CompanyCD;
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtData = HumanReportBus.DeptPieceReportSelect(searchModel, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
        {
            sb.Append("[{\"EmployeeID\":\"\"}]");
        }
        else
        {
            //DataTable dtTemp = CommonYearSelectControl.GetNewDataTable(dtData, "WorkMoney>'" + 0.00 + "'");
            sb.Append(JsonClass.DataTable2Json(dtData));
        }
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    public void EmployeeConditionByLiZhi(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "DeptID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数



        //获取数据       
        string deptid = context.Request.Form["deptid"] == null ? string.Empty : context.Request.Form["deptid"].ToString();
        string year = context.Request.Form["year"] == null ? string.Empty : context.Request.Form["year"].ToString();
        string month = context.Request.Form["month"] == null ? string.Empty : context.Request.Form["month"].ToString();
        string day = "";
        day = CommonYearSelectControl.GetMonthLastDay(month, year);

        string monthStartDate = year + "-" + month + "-" + "01";
        string monthEndDate = year + "-" + month + "-" + day;
        //获取数据    
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        string CompanyCD = userInfo.CompanyCD;
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtData = HumanReportBus.EmployeeConditionByLiZhi(CompanyCD, deptid, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"DeptID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    public void EmployeeConditionByQianRu(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "DeptID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数



        //获取数据       
        string deptid = context.Request.Form["deptid"] == null ? string.Empty : context.Request.Form["deptid"].ToString();
        string year = context.Request.Form["year"] == null ? string.Empty : context.Request.Form["year"].ToString();
        string month = context.Request.Form["month"] == null ? string.Empty : context.Request.Form["month"].ToString();
        string day = "";
        day = CommonYearSelectControl.GetMonthLastDay(month, year);

        string monthStartDate = year + "-" + month + "-" + "01";
        string monthEndDate = year + "-" + month + "-" + day;
        //获取数据    
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        string CompanyCD = userInfo.CompanyCD;
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtData = HumanReportBus.EmployeeConditionByQianRu(CompanyCD, deptid, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"DeptID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    public void EmployeeConditionByQianchu(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "DeptID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数



        //获取数据       
        string deptid = context.Request.Form["deptid"] == null ? string.Empty : context.Request.Form["deptid"].ToString();
        string year = context.Request.Form["year"] == null ? string.Empty : context.Request.Form["year"].ToString();
        string month = context.Request.Form["month"] == null ? string.Empty : context.Request.Form["month"].ToString();
        string day = "";
        day = CommonYearSelectControl.GetMonthLastDay(month, year);

        string monthStartDate = year + "-" + month + "-" + "01";
        string monthEndDate = year + "-" + month + "-" + day;
        //获取数据    
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        string CompanyCD = userInfo.CompanyCD;
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtData = HumanReportBus.EmployeeConditionByQianchu(CompanyCD, deptid, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"DeptID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    public void EmployeeConditionByQingjia(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "DeptID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数



        //获取数据       
        string deptid = context.Request.Form["deptid"] == null ? string.Empty : context.Request.Form["deptid"].ToString();
        string year = context.Request.Form["year"] == null ? string.Empty : context.Request.Form["year"].ToString();
        string month = context.Request.Form["month"] == null ? string.Empty : context.Request.Form["month"].ToString();
        string day = "";
        day = CommonYearSelectControl.GetMonthLastDay(month, year);

        string monthStartDate = year + "-" + month + "-" + "01";
        string monthEndDate = year + "-" + month + "-" + day;
        //获取数据    
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        string CompanyCD = userInfo.CompanyCD;
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtData = HumanReportBus.EmployeeConditionByQingjia(CompanyCD, deptid, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"DeptID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    public void EmployeeConditionByKuangGong(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "DeptID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数



        //获取数据       
        string deptid = context.Request.Form["deptid"] == null ? string.Empty : context.Request.Form["deptid"].ToString();
        string year = context.Request.Form["year"] == null ? string.Empty : context.Request.Form["year"].ToString();
        string month = context.Request.Form["month"] == null ? string.Empty : context.Request.Form["month"].ToString();
        string day = "";
        day = CommonYearSelectControl.GetMonthLastDay(month, year);

        string monthStartDate = year + "-" + month + "-" + "01";
        string monthEndDate = year + "-" + month + "-" + day;
        //获取数据    
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        string CompanyCD = userInfo.CompanyCD;
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtData = HumanReportBus.EmployeeConditionByKuangGong(CompanyCD, deptid, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"DeptID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    public void EmployeeConditionByZaoTui(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "DeptID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数



        //获取数据       
        string deptid = context.Request.Form["deptid"] == null ? string.Empty : context.Request.Form["deptid"].ToString();
        string year = context.Request.Form["year"] == null ? string.Empty : context.Request.Form["year"].ToString();
        string month = context.Request.Form["month"] == null ? string.Empty : context.Request.Form["month"].ToString();
        string day = "";
        day = CommonYearSelectControl.GetMonthLastDay(month, year);

        string monthStartDate = year + "-" + month + "-" + "01";
        string monthEndDate = year + "-" + month + "-" + day;
        //获取数据    
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        string CompanyCD = userInfo.CompanyCD;
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtData = HumanReportBus.EmployeeConditionByZaoTui(CompanyCD, deptid, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"DeptID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    public void EmployeeConditionByChiDao(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "DeptID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数



        //获取数据       
        string deptid = context.Request.Form["deptid"] == null ? string.Empty : context.Request.Form["deptid"].ToString();
        string year = context.Request.Form["year"] == null ? string.Empty : context.Request.Form["year"].ToString();
        string month = context.Request.Form["month"] == null ? string.Empty : context.Request.Form["month"].ToString();
        string day = "";
        day = CommonYearSelectControl.GetMonthLastDay(month, year);

        string monthStartDate = year + "-" + month + "-" + "01";
        string monthEndDate = year + "-" + month + "-" + day;
        //获取数据    
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        string CompanyCD = userInfo.CompanyCD;
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtData = HumanReportBus.EmployeeConditionByChiDao(CompanyCD, deptid, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"DeptID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    public void EmployeeConditionByBaoDao(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "DeptID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数



        //获取数据       
        string deptid = context.Request.Form["deptid"] == null ? string.Empty : context.Request.Form["deptid"].ToString();
        string year = context.Request.Form["year"] == null ? string.Empty : context.Request.Form["year"].ToString();
        string month = context.Request.Form["month"] == null ? string.Empty : context.Request.Form["month"].ToString();
        string day = "";
        day = CommonYearSelectControl.GetMonthLastDay(month, year);

        string monthStartDate = year + "-" + month + "-" + "01";
        string monthEndDate = year + "-" + month + "-" + day;
        //获取数据    
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        string CompanyCD = userInfo.CompanyCD;
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtData = HumanReportBus.EmployeeConditionByBaoDao(CompanyCD, deptid, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"DeptID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    public void EmployeeConditionByMianShi(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "DeptID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数



        //获取数据       
        string deptid = context.Request.Form["deptid"] == null ? string.Empty : context.Request.Form["deptid"].ToString();
        string year = context.Request.Form["year"] == null ? string.Empty : context.Request.Form["year"].ToString();
        string month = context.Request.Form["month"] == null ? string.Empty : context.Request.Form["month"].ToString();
        string day = "";
        day = CommonYearSelectControl.GetMonthLastDay(month, year);

        string monthStartDate = year + "-" + month + "-" + "01";
        string monthEndDate = year + "-" + month + "-" + day;
        //获取数据    
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        string CompanyCD = userInfo.CompanyCD;
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtData = HumanReportBus.EmployeeConditionByMianShi(CompanyCD, deptid, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"DeptID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    public void EmployeeConditionByZhaoPing(HttpContext   context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "DeptID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数


      
        //获取数据       
        string deptid = context.Request.Form["deptid"] == null ? string.Empty : context.Request.Form["deptid"].ToString();
        string year = context.Request.Form["year"] == null ? string.Empty : context.Request.Form["year"].ToString();
        string month = context.Request.Form["month"] == null ? string.Empty : context.Request.Form["month"].ToString();
        string day = "";
        day = CommonYearSelectControl.GetMonthLastDay(month, year);

        string monthStartDate = year + "-" + month + "-" + "01";
        string monthEndDate = year + "-" + month + "-" + day; 
        //获取数据    
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        string CompanyCD = userInfo.CompanyCD;
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtData = HumanReportBus.EmployeeConditionByZhaoPing(CompanyCD, deptid, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref totalCount);
 
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"DeptID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    
    
    public void TrainningCountAnalysePrintSelect(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "DeptID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数



        //获取数据       
        string deptid = context.Request.Form["deptid"] == null ? string.Empty : context.Request.Form["deptid"].ToString();
        string SearchEmployee = context.Request.Form["EmployeeID"] == null ? string.Empty : context.Request.Form["EmployeeID"].ToString();
        string StartDate = context.Request.Form["DateBegin"] == null ? string.Empty : context.Request.Form["DateBegin"].ToString();
        string StartToDate = context.Request.Form["DateEnd"] == null ? string.Empty : context.Request.Form["DateEnd"].ToString();
        string EndDate = context.Request.Form["DateB"] == null ? string.Empty : context.Request.Form["DateB"].ToString();
        string EndToDate = context.Request.Form["DateE"] == null ? string.Empty : context.Request.Form["DateE"].ToString();



        //获取数据    
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
    
       string     CompanyCD = userInfo.CompanyCD;
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;


        DataTable dtData = HumanReportBus.TrainningCountAnalysePrintSelect(CompanyCD, deptid, SearchEmployee, StartDate, StartToDate, EndDate, EndToDate, pageIndex, pageCount, ord, ref totalCount);



        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"DeptID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    public void EmployeeTestCountPrintSelect(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "DeptID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

      
        
        
        //获取数据       
        string deptid = context.Request.Form["deptid"] == null ? string.Empty : context.Request.Form["deptid"].ToString();
        string SearchEmployee = context.Request.Form["SearchEmployee"] == null ? string.Empty : context.Request.Form["SearchEmployee"].ToString();
        string StartDate = context.Request.Form["StartDate"] == null ? string.Empty : context.Request.Form["StartDate"].ToString();
        string StartToDate = context.Request.Form["StartToDate"] == null ? string.Empty : context.Request.Form["StartToDate"].ToString();
        string EndDate = context.Request.Form["EndDate"] == null ? string.Empty : context.Request.Form["EndDate"].ToString();
        string EndToDate = context.Request.Form["EndToDate"] == null ? string.Empty : context.Request.Form["EndToDate"].ToString(); 



        //获取数据    
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        EmployeeTestSearchModel searchModel = new EmployeeTestSearchModel();
        searchModel.Addr = deptid;
        searchModel.StatusName = SearchEmployee;
        searchModel.StartDate = StartDate;
        searchModel.StartToDate =StartToDate ;
        searchModel.EndDate = EndDate ;
        searchModel.EndToDate = EndToDate ;
        searchModel.CompanyCD = userInfo.CompanyCD;
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;


        DataTable dtData = HumanReportBus.EmployeeTestCountPrintSelect(searchModel, pageIndex, pageCount, ord, ref totalCount);
        
        

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"DeptID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    public void PerformanceDetailsByLAPrintSelect(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "EmployeeID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据       
        string deptid = context.Request.Form["deptid"] == null ? string.Empty : context.Request.Form["deptid"].ToString();
        string TaskFlag = context.Request.Form["TaskFlag"] == null ? string.Empty : context.Request.Form["TaskFlag"].ToString();
        string TaskNo = context.Request.Form["TaskNo"] == null ? string.Empty : context.Request.Form["TaskNo"].ToString();
        string PerType = context.Request.Form["PerType"] == null ? string.Empty : context.Request.Form["PerType"].ToString();
        string TaskNum = context.Request.Form["TaskNum"] == null ? string.Empty : context.Request.Form["TaskNum"].ToString();
        string LevelType = context.Request.Form["LevelType"] == null ? string.Empty : context.Request.Form["LevelType"].ToString();
        string TaskYear = context.Request.Form["TaskYear"] == null ? string.Empty : context.Request.Form["TaskYear"].ToString();

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        if (deptid != "0" && deptid != string.Empty)// 
        {
            searchModel.SummaryDate = deptid;
        }
        if (TaskFlag != "0" && TaskFlag != string.Empty)
            searchModel.TaskFlag = TaskFlag;//考核期间类型

        if (TaskNo != "0" && TaskNo != string.Empty)
            searchModel.TaskNo = TaskNo; //考核任务编号


        if (PerType != "0" && PerType != string.Empty)
            searchModel.CompleteDate = PerType;//考核类型

        if (TaskFlag != "4" && TaskFlag != "5" && TaskFlag != string.Empty)
        {

            if (TaskNum != "0" && TaskNum != string.Empty)
                searchModel.TaskNum = TaskNum;//考核期间 
        }
        if (LevelType != "0" && LevelType != string.Empty)
            searchModel.Title = LevelType;//考核建议

        if (TaskYear != "0" && TaskYear != string.Empty)
            searchModel.TaskDate = TaskYear;//

        searchModel.CompanyCD = CompanyCD;
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;


        DataTable dtData = HumanReportBus.PerformanceDetailsByLAPrintSelect(searchModel, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"EmployeeID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

    public void PerformanceDetailsByLTPrintSelect(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "EmployeeID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据       
        string deptid = context.Request.Form["deptid"] == null ? string.Empty : context.Request.Form["deptid"].ToString ();
        string TaskFlag = context.Request.Form["TaskFlag"] == null ? string.Empty : context.Request.Form["TaskFlag"].ToString();
        string TaskNo = context.Request.Form["TaskNo"] == null ? string.Empty : context.Request.Form["TaskNo"].ToString();
        string PerType = context.Request.Form["PerType"] == null ? string.Empty : context.Request.Form["PerType"].ToString();
        string TaskNum = context.Request.Form["TaskNum"] == null ? string.Empty : context.Request.Form["TaskNum"].ToString();
        string LevelType = context.Request.Form["LevelType"] == null ? string.Empty : context.Request.Form["LevelType"].ToString();
        string TaskYear = context.Request.Form["TaskYear"] == null ? string.Empty : context.Request.Form["TaskYear"].ToString();

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        if (deptid != "0" && deptid !=string .Empty  )// 
        {
            searchModel.SummaryDate = deptid;
        }
        if (TaskFlag != "0" && TaskFlag != string.Empty)
        searchModel.TaskFlag = TaskFlag;//考核期间类型

        if (TaskNo != "0" && TaskNo != string.Empty)
            searchModel.TaskNo = TaskNo; //考核任务编号


        if (PerType != "0" && PerType != string.Empty)
            searchModel.CompleteDate = PerType;//考核类型

        if (TaskFlag != "4" && TaskFlag != "5" && TaskFlag != string.Empty)
        {

            if (TaskNum != "0" && TaskNum != string.Empty)
                searchModel.TaskNum = TaskNum;//考核期间 
        }
        if (LevelType != "0" && LevelType != string.Empty)
            searchModel.Summaryer = LevelType;//考核等级

        if (TaskYear != "0" && TaskYear != string.Empty)
            searchModel.TaskDate = TaskYear;//考核建议

        searchModel.CompanyCD = CompanyCD ;
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;


        DataTable dtData = HumanReportBus.PerformanceDetailsByLTPrintSelect(searchModel, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"EmployeeID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    
    public void DeptTimeReportPrintSelect(HttpContext context)
    { 

        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "EmployeeID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据       
        string DeptID = context.Request.Form["DeptID"].ToString();
        string Month = context.Request.Form["Month"].ToString();
        string year = context.Request.Form["year"].ToString();

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        SalaryStandardModel searchModel = new SalaryStandardModel();
        //设置查询条件
        //岗位

        if (DeptID != "0")//部门
            searchModel.QuarterID = DeptID;
        if (Month != "0")//起始月份
            searchModel.AdminLevel = Month;
        if (year != "0")//年度
            searchModel.UnitPrice = year;
        searchModel.CompanyCD = CompanyCD;
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtData = HumanReportBus.DeptTimeReportPrintSelect(searchModel, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"EmployeeID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();

    }
  
    public void DeptRealMoneyReportPrintSelect(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "EmployeeID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据       
        string DeptID = context.Request.Form["DeptID"].ToString();
        string Month = context.Request.Form["Month"].ToString();
        string year = context.Request.Form["year"].ToString();

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        SalaryStandardModel searchModel = new SalaryStandardModel();
        //设置查询条件
        //岗位

        if (DeptID != "0")//部门
            searchModel.QuarterID = DeptID;
        if (Month != "0")//起始月份
            searchModel.AdminLevel = Month;
        if (year != "0")//年度
            searchModel.UnitPrice = year;
        searchModel.CompanyCD = CompanyCD;
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtData = HumanReportBus.DeptRealMoneyReportPrintSelect(searchModel, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"EmployeeID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    public void DeptPieceReportSelect(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "EmployeeID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据       
        string DeptID = context.Request.Form["DeptID"].ToString();
        string Month = context.Request.Form["Month"].ToString();
        string year = context.Request.Form["year"].ToString();

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        SalaryStandardModel searchModel = new SalaryStandardModel();
        //设置查询条件
        //岗位

        if (DeptID != "0")//部门
            searchModel.QuarterID = DeptID;
        if (Month != "0")//起始月份
            searchModel.AdminLevel = Month; 
        if (year != "0")//年度
            searchModel.UnitPrice = year;
        searchModel.CompanyCD = CompanyCD;
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtData = HumanReportBus.DeptPieceReportSelect(searchModel, pageIndex, pageCount, ord, ref totalCount);
   
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
        {
            sb.Append("[{\"EmployeeID\":\"\"}]");
        }
        else
        {
            //DataTable dtTemp = CommonYearSelectControl.GetNewDataTable(dtData, "WorkMoney>'" + 0.00 + "'");
            sb.Append(JsonClass.DataTable2Json(dtData));
        }
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    public void SalarySummerySelect(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "Remark";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据       
        string DeptID = context.Request.Form["DeptID"].ToString(); 
        string startMonth = context.Request.Form["startMonth"].ToString();
        string endMonth = context.Request.Form["endMonth"].ToString();
        string year = context.Request.Form["year"].ToString(); 
        
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        SalaryStandardModel searchModel = new SalaryStandardModel();
        //设置查询条件
        //岗位

        if (DeptID != "0")//部门
            searchModel.QuarterID = DeptID;
        if (startMonth != "0")//起始月份
            searchModel.AdminLevel = startMonth;
        if (endMonth != "0")//结束月份
            searchModel.AdminLevelName = endMonth;
        if (year != "0")//年度
            searchModel.UnitPrice = year;
        searchModel.CompanyCD = CompanyCD;
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //查询数据
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dtNewTable = HumanReportBus.SalarySummerySelect(searchModel, pageIndex, pageCount, ord, ref totalCount);
        DataTable dtData = new DataTable();
        dtData.Columns.Add("Remark");
        dtData.Columns.Add("itemNo");
        dtData.Columns.Add("CompanyCD");
        dtData.Columns.Add("UnitPrice");
        for (int i = 0; i < dtNewTable.Rows.Count; i++)
        {
            DataRow newRow = dtData.NewRow();
            newRow["Remark"] = getDeptName(dtNewTable.Rows[i]["Remark"] == null ? "" : dtNewTable.Rows[i]["Remark"].ToString());
            newRow["itemNo"] = dtNewTable.Rows[i]["itemNo"] == null ? "" : dtNewTable.Rows[i]["itemNo"].ToString();
            newRow["CompanyCD"] = dtNewTable.Rows[i]["CompanyCD"] == null ? "" : dtNewTable.Rows[i]["CompanyCD"].ToString();
            newRow["UnitPrice"] = dtNewTable.Rows[i]["UnitPrice"] == null ? "" : dtNewTable.Rows[i]["UnitPrice"].ToString();
            dtData.Rows.Add(newRow);
        }



        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dtData.Rows.Count == 0)
            sb.Append("[{\"Remark\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtData));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }


    protected string getDeptName(string DeptId)
    {
        return SalaryStandardBus.GetNameByDeptID(DeptId);
    }
    
    public void SalaryStandardSelect(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "QuarterID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据       
        string quterID = context.Request.Form["quterID"].ToString();
        string QuaterAdmin = context.Request.Form["QuaterAdmin"].ToString(); 
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        SalaryStandardModel searchModel = new SalaryStandardModel();
        if (quterID != "0")
            searchModel.QuarterID = quterID;
        //岗位职等
        if (QuaterAdmin != "0")
            searchModel.AdminLevel = QuaterAdmin;
        //启用状态·
        searchModel.UsedStatus = "1";
        searchModel.CompanyCD = CompanyCD;
 

        string ord = orderBy + " " + order;
        int totalCount = 0;

        DataTable dt = HumanReportBus.SalaryStandardSelect(searchModel, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"QuarterID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

 
    public void EmployeeExaminationSelect(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据       
        string DeptID = context.Request.Form["DeptID"].ToString();
        string year = context.Request.Form["year"].ToString();
        string month = context.Request.Form["month"].ToString();
        string day = "";
        day = CommonYearSelectControl.GetMonthLastDay(month, year);

        string monthStartDate = year + "-" + month + "-" + "01";
        string monthEndDate = year + "-" + month + "-" + day;
        
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        string ord = orderBy + " " + order;
        int totalCount = 0;

        DataTable dt = HumanReportBus.EmployeeExamination2Select(CompanyCD, DeptID, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
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
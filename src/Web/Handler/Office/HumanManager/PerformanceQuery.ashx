<%@ WebHandler Language="C#" Class="PerformanceQuery" %>
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using XBase.Common;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;

public class PerformanceQuery : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取请求动作
        string action = context.Request.Params["Action"];
        if ("SearchInfo".Equals(action))
        {
            //执行查询
            SearchData(context);
        }
        else if ("SearchPerTypeInfo".Equals(action))
        {
            //执行查询
            SearchPerTypeInfo(context);
        }
        else if ("SearchScoreInfo".Equals(action))
        {
            //执行查询
            SearchScoreInfo(context);
        }
        else if ("SearchStaticsInfo".Equals(action))
        {
            //执行查询
            SearchStaticsInfo(context);
        }
        else if ("SearchDetailsInfo".Equals(action))
        {
            //执行查询
            SearchDetailsInfo(context);
        }
        else if ("SearchDetailsInfoByLT".Equals(action))
        {
            //执行查询
            SearchDetailsInfoByLT(context);
        }
                    
    }
    private void SearchStaticsInfo(HttpContext context)
    {
        //从请求中获取排序列
        string orderString = context.Request.QueryString["OrderBy"];

        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "deptID";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        int pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(context.Request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;
        //获取数据
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        if (context.Request.QueryString["TaskFlag"] != "0")
            searchModel.TaskFlag = context.Request.QueryString["TaskFlag"];//考核期间类型
        if (!string.IsNullOrEmpty(context.Request.QueryString["TaskNo"]))
            searchModel.TaskNo = context.Request.QueryString["TaskNo"];//考核任务编号
        if (context.Request.QueryString["PerTypeID"] != "0")
            searchModel.CompleteDate = context.Request.QueryString["PerTypeID"];//考核类型
        if (context.Request.QueryString["TaskFlag"] != "4" && context.Request.QueryString["TaskFlag"] != "5")
        {
            if (context.Request.QueryString["TaskNum"] != "0")
                searchModel.TaskNum = context.Request.QueryString["TaskNum"];//考核期间 
        }
        if (context.Request.QueryString["LevelType"] != "0")
            searchModel.Summaryer = context.Request.QueryString["LevelType"];//考核等级
        if (context.Request.QueryString["AdviceType"] != "0")
            searchModel.Title = context.Request.QueryString["AdviceType"];//考核建议

        if (context.Request.QueryString["TaskDate"] != "0")
            searchModel.TaskDate  = context.Request.QueryString["TaskDate"];//考核建议
        if (!string.IsNullOrEmpty(context.Request.QueryString["DeptID"]))
        {    searchModel.EditFlag = context.Request.QueryString["DeptID"];//部门
        }
        
        searchModel.CompanyCD = userInfo.CompanyCD;
        
        //查询数据
        DataTable dtData = PerformanceQueryBus.SearchStaticsInfo(searchModel);
        //定义转化需要的变量
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new  PerformanceSummaryModel ()
             {
                 Title = x.Element("Title") == null ? "" : x.Element("Title").Value,//考核任务名称
                 LevelType = x.Element("LevelType") == null ? "" : x.Element("LevelType").Value,//
                 AdviceType = x.Element("AdviceType") == null ? "" : x.Element("AdviceType").Value,//
                 TaskFlag = x.Element("TaskFlag") == null ? "" : x.Element("TaskFlag").Value,//ID
                 TaskNum = x.Element("TaskNum") == null ? "" : x.Element("TaskNum").Value,//
                 CountPerson = x.Element("CountPerson") == null ? "" : x.Element("CountPerson").Value,//
                 DeptName = x.Element("DeptName") == null ? "" : x.Element("DeptName").Value//
             })
                       :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PerformanceSummaryModel()
             {
                 Title = x.Element("Title") == null ? "" : x.Element("Title").Value,//考核任务名称
                 LevelType = x.Element("LevelType") == null ? "" : x.Element("LevelType").Value,//
                 AdviceType = x.Element("AdviceType") == null ? "" : x.Element("AdviceType").Value,//
                 TaskFlag = x.Element("TaskFlag") == null ? "" : x.Element("TaskFlag").Value,//ID
                 TaskNum = x.Element("TaskNum") == null ? "" : x.Element("TaskNum").Value,//
                 CountPerson = x.Element("CountPerson") == null ? "" : x.Element("CountPerson").Value,//
                 DeptName = x.Element("DeptName") == null ? "" : x.Element("DeptName").Value//
             });
        //获取记录总数
        string ss = dsLinq.ToString();
        int totalCount = dsLinq.Count();
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //设置记录总数
        sbReturn.Append("{");
        sbReturn.Append("totalCount:");
        sbReturn.Append(totalCount.ToString());
        //设置数据
        sbReturn.Append(",data:");
        sbReturn.Append(PageListCommon.ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }
    private void SearchScoreInfo(HttpContext context)
    {
        //从请求中获取排序列
        string orderString = context.Request.QueryString["OrderBy"];

        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "TaskNo";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        int pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(context.Request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;
        //获取数据
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        if (context.Request.QueryString["TaskFlag"]!="0" )
        searchModel.TaskFlag = context.Request.QueryString["TaskFlag"];//考核期间类型
        if (!string.IsNullOrEmpty(context.Request.QueryString["TaskNo"]))
        searchModel.TaskNo = context.Request.QueryString["TaskNo"];//考核任务编号
        if (context.Request.QueryString["PerTypeID"] != "0")
        searchModel.CompleteDate  = context.Request.QueryString["PerTypeID"];//考核类型
        if (context.Request.QueryString["TaskFlag"] != "4" && context.Request.QueryString["TaskFlag"] != "5")
        {
               if (context.Request.QueryString["TaskNum"] != "0")
                searchModel.TaskNum = context.Request.QueryString["TaskNum"];//考核期间 
        }
        if (context.Request.QueryString["LevelType"] != "0")
        searchModel.Summaryer  = context.Request.QueryString["LevelType"];//考核等级
        if (context.Request.QueryString["AdviceType"] != "0")
        searchModel.Title = context.Request.QueryString["AdviceType"];//考核建议
        if (context.Request.QueryString["TaskDate"] != "0")
            searchModel.TaskDate  = context.Request.QueryString["TaskDate"];//考核建议
        
        if ( !string .IsNullOrEmpty ( context.Request.QueryString["EmployeeID"] ))
        searchModel.EditFlag = context.Request.QueryString["EmployeeID"];//被考核人
        searchModel.CompanyCD = userInfo.CompanyCD;
        //查询数据
        DataTable dtData = PerformanceQueryBus.SearchScoreInfo(searchModel);

        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new PerformanceTaskModel()
             {
                 ID = x.Element("ID") == null ? "" : x.Element("ID").Value,//ID
                 TaskNo = x.Element("TaskNo") == null ? "" : x.Element("TaskNo").Value,
                 Title = x.Element("Title") == null ? "" : x.Element("Title").Value,
                 TaskDate = x.Element("TaskDate") == null ? "" : x.Element("TaskDate").Value,
                 TaskNum = x.Element("TaskNum") == null ? "" : x.Element("TaskNum").Value,
                 StartDate = x.Element("RealScore") == null ? "" : x.Element("RealScore").Value,
                 EditFlag = x.Element("LevelType") == null ? "" : x.Element("LevelType").Value,
                 Status = x.Element("KillScore") == null ? "" : x.Element("KillScore").Value,
                 Creator = x.Element("AddScore") == null ? "" : x.Element("AddScore").Value,
                 CreateDate = x.Element("TotalScore") == null ? "" : x.Element("TotalScore").Value,
                 SummaryDate = x.Element("EvaluateDate") == null ? "" : x.Element("EvaluateDate").Value,
                 Summaryer = x.Element("EvaluaterName") == null ? "" : x.Element("EvaluaterName").Value,
                 TaskFlag = x.Element("TaskFlag") == null ? "" : x.Element("TaskFlag").Value,
                 Remark = x.Element("passEmployeeName") == null ? "" : x.Element("passEmployeeName").Value,
                 ModifiedDate = x.Element("passEmployeeID") == null ? "" : x.Element("passEmployeeID").Value,
                 CompanyCD = x.Element("templateName") == null ? "" : x.Element("templateName").Value,
                 EndDate = x.Element("AdviceType") == null ? "" : x.Element("AdviceType").Value,
                 CompleteDate = x.Element("TemplateNo") == null ? "" : x.Element("TemplateNo").Value,
                 ModifiedUserID = x.Element("SignDate") == null ? "" : x.Element("SignDate").Value
             })
                       :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PerformanceTaskModel()
             {
                 ID = x.Element("ID") == null ? "" : x.Element("ID").Value,//ID
                 TaskNo = x.Element("TaskNo") == null ? "" : x.Element("TaskNo").Value,
                 Title = x.Element("Title") == null ? "" : x.Element("Title").Value,
                 TaskDate = x.Element("TaskDate") == null ? "" : x.Element("TaskDate").Value,
                 TaskNum = x.Element("TaskNum") == null ? "" : x.Element("TaskNum").Value,
                 StartDate = x.Element("RealScore") == null ? "" : x.Element("RealScore").Value,
                 EditFlag = x.Element("LevelType") == null ? "" : x.Element("LevelType").Value,
                 Status = x.Element("KillScore") == null ? "" : x.Element("KillScore").Value,
                 Creator = x.Element("AddScore") == null ? "" : x.Element("AddScore").Value,
                 CreateDate = x.Element("TotalScore") == null ? "" : x.Element("TotalScore").Value,
                 SummaryDate = x.Element("EvaluateDate") == null ? "" : x.Element("EvaluateDate").Value,
                 Summaryer = x.Element("EvaluaterName") == null ? "" : x.Element("EvaluaterName").Value,
                 TaskFlag = x.Element("TaskFlag") == null ? "" : x.Element("TaskFlag").Value,
                 Remark = x.Element("passEmployeeName") == null ? "" : x.Element("passEmployeeName").Value,
                 ModifiedDate = x.Element("passEmployeeID") == null ? "" : x.Element("passEmployeeID").Value,
                 CompanyCD = x.Element("templateName") == null ? "" : x.Element("templateName").Value,
                 EndDate = x.Element("AdviceType") == null ? "" : x.Element("AdviceType").Value,
                 CompleteDate = x.Element("TemplateNo") == null ? "" : x.Element("TemplateNo").Value,
                 ModifiedUserID = x.Element("SignDate") == null ? "" : x.Element("SignDate").Value
             });
        //获取记录总数
        string ss = dsLinq.ToString();
        int totalCount = dsLinq.Count();
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //设置记录总数
        sbReturn.Append("{");
        sbReturn.Append("totalCount:");
        sbReturn.Append(totalCount.ToString());
        //设置数据
        sbReturn.Append(",data:");
        sbReturn.Append(PageListCommon.ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }
    private void SearchPerTypeInfo(HttpContext context)
    {
        //获取数据
        PerformanceTypeModel searchModel = new PerformanceTypeModel();
        DataTable dtData = PerformanceQueryBus.SearchPerTypeInfo (searchModel);
        StringBuilder sbReturn = new StringBuilder();
        sbReturn.Append("{");
        sbReturn.Append("data:");
        if (dtData.Rows.Count > 0)
        {
            sbReturn.Append(JsonClass.DataTable2Json(dtData));
        }
        else
        {
            sbReturn.Append("[]");
        }
        sbReturn.Append("}");
        context.Response.ContentType = "text/plain";
        //返回数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    
    }
    private void SearchData(HttpContext context)
    {
        //从请求中获取排序列
        string orderString = context.Request.QueryString["OrderBy"];

        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "TaskNo";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        int pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(context.Request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;
 
        //获取数据
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        //设置查询条件
        //要素名称
        /// searchModel.ElemName = context.Request.QueryString["ElemName"];
        //启用状态
        /// searchModel.UsedStatus = context.Request.QueryString["UsedStatus"];

        //查询数据
        DataTable dtData = PerformanceQueryBus.SearchRectCheckElemInfo(searchModel);
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new PerformanceTaskModel()
             {
                 TaskNo = x.Element("TaskNo").Value,//ID
                 ID = x.Element("ID").Value,//ID
                 Title = x.Element("Title").Value//类型名称



             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PerformanceTaskModel()
             {
                 TaskNo = x.Element("TaskNo").Value,//ID
                 ID = x.Element("ID").Value,//ID
                 Title = x.Element("Title").Value//类型名称
             });
        //获取记录总数
        int totalCount = dsLinq.Count();
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //设置记录总数
        sbReturn.Append("{");
        sbReturn.Append("totalCount:");
        sbReturn.Append(totalCount.ToString());
        //设置数据
        sbReturn.Append(",data:");
        sbReturn.Append(PageListCommon.ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }
    private void SearchDetailsInfo(HttpContext context)
    {
        //从请求中获取排序列
        string orderString = context.Request.QueryString["OrderBy"];

        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "TaskNo";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        int pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(context.Request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;
        //获取数据
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        if (context.Request.QueryString["TaskFlag"] != "0")
            searchModel.TaskFlag = context.Request.QueryString["TaskFlag"];//考核期间类型
        if (!string.IsNullOrEmpty(context.Request.QueryString["TaskNo"]))
            searchModel.TaskNo = context.Request.QueryString["TaskNo"];//考核任务编号
        if (context.Request.QueryString["PerTypeID"] != "0")
            searchModel.CompleteDate = context.Request.QueryString["PerTypeID"];//考核类型
        if (context.Request.QueryString["TaskFlag"] != "4" && context.Request.QueryString["TaskFlag"] != "5")
        {
            if (context.Request.QueryString["TaskNum"] != "0")
                searchModel.TaskNum = context.Request.QueryString["TaskNum"];//考核期间 
        }
        if (context.Request.QueryString["LevelType"] != "0")
            searchModel.Summaryer = context.Request.QueryString["LevelType"];//考核等级
        if (context.Request.QueryString["AdviceType"] != "0")
            searchModel.Title = context.Request.QueryString["AdviceType"];//考核建议
        if (context.Request.QueryString["TaskDate"] != "0")
            searchModel.TaskDate = context.Request.QueryString["TaskDate"];//考核建议
        if (context.Request.QueryString["DeptID"] != "0")
            searchModel.EndDate = context.Request.QueryString["DeptID"];//部门

        if (!string.IsNullOrEmpty(context.Request.QueryString["EmployeeID"]))
            searchModel.EditFlag = context.Request.QueryString["EmployeeID"];//被考核人
        searchModel.CompanyCD = userInfo.CompanyCD;
        //查询数据
        DataTable dtData = PerformanceQueryBus.SearchScoreInfo(searchModel);

        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new PerformanceTaskModel()
             {
                 ID = x.Element("ID") == null ? "" : x.Element("ID").Value,//ID
                 TaskNo = x.Element("DeptName") == null ? "" : x.Element("DeptName").Value,//部门
                 StartDate = x.Element("RealScore") == null ? "" : x.Element("RealScore").Value,//实际得分
                 EditFlag = x.Element("LevelType") == null ? "" : x.Element("LevelType").Value,//考核等级
                 Status = x.Element("KillScore") == null ? "" : x.Element("KillScore").Value,//累计扣分
                 Creator = x.Element("AddScore") == null ? "" : x.Element("AddScore").Value,//累计加分
                 CreateDate = x.Element("TotalScore") == null ? "" : x.Element("TotalScore").Value,//考核总得分
                 Summaryer = x.Element("TypeName") == null ? "" : x.Element("TypeName").Value,//考核类型名称
                 EndDate = x.Element("AdviceType") == null ? "" : x.Element("AdviceType").Value,
                 Remark = x.Element("passEmployeeName") == null ? "" : x.Element("passEmployeeName").Value,//人员
             })
                       :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PerformanceTaskModel()
             {
                 ID = x.Element("ID") == null ? "" : x.Element("ID").Value,//ID
                 TaskNo = x.Element("DeptName") == null ? "" : x.Element("DeptName").Value,//部门
                 StartDate = x.Element("RealScore") == null ? "" : x.Element("RealScore").Value,//实际得分
                 EditFlag = x.Element("LevelType") == null ? "" : x.Element("LevelType").Value,//考核等级
                 Status = x.Element("KillScore") == null ? "" : x.Element("KillScore").Value,//累计扣分
                 Creator = x.Element("AddScore") == null ? "" : x.Element("AddScore").Value,//累计加分
                 CreateDate = x.Element("TotalScore") == null ? "" : x.Element("TotalScore").Value,//考核总得分
                 Summaryer = x.Element("TypeName") == null ? "" : x.Element("TypeName").Value,//考核类型名称
                 EndDate = x.Element("AdviceType") == null ? "" : x.Element("AdviceType").Value,
                 Remark = x.Element("passEmployeeName") == null ? "" : x.Element("passEmployeeName").Value,//人员
             });
        //获取记录总数
        string ss = dsLinq.ToString();
        int totalCount = dsLinq.Count();
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //设置记录总数
        sbReturn.Append("{");
        sbReturn.Append("totalCount:");
        sbReturn.Append(totalCount.ToString());
        //设置数据
        sbReturn.Append(",data:");
        sbReturn.Append(PageListCommon.ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }
    private void SearchDetailsInfoByLT(HttpContext context)
    {
        //从请求中获取排序列
        string orderString = context.Request.QueryString["OrderBy"];

        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "LevelType";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        int pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(context.Request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;
        //获取数据
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        if (context.Request.QueryString["TaskFlag"] != "0")
            searchModel.TaskFlag = context.Request.QueryString["TaskFlag"];//考核期间类型
        if (!string.IsNullOrEmpty(context.Request.QueryString["TaskNo"]))
            searchModel.TaskNo = context.Request.QueryString["TaskNo"];//考核任务编号
        if (context.Request.QueryString["PerTypeID"] != "0")
            searchModel.CompleteDate = context.Request.QueryString["PerTypeID"];//考核类型
        if (context.Request.QueryString["TaskFlag"] != "4" && context.Request.QueryString["TaskFlag"] != "5")
        {
            if (context.Request.QueryString["TaskNum"] != "0")
                searchModel.TaskNum = context.Request.QueryString["TaskNum"];//考核期间 
        }
        if (context.Request.QueryString["LevelType"] != "0")
            searchModel.Summaryer = context.Request.QueryString["LevelType"];//考核等级
        if (context.Request.QueryString["AdviceType"] != "0")
            searchModel.Title = context.Request.QueryString["AdviceType"];//考核建议
        if (context.Request.QueryString["TaskDate"] != "0")
            searchModel.TaskDate = context.Request.QueryString["TaskDate"];//考核建议

        if (!string.IsNullOrEmpty(context.Request.QueryString["EmployeeID"]))
            searchModel.EditFlag = context.Request.QueryString["EmployeeID"];//被考核人
        searchModel.CompanyCD = userInfo.CompanyCD;
        //查询数据
        DataTable dtData = PerformanceQueryBus.SearchDetailsInfoByLT(searchModel);

        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new PerformanceTaskModel()
             {
                 TaskNo = x.Element("LevelType") == null ? "" : x.Element("LevelType").Value,//考核等级
                 Remark = x.Element("ID") == null ? "" : x.Element("ID").Value//人数
             })
                       :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PerformanceTaskModel()
             {
                 TaskNo = x.Element("LevelType") == null ? "" : x.Element("LevelType").Value,//考核等级
                 Remark = x.Element("ID") == null ? "" : x.Element("ID").Value//人数
             });
        //获取记录总数
        string ss = dsLinq.ToString();
        int totalCount = dsLinq.Count();
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //设置记录总数
        sbReturn.Append("{");
        sbReturn.Append("totalCount:");
        sbReturn.Append(totalCount.ToString());
        //设置数据
        sbReturn.Append(",data:");
        sbReturn.Append(PageListCommon.ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}
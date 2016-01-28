<%@ WebHandler Language="C#" Class="TaskList" %>

using System;
using System.Web;
using System.Data;

using XBase.Common;
using XBase.Model.Personal.Task;
using XBase.Business.Personal.Task;

public class TaskList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    string orderby = "";
    public void ProcessRequest(HttpContext context)
    {
        //定义Json返回变量
        JsonClass json = new JsonClass();

        // 构建任务model

        TaskModel model = BuildingTaskModel(context.Request);
        //执行获取操作
        DataTable ListDt = new DataTable();
        if (model.Attachment == "Dept")
        {
            ListDt = TaskListBus.GetTaskListReportDept(model);
        }
        else if (model.Attachment == "Personal")
        {
            ListDt = TaskListBus.GetTaskListReportPrincipal(model);

        }
        else if (model.Attachment == "Status")
        {
            ListDt = TaskListBus.GetTaskListReportStatus(model);
        }
        else
        {
            ListDt = TaskListBus.GetTaskList(model, orderby);
        }

        SessionUtil.Session.Add("CurrentListTable", ListDt);
        //获取成功时
        if (ListDt != null && ListDt.Rows.Count > 0)
        {
            //string jsondataStr = JsonClass.DataTable2Json(ListDt);
            string jsondataStr = "[";
            foreach (DataRow dr in ListDt.Rows)
            {
                jsondataStr += "{";
                jsondataStr += "'ID':'" + dr["ID"] + "',";
                jsondataStr += "'TaskNo':'" + dr["TaskNo"] + "',";
                jsondataStr += "'Title':'" + dr["Title"] + "',";
                jsondataStr += "'TaskTypeID':'" + dr["TaskTypeID"] + "',";
                jsondataStr += "'Principal':'" + dr["Principal"] + "',";
                jsondataStr += "'PrincipalName':'" + dr["PrincipalName"] + "',";
                jsondataStr += "'Critical':'" + dr["Critical"] + "',";
                jsondataStr += "'Important':'" + dr["Important"] + "',";
                jsondataStr += "'Priority':'" + dr["Priority"] + "',";
                jsondataStr += "'CompleteDate':'" + dr["CompleteDate"] + "',";
                jsondataStr += "'CompleteTime':'" + dr["CompleteTime"] + "',";
                jsondataStr += "'Status':'" + dr["Status"] + "',";
                if (string.IsNullOrEmpty(model.Attachment))
                {
                    jsondataStr += "'Creator':'" + dr["Creator"] + "',";
                }
                jsondataStr += "'CreateDate':'" + dr["CreateDate"] + "'";
                jsondataStr += "},";
            }
            jsondataStr = jsondataStr.Substring(0, jsondataStr.Length - 1) + "]";
            json = new JsonClass("SELECT", jsondataStr, 1);
        }
        //获取失败时
        else
        {
            json = new JsonClass("SELECT", "", 0);
        }

        //输出响应
        context.Response.Write(json);

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


    private TaskModel BuildingTaskModel(HttpRequest request)
    {
        orderby = request.QueryString["OrderBy"];
        TaskModel model = new TaskModel();
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        model.TaskNo = request.QueryString["TaskNo"];
        try { model.TaskTypeID = Convert.ToInt32(request.QueryString["TaskTypeID"]); }
        catch { model.TaskTypeID = -1; }
        model.Title = request.QueryString["Title"];
        try { model.Principal = Convert.ToInt32(request.QueryString["Principal"]); }
        catch { model.Principal = 0; }
        model.TaskType = request.QueryString["TaskType"];
        model.Status = request.QueryString["Status"];
        model.Critical = request.QueryString["Critical"];
        model.Important = request.QueryString["Important"];
        model.Priority = request.QueryString["Priority"];
        try
        {
            model.SendDate = DateTime.Parse(request.QueryString["StartDate"].ToString());

        }
        catch { }
        try
        {
            model.ReportDate = DateTime.Parse(request.QueryString["EndDate"].ToString());
        }
        catch { }
        model.Joins = userInfo.EmployeeName;
        try
        {
            model.DeptID = int.Parse(request.QueryString["DeptID"].ToString());
        }
        catch { }
        model.Attachment = request.QueryString["FromWitch"];
        try
        {
            model.CheckUserID = int.Parse(request.QueryString["SearchID"].ToString());
        }
        catch { model.CheckUserID = 0; }

        model.CheckNote = request.QueryString["SearchType"];

        return model;
    }

}
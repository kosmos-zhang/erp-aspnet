<%@ WebHandler Language="C#" Class="PerformanceGrade" %>
/**********************************************
 * 作    者： 王保军
 * 创建日期： 2009/05/11
 * 描    述： 待评分列表
 * 修改日期： 2009/05/11
 * 版    本： 0.5.0
 ***********************************************/
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
public class PerformanceGrade : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        //获取请求动作
        string action = context.Request.Params["action"];
        //保存评分项目信息  
        //初期化评分项目树时
        if ("GetTemplateInfo".Equals(action))
        {
            //获取要素ID

            //获取考核类型信息
            DataTable dtDeptInfo = PerformanceTaskWorkBus.GetTemplateInfoo();

            //定义放回变量
            StringBuilder sbReturn = new StringBuilder();
            sbReturn.Append("{");
            sbReturn.Append("data:");
            if (dtDeptInfo.Rows.Count > 0)
            {
                sbReturn.Append(JsonClass.DataTable2Json(dtDeptInfo));
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
        else if ("GetEmployeeInf".Equals(action))
        {
            //获取要素ID

            //获取考核类型信息
            DataTable dtDeptInfo = PerformanceTaskWorkBus.GetEmployeeInfo();

            //定义放回变量
            StringBuilder sbReturn = new StringBuilder();
            sbReturn.Append("{");
            sbReturn.Append("data:");
            if (dtDeptInfo.Rows.Count > 0)
            {
                sbReturn.Append(JsonClass.DataTable2Json(dtDeptInfo));
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
        else if ("GetTaskInfoByTaskNO".Equals(action))
        {
            //获取要素ID
            string taskNo = context.Request.Params["taskNo"];
            //获取考核类型信息
            DataTable dtDeptInfo = PerformancePersonalBus.SearchTaskInfoByTaskNO(taskNo);

            //定义放回变量
            StringBuilder sbReturn = new StringBuilder();
            sbReturn.Append("{");
            sbReturn.Append("data:");
            if (dtDeptInfo.Rows.Count > 0)
            {
                sbReturn.Append(JsonClass.DataTable2Json(dtDeptInfo));
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
        else if ("GetEmployeeInfoByTemplateNo".Equals(action))
        {
            //获取要素ID
            string templateNo = context.Request.Params["templateNo"];
            //获取考核类型信息
            DataTable dtDeptInfo = PerformanceTaskWorkBus.GetEmployeeInfoByTemplateNo(templateNo);

            //定义放回变量
            StringBuilder sbReturn = new StringBuilder();
            sbReturn.Append("{");
            sbReturn.Append("data:");
            if (dtDeptInfo.Rows.Count > 0)
            {
                sbReturn.Append(JsonClass.DataTable2Json(dtDeptInfo));
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
        else if ("EditInfo".Equals(action))
        {
            PerformancePersonalModel model = new PerformancePersonalModel();
            //编辑模式
            model.EditFlag = context.Request.QueryString["EditFlag"];
            //类型ID 
            model.Title = context.Request.QueryString["Title"];
            //类型名称
            model.StartDate = context.Request.QueryString["StartDate"];
            //启用状态
            model.EndDate = context.Request.QueryString["EndDate"];

            string flag = context.Request.QueryString["TaskFlag"];
            model.TaskFlag = flag;
            if (flag == "4")
            {
                model.TaskNum = DateTime.Now.Year.ToString();

            }
            else
            {
                model.TaskNum = context.Request.QueryString["TaskNum"];
            }
            model.TaskDate = DateTime.Now.Year.ToString();


            model.Status = "0";//草稿
            model.Remark = context.Request.QueryString["Remark"];


            model.WorkContent = context.Request.QueryString["WorkContent"];
            model.Complete = context.Request.QueryString["Complete"];
            model.AfterWork = context.Request.QueryString["AfterWork"];
            model.Defects = context.Request.QueryString["Defects"];
            model.Problems = context.Request.QueryString["Problems"];
            model.Advices = context.Request.QueryString["Advices"];
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            model.Creator = userInfo.EmployeeID.ToString();
            model.ModifiedUserID = userInfo.EmployeeID.ToString();
            model.TaskNo = context.Request.QueryString["TaskNo"];
            if (model.EditFlag == "INSERT")
            {
                if (PerformancePersonalBus.IsExist(model.TaskNo))
                {
                    context.Response.Write(new JsonClass("", "", 2));
                    return;
                }
            }


            model.CompanyCD = userInfo.CompanyCD;
            //执行保存
            bool isSucc = PerformancePersonalBus.SaveProPersonalInfo(model);
            //成功
            if (isSucc)
            {
                context.Response.Write(new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, model.TaskNo, 1));
            }
            //失败
            else
            {
                context.Response.Write(new JsonClass("", "", 0));
            }
        }
        else if ("UpdateInfo".Equals(action))
        {
            PerformancePersonalModel model = new PerformancePersonalModel();
            //编辑模式
            model.EditFlag = context.Request.QueryString["EditFlag"];
            //类型ID 
            model.Title = context.Request.QueryString["Title"];
            //类型名称
            model.StartDate = context.Request.QueryString["StartDate"];
            //启用状态
            model.EndDate = context.Request.QueryString["EndDate"];

            string flag = context.Request.QueryString["TaskFlag"];
            model.TaskFlag = flag;
            if (flag == "4")
            {
                model.TaskNum = DateTime.Now.Year.ToString();

            }
            else
            {
                model.TaskNum = context.Request.QueryString["TaskNum"];
            }
            model.TaskDate = DateTime.Now.Year.ToString();


            model.Status = "1";//已确认
            model.Remark = context.Request.QueryString["Remark"];


            model.WorkContent = context.Request.QueryString["WorkContent"];
            model.Complete = context.Request.QueryString["Complete"];
            model.AfterWork = context.Request.QueryString["AfterWork"];
            model.Defects = context.Request.QueryString["Defects"];
            model.Problems = context.Request.QueryString["Problems"];
            model.Advices = context.Request.QueryString["Advices"];
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            model.Creator = userInfo.EmployeeID.ToString();
            model.ModifiedUserID = userInfo.EmployeeID.ToString();
            model.TaskNo = context.Request.QueryString["TaskNo"];
            model.Checker = userInfo.EmployeeID.ToString();
            if (model.EditFlag == "INSERT")
            {
                if (PerformancePersonalBus.IsExist(model.TaskNo))
                {
                    context.Response.Write(new JsonClass("", "", 2));
                    return;
                }
            }


            model.CompanyCD = userInfo.CompanyCD;
            //执行保存
            bool isSucc = PerformancePersonalBus.UpdateProPersonalInfo(model);
            //成功
            if (isSucc)
            {
                context.Response.Write(new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, model.TaskNo, 1));
            }
            //失败
            else
            {
                context.Response.Write(new JsonClass("", "", 0));
            }
        }
        else if ("SearchInfo".Equals(action))
        {
            //执行查询
            SearchData(context);
        }
        else if ("DeleteInfo".Equals(action))
        {
            //获取要素ID
            string elemID = context.Request.QueryString["DeleteNO"];
            //替换引号
            elemID = elemID.Replace("'", "");

            //判断要素是否被使用
            // bool isUsed = PerformanceTypeBus.IsTemplateUsed(elemID);
            //已经被使用
            //if (isUsed)
            //{
            //    //输出响应 返回不执行删除
            //    context.Response.Write(new JsonClass("", "", 2));
            //}
            //else
            //{
            //删除要素
            bool isSucc = PerformancePersonalBus.DeletePerTypeInfo(elemID);
            //删除成功
            if (isSucc)
            {
                //输出响应
                context.Response.Write(new JsonClass("", "", 1));
            }
            //删除失败
            else
            {
                //输出响应
                context.Response.Write(new JsonClass("", "", 0));
            }
            //}
        }
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
        PerformanceScoreModel searchModel = new PerformanceScoreModel();
        //设置查询条件
        //要素名称
        //search += "TaskNo=" + document.getElementById("txtSearchTaskNo").value;
        ////启用状态
        //search += "&Title=" + document.getElementById("inptTitle").value;
        //search += "&TaskFlag=" + document.getElementById("selTaskFlag").value;
        //search += "&TaskNum=" + document.getElementById("selTaskNum").value;

        searchModel.TaskNo = context.Request.QueryString["searchTaskNo"];
        //启用状态
        searchModel.TaskTitle = context.Request.QueryString["searchTitle"];
        if (context.Request.QueryString["searchTaskFlag"] != "0")
            searchModel.TaskFlag = context.Request.QueryString["searchTaskFlag"];
        //启用状态
        searchModel.EmployeeID = context.Request.QueryString["searchEmployeeID"];
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        searchModel.ScoreEmployee  = userInfo.EmployeeID.ToString ();
        searchModel.Status = context.Request.QueryString["Searchstatus"];
        //查询数据

        DataTable dtData = PerformanceGradeBus.SearchTaskInfo(searchModel);
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new PerformanceScoreModel()
             {
                  AdviceNote = x.Element("EmployeeID") == null ? "" : x.Element("EmployeeID").Value,
                 TaskNo = x.Element("TaskNo") == null ? "" : x.Element("TaskNo").Value,
                 TaskTitle = x.Element("TaskTitle") == null ? "" : x.Element("TaskTitle").Value,
                 TemplateName = x.Element("TemplateName") == null ? "" : x.Element("TemplateName").Value,
                 TaskFlag = x.Element("TaskFlag") == null ? "" : x.Element("TaskFlag").Value,
                 EmployeeName = x.Element("EmployeeName") == null ? "" : x.Element("EmployeeName").Value,
                 CreateEmployeeName = x.Element("CreateEmployeeName") == null ? "" : x.Element("CreateEmployeeName").Value,
                 TemplateNo = x.Element("TemplateNo") == null ? "" : x.Element("TemplateNo").Value,
                 CreateDate = x.Element("CreateDate") == null ? "" : x.Element("CreateDate").Value
                  
             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PerformanceScoreModel()
             {
                 AdviceNote = x.Element("EmployeeID") == null ? "" : x.Element("EmployeeID").Value,
                 TaskNo = x.Element("TaskNo") == null ? "" : x.Element("TaskNo").Value,
                 TaskTitle = x.Element("TaskTitle") == null ? "" : x.Element("TaskTitle").Value,
                 TemplateName = x.Element("TemplateName") == null ? "" : x.Element("TemplateName").Value,
                 TaskFlag = x.Element("TaskFlag") == null ? "" : x.Element("TaskFlag").Value,
                 EmployeeName = x.Element("EmployeeName") == null ? "" : x.Element("EmployeeName").Value,
                 CreateEmployeeName = x.Element("CreateEmployeeName") == null ? "" : x.Element("CreateEmployeeName").Value,
                 TemplateNo = x.Element("TemplateNo") == null ? "" : x.Element("TemplateNo").Value,
                 CreateDate = x.Element("CreateDate") == null ? "" : x.Element("CreateDate").Value
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
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
<%@ WebHandler Language="C#" Class="PerformancePersonal" %>
/**********************************************
 * 作    者： 王保军
 * 创建日期： 2009/05/10
 * 描    述： 新建自我鉴定
 * 修改日期： 2009/05/10
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
public class PerformancePersonal : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            foreach (DataRow row in dtDeptInfo.Rows)
            {
                string tt = row["WorkContent"].ToString();
                string Complete = row["Complete"].ToString();
                string AfterWork = row["AfterWork"].ToString();
                string Defects = row["Defects"].ToString();
                string Problems = row["Problems"].ToString();
                string Advices = row["Advices"].ToString();
                string Remark = row["Remark"].ToString(); 
                row["WorkContent"] = tt.Replace("\r\n", "<br/>").Replace("\\0", "/0");
                row["Complete"] = Complete.Replace("\r\n", "<br/>").Replace("\\0", "/0");
                row["AfterWork"] = AfterWork.Replace("\r\n", "<br/>").Replace("\\0", "/0");
                row["Defects"] = Defects.Replace("\r\n", "<br/>").Replace("\\0", "/0");
                row["Problems"] = Problems.Replace("\r\n", "<br/>").Replace("\\0", "/0");
                row["Advices"] = Advices.Replace("\r\n", "<br/>").Replace("\\0", "/0");
                row["Remark"] = Remark.Replace("\r\n", "<br/>").Replace("\\0", "/0");
            }
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
            sbReturn.Append(JsonClass.DataTable2Json(dtDeptInfo));
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
            if (flag == "4" || flag == "5")
            {
               // model.TaskNum = DateTime.Now.Year.ToString();
                model.TaskDate = DateTime.Now.Year.ToString();
            }
            else
            {
                model.TaskDate = context.Request.QueryString["TaskDate"];
                model.TaskNum = context.Request.QueryString["TaskNum"];
            }
           // model.TaskDate = DateTime.Now.Year.ToString();


            model.Status = "0";//草稿
            model.Remark = context.Request.QueryString["Remark"].Replace("\r\n", "\\r\\n");


            model.WorkContent = context.Request.QueryString["WorkContent"].Replace("\r\n", "\\r\\n");
            model.Complete = context.Request.QueryString["Complete"].Replace("\r\n", "\\r\\n");
            model.AfterWork = context.Request.QueryString["AfterWork"].Replace("\r\n", "\\r\\n");
            model.Defects = context.Request.QueryString["Defects"].Replace("\r\n", "\\r\\n");
            model.Problems = context.Request.QueryString["Problems"].Replace("\r\n", "\\r\\n");
            model.Advices = context.Request.QueryString["Advices"].Replace("\r\n", "\\r\\n");
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            model.Creator = userInfo.EmployeeID.ToString();
            model.ModifiedUserID = userInfo.EmployeeID.ToString();
            model.TaskNo = context.Request.QueryString["TaskNo"];
            if (model.EditFlag == "INSERT")
            {
                if (string.IsNullOrEmpty(model.TaskNo))
                {
                    //获取编码规则编号
                    string codeRuleID = context.Request.QueryString["CodeRuleID"];
                    //通过编码规则代码获取人员编码
                    model.TaskNo = ItemCodingRuleBus.GetCodeValue(codeRuleID);
                }
                if (PerformancePersonalBus.IsExist(model.TaskNo))
                {
                    context.Response.Write(new JsonClass("", "", 2));
                    return;
                }
            }


            model.CompanyCD = userInfo.CompanyCD;
            //执行保存
            bool isSucc = PerformancePersonalBus.SaveProPersonalInfo (model);
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
              string[] elemIDlist = elemID.Split(',');
            bool isSucc = false;
            bool isCheck = false;
            string temElem = string.Empty;
            for (int i = 0; i < elemIDlist.Length; i++)
            {

                isCheck = PerformancePersonalBus.IsCheck(elemIDlist[i]);
                
                if (isCheck)
                {
                    isSucc = PerformancePersonalBus.DeletePerTypeInfo(elemIDlist[i]);
                    if (isSucc)
                    {
                        continue;
                    }
                    else
                    {
                        isSucc = false;
                        break;
                    }
                }
                else
                {
                    temElem = elemIDlist[i];
                    break;
                }
                
            }
            if (!isCheck)
            {
                context.Response.Write(new JsonClass("", temElem , 2));
            }
            else
            {
                if (isSucc)
                {
                    context.Response.Write(new JsonClass("", "", 1));
                }
                else
                {
                    context.Response.Write(new JsonClass("", "", 0));
                }
            }
        }
    }
    private void SearchData(HttpContext context)
    {
        //从请求中获取排序列
        string orderString = context.Request.QueryString["OrderBy"];

        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";
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
        PerformancePersonalModel searchModel = new PerformancePersonalModel();
        //设置查询条件
        //要素名称
        //search += "TaskNo=" + document.getElementById("txtSearchTaskNo").value;
        ////启用状态
        //search += "&Title=" + document.getElementById("inptTitle").value;
        //search += "&TaskFlag=" + document.getElementById("selTaskFlag").value;
        //search += "&TaskNum=" + document.getElementById("selTaskNum").value;
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        searchModel.Creator = userInfo.EmployeeID.ToString();

        searchModel.TaskNo = context.Request.QueryString["TaskNo"];
        //启用状态
        searchModel.Title = context.Request.QueryString["Title"];
        if (context.Request.QueryString["TaskFlag"]!="0")
        searchModel.TaskFlag = context.Request.QueryString["TaskFlag"];
        if (context.Request.QueryString["TaskDate"] != "0")
            searchModel.TaskDate  = context.Request.QueryString["TaskDate"];
        
        
        
        //启用状态
        if (context.Request.QueryString["TaskNum"] != "0")
        searchModel.TaskNum = context.Request.QueryString["TaskNum"];
       // searchModel.Status = "0";//默认草稿状态
        //查询数据
        DataTable dtData = PerformancePersonalBus.SearchTaskInfo (searchModel);
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new PerformancePersonalModel()
             {
                 ID = x.Element("ID") == null ? "" : x.Element("ID").Value,//ID
                 TaskNo = x.Element("TaskNo") == null ? "" : x.Element("TaskNo").Value,
                 Title = x.Element("Title") == null ? "" : x.Element("Title").Value,
                 TaskFlag = x.Element("TaskFlag") == null ? "" : x.Element("TaskFlag").Value,
                 TaskNum = x.Element("TaskNum") == null ? "" : x.Element("TaskNum").Value,
                 StartDate = x.Element("StartDate") == null ? "" : x.Element("StartDate").Value,
                 CreateDate = x.Element("CreateDate") == null ? "" : x.Element("CreateDate").Value,
                 Status = x.Element("Status") == null ? "" : x.Element("Status").Value,
                 EndDate = x.Element("EndDate") == null ? "" : x.Element("EndDate").Value
             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PerformancePersonalModel()
             { 
                 ID = x.Element("ID") == null ? "" : x.Element("ID").Value,//ID
                 TaskNo = x.Element("TaskNo") == null ? "" : x.Element("TaskNo").Value,
                 Title = x.Element("Title") == null ? "" : x.Element("Title").Value,
                 TaskFlag = x.Element("TaskFlag") == null ? "" : x.Element("TaskFlag").Value,
                 TaskNum = x.Element("TaskNum") == null ? "" : x.Element("TaskNum").Value,
                 StartDate = x.Element("StartDate") == null ? "" : x.Element("StartDate").Value,
                 CreateDate = x.Element("CreateDate") == null ? "" : x.Element("CreateDate").Value,
                 Status= x.Element("Status") == null ? "" : x.Element("Status").Value,
                 EndDate = x.Element("EndDate") == null ? "" : x.Element("EndDate").Value
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
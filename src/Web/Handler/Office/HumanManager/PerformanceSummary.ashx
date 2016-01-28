<%@ WebHandler Language="C#" Class="PerformanceSummary" %>

/**********************************************
 * 作    者： 王保军
 * 创建日期： 2009/05/14
 * 描    述： 待评分列表
 * 修改日期： 2009/05/14
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
public class PerformanceSummary : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
        else if ("GetTaskInfoByTaskNO".Equals(action)) //有用
        {
            //获取要素ID
            string taskNo = context.Request.Params["taskNo"];
            string tempalteNo = context.Request.Params["tempalteNo"];
            string EmployeId = context.Request.Params["EmployeId"];
            
            //获取考核类型信息
            DataTable dtDeptInfo =null ;
            if (string.IsNullOrEmpty(tempalteNo))
            {
                dtDeptInfo = PerformanceSummaryBus.SearchTaskInfoByTaskNO(taskNo);
            }
            else
            {
                dtDeptInfo = PerformanceSummaryBus.SearchTaskInfoByTaskNO(taskNo, tempalteNo, EmployeId);
            }
            int totalLen = dtDeptInfo.Rows.Count;
            string[] temp = new String[totalLen ];
            for (int i = 0; i < totalLen; i++)
            {
                temp[i] = dtDeptInfo.Rows[i]["EmployeeID"] == null ? "" : dtDeptInfo.Rows[i]["EmployeeID"].ToString();
            }
            string[] employeeIDList = GetString(temp);
            int  employeeCount = employeeIDList.Length;
            
            
            for (int i = 0; i < totalLen; i++)
            {
                temp[i] = dtDeptInfo.Rows[i]["ScoreEmployee"] == null ? "" : dtDeptInfo.Rows[i]["ScoreEmployee"].ToString();
            }
            string[] scoreIDList = GetString(temp);
            int scoreCount = scoreIDList.Length;
            int hasFinished =scoreCount  ;
            for (int a = 0; a < scoreCount; a++)
            {
                DataRow[] dt1 = dtDeptInfo.Select("ScoreEmployee='" + scoreIDList[a] + "'");
                DataRow[] dt2 = dtDeptInfo.Select("ScoreEmployee='" + scoreIDList[a] + "' and Status='1'");
               if (dt2.Length != dt1.Length)
               {
                   hasFinished --;
               }
            }
            int notFinish ;
            string isCheck = "0";
            if (hasFinished == scoreCount)
            {
                notFinish = 0;
                isCheck = "1";
            }
            else
            {
                notFinish = scoreCount - hasFinished; 
            }

            string info = employeeCount.ToString() + "_" + hasFinished.ToString() + "_" + notFinish.ToString()+"_"+isCheck ;         
            //定义放回变量
            StringBuilder sbReturn = new StringBuilder();
            sbReturn.Append("{");
            sbReturn.Append("data:");
            if (dtDeptInfo.Rows.Count > 0)
            {
                sbReturn.Append(JsonClass.DataTable2Json(dtDeptInfo));
                sbReturn.Append(",\"info\":\"" + info + "\"");
            }
            else
            {
                sbReturn.Append("[]");
            }
            sbReturn.Append("}");  
            context.Response.ContentType = "text/plain";
            //返回数据
            context.Response.Write(sbReturn.ToString());
            //context.Response.Write(new JsonClass("", sbReturn.ToString(), 0));
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
            PerformanceTaskModel model = new PerformanceTaskModel();
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
                model.TaskNum = DateTime.Now.Year.ToString();
            }
            else
            {
                model.TaskNum = context.Request.QueryString["TaskNum"];
            }
            model.TaskDate = DateTime.Now.Year.ToString();
            model.Status = "1";//待评分
            model.Remark = context.Request.QueryString["Remark"];
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            model.Creator = userInfo.EmployeeID.ToString();
            model.ModifiedUserID = userInfo.EmployeeID.ToString();
            model.TaskNo = context.Request.QueryString["TaskNo"];
            if (model.EditFlag == "INSERT")
            {
                if (PerformanceTaskWorkBus.IsExist(model.TaskNo))
                {
                    context.Response.Write(new JsonClass("", "", 2));
                    return;
                }
            }
            model.CompanyCD = userInfo.CompanyCD; ;
            string employeeIDList = context.Request.QueryString["employeeIDList"];
            string TemplateNoList = context.Request.QueryString["TemplateNoList"];
            IList<PerformanceScoreModel> modleList = new List<PerformanceScoreModel>();
            IList<PerformanceSummaryModel> modleSummaryList = new List<PerformanceSummaryModel>();
            string[] employeeID = employeeIDList.Split(',');
            string[] templateNo = TemplateNoList.Split(',');
            for (int i = 0; i < templateNo.Length; i++)
            {
                for (int a = 0; a < employeeID.Length; a++)
                {
                    DataTable dt = PerformanceTaskWorkBus.GetTemplatebyNO(templateNo[i], employeeID[a].ToString());
                    if (dt.Rows.Count < 0)
                    { return; }
                    for (int c = 0; c < dt.Rows.Count; c++)
                    {
                        PerformanceScoreModel modelScore = new PerformanceScoreModel();
                        modelScore.EditFlag = context.Request.QueryString["EditFlag"];
                        modelScore.TaskNo = context.Request.QueryString["TaskNo"];
                        modelScore.TemplateNo = templateNo[i];
                        modelScore.EmployeeID = employeeID[a];
                        modelScore.CompanyCD = userInfo.CompanyCD;
                        modelScore.ElemName = dt.Rows[c]["ElemName"] == null ? "" : dt.Rows[c]["ElemName"].ToString();
                        modelScore.ElemID = dt.Rows[c]["ElemID"] == null ? "" : dt.Rows[c]["ElemID"].ToString();
                        modelScore.StepNo = dt.Rows[c]["StepNo"] == null ? "" : dt.Rows[c]["StepNo"].ToString();
                        modelScore.StepName = dt.Rows[c]["StepName"] == null ? "" : dt.Rows[c]["StepName"].ToString();
                        modelScore.ScoreEmployee = dt.Rows[c]["ScoreEmployee"] == null ? "" : dt.Rows[c]["ScoreEmployee"].ToString();
                        modelScore.Rate = dt.Rows[c]["Rate"] == null ? "" : dt.Rows[c]["Rate"].ToString();
                        modleList.Add(modelScore);
                    }
                    PerformanceSummaryModel modelSummary = new PerformanceSummaryModel();
                    modelSummary.CompanyCD = userInfo.CompanyCD;
                    modelSummary.TaskNo = context.Request.QueryString["TaskNo"];
                    modelSummary.TemplateNo = templateNo[i];
                    modelSummary.EmployeeID = employeeID[a];
                    modleSummaryList.Add(modelSummary);
                }
            }
            //执行保存
            bool isSucc = PerformanceTaskWorkBus.SaveProTaskInfo(model, modleList, modleSummaryList);
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
        else if ("SearchInfo".Equals(action))//有用
        {
            //执行查询
            SearchData(context);
        }
        else if ("SearchSurmmaryInfo".Equals(action))//有用
        {
            //执行查询
            SearchSurmmaryInfo(context);
        }
        else if ("SearchSurmmaryCheckInfo".Equals(action))//有用
        {
            //执行查询
            SearchSurmmaryCheckInfo(context);
        }
        else if ("DeleteInfo".Equals(action))
        {
            //获取要素ID
            string elemID = context.Request.QueryString["DeleteNO"];
            //替换引号
            elemID = elemID.Replace("'", "");
            bool isSucc = PerformanceTaskWorkBus.DeletePerTypeInfo(elemID);
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
            
        }
        else if ("GatherInfo".Equals(action))
        {
            GatherInfo(context );
        }
        else if ("GatherSummaryInfo".Equals(action))
        {
            GatherSummaryInfo(context);
        }
        else if ("GatherSummaryCheckInfo".Equals(action))
        {
            GatherSummaryCheckInfo(context);
        }
        else if ("GetSurmarryInfoByTaskNO".Equals(action)) //有用
        {
            //获取要素ID
            string taskNo = context.Request.Params["taskNo"]; 
            //获取考核类型信息
            DataTable dtDeptInfo = PerformanceSummaryBus.GetSurmarryInfoByTaskNO(taskNo);
            int totalLen = dtDeptInfo.Rows.Count;
            string[] temp = new String[totalLen];
            for (int i = 0; i < totalLen; i++)
            {
                temp[i] = dtDeptInfo.Rows[i]["EmployeeID"] == null ? "" : dtDeptInfo.Rows[i]["EmployeeID"].ToString();
            }
            string[] employeeIDList = GetString(temp);
            int employeeCount = employeeIDList.Length;
            int hasFinished = employeeCount;
            for (int a = 0; a < employeeCount; a++)
            {
                DataRow[] dt1 = dtDeptInfo.Select("EmployeeID='" + employeeIDList[a] + "'");
                DataRow[] dt2 = dtDeptInfo.Select("EmployeeID='" + employeeIDList[a] + "' and Status='1'");
                if (dt2.Length != dt1.Length)
                {
                    hasFinished--;
                }
            }
            int notFinish;
            string isCheck = "0";
            if (hasFinished == employeeCount)
            {
                notFinish = 0;
                isCheck = "1";
            }
            else
            {
                notFinish = employeeCount - hasFinished;
            }
            string info = employeeCount.ToString() + "_" + hasFinished.ToString() + "_" + notFinish.ToString() + "_" + isCheck;
            //定义放回变量
            StringBuilder sbReturn = new StringBuilder();
            sbReturn.Append("{");
            sbReturn.Append("data:");
            if (dtDeptInfo.Rows.Count > 0)
            {
                // return "{\"data\":\"" + data + "\",\"info\":\"" + info + "\",\"sta\":" + sta + "}"; 
                sbReturn.Append(JsonClass.DataTable2Json(dtDeptInfo));
                sbReturn.Append(",\"info\":\"" + info + "\"");
            }
            else
            {
                sbReturn.Append("[]");
            }
            sbReturn.Append("}");
            context.Response.ContentType = "text/plain";
            //返回数据
            context.Response.Write(sbReturn.ToString());
            //context.Response.Write(new JsonClass("", sbReturn.ToString(), 0));
            context.Response.End();
        }
        else if ("GetSurmarryInfoByTaskNOEmployeeID".Equals(action)) //有用
        {
            //获取要素ID 
            string taskNo = context.Request.Params["taskNo"];
            string EmployeId = context.Request.Params["EmployeId"];
            string templateNo = context.Request.Params["TemplateNo"];
            //获取考核类型信息

            DataTable dtDeptInfo = PerformanceSummaryBus.GetSurmarryInfoByTaskNOEmployeeID(taskNo, EmployeId, templateNo);
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
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        searchModel.TaskNo = context.Request.QueryString["TaskNo"];
        //启用状态
        searchModel.Title = context.Request.QueryString["Title"];
        if (context.Request.QueryString["TaskFlag"]!="0")
        searchModel.TaskFlag = context.Request.QueryString["TaskFlag"];
        //启用状态
        if (context.Request.QueryString["TaskNum"] != "0")
        searchModel.TaskNum = context.Request.QueryString["TaskNum"];
        if (context.Request.QueryString["TaskDate"] != "0")
            searchModel.TaskDate  = context.Request.QueryString["TaskDate"];
        
        //查询数据
        DataTable dtData = PerformanceSummaryBus.SearchTaskInfo(searchModel);
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
                 StartDate = x.Element("StartDate") == null ? "" : x.Element("StartDate").Value,

                 Status = x.Element("Status") == null ? "" : x.Element("Status").Value,
                 Creator = x.Element("Creator") == null ? "" : x.Element("Creator").Value,
                  CreateDate  = x.Element("CreateDate") == null ? "" : x.Element("CreateDate").Value,
                 SummaryDate = x.Element("SummaryDate") == null ? "" : x.Element("SummaryDate").Value,
                 Summaryer = x.Element("Summaryer") == null ? "" : x.Element("Summaryer").Value,
                 TaskFlag = x.Element("TaskFlag") == null ? "" : x.Element("TaskFlag").Value,
                 EndDate = x.Element("EndDate") == null ? "" : x.Element("EndDate").Value
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
                 StartDate = x.Element("StartDate") == null ? "" : x.Element("StartDate").Value,

                 Status = x.Element("Status") == null ? "" : x.Element("Status").Value,
                 Creator = x.Element("Creator") == null ? "" : x.Element("Creator").Value,
                 CreateDate = x.Element("CreateDate") == null ? "" : x.Element("CreateDate").Value,
                 SummaryDate = x.Element("SummaryDate") == null ? "" : x.Element("SummaryDate").Value,
                 Summaryer = x.Element("Summaryer") == null ? "" : x.Element("Summaryer").Value,
                 TaskFlag = x.Element("TaskFlag") == null ? "" : x.Element("TaskFlag").Value,
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

    private void SearchSurmmaryCheckInfo(HttpContext context)
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
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
            searchModel.TaskFlag = context.Request.QueryString["TaskFlag"];
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
      searchModel.EditFlag = userInfo.EmployeeID.ToString();
        searchModel.CompanyCD = userInfo.CompanyCD;
        //查询数据
        DataTable dtData = PerformanceSummaryBus.SearchSurmmaryCheckInfo (searchModel);

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
                  ModifiedDate  = x.Element("passEmployeeID") == null ? "" : x.Element("passEmployeeID").Value,
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
      string ss=  dsLinq.ToString();
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
    private void SearchSurmmaryInfo(HttpContext context)
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
        PerformanceTaskModel searchModel = new PerformanceTaskModel();
        searchModel.TaskNo = context.Request.QueryString["TaskNo"];
        //启用状态
        searchModel.Title = context.Request.QueryString["Title"];
        if (context.Request.QueryString["TaskFlag"] != "0")
            searchModel.TaskFlag = context.Request.QueryString["TaskFlag"];
        //启用状态
        if (context.Request.QueryString["TaskNum"] != "0")
            searchModel.TaskNum = context.Request.QueryString["TaskNum"];
        if (context.Request.QueryString["taskStatus"] != "0")
            searchModel.Status = context.Request.QueryString["taskStatus"];
        if (context.Request.QueryString["TaskDate"] != "0")
            searchModel.TaskDate  = context.Request.QueryString["TaskDate"];
        
        
        
        searchModel.EditFlag = context.Request.QueryString["employeeID"];
        //查询数据
        DataTable dtData = PerformanceSummaryBus.SearchSurmmaryInfo(searchModel);

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
                  ModifiedDate  = x.Element("passEmployeeID") == null ? "" : x.Element("passEmployeeID").Value,
                  CompanyCD = x.Element("templateName") == null ? "" : x.Element("templateName").Value,
                 EndDate = x.Element("AdviceType") == null ? "" : x.Element("AdviceType").Value,
                 CompleteDate = x.Element("TemplateNo") == null ? "" : x.Element("TemplateNo").Value
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
                 CompleteDate = x.Element("TemplateNo") == null ? "" : x.Element("TemplateNo").Value
             });
        //获取记录总数
      string ss=  dsLinq.ToString();
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
    public static string[] GetString(string[] myData)
    {
        if (myData.Length > 0)
        {
            Array.Sort(myData); //先对数组进行排序
            int size = 1;  //定义删除重复项后的数组长度 
            for (int i = 1; i < myData.Length; i++)
                if (myData[i] != myData[i - 1])
                    size++;
            String[] myTempData = new String[size];
            int j = 0;

            myTempData[j++] = myData[0];

            for (int i = 1; i < myData.Length; i++) //遍历数组成员 
                if (myData[i] != myData[i - 1])  //如果相邻的两个不相等则放入新数组
                    myTempData[j++] = myData[i];

            return myTempData;
        }
        return myData;

    }
    protected void GatherSummaryInfo(HttpContext context)
    {
        string taskNo = context.Request.QueryString["TaskNo"];
        string EmployeeID = context.Request.QueryString["EmployeeID"];
        string TemplateNo = context.Request.QueryString["TemplateNo"];
        string KillScore = context.Request.QueryString["KillScore"];
        string AddScore = context.Request.QueryString["AddScore"];
        string AdviceType = context.Request.QueryString["AdviceType"];
        string LevelType = context.Request.QueryString["LevelType"];
        string SummaryNote = context.Request.QueryString["SummaryNote"];
        string SumarryRemark = context.Request.QueryString["SumarryRemark"];
        string RewardNote = context.Request.QueryString["RewardNote"];
        string AdviceNote = context.Request.QueryString["AdviceNote"];
        string TotalScore = context.Request.QueryString["TotalScore"];
        
          UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        PerformanceSummaryModel model = new PerformanceSummaryModel();
        model.AddScore = Math.Round(Convert.ToDecimal(AddScore), 0).ToString ();
        model.AdviceNote = AdviceNote;
        model.AdviceType = AdviceType;
        model.CompanyCD = userInfo.CompanyCD;
        model.Evaluater = userInfo.EmployeeID.ToString();
        model.KillScore = Math.Round(Convert.ToDecimal(KillScore), 0).ToString ();
        model.LevelType = LevelType;
        model.ModifiedUserID = userInfo.EmployeeID.ToString(); ;
        model.Remark = SumarryRemark;
        model.RewardNote = RewardNote;
        model.SummaryNote = SummaryNote;
        model.TaskNo = taskNo;
        model.TemplateNo = TemplateNo;
        model.EmployeeID = EmployeeID;
       decimal RealScoreTemp=  Convert.ToDecimal(TotalScore) -Math .Round ( Convert.ToDecimal(KillScore),0) + Math .Round ( Convert.ToDecimal(AddScore),0);
       int RealScore = Convert.ToInt32(Math.Round(RealScoreTemp, 0));
       model.RealScore = RealScore.ToString ();

       PerformanceTaskModel taskModel2 = new PerformanceTaskModel();
       if (PerformanceSummaryBus.GatherSummaryInfo(taskModel2, model, false))
        {
            bool sign = true;
            DataTable dtSummary = PerformanceSummaryBus.CheckSummary(taskNo);
            int suumaRowsLen = dtSummary.Rows.Count;
            for (int a = 0; a < suumaRowsLen; a++)
            {
                string ss = dtSummary.Rows[a]["RealScore"].ToString();
                if (dtSummary.Rows[a]["RealScore"] == null | string.IsNullOrEmpty(dtSummary.Rows[a]["RealScore"].ToString()))
                {
                    sign = false;
                    break;
                }
                if (dtSummary.Rows[a]["TotalScore"] == null | string.IsNullOrEmpty(dtSummary.Rows[a]["TotalScore"].ToString()))
                {
                    sign = false;
                    break;
                }

            }
            PerformanceTaskModel taskModel = new PerformanceTaskModel();
            if (sign)
            {
                taskModel.Status = "3";//已完成
                taskModel.TaskNo = taskNo;
                taskModel.ModifiedUserID = userInfo.EmployeeID.ToString();
                taskModel.CompanyCD = userInfo.CompanyCD;
                PerformanceSummaryBus.UpdateTaskStatusInfo(taskModel);
            
            }
            context.Response.Write(new JsonClass("1", "", 1));
        }
        else
        {
            context.Response.Write(new JsonClass("", "", 0));
        }
        
    }

    protected void GatherSummaryCheckInfo(HttpContext context)
    {
        string taskNo = context.Request.QueryString["TaskNo"];
        string EmployeeID = context.Request.QueryString["EmployeeID"];
        string TemplateNo = context.Request.QueryString["TemplateNo"];
        string SignNote = context.Request.QueryString["SignNote"];
        
          UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        PerformanceSummaryModel model = new PerformanceSummaryModel();
      
        model.CompanyCD = userInfo.CompanyCD;
        model.ModifiedUserID = userInfo.EmployeeID.ToString();
        model.SignNote = SignNote;
        model.TaskNo = taskNo;
        model.TemplateNo = TemplateNo;
        model.EmployeeID = EmployeeID;
        if (PerformanceSummaryBus.GatherSummaryCheckInfo(model))
            {
                context.Response.Write(new JsonClass("1", "", 1));
            }
            else
            {
                context.Response.Write(new JsonClass("", "", 0));
            }
        
    }
    protected void GatherInfo(HttpContext context)
    {
        string taskNo = context.Request.QueryString["TaskNo"];
        DataTable dtDeptInfo = PerformanceSummaryBus.SearchTaskInfoByTaskNO(taskNo);

        int totalLen = dtDeptInfo.Rows.Count;
        string[] temp = new String[totalLen];
            string[] temp3 = new String[totalLen];
        for (int i = 0; i < totalLen; i++)
        {
            temp[i] = dtDeptInfo.Rows[i]["EmployeeID"] == null ? "" : dtDeptInfo.Rows[i]["EmployeeID"].ToString();
            temp3[i] = dtDeptInfo.Rows[i]["TemplateNo"] == null ? "" : dtDeptInfo.Rows[i]["TemplateNo"].ToString();
        }

        string[] employeeIDList = GetString(temp);
        int employeeCount = employeeIDList.Length;
        string[] TemplateNoList = GetString(temp3);
        int TempalteNoLengh = TemplateNoList.Length;
        
        int hasFinished = employeeCount;
        IList<PerformanceSummaryModel> summaryList = new List <PerformanceSummaryModel>();
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        for (int a = 0; a < employeeCount; a++)
        {
            for (int b = 0; b < TempalteNoLengh; b++)
            {
                DataTable dtNew = GetNewDataTable(dtDeptInfo, "EmployeeID='" + employeeIDList[a] + "' and TemplateNo='" + TemplateNoList [b]+ "'");
                int rowsLen = dtNew.Rows.Count;
                int sum = 0;
                decimal tempSum = 0;
                PerformanceSummaryModel summaryModel = new PerformanceSummaryModel();
                summaryModel.TemplateNo = TemplateNoList[b].ToString();
           
                for (int i = 0; i < rowsLen; i++)
                {
                    decimal elemRate = Convert.ToDecimal(dtNew.Rows[i]["ElemRate"] == null ? "0" : dtNew.Rows[i]["ElemRate"].ToString());
                    decimal temp1 = Convert.ToDecimal(dtNew.Rows[i]["Score"] == null ? "0" : dtNew.Rows[i]["Score"].ToString());
                    decimal temp2 = Convert.ToDecimal(dtNew.Rows[i]["StepRate"] == null ? "0" : dtNew.Rows[i]["StepRate"].ToString());
         
                    tempSum = tempSum + temp1 * elemRate * temp2 / 10000;
                }


                sum = Convert.ToInt32(Math.Round(tempSum, 0));
                summaryModel.CompanyCD = userInfo.CompanyCD;
                summaryModel.ModifiedUserID = userInfo.EmployeeID.ToString();
                summaryModel.TotalScore = sum.ToString();
                summaryModel.EmployeeID = employeeIDList[a].ToString();
                summaryModel.TaskNo = taskNo;
                summaryList.Add(summaryModel);
            }
        }
        PerformanceTaskModel taskModel = new PerformanceTaskModel();
        taskModel.Status = "2";//待总评
        taskModel.Summaryer = userInfo.EmployeeID.ToString();
        taskModel.TaskNo = taskNo;
        taskModel.ModifiedUserID = userInfo.EmployeeID.ToString();
        taskModel .CompanyCD =userInfo.CompanyCD ;
       if (PerformanceSummaryBus.GatherInfo(taskModel, summaryList))
        {
            context.Response.Write(new JsonClass("1", "", 1));
        }
        else
        {
            context.Response.Write(new JsonClass("", "", 0));
        }
    }

    private DataTable GetNewDataTable(DataTable dt, string condition)
    {
        DataTable newdt = new DataTable();
        newdt = dt.Clone();
        DataRow[] dr = dt.Select(condition);
        for (int i = 0; i < dr.Length; i++)
        {
            newdt.ImportRow((DataRow)dr[i]);
        }
        return newdt;//返回的查询结果
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
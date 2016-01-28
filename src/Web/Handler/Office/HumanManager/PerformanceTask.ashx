<%@ WebHandler Language="C#" Class="PerformanceTask" %>

/**********************************************
 * 作    者： 王保军
 * 创建日期： 2009/05/05
 * 描    述： 考核任务
 * 修改日期： 2009/05/05
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


public class PerformanceTask : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
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
            DataTable dtDeptInfo = PerformanceTaskWorkBus.GetEmployeeInfo ();
           
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
            DataTable dtDeptInfo = PerformanceTaskWorkBus.SearchTaskInfoByTaskNO (taskNo );

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
         string[] temp = templateNo.Split(',');
         string outerString = string.Empty;
         for (int i = 0; i < temp.Length; i++)
         {
             if (i == temp.Length - 1)
             {
                 outerString =  outerString +"'" +Convert .ToString ( temp[i])+ "'"; 
             }
             else
             {
                 outerString = outerString + "'" + Convert.ToString(temp[i]) + "'" + ","; 
             }
         }
            //获取考核类型信息
         DataTable dtDeptInfo = PerformanceTaskWorkBus.GetEmployeeInfoByTemplateNo(outerString);

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
            model.EditFlag  = context.Request.QueryString["EditFlag"];
            //类型ID 
            model.Title  = context.Request.QueryString["Title"];
            //类型名称
            model.StartDate = context.Request.QueryString["StartDate"];
            //启用状态
            model.EndDate  = context.Request.QueryString["EndDate"];
           
            string flag=context.Request.QueryString["TaskFlag"];
            model.TaskFlag = flag;
            if (flag == "4" || flag == "5")
            {
              //  model.TaskNum = DateTime.Now.Year.ToString();
                model.TaskDate = DateTime.Now.Year.ToString();
            }
            else
            {
                model.TaskDate = context.Request.QueryString["TaskDate"];
                model.TaskNum = context.Request.QueryString["TaskNum"];
            }
         //   model.TaskDate = DateTime.Now.Year.ToString();


            model.Status = "1";//待评分
            model.Remark  = context.Request.QueryString["Remark"];
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            model.Creator = userInfo.EmployeeID.ToString();
            model.ModifiedUserID = userInfo.EmployeeID.ToString();
            model.TaskNo =context.Request.QueryString["TaskNo"];
            if (model.EditFlag == "INSERT")
            {
                if (string.IsNullOrEmpty(model.TaskNo))
                {
                    //获取编码规则编号
                    string codeRuleID = context.Request.QueryString["CodeRuleID"];
                    //通过编码规则代码获取人员编码
                    model.TaskNo = ItemCodingRuleBus.GetCodeValue(codeRuleID);
                }
                if (PerformanceTaskWorkBus.IsExist(model.TaskNo))
                {
                    context.Response.Write(new JsonClass("", "", 2));
                    return;
                }
            }
            if (model.EditFlag == "UPDATE")
            {
                if (!IsCheckED(model.TaskNo))
                {
                    context.Response.Write(new JsonClass("", "", 3));
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

                    DataTable dt = PerformanceTaskWorkBus.GetTemplatebyNO(templateNo[i], employeeID[a].ToString ());
                    if (dt.Rows.Count <0)
                    { return ;}
                    for (int c=0;c<dt.Rows.Count ;c++)
                    {   PerformanceScoreModel modelScore = new PerformanceScoreModel();
                        modelScore.EditFlag = context.Request.QueryString["EditFlag"];
                        modelScore.TaskNo = model.TaskNo;
                        modelScore.TemplateNo = templateNo[i];
                        modelScore.EmployeeID = employeeID[a];
                        modelScore.CompanyCD = userInfo.CompanyCD;
                        modelScore.ElemName = dt.Rows[c]["ElemName"] == null ? "" : dt.Rows[c]["ElemName"].ToString();
                        modelScore.ElemID = dt.Rows[c]["ElemID"] == null ? "" : dt.Rows[c]["ElemID"].ToString();
                        modelScore.StepNo = dt.Rows[c]["StepNo"] == null ? "" : dt.Rows[c]["StepNo"].ToString();
                        modelScore.StepName = dt.Rows[c]["StepName"] == null ? "" : dt.Rows[c]["StepName"].ToString();
                        modelScore.ScoreEmployee = dt.Rows[c]["ScoreEmployee"] == null ? "" : dt.Rows[c]["ScoreEmployee"].ToString();
                        modelScore.Rate = dt.Rows[c]["Rate"] == null ? "" : dt.Rows[c]["Rate"].ToString();
                        modelScore .Status ="0";//表示草稿状态
                        modleList.Add(modelScore );
                      
                        
                    }
                    PerformanceSummaryModel modelSummary = new PerformanceSummaryModel();
                    modelSummary.CompanyCD = userInfo.CompanyCD;
                    modelSummary.TaskNo = model.TaskNo;
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
                context.Response.Write(new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, model.TaskNo , 1));
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
            string failureList = string.Empty;
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
            bool isSucc = true;
            string [] elemIDList=elemID .Split (',');

            for (int i = 0; i < elemIDList.Length; i++)
            {
                if (context.Request.QueryString["sign"] == "0")
                {
                    if (!IsCheckED(elemIDList[i]))
                    {
                        failureList += elemIDList[i] + ",";
                        continue;
                    }
                }
                isSucc = PerformanceTaskWorkBus.DeletePerTypeInfo(elemIDList[i]);
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
            //删除成功
            if (isSucc)
            {
                //输出响应
                context.Response.Write(new JsonClass(failureList, "", 1));
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
    public bool  IsCheckED(string taskNo)
    {
        DataTable dtDeptInfo = PerformanceSummaryBus.SearchTaskInfoByTaskNO(taskNo);
        int totalLen = dtDeptInfo.Rows.Count;
        string[] temp = new String[totalLen];
        for (int i = 0; i < totalLen; i++)
        {
            temp[i] = dtDeptInfo.Rows[i]["EmployeeID"] == null ? "" : dtDeptInfo.Rows[i]["EmployeeID"].ToString();
        }
        string[] employeeIDList = GetString(temp);
        int employeeCount = employeeIDList.Length;


        for (int i = 0; i < totalLen; i++)
        {
            temp[i] = dtDeptInfo.Rows[i]["ScoreEmployee"] == null ? "" : dtDeptInfo.Rows[i]["ScoreEmployee"].ToString();
        }
        string[] scoreIDList = GetString(temp);
        int scoreCount = scoreIDList.Length;
        int hasFinished = scoreCount;
        for (int a = 0; a < scoreCount; a++)
        {
            DataRow[] dt1 = dtDeptInfo.Select("ScoreEmployee='" + scoreIDList[a] + "'");
            DataRow[] dt2 = dtDeptInfo.Select("ScoreEmployee='" + scoreIDList[a] + "' and Status='1'");
            if (dt2.Length != dt1.Length)
            {
                hasFinished--;
            }
        }
        int notFinish;
        if (hasFinished == scoreCount)
        {
            notFinish = 0;
        }
        else
        {
            notFinish = scoreCount - hasFinished;
        }
        if (scoreCount == notFinish)
        {
            return true ;
        }
        else
        {
            return false; 
        }
        
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
        //设置查询条件
        //要素名称
        //search += "TaskNo=" + document.getElementById("txtSearchTaskNo").value;
        ////启用状态
        //search += "&Title=" + document.getElementById("inptTitle").value;
        //search += "&TaskFlag=" + document.getElementById("selTaskFlag").value;
        //search += "&TaskNum=" + document.getElementById("selTaskNum").value;


        searchModel.TaskNo  = context.Request.QueryString["TaskNo"];
        //启用状态
        searchModel.Title  = context.Request.QueryString["Title"];
        if (context.Request.QueryString["TaskFlag"] != "0")
        searchModel.TaskFlag  = context.Request.QueryString["TaskFlag"];
        //启用状态
        if (context.Request.QueryString["TaskNum"] != "0")
        searchModel.TaskNum   = context.Request.QueryString["TaskNum"];
        if (context.Request.QueryString["TaskDate"] != "0")
        searchModel.TaskDate = context.Request.QueryString["TaskDate"];
        //查询数据
        DataTable dtData = PerformanceTaskWorkBus.SearchTaskInfo(searchModel);
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
                 Remark = x.Element("TaskFlag") == null ? "" : x.Element("TaskFlag").Value,
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
                 Remark = x.Element("TaskFlag") == null ? "" : x.Element("TaskFlag").Value,
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
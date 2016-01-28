<%@ WebHandler Language="C#" Class="PerformanceCheck" %>
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
using System.Collections;


public class PerformanceCheck : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取请求动作
        string action = context.Request.Params["action"];
        //保存评分项目信息  
        //初期化评分项目树时
        
       if ("GetTaskInfoByTaskNO".Equals(action))
        {
             
            //获取要素ID
            string taskNo = context.Request.Params["taskNo"];
            string employeeID = context.Request.Params["EmployeeID"];
            string templateNo = context.Request.Params["templateNo"];
            string totalCount = string.Empty;
            string checkName = string.Empty;
            string stepTempNo = string.Empty;
            ArrayList checker = new ArrayList();
            //获取考核类型信息
            DataTable dtCheckStep = PerformanceCheckBus.CheckTaskStep(taskNo, employeeID, templateNo);
            ArrayList CheckStep = new ArrayList();
           string StepNo=string .Empty ;
            for (int i = 0; i < dtCheckStep.Rows.Count; i++)
            {
                string temp1 = dtCheckStep.Rows[i]["StepNo"].ToString() +"|"+ dtCheckStep.Rows[i]["Status"].ToString();
                string temp2 = dtCheckStep.Rows[i]["ScoreEmployee"].ToString();
                if (!CheckStep.Contains(temp1))
                {
                    CheckStep.Add(temp1);
                }
                if (!checker.Contains(temp2))
                {
                    checker.Add(temp2);
                }
            }
            DataTable dtCheckerGet = PerformanceCheckBus.SearchTaskInfoByTaskNO(taskNo, employeeID, templateNo);
           ArrayList checkerGet=new ArrayList ();
           for (int i = 0; i < dtCheckerGet.Rows.Count; i++)
           {
               string temp = dtCheckerGet.Rows[i]["StepNo"].ToString() +"|"+ dtCheckerGet.Rows[i]["Status"].ToString();
               if (!checkerGet .Contains (temp ))
               {   
                   checkerGet.Add(temp );
               }
           }
           CheckStep.Sort();
           checkerGet.Sort();
    
           
           
           for (int c = 0; c < checkerGet.Count; c++)
           {
               string[] CheckGet = checkerGet[c].ToString().Split('|');
               if (CheckGet[1] == "1")
               {
                   continue;
               }
               else
               {
                   bool sign = false;
                   if (CheckGet[0] == "1")
                   {
                       StepNo = CheckGet[0];
                       break ;
                   }
                   else
                   {
                       int temp = Convert.ToInt32(CheckGet[0]) - 1;
                       for (int d = 0; d < CheckStep.Count; d++)
                       {
                          string [] stepTemp= CheckStep[d].ToString().Split('|');
                           if (stepTemp [0]==temp .ToString ())
                           {
                               if (stepTemp[1]=="1")
                               {
                                   
                                   StepNo = CheckGet[0];
                                   sign = true;
                                   break;
                               }
                               else
                               {
                                   StepNo =string .Empty ;
                                   sign = true;
                                   break ;
                               }
                           }
                       }
                       if (sign)
                       {
                           break; 
                       }
                       
                       
                   }
                   
                   
               }
               
              
              
           }

           DataTable dtDeptInfo = new DataTable();
           DataTable dtNew = new DataTable();
         string info = StepNo;
         string sta = string.Empty;
           if (!string.IsNullOrEmpty(StepNo))
           {
                 dtDeptInfo = GetNewDataTable(dtCheckerGet, "StepNo='" + StepNo + "'");
               sta = "1";
           }
           else
           {
               sta = "0";
              dtNew= PerformanceTaskWorkBus.SearchTaskForDetails (taskNo );
              string step = string.Empty;
              foreach (string sd in checkerGet)
              {
                  string[] sdTem = sd.Split('|');
                  if (checkerGet.Count ==1)
                  {
                      step = step + sdTem[0] ;
                  }
                  else
                  {
                      step = step + sdTem[0] + ",";
                  }
                 
              }

             // totalCount = "评分人数:" + checker.Count.ToString() + "个" + "&nbsp;&nbsp;&nbsp;" + "您的评分步骤为:" + step + "&nbsp;&nsbp;&nbsp&nbsp;前一步骤考评人未打分,相关信息请参照考评打分记录项";
              totalCount = checker.Count.ToString();
              stepTempNo = step;
           }
     
            //定义放回变量
            StringBuilder sbReturn = new StringBuilder();
            sbReturn.Append("{");
            sbReturn.Append("data:");
            if (dtDeptInfo.Rows.Count > 0 && dtDeptInfo !=null )
            {
                sbReturn.Append(JsonClass.DataTable2Json(dtDeptInfo));
            }
            else
            {
                sbReturn.Append(JsonClass.DataTable2Json(dtNew));
            }
           // return "{\"data\":\"" + data + "\",\"info\":\"" + info + "\",\"sta\":" + sta + "}";
            sbReturn.Append(",info:[" + info + "]");
            sbReturn.Append(",sta:[" + sta+"]" );
            sbReturn.Append(",message:[" + totalCount  + "]");
            sbReturn.Append(",Step:[" + stepTempNo + "]");
            sbReturn.Append("}");
            context.Response.ContentType = "text/plain";
            //返回数据
            context.Response.Write(sbReturn.ToString());
            context.Response.End();
        }
        else if ("EditInfo".Equals(action))
        {
            
            //启用状态 
            string elem = context.Request.QueryString["elemList"];

            string[] elemList = elem.Split(',');
      string stepNo= context.Request.QueryString["stepNo"];

            int leng = elemList.Length;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           IList <  PerformanceScoreModel> modellist=new List <PerformanceScoreModel >();
           for (int i = 0; i < leng; i++)
           {
               PerformanceScoreModel model = new PerformanceScoreModel();
               //编辑模式
            //  model.EditFlag = context.Request.QueryString["EditFlag"];
               //类型ID 
               model.AdviceNote = context.Request.QueryString["AdviceNote"];
               //类型名称
               model.Note = context.Request.QueryString["Note"];
               model.TaskNo = context.Request.QueryString["TaskNo"];
               model.TemplateNo = context.Request.QueryString["templateNo"];
               string[] infoList = elemList[i].Split('_');
              int  trueScore= Convert.ToInt32 (infoList[0]);
              decimal rate = Convert.ToDecimal(infoList[2]);

              model.Score = trueScore.ToString();
                model.ElemID = infoList[1];
               model.ScoreEmployee = userInfo.EmployeeID.ToString ();
               model.CompanyCD = userInfo.CompanyCD;
               model.EmployeeID = context.Request.QueryString["EmployeId"];
               model.ModifiedUserID = userInfo.EmployeeID.ToString();
               model.EditFlag = "UPDATE";
               model.Status = context.Request.QueryString["Status"];
               model.StepNo = stepNo;
               modellist.Add(model );
               
           }
            
            
            

            ////执行保存
           bool isSucc = PerformanceCheckBus.SaveProScoreInfo(modellist); 
           //成功
           if (isSucc)
           {
               DataTable dtDeptInfo = PerformanceSummaryBus.SearchTaskInfoByTaskNO(context.Request.QueryString["TaskNo"]);
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
               if (notFinish == 0 &&  context.Request.QueryString["Status"]=="1")
               {
                   if (PerformanceCheckBus.UpdateTask(context.Request.QueryString["TaskNo"]))
                   {
                       context.Response.Write(new JsonClass(context.Request.QueryString["Status"], modellist[0].TaskNo, 1));
                   }
                   else
                   {
                       context.Response.Write(new JsonClass(context.Request.QueryString["Status"], modellist[0].TaskNo, 1));
                   }
               }
               else
               {
                   context.Response.Write(new JsonClass(context.Request.QueryString["Status"], modellist[0].TaskNo, 1));
               }
               
               
               
           
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
       else if ("GetStepInfoByTaskNO".Equals(action))
       {

           //获取要素ID
           string taskNo = context.Request.Params["taskNo"];
           string employeeID = context.Request.Params["EmployeeID"];
           string stepNo = context.Request.Params["StepNo"];
           string templateNo = context.Request.Params["templateNo"];
           DataTable dtDeptInfo = PerformanceCheckBus.SearchStepInfoByTaskNO(taskNo, employeeID, stepNo, templateNo);
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
           // return "{\"data\":\"" + data + "\",\"info\":\"" + info + "\",\"sta\":" + sta + "}";
          // sbReturn.Append(",info:" + info);
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
        //设置查询条件
        //要素名称
        //search += "TaskNo=" + document.getElementById("txtSearchTaskNo").value;
        ////启用状态
        //search += "&Title=" + document.getElementById("inptTitle").value;
        //search += "&TaskFlag=" + document.getElementById("selTaskFlag").value;
        //search += "&TaskNum=" + document.getElementById("selTaskNum").value;


        searchModel.TaskNo = context.Request.QueryString["TaskNo"];
        //启用状态
        searchModel.Title = context.Request.QueryString["Title"];
        searchModel.TaskFlag = context.Request.QueryString["TaskFlag"];
        //启用状态
        searchModel.TaskNum = context.Request.QueryString["TaskNum"];

        //查询数据
        DataTable dtData = PerformanceCheckBus.SearchTaskInfo(searchModel);
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
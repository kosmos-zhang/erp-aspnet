/**********************************************
 * 类作用：  考核自我鉴定操作
 * 建立人：   王保军
 * 建立时间： 2009/05/10
 ***********************************************/
using System;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using XBase.Common;
using System.Collections;
using System.Collections.Generic;

namespace XBase.Data.Office.HumanManager
{
   public  class PerformanceSummaryDBHelper
    {

       public static DataTable GetSurmarryInfoByTaskNO(string companyCD, string taskNo)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("select a.TemplateNo,a.Score,isnull(a.AdviceNote,'') as AdviceNote,isnull(a.Note,'') as Note,");
           searchSql.AppendLine("e.Rate as ElemRate,a.Rate as StepRate, a.Status, a.EmployeeID,");
           searchSql.AppendLine("isnull( d.ElemName,'') as ParentElemName,");
           searchSql.AppendLine("a.ElemID,isnull(a.ElemName,'') as ElemName,isnull(b.ScoreRules,'') as ScoreRules,b.StandardScore,b.MinScore,b.MaxScore,");
           searchSql.AppendLine("isnull(b.AsseStandard,'') as AsseStandard,isnull(b.AsseFrom,'') as AsseFrom,isnull(b.Remark,'') as  Remark,");
           searchSql.AppendLine("c.TaskNo,isnull(c.Title,'') as Title,c.TaskFlag,c.TaskNum,CONVERT(VARCHAR(10),c.StartDate,21) AS StartDate ,");
           searchSql.AppendLine("CONVERT(VARCHAR(10),c.EndDate,21) AS EndDate,c.Remark,CONVERT(VARCHAR(10),c.CreateDate,21) AS CreateDate,");
           searchSql.AppendLine("case when c.Status='1' then '待评分'");
           searchSql.AppendLine("when c.Status='2' then '待总评'");
           searchSql.AppendLine("when c.Status='3' then '已完成'");
           searchSql.AppendLine("when c.Status='4' then '待汇总'");
           searchSql.AppendLine("end as StatusName");
           searchSql.AppendLine(",CONVERT(VARCHAR(10),c.SummaryDate,21) AS SummaryDate,c.Status as TaskStaus,isnull(f.EmployeeName,'') as Creator,isnull(i.EmployeeName,'') as Summaryer,isnull(h.EmployeeName,'') as Evaluater,CONVERT(VARCHAR(10),g.EvaluateDate,21) AS EvaluateDate,g.TotalScore,isnull(g.RewardNote,'') as RewardNote,g.KillScore,g.AddScore,g.RealScore ");
           searchSql.AppendLine(",isnull(g.SummaryNote,'') as SummaryNote,isnull(g.AdviceNote,'') as SummaryAdviceNote, isnull(g.Remark,'') as Remark, ");
           searchSql.AppendLine(" 	CASE when g.AdviceType='1' then '不做处理'");
           searchSql.AppendLine(" 	     when g.AdviceType='2' then '调整薪资'");
           searchSql.AppendLine(" 	     when g.AdviceType='3' then '晋升'");
           searchSql.AppendLine(" 	     when g.AdviceType='4' then '调职'");
           searchSql.AppendLine(" 	     when g.AdviceType='5' then '辅导'");
           searchSql.AppendLine(" 	     when g.AdviceType='6' then '培训'");
           searchSql.AppendLine(" 	     when g.AdviceType='7' then '辞退'");
           searchSql.AppendLine(" 	     when g.AdviceType is null then ''");
           searchSql.AppendLine(" 	 end as AdviceType                            ");
           searchSql.AppendLine(" 	,CASE when g.LevelType='1' then '达到要求'");
           searchSql.AppendLine(" 	     when g.LevelType='2' then '超过要求'");
           searchSql.AppendLine(" 	     when g.LevelType='3' then '表现突出'");
           searchSql.AppendLine(" 	     when g.LevelType='4' then '需要改进'");
           searchSql.AppendLine(" 	     when g.LevelType='5' then '不合格'");
           searchSql.AppendLine(" 	     when g.LevelType is null  then ' '");
           searchSql.AppendLine(" 	 end as LevelType  ,CONVERT(VARCHAR(10),g.SignDate,21) AS SignDate,isnull(g. SignNote,'') as SignNote   ");
           searchSql.AppendLine(" from officedba.PerformanceScore a ");
           searchSql.AppendLine(" left outer join  officedba.PerformanceElem b ");
           searchSql.AppendLine("on  b.CompanyCD=a.CompanyCD and a.ElemID=b.ElemNo ");
           searchSql.AppendLine("left outer join officedba.PerformanceTask c");
           searchSql.AppendLine(" on  c.CompanyCD=a.CompanyCD and a.TaskNo=c.TaskNo  ");
           searchSql.AppendLine("left outer join officedba.PerformanceElem d ");
           searchSql.AppendLine(" on d.CompanyCD=a.CompanyCD  and  b.ParentElemNo=d.ElemNo  left outer join officedba.PerformanceTemplateElem e ");
           searchSql.AppendLine("on e.CompanyCD=a.CompanyCD and a.TemplateNo=e.TemplateNo and a.ElemID=e.ElemID  ");
           searchSql.AppendLine("left outer join officedba.EmployeeInfo f");
           searchSql.AppendLine("on f.CompanyCD=a.CompanyCD and c.Creator=f.ID left outer join officedba.PerformanceSummary g on  g.CompanyCD=a.CompanyCD  and c.TaskNo=g.TaskNo  left outer join officedba.EmployeeInfo h on  h.CompanyCD=a.CompanyCD and g.Evaluater=h.ID");
           searchSql.AppendLine("left outer join officedba.EmployeeInfo i on i.CompanyCD=a.CompanyCD and  c.Summaryer=i.ID ");
           searchSql.AppendLine(" where	a.CompanyCD = @CompanyCD            ");
           searchSql.AppendLine(" and 	a.TaskNo = @TaskNo          ");
           #endregion
           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", taskNo));

           //启用状态

           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       public static DataTable GetSurmarryInfoByTaskNOEmployeeID(string companyCD, string taskNo,string employeeID,string templateNo)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("select a.TaskNo,a.TotalScore,isnull(a.RewardNote,'') as RewardNote,a.KillScore,a.AddScore,a.RealScore,isnull(a.SummaryNote,'') as SummaryNote,a.LevelType,a.AdviceType,isnull(a.AdviceNote,'') as SummaryAdviceNote,isnull(a.Remark,'') as Remark,isnull(a.SignNote,'') as  SignNote from officedba.PerformanceSummary a where a.CompanyCD=@CompanyCD  and a.TaskNo=@TaskNo and TemplateNo=@TemplateNo  and a.EmployeeID=@EmployeeID ");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", taskNo));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", employeeID));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", templateNo ));

           //启用状态

           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       public static DataTable CheckSummary(string companyCD, string taskNo)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("select a.TaskNo,a.TotalScore,a.RealScore from officedba.PerformanceSummary a where  a.CompanyCD=@CompanyCD  and a.TaskNo=@TaskNo ");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", taskNo));

           //启用状态

           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }

       public static DataTable SearchTaskInfoByTaskNO(string companyCD, string taskNo,string tempalteNo, string  EmployeId)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("select a.TemplateNo,a.Score,isnull(a.AdviceNote,'') as AdviceNote,isnull(a.Note,'') as Note,");
           searchSql.AppendLine("e.Rate as ElemRate,a.Rate as StepRate, a.Status, a.EmployeeID,");
           searchSql.AppendLine("isnull( d.ElemName,'') as ParentElemName,");
            searchSql.AppendLine("a.ElemID,isnull(a.ElemName,'') as ElemName,isnull(b.ScoreRules,'') as ScoreRules,b.StandardScore,b.MinScore,b.MaxScore,");
           searchSql.AppendLine("isnull(b.AsseStandard,'') as AsseStandard,isnull(b.AsseFrom,'') as AsseFrom,isnull(b.Remark,'') as ElemRemark ,");
           searchSql.AppendLine("c.TaskNo,isnull(c.Title,'') as Title,CONVERT(VARCHAR(10),c.StartDate,21) AS StartDate ,CONVERT(VARCHAR(10),c.SummaryDate,21) AS SummaryDate");
           searchSql.AppendLine("     	,CASE when c.TaskFlag='2' and c.TaskNum='1' then c.TaskDate+'年第一季度'");
           searchSql.AppendLine("    	  when c.TaskFlag='2' and c.TaskNum='2' then c.TaskDate+'年第二季度'");
           searchSql.AppendLine("    	  when c.TaskFlag='2' and c.TaskNum='3' then c.TaskDate+'年第三季度'");
           searchSql.AppendLine("      	  when c.TaskFlag='2' and c.TaskNum='4' then c.TaskDate+'年第四季度'");
           searchSql.AppendLine("   	  when c.TaskFlag='3' and c.TaskNum='1' then c.TaskDate+'年上半年'");
           searchSql.AppendLine("    when c.TaskFlag='3' and c.TaskNum='2' then c.TaskDate+'年下半年'");
           searchSql.AppendLine("     when c.TaskFlag='1' then c.TaskDate+'年'+cast(c.TaskNum as varchar)+'月' ");
           searchSql.AppendLine("     when c.TaskFlag='4' then c.TaskDate+'年'+isnull( cast(c.TaskNum as varchar),'') ");
           searchSql.AppendLine("   when c.TaskFlag='5' then isnull(cast(TaskNum as varchar),'') ");
           searchSql.AppendLine("   when c.TaskFlag is null then ''");
           searchSql.AppendLine("    end as TaskNum                                 ");
           searchSql.AppendLine("   ,CASE when c.TaskFlag='1'  then '月考核'");
           searchSql.AppendLine("     when c.TaskFlag='2'  then '季考核'");
           searchSql.AppendLine("     when c.TaskFlag='3'  then '半年考核'");
           searchSql.AppendLine("     when c.TaskFlag='4'  then '年考核'");
           searchSql.AppendLine("   	  when c.TaskFlag='5'  then '临时考核'");
           searchSql.AppendLine("   	  when c.TaskFlag is null  then ''");
           searchSql.AppendLine("   	 end as TaskFlag,     ");
           searchSql.AppendLine("CONVERT(VARCHAR(10),c.EndDate,21) AS EndDate ,isnull(c.Remark,'') as Remark,CONVERT(VARCHAR(10),c.CreateDate,21) AS CreateDate,");
           searchSql.AppendLine("case when c.Status='1' then '待评分'");
           searchSql.AppendLine("when c.Status='2' then '待总评'");
           searchSql.AppendLine("when c.Status='3' then '已完成'");
           searchSql.AppendLine("when c.Status='4' then '待汇总'");
           searchSql.AppendLine("when c.Status is null then ' '");
           searchSql.AppendLine("end as StatusName");
           searchSql.AppendLine(",c.Status as TaskStaus,isnull(f.EmployeeName,'') as Creator ,isnull(h.EmployeeName,'') as Summaryer ,a.ScoreEmployee");
           searchSql.AppendLine(" from officedba.PerformanceScore a ");
           searchSql.AppendLine(" left outer join  officedba.PerformanceElem b ");
           searchSql.AppendLine("on    b.CompanyCD=a.CompanyCD and  a.ElemID=b.ID");
           searchSql.AppendLine("left outer join officedba.PerformanceTask c");
           searchSql.AppendLine(" on  c.CompanyCD=a.CompanyCD and a.TaskNo=c.TaskNo  ");
           searchSql.AppendLine("left outer join officedba.PerformanceElem d ");
           searchSql.AppendLine(" on  d.CompanyCD=a.CompanyCD and b.ParentElemNo=d.ID left outer join officedba.PerformanceTemplateElem e ");
           searchSql.AppendLine("on   e.CompanyCD=a.CompanyCD and a.TemplateNo=e.TemplateNo and a.ElemID=e.ElemID ");
           searchSql.AppendLine("left outer join officedba.EmployeeInfo f");
           searchSql.AppendLine("on f.CompanyCD=a.CompanyCD and c.Creator=f.ID   ");
           searchSql.AppendLine("left outer join officedba.EmployeeInfo h");
           searchSql.AppendLine("on h.CompanyCD=a.CompanyCD and c.Summaryer=h.ID   ");
           searchSql.AppendLine(" where	a.CompanyCD = @CompanyCD            ");
           searchSql.AppendLine(" and 	a.TaskNo = @TaskNo and    a.TemplateNo=@TemplateNo  and  a.EmployeeID=@EmployeeID      ");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", taskNo));

           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", tempalteNo ));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", EmployeId));

           //启用状态

           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       public static DataTable SearchTaskInfoByTaskNO(string companyCD, string taskNo )
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("select a.TemplateNo,a.Score,isnull(a.AdviceNote,'') as AdviceNote,isnull(a.Note,'') as Note,");
           searchSql.AppendLine("e.Rate as ElemRate,a.Rate as StepRate, a.Status, a.EmployeeID,");
           searchSql.AppendLine("isnull( d.ElemName,'') as ParentElemName,");
           searchSql.AppendLine("a.ElemID,isnull(a.ElemName,'') as ElemName,isnull(b.ScoreRules,'') as ScoreRules,b.StandardScore,b.MinScore,b.MaxScore,");
           searchSql.AppendLine("isnull(b.AsseStandard,'') as AsseStandard,isnull(b.AsseFrom,'') as AsseFrom,isnull(b.Remark,'') as ElemRemark ,");
           searchSql.AppendLine("c.TaskNo,isnull(c.Title,'') as Title,CONVERT(VARCHAR(10),c.StartDate,21) AS StartDate ,CONVERT(VARCHAR(10),c.SummaryDate,21) AS SummaryDate");
           searchSql.AppendLine("     	,CASE when c.TaskFlag='2' and c.TaskNum='1' then c.TaskDate+'年第一季度'");
           searchSql.AppendLine("    	  when c.TaskFlag='2' and c.TaskNum='2' then c.TaskDate+'年第二季度'");
           searchSql.AppendLine("    	  when c.TaskFlag='2' and c.TaskNum='3' then c.TaskDate+'年第三季度'");
           searchSql.AppendLine("      	  when c.TaskFlag='2' and c.TaskNum='4' then c.TaskDate+'年第四季度'");
           searchSql.AppendLine("   	  when c.TaskFlag='3' and c.TaskNum='1' then c.TaskDate+'年上半年'");
           searchSql.AppendLine("    when c.TaskFlag='3' and c.TaskNum='2' then c.TaskDate+'年下半年'");
           searchSql.AppendLine("     when c.TaskFlag='1' then c.TaskDate+'年'+cast(c.TaskNum as varchar)+'月' ");
           searchSql.AppendLine("     when c.TaskFlag='4' then c.TaskDate+'年'+isnull( cast(c.TaskNum as varchar),'') ");
           searchSql.AppendLine("   when c.TaskFlag='5' then isnull(cast(TaskNum as varchar),'') ");
           searchSql.AppendLine("   when c.TaskFlag is null then ''");
           searchSql.AppendLine("    end as TaskNum                                 ");
           searchSql.AppendLine("   ,CASE when c.TaskFlag='1'  then '月考核'");
           searchSql.AppendLine("     when c.TaskFlag='2'  then '季考核'");
           searchSql.AppendLine("     when c.TaskFlag='3'  then '半年考核'");
           searchSql.AppendLine("     when c.TaskFlag='4'  then '年考核'");
           searchSql.AppendLine("   	  when c.TaskFlag='5'  then '临时考核'");
           searchSql.AppendLine("   	  when c.TaskFlag is null  then ''");
           searchSql.AppendLine("   	 end as TaskFlag,     ");
           searchSql.AppendLine("CONVERT(VARCHAR(10),c.EndDate,21) AS EndDate ,isnull(c.Remark,'') as Remark,CONVERT(VARCHAR(10),c.CreateDate,21) AS CreateDate,");
           searchSql.AppendLine("case when c.Status='1' then '待评分'");
           searchSql.AppendLine("when c.Status='2' then '待总评'");
           searchSql.AppendLine("when c.Status='3' then '已完成'");
           searchSql.AppendLine("when c.Status='4' then '待汇总'");
           searchSql.AppendLine("when c.Status is null then ' '");
           searchSql.AppendLine("end as StatusName");
           searchSql.AppendLine(",c.Status as TaskStaus,isnull(f.EmployeeName,'') as Creator ,isnull(h.EmployeeName,'') as Summaryer ,a.ScoreEmployee");
           searchSql.AppendLine(" from officedba.PerformanceScore a ");
           searchSql.AppendLine(" left outer join  officedba.PerformanceElem b ");
           searchSql.AppendLine("on  b.CompanyCD=a.CompanyCD and  a.ElemID=b.ID");
           searchSql.AppendLine("left outer join officedba.PerformanceTask c");
           searchSql.AppendLine(" on c.CompanyCD=a.CompanyCD  and  a.TaskNo=c.TaskNo ");
           searchSql.AppendLine("left outer join officedba.PerformanceElem d ");
           searchSql.AppendLine(" on d.CompanyCD=a.CompanyCD and  b.ParentElemNo=d.ID left outer join officedba.PerformanceTemplateElem e ");
           searchSql.AppendLine("on   e.CompanyCD=a.CompanyCD and a.TemplateNo=e.TemplateNo and a.ElemID=e.ElemID ");
           searchSql.AppendLine("left outer join officedba.EmployeeInfo f");
           searchSql.AppendLine("on f.CompanyCD=a.CompanyCD and c.Creator=f.ID   ");
           searchSql.AppendLine("left outer join officedba.EmployeeInfo h");
           searchSql.AppendLine("on h.CompanyCD=a.CompanyCD and c.Summaryer=h.ID   ");
           searchSql.AppendLine(" where	a.CompanyCD = @CompanyCD            ");
           searchSql.AppendLine(" and 	a.TaskNo = @TaskNo        ");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", taskNo));
 

           //启用状态

           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }

       public static DataTable SearchSurmmaryCheckInfo(PerformanceTaskModel model)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine(" SELECT                             ");
           searchSql.AppendLine(" 	CASE when a.TaskFlag='1'  then '月考核'");
           searchSql.AppendLine(" 	  when a.TaskFlag='2'  then '季考核'");
           searchSql.AppendLine(" 	  when a.TaskFlag='3'  then '半年考核'");
           searchSql.AppendLine(" 	  when a.TaskFlag='4'  then '年考核'");
           searchSql.AppendLine(" 	  when a.TaskFlag='5'  then '临时考核'");
           searchSql.AppendLine(" 	  when a.TaskFlag is null  then ' '");
           searchSql.AppendLine(" 	 end as TaskFlag");
           searchSql.AppendLine(" 	 ,a.ID , d.TemplateNo                             ");
           searchSql.AppendLine(" 	,CASE when a.taskflag='2' and a.TaskNum='1' then a.TaskDate+'年第一季度'");
           searchSql.AppendLine(" 	  when a.taskflag='2' and a.TaskNum='2' then a.TaskDate+'年第二季度'");
           searchSql.AppendLine(" 	  when a.taskflag='2' and a.TaskNum='3' then a.TaskDate+'年第三季度'");
           searchSql.AppendLine(" 	  when a.taskflag='2' and a.TaskNum='4' then a.TaskDate+'年第四季度'");
           searchSql.AppendLine(" 	  when a.taskflag='3' and a.TaskNum='1' then  a.TaskDate+'年上半年'");
           searchSql.AppendLine(" 	  when a.taskflag='3' and a.TaskNum='2' then  a.TaskDate+'年下半年'");
           searchSql.AppendLine(" 	  when a.taskflag='1' then  a.TaskDate+'年'+cast(a.TaskNum as varchar)+'月' ");
           searchSql.AppendLine(" 	  when a.taskflag='4' then a.TaskDate+'年'+isnull(cast(a.TaskNum as varchar),'') ");
           searchSql.AppendLine(" 	  when a.taskflag='5' then cast(a.TaskNum as varchar) ");
           searchSql.AppendLine(" 	  when a.taskflag is null then '' ");
           searchSql.AppendLine(" 	 end as TaskNum ,                           ");
           searchSql.AppendLine(" 	CASE when d.AdviceType='1' then '不做处理'");
           searchSql.AppendLine(" 	     when d.AdviceType='2' then '调整薪资'");
           searchSql.AppendLine(" 	     when d.AdviceType='3' then '晋升'");
           searchSql.AppendLine(" 	     when d.AdviceType='4' then '调职'");
           searchSql.AppendLine(" 	     when d.AdviceType='5' then '辅导'");
           searchSql.AppendLine(" 	     when d.AdviceType='6' then '培训'");
           searchSql.AppendLine(" 	     when d.AdviceType='7' then '辞退'");
           searchSql.AppendLine("     when d.AdviceType is null then ''");
           searchSql.AppendLine(" 	 end as AdviceType                            ");
           searchSql.AppendLine(" 	,CASE when d.LevelType='1' then '达到要求'");
           searchSql.AppendLine(" 	     when d.LevelType='2' then '超过要求'");
           searchSql.AppendLine(" 	     when d.LevelType='3' then '表现突出'");
           searchSql.AppendLine(" 	     when d.LevelType='4' then '需要改进'");
           searchSql.AppendLine(" 	     when d.LevelType='5' then '不合格'");
           searchSql.AppendLine("        when d.LevelType is null then ''");
           searchSql.AppendLine(" 	 end as LevelType                            ");
           searchSql.AppendLine(" 	,isnull(  d.RealScore,'') as RealScore ");
           searchSql.AppendLine(" 	,isnull(  d.KillScore,'') as KillScore ");
           searchSql.AppendLine(" 	,isnull(  d.AddScore,'') as AddScore ");
           searchSql.AppendLine(" 	,isnull(  d.TotalScore,'') as TotalScore ");
               searchSql.AppendLine(" 	,a.TaskNo ");
               searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), d.SignDate,21),'') as SignDate");
               searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), a.SummaryDate,21),'') as SummaryDate");
               searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), a.CreateDate ,21),'') as CreateDate");
               searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), d.EvaluateDate ,21),'') as EvaluateDate");
               searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), a.TaskDate ,21),'') as TaskDate");
               searchSql.AppendLine(" 	,a.TaskFlag ");
               searchSql.AppendLine(" 	,isnull( a.Title,'') as Title ");
               searchSql.AppendLine(" 	,a.TaskNo ");
               searchSql.AppendLine(" 	,CASE when a.status='1' then '待评分'");
               searchSql.AppendLine(" 	      when a.status='2' then '待总评'");
               searchSql.AppendLine(" 	      when a.status='3' then '已完成'");
                searchSql.AppendLine("when a.Status='4' then '待汇总'");
               searchSql.AppendLine(" 	      when a.status IS null  then ' '");
               searchSql.AppendLine(" 	 end as Status                            ");
               searchSql.AppendLine(" 	 ,  isnull( b.EmployeeName,'')  as Creator , isnull( c.EmployeeName,'')  as Summaryer ,isnull( e.EmployeeName,'') as passEmployeeName,d.EmployeeID as passEmployeeID,isnull( f.EmployeeName,'') as EvaluaterName, isnull( g.Title,'') as templateName                       ");


               searchSql.AppendLine(" FROM    officedba.PerformanceTask  a left outer join  officedba.EmployeeInfo b on b.CompanyCD =a.CompanyCD and a.Creator=b.ID left outer join  officedba.EmployeeInfo c on c.CompanyCD=a.CompanyCD and a.Summaryer=c.ID left outer join officedba.PerformanceSummary d on  d.CompanyCD=a.CompanyCD and a.TaskNo=d.TaskNo  left outer join  officedba.EmployeeInfo e on e.CompanyCD=a.CompanyCD and d.EmployeeID=e.ID left outer join  officedba.EmployeeInfo f on f.CompanyCD=a.CompanyCD and d.Evaluater=f.ID left outer join  officedba.PerformanceTemplate g on   g.CompanyCD=a.CompanyCD and d.TemplateNo= g.TemplateNo");
               searchSql.AppendLine(" WHERE	a.CompanyCD = @CompanyCD and a.Status='3'            ");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
           if (!string.IsNullOrEmpty(model.EditFlag ))
           {
               searchSql.AppendLine(" AND d.EmployeeID=@EmployeeID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EditFlag));
           }
          

             if (!string.IsNullOrEmpty(model.TaskFlag  ))
           {
               if (model.TaskFlag == "1")
               {
                   searchSql.AppendLine(" AND d.SignDate IS NULL ");

               }
               else
               {
                   searchSql.AppendLine(" AND d.SignDate IS not NULL ");
               }
           }

           //启用状态

           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       public static DataTable SearchSurmmaryInfo(PerformanceTaskModel model)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine(" SELECT                             ");
           searchSql.AppendLine(" 	CASE when a.TaskFlag='1'  then '月考核'");
           searchSql.AppendLine(" 	  when a.TaskFlag='2'  then '季考核'");
           searchSql.AppendLine(" 	  when a.TaskFlag='3'  then '半年考核'");
           searchSql.AppendLine(" 	  when a.TaskFlag='4'  then '年考核'");
           searchSql.AppendLine(" 	  when a.TaskFlag='5'  then '临时考核'");
           searchSql.AppendLine(" 	  when a.TaskFlag is null then ''");
           searchSql.AppendLine(" 	 end as TaskFlag");
           searchSql.AppendLine(" 	 ,a.ID , d.TemplateNo                             ");
           searchSql.AppendLine(" 	,CASE when a.taskflag='2' and a.TaskNum='1' then a.TaskDate+'年第一季度'");
           searchSql.AppendLine(" 	  when a.taskflag='2' and a.TaskNum='2' then a.TaskDate+'年第二季度'");
           searchSql.AppendLine(" 	  when a.taskflag='2' and a.TaskNum='3' then a.TaskDate+'年第三季度'");
           searchSql.AppendLine(" 	  when a.taskflag='2' and a.TaskNum='4' then a.TaskDate+'年第四季度'");
           searchSql.AppendLine(" 	  when a.taskflag='3' and a.TaskNum='1' then a.TaskDate+'年上半年'");
           searchSql.AppendLine(" 	  when a.taskflag='3' and a.TaskNum='2' then a.TaskDate+'年下半年'");
           searchSql.AppendLine(" 	  when a.taskflag='1' then a.TaskDate+'年'+cast(a.TaskNum as varchar)+'月' ");
           searchSql.AppendLine(" 	  when a.taskflag='4' then a.TaskDate+'年'+isnull(cast(a.TaskNum as varchar),'') ");
           searchSql.AppendLine(" 	  when a.taskflag='5' then cast(a.TaskNum as varchar) ");
           searchSql.AppendLine(" 	  when a.taskflag is null  then ''");
           searchSql.AppendLine(" 	 end as TaskNum ,                           ");
           searchSql.AppendLine(" 	CASE when d.AdviceType='1' then '不做处理'");
           searchSql.AppendLine(" 	     when d.AdviceType='2' then '调整薪资'");
           searchSql.AppendLine(" 	     when d.AdviceType='3' then '晋升'");
           searchSql.AppendLine(" 	     when d.AdviceType='4' then '调职'");
           searchSql.AppendLine(" 	     when d.AdviceType='5' then '辅导'");
           searchSql.AppendLine(" 	     when d.AdviceType='6' then '培训'");
           searchSql.AppendLine(" 	     when d.AdviceType='7' then '辞退'");
           searchSql.AppendLine(" 	     when d.AdviceType is null then ''");
           searchSql.AppendLine(" 	 end as AdviceType                            ");
           searchSql.AppendLine(" 	,CASE when d.LevelType='1' then '达到要求'");
           searchSql.AppendLine(" 	     when d.LevelType='2' then '超过要求'");
           searchSql.AppendLine(" 	     when d.LevelType='3' then '表现突出'");
           searchSql.AppendLine(" 	     when d.LevelType='4' then '需要改进'");
           searchSql.AppendLine(" 	     when d.LevelType='5' then '不合格'");
           searchSql.AppendLine(" 	     when d.LevelType is null then ''");
           searchSql.AppendLine(" 	 end as LevelType                            ");
           searchSql.AppendLine(" 	,isnull(  d.RealScore,'') as RealScore ");
           searchSql.AppendLine(" 	,isnull(  d.KillScore,'') as KillScore ");
           searchSql.AppendLine(" 	,isnull(  d.AddScore,'') as AddScore ");
           searchSql.AppendLine(" 	,isnull(  d.TotalScore,'') as TotalScore ");
               searchSql.AppendLine(" 	,a.TaskNo ");
             
               searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), a.SummaryDate,21),'') as SummaryDate");
               searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), a.CreateDate ,21),'') as CreateDate");
               searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), d.EvaluateDate ,21),'') as EvaluateDate");
               searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), a.TaskDate ,21),'') as TaskDate");
               searchSql.AppendLine(" 	,a.TaskFlag ");
               searchSql.AppendLine(" 	,isnull( a.Title,'') as Title ");
               searchSql.AppendLine(" 	,a.TaskNo ");
               searchSql.AppendLine(" 	,CASE when a.status='1' then '待评分'");
               searchSql.AppendLine(" 	      when a.status='2' then '待总评'");
               searchSql.AppendLine(" 	      when a.status='3' then '已完成'");
               searchSql.AppendLine("when a.Status='4' then '待汇总'");
               searchSql.AppendLine(" 	      when a.status IS null  then ' '");
               searchSql.AppendLine(" 	 end as Status                            ");
               searchSql.AppendLine(" 	 ,  isnull( b.EmployeeName,'')  as Creator , isnull( c.EmployeeName,'')  as Summaryer ,isnull( e.EmployeeName,'') as passEmployeeName,d.EmployeeID as passEmployeeID,isnull( f.EmployeeName,'') as EvaluaterName, isnull( g.Title,'') as templateName                       ");


               searchSql.AppendLine(" FROM    officedba.PerformanceTask  a left outer join  officedba.EmployeeInfo b on b.CompanyCD=a.CompanyCD and a.Creator=b.ID left outer join  officedba.EmployeeInfo c on c.CompanyCD=a.CompanyCD and a.Summaryer=c.ID left outer join officedba.PerformanceSummary d on   d.CompanyCD=a.CompanyCD and a.TaskNo=d.TaskNo   left outer join  officedba.EmployeeInfo e on e.CompanyCD=a.CompanyCD and  d.EmployeeID=e.ID left outer join  officedba.EmployeeInfo f on f.CompanyCD=a.CompanyCD and  d.Evaluater=f.ID left outer join  officedba.PerformanceTemplate g on   g.CompanyCD=a.CompanyCD and d.TemplateNo= g.TemplateNo");
           searchSql.AppendLine(" WHERE	a.CompanyCD = @CompanyCD            ");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
           
           if (!string.IsNullOrEmpty(model.Status ))
           {
               searchSql.AppendLine(" AND a.Status = @Status ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", model.Status ));
           }
           if (!string.IsNullOrEmpty(model.TaskDate))
           {
               searchSql.AppendLine(" AND a.TaskDate = @TaskDate ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskDate", model.TaskDate));
           }
           if (!string.IsNullOrEmpty(model.EditFlag ))
           {
               searchSql.AppendLine(" AND d.EmployeeID=@EmployeeID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EditFlag));
           }
          
           if (!string.IsNullOrEmpty(model.TaskNo ))
           {
               searchSql.AppendLine(" AND a.TaskNo like @TaskNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", "%" + model.TaskNo + "%"));
           }

             if (!string.IsNullOrEmpty(model.TaskFlag  ))
           {
                
               searchSql.AppendLine(" AND a.TaskFlag = @TaskFlag ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag", model.TaskFlag));
           }

             if (model.TaskFlag != "4" || model.TaskFlag != "5")
             {
                 if (!string.IsNullOrEmpty(model.TaskNum))
                 {
                     searchSql.AppendLine(" AND a.TaskNum = @TaskNum ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNum", model.TaskNum));
                 }
             }
           if (!string.IsNullOrEmpty(model.Title ))
           {
               searchSql.AppendLine(" AND a.Title LIKE @Title ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + model.Title + "%"));
           }
           //启用状态

           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
           public static DataTable SearchTaskInfo(PerformanceTaskModel  model)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine(" SELECT                             ");
           searchSql.AppendLine(" 	CASE when a.TaskFlag='1'  then '月考核'");
           searchSql.AppendLine(" 	  when a.TaskFlag='2'  then '季考核'");
           searchSql.AppendLine(" 	  when a.TaskFlag='3'  then '半年考核'");
           searchSql.AppendLine(" 	  when a.TaskFlag='4'  then '年考核'");
           searchSql.AppendLine(" 	  when a.TaskFlag='5'  then '临时考核'");
           searchSql.AppendLine(" 	  when a.TaskFlag is null then ''");
           searchSql.AppendLine(" 	 end as TaskFlag");
           searchSql.AppendLine(" 	 ,a.ID ,                              ");
           searchSql.AppendLine(" 	CASE when a.taskflag='2' and a.TaskNum='1' then a.TaskDate+'年第一季度'");
           searchSql.AppendLine(" 	  when a.taskflag='2' and a.TaskNum='2' then a.TaskDate+'年第二季度'");
           searchSql.AppendLine(" 	  when a.taskflag='2' and a.TaskNum='3' then a.TaskDate+'年第三季度'");
           searchSql.AppendLine(" 	  when a.taskflag='2' and a.TaskNum='4' then a.TaskDate+'年第四季度'");
           searchSql.AppendLine(" 	  when a.taskflag='3' and a.TaskNum='1' then a.TaskDate+'年上半年'");
           searchSql.AppendLine(" 	  when a.taskflag='3' and a.TaskNum='2' then a.TaskDate+'年下半年'");
           searchSql.AppendLine(" 	  when a.taskflag='1' then a.TaskDate+'年'+cast(a.TaskNum as varchar)+'月' ");
           searchSql.AppendLine(" 	  when a.taskflag='4' then a.TaskDate+'年'+isnull(cast(a.TaskNum as varchar),'') ");
           searchSql.AppendLine(" 	  when a.taskflag='5' then cast(a.TaskNum as varchar) ");
           searchSql.AppendLine(" 	  when a.taskflag is null  then ''");
           searchSql.AppendLine(" 	 end as TaskNum                            ");
           searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), a.StartDate ,21),'') as StartDate");
           searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), a.EndDate ,21),'') as EndDate");
               searchSql.AppendLine(" 	,a.TaskNo ");
               searchSql.AppendLine(" 	,a.TaskFlag ");
               searchSql.AppendLine(" 	,isnull( a.Title,'') as Title ");
               searchSql.AppendLine(" 	,a.TaskNo ");
               searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), a.TaskDate ,21),'') as TaskDate");
               searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), a.CreateDate ,21),'') as CreateDate");
               searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), a.SummaryDate ,21),'') as SummaryDate");
               searchSql.AppendLine(" 	,CASE when a.status='1' then '待评分'");
               searchSql.AppendLine(" 	      when a.status='2' then '待总评'");
               searchSql.AppendLine(" 	      when a.status='3' then '已完成'");
               searchSql.AppendLine(" 	      when a.status='4' then '待汇总'");
               searchSql.AppendLine(" 	      when a.status IS null  then ' '");
               searchSql.AppendLine(" 	 end as Status                            ");
               searchSql.AppendLine(" 	 , isnull(b.EmployeeName,'')  as Creator  , isnull( c.EmployeeName,'')  as Summaryer                         ");


               searchSql.AppendLine(" FROM    officedba.PerformanceTask  a left outer join  officedba.EmployeeInfo b on  b.CompanyCD=a.CompanyCD and a.Creator=b.ID left outer join  officedba.EmployeeInfo c on c.CompanyCD=a.CompanyCD and  a.Summaryer=c.ID ");
           searchSql.AppendLine(" WHERE	a.CompanyCD = @CompanyCD  and a.status='4'          ");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

           //l
           if (!string.IsNullOrEmpty(model.TaskNo ))
           {
               searchSql.AppendLine(" AND a.TaskNo like @TaskNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", "%" + model.TaskNo + "%" ));
           }

           if (!string.IsNullOrEmpty(model.TaskDate))
           {

               searchSql.AppendLine(" AND a.TaskDate = @TaskDate ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskDate", model.TaskDate));
           }
             if (!string.IsNullOrEmpty(model.TaskFlag  ))
           {
                
               searchSql.AppendLine(" AND a.TaskFlag = @TaskFlag ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag", model.TaskFlag));
           }

             if (model.TaskFlag != "4" || model.TaskFlag != "5")
             {
                 if (!string.IsNullOrEmpty(model.TaskNum))
                 {
                     searchSql.AppendLine(" AND a.TaskNum = @TaskNum ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNum", model.TaskNum));
                 }
             }
           if (!string.IsNullOrEmpty(model.Title ))
           {
               searchSql.AppendLine(" AND a.Title LIKE @Title ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + model.Title + "%"));
           }
           //启用状态

           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
           public static bool UpdatePerSummaryInfo(IList<PerformanceSummaryModel> modeList)
           {
               bool isSucc = false;
               foreach (PerformanceSummaryModel model in modeList)
               {
                   #region 插入SQL拼写
                   StringBuilder insertSql = new StringBuilder();
                   insertSql.AppendLine("update officedba.PerformanceSummary ");
                   insertSql.AppendLine("           set TotalScore=@TotalScore ");
                   insertSql.AppendLine("           ,ModifiedUserID=@ModifiedUserID     ");
                   insertSql.AppendLine("           ,ModifiedDate=getdate()             ");
                   insertSql.AppendLine("where CompanyCD=@CompanyCD and TaskNo=@TaskNo  and TemplateNo=@TemplateNo   and EmployeeID=@EmployeeID  ");
                
                   #endregion
                   //定义插入基本信息的命令
                   SqlCommand comm = new SqlCommand();
                   comm.CommandText = insertSql.ToString();
                   //设置保存的参数
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));	//类型名称
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo));	//创建人
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));	//启用状态
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalScore", model.TotalScore));	//启用状态
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID ));	
                   //添加返回参数
                   //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

                   //执行插入操作
                   isSucc = SqlHelper.ExecuteTransWithCommand(comm);
                   if (!isSucc)
                   {
                       isSucc = false;
                       break;
                   }
                   else
                   {
                       continue;
                   }
               }
               return isSucc;

           }
           public static bool UpdateGatherSummaryCheckInfo(PerformanceSummaryModel model)
           {
               bool isSucc = false;

               #region 插入SQL拼写
               StringBuilder insertSql = new StringBuilder();
               insertSql.AppendLine("update officedba.PerformanceSummary ");
               insertSql.AppendLine("           set SignNote=@SignNote ");
               insertSql.AppendLine("            , SignDate=getdate() ");
               insertSql.AppendLine("           ,ModifiedUserID=@ModifiedUserID     ");
               insertSql.AppendLine("           ,ModifiedDate=getdate()             ");
               insertSql.AppendLine("where CompanyCD=@CompanyCD  and TaskNo=@TaskNo  and TemplateNo=@TemplateNo   and EmployeeID=@EmployeeID  ");
               insertSql.AppendLine("         ");
               #endregion
               //定义插入基本信息的命令
               SqlCommand comm = new SqlCommand();
               comm.CommandText = insertSql.ToString();
               //设置保存的参数
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));	//类型名称
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo));	//创建人
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));	//启用状态
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@SignNote", model.SignNote ));	//公司代码
               
               //添加返回参数
               //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

               //执行插入操作
               isSucc = SqlHelper.ExecuteTransWithCommand(comm);

               return isSucc;

           }
           public static bool UpdatePerSummaryInfobyCheck(PerformanceSummaryModel model)
           {
               bool isSucc = false;
               
                   #region 插入SQL拼写
                   StringBuilder insertSql = new StringBuilder();
                   insertSql.AppendLine("update officedba.PerformanceSummary ");
                   insertSql.AppendLine("           set AddScore=@AddScore ");

                   insertSql.AppendLine("            ,AdviceNote=@AdviceNote ");
                   insertSql.AppendLine("             ,AdviceType=@AdviceType ");
                   insertSql.AppendLine("            , Evaluater=@Evaluater ");
                   insertSql.AppendLine("            , EvaluateDate=getdate() ");
                   insertSql.AppendLine("             ,KillScore=@KillScore ");
                   insertSql.AppendLine("             ,LevelType=@LevelType ");
                   insertSql.AppendLine("            , Remark=@Remark ");
                   insertSql.AppendLine("            , RewardNote=@RewardNote ");
                   insertSql.AppendLine("            , SummaryNote=@SummaryNote ");
                   insertSql.AppendLine("             ,RealScore=@RealScore ");
                   insertSql.AppendLine("           ,ModifiedUserID=@ModifiedUserID     ");
                   insertSql.AppendLine("           ,ModifiedDate=getdate()             ");
                   insertSql.AppendLine("where CompanyCD=@CompanyCD and TaskNo=@TaskNo and TemplateNo=@TemplateNo  and EmployeeID=@EmployeeID  ");
                  
                   #endregion
                   //定义插入基本信息的命令
                   SqlCommand comm = new SqlCommand();
                   comm.CommandText = insertSql.ToString();
                   //设置保存的参数
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));	//类型名称
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo));	//创建人
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));	//启用状态
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@AddScore", model.AddScore));	//公司代码
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdviceNote", model.AdviceNote));	//类型名称
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdviceType", model.AdviceType));	//创建人
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Evaluater", model.Evaluater));	//启用状态
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@KillScore", model.KillScore));	//启用状态
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@LevelType", model.LevelType));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));	//创建人
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@RewardNote", model.RewardNote));	//启用状态
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@SummaryNote", model.SummaryNote));	//启用状态
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@RealScore", model.RealScore));
                   //添加返回参数
                   //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

                   //执行插入操作
                   isSucc = SqlHelper.ExecuteTransWithCommand(comm);
                
               return isSucc;

           }
           public static bool UpdateTaskStatusInfo(PerformanceTaskModel model)
           {

               #region 插入SQL拼写
               StringBuilder insertSql = new StringBuilder();
               insertSql.AppendLine("update officedba.PerformanceTask ");
               insertSql.AppendLine("           set Status=@Status ");
               if (!string.IsNullOrEmpty(model.Summaryer))
               {
                   insertSql.AppendLine("           ,Summaryer=@Summaryer     ");
                   insertSql.AppendLine("           ,SummaryDate=getdate()             ");
                   insertSql.AppendLine("           ,CompleteDate=getdate()             ");
               }
               insertSql.AppendLine("           ,ModifiedUserID=@ModifiedUserID     ");
               insertSql.AppendLine("           ,ModifiedDate=getdate()             ");
               insertSql.AppendLine("where CompanyCD=@CompanyCD and TaskNo=@TaskNo  ");
               #endregion
               //定义插入基本信息的命令
               SqlCommand comm = new SqlCommand();
               comm.CommandText = insertSql.ToString();
               //设置保存的参数
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));	//类型名称
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.Status ));	//创建人
               if (!string.IsNullOrEmpty(model.Summaryer))
               {
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Summaryer", model.Summaryer));	//启用状态
               }
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));	//启用
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", model.Status ));	//启用状态
               //添加返回参数
               //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

               //执行插入操作
              bool  isSucc = SqlHelper.ExecuteTransWithCommand(comm);
              return isSucc;

           }
    }
}

/*************王保军
 * 建立时间： 2009/05/18
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
   public  class PerformanceQueryDBHelper
    {
       public static DataTable SearchCheckElemInfo(PerformanceTaskModel  model)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine(" SELECT  ID,                           ");
           searchSql.AppendLine(" 	 TaskNo                               ");
           searchSql.AppendLine(" 	,ISNULL(Title, '') AS Title ");
           searchSql.AppendLine(" FROM                               ");
           searchSql.AppendLine(" 	officedba.PerformanceTask         ");
           searchSql.AppendLine(" WHERE                              ");
           searchSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
         //  searchSql.AppendLine(" and 	UsedStatus = @UsedStatus            ");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
          // comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", "1"));
           //l
           //启用状态


           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }

       public static DataTable SearchDeptInfo( string companyCD)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine(" select distinct (ID),DeptName from officedba.DeptInfo     ");
           searchSql.AppendLine(" where	CompanyCD = @CompanyCD            ");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
           //l
           //启用状态


           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       public static DataTable SearchSubDeptInfo(string deptID,string companyCD)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine(" select distinct (ID) from officedba.DeptInfo     ");
           searchSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
           searchSql.AppendLine(" and 	SuperDeptID = @deptID            ");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@deptID", deptID));
           //l
           //启用状态


           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       public static DataTable SearchPerTypeInfo(PerformanceTypeModel  model)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine(" SELECT                             ");
           searchSql.AppendLine(" 	 ID                               ");
           searchSql.AppendLine(" 	,ISNULL(TypeName, '') AS TypeName ");
           searchSql.AppendLine(" FROM                               ");
           searchSql.AppendLine(" 	officedba.PerformanceType         ");
           searchSql.AppendLine(" WHERE                              ");
           searchSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
           searchSql.AppendLine(" and 	UsedStatus = @UsedStatus            ");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", "1"));
           //l
           //启用状态


           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }

       public static DataTable SearchStaticsInfo(PerformanceTaskModel model)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("                 SELECT       isnull(e.deptID,'') as deptID,a.taskNo, Isnull( a.Title,'') as Title,      ");                
            	   searchSql.AppendLine("   CASE when a.TaskFlag='1'  then '月考核'");
            	   searchSql.AppendLine("     when a.TaskFlag='2'  then '季考核'");
            	   searchSql.AppendLine("     when a.TaskFlag='3'  then '半年考核'");
            	   searchSql.AppendLine("     when a.TaskFlag='4'  then '年考核'");
               searchSql.AppendLine("   	  when a.TaskFlag='5'  then '临时考核'");
               searchSql.AppendLine("   	  when a.TaskFlag is null  then ''");
               searchSql.AppendLine("   	 end as TaskFlag      ");
             searchSql.AppendLine("     	   	,CASE when a.taskflag='2' and a.TaskNum='1' then a.TaskDate+'年第一季度'");
             searchSql.AppendLine("    	  when a.taskflag='2' and a.TaskNum='2' then a.TaskDate+'年第二季度'");
             searchSql.AppendLine("    	  when a.taskflag='2' and a.TaskNum='3' then a.TaskDate+'年第三季度'");
             searchSql.AppendLine("      	  when a.taskflag='2' and a.TaskNum='4' then a.TaskDate+'年第四季度'");
             searchSql.AppendLine("   	  when a.taskflag='3' and a.TaskNum='1' then a.TaskDate+'年上半年'");
             searchSql.AppendLine("    when a.taskflag='3' and a.TaskNum='2' then a.TaskDate+'年下半年'");
             searchSql.AppendLine("     when a.taskflag='1' then a.TaskDate+'年'+cast(a.TaskNum as varchar)+'月' ");
             searchSql.AppendLine("     when a.taskflag='4' then a.TaskDate+'年'+isnull(cast(a.TaskNum as varchar),'') ");
           	     searchSql.AppendLine("   when a.taskflag='5' then cast(a.TaskNum as varchar) ");
                 searchSql.AppendLine("   when a.taskflag is null then ''");
           	   searchSql.AppendLine("    end as TaskNum                                 ");                        
               searchSql.AppendLine("   	,CASE when d.LevelType='1' then '达到要求'");
               searchSql.AppendLine("   	     when d.LevelType='2' then '超过要求'");
            	   searchSql.AppendLine("        when d.LevelType='3' then '表现突出'");
            	   searchSql.AppendLine("        when d.LevelType='4' then '需要改进'");
            	   searchSql.AppendLine("        when d.LevelType='5' then '不合格'");
                   searchSql.AppendLine("        when d.LevelType is null then ''");
            	   searchSql.AppendLine("    end as LevelType ,");
         	   searchSql.AppendLine("   CASE when d.AdviceType='1' then '不做处理'");
           	   searchSql.AppendLine("        when d.AdviceType='2' then '调整薪资'");
           	   searchSql.AppendLine("        when d.AdviceType='3' then '晋升'");
           	   searchSql.AppendLine("        when d.AdviceType='4' then '调职'");
           	    searchSql.AppendLine("       when d.AdviceType='5' then '辅导'");
           	    searchSql.AppendLine("       when d.AdviceType='6' then '培训'");
           	      searchSql.AppendLine("     when d.AdviceType='7' then '辞退'");
                  searchSql.AppendLine("     when d.AdviceType is null then ''");
                  searchSql.AppendLine("    end as AdviceType ,isnull( h.DeptName,'') as DeptName      ");                    
               searchSql.AppendLine("   	,count( e.deptID) as CountPerson       ");     
            searchSql.AppendLine("      FROM    officedba.PerformanceTask  a left outer join ");
    searchSql.AppendLine("   officedba.EmployeeInfo b");
    searchSql.AppendLine("    on b.CompanyCD=a.CompanyCD and  a.Creator=b.ID ");
   searchSql.AppendLine("   left outer join  officedba.EmployeeInfo c ");
   searchSql.AppendLine("   on c.CompanyCD=a.CompanyCD and  a.Summaryer=c.ID");
   searchSql.AppendLine("    left outer join officedba.PerformanceSummary d ");
   searchSql.AppendLine("   on  d.CompanyCD=a.CompanyCD and a.TaskNo=d.TaskNo ");
   searchSql.AppendLine("   left outer join  officedba.EmployeeInfo e");
   searchSql.AppendLine("   on  e.CompanyCD=a.CompanyCD and d.EmployeeID=e.ID ");
   searchSql.AppendLine("   left outer join  officedba.EmployeeInfo f ");
   searchSql.AppendLine("   on f.CompanyCD=a.CompanyCD and d.Evaluater=f.ID ");
   searchSql.AppendLine("   left outer join  officedba.PerformanceTemplate g");
   searchSql.AppendLine("   on    g.CompanyCD=a.CompanyCD and d.TemplateNo= g.TemplateNo");
   searchSql.AppendLine("   left outer join officedba.DeptInfo h");
   searchSql.AppendLine("   on h.CompanyCD=a.CompanyCD and e.deptID=h.ID");

   searchSql.AppendLine("   where a.CompanyCD=@CompanyCD and a.Status='3'");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
           if (!string.IsNullOrEmpty(model.EditFlag))//被考核人
           {
               searchSql.AppendLine(" AND e.DeptID=@DeptID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.EditFlag));
           }
           if (!string.IsNullOrEmpty(model.TaskNo ))//考核任务编号
           {
               searchSql.AppendLine(" AND a.TaskNo=@TaskNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));
           }
           if (!string.IsNullOrEmpty(model.CompleteDate))//考核类型
           {
               searchSql.AppendLine(" AND g.TypeID=@TypeID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", model.CompleteDate));
           }
           if (!string.IsNullOrEmpty(model.TaskNum))//考核期间 
           {
               searchSql.AppendLine(" AND a.TaskNum=@TaskNum ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNum", model.TaskNum));
           }
           if (!string.IsNullOrEmpty(model.Summaryer))//考核等级
           {
               searchSql.AppendLine(" AND d.LevelType=@LevelType ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@LevelType", model.Summaryer));
           }
           if (!string.IsNullOrEmpty(model.Title))//考核建议
           {
               searchSql.AppendLine(" AND d.AdviceType=@AdviceType ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdviceType", model.Title));
           }
           if (!string.IsNullOrEmpty(model.TaskDate ))//考核建议
           {
               searchSql.AppendLine(" AND a.TaskDate=@TaskDate ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskDate", model.TaskDate ));
           }
           if (!string.IsNullOrEmpty(model.TaskFlag))//考核期间类型
           {
               searchSql.AppendLine(" AND a.TaskFlag=@TaskFlag ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag", model.TaskFlag));
           }
             searchSql.AppendLine(" group by e.deptID,a.TaskNo,a.taskflag,a.TaskNum,d.LevelType,d.AdviceType,a.TaskDate,h.DeptName,a.title");
           //启用状态

           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       public static DataTable SearchDetailsInfoByLT(PerformanceTaskModel model)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("                 SELECT   ");
           searchSql.AppendLine("   	CASE when d.LevelType='1' then '达到要求'");
           searchSql.AppendLine("   	     when d.LevelType='2' then '超过要求'");
           searchSql.AppendLine("        when d.LevelType='3' then '表现突出'");
           searchSql.AppendLine("        when d.LevelType='4' then '需要改进'");
           searchSql.AppendLine("        when d.LevelType='5' then '不合格'");
           searchSql.AppendLine("        when d.LevelType is null then ''");
           searchSql.AppendLine("    end as LevelType,d.LevelType as typeID ");
           searchSql.AppendLine("   	,count( e.deptID) as ID   ");
           searchSql.AppendLine("      FROM    officedba.PerformanceTask  a left outer join ");
           searchSql.AppendLine("   officedba.EmployeeInfo b");
           searchSql.AppendLine("    on b.CompanyCD=a.CompanyCD and a.Creator=b.ID ");
           searchSql.AppendLine("   left outer join  officedba.EmployeeInfo c ");
           searchSql.AppendLine("   on c.CompanyCD=a.CompanyCD and a.Summaryer=c.ID");
           searchSql.AppendLine("    left outer join officedba.PerformanceSummary d ");
           searchSql.AppendLine("   on  d.CompanyCD=a.CompanyCD   and a.TaskNo=d.TaskNo");
           searchSql.AppendLine("   left outer join  officedba.EmployeeInfo e");
           searchSql.AppendLine("   on e.CompanyCD=a.CompanyCD and d.EmployeeID=e.ID ");
           searchSql.AppendLine("   left outer join  officedba.EmployeeInfo f ");
           searchSql.AppendLine("   on f.CompanyCD=a.CompanyCD and  d.Evaluater=f.ID ");
           searchSql.AppendLine("   left outer join  officedba.PerformanceTemplate g");
           searchSql.AppendLine("   on  g.CompanyCD=a.CompanyCD  and d.TemplateNo= g.TemplateNo");
           searchSql.AppendLine("   left outer join officedba.DeptInfo h");
           searchSql.AppendLine("   on h.CompanyCD=a.CompanyCD and e.deptID=h.ID");

           searchSql.AppendLine("   where a.CompanyCD=@CompanyCD and a.Status='3'");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
           if (!string.IsNullOrEmpty(model.EditFlag))//被考核人
           {
               searchSql.AppendLine(" AND e.DeptID=@DeptID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.EditFlag));
           }
           if (!string.IsNullOrEmpty(model.TaskNo))//考核任务编号
           {
               searchSql.AppendLine(" AND a.TaskNo=@TaskNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));
           }
           if (!string.IsNullOrEmpty(model.CompleteDate))//考核类型
           {
               searchSql.AppendLine(" AND g.TypeID=@TypeID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", model.CompleteDate));
           }
           if (!string.IsNullOrEmpty(model.TaskNum))//考核期间 
           {
               searchSql.AppendLine(" AND a.TaskNum=@TaskNum ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNum", model.TaskNum));
           }
           if (!string.IsNullOrEmpty(model.Summaryer))//考核等级
           {
               searchSql.AppendLine(" AND d.LevelType=@LevelType ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@LevelType", model.Summaryer));
           }
           if (!string.IsNullOrEmpty(model.Title))//考核建议
           {
               searchSql.AppendLine(" AND d.AdviceType=@AdviceType ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdviceType", model.Title));
           }
           if (!string.IsNullOrEmpty(model.TaskDate))//考核建议
           {
               searchSql.AppendLine(" AND a.TaskDate=@TaskDate ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskDate", model.TaskDate));
           }
           if (!string.IsNullOrEmpty(model.TaskFlag))//考核期间类型
           {
               searchSql.AppendLine(" AND a.TaskFlag=@TaskFlag ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag", model.TaskFlag));
           }
           if (!string.IsNullOrEmpty(model.SummaryDate ))//部门
           {
               searchSql.AppendLine(" AND e.deptID=@deptID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@deptID", model.SummaryDate));
           }
          // e.deptID,g.TypeID,a.TaskNo,a.taskflag,a.TaskNum,
           searchSql.AppendLine(" group by d.LevelType order by ID desc");
           //启用状态

           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       public static DataTable SearchDetailsInfoByLA(PerformanceTaskModel model)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("                 SELECT    ");
           searchSql.AppendLine("   	CASE when d.AdviceType='1' then '不做处理'");
           searchSql.AppendLine("   	     when d.AdviceType='2' then '调整薪资'");
           searchSql.AppendLine("        when d.AdviceType='3' then '晋升'");
           searchSql.AppendLine("        when d.AdviceType='4' then '调职'");
           searchSql.AppendLine("        when d.AdviceType='5' then '辅导'");
           searchSql.AppendLine("        when d.AdviceType='6' then '培训'");
           searchSql.AppendLine("        when d.AdviceType='7' then '辞退'");
           searchSql.AppendLine("        when d.AdviceType is null then ''");
           searchSql.AppendLine("    end as LevelType ");
           searchSql.AppendLine("   	,count( e.deptID) as ID ,d.AdviceType as typeID  ");
           searchSql.AppendLine("      FROM    officedba.PerformanceTask  a left outer join ");
           searchSql.AppendLine("   officedba.EmployeeInfo b");
           searchSql.AppendLine("    on b.CompanyCD=a.CompanyCD and a.Creator=b.ID ");
           searchSql.AppendLine("   left outer join  officedba.EmployeeInfo c ");
           searchSql.AppendLine("   on c.CompanyCD=a.CompanyCD and a.Summaryer=c.ID");
           searchSql.AppendLine("    left outer join officedba.PerformanceSummary d ");
           searchSql.AppendLine("   on  d.CompanyCD=a.CompanyCD  and a.TaskNo=d.TaskNo ");
           searchSql.AppendLine("   left outer join  officedba.EmployeeInfo e");
           searchSql.AppendLine("   on e.CompanyCD =a.CompanyCD and d.EmployeeID=e.ID ");
           searchSql.AppendLine("   left outer join  officedba.EmployeeInfo f ");
           searchSql.AppendLine("   on f.CompanyCD =a.CompanyCD and  d.Evaluater=f.ID ");
           searchSql.AppendLine("   left outer join  officedba.PerformanceTemplate g");
           searchSql.AppendLine("   on   g.CompanyCD=a.CompanyCD and d.TemplateNo= g.TemplateNo");
           searchSql.AppendLine("   left outer join officedba.DeptInfo h");
           searchSql.AppendLine("   on h.CompanyCD =a.CompanyCD and e.deptID=h.ID");

           searchSql.AppendLine("   where a.CompanyCD=@CompanyCD and a.Status='3'");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
           if (!string.IsNullOrEmpty(model.EditFlag))//被考核人
           {
               searchSql.AppendLine(" AND e.DeptID=@DeptID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.EditFlag));
           }
           if (!string.IsNullOrEmpty(model.TaskNo))//考核任务编号
           {
               searchSql.AppendLine(" AND a.TaskNo=@TaskNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));
           }
           if (!string.IsNullOrEmpty(model.CompleteDate))//考核类型
           {
               searchSql.AppendLine(" AND g.TypeID=@TypeID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", model.CompleteDate));
           }
           if (!string.IsNullOrEmpty(model.TaskNum))//考核期间 
           {
               searchSql.AppendLine(" AND a.TaskNum=@TaskNum ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNum", model.TaskNum));
           }
           if (!string.IsNullOrEmpty(model.Summaryer))//考核等级
           {
               searchSql.AppendLine(" AND d.LevelType=@LevelType ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@LevelType", model.Summaryer));
           }
           if (!string.IsNullOrEmpty(model.Title))//考核建议
           {
               searchSql.AppendLine(" AND d.AdviceType=@AdviceType ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdviceType", model.Title));
           }
           if (!string.IsNullOrEmpty(model.TaskDate))//考核建议
           {
               searchSql.AppendLine(" AND a.TaskDate=@TaskDate ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskDate", model.TaskDate));
           }
           if (!string.IsNullOrEmpty(model.TaskFlag))//考核期间类型
           {
               searchSql.AppendLine(" AND a.TaskFlag=@TaskFlag ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag", model.TaskFlag));
           }
           if (!string.IsNullOrEmpty(model.SummaryDate))//部门
           {
               searchSql.AppendLine(" AND e.deptID=@deptID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@deptID", model.SummaryDate));
           }
           // e.deptID,g.TypeID,a.TaskNo,a.taskflag,a.TaskNum,
           searchSql.AppendLine(" group by d.AdviceType order by ID desc");
           //启用状态

           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       public static DataTable SearchScoreInfo(PerformanceTaskModel model)
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
           searchSql.AppendLine(" 	,a.TaskNo, pt.TypeName");
           searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), d.SignDate,21),'') as SignDate");
           searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), a.SummaryDate,21),'') as SummaryDate");
           searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), a.CreateDate ,21),'') as CreateDate");
           searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), d.EvaluateDate ,21),'') as EvaluateDate");
           searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), a.TaskDate ,21),'') as TaskDate");
           searchSql.AppendLine(" 	,a.TaskFlag ");
           searchSql.AppendLine(" 	,isnull( a.Title,'') as Title ");
           searchSql.AppendLine(" 	,a.TaskNo ,isnull( h.DeptName,'') as DeptName ");
           searchSql.AppendLine(" 	,CASE when a.status='1' then '待评分'");
           searchSql.AppendLine(" 	      when a.status='2' then '待总评'");
           searchSql.AppendLine(" 	      when a.status='3' then '已完成'");
           searchSql.AppendLine("when a.Status='4' then '待汇总'");
           searchSql.AppendLine(" 	      when a.status IS null  then ' '");
           searchSql.AppendLine(" 	 end as Status                            ");
           searchSql.AppendLine(" 	 ,  isnull( b.EmployeeName,'')  as Creator , isnull( c.EmployeeName,'')  as Summaryer ,isnull( e.EmployeeName,'') as passEmployeeName,d.EmployeeID as passEmployeeID,isnull( f.EmployeeName,'') as EvaluaterName, isnull( g.Title,'') as templateName                       ");


           searchSql.AppendLine(" FROM    officedba.PerformanceTask  a left outer join  officedba.EmployeeInfo b on b.CompanyCD=a.CompanyCD and a.Creator=b.ID left outer join  officedba.EmployeeInfo c on  c.CompanyCD=a.CompanyCD and a.Summaryer=c.ID left outer join officedba.PerformanceSummary d on  d.CompanyCD=a.CompanyCD   and a.TaskNo=d.TaskNo  left outer join  officedba.EmployeeInfo e on e.CompanyCD=a.CompanyCD and  d.EmployeeID=e.ID left outer join  officedba.EmployeeInfo f on  f.CompanyCD=a.CompanyCD and d.Evaluater=f.ID left outer join  officedba.PerformanceTemplate g on   g.CompanyCD=a.CompanyCD and d.TemplateNo= g.TemplateNo");
           searchSql.AppendLine("   left outer join officedba.DeptInfo h");
           searchSql.AppendLine("   on h.CompanyCD=a.CompanyCD and  e.deptID=h.ID");
           searchSql.AppendLine("   left outer join officedba.PerformanceType pt");
           searchSql.AppendLine("   on pt.CompanyCD=a.CompanyCD and g.TypeID=pt.ID");
           searchSql.AppendLine(" WHERE	a.CompanyCD = @CompanyCD  and a.Status='3'           ");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
           if (!string.IsNullOrEmpty(model.EndDate))//部门
           {
               searchSql.AppendLine(" AND e.deptID=@deptID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@deptID", model.EndDate));
           }
           if (!string.IsNullOrEmpty(model.EditFlag))//被考核人
           {
               searchSql.AppendLine(" AND d.EmployeeID=@EmployeeID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EditFlag));
           }
           if (!string.IsNullOrEmpty(model.TaskNo ))//考核任务编号
           {
               searchSql.AppendLine(" AND a.TaskNo=@TaskNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));
           }

           if (!string.IsNullOrEmpty(model.TaskDate))//考核任务编号
           {
               searchSql.AppendLine(" AND a.TaskDate=@TaskDate ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskDate", model.TaskDate ));
           }
           if (!string.IsNullOrEmpty(model.CompleteDate))//考核类型
           {
               searchSql.AppendLine(" AND g.TypeID=@TypeID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", model.CompleteDate));
           }
           if (!string.IsNullOrEmpty(model.TaskNum))//考核期间 
           {
               searchSql.AppendLine(" AND a.TaskNum=@TaskNum ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNum", model.TaskNum));
           }
           if (!string.IsNullOrEmpty(model.Summaryer))//考核等级
           {
               searchSql.AppendLine(" AND d.LevelType=@LevelType ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@LevelType", model.Summaryer));
           }
           if (!string.IsNullOrEmpty(model.Title))//考核建议
           {
               searchSql.AppendLine(" AND d.AdviceType=@AdviceType ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdviceType", model.Title));
           }
           if (!string.IsNullOrEmpty(model.TaskFlag))//考核期间类型
           {
               searchSql.AppendLine(" AND a.TaskFlag=@TaskFlag ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag", model.TaskFlag));
           }

           //启用状态

           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       public static DataTable SearchSummaryInfo(string taskNo, string templateNo, string employeeID, string companyCD)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine(" select a.TemplateNo,isnull(a.ElemName,'') as ElemName,a.StepNo,isnull(a.StepName,'') as StepName,a.Score,a.Rate,a.ElemID ,d.rate as ElemRate                            ");
           searchSql.AppendLine(" ,isnull(b.EmployeeName,'') as ScoreEmployeeName,a.AdviceNote,a.Note ,CONVERT(VARCHAR(10),a.ScoreDate,21) AS ScoreDate  "); 
           searchSql.AppendLine(" ,e.ElemName as ParentName");
           searchSql.AppendLine(" from officedba.PerformanceScore a ");
           searchSql.AppendLine(" left outer join officedba.EmployeeInfo b");
           searchSql.AppendLine(" on b.CompanyCD=a.CompanyCD and a.ScoreEmployee=b.ID");
           searchSql.AppendLine(" left outer join officedba.PerformanceElem c  on   c.CompanyCD=a.CompanyCD  and a.ElemID=c.ID ");
           searchSql.AppendLine(" left outer join officedba.PerformanceElem e");
           searchSql.AppendLine(" on  e.CompanyCD=a.CompanyCD and  c.ParentElemNo=e.ID ");
           searchSql.AppendLine("  left outer join officedba.PerformanceTemplateElem d on d.CompanyCD=a.CompanyCD  and   a.TemplateNo=d.TemplateNo and a.ElemID=d.ElemID                      ");
           searchSql.AppendLine(" where a.CompanyCD=@CompanyCD and  a.taskno=@taskno  and a.TemplateNo=@TemplateNo and a.EmployeeID=@EmployeeID   ");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", employeeID));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", taskNo));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", templateNo));
           //启用状态

           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }

    }
}

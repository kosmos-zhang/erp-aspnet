/**********************************************
 * 类作用：   考核任务操作
 * 建立人：   王保军
 * 建立时间： 2009/05/05
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
   public  class PerformanceTaskDBHelper
   {
      /// <summary>
      /// 获取设置为启用的模板信息
      /// </summary>
      /// <param name="CompanyCD">公司代码</param>
      /// <returns></returns>
       public static DataTable GetTemplateInfo(string CompanyCD)
       {
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("select distinct(TemplateNo),ISNULL (Title,'') as Title from officedba.PerformanceTemplate where CompanyCD=@CompanyCD and UsedStatus=@UsedStatus ");
           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", "1"));
           //l
           //启用状态
           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       /// <summary>
       /// 获取设置为启用的模板信息
       /// </summary>
       /// <param name="CompanyCD">公司代码</param>
       /// <returns></returns>
       public static DataTable GetTemplatebyNO(string TemplateNo , string EmployeeID, string CompanyCD)
       {
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("select a.StepNo,ISNULL( a.StepName,'') as StepName , a.Rate,a.ScoreEmployee,c.ElemID,isnull( d.ElemName,'') as ElemName, a.TemplateNo from officedba.PerformanceTemplateEmp  a  left outer join officedba.PerformanceTemplate b  on  b.CompanyCD=a.CompanyCD   and  a.TemplateNo=b.TemplateNo left outer join officedba.PerformanceTemplateElem c  on   c.CompanyCD=a.CompanyCD  and  b.TemplateNo=c.TemplateNo left outer join officedba.PerformanceElem d  on  d.CompanyCD=a.CompanyCD   and  c.ElemID=d.ID  where    a.CompanyCD=@CompanyCD and a.TemplateNo=@TemplateNo ");
           if (!string.IsNullOrEmpty(EmployeeID))
           { 
               searchSql.AppendLine(" and a.EmployeeID=@EmployeeID ");
           }

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo",TemplateNo ));
           if (!string.IsNullOrEmpty(EmployeeID))
           {
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", EmployeeID));
           }
          
           //l
           //启用状态
           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       /// <summary>
       /// 获取被模板设置的考核人信息
       /// </summary>
       /// <param name="CompanyCD">公司代码</param>
       /// <returns></returns>
       public static DataTable GetEmployeeInfo(string CompanyCD)
       {
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("select distinct(EmployeeID),isnull(b.EmployeeName,'') as EmployeeName from officedba.PerformanceTemplateEmp a left outer join officedba.EmployeeInfo b on b.CompanyCD=a.CompanyCD and a.EmployeeID=b.ID left outer join officedba.PerformanceTemplate c on     c.CompanyCD=a.CompanyCD and a.TemplateNo=c.TemplateNo where a.CompanyCD=@CompanyCD and c.UsedStatus=@UsedStatus ");
           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", "1"));
           //l
           //启用状态
           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       /// <summary>
       /// 获取被模板设置的考核人信息
       /// </summary>
       /// <param name="CompanyCD">公司代码</param>
       /// <returns></returns>
       public static DataTable GetEmployeeInfoByTemplateNo(string TemplateNo,string CompanyCD)
       {



           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("select distinct(EmployeeID),isnull(b.EmployeeName,'') as  EmployeeName from officedba.PerformanceTemplateEmp a left outer join officedba.EmployeeInfo b on b.CompanyCD =a.CompanyCD and a.EmployeeID=b.ID left outer join officedba.PerformanceTemplate c on   c.CompanyCD=a.CompanyCD   and a.TemplateNo=c.TemplateNo  where a.CompanyCD=@CompanyCD and c.UsedStatus=@UsedStatus and a.TemplateNo in (" + TemplateNo + ")");
           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", "1"));
          // comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", TemplateNo));
           //l
           //启用状态
           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }

       /// <summary>
       /// 批插入考核任务表信息
       /// </summary>
       /// <param name="modeList"></param>
       /// <returns></returns>
       public static bool InsertPerTaskInfo(PerformanceTaskModel model)
       {
          
               #region 插入SQL拼写
               StringBuilder insertSql = new StringBuilder();
               insertSql.AppendLine("INSERT INTO officedba.PerformanceTask ");
               insertSql.AppendLine("           (CompanyCD             ");
               insertSql.AppendLine("           ,TaskNo                ");
               insertSql.AppendLine("           ,Title              ");
               insertSql.AppendLine("           ,TaskFlag                 ");
               insertSql.AppendLine("           ,TaskDate           ");
               insertSql.AppendLine("           ,TaskNum               ");
               insertSql.AppendLine("           ,StartDate               ");
               insertSql.AppendLine("           ,EndDate               ");
               insertSql.AppendLine("           ,Status               ");
               insertSql.AppendLine("           ,CreateDate               ");
               insertSql.AppendLine("           ,Remark               ");
               insertSql.AppendLine("           ,Creator)                 ");

               insertSql.AppendLine("     VALUES                        ");
               insertSql.AppendLine("           (@CompanyCD            ");
               insertSql.AppendLine("           ,@TaskNo               ");
               insertSql.AppendLine("           ,@Title             ");
               insertSql.AppendLine("           ,@TaskFlag               ");
               insertSql.AppendLine("           ,@TaskDate          ");
               insertSql.AppendLine("           ,@TaskNum             ");
               insertSql.AppendLine("           ,@StartDate               ");
               insertSql.AppendLine("           ,@EndDate               ");
               insertSql.AppendLine("           ,@Status             ");

               insertSql.AppendLine("           ,getdate()               ");
               insertSql.AppendLine("           ,@Remark               ");
               insertSql.AppendLine("           ,@Creator)                ");
               //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
               #endregion
               //定义插入基本信息的命令
               SqlCommand comm = new SqlCommand();
               comm.CommandText = insertSql.ToString();
               //设置保存的参数
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD ));	//公司代码
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo ));	//类型名称
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title ));	//创建人
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag", model.TaskFlag ));	//启用状态
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskDate", model.TaskDate ));	//更新用户ID
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNum", model.TaskNum ));	//更新用户ID
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate ));	//更新用户ID
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate ));	//更新用户ID
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", model.Status ));	//更新用户ID
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark ));	//更新用户ID
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", model.Creator ));	//更新用户ID

               //添加返回参数
               //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

               //执行插入操作
               bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);

               return isSucc;


       }
       /// <summary>
       /// 批插入模板信息
       /// </summary>
       /// <param name="modeList"></param>
       /// <returns></returns>
       public static bool UpdatePerTaskInfo(PerformanceTaskModel model, IList<PerformanceScoreModel> modeList, IList<PerformanceSummaryModel> modleSummaryList)
       {

           #region 插入SQL拼写
           StringBuilder insertSql = new StringBuilder();
           insertSql.AppendLine("update  officedba.PerformanceTask ");
           insertSql.AppendLine("        set   Title=@Title              ");
           insertSql.AppendLine("           ,TaskFlag=@TaskFlag                 ");
           insertSql.AppendLine("           ,TaskDate=@TaskDate           ");
           insertSql.AppendLine("           ,TaskNum=@TaskNum               ");
           insertSql.AppendLine("           ,StartDate=@StartDate               ");
           insertSql.AppendLine("           ,EndDate=@EndDate               ");
           insertSql.AppendLine("           ,ModifiedDate=getdate()               ");
           insertSql.AppendLine("           ,ModifiedUserID=@ModifiedUserID               ");
           insertSql.AppendLine("           ,Remark=@Remark    where   CompanyCD=@CompanyCD and  TaskNo=@TaskNo          ");
           //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
           #endregion
           //定义插入基本信息的命令
           SqlCommand comm = new SqlCommand();
           comm.CommandText = insertSql.ToString();
           //设置保存的参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));	//类型名称
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));	//创建人
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag", model.TaskFlag));	//启用状态
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskDate", model.TaskDate));	//更新用户ID
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNum", model.TaskNum));	//更新用户ID
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));	//更新用户ID
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));	//更新用户ID
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));	//更新用户ID
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));	//更新用户ID

           //添加返回参数
           //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

           //执行插入操作
           bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);

           if (isSucc)
           {
               if (DeleteByTaskNo(model.TaskNo, model.CompanyCD))
               {
                   if (InsertPerScoreInfo(modeList))
                   {
                       isSucc = true;
                   }
                   else
                   {
                       isSucc = false;
                   }
               }
               else
               {
                   isSucc = false;
               }
               if (DeleteByTaskNoInSummary(model.TaskNo, model.CompanyCD))
               {
                   if (InsertPerSummaryInfo(modleSummaryList))
                   {
                       isSucc = true;
                   }
                   else
                   {
                       isSucc = false;
                   }
               }
               else
               {
                   isSucc = false;
               }










           }
           else
           { isSucc = false; }



           return isSucc;


       }
       /// <summary>
       /// 删除模板
       /// </summary>
       /// <param name="modeList"></param>
       /// <returns></returns>
       public static bool DeleteByTaskNo(string TaskNo, string CompanyCD)
       {

           #region 插入SQL拼写
           StringBuilder insertSql = new StringBuilder();
           insertSql.AppendLine("delete from officedba.PerformanceScore where  CompanyCD=@CompanyCD  and  TaskNo=@TaskNo");
           //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
           #endregion
           //定义插入基本信息的命令
           SqlCommand comm = new SqlCommand();
           comm.CommandText = insertSql.ToString();
           //设置保存的参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",CompanyCD ));	//公司代码
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", TaskNo ));	//类型名称
           //添加返回参数
           //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

           //执行插入操作
           bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);

           return isSucc;



       }
       /// <summary>
       /// 批插入评分记录表信息
       /// </summary>
       /// <param name="modeList"></param>
       /// <returns></returns>
       public static bool InsertPerScoreInfo(IList<PerformanceScoreModel > modeList)
       {
           bool isSucc = false;
           foreach (PerformanceScoreModel model in modeList)
           {
               #region 插入SQL拼写
               StringBuilder insertSql = new StringBuilder();
               insertSql.AppendLine("INSERT INTO officedba.PerformanceScore ");
               insertSql.AppendLine("           (CompanyCD             ");
               insertSql.AppendLine("           ,TaskNo                ");
               insertSql.AppendLine("           ,TemplateNo              ");
               insertSql.AppendLine("           ,EmployeeID                 ");
               insertSql.AppendLine("           ,ElemID           ");
               insertSql.AppendLine("           ,ElemName               ");
               insertSql.AppendLine("           ,StepNo               ");
               insertSql.AppendLine("           ,StepName               ");
               insertSql.AppendLine("           ,ScoreEmployee               ");
               insertSql.AppendLine("           ,Status               ");
               insertSql.AppendLine("           ,Rate)                 ");

               insertSql.AppendLine("     VALUES                        ");
               insertSql.AppendLine("           (@CompanyCD            ");
               insertSql.AppendLine("           ,@TaskNo               ");
               insertSql.AppendLine("           ,@TemplateNo             ");
               insertSql.AppendLine("           ,@EmployeeID               ");
               insertSql.AppendLine("           ,@ElemID          ");
               insertSql.AppendLine("           ,@ElemName             ");
               insertSql.AppendLine("           ,@StepNo               ");
               insertSql.AppendLine("           ,@StepName               ");
               insertSql.AppendLine("           ,@ScoreEmployee            ");
               insertSql.AppendLine("           ,@Status               ");
               insertSql.AppendLine("           ,@Rate)                ");
               //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
               #endregion
               //定义插入基本信息的命令
               SqlCommand comm = new SqlCommand();
               comm.CommandText = insertSql.ToString();
               //设置保存的参数
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));	//类型名称
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo ));	//创建人
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID ));	//启用状态
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@ElemID", model.ElemID ));	//更新用户ID
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@ElemName", model.ElemName ));	//更新用户ID
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@StepNo", model.StepNo ));	//更新用户ID
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@StepName", model.StepName ));	//更新用户ID
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@ScoreEmployee", model.ScoreEmployee ));	//更新用户ID
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate", model.Rate ));	//更新用户ID
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", model.Status));
               //添加返回参数
               //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

               //执行插入操作
                isSucc = SqlHelper.ExecuteTransWithCommand(comm);
               if (!isSucc)
               {
                   isSucc= false;break; 
               }
               else
               {
                   continue;
               }
           }
           return isSucc;



       }
       public static DataTable SearchTaskInfo(PerformanceTaskModel  model)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine(" SELECT isnull( Convert(varchar(100),ModifiedDate,23),'') AS ModifiedDate,                            ");
           searchSql.AppendLine("     	   ID  	,CASE when taskflag='2' and TaskNum='1' then TaskDate+'年第一季度'");
           searchSql.AppendLine("    	  when taskflag='2' and TaskNum='2' then TaskDate+'年第二季度'");
           searchSql.AppendLine("    	  when taskflag='2' and TaskNum='3' then TaskDate+'年第三季度'");
           searchSql.AppendLine("      	  when taskflag='2' and TaskNum='4' then TaskDate+'年第四季度'");
           searchSql.AppendLine("   	  when taskflag='3' and TaskNum='1' then TaskDate+'年上半年'");
           searchSql.AppendLine("    when taskflag='3' and TaskNum='2' then TaskDate+'年下半年'");
           searchSql.AppendLine("     when taskflag='1' then TaskDate+'年'+cast(TaskNum as varchar)+'月' ");
           searchSql.AppendLine("     when taskflag='4' then TaskDate+'年'+isnull( cast(TaskNum as varchar),'') ");
           searchSql.AppendLine("   when taskflag='5' then isnull(cast(TaskNum as varchar),'') ");
           searchSql.AppendLine("   when taskflag is null then ''");
           searchSql.AppendLine("    end as TaskNum                                 ");
           searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), StartDate,21),'') as StartDate");
           searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), EndDate,21),'') as EndDate");
           searchSql.AppendLine(" 	, ISNULL( CONVERT(VARCHAR(10), TaskDate,21),'') as TaskDate");
               searchSql.AppendLine("   ,CASE when TaskFlag='1'  then '月考核'");
               searchSql.AppendLine("     when TaskFlag='2'  then '季考核'");
               searchSql.AppendLine("     when TaskFlag='3'  then '半年考核'");
               searchSql.AppendLine("     when TaskFlag='4'  then '年考核'");
               searchSql.AppendLine("   	  when TaskFlag='5'  then '临时考核'");
               searchSql.AppendLine("   	  when TaskFlag is null  then ''");
               searchSql.AppendLine("   	 end as TaskFlag      ");
               searchSql.AppendLine(" 	,isnull( Title,'') as Title ");
               searchSql.AppendLine(" 	,TaskNo ");
           searchSql.AppendLine(" FROM    officedba.PerformanceTask   ");
           searchSql.AppendLine(" WHERE	CompanyCD = @CompanyCD            ");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

           //l
           if (!string.IsNullOrEmpty(model.TaskNo ))
           {
               searchSql.AppendLine(" AND TaskNo like @TaskNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo","%" + model.TaskNo + "%"));
           }

             if (!string.IsNullOrEmpty(model.TaskFlag  ))
           {
                
               searchSql.AppendLine(" AND TaskFlag = @TaskFlag ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag", model.TaskFlag));
           }

             if (model.TaskFlag != "4" || model.TaskFlag != "5")
             {
                 if (!string.IsNullOrEmpty(model.TaskNum))
                 {
                     searchSql.AppendLine(" AND TaskNum = @TaskNum ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNum", model.TaskNum));
                 }
             }
             if (!string.IsNullOrEmpty(model.TaskDate ))
             {
                 searchSql.AppendLine(" AND TaskDate =@TaskDate ");
                 comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskDate", model .TaskDate ));
             }
           if (!string.IsNullOrEmpty(model.Title ))
           {
               searchSql.AppendLine(" AND Title LIKE @Title ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + model.Title + "%"));
           }
           //启用状态

           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       public static DataTable SearchTaskForDetails(string companyCD, string taskNo)
       {
         
           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine(" SELECT   a.ID,a.TaskNo,isnull(a.Title,'') as Title, CONVERT(VARCHAR(10),a.TaskDate,21) AS TaskDate, CONVERT(VARCHAR(10),a.StartDate,21) AS StartDate,CONVERT(VARCHAR(10),a.EndDate,21) AS EndDate,isnull(a.Remark,'') as Remark ,b.TemplateNo,b.EmployeeID   ");
           searchSql.AppendLine("     	,CASE when a.TaskFlag='2' and a.TaskNum='1' then a.TaskDate+'年第一季度'");
           searchSql.AppendLine("    	  when a.TaskFlag='2' and a.TaskNum='2' then a.TaskDate+'年第二季度'");
           searchSql.AppendLine("    	  when a.TaskFlag='2' and a.TaskNum='3' then a.TaskDate+'年第三季度'");
           searchSql.AppendLine("      	  when a.TaskFlag='2' and a.TaskNum='4' then a.TaskDate+'年第四季度'");
           searchSql.AppendLine("   	  when a.TaskFlag='3' and a.TaskNum='1' then a.TaskDate+'年上半年'");
           searchSql.AppendLine("    when a.TaskFlag='3' and a.TaskNum='2' then a.TaskDate+'年下半年'");
           searchSql.AppendLine("     when a.TaskFlag='1' then a.TaskDate+'年'+cast(a.TaskNum as varchar)+'月' ");
           searchSql.AppendLine("     when a.TaskFlag='4' then a.TaskDate+'年'+isnull( cast(a.TaskNum as varchar),'') ");
           searchSql.AppendLine("   when a.TaskFlag='5' then isnull(cast(TaskNum as varchar),'') ");
           searchSql.AppendLine("   when a.TaskFlag is null then ''");
           searchSql.AppendLine("    end as TaskNum,CONVERT(VARCHAR(10),b.ScoreDate,21) AS ScoreDate                                 ");
                 searchSql.AppendLine("   ,CASE when a.TaskFlag='1'  then '月考核'");
            searchSql.AppendLine("     when a.TaskFlag='2'  then '季考核'");
            searchSql.AppendLine("     when a.TaskFlag='3'  then '半年考核'");
            searchSql.AppendLine("     when a.TaskFlag='4'  then '年考核'");
            searchSql.AppendLine("   	  when a.TaskFlag='5'  then '临时考核'");
            searchSql.AppendLine("   	  when a.TaskFlag is null  then ''");
            searchSql.AppendLine("   	 end as TaskFlag");
            searchSql.AppendLine(" FROM    officedba.PerformanceTask a left outer join  officedba.PerformanceScore b  on   b.CompanyCD=a.CompanyCD  and   a.TaskNo=b.TaskNo       ");
           searchSql.AppendLine(" WHERE	a.CompanyCD = @CompanyCD            ");
           searchSql.AppendLine(" and 	a.TaskNo = @TaskNo            ");
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


       public static DataTable SearchTaskInfoByTaskNO(string companyCD, string taskNo)
       {
          
           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine(" SELECT   a.ID,a.TaskNo,isnull(a.Title,'') as Title,a.TaskFlag, CONVERT(VARCHAR(10),a.TaskDate,21) AS TaskDate, a.TaskNum, CONVERT(VARCHAR(10),a.StartDate,21) AS StartDate,CONVERT(VARCHAR(10),a.EndDate,21) AS EndDate,isnull(a.Remark,'') as Remark ,b.TemplateNo,b.EmployeeID  FROM    officedba.PerformanceTask a left outer join  officedba.PerformanceScore b  on    b.CompanyCD=a.CompanyCD and a.TaskNo=b.TaskNo  ");
           searchSql.AppendLine(" WHERE	a.CompanyCD = @CompanyCD            ");
           searchSql.AppendLine(" and 	a.TaskNo = @TaskNo            ");
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
       public static bool IsExist(string TaskNo, string CompanyCD)
       {
           //校验SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine(" SELECT                       ");
           searchSql.AppendLine(" 	COUNT(*) AS UsedCount       ");
           searchSql.AppendLine(" FROM                         ");
           searchSql.AppendLine(" 	officedba.PerformanceTask ");
           searchSql.AppendLine(" WHERE  CompanyCD='"+CompanyCD+"'");
           searchSql.AppendLine("and TaskNo='"+TaskNo+"'");

           //执行查询
           DataTable dtCount = SqlHelper.ExecuteSql(searchSql.ToString());
           //获取记录数
           int count = GetSafeData.ValidateDataRow_Int(dtCount.Rows[0], "UsedCount");

           //返回结果
           return count > 0 ? true : false;
       }
       public static bool DeletePerTypeInfo(string elemID, string CompanyCD)
       {
           //删除SQL拼写

           StringBuilder deleteSql = new StringBuilder();
           deleteSql.AppendLine(" DELETE FROM officedba.PerformanceTask ");
           deleteSql.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "'");
           deleteSql.AppendLine("and   TaskNo='" + elemID + "'");

           //定义更新基本信息的命令
           SqlCommand comm = new SqlCommand();
           comm.CommandText = deleteSql.ToString();

           //执行插入操作并返回更新结果
           bool result= SqlHelper.ExecuteTransWithCommand(comm);
           if (result)
           {
               StringBuilder deleteSq = new StringBuilder();
               deleteSq.AppendLine(" DELETE FROM officedba.PerformanceScore ");
               deleteSq.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "'");
               deleteSq.AppendLine("and  TaskNo='" + elemID + "'");

               //定义更新基本信息的命令
               SqlCommand commm = new SqlCommand();
               commm.CommandText = deleteSq.ToString();
               bool resul = SqlHelper.ExecuteTransWithCommand(commm);
               if (resul)
               {
                   StringBuilder deleteS = new StringBuilder();
                   deleteS.AppendLine(" DELETE FROM officedba.PerformanceSummary ");
                   deleteS.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "'");
                   deleteS.AppendLine("and  TaskNo='"+elemID+"'");

                   //定义更新基本信息的命令
                   SqlCommand commmm = new SqlCommand();
                   commmm.CommandText = deleteS.ToString();
                   bool resu = SqlHelper.ExecuteTransWithCommand(commmm);
                   if (resu)
                   {
                       return true;
                   }
                   else
                   {
                       return false;
                   }
               }
               else
               {
                   return false;
               }
           }
           else
           {
               return false;
           }
       }
       public static bool InsertPerSummaryInfo(IList<PerformanceSummaryModel> modeList)
       {
           foreach (PerformanceSummaryModel model in modeList)
           {
               #region 插入SQL拼写
               StringBuilder insertSql = new StringBuilder();
               insertSql.AppendLine("INSERT INTO officedba.PerformanceSummary ");
               insertSql.AppendLine("           (CompanyCD             ");
               insertSql.AppendLine("           ,TaskNo                ");
               insertSql.AppendLine("           ,TemplateNo              ");
               insertSql.AppendLine("           ,EmployeeID  )               ");

               insertSql.AppendLine("     VALUES                        ");
               insertSql.AppendLine("           (@CompanyCD            ");
               insertSql.AppendLine("           ,@TaskNo               ");
               insertSql.AppendLine("           ,@TemplateNo             ");
               insertSql.AppendLine("           ,@EmployeeID     )          ");
               //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
               #endregion
               //定义插入基本信息的命令
               SqlCommand comm = new SqlCommand();
               comm.CommandText = insertSql.ToString();
               //设置保存的参数
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));	//类型名称
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo));	//创建人
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));	//启用状态

               //添加返回参数
               //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

               //执行插入操作
               bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
               if (!isSucc)
               {
                   break;
               }
               else
               {
                   continue;
               }
           }
           return true;



       }
       /// <summary>
       /// 删除模板
       /// </summary>
       /// <param name="modeList"></param>
       /// <returns></returns>
       public static bool DeleteByTaskNoInSummary(string TaskNo, string CompanyCD)
       {

           #region 插入SQL拼写
           StringBuilder insertSql = new StringBuilder();
           insertSql.AppendLine("delete from officedba.PerformanceSummary where   CompanyCD=@CompanyCD  and TaskNo=@TaskNo ");
           //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
           #endregion
           //定义插入基本信息的命令
           SqlCommand comm = new SqlCommand();
           comm.CommandText = insertSql.ToString();
           //设置保存的参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));	//公司代码
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", TaskNo));	//类型名称
           //添加返回参数
           //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

           //执行插入操作
           bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);

           return isSucc;



       }
    }
}

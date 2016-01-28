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
    public class PerformanceCheckDBHelper
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
        public static DataTable GetTemplatebyNO(string CompanyCD, string TemplateNo)
        {
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select a.StepNo,ISNULL( a.StepName,'') as StepName ,a.Rate,a.ScoreEmployee,c.ElemID,isnull( d.ElemName,'') as ElemName,a.TemplateNo from officedba.PerformanceTemplateEmp  a left outer join officedba.PerformanceTemplate b on a.CompanyCD=b.CompanyCD  and a.TemplateNo=b.TemplateNo  left outer join officedba.PerformanceTemplateElem c on a.CompanyCD=c.CompanyCD and b.TemplateNo=c.TemplateNo   left outer join officedba.PerformanceElem d on d.CompanyCD=a.CompanyCD and  c.ElemID=d.ID and a.TemplateNo=@TemplateNo and a.CompanyCD=@CompanyCD ");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", TemplateNo));
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
            searchSql.AppendLine("select distinct(EmployeeID),isnull(b.EmployeeName,'') as EmployeeName from officedba.PerformanceTemplateEmp a left outer join officedba.EmployeeInfo b on b.CompanyCD=a.CompanyCD and  a.EmployeeID=b.ID left outer join officedba.PerformanceTemplate c on  c.CompanyCD=a.CompanyCD  and  a.TemplateNo=c.TemplateNo where a.CompanyCD=@CompanyCD  and c.UsedStatus=@UsedStatus ");
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
        public static DataTable GetEmployeeInfoByTemplateNo(string TemplateNo, string CompanyCD)
        {
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select distinct(EmployeeID),ISNULL(b.EmployeeName,'') as EmployeeName from officedba.PerformanceTemplateEmp a left outer join officedba.EmployeeInfo b on b.CompanyCD=a.CompanyCD and a.EmployeeID=b.ID left outer join officedba.PerformanceTemplate c on  a.CompanyCD=c.CompanyCD  and a.TemplateNo=c.TemplateNo  where a.CompanyCD=@CompanyCD and c.UsedStatus=@UsedStatus and a.TemplateNo=@TemplateNo ");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", "1"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", TemplateNo));
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
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));	//类型名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));	//创建人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag", model.TaskFlag));	//启用状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskDate", model.TaskDate));	//更新用户ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNum", model.TaskNum));	//更新用户ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));	//更新用户ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));	//更新用户ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", model.Status));	//更新用户ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));	//更新用户ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", model.Creator));	//更新用户ID

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
        public static bool UpdatePerScoreInfo(IList<PerformanceScoreModel> modeList)
        {
            bool isSucc = false;
            foreach (PerformanceScoreModel model in modeList)
            {
                #region 插入SQL拼写
                StringBuilder insertSql = new StringBuilder();
                insertSql.AppendLine("update  officedba.PerformanceScore ");
                insertSql.AppendLine("        set   Score=@Score              ");
                insertSql.AppendLine("           ,ScoreDate=getdate()                ");
                insertSql.AppendLine("           ,AdviceNote=@AdviceNote           ");
                insertSql.AppendLine("           ,Note=@Note               ");
                insertSql.AppendLine("           ,ModifiedDate=getdate()              ");
                insertSql.AppendLine("           ,ModifiedUserID=@ModifiedUserID               ");
                insertSql.AppendLine("           ,Status=@Status               ");
                insertSql.AppendLine("              where   CompanyCD=@CompanyCD and TaskNo=@TaskNo and TemplateNo=@TemplateNo and ElemID=@ElemID   and EmployeeID=@EmployeeID and ScoreEmployee=@ScoreEmployee    and StepNo=@StepNo    ");
                //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
                #endregion
                //定义插入基本信息的命令
                SqlCommand comm = new SqlCommand();
                comm.CommandText = insertSql.ToString();
                //设置保存的参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));	//类型名称
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo ));	//模板编号
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Score", model.Score));	//创建人
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdviceNote", model.AdviceNote));	//启用状态
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Note", model.Note));	//更新用户ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));	//更新用户ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", model.Status));	//更新用户ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));	//更新用户ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ElemID", model.ElemID ));	//更新用户ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ScoreEmployee", model.ScoreEmployee ));	//更新用户ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StepNo", model.StepNo ));	//更新用户ID

                //添加返回参数
                //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

                //执行插入操作
                isSucc = SqlHelper.ExecuteTransWithCommand(comm);
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
            insertSql.AppendLine("delete from officedba.PerformanceScore where   CompanyCD=@CompanyCD  and TaskNo=@TaskNo");
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
        /// <summary>
        /// 批插入评分记录表信息
        /// </summary>
        /// <param name="modeList"></param>
        /// <returns></returns>
        public static bool InsertPerScoreInfo(IList<PerformanceScoreModel> modeList)
        {
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
                insertSql.AppendLine("           ,@Rate)                ");
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
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ElemID", model.ElemID));	//更新用户ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ElemName", model.ElemName));	//更新用户ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StepNo", model.StepNo));	//更新用户ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StepName", model.StepName));	//更新用户ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ScoreEmployee", model.ScoreEmployee));	//更新用户ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate", model.Rate));	//更新用户ID

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
        public static DataTable SearchTaskInfo(PerformanceTaskModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                             ");
            searchSql.AppendLine(" 	 ID                               ");
            if (model.TaskFlag == "1")
            {
                searchSql.AppendLine(" 	,CASE TaskNum                  ");
                searchSql.AppendLine(" 	WHEN '1' THEN '1月'              ");
                searchSql.AppendLine(" 	WHEN '2' THEN '2月'              ");
                searchSql.AppendLine(" 	WHEN '3' THEN '3月'              ");
                searchSql.AppendLine(" 	WHEN '4' THEN '4月'              ");
                searchSql.AppendLine(" 	WHEN '5' THEN '5月'              ");
                searchSql.AppendLine(" 	WHEN '6' THEN '6月'              ");
                searchSql.AppendLine(" 	WHEN '7' THEN '7月'              ");
                searchSql.AppendLine(" 	WHEN '8' THEN '8月'              ");
                searchSql.AppendLine(" 	WHEN '9' THEN '9月'              ");
                searchSql.AppendLine(" 	WHEN '10' THEN '10月'              ");
                searchSql.AppendLine(" 	WHEN '11' THEN '12月'              ");
                searchSql.AppendLine(" 	WHEN '12' THEN '12月'              ");
                searchSql.AppendLine(" 	ELSE ''                           ");
                searchSql.AppendLine(" 	END AS TaskNum             ");

            }
            if (model.TaskFlag == "2")
            {
                searchSql.AppendLine(" 	,CASE TaskNum                  ");
                searchSql.AppendLine(" 	WHEN '1' THEN TaskDate+'第一季度'              ");
                searchSql.AppendLine(" 	WHEN '2' THEN TaskDate+'第二季度'               ");
                searchSql.AppendLine(" 	WHEN '3' THEN TaskDate+'第三季度'               ");
                searchSql.AppendLine(" 	WHEN '4' THEN TaskDate+'第四季度'               ");
                searchSql.AppendLine(" 	ELSE ''                           ");
                searchSql.AppendLine(" 	END AS TaskNum             ");

            }
            if (model.TaskFlag == "3")
            {
                searchSql.AppendLine(" 	,CASE TaskNum                  ");
                searchSql.AppendLine(" 	WHEN '1' THEN TaskDate+'上半年'              ");
                searchSql.AppendLine(" 	WHEN '2' THEN TaskDate+'下半年'               ");
                searchSql.AppendLine(" 	ELSE ''                           ");
                searchSql.AppendLine(" 	END AS TaskNum             ");

            }
            if (model.TaskFlag == "4" || model.TaskFlag == "5")
            {
                searchSql.AppendLine(" 	,TaskNum                  ");
            }

            searchSql.AppendLine(" 	,StartDate ");
            searchSql.AppendLine(" 	,EndDate ");
            searchSql.AppendLine(" 	,TaskFlag ");
            searchSql.AppendLine(" 	,Title ");
            searchSql.AppendLine(" 	,TaskNo ");
            searchSql.AppendLine(" 	,TaskDate ");
            searchSql.AppendLine(" FROM    officedba.PerformanceTask   ");
            searchSql.AppendLine(" WHERE	CompanyCD = @CompanyCD            ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //l
            if (!string.IsNullOrEmpty(model.TaskNo))
            {
                searchSql.AppendLine(" AND TaskNo = @TaskNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));
            }

            if (!string.IsNullOrEmpty(model.TaskFlag))
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
            if (!string.IsNullOrEmpty(model.Title))
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

        public static DataTable CheckTaskStep(string companyCD, string taskNo, string employeeID, string templateNo)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("  select a.StepNo,a.Score,a.Status,a.ScoreEmployee,CONVERT(VARCHAR(10),a.ScoreDate,21) AS ScoreDate ");
            searchSql.AppendLine(" from officedba.PerformanceScore a left outer join  officedba.PerformanceElem b on a.ElemID=b.ID and a.CompanyCD=b.CompanyCD  left outer join officedba.PerformanceTask c on a.TaskNo=c.TaskNo and c.CompanyCD=a.CompanyCD  left outer join officedba.PerformanceElem d  on b.ParentElemNo=d.ID  and d.CompanyCD=a.CompanyCD left outer join officedba.PerformanceTemplateElem e  on a.TemplateNo=e.TemplateNo and e.CompanyCD=a.CompanyCD  and a.ElemID=e.ElemID        ");
            searchSql.AppendLine(" where  a.CompanyCD = @CompanyCD            ");
            searchSql.AppendLine(" and 	a.TaskNo = @TaskNo  and a.TemplateNo=@TemplateNo   and    a.EmployeeID=@EmployeeID   ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", employeeID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", taskNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", templateNo));
            //启用状态

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        public static DataTable SearchTaskInfoByTaskNO(string companyCD, string taskNo, string scoreEmployee, string employeeID,string  templateNo)
        {
        
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("  select isnull(a.AdviceNote,'') as AdviceNote ,isnull(a.Note,'') as Note,isnull( a.Score,'') as Score,e.Rate as ElemRate,a.Rate as StepRate, a.Status, a.EmployeeID,isnull( d.ElemName,'') as ParentElemName,a.ElemID,isnull(a.ElemName,'') as ElemName,isnull(b.ScoreRules,'') as ScoreRules,isnull(b.StandardScore,'') as StandardScore,isnull(b.MinScore,'') as MinScore,isnull(b.MaxScore,'') as MaxScore,isnull( b.AsseStandard,'') as AsseStandard, isnull(b.AsseFrom,'') as AsseFrom ,isnull(b.Remark,'') as ElemRemark ,c.TaskNo,isnull(c.Title,'') as Title,CONVERT(VARCHAR(10),c.StartDate,21) AS StartDate ,CONVERT(VARCHAR(10),c.EndDate,21) AS EndDate ,isnull(c.Remark,'') as Remark,a.StepNo,a.StepName,a.Status ,CONVERT(VARCHAR(10),a.ScoreDate,21) AS ScoreDate   ");
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
            searchSql.AppendLine("   	 end as TaskFlag      ");
            searchSql.AppendLine(" from officedba.PerformanceScore a left outer join  officedba.PerformanceElem b on  b.CompanyCD=a.CompanyCD and a.ElemID=b.ID left outer join officedba.PerformanceTask c on c.CompanyCD=a.CompanyCD and a.TaskNo=c.TaskNo    left outer join officedba.PerformanceElem d  on  d.CompanyCD=a.CompanyCD  and  b.ParentElemNo=d.ID left outer join officedba.PerformanceTemplateElem e  on  e.CompanyCD=a.CompanyCD and a.TemplateNo=e.TemplateNo and a.ElemID=e.ElemID        ");
            searchSql.AppendLine(" where  a.CompanyCD = @CompanyCD and             ");
            searchSql.AppendLine("   	a.TaskNo = @TaskNo and  a.TemplateNo=@TemplateNo  and a.ScoreEmployee=@ScoreEmployee    and  a.EmployeeID=@EmployeeID       ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", employeeID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", templateNo ));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", taskNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ScoreEmployee", scoreEmployee));
            //启用状态

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        public static DataTable SearchStepInfoByTaskNO(string companyCD, string taskNo, string stepNo, string employeeID, string templateNo)
        {

            #region 查询语句
            //查询SQL拼写 
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("  select isnull(a.AdviceNote,'') as AdviceNote ,isnull(a.Note,'') as Note,isnull( a.Score,'') as Score,e.Rate as ElemRate,a.Rate as StepRate, a.Status, a.EmployeeID,isnull( d.ElemName,'') as ParentElemName,a.ElemID,isnull(a.ElemName,'') as ElemName,isnull(b.ScoreRules,'') as ScoreRules,isnull(b.StandardScore,'') as StandardScore,isnull(b.MinScore,'') as MinScore,isnull(b.MaxScore,'') as MaxScore,isnull( b.AsseStandard,'') as AsseStandard, isnull(b.AsseFrom,'') as AsseFrom ,isnull(b.Remark,'') as ElemRemark ,c.TaskNo,isnull(c.Title,'') as Title,CONVERT(VARCHAR(10),c.StartDate,21) AS StartDate ,CONVERT(VARCHAR(10),c.EndDate,21) AS EndDate ,isnull(c.Remark,'') as Remark,a.StepNo,a.StepName,a.Status,CONVERT(VARCHAR(10),a.ScoreDate,21) AS ScoreDate    ");
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
            searchSql.AppendLine("   	 end as TaskFlag,f.EmployeeName as ScoreEmpName      ");
            searchSql.AppendLine(" from officedba.PerformanceScore a left outer join  officedba.PerformanceElem b on   a.CompanyCD=b.CompanyCD and a.ElemID=b.ID  left outer join officedba.PerformanceTask c on    a.CompanyCD=c.CompanyCD and a.TaskNo=c.TaskNo  left outer join officedba.PerformanceElem d  on  d.CompanyCD=a.CompanyCD   and b.ParentElemNo=d.ID   left outer join officedba.PerformanceTemplateElem e  on  e.CompanyCD=a.CompanyCD and a.TemplateNo=e.TemplateNo   and a.ElemID=e.ElemID      left outer join officedba.EmployeeInfo f on a.ScoreEmployee=f.ID   ");
            searchSql.AppendLine(" where     a.CompanyCD = @CompanyCD  and   a.TaskNo = @TaskNo   and  a.TemplateNo=@TemplateNo       ");
            searchSql.AppendLine(" and  a.EmployeeID=@EmployeeID and a.Score is not null        ");
              
            if (!string.IsNullOrEmpty(stepNo))
            {
                searchSql.AppendLine(" and 	    a.StepNo=@StepNo    ");
            }
            searchSql.AppendLine(" order by  a.StepNo desc    ");
            #endregion
            
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", employeeID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", taskNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", templateNo));
            if (!string.IsNullOrEmpty(stepNo))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StepNo", stepNo));
            }
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
            searchSql.AppendLine(" WHERE  CompanyCD='"+CompanyCD+"' ");
            searchSql.AppendLine("and TaskNo ='"+TaskNo+"'");

            //执行查询
            DataTable dtCount = SqlHelper.ExecuteSql(searchSql.ToString());
            //获取记录数
            int count = GetSafeData.ValidateDataRow_Int(dtCount.Rows[0], "UsedCount");

            //返回结果
            return count > 0 ? true : false;
        }
       
        public static bool UpdateTask(string TaskNo, string CompanyCD)
        {

            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine("update officedba.PerformanceTask set Status='4'  where  CompanyCD=@CompanyCD and TaskNo=@TaskNo  ");
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
        public static bool DeletePerTypeInfo(string elemID, string CompanyCD)
        {
            //删除SQL拼写

            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.PerformanceTask ");
            deleteSql.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "'");
            deleteSql.AppendLine("and  TaskNo='"+elemID+"'");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //执行插入操作并返回更新结果
            bool result = SqlHelper.ExecuteTransWithCommand(comm);
            if (result)
            {
                StringBuilder deleteSq = new StringBuilder();
                deleteSq.AppendLine(" DELETE FROM officedba.PerformanceScore ");
                deleteSq.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "'");
                deleteSq.AppendLine("and  TaskNo='"+elemID+"'");

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
                    return false;
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
            insertSql.AppendLine("delete from officedba.PerformanceSummary where  CompanyCD=@CompanyCD  and TaskNo=@TaskNo ");
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

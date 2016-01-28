/**********************************************
 * 类作用：   新建招聘活动
 * 建立人：   吴志强
 * 建立时间： 2009/03/30
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

namespace XBase.Data.Office.HumanManager
{
    /// <summary>
    /// 类名：RectPlanDBHelper
    /// 描述：新建招聘活动
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/30
    /// 最后修改时间：2009/03/30
    /// </summary>
    ///
    public class RectPlanDBHelper
    {

        #region 通过ID查询招聘活动信息
        /// <summary>
        /// 查询招聘活动信息
        /// </summary>
        /// <param name="rectPlanyID">招聘活动ID</param>
        /// <returns></returns>
        public static DataSet GetRectPlanInfoWithID(string rectPlanyID, string companyCD)
        {
            //定义返回的数据变量
            DataSet dsRectPlanInfo = new DataSet();

            #region 查询招聘活动信息
            StringBuilder planSql = new StringBuilder();
            planSql.AppendLine(" SELECT A.ID               ");
            planSql.AppendLine("       ,A.CompanyCD        ");
            planSql.AppendLine("       ,A.PlanNo           ");
            planSql.AppendLine("       ,isnull(A.Title,'') as   Title          ");
             planSql.AppendLine("       ,A.Status            ");
             planSql.AppendLine("       ,A.PlanFee        "); 
             planSql.AppendLine("       ,isnull(A.FeeNote,'') as   FeeNote          ");
             planSql.AppendLine("       ,isnull(A.JoinMan,'') as   JoinMan          ");
             planSql.AppendLine("       ,isnull(A.JoinNote,'') as   JoinNote          ");
             planSql.AppendLine("       ,A.RequireNum      ");
            planSql.AppendLine("       ,CONVERT(VARCHAR(10),A.StartDate,21) AS StartDate ");
            planSql.AppendLine("       ,CONVERT(VARCHAR(10),A.EndDate,21) AS EndDate ");
            planSql.AppendLine("       ,A.Principal        ");
            planSql.AppendLine("       ,isnull( B.EmployeeName,'') AS PrincipalName  ");
            planSql.AppendLine(" FROM officedba.RectPlan A ");
            planSql.AppendLine(" left join officedba.EmployeeInfo B ");
            planSql.AppendLine(" on B.companyCD=A.companyCD AND A.Principal = B.ID ");
            planSql.AppendLine(" WHERE  A.companyCD=@companyCD ");
            planSql.AppendLine(" and   A.ID = @RectPlanID");
            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //招聘活动ID
            param[0] = SqlHelper.GetParameter("@RectPlanID", rectPlanyID);
            param[1] = SqlHelper.GetParameter("@companyCD", companyCD);
            //执行查询
            DataTable dtBaseInfo = new DataTable("BaseInfo");
            dtBaseInfo = SqlHelper.ExecuteSql(planSql.ToString(), param);
            //设置招聘活动基本信息
            dsRectPlanInfo.Tables.Add(dtBaseInfo);
            //招聘活动信息存在时，查询招聘目标以及信息发布信息
            if (dtBaseInfo.Rows.Count > 0)
            {
                //获取公司代码
                string companyCD2 = dtBaseInfo.Rows[0]["CompanyCD"].ToString();
                //获取招聘活动编号
                string planNo = dtBaseInfo.Rows[0]["PlanNo"].ToString();
                //设置招聘目标
                dsRectPlanInfo.Tables.Add(GetGoalInfoWithID(companyCD2, planNo));
                //设置信息发布
                dsRectPlanInfo.Tables.Add(GetPublishInfoWithID(companyCD2, planNo));

            }

            #endregion

            return dsRectPlanInfo;
        }

        #region 通过公司代码以及招聘活动编号招聘目标信息
        /// <summary>
        /// 通过公司代码以及招聘活动编号获取招聘目标
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="planNo">招聘活动编号</param>
        /// <returns></returns>
        private static DataTable GetGoalInfoWithID(string companyCD, string planNo)
        {
            DataTable dtGoalInfo = new DataTable("GoalInfo");
            //查询语句
            StringBuilder selectSql = new StringBuilder();
            selectSql.AppendLine(" SELECT                   ");
            selectSql.AppendLine("    a.ApplyDept             ");
            selectSql.AppendLine("   ,a.PositionTitle         ");
            selectSql.AppendLine("   ,a.PositionID         ");
            selectSql.AppendLine("   ,a.PersonCount           ");
            selectSql.AppendLine("   ,a.Sex                   ");
            selectSql.AppendLine("   ,a.Age                   ");
            selectSql.AppendLine("   ,a.CultureLevel          ");
            selectSql.AppendLine("   ,a.Professional          ");
            selectSql.AppendLine("   ,a.Requisition           ");
            selectSql.AppendLine("   ,a.CompleteDate          ");
            selectSql.AppendLine("   ,a.WorkAge          ");
            selectSql.AppendLine("   ,b.DeptName         ");
            selectSql.AppendLine(" FROM                     ");
            selectSql.AppendLine("   officedba.RectGoal  a  left outer join officedba.DeptInfo b   ");
            selectSql.AppendLine("   on B.companyCD=A.companyCD AND a. ApplyDept=b.ID   ");
            selectSql.AppendLine(" WHERE                    ");
            selectSql.AppendLine("   a.CompanyCD = @CompanyCD ");
            selectSql.AppendLine("   AND a.PlanNo = @PlanNo   ");

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //招聘活动ID
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //招聘活动ID
            param[1] = SqlHelper.GetParameter("@PlanNo", planNo);
            //执行查询
            dtGoalInfo = SqlHelper.ExecuteSql(selectSql.ToString(), param);

            return dtGoalInfo;
        }
        #endregion

        #region 通过公司代码以及招聘活动编号获取信息发布
        /// <summary>
        /// 通过公司代码以及招聘活动编号获取信息发布
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="planNo">招聘活动编号</param>
        /// <returns></returns>
        private static DataTable GetPublishInfoWithID(string companyCD, string planNo)
        {
            DataTable dtPublish = new DataTable("PublishInfo");
            //查询语句
            StringBuilder selectSql = new StringBuilder();
            selectSql.AppendLine(" SELECT                      ");
            selectSql.AppendLine("        PublishPlace         ");
            selectSql.AppendLine("       ,PublishDate          ");
            selectSql.AppendLine("       ,Valid                ");
            selectSql.AppendLine("       ,EndDate              ");
            selectSql.AppendLine("       ,Cost                 ");
            selectSql.AppendLine("       ,Effect               ");
            selectSql.AppendLine("       ,Status               ");
            selectSql.AppendLine(" FROM                        ");
            selectSql.AppendLine(" 	officedba.RectPublish      ");
            selectSql.AppendLine(" WHERE                       ");
            selectSql.AppendLine("      CompanyCD = @CompanyCD ");
            selectSql.AppendLine("      AND PlanNo = @PlanNo   ");

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //招聘活动ID
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //招聘活动ID
            param[1] = SqlHelper.GetParameter("@PlanNo", planNo);
            //执行查询
            dtPublish = SqlHelper.ExecuteSql(selectSql.ToString(), param);

            return dtPublish;
        }
        #endregion

        #endregion

        #region 通过检索条件查询招聘活动信息
        /// <summary>
        /// 查询招聘活动信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchRectPlanInfo(RectPlanSearchModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("  select * from (  SELECT A.ID     AS  ID                     ");
            searchSql.AppendLine("       ,A.PlanNo AS  RectPlanNo             ");
            searchSql.AppendLine("       ,A.Title  AS  Title                  "); 
            searchSql.AppendLine("       ,A.Status  AS  Status                  ");
            searchSql.AppendLine("       ,A.Principal  AS  Principal                  ");
            searchSql.AppendLine("       ,CONVERT(VARCHAR(10),A.StartDate,21) ");
            searchSql.AppendLine(" 			AS StartDate                      ");
            searchSql.AppendLine("       ,CONVERT(VARCHAR(10),A.EndDate,21) ");
            searchSql.AppendLine(" 			AS EndDate                      ");
            searchSql.AppendLine("       ,ISNULL(B.EmployeeName, '') AS PrincipalName ");
            searchSql.AppendLine("       , CASE A.Status                      ");
            searchSql.AppendLine(" 		WHEN '0' THEN '未完成'                ");
            searchSql.AppendLine(" 		WHEN '1' THEN '已完成'                ");
            searchSql.AppendLine(" 		ELSE ''                               ");
            searchSql.AppendLine(" 		END AS StatusName                     ");
            searchSql.AppendLine(" 	  ,ISNULL((                               ");
            searchSql.AppendLine(" 		Select SUM(PersonCount)               ");
            searchSql.AppendLine(" 		FROM officedba.RectGoal C             ");
            searchSql.AppendLine(" 		WHERE C.CompanyCD = A.CompanyCD       ");
            searchSql.AppendLine(" 		AND C.PlanNo = A.PlanNo               ");
            searchSql.AppendLine(" 		), 0) AS PersonCount                 ");
            searchSql.AppendLine(" 	  ,(                                      ");
            searchSql.AppendLine(" 		Select COUNT(ID)                      ");
            searchSql.AppendLine(" 		FROM officedba.RectInterview E        ");
            searchSql.AppendLine(" 		WHERE  E.CompanyCD=A.CompanyCD and  E.PlanID = A.PlanNo                ");
            searchSql.AppendLine(" 			AND (E.InterviewResult = '1'  or E.FinalResult is not null  )        ");
            searchSql.AppendLine(" 		) AS ReviewStatus                     ");
            searchSql.AppendLine(" 	  ,(                                      ");
            searchSql.AppendLine(" 		Select COUNT(ID)                      ");
            searchSql.AppendLine(" 		FROM officedba.RectInterview F        ");
            searchSql.AppendLine(" 		WHERE   F.CompanyCD=A.CompanyCD   and   F.PlanID = A.PlanNo         ");
            searchSql.AppendLine(" 			AND F.FinalResult = '1'          ");
            searchSql.AppendLine(" 		) AS EmployStatus ,isnull( Convert(varchar(100),A.ModifiedDate,23),'') AS ModifiedDate                    ");
            searchSql.AppendLine(" FROM officedba.RectPlan A                  ");
            searchSql.AppendLine(" 	left join officedba.EmployeeInfo B        ");
            searchSql.AppendLine(" 		on B.companyCD=A.companyCD AND A.Principal = B.ID                 ");
            searchSql.AppendLine(" WHERE                                      ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD   ) x  where 1=1               ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //活动编号
            if (!string.IsNullOrEmpty(model.RectPlanNo))
            {
                searchSql.AppendLine(" AND x.RectPlanNo LIKE '%' + @RectPlanNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RectPlanNo", model.RectPlanNo));
            }
            //主题
            if (!string.IsNullOrEmpty(model.Title))
            {
                searchSql.AppendLine(" AND x.Title LIKE '%' + @Title + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            //开始时间
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                searchSql.AppendLine(" AND x.StartDate >= @StartDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));
            }
            if (!string.IsNullOrEmpty(model.StartToDate))
            {
                searchSql.AppendLine(" AND x.StartDate <= @StartToDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartToDate", model.StartToDate));
            }
            //负责人
            if (!string.IsNullOrEmpty(model.PrincipalID))
            {
                searchSql.AppendLine(" AND x.Principal = @PrincipalID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PrincipalID", model.PrincipalID));
            }
            //招聘人数
            if (!string.IsNullOrEmpty(model.PersonCount))
            {
                searchSql.AppendLine(" AND x.PersonCount= @PersonCount ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PersonCount", model.PersonCount));
            }
            //活动状态
            if (!string.IsNullOrEmpty(model.StatusID))
            {
                searchSql.AppendLine(" AND  x.Status = @StatusID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StatusID", model.StatusID));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        public static DataTable SearchRectExport(RectPlanSearchModel model,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("  select * from (  SELECT A.ID     AS  ID                     ");
            searchSql.AppendLine("       ,A.PlanNo AS  RectPlanNo             ");
            searchSql.AppendLine("       ,A.Title  AS  Title                  ");
            searchSql.AppendLine("       ,A.Status  AS  Status                  ");
            searchSql.AppendLine("       ,A.Principal  AS  Principal                  ");
            searchSql.AppendLine("       ,CONVERT(VARCHAR(10),A.StartDate,21) ");
            searchSql.AppendLine(" 			AS StartDate                      ");
            searchSql.AppendLine("       ,CONVERT(VARCHAR(10),A.EndDate,21) ");
            searchSql.AppendLine(" 			AS EndDate                      ");
            searchSql.AppendLine("       ,ISNULL(B.EmployeeName, '') AS PrincipalName ");
            searchSql.AppendLine("       , CASE A.Status                      ");
            searchSql.AppendLine(" 		WHEN '0' THEN '未完成'                ");
            searchSql.AppendLine(" 		WHEN '1' THEN '已完成'                ");
            searchSql.AppendLine(" 		ELSE ''                               ");
            searchSql.AppendLine(" 		END AS StatusName                     ");
            searchSql.AppendLine(" 	  ,ISNULL((                               ");
            searchSql.AppendLine(" 		Select SUM(PersonCount)               ");
            searchSql.AppendLine(" 		FROM officedba.RectGoal C             ");
            searchSql.AppendLine(" 		WHERE C.CompanyCD = A.CompanyCD       ");
            searchSql.AppendLine(" 		AND C.PlanNo = A.PlanNo               ");
            searchSql.AppendLine(" 		), 0) AS PersonCount                 ");
            searchSql.AppendLine(" 	  ,(                                      ");
            searchSql.AppendLine(" 		Select COUNT(ID)                      ");
            searchSql.AppendLine(" 		FROM officedba.RectInterview E        ");
            searchSql.AppendLine(" 		WHERE  E.CompanyCD=A.CompanyCD and  E.PlanID = A.PlanNo                ");
            searchSql.AppendLine(" 			AND (E.InterviewResult = '1'  or E.FinalResult is not null  )        ");
            searchSql.AppendLine(" 		) AS ReviewStatus                     ");
            searchSql.AppendLine(" 	  ,(                                      ");
            searchSql.AppendLine(" 		Select COUNT(ID)                      ");
            searchSql.AppendLine(" 		FROM officedba.RectInterview F        ");
            searchSql.AppendLine(" 		WHERE   F.CompanyCD=A.CompanyCD   and   F.PlanID = A.PlanNo         ");
            searchSql.AppendLine(" 			AND F.FinalResult = '1'          ");
            searchSql.AppendLine(" 		) AS EmployStatus ,isnull( Convert(varchar(100),A.ModifiedDate,23),'') AS ModifiedDate                    ");
            searchSql.AppendLine(" FROM officedba.RectPlan A                  ");
            searchSql.AppendLine(" 	left join officedba.EmployeeInfo B        ");
            searchSql.AppendLine(" 		on B.companyCD=A.companyCD AND A.Principal = B.ID                 ");
            searchSql.AppendLine(" WHERE                                      ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD   ) x  where 1=1               ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //活动编号
            if (!string.IsNullOrEmpty(model.RectPlanNo))
            {
                searchSql.AppendLine(" AND x.RectPlanNo LIKE '%' + @RectPlanNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RectPlanNo", model.RectPlanNo));
            }
            //主题
            if (!string.IsNullOrEmpty(model.Title))
            {
                searchSql.AppendLine(" AND x.Title LIKE '%' + @Title + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            //开始时间
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                searchSql.AppendLine(" AND x.StartDate >= @StartDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));
            }
            if (!string.IsNullOrEmpty(model.StartToDate))
            {
                searchSql.AppendLine(" AND x.StartDate <= @StartToDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartToDate", model.StartToDate));
            }
            //负责人
            if (!string.IsNullOrEmpty(model.PrincipalID))
            {
                searchSql.AppendLine(" AND x.Principal = @PrincipalID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PrincipalID", model.PrincipalID));
            }
            //招聘人数
            if (!string.IsNullOrEmpty(model.PersonCount))
            {
                searchSql.AppendLine(" AND x.PersonCount= @PersonCount ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PersonCount", model.PersonCount));
            }
            //活动状态
            if (!string.IsNullOrEmpty(model.StatusID))
            {
                searchSql.AppendLine(" AND  x.Status = @StatusID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StatusID", model.StatusID));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            //return SqlHelper.ExecuteSearch(comm);
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount); //执行查询
        }

        #region 添加招聘活动以及相关信息
        /// <summary>
        /// 添加招聘活动
        /// </summary>
        /// <param name="model">招聘活动信息</param>
        /// <returns></returns>
        public static bool InsertRectPlanInfo(RectPlanModel model)
        {

            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();

            insertSql.AppendLine(" INSERT INTO officedba.RectPlan ");
            insertSql.AppendLine("            (CompanyCD          ");
            insertSql.AppendLine("            ,PlanNo             ");
            insertSql.AppendLine("            ,Title              ");
            insertSql.AppendLine("            ,StartDate          ");
            insertSql.AppendLine("            ,EndDate          ");
            insertSql.AppendLine("            ,Principal          ");
            insertSql.AppendLine("            ,Status             ");
            insertSql.AppendLine("            ,PlanFee              ");
            insertSql.AppendLine("            ,FeeNote          ");
            insertSql.AppendLine("            ,JoinMan          ");
            insertSql.AppendLine("            ,JoinNote          ");
            insertSql.AppendLine("            ,RequireNum             ");
            insertSql.AppendLine("            ,ModifiedDate       ");
            insertSql.AppendLine("            ,ModifiedUserID)    ");
            insertSql.AppendLine("      VALUES                    ");
            insertSql.AppendLine("            (@CompanyCD         ");
            insertSql.AppendLine("            ,@PlanNo            ");
            insertSql.AppendLine("            ,@Title             ");
            insertSql.AppendLine("            ,@StartDate         ");
            insertSql.AppendLine("            ,@EndDate          ");
            insertSql.AppendLine("            ,@Principal         ");
            insertSql.AppendLine("            ,@Status            ");
            insertSql.AppendLine("            ,@PlanFee              ");
            insertSql.AppendLine("            ,@FeeNote          ");
            insertSql.AppendLine("            ,@JoinMan          ");
            insertSql.AppendLine("            ,@JoinNote          ");
            insertSql.AppendLine("            ,@RequireNum             ");
            insertSql.AppendLine("            ,getdate()          ");
            insertSql.AppendLine("            ,@ModifiedUserID)   ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);
            //定义更新列表
            ArrayList lstInsert = new ArrayList();
            //添加基本信息更新命令
            lstInsert.Add(comm);
            //登陆或者更新招聘目标信息
            EditRectGoalInfo(lstInsert, model.GoalList, model.PlanNo, model.CompanyCD, model.ModifiedUserID);
            //登陆或者更新信息发布
            EditRectPublishInfo(lstInsert, model.PublishList, model.PlanNo, model.CompanyCD, model.ModifiedUserID);

            //执行更新操作并返回更新结果
            return SqlHelper.ExecuteTransWithArrayList(lstInsert);

        }
        #endregion

        #region 更新招聘活动以及相关信息
        /// <summary>
        /// 更新招聘活动以及相关信息
        /// </summary>
        /// <param name="model">招聘活动信息</param>
        /// <returns></returns>
        public static bool UpdateRectPlanInfo(RectPlanModel model)
        {

            #region 更新SQL拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.RectPlan               ");
            updateSql.AppendLine("    SET Title = @Title                   ");
            updateSql.AppendLine("       ,StartDate = @StartDate           ");
            updateSql.AppendLine("       ,EndDate = @EndDate           ");
            updateSql.AppendLine("       ,Principal = @Principal           ");
            updateSql.AppendLine("       ,Status = @Status                 ");
            updateSql.AppendLine("            ,PlanFee=@PlanFee              ");
            updateSql.AppendLine("            ,FeeNote=@FeeNote          ");
            updateSql.AppendLine("            ,JoinMan=@JoinMan          ");
            updateSql.AppendLine("            ,JoinNote=@JoinNote          ");
            updateSql.AppendLine("            ,RequireNum=@RequireNum             ");
            updateSql.AppendLine("       ,ModifiedDate = getdate()         ");
            updateSql.AppendLine("       ,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine("  WHERE CompanyCD = @CompanyCD           ");
            updateSql.AppendLine("       AND PlanNo = @PlanNo              ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);
            //定义更新列表
            ArrayList lstUpdate = new ArrayList();
            //添加基本信息更新命令
            lstUpdate.Add(comm);
            //登陆或者更新招聘目标信息
            EditRectGoalInfo(lstUpdate, model.GoalList, model.PlanNo, model.CompanyCD, model.ModifiedUserID);
            //登陆或者更新信息发布
            EditRectPublishInfo(lstUpdate, model.PublishList, model.PlanNo, model.CompanyCD, model.ModifiedUserID);

            //执行更新操作并返回更新结果
            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);
        }
        #endregion

        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">人员信息</param>
        private static void SetSaveParameter(SqlCommand comm, RectPlanModel model)
        {
            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//企业代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", model.PlanNo));//招聘计划编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));//开始时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Principal", model.Principal));//负责人(对应员工表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", model .Status ));//状态(0计划中，1实行中，2已结束)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model .EndDate ));//状态(0计划中，1实行中，2已结束)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//更新用户ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanFee", model.PlanFee));//开始时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FeeNote", model.FeeNote));//负责人(对应员工表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@JoinMan", model.JoinMan));//状态(0计划中，1实行中，2已结束)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@JoinNote", model.JoinNote));//状态(0计划中，1实行中，2已结束)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RequireNum", model.RequireNum));//更新用户ID
        }
        #endregion

        #region 登陆或更新招聘活动的招聘目标信息
        /// <summary>
        /// 登陆或更新招聘活动的招聘目标信息
        /// </summary>
        /// <param name="lstCommand">数据库操作命令列表</param>
        /// <param name="lstGoal">招聘目标信息</param>
        /// <param name="planNo">招聘活动编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="modifiedUserID">最后更新者ID</param>
        /// <returns></returns>
        private static void EditRectGoalInfo(ArrayList lstCommand, ArrayList lstGoal, string planNo
                                                        , string companyCD, string modifiedUserID)
        {
            //未填写招聘目标时，返回true
            StringBuilder deleteSql = new StringBuilder();
            SqlCommand comm = new SqlCommand();

            if (lstGoal == null || lstGoal.Count < 1)
            {
                deleteSql.AppendLine(" DELETE FROM officedba.RectGoal ");
                deleteSql.AppendLine(" WHERE                          ");
                deleteSql.AppendLine("      PlanNo = @PlanNo          ");
                deleteSql.AppendLine("  AND CompanyCD = @CompanyCD    ");
                //定义Command
                //设置执行 Transact-SQL 语句
                comm.CommandText = deleteSql.ToString();
                //员工编号
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", planNo));
                //公司编码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
                //添加删除命令
                lstCommand.Add(comm);
                return;
            }

            //全删全插方式插入招聘目标信息

            /* 删除操作 */

            //删除SQL
            deleteSql.AppendLine(" DELETE FROM officedba.RectGoal ");
            deleteSql.AppendLine(" WHERE                          ");
            deleteSql.AppendLine("      PlanNo = @PlanNo          ");
            deleteSql.AppendLine("  AND CompanyCD = @CompanyCD    ");
            //定义Command
            //设置执行 Transact-SQL 语句
            comm.CommandText = deleteSql.ToString();
            //员工编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", planNo));
            //公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //添加删除命令
            lstCommand.Add(comm);

            /* 插入操作 */

            #region 插入SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO officedba.RectGoal ");
            insertSql.AppendLine("            (CompanyCD          ");
            insertSql.AppendLine("            ,PlanNo             ");
            insertSql.AppendLine("            ,ApplyDept          ");
            insertSql.AppendLine("            ,PositionTitle      ");
            insertSql.AppendLine("            ,PositionID      ");
            insertSql.AppendLine("            ,PersonCount        ");
            insertSql.AppendLine("            ,Sex                ");
            insertSql.AppendLine("            ,WorkAge                ");
            insertSql.AppendLine("            ,Age                ");
            insertSql.AppendLine("            ,CultureLevel       ");
            insertSql.AppendLine("            ,Professional       ");
            insertSql.AppendLine("            ,Requisition        ");
            insertSql.AppendLine("            ,CompleteDate       ");
            insertSql.AppendLine("            ,ModifiedDate       ");
            insertSql.AppendLine("            ,ModifiedUserID)    ");
            insertSql.AppendLine("      VALUES                    ");
            insertSql.AppendLine("            (@CompanyCD         ");
            insertSql.AppendLine("            ,@PlanNo            ");
            insertSql.AppendLine("            ,@ApplyDept         ");
            insertSql.AppendLine("            ,@PositionTitle     ");
            insertSql.AppendLine("            ,@PositionID      ");
            insertSql.AppendLine("            ,@PersonCount       ");
            insertSql.AppendLine("            ,@Sex               ");
            insertSql.AppendLine("            ,@WorkAge                ");
            insertSql.AppendLine("            ,@Age               ");
            insertSql.AppendLine("            ,@CultureLevel      ");
            insertSql.AppendLine("            ,@Professional      ");
            insertSql.AppendLine("            ,@Requisition       ");
            insertSql.AppendLine("            ,@CompleteDate      ");
            insertSql.AppendLine("            ,getdate()          ");
            insertSql.AppendLine("            ,@ModifiedUserID)   ");
            #endregion

            //遍历所有的履历信息
            for (int i = 0; i < lstGoal.Count; i++)
            {
                //获取单条目标记录
                RectGoalModel model = (RectGoalModel)lstGoal[i];
                //定义Command
                comm = new SqlCommand();
                //设置执行 Transact-SQL 语句
                comm.CommandText = insertSql.ToString();
                 
                #region 设置参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));//企业代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", planNo));//招聘计划编号 
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyDept", model.ApplyDept));//申请部门(对应部门表ID) 
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PositionTitle", model.PositionTitle));//岗位名称
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PositionID", model.PositionID));//岗位ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PersonCount", model.PersonCount));//人员数量
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Sex", model.Sex));//性别(1 男，2 女，3不限)
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Age", model.Age));//年龄要求
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CultureLevel", model.CultureLevel));//学历ID(对应分类代码表ID) 
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Professional", model.Professional));//专业ID(对应分类代码表ID) 
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Requisition", model.Requisition));//要求 
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompleteDate", model.CompleteDate));//计划完成时间
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", modifiedUserID));//更新用户ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@WorkAge", model .WorkAge ));//更新用户ID
                #endregion

                //添加插入命令
                lstCommand.Add(comm);

            }
        }
        #endregion

        #region 登陆或更新招聘信息发布
        /// <summary>
        /// 登陆或更新招聘信息发布
        /// </summary>
        /// <param name="lstCommand">数据库操作命令列表</param>
        /// <param name="lstPublish">招聘信息发布</param>
        /// <param name="planNo">招聘活动编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="modifiedUserID">最后更新者ID</param>
        /// <returns></returns>
        private static void EditRectPublishInfo(ArrayList lstCommand, ArrayList lstPublish, string planNo
                                                , string companyCD, string modifiedUserID)
        {
            //未填写招聘信息发布时，返回true
            StringBuilder deleteSql = new StringBuilder();
            SqlCommand comm = new SqlCommand();


            if (lstPublish == null || lstPublish.Count < 1)
            {
                deleteSql.AppendLine(" DELETE FROM officedba.RectPublish ");
                deleteSql.AppendLine(" WHERE                             "); 
                deleteSql.AppendLine(" CompanyCD = @CompanyCD  AND       ");
                deleteSql.AppendLine("      PlanNo = @PlanNo             ");
              
                //定义Command
                //设置执行 Transact-SQL 语句
                comm.CommandText = deleteSql.ToString();
                //员工编号
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", planNo));
                //公司编码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
                //添加删除命令
                lstCommand.Add(comm);

                return;
            }

            //全删全插方式插入招聘目标信息

            /* 删除操作 */

            //删除SQL
            deleteSql.AppendLine(" DELETE FROM officedba.RectPublish ");
            deleteSql.AppendLine(" WHERE                             ");       
            deleteSql.AppendLine(" CompanyCD = @CompanyCD  AND       ");
            deleteSql.AppendLine("      PlanNo = @PlanNo             ");
    
            //定义Command
            //设置执行 Transact-SQL 语句
            comm.CommandText = deleteSql.ToString();
            //员工编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", planNo));
            //公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //添加删除命令
            lstCommand.Add(comm);

            /* 插入操作 */

            #region 插入SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO officedba.RectPublish ");
            insertSql.AppendLine("            (CompanyCD             ");
            insertSql.AppendLine("            ,PlanNo                ");
            insertSql.AppendLine("            ,PublishPlace          ");
            insertSql.AppendLine("            ,PublishDate           ");
            insertSql.AppendLine("            ,Valid                 ");
            insertSql.AppendLine("            ,EndDate               ");
            insertSql.AppendLine("            ,Cost                  ");
            insertSql.AppendLine("            ,Effect                ");
            insertSql.AppendLine("            ,Status                ");
            insertSql.AppendLine("            ,ModifiedDate          ");
            insertSql.AppendLine("            ,ModifiedUserID)       ");
            insertSql.AppendLine("      VALUES                       ");
            insertSql.AppendLine("            (@CompanyCD            ");
            insertSql.AppendLine("            ,@PlanNo               ");
            insertSql.AppendLine("            ,@PublishPlace         ");
            insertSql.AppendLine("            ,@PublishDate          ");
            insertSql.AppendLine("            ,@Valid                ");
            insertSql.AppendLine("            ,@EndDate              ");
            insertSql.AppendLine("            ,@Cost                 ");
            insertSql.AppendLine("            ,@Effect               ");
            insertSql.AppendLine("            ,@Status               ");
            insertSql.AppendLine("            ,getdate()             ");
            insertSql.AppendLine("            ,@ModifiedUserID)      ");
            #endregion

            //遍历所有的履历信息
            for (int i = 0; i < lstPublish.Count; i++)
            {
                //获取单条目标记录
                RectPublishModel model = (RectPublishModel)lstPublish[i];
                //定义Command
                comm = new SqlCommand();
                //设置执行 Transact-SQL 语句
                comm.CommandText = insertSql.ToString();

                #region 设置参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));//企业代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", planNo));//招聘计划编号
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PublishPlace", model.PublishPlace));//发布渠道
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PublishDate ", model.PublishDate));//发布时间
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Valid", model.Valid));//有效时间(天数)
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));//截止时间
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Cost", model.Cost));//费用
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Effect", model.Effect));//效果
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", model.Status));//发布状态(0 暂停，1 发布中，2 结束)
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", modifiedUserID));//更新用户ID
                #endregion

                //添加插入命令
                lstCommand.Add(comm);

            }
        }
        #endregion

        #region 删除招聘活动信息
        /// <summary>
        /// 删除招聘活动信息
        /// </summary>
        /// <param name="planNo">招聘活动编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteRectPlanInfo(string planNo, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.RectPlan ");
            deleteSql.AppendLine(" WHERE ");    
            deleteSql.AppendLine(" CompanyCD = @CompanyCD  AND");
            deleteSql.AppendLine(" PlanNo In( " + planNo + ")");
       

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));

            //定义更新列表
            ArrayList lstDelete = new ArrayList();
            //添加基本信息更新命令
            lstDelete.Add(comm);
            //删除招聘目标信息
            DeleteGoalInfo(lstDelete, companyCD, planNo);
            //删除信息发布信息
            DeletePublishInfo(lstDelete, companyCD, planNo);
            //执行删除并返回
            return SqlHelper.ExecuteTransWithArrayList(lstDelete);
        }
        #endregion

        #region 删除招聘目标信息
        /// <summary>
        /// 删除招聘目标信息
        /// </summary>
        /// <param name="lstCommand">命令列表</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="planNo">招聘活动编号</param>
        /// <returns></returns>
        private static void DeleteGoalInfo(ArrayList lstCommand, string companyCD, string planNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.RectGoal ");
            deleteSql.AppendLine(" WHERE ");  
            deleteSql.AppendLine(" CompanyCD = @CompanyCD  AND  ");
            deleteSql.AppendLine(" PlanNo In( " + planNo + " ) ");
          

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
            //添加命令
            lstCommand.Add(comm);
        }
        #endregion

        #region 删除信息发布信息
        /// <summary>
        /// 删除信息发布信息
        /// </summary>
        /// <param name="lstCommand">命令列表</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="planNo">招聘活动编号</param>
        /// <returns></returns>
        private static void DeletePublishInfo(ArrayList lstCommand, string companyCD, string planNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.RectPublish ");
            deleteSql.AppendLine(" WHERE ");   
            deleteSql.AppendLine("CompanyCD = @CompanyCD  AND  ");
            deleteSql.AppendLine(" PlanNo in ( " + planNo + " ) ");
        

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
            //添加命令
            lstCommand.Add(comm);
        }
        #endregion

    }
}

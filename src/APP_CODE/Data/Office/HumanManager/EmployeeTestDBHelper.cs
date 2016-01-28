/**********************************************
 * 类作用：   新建考试记录
 * 建立人：   吴志强
 * 建立时间： 2009/04/08
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
    /// 类名：EmployeeTestDBHelper
    /// 描述：新建考试记录
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/08
    /// 最后修改时间：2009/04/08
    /// </summary>
    ///
    public class EmployeeTestDBHelper
    {
        #region 添加考试记录信息
        /// <summary>
        /// 添加考试记录信息
        /// </summary>
        /// <param name="model">考试信息</param>
        /// <returns></returns>
        public static bool InsertEmployeeTestInfo(EmployeeTestModel model)
        {

            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO officedba.EmployeeTest ");
            insertSql.AppendLine(" 	(CompanyCD                        ");
            insertSql.AppendLine(" 	,TestNo                           ");
            insertSql.AppendLine(" 	,Title                            ");
            insertSql.AppendLine(" 	,Teacher                          ");
            insertSql.AppendLine(" 	,StartDate                        ");
            insertSql.AppendLine(" 	,EndDate                          ");
            insertSql.AppendLine(" 	,Addr                             ");
            insertSql.AppendLine(" 	,TestContent                      ");
            insertSql.AppendLine(" 	,TestResult                       ");
            insertSql.AppendLine(" 	,Remark                           ");
            insertSql.AppendLine(" 	,Status                           ");
            insertSql.AppendLine(" 	,Attachment                       ");
            insertSql.AppendLine(" 	,AttachmentName                   ");
            insertSql.AppendLine(" 	,AbsenceCount                     ");
            insertSql.AppendLine(" 	,ModifiedDate                     ");
            insertSql.AppendLine(" 	,ModifiedUserID)                  ");
            insertSql.AppendLine(" VALUES                             ");
            insertSql.AppendLine(" 	(@CompanyCD                       ");
            insertSql.AppendLine(" 	,@TestNo                          ");
            insertSql.AppendLine(" 	,@Title                           ");
            insertSql.AppendLine(" 	,@Teacher                         ");
            insertSql.AppendLine(" 	,@StartDate                       ");
            insertSql.AppendLine(" 	,@EndDate                         ");
            insertSql.AppendLine(" 	,@Addr                            ");
            insertSql.AppendLine(" 	,@TestContent                     ");
            insertSql.AppendLine(" 	,@TestResult                      ");
            insertSql.AppendLine(" 	,@Remark                          ");
            insertSql.AppendLine(" 	,@Status                          ");
            insertSql.AppendLine(" 	,@Attachment                      ");
            insertSql.AppendLine(" 	,@AttachmentName                  ");
            insertSql.AppendLine(" 	,@AbsenceCount                    ");
            insertSql.AppendLine(" 	,getdate()                        ");
            insertSql.AppendLine(" 	,@ModifiedUserID)                 ");
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
            //考核结果
            EditResultInfo(lstInsert, model.ScoreList, model.TestNo, model.CompanyCD, model.ModifiedUserID);

            //执行更新操作并返回更新结果
            return SqlHelper.ExecuteTransWithArrayList(lstInsert);

        }
        #endregion

        #region 更新考试记录信息
        /// <summary>
        /// 更新考试记录信息
        /// </summary>
        /// <param name="model">保存信息</param>
        /// <returns></returns>
        public static bool UpdateEmployeeTestInfo(EmployeeTestModel model)
        {

            #region 更新SQL拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.EmployeeTest      ");
            updateSql.AppendLine(" SET                                ");
            updateSql.AppendLine(" 	Title = @Title                    ");
            updateSql.AppendLine(" 	,Teacher = @Teacher               ");
            updateSql.AppendLine(" 	,StartDate = @StartDate           ");
            updateSql.AppendLine(" 	,EndDate = @EndDate               ");
            updateSql.AppendLine(" 	,Addr = @Addr                     ");
            updateSql.AppendLine(" 	,TestContent = @TestContent       ");
            updateSql.AppendLine(" 	,TestResult = @TestResult         ");
            updateSql.AppendLine(" 	,Remark = @Remark                 ");
            updateSql.AppendLine(" 	,Status = @Status                 ");
            updateSql.AppendLine(" 	,Attachment = @Attachment         ");
            updateSql.AppendLine(" 	,AttachmentName = @AttachmentName ");
            updateSql.AppendLine(" 	,AbsenceCount = @AbsenceCount     ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            updateSql.AppendLine(" 	AND TestNo = @TestNo              ");
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
            //登陆或者更新进度安排信息
            EditResultInfo(lstUpdate, model.ScoreList, model.TestNo, model.CompanyCD, model.ModifiedUserID);

            //执行更新操作并返回更新结果
            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);
        }
        #endregion

        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">保存信息</param>
        private static void SetSaveParameter(SqlCommand comm, EmployeeTestModel model)
        {
            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TestNo", model.TestNo));//考试编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Teacher", model.Teacher));//考试负责人ID(对应员工表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));//开始时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));//结束时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Addr", model.Addr));//考试地点
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TestContent", model.TestContent));//考试内容摘要
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TestResult", model.TestResult));//考试结果
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", model.Status));//考试状态(0未开始，1已结束)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Attachment", model.PageAttachment));//附件
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AttachmentName", model.AttachmentName));//附件名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AbsenceCount", model.AbsenceCount));//缺考人数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//更新用户ID
        }
        #endregion

        #region 登陆或更新考试信息
        /// <summary>
        /// 登陆或更新考试信息  
        /// </summary>
        /// <param name="lstCommand">数据库操作命令列表</param>
        /// <param name="lstScore">考试得分信息</param>
        /// <param name="testNo">考试编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="modifiedUserID">最后更新者ID</param>
        /// <returns></returns>
        private static void EditResultInfo(ArrayList lstCommand, ArrayList lstScore, string testNo
                                                        , string companyCD, string modifiedUserID)
        {
            //全删全插方式插入考试结果信息
            #region 删除操作
            //删除SQL
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.EmployeeTestScore   ");
            deleteSql.AppendLine(" WHERE                                  ");
            deleteSql.AppendLine("      TestNo = @TestNo                  ");
            deleteSql.AppendLine("      AND CompanyCD = @CompanyCD        ");
            //定义Command
            SqlCommand comm = new SqlCommand();
            //设置执行 Transact-SQL 语句
            comm.CommandText = deleteSql.ToString();
            //考试编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TestNo", testNo));
            //公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            #endregion

            //添加删除命令
            lstCommand.Add(comm);

            /* 插入操作 */

            #region 插入SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO officedba.EmployeeTestScore ");
            insertSql.AppendLine(" 	(CompanyCD                             ");
            insertSql.AppendLine(" 	,TestNo                                ");
            insertSql.AppendLine(" 	,EmployeeID                            ");
            insertSql.AppendLine(" 	,TestScore                             ");
            insertSql.AppendLine(" 	,Flag                                  ");
            insertSql.AppendLine(" 	,ModifiedDate                          ");
            insertSql.AppendLine(" 	,ModifiedUserID)                       ");
            insertSql.AppendLine(" VALUES                                  ");
            insertSql.AppendLine(" 	(@CompanyCD                            ");
            insertSql.AppendLine(" 	,@TestNo                               ");
            insertSql.AppendLine(" 	,@EmployeeID                           ");
            insertSql.AppendLine(" 	,@TestScore                            ");
            insertSql.AppendLine(" 	,@Flag                                 ");
            insertSql.AppendLine(" 	,getdate()                             ");
            insertSql.AppendLine(" 	,@ModifiedUserID)                      ");
            #endregion

            //未填写参与人员时，返回
            if (lstScore != null)
            {
                //遍历所有的得分信息
                for (int i = 0; i < lstScore.Count; i++)
                {
                    //获取单条目标记录
                    EmployeeTestScoreModel model = (EmployeeTestScoreModel)lstScore[i];
                    //定义Command
                    comm = new SqlCommand();
                    //设置执行 Transact-SQL 语句
                    comm.CommandText = insertSql.ToString();

                    #region 设置参数
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));//公司代码
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TestNo", testNo));//考试编号
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));//员工ID
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TestScore", model.TestScore));//考试得分
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", model.Flag));//参考标识
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", modifiedUserID));//更新用户ID
                    #endregion

                    //添加插入命令
                    lstCommand.Add(comm);

                }
            }
        }
        #endregion

        #region 通过检索条件查询考试信息
        /// <summary>
        /// 查询考试信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchTestInfo(EmployeeTestSearchModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                                        ");
            searchSql.AppendLine(" 	 A.ID AS ID                                                  ");
            searchSql.AppendLine(" 	,A.TestNo AS TestNo                                          ");
            searchSql.AppendLine(" 	,A.Title AS Title                                            ");
            searchSql.AppendLine(" 	,ISNULL(B.EmployeeName, '') AS TeacherName                   ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.StartDate,21),'') AS StartDate ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.EndDate,21),'') AS EndDate     ");
            searchSql.AppendLine(" 	,A.Addr AS Addr                                              ");
            searchSql.AppendLine(" 	,CASE A.Status                                               ");
            searchSql.AppendLine(" 		WHEN '0' THEN '未开始'                                   ");
            searchSql.AppendLine(" 		WHEN '1' THEN '已结束'                                   ");
            searchSql.AppendLine(" 		ELSE ''                                                  ");
            searchSql.AppendLine(" 	END AS StatusName                                            ");
            searchSql.AppendLine(" 	,ISNULL(A.AbsenceCount, '') AS   AbsenceCount                ");
            searchSql.AppendLine(" FROM                                                          ");
            searchSql.AppendLine(" 	officedba.EmployeeTest A                                     ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B                           ");
            searchSql.AppendLine(" 		ON A.Teacher = B.ID                                      ");
            searchSql.AppendLine(" WHERE                                                         ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                                     ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //添加标识参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", "0"));

            //考试编号
            if (!string.IsNullOrEmpty(model.TestNo))
            {
                searchSql.AppendLine(" AND A.TestNo LIKE '%' + @TestNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TestNo", model.TestNo));
            }
            //主题
            if (!string.IsNullOrEmpty(model.Title))
            {
                searchSql.AppendLine(" AND A.Title LIKE '%' + @Title + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            //考试地点
            if (!string.IsNullOrEmpty(model.Addr))
            {
                searchSql.AppendLine(" AND A.Addr LIKE '%' + @Addr + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Addr", model.Addr));
            }
            //考试负责人
            if (!string.IsNullOrEmpty(model.TeacherID))
            {
                searchSql.AppendLine(" AND A.Teacher = @Teacher ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Teacher", model.TeacherID));
            }
            //开始时间
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                searchSql.AppendLine(" AND A.StartDate >= @StartDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));
            }
            if (!string.IsNullOrEmpty(model.StartToDate))
            {
                searchSql.AppendLine(" AND A.StartDate <= @StartToDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartToDate", model.StartToDate));
            }
            //结束时间
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                searchSql.AppendLine(" AND A.EndDate >= @EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));
            }
            if (!string.IsNullOrEmpty(model.EndToDate))
            {
                searchSql.AppendLine(" AND A.EndDate <= @EndToDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndToDate", model.EndToDate));
            }
            //考试状态
            if (!string.IsNullOrEmpty(model.StatusID))
            {
                searchSql.AppendLine(" AND A.Status = @Status ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", model.StatusID));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 通过ID查询考试信息
        /// <summary>
        /// 查询考试信息
        /// </summary>
        /// <param name="testID">考试ID</param>
        /// <returns></returns>
        public static DataSet GetTestInfoByID(string testID)
        {
            //定义返回的数据变量
            DataSet dsTestInfo = new DataSet();
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT  * from officedba.View_EmployeeTest");
            searchSql.AppendLine(" WHERE                                 ");
            searchSql.AppendLine(" 	ID = @TestID                       ");
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //考核ID
            param[0] = SqlHelper.GetParameter("@TestID", testID);
            //执行查询
            DataTable dtBaseInfo = SqlHelper.ExecuteSql(searchSql.ToString(), param);
            //添加考试基本信息
            dsTestInfo.Tables.Add(dtBaseInfo);
            //考试信息存在时，查询参与人员以及得分
            if (dtBaseInfo.Rows.Count > 0)
            {
                //获取公司代码
                string companyCD = dtBaseInfo.Rows[0]["CompanyCD"].ToString();
                //获取培训编号
                string testNo = dtBaseInfo.Rows[0]["TestNo"].ToString();
                //设置参与人员以及得分
                dsTestInfo.Tables.Add(GetScoreInfo(companyCD, testNo));
            }
            return dsTestInfo;
        }

        public static DataSet GetTestInfoByNO(string NO, string CompanyCD)
        {
            //定义返回的数据变量
            DataSet dsTestInfo = new DataSet();
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT  * from officedba.View_EmployeeTest");
            searchSql.AppendLine(" WHERE                                 ");
            searchSql.AppendLine(" 	TestNO = @TestNO and CompanyCD=@CompanyCD ");
            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //考核ID
            param[0] = SqlHelper.GetParameter("@TestNO", NO);
            param[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //执行查询
            DataTable dtBaseInfo = SqlHelper.ExecuteSql(searchSql.ToString(), param);
            //添加考试基本信息
            dsTestInfo.Tables.Add(dtBaseInfo);
            //考试信息存在时，查询参与人员以及得分
            if (dtBaseInfo.Rows.Count > 0)
            {
                //获取公司代码
                string companyCD = dtBaseInfo.Rows[0]["CompanyCD"].ToString();
                //获取培训编号
                string testNo = dtBaseInfo.Rows[0]["TestNo"].ToString();
                //设置参与人员以及得分
                dsTestInfo.Tables.Add(GetScoreInfo(companyCD, testNo));
            }
            return dsTestInfo;
        }

       #endregion

        #region 通过ID查询考试信息
        /// <summary>
        /// 查询考试信息
        /// </summary>
        /// <param name="testID">考试ID</param>
        /// <returns></returns>
        public static DataSet GetTestInfoWithID(string testID)
        {
            //定义返回的数据变量
            DataSet dsTestInfo = new DataSet();

            #region 查询考试信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                ");
            searchSql.AppendLine(" 	 A.CompanyCD                         ");
            searchSql.AppendLine(" 	,A.TestNo                            ");
            searchSql.AppendLine(" 	,A.Title                             ");
            searchSql.AppendLine(" 	,A.Teacher                           ");
            searchSql.AppendLine(" 	,B.EmployeeName                      ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.StartDate,21) ");
            searchSql.AppendLine(" 		 AS StartDate                    ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.EndDate,21)   ");
            searchSql.AppendLine(" 			AS EndDate                   ");
            searchSql.AppendLine(" 	,A.Addr                              ");
            searchSql.AppendLine(" 	,A.TestContent                       ");
            searchSql.AppendLine(" 	,A.TestResult                        ");
            searchSql.AppendLine(" 	,A.Remark                            ");
            searchSql.AppendLine(" 	,A.Status                            ");
            searchSql.AppendLine(" 	,A.Attachment                        ");
            searchSql.AppendLine(" 	,A.AbsenceCount                      ");
            searchSql.AppendLine(" 	,A.AttachmentName                    ");
            searchSql.AppendLine(" FROM                                  ");
            searchSql.AppendLine(" 	officedba.EmployeeTest A             ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B   ");
            searchSql.AppendLine(" 		ON A.Teacher = B.ID              ");
            searchSql.AppendLine(" WHERE                                 ");
            searchSql.AppendLine(" 	A.ID = @TestID                       ");
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //考核ID
            param[0] = SqlHelper.GetParameter("@TestID", testID);
            //执行查询
            DataTable dtBaseInfo = SqlHelper.ExecuteSql(searchSql.ToString(), param);
            //添加考试基本信息
            dsTestInfo.Tables.Add(dtBaseInfo);
            //考试信息存在时，查询参与人员以及得分
            if (dtBaseInfo.Rows.Count > 0)
            {
                //获取公司代码
                string companyCD = dtBaseInfo.Rows[0]["CompanyCD"].ToString();
                //获取培训编号
                string testNo = dtBaseInfo.Rows[0]["TestNo"].ToString();
                //设置参与人员以及得分
                dsTestInfo.Tables.Add(GetScoreInfo(companyCD, testNo));
            }
            #endregion

            return dsTestInfo;
        }

        #region 通过公司代码，考试编号获取考试结果
        /// <summary>
        /// 通过公司代码，考试编号获取考试结果
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="testNo">考试编号</param>
        /// <returns></returns>
        private static DataTable GetScoreInfo(string companyCD, string testNo)
        {
            //查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                              ");
            searchSql.AppendLine(" 	 A.EmployeeID AS EmployeeID        ");
            searchSql.AppendLine(" 	,A.TestScore AS TestScore          ");
            searchSql.AppendLine(" 	,B.EmployeeName AS EmployeeName    ");
            searchSql.AppendLine(" FROM                                ");
            searchSql.AppendLine(" 	officedba.EmployeeTestScore A      ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B ");
            searchSql.AppendLine(" 		ON A.EmployeeID = B.ID         ");
            searchSql.AppendLine(" WHERE                               ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD           ");
            searchSql.AppendLine("   AND A.TestNo = @TestNo            ");

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //考试编号
            param[1] = SqlHelper.GetParameter("@TestNo", testNo);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);

        }
        #endregion

        #endregion

        #region 删除考试记录信息

        #region 删除考试记录信息
        /// <summary>
        /// 删除考试记录信息
        /// </summary>
        /// <param name="testNo">考试编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteTestInfo(string testNo, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.EmployeeTest ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" TestNo In( " + testNo + ")");
            deleteSql.AppendLine(" AND CompanyCD = @CompanyCD ");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));

            //定义更新列表
            ArrayList lstDelete = new ArrayList();
            //添加基本信息更新命令
            lstDelete.Add(comm);
            //删除考试结果信息
            DeleteScoreInfo(lstDelete, companyCD, testNo);
            //执行删除并返回
            return SqlHelper.ExecuteTransWithArrayList(lstDelete);
        }
        #endregion

        #region 删除考试结果信息
        /// <summary>
        /// 删除考试结果信息
        /// </summary>
        /// <param name="lstCommand">命令列表</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="testNo">考试编号</param>
        /// <returns></returns>
        private static void DeleteScoreInfo(ArrayList lstCommand, string companyCD, string testNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.EmployeeTestScore ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" TestNo In( " + testNo + " ) ");
            deleteSql.AppendLine(" AND CompanyCD = @CompanyCD ");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
            //添加命令
            lstCommand.Add(comm);
        }
        #endregion

        #endregion
        public static DataTable EmployeeExaminationReportInfo(EmployeeTestSearchModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select C.DeptID AS LevelType,COUNT(C.DeptID) AS ID from officedba. EmployeeTest A                                                         ");
            searchSql.AppendLine(" 	left outer join officedba. EmployeeTestScore B                                            ");
            searchSql.AppendLine(" on B.CompanyCD=A.CompanyCD AND B.TestNo=A.TestNo                                        ");
            searchSql.AppendLine("LEFT OUTER JOIN officedba.EmployeeInfo c                                       ");
            searchSql.AppendLine(" ON C.CompanyCD=A.CompanyCD AND  B.EmployeeID=C.ID                 ");
            searchSql.AppendLine(" 	where  A.CompanyCD = @CompanyCD   and A.Status='1'");
      
            
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD)); 

           
            //部门ID
            if (!string.IsNullOrEmpty(model.Addr))
            {
                searchSql.AppendLine(" AND  C.DeptID=@Addr ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Addr", model.Addr));
            }
      
            //开始时间
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                searchSql.AppendLine(" AND A.StartDate >= @StartDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));
            }
            if (!string.IsNullOrEmpty(model.StartToDate))
            {
                searchSql.AppendLine(" AND A.StartDate <= @StartToDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartToDate", model.StartToDate));
            }
            //结束时间
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                searchSql.AppendLine(" AND A.EndDate >= @EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));
            }
            if (!string.IsNullOrEmpty(model.EndToDate))
            {
                searchSql.AppendLine(" AND A.EndDate <= @EndToDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndToDate", model.EndToDate));
            }
            //考试状态
            if (!string.IsNullOrEmpty(model.StatusName))
            {
                searchSql.AppendLine(" AND B.EmployeeID=@EmployeeID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.StatusName));
            }
            searchSql.AppendLine(" GROUP BY C.DeptID  ");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        public static string  getDept(string DeptID,string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select  isnull( DeptName,'') as  DeptName from officedba.DeptInfo   ");
            searchSql.AppendLine(" 	where CompanyCD=@CompanyCD and  ID=@DeptID                                             "); 
            #endregion
         
            SqlParameter [] p=new SqlParameter [2];
            //定义查询的命令
            p[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           p[1] = SqlHelper.GetParameter("@DeptID", DeptID);

            string i = Convert.ToString(SqlHelper.ExecuteScalar(searchSql.ToString(), p));
          return i;
           
        }


        public static DataTable GetInterviewNum(string DeptID, string monthStartDate, string monthEndDate, string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select D.DeptID,COUNT(D.DeptID) as InterviewNum    from officedba.RectInterview C   ");
            searchSql.AppendLine(" 	LEFT OUTER JOIN officedba.EmployeeInfo D                           ");
            searchSql.AppendLine("ON D.CompanyCD=C.CompanyCD AND  C.StaffName=D.ID                           ");
            searchSql.AppendLine("where C.CompanyCD=@CompanyCD                       "); 

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD  ));
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine("  and   D.DeptID=@DeptID                  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID ));
            }
            if (!string.IsNullOrEmpty(monthStartDate ))
            {
                searchSql.AppendLine("  and   C.InterviewDate>=@StartDate     and C.InterviewDate<=@EndDate              ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", monthStartDate ));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", monthEndDate ));
            }

            searchSql.AppendLine(" GROUP BY D.DeptID");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        public static DataTable GetRequireNum(string  DeptID ,string  monthStartDate,string  monthEndDate ,string  CompanyCD )
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select B.applyDept,sum(PersonCount) as  PersonCount from officedba.RectPlan A   ");
            searchSql.AppendLine(" 	left outer join officedba.RectGoal B                                     ");
            searchSql.AppendLine("ON A.CompanyCD=B.CompanyCD AND A.PlanNo=B.PlanNo                              ");
            searchSql.AppendLine("where B.CompanyCD=@CompanyCD                       "); 

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD  ));
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine(" AND   B.applyDept=@DeptID                  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID ));
            }
            if (!string.IsNullOrEmpty(monthStartDate ))
            {
                searchSql.AppendLine("  AND  A.StartDate>=@StartDate     and A.StartDate<=@EndDate              ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", monthStartDate ));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", monthEndDate ));
            }

            searchSql.AppendLine(" GROUP BY B.applyDept ");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        public static DataTable GetReportedNum(string DeptID, string monthStartDate, string monthEndDate, string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select D.DeptID,COUNT(D.DeptID) as ReportedNum   from officedba.RectInterview C   ");
            searchSql.AppendLine(" 	LEFT OUTER JOIN officedba.EmployeeInfo D                           ");
            searchSql.AppendLine("ON D.CompanyCD=C.CompanyCD AND  C.StaffName=D.ID                           ");
            searchSql.AppendLine("where C.CompanyCD=@CompanyCD and d.Flag='1'                      ");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine(" AND   D.DeptID=@DeptID                  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            if (!string.IsNullOrEmpty(monthStartDate))
            {
                searchSql.AppendLine(" AND   C.InterviewDate>=@StartDate     and C.InterviewDate<=@EndDate              ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", monthStartDate));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", monthEndDate));
            }

            searchSql.AppendLine(" GROUP BY D.DeptID");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        public static DataTable GetLatedNum(string DeptID, string monthStartDate, string monthEndDate, string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT B.DeptID,COUNT(*) AS DelayManCount FROM (SELECT Date,CompanyCD,EmployeeID FROM officedba.DailyAttendance  ");
            searchSql.AppendLine(" 	WHERE IsDelay='1' AND Date>=@StartDate AND Date<=@EndDate                      ");
            searchSql.AppendLine("GROUP BY Date,CompanyCD,EmployeeID) A  LEFT OUTER JOIN officedba.EmployeeInfo B ON A.CompanyCD=B.CompanyCD  AND  A.EmployeeID=B.ID                  ");
            searchSql.AppendLine("where A.CompanyCD=@CompanyCD                   ");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", monthStartDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", monthEndDate));
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine(" AND   B.DeptID=@DeptID                  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            searchSql.AppendLine(" GROUP BY  B.DeptID");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        public static DataTable GetLeaveEarlyNum(string DeptID, string monthStartDate, string monthEndDate, string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT B.DeptID,COUNT(*) AS DelayManCount FROM (SELECT Date,CompanyCD,EmployeeID FROM officedba.DailyAttendance  ");
            searchSql.AppendLine(" 	WHERE CompanyCD=@CompanyCD and IsForwarOff='1' AND Date>=@StartDate AND Date<=@EndDate                      ");
            searchSql.AppendLine("GROUP BY Date,CompanyCD,EmployeeID) A  LEFT OUTER JOIN officedba.EmployeeInfo B ON A.CompanyCD=B.CompanyCD  AND  A.EmployeeID=B.ID                  ");
            searchSql.AppendLine("where A.CompanyCD=@CompanyCD                   ");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", monthStartDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", monthEndDate));
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine("  AND  B.DeptID=@DeptID                  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            searchSql.AppendLine(" GROUP BY  B.DeptID");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        public static DataTable GetAbsenteeismNum(string DeptID, string monthStartDate, string monthEndDate, string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT A.CompanyCD, A.DeptID,COUNT(*) AS Absentee FROM (SELECT X.ID,X.EmployeeName,X.CompanyCD,X.EmployeeNo,X.DeptID,X.EveryDay,Y.Date,Y.StartTime FROM  (SELECT A.*,B.EveryDay   FROM  (select distinct b.ID,b.EmployeeName,b.DeptID,b.CompanyCD,b.EmployeeNo,b.QuarterID from officedba.EmployeeAttendanceSet a  LEFT OUTER JOIN officedba.EmployeeInfo b  ON a.CompanyCD=b.CompanyCD and a.EmployeeID=b.ID WHERE a.CompanyCD =@CompanyCD and  b.Flag='1') A   CROSS JOIN officedba.AttendanceEveryDay B  WHERE  A.CompanyCD=@CompanyCD  AND    B.EveryDay>=@StartDate AND B.EveryDay<=@EndDate) X LEFT OUTER JOIN  officedba.DailyAttendance Y  ON X.CompanyCD=Y.CompanyCD AND  X.ID=Y.EmployeeID AND X.EveryDay=Y.Date) A   WHERE   A.CompanyCD=@CompanyCD  and StartTime is null"); 

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", monthStartDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", monthEndDate));
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine(" AND   A.DeptID=@DeptID                  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            searchSql.AppendLine(" GROUP BY A.CompanyCD,A.DeptID");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        public static DataTable GetLeaveNum(string DeptID, string monthStartDate, string monthEndDate, string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT A.DeptID,A.CompanyCD, COUNT(*) AS leaveCount  FROM(select a.*,b.DeptID from officedba.AttendanceApply  a left outer join officedba.EmployeeInfo  b ON  a.CompanyCD=b.CompanyCD AND a.EmployeeID=b.ID where a.Flag='1')A  where  A.ApplyDate>=@StartDate and A.ApplyDate<=@EndDate  "); 

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", monthStartDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", monthEndDate));
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine(" AND   A.DeptID=@DeptID                  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            searchSql.AppendLine("GROUP BY A.CompanyCD, A.DeptID");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }


        public static DataTable GetDeptInfo(string CompanyCD, string DeptID)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select DeptName,ID from officedba.DeptInfo where  CompanyCD=@CompanyCD  "); 

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine(" and ID=@DeptID  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID ));
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        public static DataTable GetDeptInNum(string DeptID, string monthStartDate, string monthEndDate, string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select NewDeptID,count(NewDeptID) as countQian from officedba.EmplApplyNotify  where CompanyCD=@CompanyCD and BillStatus='2'  and IntDate>=@StartDate and  IntDate<=@EndDate");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", monthStartDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", monthEndDate));
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine(" AND    NewDeptID=@DeptID                  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            searchSql.AppendLine("group by NewDeptID ");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        public static DataTable GetDeptOutNum(string DeptID, string monthStartDate, string monthEndDate, string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select NowDeptID,count(NowDeptID) as countQian from officedba.EmplApplyNotify  where CompanyCD=@CompanyCD and BillStatus='2'  and OutDate>=@StartDate and  OutDate<=@EndDate");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", monthStartDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", monthEndDate));
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine(" AND    NowDeptID=@DeptID                  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            searchSql.AppendLine("group by NowDeptID ");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        public static DataTable GetSeparationNum(string DeptID, string monthStartDate, string monthEndDate, string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select b.DeptID,count(b.DeptID) as separateNum from officedba.MoveNotify a left outer join officedba.EmployeeInfo b on b.CompanyCD=a.CompanyCD and a.EmployeeID=b.ID where a.CompanyCD=@CompanyCD and  a.BillStatus='2' and OutDate>=@StartDate and  OutDate<=@EndDate");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", monthStartDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", monthEndDate));
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine(" AND   b.DeptID =@DeptID                  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            searchSql.AppendLine("group by b.DeptID  ");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
    }
}

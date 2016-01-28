/**********************************************
 * 类作用：   新建培训考核
 * 建立人：   吴志强
 * 建立时间： 2009/04/05
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
    /// 类名：TrainingAsseDBHelper
    /// 描述：新建培训考核
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/05
    /// 最后修改时间：2009/04/05
    /// </summary>
    ///
    public class TrainingAsseDBHelper
    {
        #region 添加培训考核信息
        /// <summary>
        /// 添加培训考核信息
        /// </summary>
        /// <param name="model">培训考核信息</param>
        /// <returns></returns>
        public static bool InsertTrainingAsseInfo(TrainingAsseModel model)
        {

            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO officedba.TrainingAsse ");
            insertSql.AppendLine("            (CompanyCD              ");
            insertSql.AppendLine("            ,AsseNo                 ");
            insertSql.AppendLine("            ,TrainingNo             ");
            insertSql.AppendLine("            ,CheckPerson            ");
            insertSql.AppendLine("            ,TrainingPlan           ");
            insertSql.AppendLine("            ,FillUser               ");
            insertSql.AppendLine("            ,LeadViews              ");
            insertSql.AppendLine("            ,Description            ");
            insertSql.AppendLine("            ,CheckWay               ");
            insertSql.AppendLine("            ,CheckDate              ");
            insertSql.AppendLine("            ,GeneralComment         ");
            insertSql.AppendLine("            ,CheckRemark            ");
            insertSql.AppendLine("            ,ModifiedDate           ");
            insertSql.AppendLine("            ,ModifiedUserID)        ");
            insertSql.AppendLine(" VALUES                             ");
            insertSql.AppendLine("            (@CompanyCD             ");
            insertSql.AppendLine("            ,@AsseNo                ");
            insertSql.AppendLine("            ,@TrainingNo            ");
            insertSql.AppendLine("            ,@CheckPerson           ");
            insertSql.AppendLine("            ,@TrainingPlan          ");
            insertSql.AppendLine("            ,@FillUser              ");
            insertSql.AppendLine("            ,@LeadViews             ");
            insertSql.AppendLine("            ,@Description           ");
            insertSql.AppendLine("            ,@CheckWay              ");
            insertSql.AppendLine("            ,@CheckDate             ");
            insertSql.AppendLine("            ,@GeneralComment        ");
            insertSql.AppendLine("            ,@CheckRemark           ");
            insertSql.AppendLine("            ,getdate()              ");
            insertSql.AppendLine("            ,@ModifiedUserID)       ");
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
            EditResultInfo(lstInsert, model.ResultList, model.AsseNo, model.CompanyCD, model.ModifiedUserID);

            //执行更新操作并返回更新结果
            return SqlHelper.ExecuteTransWithArrayList(lstInsert);

        }
        #endregion

        #region 更新考核信息
        /// <summary>
        /// 更新考核信息
        /// </summary>
        /// <param name="model">考核信息</param>
        /// <returns></returns>
        public static bool UpdateTrainingAsseInfo(TrainingAsseModel model)
        {

            #region 更新SQL拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.TrainingAsse      ");
            updateSql.AppendLine(" SET                                ");
            updateSql.AppendLine(" 	 TrainingNo = @TrainingNo         ");
            updateSql.AppendLine(" 	,CheckPerson = @CheckPerson       ");
            updateSql.AppendLine(" 	,TrainingPlan = @TrainingPlan     ");
            updateSql.AppendLine(" 	,LeadViews = @LeadViews           ");
            updateSql.AppendLine(" 	,Description = @Description       ");
            updateSql.AppendLine(" 	,CheckWay = @CheckWay             ");
            updateSql.AppendLine(" 	,CheckDate = @CheckDate           ");
            updateSql.AppendLine(" 	,GeneralComment = @GeneralComment ");
            updateSql.AppendLine(" 	,CheckRemark = @CheckRemark       ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine("  WHERE                             ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            updateSql.AppendLine(" 	AND AsseNo = @AsseNo              ");
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
            EditResultInfo(lstUpdate, model.ResultList, model.AsseNo, model.CompanyCD, model.ModifiedUserID);

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
        private static void SetSaveParameter(SqlCommand comm, TrainingAsseModel model)
        {
            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AsseNo", model.AsseNo));//考核编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingNo", model.TrainingNo));//培训编号（对应培训表中的培训编号）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckPerson", model.CheckPerson));//考核人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingPlan", model.TrainingPlan));//培训规划
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@LeadViews", model.LeadViews));//领导意见
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Description", model.Description));//说明
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckWay", model.CheckWay));//考核方式
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckDate", model.CheckDate));//考核时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@GeneralComment", model.GeneralComment));//考核总评
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckRemark", model.CheckRemark));//考核备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//更新用户ID
            //插入时，添加填写人参数
            if (ConstUtil.EDIT_FLAG_INSERT == model.EditFlag)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FillUser", model.FillUser));//填写人
            }
        }
        #endregion

        #region 登陆或更新考核结果信息
        /// <summary>
        /// 登陆或更新考核结果信息  
        /// </summary>
        /// <param name="lstCommand">数据库操作命令列表</param>
        /// <param name="lstResult">考核结果信息</param>
        /// <param name="asseNo">考核编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="modifiedUserID">最后更新者ID</param>
        /// <returns></returns>
        private static void EditResultInfo(ArrayList lstCommand, ArrayList lstResult, string asseNo
                                                        , string companyCD, string modifiedUserID)
        {
            //未填写参与人员时，返回
            if (lstResult == null || lstResult.Count < 1)
            {
                return;
            }

            //全删全插方式插入参与人员信息

            #region 删除操作
            //删除SQL
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.TrainingDetail   ");
            deleteSql.AppendLine(" WHERE                                  ");
            deleteSql.AppendLine("      AsseNo = @AsseNo                  ");
            deleteSql.AppendLine("      AND CompanyCD = @CompanyCD        ");
            //定义Command
            SqlCommand comm = new SqlCommand();
            //设置执行 Transact-SQL 语句
            comm.CommandText = deleteSql.ToString();
            //考核编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AsseNo", asseNo));
            //公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            #endregion

            //添加删除命令
            lstCommand.Add(comm);

            /* 插入操作 */

            #region 插入SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO officedba.TrainingDetail ");
            insertSql.AppendLine("            (CompanyCD                ");
            insertSql.AppendLine("            ,AsseNo                   ");
            insertSql.AppendLine("            ,EmployeeID               ");
            insertSql.AppendLine("            ,AssessLevel              ");
            insertSql.AppendLine("            ,AssessScore              ");
            insertSql.AppendLine("            ,ModifiedDate             ");
            insertSql.AppendLine("            ,ModifiedUserID)          ");
            insertSql.AppendLine(" VALUES                               ");
            insertSql.AppendLine("            (@CompanyCD               ");
            insertSql.AppendLine("            ,@AsseNo                  ");
            insertSql.AppendLine("            ,@EmployeeID              ");
            insertSql.AppendLine("            ,@AssessLevel             ");
            insertSql.AppendLine("            ,@AssessScore             ");
            insertSql.AppendLine("            ,getdate()                ");
            insertSql.AppendLine("            ,@ModifiedUserID)         ");
            #endregion

            //遍历所有的履历信息
            for (int i = 0; i < lstResult.Count; i++)
            {
                //获取单条目标记录
                TrainingDetailModel model = (TrainingDetailModel)lstResult[i];
                //定义Command
                comm = new SqlCommand();
                //设置执行 Transact-SQL 语句
                comm.CommandText = insertSql.ToString();

                #region 设置参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));//公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AsseNo", asseNo));//考核编号
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));//员工ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AssessLevel", model.AssessLevel));//考核等级
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AssessScore", model.AssessScore));//考核得分
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", modifiedUserID));//更新用户ID
                #endregion

                //添加插入命令
                lstCommand.Add(comm);

            }
        }
        #endregion

        #region 通过检索条件查询培训考核信息
        /// <summary>
        /// 查询培训考核信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchTrainingAsseInfo(TrainingAsseSearchModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                                       ");
            searchSql.AppendLine(" 	 A.ID AS ID                                                 ");
            searchSql.AppendLine(" 	,A.AsseNo AS AsseNo                                         ");
            searchSql.AppendLine(" 	,A.TrainingNo AS TrainingNo                                 ");
            searchSql.AppendLine(" 	,ISNULL(B.TrainingName, '') AS TrainingName                 ");
            searchSql.AppendLine(" 	,ISNULL(B.TrainingTeacher,'') AS TrainingTeacher            ");
            searchSql.AppendLine(" 	,ISNULL(C.TypeName,'') AS TrainingWayName                   ");
            searchSql.AppendLine(" 	,ISNULL(A.CheckPerson,'') AS CheckPerson                    ");
            searchSql.AppendLine(" 	,ISNULL(A.CheckWay,'') AS AsseWay                           ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.CheckDate,21),'') AS AsseDate ");
            searchSql.AppendLine(" FROM                                                         ");
            searchSql.AppendLine(" 	officedba.TrainingAsse A                                    ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeTraining B ON                   ");
            searchSql.AppendLine(" 		A.CompanyCD = B.CompanyCD                               ");
            searchSql.AppendLine(" 		AND A.TrainingNo = B.TrainingNo                         ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType C ON                     ");
            searchSql.AppendLine(" 		B.TrainingWay = C.ID                                    ");
            searchSql.AppendLine(" WHERE                                                        ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                                    ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //考核编号
            if (!string.IsNullOrEmpty(model.AsseNo))
            {
                searchSql.AppendLine(" AND A.AsseNo LIKE '%' + @AsseNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AsseNo", model.AsseNo));
            }
            //培训编号
            if (!string.IsNullOrEmpty(model.TrainingNo))
            {
                searchSql.AppendLine(" AND A.TrainingNo LIKE '%' + @TrainingNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingNo", model.TrainingNo));
            }
            //培训名称
            if (!string.IsNullOrEmpty(model.TrainingName))
            {
                searchSql.AppendLine(" AND B.TrainingName LIKE '%' + @TrainingName + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingName", model.TrainingName));
            }
            //培训方式
            if (!string.IsNullOrEmpty(model.TrainingWayID))
            {
                searchSql.AppendLine(" AND B.TrainingWay = @TrainingWayID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingWayID", model.TrainingWayID));
            }
            //考评人
            if (!string.IsNullOrEmpty(model.CheckPerson))
            {
                searchSql.AppendLine(" AND A.CheckPerson LIKE '%' + @CheckPerson + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckPerson", model.CheckPerson));
            }
            //考核时间
            if (!string.IsNullOrEmpty(model.AsseDate))
            {
                searchSql.AppendLine(" AND A.CheckDate >= @CheckDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckDate", model.AsseDate));
            }
            if (!string.IsNullOrEmpty(model.AsseEndDate))
            {
                searchSql.AppendLine(" AND A.CheckDate <= @AsseEndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AsseEndDate", model.AsseEndDate));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 通过ID查询培训考核信息
        /// <summary>
        /// 查询培训考核信息
        /// </summary>
        /// <param name="asseID">考核ID</param>
        /// <returns></returns>
        public static DataSet GetTrainingAsseInfoWithID(string asseID)
        {
            //定义返回的数据变量
            DataSet dsTrainingAsseInfo = new DataSet();

            #region 查询招聘活动信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                           ");
            searchSql.AppendLine(" 	 A.CompanyCD                    ");
            searchSql.AppendLine(" 	,A.AsseNo                       ");
            searchSql.AppendLine(" 	,A.TrainingNo                   ");
            searchSql.AppendLine(" 	,A.CheckPerson                  ");
            searchSql.AppendLine(" 	,A.TrainingPlan                 ");
            searchSql.AppendLine(" 	,A.FillUser                     ");
            searchSql.AppendLine(" 	,B.EmployeeName AS FillUserName ");
            searchSql.AppendLine(" 	,A.LeadViews                    ");
            searchSql.AppendLine(" 	,A.Description                  ");
            searchSql.AppendLine(" 	,A.CheckWay                     ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.CheckDate,21) AS CheckDate ");
            searchSql.AppendLine(" 	,A.GeneralComment               ");
            searchSql.AppendLine(" 	,A.CheckRemark                  ");
            searchSql.AppendLine(" FROM                             ");
            searchSql.AppendLine(" 	officedba.TrainingAsse A        ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B  ");
            searchSql.AppendLine(" 		ON A.FillUser = B.ID      ");
            searchSql.AppendLine(" WHERE                            ");
            searchSql.AppendLine(" 	A.ID = @AsseID                    ");
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //考核ID
            param[0] = SqlHelper.GetParameter("@AsseID", asseID);
            //执行查询
            DataTable dtBaseInfo = SqlHelper.ExecuteSql(searchSql.ToString(), param);
            //添加培训基本信息
            dsTrainingAsseInfo.Tables.Add(dtBaseInfo);
            //培训信息存在时，查询参与人员以及进度安排
            if (dtBaseInfo.Rows.Count > 0)
            {
                //获取公司代码
                string companyCD = dtBaseInfo.Rows[0]["CompanyCD"].ToString();
                //获取培训编号
                string asseNo = dtBaseInfo.Rows[0]["AsseNo"].ToString();
                //设置参与人员
                dsTrainingAsseInfo.Tables.Add(GetResultInfoWithID(companyCD, asseNo));
            }
            #endregion

            return dsTrainingAsseInfo;
        }

        #region 通过公司代码，培训考核编号获取考核结果
        /// <summary>
        /// 通过公司代码，培训考核编号获取考核结果
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="asseNo">培训考核编号</param>
        /// <returns></returns>
        private static DataTable GetResultInfoWithID(string companyCD, string asseNo)
        {
            //查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                              ");
            searchSql.AppendLine(" 	 A.EmployeeID AS EmployeeID        ");
            searchSql.AppendLine(" 	,B.EmployeeName AS EmployeeName    ");
            searchSql.AppendLine(" 	,A.AssessLevel AS AssessLevel      ");
            searchSql.AppendLine(" 	,A.AssessScore AS AssessScore      ");
            searchSql.AppendLine(" FROM officedba.TrainingDetail A     ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B ");
            searchSql.AppendLine(" 		ON A.EmployeeID = B.ID         ");
            searchSql.AppendLine(" WHERE                               ");
            searchSql.AppendLine("     A.CompanyCD = @CompanyCD        ");
            searchSql.AppendLine("     AND A.AsseNo = @AsseNo          ");

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //考核编号
            param[1] = SqlHelper.GetParameter("@AsseNo", asseNo);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);

        }
        #endregion

        #endregion

        #region 删除培训考核信息

        #region 删除培训考核信息
        /// <summary>
        /// 删除培训信息
        /// </summary>
        /// <param name="asseNo">培训考核编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteTrainingAsseInfo(string asseNo, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.TrainingAsse ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" AsseNo In( " + asseNo + ")");
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
            //删除考核结果信息
            DeleteResultInfo(lstDelete, companyCD, asseNo);
            //执行删除并返回
            return SqlHelper.ExecuteTransWithArrayList(lstDelete);
        }
        #endregion

        #region 删除考核结果信息
        /// <summary>
        /// 删除考核结果信息
        /// </summary>
        /// <param name="lstCommand">命令列表</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="asseNo">培训考核编号</param>
        /// <returns></returns>
        private static void DeleteResultInfo(ArrayList lstCommand, string companyCD, string asseNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.TrainingDetail ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" AsseNo In( " + asseNo + " ) ");
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

        #region 培训考核表打印
        /// <summary>
        /// 培训考核表打印
        /// </summary>
        /// <param name="companyCD"></param>
        /// <param name="AsseNo"></param>
        /// <returns></returns>
        public static DataTable PrintTrainingAsse(string companyCD, string AsseNo)
        {
            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                           ");
            searchSql.AppendLine(" 	 A.CompanyCD                    ");
            searchSql.AppendLine(" 	,A.AsseNo                       ");
            searchSql.AppendLine(" 	,A.TrainingNo                   ");
            searchSql.AppendLine(" 	,et.TrainingName                ");
            searchSql.AppendLine(" 	,A.CheckPerson                  ");
            searchSql.AppendLine(" 	,A.TrainingPlan                 ");
            searchSql.AppendLine(" 	,A.FillUser                     ");
            searchSql.AppendLine(" 	,B.EmployeeName AS FillUserName ");
            searchSql.AppendLine(" 	,A.LeadViews                    ");
            searchSql.AppendLine(" 	,A.Description                  ");
            searchSql.AppendLine(" 	,A.CheckWay                     ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.CheckDate,21) AS CheckDate ");
            searchSql.AppendLine(" 	,A.GeneralComment               ");
            searchSql.AppendLine(" 	,A.CheckRemark                  ");
            searchSql.AppendLine(" FROM                             ");
            searchSql.AppendLine(" 	officedba.TrainingAsse A        ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B  ");
            searchSql.AppendLine(" 		ON A.FillUser = B.ID      ");
            searchSql.AppendLine(" left join officedba.EmployeeTraining et on et.TrainingNo = A.TrainingNo ");
            searchSql.AppendLine(" WHERE                            ");
            searchSql.AppendLine(" A.AsseNo = @AsseNo and A.CompanyCD = @CompanyCD ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameterFromString("@CompanyCD", companyCD);
            //培训编号
            param[1] = SqlHelper.GetParameterFromString("@AsseNo", AsseNo);
            //执行查询
            DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), param);

            return data;
        }
        #endregion

        #region 打印考核结果
        /// <summary>
        /// 打印考核结果
        /// </summary>
        /// <param name="companyCD"></param>
        /// <param name="AsseNo"></param>
        /// <returns></returns>
        public static DataTable PrintTrainingDetail(string companyCD, string AsseNo)
        {
            #region 查询SQL拼写
            //查询SQL拼写
            string searchSql = "select td.EmployeeID,ei.employeeName," +
                                       " (case td.AssessLevel when '1' then '不及格' when '2' then '及格' when '3' then '良好' when '4' then '优秀' end) AssessLevel," +
                                       " td.AssessScore" +
                               " from officedba. TrainingDetail td" +
                               " left join officedba.EmployeeInfo ei on ei.id = td.EmployeeID" +
                               " where td.AsseNo = '" + AsseNo + "' and td.CompanyCD = '" + companyCD + "'";
            #endregion
                       
            //执行查询
            return SqlHelper.ExecuteSql(searchSql);
        }
        #endregion
    }
}

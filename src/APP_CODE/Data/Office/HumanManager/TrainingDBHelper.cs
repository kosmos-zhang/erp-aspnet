/**********************************************
 * 类作用：   新建培训
 * 建立人：   吴志强
 * 建立时间： 2009/04/02
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
    /// 类名：TrainingDBHelper
    /// 描述：新建培训
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/02
    /// 最后修改时间：2009/04/02
    /// </summary>
    ///
    public class TrainingDBHelper
    {

        #region 查询培训的参与人员信息
        /// <summary>
        /// 查询培训的参与人员信息
        /// </summary>
        /// <param name="traningNo">培训编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetJionUserInfo(string traningNo, string companyCD)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                 ");
            searchSql.AppendLine(" 	 A.JoinID AS UserID                   ");
            searchSql.AppendLine(" 	,ISNULL(B.EmployeeName,'') AS UserName");
            searchSql.AppendLine(" FROM                                   ");
            searchSql.AppendLine(" 	officedba.TrainingUser A              ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B ON ");
            searchSql.AppendLine(" 		A.JoinID = B.ID                   ");
            searchSql.AppendLine(" WHERE                                  ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD              ");
            searchSql.AppendLine("   AND A.TrainingNo = @TrainingNo       ");
            searchSql.AppendLine("   AND A.Flag = @FlagE                  ");
            searchSql.AppendLine(" UNION                                  ");
            searchSql.AppendLine(" SELECT DISTINCT                        ");
            searchSql.AppendLine(" 	 D.ID AS UserID                       ");
            searchSql.AppendLine(" 	,ISNULL(D.EmployeeName,'') AS UserName");
            searchSql.AppendLine(" FROM                                   ");
            searchSql.AppendLine(" 	officedba.TrainingUser C,             ");
            searchSql.AppendLine(" 	officedba.EmployeeInfo D              ");
            searchSql.AppendLine(" WHERE                                  ");
            searchSql.AppendLine(" 	C.JoinID = D.DeptID                   ");
            searchSql.AppendLine(" 	AND D.Flag <> @LeaveFlag              ");
            searchSql.AppendLine(" 	AND C.CompanyCD = @CompanyCD          ");
            searchSql.AppendLine("   AND C.TrainingNo = @TrainingNo       ");
            searchSql.AppendLine("   AND C.Flag = @FlagD                  ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[5];
            int i = 0;
            //公司代码
            param[i++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //培训编号
            param[i++] = SqlHelper.GetParameter("@TrainingNo", traningNo);
            //部门标识
            param[i++] = SqlHelper.GetParameter("@FlagD", ConstUtil.DEPT_EMPLOY_FLAG_DEPT);
            //员工标识 
            param[i++] = SqlHelper.GetParameter("@FlagE", ConstUtil.DEPT_EMPLOY_FLAG_EMPLOY);
            //离职标识
            param[i++] = SqlHelper.GetParameter("@LeaveFlag", ConstUtil.JOB_FLAG_LEAVE);

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 查询仍在培训中的培训信息
        /// <summary>
        /// 查询仍在培训中的培训信息
        /// </summary>
        /// <returns></returns>
        public static DataTable SearchOnTrainingInfo(string companyCD)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT ID                   ");
            searchSql.AppendLine(" 	,TrainingNo                ");
            searchSql.AppendLine(" 	,TrainingName              ");
            searchSql.AppendLine(" FROM                        ");
            searchSql.AppendLine(" 	officedba.EmployeeTraining ");
            searchSql.AppendLine(" WHERE                       ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD     ");
            searchSql.AppendLine(" 	AND Status = @Status       ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //状态
            param[1] = SqlHelper.GetParameter("@Status", "0");
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 通过ID查询培训信息
        /// <summary>
        /// 查询培训信息
        /// </summary>
        /// <param name="trainingID">培训ID</param>
        /// <returns></returns>
        public static DataSet GetTrainingInfoWithID(string trainingID)
        {
            //定义返回的数据变量
            DataSet dsTrainingInfo = new DataSet();

            #region 查询招聘活动信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                             ");
            searchSql.AppendLine(" 	 A.CompanyCD AS CompanyCD                         ");
            searchSql.AppendLine(" 	,A.TrainingNo AS TrainingNo                       ");
            searchSql.AppendLine(" 	,A.TrainingName AS TrainingName                   ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.ApplyDate,21) AS ApplyDate ");
            searchSql.AppendLine(" 	,A.EmployeeID AS EmployeeID                       ");
            searchSql.AppendLine(" 	,B.EmployeeName AS EmployeeName                   ");
            searchSql.AppendLine(" 	,A.ProjectNo AS ProjectNo                         ");
            searchSql.AppendLine(" 	,A.ProjectName AS ProjectName                     ");
            searchSql.AppendLine(" 	,A.TrainingOrgan AS TrainingOrgan                 ");
            searchSql.AppendLine(" 	,A.PlanCost AS PlanCost                           ");
            searchSql.AppendLine(" 	,A.TrainingCount AS TrainingCount                 ");
            searchSql.AppendLine(" 	,A.Goal  AS Goal                                  ");
            searchSql.AppendLine(" 	,A.TrainingPlace  AS TrainingPlace                ");
            searchSql.AppendLine(" 	,A.TrainingWay  AS TrainingWay                    ");
            searchSql.AppendLine(" 	,A.TrainingTeacher  AS TrainingTeacher            ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.StartDate,21) AS StartDate ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.EndDate,21) AS EndDate     ");
            searchSql.AppendLine(" 	,A.TrainingRemark AS TrainingRemark               ");
            searchSql.AppendLine(" 	,A.CheckPerson   AS CheckPerson                   ");
            searchSql.AppendLine(" 	,A.Attachment    AS Attachment                    ");
            searchSql.AppendLine(" 	,A.AttachmentName    AS AttachmentName            ");
            searchSql.AppendLine(" FROM                                               ");
            searchSql.AppendLine(" 	officedba.EmployeeTraining A                      ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B ON             ");
            searchSql.AppendLine(" 		A.EmployeeID = B.ID                           ");
            searchSql.AppendLine(" WHERE                                              ");
            searchSql.AppendLine(" 	A.ID = @TrainingID                                ");
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //培训ID
            param[0] = SqlHelper.GetParameter("@TrainingID", trainingID);
            //执行查询
            DataTable dtBaseInfo = SqlHelper.ExecuteSql(searchSql.ToString(), param);
            //添加培训基本信息
            dsTrainingInfo.Tables.Add(dtBaseInfo);
            //培训信息存在时，查询参与人员以及进度安排
            if (dtBaseInfo.Rows.Count > 0)
            {
                //获取公司代码
                string companyCD = dtBaseInfo.Rows[0]["CompanyCD"].ToString();
                //获取培训编号
                string trainingNo = dtBaseInfo.Rows[0]["TrainingNo"].ToString();
                //设置参与人员
                dsTrainingInfo.Tables.Add(GetJoinUserInfo(companyCD, trainingNo));
                //设置进度安排
                dsTrainingInfo.Tables.Add(GetScheduleInfoWithID(companyCD, trainingNo));
            }
            #endregion

            return dsTrainingInfo;
        }

        #region 通过公司代码，培训编号获取参与人员
        /// <summary>
        /// 通过公司代码，培训编号获取参与人员
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="trainingNo">培训编号</param>
        /// <returns></returns>
        private static DataTable GetJoinUserInfo(string companyCD, string trainingNo)
        {
            //查询语句
            StringBuilder searchSql = new StringBuilder();
            //searchSql.AppendLine(" SELECT                                 ");
            //searchSql.AppendLine(" 	A.Flag AS Flag                        ");
            //searchSql.AppendLine(" 	,A.JoinID AS JoinID                   ");
            //searchSql.AppendLine(" 	,B.EmployeeName AS EmployeeName       ");
            //searchSql.AppendLine(" 	,C.DeptName AS DeptName               ");
            //searchSql.AppendLine(" FROM                                   ");
            //searchSql.AppendLine(" 	officedba.TrainingUser A              ");
            //searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B ON ");
            //searchSql.AppendLine(" 	 A.JoinID = B.ID AND A.Flag = @FlagE  ");
            //searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo C ON     ");
            //searchSql.AppendLine(" 	 A.JoinID = C.ID AND A.Flag = @FlagD  ");
            //searchSql.AppendLine(" WHERE                                  ");
            //searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD              ");
            //searchSql.AppendLine(" 	AND A.TrainingNo = @TrainingNo        ");

            searchSql.AppendLine(" SELECT                                 ");
            searchSql.AppendLine(" 	A.Flag AS Flag                        ");
            searchSql.AppendLine(" 	,A.JoinID AS JoinID                   ");
            searchSql.AppendLine(" 	,B.EmployeeName AS EmployeeName       ");
            //searchSql.AppendLine(" 	,'' AS DeptName               ");
            searchSql.AppendLine(" FROM                                   ");
            searchSql.AppendLine(" 	officedba.TrainingUser A              ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B ON ");
            searchSql.AppendLine(" 	 A.JoinID = B.ID  ");            
            searchSql.AppendLine(" WHERE                                  ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD              ");
            searchSql.AppendLine(" 	AND A.TrainingNo = @TrainingNo        ");

            //设置参数
            SqlParameter[] param = new SqlParameter[4];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //培训编号
            param[1] = SqlHelper.GetParameter("@TrainingNo", trainingNo);
            //人员标识
            param[2] = SqlHelper.GetParameter("@FlagE", ConstUtil.DEPT_EMPLOY_FLAG_EMPLOY);
            //部门标识
            param[3] = SqlHelper.GetParameter("@FlagD", ConstUtil.DEPT_EMPLOY_FLAG_DEPT);
            //执行查询并返回查询记录集
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 通过公司代码，培训编号获取进度安排
        /// <summary>
        /// 通过公司代码，培训编号获取进度安排
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="trainingNo">培训编号</param>
        /// <returns></returns>
        private static DataTable GetScheduleInfoWithID(string companyCD, string trainingNo)
        {
            //查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT ID                      ");
            searchSql.AppendLine("       ,ScheduleDate            ");
            searchSql.AppendLine("       ,Abstract                ");
            searchSql.AppendLine("       ,Remark                  ");
            searchSql.AppendLine(" FROM                           ");
            searchSql.AppendLine(" 	officedba.TrainingSchedule    ");
            searchSql.AppendLine(" WHERE                          ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD        ");
            searchSql.AppendLine(" 	AND TrainingNo = @TrainingNo  ");

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //招聘活动ID
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //招聘活动ID
            param[1] = SqlHelper.GetParameter("@TrainingNo", trainingNo);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
             
        }
        #endregion

        #endregion

        #region 通过检索条件查询培训信息
        /// <summary>
        /// 通过检索条件查询培训信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchTrainingInfo(TrainingSearchModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                                        ");
            searchSql.AppendLine(" 	A.ID AS ID                                                   ");
            searchSql.AppendLine(" 	,A.TrainingNo AS TrainingNo                                  ");
            searchSql.AppendLine(" 	,A.TrainingName AS TrainingName                              ");
            searchSql.AppendLine(" 	,ISNULL(A.TrainingPlace, '') AS TrainingPlace                ");
            searchSql.AppendLine(" 	,ISNULL(B.TypeName, '') AS TrainingWayName                   ");
            searchSql.AppendLine(" 	,ISNULL(A.TrainingTeacher,'') AS TrainingTeacher             ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.StartDate,21),'') AS StartDate ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.EndDate,21),'') AS EndDate     ");
            searchSql.AppendLine(" FROM                                                          ");
            searchSql.AppendLine(" 	officedba.EmployeeTraining A                                 ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType B                         ");
            searchSql.AppendLine(" 	ON A.TrainingWay = B.ID                                      ");
            searchSql.AppendLine(" WHERE                                                         ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                                     ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //培训编号
            if (!string.IsNullOrEmpty(model.TrainingNo))
            {
                searchSql.AppendLine(" AND A.TrainingNo LIKE  '%' + @TrainingNo + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingNo", model.TrainingNo));
            }
            //培训名称
            if (!string.IsNullOrEmpty(model.TrainingName))
            {
                searchSql.AppendLine(" AND A.TrainingName LIKE  '%' + @TrainingName + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingName", model.TrainingName));
            }
            //培训方式
            if (!string.IsNullOrEmpty(model.TrainingWayID))
            {
                searchSql.AppendLine(" AND A.TrainingWay = @TrainingWayID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingWayID", model.TrainingWayID));
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

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 添加培训以及相关信息
        /// <summary>
        /// 添加培训以及相关信息
        /// </summary>
        /// <param name="model">培训信息</param>
        /// <returns></returns>
        public static bool InsertTrainingInfo(TrainingModel model)
        {

            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO officedba.EmployeeTraining ");
            insertSql.AppendLine("            (CompanyCD                  ");
            insertSql.AppendLine("            ,TrainingNo                 ");
            insertSql.AppendLine("            ,TrainingName               ");
            insertSql.AppendLine("            ,ApplyDate                  ");
            insertSql.AppendLine("            ,EmployeeID                 ");
            insertSql.AppendLine("            ,ProjectNo                  ");
            insertSql.AppendLine("            ,ProjectName                ");
            insertSql.AppendLine("            ,TrainingOrgan              ");
            insertSql.AppendLine("            ,PlanCost                   ");
            insertSql.AppendLine("            ,TrainingCount              ");
            insertSql.AppendLine("            ,Goal                       ");
            insertSql.AppendLine("            ,TrainingPlace              ");
            insertSql.AppendLine("            ,TrainingWay                ");
            insertSql.AppendLine("            ,TrainingTeacher            ");
            insertSql.AppendLine("            ,StartDate                  ");
            insertSql.AppendLine("            ,EndDate                    ");
            insertSql.AppendLine("            ,TrainingRemark             ");
            insertSql.AppendLine("            ,CheckPerson                ");
            insertSql.AppendLine("            ,Attachment                 ");
            insertSql.AppendLine("            ,AttachmentName             ");
            insertSql.AppendLine("            ,Status                     ");
            insertSql.AppendLine("            ,ModifiedDate               ");
            insertSql.AppendLine("            ,ModifiedUserID)            ");
            insertSql.AppendLine("      VALUES                            ");
            insertSql.AppendLine("            (@CompanyCD                 ");
            insertSql.AppendLine("            ,@TrainingNo                ");
            insertSql.AppendLine("            ,@TrainingName              ");
            insertSql.AppendLine("            ,@ApplyDate                 ");
            insertSql.AppendLine("            ,@EmployeeID                ");
            insertSql.AppendLine("            ,@ProjectNo                 ");
            insertSql.AppendLine("            ,@ProjectName               ");
            insertSql.AppendLine("            ,@TrainingOrgan             ");
            insertSql.AppendLine("            ,@PlanCost                  ");
            insertSql.AppendLine("            ,@TrainingCount             ");
            insertSql.AppendLine("            ,@Goal                      ");
            insertSql.AppendLine("            ,@TrainingPlace             ");
            insertSql.AppendLine("            ,@TrainingWay               ");
            insertSql.AppendLine("            ,@TrainingTeacher           ");
            insertSql.AppendLine("            ,@StartDate                 ");
            insertSql.AppendLine("            ,@EndDate                   ");
            insertSql.AppendLine("            ,@TrainingRemark            ");
            insertSql.AppendLine("            ,@CheckPerson               ");
            insertSql.AppendLine("            ,@Attachment                ");
            insertSql.AppendLine("            ,@AttachmentName            ");
            insertSql.AppendLine("            ,'0'                        ");
            insertSql.AppendLine("            ,getdate()                  ");
            insertSql.AppendLine("            ,@ModifiedUserID)           ");
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
            //
            EditJionInfo(lstInsert, model.UserList, model.TrainingNo, model.CompanyCD, model.ModifiedUserID);
            //登陆或者更新进度安排信息
            EditScheduleInfo(lstInsert, model.ScheduleList, model.TrainingNo, model.CompanyCD, model.ModifiedUserID);

            //执行更新操作并返回更新结果
            return SqlHelper.ExecuteTransWithArrayList(lstInsert);

        }
        #endregion

        #region 更新培训以及相关信息
        /// <summary>
        /// 更新培训以及相关信息
        /// </summary>
        /// <param name="model">培训信息</param>
        /// <returns></returns>
        public static bool UpdateTrainingInfo(TrainingModel model)
        {

            #region 更新SQL拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.EmployeeTraining    ");
            updateSql.AppendLine(" SET TrainingName = @TrainingName     ");
            updateSql.AppendLine(" 	,ApplyDate = @ApplyDate             ");
            updateSql.AppendLine(" 	,EmployeeID = @EmployeeID           ");
            updateSql.AppendLine(" 	,ProjectNo = @ProjectNo             ");
            updateSql.AppendLine(" 	,ProjectName = @ProjectName         ");
            updateSql.AppendLine(" 	,TrainingOrgan = @TrainingOrgan     ");
            updateSql.AppendLine(" 	,PlanCost = @PlanCost               ");
            updateSql.AppendLine(" 	,TrainingCount = @TrainingCount     ");
            updateSql.AppendLine(" 	,Goal = @Goal                       ");
            updateSql.AppendLine(" 	,TrainingPlace = @TrainingPlace     ");
            updateSql.AppendLine(" 	,TrainingWay = @TrainingWay         ");
            updateSql.AppendLine(" 	,TrainingTeacher = @TrainingTeacher ");
            updateSql.AppendLine(" 	,StartDate = @StartDate             ");
            updateSql.AppendLine(" 	,EndDate = @EndDate                 ");
            updateSql.AppendLine(" 	,TrainingRemark = @TrainingRemark   ");
            updateSql.AppendLine(" 	,CheckPerson = @CheckPerson         ");
            updateSql.AppendLine(" 	,Attachment = @Attachment           ");
            updateSql.AppendLine(" 	,AttachmentName = @AttachmentName   ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()           ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID   ");
            updateSql.AppendLine("  WHERE                               ");
            updateSql.AppendLine(" 	   CompanyCD = @CompanyCD           ");
            updateSql.AppendLine("     AND TrainingNo = @TrainingNo     ");
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
            //zzzzzzzzzzzzzzz
            EditJionInfo(lstUpdate, model.UserList, model.TrainingNo, model.CompanyCD, model.ModifiedUserID);
            //登陆或者更新进度安排信息
            EditScheduleInfo(lstUpdate, model.ScheduleList, model.TrainingNo, model.CompanyCD, model.ModifiedUserID);

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
        private static void SetSaveParameter(SqlCommand comm, TrainingModel model)
        {
            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingNo", model.TrainingNo));//培训编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingName", model.TrainingName));//培训名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyDate", model.ApplyDate));//发起时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));//发起人ID(对应员工表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectNo", model.ProjectNo));//项目编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectName", model.ProjectName));//项目名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingOrgan", model.TrainingOrgan));//培训机构
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanCost", model.PlanCost));//费用预算
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingCount", model.TrainingCount));//培训天数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Goal", model.Goal));//目的
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingPlace", model.TrainingPlace));//培训地点
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingWay", model.TrainingWay));//培训方式ID（对应分类代码表ID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingTeacher", model.TrainingTeacher));//培训老师
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));//开始时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));//结束时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingRemark", model.TrainingRemark));//培训备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckPerson", model.CheckPerson));//考核人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Attachment", model.PageAttachment));//附件
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AttachmentName", model.AttachmentName));//附件
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//更新用户ID
        }
        #endregion

        #region 登陆或更新培训参与人员信息
        /// <summary>
        /// 登陆或更新培训参与人员信息  
        /// </summary>
        /// <param name="lstCommand">数据库操作命令列表</param>
        /// <param name="lstUser">参与人员信息</param>
        /// <param name="trainingNo">培训编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="modifiedUserID">最后更新者ID</param>
        /// <returns></returns>
        private static void EditJionInfo(ArrayList lstCommand, ArrayList lstUser, string trainingNo
                                                        , string companyCD, string modifiedUserID)
        {
            //未填写参与人员时，返回
            if (lstUser == null || lstUser.Count < 1)
            {
                return;
            }

            //全删全插方式插入参与人员信息

            #region 删除操作
            //删除SQL
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.TrainingUser     ");
            deleteSql.AppendLine(" WHERE                                  ");
            deleteSql.AppendLine("      TrainingNo = @TrainingNo          ");
            deleteSql.AppendLine("      AND CompanyCD = @CompanyCD        ");
            //定义Command
            SqlCommand comm = new SqlCommand();
            //设置执行 Transact-SQL 语句
            comm.CommandText = deleteSql.ToString();
            //培训编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingNo", trainingNo));
            //公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            #endregion

            //添加删除命令
            lstCommand.Add(comm);

            /* 插入操作 */

            #region 插入SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO officedba.TrainingUser ");
            insertSql.AppendLine("            (CompanyCD              ");
            insertSql.AppendLine("            ,TrainingNo             ");
            insertSql.AppendLine("            ,Flag                   ");
            insertSql.AppendLine("            ,JoinID                 ");
            insertSql.AppendLine("            ,ModifiedDate           ");
            insertSql.AppendLine("            ,ModifiedUserID)        ");
            insertSql.AppendLine("      VALUES                        ");
            insertSql.AppendLine("            (@CompanyCD             ");
            insertSql.AppendLine("            ,@TrainingNo            ");
            insertSql.AppendLine("            ,@Flag                  ");
            insertSql.AppendLine("            ,@JoinID                ");
            insertSql.AppendLine("            ,getdate()              ");
            insertSql.AppendLine("            ,@ModifiedUserID)       ");
            #endregion

            //遍历所有的履历信息
            for (int i = 0; i < lstUser.Count; i++)
            {
                //获取单条目标记录
                TrainingUserModel model = (TrainingUserModel)lstUser[i];
                //定义Command
                comm = new SqlCommand();
                //设置执行 Transact-SQL 语句
                comm.CommandText = insertSql.ToString();

                #region 设置参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));//公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingNo", trainingNo));//培训编号（对应培训表中的培训编号）
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", "1"));//区分(1 员工，2部门)//zyy
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@JoinID", model.JoinID));//参与人ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", modifiedUserID));//更新用户ID
                #endregion

                //添加插入命令
                lstCommand.Add(comm);

            }
        }
        #endregion

        #region 登陆或更新进度安排目标信息
        /// <summary>
        /// 登陆或更新进度安排目标信息
        /// </summary>
        /// <param name="lstCommand">数据库操作命令列表</param>
        /// <param name="lstSchedule">进度安排信息</param>
        /// <param name="trainingNo">培训编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="modifiedUserID">最后更新者ID</param>
        /// <returns></returns>
        private static void EditScheduleInfo(ArrayList lstCommand, ArrayList lstSchedule, string trainingNo
                                                        , string companyCD, string modifiedUserID)
        {
            //未填写进度安排时，返回true
            //if (lstSchedule == null || lstSchedule.Count < 1)
            //{
            //    return;
            //}

            //全删全插方式插入进度安排信息

            #region 删除操作
            //删除SQL
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.TrainingSchedule ");
            deleteSql.AppendLine(" WHERE                                  ");
            deleteSql.AppendLine("      TrainingNo = @TrainingNo          ");
            deleteSql.AppendLine("      AND CompanyCD = @CompanyCD        ");
            //定义Command
            SqlCommand comm = new SqlCommand();
            //设置执行 Transact-SQL 语句
            comm.CommandText = deleteSql.ToString();
            //培训编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingNo", trainingNo));
            //公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            #endregion

            //添加删除命令
            lstCommand.Add(comm);

            /* 插入操作 */

            #region 插入SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO officedba.TrainingSchedule ");
            insertSql.AppendLine("            (CompanyCD                  ");
            insertSql.AppendLine("            ,TrainingNo                 ");
            insertSql.AppendLine("            ,ScheduleDate               ");
            insertSql.AppendLine("            ,Abstract                   ");
            insertSql.AppendLine("            ,Remark                     ");
            insertSql.AppendLine("            ,ModifiedDate               ");
            insertSql.AppendLine("            ,ModifiedUserID)            ");
            insertSql.AppendLine("      VALUES                            ");
            insertSql.AppendLine("            (@CompanyCD                 ");
            insertSql.AppendLine("            ,@TrainingNo                ");
            insertSql.AppendLine("            ,@ScheduleDate              ");
            insertSql.AppendLine("            ,@Abstract                  ");
            insertSql.AppendLine("            ,@Remark                    ");
            insertSql.AppendLine("            ,getdate()                  ");
            insertSql.AppendLine("            ,@ModifiedUserID)           ");
            #endregion

            //遍历所有的履历信息
            for (int i = 0; i < lstSchedule.Count; i++)
            {
                //获取单条目标记录
                TrainingScheduleModel model = (TrainingScheduleModel)lstSchedule[i];
                //定义Command
                comm = new SqlCommand();
                //设置执行 Transact-SQL 语句
                comm.CommandText = insertSql.ToString();

                #region 设置参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));//公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrainingNo", trainingNo));//培训编号（对应培训表中的培训编号）
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ScheduleDate", model.ScheduleDate));//时间
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Abstract", model.Abstract));//内容摘要
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//备注
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", modifiedUserID));//更新用户ID
                #endregion

                //添加插入命令
                lstCommand.Add(comm);

            }
        }
        #endregion

        #region 删除培训信息

        #region 删除培训信息
        /// <summary>
        /// 删除培训信息
        /// </summary>
        /// <param name="trainingNo">培训编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteTrainingInfo(string trainingNo, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.EmployeeTraining ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" TrainingNo In( " + trainingNo + ")");
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
            //删除参与人员信息
            DeleteJoinUserInfo(lstDelete, companyCD, trainingNo);
            //删除技能信息
            DeleteScheduleInfo(lstDelete, companyCD, trainingNo);
            //删除培训考核
            DeleteTrainingAsseInfo(lstDelete, companyCD, trainingNo);
            //删除培训成绩
            DeleteTrainingDetailInfo(lstDelete, companyCD, trainingNo);

            //执行删除并返回
            return SqlHelper.ExecuteTransWithArrayList(lstDelete);
        }
        #endregion

        #region 根据培训编号查看是否有培训考核信息
        /// <summary>
        /// 根据培训编号查看是否有培训考核信息
        /// </summary>
        /// <param name="trainingNo"></param>
        /// <returns></returns>
        public static bool GetAsseByTraNo(string trainingNo)
        {
            try
            {
                string sql = "select id from officedba.TrainingAsse where TrainingNo in (" + trainingNo + ")";

                bool IsHave = SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;

                if (IsHave)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch 
            {
                return false;
            }
        }
        #endregion

        #region 删除参与人员信息
        /// <summary>
        /// 删除参与人员信息
        /// </summary>
        /// <param name="lstCommand">命令列表</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="trainingNo">培训编号</param>
        /// <returns></returns>
        private static void DeleteJoinUserInfo(ArrayList lstCommand, string companyCD, string trainingNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.TrainingUser ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" TrainingNo In( " + trainingNo + " ) ");
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

        #region 删除进度安排信息
        /// <summary>
        /// 删除进度安排信息
        /// </summary>
        /// <param name="lstCommand">命令列表</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="trainingNo">培训编号</param>
        /// <returns></returns>
        private static void DeleteScheduleInfo(ArrayList lstCommand, string companyCD, string trainingNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.TrainingSchedule ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" TrainingNo in ( " + trainingNo + " ) ");
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

        #region 删除培训考核
        /// <summary>
        /// 删除培训考核
        /// </summary>
        /// <param name="lstCommand">命令列表</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="trainingNo">培训编号</param>
        /// <returns></returns>
        private static void DeleteTrainingAsseInfo(ArrayList lstCommand, string companyCD, string trainingNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.TrainingAsse ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" TrainingNo in ( " + trainingNo + " ) ");
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

        #region 删除培训成绩
        /// <summary>
        /// 删除培训成绩
        /// </summary>
        /// <param name="lstCommand">命令列表</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="trainingNo">培训编号</param>
        /// <returns></returns>
        private static void DeleteTrainingDetailInfo(ArrayList lstCommand, string companyCD, string trainingNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.TrainingDetail ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" AsseNo in ( " + trainingNo + " ) ");
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

        #region 培训数量分析
        /// <summary>
        /// 培训数量分析
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="JoinID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetTrainingCount(string CompanyCD, string DeptID,string JoinID,string BeginDate,string EndDate,string BDate,string EDate)
        {
            try
            {
                string sql = "  select di.DeptName,A.num,A.DeptID" +
                               " from " +
                                   " (select ei.DeptID,Count(ei.DeptID) num from officedba.TrainingUser tu" +
                                   " left join officedba.EmployeeInfo ei on ei.id = tu.JoinID" +
                                   " left join officedba.EmployeeTraining et on et.TrainingNo = tu.TrainingNo " +
                                   " where tu.CompanyCD = '" + CompanyCD + "' and ei.DeptID <> 0 ";
                if (DeptID != "")
                {
                    sql += " and ei.DeptID = '" + DeptID + "'";
                }
                if (JoinID != "")
                {
                    sql += " and tu.JoinID = '" + JoinID + "'";
                }
                if (BeginDate != "")
                {
                    sql += " and et.StartDate >= '" + BeginDate + "'";
                }
                if (EndDate != "")
                {
                    sql += " and et.StartDate <= '" + EndDate + "'";
                }
                if (BDate != "")
                {
                    sql += " and et.EndDate >= '" + BDate + "'";
                }
                if (EDate != "")
                {
                    sql += " and et.EndDate <= '" + EDate + "'";
                }
                sql += " and ei.DeptID is not null " +
                       " group by ei.DeptID ) A" +
                   " left join officedba.DeptInfo di on di.id = A.DeptID ";
                
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            string strSql = "SELECT et.ID, et.CompanyCD, et.TrainingNo, et.TrainingName, et.ApplyDate, et.EmployeeID, et.ProjectNo, et.ProjectName, et.PlanCost, et.TrainingCount,  ";
            strSql += "et.TrainingOrgan, et.Goal, et.TrainingPlace, et.TrainingWay, et.TrainingTeacher, et.StartDate, et.EndDate, et.TrainingRemark, et.CheckPerson,          ";
            strSql += " et.ModifiedDate, et.ModifiedUserID, CASE et.status WHEN '0' THEN '培训中' WHEN '1' THEN '培训开始' END AS statusName,                   ";
            strSql += "e.EmployeeName as Attachment, cpt.TypeName AS Status                                                                                                                 ";
            strSql += "FROM officedba.EmployeeTraining AS et left JOIN                                                                                                       ";
            strSql += "officedba.EmployeeInfo AS e ON et.EmployeeID = e.ID left JOIN                                                                                         ";
            strSql += "officedba.CodePublicType AS cpt ON et.TrainingWay = cpt.ID                                                                                             ";
            strSql += "where et.TrainingNo=@TrainingNo and et.CompanyCD=@CompanyCD ";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@TrainingNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }

        /// <summary>
        /// 获取培训人员 
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepTrainingUser(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            string strSql = "SELECT isnull(e.EmployeeName,'') as EmployeeName FROM officedba.TrainingUser AS t left JOIN  officedba.EmployeeInfo AS e ON t.JoinID = e.ID AND t.Flag = '1' WHERE (t.TrainingNo = @TrainingNo) AND (t.CompanyCD = @CompanyCD)";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@TrainingNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderDetail(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            string strSql = "select  * from  officedba.TrainingSchedule WHERE (TrainingNo = @TrainingNo) AND (CompanyCD = @CompanyCD) order by ScheduleDate asc";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@TrainingNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }

    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using XBase.Data.DBHelper;

using XBase.Model.Personal.Agenda;

namespace XBase.Data.Personal.Agenda
{
    public class AgendaInfoDBHelper
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool InsertAgendaInfo(PersonalDateArrange model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.PersonalDateArrange(");
            strSql.Append("CompanyCD,ArrangeTItle,Critical,Content,StartDate,StartTime,EndTime,Creator,IsPublic,CreateDate,IsMobileNotice,AheadTimes,ModifiedDate,ModifiedUserID,Important)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@ArrangeTItle,@Critical,@Content,@StartDate,@StartTime,@EndTime,@Creator,@IsPublic,@CreateDate,@IsMobileNotice,@AheadTimes,@ModifiedDate,@ModifiedUserID,@Important)");
            strSql.Append("; select  @SourceID = @@IDENTITY  ");

            if (model.IsMobileNotice == "1")
            {
                strSql.Append("insert into officedba.NoticeHistory(");
                strSql.Append("   CompanyCD,SourceFlag,SourceID,PlanNoticeDate  )  ");
                strSql.Append("      values(@CompanyCD, @SourceFlag,@SourceID,@PlanNoticeDate  ) ");
            }
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@ArrangeNo", SqlDbType.VarChar,50),
					new SqlParameter("@ArrangeTItle", SqlDbType.VarChar,100),
					new SqlParameter("@Critical", SqlDbType.Char,1),
					new SqlParameter("@Content", SqlDbType.VarChar,200),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@StartTime", SqlDbType.VarChar,4),
					new SqlParameter("@EndTime", SqlDbType.VarChar,4),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@IsPublic", SqlDbType.Char,1),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@IsMobileNotice", SqlDbType.Char,1),
					new SqlParameter("@AheadTimes", SqlDbType.Int,4),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10),
                    new SqlParameter("@SourceFlag", SqlDbType.Char,1),
                    new SqlParameter("@PlanNoticeDate", SqlDbType.DateTime)  ,
                    new SqlParameter("@SourceID", SqlDbType.Int,4),
                    new SqlParameter("@Important", SqlDbType.Char,1) };
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = "未定义";
            parameters[2].Value = model.ArrangeTItle;
            parameters[3].Value = model.Critical;
            parameters[4].Value = model.Content;
            parameters[5].Value = model.StartDate;
            parameters[6].Value = model.StartTime;
            parameters[7].Value = model.EndTime;
            parameters[8].Value = model.Creator;
            parameters[9].Value = model.IsPublic;
            parameters[10].Value = DateTime.Now;
            parameters[11].Value = model.IsMobileNotice;
            parameters[12].Value = model.AheadTimes;
            parameters[13].Value = DateTime.Now;
            parameters[14].Value = model.ModifiedUserID;
            parameters[15].Value = '1';
            if (model.IsMobileNotice == "1")
            {
                try
                {
                    double hour = Convert.ToDouble(model.StartTime.Substring(0, 2));
                    double minite = Convert.ToDouble(model.StartTime.Substring(2, 2));
                    DateTime datetime = (DateTime)model.StartDate;
                    datetime = datetime.AddHours(hour);
                    datetime = datetime.AddMinutes(minite);
                    double aheadtime = Convert.ToDouble(model.AheadTimes) * -1;
                    datetime = datetime.AddHours(aheadtime);
                    parameters[16].Value = datetime;
                }
                catch
                {
                    parameters[16].Value = DateTime.Now;
                }


            }
            else
            {
                parameters[16].Value = DateTime.Now;
            }
            parameters[17].Value = 0;
            parameters[18].Value = model.Important;


            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();
            //设置保存的参数
            foreach (SqlParameter sqp in parameters)
                comm.Parameters.Add(sqp);
            //返回操作结果  
            return SqlHelper.ExecuteTransWithCommand(comm);
        }

        public static bool UpdateAgendaInfo(PersonalDateArrange model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.PersonalDateArrange  set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("ArrangeTItle=@ArrangeTItle,");
            strSql.Append("Critical=@Critical,");
            strSql.Append("Content=@Content,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("StartTime=@StartTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("IsPublic=@IsPublic,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("IsMobileNotice=@IsMobileNotice,");
            strSql.Append("AheadTimes=@AheadTimes,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID,");
            strSql.Append("Important=@Important");
            strSql.Append(" where ID=@ID ");

            if (model.IsMobileNotice == "1")
            {
                strSql.Append("delete  from  officedba.NoticeHistory  where  SourceID=@ID and @SourceFlag ='1'  ");

                strSql.Append("insert into officedba.NoticeHistory(");
                strSql.Append("   CompanyCD,SourceFlag,SourceID,PlanNoticeDate  )  ");
                strSql.Append("      values(@CompanyCD, @SourceFlag,@ID,@PlanNoticeDate  ) ");
            }
            else
            {
                strSql.Append("delete  from  officedba.NoticeHistory  where  SourceID=@ID and @SourceFlag ='1'  ");
            }
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@ArrangeNo", SqlDbType.VarChar,50),
					new SqlParameter("@ArrangeTItle", SqlDbType.VarChar,100),
					new SqlParameter("@Critical", SqlDbType.Char,1),
					new SqlParameter("@Content", SqlDbType.VarChar,200),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@StartTime", SqlDbType.VarChar,4),
					new SqlParameter("@EndTime", SqlDbType.VarChar,4),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@IsPublic", SqlDbType.Char,1),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@IsMobileNotice", SqlDbType.Char,1),
					new SqlParameter("@AheadTimes", SqlDbType.Int,4),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10),
                    new SqlParameter("@SourceFlag", SqlDbType.Char,1),
                    new SqlParameter("@PlanNoticeDate", SqlDbType.DateTime)  ,
                    new SqlParameter("@SourceID", SqlDbType.Int,4),
                    new SqlParameter("@ID",SqlDbType.Int,4),
                    new SqlParameter("@Important", SqlDbType.Char,1) };

            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = "未定义";
            parameters[2].Value = model.ArrangeTItle;
            parameters[3].Value = model.Critical;
            parameters[4].Value = model.Content;
            parameters[5].Value = model.StartDate;
            parameters[6].Value = model.StartTime;
            parameters[7].Value = model.EndTime;
            parameters[8].Value = model.Creator;
            parameters[9].Value = model.IsPublic;
            parameters[10].Value = DateTime.Now;
            parameters[11].Value = model.IsMobileNotice;
            parameters[12].Value = model.AheadTimes;
            parameters[13].Value = DateTime.Now;
            parameters[14].Value = model.ModifiedUserID;
            parameters[15].Value = '1';
            if (model.IsMobileNotice == "1")
            {
                try
                {
                    double hour = Convert.ToDouble(model.StartTime.Substring(0, 2));
                    double minite = Convert.ToDouble(model.StartTime.Substring(2, 2));
                    DateTime datetime = (DateTime)model.StartDate;
                    datetime = datetime.AddHours(hour);
                    datetime = datetime.AddMinutes(minite);
                    double aheadtime = Convert.ToDouble(model.AheadTimes) * -1;
                    datetime = datetime.AddHours(aheadtime);
                    parameters[16].Value = datetime;
                }
                catch
                {
                    parameters[16].Value = DateTime.Now;
                }


            }
            else
            {
                parameters[16].Value = DateTime.Now;
            }
            parameters[17].Value = model.ID;
            parameters[18].Value = model.ID;
            parameters[19].Value = model.Important;

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();
            //设置保存的参数
            foreach (SqlParameter sqp in parameters)
                comm.Parameters.Add(sqp);
            //返回操作结果  
            return SqlHelper.ExecuteTransWithCommand(comm);
        }

        public static bool DeleteAgendaInfo(int aid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from officedba.PersonalDateArrange   ");
            strSql.Append("  where  ID= @ID ");
            strSql.Append("delete  from  officedba.NoticeHistory  where  SourceID=@ID");
            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();

            comm.Parameters.AddWithValue("@ID", SqlDbType.Int);
            comm.Parameters["@ID"].Value = aid;
            //返回操作结果  
            return SqlHelper.ExecuteTransWithCommand(comm);
        }

        public static DataTable SelectAgendaById(int id)
        {

            string sqlstr = "select *, dbo.getEmployeeName(Creator) as CreatorName   from officedba.PersonalDateArrange where ID=@id";


            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlstr.ToString();
            //设置保存的参数
            comm.Parameters.AddWithValue("@id", SqlDbType.Int);
            comm.Parameters["@id"].Value = id;

            return SqlHelper.ExecuteSearch(comm);

        }

    }
}


/**********************************************
 * 类作用   日程安排数据处理层
 * 创建人   xz
 * 创建时间 2010-7-7 10:12:05 
 ***********************************************/
namespace XBase.Data.Personal.Agenda
{
    /// <summary>
    /// 日程安排数据处理类
    /// </summary>
    public class PersonalDateArrangeDBHelper
    {
        #region 字段

        //sql语句
        private const string C_SELECT_ALL =
                                @" SELECT ID,CompanyCD,ArrangeTItle,Critical,ArrangPerson,Content,StartDate,StartTime,EndTime,Creator
                                	,IsPublic,CreateDate,IsMobileNotice,AheadTimes,ModifiedDate,ModifiedUserID,Important,EndDate,ToManagerID,ManagerNote
                                	,ManagerDate,CanViewUser,CanViewUserName,Status
                                   FROM officedba.PersonalDateArrange";
        private const string C_SELECT_ID =
                               @"   SELECT pda.ID, pda.CompanyCD, pda.ArrangeTItle, pda.Critical, pda.ArrangPerson, 
                                           pda.[Content], SUBSTRING(CONVERT(VARCHAR, pda.StartDate, 120), 0, 11) AS StartDate, 
                                           SUBSTRING(CONVERT(VARCHAR, ISNULL(pda.EndDate, pda.StartDate), 120),0,11) AS EndDate, 
                                           pda.StartTime, pda.EndTime, pda.Creator, pda.IsPublic, pda.CreateDate, 
                                           pda.IsMobileNotice, pda.AheadTimes, pda.ModifiedDate, pda.ModifiedUserID, 
                                           pda.Important, pda.ToManagerID, pda.ManagerNote, pda.ManagerDate, pda.CanViewUser, 
                                           pda.CanViewUserName, pda.[Status], ei.EmployeeName AS ToManagerName, ei1.EmployeeName AS CreatorName
                                    FROM   officedba.PersonalDateArrange pda
                                    LEFT JOIN officedba.EmployeeInfo ei ON  ei.ID = pda.ToManagerID
                                    LEFT JOIN officedba.EmployeeInfo ei1 ON  ei1.ID = pda.Creator
                                  WHERE pda.ID =@ID";
        private const string C_SELECT =
                               @" SELECT ID,CompanyCD,ArrangeTItle,Critical,ArrangPerson,Content,StartDate,StartTime,EndTime,Creator
                                     ,IsPublic,CreateDate,IsMobileNotice,AheadTimes,ModifiedDate,ModifiedUserID,Important,EndDate,ToManagerID,ManagerNote
                                     ,ManagerDate,CanViewUser,CanViewUserName,Status
                                  FROM officedba.PersonalDateArrange
                                  WHERE CompanyCD=@CompanyCD  ";
        private const string C_INSERT =
                               @" INSERT officedba.PersonalDateArrange(
                                     CompanyCD,ArrangeTItle,Critical,ArrangPerson,Content,StartDate,StartTime,EndTime,Creator
                                     ,IsPublic,CreateDate,IsMobileNotice,AheadTimes,Important,EndDate,ToManagerID,CanViewUser,CanViewUserName,Status )
                                  VALUES (
                                     @CompanyCD,@ArrangeTItle,@Critical,@ArrangPerson,@Content,@StartDate,@StartTime,@EndTime,@Creator
                                     ,@IsPublic,@CreateDate,@IsMobileNotice,@AheadTimes,@Important,@EndDate,@ToManagerID,@CanViewUser,@CanViewUserName,@Status )";
        private const string C_UPDATE =
                               @" UPDATE officedba.PersonalDateArrange SET
                                     CompanyCD=@CompanyCD,ArrangeTItle=@ArrangeTItle,Critical=@Critical,ArrangPerson=@ArrangPerson
                                     ,Content=@Content,StartDate=@StartDate,StartTime=@StartTime,EndTime=@EndTime
                                     ,IsPublic=@IsPublic,IsMobileNotice=@IsMobileNotice,AheadTimes=@AheadTimes,ModifiedDate=@ModifiedDate
                                     ,ModifiedUserID=@ModifiedUserID,Important=@Important,EndDate=@EndDate,ToManagerID=@ToManagerID
                                     ,CanViewUser=@CanViewUser,CanViewUserName=@CanViewUserName,Status=@Status
                                  WHERE ID=@ID";
        private const string C_DELETE =
                               @" DELETE FROM officedba.PersonalDateArrange WHERE CompanyCD=@CompanyCD ";

        private const string C_DELETE_ID =
                               @" DELETE FROM officedba.PersonalDateArrange WHERE ID IN ({0}) ";


        //字段顺序变量定义
        private const byte m_iDCol = 0; // 自动生成列
        private const byte m_companyCDCol = 1; // 企业代码列
        private const byte m_arrangeTItleCol = 2; // 安排主题列
        private const byte m_criticalCol = 3; // 紧急程度(1宽松,2一般,3较急,4紧急,5特急)列
        private const byte m_arrangPersonCol = 4; // 保留列
        private const byte m_contentCol = 5; // 日程内容列
        private const byte m_startDateCol = 6; // 日程日期列
        private const byte m_startTimeCol = 7; // 开始时间（时分）列
        private const byte m_endTimeCol = 8; // 结束时间（时分）列
        private const byte m_creatorCol = 9; // 日程安排人ID(对应员工表ID)列
        private const byte m_isPublicCol = 10; // 保密度（0不公开，1公开）列
        private const byte m_createDateCol = 11; // 创建时间列
        private const byte m_isMobileNoticeCol = 12; // 是否手机短信提醒（0否，1是）列
        private const byte m_aheadTimesCol = 13; // 提前时间（小时）列
        private const byte m_modifiedDateCol = 14; // 最后更新日期列
        private const byte m_modifiedUserIDCol = 15; // 最后更新用户ID（对应操作用户表中的UserID）列
        private const byte m_importantCol = 16; // 重要程度(1不重要,2普通,3重要,4关键)列
        private const byte m_endDateCol = 17; // 结束日期列
        private const byte m_toManagerIDCol = 18; // 点评人ID列
        private const byte m_managerNoteCol = 19; // 点评内容列
        private const byte m_managerDateCol = 20; // 点评日期列
        private const byte m_canViewUserCol = 21; // 可查看人员ID列
        private const byte m_canViewUserNameCol = 22; // 可查看人员姓名列
        private const byte m_statusCol = 23; // 日程状态（0草稿，1提交,2已点评）列
        #endregion

        #region 方法

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD">自动生成</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int iD)
        {
            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_SELECT_ID);

            // 参数设置
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.Int, 4)
                        };
            parameters[0].Value = iD; // 自动生成

            // 执行
            return SqlHelper.ExecuteSql(sqlSentence.ToString(), parameters);
        }

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="companyCD">企业代码</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectWithKey(string companyCD)
        {
            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_SELECT);

            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, companyCD);

            // 执行
            return SqlHelper.ExecuteSql(sqlSentence.ToString(), parameters);
        }

        /// <summary>
        /// 列表界面查询方法
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页记录数</param>
        /// <param name="orderBy">排序方法</param>
        /// <param name="TotalCount">总记录数</param>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public static DataTable SelectListData(int pageIndex, int pageCount, string orderBy, ref int TotalCount
            , PersonalDateArrangeModel model)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder(C_SELECT_ALL);
            sql.Append(" WHERE 1=1 ");
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }

        /// <summary>
        /// 插入操作的执行命令
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>插入操作的执行命令</returns>
        public static SqlCommand InsertCommand(PersonalDateArrangeModel model)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_INSERT + " SET @IndexID = @@IDENTITY ";
            // 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);

            comm.Parameters.AddRange(parameters);
            SqlParameter IndexID = new SqlParameter("@IndexID", SqlDbType.Int);
            IndexID.Direction = ParameterDirection.Output;
            comm.Parameters.Add(IndexID);

            return comm;
        }

        /// <summary>
        /// 修改数据记录
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>修改操作的执行命令</returns>
        public static SqlCommand UpdateCommand(PersonalDateArrangeModel model)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_UPDATE;
            // 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);

            comm.Parameters.AddRange(parameters);

            return comm;
        }

        /// <summary>
        /// 修改数据记录
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>修改数据是否成功:true成功,false不成功</returns>
        public static bool Update(PersonalDateArrangeModel model)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_UPDATE);

            // 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);

            //执行SQL
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;

            return returnValue;
        }



        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">自动生成集合</param>
        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommand(string iD)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = String.Format(C_DELETE_ID, iD); // 自动生成集合

            return comm;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">自动生成集合</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(string iD)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder();

            sqlSentence.AppendFormat(C_DELETE_ID, iD);// 自动生成集合

            SqlParameter[] parameters = new SqlParameter[] { };

            // 执行
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;

            return returnValue;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="companyCD">企业代码</param>

        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommandWithKey(string companyCD)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_DELETE;
            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, companyCD);

            comm.Parameters.AddRange(parameters);

            return comm;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="companyCD">企业代码</param>

        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool DeleteWithKey(string companyCD)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_DELETE);

            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, companyCD);


            // 执行
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;

            return returnValue;
        }


        /// <summary>
        /// 设置查询和删除的参数数组
        /// </summary>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetSelectAndDeleteParameters()
        {
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@CompanyCD", SqlDbType.VarChar, 8) // 企业代码
                        };

            return parameters;
        }


        /// <summary>
        /// 设置新增和修改的参数数组
        /// </summary>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetInsertAndUpdateParameters()
        {
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.Int,4), // 自动生成
                            new SqlParameter("@CompanyCD", SqlDbType.VarChar,8), // 企业代码
                            new SqlParameter("@ArrangeTItle", SqlDbType.VarChar,100), // 安排主题
                            new SqlParameter("@Critical", SqlDbType.Char,1), // 紧急程度(1宽松,2一般,3较急,4紧急,5特急)
                            new SqlParameter("@ArrangPerson", SqlDbType.VarChar,200), // 保留
                            new SqlParameter("@Content", SqlDbType.VarChar,200), // 日程内容
                            new SqlParameter("@StartDate", SqlDbType.DateTime,8), // 日程日期
                            new SqlParameter("@StartTime", SqlDbType.VarChar,4), // 开始时间（时分）
                            new SqlParameter("@EndTime", SqlDbType.VarChar,4), // 结束时间（时分）
                            new SqlParameter("@Creator", SqlDbType.Int,4), // 日程安排人ID(对应员工表ID)
                            new SqlParameter("@IsPublic", SqlDbType.Char,1), // 保密度（0不公开，1公开）
                            new SqlParameter("@CreateDate", SqlDbType.DateTime,8), // 创建时间
                            new SqlParameter("@IsMobileNotice", SqlDbType.Char,1), // 是否手机短信提醒（0否，1是）
                            new SqlParameter("@AheadTimes", SqlDbType.Int,4), // 提前时间（小时）
                            new SqlParameter("@ModifiedDate", SqlDbType.DateTime,8), // 最后更新日期
                            new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10), // 最后更新用户ID（对应操作用户表中的UserID）
                            new SqlParameter("@Important", SqlDbType.Char,1), // 重要程度(1不重要,2普通,3重要,4关键)
                            new SqlParameter("@EndDate", SqlDbType.DateTime,8), // 结束日期
                            new SqlParameter("@ToManagerID", SqlDbType.Int,4), // 点评人ID
                            new SqlParameter("@ManagerNote", SqlDbType.VarChar,1024), // 点评内容
                            new SqlParameter("@ManagerDate", SqlDbType.DateTime,8), // 点评日期
                            new SqlParameter("@CanViewUser", SqlDbType.VarChar,1024), // 可查看人员ID
                            new SqlParameter("@CanViewUserName", SqlDbType.VarChar,1024), // 可查看人员姓名
                            new SqlParameter("@Status", SqlDbType.VarChar,1)  // 日程状态（0草稿，1提交,2已点评）
                        };

            return parameters;
        }



        /// <summary>
        /// 设置查询和删除的参数数组的值，此方法适用于两个字段作为主键的情况
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="companyCD">企业代码的值</param>

        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetSelectAndDeleteParametersValue(SqlParameter[] parameters, string companyCD)
        {
            parameters[0].Value = companyCD; // 企业代码


            return parameters;
        }



        /// <summary>
        /// 设置新增和修改的参数数组的值
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="model">实体类</param>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetInsertAndUpdateParametersValue(SqlParameter[] parameters, PersonalDateArrangeModel model)
        {
            if (!model.ID.HasValue) parameters[m_iDCol].Value = System.DBNull.Value; else parameters[m_iDCol].Value = model.ID; // 自动生成
            parameters[m_companyCDCol].Value = model.CompanyCD; // 企业代码
            parameters[m_arrangeTItleCol].Value = model.ArrangeTItle; // 安排主题
            parameters[m_criticalCol].Value = model.Critical; // 紧急程度(1宽松,2一般,3较急,4紧急,5特急)
            parameters[m_arrangPersonCol].Value = model.ArrangPerson; // 保留
            parameters[m_contentCol].Value = model.Content; // 日程内容
            if (!model.StartDate.HasValue) parameters[m_startDateCol].Value = System.DBNull.Value; else parameters[m_startDateCol].Value = model.StartDate; // 日程日期
            parameters[m_startTimeCol].Value = model.StartTime; // 开始时间（时分）
            parameters[m_endTimeCol].Value = model.EndTime; // 结束时间（时分）
            if (!model.Creator.HasValue) parameters[m_creatorCol].Value = System.DBNull.Value; else parameters[m_creatorCol].Value = model.Creator; // 日程安排人ID(对应员工表ID)
            parameters[m_isPublicCol].Value = model.IsPublic; // 保密度（0不公开，1公开）
            if (!model.CreateDate.HasValue) parameters[m_createDateCol].Value = System.DBNull.Value; else parameters[m_createDateCol].Value = model.CreateDate; // 创建时间
            parameters[m_isMobileNoticeCol].Value = model.IsMobileNotice; // 是否手机短信提醒（0否，1是）
            if (!model.AheadTimes.HasValue) parameters[m_aheadTimesCol].Value = System.DBNull.Value; else parameters[m_aheadTimesCol].Value = model.AheadTimes; // 提前时间（小时）
            if (!model.ModifiedDate.HasValue) parameters[m_modifiedDateCol].Value = System.DBNull.Value; else parameters[m_modifiedDateCol].Value = model.ModifiedDate; // 最后更新日期
            parameters[m_modifiedUserIDCol].Value = model.ModifiedUserID; // 最后更新用户ID（对应操作用户表中的UserID）
            parameters[m_importantCol].Value = model.Important; // 重要程度(1不重要,2普通,3重要,4关键)
            if (!model.EndDate.HasValue) parameters[m_endDateCol].Value = System.DBNull.Value; else parameters[m_endDateCol].Value = model.EndDate; // 结束日期
            if (!model.ToManagerID.HasValue) parameters[m_toManagerIDCol].Value = System.DBNull.Value; else parameters[m_toManagerIDCol].Value = model.ToManagerID; // 点评人ID
            parameters[m_managerNoteCol].Value = model.ManagerNote; // 点评内容
            if (!model.ManagerDate.HasValue) parameters[m_managerDateCol].Value = System.DBNull.Value; else parameters[m_managerDateCol].Value = model.ManagerDate; // 点评日期
            parameters[m_canViewUserCol].Value = model.CanViewUser; // 可查看人员ID
            parameters[m_canViewUserNameCol].Value = model.CanViewUserName; // 可查看人员姓名
            parameters[m_statusCol].Value = model.Status; // 日程状态（0草稿，1提交,2已点评）

            return parameters;
        }


        #endregion

        #region 自定义
        /// <summary>
        /// 更新点评信息
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>修改数据是否成功:true成功,false不成功</returns>
        public static bool UpdateManager(PersonalDateArrangeModel model)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(@"UPDATE officedba.PersonalDateArrange
SET    ManagerNote = @ManagerNote,
       ManagerDate = @ManagerDate,
       [Status] = @Status
WHERE  ID = @ID");

            // 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);

            //执行SQL
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;

            return returnValue;
        }

        /// <summary>
        /// 添加短信命令
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SqlCommand InsertMessage(PersonalDateArrangeModel model)
        {
            SqlCommand cmd = new SqlCommand();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE  FROM  officedba.NoticeHistory  WHERE CompanyCD=@CompanyCD AND SourceID=@ID AND SourceFlag =@SourceFlag  ");
            if (model.IsMobileNotice == "1")
            {// 添加短信
                strSql.Append("insert into officedba.NoticeHistory(");
                strSql.Append("   CompanyCD,SourceFlag,SourceID,PlanNoticeDate )  ");
                strSql.Append("      values(@CompanyCD, @SourceFlag, @ID, @PlanNoticeDate  ) ");
            }
            cmd.CommandText = strSql.ToString();
            cmd.Parameters.Add(new SqlParameter("@CompanyCD", model.CompanyCD));
            cmd.Parameters.Add(new SqlParameter("@SourceFlag", 1));
            cmd.Parameters.Add(new SqlParameter("@ID", model.ID.Value));
            DateTime dt = DateTime.Now;
            if (model.IsMobileNotice == "1")
            {// 计算发送时间
                try
                {
                    double hour = Convert.ToDouble(model.StartTime.Substring(0, 2));
                    double minite = Convert.ToDouble(model.StartTime.Substring(2, 2));
                    DateTime datetime = (DateTime)model.StartDate;
                    datetime = datetime.AddHours(hour);
                    datetime = datetime.AddMinutes(minite);
                    double aheadtime = Convert.ToDouble(model.AheadTimes) * -1;
                    datetime = datetime.AddHours(aheadtime);
                    dt = datetime;
                }
                catch
                {
                }
            }
            cmd.Parameters.Add(new SqlParameter("@PlanNoticeDate", dt));
            return cmd;
        }
        #endregion
    }
}

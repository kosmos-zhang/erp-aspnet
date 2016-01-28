/**********************************************
 * 类作用：   个人桌面模块数据层处理
 * 建立人：   王乾睿
 * 建立时间： 2009.4.6
 ***********************************************/
using System;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using XBase.Common;
using System.Collections;

using XBase.Model.Personal.AimManager;
using XBase.Data.Common;

namespace XBase.Data.Personal.AimManager
{
    /// <summary>
    /// 类名：AimDBHelper
    /// 描述：目标管理模块数据层方法
    /// 
    /// 作者：王乾睿
    /// 创建时间：2009.4.6
    /// 最后修改时间：2009.4.6
    /// </summary>
    ///
    public class AimInfoDBHelper : System.Web.SessionState.IRequiresSessionState
    {
        #region 获取所有员工列表
        /// <summary>
        /// 获取所有员工的ID和姓名等信息以供备选
        /// </summary>
        /// <param name="AttendanceInfo">工作日信息</param>
        /// <returns>返回的是获取到的员工姓名和ID的表格</returns>
        public static DataTable GetStaffNameList()
        {
            string sqlStr = "SELECT  ID,EmployeeName FROM  officedba. EmployeeInfo";
            return SqlHelper.ExecuteSql(sqlStr);
        }
        #endregion

        #region  目标表信息编辑
        public static int InsertAimInfo(AimInfoModel model)
        {
            #region  SQL语句拼写
            StringBuilder InsertAimSqlString = new StringBuilder();
            InsertAimSqlString.Append("INSERT INTO  officedba.PlanAim");
            InsertAimSqlString.Append("  (      CompanyCD,");
            InsertAimSqlString.Append("     AimNo ,");
            InsertAimSqlString.Append("      AimTypeID ,");
            InsertAimSqlString.Append("      AimTitle ,");
            InsertAimSqlString.Append("      AimContent,");
            InsertAimSqlString.Append("      AimStandard ,");
            InsertAimSqlString.Append("      AimFlag ,");
            InsertAimSqlString.Append("      JoinUserIDList ,");
            InsertAimSqlString.Append("      JoinUserNameList,");
            InsertAimSqlString.Append("      CanViewUser ,");
            InsertAimSqlString.Append("      CanViewUserName ,");
            InsertAimSqlString.Append("      IsMobileNotice ,");
            if (model.IsMobileNotice =="1" )
            InsertAimSqlString.Append("      RemindTime,");
            InsertAimSqlString.Append("      PrincipalID ,");
            InsertAimSqlString.Append("       Attachment,");
            InsertAimSqlString.Append("       Critical,");
            InsertAimSqlString.Append("      Important ,");
            InsertAimSqlString.Append("       Priority,");
            InsertAimSqlString.Append("      AttentionName ,");
            InsertAimSqlString.Append("      Status ,");
            InsertAimSqlString.Append("      Remark ,");
            InsertAimSqlString.Append("      AimStep ,");
            InsertAimSqlString.Append("      Resources ,");
            InsertAimSqlString.Append("      Qustion ,");
            InsertAimSqlString.Append("      Method ,");
            InsertAimSqlString.Append("      Creator ,");
            InsertAimSqlString.Append("      Checkor ,");
            InsertAimSqlString.Append("       CreateDate,");
            InsertAimSqlString.Append("       Coordinator,");
            InsertAimSqlString.Append("       StartDate,");
            InsertAimSqlString.Append("       EndDate,");
            InsertAimSqlString.Append("       ModifiedUserID,");
            InsertAimSqlString.Append("       AimDate,");
            InsertAimSqlString.Append("       AimNum,");
            InsertAimSqlString.Append("       IsDirection,");
            InsertAimSqlString.Append("       DeptID)");
            InsertAimSqlString.Append("   VALUES   ");
            InsertAimSqlString.Append(" (  @CompanyCD,           ");
            InsertAimSqlString.Append("    @AimNo                    ,          ");
            InsertAimSqlString.Append("    @AimTypeID                         ,");
            InsertAimSqlString.Append("    @AimTitle                   ,");
            InsertAimSqlString.Append("    @AimContent               ,");
            InsertAimSqlString.Append("    @AimStandard                  ,");
            InsertAimSqlString.Append("    @AimFlag                             ,");
            InsertAimSqlString.Append("    @JoinUserIDList                 ,");
            InsertAimSqlString.Append("    @JoinUserNameList          ,");
            InsertAimSqlString.Append("    @CanViewUser       ,");
            InsertAimSqlString.Append("    @CanViewUserName       ,");
            InsertAimSqlString.Append("    @IsMobileNotice        ,");
            if (model.IsMobileNotice == "1")
            InsertAimSqlString.Append("    @RemindTime     ,           ");
            InsertAimSqlString.Append("    @PrincipalID                       ,");
            InsertAimSqlString.Append("    @Attachment                              ,");
            InsertAimSqlString.Append("    @Critical                           ,");
            InsertAimSqlString.Append("    @Important                         ,");
            InsertAimSqlString.Append("    @Priority                        ,");
            InsertAimSqlString.Append("    @AttentionName                     ,");
            InsertAimSqlString.Append("    @Status                        ,");
            InsertAimSqlString.Append("    @Remark                                   ,");
            InsertAimSqlString.Append("    @AimStep                                   ,");
            InsertAimSqlString.Append("    @Resources                                   ,");
            InsertAimSqlString.Append("    @Qustion                                   ,");
            InsertAimSqlString.Append("    @Method                                   ,");
            InsertAimSqlString.Append("    @Creator                               ,");
            InsertAimSqlString.Append("    @Checkor                               ,");
            InsertAimSqlString.Append("    @CreateDate                             ,");
            InsertAimSqlString.Append("    @Coordinator                                ,");
            InsertAimSqlString.Append("    @StartDate                          ,");
            InsertAimSqlString.Append("    @EndDate                            ,");
            InsertAimSqlString.Append("    @ModifiedUserID                  ,");
            InsertAimSqlString.Append("    @AimDate,");
            InsertAimSqlString.Append("    @AimNum, ");
            InsertAimSqlString.Append("    @IsDirection, ");
            InsertAimSqlString.Append("    @DeptID  )");
            InsertAimSqlString.Append(";select @RID =@@IDENTITY  ");
            #endregion
            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = InsertAimSqlString.ToString();
            //设置保存的参数
            SetAimInfoParm(comm, model);
            SqlParameter sqlp = new SqlParameter("@RID",SqlDbType.Int,4)  ;
            sqlp.Direction = ParameterDirection.Output;
            comm.Parameters.Add(sqlp);
             
            //返回操作结果  
            SqlHelper.ExecuteTransWithCommand(comm);

            if (model.IsMobileNotice == "1")
            {
              
                StringBuilder strSql = new StringBuilder(); 
                strSql.Append("insert into officedba.NoticeHistory(");
                strSql.Append("   CompanyCD,SourceFlag,SourceID,PlanNoticeDate  )  ");
                strSql.Append("      values(@CompanyCD, @SourceFlag,@SourceID,@PlanNoticeDate  ) ");
                SqlCommand commN = new SqlCommand();
                commN.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar);
                commN.Parameters["@CompanyCD"].Value = model.CompanyCD;
                commN.Parameters.AddWithValue("@SourceFlag", SqlDbType.VarChar);
                commN.Parameters["@SourceFlag"].Value = "2";
                commN.Parameters.AddWithValue("@SourceID", SqlDbType.Int);
                commN.Parameters["@SourceID"].Value = sqlp.Value;
                commN.Parameters.AddWithValue("@PlanNoticeDate", SqlDbType.DateTime);
                commN.Parameters["@PlanNoticeDate"].Value = model.RemindTime;
                commN.CommandText = strSql.ToString();
                SqlHelper.ExecuteTransWithCommand(commN);
            }

            return (int)sqlp.Value;         
        }

        public static int UpdateAimInfo(AimInfoModel model)
        {

            #region  SQL语句拼写
            StringBuilder UpdateAimSqlString = new StringBuilder();
            UpdateAimSqlString.Append("UPDATE  officedba.PlanAim");
            UpdateAimSqlString.Append("   SET   ");
            UpdateAimSqlString.Append("      AimTitle=@AimTitle ,");
            UpdateAimSqlString.Append("      AimContent=@AimContent,");
            UpdateAimSqlString.Append("      AimStandard=@AimStandard ,");
            UpdateAimSqlString.Append("      PrincipalID=@PrincipalID ,");
            UpdateAimSqlString.Append("      Checkor=@Checkor ,");
            UpdateAimSqlString.Append("       Attachment=@Attachment,");
            UpdateAimSqlString.Append("      Status=@Status ,");
            UpdateAimSqlString.Append("      AimStep=@AimStep ,");
            UpdateAimSqlString.Append("      Resources=@Resources ,");
            UpdateAimSqlString.Append("      Qustion=@Qustion ,");
            UpdateAimSqlString.Append("      Method=@Method ,");
            UpdateAimSqlString.Append("      Remark=@Remark ,");
            UpdateAimSqlString.Append("       StartDate=@StartDate,");
            UpdateAimSqlString.Append("       EndDate=@EndDate,");
            UpdateAimSqlString.Append("       ModifiedUserID=@ModifiedUserID,");
            UpdateAimSqlString.Append("       DeptID=@DeptID,");
            UpdateAimSqlString.Append("       Coordinator=@Coordinator,");
            UpdateAimSqlString.Append("       JoinUserNameList=@JoinUserNameList,");
            UpdateAimSqlString.Append("       JoinUserIDList=@JoinUserIDList,");
            UpdateAimSqlString.Append("      CanViewUser=@CanViewUser ,");
            UpdateAimSqlString.Append("      CanViewUserName =@CanViewUserName ,");
            UpdateAimSqlString.Append("      AttentionName =@AttentionName ,");
            UpdateAimSqlString.Append("      IsMobileNotice=@IsMobileNotice ,");
            if (model.IsMobileNotice == "1")
                UpdateAimSqlString.Append("      RemindTime =@RemindTime,");
            UpdateAimSqlString.Append("       ModifiedDate=@ModifiedDate,  ");
            UpdateAimSqlString.Append("       IsDirection=@IsDirection  ");
            UpdateAimSqlString.Append("   WHERE   ID =@ID  ");

            #endregion
            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = UpdateAimSqlString.ToString();
            //设置保存的参数
            SetUpdateAimInfoParm(comm, model);

            //返回操作结果  
            if (SqlHelper.ExecuteTransWithCommand(comm))
            {
                if (model.IsMobileNotice == "1")
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete  from  officedba.NoticeHistory  where  SourceID=@ID and  SourceFlag ='2'  ");
                strSql.Append("insert into officedba.NoticeHistory(");
                strSql.Append("   CompanyCD,SourceFlag,SourceID,PlanNoticeDate  )  ");
                strSql.Append("      values(@CompanyCD, @SourceFlag,@SourceID,@PlanNoticeDate  ) ");
                SqlCommand commN = new SqlCommand();
                commN.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar);
                commN.Parameters["@CompanyCD"].Value = model.CompanyCD;
                commN.Parameters.AddWithValue("@SourceFlag", SqlDbType.VarChar);
                commN.Parameters["@SourceFlag"].Value ="2" ;
                commN.Parameters.AddWithValue("@SourceID", SqlDbType.Int);
                commN.Parameters["@SourceID"].Value = model.ID;
                commN.Parameters.AddWithValue("@PlanNoticeDate", SqlDbType.DateTime);
                commN.Parameters["@PlanNoticeDate"].Value = model.RemindTime;
                commN.Parameters.AddWithValue("@ID", SqlDbType.Int);
                commN.Parameters["@ID"].Value = model.ID;
                commN.CommandText = strSql.ToString();
                SqlHelper.ExecuteTransWithCommand(commN);
            } else
                {
                    StringBuilder strSql = new StringBuilder();
                    SqlCommand commN = new SqlCommand();
                    strSql.Append("delete  from  officedba.NoticeHistory  where  SourceID=@ID and  SourceFlag ='2'  ");
                    commN.Parameters.AddWithValue("@ID", SqlDbType.Int);
                    commN.Parameters["@ID"].Value = model.ID;
                    commN.CommandText = strSql.ToString();
                    SqlHelper.ExecuteTransWithCommand(commN);
                }
                return 1;
            }
            else {
                return 0;
            }

        }
        public static bool AddSummarizeNote(AimInfoModel model)
        {
            string sqlStr = "UPDATE   officedba.PlanAim SET   SummarizeNote = @SummarizeNote,  Status='4' ,AimRealResult=@AimRealResult, " +
                                    "   Summarizer=@Summarizer,  " +
                                    "      AddOrCut = @AddOrCut ,     " +
                                    "    Difference=@Difference,   " +
                                    "  CompletePercent =@CompletePercent ," +
                                    "   SummarizeDate = @SummarizeDate    " +
                                    " WHERE  ID = @ID ";
            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置两个参数
            comm.CommandText = sqlStr;

            comm.Parameters.AddWithValue("@Summarizer", SqlDbType.Int);
            comm.Parameters["@Summarizer"].Value = model.Summarizer;

            comm.Parameters.AddWithValue("@SummarizeNote", SqlDbType.VarChar);
            comm.Parameters["@SummarizeNote"].Value = model.SummarizeNote;

            comm.Parameters.AddWithValue("@CompletePercent", SqlDbType.Decimal);
            comm.Parameters["@CompletePercent"].Value = model.CompletePercent;

            comm.Parameters.AddWithValue("@AimRealResult", SqlDbType.VarChar);
            comm.Parameters["@AimRealResult"].Value = model.AimRealResult;

            comm.Parameters.AddWithValue("@AddOrCut", SqlDbType.VarChar);
            comm.Parameters["@AddOrCut"].Value = model.AddOrCut;

            comm.Parameters.AddWithValue("@Difference", SqlDbType.VarChar);
            comm.Parameters["@Difference"].Value = model.Difference;

            comm.Parameters.AddWithValue("@SummarizeDate", SqlDbType.VarChar);
            comm.Parameters["@SummarizeDate"].Value = DateTime.Now;

            comm.Parameters.AddWithValue("@ID", SqlDbType.Int);
            comm.Parameters["@ID"].Value = model.ID;

            //返回操作结果  
            return SqlHelper.ExecuteTransWithCommand(comm);

        }
        public static bool AddAimCheck(AimInfoModel model)
        {
            string sqlStr = "UPDATE   officedba.PlanAim SET   CheckNote = @CheckNote,  Status='5', " +
                                   "   CheckUserID=@CheckUserID,  " +
                                   "    CheckScore=@CheckScore,  " +
                                   "  ScoreNote =@ScoreNote ," +
                                   "   CheckDate = @CheckDate    " +
                                   " WHERE  ID = @ID ";
            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置两个参数
            comm.CommandText = sqlStr;

            comm.Parameters.AddWithValue("@CheckUserID", SqlDbType.Int);
            comm.Parameters["@CheckUserID"].Value = model.CheckUserID;

            comm.Parameters.AddWithValue("@CheckNote", SqlDbType.VarChar);
            comm.Parameters["@CheckNote"].Value = model.CheckNote;

            comm.Parameters.AddWithValue("@ScoreNote", SqlDbType.VarChar);
            comm.Parameters["@ScoreNote"].Value = model.ScoreNote;

            comm.Parameters.AddWithValue("@CheckScore", SqlDbType.Int);
            comm.Parameters["@CheckScore"].Value = model.CheckScore;

            comm.Parameters.AddWithValue("@CheckDate", SqlDbType.VarChar);
            comm.Parameters["@CheckDate"].Value = DateTime.Now;

            comm.Parameters.AddWithValue("@ID", SqlDbType.Int);
            comm.Parameters["@ID"].Value = model.ID;

            //返回操作结果  
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        public static bool DelAimInfoByIdArray(string[] IdArray)
        {
            string sqlStr = "DELETE   officedba.PlanAim  WHERE  " + SetDeleteWhereString(IdArray) + " AND  Status = '1'   ";

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置两个参数
            comm.CommandText = sqlStr;
            //返回操作结果  
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        public static bool ChangeAimStatus(int aimid, string sta, string celreson, int oprateorid)
        {
            string sqlStr = string.Empty;
            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            if (sta != "1" && sta != "3")
            {
                sqlStr = "UPDATE   officedba.PlanAim SET   Status =@Status    WHERE    ID=@ID     ";
                //添加参数
                comm.Parameters.AddWithValue("@ID", SqlDbType.Int);
                comm.Parameters["@ID"].Value = aimid;

                comm.Parameters.AddWithValue("@Status", SqlDbType.Char);
                comm.Parameters["@Status"].Value = sta;
            }
            else if (sta == "1")
            {
                sqlStr = "UPDATE   officedba.PlanAim SET   Status =@Status , Confirmor=@Confirmor , ConfirmDate=@ConfirmDate  WHERE    ID=@ID      ";
                //添加参数
                comm.Parameters.AddWithValue("@ID", SqlDbType.Int);
                comm.Parameters["@ID"].Value = aimid;

                comm.Parameters.AddWithValue("@Status", SqlDbType.Char);
                comm.Parameters["@Status"].Value = sta;

                comm.Parameters.AddWithValue("@Confirmor", SqlDbType.Int);
                comm.Parameters["@Confirmor"].Value = oprateorid;

                comm.Parameters.AddWithValue("@ConfirmDate", SqlDbType.DateTime);
                comm.Parameters["@ConfirmDate"].Value = DateTime.Now;

            }
            else if (sta == "3")
            {
                sqlStr = "UPDATE   officedba.PlanAim SET   Status =@Status , Canceler=@Canceler , CancelDate=@CancelDate , CancelReson=@CancelReson   WHERE    ID=@ID     ";
                //添加参数
                comm.Parameters.AddWithValue("@ID", SqlDbType.Int);
                comm.Parameters["@ID"].Value = aimid;

                comm.Parameters.AddWithValue("@Status", SqlDbType.Char);
                comm.Parameters["@Status"].Value = sta;

                comm.Parameters.AddWithValue("@Canceler", SqlDbType.Int);
                comm.Parameters["@Canceler"].Value = oprateorid;

                comm.Parameters.AddWithValue("@CancelDate", SqlDbType.DateTime);
                comm.Parameters["@CancelDate"].Value = DateTime.Now;

                comm.Parameters.AddWithValue("@CancelReson", SqlDbType.VarChar);
                comm.Parameters["@CancelReson"].Value = celreson;

            }

            comm.CommandText = sqlStr;
            //返回操作结果  
            bool result = false;

            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                FlowDBHelper.OperateCancelConfirm(userInfo.CompanyCD, 1, 1, aimid, userInfo.UserID, tran);//撤销审批 
                result =  SqlHelper.ExecuteTransWithCommand(comm);
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }

            return result;
        }
        #endregion

        #region 目标列表查询


        public static DataTable SelectAimListReportDept(Hashtable parm)
        {
            #region  SQL语句拼写
            string whereStr = SetSearchReportWhereString(parm);
            StringBuilder SelectAimListSqlString = new StringBuilder();
            if (parm["ForWhich"].ToString() == "PIC")
            {
                SelectAimListSqlString.Append("SELECT  count(*)  as Num , dbo.getDeptNameByID(DeptID) as DeptName ,DeptID  ");
                SelectAimListSqlString.Append("          FROM  officedba.PlanAim   ");
                SelectAimListSqlString.Append(whereStr);
                SelectAimListSqlString.Append("  GROUP BY DeptID ");
            }
            else
            {
                SelectAimListSqlString.Append("SELECT  *,dbo.getDeptNameByID(DeptID) as DeptName, dbo.getEmployeeName(PrincipalID) as PrincipalName  ");
                SelectAimListSqlString.Append("          FROM  officedba.PlanAim   ");
                SelectAimListSqlString.Append(whereStr);
            }

            #endregion

            DataTable dt = new DataTable();
            // 执行查找操作
            int totalcount = 0;
            dt = SqlHelper.CreateSqlByPageExcuteSql(SelectAimListSqlString.ToString(), 1, 99999, "DeptID", new SqlParameter[0], ref totalcount);

            return dt;

        }
        public static DataTable SelectAimListReportDeptPrint(Hashtable parm, int pageindex, int pagecount, ref int totalcount)
        {
            #region  SQL语句拼写
            string whereStr = SetSearchReportWhereString(parm);
            StringBuilder SelectAimListSqlString = new StringBuilder();
            if (parm["ForWhich"].ToString() == "PIC")
            {
                SelectAimListSqlString.Append("SELECT  count(*)  as Num , dbo.getDeptNameByID(DeptID) as DeptName ,DeptID  ");
                SelectAimListSqlString.Append("          FROM  officedba.PlanAim   ");
                SelectAimListSqlString.Append(whereStr);
                SelectAimListSqlString.Append("  GROUP BY DeptID ");
            }
            else
            {
                SelectAimListSqlString.Append("SELECT  *,dbo.getDeptNameByID(DeptID) as DeptName, dbo.getEmployeeName(PrincipalID) as PrincipalName, dbo.getEmployeeName(Creator) as CreatorName ");
                SelectAimListSqlString.Append("          FROM  officedba.PlanAim   ");
                SelectAimListSqlString.Append(whereStr);
            }

            #endregion

            DataTable dt = new DataTable();
            // 执行查找操作
            dt = SqlHelper.CreateSqlByPageExcuteSql(SelectAimListSqlString.ToString(), pageindex, pagecount, "ID", new SqlParameter[0], ref totalcount);

            return dt;

        }

        public static DataTable GetAimListReportPrincipal(Hashtable parm)
        {
            #region  SQL语句拼写
            string whereStr = SetSearchReportWhereString(parm);

            StringBuilder SelectAimListSqlString = new StringBuilder();
            if (parm["ForWhich"].ToString() == "PIC")
            {
                SelectAimListSqlString.Append("SELECT  count(*)  as Num , dbo.getEmployeeName(PrincipalID) as PrincipalName ,PrincipalID  ");
                SelectAimListSqlString.Append("          FROM  officedba.PlanAim   ");
                SelectAimListSqlString.Append(whereStr);
                SelectAimListSqlString.Append("  GROUP BY PrincipalID ");
            }
            else
            {
                SelectAimListSqlString.Append("SELECT  *,dbo.getDeptNameByID(DeptID) as DeptName, dbo.getEmployeeName(PrincipalID) as PrincipalName   ");
                SelectAimListSqlString.Append("          FROM  officedba.PlanAim   ");
                SelectAimListSqlString.Append(whereStr);
            }

            #endregion

            int totalcount = 0;
            DataTable dt = new DataTable();
            // 执行查找操作
            dt = SqlHelper.CreateSqlByPageExcuteSql(SelectAimListSqlString.ToString(), 1, 99999, "PrincipalID", new SqlParameter[0], ref totalcount);

            return dt;

        }
        public static DataTable GetAimListReportPrincipalPrint(Hashtable parm, int pagesize, int pagecount, ref int totalcount)
        {
            #region  SQL语句拼写
            string whereStr = SetSearchReportWhereString(parm);

            StringBuilder SelectAimListSqlString = new StringBuilder();
            if (parm["ForWhich"].ToString() == "PIC")
            {
                SelectAimListSqlString.Append("SELECT  count(*)  as Num , dbo.getEmployeeName(PrincipalID) as PrincipalName ,PrincipalID  ");
                SelectAimListSqlString.Append("          FROM  officedba.PlanAim   ");
                SelectAimListSqlString.Append(whereStr);
                SelectAimListSqlString.Append("  GROUP BY PrincipalID ");
            }
            else
            {
                SelectAimListSqlString.Append("SELECT  *,dbo.getDeptNameByID(DeptID) as DeptName, dbo.getEmployeeName(PrincipalID) as PrincipalName, dbo.getEmployeeName(Creator) as CreatorName    ");
                SelectAimListSqlString.Append("          FROM  officedba.PlanAim   ");
                SelectAimListSqlString.Append(whereStr);
            }

            #endregion


            DataTable dt = new DataTable();
            // 执行查找操作
            dt = SqlHelper.CreateSqlByPageExcuteSql(SelectAimListSqlString.ToString(), pagesize, pagecount, "PrincipalID", new SqlParameter[0], ref totalcount);

            return dt;

        }


        public static DataTable GetAimListReportStatus(Hashtable parm)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region  SQL语句拼写
            string whereStr = SetSearchWhereString(parm, parm["EID"].ToString());

            StringBuilder SelectAimListSqlString = new StringBuilder();
            if (parm["ForWhich"].ToString() == "PIC")
            {
                SelectAimListSqlString.Append("SELECT  count(*)  as Num , Status  ");
                SelectAimListSqlString.Append("          FROM  officedba.PlanAim   ");
                SelectAimListSqlString.Append(whereStr);
                SelectAimListSqlString.Append("  GROUP BY Status ");
            }
            else
            {
                SelectAimListSqlString.Append(" SELECT  *,dbo.getDeptNameByID(DeptID) as DeptName, dbo.getEmployeeName(PrincipalID) as PrincipalName   ");
                SelectAimListSqlString.Append("          FROM  officedba.PlanAim   ");
                SelectAimListSqlString.Append(whereStr);
            }

            #endregion
            //定义更新基本信息的命令 
            SqlCommand comm = new SqlCommand();
            comm.CommandText = SelectAimListSqlString.ToString();
            //设置保存的参数
            SetSearchParm(comm, parm, 1, 10000);

            DataTable dt = new DataTable();
            // 执行查找操作
            dt = SqlHelper.ExecuteSearch(comm);

            return dt;

        }

        public static DataTable GetAimListReportStatus1(Hashtable parm)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region  SQL语句拼写
            string whereStr = SetSearchWhereString1(parm, parm["EID"].ToString());

            StringBuilder SelectAimListSqlString = new StringBuilder();
            if (parm["ForWhich"].ToString() == "PIC")
            {
                SelectAimListSqlString.Append("SELECT  count(*)  as Num , Status  ");
                SelectAimListSqlString.Append("          FROM  officedba.PlanAim   ");
                SelectAimListSqlString.Append(whereStr);
                SelectAimListSqlString.Append("  GROUP BY Status ");
            }
            else
            {
                SelectAimListSqlString.Append("SELECT  *,dbo.getDeptNameByID(DeptID) as DeptName, dbo.getEmployeeName(PrincipalID) as PrincipalName   ");
                SelectAimListSqlString.Append("          FROM  officedba.PlanAim   ");
                SelectAimListSqlString.Append(whereStr);
            }

            #endregion
            //定义更新基本信息的命令 
            SqlCommand comm = new SqlCommand();
            comm.CommandText = SelectAimListSqlString.ToString();
            //设置保存的参数
            SetSearchParm(comm, parm, 1, 10000);

            DataTable dt = new DataTable();
            // 执行查找操作
            dt = SqlHelper.ExecuteSearch(comm);

            return dt;

        }

        public static DataTable SelectAimList(int pageindex, int pagesize, Hashtable parm,out int RecordCount )
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region  SQL语句拼写
            string whereStr = SetSearchWhereString(parm, parm["EID"].ToString());
            string orderbyStr = "  ORDER BY pa.id DESC   ";
            if (parm.ContainsKey("OrderBy"))
            {
                orderbyStr = parm["OrderBy"].ToString();
            }
            StringBuilder SelectAimListSqlString = new StringBuilder(@"SELECT  count(*) as num ,[BillTypeFlag] ,[BillTypeCode] ,[BillNo],max(ID) as aimflowid
                                                                                                           into #temp_table     FROM [officedba].[FlowInstance]  
                                                                                                           where BillTypeFlag=1 and  BillTypeCode=1  group by  [BillTypeFlag] ,[BillTypeCode] ,[BillNo]   ");
            SelectAimListSqlString.Append(" SELECT TOP   " + pagesize + "   *  ,  officedba.getEmployeeNameByID(PrincipalID)  AS  PrincipalName,");
            SelectAimListSqlString.Append("   officedba.getEmployeeNameByID(Coordinator) AS CoordinatorName ,    ");
            SelectAimListSqlString.Append("   officedba.getEmployeeNameByID(Creator) AS CreatorName     ");
            SelectAimListSqlString.Append("          FROM  officedba.PlanAim as pa left join  #temp_table  on pa.AimNo = #temp_table.BillNo  left join officedba.FlowInstance ff on  #temp_table.aimflowid = ff.id ");
            SelectAimListSqlString.Append("       WHERE (  pa.ID NOT IN   ");
            SelectAimListSqlString.Append("         (SELECT TOP (" + pagesize + "*" + pageindex + ")  pa1.id   ");
            SelectAimListSqlString.Append("       FROM officedba.PlanAim  as pa1 left join  #temp_table  on pa1.AimNo = #temp_table.BillNo  ");
            SelectAimListSqlString.Append(whereStr.Replace("CompanyCD=", "pa1.CompanyCD="));
            SelectAimListSqlString.Append("        " + orderbyStr.Replace("pa.id DESC", "pa1.id DESC") + " ))    ");
            SelectAimListSqlString.Append(whereStr.Replace("WHERE", " AND  ").Replace("CompanyCD=", "pa.CompanyCD="));
            SelectAimListSqlString.Append(orderbyStr);
            SelectAimListSqlString.Append(";select  @RecordCount=count(*)   from officedba.PlanAim as pa2 left join  #temp_table  on pa2.AimNo = #temp_table.BillNo  left join officedba.FlowInstance ff on  #temp_table.aimflowid = ff.id ");
            SelectAimListSqlString.Append(whereStr.Replace("CompanyCD=", "pa2.CompanyCD="));

            SelectAimListSqlString.Append("  Drop   Table   #temp_table   ");
            #endregion
            //定义更新基本信息的命令 
            SqlCommand comm = new SqlCommand();
            comm.CommandText = SelectAimListSqlString.ToString();
            //设置保存的参数
            SetSearchParm(comm, parm, pageindex, pagesize);

            DataTable dt = new DataTable();
            // 执行查找操作
            dt= SqlHelper.ExecuteSearch(comm);
            try
            {
                RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value.ToString());
            }
            catch
            {
                RecordCount = 0;
            }
            return dt;
            
        }

        public static DataTable SelectAimList1(int pageindex, int pagesize, Hashtable parm, out int RecordCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region  SQL语句拼写
            string whereStr = SetSearchWhereString1(parm, parm["EID"].ToString());
            string orderbyStr = "  ORDER BY pa.id DESC   ";
            if (parm.ContainsKey("OrderBy"))
            {
                orderbyStr = parm["OrderBy"].ToString();
            }
            StringBuilder SelectAimListSqlString = new StringBuilder(@"SELECT  count(*) as num ,[BillTypeFlag] ,[BillTypeCode] ,[BillNo],max(ID) as aimflowid
                                                                                                           into #temp_table     FROM [officedba].[FlowInstance]  
                                                                                                           where BillTypeFlag=1 and  BillTypeCode=1  group by  [BillTypeFlag] ,[BillTypeCode] ,[BillNo]   ");
            SelectAimListSqlString.Append(" SELECT TOP   " + pagesize + "   *  ,  officedba.getEmployeeNameByID(PrincipalID)  AS  PrincipalName,");
            SelectAimListSqlString.Append("   officedba.getEmployeeNameByID(Coordinator) AS CoordinatorName ,    ");
            SelectAimListSqlString.Append("   officedba.getEmployeeNameByID(Creator) AS CreatorName     ");
            SelectAimListSqlString.Append("          FROM  officedba.PlanAim as pa left join  #temp_table  on pa.AimNo = #temp_table.BillNo  left join officedba.FlowInstance ff on  #temp_table.aimflowid = ff.id ");
            SelectAimListSqlString.Append("       WHERE (  pa.ID NOT IN   ");
            SelectAimListSqlString.Append("         (SELECT TOP (" + pagesize + "*" + pageindex + ")  pa1.id   ");
            SelectAimListSqlString.Append("       FROM officedba.PlanAim  as pa1 left join  #temp_table  on pa1.AimNo = #temp_table.BillNo  ");
            SelectAimListSqlString.Append(whereStr.Replace("CompanyCD=", "pa1.CompanyCD="));
            SelectAimListSqlString.Append("        " + orderbyStr.Replace("pa.id DESC", "pa1.id DESC") + " ))    ");
            SelectAimListSqlString.Append(whereStr.Replace("WHERE", " AND  ").Replace("CompanyCD=", "pa.CompanyCD="));
            SelectAimListSqlString.Append(orderbyStr);
            SelectAimListSqlString.Append(";select  @RecordCount=count(*)   from officedba.PlanAim as pa2 left join  #temp_table  on pa2.AimNo = #temp_table.BillNo  left join officedba.FlowInstance ff on  #temp_table.aimflowid = ff.id ");
            SelectAimListSqlString.Append(whereStr.Replace("CompanyCD=", "pa2.CompanyCD="));

            SelectAimListSqlString.Append("  Drop   Table   #temp_table   ");
            #endregion
            //定义更新基本信息的命令 
            SqlCommand comm = new SqlCommand();
            comm.CommandText = SelectAimListSqlString.ToString();
            //设置保存的参数
            SetSearchParm(comm, parm, pageindex, pagesize);

            DataTable dt = new DataTable();
            // 执行查找操作
            dt = SqlHelper.ExecuteSearch(comm);
            try
            {
                RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value.ToString());
            }
            catch
            {
                RecordCount = 0;
            }
            return dt;

        }

        public static DataTable SelectAimInfoById(int id)
        {
            #region  SQL语句拼写
            StringBuilder SelectAimListSqlString = new StringBuilder();
            SelectAimListSqlString.Append(@" SELECT pa.ID, pa.CompanyCD, pa.AimNo, pa.AimTypeID, pa.AimTitle, pa.AimContent, 
       pa.AimStandard, pa.AimStep, pa.Resources, pa.Qustion, pa.Method, pa.AimFlag, 
       pa.JoinUserIDList, pa.JoinUserNameList, pa.PrincipalID, pa.Attachment, pa.Memo, 
       pa.Critical, pa.Important, pa.Priority, pa.AttentionName, pa.[Status], pa.Remark, 
       pa.Creator, pa.CreateDate, pa.Coordinator, pa.SummarizeDate, pa.SummarizeNote, 
       pa.Summarizer, pa.AimRealResult, pa.AddOrCut, pa.[Difference], pa.CompletePercent, 
       pa.CheckScore, pa.ScoreNote, pa.CheckUserID, pa.CheckNote, pa.CheckDate, 
       SUBSTRING(CONVERT(VARCHAR,pa.StartDate,120),0,11) AS StartDate, 
       SUBSTRING(CONVERT(VARCHAR,pa.EndDate,120),0,11) AS EndDate,
       pa.Confirmor, pa.ConfirmDate, pa.Canceler, pa.CancelDate, 
       pa.CancelReson, pa.ModifiedDate, pa.ModifiedUserID, pa.AimNum, pa.AimDate, 
       pa.DeptID, pa.CanViewUser, pa.CanViewUserName, pa.IsMobileNotice,CONVERT(VARCHAR,pa.RemindTime,120) AS RemindTime, 
       pa.IsDirection, pa.Checkor, 
       dbo.getDeptNameByID (DeptID) AS DeptName, 
       officedba.getEmployeeNameByID(PrincipalID) AS PrincipalName, 
       officedba.getEmployeeNameByID(Coordinator) AS CoordinatorName, 
       officedba.getEmployeeNameByID(Checkor) AS CheckorName
       FROM   officedba.PlanAim pa WHERE pa.ID=@ID");

            #endregion
            //定义更新基本信息的命令 
            SqlCommand comm = new SqlCommand();
            comm.CommandText = SelectAimListSqlString.ToString();
            //设置保存的参数
            comm.Parameters.AddWithValue("@ID", SqlDbType.Int);
            comm.Parameters["@ID"].Value = id;
            // 执行查找操作
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        public static void SetAimInfoParm(SqlCommand comm, AimInfoModel model)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AimNo", model.AimNO));//
            comm.Parameters.AddWithValue("@AimTypeID", SqlDbType.Int);
            comm.Parameters["@AimTypeID"].Value = model.AimTypeID;
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AimTitle", model.AimTitle));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AimContent", model.AimContent));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AimStep", model.AimStep));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Resources", model.Resources));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Method", model.Method));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Qustion", model.Qustion));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AimStandard", model.AimStandard));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AimFlag", model.AimFlag));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@JoinUserIDList", model.JoinUserIDList));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@JoinUserNameList", model.JoinUserNameList));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUser", model.CanViewUser  ));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUserName", model.CanViewUserName));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsMobileNotice", model.IsMobileNotice));//
            comm.Parameters.AddWithValue("@PrincipalID", SqlDbType.Int);
            comm.Parameters["@PrincipalID"].Value = model.PrincipalID;
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Attachment", model.Attachment));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Critical", model.Critical));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Important", model.Important));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Priority", model.Priority));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AttentionName", model.AttentionName));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", model.Status));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//

            comm.Parameters.AddWithValue("@Creator", SqlDbType.Int);
            comm.Parameters["@Creator"].Value = model.Creator;

            comm.Parameters.AddWithValue("@Checkor", SqlDbType.Int);
            comm.Parameters["@Checkor"].Value = model.Checkor;

            comm.Parameters.AddWithValue("@CreateDate", SqlDbType.DateTime);
            comm.Parameters["@CreateDate"].Value = model.CreateDate;

            comm.Parameters.AddWithValue("@Coordinator", SqlDbType.Int);
            comm.Parameters["@Coordinator"].Value = model.Coordinator;

            comm.Parameters.AddWithValue("@StartDate", SqlDbType.DateTime);
            comm.Parameters["@StartDate"].Value = model.StartDate;

            comm.Parameters.AddWithValue("@ModifiedDate", SqlDbType.DateTime);
            comm.Parameters["@ModifiedDate"].Value = model.ModifiedDate;

            comm.Parameters.AddWithValue("@EndDate", SqlDbType.DateTime);
            comm.Parameters["@EndDate"].Value = model.EndDate;

            comm.Parameters.AddWithValue("@IsDirection",SqlDbType.VarChar );
            comm.Parameters["@IsDirection"].Value = model.IsDirection;

            if (model.IsMobileNotice == "1")
            {
                comm.Parameters.AddWithValue("@RemindTime", SqlDbType.DateTime);
                comm.Parameters["@RemindTime"].Value = model.RemindTime;
            }
            comm.Parameters.AddWithValue("@AimDate", SqlDbType.VarChar);
            comm.Parameters["@AimDate"].Value = model.AimDate;

            comm.Parameters.AddWithValue("@AimNum", SqlDbType.Int);
            comm.Parameters["@AimNum"].Value = model.AimNum;

            comm.Parameters.AddWithValue("@ID", SqlDbType.Int);
            comm.Parameters["@ID"].Value = model.ID;

            comm.Parameters.AddWithValue("@DeptID", SqlDbType.Int);
            comm.Parameters["@DeptID"].Value = model.DeptID;

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", userInfo.UserID));//
        }
        private static void SetSearchParm(SqlCommand comm, Hashtable hs, int pageindex, int pagesize)
        {
            comm.Parameters.AddWithValue("@RecordCount", SqlDbType.Int);
            comm.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
            
            comm.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar );
            comm.Parameters["@CompanyCD"].Value =hs["CompanyCD"].ToString(); 

            if (hs.ContainsKey("AimTypeID"))
            {
                comm.Parameters.AddWithValue("@AimTypeID", SqlDbType.Int);
                try { comm.Parameters["@AimTypeID"].Value = Convert.ToInt32(hs["AimTypeID"]); }
                catch { comm.Parameters["@AimTypeID"].Value = DBNull.Value; }
            }
            if (hs.ContainsKey("AimFlag"))
            {
                comm.Parameters.AddWithValue("@AimFlag", SqlDbType.VarChar);
                try { comm.Parameters["@AimFlag"].Value = hs["AimFlag"].ToString(); }
                catch { comm.Parameters["@AimFlag"].Value = DBNull.Value; }
            }

            if (hs.ContainsKey("DeptID"))
            {
                comm.Parameters.AddWithValue("@DeptID", SqlDbType.Int);
                try { comm.Parameters["@DeptID"].Value = Convert.ToInt32(hs["DeptID"]); }
                catch { comm.Parameters["@DeptID"].Value = DBNull.Value; }
            }

            if (hs.ContainsKey("SearchID"))
            {
                    comm.Parameters.AddWithValue("@SearchID", SqlDbType.Int);
                    try { comm.Parameters["@SearchID"].Value = Convert.ToInt32(hs["SearchID"]); }
                    catch { comm.Parameters["@SearchID"].Value = DBNull.Value; }        
            }

            if (hs.ContainsKey("PrincipalID"))
            {
                comm.Parameters.AddWithValue("@PrincipalID", SqlDbType.Int);
                try { comm.Parameters["@PrincipalID"].Value = Convert.ToInt32(hs["PrincipalID"].ToString()); }
                catch { comm.Parameters["@PrincipalID"].Value = DBNull.Value; }
            }
            if (hs.ContainsKey("Coordinator"))
            {
                comm.Parameters.AddWithValue("@Coordinator", SqlDbType.Int);
                try { comm.Parameters["@Coordinator"].Value = Convert.ToInt32(hs["Coordinator"].ToString()); }
                catch { comm.Parameters["@Coordinator"].Value = DBNull.Value; }
            }
            if (hs.ContainsKey("AimTitle"))
            {
                comm.Parameters.AddWithValue("@AimTitle", SqlDbType.VarChar);
                try { comm.Parameters["@AimTitle"].Value = "%" + hs["AimTitle"].ToString() + "%"; }
                catch { comm.Parameters["@AimTitle"].Value = DBNull.Value; }
            }
            if (hs.ContainsKey("StartDate"))
            {
                comm.Parameters.AddWithValue("@StartDate", SqlDbType.DateTime);
                try { comm.Parameters["@StartDate"].Value = Convert.ToDateTime(hs["StartDate"]); }
                catch { comm.Parameters["@StartDate"].Value = DBNull.Value; }
            }
            if (hs.ContainsKey("EndDate"))
            {
                comm.Parameters.AddWithValue("@EndDate", SqlDbType.DateTime);
                try { comm.Parameters["@EndDate"].Value = Convert.ToDateTime(hs["EndDate"]); }
                catch { comm.Parameters["@EndDate"].Value = DBNull.Value; }
            }
            if (hs.ContainsKey("AimDate"))
            {
                comm.Parameters.AddWithValue("@AimDate", SqlDbType.VarChar);
                try {
                        comm.Parameters["@AimDate"].Value = hs["AimDate"].ToString();
                }catch { comm.Parameters["@AimDate"].Value =   DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")    ; }
            }
            if (hs.ContainsKey("AimNum"))
            {
                comm.Parameters.AddWithValue("@AimNum", SqlDbType.Int);
                try { comm.Parameters["@AimNum"].Value = Convert.ToInt32(hs["AimNum"]); }
                catch { comm.Parameters["@AimNum"].Value = DBNull.Value; }
            }
            if (hs.ContainsKey("AimNo"))
            {
                comm.Parameters.AddWithValue("@AimNo", SqlDbType.VarChar);
                try { comm.Parameters["@AimNo"].Value = "%" + hs["AimNo"].ToString() + "%"; }
                catch { comm.Parameters["@AimNo"].Value = DBNull.Value; }
            }

            if (hs.ContainsKey("Status"))
            {
                comm.Parameters.AddWithValue("@Status", SqlDbType.Int);
                try { comm.Parameters["@Status"].Value = Convert.ToInt32(hs["Status"]); }
                catch { comm.Parameters["@Status"].Value = DBNull.Value; }
            }

            if (hs.ContainsKey("FlowStatus"))
            {
                comm.Parameters.AddWithValue("@FlowStatus", SqlDbType.VarChar);
                try { comm.Parameters["@FlowStatus"].Value = hs["FlowStatus"].ToString() ; }
                catch { comm.Parameters["@FlowStatus"].Value = DBNull.Value; }
            }
            
            if (hs.ContainsKey("AimName"))
            {
                comm.Parameters.AddWithValue("@AimName", SqlDbType.VarChar);
                try { comm.Parameters["@AimName"].Value = hs["AimName"]; }
                catch { comm.Parameters["@AimName"].Value = DBNull.Value; }
            }

            if (hs.ContainsKey("Creator"))
            {
                comm.Parameters.AddWithValue("@Creator", SqlDbType.Int);
                try { comm.Parameters["@Creator"].Value =  Convert.ToInt32( hs["Creator"].ToString() ); }
                catch { comm.Parameters["@Creator"].Value = DBNull.Value; }
            }


        }
        private static string SetSearchWhereString(Hashtable hs,string eid)
        {
            int paramCount = 0;
            StringBuilder whereStr = new StringBuilder();
            if (hs.Keys.Count == 1 && hs.ContainsKey("OrderBy"))
                return "";

            if (hs.Keys.Count > 0)
            {
                whereStr.Append("  WHERE ");
                 
                if( hs["FormPage"].ToString() =="New"  ){
                     whereStr.Append(" (   charindex('," + eid + ",' , ','+JoinUserIDList+',')>0  OR PrincipalID= " + eid + " OR  Creator=" + eid + "      or Coordinator=" + eid + "     )");
                }else{
                     whereStr.Append(" ( charindex('," + eid + ",' , ','+CanViewUser+',')>0 OR  charindex('," + eid + ",' , ','+JoinUserIDList+',')>0  OR PrincipalID= " + eid + " OR  Creator=" + eid + " OR CanViewUser='' OR CanViewUser is null   or Coordinator=" + eid + "    or  Checkor=" + eid + "  )");
                }
                paramCount++;

                whereStr.Append("  AND  CompanyCD=@CompanyCD ");

                if (hs.ContainsKey("AimTypeID"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimTypeID=@AimTypeID ");
                    paramCount++;
                }
                if (hs.ContainsKey("AimFlag"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimFlag=@AimFlag ");
                    paramCount++;
                }

                if (hs.ContainsKey("Creator"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  Creator=@Creator ");
                    paramCount++;
                }

                if (hs.ContainsKey("SearchID"))
                {
                    if (hs["SearchType"].ToString() == "1")
                    {
                        if (paramCount > 0) whereStr.Append("  AND  ");
                        whereStr.Append("  DeptID  = @SearchID ");
                        paramCount++;
                    }
                    else {
                        if (paramCount > 0) whereStr.Append("  AND  ");
                        whereStr.Append("  PrincipalID=@SearchID ");
                        paramCount++;
                    }
                }

                if (hs.ContainsKey("PrincipalID"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  PrincipalID=@PrincipalID ");
                    paramCount++;
                }
                if (hs.ContainsKey("Coordinator"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  Coordinator=@Coordinator ");
                    paramCount++;
                }
                if (hs.ContainsKey("AimTitle"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimTitle LIKE  @AimTitle ");
                    paramCount++;
                }
                if (hs.ContainsKey("StartDate"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  ( AimDate  >=@StartDate  or  AimDate  ='1900-12-31 0:00:00'  ) ");
                    paramCount++;
                }
                if (hs.ContainsKey("EndDate"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("(  AimDate  <= @EndDate  or   AimDate ='3000-12-31 0:00:00' ) ");
                    paramCount++;
                }
                if (hs.ContainsKey("AimDate"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimDate = @AimDate ");
                    paramCount++;
                }
                if (hs.ContainsKey("AimNum"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimNum  = @AimNum ");
                    paramCount++;
                }
                if (hs.ContainsKey("AimNo"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimNo  like @AimNo ");
                    paramCount++;
                }
                if (hs.ContainsKey("Status"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  Status  = @Status ");
                    paramCount++;
                }

                if (hs.ContainsKey("DeptID"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  DeptID  = @DeptID ");
                    paramCount++;
                }

                if (hs.ContainsKey("FlowStatus"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");

                    if (hs["FlowStatus"].ToString() == "NULL")
                    {
                        whereStr.Append("  FlowStatus  is  NULL  ");
                    }
                    else {
                        whereStr.Append("  FlowStatus  = @FlowStatus ");
                    }
                    paramCount++;
                }

                if (hs.ContainsKey("AimName"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimName  LIKE   @AimName  ");
                    paramCount++;
                }

            }
            return whereStr.ToString();
        }

        private static string SetSearchWhereString1(Hashtable hs, string eid)
        {
            int paramCount = 0;
            StringBuilder whereStr = new StringBuilder();
            if (hs.Keys.Count == 1 && hs.ContainsKey("OrderBy"))
                return "";

            if (hs.Keys.Count > 0)
            {
                whereStr.Append("  WHERE ");

                if (hs["FormPage"].ToString() == "New")
                {
                    whereStr.Append(" (   charindex('," + eid + ",' , ','+JoinUserIDList+',')>0  OR PrincipalID= " + eid + " OR  Creator=" + eid + "      or Coordinator=" + eid + "     )");
                }
                else
                {
                    whereStr.Append(" ( charindex('," + eid + ",' , ','+CanViewUser+',')>0 OR  charindex('," + eid + ",' , ','+JoinUserIDList+',')>0  OR PrincipalID= " + eid + " OR  Creator=" + eid + " OR CanViewUser='' OR CanViewUser is null   or Coordinator=" + eid + "    or  Checkor=" + eid + "  )");
                }
                paramCount++;

                whereStr.Append("  AND  CompanyCD=@CompanyCD ");

                if (hs.ContainsKey("AimTypeID"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimTypeID=@AimTypeID ");
                    paramCount++;
                }
                if (hs.ContainsKey("AimFlag"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimFlag=@AimFlag ");
                    paramCount++;
                    if (hs["AimFlag"].ToString()=="1")
                    {
                        if (hs.ContainsKey("StartDate"))
                        {
                            if (paramCount > 0) whereStr.Append("  AND  ");
                            whereStr.Append("  ( AimDate >=@StartDate ) ");
                            paramCount++;
                        }
                        if (hs.ContainsKey("EndDate"))
                        {
                            if (paramCount > 0) whereStr.Append("  AND  ");
                            whereStr.Append("(  AimDate <= @EndDate ) ");
                            paramCount++;
                        }
                    }
                    else
                    {
                        if (hs.ContainsKey("StartDate"))
                        {
                            if (paramCount > 0) whereStr.Append("  AND  ");
                            whereStr.Append("  ( AimDate+'-'+cast(AimNum,as varchar(20)) >=@StartDate ) ");
                            paramCount++;
                        }
                        if (hs.ContainsKey("EndDate"))
                        {
                            if (paramCount > 0) whereStr.Append("  AND  ");
                            whereStr.Append("(  AimDate+'-'+cast(AimNum,as varchar(20)) <= @EndDate ) ");
                            paramCount++;
                        }
                    }
                   

                }

                if (hs.ContainsKey("Creator"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  Creator=@Creator ");
                    paramCount++;
                }

                if (hs.ContainsKey("SearchID"))
                {
                    if (hs["SearchType"].ToString() == "1")
                    {
                        if (paramCount > 0) whereStr.Append("  AND  ");
                        whereStr.Append("  DeptID  = @SearchID ");
                        paramCount++;
                    }
                    else
                    {
                        if (paramCount > 0) whereStr.Append("  AND  ");
                        whereStr.Append("  PrincipalID=@SearchID ");
                        paramCount++;
                    }
                }

                if (hs.ContainsKey("PrincipalID"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  PrincipalID=@PrincipalID ");
                    paramCount++;
                }
                if (hs.ContainsKey("Coordinator"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  Coordinator=@Coordinator ");
                    paramCount++;
                }
                if (hs.ContainsKey("AimTitle"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimTitle LIKE  @AimTitle ");
                    paramCount++;
                }
             
                if (hs.ContainsKey("AimDate"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimDate = @AimDate ");
                    paramCount++;
                }
                if (hs.ContainsKey("AimNum"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimNum  = @AimNum ");
                    paramCount++;
                }
                if (hs.ContainsKey("AimNo"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimNo  like @AimNo ");
                    paramCount++;
                }
                if (hs.ContainsKey("Status"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  Status  = @Status ");
                    paramCount++;
                }

                if (hs.ContainsKey("DeptID"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  DeptID  = @DeptID ");
                    paramCount++;
                }

                if (hs.ContainsKey("FlowStatus"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");

                    if (hs["FlowStatus"].ToString() == "NULL")
                    {
                        whereStr.Append("  FlowStatus  is  NULL  ");
                    }
                    else
                    {
                        whereStr.Append("  FlowStatus  = @FlowStatus ");
                    }
                    paramCount++;
                }

                if (hs.ContainsKey("AimName"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimName  LIKE   @AimName  ");
                    paramCount++;
                }

            }
            return whereStr.ToString();
        }
        public static void SetUpdateAimInfoParm(SqlCommand comm, AimInfoModel model)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AimTitle", model.AimTitle));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AimContent", model.AimContent));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AimStandard", model.AimStandard));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUser", model.CanViewUser));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUserName", model.CanViewUserName));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AttentionName", model.AttentionName));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsMobileNotice", model.IsMobileNotice));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Qustion", model.Qustion));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Resources", model.Resources));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Method", model.Method));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AimStep", model.AimStep));//

            comm.Parameters.AddWithValue("@PrincipalID", SqlDbType.Int);
            comm.Parameters["@PrincipalID"].Value = model.PrincipalID;

            comm.Parameters.AddWithValue("@Checkor", SqlDbType.Int);
            comm.Parameters["@Checkor"].Value = model.Checkor;

            comm.Parameters.AddWithValue("@Coordinator", SqlDbType.Int);
            comm.Parameters["@Coordinator"].Value = model.Coordinator;

            comm.Parameters.AddWithValue("@JoinUserNameList", SqlDbType.VarChar);
            comm.Parameters["@JoinUserNameList"].Value = model.JoinUserNameList;

            comm.Parameters.AddWithValue("@JoinUserIDList", SqlDbType.VarChar);
            comm.Parameters["@JoinUserIDList"].Value = model.JoinUserIDList;

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Attachment", model.Attachment));//

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", model.Status));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//

            comm.Parameters.AddWithValue("@StartDate", SqlDbType.DateTime);
            comm.Parameters["@StartDate"].Value = model.StartDate;

            comm.Parameters.AddWithValue("@ModifiedDate", SqlDbType.DateTime);
            comm.Parameters["@ModifiedDate"].Value = model.ModifiedDate;

            comm.Parameters.AddWithValue("@EndDate", SqlDbType.DateTime);
            comm.Parameters["@EndDate"].Value = model.EndDate;

            if (model.IsMobileNotice == "1")
            {
                comm.Parameters.AddWithValue("@RemindTime", SqlDbType.DateTime);
                comm.Parameters["@RemindTime"].Value = model.RemindTime;
            }
            comm.Parameters.AddWithValue("@ID", SqlDbType.Int);
            comm.Parameters["@ID"].Value = model.ID;

            comm.Parameters.AddWithValue("@IsDirection", SqlDbType.VarChar);
            comm.Parameters["@IsDirection"].Value = model.IsDirection;

            comm.Parameters.AddWithValue("@DeptID", SqlDbType.Int);
            comm.Parameters["@DeptID"].Value = model.DeptID;

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", userInfo.UserID));//
        }
        private static string SetDeleteWhereString(string[] idarray)
        {
            int paramCount = 0;
            StringBuilder whereStr = new StringBuilder();
            whereStr.Append("  (  ");

            foreach (string s in idarray)
            {
                if (paramCount > 0)
                    whereStr.Append("   OR   id=" + s.ToString());
                else
                    whereStr.Append("    id=" + s.ToString());
                paramCount++;
            }

            whereStr.Append("  )  ");

            return whereStr.ToString();
        }

        private static string SetSearchReportWhereString(Hashtable hs)
        {
            StringBuilder whereStr = new StringBuilder();

            if (hs.Keys.Count > 0)
            {
                whereStr.Append(" WHERE  ( charindex('," + hs["EID"] + ",' , ','+CanViewUser+',')>0 OR  charindex('," + hs["EID"] + ",' , ','+JoinUserIDList+',')>0  OR PrincipalID= " + hs["EID"] + " OR  Creator=" + hs["EID"] + " OR CanViewUser='' OR CanViewUser is null   or Coordinator=" + hs["EID"] + "    or  Checkor=" + hs["EID"] + "  )");

                if (hs.ContainsKey("TimeArea"))
                {
                    whereStr.Append("  " + hs["TimeArea"] + "  ");
                }

                if (hs.ContainsKey("DeptID"))
                {
                    whereStr.Append("  and  DeptID =" + hs["DeptID"] + "  ");
                }

                if (hs.ContainsKey("PrincipalID"))
                {
                    whereStr.Append("  and  PrincipalID =" + hs["PrincipalID"] + "  ");
                }
            }
            return whereStr.ToString();
        }

    }



}

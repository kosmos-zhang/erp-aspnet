/**********************************************
 * 类作用：   添加考勤报表数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/23
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;

namespace XBase.Data.Office.AdminManager
{
    /// <summary>
    /// 类名：AttendanceReportDBHelper
    /// 描述：添加考勤报表数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/04/23
    /// </summary>
   public class AttendanceReportDBHelper
   {
       #region 添加报表信息
       /// <summary>
        /// 添加报表信息
        /// </summary>
        /// <param name="AttendanceReportM">报表信息</param>
        /// <param name="reportdatas">报表详细</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddAttendanceReportInfo(AttendanceReportModel AttendanceReportM, string reportdatas)
        {
            try
            {
                StringBuilder AttendanceReportSql = new StringBuilder();
                AttendanceReportSql.AppendLine("INSERT INTO officedba.AttendanceReport");
                AttendanceReportSql.AppendLine("		(ReprotNo      ");
                AttendanceReportSql.AppendLine("		,CompanyCD         ");
                AttendanceReportSql.AppendLine("		,ReportName       ");
                AttendanceReportSql.AppendLine("		,Month       ");
                AttendanceReportSql.AppendLine("		,StartDate       ");
                AttendanceReportSql.AppendLine("		,EndDate       ");
                AttendanceReportSql.AppendLine("		,CreateUserID      ");
                AttendanceReportSql.AppendLine("		,CreateDate   ");
                AttendanceReportSql.AppendLine("		,Status");
                AttendanceReportSql.AppendLine("		,ModifiedDate        ");
                AttendanceReportSql.AppendLine("		,ModifiedUserID)        ");
                AttendanceReportSql.AppendLine("VALUES                  ");
                AttendanceReportSql.AppendLine("		('" + AttendanceReportM.ReprotNo + "'     ");
                AttendanceReportSql.AppendLine("		,'" + AttendanceReportM.CompanyCD + "'       ");
                AttendanceReportSql.AppendLine("		,'" + AttendanceReportM.ReportName + "'      ");
                AttendanceReportSql.AppendLine("		,'" + AttendanceReportM.Month + "'      ");
                AttendanceReportSql.AppendLine("		,'" + AttendanceReportM.StartDate + "'      ");
                AttendanceReportSql.AppendLine("		,'" + AttendanceReportM.EndDate + "'      ");
                AttendanceReportSql.AppendLine("		,'" + AttendanceReportM.CreateUserID + "'     ");
                AttendanceReportSql.AppendLine("		,'" + AttendanceReportM.CreateDate + "'  ");
                AttendanceReportSql.AppendLine("		,'" + AttendanceReportM.Status + "'");
                AttendanceReportSql.AppendLine("		,'" + AttendanceReportM.ModifiedDate + "'       ");
                AttendanceReportSql.AppendLine("		,'" + AttendanceReportM.ModifiedUserID + "')       ");
                return InsertAll(AttendanceReportSql.ToString(), reportdatas, AttendanceReportM.CompanyCD, AttendanceReportM.ModifiedUserID);
            }
            catch
            {
                return false;
            }
        }

       /// <summary>
       /// 添加报表信息
       /// </summary>
       /// <param name="AttendanceReportSql">主表SQL</param>
       /// <param name="reportdatas">报表详细数据</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InsertAll(string AttendanceReportSql, string reportdatas, string CompanyCD, string UserID)
        {
            AttendanceReportDetailModel AttendanceReportDetailM = new AttendanceReportDetailModel();
            string[] strarray = null;
            string recorditems = "";
            string[] inseritems = null;
            try
            {
                strarray = reportdatas.Split('|');
                string[] sqlarray = new string[strarray.Length+1];
                sqlarray[0] = AttendanceReportSql;
                for (int i = 0; i < strarray.Length; i++)
                {
                    StringBuilder ReportDetailSql = new StringBuilder();
                    recorditems = strarray[i];
                    inseritems = recorditems.Split(',');
                    if (recorditems.Length != 0)
                    {
                        string ReportNo = inseritems[0].ToString();//报表编号
                        string EmployeeID = inseritems[1].ToString();//员工ID
                        string Hours = inseritems[2].ToString();//应出勤时长
                        string WorkHour=inseritems[3].ToString();//实际出勤时长
                        string Leave=inseritems[4].ToString();//请假时长
                        string Overtime=inseritems[5].ToString();//加班时长
                        string Out=inseritems[6].ToString();//外出时长
                        string Business=inseritems[7].ToString();//出差时长
                        string Instead=inseritems[8].ToString();//替班时长
                        string Transferred=inseritems[9].ToString();//年休时长
                        string LateMinute=inseritems[10].ToString();//迟到时长
                        string LeaveEarlyMinute=inseritems[11].ToString();//早退时长
                        string AttendanceRate = inseritems[12].ToString();//出勤率
                        string AttendanceType = inseritems[13].ToString();//考勤类型
                        string NomalHour = inseritems[14].ToString();//正常考勤时间

                        AttendanceReportDetailM.ReprotNo = ReportNo;
                        AttendanceReportDetailM.CompanyCD = CompanyCD;
                        AttendanceReportDetailM.EmployeeID =Convert.ToInt32(EmployeeID);
                        //AttendanceReportDetailM.StartDate =Convert.ToDateTime(StartDate);
                        //AttendanceReportDetailM.EndDate = EndDate;
                        AttendanceReportDetailM.Hours = Convert.ToDecimal(Hours);
                        AttendanceReportDetailM.WorkHour = Convert.ToDecimal(WorkHour);
                        AttendanceReportDetailM.Leave =Convert.ToDecimal(Leave);
                        AttendanceReportDetailM.Overtime = Convert.ToDecimal(Overtime);
                        AttendanceReportDetailM.Out = Convert.ToDecimal(Out);
                        AttendanceReportDetailM.Business = Convert.ToDecimal(Business);
                        AttendanceReportDetailM.Instead = Convert.ToDecimal(Instead);
                        AttendanceReportDetailM.Transferred = Convert.ToDecimal(Transferred);
                        AttendanceReportDetailM.LateMinute = Convert.ToDecimal(LateMinute);
                        AttendanceReportDetailM.LeaveEarlyMinute = Convert.ToDecimal(LeaveEarlyMinute);
                        AttendanceReportDetailM.ModifiedDate = System.DateTime.Now;
                        AttendanceReportDetailM.ModifiedUserID = UserID;

                        ReportDetailSql.AppendLine("INSERT INTO officedba.AttendanceReportMonth");
                        ReportDetailSql.AppendLine("		(ReprotNo      ");
                        ReportDetailSql.AppendLine("		,CompanyCD        ");
                        ReportDetailSql.AppendLine("		,EmployeeID        ");
                        ReportDetailSql.AppendLine("		,NomalHour        ");
                        ReportDetailSql.AppendLine("		,AttendanceType        ");
                        ReportDetailSql.AppendLine("		,Hours        ");
                        ReportDetailSql.AppendLine("		,WorkHour        ");
                        ReportDetailSql.AppendLine("		,Leave        ");
                        ReportDetailSql.AppendLine("		,Overtime        ");
                        ReportDetailSql.AppendLine("		,Out        ");
                        ReportDetailSql.AppendLine("		,Business        ");
                        ReportDetailSql.AppendLine("		,[Instead]        ");
                        ReportDetailSql.AppendLine("		,Transferred        ");
                        ReportDetailSql.AppendLine("		,LateMinute        ");
                        ReportDetailSql.AppendLine("		,LeaveEarlyMinute        ");
                        ReportDetailSql.AppendLine("		,AttendanceRate        ");
                        ReportDetailSql.AppendLine("		,ModifiedDate        ");
                        ReportDetailSql.AppendLine("		,ModifiedUserID)        ");
                        ReportDetailSql.AppendLine("VALUES                  ");
                        ReportDetailSql.AppendLine("		('" + AttendanceReportDetailM.ReprotNo + "'     ");
                        ReportDetailSql.AppendLine("		,'" + AttendanceReportDetailM.CompanyCD + "'       ");
                        ReportDetailSql.AppendLine("		,'" + AttendanceReportDetailM.EmployeeID + "'       ");
                        ReportDetailSql.AppendLine("		," + NomalHour + "       ");
                        ReportDetailSql.AppendLine("		,'" + AttendanceType + "'       ");
                        ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.Hours + "       ");
                        ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.WorkHour + "       ");
                        ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.Leave + "       ");
                        ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.Overtime + "       ");
                        ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.Out + "       ");
                        ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.Business + "       ");
                        ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.Instead + "       ");
                        ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.Transferred + "       ");
                        ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.LateMinute + "       ");
                        ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.LeaveEarlyMinute + "       ");
                        ReportDetailSql.AppendLine("		," +Convert.ToDecimal(AttendanceRate) + "       ");
                        ReportDetailSql.AppendLine("		,'" + AttendanceReportDetailM.ModifiedDate + "'       ");
                        ReportDetailSql.AppendLine("		,'" + AttendanceReportDetailM.ModifiedUserID + "')       ");
                        sqlarray[i+1] = ReportDetailSql.ToString();
                    }
                }
                SqlHelper.ExecuteTransForListWithSQL(sqlarray);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

       #region 修改报表信息
       /// <summary>
       /// 修改报表信息
       /// </summary>
       /// <param name="AttendanceReportM">报表信息</param>
       /// <param name="reportdatas">报表详细</param>
       /// <returns>修改是否成功 false:失败，true:成功</returns>
       public static bool UpdateAttendanceReportInfo(AttendanceReportModel AttendanceReportM, string reportdatas)
       {
           try
           {
               StringBuilder AttendanceReportSql = new StringBuilder();
               AttendanceReportSql.AppendLine("UPDATE officedba.AttendanceReport");
               AttendanceReportSql.AppendLine("		SET ReportName='" + AttendanceReportM.ReportName + "'       ");
               AttendanceReportSql.AppendLine("		,StartDate='" + AttendanceReportM.StartDate + "'       ");
               AttendanceReportSql.AppendLine("		,EndDate='" + AttendanceReportM.EndDate + "'       ");
               AttendanceReportSql.AppendLine("		,CreateUserID='" + AttendanceReportM.CreateUserID + "'      ");
               AttendanceReportSql.AppendLine("		,CreateDate='" + AttendanceReportM.CreateDate + "'   ");
               AttendanceReportSql.AppendLine("		,Status='" + AttendanceReportM.Status + "'");
               AttendanceReportSql.AppendLine("		,ModifiedDate='" + AttendanceReportM.ModifiedDate + "'        ");
               AttendanceReportSql.AppendLine("		,ModifiedUserID='" + AttendanceReportM.ModifiedUserID + "'        ");
               AttendanceReportSql.AppendLine("		WHERE  ReprotNo='" + AttendanceReportM .ReprotNo+ "'       ");
               AttendanceReportSql.AppendLine("		AND  CompanyCD='" + AttendanceReportM.CompanyCD + "'       ");
               AttendanceReportSql.AppendLine("		AND  Month='" + AttendanceReportM.Month + "'       ");
               return UpdateAll(AttendanceReportSql.ToString(), reportdatas, AttendanceReportM.CompanyCD, AttendanceReportM.ReprotNo, AttendanceReportM.ModifiedUserID);
           }
           catch 
           {
               return false;
           }
       }

       /// <summary>
       /// 修改报表信息
       /// </summary>
       /// <param name="AttendanceReportSql">主表SQL</param>
       /// <param name="reportdatas">报表详细数据</param>
       /// <returns>修改是否成功 false:失败，true:成功</returns>
       public static bool UpdateAll(string AttendanceReportSql, string reportdatas, string CompanyCD, string ReprotNo, string UserID)
       {
           AttendanceReportDetailModel AttendanceReportDetailM = new AttendanceReportDetailModel();
           string[] strarray = null;
           string recorditems = "";
           string[] inseritems = null;
           try
           {
               strarray = reportdatas.Split('|');
               string[] sqlarray = new string[strarray.Length + 2];
               sqlarray[0] = AttendanceReportSql;
               sqlarray[1] = "DELETE FROM officedba.AttendanceReportMonth WHERE CompanyCD='" + CompanyCD + "' AND ReprotNo='" + ReprotNo + "'";
               for (int i = 0; i < strarray.Length; i++)
               {
                   StringBuilder ReportDetailSql = new StringBuilder();
                   recorditems = strarray[i];
                   inseritems = recorditems.Split(',');
                   if (recorditems.Length != 0)
                   {
                       string ReportNo = inseritems[0].ToString();//报表编号
                       string EmployeeID = inseritems[1].ToString();//员工ID
                       string Hours = inseritems[2].ToString();//应出勤时长
                       string WorkHour = inseritems[3].ToString();//实际出勤时长
                       string Leave = inseritems[4].ToString();//请假时长
                       string Overtime = inseritems[5].ToString();//加班时长
                       string Out = inseritems[6].ToString();//外出时长
                       string Business = inseritems[7].ToString();//出差时长
                       string Instead = inseritems[8].ToString();//替班时长
                       string Transferred = inseritems[9].ToString();//年休时长
                       string LateMinute = inseritems[10].ToString();//迟到时长
                       string LeaveEarlyMinute = inseritems[11].ToString();//早退时长
                       string AttendanceRate = inseritems[12].ToString();//出勤率
                       string AttendanceType = inseritems[13].ToString();//考勤类型
                       string NomalHour = inseritems[14].ToString();//正常考勤时间


                       AttendanceReportDetailM.ReprotNo = ReportNo;
                       AttendanceReportDetailM.CompanyCD = CompanyCD;
                       AttendanceReportDetailM.EmployeeID = Convert.ToInt32(EmployeeID);
                      // AttendanceReportDetailM.StartDate = Convert.ToDateTime(StartDate);
                      // AttendanceReportDetailM.EndDate = EndDate;
                       AttendanceReportDetailM.Hours = Convert.ToDecimal(Hours);
                       AttendanceReportDetailM.WorkHour = Convert.ToDecimal(WorkHour);
                       AttendanceReportDetailM.Leave = Convert.ToDecimal(Leave);
                       AttendanceReportDetailM.Overtime = Convert.ToDecimal(Overtime);
                       AttendanceReportDetailM.Out = Convert.ToDecimal(Out);
                       AttendanceReportDetailM.Business = Convert.ToDecimal(Business);
                       AttendanceReportDetailM.Instead = Convert.ToDecimal(Instead);
                       AttendanceReportDetailM.Transferred = Convert.ToDecimal(Transferred);
                       AttendanceReportDetailM.LateMinute = Convert.ToDecimal(LateMinute);
                       AttendanceReportDetailM.LeaveEarlyMinute = Convert.ToDecimal(LeaveEarlyMinute);
                       AttendanceReportDetailM.ModifiedDate = System.DateTime.Now;
                       AttendanceReportDetailM.ModifiedUserID = UserID;

                       ReportDetailSql.AppendLine("INSERT INTO officedba.AttendanceReportMonth");
                       ReportDetailSql.AppendLine("		(ReprotNo      ");
                       ReportDetailSql.AppendLine("		,CompanyCD        ");
                       ReportDetailSql.AppendLine("		,EmployeeID        ");
                       ReportDetailSql.AppendLine("		,NomalHour        ");
                       ReportDetailSql.AppendLine("		,AttendanceType        ");
                       ReportDetailSql.AppendLine("		,Hours        ");
                       ReportDetailSql.AppendLine("		,WorkHour        ");
                       ReportDetailSql.AppendLine("		,Leave        ");
                       ReportDetailSql.AppendLine("		,Overtime        ");
                       ReportDetailSql.AppendLine("		,Out        ");
                       ReportDetailSql.AppendLine("		,Business        ");
                       ReportDetailSql.AppendLine("		,[Instead]        ");
                       ReportDetailSql.AppendLine("		,Transferred        ");
                       ReportDetailSql.AppendLine("		,LateMinute        ");
                       ReportDetailSql.AppendLine("		,LeaveEarlyMinute        ");
                       ReportDetailSql.AppendLine("		,AttendanceRate        ");
                       ReportDetailSql.AppendLine("		,ModifiedDate        ");
                       ReportDetailSql.AppendLine("		,ModifiedUserID)        ");
                       ReportDetailSql.AppendLine("VALUES                  ");
                       ReportDetailSql.AppendLine("		('" + AttendanceReportDetailM.ReprotNo + "'     ");
                       ReportDetailSql.AppendLine("		,'" + AttendanceReportDetailM.CompanyCD + "'       ");
                       ReportDetailSql.AppendLine("		,'" + AttendanceReportDetailM.EmployeeID + "'       ");
                       ReportDetailSql.AppendLine("		," + NomalHour + "       ");
                       ReportDetailSql.AppendLine("		,'" + AttendanceType + "'       ");
                       ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.Hours + "       ");
                       ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.WorkHour + "       ");
                       ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.Leave + "       ");
                       ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.Overtime + "       ");
                       ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.Out + "       ");
                       ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.Business + "       ");
                       ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.Instead + "       ");
                       ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.Transferred + "       ");
                       ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.LateMinute + "       ");
                       ReportDetailSql.AppendLine("		," + AttendanceReportDetailM.LeaveEarlyMinute + "       ");
                       ReportDetailSql.AppendLine("		," + Convert.ToDecimal(AttendanceRate) + "       ");
                       ReportDetailSql.AppendLine("		,'" + AttendanceReportDetailM.ModifiedDate + "'       ");
                       ReportDetailSql.AppendLine("		,'" + AttendanceReportDetailM.ModifiedUserID + "')       ");
                       sqlarray[i + 2] = ReportDetailSql.ToString();
                   }
               }
               SqlHelper.ExecuteTransForListWithSQL(sqlarray);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion
       #region 根据公司ID和所属月份判断此公司某月报表是否已经存在
       /// <summary>
       /// 根据公司ID和所属月份判断此公司某月报表是否已经存在
       /// </summary>
       /// <param name="CompanyID">公司ID</param>
       /// <returns></returns>
       public static int AttendanceReportInfoIsExist(string Month, string CompanyCD)
       {
           string sql = "SELECT COUNT(*) AS IndexCount FROM officedba.AttendanceReport WHERE Month ='" + Month + "' AND CompanyCD='" + CompanyCD + "'";
           DataTable IndexCount = SqlHelper.ExecuteSql(sql);
           if (IndexCount != null && (int)IndexCount.Rows[0][0] > 0)
           {
               return (int)IndexCount.Rows[0][0];
           }
           else
           {
               return 0;
           }
       }
       #endregion
       #region 报表编号是否已经存在
       /// <summary>
       /// 根据公司ID报表编号是否已经存在
       /// </summary>
       /// <param name="CompanyID">公司ID</param>
       /// <returns></returns>
       public static int AttendanceReportNoIsExist(string ReportNo, string CompanyCD)
       {
           string sql = "SELECT COUNT(*) AS IndexCount FROM officedba.AttendanceReport WHERE ReprotNo ='" + ReportNo + "' AND CompanyCD='" + CompanyCD + "'";
           DataTable IndexCount = SqlHelper.ExecuteSql(sql);
           if (IndexCount != null && (int)IndexCount.Rows[0][0] > 0)
           {
               return (int)IndexCount.Rows[0][0];
           }
           else
           {
               return 0;
           }
       }
       #endregion

       #region 查询考勤报表
       /// <summary>
       /// 查询考勤报表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable SearchReportData(string CompanyID, string ReportNo, string ReportName, string BelongMonth,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           string sql = "select a.ID,a.ReprotNo,a.ReportName,a.[Month],convert(varchar(10),"
                            +"a.StartDate,120)StartDate,convert(varchar(10),a.EndDate,120)EndDate,"
                            + "convert(varchar(10),a.CreateDate,120)CreateDate,a.CreateUserID,case a.Status when 0 then '草稿' when '1' then '已确认' end Status,"
                            +"b.EmployeeName "
                            +"from officedba.AttendanceReport a "
                            +"left outer join  "
                            +"officedba.EmployeeInfo b "
                            + "on a.CreateUserID=b.ID where a.CompanyCD='" + CompanyID + "'";
           if (ReportNo != "")
               sql += " and a.ReprotNo like '%" + ReportNo + "%'";
           if (ReportName != "")
               sql += "  and a.ReportName LIKE '%" + ReportName + "%'";
           if (BelongMonth != "")
               sql += " and a.[Month] like '%" + BelongMonth + "%'";
           //return SqlHelper.ExecuteSql(sql);
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

       }
       #endregion

       #region 保存报表调整信息
       /// <summary>
       /// 保存报表调整信息
       /// </summary>
       /// <param name="AttendanceReportDetailM">报表调整信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool SaveAttendanceChangeInfo(AttendanceReportDetailModel AttendanceReportDetailM, string AttendanceType)
       {
           try
           {
               #region 报表调整信息SQL拼写
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.AttendanceReportMonth SET ");
               sql.AppendLine("		 ChangeTimes=@ChangeTimes        ");
               sql.AppendLine("		,ChangeType=@ChangeType        ");
               sql.AppendLine("		,ChangeNote=@ChangeNote        ");
               sql.AppendLine("		,AttendanceRate=@AttendanceRate        ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
               sql.AppendLine(" WHERE  ");
               sql.AppendLine(" ReprotNo = @ReprotNo ");
               sql.AppendLine(" AND CompanyCD = @CompanyCD  ");
               sql.AppendLine(" AND EmployeeID = @EmployeeID  ");
               sql.AppendLine(" AND AttendanceType = @AttendanceType  ");
               #endregion
               #region 报表调整信息参数设置
               SqlParameter[] param;
               param = new SqlParameter[10];
               param[0] = SqlHelper.GetParameter("@ChangeTimes", AttendanceReportDetailM.ChangeTimes);
               param[1] = SqlHelper.GetParameter("@ChangeType", AttendanceReportDetailM.ChangeType);
               param[2] = SqlHelper.GetParameter("@ChangeNote", AttendanceReportDetailM.ChangeNote);
               param[3] = SqlHelper.GetParameter("@AttendanceRate", AttendanceReportDetailM.AttendanceRate);
               param[4] = SqlHelper.GetParameter("@ModifiedDate", AttendanceReportDetailM.ModifiedDate);
               param[5] = SqlHelper.GetParameter("@ModifiedUserID", AttendanceReportDetailM.ModifiedUserID);
               param[6] = SqlHelper.GetParameter("@ReprotNo", AttendanceReportDetailM.ReprotNo);
               param[7] = SqlHelper.GetParameter("@CompanyCD", AttendanceReportDetailM.CompanyCD);
               param[8] = SqlHelper.GetParameter("@EmployeeID", AttendanceReportDetailM.EmployeeID);
               param[9] = SqlHelper.GetParameter("@AttendanceType", AttendanceType);

               #endregion
               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion

       #region 确认报表信息
       /// <summary>
       /// 确认报表信息
       /// </summary>
       /// <param name="AttendanceReportDetailM">报表信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool ConfirmAttendanceChangeInfo(AttendanceReportModel AttendanceReportM)
       {
           try
           {
               #region 报表调整信息SQL拼写
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.AttendanceReport SET ");
               sql.AppendLine("		 Status=@Status        ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
               sql.AppendLine(" WHERE  ");
               sql.AppendLine(" ReprotNo = @ReprotNo ");
               sql.AppendLine(" AND CompanyCD = @CompanyCD  ");
               #endregion
               #region 报表调整信息参数设置
               SqlParameter[] param;
               param = new SqlParameter[5];
               param[0] = SqlHelper.GetParameter("@Status", AttendanceReportM.Status);
               param[1] = SqlHelper.GetParameter("@ModifiedDate", AttendanceReportM.ModifiedDate);
               param[2] = SqlHelper.GetParameter("@ModifiedUserID", AttendanceReportM.ModifiedUserID);
               param[3] = SqlHelper.GetParameter("@ReprotNo", AttendanceReportM.ReprotNo);
               param[4] = SqlHelper.GetParameter("@CompanyCD", AttendanceReportM.CompanyCD);
               #endregion
               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion
       #region 能否删除考勤报表信息
       /// <summary>
       /// 能否删除考勤报表信息
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码</param>
       /// <returns></returns>
       public static bool IfDeleteAttendanceReportInfo(string AttendanceReportNOS, string CompanyID)
       {
           string[] NOS = null;
           NOS = AttendanceReportNOS.Split(',');
           bool Flag = true;

           for (int i = 0; i < NOS.Length; i++)
           {
               if (IsExistInfo(NOS[i], CompanyID))
               {
                   Flag = false;
                   break;
               }
           }
           return Flag;
       }
       #endregion
       #region 能否删除考勤报表信息
       /// <summary>
       /// 能否删除考勤报表信息
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码</param>
       /// <returns></returns>
       public static bool IsExistInfo(string No, string CompanyID)
       {

           string sql = "SELECT * FROM officedba.AttendanceReport WHERE ReprotNo='" + No + "' AND CompanyCD='" + CompanyID + "' AND Status=1";
           return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
       }
       #endregion
       #region 删除考勤信息
       /// <summary>
       /// 删除考勤信息
       /// </summary>
       /// <param name="EquipmentIDS">设备IDS</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool DeleteAttendanceReportInfo(string AttendanceNos, string CompanyID)
       {
           string allApplyID = "";
           System.Text.StringBuilder sb = new System.Text.StringBuilder();
           string[] Delsql = new string[2];
           try
           {
               string[] IDS = null;
               IDS = AttendanceNos.Split(',');

               for (int i = 0; i < IDS.Length; i++)
               {
                   IDS[i] = "'" + IDS[i] + "'";
                   sb.Append(IDS[i]);
               }

               allApplyID = sb.ToString().Replace("''", "','");
               Delsql[0] = "DELETE FROM officedba.AttendanceReport WHERE ReprotNo IN (" + allApplyID + ") AND CompanyCD='" + CompanyID + "'";
               Delsql[1] = "DELETE FROM officedba.AttendanceReportMonth WHERE ReprotNo IN (" + allApplyID + ") AND CompanyCD='" + CompanyID + "'";
               SqlHelper.ExecuteTransForListWithSQL(Delsql);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion

       #region 考勤日报表
       /// <summary>
       /// 考勤日报表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceDailyList(string StartDate, string EndDate, string DeptID, string CompanyID, int pageIndex, int pageCount, string ord, ref int TotalCount)
       {
              string sql = "SELECT A.WorkNowCount,ISNULL(C.DeptName,'')DeptName,B.*,"
                            +"(Convert(INT,ISNULL(A.WorkNowCount,0))-Convert(INT,ISNULL(B.FactManCount,0))) AS NoManCount "
                            +" FROM "
                            + "(SELECT ISNULL(DeptID,'')DeptID,"
                            +"CompanyCD,Count(*) AS WorkNowCount FROM officedba.EmployeeInfo "
                            +"WHERE Flag='1' "
                            +"GROUP BY DeptID,CompanyCD) A "
                            +"FULL JOIN "
                            + "(SELECT Convert(varchar(10),(ISNULL(ISNULL(ISNULL(Z.Date,Y.Date),X.EnterDate),W.OutDate)),120)Date,"
                            +"ISNULL(ISNULL(ISNULL(Z.CompanyCD,Y.CompanyCD),X.CompanyCD),W.CompanyCD)CompanyCD,"
                            +"ISNULL(ISNULL(ISNULL(Z.DeptID,Y.DeptID),X.DeptID),W.DeptID)DeptID,"
                            +"ISNULL(Z.FactManCount,'')FactManCount,ISNULL(Y.DelayManCount,'')DelayManCount,"
                            +"ISNULL(X.NewManCount,'')NewManCount,ISNULL(W.OutManCount,'')OutManCount "
                            +"FROM "
                            +"(SELECT A.Date,A.CompanyCD,B.DeptID,COUNT(*) AS FactManCount FROM "
                            +"(SELECT Date,CompanyCD,EmployeeID FROM officedba.DailyAttendance "
                            +"GROUP BY Date,CompanyCD,EmployeeID) A "
                            +"LEFT OUTER JOIN officedba.EmployeeInfo B "
                            +"ON A.EmployeeID=B.ID AND A.CompanyCD=B.CompanyCD "
                            + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' "
                            +"GROUP BY A.Date,A.CompanyCD,B.DeptID) Z "
                            +"FULL JOIN "
                            +"(SELECT A.Date,A.CompanyCD,B.DeptID,COUNT(*) AS DelayManCount FROM "
                            +"(SELECT Date,CompanyCD,EmployeeID FROM officedba.DailyAttendance "
                            + "WHERE IsDelay='1' AND Date>='" + StartDate + "' AND Date<='" + EndDate + "' "
                            +"GROUP BY Date,CompanyCD,EmployeeID) A "
                            +"LEFT OUTER JOIN officedba.EmployeeInfo B "
                            +"ON A.EmployeeID=B.ID AND A.CompanyCD=B.CompanyCD "
                            +"GROUP BY A.Date,A.CompanyCD,B.DeptID "
                            +") Y "
                            +"ON Z.Date=Y.Date AND Z.CompanyCD=Y.CompanyCD AND Z.DeptID=Y.DeptID "
                            +"FULL JOIN "
                            +"(SELECT EnterDate,ISNULL(DeptID,'')DeptID,"
                            +"CompanyCD,Count(*) AS NewManCount FROM officedba.EmployeeInfo "
                            + "WHERE Flag='1' AND EnterDate>='" + StartDate + "' AND EnterDate<='" + EndDate + "' "
                            +"GROUP BY EnterDate,DeptID,CompanyCD) X "
                            +"ON X.EnterDate=Y.Date AND X.CompanyCD=Y.CompanyCD AND X.DeptID=Y.DeptID "
                            +"FULL JOIN "
                            +"(SELECT A.OutDate,A.CompanyCD,isnull(B.DeptID,'')DeptID,Count(*) AS OutManCount FROM officedba.MoveNotify A "
                            +"LEFT OUTER JOIN officedba.EmployeeInfo B "
                            +"ON A.CompanyCD=B.CompanyCD AND A.EmployeeID=B.ID "
                            + "WHERE 1=1 AND OutDate>='" + StartDate + "' AND OutDate<='" + EndDate + "' "
                            +"GROUP BY A.OutDate,A.CompanyCD,B.DeptID) W "
                            +"ON X.EnterDate=W.OutDate AND X.CompanyCD=W.CompanyCD AND X.DeptID=W.DeptID) B "
                            +"ON A.CompanyCD=B.CompanyCD AND A.DeptID=B.DeptID "
                            +"LEFT OUTER JOIN officedba.DeptInfo C "
                            +"ON A.DeptID=C.ID "
                            + " WHERE B.CompanyCD='" + CompanyID + "'  ";
              if (DeptID != "")
                  sql += " and B.DeptID = '" + DeptID + "'";
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
           //return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 考勤日报表
       /// <summary>
       /// 考勤日报表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceDailyPrintList(string StartDate, string EndDate, string DeptID, string CompanyID,string ord)
       {
           string sql = "SELECT A.WorkNowCount,ISNULL(C.DeptName,'')DeptName,B.*,"
                         + "(Convert(INT,ISNULL(A.WorkNowCount,0))-Convert(INT,ISNULL(B.FactManCount,0))) AS NoManCount "
                         + " FROM "
                         + "(SELECT ISNULL(DeptID,'')DeptID,"
                         + "CompanyCD,Count(*) AS WorkNowCount FROM officedba.EmployeeInfo "
                         + "WHERE Flag='1' "
                         + "GROUP BY DeptID,CompanyCD) A "
                         + "FULL JOIN "
                         + "(SELECT Convert(varchar(10),(ISNULL(ISNULL(ISNULL(Z.Date,Y.Date),X.EnterDate),W.OutDate)),120)Date,"
                         + "ISNULL(ISNULL(ISNULL(Z.CompanyCD,Y.CompanyCD),X.CompanyCD),W.CompanyCD)CompanyCD,"
                         + "ISNULL(ISNULL(ISNULL(Z.DeptID,Y.DeptID),X.DeptID),W.DeptID)DeptID,"
                         + "ISNULL(Z.FactManCount,'')FactManCount,ISNULL(Y.DelayManCount,'')DelayManCount,"
                         + "ISNULL(X.NewManCount,'')NewManCount,ISNULL(W.OutManCount,'')OutManCount "
                         + "FROM "
                         + "(SELECT A.Date,A.CompanyCD,B.DeptID,COUNT(*) AS FactManCount FROM "
                         + "(SELECT Date,CompanyCD,EmployeeID FROM officedba.DailyAttendance "
                         + "GROUP BY Date,CompanyCD,EmployeeID) A "
                         + "LEFT OUTER JOIN officedba.EmployeeInfo B "
                         + "ON A.EmployeeID=B.ID AND A.CompanyCD=B.CompanyCD "
                         + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' "
                         + "GROUP BY A.Date,A.CompanyCD,B.DeptID) Z "
                         + "FULL JOIN "
                         + "(SELECT A.Date,A.CompanyCD,B.DeptID,COUNT(*) AS DelayManCount FROM "
                         + "(SELECT Date,CompanyCD,EmployeeID FROM officedba.DailyAttendance "
                         + "WHERE IsDelay='1' AND Date>='" + StartDate + "' AND Date<='" + EndDate + "' "
                         + "GROUP BY Date,CompanyCD,EmployeeID) A "
                         + "LEFT OUTER JOIN officedba.EmployeeInfo B "
                         + "ON A.EmployeeID=B.ID AND A.CompanyCD=B.CompanyCD "
                         + "GROUP BY A.Date,A.CompanyCD,B.DeptID "
                         + ") Y "
                         + "ON Z.Date=Y.Date AND Z.CompanyCD=Y.CompanyCD AND Z.DeptID=Y.DeptID "
                         + "FULL JOIN "
                         + "(SELECT EnterDate,ISNULL(DeptID,'')DeptID,"
                         + "CompanyCD,Count(*) AS NewManCount FROM officedba.EmployeeInfo "
                         + "WHERE Flag='1' AND EnterDate>='" + StartDate + "' AND EnterDate<='" + EndDate + "' "
                         + "GROUP BY EnterDate,DeptID,CompanyCD) X "
                         + "ON X.EnterDate=Y.Date AND X.CompanyCD=Y.CompanyCD AND X.DeptID=Y.DeptID "
                         + "FULL JOIN "
                         + "(SELECT A.OutDate,A.CompanyCD,isnull(B.DeptID,'')DeptID,Count(*) AS OutManCount FROM officedba.MoveNotify A "
                         + "LEFT OUTER JOIN officedba.EmployeeInfo B "
                         + "ON A.CompanyCD=B.CompanyCD AND A.EmployeeID=B.ID "
                         + "WHERE 1=1 AND OutDate>='" + StartDate + "' AND OutDate<='" + EndDate + "' "
                         + "GROUP BY A.OutDate,A.CompanyCD,B.DeptID) W "
                         + "ON X.EnterDate=W.OutDate AND X.CompanyCD=W.CompanyCD AND X.DeptID=W.DeptID) B "
                         + "ON A.CompanyCD=B.CompanyCD AND A.DeptID=B.DeptID "
                         + "LEFT OUTER JOIN officedba.DeptInfo C "
                         + "ON A.DeptID=C.ID "
                         + " WHERE B.CompanyCD='" + CompanyID + "'  ";
           if (DeptID != "")
               sql += " and B.DeptID = '" + DeptID + "'";
           sql = sql + ord;
           return SqlHelper.ExecuteSql(sql);

           //return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
           //return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 考勤月报表
       /// <summary>
       /// 考勤月报表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceMonthList(string StartDate, string EndDate, string DeptID,string EmpID, string CompanyID,string YearMonth, int pageIndex, int pageCount, string ord, ref int TotalCount)
       {
           string sql = "SELECT N.DeptName,O.EmployeeName,M.* FROM "
                    + "(SELECT ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.CompanyCD,Y.CompanyCD),X.CompanyCD),W.CompanyCD),V.CompanyCD),U.CompanyCD)CompanyCD,"
                    +"ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.EmployeeID,Y.EmployeeID),X.EmployeeID),W.EmployeeID),V.EmployeeID),U.EmployeeID)EmployeeID,"
                    +"ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.DeptID,Y.DeptID),X.DeptID),W.DeptID),V.DeptID),U.DeptID)DeptID,"
                    +"ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.YearMonth,Y.YearMonth),X.YearMonth),W.YearMonth),V.YearMonth),U.YearMonth)YearMonth,"
                    +"ISNULL(Z.AttendanceInDay,'')AttendanceInDay,ISNULL(Y.AttendanceDelayDay,'')AttendanceDelayDay,"
                    +"ISNULL(X.AttendanceForwaroffDay,'')AttendanceForwaroffDay,ISNULL(W.LeaveDays,'')LeaveDays,ISNULL(V.BusinessDays,'')BusinessDays,"
                    +"ISNULL(U.YearDays,'')YearDays,ISNULL(T.MonthRestDays,'')MonthRestDays "
                    +"FROM "
                    +"(SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    +",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth, "
                    +"COUNT(*) AS AttendanceInDay "
                    +"FROM "
                    +"("
                    +"SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    +"LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    +"ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' "
                    +") A "
                    +"GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    +") Z "//---总出勤
                    +"FULL JOIN "
                    +"(SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    +",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth,"
                    +"COUNT(*) AS AttendanceDelayDay "
                    +"FROM "
                    +"("
                    +"SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    +"LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    +"ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' AND IsDelay='1' "
                    +") A "
                    +"GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    +") Y "//-----迟到
                    +"ON Z.CompanyCD=Y.CompanyCD AND Z.EmployeeID=Y.EmployeeID AND Z.DeptID=Y.DeptID AND Z.YearMonth=Y.YearMonth "
                    +"FULL JOIN "
                    +"("
                    +"SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    +",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth,"
                    +"COUNT(*) AS AttendanceForwaroffDay "
                    +"FROM "
                    +"("
                    +"SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    +"LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    +"ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' AND IsForwarOff='1' "
                    +") A "
                    +"GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    +") X "//---早退
                    +"ON X.CompanyCD=Z.CompanyCD AND X.EmployeeID=Z.EmployeeID AND X.DeptID=Z.DeptID AND X.YearMonth=Z.YearMonth "
                    +"FULL JOIN "
                    +"(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    +"SUM(LeaveDay) LeaveDays "
                    +"FROM "
                    +"(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS LeaveDay "
                    +"FROM officedba.AttendanceApply a "
                    +"LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    +"ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    +"LEFT OUTER JOIN officedba.FlowInstance d "
                    +"ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    +"AND d.BillTypeCode='20'  "
                    +"LEFT OUTER JOIN "
                    +"(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    +"where m.BillTypeFlag='3' " 
                    +"AND m.BillTypeCode='20' and  m.BillID=n.ID group by m.BillID) e "
                    +"ON d.id=e.id "
                    + "WHERE a.Flag='1' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    +"GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    +") W "//---请假
                    +"ON W.CompanyCD=Z.CompanyCD AND W.EmployeeID=Z.EmployeeID AND W.DeptID=Z.DeptID AND W.YearMonth=Z.YearMonth "
                    +"FULL JOIN "
                    +"(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.BusinessDate))+'-'+convert(varchar(2),DATEPART(Mm, A.BusinessDate)) AS YearMonth,"
                    +"SUM(BusinessDay) BusinessDays "
                    +"FROM "
                    +"(SELECT a.CompanyCD,a.BusinessDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.BusinessDate)),convert(datetime,(a.BusinessPlanDate)))AS BusinessDay "
                    +"FROM officedba.AttendanceApply a "
                    +"LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    +"ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    +"LEFT OUTER JOIN officedba.FlowInstance d "
                    +"ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    +"AND d.BillTypeCode='23'  "
                    +"LEFT OUTER JOIN "
                    +"(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    +"where m.BillTypeFlag='3' "
                    +"AND m.BillTypeCode='23' and  m.BillID=n.ID group by m.BillID) e "
                    +"ON d.id=e.id "
                    + "WHERE a.Flag='4' AND a.BusinessDate>='" + StartDate + "' AND a.BusinessDate<='" + EndDate + "') A "
                    +"GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.BusinessDate))+'-'+convert(varchar(2),DATEPART(Mm, A.BusinessDate)) "
                    +") V "//---出差
                    +"ON V.CompanyCD=Z.CompanyCD AND V.EmployeeID=Z.EmployeeID AND V.DeptID=Z.DeptID AND V.YearMonth=Z.YearMonth "
                    +"FULL JOIN "
                    +"(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    +"SUM(YearDay) YearDays "
                    +"FROM "
                    +"(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS YearDay "
                    +"FROM officedba.AttendanceApply a "
                    +"LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    +"ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    +"LEFT OUTER JOIN officedba.FlowInstance d "
                    +"ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    +"AND d.BillTypeCode='25'  "
                    +"LEFT OUTER JOIN "
                    +"(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    +"where m.BillTypeFlag='3' "
                    +"AND m.BillTypeCode='25' and  m.BillID=n.ID group by m.BillID) e "
                    +"ON d.id=e.id "
                    + "WHERE a.Flag='6' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    +"GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    +") U "//--年休
                    +"ON U.CompanyCD=Z.CompanyCD AND U.EmployeeID=Z.EmployeeID AND U.DeptID=Z.DeptID AND U.YearMonth=Z.YearMonth "
                    +"FULL JOIN "
                    +"(SELECT COUNT(*)MonthRestDays,A.CompanyCD,A.EmployeeID,isnull(A.DeptID,'')DeptID,YearMonth FROM "
                    +"(select DISTINCT a.MonthRestDay,a.CompanyCD,a.EmployeeID,b.DeptID,"
                    +"Convert(varchar(4),DATEPART(Yy, a.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, a.StartDate)) AS YearMonth "
                    +"from officedba.EmployeeAttendanceSet a "
                    +"LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    +"ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE (a.StartDate>='" + StartDate + "' AND (a.EndDate<='" + EndDate + "' OR a.EndDate is null))) A "
                    +"GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,YearMonth) T "
                    +"ON T.CompanyCD=Z.CompanyCD AND T.EmployeeID=Z.EmployeeID AND T.DeptID=Z.DeptID AND T.YearMonth=Z.YearMonth "
                    +") M "
                    +"LEFT OUTER JOIN officedba.DeptInfo N "
                    +"ON M.CompanyCD=N.CompanyCD AND M.DeptID=N.ID "
                    +"LEFT OUTER JOIN officedba.EmployeeInfo O "
                    +"ON M.EmployeeID=O.ID "
                    + "WHERE M.YearMonth='" + YearMonth + "' AND M.CompanyCD='" + CompanyID + "' ";
           if (DeptID != "")
               sql += " and M.DeptID = '" + DeptID + "'";
           if (EmpID != "")
               sql += " and M.EmployeeID = '" + EmpID + "'";
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
           //return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 考勤月报表打印
       /// <summary>
       /// 考勤月报表打印
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceMonthPrintList(string StartDate, string EndDate, string DeptID, string EmpID, string CompanyID, string YearMonth,string ord)
       {
           string sql = "SELECT N.DeptName,O.EmployeeName,M.* FROM "
                    + "(SELECT ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.CompanyCD,Y.CompanyCD),X.CompanyCD),W.CompanyCD),V.CompanyCD),U.CompanyCD)CompanyCD,"
                    + "ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.EmployeeID,Y.EmployeeID),X.EmployeeID),W.EmployeeID),V.EmployeeID),U.EmployeeID)EmployeeID,"
                    + "ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.DeptID,Y.DeptID),X.DeptID),W.DeptID),V.DeptID),U.DeptID)DeptID,"
                    + "ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.YearMonth,Y.YearMonth),X.YearMonth),W.YearMonth),V.YearMonth),U.YearMonth)YearMonth,"
                    + "ISNULL(Z.AttendanceInDay,'')AttendanceInDay,ISNULL(Y.AttendanceDelayDay,'')AttendanceDelayDay,"
                    + "ISNULL(X.AttendanceForwaroffDay,'')AttendanceForwaroffDay,ISNULL(W.LeaveDays,'')LeaveDays,ISNULL(V.BusinessDays,'')BusinessDays,"
                    + "ISNULL(U.YearDays,'')YearDays,ISNULL(T.MonthRestDays,'')MonthRestDays "
                    + "FROM "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    + ",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth, "
                    + "COUNT(*) AS AttendanceInDay "
                    + "FROM "
                    + "("
                    + "SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' "
                    + ") A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    + ") Z "//---总出勤
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    + ",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth,"
                    + "COUNT(*) AS AttendanceDelayDay "
                    + "FROM "
                    + "("
                    + "SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' AND IsDelay='1' "
                    + ") A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    + ") Y "//-----迟到
                    + "ON Z.CompanyCD=Y.CompanyCD AND Z.EmployeeID=Y.EmployeeID AND Z.DeptID=Y.DeptID AND Z.YearMonth=Y.YearMonth "
                    + "FULL JOIN "
                    + "("
                    + "SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    + ",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth,"
                    + "COUNT(*) AS AttendanceForwaroffDay "
                    + "FROM "
                    + "("
                    + "SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' AND IsForwarOff='1' "
                    + ") A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    + ") X "//---早退
                    + "ON X.CompanyCD=Z.CompanyCD AND X.EmployeeID=Z.EmployeeID AND X.DeptID=Z.DeptID AND X.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(LeaveDay) LeaveDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS LeaveDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='20'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='20' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='1' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") W "//---请假
                    + "ON W.CompanyCD=Z.CompanyCD AND W.EmployeeID=Z.EmployeeID AND W.DeptID=Z.DeptID AND W.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.BusinessDate))+'-'+convert(varchar(2),DATEPART(Mm, A.BusinessDate)) AS YearMonth,"
                    + "SUM(BusinessDay) BusinessDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.BusinessDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.BusinessDate)),convert(datetime,(a.BusinessPlanDate)))AS BusinessDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='23'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='23' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='4' AND a.BusinessDate>='" + StartDate + "' AND a.BusinessDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.BusinessDate))+'-'+convert(varchar(2),DATEPART(Mm, A.BusinessDate)) "
                    + ") V "//---出差
                    + "ON V.CompanyCD=Z.CompanyCD AND V.EmployeeID=Z.EmployeeID AND V.DeptID=Z.DeptID AND V.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(YearDay) YearDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS YearDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='25'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='25' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='6' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") U "//--年休
                    + "ON U.CompanyCD=Z.CompanyCD AND U.EmployeeID=Z.EmployeeID AND U.DeptID=Z.DeptID AND U.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT COUNT(*)MonthRestDays,A.CompanyCD,A.EmployeeID,isnull(A.DeptID,'')DeptID,YearMonth FROM "
                    + "(select DISTINCT a.MonthRestDay,a.CompanyCD,a.EmployeeID,b.DeptID,"
                    + "Convert(varchar(4),DATEPART(Yy, a.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, a.StartDate)) AS YearMonth "
                    + "from officedba.EmployeeAttendanceSet a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE (a.StartDate>='" + StartDate + "' AND (a.EndDate<='" + EndDate + "' OR a.EndDate is null))) A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,YearMonth) T "
                    + "ON T.CompanyCD=Z.CompanyCD AND T.EmployeeID=Z.EmployeeID AND T.DeptID=Z.DeptID AND T.YearMonth=Z.YearMonth "
                    + ") M "
                    + "LEFT OUTER JOIN officedba.DeptInfo N "
                    + "ON M.CompanyCD=N.CompanyCD AND M.DeptID=N.ID "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo O "
                    + "ON M.EmployeeID=O.ID "
                    + "WHERE M.YearMonth='" + YearMonth + "' AND M.CompanyCD='" + CompanyID + "' ";
           if (DeptID != "")
               sql += " and M.DeptID = '" + DeptID + "'";
           if (EmpID != "")
               sql += " and M.EmployeeID = '" + EmpID + "'";
           sql = sql + ord;
           return SqlHelper.ExecuteSql(sql);

           //return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
           //return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 考勤年报表
       /// <summary>
       /// 考勤年报表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceYearList(string StartDate, string EndDate, string DeptID,string EmpID, string CompanyID, string Year, int pageIndex, int pageCount, string ord, ref int TotalCount)
       {
           string sql = "SELECT N.DeptName,O.EmployeeName,M.* FROM "
                    + "(SELECT ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.CompanyCD,Y.CompanyCD),X.CompanyCD),W.CompanyCD),V.CompanyCD),U.CompanyCD)CompanyCD,"
                    + "ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.EmployeeID,Y.EmployeeID),X.EmployeeID),W.EmployeeID),V.EmployeeID),U.EmployeeID)EmployeeID,"
                    + "ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.DeptID,Y.DeptID),X.DeptID),W.DeptID),V.DeptID),U.DeptID)DeptID,"
                    + "ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.YearMonth,Y.YearMonth),X.YearMonth),W.YearMonth),V.YearMonth),U.YearMonth)YearMonth,"
                    + "ISNULL(Z.AttendanceInDay,'')AttendanceInDay,ISNULL(Y.AttendanceDelayDay,'')AttendanceDelayDay,"
                    + "ISNULL(X.AttendanceForwaroffDay,'')AttendanceForwaroffDay,ISNULL(W.LeaveDays,'')LeaveDays,ISNULL(V.BusinessDays,'')BusinessDays,"
                    + "ISNULL(U.YearDays,'')YearDays,ISNULL(T.MonthRestDays,'')MonthRestDays "
                    + "FROM "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    + ",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth, "
                    + "COUNT(*) AS AttendanceInDay "
                    + "FROM "
                    + "("
                    + "SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' "
                    + ") A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    + ") Z "//---总出勤
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    + ",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth,"
                    + "COUNT(*) AS AttendanceDelayDay "
                    + "FROM "
                    + "("
                    + "SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' AND IsDelay='1' "
                    + ") A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    + ") Y "//-----迟到
                    + "ON Z.CompanyCD=Y.CompanyCD AND Z.EmployeeID=Y.EmployeeID AND Z.DeptID=Y.DeptID AND Z.YearMonth=Y.YearMonth "
                    + "FULL JOIN "
                    + "("
                    + "SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    + ",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth,"
                    + "COUNT(*) AS AttendanceForwaroffDay "
                    + "FROM "
                    + "("
                    + "SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' AND IsForwarOff='1' "
                    + ") A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    + ") X "//---早退
                    + "ON X.CompanyCD=Z.CompanyCD AND X.EmployeeID=Z.EmployeeID AND X.DeptID=Z.DeptID AND X.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(LeaveDay) LeaveDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS LeaveDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='20'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='20' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='1' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") W "//---请假
                    + "ON W.CompanyCD=Z.CompanyCD AND W.EmployeeID=Z.EmployeeID AND W.DeptID=Z.DeptID AND W.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.BusinessDate))+'-'+convert(varchar(2),DATEPART(Mm, A.BusinessDate)) AS YearMonth,"
                    + "SUM(BusinessDay) BusinessDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.BusinessDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.BusinessDate)),convert(datetime,(a.BusinessPlanDate)))AS BusinessDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='23'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='23' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='4' AND a.BusinessDate>='" + StartDate + "' AND a.BusinessDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.BusinessDate))+'-'+convert(varchar(2),DATEPART(Mm, A.BusinessDate)) "
                    + ") V "//---出差
                    + "ON V.CompanyCD=Z.CompanyCD AND V.EmployeeID=Z.EmployeeID AND V.DeptID=Z.DeptID AND V.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(YearDay) YearDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS YearDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='25'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='25' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='6' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") U "//--年休
                    + "ON U.CompanyCD=Z.CompanyCD AND U.EmployeeID=Z.EmployeeID AND U.DeptID=Z.DeptID AND U.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT COUNT(*)MonthRestDays,A.CompanyCD,A.EmployeeID,isnull(A.DeptID,'')DeptID,YearMonth FROM "
                    + "(select DISTINCT a.MonthRestDay,a.CompanyCD,a.EmployeeID,b.DeptID,"
                    + "Convert(varchar(4),DATEPART(Yy, a.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, a.StartDate)) AS YearMonth "
                    + "from officedba.EmployeeAttendanceSet a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE (a.StartDate>='" + StartDate + "' AND (a.EndDate<='" + EndDate + "' OR a.EndDate is null))) A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,YearMonth) T "
                    + "ON T.CompanyCD=Z.CompanyCD AND T.EmployeeID=Z.EmployeeID AND T.DeptID=Z.DeptID AND T.YearMonth=Z.YearMonth "
                    + ") M "
                    + "LEFT OUTER JOIN officedba.DeptInfo N "
                    + "ON M.CompanyCD=N.CompanyCD AND M.DeptID=N.ID "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo O "
                    + "ON M.EmployeeID=O.ID "
                    + "WHERE M.YearMonth like '%" + Year + "%' AND M.CompanyCD='" + CompanyID + "' ";
           if (DeptID != "")
               sql += " and M.DeptID = '" + DeptID + "'";
           if (EmpID != "")
               sql += " and M.EmployeeID = '" + EmpID + "'";
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
           //return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 考勤年报表打印
       /// <summary>
       /// 考勤年报表打印
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceYearListPrint(string StartDate, string EndDate, string DeptID, string EmpID, string CompanyID, string Year,string ord)
       {
           string sql = "SELECT N.DeptName,O.EmployeeName,M.* FROM "
                    + "(SELECT ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.CompanyCD,Y.CompanyCD),X.CompanyCD),W.CompanyCD),V.CompanyCD),U.CompanyCD)CompanyCD,"
                    + "ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.EmployeeID,Y.EmployeeID),X.EmployeeID),W.EmployeeID),V.EmployeeID),U.EmployeeID)EmployeeID,"
                    + "ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.DeptID,Y.DeptID),X.DeptID),W.DeptID),V.DeptID),U.DeptID)DeptID,"
                    + "ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(Z.YearMonth,Y.YearMonth),X.YearMonth),W.YearMonth),V.YearMonth),U.YearMonth)YearMonth,"
                    + "ISNULL(Z.AttendanceInDay,'')AttendanceInDay,ISNULL(Y.AttendanceDelayDay,'')AttendanceDelayDay,"
                    + "ISNULL(X.AttendanceForwaroffDay,'')AttendanceForwaroffDay,ISNULL(W.LeaveDays,'')LeaveDays,ISNULL(V.BusinessDays,'')BusinessDays,"
                    + "ISNULL(U.YearDays,'')YearDays,ISNULL(T.MonthRestDays,'')MonthRestDays "
                    + "FROM "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    + ",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth, "
                    + "COUNT(*) AS AttendanceInDay "
                    + "FROM "
                    + "("
                    + "SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' "
                    + ") A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    + ") Z "//---总出勤
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    + ",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth,"
                    + "COUNT(*) AS AttendanceDelayDay "
                    + "FROM "
                    + "("
                    + "SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' AND IsDelay='1' "
                    + ") A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    + ") Y "//-----迟到
                    + "ON Z.CompanyCD=Y.CompanyCD AND Z.EmployeeID=Y.EmployeeID AND Z.DeptID=Y.DeptID AND Z.YearMonth=Y.YearMonth "
                    + "FULL JOIN "
                    + "("
                    + "SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    + ",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth,"
                    + "COUNT(*) AS AttendanceForwaroffDay "
                    + "FROM "
                    + "("
                    + "SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' AND IsForwarOff='1' "
                    + ") A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    + ") X "//---早退
                    + "ON X.CompanyCD=Z.CompanyCD AND X.EmployeeID=Z.EmployeeID AND X.DeptID=Z.DeptID AND X.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(LeaveDay) LeaveDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS LeaveDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='20'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='20' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='1' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") W "//---请假
                    + "ON W.CompanyCD=Z.CompanyCD AND W.EmployeeID=Z.EmployeeID AND W.DeptID=Z.DeptID AND W.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.BusinessDate))+'-'+convert(varchar(2),DATEPART(Mm, A.BusinessDate)) AS YearMonth,"
                    + "SUM(BusinessDay) BusinessDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.BusinessDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.BusinessDate)),convert(datetime,(a.BusinessPlanDate)))AS BusinessDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='23'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='23' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='4' AND a.BusinessDate>='" + StartDate + "' AND a.BusinessDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.BusinessDate))+'-'+convert(varchar(2),DATEPART(Mm, A.BusinessDate)) "
                    + ") V "//---出差
                    + "ON V.CompanyCD=Z.CompanyCD AND V.EmployeeID=Z.EmployeeID AND V.DeptID=Z.DeptID AND V.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(YearDay) YearDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS YearDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='25'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='25' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='6' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") U "//--年休
                    + "ON U.CompanyCD=Z.CompanyCD AND U.EmployeeID=Z.EmployeeID AND U.DeptID=Z.DeptID AND U.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT COUNT(*)MonthRestDays,A.CompanyCD,A.EmployeeID,isnull(A.DeptID,'')DeptID,YearMonth FROM "
                    + "(select DISTINCT a.MonthRestDay,a.CompanyCD,a.EmployeeID,b.DeptID,"
                    + "Convert(varchar(4),DATEPART(Yy, a.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, a.StartDate)) AS YearMonth "
                    + "from officedba.EmployeeAttendanceSet a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE (a.StartDate>='" + StartDate + "' AND (a.EndDate<='" + EndDate + "' OR a.EndDate is null))) A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,YearMonth) T "
                    + "ON T.CompanyCD=Z.CompanyCD AND T.EmployeeID=Z.EmployeeID AND T.DeptID=Z.DeptID AND T.YearMonth=Z.YearMonth "
                    + ") M "
                    + "LEFT OUTER JOIN officedba.DeptInfo N "
                    + "ON M.CompanyCD=N.CompanyCD AND M.DeptID=N.ID "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo O "
                    + "ON M.EmployeeID=O.ID "
                    + "WHERE M.YearMonth like '%" + Year + "%' AND M.CompanyCD='" + CompanyID + "' ";
           if (DeptID != "")
               sql += " and M.DeptID = '" + DeptID + "'";
           if (EmpID != "")
               sql += " and M.EmployeeID = '" + EmpID + "'";
           sql = sql + ord;
           return SqlHelper.ExecuteSql(sql);
           //return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
           //return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 考勤年度缺勤报表
       /// <summary>
       /// 考勤年度缺勤报表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceAbsenceYearList(string StartDate, string EndDate, string DeptID, string EmpID,string CompanyID, string Year, int pageIndex, int pageCount, string ord, ref int TotalCount)
       {
           string sql = "SELECT N.DeptName,O.EmployeeName,day(dateadd(mm,1,getdate())-day(getdate()))-M.AttendanceInDay-M.YearDays-M.LeaveDays-M.BusinessDays-M.MonthRestDays as totalDays,M.* FROM "
                    + "(SELECT ISNULL(ISNULL(ISNULL(Z.CompanyCD,W.CompanyCD),V.CompanyCD),U.CompanyCD)CompanyCD,"
                    + "ISNULL(ISNULL(ISNULL(Z.EmployeeID,W.EmployeeID),V.EmployeeID),U.EmployeeID)EmployeeID,"
                    + "ISNULL(ISNULL(ISNULL(Z.DeptID,W.DeptID),V.DeptID),U.DeptID)DeptID,"
                    + "ISNULL(ISNULL(ISNULL(Z.YearMonth,W.YearMonth),V.YearMonth),U.YearMonth)YearMonth,"
                    + "ISNULL(Z.AttendanceInDay,'')AttendanceInDay,"
                    + "ISNULL(W.LeaveDays,'')LeaveDays,ISNULL(V.BusinessDays,'')BusinessDays,"
                    + "ISNULL(U.YearDays,'')YearDays,ISNULL(T.MonthRestDays,'')MonthRestDays,ISNULL(WW.LeaveDiseaseDays,'')LeaveDiseaseDays,ISNULL(WWW.LeavePersonDays,'')LeavePersonDays,ISNULL(WWWW.LeaveOtherDays,'')LeaveOtherDays "
                    + "FROM "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    + ",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth, "
                    + "COUNT(*) AS AttendanceInDay "
                    + "FROM "
                    + "("
                    + "SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' "
                    + ") A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    + ") Z "//---总出勤
                    //+ "FULL JOIN "
                    //+ "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    //+ ",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth,"
                    //+ "COUNT(*) AS AttendanceDelayDay "
                    //+ "FROM "
                    //+ "("
                    //+ "SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    //+ "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    //+ "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    //+ "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' AND IsDelay='1' "
                    //+ ") A "
                    //+ "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    //+ ") Y "//-----迟到
                    //+ "ON Z.CompanyCD=Y.CompanyCD AND Z.EmployeeID=Y.EmployeeID AND Z.DeptID=Y.DeptID AND Z.YearMonth=Y.YearMonth "
                    //+ "FULL JOIN "
                    //+ "("
                    //+ "SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    //+ ",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth,"
                    //+ "COUNT(*) AS AttendanceForwaroffDay "
                    //+ "FROM "
                    //+ "("
                    //+ "SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    //+ "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    //+ "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    //+ "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' AND IsForwarOff='1' "
                    //+ ") A "
                    //+ "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    //+ ") X "//---早退
                    //+ "ON X.CompanyCD=Z.CompanyCD AND X.EmployeeID=Z.EmployeeID AND X.DeptID=Z.DeptID AND X.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(LeaveDay) LeaveDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS LeaveDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='20'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='20' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='1' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") W "//---请假
                    + "ON W.CompanyCD=Z.CompanyCD AND W.EmployeeID=Z.EmployeeID AND W.DeptID=Z.DeptID AND W.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.BusinessDate))+'-'+convert(varchar(2),DATEPART(Mm, A.BusinessDate)) AS YearMonth,"
                    + "SUM(BusinessDay) BusinessDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.BusinessDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.BusinessDate)),convert(datetime,(a.BusinessPlanDate)))AS BusinessDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='23'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='23' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='4' AND a.BusinessDate>='" + StartDate + "' AND a.BusinessDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.BusinessDate))+'-'+convert(varchar(2),DATEPART(Mm, A.BusinessDate)) "
                    + ") V "//---出差
                    + "ON V.CompanyCD=Z.CompanyCD AND V.EmployeeID=Z.EmployeeID AND V.DeptID=Z.DeptID AND V.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(YearDay) YearDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS YearDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='25'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='25' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='6' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") U "//--年休
                    + "ON U.CompanyCD=Z.CompanyCD AND U.EmployeeID=Z.EmployeeID AND U.DeptID=Z.DeptID AND U.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT COUNT(*)MonthRestDays,A.CompanyCD,A.EmployeeID,isnull(A.DeptID,'')DeptID,YearMonth FROM "
                    + "(select DISTINCT a.MonthRestDay,a.CompanyCD,a.EmployeeID,b.DeptID,"
                    + "Convert(varchar(4),DATEPART(Yy, a.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, a.StartDate)) AS YearMonth "
                    + "from officedba.EmployeeAttendanceSet a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE (a.StartDate>='" + StartDate + "' AND (a.EndDate<='" + EndDate + "' OR a.EndDate is null))) A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,YearMonth) T "
                    + "ON T.CompanyCD=Z.CompanyCD AND T.EmployeeID=Z.EmployeeID AND T.DeptID=Z.DeptID AND T.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(LeaveDay) LeaveDiseaseDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS LeaveDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='20'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='20' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='1' AND a.LeaveType='3' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") WW "//---病假
                    + "ON WW.CompanyCD=Z.CompanyCD AND WW.EmployeeID=Z.EmployeeID AND WW.DeptID=Z.DeptID AND WW.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(LeaveDay) LeavePersonDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS LeaveDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='20'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='20' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='1' AND a.LeaveType='4' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") WWW "//---事假
                    + "ON WWW.CompanyCD=Z.CompanyCD AND WWW.EmployeeID=Z.EmployeeID AND WWW.DeptID=Z.DeptID AND WWW.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(LeaveDay) LeaveOtherDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS LeaveDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='20'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='20' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='1' AND a.LeaveType='5' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") WWWW "//---其他原因
                    + "ON WWWW.CompanyCD=Z.CompanyCD AND WWWW.EmployeeID=Z.EmployeeID AND WWWW.DeptID=Z.DeptID AND WWW.YearMonth=Z.YearMonth "
                    + ") M "
                    + "LEFT OUTER JOIN officedba.DeptInfo N "
                    + "ON M.CompanyCD=N.CompanyCD AND M.DeptID=N.ID "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo O "
                    + "ON M.EmployeeID=O.ID "
                    + "WHERE M.YearMonth like '%" + Year + "%' AND M.CompanyCD='" + CompanyID + "' ";
           if (DeptID != "")
               sql += " and M.DeptID = '" + DeptID + "' ";
           if (EmpID != "")
               sql += " and M.EmployeeID = '" + EmpID + "'";
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
           //return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 考勤年度缺勤报表打印
       /// <summary>
       /// 考勤年度缺勤报表打印
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceAbsenceYearListPrint(string StartDate, string EndDate, string DeptID, string EmpID, string CompanyID, string Year,string ord)
       {
           string sql = "SELECT N.DeptName,O.EmployeeName,M.* FROM "
                    + "(SELECT ISNULL(ISNULL(ISNULL(Z.CompanyCD,W.CompanyCD),V.CompanyCD),U.CompanyCD)CompanyCD,"
                    + "ISNULL(ISNULL(ISNULL(Z.EmployeeID,W.EmployeeID),V.EmployeeID),U.EmployeeID)EmployeeID,"
                    + "ISNULL(ISNULL(ISNULL(Z.DeptID,W.DeptID),V.DeptID),U.DeptID)DeptID,"
                    + "ISNULL(ISNULL(ISNULL(Z.YearMonth,W.YearMonth),V.YearMonth),U.YearMonth)YearMonth,"
                    + "ISNULL(Z.AttendanceInDay,'')AttendanceInDay,"
                    + "ISNULL(W.LeaveDays,'')LeaveDays,ISNULL(V.BusinessDays,'')BusinessDays,"
                    + "ISNULL(U.YearDays,'')YearDays,ISNULL(T.MonthRestDays,'')MonthRestDays,ISNULL(WW.LeaveDiseaseDays,'')LeaveDiseaseDays,ISNULL(WWW.LeavePersonDays,'')LeavePersonDays,ISNULL(WWWW.LeaveOtherDays,'')LeaveOtherDays "
                    + "FROM "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID "
                    + ",Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) AS YearMonth, "
                    + "COUNT(*) AS AttendanceInDay "
                    + "FROM "
                    + "("
                    + "SELECT DISTINCT a.CompanyCD,a.EmployeeID,b.DeptID,a.Date FROM officedba.DailyAttendance a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE Date>='" + StartDate + "' AND Date<='" + EndDate + "' "
                    + ") A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.Date))+'-'+convert(varchar(2),DATEPART(Mm, A.Date)) "
                    + ") Z "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(LeaveDay) LeaveDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS LeaveDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='20'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='20' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='1' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") W "//---请假
                    + "ON W.CompanyCD=Z.CompanyCD AND W.EmployeeID=Z.EmployeeID AND W.DeptID=Z.DeptID AND W.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.BusinessDate))+'-'+convert(varchar(2),DATEPART(Mm, A.BusinessDate)) AS YearMonth,"
                    + "SUM(BusinessDay) BusinessDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.BusinessDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.BusinessDate)),convert(datetime,(a.BusinessPlanDate)))AS BusinessDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='23'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='23' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='4' AND a.BusinessDate>='" + StartDate + "' AND a.BusinessDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.BusinessDate))+'-'+convert(varchar(2),DATEPART(Mm, A.BusinessDate)) "
                    + ") V "//---出差
                    + "ON V.CompanyCD=Z.CompanyCD AND V.EmployeeID=Z.EmployeeID AND V.DeptID=Z.DeptID AND V.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(YearDay) YearDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS YearDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='25'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='25' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='6' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") U "//--年休
                    + "ON U.CompanyCD=Z.CompanyCD AND U.EmployeeID=Z.EmployeeID AND U.DeptID=Z.DeptID AND U.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT COUNT(*)MonthRestDays,A.CompanyCD,A.EmployeeID,isnull(A.DeptID,'')DeptID,YearMonth FROM "
                    + "(select DISTINCT a.MonthRestDay,a.CompanyCD,a.EmployeeID,b.DeptID,"
                    + "Convert(varchar(4),DATEPART(Yy, a.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, a.StartDate)) AS YearMonth "
                    + "from officedba.EmployeeAttendanceSet a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "WHERE (a.StartDate>='" + StartDate + "' AND (a.EndDate<='" + EndDate + "' OR a.EndDate is null))) A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,YearMonth) T "
                    + "ON T.CompanyCD=Z.CompanyCD AND T.EmployeeID=Z.EmployeeID AND T.DeptID=Z.DeptID AND T.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(LeaveDay) LeaveDiseaseDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS LeaveDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='20'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='20' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='1' AND a.LeaveType='3' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") WW "//---病假
                    + "ON WW.CompanyCD=Z.CompanyCD AND WW.EmployeeID=Z.EmployeeID AND WW.DeptID=Z.DeptID AND WW.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(LeaveDay) LeavePersonDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS LeaveDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='20'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='20' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='1' AND a.LeaveType='4' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") WWW "//---事假
                    + "ON WWW.CompanyCD=Z.CompanyCD AND WWW.EmployeeID=Z.EmployeeID AND WWW.DeptID=Z.DeptID AND WWW.YearMonth=Z.YearMonth "
                    + "FULL JOIN "
                    + "(SELECT A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) AS YearMonth,"
                    + "SUM(LeaveDay) LeaveOtherDays "
                    + "FROM "
                    + "(SELECT a.CompanyCD,a.StartDate,a.EmployeeID,b.DeptID,DateDiff(Day,Convert(datetime,(a.StartDate+a.StartTime)),convert(datetime,(a.EndDate+a.EndTime)))AS LeaveDay "
                    + "FROM officedba.AttendanceApply a "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo  b "
                    + "ON a.EmployeeID=b.ID AND a.CompanyCD=b.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                    + "ON a.ApplyNo=d.BillNo AND d.BillTypeFlag='3' "
                    + "AND d.BillTypeCode='20'  "
                    + "LEFT OUTER JOIN "
                    + "(select max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.BillTypeFlag='3' "
                    + "AND m.BillTypeCode='20' and  m.BillID=n.ID group by m.BillID) e "
                    + "ON d.id=e.id "
                    + "WHERE a.Flag='1' AND a.LeaveType='5' AND a.StartDate>='" + StartDate + "' AND a.StartDate<='" + EndDate + "') A "
                    + "GROUP BY A.CompanyCD,A.EmployeeID,A.DeptID,Convert(varchar(4),DATEPART(Yy, A.StartDate))+'-'+convert(varchar(2),DATEPART(Mm, A.StartDate)) "
                    + ") WWWW "//---其他原因
                    + "ON WWWW.CompanyCD=Z.CompanyCD AND WWWW.EmployeeID=Z.EmployeeID AND WWWW.DeptID=Z.DeptID AND WWW.YearMonth=Z.YearMonth "
                    + ") M "
                    + "LEFT OUTER JOIN officedba.DeptInfo N "
                    + "ON M.CompanyCD=N.CompanyCD AND M.DeptID=N.ID "
                    + "LEFT OUTER JOIN officedba.EmployeeInfo O "
                    + "ON M.EmployeeID=O.ID "
                    + "WHERE M.YearMonth like '%" + Year + "%' AND M.CompanyCD='" + CompanyID + "' ";
           if (DeptID != "")
               sql += " and M.DeptID = '" + DeptID + "' ";
           if (EmpID != "")
               sql += " and M.EmployeeID = '" + EmpID + "'";
           sql = sql + ord;
           return SqlHelper.ExecuteSql(sql);

           //return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
           //return SqlHelper.ExecuteSql(sql);
       }
       #endregion
    }
}

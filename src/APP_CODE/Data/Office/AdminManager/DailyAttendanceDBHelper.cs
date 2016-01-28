/**********************************************
 * 类作用：   日常考勤数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/02
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
    /// 类名：DailyAttendanceDBHelper
    /// 描述：日常考勤数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/04/02
    /// </summary>
   public class DailyAttendanceDBHelper
   {
       #region 插入员工日常考勤签到信息
       /// <summary>
       /// 插入员工日常考勤签到信息
       /// </summary>
       /// <param name="DailyAttendanceM">签到信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddDailyAttendanceSignIn(DailyAttendanceModel DailyAttendanceM)
        {
            try
            {
                #region 日常考勤签到信息SQL拼写
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO officedba.DailyAttendance");
                sql.AppendLine("		(CompanyCD      ");
                sql.AppendLine("		,EmployeeID        ");
                sql.AppendLine("		,Date        ");
                sql.AppendLine("		,StartTime        ");
                sql.AppendLine("		,WorkShiftTimeID        ");
                sql.AppendLine("		,DelayTimeLong        ");
                sql.AppendLine("		,IsDelay        ");
                sql.AppendLine("		,EmployeeAttendanceSetID        ");
                sql.AppendLine("		,SignInRemark)        ");
                sql.AppendLine("VALUES                  ");
                sql.AppendLine("		(@CompanyCD   ");
                sql.AppendLine("		,@EmployeeID       ");
                sql.AppendLine("		,@Date       ");
                sql.AppendLine("		,@StartTime       ");
                sql.AppendLine("		,@WorkShiftTimeID       ");
                sql.AppendLine("		,@DelayTimeLong       ");
                sql.AppendLine("		,@IsDelay       ");
                sql.AppendLine("		,@EmployeeAttendanceSetID       ");
                sql.AppendLine("		,@Remark)       ");
                #endregion
                #region 日常考勤签到信息参数设置
                SqlParameter[] param;
                param = new SqlParameter[9];
                param[0] = SqlHelper.GetParameter("@CompanyCD", DailyAttendanceM.CompanyCD);
                param[1] = SqlHelper.GetParameter("@EmployeeID", DailyAttendanceM.EmployeeID);
                param[2] = SqlHelper.GetParameter("@Date", DailyAttendanceM.Date);
                param[3] = SqlHelper.GetParameter("@StartTime", DailyAttendanceM.StartTime);
                param[4] = SqlHelper.GetParameter("@WorkShiftTimeID", DailyAttendanceM.WorkShiftTimeID);
                param[5] = SqlHelper.GetParameter("@DelayTimeLong", DailyAttendanceM.DelayTimeLong);
                param[6] = SqlHelper.GetParameter("@IsDelay", DailyAttendanceM.IsDelay);
                param[7] = SqlHelper.GetParameter("@EmployeeAttendanceSetID", DailyAttendanceM.EmployeeAttendanceSetID);
                param[8] = SqlHelper.GetParameter("@Remark", DailyAttendanceM.SignInRemark);
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
       #region 获取考勤信息列表
       /// <summary>
       /// 获取考勤信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetDailyAttendanceInfo(string EmployeeNo, string EmployeeName, string Quarter, string AttendanceStartDate, string AttendanceEndDate,string CompanyID,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           string sql = "select a.ID,a.CompanyCD,a.EmployeeID,convert(varchar(10),convert(datetime,a.Date),120) Date,"
                         +"CONVERT(varchar, a.StartTime, 120 ) StartTime,CONVERT(varchar, isnull(a.EndTime,'1900-1-1'), 120) EndTime,"
                         + "isnull(a.SignInRemark,'') SignInRemark,isnull(a.SignOutRemark,'') SignOutRemark,a.IsExtraSignIn,a.IsExtraSignOut,a.ProcessSignInReason,a.ProcessSignOutReason,a.ProcessDateTime,f.EmployeeName AS ProcessUserName,"
                         + "b.EmployeeName,b.EmployeeNo,isnull(c.DeptName,'')DeptName,a.WorkShiftTimeID,isnull(y.ShiftTimeName,'')ShiftTimeName,"
                         + "isnull(d.QuarterName,0) NowQuarterID  "
                         +"from officedba.DailyAttendance a  "
                         +"LEFT OUTER JOIN  "
                         +"officedba.EmployeeInfo b "
                         +"on a.EmployeeID=b.ID "
                         + "LEFT OUTER JOIN  "
                         + "officedba.EmployeeInfo f "
                         + "on a.ProcessUserID=f.ID "
                         + "LEFT OUTER JOIN officedba.DeptInfo c "
                         + "on b.DeptID=c.ID "
                         + "LEFT OUTER JOIN  "
                         + "officedba.DeptQuarter d "
                         + "on b.QuarterID=d.ID "
                         +"LEFT OUTER JOIN officedba.WorkShiftTime y "
                         + "on a.WorkShiftTimeID=y.ID WHERE a.CompanyCD='" + CompanyID + "' ";
           if (EmployeeNo != "")
               sql += " and b.EmployeeNo like '%" + EmployeeNo + "%'";
           if (EmployeeName != "")
               sql += " and b.EmployeeName like '%" + EmployeeName + "%'";
           if (Quarter != "")
               sql += " and d.ID=" + Quarter + "";
           if (AttendanceStartDate != "")
               sql += " and a.Date>='" + AttendanceStartDate + "'";
           if (AttendanceEndDate != "")
               sql += " and a.Date<='" + AttendanceEndDate + "'";
           //return SqlHelper.ExecuteSql(sql);
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

       }
       #endregion
       #region 签到时的判断
       /// <summary>
       /// 签到时的判断
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码0</param>
       /// <returns></returns>
       public static int IsSignIn(int EmployeeID,int WorkShiftTimeID)
       {
           string sql = "select isnull(max(id),0) as IsExist from officedba.DailyAttendance "
                         + "where "
                         + "WorkShiftTimeID=" + WorkShiftTimeID + " and EmployeeID=" + EmployeeID + " "
                         + "and Date=convert(varchar(10),convert(datetime,getdate()),120) ";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           if (IsExist != null && (int)IsExist.Rows[0][0] > 0)
           {
               return (int)IsExist.Rows[0][0];//已经签到
           }
           else
           {
               return 0;//签到
           }
       }
       #endregion
       #region 签退时的判断，是否已经做过签到
       /// <summary>
       /// 签退时的判断(是否已经做过签到)
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码0</param>
       /// <returns></returns>
       public static int IsSignOut(int EmployeeID, int WorkShiftTimeID)
       {
           string sql = "select isnull(max(id),0) as IsExist from officedba.DailyAttendance "
                         + "where "
                         + "WorkShiftTimeID=" + WorkShiftTimeID + " and EmployeeID=" + EmployeeID + " "
                         + "and Date=convert(varchar(10),convert(datetime,getdate()),120) "
                         + " and StartTime is not null";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           if (IsExist != null && (int)IsExist.Rows[0][0] > 0)
           {
               return (int)IsExist.Rows[0][0];//已经签到
           }
           else
           {
               return 0;//签到
           }
       }
       #endregion
       #region 签退时的判断，是否已经做过签退
       /// <summary>
       /// 签退时的判断(是否已经做过签退)
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码0</param>
       /// <returns></returns>
       public static int IsSignOut1(int EmployeeID, int WorkShiftTimeID)
       {
           string sql = "select isnull(max(id),0) as IsExist from officedba.DailyAttendance "
                         + "where "
                         + "WorkShiftTimeID=" + WorkShiftTimeID + " and EmployeeID=" + EmployeeID + " "
                         + "and Date=convert(varchar(10),convert(datetime,getdate()),120) "
                         + " and StartTime is not null and EndTime is not null";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           if (IsExist != null && (int)IsExist.Rows[0][0] > 0)
           {
               return (int)IsExist.Rows[0][0];//已经签到
           }
           else
           {
               return 0;//签到
           }
       }
       #endregion
       #region 判断签到还是签退
       /// <summary>
       /// 判断签到还是签退
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码0</param>
       /// <returns></returns>
       public static int SignInOrOut(int EmployeeID, string CompanyID)
       {
           string sql = "select isnull(max(id),0) as IsExist from officedba.DailyAttendance "
                         +"where "
                         + "CompanyCD='" + CompanyID + "' and EmployeeID=" + EmployeeID + " "
                         +"and Date=convert(varchar(10),convert(datetime,getdate()),120) "
                         +"and StartTime is not null and EndTime is null";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           if (IsExist != null && (int)IsExist.Rows[0][0] > 0)
           {
               return (int)IsExist.Rows[0][0];//签退
           }
           else
           {
               return 0;//签到
           }
       }
       #endregion
       #region 更新用户签退信息
       /// <summary>
       /// 更新用户签退信息
       /// </summary>
       /// <param name="DailyAttendanceM">签退信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateailyAttendanceSignOut(DailyAttendanceModel DailyAttendanceM)
       {
           try
           {
               #region 日常考勤签退信息SQL拼写
               string sql = "update officedba.DailyAttendance SET EndTime='" + DailyAttendanceM.EndTime + "',"
                             +"SignOutRemark='" + DailyAttendanceM .SignOutRemark+ "',"
                             + "ForWardOffTimeLong='" + DailyAttendanceM.ForWardOffTimeLong + "',"
                             + "IsForwarOff='" + DailyAttendanceM.IsForwarOff + "', "
                             + "EmployeeAttendanceSetID='" + DailyAttendanceM.EmployeeAttendanceSetID + "' "
                             +" where id=" 
                             +"(select isnull(max(id),0) as IsExist from officedba.DailyAttendance "
                             + "where CompanyCD='" + DailyAttendanceM.CompanyCD + "' and EmployeeID=" + DailyAttendanceM .EmployeeID+ " "
                             +"and Date=convert(varchar(10),convert(datetime,getdate()),120)  "
                             +"and StartTime is not null and EndTime is null)";
               #endregion
               SqlHelper.ExecuteTransSql(sql.ToString());
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               return false;
           }
       }
       #endregion
       #region 获取班组类型
       /// <summary>
       /// 获取班组类型
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetWorkGroupType(string CompanyCD, int Employeeid, string date)
       {
           string sql = "select a.WorkGroupNo,b.WorkGroupType "
                         +"from officedba.EmployeeAttendanceSet a "
                         +"left outer join officedba.WorkGroup b "
                         +"on a.CompanyCD=b.CompanyCD AND a.WorkGroupNo=b.WorkGroupNo "
                         + "where StartDate<='" + date + "' "
                         + "and (EndDate>'" + date + "' or EndDate is null) and EmployeeID=" + Employeeid + " and a.CompanyCD='"+CompanyCD+"'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 根据当前日期获取时间所在的时间段的开始日期以获取序号
       /// <summary>
       /// 获取某人当日班次对应的序号以找出其班次
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetStartDate(string CompanyCD, int Employeeid,string date)
       {
           string sql = "select * from officedba.WorkPlan where WorkPlanStartDate<='" +Convert.ToDateTime(date) + "' "
                            + "and (WorkPlanEndDate is null or WorkPlanEndDate>'" + Convert.ToDateTime(date) + "') and CompanyCD='" + CompanyCD + "'"
                            +"and WorkGroupNo=(select WorkGroupNo from officedba.EmployeeAttendanceSet "
                            + " where StartDate<='" + Convert.ToDateTime(date) + "' and (EndDate>'" + Convert.ToDateTime(date) + "' or EndDate is null) "
                            + "and EmployeeID=" + Employeeid + ")";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 根据序号获取班次绑定list
       /// <summary>
       /// 根据序号获取班次绑定
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetWorkShiftInfo(string CompanyCD, int Employeeid, string date,int xh)
       {
           string sql = "select b.ID,b.ShiftTimeName,b.OnTime,b.OffTime from "
                            + " ( "
                            + "select * from officedba.WorkPlan where WorkPlanStartDate<='" + date + "' "
                            + "and (WorkPlanEndDate is null or WorkPlanEndDate>'" + date + "') and CompanyCD='" + CompanyCD + "' "
                            + "and WorkGroupNo=(select WorkGroupNo from officedba.EmployeeAttendanceSet "
                            + "where StartDate<='" + date + "' and (EndDate>'" + date + "' or EndDate is null) "
                            + "and EmployeeID=" + Employeeid + ")  and WorkShiftIndex='" + xh + "') a left outer join officedba.WorkShiftTime b "
                            + "on a.WorkShiftNo=b.WorkShiftNo and a.CompanyCD=b.CompanyCD where b.CompanyCD='" + CompanyCD + "'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 根据序号获取班次绑定list
       /// <summary>
       /// 根据序号获取班次绑定
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetWorkShiftInfo(string CompanyCD, string WorkGroupNo)
       {
           string sql = "select b.ID,b.ShiftTimeName,b.OnTime,b.OffTime "
                        +"from "
                        +"officedba.WorkGroup a "
                        +"left outer join "
                        +"officedba.WorkShiftTime b "
                        +"on a.CompanyCD=b.CompanyCD AND  "
                        +"a.WorkShiftNo=b.WorkShiftNo "
                        + "where a.WorkGroupNo='" + WorkGroupNo + "' and a.CompanyCD='" + CompanyCD + "'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 考勤时判断此班段是否休息
       /// <summary>
       /// 根据序号获取班次绑定
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable IsRestDay(string CompanyCD, int Employeeid, string date, string xh)
       {
           string sql = "select * from officedba.WorkPlan where WorkPlanStartDate<='" + date + "' "
                            + "and (WorkPlanEndDate is null or WorkPlanEndDate>'" + date + "') and CompanyCD='" + CompanyCD + "' "
                            + "and WorkGroupNo=(select WorkGroupNo from officedba.EmployeeAttendanceSet "
                            + "where StartDate<='" + date + "' and (EndDate>'" + date + "' or EndDate is null) "
                            + "and EmployeeID=" + Employeeid + ")  and WorkShiftIndex='" + xh + "' ";                           
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 获取考勤异常时判断当天所有班次的班段是否都做过考勤（如果没有添加一行）
       /// <summary>
       /// 获取考勤异常时判断当天所有班次的班段是否都做过考勤（如果没有添加一行）
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable IsHaveAllWorkShiftTime(int EmployeeID, string Date, int WorkShiftTimeID)
       {
           string sql = "select * from officedba.DailyAttendance where EmployeeID=" + EmployeeID + " "
                           + "and Date='" + Date + "' and WorkShiftTimeID=" + WorkShiftTimeID + "";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 获取考勤申请数据
       /// <summary>
       /// 获取考勤申请数据
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceApplyData(string CompanyCD, int Employeeid, string date)
       {
           string sql = "select * from officedba.AttendanceApply where "
                        + "CompanyCD='" + CompanyCD + "' and  "
                        + "ApplyUserID=" + Employeeid + " and StartDate<='" + date + "' and EndDate>='" + date + "' "
                        +"and (Flag='1' or Flag='3' or Flag='4' or Flag='6')";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 获取人员最近人员设置信息
       /// <summary>
       /// 获取人员最近人员设置信息
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEmployeeAttendanceSetInfo(string CompanyCD, int Employeeid, string date)
       {
           string sql = "select ID,AttendanceType from officedba.EmployeeAttendanceSet "
                             + "where StartDate<='" + date + "' and (EndDate>'" + date + "' or EndDate is null) "
                             + "and EmployeeID=" + Employeeid + " AND CompanyCD='" + CompanyCD + "' ";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 获取此班次的的开始时间，结束时间和可迟到时间，可早退时间
       /// <summary>
       /// 根据序号获取班次绑定
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetWorkShiftTimeInfo(string workshifttimeid)
       {
           string sql = "select OnTime,OffTime,OnLateTime,OffForwordTime from officedba.WorkShiftTime WHERE ID=" + workshifttimeid + "";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 判断此员工当天有无签到（没有签到不能进行考勤申请）
       /// <summary>
       /// 判断此员工当天有无签到
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码0</param>
       /// <returns></returns>
       public static int IfSignIn(int EmployeeID)
       {
           string sql = "select isnull(max(id),0) as IsExist from officedba.DailyAttendance "
                         + "where "
                         + "EmployeeID=" + EmployeeID + " "
                         + "and Date=convert(varchar(10),convert(datetime,getdate()),120) ";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           if (IsExist != null && (int)IsExist.Rows[0][0] > 0)
           {
               return (int)IsExist.Rows[0][0];//已经签到
           }
           else
           {
               return 0;//签到
           }
       }
       #endregion

       #region 补签到前的判断
       /// <summary>
       /// 补签到前的判断
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetDailyInfoBeforeExtraSign(DailyAttendanceModel DailyAttendanceM)
       {
           string sql = "select * from officedba.DailyAttendance "
           + "WHERE  CompanyCD='" + DailyAttendanceM.CompanyCD + "' AND EmployeeID=" + DailyAttendanceM.EmployeeID + " AND "
           + "Date='" + DailyAttendanceM.Date + "' AND WorkShiftTimeID=" + DailyAttendanceM.WorkShiftTimeID + "";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 更新员工补签到信息
       /// <summary>
       /// 更新员工补签到信息
       /// </summary>
       /// <param name="DailyAttendanceM">补签到信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateExtraDailyAttendanceSignIn(DailyAttendanceModel DailyAttendanceM)
       {
           try
           {
               #region 日常考勤签到信息SQL拼写
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.DailyAttendance");
               sql.AppendLine("		SET StartTime=@StartTime       ");
               sql.AppendLine("		    ,DelayTimeLong=@DelayTimeLong       ");
               sql.AppendLine("		    ,IsDelay=@IsDelay       ");
               sql.AppendLine("		    ,IsExtraSignIn=1       ");
               sql.AppendLine("		    ,ProcessSignInReason=@ProcessSignInReason       ");
               sql.AppendLine("		    ,ProcessUserID=@ProcessUserID       ");
               sql.AppendLine("		    ,ProcessDateTime=@ProcessDateTime       ");
               sql.AppendLine("	  WHERE  CompanyCD=@CompanyCD AND        ");
               sql.AppendLine("		EmployeeID=@EmployeeID AND       ");
               sql.AppendLine("		Date=@Date AND        ");
               sql.AppendLine("		WorkShiftTimeID=@WorkShiftTimeID        ");
               #endregion
               #region 日常考勤签到信息参数设置
               SqlParameter[] param;
               param = new SqlParameter[10];
               param[0] = SqlHelper.GetParameter("@StartTime", DailyAttendanceM.StartTime);
               param[1] = SqlHelper.GetParameter("@DelayTimeLong", DailyAttendanceM.DelayTimeLong);
               param[2] = SqlHelper.GetParameter("@IsDelay", DailyAttendanceM.IsDelay);
               param[3] = SqlHelper.GetParameter("@ProcessSignInReason", DailyAttendanceM.ProcessSignInReason);
               param[4] = SqlHelper.GetParameter("@ProcessUserID", DailyAttendanceM.ProcessUserID);
               param[5] = SqlHelper.GetParameter("@ProcessDateTime", DailyAttendanceM.ProcessDateTime);
               param[6] = SqlHelper.GetParameter("@CompanyCD", DailyAttendanceM.CompanyCD);
               param[7] = SqlHelper.GetParameter("@EmployeeID", DailyAttendanceM.EmployeeID);
               param[8] = SqlHelper.GetParameter("@Date", DailyAttendanceM.Date);
               param[9] = SqlHelper.GetParameter("@WorkShiftTimeID", DailyAttendanceM.WorkShiftTimeID);
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

       #region 更新用户补签退信息
       /// <summary>
       /// 更新用户补签退信息
       /// </summary>
       /// <param name="DailyAttendanceM">签退信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateExtraDailyAttendanceSignOut(DailyAttendanceModel DailyAttendanceM)
       {
               try
               {
                   #region 日常考勤签到信息SQL拼写
                   StringBuilder sql = new StringBuilder();
                   sql.AppendLine("UPDATE officedba.DailyAttendance");
                   sql.AppendLine("		SET EndTime=@EndTime       ");
                   sql.AppendLine("		    ,ForWardOffTimeLong=@ForWardOffTimeLong       ");
                   sql.AppendLine("		    ,IsForwarOff=@IsForwarOff       ");
                   sql.AppendLine("		    ,IsExtraSignOut=1       ");
                   sql.AppendLine("		    ,ProcessSignOutReason=@ProcessSignOutReason       ");
                   sql.AppendLine("		    ,ProcessUserID=@ProcessUserID       ");
                   sql.AppendLine("		    ,ProcessDateTime=@ProcessDateTime       ");
                   sql.AppendLine("	  WHERE  CompanyCD=@CompanyCD AND        ");
                   sql.AppendLine("		EmployeeID=@EmployeeID AND       ");
                   sql.AppendLine("		Date=@Date AND        ");
                   sql.AppendLine("		WorkShiftTimeID=@WorkShiftTimeID        ");
                   #endregion
                   #region 日常考勤签到信息参数设置
                   SqlParameter[] param;
                   param = new SqlParameter[10];
                   param[0] = SqlHelper.GetParameter("@EndTime", DailyAttendanceM.EndTime);
                   param[1] = SqlHelper.GetParameter("@ForWardOffTimeLong", DailyAttendanceM.ForWardOffTimeLong);
                   param[2] = SqlHelper.GetParameter("@IsForwarOff", DailyAttendanceM.IsForwarOff);
                   param[3] = SqlHelper.GetParameter("@ProcessSignOutReason", DailyAttendanceM.ProcessSignOutReason);
                   param[4] = SqlHelper.GetParameter("@ProcessUserID", DailyAttendanceM.ProcessUserID);
                   param[5] = SqlHelper.GetParameter("@ProcessDateTime", DailyAttendanceM.ProcessDateTime);
                   param[6] = SqlHelper.GetParameter("@CompanyCD", DailyAttendanceM.CompanyCD);
                   param[7] = SqlHelper.GetParameter("@EmployeeID", DailyAttendanceM.EmployeeID);
                   param[8] = SqlHelper.GetParameter("@Date", DailyAttendanceM.Date);
                   param[9] = SqlHelper.GetParameter("@WorkShiftTimeID", DailyAttendanceM.WorkShiftTimeID);
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

       #region 插入用户补签到签退信息
       /// <summary>
       /// 插入用户补签到签退信息
       /// </summary>
       /// <param name="DailyAttendanceM">签退信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool InsertExtraDailyAttendanceSignIn_Out(DailyAttendanceModel DailyAttendanceM)
       {
           try
           {
              
               #region 日常考勤签到信息SQL拼写
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("INSERT INTO officedba.DailyAttendance");
               sql.AppendLine("		(CompanyCD      ");
               sql.AppendLine("		,EmployeeID        ");
               sql.AppendLine("		,Date        ");
               sql.AppendLine("		,StartTime        ");
               sql.AppendLine("		,EndTime        ");
               sql.AppendLine("		,WorkShiftTimeID        ");
               sql.AppendLine("		,DelayTimeLong        ");
               sql.AppendLine("		,ForWardOffTimeLong        ");
               sql.AppendLine("		,IsDelay        ");
               sql.AppendLine("		,IsForwarOff        ");
               sql.AppendLine("		,EmployeeAttendanceSetID        ");
               sql.AppendLine("		,IsExtraSignIn        ");
               sql.AppendLine("		,IsExtraSignOut        ");
               sql.AppendLine("		,ProcessSignInReason        ");
               sql.AppendLine("		,ProcessSignOutReason        ");
               sql.AppendLine("		,ProcessUserID        ");
               sql.AppendLine("		,ProcessDateTime)        ");
               sql.AppendLine("VALUES                  ");
               sql.AppendLine("		(@CompanyCD   ");
               sql.AppendLine("		,@EmployeeID       ");
               sql.AppendLine("		,@Date       ");
               sql.AppendLine("		,@StartTime       ");
               sql.AppendLine("		,@EndTime       ");
               sql.AppendLine("		,@WorkShiftTimeID       ");
               sql.AppendLine("		,@DelayTimeLong       ");
               sql.AppendLine("		,@ForWardOffTimeLong       ");
               sql.AppendLine("		,@IsDelay       ");
               sql.AppendLine("		,@IsForwarOff       ");
               sql.AppendLine("		,@EmployeeAttendanceSetID       ");
               sql.AppendLine("		,1       ");
               sql.AppendLine("		,1       ");
               sql.AppendLine("		,@ProcessSignInReason       ");
               sql.AppendLine("		,@ProcessSignOutReason       ");
               sql.AppendLine("		,@ProcessUserID       ");
               sql.AppendLine("		,@ProcessDateTime)       ");
               #endregion
               #region 日常考勤签到信息参数设置
               SqlParameter[] param;
               param = new SqlParameter[15];
               param[0] = SqlHelper.GetParameter("@CompanyCD", DailyAttendanceM.CompanyCD);
               param[1] = SqlHelper.GetParameter("@EmployeeID", DailyAttendanceM.EmployeeID);
               param[2] = SqlHelper.GetParameter("@Date", DailyAttendanceM.Date);
               param[3] = SqlHelper.GetParameter("@StartTime", DailyAttendanceM.StartTime);
               param[4] = SqlHelper.GetParameter("@EndTime", DailyAttendanceM.EndTime);
               param[5] = SqlHelper.GetParameter("@WorkShiftTimeID", DailyAttendanceM.WorkShiftTimeID);
               param[6] = SqlHelper.GetParameter("@DelayTimeLong", DailyAttendanceM.DelayTimeLong);
               param[7] = SqlHelper.GetParameter("@ForWardOffTimeLong", DailyAttendanceM.ForWardOffTimeLong);
               param[8] = SqlHelper.GetParameter("@IsDelay", DailyAttendanceM.IsDelay);
               param[9] = SqlHelper.GetParameter("@IsForwarOff", DailyAttendanceM.IsForwarOff);
               param[10] = SqlHelper.GetParameter("@EmployeeAttendanceSetID", DailyAttendanceM.EmployeeAttendanceSetID);
               param[11] = SqlHelper.GetParameter("@ProcessSignInReason", DailyAttendanceM.ProcessSignInReason);
               param[12] = SqlHelper.GetParameter("@ProcessSignOutReason", DailyAttendanceM.ProcessSignInReason);
               param[13] = SqlHelper.GetParameter("@ProcessUserID", DailyAttendanceM.ProcessUserID);
               param[14] = SqlHelper.GetParameter("@ProcessDateTime", DailyAttendanceM.ProcessDateTime);
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

       #region 更新用户补签到签退信息
       /// <summary>
       /// 更新用户补签到签退信息
       /// </summary>
       /// <param name="DailyAttendanceM">签退信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateExtraDailyAttendanceSignIn_Out(DailyAttendanceModel DailyAttendanceM)
       {
           try
           {

               #region 日常考勤签到信息SQL拼写
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.DailyAttendance");
               sql.AppendLine("		SET StartTime=@StartTime        ");
               sql.AppendLine("		,EndTime=@EndTime        ");
               sql.AppendLine("		,DelayTimeLong=@DelayTimeLong        ");
               sql.AppendLine("		,ForWardOffTimeLong=@ForWardOffTimeLong        ");
               sql.AppendLine("		,IsDelay=@IsDelay        ");
               sql.AppendLine("		,IsForwarOff=@IsForwarOff        ");
               sql.AppendLine("		,EmployeeAttendanceSetID=@EmployeeAttendanceSetID        ");
               sql.AppendLine("		,IsExtraSignIn='1'        ");
               sql.AppendLine("		,IsExtraSignOut='1'        ");
               sql.AppendLine("		,ProcessSignInReason=@ProcessSignInReason        ");
               sql.AppendLine("		,ProcessSignOutReason=@ProcessSignOutReason        ");
               sql.AppendLine("		,ProcessUserID=@ProcessUserID        ");
               sql.AppendLine("		,ProcessDateTime=@ProcessDateTime         ");
               sql.AppendLine("		WHERE CompanyCD=@CompanyCD      ");
               sql.AppendLine("		AND EmployeeID=@EmployeeID        ");
               sql.AppendLine("		AND Date=@Date        ");
               sql.AppendLine("		AND WorkShiftTimeID=@WorkShiftTimeID        ");
               #endregion
               #region 日常考勤签到信息参数设置
               SqlParameter[] param;
               param = new SqlParameter[15];
               param[0] = SqlHelper.GetParameter("@CompanyCD", DailyAttendanceM.CompanyCD);
               param[1] = SqlHelper.GetParameter("@EmployeeID", DailyAttendanceM.EmployeeID);
               param[2] = SqlHelper.GetParameter("@Date", DailyAttendanceM.Date);
               param[3] = SqlHelper.GetParameter("@StartTime", DailyAttendanceM.StartTime);
               param[4] = SqlHelper.GetParameter("@EndTime", DailyAttendanceM.EndTime);
               param[5] = SqlHelper.GetParameter("@WorkShiftTimeID", DailyAttendanceM.WorkShiftTimeID);
               param[6] = SqlHelper.GetParameter("@DelayTimeLong", DailyAttendanceM.DelayTimeLong);
               param[7] = SqlHelper.GetParameter("@ForWardOffTimeLong", DailyAttendanceM.ForWardOffTimeLong);
               param[8] = SqlHelper.GetParameter("@IsDelay", DailyAttendanceM.IsDelay);
               param[9] = SqlHelper.GetParameter("@IsForwarOff", DailyAttendanceM.IsForwarOff);
               param[10] = SqlHelper.GetParameter("@EmployeeAttendanceSetID", DailyAttendanceM.EmployeeAttendanceSetID);
               param[11] = SqlHelper.GetParameter("@ProcessSignInReason", DailyAttendanceM.ProcessSignInReason);
               param[12] = SqlHelper.GetParameter("@ProcessSignOutReason", DailyAttendanceM.ProcessSignInReason);
               param[13] = SqlHelper.GetParameter("@ProcessUserID", DailyAttendanceM.ProcessUserID);
               param[14] = SqlHelper.GetParameter("@ProcessDateTime", DailyAttendanceM.ProcessDateTime);
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
       #region 日常考勤列表导出
       /// <summary>
       /// 日常考勤列表导出
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetDailyAttendanceInfoForExp(string EmployeeNo, string EmployeeName, string Quarter, string AttendanceStartDate, string AttendanceEndDate, string CompanyID,string ord)
       {
           string sql = "select a.ID,a.CompanyCD,a.EmployeeID,convert(varchar(10),convert(datetime,a.Date),120) Date,"
                         + "CONVERT(varchar, a.StartTime, 120 ) StartTime,CONVERT(varchar, isnull(a.EndTime,'1900-1-1'), 120) EndTime,"
                         + "isnull(a.SignInRemark,'') SignInRemark,isnull(a.SignOutRemark,'') SignOutRemark,a.IsExtraSignIn,a.IsExtraSignOut,a.ProcessSignInReason,a.ProcessSignOutReason,a.ProcessDateTime,f.EmployeeName AS ProcessUserName,"
                         + "b.EmployeeName,b.EmployeeNo,isnull(c.DeptName,'')DeptName,a.WorkShiftTimeID,isnull(y.ShiftTimeName,'')ShiftTimeName,"
                         + "isnull(d.QuarterName,0) NowQuarterID  "
                         + "from officedba.DailyAttendance a  "
                         + "LEFT OUTER JOIN  "
                         + "officedba.EmployeeInfo b "
                         + "on a.EmployeeID=b.ID "
                         + "LEFT OUTER JOIN  "
                         + "officedba.EmployeeInfo f "
                         + "on a.ProcessUserID=f.ID "
                         + "LEFT OUTER JOIN officedba.DeptInfo c "
                         + "on b.DeptID=c.ID "
                         + "LEFT OUTER JOIN  "
                         + "officedba.DeptQuarter d "
                         + "on b.QuarterID=d.ID "
                         + "LEFT OUTER JOIN officedba.WorkShiftTime y "
                         + "on a.WorkShiftTimeID=y.ID WHERE a.CompanyCD='" + CompanyID + "' ";
           if (EmployeeNo != "")
               sql += " and b.EmployeeNo like '%" + EmployeeNo + "%'";
           if (EmployeeName != "")
               sql += " and b.EmployeeName like '%" + EmployeeName + "%'";
           if (Quarter != "")
               sql += " and d.ID=" + Quarter + "";
           if (AttendanceStartDate != "")
               sql += " and a.Date>='" + AttendanceStartDate + "'";
           if (AttendanceEndDate != "")
               sql += " and a.Date<='" + AttendanceEndDate + "' ";
           sql += ord;
           return SqlHelper.ExecuteSql(sql);
           //return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

       }
       #endregion
       #region 判断给定企业编号、班组名称，查询是否有此班组信息
       /// <summary>
       /// 判断给定企业编号、班组名称，查询是否有此班组信息
       /// lysong 2009-09-04
       /// </summary>
       /// <param name="WorkGroupName"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable IsHaveAttendanceRecords(string AttendanceDate, string EmployeeID, string CompanyCD, string WorkShiftTimeID)
       {
           try
           {
               SqlParameter[] parameters = {   
                                               new SqlParameter("@AttendanceDate", SqlDbType.VarChar, 50),
                                               new SqlParameter("@EmployeeID",SqlDbType.VarChar,50),
                                               new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                               new SqlParameter("@WorkShiftTimeID",SqlDbType.VarChar,50)
                                           };
               parameters[0].Value = AttendanceDate;
               parameters[1].Value = EmployeeID;
               parameters[2].Value = CompanyCD;
               parameters[3].Value = WorkShiftTimeID;
               string searchSql = "select * from officedba.dailyattendance where Date=@AttendanceDate and EmployeeID=@EmployeeID and CompanyCD=@CompanyCD and WorkShiftTimeID=WorkShiftTimeID";
               DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), parameters);
               return data;
           }
           catch
           {
               return null;
           }

       }
       #endregion
       #region 插入考勤记录导入信息
       /// <summary>
       /// 插入考勤记录导入信息
       /// </summary>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InsertAttendanceRecord(DataTable dt, string CompanyCD)
       {
           try
           {
               SqlCommand[] comms = new SqlCommand[dt.Rows.Count];
               string IsForward = "0";//是否早退
               int ForwardMinute = 0;//早退时间
               string IsDelay = "0";//是否迟到
               int DelayMinute = 0;//迟到时间

               for (int i = 0; i < dt.Rows.Count; i++) //循环数组
               {
                   string CurrDatePart = Convert.ToDateTime(dt.Rows[i]["考勤日期"].ToString()).ToShortDateString();//考勤日期
                   //签到
                   TimeSpan spanin = DateTime.Parse((CurrDatePart + " " + dt.Rows[i]["开始时间"].ToString())) - DateTime.Parse(CurrDatePart + " " + dt.Rows[i]["OnTime"].ToString());
                   int Minute = spanin.Hours * 60 + spanin.Minutes;//相差分钟数
                   if (Minute > Convert.ToInt32(dt.Rows[0]["OnLateTime"]))//是否超过允许迟到的时间
                   {
                       IsDelay = "1";//迟到
                       DelayMinute = Minute;//迟到分钟数
                   }
                   //签退
                   TimeSpan spanout = DateTime.Parse(CurrDatePart + " " + dt.Rows[0]["OffTime"].ToString()) - DateTime.Parse((CurrDatePart + " " + dt.Rows[i]["结束时间"].ToString()));
                   int Minuteout = spanout.Hours * 60 + spanout.Minutes;//相差分钟数
                   if (Minuteout > Convert.ToInt32(dt.Rows[0]["OffForwordTime"]))//是否超过允许早退的时间
                   {
                       IsForward = "1";//早退
                       ForwardMinute = Minuteout;//早退分钟数
                   }
                   string EmployeeID = dt.Rows[i]["EmployeeID"].ToString().Trim();//被考勤人
                   string AttendanceDate = CurrDatePart;//考勤日期
                   string StartTime = CurrDatePart + " " + dt.Rows[i]["开始时间"].ToString();//上班时间
                   string EndTime = CurrDatePart + " " + dt.Rows[i]["结束时间"].ToString();//下班时间
                   string WorkShiftTimeID = dt.Rows[i]["WorkShiftTimeID"].ToString();//考勤班段
                   //公司编码
                   //是否迟到
                   //是否早退
                   //迟到时间
                   //早退时间
                   string EmployeeAttendanceSetID = dt.Rows[i]["EmployeeAttenanceSetID"].ToString();//人员考勤设置ID
                   #region 拼写添加入库明细信息sql语句
                   StringBuilder sql = new StringBuilder();
                   sql.AppendLine("INSERT INTO officedba.DailyAttendance");
                   sql.AppendLine("(CompanyCD");
                   sql.AppendLine(",EmployeeID     ");
                   sql.AppendLine(",Date");
                   sql.AppendLine(",StartTime   ");
                   sql.AppendLine(",EndTime  ");
                   sql.AppendLine(",WorkShiftTimeID    ");
                   sql.AppendLine(",DelayTimeLong    ");
                   sql.AppendLine(",ForWardOffTimeLong");
                   sql.AppendLine(",IsDelay");
                   sql.AppendLine(",IsForwarOff");
                   sql.AppendLine(",EmployeeAttendanceSetID)");
                   sql.AppendLine(" values ");
                   sql.AppendLine("(@CompanyCD");
                   sql.AppendLine(",@EmployeeID     ");
                   sql.AppendLine(",@Date");
                   sql.AppendLine(",@StartTime   ");
                   sql.AppendLine(",@EndTime  ");
                   sql.AppendLine(",@WorkShiftTimeID    ");
                   sql.AppendLine(",@DelayTimeLong    ");
                   sql.AppendLine(",@ForWardOffTimeLong");
                   sql.AppendLine(",@IsDelay");
                   sql.AppendLine(",@IsForwarOff");
                   sql.AppendLine(",@EmployeeAttendanceSetID)");
                   #endregion
                   #region 设置参数
                   SqlParameter[] Params = new SqlParameter[11];
                   Params[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                   Params[1] = SqlHelper.GetParameter("@EmployeeID", EmployeeID);
                   Params[2] = SqlHelper.GetParameter("@Date", AttendanceDate);
                   Params[3] = SqlHelper.GetParameter("@StartTime", StartTime);
                   Params[4] = SqlHelper.GetParameter("@EndTime", EndTime);
                   Params[5] = SqlHelper.GetParameter("@WorkShiftTimeID", WorkShiftTimeID);
                   Params[6] = SqlHelper.GetParameter("@DelayTimeLong", DelayMinute);
                   Params[7] = SqlHelper.GetParameter("@ForWardOffTimeLong", ForwardMinute);
                   Params[8] = SqlHelper.GetParameter("@IsDelay", IsDelay);
                   Params[9] = SqlHelper.GetParameter("@IsForwarOff", IsForward);
                   Params[10] = SqlHelper.GetParameter("@EmployeeAttendanceSetID", EmployeeAttendanceSetID);

                   SqlCommand Command = new SqlCommand(sql.ToString());
                   Command.Parameters.AddRange(Params);
                   comms[i] = Command;
                   #endregion
               }
               //执行
               SqlHelper.ExecuteTransForList(comms);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               return false;
           }
       }
       #endregion
    }
}

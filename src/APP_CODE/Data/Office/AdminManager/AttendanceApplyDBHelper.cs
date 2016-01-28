/**********************************************
 * 类作用：   请假数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/14
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;

namespace XBase.Data.Office.AdminManager
{
    /// <summary>
    /// 类名：AttendanceApplyDBHelper
    /// 描述：请假数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/04/14
    /// </summary>
   public class AttendanceApplyDBHelper
   {
       #region 添加请假信息
        /// <summary>
       /// 添加请假信息
        /// </summary>
       /// <param name="AttendanceApplyM">请假信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InsertAttendanceApplyData(AttendanceApplyModel AttendanceApplyM,out int RetValID)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO officedba.AttendanceApply");
                sql.AppendLine("		(CompanyCD      ");
                sql.AppendLine("		,EmployeeID         ");
                sql.AppendLine("		,ApplyUserID         ");
                sql.AppendLine("		,Flag       ");
                sql.AppendLine("		,ApplyNo       ");
                sql.AppendLine("		,ApplyDate       ");
                sql.AppendLine("		,StartDate       ");
                sql.AppendLine("		,EndDate       ");
                sql.AppendLine("		,StartTime       ");
                sql.AppendLine("		,EndTime       ");
                sql.AppendLine("		,ApplyReason      ");
                sql.AppendLine("		,LeaveType   ");
                sql.AppendLine("		,ModifiedDate        ");
                sql.AppendLine("		,ModifiedUserID)        ");
                sql.AppendLine("VALUES                  ");
                sql.AppendLine("		(@CompanyCD     ");
                sql.AppendLine("		,@EmployeeID        ");
                sql.AppendLine("		,@ApplyUserID        ");
                sql.AppendLine("		,@Flag      ");
                sql.AppendLine("		,@ApplyNo      ");
                sql.AppendLine("		,@ApplyDate      ");
                sql.AppendLine("		,@StartDate      ");
                sql.AppendLine("		,@EndDate      ");
                sql.AppendLine("		,@StartTime      ");
                sql.AppendLine("		,@EndTime      ");
                sql.AppendLine("		,@ApplyReason     ");
                sql.AppendLine("		,@LeaveType  ");
                sql.AppendLine("		,@ModifiedDate       ");
                sql.AppendLine("		,@ModifiedUserID)       ");
                sql.AppendLine("set @ID=@@IDENTITY");
                //设置参数
                SqlParameter[] param;
                param = new SqlParameter[15];
                param[0] = SqlHelper.GetParameter("@CompanyCD", AttendanceApplyM.CompanyCD);
                param[1] = SqlHelper.GetParameter("@EmployeeID", AttendanceApplyM.EmployeeID);
                param[2] = SqlHelper.GetParameter("@Flag", AttendanceApplyM.Flag);
                param[3] = SqlHelper.GetParameter("@ApplyDate", AttendanceApplyM.ApplyDate);
                param[4] = SqlHelper.GetParameter("@StartDate", AttendanceApplyM.StartDate);
                param[5] = SqlHelper.GetParameter("@EndDate", AttendanceApplyM.EndDate);
                param[6] = SqlHelper.GetParameter("@StartTime", AttendanceApplyM.StartTime);
                param[7] = SqlHelper.GetParameter("@EndTime", AttendanceApplyM.EndTime);
                param[8] = SqlHelper.GetParameter("@ApplyReason", AttendanceApplyM.ApplyReason);
                param[9] = SqlHelper.GetParameter("@LeaveType", AttendanceApplyM.LeaveType);
                param[10] = SqlHelper.GetParameter("@ModifiedDate", AttendanceApplyM.ModifiedDate);
                param[11] = SqlHelper.GetParameter("@ModifiedUserID", AttendanceApplyM.ModifiedUserID);
                param[12] = SqlHelper.GetParameter("@ApplyNo", AttendanceApplyM.ApplyNo);
                param[13] = SqlHelper.GetParameter("@ApplyUserID", AttendanceApplyM.ApplyUserID);
                param[14] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);
                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                RetValID = Convert.ToInt32(param[14].Value.ToString());
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch
            {
                RetValID = 0;
                return false;
            }
        }
        #endregion
       #region 查询考勤申请列表
       /// <summary>
       /// 查询
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceApplyInfo(string JoinUser,string ApplyDate, string LeaveType, string ApplyStatus, string Flag, string CompanyID,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           string sql = "";
           string BillTypeCode = "";
           if (Flag == "2") BillTypeCode = "21";
           if (Flag == "3") BillTypeCode = "22";
           if (Flag == "4") BillTypeCode = "23";
           if (Flag == "5") BillTypeCode = "24";
           if (Flag == "6") BillTypeCode = "25";
           if (Flag == "1")
           {
               sql = "select a.ID,a.Flag,a.EmployeeID,CONVERT(VARCHAR(10),a.ApplyDate,120) as ApplyDate,CONVERT(VARCHAR(10),isnull(a.StartDate,'1900-1-1'),120) as StartDate,"
                   + "CONVERT(VARCHAR(10),isnull(a.EndDate,'1900-1-1'),120) as EndDate,"
                   + "CONVERT(VARCHAR(10),isnull(a.BusinessDate,'1900-1-1'),120) as BusinessDate,"
                   + "CONVERT(VARCHAR(10),isnull(a.BusinessPlanDate,'1900-1-1'),120) as BusinessPlanDate,"
                   + "isnull(a.StartTime,'')StartTime,isnull(a.EndTime,'')EndTime,"
                   + "isnull(a.ApplyReason,'')ApplyReason,isnull(a.LeaveType,'')LeaveType,"
                   + "isnull(a.BusinessPlace,'') BusinessPlace,isnull(a.BusinessPeer,'')BusinessPeer,"
                   + "isnull(a.BusinessTransport,'')BusinessTransport,isnull(a.BusinessAdvance,0.00)BusinessAdvance,"
                   + "isnull(a.BusinessRemark,'')BusinessRemark,isnull(OvertimeType,'')OvertimeType,"
                   + "isnull(a.InsteaEmployees,'')InsteaEmployees,isnull(a.InsteadEmployees,'')InsteadEmployees,isnull(a.InsteadStartTime,'')InsteadStartTime,"
                   + "isnull(a.InsteadEndTime,'')InsteadEndTime,"
                   + "b.EmployeeName,b.NowDeptID,b.NowQuarterID,b.ID AS ID1,isnull(b.DeptName,'')DeptName,CASE a.LeaveType WHEN '1' THEN '调休' WHEN '2' THEN '公假'  WHEN '3' THEN '病假'  WHEN '4' THEN '事假'  WHEN '5' THEN '其他' END TypeName,  "
                   + "CASE isnull(d.FlowStatus,0) WHEN 0 THEN '' "
                  + "WHEN 0 THEN '' "
                   + "WHEN 1 THEN '待审批' "
                   + "WHEN 2 THEN '审批中' "
                   + "WHEN 3 THEN '审批通过' "
                   + "WHEN 4 THEN '审批不通过' "
                   + "WHEN 5 THEN '撤销审批' "
                   + "END FlowStatus "
                   + "from officedba.AttendanceApply a LEFT OUTER JOIN  "
                   + "(select m.ID,m.EmployeeName,m.QuarterID as NowQuarterID,m.DeptID as NowDeptID,m.EmployeeNo,l.DeptName,m.CompanyCD "
                   + "from officedba.EmployeeInfo m "
                   + "left outer join officedba.DeptInfo l "
                   + "on m.DeptID=l.ID and m.CompanyCD=l.CompanyCD) b "
                   + "on a.ApplyUserID=b.id and a.CompanyCD=b.CompanyCD "
                   + "LEFT OUTER JOIN "
                   + "(select m.CompanyCD,m.BillNo,max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                   + "where m.CompanyCD='" + CompanyID + "' and m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND m.BillTypeCode='" + ConstUtil.CODING_RULE_ATTEDDANCEAPPLY_LEAVE + "' "
                   + "and  m.BillID=n.ID and m.CompanyCD=n.CompanyCD and m.BillNo=n.ApplyNo "
                   + "group by m.BillID,m.CompanyCD,m.BillNo) e "
                   + "on a.ApplyNO=e.BillNO and a.CompanyCD=e.CompanyCD "
                   + "LEFT OUTER JOIN officedba.FlowInstance d "
                   + "ON  d.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' "
                   + "AND d.BillTypeCode='" + ConstUtil.CODING_RULE_ATTEDDANCEAPPLY_LEAVE + "'  "
                   + " and d.id=e.id and d.CompanyCD=e.CompanyCD "
                   + "WHERE a.Flag='" + Flag + "' AND a.CompanyCD='" + CompanyID + "' ";
           }
           else 
           {
               sql = "select a.ID,a.Flag,a.EmployeeID,CONVERT(VARCHAR(10),a.ApplyDate,120) as ApplyDate,CONVERT(VARCHAR(10),isnull(a.StartDate,'1900-1-1'),120) as StartDate,"
                   + "CONVERT(VARCHAR(10),isnull(a.EndDate,'1900-1-1'),120) as EndDate,"
                   + "CONVERT(VARCHAR(10),isnull(a.BusinessDate,'1900-1-1'),120) as BusinessDate,"
                   + "CONVERT(VARCHAR(10),isnull(a.BusinessPlanDate,'1900-1-1'),120) as BusinessPlanDate,"
                   + "isnull(a.StartTime,'')StartTime,isnull(a.EndTime,'')EndTime,"
                   + "isnull(a.ApplyReason,'')ApplyReason,isnull(a.LeaveType,'')LeaveType,"
                   + "isnull(a.BusinessPlace,'') BusinessPlace,isnull(a.BusinessPeer,'')BusinessPeer,"
                   + "isnull(a.BusinessTransport,'')BusinessTransport,isnull(a.BusinessAdvance,0.00)BusinessAdvance,"
                   + "isnull(a.BusinessRemark,'')BusinessRemark,isnull(OvertimeType,'')OvertimeType,"
                   + "isnull(a.InsteaEmployees,'')InsteaEmployees,isnull(a.InsteadEmployees,'')InsteadEmployees,isnull(a.InsteadStartTime,'')InsteadStartTime,"
                   + "isnull(a.InsteadEndTime,'')InsteadEndTime,"
                   + "b.EmployeeName,b.NowDeptID,b.NowQuarterID,b.ID AS ID1,isnull(b.DeptName,'')DeptName,TypeName='无', "
                   + "CASE isnull(d.FlowStatus,0) WHEN 0 THEN '' "
                  + "WHEN 0 THEN '' "
                   + "WHEN 1 THEN '待审批' "
                   + "WHEN 2 THEN '审批中' "
                   + "WHEN 3 THEN '审批通过' "
                   + "WHEN 4 THEN '审批不通过' "
                   + "WHEN 5 THEN '撤销审批' "
                   + "END FlowStatus "
                   + "from officedba.AttendanceApply a LEFT OUTER JOIN  "
                   + "(select m.ID,m.EmployeeName,m.QuarterID as NowQuarterID,m.DeptID as NowDeptID,m.EmployeeNo,l.DeptName,m.CompanyCD "
                   + "from officedba.EmployeeInfo m "
                   + "left outer join officedba.DeptInfo l "
                   + "on m.DeptID=l.ID AND m.CompanyCD=l.CompanyCD) b "
                   + "on a.ApplyUserID=b.id and a.CompanyCD=b.CompanyCD "
                   + "LEFT OUTER JOIN "
                   + "(select m.CompanyCD,m.BillNo,max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.CompanyCD='" + CompanyID + "' and m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND m.BillTypeCode='" + BillTypeCode + "' "
                    + "and  m.BillID=n.ID and m.CompanyCD=n.CompanyCD and m.BillNo=n.ApplyNo "
                    + "group by m.BillID,m.CompanyCD,m.BillNo) e "
                    + "on a.ApplyNO=e.BillNO and a.CompanyCD=e.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                   + "ON d.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' "
                   + "AND d.BillTypeCode='" + BillTypeCode + "'  "
                   + " and d.id=e.id and d.CompanyCD=e.CompanyCD "
                   + "WHERE Flag='" + Flag + "' AND a.CompanyCD='" + CompanyID + "' ";
           }
           if (JoinUser != "")
               sql += " and b.ID='" + JoinUser + "'";
           //if (ApplyLeaveUser != "")
           //    sql += " and WorkShiftNo LIKE '%" + WorkShiftName + "%'";
           if (ApplyDate != "")
               sql += " and a.ApplyDate='" + ApplyDate + "'";
           if (LeaveType != "")
               sql += " and LeaveType=" + LeaveType + "";
           if (ApplyStatus != "" && ApplyStatus != "0")
               sql += " and d.FlowStatus = '" + ApplyStatus + "'";
           if (ApplyStatus == "0")
               sql += " and d.FlowStatus IS NULL";
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

           //return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 根据ID获取人员请假信息供查看或修改
       /// <summary>
       /// 根据ID获取人员请假信息供查看或修改
       /// </summary>
       /// <param name="ID">人员请假ID</param>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceApplyByID(string ID, string LeaveType)
       {
           var BillTypeCode = "";
           if (LeaveType == "1") BillTypeCode = "20";
           if (LeaveType == "2") BillTypeCode = "21";
           if (LeaveType == "3") BillTypeCode = "22";
           if (LeaveType == "4") BillTypeCode = "23";
           if (LeaveType == "5") BillTypeCode = "24";
           if (LeaveType == "6") BillTypeCode = "25";
           string sql = "select a.*,b.EmployeeName,u.EmployeeName as ApplyUserName, "
                          + "CASE isnull(h.FlowStatus,0) WHEN 0 THEN '' "
                            + "WHEN 1 THEN '待审批' "
                            + " WHEN 2 THEN '审批中' "
                            + "WHEN 3 THEN '审批通过'"
                            + "WHEN 4 THEN '审批不通过' "
                            + " WHEN 5 THEN '撤销审批' "
                            + "END FlowStatus "
                            +"from officedba.AttendanceApply a "
                            +"left outer join officedba.EmployeeInfo b "
                            +"On a.EmployeeID=b.ID "
                            + "left outer join officedba.EmployeeInfo u "
                            + "On a.ApplyUserID=u.ID "
                            + "LEFT OUTER JOIN "
                            + "(select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,officedba.AttendanceApply n  "
                            + "where m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND "
                            + "m.BillTypeCode='" + BillTypeCode + "' and  m.BillID=n.ID and Billid=" + ID + " group by m.BillID,m.BillNo,m.CompanyCD) g "
                            + "on a.ApplyNo=g.BillNo and a.CompanyCD=g.CompanyCD "
                            + "LEFT OUTER JOIN officedba.FlowInstance h "
                            + "ON g.ID=h.ID and g.CompanyCD=h.CompanyCD "
                            +"where a.id="+ID+"";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 修改请假信息
       /// <summary>
       /// 修改请假信息
       /// </summary>
       /// <param name="AttendanceApplyM">请假信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateAttendanceApplyData(AttendanceApplyModel AttendanceApplyM, string ID)
       {
           try
           {
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("Update officedba.AttendanceApply");
               sql.AppendLine("	 SET CompanyCD=@CompanyCD     ");
               //sql.AppendLine("		,EmployeeID=@EmployeeID         ");
               sql.AppendLine("		,Flag=@Flag       ");
               sql.AppendLine("		,ApplyDate=@ApplyDate       ");
               sql.AppendLine("		,StartDate=@StartDate       ");
               sql.AppendLine("		,EndDate=@EndDate       ");
               sql.AppendLine("		,StartTime=@StartTime       ");
               sql.AppendLine("		,EndTime=@EndTime       ");
               sql.AppendLine("		,ApplyReason=@ApplyReason      ");
               sql.AppendLine("		,LeaveType=@LeaveType   ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID      ");
               sql.AppendLine("		where id="+ID+"      ");
               //设置参数
               SqlParameter[] param;
               param = new SqlParameter[11];
               param[0] = SqlHelper.GetParameter("@CompanyCD", AttendanceApplyM.CompanyCD);
              // param[1] = SqlHelper.GetParameter("@EmployeeID", AttendanceApplyM.EmployeeID);
               param[1] = SqlHelper.GetParameter("@Flag", AttendanceApplyM.Flag);
               param[2] = SqlHelper.GetParameter("@ApplyDate", AttendanceApplyM.ApplyDate);
               param[3] = SqlHelper.GetParameter("@StartDate", AttendanceApplyM.StartDate);
               param[4] = SqlHelper.GetParameter("@EndDate", AttendanceApplyM.EndDate);
               param[5] = SqlHelper.GetParameter("@StartTime", AttendanceApplyM.StartTime);
               param[6] = SqlHelper.GetParameter("@EndTime", AttendanceApplyM.EndTime);
               param[7] = SqlHelper.GetParameter("@ApplyReason", AttendanceApplyM.ApplyReason);
               param[8] = SqlHelper.GetParameter("@LeaveType", AttendanceApplyM.LeaveType);
               param[9] = SqlHelper.GetParameter("@ModifiedDate", AttendanceApplyM.ModifiedDate);
               param[10] = SqlHelper.GetParameter("@ModifiedUserID", AttendanceApplyM.ModifiedUserID);
               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               return false;
           }
       }
       #endregion

       #region 添加加班信息
       /// <summary>
       /// 添加加班信息
       /// </summary>
       /// <param name="AttendanceApplyM">加班信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InsertOverTimeData(AttendanceApplyModel AttendanceApplyM,out int RetValID)
       {
           try
           {
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("INSERT INTO officedba.AttendanceApply");
               sql.AppendLine("		(CompanyCD      ");
               sql.AppendLine("		,EmployeeID         ");
               sql.AppendLine("		,ApplyUserID         ");
               sql.AppendLine("		,Flag       ");
               sql.AppendLine("		,ApplyNo       ");
               sql.AppendLine("		,ApplyDate       ");
               sql.AppendLine("		,StartDate       ");
               sql.AppendLine("		,EndDate       ");
               sql.AppendLine("		,StartTime       ");
               sql.AppendLine("		,EndTime       ");
               sql.AppendLine("		,BusinessPeer      ");
               sql.AppendLine("		,OvertimeType   ");
               sql.AppendLine("		,ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID)        ");
               sql.AppendLine("VALUES                  ");
               sql.AppendLine("		(@CompanyCD     ");
               sql.AppendLine("		,@EmployeeID        ");
               sql.AppendLine("		,@ApplyUserID         ");
               sql.AppendLine("		,@Flag      ");
               sql.AppendLine("		,@ApplyNo       ");
               sql.AppendLine("		,@ApplyDate      ");
               sql.AppendLine("		,@StartDate      ");
               sql.AppendLine("		,@EndDate      ");
               sql.AppendLine("		,@StartTime      ");
               sql.AppendLine("		,@EndTime      ");
               sql.AppendLine("		,@BusinessPeer     ");
               sql.AppendLine("		,@OvertimeType  ");
               sql.AppendLine("		,@ModifiedDate       ");
               sql.AppendLine("		,@ModifiedUserID)       ");
               sql.AppendLine("set @ID=@@IDENTITY");

               //设置参数
               SqlParameter[] param;
               param = new SqlParameter[15];
               param[0] = SqlHelper.GetParameter("@CompanyCD", AttendanceApplyM.CompanyCD);
               param[1] = SqlHelper.GetParameter("@EmployeeID", AttendanceApplyM.EmployeeID);
               param[2] = SqlHelper.GetParameter("@Flag", AttendanceApplyM.Flag);
               param[3] = SqlHelper.GetParameter("@ApplyDate", AttendanceApplyM.ApplyDate);
               param[4] = SqlHelper.GetParameter("@StartDate", AttendanceApplyM.StartDate);
               param[5] = SqlHelper.GetParameter("@EndDate", AttendanceApplyM.EndDate);
               param[6] = SqlHelper.GetParameter("@StartTime", AttendanceApplyM.StartTime);
               param[7] = SqlHelper.GetParameter("@EndTime", AttendanceApplyM.EndTime);
               param[8] = SqlHelper.GetParameter("@BusinessPeer", AttendanceApplyM.BusinessPeer);
               param[9] = SqlHelper.GetParameter("@OvertimeType", AttendanceApplyM.OvertimeType);
               param[10] = SqlHelper.GetParameter("@ModifiedDate", AttendanceApplyM.ModifiedDate);
               param[11] = SqlHelper.GetParameter("@ModifiedUserID", AttendanceApplyM.ModifiedUserID);
               param[12] = SqlHelper.GetParameter("@ApplyNo", AttendanceApplyM.ApplyNo);
               param[13] = SqlHelper.GetParameter("@ApplyUserID", AttendanceApplyM.ApplyUserID);
               param[14] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);

               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               RetValID = Convert.ToInt32(param[14].Value.ToString());
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               RetValID = 0;
               return false;
           }
       }
       #endregion

       #region 修改加班信息
       /// <summary>
       /// 修改加班信息
       /// </summary>
       /// <param name="AttendanceApplyM">加班信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateOverTimeData(AttendanceApplyModel AttendanceApplyM, string ID)
       {
           try
           {
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.AttendanceApply");
               sql.AppendLine("	Set	CompanyCD=@CompanyCD      ");
              // sql.AppendLine("		,EmployeeID=@EmployeeID         ");
               sql.AppendLine("		,Flag=@Flag       ");
               sql.AppendLine("		,ApplyDate=@ApplyDate       ");
               sql.AppendLine("		,StartDate=@StartDate       ");
               sql.AppendLine("		,EndDate=@EndDate       ");
               sql.AppendLine("		,StartTime=@StartTime       ");
               sql.AppendLine("		,EndTime=@EndTime       ");
               sql.AppendLine("		,BusinessPeer=@BusinessPeer      ");
               sql.AppendLine("		,OvertimeType=@OvertimeType   ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
               sql.AppendLine("		where id=" + ID + "      ");

               //设置参数
               SqlParameter[] param;
               param = new SqlParameter[11];
               param[0] = SqlHelper.GetParameter("@CompanyCD", AttendanceApplyM.CompanyCD);
              // param[1] = SqlHelper.GetParameter("@EmployeeID", AttendanceApplyM.EmployeeID);
               param[1] = SqlHelper.GetParameter("@Flag", AttendanceApplyM.Flag);
               param[2] = SqlHelper.GetParameter("@ApplyDate", AttendanceApplyM.ApplyDate);
               param[3] = SqlHelper.GetParameter("@StartDate", AttendanceApplyM.StartDate);
               param[4] = SqlHelper.GetParameter("@EndDate", AttendanceApplyM.EndDate);
               param[5] = SqlHelper.GetParameter("@StartTime", AttendanceApplyM.StartTime);
               param[6] = SqlHelper.GetParameter("@EndTime", AttendanceApplyM.EndTime);
               param[7] = SqlHelper.GetParameter("@BusinessPeer", AttendanceApplyM.BusinessPeer);
               param[8] = SqlHelper.GetParameter("@OvertimeType", AttendanceApplyM.OvertimeType);
               param[9] = SqlHelper.GetParameter("@ModifiedDate", AttendanceApplyM.ModifiedDate);
               param[10] = SqlHelper.GetParameter("@ModifiedUserID", AttendanceApplyM.ModifiedUserID);
               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               return false;
           }
       }
       #endregion

       #region 添加外出信息
       /// <summary>
       /// 添加外出信息
       /// </summary>
       /// <param name="AttendanceApplyM">外出信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InsertBeOutData(AttendanceApplyModel AttendanceApplyM,out int RetValID)
       {
           try
           {
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("INSERT INTO officedba.AttendanceApply");
               sql.AppendLine("		(CompanyCD      ");
               sql.AppendLine("		,EmployeeID         ");
               sql.AppendLine("		,ApplyUserID         ");
               sql.AppendLine("		,Flag       ");
               sql.AppendLine("		,ApplyNo       ");
               sql.AppendLine("		,ApplyDate       ");
               sql.AppendLine("		,StartDate       ");
               sql.AppendLine("		,EndDate       ");
               sql.AppendLine("		,StartTime       ");
               sql.AppendLine("		,EndTime       ");
               sql.AppendLine("		,BusinessPlace      ");
               sql.AppendLine("		,ApplyReason   ");
               sql.AppendLine("		,ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID)        ");
               sql.AppendLine("VALUES                  ");
               sql.AppendLine("		(@CompanyCD     ");
               sql.AppendLine("		,@EmployeeID        ");
               sql.AppendLine("		,@ApplyUserID         ");
               sql.AppendLine("		,@Flag      ");
               sql.AppendLine("		,@ApplyNo       ");
               sql.AppendLine("		,@ApplyDate      ");
               sql.AppendLine("		,@StartDate      ");
               sql.AppendLine("		,@EndDate      ");
               sql.AppendLine("		,@StartTime      ");
               sql.AppendLine("		,@EndTime      ");
               sql.AppendLine("		,@BusinessPlace     ");
               sql.AppendLine("		,@ApplyReason  ");
               sql.AppendLine("		,@ModifiedDate       ");
               sql.AppendLine("		,@ModifiedUserID)       ");
               sql.AppendLine("set @ID=@@IDENTITY");

               //设置参数
               SqlParameter[] param;
               param = new SqlParameter[15];
               param[0] = SqlHelper.GetParameter("@CompanyCD", AttendanceApplyM.CompanyCD);
               param[1] = SqlHelper.GetParameter("@EmployeeID", AttendanceApplyM.EmployeeID);
               param[2] = SqlHelper.GetParameter("@Flag", AttendanceApplyM.Flag);
               param[3] = SqlHelper.GetParameter("@ApplyDate", AttendanceApplyM.ApplyDate);
               param[4] = SqlHelper.GetParameter("@StartDate", AttendanceApplyM.StartDate);
               param[5] = SqlHelper.GetParameter("@EndDate", AttendanceApplyM.EndDate);
               param[6] = SqlHelper.GetParameter("@StartTime", AttendanceApplyM.StartTime);
               param[7] = SqlHelper.GetParameter("@EndTime", AttendanceApplyM.EndTime);
               param[8] = SqlHelper.GetParameter("@BusinessPlace", AttendanceApplyM.BusinessPlace);
               param[9] = SqlHelper.GetParameter("@ApplyReason", AttendanceApplyM.ApplyReason);
               param[10] = SqlHelper.GetParameter("@ModifiedDate", AttendanceApplyM.ModifiedDate);
               param[11] = SqlHelper.GetParameter("@ModifiedUserID", AttendanceApplyM.ModifiedUserID);
               param[12] = SqlHelper.GetParameter("@ApplyNo", AttendanceApplyM.ApplyNo);
               param[13] = SqlHelper.GetParameter("@ApplyUserID", AttendanceApplyM.ApplyUserID);
               param[14] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);

               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               RetValID = Convert.ToInt32(param[14].Value.ToString());
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               RetValID = 0;
               return false;
           }
       }
       #endregion

       #region 修改外出信息
       /// <summary>
       /// 修改外出信息
       /// </summary>
       /// <param name="AttendanceApplyM">外出信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateBeOutData(AttendanceApplyModel AttendanceApplyM, string ID)
       {
           try
           {
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.AttendanceApply");
               sql.AppendLine("	Set	CompanyCD=@CompanyCD      ");
               //sql.AppendLine("		,EmployeeID=@EmployeeID         ");
               sql.AppendLine("		,Flag=@Flag       ");
               sql.AppendLine("		,ApplyDate=@ApplyDate       ");
               sql.AppendLine("		,StartDate=@StartDate       ");
               sql.AppendLine("		,EndDate=@EndDate       ");
               sql.AppendLine("		,StartTime=@StartTime       ");
               sql.AppendLine("		,EndTime=@EndTime       ");
               sql.AppendLine("		,BusinessPlace=@BusinessPlace      ");
               sql.AppendLine("		,ApplyReason=@ApplyReason   ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
               sql.AppendLine("		where id=" + ID + "      ");

               //设置参数
               SqlParameter[] param;
               param = new SqlParameter[11];
               param[0] = SqlHelper.GetParameter("@CompanyCD", AttendanceApplyM.CompanyCD);
              // param[1] = SqlHelper.GetParameter("@EmployeeID", AttendanceApplyM.EmployeeID);
               param[1] = SqlHelper.GetParameter("@Flag", AttendanceApplyM.Flag);
               param[2] = SqlHelper.GetParameter("@ApplyDate", AttendanceApplyM.ApplyDate);
               param[3] = SqlHelper.GetParameter("@StartDate", AttendanceApplyM.StartDate);
               param[4] = SqlHelper.GetParameter("@EndDate", AttendanceApplyM.EndDate);
               param[5] = SqlHelper.GetParameter("@StartTime", AttendanceApplyM.StartTime);
               param[6] = SqlHelper.GetParameter("@EndTime", AttendanceApplyM.EndTime);
               param[7] = SqlHelper.GetParameter("@BusinessPlace", AttendanceApplyM.BusinessPlace);
               param[8] = SqlHelper.GetParameter("@ApplyReason", AttendanceApplyM.ApplyReason);
               param[9] = SqlHelper.GetParameter("@ModifiedDate", AttendanceApplyM.ModifiedDate);
               param[10] = SqlHelper.GetParameter("@ModifiedUserID", AttendanceApplyM.ModifiedUserID);
               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               return false;
           }
       }
       #endregion

       #region 添加出差信息
       /// <summary>
       /// 添加出差信息
       /// </summary>
       /// <param name="AttendanceApplyM">出差信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InsertBusinessData(AttendanceApplyModel AttendanceApplyM,out int RetValID)
       {
           try
           {
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("INSERT INTO officedba.AttendanceApply");
               sql.AppendLine("		(CompanyCD      ");
               sql.AppendLine("		,EmployeeID         ");
               sql.AppendLine("		,ApplyUserID         ");
               sql.AppendLine("		,Flag       ");
               sql.AppendLine("		,ApplyNo       ");
               sql.AppendLine("		,ApplyDate       ");
               sql.AppendLine("		,BusinessDate       ");
               sql.AppendLine("		,BusinessPlanDate       ");
               sql.AppendLine("		,BusinessPlace       ");
               sql.AppendLine("		,BusinessTransport       ");
               sql.AppendLine("		,BusinessAdvance      ");
               sql.AppendLine("		,ApplyReason   ");
               sql.AppendLine("		,BusinessPeer       ");
               sql.AppendLine("		,BusinessRemark      ");
               sql.AppendLine("		,ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID)        ");
               sql.AppendLine("VALUES                  ");
               sql.AppendLine("		(@CompanyCD     ");
               sql.AppendLine("		,@EmployeeID        ");
               sql.AppendLine("		,@ApplyUserID         ");
               sql.AppendLine("		,@Flag      ");
               sql.AppendLine("		,@ApplyNo       ");
               sql.AppendLine("		,@ApplyDate      ");
               sql.AppendLine("		,@BusinessDate      ");
               sql.AppendLine("		,@BusinessPlanDate      ");
               sql.AppendLine("		,@BusinessPlace      ");
               sql.AppendLine("		,@BusinessTransport      ");
               sql.AppendLine("		,@BusinessAdvance     ");
               sql.AppendLine("		,@ApplyReason  ");
               sql.AppendLine("		,@BusinessPeer     ");
               sql.AppendLine("		,@BusinessRemark  ");
               sql.AppendLine("		,@ModifiedDate       ");
               sql.AppendLine("		,@ModifiedUserID)       ");
               sql.AppendLine("set @ID=@@IDENTITY");

               //设置参数
               SqlParameter[] param;
               param = new SqlParameter[17];
               param[0] = SqlHelper.GetParameter("@CompanyCD", AttendanceApplyM.CompanyCD);
               param[1] = SqlHelper.GetParameter("@EmployeeID", AttendanceApplyM.EmployeeID);
               param[2] = SqlHelper.GetParameter("@Flag", AttendanceApplyM.Flag);
               param[3] = SqlHelper.GetParameter("@ApplyDate", AttendanceApplyM.ApplyDate);
               param[4] = SqlHelper.GetParameter("@BusinessDate", AttendanceApplyM.BusinessDate);
               param[5] = SqlHelper.GetParameter("@BusinessPlanDate", AttendanceApplyM.BusinessPlanDate);
               param[6] = SqlHelper.GetParameter("@BusinessPlace", AttendanceApplyM.BusinessPlace);
               param[7] = SqlHelper.GetParameter("@BusinessTransport", AttendanceApplyM.BusinessTransport);
               param[8] = SqlHelper.GetParameter("@BusinessAdvance", AttendanceApplyM.BusinessAdvance);
               param[9] = SqlHelper.GetParameter("@ApplyReason", AttendanceApplyM.ApplyReason);
               param[10] = SqlHelper.GetParameter("@BusinessPeer", AttendanceApplyM.BusinessPeer);
               param[11] = SqlHelper.GetParameter("@BusinessRemark", AttendanceApplyM.BusinessRemark);
               param[12] = SqlHelper.GetParameter("@ModifiedDate", AttendanceApplyM.ModifiedDate);
               param[13] = SqlHelper.GetParameter("@ModifiedUserID", AttendanceApplyM.ModifiedUserID);
               param[14] = SqlHelper.GetParameter("@ApplyNo", AttendanceApplyM.ApplyNo);
               param[15] = SqlHelper.GetParameter("@ApplyUserID", AttendanceApplyM.ApplyUserID);
               param[16] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);

               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               RetValID = Convert.ToInt32(param[16].Value.ToString());
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               RetValID = 0;
               return false;
           }
       }
       #endregion

       #region 修改出差信息
       /// <summary>
       /// 修改出差信息
       /// </summary>
       /// <param name="AttendanceApplyM">出差信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateBusinessData(AttendanceApplyModel AttendanceApplyM, string ID)
       {
           try
           {
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.AttendanceApply");
               sql.AppendLine("	Set	CompanyCD=@CompanyCD      ");
               //sql.AppendLine("		,EmployeeID=@EmployeeID         ");
               sql.AppendLine("		,Flag=@Flag       ");
               sql.AppendLine("		,ApplyDate=@ApplyDate       ");
               sql.AppendLine("		,BusinessDate=@BusinessDate       ");
               sql.AppendLine("		,BusinessPlanDate=@BusinessPlanDate       ");
               sql.AppendLine("		,BusinessPlace=@BusinessPlace       ");
               sql.AppendLine("		,BusinessTransport=@BusinessTransport       ");
               sql.AppendLine("		,BusinessAdvance=@BusinessAdvance      ");
               sql.AppendLine("		,ApplyReason=@ApplyReason   ");
               sql.AppendLine("		,BusinessPeer=@BusinessPeer       ");
               sql.AppendLine("		,BusinessRemark=@BusinessRemark      ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        "); 
               sql.AppendLine("		where id=" + ID + "      ");

               //设置参数
               SqlParameter[] param;
               param = new SqlParameter[13];
               param[0] = SqlHelper.GetParameter("@CompanyCD", AttendanceApplyM.CompanyCD);
              // param[1] = SqlHelper.GetParameter("@EmployeeID", AttendanceApplyM.EmployeeID);
               param[1] = SqlHelper.GetParameter("@Flag", AttendanceApplyM.Flag);
               param[2] = SqlHelper.GetParameter("@ApplyDate", AttendanceApplyM.ApplyDate);
               param[3] = SqlHelper.GetParameter("@BusinessDate", AttendanceApplyM.BusinessDate);
               param[4] = SqlHelper.GetParameter("@BusinessPlanDate", AttendanceApplyM.BusinessPlanDate);
               param[5] = SqlHelper.GetParameter("@BusinessPlace", AttendanceApplyM.BusinessPlace);
               param[6] = SqlHelper.GetParameter("@BusinessTransport", AttendanceApplyM.BusinessTransport);
               param[7] = SqlHelper.GetParameter("@BusinessAdvance", AttendanceApplyM.BusinessAdvance);
               param[8] = SqlHelper.GetParameter("@ApplyReason", AttendanceApplyM.ApplyReason);
               param[9] = SqlHelper.GetParameter("@BusinessPeer", AttendanceApplyM.BusinessPeer);
               param[10] = SqlHelper.GetParameter("@BusinessRemark", AttendanceApplyM.BusinessRemark);
               param[11] = SqlHelper.GetParameter("@ModifiedDate", AttendanceApplyM.ModifiedDate);
               param[12] = SqlHelper.GetParameter("@ModifiedUserID", AttendanceApplyM.ModifiedUserID);
               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               return false;
           }
       }
       #endregion

       #region 添加替班信息
       /// <summary>
       /// 添加替班信息
       /// </summary>
       /// <param name="AttendanceApplyM">替班信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InsertInsteadData(AttendanceApplyModel AttendanceApplyM,out int RetValID)
       {
           try
           {
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("INSERT INTO officedba.AttendanceApply");
               sql.AppendLine("		(CompanyCD      ");
               sql.AppendLine("		,EmployeeID         ");
               sql.AppendLine("		,ApplyUserID         ");
               sql.AppendLine("		,Flag       ");
               sql.AppendLine("		,ApplyNo       ");
               sql.AppendLine("		,ApplyDate       ");
               sql.AppendLine("		,InsteaEmployees       ");
               sql.AppendLine("		,InsteadEmployees       ");
               sql.AppendLine("		,StartDate       ");
               sql.AppendLine("		,InsteadStartTime       ");
               sql.AppendLine("		,EndDate      ");
               sql.AppendLine("		,InsteadEndTime   ");
               sql.AppendLine("		,ApplyReason       ");
               sql.AppendLine("		,ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID)        ");
               sql.AppendLine("VALUES                  ");
               sql.AppendLine("		(@CompanyCD     ");
               sql.AppendLine("		,@EmployeeID        ");
               sql.AppendLine("		,@ApplyUserID         ");
               sql.AppendLine("		,@Flag      ");
               sql.AppendLine("		,@ApplyNo       ");
               sql.AppendLine("		,@ApplyDate      ");
               sql.AppendLine("		,@InsteaEmployees      ");
               sql.AppendLine("		,@InsteadEmployees      ");
               sql.AppendLine("		,@StartDate      ");
               sql.AppendLine("		,@InsteadStartTime      ");
               sql.AppendLine("		,@EndDate     ");
               sql.AppendLine("		,@InsteadEndTime  ");
               sql.AppendLine("		,@ApplyReason     ");
               sql.AppendLine("		,@ModifiedDate       ");
               sql.AppendLine("		,@ModifiedUserID)       ");
               sql.AppendLine("set @ID=@@IDENTITY");


               //设置参数
               SqlParameter[] param;
               param = new SqlParameter[16];
               param[0] = SqlHelper.GetParameter("@CompanyCD", AttendanceApplyM.CompanyCD);
               param[1] = SqlHelper.GetParameter("@EmployeeID", AttendanceApplyM.EmployeeID);
               param[2] = SqlHelper.GetParameter("@Flag", AttendanceApplyM.Flag);
               param[3] = SqlHelper.GetParameter("@ApplyDate", AttendanceApplyM.ApplyDate);
               param[4] = SqlHelper.GetParameter("@InsteaEmployees", AttendanceApplyM.InsteaEmployees);
               param[5] = SqlHelper.GetParameter("@InsteadEmployees", AttendanceApplyM.InsteadEmployees);
               param[6] = SqlHelper.GetParameter("@StartDate", AttendanceApplyM.StartDate);
               param[7] = SqlHelper.GetParameter("@InsteadStartTime", AttendanceApplyM.InsteadStartTime);
               param[8] = SqlHelper.GetParameter("@EndDate", AttendanceApplyM.EndDate);
               param[9] = SqlHelper.GetParameter("@InsteadEndTime", AttendanceApplyM.InsteadEndTime);
               param[10] = SqlHelper.GetParameter("@ApplyReason", AttendanceApplyM.ApplyReason);
               param[11] = SqlHelper.GetParameter("@ModifiedDate", AttendanceApplyM.ModifiedDate);
               param[12] = SqlHelper.GetParameter("@ModifiedUserID", AttendanceApplyM.ModifiedUserID);
               param[13] = SqlHelper.GetParameter("@ApplyNo", AttendanceApplyM.ApplyNo);
               param[14] = SqlHelper.GetParameter("@ApplyUserID", AttendanceApplyM.ApplyUserID);
               param[15] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);

               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               RetValID = Convert.ToInt32(param[15].Value.ToString());
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               RetValID = 0;
               return false;
           }
       }
       #endregion

       #region 修改替班信息
       /// <summary>
       /// 修改替班信息
       /// </summary>
       /// <param name="AttendanceApplyM">替班信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateInsteadData(AttendanceApplyModel AttendanceApplyM, string ID)
       {
           try
           {
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.AttendanceApply");
               sql.AppendLine("	Set	CompanyCD=@CompanyCD      ");
               //sql.AppendLine("		,EmployeeID=@EmployeeID         ");
               sql.AppendLine("		,Flag=@Flag       ");
               sql.AppendLine("		,ApplyDate=@ApplyDate       ");
               sql.AppendLine("		,InsteaEmployees=@InsteaEmployees       ");
               sql.AppendLine("		,InsteadEmployees=@InsteadEmployees       ");
               sql.AppendLine("		,StartDate=@StartDate       ");
               sql.AppendLine("		,InsteadStartTime=@InsteadStartTime       ");
               sql.AppendLine("		,EndDate=@EndDate      ");
               sql.AppendLine("		,InsteadEndTime=@InsteadEndTime   ");
               sql.AppendLine("		,ApplyReason=@ApplyReason       ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
               sql.AppendLine("		where id=" + ID + "      ");

               //设置参数
               SqlParameter[] param;
               param = new SqlParameter[12];
               param[0] = SqlHelper.GetParameter("@CompanyCD", AttendanceApplyM.CompanyCD);
               //param[1] = SqlHelper.GetParameter("@EmployeeID", AttendanceApplyM.EmployeeID);
               param[1] = SqlHelper.GetParameter("@Flag", AttendanceApplyM.Flag);
               param[2] = SqlHelper.GetParameter("@ApplyDate", AttendanceApplyM.ApplyDate);
               param[3] = SqlHelper.GetParameter("@InsteaEmployees", AttendanceApplyM.InsteaEmployees);
               param[4] = SqlHelper.GetParameter("@InsteadEmployees", AttendanceApplyM.InsteadEmployees);
               param[5] = SqlHelper.GetParameter("@StartDate", AttendanceApplyM.StartDate);
               param[6] = SqlHelper.GetParameter("@InsteadStartTime", AttendanceApplyM.InsteadStartTime);
               param[7] = SqlHelper.GetParameter("@EndDate", AttendanceApplyM.EndDate);
               param[8] = SqlHelper.GetParameter("@InsteadEndTime", AttendanceApplyM.InsteadEndTime);
               param[9] = SqlHelper.GetParameter("@ApplyReason", AttendanceApplyM.ApplyReason);
               param[10] = SqlHelper.GetParameter("@ModifiedDate", AttendanceApplyM.ModifiedDate);
               param[11] = SqlHelper.GetParameter("@ModifiedUserID", AttendanceApplyM.ModifiedUserID);
               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               return false;
           }
       }
       #endregion

       #region 添加年休信息
       /// <summary>
       /// 添加年休信息
       /// </summary>
       /// <param name="AttendanceApplyM">年休信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InsertYearHolidayData(AttendanceApplyModel AttendanceApplyM,out int RetValID)
       {
           try
           {
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("INSERT INTO officedba.AttendanceApply");
               sql.AppendLine("		(CompanyCD      ");
               sql.AppendLine("		,EmployeeID         ");
               sql.AppendLine("		,ApplyUserID         ");
               sql.AppendLine("		,Flag       ");
               sql.AppendLine("		,ApplyNo       ");
               sql.AppendLine("		,ApplyDate       ");
               sql.AppendLine("		,StartDate       ");
               sql.AppendLine("		,EndDate       ");
               sql.AppendLine("		,StartTime      ");
               sql.AppendLine("		,EndTime   ");
               sql.AppendLine("		,ApplyReason       ");
               sql.AppendLine("		,ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID)        ");
               sql.AppendLine("VALUES                  ");
               sql.AppendLine("		(@CompanyCD     ");
               sql.AppendLine("		,@EmployeeID        ");
               sql.AppendLine("		,@ApplyUserID         ");
               sql.AppendLine("		,@Flag      ");
               sql.AppendLine("		,@ApplyNo       ");
               sql.AppendLine("		,@ApplyDate      ");
               sql.AppendLine("		,@StartDate      ");
               sql.AppendLine("		,@EndDate      ");
               sql.AppendLine("		,@StartTime     ");
               sql.AppendLine("		,@EndTime  ");
               sql.AppendLine("		,@ApplyReason     ");
               sql.AppendLine("		,@ModifiedDate       ");
               sql.AppendLine("		,@ModifiedUserID)       ");
               sql.AppendLine("set @ID=@@IDENTITY");

               //设置参数
               SqlParameter[] param;
               param = new SqlParameter[14];
               param[0] = SqlHelper.GetParameter("@CompanyCD", AttendanceApplyM.CompanyCD);
               param[1] = SqlHelper.GetParameter("@EmployeeID", AttendanceApplyM.EmployeeID);
               param[2] = SqlHelper.GetParameter("@Flag", AttendanceApplyM.Flag);
               param[3] = SqlHelper.GetParameter("@ApplyDate", AttendanceApplyM.ApplyDate);
               param[4] = SqlHelper.GetParameter("@StartDate", AttendanceApplyM.StartDate);
               param[5] = SqlHelper.GetParameter("@EndDate", AttendanceApplyM.EndDate);
               param[6] = SqlHelper.GetParameter("@StartTime", AttendanceApplyM.StartTime);
               param[7] = SqlHelper.GetParameter("@EndTime", AttendanceApplyM.EndTime);
               param[8] = SqlHelper.GetParameter("@ApplyReason", AttendanceApplyM.ApplyReason);
               param[9] = SqlHelper.GetParameter("@ModifiedDate", AttendanceApplyM.ModifiedDate);
               param[10] = SqlHelper.GetParameter("@ModifiedUserID", AttendanceApplyM.ModifiedUserID);
               param[11] = SqlHelper.GetParameter("@ApplyNo", AttendanceApplyM.ApplyNo);
               param[12] = SqlHelper.GetParameter("@ApplyUserID", AttendanceApplyM.ApplyUserID);
               param[13] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);

               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               RetValID = Convert.ToInt32(param[13].Value.ToString());
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               RetValID = 0;
               return false;
           }
       }
       #endregion

       #region 修改年休信息
       /// <summary>
       /// 修改年休信息
       /// </summary>
       /// <param name="AttendanceApplyM">年休信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateYearHolidayApplyData(AttendanceApplyModel AttendanceApplyM, string ID)
       {
           try
           {
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.AttendanceApply");
               sql.AppendLine("	Set	CompanyCD=@CompanyCD      ");
               //sql.AppendLine("		,EmployeeID=@EmployeeID         ");
               sql.AppendLine("		,ApplyDate=@ApplyDate       ");
               sql.AppendLine("		,StartDate=@StartDate       ");
               sql.AppendLine("		,EndDate=@EndDate       ");
               sql.AppendLine("		,StartTime=@StartTime      ");
               sql.AppendLine("		,EndTime=@EndTime   ");
               sql.AppendLine("		,ApplyReason=@ApplyReason       ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
               sql.AppendLine("		where id=" + ID + "      ");

               //设置参数
               SqlParameter[] param;
               param = new SqlParameter[10];
               param[0] = SqlHelper.GetParameter("@CompanyCD", AttendanceApplyM.CompanyCD);
               //param[1] = SqlHelper.GetParameter("@EmployeeID", AttendanceApplyM.EmployeeID);
               param[1] = SqlHelper.GetParameter("@Flag", AttendanceApplyM.Flag);
               param[2] = SqlHelper.GetParameter("@ApplyDate", AttendanceApplyM.ApplyDate);
               param[3] = SqlHelper.GetParameter("@StartDate", AttendanceApplyM.StartDate);
               param[4] = SqlHelper.GetParameter("@EndDate", AttendanceApplyM.EndDate);
               param[5] = SqlHelper.GetParameter("@StartTime", AttendanceApplyM.StartTime);
               param[6] = SqlHelper.GetParameter("@EndTime", AttendanceApplyM.EndTime);
               param[7] = SqlHelper.GetParameter("@ApplyReason", AttendanceApplyM.ApplyReason);
               param[8] = SqlHelper.GetParameter("@ModifiedDate", AttendanceApplyM.ModifiedDate);
               param[9] = SqlHelper.GetParameter("@ModifiedUserID", AttendanceApplyM.ModifiedUserID);
               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               return false;
           }
       }
       #endregion

       #region 查询异常列表
       /// <summary>
       /// 查询
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetExceptionInfo(string ApplyDate, string ApplyEndDate, string EmployeeNo, string EmployeeName, string Quarter, string CompanyID)
       {
           string sql = "SELECT X.ID,X.EmployeeName,X.EmployeeNo,X.EveryDay,isnull(Y.WorkShiftTimeID,'')WorkShiftTimeID,isnull(Y.Date,'')Date,isnull(Y.StartTime,'')StartTime,"
                       + "isnull(Y.EndTime,'')EndTime,isnull(Y.DelayTimeLong,0)DelayTimeLong,isnull(Y.ForWardOffTimeLong,0)ForWardOffTimeLong,"
                       + "isnull(Y.IsDelay,'-')IsDelay,isnull(Y.IsForwarOff,'-')IsForwarOff,"
                       + "isnull(F.TotalDelayTimeLong,0)TotalDelayTimeLong,isnull(F.TotalForWardOffiTimeLong,0)TotalForWardOffiTimeLong,"
                       +"isnull(J.DeptName,'')DeptName,isnull(K.QuarterName,'')QuarterName,"
                       + "isnull(H.ShiftTimeName,'')ShiftTimeName,isnull(H.Ontime,'')Ontime,isnull(H.OnlateTime,'')OnlateTime,"
                       + "isnull(H.IfAttendance,'')IfAttendance,isnull(H.OffTime,'')OffTime,isnull(H.OffForwordTime,'')OffForwordTime,"
                       + "isnull(I.LateAbsent,'')LateAbsent,isnull(I.ForwardAbsent,'')ForwardAbsent,"
                       + "isnull(G.AttendanceType,'')AttendanceType,isnull(G.WeekRestDay,'')WeekRestDay,isnull(G.MonthRestDay,'')MonthRestDay  "
                       + "FROM "
                       + "(SELECT A.*,B.EveryDay "
                       + "FROM "
                       + "(select distinct b.ID,b.EmployeeName,b.EmployeeNo,b.CompanyCD,b.DeptID,b.QuarterID from officedba.EmployeeAttendanceSet a "
                       + " LEFT OUTER JOIN officedba.EmployeeInfo b "
                       + "ON a.EmployeeID=b.ID WHERE b.Flag='1') A "
                       + "CROSS JOIN officedba.AttendanceEveryDay B "
                       + "WHERE 1=1  ";
           if (ApplyDate != "")
               sql += " and B.EveryDay>='" + ApplyDate + "'";
           if (ApplyEndDate != "")
               sql += " and B.EveryDay<='" + ApplyEndDate + "'";
           sql += " ) X LEFT OUTER JOIN "
            + "officedba.DailyAttendance Y "
            + "ON X.ID=Y.EmployeeID AND X.EveryDay=Y.Date "
            + "LEFT OUTER JOIN "
            + "("
            + "select EmployeeID,Date,Sum(DelayTimeLong)TotalDelayTimeLong,SUM(ForWardOffTimeLong)TotalForWardOffiTimeLong from officedba.DailyAttendance "
            + "GROUP BY EmployeeID,Date "
            + ") F "
            + "ON X.EveryDay=F.Date AND X.ID=F.EmployeeID "
            + "LEFT OUTER JOIN officedba.EmployeeAttendanceSet G "
            + "ON X.ID=G.EmployeeID and Y.EmployeeAttendanceSetID=G.ID "
            + "AND (G.AttendanceType='0' or G.AttendanceType is null) "
            + "LEFT OUTER JOIN officedba.WorkShiftTime H "
            + "ON Y.WorkShiftTimeID=H.id  "//AND H.IfAttendance='1' 
            + "LEFT OUTER JOIN officedba.WorkshiftSet I "
            + "ON H.WorkShiftNo=I.WorkShiftNo "
            + "LEFT OUTER JOIN officedba.DeptInfo J "
            + "ON X.DeptID=J.id "
            + "LEFT OUTER JOIN officedba.DeptQuarter K "
            + "ON X.QuarterID=K.ID "
            + "WHERE  X.CompanyCD='" + CompanyID + "' ";
           //((Y.IsDelay='1' or Y.IsDelay IS NULL) or (Y.IsForwarOff='1' OR Y.IsForwarOff IS NULL)) AND
           //if (ExceptionType != "") 
           //{
           //    if (ExceptionType == "0") sql += " and ((F.TotalDelayTimeLong>I.LateAbsent OR F.TotalForWardOffiTimeLong>I.ForwardAbsent) OR (Y.IsForwarOff IS NULL AND Y.IsDelay IS NULL)) ";
           //    else if (ExceptionType == "1") sql += " and (Y.IsForwarOff!='1' AND Y.IsDelay='1' AND Y.IsForwarOff IS NOT NULL) ";//迟到
           //    else if (ExceptionType == "2") sql += " and (Y.IsForwarOff='1' AND Y.IsDelay!='1' AND  Y.IsDelay IS NOT NULL) ";//早退
           //    else if (ExceptionType == "3") sql += " and (Y.IsForwarOff IS NULL AND (Y.IsDelay='1' OR Y.IsDelay='0')) ";//有签到无签退
           //    else if (ExceptionType == "4") sql += " and ((Y.IsForwarOff='1' OR Y.IsForwarOff='0') AND Y.IsDelay IS NULL) ";//有签退无签到
           //    else if (ExceptionType == "4") sql += " and (Y.IsForwarOff='1' AND Y.IsDelay='1') ";//迟到、早退
           //}
           if (EmployeeName != "")
               sql += " and X.EmployeeName='" + EmployeeName + "'";
           if (Quarter != "")
               sql += " and X.QuarterID=" + Quarter + "";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion

       #region 获取人员考勤中的休息日（有的话排除）
       /// <summary>
       /// 获取人员考勤中的休息日
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEmployeeSetInfo(int EmployeeID, string Date)
       {
           string sql = "select * from officedba.EmployeeAttendanceSet "
                        + "where EmployeeID=" + EmployeeID + " and StartDate<='" + Date + "' and (EndDate>'" + Date + "' or EndDate is null)";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 查询异常列表
       /// <summary>
       /// 查询
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetExceptionInfo(string ApplyDate, string ApplyEndDate, string ExceptionType, string EmployeeName, string Quarter, string CompanyID, int pageIndex, int pageCount, string ord, ref int TotalCount)
       {
           string sql = "SELECT X.ID,X.EmployeeName,X.EmployeeNo,X.EveryDay,Y.Date,Y.StartTime,Y.EndTime,Y.SignInRemark,Y.SignOutRemark,Y.DelayTimeLong,Y.ForWardOffTimeLong,"
		                +"Y.IsDelay,Y.IsForwarOff,Y.IsExtraSignIn,Y.IsExtraSignOut,Y.ProcessSignInReason,Y.ProcessSignOutReason,Y.ProcessDateTime,"
		                +"F.TotalDelayTimeLong,F.TotalForWardOffiTimeLong,isnull(J.DeptName,'')DeptName,isnull(K.QuarterName,'')QuarterName,"
	                    +"isnull(H.ShiftTimeName,'')ShiftTimeName,isnull(H.Ontime,'')Ontime,isnull(H.OnlateTime,'')OnlateTime,"
                        +"isnull(H.IfAttendance,'')IfAttendance,isnull(H.OffTime,'')OffTime,isnull(H.OffForwordTime,'')OffForwordTime,"
                        +"isnull(I.LateAbsent,'')LateAbsent,isnull(I.ForwardAbsent,'')ForwardAbsent,"
                        +"isnull(G.AttendanceType,'')AttendanceType,isnull(G.WeekRestDay,'')WeekRestDay,isnull(G.MonthRestDay,'')MonthRestDay  "
                        +"FROM "
                        +"(SELECT A.*,B.EveryDay "
	                    +"FROM "
                        + "(select distinct b.ID,b.EmployeeName,b.EmployeeNo,b.CompanyCD,b.DeptID,b.QuarterID from officedba.EmployeeAttendanceSet a "
	                    +" LEFT OUTER JOIN officedba.EmployeeInfo b "
	                    +"ON a.EmployeeID=b.ID WHERE b.Flag='1') A "
                        +"CROSS JOIN officedba.AttendanceEveryDay B "
	                    +"WHERE 1=1  ";
                       if (ApplyDate != "")
                           sql += " and B.EveryDay>='" + ApplyDate + "'";
                       if (ApplyEndDate != "")
                           sql += " and B.EveryDay<='" + ApplyEndDate + "'";
                       sql += " ) X LEFT OUTER JOIN "
	                    +"officedba.DailyAttendance Y "
	                    +"ON X.ID=Y.EmployeeID AND X.EveryDay=Y.Date "
                        +"LEFT OUTER JOIN "
	                    +"("
	                    +"select EmployeeID,Date,Sum(DelayTimeLong)TotalDelayTimeLong,SUM(ForWardOffTimeLong)TotalForWardOffiTimeLong from officedba.DailyAttendance "
	                    +"GROUP BY EmployeeID,Date "
	                    +") F "
                        +"ON X.EveryDay=F.Date AND X.ID=F.EmployeeID "
                        +"LEFT OUTER JOIN officedba.EmployeeAttendanceSet G "
                        + "ON X.ID=G.EmployeeID and Y.EmployeeAttendanceSetID=G.ID "
                        +"AND (G.AttendanceType='0' or G.AttendanceType is null) "
                        +"LEFT OUTER JOIN officedba.WorkShiftTime H "
                        + "ON Y.WorkShiftTimeID=H.id  "//AND H.IfAttendance='1' 
                        +"LEFT OUTER JOIN officedba.WorkshiftSet I "
				        +"ON H.WorkShiftNo=I.WorkShiftNo "
                        +"LEFT OUTER JOIN officedba.DeptInfo J "
                        +"ON X.DeptID=J.id "
                        +"LEFT OUTER JOIN officedba.DeptQuarter K "
                        +"ON X.QuarterID=K.ID "
                        + "WHERE ((Y.IsDelay='1' or Y.IsDelay IS NULL) or (Y.IsForwarOff='1' OR Y.IsForwarOff IS NULL)) AND X.CompanyCD='" + CompanyID + "' ";
                       //if (ExceptionType != "") 
                       //{
                       //    if (ExceptionType == "0") sql += " and ((F.TotalDelayTimeLong>I.LateAbsent OR F.TotalForWardOffiTimeLong>I.ForwardAbsent) OR (Y.IsForwarOff IS NULL AND Y.IsDelay IS NULL)) ";
                       //    else if (ExceptionType == "1") sql += " and (Y.IsForwarOff!='1' AND Y.IsDelay='1' AND Y.IsForwarOff IS NOT NULL) ";//迟到
                       //    else if (ExceptionType == "2") sql += " and (Y.IsForwarOff='1' AND Y.IsDelay!='1' AND  Y.IsDelay IS NOT NULL) ";//早退
                       //    else if (ExceptionType == "3") sql += " and (Y.IsForwarOff IS NULL AND (Y.IsDelay='1' OR Y.IsDelay='0')) ";//有签到无签退
                       //    else if (ExceptionType == "4") sql += " and ((Y.IsForwarOff='1' OR Y.IsForwarOff='0') AND Y.IsDelay IS NULL) ";//有签退无签到
                       //    else if (ExceptionType == "4") sql += " and (Y.IsForwarOff='1' AND Y.IsDelay='1') ";//迟到、早退
                       //}
           if (EmployeeName != "")
               sql += " and X.EmployeeName='" + EmployeeName + "'";
           if (Quarter != "")
               sql += " and X.QuarterID=" + Quarter + "";
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

           //return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 查询考勤明细报表
       /// <summary>
       /// 查询考勤明细报表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceDetailReportInfo(string EmpID, string StartDate, string EndDate, string CompanyID, string AttendanceType)
       {
           string sql = "SELECT A.Date,Isnull(E.EmployeeName,'')EmployeeName,ISNULL(E.EmployeeNo,'')EmployeeNo,isnull(E.DeptName,'')DeptName,isnull(E.QuarterName,'')QuarterName,A.EmployeeID,isnull(B.AttendanceType,'')AttendanceType,"
                         + "ISNULL(SUM(C.AttendanceTimeLong),0)AttendanceTimeLong,"// ----应出勤时间
                         + "isnull(sum(DATEDIFF(Hour,(A.StartTime),(A.EndTime))),0) AS FactAttendanceTimeLong,"//---正常出勤时间
                         + "isnull(sum(A.DelayTimeLong),0)DelayTimeLong,"//---迟到时间
                         + "isnull(sum(A.ForWardOffTimeLong),0)ForWardOffTimeLong,"//---早退时间
                         + "isnull(sum(D.LeaveTimeLong),0)LeaveTimeLong,"//--请假
                         + "isnull(sum(D.OverTimeLong),0)OverTimeLong,"//--加班
                         + "isnull(sum(D.BeOutTimeLong),0)BeOutTimeLong,"//--外出
                         + "isnull(sum(D.BusinessTimeLong),0)BusinessTimeLong,"//--出差
                         + "isnull(sum(D.InsteadTimeLong),0)InsteadTimeLong,"//---替班
                         + "isnull(sum(YearHolidayTimeLong),0)YearHolidayTimeLong "//----年休
                         + "FROM officedba.DailyAttendance A "
                         + "LEFT OUTER JOIN officedba.EmployeeAttendanceSet B "
                         + "ON A.EmployeeAttendanceSetID=B.ID AND A.CompanyCD=B.CompanyCD "
                         + "LEFT OUTER JOIN officedba.WorkShiftTime C "
                         + "ON A.WorkShiftTimeID=C.ID AND A.CompanyCD=C.CompanyCD "
                         + "LEFT OUTER JOIN "
                         + "(select a.*,b.LeaveTimeLong,c.OverTimeLong,d.BeOutTimeLong,e.BusinessTimeLong,f.InsteadTimeLong,g.YearHolidayTimeLong "
                         + "from "
                         + "(select startdate,ApplyUserID,CompanyCD, "
                         + "case Flag when 1 then '请假' "
                         + "when 2 then '加班' "
                         + "when 3 then '外出' "
                         + "when 4 then '出差' "
                         + "when 5 then '替班' "
                         + "when 6 then '年休' "
                         + "end Flag from officedba.AttendanceApply group by Flag,startdate,ApplyUserID,CompanyCD) a "
                         + "left join  "
                         + "( "
                         + "select startdate,ApplyUserID,b.CompanyCD,  "
                         + "case Flag when 1 then '请假'  "
                         + "when 2 then '加班' "
                         + "when 3 then '外出' "
                         + "when 4 then '出差' "
                         + "when 5 then '替班' "
                         + "when 6 then '年休' "
                         + "end Flag,sum(DATEDIFF(Hour,(StartDate+StartTime),(EndDate+EndTime))) AS LeaveTimeLong "
                         + "from "
                         + "(select max(m.id) as id,m.BillID,m.CompanyCD "
                         + "from officedba.FlowInstance m "
                         + "left outer join officedba.AttendanceApply n "
                         + "on m.BillID=n.ID and m.CompanyCD=n.CompanyCD "
                         + "where m.BillTypeFlag='3' "
                         + "AND m.BillTypeCode='20' and m.FlowStatus='3' "
                         + "group by m.BillID,m.CompanyCD) a "
                         + "left outer join officedba.AttendanceApply b "
                         + "on a.BillID=b.ID and a.CompanyCD=b.CompanyCD "
                         + "where flag=1 group by Flag,startdate,ApplyUserID,b.CompanyCD "
                         + ") b "
                         + "on a.ApplyUserID=b.ApplyUserID and a.startdate=b.startdate and a.CompanyCD=b.CompanyCD "
                         + "left join "
                         + "( "
                         + "select startdate,ApplyUserID, b.CompanyCD, "
                         + "case Flag when 1 then '请假'  "
                         + "when 2 then '加班' "
                         + "when 3 then '外出' "
                         + "when 4 then '出差' "
                         + "when 5 then '替班' "
                         + "when 6 then '年休' "
                         + "end Flag,sum(DATEDIFF(Hour,(StartDate+StartTime),(EndDate+EndTime))) AS OverTimeLong "
                         + "from "
                          + "(select max(m.id) as id,m.BillID,m.CompanyCD "
                         + "from officedba.FlowInstance m "
                         + "left outer join officedba.AttendanceApply n "
                         + "on m.BillID=n.ID   and m.CompanyCD=n.CompanyCD "
                         + "where m.BillTypeFlag='3' "
                         + "AND m.BillTypeCode='21' and m.FlowStatus='3' "
                         + "group by m.BillID,m.CompanyCD) a "
                         + "left outer join officedba.AttendanceApply b "
                         + "on a.BillID=b.ID and a.CompanyCD=b.CompanyCD "
                         + "where flag=2 group by Flag,startdate,ApplyUserID,b.CompanyCD "
                         + ") c "
                         + "on a.ApplyUserID=c.ApplyUserID and a.startdate=c.startdate  and a.CompanyCD=c.CompanyCD "
                         + "left join "
                         + "( "
                         + "select startdate,ApplyUserID,b.CompanyCD ,  "
                         + "case Flag when 1 then '请假' "
                         + "when 2 then '加班' "
                         + "when 3 then '外出' "
                         + "when 4 then '出差' "
                         + "when 5 then '替班' "
                         + "when 6 then '年休' "
                         + "end Flag,sum(DATEDIFF(Hour,(StartDate+StartTime),(EndDate+EndTime))) AS BeOutTimeLong "
                         + "from "
                          + "(select max(m.id) as id,m.BillID,m.CompanyCD "
                         + "from officedba.FlowInstance m "
                         + "left outer join officedba.AttendanceApply n "
                         + "on m.BillID=n.ID   and m.CompanyCD=n.CompanyCD "
                         + "where m.BillTypeFlag='3' "
                         + "AND m.BillTypeCode='22' and m.FlowStatus='3' "
                         + "group by m.BillID,m.CompanyCD) a "
                         + "left outer join officedba.AttendanceApply b "
                         + "on a.BillID=b.ID  and a.CompanyCD=b.CompanyCD "
                         + "where flag=3 group by Flag,startdate,ApplyUserID,b.CompanyCD  "
                         + ") d "
                         + "on a.ApplyUserID=d.ApplyUserID and a.startdate=d.startdate and a.CompanyCD=d.CompanyCD "
                         + "left join "
                         + "( "
                         + "select startdate,ApplyUserID,b.CompanyCD,  "
                         + "case Flag when 1 then '请假'"
                         + "when 2 then '加班' "
                         + "when 3 then '外出' "
                         + "when 4 then '出差' "
                         + "when 5 then '替班' "
                         + "when 6 then '年休' "
                         + "end Flag,sum(DATEDIFF(Hour,(StartDate+StartTime),(EndDate+EndTime))) AS BusinessTimeLong "
                         + "from "
                          + "(select max(m.id) as id,m.BillID,m.CompanyCD "
                         + "from officedba.FlowInstance m "
                         + "left outer join officedba.AttendanceApply n "
                         + "on m.BillID=n.ID   and m.CompanyCD=n.CompanyCD "
                         + "where m.BillTypeFlag='3' "
                         + "AND m.BillTypeCode='23' and m.FlowStatus='3' "
                         + "group by m.BillID,m.CompanyCD) a "
                         + "left outer join officedba.AttendanceApply b "
                         + "on a.BillID=b.ID and a.CompanyCD=b.CompanyCD "
                         + "where flag=4 group by Flag,startdate,ApplyUserID,b.CompanyCD "
                         + ") e "
                         + "on a.ApplyUserID=e.ApplyUserID and a.startdate=e.startdate and a.CompanyCD=e.CompanyCD "
                         + "left join "
                         + "( "
                         + "select startdate,ApplyUserID,b.CompanyCD,  "
                         + "case Flag when 1 then '请假'  "
                         + "when 2 then '加班' "
                         + "when 3 then '外出' "
                         + "when 4 then '出差' "
                         + "when 5 then '替班' "
                         + "when 6 then '年休' "
                         + "end Flag,sum(DATEDIFF(Hour,(StartDate+InsteadStartTime),(EndDate+InsteadEndTime))) AS InsteadTimeLong "
                         + "from "
                          + "(select max(m.id) as id,m.BillID,m.CompanyCD "
                         + "from officedba.FlowInstance m "
                         + "left outer join officedba.AttendanceApply n "
                         + "on m.BillID=n.ID   and m.CompanyCD=n.CompanyCD "
                         + "where m.BillTypeFlag='3' "
                         + "AND m.BillTypeCode='24' and m.FlowStatus='3' "
                         + "group by m.BillID,m.CompanyCD) a "
                         + "left outer join officedba.AttendanceApply b "
                         + "on a.BillID=b.ID and a.CompanyCD=b.CompanyCD "
                         + "where flag=5 group by Flag,startdate,ApplyUserID,b.CompanyCD "
                         + ") f "
                         + "on  a.ApplyUserID=f.ApplyUserID and a.startdate=f.startdate and a.CompanyCD=f.CompanyCD "
                         + "left join "
                         + "( "
                         + "select startdate,ApplyUserID,b.CompanyCD,  "
                         + "case Flag when 1 then '请假'  "
                         + "when 2 then '加班' "
                         + "when 3 then '外出' "
                         + "when 4 then '出差' "
                         + "when 5 then '替班' "
                         + "when 6 then '年休' "
                         + "end Flag,sum(DATEDIFF(Hour,(StartDate+StartTime),(EndDate+EndTime))) AS YearHolidayTimeLong "
                         + "from "
                          + "(select max(m.id) as id,m.BillID,m.CompanyCD "
                         + "from officedba.FlowInstance m "
                         + "left outer join officedba.AttendanceApply n "
                         + "on m.BillID=n.ID   and m.CompanyCD=n.CompanyCD "
                         + "where m.BillTypeFlag='3' "
                         + "AND m.BillTypeCode='25' and m.FlowStatus='3' "
                         + "group by m.BillID,m.CompanyCD) a "
                         + "left outer join officedba.AttendanceApply b "
                         + "on a.BillID=b.ID and a.CompanyCD=b.CompanyCD "
                         + "where flag=6 group by Flag,startdate,ApplyUserID,b.CompanyCD  "
                         + ") g "
                         + "on  a.ApplyUserID=g.ApplyUserID  and a.startdate=g.startdate and a.CompanyCD=g.CompanyCD) D "
                         + "ON A.Date=D.StartDate AND A.EmployeeID=D.ApplyUserID and A.CompanyCD=D.CompanyCD "
                         + "LEFT OUTER JOIN "
                         + "(SELECT a.ID,a.EmployeeName,a.CompanyCD,a.EmployeeNo,b.DeptName,c.QuarterName FROM Officedba.EmployeeInfo a "
                         + "LEFT OUTER JOIN officedba.DeptInfo b "
                         + "ON a.DeptID=b.ID AND  a.CompanyCD=b.CompanyCD "
                         + "LEFT OUTER JOIN officedba.DeptQuarter c "
                         + "on a.QuarterID=c.ID and a.CompanyCD=c.CompanyCD "
                         + "WHERE (Flag='1' or Flag='3')) E "
                         + "ON A.EmployeeID=E.ID AND A.CompanyCD=E.CompanyCD "
                         + "WHERE  A.CompanyCD='" + CompanyID + "' AND A.EmployeeID=" + EmpID + " AND B.AttendanceType='" + AttendanceType + "' ";
                       if (StartDate != "")
                           sql += " AND A.Date>='" + StartDate + "'  ";
                       if (EndDate != "")
                           sql += " AND A.Date<='" + EndDate + "'  ";
                       sql += " Group BY A.Date,A.EmployeeID,B.AttendanceType,E.EmployeeName,E.EmployeeNo,E.DeptName,E.QuarterName"; 
                       return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 查询考勤统计报表根据页面传入的RecordNo
       /// <summary>
       /// 查询考勤统计报表根据页面传入的RecordNo
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceReportInfoByNo(string ReportNo,string CompanyID)
       {
           string sql = "select b.EmployeeID,b.Hours as AttendanceTimeLong,isnull(b.NomalHour,0)NomalHour,b.WorkHour as FactAttendanceTimeLong,isnull(b.AttendanceType,'')AttendanceType,"
                        +"b.Overtime as OverTimeLong,b.Leave as LeaveTimeLong,b.Out as BeOutTimeLong,"
                        +"b.Business as BusinessTimeLong,b.[Instead] as InsteadTimeLong,"
                        + "b.LateMinute as DelayTimeLong,b.LeaveEarlyMinute as ForWardOffTimeLong,b.Transferred as YearHolidayTimeLong,"
                        +"isnull(b.ChangeTimes,0)ChangeTimes,isnull(b.ChangeType,'')ChangeType,isnull(b.ChangeNote,0)ChangeNote,"
                        + "b.AttendanceRate,isnull(c.EmployeeNo,'')EmployeeNo,isnull(c.EmployeeName,'')EmployeeName,isnull(d.DeptName,'')DeptName,isnull(e.QuarterName,'')QuarterName "
                        +"from officedba.AttendanceReport a "
                        +"LEFT OUTER JOIN officedba.AttendanceReportMonth b "
                        + "ON a.ReprotNo=b.ReprotNo AND b.CompanyCD='" + CompanyID + "' "
                        +"LEFT OUTER JOIN officedba.EmployeeInfo c "
                        +"ON b.EmployeeID=c.ID "
                        +"LEFT OUTER JOIN officedba.DeptInfo d "
                        +"ON c.DeptID=d.ID "
                        +"LEFT OUTER JOIN officedba.DeptQuarter e "
                        +"ON c.QuarterID=e.ID  "
                        + "where b.CompanyCD='" + CompanyID + "' AND b.ReprotNo='" + ReportNo + "'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 查询考勤月报表
       /// <summary>
       /// 查询考勤月报表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceMonthReportInfo(string StartDate, string EndDate, string CompanyID)
       {
           string sql = "SELECT Isnull(E.EmployeeName,'')EmployeeName,ISNULL(E.EmployeeNo,'')EmployeeNo,isnull(E.DeptName,'')DeptName,isnull(E.QuarterName,'')QuarterName,A.EmployeeID,ISNULL(B.AttendanceType,'')AttendanceType,"
                        + "ISNULL(SUM(C.AttendanceTimeLong),0)AttendanceTimeLong,"// ----应出勤时间
                        + "isnull(sum(DATEDIFF(Hour,(A.StartTime),(A.EndTime))),0) AS FactAttendanceTimeLong,"//---正常出勤时间
                        + "isnull(sum(A.DelayTimeLong),0)DelayTimeLong,"//---迟到时间
                        + "isnull(sum(A.ForWardOffTimeLong),0)ForWardOffTimeLong,"//---早退时间
                        + "isnull(sum(D.LeaveTimeLong),0)LeaveTimeLong,"//--请假
                        + "isnull(sum(D.OverTimeLong),0)OverTimeLong,"//--加班
                        + "isnull(sum(D.BeOutTimeLong),0)BeOutTimeLong,"//--外出
                        + "isnull(sum(D.BusinessTimeLong),0)BusinessTimeLong,"//--出差
                        + "isnull(sum(D.InsteadTimeLong),0)InsteadTimeLong,"//---替班
                        + "isnull(sum(YearHolidayTimeLong),0)YearHolidayTimeLong "//----年休
                        + "FROM officedba.DailyAttendance A "
                        + "LEFT OUTER JOIN officedba.EmployeeAttendanceSet B "
                        + "ON A.EmployeeAttendanceSetID=B.ID AND A.CompanyCD=B.CompanyCD "
                        + "LEFT OUTER JOIN officedba.WorkShiftTime C "
                        + "ON A.WorkShiftTimeID=C.ID AND A.CompanyCD=C.CompanyCD "
                        + "LEFT OUTER JOIN "
                        + "(select a.*,b.LeaveTimeLong,c.OverTimeLong,d.BeOutTimeLong,e.BusinessTimeLong,f.InsteadTimeLong,g.YearHolidayTimeLong "
                        + "from "
                        + "(select startdate,ApplyUserID,CompanyCD, "
                        + "case Flag when 1 then '请假' "
                        + "when 2 then '加班' "
                        + "when 3 then '外出' "
                        + "when 4 then '出差' "
                        + "when 5 then '替班' "
                        + "when 6 then '年休' "
                        + "end Flag from officedba.AttendanceApply group by Flag,startdate,ApplyUserID,CompanyCD) a "
                        + "left join  "
                        + "( "
                        + "select startdate,ApplyUserID,b.CompanyCD,  "
                        + "case Flag when 1 then '请假'  "
                        + "when 2 then '加班' "
                        + "when 3 then '外出' "
                        + "when 4 then '出差' "
                        + "when 5 then '替班' "
                        + "when 6 then '年休' "
                        + "end Flag,sum(DATEDIFF(Hour,(StartDate+StartTime),(EndDate+EndTime))) AS LeaveTimeLong "
                        + "from "
                        + "(select max(m.id) as id,m.BillID,m.CompanyCD "
                        + "from officedba.FlowInstance m "
                        + "left outer join officedba.AttendanceApply n "
                        + "on m.BillID=n.ID and m.CompanyCD=n.CompanyCD "
                        + "where m.BillTypeFlag='3' "
                        + "AND m.BillTypeCode='20' and m.FlowStatus='3' "
                        + "group by m.BillID,m.CompanyCD) a "
                        + "left outer join officedba.AttendanceApply b "
                        + "on a.BillID=b.ID and a.CompanyCD=b.CompanyCD "
                        + "where flag=1 group by Flag,startdate,ApplyUserID,b.CompanyCD "
                        + ") b "
                        + "on a.ApplyUserID=b.ApplyUserID and a.startdate=b.startdate and a.CompanyCD=b.CompanyCD "
                        + "left join "
                        + "( "
                        + "select startdate,ApplyUserID, b.CompanyCD, "
                        + "case Flag when 1 then '请假'  "
                        + "when 2 then '加班' "
                        + "when 3 then '外出' "
                        + "when 4 then '出差' "
                        + "when 5 then '替班' "
                        + "when 6 then '年休' "
                        + "end Flag,sum(DATEDIFF(Hour,(StartDate+StartTime),(EndDate+EndTime))) AS OverTimeLong "
                        + "from "
                         + "(select max(m.id) as id,m.BillID,m.CompanyCD "
                        + "from officedba.FlowInstance m "
                        + "left outer join officedba.AttendanceApply n "
                        + "on m.BillID=n.ID   and m.CompanyCD=n.CompanyCD "
                        + "where m.BillTypeFlag='3' "
                        + "AND m.BillTypeCode='21' and m.FlowStatus='3' "
                        + "group by m.BillID,m.CompanyCD) a "
                        + "left outer join officedba.AttendanceApply b "
                        + "on a.BillID=b.ID and a.CompanyCD=b.CompanyCD "
                        + "where flag=2 group by Flag,startdate,ApplyUserID,b.CompanyCD "
                        + ") c "
                        + "on a.ApplyUserID=c.ApplyUserID and a.startdate=c.startdate  and a.CompanyCD=c.CompanyCD "
                        + "left join "
                        + "( "
                        + "select startdate,ApplyUserID,b.CompanyCD ,  "
                        + "case Flag when 1 then '请假' "
                        + "when 2 then '加班' "
                        + "when 3 then '外出' "
                        + "when 4 then '出差' "
                        + "when 5 then '替班' "
                        + "when 6 then '年休' "
                        + "end Flag,sum(DATEDIFF(Hour,(StartDate+StartTime),(EndDate+EndTime))) AS BeOutTimeLong "
                        + "from "
                         + "(select max(m.id) as id,m.BillID,m.CompanyCD "
                        + "from officedba.FlowInstance m "
                        + "left outer join officedba.AttendanceApply n "
                        + "on m.BillID=n.ID   and m.CompanyCD=n.CompanyCD "
                        + "where m.BillTypeFlag='3' "
                        + "AND m.BillTypeCode='22' and m.FlowStatus='3' "
                        + "group by m.BillID,m.CompanyCD) a "
                        + "left outer join officedba.AttendanceApply b "
                        + "on a.BillID=b.ID  and a.CompanyCD=b.CompanyCD "
                        + "where flag=3 group by Flag,startdate,ApplyUserID,b.CompanyCD  "
                        + ") d "
                        + "on a.ApplyUserID=d.ApplyUserID and a.startdate=d.startdate and a.CompanyCD=d.CompanyCD "
                        + "left join "
                        + "( "
                        + "select startdate,ApplyUserID,b.CompanyCD,  "
                        + "case Flag when 1 then '请假'"
                        + "when 2 then '加班' "
                        + "when 3 then '外出' "
                        + "when 4 then '出差' "
                        + "when 5 then '替班' "
                        + "when 6 then '年休' "
                        + "end Flag,sum(DATEDIFF(Hour,(StartDate+StartTime),(EndDate+EndTime))) AS BusinessTimeLong "
                        + "from "
                         + "(select max(m.id) as id,m.BillID,m.CompanyCD "
                        + "from officedba.FlowInstance m "
                        + "left outer join officedba.AttendanceApply n "
                        + "on m.BillID=n.ID   and m.CompanyCD=n.CompanyCD "
                        + "where m.BillTypeFlag='3' "
                        + "AND m.BillTypeCode='23' and m.FlowStatus='3' "
                        + "group by m.BillID,m.CompanyCD) a "
                        + "left outer join officedba.AttendanceApply b "
                        + "on a.BillID=b.ID and a.CompanyCD=b.CompanyCD "
                        + "where flag=4 group by Flag,startdate,ApplyUserID,b.CompanyCD "
                        + ") e "
                        + "on a.ApplyUserID=e.ApplyUserID and a.startdate=e.startdate and a.CompanyCD=e.CompanyCD "
                        + "left join "
                        + "( "
                        + "select startdate,ApplyUserID,b.CompanyCD,  "
                        + "case Flag when 1 then '请假'  "
                        + "when 2 then '加班' "
                        + "when 3 then '外出' "
                        + "when 4 then '出差' "
                        + "when 5 then '替班' "
                        + "when 6 then '年休' "
                        + "end Flag,sum(DATEDIFF(Hour,(StartDate+InsteadStartTime),(EndDate+InsteadEndTime))) AS InsteadTimeLong "
                        + "from "
                         + "(select max(m.id) as id,m.BillID,m.CompanyCD "
                        + "from officedba.FlowInstance m "
                        + "left outer join officedba.AttendanceApply n "
                        + "on m.BillID=n.ID   and m.CompanyCD=n.CompanyCD "
                        + "where m.BillTypeFlag='3' "
                        + "AND m.BillTypeCode='24' and m.FlowStatus='3' "
                        + "group by m.BillID,m.CompanyCD) a "
                        + "left outer join officedba.AttendanceApply b "
                        + "on a.BillID=b.ID and a.CompanyCD=b.CompanyCD "
                        + "where flag=5 group by Flag,startdate,ApplyUserID,b.CompanyCD "
                        + ") f "
                        + "on  a.ApplyUserID=f.ApplyUserID and a.startdate=f.startdate and a.CompanyCD=f.CompanyCD "
                        + "left join "
                        + "( "
                        + "select startdate,ApplyUserID,b.CompanyCD,  "
                        + "case Flag when 1 then '请假'  "
                        + "when 2 then '加班' "
                        + "when 3 then '外出' "
                        + "when 4 then '出差' "
                        + "when 5 then '替班' "
                        + "when 6 then '年休' "
                        + "end Flag,sum(DATEDIFF(Hour,(StartDate+StartTime),(EndDate+EndTime))) AS YearHolidayTimeLong "
                        + "from "
                         + "(select max(m.id) as id,m.BillID,m.CompanyCD "
                        + "from officedba.FlowInstance m "
                        + "left outer join officedba.AttendanceApply n "
                        + "on m.BillID=n.ID   and m.CompanyCD=n.CompanyCD "
                        + "where m.BillTypeFlag='3' "
                        + "AND m.BillTypeCode='25' and m.FlowStatus='3' "
                        + "group by m.BillID,m.CompanyCD) a "
                        + "left outer join officedba.AttendanceApply b "
                        + "on a.BillID=b.ID and a.CompanyCD=b.CompanyCD "
                        + "where flag=6 group by Flag,startdate,ApplyUserID,b.CompanyCD  "
                        + ") g "
                        + "on  a.ApplyUserID=g.ApplyUserID  and a.startdate=g.startdate and a.CompanyCD=g.CompanyCD) D "
                        + "ON A.Date=D.StartDate AND A.EmployeeID=D.ApplyUserID and A.CompanyCD=D.CompanyCD "
                        + "LEFT OUTER JOIN "
                        + "(SELECT a.ID,a.EmployeeName,a.CompanyCD,a.EmployeeNo,b.DeptName,c.QuarterName FROM Officedba.EmployeeInfo a "
                        + "LEFT OUTER JOIN officedba.DeptInfo b "
                        + "ON a.DeptID=b.ID AND  a.CompanyCD=b.CompanyCD "
                        + "LEFT OUTER JOIN officedba.DeptQuarter c "
                        + "on a.QuarterID=c.ID and a.CompanyCD=c.CompanyCD "
                        + "WHERE (Flag='1' or Flag='3')) E "
                        + "ON A.EmployeeID=E.ID AND A.CompanyCD=E.CompanyCD "
                        + "WHERE  A.CompanyCD='" + CompanyID + "' ";
                        if(StartDate!="") 
                            sql+=" AND A.Date>='"+StartDate+"'  ";
                       if(EndDate!="")
                            sql+=" AND A.Date<='"+EndDate+"'  ";
                        sql+=" Group BY A.EmployeeID,B.AttendanceType,E.EmployeeName,E.EmployeeNo,E.DeptName,E.QuarterName";
                       return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 能否删除考勤信息
       /// <summary>
       /// 能否删除考勤信息
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码</param>
       /// <returns></returns>
       public static bool IfDeleteAttendanceApply(string AttendanceApplyIDS,string BillTypeFlag,string BillTypeCode)
       {
           string[] IDS = null;
           IDS = AttendanceApplyIDS.Split(',');
           bool Flag = true;

           for (int i = 0; i < IDS.Length; i++)
           {
               if (IsExistInfo(IDS[i],BillTypeFlag,BillTypeCode)) 
               {
                   Flag = false;
                   break;
               }
           }
           return Flag;
       }
       #endregion
       #region 判断能否删除考勤信息
       /// <summary>
       /// 判断能否删除考勤信息
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码</param>
       /// <returns></returns>
       public static bool IsExistInfo(string ID, string BillTypeFlag, string BillTypeCode)
       {

           string sql = "SELECT * FROM officedba.FlowInstance WHERE BillID="+ID+" AND BillTypeFlag='"+BillTypeFlag+"' AND BillTypeCode='"+BillTypeCode+"'";
           return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
       }
       #endregion
       #region 删除考勤信息
       /// <summary>
       /// 删除考勤信息
       /// </summary>
       /// <param name="EquipmentIDS">设备IDS</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool DeleteAttendanceApply(string ApplyIDS)
       {
           string allApplyID = "";
           System.Text.StringBuilder sb = new System.Text.StringBuilder();
           string[] Delsql = new string[1];
           try
           {
               string[] IDS = null;
               IDS = ApplyIDS.Split(',');

               for (int i = 0; i < IDS.Length; i++)
               {
                   IDS[i] = "'" + IDS[i] + "'";
                   sb.Append(IDS[i]);
               }

               allApplyID = sb.ToString().Replace("''", "','");
               Delsql[0] = "DELETE FROM officedba.AttendanceApply WHERE ID IN (" + allApplyID + ")";
               SqlHelper.ExecuteTransForListWithSQL(Delsql);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               return false;
           }
       }
       #endregion
       #region 考勤申请单据打印
       /// <summary>
       /// 考勤申请单据打印
       /// </summary>
       /// <param name="ID">人员请假ID</param>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceApplyByIDForPrint(string ID, string CompanyCD)
       {
           string sql = "select a.*,"
                            +"case a.LeaveType "
                            +"when '1' then '调休' when '2' then '公假' when '3' then '病假' when '4' then '事假' "
                            +"when '5' then '其他' End LeaveTypeName,"
                            +"b.EmployeeName AS ApplyUserName,c.EmployeeName AS CreateUserName "
                            +"from officedba.AttendanceApply a "
                            +"left outer join officedba.EmployeeInfo b "
                            +"On a.CompanyCD='"+CompanyCD+"'and a.ApplyUserID=b.ID and a.CompanyCD=b.CompanyCD "
                            +"left outer join officedba.EmployeeInfo c "
                            + "On a.CompanyCD='" + CompanyCD + "' and a.EmployeeID=c.ID  and a.CompanyCD=c.CompanyCD "
                            + "where a.ID=" + ID + "";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 考勤申请列表导出
       /// <summary>
       /// 考勤申请列表导出
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAttendanceApplyInfoForExp(string JoinUser, string ApplyDate, string LeaveType, string ApplyStatus, string Flag, string CompanyID,string ord)
       {
           string sql = "";
           string BillTypeCode = "";
           if (Flag == "2") BillTypeCode = "21";
           if (Flag == "3") BillTypeCode = "22";
           if (Flag == "4") BillTypeCode = "23";
           if (Flag == "5") BillTypeCode = "24";
           if (Flag == "6") BillTypeCode = "25";
           if (Flag == "1")
           {
               sql = "select a.ID,a.Flag,a.EmployeeID,CONVERT(VARCHAR(10),a.ApplyDate,120) as ApplyDate,CONVERT(VARCHAR(10),isnull(a.StartDate,'1900-1-1'),120) as StartDate,"
                   + "CONVERT(VARCHAR(10),isnull(a.EndDate,'1900-1-1'),120) as EndDate,"
                   + "CONVERT(VARCHAR(10),isnull(a.BusinessDate,'1900-1-1'),120) as BusinessDate,"
                   + "CONVERT(VARCHAR(10),isnull(a.BusinessPlanDate,'1900-1-1'),120) as BusinessPlanDate,"
                   + "isnull(a.StartTime,'')StartTime,isnull(a.EndTime,'')EndTime,"
                   + "isnull(a.ApplyReason,'')ApplyReason,isnull(a.LeaveType,'')LeaveType,"
                   + "isnull(a.BusinessPlace,'') BusinessPlace,isnull(a.BusinessPeer,'')BusinessPeer,"
                   + "isnull(a.BusinessTransport,'')BusinessTransport,isnull(a.BusinessAdvance,0.00)BusinessAdvance,"
                   + "isnull(a.BusinessRemark,'')BusinessRemark,isnull(OvertimeType,'')OvertimeType,"
                   + "isnull(a.InsteaEmployees,'')InsteaEmployees,isnull(a.InsteadEmployees,'')InsteadEmployees,isnull(a.InsteadStartTime,'')InsteadStartTime,"
                   + "isnull(a.InsteadEndTime,'')InsteadEndTime,"
                   + "b.EmployeeName,b.NowDeptID,b.NowQuarterID,b.ID AS ID1,isnull(b.DeptName,'')DeptName,CASE a.LeaveType WHEN '1' THEN '调休' WHEN '2' THEN '公假'  WHEN '3' THEN '病假'  WHEN '4' THEN '事假'  WHEN '5' THEN '其他' END TypeName,  "
                   + "CASE isnull(d.FlowStatus,0) WHEN 0 THEN '' "
                  + "WHEN 0 THEN '' "
                   + "WHEN 1 THEN '待审批' "
                   + "WHEN 2 THEN '审批中' "
                   + "WHEN 3 THEN '审批通过' "
                   + "WHEN 4 THEN '审批不通过' "
                   + "WHEN 5 THEN '撤销审批' "
                   + "END FlowStatus "
                   + "from officedba.AttendanceApply a LEFT OUTER JOIN  "
                   + "(select m.ID,m.EmployeeName,m.QuarterID as NowQuarterID,m.DeptID as NowDeptID,m.EmployeeNo,l.DeptName,m.CompanyCD "
                   + "from officedba.EmployeeInfo m "
                   + "left outer join officedba.DeptInfo l "
                   + "on m.DeptID=l.ID and m.CompanyCD=l.CompanyCD) b "
                   + "on a.ApplyUserID=b.id and a.CompanyCD=b.CompanyCD "
                   + "LEFT OUTER JOIN "
                   + "(select m.CompanyCD,m.BillNo,max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                   + "where m.CompanyCD='" + CompanyID + "' and m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND m.BillTypeCode='" + ConstUtil.CODING_RULE_ATTEDDANCEAPPLY_LEAVE + "' "
                   + "and  m.BillID=n.ID and m.CompanyCD=n.CompanyCD and m.BillNo=n.ApplyNo "
                   + "group by m.BillID,m.CompanyCD,m.BillNo) e "
                   + "on a.ApplyNO=e.BillNO and a.CompanyCD=e.CompanyCD "
                   + "LEFT OUTER JOIN officedba.FlowInstance d "
                   + "ON  d.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' "
                   + "AND d.BillTypeCode='" + ConstUtil.CODING_RULE_ATTEDDANCEAPPLY_LEAVE + "'  "
                   + " and d.id=e.id and d.CompanyCD=e.CompanyCD "
                   + "WHERE a.Flag='" + Flag + "' AND a.CompanyCD='" + CompanyID + "' ";
           }
           else
           {
               sql = "select a.ID,a.Flag,a.EmployeeID,CONVERT(VARCHAR(10),a.ApplyDate,120) as ApplyDate,CONVERT(VARCHAR(10),isnull(a.StartDate,'1900-1-1'),120) as StartDate,"
                   + "CONVERT(VARCHAR(10),isnull(a.EndDate,'1900-1-1'),120) as EndDate,"
                   + "CONVERT(VARCHAR(10),isnull(a.BusinessDate,'1900-1-1'),120) as BusinessDate,"
                   + "CONVERT(VARCHAR(10),isnull(a.BusinessPlanDate,'1900-1-1'),120) as BusinessPlanDate,"
                   + "isnull(a.StartTime,'')StartTime,isnull(a.EndTime,'')EndTime,"
                   + "isnull(a.ApplyReason,'')ApplyReason,isnull(a.LeaveType,'')LeaveType,"
                   + "isnull(a.BusinessPlace,'') BusinessPlace,isnull(a.BusinessPeer,'')BusinessPeer,"
                   + "isnull(a.BusinessTransport,'')BusinessTransport,isnull(a.BusinessAdvance,0.00)BusinessAdvance,"
                   + "isnull(a.BusinessRemark,'')BusinessRemark,isnull(OvertimeType,'')OvertimeType,"
                   + "isnull(a.InsteaEmployees,'')InsteaEmployees,isnull(a.InsteadEmployees,'')InsteadEmployees,isnull(a.InsteadStartTime,'')InsteadStartTime,"
                   + "isnull(a.InsteadEndTime,'')InsteadEndTime,"
                   + "b.EmployeeName,b.NowDeptID,b.NowQuarterID,b.ID AS ID1,isnull(b.DeptName,'')DeptName,TypeName='无', "
                   + "CASE isnull(d.FlowStatus,0) WHEN 0 THEN '' "
                  + "WHEN 0 THEN '' "
                   + "WHEN 1 THEN '待审批' "
                   + "WHEN 2 THEN '审批中' "
                   + "WHEN 3 THEN '审批通过' "
                   + "WHEN 4 THEN '审批不通过' "
                   + "WHEN 5 THEN '撤销审批' "
                   + "END FlowStatus "
                   + "from officedba.AttendanceApply a LEFT OUTER JOIN  "
                   + "(select m.ID,m.EmployeeName,m.QuarterID as NowQuarterID,m.DeptID as NowDeptID,m.EmployeeNo,l.DeptName,m.CompanyCD "
                   + "from officedba.EmployeeInfo m "
                   + "left outer join officedba.DeptInfo l "
                   + "on m.DeptID=l.ID AND m.CompanyCD=l.CompanyCD) b "
                   + "on a.ApplyUserID=b.id and a.CompanyCD=b.CompanyCD "
                   + "LEFT OUTER JOIN "
                   + "(select m.CompanyCD,m.BillNo,max(m.id) as id from officedba.FlowInstance m,officedba.AttendanceApply n "
                    + "where m.CompanyCD='" + CompanyID + "' and m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND m.BillTypeCode='" + BillTypeCode + "' "
                    + "and  m.BillID=n.ID and m.CompanyCD=n.CompanyCD and m.BillNo=n.ApplyNo "
                    + "group by m.BillID,m.CompanyCD,m.BillNo) e "
                    + "on a.ApplyNO=e.BillNO and a.CompanyCD=e.CompanyCD "
                    + "LEFT OUTER JOIN officedba.FlowInstance d "
                   + "ON d.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' "
                   + "AND d.BillTypeCode='" + BillTypeCode + "'  "
                   + " and d.id=e.id and d.CompanyCD=e.CompanyCD "
                   + "WHERE Flag='" + Flag + "' AND a.CompanyCD='" + CompanyID + "' ";
           }
           if (JoinUser != "")
               sql += " and b.ID='" + JoinUser + "'";
           //if (ApplyLeaveUser != "")
           //    sql += " and WorkShiftNo LIKE '%" + WorkShiftName + "%'";
           if (ApplyDate != "")
               sql += " and a.ApplyDate='" + ApplyDate + "'";
           if (LeaveType != "")
               sql += " and LeaveType=" + LeaveType + "";
           if (ApplyStatus != "" && ApplyStatus != "0")
               sql += " and d.FlowStatus = '" + ApplyStatus + "'";
           if (ApplyStatus == "0")
               sql += " and d.FlowStatus IS NULL ";
           sql += ord;
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 查询异常列表导出
       /// <summary>
       /// 查询异常列表导出
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetExceptionInfoForExp(string ApplyDate, string ApplyEndDate, string ExceptionType, string EmployeeName, string Quarter, string CompanyID,string ord)
       {
           string sql = "SELECT X.ID,X.EmployeeName,X.EmployeeNo,X.EveryDay,Y.Date,Y.StartTime,Y.EndTime,Y.SignInRemark,Y.SignOutRemark,ISNULL(Y.DelayTimeLong,0)DelayTimeLong,ISNULL(Y.ForWardOffTimeLong,0)ForWardOffTimeLong,"
                        + "Y.IsDelay,Y.IsForwarOff,Y.IsExtraSignIn,Y.IsExtraSignOut,Y.ProcessSignInReason,Y.ProcessSignOutReason,Y.ProcessDateTime,"
                        + "ISNULL(F.TotalDelayTimeLong,0)TotalDelayTimeLong,ISNULL(F.TotalForWardOffiTimeLong,0)TotalForWardOffiTimeLong,isnull(J.DeptName,'')DeptName,isnull(K.QuarterName,'')QuarterName,"
                        + "isnull(H.ShiftTimeName,'')ShiftTimeName,isnull(H.Ontime,'')Ontime,isnull(H.OnlateTime,'')OnlateTime,"
                        + "isnull(H.IfAttendance,'')IfAttendance,isnull(H.OffTime,'')OffTime,isnull(H.OffForwordTime,'')OffForwordTime,"
                        + "isnull(I.LateAbsent,0)LateAbsent,isnull(I.ForwardAbsent,0)ForwardAbsent,"
                        + "isnull(G.AttendanceType,'')AttendanceType,isnull(G.WeekRestDay,'')WeekRestDay,isnull(G.MonthRestDay,'')MonthRestDay  "
                        + "FROM "
                        + "(SELECT A.*,B.EveryDay "
                        + "FROM "
                        + "(select distinct b.ID,b.EmployeeName,b.EmployeeNo,b.CompanyCD,b.DeptID,b.QuarterID from officedba.EmployeeAttendanceSet a "
                        + " LEFT OUTER JOIN officedba.EmployeeInfo b "
                        + "ON a.EmployeeID=b.ID WHERE b.Flag='1') A "
                        + "CROSS JOIN officedba.AttendanceEveryDay B "
                        + "WHERE 1=1  ";
           if (ApplyDate != "")
               sql += " and B.EveryDay>='" + ApplyDate + "'";
           if (ApplyEndDate != "")
               sql += " and B.EveryDay<='" + ApplyEndDate + "'";
           sql += " ) X LEFT OUTER JOIN "
            + "officedba.DailyAttendance Y "
            + "ON X.ID=Y.EmployeeID AND X.EveryDay=Y.Date "
            + "LEFT OUTER JOIN "
            + "("
            + "select EmployeeID,Date,Sum(DelayTimeLong)TotalDelayTimeLong,SUM(ForWardOffTimeLong)TotalForWardOffiTimeLong from officedba.DailyAttendance "
            + "GROUP BY EmployeeID,Date "
            + ") F "
            + "ON X.EveryDay=F.Date AND X.ID=F.EmployeeID "
            + "LEFT OUTER JOIN officedba.EmployeeAttendanceSet G "
            + "ON X.ID=G.EmployeeID and Y.EmployeeAttendanceSetID=G.ID "
            + "AND (G.AttendanceType='0' or G.AttendanceType is null) "
            + "LEFT OUTER JOIN officedba.WorkShiftTime H "
            + "ON Y.WorkShiftTimeID=H.id  "//AND H.IfAttendance='1' 
            + "LEFT OUTER JOIN officedba.WorkshiftSet I "
            + "ON H.WorkShiftNo=I.WorkShiftNo "
            + "LEFT OUTER JOIN officedba.DeptInfo J "
            + "ON X.DeptID=J.id "
            + "LEFT OUTER JOIN officedba.DeptQuarter K "
            + "ON X.QuarterID=K.ID "
            + "WHERE ((Y.IsDelay='1' or Y.IsDelay IS NULL) or (Y.IsForwarOff='1' OR Y.IsForwarOff IS NULL)) AND X.CompanyCD='" + CompanyID + "' ";
           if (EmployeeName != "")
               sql += " and X.EmployeeName='" + EmployeeName + "'";
           if (Quarter != "")
               sql += " and X.QuarterID=" + Quarter + "";
           sql += ord;
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
    }
}

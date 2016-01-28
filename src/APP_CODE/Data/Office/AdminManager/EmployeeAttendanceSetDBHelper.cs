/**********************************************
 * 类作用：   人员考勤设置数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/12
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
    /// 类名：EmployeeAttendanceSetDBHelper
    /// 描述：人员考勤设置数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/04/10
    /// </summary>
   public class EmployeeAttendanceSetDBHelper
   {
       #region 添加人员考勤设置信息
       /// <summary>
       /// 添加人员考勤设置信息
       /// </summary>
       /// <param name="EmployeeAttendanceSetM">人员考勤信息</param>
       /// <param name="Employees">人员信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddEmployeeAttendance(EmployeeAttendanceSetModel EmployeeAttendanceSetM, string Employees)
        {
            try
            {
                string[] strarray = null;
                string recorditems = "";
                string[] inseritems = null;
                strarray = Employees.Split(',');
                string[] sqlarray = new string[strarray.Length];
                //sqlarray[0] = "";

                for (int i = 0; i < strarray.Length; i++)
                {
                    StringBuilder EmployeeAttendanceSql = new StringBuilder();
                    recorditems = strarray[i];
                    inseritems = recorditems.Split(',');
                    if (recorditems.Length != 0)
                    {
                        EmployeeAttendanceSetM.EmployeeID =Convert.ToInt32(inseritems[0].ToString());

                        EmployeeAttendanceSql.AppendLine("INSERT INTO officedba.EmployeeAttendanceSet");
                        EmployeeAttendanceSql.AppendLine("		(CompanyCD      ");
                        EmployeeAttendanceSql.AppendLine("		,EmployeeID        ");
                        EmployeeAttendanceSql.AppendLine("		,WorkGroupNo        ");
                        EmployeeAttendanceSql.AppendLine("		,AttendanceType        ");
                        EmployeeAttendanceSql.AppendLine("		,WorkOverTimeType        ");
                        EmployeeAttendanceSql.AppendLine("		,WeekRestDay        ");
                        EmployeeAttendanceSql.AppendLine("		,MonthRestDay        ");
                        EmployeeAttendanceSql.AppendLine("		,StartDate        ");
                        EmployeeAttendanceSql.AppendLine("		,ModifiledDate        ");
                        EmployeeAttendanceSql.AppendLine("		,ModifiledUserID)        ");
                        EmployeeAttendanceSql.AppendLine("VALUES                  ");
                        EmployeeAttendanceSql.AppendLine("		('" + EmployeeAttendanceSetM.CompanyCD + "'     ");
                        EmployeeAttendanceSql.AppendLine("		," + Employees.Replace(",","").Trim() + "       ");
                        EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.WorkGroupNo + "'       ");
                        EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.AttendanceType + "'       ");
                        EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.WorkOverTimeType + "'       ");
                        EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.WeekRestDay + "'       ");
                        EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.MonthRestDay + "'       ");
                        EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.StartDate + "'       ");
                        EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.ModifiledDate + "'       ");
                        EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.ModifiledUserID + "')       ");
                        sqlarray[i] = EmployeeAttendanceSql.ToString();
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
       #region 更新人员设置信息根据开始时间
       /// <summary>
       /// 更新人员设置信息根据开始时间
       /// </summary>
       /// <param name="EmployeeAttendanceSetM">人员考勤信息</param>
       /// <param name="Employees">人员信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateEmployeeAttendanceByDate(EmployeeAttendanceSetModel EmployeeAttendanceSetM, string Employees)
       {
           try
           {
               string[] strarray = null;
               string recorditems = "";
               string[] inseritems = null;
               strarray = Employees.Split(',');
               string[] sqlarray = new string[strarray.Length];
               sqlarray[0] = "DELETE FROM officedba.EmployeeAttendanceSet WHERE EmployeeID=" + Employees.Replace(",","").Trim() + "  "
                          + "and StartDate='" + EmployeeAttendanceSetM.StartDate + "' and EndDate is null";

               for (int i = 0; i < strarray.Length; i++)
               {
                   StringBuilder EmployeeAttendanceSql = new StringBuilder();
                   recorditems = strarray[i];
                   inseritems = recorditems.Split(',');
                   if (recorditems.Length != 0)
                   {
                       EmployeeAttendanceSetM.EmployeeID = Convert.ToInt32(inseritems[0].ToString());

                       EmployeeAttendanceSql.AppendLine("INSERT INTO officedba.EmployeeAttendanceSet");
                       EmployeeAttendanceSql.AppendLine("		(CompanyCD      ");
                       EmployeeAttendanceSql.AppendLine("		,EmployeeID        ");
                       EmployeeAttendanceSql.AppendLine("		,WorkGroupNo        ");
                       EmployeeAttendanceSql.AppendLine("		,AttendanceType        ");
                       EmployeeAttendanceSql.AppendLine("		,WorkOverTimeType        ");
                       EmployeeAttendanceSql.AppendLine("		,WeekRestDay        ");
                       EmployeeAttendanceSql.AppendLine("		,MonthRestDay        ");
                       EmployeeAttendanceSql.AppendLine("		,StartDate        ");
                       EmployeeAttendanceSql.AppendLine("		,ModifiledDate        ");
                       EmployeeAttendanceSql.AppendLine("		,ModifiledUserID)        ");
                       EmployeeAttendanceSql.AppendLine("VALUES                  ");
                       EmployeeAttendanceSql.AppendLine("		('" + EmployeeAttendanceSetM.CompanyCD + "'     ");
                       EmployeeAttendanceSql.AppendLine("		," + Employees.Replace(",", "").Trim() + "       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.WorkGroupNo + "'       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.AttendanceType + "'       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.WorkOverTimeType + "'       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.WeekRestDay + "'       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.MonthRestDay + "'       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.StartDate + "'       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.ModifiledDate + "'       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.ModifiledUserID + "')       ");
                       sqlarray[i+1] = EmployeeAttendanceSql.ToString();
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
       #region 更新排班信息根据(更新上一条)
       /// <summary>
       /// 更新排班信息根据(更新上一条)
       /// </summary>
       /// </summary>
       /// <param name="EmployeeAttendanceSetM">人员考勤信息</param>
       /// <param name="Employees">人员信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateEmployeeAttendanceInfo(EmployeeAttendanceSetModel EmployeeAttendanceSetM, string Employees)
       {
               try
               {
                   string[] strarray = null;
                   string recorditems = "";
                   string[] inseritems = null;
                   strarray = Employees.Split(',');
                   string[] sqlarray = new string[strarray.Length];
                   sqlarray[0] = "UPDATE officedba.EmployeeAttendanceSet SET EndDate='" + EmployeeAttendanceSetM.StartDate + "',"
                                 + "ModifiledDate='" + EmployeeAttendanceSetM.ModifiledDate + "',ModifiledUserID='" + EmployeeAttendanceSetM.ModifiledUserID + "' "
                                 + " WHERE EmployeeID=" + Employees.Replace(",", "").Trim() + " and WorkGroupNo='" + EmployeeAttendanceSetM.WorkGroupNo + "'"
                                 + "and StartDate<'" + EmployeeAttendanceSetM.StartDate + "' and EndDate is null";
                   for (int i = 0; i < strarray.Length; i++)
                   {
                       StringBuilder EmployeeAttendanceSql = new StringBuilder();
                       recorditems = strarray[i];
                       inseritems = recorditems.Split(',');
                       if (recorditems.Length != 0)
                       {
                           EmployeeAttendanceSetM.EmployeeID = Convert.ToInt32(inseritems[0].ToString());

                           EmployeeAttendanceSql.AppendLine("INSERT INTO officedba.EmployeeAttendanceSet");
                           EmployeeAttendanceSql.AppendLine("		(CompanyCD      ");
                           EmployeeAttendanceSql.AppendLine("		,EmployeeID        ");
                           EmployeeAttendanceSql.AppendLine("		,WorkGroupNo        ");
                           EmployeeAttendanceSql.AppendLine("		,AttendanceType        ");
                           EmployeeAttendanceSql.AppendLine("		,WorkOverTimeType        ");
                           EmployeeAttendanceSql.AppendLine("		,WeekRestDay        ");
                           EmployeeAttendanceSql.AppendLine("		,MonthRestDay        ");
                           EmployeeAttendanceSql.AppendLine("		,StartDate        ");
                           EmployeeAttendanceSql.AppendLine("		,ModifiledDate        ");
                           EmployeeAttendanceSql.AppendLine("		,ModifiledUserID)        ");
                           EmployeeAttendanceSql.AppendLine("VALUES                  ");
                           EmployeeAttendanceSql.AppendLine("		('" + EmployeeAttendanceSetM.CompanyCD + "'     ");
                           EmployeeAttendanceSql.AppendLine("		," + Employees.Replace(",", "").Trim() + "       ");
                           EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.WorkGroupNo + "'       ");
                           EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.AttendanceType + "'       ");
                           EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.WorkOverTimeType + "'       ");
                           EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.WeekRestDay + "'       ");
                           EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.MonthRestDay + "'       ");
                           EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.StartDate + "'       ");
                           EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.ModifiledDate + "'       ");
                           EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.ModifiledUserID + "')       ");
                           sqlarray[i+1] = EmployeeAttendanceSql.ToString();
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
       #region 判断是插入还是更新某阶段的人员考勤设置
       /// <summary>
       /// 判断是插入还是更新某阶段的人员考勤设置
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="WorkGroupNo">班组号</param>
       /// <param name="WorkPlanStartDate">设置开始日期</param>
       /// <returns></returns>
       public static int InsertOrUpdateWorkPlanInfo(string EmployeesId, string WorkGroupNo, string StartDate)
       {
           string sql = "select isnull(max(id),0) as IsExist from  officedba.EmployeeAttendanceSet  "
                         + "where "
                         + "EmployeeID='" + EmployeesId + "' and WorkGroupNo='" + WorkGroupNo + "' "
                         + "and StartDate<'" + StartDate + "' and EndDate is null";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           if (IsExist != null && (int)IsExist.Rows[0][0] > 0)
           {
               return (int)IsExist.Rows[0][0];//存在（更新上一条，插入新的一条）
           }
           else
           {
               return 0;//不存在 直接插入一条
           }
       }
       #endregion
       #region 判断此员工是否在表中有记录
       /// <summary>
       /// 判断此员工是否在表中有记录
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="WorkGroupNo">班组号</param>
       /// <param name="WorkPlanStartDate">排班开始日期</param>
       /// <returns></returns>
       public static int IsExistEmployeeID(string EmployeesId)
       {
           string sql = "SELECT isnull(max(id),0) as IsExist FROM officedba.EmployeeAttendanceSet "
                            + "WHERE EmployeeID=" + EmployeesId + "";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           if (IsExist != null && (int)IsExist.Rows[0][0] > 0)
           {
               return (int)IsExist.Rows[0][0];//有，判断其他
           }
           else
           {
               return 0;//没有直接插入一条
           }
       }
       #endregion
       #region 此时间是不是已在其他班组
       /// <summary>
       /// 此时间是不是已在其他班组
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="WorkGroupNo">班组号</param>
       /// <param name="WorkPlanStartDate">排班开始日期</param>
       /// <returns></returns>
       public static int IsExistOtherGroupNo(string EmployeesId, string WorkGroupNo, string StartDate)
       {
           string sql = "SELECT isnull(max(id),0) as IsExist FROM officedba.EmployeeAttendanceSet "
                            + "WHERE EmployeeID=" + EmployeesId + " AND WorkGroupNo NOT IN ('" + WorkGroupNo + "') "
                            + "AND StartDate<='" + StartDate + "' AND EndDate >'" + StartDate + "'";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           if (IsExist != null && (int)IsExist.Rows[0][0] > 0)
           {
               return (int)IsExist.Rows[0][0];//有，不能插入新的班组
           }
           else
           {
               return 0;//不在其他班组，可以设置
           }
       }
       #endregion
       #region 开始时间相等，结束时间为空时，是否在其他班组
       /// <summary>
       /// 开始时间相等，结束时间为空时，是否在其他班组
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="WorkGroupNo">班组号</param>
       /// <param name="WorkPlanStartDate">排班开始日期</param>
       /// <returns></returns>
       public static int IsExistOtherGroupNoByDate(string EmployeesId, string WorkGroupNo, string StartDate)
       {
           string sql = "SELECT isnull(max(id),0) as IsExist FROM officedba.EmployeeAttendanceSet "
                            + "WHERE EmployeeID=" + EmployeesId + " AND WorkGroupNo NOT IN ('" + WorkGroupNo + "') "
                            + "AND StartDate='" + StartDate + "' AND EndDate IS NULL";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           if (IsExist != null && (int)IsExist.Rows[0][0] > 0)
           {
               return (int)IsExist.Rows[0][0];//有，不能插入新的班组
           }
           else
           {
               return 0;//不在其他班组，可以设置
           }
       }
       #endregion
       #region 是否存在此班组的排班
       /// <summary>
       /// 是否存在此班组的排班
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="WorkGroupNo">班组号</param>
       /// <param name="WorkPlanStartDate">排班开始日期</param>
       /// <returns></returns>
       public static DataTable IsExistGroupPlane(string WorkGroupNo, string CompanyCD)
       {
           string sql = "select * from officedba.WorkPlan  where workgroupno='" + WorkGroupNo + "' and CompanyCD='" + CompanyCD + "'";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           return IsExist;

       }
       #endregion
       #region 此班组是正常班还是需要排班的
       /// <summary>
       /// 此班组是正常班还是需要排班的
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="WorkGroupNo">班组号</param>
       /// <param name="WorkPlanStartDate">排班开始日期</param>
       /// <returns></returns>
       public static int IsNormalGroup(string WorkGroupNo, string CompanyCD)
       {
           string sql = "select * from officedba.WorkGroup  where WorkGroupNo='" + WorkGroupNo + "' and CompanyCD='" + CompanyCD + "' and WorkGroupType=1";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           if (IsExist != null)
           {
               if (IsExist.Rows.Count > 0)
                   return IsExist.Rows.Count;
               else
                   return 0;
           }
           else
           {
               return 0;
           }

       }
       #endregion
       #region 是否小于当月排班开始日期
       /// <summary>
       /// 是否小于当月排班开始日期
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="WorkGroupNo">班组号</param>
       /// <param name="WorkPlanStartDate">排班开始日期</param>
       /// <returns></returns>
       public static DataTable IsExistMonthDay(string WorkGroupNo, string StartDate)
       {
           string sql = "select isnull(b.WorkGroupNo,'')WorkGroupNo,min(a.WorkPlanStartDate)StartDate "
                        +"from officedba.WorkPlan a "
                        +"LEFT OUTER  join "
                        +"(select WorkGroupNo,min(WorkPlanStartDate) as StartDate from officedba.WorkPlan  "
                        + "WHERE WorkPlanStartDate<='" + StartDate + "' and WorkGroupNo='"+WorkGroupNo+"' group by WorkGroupNo) b "
                        +"on a.WorkPlanStartDate=b.StartDate "
                        +"GROUP BY b.WorkGroupNo";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           return IsExist;
           
       }
       #endregion
       #region 此时间后换成其他班组，更新最新的班组结束时间（最大ID为条件更新上条记录），在插入新的一条记录
       /// <summary>
       /// 此时间后换成其他班组，更新最新的班组结束时间（最大ID为条件更新上条记录），在插入新的一条记录
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="WorkGroupNo">班组号</param>
       /// <param name="WorkPlanStartDate">设置开始日期</param>
       /// <returns></returns>
       public static int InsertOrUpdateOtherGroupNo(string EmployeesId, string WorkGroupNo, string StartDate)
       {
           string sql = "select isnull(max(id),0) as IsExist from  officedba.EmployeeAttendanceSet  "
                         + "where "
                         + "EmployeeID='" + EmployeesId + "' and WorkGroupNo NOT IN ('" + WorkGroupNo + "') "
                         + "and StartDate<'" + StartDate + "' and EndDate is null";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           if (IsExist != null && (int)IsExist.Rows[0][0] > 0)
           {
               return (int)IsExist.Rows[0][0];//存在（更新上一条(根据返回的ID更新上条记录)，插入新的一条）
           }
           else
           {
               return 0;//不存在 直接插入一条
           }
       }
       #endregion
       #region 日期相等时的判断
       /// <summary>
       /// 日期相等时的判断
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="WorkGroupNo">班组号</param>
       /// <param name="WorkPlanStartDate">排班开始日期</param>
       /// <returns></returns>
       public static int InsertOrUpdateWorkPlanInfoByDate(string EmployeesId,string StartDate)
       {
           string sql = "select isnull(max(id),0) as IsExist from  officedba.EmployeeAttendanceSet  "
                         + "where "
                         + "EmployeeID='" + EmployeesId + "' "
                         + "and StartDate='" + StartDate + "' and EndDate is null";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           if (IsExist != null && (int)IsExist.Rows[0][0] > 0)
           {
               return (int)IsExist.Rows[0][0];//存在（更新上一条，插入新的一条）
           }
           else
           {
               return 0;//不存在 直接插入一条
           }
       }
       #endregion
       #region 判断能否插入新的人员设置
       /// <summary>
       /// 判断能否插入新的人员设置
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="WorkGroupNo">班组号</param>
       /// <param name="WorkPlanStartDate">排班开始日期</param>
       /// <returns></returns>
       public static int IfInsertOrUpdate(string EmployeesId, string WorkGroupNo, string StartDate)
       {
           string sql = "select isnull(max(id),0) as IsExist from  officedba.EmployeeAttendanceSet  "
                         + "where "
                         + "EmployeeID='" + EmployeesId + "' and WorkGroupNo='" + WorkGroupNo + "' "
                         + "and StartDate<='" + StartDate + "' and EndDate >'" + StartDate + "'";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           if (IsExist != null && (int)IsExist.Rows[0][0] > 0)
           {
               return (int)IsExist.Rows[0][0];
           }
           else
           {
               return 0;
           }
       }
       #endregion
       #region 设置日期在开始和结束日期中间不能进行设置
       /// <summary>
       /// 设置日期在开始和结束日期中间不能进行设置
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="WorkGroupNo">班组号</param>
       /// <param name="WorkPlanStartDate">排班开始日期</param>
       /// <returns></returns>
       public static int MiddleDateNotInsert(string EmployeesId, string WorkGroupNo, string StartDate)
       {
           string sql = "select isnull(max(id),0) as IsExist from  officedba.EmployeeAttendanceSet  "
                         + "where "
                         + "EmployeeID='" + EmployeesId + "' and WorkGroupNo='" + WorkGroupNo + "' "
                         + "and StartDate<'" + StartDate + "' and EndDate>'" + StartDate + "'";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           if (IsExist != null && (int)IsExist.Rows[0][0] > 0)
           {
               return (int)IsExist.Rows[0][0];//存在（更新上一条，插入新的一条）
           }
           else
           {
               return 0;//不存在 直接插入一条
           }
       }
       #endregion

       #region 更新排班信息根据(其他班组信息，根据返回的ID更新)
       /// <summary>
       /// 更新排班信息根据(其他班组信息，根据返回的ID更新)
       /// </summary>
       /// </summary>
       /// <param name="EmployeeAttendanceSetM">人员考勤信息</param>
       /// <param name="Employees">人员信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateEmployeeAttendanceOtherGroupInfo(EmployeeAttendanceSetModel EmployeeAttendanceSetM, string Employees,int ID)
       {
           try
           {
               string[] strarray = null;
               string recorditems = "";
               string[] inseritems = null;
               strarray = Employees.Split(',');
               string[] sqlarray = new string[strarray.Length];
               sqlarray[0] = "UPDATE officedba.EmployeeAttendanceSet SET EndDate='" + EmployeeAttendanceSetM.StartDate + "',"
                             + "ModifiledDate='" + EmployeeAttendanceSetM.ModifiledDate + "',ModifiledUserID='" + EmployeeAttendanceSetM.ModifiledUserID + "' "
                             + " WHERE ID=" + ID + "";
               for (int i = 0; i < strarray.Length; i++)
               {
                   StringBuilder EmployeeAttendanceSql = new StringBuilder();
                   recorditems = strarray[i];
                   inseritems = recorditems.Split(',');
                   if (recorditems.Length != 0)
                   {
                       EmployeeAttendanceSetM.EmployeeID = Convert.ToInt32(inseritems[0].ToString());

                       EmployeeAttendanceSql.AppendLine("INSERT INTO officedba.EmployeeAttendanceSet");
                       EmployeeAttendanceSql.AppendLine("		(CompanyCD      ");
                       EmployeeAttendanceSql.AppendLine("		,EmployeeID        ");
                       EmployeeAttendanceSql.AppendLine("		,WorkGroupNo        ");
                       EmployeeAttendanceSql.AppendLine("		,AttendanceType        ");
                       EmployeeAttendanceSql.AppendLine("		,WorkOverTimeType        ");
                       EmployeeAttendanceSql.AppendLine("		,WeekRestDay        ");
                       EmployeeAttendanceSql.AppendLine("		,MonthRestDay        ");
                       EmployeeAttendanceSql.AppendLine("		,StartDate        ");
                       EmployeeAttendanceSql.AppendLine("		,ModifiledDate        ");
                       EmployeeAttendanceSql.AppendLine("		,ModifiledUserID)        ");
                       EmployeeAttendanceSql.AppendLine("VALUES                  ");
                       EmployeeAttendanceSql.AppendLine("		('" + EmployeeAttendanceSetM.CompanyCD + "'     ");
                       EmployeeAttendanceSql.AppendLine("		," + Employees.Replace(",", "").Trim() + "       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.WorkGroupNo + "'       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.AttendanceType + "'       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.WorkOverTimeType + "'       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.WeekRestDay + "'       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.MonthRestDay + "'       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.StartDate + "'       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.ModifiledDate + "'       ");
                       EmployeeAttendanceSql.AppendLine("		,'" + EmployeeAttendanceSetM.ModifiledUserID + "')       ");
                       sqlarray[i + 1] = EmployeeAttendanceSql.ToString();
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

       #region  选择人员查看考勤设置
       /// <summary>
       /// 选择人员查看考勤设置
       /// </summary>
       /// <param name="UserJoin">人员ID</param>
       /// <returns>DataTable</returns>
       public static DataTable GetInfoByEmployeeID(string UserJoin)
       {
           string sql = "select a.*,b.EmployeeName from officedba.EmployeeAttendanceSet a "
                          +"LEFT OUTER JOIN officedba.EmployeeInfo b "
                          +"ON a.EmployeeID=b.ID "
                          + "WHERE a.EmployeeID=" + UserJoin.Trim() + " AND a.EndDate is null";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion

       #region  选择人员查看考勤设置列表
       /// <summary>
       /// 选择人员查看考勤设置列表
       /// </summary>
       /// <param name="UserJoin">人员ID</param>
       /// <returns>DataTable</returns>
       public static DataTable GetInfoListByEmployeeID(string UserJoin, int pageIndex, int pageCount, string ord, ref int TotalCount)
       {
           string sql = "select a.*,b.EmployeeName,c.WorkGroupName from officedba.EmployeeAttendanceSet a "
                            + "left outer join officedba.EmployeeInfo b "
                            +"on a.EmployeeID=b.ID and a.CompanyCD=b.CompanyCD "
                            +"left outer join officedba.WorkGroup c "
                            +"on a.WorkGroupNo=c.WorkGroupNo and a.CompanyCD=c.CompanyCD "
                            + "WHERE a.EmployeeID=" + UserJoin.Trim() + " ";
           //return SqlHelper.ExecuteSql(sql);
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

       }
       #endregion

       #region 删除人员考勤设置
       /// <summary>
       /// 删除人员考勤设置
       /// </summary>
       /// <param name="EquipReceiveNos">设备领用IDS</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool DelEmployeeSetInfo(string EmpID,string StartDate)
       {
           System.Text.StringBuilder sb = new System.Text.StringBuilder();
           string[] Delsql = new string[2];
           try
           {
               Delsql[0] = "DELETE FROM officedba.EmployeeAttendanceSet WHERE EmployeeID=" + EmpID + " and StartDate='" + StartDate + "' and EndDate IS NULL";
               Delsql[1] = "UPDATE officedba.EmployeeAttendanceSet SET EndDate=NULL WHERE EmployeeID=" + EmpID + " and EndDate='" + StartDate + "' and EndDate IS NOT NULL";
               SqlHelper.ExecuteTransForListWithSQL(Delsql);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               return false;
           }
       }
       #endregion

       #region 判断是否已经设置过休息日
       /// <summary>
       /// 判断是否已经设置过休息日
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="WorkGroupNo">班组号</param>
       /// <param name="WorkPlanStartDate">排班开始日期</param>
       /// <returns></returns>
       public static int IfHaveRestDay(string CompanyID, string WorkGroupNo, string StartDate)
       {
           string sql = "select * from officedba.WorkPlan "
                    + "where CompanyCD='" + CompanyID + "' and WorkPlanStartDate<='" + StartDate + "' and (WorkPlanEndDate is null or WorkPlanEndDate>'" + StartDate + "') "
                    + "and WorkGroupNo='" + WorkGroupNo + "' and IsRestDay='1'";
           DataTable IsExist = SqlHelper.ExecuteSql(sql);
           if (IsExist != null)
           {
               if (IsExist.Rows.Count > 0)
                   return IsExist.Rows.Count;
               else
                   return 0;
           }
           else
           {
               return 0;
           }
       }
       #endregion
    }
}

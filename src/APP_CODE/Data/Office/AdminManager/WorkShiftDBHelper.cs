/**********************************************
 * 类作用：   班次设置数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/07
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
    /// 类名：WorkShiftDBHelper
    /// 描述：班次设置数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/04/07
    /// </summary>
   public class WorkShiftDBHelper
   {
       #region 添加班次信息
       /// <summary>
       /// 添加班次信息
        /// </summary>
       /// <param name="WorkShiftSetM">班次信息</param>
       /// <param name="WorkShiftTimeInfos">班段信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddWorkShiftInfo(WorkShiftSetModel WorkShiftSetM, string WorkShiftTimeInfos)
        {
            try
            {
                StringBuilder WorkShiftSql = new StringBuilder();
                WorkShiftSql.AppendLine("INSERT INTO officedba.WorkshiftSet");
                WorkShiftSql.AppendLine("		(CompanyCD      ");
                WorkShiftSql.AppendLine("		,WorkShiftNo         ");
                WorkShiftSql.AppendLine("		,WorkShiftName       ");
                WorkShiftSql.AppendLine("		,DayWorkLong       ");
                WorkShiftSql.AppendLine("		,LateAbsent       ");
                WorkShiftSql.AppendLine("		,ForwardAbsent       ");
                WorkShiftSql.AppendLine("		,WorkOvertime      ");
                WorkShiftSql.AppendLine("		,ModifiedDate   ");
                WorkShiftSql.AppendLine("		,ModifiedUserID)        ");
                WorkShiftSql.AppendLine("VALUES                  ");
                WorkShiftSql.AppendLine("		('" + WorkShiftSetM.CompanyCD + "'     ");
                WorkShiftSql.AppendLine("		,'" + WorkShiftSetM.WorkShiftNo + "'       ");
                WorkShiftSql.AppendLine("		,'" + WorkShiftSetM.WorkShiftName + "'      ");
                WorkShiftSql.AppendLine("		," + WorkShiftSetM.DayWorkLong + "      ");
                WorkShiftSql.AppendLine("		," + WorkShiftSetM.LateAbsent + "      ");
                WorkShiftSql.AppendLine("		," + WorkShiftSetM.ForwardAbsent + "     ");
                WorkShiftSql.AppendLine("		," + WorkShiftSetM.WorkOvertime + "  ");
                WorkShiftSql.AppendLine("		,'" + WorkShiftSetM.ModifiedDate + "'");
                WorkShiftSql.AppendLine("		,'" + WorkShiftSetM.ModifiedUserID + "')       ");
                return InsertAll(WorkShiftSql.ToString(), WorkShiftTimeInfos, WorkShiftSetM.WorkShiftNo, WorkShiftSetM.CompanyCD, WorkShiftSetM.ModifiedUserID);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
       /// 添加班次信息
        /// </summary>
       /// <param name="WorkShiftSetM">班次信息</param>
       /// <param name="WorkShiftTimeInfos">班段信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InsertAll(string WorkShiftSql, string WorkShiftTimeInfos, string WorkShiftNo,string CompanyCD,string UserID)
        {
           // EquipmentFitModel EquipFitM = new EquipmentFitModel();
            WorkShiftTimeModel WorkShiftTimeM = new WorkShiftTimeModel();
            string[] strarray = null;
            string recorditems = "";
            string[] inseritems = null;
            try
            {
                strarray = WorkShiftTimeInfos.Split('|');
                string[] sqlarray = new string[strarray.Length];
                sqlarray[0] = WorkShiftSql;
                for (int i = 0; i < strarray.Length; i++)
                {
                    StringBuilder WorkShiftTimeSql = new StringBuilder();
                    recorditems = strarray[i];
                    inseritems = recorditems.Split(',');
                    if (recorditems.Length != 0)
                    {
                        WorkShiftTimeM.WorkShiftNo = WorkShiftNo;
                        WorkShiftTimeM.ShiftTimeName = inseritems[1].ToString();
                        WorkShiftTimeM.OnTime = inseritems[2].ToString();
                        WorkShiftTimeM.OnForwordTime =Convert.ToInt32(inseritems[3].ToString());
                        WorkShiftTimeM.OnLateTime =inseritems[4].ToString()==""?0:Convert.ToInt32(inseritems[4].ToString());
                        WorkShiftTimeM.IfAttendance = inseritems[5].ToString();
                        WorkShiftTimeM.IfFlag = inseritems[6].ToString();
                        WorkShiftTimeM.OffTime = inseritems[7].ToString();
                        WorkShiftTimeM.OffForwordTime =inseritems[8].ToString()==""?0:Convert.ToInt32(inseritems[8].ToString());
                        WorkShiftTimeM.OffLateTime =Convert.ToInt32(inseritems[9].ToString());
                        WorkShiftTimeM.AttendanceTimeLong =Convert.ToInt32(inseritems[10].ToString());

                        WorkShiftTimeSql.AppendLine("INSERT INTO officedba.WorkShiftTime");
                        WorkShiftTimeSql.AppendLine("		(WorkShiftNo      ");
                        WorkShiftTimeSql.AppendLine("		,ShiftTimeName        ");
                        WorkShiftTimeSql.AppendLine("		,OnTime        ");
                        WorkShiftTimeSql.AppendLine("		,OnForwordTime        ");
                        WorkShiftTimeSql.AppendLine("		,OnLateTime        ");
                        WorkShiftTimeSql.AppendLine("		,IfAttendance        ");
                        WorkShiftTimeSql.AppendLine("		,IfFlag        ");
                        WorkShiftTimeSql.AppendLine("		,OffTime        ");
                        WorkShiftTimeSql.AppendLine("		,OffForwordTime        ");
                        WorkShiftTimeSql.AppendLine("		,OffLateTime        ");
                        WorkShiftTimeSql.AppendLine("		,AttendanceTimeLong        ");
                        WorkShiftTimeSql.AppendLine("		,CompanyCD        ");
                        WorkShiftTimeSql.AppendLine("		,ModifiedDate        ");
                        WorkShiftTimeSql.AppendLine("		,ModifiedUserID)        ");
                        WorkShiftTimeSql.AppendLine("VALUES                  ");
                        WorkShiftTimeSql.AppendLine("		('" + WorkShiftTimeM.WorkShiftNo + "'     ");
                        WorkShiftTimeSql.AppendLine("		,'" + WorkShiftTimeM.ShiftTimeName + "'       ");
                        WorkShiftTimeSql.AppendLine("		,'" + WorkShiftTimeM.OnTime + "'       ");
                        WorkShiftTimeSql.AppendLine("		," + WorkShiftTimeM.OnForwordTime + "       ");
                        WorkShiftTimeSql.AppendLine("		," + WorkShiftTimeM.OnLateTime + "       ");
                        WorkShiftTimeSql.AppendLine("		," + WorkShiftTimeM.IfAttendance + "       ");
                        WorkShiftTimeSql.AppendLine("		,'" + WorkShiftTimeM.IfFlag + "'       ");
                        WorkShiftTimeSql.AppendLine("		,'" + WorkShiftTimeM.OffTime + "'       ");
                        WorkShiftTimeSql.AppendLine("		," + WorkShiftTimeM.OffForwordTime + "       ");
                        WorkShiftTimeSql.AppendLine("		," + WorkShiftTimeM.OffLateTime + "       ");
                        WorkShiftTimeSql.AppendLine("		," + WorkShiftTimeM.AttendanceTimeLong + "       ");
                        WorkShiftTimeSql.AppendLine("		,'" + CompanyCD + "'       ");
                        WorkShiftTimeSql.AppendLine("		,'" + System.DateTime.Now.ToShortDateString() + "'       ");
                        WorkShiftTimeSql.AppendLine("		,'" + UserID + "')       ");
                        sqlarray[i] = WorkShiftTimeSql.ToString();
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
       #region 更新班次信息
       /// <summary>
       /// 更新班次信息
       /// </summary>
       /// <param name="WorkShiftSetM">班次信息</param>
       /// <param name="WorkShiftTimeInfos">班段信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateEquipMnetAndFitInfo(WorkShiftSetModel WorkShiftSetM, string WorkShiftTimeInfos)
       {
           try
           {
               StringBuilder UpdateWorkShiftSql = new StringBuilder();
               UpdateWorkShiftSql.AppendLine("UPDATE officedba.WorkshiftSet");
               UpdateWorkShiftSql.AppendLine("SET                                      ");
               UpdateWorkShiftSql.AppendLine("		CompanyCD='" + WorkShiftSetM.CompanyCD + "'      ");
               UpdateWorkShiftSql.AppendLine("		,WorkShiftName='" + WorkShiftSetM.WorkShiftName + "'       ");
               UpdateWorkShiftSql.AppendLine("		,DayWorkLong=" + WorkShiftSetM.DayWorkLong + "       ");
               UpdateWorkShiftSql.AppendLine("		,LateAbsent=" + WorkShiftSetM.LateAbsent + "       ");
               UpdateWorkShiftSql.AppendLine("		,ForwardAbsent=" + WorkShiftSetM.ForwardAbsent + "       ");
               UpdateWorkShiftSql.AppendLine("		,WorkOvertime=" + WorkShiftSetM.WorkOvertime + "      ");
               UpdateWorkShiftSql.AppendLine("		,ModifiedDate='" + WorkShiftSetM.ModifiedDate + "'   ");
               UpdateWorkShiftSql.AppendLine("		,ModifiedUserID='" + WorkShiftSetM.ModifiedUserID + "'        ");
               UpdateWorkShiftSql.AppendLine("WHERE  WorkShiftNo='" + WorkShiftSetM.WorkShiftNo + "'                 ");
               return UpdateAll(UpdateWorkShiftSql.ToString(), WorkShiftTimeInfos, WorkShiftSetM.WorkShiftNo, WorkShiftSetM.CompanyCD, WorkShiftSetM.ModifiedUserID);
           }
           catch 
           {
               return false;
           }
       }
       /// <summary>
       /// 更新班次信息
       /// </summary>
       /// <param name="WorkShiftSetM">班次信息</param>
       /// <param name="WorkShiftTimeInfos">班段信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateAll(string UpdateWorkShiftSql, string WorkShiftTimeInfos, string WorkShiftNo, string CompanyCD, string UserID)
       {
           WorkShiftTimeModel WorkShiftTimeM = new WorkShiftTimeModel();
           string[] strarray = null;
           string recorditems = "";
           string[] inseritems = null;
           try
           {
               strarray = WorkShiftTimeInfos.Split('|');
               string[] sqlarray = new string[strarray.Length + 1];
               sqlarray[0] = UpdateWorkShiftSql;
               sqlarray[1] = "DELETE officedba.WorkShiftTime WHERE WorkShiftNo = '" + WorkShiftNo + "'";
               for (int i = 1; i < strarray.Length; i++)
               {
                   StringBuilder WorkShiftTimeSql = new StringBuilder();
                   recorditems = strarray[i];
                   inseritems = recorditems.Split(',');
                   if (recorditems.Length != 0)
                   {
                       WorkShiftTimeM.WorkShiftNo = WorkShiftNo;
                       WorkShiftTimeM.ShiftTimeName = inseritems[1].ToString();
                       WorkShiftTimeM.OnTime = inseritems[2].ToString();
                       WorkShiftTimeM.OnForwordTime = Convert.ToInt32(inseritems[3].ToString());
                       WorkShiftTimeM.OnLateTime = inseritems[4].ToString()==""?0:Convert.ToInt32(inseritems[4].ToString());
                       WorkShiftTimeM.IfAttendance = inseritems[5].ToString();
                       WorkShiftTimeM.IfFlag = inseritems[6].ToString();
                       WorkShiftTimeM.OffTime = inseritems[7].ToString();
                       WorkShiftTimeM.OffForwordTime = inseritems[8].ToString()==""?0:Convert.ToInt32(inseritems[8].ToString());
                       WorkShiftTimeM.OffLateTime = Convert.ToInt32(inseritems[9].ToString());
                       WorkShiftTimeM.AttendanceTimeLong = Convert.ToInt32(inseritems[10].ToString());

                       WorkShiftTimeSql.AppendLine("INSERT INTO officedba.WorkShiftTime");
                       WorkShiftTimeSql.AppendLine("		(WorkShiftNo      ");
                       WorkShiftTimeSql.AppendLine("		,ShiftTimeName        ");
                       WorkShiftTimeSql.AppendLine("		,OnTime        ");
                       WorkShiftTimeSql.AppendLine("		,OnForwordTime        ");
                       WorkShiftTimeSql.AppendLine("		,OnLateTime        ");
                       WorkShiftTimeSql.AppendLine("		,IfAttendance        ");
                       WorkShiftTimeSql.AppendLine("		,IfFlag        ");
                       WorkShiftTimeSql.AppendLine("		,OffTime        ");
                       WorkShiftTimeSql.AppendLine("		,OffForwordTime        ");
                       WorkShiftTimeSql.AppendLine("		,OffLateTime        ");
                       WorkShiftTimeSql.AppendLine("		,AttendanceTimeLong        ");
                       WorkShiftTimeSql.AppendLine("		,CompanyCD        ");
                       WorkShiftTimeSql.AppendLine("		,ModifiedDate        ");
                       WorkShiftTimeSql.AppendLine("		,ModifiedUserID)        ");
                       WorkShiftTimeSql.AppendLine("VALUES                  ");
                       WorkShiftTimeSql.AppendLine("		('" + WorkShiftTimeM.WorkShiftNo + "'     ");
                       WorkShiftTimeSql.AppendLine("		,'" + WorkShiftTimeM.ShiftTimeName + "'       ");
                       WorkShiftTimeSql.AppendLine("		,'" + WorkShiftTimeM.OnTime + "'       ");
                       WorkShiftTimeSql.AppendLine("		," + WorkShiftTimeM.OnForwordTime + "       ");
                       WorkShiftTimeSql.AppendLine("		," + WorkShiftTimeM.OnLateTime + "       ");
                       WorkShiftTimeSql.AppendLine("		," + WorkShiftTimeM.IfAttendance + "       ");
                       WorkShiftTimeSql.AppendLine("		,'" + WorkShiftTimeM.IfFlag + "'       ");
                       WorkShiftTimeSql.AppendLine("		,'" + WorkShiftTimeM.OffTime + "'       ");
                       WorkShiftTimeSql.AppendLine("		," + WorkShiftTimeM.OffForwordTime + "       ");
                       WorkShiftTimeSql.AppendLine("		," + WorkShiftTimeM.OffLateTime + "       ");
                       WorkShiftTimeSql.AppendLine("		," + WorkShiftTimeM.AttendanceTimeLong + "       ");
                       WorkShiftTimeSql.AppendLine("		,'" + CompanyCD + "'       ");
                       WorkShiftTimeSql.AppendLine("		,'" + System.DateTime.Now.ToShortDateString() + "'       ");
                       WorkShiftTimeSql.AppendLine("		,'" + UserID + "')       ");
                       sqlarray[i + 1] = WorkShiftTimeSql.ToString();
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
       #region 查询班次列表
       /// <summary>
       /// 查询班次列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetWorkShiftInfo(string WorkShiftNo, string WorkShiftName, string CompanyCD)
       {
           string sql = "SELECT ID"
                      +",CompanyCD"
                      +",WorkShiftNo"
                      +",WorkShiftName"
                      +",DayWorkLong"
                      +",LateAbsent"
                      +",ForwardAbsent"
                      +",WorkOvertime"
                      +",ModifiedDate"
                      +",ModifiedUserID"
                      + " FROM officedba.WorkshiftSet WHERE CompanyCD='" + CompanyCD + "'";
           if (WorkShiftNo != "")
               sql += " and WorkShiftNo LIKE '%" + WorkShiftNo + "%'";
           if (WorkShiftName != "")
               sql += " and WorkShiftName LIKE '%" + WorkShiftName + "%'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 根据recordno获取班次信息以供查看或修改
       /// <summary>
       /// 根据recordno获取班次信息以供查看或修改
       /// </summary>
       /// <param name="WorkShiftNo">班次编号</param>
       /// <returns>DataTable</returns>
       public static DataTable GetWorkShiftInfoByWorkshiftNo(string WorkShiftNo)
       {
           string sql = "SELECT a.WorkShiftName, a.DayWorkLong, a.LateAbsent," 
                      +"a.ForwardAbsent, a.WorkOvertime, b.WorkShiftNo, "
                      +"b.ShiftTimeName, b.OnTime, b.OnForwordTime, "
                      +"b.OnLateTime, b.IfAttendance, b.IfFlag, "
                      +"b.OffTime, b.OffForwordTime, b.OffLateTime, "
                      +"b.AttendanceTimeLong "
                      +"FROM officedba.WorkshiftSet a INNER JOIN "
                      +"officedba.WorkShiftTime b "
                      + "ON a.WorkShiftNo = b.WorkShiftNo WHERE a.WorkShiftNo='" + WorkShiftNo.Trim() + "'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 删除设班次信息
       /// <summary>
       /// 删除设班次信息
       /// </summary>
       /// <param name="WorkShiftIds">班次编号IDS</param>
       /// <returns>删除是否成功 false:失败，true:成功</returns>
       public static bool DelWorkShiftInfo(string WorkShiftIds)
       {
           string allEquipID = "";
           System.Text.StringBuilder sb = new System.Text.StringBuilder();
           string[] Delsql = new string[2];
           try
           {
               string[] EquipIDS = null;
               EquipIDS = WorkShiftIds.Split(',');

               for (int i = 0; i < EquipIDS.Length; i++)
               {
                   EquipIDS[i] = "'" + EquipIDS[i] + "'";
                   sb.Append(EquipIDS[i]);
               }

               allEquipID = sb.ToString().Replace("''", "','");
               Delsql[0] = "DELETE FROM officedba.WorkshiftSet WHERE WorkShiftNo IN (" + allEquipID + ")";
               Delsql[1] = "DELETE FROM officedba.WorkShiftTime WHERE WorkShiftNo IN (" + allEquipID + ")";
               SqlHelper.ExecuteTransForListWithSQL(Delsql);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion
       #region 获取班次下拉列表
       /// <summary>
       /// 获取班次下拉列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable BindWorkShift(string CompanyCD)
       {
           string sql = "SELECT  ID,WorkShiftNo,WorkShiftName"
                        +" FROM officedba.WorkshiftSet"
                        + " WHERE CompanyCD = '" + CompanyCD + "'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 班次列表导出
       /// <summary>
       /// 班次列表导出
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetWorkShiftInfoForExp(string WorkShiftNo, string WorkShiftName, string CompanyCD, string ord)
       {
           string sql = "SELECT ID"
                      + ",CompanyCD"
                      + ",WorkShiftNo"
                      + ",WorkShiftName"
                      + ",DayWorkLong"
                      + ",LateAbsent"
                      + ",ForwardAbsent"
                      + ",WorkOvertime"
                      + ",ModifiedDate"
                      + ",ModifiedUserID"
                      + " FROM officedba.WorkshiftSet WHERE CompanyCD='" + CompanyCD + "'";
           if (WorkShiftNo != "")
               sql += " and WorkShiftNo LIKE '%" + WorkShiftNo + "%'";
           if (WorkShiftName != "")
               sql += " and WorkShiftName LIKE '%" + WorkShiftName + "%'";
           sql = sql + ord;
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
    }
}

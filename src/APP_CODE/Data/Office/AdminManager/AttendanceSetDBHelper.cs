/**********************************************
 * 类作用：   考勤设置数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/03/30
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
    /// 类名：AttendanceSetDBHelper
    /// 描述：考勤设置数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/03/20
    /// </summary>
   public class AttendanceSetDBHelper
   {
       #region 工作日设置添加操作
       /// <summary>
        /// 工作日设置添加操作
        /// </summary>
        /// <param name="AttendanceInfo">工作日信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InsertAttendanceInfo(string AttendanceInfo, string CompanyID, string UserID)
        {
            AttendanceSetModel AttendanceSetM = new AttendanceSetModel();
            string[] strarray = null;
            try
            {
                strarray = AttendanceInfo.Split(',');
                string[] sqlarray = new string[strarray.Length];
                string Attendancesql = "";
                sqlarray[0] = "DELETE FROM OFFICEDBA.WorkDay";
                for (int i = 0; i < strarray.Length; i++)
                {
                    string recorditem = strarray[i];
                    if (recorditem != "")
                    {
                        Attendancesql = "INSERT INTO OFFICEDBA.WorkDay(CompanyCD,WeekDay,ModifiedDate,ModifiedUserID)"
                        + "VALUES ('"+CompanyID+"',"+Convert.ToInt32(recorditem)+",'"+System.DateTime.Now+"','"+UserID+"')";
                        sqlarray[i + 1] = Attendancesql;
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
       #region 获取工作日初始化页面
       /// <summary>
       /// 获取工作日初始化页面
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetAttcedanceSet()
       {
           string sql = "SELECT WeekDay FROM officedba.WorkDay";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
    }
}

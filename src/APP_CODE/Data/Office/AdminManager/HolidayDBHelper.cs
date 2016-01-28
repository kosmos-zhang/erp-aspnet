/**********************************************
 * 类作用：   考勤节假日设置数据库层处理
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
    /// 类名：HolidayDBHelper
    /// 描述：考勤节假日设置数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/03/20
    /// </summary>
   public class HolidayDBHelper
   {
        #region 添加节假日设置信息
        /// <summary>
        /// 添加节假日设置信息
        /// </summary>
       /// <param name="EquipUselessModel">添加节假日设置信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddHolidaySetInfo(HolidayModel HolidayM)
        {
            try
            {
                #region 添加节假日设置信息SQL拼写
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO officedba.Holiday");
                sql.AppendLine("		(CompanyCD      ");
                sql.AppendLine("		,AttendanceDate        ");
                sql.AppendLine("		,StartDate        ");
                sql.AppendLine("		,EndDate        ");
                sql.AppendLine("		,Remark        ");
                sql.AppendLine("		,ModifiedDate        ");
                sql.AppendLine("		,ModifiedUserID)        ");
                sql.AppendLine("VALUES                  ");
                sql.AppendLine("		(@CompanyCD   ");
                sql.AppendLine("		,@AttendanceDate       ");
                sql.AppendLine("		,@StartDate       ");
                sql.AppendLine("		,@EndDate       ");
                sql.AppendLine("		,@Remark       ");
                sql.AppendLine("		,@ModifiedDate       ");
                sql.AppendLine("		,@ModifiedUserID)       ");
                #endregion
                #region 添加设备报废参数设置
                SqlParameter[] param;
                param = new SqlParameter[7];
                param[0] = SqlHelper.GetParameter("@CompanyCD", HolidayM.CompanyCD);
                param[1] = SqlHelper.GetParameter("@AttendanceDate", HolidayM.AttendanceDate);
                param[2] = SqlHelper.GetParameter("@StartDate", HolidayM.StartDate);
                param[3] = SqlHelper.GetParameter("@EndDate", HolidayM.EndDate);
                param[4] = SqlHelper.GetParameter("@Remark", HolidayM.Remark);
                param[5] = SqlHelper.GetParameter("@ModifiedDate", HolidayM.ModifiedDate);
                param[6] = SqlHelper.GetParameter("@ModifiedUserID", HolidayM.ModifiedUserID);
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
        #region 获取节假日信息列表
       /// <summary>
       /// 获取节假日信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetHolidayInfo(string CompanyID)
       {
           string sql = "SELECT ID, CompanyCD, AttendanceDate, StartDate, EndDate, isnull(Remark,'') Remark"
                        +" FROM officedba.Holiday WHERE CompanyCD='"+CompanyID+"'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
        #region 更新节假日设置信息
       /// <summary>
       /// 更新节假日设置信息
       /// </summary>
       /// <param name="EquipUselessModel">节假日设置信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateHolidaySetInfo(HolidayModel HolidayM, string HolidayID)
       {
           try
           {
               #region 添加节假日设置信息SQL拼写
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.Holiday SET ");
               sql.AppendLine("		CompanyCD=@CompanyCD      ");
               sql.AppendLine("		,AttendanceDate=@AttendanceDate        ");
               sql.AppendLine("		,StartDate=@StartDate        ");
               sql.AppendLine("		,EndDate=@EndDate        ");
               sql.AppendLine("		,Remark=@Remark        ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
               sql.AppendLine("		WHERE ID=@HolidayID       ");
               #endregion
               #region 添加设备报废参数设置
               SqlParameter[] param;
               param = new SqlParameter[8];
               param[0] = SqlHelper.GetParameter("@CompanyCD", HolidayM.CompanyCD);
               param[1] = SqlHelper.GetParameter("@AttendanceDate", HolidayM.AttendanceDate);
               param[2] = SqlHelper.GetParameter("@StartDate", HolidayM.StartDate);
               param[3] = SqlHelper.GetParameter("@EndDate", HolidayM.EndDate);
               param[4] = SqlHelper.GetParameter("@Remark", HolidayM.Remark);
               param[5] = SqlHelper.GetParameter("@ModifiedDate", HolidayM.ModifiedDate);
               param[6] = SqlHelper.GetParameter("@ModifiedUserID", HolidayM.ModifiedUserID);
               param[7] = SqlHelper.GetParameter("@HolidayID", HolidayID);
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
        #region 删除节假日信息
       /// <summary>
       /// 删除节假日信息
       /// </summary>
       /// <param name="HolidayIDS">节假日IDS</param>
       /// <returns>删除是否成功 false:失败，true:成功</returns>
       public static bool DelHolidayInfo(string HolidayIDS)
       {
           string allEquipID = "";
           System.Text.StringBuilder sb = new System.Text.StringBuilder();
           string[] Delsql = new string[1];
           try
           {
               string[] EquipIDS = null;
               EquipIDS = HolidayIDS.Split(',');

               for (int i = 0; i < EquipIDS.Length; i++)
               {
                   EquipIDS[i] = "'" + EquipIDS[i] + "'";
                   sb.Append(EquipIDS[i]);
               }

               allEquipID = sb.ToString().Replace("''", "','");
               Delsql[0] = "DELETE FROM officedba.Holiday WHERE ID IN (" + allEquipID + ")";
               SqlHelper.ExecuteTransForListWithSQL(Delsql);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion
        #region 判断能否添加新的节假日信息
       /// <summary>
       /// 判断能否添加新的节假日信息
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码</param>
       /// <returns></returns>
       public static bool IsExistInfo(string HolidayName, string CompanyCD)
       {

           string sql = "SELECT * FROM officedba.Holiday WHERE AttendanceDate='" + HolidayName + "' AND CompanyCD='" + CompanyCD + "'";
           return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
       }
       #endregion
    }
}

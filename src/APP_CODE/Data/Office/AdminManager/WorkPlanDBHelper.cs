/**********************************************
 * 类作用：   考勤排班设置数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/10
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
    /// 类名：WorkPlanDBHelper
    /// 描述：考勤排班设置数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/04/10
    /// </summary>
   public class WorkPlanDBHelper
   {
       #region 添加排班设置信息
          /// <summary>
          /// 添加排班设置信息
          /// </summary>
         /// <param name="WorkPlanSetM">排班信息</param>
         /// <param name="workplanarraryinfo">排班设置</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddWorkPlanInfo(WorkPlanModel WorkPlanSetM, string workplanarraryinfo)
        {
            try
            {
                string[] strarray = null;
                string recorditems = "";
                string[] inseritems = null;
                string IsRestDay = "";
                try
                {
                    strarray = workplanarraryinfo.Split('|');
                    string[] sqlarray = new string[strarray.Length-1];
                    for (int i = 0; i < strarray.Length; i++)
                    {
                        StringBuilder WorkPlanSql = new StringBuilder();
                        recorditems = strarray[i];
                        inseritems = recorditems.Split(',');
                        if (recorditems.Length != 0)
                        {
                            WorkPlanSetM.WorkShiftIndex = inseritems[0].ToString();
                            WorkPlanSetM.WorkShiftNo = inseritems[2].ToString();
                            IsRestDay = inseritems[3].ToString();

                            WorkPlanSql.AppendLine("INSERT INTO officedba.WorkPlan");
                            WorkPlanSql.AppendLine("		(CompanyCD      ");
                            WorkPlanSql.AppendLine("		,WorkGroupNo        ");
                            WorkPlanSql.AppendLine("		,WorkShiftIndex        ");
                            WorkPlanSql.AppendLine("		,WorkShiftNo        ");
                            WorkPlanSql.AppendLine("		,WorkPlanStartDate        ");
                            WorkPlanSql.AppendLine("		,ModifiedDate        ");
                            WorkPlanSql.AppendLine("		,IsRestDay        ");
                            WorkPlanSql.AppendLine("		,ModifiedUserID)        ");
                            WorkPlanSql.AppendLine("VALUES                  ");
                            WorkPlanSql.AppendLine("		('" + WorkPlanSetM.CompanyCD + "'     ");
                            WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.WorkGroupNo + "'       ");
                            WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.WorkShiftIndex + "'       ");
                            WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.WorkShiftNo + "'       ");
                            WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.WorkPlanStartDate + "'       ");
                            WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.ModifiedDate + "'       ");
                            WorkPlanSql.AppendLine("		,'" + IsRestDay + "'       ");
                            WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.ModifiedUserID + "')       ");
                            sqlarray[i - 1] = WorkPlanSql.ToString();
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
            catch 
            {
                return false;
            }
        }
        #endregion
       #region 更新排班信息根据开始时间
       /// <summary>
       /// 更新排班信息根据开始时间
       /// </summary>
       /// <param name="WorkPlanSetM">排班信息</param>
       /// <param name="workplanarraryinfo">排班设置</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateWorkPlanInfoByDate(WorkPlanModel WorkPlanSetM, string workplanarraryinfo)
       {
           try
           {
               string[] strarray = null;
               string recorditems = "";
               string[] inseritems = null;
               try
               {
                   strarray = workplanarraryinfo.Split('|');
                   string[] sqlarray = new string[strarray.Length];
                   string IsRestDay = "";
                   sqlarray[0] = "DELETE FROM officedba.WorkPlan WHERE CompanyCD='" + WorkPlanSetM.CompanyCD + "' and WorkGroupNo='" + WorkPlanSetM .WorkGroupNo+ "'"
                          + "and WorkPlanStartDate='" + WorkPlanSetM .WorkPlanStartDate+ "' and WorkPlanEndDate is null";
                   for (int i = 0; i < strarray.Length; i++)
                   {
                       StringBuilder WorkPlanSql = new StringBuilder();
                       recorditems = strarray[i];
                       inseritems = recorditems.Split(',');
                       if (recorditems.Length != 0)
                       {
                           WorkPlanSetM.WorkShiftIndex = inseritems[0].ToString();
                           WorkPlanSetM.WorkShiftNo = inseritems[2].ToString();
                           IsRestDay = inseritems[3].ToString();

                           WorkPlanSql.AppendLine("INSERT INTO officedba.WorkPlan");
                           WorkPlanSql.AppendLine("		(CompanyCD      ");
                           WorkPlanSql.AppendLine("		,WorkGroupNo        ");
                           WorkPlanSql.AppendLine("		,WorkShiftIndex        ");
                           WorkPlanSql.AppendLine("		,WorkShiftNo        ");
                           WorkPlanSql.AppendLine("		,WorkPlanStartDate        ");
                           WorkPlanSql.AppendLine("		,ModifiedDate        ");
                           WorkPlanSql.AppendLine("		,IsRestDay        ");
                           WorkPlanSql.AppendLine("		,ModifiedUserID)        ");
                           WorkPlanSql.AppendLine("VALUES                  ");
                           WorkPlanSql.AppendLine("		('" + WorkPlanSetM.CompanyCD + "'     ");
                           WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.WorkGroupNo + "'       ");
                           WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.WorkShiftIndex + "'       ");
                           WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.WorkShiftNo + "'       ");
                           WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.WorkPlanStartDate + "'       ");
                           WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.ModifiedDate + "'       ");
                           WorkPlanSql.AppendLine("		,'" + IsRestDay + "'       ");
                           WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.ModifiedUserID + "')       ");
                           sqlarray[i] = WorkPlanSql.ToString();
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
       /// <param name="WorkPlanSetM">排班信息</param>
       /// <param name="workplanarraryinfo">排班设置</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateWorkPlanInfo(WorkPlanModel WorkPlanSetM, string workplanarraryinfo)
       {
           try
           {
               string[] strarray = null;
               string recorditems = "";
               string[] inseritems = null;
               try
               {
                   strarray = workplanarraryinfo.Split('|');
                   string[] sqlarray = new string[strarray.Length];
                   string IsRestDay = "";
                   sqlarray[0] = "UPDATE officedba.WorkPlan SET WorkPlanEndDate='" + WorkPlanSetM.WorkPlanStartDate + "',"
                                 +"ModifiedDate='" + WorkPlanSetM.ModifiedDate + "',ModifiedUserID='" + WorkPlanSetM .ModifiedUserID+ "' "
                                 +" WHERE CompanyCD='" + WorkPlanSetM.CompanyCD + "' and WorkGroupNo='" + WorkPlanSetM.WorkGroupNo + "'"
                                 + "and WorkPlanStartDate<'" + WorkPlanSetM.WorkPlanStartDate + "' and WorkPlanEndDate is null";
                   for (int i = 0; i < strarray.Length; i++)
                   {
                       StringBuilder WorkPlanSql = new StringBuilder();
                       recorditems = strarray[i];
                       inseritems = recorditems.Split(',');
                       if (recorditems.Length != 0)
                       {
                           WorkPlanSetM.WorkShiftIndex = inseritems[0].ToString();
                           WorkPlanSetM.WorkShiftNo = inseritems[2].ToString();

                           IsRestDay = inseritems[3].ToString();

                           WorkPlanSql.AppendLine("INSERT INTO officedba.WorkPlan");
                           WorkPlanSql.AppendLine("		(CompanyCD      ");
                           WorkPlanSql.AppendLine("		,WorkGroupNo        ");
                           WorkPlanSql.AppendLine("		,WorkShiftIndex        ");
                           WorkPlanSql.AppendLine("		,WorkShiftNo        ");
                           WorkPlanSql.AppendLine("		,WorkPlanStartDate        ");
                           WorkPlanSql.AppendLine("		,ModifiedDate        ");
                           WorkPlanSql.AppendLine("		,IsRestDay        ");
                           WorkPlanSql.AppendLine("		,ModifiedUserID)        ");
                           WorkPlanSql.AppendLine("VALUES                  ");
                           WorkPlanSql.AppendLine("		('" + WorkPlanSetM.CompanyCD + "'     ");
                           WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.WorkGroupNo + "'       ");
                           WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.WorkShiftIndex + "'       ");
                           WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.WorkShiftNo + "'       ");
                           WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.WorkPlanStartDate + "'       ");
                           WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.ModifiedDate + "'       ");
                           WorkPlanSql.AppendLine("		,'" + IsRestDay + "'       ");
                           WorkPlanSql.AppendLine("		,'" + WorkPlanSetM.ModifiedUserID + "')       ");
                           sqlarray[i] = WorkPlanSql.ToString();
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
           catch 
           {
               return false;
           }
       }
       #endregion
       #region 判断是插入还是更新某阶段的排班
        /// <summary>
        /// 判断是插入还是更新某阶段的排班
        /// </summary>
        /// <param name="CompanyID">公司代码</param>
        /// <param name="WorkGroupNo">班组号</param>
        /// <param name="WorkPlanStartDate">排班开始日期</param>
        /// <returns></returns>
        public static int InsertOrUpdateWorkPlanInfo(string CompanyID,string WorkGroupNo,string WorkPlanStartDate)
        {
            string sql = "select isnull(max(id),0) as IsExist from  officedba.WorkPlan  "
                          +"where "
                          + "CompanyCD='" + CompanyID + "' and WorkGroupNo='" + WorkGroupNo + "' "
                          + "and WorkPlanStartDate<'" + WorkPlanStartDate + "' and WorkPlanEndDate is null";
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
       #region 日期相等时的判断
        /// <summary>
        /// 日期相等时的判断
        /// </summary>
        /// <param name="CompanyID">公司代码</param>
        /// <param name="WorkGroupNo">班组号</param>
        /// <param name="WorkPlanStartDate">排班开始日期</param>
        /// <returns></returns>
        public static int InsertOrUpdateWorkPlanInfoByDate(string CompanyID, string WorkGroupNo, string WorkPlanStartDate)
        {
            string sql = "select isnull(max(id),0) as IsExist from  officedba.WorkPlan  "
                          + "where "
                          + "CompanyCD='" + CompanyID + "' and WorkGroupNo='" + WorkGroupNo + "' "
                          + "and WorkPlanStartDate='" + WorkPlanStartDate + "' and WorkPlanEndDate is null";
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
       #region 判断能否排班
        /// <summary>
        /// 判断能否排班
        /// </summary>
        /// <param name="CompanyID">公司代码</param>
        /// <param name="WorkGroupNo">班组号</param>
        /// <param name="WorkPlanStartDate">排班开始日期</param>
        /// <returns></returns>
        public static int IfInsertOrUpdate(string CompanyID, string WorkGroupNo, string WorkPlanStartDate)
        {
            string sql = "select isnull(max(id),0) as IsExist from  officedba.WorkPlan  "
                          + "where "
                          + "CompanyCD='" + CompanyID + "' and WorkGroupNo='" + WorkGroupNo + "' "
                          + "and WorkPlanStartDate<='" + WorkPlanStartDate + "' and WorkPlanEndDate >'" + WorkPlanStartDate + "'";
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
        #region  选择某一班组查看排班信息
        /// <summary>
        /// 选择某一班组查看排班信息
        /// </summary>
        /// <param name="GroupNo">班组编号</param>
        /// <returns>DataTable</returns>
        public static DataTable GetGroupPlanByGroupNo(string GroupNo, string CompanyID)
        {
            string sql = "select a.WorkGroupNo,a.WorkShiftIndex,a.WorkShiftNo,"
                            +"a.WorkPlanStartDate,b.WorkShiftName "
                            +"from officedba.WorkPlan a "
                            +"LEFT OUTER JOIN officedba.WorkshiftSet b "
                            +"ON a.WorkShiftNo=b.WorkShiftNo "
                            + "WHERE a.WorkGroupNo='" + GroupNo + "' AND a.CompanyCD='" + CompanyID + "' "
                            +"AND a.WorkPlanEndDate is null";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
    }
}

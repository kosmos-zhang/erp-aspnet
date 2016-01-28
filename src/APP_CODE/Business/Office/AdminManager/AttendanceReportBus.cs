/**********************************************
 * 类作用：   考勤生成报表事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/23
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;

namespace XBase.Business.Office.AdminManager
{
    /// <summary>
    /// 类名：AttendanceReportBus
    /// 描述：生成考勤报表事务层处理
    /// 作者：lysong
    /// 创建时间：2009/04/23
    /// </summary>
   public class AttendanceReportBus
    {
        #region 添加考勤报表信息
        /// <summary>
        /// 添加报表信息
        /// </summary>
        /// <param name="AttendanceReportM">报表信息</param>
        /// <param name="reportdatas">报表详细</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddAttendanceReportInfo(AttendanceReportModel AttendanceReportM, string reportdatas)
        {
            return AttendanceReportDBHelper.AddAttendanceReportInfo(AttendanceReportM, reportdatas);
        }
        #endregion

        #region 查询考勤报表
       /// <summary>
       /// 查询考勤报表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable SearchReportData(string CompanyID,string ReportNo,string ReportName,string BelongMonth,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           try
           {
               return AttendanceReportDBHelper.SearchReportData(CompanyID, ReportNo, ReportName, BelongMonth, pageIndex, pageCount, ord, ref TotalCount);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 修改考勤报表信息
       /// <summary>
       /// 修改考勤报表信息
       /// </summary>
       /// <param name="AttendanceReportM">报表信息</param>
       /// <param name="reportdatas">报表详细</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateAttendanceReportInfo(AttendanceReportModel AttendanceReportM, string reportdatas)
       {
           return AttendanceReportDBHelper.UpdateAttendanceReportInfo(AttendanceReportM, reportdatas);
       }
       #endregion

       #region 保存报表调整信息
       /// <summary>
       /// 保存报表调整信息
       /// </summary>
       /// <param name="AttendanceReportM">报表信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool SaveAttendanceChangeInfo(AttendanceReportDetailModel AttendanceReportDetailM, string AttendanceType)
       {
           return AttendanceReportDBHelper.SaveAttendanceChangeInfo(AttendanceReportDetailM, AttendanceType);
       }
       #endregion

       #region 确认报表信息
       /// <summary>
       /// 确认报表信息
       /// </summary>
       /// <param name="AttendanceReportM">报表信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool ConfirmAttendanceChangeInfo(AttendanceReportModel AttendanceReportM)
       {
           return AttendanceReportDBHelper.ConfirmAttendanceChangeInfo(AttendanceReportM);
       }
       #endregion
       
    }
}

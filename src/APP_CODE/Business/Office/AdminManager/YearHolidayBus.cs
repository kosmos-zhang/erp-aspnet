/**********************************************
 * 类作用：   考勤年休假事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/03/31
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;


namespace XBase.Business.Office.AdminManager
{
    /// <summary>
    /// 类名：YearHolidayBus
    /// 描述：考勤年休假事务层处理
    /// 作者：lysong
    /// 创建时间：2009/03/31
    /// </summary>
   public class YearHolidayBus
    {
        #region 获取user
        /// <summary>
        /// 获取user
        /// </summary>
        /// <returns></returns>
       public static DataTable GetUserList()
        {
            return YearHolidayDBHelper.GetUserList();
        }
        #endregion
        #region 年休假设置信息添加
       /// <summary>
       /// 年休假设置信息添加
       /// </summary>
       /// <param name="YearHolidayM">YearHolidayM</param>
       /// <param name="StrYearHoliday">年休假设置信息</param>
      /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static string  AddYearHolidayInfoSet(YearHolidayModel YearHolidayM, string StrYearHoliday)
       {
           return YearHolidayDBHelper.AddYearHolidayInfoSet(YearHolidayM, StrYearHoliday);
       }
       #endregion
        #region  获取年休假信息
       /// <summary>
       /// 获取年休假信息
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetYearHolidayInfo(string UserName, string EmployNo, string CompanyID)
       {
           try
           {
               return YearHolidayDBHelper.GetYearHolidayInfo(UserName, EmployNo, CompanyID);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion
    }
}

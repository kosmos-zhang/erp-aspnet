/**********************************************
 * 类作用：   考勤节假日事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/03/30
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;

namespace XBase.Business.Office.AdminManager
{
    /// <summary>
    /// 类名：HolidayBus
    /// 描述：考勤节假日事务层处理
    /// 作者：lysong
    /// 创建时间：2009/03/30
    /// </summary>
   public class HolidayBus
    {
        #region 添加节假日设置信息
        /// <summary>
        /// 添加节假日设置信息
        /// </summary>
        /// <param name="HolidayM">节假日设置信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddHolidaySetInfo(HolidayModel HolidayM)
        {
            return HolidayDBHelper.AddHolidaySetInfo(HolidayM);
        }
        #endregion
        #region 获取节假日信息列表
       /// <summary>
       /// 获取节假日信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetHolidayInfo(string CompanyID)
       {
           try
           {
               return HolidayDBHelper.GetHolidayInfo(CompanyID);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion
        #region 更新节假日设置信息
       /// <summary>
       /// 更新节假日设置信息
       /// </summary>
       /// <param name="HolidayM">节假日设置信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateHolidaySetInfo(HolidayModel HolidayM,string HolidayID)
       {
           return HolidayDBHelper.UpdateHolidaySetInfo(HolidayM,HolidayID);
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
           return HolidayDBHelper.DelHolidayInfo(HolidayIDS);
       }
       #endregion
    }
}

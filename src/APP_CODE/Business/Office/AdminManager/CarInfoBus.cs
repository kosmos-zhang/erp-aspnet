/**********************************************
 * 类作用：   车辆管理事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/25
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;
namespace XBase.Business.Office.AdminManager
{
    /// <summary>
    /// 类名：CarInfoBus
    /// 描述：车辆管理事务层处理
    /// 作者：lysong
    /// 创建时间：2009/04/25
    /// </summary>
   public class CarInfoBus
    {
       #region 添加车辆信息
        /// <summary>
        /// 添加车辆信息
        /// </summary>
        /// <param name="CarInfoM">车辆信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddCarInfo(CarInfoModel CarInfoM)
        {
            return CarInfoDBHelper.AddCarInfo(CarInfoM);
        }
        #endregion
       #region 修改车辆信息
       /// <summary>
       /// 修改车辆信息
       /// </summary>
       /// <param name="CarInfoM">车辆信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateCarInfo(CarInfoModel CarInfoM)
       {
           return CarInfoDBHelper.UpdateCarInfo(CarInfoM);
       }
       #endregion
       #region 查询车辆信息列表
       /// <summary>
       /// 查询车辆信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetCarInfoList(string CarNo, string CarName, string CarMark, string CarType, string CompanyID,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           try
           {
               return CarInfoDBHelper.GetCarInfoList(CarNo, CarName, CarMark, CarType, CompanyID, pageIndex, pageCount, ord, ref TotalCount);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion
       #region  由车辆编号获取信息，查看或修改
       /// <summary>
       /// 由车辆编号获取信息，查看或修改
       /// </summary>
       /// <param name="CarNo">车辆编号</param>
       /// <returns>DataTable</returns>
       public static DataTable GetCarInfoByCarNo(string CarNo,string CompanyID)
       {
           try
           {
               return CarInfoDBHelper.GetCarInfoByCarNo(CarNo, CompanyID);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion
    }
}

/**********************************************
 * 类作用：   车辆申请事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/27
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;

namespace XBase.Business.Office.AdminManager
{
    /// <summary>
    /// 类名：CarApplyBus
    /// 描述：车辆申请事务层处理
    /// 作者：lysong
    /// 创建时间：2009/04/27
    /// </summary>
   public class CarApplyBus
    {
       #region 获取车编号下拉列表
        /// <summary>
        /// 获取车编号下拉列表
        /// </summary>
        /// <returns></returns>
       public static DataTable GetCarNo(string CompanyCD)
        {
            return CarApplyDBHelper.GetCarNo(CompanyCD);
        }
        #endregion

       #region 添加车辆申请信息
       /// <summary>
       /// 添加车辆申请信息
       /// </summary>
       /// <param name="CarApplyM">车辆申请信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InsertCarApplyInfoData(CarApplyModel CarApplyM,out int RetValID)
       {
           return CarApplyDBHelper.InsertCarApplyInfoData(CarApplyM,out RetValID);
       }
       #endregion

       #region 修改车辆申请信息
       /// <summary>
       /// 修改车辆申请信息
       /// </summary>
       /// <param name="CarApplyM">车辆申请信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateCarApplyInfoData(CarApplyModel CarApplyM)
       {
           return CarApplyDBHelper.UpdateCarApplyInfoData(CarApplyM);
       }
       #endregion

       #region 查询车辆申请信息列表
       /// <summary>
       /// 查询车辆申请信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetCarApplyList(string CompanyID, string RecordNo, string ApplyTitle, string CarNo, string CarMark, string JoinUser, string FlowStatus,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           try
           {
               return CarApplyDBHelper.GetCarApplyList(CompanyID, RecordNo, ApplyTitle, CarNo, CarMark, JoinUser, FlowStatus, pageIndex, pageCount, ord, ref TotalCount);
           }
              
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region  由车辆申请单据编号获取信息，查看或修改
       /// <summary>
       /// 由车辆申请单据编号获取信息，查看或修改
       /// </summary>
       /// <param name="CarApplyNo">车辆申请单据编号</param>
       /// <returns>DataTable</returns>
       public static DataTable GetCarApplyByCarApplyNo(string CarApplyNo,string NO, string CompanyID)
       {
           try
           {
               return CarApplyDBHelper.GetCarApplyByCarApplyNo(CarApplyNo,NO, CompanyID);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 更新车辆申请确认信息
       /// <summary>
       /// 更新车辆申请确认信息
       /// </summary>
       /// <param name="CarApplyM">车辆申请信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateCarApplyConfirm(string BillStatus,string Confirmor,string ConfirmDate,string ID,string userID)
       {
           return CarApplyDBHelper.UpdateCarApplyConfirm(BillStatus, Confirmor, ConfirmDate, ID, userID);
       }
       #endregion
    }
}

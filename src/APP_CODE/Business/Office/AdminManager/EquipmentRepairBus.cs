/**********************************************
 * 类作用：   设备维修事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/03/26
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;
namespace XBase.Business.Office.AdminManager
{
   public class EquipmentRepairBus
    {
        #region 添加设备维修信息 
        /// <summary>
        /// 添加设备维修信息
        /// </summary>
        /// <param name="EquipRepairModel">设备维修信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddEquipmentRepairInfo(EquipmentRepairModel EquipRepairModel, out int RetValID,int CreateUserID,string CreateDate)
        {
            return EquipmentRepairDBHelper.AddEquipmentRepairInfo(EquipRepairModel,out RetValID,CreateUserID, CreateDate);
        }
        #endregion
        #region 修改设备维修信息
       /// <summary>
       /// 修改设备维修信息
       /// </summary>
       /// <param name="EquipRepairModel">修改设备维修信息</param>
       /// <returns>修改是否成功 false:失败，true:成功</returns>
       public static bool UpdateEquipmentRepairInfo(EquipmentRepairModel EquipRepairModel)
       {
           return EquipmentRepairDBHelper.UpdateEquipmentRepairInfo(EquipRepairModel);
       }
       #endregion
        #region 查询设备维修信息列表
       /// <summary>
       /// 查询设备维修信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentRepairInfoBycondition(EquipmentRepairModel EquipmentRepairM, string EquipName, string EquipIndex, string FlowStatus,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           try
           {
               return EquipmentRepairDBHelper.GetEquipmentRepairInfoBycondition(EquipmentRepairM, EquipName, EquipIndex, FlowStatus, pageIndex, pageCount, ord, ref TotalCount);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion
        #region  根据领用单据号获取设备维修信息以查看或修改
       /// <summary>
       /// 根据领用单据号获取设备维修信息以查看或修改
       /// </summary>
       /// <param name="RecordNo">设备维修单据号</param>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentRepairInfoByRecordNo(string RecordNo, string CompanyID)
       {
           try
           {
               return EquipmentRepairDBHelper.GetEquipmentReceiveInfoByRecordNo(RecordNo,CompanyID);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion
        #region 删除设备维修信息
       /// <summary>
       /// 删除设备维修信息
       /// </summary>
       /// <param name="EquipReceiveNos">设备单据号</param>
       /// <returns>删除是否成功 false:失败，true:成功</returns>
       public static bool DelEquipRepairInfo(string EquipRepairNos)
       {
           return EquipmentRepairDBHelper.DelEquipRepairInfo(EquipRepairNos);
       }
       #endregion
       #region 修改设备完成维修信息
       /// <summary>
       /// 修改设备完成维修信息
       /// </summary>
       /// <param name="EquipRepairModel">修改设备完成维修信息</param>
       /// <returns>修改是否成功 false:失败，true:成功</returns>
       public static bool UpdateEquipmentCompleteRepairInfo(EquipmentRepairModel EquipRepairModel)
       {
           return EquipmentRepairDBHelper.UpdateEquipmentCompleteRepairInfo(EquipRepairModel);
       }
       #endregion

    }
}

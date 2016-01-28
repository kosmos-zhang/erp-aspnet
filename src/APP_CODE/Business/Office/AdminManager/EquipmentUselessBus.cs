/**********************************************
 * 类作用：   设备事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/03/28
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;

namespace XBase.Business.Office.AdminManager
{
   public class EquipmentUselessBus
    {
        #region 添加设备报废信息
        /// <summary>
        /// 添加设备报废信息
        /// </summary>
        /// <param name="EquipRepairModel">设备报废信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool AddEquipmentUselessInfo(EquipmentUselessModel EquipUselessModel, out int RetValID,int CreateUserID,string CreateDate)
        {
            return EquipmentUselessDBHelper.AddEquipmentUselessInfo(EquipUselessModel, out RetValID, CreateUserID, CreateDate);
        }
        #endregion
        #region 修改设备报废信息
        /// <summary>
        /// 修改设备报废信息
        /// </summary>
        /// <param name="EquipRepairModel">设备报废信息</param>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateEquipmentUserlessInfo(EquipmentUselessModel EquipUselessModel)
        {
            return EquipmentUselessDBHelper.UpdateEquipmentUserlessInfo(EquipUselessModel);
        }
        #endregion
        #region 查询设备报废信息列表
        /// <summary>
        /// 查询设备报废信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetEquipmentUselessInfoBycondition(EquipmentUselessModel EquipmentUselessM, string EquipName, string EquipIndex,string FlowStatus, string CompanyID,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            try
            {
                return EquipmentUselessDBHelper.GetEquipmentUselessInfoBycondition(EquipmentUselessM, EquipName, EquipIndex, FlowStatus, CompanyID, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region  根据报废单据号获取设备报废信息以查看或修改
        /// <summary>
        /// 根据报废单据号获取设备报废信息以查看或修改
        /// </summary>
        /// <param name="RecordNo">设备报废单据号</param>
        /// <returns>DataTable</returns>
        public static DataTable GetEquipmentUselessInfoByRecordNo(string RecordNo, string CompanyID)
        {
            try
            {
                return EquipmentUselessDBHelper.GetEquipmentUselessInfoByRecordNo(RecordNo, CompanyID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 删除设备报废信息
        /// <summary>
        /// 删除设备报废信息
        /// </summary>
        /// <param name="EquipReceiveNos">设备单据号</param>
        /// <returns>删除是否成功 false:失败，true:成功</returns>
        public static bool DelEquipUselessInfo(string EquipUselessNos)
        {
            return EquipmentUselessDBHelper.DelEquipUselessInfo(EquipUselessNos);
        }
        #endregion
    }
}

/**********************************************
 * 类作用：   设备领用事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/03/12
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;

namespace XBase.Business.Office.AdminManager
{
    public class EquipmentReceiveBus
    {
        #region 添加设备领用信息
        /// <summary>
        /// 添加设备领用信息
        /// </summary>
        /// <param name="EquipReceiveModel">设备信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool AddEquipReceiveInfo(EquipmentReceiveModel EquipReceiveModel,out int RetValID,int CreateUserID,string CreateDate)
        {
            return EquipmentReceiveDBHelper.AddEquipmentReceiveInfo(EquipReceiveModel, out RetValID, CreateUserID, CreateDate);
        }
        #endregion
        #region 修改设备领用信息
        /// <summary>
        /// 修改设备领用信息
        /// </summary>
        /// <param name="EquipReceiveModel">设备信息</param>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateEquipMnetAndInfo(EquipmentReceiveModel EquipReceiveModel)
        {
            return EquipmentReceiveDBHelper.UpdateEquipMnetAndInfo(EquipReceiveModel);
        }
        #endregion
        #region 查询设备领用信息列表
        /// <summary>
        /// 查询设备信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetEquipmentReceiveInfoBycondition(EquipmentReceiveModel EquipmentReceiveM, string EquipName, string EquipIndex, string FlowStatus,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            try 
            {
                return EquipmentReceiveDBHelper.GetEquipmentReceiveInfoBycondition(EquipmentReceiveM, EquipName, EquipIndex, FlowStatus,pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 删除设备领用信息
        /// <summary>
        /// 删除设备领用信息
        /// </summary>
        /// <param name="EquipReceiveNos">设备单据号IDS</param>
        /// <returns>删除是否成功 false:失败，true:成功</returns>
        public static bool DelEquipReceiveInfo(string EquipReceiveNos)
        {
            return EquipmentReceiveDBHelper.DelEquipReceiveInfo(EquipReceiveNos);
        }
        #endregion
        #region  根据领用单据号获取设备领用信息
        /// <summary>
        /// 根据领用单据号获取设备领用信息
        /// </summary>
        /// <param name="RecordNo">设备单据号</param>
        /// <returns>DataTable</returns>
        public static DataTable GetEquipmentReceiveInfoByRecordNo(string RecordNo, string CompanyCD)
        {
            try
            {
                return EquipmentReceiveDBHelper.GetEquipmentReceiveInfoByRecordNo(RecordNo, CompanyCD);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 设备归还
        /// <summary>
        /// 设备归还
        /// </summary>
        /// <param name="EquipReceiveModel">归还信息</param>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateEquipMnetReturnInfo(EquipmentReceiveModel EquipReceiveModel)
        {
            return EquipmentReceiveDBHelper.UpdateEquipMnetReturnInfo(EquipReceiveModel);
        }
        #endregion
        #region 设备领用统计报表列表
        /// <summary>
        /// 设备领用统计报表列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetEquipmentUsedDetailInfo(string StartDate,string EndDate,string EquipType, string ReceiveDept,string CompanyID,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            try
            {
                return EquipmentReceiveDBHelper.GetEquipmentUsedDetailInfo(StartDate, EndDate, EquipType, ReceiveDept, CompanyID, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 设备领用统计报表打印
        /// <summary>
        /// 设备领用统计报表打印
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetEquipmentUsedDetailReportInfo(string StartDate,string EndDate,string EquipType,string ReceiveDept,string CompanyID,string ord)
        {
            try
            {
                return EquipmentReceiveDBHelper.GetEquipmentUsedDetailReportInfo(StartDate, EndDate, EquipType, ReceiveDept, CompanyID, ord);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

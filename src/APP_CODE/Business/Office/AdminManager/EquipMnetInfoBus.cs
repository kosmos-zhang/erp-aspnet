/**********************************************
 * 类作用：   设备事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/02/26
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;

namespace XBase.Business.Office.AdminManager
{
    /// <summary>
    /// 类名：EquipMnetInfoBus
    /// 描述：设备事务层处理
    /// 
    /// 作者：lysong
    /// 创建时间：2009/02/26
    /// 最后修改时间：2009/02/26
    /// </summary>
   
   public class EquipMnetInfoBus
   {
       #region 添加设备信息（无配件）
       /// <summary>
        /// 添加设备信息（无配件）
        /// </summary>
        /// <param name="EquipModel">设备信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddEquipInfo(EquipMnetInfoModel EquipModel)
        {
            return EquipMentInfoDBHelper.AddEquipMnetInfo(EquipModel);
        }
       #endregion
       #region 修改设备信息（无配件）
       /// <summary>
       /// 修改设备信息（无配件）
       /// </summary>
       /// <param name="EquipModel">设备信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateEquipInfo(EquipMnetInfoModel EquipModel)
       {
           return EquipMentInfoDBHelper.UpdateEquipMnetInfo(EquipModel);
       }
       #endregion
       #region 添加设备和配件
       /// <summary>
       /// 添加设备和配件
       /// </summary>
       /// <param name="EquipModel">设备信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddEquipAndFitInfo(EquipMnetInfoModel EquipModel,string FitInfo)
       {
           return EquipMentInfoDBHelper.AddEquipMnetAndFitInfo(EquipModel, FitInfo);
       }
       #endregion
       #region 获取设备分类
       /// <summary>
       /// 获取设备分类
       /// </summary>
       /// <param name="EquipModel">设备信息</param>
       /// <returns></returns>
       public static DataTable GetEquipType()
       {
           return EquipMentInfoDBHelper.GetEquipType();
       }
       #endregion
       #region 获取设备编号下拉列表
       /// <summary>
       /// 获取设备编号下拉列表
       /// </summary>
       /// <returns></returns>
       public static DataTable GetEquipNo()
       {
           return EquipMentInfoDBHelper.GetEquipNo();
       }
       #endregion

       #region 删除设备和配件
       /// <summary>
       /// 删除设备和配件
       /// </summary>
       /// <param name="equipmentids">设备IDS</param>
       /// <returns>删除是否成功 false:失败，true:成功</returns>
       public static bool DelEquipAndFitInfo(string equipmentids)
       {
           return EquipMentInfoDBHelper.DelEquipMnetAndFitInfo(equipmentids);
       }
       #endregion
       #region 更新设备和配件
       /// <summary>
       /// 更新设备和配件
       /// </summary>
       /// <param name="EquipModel">设备信息</param>
       /// <param name="FitInfo">配件信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateEquipAndFitInfo(EquipMnetInfoModel EquipModel, string FitInfo)
       {
           return EquipMentInfoDBHelper.UpdateEquipMnetAndFitInfo(EquipModel, FitInfo);
       }
       #endregion
       #region 获取设备信息byindex
       /// <summary>
       /// 获取设备信息byindex
       /// </summary>
       /// <param name="EquipCode">设备序列号</param>
       /// <returns>用户信息</returns>
       public static int GetEquipInfoByIndex(string EquipCode, string TableName, string ColName,string CompanyCD)
       {
           return EquipMentInfoDBHelper.GetEquipInfoByIndex(EquipCode,TableName,ColName,CompanyCD);
       }
       #endregion
       #region 获取设备信息列表
       /// <summary>
       /// 获取设备信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentInfo()
       {
           try
           {
               return EquipMentInfoDBHelper.GetEquipmentTable();
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion
       #region 查询设备明细信息列表
       /// <summary>
       /// 查询设备信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentInfoBycondition(EquipMnetInfoModel equip_M,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           try
           {
               return EquipMentInfoDBHelper.GetEquipmentTableBycondition(equip_M, pageIndex, pageCount, ord, ref TotalCount);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 查询设备明细信息列表(金额合计)
       /// <summary>
       /// 查询设备信息列表(金额合计)
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentTotalTableBycondition(EquipMnetInfoModel equip_M)
       {
           try
           {
               return EquipMentInfoDBHelper.GetEquipmentTotalTableBycondition(equip_M);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 根据flag获取不同申请下需要显示的设备
       /// <summary>
       /// 根据flag获取不同申请下需要显示的设备
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentInfoBycondition(EquipMnetInfoModel equip_M, int pageIndex, int pageCount, string ord, ref int TotalCount,string flag)
       {
           try
           {
               return EquipMentInfoDBHelper.GetEquipmentTableBycondition(equip_M, pageIndex, pageCount, ord, ref TotalCount, flag);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion
       #region 查询设备汇总信息列表
       /// <summary>
       /// 查询设备汇总信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentTotalInfoBycondition(EquipMnetInfoModel equip_M,string EquipPre,int Eplength,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           try
           {
               return EquipMentInfoDBHelper.GetEquipmentTotalTableBycondition(equip_M, EquipPre, Eplength, pageIndex, pageCount, ord, ref TotalCount);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 查询设备汇总信息列表(金额合计)
       /// <summary>
       /// 查询设备汇总信息列表(金额合计)
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentTotalSumTableBycondition(EquipMnetInfoModel equip_M)
       {
           try
           {
               return EquipMentInfoDBHelper.GetEquipmentTotalSumTableBycondition(equip_M);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region  根据设备编号查询设备信息
       /// <summary>
       /// 根据设备编号查询设备信息
       /// </summary>
       /// <param name="EquipmnetNO">设备编号</param>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentInfoByEquipmentNo(string EquipmnetNO, string CompanyID)
       {
           try
           {
               return EquipMentInfoDBHelper.GetEquipmentInfoByEquipmentNo(EquipmnetNO, CompanyID);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion
       #region 根据状态和设备编号判断此设备是否空闲
       /// <summary>
       /// 根据状态和设备编号判断此设备是否空闲
       /// </summary>
       /// <param name="EquipNo">设备编号</param>
       /// <param name="IsFree">空闲状态0</param>
       /// <returns></returns>
       public static int EquipIsFree(string EquipNo, string IsFree, string CompanyID)
       {
           return EquipMentInfoDBHelper.EquipIsFree(EquipNo, IsFree, CompanyID);
       }
       #endregion

       #region 查询设备明细信息报表列表
       /// <summary>
       /// 查询设备明细信息报表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentReportInfo(EquipMnetInfoModel equip_M,string EndDate,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           try
           {
               return EquipMentInfoDBHelper.GetEquipmentReportInfo(equip_M, EndDate, pageIndex, pageCount, ord, ref TotalCount);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 查询设备明细信息报表
       /// <summary>
       /// 查询设备明细信息报表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentDetailInfoAll(EquipMnetInfoModel equip_M, string ord, string BuyEndDate)
       {
           try
           {
               return EquipMentInfoDBHelper.GetEquipmentDetailInfoAll(equip_M, ord, BuyEndDate);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 新报表

       public static DataTable EquipmentByDetail(string CompanyCD, string EquipmentName, string Type, string DeptID, string Status, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
     {
        return EquipMentInfoDBHelper.EquipmentByDetail(CompanyCD,EquipmentName,Type,DeptID,Status,BeginDate,EndDate,pageIndex,pageCount,ord,ref TotalCount);
     }

       public static DataTable EquipmentUsedByDetail(string CompanyCD,string StartDate, string EndDate, string Type, string DeptID,string DateType,string DateValue, int pageIndex, int pageCount, string ord, ref int TotalCount)
       {
           return EquipMentInfoDBHelper.EquipmentUsedByDetail(StartDate, EndDate, Type, DeptID, CompanyCD,DateType,DateValue,pageIndex, pageCount, ord, ref TotalCount);
       }

       /// <summary>
       /// 设备状况分布
       /// </summary>
       public static DataTable EquipmentByStatus(string CompanyCD, string EquipmentName, string Type, string DeptID, string BeginDate, string EndDate)
       {
           return EquipMentInfoDBHelper.EquipmentByStatus(CompanyCD, EquipmentName, Type, DeptID, BeginDate, EndDate);
       }

       /// <summary>
       /// 设备部门分布
       /// </summary>
       public static DataTable EquipmentByDept(string CompanyCD, string EquipmentName, string Type, string Status, string BeginDate, string EndDate)
       {
           return EquipMentInfoDBHelper.EquipmentByDept(CompanyCD, EquipmentName, Type, Status, BeginDate, EndDate);
       }

       /// <summary>
       /// 部门领用分析
       /// </summary>
       public static DataTable EquipmentUsedByDept(string CompanyCD,string Type, string BeginDate, string EndDate)
       {
           return EquipMentInfoDBHelper.EquipmentUsedByDept(CompanyCD, Type, BeginDate, EndDate);
       }


       /// <summary>
       /// 部门领用走势
       /// </summary>
       public static DataTable EquipmentUsedByTrend(string CompanyCD, string DeptID, string Type, string DateType, string BeginDate, string EndDate)
       {
           return EquipMentInfoDBHelper.EquipmentUsedByTrend(CompanyCD, DeptID, Type, DateType, BeginDate, EndDate);
       } 
       #endregion

   }
}

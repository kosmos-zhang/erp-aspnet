/**********************************************
 * 类作用：   设备配件事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/03/06
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;

namespace XBase.Business.Office.AdminManager
{
   public class EquipmentFitBus
    {
        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="EquipModel">设备信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddEquipInfo(EquipmentFitModel EquipFitModel)
        {
            return EquipmentFitDBHelper.AddEquipmentFitInfo(EquipFitModel);
        }
    }
}

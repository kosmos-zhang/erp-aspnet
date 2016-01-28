/**********************************************
 * 类作用：   仓库信息事务层处理
 * 建立人：   肖合明
 * 建立时间： 2009/03/29
 ***********************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.StorageManager;
using XBase.Common;
using System.Data;
using XBase.Business.Common;

namespace XBase.Business.Office.StorageManager
{
    public class StorageSellBackBus
    {
        #region 销售退货单及其明细信息列表(弹出层显示)
        /// <summary>
        /// 销售退货单及其明细信息列表(弹出层显示)
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSBDetailInfo(string CompanyCD, string BackNo, string Title)
        {
            return StorageSellBackDBHelper.GetSBDetailInfo(CompanyCD,BackNo,Title);
        }
        #endregion

        #region 根据销售退货单明细中ID数组来获取信息（填充入库单中的明细）
        /// <summary>
        /// 根据销售退货单明细中ID数组来获取信息（填充入库单中的明细
        /// </summary>
        /// <param name="strDetailIDList"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetInfoByDetalIDList(string strDetailIDList, string CompanyCD)
        {
            return StorageSellBackDBHelper.GetInfoByDetalIDList(strDetailIDList, CompanyCD);
        }
        #endregion

    }
}

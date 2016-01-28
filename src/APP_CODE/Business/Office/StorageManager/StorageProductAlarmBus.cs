/**********************************************
 * 类作用：   采购入库和采购入库明细事务层处理
 * 建立人：   肖合明
 * 建立时间： 2009/06/04
 ***********************************************/

using System;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Business.Common;
using System.Collections.Generic;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Common;

namespace XBase.Business.Office.StorageManager
{
    public class StorageProductAlarmBus
    {
        #region 查询：库存限量报警
        /// <summary>
        /// 库存限量报警
        /// </summary>
        /// <param name="AlarmType">0-全部，1-上限报警，2-下限报警</param>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageProductAlarm(string AlarmType, StockAccountModel model, string BarCode, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return StorageProductAlarmDBHelper.GetStorageProductAlarm(AlarmType, model, BarCode, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DataTable GetStorageProductAlarm(string AlarmType, StockAccountModel model, string orderby, string BarCode)
        {
            try
            {
                return StorageProductAlarmDBHelper.GetStorageProductAlarm(AlarmType, model, orderby, BarCode);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}

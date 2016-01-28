/**********************************************
 * 类作用：   仓库信息事务层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/12
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
    public class CodeReasonTypeBus
    {
        #region 获取出入库原因信息
        /// <summary>
        /// 获取出入库原因信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetReasonType(string CompanyCD)
        {
            return CodeReasonTypeDBHelper.GetReasonType(CompanyCD);
        }
        #endregion

        #region 获取出入库原因信息
        /// <summary>
        /// 根据flag获取原因信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="flag">类型原因类别（1出入库原因，2借货原因，3库存调整原因，4库存调拨原因，5库存报损原因，20销售退货原因，21采购退货原因）</param>
        /// <returns></returns>
        public static DataTable GetReasonTypeByFlag(string CompanyCD, string flag)
        {
            return CodeReasonTypeDBHelper.GetReasonTypeByFlag(CompanyCD, flag);
        }
        #endregion
    }
}

/**************************
 * 描述：销售发货明细
 * 创建人：hexw
 * 创建时间：2010-6-21
 * *************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using XBase.Model.Office.SellManager;

namespace XBase.Business.Office.SellManager
{
    public class SellSendDetailsListBus
    {
        #region 根据条件获取 销售发货明细列表
        /// <summary>
        /// 根据条件获取 销售发货明细列表
        /// </summary>
        /// <param name="model">检索条件实体</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetSellSendDetailListData(SellSendDetailsListModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return XBase.Data.Office.SellManager.SellSendDetailsListDBHelper.GetSellSendDetailListData(model, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion
    }
}

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.SellManager;
using XBase.Data.Office.SellManager;
using XBase.Common;

namespace XBase.Business.Office.SellManager
{
    public  class SellModuleSelectCurrencyBus
    {
        /// <summary>
        /// 获取币种
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCurrencyTypeSetting(int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellModuleSelectCurrencyDBHelper.GetCurrencyTypeSetting(pageIndex, pageCount, ord, ref TotalCount);
        }

          /// <summary>
        /// 获取币种
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCurrencyTypeSetting()
        {
            return SellModuleSelectCurrencyDBHelper.GetCurrencyTypeSetting();
        }
    }
}

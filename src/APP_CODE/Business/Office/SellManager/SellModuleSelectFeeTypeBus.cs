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
   public  class SellModuleSelectFeeTypeBus
    {
        
        /// <summary>
        /// 获取费用类别
        /// </summary>
        /// <returns></returns>
       public static DataTable GetFeeType(int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellModuleSelectFeeTypeDBHelper.GetFeeType(pageIndex, pageCount, ord, ref TotalCount);
        }
    }
}

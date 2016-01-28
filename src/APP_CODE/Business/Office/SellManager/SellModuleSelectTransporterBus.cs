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
    public  class SellModuleSelectTransporterBus
    {
        /// <summary>
        /// 获取运输商详细信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTransporter(string OrderNo, string Title, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellModuleSelectTransporterDBHelpercs.GetTransporter(OrderNo, Title, pageIndex, pageCount, ord, ref TotalCount);
        }
    }
}

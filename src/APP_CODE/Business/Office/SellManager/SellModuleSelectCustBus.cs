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
   public  class SellModuleSelectCustBus
    {
         /// <summary>
        /// 选择执行状态的单据
        /// </summary>
        /// <returns></returns>
       public static DataTable GetCustList(string OrderNo, string Title, string model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellModuleSelectCustDBHelper.GetCustList(OrderNo, Title, model, pageIndex, pageCount, ord, ref TotalCount);
        }
        /// <summary>
        /// 获取客户详细信息
        /// </summary>
        /// <param name="strID">客户编号</param>
        /// <returns></returns>
       public static DataTable GetCustInfo(string strID)
       {
           return SellModuleSelectCustDBHelper.GetCustInfo(strID);
       }
    }
}

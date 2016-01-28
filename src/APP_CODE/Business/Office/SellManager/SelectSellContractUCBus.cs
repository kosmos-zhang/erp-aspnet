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
    public  class SelectSellContractUCBus
    {
        /// <summary>
        /// 选择执行状态的单据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSellContractList(string OrderNo, string Title, string CustName, int? CurrencyType, string model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SelectSellContractUCDBHelper.GetSellContractList(OrderNo, Title, CustName, CurrencyType, model, pageIndex, pageCount, ord, ref TotalCount);
        }

         /// <summary>
        /// 获取合同详细信息
        /// </summary>
        /// <param name="strOffNo"></param>
        public static DataTable GetSellContract(string strContractNo)
        {
            return SelectSellContractUCDBHelper.GetSellContract(strContractNo) ;
        }

         /// <summary>
        /// 获取合同明细信息
        /// </summary>
        /// <param name="strOffNo"></param>
        /// <returns></returns>
        public static DataTable GetSellContractInfo(string strContractNo)
        {
            return SelectSellContractUCDBHelper.GetSellContractInfo(strContractNo);
        }
    }
}

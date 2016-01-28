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
   public  class SelectSellOfferUCBus
    {
        /// <summary>
        /// 选择执行状态的单据
        /// </summary>
        /// <returns></returns>
       public static DataTable GetSellOfferList(string OrderNo, string Title, string CustName, int? CurrencyType, string model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SelectSellOfferUCDBHelper.GetSellOfferList(OrderNo, Title, CustName, CurrencyType, model, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 获取报价单详细信息及明细
        /// </summary>
        /// <param name="strOffNo"></param>
        public static  DataTable  GetSellOffer( string strOffNo)
        {
            return  SelectSellOfferUCDBHelper.GetSellOffer( strOffNo);
        }

       /// <summary>
        /// 获取报价单明细信息
        /// </summary>
        /// <param name="strOffNo"></param>
        /// <returns></returns>
        public static DataTable GetSellOfferInfo(string strOffNo)
        {
            return SelectSellOfferUCDBHelper.GetSellOfferInfo(strOffNo);
        }
    }
}

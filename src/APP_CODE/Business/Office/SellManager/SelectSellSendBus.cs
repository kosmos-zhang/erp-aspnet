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
    public  class SelectSellSendBus
    {
        /// <summary>
        /// 选择执行状态的单据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSellSendList(string busType, string OrderNo, string Title, string CustName,int? CurrencyType, string model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SelectSellSendDBHelper.GetSellSendList(busType, OrderNo, Title, CustName, CurrencyType, model, pageIndex, pageCount, ord, ref TotalCount);
        }

        
        /// <summary>
        /// 获取发货单详细信息
        /// </summary>
        /// <param name="strOffNo"></param>
        public static DataTable GetSellSendInfo(string strOrderID)
        {
            return SelectSellSendDBHelper.GetSellSendInfo(strOrderID);
        }

         /// <summary>
        /// 获取发货单明细信息
        /// </summary>
        /// <param name="strOffNo"></param>
        public static DataTable GetSellSendDetail(string strOrderID, string strOrderDetailID, string CustID, string busType)
        {
            return SelectSellSendDBHelper.GetSellSendDetail(strOrderID, strOrderDetailID, CustID, busType);
        }

        /// <summary>
        /// 获取发货单明细信息
        /// </summary>
        /// <param name="strOffNo"></param>
        public static DataTable GetSellSendDetail(string strOrderID, string strOrderDetailID, string CustID, string busType, string CurrencyType, string Rate, string OrderID,
            string OrderNo, string Title, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SelectSellSendDBHelper.GetSellSendDetail(strOrderID, strOrderDetailID, CustID, busType, CurrencyType,Rate, OrderID, OrderNo,
            Title, pageIndex, pageCount, ord, ref TotalCount);
        }
    }
}

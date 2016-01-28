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
    public class SelectSellOrderDetailBus
    {
         /// <summary>
        /// 选择执行状态的单据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSellOrderList(string OrderNo, string Title, string CustName, int? CurrencyType, string model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SelectSellOrderDetailDBHelper.GetSellOrderList(OrderNo, Title, CustName, CurrencyType, model, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 获取订单明细列表
        /// </summary>
        /// <param name="iCustID">客户编号</param>
        /// <param name="strDetailID2">订单明细ID列表</param>
        /// <param name="strOrderID">订单编号</param>
        /// <param name="CurrencyType">币种</param>
        /// <param name="OrderID">已选择的订单的id</param>
        /// <param name="OrderNo">查询条件订单编号</param>
        /// <param name="Title">查询条件订单主题</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSellOrderDetailByCustID(int? iCustID, string strDetailID2,string strOrderID,string CurrencyType,string Rate,string OrderID,
            string OrderNo, string Title, int pageIndex, int pageCount, string ord, ref int TotalCount)
        
        {
            return SelectSellOrderDetailDBHelper.GetSellOrderDetailByCustID(iCustID, strDetailID2, strOrderID, CurrencyType,Rate, OrderID, OrderNo,
            Title, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 获取订单明细列表，三个参数可以全部为空，全部为空时默认取出该单位所有的订单明细
        /// </summary>
        /// <param name="iCustID">客户编号,可以为空，为空时不根据客户取数据</param>
        /// <param name="strDetailID2">订单明细ID列表可以为空，为空时不根据明细ID取数据</param>
        /// <param name="strOrderID">订单编号可以为空，为空时不根据订单编号去数据</param>
        /// <returns></returns>
        public static DataTable GetSellOrderDetailByCustID(int? iCustID, string strDetailID2, string strOrderID)
        {
            return SelectSellOrderDetailDBHelper.GetSellOrderDetailByCustID(iCustID, strDetailID2, strOrderID);

        }
         /// <summary>
        /// 获取订单详细信息
        /// </summary>
        /// <param name="strOffNo"></param>
        public static DataTable GetSellOrderInfo(string strOrderID)
        {
            return SelectSellOrderDetailDBHelper.GetSellOrderInfo(strOrderID);
        }
    }
}

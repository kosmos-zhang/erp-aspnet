/**********************************************
 * 描述：     库存流水账业务处理
 * 建立人：   莫申林
 * 建立时间： 2010/02/09
 ***********************************************/
using System;
using XBase.Data.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using System.Data;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
namespace XBase.Business.Office.StorageManager
{
    public class StrongeJournalBus
    {
        /// <summary>
        /// 库存流水账--汇总信息
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSumStrongJournal(string queryStr,string extQueryStr,int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return StrongeJournalDBHelper.GetSumStrongJournal(queryStr,extQueryStr, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 库存流水账--明细信息
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetDetailStrongJournal(string queryStr, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return StrongeJournalDBHelper.GetDetailStrongJournal(queryStr, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据供应商查询入库总量
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="extQueryStr">物品扩展属性查询条件</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetStrongJournalByPro(string queryStr, string extQueryStr, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return StrongeJournalDBHelper.GetStrongJournalByPro(queryStr, extQueryStr, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据供应商查询库存流水账
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetDetailStrongJournalByPro(string queryStr, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return StrongeJournalDBHelper.GetDetailStrongJournalByPro(queryStr, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         /// <summary>
        /// 分店库存流水账--明细信息
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSubStrongJournalDetail(string queryStr, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return StrongeJournalDBHelper.GetSubStrongJournalDetail(queryStr, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

          /// <summary>
        /// 库存流水账--明细统计信息
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static string GetSumJournal(string queryStr)
        {
            try
            {
                return StrongeJournalDBHelper.GetSumJournal(queryStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

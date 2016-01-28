/********************
**销售汇报
*创建人：hexw
*创建时间：2010-7-6
********************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using XBase.Data.Office.SellReport;
using XBase.Model.Office.SellReport;

namespace XBase.Business.Office.SellReport
{
    public class SellProductReportBus
    {

        #region 增、删、改
        #region 添加
        public static bool Insert(SellReportModel sellrptModel, List<SellReportDetailModel> sellRptDetailModellList, out string strMsg)
        {
            return SellProductReportDBHepler.Insert(sellrptModel, sellRptDetailModellList, out strMsg);
        }
        #endregion

        #region 删除销售汇报
        /// <summary>
        /// 删除销售汇报
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DelSellRpt(string ids, string strCompanyCD, out string strMsg, out string strFieldText)
        {
            return SellProductReportDBHepler.DelSellRpt(ids, strCompanyCD, out strMsg, out strFieldText);
        }
        #endregion

        #region 更新销售汇报
        /// <summary>
        /// 更新销售发汇报
        /// </summary>
        /// <returns></returns>
        public static bool UpdateSellReport(SellReportModel sellrptModel, List<SellReportDetailModel> sellRptDetailModellList, out string strMsg)
        {
            return SellProductReportDBHepler.UpdateSellReport(sellrptModel, sellRptDetailModellList, out strMsg);
        }
        #endregion
        
        #endregion

        #region 获取相关信息
        #region 获取产品列表
        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="strCompanyCD">公司编号</param>
        /// <returns>ID和Name组成的Datatable</returns>
        public static DataTable GetProductList(string strCompanyCD)
        {
            return SellProductReportDBHepler.GetProductList(strCompanyCD);
        }
        #endregion        

        #region 根据ID获取单据产品汇报详细信息
        /// <summary>
        /// 根据ID获取单据产品汇报详细信息
        /// </summary>
        /// <param name="id">单据ID</param>
        /// <returns></returns>
        public static DataTable GetSellReportMain(int id, string strCompanyCD)
        {
            return SellProductReportDBHepler.GetSellReportMain(id, strCompanyCD);
        }
        #endregion

        #region 根据ID获取单据产品汇报详细信息(精度控制)
        /// <summary>
        /// 根据ID获取单据产品汇报详细信息(精度控制)
        /// </summary>
        /// <param name="id">单据ID</param>
        /// <param name="sellPointLen">精度</param>
        /// <returns></returns>
        public static DataTable GetSellReportMain(int id,string strCompanyCD, string sellPointLen)
        {
            return SellProductReportDBHepler.GetSellReportMain(id, strCompanyCD, sellPointLen);
        }
        #endregion

        #region 获取明细
        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="id">汇报ID</param>
        /// <returns></returns>
        public static DataTable GetSellReportDetail(int id)
        {
            return SellProductReportDBHepler.GetSellReportDetail(id);
        }
        #endregion

        #region 获取销售汇报列表
        /// <summary>
        /// 获取销售汇报列表
        /// </summary>
        /// <param name="sellrptModel">sellrptModel实体</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSellRptList(SellReportModel sellrptModel, DateTime? CreateDate1, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellProductReportDBHepler.GetSellRptList(sellrptModel,CreateDate1, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion
        #endregion

        #region 获取产品信息
        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns></returns>
        public static DataTable GetProductInfoByID(int productID)
        {
            return SellProductReportDBHepler.GetProductInfoByID(productID);
        }
        #endregion
    }
}

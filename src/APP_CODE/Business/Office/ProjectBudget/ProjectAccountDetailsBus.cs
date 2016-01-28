/********************************
 * 描述：核算明细
 * 创建人：hexw
 * 创建时间：2010-5-18
 * *****************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Office.ProjectBudget;
namespace XBase.Business.Office.ProjectBudget
{
    public class ProjectAccountDetailsBus
    {
        #region  获取“设备与原材料采购”明细列表
        /// <summary>
        /// 获取“设备与原材料采购”明细列表
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetPurchaseDetailInfoList(string purchaseType,bool isMoreUnit,string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            return ProjectAccountDetailsDBHelper.GetPurchaseDetailInfoList(purchaseType,isMoreUnit, strCompanyCD, ProjectID, BeginDate, EndDate);
        }
        #endregion

        #region  获取“原材料消耗”明细列表
        /// <summary>
        /// 获取“原材料消耗”明细列表
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetTakeMaterialDetailInfoList(bool isMoreUnit, string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            return ProjectAccountDetailsDBHelper.GetTakeMaterialDetailInfoList(isMoreUnit,strCompanyCD, ProjectID, BeginDate, EndDate);
        }
        #endregion

        #region  获取“其它入库”明细列表
        /// <summary>
        /// 获取“其它入库”明细列表
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOtherInDetailInfoList(bool isMoreUnit, string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            return ProjectAccountDetailsDBHelper.GetOtherInDetailInfoList(isMoreUnit,strCompanyCD, ProjectID, BeginDate, EndDate);
        }
        #endregion

        #region  获取“其它出库”明细列表
        /// <summary>
        /// 获取“其它出库”明细列表
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOtherOutDetailInfoList(bool isMoreUnit, string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            return ProjectAccountDetailsDBHelper.GetOtherOutDetailInfoList(isMoreUnit,strCompanyCD, ProjectID, BeginDate, EndDate);
        }
        #endregion

        #region  获取“销售收入”明细列表
        /// <summary>
        /// 获取“销售收入”明细列表
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetSellDetailInfoList(bool isMoreUnit, string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            return ProjectAccountDetailsDBHelper.GetSellDetailInfoList(isMoreUnit,strCompanyCD, ProjectID, BeginDate, EndDate);
        }
        #endregion

        #region 获取“费用支出”
        /// <summary>
        /// 获取“费用支出”
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetFeeDetailInfoList(string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            return ProjectAccountDetailsDBHelper.GetFeeDetailInfoList(strCompanyCD, ProjectID, BeginDate, EndDate);
        }
        #endregion

        #region  获取“收款单”明细列表
        /// <summary>
        /// 获取“收款单”明细列表
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetInComeBillDetailInfoList(string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            return ProjectAccountDetailsDBHelper.GetInComeBillDetailInfoList(strCompanyCD, ProjectID, BeginDate, EndDate);
        }
        #endregion

        #region  获取“付款单”明细列表
        /// <summary>
        /// 获取“付款单”明细列表
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetPayBillDetailInfoList(string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            return ProjectAccountDetailsDBHelper.GetPayBillDetailInfoList(strCompanyCD, ProjectID, BeginDate, EndDate);
        }
        #endregion
    }
}

/**********************************************
 * 描述：     资产负债表计算公式明细业务处理
 * 建立人：   莫申林
 * 建立时间： 2009/05/22
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;
using System.Collections;

namespace XBase.Business.Office.FinanceManager
{
    public class FormulaDetailsBus
    {
        /// <summary>
        /// 添加资产负债表公式明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="IntID"></param>
        /// <returns></returns>
        public static bool InsertFormulaDetails(FormulaDetailsModel model)
        {
            try
            {
                return FormulaDetailsDBHelper.InsertFormulaDetails(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  修改资产负债表计算公式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateFormulaDetails(FormulaDetailsModel model)
        {
            try
            {
                return FormulaDetailsDBHelper.UpdateFormulaDetails(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据查询条件查询资产负债表计算公式信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="KeyID">主键</param>
        /// <param name="companyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetBalanceFormulaDetails(string KeyID, string companyCD, string FormulaID)
        {
            try
            {
                return FormulaDetailsDBHelper.GetBalanceFormulaDetails(KeyID, companyCD,FormulaID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除资产负债表计算公式信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteFormulaDetailsInfo(string ids)
        {
            try
            {
                return FormulaDetailsDBHelper.DeleteFormulaDetailsInfo(ids);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

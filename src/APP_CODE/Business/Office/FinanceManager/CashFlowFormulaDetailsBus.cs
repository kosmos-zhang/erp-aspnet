/**********************************************
 * 描述：     现金流量表计算公式业务处理
 * 建立人：   莫申林
 * 建立时间： 2009/05/26
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;
using System.Collections;

namespace XBase.Business.Office.FinanceManager
{
    public class CashFlowFormulaDetailsBus
    { 
        /// <summary>
        /// 添加现金流量表公式明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="IntID"></param>
        /// <returns></returns>
        public static bool InsertCashFlowFormulaDetails(CashFlowFormulaDetailsModel model)
        {
            try
            {
                return CashFlowFormulaDetailsDBHelper.InsertCashFlowFormulaDetails(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  修改现金流量表计算公式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateCashFlowFormulaDetails(CashFlowFormulaDetailsModel model)
        {
            try
            {
                return CashFlowFormulaDetailsDBHelper.UpdateCashFlowFormulaDetails(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据查询条件查询现金流量表计算公式信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="KeyID">主键</param>
        /// <param name="companyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetCashFlowFormulaDetails(string KeyID, string FormulaID)
        {
            try
            {
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                return CashFlowFormulaDetailsDBHelper.GetCashFlowFormulaDetails(KeyID,companyCD,FormulaID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除现金流量表计算公式信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteCashFlowFormulaDetailsInfo(string ids)
        {
            try
            {
                return CashFlowFormulaDetailsDBHelper.DeleteCashFlowFormulaDetailsInfo(ids);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

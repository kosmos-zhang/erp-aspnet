/**********************************************
 * 描述：     现金流量表项目业务处理
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
   public class CashFlowFormulaBus
    {
       /// <summary>
        /// 根据ID获取计算公式信息
        /// </summary>
        /// <param name="KeyID"></param> 
        /// <param name="companyCD"></param>
        /// <returns></returns>
       public static DataTable GetCashFlowFormulaInfo(string KeyID)
       {
           try
           {
               return CashFlowFormulaDBHelper.GetCashFlowFormulaInfo(KeyID);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}

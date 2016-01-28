/**********************************************
 * 类作用：   勾兑操作步骤明细表业务层处理
 * 建立人：   莫申林
 * 建立时间： 2009/07/02
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;
using System.Collections;


namespace XBase.Business.Office.FinanceManager
{
    public class StepsDetailsBus
    {
        public static bool InSertOrUpdateStepsDetails(ArrayList MyList)
        {
            try
            {
                string ids;
                bool rev = false;
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                if (MyList.Count > 0)
                {
                    if (StepsDetailsDBHelper.IsExits((MyList[0] as StepsDetailsModel).PayOrInComeType, (MyList[0] as StepsDetailsModel).SourceID.ToString(), CompanyCD,out ids))
                    {
                        rev = StepsDetailsDBHelper.UpdateStepsDetails(MyList);
                    }
                    else
                    {
                        rev = StepsDetailsDBHelper.InSertStepsDetails(MyList);
                    }

                }
                return rev;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region 是否存在记录
        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="payOrIncometype">收付款类别</param>
        /// <param name="SourceID">收付款单ID</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static bool IsExits(string payOrIncometype, string SourceID, string CompanyCD, out string IDS)
        {
            try
            {
                return StepsDetailsDBHelper.IsExits(payOrIncometype, SourceID, CompanyCD, out IDS);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 添加勾兑明细操作记录
        /// <summary>
        /// 添加勾兑明细操作记录
        /// </summary>
        /// <param name="MyList">ArrayList</param>
        /// <returns></returns>
        public static bool InSertStepsDetails(ArrayList MyList)
        {
            try
            {
                return StepsDetailsDBHelper.InSertStepsDetails(MyList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 修改勾兑明细操作记录
        /// <summary>
        /// 修改勾兑明细操作记录
        /// </summary>
        /// <param name="MyList">ArrayList</param>
        /// <returns></returns>
        public static bool UpdateStepsDetails(ArrayList MyList)
        {
            try
            {
                return StepsDetailsDBHelper.UpdateStepsDetails(MyList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region
        /// <summary>
        /// 获取勾兑操作明细记录
        /// </summary>
        /// <param name="SourceID">收付款单ID</param>
        /// <param name="BlendingID">勾兑明细ID</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="PayOrIncomeType">收付款类别</param>
        /// <returns></returns>
        public static DataTable GetStepsDetails(int SourceID, int BlendingID, string PayOrIncomeType)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return StepsDetailsDBHelper.GetStepsDetails(SourceID, BlendingID, CompanyCD, PayOrIncomeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除勾兑操作明细记录
        /// <summary>
        /// 删除勾兑操作明细记录
        /// </summary>
        /// <param name="SourceID">收付款单ID</param>
        /// <param name="PayOrIncomeType">收付款单类别</param>
        /// <returns></returns>
        public static bool DeleteStepsDetails(int SourceID, string PayOrIncomeType)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return StepsDetailsDBHelper.DeleteStepsDetails(SourceID, PayOrIncomeType, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

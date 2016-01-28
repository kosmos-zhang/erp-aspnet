using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Office.ProjectBudget;
using XBase.Model.Office.ProjectBudget;

namespace XBase.Business.Office.ProjectBudget
{
    public  class SubBudgetBus
    {
        #region 添加修改“分项预算概要”
        public static int AddSubBudgetInfo(SubBudgetModel subBudgetModel, XBase.Common.UserInfoUtil userinfo)
        {
            return SubBudgetDBHelper.AddSubBudgetInfo(subBudgetModel, userinfo);
        }

        public static int EditSubBudget(SubBudgetModel subBudgetModel, XBase.Common.UserInfoUtil userinfo)
        {
            return SubBudgetDBHelper.EditSubBudget(subBudgetModel, userinfo);
        }
        #endregion

        #region 获取分项预算概要列表
        /// <summary>
        /// 获取分项预算概要列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagecount"></param>
        /// <param name="projectid"></param>
        /// <param name="summaryname"></param>
        /// <param name="OrderBy"></param>
        /// <param name="userinfo"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetSubBudgetList(int pageindex, int pagecount, string projectid, string summaryname, string OrderBy, XBase.Common.UserInfoUtil userinfo, ref int totalCount)
        {
            return SubBudgetDBHelper.GetSubBudgetList(pageindex, pagecount, projectid, summaryname, OrderBy, userinfo, ref totalCount);
        }
        #endregion
        #region 删除分项预算概要
        /// <summary>
        /// 删除分项预算概要
        /// </summary>
        /// <param name="billID">删除的ID串</param>
        /// <returns></returns>
        public static int DeLSubBudgetInfo(string billID)
        {
            return SubBudgetDBHelper.DeLSubBudgetInfo(billID);
        }
        #endregion

        #region 获取分项预算概要详细信息
        /// <summary>
        /// 获取分项预算概要详细信息
        /// </summary>
        /// <param name="billID">单据ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetSubBudgetInfo(string billID, string strCompanyCD)
        {
            return SubBudgetDBHelper.GetSubBudgetInfo(billID, strCompanyCD);
        }
        #endregion
    }
}

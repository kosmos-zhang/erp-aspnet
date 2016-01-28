/***********************************
 * 描述：销售状况分析
 * 创建人：何小武
 * 创建时间：2010-6-1
 * ********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.OperatingModel.DecisionData;

namespace XBase.Business.OperatingModel.DecisionData
{
    public class SalesFiguresAnalysisBus
    {
        #region 获取销售状况分析结果列表
        /// <summary>
        /// 获取销售状况分析结果列表
        /// </summary>
        /// <param name="model">检索条件实体</param>
        /// <param name="userinfo">session用户信息</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetSalesFiguresList(XBase.Model.Office.OperatingModel.SalesFiguresAnalysisModel model, XBase.Common.UserInfoUtil userinfo, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            return SalesFiguresAnalysisDBHelper.GetSalesFiguresList(model, userinfo, pageIndex, pageCount, OrderBy, ref  totalCount);
        }
        #endregion
    }
}

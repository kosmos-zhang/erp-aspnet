/**********************************************
 * 类作用：   仓库信息事务层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/17
 ***********************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.StorageManager;
using XBase.Common;
using System.Data;
using XBase.Business.Common;
namespace XBase.Business.Office.StorageManager
{
    public class InTypeBus
    {
        /// <summary>
        /// 查出对应入库类型的基本信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="InType"></param>
        /// <returns></returns>
        public static DataTable GetInTypeInfo(string CompanyCD, string InType, string InNo, string Title)
        {
            return InTypeDBHelper.GetInTypeInfo(CompanyCD, InType, InNo, Title);
        }
        /// <summary>
        /// 根据的入库编号查询出相关信息及其明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="InType"></param>
        /// <param name="InNo"></param>
        /// <returns></returns>
        public static DataTable GetDetailInfo(string CompanyCD, string InType, string InNo)
        {
            return InTypeDBHelper.GetDetailInfo(CompanyCD, InType, InNo);
        }
    }
}

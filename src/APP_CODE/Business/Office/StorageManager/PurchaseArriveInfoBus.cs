/**********************************************
 * 类作用：   仓库信息事务层处理
 * 建立人：   肖合明
 * 建立时间： 2009/03/29
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
    public class PurchaseArriveInfoBus
    {
        public static DataTable GetPurchaseArriveTableBycondition()
        {
            return PurchaseArriveInfoDBHelper.GetPurchaseArriveTableBycondition();
        }

        public static DataTable GetPurchaseArriveInfo(string CompanyCD, string ArriveNo, string Title)
        {
            return PurchaseArriveInfoDBHelper.GetPurchaseArriveInfo(CompanyCD,ArriveNo,Title);
        }

        public static DataTable GetPAInfo(string ArriveNo, string CompanyCD)
        {
            return PurchaseArriveInfoDBHelper.GetPAInfo(ArriveNo, CompanyCD);
        }
        public static DataTable GetPADetailInfo(string ArriveNo, string CompanyCD)
        {
            return PurchaseArriveInfoDBHelper.GetPADetailInfo(ArriveNo, CompanyCD);
        }

        /// <summary>
        /// 根据传过来的明细ID数组来获取明细列表
        /// </summary>
        /// <param name="strDetailIDList"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetInfoByArriveList(string strDetailIDList, string CompanyCD)
        {
            return PurchaseArriveInfoDBHelper.GetInfoByArriveList(strDetailIDList, CompanyCD);
        }
    }
}

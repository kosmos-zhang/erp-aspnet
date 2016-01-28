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
    public class ManufactureTaskInfoBus
    {

        #region 生产任务单及其明细信息列表(弹出层显示)
        /// <summary>
        /// 生产任务单及其明细信息列表(弹出层显示)
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetMTDetailInfo(string CompanyCD, string TaskNo, string Subject)
        {
            return ManufactureTaskInfoDBHelper.GetMTDetailInfo(CompanyCD, TaskNo, Subject);
        }
        #endregion


        /// <summary>
        /// 根据生产任务单编号，获取基本信息
        /// </summary>
        /// <param name="TaskNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetMTInfo(string TaskNo, string CompanyCD)
        {
            return ManufactureTaskInfoDBHelper.GetMTInfo(TaskNo, CompanyCD);
        }

        /// <summary>
        /// 根据传过来的明细ID数组来获取明细列表
        /// </summary>
        /// <param name="strDetailIDList"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetInfoByDetalIDList(string strDetailIDList, string CompanyCD)
        {
            return ManufactureTaskInfoDBHelper.GetInfoByDetalIDList(strDetailIDList, CompanyCD);
        }
    }
}

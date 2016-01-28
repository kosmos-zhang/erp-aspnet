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
    public class StorageCommonBus
    {
        /// <summary>
        /// 根据表名，主键，公司代码查出是否状态为制单
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool IsDelete(string TableName, string ID, string CompanyCD)
        {
            return StorageCommonDBHelper.IsDelete(TableName, ID, CompanyCD);
        }
    }
}

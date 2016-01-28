/**********************************************
 * 类作用：   出库入库原因数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/12
 ***********************************************/


using System;
using XBase.Model.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;

namespace XBase.Data.Office.StorageManager
{
    public class CodeReasonTypeDBHelper
    {
        /// <summary>
        /// 获取出入库原因信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetReasonType(string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT [ID]");
            sql.AppendLine(",[CodeName]");
            sql.AppendLine(",[Description]");
            sql.AppendLine("FROM [officedba].[CodeReasonType] where CompanyCD='" + CompanyCD + "' and Flag=1 and UsedStatus='1'");
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        /// <summary>
        /// 根据flag获取原因信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="flag">类型原因类别（1出入库原因，2借货原因，3库存调整原因，4库存调拨原因，5库存报损原因，20销售退货原因，21采购退货原因）</param>
        /// <returns></returns>
        public static DataTable GetReasonTypeByFlag(string CompanyCD, string flag)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT [ID]");
            sql.AppendLine(",[CodeName]");
            sql.AppendLine("FROM [officedba].[CodeReasonType] where CompanyCD='" + CompanyCD + "' and Flag=" + flag + " and UsedStatus='1'");
            return SqlHelper.ExecuteSql(sql.ToString());
        }
    }
}

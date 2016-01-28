using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Business.Common
{
    public class CheckQuoteBus
    {
        #region 验证单据是否被指定表引用
        public static bool CheckBill(string tableName, string colName, object value, string CompanyCD)
        {
            return XBase.Data.Common.CheckQuoteDBHelper.CheckBill(tableName, colName, value, CompanyCD);
        }
        #endregion
    }
}

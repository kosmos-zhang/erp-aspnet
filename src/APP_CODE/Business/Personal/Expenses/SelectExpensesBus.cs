using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.Personal.Expenses;

namespace XBase.Business.Personal.Expenses
{
    public class SelectExpensesBus
    {
        /// <summary>
        /// 获取费用类别
        /// </summary>
        /// <returns></returns>
        public static DataTable GeExpTypeList(int ExpBigTypeID, string strCompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SelectExpensesDBHelper.GeExpTypeList(ExpBigTypeID, strCompanyCD, pageIndex, pageCount, ord, ref TotalCount);
        }
    }
}

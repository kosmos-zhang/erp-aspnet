/********************************
 * 创建人：何小武
 * 创建时间：2009-9-11
 * 描述：费用
 ********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;

namespace XBase.Data.Personal.Expenses
{
    public class SelectExpensesDBHelper
    {
        #region 根据费用大类获取费用列表
        /// <summary>
        /// 根据费用大类获取费用列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GeExpTypeList(int ExpBigTypeID, string strCompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select f.ID, f.CodeName,f.FeeSubjectsNo,d.SubjectsName, c.TypeName, f.Description ");
            strSql.Append(" from officedba.CodeFeeType AS f ");
            strSql.Append(" inner join officedba.CodePublicType AS c ON f.Flag = c.ID AND f.CompanyCD =c.CompanyCD ");
            strSql.Append(" left join officedba.AccountSubjects AS d ON f.FeeSubjectsNo = d.SubjectsCD AND f.CompanyCD =d.CompanyCD ");
            //strSql.Append(" and f.UsedStatus = '1' ");
            if (ExpBigTypeID == -1 || ExpBigTypeID==0)
            {
                strSql.Append(" where f.CompanyCD=@CompanyCD  and f.UsedStatus = '1'");
            }
            else
            {
                strSql.Append(" where f.Flag=@ExpType and f.CompanyCD=@CompanyCD  and f.UsedStatus = '1'");
            }
            SqlParameter[] param = { 
                                   new SqlParameter("@CompanyCD",strCompanyCD),
                                   new SqlParameter("@ExpType",ExpBigTypeID)
                                   };
            return SqlHelper.CreateSqlByPageExcuteSql(strSql.ToString(), pageIndex, pageCount, ord, param, ref TotalCount);
        }
        #endregion
    }
}

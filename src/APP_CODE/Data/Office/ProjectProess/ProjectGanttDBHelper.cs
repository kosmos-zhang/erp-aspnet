using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
namespace XBase.Data.Office.ProjectProess
{
    public class ProjectGanttDBHelper
    {
        public static DataSet GetProssList(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            SqlParameter[] param ={
                                    new SqlParameter("@companyCD",SqlDbType.VarChar,50),
                                    new SqlParameter("@projectid",SqlDbType.Int,4)
                                };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = projectID;
            return SqlHelper.ExecuteSqlX("exec officedba.GetProssList @companyCD,@projectid", param);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

using XBase.Common;
using XBase.Data.DBHelper;

namespace XBase.Data.Personal.AimManager
{
    public class GetDeptListDBHelper
    {
        public static DataTable SelectDeptList() {
            string sqlstr = "select * from  officedba.DeptInfo ";
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlstr;
            
            return SqlHelper.ExecuteSearch(comm);

        }
    }
}

using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;

namespace XBase.Data.SystemManager
{
    public class BillType
    {
        public DataTable GetBillTypeList()
        {
            string sql = "select * from [pubdba].BillType";
            return SqlHelper.ExecuteSql(sql);
        }
    }
}

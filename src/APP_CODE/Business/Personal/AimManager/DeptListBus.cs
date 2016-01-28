using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using XBase.Data.Personal.AimManager;

namespace XBase.Business.Personal.AimManager
{
    public class DeptListBus
    {
           public static DataTable SelectDeptList() {
            DataTable listtable = new DataTable();
            try
            {
                //执行更新操作
                listtable = GetDeptListDBHelper.SelectDeptList();
            }
            catch
            {
                //输出日志

            }
            return listtable;
        }
    }
}

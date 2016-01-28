using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

using XBase.Data.Personal.Task;

namespace XBase.Business.Personal.Task
{
    public class TaskReportBus
    {
        public static DataTable StatTask(Hashtable hs)
        {
            DataTable dt = new DataTable();
            try
            {
                //执行更新操作
                dt = TaskReportDBHelper.StatTask(hs);

            }
            catch
            {
                //输出日志

            }
            return dt;
        }
    }
}

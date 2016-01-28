using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using System.Data;
using XBase.Data.Personal.WorkFlow;

namespace XBase.Business.Personal.WorkFlow
{
    public class FlowReportBus
    {
        public static DataTable StatFlow(Hashtable hs)
        {
            DataTable dt = new DataTable();
            try
            {
                //执行更新操作
                dt = FlowReportDBHelper.StatFlow(hs);

            }
            catch
            {
                //输出日志

            }
            return dt;
        }

        public static DataTable StatFlow(Hashtable hs, int pageindex, int pagesize, string OrderBy, ref int totalcount)
        {
            DataTable dt = new DataTable();
            try
            {
                //执行更新操作
                dt = FlowReportDBHelper.StatFlow(hs, pageindex, pagesize, OrderBy, ref  totalcount);

            }
            catch
            {
                //输出日志

            }
            return dt;
        }
    }
}

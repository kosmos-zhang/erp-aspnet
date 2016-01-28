using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using System.Data;
using XBase.Data.Personal.AimManager;

namespace XBase.Business.Personal.AimManager
{
    public  class AimReportBus
    {
        public static DataTable StatAim( Hashtable hs   ) {
            DataTable dt = new DataTable();
            try
            {
                //执行更新操作
                dt =  AimReportDBHelper.StatAim(hs)  ;

            }
            catch
            {
                //输出日志

            }
            return dt;
        }

        public static DataTable GetAimByDept()
        {
            return AimReportDBHelper.GetAimByDept();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Common
{
    public class XgLoger
    {
        private static EasyLoger.Loger myloger = EasyLoger.Loger.GetInstance();
        public static void Log(EasyLoger.LogMessage msg)
        {
            myloger.Write(msg);
        }

        public static void Log(string msgtxt)
        {
            myloger.Write(new EasyLoger.LogMessage(msgtxt));
        }

        public static void Log(Exception ex)
        {
            myloger.Write(new EasyLoger.LogMessage(ex));

        }
    }
}

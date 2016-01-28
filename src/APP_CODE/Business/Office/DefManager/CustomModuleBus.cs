using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using XBase.Common;

using XBase.Data.Office.DefManager;
namespace XBase.Business.DefManager
{
    public class CustomModuleBus
    {
        public static DataTable GetDataTableList()
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            return CustomModuleDBHelper.GetDataTableList(userInfo);
        }
    }
}

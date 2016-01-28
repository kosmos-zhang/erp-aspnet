using System;
using XBase.Model.Office.HumanManager;
using XBase.Data.Office.HumanManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;
using System.Collections.Generic;

namespace XBase.Business.Office.HumanManager
{
   public  class PerformanceGradeBus
    {

       public static DataTable SearchTaskInfo(PerformanceScoreModel  model)
       {
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //获取公司代码
           model.CompanyCD = userInfo.CompanyCD;
           //执行查询

           return PerformanceGradeDBHelper.SearchTaskInfo(model);

       }




    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using XBase.Business.Common;
using XBase.Common;
using XBase.Data.Common;
using XBase.Data.DBHelper;
using XBase.Model.Common;

using XBase.Data.Office.SellReport;


namespace XBase.Business.Office.SellReport
{
    /// <summary>
    /// 施工摘要表业务类
    /// </summary>
    public class UserProjectInfoBus
    {
        public static int Add(XBase.Model.Office.SellReport.UserProductInfo model)
        {
            return UserProjectInfoDBHelper.Add(model);
        }
        public static int Update(XBase.Model.Office.SellReport.UserProductInfo model)
        {
            return UserProjectInfoDBHelper.Update(model);
        }

        public static DataTable DataList(int pageindex, int pagecount, string OrderBy, XBase.Common.UserInfoUtil userinfo, ref int totalCount)
        {
            return UserProjectInfoDBHelper.DataList(pageindex, pagecount, OrderBy, userinfo, ref totalCount);
        }

        public static DataTable GetDataDetailByID(string id,XBase.Common.UserInfoUtil userinfo)
        {
            return UserProjectInfoDBHelper.GetDataDetailByID(id,userinfo);
        }

        public static int Delete(string idlist)
        {
            return UserProjectInfoDBHelper.Delete(idlist);
        }

        public static DataSet GetSummaryData(string begintime, string endtime, XBase.Common.UserInfoUtil userinfo, string orderby,int summarytype,string point)
        {
            return UserProjectInfoDBHelper.GetSummaryData(begintime, endtime, userinfo, orderby,summarytype,point);
        }
    }
}




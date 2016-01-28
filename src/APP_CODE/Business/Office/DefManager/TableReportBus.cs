using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using XBase.Common;
using XBase.Data.Office.DefManager;
using XBase.Model.Office.DefManager;

namespace XBase.Business.Office.DefManager
{
    public class TableReportBus
    {
        public static int InsertReport(ReportTableModel report, XBase.Common.UserInfoUtil userinfo, string ID,string useridlist)
        {
            return TableReportDBHelper.InsertReport(report, userinfo,ID,useridlist);
        }

        public static DataSet GetTableList()
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            return TableReportDBHelper.GetTableList(userInfo);
        }

        public static DataTable GetReportList(XBase.Common.UserInfoUtil userinfo, string menuname, int pageindex, int pagecount, string OrderBy, ref int totalCount)
        {
            return TableReportDBHelper.GetReportList(userinfo,menuname,pageindex,pagecount,OrderBy,ref totalCount);
        }

        public static DataSet GetRePortByID(string ID)
        {
            return TableReportDBHelper.GetRePortByID(ID);
        }

        public static DataTable GetReportTableByID(string ID, string begindate, string enddate)
        {
            return TableReportDBHelper.GetReportTableByID(ID, begindate, enddate);
        }

        public static int DelReportList(string idlist)
        {
            return TableReportDBHelper.DelReportList(idlist);
        }

        public static string GetUserNameList(string useridlist)
        {
            return TableReportDBHelper.GetUserNameList(useridlist);
        }
    }
}

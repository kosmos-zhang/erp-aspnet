using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

using XBase.Data.DBHelper;

namespace XBase.Data.Personal.WorkFlow
{
    public class FlowReportDBHelper
    {
        public static DataTable StatFlow(Hashtable hs)
        {
            string sqlstr = GetSqlText(hs);
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlstr;
            return SqlHelper.ExecuteSearch(comm);
        }

        public static DataTable StatFlow(Hashtable hs, int pageindex, int pagecount, string OrderBy, ref int Totalcount)
        {
            string sqlstr = GetSqlText(hs);
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlstr;
            return SqlHelper.CreateSqlByPageExcuteSql(comm.CommandText, pageindex, pagecount, OrderBy, null, ref Totalcount);
        }


        private static string GetSqlText(Hashtable hs)
        {
            string groupbystr = "";
            string selectgourstr = "";
            string tempstr = "";
            StringBuilder sb = new StringBuilder();
            GetSelectGroupBy(hs, out groupbystr, out selectgourstr);

            if (hs["FromWitch"].ToString() == "PIC")
            {
                sb.Append(" select  ");
                sb.Append(selectgourstr);
                sb.Append(",count(*)  as Num ");
                sb.Append(" from  officedba.View_RFlow  ");
                tempstr += " where   ApplyDate>='" + hs["StartDate"] + "'  and  ApplyDate<='" + DateTime.Parse(hs["EndDate"].ToString()).AddDays(1).ToString("yyyy-MM-dd") + "' and  CompanyCD='" + hs["CompanyCD"] + "'";
                if (hs.ContainsKey("Status"))
                {
                    tempstr += "   and FlowStatus ='" + hs["Status"] + "'  ";
                }

                if (hs.ContainsKey("DeptID"))
                {
                    tempstr += "   and DeptID ='" + hs["DeptID"] + "'  ";
                }

                sb.Append(tempstr);
                sb.Append(" group by " + groupbystr);
            }
            else
            {
                sb.Append(" select  *  ");
                sb.Append("   from  officedba.View_RFlow  ");
                tempstr += " where   ApplyDate>='" + hs["StartDate"] + "'  and  ApplyDate<='" + DateTime.Parse(hs["EndDate"].ToString()).AddDays(1).ToString("yyyy-MM-dd") + "' and  CompanyCD='" + hs["CompanyCD"] + "'";
                if (hs.ContainsKey("Status"))
                {
                    tempstr += "   and FlowStatus ='" + hs["Status"] + "'  ";
                }
                if (hs.ContainsKey("DeptID"))
                {
                    tempstr += "   and DeptID ='" + hs["DeptID"] + "'  ";
                }
                if (hs.ContainsKey("EmployeeID"))
                {
                    tempstr += "   and EmployeeID ='" + hs["EmployeeID"] + "'  ";
                }

                sb.Append(tempstr);

            }
            return sb.ToString();
        }

        private static void GetSelectGroupBy(Hashtable hs, out string groupbystr, out string selectgourstr)
        {

            string groupbystr1 = "";
            string selectgourstr1 = "";

            if (hs["GroupBy"] != null)
            {
                if (hs["GroupBy"].ToString() == "EmployeeID")
                {
                    selectgourstr1 += " dbo.getEmployeeName(EmployeeID) as  EmployeeName,EmployeeID";
                    groupbystr1 += "  EmployeeID  ";
                }
                else if (hs["GroupBy"].ToString() == "DeptID")
                {
                    selectgourstr1 += " dbo.getDeptNameByID(DeptID) as  DeptName ,DeptID";
                    groupbystr1 += " DeptID  ";
                }
                else if (hs["GroupBy"].ToString() == "FlowStatus")
                {
                    selectgourstr1 += " FlowStatus";
                    groupbystr1 += " FlowStatus  ";
                }
            }
            selectgourstr = selectgourstr1;
            groupbystr = groupbystr1;

        }


    }
}

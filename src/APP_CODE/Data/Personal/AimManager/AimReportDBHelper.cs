using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

using XBase.Data.DBHelper;


namespace XBase.Data.Personal.AimManager
{
    public  class AimReportDBHelper
    {
        
        public static DataTable StatAim( Hashtable hs  ) {
            string sqlstr =  GetSqlText(hs) ;
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlstr;
            return SqlHelper.ExecuteSearch(comm);
        }
      
        private static string GetSqlText( Hashtable hs ) {
            string groupbystr = "";
            string selectgourstr = "";
            string tempstr = "";
            StringBuilder sb = new StringBuilder();
            GetSelectGroupBy(hs,out groupbystr,out selectgourstr);   
            sb.Append( " select  ");
            sb.Append(selectgourstr);
            sb.Append(",count(*)  as ID ");  
            sb.Append(" from  officedba.PlanAim  ");
            tempstr += " where   CreateDate>='" + hs["StartDate"] + "'  and  CreateDate<='" + DateTime.Parse(hs["EndDate"].ToString()).AddDays(1).ToString("yyyy-MM-dd") + "'  and  CompanyCD='" + hs["CompanyCD"] + "'";
            sb.Append(tempstr);
            sb.Append(" group by " + groupbystr);
            return sb.ToString();
        }

        public static DataTable GetAimByDept()
        {
            string str = "select B.* ,A.DeptName from officedba.DeptInfo A inner join (	select DeptID,count(*) statusNum  from officedba.PlanAim "
                        + "where Status=1 and CreateDate>='2008-10-01' and CreateDate<='2009-10-15' and CompanyCD='T0004' "
                        + "group by DeptID "
                        + ")B "
                        + "on A.ID=B.DeptID";
            return SqlHelper.ExecuteSql(str);
        }

        private static void GetSelectGroupBy(Hashtable hs,out string groupbystr, out string selectgourstr)
        {
            string groupbystr1 = "";
            string selectgourstr1 = "";
            if (hs["GroupBy"].ToString() == "PrincipalID")
            {
                selectgourstr1 += " officedba.getEmployeeNameByID(PrincipalID) as PrincipalID";
                groupbystr1 += "  PrincipalID ";
            }
            else if (hs["GroupBy"].ToString() == "DeptID")
            {
                selectgourstr1 += " dbo.getDeptNameByID(DeptID) as  DeptID";
                 groupbystr1  += " DeptID  ";
            }

            if (hs["OrderGroup"].ToString().Trim() != "")
            {
                selectgourstr1 += hs["OrderGroup"].ToString();
                groupbystr1 += hs["OrderGroup"].ToString();
            }

            selectgourstr=selectgourstr1;
            groupbystr = groupbystr1;

        }

       

    }
}

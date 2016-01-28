using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using XBase.Common;
using XBase.Data.DBHelper;
using System.Data.SqlClient;

namespace XBase.Business.Personal.Scratchpad
{
   public  class Scratchpad
    {
       public static  DataTable SearchScratchpad(string where, int pageindex, int pagesize,string orderby, ref int count){
           UserInfoUtil userinfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
           DataTable dt = new DataTable();
           string sqlstr = "select  * from officedba.PersonalScratchpad " + where + " and  Owner=" + userinfo.EmployeeID;

           return dt = SqlHelper.CreateSqlByPageExcuteSql(sqlstr, pageindex, pagesize, orderby, new SqlParameter[0], ref count);
       }

       public static bool DelAddressBook(string ids)
       {
           UserInfoUtil userinfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
           DataTable dt = new DataTable();
           string sqlstr = " delete  from officedba.PersonalScratchpad  where id in (" + ids + ") ";
           SqlCommand comm = new SqlCommand();
           comm.CommandText = sqlstr;
           return SqlHelper.ExecuteTransWithCommand(comm);
       }
    }

}

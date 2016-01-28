using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using XBase.Data.DBHelper;

namespace XBase.Data.Personal.Scratchpad
{
    public  class ScratchpadDBHelper
    {
        public static bool InsertScratchpad(string[]  parm  ) {
            string sqlstr = @"INSERT INTO  [officedba].[PersonalScratchpad]
           ([CompanyCD]
           ,[Owner]
           ,[Title]
           ,[Content]
           ,[RecordsDate]
           ,[Remark])
          VALUES
           (@CompanyCD
           ,@Owner
           ,@Title
           ,@Content
           ,@RecordsDate
           ,@Remark)";

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlstr;
            comm.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar);
            comm.Parameters.AddWithValue("@Owner", SqlDbType.Int);
            comm.Parameters.AddWithValue("@Title", SqlDbType.VarChar);
            comm.Parameters.AddWithValue("@Content", SqlDbType.VarChar);
            comm.Parameters.AddWithValue("@RecordsDate", SqlDbType.DateTime);
            comm.Parameters.AddWithValue("@Remark", SqlDbType.VarChar);
             comm.Parameters["@CompanyCD"].Value = parm[5];
             comm.Parameters["@Owner"].Value =int.Parse(parm[4]);
             comm.Parameters["@Title"].Value = parm[1];
             comm.Parameters["@Content"].Value = parm[2];
             comm.Parameters["@RecordsDate"].Value =DateTime.Now;
             comm.Parameters["@Remark"].Value = parm[3];

           return   SqlHelper.ExecuteTransWithCommand(comm);

        }

        public static bool UpdateScratchpad(string[] parm,string id)
        {
            string sqlstr = @"UPDATE [officedba].[PersonalScratchpad]
                         SET                 
                         [Title] = @Title
                         ,[Content] = @Content
                         ,[Remark] = @Remark
                         WHERE  ID="+id;

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlstr;

            comm.Parameters.AddWithValue("@Title", SqlDbType.VarChar);
            comm.Parameters.AddWithValue("@Content", SqlDbType.VarChar);
            comm.Parameters.AddWithValue("@Remark", SqlDbType.VarChar);
             comm.Parameters["@Title"].Value = parm[1];
             comm.Parameters["@Content"].Value = parm[2];
             comm.Parameters["@Remark"].Value = parm[3];

           return   SqlHelper.ExecuteTransWithCommand(comm);

        }
        

        public static DataTable GetScratchpad(int uid){
            string sqlstr = " select * from officedba.PersonalScratchpad where Id =" + uid;
            return  SqlHelper.ExecuteSql(sqlstr);
        }

    }
}

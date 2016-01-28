using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.DBHelper;

using System.Data.SqlClient;
using System.Data;

using System.Data.OleDb;
using XBase.Model.Personal.AddressBook;
using XBase.Data.Personal.AddressBook;

namespace XBase.Data.Personal.AddressBook
{
    public class AddressBookDBHelper
    {
        public static int InsertAgendaInfo(PersonalLinkman model) {
            string SQLstr = "";
            SQLstr += @"INSERT INTO  [officedba].[PersonalLinkman]
           ([CompanyCD]
           ,[Creator]
           ,[LinkmanGroupID]
           ,[LinkmanName]
           ,[Sex]
           ,[CompanyName]
           ,[Birthday]
           ,[MobilePhone]
           ,[CompanyPhone]
           ,[Email]
           ,[Fax]
           ,[QQ]
           ,[ICQ]
           ,[MSN]
           ,[CompanyWebsite]
           ,[CompanyAddress]
           ,[principalship]
           ,[Remark])
     VALUES
           (@CompanyCD
           ,@Creator
           ,@LinkmanGroupID
           ,@Name
           ,@Sex
           ,@CompanyName
           ,@Birthday
           ,@MobilePhone
           ,@CompanyPhone
           ,@Email
           ,@Fax
           ,@QQ
           ,@ICQ
           ,@MSN
           ,@CompanyWebsite
           ,@CompanyAddress
           ,@principalship
           ,@Remark );select  @SourceID = @@IDENTITY  ";
            if (model.Birthday == "") {
                SQLstr = SQLstr.Replace(",[Birthday]", "").Replace(",@Birthday","");
            }
           SqlCommand comm = new SqlCommand();
           comm.CommandText = SQLstr;
           comm.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar);
           comm.Parameters.AddWithValue("@Creator", SqlDbType.Int);
           comm.Parameters.AddWithValue("@LinkmanGroupID", SqlDbType.Int);
           comm.Parameters.AddWithValue("@Name", SqlDbType.VarChar);
           comm.Parameters.AddWithValue("@Sex", SqlDbType.Char);
           comm.Parameters.AddWithValue("@CompanyName", SqlDbType.VarChar);
           comm.Parameters.AddWithValue("@Birthday", SqlDbType.DateTime);
           comm.Parameters.AddWithValue("@MobilePhone", SqlDbType.VarChar);
           comm.Parameters.AddWithValue("@CompanyPhone", SqlDbType.VarChar);
           comm.Parameters.AddWithValue("@Email", SqlDbType.VarChar);
           comm.Parameters.AddWithValue("@Fax", SqlDbType.VarChar);
           comm.Parameters.AddWithValue("@QQ", SqlDbType.VarChar);
           comm.Parameters.AddWithValue("@ICQ", SqlDbType.VarChar);
           comm.Parameters.AddWithValue("@MSN", SqlDbType.VarChar);
           comm.Parameters.AddWithValue("@CompanyWebsite", SqlDbType.VarChar);
           comm.Parameters.AddWithValue("@CompanyAddress", SqlDbType.VarChar);
           comm.Parameters.AddWithValue("@principalship", SqlDbType.VarChar);
           comm.Parameters.AddWithValue("@Remark", SqlDbType.VarChar);
           comm.Parameters.AddWithValue("@SourceID", SqlDbType.Int);
            comm.Parameters["@CompanyCD"].Value = model.CompanyCD;
            comm.Parameters["@Creator"].Value = model.Creator;
            comm.Parameters["@LinkmanGroupID"].Value = model.LinkmanGroupID;
            comm.Parameters["@Name"].Value = model.Name;
            comm.Parameters["@Sex"].Value = model.Sex;
            comm.Parameters["@CompanyName"].Value = model.CompanyName;
            if (model.Birthday == "")
            {
                comm.Parameters["@Birthday"].Value = DateTime.Now;
            }
            else {
                comm.Parameters["@Birthday"].Value = Convert.ToDateTime(model.Birthday);
            }            
            comm.Parameters["@MobilePhone"].Value = model.MobilePhone;
            comm.Parameters["@CompanyPhone"].Value = model.CompanyPhone;
            comm.Parameters["@Email"].Value = model.Email;
            comm.Parameters["@Fax"].Value = model.Fax;
            comm.Parameters["@QQ"].Value = model.QQ;
            comm.Parameters["@ICQ"].Value = model.ICQ;
            comm.Parameters["@MSN"].Value = model.MSN;
            comm.Parameters["@CompanyWebsite"].Value = model.CompanyWebsite;
            comm.Parameters["@CompanyAddress"].Value = model.CompanyAddress;
            comm.Parameters["@principalship"].Value = model.principalship;
            comm.Parameters["@Remark"].Value = model.Remark;
            comm.Parameters["@SourceID"].Direction = ParameterDirection.Output;
            if (SqlHelper.ExecuteTransWithCommand(comm))
            {
                int temp=-1;
                if (comm.Parameters["@SourceID"].Value != null)
                    temp = Convert.ToInt32(comm.Parameters["@SourceID"].Value);
                return   temp;
            }
            else {
                return -1;
            }
        }

        public static bool UpdateAgendaInfo(PersonalLinkman model)
        { 
         string SQLstr = "";
         SQLstr += @"UPDATE [officedba].[PersonalLinkman]
       SET [CompanyCD] =  @CompanyCD
      ,[Creator] =  @Creator
      ,[LinkmanGroupID] = @LinkmanGroupID
      ,[LinkmanName] =  @Name
      ,[Sex] = @Sex
      ,[CompanyName] = @CompanyName
      ,[Birthday] = @Birthday
      ,[MobilePhone] =@MobilePhone
      ,[CompanyPhone] = @CompanyPhone
      ,[Email] =  @Email
      ,[Fax] =  @Fax
      ,[QQ] =  @QQ
      ,[ICQ] =  @ICQ
      ,[MSN] =  @MSN
      ,[CompanyWebsite] =  @CompanyWebsite
      ,[CompanyAddress] =  @CompanyAddress
      ,[principalship] =  @principalship
      ,[Remark] = @Remark
      WHERE  ID=" + model.ID;
         if (model.Birthday == "")
         {
             SQLstr = SQLstr.Replace(",[Birthday] = @Birthday", "");
         }
         SqlCommand comm = new SqlCommand();
         comm.CommandText = SQLstr;
         comm.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar);
         comm.Parameters.AddWithValue("@Creator", SqlDbType.Int);
         comm.Parameters.AddWithValue("@LinkmanGroupID", SqlDbType.Int);
         comm.Parameters.AddWithValue("@Name", SqlDbType.VarChar);
         comm.Parameters.AddWithValue("@Sex", SqlDbType.Char);
         comm.Parameters.AddWithValue("@CompanyName", SqlDbType.VarChar);
         comm.Parameters.AddWithValue("@Birthday", SqlDbType.DateTime);
         comm.Parameters.AddWithValue("@MobilePhone", SqlDbType.VarChar);
         comm.Parameters.AddWithValue("@CompanyPhone", SqlDbType.VarChar);
         comm.Parameters.AddWithValue("@Email", SqlDbType.VarChar);
         comm.Parameters.AddWithValue("@Fax", SqlDbType.VarChar);
         comm.Parameters.AddWithValue("@QQ", SqlDbType.VarChar);
         comm.Parameters.AddWithValue("@ICQ", SqlDbType.VarChar);
         comm.Parameters.AddWithValue("@MSN", SqlDbType.VarChar);
         comm.Parameters.AddWithValue("@CompanyWebsite", SqlDbType.VarChar);
         comm.Parameters.AddWithValue("@CompanyAddress", SqlDbType.VarChar);
         comm.Parameters.AddWithValue("@principalship", SqlDbType.VarChar);
         comm.Parameters.AddWithValue("@Remark", SqlDbType.VarChar);
         comm.Parameters["@CompanyCD"].Value = model.CompanyCD;
         comm.Parameters["@Creator"].Value = model.Creator;
         comm.Parameters["@LinkmanGroupID"].Value = model.LinkmanGroupID;
         comm.Parameters["@Name"].Value = model.Name;
         comm.Parameters["@Sex"].Value = model.Sex;
         comm.Parameters["@CompanyName"].Value = model.CompanyName;
         if (model.Birthday == "")
         {
             comm.Parameters["@Birthday"].Value = DateTime.Now;
         }
         else
         {
             comm.Parameters["@Birthday"].Value = Convert.ToDateTime(model.Birthday);
         }
         comm.Parameters["@MobilePhone"].Value = model.MobilePhone;
         comm.Parameters["@CompanyPhone"].Value = model.CompanyPhone;
         comm.Parameters["@Email"].Value = model.Email;
         comm.Parameters["@Fax"].Value = model.Fax;
         comm.Parameters["@QQ"].Value = model.QQ;
         comm.Parameters["@ICQ"].Value = model.ICQ;
         comm.Parameters["@MSN"].Value = model.MSN;
         comm.Parameters["@CompanyWebsite"].Value = model.CompanyWebsite;
         comm.Parameters["@CompanyAddress"].Value = model.CompanyAddress;
         comm.Parameters["@principalship"].Value = model.principalship;
         comm.Parameters["@Remark"].Value = model.Remark;
         return SqlHelper.ExecuteTransWithCommand(comm);
          
           
        }


        /// <summary>
        /// 取Excel数据并读取到officedba.AddressInfo中
        /// zxb 2009-09-01
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static DataSet ReadEexcel(string FilePath, string companycd)
        {
            DataTable dt = ExcelToDS(FilePath, "Sheet1");


            DataSet ds = new DataSet();
            ds.Tables.Add(dt.Copy());
            return ds;
        }

                /// <summary>
        /// 取Excel数据并读取到officedba.AddressInfo中
        /// zxb 2009-09-01
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static bool AppendRecordByExcel( string sqlstr)
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlstr;
           return SqlHelper.ExecuteTransWithCommand(comm);
        }

        

        public static DataTable ExcelToDS(string Path, string TabelName)
        {
            string strConn =  @"Provider=Microsoft.ACE.OLEDB.12.0; "+   "Data Source= "+Path+ ";Extended Properties= 'Excel 12.0;HDR=Yes;IMEX=1 ' "; 
            DataSet ds = null;
            //try
            //{
                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                strExcel = "select  distinct * from [" + TabelName + "$]  as " + TabelName;
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                ds = new DataSet();
                myCommand.Fill(ds, TabelName);
              
            //}
            //catch {
                
            //}
            if (ds == null)
            {
                return new DataTable();
            }
            return ds.Tables[0];
        }



        public static DataTable GetLinkmanType(int uid) {
            string sqlstr = "select  * from  officedba.LinkmanType where Owner="+ uid ;
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(sqlstr);
            return dt;
        }


        public static XBase.Model.Personal.AddressBook.PersonalLinkman GetPersonalLinkmanModel(int ID)
        {
            XBase.Model.Personal.AddressBook.PersonalLinkman model = new PersonalLinkman();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" ID,CompanyCD,Creator,LinkmanGroupID,LinkmanName,Sex,CompanyName,Birthday,MobilePhone,CompanyPhone,Email,Fax,QQ,ICQ,MSN,CompanyWebsite,CompanyAddress,principalship,Remark ");
            strSql.Append(" from officedba.PersonalLinkman ");
            strSql.Append(" where ID=" + ID + " ");
            DataSet ds = new DataSet();
            DataTable dt   = SqlHelper.ExecuteSql(strSql.ToString());
            ds.Tables.Add(dt);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.CompanyCD = ds.Tables[0].Rows[0]["CompanyCD"].ToString();
                if (ds.Tables[0].Rows[0]["Creator"].ToString() != "")
                {
                    model.Creator = int.Parse(ds.Tables[0].Rows[0]["Creator"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LinkmanGroupID"].ToString() != "")
                {
                    model.LinkmanGroupID = int.Parse(ds.Tables[0].Rows[0]["LinkmanGroupID"].ToString());
                }
                model.Name = ds.Tables[0].Rows[0]["LinkmanName"].ToString();
                model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                model.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = ds.Tables[0].Rows[0]["Birthday"].ToString();
                }
                model.MobilePhone = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
                model.CompanyPhone = ds.Tables[0].Rows[0]["CompanyPhone"].ToString();
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                model.Fax = ds.Tables[0].Rows[0]["Fax"].ToString();
                model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                model.ICQ = ds.Tables[0].Rows[0]["ICQ"].ToString();
                model.MSN = ds.Tables[0].Rows[0]["MSN"].ToString();
                model.CompanyWebsite = ds.Tables[0].Rows[0]["CompanyWebsite"].ToString();
                model.CompanyAddress = ds.Tables[0].Rows[0]["CompanyAddress"].ToString();
                model.principalship = ds.Tables[0].Rows[0]["principalship"].ToString();
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
            }
            return model;
        }

    }
}

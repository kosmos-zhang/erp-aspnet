using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using XBase.Model.Personal.AddressBook;
using XBase.Data.Personal.AddressBook;
using XBase.Common;
using XBase.Data.DBHelper;

namespace XBase.Business.Personal.AddressBook
{
    public class AddressBookInfo : System.Web.SessionState.IRequiresSessionState
    {
        public static int SaveAgendaInfo(PersonalLinkman model)
        {
            if (ConstUtil.EDIT_FLAG_INSERT.Equals(model.EditFlag))
            {
                return AddressBookDBHelper.InsertAgendaInfo(model);
            }
            else if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {
                return AddressBookDBHelper.UpdateAgendaInfo(model) == true ? 0 : -1;
            }

            return -1;
        }

        public static DataTable GetLinkmanType(int uid)
        {
            return AddressBookDBHelper.GetLinkmanType(uid);
        }


        public static PersonalLinkman GetPersonalLinkmanModel(int id)
        {
            return AddressBookDBHelper.GetPersonalLinkmanModel(id);
        }

        public static DataTable SearchAddressBook(PersonalLinkman model, int pageindex, int pagesize, string orderby, ref int count)
        {

            UserInfoUtil userinfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
            DataTable dt = new DataTable();
            string sqlstr = "select  * from officedba.PersonalLinkman where  Creator=" + userinfo.EmployeeID;
            SqlCommand comm = new SqlCommand();

            if (model.Birthday != null && model.Birthday != "")
            {
                sqlstr += " and Birthday=@Birthday ";
                comm.Parameters.AddWithValue("@Birthday", SqlDbType.DateTime);
                comm.Parameters["@Birthday"].Value = Convert.ToDateTime(model.Birthday);
            }
            if (model.Name != null && model.Name != "")
            {
                sqlstr += " and LinkmanName   like  @Name ";
                comm.Parameters.AddWithValue("@Name", SqlDbType.VarChar);
                comm.Parameters["@Name"].Value = "%" + model.Name + "%";
            }
            if (model.MobilePhone != null && model.MobilePhone != "")
            {
                sqlstr += " and MobilePhone=@MobilePhone ";
                comm.Parameters.AddWithValue("@MobilePhone", SqlDbType.VarChar);
                comm.Parameters["@MobilePhone"].Value = model.MobilePhone;
            }
            if (model.CompanyPhone != null && model.CompanyPhone != "")
            {
                sqlstr += " and CompanyPhone=@CompanyPhone ";
                comm.Parameters.AddWithValue("@CompanyPhone", SqlDbType.VarChar);
                comm.Parameters["@CompanyPhone"].Value = model.CompanyPhone;
            }
            if (model.QQ != null && model.QQ != "")
            {
                sqlstr += " and QQ=@QQ ";
                comm.Parameters.AddWithValue("@QQ", SqlDbType.VarChar);
                comm.Parameters["@QQ"].Value = model.QQ;
            }
            if (model.CompanyName != null && model.CompanyName != "")
            {
                sqlstr += " and CompanyName  like @CompanyName ";
                comm.Parameters.AddWithValue("@CompanyName", SqlDbType.VarChar);
                comm.Parameters["@CompanyName"].Value = "%" + model.CompanyName + "%";
            }
            if (model.LinkmanGroupID != 0)
            {
                sqlstr += " and LinkmanGroupID=@LinkmanGroupID ";
                comm.Parameters.AddWithValue("@LinkmanGroupID", SqlDbType.VarChar);
                comm.Parameters["@LinkmanGroupID"].Value = model.LinkmanGroupID;
            }

            SqlParameter[] parmlist = new SqlParameter[comm.Parameters.Count];
            int i = 0;
            foreach (SqlParameter sp in comm.Parameters)
            {
                parmlist[i] = new SqlParameter(sp.ParameterName, sp.DbType);
                parmlist[i].Value = sp.Value;
                i++;
            }

            return dt = SqlHelper.CreateSqlByPageExcuteSql(sqlstr, pageindex, pagesize, orderby, parmlist, ref count);

        }

        public static bool DelAddressBook(string ids)
        {
            UserInfoUtil userinfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
            DataTable dt = new DataTable();
            string sqlstr = " delete  from officedba.PersonalLinkman where id in (" + ids + ") ";
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlstr;
            return SqlHelper.ExecuteTransWithCommand(comm);
        }

        public static bool DelAddressBookGroup(string ids)
        {
            UserInfoUtil userinfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
            DataTable dt = new DataTable();
            string sqlstr = " delete  from  officedba.LinkmanType where id in (" + ids + ") ";
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlstr;
            return SqlHelper.ExecuteTransWithCommand(comm);

        }
        public static int AddAddressBookGroup(string title)
        {
            UserInfoUtil userinfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
            DataTable dt = new DataTable();
            string sqlstr = @"INSERT INTO [officedba].[LinkmanType]
           ([CompanyCD]
           ,[Owner]
           ,[Title]
           )
     VALUES
           ( @CompanyCD
           ,@Owner
           ,@Title
           );select @id =@@IDENTITY ";
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlstr;

            comm.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar);
            comm.Parameters["@CompanyCD"].Value = userinfo.CompanyCD;

            comm.Parameters.AddWithValue("@Owner", SqlDbType.VarChar);
            comm.Parameters["@Owner"].Value = userinfo.EmployeeID;

            comm.Parameters.AddWithValue("@Title", SqlDbType.VarChar);
            comm.Parameters["@Title"].Value = title;

            comm.Parameters.AddWithValue("@id", SqlDbType.Int);
            comm.Parameters["@id"].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteTransWithCommand(comm);
            return int.Parse(comm.Parameters["@id"].Value.ToString());
        }

        public static bool ModAddressBook(string ids, string title)
        {
            UserInfoUtil userinfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
            DataTable dt = new DataTable();
            string sqlstr = " UPDATE [officedba].[LinkmanType]  SET   [Title] = '" + title + "'  WHERE  ID=" + ids;
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlstr;
            return SqlHelper.ExecuteTransWithCommand(comm);

        }

        public static DataTable GetGroupInfo() {
            UserInfoUtil userinfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
            string sqlstr = "select * from officedba.LinkmanType  where Owner=" + userinfo.EmployeeID;
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlstr;
            return SqlHelper.ExecuteSearch(comm);
        }

        public static bool AppendRecordByExcel(DataTable dt)
        {
            UserInfoUtil userinfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
            string sqlstr = "";
            foreach (DataRow dr in dt.Rows)
            {
                sqlstr += @"INSERT INTO [officedba].[PersonalLinkman]
                    ([CompanyCD]
           ,[Creator]
           ,[LinkmanGroupID]
           ,[LinkmanName]
           ,[Sex]
           ,[CompanyName]";
                if ((dr["Birthday"] + "").Trim() != "")
                {
                    sqlstr += ",[Birthday]";
                }
           sqlstr +=@"  ,[MobilePhone]
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
                 VALUES  ( '";
                sqlstr +=  userinfo.CompanyCD+"',";
                sqlstr += userinfo.EmployeeID + ",";
                sqlstr += dr["LinkmanGroupID"] + ",'";
                sqlstr += dr["LinkmanName"] + "','";
                sqlstr += dr["Sex"] + "','";
                sqlstr += dr["CompanyName"] + "','";
                if ( (dr["Birthday"]+"").Trim() != "")
                {
                    sqlstr += dr["Birthday"] + "','";
                }
                sqlstr += dr["MobilePhone"] + "','";
                sqlstr += dr["CompanyPhone"] + "','";
                sqlstr += dr["Email"] + "','";
                sqlstr += dr["Fax"] + "','";
                sqlstr += dr["QQ"] + "','";
                sqlstr += dr["ICQ"] + "','";
                sqlstr += dr["MSN"] + "','";
                sqlstr += dr["CompanyWebsite"] + "','";
                sqlstr += dr["CompanyAddress"] + "','";
                sqlstr += dr["principalship"] + "','";
                sqlstr += dr["Remark"] + "')";
            }
            return XBase.Data.Personal.AddressBook.AddressBookDBHelper.AppendRecordByExcel(sqlstr);
        }

        public static DataSet ReadEexcel(string FilePath, string companycd)
        {
            return XBase.Data.Personal.AddressBook.AddressBookDBHelper.ReadEexcel(FilePath, companycd);
        }

    }
}

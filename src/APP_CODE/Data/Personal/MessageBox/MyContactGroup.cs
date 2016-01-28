using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;

namespace XBase.Data.Personal.MessageBox
{
	/// <summary>
	/// 数据访问类MyContactGroup。
	/// </summary>
	public class MyContactGroup
	{
		public MyContactGroup()
		{}
		#region  成员方法


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from officedba.MyContactGroup");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

            return SqlHelper.ExecuteSql(strSql.ToString(), parameters).Rows.Count > 0;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(XBase.Model.Personal.MessageBox.MyContactGroup model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into officedba.MyContactGroup(");
			strSql.Append("CompanyCD,GroupName,Creator,CreateDate,ModifiedDate,ModifiedUserID)");
			strSql.Append(" values (");
			strSql.Append("@CompanyCD,@GroupName,@Creator,@CreateDate,@ModifiedDate,@ModifiedUserID)");
            strSql.Append(";select @ID=@@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@GroupName", SqlDbType.VarChar,50),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                     new SqlParameter("@ID", SqlDbType.Int,0) };
			parameters[0].Value = model.CompanyCD;
			parameters[1].Value = model.GroupName;
			parameters[2].Value = model.Creator;
			parameters[3].Value = model.CreateDate;
			parameters[4].Value = model.ModifiedDate;
			parameters[5].Value = model.ModifiedUserID;
            parameters[6].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
            return int.Parse(parameters[6].Value.ToString());
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(XBase.Model.Personal.MessageBox.MyContactGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update officedba.MyContactGroup set ");
			strSql.Append("CompanyCD=@CompanyCD,");
			strSql.Append("GroupName=@GroupName,");
			strSql.Append("Creator=@Creator,");
			strSql.Append("CreateDate=@CreateDate,");
			strSql.Append("ModifiedDate=@ModifiedDate,");
			strSql.Append("ModifiedUserID=@ModifiedUserID");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@GroupName", SqlDbType.VarChar,50),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.CompanyCD;
			parameters[2].Value = model.GroupName;
			parameters[3].Value = model.Creator;
			parameters[4].Value = model.CreateDate;
			parameters[5].Value = model.ModifiedDate;
			parameters[6].Value = model.ModifiedUserID;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete officedba.MyContactGroup");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public XBase.Model.Personal.MessageBox.MyContactGroup GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CompanyCD,GroupName,Creator,CreateDate,ModifiedDate,ModifiedUserID from officedba.MyContactGroup");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			XBase.Model.Personal.MessageBox.MyContactGroup model=new XBase.Model.Personal.MessageBox.MyContactGroup();
            DataTable dt = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.CompanyCD=ds.Tables[0].Rows[0]["CompanyCD"].ToString();
				model.GroupName=ds.Tables[0].Rows[0]["GroupName"].ToString();
				if(ds.Tables[0].Rows[0]["Creator"].ToString()!="")
				{
					model.Creator=int.Parse(ds.Tables[0].Rows[0]["Creator"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ModifiedDate"].ToString()!="")
				{
					model.ModifiedDate=DateTime.Parse(ds.Tables[0].Rows[0]["ModifiedDate"].ToString());
				}
				model.ModifiedUserID=ds.Tables[0].Rows[0]["ModifiedUserID"].ToString();
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataTable GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,CompanyCD,GroupName,Creator,CreateDate,ModifiedDate,ModifiedUserID ");
            strSql.Append(" FROM officedba.MyContactGroup ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return DBHelper.SqlHelper.ExecuteSql(strSql.ToString());
		}

        
		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "MyContactGroup";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  成员方法
	}
}


using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace XBase.Data.Decision
{
	/// <summary>
	/// 数据访问类DataCustLevel。
	/// </summary>
	public class DataCustLevel
	{
		public DataCustLevel()
		{}
		#region  成员方法

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from statdba.DataCustLevel");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

            return Database.RunSql(strSql.ToString(), parameters).Tables[0].Rows.Count > 0;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(XBase.Model.Decision.DataCustLevel model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into statdba.DataCustLevel(");
			strSql.Append("CompanyCD,GName,GUp,GDown)");
			strSql.Append(" values (");
			strSql.Append("@CompanyCD,@GName,@GUp,@GDown)");
			SqlParameter[] parameters = {					
					new SqlParameter("@CompanyCD", SqlDbType.Char,8),
					new SqlParameter("@GName", SqlDbType.NVarChar,50),
					new SqlParameter("@GUp", SqlDbType.Int,4),
					new SqlParameter("@GDown", SqlDbType.Int,4)};		
			parameters[0].Value = model.CompanyCD;
			parameters[1].Value = model.GName;
			parameters[2].Value = model.GUp;
			parameters[3].Value = model.GDown;

            Database.RunSql(strSql.ToString(), parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public void Update(XBase.Model.Decision.DataCustLevel model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update statdba.DataCustLevel set ");
			strSql.Append("CompanyCD=@CompanyCD,");
			strSql.Append("GName=@GName,");
			strSql.Append("GUp=@GUp,");
			strSql.Append("GDown=@GDown");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.Char,8),
					new SqlParameter("@GName", SqlDbType.NVarChar,50),
					new SqlParameter("@GUp", SqlDbType.Int,4),
					new SqlParameter("@GDown", SqlDbType.Int,4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.CompanyCD;
			parameters[2].Value = model.GName;
			parameters[3].Value = model.GUp;
			parameters[4].Value = model.GDown;

            Database.RunSql(strSql.ToString(), parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete statdba.DataCustLevel ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

            Database.RunSql(strSql.ToString(), parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public XBase.Model.Decision.DataCustLevel GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 ID,CompanyCD,GName,GUp,GDown from statdba.DataCustLevel ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

            XBase.Model.Decision.DataCustLevel model = new XBase.Model.Decision.DataCustLevel();
            DataSet ds = Database.RunSql(strSql.ToString(), parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.CompanyCD=ds.Tables[0].Rows[0]["CompanyCD"].ToString();
				model.GName=ds.Tables[0].Rows[0]["GName"].ToString();
				if(ds.Tables[0].Rows[0]["GUp"].ToString()!="")
				{
					model.GUp=int.Parse(ds.Tables[0].Rows[0]["GUp"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GDown"].ToString()!="")
				{
					model.GDown=int.Parse(ds.Tables[0].Rows[0]["GDown"].ToString());
				}
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
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,CompanyCD,GName,GUp,GDown ");
            strSql.Append(" FROM statdba.DataCustLevel ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return Database.RunSql(strSql.ToString());
		}

		

		#endregion  成员方法
	}
}


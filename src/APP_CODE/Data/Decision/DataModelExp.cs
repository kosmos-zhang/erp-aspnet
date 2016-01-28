using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace XBase.Data.Decision
{
	/// <summary>
	/// 数据访问类DataModelExp。
	/// </summary>
	public class DataModelExp
	{
		public DataModelExp()
		{}
		#region  成员方法

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from statdba.DataModelExp");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

            return Database.RunSql(strSql.ToString(), parameters).Tables[0].Rows.Count > 0;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(XBase.Model.Decision.DataModelExp model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into statdba.DataModelExp(");
            strSql.Append("CompanyCD,Expressions,ExpType,CreateDate)");
			strSql.Append(" values (");
            strSql.Append("@CompanyCD,@Expressions,@ExpType,@CreateDate)");
			SqlParameter[] parameters = {					
					new SqlParameter("@CompanyCD", SqlDbType.Char,8),
					new SqlParameter("@Expressions", SqlDbType.VarChar,512),
                    new SqlParameter("@ExpType", SqlDbType.Char,1),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			
			parameters[0].Value = model.CompanyCD;
			parameters[1].Value = model.Expressions;
            parameters[2].Value = model.ExpType;
			parameters[3].Value = model.CreateDate;

			Database.RunSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public void Update(XBase.Model.Decision.DataModelExp model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update statdba.DataModelExp set ");
			strSql.Append("CompanyCD=@CompanyCD,");
			strSql.Append("Expressions=@Expressions,");
            strSql.Append("ExpType=@ExpType,");
			strSql.Append("CreateDate=@CreateDate");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.Char,8),
					new SqlParameter("@Expressions", SqlDbType.VarChar,512),
                    new SqlParameter("@ExpType", SqlDbType.Char,1),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.CompanyCD;
			parameters[2].Value = model.Expressions;
            parameters[3].Value = model.ExpType;
			parameters[4].Value = model.CreateDate;

			Database.RunSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete statdba.DataModelExp ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			Database.RunSql(strSql.ToString(),parameters);
		}


        /// <summary>
        /// 删除多条数据
        /// </summary>
        public void Delete(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete statdba.DataModelExp ");
            strSql.Append(" where ");
            strSql.Append(strWhere);

            Database.RunSql(strSql.ToString());
        }


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public XBase.Model.Decision.DataModelExp GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ID,CompanyCD,Expressions,ExpType,CreateDate from statdba.DataModelExp ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

            XBase.Model.Decision.DataModelExp model = new XBase.Model.Decision.DataModelExp();
            DataSet ds = Database.RunSql(strSql.ToString(), parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.CompanyCD=ds.Tables[0].Rows[0]["CompanyCD"].ToString();
				model.Expressions=ds.Tables[0].Rows[0]["Expressions"].ToString();
                model.ExpType = ds.Tables[0].Rows[0]["ExpType"].ToString();
				if(ds.Tables[0].Rows[0]["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
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
            strSql.Append("select ID,CompanyCD,Expressions,ExpType,CreateDate ");
            strSql.Append(" FROM statdba.DataModelExp ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return Database.RunSql(strSql.ToString());
		}

		

		#endregion  成员方法
	}
}


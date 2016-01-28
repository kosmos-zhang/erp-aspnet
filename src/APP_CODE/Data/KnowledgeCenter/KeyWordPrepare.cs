using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace XBase.Data.KnowledgeCenter
{
	/// <summary>
	/// 数据访问
	/// </summary>
	public class KeyWordPrepare
	{
		private XBase.Model.KnowledgeCenter.KeyWordPrepare GetEntity(SqlDataReader dr)
		{
			XBase.Model.KnowledgeCenter.KeyWordPrepare entity = new XBase.Model.KnowledgeCenter.KeyWordPrepare();
			entity.ID = SqlClientUtility.GetInt32(dr,"ID",0);
			entity.TypeID = SqlClientUtility.GetInt32(dr,"TypeID",0);
			entity.KeyWordName = SqlClientUtility.GetString(dr,"KeyWordName",String.Empty);
			entity.CreateUserID = SqlClientUtility.GetString(dr,"CreateUserID",String.Empty);
			entity.CreateDate = SqlClientUtility.GetDateTime(dr,"CreateDate",DateTime.Now);
			entity.ModifiedDate = SqlClientUtility.GetDateTime(dr,"ModifiedDate",DateTime.Now);
			entity.ModifiedUserID = SqlClientUtility.GetString(dr,"ModifiedUserID",String.Empty);
			return entity;
		}
		public bool Create(XBase.Model.KnowledgeCenter.KeyWordPrepare entity)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@TypeID",SqlDbType.Int,0,entity.TypeID),
				SqlParameterHelper.MakeInParam("@KeyWordName",SqlDbType.VarChar,100,entity.KeyWordName),
				SqlParameterHelper.MakeInParam("@CreateUserID",SqlDbType.VarChar,20,entity.CreateUserID),
				SqlParameterHelper.MakeInParam("@CreateDate",SqlDbType.DateTime,0,entity.CreateDate),
				SqlParameterHelper.MakeInParam("@ModifiedDate",SqlDbType.DateTime,0,entity.ModifiedDate),
				SqlParameterHelper.MakeInParam("@ModifiedUserID",SqlDbType.VarChar,20,entity.ModifiedUserID)
			};
			bool result;
			Database.RunProc("[knowdba].KeyWordPrepare_Create",parameters,out result); 
			return result;
		}


		public int UpdateByID(XBase.Model.KnowledgeCenter.KeyWordPrepare entity)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,entity.ID),
				SqlParameterHelper.MakeInParam("@TypeID",SqlDbType.Int,0,entity.TypeID),
				SqlParameterHelper.MakeInParam("@KeyWordName",SqlDbType.VarChar,100,entity.KeyWordName),
				SqlParameterHelper.MakeInParam("@CreateUserID",SqlDbType.VarChar,20,entity.CreateUserID),
				SqlParameterHelper.MakeInParam("@CreateDate",SqlDbType.DateTime,0,entity.CreateDate),
				SqlParameterHelper.MakeInParam("@ModifiedDate",SqlDbType.DateTime,0,entity.ModifiedDate),
				SqlParameterHelper.MakeInParam("@ModifiedUserID",SqlDbType.VarChar,20,entity.ModifiedUserID)
			};
			int result;
			Database.RunProc("[knowdba].KeyWordPrepare_UpdateByID",parameters,out result); 
			return result;
		}


		public int DeleteByID(int iD)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,iD)
			};
			int result;
			Database.RunProc("[knowdba].KeyWordPrepare_DeleteByID",parameters,out result); 
			return result;
		}


		public XBase.Model.KnowledgeCenter.KeyWordPrepare GetEntityByID(int iD)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,iD)
			};
			XBase.Model.KnowledgeCenter.KeyWordPrepare entity = null ;
			SqlDataReader dr;
			Database.RunProc("[knowdba].KeyWordPrepare_GetEntityByID",parameters,out dr); 
			if(dr.Read())
			{
				 entity = GetEntity(dr);
			}
			dr.Close();
			return entity;
		}


		public DataSet Select()
		{
			SqlParameter[] parameters = new SqlParameter[]{
			};
			DataSet ds;
			Database.RunProc("[knowdba].KeyWordPrepare_Select",parameters,out ds); 
			return ds;
		}


		public DataSet SelectWhereOrderedEx(string Where,string OrderExp,string Fields)
		{
			SqlParameter[] parameters = new SqlParameter[]{
SqlParameterHelper.MakeInParam("@Where",SqlDbType.VarChar,200,Where)
,SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.VarChar,100,OrderExp)
,SqlParameterHelper.MakeInParam("@Fields",SqlDbType.VarChar,300,Fields)
			};
			DataSet ds;
			Database.RunProc("[knowdba].KeyWordPrepare_SelectWhereOrderedEx",parameters,out ds); 
			return ds;
		}


		public DataSet SelectWhereOrderedTopNEx(string Where,string OrderExp,int Num,string Fields)
		{
			SqlParameter[] parameters = new SqlParameter[]{
SqlParameterHelper.MakeInParam("@Where",SqlDbType.VarChar,200,Where)
,SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.VarChar,100,OrderExp)
,SqlParameterHelper.MakeInParam("@Num",SqlDbType.Int,0,Num)
,SqlParameterHelper.MakeInParam("@Fields",SqlDbType.VarChar,300,Fields)
			};
			DataSet ds;
			Database.RunProc("[knowdba].KeyWordPrepare_SelectWhereOrderedTopNEx",parameters,out ds); 
			return ds;
		}


		public int GetCount(string where)
		{
			SqlParameter[] parameters = new SqlParameter[]{
SqlParameterHelper.MakeInParam("@where",SqlDbType.VarChar,200,where),
SqlParameterHelper.MakeParam("@RecCount",SqlDbType.Int,0,ParameterDirection.Output,null)
			};
			Database.RunProc("[knowdba].KeyWordPrepare_GetCount",parameters); 
return Convert.ToInt32(parameters[parameters.Length - 1].Value);
		}


	}
}
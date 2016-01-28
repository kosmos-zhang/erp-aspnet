using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace XBase.Data.KnowledgeCenter
{
	/// <summary>
	/// 数据访问
	/// </summary>
	public class KnowledgeType
	{
		private XBase.Model.KnowledgeCenter.KnowledgeType GetEntity(SqlDataReader dr)
		{
			XBase.Model.KnowledgeCenter.KnowledgeType entity = new XBase.Model.KnowledgeCenter.KnowledgeType();
			entity.ID = SqlClientUtility.GetInt32(dr,"ID",0);
			entity.Flag = SqlClientUtility.GetString(dr,"Flag",String.Empty);
			entity.TypeName = SqlClientUtility.GetString(dr,"TypeName",String.Empty);
			entity.SupperTypeID = SqlClientUtility.GetInt32(dr,"SupperTypeID",0);
			entity.ModifiedDate = SqlClientUtility.GetDateTime(dr,"ModifiedDate",DateTime.Now);
			entity.ModifiedUserID = SqlClientUtility.GetString(dr,"ModifiedUserID",String.Empty);
            entity.Path = SqlClientUtility.GetString(dr, "Path", string.Empty);
			return entity;
		}
		public bool Create(XBase.Model.KnowledgeCenter.KnowledgeType entity)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@Flag",SqlDbType.Char,1,entity.Flag),
				SqlParameterHelper.MakeInParam("@TypeName",SqlDbType.VarChar,100,entity.TypeName),
				SqlParameterHelper.MakeInParam("@SupperTypeID",SqlDbType.Int,0,entity.SupperTypeID),
				SqlParameterHelper.MakeInParam("@ModifiedDate",SqlDbType.DateTime,0,entity.ModifiedDate),
				SqlParameterHelper.MakeInParam("@ModifiedUserID",SqlDbType.VarChar,20,entity.ModifiedUserID)
			};
			bool result;
			Database.RunProc("[knowdba].KnowledgeType_Create",parameters,out result); 
			return result;
		}


		public int UpdateByID(XBase.Model.KnowledgeCenter.KnowledgeType entity)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,entity.ID),
				SqlParameterHelper.MakeInParam("@Flag",SqlDbType.Char,1,entity.Flag),
				SqlParameterHelper.MakeInParam("@TypeName",SqlDbType.VarChar,100,entity.TypeName),
				SqlParameterHelper.MakeInParam("@SupperTypeID",SqlDbType.Int,0,entity.SupperTypeID),
				SqlParameterHelper.MakeInParam("@ModifiedDate",SqlDbType.DateTime,0,entity.ModifiedDate),
				SqlParameterHelper.MakeInParam("@ModifiedUserID",SqlDbType.VarChar,20,entity.ModifiedUserID)
			};
			int result;
			Database.RunProc("[knowdba].KnowledgeType_UpdateByID",parameters,out result); 
			return result;
		}


		public int DeleteByID(int iD)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,iD)
			};
			int result;
			Database.RunProc("[knowdba].KnowledgeType_DeleteByID",parameters,out result); 
			return result;
		}


		public XBase.Model.KnowledgeCenter.KnowledgeType GetEntityByID(int iD)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,iD)
			};
			XBase.Model.KnowledgeCenter.KnowledgeType entity = null ;
			SqlDataReader dr;
			Database.RunProc("[knowdba].KnowledgeType_GetEntityByID",parameters,out dr); 
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
			Database.RunProc("[knowdba].KnowledgeType_Select",parameters,out ds); 
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
			Database.RunProc("[knowdba].KnowledgeType_SelectWhereOrderedEx",parameters,out ds); 
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
			Database.RunProc("[knowdba].KnowledgeType_SelectWhereOrderedTopNEx",parameters,out ds); 
			return ds;
		}


		public int GetCount(string where)
		{
			SqlParameter[] parameters = new SqlParameter[]{
SqlParameterHelper.MakeInParam("@where",SqlDbType.VarChar,200,where),
SqlParameterHelper.MakeParam("@RecCount",SqlDbType.Int,0,ParameterDirection.Output,null)
			};
			Database.RunProc("[knowdba].KnowledgeType_GetCount",parameters); 
return Convert.ToInt32(parameters[parameters.Length - 1].Value);
		}


	}
}
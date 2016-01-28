using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace XBase.Data.KnowledgeCenter
{
	/// <summary>
	/// 数据访问
	/// </summary>
	public class SubscribeHistory
	{
		private XBase.Model.KnowledgeCenter.SubscribeHistory GetEntity(SqlDataReader dr)
		{
			XBase.Model.KnowledgeCenter.SubscribeHistory entity = new XBase.Model.KnowledgeCenter.SubscribeHistory();
			entity.ID = SqlClientUtility.GetInt32(dr,"ID",0);
			entity.CompanyCD = SqlClientUtility.GetString(dr,"CompanyCD",String.Empty);
			entity.Receiver = SqlClientUtility.GetString(dr,"Receiver",String.Empty);
			entity.KnowledgeID = SqlClientUtility.GetInt32(dr,"KnowledgeID",0);
			entity.SendDate = SqlClientUtility.GetDateTime(dr,"SendDate",DateTime.Now);
			entity.KeyWordID = SqlClientUtility.GetString(dr,"KeyWordID",String.Empty);
			entity.KeyWordName = SqlClientUtility.GetString(dr,"KeyWordName",String.Empty);
			return entity;
		}
		public bool Create(XBase.Model.KnowledgeCenter.SubscribeHistory entity)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@CompanyCD",SqlDbType.VarChar,8,entity.CompanyCD),
				SqlParameterHelper.MakeInParam("@Receiver",SqlDbType.VarChar,20,entity.Receiver),
				SqlParameterHelper.MakeInParam("@KnowledgeID",SqlDbType.Int,0,entity.KnowledgeID),
				SqlParameterHelper.MakeInParam("@SendDate",SqlDbType.DateTime,0,entity.SendDate),
				SqlParameterHelper.MakeInParam("@KeyWordID",SqlDbType.VarChar,50,entity.KeyWordID),
				SqlParameterHelper.MakeInParam("@KeyWordName",SqlDbType.VarChar,50,entity.KeyWordName)
			};
			bool result;
			Database.RunProc("[knowdba].SubscribeHistory_Create",parameters,out result); 
			return result;
		}


		public int UpdateByID(XBase.Model.KnowledgeCenter.SubscribeHistory entity)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,entity.ID),
				SqlParameterHelper.MakeInParam("@CompanyCD",SqlDbType.VarChar,8,entity.CompanyCD),
				SqlParameterHelper.MakeInParam("@Receiver",SqlDbType.VarChar,20,entity.Receiver),
				SqlParameterHelper.MakeInParam("@KnowledgeID",SqlDbType.Int,0,entity.KnowledgeID),
				SqlParameterHelper.MakeInParam("@SendDate",SqlDbType.DateTime,0,entity.SendDate),
				SqlParameterHelper.MakeInParam("@KeyWordID",SqlDbType.VarChar,50,entity.KeyWordID),
				SqlParameterHelper.MakeInParam("@KeyWordName",SqlDbType.VarChar,50,entity.KeyWordName)
			};
			int result;
			Database.RunProc("[knowdba].SubscribeHistory_UpdateByID",parameters,out result); 
			return result;
		}


		public int DeleteByID(int iD)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,iD)
			};
			int result;
			Database.RunProc("[knowdba].SubscribeHistory_DeleteByID",parameters,out result); 
			return result;
		}


		public XBase.Model.KnowledgeCenter.SubscribeHistory GetEntityByID(int iD)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,iD)
			};
			XBase.Model.KnowledgeCenter.SubscribeHistory entity = null ;
			SqlDataReader dr;
			Database.RunProc("[knowdba].SubscribeHistory_GetEntityByID",parameters,out dr); 
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
			Database.RunProc("[knowdba].SubscribeHistory_Select",parameters,out ds); 
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
			Database.RunProc("[knowdba].SubscribeHistory_SelectWhereOrderedEx",parameters,out ds); 
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
			Database.RunProc("[knowdba].SubscribeHistory_SelectWhereOrderedTopNEx",parameters,out ds); 
			return ds;
		}


		public int GetCount(string where)
		{
			SqlParameter[] parameters = new SqlParameter[]{
SqlParameterHelper.MakeInParam("@where",SqlDbType.VarChar,200,where),
SqlParameterHelper.MakeParam("@RecCount",SqlDbType.Int,0,ParameterDirection.Output,null)
			};
			Database.RunProc("[knowdba].SubscribeHistory_GetCount",parameters); 
return Convert.ToInt32(parameters[parameters.Length - 1].Value);
		}


	}
}
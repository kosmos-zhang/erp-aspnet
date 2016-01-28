using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace XBase.Data.KnowledgeCenter
{
	/// <summary>
	/// 数据访问
	/// </summary>
	public class MyCollector
	{
		private XBase.Model.KnowledgeCenter.MyCollector GetEntity(SqlDataReader dr)
		{
			XBase.Model.KnowledgeCenter.MyCollector entity = new XBase.Model.KnowledgeCenter.MyCollector();
			entity.ID = SqlClientUtility.GetInt32(dr,"ID",0);
			entity.Flag = SqlClientUtility.GetString(dr,"Flag",String.Empty);
			entity.KnowledgeID = SqlClientUtility.GetInt32(dr,"KnowledgeID",0);
			entity.Owner = SqlClientUtility.GetString(dr,"Owner",String.Empty);
			entity.SourceType = SqlClientUtility.GetString(dr,"SourceType",String.Empty);
			entity.CreateDate = SqlClientUtility.GetDateTime(dr,"CreateDate",DateTime.Now);
			entity.ReadStatus = SqlClientUtility.GetString(dr,"ReadStatus",String.Empty);
			entity.ReadDate = SqlClientUtility.GetDateTime(dr,"ReadDate",DateTime.Now);
			entity.ModifiedDate = SqlClientUtility.GetDateTime(dr,"ModifiedDate",DateTime.Now);
			entity.ModifiedUserID = SqlClientUtility.GetString(dr,"ModifiedUserID",String.Empty);
			return entity;
		}
		public bool Create(XBase.Model.KnowledgeCenter.MyCollector entity)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@Flag",SqlDbType.Char,1,entity.Flag),
				SqlParameterHelper.MakeInParam("@KnowledgeID",SqlDbType.Int,0,entity.KnowledgeID),
				SqlParameterHelper.MakeInParam("@Owner",SqlDbType.VarChar,20,entity.Owner),
				SqlParameterHelper.MakeInParam("@SourceType",SqlDbType.Char,1,entity.SourceType),
				SqlParameterHelper.MakeInParam("@CreateDate",SqlDbType.DateTime,0,entity.CreateDate),
				SqlParameterHelper.MakeInParam("@ReadStatus",SqlDbType.Char,1,entity.ReadStatus),
				SqlParameterHelper.MakeInParam("@ReadDate",SqlDbType.DateTime,0,entity.ReadDate),
				SqlParameterHelper.MakeInParam("@ModifiedDate",SqlDbType.DateTime,0,entity.ModifiedDate),
				SqlParameterHelper.MakeInParam("@ModifiedUserID",SqlDbType.VarChar,20,entity.ModifiedUserID)
			};
			bool result;
			Database.RunProc("[knowdba].MyCollector_Create",parameters,out result); 
			return result;
		}


		public int UpdateByID(XBase.Model.KnowledgeCenter.MyCollector entity)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,entity.ID),
				SqlParameterHelper.MakeInParam("@Flag",SqlDbType.Char,1,entity.Flag),
				SqlParameterHelper.MakeInParam("@KnowledgeID",SqlDbType.Int,0,entity.KnowledgeID),
				SqlParameterHelper.MakeInParam("@Owner",SqlDbType.VarChar,20,entity.Owner),
				SqlParameterHelper.MakeInParam("@SourceType",SqlDbType.Char,1,entity.SourceType),
				SqlParameterHelper.MakeInParam("@CreateDate",SqlDbType.DateTime,0,entity.CreateDate),
				SqlParameterHelper.MakeInParam("@ReadStatus",SqlDbType.Char,1,entity.ReadStatus),
				SqlParameterHelper.MakeInParam("@ReadDate",SqlDbType.DateTime,0,entity.ReadDate),
				SqlParameterHelper.MakeInParam("@ModifiedDate",SqlDbType.DateTime,0,entity.ModifiedDate),
				SqlParameterHelper.MakeInParam("@ModifiedUserID",SqlDbType.VarChar,20,entity.ModifiedUserID)
			};
			int result;
			Database.RunProc("[knowdba].MyCollector_UpdateByID",parameters,out result); 
			return result;
		}


		public int DeleteByID(int iD)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,iD)
			};
			int result;
			Database.RunProc("[knowdba].MyCollector_DeleteByID",parameters,out result); 
			return result;
		}


		public XBase.Model.KnowledgeCenter.MyCollector GetEntityByID(int iD)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,iD)
			};
			XBase.Model.KnowledgeCenter.MyCollector entity = null ;
			SqlDataReader dr;
			Database.RunProc("[knowdba].MyCollector_GetEntityByID",parameters,out dr); 
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
			Database.RunProc("[knowdba].MyCollector_Select",parameters,out ds); 
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
			Database.RunProc("[knowdba].MyCollector_SelectWhereOrderedEx",parameters,out ds); 
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
			Database.RunProc("[knowdba].MyCollector_SelectWhereOrderedTopNEx",parameters,out ds); 
			return ds;
		}


		public int GetCount(string where)
		{
			SqlParameter[] parameters = new SqlParameter[]{
SqlParameterHelper.MakeInParam("@where",SqlDbType.VarChar,200,where),
SqlParameterHelper.MakeParam("@RecCount",SqlDbType.Int,0,ParameterDirection.Output,null)
			};
			Database.RunProc("[knowdba].MyCollector_GetCount",parameters); 
return Convert.ToInt32(parameters[parameters.Length - 1].Value);
		}


        /// <summary>
        /// 获取收藏夹的分页数据
        /// </summary>
        /// <param name="tb">输出的数据表</param>
        /// <param name="PageSize">页尺寸</param>
        /// <param name="PageIndex">页索引</param>
        /// <param name="queryCondition">查询条件</param>
        /// <param name="sortExp">排序表达式如： ID ASC</param>
        /// <param name="fieldList">查询的字段列表</param>
        /// <returns>记录总数</returns>
        public int GetPageData(out DataTable tb, int PageSize, int PageIndex, string queryCondition, string sortExp, string fieldList)
        {
            /*
                @fields varchar(512),
                @where varchar(200),
                @OrderExp varchar(50),
                @pageIndex int,
                @pageSize int,
                @RecsCount int output
             */
            SqlParameter[] prams = {									
									SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.NVarChar,0,sortExp),
									SqlParameterHelper.MakeInParam("@fields",SqlDbType.NVarChar,0,fieldList),
									SqlParameterHelper.MakeInParam("@where",SqlDbType.NVarChar,0,queryCondition),

									SqlParameterHelper.MakeInParam("@pageSize",SqlDbType.Int,0,PageSize),
									SqlParameterHelper.MakeInParam("@pageIndex",SqlDbType.Int,0,PageIndex),
									SqlParameterHelper.MakeParam("@RecsCount",SqlDbType.Int,0,ParameterDirection.Output,null)
									
								   };

            DataSet ds;
            Database.RunProc("[knowdba].MyCollector_GetPageData", prams, out ds);
            tb = ds.Tables[0];

            int recCount = Convert.ToInt32(prams[prams.Length - 1].Value);

            return recCount;
        }


	}
}
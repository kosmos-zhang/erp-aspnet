using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace XBase.Data.KnowledgeCenter
{
	/// <summary>
	/// 数据访问
	/// </summary>
	public class KnowledgeRequire
	{
		private XBase.Model.KnowledgeCenter.KnowledgeRequire GetEntity(SqlDataReader dr)
		{
			XBase.Model.KnowledgeCenter.KnowledgeRequire entity = new XBase.Model.KnowledgeCenter.KnowledgeRequire();
			entity.ID = SqlClientUtility.GetInt32(dr,"ID",0);
			entity.CompanyCD = SqlClientUtility.GetString(dr,"CompanyCD",String.Empty);
			entity.SendDate = SqlClientUtility.GetDateTime(dr,"SendDate",DateTime.Now);
			entity.SendUserID = SqlClientUtility.GetString(dr,"SendUserID",String.Empty);
			entity.Content = SqlClientUtility.GetString(dr,"Content",String.Empty);
			entity.FeeBackStatus = SqlClientUtility.GetString(dr,"FeeBackStatus",String.Empty);
			entity.FeeBackDate = SqlClientUtility.GetDateTime(dr,"FeeBackDate",DateTime.Now);
			entity.FeeBackUserID = SqlClientUtility.GetString(dr,"FeeBackUserID",String.Empty);
			return entity;
		}
		public bool Create(XBase.Model.KnowledgeCenter.KnowledgeRequire entity)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@CompanyCD",SqlDbType.VarChar,8,entity.CompanyCD),
				SqlParameterHelper.MakeInParam("@SendDate",SqlDbType.DateTime,0,entity.SendDate),
				SqlParameterHelper.MakeInParam("@SendUserID",SqlDbType.VarChar,20,entity.SendUserID),
				SqlParameterHelper.MakeInParam("@Content",SqlDbType.NVarChar,200,entity.Content),
				SqlParameterHelper.MakeInParam("@FeeBackStatus",SqlDbType.Char,1,entity.FeeBackStatus),
				SqlParameterHelper.MakeInParam("@FeeBackDate",SqlDbType.DateTime,0,entity.FeeBackDate),
				SqlParameterHelper.MakeInParam("@FeeBackUserID",SqlDbType.VarChar,20,entity.FeeBackUserID)
			};
			bool result;
			Database.RunProc("[knowdba].KnowledgeRequire_Create",parameters,out result); 
			return result;
		}


		public int UpdateByID(XBase.Model.KnowledgeCenter.KnowledgeRequire entity)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,entity.ID),
				SqlParameterHelper.MakeInParam("@CompanyCD",SqlDbType.VarChar,8,entity.CompanyCD),
				SqlParameterHelper.MakeInParam("@SendDate",SqlDbType.DateTime,0,entity.SendDate),
				SqlParameterHelper.MakeInParam("@SendUserID",SqlDbType.VarChar,20,entity.SendUserID),
				SqlParameterHelper.MakeInParam("@Content",SqlDbType.NVarChar,200,entity.Content),
				SqlParameterHelper.MakeInParam("@FeeBackStatus",SqlDbType.Char,1,entity.FeeBackStatus),
				SqlParameterHelper.MakeInParam("@FeeBackDate",SqlDbType.DateTime,0,entity.FeeBackDate),
				SqlParameterHelper.MakeInParam("@FeeBackUserID",SqlDbType.VarChar,20,entity.FeeBackUserID)
			};
			int result;
			Database.RunProc("[knowdba].KnowledgeRequire_UpdateByID",parameters,out result); 
			return result;
		}


		public int DeleteByID(int iD)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,iD)
			};
			int result;
			Database.RunProc("[knowdba].KnowledgeRequire_DeleteByID",parameters,out result); 
			return result;
		}


		public XBase.Model.KnowledgeCenter.KnowledgeRequire GetEntityByID(int iD)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,iD)
			};
			XBase.Model.KnowledgeCenter.KnowledgeRequire entity = null ;
			SqlDataReader dr;
			Database.RunProc("[knowdba].KnowledgeRequire_GetEntityByID",parameters,out dr); 
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
			Database.RunProc("[knowdba].KnowledgeRequire_Select",parameters,out ds); 
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
			Database.RunProc("[knowdba].KnowledgeRequire_SelectWhereOrderedEx",parameters,out ds); 
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
			Database.RunProc("[knowdba].KnowledgeRequire_SelectWhereOrderedTopNEx",parameters,out ds); 
			return ds;
		}


		public int GetCount(string where)
		{
			SqlParameter[] parameters = new SqlParameter[]{
SqlParameterHelper.MakeInParam("@where",SqlDbType.VarChar,200,where),
SqlParameterHelper.MakeParam("@RecCount",SqlDbType.Int,0,ParameterDirection.Output,null)
			};
			Database.RunProc("[knowdba].KnowledgeRequire_GetCount",parameters); 
return Convert.ToInt32(parameters[parameters.Length - 1].Value);
		}

        /// <summary>
        /// 获取知识库的分页数据
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
            Database.RunProc("[knowdba].KnowledgeRequire_GetPageData", prams, out ds);
            tb = ds.Tables[0];

            int recCount = Convert.ToInt32(prams[prams.Length - 1].Value);

            return recCount;
        }

	}
}
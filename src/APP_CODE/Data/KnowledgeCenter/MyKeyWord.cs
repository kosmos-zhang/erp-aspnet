using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace XBase.Data.KnowledgeCenter
{
	/// <summary>
	/// 数据访问
	/// </summary>
	public class MyKeyWord
	{
		private XBase.Model.KnowledgeCenter.MyKeyWord GetEntity(SqlDataReader dr)
		{
			XBase.Model.KnowledgeCenter.MyKeyWord entity = new XBase.Model.KnowledgeCenter.MyKeyWord();
			entity.ID = SqlClientUtility.GetInt32(dr,"ID",0);
			entity.CompanyCD = SqlClientUtility.GetString(dr,"CompanyCD",String.Empty);
			entity.TypeID = SqlClientUtility.GetInt32(dr,"TypeID",0);
			entity.KeyType = SqlClientUtility.GetString(dr,"KeyType",String.Empty);
			entity.KeyWordID = SqlClientUtility.GetInt32(dr,"KeyWordID",0);
			entity.KeyWordName = SqlClientUtility.GetString(dr,"KeyWordName",String.Empty);
			entity.Creator = SqlClientUtility.GetString(dr,"Creator",String.Empty);
			entity.CreateDate = SqlClientUtility.GetDateTime(dr,"CreateDate",DateTime.Now);
			entity.ModifiedDate = SqlClientUtility.GetDateTime(dr,"ModifiedDate",DateTime.Now);
			entity.ModifiedUserID = SqlClientUtility.GetString(dr,"ModifiedUserID",String.Empty);
			return entity;
		}
		public bool Create(XBase.Model.KnowledgeCenter.MyKeyWord entity)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@CompanyCD",SqlDbType.VarChar,8,entity.CompanyCD),
				SqlParameterHelper.MakeInParam("@TypeID",SqlDbType.Int,0,entity.TypeID),
				SqlParameterHelper.MakeInParam("@KeyType",SqlDbType.Char,1,entity.KeyType),
				SqlParameterHelper.MakeInParam("@KeyWordID",SqlDbType.Int,0,entity.KeyWordID),
				SqlParameterHelper.MakeInParam("@KeyWordName",SqlDbType.VarChar,100,entity.KeyWordName),
				SqlParameterHelper.MakeInParam("@Creator",SqlDbType.VarChar,20,entity.Creator),
				SqlParameterHelper.MakeInParam("@CreateDate",SqlDbType.DateTime,0,entity.CreateDate),
				SqlParameterHelper.MakeInParam("@ModifiedDate",SqlDbType.DateTime,0,entity.ModifiedDate),
				SqlParameterHelper.MakeInParam("@ModifiedUserID",SqlDbType.VarChar,20,entity.ModifiedUserID)
			};
			bool result;
			Database.RunProc("[knowdba].MyKeyWord_Create",parameters,out result); 
			return result;
		}


		public int UpdateByID(XBase.Model.KnowledgeCenter.MyKeyWord entity)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,entity.ID),
				SqlParameterHelper.MakeInParam("@CompanyCD",SqlDbType.VarChar,8,entity.CompanyCD),
				SqlParameterHelper.MakeInParam("@TypeID",SqlDbType.Int,0,entity.TypeID),
				SqlParameterHelper.MakeInParam("@KeyType",SqlDbType.Char,1,entity.KeyType),
				SqlParameterHelper.MakeInParam("@KeyWordID",SqlDbType.Int,0,entity.KeyWordID),
				SqlParameterHelper.MakeInParam("@KeyWordName",SqlDbType.VarChar,100,entity.KeyWordName),
				SqlParameterHelper.MakeInParam("@Creator",SqlDbType.VarChar,20,entity.Creator),
				SqlParameterHelper.MakeInParam("@CreateDate",SqlDbType.DateTime,0,entity.CreateDate),
				SqlParameterHelper.MakeInParam("@ModifiedDate",SqlDbType.DateTime,0,entity.ModifiedDate),
				SqlParameterHelper.MakeInParam("@ModifiedUserID",SqlDbType.VarChar,20,entity.ModifiedUserID)
			};
			int result;
			Database.RunProc("[knowdba].MyKeyWord_UpdateByID",parameters,out result); 
			return result;
		}


		public int DeleteByID(int iD)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,iD)
			};
			int result;
			Database.RunProc("[knowdba].MyKeyWord_DeleteByID",parameters,out result); 
			return result;
		}


		public XBase.Model.KnowledgeCenter.MyKeyWord GetEntityByID(int iD)
		{
			SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,iD)
			};
			XBase.Model.KnowledgeCenter.MyKeyWord entity = null ;
			SqlDataReader dr;
			Database.RunProc("[knowdba].MyKeyWord_GetEntityByID",parameters,out dr); 
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
			Database.RunProc("[knowdba].MyKeyWord_Select",parameters,out ds); 
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
			Database.RunProc("[knowdba].MyKeyWord_SelectWhereOrderedEx",parameters,out ds); 
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
			Database.RunProc("[knowdba].MyKeyWord_SelectWhereOrderedTopNEx",parameters,out ds); 
			return ds;
		}


		public int GetCount(string where)
		{
			SqlParameter[] parameters = new SqlParameter[]{
SqlParameterHelper.MakeInParam("@where",SqlDbType.VarChar,200,where),
SqlParameterHelper.MakeParam("@RecCount",SqlDbType.Int,0,ParameterDirection.Output,null)
			};
			Database.RunProc("[knowdba].MyKeyWord_GetCount",parameters); 
return Convert.ToInt32(parameters[parameters.Length - 1].Value);
		}

        
        /// <summary>
        /// 最大关键字数
        /// </summary>
        /// <param name="companyCD">公司</param>
        /// <returns></returns>
        public int GetMaxKeywordCount(string companyCD)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                SqlParameterHelper.MakeInParam("@CompanyCD",SqlDbType.VarChar,200,companyCD)
			};
            string sql = "SELECT [MaxKeywords] FROM [pubdba].[companyOpenServ] WHERE [CompanyCD]=@CompanyCD";
            DataSet ds = Database.RunSql(sql, parameters);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return -1;
            }
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        
        /// <summary>
        /// 自定义的关键字最大数量
        /// </summary>
        /// <param name="companyCD">公司</param>
        /// <returns></returns>
        public int GetMaxUserKeywordCount(string companyCD)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                SqlParameterHelper.MakeInParam("@CompanyCD",SqlDbType.VarChar,200,companyCD)
			};
            string sql = "SELECT [MaxUserKeywords] FROM [pubdba].[companyOpenServ] WHERE [CompanyCD]=@CompanyCD";
            DataSet ds = Database.RunSql(sql, parameters);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return -1;
            }
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }


        /// <summary>
        /// GetCompanyOpenServ
        /// </summary>
        /// <param name="companyCD">公司</param>
        /// <returns></returns>
        public DataSet GetCompanyOpenServ(string companyCD)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                SqlParameterHelper.MakeInParam("@CompanyCD",SqlDbType.VarChar,200,companyCD)
			};
            string sql = "SELECT * FROM [pubdba].[companyOpenServ] WHERE [CompanyCD]=@CompanyCD";

            return Database.RunSql(sql, parameters);            
        }


        /// <summary>
        /// 获取分页数据
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
            Database.RunProc("[knowdba].MyKeyword_GetPageData", prams, out ds);
            tb = ds.Tables[0];

            int recCount = Convert.ToInt32(prams[prams.Length - 1].Value);

            return recCount;
        }
 
	}
}
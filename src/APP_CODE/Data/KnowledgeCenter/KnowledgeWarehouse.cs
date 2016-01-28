using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace XBase.Data.KnowledgeCenter
{
	/// <summary>
	/// 数据访问
	/// </summary>
	public class KnowledgeWarehouse
	{
        private XBase.Model.KnowledgeCenter.KnowledgeWarehouse GetEntity(SqlDataReader dr)
        {
            XBase.Model.KnowledgeCenter.KnowledgeWarehouse entity = new XBase.Model.KnowledgeCenter.KnowledgeWarehouse();
            entity.ID = SqlClientUtility.GetInt32(dr, "ID", 0);
            entity.KnowledgeNo = SqlClientUtility.GetString(dr, "KnowledgeNo", String.Empty);
            entity.Flag = SqlClientUtility.GetString(dr, "Flag", String.Empty);
            entity.TypeID = SqlClientUtility.GetInt32(dr, "TypeID", 0);
            entity.Title = SqlClientUtility.GetString(dr, "Title", String.Empty);
            entity.Keyword = SqlClientUtility.GetString(dr, "Keyword", String.Empty);
            entity.Content = SqlClientUtility.GetString(dr, "Content", String.Empty);
            entity.IsShow = SqlClientUtility.GetString(dr, "IsShow", String.Empty);
            entity.SourceFrom = SqlClientUtility.GetString(dr, "SourceFrom", String.Empty);
            entity.SafeLevel = SqlClientUtility.GetString(dr, "SafeLevel", String.Empty);
            entity.Author = SqlClientUtility.GetString(dr, "Author", String.Empty);
            entity.Attachment = SqlClientUtility.GetString(dr, "Attachment", String.Empty);
            entity.CreateUserID = SqlClientUtility.GetString(dr, "CreateUserID", String.Empty);
            entity.CreateDate = SqlClientUtility.GetDateTime(dr, "CreateDate", DateTime.Now);
            entity.ModifiedDate = SqlClientUtility.GetDateTime(dr, "ModifiedDate", DateTime.Now);
            entity.ModifiedUserID = SqlClientUtility.GetString(dr, "ModifiedUserID", String.Empty);
            entity.ReadTimes = SqlClientUtility.GetInt32(dr, "ReadTimes", 0);
            entity.DownloadTimes = SqlClientUtility.GetInt32(dr, "DownloadTimes", 0);
            return entity;
        }
        public bool Create(XBase.Model.KnowledgeCenter.KnowledgeWarehouse entity)
        {
            SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@KnowledgeNo",SqlDbType.VarChar,50,entity.KnowledgeNo),
				SqlParameterHelper.MakeInParam("@Flag",SqlDbType.Char,1,entity.Flag),
				SqlParameterHelper.MakeInParam("@TypeID",SqlDbType.Int,0,entity.TypeID),
				SqlParameterHelper.MakeInParam("@Title",SqlDbType.VarChar,100,entity.Title),
				SqlParameterHelper.MakeInParam("@Keyword",SqlDbType.VarChar,100,entity.Keyword),
				SqlParameterHelper.MakeInParam("@Content",SqlDbType.NText,0,entity.Content),
				SqlParameterHelper.MakeInParam("@IsShow",SqlDbType.Char,1,entity.IsShow),
				SqlParameterHelper.MakeInParam("@SourceFrom",SqlDbType.Char,1,entity.SourceFrom),
				SqlParameterHelper.MakeInParam("@SafeLevel",SqlDbType.Char,1,entity.SafeLevel),
				SqlParameterHelper.MakeInParam("@Author",SqlDbType.VarChar,50,entity.Author),
				SqlParameterHelper.MakeInParam("@Attachment",SqlDbType.VarChar,200,entity.Attachment),
				SqlParameterHelper.MakeInParam("@CreateUserID",SqlDbType.VarChar,20,entity.CreateUserID),
				SqlParameterHelper.MakeInParam("@CreateDate",SqlDbType.DateTime,0,entity.CreateDate),
				SqlParameterHelper.MakeInParam("@ModifiedDate",SqlDbType.DateTime,0,entity.ModifiedDate),
				SqlParameterHelper.MakeInParam("@ModifiedUserID",SqlDbType.VarChar,20,entity.ModifiedUserID),
				SqlParameterHelper.MakeInParam("@ReadTimes",SqlDbType.Int,0,entity.ReadTimes),
				SqlParameterHelper.MakeInParam("@DownloadTimes",SqlDbType.Int,0,entity.DownloadTimes)
			};
            bool result;
            Database.RunProc("[knowdba].KnowledgeWarehouse_Create", parameters, out result);
            return result;
        }


        public int UpdateByID(XBase.Model.KnowledgeCenter.KnowledgeWarehouse entity)
        {
            SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,entity.ID),
				SqlParameterHelper.MakeInParam("@KnowledgeNo",SqlDbType.VarChar,50,entity.KnowledgeNo),
				SqlParameterHelper.MakeInParam("@Flag",SqlDbType.Char,1,entity.Flag),
				SqlParameterHelper.MakeInParam("@TypeID",SqlDbType.Int,0,entity.TypeID),
				SqlParameterHelper.MakeInParam("@Title",SqlDbType.VarChar,100,entity.Title),
				SqlParameterHelper.MakeInParam("@Keyword",SqlDbType.VarChar,100,entity.Keyword),
				SqlParameterHelper.MakeInParam("@Content",SqlDbType.NText,0,entity.Content),
				SqlParameterHelper.MakeInParam("@IsShow",SqlDbType.Char,1,entity.IsShow),
				SqlParameterHelper.MakeInParam("@SourceFrom",SqlDbType.Char,1,entity.SourceFrom),
				SqlParameterHelper.MakeInParam("@SafeLevel",SqlDbType.Char,1,entity.SafeLevel),
				SqlParameterHelper.MakeInParam("@Author",SqlDbType.VarChar,50,entity.Author),
				SqlParameterHelper.MakeInParam("@Attachment",SqlDbType.VarChar,200,entity.Attachment),
				SqlParameterHelper.MakeInParam("@CreateUserID",SqlDbType.VarChar,20,entity.CreateUserID),
				SqlParameterHelper.MakeInParam("@CreateDate",SqlDbType.DateTime,0,entity.CreateDate),
				SqlParameterHelper.MakeInParam("@ModifiedDate",SqlDbType.DateTime,0,entity.ModifiedDate),
				SqlParameterHelper.MakeInParam("@ModifiedUserID",SqlDbType.VarChar,20,entity.ModifiedUserID),
				SqlParameterHelper.MakeInParam("@ReadTimes",SqlDbType.Int,0,entity.ReadTimes),
				SqlParameterHelper.MakeInParam("@DownloadTimes",SqlDbType.Int,0,entity.DownloadTimes) 
			};
            int result;
            Database.RunProc("[knowdba].KnowledgeWarehouse_UpdateByID", parameters, out result);
            return result;
        }


        public int DeleteByID(int iD)
        {
            SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,iD)
			};
            int result;
            Database.RunProc("[knowdba].KnowledgeWarehouse_DeleteByID", parameters, out result);
            return result;
        }


        public XBase.Model.KnowledgeCenter.KnowledgeWarehouse GetEntityByID(int iD)
        {
            SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,0,iD)
			};
            XBase.Model.KnowledgeCenter.KnowledgeWarehouse entity = null;
            SqlDataReader dr;
            Database.RunProc("[knowdba].KnowledgeWarehouse_GetEntityByID", parameters, out dr);
            if (dr.Read())
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
            Database.RunProc("[knowdba].KnowledgeWarehouse_Select", parameters, out ds);
            return ds;
        }


        public DataSet SelectWhereOrderedEx(string Where, string OrderExp, string Fields)
        {
            SqlParameter[] parameters = new SqlParameter[]{
SqlParameterHelper.MakeInParam("@Where",SqlDbType.VarChar,200,Where)
,SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.VarChar,100,OrderExp)
,SqlParameterHelper.MakeInParam("@Fields",SqlDbType.VarChar,300,Fields)
			};
            DataSet ds;
            Database.RunProc("[knowdba].KnowledgeWarehouse_SelectWhereOrderedEx", parameters, out ds);
            return ds;
        }


        public DataSet SelectWhereOrderedTopNEx(string Where, string OrderExp, int Num, string Fields)
        {
            SqlParameter[] parameters = new SqlParameter[]{
SqlParameterHelper.MakeInParam("@Where",SqlDbType.VarChar,200,Where)
,SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.VarChar,100,OrderExp)
,SqlParameterHelper.MakeInParam("@Num",SqlDbType.Int,0,Num)
,SqlParameterHelper.MakeInParam("@Fields",SqlDbType.VarChar,300,Fields)
			};
            DataSet ds;
            Database.RunProc("[knowdba].KnowledgeWarehouse_SelectWhereOrderedTopNEx", parameters, out ds);
            return ds;
        }


        public int GetCount(string where)
        {
            SqlParameter[] parameters = new SqlParameter[]{
SqlParameterHelper.MakeInParam("@where",SqlDbType.VarChar,200,where),
SqlParameterHelper.MakeParam("@RecCount",SqlDbType.Int,0,ParameterDirection.Output,null)
			};
            Database.RunProc("[knowdba].KnowledgeWarehouse_GetCount", parameters);
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
            Database.RunProc("[knowdba].KnowledgeWarehouse_GetPageData", prams, out ds);
            tb = ds.Tables[0];

            int recCount = Convert.ToInt32(prams[prams.Length - 1].Value);

            return recCount;
        }




        /// <summary>
        /// 获取知识库的分页数据2
        /// </summary>
        /// <param name="tb">输出的数据表</param>
        /// <param name="PageSize">页尺寸</param>
        /// <param name="PageIndex">页索引</param>
        /// <param name="queryCondition">查询条件</param>
        /// <param name="sortExp">排序表达式如： ID ASC</param>
        /// <param name="fieldList">查询的字段列表</param>
        /// <returns>记录总数</returns>
        public int GetPageData(out DataTable tb, int PageSize, int PageIndex, string queryCondition, string sortExp, string fieldList,int maxCount)
        {
            SqlParameter[] prams = {									
									SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.NVarChar,0,sortExp),
									SqlParameterHelper.MakeInParam("@fields",SqlDbType.NVarChar,0,fieldList),
									SqlParameterHelper.MakeInParam("@where",SqlDbType.NVarChar,0,queryCondition),

									SqlParameterHelper.MakeInParam("@pageSize",SqlDbType.Int,0,PageSize),
									SqlParameterHelper.MakeInParam("@pageIndex",SqlDbType.Int,0,PageIndex),
                                    SqlParameterHelper.MakeInParam("@maxCount",SqlDbType.Int,0,maxCount),
									SqlParameterHelper.MakeParam("@RecsCount",SqlDbType.Int,0,ParameterDirection.Output,null)
									
								   };

            DataSet ds;
            Database.RunProc("[knowdba].KnowledgeWarehouse_GetSearchPageData", prams, out ds);
            tb = ds.Tables[0];

            int recCount = Convert.ToInt32(prams[prams.Length - 1].Value);
            return recCount;
        }


        public void SubscribeSend(DateTime LastEndDate)
        {
            SqlParameter[] prams = {									
				SqlParameterHelper.MakeInParam("@LastEndDate",SqlDbType.DateTime,0,LastEndDate)
                };

            Database.RunProc("[knowdba].SubscribeSend", prams);
        }

	}
}
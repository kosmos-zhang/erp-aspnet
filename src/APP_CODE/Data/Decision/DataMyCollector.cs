using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace XBase.Data.Decision
{
    public class DataMyCollector
    {

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="cond">查询条件,多个条件应该以And连接</param>
        /// <param name="orderExp">展现顺序</param>
        /// <returns>List</returns>
        public IList<XBase.Model.Decision.DataMyCollector> GetDataMyCollectorListbyCond(string cond, string orderExp)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                SqlParameterHelper.MakeInParam("@Where",SqlDbType.VarChar,100,cond),
                SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.VarChar,50,orderExp)
			};
            XBase.Model.Decision.DataMyCollector entity = null;
            List<XBase.Model.Decision.DataMyCollector> lst = new List<XBase.Model.Decision.DataMyCollector>();
            SqlDataReader dr;
            Database.RunProc("statdba.DataMyCollector_SelectbyCond", parameters, out dr);
            while (dr.Read())
            {
                entity = GetList(dr);
                lst.Add(entity);
            }
            return lst;
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="cond">查询条件,多个条件应该以And连接</param>
        /// <param name="orderExp">展现顺序</param>
        /// <returns>List</returns>
        public DataSet GetDataMyCollectorByWhere(string cond, string orderExp)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                SqlParameterHelper.MakeInParam("@Where",SqlDbType.VarChar,100,cond),
                SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.VarChar,50,orderExp)
			};
            List<XBase.Model.Decision.DataMyCollector> lst = new List<XBase.Model.Decision.DataMyCollector>();
            DataSet dr=null;
            Database.RunProc("statdba.DataMyCollector_SelectbyCond", parameters, out dr);
            
            return dr;
        }

        #region [私有函数]
        /// <summary>
        /// 添加一个实体类
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private XBase.Model.Decision.DataMyCollector GetList(SqlDataReader dr)
        {
            XBase.Model.Decision.DataMyCollector entity = new XBase.Model.Decision.DataMyCollector();
            entity.ID = SqlClientUtility.GetInt32(dr, "ID", 0);
            entity.KeyWordID = SqlClientUtility.GetInt32(dr, "KeyWordID", 0);
            entity.ActionID = SqlClientUtility.GetInt32(dr, "ActionID", 0);
            entity.ActionDetailID = SqlClientUtility.GetInt32(dr, "ActionDetailID", 0);
            entity.Frequency = SqlClientUtility.GetInt32(dr, "Frequency", 0);
            entity.CompanyCD = SqlClientUtility.GetString(dr, "CompanyCD", String.Empty);
            entity.Flag = SqlClientUtility.GetString(dr, "Flag", String.Empty);
            entity.Owner = SqlClientUtility.GetString(dr, "Owner", String.Empty);
            entity.ReportDate = SqlClientUtility.GetDateTime(dr, "ReportDate", DateTime.Now);
            entity.ReadStatus = SqlClientUtility.GetString(dr, "ReadStatus", String.Empty);
            entity.ReadDate = SqlClientUtility.GetDateTime(dr, "ReadDate", DateTime.Now);
            entity.ReportTxt = SqlClientUtility.GetString(dr, "ReportTxt", String.Empty);
            return entity;
        }
        #endregion
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddDataMyCollector(XBase.Model.Decision.DataMyCollector entity)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                SqlParameterHelper.MakeInParam("@KeyWordID",SqlDbType.Int,4,entity.KeyWordID),
                SqlParameterHelper.MakeInParam("@ActionID",SqlDbType.Int,4,entity.ActionID),
                SqlParameterHelper.MakeInParam("@ActionDetailID",SqlDbType.Int,4,entity.ActionDetailID),
                SqlParameterHelper.MakeInParam("@Frequency",SqlDbType.Int,4,entity.Frequency),
                SqlParameterHelper.MakeInParam("@CompanyCD",SqlDbType.VarChar,8,entity.CompanyCD),
				SqlParameterHelper.MakeInParam("@Flag",SqlDbType.Char,1,entity.Flag),
				SqlParameterHelper.MakeInParam("@ReportDate",SqlDbType.DateTime,8,entity.ReportDate),
				SqlParameterHelper.MakeInParam("@Owner",SqlDbType.VarChar,20,entity.Owner),
				SqlParameterHelper.MakeInParam("@ReportTxt",SqlDbType.NVarChar,200,entity.ReportTxt),
                SqlParameterHelper.MakeInParam("@ReadStatus",SqlDbType.Char,1,entity.ReadStatus)
			};
            bool ret;
            Database.RunProc("[statdba].DataMyCollector_Add", parameters, out ret);
            return ret;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelDataMyCollector(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,4,id)
             };
            bool ret;
            Database.RunProc("[statdba].DataMyCollector_Del", parameters, out ret);
            return ret;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ModDataMyCollector(XBase.Model.Decision.DataMyCollector entity)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,4,entity.ID),
				SqlParameterHelper.MakeInParam("@Flag",SqlDbType.Char,1,entity.Flag),
                SqlParameterHelper.MakeInParam("@ReadStatus",SqlDbType.Char,1,entity.ReadStatus),
                SqlParameterHelper.MakeInParam("@ReadDate",SqlDbType.DateTime,8,entity.ReadDate)
			};
            bool ret;
            Database.RunProc("[statdba].DataMyCollector_Mod", parameters, out ret);
            return ret;
        }


        public void ModRead(int Id) 
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update statdba.DataMyCollector set ReadStatus=1,ReadDate=getdate() where [ID]=@ID");
            SqlParameter[] parameters = new SqlParameter[]{
                SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,4,Id)
			};
            Database.RunSql(sql.ToString(),parameters);
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
            Database.RunProc("[statdba].DataMyCollector_GetPageData", prams, out ds);
            tb = ds.Tables[0];

            int recCount = Convert.ToInt32(prams[prams.Length - 1].Value);

            return recCount;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace XBase.Data.Decision
{
    public class DataStat
    {
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="cond">查询条件,多个条件应该以And连接</param>
        /// <param name="orderExp">展现顺序</param>
        /// <returns>List</returns>
        public IList<XBase.Model.Decision.DataStat> GetDataStatListbyCond(string cond, string orderExp)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                SqlParameterHelper.MakeInParam("@Where",SqlDbType.VarChar,100,cond),
                SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.VarChar,50,orderExp)
			};
            XBase.Model.Decision.DataStat entity = null;
            List<XBase.Model.Decision.DataStat> lst = new List<XBase.Model.Decision.DataStat>();
            SqlDataReader dr;
            Database.RunProc("statdba.DataStat_SelectbyCond", parameters, out dr);
            while (dr.Read())
            {
                entity = GetList(dr);
                lst.Add(entity);
            }
            return lst;
        }


        /// <summary>
        /// 获取数据搜索的分页数据
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
            Database.RunProc("[statdba].[datastat_GetPageData]", prams, out ds);
            tb = ds.Tables[0];

            int recCount = Convert.ToInt32(prams[prams.Length - 1].Value);

            return recCount;
        }


        #region [私有函数]
        /// <summary>
        /// 添加一个实体类
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private XBase.Model.Decision.DataStat GetList(SqlDataReader dr)
        {
            XBase.Model.Decision.DataStat entity = new XBase.Model.Decision.DataStat();
            entity.ID = SqlClientUtility.GetInt32(dr, "ID", 0);
            entity.CompanyCD = SqlClientUtility.GetString(dr, "CompanyCD", String.Empty);
            entity.DataID = SqlClientUtility.GetInt32(dr, "DataID", 0);
            entity.DataName = SqlClientUtility.GetString(dr, "DataName", String.Empty);
            entity.DataVarValue = SqlClientUtility.GetString(dr, "DataVarValue", String.Empty);
            entity.DataNum = SqlClientUtility.GetDecimal(dr, "DataNum",Convert.ToDecimal("0.00"));
            entity.StatType = SqlClientUtility.GetString(dr, "StatType", String.Empty);
            entity.DataDateim = SqlClientUtility.GetDateTime(dr, "DataDateim", DateTime.Now);
            return entity;
        }
        #endregion
    }
}

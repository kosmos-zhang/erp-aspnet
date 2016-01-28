using System;
using System.Collections;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using XBase.Data.DBHelper;

namespace XBase.Data.Common
{
    /// <summary>
    /// 游德春 @2009.5.25
    /// </summary>
    public class PagerDBHelper
    {
       
        /// <summary>
        /// GetPageData
        /// </summary>
        /// <param name="table">表，如："a"</param>
        /// <param name="keyField">主见，如： "a.fd1"</param>
        /// <param name="fields">要查询的字段列表， 如： "a.fd1,b.fd1,c.fd1"</param>
        /// <param name="orderExp">排序表达式，如： "a.fd1 desc"</param>
        /// <param name="where">条件表达式，如："a.fd1='x' and b.fd1='x' and c.fd1'x'"</param>
        /// <param name="otherjoin">关联表达式，如： "leftjoin b on a.fd1=b.fd1 inner join c on a.fd1 = c.fd1"</param>
        /// <returns></returns>
        public static int GetPageData(out DataTable dt, string table, string keyField, string fields, string orderExp, string where,  string otherjoin,int pagesize,int pageindex)
        {
            if (where.Trim() + "" == "")
            {
                where = "1=1";
            }
            if (orderExp.Trim() + "" == "")
            {
                orderExp = keyField + " asc";
            }

            SqlParameter[] prams = {
                    SqlParameterHelper.MakeInParam("@table",SqlDbType.NVarChar,0,table),
                    SqlParameterHelper.MakeInParam("@keyfield",SqlDbType.NVarChar,0,keyField),
                    SqlParameterHelper.MakeInParam("@orderExp",SqlDbType.NVarChar,0,orderExp),
                    SqlParameterHelper.MakeInParam("@fields",SqlDbType.NVarChar,0,fields),
                    SqlParameterHelper.MakeInParam("@where",SqlDbType.NVarChar,0,where),
                    SqlParameterHelper.MakeInParam("@otherjoin",SqlDbType.NVarChar,0,otherjoin),
                    SqlParameterHelper.MakeInParam("@pageSize",SqlDbType.Int,0,pagesize),
                    SqlParameterHelper.MakeInParam("@pageIndex",SqlDbType.Int,0,pageindex),
                    SqlParameterHelper.MakeParam("@RecsCount",SqlDbType.Int,0,ParameterDirection.Output,null)									
			};

            DataSet ds = SqlHelper.ExecuteDataset("", "[dbo].GetPageDataEx", prams);           
            dt = ds.Tables[0];
            return Convert.ToInt32(prams[prams.Length - 1].Value);

        }

    }
















    public abstract class SqlParameterHelper
    {
        // Hashtable to store cached parameters
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        #region SqlParameter ����
        /// <summary>
        /// Make input param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>
        public static SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }

        /// <summary>
        /// Make input param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <returns>New parameter.</returns>
        public static SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input);
        }

        /// <summary>
        /// Make input param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <returns>New parameter.</returns>
        public static SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }

        /// <summary>
        /// Make stored procedure param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <param name="Direction">Parm direction.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>
        public static SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size,
            ParameterDirection Direction, object Value)
        {
            SqlParameter param;

            if (Size > 0)
                param = new SqlParameter(ParamName, DbType, Size);
            else
                param = new SqlParameter(ParamName, DbType);

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;

            return param;
        }

        /// <summary>
        /// Make stored procedure param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <param name="Direction">Parm direction.</param>
        /// <returns>New parameter.</returns>
        public static SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size,
            ParameterDirection Direction)
        {
            SqlParameter param;

            if (Size > 0)
                param = new SqlParameter(ParamName, DbType, Size);
            else
                param = new SqlParameter(ParamName, DbType);

            param.Direction = Direction;

            return param;
        }
        #endregion

        #region SqlParameter Cache����
        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] cmdParms)
        {
            parmCache[cacheKey] = cmdParms;
        }


        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }
        #endregion
    }
}

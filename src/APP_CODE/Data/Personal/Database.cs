using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

//using Microsoft.ApplicationBlocks.Data;
using XBase.Data.DBHelper;

namespace XBase.Data.Personal
{
    /// <summary>
    /// ADO.NET data access using the SQL Server Managed Provider.
    /// </summary>
    public static class Database
    {
        //private static string _connectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToString();
        private static string _connectionString = string.Empty;


        /// <summary>
        /// Run stored procedure.
        /// </summary>
        /// <param name="procName">Name of stored procedure.</param>
        /// <returns>Stored procedure return value.</returns>
        public static int RunProc(string procName)
        {
            return SqlHelper.ExecuteNonQuery(_connectionString, procName);

        }

        /// <summary>
        /// Run stored procedure.
        /// </summary>
        /// <param name="procName">Name of stored procedure.</param>
        /// <param name="prams">Stored procedure params.</param>
        /// <returns>Stored procedure return value.</returns>
        public static int RunProc(string procName, SqlParameter[] prams)
        {
            //prams = CloneParameters(prams);
            return SqlHelper.ExecuteNonQuery(_connectionString, procName, prams);
        }

        public static void RunProc(string procName, SqlParameter[] prams, out int rowsEffect)
        {
            //prams = CloneParameters(prams);
            rowsEffect = SqlHelper.ExecuteNonQuery(_connectionString, procName, prams);
        }

        public static void RunProc(string procName, SqlParameter[] prams, out bool isSuc)
        {

            //prams = CloneParameters(prams);
            isSuc = SqlHelper.ExecuteNonQuery(_connectionString, procName, prams) > 0 ? true : false;
        }

        /// <summary>
        /// Run stored procedure.
        /// </summary>
        /// <param name="procName">Name of stored procedure.</param>
        /// <param name="dataReader">Return result of procedure.</param>
        public static void RunProc(string procName, out SqlDataReader dataReader)
        {

            dataReader = SqlHelper.ExecuteReader(_connectionString, procName);

        }

        /// <summary>
        /// Run stored procedure.
        /// </summary>
        /// <param name="procName">Name of stored procedure.</param>
        /// <param name="prams">Stored procedure params.</param>
        /// <param name="dataReader">Return result of procedure.</param>
        public static void RunProc(string procName, SqlParameter[] prams, out SqlDataReader dataReader)
        {
            //prams = CloneParameters(prams);
            dataReader = SqlHelper.ExecuteReader(_connectionString, procName, prams);
            
        }

        public static void RunProc(string procName, SqlParameter[] prams, out DataSet ds)
        {
            //prams = CloneParameters(prams);
            ds = SqlHelper.ExecuteDataset(_connectionString, procName, prams);

        }

        public static DataSet RunSql(string sqlStatement, params SqlParameter[] parameterValues)
        {
            return SqlHelper.ExecuteSqlX(sqlStatement, parameterValues);
        }
        

        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            int plen = originalParameters.Length;

            SqlParameter[] clonedParameters = new SqlParameter[plen+1];

            for (int i = 0; i < plen; i++)
            {
                clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
            }

            clonedParameters[plen] = new SqlParameter ( "ReturnValue",
				SqlDbType.Int,4,ParameterDirection.ReturnValue,
				false,0,0,string.Empty,DataRowVersion.Default,null );


            return clonedParameters;
        }
                

        public static DataTable GetDataTable(string Sql, string ConnectString)
        {
            SqlDataAdapter da = new SqlDataAdapter(Sql, ConnectString);
            DataTable dt = new DataTable();

            int rows = da.Fill(dt);

            return dt;
        }
        


        public static int GetPageData(out DataTable tb,string tableName,string keyField, int PageSize, int PageIndex, string queryCondition, string sortExp, string fieldList)
        {
            /*
                @table nvarchar(50),
                @keyField nvarchar(50),
                @sortExp nvarchar(50),
                @fieldList nvarchar(255),
                @queryCondition nvarchar(255),
                @PageSize int,
                @PageIndex int,
                @recsCount int OUTPUT
            */
            SqlParameter[] prams = {
									SqlParameterHelper.MakeInParam("@table",SqlDbType.NVarChar,0,tableName),
									SqlParameterHelper.MakeInParam("@keyField",SqlDbType.NVarChar,0,keyField),

									SqlParameterHelper.MakeInParam("@sortExp",SqlDbType.NVarChar,0,sortExp),
									SqlParameterHelper.MakeInParam("@fieldList",SqlDbType.NVarChar,0,fieldList),
									SqlParameterHelper.MakeInParam("@queryCondition",SqlDbType.NVarChar,0,queryCondition),

									SqlParameterHelper.MakeInParam("@PageSize",SqlDbType.Int,0,PageSize),
									SqlParameterHelper.MakeInParam("@PageIndex",SqlDbType.Int,0,PageIndex),
									SqlParameterHelper.MakeParam("@recsCount",SqlDbType.Int,0,ParameterDirection.Output,null)
									
								   };

            DataSet ds;
            Database.RunProc("pageList", prams, out ds);
            tb = ds.Tables[0];

            int recCount = Convert.ToInt32(prams[prams.Length - 1].Value);

            return recCount;
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


    /// <summary>
    /// SqlClientUtility
    /// </summary>
    public class SqlClientUtility
    {
        private static object CheckValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            return value;
        }

        public static bool GetBoolean(DataRow dataRow, string columnName, bool valueIfNull)
        {
            object value = GetObject(dataRow, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is bool)
            {
                return (bool)value;
            }
            if (!(value is byte))
            {
                return bool.Parse(value.ToString());
            }
            if (((byte)value) == 0)
            {
                return false;
            }
            return true;
        }

        public static bool GetBoolean(SqlDataReader dataReader, string columnName, bool valueIfNull)
        {
            object value = GetObject(dataReader, columnName, valueIfNull);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is bool)
            {
                return (bool)value;
            }
            if (!(value is byte))
            {
                return bool.Parse(value.ToString());
            }
            if (((byte)value) == 0)
            {
                return false;
            }
            return true;
        }

        public static byte GetByte(DataRow dataRow, string columnName, byte valueIfNull)
        {
            object value = GetObject(dataRow, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is byte)
            {
                return (byte)value;
            }
            return byte.Parse(value.ToString());
        }

        public static byte GetByte(SqlDataReader dataReader, string columnName, byte valueIfNull)
        {
            object value = GetObject(dataReader, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is byte)
            {
                return (byte)value;
            }
            return byte.Parse(value.ToString());
        }

        public static byte[] GetBytes(DataRow dataRow, string columnName, byte[] valueIfNull)
        {
            object value = GetObject(dataRow, columnName, null);
            if ((value != null) && (value is byte[]))
            {
                return (byte[])value;
            }
            return valueIfNull;
        }

        public static byte[] GetBytes(SqlDataReader dataReader, string columnName, byte[] valueIfNull)
        {
            object value = GetObject(dataReader, columnName, null);
            if ((value != null) && (value is byte[]))
            {
                return (byte[])value;
            }
            return valueIfNull;
        }

        public static DataRow GetDataRow(DataSet dataSet)
        {
            return GetDataRow(GetDataTable(dataSet));
        }

        public static DataRow GetDataRow(DataTable dataTable)
        {
            if ((dataTable != null) && (dataTable.Rows.Count == 1))
            {
                return dataTable.Rows[0];
            }
            return null;
        }

        public static DataTable GetDataTable(DataSet dataSet)
        {
            if ((dataSet != null) && (dataSet.Tables.Count == 1))
            {
                return dataSet.Tables[0];
            }
            return null;
        }

        public static DateTime GetDateTime(DataRow dataRow, string columnName, DateTime valueIfNull)
        {
            object value = GetObject(dataRow, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is DateTime)
            {
                return (DateTime)value;
            }
            return DateTime.Parse(value.ToString());
        }

        public static DateTime GetDateTime(SqlDataReader dataReader, string columnName, DateTime valueIfNull)
        {
            object value = GetObject(dataReader, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is DateTime)
            {
                return (DateTime)value;
            }
            return DateTime.Parse(value.ToString());
        }

        public static decimal GetDecimal(DataRow dataRow, string columnName, decimal valueIfNull)
        {
            object value = GetObject(dataRow, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is decimal)
            {
                return (decimal)value;
            }
            return decimal.Parse(value.ToString());
        }

        public static decimal GetDecimal(SqlDataReader dataReader, string columnName, decimal valueIfNull)
        {
            object value = GetObject(dataReader, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is decimal)
            {
                return (decimal)value;
            }
            return decimal.Parse(value.ToString());
        }

        public static double GetDouble(DataRow dataRow, string columnName, double valueIfNull)
        {
            object value = GetObject(dataRow, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is double)
            {
                return (double)value;
            }
            return double.Parse(value.ToString());
        }

        public static double GetDouble(SqlDataReader dataReader, string columnName, double valueIfNull)
        {
            object value = GetObject(dataReader, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is double)
            {
                return (double)value;
            }
            return double.Parse(value.ToString());
        }

        public static Guid GetGuid(DataRow dataRow, string columnName, Guid valueIfNull)
        {
            object value = GetObject(dataRow, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is Guid)
            {
                return (Guid)value;
            }
            return new Guid(value.ToString());
        }

        public static Guid GetGuid(SqlDataReader dataReader, string columnName, Guid valueIfNull)
        {
            object value = GetObject(dataReader, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is Guid)
            {
                return (Guid)value;
            }
            return new Guid(value.ToString());
        }

        public static int GetInt32(DataRow dataRow, string columnName, int valueIfNull)
        {
            object value = GetObject(dataRow, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is int)
            {
                return (int)value;
            }
            return int.Parse(value.ToString());
        }

        public static int GetInt32(SqlDataReader dataReader, string columnName, int valueIfNull)
        {
            object value = GetObject(dataReader, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is int)
            {
                return (int)value;
            }
            return int.Parse(value.ToString());
        }

        public static long GetInt64(DataRow dataRow, string columnName, long valueIfNull)
        {
            object value = GetObject(dataRow, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is long)
            {
                return (long)value;
            }
            return long.Parse(value.ToString());
        }

        public static long GetInt64(SqlDataReader dataReader, string columnName, long valueIfNull)
        {
            object value = GetObject(dataReader, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is long)
            {
                return (long)value;
            }
            return long.Parse(value.ToString());
        }

        public static object GetObject(DataRow dataRow, string columnName, object valueIfNull)
        {
            object value = dataRow[columnName];
            if ((value != null) && (value != DBNull.Value))
            {
                return value;
            }
            return valueIfNull;
        }

        public static object GetObject(SqlDataReader dataReader, string columnName, object valueIfNull)
        {
            object value = dataReader[columnName];
            if ((value != null) && (value != DBNull.Value))
            {
                return value;
            }
            return valueIfNull;
        }

        public static float GetSingle(DataRow dataRow, string columnName, float valueIfNull)
        {
            object value = GetObject(dataRow, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is float)
            {
                return (float)value;
            }
            return float.Parse(value.ToString());
        }

        public static float GetSingle(SqlDataReader dataReader, string columnName, float valueIfNull)
        {
            object value = GetObject(dataReader, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is float)
            {
                return (float)value;
            }
            return float.Parse(value.ToString());
        }

        public static string GetString(DataRow dataRow, string columnName, string valueIfNull)
        {
            object value = GetObject(dataRow, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is string)
            {
                return (string)value;
            }
            return value.ToString();
        }

        public static string GetString(SqlDataReader dataReader, string columnName, string valueIfNull)
        {
            object value = GetObject(dataReader, columnName, null);
            if (value == null)
            {
                return valueIfNull;
            }
            if (value is string)
            {
                return (string)value;
            }
            return value.ToString();
        }

    }

}
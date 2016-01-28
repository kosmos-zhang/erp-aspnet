/**********************************************
 * 类作用：   数据库操作类
 * 创建人：   吴志强
 * 创建时间： 2008/12/30
 * 版本：     0.50
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using XBase.Common;

// Version：    0.5.0
// Author:      吴志强
// Date:        2008-12-30

namespace XBase.Data.DBHelper
{

    /// <summary>
    /// SQL 辅助类
    /// </summary>
    /// <example>
    /// 以下是使用 SqlHelper 的一个示例：
    /// <code>
    ///     SqlHelper.ExecuteSql( "Select * From MyTable" );    //执行 SQL 语句
    ///     repeater1.DataSource = SqlHelper.Result.DataTable;  //得到结果
    ///     repeater1.DataBind();
    /// </code>
    /// </example>
    /// <remarks>
    /// 
    /// SqlHelper 是一个对 .NET Sql 数据库操作的封装类，它使用外部配置文件指示连接字符串。
    /// 
    /// 在使用之前，应该先在 web.config (Web 项目) 或 app.config (Application 项目)中配置 SqlConnection 节，例如：
    ///     
    ///     <connectionStrings>
    ///             <add name="ConnectionString" connectionString="Data Source=.;Initial Catalog=devtest;persist security info=False;user id=sa;pwd=123456;" providerName="System.Data.SqlClient"/>
    ///     </connectionStrings>    
    /// 
    /// 要执行 SQL 存储过程请使用 Execute() 函数。
    /// 
    /// 若仅执行SQL语句则使用 ExecuteSql() 函数。
    /// 
    /// SqlHelper 提供了创建普通参数的便捷方式，使用 GetParameter() 能得到一个普通参数的实例，使用 GetReturnValueParameter() 函数
    /// 能得到一个返回值类型的参数，使用 GetOutputParameter() 函数能得到一个输出参数。
    /// 
    /// Result 是每次执行 Execute 或者 ExecuteSql() 函数的结果。这两个函数每执行一次，Result 都会被更新为最近一次执行的结果。
    /// 
    /// </remarks>
    public class SqlHelper
    {
        //数据库连接语句
        
        private static string _connectionString_inner = string.Empty;
        private static string _connectionString
        {

            
            get {
                if (_connectionString_inner == string.Empty)
                {
                    _connectionString_inner = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                    if (ConfigurationManager.AppSettings["enableEncryptConnectionString"] == "1")
                    {
                        _connectionString_inner = XBase.Common.SecurityUtil.DecryptDES(_connectionString_inner);
                    }
                }
                return _connectionString_inner;
            }

            set {
                _connectionString_inner = value;
            }
        }

        /*数据库连接语句*/
        public static string  _connectionStringStr
        {


            get
            {
                if (_connectionString_inner == string.Empty)
                {
                    _connectionString_inner = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                    if (ConfigurationManager.AppSettings["enableEncryptConnectionString"] == "1")
                    {
                        _connectionString_inner = XBase.Common.SecurityUtil.DecryptDES(_connectionString_inner);
                    }
                }
                return _connectionString_inner;
            }

            set
            {
                _connectionString_inner = value;
            }
        }

        //直接获得数据库连接串的方法
        public static String GetConnection()
        {
            return DecryptPassword(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        }

        /// <summary>
        /// 根据模块名称获得连接串的方法
        /// </summary>
        /// <param name="module">模块名称</param>
        /// <returns>返回相应连接串</returns>
        public static String GetConnection(string userDba)
        {
            switch (userDba)
            {
                case "officedba"://办公模式
                    return DecryptPassword(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                case "pubdba"://运营模式
                    return DecryptPassword(ConfigurationManager.ConnectionStrings["PubdbaConn"].ToString());
                case "statdba"://决策模式
                    return DecryptPassword(ConfigurationManager.ConnectionStrings["StatdbaConn"].ToString());
                case "konowdba"://知识中心
                    return DecryptPassword(ConfigurationManager.ConnectionStrings["KonowdbaConn"].ToString());
                case "infodba"://信息中心
                    return DecryptPassword(ConfigurationManager.ConnectionStrings["InfodbaConn"].ToString());
                default://默认办公模式
                    return DecryptPassword(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            }
        }

        //解密登录数据库密码
        public static String DecryptPassword(string connString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connString);
            string PassWord = SecurityUtil.DecryptDES(builder.Password);
            builder.Password = PassWord;
            return builder.ConnectionString;
        }
        //初始化返回结果
        private static Utility.Result _result = new XBase.Data.DBHelper.Utility.Result()
        {
            DataTable = new DataTable(),//数据集合
            ErrorMessage = string.Empty,//错误信息
            HasError = false,//有否错误标志
            Parameters = null,//参数集
            OprateCount = 0

        };

        public static DBHelper.Utility.Result Result
        {
            get { return _result; }
        }

        /// <summary>
        /// 创建普通参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static SqlParameter GetParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, value);
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static SqlParameter GetParameterFromString(string parameterName, string value)
        {
            if (string.IsNullOrEmpty(value))
                return new SqlParameter(parameterName, DBNull.Value);
            else
                return new SqlParameter(parameterName, value);
        }

        #region 带事务操作的方法,周军添加
        /// <summary>
        /// 准备一个可以执行的Sql命令对象。
        /// </summary>
        /// <param name="cmd">命令对象</param>
        /// <param name="conn">连接对象</param>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// 执行一个sql命令，仅仅返回数据库受影响行数。(用于需要事务的情况)
        /// 所需参数：事务对象，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>数据库受影响行数</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
          //  trans.Connection.Close();

            return val;
        }

        #endregion

        /// <summary>
        /// 创建返回值参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <returns>创建的参数对象</returns>
        public static SqlParameter GetReturnValueParameter(string parameterName, SqlDbType dbType)
        {
            return new SqlParameter(parameterName, dbType) { Direction = ParameterDirection.ReturnValue };
        }

        /// <summary>
        /// 创建输出参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">参数值类型</param>
        /// <returns>创建的参数对象</returns>
        public static SqlParameter GetOutputParameter(string parameterName, SqlDbType dbType)
        {
            return new SqlParameter(parameterName, dbType) { Direction = ParameterDirection.Output };
        }

        /// <summary>
        /// 获得前一次查询的结果集
        /// </summary>
        /// <returns>前一次查询的结果集DataTable</returns>
        public static DataTable GetPreData()
        {
            return _result.DataTable;
        }

        /// <summary>
        /// 执行存储过程(无输出参数)
        /// </summary>
        /// <param name="storedProcedureName">存储过程名</param>
        /// <param name="p">参数(允许0个或者0个以上)</param>
        public static DataTable ExecuteStoredProcedure(string storedProcedureName, params SqlParameter[] p)
        {
            //创建命令
            SqlCommand comm = new SqlCommand()
            {
                CommandText = storedProcedureName,
                CommandType = CommandType.StoredProcedure
            };
            return ExecuteSearch(comm, p);
        }

        /// <summary>
        /// 执行存储过程(无输出参数)
        /// </summary>
        /// <param name="storedProcedureName">存储过程名</param>
        /// <param name="paramList">SqlParameter参数list</param>
        public static DataTable ExecuteStoredProcedure(string storedProcedureName, ArrayList paramList)
        {
            //创建命令
            SqlCommand comm = new SqlCommand()
            {
                CommandText = storedProcedureName,
                CommandType = CommandType.StoredProcedure
            };

            SqlParameter[] p = null;
            if (paramList != null && paramList.Count > 0)
            {
                p = new SqlParameter[paramList.Count];
                for (int i = 0; i < paramList.Count; i++)
                {
                    p[i] = (SqlParameter)paramList[i];
                }
            }
            return ExecuteSearch(comm, p);
        }


        /// <summary>
        /// 执行Sql返回Dependency对象 added by 江贻明
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="Dependency">Dependency对象</param>
        /// <param name="parms">参数</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteSqlDependency(string sql,
            ref  System.Web.Caching.SqlCacheDependency Dependency, params SqlParameter[] parms)
        {
            SqlDependency.Start(_connectionString);
            //获得返回集实例
            _result = Utility.Result.GetInstance();
            //创建连接
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand comm = new SqlCommand();
                //设置参数
                if (parms != null && parms.Length > 0)
                {
                    foreach (SqlParameter item in parms)
                        comm.Parameters.Add(item);
                }
                comm.CommandText = sql;
                System.Web.Caching.SqlCacheDependency dependency =
                new System.Web.Caching.SqlCacheDependency(comm);
                Dependency = dependency;
                comm.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(comm);
                try
                {
                    //填充数据
                    da.Fill(_result.DataTable);
                    //设置查询参数
                    _result.Parameters = comm.Parameters;
                    //设置Error标志
                    _result.HasError = false;
                }
                catch (Exception e)
                {
                    //设置Error标志
                    _result.HasError = true;
                    //设置Error信息
                    _result.ErrorMessage = e.Message;
                    //设置查询信息为空DataTable
                    _result.DataTable = new DataTable();
                }
                finally
                {
                    //连接打开时，关闭连接
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
            return _result.DataTable;
        }

        /// <summary>
        /// 执行聚会函数 added by jiangym
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] p)
        {
            //获得返回集实例
            _result = Utility.Result.GetInstance();
            object obj = null;
            SqlCommand comm = new SqlCommand();
            //创建连接
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                //设置参数
                if (p != null && p.Length > 0)
                {
                    foreach (SqlParameter item in p)
                        comm.Parameters.Add(item);
                }
                comm.Connection = conn;
                //打开连接
                conn.Open();
                comm.CommandText = sql;
                obj = comm.ExecuteScalar();
                try
                {
                    //设置查询参数
                    _result.Parameters = comm.Parameters;
                    //设置Error标志
                    _result.HasError = false;
                }
                catch (Exception e)
                {
                    //设置Error标志
                    _result.HasError = true;
                    //设置Error信息
                    _result.ErrorMessage = e.Message;

                }
                finally
                {
                    //连接打开时，关闭连接
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }

               
            }
            return obj;
        }


        /// <summary>
        /// 执行 SQL 语句
        /// </summary>
        /// <param name="comm">执行的命令</param>
        /// <param name="p">参数集</param>
        /// <returns>查询的结果集</returns>
        private static DataTable ExecuteSearch(SqlCommand comm, params SqlParameter[] p)
        {
            //获得返回集实例
            //_result = Utility.Result.GetInstance();
            DataTable dt = new DataTable();
            //创建连接
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                //设置参数
                if (p != null && p.Length > 0)
                {
                    foreach (SqlParameter item in p)
                        comm.Parameters.Add(item);
                }
                comm.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(comm);
                //try
                //{
                    //填充数据
                da.Fill(dt);
                    //设置查询参数
                   // _result.Parameters = comm.Parameters;
                    //设置Error标志
                   // _result.HasError = false;
                //}
                //catch (Exception e)
                //{
                //    ////设置Error标志
                //    //_result.HasError = true;
                //    ////设置Error信息
                //    //_result.ErrorMessage = e.Message;
                //    ////设置查询信息为空DataTable
                //    //_result.DataTable = new DataTable();
                //    throw e;
                //}
                //finally
                //{
                //    //连接打开时，关闭连接
                //    if (conn != null)
                //    {
                //        conn.Close();
                //    }
                //}
                conn.Close();

            }
            return dt;
        }


        /// <summary>
        /// 执行 SQL 语句
        /// </summary>
        /// <param name="comm">执行的命令</param>
        /// <param name="p">参数集</param>
        /// <returns>查询的结果集</returns>
        public static DataTable ExecuteSearch(SqlCommand comm)
        {
            //获得返回集实例

            DataTable result = new DataTable();
            // _result = Utility.Result.GetInstance();
            //创建连接
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                comm.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(comm);
                //try
                //{
                    //填充数据
                    da.Fill(result);
                 
                //}
                //catch (Exception e)
               // {
                   
                //    throw e;
               // }
               // finally
               // {
                    //连接打开时，关闭连接
                //    if (conn != null)
                //    {
                 //       conn.Close();
                 //   }
                //}
                    conn.Close();
            }

            return result;
        }

        /// <summary>
        /// 执行 SQL 语句
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="paramList">SqlParameter参数集</param>
        /// <returns></returns>
        public static DataTable ExecuteSql(string sql, ArrayList paramList)
        {
            //创建命令
            SqlCommand comm = new SqlCommand()
            {
                CommandText = sql
            };

            SqlParameter[] p = null;
            if (paramList != null && paramList.Count > 0)
            {
                p = new SqlParameter[paramList.Count];
                for (int i = 0; i < paramList.Count; i++)
                {
                    p[i] = (SqlParameter)paramList[i];
                }
            }

            return ExecuteSearch(comm, p);
        }

        /// <summary>
        /// 执行 SQL 语句
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="p">参数集</param>
        /// <returns></returns>
        public static DataTable ExecuteSql(string sql, params SqlParameter[] p)
        {
            //创建命令
            SqlCommand comm = new SqlCommand()
            {
                CommandText = sql
            };

            return ExecuteSearch(comm, p);
        }

        /// <summary>
        /// 执行 SQL 语句
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="p">参数集</param>
        /// <returns></returns>
        public static DataTable ExecuteSql(string sql)
        {
            //创建命令
            SqlCommand comm = new SqlCommand()
            {
                CommandText = sql//SQL语句
            };
            return ExecuteSearch(comm, null);
        }

        /// <summary>
        /// 执行数据库操作
        /// </summary>
        /// <param name="comm">执行的命令</param>
        /// <param name="p">参数集</param>
        /// <returns>操作的记录数</returns>
        private static int ExecuteTrans(SqlCommand comm, params SqlParameter[] p)
        {
            //获得返回集实例
            _result = Utility.Result.GetInstance();
            //创建连接
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                comm.Parameters.Clear();
                //设置参数
                if (p != null && p.Length > 0)
                {
                    foreach (SqlParameter item in p)
                        comm.Parameters.Add(item);
                }
                //设置命令数据库连接
                comm.Connection = conn;
                //打开连接
                conn.Open();
                try
                {
                    //设置事务
                    comm.Transaction = conn.BeginTransaction();
                    //执行数据库操作
                    _result.OprateCount = comm.ExecuteNonQuery();
                    //提交事务
                    comm.Transaction.Commit();
                    //设置查询参数
                    _result.Parameters = comm.Parameters;
                    //设置Error标志
                    _result.HasError = false;
                }
                catch (Exception e)
                {
                    //如果开始了事务，则回滚事务
                    if (comm.Transaction != null)
                    {
                        comm.Transaction.Rollback();
                    }
                    //设置Error标志
                    _result.HasError = true;
                    //设置Error信息
                    _result.ErrorMessage = e.Message;
                    //设置影响记录数为0
                    _result.OprateCount = 0;
                    throw e;
                }
                finally
                {
                    //连接打开时，关闭连接
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }

            return _result.OprateCount;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcedureName">存储过程名</param>
        /// <param name="p">参数集</param>
        /// <returns>操作的记录数</returns>
        public static int ExecuteTransStoredProcedure(string storedProcedureName, params SqlParameter[] p)
        {
            //创建命令
            SqlCommand comm = new SqlCommand() { CommandText = storedProcedureName, CommandType = CommandType.StoredProcedure };

            return ExecuteTrans(comm, p);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcedureName">存储过程名</param>
        /// <param name="comm">命令</param>
        /// <param name="p">参数集</param>
        /// <returns>操作的记录数</returns>
        public static int ExecuteTransStoredProcedure(string storedProcedureName, SqlCommand comm, params SqlParameter[] p)
        {
            comm.CommandText = storedProcedureName;
            comm.CommandType = CommandType.StoredProcedure;

            return ExecuteTrans(comm, p);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcedureName">存储过程名</param>
        /// <param name="paramList">SqlParameter参数集</param>
        /// <returns>操作的记录数</returns>
        public static int ExecuteTransStoredProcedure(string storedProcedureName, ArrayList paramList)
        {
            //创建命令
            SqlCommand comm = new SqlCommand() { CommandText = storedProcedureName, CommandType = CommandType.StoredProcedure };

            SqlParameter[] p = null;
            if (paramList != null && paramList.Count > 0)
            {
                p = new SqlParameter[paramList.Count];
                for (int i = 0; i < paramList.Count; i++)
                {
                    p[i] = (SqlParameter)paramList[i];
                }
            }

            return ExecuteTrans(comm, p);
        }

        /// <summary>
        /// 执行 SQL 语句
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="p">参数集</param>
        /// <returns>操作的记录数</returns>
        public static int ExecuteTransSql(string sql, params SqlParameter[] p)
        {
            //创建命令
            SqlCommand comm = new SqlCommand()
            {
                CommandText = sql//SQL语句
            };
            return ExecuteTrans(comm, p);
        }

        /// <summary>
        /// 执行 SQL 语句
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="paramList">SqlParameter参数集</param>
        /// <returns>操作的记录数</returns>
        public static int ExecuteTransSql(string sql, ArrayList paramList)
        {
            //创建命令
            SqlCommand comm = new SqlCommand()
            {
                CommandText = sql//SQL语句
            };

            SqlParameter[] p = null;
            if (paramList != null && paramList.Count > 0)
            {
                p = new SqlParameter[paramList.Count];
                for (int i = 0; i < paramList.Count; i++)
                {
                    p[i] = (SqlParameter)paramList[i];
                }
            }

            return ExecuteTrans(comm, p);
        }

        /// <summary>
        /// 更新或者登陆多条数据
        /// </summary>
        /// <param name="sql">命令集</param>
        /// <returns>操作的记录数</returns>
        public static bool ExecuteTransForListWithSQL(string[] sql)
        {
            //获得返回集实例
            _result = Utility.Result.GetInstance();
            //创建连接
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    //打开连接
                    conn.Open();
                    //开始事务
                    transaction = conn.BeginTransaction();
                    int totalCount = 0;
                    //设置参数
                    if (sql != null && sql.Length > 0)
                    {
                        for (int i = 0; i < sql.Length; i++)
                        {
                            if (sql[i] != null && sql[i] != "")
                            {
                                SqlCommand comm = new SqlCommand(sql[i]);
                                //设置命令数据库连接
                                comm.Connection = conn;
                                comm.Transaction = transaction;
                                //执行数据库操作
                                totalCount += comm.ExecuteNonQuery();
                            }
                        }
                    }
                    //提交事务
                    transaction.Commit();
                    //设置Error标志
                    _result.HasError = false;
                    //设置影响记录数
                    _result.OprateCount = totalCount;
                }
                catch (Exception e)
                {
                    //如果开始了事务，则回滚事务
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    //设置Error标志
                    _result.HasError = true;
                    //设置Error信息
                    _result.ErrorMessage = e.Message;
                    //设置影响记录数为0
                    _result.OprateCount = 0;
                    //
                    throw e;
                }
                finally
                {
                    //连接打开时，关闭连接
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 更新或者登陆多条数据
        /// </summary>
        /// <param name="lstComm">命令集</param>
        /// <returns>操作的记录数</returns>
        public static bool ExecuteTransWithArrayList(ArrayList lstComm)
        {
            //获得返回集实例
            _result = Utility.Result.GetInstance();
            //创建连接
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    //打开连接
                    conn.Open();
                    //开始事务
                    transaction = conn.BeginTransaction();
                    int totalCount = 0;
                    int count = lstComm.Count;
                    //设置参数
                    if (lstComm != null && count > 0)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            SqlCommand comm = (SqlCommand)lstComm[i];
                            //设置命令数据库连接
                            comm.Connection = conn;
                            comm.Transaction = transaction;
                            //执行数据库操作
                            totalCount += comm.ExecuteNonQuery();
                        }
                    }
                    //提交事务
                    transaction.Commit();
                    //设置Error标志
                    _result.HasError = false;
                    _result.OprateCount = totalCount;
                }
                catch (Exception e)
                {
                    //如果开始了事务，则回滚事务
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    //设置Error标志
                    _result.HasError = true;
                    //设置Error信息
                    _result.ErrorMessage = e.Message;
                    //设置影响记录数为0
                    _result.OprateCount = 0;
                    throw e;
                }
                finally
                {
                    //连接打开时，关闭连接
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }

            return true;
        }



        /// <summary>
        /// 执行数据库操作
        /// </summary>
        /// <param name="comm">命令</param>
        /// <returns>操作是否成功</returns>
        public static bool ExecuteTransWithCommand(SqlCommand comm)
        {
            bool isSucc = false;
            //创建连接
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    //打开连接
                    conn.Open();
                    //开始事务
                    transaction = conn.BeginTransaction();
                    //设置命令数据库连接
                    comm.Connection = conn;
                    comm.Transaction = transaction;
                    //执行数据库操作
                    comm.ExecuteNonQuery();
                    //提交事务
                    transaction.Commit();

                    isSucc = true;
                }
                catch (Exception e)
                {
                    //如果开始了事务，则回滚事务
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    throw e;
                }
                finally
                {
                    //连接打开时，关闭连接
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }

            return isSucc;
        }

        /// <summary>
        /// 更新或者登陆多条数据
        /// </summary>
        /// <param name="comm">命令集</param>
        /// <returns>操作的记录数</returns>
        public static bool ExecuteTransForList(SqlCommand[] comms)
        {
            //获得返回集实例
            _result = Utility.Result.GetInstance();
            //创建连接
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    //打开连接
                    conn.Open();
                    //开始事务
                    transaction = conn.BeginTransaction();
                    //定义操作COUNT
                    int totalCount = 0;
                    //设置参数
                    if (comms != null && comms.Length > 0)
                    {
                        for (int i = 0; i < comms.Length; i++)
                        {
                            SqlCommand comm = comms[i];
                            //设置命令数据库连接
                            comm.Connection = conn;
                            comm.Transaction = transaction;
                            //执行数据库操作
                            totalCount += comm.ExecuteNonQuery();
                        }
                    }
                    //提交事务
                    transaction.Commit();
                    //设置Error标志
                    _result.HasError = false;
                    //设置影响记录数
                    _result.OprateCount = totalCount;
                }
                catch (Exception e)
                {
                    //如果开始了事务，则回滚事务
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    //设置Error标志
                    _result.HasError = true;
                    //设置Error信息
                    _result.ErrorMessage = e.Message;
                    //设置影响记录数为0
                    _result.OprateCount = 0;
                    //
                    throw e;
                }
                finally
                {
                    //连接打开时，关闭连接
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }

            return true;
        }


        /// <summary>
        /// 执行多条查询select语句,返回DataSet
        /// </summary>
        public static DataSet ExecuteForListWithSQL(string[] sql)
        {
            DataSet ds = new DataSet();
            //设置参数
            if (sql != null && sql.Length > 0)
            {
                for (int i = 0; i < sql.Length; i++)
                {
                    if (sql[i] != null && sql[i] != "")
                    {
                        ds.Tables.Add(SqlHelper.ExecuteSql(sql[i]));
                    }
                }
            }
            return ds;
        }

        private static Utility.IConnectAssist _connectAssist = new ConnectAssistTag();
        /// <summary>
        /// 连接辅助
        /// </summary>
        public static Utility.IConnectAssist ConnectAssist { get { return _connectAssist; } }

        /// <summary>
        /// 连接助手类，当需要临时更改数据库、密码、服务器、用户，则可以使用此类的成员对 SqlHelper 进行更改。
        /// </summary>
        /// <remarks>此类不保存设置，仅在程序运行过程中有效，不会自动去更新配置文件。</remarks>
        private class ConnectAssistTag : XBase.Data.DBHelper.Utility.IConnectAssist
        {
            #region IConnectAssist 成员

            private SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(SqlHelper._connectionString);
            /// <summary>
            /// 更改服务器
            /// </summary>
            /// <param name="server">服务器名称</param>
            public void ChangeServer(string server)
            {
                builder.DataSource = server;
                SubmitChange();
            }
            /// <summary>
            /// 更改数据库
            /// </summary>
            /// <param name="database">数据库</param>
            public void ChangeDatabase(string database)
            {
                builder.InitialCatalog = database;
                SubmitChange();
            }
            /// <summary>
            /// 更改用户
            /// </summary>
            /// <param name="user">用户名</param>
            public void ChangeUser(string user)
            {
                builder.UserID = user;
                SubmitChange();
            }
            /// <summary>
            /// 更改密码
            /// </summary>
            /// <param name="password">新密码</param>
            public void ChangePassword(string password)
            {
                builder.Password = password;
                SubmitChange();
            }
            /// <summary>
            /// 应用更改
            /// </summary>
            protected void SubmitChange()
            {
                SqlHelper._connectionString = builder.ConnectionString;
            }

            /// <summary>
            /// 重置为默认连接字符串(从配置文件中从新读取)
            /// </summary>
            public void ReSet()
            {
                SqlHelper._connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            }

            /// <summary>
            /// 重置为指定链接字符串
            /// </summary>
            /// <param name="connectionString"></param>
            public void ReSet(string connectionString)
            {
                SqlHelper._connectionString = connectionString;
            }

            #endregion
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static ArrayList SpecailExecuteList(SqlCommand comm, params SqlParameter[] p)
        {
            ArrayList dataList = new ArrayList();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                //设置参数
                if (p != null && p.Length > 0)
                {
                    foreach (SqlParameter item in p)
                        comm.Parameters.Add(item);
                }
                comm.Connection = conn;

                conn.Open();

                DateTime st1 = DateTime.Now;
                SqlDataReader sdr = comm.ExecuteReader(CommandBehavior.CloseConnection);
                DateTime st2 = DateTime.Now;

               // System.Web.HttpContext.Current.Response.Write(( (st2-st1).Seconds ).ToString() + "::<br>\n");

               // int i = 0;
                st1 = DateTime.Now;
                while (sdr.Read())
                {
                    object[] cols = new object[sdr.FieldCount];
                    sdr.GetValues(cols);

                    dataList.Add(cols);

                   // System.Web.HttpContext.Current.Response.Write((i++).ToString()+"<br>\n");
                }
                st2 = DateTime.Now;
                //System.Web.HttpContext.Current.Response.Write(((st2 - st1).Seconds).ToString() + "::<br>\n");

                sdr.Close();
                conn.Close();

            }
            return dataList;
        }




        #region 适配Microsoft.ApplicationBlocks.Data.SqlHelper
        /// <summary>
        /// 执行数据库操作
        /// </summary>
        /// <param name="comm">执行的命令</param>
        /// <param name="p">参数集</param>
        /// <returns>SqlDataReader</returns>
        private static SqlDataReader ExecuteSqlDataReader(SqlCommand comm, params SqlParameter[] p)
        {
            SqlDataReader dr = null;
            //创建连接
            SqlConnection conn = new SqlConnection(_connectionString);

            comm.Parameters.Clear();
            //设置参数
            if (p != null && p.Length > 0)
            {
                foreach (SqlParameter item in p)
                    comm.Parameters.Add(item);
            }

            comm.Connection = conn;
            conn.Open();
            try
            {
                dr = comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }



            return dr;
        }

        private static DataSet ExecuteDataSet(SqlCommand comm, params SqlParameter[] p)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                //设置参数
                if (p != null && p.Length > 0)
                {
                    foreach (SqlParameter item in p)
                        comm.Parameters.Add(item);
                }
                comm.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(comm);
                try
                {
                    //填充数据
                    da.Fill(ds);
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //连接打开时，关闭连接
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }

            return ds;


        }

        private static int ExecuteNonQuery(SqlCommand comm, params SqlParameter[] p)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                //设置参数
                if (p != null && p.Length > 0)
                {
                    foreach (SqlParameter item in p)
                        comm.Parameters.Add(item);
                }
                comm.Connection = conn;
                conn.Open();

                try
                {
                    return comm.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //连接打开时，关闭连接
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }

        }



        public static int ExecuteNonQuery(string connectionString, string spName)
        {
            //创建命令
            SqlCommand comm = new SqlCommand()
            {
                CommandText = spName,
                CommandType = CommandType.StoredProcedure
            };

            return ExecuteNonQuery(comm, null);

        }

        public static int ExecuteNonQuery(string connectionString, string spName, params SqlParameter[] parameterValues)
        {
            //创建命令
            SqlCommand comm = new SqlCommand()
            {
                CommandText = spName,
                CommandType = CommandType.StoredProcedure
            };

            return ExecuteNonQuery(comm, parameterValues);
        }


        public static SqlDataReader ExecuteReader(string connectionString, string spName)
        {
            //创建命令
            SqlCommand comm = new SqlCommand()
            {
                CommandText = spName,
                CommandType = CommandType.StoredProcedure
            };

            return ExecuteSqlDataReader(comm, null);
        }

        public static SqlDataReader ExecuteReader(string connectionString, string spName, params SqlParameter[] parameterValues)
        {
            //创建命令
            SqlCommand comm = new SqlCommand()
            {
                CommandText = spName,
                CommandType = CommandType.StoredProcedure
            };

            return ExecuteSqlDataReader(comm, parameterValues);
        }



        public static DataSet ExecuteDataset(string connectionString, string spName)
        {
            //创建命令
            SqlCommand comm = new SqlCommand()
            {
                CommandText = spName,
                CommandType = CommandType.StoredProcedure
            };

            return ExecuteDataSet(comm, null);
        }

        public static DataSet ExecuteDataset(string connectionString, string spName, params SqlParameter[] parameterValues)
        {
            //创建命令
            SqlCommand comm = new SqlCommand()
            {
                CommandText = spName,
                CommandType = CommandType.StoredProcedure
            };

            return ExecuteDataSet(comm, parameterValues);
        }



        public static DataSet ExecuteSqlX(string sqlStatement, params SqlParameter[] parameterValues)
        {
            //创建命令
            SqlCommand comm = new SqlCommand()
            {
                CommandText = sqlStatement,
                CommandType = CommandType.Text
            };

            return ExecuteDataSet(comm, parameterValues);
        }
        #endregion

        #region 判断记录是否存在
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null,CommandType.Text,SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }

        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 构造一个新的SqlCommond实例，并初始化
        public static SqlCommand GetNewSqlCommond(string Sql, SqlParameter[] Paras)
        {
            SqlCommand SqlCmd = new SqlCommand() { CommandText = Sql };
            SqlCmd.Parameters.AddRange(Paras);
            return SqlCmd;
        }
        #endregion

        #region 使用泛型作为SqlCommond 参数集
        /// <summary>
        /// 更新或者登陆多条数据
        /// </summary>
        /// <param name="lstComm">命令集</param>
        /// <returns>操作的记录数</returns>
        public static bool ExecuteTransWithCollections(List<SqlCommand> lstComm)
        {
            //获得返回集实例
            _result = Utility.Result.GetInstance();
            //创建连接
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    //打开连接
                    conn.Open();
                    //开始事务
                    transaction = conn.BeginTransaction();
                    int totalCount = 0;
                    int count = lstComm.Count;
                    //设置参数
                    if (lstComm != null && count > 0)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            SqlCommand comm = lstComm[i];
                            //设置命令数据库连接
                            comm.Connection = conn;
                            comm.Transaction = transaction;
                            //执行数据库操作
                            totalCount += comm.ExecuteNonQuery();
                        }
                    }
                    //提交事务
                    transaction.Commit();
                    //设置Error标志
                    _result.HasError = false;
                    _result.OprateCount = totalCount;
                }
                catch (Exception e)
                {
                    //如果开始了事务，则回滚事务
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    //设置Error标志
                    _result.HasError = true;
                    //设置Error信息
                    _result.ErrorMessage = e.Message;
                    //设置影响记录数为0
                    _result.OprateCount = 0;
                    throw e;
                }
                finally
                {
                    //连接打开时，关闭连接
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }

            return true;
        }

        #endregion

        #region 分页
        /// <summary>
        /// 通用分页
        /// </summary>
        /// <param name="Sql">构造完成的SQL字符串</param>
        /// <param name="PageIndex">页数</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="OrderBy">排序字段 比如 ： ID DESC </param> 
        /// <param name="Paras">参数集</param>
        /// <param name="TotalCount">返回总记录数</param>
        /// <returns>datatable</returns>
        public static DataTable CreateSqlByPageExcuteSql(string Sql,int PageIndex,int PageSize,string OrderBy,SqlParameter[] Paras,ref int TotalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            if (PageIndex == 1)
                sbSql.Append("SELECT TOP " + PageSize + " *  FROM");
            else
                sbSql.Append("SELECT * FROM ");
            sbSql.Append(" ( SELECT ROW_NUMBER() OVER (ORDER BY " + OrderBy + ") as RowNumber,tempTable.*");
            sbSql.Append(" FROM ( " + Sql + " ) AS  tempTable ) AS tmp ");

            if (PageIndex != 1)
                sbSql.Append("WHERE RowNumber BETWEEN CONVERT(varchar,(@PageIndex-1)*@PageSize+1) AND CONVERT(varchar,(@PageIndex-1)*@PageSize+@PageSize) ");

            sbSql.Append(" SELECT @TotalRecord=count(*) from (" + Sql + ") tempTable");

            /*重新构造SqlParameter*/
            int index = 0;
            int Length = 0;
            SqlParameter[] SqlParas;

            if (Paras != null && Paras.Length > 0)
            {
                Length = Paras.Length;
                SqlParas = new SqlParameter[Length + 3];
                for (int i = 0; i < Paras.Length; i++)
                {
                    SqlParas[i] = Paras[i];
                    index++;
                }
            }
            else
                SqlParas = new SqlParameter[Length + 3];
            

            
            /*将分页参数追加至SqlParameter*/
            SqlParas[index] = new SqlParameter("@PageIndex", SqlDbType.Int);
            SqlParas[index].Value = PageIndex;
            index++;
            SqlParas[index] = new SqlParameter("@PageSize", SqlDbType.Int);
            SqlParas[index].Value = PageSize;
            index++;
            SqlParas[index] = new SqlParameter("@TotalRecord", SqlDbType.Int);
            SqlParas[index].Direction = ParameterDirection.Output;
            DataTable dtTemp = ExecuteSql(sbSql.ToString(), SqlParas);
            TotalCount = (int)SqlParas[index].Value;
            return dtTemp;
        }

        /// <summary>
        /// 通用分页
        /// </summary>
        /// <param name="Sql">构造完成的SQL字符串</param>
        /// <param name="PageIndex">页数</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="OrderBy">排序字段 比如 ： ID DESC </param> 
        /// <param name="paramList">参数集</param>
        /// <param name="TotalCount">返回总记录数</param>
        /// <returns>datatable</returns>
        public static DataTable CreateSqlByPageExcuteSqlArr(string Sql, int PageIndex, int PageSize, string OrderBy, ArrayList paramList, ref int TotalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            if (PageIndex == 1)
                sbSql.Append("SELECT TOP " + PageSize + " *  FROM");
            else
                sbSql.Append("SELECT * FROM ");
            sbSql.Append(" ( SELECT ROW_NUMBER() OVER (ORDER BY " + OrderBy + ") as RowNumber,tempTable.*");
            sbSql.Append(" FROM ( " + Sql + " ) AS  tempTable ) AS tmp ");

            if (PageIndex != 1)
                sbSql.Append("WHERE RowNumber BETWEEN CONVERT(varchar,(@PageIndex-1)*@PageSize+1) AND CONVERT(varchar,(@PageIndex-1)*@PageSize+@PageSize) ");

            sbSql.Append(" SELECT @TotalRecord=count(*) from (" + Sql + ") tempTable");

            /*重新构造SqlParameter*/
            int index = 0;
            int Length = 0;
            SqlParameter[] SqlParas;

           
            if (paramList != null && paramList.Count > 0)
            {
                SqlParas = new SqlParameter[paramList.Count+3];
                for (int i = 0; i < paramList.Count; i++)
                {
                    SqlParas[i] = (SqlParameter)paramList[i];
                    index++;
                }
            }

           
            else
                SqlParas = new SqlParameter[Length + 3];



            /*将分页参数追加至SqlParameter*/
            SqlParas[index] = new SqlParameter("@PageIndex", SqlDbType.Int);
            SqlParas[index].Value = PageIndex;
            index++;
            SqlParas[index] = new SqlParameter("@PageSize", SqlDbType.Int);
            SqlParas[index].Value = PageSize;
            index++;
            SqlParas[index] = new SqlParameter("@TotalRecord", SqlDbType.Int);
            SqlParas[index].Direction = ParameterDirection.Output;
            DataTable dtTemp = ExecuteSql(sbSql.ToString(), SqlParas);
            TotalCount = (int)SqlParas[index].Value;
            return dtTemp;
        }

        #endregion
        /* Add by zqwu 2009-06-10 start */
        #region 分页WithCommand
        /// <summary>
        /// 分页WithCommand
        /// </summary>
        /// <param name="cmd">执行命令 该命令必须指定了SQL语句</param>
        /// <param name="PageIndex">页数</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="OrderBy">排序字段 比如 ： ID DESC </param> 
        /// <param name="TotalCount">返回总记录数</param>
        /// <returns>datatable</returns>
        public static DataTable PagerWithCommand(SqlCommand cmd, int PageIndex, int PageSize, string OrderBy, ref int TotalCount)
        {
            //变量定义
            StringBuilder sbSql = new StringBuilder();
            //第一页时
            if (PageIndex == 1)
                sbSql.Append("SELECT TOP " + PageSize + " *  FROM");
            else
                sbSql.Append("SELECT * FROM ");
            sbSql.Append(" ( SELECT ROW_NUMBER() OVER (ORDER BY " + OrderBy + ") as RowNumber,tempTable.*");
            sbSql.Append(" FROM ( " + cmd.CommandText + " ) AS  tempTable ) AS tmp ");

            if (PageIndex != 1)
                sbSql.Append("WHERE RowNumber BETWEEN CONVERT(varchar,(@PageIndex-1)*@PageSize+1) AND CONVERT(varchar,(@PageIndex-1)*@PageSize+@PageSize) ");

            sbSql.Append("; SELECT @TotalRecord = count(*) FROM (" + cmd.CommandText + ") tempTable");

            //重新设置命令SQL语句
            cmd.CommandText = sbSql.ToString();

            /* 将分页参数追加至SqlParameter */

            //当前页
            SqlParameter param = new SqlParameter("@PageIndex", SqlDbType.Int);
            param.Value = PageIndex;
            cmd.Parameters.Add(param);
            //每页显示数
            param = new SqlParameter("@PageSize", SqlDbType.Int);
            param.Value = PageSize;
            cmd.Parameters.Add(param);
            //总数
            param = new SqlParameter("@TotalRecord", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            DataTable dtTemp = ExecuteSearch(cmd);

            TotalCount = (int)cmd.Parameters["@TotalRecord"].Value;

            return dtTemp;
        }
        #endregion
        /* Add by zqwu 2009-06-10 end */

    }
}

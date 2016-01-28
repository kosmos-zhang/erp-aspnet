/**********************************************
 * 类作用：   数据库操作返回数据集
 * 建立人：   吴志强
 * 建立时间： 2008/12/30
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace XBase.Data.DBHelper.Utility
{
    /// <summary>
    /// Result 是对 SqlHelper.Execute() 或 SqlHelper.ExecuteSql() 函数执行后的结果进行的封装。提供上一次执行的结果集合。
    /// </summary>  
    /// <remarks>
    /// Result 是对 SqlHelper.Execute() 或 SqlHelper.ExecuteSql() 函数执行后的结果进行的封装。提供上一次执行的结果集合。
    /// 
    /// Result 保存上一次执行所使用的所有参数(通过 Parameters 属性可以获得)，并记录是否产生错误（不抛出异常），所以每次
    /// 执行完 SqlHelper.Execute() 或 SqlHelper.ExecuteSql() 函数后都应该使用 Result.HasError 属性来判断执行过程中是否产生
    /// 了错误，若为 True，可以从 ErrorMessage 中得到错误信息。
    /// 
    /// Result.DataTable 属性可以读出执行完毕后的结果，例如使用 Select 语句执行后的结果。
    /// 
    /// Result.DataSet 可以得到结果集的 DataSet 形式；
    /// 
    /// Result.SingleResult 可以得到第一行，第一列的数据（效果与执行 SqlCommand.ExecuteScalar() 一样。 ）。
    /// 
    /// </remarks>
    public sealed class Result
    {
        /// <summary>
        /// 参数集
        /// </summary>
        public SqlParameterCollection Parameters { get; internal set; }
        /// <summary>
        /// 是否产生错误
        /// </summary>
        public bool HasError { get; internal set; }
        /// <summary>
        /// 错误信息（若产生错误）
        /// </summary>
        public string ErrorMessage { get; internal set; }
        /// <summary>
        /// 执行后的结果集(数据表)
        /// </summary>
        public DataTable DataTable { get; internal set; }
        /// <summary>
        /// 操作的记录数
        /// </summary>
        public int OprateCount { get; internal set; }

        /// <summary>
        /// 执行后的结果集(DataSet形式)
        /// </summary>
        public DataSet DataSet
        {
            get
            {
                DataSet dataSet = new DataSet();
                if (DataTable != null) dataSet.Tables.Add(DataTable);
                return dataSet;
            }
        }

        /// <summary>
        /// 执行后的单个结果(第一行，第一列的数据)
        /// </summary>
        public object SingleResult
        {
            get
            {
                return (DataTable != null && DataTable.Rows.Count > 0) ? DataTable.Rows[0][0] : null;
            }
        }

        /// <summary>
        /// 执行后的数据集记录数
        /// </summary>
        public int RecordCount
        {
            get
            {
                return (DataTable != null && DataTable.Rows.Count > 0) ? DataTable.Rows.Count : 0;
            }
        }

        /// <summary>
        /// 获得一个初始化实例，它将生成一个属性值为 { DataTable = new DataTable(), ErrorMessage = string.Empty, HasError = false } 的
        /// Result 实例。
        /// </summary>
        /// <returns>返回一个 Result 实例。</returns>
        internal static Result GetInstance()
        {
            return new Result() { DataTable = new DataTable(), ErrorMessage = string.Empty, HasError = false, OprateCount = 0 };
        }
    }
}

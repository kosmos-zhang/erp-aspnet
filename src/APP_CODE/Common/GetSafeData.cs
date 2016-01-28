/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2008.12.30
 * 描    述： 获取安全数据类
 * 修改日期： 2009.01.10
 * 版    本： 0.5.0
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XBase.Common
{
    /// <summary>
    /// 类名：GetSafeData
    /// 描述：安全获取数据，即当数据库中的数据为NULL时，保证读取不发生异常。
    /// 
    /// 作者：吴志强
    /// 创建时间：2008/12/30
    /// 最后修改时间：2008/12/30
    /// </summary>
    public class GetSafeData
    {
        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为字符串类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="columnName">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.String.Empty</returns>
        public static string ValidateDataRow_String(DataRow row, string columnName)
        {
            if (row[columnName] != DBNull.Value)
                return row[columnName].ToString();
            else
                return System.String.Empty;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为整数类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="columnName">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.Int32.MinValue</returns>
        public static int ValidateDataRow_Int(DataRow row, string columnName)
        {
            if (row[columnName] != DBNull.Value)
                return Convert.ToInt32(row[columnName]);
            else
                return System.Int32.MinValue;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为整数类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="columnName">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.Int32.MinValue</returns>
        public static long ValidateDataRow_Long(DataRow row, string columnName)
        {
            if (row[columnName] != DBNull.Value)
                return long.Parse(row[columnName].ToString());
            else
                return long.MinValue;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="columnName">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.Int32.MinValue</returns>
        public static string GetStringFromInt(DataRow row, string columnName)
        {
            if (row[columnName] != DBNull.Value)
                return Convert.ToInt32(row[columnName]).ToString();
            else
                return string.Empty;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为布尔类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="columnName">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.Int32.MinValue</returns>
        public static bool ValidateDataRow_Bool(DataRow row, string columnName)
        {
            if (row[columnName] != DBNull.Value)
                return Convert.ToBoolean(row[columnName]);
            else
                return false;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为浮点数类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="columnName">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.Double.MinValue</returns>
        public static double ValidateDataRow_Double(DataRow row, string columnName)
        {
            if (row[columnName] != DBNull.Value)
                return Convert.ToDouble(row[columnName]);
            else
                return System.Double.MinValue;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="columnName">列名</param>
        /// <returns>如果值存在，返回；否则，返回string.Empty</returns>
        public static string GetStringFromDouble(DataRow row, string columnName)
        {
            if (row[columnName] != DBNull.Value)
                return Convert.ToDouble(row[columnName]).ToString();
            else
                return string.Empty;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为浮点数类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="columnName">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.Decimal.MinValue</returns>
        public static decimal ValidateDataRow_Decimal(DataRow row, string columnName)
        {
            if (row[columnName] != DBNull.Value)
                return Convert.ToDecimal(row[columnName]);
            else
                return System.Decimal.MinValue;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="columnName">列名</param>
        /// <returns>如果值存在，返回；否则，返回string.Empty</returns>
        public static string GetStringFromDecimal(DataRow row, string columnName)
        {
            if (row[columnName] != DBNull.Value)
                return Convert.ToDecimal(row[columnName]).ToString();
            else
                return string.Empty;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为时间类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="columnName">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.DateTime.MinValue;</returns>
        public static DateTime ValidateDataRow_DateTime(DataRow row, string columnName)
        {
            if (row[columnName] != DBNull.Value)
                return Convert.ToDateTime(row[columnName]);
            else
                return System.DateTime.MinValue;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为字符型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="columnName">列名</param>
        /// <param name="format">日期格式</param>
        /// <returns>如果值存在，返回；否则，返回string.Empty;</returns>
        public static string GetStringFromDateTime(DataRow row, string columnName, string format)
        {
            if (row[columnName] != DBNull.Value)
                return Convert.ToDateTime(row[columnName]).ToString(format);
            else
                return string.Empty;
        }

    }
}

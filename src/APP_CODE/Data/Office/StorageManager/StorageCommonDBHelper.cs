using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using XBase.Data.DBHelper;

namespace XBase.Data.Office.StorageManager
{
    public class StorageCommonDBHelper
    {
        /// <summary>
        /// 根据表名，主键，公司代码查出是否状态为制单
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool IsDelete(string TableName, string ID, string CompanyCD)
        {
            string sql = "select BillStatus from " + TableName + " where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["BillStatus"].ToString() == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        #region 判断一个字符串是否为数字--hm--Add
        /// <summary>
        /// 判断一个字符串是否为数字--hm--Add
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool isNumber(string s)
        {
            int Flag = 0;
            char[] str = s.ToCharArray();
            for (int i = 0; i < str.Length; i++)
            {
                if (Char.IsNumber(str[i]))
                {
                    Flag++;
                }
                else
                {
                    Flag = -1;
                    break;
                }
            }
            if (Flag > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 判断这个排序字段可是需要按是数字来排序的
        public static bool IsOrderbyNum(string ordcolumn, string[] orderArray)
        {
            bool result = false;
            for (int i = 0; i < orderArray.Length; i++)
            {
                if (ordcolumn == orderArray[i])
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        #endregion
    }
}

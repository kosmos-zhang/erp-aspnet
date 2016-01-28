/***********************************************************************
 * 
 * Module Name: XBase.Common.StringUtil
 * Current Version: 1.0
 * Creator: jiangym
 * Auditor:2008-12-29 00:00:00
 * End Date:
 * Description:
 * Version History:
 ***********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Data;

namespace XBase.Common
{
    public class StringUtil
    {
        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                    {
                        return i;
                    }
                }
                else
                {
                    if (strSearch == stringArray[i])
                    {
                        return i;
                    }
                }

            }
            return -1;

        }

        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>		
        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            return GetInArrayID(strSearch, stringArray, true);
        }


        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns></returns>
        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="password">页面输入的密码</param>
        /// <returns>返回一个经过MD5加密的密码。</returns>
        public static string EncryptPasswordWitdhMD5(string password)
        {
            //密码没有输入时，返回null
            if (password == null || string.Empty.Equals(password))
            {
                return null;
            }
            byte[] result = Encoding.Default.GetBytes(password);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string rtPassword = BitConverter.ToString(output).Replace("-", "");
            //加密后密码超过数据库最大长度时，截取数据库中的最大长度
            if (rtPassword.Length > 50)
            {
                rtPassword = rtPassword.Substring(0, 50);
            }
            return rtPassword;
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitStringByRegex(string strContent, string strSplit)
        {
            if (!Regex.IsMatch(strContent, strSplit))
            {
                string[] tmp = { strContent };
                return tmp;
            }
            return Regex.Split(strContent, strSplit, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 获取数组元素的合并字符串
        /// </summary>
        /// <param name="stringArray"></param>
        /// <returns></returns>
        public static string GetArrayString(string[] stringArray)
        {
            string totalString = null;
            for (int i = 0; i < stringArray.Length; i++)
            {
                totalString = totalString + stringArray[i];
            }
            return totalString;
        }
        /// <summary>
        /// 获取拆分符左边的字符串
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static string LeftSplit(string sourceString, char splitChar)
        {
            string result = null;
            string[] tempString = sourceString.Split(splitChar);
            if (tempString.Length > 0)
            {
                result = tempString[0].ToString();
            }
            return result;
        }
        /// <summary>
        /// 获取拆分符右边的字符串
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static string RightSplit(string sourceString, char splitChar)
        {
            string result = null;
            string[] tempString = sourceString.Split(splitChar);
            if (tempString.Length > 0)
            {
                result = tempString[tempString.Length - 1].ToString();
            }
            return result;
        }

        /// <summary>
        /// 将全角数字转换为数字
        /// </summary>
        /// <param name="SBCCase"></param>
        /// <returns></returns>
        public static string SBCCaseToNumberic(string SBCCase)
        {
            char[] c = SBCCase.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }
        /// <summary>
        /// 删除最后一个字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearLastChar(string str)
        {
            if (str == "")
                return "";
            else
                return str.Substring(0, str.Length - 1);
        }
        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }


        /// <summary> 转半角的函数(DBC case) </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        /// <summary> 
        /// 判断字符是否是null 或者 empty
        /// </summary>
        /// <param name="str">任意字符串</param>
        /// <returns>true false</returns>
        public static bool IsNullOrBlank(string str)
        {
            if (string.IsNullOrEmpty(str))
                return true;
            return false;
        }

        /// <summary> 
        /// 去除小数点后没有意义的0
        /// </summary>
        /// <param name="str">任意字符串</param>
        /// <returns>true false</returns>
        public static string TrimZero(string str)
        {
            //字符为空时，返回
            if (string.IsNullOrEmpty(str))
                return str;
            else
            {
                //有小数时，去除末尾的0
                if (str.IndexOf(".") > 0)
                {
                    str = str.TrimEnd('0');
                    //替换掉0 后 末位为小数点时
                    if (str.IndexOf(".") == str.Length - 1)
                    {
                        str = str.TrimEnd('.');
                    }
                }
                //返回
                return str;
            }
        }

        /// <summary>
        /// 循环DataTable，取指定列集合，返回字符串
        /// </summary>
        /// <param name="dt">欲取DataTable</param>
        /// <param name="j">列索引</param>
        /// <returns></returns>
        public static string FillRowBydt(DataTable dt, int j)
        {
            string RowIdStr = "";

            if (dt == null)
            {
                return "";
            }

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RowIdStr += "," + dt.Rows[i][j].ToString();
                }
                return RowIdStr.TrimStart(',');
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 将decimal类型数据格式化
        /// </summary>
        /// <param name="selPoint">小数位数</param>
        /// <param name="dt">数据源</param>
        public static void DecimalFormatPoint(int selPoint, DataTable dt)
        {
            string format = new string('0', selPoint);
            format = "{0:0." + format + "}";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].DataType == typeof(decimal))
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        dt.Rows[j][i] = String.Format(format, (string.IsNullOrEmpty(dt.Rows[j][i].ToString()) ? 0 : dt.Rows[j][i]));
                    }
                }
            }
        }

    }
}

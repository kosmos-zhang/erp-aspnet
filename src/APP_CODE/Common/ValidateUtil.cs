/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2008.12.30
 * 描    述： 验证实用类
 * 修改日期： 2009.01.10
 * 版    本： 0.5.0
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;

namespace XBase.Common
{
    /// <summary>
    /// 类名：ValidateUtil
    /// 描述：提供一些常用的验证的方法
    /// 
    /// 作者：吴志强
    /// 创建时间：2008/12/30
    /// 最后修改时间：2008/12/30
    /// </summary>
    ///
    public class ValidateUtil
    {

        #region 数据类型验证

        /// <summary>
        /// 判断是否为base64字符串
        /// </summary>
        /// <param name="str">判断字符串</param>
        /// <returns>true 是 false 不是</returns>
        public static bool IsBase64String(string str)
        {
            //A-Z, a-z, 0-9, +, /, =
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }

        /// <summary>
        /// 验证是否为非负整数
        /// </summary>
        /// <param name="str">判断字符串</param>
        /// <returns>true 是 false 不是</returns>
        public static bool IsInt(string str)
        {
            return Regex.IsMatch(str, @"^[0-9]*$");
        }

        /// <summary>
        /// 验证是否为正整数
        /// </summary>
        /// <param name="str">判断字符串</param>
        /// <returns>true 是 false 不是</returns>
        public static bool IsZInt(string str)
        {
            return Regex.IsMatch(str, @"^[0-9]*[1-9][0-9]*$");
        }


        /// <summary>
        /// 判断对象是否为数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object Expression)
        {
            if (Expression != null)
            {
                string str = Expression.ToString();
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 判断对象是否为Double类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsDouble(object Expression)
        {
            if (Expression != null)
            {
                return Regex.IsMatch(Expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");
            }
            return false;
        }

        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            foreach (string id in strNumber)
            {
                if (!IsNumeric(id))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion


        #region 字符串功能验证

        /// <summary>
        /// 是否包涵制定的字符串
        /// str中是否包含strArray
        /// </summary>
        /// <param name="str">被测字符串</param>
        /// <param name="stringarray">被包含的字符串</param>
        /// <param name="strsplit">字符串分隔符</param>
        /// <returns></returns>
        public static bool IsCompriseStr(string str, string strArray, string strSplit)
        {
            if (strArray == "" || strArray == null)
            {
                return false;
            }

            str = str.ToLower();
            string[] stringArray = StringUtil.SplitStringByRegex(strArray.ToLower(), strSplit);
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (str.IndexOf(stringArray[i]) > -1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断字符串是否是mail地址形式
        /// </summary>
        /// <param name="str">被测字符串</param>
        /// <returns>true 是mail地址 false 不是mail地址</returns>
        public static bool IsMailAddressStr(string str)
        {
            return Regex.IsMatch(str, "^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$");
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果 true 包含 false 不包含</returns>
        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return StringUtil.GetInArrayID(strSearch, stringArray, caseInsensetive) >= 0;
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="strArray">字符串数组</param>
        /// <returns>判断结果 true 包含 false 不包含</returns>
        public static bool InArray(string str, string[] strArray)
        {
            return InArray(str, strArray, false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="strArray">内部以逗号分割单词的字符串</param>
        /// <returns>判断结果 true 包含 false 不包含</returns>
        public static bool InArray(string str, string strArray)
        {
            return InArray(str, StringUtil.SplitStringByRegex(strArray, ","), false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="strArray">内部以逗号分割单词的字符串</param>
        /// <param name="strSplit">分割字符串</param>
        /// <returns>判断结果 true 包含 false 不包含</returns>
        public static bool InArray(string str, string strArray, string strSplit)
        {
            return InArray(str, StringUtil.SplitStringByRegex(strArray, strSplit), false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="strArray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string strArray, string strsplit, bool caseInsensetive)
        {
            return InArray(str, StringUtil.SplitStringByRegex(strArray, strsplit), caseInsensetive);
        }

        /// <summary>
        /// 判断一个字符串是否为空
        /// </summary>
        /// <param name="strInput">输入的字符串</param>
        /// <returns>true/false</returns>
        public static bool IsBlank(string strInput)
        {
            if (strInput == null || strInput.Trim() == "")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 判断一个字符串是否为英文字母或数字
        /// </summary>
        /// <param name="str">输入的字符串</param>
        /// <returns></returns>
        public static bool IsLetterOrNumber(string str)
        {
            return new Regex(@"^[A-Za-z0-9]+$").IsMatch(str);
        }
        #endregion

        /// <summary>
        /// 判断一个字符串是否由字母和数字一起组成
        /// </summary>
        /// <param name="str">输入的字符串</param>
        /// <returns></returns>
        public static bool IsLetterAndNumber(string str)
        {
            //如果字符串为空，则返回false
            if (str == null || str.Length == 0)
                return false;
            //是否包含数字标志位
            bool isContainNumber = false;
            //是否包含字母标志位
            bool isContainLetter = false;

            //获得字符串Unicode 字符数组
            char[] charactors = str.ToCharArray();
            foreach (char charc in charactors)
            {
                //如果字符串中尚未找到数字，判断该字符是否属于数字
                if (!isContainNumber && Char.IsNumber(charc))
                {
                    isContainNumber = true;
                    continue;
                }
                //如果字符串中尚未找到字母，判断该字符是否属于字母
                if (!isContainLetter && Char.IsLetter(charc))
                {
                    isContainLetter = true;
                    continue;
                }
                //如果字符串中同时包含了数字和字母，则退出循环
                if (isContainNumber && isContainLetter)
                {
                    break;
                }
            }
            if (isContainLetter && isContainNumber)
                return true;
            return false;
        }


        #region IP地址 SQL注入等安全验证

        /// <summary>
        /// 判断一个字符串是否为ip
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>true 正确IP false 错误IP</returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 判断一个字符串是否为i是否为不完全显示的ip
        /// 最后为*，不完全显示IP
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>true 正确IP false 错误IP</returns>
        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");

        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 检测是否有危险的可能用于链接的字符串
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|游客|^Guest");
        }

        /// <summary>
        /// 返回指定IP是否在指定的IP数组所限定的范围内, IP数组内的IP地址可以使用*表示该IP段任意, 例如192.168.1.*
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="iparray"></param>
        /// <returns></returns>
        public static bool InIPArray(string ip, string[] iparray)
        {
            string[] userip = StringUtil.SplitStringByRegex(ip, @".");
            for (int ipIndex = 0; ipIndex < iparray.Length; ipIndex++)
            {
                string[] tmpip = StringUtil.SplitStringByRegex(iparray[ipIndex], @".");
                int r = 0;
                for (int i = 0; i < tmpip.Length; i++)
                {
                    if (tmpip[i] == "*")
                    {
                        return true;
                    }

                    if (userip.Length > i)
                    {
                        if (tmpip[i] == userip[i])
                        {
                            r++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                if (r == 4)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion


        #region 其他功能验证

        /// <summary>
        /// 判断文件流是否为UTF8字符集
        /// </summary>
        /// <param name="sbInputStream">文件流</param>
        /// <returns>判断结果</returns>
        private static bool IsUTF8(FileStream sbInputStream)
        {
            int i;
            byte cOctets;  // octets to go in this UTF-8 encoded character 
            byte chr;
            bool bAllAscii = true;
            long iLen = sbInputStream.Length;

            cOctets = 0;
            for (i = 0; i < iLen; i++)
            {
                chr = (byte)sbInputStream.ReadByte();

                if ((chr & 0x80) != 0) bAllAscii = false;

                if (cOctets == 0)
                {
                    if (chr >= 0x80)
                    {
                        do
                        {
                            chr <<= 1;
                            cOctets++;
                        }
                        while ((chr & 0x80) != 0);

                        cOctets--;
                        if (cOctets == 0) return false;
                    }
                }
                else
                {
                    if ((chr & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    cOctets--;
                }
            }

            if (cOctets > 0)
            {
                return false;
            }

            if (bAllAscii)
            {
                return false;
            }

            return true;

        }


        #endregion


        #region 日期类型格式验证
        /// <summary>
        /// 判断是否是正确的时间 24小时制
        /// </summary>
        /// <returns></returns>
        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }

        /// <summary>
        /// 判断字符串是否是yy-mm-dd字符串
        /// </summary>
        /// <param name="str">待判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsDateString(string str)
        {
            return Regex.IsMatch(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
        }

        /// <summary>
        /// 判断字符是否是Decimal类型值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDecimal(string str)
        {
            return Regex.IsMatch(str, @"^\d+(\.\d*)?$");
        }

        /// <summary>
        /// 判断字符串是否日期
        /// </summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool IsDate(string strInput)
        {
            string datestr = strInput;
            string year, month, day;
            //日期分隔符
            string[] c = { "/", "-", "." };
            string cs = "";
            //获得该字符串的分隔符
            for (int i = 0; i < c.Length; i++)
            {
                if (datestr.IndexOf(c[i]) > 0)
                {
                    cs = c[i];
                    break;
                }
            }

            if (cs != "")
            {
                //获得年
                year = datestr.Substring(0, datestr.IndexOf(cs));
                //年的长度不为4是返回false
                if (year.Length != 4) 
                    return false;
                datestr = datestr.Substring(datestr.IndexOf(cs) + 1);
                //获得月
                month = datestr.Substring(0, datestr.IndexOf(cs));
                //不是正确的月份时返回false
                if ((month.Length != 2) || (Convert.ToInt16(month) > 12))
                    return false;

                datestr = datestr.Substring(datestr.IndexOf(cs) + 1);
                //获得日
                day = datestr;
                //不是正确的日时返回false
                if ((day.Length != 2) || (Convert.ToInt16(day) > 31)) { return false; };                
            }
            else
            {
                if (strInput.Length == 8)
                {
                    year = strInput.Substring(0, 4);
                    month = strInput.Substring(4, 2);
                    day = strInput.Substring(6);
                } else  return false;
            }
            return CheckDatePart(year, month, day);
        }

        /// <summary>
        /// 检查年月日是否合法
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns></returns>
        private static bool CheckDatePart(string year, string month, string day)
        {
            int iyear = Convert.ToInt16(year);
            int imonth = Convert.ToInt16(month);
            int iday = Convert.ToInt16(day);
            //if (iyear > 2099 || iyear < 1900) { return false; }
            if (imonth > 12 || imonth < 1) { return false; }
            if (iday > DateUtil.GetDaysOfMonth(iyear, imonth) || iday < 1) { return false; };
            return true;
        }

        /// <summary>
        /// 比较两个日期大小
        /// 有符号数字，指示此 strDateTime1 和 strDateTime2 的相对值。
        /// 值 说明 
        ///     小于零 此 strDateTime1 早于 strDateTime2
        ///     零     此 strDateTime1 与 strDateTime2 相同
        ///     大于零 此 strDateTime1 晚于 strDateTime2
        /// </summary>
        /// <param name="strDateTime1">比较的日期1</param>
        /// <param name="strDateTime2">比较的日期2</param>
        /// <returns></returns>
        public static int CompareDate(string strDateTime1, string strDateTime2)
        {
            //判断字符串是否是合法的日期形式
            if (!IsDate(strDateTime1) || !IsDate(strDateTime2))
            {
                return int.MinValue;
            }
            //转化为DateTime类型
            DateTime DateTime1 = DateTime.Parse(strDateTime1);
            DateTime DateTime2 = DateTime.Parse(strDateTime2);
            return DateTime1.CompareTo(DateTime2);
        }

        /// <summary>
        /// 判断是否为数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNum(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] < '0' || str[i] > '9')
                    return false;
            }
            return true;
        }

        #endregion

    }
}

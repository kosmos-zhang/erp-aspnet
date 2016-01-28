using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Common
{
    /// <summary>
    /// CRC算法类
    /// </summary>
    public class CRCer
    {
        private readonly static System.Random _random = new Random(unchecked((int)DateTime.Now.Ticks));

        /// <summary>
        /// 校验输入字符串是否用自定义的CRC算法加密过
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        public static bool CheckString(string inputText)
        {
            if (inputText.Length <= 6)
            {
                return false;
            }

            //原始字符串
            string primeText = inputText.Substring(0, inputText.Length - 6);
            //richCRCCode
            string richCRCCode = inputText.Substring(inputText.Length - 6, 6);

            string crcCode = GetCRCfromRichCRC(richCRCCode);
            if (crcCode == string.Empty)
                return false;

            return crcCode == Get16CRC(primeText);

        }


        /// <summary>
        /// 从6位的RichCRC中获取实际的CRC CODE
        /// </summary>
        /// <param name="RichCRC">长度必须为6</param>
        /// <returns></returns>
        public static string GetCRCfromRichCRC(string RichCRC)
        {
            if (RichCRC.Length != 6)
                return string.Empty;


            //取标志位
            int flag = int.Parse(RichCRC.Substring(0, 1));

            //如果CRC码的长度正好是5位，则标志位为5-9之间的随机值
            if (flag >= 5 && flag <= 9)
            {
                //返回CRC码
                return RichCRC.Substring(1, 5);
            }

            //如果CRC码的长度小于5位，标志位为空缺长度（1-4）
            return RichCRC.Substring(flag + 1);

        }

        /// <summary>
        /// 完整的RichCRC码是6位的
        /// </summary>
        /// <param name="CRCCode"></param>
        /// <param name="richText"></param>
        /// <returns></returns>
        public static string Get16RichCRC(string inputText)
        {
            //获取CRC码
            string CRCCode = Get16CRC(inputText);


            //如果CRC码的长度正好是5位，则标志位为5-9之间的随机值
            if (CRCCode.Length == 5)
            {
                string flag = _random.Next(5, 10).ToString();
                return flag + CRCCode;
            }

            //如果CRC码的长度小于5位，则在空缺位置补上随机值，并置标志位为空缺长度（1-4）
            int blank = 5 - CRCCode.Length;

            string sblank = "";
            for (int i = 0; i < blank; i++)
            {
                sblank += _random.Next(10).ToString();
            }

            //返回完整的RichCRC码
            return blank.ToString() + sblank + CRCCode;
        }

        /// <summary>
        /// 获取随即字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomString(int length)
        {
            string sblank = "";
            for (int i = 0; i < length; i++)
            {
                sblank += _random.Next(10).ToString();
            }

            return sblank;
        }


        /// <summary>
        /// Get16CRC --- CRC16校验算法
        /// </summary>
        /// <param name="sbuffer"></param>
        /// <returns></returns>
        public static string Get16CRC(string sbuffer)
        {
            System.Text.UnicodeEncoding cvt = new System.Text.UnicodeEncoding();
            byte[] buffer = cvt.GetBytes(sbuffer);

            //CRC = 1111,1111,1111,1111  CRC初始值
            UInt16 CRC = 0xFFFF;

            //temp=1010,0000,0000,0001   CRC 多项式（权）
            UInt16 temp = 0xA001;

            for (int k = 0; k < buffer.Length; k++)
            {
                CRC ^= buffer[k];
                for (int i = 0; i < 8; i++)
                {
                    int j = CRC & 1;
                    CRC >>= 1;

                    //0x7FFF = 0111,1111,1111,1111
                    CRC &= 0x7FFF;
                    if (j == 1)
                        CRC ^= temp;
                }
            }
            return Convert.ToString(CRC);
        }


        /*  应用相关 */
        /// <summary>
        /// 获取页面安全验证码
        /// </summary>
        /// <returns></returns>
        public static void GetValidateCode(System.Web.UI.Page page)
        {
            //string tick = DateTime.Now.Ticks.ToString();
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string url = context.Request.Url.PathAndQuery;

            string crcedtick = url + CRCer.Get16RichCRC(url);
            crcedtick = context.Server.UrlEncode(crcedtick);

            string outputJs = "var _page_security_validate_code=\"" + crcedtick + "\";";

            page.ClientScript.RegisterStartupScript(page.GetType(), "jskey_page_security_validate_code", outputJs, true);
        }
    }
}

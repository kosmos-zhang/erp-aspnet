/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2008.12.30
 * 描    述： 验证码类
 * 修改日期： 2009.01.10
 * 版    本： 0.5.0
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace XBase.Common
{
    /// <summary>
    /// 类名：ValidateImage
    /// 描述：生成页面上验证码的图片
    /// 
    /// 作者：吴志强
    /// 创建时间：2008/12/30
    /// 最后修改时间：2008/12/30
    /// </summary>
    ///
    public class ValidateImage
    {
        /// <summary>
        /// 生成4位字母和数字的验证码图片
        /// </summary>
        /// <returns>验证码的字符串</returns>
        public static MemoryStream GenerateImage()
        {
            //产生验证码
            string CheckCode = GenerateCheckCode();
            //构造验证码图片
            return CreateImage(CheckCode);
        }

        /// <summary>
        /// 生成4位字母和数字
        /// </summary>
        /// <returns>验证码的字符串</returns>
        private static string GenerateCheckCode()
        {
            int number;
            char code;
            string CheckCode = String.Empty;
            System.Random random = new Random();
            for (int iCount = 0; iCount < 4; iCount++)
            {
                //产生随机数
                number = random.Next();
                //根据随机数奇偶性来产生数字或者字母
                if (number % 2 == 0)
                {
                    //数字验证码
                    code = (char)('0' + (char)(number % 10));
                }
                else
                {
                    //字母验证码
                    code = (char)('A' + (char)(number % 26));
                }
                CheckCode += code.ToString();
            }

            SessionUtil.Session.Add("CheckCode", CheckCode);
            return CheckCode;
        }

        /// <summary>
        /// 创建验证码图片,并将其写入内存流中
        /// </summary>
        /// <param name="CheckCode"></param>
        private static MemoryStream CreateImage(string CheckCode)
        {
            //验证码未设置时，不生成图片
            if (CheckCode == null || CheckCode.Trim() == String.Empty)
            {
                return null;
            }
            Bitmap img = new Bitmap(CheckCode.Length * 13, 22);
            Graphics g = Graphics.FromImage(img);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的背景噪音线
                for (int iCount = 0; iCount < 25; iCount++)
                {
                    int x1 = random.Next(img.Width);
                    int x2 = random.Next(img.Width);
                    int y1 = random.Next(img.Height);
                    int y2 = random.Next(img.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(CheckCode, font, brush, 2, 2);

                //画图片的前景噪音点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(img.Width);
                    int y = random.Next(img.Height);
                    img.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, img.Width - 1, img.Height - 1);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms;
                //Response.ClearContent();
                //Response.ContentType = "image/Gif";
                //Response.BinaryWrite(ms.ToArray());
                //return img;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                g.Dispose();
                img.Dispose();
            }
        }
    }
}

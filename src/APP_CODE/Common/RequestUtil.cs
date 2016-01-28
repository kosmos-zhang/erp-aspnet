
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace XBase.Common
{
   public class RequestUtil
    {

        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }

        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }


        /// <summary>
        /// 返回指定的服务器变量信息
        /// </summary>
        /// <param name="strName">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        public static string GetServerString(string strName)
        {
            //
            if (HttpContext.Current.Request.ServerVariables[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.ServerVariables[strName].ToString();
        }

        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;

            try
            {
                retVal = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch { }

            if (retVal == null)
                return "";

            return retVal;

        }

        /// <summary>
        /// 得到当前完整主机头
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost()
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            if (!request.Url.IsDefaultPort)
            {
                return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
            }
            return request.Url.Host;
        }

        /// <summary>
        /// 得到主机头
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }
        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns>是否来自搜索引擎链接</returns>
        public static bool IsSearchEnginesGet()
        {
            if (HttpContext.Current.Request.UrlReferrer == null)
            {
                return false;
            }
            string[] SearchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };
            string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
            for (int i = 0; i < SearchEngine.Length; i++)
            {
                if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        /// <summary>
        /// 获得当前页面的名称
        /// </summary>
        /// <returns>当前页面的名称</returns>
        public static string GetPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {

            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return String.Empty;
            }
            return HttpContext.Current.Request.QueryString[strName];

        }

        /// <summary>
        /// 返回表单或Url参数的总个数
        /// </summary>
        /// <returns></returns>
        public static int GetParamCount()
        {
            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
        }


        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (null == result || result == String.Empty || !ValidateUtil.IsIP(result))
            {
                return "0.0.0.0";
            }
            return result;
        }

        /// <summary>
        /// 获得当前客户端使用的浏览器类型
        /// </summary>
        /// <returns>获得当前客户端使用的浏览器类型名</returns>
        public static string GetBrowserType()
        {
            HttpBrowserCapabilities bc = HttpContext.Current.Request.Browser;
            return bc.Type;
        }

        /// <summary>
        /// 保存用户上传的文件
        /// </summary>
        /// <param name="path">保存路径</param>
        public static void SaveRequestFile(string path)
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpContext.Current.Request.Files[0].SaveAs(path);
            }
        }

        /// <summary>
        /// 保存上传的文件
        /// </summary>
        /// <param name="MaxAllowFileCount">最大允许的上传文件个数</param>
        /// <param name="MaxAllowFileSize">最大允许的文件长度(单位: KB)</param>
        /// <param name="AllowFileExtName">允许的文件扩展名, 以string[]形式提供</param>
        /// <param name="AllowFileType">允许的文件类型, 以string[]形式提供</param>
        /// <param name="Dir">目录</param>
        /// <returns></returns>
        //public static string[] SaveRequestFiles(int MaxAllowFileCount, int MaxAllowFileSize, string[] AllowFileExtName, string[] AllowFileType, string Dir)
        //{
        //    string[] attachmentinfo = new string[MaxAllowFileCount];
        //    int fcount = Math.Min(MaxAllowFileCount, HttpContext.Current.Request.Files.Count);
        //    for (int i = 0; i < fcount; i++)
        //    {
        //        string filename = HttpContext.Current.Request.Files[i].FileName;
        //        string fileextname = filename.Substring(filename.LastIndexOf("."));
        //        string filetype = HttpContext.Current.Request.Files[i].ContentType;
        //        int filesize = HttpContext.Current.Request.Files[i].ContentLength;
        //        // 判断 文件扩展名/文件大小/文件类型 是否符合要求
        //        if (ValidateUtil.InArray(fileextname, AllowFileExtName) && (filesize <= MaxAllowFileSize * 1024) && ValidateUtils.InArray(filetype, AllowFileType))
        //        {
        //            HttpContext.Current.Request.Files[i].SaveAs(Dir + DateUtil.GetDateTime() + Environment.TickCount.ToString() + fileextname);
        //            attachmentinfo[i] = Dir + DateUtil.GetDateTime() + Environment.TickCount.ToString() + fileextname;
        //        }
        //    }
        //    return attachmentinfo;

        //}
    } 
}

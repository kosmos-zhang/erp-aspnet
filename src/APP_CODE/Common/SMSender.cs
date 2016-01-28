using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Configuration;


namespace XBase.Common
{
    public class SMSender
    {

        #region 日志初始化 --add by Moshenlin 2010-07-02
        #region 变量定义

        //配置文件中设置日志路径的KEY
        private const string FILE_PATH_LOG = "LogPath";
        //存放日志的路径
        private static string _LogFilePath = string.Empty;
        //配置文件中设置日志路径的KEY
        private const string FILE_PATH_LOG_BAK = "LogArchPath";
        //存放日志的路径
        private static string _LogFilePathBak = string.Empty;

        //配置文件中设置日志最大长度的KEY
        private const string FILE_MAX_SIZE_LOG = "FILE_MAX_SIZE_LOG";

        //日志文件的最大长度(KB)
        private static long _FileMaxSizeLog = 1024000;

        //系统日志文件名
        private const string LOG_FILE_NAME_SYSTEM = "LOG_FILE_NAME_SYSTEM";
        private static string _LogFileNameSystem = "SystemLog.txt";

        //登陆日志文件名
        private const string LOG_FILE_NAME_LOGIN = "LOG_FILE_NAME_LOGIN";
        private static string _LogFileNameLogin = "Login.txt";

        //从配置文件读取配置标志
        private static bool _IsLoad = false;

        #endregion

        #region 日志初期设置

        /// <summary>
        /// 初期化日志操作的一些参数
        /// </summary>
        private static void Init()
        {
            //读取配置文件
            if (!_IsLoad)
            {
                string rootPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;

                //获取日志文件路径
                if (ConfigurationManager.AppSettings[FILE_PATH_LOG] == null)
                {
                    _LogFilePath = rootPath + "\\log";//ConfigurationManager.AppSettings[FILE_PATH_LOG];
                }
                else
                {
                    _LogFilePath = ConfigurationManager.AppSettings[FILE_PATH_LOG];
                }


                //日志备份路径
                if (ConfigurationManager.AppSettings[FILE_PATH_LOG_BAK] == null)
                {
                    _LogFilePathBak = rootPath + "\\log\\bak";//ConfigurationManager.AppSettings[FILE_PATH_LOG_BAK];
                }
                else
                {
                    _LogFilePathBak = ConfigurationManager.AppSettings[FILE_PATH_LOG_BAK];
                }



                //日志文件的最大长度
                _FileMaxSizeLog = long.Parse(ConfigurationManager.AppSettings[FILE_MAX_SIZE_LOG]);


                //系统日志文件名
                _LogFileNameSystem = ConfigurationManager.AppSettings[LOG_FILE_NAME_SYSTEM];
                //登陆日志文件名
                _LogFileNameLogin = ConfigurationManager.AppSettings[LOG_FILE_NAME_LOGIN];
                _IsLoad = true;
            }
        }

        #endregion
        #endregion

        #region 写入日志文件

        /// <summary>
        /// 将文本写入日志文件
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="fileName">日志文件名</param>
        private static void WriteTextToFile(string msg, string fileName)
        {
            //读取配置文件设置
            Init();
            //日志文件目录未创建时，创建文件夹
            if (!Directory.Exists(SMSender._LogFilePath))
            {
                Directory.CreateDirectory(SMSender._LogFilePath);
            }
            StreamWriter sw = null;
            FileInfo fileInfo = new FileInfo(Path.Combine(SMSender._LogFilePath, fileName));
            //文件不存在
            if (!fileInfo.Exists)
            {
                sw = File.CreateText(Path.Combine(SMSender._LogFilePath, fileName));
            }
            else
            {
                //文件大小超过最大的文件大小
                if (fileInfo.Length >= SMSender._FileMaxSizeLog)
                {
                    string BackupFilePath = Path.Combine(SMSender._LogFilePathBak, DateTime.Now.ToString("yyyyMMdd HHmmss"));
                    //备份目录未创建时，创建文件夹
                    if (!Directory.Exists(BackupFilePath))
                    {
                        Directory.CreateDirectory(BackupFilePath);
                    }
                    //备份原文件
                    fileInfo.MoveTo(Path.Combine(BackupFilePath, fileName));
                    //重新生成新文件
                    sw = File.CreateText(Path.Combine(SMSender._LogFilePath, fileName));
                }
                else
                {
                    sw = new StreamWriter(fileInfo.OpenWrite());
                }
            }

            sw.BaseStream.Seek(0, SeekOrigin.End);

            sw.Write(msg);
            sw.Flush();
            sw.Close();
        }

        #endregion

        private static string _SMSUserID = "";
        private static string SMSUserID
        {
            get
            {
                if (_SMSUserID == "")
                {
                    _SMSUserID = System.Configuration.ConfigurationManager.AppSettings["SMSUserID"];
                }
                return _SMSUserID;
            }
        }

        private static string _SMSPassword = "";
        private static string SMSPassword
        {
            get
            {
                if (_SMSPassword == "")
                {
                    _SMSPassword = System.Configuration.ConfigurationManager.AppSettings["SMSPassword"];
                }
                return _SMSPassword;
            }
        }

        //private static BackgroundTask bkTask = new BackgroundTask();

        /// <summary>
        /// SendBatch
        /// </summary>
        /// <param name="phoneNums"></param>
        /// <param name="shortMessage"></param>
        public static void SendBatch(string phoneNums, string shortMessage)
        {
            //bkTask.Run(
            InternalSend(phoneNums, shortMessage);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNums"></param>
        /// <param name="shortMessage"></param>
        public static void InternalSend(string phoneNums, string shortMessage)
        {
            var url = "http://www.xunsai.net:8000/";
            //?user=username&password=password
            //&phonenumber=13761111022
            //&text=短信测试&charset=gb2312

            var data = "user=" + System.Web.HttpUtility.UrlEncode(SMSUserID, System.Text.Encoding.UTF8);
            data += "&password=" + System.Web.HttpUtility.UrlEncode(SMSPassword,System.Text.Encoding.UTF8);
            data += "&phonenumber=" + phoneNums;
            data += "&text=" + System.Web.HttpUtility.UrlEncode(shortMessage, System.Text.Encoding.Default) + "&charset=gb2312";

            //string result = 
            SendData(url, data, "POST");

        }

        public static bool InternalSendbackgroud(string phoneNums, string shortMessage)
        {
            if (phoneNums == "")
                return false ;
            var url = "http://www.xunsai.net:8000/";
            //?user=username&password=password
            //&phonenumber=13761111022
            //&text=短信测试&charset=gb2312

            var data = "user=" + SMSUserID;
            data += "&password=" +SMSPassword;
            data += "&phonenumber=" + phoneNums;
            data += "&text=" + shortMessage + "&charset=gb2312";

            //string result = 
            SendData(url, data, "POST");
            return true;
        }


        /// <summary>
        /// 返回URL内容,带POST数据提交
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method">GET/POST(默认)</param>
        /// <returns></returns>
        private static string SendData(string url, string data, string method)
        {

            try
            {
                WebRequest wr = WebRequest.Create(url);
                wr.Method = method;
                wr.ContentType = "application/x-www-form-urlencoded";
                // char[] reserved = { '?', '=', '&' };
                StringBuilder UrlEncoded = new StringBuilder();
                byte[] SomeBytes = null;
                if (data != null)
                {
                    SomeBytes = System.Text.Encoding.Default.GetBytes(data);
                    wr.ContentLength = SomeBytes.Length;
                    Stream newStream = wr.GetRequestStream();
                    newStream.Write(SomeBytes, 0, SomeBytes.Length);
                    newStream.Close();
                }
                else
                {
                    wr.ContentLength = 0;
                }

                Stream resStream = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(resStream, System.Text.Encoding.GetEncoding("GB2312"));
                string re = sr.ReadToEnd();

                #region old
                //try
                //{
                //    WebResponse result = wr.GetResponse();
                //    Stream ReceiveStream = result.GetResponseStream();

                //    Byte[] read = new Byte[512];
                //    int bytes = ReceiveStream.Read(read, 0, 512);

                //    re = "";
                //    while (bytes > 0)
                //    {

                //        // 注意：
                //        // 下面假定响应使用 UTF-8 作为编码方式。
                //        // 如果内容以 ANSI 代码页形式（例如，932）发送，则使用类似下面的语句：
                //        //  Encoding encode = System.Text.Encoding.GetEncoding("shift-jis");
                //        Encoding encode = System.Text.Encoding.GetEncoding("gb2312");
                //        re += encode.GetString(read, 0, bytes);
                //        bytes = ReceiveStream.Read(read, 0, 512);
                //    }
                //}
                //catch (Exception e)
                //{
                //    re = e.Message;
                //}
                #endregion

                return re;
            }
            catch (Exception ex)
            {
                string filename = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID + "." + DateTime.Now.ToString("yyyyMMdd") + "." + _LogFileNameLogin;
                WriteTextToFile(ex.ToString(), filename);
                throw ex;
            }
        }



        


    }
}

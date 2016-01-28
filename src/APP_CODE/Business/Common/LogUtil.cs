/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2008.12.29
 * 描    述： 日志输出类
 * 修改日期： 2009.03.06
 * 版    本： 0.5.0
 ***********************************************/

using System;
using XBase.Data.Common;
using XBase.Model.Common;
using System.IO;
using System.Configuration;
using System.Text;
using XBase.Common;

namespace XBase.Business.Common
{
    /// <summary>
    /// 类名：LogUtil
    /// 描述：登陆业务操作的日志
    /// 
    /// 作者：吴志强
    /// 创建时间：2008-12-29
    /// 最后修改时间：2009-03-06
    /// </summary>
    /// 
    public class LogUtil
    {
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
                else {
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

        #region 将日志写入文件

        /// <summary>
        /// 将日志写入日志文件
        /// </summary>
        /// <param name="log">日志相关信息</param>
        private static void WriteLogToFile(LogInfo log)
        {
            //日志文件名
            string fileName = string.Empty;
            //日志内容
            string msg = string.Empty;
            //登陆日志
            if (LogInfo.LogType.LOGIN == log.Type)
            {
                fileName = log.UserInfo.UserID + "." + DateTime.Now.ToString("yyyyMMdd") + "." + _LogFileNameLogin;
                //编辑登陆日志内容
                msg = EditLoginLogInfo(log);
            }
            //系统日志
            else if (LogInfo.LogType.SYSTEM == log.Type)
            {
                fileName = _LogFileNameSystem;
                //编辑系统日志内容
                msg = EditSystemLog(log);
            }
            //写入日志
            WriteTextToFile(msg, fileName);
        }

        #endregion

        #region 编辑登陆日志内容

        /// <summary>
        /// 编辑登陆日志内容
        /// </summary>
        /// <param name="log">日志相关信息</param>
        private static string EditLoginLogInfo(LogInfo log)
        {
            StringBuilder sb = new StringBuilder();
            //登录时间
            sb.Append(DateTime.Now.ToString("HH:mm:ss") + ConstUtil.TAB);
            //登录IP
            sb.Append(RequestUtil.GetIP() + ConstUtil.TAB);
            //浏览器类型
            sb.Append(RequestUtil.GetBrowserType() + ConstUtil.TAB);
            //登录类型
            if (LogInfo.LoginLogKind.LOGIN_LOGIN == log.LoginKind)
            {
                sb.Append(ConstUtil.LOG_LOGIN + ConstUtil.TAB);
            }
            else if (LogInfo.LoginLogKind.LOGIN_LOGOUT == log.LoginKind)
            {
                sb.Append(ConstUtil.LOG_LOGOUT + ConstUtil.TAB);
            }
            //登录状态
            if (LogInfo.OperateStatus.SUCCESS == log.Status)
            {
                sb.Append(ConstUtil.LOG_LOGIN_SUCCESS);
            }
            else if (LogInfo.OperateStatus.FAILED == log.Status)
            {
                sb.Append(ConstUtil.LOG_LOGIN_FAILURE);
            }

            sb.Append("\n");
            return sb.ToString();
        }

        #endregion

        #region 编辑系统日志内容

        /// <summary>
        /// 编辑系统日志内容
        /// </summary>
        /// <param name="log">日志相关信息</param>
        private static string EditSystemLog(LogInfo log)
        {
            StringBuilder sb = new StringBuilder();
            //类型
            if (LogInfo.SystemLogKind.SYSTEM_INFO == log.SystemKind)
            {
                sb.Append(ConstUtil.LOG_SYSTEM_INFO + ConstUtil.TAB);
            }
            else if (LogInfo.SystemLogKind.SYSTEM_WARNING == log.SystemKind)
            {
                sb.Append(ConstUtil.LOG_SYSTEM_WARNING + ConstUtil.TAB);
            }
            else if (LogInfo.SystemLogKind.SYSTEM_ERROR == log.SystemKind)
            {
                sb.Append(ConstUtil.LOG_SYSTEM_ERROR + ConstUtil.TAB);
            }
            //用户ID
            sb.Append(log.UserInfo.UserID + ConstUtil.TAB);
            //日期
            sb.Append(DateTime.Now.ToString("yyyy-MM-dd") + ConstUtil.TAB);
            //时间
            sb.Append(DateTime.Now.ToString("HH:mm:ss") + ConstUtil.TAB);
            //来源
            sb.Append(log.ModuleID + ConstUtil.TAB);
            //描述
            sb.Append(log.Description + ConstUtil.TAB);

            sb.Append("\n");
            return sb.ToString();
        }

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
            if (!Directory.Exists(LogUtil._LogFilePath))
            {
                Directory.CreateDirectory(LogUtil._LogFilePath);
            }
            StreamWriter sw = null;
            FileInfo fileInfo = new FileInfo(Path.Combine(LogUtil._LogFilePath, fileName));
            //文件不存在
            if (!fileInfo.Exists)
            {
                sw = File.CreateText(Path.Combine(LogUtil._LogFilePath, fileName));
            }
            else
            {
                //文件大小超过最大的文件大小
                if (fileInfo.Length >= LogUtil._FileMaxSizeLog)
                {
                    string BackupFilePath = Path.Combine(LogUtil._LogFilePathBak, DateTime.Now.ToString("yyyyMMdd HHmmss"));
                    //备份目录未创建时，创建文件夹
                    if (!Directory.Exists(BackupFilePath))
                    {
                        Directory.CreateDirectory(BackupFilePath);
                    }
                    //备份原文件
                    fileInfo.MoveTo(Path.Combine(BackupFilePath, fileName));
                    //重新生成新文件
                    sw = File.CreateText(Path.Combine(LogUtil._LogFilePath, fileName));
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

        #region 设置日志模板

        /// <summary>
        /// 编辑日志
        /// </summary>
        /// <param name="log">日志信息</param>
        private static LogInfoModel EditLogInfo(LogInfo log)
        {
            //日志数据模板定义
            LogInfoModel model = new LogInfoModel();
            //公司代码
            model.CompanyCD = log.UserInfo.CompanyCD;
            //登陆用户ID
            model.UserID = log.UserInfo.UserID;
            //操作模块ID
            model.ModuleID = log.ModuleID;
            //操作单据编号
            model.ObjectID = log.ObjectID;
            //操作对象
            model.ObjectName = log.ObjectName;
            //涉及关键元素
            model.Element = log.Element;
            //备注
            model.Remark = log.Description;

            return model;
        }

        #endregion

        #region 日志输出，外部接口

        /// <summary>
        /// 登陆日志内容
        /// </summary>
        /// <param name="log">日志内容</param>
        /// <remarks>
        /// 
        /// </remarks>
        public static void WriteLog(LogInfo log)
        {
            //操作日志
            if (LogInfo.LogType.PROCESS == log.Type)
            {
                //编辑日志内容
                LogInfoModel model = EditLogInfo(log);
                //登陆数据库
                bool insertSucc = LogDBHelper.InsertLog(model);
            }
            else
            {
                //日志写入文件
                WriteLogToFile(log);
            }
        }

        #endregion

    }
}


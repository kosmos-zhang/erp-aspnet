/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009.01.07
 * 描    述： 日志输出辅助类
 * 修改日期： 2009.03.06
 * 版    本： 0.5.0
 ***********************************************/

using System;
using XBase.Common;

namespace XBase.Business.Common
{
    /// <summary>
    /// 类名：LogInfo
    /// 描述：提供输出日志的各种属性
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/01/07
    /// 最后修改时间：2009/03/06
    /// </summary>
    ///
    public class LogInfo
    {
        //日志类别
        private LogType _LogType;
        //用户信息
        private UserInfoUtil _UserInfo;
        //操作模块ID
        private string _ModuleID;
        //操作状态
        private OperateStatus _OperateStatus;
        //登陆日志种类
        private LoginLogKind _LoginLogKind;
        //系统日志种类
        private SystemLogKind _SystemLogKind;
        //操作单据编号
        private string _LogObjectID;
        //操作对象
        private string _LogObjectName;
        //涉及关键元素
        private string _LogElement;
        //描述
        private string _LogRemark;

        /// <summary>
        /// 操作状态
        /// 1 成功
        /// 0 失败
        /// </summary>
        public enum OperateStatus
        {
            SUCCESS = 1,
            FAILED = 0
        }

        /// <summary>
        /// 日志类别
        /// 1 登陆日志
        /// 2 系统日志
        /// 3 操作日志
        /// </summary>
        public enum LogType
        {
            LOGIN = 1,
            SYSTEM = 2,
            PROCESS = 3
        }

        /// <summary>
        /// 登陆日志种类
        /// 登陆日志种类：登陆
        /// 登陆日志种类：注销
        /// </summary>
        public enum LoginLogKind
        {
            LOGIN_LOGIN = 1,
            LOGIN_LOGOUT = 2
        }

        /// <summary>
        /// 系统日志种类
        /// 1 系统日志种类：信息
        /// 2 系统日志种类：警告
        /// 3 系统日志种类：异常
        /// </summary>
        public enum SystemLogKind
        {
            SYSTEM_INFO = 1,
            SYSTEM_WARNING = 2,
            SYSTEM_ERROR = 3
        }

        /// <summary>
        /// 日志类别
        /// </summary>
        public LogType Type
        {
            get
            {
                return _LogType;
            }
            set
            {
                _LogType = value;
            }
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoUtil UserInfo
        {
            get
            {
                return _UserInfo;
            }
            set
            {
                _UserInfo = value;
            }
        }

        /// <summary>
        /// 操作状态
        /// </summary>
        public OperateStatus Status
        {
            get
            {
                return _OperateStatus;
            }
            set
            {
                _OperateStatus = value;
            }
        }

        /// <summary>
        /// 登陆日志种类
        /// </summary>
        public LoginLogKind LoginKind
        {
            get
            {
                return _LoginLogKind;
            }
            set
            {
                _LoginLogKind = value;
            }
        }

        /// <summary>
        /// 系统日志种类
        /// </summary>
        public SystemLogKind SystemKind
        {
            get
            {
                return _SystemLogKind;
            }
            set
            {
                _SystemLogKind = value;
            }
        }

        /// <summary>
        /// 操作模块ID
        /// </summary>
        public string ModuleID
        {
            get
            {
                return _ModuleID;
            }
            set
            {
                _ModuleID = value;
            }
        }

        /// <summary>
        /// 操作单据编号
        /// </summary>
        public string ObjectID
        {
            get
            {
                return _LogObjectID;
            }
            set
            {
                _LogObjectID = value;
            }
        }

        /// <summary>
        /// 操作对象
        /// </summary>
        public string ObjectName
        {
            get
            {
                return _LogObjectName;
            }
            set
            {
                _LogObjectName = value;
            }
        }

        /// <summary>
        /// 涉及关键元素
        /// </summary>
        public string Element
        {
            get
            {
                return _LogElement;
            }
            set
            {
                _LogElement = value;
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get
            {
                return _LogRemark;
            }
            set
            {
                _LogRemark = value;
            }
        }
    }
}

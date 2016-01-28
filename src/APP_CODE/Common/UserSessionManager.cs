/*
 written by dcyou at 09.08,04
 * email:dchunzi@163.com
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;

namespace XBase.Common
{
    /// <summary>
    /// SESSION状态实体
    /// </summary>
    [Serializable]
    public class UserSessionModel
    {
        /// <summary>
        /// SessionID
        /// </summary>
        private string sessionId;
        public string SessionId
        {
            get { return sessionId; }
            set { sessionId = value; }
        }

        /// <summary>
        /// UserID
        /// </summary>
        private string userID;
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }


        /// <summary>
        /// 最后次心跳时间
        /// </summary>
        private long lastLifeTime;
        public long LastLifeTime
        {
            get { return lastLifeTime; }
            set { lastLifeTime = value; }
        }


        /// <summary>
        /// 最后次活动时间
        /// </summary>
        private long lastActiveTime;
        public long LastActiveTime
        {
            get { return lastActiveTime; }
            set { lastActiveTime = value; }
        }


    }


    /// <summary>
    /// SESSION状态管理类
    /// </summary>
    public class UserSessionManager
    {
        /// <summary>
        /// 存储会话列表
        /// </summary>
        private static Hashtable UserSessionList
        {
            get {

                if (System.Web.HttpContext.Current.Application["UserSessionManager_ht"] == null)
                {
                    Hashtable ht = Hashtable.Synchronized(new Hashtable());

                    System.Web.HttpContext.Current.Application.Lock();
                    System.Web.HttpContext.Current.Application["UserSessionManager_ht"] = ht;
                    System.Web.HttpContext.Current.Application.UnLock();

                    return ht;
                }else{
                    return (Hashtable)System.Web.HttpContext.Current.Application["UserSessionManager_ht"];
                }

            }
        }

        public static string[] GetUserList()
        {
            string [] userList = new string[UserSessionList.Keys.Count];
            int i=0;
            foreach (string key in UserSessionList.Keys)
            {
                userList[i] = ((UserSessionModel)UserSessionList[key]).UserID;
                i++;
            }

            return userList;
        }


        //后台任务实例
        private static XBase.Common.BackgroundTask task = new XBase.Common.BackgroundTask();

        /// <summary>
        /// 最大活动时间间隔(秒)
        /// </summary>
        private static long maxActiveTime=60*60;
        public static long MaxActiveTime
        {
            get { return maxActiveTime; }
            set { maxActiveTime = value; }
        }


        /// <summary>
        /// 最大心跳时间间隔(秒)
        /// </summary>
        private static long maxLifeTime=60*2;
        public static long MaxLifeTime
        {
            get { return maxLifeTime; }
            set { maxLifeTime = value; }
        }


        /// <summary>
        /// 检查间隔(秒)
        /// </summary>
        private static long checkInterval = 60;
        public static long CheckInterval
        {
            get { return checkInterval; }
            set { checkInterval = value; }
        }


        /// <summary>
        /// 检查延时(秒)
        /// </summary>
        private static long checkDelay = 60;
        public static long CheckDelay
        {
            get { return checkDelay; }
            set { checkDelay = value; }
        }


        static UserSessionManager()
        {           
            if (ConfigurationManager.AppSettings["UserSessionMaxLife"] != null)
            {
                maxLifeTime = long.Parse(ConfigurationManager.AppSettings["UserSessionMaxLife"]);
            }
            if (ConfigurationManager.AppSettings["UserSessionMaxActiveTime"] != null)
            {
                maxActiveTime = long.Parse(ConfigurationManager.AppSettings["UserSessionMaxActiveTime"]);
            }
        }


        /// <summary>
        /// 删除一个USERSESSION对象
        /// </summary>
        /// <param name="sessionID"></param>
        public static void Remove(string sessionID)
        {
            if (UserSessionList.ContainsKey(sessionID))
            {
                UserSessionList.Remove(sessionID);
            }
        }


        /// <summary>
        /// 添加一个USERSESSION对象
        /// </summary>
        /// <param name="userSession"></param>
        public static void Add(UserSessionModel userSession)
        {
            if (UserSessionList.ContainsKey(userSession.SessionId))
            {
                throw new Exception("已经存在的SESSIONID");
            }
            UserSessionList.Add(userSession.SessionId, userSession);
        }


        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="sessionID"></param>
        public static void Life(string sessionID)
        {
            UserSessionModel userSession = (UserSessionModel)UserSessionList[sessionID];
            if (userSession != null)
            {
                userSession.LastLifeTime = DateTime.Now.Ticks;
                UserSessionList[sessionID] = userSession;
            }
        }

        /// <summary>
        /// 活动
        /// </summary>
        /// <param name="sessionID"></param>
        public static void Active(string sessionID)
        {
            UserSessionModel userSession = (UserSessionModel)UserSessionList[sessionID];
            if (userSession != null)
            {
                userSession.LastActiveTime = DateTime.Now.Ticks;
                UserSessionList[sessionID] = userSession;
            }
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool Exists(string userID)
        {
            UserSessionManager.Check(null);
            
            foreach (string key in UserSessionList.Keys)
            {
                UserSessionModel usersession = (UserSessionModel)UserSessionList[key];
                if (usersession == null)
                {
                    continue;
                }
                if (usersession.UserID.ToLower() == userID.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool ExistsBySessionID(string sessionID)
        {
            //UserSessionManager.Check(null);

            return UserSessionList.ContainsKey(sessionID);
        }
        

        /// <summary>
        /// 检查列表(清除无效记录)
        /// </summary>
        internal static void Check(object o)
        {
            lock (UserSessionList)
            {
            recheck:
                foreach (string key in UserSessionList.Keys)
                {
                    UserSessionModel userSession = (UserSessionModel)UserSessionList[key];
                    if (userSession.LastLifeTime + maxLifeTime * 10000 * 1000 <= DateTime.Now.Ticks)
                    {
                        UserSessionList.Remove(key);

                        goto recheck;
                    }


                    if (userSession.LastActiveTime + maxActiveTime * 10000 * 1000 <= DateTime.Now.Ticks)
                    {
                        UserSessionList.Remove(key);
                        goto recheck;
                    }

                }
            }
        }

        /// <summary>
        /// 开始检查(暂不采用)
        /// </summary>
        public static void Start()
        {
            task.Run(UserSessionManager.Check, null, 1000 * (int)UserSessionManager.checkDelay, 1000 * (int)UserSessionManager.checkInterval);
        }
    }
}

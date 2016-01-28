/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2008.12.30
 * 描    述： Session管理类
 * 修改日期： 2009.01.10
 * 版    本： 0.5.0
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web.SessionState;
using System.Web;
using System.Configuration;

namespace XBase.Common
{
    /// <summary>
    /// 类名：SessionUtil
    /// 描述：提供与Session相关的一些方法
    /// 
    /// 作者：吴志强
    /// 创建时间：2008/12/30
    /// 最后修改时间：2008/12/30
    /// </summary>
    ///
    public class SessionUtil : IDictionary, ICollection
    {
        

        private static SessionUtil _SessionUtil;

        /// <summary>
        /// 获得Session
        /// </summary>
        public static SessionUtil Session
        {
            get
            {
                if (_SessionUtil == null)
                {
                    _SessionUtil = new SessionUtil();
                }
                return _SessionUtil;
            }
        }

        //private HttpSessionState _Session;
        //private SessionUtil()
        //{
        //    _Session = HttpContext.Current.Session;
        //}


        private HttpSessionState _Session
        {
            get { return HttpContext.Current.Session; }
        }


        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取存储在会话状态集合中所有值的键的集合。
        /// </summary>
        public ICollection Keys
        {
            get { return _Session.Keys; }
        }

        /// <summary>
        /// 返回一个枚举数，可用来读取当前会话中所有会话状态的变量名称。
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            return _Session.GetEnumerator();
        }

        /// <summary>
        /// 判断Session中是否包含了给定的key。
        /// </summary>
        public bool Contains(object key)
        {
            return _Session[(String)key] != null;
        }

        /// <summary>
        /// 向会话状态集合添加一个新项。
        /// </summary>
        public void Add(object key, object value)
        {
            _Session.Add((String)key, value);
        }

        /// <summary>
        /// 从会话状态集合中移除所有的键和值。
        /// </summary>
        public void Clear()
        {
            _Session.Clear();
        }

        /// <summary>
        /// 获取并设置在会话状态提供程序终止会话之前各请求之间所允许的时间（以分钟为单位）。
        /// </summary>
        public int TimeOut
        {
            get { return _Session.Timeout; }
            set { _Session.Timeout = value; }
        }
        
        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取会话状态集合中的项数。
        /// </summary>
        public int Count
        {
            get { return _Session.Count; }
        }

        /// <summary>
        /// 获取一个对象，该对象可用于同步对会话状态值的集合的访问。
        /// </summary>
        public object SyncRoot
        {
            get { return _Session.SyncRoot; }
        }

        /// <summary>
        /// 获取一个值，该值指示对会话状态值的集合的访问是否是同步（线程安全）的。
        /// </summary>
        public bool IsSynchronized
        {
            get { return _Session.IsSynchronized; }
        }

        /// <summary>
        /// 删除会话状态集合中的项。
        /// </summary>
        public void Remove(object key)
        {
            _Session.Remove((String)key);
        }

        /// <summary>
        /// 从会话状态集合中移除所有的键和值。
        /// </summary>
        public void RemoveAll()
        {
            _Session.RemoveAll();
        }

        /// <summary>
        /// 取消当前会话。
        /// </summary>
        public void Abandon()
        {
            _Session.Abandon();
        }

        public ICollection Values
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// 获取一个值，该值指示会话是否为只读。
        /// </summary>
        public bool IsReadOnly
        {
            get { return _Session.IsReadOnly; }
        }

        /// <summary>
        /// 获取一个值，该值指示会话是否为固定大小的。
        /// </summary>
        public bool IsFixedSize
        {
            get { return false; }
        }

        public object this[object key]
        {
            get { return _Session[(String)key]; }
            set
            {
                _Session[(String)key] = value;
            }
        }
    }
}


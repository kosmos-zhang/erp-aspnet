/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009.01.06
 * 描    述： XML文件读取提示信息操作类
 * 修改日期： 2009.01.10
 * 版    本： 0.5.0
 ***********************************************/

using System;
using System.Xml;
using System.Configuration;
using System.IO;

namespace XBase.Common
{
    /// <summary>
    /// 类名：MessageUtil
    /// 描述：从XML文件中读取提示信息。
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/01/06
    /// 最后修改时间：2009/01/06
    /// </summary>
    public class MessageUtil
    {
        //信息文件路径KEY
        private const string MESSAGE_FILE_PATH_KEY = "MESSAGE_FILE_PATH";
        //信息文件路径名
        private static string messageFilePath = null;

        /// <summary>
        /// 从文件中获取相应的信息内容
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        /// <param name="messageID">信息ID</param>
        /// <param name="param">需要替换的参数数组</param>
        /// <returns>返回信息的内容</returns>
        private static string GetMessageFromFile(string moduleID, string messageID, params string[] param)
        {
            string messageXML = null;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                //从配置文件中读取信息文件名
                if (messageFilePath == null)
                {
                    messageFilePath = ConfigurationManager.AppSettings[MESSAGE_FILE_PATH_KEY];
                    //如果是相对路径则把工程路径添加上去
                    if (messageFilePath.IndexOf(":") == -1)
                    {
                        //获得工程路径
                        String currentDomainPath = System.AppDomain.CurrentDomain.BaseDirectory;
                        messageFilePath = Path.Combine(currentDomainPath, messageFilePath);
                    }
                }
                //初始化XML文本文件
                xmlDoc.Load(messageFilePath);
                //获取XML文本跟节点
                XmlElement xmlRoot = xmlDoc.DocumentElement;
                //通过模块ID和信息ID获取XML中的信息内容
                messageXML = xmlRoot.SelectSingleNode(moduleID + "/" + messageID).InnerText;
                //需要替换参数时，替换参数
                if (param != null && param.Length > 0)
                {
                    //对参数进行遍历
                    for (int i = 0; i < param.Length; i++)
                    {
                        //替换参数
                        messageXML = messageXML.Replace("{" + i.ToString() + "}", param[i]);
                    }
                }
            }
            catch (Exception ex) {
                return ex.ToString();
            }
            return messageXML;
        }

        /// <summary>
        /// 从文件中获取相应的信息内容
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        /// <param name="messageID">信息ID</param>
        /// <returns>返回信息的内容</returns>
        public static string GetMessage(string moduleID, string messageID)
        {
            return GetMessageFromFile(moduleID, messageID, null);
        }

        /// <summary>
        /// 从文件中获取相应的信息内容
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        /// <param name="messageID">信息ID</param>
        /// <param name="param">需要替换的参数数组</param>
        /// <returns>返回信息的内容</returns>
        public static string GetMessage(string moduleID, string messageID, params string[] param)
        {
            return GetMessageFromFile(moduleID, messageID, param);
        }
    }
}

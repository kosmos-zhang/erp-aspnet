using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using System.Xml;

namespace XBase.Business.KnowledgeCenter
{
    public class SubscribeTimerWorkerLog
    {
        private static SubscribeTimerWorkerLog _instance;
        public static SubscribeTimerWorkerLog GetInstance()
        {
            if (_instance == null)
                _instance = new SubscribeTimerWorkerLog();
            return _instance;
        }

        /*
         <root>
              <record>
                <startdate></startdate>
                <enddate></enddate>
              </record>  
         </root>
         */
        private string path = AppDomain.CurrentDomain.BaseDirectory + "\\SubscribeTimerWorkerLog.xml";
        private XmlDocument logDoc;
        private SubscribeTimerWorkerLog()
        {
            logDoc = new XmlDocument();
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><root></root>";

            if (System.IO.File.Exists(path))
            {
                try
                {
                    logDoc.Load(path);
                }
                catch
                {
                    logDoc.LoadXml(xml);
                }
            }
            else
            {
                logDoc.LoadXml(xml);
            }

        }

        /// <summary>
        /// 写入新的记录
        /// </summary>
        /// <param name="dtStart">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        public void Write(DateTime dtStart, DateTime dtEnd)
        {
            XmlElement ele = logDoc.CreateElement("Record");
            XmlAttribute elea1 = logDoc.CreateAttribute("StartDate");
            XmlAttribute elea2 = logDoc.CreateAttribute("EndDate");
            ele.Attributes.Append(elea1);
            ele.Attributes.Append(elea2);

            elea1.Value = dtStart.ToString();
            elea2.Value = dtEnd.ToString();

            logDoc.DocumentElement.AppendChild(ele);

            logDoc.Save(path);
        }

        /// <summary>
        /// 取最新的一条记录的完成时间
        /// </summary>
        /// <returns></returns>
        public DateTime ReadLastEndDate()
        {
            int count = logDoc.DocumentElement.ChildNodes.Count;
            if (count == 0)
                return DateTime.Now.AddYears(-10);

            XmlNode node = logDoc.DocumentElement.ChildNodes[count - 1];
            string dtstr = node.Attributes["EndDate"].Value;
            DateTime dt = DateTime.Parse(dtstr);
            return dt;
        }
    }


    public class SubscribeTimerWorker
    {
        //后台订阅发送服务的启动延时（单位 秒）
        private static int SubscribeTimerDelay
        {
            get
            {
                string val = System.Web.Configuration.WebConfigurationManager.AppSettings["SubscribeTimerDelay"];
                if (val + "" == "")
                {
                    throw new Exception("后台订阅发送服务的启动延时 未配置");
                }
                return int.Parse(val);
            }
        }

        //后台订阅发送服务的执行间隔（单位 秒）
        private static int SubscribeTimerInterval
        {
            get
            {
                string val = System.Web.Configuration.WebConfigurationManager.AppSettings["SubscribeTimerInterval"];
                if (val + "" == "")
                {
                    throw new Exception("后台订阅发送服务的执行间隔 未配置");
                }
                return int.Parse(val);
            }
        }


        private static XBase.Data.KnowledgeCenter.KnowledgeWarehouse dal = new XBase.Data.KnowledgeCenter.KnowledgeWarehouse();


        /// <summary>
        /// 执行程式
        /// </summary>
        /// <param name="state"></param>
        public void DoWork(object state)
        {
            //SubscribeTimerWorker obj = (SubscribeTimerWorker)state;

            //记录开始时间
            DateTime dtStart = DateTime.Now;

            //**** 读取 上次处理完成的时间
            DateTime lastEndDate = SubscribeTimerWorkerLog.GetInstance().ReadLastEndDate();

            //do work
            dal.SubscribeSend(lastEndDate);

            //记录结束时间
            DateTime dtEnd = DateTime.Now;

            //记录
            SubscribeTimerWorkerLog.GetInstance().Write(dtStart, dtEnd);
        }


        //后台任务实例
        private static XBase.Common.BackgroundTask task = new XBase.Common.BackgroundTask();
        /// <summary>
        /// 订阅发送
        /// </summary>
        public static void Start()
        {
            SubscribeTimerWorker worker = new SubscribeTimerWorker();
            task.Run(worker.DoWork, worker, SubscribeTimerDelay * 1000, SubscribeTimerInterval * 1000);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace XBase.Business.Decision
{
   public class DataStatTimeWorkerMM
    {
        private string path = AppDomain.CurrentDomain.BaseDirectory + "\\DataStatTimerWorkerMMLog.xml";
        private XmlDocument logDoc;
        private static XBase.Data.Decision.DataStatWarehouse dal = new XBase.Data.Decision.DataStatWarehouse();
        private static System.Threading.Timer timer = null;
        private static bool flag = true;
        private static string MonthOfDay
        {
            get
            {
                string val = System.Web.Configuration.WebConfigurationManager.AppSettings["StatTimerMonth"];
                if (val + "" == "")
                {
                    throw new Exception("后台订阅发送服务的启动执行时间  未配置");
                }
                return val;
            }
        }
        
        private static long DataSubscribeMMTimerInterval
        {
            get
            {
                string val = System.Web.Configuration.WebConfigurationManager.AppSettings["DataSubscribeMMTimerInterval"];
                if (val + "" == "")
                {
                    throw new Exception("后台订阅发送服务的启动延时 未配置");
                }
                return Convert.ToInt64(val);
            }
        }
        public static void Start()
        {
            DataStatTimeWorkerMM work = new DataStatTimeWorkerMM();
            timer = new System.Threading.Timer(new System.Threading.TimerCallback(work.DoWork));
            timer.Change(10, DataSubscribeMMTimerInterval);
        }

        /// <summary>
        /// 执行程式
        /// </summary>
        /// <param name="state"></param>
        public void DoWork(object state)
        {
            if (flag)
            {
                flag = false;
                //记录开始时间
                DateTime dtStart = DateTime.Now;
                if (dtStart.ToString("dd") == MonthOfDay)
                {
                    LoadXml();
                    //**** 读取 上次处理完成的时间
                    DateTime lastEndDate = ReadLastEndDate();
                    //do work
                    dal.DataStatSend(lastEndDate, "2");
                    //记录结束时间
                    DateTime dtEnd = DateTime.Now;
                    //记录
                    Write(dtStart, dtEnd);
                }
                flag = true;
            }
        }


        public void LoadXml()
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
            try
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
            catch
            {
            	
            }
           
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
}

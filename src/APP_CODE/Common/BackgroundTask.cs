using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace XBase.Common
{
    /// <summary>
    /// 工作者程式
    /// </summary>
    /// <param name="o">The state object 0 is the Timer object.</param>
    public delegate void WorkItem(object o);

    public class BackgroundTask
    {        

        private System.Threading.Timer timer = null;

        public void Run(WorkItem workProc,object o, int delay, int interval)
        {
            System.Threading.TimerCallback t = new System.Threading.TimerCallback(workProc);
            timer = new System.Threading.Timer(t, o, delay, interval); 
           
        }


         
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace XBase.Common
{
    public class SessionTimeoutException : Exception
    {
        private int _code;
        public int Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public SessionTimeoutException(string messagae, int code)
            : base(messagae)
        {
            _code = code;
        }

    }

    /// <summary>
    /// 主要实现全局异常处理
    /// written by youdc at 2009.04.14
    /// </summary>
    public class HttpModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {            
            app.Error += new EventHandler(this.Application_OnError);
            app.PreRequestHandlerExecute += new EventHandler(OnPreRequestHandlerExecute);
        }
        public virtual void Dispose() { }

        private void OnPreRequestHandlerExecute(object sender, EventArgs e)
        {
            // Ensure we have a HttpContext
            if (HttpContext.Current == null)
            {
                //Debug.WriteLine("No current http context");
                return;
            }
            if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().EndsWith("error.aspx"))
            {
                return;
            }
            if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().EndsWith("userlifehandler.ashx"))
            {
                return;
            }
            if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().EndsWith("login.ashx"))
            {
                return;
            }
            if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().EndsWith("login.aspx"))
            {
                return;
            }
            if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().EndsWith("checkcode.aspx"))
            {
                return;
            }
            if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().EndsWith("logout.aspx"))
            {
                return;
            }

            if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().EndsWith("tasklist.ashx"))
                return;
            if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().EndsWith("desktop.ashx"))
                return;
            if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().EndsWith("inputbox.ashx"))
                return;
            if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().EndsWith("notice.ashx"))
                return;
            if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().EndsWith("changepsd.ashx"))
                return;
            if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().EndsWith("ChartAxd.axd"))
                return;
            // Ensure we have a current session
            if (HttpContext.Current.Session == null)
            {
                //Debug.WriteLine("No current session");
                return;
            }

            //if (!XBase.Common.UserSessionManager.ExistsBySessionID(HttpContext.Current.Session.SessionID))
            //{
            //    throw new SessionTimeoutException("客户端长时间未活动并且相同账号在另一个地点登陆,当前账号自动退出", 0);
            //}

         //   XBase.Common.FileUtil.SaveTextToFileEnd("我再活动,当前的SessionID:" + HttpContext.Current.Session.SessionID + "，时间：" + DateTime.Now.ToString() + "\r\n", "d:\\2.txt");
            XBase.Common.UserSessionManager.Active(HttpContext.Current.Session.SessionID);
             
        }

        private void Application_OnError(Object source, EventArgs e)
        { 
            HttpContext context = HttpContext.Current;
            Exception ex = context.Server.GetLastError().GetBaseException();

            //记录异常日志
            XgLoger.Log(ex);
         
            if (context.Request.Url.AbsolutePath.ToLower().EndsWith(".ashx"))
            {
                context.Response.Clear();
                //Handler.ashx 抛出的异常
                if (ex is SessionTimeoutException)
                {
                    //Handler.ashx 抛出的 SessionTimeoutException 异常                    
                    context.Response.Write("{result:false,data:\"" + ex.Message + "\"}");                   
                }
                else
                {
                    //Handler.ashx 抛出的 未定义 异常                    
                    context.Response.Write("{result:false,data:\"" + ex.Message + "\"}");  
                }
                context.Response.End();

            }
            else {
                if (context.Request.Url.AbsolutePath.ToLower().IndexOf("/error.aspx") != -1)
                {
                    return;
                }
                //.aspx 抛出的异常
                context.Response.Clear();
                context.Response.Redirect("/error.aspx?msg="+context.Server.UrlEncode(ex.Message));
            }
			
		
        }
    }
}

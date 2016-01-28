<%@ Application Language="C#" %>

<script RunAt="server">
    static Cache _cache = null;
    
    
    void Application_Start(object sender, EventArgs e)
    {
        _cache = Context.Cache; // Save reference for later 
        RefreshCache(null, null, 0);
        if (ConfigurationSettings.AppSettings["SendMessageSwitch"].ToString() == "true")
        {
            XBase.Business.SendNotice.Start();
            XBase.Business.Decision.DataStatTimeWorkerDD.Start(); //数据订阅日发送
            XBase.Business.Decision.DataStatTimeWorkerMM.Start(); //数据订阅月发送
        }

        //启动 订阅发送程式
        XBase.Business.KnowledgeCenter.SubscribeTimerWorker.Start();
       
    }
    /// <summary> 
    /// 刷新缓存信息
    /// </summary>
    static void RefreshCache(string key, object item, CacheItemRemovedReason reason)
    {

        //new CacheDependency(DepFile) //部署在服务器上在调用
        string DepFile = ConfigurationSettings.AppSettings["DependencyFile"].ToString();
        System.Data.DataTable PubParmsDt = null;
        System.Data.DataTable CompanyDt = null;
        System.Data.DataTable CompanyOpenSerDt = null;
        PubParmsDt = XBase.Business.SystemManager.SystemBus.GetPubParms();
        CompanyDt = XBase.Business.SystemManager.SystemBus.GetCompanyParms();
        CompanyOpenSerDt = XBase.Business.SystemManager.SystemBus.GetCompanyOpenSevParms();
        _cache.Insert(
        "PubParms",
        PubParmsDt,
        null,
        Cache.NoAbsoluteExpiration,
        Cache.NoSlidingExpiration,
        CacheItemPriority.Default,
        new CacheItemRemovedCallback(RefreshCache)
        );

        _cache.Insert(
       "CompanyInfo",
       CompanyDt,
       null,
       Cache.NoAbsoluteExpiration,
       Cache.NoSlidingExpiration,
       CacheItemPriority.Default,
       new CacheItemRemovedCallback(RefreshCache)
       );

        _cache.Insert(
        "CompanyOS",
        CompanyOpenSerDt,
        null,
        Cache.NoAbsoluteExpiration,
        Cache.NoSlidingExpiration,
        CacheItemPriority.Default,
        new CacheItemRemovedCallback(RefreshCache)
        );
    }



    void Application_End(object sender, EventArgs e)
    {
        //在应用程序关闭时运行的代码
        _cache.Remove("PubParms");
        _cache.Remove("CompanyInfo");
        _cache.Remove("CompanyOS");
    }

    void Application_Error(object sender, EventArgs e)
    {
        //在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e)
    {
        //在新会话启动时运行的代码
    }

    void Session_End(object sender, EventArgs e)
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。
        //获取用户信息
        XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
        //移除自定义的Session的值
        XBase.Common.SessionUtil.Session.Abandon();      
        //移除内存中已登录信息
        _cache.Remove(userInfo.UserID);
    }
       
</script>


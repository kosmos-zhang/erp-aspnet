/***********************************************************************
 * 
 * Module Name:XBase.Business.Office.SystemManager.AdversaryInfoBus.cs
 * Current Version: 1.0 
 * Creator: 周军
 * Auditor:2009-08-13
 * End Date:
 * Description: 日志处理
 * Version History:
 ***********************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.SellManager;
using XBase.Data.Office.SellManager;
using XBase.Common;
using System.Data;
using XBase.Model.Common;
using XBase.Business.Common;
using XBase.Data.Common;


namespace XBase.Business.Office.SupplyChain
{
    public class SellLogCommon
    {
        #region
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">登陆用户信息</param>
        /// <param name="ex">异常信息</param>
        /// <param name="Type">日志类型</param>
        /// <param name="SystemKind">系统日志类型</param>
        /// <param name="ModuleID">模块ID</param>
        public static void WriteSystemLog(Exception ex, LogInfo.LogType Type, LogInfo.SystemLogKind SystemKind, string ModuleID)
        {
            /* 
             * 出现异常时，输出系统日志到文本文件 
             * 考虑出现异常情况比较少，尽管一个方法可能多次异常，
             *      但还是考虑将异常日志的变量定义放在catch里面
             */
            //定义变量
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            LogInfo logSys = new LogInfo();
            //设置日志类型 需要指定为系统日志
            logSys.Type = Type;
            //指定系统日志类型 出错信息
            logSys.SystemKind = SystemKind;
            //指定登陆用户信息
            logSys.UserInfo = userInfo;
            //设定模块ID
            logSys.ModuleID = ModuleID;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion

        #region 设置操作日志内容

        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <param name="ApplyNo">单据编号</param>
        /// <param name="ModuleID">模块ID</param>
        /// <param name="ObjectName">操作对象(相关表)</param>
        /// <param name="remark">备注,设置操作成功标识(操作成功或失败)</param>
        /// <param name="Element">涉及关键元素（操作名称） 这个需要根据每个页面具体设置</param>
        /// <returns></returns>
        public static void InsertLog(string OrderNo, string ModuleID, string ObjectName, string remark, string Element)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ModuleID;
            //设置操作日志类型 修改
            logModel.ObjectName = ObjectName;
            //操作对象
            logModel.ObjectID = OrderNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = Element;
            //设置操作成功标识
            logModel.Remark = remark;


            //插入日志
            LogDBHelper.InsertLog(logModel);
        }
        #endregion
    }
}

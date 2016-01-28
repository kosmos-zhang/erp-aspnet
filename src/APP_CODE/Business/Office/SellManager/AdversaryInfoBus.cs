/***********************************************************************
 * 
 * Module Name:XBase.Business.Office.SystemManager.AdversaryInfoBus.cs
 * Current Version: 1.0 
 * Creator: 周军
 * Auditor:2009-01-12
 * End Date:
 * Description: 竞争对手业务层处理
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

namespace XBase.Business.Office.SellManager
{
    /// <summary>
    /// 竞争对手业务层
    /// </summary>
    public class AdversaryInfoBus
    {
        /// <summary>
        /// 添加单据
        /// </summary>
        /// <param name="sellChanceModel">销售机会表实体</param>
        /// <param name="sellChancePushModel">销售阶段表实体</param>
        /// <returns>是否添加成功</returns>
        public static bool InsertOrder(AdversaryInfoModel adversaryInfoModel, List<AdversaryDynamicModel> adversaryDynamicModelList)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                isSucc = AdversaryInfoDBHelper.InsertOrder(adversaryInfoModel, adversaryDynamicModelList);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            LogInfoModel logModel = InitLogInfo(adversaryInfoModel.CustNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }

        /// <summary>
        /// 跟新单据单据
        /// </summary>
        /// <param name="sellChanceModel">销售机会表实体</param>
        /// <param name="sellChancePushModel">销售阶段表实体</param>
        /// <returns>是否添加成功</returns>
        public static bool UpdateOrder(AdversaryInfoModel adversaryInfoModel, List<AdversaryDynamicModel> adversaryDynamicModelList)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                isSucc = AdversaryInfoDBHelper.UpdateOrder(adversaryInfoModel, adversaryDynamicModelList);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            LogInfoModel logModel = InitLogInfo(adversaryInfoModel.CustNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }


        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="orderNos"></param>
        /// <returns></returns>
        public static bool DelOrder(string orderNos)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                isSucc = AdversaryInfoDBHelper.DelOrder(orderNos);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            LogInfoModel logModel = InitLogInfo(orderNos);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
           
        }

        /// <summary>
        /// 获取单据列表 
        /// </summary>
        /// <param name="sellContractModel">sellContractModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(AdversaryInfoModel adversaryInfoModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return AdversaryInfoDBHelper.GetOrderList(adversaryInfoModel, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 获取单据明细信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable GetOrderDetail(string orderNo)
        {
            return AdversaryInfoDBHelper.GetOrderDetail(orderNo);
        }

        /// <summary>
        /// 获取单据主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            return AdversaryInfoDBHelper.GetOrderInfo(orderID);
        }

        /// <summary>
        /// 获取竞争对手类别
        /// </summary>
        /// <param name="ComPanyCD"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static DataTable GetAdversaryType()
        {
            return AdversaryInfoDBHelper.GetAdversaryType();
        }

        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            return AdversaryInfoDBHelper.GetRepOrder(OrderNo);
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderDetail(string OrderNo)
        {
            return AdversaryInfoDBHelper.GetRepOrderDetail(OrderNo);
        }



        #region 输出系统日志
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">异常信息</param>
        private static void WriteSystemLog(UserInfoUtil userInfo, Exception ex)
        {
            /* 
             * 出现异常时，输出系统日志到文本文件 
             * 考虑出现异常情况比较少，尽管一个方法可能多次异常，
             *      但还是考虑将异常日志的变量定义放在catch里面
             */
            //定义变量
            LogInfo logSys = new LogInfo();
            //设置日志类型 需要指定为系统日志
            logSys.Type = LogInfo.LogType.SYSTEM;
            //指定系统日志类型 出错信息
            logSys.SystemKind = LogInfo.SystemLogKind.SYSTEM_ERROR;
            //指定登陆用户信息
            logSys.UserInfo = userInfo;
            //设定模块ID
            logSys.ModuleID = ConstUtil.MODULE_ID_ADVERSARYINFO_ADD;
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
        /// <returns></returns>
        private static LogInfoModel InitLogInfo(string ApplyNo)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_ADVERSARYINFO_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_ADVERSARYINFO;
            //操作对象
            logModel.ObjectID = ApplyNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion
    }

}

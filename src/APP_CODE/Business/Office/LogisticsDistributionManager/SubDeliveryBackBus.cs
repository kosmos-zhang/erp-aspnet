using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Common;
using XBase.Model.Office.LogisticsDistributionManager;
namespace XBase.Business.Office.LogisticsDistributionManager
{
    public class SubDeliveryBackBus
    {
        #region 读取分店存量
        public static DataTable GetSubStorageProduct(SubStorageProductModel model, int PageIndex, int PageSize, string OrderBy, ref int PageCount, Hashtable htPara)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryBackDBHelper.GetSubStorageProduct(model, PageIndex, PageSize, OrderBy, ref PageCount, htPara);
        }

        /*条码扫描使用*/
        public static DataTable GetSubStorageProduct(SubStorageProductModel model)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryBackDBHelper.GetSubStorageProduct(model);
        }
        #endregion

        #region 保存配送退货单及其明细
        public static string AddSubDeliveryBack(SubDeliveryBack model, List<SubDeliveryBackDetail> modelList, Hashtable htExtAttr)
        {
            //定义返回变量
            string res = string.Empty;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取当前用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //执行操作
            try
            {
                //执行操作
                res = XBase.Data.Office.LogisticsDistributionManager.SubDeliveryBackDBHelper.AddSubDeliveryBack(model, modelList, htExtAttr);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res != string.Empty)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.BackNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT; ;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
        }
        #endregion

        #region 更新状态
        public static bool UpdateStatus(SubDeliveryBack model, int stype)
        {
            //定义返回变量
            bool res = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取当前用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //执行操作
            try
            {
                //执行操作
                res = XBase.Data.Office.LogisticsDistributionManager.SubDeliveryBackDBHelper.UpdateStatus(model, stype);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.BackNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空

            string msg = string.Empty;
            switch (stype)
            {
                case 1:
                    /*确认*/
                    msg = ConstUtil.LOG_PROCESS_CONFIRM;
                    break;
                case 2:
                    /*结单*/
                    msg = ConstUtil.LOG_PROCESS_COMPLETE;
                    break;
                case 3:
                    /*取消结单*/
                    msg = ConstUtil.LOG_PROCESS_CONCELCOMPLETE;
                    break;
                case 4:
                    /*取消确认*/
                    msg = ConstUtil.LOG_PROCESS_UNCONFIRM;
                    break;
            }
            logModel.Element = msg;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
        }
        #endregion

        #region 更新配送退货单及其明细
        public static bool UpdateSubDeliverySend(SubDeliveryBack model, List<SubDeliveryBackDetail> modelList, Hashtable htExtAttr)
        {
            //定义返回变量
            bool res = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取当前用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //执行操作
            try
            {
                //执行操作
                res = XBase.Data.Office.LogisticsDistributionManager.SubDeliveryBackDBHelper.UpdateSubDeliverySend(model, modelList, htExtAttr);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.BackNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
        }
        #endregion

        #region 读取退货单列表
        public static DataTable GetSubDeliveryBackList(Hashtable htPara, int PageIndex, int PageSize, string OrderBy, string EFIndex, string EFDesc, ref int TotalCount)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryBackDBHelper.GetSubDeliveryBackList(htPara, PageIndex, PageSize, OrderBy, EFIndex, EFDesc, ref  TotalCount);
        }

        /*不分页*/
        public static DataTable GetSubDeliveryBackList(Hashtable htPara, string OrderBy, string EFIndex, string EFDesc)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryBackDBHelper.GetSubDeliveryBackList(htPara, OrderBy, EFIndex, EFDesc);
        }
        #endregion

        #region 删除
        public static bool DelSubDeliverySend(string[] IDList)
        {
            bool flag = false;

            //string res = string.Empty;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取当前用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //执行操作
            try
            {
                //执行操作
                flag = XBase.Data.Office.LogisticsDistributionManager.SubDeliveryBackDBHelper.DelSubDeliveryBack(IDList);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (flag)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            string idInfo = string.Empty;
            foreach (string str in IDList)
            {
                idInfo += str + ",";
            }
            //操作日志
            LogInfoModel logModel = InitLogInfo(idInfo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return flag;

        }
        #endregion

        #region 读取配送退还单信息
        public static DataTable GetSubDeliveryBackInfo(SubDeliveryBack model)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryBackDBHelper.GetSubDeliveryBackInfo(model);
        }


        /*打印使用*/
        public static DataTable GetSubDeliveryBackInfoPrint(SubDeliveryBack model)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryBackDBHelper.GetSubDeliveryBackInfoPrint(model);
        }
        #endregion

        #region 读取配送单明细信息
        public static DataTable GetSubDeliveryBackDetail(SubDeliveryBackDetail model)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryBackDBHelper.GetSubDeliveryBackDetail(model);
        }
        #endregion

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
            logSys.ModuleID = ConstUtil.MODULE_ID_SUBDELIVERYBACK_SAVE;
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
        private static LogInfoModel InitLogInfo(string InNo)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_SUBDELIVERYBACK_SAVE;
            //设置操作日志类型 修改
            logModel.ObjectName = "SubDeliveryBack";
            //操作对象
            logModel.ObjectID = InNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        #region 分店出库
        public static bool SubDeliveryBackOut(SubDeliveryBack model)
        {
            //定义返回变量
            bool res = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取当前用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //执行操作
            try
            {
                //执行操作
                res = XBase.Data.Office.LogisticsDistributionManager.SubDeliveryBackDBHelper.SubDeliveryBackOut(model);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.BackNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
        }
        #endregion

        #region 验货入库
        public static bool SubDeliveryBackIn(SubDeliveryBack model, List<SubDeliveryBackDetail> modelList)
        {
            //定义返回变量
            bool res = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取当前用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //执行操作
            try
            {
                //执行操作
                res = XBase.Data.Office.LogisticsDistributionManager.SubDeliveryBackDBHelper.SubDeliveryBackIn(model, modelList);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.BackNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
        }
        #endregion
    }
}

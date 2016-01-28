/**********************************************
 * 类作用：   退料事务层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/30
 ***********************************************/

using System;
using System.Collections.Generic;
using XBase.Model.Office.ProductionManager;
using XBase.Data.Office.ProductionManager;
using XBase.Common;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Business.Common;
using System.Text;
using System.Collections;
using XBase.Model.Common;
using XBase.Data.Common;



namespace XBase.Business.Office.ProductionManager
{
    public class BackMaterialBus
    {
        #region 退料单插入
        /// <summary>
        /// 退料单插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertBackMaterial(BackMaterialModel model, Hashtable ht, out string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ID = "0";
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.BackNo, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;

                succ = BackMaterialDBHelper.InsertBackMaterial(model,ht, loginUserID, out ID);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, 0, ex);
                return false;
            }

        }
        #endregion

        #region 退料单详细信息
        /// <summary>
        /// 退料单详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetBackInfo(BackMaterialModel model)
        {
            try
            {
                return BackMaterialDBHelper.GetBackInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 退料单明细详细信息
        /// <summary>
        /// 退料单明细详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetBackDetailInfo(BackMaterialModel model)
        {
            try
            {
                return BackMaterialDBHelper.GetBackDetailInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 更新退料单信息
        /// <summary>
        /// 更新退料单信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UpdateID"></param>
        /// <returns></returns>
        public static bool UpdateBackMaterialInfo(BackMaterialModel model,Hashtable ht)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.BackNo, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

                succ = BackMaterialDBHelper.UpdateBackMaterialInfo(model,ht, loginUserID);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, 0, ex);
                return false;
            }

        }

        #endregion

        #region 通过检索条件查询退料单信息
        /// <summary>
        /// 通过检索条件查询退料单信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetBackMaterialListBycondition(BackMaterialModel model, string ReceiveDateStart, string ReceiveDateEnd, int BillTypeFlag, int BillTypeCode, int FlowStatus, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return BackMaterialDBHelper.GetBackMaterialListBycondition(model, ReceiveDateStart, ReceiveDateEnd, BillTypeFlag, BillTypeCode, FlowStatus, EFIndex, EFDesc, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除退料单
        /// <summary>
        /// 删除退料单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteBackMaterial(string ID, string CompanyCD)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return false;
            }
            if (string.IsNullOrEmpty(CompanyCD))
            {
                CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            bool isSucc = BackMaterialDBHelper.DeleteBackMaterial(ID, CompanyCD);
            //定义变量
            string remark;
            //成功时
            if (isSucc)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //获取删除的编号列表
            string[] noList = ID.Split(',');
            //遍历所有编号，登陆操作日志
            for (int i = 0; i < noList.Length; i++)
            {
                //获取编号
                string no = noList[i];
                //替换两边的 '
                no = no.Replace("'", string.Empty);

                //操作日志
                LogInfoModel logModel = InitLogInfo("退料单ID：" + no, 1);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                //设置操作成功标识
                logModel.Remark = remark;

                //登陆日志
                LogDBHelper.InsertLog(logModel);
            }
            return isSucc;
        }
        #endregion

        #region 收料
        /// <summary>
        /// 收料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ReceiveBackMaterial(BackMaterialModel model, out string reason)
        {
            try
            {
                return BackMaterialDBHelper.ReceiveBackMaterial(model, out reason);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 确认或结单
        /// <summary>
        /// 确认或结单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isConfirm"></param>
        /// <returns></returns>
        public static bool ConfirmOrCompleteBackMaterial(BackMaterialModel model, int OperateType)
        {
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            //string loginUserID = "admin";//[待修改]
            return BackMaterialDBHelper.ConfirmOrCompleteBackMaterial(model, loginUserID, OperateType);
        }
        #endregion

        #region 取消确认
        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isConfirm"></param>
        /// <returns></returns>
        public static bool CancelConfirmBackMaterial(BackMaterialModel model, int BillTypeFlag, int BillTypeCode)
        {
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            return BackMaterialDBHelper.CancelConfirmOperate(model, BillTypeFlag, BillTypeCode, loginUserID);
        }
        #endregion

        #region 输出系统日志
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">异常信息</param>
        private static void WriteSystemLog(UserInfoUtil userInfo, int ModuleType, Exception ex)
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
            if (ModuleType == 0)
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_BACKMATERIAL_EDIT;
            }
            else
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_BACKMATERIAL_LIST;
            }
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
        private static LogInfoModel InitLogInfo(string wcno, int ModuleType)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_BOM_LIST;
            if (ModuleType == 0)
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_BACKMATERIAL_EDIT;
            }
            else
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_BACKMATERIAL_LIST;
            }
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_BACK;
            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion
    }
}

/**********************************************
 * 类作用：   主生产计划单事务层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/03
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
    public class MasterProductScheduleBus
    {
        /// <summary>
        /// 主生产计划单插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertMasterProductSchedule(MasterProductScheduleModel model, Hashtable ht, out string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ID = "0";
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.PlanNo, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;

                succ = MasterProductScheduleDBHelper.InsertMasterProductSchedule(model, ht,loginUserID, out ID);
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


        /// <summary>
        /// 获取主生产计划单详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetMasterProductScheduleDetailInfo(MasterProductScheduleModel model)
        {
            try
            {
                return MasterProductScheduleDBHelper.GetMasterProductScheduleDetailInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #region 获取主生产计划单主表信息
        /// <summary>
        /// 获取主生产计划单主表信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetMasterProductScheduleInfo(MasterProductScheduleModel model)
        {
            try
            {
                return MasterProductScheduleDBHelper.GetMasterProductScheduleInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取主生产计划单子表信息
        /// <summary>
        /// 获取主生产计划单子表信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetMasterProductScheduleDetailInfoList(MasterProductScheduleModel model)
        {
            try
            {
                return MasterProductScheduleDBHelper.GetMasterProductScheduleDetailInfoList(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region 更新主生产计划单
        /// <summary>
        /// 更新主生产计划单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="DetailUpdate"></param>
        /// <returns></returns>
        public static bool UpdateMasterProductSchedule(MasterProductScheduleModel model, Hashtable ht, string DetailUpdate)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.PlanNo,0);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

                succ = MasterProductScheduleDBHelper.UpdateMasterproductSchedule(model, ht, DetailUpdate, loginUserID);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo,0, ex);
                return false;
            }
        }

        #endregion

        /// <summary>
        /// 确认或结单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isConfirm"></param>
        /// <returns></returns>
        public static bool ConfirmOrCompleteMasterProductSchedule(MasterProductScheduleModel model, int OperateType)
        {
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            //string loginUserID = "admin";//[待修改]
            return MasterProductScheduleDBHelper.ConfirmOrCompleteMasterProductSchedule(model, loginUserID, OperateType);
        }

        #region 取消确认
        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isConfirm"></param>
        /// <returns></returns>
        public static bool CancelConfirmMasterProductSchedule(MasterProductScheduleModel model,int BillTypeFlag,int BillTypeCode)
        {
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            return MasterProductScheduleDBHelper.CancelConfirmOperate(model, BillTypeFlag, BillTypeCode, loginUserID);
        }
        #endregion


        #region 通过检索条件查询主生产计划单信息
        /// <summary>
        /// 查询主生产计划单信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable GetMasterProductScheduleListBycondition(MasterProductScheduleModel model, int FromBillID, int FlowStatus, string BillTypeFlag, string BillTypeCode, string EFIndex,string EFDesc,int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return MasterProductScheduleDBHelper.GetMasterProductScheduleListBycondition(model, FromBillID, FlowStatus, BillTypeFlag, BillTypeCode, EFIndex, EFDesc, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 是否被引用
        /// <summary>
        /// 判断要删除的ID是否已经被引用
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ID"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static int CountRefrence(string CompanyCD, string ID, string TableName)
        {
            try
            {
                return MasterProductScheduleDBHelper.CountRefrence(CompanyCD, ID, TableName, "PlanID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        /// <summary>
        /// 删除主生产计划单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteMasterProductSchedule(string ID, string CompanyCD)
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

            bool isSucc = MasterProductScheduleDBHelper.DeleteMasterProductSchedule(ID, CompanyCD);
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
                LogInfoModel logModel = InitLogInfo("主生产计划ID：" + no,1);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                //设置操作成功标识
                logModel.Remark = remark;

                //登陆日志
                LogDBHelper.InsertLog(logModel);
            }
            return isSucc;
        }

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
                logSys.ModuleID = ConstUtil.MODULE_ID_SCHEDULE_EDIT;
            }
            else
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_SCHEDULE_LIST;
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
                logModel.ModuleID = ConstUtil.MODULE_ID_SCHEDULE_EDIT;
            }
            else
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_SCHEDULE_LIST;
            }
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_SCHEDULE;
            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion
    }
}

/**********************************************
 * 类作用：   生产任务事务层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/24
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
    public class ManufactureTaskBus
    {
        #region 生产任务插入
        /// <summary>
        /// 生产任务插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertManufactureTask(ManufactureTaskModel model, Hashtable ht, out string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ID = "0";
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.TaskNo, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;

                succ = ManufactureTaskDBHelper.InsertManufactureTask(model, ht,loginUserID, out ID);
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

        #region 更新生产任务信息
        /// <summary>
        /// 更新BOM和子件信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UpdateID"></param>
        /// <returns></returns>
        public static bool UpdateManufactureTaskInfo(ManufactureTaskModel model, Hashtable ht, string UpdateID)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.TaskNo, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

                succ = ManufactureTaskDBHelper.UpdateManufactureTaskInfo(model,ht, loginUserID, UpdateID);
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

        #region 生产任务单详细信息
        /// <summary>
        /// 生产任务单详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetTaskInfo(ManufactureTaskModel model)
        {
            try
            {
                return ManufactureTaskDBHelper.GetTaskInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 生产任务单明细详细信息
        /// <summary>
        /// 生产任务单明细详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetTaskDetailInfo(ManufactureTaskModel model)
        {
            try
            {
                return ManufactureTaskDBHelper.GetTaskDetailInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 源单总览：主生产计划明细
        /// <summary>
        /// 主生产计划中是来源自生产的明细
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetMasterProductScheduleFromPlan(string CompanyCD, string strPlanID)
        {
            try
            {
                return ManufactureTaskDBHelper.GetMasterProductScheduleFromPlan(CompanyCD, strPlanID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 通过检索条件查询生产任务单信息
        /// <summary>
        /// 通过检索条件查询生产任务单信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetManufactureTaskListBycondition(ManufactureTaskModel model, int FlowStatus, int BillTypeFlag, int BillTypeCode, string CreateDate, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return ManufactureTaskDBHelper.GetManufactureTaskListBycondition(model, FlowStatus, BillTypeFlag, BillTypeCode, CreateDate, EFIndex, EFDesc, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除主生产任务
        /// <summary>
        /// 删除主生产任务
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteManufactureTask(string ID, string CompanyCD)
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

            bool isSucc = ManufactureTaskDBHelper.DeleteManufactureTask(ID, CompanyCD);
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
                LogInfoModel logModel = InitLogInfo("生产任务单ID：" + no, 1);
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

        #region 确认或结单
        /// <summary>
        /// 确认或结单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isConfirm"></param>
        /// <returns></returns>
        public static bool ConfirmOrCompleteManufactureTask(ManufactureTaskModel model, int OperateType)
        {
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            //string loginUserID = "admin";//[待修改]
            return ManufactureTaskDBHelper.ConfirmOrCompleteManufactureTask(model, loginUserID, OperateType);
        }
        #endregion

        #region 取消确认
        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isConfirm"></param>
        /// <returns></returns>
        public static bool CancelConfirmTask(ManufactureTaskModel model, int BillTypeFlag, int BillTypeCode)
        {
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            return ManufactureTaskDBHelper.CancelConfirmOperate(model, BillTypeFlag, BillTypeCode, loginUserID);
            
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
                return ManufactureTaskDBHelper.CountRefrence(CompanyCD, ID, TableName, "FromBillNo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 是否被领料单引用
        /// <summary>
        /// 是否被领料单引用
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ID"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static int CountRefrenceTakeMaterial(string CompanyCD, string ID, string TableName)
        {
            try
            {
                return ManufactureTaskDBHelper.CountRefrenceTakeMaterial(CompanyCD, ID, TableName, "TaskID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                logSys.ModuleID = ConstUtil.MODULE_ID_MANUFACTURETASK_EDIT;
            }
            else
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_MANUFACTURETASK_LIST;
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
                logModel.ModuleID = ConstUtil.MODULE_ID_MANUFACTURETASK_EDIT;
            }
            else
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_MANUFACTURETASK_LIST;
            }
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_MANUFACTURETASK;
            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        #region 运营模式：(生产任务单执行汇总表)
        /// <summary>
        /// 运营模式:打印生产任务单执行汇总表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="ConfirmDateStart"></param>
        /// <param name="ConfirmDateEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskListBycondition_Operating(string CompanyCD, int DeptID, string ConfirmDateStart, string ConfirmDateEnd, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return ManufactureTaskDBHelper.GetManufactureTaskListBycondition_Operating(CompanyCD, DeptID, ConfirmDateStart, ConfirmDateEnd, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 运营模式：(打印生产任务单执行汇总表)
        /// <summary>
        /// 运营模式:打印生产任务单执行汇总表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="ConfirmDateStart"></param>
        /// <param name="ConfirmDateEnd"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskListBycondition_Operating_Print(string CompanyCD, int DeptID, string ConfirmDateStart, string ConfirmDateEnd, string orderColumn, string orderType)
        {
            try
            {
                return ManufactureTaskDBHelper.GetManufactureTaskListBycondition_Operating_Print(CompanyCD,DeptID,ConfirmDateStart, ConfirmDateEnd, orderColumn, orderType);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 运营模式：(在制品存量统计表)
        /// <summary>
        /// 在制品存量统计表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ConfirmDateStart"></param>
        /// <param name="ConfirmDateEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetRoadStorageProductBycondition_Operating(string CompanyCD, int ProductID,int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return ManufactureTaskDBHelper.GetRoadStorageProductBycondition_Operating(CompanyCD,ProductID, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 运营模式：(打印在制品存量统计表)
        /// <summary>
        /// 运营模式：打印在制品存量统计表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CheckDateStart"></param>
        /// <param name="CheckDateEnd"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetRoadStorageProductBycondition_Operating_Print(string CompanyCD, int ProductID,string orderColumn, string orderType)
        {
            try
            {
                return ManufactureTaskDBHelper.GetRoadStorageProductBycondition_Operating_Print(CompanyCD,ProductID,orderColumn, orderType);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 运营模式：(在制品价值汇总表)
        /// <summary>
        /// 运营模式:在制品价值汇总表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CheckDateStart"></param>
        /// <param name="CheckDateEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetRoadStorageProductValueBycondition_Operating(string CompanyCD, int ProductID,int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return ManufactureTaskDBHelper.GetRoadStorageProductValueBycondition_Operating(CompanyCD, ProductID, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 运营模式：(打印在制品价值汇总表)
        /// <summary>
        /// 运营模式:打印在制品价值汇总表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CheckDateStart"></param>
        /// <param name="CheckDateEnd"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetRoadStorageProductValueBycondition_Operating_Print(string CompanyCD,int ProductID, string orderColumn, string orderType)
        {
            try
            {
                return ManufactureTaskDBHelper.GetRoadStorageProductValueBycondition_Operating_Print(CompanyCD, ProductID, orderColumn, orderType);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 运营模式：(生产日报表)
        /// <summary>
        /// 生产日报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="theDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskDateBycondition_Operating(string CompanyCD, int DeptID, string theDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return ManufactureTaskDBHelper.GetManufactureTaskDateBycondition_Operating(CompanyCD,DeptID,theDate, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 运营模式：(打印生产日报表)
        /// <summary>
        /// 打印生产日报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="theDate"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskDateBycondition_Operating_Print(string CompanyCD, int DeptID,string theDate, string orderColumn, string orderType)
        {
            try
            {
                return ManufactureTaskDBHelper.GetManufactureTaskDateBycondition_Operating_Print(CompanyCD,DeptID,theDate, orderColumn, orderType);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 运营模式：(生产月报表)
        /// <summary>
        /// 生产月报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="QueryDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskMonthBycondition_Operating(string CompanyCD, int DeptID, string QueryDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return ManufactureTaskDBHelper.GetManufactureTaskMonthBycondition_Operating(CompanyCD, DeptID,QueryDate, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 运营模式：(打印生产月报表)
        /// <summary>
        /// 打印生产月报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="QueryDate"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskMonthBycondition_Operating_Print(string CompanyCD, int DeptID, string QueryDate, string orderColumn, string orderType)
        {
            try
            {
                return ManufactureTaskDBHelper.GetManufactureTaskMonthBycondition_Operating_Print(CompanyCD, DeptID, QueryDate, orderColumn, orderType);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

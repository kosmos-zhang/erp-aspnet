/**********************************************
 * 类作用：   生产任务汇报单事务层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/22
 * 修改时间： 2009/05/04
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
    public class ManufactureReportBus
    {
        #region 生产任务汇报单插入
        /// <summary>
        /// 生产任务汇报单插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertManufactureReport(ManufactureReportModel model, Hashtable ht, ManufactureReportProductModel modelProduct, ManufactureReportStaffModel modelStaff, ManufactureReportMachineModel modelMachine, ManufactureReportMeterialModel modelMeterial, out string ID)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ID = "0";
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.ReportNo, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;

                succ = ManufactureReportDBHelper.InsertManufactureReport(model, ht, modelProduct, modelStaff, modelMachine, modelMeterial, loginUserID, out ID);
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

        #region 更新生产任务汇报单和各明细信息
        /// <summary>
        /// 更新生产任务汇报单和各明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelProduct"></param>
        /// <param name="modelStaff"></param>
        /// <param name="modelMachine"></param>
        /// <param name="modelMeterial"></param>
        /// <returns></returns>
        public static bool UpdateManufactureReportInfo(ManufactureReportModel model, Hashtable ht, ManufactureReportProductModel modelProduct, ManufactureReportStaffModel modelStaff, ManufactureReportMachineModel modelMachine, ManufactureReportMeterialModel modelMeterial)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.ReportNo, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

                succ = ManufactureReportDBHelper.UpdateManufactureReportInfo(model, ht, modelProduct, modelStaff, modelMachine, modelMeterial, loginUserID);
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

        #region 生产任务汇报单详细信息
        /// <summary>
        /// 生产任务汇报单详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetManufactureReport(ManufactureReportModel model)
        {
            try
            {
                return ManufactureReportDBHelper.GetManufactureReport(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 生产明细
        /// <summary>
        /// 生产明细
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetManufactureReportProduct(ManufactureReportModel model)
        {
            try
            {
                return ManufactureReportDBHelper.GetManufactureReportProduct(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 人员明细
        /// <summary>
        /// 人员明细
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetManufactureReportStaff(ManufactureReportModel model)
        {
            try
            {
                return ManufactureReportDBHelper.GetManufactureReportStaff(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 设备明细
        /// <summary>
        /// 设备明细
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetManufactureReportMachine(ManufactureReportModel model)
        {
            try
            {
                return ManufactureReportDBHelper.GetManufactureReportMachine(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 物料明细
        /// <summary>
        /// 物料明细
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetManufactureReportMeterial(ManufactureReportModel model)
        {
            try
            {
                return ManufactureReportDBHelper.GetManufactureReportMeterial(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 设备列表
        /// <summary>
        /// 设备列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetEquipmentList(string CompanyCD)
        {
            try
            {
                return ManufactureReportDBHelper.GetEquipmentList(CompanyCD);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 通过检索条件查询生产任务汇报单信息
        /// <summary>
        /// 通过检索条件查询生产任务汇报单信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetManufactureReportListBycondition(ManufactureReportModel model, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return ManufactureReportDBHelper.GetManufactureReportListBycondition(model, EFIndex, EFDesc, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除生产任务汇报单
        /// <summary>
        /// 删除生产任务汇报单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteManufactureReport(string ID, string CompanyCD)
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

            bool isSucc = ManufactureReportDBHelper.DeleteManufactureReport(ID, CompanyCD);
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
                LogInfoModel logModel = InitLogInfo("生产任务汇报单ID：" + no, 1);
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

        #region 确认生产任务汇报单
        /// <summary>
        /// 确认生产任务汇报单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ConfirmMenufactureReport(ManufactureReportModel model,int EditType)
        {
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            //string loginUserID = "admin";//[待修改]
            return ManufactureReportDBHelper.ConfirmMenufactureReport(model, loginUserID, EditType);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_MANUFACTUREREPORT_EDIT;
            }
            else
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_MANUFACTUREREPORT_LIST;
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
                logModel.ModuleID = ConstUtil.MODULE_ID_MANUFACTUREREPORT_EDIT;
            }
            else
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_MANUFACTUREREPORT_LIST;
            }
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_REPORT;
            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        #region 运营模式：(生产任务单汇报明细表)
        /// <summary>
        /// 生产任务单汇报明细表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="TaskNo"></param>
        /// <param name="ConfirmDateStart"></param>
        /// <param name="ConfirmDateEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetManufactureReportListBycondition_Operating(string CompanyCD, int DeptID, string TaskNo, string ConfirmDateStart, string ConfirmDateEnd, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return ManufactureReportDBHelper.GetManufactureReportListBycondition_Operating(CompanyCD, DeptID, TaskNo, ConfirmDateStart, ConfirmDateEnd, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 运营模式：(打印生产任务单汇报明细表)
        /// <summary>
        /// 打印生产任务单汇报明细表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="TaskNo"></param>
        /// <param name="ConfirmDateStart"></param>
        /// <param name="ConfirmDateEnd"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetManufactureReportListBycondition_Operating_Print(string CompanyCD, int DeptID, string TaskNo, string ConfirmDateStart, string ConfirmDateEnd, string orderColumn, string orderType)
        {
            try
            {
                return ManufactureReportDBHelper.GetManufactureReportListBycondition_Operating_Print(CompanyCD, DeptID, TaskNo, ConfirmDateStart, ConfirmDateEnd, orderColumn, orderType);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}

/**********
 *  * 
***********/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using XBase.Data.DBHelper;
using XBase.Data.Office.StorageManager;
using XBase.Business.Office.SystemManager;
using XBase.Model.Office.StorageManager;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using System.Collections;
namespace XBase.Business.Office.StorageManager
{
    public class CheckReportBus
    {
        public static DataTable GetCheckApplay(string method, string ReportStr)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return CheckReportDBHelper.GetCheckApplay(CompanyCD, method, ReportStr);
        }
        public static DataTable GetCheckDetailBy(string ApplyNo, string CompanyCD, string ReportStr)
        {
            return CheckReportDBHelper.GetDetailBy(ApplyNo, CompanyCD, ReportStr);
        }
        /// <summary>
        /// 获取生产相关的物品信息
        /// </summary>
        /// <param name="TaskNo"></param>
        /// <returns></returns>
        public static DataTable GetManDetail(string TaskNo, string CompanyCD, string ReportStr)
        {
            return CheckReportDBHelper.GetManDetail(TaskNo, CompanyCD, ReportStr);
        }
        /// <summary>
        ///  获取质检报告生产需要
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMan(string method, string ReportStr)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return CheckReportDBHelper.GetMan(CompanyCD, method, ReportStr);
        }
        /// <summary>
        /// 获取源单类型为质检报告单时
        /// </summary>
        /// <returns></returns>
        public static DataTable GetReportInfo(string method, string ReportStr)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return CheckReportDBHelper.GetReportInfo(CompanyCD, method, ReportStr);
        }
        public static DataTable GetCodePublicType()
        {
            string TypeFlag = ConstUtil.CODING_Public_StorageCheckReportTypeFlag;
            string TypeCode = ConstUtil.CODING_Public_StorageCheckReportTypeCode;
            return XBase.Business.Office.SystemManager.CodePublicTypeBus.GetCodePublicByCode(TypeFlag, TypeCode);
        }


        /// <summary>
        /// 添加质检
        /// </summary>
        /// <param name="model">质检申请单</param>
        /// <param name="detailList">明细信息</param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static bool AddReport(StorageQualityCheckReportModel model, List<StorageQualityCheckReportDetailModel> detail, Hashtable htExtAttr)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {

                bool result = false;
                LogInfoModel logModel = InitLogInfo(model.ReportNo);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                result = CheckReportDBHelper.AddReport(model, detail, htExtAttr);
                if (!result)
                {
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                }
                else
                {
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                    model.ID = IDIdentityUtil.GetIDIdentity("officedba.QualityCheckReport");
                }
                LogDBHelper.InsertLog(logModel);
                return result;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex);
                return false;
            }
        }
        /// <summary>
        /// 检索质检报告
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="FlowStatus"></param>
        /// <param name="ReportID"></param>
        /// <returns></returns>
        public static DataTable SearchReport(StorageQualityCheckReportModel model, string BeginTime, string EndTime, string FlowStatus, string EFIndex, string EFDesc, ref int TotalCount)
        {
            return CheckReportDBHelper.GetAllReport(model, BeginTime, EndTime, FlowStatus, EFIndex, EFDesc, ref TotalCount);
        }

        public static bool DeleteReport(string ID, string CompanyCD)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //string CompanyCD = "AAAAAA";
            bool isSucc = CheckReportDBHelper.DelReport(ID, CompanyCD);
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
                LogInfoModel logModel = InitLogInfo("质检报告单ID：" + no);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                //设置操作成功标识
                logModel.Remark = remark;

                //登陆日志
                LogDBHelper.InsertLog(logModel);
            }
            return isSucc;
        }
        /// <summary>
        /// 获取报告单详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetReportInfo(StorageQualityCheckReportModel model)
        {
            try
            {
                return CheckReportDBHelper.GetReportInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取报告单明细详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetReportDetailInfo(StorageQualityCheckReportModel model)
        {
            try
            {
                return CheckReportDBHelper.GetReportDetailInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新汇报单
        /// </summary>
        /// <param name="model">汇报单</param>
        /// <param name="detailList">明细</param>
        /// <param name="SortID"></param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static bool UpdateReport(StorageQualityCheckReportModel model, List<StorageQualityCheckReportDetailModel> detailList, string[] SortID, Hashtable htExtAttr)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {

                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.ReportNo);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                succ = CheckReportDBHelper.UpdateReport(model, detailList, SortID, htExtAttr);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex);
                return false;
            }
        }
        public static DataTable GetForeignInfo()
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return CheckReportDBHelper.GetFroeignCorpInfo(CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetOtherInfo()
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return CheckReportDBHelper.GetOtherCorpInfo(CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 回写质检报告
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool UpdateReport(StorageQualityCheckReportModel model)
        {
            return CheckReportDBHelper.UpdateReport(model);
        }
        /// <summary>
        /// 回写质检
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool UpdateApply(StorageQualityCheckReportModel model)
        {
            return CheckReportDBHelper.UpdateApply(model);
        }
        public static bool UpdatePur(StorageQualityCheckReportModel model)
        {
            return CheckReportDBHelper.UpdatePur(model);
        }
        /// <summary>
        /// 回写生产任务
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool UpdateMan(StorageQualityCheckReportModel model)
        {
            return CheckReportDBHelper.UpdateManufa(model);
        }

        public static bool ConfirmBill(StorageQualityCheckReportModel model)
        {
            return CheckReportDBHelper.ConfirmBill(model);
        }
        public static bool CloseBill(StorageQualityCheckReportModel model, string method)
        {
            return CheckReportDBHelper.CloseBill(model, method);
        }


        /// <summary>
        /// 判断单据是否被引用
        /// </summary>
        /// <param name="ReportID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static string IsTransfer(int ReportID, string CompanyCD)
        {
            return CheckReportDBHelper.IsTransfer(ReportID, CompanyCD);
        }
        /// <summary>
        /// 判断制单单据是否提交审批
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string IsFlow(int ID)
        {
            return CheckReportDBHelper.IsFlow(ID);
        }

        public static DataTable GetReportPur(string CompanyCD, string method, string ReportStr)
        {
            return CheckReportDBHelper.GetReportPur(CompanyCD, method, ReportStr);
        }
        public static DataTable GetReprotPurDetail(string ArriveNo, string CompanyCD, string ReportStr)
        {
            return CheckReportDBHelper.GetReportPurDetail(ArriveNo, CompanyCD, ReportStr);
        }

        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool UnConfirm(StorageQualityCheckReportModel model)
        {
            return CheckReportDBHelper.UnConfirm(model);
        }
        /// <summary>
        /// 确认回写时候 判断已检数量和报检数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string CheckConfirm(StorageQualityCheckReportModel model)
        {
            return CheckReportDBHelper.GetCheckConfirm(model);
        }
        public static string GetCheckSave(StorageQualityCheckReportModel model)
        {
            return CheckReportDBHelper.GetCheckSave(model);
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
            logSys.ModuleID = ConstUtil.MODULE_CODING_RULE_TABLE_ReportInfo;
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
        private static LogInfoModel InitLogInfo(string prodno)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_QUALITYREPORT_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.MODULE_CODING_RULE_TABLE_ReportInfo;
            //操作对象
            logModel.ObjectID = prodno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion


        //--------------------------------------------------------------打印需要
        public static DataTable GetReportInfo(int ID)
        {
            return CheckReportDBHelper.GetReportInfo(ID);
        }
        public static DataTable GetDetailInfo(int ID)
        {
            string TypeFlag = ConstUtil.CODING_Public_StorageCheckReportTypeFlag;
            string TypeCode = ConstUtil.CODING_Public_StorageCheckReportTypeCode;
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return CheckReportDBHelper.GetReportDetailInfo(ID, TypeFlag, TypeCode, CompanyCD);
        }

        /// <summary>
        /// 更新附件字段
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="Attachment">附件URL</param>
        public static void UpDateAttachment(int ID, string Attachment)
        {
            CheckReportDBHelper.UpDateAttachment(ID, Attachment);
        }
    }
}

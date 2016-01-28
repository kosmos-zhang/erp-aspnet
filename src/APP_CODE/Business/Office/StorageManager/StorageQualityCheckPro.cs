using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.PurchaseManager;
using XBase.Data.Office.StorageManager;
using XBase.Business.Common;
using XBase.Data.Common;
using XBase.Common;
using XBase.Model.Common;
using System.Collections;

namespace XBase.Business.Office.StorageManager
{
    public class StorageQualityCheckPro
    {
        public static DataTable GetStorageQualityCheckPro(string Method, string QuaStr)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageQualityCheckProDBHelper.GetQualityCheckPro(Method, QuaStr, CompanyCD);
        }
        public static DataTable GetPurDetail(string IDList, string CompanyCD)
        {
            return StorageQualityCheckProDBHelper.GetPurDetailList(IDList, CompanyCD);
        }
        public static DataTable GetManDetail(string IDlist, string CompanyCD)
        {
            return StorageQualityCheckProDBHelper.GetManDetailList(IDlist, CompanyCD);
        }
        public static DataTable GetEmplpyeeForQuality()
        {
            return StorageQualityCheckProDBHelper.GetEmployeeForQuality();
        }
        /// <summary>
        /// 添加质检
        /// </summary>
        /// <param name="model"></param>
        /// <param name="detailList"></param>
        /// <returns></returns>
        public static bool AddQualityCheck(StorageQualityCheckApplay model, List<StorageQualityCheckApplyDetail> detailList, Hashtable htExtAttr)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {

                bool result = false;
                LogInfoModel logModel = InitLogInfo(model.ApplyNO);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                result = StorageQualityCheckProDBHelper.AddStoryQualityCheckDB(model, detailList, htExtAttr);
                if (!result)
                {
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                }
                else
                {
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                    model.ID = IDIdentityUtil.GetIDIdentity("officedba.QualityCheckApplay");
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
        public static bool ConfirmBill(StorageQualityCheckApplay model)
        {
            return StorageQualityCheckProDBHelper.ConfirmBill(model);
        }

        public static bool UpdateQualityCheck(StorageQualityCheckApplay model, List<StorageQualityCheckApplyDetail> DetailList, string[] ProductID, Hashtable htExtAttr)
        {
            //return StorageQualityCheckProDBHelper.UpdateStorageCheck(model, DetailList, ProductID);

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {

                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.ApplyNO);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                succ = StorageQualityCheckProDBHelper.UpdateStorageCheck(model, DetailList, ProductID, htExtAttr);
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


        public static DataTable GetQualityList(StorageQualityCheckApplay model, DateTime EndCheckDate, string FlowStatus, string EFIndex, string EFDesc, ref int TotalCount)
        {
            return StorageQualityCheckProDBHelper.GetQualityCheckList(model, EndCheckDate, FlowStatus, EFIndex, EFDesc, ref TotalCount);
        }
        public static DataTable GetQualityDetail(int ID, string CompanyCD)
        {
            return StorageQualityCheckProDBHelper.GetQualityDetail(ID, CompanyCD);
        }
        public static DataTable GetOneQuality(int ID, string CompanyCD)
        {
            return StorageQualityCheckProDBHelper.GetOneQualityDB(ID, CompanyCD);
        }

        public static bool DeleteQualityDB(string ID)
        {


            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //string CompanyCD = "AAAAAA";
            bool isSucc = StorageQualityCheckProDBHelper.DeleteQualityApply(ID);
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
                LogInfoModel logModel = InitLogInfo("质检申请单ID：" + no);
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
        /// 确认后回写数据
        /// </summary>
        /// <param name="CheckedCount"></param>
        /// <param name="ID"></param>
        /// <param name="FromTyep"></param>
        /// <returns></returns>
        public static bool UpdateBackkData(string CheckedCount, string ID, string FromType)
        {
            return StorageQualityCheckProDBHelper.UpdatePurDetail(CheckedCount, ID, FromType);
        }

        public static bool CloseBill(StorageQualityCheckApplay model, string Method)
        {
            return StorageQualityCheckProDBHelper.CloseBill(model, Method);
        }
        /// <summary>
        /// 判断质检申请单是否被引用(修改需要)
        /// </summary>
        /// <param name="FromReportNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static string IsTransfer(int ID, string CompanyCD)
        {
            return StorageQualityCheckProDBHelper.IsTransfer(ID, CompanyCD);
        }

        public static string IsFlow(int ID)
        {
            return StorageQualityCheckProDBHelper.IsFlow(ID);
        }
        /// <summary>
        /// 取消确认时回写数据
        /// </summary>
        /// <param name="CheckedCount"></param>
        /// <param name="ID"></param>
        /// <param name="FromType"></param>
        /// <returns></returns>
        public static bool UpBackkData(string CheckedCount, string ID, string FromType, StorageQualityCheckApplay model)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string ModifiedID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            return StorageQualityCheckProDBHelper.UpPurDetail(CheckedCount, ID, FromType, ModifiedID, companyCD, model);
        }

        public static bool GetCheckedCount(StorageQualityCheckApplay model, string IDList, string TheCheckedCount)
        {
            return StorageQualityCheckProDBHelper.GetCheckCount(model, IDList, TheCheckedCount);
        }

        public static bool GetCheckSaveCount(StorageQualityCheckApplay model, string IDList, string TheCheckedCount)
        {
            return StorageQualityCheckProDBHelper.GetCheckSaveCount(model, IDList, TheCheckedCount);
        }

        //----------------------------------------------修改页打印需要
        public static DataTable GetPrintQualityDetail(string ApplyNo, string CompanyCD, string Method)
        {
            return StorageQualityCheckProDBHelper.GetPrintQualityDetail(ApplyNo, CompanyCD, Method);
        }
        public static DataTable GetPrintQualityDB(int ID)
        {
            return StorageQualityCheckProDBHelper.GetPrintQualityDB(ID);
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
            logSys.ModuleID = ConstUtil.MODULE_CODING_RULE_TABLE_QuaInfo;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_QUALITY_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.MODULE_CODING_RULE_TABLE_QuaInfo;
            //操作对象
            logModel.ObjectID = prodno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        
        /// <summary>
        /// 更新附件字段
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="Attachment">附件URL</param>
        public static void UpDateAttachment(int ID, string Attachment)
        {
            StorageQualityCheckProDBHelper.UpDateAttachment(ID, Attachment);
        }
    }
}

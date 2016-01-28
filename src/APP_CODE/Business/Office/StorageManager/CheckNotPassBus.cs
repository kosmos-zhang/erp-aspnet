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
    public class CheckNotPassBus
    {
        /// <summary>
        /// 获取不合格原因
        /// </summary>
        /// <returns></returns>
        public static DataTable GetReason()
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return CheckNotPassDBHelper.GetReason(CompanyCD);
        }
        /// <summary>
        /// 带出源单类型为质检报告的信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetReportInfo(string method, string NoPassStr)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return CheckNotPassDBHelper.GetReportInfo(CompanyCD, method, NoPassStr);
        }

        /// <summary>
        /// 添加不合格品处置单 
        /// </summary>
        /// <param name="model">不合格品处置单 </param>
        /// <param name="detail">明细</param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static bool AddNoPass(CheckNotPassModel model, List<CheckNotPassDetailModel> detail, Hashtable htExtAttr)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {

                bool result = false;
                LogInfoModel logModel = InitLogInfo(model.ProcessNo);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                result = CheckNotPassDBHelper.AddNoPass(model, detail, htExtAttr);
                if (!result)
                {
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                }
                else
                {
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                    model.ID = IDIdentityUtil.GetIDIdentity("officedba.CheckNotPass");
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

        public static DataTable SearchNoPass(CheckNotPassModel model, string ProductID, string BeginTime, string EndTime, string FlowStatus, string EFIndex, string EFDesc, ref int TotalCount)
        {
            return CheckNotPassDBHelper.GetAllNoPass(model, ProductID, BeginTime, EndTime, FlowStatus,EFIndex,EFDesc, ref TotalCount);
        }

        public static bool DelNoPass(string ID, string CompanyCD)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //string CompanyCD = "AAAAAA";
            bool isSucc = CheckNotPassDBHelper.DelNoPass(ID, CompanyCD);
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
                LogInfoModel logModel = InitLogInfo("不合格处置单ID：" + no);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                //设置操作成功标识
                logModel.Remark = remark;

                //登陆日志
                LogDBHelper.InsertLog(logModel);
            }
            return isSucc;
        }

        public static DataTable GetNoPassInfo(CheckNotPassModel modle)
        {
            return CheckNotPassDBHelper.GetNoPassInfo(modle);
        }
        public static DataTable GetNoPassDetailInfo(CheckNotPassModel model)
        {
            return CheckNotPassDBHelper.GetNoPassDetail(model);
        }

        /// <summary>
        /// 更新不合格品处置单 
        /// </summary>
        /// <param name="model">不合格品处置单 </param>
        /// <param name="detailmodel">明细</param>
        /// <param name="SortID"></param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static bool UpdateNoPassInfo(CheckNotPassModel model, List<CheckNotPassDetailModel> detail, string[] SortID, Hashtable htExtAttr)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {

                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.ProcessNo);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                succ = CheckNotPassDBHelper.UpdateNoPassInfo(model, detail, SortID, htExtAttr);
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

        public static bool ConfirmBill(CheckNotPassModel model, string CheckNum)
        {
            return CheckNotPassDBHelper.ConfirmBill(model, CheckNum);
        }

        public static bool CloseBill(CheckNotPassModel model, string Method)
        {
            return CheckNotPassDBHelper.CloseBill(model, Method);
        }
        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UnConfirmBill(CheckNotPassModel model, string CheckNum)
        {
            return CheckNotPassDBHelper.UnConfirmBill(model, CheckNum);
        }
        public static string IsFlow(int ID)
        {
            return CheckNotPassDBHelper.IsFlow(ID);
        }
        public static string GetNotPassNum(CheckNotPassModel model)
        {
            return CheckNotPassDBHelper.GetNoExecutNum(model);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_QUALITYNOPASS_ADD;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_QUALITYNOPASS_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.MODULE_CODING_RULE_TABLE_NOPASSInfo;
            //操作对象
            logModel.ObjectID = prodno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion


        //-----------------------------------------------------------------------------页面打印需要
        public static DataTable GetNoPassInfo(int ID)
        {
            return CheckNotPassDBHelper.GetNoPassInfo(ID);
        }
        public static DataTable GetNoPassDetailInfo(int ID)
        {
            return CheckNotPassDBHelper.GetNoPassDetail(ID);
        }
    }
}
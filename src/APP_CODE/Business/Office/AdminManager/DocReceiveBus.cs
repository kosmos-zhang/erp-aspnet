using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Common;
using XBase.Common;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Business.Common;
using XBase.Data.Common;
using System.Data;
using XBase.Data.Office.CustManager;

namespace XBase.Business.Office.AdminManager
{
    public class DocReceiveBus
    {
        //private static UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];//获取登陆用户信息

        #region 添加收文信息的方法
        /// <summary>
        /// 添加收文信息的方法
        /// </summary>
        /// <param name="DocReceiveM">收文信息</param>
        /// <returns>被添加收文ID</returns>
        public static int DocReceiveAdd(DocReceiveModel DocReceiveM)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            LogInfoModel logModel = new LogInfoModel(); //操作日志
            int isSucc = 0;//定义返回变量

            #region 设置操作日志内容
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_DOCRECEIVE_ADD;
            //操作单据编号  收文编号
            logModel.ObjectID = DocReceiveM.ReceiveDocNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_DOCRECEIVE;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_INSERT;
            #endregion

            try
            {
                isSucc = DocReceiveDBHelper.DocReceiveAdd(DocReceiveM);
            }
            catch (System.Exception ex)
            {
                #region  操作失败时记录日志到文件
                //定义变量
                LogInfo logSys = new LogInfo();
                //设置日志类型 需要指定为系统日志
                logSys.Type = LogInfo.LogType.SYSTEM;
                //指定系统日志类型 出错信息
                logSys.SystemKind = LogInfo.SystemLogKind.SYSTEM_ERROR;
                //指定登陆用户信息
                logSys.UserInfo = userInfo;
                //设定模块ID
                logSys.ModuleID = ConstUtil.MODULE_ID_DOCRECEIVE_ADD;
                //描述
                logSys.Description = ex.ToString();
                //输出日志
                LogUtil.WriteLog(logSys);
                #endregion
            }

            if (isSucc > 0)//操作成功
            {
                logModel.Remark += "成功";
            }
            else//操作失败
            {
                logModel.Remark += "失败";
            }

            //记录日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;
        }
        #endregion

        #region 根据收文ID修改收文信息
        /// <summary>
        /// 根据收文ID修改收文信息
        /// </summary>
        /// <param name="DocReceiveM">收文信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateDocReceive(DocReceiveModel DocReceiveM)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            LogInfoModel logModel = new LogInfoModel(); //操作日志
            bool isSucc = false;//定义返回变量

            #region 设置操作日志内容
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_DOCRECEIVE_LIST;
            //操作单据编号  编号
            logModel.ObjectID = DocReceiveM.ReceiveDocNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_DOCRECEIVE;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_UPDATE;
            #endregion

            try
            {
                isSucc = DocReceiveDBHelper.UpdateDocReceive(DocReceiveM);
            }
            catch (System.Exception ex)
            {
                #region  操作失败时记录日志到文件
                //定义变量
                LogInfo logSys = new LogInfo();
                //设置日志类型 需要指定为系统日志
                logSys.Type = LogInfo.LogType.SYSTEM;
                //指定系统日志类型 出错信息
                logSys.SystemKind = LogInfo.SystemLogKind.SYSTEM_ERROR;
                //指定登陆用户信息
                logSys.UserInfo = userInfo;
                //设定模块ID
                logSys.ModuleID = ConstUtil.MODULE_ID_DOCRECEIVE_LIST;
                //描述
                logSys.Description = ex.ToString();
                //输出日志
                LogUtil.WriteLog(logSys);
                #endregion
            }

            if (isSucc)//操作成功
            {
                logModel.Remark += "成功";
            }
            else//操作失败
            {
                logModel.Remark += "失败";
            }

            //记录日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;
        }
        #endregion

        #region 根据条件检索收文信息
        /// <summary>
        /// 根据条件检索收文信息
        /// </summary>
        /// <param name="DocReceiveM">收文信息</param>
        /// <param name="FileDateBegin">开始时间</param>
        /// <param name="FileDateEnd">结束时间</param>
        /// <param name="FileCompany">来文单位</param>
        /// <returns>收文列表信息</returns>
        public static DataTable GetDocReceiveBycondition(DocReceiveModel DocReceiveM, string FileDateBegin, string FileDateEnd, string FileCompany, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return DocReceiveDBHelper.GetDocReceiveBycondition(DocReceiveM, FileDateBegin, FileDateEnd, FileCompany, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

        #region 根据收文ID删除收文信息
        /// <summary>
        /// 根据收文ID删除收文信息
        /// </summary>
        /// <param name="ReceiveID">收文ID</param>
        /// <returns>操作记录数</returns>
        public static int DelDocReceive(string[] ReceiveID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            LogInfoModel logModel = new LogInfoModel(); //操作日志
            int isSucc = 0;//定义返回变量

            #region 设置操作日志内容
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_DOCRECEIVE_LIST;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < ReceiveID.Length; i++)
            {
                sb.Append(ReceiveID[i] + ";");
            }

            //操作单据编号  联系人姓名
            logModel.ObjectID = "收文ID:" + sb.ToString();
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_DOCRECEIVE;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_DELETE;
            #endregion

            try
            {
                isSucc = LinkManDBHelper.DelLinkInfo(ReceiveID, ConstUtil.TABLE_NAME_DOCRECEIVE);
            }
            catch (System.Exception ex)
            {
                #region  操作失败时记录日志到文件
                //定义变量
                LogInfo logSys = new LogInfo();
                //设置日志类型 需要指定为系统日志
                logSys.Type = LogInfo.LogType.SYSTEM;
                //指定系统日志类型 出错信息
                logSys.SystemKind = LogInfo.SystemLogKind.SYSTEM_ERROR;
                //指定登陆用户信息
                logSys.UserInfo = userInfo;
                //设定模块ID
                logSys.ModuleID = ConstUtil.MODULE_ID_DOCRECEIVE_LIST;
                //描述
                logSys.Description = ex.ToString();
                //输出日志
                LogUtil.WriteLog(logSys);
                #endregion
            }
            if (isSucc > 0)//操作成功
            {
                logModel.Remark += "成功";
            }
            else//操作失败
            {
                logModel.Remark += "失败";
            }

            //记录日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;
        }
        #endregion

        #region 根据ID获得收文详细信息
        /// <summary>
        /// 根据ID获得收文详细信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="DocReceiveID">收文ID</param>
        /// <returns>收文信息</returns>
        public static DataTable GetDocReceiveByID(string CompanyCD, int DocReceiveID)
        {
            return DocReceiveDBHelper.GetDocReceiveByID(CompanyCD, DocReceiveID);
        }
        #endregion

        #region 收文明细
        /// <summary>
        /// 收文明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="TypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetDocReceiveList(string CompanyCD, string TypeID, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return DocReceiveDBHelper.GetDocReceiveList(CompanyCD, TypeID, BeginDate, EndDate, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 收文明细打印
        /// <summary>
        /// 收文明细打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="TypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetDocReceiveListPrint(string CompanyCD, string TypeID, string BeginDate, string EndDate, string ord)
        {
            return DocReceiveDBHelper.GetDocReceiveListPrint(CompanyCD, TypeID, BeginDate, EndDate, ord);
        }
        #endregion

        public static DataTable GetSendReceiveCount(string CompanyCD, string TableName, string TypeID, string BeginDate, string EndDate)
        {
            string TypeIdName = "";
            if (TableName == "officedba.DocReceiveInfo")
            {
                TypeIdName = "ReceiveDocTypeID";
            }
            else
            {
                TypeIdName = "SendDocTypeID";
            }
            return DocReceiveDBHelper.GetSendReceiveCount(CompanyCD, TableName, TypeIdName, TypeID, BeginDate, EndDate);
        }

        #region 导出收文列表
        /// <summary>
        /// 导出收文列表
        /// </summary>
        /// <param name="DocReceiveM"></param>
        /// <param name="FileDateBegin"></param>
        /// <param name="FileDateEnd"></param>
        /// <param name="FileCompany"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportDocReceive(DocReceiveModel DocReceiveM, string FileDateBegin, string FileDateEnd, string FileCompany, string ord)
        {
            return DocReceiveDBHelper.ExportDocReceive(DocReceiveM, FileDateBegin, FileDateEnd, FileCompany, ord);
        }
        #endregion
    }
}

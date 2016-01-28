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
    public class DocBus
    {
        //private static UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];//获取登陆用户信息

        #region 添加文档信息的方法
        /// <summary>
        /// 添加文档信息的方法
        /// </summary>
        /// <param name="DocM">文档信息</param>
        /// <returns>被添加文档ID</returns>
        public static int DocAdd(DocModel DocM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_DOC_ADD;
            //操作单据编号  文档编号
            logModel.ObjectID = DocM.DocumentNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_DOC;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_INSERT;
            #endregion

            try
            {
                isSucc = DocDBHelper.DocAdd(DocM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_DOC_ADD;
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

        #region 根据文档ID修改文档信息
        /// <summary>
        /// 根据文档ID修改文档信息
        /// </summary>
        /// <param name="DocM">文档信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateDoc(DocModel DocM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_DOC_LIST;
            //操作单据编号  编号
            logModel.ObjectID = DocM.DocumentNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_DOC;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_UPDATE;
            #endregion

            try
            {
                isSucc = DocDBHelper.UpdateDoc(DocM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_DOC_LIST;
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

        #region 根据条件检索文档信息
        /// <summary>
        /// 根据条件检索文档信息
        /// </summary>
        /// <param name="DocM">文档信息</param>
        /// <param name="FileDateBegin">开始时间</param>
        /// <param name="FileDateEnd">结束时间</param>       
        /// <returns>文档列表信息</returns>
        public static DataTable GetDocBycondition(DocModel DocM, string FileDateBegin, string FileDateEnd, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return DocDBHelper.GetDocBycondition(DocM, FileDateBegin, FileDateEnd, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

        #region 根据文档ID删除文档信息
        /// <summary>
        /// 根据文档ID删除文档信息
        /// </summary>
        /// <param name="DocID">文档ID</param>
        /// <returns>操作记录数</returns>
        public static int DelDocDoc(string[] DocID)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_DOC_LIST;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < DocID.Length; i++)
            {
                sb.Append(DocID[i] + ";");
            }

            //操作单据编号 
            logModel.ObjectID = "文档ID:" + sb.ToString();
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_DOC;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_DELETE;
            #endregion

            try
            {
                isSucc = LinkManDBHelper.DelLinkInfo(DocID, ConstUtil.TABLE_NAME_DOC);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_DOC_LIST;
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

        #region 根据ID获得文档详细信息
        /// <summary>
        /// 根据ID获得文档详细信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="DocID">文档ID</param>
        /// <returns>文档信息</returns>
        public static DataTable GetDocByID(string CompanyCD, int DocID)
        {
            return DocDBHelper.GetDocByID(CompanyCD, DocID);
        }
        #endregion

        #region 获取文档分类
        /// <summary>
        /// 获取文档分类
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetDocType()
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            return DocDBHelper.GetDocType(userInfo.CompanyCD);
        }
        #endregion

         #region 导出文档列表
        /// <summary>
        /// 导出文档列表
        /// </summary>
        /// <param name="DocM"></param>
        /// <param name="FileDateBegin"></param>
        /// <param name="FileDateEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportDoc(DocModel DocM, string FileDateBegin, string FileDateEnd, string ord)
        {
            return DocDBHelper.ExportDoc(DocM, FileDateBegin, FileDateEnd, ord);
        }
        #endregion
    }
}

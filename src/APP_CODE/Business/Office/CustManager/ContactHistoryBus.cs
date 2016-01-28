using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.Office.CustManager;
using System.Data;
using XBase.Model.Office.CustManager;
using XBase.Model.Common;
using XBase.Common;
using XBase.Data.Common;
using XBase.Business.Common;

namespace XBase.Business.Office.CustManager
{
    public class ContactHistoryBus
    {        
        #region 添加客户联络信息的方法
        /// <summary>
        /// 添加客户联络信息的方法
        /// </summary>
        /// <param name="ContactHistoryM">客户联络信息</param>
        /// <returns>返回bool值</returns>
        public static int ContactHistoryAdd(ContactHistoryModel ContactHistoryM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_CONTACT_ADD;
            //操作单据编号  联络编号
            logModel.ObjectID = ContactHistoryM.ContactNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_CONTACT;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_INSERT;
            #endregion

            try
            {
                isSucc = ContactHistoryDBHelper.ContactHistoryAdd(ContactHistoryM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_CONTACT_ADD;
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

        #region 根据联络信息ID修改联络信息
        /// <summary>
        /// 根据联络信息ID修改联络信息
        /// </summary>
        /// <param name="ContactM">联络信息</param>
        /// <returns>操作记录数</returns>
        public static bool UpdateContact(ContactHistoryModel ContactM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_CONTACT_LIST;
            //操作单据编号  联络人编号
            logModel.ObjectID = ContactM.ContactNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_CONTACT;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_UPDATE;
            #endregion

            try
            {
                isSucc = ContactHistoryDBHelper.UpdateContact(ContactM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_CONTACT_LIST;
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

        #region 根据查询条件获取客户联络列表信息的方法
        /// <summary>
        /// 根据查询条件获取客户联络列表信息的方法
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="CustClass">客户类型</param>
        /// <param name="ContactM">联络model</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="txtLinkDateEnd">结束时间</param>
        /// <returns>结果集</returns>
        public static DataTable GetContactInfoBycondition(string CanUserID,string CustName, string CustLinkMan, ContactHistoryModel ContactM, string LinkDateBegin, string txtLinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ContactHistoryDBHelper.GetContactInfoBycondition(CanUserID,CustName, CustLinkMan, ContactM, LinkDateBegin, txtLinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }

        public static DataTable GetContactInfoBycondition(string CustName, string CustLinkMan, ContactHistoryModel ContactM, string LinkDateBegin, string txtLinkDateEnd, string ReasonId, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ContactHistoryDBHelper.GetContactInfoBycondition(CustName, CustLinkMan, ContactM, LinkDateBegin, txtLinkDateEnd, ReasonId, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }
        #endregion
       
        #region 根据联络ID获得联络信息
        /// <summary>
        /// 根据联络ID获得联络信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="contactid">联络单ID</param>
        /// <returns>查询记录结果</returns>
        public static DataTable GetContactInfoByID(string CompanyCD, int contactid)
        {
            try
            {
                return ContactHistoryDBHelper.GetContactInfoByID(CompanyCD, contactid);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据联络编号批量删除联络信息
        /// <summary>
        /// 根据联络编号批量删除联络信息
        /// </summary>
        /// <param name="ContactID">联络ID</param>
        /// <returns>操作记录数</returns>
        public static int DelContactInfo(string[] ContactID)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_CONTACT_LIST;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < ContactID.Length; i++)
            {
                sb.Append(ContactID[i] + ";");
            }

            //操作单据编号  联系人姓名
            logModel.ObjectID = "客户联络单ID:" + sb.ToString();
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_CONTACT;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_DELETE;
            #endregion

            try
            {
                isSucc = LinkManDBHelper.DelLinkInfo(ContactID, ConstUtil.TABLE_NAME_CONTACT);
            }
            catch (Exception ex)
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_CONTACT_LIST;
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

        #region 获取所有延期未联络客户信息的方法
        /// <summary>
        /// 获取所有延期未联络的
        /// </summary>
        /// <returns></returns>
        public static DataTable GetContactDefer(string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ContactHistoryDBHelper.GetContactDefer(CompanyCD,pageIndex,pageCount,ord,ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 客户联络一览表__报表
        /// <summary>
        /// 根据查询条件获取客户联络一览表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns>结果集</returns>
        public static DataTable GetContactInfoBycondition(string CustName,string CompanyCD,string  LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ContactHistoryDBHelper.GetContactInfoBycondition(CustName,CompanyCD,LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }

        /// <summary>
        /// 根据查询条件获取客户联络一览表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns>结果集</returns>
        public static DataTable GetContactInfoByconditionPrint(string CustName, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return ContactHistoryDBHelper.GetContactInfoByconditionPrint(CustName, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch 
            {
                return null;
            }
        } 

        #endregion

        #region  客户联络次数统计_报表
        /// <summary>
        /// 客户联络次数统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCust(string CustName, string CompanyCD, string  LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            { 
                return ContactHistoryDBHelper.GetStatContactNumByCust(CustName,CompanyCD,LinkDateBegin,LinkDateEnd,pageIndex,pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }


        /// <summary>
        /// 客户联络次数统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCustPrint(string CustName, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return ContactHistoryDBHelper.GetStatContactNumByCustPrint(CustName, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch 
            {
                return null;
            }
        }


        #endregion

        #region  客户联络事由统计_报表
        /// <summary>
        /// 客户联络事由统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkReasonId">联络事由ID</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCustAndReason(string CustName,string LinkReasonId,string CompanyCD,string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ContactHistoryDBHelper.GetStatContactNumByCustAndReason(CustName,LinkReasonId,CompanyCD,LinkDateBegin,LinkDateEnd,pageIndex,pageCount,ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }

         /// <summary>
        /// 客户联络事由统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkReasonId">联络事由ID</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCustAndReasonPrint(string CustName, string LinkReasonId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return ContactHistoryDBHelper.GetStatContactNumByCustAndReasonPrint(CustName, LinkReasonId, CompanyCD, LinkDateBegin, LinkDateEnd,ord);
            }
            catch 
            {
                return null;
            } 
        }

        public static DataTable GetStatContactNumByCustAndReasonPrint(string CustName, string LinkReasonId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord,bool isreason)
        {
            try
            {
                return ContactHistoryDBHelper.GetStatContactNumByCustAndReasonPrint(CustName, LinkReasonId, CompanyCD, LinkDateBegin, LinkDateEnd, ord,isreason);
            }
            catch 
            {
                return null;
            }
        }

        #endregion

        #region  客户联络方式统计_报表
        /// <summary>
        /// 客户联络方式统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkReasonId">联络事由ID</param>
        /// <param name="LinkModeId">联系方式ID</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCustAndLinkMode(string CustName, string LinkReasonId, string LinkModeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
               return ContactHistoryDBHelper.GetStatContactNumByCustAndLinkMode(CustName,LinkReasonId,LinkModeId,CompanyCD,LinkDateBegin,LinkDateEnd, pageIndex,pageCount,ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }

        /// <summary>
        /// 客户联络方式统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkReasonId">联络事由ID</param>
        /// <param name="LinkModeId">联系方式ID</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCustAndLinkModePrint(string CustName, string LinkReasonId, string LinkModeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return ContactHistoryDBHelper.GetStatContactNumByCustAndLinkModePrint(CustName, LinkReasonId, LinkModeId, CompanyCD, LinkDateBegin, LinkDateEnd,ord);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region  客户联络人统计_报表
        /// <summary>
        /// 客户联络人统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkReasonId">联络人ID</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCustAndLinkMan(string CustName, string Linker,string CompanyCD,string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                 return ContactHistoryDBHelper.GetStatContactNumByCustAndLinkMan(CustName,Linker,CompanyCD,LinkDateBegin,LinkDateEnd,pageIndex,pageCount,ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }
        /// <summary>
        /// 客户联络人统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkReasonId">联络人ID</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCustAndLinkManPrint(string CustName, string Linker, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return ContactHistoryDBHelper.GetStatContactNumByCustAndLinkManPrint(CustName, Linker, CompanyCD, LinkDateBegin, LinkDateEnd,ord);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region  未联络客户统计_报表
        /// <summary>
        /// 未联络客户统计
        /// </summary>
        /// <param name="CustName">公司ID</param>
        /// <param name="LinkReasonId">天数</param>
        /// <returns></returns>
        public static DataTable GetStatCustByDays(string CompanyCD, int Days, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ContactHistoryDBHelper.GetStatCustByDays(CompanyCD,Days,pageIndex,pageCount,ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }

        /// <summary>
        /// 未联络客户统计
        /// </summary>
        /// <param name="CustName">公司ID</param>
        /// <param name="LinkReasonId">天数</param>
        /// <returns></returns>
        public static DataTable GetStatCustByDaysPrint(string CompanyCD, int Days,string ord)
        {
            try
            {
                return ContactHistoryDBHelper.GetStatCustByDaysPrint(CompanyCD, Days,ord);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 客户联络信息打印
        /// <summary>
        /// 客户联络信息打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="contactid"></param>
        /// <returns></returns>
        public static DataTable PrintContactInfoByID(string CompanyCD, string contactid)
        {
            return ContactHistoryDBHelper.PrintContactInfoByID(CompanyCD, contactid);
        }
        #endregion

        #region 导出客户联络列表信息
        /// <summary>
        /// 导出客户联络列表信息
        /// </summary>
        /// <param name="CustName"></param>
        /// <param name="CustLinkMan"></param>
        /// <param name="ContactM"></param>
        /// <param name="LinkDateBegin"></param>
        /// <param name="txtLinkDateEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportContactInfo(string CanUserID, string CustID, string CustLinkMan, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            return ContactHistoryDBHelper.ExportContactInfo(CanUserID,CustID, CustLinkMan, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
        }
        #endregion
    }
}

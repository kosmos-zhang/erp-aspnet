using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Common;
using XBase.Common;
using XBase.Model.Office.CustManager;
using XBase.Data.Office.CustManager;
using XBase.Business.Common;
using XBase.Data.Common;
using System.Data;

namespace XBase.Business.Office.CustManager
{
    public class ComplainBus
    {        
        #region 添加客户投诉信息的方法
        /// <summary>
        /// 添加客户投诉信息的方法
        /// </summary>
        /// <param name="CustComplainM">客户投诉信息</param>
        /// <returns>投诉信息ID</returns>
        public static int CustComplainAdd(CustComplainModel CustComplainM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_COMPLAIN_ADD;
            //操作单据编号  投诉单编号
            logModel.ObjectID = CustComplainM.ComplainNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_COMPLAIN;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_INSERT;
            #endregion

            try
            {
                isSucc = ComplainDBHelper.CustComplainAdd(CustComplainM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_COMPLAIN_ADD;
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

        #region 修改客户投诉信息的方法
        /// <summary>
        /// 修改客户投诉信息的方法
        /// </summary>
        /// <param name="CustComplainM">客户投诉信息</param>
        /// <returns>操作结果</returns>
        public static bool UpdateComplain(CustComplainModel CustComplainM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_COMPLAIN_LIST;
            //操作单据编号  投诉单编号
            logModel.ObjectID = CustComplainM.ComplainNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_COMPLAIN;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_UPDATE;
            #endregion

            try
            {
                isSucc = ComplainDBHelper.UpdateComplain(CustComplainM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_COMPLAIN_LIST;
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

        #region 根据条件检索客户
        /// <summary>
        /// 根据条件检索客户
        /// </summary>
        /// <param name="CustName">投诉客户</param>
        /// <param name="CustComplainM">投诉信息</param>
        /// <param name="ComplainBegin">投诉开始时间</param>
        /// <param name="ComplainEnd">投诉结束时间</param>
        /// <param name="CustLinkMan">客户联系人</param>
        /// <param name="DestClerk">接待人</param>
        /// <returns></returns>
        public static DataTable GetComplainInfoBycondition(string CanUserID, string CustName, CustComplainModel CustComplainM, string ComplainBegin, string ComplainEnd, string CustLinkMan, string DestClerk, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return ComplainDBHelper.GetComplainInfoBycondition(CanUserID,CustName, CustComplainM, ComplainBegin, ComplainEnd, CustLinkMan, DestClerk, pageIndex, pageCount, ord, ref totalCount);
        }
        public static DataTable GetComplainInfoComplainType(string ComplainType, string CustName, CustComplainModel CustComplainM, string ComplainBegin, string ComplainEnd, string CustLinkMan, string DestClerk, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return ComplainDBHelper.GetComplainInfoComplainType( ComplainType,  CustName,  CustComplainM,  ComplainBegin,  ComplainEnd,  CustLinkMan,  DestClerk,  pageIndex,  pageCount,  ord, ref  totalCount);
        }
        public static DataTable GetComplainInfoByComplainPerson(string ComplainPerson,string CustName, CustComplainModel CustComplainM, string ComplainBegin, string ComplainEnd, string CustLinkMan, string DestClerk, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return ComplainDBHelper.GetComplainInfoByComplainPerson(ComplainPerson, CustName,  CustComplainM,  ComplainBegin,  ComplainEnd,  CustLinkMan,  DestClerk,  pageIndex,  pageCount,  ord, ref  totalCount);
        }
        #endregion

        #region 根据服务编号批量删除客户投诉信息
        /// <summary>
        /// 根据投诉ID批量删除客户投诉信息
        /// </summary>
        /// <param name="ServiceID">投诉ID</param>
        /// <returns>操作记录数</returns>
        public static int DelComplainInfo(string[] ComplainID)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_COMPLAIN_LIST;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < ComplainID.Length; i++)
            {
                sb.Append(ComplainID[i] + ";");
            }

            //操作单据编号  联系人姓名
            logModel.ObjectID = "客户投诉ID:" + sb.ToString();
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_COMPLAIN;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_DELETE;
            #endregion

            try
            {
                isSucc = LinkManDBHelper.DelLinkInfo(ComplainID, ConstUtil.TABLE_NAME_COMPLAIN);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_COMPLAIN_LIST;
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

        #region 根据投诉ID获取此条投诉信息
        /// <summary>
        /// 根据投诉ID获取此条投诉信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="ComplainID">投诉ID</param>
        /// <returns>返回记录</returns>
        public static DataTable GetComplainInfoByID(string CompanyCD, int ComplainID)
        {
            return ComplainDBHelper.GetComplainInfoByID(CompanyCD, ComplainID);
        }
        #endregion

        #region 投诉一览表_报表
        /// <summary>
        /// 投诉一览表_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">投诉分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetComplainList(string CustName, string TypeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ComplainDBHelper.GetComplainList(CustName, TypeId, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }

        public static DataTable GetComplainListPrint(string CustName, string TypeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return ComplainDBHelper.GetComplainListPrint(CustName, TypeId, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 投诉次数统计_报表
        /// <summary>
        /// 投诉次数统计_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">投诉分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetComplainCount(string CustName, string TypeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ComplainDBHelper.GetComplainCount(CustName, TypeId, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }

        public static DataTable GetComplainCountPrint(string CustName, string TypeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return ComplainDBHelper.GetComplainCountPrint(CustName, TypeId, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 投诉分类统计_报表
        /// <summary>
        /// 投诉分类统计_报表
        /// </summary>
        /// <param name="TypeId">投诉分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetComplainByType(string TypeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ComplainDBHelper.GetComplainByType(TypeId, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }

        public static DataTable GetComplainByTypePrint(string TypeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return ComplainDBHelper.GetComplainByTypePrint(TypeId, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 被投诉人统计_报表
        /// <summary>
        /// 被投诉人统计_报表
        /// </summary>
        /// <param name="TypeId">投诉人ID</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetComplainByMan(string ComplainedMan, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ComplainDBHelper.GetComplainByMan(ComplainedMan, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }


        public static DataTable GetComplainByManPrint(string ComplainedMan, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return ComplainDBHelper.GetComplainByManPrint(ComplainedMan, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch 
            {
                return null;
            }
        }

        /// <summary>
        /// 被投诉人统计_报表
        /// </summary>
        /// <param name="TypeId">投诉人ID</param>
        /// <param name="TypeId">投诉人ID</param>
        /// <param name="TypeId">投诉人ID</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetComplainByTimeBehaviour(string CustName, string ComplainedMan, string GroupBy, string TimeIndex, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ComplainDBHelper.GetComplainByTimeBehaviour(CustName, ComplainedMan, GroupBy, TimeIndex, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }
        public static DataTable GetComplainByTimeBehaviour(string CustName, string ComplainedMan, string GroupBy, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ComplainDBHelper.GetComplainByTimeBehaviour(CustName, ComplainedMan, GroupBy, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }
        public static DataTable GetComplainByTimeBehaviourPrint(string CustName, string ComplainedMan, string GroupBy, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return ComplainDBHelper.GetComplainByTimeBehaviourPrint(CustName, ComplainedMan, GroupBy, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch 
            {
                return null;
            }
        }

        #endregion

        #region 零投诉客户统计_报表
        /// <summary>
        /// 零投诉客户统计_报表
        /// </summary>
        /// <param name="Days">天数</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <returns>记录集</returns>
        public static DataTable GetComplainByDays(string Days, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
               return ComplainDBHelper.GetComplainByDays(Days, CompanyCD,pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }

        public static DataTable GetComplainByDaysPrint(string Days, string CompanyCD, string ord)
        {
            try
            {
                return ComplainDBHelper.GetComplainByDaysPrint(Days, CompanyCD, ord);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 客户投诉信息打印
        /// <summary>
        /// 客户投诉信息打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ComplainID"></param>
        /// <returns></returns>
        public static DataTable PrintComplain(string CompanyCD, string ComplainID)
        {
            return ComplainDBHelper.PrintComplain(CompanyCD, ComplainID);
        }
        #endregion
         
        #region 导出客户投诉信息
        /// <summary>
        /// 导出客户投诉信息
        /// </summary>
        /// <param name="CustID"></param>
        /// <param name="CustComplainM"></param>
        /// <param name="ComplainBegin"></param>
        /// <param name="ComplainEnd"></param>
        /// <param name="CustLinkMan"></param>
        /// <param name="DestClerk"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportComplainInfo(string CanUserID,string CustID, CustComplainModel CustComplainM, string ComplainBegin, string ComplainEnd, string CustLinkMan, string DestClerk, string ord)
        {
            return ComplainDBHelper.ExportComplainInfo(CanUserID,CustID, CustComplainM, ComplainBegin, ComplainEnd, CustLinkMan, DestClerk, ord);
        }
        #endregion
    }
}

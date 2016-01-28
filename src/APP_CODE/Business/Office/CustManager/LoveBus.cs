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
    public class LoveBus
    {        
        #region 添加客户关怀信息的方法
        /// <summary>
        /// 添加客户关怀信息的方法
        /// </summary>
        /// <param name="CustLoveM">客户关怀信息</param>
        /// <returns>操作记录数</returns>
        public static int CustLoveAdd(CustLoveModel CustLoveM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_LOVE_ADD;
            //操作单据编号  关怀单编号
            logModel.ObjectID = CustLoveM.LoveNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_LOVE;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_INSERT;
            #endregion

            try
            {
                isSucc = LoveDBHelper.CustLoveAdd(CustLoveM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_LOVE_ADD;
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

        #region 根据客户关怀ID修改关怀信息
        /// <summary>
        /// 根据客户关怀ID修改关怀信息
        /// </summary>
        /// <param name="CustLoveM">客户关怀信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateLove(CustLoveModel CustLoveM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_LOVE_LIST;
            //操作单据编号  关怀单编号
            logModel.ObjectID = CustLoveM.LoveNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_LOVE;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_UPDATE;
            #endregion

            try
            {
                isSucc = LoveDBHelper.UpdateLove(CustLoveM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_LOVE_LIST;
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

        #region 根据条件查询客户关怀
        /// <summary>
        /// 根据条件查询客户关怀
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="CustLoveM">关怀信息</param>
        /// <param name="LoveBegin">开始时间</param>
        /// <param name="LoveEnd">结束时间</param>
        /// <param name="CustLinkMan">客户联系人</param>
        /// <returns>查询结果</returns>
        public static DataTable GetLoveInfoBycondition(string CanUserID, string CustName, CustLoveModel CustLoveM, string LoveBegin, string LoveEnd, string CustLinkMan, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return LoveDBHelper.GetLoveInfoBycondition(CanUserID,CustName, CustLoveM, LoveBegin, LoveEnd, CustLinkMan, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

        #region 根据关怀ID获取此条关怀信息
        /// <summary>
        /// 根据关怀ID获取此条关怀信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="LoveID">关怀ID</param>
        /// <returns></returns>
        public static DataTable GetLoveInfoByID(string CompanyCD, int LoveID)
        {
            return LoveDBHelper.GetLoveInfoByID(CompanyCD, LoveID);
        }
        #endregion

        public static int DelLoveInfo(string[] LoveID)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_LOVE_LIST;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < LoveID.Length; i++)
            {
                sb.Append(LoveID[i] + ";");
            }

            //操作单据编号  联系人姓名
            logModel.ObjectID = "客户关怀ID:" + sb.ToString();
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_LOVE;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_DELETE;
            #endregion

            try
            {
                isSucc = LinkManDBHelper.DelLinkInfo(LoveID, ConstUtil.TABLE_NAME_LOVE);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_LOVE_LIST;
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

        #region 关怀一览表_报表
        /// <summary>
        /// 关怀一览表_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">关怀分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetLoveList(string CustName, string LoveType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return LoveDBHelper.GetLoveList(CustName, LoveType, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }

        public static DataTable GetLoveListPrint(string CustName, string LoveType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return LoveDBHelper.GetLoveListPrint(CustName, LoveType, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 关怀次数统计_报表
        /// <summary>
        /// 关怀次数统计_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">服务分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetCustLoveCount(string CustName, string LoveType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return LoveDBHelper.GetCustLoveCount(CustName, LoveType, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }

        public static DataTable GetCustLoveCountPrint(string CustName, string LoveType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return LoveDBHelper.GetCustLoveCountPrint(CustName, LoveType, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 按关怀分类统计_报表
        /// <summary>
        /// 按关怀分类统计_报表
        /// </summary>
        /// <param name="TypeId">服务分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetLoveByType(string LoveType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return LoveDBHelper.GetCustLoveByType(LoveType, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }

        public static DataTable GetLoveByTypePrint(string LoveType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return LoveDBHelper.GetCustLoveByTypePrint(LoveType, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 按关怀执行人统计_报表
        /// <summary>
        /// 按关怀执行人统计_报表
        /// </summary>
        /// <param name="TypeId">执行人ID</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetLoveByMan(string Linker, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return LoveDBHelper.GetLoveByMan(Linker, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }

        public static DataTable GetLoveByManPrint(string Linker, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return LoveDBHelper.GetLoveByManPrint(Linker, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 未关怀客户统计_报表
        /// <summary>
        /// 未关怀客户统计_报表
        /// </summary>
        /// <param name="Days">天数</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <returns>记录集</returns>
        public static DataTable GetLoveByDays(string Days, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return LoveDBHelper.GetLoveByDays(Days, CompanyCD, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }

        public static DataTable GetLoveByDaysPrint(string Days, string CompanyCD, string ord)
        {
            try
            {
                return LoveDBHelper.GetLoveByDaysPrint(Days, CompanyCD, ord);
            }
            catch 
            {
                return null;
            }
        }
        #endregion
         
        #region 客户关怀信息打印
        /// <summary>
        /// 客户关怀信息打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="LoveID"></param>
        /// <returns></returns>
        public static DataTable PrintLove(string CompanyCD, string LoveID)
        {
            return LoveDBHelper.PrintLove(CompanyCD, LoveID);
        }
        #endregion

        #region 导出客户关怀信息列表
        /// <summary>
        /// 导出客户关怀信息列表
        /// </summary>
        /// <param name="CustName"></param>
        /// <param name="CustLoveM"></param>
        /// <param name="LoveBegin"></param>
        /// <param name="LoveEnd"></param>
        /// <param name="CustLinkMan"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportLoveInfo(string CanUserID,string CustID, CustLoveModel CustLoveM, string LoveBegin, string LoveEnd, string CustLinkMan, string ord)
        {
            return LoveDBHelper.ExportLoveInfo(CanUserID,CustID, CustLoveM, LoveBegin, LoveEnd, CustLinkMan, ord);
        }
        #endregion
    }
}

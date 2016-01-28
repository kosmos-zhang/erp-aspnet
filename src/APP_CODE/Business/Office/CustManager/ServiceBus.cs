using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.Office.CustManager;
using XBase.Model.Common;
using XBase.Common;
using XBase.Model.Office.CustManager;
using XBase.Business.Common;
using XBase.Data.Common;
using System.Data;

namespace XBase.Business.Office.CustManager
{
    public class ServiceBus
    {
        #region 添加客户服务信息的方法
        /// <summary>
        /// 添加客户服务信息的方法
        /// </summary>
        /// <param name="CustServiceM">客户服务信息</param>
        /// <returns>返回操作记录值</returns>
        public static int CustServiceAdd(CustServiceModel CustServiceM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_SERVICE_ADD;
            //操作单据编号  服务单编号
            logModel.ObjectID = CustServiceM.ServeNO;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_SERVICE;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_INSERT;
            #endregion

            try
            {
                isSucc = ServiceDBHelper.CustServiceAdd(CustServiceM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_SERVICE_ADD;
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

        #region 修改客户服务信息的方法
        /// <summary>
        /// 修改客户服务信息的方法
        /// </summary>
        /// <param name="CustServiceM">服务信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateService(CustServiceModel CustServiceM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_SERVICE_LIST;
            //操作单据编号  客户服务单编号
            logModel.ObjectID = CustServiceM.ServeNO;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_SERVICE;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_UPDATE;
            #endregion
            try
            {
                isSucc = ServiceDBHelper.UpdateService(CustServiceM);
            }
            catch(Exception ex)
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_SERVICE_LIST;
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

        #region 根据查询条件获取客户服务信息
        /// <summary>
        /// 根据查询条件获取客户服务信息
        /// </summary>
        /// <param name="CustName">客户名</param>
        /// <param name="CustServiceM">服务信息</param>
        /// <param name="ServiceDateBegin">开始时间</param>
        /// <param name="ServiceDateEnd">结束时间</param>
        /// <param name="CustLinkMan">客户联系人</param>
        /// <param name="Executant">执行人</param>
        /// <returns>查询结果</returns>
        public static DataTable GetServiceInfoServerType(string ServerType, string CustName, CustServiceModel CustServiceM, string ServiceDateBegin, string ServiceDateEnd, string Executant, string CustLinkMan, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            try
            {
                return ServiceDBHelper.GetServiceInfoServerType(ServerType, CustName, CustServiceM, ServiceDateBegin, ServiceDateEnd, Executant, CustLinkMan, pageIndex, pageCount, ord, ref totalCount);
            }
            catch
            {
                return null;
            }
        }
        public static DataTable GetServiceInfoByServerPerson(string ServerPerson, string CustName, CustServiceModel CustServiceM, string ServiceDateBegin, string ServiceDateEnd, string Executant, string CustLinkMan, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            try
            {
                return ServiceDBHelper.GetServiceInfoByServerPerson(ServerPerson, CustName, CustServiceM, ServiceDateBegin, ServiceDateEnd, Executant, CustLinkMan, pageIndex, pageCount, ord, ref totalCount);
            }
            catch
            {
                return null;
            }
        }
        public static DataTable GetServiceInfoBycondition(string CanUserID, string CustName, CustServiceModel CustServiceM, string ServiceDateBegin, string ServiceDateEnd, string Executant, string CustLinkMan, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            try
            {
                return ServiceDBHelper.GetServiceInfoBycondition(CanUserID,CustName, CustServiceM, ServiceDateBegin, ServiceDateEnd, Executant, CustLinkMan, pageIndex, pageCount, ord, ref totalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 根据服务ID获取此条客户服务信息
        /// <summary>
        /// 根据服务ID获取此条客户服务信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="serviceid">服务信息ID</param>
        /// <returns>返回一条结果记录</returns>
        public static DataTable GetServiceInfoByID(string CompanyCD, int serviceid)
        {
            try
            {
                return ServiceDBHelper.GetServiceInfoByID(CompanyCD, serviceid);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 根据服务编号批量删除客户服务信息
        /// <summary>
        /// 根据服务编号批量删除客户服务信息
        /// </summary>
        /// <param name="ServiceID">服务ID</param>
        /// <returns>操作记录数</returns>
        public static int DelServiceInfo(string[] ServiceID)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_SERVICE_LIST;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < ServiceID.Length; i++)
            {
                sb.Append(ServiceID[i] + ";");
            }

            //操作单据编号  联系人姓名
            logModel.ObjectID = "客户服务ID:" + sb.ToString();
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_SERVICE;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_DELETE;
            #endregion

            try
            {
                isSucc = LinkManDBHelper.DelLinkInfo(ServiceID, ConstUtil.TABLE_NAME_SERVICE);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_SERVICE_LIST;
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

        #region 获取产品销售记录的方法
        /// <summary>
        /// 获取产品销售记录的方法
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>查询结果</returns>
        public static DataTable GetSellAnnal(string CompanyCD, string CustID, string ProductID, string DateBegin, string DateEnd)
        {
            try
            {
                return ServiceDBHelper.GetSellAnnal(CompanyCD,CustID,ProductID,DateBegin,DateEnd);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 获取产品销售记录列表（分页）
        /// <summary>
        /// 获取产品销售记录列表（分页）
        /// </summary>
        /// <param name="userinfo">用户session信息</param>
        /// <param name="CustID">检索条件：客户</param>
        /// <param name="ProductID">检索条件：物品</param>
        /// <param name="DateBegin">检索条件：开始时间</param>
        /// <param name="DateEnd">检索条件：结束时间</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetSellAnnalList(XBase.Common.UserInfoUtil userinfo, string CustID, string ProductID, string DateBegin, string DateEnd, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            return ServiceDBHelper.GetSellAnnalList(userinfo, CustID, ProductID, DateBegin, DateEnd, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 服务一览表_报表
        /// <summary>
        /// 服务一览表_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">服务分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetServiceList(string CustName, string ServeType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ServiceDBHelper.GetServiceList(CustName, ServeType, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetServiceListPrint(string CustName, string ServeType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return ServiceDBHelper.GetServiceListPrint(CustName, ServeType, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 服务次数统计_报表
        /// <summary>
        /// 服务次数统计_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">服务分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetCustServiceCount(string CustName, string ServeType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ServiceDBHelper.GetCustServiceCount(CustName, ServeType, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetCustServiceCountPrint(string CustName, string ServeType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return ServiceDBHelper.GetCustServiceCountPrint(CustName, ServeType, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 按服务分类统计_报表
        /// <summary>
        /// 按服务分类统计_报表
        /// </summary>
        /// <param name="TypeId">服务分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetServiceByType(string ServeType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ServiceDBHelper.GetCustServiceByType(ServeType, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetServiceByTypePrint(string ServeType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return ServiceDBHelper.GetCustServiceByTypePrint(ServeType, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 按服务执行人统计_报表
        /// <summary>
        /// 按服务执行人统计_报表
        /// </summary>
        /// <param name="TypeId">执行人ID</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetServiceByMan(string Executant, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ServiceDBHelper.GetServiceByMan(Executant, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetServiceByManPrint(string Executant, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return ServiceDBHelper.GetServiceByManPrint(Executant, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 零服务客户统计_报表
        /// <summary>
        /// 零服务客户统计_报表
        /// </summary>
        /// <param name="Days">天数</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <returns>记录集</returns>
        public static DataTable GetServiceByDays(string Days, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return ServiceDBHelper.GetServiceByDays(Days, CompanyCD, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetServiceByDaysPrint(string Days, string CompanyCD, string ord)
        {
            try
            {
                return ServiceDBHelper.GetServiceByDaysPrint(Days, CompanyCD, ord);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 客户服务信息打印
        /// <summary>
        /// 客户服务信息打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="serviceid"></param>
        /// <returns></returns>
        public static DataTable PrintService(string CompanyCD, string serviceid)
        {
            return ServiceDBHelper.PrintService(CompanyCD, serviceid);
        }
        #endregion

        #region 导出客户服务信息
        /// <summary>
        /// 导出客户服务信息
        /// </summary>
        /// <param name="CustName"></param>
        /// <param name="CustServiceM"></param>
        /// <param name="ServiceDateBegin"></param>
        /// <param name="ServiceDateEnd"></param>
        /// <param name="Executant"></param>
        /// <param name="CustLinkMan"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportServiceInfo(string CanUserID,string CustID, CustServiceModel CustServiceM, string ServiceDateBegin, string ServiceDateEnd, string Executant, string CustLinkMan, string ord)
        {
            return ServiceDBHelper.ExportServiceInfo(CanUserID,CustID, CustServiceM, ServiceDateBegin, ServiceDateEnd, Executant, CustLinkMan, ord);
        }
        #endregion
    }
}

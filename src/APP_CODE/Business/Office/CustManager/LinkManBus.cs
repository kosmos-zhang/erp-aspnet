using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.CustManager;
using XBase.Data.Office.CustManager;
using System.Data;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;

namespace XBase.Business.Office.CustManager
{
    public class LinkManBus
    {       
          /// <summary>
        /// 获取联系人列表信息
        /// </summary>
        /// <param name="companyCD">companyCD</param>
        /// <returns></returns>
        public static DataTable GetLinkManListEx(string companyCD)
        {
            return LinkManDBHelper.GetLinkManListEx(companyCD);
        }

        #region 增加客户联系人的方法
        /// <summary>
        /// 增加客户联系人的方法
        /// </summary>
        /// <param name="CustModel">客户联系人Model</param>
        /// <returns>bool值</returns>
        public static int LinkManAdd(LinkManModel LinkManModel)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_LINK_ADD;
            //操作单据编号  联系人姓名
            logModel.ObjectID = LinkManModel.LinkManName;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_LISIMAN;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_INSERT;
            #endregion

            try
            {
                isSucc = LinkManDBHelper.LinkManAdd(LinkManModel);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_LINK_ADD;
                //描述
                logSys.Description = ex.ToString();
                //输出日志
                LogUtil.WriteLog(logSys);
                #endregion
            }

            if (isSucc >0 )//操作成功
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
     
        #region 根据客户编号修改联系人信息的方法
        /// <summary>
         /// 根据客户编号修改联系人信息的方法
        /// </summary>
        /// <param name="LinkManM">联系人信息Model</param>
        /// <param name="CustNo">联系人对应客户编号</param>
         /// <returns>bool值</returns>
         public static bool UpdateLinkMan(LinkManModel LinkManM)
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
             logModel.ModuleID = ConstUtil.MODULE_ID_CUST_LINK_LIST;
             //操作单据编号  联系人姓名
             logModel.ObjectID = LinkManM.LinkManName;
             //操作对象 操作的表信息
             logModel.ObjectName = ConstUtil.TABLE_NAME_LISIMAN;
             //涉及关键元素 涉及其他业务、表关系
             logModel.Element = string.Empty;
             //备注 操作类型
             logModel.Remark = ConstUtil.LOG_PROCESS_UPDATE;
             #endregion

             try
            {
                isSucc = LinkManDBHelper.UpdateLinkMan(LinkManM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_LINK_LIST;
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

        #region 根据联系人ID获取联系人信息的方法
        /// <summary>
        /// 根据联系人ID获取联系人信息的方法
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="linkid">联系人ID</param>
        /// <returns></returns>
        public static DataTable GetLinkInfoByID(string CompanyCD, int linkid)
        {
            try
            {
                return LinkManDBHelper.GetLinkInfoByID(CompanyCD, linkid);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据查询条件获取联系人列表信息的方法
         /// <summary>
         /// 根据查询条件获取联系人列表信息的方法
        /// </summary>
         /// <param name="LinkManM">查询条件</param>
         /// <param name="Manager">登陆人</param>
         /// <returns>联系人列表结果集</returns>
        public static DataTable GetLinkManInfoBycondition(string CustNam, LinkManModel LinkManM, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
            try
            {
                return LinkManDBHelper.GetLinkManInfoBycondition(CustNam, LinkManM,BeginDate,EndDate, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch 
            {
                return null;
            }
       }
        #endregion

        #region 根据天数查询联系人生日
         /// <summary>
        /// 根据天数查询联系人生日
        /// </summary>
        /// <param name="Days"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetLinkRemind(string Days, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return LinkManDBHelper.GetLinkRemind(Days, CompanyCD, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 批量删除某张表中的记录
        /// <summary>
        /// 批量删除某张表中的记录
        /// </summary>
        /// <param name="LinkID"></param>
        /// <param name="TabelName">表名</param>
        /// <returns>操作记录数</returns>
        public static int DelLinkInfo(string[] LinkID)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_LINK_LIST;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < LinkID.Length; i++)
            {
                //LinkID[i] = "'" + LinkID[i] + "'";
                sb.Append(LinkID[i] + ";");
            }

            //操作单据编号  联系人姓名
            logModel.ObjectID = "联系人ID:" + sb.ToString();
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_LISIMAN;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_DELETE;
            #endregion

            try
            {
                isSucc = LinkManDBHelper.DelLinkInfo(LinkID, "officedba.custlinkman");
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_LINK_LIST;
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

        #region 根据客户编号获取联系人姓名
         /// <summary>
        /// 根据客户编号获取联系人姓名
        /// </summary>
         /// <param name="CompanyCD">公司代码</param>
        /// <param name="CustNo">客户编号</param>
        /// <returns>联系人列表</returns>
         public static DataTable GetLinkManByCustNo(string LinkManName,string Appellation,string Department,string CompanyCD, string CustNo)
         {
             try
             {
                 return LinkManDBHelper.GetLinkManByCustNo(LinkManName, Appellation, Department, CompanyCD, CustNo);
             }
             catch 
             {
                 return null;
             }
         }
        #endregion

        #region 根据联系人ID获取是否含有联系人的方法
         /// <summary>
         /// 根据联系人ID获取是否含有联系人的方法
         /// </summary>
         /// <param name="LinkManID"></param>
         /// <returns></returns>
         public static bool GetLinkManByID(string[] LinkManID)
         {
             return LinkManDBHelper.GetLinkManByID(LinkManID);
         }
        #endregion

        #region 联系人信息打印
        /// <summary>
        /// 联系人信息打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="linkid"></param>
        /// <returns></returns>
         public static DataTable PrintLink(string CompanyCD, string linkid)
         {
             return LinkManDBHelper.PrintLink(CompanyCD, linkid);
         }
        #endregion

        #region 导出联系人列表
        /// <summary>
        /// 导出联系人列表
        /// </summary>
        /// <param name="CustNam"></param>
        /// <param name="LinkManM"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
         public static DataTable ExportLinkManInfo(string CustNam, LinkManModel LinkManM, string BeginDate, string EndDate, string ord)
         {
             return LinkManDBHelper.ExportLinkManInfo(CustNam, LinkManM, BeginDate, EndDate, ord);
         }
        #endregion
    }
}

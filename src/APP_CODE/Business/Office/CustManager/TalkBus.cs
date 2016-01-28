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
//using XBase.Data.Common;

namespace XBase.Business.Office.CustManager
{
    public class TalkBus
    {
        #region 新建洽谈信息的方法
        /// <summary>
        /// 新建洽谈信息的方法
        /// </summary>
        /// <param name="CustTalkM">洽谈信息</param>
        /// <returns>返回洽谈ID</returns>
        public static int CustTalkAdd(CustTalkModel CustTalkM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_TALK_ADD;
            //操作单据编号  关怀单编号
            logModel.ObjectID = CustTalkM.TalkNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_TALK;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_INSERT;
            #endregion

            try
            {
                isSucc = TalkDBHelper.CustTalkAdd(CustTalkM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_TALK_ADD;
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

        #region 修改客户洽谈信息
        /// <summary>
        /// 修改客户洽谈信息
        /// </summary>
        /// <param name="CustTalkM">客户洽谈信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateTalk(CustTalkModel CustTalkM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_TALK_LIST;
            //操作单据编号  编号
            logModel.ObjectID = CustTalkM.TalkNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_TALK;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_UPDATE;
            #endregion

            try
            {
                isSucc = TalkDBHelper.UpdateTalk(CustTalkM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_TALK_LIST;
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

        #region 根据条件查询客户洽谈信息的方法
        /// <summary>
        /// 根据条件查询客户洽谈信息的方法
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="CustTalkM">客户洽谈信息</param>
        /// <param name="TalkBegin">开始时间</param>
        /// <param name="TalkEnd">结束时间</param>
        /// <returns>返回查询结果</returns>
        public static DataTable GetTalkInfoBycondition(string CanUserID, string CustName, CustTalkModel CustTalkM, string TalkBegin, string TalkEnd, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            //return TalkDBHelper.GetTalkInfoBycondition(CustName, CustTalkM, TalkBegin, TalkEnd);

            //string Linker = "";//单个ID
            string LinkerIds = "";//每条记录多个ID
            string EmployeeNames = "";//多个员工姓名

            DataTable dt = TalkDBHelper.GetTalkInfoBycondition(CanUserID,CustName, CustTalkM, TalkBegin, TalkEnd, pageIndex, pageCount, ord, ref totalCount);

            DataColumn LinkerName = new DataColumn();
            dt.Columns.Add("LinkerName");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                LinkerIds = dt.Rows[i]["Linker"].ToString();

                string[] LinkerList = LinkerIds.Split(',');

                for (int j = 0; j < LinkerList.Length; j++)
                {
                    //获取参与人ID
                    int inputID = Convert.ToInt32(LinkerList[j]);
                    //调用方法取name
                    EmployeeNames = EmployeeNames + "," + EmployeeDBHelper.GetEmployeeNameByID(inputID, CustTalkM.CompanyCD);                    
                }

                //插入EmployeeNames到一条记录
                dt.Rows[i]["LinkerName"] = EmployeeNames.Substring(1);
                EmployeeNames = "";
            }

            return dt;
            
        }
        #endregion

        #region 根据ID删除客户洽谈的方法
        public static int DelTalkInfo(string[] TalkID)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_TALK_LIST;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < TalkID.Length; i++)
            {
                sb.Append(TalkID[i] + ";");
            }

            //操作单据编号  
            logModel.ObjectID = "客户洽谈ID:" + sb.ToString();
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_TALK;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_DELETE;
            #endregion

            try
            {
                isSucc = LinkManDBHelper.DelLinkInfo(TalkID, ConstUtil.TABLE_NAME_TALK);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_TALK_LIST;
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

        #region 根据洽谈ID获取洽谈信息的方法
        /// <summary>
        /// 根据洽谈ID获取洽谈信息的方法
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="TalkID">洽谈ID</param>
        /// <returns>返回洽谈信息</returns>
        public static DataTable GetTalkInfoByID(string CompanyCD, int TalkID)
        {
            //return TalkDBHelper.GetTalkInfoByID(CompanyCD, TalkID);

            string Linker = "";//单个ID
            string LinkerIds = "";//每条记录多个ID
            string EmployeeNames = "";//多个员工姓名

            DataTable dt = TalkDBHelper.GetTalkInfoByID(CompanyCD, TalkID);

            DataColumn LinkerName = new DataColumn();
            dt.Columns.Add("LinkerName");
            dt.Columns.Add("LinkerID");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                LinkerIds = dt.Rows[i]["Linker"].ToString();

                string[] LinkerList = LinkerIds.Split(',');

                for (int j = 0; j < LinkerList.Length; j++)
                {
                    //获取参与人ID
                    int inputID = Convert.ToInt32(LinkerList[j]);
                    //调用方法取name
                    EmployeeNames = EmployeeNames + "," + EmployeeDBHelper.GetEmployeeNameByID(inputID, CompanyCD);
                    Linker = Linker + "," + inputID; 
                }

                //插入EmployeeNames到一条记录
                dt.Rows[i]["LinkerName"] = EmployeeNames.Substring(1);
                dt.Rows[i]["LinkerID"] = Linker.Substring(1);
                //EmployeeNames = "";
            }

            return dt;
        }
        #endregion

        #region 洽谈一览表_报表
        /// <summary>
        /// 洽谈一览表_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">洽谈方式</param>
        /// <param name="TypeId">优先级别</param>
        /// <param name="TypeId">状态</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetTalkList(string CustName, string TalkType, string Priority, string Status, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return TalkDBHelper.GetTalkList(CustName, TalkType,Priority,Status,CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetTalkListPrint(string CustName, string TalkType, string Priority, string Status, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return TalkDBHelper.GetTalkListPrint(CustName, TalkType, Priority, Status, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 按洽谈次数统计_报表
        /// <summary>
        /// 按洽谈次数统计_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        /// 
        public static DataTable GetCustTalkCount(string CustName,string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return TalkDBHelper.GetCustTalkCount(CustName,CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetCustTalkCountPrint(string CustName,string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return TalkDBHelper.GetCustTalkCountPrint(CustName,CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 按洽谈方式统计_报表
        /// <summary>
        /// 按洽谈方式统计_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TalkType">洽谈方式</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetTalkByType(string CustName, string TalkType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return TalkDBHelper.GetCustTalkByType(CustName,TalkType, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetTalkByTypePrint(string CustName,string TalkType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return TalkDBHelper.GetCustTalkByTypePrint(CustName,TalkType, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 按优先级别统计_报表
        /// <summary>
        /// 按优先级别统计_报表
        /// </summary>
        /// <param name="TypeId">客户名称</param>
        /// <param name="Priority">优先级别</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetCustTalkByPriority(string CustName, string Priority, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return TalkDBHelper.GetCustTalkByPriority(CustName, Priority, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetCustTalkByPriorityPrint(string CustName, string Priority, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return TalkDBHelper.GetCustTalkByPriorityPrint(CustName, Priority, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 按洽谈人统计_报表
        /// <summary>
        /// 按洽谈人统计_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">执行人ID</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetTalkByMan(string CustName, string Linker, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return TalkDBHelper.GetTalkByMan(CustName,Linker, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetTalkByManPrint(string CustName,string Linker, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                return TalkDBHelper.GetTalkByManPrint(CustName,Linker, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 未洽谈客户统计_报表
        /// <summary>
        /// 未洽谈客户统计_报表
        /// </summary>
        /// <param name="Days">天数</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <returns>记录集</returns>
        public static DataTable GetTalkByDays(string Days, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return TalkDBHelper.GetTalkByDays(Days, CompanyCD, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetTalkByDaysPrint(string Days, string CompanyCD, string ord)
        {
            try
            {
                return TalkDBHelper.GetTalkByDaysPrint(Days, CompanyCD, ord);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 客户洽谈打印
        /// <summary>
        /// 客户洽谈打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="TalkID"></param>
        /// <returns></returns>
        public static DataTable PrintTalk(string CompanyCD, string TalkID)
        {
            //return TalkDBHelper.PrintTalk(CompanyCD, TalkID);

            string LinkerIds = "";//每条记录多个ID
            string EmployeeNames = "";//多个员工姓名

            DataTable dt = TalkDBHelper.PrintTalk(CompanyCD, TalkID);

            DataColumn LinkerName = new DataColumn();
            dt.Columns.Add("LinkerName");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                LinkerIds = dt.Rows[i]["Linker"].ToString();

                string[] LinkerList = LinkerIds.Split(',');

                for (int j = 0; j < LinkerList.Length; j++)
                {
                    //获取参与人ID
                    int inputID = Convert.ToInt32(LinkerList[j]);
                    //调用方法取name
                    EmployeeNames = EmployeeNames + "," + EmployeeDBHelper.GetEmployeeNameByID(inputID, CompanyCD);
                }

                //插入EmployeeNames到一条记录
                dt.Rows[i]["LinkerName"] = EmployeeNames.Substring(1);
                EmployeeNames = "";
            }
            return dt;
        }
        #endregion

        #region 打印客户洽谈信息
        /// <summary>
        /// 打印客户洽谈信息
        /// </summary>
        /// <param name="CustID"></param>
        /// <param name="CustTalkM"></param>
        /// <param name="TalkBegin"></param>
        /// <param name="TalkEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportTalkInfo(string CanUserID,string CustID, CustTalkModel CustTalkM, string TalkBegin, string TalkEnd, string ord)
        {
           // return TalkDBHelper.ExportTalkInfo(CustID, CustTalkM, TalkBegin, TalkEnd, ord);
            string LinkerIds = "";//每条记录多个ID
            string EmployeeNames = "";//多个员工姓名

            DataTable dt = TalkDBHelper.ExportTalkInfo(CanUserID,CustID, CustTalkM, TalkBegin, TalkEnd, ord);

            DataColumn LinkerName = new DataColumn();
            dt.Columns.Add("LinkerName");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                LinkerIds = dt.Rows[i]["Linker"].ToString();

                string[] LinkerList = LinkerIds.Split(',');

                for (int j = 0; j < LinkerList.Length; j++)
                {
                    //获取参与人ID
                    int inputID = Convert.ToInt32(LinkerList[j]);
                    //调用方法取name
                    EmployeeNames = EmployeeNames + "," + EmployeeDBHelper.GetEmployeeNameByID(inputID, CustTalkM.CompanyCD);
                }

                //插入EmployeeNames到一条记录
                dt.Rows[i]["LinkerName"] = EmployeeNames.Substring(1);
                EmployeeNames = "";
            }

            return dt;
        }
        #endregion
    }
}

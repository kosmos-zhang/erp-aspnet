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
    public class DocRequstBus
    {
        //private static UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];//获取登陆用户信息

        #region 添加请示信息的方法
        /// <summary>
        /// 添加请示信息的方法
        /// </summary>
        /// <param name="DocRequstM">请示信息</param>
        /// <returns>被添加请示ID</returns>
        public static int DocRequstAdd(DocRequstModel DocRequstM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_DOCREQUST_ADD;
            //操作单据编号  请示编号
            logModel.ObjectID = DocRequstM.DocumentNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_DOCREQUST;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_INSERT;
            #endregion

            try
            {
                isSucc = DocRequstDBHelper.DocRequstAdd(DocRequstM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_DOCREQUST_ADD;
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

        #region 根据请示ID修改请示信息
        /// <summary>
        /// 根据请示ID修改请示信息
        /// </summary>
        /// <param name="DocSendM">请示信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateDocRequst(DocRequstModel DocRequstM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_DOCREQUST_LIST;
            //操作单据编号  编号
            logModel.ObjectID = DocRequstM.DocumentNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_DOCREQUST;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_UPDATE;
            #endregion

            try
            {
                isSucc = DocRequstDBHelper.UpdateDocRequst(DocRequstM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_DOCREQUST_LIST;
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

        #region 根据条件检索请示信息
        /// <summary>
        /// 根据条件检索请示信息的方法
        /// </summary>
        /// <param name="DocRequstM">请示信息</param>
        /// <param name="FileDateBegin">开始时间</param>
        /// <param name="FileDateEnd">结束时间</param>
        /// <returns>请示列表信息</returns>
        public static DataTable GetDocRequstBycondition(DocRequstModel DocRequstM, string FileDateBegin, string FileDateEnd, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return DocRequstDBHelper.GetDocRequstBycondition(DocRequstM, FileDateBegin, FileDateEnd, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

        #region 根据请示ID删除请示信息
        /// <summary>
        /// 根据请示ID删除请示信息
        /// </summary>
        /// <param name="RequstID">请示ID</param>
        /// <returns>操作记录数</returns>
        public static int DelDocRequst(string[] RequstID)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_DOCREQUST_LIST;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < RequstID.Length; i++)
            {
                sb.Append(RequstID[i] + ";");
            }

            //操作单据编号  
            logModel.ObjectID = "请示ID:" + sb.ToString();
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_DOCREQUST;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_DELETE;
            #endregion

            try
            {
                isSucc = LinkManDBHelper.DelLinkInfo(RequstID, ConstUtil.TABLE_NAME_DOCREQUST);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_DOCREQUST_LIST;
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

        #region 根据ID获得请示详细信息
        /// <summary>
        /// 根据ID获得请示详细信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="DocRequstID">请示ID</param>
        /// <returns>请示信息</returns>
        public static DataTable GetDocRequstByID(string CompanyCD, int DocRequstID)
        {
            //return DocRequstDBHelper.GetDocRequstByID(CompanyCD, DocRequstID);

            //string Linker = "";//单个ID
            string MainIds = "";//每条记录多个ID
            string EmployeeNames = "";//多个员工姓名

            DataTable dt = DocRequstDBHelper.GetDocRequstByID(CompanyCD, DocRequstID);

            DataColumn MainName = new DataColumn();
            dt.Columns.Add("MainName");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MainIds = dt.Rows[i]["Main"].ToString();

                string[] MainList = MainIds.Split(',');

                for (int j = 0; j < MainList.Length; j++)
                {
                    //获取参与人ID
                    int inputID = Convert.ToInt32(MainList[j]);
                    //调用方法取name
                    EmployeeNames = EmployeeNames + "," + EmployeeDBHelper.GetEmployeeNameByID(inputID, CompanyCD);
                }

                //插入EmployeeNames到一条记录
                dt.Rows[i]["MainName"] = EmployeeNames.Substring(1);
                EmployeeNames = "";
            }

            return dt;
        }
        #endregion

        #region 请示明细
        /// <summary>
        /// 请示明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="RequestDept"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetDocRequestList(string CompanyCD, string RequestDept, string EmployeeID, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return DocRequstDBHelper.GetDocRequestList(CompanyCD, RequestDept, EmployeeID, BeginDate, EndDate, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 请示明细打印
        /// <summary>
        /// 请示明细打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="RequestDept"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetDocRequestListPrint(string CompanyCD, string RequestDept, string EmployeeID, string BeginDate, string EndDate, string ord)
        {
            return DocRequstDBHelper.GetDocRequestListPrint(CompanyCD, RequestDept, EmployeeID, BeginDate, EndDate, ord);
        }
        #endregion

         #region 请示统计
        /// <summary>
        /// 请示统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="RequestDept"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetSendRequestCount(string CompanyCD, string RequestDept, string EmployeeID, string BeginDate, string EndDate)
        {
            return DocRequstDBHelper.GetSendRequestCount(CompanyCD, RequestDept, EmployeeID, BeginDate, EndDate);
        }
        #endregion

        #region 导出请示列表
        /// <summary>
        /// 导出请示列表
        /// </summary>
        /// <param name="DocRequstM"></param>
        /// <param name="FileDateBegin"></param>
        /// <param name="FileDateEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportDocRequst(DocRequstModel DocRequstM, string FileDateBegin, string FileDateEnd, string ord)
        {
            return DocRequstDBHelper.ExportDocRequst(DocRequstM, FileDateBegin, FileDateEnd, ord);
        }
        #endregion
    }
}

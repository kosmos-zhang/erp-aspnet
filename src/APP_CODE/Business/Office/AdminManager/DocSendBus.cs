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
    public class DocSendBus
    {
        //private static UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];//获取登陆用户信息

        #region 添加收文信息的方法
        /// <summary>
        /// 添加收文信息的方法
        /// </summary>
        /// <param name="DocReceiveM">收文信息</param>
        /// <returns>被添加收文ID</returns>
        public static int DocSendAdd(DocSendModel DocSendM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_DOCSEND_ADD;
            //操作单据编号  发文编号
            logModel.ObjectID = DocSendM.DocumentNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_DOCSEND;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_INSERT;
            #endregion

            try
            {
                isSucc = DocSendDBHelper.DocReceiveAdd(DocSendM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_DOCSEND_ADD;
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

        #region 根据发文ID修改发文信息
        /// <summary>
        /// 根据发文ID修改发文信息
        /// </summary>
        /// <param name="DocSendM">发文信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateDocSend(DocSendModel DocSendM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_DOCSEND_LIST;
            //操作单据编号  编号
            logModel.ObjectID = DocSendM.DocumentNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_DOCSEND;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_UPDATE;
            #endregion

            try
            {
                isSucc = DocSendDBHelper.UpdateDocSend(DocSendM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_DOCSEND_LIST;
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

        #region 根据条件检索发文信息
        /// <summary>
        /// 根据条件检索发文信息
        /// </summary>
        /// <param name="DocReceiveM">发文信息</param>
        /// <param name="FileDateBegin">开始时间</param>
        /// <param name="FileDateEnd">结束时间</param>
        /// <returns>发文列表信息</returns>
        public static DataTable GetDocSendBycondition(DocSendModel DocSendM, string FileDateBegin, string FileDateEnd, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return DocSendDBHelper.GetDocSendBycondition(DocSendM, FileDateBegin, FileDateEnd, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

        #region 根据发文ID删除收文信息
        /// <summary>
        /// 根据发文ID删除收文信息的方法
        /// </summary>
        /// <param name="SendID">发文ID</param>
        /// <returns>操作记录数</returns>
        public static int DelDocSend(string[] SendID)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_DOCSEND_LIST;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < SendID.Length; i++)
            {
                sb.Append(SendID[i] + ";");
            }

            //操作单据编号  
            logModel.ObjectID = "发文ID:" + sb.ToString();
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_DOCSEND;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_DELETE;
            #endregion

            try
            {
                isSucc = LinkManDBHelper.DelLinkInfo(SendID, ConstUtil.TABLE_NAME_DOCSEND);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_DOCSEND_LIST;
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

        #region 根据ID获得发文详细信息
        /// <summary>
        /// 根据ID获得发文详细信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="DocReceiveID">发文ID</param>
        /// <returns>发文信息</returns>
        public static DataTable GetDocSendByID(string CompanyCD, int DocSendID)
        {
            //return DocSendDBHelper.GetDocSendByID(CompanyCD, DocSendID);
            string MainIds = "";//每条记录多个主送部门ID
            string CCSendIds = "";
            string DeptNames = "";//多个主送部门名
            string CCSendNames = "";//多个抄送部门名

            DataTable dt = DocSendDBHelper.GetDocSendByID(CompanyCD, DocSendID);

            DataColumn MainDeptName = new DataColumn();
            dt.Columns.Add("MainDeptName");
            DataColumn CCDeptName = new DataColumn();
            dt.Columns.Add("CCDeptName");

            DataTable dtDept = null;
            DataTable dtCCSend = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MainIds = dt.Rows[i]["MainSend"].ToString();
                CCSendIds = dt.Rows[i]["CCSend"].ToString();

                string[] MainList = MainIds.Split(',');

                if (MainIds == "")
                {
                    dt.Rows[i]["MainDeptName"] = "";
                }
                else
                {
                    for (int j = 0; j < MainList.Length; j++)
                    {
                        //获取部门ID
                        string inputID = MainList[j].ToString();
                        //调用方法取name
                        //EmployeeNames = EmployeeNames + "," + EmployeeDBHelper.GetEmployeeNameByID(inputID, CompanyCD);
                        dtDept = XBase.Data.Office.HumanManager.DeptInfoDBHelper.GetDeptNameByID(inputID);
                        if (dtDept.Rows.Count > 0)
                        {
                            DeptNames = DeptNames + "," + dtDept.Rows[0]["DeptName"].ToString();
                        }
                    }
                    //插入DeptNames到一条记录
                    dt.Rows[i]["MainDeptName"] = DeptNames.Substring(1);
                }
                DeptNames = "";


                string[] CCSendList = CCSendIds.Split(',');
                if (CCSendIds == "")
                {
                    dt.Rows[i]["CCDeptName"] = "";
                }
                else
                {
                    for (int k = 0; k < CCSendList.Length; k++)
                    {
                        string CCSendDeptID = CCSendList[k].ToString();
                        dtCCSend = XBase.Data.Office.HumanManager.DeptInfoDBHelper.GetDeptNameByID(CCSendDeptID);
                        if(dtCCSend.Rows.Count > 0)
                        {
                            CCSendNames = CCSendNames + "," + dtCCSend.Rows[0]["DeptName"].ToString();
                        }
                        
                    }
                    dt.Rows[i]["CCDeptName"] = CCSendNames.Substring(1);
                }               
                CCSendNames = "";
            }

            return dt;

        }
        #endregion

        #region 发文明细表
        /// <summary>
        /// 发文明细
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
        public static DataTable GetDocSendList(string CompanyCD, string TypeID, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return DocSendDBHelper.GetDocSendList(CompanyCD, TypeID, BeginDate, EndDate, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 发文明细打印
        /// <summary>
        /// 发文明细打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="TypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetDocSendList(string CompanyCD, string TypeID, string BeginDate, string EndDate, string ord)
        {
            return DocSendDBHelper.GetDocSendList(CompanyCD, TypeID, BeginDate, EndDate, ord);
        }
        #endregion
        
        #region 导出发文列表
        /// <summary>
        /// 导出发文列表
        /// </summary>
        /// <param name="DocSendM"></param>
        /// <param name="FileDateBegin"></param>
        /// <param name="FileDateEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportDocSend(DocSendModel DocSendM, string FileDateBegin, string FileDateEnd, string ord)
        {
            return DocSendDBHelper.ExportDocSend(DocSendM, FileDateBegin, FileDateEnd, ord);
        }
        #endregion
    }
}

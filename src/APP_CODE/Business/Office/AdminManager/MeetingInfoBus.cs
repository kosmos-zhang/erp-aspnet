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
    public class MeetingInfoBus
    {
        //private static UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];//获取登陆用户信息

        #region 添加会议通知的方法
        /// <summary>
        /// 添加会议通知的方法
        /// </summary>
        /// <param name="MeetingInfoM">会议通知信息</param>
        /// <returns>被添加会议通知ID</returns>
        public static int MeetingInfoAdd(MeetingInfoModel MeetingInfoM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_MEETINGINFO_ADD;
            //操作单据编号  会议编号
            logModel.ObjectID = MeetingInfoM.MeetingNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_MEETINGINFO;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_INSERT;
            #endregion

            try
            {
                isSucc = MeetingInfoDBHelper.MeetingInfoAdd(MeetingInfoM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_MEETINGINFO_ADD;
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

        #region 根据会议通知ID修改会议通知信息
        /// <summary>
        /// 根据会议通知ID修改会议通知信息
        /// </summary>
        /// <param name="MeetingInfoM">会议通知信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateMeetingInfo(MeetingInfoModel MeetingInfoM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_MEETINGINFO_LIST;
            //操作单据编号  编号
            logModel.ObjectID = MeetingInfoM.MeetingNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_MEETINGINFO;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_UPDATE;
            #endregion

            try
            {
                isSucc = MeetingInfoDBHelper.UpdateMeetingInfo(MeetingInfoM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_MEETINGINFO_LIST;
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

        #region 根据条件检索会议信息
        /// <summary>
        /// 根据条件检索会议信息
        /// </summary>
        /// <param name="MeetingInfoM">会议信息</param>
        /// <param name="FileDateBegin">开始时间</param>
        /// <param name="FileDateEnd">结束时间</param>        
        /// <returns>会议列表信息</returns>
        public static DataTable GetMeetingInfoBycondition(MeetingInfoModel MeetingInfoM, string FileDateBegin, string FileDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return MeetingInfoDBHelper.GetMeetingInfoBycondition(MeetingInfoM, FileDateBegin, FileDateEnd, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 根据会议ID删除会议信息
        /// <summary>
        /// 根据会议ID删除会议信息
        /// </summary>
        /// <param name="MeetingInfoID">会议ID</param>
        /// <returns>操作记录数</returns>
        public static int DelMeetingInfo(string[] MeetingInfoID)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_MEETINGINFO_LIST;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < MeetingInfoID.Length; i++)
            {
                sb.Append(MeetingInfoID[i] + ";");
            }

            //操作单据编号
            logModel.ObjectID = "会议ID:" + sb.ToString();
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_MEETINGINFO;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_DELETE;
            #endregion

            try
            {
                isSucc = LinkManDBHelper.DelLinkInfo(MeetingInfoID, ConstUtil.TABLE_NAME_MEETINGINFO);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_MEETINGINFO_LIST;
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

        #region 根据ID获得会议详细信息
        /// <summary>
        /// 根据ID获得会议详细信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="MeetingInfoID">会议ID</param>
        /// <returns>会议信息</returns>
        public static DataTable GetMeetingInfoByID(string CompanyCD, int MeetingInfoID)
        {
            //return MeetingInfoDBHelper.GetMeetingInfoByID(CompanyCD, MeetingInfoID);

            string MainIds = "";//每条记录多个ID
            string EmployeeNames = "";//多个员工姓名

            DataTable dt = MeetingInfoDBHelper.GetMeetingInfoByID(CompanyCD, MeetingInfoID);

            DataColumn JoinName = new DataColumn();
            dt.Columns.Add("JoinName");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MainIds = dt.Rows[i]["JoinUser"].ToString();
                if (MainIds.StartsWith(",")) MainIds = MainIds.Remove(0,1);
                if (MainIds.EndsWith(",")) MainIds = MainIds.Remove(MainIds.Length - 1, 1);

                string[] MainList = MainIds.Split(',');

                for (int j = 0; j < MainList.Length; j++)
                {
                    //获取参与人ID
                    int inputID = Convert.ToInt32(MainList[j]);
                    //调用方法取name
                    EmployeeNames = EmployeeNames + "," + EmployeeDBHelper.GetEmployeeNameByID(inputID, CompanyCD);
                }

                //插入EmployeeNames到一条记录
                dt.Rows[i]["JoinName"] = EmployeeNames.Substring(1);
                EmployeeNames = "";
            }

            return dt;
        }
        #endregion

        #region 取消会议通知的方法
        /// <summary>
        /// 取消会议通知的方法
        /// </summary>
        /// <param name="id">会议通知ID</param>
        /// <returns>BOOL值</returns>
        public static bool UpdateMeetingCancel(int id)
        {
            return MeetingInfoDBHelper.UpdateMeetingCancel(id);
        }
        #endregion

        #region 延期会议通知的方法
        /// <summary>
        /// 延期会议通知的方法
        /// </summary>
        /// <param name="id">会议通知ID</param>
        /// <param name="StartDate">会议通知日期</param>
        /// <param name="StartTime">会议通知时间</param>
        /// <returns>BOOL值</returns>
        public static bool DeferMeeting(int id, string StartDate, string StartTime)
        {
            return MeetingInfoDBHelper.DeferMeeting(id, StartDate, StartTime);
        }
        #endregion

        #region 获取会议通知单列表的方法
        /// <summary>
        /// 获取会议通知单列表的方法
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetMeetingInfo(string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return MeetingInfoDBHelper.GetMeetingInfo(CompanyCD, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 获得会议通知单的方法
        /// <summary>
        /// 获得会议通知单的方法
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetMeetingInfoNo(string CompanyCD)
        {
            return MeetingInfoDBHelper.GetMeetingInfoNo(CompanyCD);
        }
        #endregion

        #region 根据会议通知ID修改会议通知状态
        /// <summary>
        /// 根据会议通知ID修改会议通知状态
        /// </summary>
        /// <param name="MeetingInfoM">会议通知ID</param>
        /// <returns>bool值</returns>
        public static bool UpdateMeetingStatusByID(string ID, string MeetingStatus, string ModifiedUserID, string ModifiedDate)
        {
            return MeetingInfoDBHelper.UpdateMeetingStatusByID(ID, MeetingStatus, ModifiedUserID, ModifiedDate);
        }
        #endregion

        #region 导出会议通知列表
        /// <summary>
        /// 导出会议通知列表
        /// </summary>
        /// <param name="MeetingInfoM"></param>
        /// <param name="FileDateBegin"></param>
        /// <param name="FileDateEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportMeetingInfo(MeetingInfoModel MeetingInfoM, string FileDateBegin, string FileDateEnd, string ord)
        {
            return MeetingInfoDBHelper.ExportMeetingInfo(MeetingInfoM, FileDateBegin, FileDateEnd, ord);
        }
        #endregion
    }
}

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
    public class MeetingRoomBus
    {
        //private static UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];//获取登陆用户信息

        #region 添加会议室信息的方法
        /// <summary>
        /// 添加会议室信息的方法
        /// </summary>
        /// <param name="MeetingRoomM">会议室信息</param>
        /// <returns>被添加会议室ID</returns>
        public static int MeetingRoomAdd(MeetingRoomModel MeetingRoomM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_MEETINGROOM_ADD;
            //操作单据编号  会议室名称
            logModel.ObjectID = MeetingRoomM.RoomName;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_MEETINGROOM;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_INSERT;
            #endregion

            try
            {
                isSucc = MeetingRoomDBHelper.MeetingRoomAdd(MeetingRoomM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_MEETINGROOM_ADD;
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

        #region 根据会议室ID修改会议室信息
        /// <summary>
        /// 根据会议室ID修改会议室信息
        /// </summary>
        /// <param name="MeetingRoomM">会议室信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateMeetingRoom(MeetingRoomModel MeetingRoomM)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_MEETINGROOM_LIST;
            //操作单据编号 会议室名称
            logModel.ObjectID = MeetingRoomM.RoomName;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_DOC;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_UPDATE;
            #endregion

            try
            {
                isSucc = MeetingRoomDBHelper.UpdateMeetingRoom(MeetingRoomM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_MEETINGROOM_LIST;
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

        #region 检索会议室信息的方法
        /// <summary>
        /// 检索会议室信息的方法
        /// </summary>
        /// <param name="MeetingRoomM">会议室信息</param> 
        /// <returns>会议室列表信息</returns>
        public static DataTable GetMeetingRoom(string CompanyCD, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return MeetingRoomDBHelper.GetMeetingRoom(CompanyCD, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

        #region 根据ID获得会议室详细信息
        /// <summary>
        /// 根据ID获得会议室详细信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="MeetingRoomID">会议室ID</param>
        /// <returns>会议室信息</returns>
        public static DataTable GetMeetingRoomByID(string CompanyCD, int MeetingRoomID)
        {
            return MeetingRoomDBHelper.GetMeetingRoomByID(CompanyCD, MeetingRoomID);
        }
        #endregion

        #region 根据会议室ID删除会议室信息
        /// <summary>
        /// 根据会议室ID删除会议室信息
        /// </summary>
        /// <param name="MeetingRoomID">会议室ID</param>
        /// <returns>操作记录数</returns>
        public static int DelMeetingRoom(string[] MeetingRoomID)
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
            logModel.ModuleID = ConstUtil.MODULE_ID_MEETINGROOM_LIST;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < MeetingRoomID.Length; i++)
            {
                sb.Append(MeetingRoomID[i] + ";");
            }

            //操作单据编号 
            logModel.ObjectID = "会议室ID:" + sb.ToString();
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_MEETINGROOM;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_DELETE;
            #endregion

            try
            {
                isSucc = LinkManDBHelper.DelLinkInfo(MeetingRoomID, ConstUtil.TABLE_NAME_MEETINGROOM);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_MEETINGROOM_LIST;
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

        #region 根据会议室ID修改会议室功能/状态
        /// <summary>
        /// 根据会议室ID修改会议室功能/状态
        /// </summary>
        /// <param name="id">会议室ID</param>
        /// <param name="Filed">字段名</param>
        /// <param name="FiledValue">字段值</param>
        /// <returns>Bool值</returns>
        public static bool UpdateMeetingFlag(int id, string Filed, string FiledValue)
        {
            return MeetingRoomDBHelper.UpdateMeetingFlag(id, Filed, FiledValue);
        }
        #endregion

        #region 检索会议室名的方法
        /// <summary>
        /// 检索会议室名的方法
        /// </summary>
        /// <param name="MeetingRoomM">会议室名</param> 
        /// <returns>会议室名</returns>
        public static DataTable GetMeetingRoomName(string CompanyCD)
        {
            return MeetingRoomDBHelper.GetMeetingRoomName(CompanyCD);
        }
        #endregion

        #region 导出会议室列表
        /// <summary>
        /// 导出会议室列表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable ExportMeetingRoom(string CompanyCD)
        {
            return MeetingRoomDBHelper.ExportMeetingRoom(CompanyCD);
        }
        #endregion
    }
}

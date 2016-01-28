/**********************************************
 * 类作用：   招聘申请表格操作
 * 建立人：   吴志强
 * 建立时间： 2009/03/27
 ***********************************************/
using System;
using XBase.Model.Office.HumanManager;
using XBase.Data.Office.HumanManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;

namespace XBase.Business.Office.HumanManager
{
    /// <summary>
    /// 类名：RectApplyBus
    /// 描述：招聘申请表格操作
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/27
    /// 最后修改时间：2009/03/27
    /// </summary>
    ///
    public class RectApplyBus
    {

        public static bool UpdateReportStatus(string status, string reprotNo,string DoStatus)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //执行查询
            return RectApplyDBHelper.UpdateReportStatus(status, userInfo.CompanyCD, reprotNo, userInfo.UserID, DoStatus,userInfo .EmployeeID );
        }



        #region 从招聘申请中获取招聘目标
        /// <summary>
        /// 从招聘申请中获取招聘目标
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetGoalInfoFromRectApply()
        {
            //公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //执行查询
            DataTable dtApplyData = RectApplyDBHelper.GetGoalInfoFromRectApply(companyCD);
            //数据存在时，编辑年龄
            if (dtApplyData != null && dtApplyData.Rows.Count > 0)
            {
                for (int i = 0; i < dtApplyData.Rows.Count; i++)
                {
                    //获取最小年龄
                    string minAge = GetSafeData.ValidateDataRow_String (dtApplyData.Rows[i], "MinAge");
                    //获取最大年龄
                    string maxAge = GetSafeData.ValidateDataRow_String(dtApplyData.Rows[i], "MaxAge");
                    //最大年龄和最小年龄都未输入时
                    //最小年龄输入，最大年龄未输入时
                    if (!string.IsNullOrEmpty(minAge) && string.IsNullOrEmpty(maxAge))
                    {
                        //设置年龄
                        dtApplyData.Rows[i]["Age"] = minAge + " 岁以上";
                    }
                    //最大年龄输入，最小年龄未输入时
                    else if (string.IsNullOrEmpty(minAge) && !string.IsNullOrEmpty(maxAge))
                    {
                        //设置年龄
                        dtApplyData.Rows[i]["Age"] = maxAge + " 岁以下";
                    }
                    //最大年龄和最小年龄都输入时
                    else if (!string.IsNullOrEmpty(minAge) && !string.IsNullOrEmpty(maxAge))
                    {
                        //设置年龄
                        dtApplyData.Rows[i]["Age"] = minAge + " 岁 ~ " + maxAge + " 岁";
                    }

                }
            }
            //返回数据
            return dtApplyData;
        }
        #endregion

        #region 编辑招聘申请信息
        /// <summary>
        /// 编辑招聘申请信息
        /// </summary>
        /// <param name="model">招聘申请信息</param>
        /// <returns></returns>
        public static RectApplyModel SaveRectApplyInfo(RectApplyModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            model.ModifiedUserID = userInfo.UserID;
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.RectApplyNo);

            //更新
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {
                try
                {
                    logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                    //执行更新操作
                    model = RectApplyDBHelper.UpdateRectApplyInfo(model);
                }
                catch (Exception ex)
                {
                    //输出系统日志
                    WriteSystemLog(userInfo, ex);
                }
            }
            //插入
            else
            {
                try
                {
                    logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                    //执行插入操作
                    model = RectApplyDBHelper.InsertRectApplyInfo(model);
                }
                catch (Exception ex)
                {
                    //输出系统日志
                    WriteSystemLog(userInfo, ex);
                }
            }
            //更新成功时
            if (model.IsSuccess)
            {
                //设置操作成功标识
                logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            //更新不成功
            else
            {
                //设置操作成功标识 
                logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
            }

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return model;
        }
        #endregion

        #region 输出系统日志
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">异常信息</param>
        private static void WriteSystemLog(UserInfoUtil userInfo, Exception ex)
        {
            /* 
             * 出现异常时，输出系统日志到文本文件 
             * 考虑出现异常情况比较少，尽管一个方法可能多次异常，
             *      但还是考虑将异常日志的变量定义放在catch里面
             */
            //定义变量
            LogInfo logSys = new LogInfo();
            //设置日志类型 需要指定为系统日志
            logSys.Type = LogInfo.LogType.SYSTEM;
            //指定系统日志类型 出错信息
            logSys.SystemKind = LogInfo.SystemLogKind.SYSTEM_ERROR;
            //指定登陆用户信息
            logSys.UserInfo = userInfo;
            //设定模块ID
            logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_RECTAPPLY_EDIT;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion

        #region 通过ID查询招聘申请信息
        /// <summary>
        /// 查询招聘申请信息
        /// </summary>
        /// <param name="rectApplyID">招聘申请ID</param>
        /// <returns></returns>
        public static DataTable GetRectApplyInfoWithID(string rectApplyID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string companyCD = userInfo.CompanyCD;
            return RectApplyDBHelper.GetRectApplyInfoWithID(rectApplyID, companyCD);
        }
        #endregion
        public static DataTable GetGoalDetailsWithID(string rectApplyNo, string companyCD)
        {
       
            return RectApplyDBHelper.GetGoalDetailsWithID(rectApplyNo,companyCD );
        }

        public static DataTable GetRectApplyDetailsInfoByReport(string rectApplyID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string companyCD = userInfo.CompanyCD;
            return RectApplyDBHelper.GetRectApplyDetailsInfoByReport(rectApplyID, companyCD);
        }
        public static DataTable GetRectApplyInfoWithIDByReport(string rectApplyID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string companyCD = userInfo.CompanyCD;
            return RectApplyDBHelper.GetRectApplyInfoWithIDByReport(rectApplyID,companyCD );
        }
        #region 通过检索条件查询招聘申请信息
        /// <summary>
        /// 查询招聘申请信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchRectApplyInfo(RectApplyModel model,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            return RectApplyDBHelper.SearchRectApplyInfo(model, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 删除招聘申请
        /// <summary>
        /// 删除招聘申请
        /// </summary>
        /// <param name="applyNo">申请编号</param>
        /// <returns></returns>
        public static bool DeleteRectApplyInfo(string applyNo)
        {
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //执行删除操作
            bool isSucc = RectApplyDBHelper.DeleteRectApplyInfo(applyNo, companyCD);

            //定义变量
            string remark;
            //成功时
            if (isSucc)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //获取删除的编号列表
            string[] noList = applyNo.Split(',');
            //遍历所有编号，登陆操作日志
            for (int i = 0; i < noList.Length; i++)
            {
                //获取编号
                string no = noList[i];
                //替换两边的 '
                no = no.Replace("'", string.Empty);

                //操作日志
                LogInfoModel logModel = InitLogInfo(no);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                //设置操作成功标识
                logModel.Remark = remark;

                //登陆日志
                LogDBHelper.InsertLog(logModel);
            }

            return isSucc;
        }
        #endregion

        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <param name="applyNo">申请编号</param>
        /// <returns></returns>
        private static LogInfoModel InitLogInfo(string applyNo)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID
            logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_RECTAPPLY_EDIT;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_RECTAPPLY;//操作对象
            //
            logModel.ObjectID = applyNo;

            return logModel;

        }
        #endregion
    }
}

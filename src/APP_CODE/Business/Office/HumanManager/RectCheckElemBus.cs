/**********************************************
 * 类作用：   面试评测要素操作
 * 建立人：   吴志强
 * 建立时间： 2009/04/15
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
    /// 类名：RectCheckElemBus
    /// 描述：面试评测要素操作
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/15
    /// 最后修改时间：2009/04/15
    /// </summary>
    ///
    public class RectCheckElemBus
    {
        #region 编辑面试评测要素信息
        /// <summary>
        /// 编辑面试评测要素信息
        /// </summary>
        /// <param name="model">保存信息</param>
        /// <returns></returns>
        public static bool SaveRectCheckElemInfo(RectCheckElemModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //设置最后修改者
            model.ModifiedUserID = userInfo.UserID;
            //定义返回变量
            bool isSucc = false;
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.ID);

            //更新
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {
                try
                {
                    logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                    //执行更新操作
                    isSucc = RectCheckElemDBHelper.UpdateCheckElemInfo(model);
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
                    isSucc = RectCheckElemDBHelper.InsertCheckElemInfo(model);
                    logModel.ObjectID = model.ID;
                }
                catch (Exception ex)
                {
                    //输出系统日志
                    WriteSystemLog(userInfo, ex);
                }
            }
            //更新成功时
            if (isSucc)
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

            return isSucc;
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
            logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_RECTCHECKELEM_EDIT;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion

        #region 通过ID查询面试评测要素信息
        /// <summary>
        /// 查询面试评测要素信息
        /// </summary>
        /// <param name="elemID">要素ID</param>
        /// <returns></returns>
        public static DataTable GetRectCheckElemInfoWithID(string elemID)
        {
            return RectCheckElemDBHelper.GetCheckElemInfoWithID(elemID);
        }
        #endregion

        #region 通过检索条件查询面试评测要素信息
        /// <summary>
        /// 查询面试评测要素信息
        /// </summary>
        /// <param name="deptID">组织机构ID</param>
        /// <returns></returns>
        public static DataTable SearchRectCheckElemInfo(RectCheckElemModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询
            return RectCheckElemDBHelper.SearchCheckElemInfo(model);
        }
        #endregion

        #region 删除面试评测要素信息
        /// <summary>
        /// 删除面试评测要素信息
        /// </summary>
        /// <param name="elemID">要素ID</param>
        /// <returns></returns>
        public static bool DeleteRectCheckElemInfo(string elemID)
        {
            //执行删除操作
            bool isSucc = RectCheckElemDBHelper.DeleteRectCheckElemInfo(elemID);
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

            //操作日志
            LogInfoModel logModel = InitLogInfo(elemID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;
        }
        #endregion

        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <returns></returns>
        private static LogInfoModel InitLogInfo(string no)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID
            logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_RECTCHECKELEM_EDIT;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_RECTCHECKELEM;
            //操作单据编号
            logModel.ObjectID = no;

            return logModel;

        }
        #endregion

        #region 校验面试评测要素是否被引用
        /// <summary>
        /// 校验面试评测要素是否被引用
        /// </summary>
        /// <param name="elemID">要素ID</param>
        /// <returns></returns>
        public static bool IsRectCheckElemUsed(string elemID)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
          string CompanyCD = userInfo.CompanyCD;
            //执行删除操作
          bool isSucc = RectCheckElemDBHelper.IsRectCheckElemUsed(elemID, CompanyCD);

            return isSucc;
        }
        #endregion

    }
}

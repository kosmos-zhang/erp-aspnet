/**********************************************
 * 类作用：   新建培训表格操作
 * 建立人：   吴志强
 * 建立时间： 2009/04/02
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
    /// 类名：TrainingBus
    /// 描述：新建培训表格操作
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/02
    /// 最后修改时间：2009/04/02
    /// </summary>
    ///
    public class TrainingBus
    {

        #region 查询培训的参与人员信息
        /// <summary>
        /// 查询培训的参与人员信息
        /// </summary>
        /// <param name="traningNo">培训编号</param>
        /// <returns></returns>
        public static DataTable GetJionUserInfo(string traningNo)
        {
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //执行查询并返回查询值
            return TrainingDBHelper.GetJionUserInfo(traningNo, companyCD);
        }
        #endregion

        #region 查询仍在培训中的培训信息
        /// <summary>
        /// 查询仍在培训中的培训信息
        /// </summary>
        /// <returns></returns>
        public static DataTable SearchOnTrainingInfo()
        {
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //执行查询并返回查询值
            return TrainingDBHelper.SearchOnTrainingInfo(companyCD);
        }
        #endregion

        #region 通过检索条件查询培训信息
        /// <summary>
        /// 通过检索条件查询培训信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchTrainingInfo(TrainingSearchModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询并返回查询值
            return TrainingDBHelper.SearchTrainingInfo(model);
        }
        #endregion

        #region 编辑培训信息
        /// <summary>
        /// 编辑培训信息
        /// </summary>
        /// <param name="model">培训信息</param>
        /// <returns></returns>
        public static bool SaveTrainingPlanInfo(TrainingModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            model.ModifiedUserID = userInfo.UserID;
            //定义返回变量
            bool isSucc = false;
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.TrainingNo);

            //更新
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {
                try
                {
                    logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                    //执行更新操作
                    isSucc = TrainingDBHelper.UpdateTrainingInfo(model);
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
                    isSucc = TrainingDBHelper.InsertTrainingInfo(model);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_TRAINING_EDIT;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion

        #region 通过ID查询培训信息
        /// <summary>
        /// 查询培训信息
        /// </summary>
        /// <param name="trainingID">培训ID</param>
        /// <returns></returns>
        public static DataSet GetTrainingInfoWithID(string trainingID)
        {
            return TrainingDBHelper.GetTrainingInfoWithID(trainingID);
        }
        #endregion

        #region 通过检索条件查询招聘活动信息
        /// <summary>
        /// 查询招聘活动信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchRectPlanInfo(RectPlanSearchModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            return RectPlanDBHelper.SearchRectPlanInfo(model);
        }
        #endregion

        #region 删除培训信息
        /// <summary>
        /// 删除培训信息
        /// </summary>
        /// <param name="trainingNo">培训编号</param>
        /// <returns></returns>
        public static bool DeleteTrainingInfo(string trainingNo)
        {
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //执行删除操作
            bool isSucc = TrainingDBHelper.DeleteTrainingInfo(trainingNo, companyCD);

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
            string[] noList = trainingNo.Split(',');
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
        
        #region 根据培训编号查看是否有培训考核信息
        /// <summary>
        /// 根据培训编号查看是否有培训考核信息
        /// </summary>
        /// <param name="trainingNo"></param>
        /// <returns></returns>
        public static bool GetAsseByTraNo(string trainingNo)
        {
            return TrainingDBHelper.GetAsseByTraNo(trainingNo);
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
            logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_TRAINING_EDIT;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_TRAINING;
            //操作单据编号
            logModel.ObjectID = no;

            return logModel;

        }
        #endregion

        #region 培训数量分析
        /// <summary>
        /// 培训数量分析
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="JoinID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetTrainingCount(string CompanyCD, string DeptID, string JoinID, string BeginDate, string EndDate, string BDate, string EDate)
        {
            return TrainingDBHelper.GetTrainingCount(CompanyCD, DeptID, JoinID, BeginDate, EndDate,BDate,EDate);
        }
        #endregion

        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            return TrainingDBHelper.GetRepOrder(OrderNo);
        }

        
        /// <summary>
        /// 获取培训人员 
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepTrainingUser(string OrderNo)
        {
            return TrainingDBHelper.GetRepTrainingUser(OrderNo);
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderDetail(string OrderNo)
        {
            return TrainingDBHelper.GetRepOrderDetail(OrderNo);
        }
    }

}

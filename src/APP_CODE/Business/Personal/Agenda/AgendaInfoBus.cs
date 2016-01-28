using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using XBase.Business.Common;
using XBase.Common;
using XBase.Data.Common;
using XBase.Data.DBHelper;
using XBase.Data.Personal.Agenda;
using XBase.Model.Common;
using XBase.Model.Personal.Agenda;



namespace XBase.Business.Personal.Agenda
{
    public class AgendaInfoBus : System.Web.SessionState.IRequiresSessionState
    {
        public static bool SaveAgendaInfo(PersonalDateArrange model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //定义返回变量
            bool isSucc = false;

            //ID存在时，更新
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {

                try
                {
                    //执行更新操作
                    isSucc = AgendaInfoDBHelper.UpdateAgendaInfo(model);
                    LogInfoModel logModel = InitLogInfo("更新日程");
                    if (isSucc)
                    {
                        logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                    }
                    else
                    {
                        logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                    }
                    logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                    logModel.ModuleID = "10411";
                    LogDBHelper.InsertLog(logModel);
                }
                catch (Exception ex)
                {
                    WriteSystemLog(userInfo, ex, ConstUtil.MODULE_ID_AGENDA_INFO);
                }
            }
            //插入
            else
            {
                try
                {
                    //执行插入操作
                    isSucc = AgendaInfoDBHelper.InsertAgendaInfo(model);
                    LogInfoModel logModel = InitLogInfo("新建日程");
                    if (isSucc)
                    {
                        logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                    }
                    else
                    {
                        logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                    }
                    logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                    logModel.ModuleID = "10411";
                    LogDBHelper.InsertLog(logModel);
                }
                catch (Exception ex)
                {
                    WriteSystemLog(userInfo, ex, ConstUtil.MODULE_ID_AGENDA_INFO);

                }
            }
            return isSucc;
        }

        public static bool DeleteAgendaInfo(int aid)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            bool isSucc = false;
            try
            {
                //执行更新操作
                isSucc = AgendaInfoDBHelper.DeleteAgendaInfo(aid);
                LogInfoModel logModel = InitLogInfo(aid.ToString());
                logModel.ModuleID = "10411";
                if (isSucc)
                {
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                }
                else
                {
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                }
                logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                LogDBHelper.InsertLog(logModel);
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex, ConstUtil.MODULE_ID_AGENDA_INFO);
            }

            return isSucc;
        }


        #region 输出系统日志
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">异常信息</param>
        private static void WriteSystemLog(UserInfoUtil userInfo, Exception ex, string moduleid)
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
            logSys.ModuleID = moduleid;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion

        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <returns></returns>
        private static LogInfoModel InitLogInfo(string ApplyNo)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PERSONALDATEARRANGE;
            //操作对象
            logModel.ObjectID = ApplyNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            logModel.ModuleID = "10411";
            return logModel;

        }
        #endregion

    }
}

/**********************************************
 * 类作用   日程安排业务处理层
 * 创建人   xz
 * 创建时间 2010-7-7 10:12:06 
 ***********************************************/

namespace XBase.Business.Personal.Agenda
{
    /// <summary>
    /// 日程安排业务类
    /// </summary>
    public class PersonalDateArrangeBus
    {
        #region 默认方法
        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD">自动生成</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int iD)
        {
            return PersonalDateArrangeDBHelper.SelectDataTable(iD);
        }

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="companyCD">企业代码</param>

        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectWithKey(string companyCD)
        {
            return PersonalDateArrangeDBHelper.SelectWithKey(companyCD);
        }


        /// <summary>
        /// 列表界面查询方法
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页记录数</param>
        /// <param name="orderBy">排序方法</param>
        /// <param name="TotalCount">总记录数</param>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public static DataTable SelectListData(int pageIndex, int pageCount, string orderBy, ref int TotalCount
            , PersonalDateArrangeModel model)
        {
            return PersonalDateArrangeDBHelper.SelectListData(pageIndex, pageCount, orderBy, ref TotalCount, model);
        }

        /// <summary>
        /// 插入数据记录
        /// </summary>
        ///<param name="model">实体类</param>
        /// <returns>插入数据是否成功:true成功,false不成功</returns>
        public static bool Insert(PersonalDateArrangeModel model)
        {
            ArrayList sqlList = new ArrayList();
            SqlCommand cmd = PersonalDateArrangeDBHelper.InsertCommand(model);

            sqlList.Add(cmd);

            if (SqlHelper.ExecuteTransWithArrayList(sqlList))
            {
                int i = 0;
                if (int.TryParse(cmd.Parameters["@IndexID"].Value.ToString(), out i))
                {
                    model.ID = i;
                }
                sqlList.Clear();
                sqlList.Add(PersonalDateArrangeDBHelper.InsertMessage(model));
                SqlHelper.ExecuteTransWithArrayList(sqlList);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改数据记录
        /// </summary>
        ///<param name="model">实体类</param>
        /// <returns>修改数据是否成功:true成功,false不成功</returns>
        public static bool Update(PersonalDateArrangeModel model)
        {
            ArrayList sqlList = new ArrayList();
            sqlList.Add(PersonalDateArrangeDBHelper.UpdateCommand(model));
            sqlList.Add(PersonalDateArrangeDBHelper.InsertMessage(model));
            if (SqlHelper.ExecuteTransWithArrayList(sqlList))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">ID集合</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(string iD)
        {
            return PersonalDateArrangeDBHelper.Delete(iD);
        }

        #endregion

        #region 自定义
        /// <summary>
        /// 更新点评信息
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>修改数据是否成功:true成功,false不成功</returns>
        public static bool UpdateManager(PersonalDateArrangeModel model)
        {
            return PersonalDateArrangeDBHelper.UpdateManager(model);
        }
        #endregion

    }
}

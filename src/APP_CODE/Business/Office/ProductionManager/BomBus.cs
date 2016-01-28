/**********************************************
 * 类作用：   BOM事务层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/16
 ***********************************************/

using System;
using System.Collections.Generic;
using XBase.Model.Office.ProductionManager;
using XBase.Data.Office.ProductionManager;
using XBase.Common;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Business.Common;
using System.Text;
using System.Collections;
using XBase.Model.Common;
using XBase.Data.Common;


namespace XBase.Business.Office.ProductionManager
{
    public class BomBus
    {
        /// <summary>
        /// BOM插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertBom(BomModel model, out string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ID = "0";
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.BomNo,0);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;

                succ = BomDBHelper.InsertBom(model, loginUserID, out ID);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo,0, ex);
                return false;
            }

        }


        #region 加载BOM树
        /// <summary>
        /// 加载BOM树
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetBomTree(BomModel model)
        {
            try
            {
                return BomDBHelper.GetBomTree(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 子节点数量
        /// <summary>
        /// 判断该结点下，是否还有子结点
        /// </summary>
        /// <param name="ParentCode">上级编码</param>
        /// <returns>大于0还有子节点，否则无子节点</returns>
        public static int ChildrenCount(BomModel model)
        {
            try
            {
                return BomDBHelper.ChildrenCount(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 父件唯一性验证
        /// <summary>
        /// 父件唯一性验证
        /// </summary>
        /// <param name="ParentCode">上级编码</param>
        /// <returns>大于0：已经有父件引用该物品了，否则无父件引用该物品</returns>
        public static int ProductCount(BomModel model)
        {
            try
            {
                return BomDBHelper.ProductCount(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region BOM详细信息
        /// <summary>
        /// 获取BOM信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetBomInfo(BomModel model)
        {
            try
            {
                return BomDBHelper.GetBomInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region BOM子件详细信息
        /// <summary>
        /// 获取BOM子件信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetBomSubInfo(BomModel model)
        {
            try
            {
                return BomDBHelper.GetBomSubInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 更新BOM和子件信息
        /// <summary>
        /// 更新BOM和子件信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UpdateID"></param>
        /// <returns></returns>
        public static bool UpdateBomInfo(BomModel model,string UpdateID)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.BomNo,0);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

                succ = BomDBHelper.UpdateBomInfo(model, loginUserID, UpdateID);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo,0,ex);
                return false;
            }
        }

        #endregion

        #region 通过检索条件查询物料清单信息
        /// <summary>
        /// 通过检索条件查询物料清单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetBomListBycondition(BomModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return BomDBHelper.GetBomListBycondition(model, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  BOM控件查询Bom信息
        /// <summary>
        ///  BOM控件查询Bom信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetBomControlList(BomModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return BomDBHelper.GetBomControlList(model,pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除BOM
        /// <summary>
        /// 删除BOM
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteBom(string ID, string CompanyCD)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return false;
            }
            if (string.IsNullOrEmpty(CompanyCD))
            {
                CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            bool isSucc = BomDBHelper.DeleteBom(ID, CompanyCD);
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
            string[] noList = ID.Split(',');
            //遍历所有编号，登陆操作日志
            for (int i = 0; i < noList.Length; i++)
            {
                //获取编号
                string no = noList[i];
                //替换两边的 '
                no = no.Replace("'", string.Empty);

                //操作日志
                LogInfoModel logModel = InitLogInfo("物料清单ID：" + no,1);
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

        #region 是否被引用
        /// <summary>
        /// 判断要删除的ID是否已经被引用
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ID"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static int CountRefrence(string CompanyCD, string ID, string TableName,string ColumnName)
        {
            try
            {
                return BomDBHelper.CountRefrence(CompanyCD, ID, TableName, ColumnName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 设置是否合法
        /// <summary>
        /// 设置是否合法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int CountInvalid(BomModel model)
        {
            try
            {
                return BomDBHelper.CountInvalid(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 输出系统日志
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">异常信息</param>
        private static void WriteSystemLog(UserInfoUtil userInfo, int ModuleType,Exception ex)
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
            if (ModuleType == 0)
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_BOM_EDIT;
            }
            else
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_BOM_LIST;
            }
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
        private static LogInfoModel InitLogInfo(string wcno,int ModuleType)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_BOM_LIST;
            if (ModuleType == 0)
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_BOM_EDIT;
            }
            else
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_BOM_LIST;
            }
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_BOM;
            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion
    }
}

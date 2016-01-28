/**********************************************
 * 类作用：   MRP事务层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/22
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
    public class MRPBus
    {
        #region MRP插入
        /// <summary>
        /// MRP插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertMRP(MRPModel model, Hashtable ht, out string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ID = "0";
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.MRPNo, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;

                succ = MRPDBHelper.InsertMRP(model, ht,loginUserID, out ID);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, 0, ex);
                return false;
            }

        }
        #endregion

        #region 更新物料需求计划和物料需求计划明细信息
        /// <summary>
        /// 更新物料需求计划和物料需求计划明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UpdateID"></param>
        /// <returns></returns>
        public static bool UpdateMRPInfo(MRPModel model, Hashtable ht, string UpdateID)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.MRPNo, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

                succ = MRPDBHelper.UpdateMRPInfo(model, ht,loginUserID, UpdateID);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, 0, ex);
                return false;
            }
        }

        #endregion

        #region MRP详细信息
        /// <summary>
        /// 获取MRP信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetMRPInfo(MRPModel model)
        {
            try
            {
                return MRPDBHelper.GetMRPInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MRP明细信息
        /// <summary>
        /// MRP明细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetMRPDetailInfo(MRPModel model)
        {
            try
            {
                return MRPDBHelper.GetMRPDetailInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 通过检索条件查询物料需求计划单信息
        /// <summary>
        /// 查询主生产计划单信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable GetMRPListBycondition(MRPModel model, int FlowStatus, int BillTypeFlag, int BillTypeCode, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return MRPDBHelper.GetMRPListBycondition(model, FlowStatus, BillTypeFlag, BillTypeCode, EFIndex, EFDesc, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MRP运算时,根据父件物品ID，列出所有子件
        /// <summary>
        /// MRP运算时,根据父件物品ID，列出所有子件
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ProductID"></param>
        /// <param name="ProduceCount"></param>
        /// <returns></returns>
        public static DataTable MRPCompute_ByParentProduct_GetList(string CompanyCD, int ProductID, Decimal ProduceCount, Decimal ParentProduceCount)
        {
            try
            {
                return MRPDBHelper.MRPCompute_ByParentProduct_GetList(CompanyCD, ProductID, ProduceCount, ParentProduceCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MRP运算:根据主生产计划单据编号查询主生产计划明细
        /// <summary>
        /// 根据主生产计划单据编号查询主生产计划明细
        /// </summary>
        /// <param name="PlanNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetMasterProductScheduleDetail_ByPlanNo(string PlanNo, string CompanyCD)
        {
            try
            {
                return MRPDBHelper.GetMasterProductScheduleDetail_ByPlanNo(PlanNo, CompanyCD);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MRP运算:判断是否有父件等于该ProductID的
        /// <summary>
        /// MRP运算:判断是否有父件等于该ProductID的
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static int HaveBomParentProduct(string CompanyCD, int ProductID)
        {
            try
            {
                return MRPDBHelper.HaveBomParentProduct(CompanyCD, ProductID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MRP运算:物品库存快照
        /// <summary>
        /// 物品库存快照
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetProductSnapshot(int ProductID, string CompanyCD)
        {
            try
            {
                return MRPDBHelper.GetProductSnapshot(ProductID, CompanyCD);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MRP运算:可用存量
        /// <summary>
        /// 可用存量
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static Decimal ProductUseCount(string CompanyCD, int ProductID)
        {
            try
            {
                return MRPDBHelper.ProductUseCount(ProductID, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除MRP
        /// <summary>
        /// 删除MRP
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteMRP(string ID, string CompanyCD)
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

            bool isSucc = MRPDBHelper.DeleteMRP(ID, CompanyCD);
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
                LogInfoModel logModel = InitLogInfo("物料需求计划单ID：" + no, 1);
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

        #region 确认或结单
        /// <summary>
        /// 确认或结单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isConfirm"></param>
        /// <returns></returns>
        public static bool ConfirmOrCompleteMRP(MRPModel model, int OperateType)
        {
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            //string loginUserID = "admin";//[待修改]
            return MRPDBHelper.ConfirmOrCompleteMRP(model, loginUserID, OperateType);
        }
        #endregion

        #region 主生产计划唯一性验证
        /// <summary>
        /// 主生产计划唯一性验证
        /// </summary>
        /// <param name="ParentCode">上级编码</param>
        /// <returns>大于0：已经有物料需求计划引用该计划了，否则无物料需求计划引用该计划</returns>
        public static int PlanCount(MRPModel model)
        {
            try
            {
                return MRPDBHelper.PlanCount(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public static int CountRefrence(string CompanyCD, string ID, string TableName)
        {
            try
            {
                return MRPDBHelper.CountRefrence(CompanyCD, ID, TableName, "FromBillNo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购需求中是否被引用
        /// <summary>
        /// 判断要删除的ID是否已经被引用
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ID"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static int CountRefrencePurchaseRequire(string CompanyCD, string ID, string TableName)
        {
            try
            {
                return MRPDBHelper.CountRefrencePurchaseRequire(CompanyCD, ID, TableName, "MRPCD");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 生成采购需求
        /// <summary>
        /// 生成采购需求
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool SendPurchase(string CompanyCD,string MRPID,out string reason)
        {
            try
            {
                int loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;

                return MRPDBHelper.SendPurchase(CompanyCD, MRPID, loginUserID, out reason);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 物料需求计划明细中是否包含已经生成采购需求的
        /// <summary>
        /// 物料需求计划明细中是否包含已经生成采购需求的
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="strMRPID"></param>
        /// <returns></returns>
        public static bool IsHavePurchaseRequire(string CompanyCD, string strMRPID)
        {
            try
            {
                return MRPDBHelper.IsHavePurchaseRequire(CompanyCD, strMRPID);
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
        private static void WriteSystemLog(UserInfoUtil userInfo, int ModuleType, Exception ex)
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
                logSys.ModuleID = ConstUtil.MODULE_ID_MRP_EDIT;
            }
            else
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_MRP_LIST;
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
        private static LogInfoModel InitLogInfo(string wcno, int ModuleType)
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
                logModel.ModuleID = ConstUtil.MODULE_ID_MRP_EDIT;
            }
            else
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_MRP_LIST;
            }
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_MRP;
            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        #region 取消确认
        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isConfirm"></param>
        /// <returns></returns>
        public static bool CancelConfirmMRP(MRPModel model, int BillTypeFlag, int BillTypeCode)
        {
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            return MRPDBHelper.CancelConfirmOperate(model, BillTypeFlag, BillTypeCode, loginUserID);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using XBase.Model.Office.PurchaseManager;
using XBase.Data.Office.PurchaseManager;
using XBase.Common;
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Collections;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;

namespace XBase.Business.Office.PurchaseManager
{
    public class PurchasePlanBus
    {
        #region 采购计划新增
        /// <summary>
        /// 采购计划新增
        /// </summary>
        /// <param name="PurchasePlanM">主表model</param>
        /// <param name="PurchasePlanSourceMList">明细来源表model列表</param>
        /// <param name="PurchasePlanDetailMList">明细表model列表</param>
        /// <param name="IndexIDentity">主表ID</param>
        /// <returns>bool</returns>
        public static bool InsertPurchasePlanAll(PurchasePlanModel PurchasePlanM, List<PurchasePlanSourceModel> PurchasePlanSourceMList,
            List<PurchasePlanDetailModel> PurchasePlanDetailMList, out int IndexIDentity, Hashtable htExtAttr)
        {
            ArrayList lstAdd = new ArrayList();

            SqlCommand AddPri = PurchasePlanDBHelper.InsertPurchasePlanPrimary(PurchasePlanM);
            lstAdd.Add(AddPri);
            foreach (PurchasePlanSourceModel PurchasePlanSourceM in PurchasePlanSourceMList)
            {
                SqlCommand AddSource = PurchasePlanDBHelper.InsertPurchasePlanSource(PurchasePlanSourceM);
                lstAdd.Add(AddSource);
            }
            foreach (PurchasePlanDetailModel PurchasePlanDetailM in PurchasePlanDetailMList)
            {
                SqlCommand AddDetail = PurchasePlanDBHelper.InsertPurchasePlanDetail(PurchasePlanDetailM);
                lstAdd.Add(AddDetail);
            }
            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(PurchasePlanM, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                lstAdd.Add(cmd);
            #endregion
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //定义返回变量
            bool isSucc = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */

            //执行插入操作
            try
            {
                isSucc = SqlHelper.ExecuteTransWithArrayList(lstAdd);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }


            //定义变量
            string remark;
            //成功时
            if (isSucc)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
                IndexIDentity = int.Parse(((SqlCommand)AddPri).Parameters["@IndexID"].Value.ToString());
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
                IndexIDentity = 0;
            }

            LogInfoModel logModel = InitLogInfo(PurchasePlanM.PlanNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }
        #endregion
        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(PurchasePlanModel  model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.PurchasePlan set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND PlanNo = @PlanNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@PlanNo", model.PlanNo );
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion
        #region 修改采购计划
        /// <summary>
        /// 修改采购计划
        /// </summary>
        /// <param name="PurchasePlanM">主表model</param>
        /// <param name="PurchasePlanSourceMList">明细来源表model列表</param>
        /// <param name="PurchasePlanDetailMList">明细表model列表</param>
        /// <returns>bool</returns>
        public static bool UpdatePurchasePlanAll(PurchasePlanModel PurchasePlanM, List<PurchasePlanSourceModel> PurchasePlanSourceMList,
            List<PurchasePlanDetailModel> PurchasePlanDetailMList, Hashtable htExtAttr)
        {
            ArrayList lstUpdate = new ArrayList();

            //更新主表
            SqlCommand UpdatePri = PurchasePlanDBHelper.UpdatePurchasePlanPrimary(PurchasePlanM);
            lstUpdate.Add(UpdatePri);
              #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(PurchasePlanM, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                lstUpdate.Add(cmd);
            #endregion

            //删除来源表
            SqlCommand DeleteSource = PurchasePlanDBHelper.DeletePurchasePlanSource(PurchasePlanM.PlanNo);
            lstUpdate.Add(DeleteSource);

            //新增来源表
            foreach (PurchasePlanSourceModel PurchasePlanSourceM in PurchasePlanSourceMList)
            {
                SqlCommand AddSource = PurchasePlanDBHelper.InsertPurchasePlanSource(PurchasePlanSourceM);
                lstUpdate.Add(AddSource);
            }

            //删除明细表
            SqlCommand DeleteDetail = PurchasePlanDBHelper.DeletePurchasePlanDetail(PurchasePlanM.PlanNo);
            lstUpdate.Add(DeleteDetail);

            //新增明细表
            foreach (PurchasePlanDetailModel PurchasePlanDetailM in PurchasePlanDetailMList)
            {
                SqlCommand AddDetail = PurchasePlanDBHelper.InsertPurchasePlanDetail(PurchasePlanDetailM);
                lstUpdate.Add(AddDetail);
            }
            

            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //定义返回变量
            bool isSucc = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */

            //执行插入操作
            try
            {
                isSucc = SqlHelper.ExecuteTransWithArrayList(lstUpdate);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
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

            LogInfoModel logModel = InitLogInfo(PurchasePlanM.PlanNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }
        #endregion

        #region 删除采购计划
        /// <summary>
        /// 删除采购计划
        /// </summary>
        /// <param name="IDs">主表ID串</param>
        /// <param name="PlanNos">主表PlanNo串</param>
        /// <returns>bool</returns>
        public static bool DeletePurchasePlanAll(string IDs,string PlanNos)
        {
            ArrayList lstDelete = new ArrayList();
            SqlCommand deletepri = PurchasePlanDBHelper.DeletePurchasePlanPrimary(IDs);
            lstDelete.Add(deletepri);

            SqlCommand deletesource = PurchasePlanDBHelper.DeletePurchasePlanSource(PlanNos);
            lstDelete.Add(deletesource);

            SqlCommand deletedetail = PurchasePlanDBHelper.DeletePurchasePlanDetail(PlanNos);
            lstDelete.Add(deletedetail);

            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //定义返回变量
            bool isSucc = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */

            //执行删除操作
            try
            {
                isSucc = SqlHelper.ExecuteTransWithArrayList(lstDelete);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }


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
            string[] noList = PlanNos.Split(',');
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

        #region 查询采购计划
        public static DataTable SelectPurchasePlan(PurchasePlanModel PurchasePlanM, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                DataTable dt = PurchasePlanDBHelper.SelectPurchasePlanPrimary(PurchasePlanM,pageIndex,pageCount,OrderBy,ref totalCount);
                //DataColumn JoinName = new DataColumn();
                //dt.Columns.Add("IsCite");

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string ID = dt.Rows[i]["ID"].ToString();

                //    bool IsCite = PurchasePlanDBHelper.IsCitePurPlan(ID);

                //    dt.Rows[i]["IsCite"] = IsCite;
                //}
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable SelectPurchasePlan(PurchasePlanModel PurchasePlanM, string OrderBy)
        {
            try
            {
                DataTable dt = PurchasePlanDBHelper.SelectPurchasePlanPrimary(PurchasePlanM, OrderBy);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 确定单据有没有被引用
        public static bool IsCitePurPlan(string ID)
        {
            try
            {
                return PurchasePlanDBHelper.IsCitePurPlan(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 确认采购计划
        public static bool ConfirmPurchasePlan(string ID,string No,List<ProductModel> ProductMList,out string Reason)
        {
            Reason = string.Empty;
            if(!PurchasePlanDBHelper.CanConfirmPurPlan(ID,out Reason))
                return false;
            ArrayList lstConfirm = new ArrayList(); 
            //写确认人，确认时间
            lstConfirm.Add(PurchasePlanDBHelper.ConfirmPurchasePlan(ID));
            //回写采购申请
            if (ProductMList != null)
            {
                foreach (ProductModel ProductM in ProductMList)
                {
                    if (ProductM.FromType == "1")
                    {
                        lstConfirm.Add(PurchasePlanDBHelper.WritePurchaseApply(ProductM));
                    }
                    else if (ProductM.FromType == "2")
                    {
                        lstConfirm.Add(PurchasePlanDBHelper.WritePurchaseRequire(ProductM));
                    }
                }
            }

            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //定义返回变量
            bool isSucc = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */

            //执行插入操作
            try
            {
                isSucc = SqlHelper.ExecuteTransWithArrayList(lstConfirm);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
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

            LogInfoModel logModel = InitLogInfo(No);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_CONFIRM;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }
        #endregion

        #region 取消确认
        public static bool CancelConfirm(string ID, string PlanNo, List<ProductModel> ProductMList)
        {
            try
            {
                ArrayList lstCancelConfirm = new ArrayList();
                //更新主表
                lstCancelConfirm.Add(PurchasePlanDBHelper.CancelConfirm(ID));

                //回写采购申请中已计划数量
                if (ProductMList != null)
                {
                    foreach (ProductModel ProductM in ProductMList)
                    {
                        if (ProductM.FromType == "1")
                        {
                            SqlCommand DescPurApply = PurchasePlanDBHelper.WritePurchaseApplyDesc(ProductM);
                            lstCancelConfirm.Add(DescPurApply);
                        }
                        else if (ProductM.FromType == "2")
                        {
                            SqlCommand DesePurRequire = PurchasePlanDBHelper.WritePurchaseRequireDesc(ProductM);
                            lstCancelConfirm.Add(DesePurRequire);
                        }
                    }
                }
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

                //撤销审批
                string CompanyCD = userInfo.CompanyCD;
                string BillTypeFlag = ConstUtil.CODING_RULE_PURCHASE;
                string BillTypeCode = ConstUtil.CODING_RULE_PURCHASE_PLAN;
                string strUserID = userInfo.UserID; ;
                DataTable dt = FlowDBHelper.GetFlowInstanceInfo(CompanyCD, Convert.ToInt32(BillTypeFlag), Convert.ToInt32(BillTypeCode), Convert.ToInt32(ID));
                if (dt.Rows.Count > 0)
                {
                    string FlowInstanceID = dt.Rows[0]["FlowInstanceID"].ToString();
                    string FlowStatus = dt.Rows[0]["FlowStatus"].ToString();
                    string FlowNo = dt.Rows[0]["FlowNo"].ToString();

                    lstCancelConfirm.Add(FlowDBHelper.CancelConfirmHis(CompanyCD, FlowInstanceID, FlowNo, BillTypeFlag, BillTypeCode, strUserID));
                    lstCancelConfirm.Add(FlowDBHelper.CancelConfirmTsk(CompanyCD, FlowInstanceID, strUserID));
                    lstCancelConfirm.Add(FlowDBHelper.CancelConfirmIns(CompanyCD, FlowNo, BillTypeFlag, BillTypeCode, ID, strUserID));
                }

                //定义返回变量
                bool isSucc = false;
                /* 
                 * 定义日志内容变量 
                 * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
                 * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
                 */

                //执行插入操作
                try
                {
                    isSucc = SqlHelper.ExecuteTransWithArrayList(lstCancelConfirm);
                }
                catch (Exception ex)
                {
                    //输出日志
                    WriteSystemLog(userInfo, ex);
                }
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

                LogInfoModel logModel = InitLogInfo(PlanNo);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_UNCONFIRM;
                //设置操作成功标识
                logModel.Remark = remark;

                //登陆日志
                LogDBHelper.InsertLog(logModel);
                return isSucc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        #endregion

        #region 手工结单
        public static bool ClosePurchasePlan(string ID)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //定义返回变量
            bool isSucc = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */

            //执行插入操作
            try
            {
                isSucc = PurchasePlanDBHelper.ClosePurchasePlan(ID);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
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

            LogInfoModel logModel = InitLogInfo(ID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_COMPLETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }
        #endregion

        #region 取消结单
        public static bool CancelClosePurchasePlan(string ID)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //定义返回变量
            bool isSucc = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */

            //执行插入操作
            try
            {
                isSucc = PurchasePlanDBHelper.CancelClosePurchasePlan(ID);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
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

            LogInfoModel logModel = InitLogInfo(ID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_CONCELCOMPLETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }
        #endregion

        #region 获取某条采购计划主表
        public static DataTable GetPurchasePlanPrimary(string ID)
        {
            try
            {
                DataTable dt = PurchasePlanDBHelper.GetPurchasePlanPrimary(ID);
                dt.Columns.Add("IsCite");
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    bool IsCite = PurchasePlanDBHelper.IsCitePurPlan(ID);

                    dt.Rows[i]["IsCite"] = IsCite;
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取某条采购计划明细来源
        public static DataTable GetPurchasePlanSource(string ID)
        {
            try
            {
                return PurchasePlanDBHelper.GetPurchasePlanSource(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取某条采购计划明细信息
        public static DataTable GetPurchasePlanDetail(string ID)
        {
            try
            {
                return PurchasePlanDBHelper.GetPurchasePlanDetail(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 选择采购需求
        public static DataTable GetPurchaseRequire(string CompanyCD, string ProductNo, string ProductName, string StartDate, string EndDate
            , int pageIndex, int pageCount, string OrderBy, string OrderByType, ref int totalCount)
        {
            try
            {
                return PurchasePlanDBHelper.GetPurchaseRequire(CompanyCD, ProductNo, ProductName, StartDate, EndDate, pageIndex, pageCount, OrderBy, OrderByType, ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 选择采购申请
        public static DataTable GetPurchaseApply(string CompanyCD, string ProductNo, string ProductName, string StartDate, string EndDate
            , int pageIndex, int pageCount, string OrderBy, string OrderByType, ref int totalCount,string PurchaseArriveEFIndex,string PurchaseArriveEFDesc)
        {
            try
            {
                return PurchasePlanDBHelper.GetPurchaseApply(CompanyCD, ProductNo, ProductName, StartDate, EndDate, pageIndex, pageCount, OrderBy, OrderByType, ref totalCount, PurchaseArriveEFIndex, PurchaseArriveEFDesc);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_PURCHASEPLAN_ADD;
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
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_PURCHASEPLAN_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PURCHASEPLAN;
            //操作对象
            logModel.ObjectID = ApplyNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        #region 根据物品ID选择推荐供应商
        public static string GetRcmPrv(string ProductID)
        {
            try
            {
                return PurchasePlanDBHelper.GetRcmPrv(ProductID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

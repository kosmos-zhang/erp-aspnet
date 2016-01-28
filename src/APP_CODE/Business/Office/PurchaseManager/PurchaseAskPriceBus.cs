/**********************************************
 * 类作用：   采购询价单业务逻辑层处理
 * 建立人：   王超
 * 建立时间： 2009/04/30
 ***********************************************/
using System;
using System.Collections.Generic;
using XBase.Model.Office.PurchaseManager;
using XBase.Data.Office.PurchaseManager;
using XBase.Common;
using System.Data;
using XBase.Data.DBHelper;
using System.Collections;
using System.Data.SqlClient;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;

namespace XBase.Business.Office.PurchaseManager
{
    public class PurchaseAskPriceBus
    {
        #region Insert
        /// <summary>
        /// 插入采购询价单
        /// </summary>
        /// <param name="PurchaseAskPriceM">采购询价单主表model</param>
        /// <param name="PurchaseAskPriceDetailMList">采购询价单明细表model列表</param>
        /// <returns>bool</returns>
        public static bool InsertPurchaseAskPrice(PurchaseAskPriceModel PurchaseAskPriceM
            , List<PurchaseAskPriceDetailModel> PurchaseAskPriceDetailMList, out int IndexIDentity, Hashtable htExtAttr)
        {
            try
            {
                ArrayList lstAdd = new ArrayList();
                SqlCommand AddPri = PurchaseAskPriceDBHelper.InsertPurAskPricePri(PurchaseAskPriceM);
                lstAdd.Add(AddPri);
                foreach (PurchaseAskPriceDetailModel PurchaseAskPriceDetailM in PurchaseAskPriceDetailMList)
                {
                    SqlCommand AddDtl = PurchaseAskPriceDBHelper.InsertPurAskPriceDetail(PurchaseAskPriceDetailM);
                    lstAdd.Add(AddDtl);
                }

                #region 拓展属性
                SqlCommand cmd = new SqlCommand();
                GetExtAttrCmd(PurchaseAskPriceM, htExtAttr, cmd);
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

                LogInfoModel logModel = InitLogInfo(PurchaseAskPriceM.AskNo);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
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

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(PurchaseAskPriceModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.PurchaseAskPrice set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND AskNo = @AskNo";
                cmd.Parameters.AddWithValue("@CompanyCD",((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                cmd.Parameters.AddWithValue("@AskNo", model.AskNo );
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion
        #region Update
        /// <summary>
        /// 更新采购询价单
        /// </summary>
        /// <param name="PurchaseAskPriceM">采购询价单主表model</param>
        /// <param name="PurchaseAskPriceDetailMList">采购询价单明细表model列表</param>
        /// <returns>bool</returns>
        /// 
        public static bool UpdatePurchaseAskPrice(PurchaseAskPriceModel PurchaseAskPriceM
            , List<PurchaseAskPriceDetailModel> PurchaseAskPriceDetailMList, Hashtable htExtAttr)
        {
            try
            {
                ArrayList lstUpdate = new ArrayList();
                if (PurchaseAskPriceM.AskAgain == "1")
                {//再次询价，需将此次询价之前的那次询价记录记入询价历史表
                    SqlCommand AskAgain = PurchaseAskPriceDBHelper.InsertPurchaseAskHistory(PurchaseAskPriceM.ID);
                    lstUpdate.Add(AskAgain);
                }
                SqlCommand UpdatePri = PurchaseAskPriceDBHelper.UpdatePurAskPricePri(PurchaseAskPriceM);
                lstUpdate.Add(UpdatePri);
                  #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(PurchaseAskPriceM, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                lstUpdate.Add(cmd);
            #endregion

                SqlCommand DelDtl = PurchaseAskPriceDBHelper.DeletePurAskPriceDetail(PurchaseAskPriceM.AskNo);
                lstUpdate.Add(DelDtl);
                foreach (PurchaseAskPriceDetailModel PurchaseAskPriceDetailM in PurchaseAskPriceDetailMList)
                {
                    SqlCommand AddDtl = PurchaseAskPriceDBHelper.InsertPurAskPriceDetail(PurchaseAskPriceDetailM);
                    lstUpdate.Add(AddDtl);
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

                LogInfoModel logModel = InitLogInfo(PurchaseAskPriceM.AskNo);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
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

        #region Select
        /// <summary>
        /// 检索采购询价单
        /// </summary>
        /// <param name="PurchaseAskPriceM">采购询价单主表model</param>
        /// <returns>datatable</returns>
        /// 
        public static DataTable GetPurchaseAskPrice(PurchaseAskPriceModel PurchaseAskPriceM, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                DataTable dt = PurchaseAskPriceDBHelper.SelectPurAskPricePri(PurchaseAskPriceM, pageIndex, pageCount, OrderBy, ref totalCount);
                //DataColumn JoinName = new DataColumn();
                //dt.Columns.Add("IsCite");
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string ID = dt.Rows[i]["ID"].ToString();

                //    bool IsCite = PurchaseAskPriceDBHelper.IsCite(ID);

                //    dt.Rows[i]["IsCite"] = IsCite;
                //}
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetPurchaseAskPrice(PurchaseAskPriceModel PurchaseAskPriceM, string OrderBy)
        {
            try
            {
                DataTable dt = PurchaseAskPriceDBHelper.SelectPurAskPricePri(PurchaseAskPriceM,OrderBy);
                //DataColumn JoinName = new DataColumn();
                //dt.Columns.Add("IsCite");
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string ID = dt.Rows[i]["ID"].ToString();

                //    bool IsCite = PurchaseAskPriceDBHelper.IsCite(ID);

                //    dt.Rows[i]["IsCite"] = IsCite;
                //}
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除采购询价单
        /// </summary>
        /// <param name="IDs">采购询价单主表ID串，已经拼接好的</param>
        /// <param name="AskNos">采购询价单主表AskNo串，已经拼接好的</param>
        /// <returns>bool</returns>
        /// 
        public static bool DeletePurAsk(string IDs, string AskNos)
        {
            try
            {
                ArrayList lstDelete = new ArrayList();
                SqlCommand delpri = PurchaseAskPriceDBHelper.DeletePurAskPricePri(IDs);
                lstDelete.Add(delpri);
                SqlCommand deldtl = PurchaseAskPriceDBHelper.DeletePurAskPriceDetailS(AskNos);
                lstDelete.Add(deldtl);

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
                string[] noList = AskNos.Split(',');
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据主表ID得到主表相关信息
        /// <summary>
        ///根据主表ID得到主表相关信息
        /// </summary>
        /// <param name="ID">采购询价主表ID</param>
        /// <returns>datatable</returns>
        /// 
        public static DataTable GetPurAskPricePriByID(string ID)
        {
            try
            {
                DataTable dt = PurchaseAskPriceDBHelper.GetPurAskPricePriByID(ID);
                bool IsCite = PurchaseAskPriceDBHelper.IsCite(ID);
                DataColumn JoinName = new DataColumn();
                dt.Columns.Add("IsCite");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
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

        #region 根据AskNo和AskOrder查找明细表
        /// <summary>
        ///根据AskNo和AskOrder查找明细表
        /// </summary>
        /// <param name="AskNo">AskNo</param>
        /// <param name="AskOrder">AskOrder</param>
        /// <returns>datatable</returns>
        /// 
        public static DataTable GetPurAskPriceDetail(string ID)
        {
            try
            {
                return PurchaseAskPriceDBHelper.GetPurAskPriceDetail(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 确认采购询价单
        /// <summary>
        ///确认采购询价
        /// </summary>
        /// <param name="ID">采购询价主表ID</param>
        /// <returns>bool</returns>
        /// 
        public static bool ConfirmPurAskPrice(string ID,string No)
        {
            try
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
                    isSucc = PurchaseAskPriceDBHelper.ConfirmPurAskPrice(ID);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static bool IsCite(string ID)
        {
            try
            {
                return PurchaseAskPriceDBHelper.IsCite(ID);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        #region 取消确认
        public static bool CancelConfirm(string ID, string No)
        {
            try
            {
                //被引用不可以取消确认
                if (PurchaseAskPriceDBHelper.IsCite(ID))
                    return false;

                ArrayList lstCancelConfirm = new ArrayList();
                //主表操作
                lstCancelConfirm.Add(PurchaseAskPriceDBHelper.CancelConfirm(ID));
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

                //撤销审批
                string CompanyCD = userInfo.CompanyCD;
                string BillTypeFlag = ConstUtil.CODING_RULE_PURCHASE;
                string BillTypeCode = ConstUtil.CODING_RULE_PURCHASE_ASKPRICE;
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

                LogInfoModel logModel = InitLogInfo(No);
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

        #region 采购询价单结单
        /// <summary>
        ///采购询价单结单
        /// </summary>
        /// <param name="ID">采购询价主表ID</param>
        /// <returns>bool</returns>
        /// 
        public static bool CompletePurAskPrice(string ID)
        {
            try
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
                    isSucc = PurchaseAskPriceDBHelper.CompletePurAskPrice(ID);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购询价单取消结单
        /// <summary>
        ///采购询价单取消结单
        /// </summary>
        /// <param name="ID">采购询价主表ID</param>
        /// <returns>bool</returns>
        /// 
        public static bool ConcelCompletePurAskPrice(string ID)
        {
            try
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
                    isSucc = PurchaseAskPriceDBHelper.ConcelCompletePurAskPrice(ID);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询采购询价历史
        public static DataTable GetPurAskPriceHistory(string CompanyCD, string AskNo, int pageIndex, int pageCount, string OrderBy, out int totalCount)
        {
            try
            {
                return PurchaseAskPriceDBHelper.GetPurAskPriceHistory(CompanyCD,AskNo,pageIndex,pageCount,OrderBy,out totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 询价历史表插入
        //public static bool InsertPurchaseAskHistory(List<PurchaseAskPriceHistoryModel> PurchaseAskPriceHistoryMList)
        //{
        //    try
        //    {
                
        //        ArrayList lstHistory = new ArrayList();
        //        foreach (PurchaseAskPriceHistoryModel PurchaseAskPriceHistoryM in PurchaseAskPriceHistoryMList)
        //        {
        //            if (true == PurchaseAskPriceDBHelper.IsInHistory(PurchaseAskPriceHistoryM.AskNo, PurchaseAskPriceHistoryM.AskOrder))
        //            {
        //                return true;
        //            }
        //            SqlCommand comm = PurchaseAskPriceDBHelper.InsertPurchaseAskHistory(PurchaseAskPriceHistoryM);
        //            lstHistory.Add(comm);
        //        }
        //        return SqlHelper.ExecuteTransWithArrayList(lstHistory);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
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
            logSys.ModuleID = ConstUtil.MODULE_ID_PURCHASEASKPRICE_ADD;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_PURCHASEASKPRICE_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PURCHASEASKPRICE;
            //操作对象
            logModel.ObjectID = ApplyNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        #region 询价历史表数据显示
        public static DataTable GetPurAskHistory(string CompanyCD, string AskNo)
        {
            try
            {
                return PurchaseAskPriceDBHelper.GetPurAskHistory(CompanyCD, AskNo);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        #endregion
    }
}

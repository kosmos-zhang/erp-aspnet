/**********************************************
 * 类作用：   采购订单业务逻辑层处理
 * 建立人：   王超
 * 建立时间： 2009/04/16
 ***********************************************/
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
using XBase.Business.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.PurchaseManager
{
    public class PurchaseOrderBus
    {

        #region 更新开票状态
        /// <summary>
        /// 更新开票状态  Added By jiangym 2009-04-22
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns></returns>
        public static bool UpdateisOpenBill(string ID)
        {
            try
            {
                return PurchaseOrderDBHelper.UpdateisOpenBill(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 根据检索条件检索出满足条件的信息 Added By jiangym 2009-04-16
        /// </summary>
        /// <param name="OrderNo">订单号</param>
        /// <param name="Title">主题</param>
        /// <param name="CustName">客户</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns>DataTable</returns>
        public static DataTable SearchOrderByCondition(string OrderNo, string Title,
          string CustName, string StartDate, string EndDate)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return PurchaseOrderDBHelper.SearchOrderByCondition(CompanyCD, OrderNo, Title, CustName, StartDate, EndDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        /// <summary>
        /// 根据检索条件检索出满足条件的信息 Added By Moshenlin 2009-04-16
        /// </summary>
        /// <param name="OrderNo">订单号</param>
        /// <param name="Title">主题</param>
        /// <param name="CustName">客户</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns>DataTable</returns>
        public static DataTable SearchOrderByCondition(string OrderNo, string Title,
          string CustName, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return PurchaseOrderDBHelper.SearchOrderByCondition(CompanyCD, OrderNo, Title, CustName, StartDate, EndDate, pageIndex, pageSize, OrderBy, ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        /// <summary>
        /// 根据检索条件检索出满足条件的信息 Added By moshenlin 2009-06-29
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable SearchOrderByCondition(string ids)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return PurchaseOrderDBHelper.SearchOrderByCondition(ids, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 采购计划控件
        public static DataTable GetPurPlanByProvider(string CompanyCD, int ProviderID, string ProductNo, string ProductName, string StartDate, string EndDate
            , int pageIndex, int pageCount, string OrderBy, string OrderByType, ref int totalCount)
        {
            try
            {
                return PurchaseOrderDBHelper.GetPurPlanByProvider(CompanyCD, ProviderID, ProductNo, ProductName, StartDate, EndDate, pageIndex, pageCount, OrderBy, OrderByType, ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购合同
        //明细获取
        public static DataTable GetContractDetail(string CompanyCD, string ContractNo, string Title, int ProviderID, int Currency, int pageIndex, int pageSize, string OrderBy, out int totalRecord)
        {
            try
            {
                return PurchaseOrderDBHelper.GetContractDetail(CompanyCD, ContractNo, Title, ProviderID, Currency, pageIndex, pageSize, OrderBy, out totalRecord);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //主表取值
        public static DataTable GetContract(string CompanyCD, int ID)
        {
            try
            {
                return PurchaseOrderDBHelper.GetContract(CompanyCD, ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据供应商ID查询采购合同主表信息
        /// <summary>
        /// 根据供应商ID查询采购合同主表信息 
        /// </summary>
        /// <param name="ProviderID">供应商ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetPurContract(string CompanyCD, string ProviderName, string ContractNo, string Title, string StartDate, string EndDate
            , int pageIndex, int pageCount, string OrderBy, string OrderByType, ref int totalCount)
        {
            try
            {
                return PurchaseOrderDBHelper.GetPurContract(CompanyCD, ProviderName, ContractNo, Title, StartDate, EndDate, pageIndex, pageCount, OrderBy, OrderByType, ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  根据采购合同编号查询采购合同明细
        /// <summary>
        /// 根据采购合同编号查询采购合同明细
        /// </summary>
        /// <param name="ContractNo">采购合同编号</param>
        /// <returns>DataTable</returns>
        public static DataTable GetPurOrderDetailByContractNo(string ContractNo)
        {
            try
            {
                return PurchaseOrderDBHelper.GetPurOrderDetailByContractNo(ContractNo);
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
        private static void GetExtAttrCmd(PurchaseOrderModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.PurchaseOrder set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND OrderNo = @OrderNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@OrderNo", model.OrderNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion

        #region insert
        public static bool InsertPurchaseOrder(PurchaseOrderModel PurchaseOrderM, List<PurchaseOrderDetailModel> PurchaseOrderDetailMList, out int IndexIDentity, out string Reason, Hashtable htExtAttr)
        {
            try
            {
                IndexIDentity = 0;
                //判断引用源单数量有没有超过，超过了就不让保存
                if (!PurchaseOrderDBHelper.CanSave(PurchaseOrderDetailMList, out Reason))
                    return false;
                ArrayList lstAdd = new ArrayList();
                //插入主表
                SqlCommand AddPri = PurchaseOrderDBHelper.InsertPurchaseOrder(PurchaseOrderM);
                lstAdd.Add(AddPri);
                string OrderNo = PurchaseOrderM.OrderNo;
                //插入明细
                foreach (PurchaseOrderDetailModel PurchaseOrderDetailM in PurchaseOrderDetailMList)
                {
                    SqlCommand AddDetail = PurchaseOrderDBHelper.InsertPurchaseOrderDetail(PurchaseOrderDetailM, OrderNo);
                    lstAdd.Add(AddDetail);
                }
                #region 拓展属性
                SqlCommand cmd = new SqlCommand();
                GetExtAttrCmd(PurchaseOrderM, htExtAttr, cmd);
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

                LogInfoModel logModel = InitLogInfo(OrderNo);
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

        #region update
        //更新之前判断有没有被引用
        public static bool IsCite(string ID)
        {
            try
            {
                return PurchaseOrderDBHelper.IsCite(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新采购订单
        /// </summary>
        /// <param name="PurchaseOrderM">主表model</param>
        /// <param name="PurchaseOrderDetailMList">明细表modellist</param>
        /// <param name="ProductMStorList">用于回写库存的物品modellist</param>
        /// <param name="ProductMList">用于回写计划或是合同的物品modellist</param>
        /// <returns>DataTable</returns>
        public static bool UpdatePurchaseOrder(PurchaseOrderModel PurchaseOrderM, List<PurchaseOrderDetailModel> PurchaseOrderDetailMList
            , List<ProductModel> ProductMStorList, List<ProductModel> ProductMList, out string Reason, Hashtable htExtAttr)
        {
            try
            {
                //判断引用源单数量有没有超过，超过了就不让保存
                if (!PurchaseOrderDBHelper.CanSave(PurchaseOrderDetailMList, out Reason))
                    return false;
                //判断该单据有没有确认


                //判断该单据是不是可以确认，包括引用的源单数量是不是合法，有没有到达库存报警上限



                ArrayList lstUpdate = new ArrayList();

                //更新主表
                SqlCommand UpdatePri = PurchaseOrderDBHelper.UpdatePurchaseOrder(PurchaseOrderM);
                lstUpdate.Add(UpdatePri);
                #region 拓展属性
                SqlCommand cmd = new SqlCommand();
                GetExtAttrCmd(PurchaseOrderM, htExtAttr, cmd);
                if (htExtAttr.Count > 0)
                    lstUpdate.Add(cmd);
                #endregion

                string OrderNo = PurchaseOrderM.OrderNo;
                //删除明细
                SqlCommand DeleteDetail = PurchaseOrderDBHelper.DeletePurchaseOrderDetailSingle(OrderNo);
                lstUpdate.Add(DeleteDetail);

                //插入明细
                foreach (PurchaseOrderDetailModel PurchaseOrderDetailM in PurchaseOrderDetailMList)
                {
                    SqlCommand AddDetail = PurchaseOrderDBHelper.InsertPurchaseOrderDetail(PurchaseOrderDetailM, OrderNo);
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

                LogInfoModel logModel = InitLogInfo(OrderNo);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
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

        #region select
        /// <summary>
        /// 检索采购订单
        /// </summary>
        /// <param name="PurchaseOrderM">采购订单Model</param>
        /// <returns>DataTable</returns>
        /// 
        public static DataTable GetPurchaseOrder(PurchaseOrderModel PurchaseOrderM, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                DataTable dt = PurchaseOrderDBHelper.GetPurchaseOrder(PurchaseOrderM, pageIndex, pageCount, OrderBy, ref totalCount);
                //DataColumn JoinName = new DataColumn();
                //dt.Columns.Add("IsCite");

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string ID = dt.Rows[i]["ID"].ToString();

                //    bool IsCite = PurchaseOrderDBHelper.IsCite(ID);

                //    dt.Rows[i]["IsCite"] = IsCite;
                //}
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetPurchaseOrderAnaylise(PurchaseOrderModel PurchaseOrderM, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                DataTable dt = PurchaseOrderDBHelper.GetPurchaseOrderAnaylise(PurchaseOrderM, pageIndex, pageCount, OrderBy, ref totalCount);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetPurchaseOrder(PurchaseOrderModel PurchaseOrderM, string OrderBy)
        {
            try
            {
                DataTable dt = PurchaseOrderDBHelper.GetPurchaseOrder(PurchaseOrderM, OrderBy);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 批量删除采购订单
        public static bool DeletePurchaseOrder(string IDs, string OrderNos)
        {
            try
            {
                ArrayList lstDelete = new ArrayList();
                SqlCommand DelPri = PurchaseOrderDBHelper.DeletePurchaseOrder(IDs);
                lstDelete.Add(DelPri);
                SqlCommand DelDtl = PurchaseOrderDBHelper.DeletePurchaseOrderDetail(OrderNos);
                lstDelete.Add(DelDtl);

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
                string[] noList = OrderNos.Split(',');
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

        #region 确认采购订单
        public static bool ConfirmPuechaseOrder(string CompanyCD, string ID, string FromType, string OrderNo, out string Reason, List<ProductModel> ProductMList, List<ProductModel> ProductMStorList)
        {
            try
            {

                if (!PurchaseOrderDBHelper.CanConfirm(CompanyCD, OrderNo, int.Parse(ID), FromType, out Reason))
                    return false;
                ArrayList lstConfirm = new ArrayList();
                //写确认人，确认时间，单据状态
                lstConfirm.Add(PurchaseOrderDBHelper.ConfirmPurchaseOrder(ID));
                //回写合同表
                if (FromType == "4")
                {
                    foreach (ProductModel ProductM in ProductMList)
                    {
                        lstConfirm.Add(PurchaseOrderDBHelper.WritePurContract(ProductM));
                    }
                }
                else if (FromType == "2")
                {//回写计划表
                    foreach (ProductModel ProductM in ProductMList)
                    {
                        lstConfirm.Add(PurchaseOrderDBHelper.WritePurPlanConfirm(ProductM));
                    }
                }
                //回写库存分仓存量表
                foreach (ProductModel ProductM in ProductMStorList)
                {
                    lstConfirm.Add(PurchaseOrderDBHelper.WriteStorge(ProductM));
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

                LogInfoModel logModel = InitLogInfo(OrderNo);
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

        #region 取消确认
        public static bool ConcelConfirm(string ID, string FromType, string OrderNo, List<ProductModel> ProductMList, List<ProductModel> ProductMStorList, out string Reason)
        {
            try
            {
                Reason = string.Empty;
                //不可以确认返回false
                if (!PurchaseOrderDBHelper.CanConcel(ID, out Reason))
                    return false;
                ArrayList lstConcelConfirm = new ArrayList();
                //更新主表
                SqlCommand ConcelPri = PurchaseOrderDBHelper.ConcelConfirm(ID);
                lstConcelConfirm.Add(ConcelPri);

                //更新分仓存量表
                if (ProductMStorList != null)
                {
                    foreach (ProductModel ProductM in ProductMStorList)
                    {
                        SqlCommand comm = PurchaseOrderDBHelper.WriteStorgeDecr(ProductM);
                        lstConcelConfirm.Add(comm);
                    }
                }
                if (ProductMList != null)
                {
                    if (FromType == "2")
                    {//回写采购计划
                        foreach (ProductModel ProductM in ProductMList)
                        {
                            SqlCommand comm = PurchaseOrderDBHelper.WritePurPlanDecr(ProductM);
                            lstConcelConfirm.Add(comm);
                        }
                    }
                    else if (FromType == "4")
                    {//回写采购合同
                        foreach (ProductModel ProductM in ProductMList)
                        {
                            SqlCommand comm = PurchaseOrderDBHelper.WritePurContDecr(ProductM);
                            lstConcelConfirm.Add(comm);
                        }
                    }
                }
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

                //撤销审批
                string CompanyCD = userInfo.CompanyCD;
                string BillTypeFlag = ConstUtil.CODING_RULE_PURCHASE;
                string BillTypeCode = ConstUtil.CODING_RULE_PURCHASE_ORDER;
                string strUserID = userInfo.UserID; ;
                DataTable dt = FlowDBHelper.GetFlowInstanceInfo(CompanyCD, Convert.ToInt32(BillTypeFlag), Convert.ToInt32(BillTypeCode), Convert.ToInt32(ID));
                if (dt.Rows.Count > 0)
                {
                    string FlowInstanceID = dt.Rows[0]["FlowInstanceID"].ToString();
                    string FlowStatus = dt.Rows[0]["FlowStatus"].ToString();
                    string FlowNo = dt.Rows[0]["FlowNo"].ToString();

                    lstConcelConfirm.Add(FlowDBHelper.CancelConfirmHis(CompanyCD, FlowInstanceID, FlowNo, BillTypeFlag, BillTypeCode, strUserID));
                    lstConcelConfirm.Add(FlowDBHelper.CancelConfirmTsk(CompanyCD, FlowInstanceID, strUserID));
                    lstConcelConfirm.Add(FlowDBHelper.CancelConfirmIns(CompanyCD, FlowNo, BillTypeFlag, BillTypeCode, ID, strUserID));
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
                    isSucc = SqlHelper.ExecuteTransWithArrayList(lstConcelConfirm);
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

                LogInfoModel logModel = InitLogInfo(OrderNo);
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


        #region 采购订单结单
        public static bool CompletePurchaseOrder(string ID)
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
                    isSucc = PurchaseOrderDBHelper.CompletePurchaseOrder(ID);
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

        #region 采购订单取消结单
        public static bool ConcelCompletePurchaseOrder(string ID)
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
                    isSucc = PurchaseOrderDBHelper.ConcelCompletePurchaseOrder(ID);
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

        #region 根据采购订单ID填充主表
        /// <summary>
        /// 根据采购订单ID填充主表
        /// </summary>
        /// <param name="ID">订单ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetPurchaseOrderByID(string ID)
        {
            try
            {
                DataTable dt = PurchaseOrderDBHelper.GetPurchaseOrderByID(ID);
                dt.Columns.Add("IsCite");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bool IsCite = PurchaseOrderDBHelper.IsCite(ID);

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

        #region 打印填充主表
        public static DataTable GetPurchaseOrderPrintByID(string ID)
        {
            try
            {
                return PurchaseOrderDBHelper.GetPurchaseOrderPrintByID(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region 根据采购订单编号填充明细表
        public static DataTable GetPurchaseOrderDetail(string ID)
        {
            try
            {
                return PurchaseOrderDBHelper.GetPurchaseOrderDetail(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询采购历史价格列表
        /// <summary>
        /// 根据检索条件检索出满足条件的信息 Added By songfei 2009-04-24
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable SelectPurchaseHistoryAskPrice(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProductID, string StartPurchaseDate, string EndPurchaseDate)
        {
            try
            {
                return PurchaseOrderDBHelper.SelectHistoryAskPriceList(pageIndex, pageCount, orderBy, ref TotalCount, ProductID, StartPurchaseDate, EndPurchaseDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable SelectPurchaseHistoryAskPricePrint(string orderBy, string ProductID, string StartPurchaseDate, string EndPurchaseDate)
        {
            try
            {
                return PurchaseOrderDBHelper.SelectHistoryAskPriceListPrint(orderBy, ProductID, StartPurchaseDate, EndPurchaseDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询采购历史价格链接页面
        /// <summary>
        /// 根据物品编号链接要显示的信息 Added By songfei 2009-04-24
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable SelectPurchaseHistoryAskPriceShow(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProductID)
        {
            try
            {
                return PurchaseOrderDBHelper.SelectHistoryAskPriceShowList(pageIndex, pageCount, orderBy, ref TotalCount, ProductID);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_PURCHASEORDER_ADD;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_PURCHASEORDER_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PURCHASEORDER;
            //操作对象
            logModel.ObjectID = ApplyNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        #region 采购报表采购价格分析
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchasePriceAnalyse(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProductID, string StartOrderDate, string EndOrderDate, string CompanyCD)
        {
            try
            {
                return PurchaseOrderDBHelper.PurchasePriceAnalyse(pageIndex, pageCount, orderBy, ref TotalCount, ProductID, StartOrderDate, EndOrderDate, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购报表采购价格分析打印
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchasePriceAnalysePrint(string ProductID, string StartOrderDate, string EndOrderDate, string CompanyCD, string orderBy)
        {
            try
            {
                return PurchaseOrderDBHelper.PurchasePriceAnalysePrint(ProductID, StartOrderDate, EndOrderDate, CompanyCD, orderBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购报表采购历史价查询
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseHisPriceQuery(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProductID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            try
            {
                return PurchaseOrderDBHelper.PurchaseHisPriceQuery(pageIndex, pageCount, orderBy, ref TotalCount, ProductID, StartConfirmDate, EndConfirmDate, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购报表采购历史价查询打印
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseHisPriceQueryPrint(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProductID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            try
            {
                return PurchaseOrderDBHelper.PurchaseHisPriceQueryPrint(pageIndex, pageCount, orderBy, ref TotalCount, ProductID, StartConfirmDate, EndConfirmDate, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region baobiao
        //采购订单报表
        public static DataTable SelectPurchaseOrder(string CompanyCD, string BillStatus, string StartDate, string EndDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            return PurchaseOrderDBHelper.SelectPurchaseOrder(CompanyCD, BillStatus, StartDate, EndDate, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        public static DataTable SelectPurchaseOrder(string CompanyCD, string OrderBy, string BillStatus, string StartDate, string EndDate)
        {
            return PurchaseOrderDBHelper.SelectPurchaseOrder(CompanyCD, OrderBy, BillStatus, StartDate, EndDate);
        }
        //采购价格查询
        public static DataTable GetPurchasePrice(string CompanyCD, string OrderBy, string ProductID, string StartDate, string EndDate)
        {
            try
            {
                return PurchaseOrderDBHelper.GetPurchasePrice(CompanyCD, OrderBy, ProductID, StartDate, EndDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetPurchasePrice(string CompanyCD, string ProductID, string StartDate, string EndDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return PurchaseOrderDBHelper.GetPurchasePrice(CompanyCD, ProductID, StartDate, EndDate, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //采购查询统计--按照采购员
        public static DataTable GetPurStatByPurchaser(string CompanyCD, string Purchaser, string StartDate, string EndDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return PurchaseOrderDBHelper.GetPurStatByPurchaser(CompanyCD, Purchaser, StartDate, EndDate, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetPurStatByPurchaser(string CompanyCD, string Purchaser, string StartDate, string EndDate, string OrderBy)
        {
            try
            {
                return PurchaseOrderDBHelper.GetPurStatByPurchaser(CompanyCD, Purchaser, StartDate, EndDate, OrderBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //采购统计--按照物品
        public static DataTable GetPurByProduct(string CompanyCD, string ProductID, string StartDate, string EndDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return PurchaseOrderDBHelper.GetPurByProduct(CompanyCD, ProductID, StartDate, EndDate, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetPurByProduct(string CompanyCD, string OrderBy, string ProductID, string StartDate, string EndDate)
        {
            try
            {
                return PurchaseOrderDBHelper.GetPurByProduct(CompanyCD, OrderBy, ProductID, StartDate, EndDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //采购汇总查询
        public static DataTable GetPurCll(string CompanyCD, string Field, string ProviderID, string ProviderName, string ProductID, string StartDate, string EndDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return PurchaseOrderDBHelper.GetPurCll(CompanyCD, Field, ProviderID, ProviderName, ProductID, StartDate, EndDate, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetPurCll(string CompanyCD, string ProviderID, string ProductID, string StartDate, string EndDate)
        {
            try
            {
                return PurchaseOrderDBHelper.GetPurCll(CompanyCD, ProviderID, ProductID, StartDate, EndDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获得采购订单编号
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页记录数</param>
        /// <param name="orderBy">排序方法</param>
        /// <param name="TotalCount">总记录数</param>
        /// <param name="userInfo">用户信息实体类</param>
        /// <returns></returns>
        public static DataTable SelectPurchaseOrder(int pageIndex, int pageCount, string orderBy, ref int TotalCount
            , UserInfoUtil userInfo)
        {
            return PurchaseOrderDBHelper.SelectPurchaseOrder(pageIndex, pageCount, orderBy, ref TotalCount, userInfo);
        }

        /// <summary>
        /// 在途物品查询
        /// </summary>
        /// <param name="CompanyCD">公司CD</param>
        /// <param name="ProductID">产品ID</param>
        /// <param name="ProviderID">供应商ID</param>
        /// <param name="StartDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <param name="OrderNo">订单编号</param>
        /// <param name="pageIndex">查询页</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="OrderBy">排序</param>
        /// <param name="TotalCount">总数</param>
        /// <returns></returns>
        public static DataTable ProdOnRoadQry(string CompanyCD, string ProductID, string ProviderID
            , string StartDate, string EndDate, string OrderNo, int pageIndex
            , int pageSize, string OrderBy, ref int TotalCount)
        {
            try
            {
                return PurchaseOrderDBHelper.ProdOnRoadQry(CompanyCD, ProductID, ProviderID, StartDate, EndDate, OrderNo, pageIndex, pageSize, OrderBy, ref TotalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable ProdOnRoadQry(string CompanyCD, string ProductID, string ProviderID, string StartDate, string EndDate, string OrderBy)
        {
            try
            {
                return PurchaseOrderDBHelper.ProdOnRoadQry(CompanyCD, ProductID, ProviderID, StartDate, EndDate, OrderBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 采购订单数量部门分布
        /// </summary>
        public static DataTable GetOrderByTypeNum(string OrderStatus, string FlowStatus, string FromType, string BeginDate, string EndDate)
        {
            return PurchaseOrderDBHelper.GetOrderByTypeNum(OrderStatus, FlowStatus, FromType, BeginDate, EndDate);
        }
        /// <summary>
        /// 采购订单数量部门分布
        /// </summary>
        public static DataTable GetOrderByDeptNum(string OrderStatus, string FlowStatus, string FromType, string BeginDate, string EndDate)
        {
            return PurchaseOrderDBHelper.GetOrderByDeptNum(OrderStatus, FlowStatus, FromType, BeginDate, EndDate);
        }

        /// <summary>
        /// 采购数量供应商分布
        /// </summary>
        public static DataTable GetOrderByProviderNum(string OrderStatus, string FlowStatus, string FromType, string BeginDate, string EndDate)
        {
            return PurchaseOrderDBHelper.GetOrderByProviderNum(OrderStatus, FlowStatus, FromType, BeginDate, EndDate);
        }

        /// <summary>
        /// 采购数量走势
        /// </summary>
        public static DataTable GetOrderByTrendNum(string OrderStatus, string FlowStatus, string FromType, string Type, string DateType, string BeginDate, string EndDate)
        {
            return PurchaseOrderDBHelper.GetOrderByTrendNum(OrderStatus, FlowStatus, FromType, Type, DateType, BeginDate, EndDate);
        }


        /// <summary>
        /// 采购订单金额部门分布
        /// </summary>
        public static DataTable GetOrderByTypePrice(string OrderStatus, string FlowStatus, string FromType, string BeginDate, string EndDate)
        {
            return PurchaseOrderDBHelper.GetOrderByTypePrice(OrderStatus, FlowStatus, FromType, BeginDate, EndDate);
        }
        /// <summary>
        /// 采购订单金额部门分布
        /// </summary>
        public static DataTable GetOrderByDeptPrice(string OrderStatus, string FlowStatus, string FromType, string BeginDate, string EndDate)
        {
            return PurchaseOrderDBHelper.GetOrderByDeptPrice(OrderStatus, FlowStatus, FromType, BeginDate, EndDate);
        }

        /// <summary>
        /// 采购金额供应商分布
        /// </summary>
        public static DataTable GetOrderByProviderPrice(string OrderStatus, string FlowStatus, string FromType, string BeginDate, string EndDate)
        {
            return PurchaseOrderDBHelper.GetOrderByProviderPrice(OrderStatus, FlowStatus, FromType, BeginDate, EndDate);
        }

        /// <summary>
        /// 采购金额走势
        /// </summary>
        public static DataTable GetOrderByTrendPrice(string OrderStatus, string FlowStatus, string FromType, string Type, string DateType, string BeginDate, string EndDate)
        {
            return PurchaseOrderDBHelper.GetOrderByTrendPrice(OrderStatus, FlowStatus, FromType, Type, DateType, BeginDate, EndDate);
        }

        public static DataTable GetPurchaseOrderDetail(string OrderStatus, string FlowStatus, string FromType, string Type, string DateType, string BeginDate, string EndDate, string DeptId, string ProviderId, string DateValue, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            return PurchaseOrderDBHelper.GetPurchaseOrderDetail(OrderStatus, FlowStatus, FromType, Type, DateType, BeginDate, EndDate, DeptId, ProviderId, DateValue, pageIndex, pageCount, OrderBy, ref totalCount);
        }

        /// <summary>
        /// 采购物品分布
        /// </summary>
        public static DataTable GetOrderByProduct(string ProviderID, string DeptID, string BeginDate, string EndDate, string StatType)
        {
            return PurchaseOrderDBHelper.GetOrderByProduct(ProviderID, DeptID, BeginDate, EndDate, StatType);
        }

        /// <summary>
        /// 采购物品走势
        /// </summary>
        public static DataTable GetOrderByProductTrend(string ProviderID, string DeptID, string BeginDate, string EndDate, string DateType, string StatType, string ProductID)
        {
            return PurchaseOrderDBHelper.GetOrderByProductTrend(ProviderID, DeptID, BeginDate, EndDate, DateType, StatType, ProductID);
        }

        /// <summary>
        /// 采购物品分析
        /// </summary>
        public static DataTable GetPurchaseOrderProductDetail(string ProviderID, string DeptID, string BeginDate, string EndDate, string DateType, string DateValue, string ProductID, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            return PurchaseOrderDBHelper.GetPurchaseOrderProductDetail(ProviderID, DeptID, BeginDate, EndDate, DateType, DateValue, ProductID, pageIndex, pageCount, OrderBy, ref totalCount);
        }
    }
}

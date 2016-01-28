/**********************************************
 * 类作用：   采购管理事务层处理
 * 建立人：   王超
 * 建立时间： 2009/03/05
 ***********************************************/

using System;
using System.Collections.Generic;
using XBase.Model.Office.PurchaseManager;
using XBase.Data.Office.PurchaseManager;
using XBase.Common;
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using XBase.Model.Common;
using XBase.Business.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.PurchaseManager
{
    /// <summary>
    /// 类名：PurchaseApplyBus
    /// 描述：采购管理事务层处理
    /// 
    /// 作者：王超
    /// 创建时间：2009/03/05
    /// </summary>

    public class PurchaseApplyBus
    {
        #region 新建采购申请
        public static bool InsertPurchaseApply(PurchaseApplyModel PurchaseApplyM, List<PurchaseApplyDetailSourceModel> PurchaseApplyDetailSourceMList
            , List<PurchaseApplyDetailModel> PurchaseApplyDetailMList, out int IndexIDentity, Hashtable htExtAttr)
        {

            ArrayList lstAdd = new ArrayList();

            //插入主表
            SqlCommand AddPri = PurchaseApplyDBHelper.InsertPrimary(PurchaseApplyM);
            lstAdd.Add(AddPri);

            string ApplyNo = PurchaseApplyM.ApplyNo;
            foreach (PurchaseApplyDetailSourceModel PurchaseApplyDetailSourceM in PurchaseApplyDetailSourceMList)
            {
                SqlCommand AddDtlS = PurchaseApplyDBHelper.InsertDtlS(PurchaseApplyDetailSourceM, ApplyNo);
                lstAdd.Add(AddDtlS);
            }
            foreach (PurchaseApplyDetailModel PurchaseApplyDetailM in PurchaseApplyDetailMList)
            {
                SqlCommand AddDtl = PurchaseApplyDBHelper.InsertDtl(PurchaseApplyDetailM, ApplyNo);
                lstAdd.Add(AddDtl);
            }
            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(PurchaseApplyM, htExtAttr, cmd);
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

            LogInfoModel logModel = InitLogInfo(ApplyNo);
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
        private static void GetExtAttrCmd(PurchaseApplyModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.PurchaseApply set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND ApplyNo = @ApplyNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@ApplyNo", model.ApplyNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion
        #region 查询采购申请
        public static DataTable SelectPurchaseApply(PurchaseApplyModel PurchaseApplyM, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                DataTable dt = PurchaseApplyDBHelper.SelectPrimary(PurchaseApplyM, pageIndex, pageCount, OrderBy, ref totalCount);
                //dt.Columns.Add("IsCite");
                //dt.Columns.Add("IsCiteName");

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string ID = dt.Rows[i]["ID"].ToString();

                //    bool IsCite = PurchaseApplyBus.IsCitePurApply(ID);

                //    dt.Rows[i]["IsCite"] = IsCite;
                //    dt.Rows[i]["IsCiteName"] = IsCite ? '是' : '否';
                //}

                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable SelectPurchaseApply(PurchaseApplyModel PurchaseApplyM, string OrderBy)
        {
            try
            {
                DataTable dt = PurchaseApplyDBHelper.SelectPrimary(PurchaseApplyM, OrderBy);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 修改采购申请
        /// <summary>
        /// 修改采购申请
        /// </summary>
        /// <param name="PurchaseApplyM">主表</param>
        /// <param name="str">明细来源</param>
        /// <param name="start"></param>
        /// <param name="end">从start--end需要更新，end以后要插入</param>
        /// <param name="str2">明细信息</param>
        /// <param name="ApplyNo"></param>
        /// <returns></returns>
        public static bool UpdatePurchaseApply(PurchaseApplyModel PurchaseApplyM, List<PurchaseApplyDetailSourceModel> PurchaseApplyDetailSourceMList
            , List<PurchaseApplyDetailModel> PurchaseApplyDetailMList, Hashtable htExtAttr)
        {
            try
            {
                ArrayList lstUpdate = new ArrayList();
                //更新主表
                SqlCommand UpdatePri = PurchaseApplyDBHelper.UpdatePrimary(PurchaseApplyM);
                lstUpdate.Add(UpdatePri);

                string ApplyNo = PurchaseApplyM.ApplyNo;
                string FromType = PurchaseApplyM.FromType;

                #region 拓展属性
                SqlCommand cmd = new SqlCommand();
                GetExtAttrCmd(PurchaseApplyM, htExtAttr, cmd);
                if (htExtAttr.Count > 0)
                    lstUpdate.Add(cmd);
                #endregion


                //删除原来明细来源
                SqlCommand DelDtlS = PurchaseApplyDBHelper.DeleteDtlS(ApplyNo);
                lstUpdate.Add(DelDtlS);

                //插入现在的明细来源
                foreach (PurchaseApplyDetailSourceModel PurchaseApplyDetailSourceM in PurchaseApplyDetailSourceMList)
                {
                    SqlCommand AddDtlS = PurchaseApplyDBHelper.InsertDtlS(PurchaseApplyDetailSourceM, ApplyNo);
                    lstUpdate.Add(AddDtlS);
                }

                //删除原来明细
                SqlCommand DelDtl = PurchaseApplyDBHelper.DeleteDtl(ApplyNo);
                lstUpdate.Add(DelDtl);

                //插入现在的明细
                foreach (PurchaseApplyDetailModel PurchaseApplyDetailM in PurchaseApplyDetailMList)
                {
                    SqlCommand AddDtl = PurchaseApplyDBHelper.InsertDtl(PurchaseApplyDetailM, ApplyNo);
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

                LogInfoModel logModel = InitLogInfo(ApplyNo);
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

        #region 根据ID填充
        //填充主表
        public static DataTable GetPurchaseApply(string ID)
        {
            SqlCommand comm = PurchaseApplyDBHelper.GetPurchaseApply(ID);
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            dt.Columns.Add("IsCite");
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                //string ID = dt.Rows[i]["ID"].ToString();
                bool IsCite = PurchaseApplyDBHelper.IsCitePurApply(ID);
                dt.Rows[i]["IsCite"] = IsCite;
            }
            return dt;
        }
        //填充明细来源
        public static DataTable GetPurchaseApplySource(string ID)
        {
            SqlCommand comm = PurchaseApplyDBHelper.GetPurchaseApplySource(ID);
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            return dt;
        }
        //填充明细
        public static DataTable GetPurchaseApplyDetail(string ID)
        {
            SqlCommand comm = PurchaseApplyDBHelper.GetPurchaseApplyDetail(ID);
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            return dt;
        }
        #endregion

        #region 判断采购申请有没有被引用
        public static bool IsCitePurApply(string ID)
        {
            try
            {
                return PurchaseApplyDBHelper.IsCitePurApply(ID);
            }
            catch 
            {
                throw;
            }
        }
        #endregion

        #region 删除采购申请
        public static bool DeleteApply(string ApplyNos, string IDs)
        {
            ArrayList lstDel = new ArrayList();
            //删除主表
            SqlCommand DelPri = PurchaseApplyDBHelper.DeletePurchaseApply(IDs);
            lstDel.Add(DelPri);
            //删除明细来源
            SqlCommand DelDtlS = PurchaseApplyDBHelper.DeleteDtlS(ApplyNos);
            lstDel.Add(DelDtlS);
            //删除明细
            SqlCommand DelDtl = PurchaseApplyDBHelper.DeleteDtl(ApplyNos);
            lstDel.Add(DelDtl);


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
                isSucc = SqlHelper.ExecuteTransWithArrayList(lstDel);
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
            string[] noList = ApplyNos.Split(',');
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

        #region 确认采购申请
        public static bool ConfirmPurchaseApply(string ID)
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
                    isSucc = PurchaseApplyDBHelper.ConfirmPurchaseApply(ID);
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

        #region 取消确认采购申请
        public static bool CancelConfirm(string ID,string No)
        {
            try
            {
                ////已经被引用的采购申请不能取消确认
                //if (IsCitePurApply(ID))
                //{
                //    return false;
                //}
                ArrayList lstCancelConfirm = new ArrayList();
                //更改主表
                lstCancelConfirm.Add(PurchaseApplyDBHelper.CancelConfirm(ID));
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

                //撤销审批
                string CompanyCD = userInfo.CompanyCD;
                string BillTypeFlag = ConstUtil.CODING_RULE_PURCHASE;
                string BillTypeCode = ConstUtil.CODING_RULE_PURCHASE_APPLY;
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

        #region 采购申请结单
        public static bool CompletePurchaseApply(string ID)
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
                    isSucc = PurchaseApplyDBHelper.CompletePuechaseApply(ID);
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

        #region 采购申请取消结单
        public static bool ConcelCompletePurchaseApply(string ID)
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
                    isSucc = PurchaseApplyDBHelper.ConcelCompletePurchaseApply(ID);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_PURCHASEAPPLY_ADD;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_PURCHASEAPPLY_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PURCHASEAPPLY;
            //操作对象
            logModel.ObjectID = ApplyNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion


        #region 销售订单控件取值
        public static DataTable GetSellOrderDetail(string CompanyCD,string ProductNo, string ProductName, string StartDate, string EndDate
            , int pageIndex, int pageCount, string OrderBy, string OrderByType, ref int totalCount)
        {
            try
            {
                return PurchaseApplyDBHelper.GetSellOrderDetail(CompanyCD, ProductNo, ProductName, StartDate, EndDate, pageIndex, pageCount, OrderBy, OrderByType, ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 物料需求控件取值
        public static DataTable GetMaterialDetail(string CompanyCD, string ProductNo, string ProductName, string StartDate, string EndDate
            , int pageIndex, int pageCount, string OrderBy, string OrderByType, ref int totalCount)
        {
            try
            {
                return PurchaseApplyDBHelper.GetMaterialDetail(CompanyCD, ProductNo, ProductName, StartDate, EndDate, pageIndex, pageCount, OrderBy, OrderByType, ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 使用商品名称查询数据
        public static DataTable GetGoodsByProductName(string CompanyCD, string ProductNameKeys)
        {
            return XBase.Data.Office.PurchaseManager.PurchaseApplyDBHelper.GetGoodsByProductName(CompanyCD, ProductNameKeys);
        }
        #endregion
    }
}

/***********************************************
 * 类作用：   门店管理销售订单事务层处理       *
 * 建立人：   王超                             *
 * 建立时间： 2009/05/21                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Model.Office.SubStoreManager;
using XBase.Data.Office.SubStoreManager;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;
using System.Data.SqlTypes;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using System.Collections;
using XBase.Business.Office.SellManager;

namespace XBase.Business.Office.SubStoreManager
{
    public class SubSellOrderBus
    {
        #region 新增
        public static bool InsertSubSellOrder(SubSellOrderModel SubSellOrderM, List<SubSellOrderDetailModel> SubSellOrderDetailMList, out int IndexIDentity, Hashtable htExtAttr)
        {
            ArrayList lstAdd = new ArrayList();

            //主表新增
            SqlCommand AddPri = SubSellOrderDBHelper.InsertSubSellOrder(SubSellOrderM);
            lstAdd.Add(AddPri);

            // 更新扩展属性
            SqlCommand commExtAttr = SubSellOrderDBHelper.UpdateExtAttr(SubSellOrderM, htExtAttr);
            if (commExtAttr != null)
            {
                lstAdd.Add(commExtAttr);
            }

            //明细表新增
            foreach (SubSellOrderDetailModel SubSellOrderDetailM in SubSellOrderDetailMList)
            {
                SqlCommand AddDtl = SubSellOrderDBHelper.InsertSubSellOrderDetail(SubSellOrderDetailM);
                lstAdd.Add(AddDtl);
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

            LogInfoModel logModel = InitLogInfo(SubSellOrderM.OrderNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }
        #endregion

        #region 更新
        public static bool UpdateSubSellOrder(SubSellOrderModel SubSellOrderM, List<SubSellOrderDetailModel> SubSellOrderDetailMList, Hashtable htExtAttr)
        {
            ArrayList lstUpdate = new ArrayList();
            //更新主表
            SqlCommand UpdatePri = SubSellOrderDBHelper.UpdateSubSellOrder(SubSellOrderM);
            lstUpdate.Add(UpdatePri);

            // 更新扩展属性
            SqlCommand commExtAttr = SubSellOrderDBHelper.UpdateExtAttr(SubSellOrderM, htExtAttr);
            if (commExtAttr != null)
            {
                lstUpdate.Add(commExtAttr);
            }

            //删除明细
            SqlCommand DeleteDetail = SubSellOrderDBHelper.DeleteSubSellOrderDetail(SubSellOrderM.OrderNo);
            lstUpdate.Add(DeleteDetail);
            //新增明细
            foreach (SubSellOrderDetailModel SubSellOrderDetailM in SubSellOrderDetailMList)
            {
                SqlCommand AddDetail = SubSellOrderDBHelper.InsertSubSellOrderDetail(SubSellOrderDetailM);
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

            LogInfoModel logModel = InitLogInfo(SubSellOrderM.OrderNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }
        #endregion

        #region 删除
        public static bool DeleteSubSellOrder(string IDs, string OrderNos)
        {
            ArrayList lstDelete = new ArrayList();

            SqlCommand deletePri = SubSellOrderDBHelper.DeleteSubSellOrder(IDs);
            lstDelete.Add(deletePri);

            SqlCommand deleteDeteil = SubSellOrderDBHelper.DeleteSubSellOrderDetail(OrderNos);
            lstDelete.Add(deleteDeteil);

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
        #endregion


        #region 确认
        //不需要插入客户
        public static bool ConfirmSubSellOrder(string ID, string No)
        {
            ArrayList lstConfirm = new ArrayList();

            //单据确认人，确认时间，单据状态，业务状态
            SqlCommand Confirm = SubSellOrderDBHelper.ConfirmSubSellOrder(ID);
            lstConfirm.Add(Confirm);

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
        //需要插入客户
        public static bool ConfirmSubSellOrder(string ID, string No, SubSellCustInfoModel SubSellCustInfoM)
        {
            ArrayList lstConfirm = new ArrayList();

            //单据确认人，确认时间，单据状态，业务状态
            SqlCommand Confirm = SubSellOrderDBHelper.ConfirmSubSellOrder(ID);
            lstConfirm.Add(Confirm);

            //新增客户信息
            SqlCommand AddCust = SubSellOrderDBHelper.InsertCustInfo(SubSellCustInfoM);
            lstConfirm.Add(AddCust);

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
        public static bool ConcelConfirm(string ID)
        {
            try
            {
                //不符合可以取消确认的条件
                if (!SubSellOrderDBHelper.CanConcelConfirm(ID))
                    return false;
                SqlCommand comm = SubSellOrderDBHelper.ConcelConfirm(ID);
                if (SqlHelper.ExecuteTransWithCommand(comm))
                    return true;//取消确认成功
                return false;//取消确认失败
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 销售订单发货确认
        /// <summary>
        /// 销售订单发货确认判断
        /// </summary>
        /// <param name="SubSellOrderM"></param>
        /// <param name="SubSellOrderDetailMList"></param>
        /// <param name="ProductID">负库存产品ID</param>
        /// <returns>返回1 则数量合法，2 则确认后会有负库存，且不允许出现负库存，3 则确认后会有负库存，允许出现负库存</returns>
        public static int CanConfirmOutSubSellOrder(SubSellOrderModel SubSellOrderM, List<SubSellOrderDetailModel> SubSellOrderDetailMList, ref string ProductID)
        {
            try
            {
                int a = 1;
                Dictionary<int, List<string>> dic = new Dictionary<int, List<string>>();

                //判断是总部发货还是分店发货
                if (SubSellOrderM.SendMode == "1")
                {//分店发货
                    foreach (SubSellOrderDetailModel item in SubSellOrderDetailMList)
                    {
                        a = SubSellOrderDBHelper.CanConfirmOutSub(SubSellOrderM.DeptID, item.ProductID, item.ProductCount, item.BatchNo);
                        if (dic.ContainsKey(a))
                        {// 状态存在
                            if (!dic[a].Contains(item.ProductID))
                            {
                                dic[a].Add(item.ProductID);
                            }
                        }
                        else
                        {// 状态不存在
                            dic.Add(a, new List<string>() { item.ProductID });
                        }
                    }

                }
                else if (SubSellOrderM.SendMode == "2")
                {//总部发货
                    foreach (SubSellOrderDetailModel item in SubSellOrderDetailMList)
                    {
                        a = SubSellOrderDBHelper.CanConfirmOutHq(item.StorageID, item.ProductID, item.ProductCount, item.BatchNo);
                        if (dic.ContainsKey(a))
                        {// 状态存在
                            if (!dic[a].Contains(item.ProductID))
                            {
                                dic[a].Add(item.ProductID);
                            }
                        }
                        else
                        {// 状态不存在
                            dic.Add(a, new List<string>() { item.ProductID });
                        }

                    }
                }
                if (dic.ContainsKey(2))
                {
                    a = 2;
                }
                else if (dic.ContainsKey(3))
                {
                    a = 3;
                }
                else
                {
                    a = 1;
                }
                ProductID = string.Join(",", dic[a].ToArray());
                return a;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="SubSellOrderM"></param>
        /// <param name="SubSellOrderDetailMList"></param>
        /// <returns></returns>
        public static bool ConfirmOutSubSellOrder(SubSellOrderModel SubSellOrderM, List<SubSellOrderDetailModel> SubSellOrderDetailMList)
        {
            try
            {
                ArrayList lstConfirm = new ArrayList();
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                int id = 0;
                decimal count = 0m;
                if (SubSellOrderM.SendMode == "1")
                {//分店发货
                    //确认时，更新分店存量表
                    foreach (SubSellOrderDetailModel SubSellOrderDetailM in SubSellOrderDetailMList)
                    {
                        #region 添加门店库存流水帐
                        SubStorageAccountModel aModel = new SubStorageAccountModel();
                        aModel.BatchNo = SubSellOrderDetailM.BatchNo;
                        aModel.BillNo = SubSellOrderM.OrderNo;
                        aModel.BillType = 3;
                        aModel.CompanyCD = SubSellOrderDetailM.CompanyCD;
                        aModel.PageUrl = "";
                        if (int.TryParse(SubSellOrderM.Creator, out id))
                        {
                            aModel.Creator = id;
                        }
                        if (int.TryParse(SubSellOrderM.DeptID, out id))
                        {
                            aModel.DeptID = id;
                        }
                        aModel.HappenDate = DateTime.Now;
                        if (int.TryParse(SubSellOrderDetailM.ProductID, out id))
                        {
                            aModel.ProductID = id;
                        }
                        if (decimal.TryParse(SubSellOrderDetailM.UnitPrice, out count))
                        {
                            aModel.Price = count;
                        }
                        if (decimal.TryParse(SubSellOrderDetailM.ProductCount, out count))
                        {
                            aModel.HappenCount = -count;
                        }
                        aModel.PageUrl = SubSellOrderM.Remark;
                        lstConfirm.Add(XBase.Data.Office.SubStoreManager.SubStorageAccountDBHelper.GetCountAndInsertCommand(aModel));
                        #endregion

                        lstConfirm.Add(XBase.Data.Office.LogisticsDistributionManager.StorageProductQueryDBHelper.UpdateProductCount(userInfo.CompanyCD
                            , SubSellOrderDetailM.ProductID, SubSellOrderM.DeptID, SubSellOrderDetailM.BatchNo, -count));
                    }
                }
                else if (SubSellOrderM.SendMode == "2")
                {//总部发货
                    //确认时更新分仓存量表
                    foreach (SubSellOrderDetailModel SubSellOrderDetailM in SubSellOrderDetailMList)
                    {
                        #region 添加库存流水帐

                        XBase.Model.Office.StorageManager.StorageAccountModel sModel = new XBase.Model.Office.StorageManager.StorageAccountModel();
                        sModel.BatchNo = SubSellOrderDetailM.BatchNo;
                        sModel.BillNo = SubSellOrderM.OrderNo;
                        sModel.BillType = 21;
                        sModel.CompanyCD = SubSellOrderDetailM.CompanyCD;
                        if (int.TryParse(SubSellOrderDetailM.StorageID, out id))
                        {
                            sModel.StorageID = id;
                        }
                        if (int.TryParse(SubSellOrderM.Creator, out id))
                        {
                            sModel.Creator = id;
                        }
                        if (int.TryParse(SubSellOrderDetailM.ProductID, out id))
                        {
                            sModel.ProductID = id;
                        }
                        if (decimal.TryParse(SubSellOrderDetailM.UnitPrice, out count))
                        {
                            sModel.Price = count;
                        }
                        if (decimal.TryParse(SubSellOrderDetailM.ProductCount, out count))
                        {
                            sModel.HappenCount = count;
                        }
                        sModel.HappenDate = DateTime.Now;
                        sModel.PageUrl = SubSellOrderM.Remark;
                        lstConfirm.Add(XBase.Data.Office.StorageManager.StorageAccountDBHelper.InsertStorageAccountCommand(sModel, "1"));

                        #endregion

                        // 更新库存
                        lstConfirm.Add(XBase.Data.Office.StorageManager.StorageSearchDBHelper.UpdateProductCount(userInfo.CompanyCD
                            , SubSellOrderDetailM.ProductID, SubSellOrderDetailM.StorageID
                            , SubSellOrderDetailM.BatchNo, -count));
                    }
                }

                //更新发货信息和安装信息
                lstConfirm.Add(SubSellOrderDBHelper.ConfirmOutSubSellOrder(SubSellOrderM));

                //获取登陆用户信息
                //UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
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

                LogInfoModel logModel = InitLogInfo(SubSellOrderM.OrderNo);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_OUT;
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

        #region 销售订单结算确认
        public static bool ConfirmSttlSubSellOrder(SubSellOrderModel SubSellOrderM)
        {
            try
            {
                SqlCommand comm = SubSellOrderDBHelper.ConfirmSettSubSellOrder(SubSellOrderM);
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
                    isSucc = SqlHelper.ExecuteTransWithCommand(comm);
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

                LogInfoModel logModel = InitLogInfo(SubSellOrderM.OrderNo);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_STTL;
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

        #region 检索客户信息
        public static DataTable GetCustInfo(SubSellCustInfoModel SubSellCustInfoM)
        {
            try
            {
                SqlCommand comm = SubSellOrderDBHelper.GetCustInfo(SubSellCustInfoM);
                return SqlHelper.ExecuteSearch(comm);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_SUBSTOREMANAGER_SELLORDERADD;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_SUBSTOREMANAGER_SELLORDERADD;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_SUBSTOREMANAGER_SUBSELLORDER;
            //操作对象
            logModel.ObjectID = ApplyNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        #region 根据销售订单ID检索销售订单信息
        //检索主表
        public static DataTable GetSubSellOrder(string ID)
        {
            try
            {
                SqlCommand comm = SubSellOrderDBHelper.GetSubSellOrder(ID);
                return SqlHelper.ExecuteSearch(comm);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //检索明细
        public static DataTable GetSubSellOrderDetail(string ID)
        {
            try
            {
                SqlCommand comm = SubSellOrderDBHelper.GetSubSellOrderDetail(ID);
                return SqlHelper.ExecuteSearch(comm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///单据打印 
        //检索主表
        public static DataTable GetSubSellOrderPrint(string ID)
        {
            try
            {
                SqlCommand comm = SubSellOrderDBHelper.GetSubSellOrderPrint(ID);
                return SqlHelper.ExecuteSearch(comm);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //检索明细
        public static DataTable GetSubSellOrderDetailPrint(string ID)
        {
            try
            {
                SqlCommand comm = SubSellOrderDBHelper.GetSubSellOrderDetailPrint(ID);
                return SqlHelper.ExecuteSearch(comm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 检索销售订单
        public static DataTable SelectSubSellOrder(SubSellOrderModel SubSellOrderM, int pageIndex, int pageCount, string OrderBy, string EFIndex, string EFDesc, ref int totalCount)
        {
            try
            {
                SqlCommand comm = SubSellOrderDBHelper.SelectSubSellOrder(SubSellOrderM, pageIndex, pageCount, OrderBy, EFIndex, EFDesc, ref totalCount);
                //执行查询
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 分店物品控件
        /// <summary>
        /// 分店物品控件
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="Rate"></param>
        /// <param name="LastRate"></param>
        /// <param name="totalCount"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <param name="ProductName"></param>
        /// <param name="ProductNo"></param>
        /// <param name="Specification"></param>
        /// <returns></returns>
        public static DataTable GetProdPrice(string CompanyCD, string DeptID, int pageIndex
            , int pageCount, string OrderBy, string Rate, string LastRate, ref int totalCount
            , string EFIndex, string EFDesc, string ProductName, string ProductNo, string Specification)
        {
            try
            {
                return SubSellOrderDBHelper.GetProdPrice(CompanyCD, DeptID, pageIndex, pageCount, OrderBy, Rate, LastRate, ref totalCount, EFIndex, EFDesc, ProductName, ProductNo, Specification);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 分店物品-条码扫描-控件
        public static DataTable GetProdPrice(string CompanyCD, string DeptID, string Rate, string LastRate, string EFIndex, string EFDesc, string BarCode)
        {
            try
            {
                return SubSellOrderDBHelper.GetProdPrice(CompanyCD, DeptID, Rate, LastRate, EFIndex, EFDesc, BarCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 分店客户需要
        /// <summary>
        /// 添加分店客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool SubStorageCustAdd(SubSellCustInfoModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {

                bool result = false;
                LogInfoModel logModel = InitLogInfo(model.CustName);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                result = SubSellOrderDBHelper.SubStorageCustAdd(model);
                if (!result)
                {
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                }
                else
                {
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                    model.ID = IDIdentityUtil.GetIDIdentity("officedba.SubSellCustInfo").ToString();
                }
                LogDBHelper.InsertLog(logModel);
                return result;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex);
                return false;
            }
        }
        /// <summary>
        /// 检索客户
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetAllCustInfo(SubSellCustInfoModel model, string Method, int pageIndex, int pageSize, string OrderBy, ref int TotalCount)
        {

            return SubSellOrderDBHelper.GetAllCust(model, Method, pageIndex, pageSize, OrderBy, ref TotalCount);

        }
        /// <summary>
        /// 获取一个
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetOneData(int ID)
        {
            return SubSellOrderDBHelper.GetOneData(ID);
        }
        public static bool UpdateCust(SubSellCustInfoModel model)
        {
            return SubSellOrderDBHelper.UpdateCust(model);
        }
        public static bool DeleteCust(int ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //string CompanyCD = "AAAAAA";
            bool isSucc = SubSellOrderDBHelper.DelCust(ID);
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
            string[] noList = ID.ToString().Split(',');
            //遍历所有编号，登陆操作日志
            for (int i = 0; i < noList.Length; i++)
            {
                //获取编号
                string no = noList[i];
                //替换两边的 '
                no = no.Replace("'", string.Empty);

                //操作日志
                LogInfoModel logModel = InitLogInfo("分店客户ID：" + no);
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
    }
}

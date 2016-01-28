using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Common;
using XBase.Data.Office.LogisticsDistributionManager;
namespace XBase.Business.Office.LogisticsDistributionManager
{
    public class SubProductSellPriceBus
    {

        #region 读取分店列表
        public static DataTable GetSubStore(Model.Office.HumanManager.DeptModel model)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubProductSendPriceDBHelper.GetSubStore(model);
        }
        #endregion

        #region 添加零售价格
        public static string AddSubProductSellPrice(Model.Office.LogisticsDistributionManager.SubProductSellPrice model)
        {
            //定义返回变量
            string res = string.Empty;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取当前用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //执行操作
            try
            {
                //执行操作
                res = XBase.Data.Office.LogisticsDistributionManager.SubProductSellPriceDBHelper.AddSubProductSellPrice(model);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res != string.Empty && res != "-1")
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
            LogInfoModel logModel = InitLogInfo("无");
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT; ;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;

        }
        #endregion

        #region 读取价格配置列表
        public static DataTable GetSubProductSellPriceList(Hashtable htPara, ref int TotalCount)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubProductSellPriceDBHelper.GetSubProductSellPriceList(htPara, ref TotalCount);
        }

        /*不分页*/
        public static DataTable GetSubProductSellPriceList(Hashtable htPara)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubProductSellPriceDBHelper.GetSubProductSellPriceList(htPara);
        }
        #endregion

        #region 更新价格配置
        public static string UpdateSubProductSellPrice(Model.Office.LogisticsDistributionManager.SubProductSellPrice model)
        {
            //return XBase.Data.Office.LogisticsDistributionManager.SubProductSendPriceDBHelper.UpdateSubProductSendPrice(model);
            //定义返回变量
            string res = string.Empty;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取当前用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //执行操作
            try
            {
                //执行操作
                res = XBase.Data.Office.LogisticsDistributionManager.SubProductSellPriceDBHelper.UpdateSubProductSellPrice(model);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res == "1")
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
            LogInfoModel logModel = InitLogInfo(model.ID.ToString());
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE; ;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;


        }
        #endregion

        #region 删除配置价格
        public static bool DelSubProductSellPrice(int[] IDList)
        {
            //定义返回变量
            bool res = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取当前用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //执行操作
            try
            {
                //执行操作
                res = XBase.Data.Office.LogisticsDistributionManager.SubProductSellPriceDBHelper.DelSubProductSellPrice(IDList);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            string tmp = string.Empty;
            for (int i = 0; i < IDList.Length; i++)
                tmp += IDList[i].ToString() + "|";


            //操作日志
            LogInfoModel logModel = InitLogInfo(tmp);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE; ;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
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
            logSys.ModuleID = ConstUtil.MODULE_ID_SUBPRODUCTSELLPRICESETTING;
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
        private static LogInfoModel InitLogInfo(string InNo)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_SUBPRODUCTSELLPRICESETTING;
            //设置操作日志类型 修改
            logModel.ObjectName = "SubProductSendPrice";
            //操作对象
            logModel.ObjectID = InNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        #region 导入销售价格

        /// <summary>
        /// 判断销售价格是否已经存在
        /// </summary>
        /// <param name="CompanyCD">公司</param>
        /// <param name="DeptName">分店</param>
        /// <param name="ProdNo">物品编号</param>
        /// <returns></returns>
        public static bool ExisitSellPrice(string CompanyCD, string DeptName, string ProdNo)
        {
            return SubProductSellPriceDBHelper.ExisitSellPrice(CompanyCD, DeptName, ProdNo);
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="userInfo">人员信息</param>
        /// <returns></returns>
        public static bool ImportData(DataTable dt, UserInfoUtil userInfo)
        {
            return SubProductSellPriceDBHelper.ImportData(dt, userInfo);
        }
        #endregion
    }
}

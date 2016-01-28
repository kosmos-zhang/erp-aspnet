using System;
using XBase.Model.Office.HumanManager;
using XBase.Data.Office.HumanManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using XBase.Model.Office.SellManager;
namespace XBase.Business.Office.HumanManager
{
  public class InputPersonalRoyaltyBus
    {
        public static bool SaveInsuPersonInfo(IList<PersonTrueIncomeTaxModel> modelist)
        {
            //定义返回变量
            bool isSucc = true;
            //信息存在时，进行操作
            if (modelist != null && modelist.Count > 0)
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //执行保存操作
                try
                {
                    //执行保存操作
                    //isSucc = InputPersonalRoyaltyDBHelper.UpdateIsuPersonalTaxInfo(modelist,"");
                }
                catch (Exception ex)
                {
                    //输出系统日志
                    WriteSystemLog(userInfo, ex);
                }
                //操作日志
                LogInfoModel logModel = InitLogInfo(userInfo.CompanyCD);
                //设置关键元素
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

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
            }

            return isSucc;
        }
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
            logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_SALARY_SET;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }

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
            logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_SALARY_SET;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_INSU_SOCIAL;//操作对象
            //
            logModel.ObjectID = no;

            return logModel;

        }
        #region 查询销售订单相关信息
        /// <summary>
        /// 查询在职部门岗位等相关信息
        /// </summary>
        /// <returns></returns>
        public static DataTable SearchPersonTaxInfo( string OrderNo, string EmpName, string StartDate, string EndDate, int pageIndex,int pageCount, string ord, ref int totalCount)
        {

            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string  CompanyCD = userInfo.CompanyCD;
            //查询并返回人员信息
            return InputPersonalRoyaltyDBHelper.SearchPersonTaxInfo(CompanyCD, OrderNo, EmpName, StartDate, EndDate,pageIndex,pageCount,ord,ref totalCount);
        }
        #endregion
        /// <summary>
        /// 根据订单编号获取信息
        /// </summary>
        /// <param name="OrdrNO"></param>
        /// <returns></returns>
        public static DataTable GetSellOrderSynchronizer(string OrdrNO)
        {
            DataTable dt =new DataTable() ;
            try
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置公司代码
                string CompanyCD = userInfo.CompanyCD;
                return InputPersonalRoyaltyDBHelper.GetSellOrderSynchronizer(OrdrNO,CompanyCD);
            }
            catch 
            {
                return dt ;
            }
         
        }
        public static bool UpdateIsuPersonalTaxInfo(DataTable dt)
        {
            try
            {
                 UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置公司代码
                string ModifiedUserID = userInfo.UserID;
                return InputPersonalRoyaltyDBHelper.UpdateIsuPersonalTaxInfo(dt, ModifiedUserID);
            }
            catch 
            {
                
                throw;
            }
        }


        public static bool UpdateSellDetailInfo(DataTable dt)
        {
            try
            {
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置公司代码
                string ModifiedUserID = userInfo.UserID;
                string CompanyCD=userInfo.CompanyCD;
                return InputPersonalRoyaltyDBHelper.UpdatePieceworkSalary(dt, ModifiedUserID,CompanyCD);
            }
            catch 
            {

                throw;
            }
        }
        public static DataTable GetSellDetail(string OrderNO)
        {
            DataTable dt = new DataTable();
            try
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置公司代码
                string CompanyCD = userInfo.CompanyCD;
                return InputPersonalRoyaltyDBHelper.GetSellDetail(CompanyCD, OrderNO);
            }
            catch 
            {
                return dt;
            }
        }

        public static DataTable GetSynchronizerRate(string ID)
        {
            DataTable dt = new DataTable();
            try
            {
                return InputPersonalRoyaltyDBHelper.GetSynchronizerRate(ID);
            }
            catch 
            {
                return dt;
            }
        }
        /// <summary>
        /// 获取个人提成工资录入
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="EmpNO"></param>
        /// <param name="EmpName"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable SearchInputPersonalRoyalty( string EmpNO, string EmpName, string StartDate, string EndDate)
        {
            DataTable dt = new DataTable();
            try
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置公司代码
                string CompanyCD = userInfo.CompanyCD;
                return InputPersonalRoyaltyDBHelper.SearchInputPersonalRoyalty(CompanyCD,EmpNO,EmpName,StartDate,EndDate);
            }
            catch 
            {
                return dt;
            }
        }
      /// <summary>
      /// 判断新老客户
      /// </summary>
      /// <param name="CustID"></param>
      /// <param name="CompanyCD"></param>
      /// <param name="OrderDate"></param>
      /// <returns></returns>
        public static bool ChargeNewOrOld(string CustID, string OrderDate)
        {
            try
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置公司代码
                string CompanyCD = userInfo.CompanyCD;
                return InputPersonalRoyaltyDBHelper.ChargeNewOrOld(CustID, CompanyCD,OrderDate);
            }
            catch 
            {
                return false;
            }
        }
        public static bool ChargeCust(string CustID)
        {
            try
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置公司代码
                string CompanyCD = userInfo.CompanyCD;
                return InputPersonalRoyaltyDBHelper.ChargeCust(CustID, CompanyCD);
            }
            catch 
            {
                return false;
            }
        }
        public static bool ChargeEmp(string EmpID)
        {
            try
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置公司代码
                string CompanyCD = userInfo.CompanyCD;
                return InputPersonalRoyaltyDBHelper.ChargeEmp(EmpID, CompanyCD);
            }
            catch 
            {
                return false;
            }
        }
        public static bool ChargeHava()
        {
            try
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置公司代码
                string CompanyCD = userInfo.CompanyCD;
                return InputPersonalRoyaltyDBHelper.ChargeHava(CompanyCD);
            }
            catch 
            {
                return false;
            }
        }
        /// <summary>
      /// 根据员工和客户获取整个个人提成设置
      /// </summary>
      /// <param name="OrdrNO"></param>
      /// <param name="CompanyCD"></param>
      /// <returns></returns>
        public static DataTable GetSellOrderSynchronizerDetail(string CustID, string EmpID)
        {
            try
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置公司代码
                string CompanyCD = userInfo.CompanyCD;
                return InputPersonalRoyaltyDBHelper.GetSellOrderSynchronizerDetail(CustID,EmpID, CompanyCD);
            }
            catch 
            {
                return null;
            }
        }
     
      /// <summary>
      /// 同步提成率
      /// </summary>
      /// <param name="ID"></param>
      /// <param name="Rate"></param>
      /// <returns></returns>
        public static bool UpdateRate(string ID, string Rate)
        {
            try
            {
                return InputPersonalRoyaltyDBHelper.UpdateRate(ID, Rate);
            }
            catch
            {
                return false;
                throw;
            }
        }
      /// <summary>
          /// 获取销售订单明细信息
          /// </summary>
          /// <param name="CompanyCD"></param>
          /// <param name="OrderNo"></param>
          /// <returns></returns>
        public static DataTable GetSellDetailProdNo(string OrdrNO)
        {
            DataTable dt = new DataTable();
            try
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置公司代码
                string CompanyCD = userInfo.CompanyCD;
                return InputPersonalRoyaltyDBHelper.GetSellDetailProdNo(CompanyCD, OrdrNO);
            }
            catch 
            {
                return dt;
            }
        }

       /// <summary>
        /// 获取所有的产品单品提成设置
      /// </summary>
      /// <param name="CompanyCD"></param>
      /// <param name="OrdrNO"></param>
      /// <returns></returns>
        public static DataTable GetCommissionItemProdNo()
        {
            DataTable dt = new DataTable();
            try
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置公司代码
                string CompanyCD = userInfo.CompanyCD;
                return InputPersonalRoyaltyDBHelper.GetCommissionItemProdNo(CompanyCD);
            }
            catch 
            {
                return dt;
            }
        }

        /// <summary>
        /// 获取销售订单明细信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetNotSetSellDetail(string OrdrNO)
        {
            DataTable dt = new DataTable();
            try
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置公司代码
                string CompanyCD = userInfo.CompanyCD;
                return InputPersonalRoyaltyDBHelper.GetNotSetSellDetail(CompanyCD, OrdrNO);
            }
            catch 
            {
                return dt;
            }
        }
    }
}

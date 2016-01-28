using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.Office.SystemManager;
using XBase.Model.Office.SystemManager;
using System.Data;
using XBase.Common;
using XBase.Business.Common;
using XBase.Data.Common;
using XBase.Model.Common;
namespace XBase.Business.Office.SystemManager
{
   public class ItemCodingRuleBus
    {
        /// <summary>
        /// 插入编码规则
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public static bool InsertItemCodingRule(ItemCodingRuleModel model)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //登陆日志
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.RuleName);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                succ = XBase.Data.Office.SystemManager.ItemCodingRuleDBHelper.InsertItemCodingRule(model);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex);
                return false;
            }

        }
        /// <summary>
        /// 修改编码规则
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public static bool UpdateItemCodingRule(ItemCodingRuleModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //登陆日志
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.RuleName);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                succ = XBase.Data.Office.SystemManager.ItemCodingRuleDBHelper.UpdateItemCodingRule(model);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }


        /// <summary>
        /// 获取公编码规则
        /// </summary>
        /// <returns></returns>
       public static DataTable GetItemCodingRule(string TypeFlag, string CompanyCD, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            if (string.IsNullOrEmpty(TypeFlag))
                return null;
            try
            {
                return XBase.Data.Office.SystemManager.ItemCodingRuleDBHelper.GetItemCodingRule(TypeFlag, CompanyCD, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        /// <summary>
        /// 删除编码规则
        /// </summary>
        /// <param name="TypeFlag"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
       public static bool DeleteItemCodingRule(string ID, string CompanyCD)
        {

            if (string.IsNullOrEmpty(ID))
                return false;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //string CompanyCD = "AAAAAA";
            bool isSucc = XBase.Data.Office.SystemManager.ItemCodingRuleDBHelper.DeleteItemCodingRule(ID, CompanyCD);
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
                LogInfoModel logModel = InitLogInfo("编号规则ID：" + no);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                //设置操作成功标识
                logModel.Remark = remark;

                //登陆日志
                LogDBHelper.InsertLog(logModel);
            }
            return isSucc;
        }

       /// <summary>
       /// 校验编号的唯一性
       /// </summary>
       /// <param name="tableName">表名</param>
       /// <param name="columnName">列名</param>
       /// <param name="codeValue">输入的编码值</param>
       /// <returns>bool 是否已经存在 true 不存在 false 存在</returns>
       public static bool CheckCodeUniq(string RuleName, string CodingType, string ItemTypeID)
       {
           string companyCD = string.Empty;
           //获取公司代码
           try
           {
               companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           }
           catch (System.Exception)
           {

               companyCD = "AAAAAA"; ;
           }
           //校验存在性
           return XBase.Data.Office.SystemManager.ItemCodingRuleDBHelper.CheckCodeUniq(RuleName, CodingType, ItemTypeID, companyCD);
       }
       /// <summary>
       /// 校验编号实例唯一性
       /// </summary>
       /// <param name="RuleName"></param>
       /// <param name="CodingType"></param>
       /// <param name="ItemTypeID"></param>
       /// <param name="companyCD"></param>
       /// <returns></returns>
       public static bool CheckCodeRuleExample(string RuleName, string CodingType, string ItemTypeID)
       {
           string companyCD = string.Empty;
           //获取公司代码
           try
           {
               companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           }
           catch (System.Exception)
           {

               companyCD = "AAAAAA"; ;
           }
           //校验存在性
           return XBase.Data.Office.SystemManager.ItemCodingRuleDBHelper.CheckCodeRuleExample(RuleName, CodingType, ItemTypeID, companyCD);
       }

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
           logSys.ModuleID = ConstUtil.Menu_ItemCodingRule;//待改
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
       private static LogInfoModel InitLogInfo(string prodno)
       {
           LogInfoModel logModel = new LogInfoModel();
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //设置公司代码
           logModel.CompanyCD = userInfo.CompanyCD;
           //设置登陆用户ID
           logModel.UserID = userInfo.UserID;
           //设置模块ID 模块ID请在ConstUtil中定义，以便维护
           logModel.ModuleID = ConstUtil.Menu_ItemCodingRule;
           //设置操作日志类型 修改
           logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_ItemCodingRule;
           //操作对象
           logModel.ObjectID = prodno;
           //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
           logModel.Element = string.Empty;

           return logModel;

       }
       #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Model.Office.SupplyChain;
using XBase.Data.Office.SupplyChain;
using XBase.Common;
using XBase.Business.Common;
using XBase.Data.Common;
using XBase.Model.Common;
namespace XBase.Business.Office.SupplyChain
{
   public class CodeReasonFeeBus
   {
       #region 原因分类|费用分类|计量单位代码
       /// <summary>
        /// 获取文档分类
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
       public static DataTable GetThreeCodeType(string CompanyCD,string SubNo, string TableName, string UsedStatus,string Name,string Flag)
        {
            if (string.IsNullOrEmpty(CompanyCD))
                return null;
            try
            {
                return CodeReasonFeeDBHelper.GetThreeCodeType(CompanyCD,SubNo,TableName, UsedStatus, Name, Flag);
            }
            catch
            {
                return null;
                throw;
            }

        }
       public static DataTable GetCodeUnitType()
       {
           try
           {
               UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
               string CompanyCD = userInfo.CompanyCD;
               return CodeReasonFeeDBHelper.GetCodeUnitType(CompanyCD);
           }
           catch
           {
               return null;
               throw;
           }
       }
        /// <summary>
        /// 插入文档信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public static bool InsertThreeCodeInfo(CodeReasonFeeModel model, string TableName)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            if (model == null)
                return false;
            //登陆日志
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.CodeName,TableName);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                succ = CodeReasonFeeDBHelper.InsertThreeCodeInfo(model, TableName);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex,TableName);
                return false;
            }

        }

        /// <summary>
        /// 修改文档种类信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public static bool UpdateThreeCodeInfo(CodeReasonFeeModel model, string TableName)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            if (model == null)
                return false;
            //登陆日志
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.CodeName,TableName);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                succ = CodeReasonFeeDBHelper.UpdateThreeCodeInfo(model, TableName);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex,TableName);
                return false;
            }

        }

        /// <summary>
        /// 根据id获取文档种类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public static CodeReasonFeeModel GetThreeCodeById(int id, string TableName)
        {
            if (id == 0)
                return null;
            try
            {
                return CodeReasonFeeDBHelper.GetThreeCodeById(id, TableName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 删除文档分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteThreeCodeType(string id, string TableName)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //string CompanyCD = "AAAAAA";
            bool isSucc = CodeReasonFeeDBHelper.DeleteThreeCodeType(id, TableName);
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
            string[] noList = id.Split(',');
            //遍历所有编号，登陆操作日志
            for (int i = 0; i < noList.Length; i++)
            {
                //获取编号
                string no = noList[i];
                //替换两边的 '
                no = no.Replace("'", string.Empty);
                string name = "";
                //操作日志
                switch (TableName)
                {
                    case "officedba.CodeReasonType":
                        name = "原因ID：";
                        break;
                    case "officedba.CodeFeeType":
                        name = "费用ID：";
                        break;
                    case "officedba.CodeUnitType":
                        name = "计量单位ID：";
                        break;
                }

                LogInfoModel logModel = InitLogInfo(name + no,TableName);
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


        #region 输出系统日志
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">异常信息</param>
        private static void WriteSystemLog(UserInfoUtil userInfo, Exception ex,string TableName)
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
            switch (TableName)
            {
                case "officedba.CodeReasonType":
                    logSys.ModuleID = ConstUtil.Menu_CodeReasonType;
                    break;
                case "officedba.CodeFeeType":
                    logSys.ModuleID = ConstUtil.Menu_CodeFeeType;
                    break;
                case "officedba.CodeUnitType":
                    logSys.ModuleID = ConstUtil.Menu_CodeUnitType;
                    break;
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
        private static LogInfoModel InitLogInfo(string prodno,string TableName)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            switch (TableName)
            {
                case "officedba.CodeReasonType":
                    logModel.ModuleID = ConstUtil.Menu_CodeReasonType;
                    logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_CodeReasonType;
                    break;
                case "officedba.CodeFeeType":
                    logModel.ModuleID = ConstUtil.Menu_CodeFeeType;
                    logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_CodeFeeType;
                    break;
                case "officedba.CodeUnitType":
                    logModel.ModuleID = ConstUtil.Menu_CodeUnitType;
                    logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_CodeUnitType;
                    break;
            }
            //设置操作日志类型 修改
            //操作对象
            logModel.ObjectID = prodno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion
        /// <summary>
        /// 校验编号的唯一性
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="codeValue">输入的编码值</param>
        /// <returns>bool 是否已经存在 true 不存在 false 存在</returns>
        public static bool CheckCodeUniq(string tableName, string CodeName, string Flag)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            //校验存在性
            return CodeReasonFeeDBHelper.CheckCodeUniq(tableName, companyCD, CodeName, Flag);
        }
         /// <summary>
       /// 判断费用表是否已经使用这个费用分类了
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
        public static bool ChargeFee(string ID)
        {
            return CodeReasonFeeDBHelper.ChargeFee(ID);
        }
    }
}

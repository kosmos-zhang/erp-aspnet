using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Model.Office.CustManager;
using XBase.Data.Office.CustManager;
using XBase.Common;
using XBase.Business.Common;
using XBase.Data.Common;
using XBase.Model.Common;
using XBase.Model.Office.SupplyChain;
namespace XBase.Business.Office.CustManager
{
    public  class TableExtFieldsBus
    {
        public static bool Add(TableExtFields tableExtFields, out string strMsg)
        {
            bool isSuc = false;
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSuc = TableExtFieldsDBHelper.Add(tableExtFields, out strMsg);
                //设置操作成功标识
                remark = "成功";
               
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULEID_CUSTINFO_ExtAttribute);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
                strMsg = "保存失败，请联系系统管理员！";
            }
            InsertLog(tableExtFields.EFDesc, ConstUtil.MODULEID_CUSTINFO_ExtAttribute, "officedba.TableExtFields", remark, "添加");
            return isSuc;
        }

        public static bool Update(TableExtFields tableExtFields, out string strMsg)
        {
            bool isSuc = false;
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSuc = TableExtFieldsDBHelper.Update(tableExtFields, out strMsg);
                //设置操作成功标识
                remark = "成功";
               
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULEID_CUSTINFO_ExtAttribute);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
                strMsg = "保存失败，请联系系统管理员！";
            }
            InsertLog(tableExtFields.EFDesc, ConstUtil.MODULEID_CUSTINFO_ExtAttribute, "officedba.TableExtFields", remark, "更新");
            return isSuc;
        }


         /// <summary>
       /// 获取列表
       /// </summary>
       /// <param name="EFDesc">描述</param>
       /// <param name="EFType">类别</param>
       /// <param name="pageIndex"></param>
       /// <param name="pageCount"></param>
       /// <param name="ord"></param>
       /// <param name="TotalCount"></param>
       /// <returns></returns>
        public static DataTable Getlist(string CompanyCD, string EFDesc, string EFType, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return TableExtFieldsDBHelper.Getlist( CompanyCD,  EFDesc,  EFType,  pageIndex,  pageCount,  ord, ref  TotalCount);
        }

         /// <summary>
        /// 初始化商品档案页面获取所有字段
        /// </summary>
        /// <param name="EFDesc">描述</param>
        /// <param name="EFType">类别</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetAllList(string CompanyCD)
        {
            return TableExtFieldsDBHelper.GetAllList(CompanyCD);
        }
        /// <summary>
        /// 打印需要
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetAllListReport(string CompanyCD)
        {
            return TableExtFieldsDBHelper.GetAllListReport(CompanyCD);
        }

         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strCompanyCD"></param>
        /// <param name="IDS">id列表</param>
        /// <param name="strMsg"></param>
        /// <param name="strFieldText"></param>
        /// <returns></returns>
        public static bool Delete(string strCompanyCD, string IDS, out string strMsg, out string strFieldText)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            strFieldText = "";
            strMsg = "";
            try
            {
                isSucc = TableExtFieldsDBHelper.Delete(strCompanyCD, IDS, out strMsg, out strFieldText);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULEID_CUSTINFO_ExtAttribute);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
                strMsg = "删除失败，请联系系统管理员！";
            }
            string[] orderNoS = null;
            orderNoS = IDS.Split(',');

            for (int i = 0; i < orderNoS.Length; i++)
            {
                InsertLog(orderNoS[i], ConstUtil.MODULEID_CUSTINFO_ExtAttribute, "officedba.TableExtFields", remark, "删除");
            }


            return isSucc;
        }

        #region
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">登陆用户信息</param>
        /// <param name="ex">异常信息</param>
        /// <param name="Type">日志类型</param>
        /// <param name="SystemKind">系统日志类型</param>
        /// <param name="ModuleID">模块ID</param>
        public static void WriteSystemLog(Exception ex, LogInfo.LogType Type, LogInfo.SystemLogKind SystemKind, string ModuleID)
        {
            /* 
             * 出现异常时，输出系统日志到文本文件 
             * 考虑出现异常情况比较少，尽管一个方法可能多次异常，
             *      但还是考虑将异常日志的变量定义放在catch里面
             */
            //定义变量
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            LogInfo logSys = new LogInfo();
            //设置日志类型 需要指定为系统日志
            logSys.Type = Type;
            //指定系统日志类型 出错信息
            logSys.SystemKind = SystemKind;
            //指定登陆用户信息
            logSys.UserInfo = userInfo;
            //设定模块ID
            logSys.ModuleID = ModuleID;
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
        /// <param name="ApplyNo">单据编号</param>
        /// <param name="ModuleID">模块ID</param>
        /// <param name="ObjectName">操作对象(相关表)</param>
        /// <param name="remark">备注,设置操作成功标识(操作成功或失败)</param>
        /// <param name="Element">涉及关键元素（操作名称） 这个需要根据每个页面具体设置</param>
        /// <returns></returns>
        public static void InsertLog(string OrderNo, string ModuleID, string ObjectName, string remark, string Element)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ModuleID;
            //设置操作日志类型 修改
            logModel.ObjectName = ObjectName;
            //操作对象
            logModel.ObjectID = OrderNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = Element;
            //设置操作成功标识
            logModel.Remark = remark;


            //插入日志
            LogDBHelper.InsertLog(logModel);
        }
        #endregion
    }
}

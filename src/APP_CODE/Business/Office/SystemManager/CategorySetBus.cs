using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Office.SystemManager;
using XBase.Model.Office.SystemManager;
using XBase.Common;
using XBase.Business.Common;
using XBase.Data.Common;
using XBase.Model.Common;
namespace XBase.Business.Office.SystemManager
{
   public class CategorySetBus
   {
       #region 设备分类
       public static DataTable GetCodeEquipmentType(string CompanyCD,string flag)
       {
           if (string.IsNullOrEmpty(CompanyCD))
               return null;
           try 
           {
               return CategorySetDBHelper.GetCodeEquipmentType(CompanyCD,flag);
           }
           catch (Exception ex)
           {
               throw ex;
           }
 
       }
       /// <summary>
       /// 插入设备信息
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static int InsertCodeEquipmentInfo(CodeEquipmentTypeModel model)
       {

           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //登陆日志
           try
           {
               int succ = 0;
               LogInfoModel logModel = InitLogInfo(model.CodeName);
               logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
               //设置模块ID 模块ID请在ConstUtil中定义，以便维护
               logModel.ModuleID = ConstUtil.Menu_Equipment;
               //设置操作日志类型 修改
               logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_Equipment;
               //操作对象
               succ = CategorySetDBHelper.InsertCodeEquipmentInfo(model);
               if (succ<1)
                   logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
               else
                   logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
               LogDBHelper.InsertLog(logModel);
               return succ;
           }
           catch (Exception ex)
           {
               WriteSystemLog(userInfo, ex);
               return 0;
           }
       }
       /// <summary>
       /// 根据id获取设备种类
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public static CodeEquipmentTypeModel GetCodeEuipment(int id)
       {
           if (id == 0)
               return null;
           try
           {
               return CategorySetDBHelper.GetCodeEuipment(id);
           }
           catch ( Exception  ex)
           {
               
               throw ex;
           }
       }
       /// <summary>
       /// 修改设备种类信息
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static int UpdateCodeEquipmentInfo(CodeEquipmentTypeModel model)
       {


           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //登陆日志
           try
           {
               int succ = 0;
               LogInfoModel logModel = InitLogInfo(model.CodeName);
               logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
               logModel.ModuleID = ConstUtil.Menu_Equipment;
               //设置操作日志类型 修改
               logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_Equipment;
               succ = CategorySetDBHelper.UpdateCodeEquipmentInfo(model);
               if (succ < 1)
                   logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
               else
                   logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
               LogDBHelper.InsertLog(logModel);
               return succ;
           }
           catch (Exception)
           {
               return 0;
               throw;
           }
       }

       public static int DeleteCodeEquipmentInfo(int id)
       {
           if (id == 0)
               return 0;
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           string CompanyCD = userInfo.CompanyCD;
           //string CompanyCD = "AAAAAA";
           int isSucc = CategorySetDBHelper.DeleteCodeEquipmentInfo(id);
           //定义变量
           string remark;
           //成功时
           if (isSucc>0)
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
               //操作日志
               LogInfoModel logModel = InitLogInfo(Convert.ToString(id));
               //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
               logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
               logModel.ModuleID = ConstUtil.Menu_Equipment;
               logModel.ObjectID = "设备分类ID："+id;

               //设置操作日志类型 修改
               logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_Equipment;
               //设置操作成功标识
               logModel.Remark = remark;
               //登陆日志
               LogDBHelper.InsertLog(logModel);
           return isSucc;

       }
       #endregion

       #region 文档分类

       /// <summary>
       /// 获取文档分类
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable GetCodeDocType(string CompanyCD)
       {

           if (string.IsNullOrEmpty(CompanyCD))
               return null;
           try
           {
               return CategorySetDBHelper.GetCodeDocType(CompanyCD);
           }
           catch
           {
               return null;
               throw;
           }
       }

       #region 获取文档类型的方法 zhangyy
       /// <summary>
       /// 获取文档类型的方法
       /// </summary>
       /// <param name="CompanyCD">公司代码</param>
       /// <returns>返回所有类型及类型ID</returns>
       public static DataTable GetDocType(string CompanyCD)
       {
           return CategorySetDBHelper.GetDocType(CompanyCD);
       }
       #endregion
       /// <summary>
       /// 插入文档信息
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static int InsertCodeDocTypeInfo(CodeEquipmentTypeModel model)
       {

           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //登陆日志
           try
           {
               int succ = 0;
               LogInfoModel logModel = InitLogInfo(model.CodeName);
               logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
               logModel.ModuleID = ConstUtil.Menu_DocTypeList;
               //设置操作日志类型 修改
               logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_DocType;
               //操作对象
               succ = CategorySetDBHelper.InsertCodeDocTypeInfo(model);
               if (succ < 1)
                   logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
               else
                   logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
               LogDBHelper.InsertLog(logModel);
               return succ;
           }
           catch (Exception ex)
           {
               WriteSystemLog(userInfo, ex);
               return 0;
           }
       
       }

       /// <summary>
       /// 修改文档种类信息
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static int UpdateDocTypeInfo(CodeEquipmentTypeModel model)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //登陆日志
           try
           {
               int succ = 0;
               LogInfoModel logModel = InitLogInfo(model.CodeName);
               logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
               logModel.ModuleID = ConstUtil.Menu_DocTypeList;
               //设置操作日志类型 修改
               logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_DocType;
               succ = CategorySetDBHelper.UpdateCodeDocTypeInfo(model);
               if (succ < 1)
                   logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
               else
                   logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
               LogDBHelper.InsertLog(logModel);
               return succ;
           }
           catch (Exception)
           {
               return 0;
               throw;
           }

       }

       /// <summary>
       /// 根据id获取文档种类
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public static CodeEquipmentTypeModel GetCodeDocbyId(int id)
       {
           if (id == 0)
               return null;
           try
           {
               return CategorySetDBHelper.GetodeDocType(id);
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
       public static int DeleteCodeDocType(int id)
       {

           if (id == 0)
               return 0;
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           string CompanyCD = userInfo.CompanyCD;
           //string CompanyCD = "AAAAAA";
           int isSucc =  CategorySetDBHelper.DeleteCodeDocType(id);
           //定义变量
           string remark;
           //成功时
           if (isSucc > 0)
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
           //操作日志
           LogInfoModel logModel = InitLogInfo(Convert.ToString(id));
           //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
           logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
           logModel.ModuleID = ConstUtil.Menu_DocTypeList;
           logModel.ObjectID = "文档分类ID：" + id;

           //设置操作日志类型 修改
           logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_DocType;
           //设置操作成功标识
           logModel.Remark = remark;
           LogDBHelper.InsertLog(logModel);
           return isSucc;

       }
       #endregion



       #region 往来单位|物品分类
       /// <summary>
       /// 获取文档分类
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable GetCodeBigFlagType(string CompanyCD,string TableName)
       {
           if (string.IsNullOrEmpty(CompanyCD))
               return null;
           try
           {
               return CategorySetDBHelper.GetCodeBigType(CompanyCD,TableName );
           }
           catch
           {
               return null;
               throw;
           }
       }
       public static DataTable GetProductType()
       {
           try
           {
               string CompanyCD = "";
               try
               {
                   UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                    CompanyCD = userInfo.CompanyCD;
               }
               catch
               {
                    CompanyCD = "AAAAAA";
               }
               return CategorySetDBHelper.GetProductType(CompanyCD);
           }
           catch
           {
               return null;
               throw;
           }
       }
       public static string GetProductTypeInfo(int ID, string TypeFlag )
       {
           try
           {
               return CategorySetDBHelper.GetProductTypeInfo(ID,TypeFlag);
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
       public static int InsertCodeBigFlagInfo(CodeEquipmentTypeModel model,string TableName)
       {

           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //登陆日志
           try
           {
               int succ = 0;
               LogInfoModel logModel = InitLogInfo(model.CodeName);
               logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
               if (TableName == "officedba.CodeCompanyType")
               {
                   logModel.ModuleID = ConstUtil.Menu_CompanyType;
                   logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_CompanyType;
               }
               else if (TableName == "officedba.CodeProductType")
               {
                   logModel.ModuleID = ConstUtil.Menu_ProductType;
                   logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_ProductType;
               }
               succ = CategorySetDBHelper.InsertCodeBigTypeInfo(model, TableName);
               if (succ < 1)
                   logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
               else
                   logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
               LogDBHelper.InsertLog(logModel);
               return succ;
           }
           catch (Exception ex)
           {
               WriteSystemLog(userInfo, ex);
               return 0;
           }




           //if (model == null)
           //    return 0;
           //try
           //{
           //    return CategorySetDBHelper.InsertCodeBigTypeInfo(model,TableName);
           //}
           //catch (Exception ex)
           //{
           //    throw ex;
           //}
       }

       /// <summary>
       /// 修改文档种类信息
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static int UpdateCodeBigTypeInfo(CodeEquipmentTypeModel model,string TableName)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //登陆日志
           try
           {
               int succ = 0;
               LogInfoModel logModel = InitLogInfo(model.CodeName);
               logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
               if (TableName == "officedba.CodeCompanyType")
               {
                   logModel.ModuleID = ConstUtil.Menu_CompanyType;
                   logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_CompanyType;
               }
               else if (TableName == "officedba.CodeProductType")
               {
                   logModel.ModuleID = ConstUtil.Menu_ProductType;
                   logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_ProductType;
               }
               succ = CategorySetDBHelper.UpdateCodeBigTypeInfo(model, TableName);
               if (succ < 1)
                   logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
               else
                   logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
               LogDBHelper.InsertLog(logModel);
               return succ;
           }
           catch (Exception)
           {
               return 0;
               throw;
           }


           //if (model == null)
           //    return 0;
           //try
           //{
           //    return CategorySetDBHelper.UpdateCodeBigTypeInfo(model,TableName);
           //}
           //catch (Exception ex)
           //{
           //    throw;
           //}

       }

       /// <summary>
       /// 根据id获取文档种类
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public static CodeEquipmentTypeModel GetCodeBigTypebyId(int id,string TableName)
       {
           if (id == 0)
               return null;
           try
           {
               return CategorySetDBHelper.GetCodeBigTypeById(id,TableName);
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
       public static int DeleteCodeBigType(int id,string TableName)
       {

           if (id == 0)
               return 0;
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           string CompanyCD = userInfo.CompanyCD;
           //string CompanyCD = "AAAAAA";
           int isSucc = CategorySetDBHelper.DeleteCodeBigType(id, TableName);
           //定义变量
           string remark;
           //成功时
           if (isSucc > 0)
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
           //操作日志
           LogInfoModel logModel = InitLogInfo(Convert.ToString(id));
           //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
           logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
           if (TableName == "officedba.CodeCompanyType")
           {
               logModel.ModuleID = ConstUtil.Menu_CompanyType;
               logModel.ObjectID = "往来单位ID：" + id;
               logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_CompanyType;
           }
           else if (TableName == "officedba.CodeProductType")
           {
               logModel.ModuleID = ConstUtil.Menu_ProductType;
               logModel.ObjectID = "物品分类ID：" + id;
               logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_ProductType;
           }
           //设置操作日志类型 修改
          
           //设置操作成功标识
           logModel.Remark = remark;
           //登陆日志
           LogDBHelper.InsertLog(logModel);
           return isSucc;

           //if (id == 0)
           //    return 0;
           //try
           //{
           //    return CategorySetDBHelper.DeleteCodeBigType(id,TableName);
           //}
           //catch (Exception ex)
           //{
           //    throw;
           //}

       }
       #endregion
       public static DataTable SearchDeptInfo(string companyCD, string deptID,string TableName)
       {
           if (string.IsNullOrEmpty(TableName))
               return null;
           DataTable dt = null;
           try
           {
               if (TableName == "officedba.CodeCompanyType")
               {
                   dt= CategorySetDBHelper.SearchCodeCompanyInfo(companyCD, deptID);
               }
               else if (TableName == "officedba.CodeProductType")
               {
                   dt= CategorySetDBHelper.SearchDeptInfo(companyCD, deptID);
               }
               return dt;
           }
           catch
           {
               return null;
               throw;
           }
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
           logSys.ModuleID = ConstUtil.Menu_OtherCorpInfo;
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
           //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
           logModel.Element = string.Empty;
           logModel.ObjectID = prodno;
           return logModel;

       }
       #endregion
   }
}

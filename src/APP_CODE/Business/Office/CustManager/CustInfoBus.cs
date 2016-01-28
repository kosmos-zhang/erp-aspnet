using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Office.CustManager;
using XBase.Model.Office.CustManager;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Common;
using XBase.Data.Common;
using System.Collections;

namespace XBase.Business.Office.CustManager
{
    public class CustInfoBus
    {
        #region 获取客户分类的方法
        /// <summary>
        /// 获取客户分类的方法
        /// </summary>
        /// <param name="CompanyCD">公司编号</param>
        /// <returns>客户分类结果集</returns>
        public static DataTable GetCustClass()
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string CompanyCD = userInfo.CompanyCD;
            try
            {
                return CustInfoDBHelper.GetCustClass(CompanyCD);
            }
            catch
            {
                return null;
                //throw ex;
            }
        }
        #endregion
        #region 获取扩展属性
        public static DataTable GetExtAttrValue(string strKey, string CustNo, string CompanyCD)
        {
            return CustInfoDBHelper.GetExtAttrValue(strKey, CustNo, CompanyCD);
        }
        /// <summary>
        /// 打印需要
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="CustNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetExtAttrValueReport(string strKey, string CustNo, string CompanyCD)
        {
            return CustInfoDBHelper.GetExtAttrValueReport(strKey, CustNo, CompanyCD);
        }
        #endregion
        #region 获取币种的方法
        /// <summary>
        /// 获取币种的方法
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>币种集合</returns>
        public static DataTable GetCustCurrencyType(string CompanyCD)
        {
            try
            {
                return CustInfoDBHelper.GetCustCurrencyType(CompanyCD);
            }
            catch
            {
                return null;
                //throw ex;
            }
        }
        #endregion

        #region 增加客户信息的方法
        /// <summary>
        /// 增加客户信息的方法
        /// </summary>
        /// <param name="CustModel">客户信息Model</param>
        /// <returns>bool值</returns>
        public static bool CustInfoAdd(CustInfoModel CustModel,LinkManModel LinkManM, Hashtable htExtAttr)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            LogInfoModel logModel = new LogInfoModel(); //操作日志
            bool isSucc = false;//定义返回变量

            #region 设置操作日志内容
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_INFO_ADD;
            //操作单据编号 客户编号
            logModel.ObjectID = CustModel.CustNo;            
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_CUST;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_INSERT;
            #endregion

            try
            {
                //if (CustModel.CustBig == "2")//会员客户时，添加到2个表中
                //{

                //}
                //else
                //{
                isSucc = CustInfoDBHelper.CustInfoAdd(CustModel,LinkManM, htExtAttr);
                //}
                
            }
            catch (System.Exception ex)
            {
                #region  操作失败时记录日志到文件
                //定义变量
                LogInfo logSys = new LogInfo();
                //设置日志类型 需要指定为系统日志
                logSys.Type = LogInfo.LogType.SYSTEM;
                //指定系统日志类型 出错信息
                logSys.SystemKind = LogInfo.SystemLogKind.SYSTEM_ERROR;
                //指定登陆用户信息
                logSys.UserInfo = userInfo;
                //设定模块ID
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_INFO_ADD;
                //描述
                logSys.Description = ex.ToString();
                //输出日志
                LogUtil.WriteLog(logSys);
                #endregion
            }

            if (isSucc)//操作成功
            {
                logModel.Remark += "成功";
            }
            else//操作失败
            {
                logModel.Remark += "失败";
            }

            //记录日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;
        }
        #endregion

        #region 同时添加客户信息及联系人列表信息的方法
        /// <summary>
        /// 同时添加客户信息及联系人列表信息的方法
        /// </summary>
        /// <param name="CustInfoModel">客户信息Model</param>
        /// <param name="LinkManlist">联系人列表信息流</param>
        /// <returns>bool值</returns>
        public static bool AddCustAndLinkMan(CustInfoModel CustInfoModel, string LinkManlist)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            LogInfoModel logModel = new LogInfoModel(); //操作日志
            bool isSucc = false;//定义返回变量

            #region 设置操作日志内容
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_INFO_ADD;
            //操作单据编号 客户编号
            logModel.ObjectID = CustInfoModel.CustNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_CUST +";"+ ConstUtil.TABLE_NAME_LISIMAN;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_INSERT;
            #endregion

            try
            {
                isSucc = CustInfoDBHelper.AddCustAndLinkMan(CustInfoModel, LinkManlist);
            }
            catch (System.Exception ex)
            {
                #region  操作失败时记录日志到文件
                //定义变量
                LogInfo logSys = new LogInfo();
                //设置日志类型 需要指定为系统日志
                logSys.Type = LogInfo.LogType.SYSTEM;
                //指定系统日志类型 出错信息
                logSys.SystemKind = LogInfo.SystemLogKind.SYSTEM_ERROR;
                //指定登陆用户信息
                logSys.UserInfo = userInfo;
                //设定模块ID
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_INFO_ADD;
                //描述
                logSys.Description = ex.ToString();
                //输出日志
                LogUtil.WriteLog(logSys);
                #endregion
            }

            if (isSucc)//操作成功
            {
                logModel.Remark += "成功";
            }
            else//操作失败
            {
                logModel.Remark += "失败";
            }
            //记录日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;
        }
        #endregion

        #region 同时修改客户信息及联系人列表信息的方法
        /// <summary>
        /// 同时修改客户信息及联系人列表信息的方法
        /// </summary>
        /// <param name="CustInfoModel">客户信息Model</param>
        /// <param name="LinkManlist">联系人列表信息流</param>
        /// <returns>bool值</returns>
        public static bool UpdateCustAndLinkMan(CustInfoModel CustInfoModel, string LinkManlist)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            LogInfoModel logModel = new LogInfoModel(); //操作日志
            bool isSucc = false;//定义返回变量

            #region 设置操作日志内容
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_INFO_LIST;
            //操作单据编号 客户编号
            logModel.ObjectID = CustInfoModel.CustNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_CUST + ";" + ConstUtil.TABLE_NAME_LISIMAN;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_UPDATE;
            #endregion

            try
            {
                isSucc = CustInfoDBHelper.UpdateCustAndLinkMan(CustInfoModel, LinkManlist);
            }
            catch (System.Exception ex)
            {
                #region  操作失败时记录日志到文件
                //定义变量
                LogInfo logSys = new LogInfo();
                //设置日志类型 需要指定为系统日志
                logSys.Type = LogInfo.LogType.SYSTEM;
                //指定系统日志类型 出错信息
                logSys.SystemKind = LogInfo.SystemLogKind.SYSTEM_ERROR;
                //指定登陆用户信息
                logSys.UserInfo = userInfo;
                //设定模块ID
                logSys.ModuleID = ConstUtil.MODULE_ID_CUST_INFO_LIST;
                //描述
                logSys.Description = ex.ToString();
                //输出日志
                LogUtil.WriteLog(logSys);
                #endregion
            }

            if (isSucc)//操作成功
            {
                logModel.Remark += "成功";
            }
            else//操作失败
            {
                logModel.Remark += "失败";
            }
            //记录日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;
        }
        #endregion

        #region 根据查询条件获取客户列表信息的方法
        /// <summary>
        /// 根据查询条件获取客户列表信息的方法
        /// </summary>
        /// <param name="CustModel">查询条件</param>
        /// <returns>客户列表结果结</returns>
        public static DataTable GetCustInfoBycondition(CustInfoModel CustModel, string Manager,string CanUserID, string CreatedBegin, string CreatedEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return CustInfoDBHelper.GetCustInfoBycondition(CustModel, Manager,CanUserID, CreatedBegin, CreatedEnd,pageIndex,pageCount,ord,ref TotalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据创建人获取客户ID、编号、简称的方法
        /// <summary>
        /// 根据创建人获取客户ID、编号、简称的方法
        /// </summary>
        /// <param name="Creator">创建人</param>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>客户列表结果集</returns>
        public static DataTable GetCustName(CustInfoModel CustModel,string CanUserID, string CompanyCD)
        {
            try
            {
                return CustInfoDBHelper.GetCustName(CustModel,CanUserID, CompanyCD);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据传的一批ID批量删除ID对应信息
        /// <summary>
        /// 根据传的一批ID批量删除ID对应信息
        /// </summary>
        /// <param name="CustID">客户信息ID</param>
        /// <returns>返回影响行数</returns>
        public static int DelCustInfo(string[] CustID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            LogInfoModel logModel = new LogInfoModel(); //操作日志
            int iSucc = 0;//定义返回变量

            #region 设置操作日志内容
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_INFO_LIST;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
             for (int i = 0; i < CustID.Length; i++)
            {
                //CustID[i] = "'" + CustID[i] + "'";
                sb.Append(CustID[i] + ";");
            }

            //操作单据编号 客户编号
             logModel.ObjectID = "客户ID:" + sb.ToString();

            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_CUST;//暂未操作到其他表 +";" + ConstUtil.TABLE_NAME_LISIMAN;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = "删除客户信息，自动删除与客户对应的联系人、联络、服务...";//string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_DELETE;
            #endregion

            try
            {
                iSucc = CustInfoDBHelper.DelCustInfo(CustID);
            }
            catch (System.Exception ex)
            {
                throw ex;
                //#region  操作失败时记录日志到文件
                ////定义变量
                //LogInfo logSys = new LogInfo();
                ////设置日志类型 需要指定为系统日志
                //logSys.Type = LogInfo.LogType.SYSTEM;
                ////指定系统日志类型 出错信息
                //logSys.SystemKind = LogInfo.SystemLogKind.SYSTEM_ERROR;
                ////指定登陆用户信息
                //logSys.UserInfo = userInfo;
                ////设定模块ID
                //logSys.ModuleID = ConstUtil.MODULE_ID_CUST_INFO_LIST;
                ////描述
                //logSys.Description = ex.ToString();
                ////输出日志
                //LogUtil.WriteLog(logSys);
                //#endregion
            }
            if (iSucc > 0)//操作成功
            {
                logModel.Remark += "成功";
            }
            else//操作失败
            {
                logModel.Remark += "失败";
            }
            //记录日志
            LogDBHelper.InsertLog(logModel);

            return iSucc;
        }
        #endregion

        #region 根据客户ID获取客户详细信息的方法
        /// <summary>
        /// 根据客户ID获取客户详细信息的方法
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="custid">客户ID</param>
        /// <returns></returns>
        public static DataTable GetCustInfoByID(string CompanyCD, int custid, string CustBig, string CustNO)
        {
            try
            {
                return CustInfoDBHelper.GetCustInfoByID(CompanyCD, custid, CustBig, CustNO);
                //DataTable dt = CustInfoDBHelper.GetCustInfoByID(CompanyCD, custid);

                //DataColumn SellName = new DataColumn();
                //dt.Columns.Add("SellName");

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string LinkerIds = dt.Rows[i]["Manager"].ToString();
                //    LinkerIds = "User_" + LinkerIds;
                                       
                //    dt.Rows[i]["SellName"] = LinkerIds;
                //}
                //return dt;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据客户编号修改客户信息的方法
        /// <summary>
        /// 根据客户编号修改客户信息的方法
        /// </summary>
        /// <param name="CustInfoModel">客户信息Model</param>
        /// <returns>bool值</returns>
        public static bool UpdateCustInfo(CustInfoModel CustInfoModel, LinkManModel LinkManM, Hashtable ht)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            LogInfoModel logModel = new LogInfoModel(); //操作日志
            bool isSucc = false;//定义返回变量

            #region 设置操作日志内容
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_CUST_INFO_LIST;
            //操作单据编号 客户编号
            logModel.ObjectID = CustInfoModel.CustNo;
            //操作对象 操作的表信息
            logModel.ObjectName = ConstUtil.TABLE_NAME_CUST;
            //涉及关键元素 涉及其他业务、表关系
            logModel.Element = string.Empty;
            //备注 操作类型
            logModel.Remark = ConstUtil.LOG_PROCESS_UPDATE;
            #endregion

            try
            {
                isSucc = CustInfoDBHelper.UpdateCustInfo(CustInfoModel,LinkManM, ht);
            }
            catch (System.Exception ex)
            {
                throw ex;

                //#region  操作失败时记录日志到文件
                ////定义变量
                //LogInfo logSys = new LogInfo();
                ////设置日志类型 需要指定为系统日志
                //logSys.Type = LogInfo.LogType.SYSTEM;
                ////指定系统日志类型 出错信息
                //logSys.SystemKind = LogInfo.SystemLogKind.SYSTEM_ERROR;
                ////指定登陆用户信息
                //logSys.UserInfo = userInfo;
                ////设定模块ID
                //logSys.ModuleID = ConstUtil.MODULE_ID_CUST_INFO_LIST;
                ////描述
                //logSys.Description = ex.ToString();
                ////输出日志
                //LogUtil.WriteLog(logSys);
                //#endregion
            }
            
            if (isSucc)//操作成功
            {
                logModel.Remark += "成功";
            }
            else//操作失败
            {
                logModel.Remark += "失败";
            }
            //记录日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;
        }
        #endregion

        #region 获取在职员工姓名和ID
        /// <summary>
        /// 获取在职员工姓名和ID
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>员工ID、姓名结果集合</returns>
        public static DataTable GetCustManager(string CompanyCD)
        {
            try
            {
                return CustInfoDBHelper.GetCustManager(CompanyCD);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取客户信息 added by jiangym
        public static DataTable GetCustInfo()
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return CustInfoDBHelper.GetCustInfo(CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据客户编号获取联系人信息条数
        /// <summary>
        /// 根据客户编号获取联系人信息条数
        /// </summary>
        /// <param name="CustNo"></param>
        /// <returns></returns>
        public static bool GetLinkManByCustNo(string[] CustNos,string[] CustIDs)
        {
            return CustInfoDBHelper.GetLinkManByCustNo(CustNos, CustIDs);
        }
        #endregion

        #region 根据客户ID获取客户中关于联系人信息
        /// <summary>
        /// 根据客户ID获取客户中关于联系人信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="custid">客户ID</param>
        /// <returns></returns>
        public static DataTable GetCustLinkByID(string CompanyCD, string LinkName, string CustNo)
        {
            return CustInfoDBHelper.GetCustLinkByID(CompanyCD, LinkName, CustNo);
        }
        #endregion

        #region 客户一览表查询
        /// <summary>
        /// 客户一览表查询
        /// </summary>
        /// <param name="CustModel"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetCustListByCondition(CustInfoModel CustModel, string CanUserID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustInfoDBHelper.GetCustListByCondition(CustModel,CanUserID, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion 

        #region 客户一览表查询打印
        /// <summary>
        /// 客户一览表查询打印
        /// </summary>
        /// <param name="CustModel"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetCustListByConditionPrint(CustInfoModel CustModel, string ord)
        {
            return CustInfoDBHelper.GetCustListByConditionPrint(CustModel, ord);
        }
        #endregion

        #region 客户管理分类统计
        /// <summary>
        /// 按客户分类统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetCustListByTypeManage(string CompanyCD, string ord)
        {
            return CustInfoDBHelper.GetCustListByTypeManage(CompanyCD,ord);
        }
        #endregion

        #region 根据客户类型id、客户分类类型等条件检索客户列表信息
        public static DataTable GetCustListByTypeManageNewList(string CompanyCD,string CustSeleTypeID, string CustSeleType, string CustType, string CustClass, string Area,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return CustInfoDBHelper.GetCustListByTypeManageNewList(CompanyCD, CustSeleTypeID, CustSeleType, CustType, CustClass, Area,
                    RelaGrade, CreditGrade, BeginDate, EndDate, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        //根据客户类型id、客户分类类型等条件检索客户列表信息--导出用
        public static DataTable GetCustListByTypeManageNewList(string CompanyCD, string CustSeleTypeID, string CustSeleType, string CustType, string CustClass, string Area,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            try
            {
                return CustInfoDBHelper.GetCustListByTypeManageNewList(CompanyCD, CustSeleTypeID, CustSeleType, CustType, CustClass, Area,
                    RelaGrade, CreditGrade, BeginDate, EndDate);
            }
            catch 
            {
                return null;
            } 
        }
        public static DataTable GetCustListByTypeManageNewDetail(string CompanyCD, string CustSeleType, string CustType, string CustClass, string Area,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {

            return CustInfoDBHelper.GetCustListByTypeManageNewDetail(CompanyCD, CustSeleType, CustType, CustClass, Area,
                    RelaGrade, CreditGrade, BeginDate, EndDate); 
        }

        #region 按客户营销分类统计
        /// <summary>
        /// 按客户营销分类统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetCustListByTypeSell(string CompanyCD, string ord)
        {
            return CustInfoDBHelper.GetCustListByTypeSell(CompanyCD,ord);
        }
        #endregion

        #region 按客户时间分类统计
        /// <summary>
        /// 按客户时间分类统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetCustListByTypeTime(string CompanyCD, string ord)
        {
            return CustInfoDBHelper.GetCustListByTypeTime(CompanyCD,ord);
        }
        #endregion

        public static DataTable GetCustNumBuSeleCustType(string CompanyCD, string CustSelType, string CustType, string CustClass, string Area, string RelaGrade, string CreditGrade, string DateBegin, string DateEnd)
        {
            DataTable dt = null;
            try
            {
                switch (CustSelType)
                {
                    case "CustTypeManage"://客户管理分类
                        dt = CustInfoDBHelper.GetCustListByTypeManageNew(CompanyCD, CustType, CustClass, Area, RelaGrade, CreditGrade, DateBegin, DateEnd);
                        break;
                    case "CustTypeSell"://客户营销分类
                        dt = CustInfoDBHelper.GetCustListByTypeSellNew(CompanyCD, CustType, CustClass, Area, RelaGrade, CreditGrade, DateBegin, DateEnd);
                        break;
                    case "CustTypeTime"://客户时间分类
                        dt = CustInfoDBHelper.GetCustListByTypeTimeNew(CompanyCD, CustType, CustClass, Area, RelaGrade, CreditGrade, DateBegin, DateEnd);
                        break;
                    default://客户管理分类
                        dt = CustInfoDBHelper.GetCustListByTypeManageNew(CompanyCD, CustType, CustClass, Area, RelaGrade, CreditGrade, DateBegin, DateEnd);
                        break;
                }
            }
            catch
            {
                return null;
            }

            return dt;
        }

        #region 按客户类别分类统计
        /// <summary>
        /// 按客户类别分类统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetCustListByType(string CompanyCD, string ord)
        {
            return CustInfoDBHelper.GetCustListByType(CompanyCD, ord);
        }

        public static DataTable GetCustListByTypeNew(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustClass, string Area, string RelaGrade, string CreditGrade, string DateBegin, string DateEnd)
        {
            return CustInfoDBHelper.GetCustListByTypeNew(CompanyCD, CustTypeManage, CustTypeSell, CustTypeTime, CustClass, Area, RelaGrade, CreditGrade, DateBegin, DateEnd);
        }

        //根据客户类型id、客户分类类型等条件检索客户列表信息
        public static DataTable GetCustListByTypeNewList(string CompanyCD, string CustTypeID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustClass, string Area,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustInfoDBHelper.GetCustListByTypeNewList(CompanyCD, CustTypeID, CustTypeManage, CustTypeSell, CustTypeTime, CustClass, Area, RelaGrade, CreditGrade,
                BeginDate, EndDate, pageIndex, pageCount, ord, ref TotalCount);
        }

        //根据客户类型id、客户分类类型等条件检索客户列表信息 导出用
        public static DataTable GetCustListByTypeNewList(string CompanyCD, string CustTypeID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustClass, string Area,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            return CustInfoDBHelper.GetCustListByTypeNewList(CompanyCD, CustTypeID, CustTypeManage, CustTypeSell, CustTypeTime, CustClass, Area, RelaGrade, CreditGrade, BeginDate, EndDate);
        }

        public static DataTable GetCustListByTypeNewDetail(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustClass, string Area,
           string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            return CustInfoDBHelper.GetCustListByTypeNewDetail(CompanyCD,  CustTypeManage, CustTypeSell, CustTypeTime, CustClass, Area, RelaGrade, CreditGrade, BeginDate, EndDate);
        }
        #endregion

        #region 按客户细分统计
        /// <summary>
        /// 按客户细分统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetCustListByClass(string CompanyCD, string ord)
        {
            return CustInfoDBHelper.GetCustListByClass(CompanyCD, ord);
        }

        public static DataTable GetCustListByClassNew(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string Area, string RelaGrade, string CreditGrade, string DateBegin, string DateEnd)
        {
            return CustInfoDBHelper.GetCustListByClassNew(CompanyCD, CustTypeManage, CustTypeSell, CustTypeTime, CustType, Area, RelaGrade, CreditGrade, DateBegin, DateEnd);
        }

        //根据客户类型id、客户分类类型等条件检索客户列表信息
        public static DataTable GetCustListByClassNewList(string CompanyCD, string CustTypeID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string Area,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustInfoDBHelper.GetCustListByClassNewList(CompanyCD, CustTypeID, CustTypeManage, CustTypeSell, CustTypeTime, CustType, Area, RelaGrade, CreditGrade,
                 BeginDate, EndDate, pageIndex, pageCount, ord, ref  TotalCount);
        }

         //根据客户类型id、客户分类类型等条件检索客户列表信息 导出用
        public static DataTable GetCustListByClassNewList(string CompanyCD, string CustClassID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string Area,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            return CustInfoDBHelper.GetCustListByClassNewList(CompanyCD,  CustClassID,  CustTypeManage,  CustTypeSell, CustTypeTime, CustType,  Area,
             RelaGrade, CreditGrade, BeginDate, EndDate);
        }

        public static DataTable GetCustListByClassNewDetail(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string Area,
           string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            return CustInfoDBHelper.GetCustListByClassNewDetail(CompanyCD, CustTypeManage, CustTypeSell, CustTypeTime, CustType, Area,
             RelaGrade, CreditGrade, BeginDate, EndDate);
        }
        #endregion

        #region 按客户所在区域统计
        /// <summary>
        /// 按客户所在区域统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetCustListByArea(string CompanyCD, string ord)
        {
            return CustInfoDBHelper.GetCustListByArea(CompanyCD, ord);
        }
        //按客户所在区域统计
        public static DataTable GetCustListByAreaNew(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass, string RelaGrade, string CreditGrade, string DateBegin, string DateEnd)
        {
            return CustInfoDBHelper.GetCustListByAreaNew(CompanyCD, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass, RelaGrade, CreditGrade, DateBegin, DateEnd);
        }
        //按客户所在区域统计后列表
        public static DataTable GetCustListByAreaNewList(string CompanyCD, string AreaID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustInfoDBHelper.GetCustListByAreaNewList(CompanyCD, AreaID, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass,
                                                             RelaGrade, CreditGrade, BeginDate, EndDate, pageIndex, pageCount, ord, ref TotalCount);
        }

        //按客户所在区域统计后列表 导出用
        public static DataTable GetCustListByAreaNewList(string CompanyCD, string AreaID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
            string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            return CustInfoDBHelper.GetCustListByAreaNewList(CompanyCD, AreaID, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass,
                                                             RelaGrade, CreditGrade, BeginDate, EndDate);
        }

        public static DataTable GetCustListByAreaNewDetail(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
           string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            return CustInfoDBHelper.GetCustListByAreaNewDetail(CompanyCD, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass,
                                                             RelaGrade, CreditGrade, BeginDate, EndDate);
        }

        #endregion

        #region 按客户关系等级统计
        /// <summary>
        /// 按客户关系等级统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetCustListByRelaGrade(string CompanyCD, string ord)
        {
            return CustInfoDBHelper.GetCustListByRelaGrade(CompanyCD, ord);
        }

        //按客户关系等级统计
        public static DataTable GetCustListByRelaGradeNew(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass, string Area, string CreditGrade, string DateBegin, string DateEnd)
        {
            return CustInfoDBHelper.GetCustListByRelaGradeNew(CompanyCD, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass, Area, CreditGrade, DateBegin, DateEnd);
        }

        //按客户关系等级统计后列表
        public static DataTable GetCustListByRelaGradeNewList(string CompanyCD, string RelaGrade, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
            string Area, string CreditGrade, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustInfoDBHelper.GetCustListByRelaGradeNewList(CompanyCD, RelaGrade, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass,
            Area, CreditGrade, BeginDate, EndDate, pageIndex, pageCount, ord, ref TotalCount);
        }

         //按客户关系等级统计后列表 导出用
        public static DataTable GetCustListByRelaGradeNewList(string CompanyCD, string RelaGrade, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
            string Area, string CreditGrade, string BeginDate, string EndDate)
        {
            return CustInfoDBHelper.GetCustListByRelaGradeNewList(CompanyCD, RelaGrade, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass,
            Area, CreditGrade, BeginDate, EndDate);
        }
        public static DataTable GetCustListByRelaGradeNewDetail(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
           string Area, string CreditGrade, string BeginDate, string EndDate)
        {
            return CustInfoDBHelper.GetCustListByRelaGradeNewDetail(CompanyCD, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass,
            Area, CreditGrade, BeginDate, EndDate);
        }

        #endregion

        #region 按客户优质级别统计
        /// <summary>
        /// 按客户优质级别统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetCustListByCreditGrade(string CompanyCD, string ord)
        {
            return CustInfoDBHelper.GetCustListByCreditGrade(CompanyCD, ord);
        }

         //按客户优质级别统计
        public static DataTable GetCustListByCreditGradeNew(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass, string Area, string RelaGrade, string DateBegin, string DateEnd)
        {
            return CustInfoDBHelper.GetCustListByCreditGradeNew(CompanyCD, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass, Area, RelaGrade, DateBegin, DateEnd);
        }

        //按客户所在区域统计后列表
        public static DataTable GetCustListByCreditGradeNewList(string CompanyCD, string CreditGradeID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
            string RelaGrade, string Area, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustInfoDBHelper.GetCustListByCreditGradeNewList(CompanyCD, CreditGradeID, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass,
                                RelaGrade, Area, BeginDate, EndDate, pageIndex, pageCount, ord, ref TotalCount);
        }

        //按客户优质级别列表 导出用
        public static DataTable GetCustListByCreditGradeNewList(string CompanyCD, string CreditGradeID, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
                    string RelaGrade, string Area, string BeginDate, string EndDate)
        {
            return CustInfoDBHelper.GetCustListByCreditGradeNewList(CompanyCD, CreditGradeID, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass,
                            RelaGrade, Area, BeginDate, EndDate);
        }
        public static DataTable GetCustListByCreditGradeNewDetail(string CompanyCD, string CustTypeManage, string CustTypeSell, string CustTypeTime, string CustType, string CustClass,
                    string RelaGrade, string Area, string BeginDate, string EndDate)
        {
            return CustInfoDBHelper.GetCustListByCreditGradeNewDetail(CompanyCD, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass,
                            RelaGrade, Area, BeginDate, EndDate);
        }
        #endregion

        #region 按时间统计
        public static DataTable GetCustListByTime(string CompanyCD, string DateSele, string CustTypeManage, string CustTypeSell, string CustTypeTime, 
                                                string CustType, string CustClass, string Area, string RelaGrade, string CreditGrade, string DateBegin, string DateEnd)
        {
            return CustInfoDBHelper.GetCustListByTime(CompanyCD, DateSele, CustTypeManage, CustTypeSell, CustTypeTime, 
                                                        CustType, CustClass, Area, RelaGrade, CreditGrade, DateBegin, DateEnd);
        }

         //按时间统计后列表
        public static DataTable GetCustListByTimeNewList(string CompanyCD, string DateSele, string DateTimeID, string CustTypeManage, string CustTypeSell, string CustTypeTime,
            string CustType, string CustClass, string Area, string RelaGrade, string CreditGrade, string BeginDate, string EndDate,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustInfoDBHelper.GetCustListByTimeNewList(CompanyCD, DateSele, DateTimeID, CustTypeManage, CustTypeSell, CustTypeTime, CustType, CustClass,
                Area, RelaGrade, CreditGrade, BeginDate, EndDate, pageIndex, pageCount, ord, ref TotalCount);
        }

        //按时间统计后列表 导出用
        public static DataTable GetCustListByTimeNewList(string CompanyCD, string DateSele, string DateTimeID, string CustTypeManage, string CustTypeSell, string CustTypeTime,
            string CustType, string CustClass, string Area, string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            return CustInfoDBHelper.GetCustListByTimeNewList(CompanyCD, DateSele, DateTimeID, CustTypeManage, CustTypeSell, CustTypeTime,
                            CustType, CustClass, Area, RelaGrade, CreditGrade, BeginDate, EndDate);
        }

        public static DataTable GetCustListByTimeNewDetail(string CompanyCD, string DateSele, string CustTypeManage, string CustTypeSell, string CustTypeTime,
            string CustType, string CustClass, string Area, string RelaGrade, string CreditGrade, string BeginDate, string EndDate)
        {
            return CustInfoDBHelper.GetCustListByTimeNewDetail(CompanyCD, DateSele, CustTypeManage, CustTypeSell, CustTypeTime,
                            CustType, CustClass, Area, RelaGrade, CreditGrade, BeginDate, EndDate);
        }
        #endregion

        #region 按订单量统计
        /// <summary>
        /// 按订单量统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="NumBegin"></param>
        /// <param name="NumEnd"></param>
        /// <param name="PriceBegin"></param>
        /// <param name="PriceEnd"></param>
        /// <param name="DateBegin"></param>
        /// <param name="DateEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        //public static DataTable GetCustOrders(string CompanyCD, string NumBegin, string NumEnd, string PriceBegin, string PriceEnd, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        //{
        //    return CustInfoDBHelper.GetCustOrders(CompanyCD, NumBegin, NumEnd, PriceBegin, PriceEnd, DateBegin, DateEnd, pageIndex, pageCount, ord, ref TotalCount);
        //}
        public static DataTable GetCustOrders(string CompanyCD, string ProductID, string CustID, string NumBegin, string NumEnd, string PriceBegin, string PriceEnd, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustInfoDBHelper.GetCustOrders(CompanyCD, ProductID, CustID, NumBegin, NumEnd, PriceBegin, PriceEnd, DateBegin, DateEnd, pageIndex, pageCount, ord, ref TotalCount);
        }
        //导出
        public static DataTable GetCustOrders(string CompanyCD, string ProductID, string CustID, string NumBegin, string NumEnd, string PriceBegin, string PriceEnd, string DateBegin, string DateEnd, string ord)
        {
            return CustInfoDBHelper.GetCustOrders(CompanyCD, ProductID, CustID, NumBegin, NumEnd, PriceBegin, PriceEnd, DateBegin, DateEnd, ord);
        }
        #endregion

        #region 按订单量统计打印
        /// <summary>
        /// 按订单量统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="NumBegin"></param>
        /// <param name="NumEnd"></param>
        /// <param name="PriceBegin"></param>
        /// <param name="PriceEnd"></param>
        /// <param name="DateBegin"></param>
        /// <param name="DateEnd"></param>
        /// <returns></returns>
        public static DataTable GetCustOrdersPrint(string CompanyCD, string ProductID, string CustID, string NumBegin, string NumEnd, string PriceBegin, string PriceEnd, string DateBegin, string DateEnd, string ord)
        {
            return CustInfoDBHelper.GetCustOrdersPrint(CompanyCD,ProductID,CustID, NumBegin, NumEnd, PriceBegin, PriceEnd, DateBegin, DateEnd, ord);
        }
        #endregion

        #region  按购买物品统计_报表
        /// <summary>
        /// 按购买物品统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="MinCount">数量最小值</param>
        /// <param name="MaxCount">数量最大值</param>
        /// <param name="MinPrice">金额最小值</param>
        /// <param name="MaxPrice">金额最大值</param>
        /// <param name="LinkDateBegin">订单开始时间</param>
        /// <param name="LinkDateEnd">订单结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatCustBuyByProduct(string ProductName,string CustID, string MinCount, string MaxCount, string MinPrice, string MaxPrice
            , string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
         
             return CustInfoDBHelper.GetStatCustBuyByProduct(ProductName,CustID, MinCount, MaxCount, MinPrice, MaxPrice, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount);
        }

        public static DataTable GetStatCustBuyByProduct(string ProductName, string CustID, string MinCount, string MaxCount, string MinPrice, string MaxPrice
            , string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            return CustInfoDBHelper.GetStatCustBuyByProduct(ProductName, CustID, MinCount, MaxCount, MinPrice, MaxPrice, CompanyCD,
                LinkDateBegin, LinkDateEnd, ord);
        }

        /// <summary>
        /// 按购买物品统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="MinCount">数量最小值</param>
        /// <param name="MaxCount">数量最大值</param>
        /// <param name="MinPrice">金额最小值</param>
        /// <param name="MaxPrice">金额最大值</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatCustBuyByProductPrint(string ProductName, string CustID, string MinCount, string MaxCount, string MinPrice, string MaxPrice
            , string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
                return CustInfoDBHelper.GetStatCustBuyByProductPrint(ProductName,CustID, MinCount, MaxCount, MinPrice, MaxPrice, CompanyCD, LinkDateBegin, LinkDateEnd, ord);

        }
        #endregion

        #region  按购买日期统计_报表
        /// <summary>
        /// 按购买日期统计
        /// </summary>
        /// <param name="ProductName">物品名称</param>
        /// <param name="LinkDateBegin">订单开始时间</param>
        /// <param name="LinkDateEnd">订单结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatCustBuyByDate(string ProductName, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
                return CustInfoDBHelper.GetStatCustBuyByDate(ProductName, CompanyCD, LinkDateBegin, LinkDateEnd, pageIndex, pageCount, ord, ref TotalCount); 

        }

        /// <summary>
        /// 按购买日期统计
        /// </summary>
        /// <param name="ProductName">物品名称</param>
        /// <param name="LinkDateBegin">订单开始时间</param>
        /// <param name="LinkDateEnd">订单结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatCustBuyByDatePrint(string ProductName, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
                return CustInfoDBHelper.GetStatCustBuyByDatePrint(ProductName, CompanyCD, LinkDateBegin, LinkDateEnd, ord);
        }
        #endregion

        #region  零购买客户统计_报表
        /// <summary>
        /// 零购买客户统计
        /// </summary>
        /// <param name="CustName">公司ID</param>
        /// <param name="LinkReasonId">天数</param>
        /// <returns></returns>
        public static DataTable GetStatCustBuyByDays(string CompanyCD, int Days, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
                return CustInfoDBHelper.GetStatCustBuyByDays(CompanyCD, Days, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 零购买客户统计
        /// </summary>
        /// <param name="CustName">公司ID</param>
        /// <param name="LinkReasonId">天数</param>
        /// <returns></returns>
        public static DataTable GetStatCustBuyByDaysPrint(string CompanyCD, int Days, string ord)
        {
                return CustInfoDBHelper.GetStatCustBuyByDaysPrint(CompanyCD, Days, ord);
        }
        #endregion

        #region 客户信息打印
        /// <summary>
        /// 客户信息打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CustNo"></param>
        /// <returns></returns>
        public static DataTable GetCustInfoByNo(string CompanyCD,string CustBig, string CustNo)
        {
            return CustInfoDBHelper.GetCustInfoByNo(CompanyCD,CustBig, CustNo);
        }
        #endregion

        #region 导出客户列表信息
        /// <summary>
        /// 导出客户列表信息
        /// </summary>
        /// <param name="CustModel"></param>
        /// <param name="Manager"></param>
        /// <param name="CanUserID"></param>
        /// <param name="CreatedBegin"></param>
        /// <param name="CreatedEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportCustInfo(CustInfoModel CustModel, string Manager, string CanUserID, string CreatedBegin, string CreatedEnd, string ord)
        {
            return CustInfoDBHelper.ExportCustInfo(CustModel, Manager, CanUserID, CreatedBegin, CreatedEnd, ord);
        }
        #endregion

        #region 批量导入客户档案信息
        /// <summary>
        /// 批量导入客户档案信息
        /// </summary>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool InsertCustInfoRecord(DataTable dt, string CompanyCD, string EmployeeID, string UserID)
        {
            return CustInfoDBHelper.InsertCustInfoRecord(dt, CompanyCD, EmployeeID, UserID);
        }
        #endregion

        public static DataTable CheckArea(string AreaName, string compid)
        {
            return CustInfoDBHelper.CheckArea(AreaName, compid);
        }

        public static DataTable GetCustTalkByType(string begindate, string enddate, string custName)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return CustInfoDBHelper.GetCustTalkByType(companyCD, begindate, enddate, custName);
        }

        public static DataTable GetCustTalkByTypeDetails(string begindate, string enddate, string custName, string talkType, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return CustInfoDBHelper.GetCustTalkByTypeDetails(companyCD,begindate,enddate,custName,talkType,pageIndex,pageCount,ord,ref TotalCount);
        }

        public static DataTable GetCustTalkByPriority(string begindate, string enddate, string custName)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return CustInfoDBHelper.GetCustTalkByPriority(companyCD, begindate, enddate, custName);
        }

        public static DataTable GetCustTalkByPriorityDetails(string begindate, string enddate, string custName, string Priority, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return CustInfoDBHelper.GetCustTalkByPriorityDetails(companyCD,begindate,enddate,custName,Priority,pageIndex,pageCount,ord,ref TotalCount);
        }

         public static DataTable GetCustTalkByCountDetails(string CompanyCD, string begindate, string enddate, string custName, string CustID, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
             string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
             return CustInfoDBHelper.GetCustTalkByCountDetails(companyCD, begindate, enddate, custName, CustID, pageIndex, pageCount, ord, ref TotalCount);
         }
    }
}

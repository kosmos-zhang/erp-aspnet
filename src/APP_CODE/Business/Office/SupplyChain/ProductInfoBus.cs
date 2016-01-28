using System;
using System.Text;
using System.Data;
using XBase.Model.Office.SupplyChain;
using XBase.Data.Office.SupplyChain;
using XBase.Common;
using XBase.Model.Common;
using XBase.Business.Common;
using XBase.Data.Common;
using System.Collections;
namespace XBase.Business.Office.SupplyChain
{
    public class ProductInfoBus
    {
        public static DataTable GetProductInfoTableBycondition(ProductInfoModel model, string QueryID, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                DataTable dt = new DataTable();
                if (QueryID == "-1")
                {
                    return ProductInfoDBHelper.GetProductInfoBycondition(model);
                }
                else
                {
                    return ProductInfoDBHelper.GetProductInfoTableBycondition(model, QueryID,EFIndex,EFDesc, pageIndex, pageCount, OrderBy, ref totalCount);
                }
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public static DataTable GetProductInfoBatchTableBycondition(ProductInfoModel model, string QueryID, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                DataTable dt = new DataTable();
                if (QueryID == "-1")
                {
                    return ProductInfoDBHelper.GetProductInfoBycondition(model);
                }
                else
                {
                    return ProductInfoDBHelper.GetProductInfoBatchTableBycondition(model, QueryID, EFIndex, EFDesc, pageIndex, pageCount, OrderBy, ref totalCount);
                }
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        //采购用
        public static DataTable GetProductInfoTableBycondition(ProductInfoModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return ProductInfoDBHelper.GetProductInfoTableBycondition(model, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 插入物品档案信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertProductInfo(ProductInfoModel model, out string ID, Hashtable htExtAttr)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ID = "0";
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.ProdNo);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                succ = ProductInfoDBHelper.InsertProductInfo(model, out ID, htExtAttr);
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
        /// 周军，获取物品扩展属性值方法
        /// </summary>
        /// <returns></returns>
        public static DataTable GetExtAttrValue(string strKey, string strProNo, string CompanyCD)
        {
            return ProductInfoDBHelper.GetExtAttrValue(strKey, strProNo, CompanyCD);
        }
        /// <summary>
        /// 修改物品档案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateProductInfo(ProductInfoModel model, Hashtable htExtAttr)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.ProdNo);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                succ = ProductInfoDBHelper.UpdateProductInfo(model, htExtAttr);
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
        public static DataTable GetProductInfoTableByCheckcondition(string strid)
        {
            try
            {
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                string CompanyCD = userInfo.CompanyCD;
                return ProductInfoDBHelper.GetProductInfoTableByCheckcondition(strid, CompanyCD);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        /// <summary>
        /// 查询获取物品信息
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static DataTable GetProductInfo(ProductInfoModel Model,string EFIndex,string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return ProductInfoDBHelper.GetProductInfo(Model,EFIndex, EFDesc, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public static DataTable SearchProduct(ProductInfoModel model, string conditions, int pageIndex, int pageCount, string OrderBy, ref int totalCount,string EFIndex,string EFDesc)
        {
            return ProductInfoDBHelper.SearchProduct(model, conditions, pageIndex, pageCount, OrderBy, ref totalCount, EFIndex, EFDesc);
        }

        public static DataTable PurchaseSearchProduct(ProductInfoModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            return ProductInfoDBHelper.PurchaseSearchProduct(model, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        /// <summary>
        /// 根据ID获取物品信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetProductInfoByID(int ID)
        {
            if (ID == 0)
                return null;
            try
            {
                return ProductInfoDBHelper.GetProductInfoByID(ID);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        /// <summary>
        /// 删除物品信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteProductInfo(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string CompanyCD = userInfo.CompanyCD;
            //string CompanyCD = "AAAAAA";
            bool isSucc = ProductInfoDBHelper.DeleteProductInfo(ID, CompanyCD);
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
                LogInfoModel logModel = InitLogInfo("物品ID：" + no);
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
        /// 判断是否已存在
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool Existss(string ProductID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string CompanyCD = userInfo.CompanyCD;
            string[] IdS = null;
            ProductID = ProductID.Substring(0, ProductID.Length);
            IdS = ProductID.Split(',');
            for (int i = 0; i < IdS.Length; i++)
            {
                bool isSucc = ProductInfoDBHelper.Exists(IdS[i].ToString(), CompanyCD);
                if (isSucc)
                    return true;
            }
            return false;
        }
        #region 查询：当前库存量
        /// <summary>
        /// 当前库存量
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageCount(int ProductID)
        {
            try
            {
                return ProductInfoDBHelper.GetStorageCount(ProductID);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        #endregion
        /// <summary>
        /// 确认操作
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="Status"></param>
        /// <param name="CheckUser"></param>
        /// <param name="CheckDate"></param>
        /// <returns></returns>
        public static bool UpdateStatus(int ProductID, string Status, string CheckUser, string CheckDate, string StorageID)
        {
            if (string.IsNullOrEmpty(Status)) return false;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string ModifiedUserID = userInfo.UserID;
            try
            {
                return ProductInfoDBHelper.UpdateStatus(ProductID, Status, CheckUser, CheckDate, ModifiedUserID, StorageID, userInfo.CompanyCD);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        #region 根据ID获取物品扩展属性信息
        /// <summary>
        /// 根据ID获取物品信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetProductInfoByAttr(int ID)
        {
            if (ID == 0)
                return null;
            try
            {
                return ProductInfoDBHelper.GetProductInfoByAttr(ID);
            }
            catch (Exception ex)
            {
                return null;
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
            logSys.ModuleID = ConstUtil.Menu_AddProduct;
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
            logModel.ModuleID = ConstUtil.Menu_AddProduct;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PRODUCTINFO;
            //操作对象
            logModel.ObjectID = prodno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion


        #region  验证条码的唯一性 add by hm
        /// <summary>
        /// 如果不存在则返回false，如果存在则返回true; add by hm
        /// </summary>
        /// <param name="BarCode"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool CheckBarCode(string BarCode, string CompanyCD)
        {
            try
            {
                return ProductInfoDBHelper.CheckBarCode(BarCode, CompanyCD);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        public static string GetCompanyUpFilePath(string companycd)
        {
            return ProductInfoDBHelper.GetCompanyUpFilePath(companycd);
        }

        public static DataSet GetProductInfoFromExcel(string companycd, string fname, string tbname)
        {
            return ProductInfoDBHelper.GetProductInfoFromExcel(companycd, fname, tbname);
        }

        public static DataSet ReadEexcel(string FilePath, string companycd)
        {
            return ProductInfoDBHelper.ReadEexcel(FilePath, companycd);
        }

        public static bool ChargeProduct(string sqlTypeCode, string codeNum, string compNo)
        {
            return ProductInfoDBHelper.ChargeProduct(sqlTypeCode, codeNum, compNo);
        }

        public static bool ChargeStorage(string storagename, string compid)
        {
            return ProductInfoDBHelper.ChargeStorage(storagename, compid);
        }

        public static bool ChargeCodeUnit(string codename, string compid)
        {
            return ProductInfoDBHelper.ChargeCodeUnit(codename, compid);
        }

        public static int GetExcelToProductInfo(string companycd, string usercode)
        {
            return ProductInfoDBHelper.GetExcelToProductInfo(companycd,usercode);
        } 

        public static void DeleteFile(string companycd, string filename)
        {
            ProductInfoDBHelper.DeleteFile(companycd, filename);
        }

        public static void LogInsert(string companyCD, int deptID, string exportEmpID, string exportObject, int exportNum, int exportResult, string exportError)
        {
            ProductInfoDBHelper.LogInsert(companyCD, deptID, exportEmpID, exportObject, exportNum, exportResult, exportError);
        }

        public static DataTable GetLogPage(string userid, string begindate, string enddate, string mod, string companycd, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            return ProductInfoDBHelper.GetLogPage(userid, begindate, enddate, mod, companycd, pageIndex, pageCount, OrderBy, ref totalCount);
        }

        public static DataSet GetError(string ncode)
        {
            return ProductInfoDBHelper.GetError(ncode);
        }

        public static string GetLogTitle(string ncode)
        {
            return ProductInfoDBHelper.GetLogTitle(ncode);
        }

        public static bool ChargeCodeType(string codename, string compid)
        {
            return ProductInfoDBHelper.ChargeCodeType(codename, compid);
        }

        public static bool ChargeBarCode(string codename, string compid)
        {
            return ProductInfoDBHelper.ChargeBarCode(codename, compid);
        }

        public static bool ValidateProductColor(string colorName, string companyCD)
        {
            return ProductInfoDBHelper.ValidateProductColor(colorName, companyCD);
        }
        #region 查询计量单位组信息
        public static DataTable GetUnitGroupList(string GroupUnitNo)
        {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           return ProductInfoDBHelper.GetUnitGroupList(userInfo.CompanyCD,GroupUnitNo);
        }
        #endregion

        #region 根据物品表的生产等单位查询单位名称
        public static DataSet GetListUnitName(int ProdID)
        {
            return ProductInfoDBHelper.GetListUnitName(ProdID);
        }
        #endregion

        #region 判断某物品所有批次是否清零
        /// <summary>
        /// 判断某物品所有批次是否清零
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static decimal GetProductCountByAllBatchNo(string prodNo,string companyCD)
        {
            return ProductInfoDBHelper.GetProductCountByAllBatchNo(prodNo,companyCD);
        }
        #endregion


        #region 判断某物品是否已经确认过
        /// <summary>
        /// 判断某物品是否已经确认过
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static bool IsConfirmProduct(string ProductID)
        {
            return ProductInfoDBHelper.IsConfirmProduct(ProductID);
        }
        #endregion

        #region 根据输入的物品编号获取匹配的物品信息
        /// <summary>
        /// 根据输入的物品编号获取匹配的物品信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ProdNo"></param>
        /// <returns></returns>
        public static DataTable GetMatchProductInfo(string CompanyCD,string ProdNo)
        {
            try
            {
                return ProductInfoDBHelper.GetMatchProductInfo(CompanyCD,ProdNo);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }

}

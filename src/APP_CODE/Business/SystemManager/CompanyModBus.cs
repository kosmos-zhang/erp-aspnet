/**********************************************
 * 类作用：   企业与模块关联逻辑事务层
 * 建立人：   吴成好
 * 建立时间： 2009/01/22
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Model.SystemManager;
using XBase.Data.SystemManager;
using XBase.Common;

namespace XBase.Business.SystemManager
{
    public class CompanyModBus
    {
        /// <summary>
        /// 批量增加企业功能模块
        /// </summary>
        public static bool AddCompanyModule(string companyCD, string[] module)
        {
            try
            {
                //获取登陆用户ID
                //string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                string loginUserID = "wuchenghao";
                return CompanyModuleDBHelper.AddCompanyModule(companyCD, module, loginUserID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据企业编码获取企业的功能模块
        /// </summary>
        /// <param name="CompanyCD">企业编码</param>
        /// <returns>DataTable</returns>
        public static DataTable GetCompanyModuleInfo(string CompanyCD)
        {
            if (string.IsNullOrEmpty(CompanyCD)) return null;
            try
            {
                return CompanyModuleDBHelper.GetCompanyModuleInfo(CompanyCD);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 所有功能模块信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetSysModuleInfo()
        {
            try
            {
                return CompanyModuleDBHelper.GetSysModuleInfo();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除企业的功能模块
        /// </summary>
        public static int DelCompanyModule(CompanyModuleModel Model)
        {
            if (Model == null) return 0;
            try
            {
                return CompanyModuleDBHelper.DelCompanyModule(Model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        
    }
}

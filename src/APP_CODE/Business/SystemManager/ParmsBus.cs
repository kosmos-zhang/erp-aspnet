/***********************************************************************
 * 
 * Module Name:Business.SystemManager.SystemBus.cs
 * Current Version: 1.0 
 * Creator: jiangym
 * Auditor:
 * Create Date:2009-01-07
 * End Date:
 * Description:
 * Version History:
 ***********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.SystemManager;
using System.Data;
using System.Web.Caching;
using XBase.Common;
namespace XBase.Business.SystemManager
{
    public class SystemBus
    {

 
        /// <summary>
        /// 获取系统公参信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetPubParms()
        {
            try
            {
                return ParmsDBHelper.GetPubParms();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取企业信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCompanyParms()
        {
            //string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //if (string.IsNullOrEmpty(CompanyCD)) return null;
            try
            {
                return ParmsDBHelper.GetCompanyParms();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取企业开通服务信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCompanyOpenSevParms()
        {
            //string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //if (string.IsNullOrEmpty(CompanyCD)) return null;
            try
            {
                return ParmsDBHelper.GetCompanyOpenSevParms();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

    }
}

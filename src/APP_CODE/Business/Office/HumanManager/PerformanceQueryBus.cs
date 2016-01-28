using System;
using XBase.Model.Office.HumanManager;
using XBase.Data.Office.HumanManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;
using System.Collections.Generic;
namespace XBase.Business.Office.HumanManager
{
    public  class PerformanceQueryBus
    {
        public static DataTable SearchRectCheckElemInfo(PerformanceTaskModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询

            return PerformanceQueryDBHelper.SearchCheckElemInfo(model);

        }


        public static DataTable SearchSubDeptInfo(string deptID)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行查询

            return PerformanceQueryDBHelper.SearchSubDeptInfo(deptID,companyCD);

        }
        /// <summary>
        /// 获取公司部门信息
        /// </summary>
        /// <returns></returns>
        public static DataTable SearchDeptInfo()
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行查询

            return PerformanceQueryDBHelper.SearchDeptInfo(companyCD);

        }
        public static DataTable SearchStaticsInfo(PerformanceTaskModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询

            return PerformanceQueryDBHelper.SearchStaticsInfo(model);

        }
        /// <summary>
        /// 考核等级统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable SearchDetailsInfoByLT(PerformanceTaskModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询

            return PerformanceQueryDBHelper.SearchDetailsInfoByLT(model);

        }
        /// <summary>
        /// 建议等级统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable SearchDetailsInfoByLA(PerformanceTaskModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询

            return PerformanceQueryDBHelper.SearchDetailsInfoByLA(model);

        }
        public static DataTable SearchScoreInfo(PerformanceTaskModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询

            return PerformanceQueryDBHelper.SearchScoreInfo(model);

        }


        public static DataTable SearchPerTypeInfo(PerformanceTypeModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询

            return PerformanceQueryDBHelper.SearchPerTypeInfo (model);

        }
        public static DataTable SearchSummaryInfo(string taskNo, string templateNo, string employeeID)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行查询

            return PerformanceQueryDBHelper.SearchSummaryInfo(  taskNo,   templateNo,   employeeID,   companyCD);

        }
    }
}

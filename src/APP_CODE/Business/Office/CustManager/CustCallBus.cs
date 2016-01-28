using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Model.Office.CustManager;
using XBase.Data.Office.CustManager;
using XBase.Common;

namespace XBase.Business.Office.CustManager
{
    public class CustCallBus
    {
        #region 客户来电
        #region 根据电话号码获取来电记录列表
        public static DataTable GetCustCallByTel(CustCallModel CustCallM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCustCallByTel(CustCallM, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 根据来电号码自动记录
        public static bool AddCustCallByTel(CustCallModel model)
        {
            return CustCallDBHelper.AddCustCallByTel(model);
        }
        #endregion

        #region 根据来电号码判断是否有对应客户
        public static DataTable GetCustInfoByTel(string CompanyCD, string Tel)
        {
            return CustCallDBHelper.GetCustInfoByTel(CompanyCD, Tel);
        }
        #endregion

        #region 根据客户id获取客户信息
        public static DataTable GetCustInfoByID(string CustID, string Tel, string CompanyCD)
        {
            return CustCallDBHelper.GetCustInfoByID(CustID,Tel,CompanyCD);
        }
        #endregion

        #region 根据客户ID获取客户投诉信息
        public static DataTable GetCustComplainByCustID(CustComplainModel CustComplainM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCustComplainByCustID(CustComplainM, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 根据客户ID获取客户服务信息
        public static DataTable GetCustServiceByCustID(CustServiceModel CustServiceM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCustServiceByCustID(CustServiceM, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 根据客户ID获取客户联络信息
        public static DataTable GetCustContactByCustID(ContactHistoryModel ContactHistoryM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCustContactByCustID(ContactHistoryM, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 根据记录id获取来电记录信息
        public static DataTable GetCallInfoByID(string CallID)
        {
            return CustCallDBHelper.GetCallInfoByID(CallID);
        }
        #endregion

        #region 根据来电号码自动记录
        public static bool UpdateCallBuID(CustCallModel model)
        {
            return CustCallDBHelper.UpdateCallBuID(model);
        }
        #endregion

        #region 页面根据条件获取来电记录列表
        public static DataTable GetCustCallByCon(CustCallModel CustCallM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCustCallByCon(CustCallM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref  TotalCount);
        }
        #endregion

        #region 导出
        public static DataTable GetCustCallByCon(CustCallModel CustCallM, string DateBegin, string DateEnd, string ord)
        {
            return CustCallDBHelper.GetCustCallByCon(CustCallM, DateBegin, DateEnd, ord);
        }
        #endregion
        #endregion 

        #region 综合查询

        #region 客户档案
        public static DataTable GetCust_daByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCust_daByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 客户联系人
        public static DataTable GetCust_lxrByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCust_lxrByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 客户联络
        public static DataTable GetCust_llByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCust_llByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 客户洽谈
        public static DataTable GetCust_qtByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCust_qtByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 客户关怀
        public static DataTable GetCust_ghByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCust_ghByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 客户服务
        public static DataTable GetCust_fwByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCust_fwByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 客户投诉
        public static DataTable GetCust_tsByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCust_tsByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 客户建议
        public static DataTable GetCust_jyByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCust_jyByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 客户来电记录
        public static DataTable GetCust_ldByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCust_ldByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref TotalCount); 
        }
        #endregion

        #region Cust树
        /// <summary>
        /// Cust树
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetCustTree(CustInfoModel model, string CanUserID)
        {
            return CustCallDBHelper.GetCustTree(model, CanUserID);
        }
        #endregion

        #region 根据记录id获取客户信息
        public static DataTable GetCustInfoByID(string CustID)
        {
            return CustCallDBHelper.GetCustInfoByID(CustID);
        }
        #endregion

        #region 客户购买记录
        public static DataTable GetCust_gmByCon(CustInfoModel CustInfoM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCust_gmByCon(CustInfoM, pageIndex, pageCount, ord, ref  TotalCount);
        }
        #endregion

        #region 发货记录
        public static DataTable GetCust_fhByCon(CustInfoModel CustInfoM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCust_fhByCon(CustInfoM, pageIndex, pageCount, ord, ref  TotalCount);
        }
        #endregion

        #region 回款计划
        public static DataTable GetCust_hkByCon(CustInfoModel CustInfoM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCust_hkByCon(CustInfoM, pageIndex, pageCount, ord, ref  TotalCount);
        }
        #endregion

        #region 回款记录
        public static DataTable GetCust_jlByCon(CustInfoModel CustInfoM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCust_jlByCon(CustInfoM, pageIndex, pageCount, ord, ref  TotalCount);
        }
        #endregion

        #region 销售机会
        public static DataTable GetCust_jhByCon(CustInfoModel CustInfoM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return CustCallDBHelper.GetCust_jhByCon(CustInfoM, pageIndex, pageCount, ord, ref  TotalCount);
        }
        #endregion

        #endregion
    }
}

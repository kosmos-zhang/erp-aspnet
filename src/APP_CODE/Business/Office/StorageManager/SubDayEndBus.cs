using System;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Common;
using System.Collections;
using XBase.Data.Common;
using XBase.Model.Common;
using XBase.Business.Common;
using System.Collections.Generic;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.StorageManager;


namespace XBase.Business.Office.StorageManager
{
    public  class SubDayEndBus
    { 
        /// <summary>
        /// 从分仓存量表中获取改公司分店下的所有产品信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetCompanyProductList(string CompanyCD,string DeptID)
        {
            return SubDayEndDBHelper.GetCompanyProductList(CompanyCD,DeptID );
        }
        /// <summary>
        /// 获得前一天的某个物品的结存量
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="StorageID"></param>
        /// <param name="DailyDate"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static decimal GetFrontDayCount(string ProductID, string BatchNo, string DailyDate, string CompanyCD,string DeptID)
        {
            return SubDayEndDBHelper.GetFrontDayCount(ProductID, BatchNo, DailyDate, CompanyCD, DeptID);
        }


        /// <summary>
        /// 获得第一天的某个物品的结存量
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="StorageID"></param>
        /// <param name="DailyDate"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static decimal GetFirstDayCount(string ProductID, string BatchNo, string CompanyCD, string DeptID)
        {
            return SubDayEndDBHelper.GetFirstDayCount(ProductID, BatchNo, CompanyCD, DeptID);
        }
        /// <summary>
        /// 获得库存流水账表中取得当天的某个物品的各种单据数量信息
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="StorageID"></param>
        /// <param name="DailyDate"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="ItemsType"></param>
        /// <returns></returns>
        public static decimal GetDayItemsCount(string ProductID, string BatchNo, string DeptID,string DailyDate, string CompanyCD, int ItemsType)
        {
            return SubDayEndDBHelper.GetDayItemsCount(ProductID, BatchNo,DeptID, DailyDate, CompanyCD, ItemsType);
        }
        /// <summary>
        /// 获得库存流水账表中取得当天的某个物品的各种单据金额信息
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="StorageID"></param>
        /// <param name="DailyDate"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="ItemsType"></param>
        /// <returns></returns>
        public static decimal GetDayItemsPrice(string ProductID, string BatchNo, string StorageID, string DailyDate, string CompanyCD, int ItemsType)
        {
            return SubDayEndDBHelper.GetDayItemsPrice(ProductID, BatchNo, StorageID, DailyDate, CompanyCD, ItemsType);
        }

        /// <summary>
        ///  从分店销售订单表中分离出当天某发货模式下的信息
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="StorageID"></param>
        /// <param name="DailyDate"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSaleInfo(string ProductID, string BatchNo, string DeptID, string DailyDate, string CompanyCD, int SendModle)
        {
            return SubDayEndDBHelper.GetSaleInfo(ProductID, BatchNo, DeptID, DailyDate, CompanyCD, SendModle);
        }

        /// <summary>
        ///  从分店销售退货表中分离出当天某发货模式下的信息
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="StorageID"></param>
        /// <param name="DailyDate"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSaleRejectInfo(string ProductID, string BatchNo, string DeptID, string DailyDate, string CompanyCD, int SendModle)
        {
            return SubDayEndDBHelper.GetSaleRejectInfo(ProductID, BatchNo, DeptID, DailyDate, CompanyCD, SendModle);
        }


        #region 查询日结列表所需数据
        /// <summary>
        /// 查询日结列表所需数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="orderBy"></param>
        /// <param name="TotalCount"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static DataTable SelectDayInfo(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string day,string DeptID)
        {
            try
            {
                return SubDayEndDBHelper.SelectDayInfo(pageIndex, pageCount, orderBy, ref TotalCount, day, DeptID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// <summary>
        /// 防止重复日结某一天，出现数据冗余，先删除当天在日结表中的数据
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static bool DeleteDay(string CompanyCD, string day,string DeptID)
        {
            return SubDayEndDBHelper.DeleteDay(CompanyCD, day,DeptID );
        }
        /// <summary>
        /// 获取日结明细信息
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="DeptID"></param>
        /// <param name="DailyDate"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSubStorageDailyInfo(string ProductID, string BatchNo, string DeptID, string DailyDate, string CompanyCD)
        {
            return SubDayEndDBHelper.GetSubStorageDailyInfo(ProductID, BatchNo, DeptID, DailyDate, CompanyCD);
        }


        /// <summary>
        /// 判断前一天是否做日结
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static bool CheckDay(string CompanyCD, string day, string DeptID)
        {
            return SubDayEndDBHelper.CheckDay(CompanyCD, day,DeptID );
        }



        /// <summary>
        /// 判断前一天是否做日结(false 是第一次日结)
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static bool CheckFirstOperate(string CompanyCD,string deptID)
        {
            return SubDayEndDBHelper.CheckFirstOperate(CompanyCD, deptID );
        }


        /// <summary>
        /// 判断流水账表是否有当天的业务
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static bool isHaveData(string CompanyCD, string deptID,string day)
        {
            return SubDayEndDBHelper.isHaveData(CompanyCD, deptID,day);
        }

        /// <summary>
        /// 返回第一次日结的日期(FirstDailyDate)和最后一次做日结的日期(LastDailyDate)
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetOperateDateInfo(string CompanyCD,string deptid)
        {
            return SubDayEndDBHelper.GetOperateDateInfo(CompanyCD, deptid);
        }
    }
}

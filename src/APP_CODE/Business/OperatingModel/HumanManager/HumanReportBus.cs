using System;
using XBase.Model.Office.HumanManager;
using XBase.Data.Office.HumanManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;
using System.Collections;
using System.Collections.Generic;
namespace XBase.Business.Office.HumanManager
{
  public   class HumanReportBus
    {
        #region 人员状况明细月报搜索
        /// <summary>
        /// 人员状况明细月报
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable EmployeeExaminationSelect(string CompanyCD, string DeptID, string monthStartDate , string monthEndDate,   int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return HumanReportDBHelper.EmployeeExaminationSelect(CompanyCD, DeptID,monthStartDate ,monthEndDate,  pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion
        #region 人员状况明细月报搜索
        /// <summary>
        /// 人员状况明细月报
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable EmployeeExamination2Select(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return HumanReportDBHelper.EmployeeExamination2Select(CompanyCD, DeptID, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion
      
                  #region 人员状况明细月报  招聘人数明细
        /// <summary>
        /// 人员状况明细月报  招聘 明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable EmployeeConditionByZhaoPing(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return HumanReportDBHelper.EmployeeConditionByZhaoPing(CompanyCD, DeptID, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

                        #region 人员状况明细月报   面试人数明细
        /// <summary>
        /// 人员状况明细月报   面试明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable EmployeeConditionByMianShi(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return HumanReportDBHelper.EmployeeConditionByMianShi(CompanyCD, DeptID, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion
                    #region 人员状况明细月报   报道人数明细
        /// <summary>
        /// 人员状况明细月报   报道人数明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable EmployeeConditionByBaoDao(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return HumanReportDBHelper.EmployeeConditionByBaoDao(CompanyCD, DeptID, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion
       
                             #region 人员状况明细月报   迟到人数明细
        /// <summary>
        /// 人员状况明细月报   迟到人数明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable EmployeeConditionByChiDao(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return HumanReportDBHelper.EmployeeConditionByChiDao(CompanyCD, DeptID, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion
                               #region 人员状况明细月报   早退人数明细
        /// <summary>
        /// 人员状况明细月报   早退人数明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable EmployeeConditionByZaoTui(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return HumanReportDBHelper.EmployeeConditionByZaoTui(CompanyCD, DeptID, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion
                                     #region 人员状况明细月报   旷工人数明细
        /// <summary>
        /// 人员状况明细月报   旷工人数明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable EmployeeConditionByKuangGong(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return HumanReportDBHelper.EmployeeConditionByKuangGong(CompanyCD, DeptID, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion
      
                                               #region 人员状况明细月报   请假人数明细
        /// <summary>
        /// 人员状况明细月报   请假人数明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable EmployeeConditionByQingjia(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return HumanReportDBHelper.EmployeeConditionByQingjia(CompanyCD, DeptID, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion
        #region 人员状况明细月报   迁出人数明细
        /// <summary>
        /// 人员状况明细月报   迁出人数明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable EmployeeConditionByQianchu(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return HumanReportDBHelper.EmployeeConditionByQianchu(CompanyCD, DeptID, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 人员状况明细月报   迁入人数明细
        /// <summary>
        /// 人员状况明细月报   迁入人数明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable EmployeeConditionByQianRu(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return HumanReportDBHelper.EmployeeConditionByQianRu(CompanyCD, DeptID, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 人员状况明细月报   迁入人数明细
        /// <summary>
        /// 人员状况明细月报   迁入人数明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable EmployeeConditionByOldDepartQianRu(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return HumanReportDBHelper.EmployeeConditionByQianRu(CompanyCD, DeptID, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion
        #region 人员状况明细月报   离职人数明细
        /// <summary>
        /// 人员状况明细月报   离职人数明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable EmployeeConditionByLiZhi(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return HumanReportDBHelper.EmployeeConditionByLiZhi(CompanyCD, DeptID, monthStartDate, monthEndDate, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion
        #region 工资标准明细
        /// <summary>
        /// 工资标准明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable SalaryStandardSelect(SalaryStandardModel searchModel , int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return HumanReportDBHelper.SalaryStandardSelect(searchModel, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 部门工资明细统计
        /// <summary>
        /// 部门工资明细统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable SalarySummerySelect(SalaryStandardModel searchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return HumanReportDBHelper.SalarySummerySelect(searchModel, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 部门计件工资走势分析
        /// <summary>
        /// 部门计件工资走势分析
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable DeptPieceSelect(string CompanyCD, string DeptID, string year)
        {
            return HumanReportDBHelper.DeptPieceSelect(CompanyCD, DeptID, year);
        }
        #endregion

        #region 计件工资走势分析明细页面
        /// <summary>
        /// 计件工资走势分析明细页面
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable DeptPieceReportSelect(SalaryStandardModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return HumanReportDBHelper.DeptPieceReportSelect(model, pageIndex, pageCount, ord, ref  totalCount);
        }
        #endregion

        #region 部门计时工资走势分析
        /// <summary>
        /// 部门计时工资走势分析
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable DepTimeSelect(string CompanyCD, string DeptID, string year)
        {
            return HumanReportDBHelper.DepTimeSelect(CompanyCD, DeptID, year);
        }
        #endregion

        #region    工资月份走势
        /// <summary>
        ///  工资月份走势
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable DeptRealMoneySelect(string CompanyCD, string DeptID, string year)
        {
            return HumanReportDBHelper.DeptRealMoneySelect(CompanyCD, DeptID, year);
        }
        #endregion  DeptRealMoneyReportPrintSelect 

        #region    人员工资汇总统计
        /// <summary>
        ///  人员工资汇总统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable DeptMoneySummarySelect(string CompanyCD, string DeptID, string year)
        {
            return HumanReportDBHelper.DeptMoneySummarySelect(CompanyCD, DeptID, year);
        }
        #endregion  DeptRealMoneyReportPrintSelect 
        
        #region 工资走势月份分析明细页面
        /// <summary>
        /// 工资走势月份分析明细页面
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable DeptRealMoneyReportPrintSelect(SalaryStandardModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return HumanReportDBHelper.DeptRealMoneyReportPrintSelect(model, pageIndex, pageCount, ord, ref  totalCount);
        }
        #endregion

      
            #region 计时工资走势分析明细页面
        /// <summary>
        /// 计时工资走势分析明细页面
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable DeptTimeReportPrintSelect(SalaryStandardModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return HumanReportDBHelper.DeptTimeReportPrintSelect(model, pageIndex, pageCount, ord, ref  totalCount);
        }
        #endregion


        #region 绩效考核按等级分布明细
        /// <summary>
        /// 绩效考核按等级分布明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable PerformanceDetailsByLTPrintSelect(PerformanceTaskModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return HumanReportDBHelper.PerformanceDetailsByLTPrintSelect(model, pageIndex, pageCount, ord, ref  totalCount);
        }
        #endregion
        #region 绩效考核按建议分布明细
        /// <summary>
        /// 绩效考核按等级分布明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable PerformanceDetailsByLAPrintSelect(PerformanceTaskModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return HumanReportDBHelper.PerformanceDetailsByLAPrintSelect(model, pageIndex, pageCount, ord, ref  totalCount);
        }
        #endregion


        #region 员工考试次数分析明细
        /// <summary>
        /// 员工考试次数分析明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable EmployeeTestCountPrintSelect(EmployeeTestSearchModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return HumanReportDBHelper.EmployeeTestCountPrintSelect(model, pageIndex, pageCount, ord, ref  totalCount);
        }
        #endregion

              #region 员工培训次数分析明细
        /// <summary>
        /// 员工培训次数分析明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable TrainningCountAnalysePrintSelect(string  CompanyCD,string  RequestDept,string   EmployeeID, string  DateBegin,string   DateEnd,string   DateB,string   DateE, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return HumanReportDBHelper.TrainningCountAnalysePrintSelect(CompanyCD, RequestDept, EmployeeID, DateBegin, DateEnd, DateB, DateE, pageIndex, pageCount, ord, ref  totalCount);
        }
        #endregion
      
    }
}

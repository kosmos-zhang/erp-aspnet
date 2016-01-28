/**********************************************
 * 描述：     账簿管理业务处理
 * 建立人：   莫申林
 * 建立时间： 2009/04/16
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;
using System.Collections;

namespace XBase.Business.Office.FinanceManager
{
    public class AcountBookBus
    { 

      

        /// <summary>
       /// 根据查询条件获取账簿信息
       /// </summary>
       /// <param name="queryStr">查询条件</param>
       /// <returns></returns>
        public static DataTable GetAcountBookInfo(string queryStr)
        {
            try
            {
                return AcountBookDBHelper.GetAcountBookInfo(queryStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
       /// 总分类帐数据源
       /// </summary>
       /// <param name="beginTime">开始日期</param>
       /// <param name="EndTime">结束日期</param>
       /// <param name="SubjectsCD">科目编码</param>
       /// <returns></returns>
        public static DataTable GetAcountBookTotalSource(string beginTime, string EndTime, string SubjectsCD, string CurryTypeID, string CompanyCD, string EndSubjectsCD, string FromTBName, string FileName, string subjectsDetails)
        {
             try
            {
                return AcountBookDBHelper.GetAcountBookTotalSource(beginTime,EndTime,SubjectsCD,CurryTypeID,CompanyCD,EndSubjectsCD,FromTBName,FileName,subjectsDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     

            /// <summary>
       /// 根据查询条件获取本期借贷方发生的金额总和
       /// </summary>
       /// <param name="queryStr"></param>
       /// <returns></returns>
       public static DataTable GetAcountSumAmount(string queryStr)
       {
           try
           {
               return AcountBookDBHelper.GetAcountSumAmount(queryStr);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
         /// <summary>
       /// 明细帐数据源
       /// </summary>
       /// <param name="begindate"></param>
       /// <param name="enddate"></param>
       /// <param name="subjectsCD"></param>
       /// <returns></returns>
       public static DataTable GetAccountBookListSource(string begindate, string enddate, string subjectsCD, string CurryTypeID, string CompanyCD, string EndSubjectsCD, string FromTBName, string FileName, string subjectsDetails)
       {
           try
           {
               return AcountBookDBHelper.GetAccountBookListSource(begindate,enddate,subjectsCD,CurryTypeID,CompanyCD,EndSubjectsCD,FromTBName,FileName,subjectsDetails);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

         /// <summary>
       /// 根据期末余额和会计科目来判断期末余额方向
       /// </summary>
       /// <param name="subjectsCD"></param>
       /// <param name="money"></param>
       /// <returns></returns>
       public static string DirectionSource(string subjectsCD, decimal money)
       {
           try
           {
               return AcountBookDBHelper.DirectionSource(subjectsCD,money);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

        /// <summary>
       /// 外币明细帐数据源
       /// </summary>
       /// <param name="begindate"></param>
       /// <param name="enddate"></param>
       /// <param name="subjectsCD"></param>
       /// <returns></returns>
       public static DataTable GetForeignAccountBookListSource(string begindate, string enddate, string subjectsCD, string CurryTypeID, string EndSubjectsCD, string FromTBName, string FileName, string subjectsDetails)
       {
           try
           {
               return AcountBookDBHelper.GetForeignAccountBookListSource(begindate, enddate,subjectsCD,CurryTypeID,EndSubjectsCD,FromTBName,FileName,subjectsDetails);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

        /// <summary>
       /// 总分类帐数据源(不是本币或本位币的外币总帐)
       /// </summary>
       /// <param name="beginTime">开始日期</param>
       /// <param name="EndTime">结束日期</param>
       /// <param name="SubjectsCD">科目编码</param>
       /// <returns></returns>
       public static DataTable GetForeignAcountBookTotalSource(string beginTime, string EndTime, string SubjectsCD, string CurryTypeID, string EndSubjectsCD, string FromTBName, string FileName, string subjectsDetails)
       {
           try
           {
               return AcountBookDBHelper.GetForeignAcountBookTotalSource(beginTime, EndTime, SubjectsCD, CurryTypeID,EndSubjectsCD,FromTBName,FileName,subjectsDetails);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}

/**********************************************
 * 类作用：   库存流水账
 * 建立人：   莫申林
 * 建立时间： 2010/02/09
 ***********************************************/
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using XBase.Model.Office.PurchaseManager;
using XBase.Model.Office.ProductionManager;
using XBase.Model.Office.SellManager;
using System.Collections;
using XBase.Common;


namespace XBase.Data.Office.StorageManager
{
    public class StrongeJournalDBHelper
    {
        /// <summary>
        /// 库存流水账--汇总信息
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSumStrongJournal(string queryStr,string extQueryStr, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder SqlStr = new StringBuilder();
            if (extQueryStr.Trim().Length > 0)
            {

                SqlStr.AppendLine(" select  " + extQueryStr.Split('@')[1] +" ,a.ProductID,a.CompanyCD,a.StorageID,isnull(a.InProductCount,0) as InProductCount , ");
            }
            else
            {
                SqlStr.AppendLine(" select a.ProductID,a.CompanyCD,a.StorageID,isnull(a.InProductCount,0) as InProductCount , ");
            } 
            SqlStr.AppendLine(" isnull(a.OutProductCount,0) as OutProductCount, isnull(b.ProdNo,'') as ProdNo, ");
            SqlStr.AppendLine(" isnull(b.ProductName,'') as ProductName,isnull(b.Specification,'') as Specification, ");
            SqlStr.AppendLine(" c.StorageName,c.StorageNo,d.ProductCount as NowProductCount,isnull(b.Size,'') as ProductSize, ");
            SqlStr.AppendLine(" isnull(b.FromAddr,'') as FromAddr,case when a.BatchNo='' then NULL ELSE a.BatchNo end as BatchNo ");
            SqlStr.AppendLine(" from ( ");

            SqlStr.AppendLine(" select case when a.ProductID is null then b.ProductID else a.ProductID end as ProductID, ");
            SqlStr.AppendLine(" case when a.StorageID is null then b.StorageID else a.StorageID end as StorageID, ");
            SqlStr.AppendLine(" InProductCount,OutProductCount, case when a.BatchNo is null then b.BatchNo else a.BatchNo END AS BatchNo ,case when a.CompanyCD is null then b.CompanyCD ELSE a.CompanyCD end as CompanyCD ");
            SqlStr.AppendLine(" from ( ");

            SqlStr.AppendLine(" SELECT [ProductID]  ,[StorageID],CompanyCD,BatchNo,sum(isnull(OutProductCount,0)) as OutProductCount from  ");
            SqlStr.AppendLine(" (SELECT [ProductID]  ,[StorageID],CompanyCD,  ISNULL(BatchNo,'')  as  BatchNo ,sum(isnull(HappenCount,0)) as OutProductCount  ");

            SqlStr.AppendLine("  FROM officedba.StorageAccount  ");
            SqlStr.AppendLine(" where HappenDate is not null and BillType in ('6','7','8','10','12','16','17','19','21')  {0} ");
            SqlStr.AppendLine("  group by  StorageID,ProductID,CompanyCD ,BatchNo ) d   ");
            SqlStr.AppendLine(" group by [ProductID]  ,[StorageID],CompanyCD,BatchNo ");


            SqlStr.AppendLine(" ) a   full outer join ");
            SqlStr.AppendLine(" ( ");


            SqlStr.AppendLine(" SELECT [ProductID]  ,[StorageID],CompanyCD,BatchNo,sum(isnull(InProductCount,0)) as InProductCount from ");
            SqlStr.AppendLine(" (SELECT [ProductID]  ,[StorageID],CompanyCD,  ISNULL(BatchNo,'')  as  BatchNo ,sum(isnull(HappenCount,0)) as InProductCount ");
            SqlStr.AppendLine(" FROM officedba.StorageAccount ");
            SqlStr.AppendLine(" where HappenDate is not null and BillType in ('1','2','3','4','5','9','11','13','14','15','18','20','22')  {0} ");
            SqlStr.AppendLine(" group by  StorageID,ProductID,CompanyCD ,BatchNo ) e ");
            SqlStr.AppendLine(" group by [ProductID]  ,[StorageID],CompanyCD,BatchNo ");



            SqlStr.AppendLine(" ) b on  a.[ProductID]=b.[ProductID] and a.[StorageID]=b.[StorageID] and a.CompanyCD=b.CompanyCD and a.BatchNo=b.BatchNo  ");
            SqlStr.AppendLine(" ) a left outer join officedba.ProductInfo b ");
            SqlStr.AppendLine(" on a.ProductID=b.ID ");
            SqlStr.AppendLine(" left outer join officedba.StorageInfo c ");
            SqlStr.AppendLine(" on a.StorageID=c.ID ");
            SqlStr.AppendLine(" left outer join officedba.StorageProduct d ");
            SqlStr.AppendLine(" on a.ProductID=d.ProductID and a.StorageID=d.StorageID and a.CompanyCD=d.CompanyCD and a.BatchNo=d.BatchNo {1}");

            string sql = string.Format(SqlStr.ToString(), queryStr.Trim().Length > 0 ? queryStr : "", extQueryStr.Trim().Length > 0 ? " where " + extQueryStr.Split('@')[0] : "");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 库存流水账--明细信息
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetDetailStrongJournal(string queryStr, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {

            string expStr = string.Empty;
            int index = queryStr.IndexOf("ExtField");
            if (index > 0)
            {
                expStr = queryStr.Substring(index, 9);
            }

            StringBuilder SqlStr = new StringBuilder();
            SqlStr.AppendLine(" SELECT  a.ProductID,a.StorageID,isnull(a.HappenCount,0) as ProductCount,CONVERT(VARCHAR(10),a.HappenDate,21) as EnterDate ,a.BillType as typeflag ,a.BillNo,isnull(a.PageUrl,'') as PageUrl, ");
            SqlStr.AppendLine(" isnull(b.ProdNo,'') as ProdNo, ");
            SqlStr.AppendLine(" isnull(b.ProductName,'') as ProductName,isnull(b.Specification,'') as Specification, ");
            SqlStr.AppendLine(" c.StorageName,c.StorageNo,isnull(b.Size,'') as ProductSize,e.CodeName, ");
            SqlStr.AppendLine(" isnull(b.FromAddr,'') as FromAddr,isnull(a.BatchNo,'') as BatchNo,isnull(a.ProductCount,0) as NowProductCount,a.Price,a.Creator,a.ReMark,d.EmployeeName as CreatorName,f.TypeName as ColorName  ");
            if (!string.IsNullOrEmpty(expStr))
            {
                SqlStr.AppendLine(" ,b." + expStr + " ");
            }
            SqlStr.AppendLine(" from officedba.StorageAccount");
            SqlStr.AppendLine("  a left outer join officedba.ProductInfo b ");
            SqlStr.AppendLine(" on a.ProductID=b.ID ");
            SqlStr.AppendLine(" left outer join officedba.StorageInfo c ");
            SqlStr.AppendLine(" on a.StorageID=c.ID  ");
            SqlStr.AppendLine("  left outer join officedba.EmployeeInfo d ");
            SqlStr.AppendLine(" on a.Creator=d.ID left outer join officedba.CodeUnitType e  on b.UnitID=e.ID ");
            SqlStr.AppendLine("  left outer join officedba.CodePublicType f on b.ColorID=f.ID {0} ");
            string sql = string.Format(SqlStr.ToString(), queryStr.Trim().Length > 0 ? " where " + queryStr : "");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }




        /// <summary>
        /// 库存流水账--明细统计信息
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static string GetSumJournal(string queryStr)
        {
            string rev = string.Empty;
            string expStr = string.Empty;
            int index = queryStr.IndexOf("ExtField");
            if (index > 0)
            {
                expStr = queryStr.Substring(index, 9);
            }

            StringBuilder SqlStr = new StringBuilder();
            SqlStr.AppendLine("select sum(ProductCount) as ProductCount,sum(NowProductCount) as NowProductCount from ( SELECT  a.ProductID,a.StorageID,isnull(a.HappenCount,0) as ProductCount,CONVERT(VARCHAR(10),a.HappenDate,21) as EnterDate ,a.BillType as typeflag ,a.BillNo,isnull(a.PageUrl,'') as PageUrl, ");
            SqlStr.AppendLine(" isnull(b.ProdNo,'') as ProdNo, ");
            SqlStr.AppendLine(" isnull(b.ProductName,'') as ProductName,isnull(b.Specification,'') as Specification, ");
            SqlStr.AppendLine(" c.StorageName,c.StorageNo,isnull(b.Size,'') as ProductSize,e.CodeName, ");
            SqlStr.AppendLine(" isnull(b.FromAddr,'') as FromAddr,isnull(a.BatchNo,'') as BatchNo,isnull(a.ProductCount,0) as NowProductCount,a.Price,a.Creator,a.ReMark,d.EmployeeName as CreatorName,f.TypeName as ColorName  ");
            if (!string.IsNullOrEmpty(expStr))
            {
                SqlStr.AppendLine(" ,b." + expStr + " ");
            }
            SqlStr.AppendLine(" from officedba.StorageAccount");
            SqlStr.AppendLine("  a left outer join officedba.ProductInfo b ");
            SqlStr.AppendLine(" on a.ProductID=b.ID ");
            SqlStr.AppendLine(" left outer join officedba.StorageInfo c ");
            SqlStr.AppendLine(" on a.StorageID=c.ID  ");
            SqlStr.AppendLine("  left outer join officedba.EmployeeInfo d ");
            SqlStr.AppendLine(" on a.Creator=d.ID left outer join officedba.CodeUnitType e  on b.UnitID=e.ID ");
            SqlStr.AppendLine("  left outer join officedba.CodePublicType f on b.ColorID=f.ID {0} ) a ");


            string sql = string.Format(SqlStr.ToString(), queryStr.Trim().Length > 0 ? " where " + queryStr : "");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;

            DataTable dt = SqlHelper.ExecuteSearch(comm);

            if (dt.Rows.Count > 0)
            {
                rev = dt.Rows[0]["ProductCount"].ToString() + "|" + dt.Rows[0]["NowProductCount"].ToString();
            }

            return rev;
        }


        /// <summary>
        /// 根据供应商查询入库总量
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="extQueryStr">物品扩展属性查询条件</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetStrongJournalByPro(string queryStr, string extQueryStr, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder SqlStr = new StringBuilder();
            if (extQueryStr.Trim().Length > 0)
            {
                SqlStr.AppendLine(" select  " + extQueryStr.Split('@')[1] + ", a.ProviderID,d.CustName,a.CompanyCD,a.ProductID,a.StorageID,a.ProductCount,typeflag='采购入库单' ");
            }
            else
            {
                SqlStr.AppendLine(" select    a.ProviderID,d.CustName,a.CompanyCD,a.ProductID,a.StorageID,a.ProductCount,typeflag='采购入库单' ");
            }
            SqlStr.AppendLine(" ,isnull(b.ProdNo,'') as ProdNo,a.BatchNo, ");
            SqlStr.AppendLine(" isnull(b.ProductName,'') as ProductName,isnull(b.Specification,'') as Specification, ");
            SqlStr.AppendLine(" isnull(b.Size,'') as ProductSize,isnull(b.FromAddr,'') as FromAddr,f.StorageName,f.StorageNo ");
            SqlStr.AppendLine(" from ( ");
            SqlStr.AppendLine(" select sum(isnull(ProductCount,0)) as ProductCount,ProductID,StorageID,CompanyCD,ProviderID,BatchNo ");
            SqlStr.AppendLine(" from ( select isnull(b.BatchNo,'') as BatchNo,c.ProviderID,a.CompanyCD,b.ProductID,b.StorageID,b.ProductCount,CONVERT(VARCHAR(10), ");
            SqlStr.AppendLine(" a.EnterDate,21) as EnterDate ");
            SqlStr.AppendLine(" from officedba.StorageInPurchase a left outer join officedba.StorageInPurchaseDetail b ");
            SqlStr.AppendLine(" on a.CompanyCD=b.CompanyCD and a.InNo=b.InNo left outer join  officedba.PurchaseArrive c on a.FromBillID=c.ID ");
            SqlStr.AppendLine(" where a.BillStatus='2' and a.EnterDate is not null {0} ) a group by ProductID,StorageID,CompanyCD,ProviderID,BatchNo ");
            SqlStr.AppendLine(" ) a  ");
            SqlStr.AppendLine(" left outer join officedba.ProviderInfo  d on a.ProviderID=d.ID ");
            SqlStr.AppendLine(" left outer join officedba.ProductInfo b on a.ProductID=b.ID ");
            SqlStr.AppendLine(" left outer join officedba.StorageInfo f on a.StorageID=f.ID  {1}");
            string sql = string.Format(SqlStr.ToString(), queryStr.Trim().Length > 0 ? queryStr : "", extQueryStr.Trim().Length > 0 ? " where " + extQueryStr.Split ('@')[0] : "");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);

        }
        /// <summary>
        /// 根据供应商查询库存流水账
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetDetailStrongJournalByPro(string queryStr, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder SqlStr = new StringBuilder();
            SqlStr.AppendLine(" select c.ProviderID,d.CustName,a.CompanyCD,e.ProductID,isnull(e.BatchNo,'') as BatchNo,e.StorageID,e.ProductCount,CONVERT(VARCHAR(10),");
            SqlStr.AppendLine(" a.EnterDate,21) as EnterDate,a.InNo as BillNo, PageUrl='',typeflag='采购入库单',flag='1',isnull(b.ProdNo,'') as ProdNo, ");
            SqlStr.AppendLine(" isnull(b.ProductName,'') as ProductName,isnull(b.Specification,'') as Specification, ");
            SqlStr.AppendLine(" isnull(b.Size,'') as ProductSize,isnull(b.FromAddr,'') as FromAddr,f.StorageName,f.StorageNo,g.TypeName as ColorName   ");
            SqlStr.AppendLine(" from officedba.StorageInPurchase a left outer join officedba.StorageInPurchaseDetail e ");
            SqlStr.AppendLine(" on a.CompanyCD=e.CompanyCD and a.InNo=e.InNo left outer join  officedba.PurchaseArrive c on a.FromBillID=c.ID ");
            SqlStr.AppendLine(" left outer join officedba.ProviderInfo  d on c.ProviderID=d.ID ");
            SqlStr.AppendLine(" left outer join officedba.ProductInfo b on e.ProductID=b.ID ");
            SqlStr.AppendLine(" left outer join officedba.StorageInfo f on b.StorageID=f.ID   left outer join officedba.CodePublicType g on b.ColorID=g.ID ");
            SqlStr.AppendLine(" where a.BillStatus='2' {0} ");
            string sql = string.Format(SqlStr.ToString(), queryStr.Trim().Length > 0 ?  queryStr : "");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);

        }




        /// <summary>
        /// 分店库存流水账--明细信息
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSubStrongJournalDetail(string queryStr, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder SqlStr = new StringBuilder();

            string expStr = string.Empty;
            int index = queryStr.IndexOf("ExtField");
            if (index > 0)
            {
                expStr = queryStr.Substring(index, 9);
            }

            SqlStr.AppendLine(" SELECT a.ProductID,a.DeptID as StorageID,isnull(a.HappenCount,0) as ProductCount,CONVERT(VARCHAR(10),a.HappenDate,21) as EnterDate ,a.BillType as typeflag ,a.BillNo,isnull(a.PageUrl,'') as PageUrl, ");
            SqlStr.AppendLine("case when a.BillType=1 then '期初库存录入' when a.BillType=2 then '配送单（配送入库）' when a.BillType=3 then '销售单' when a.BillType=4 then '销售退货单' when a.BillType=5 then '门店调拨出库'  when a.BillType=6 then '门店调拨入库' when a.BillType=7 then '配送退货单(退货出库)' end as strTypeFlag,");
            SqlStr.AppendLine(" isnull(b.ProdNo,'') as ProdNo, ");
            SqlStr.AppendLine(" isnull(b.ProductName,'') as ProductName,isnull(b.Specification,'') as Specification, ");
            SqlStr.AppendLine(" c.DeptName as StorageName,StorageNo='',isnull(b.Size,'') as ProductSize, ");
            SqlStr.AppendLine(" isnull(b.FromAddr,'') as FromAddr,isnull(a.BatchNo,'') as BatchNo,isnull(a.ProductCount,0) as NowProductCount,a.Price,a.Creator,a.ReMark,d.EmployeeName as CreatorName,g.TypeName as ColorName  ");
            if (!string.IsNullOrEmpty(expStr))
            {
                SqlStr.AppendLine(" ,b." + expStr + " ");
            }
            SqlStr.AppendLine(" from officedba.SubStorageAccount");
            SqlStr.AppendLine("  a left outer join officedba.ProductInfo b ");
            SqlStr.AppendLine(" on a.ProductID=b.ID ");
            SqlStr.AppendLine(" left outer join officedba.DeptInfo c ");
            SqlStr.AppendLine(" on a.DeptID=c.ID  ");
            SqlStr.AppendLine("  left outer join officedba.EmployeeInfo d ");
            SqlStr.AppendLine(" on a.Creator=d.ID  left outer join officedba.CodePublicType g on b.ColorID=g.ID  {0} ");
            string sql = string.Format(SqlStr.ToString(), queryStr.Trim().Length > 0 ? " where " + queryStr : "");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }

    }
}

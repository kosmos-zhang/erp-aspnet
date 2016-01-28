/**********************************************
 * 类作用：   票据数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/04/14
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
namespace XBase.Data.Office.FinanceManager
{
   public  class BillingDBHelper
   {
       #region 检索销售退货单信息
       public static DataTable SellBackInfo(string CompanyCD, string BackNo, string Title,
            string CustName, string StartDate, string EndDate)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.BackNo as OrderNO,a.Title,CASE WHEN a.CreateDate IS NOT NULL THEN CONVERT(varchar(10), a.CreateDate, 120) WHEN a.CreateDate IS NULL ");
           sql.AppendLine("tHEN '' END AS CreateDate,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.Rate,1) as Rate,");
           sql.AppendLine("CASE WHEN e.CurrencyName IS NOT NULL THEN e.CurrencyName WHEN e.CurrencyName IS NULL THEN '' END AS CurrencyName, ");
           sql.AppendLine("CASE WHEN d.CustName IS NOT NULL THEN d.CustName WHEN d.CustName IS NULL THEN '' END AS CustName, ");
           sql.AppendLine("CASE WHEN a.isOpenbill = '0' THEN '未建单' WHEN a.isOpenbill = '1' THEN '已建单' END AS isOpenbill, CASE WHEN CAST(a.RealTotal AS varchar) ");
           sql.AppendLine("IS NULL THEN '' WHEN a.RealTotal IS NOT NULL THEN CAST(RealTotal AS varchar) END AS RealTotal,");
           sql.AppendLine(" CASE WHEN b.TypeName IS NOT NULL THEN b.TypeName WHEN b.TypeName IS NULL THEN '' END AS PlayWay,");
           sql.AppendLine("CASE WHEN c.TypeName IS NOT NULL THEN c.TypeName WHEN c.TypeName IS NULL THEN '' END AS MoneyWay,");
           sql.AppendLine("CASE WHEN a.ID IS NOT NULL THEN 'officedba.CustInfo' END AS FromTBName, CASE WHEN a.ID IS NOT NULL THEN 'CustName' END AS FileName, a.CustID, ");

           sql.AppendLine(" (select case when sum(isnull(Innumber,0))>0 then '已入库'   ");
           sql.AppendLine("when sum(isnull(Innumber,0))=0 then '未入库' end as StockStatus   from officedba.SellBackDetail ");
           sql.AppendLine("where CompanyCD=@CompanyCD and BackNo=a.BackNo)  as  Status");
         //  sql.AppendLine(" case when a.StockStatus is null then '' when  a.StockStatus='1' then '已入库' when a.StockStatus='2' then '未入库'");
           sql.AppendLine("from officedba.SellBack as a");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS b ON a.PayType = b.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS c ON a.MoneyType = c.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CustInfo AS d ON d.ID = a.CustID LEFT OUTER JOIN");
           sql.AppendLine("officedba.CurrencyTypeSetting AS e ON e.ID = a.CurrencyType where a.CompanyCD=@CompanyCD and a.BillStatus='2'");
           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           //订单编号
           if (!string.IsNullOrEmpty(BackNo))
           {
               sql.AppendLine(" AND a.BackNo LIKE @BackNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@BackNo", "%" + BackNo + "%"));
           }
           //主题
           if (!string.IsNullOrEmpty(Title))
           {
               sql.AppendLine(" AND a.Title LIKE @Title ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + Title + "%"));
           }
           //客户名称
           if (!string.IsNullOrEmpty(CustName))
           {
               sql.AppendLine(" AND a.CustID = @CustName ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", CustName));
           }
           //开始和结束时间
           if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
           {
               sql.AppendLine(" AND CreateDate BetWeen  @StartDate and @EndDate ");
               comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
               comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
           }
           //指定命令的SQL文
           comm.CommandText = sql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       #endregion

       #region 检索销售退货单信息 add by moshenlin
       public static DataTable SellBackInfo(string CompanyCD, string BackNo, string Title,
            string CustName, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.BackNo as OrderNO,a.Title,CASE WHEN a.CreateDate IS NOT NULL THEN CONVERT(varchar(10), a.CreateDate, 120) WHEN a.CreateDate IS NULL ");
           sql.AppendLine("tHEN '' END AS CreateDate,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.Rate,1) as Rate,");
           sql.AppendLine("CASE WHEN e.CurrencyName IS NOT NULL THEN e.CurrencyName WHEN e.CurrencyName IS NULL THEN '' END AS CurrencyName, ");
           sql.AppendLine("CASE WHEN d.CustName IS NOT NULL THEN d.CustName WHEN d.CustName IS NULL THEN '' END AS CustName, ");
           sql.AppendLine("CASE WHEN a.isOpenbill = '0' THEN '未建单' WHEN a.isOpenbill = '1' THEN '已建单' END AS isOpenbill, CASE WHEN CAST(a.RealTotal AS varchar) ");
           sql.AppendLine("IS NULL THEN '' WHEN a.RealTotal IS NOT NULL THEN CAST(RealTotal AS varchar) END AS RealTotal,");
           sql.AppendLine(" CASE WHEN b.TypeName IS NOT NULL THEN b.TypeName WHEN b.TypeName IS NULL THEN '' END AS PlayWay,");
           sql.AppendLine("CASE WHEN c.TypeName IS NOT NULL THEN c.TypeName WHEN c.TypeName IS NULL THEN '' END AS MoneyWay,");
           sql.AppendLine("CASE WHEN a.ID IS NOT NULL THEN 'officedba.CustInfo' END AS FromTBName, CASE WHEN a.ID IS NOT NULL THEN 'CustName' END AS FileName, a.CustID, ");

           sql.AppendLine(" (select case when sum(isnull(Innumber,0))>0 then '已入库'   ");
           sql.AppendLine("when sum(isnull(Innumber,0))=0 then '未入库' end as StockStatus   from officedba.SellBackDetail ");
           sql.AppendLine("where CompanyCD=@CompanyCD and BackNo=a.BackNo)  as  Status");
           //  sql.AppendLine(" case when a.StockStatus is null then '' when  a.StockStatus='1' then '已入库' when a.StockStatus='2' then '未入库'");
           sql.AppendLine("from officedba.SellBack as a");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS b ON a.PayType = b.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS c ON a.MoneyType = c.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CustInfo AS d ON d.ID = a.CustID LEFT OUTER JOIN");
           sql.AppendLine("officedba.CurrencyTypeSetting AS e ON e.ID = a.CurrencyType where a.CompanyCD=@CompanyCD and a.BillStatus='2'");
           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           //订单编号
           if (!string.IsNullOrEmpty(BackNo))
           {
               sql.AppendLine(" AND a.BackNo LIKE @BackNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@BackNo", "%" + BackNo + "%"));
           }
           //主题
           if (!string.IsNullOrEmpty(Title))
           {
               sql.AppendLine(" AND a.Title LIKE @Title ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + Title + "%"));
           }
           //客户名称
           if (!string.IsNullOrEmpty(CustName))
           {
               sql.AppendLine(" AND a.CustID = @CustName ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", CustName));
           }
           //开始和结束时间
           if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
           {
               sql.AppendLine(" AND CreateDate BetWeen  @StartDate and @EndDate ");
               comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
               comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
           }
           //指定命令的SQL文
           comm.CommandText = sql.ToString();
           //执行查询
           return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);
       }
       #endregion

       #region 检索销售退货单信息
       /// <summary>
       /// 根据检索条件检索出满足条件的信息 Added By moshenlin 2009-06-29
       /// </summary>
       /// <param name="ids"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable SellBackInfo(string ids, string CompanyCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.BackNo,a.Title,CASE WHEN a.CreateDate IS NOT NULL THEN CONVERT(varchar(10), a.CreateDate, 120) WHEN a.CreateDate IS NULL ");
           sql.AppendLine("then '' END AS CreateDate,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.Rate,1) as Rate,");
           sql.AppendLine("CASE WHEN e.CurrencyName IS NOT NULL THEN e.CurrencyName WHEN e.CurrencyName IS NULL THEN '' END AS CurrencyName, ");
           sql.AppendLine("CASE WHEN d.CustName IS NOT NULL THEN d.CustName WHEN d.CustName IS NULL THEN '' END AS CustName, ");
           sql.AppendLine("CASE WHEN a.isOpenbill = '0' THEN '未建单' WHEN a.isOpenbill = '1' THEN '已建单' END AS isOpenbill, CASE WHEN CAST(a.RealTotal AS varchar) ");
           sql.AppendLine("IS NULL THEN '' WHEN a.RealTotal IS NOT NULL THEN CAST(RealTotal AS varchar) END AS RealTotal,");
           sql.AppendLine(" CASE WHEN b.TypeName IS NOT NULL THEN b.TypeName WHEN b.TypeName IS NULL THEN '' END AS PlayWay,");
           sql.AppendLine("CASE WHEN c.TypeName IS NOT NULL THEN c.TypeName WHEN c.TypeName IS NULL THEN '' END AS MoneyWay,");
           sql.AppendLine("CASE WHEN a.ID IS NOT NULL THEN 'officedba.CustInfo' END AS FromTBName, CASE WHEN a.ID IS NOT NULL THEN 'CustName' END AS FileName, a.CustID, ");
           sql.AppendLine(" (select case when sum(isnull(Innumber,0))>0 then '已入库'   ");
           sql.AppendLine("when sum(isnull(Innumber,0))=0 then '未入库' end as StockStatus   from officedba.SellBackDetail ");
           sql.AppendLine("where CompanyCD=@CompanyCD and BackNo=a.BackNo)  as  StockStatus");
           //  sql.AppendLine(" case when a.StockStatus is null then '' when  a.StockStatus='1' then '已入库' when a.StockStatus='2' then '未入库'");
           sql.AppendLine("from officedba.SellBack as a");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS b ON a.PayType = b.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS c ON a.MoneyType = c.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CustInfo AS d ON d.ID = a.CustID LEFT OUTER JOIN");
           sql.AppendLine("officedba.CurrencyTypeSetting AS e ON e.ID = a.CurrencyType where a.CompanyCD=@CompanyCD and a.BillStatus='2' and a.ID in ( " + ids + " )  order by a.CreateDate asc ");
           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           
           //指定命令的SQL文
           comm.CommandText = sql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       #endregion

       #region 检索采购退货单信息
       public static DataTable GetPurchaseRejectInfo(string CompanyCD, string RejectNo, string Title,
            string CustName, string StartDate, string EndDate)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.RejectNo as OrderNO,a.Title,");
           sql.AppendLine("CASE WHEN d .CustName IS NOT NULL THEN d.CustName WHEN d .CustName IS NULL THEN '' END AS CustName,");
           sql.AppendLine("CASE WHEN CAST(a.RealTotal AS varchar) ");
           sql.AppendLine("IS NULL THEN '' WHEN a.RealTotal IS NOT NULL THEN CAST(RealTotal AS varchar) END AS RealTotal");
           sql.AppendLine(",CASE WHEN a.CreateDate IS NOT NULL THEN CONVERT(varchar(10), a.CreateDate, 120) WHEN a.CreateDate IS NULL ");
           sql.AppendLine(" THEN '' END AS CreateDate,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.Rate,1) as Rate,");
           sql.AppendLine("CASE WHEN a.isOpenbill = '0' THEN '未建单' WHEN a.isOpenbill = '1' THEN '已建单' END AS isOpenbill,");
           sql.AppendLine(" CASE WHEN b.TypeName IS NOT NULL THEN b.TypeName WHEN b.TypeName IS NULL THEN '' END AS PlayWay,");
           sql.AppendLine(" CASE WHEN c.TypeName IS NOT NULL THEN c.TypeName WHEN c.TypeName IS NULL THEN '' END AS MoneyWay,");
           sql.AppendLine("CASE WHEN a.ID IS NOT NULL THEN 'officedba.ProviderInfo' END AS FromTBName, CASE WHEN a.ID IS NOT NULL THEN 'CustName' END AS FileName, a.ProviderID as CustID, ");

           sql.AppendLine("CASE WHEN e.CurrencyName IS NOT NULL THEN e.CurrencyName WHEN e.CurrencyName IS NULL THEN '' END AS CurrencyName");
           sql.AppendLine(",(SELECT     CASE WHEN SUM(OutedTotal) IS NULL THEN '未退货' WHEN CAST(SUM(OutedTotal) AS int) ");
           sql.AppendLine("        > 0 THEN '已退货' WHEN CAST(SUM(OutedTotal) AS int) = 0 THEN '未退货' END AS a");
           sql.AppendLine("       FROM          officedba.PurchaseRejectDetail AS b");
           sql.AppendLine(" WHERE      (CompanyCD = a.CompanyCD) AND (RejectNo = a.RejectNo)) AS Status ");
           sql.AppendLine(" from officedba.PurchaseReject as a");
           sql.AppendLine("LEFT OUTER JOIN");
           sql.AppendLine("officedba.CodePublicType AS b ON a.PayType = b.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS c ON a.MoneyType = c.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CurrencyTypeSetting AS e ON e.ID = a.CurrencyType");
           sql.AppendLine("LEFT OUTER JOIN officedba.ProviderInfo AS d ON d.ID = a.ProviderID");
           sql.AppendLine("where a.CompanyCD=@CompanyCD and a.billStatus='2'");

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           //订单编号
           if (!string.IsNullOrEmpty(RejectNo))
           {
               sql.AppendLine(" AND a.RejectNo LIKE @RejectNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@RejectNo", "%" + RejectNo + "%"));
           }
           //主题
           if (!string.IsNullOrEmpty(Title))
           {
               sql.AppendLine(" AND a.Title LIKE @Title ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + Title + "%"));
           }
           //客户名称
           if (!string.IsNullOrEmpty(CustName))
           {
               sql.AppendLine(" AND a.ProviderID = @CustName ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", CustName));
           }
           //开始和结束时间
           if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
           {
               sql.AppendLine(" AND a.CreateDate BetWeen  @StartDate and @EndDate ");
               comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
               comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
           }
           //指定命令的SQL文
           comm.CommandText = sql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
      

       }
       #endregion

       #region 检索采购退货单信息  add by moshenlin 
       public static DataTable GetPurchaseRejectInfo(string CompanyCD, string RejectNo, string Title,
            string CustName, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.RejectNo as OrderNO,a.Title,");
           sql.AppendLine("CASE WHEN d .CustName IS NOT NULL THEN d.CustName WHEN d .CustName IS NULL THEN '' END AS CustName,");
           sql.AppendLine("CASE WHEN CAST(a.RealTotal AS varchar) ");
           sql.AppendLine("IS NULL THEN '' WHEN a.RealTotal IS NOT NULL THEN CAST(RealTotal AS varchar) END AS RealTotal");
           sql.AppendLine(",CASE WHEN a.CreateDate IS NOT NULL THEN CONVERT(varchar(10), a.CreateDate, 120) WHEN a.CreateDate IS NULL ");
           sql.AppendLine(" THEN '' END AS CreateDate,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.Rate,1) as Rate,");
           sql.AppendLine("CASE WHEN a.isOpenbill = '0' THEN '未建单' WHEN a.isOpenbill = '1' THEN '已建单' END AS isOpenbill,");
           sql.AppendLine(" CASE WHEN b.TypeName IS NOT NULL THEN b.TypeName WHEN b.TypeName IS NULL THEN '' END AS PlayWay,");
           sql.AppendLine(" CASE WHEN c.TypeName IS NOT NULL THEN c.TypeName WHEN c.TypeName IS NULL THEN '' END AS MoneyWay,");
           sql.AppendLine("CASE WHEN a.ID IS NOT NULL THEN 'officedba.ProviderInfo' END AS FromTBName, CASE WHEN a.ID IS NOT NULL THEN 'CustName' END AS FileName, a.ProviderID as CustID, ");

           sql.AppendLine("CASE WHEN e.CurrencyName IS NOT NULL THEN e.CurrencyName WHEN e.CurrencyName IS NULL THEN '' END AS CurrencyName");
           sql.AppendLine(",(SELECT     CASE WHEN SUM(OutedTotal) IS NULL THEN '未退货' WHEN CAST(SUM(OutedTotal) AS int) ");
           sql.AppendLine("        > 0 THEN '已退货' WHEN CAST(SUM(OutedTotal) AS int) = 0 THEN '未退货' END AS a");
           sql.AppendLine("       FROM          officedba.PurchaseRejectDetail AS b");
           sql.AppendLine(" WHERE      (CompanyCD = a.CompanyCD) AND (RejectNo = a.RejectNo)) AS Status ");
           sql.AppendLine(" from officedba.PurchaseReject as a");
           sql.AppendLine("LEFT OUTER JOIN");
           sql.AppendLine("officedba.CodePublicType AS b ON a.PayType = b.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS c ON a.MoneyType = c.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CurrencyTypeSetting AS e ON e.ID = a.CurrencyType");
           sql.AppendLine("LEFT OUTER JOIN officedba.ProviderInfo AS d ON d.ID = a.ProviderID");
           sql.AppendLine("where a.CompanyCD=@CompanyCD and a.billStatus='2'");

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           //订单编号
           if (!string.IsNullOrEmpty(RejectNo))
           {
               sql.AppendLine(" AND a.RejectNo LIKE @RejectNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@RejectNo", "%" + RejectNo + "%"));
           }
           //主题
           if (!string.IsNullOrEmpty(Title))
           {
               sql.AppendLine(" AND a.Title LIKE @Title ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + Title + "%"));
           }
           //客户名称
           if (!string.IsNullOrEmpty(CustName))
           {
               sql.AppendLine(" AND a.ProviderID = @CustName ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", CustName));
           }
           //开始和结束时间
           if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
           {
               sql.AppendLine(" AND a.CreateDate BetWeen  @StartDate and @EndDate ");
               comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
               comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
           }
           //指定命令的SQL文
           comm.CommandText = sql.ToString();
           //执行查询
           return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);


       }
       #endregion

       #region 更新销售退货单建单状态
       public static bool UpdateSellBackIsOpen(string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update officedba.SellBack set isOpenbill='1'");
           sql.AppendLine("where ID In( " + ID + ") ");
           SqlHelper.ExecuteTransSql(sql.ToString());
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 更新代销结算单建单状态
       public static bool UpdateSellChannelSttlIsOpen(string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update officedba.SellChannelSttl set isOpenbill='1'");
           sql.AppendLine("where ID In( " + ID + ") ");
           SqlHelper.ExecuteTransSql(sql.ToString());
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 更新采购退货单单建单状态
       public static bool UpdatePurchaseRejectIsOpen(string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update officedba.PurchaseReject set isOpenbill='1'");
           sql.AppendLine("where ID In( " + ID + ") ");
           SqlHelper.ExecuteTransSql(sql.ToString());
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 检索采购退货单信息
       /// <summary>
       /// 根据检索条件检索出满足条件的信息 Added By moshenlin 2009-06-29
       /// </summary>
       /// <param name="ids"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable GetPurchaseRejectInfo(string ids,string CompanyCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.RejectNo as OrderNO,a.Title,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.Rate,1) as Rate,");
           sql.AppendLine("CASE WHEN d .CustName IS NOT NULL THEN d.CustName WHEN d .CustName IS NULL THEN '' END AS CustName,");
           sql.AppendLine("CASE WHEN CAST(a.RealTotal AS varchar) ");
           sql.AppendLine("IS NULL THEN '' WHEN a.RealTotal IS NOT NULL THEN CAST(RealTotal AS varchar) END AS RealTotal");
           sql.AppendLine(",CASE WHEN a.CreateDate IS NOT NULL THEN CONVERT(varchar(10), a.CreateDate, 120) WHEN a.CreateDate IS NULL ");
           sql.AppendLine(" THEN '' END AS CreateDate,");
           sql.AppendLine("CASE WHEN a.isOpenbill = '0' THEN '未建单' WHEN a.isOpenbill = '1' THEN '已建单' END AS isOpenbill,");
           sql.AppendLine(" CASE WHEN b.TypeName IS NOT NULL THEN b.TypeName WHEN b.TypeName IS NULL THEN '' END AS PlayWay,");
           sql.AppendLine(" CASE WHEN c.TypeName IS NOT NULL THEN c.TypeName WHEN c.TypeName IS NULL THEN '' END AS MoneyWay,");
           sql.AppendLine("CASE WHEN a.ID IS NOT NULL THEN 'officedba.ProviderInfo' END AS FromTBName, CASE WHEN a.ID IS NOT NULL THEN 'CustName' END AS FileName, a.ProviderID as CustID, ");
           sql.AppendLine("CASE WHEN e.CurrencyName IS NOT NULL THEN e.CurrencyName WHEN e.CurrencyName IS NULL THEN '' END AS CurrencyName");
           sql.AppendLine(",(SELECT     CASE WHEN SUM(OutedTotal) IS NULL THEN '未退货' WHEN CAST(SUM(OutedTotal) AS int) ");
           sql.AppendLine("        > 0 THEN '已退货' WHEN CAST(SUM(OutedTotal) AS int) = 0 THEN '未退货' END AS a");
           sql.AppendLine("       FROM          officedba.PurchaseRejectDetail AS b");
           sql.AppendLine(" WHERE      (CompanyCD = a.CompanyCD) AND (RejectNo = a.RejectNo)) AS OutStatus ");
           sql.AppendLine(" from officedba.PurchaseReject as a");
           sql.AppendLine("LEFT OUTER JOIN");
           sql.AppendLine("officedba.CodePublicType AS b ON a.PayType = b.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS c ON a.MoneyType = c.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CurrencyTypeSetting AS e ON e.ID = a.CurrencyType");
           sql.AppendLine("LEFT OUTER JOIN officedba.ProviderInfo AS d ON d.ID = a.ProviderID");
           sql.AppendLine("where a.CompanyCD=@CompanyCD and a.billStatus='2' and a.ID in ( " + ids + " ) order by a.CreateDate asc ");

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
          
           //指定命令的SQL文
           comm.CommandText = sql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);


       }
       #endregion

       #region 检索代销结算单
       public static DataTable GetSellChannelSttlInfo(string CompanyCD, string SttlNo, string Title,
            string CustName, string StartDate, string EndDate)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.SttlNo as OrderNO,a.Title,");
           sql.AppendLine("CASE WHEN CAST(a.SttlTotal AS varchar) ");
           sql.AppendLine("IS NULL THEN '' WHEN a.SttlTotal IS NOT NULL THEN CAST(a.SttlTotal AS varchar)");
           sql.AppendLine("END AS RealTotal");
           sql.AppendLine(",CASE WHEN a.CreateDate IS NOT NULL THEN CONVERT(varchar(10), a.CreateDate, 120) ");
           sql.AppendLine("WHEN a.CreateDate IS NULL ");
           sql.AppendLine(" tHEN '' END AS CreateDate,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.Rate,1) as Rate,");
           sql.AppendLine("CASE WHEN d .CustName IS NOT NULL THEN d .CustName WHEN d .CustName IS NULL THEN '' END AS CustName,");
           sql.AppendLine("CASE WHEN a.isOpenbill = '0' THEN '未建单' WHEN a.isOpenbill = '1' THEN '已建单' END AS isOpenbill, ");
           sql.AppendLine(" CASE WHEN b.TypeName IS NOT NULL THEN b.TypeName WHEN b.TypeName IS NULL THEN '' END AS PlayWay,");
           sql.AppendLine("CASE WHEN c.TypeName IS NOT NULL THEN c.TypeName WHEN c.TypeName IS NULL THEN '' END AS MoneyWay,");
           sql.AppendLine("CASE WHEN a.ID IS NOT NULL THEN 'officedba.CustInfo' END AS FromTBName, CASE WHEN a.ID IS NOT NULL THEN 'CustName' END AS FileName, a.CustID, ");

           sql.AppendLine("CASE WHEN e.CurrencyName IS NOT NULL THEN e.CurrencyName WHEN e.CurrencyName IS NULL THEN '' ");
           sql.AppendLine("END AS CurrencyName,");
           sql.AppendLine("(SELECT     CASE WHEN SUM(totalPrice) IS NULL THEN '未结算' ");
           sql.AppendLine("WHEN CAST(SUM(totalPrice) AS int)  != cast(a.SttlTotal as int) THEN '结算中'");
           sql.AppendLine("WHEN CAST(SUM(totalPrice) AS int) = cast(a.SttlTotal as int) THEN '已结算' END AS a  ");
           sql.AppendLine("FROM  officedba.SellChannelSttlDetail AS b");
           sql.AppendLine(" WHERE (CompanyCD = a.CompanyCD) AND (SttlNo = a.SttlNo)) AS Status");
           sql.AppendLine("from officedba.SellChannelSttl as a");
           sql.AppendLine("LEFT OUTER JOIN");
           sql.AppendLine("officedba.CodePublicType AS b ON a.PayType = b.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS c ON a.MoneyType = c.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CurrencyTypeSetting AS e ON e.ID = a.CurrencyType");
           sql.AppendLine("LEFT OUTER JOIN officedba.CustInfo AS d ON d.ID = a.CustID  ");
           sql.AppendLine("where a.CompanyCD=@CompanyCD and a.billStatus='2'");
           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           //订单编号
           if (!string.IsNullOrEmpty(SttlNo))
           {
               sql.AppendLine(" AND a.SttlNo LIKE @SttlNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@SttlNo", "%" + SttlNo + "%"));
           }
           //主题
           if (!string.IsNullOrEmpty(Title))
           {
               sql.AppendLine(" AND a.Title LIKE @Title ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + Title + "%"));
           }
           //客户名称
           if (!string.IsNullOrEmpty(CustName))
           {
               sql.AppendLine(" AND a.CustID = @CustName ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", CustName));
           }
           //开始和结束时间
           if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
           {
               sql.AppendLine(" AND a.CreateDate BetWeen  @StartDate and @EndDate ");
               comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
               comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
           }
           //指定命令的SQL文
           comm.CommandText = sql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);

       }
       #endregion 

       #region 检索代销结算单  add by moshenlin
       public static DataTable GetSellChannelSttlInfo(string CompanyCD, string SttlNo, string Title,
            string CustName, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.SttlNo as OrderNO,a.Title,");
           sql.AppendLine("CASE WHEN CAST(a.SttlTotal AS varchar) ");
           sql.AppendLine("IS NULL THEN '' WHEN a.SttlTotal IS NOT NULL THEN CAST(a.SttlTotal AS varchar)");
           sql.AppendLine("END AS RealTotal");
           sql.AppendLine(",CASE WHEN a.CreateDate IS NOT NULL THEN CONVERT(varchar(10), a.CreateDate, 120) ");
           sql.AppendLine("WHEN a.CreateDate IS NULL ");
           sql.AppendLine(" tHEN '' END AS CreateDate,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.Rate,1) as Rate,");
           sql.AppendLine("CASE WHEN d .CustName IS NOT NULL THEN d .CustName WHEN d .CustName IS NULL THEN '' END AS CustName,");
           sql.AppendLine("CASE WHEN a.isOpenbill = '0' THEN '未建单' WHEN a.isOpenbill = '1' THEN '已建单' END AS isOpenbill, ");
           sql.AppendLine(" CASE WHEN b.TypeName IS NOT NULL THEN b.TypeName WHEN b.TypeName IS NULL THEN '' END AS PlayWay,");
           sql.AppendLine("CASE WHEN c.TypeName IS NOT NULL THEN c.TypeName WHEN c.TypeName IS NULL THEN '' END AS MoneyWay,");
           sql.AppendLine("CASE WHEN a.ID IS NOT NULL THEN 'officedba.CustInfo' END AS FromTBName, CASE WHEN a.ID IS NOT NULL THEN 'CustName' END AS FileName, a.CustID, ");

           sql.AppendLine("CASE WHEN e.CurrencyName IS NOT NULL THEN e.CurrencyName WHEN e.CurrencyName IS NULL THEN '' ");
           sql.AppendLine("END AS CurrencyName,");
           sql.AppendLine("(SELECT     CASE WHEN SUM(totalPrice) IS NULL THEN '未结算' ");
           sql.AppendLine("WHEN CAST(SUM(totalPrice) AS int)  != cast(a.SttlTotal as int) THEN '结算中'");
           sql.AppendLine("WHEN CAST(SUM(totalPrice) AS int) = cast(a.SttlTotal as int) THEN '已结算' END AS a  ");
           sql.AppendLine("FROM  officedba.SellChannelSttlDetail AS b");
           sql.AppendLine(" WHERE (CompanyCD = a.CompanyCD) AND (SttlNo = a.SttlNo)) AS Status");
           sql.AppendLine("from officedba.SellChannelSttl as a");
           sql.AppendLine("LEFT OUTER JOIN");
           sql.AppendLine("officedba.CodePublicType AS b ON a.PayType = b.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS c ON a.MoneyType = c.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CurrencyTypeSetting AS e ON e.ID = a.CurrencyType");
           sql.AppendLine("LEFT OUTER JOIN officedba.CustInfo AS d ON d.ID = a.CustID  ");
           sql.AppendLine("where a.CompanyCD=@CompanyCD and a.billStatus='2'");
           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           //订单编号
           if (!string.IsNullOrEmpty(SttlNo))
           {
               sql.AppendLine(" AND a.SttlNo LIKE @SttlNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@SttlNo", "%" + SttlNo + "%"));
           }
           //主题
           if (!string.IsNullOrEmpty(Title))
           {
               sql.AppendLine(" AND a.Title LIKE @Title ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + Title + "%"));
           }
           //客户名称
           if (!string.IsNullOrEmpty(CustName))
           {
               sql.AppendLine(" AND a.CustID = @CustName ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", CustName));
           }
           //开始和结束时间
           if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
           {
               sql.AppendLine(" AND a.CreateDate BetWeen  @StartDate and @EndDate ");
               comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
               comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
           }
           //指定命令的SQL文
           comm.CommandText = sql.ToString();
           //执行查询
           return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);

       }
       #endregion 

       #region 检索代销结算单
       /// <summary>
       /// 根据检索条件检索出满足条件的信息 Added By moshenlin 2009-06-29
       /// </summary>
       /// <param name="ids"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable GetSellChannelSttlInfo(string ids, string CompanyCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.SttlNo,a.Title,");
           sql.AppendLine("CASE WHEN CAST(a.SttlTotal AS varchar) ");
           sql.AppendLine("IS NULL THEN '' WHEN a.SttlTotal IS NOT NULL THEN CAST(a.SttlTotal AS varchar)");
           sql.AppendLine("END AS SttlTotal");
           sql.AppendLine(",CASE WHEN a.CreateDate IS NOT NULL THEN CONVERT(varchar(10), a.CreateDate, 120) ");
           sql.AppendLine("WHEN a.CreateDate IS NULL ");
           sql.AppendLine(" tHEN '' END AS CreateDate,");
           sql.AppendLine("CASE WHEN d .CustName IS NOT NULL THEN d .CustName WHEN d .CustName IS NULL THEN '' END AS CustName,");
           sql.AppendLine("CASE WHEN a.isOpenbill = '0' THEN '未建单' WHEN a.isOpenbill = '1' THEN '已建单' END AS isOpenbill, ");
           sql.AppendLine(" CASE WHEN b.TypeName IS NOT NULL THEN b.TypeName WHEN b.TypeName IS NULL THEN '' END AS PlayWay,");

           sql.AppendLine("CASE WHEN a.ID IS NOT NULL THEN 'officedba.CustInfo' END AS FromTBName, CASE WHEN a.ID IS NOT NULL THEN 'CustName' END AS FileName, a.CustID, ");

           sql.AppendLine("CASE WHEN c.TypeName IS NOT NULL THEN c.TypeName WHEN c.TypeName IS NULL THEN '' END AS MoneyWay,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.Rate,1) as Rate,");
           sql.AppendLine("CASE WHEN e.CurrencyName IS NOT NULL THEN e.CurrencyName WHEN e.CurrencyName IS NULL THEN '' ");
           sql.AppendLine("END AS CurrencyName,");
           sql.AppendLine("(SELECT     CASE WHEN SUM(totalPrice) IS NULL THEN '未结算' ");
           sql.AppendLine("WHEN CAST(SUM(totalPrice) AS int)  != cast(a.SttlTotal as int) THEN '结算中'");
           sql.AppendLine("WHEN CAST(SUM(totalPrice) AS int) = cast(a.SttlTotal as int) THEN '已结算' END AS a  ");
           sql.AppendLine("FROM  officedba.SellChannelSttlDetail AS b");
           sql.AppendLine(" WHERE (CompanyCD = a.CompanyCD) AND (SttlNo = a.SttlNo)) AS PayStatus");
           sql.AppendLine("from officedba.SellChannelSttl as a");
           sql.AppendLine("LEFT OUTER JOIN");
           sql.AppendLine("officedba.CodePublicType AS b ON a.PayType = b.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS c ON a.MoneyType = c.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CurrencyTypeSetting AS e ON e.ID = a.CurrencyType");
           sql.AppendLine("LEFT OUTER JOIN officedba.CustInfo AS d ON d.ID = a.CustID  ");
           sql.AppendLine("where a.CompanyCD=@CompanyCD and a.billStatus='2' and a.ID in ( " + ids + " ) order by a.CreateDate asc ");
           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           
           //指定命令的SQL文
           comm.CommandText = sql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);

       }
       #endregion

       #region 检索采购到货通知单  add by moshenlin 2010-06-21
       public static DataTable GetPurchaseIncomeInfo(string CompanyCD, string RejectNo, string Title,
            string CustName, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
       { 
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.ArriveNo as OrderNO,a.Title,");
           sql.AppendLine("CASE WHEN d .CustName IS NOT NULL THEN d.CustName WHEN d .CustName IS NULL THEN '' END AS CustName,");
           sql.AppendLine("CASE WHEN CAST(a.RealTotal AS varchar) ");
           sql.AppendLine("IS NULL THEN '' WHEN a.RealTotal IS NOT NULL THEN CAST(RealTotal AS varchar) END AS RealTotal");
           sql.AppendLine(",CASE WHEN a.CreateDate IS NOT NULL THEN CONVERT(varchar(10), a.CreateDate, 120) WHEN a.CreateDate IS NULL ");
           sql.AppendLine(" THEN '' END AS CreateDate,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.Rate,1) as Rate,");
           sql.AppendLine("CASE WHEN a.isOpenbill = '0' THEN '未建单' WHEN a.isOpenbill = '1' THEN '已建单' END AS isOpenbill,");
           sql.AppendLine(" CASE WHEN b.TypeName IS NOT NULL THEN b.TypeName WHEN b.TypeName IS NULL THEN '' END AS PlayWay,");
           sql.AppendLine(" CASE WHEN c.TypeName IS NOT NULL THEN c.TypeName WHEN c.TypeName IS NULL THEN '' END AS MoneyWay,");
           sql.AppendLine("CASE WHEN a.ID IS NOT NULL THEN 'officedba.ProviderInfo' END AS FromTBName, CASE WHEN a.ID IS NOT NULL THEN 'CustName' END AS FileName, a.ProviderID as CustID, ");

           sql.AppendLine("CASE WHEN e.CurrencyName IS NOT NULL THEN e.CurrencyName WHEN e.CurrencyName IS NULL THEN '' END AS CurrencyName");
           sql.AppendLine(",Status='' ");
           sql.AppendLine(" from officedba.PurchaseArrive as a");
           sql.AppendLine("LEFT OUTER JOIN");
           sql.AppendLine("officedba.CodePublicType AS b ON a.PayType = b.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS c ON a.MoneyType = c.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CurrencyTypeSetting AS e ON e.ID = a.CurrencyType");
           sql.AppendLine("LEFT OUTER JOIN officedba.ProviderInfo AS d ON d.ID = a.ProviderID");
           sql.AppendLine("where a.CompanyCD=@CompanyCD and a.BillStatus='2'");

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           //订单编号
           if (!string.IsNullOrEmpty(RejectNo))
           {
               sql.AppendLine(" AND a.ArriveNo LIKE @ArriveNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@ArriveNo", "%" + RejectNo + "%"));
           }
           //主题
           if (!string.IsNullOrEmpty(Title))
           {
               sql.AppendLine(" AND a.Title LIKE @Title ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + Title + "%"));
           }
           //客户名称
           if (!string.IsNullOrEmpty(CustName))
           {
               sql.AppendLine(" AND a.ProviderID = @CustName ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", CustName));
           }
           //开始和结束时间
           if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
           {
               sql.AppendLine(" AND a.CreateDate BetWeen  @StartDate and @EndDate ");
               comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
               comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
           }
           //指定命令的SQL文
           comm.CommandText = sql.ToString();
           //执行查询
           return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);


       }
       #endregion

       #region 检索销售发货通知单  add by moshenlin 2010-06-21
       public static DataTable GetSellSendInfo(string CompanyCD, string RejectNo, string Title,
            string CustName, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.SendNo as OrderNO,a.Title,");
           sql.AppendLine("CASE WHEN d .CustName IS NOT NULL THEN d.CustName WHEN d .CustName IS NULL THEN '' END AS CustName,");
           sql.AppendLine("CASE WHEN CAST(a.RealTotal AS varchar) ");
           sql.AppendLine("IS NULL THEN '' WHEN a.RealTotal IS NOT NULL THEN CAST(RealTotal AS varchar) END AS RealTotal");
           sql.AppendLine(",CASE WHEN a.CreateDate IS NOT NULL THEN CONVERT(varchar(10), a.CreateDate, 120) WHEN a.CreateDate IS NULL ");
           sql.AppendLine(" THEN '' END AS CreateDate,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.Rate,1) as Rate,");
           sql.AppendLine("CASE WHEN a.isOpenbill = '0' THEN '未建单' WHEN a.isOpenbill = '1' THEN '已建单' END AS isOpenbill,");
           sql.AppendLine(" CASE WHEN b.TypeName IS NOT NULL THEN b.TypeName WHEN b.TypeName IS NULL THEN '' END AS PlayWay,");
           sql.AppendLine(" CASE WHEN c.TypeName IS NOT NULL THEN c.TypeName WHEN c.TypeName IS NULL THEN '' END AS MoneyWay,");
           sql.AppendLine("CASE WHEN a.ID IS NOT NULL THEN 'officedba.CustInfo' END AS FromTBName, CASE WHEN a.ID IS NOT NULL THEN 'CustName' END AS FileName, a.CustID as CustID, ");

           sql.AppendLine("CASE WHEN e.CurrencyName IS NOT NULL THEN e.CurrencyName WHEN e.CurrencyName IS NULL THEN '' END AS CurrencyName");
           sql.AppendLine(",Status='' ");
           sql.AppendLine(" from officedba.SellSend as a");
           sql.AppendLine("LEFT OUTER JOIN");
           sql.AppendLine("officedba.CodePublicType AS b ON a.PayType = b.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS c ON a.MoneyType = c.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CurrencyTypeSetting AS e ON e.ID = a.CurrencyType");
           sql.AppendLine("LEFT OUTER JOIN officedba.CustInfo AS d ON d.ID = a.CustID");
           sql.AppendLine("where a.CompanyCD=@CompanyCD and a.BillStatus='2'");

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           //订单编号
           if (!string.IsNullOrEmpty(RejectNo))
           {
               sql.AppendLine(" AND a.SendNo LIKE @ArriveNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@ArriveNo", "%" + RejectNo + "%"));
           }
           //主题
           if (!string.IsNullOrEmpty(Title))
           {
               sql.AppendLine(" AND a.Title LIKE @Title ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + Title + "%"));
           }
           //客户名称
           if (!string.IsNullOrEmpty(CustName))
           {
               sql.AppendLine(" AND a.ProviderID = @CustName ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", CustName));
           }
           //开始和结束时间
           if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
           {
               sql.AppendLine(" AND a.CreateDate BetWeen  @StartDate and @EndDate ");
               comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
               comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
           }
           //指定命令的SQL文
           comm.CommandText = sql.ToString();
           //执行查询
           return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);

       }
       #endregion

       #region 检索采购到货通知单  add by moshenlin 2010-06-21
       public static DataTable GetPurchaseIncomeInfo(string ids, string CompanyCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.ArriveNo as OrderNO,a.Title,");
           sql.AppendLine("CASE WHEN d .CustName IS NOT NULL THEN d.CustName WHEN d .CustName IS NULL THEN '' END AS CustName,");
           sql.AppendLine("CASE WHEN CAST(a.RealTotal AS varchar) ");
           sql.AppendLine("IS NULL THEN '' WHEN a.RealTotal IS NOT NULL THEN CAST(RealTotal AS varchar) END AS RealTotal");
           sql.AppendLine(",CASE WHEN a.CreateDate IS NOT NULL THEN CONVERT(varchar(10), a.CreateDate, 120) WHEN a.CreateDate IS NULL ");
           sql.AppendLine(" THEN '' END AS CreateDate,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.Rate,1) as Rate,");
           sql.AppendLine("CASE WHEN a.isOpenbill = '0' THEN '未建单' WHEN a.isOpenbill = '1' THEN '已建单' END AS isOpenbill,");
           sql.AppendLine(" CASE WHEN b.TypeName IS NOT NULL THEN b.TypeName WHEN b.TypeName IS NULL THEN '' END AS PlayWay,");
           sql.AppendLine(" CASE WHEN c.TypeName IS NOT NULL THEN c.TypeName WHEN c.TypeName IS NULL THEN '' END AS MoneyWay,");
           sql.AppendLine("CASE WHEN a.ID IS NOT NULL THEN 'officedba.ProviderInfo' END AS FromTBName, CASE WHEN a.ID IS NOT NULL THEN 'CustName' END AS FileName, a.ProviderID as CustID, ");

           sql.AppendLine("CASE WHEN e.CurrencyName IS NOT NULL THEN e.CurrencyName WHEN e.CurrencyName IS NULL THEN '' END AS CurrencyName");
           sql.AppendLine(",Status='' ");
           sql.AppendLine(" from officedba.PurchaseArrive as a");
           sql.AppendLine("LEFT OUTER JOIN");
           sql.AppendLine("officedba.CodePublicType AS b ON a.PayType = b.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS c ON a.MoneyType = c.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CurrencyTypeSetting AS e ON e.ID = a.CurrencyType");
           sql.AppendLine("LEFT OUTER JOIN officedba.ProviderInfo AS d ON d.ID = a.ProviderID");
           sql.AppendLine("where a.CompanyCD=@CompanyCD and a.BillStatus='2'  and a.ID in ( " + ids + " ) order by a.CreateDate asc ");

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           
           //指定命令的SQL文
           comm.CommandText = sql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);


       }
       #endregion

       #region 检索销售发货通知单  add by moshenlin 2010-06-21
       public static DataTable GetSellSendInfo(string ids, string CompanyCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.SendNo as OrderNO,a.Title,");
           sql.AppendLine("CASE WHEN d .CustName IS NOT NULL THEN d.CustName WHEN d .CustName IS NULL THEN '' END AS CustName,");
           sql.AppendLine("CASE WHEN CAST(a.RealTotal AS varchar) ");
           sql.AppendLine("IS NULL THEN '' WHEN a.RealTotal IS NOT NULL THEN CAST(RealTotal AS varchar) END AS RealTotal");
           sql.AppendLine(",CASE WHEN a.CreateDate IS NOT NULL THEN CONVERT(varchar(10), a.CreateDate, 120) WHEN a.CreateDate IS NULL ");
           sql.AppendLine(" THEN '' END AS CreateDate,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.Rate,1) as Rate,");
           sql.AppendLine("CASE WHEN a.isOpenbill = '0' THEN '未建单' WHEN a.isOpenbill = '1' THEN '已建单' END AS isOpenbill,");
           sql.AppendLine(" CASE WHEN b.TypeName IS NOT NULL THEN b.TypeName WHEN b.TypeName IS NULL THEN '' END AS PlayWay,");
           sql.AppendLine(" CASE WHEN c.TypeName IS NOT NULL THEN c.TypeName WHEN c.TypeName IS NULL THEN '' END AS MoneyWay,");
           sql.AppendLine("CASE WHEN a.ID IS NOT NULL THEN 'officedba.CustInfo' END AS FromTBName, CASE WHEN a.ID IS NOT NULL THEN 'CustName' END AS FileName, a.CustID as CustID, ");

           sql.AppendLine("CASE WHEN e.CurrencyName IS NOT NULL THEN e.CurrencyName WHEN e.CurrencyName IS NULL THEN '' END AS CurrencyName");
           sql.AppendLine(",Status='' ");
           sql.AppendLine(" from officedba.SellSend as a");
           sql.AppendLine("LEFT OUTER JOIN");
           sql.AppendLine("officedba.CodePublicType AS b ON a.PayType = b.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType AS c ON a.MoneyType = c.ID");
           sql.AppendLine("LEFT OUTER JOIN officedba.CurrencyTypeSetting AS e ON e.ID = a.CurrencyType");
           sql.AppendLine("LEFT OUTER JOIN officedba.CustInfo AS d ON d.ID = a.CustID");
           sql.AppendLine("where a.CompanyCD=@CompanyCD and a.BillStatus='2'   and a.ID in ( " + ids + " ) order by a.CreateDate asc ");

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

           //指定命令的SQL文
           comm.CommandText = sql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);

       }
       #endregion

       #region  审核开票
       /// <summary>
       /// 审核开票信息
       /// </summary>
       /// <param name="ID">主键</param>
       /// <param name="Auditor">审核人</param>
       /// <param name="AuditDate">审核时间</param>
       /// <returns>true审核成功，false审核失败</returns>
       public static bool AuditBilling(string ID,string Auditor,DateTime AuditDate)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update officedba.Billing set Auditor=@Auditor,");
           sql.AppendLine("AuditDate=@AuditDate where ID In( " + ID + ") ");
           SqlParameter[] parms = new SqlParameter[2];
           parms[0] = SqlHelper.GetParameter("@Auditor", Auditor);
           parms[1] = SqlHelper.GetParameter("@AuditDate", AuditDate);
           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 反审批开票
       public static bool ReverseAuditBilling(string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update officedba.Billing Set Auditor=NULL ,AuditDate=NULL where ID in ("+ID.Trim()+")");
           //SqlParameter[] parms = new SqlParameter[1];
           //parms[0] = SqlHelper.GetParameter("@ID", ID);
           SqlHelper.ExecuteTransSql(sql.ToString());
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 删除发票
       /// <summary>
       /// 删除发票信息
       /// </summary>
       /// <param name="ID">主键</param>
       /// <returns>true成功，false</returns>
       public static bool DeleteBilling(string ID)
       {
           //删除SQL拼写
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("delete from officedba.Billing ");
           sql.AppendLine(" WHERE ");
           sql.AppendLine(" ID In ( " + ID + ")");

           SqlCommand comm = new SqlCommand();
           comm.CommandText = sql.ToString();

           //设置参数
           ArrayList listDelete = new ArrayList();
           UpdateIsOpenStatus(listDelete, ID);

           listDelete.Add(comm);

           return SqlHelper.ExecuteTransWithArrayList(listDelete);   
       }
       #endregion
 
       #region 更新订单
       /// <summary> 
       /// 更新销售订单活采购订单开票状态
       /// </summary>
       /// <param name="lstCommand">ArrayList数组</param>
       private static void UpdateIsOpenStatus(ArrayList lstCommand,string ID)
       {
           string sql = "select SourceDT,SourceID from officedba.Billing where ID in ("+ID+")";
           DataTable dt = SqlHelper.ExecuteSql(sql);


           string tablename, id = "";
           StringBuilder cmdsql = new StringBuilder();
           SqlCommand cmd = null;
           if (dt != null && dt.Rows.Count > 0)
           {
               foreach (DataRow rows in dt.Rows)
               {
                   cmd = new SqlCommand();
                   tablename = rows["SourceDt"].ToString();
                   id = rows["SourceID"].ToString();
                   if (!string.IsNullOrEmpty(tablename) && !string.IsNullOrEmpty(id))
                   {
                    
                       cmdsql.AppendLine("Update " + tablename + " set isOpenbill='0' where ID in ( "+id+" ) ");
                       cmd.CommandText = cmdsql.ToString();
                       //添加命令
                       lstCommand.Add(cmd);
                   }
               }
           }
       }
       #endregion
        
       #region 添加开票信息
       /// <summary>
       /// 添加开票信息
       /// </summary>
       /// <param name="model">开票实体</param>
       /// <returns>true 成功，false失败</returns>
       public static bool InsertBillingInfo(BillingModel model,out int ID)
       {

           SqlConnection conn = new SqlConnection(SqlHelper._connectionStringStr);
           conn.Open();
           SqlTransaction mytran = conn.BeginTransaction();
           try
           {
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("Insert into officedba.Billing");
               sql.AppendLine("(CompanyCD,BillCD,BillingNum,BillingType,");
               sql.AppendLine("InvoiceType,CreateDate,ContactUnits,AcceWay,");
               sql.AppendLine("TotalPrice,NAccounts,Executor,DeptID,SourceDt,");
               sql.AppendLine("SourceID,ColumnName,Remark,CurrencyType,CurrencyRate,CustID,FromTBName,FileName)");
               sql.AppendLine("values(@CompanyCD,@BillCD,@BillingNum,@BillingType,");
               sql.AppendLine("@InvoiceType,@CreateDate,@ContactUnits,@AcceWay,");
               sql.AppendLine("@TotalPrice,@NAccounts,@Executor,@DeptID,@SourceDt,@SourceID,");
               sql.AppendLine("@ColumnName,@Remark,@CurrencyType,@CurrencyRate,@CustID,@FromTBName,@FileName)");
               sql.AppendLine("set @IntID= @@IDENTITY");

               SqlParameter[] parms = new SqlParameter[22];
               parms[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
               parms[1] = SqlHelper.GetParameter("@BillCD", model.BillCD);
               parms[2] = SqlHelper.GetParameter("@BillingNum", model.BillingNum);
               parms[3] = SqlHelper.GetParameter("@BillingType", model.BillingType);
               parms[4] = SqlHelper.GetParameter("@InvoiceType", model.InvoiceType);
               parms[5] = SqlHelper.GetParameter("@CreateDate", model.CreateDate);
               parms[6] = SqlHelper.GetParameter("@ContactUnits", model.ContactUnits);
               parms[7] = SqlHelper.GetParameter("@AcceWay", model.AcceWay);
               parms[8] = SqlHelper.GetParameter("@TotalPrice", model.TotalPrice);
               parms[9] = SqlHelper.GetParameter("@NAccounts", model.TotalPrice);

               parms[10] = SqlHelper.GetParameter("@Executor", model.Executor);
               parms[11] = SqlHelper.GetParameter("@DeptID", model.DeptID);
               parms[12] = SqlHelper.GetParameter("@SourceDt", model.SourceDt);
               parms[13] = SqlHelper.GetParameter("@SourceID", model.SourceID);
               parms[14] = SqlHelper.GetParameter("@ColumnName", model.ColumnName);
               parms[15] = SqlHelper.GetParameter("@Remark", model.Remark);

               parms[16] = SqlHelper.GetParameter("@CurrencyType", model.CurrencyType);
               parms[17] = SqlHelper.GetParameter("@CurrencyRate", model.CurrencyRate);

               parms[18] = SqlHelper.GetParameter("@CustID", model.CustID);
               parms[19] = SqlHelper.GetParameter("@FromTBName", model.FromTBName);
               parms[20] = SqlHelper.GetParameter("@FileName", model.FileName);

               parms[21] = SqlHelper.GetOutputParameter("@IntID", SqlDbType.Int);

               int rev = SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, sql.ToString(), parms);
               ID = Convert.ToInt32(parms[21].Value);

               string tablename, id = "";
               StringBuilder cmdsql = new StringBuilder();
               
               tablename = model.SourceDt;
               id = model.SourceID;
               if (!string.IsNullOrEmpty(tablename) && !string.IsNullOrEmpty(id))
               {

                   cmdsql.AppendLine("Update " + tablename + " set isOpenbill='1' where ID in ( "+id+" ) ");
                   SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, cmdsql.ToString(), null);
               }
               mytran.Commit();
               return rev > 0 ? true : false;
           }
           catch (Exception ex)
           {
               mytran.Rollback();

               throw ex;
           }
           finally
           {
               conn.Close();
               conn.Dispose();
           }
       }
       #endregion

       #region 修改开票信息
       /// <summary>
       /// 修改开票信息
       /// </summary>
       /// <param name="model">开票实体</param>
       /// <returns>true 成功，false失败</returns>
       public static bool UpdateBillingInfo(BillingModel model)
       {
           
           //设置参数
           ArrayList lstCommand = new ArrayList();
           string Selectsql = "select SourceDT,SourceID from officedba.Billing where ID in (" + model.ID + ")";
           DataTable dt = SqlHelper.ExecuteSql(Selectsql);
           string tablename, id = "";
           if (dt != null && dt.Rows.Count > 0)
           {
               foreach (DataRow rows in dt.Rows)
               {
                   tablename = rows["SourceDt"].ToString();
                   id = rows["SourceID"].ToString();
                   if (!string.IsNullOrEmpty(tablename) && !string.IsNullOrEmpty(id))
                   {
                       SqlCommand comm = new SqlCommand();
                       comm.CommandText = "Update " + tablename + " set isOpenbill='0' where ID in ( " + id + " ) ";
                      
                       //添加命令
                       lstCommand.Add(comm);
                   }
               }
           }

           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update officedba.Billing set BillCD=@BillCD,");
           sql.AppendLine("BillingNum=@BillingNum,BillingType=@BillingType,");
           sql.AppendLine("InvoiceType=@InvoiceType,CreateDate=@CreateDate,ContactUnits=@ContactUnits,");
           sql.AppendLine("AcceWay=@AcceWay,TotalPrice=@TotalPrice,NAccounts=@NAccounts,Executor=@Executor,");
           sql.AppendLine("DeptID=@DeptID,SourceDt=@SourceDt,SourceID=@SourceID,ColumnName=@ColumnName,");
           sql.AppendLine("Remark=@Remark,CurrencyType=@CurrencyType,CurrencyRate=@CurrencyRate,CustID=@CustID,FromTBName=@FromTBName,FileName=@FileName  where ID=@ID");



           SqlCommand cmd = new SqlCommand();

           cmd.CommandText = sql.ToString();
           cmd.Parameters.AddWithValue("@BillCD", SqlDbType.VarChar).Value = model.BillCD;
           cmd.Parameters.AddWithValue("@BillingNum", SqlDbType.VarChar).Value = model.BillingNum;
           cmd.Parameters.AddWithValue("@BillingType", SqlDbType.VarChar).Value = model.BillingType;
           cmd.Parameters.AddWithValue("@InvoiceType", SqlDbType.VarChar).Value = model.InvoiceType;
           cmd.Parameters.AddWithValue("@CreateDate", SqlDbType.DateTime).Value = model.CreateDate;
           cmd.Parameters.AddWithValue("@ContactUnits", SqlDbType.VarChar).Value = model.ContactUnits;
           cmd.Parameters.AddWithValue("@AcceWay", SqlDbType.VarChar).Value = model.AcceWay;
           cmd.Parameters.AddWithValue("@TotalPrice", SqlDbType.Decimal).Value = model.TotalPrice;
           cmd.Parameters.AddWithValue("@NAccounts", SqlDbType.Decimal).Value = model.TotalPrice;

           cmd.Parameters.AddWithValue("@Executor", SqlDbType.Int).Value = model.Executor;
           cmd.Parameters.AddWithValue("@DeptID", SqlDbType.Int).Value = model.DeptID;
           cmd.Parameters.AddWithValue("@SourceDt", SqlDbType.VarChar).Value = model.SourceDt;
           cmd.Parameters.AddWithValue("@SourceID", SqlDbType.VarChar).Value = model.SourceID;
           cmd.Parameters.AddWithValue("@ColumnName", SqlDbType.VarChar).Value = model.ColumnName;
           cmd.Parameters.AddWithValue("@Remark", SqlDbType.VarChar).Value = model.Remark;
           cmd.Parameters.AddWithValue("@CurrencyType", SqlDbType.Int).Value = model.CurrencyType;
           cmd.Parameters.AddWithValue("@CurrencyRate", SqlDbType.Decimal).Value = model.CurrencyRate;
           cmd.Parameters.AddWithValue("@CustID", SqlDbType.Int).Value = model.CustID;
           cmd.Parameters.AddWithValue("@FromTBName", SqlDbType.VarChar).Value = model.FromTBName;
           cmd.Parameters.AddWithValue("@FileName", SqlDbType.VarChar).Value = model.FileName;
           cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = model.ID;

           lstCommand.Add(cmd);




           StringBuilder cmdsql = new StringBuilder();
           if (!string.IsNullOrEmpty(model.SourceDt) && !string.IsNullOrEmpty(model.SourceID))
           {

               cmdsql.AppendLine("Update " + model.SourceDt + " set isOpenbill='1' where ID in ( " + model.SourceID + " ) ");
               SqlCommand updatecmd = new SqlCommand();
               updatecmd.CommandText = cmdsql.ToString();
               lstCommand.Add(updatecmd);
           }



           return SqlHelper.ExecuteTransWithArrayList(lstCommand);  
       }
       #endregion

       #region 根据公司编码获取开票信息
       /// <summary>
       /// 根据公司编码获取开票信息
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetBillingInfoByCompanyCD(string CompanyCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select  case when e.EmployeeName is null then '' when e.EmployeeName is not null then e.EmployeeName");
           sql.AppendLine("end as Auditor,isnull(a.CustID,0) as CustID ,isnull(a.FromTBName,'') as FromTBName ,isnull(a.FileName,'') as FileName,b.[Name] as BillingType,a.ID,a.billingnum,Convert (varchar(10),a.CreateDate,120) as CreateDate");
           sql.AppendLine(",a.ContactUnits,case when a.AcceWay='0' then '现金' when a.acceWay='1' then '银行转账' end as AcceWay,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.CurrencyRate,1) as CurrencyRate,CASE WHEN g.CurrencyName IS NOT NULL THEN g.CurrencyName WHEN g.CurrencyName IS NULL THEN '' END AS CurrencyName,");
           sql.AppendLine("a.TotalPrice,case when a.IsAccount='0' then '未登凭证' when a.IsAccount='1' then ' 已登凭证' end as IsVoucher , ");
           sql.AppendLine("case when a.AccountsStatus='0' then '未结算' when a.AccountsStatus='1' then '已结算' end as  AccountsStatus");
           sql.AppendLine(" from officedba.billing as a left join officedba.BillingType as b ");
           sql.AppendLine("on a.BillingType=b.ID  left join  officedba.EmployeeInfo as e on a.Auditor=e.ID LEFT OUTER JOIN officedba.CurrencyTypeSetting AS g ON a.CurrencyType = g.ID  where CompanyCD=@CompanyCD");
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           return SqlHelper.ExecuteSql(sql.ToString(),parms);
       }
       #endregion

       #region 根据检索条件检索出满足条件的信息
       /// <summary>
       /// 根据检索条件检索出满足条件的信息
       /// </summary>
       /// <param name="CompanyCD">公司编码</param>
       /// <param name="OrderCD">订单编码</param>
       /// <param name="BillingNum">订单号</param>
       /// <param name="BillingType">开票类型</param>
       /// <param name="AccountsStatus">结算状态</param>
       /// <param name="StartDate">开始日期</param>
       /// <param name="EndDate">结束日期</param>
       /// <returns>DataTable</returns>
       public static DataTable SearchBillingInfo(string CompanyCD, string OrderCD, string BillingNum, string BillingType,
           string AccountsStatus, string StartDate, string EndDate, string AuditStatus, string IsVoucher, string ContactUnits,
          int pageIndex, int pageSize, string OrderBy, ref int totalCount)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select  case when  a.BillCD is not null then a.BillCD when a.BillCD is null then '' end as BillCD, case when e.EmployeeName is null then '' when e.EmployeeName is not null then e.EmployeeName");
           sql.AppendLine("end as Auditor,a.ID,isnull(a.CustID,0) as CustID ,isnull(a.FromTBName,'') as FromTBName ,isnull(a.FileName,'') as FileName,a.Billingnum as BillingNumber,Convert (varchar(10),a.CreateDate,120) as CreateDate");
           sql.AppendLine(",isnull(a.CurrencyType,'') as CurrencyType,isnull(a.CurrencyRate,1) as CurrencyRate,CASE WHEN g.CurrencyName IS NOT NULL THEN g.CurrencyName WHEN g.CurrencyName IS NULL THEN '' END AS CurrencyName,case when a.BillingType='1' then '销售订单' ");
           sql.AppendLine(" when a.BillingType='0' then '' ");
           sql.AppendLine(" when a.BillingType='2' then '采购订单' ");
           sql.AppendLine(" when a.BillingType='3' then '销售退货单' ");
           sql.AppendLine(" when a.BillingType='4' then '代销结算单' ");
           sql.AppendLine(" when a.BillingType='5' then '采购退货单' ");
           sql.AppendLine(" when a.BillingType='6' then '采购到货通知单' ");
           sql.AppendLine(" when a.BillingType='7' then '销售发货通知单' end as BillingType ");
           sql.AppendLine(",case when a.YAccounts is not null then  convert(varchar,convert(money,a.YAccounts),1) when  a.YAccounts  is null then '0' end as YAccounts,");
           sql.AppendLine("case when a.NAccounts is not null then convert(varchar,convert(money,a.NAccounts),1) when  a.NAccounts  is null then '0' end as NAccounts");
           sql.AppendLine(",a.ContactUnits,case when a.AcceWay='0' then '现金' when a.acceWay='1' then '银行转账' end as AcceWay,case when a.AuditDate is not null then  convert(varchar(10),a.AuditDate,120) when a.AuditDate is null then '' end as AuditDate,");
           sql.AppendLine("convert(varchar,convert(money,a.TotalPrice),1) as TotalPrice ,case when a.IsAccount='0' then '未登凭证' when a.IsAccount='1' then ' 已登凭证' end as IsVoucher , ");
           sql.AppendLine("case when a.AccountsStatus='0' then '未结算' when a.AccountsStatus='1' then '已结算' when a.AccountsStatus='2' then '结算中' end as  AccountsStatus,SourceDt,SourceID ");
           sql.AppendLine(" from officedba.billing as a left join officedba.BillingType as b ");
           sql.AppendLine("on a.BillingType=b.ID  left join  officedba.EmployeeInfo as e on a.Auditor=e.ID LEFT OUTER JOIN officedba.CurrencyTypeSetting AS g ON a.CurrencyType = g.ID  where a.CompanyCD=@CompanyCD");

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           //订单编号
           if (!string.IsNullOrEmpty(OrderCD))
           {
               sql.AppendLine(" AND a.BillCD LIKE @OrderCD ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCD", "%" + OrderCD + "%"));
           }
           //开票编号
           if (!string.IsNullOrEmpty(BillingNum))
           {
               sql.AppendLine(" AND a.BillingNum LIKE @BillingNum ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillingNum", "%" + BillingNum + "%"));
           }
           //开票类型
           if (!string.IsNullOrEmpty(BillingType))
           {
               sql.AppendLine(" AND a.BillingType = @BillingType ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillingType", BillingType));
           }
           //结算状态
           if (!string.IsNullOrEmpty(AccountsStatus))
           {
               sql.AppendLine(" AND a.AccountsStatus=@AccountsStatus ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@AccountsStatus", AccountsStatus));
           }
           //创建时间
           if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
           {
               sql.AppendLine(" AND a.CreateDate BetWeen  @StartDate and @EndDate ");
               comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
               comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
           }
           //登证状态
           if (!string.IsNullOrEmpty(IsVoucher))
           {
               sql.AppendLine(" AND a.IsAccount=@IsVoucher ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsVoucher", IsVoucher));
           }
           //审核状态
           if (!string.IsNullOrEmpty(AuditStatus))
           {
               if (AuditStatus == "0")
               {
                   sql.AppendLine(" AND a.Auditor  is null ");
                   
               }
               else
               {
                   sql.AppendLine(" AND a.Auditor is not null ");
               }
           }
           //往来单位
           if (!string.IsNullOrEmpty(ContactUnits))
           {
               sql.AppendLine("	AND a.ContactUnits LIKE '%' + @ContactUnits + '%'");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContactUnits", ContactUnits));
           }
 
           comm.CommandText = sql.ToString();
           return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);
       }
       #endregion

       #region 更新是否登记凭证状态
       /// <summary>
       /// 更新是否登记凭证状态
       /// </summary>
       /// <param name="ID">主键</param>
       /// <returns>true 成功，false失败</returns>
       public static bool UpdateVoucherStatus(string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update officedba.Billing Set IsAccount='1' ");
           sql.AppendLine("where ID in("+ID+")");

           SqlHelper.ExecuteTransSql(sql.ToString());
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 根据主键获取业务单信息
       /// <summary> 
       /// 根据主键获取业务单信息
       /// </summary>
       /// <param name="ID">主键ID</param>
       /// <returns>DataTable</returns>
       public static DataTable GetBillingInfoByID(string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.BillCD,isnull(a.CustID,0) as CustID ,isnull(a.FromTBName,'') as FromTBName ,isnull(a.FileName,'') as FileName,a.BillingNum,a.BillingType,a.InvoiceType,case when  a.Auditor is not null then '已审批'");
           sql.AppendLine(" when a.Auditor is null then '未审批' end isAudit,convert(varchar(10),a.CreateDate,120) as CreateDate ,a.ContactUnits,a.AcceWay,");
           sql.AppendLine("a.TotalPrice,a.YAccounts,a.NAccounts,a.Executor,a.DeptID,a.Auditor,convert (varchar(10),a.AuditDate,120)AuditDate,");
           sql.AppendLine("a.Register,case when a.IsAccount='0' then '未登记' when a.IsAccount='1' then '已登记' end as IsVoucher");
           sql.AppendLine(",isnull(a.CurrencyType,'') as CurrencyType,isnull(a.CurrencyRate,1) as CurrencyRate,CASE WHEN g.CurrencyName IS NOT NULL THEN g.CurrencyName WHEN g.CurrencyName IS NULL THEN '' END AS CurrencyName,case when a.AccountsStatus='0' then '未结算' when a.AccountsStatus='1' then '已结算' end as AccountsStatus");
           sql.AppendLine(",a.Remark, e.EmployeeName as ExecutorName,d.DeptName, c.EmployeeName as AuditorName,t.EmployeeName as RegisterName,a.SourceID from officedba.Billing  as a");
           sql.AppendLine("left join  officedba.EmployeeInfo as e  on  a.Executor=e.ID");
           sql.AppendLine("left join  officedba.deptInfo as d on a.DeptID=d.ID");
           sql.AppendLine("left join  officedba.EmployeeInfo as c on a.Auditor=c.ID");
           sql.AppendLine("left join  officedba.EmployeeInfo as t on a.Register=t.ID LEFT OUTER JOIN officedba.CurrencyTypeSetting AS g ON a.CurrencyType = g.ID ");
           sql.AppendLine("where a.ID=@ID");
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@ID", ID);
           return SqlHelper.ExecuteSql(sql.ToString(),parms);
       }
       #endregion

       #region 根据主键获取业务单信息_打印
       /// <summary> 
       /// 根据主键获取业务单信息
       /// </summary>
       /// <param name="ID">主键ID</param>
       /// <returns>DataTable</returns>
       public static DataTable GetBillingInfoByIDPrint(string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("SELECT     a.BillCD, a.BillingNum,a.CustID,a.FromTBName,a.FileName, ");
           sql.AppendLine("  (CASE a.BillingType WHEN '1' THEN '销售订单' WHEN '2' THEN '采购订单' WHEN '3' THEN '销售退货单' WHEN '4' THEN '代销结算单' WHEN '5' THEN ");
           sql.AppendLine("  '采购退货单' ELSE '' END) AS BillingType,");
           sql.AppendLine(" (CASE a.InvoiceType WHEN '1' THEN '增值税发票' WHEN '2' THEN '普通地税' WHEN '3' THEN '普通国税' WHEN '4' THEN '收据' ELSE '' END) ");
           sql.AppendLine(" AS InvoiceType, CASE WHEN a.Auditor IS NOT NULL THEN '已审批' WHEN a.Auditor IS NULL THEN '未审批' END AS isAudit, CONVERT(varchar(10), ");
           sql.AppendLine(" a.CreateDate, 120) AS CreateDate, a.ContactUnits, CASE a.AcceWay WHEN '0' THEN '现金' WHEN '1' THEN '银行转账' ELSE '' END AS AcceWay, ");
           sql.AppendLine("   a.TotalPrice, a.YAccounts, a.NAccounts, a.Executor, a.DeptID, a.Auditor, CONVERT(varchar(10), a.AuditDate, 120) AS AuditDate, a.Register, ");
           sql.AppendLine(" CASE WHEN a.IsAccount = '0' THEN '未登记' WHEN a.IsAccount = '1' THEN '已登记' END AS IsVoucher, ");
           sql.AppendLine(" isnull(a.CurrencyType,'') as CurrencyType,isnull(a.CurrencyRate,1) as CurrencyRate,CASE WHEN g.CurrencyName IS NOT NULL THEN g.CurrencyName WHEN g.CurrencyName IS NULL THEN '' END AS CurrencyName, ");
           sql.AppendLine("  CASE WHEN a.AccountsStatus = '0' THEN '未结算' WHEN a.AccountsStatus = '1' THEN '已结算' END AS AccountsStatus, a.Remark, ");
           sql.AppendLine(" e.EmployeeName AS ExecutorName, d.DeptName, c.EmployeeName AS AuditorName, t.EmployeeName AS RegisterName ");
           sql.AppendLine(" FROM         officedba.Billing AS a LEFT OUTER JOIN ");
           sql.AppendLine(" officedba.EmployeeInfo AS e ON a.Executor = e.ID LEFT OUTER JOIN ");
           sql.AppendLine("  officedba.DeptInfo AS d ON a.DeptID = d.ID LEFT OUTER JOIN ");
           sql.AppendLine(" officedba.EmployeeInfo AS c ON a.Auditor = c.ID LEFT OUTER JOIN ");
           sql.AppendLine("  officedba.EmployeeInfo AS t ON a.Register = t.ID LEFT OUTER JOIN officedba.CurrencyTypeSetting AS g ON a.CurrencyType = g.ID");
           sql.AppendLine("where a.ID=@ID");
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@ID", ID);
           return SqlHelper.ExecuteSql(sql.ToString(), parms);
       }
       #endregion

       #region 根据主键获取业务单未付金额
       /// <summary>
       /// 根据主键获取业务单未付金额
       /// </summary>
       /// <param name="ID">主键ID</param>
       /// <returns>decimal</returns>
       public static decimal GetBillingNAccounts(string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select isnull(NAccounts,0) as NAccounts from officedba.Billing  where ID=@ID");
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@ID", ID);
           object obj = SqlHelper.ExecuteScalar(sql.ToString(), parms);
           decimal result = 0;
           if (Convert.ToDecimal(obj) > 0)
           {
               result = Convert.ToDecimal(obj);
           }
           return result;
       }
       #endregion

       #region 根据主键获取业务已付金额
        /// <summary>
       /// 根据主键获取业务单已付金额
       /// </summary>
       /// <param name="ID">主键ID</param>
       /// <returns>decimal</returns>
       public static DataTable  GetBillingYAccountsDT(string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select ID,isnull(YAccounts,0) as YAccounts from officedba.Billing  where ID in("+ID+")");

           return SqlHelper.ExecuteSql(sql.ToString());
       }
       #endregion

       #region 判断开票号是否存在
       public static bool BillingNumIsexist(string CompanyCD, string BillingNum)
       {
           bool result = false;
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select count(BillingNum) from Officedba.Billing where CompanyCD=@CompanyCD");
           sql.AppendLine("and BillingNum=@BillingNum ");

           SqlParameter[] parms = {
                                      new SqlParameter("@CompanyCD",CompanyCD),
                                      new SqlParameter("BillingNum",BillingNum)
                                  };

           object objs = SqlHelper.ExecuteScalar(sql.ToString(),parms);
           if (Convert.ToInt32(objs) > 0)
           {
               result = true;
           }

           return result;
       }
       #endregion

       #region 根据主键获取业务单总金额
       /// <summary>
       /// 根据主键获取业务单总金额
       /// </summary>
       /// <param name="ID">主键ID</param>
       /// <returns>decimal</returns>
       public static decimal GetBillingTotalAccounts(string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select isnull(TotalPrice,0) as TotalPrice from officedba.Billing  where ID=@ID");
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@ID", ID);
           object obj = SqlHelper.ExecuteScalar(sql.ToString(), parms);
           decimal result = 0;
           if (Convert.ToDecimal(obj) > 0)
           {
               result = Convert.ToDecimal(obj);
           }
           return result;
       }
       #endregion

       #region 根据主键获取业务单已付金额
       /// <summary>
       /// 根据主键获取业务单已付金额
       /// </summary>
       /// <param name="ID">主键ID</param>
       /// <returns>decimal</returns>
       public static decimal GetBillingYAccounts(string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select isnull(YAccounts,0) as YAccounts from officedba.Billing  where ID=@ID");
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@ID", ID);
           object obj = SqlHelper.ExecuteScalar(sql.ToString(), parms);
           decimal result = 0;
           if (Convert.ToDecimal(obj) > 0)
           {
               result = Convert.ToDecimal(obj);
           }
           return result;
       }
       #endregion

       #region 更新业务单结算状态
       public static bool UpdateAccountStatusByID(string ID,string AccountsStatus)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update officedba.Billing set AccountsStatus=@AccountsStatus ");
           sql.AppendLine(" where ID in ("+ID+")");

           SqlParameter[] parms = 
           {
               new SqlParameter("@AccountsStatus",AccountsStatus)
           };

           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion




       #region 判断采购订单的采购到货单是否建立业务单
       /// <summary>
       /// 
       /// </summary>
       /// <param name="SourceID"></param>
       /// <param name="BillCD"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static string CheckChooseInfo(string SourceID, string BillCD, string CompanyCD)
       {
           string rev = string.Empty;
           string sql = "select isnull(isOpenbill,'0') as isOpenbill,ArriveNo from officedba.PurchaseArrive where CompanyCD=@CompanyCD and ArriveNo in (select distinct ArriveNo  from officedba.PurchaseArriveDetail  where CompanyCD=@CompanyCD and FromBillID=@FromBillID  )";

           string[] source = SourceID.Split(',');
           string[] bill = BillCD.Split(',');
           for (int i = 0; i < source.Length; i++)
           {
               if (!string.IsNullOrEmpty(source[i].ToString()))
               {
                   SqlParameter[] parms = new SqlParameter[2];
                   parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                   parms[1] = SqlHelper.GetParameter("@FromBillID", source[i].ToString());

                   DataTable obj = SqlHelper.ExecuteSql(sql, parms);

                   for (int j = 0; j < obj.Rows.Count; j++)
                   {
                       if (obj.Rows[j]["isOpenbill"].ToString() == "1")
                       {
                           rev += "采购订单【" + bill[i].ToString() + "】,对应的采购到货通知单【" + obj.Rows[j]["ArriveNo"].ToString() + "】已建单|";
                       }
                   }
               }
           }

           return rev.TrimEnd(new char[] { '|' });
       }
       #endregion


       #region 判断销售订单的销售发货单是否建立业务单
       /// <summary>
       /// 
       /// </summary>
       /// <param name="SourceID"></param>
       /// <param name="BillCD"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static string CheckChooseSellSendInfo(string SourceID, string BillCD, string CompanyCD)
       {
           string rev = string.Empty;
           string sql = "select isnull(isOpenbill,'0') as isOpenbill,SendNo from officedba.SellSend where CompanyCD=@CompanyCD  and FromBillID=@FromBillID  ";

           string[] source = SourceID.Split(',');
           string[] bill = BillCD.Split(',');
           for (int i = 0; i < source.Length; i++)
           {
               if (!string.IsNullOrEmpty(source[i].ToString()))
               {
                   SqlParameter[] parms = new SqlParameter[2];
                   parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                   parms[1] = SqlHelper.GetParameter("@FromBillID", source[i].ToString());
                   DataTable obj = SqlHelper.ExecuteSql(sql, parms);

                   for (int j = 0; j < obj.Rows.Count; j++)
                   {
                       if (obj.Rows[j]["isOpenbill"].ToString()=="1")
                       {
                           rev += "销售订单【" + bill[i].ToString() + "】,对应的销售发货通知单【" + obj.Rows[j]["SendNo"].ToString() + "】已建单| ";
                       }
                   }
               }
           }

           return rev.TrimEnd(new char[] { '|' });
       }
       #endregion


       #region 判断采购到货单对应的源单采购订单是否建立业务单
       /// <summary>
       /// 
       /// </summary>
       /// <param name="SourceID"></param>
       /// <param name="BillCD"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static string CheckChoosePucharArriveInfo(string SourceID, string BillCD)
       {
           string rev = string.Empty;
           string sql = "select isnull(isOpenbill,'0') as isOpenbill,OrderNo from officedba.PurchaseOrder where ID in ( SELECT ISNULL(B.FromBillID,0) AS FromBillID  FROM officedba.PurchaseArrive A  LEFT OUTER JOIN officedba.PurchaseArriveDetail B  ON A.COMPANYCD=B.COMPANYCD AND A.ArriveNo=B.ArriveNo  WHERE A.ID=@ID  )";

           string[] source = SourceID.Split(',');
           string[] bill = BillCD.Split(',');
           for (int i = 0; i < source.Length; i++)
           {
               if (!string.IsNullOrEmpty(source[i].ToString()))
               {
                   SqlParameter[] parms = new SqlParameter[1];
                   parms[0] = SqlHelper.GetParameter("@ID", source[i].ToString());

                   DataTable obj = SqlHelper.ExecuteSql(sql, parms);

                   for (int j = 0; j < obj.Rows.Count; j++)
                   {
                       if (obj.Rows[j]["isOpenbill"].ToString() == "1")
                       {
                           rev += "采购到货通知单【" + bill[i].ToString() + "】,对应的采购订单【" + obj.Rows[j]["OrderNo"].ToString() + "】已建单| ";
                       }
                   }
               }
           }

           return rev.TrimEnd(new char[] { '|' });
       }
       #endregion




       #region 判断销售发货单对应的源单销售订单是否建立业务单
       /// <summary>
       /// 
       /// </summary>
       /// <param name="SourceID"></param>
       /// <param name="BillCD"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static string CheckChooseSellInfo(string SourceID, string BillCD)
       {
           string rev = string.Empty;
           string sql = "select isnull(isOpenbill,'0') as isOpenbill,OrderNo from officedba.SellOrder where ID in ( SELECT ISNULL(FromBillID,0) AS FromBillID  FROM officedba.SellSend  WHERE ID=@ID  )";

           string[] source = SourceID.Split(',');
           string[] bill = BillCD.Split(',');
           for (int i = 0; i < source.Length; i++)
           {
               if (!string.IsNullOrEmpty(source[i].ToString()))
               {
                   SqlParameter[] parms = new SqlParameter[1];
                   parms[0] = SqlHelper.GetParameter("@ID", source[i].ToString());

                   DataTable obj = SqlHelper.ExecuteSql(sql, parms);

                   for (int j = 0; j < obj.Rows.Count; j++)
                   {
                       if (obj.Rows[j]["isOpenbill"].ToString() == "1")
                       {
                           rev += "销售发货通知单【" + bill[i].ToString() + "】,对应的销售订单【" + obj.Rows[j]["OrderNo"].ToString() + "】已建单| ";
                       }
                   }
               }
           }

           return rev.TrimEnd(new char[] { '|' });
       }
       #endregion

   }
}

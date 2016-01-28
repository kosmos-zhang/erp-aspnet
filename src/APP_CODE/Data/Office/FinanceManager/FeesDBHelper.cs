/**********************************************
 * 类作用：   费用票据
 * 建立人：   zhangyy
 * 建立时间： 2010/03/16
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
using System.Collections.Generic;
using System.Collections;
using XBase.Common;

namespace XBase.Data.Office.FinanceManager
{
    public class FeesDBHelper
    {
        #region 不同源单类型
        #region 采购订单
        /// <summary>
        /// 采购订单
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="SourceNo">单据编号</param>
        /// <param name="ContactUnitName">往来单位名称</param>
        /// <param name="ContactType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSourceInfoByCGDD(string CompanyCD, string SourceNo, string ContactUnitName, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region 查询SQL拼写
                //查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,a.CompanyCD                ");
                searchSql.AppendLine("      ,a.OrderNo BillNo           ");
                searchSql.AppendLine("      ,a.ProviderID CustID,b.CustName    ");
                searchSql.AppendLine("      , CONVERT(varchar(100), a.CreateDate, 23) CreateDate ");
                searchSql.AppendLine("      ,a.TotalPrice                     ");
                searchSql.AppendLine("      ,(case a.IsFeesAccount when '1' then '已开票' else '未开票' end)IsFeesAccount ");               
                searchSql.AppendLine("  FROM officedba.PurchaseOrder a ");
                searchSql.AppendLine("  left join officedba.ProviderInfo b on b.id = a.ProviderID ");
                searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");
                searchSql.AppendLine(" and a.BillStatus = @BillStatus   ");
                searchSql.AppendLine(" and (a.IsFeesAccount = '0' OR  a.IsFeesAccount is null) ");
               
                #endregion 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                //单据状态取执行
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
                //编号
                if (!string.IsNullOrEmpty(SourceNo))
                {
                    searchSql.AppendLine("	AND a.OrderNo LIKE  '%' + @OrderNo + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", SourceNo));
                }
                //往来单位名称
                if (!string.IsNullOrEmpty(ContactUnitName))
                {
                    searchSql.AppendLine("	AND b.CustName LIKE  '%' + @CustName + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", ContactUnitName));
                }

                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 采购到货单
        /// <summary>
        /// 采购到货单
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="SourceNo">单据编号</param>
        /// <param name="ContactUnitName">往来单位名称</param>
        /// <param name="ContactType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSourceInfoByDHTZD(string CompanyCD, string SourceNo, string ContactUnitName, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region 查询SQL拼写
                //查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,a.CompanyCD                ");
                searchSql.AppendLine("      ,a.ArriveNo BillNo          ");
                searchSql.AppendLine("      ,a.ProviderID CustID,b.CustName    ");
                searchSql.AppendLine("      , CONVERT(varchar(100), a.CreateDate, 23) CreateDate ");
                searchSql.AppendLine("      ,a.TotalMoney TotalPrice    ");
                searchSql.AppendLine("      ,(case a.IsFeesAccount when '1' then '已开票' else '未开票' end)IsFeesAccount ");
                searchSql.AppendLine("  FROM officedba.PurchaseArrive a ");
                searchSql.AppendLine("  left join officedba.ProviderInfo b on b.id = a.ProviderID ");
                searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");
                searchSql.AppendLine(" and a.BillStatus = @BillStatus   ");
                searchSql.AppendLine(" and (a.IsFeesAccount = '0' OR  a.IsFeesAccount is null) ");

                #endregion 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                //单据状态取执行
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
                //编号
                if (!string.IsNullOrEmpty(SourceNo))
                {
                    searchSql.AppendLine("	AND a.ArriveNo LIKE  '%' + @ArriveNo + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ArriveNo", SourceNo));
                }
                //往来单位名称
                if (!string.IsNullOrEmpty(ContactUnitName))
                {
                    searchSql.AppendLine("	AND b.CustName LIKE  '%' + @CustName + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", ContactUnitName));
                }

                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 采购退货单
        /// <summary>
        /// 采购退货单
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="SourceNo">单据编号</param>
        /// <param name="ContactUnitName">往来单位名称</param>
        /// <param name="ContactType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSourceInfoByCGTHD(string CompanyCD, string SourceNo, string ContactUnitName, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region 查询SQL拼写
                //查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,a.CompanyCD                ");
                searchSql.AppendLine("      ,a.RejectNo BillNo          ");
                searchSql.AppendLine("      ,a.ProviderID CustID,b.CustName ");
                searchSql.AppendLine("      , CONVERT(varchar(100), a.CreateDate, 23) CreateDate ");
                searchSql.AppendLine("      ,a.TotalPrice               ");
                searchSql.AppendLine("      ,(case a.IsFeesAccount when '1' then '已开票' else '未开票' end)IsFeesAccount ");
                searchSql.AppendLine("  FROM officedba.PurchaseReject a ");
                searchSql.AppendLine("  left join officedba.ProviderInfo b on b.id = a.ProviderID ");
                searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");
                searchSql.AppendLine(" and a.BillStatus = @BillStatus   ");
                searchSql.AppendLine(" and (a.IsFeesAccount = '0' OR  a.IsFeesAccount is null) ");

                #endregion 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                //单据状态取执行
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
                //编号
                if (!string.IsNullOrEmpty(SourceNo))
                {
                    searchSql.AppendLine("	AND a.RejectNo LIKE  '%' + @RejectNo + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@RejectNo", SourceNo));
                }
                //往来单位名称
                if (!string.IsNullOrEmpty(ContactUnitName))
                {
                    searchSql.AppendLine("	AND b.CustName LIKE  '%' + @CustName + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", ContactUnitName));
                }

                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 销售订单
        /// <summary>
        /// 销售订单
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="SourceNo">单据编号</param>
        /// <param name="ContactUnitName">往来单位名称</param>
        /// <param name="ContactType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSourceInfoByXSDD(string CompanyCD, string SourceNo, string ContactUnitName, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region 查询SQL拼写
                //查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,a.CompanyCD                ");
                searchSql.AppendLine("      ,a.OrderNo BillNo          ");
                searchSql.AppendLine("      ,a.CustID,b.CustName    ");
                searchSql.AppendLine("      , CONVERT(varchar(100), a.CreateDate, 23) CreateDate ");
                searchSql.AppendLine("      ,a.TotalPrice               ");
                searchSql.AppendLine("      ,(case a.IsFeesAccount when '1' then '已开票' else '未开票' end)IsFeesAccount ");
                searchSql.AppendLine("  FROM officedba.SellOrder a ");
                searchSql.AppendLine("  left join officedba.CustInfo b on b.id = a.CustID ");
                searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");
                searchSql.AppendLine(" and a.BillStatus = @BillStatus   ");
                searchSql.AppendLine(" and (a.IsFeesAccount = '0' OR  a.IsFeesAccount is null) ");

                #endregion 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                //单据状态取执行
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
                //编号
                if (!string.IsNullOrEmpty(SourceNo))
                {
                    searchSql.AppendLine("	AND a.OrderNo LIKE  '%' + @OrderNo + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", SourceNo));
                }
                //往来单位名称
                if (!string.IsNullOrEmpty(ContactUnitName))
                {
                    searchSql.AppendLine("	AND b.CustName LIKE  '%' + @CustName + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", ContactUnitName));
                }

                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 根据销售订单编号获取费用明细
        public static DataTable GetSourceDetailByXSDD(string CompanyCD, string[] OrderNos)
        {
            string allOrderNos = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (OrderNos.Length == 0)
            {
                return null;
            }

            for (int i = 0; i < OrderNos.Length; i++)
            {
                OrderNos[i] = "'" + OrderNos[i] + "'";
                sb.Append(OrderNos[i]);
            }

            allOrderNos = sb.ToString().Replace("''", "','");

            try
            {
                #region 查询SQL拼写
                //查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,a.CompanyCD,a.OrderNo      ");
                searchSql.AppendLine("      ,a.FeeID,b.CodeName,b.FeeSubjectsNo,c.SubjectsName ");
                searchSql.AppendLine("      ,a.FeeTotal,a.Remark        ");
                searchSql.AppendLine(" from officedba.SellOrderFeeDetail a");
                searchSql.AppendLine(" left join officedba.CodeFeeType b on b.id = a.FeeID ");
                searchSql.AppendLine(" left join officedba.AccountSubjects c on c.SubjectsCD = b.FeeSubjectsNo and c.CompanyCD = b.CompanyCD ");
                searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");
                searchSql.AppendLine(" and a.OrderNo in (" + allOrderNos +") ");

                #endregion 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                ////单据状态取执行
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", allOrderNos));
               
                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.ExecuteSearch(comm);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 根据费用报销编号获取费用报销明细
        public static DataTable GetFeeReturnByXSDD(string CompanyCD, string[] OrderNos)
        {
            string allOrderNos = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (OrderNos.Length == 0)
            {
                return null;
            }

            for (int i = 0; i < OrderNos.Length; i++)
            {
                OrderNos[i] = "'" + OrderNos[i] + "'";
                sb.Append(OrderNos[i]);
            }

            allOrderNos = sb.ToString().Replace("''", "','");

            try
            {
                #region 查询SQL拼写
                //查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,d.ReimbNo      ");
                searchSql.AppendLine("      ,a.FeeNameID,b.CodeName,a.SubjectsNo,c.SubjectsName ");
                searchSql.AppendLine("      ,a.ReimbAmount ExpAmount,a.Notes        ");
                searchSql.AppendLine(" from officedba.FeeReturnDetail a");
                searchSql.AppendLine(" left join officedba.CodeFeeType b on b.id = a.FeeNameID ");
                searchSql.AppendLine(" full join officedba.AccountSubjects c on c.SubjectsCD = a.SubjectsNo and c.CompanyCD = @CompanyCD  ");
                searchSql.AppendLine(" left join officedba.FeeReturn d on d.id = a.ReimbID ");
                searchSql.AppendLine(" WHERE d.CompanyCD = @CompanyCD   ");
                searchSql.AppendLine(" and d.ReimbNo in (" + allOrderNos + ") ");

                #endregion 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                ////单据状态取执行
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", allOrderNos));

                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.ExecuteSearch(comm);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 销售发货通知单
        /// <summary>
        /// 销售发货通知单
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="SourceNo">单据编号</param>
        /// <param name="ContactUnitName">往来单位名称</param>
        /// <param name="ContactType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSourceInfoByXSFHTZD(string CompanyCD, string SourceNo, string ContactUnitName, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region 查询SQL拼写
                //查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,a.CompanyCD                ");
                searchSql.AppendLine("      ,a.SendNo BillNo          ");
                searchSql.AppendLine("      ,a.CustID,b.CustName    ");
                searchSql.AppendLine("      , CONVERT(varchar(100), a.CreateDate, 23) CreateDate ");
                searchSql.AppendLine("      ,a.TotalPrice               ");
                searchSql.AppendLine("      ,(case a.IsFeesAccount when '1' then '已开票' else '未开票' end)IsFeesAccount ");
                searchSql.AppendLine("  FROM officedba.SellSend a ");
                searchSql.AppendLine("  left join officedba.CustInfo b on b.id = a.CustID ");
                searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");
                searchSql.AppendLine(" and a.BillStatus = @BillStatus   ");
                searchSql.AppendLine(" and (a.IsFeesAccount = '0' OR  a.IsFeesAccount is null) ");

                #endregion 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                //单据状态取执行
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
                //编号
                if (!string.IsNullOrEmpty(SourceNo))
                {
                    searchSql.AppendLine("	AND a.SendNo LIKE  '%' + @SendNo + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@SendNo", SourceNo));
                }
                //往来单位名称
                if (!string.IsNullOrEmpty(ContactUnitName))
                {
                    searchSql.AppendLine("	AND b.CustName LIKE  '%' + @CustName + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", ContactUnitName));
                }

                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 销售退货单
        /// <summary>
        /// 销售退货单
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="SourceNo">单据编号</param>
        /// <param name="ContactUnitName">往来单位名称</param>
        /// <param name="ContactType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSourceInfoByXSTHD(string CompanyCD, string SourceNo, string ContactUnitName, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region 查询SQL拼写
                //查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,a.CompanyCD                ");
                searchSql.AppendLine("      ,a.BackNo BillNo          ");
                searchSql.AppendLine("      ,a.CustID,b.CustName    ");
                searchSql.AppendLine("      , CONVERT(varchar(100), a.CreateDate, 23) CreateDate ");
                searchSql.AppendLine("      ,a.TotalPrice               ");
                searchSql.AppendLine("      ,(case a.IsFeesAccount when '1' then '已开票' else '未开票' end)IsFeesAccount ");
                searchSql.AppendLine("  FROM officedba.SellBack a ");
                searchSql.AppendLine("  left join officedba.CustInfo b on b.id = a.CustID ");
                searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");
                searchSql.AppendLine(" and a.BillStatus = @BillStatus   ");
                searchSql.AppendLine(" and (a.IsFeesAccount = '0' OR  a.IsFeesAccount is null) ");

                #endregion 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                //单据状态取执行
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
                //编号
                if (!string.IsNullOrEmpty(SourceNo))
                {
                    searchSql.AppendLine("	AND a.BackNo LIKE  '%' + @BackNo + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BackNo", SourceNo));
                }
                //往来单位名称
                if (!string.IsNullOrEmpty(ContactUnitName))
                {
                    searchSql.AppendLine("	AND b.CustName LIKE  '%' + @CustName + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", ContactUnitName));
                }

                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 费用报销单
        /// <summary>
        /// 费用报销单
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="SourceNo">单据编号</param>
        /// <param name="ContactUnitName">往来单位名称</param>
        /// <param name="ContactType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSourceInfoByFYBXD(string CompanyCD, string SourceNo, string ContactUnitName, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region 查询SQL拼写
                //查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,a.CompanyCD                ");
                searchSql.AppendLine("      ,a.ReimbNo BillNo          ");
                searchSql.AppendLine("      ,a.Applyor CustID,b.EmployeeName CustName    ");
                searchSql.AppendLine("      , CONVERT(varchar(100), a.CreateDate, 23) CreateDate ");
                searchSql.AppendLine("      ,a.ReimbAllAmount TotalPrice");
                searchSql.AppendLine("      ,(case a.IsFeesAccount when '1' then '已登记' else '未登记' end)IsFeesAccount ");
                searchSql.AppendLine("  FROM officedba.FeeReturn a ");
                searchSql.AppendLine("  left join officedba.EmployeeInfo b on b.id = a.Applyor ");
                searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");
                searchSql.AppendLine(" and a.Status = @Status   ");

                #endregion 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                //单据状态取执行
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", "2"));
                //编号
                if (!string.IsNullOrEmpty(SourceNo))
                {
                    searchSql.AppendLine("	AND a.ReimbNo LIKE  '%' + @ReimbNo + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReimbNo", SourceNo));
                }
                //往来单位名称
                if (!string.IsNullOrEmpty(ContactUnitName))
                {
                    searchSql.AppendLine("	AND b.EmployeeName LIKE  '%' + @EmployeeName + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeName", ContactUnitName));
                }

                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 销售出库单
        /// <summary>
        /// 销售出库单
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="SourceNo">单据编号</param>
        /// <param name="ContactUnitName">往来单位名称</param>
        /// <param name="ContactType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSourceInfoByXSCKD(string CompanyCD, string SourceNo, string ContactUnitName, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region 查询SQL拼写
                //查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,a.CompanyCD                ");
                searchSql.AppendLine("      ,a.OutNo BillNo          ");
                searchSql.AppendLine("      ,b.CustID,c.CustName    ");
                searchSql.AppendLine("      , CONVERT(varchar(100), a.CreateDate, 23) CreateDate ");
                searchSql.AppendLine("      ,a.TotalPrice               ");
                searchSql.AppendLine("      ,(case a.IsFeesAccount when '1' then '已开票' else '未开票' end)IsFeesAccount ");
                searchSql.AppendLine("  FROM officedba.StorageOutSell a ");               
                searchSql.AppendLine("  left join officedba.SellSend b on b.id = a.FromBillID ");
                searchSql.AppendLine("  left join officedba.CustInfo c on c.id = b.CustID ");
                searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");
                searchSql.AppendLine(" and a.BillStatus = @BillStatus   ");
                searchSql.AppendLine(" and (a.IsFeesAccount = '0' OR  a.IsFeesAccount is null) ");

                #endregion 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                //单据状态取执行
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
                //编号
                if (!string.IsNullOrEmpty(SourceNo))
                {
                    searchSql.AppendLine("	AND a.OutNo LIKE  '%' + @OutNo + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", SourceNo));
                }
                //往来单位名称
                if (!string.IsNullOrEmpty(ContactUnitName))
                {
                    searchSql.AppendLine("	AND b.CustName LIKE  '%' + @CustName + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", ContactUnitName));
                }

                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 其他出库单
        /// <summary>
        /// 其他出库单
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="SourceNo">单据编号</param>
        /// <param name="ContactUnitName">往来单位名称</param>
        /// <param name="ContactType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSourceInfoByQTCKD(string CompanyCD, string SourceNo, string ContactUnitName, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region 查询SQL拼写
                //查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,a.CompanyCD                ");
                searchSql.AppendLine("      ,a.OutNo BillNo          ");
                searchSql.AppendLine("      ,b.ProviderID CustID,c.CustName    ");
                searchSql.AppendLine("      , CONVERT(varchar(100), a.CreateDate, 23) CreateDate ");
                searchSql.AppendLine("      ,a.TotalPrice               ");
                searchSql.AppendLine("      ,(case a.IsFeesAccount when '1' then '已开票' else '未开票' end)IsFeesAccount ");
                searchSql.AppendLine("  FROM officedba.StorageOutOther a ");
                searchSql.AppendLine("  left join officedba.PurchaseReject b on b.id = a.FromBillID ");
                searchSql.AppendLine("  left join officedba.ProviderInfo c on c.id = b.ProviderID ");
                searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");
                searchSql.AppendLine(" and a.BillStatus = @BillStatus   ");
                searchSql.AppendLine(" and (a.IsFeesAccount = '0' OR  a.IsFeesAccount is null) ");

                #endregion 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                //单据状态取执行
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
                //编号
                if (!string.IsNullOrEmpty(SourceNo))
                {
                    searchSql.AppendLine("	AND a.OutNo LIKE  '%' + @OutNo + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", SourceNo));
                }
                //往来单位名称
                if (!string.IsNullOrEmpty(ContactUnitName))
                {
                    searchSql.AppendLine("	AND b.CustName LIKE  '%' + @CustName + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", ContactUnitName));
                }

                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #endregion

        #region 判断单据编号是否存在
        /// <summary>
        /// 判断单据编号是否存在
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <returns>返回true时表示不存在</returns>
        private static bool NoIsExist(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@FeesNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.Fees ";
            strSql += " WHERE  (FeesNo  = @FeesNo ) AND (CompanyCD = @CompanyCD) ";
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }
        #endregion

        #region 添加
        #region 添加新单据(费用票据)
        /// <summary>
        /// 添加新单据
        /// </summary>
        /// <returns></returns>
        public static bool Insert(FeesModel sellOrderModel, List<FeesDetailModel> sellOrderDetailModellList, out string strMsg,out int Id)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            Id = 0;
            //判断单据编号是否存在
            if (NoIsExist(sellOrderModel.FeesNo))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    InsertSellOrder(sellOrderModel, tran,out Id);
                    InsertSellOrderDetail(sellOrderDetailModellList, tran);
                    if (sellOrderModel.FeesType != "0")
                    {
                        //非无源单时，更改源单开票状态
                        updateSourceByFeesType(sellOrderModel.FeesType, sellOrderModel.SourceNo, tran);
                    }
                    tran.Commit();
                    isSucc = true;
                    strMsg = "保存成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "保存失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                isSucc = false;
                strMsg = "该编号已被使用，请输入未使用的编号！";
            }
            return isSucc;
        }
        #endregion
        #region 为主表插入数据 
        /// <summary>
        /// 为主表插入数据
        /// </summary>
        /// <param name="sellOrderModel"></param>
        /// <param name="tran"></param>
        private static void InsertSellOrder(FeesModel sellOrderModel, TransactionManager tran,out int IntID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.Fees(");
            strSql.Append("CompanyCD,FeesNo ,FeesNum,FeesType,InvoiceType,CreateDate,ContactUnits,ContactType,AcceWay,SubjectsNo,TotalPrice,Executor,DeptID,ConfirmStatus,Confirmor,IsAccount ,Accountor ,SourceNo,AccountsStatus,CurrencyType,CurrencyRate,Note,ProjectID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@FeesNo ,@FeesNum,@FeesType,@InvoiceType,@CreateDate,@ContactUnits,@ContactType,@AcceWay,@SubjectsNo,@TotalPrice,@Executor,@DeptID,@ConfirmStatus,@Confirmor,@IsAccount ,@Accountor ,@SourceNo,@AccountsStatus,@CurrencyType,@CurrencyRate,@Note,@ProjectID) ");
            strSql.Append(" set @IntID= @@IDENTITY");

            //SqlParameter[] param = null;
            //ArrayList lcmd = new ArrayList();
            #region 参数
            //lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellOrderModel.CompanyCD));
            //lcmd.Add(SqlHelper.GetParameterFromString("@FeesNo ", sellOrderModel.FeesNo));
            //lcmd.Add(SqlHelper.GetParameterFromString("@FeesNum", sellOrderModel.FeesNum));
            //lcmd.Add(SqlHelper.GetParameterFromString("@FeesType", sellOrderModel.FeesType));
            //lcmd.Add(SqlHelper.GetParameterFromString("@InvoiceType", sellOrderModel.InvoiceType));
            //lcmd.Add(SqlHelper.GetParameterFromString("@CreateDate", sellOrderModel.CreateDate.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@ContactUnits", sellOrderModel.ContactUnits));
            //lcmd.Add(SqlHelper.GetParameterFromString("@ContactType", sellOrderModel.ContactType));
            //lcmd.Add(SqlHelper.GetParameterFromString("@AcceWay", sellOrderModel.AcceWay));
            //lcmd.Add(SqlHelper.GetParameterFromString("@SubjectsNo", sellOrderModel.SubjectsNo));
            //lcmd.Add(SqlHelper.GetParameterFromString("@TotalPrice", sellOrderModel.TotalPrice.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@Executor", sellOrderModel.Executor));
            //lcmd.Add(SqlHelper.GetParameterFromString("@DeptID", sellOrderModel.DeptID));
            //lcmd.Add(SqlHelper.GetParameterFromString("@ConfirmStatus", sellOrderModel.ConfirmStatus));
            //lcmd.Add(SqlHelper.GetParameterFromString("@Confirmor", sellOrderModel.Confirmor));
            //lcmd.Add(SqlHelper.GetParameterFromString("@ConfirmDate", sellOrderModel.ConfirmDate.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@IsAccount", sellOrderModel.IsAccount));
            //lcmd.Add(SqlHelper.GetParameterFromString("@Accountor", sellOrderModel.Accountor));
            //lcmd.Add(SqlHelper.GetParameterFromString("@AccountDate", sellOrderModel.AccountDate.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@SourceNo", sellOrderModel.SourceNo));
            //lcmd.Add(SqlHelper.GetParameterFromString("@AccountsStatus", sellOrderModel.AccountsStatus));
            //lcmd.Add(SqlHelper.GetParameterFromString("@CurrencyType", sellOrderModel.CurrencyType));
            //lcmd.Add(SqlHelper.GetParameterFromString("@CurrencyRate", sellOrderModel.CurrencyRate.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@Note", sellOrderModel.Note));
            //lcmd.Add(SqlHelper.GetParameterFromString("@IntID", sellOrderModel.ID));
            #endregion
            SqlParameter[] parms = new SqlParameter[24];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", sellOrderModel.CompanyCD);
            parms[1] = SqlHelper.GetParameter("@FeesNo", sellOrderModel.FeesNo);
            parms[2] = SqlHelper.GetParameter("@FeesNum", sellOrderModel.FeesNum);
            parms[3] = SqlHelper.GetParameter("@FeesType", sellOrderModel.FeesType);
            parms[4] = SqlHelper.GetParameter("@InvoiceType", sellOrderModel.InvoiceType);
            parms[5] = SqlHelper.GetParameter("@CreateDate", sellOrderModel.CreateDate);
            parms[6] = SqlHelper.GetParameter("@ContactUnits", sellOrderModel.ContactUnits);
            parms[7] = SqlHelper.GetParameter("@ContactType", sellOrderModel.ContactType);
            parms[8] = SqlHelper.GetParameter("@AcceWay", sellOrderModel.AcceWay);
            parms[9] = SqlHelper.GetParameter("@SubjectsNo", sellOrderModel.SubjectsNo);
            parms[10] = SqlHelper.GetParameter("@TotalPrice", sellOrderModel.TotalPrice);
            parms[11] = SqlHelper.GetParameter("@Executor", sellOrderModel.Executor);
            parms[12] = SqlHelper.GetParameter("@DeptID", sellOrderModel.DeptID);
            parms[13] = SqlHelper.GetParameter("@ConfirmStatus", sellOrderModel.ConfirmStatus);
            parms[14] = SqlHelper.GetParameter("@Confirmor", sellOrderModel.Confirmor);
            parms[15] = SqlHelper.GetParameter("@IsAccount", sellOrderModel.IsAccount);
            parms[16] = SqlHelper.GetParameter("@Accountor", sellOrderModel.Accountor);
            parms[17] = SqlHelper.GetParameter("@SourceNo", sellOrderModel.SourceNo);
            parms[18] = SqlHelper.GetParameter("@AccountsStatus", sellOrderModel.AccountsStatus);
            parms[19] = SqlHelper.GetParameter("@CurrencyType", sellOrderModel.CurrencyType);
            parms[20] = SqlHelper.GetParameter("@CurrencyRate", sellOrderModel.CurrencyRate);
            parms[21] = SqlHelper.GetParameter("@Note", sellOrderModel.Note);
            parms[22] = SqlHelper.GetParameter("@ProjectID", sellOrderModel.ProjectID);
            parms[23] = SqlHelper.GetOutputParameter("@IntID", SqlDbType.Int);
           
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parms);

            IntID = Convert.ToInt32(parms[23].Value);
        }
        #endregion
        #region 为明细表插入数据
        /// <summary>
        /// 为明细表插入数据
        /// </summary>
        /// <param name="sellOrderDetailModellList"></param>
        /// <param name="tran"></param>
        private static void InsertSellOrderDetail(List<FeesDetailModel> sellOrderDetailModellList, TransactionManager tran)
        {
            foreach (FeesDetailModel sellOrderDetailModel in sellOrderDetailModellList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into officedba.FeesDetail(");
                strSql.Append("CompanyCD,FeesNo,SortNo,FeeID,SubjectsNo,FeeTotal,Uses,OccurTime,Remark,ModifiedDate,ModifiedUserID)");
                strSql.Append(" values (");
                strSql.Append("@CompanyCD,@FeesNo,@SortNo,@FeeID,@SubjectsNo,@FeeTotal,@Uses,@OccurTime,@Remark,@ModifiedDate,@ModifiedUserID)");
                #region 参数
                SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@FeesNo ", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
                    new SqlParameter("@FeeID", SqlDbType.Int,4),
					new SqlParameter("@SubjectsNo", SqlDbType.VarChar,50),
					new SqlParameter("@FeeTotal", SqlDbType.Decimal,9),
					new SqlParameter("@Uses", SqlDbType.VarChar,200),
					new SqlParameter("@OccurTime", SqlDbType.DateTime,8),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime,8),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10)
				};
                parameters[0].Value = sellOrderDetailModel.CompanyCD;
                parameters[1].Value = sellOrderDetailModel.FeesNo;
                parameters[2].Value = Convert.ToInt32(sellOrderDetailModel.SortNo);
                parameters[3].Value = Convert.ToInt32(sellOrderDetailModel.FeeID);
                parameters[4].Value = sellOrderDetailModel.SubjectsNo;
                parameters[5].Value = sellOrderDetailModel.FeeTotal;
                parameters[6].Value = sellOrderDetailModel.Uses;
                parameters[7].Value = sellOrderDetailModel.OccurTime;
                parameters[8].Value = sellOrderDetailModel.Remark;
                parameters[9].Value = sellOrderDetailModel.ModifiedDate;
                parameters[10].Value = sellOrderDetailModel.ModifiedUserID;

                foreach (SqlParameter para in parameters)
                {
                    if (para.Value == null)
                    {
                        para.Value = DBNull.Value;
                    }
                }
                #endregion
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
            }
        }
        #endregion
        #region 更改源单开票状态
        private static void updateSourceByFeesType(string FeesType,string SourceNo, TransactionManager tran)
        {
            #region 对应表名和编号字段名
            string strTableName = "";
            string strBillNo = "";
            switch (FeesType)
            {
                case"1":
                    strTableName = "officedba.PurchaseOrder";//采购订单表
                    strBillNo = "OrderNo";
                    break;
                case "2":
                    strTableName = "officedba.PurchaseArrive";//采购到货单表
                    strBillNo = "ArriveNo";
                    break;
                case "3":
                    strTableName = "officedba.PurchaseReject";//采购退货单表
                    strBillNo = "RejectNo";
                    break;
                case "4":
                    strTableName = "officedba.SellOrder";//销售订单表
                    strBillNo = "OrderNo";
                    break;
                case "5":
                    strTableName = "officedba.SellSend";//销售发货通知单表
                    strBillNo = "SendNo";
                    break;
                case "6":
                    strTableName = "officedba.SellBack";//销售退货单表
                    strBillNo = "BackNo";
                    break;
                case "7":
                    strTableName = "officedba.FeeReturn";//费用报销单表
                    strBillNo = "ReimbNo";
                    break;
                case "8":
                    strTableName = "officedba.StorageOutSell";//销售出库单表
                    strBillNo = "OutNo";
                    break;
                case "9":
                    strTableName = "officedba.StorageOutOther";//其他出库单表
                    strBillNo = "OutNo";
                    break;
            }
            #endregion

            string[] OrderNos = SourceNo.Split(',');
            if (OrderNos.Length == 0)
            {
                return;
            }

            for (int i = 0; i < OrderNos.Length; i++)
            {
                //OrderNos[i] = "'" + OrderNos[i] + "'";
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update " + strTableName + "");
                strSql.Append(" set IsFeesAccount = '1' ");
                strSql.Append(" where  ");
                strSql.Append("" + strBillNo + " = @BillNo and CompanyCD = @CompanyCD");
                #region 参数
                SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@BillNo ", SqlDbType.VarChar,50)
					
				};
                parameters[0].Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                parameters[1].Value = OrderNos[i];
              

                foreach (SqlParameter para in parameters)
                {
                    if (para.Value == null)
                    {
                        para.Value = DBNull.Value;
                    }
                }
                #endregion
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
            }
        }
        #endregion

        #endregion

        #region 修改
        #region 根据单据状态判断单据是否可以被修改
        /// <summary>
        /// 根据单据状态判断单据是否可以被修改
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns>返回true时表示可以修改</returns>
        private static bool IsUpdate(string FeesNo)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            string strStatus = string.Empty;

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@FeesNo ", FeesNo ),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select TOP  1  ConfirmStatus  from officedba.Fees ";
            strSql += " WHERE CompanyCD = @CompanyCD AND FeesNo  = @FeesNo   ";

            object obj = SqlHelper.ExecuteScalar(strSql, paras);
            if (obj != null)
            {
                strStatus = obj.ToString();
                if (strStatus == "1")
                {
                    isSuc = false;
                }
                else
                {
                    isSuc = true;
                }
            }
            else
            {
                isSuc = true;
            }

            return isSuc;
        }
        #endregion

        #region 修改方法
        public static bool Update(FeesModel sellOrderModel, List<FeesDetailModel> sellOrderDetailModellList,out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            if (IsUpdate(sellOrderModel.FeesNo))
            {
                string strSql = "delete from officedba.FeesDetail where  FeesNo =@FeesNo   and CompanyCD=@CompanyCD";
                SqlParameter[] paras = { new SqlParameter("@FeesNo ", sellOrderModel.FeesNo ), new SqlParameter("@CompanyCD", sellOrderModel.CompanyCD) };
               // string strSql1 = "delete from officedba.SellOrderFeeDetail where  OrderNo=@OrderNo  and CompanyCD=@CompanyCD";
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    UpdateSellOrder(sellOrderModel, tran);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);                    
                    InsertSellOrderDetail(sellOrderDetailModellList, tran);                  
                    tran.Commit();
                    isSucc = true;
                    strMsg = "保存成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "保存失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                isSucc = false;
                strMsg = "非制单状态的未提交审批、审批未通过或撤销审批单据不可修改！";
            }
            return isSucc;
        }
        #endregion

        #region 更新主表数据
        /// <summary>
        /// 跟新主表数据
        /// </summary>
        /// <param name="sellOrderModel"></param>
        /// <param name="tran"></param>
        private static void UpdateSellOrder(FeesModel sellOrderModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            #region sql语句
            strSql.Append("update officedba.Fees set ");
            strSql.Append("FeesNum=@FeesNum,");
            strSql.Append("FeesType=@FeesType,");
            strSql.Append("InvoiceType=@InvoiceType,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("ContactUnits=@ContactUnits,");
            strSql.Append("ContactType=@ContactType,");
            strSql.Append("AcceWay=@AcceWay,");
            strSql.Append("SubjectsNo=@SubjectsNo,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("Executor=@Executor,");
            strSql.Append("DeptID=@DeptID,");
            strSql.Append("ConfirmStatus=@ConfirmStatus,");
            strSql.Append("Confirmor=@Confirmor,");
            strSql.Append("ConfirmDate=@ConfirmDate,");
            strSql.Append("IsAccount=@IsAccount ,");
            strSql.Append("Accountor =@Accountor ,");
            strSql.Append("AccountDate =@AccountDate ,");
            strSql.Append("SourceNo=@SourceNo,");
            strSql.Append("AccountsStatus=@AccountsStatus,");
            strSql.Append("CurrencyType=@CurrencyType,");
            strSql.Append("CurrencyRate=@CurrencyRate,");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("Note=@Note");
            strSql.Append(" where CompanyCD=@CompanyCD and FeesNo =@FeesNo  ");
            #endregion
           
            SqlParameter[] param = null;
            ArrayList lcmd = new ArrayList();
            #region 参数
            lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellOrderModel.CompanyCD));
            lcmd.Add(SqlHelper.GetParameterFromString("@FeesNo ", sellOrderModel.FeesNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@FeesNum", sellOrderModel.FeesNum));
            lcmd.Add(SqlHelper.GetParameterFromString("@FeesType", sellOrderModel.FeesType));
            lcmd.Add(SqlHelper.GetParameterFromString("@InvoiceType", sellOrderModel.InvoiceType));
            lcmd.Add(SqlHelper.GetParameterFromString("@CreateDate", sellOrderModel.CreateDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ContactUnits", sellOrderModel.ContactUnits));
            lcmd.Add(SqlHelper.GetParameterFromString("@ContactType", sellOrderModel.ContactType));
            lcmd.Add(SqlHelper.GetParameterFromString("@AcceWay", sellOrderModel.AcceWay));
            lcmd.Add(SqlHelper.GetParameterFromString("@SubjectsNo", sellOrderModel.SubjectsNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalPrice", sellOrderModel.TotalPrice.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Executor", sellOrderModel.Executor));
            lcmd.Add(SqlHelper.GetParameterFromString("@DeptID", sellOrderModel.DeptID));
            lcmd.Add(SqlHelper.GetParameterFromString("@ConfirmStatus", sellOrderModel.ConfirmStatus));
            lcmd.Add(SqlHelper.GetParameterFromString("@Confirmor", sellOrderModel.Confirmor));
            lcmd.Add(SqlHelper.GetParameterFromString("@ConfirmDate", sellOrderModel.ConfirmDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@IsAccount", sellOrderModel.IsAccount));
            lcmd.Add(SqlHelper.GetParameterFromString("@Accountor", sellOrderModel.Accountor));
            lcmd.Add(SqlHelper.GetParameterFromString("@AccountDate", sellOrderModel.AccountDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SourceNo", sellOrderModel.SourceNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@AccountsStatus", sellOrderModel.AccountsStatus));
            lcmd.Add(SqlHelper.GetParameterFromString("@CurrencyType", sellOrderModel.CurrencyType));
            lcmd.Add(SqlHelper.GetParameterFromString("@CurrencyRate", sellOrderModel.CurrencyRate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Note", sellOrderModel.Note));
            lcmd.Add(SqlHelper.GetParameterFromString("@ProjectID", sellOrderModel.ProjectID));
                        
            #endregion
          
            if (lcmd != null && lcmd.Count > 0)
            {
                param = new SqlParameter[lcmd.Count];
                for (int i = 0; i < lcmd.Count; i++)
                {
                    param[i] = (SqlParameter)lcmd[i];
                }
            }
            
            foreach (SqlParameter para in param)
            {
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }

            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
        }
        #endregion

        #endregion

        #region 费用票据列表
        public static DataTable GetFeesBySearch(string CompanyCD, FeesModel FeesM, string DateBegin, string DateEnd,string PriceB,string PriceE, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region 查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,a.CompanyCD                ");
                searchSql.AppendLine("      ,a.FeesNo                   ");
                searchSql.AppendLine(",(case a.InvoiceType when '1' then '增值税发票' when '2' then '普通地税' when '3' then '普通国税' else '收据' end)InvoiceType  ");
                searchSql.AppendLine(",(case a.FeesType                ");
                searchSql.AppendLine("  when '0' then (case a.ContactType ");
                searchSql.AppendLine("                  when '1' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ");
                searchSql.AppendLine("                  when '2' then (select CustName from officedba.CustInfo where id=a.ContactUnits) ");
                searchSql.AppendLine("                  when '3' then (select EmployeeName from officedba.EmployeeInfo where id=a.ContactUnits) ");
                searchSql.AppendLine("                 end) ");
                searchSql.AppendLine(" when '1' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ");
                searchSql.AppendLine(" when '2' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ");
                searchSql.AppendLine(" when '3' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ");
                searchSql.AppendLine(" when '4' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ");
                searchSql.AppendLine(" when '5' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ");
                searchSql.AppendLine(" when '6' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ");
                searchSql.AppendLine(" when '7' then (select EmployeeName from officedba.EmployeeInfo where id=a.ContactUnits) ");
                searchSql.AppendLine(" when '8' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ");
                searchSql.AppendLine(" when '9' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ");
                searchSql.AppendLine(" end) CustName ");
                searchSql.AppendLine("      ,a.ContactUnits,a.FeesType                   ");
                searchSql.AppendLine("      , CONVERT(varchar(100), a.CreateDate, 23) CreateDate ");
                searchSql.AppendLine("      ,a.TotalPrice,a.Confirmor,b.EmployeeName  ConfirmorName ");
                searchSql.AppendLine("      , CONVERT(varchar(100), a.ConfirmDate, 23) ConfirmDate ");
                searchSql.AppendLine("      ,(case a.IsAccount when '1' then '已登记' else '未登记' end)IsAccount  ");
                searchSql.AppendLine("  FROM officedba.Fees a ");
                searchSql.AppendLine("  left join officedba.EmployeeInfo b on b.id = a.Confirmor ");
                searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");

                #endregion 
                #region 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

                //所属项目
                if (!string.IsNullOrEmpty(FeesM.ProjectID))
                {
                    searchSql.AppendLine("	AND a.ProjectID =  @ProjectID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", FeesM.ProjectID));
                }

                //编号
                if (!string.IsNullOrEmpty(FeesM.FeesNo))
                {
                    searchSql.AppendLine("	AND a.FeesNo LIKE  '%' + @FeesNo + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FeesNo", FeesM.FeesNo));
                }
                //票据类型
                if (FeesM.InvoiceType != "")
                {
                    searchSql.AppendLine("	AND a.InvoiceType =  @InvoiceType ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@InvoiceType", FeesM.InvoiceType));
                }
                //开始时间
                if (DateBegin != "")
                {
                    searchSql.AppendLine("	AND a.CreateDate >= @DateBegin  ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateBegin", DateBegin));
                }
                //结束时间
                if (DateEnd != "")
                {
                    searchSql.AppendLine("	AND a.CreateDate <= @DateEnd  ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateEnd", DateEnd));
                }
                //金额B
                if (PriceB != "")
                {
                    searchSql.AppendLine("	AND a.TotalPrice >= @PriceB  ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@PriceB", PriceB));
                }
                //金额E
                if (PriceE != "")
                {
                    searchSql.AppendLine("	AND a.TotalPrice <= @PriceE  ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@PriceE", PriceE));
                }
                //源单类型
                if (FeesM.FeesType != "")
                {
                    searchSql.AppendLine("	AND a.FeesType =  @FeesType ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FeesType", FeesM.FeesType));
                }
                //确认状态
                if (FeesM.ConfirmStatus != "")
                {
                    searchSql.AppendLine("	AND a.ConfirmStatus =  @ConfirmStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmStatus", FeesM.ConfirmStatus));
                }
                //是否登记
                if (FeesM.IsAccount != "")
                {
                    searchSql.AppendLine("	AND a.IsAccount  =  @IsAccount  ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsAccount ", FeesM.IsAccount));
                }
                #endregion

                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 删除
        public static bool Del(string orderNos, out string strMsg, out string strFieldText)
        {
            string strCompanyCD = string.Empty;//单位编号
            bool isSucc = false;
            string allOrderNo = "";
            strMsg = "";
            strFieldText = "";
            bool bTemp = false;//单据是否可以被删除
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            TransactionManager tran = new TransactionManager();

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string[] orderNoS = null;
            orderNoS = orderNos.Split(',');

            for (int i = 0; i < orderNoS.Length; i++)
            {
                if (!IsFlow(orderNoS[i]))
                {
                    strFieldText += "单据：" + orderNoS[i] + "|";
                    strMsg += "已确认后的单据不允许删除！|";
                    bTemp = true;
                }

                orderNoS[i] = "'" + orderNoS[i] + "'";
                sb.Append(orderNoS[i]);
            }

            allOrderNo = sb.ToString().Replace("''", "','");
            if (!bTemp)
            {
                tran.BeginTransaction();
                try
                {
                    //源单改为“未开票”
                    UpdateSourceIsFeesAccount(tran.Trans, strCompanyCD, allOrderNo);

                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.Fees WHERE FeesNo  IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.FeesDetail WHERE FeesNo  IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                                        
                    tran.Commit();
                    isSucc = true;
                    strMsg = "删除成功！";

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "删除失败，请联系系统管理员！";
                    isSucc = false;
                    throw ex;
                }
            }
            else
            {
                isSucc = false;
            }
            return isSucc;
        }

        #region 根据费用票据编号取源单编号，并回写源单“开票状态”
        private static void UpdateSourceIsFeesAccount(SqlTransaction tran, string strCompanyCD, string BillNos)
        {
            #region 查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT a.ID ,a.CompanyCD,a.FeesNo,a.FeesType,a.SourceNo ");
            searchSql.AppendLine("  FROM officedba.Fees a ");
            searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD and  a.FeesNo in ( " + BillNos + " ) ");
            #endregion

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", strCompanyCD));//公司代码

            //设定comm的SQL文
            comm.CommandText = searchSql.ToString();

            DataTable dt = SqlHelper.ExecuteSearch(comm);
                       
            if (dt != null)
            {
                for (int j = 0; j < dt.Rows.Count;j++ )
                {
                    string TableName = "";//源单表名
                    string FieldName = "";//源单字段名（编号）
                    #region 判断源单表
                    switch (dt.Rows[j][3].ToString())
                    {
                        case "0":
                            break;
                        case"1":
                            TableName = "officedba.PurchaseOrder";
                            FieldName = "OrderNo";
                            break;
                        case "2":
                            TableName = "officedba.PurchaseArrive";
                            FieldName = "ArriveNo";
                            break;
                        case "3":
                            TableName = "officedba.PurchaseReject";
                            FieldName = "RejectNo";
                            break;
                        case "4":
                            TableName = "officedba.SellOrder";
                            FieldName = "OrderNo";
                            break;
                        case "5":
                            TableName = "officedba.SellSend";
                            FieldName = "SendNo";
                            break;
                        case "6":
                            TableName = "officedba.SellBack";
                            FieldName = "BackNo";
                            break;
                        case "7":
                            TableName = "officedba.FeeReturn";
                            FieldName = "ReimbNo";
                            break;
                        case "8":
                            TableName = "officedba.StorageOutSell";
                            FieldName = "OutNo";
                            break;
                        case "9":
                            TableName = "officedba.StorageOutOther";
                            FieldName = "OutNo";
                            break;
                    }
                    #endregion

                    //更改“开票状态”
                    UpdateSourde(tran,strCompanyCD, TableName,FieldName, dt.Rows[j][4].ToString());
                }
            }

            //UpdateSourde(tran, "T0004", "officedba.PurchaseOrder", "OrderNo", "CGDD20090028,");
        }
        #endregion

        #region 更改“开票状态”
        private static void UpdateSourde(SqlTransaction tran, string strCompanyCD, string strTableName, string FieldName, string FieldValue)
        {
            string[] orderNoS = null;
            orderNoS = FieldValue.Split(',');
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < orderNoS.Length; i++)
            {
                orderNoS[i] = "'" + orderNoS[i] + "'";
                sb.Append(orderNoS[i]);
            }

            string allOrderNo = "";
            allOrderNo = sb.ToString().Replace("''", "','");


            #region 拼写修改联系人信息SQL语句
            StringBuilder sqlcontact = new StringBuilder();
            sqlcontact.AppendLine("UPDATE " + strTableName + " set ");
            sqlcontact.AppendLine("IsFeesAccount = @IsFeesAccount  ");
            sqlcontact.AppendLine(" where CompanyCD = @CompanyCD ");
            sqlcontact.AppendLine(" and " + FieldName + " in ( " + allOrderNo + " ) ");
           
            

            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@CompanyCD", strCompanyCD);
            param[1] = SqlHelper.GetParameter("@IsFeesAccount", "0");
            param[2] = SqlHelper.GetParameter("@Nos", allOrderNo);

            #endregion

            //SqlHelper.ExecuteTransSql(sqlcontact.ToString(), param);
            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sqlcontact.ToString(), param);
        }
        #endregion

       
        #region 判断状态
        private static bool IsFlow(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@FeesNo ", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " SELECT COUNT(1) FROM officedba.Fees ";
            strSql += "  WHERE FeesNo = @FeesNo and ConfirmStatus  = '1' AND CompanyCD = @CompanyCD ";
            try
            {
                iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            }
            catch
            {
                isSuc = false;
            }

            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }
        #endregion

        #endregion

        #region 根据ID获取详细
        public static DataTable GetOrderInfo(int ID)
        {
            string strCompanyCD = string.Empty;//单位编号
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string strSql = "SELECT a.ID,a.FeesNo,a.FeesNum,a.FeesType,a.InvoiceType,a.ProjectID,pj.ProjectName,";
            strSql += " CONVERT(varchar(100), a.CreateDate, 23) CreateDate,a.ContactUnits,";
            strSql += "(case a.FeesType                ";
            strSql += "  when '0' then (case a.ContactType ";
            strSql += "                  when '1' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ";
            strSql += "                  when '2' then (select CustName from officedba.CustInfo where id=a.ContactUnits) ";
            strSql += "                  when '3' then (select EmployeeName from officedba.EmployeeInfo where id=a.ContactUnits) ";
            strSql += "                 end) ";
            strSql += " when '1' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ";
            strSql += " when '2' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ";
            strSql += " when '3' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ";
            strSql += " when '4' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ";
            strSql += " when '5' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ";
            strSql += " when '6' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ";
            strSql += " when '7' then (select EmployeeName from officedba.EmployeeInfo where id=a.ContactUnits) ";
            strSql += " when '8' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ";
            strSql += " when '9' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ";
            strSql += " end) CustName, ";
            strSql += " a.ContactType, a.AcceWay,a.SubjectsNo,f.SubjectsName,a.TotalPrice,a.Executor,b.EmployeeName ExecutorName, ";
            strSql += " a.DeptID,c.DeptName,a.ConfirmStatus,a.Confirmor,d.EmployeeName ConfirmorName,";
            strSql += " CONVERT(varchar(100), a.ConfirmDate, 23) ConfirmDate,a.IsAccount,a.Accountor,e.EmployeeName AccountorName,";
            strSql += " CONVERT(varchar(100), a.AccountDate , 23) AccountDate ,a.SourceNo,a.AccountsStatus,a.CurrencyType,";
            strSql += " a.CurrencyRate,a.Note ";
            strSql += "FROM officedba.Fees a ";
            strSql += " left join officedba.EmployeeInfo b ON a.Executor = b.ID ";
            strSql += " left join officedba.DeptInfo c ON a.DeptID = c.ID ";
            strSql += " left join officedba.EmployeeInfo d ON a.Confirmor = d.ID ";
            strSql += " left join officedba.EmployeeInfo e ON a.Accountor = e.ID ";
            strSql += " left join officedba.AccountSubjects f ON a.SubjectsNo = f.SubjectsCD and f.CompanyCD = a.CompanyCD ";
            strSql += " left join officedba.ProjectInfo pj on pj.ID=a.ProjectID ";
            strSql += "WHERE a.ID = @ID and a.CompanyCD = @CompanyCD ";

            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@ID", ID);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }
        #endregion

        #region 根据编号获取对应明细
        public static DataTable GetFeeDetail(string CompanyCD, string FeesNo)
        {
            try
            {
                #region 查询SQL拼写
                //查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,a.CompanyCD,a.FeesNo,a.SortNo      ");
                searchSql.AppendLine("      ,a.FeeID,b.CodeName,a.SubjectsNo,c.SubjectsName ");
                searchSql.AppendLine("      ,a.FeeTotal,a.Uses,convert(varchar(10),a.OccurTime,23) OccurTime,a.Remark ");
                searchSql.AppendLine(" from officedba.FeesDetail a");
                searchSql.AppendLine(" left join officedba.CodeFeeType b on b.id = a.FeeID ");
                searchSql.AppendLine(" left join officedba.AccountSubjects c on c.SubjectsCD = a.SubjectsNo and c.CompanyCD = a.CompanyCD ");
                searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");
                searchSql.AppendLine(" and a.FeesNo = @FeesNo");

                #endregion 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                //单据编号
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FeesNo", FeesNo));

                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.ExecuteSearch(comm);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 根据ID获取详细
        public static DataTable GetOrderInfoByNo(string BillNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string strSql = "SELECT a.ID,a.FeesNo,a.FeesNum,";
            strSql += " (case a.FeesType when '0' then '无来源' when '1' then '采购订单' when '2' then '采购到货单' when '3' then '采购退货单' when '4' then '销售订单' ";
            strSql += "  when '5' then '销售发货通知单' when '6' then '销售退货单' when '7' then '费用报销单' when '8' then '销售出库单' else '其他出库单' end)FeesType,";
            strSql += " (case a.InvoiceType when '1' then '增值税发票' when '2' then '普通地税' when '3' then '普通国税' else '收据' end)InvoiceType,";
            strSql += " CONVERT(varchar(100), a.CreateDate, 23) CreateDate,a.ContactUnits,";
            strSql += "(case a.FeesType                ";
            strSql += "  when '0' then (case a.ContactType ";
            strSql += "                  when '1' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ";
            strSql += "                  when '2' then (select CustName from officedba.CustInfo where id=a.ContactUnits) ";
            strSql += "                  when '3' then (select EmployeeName from officedba.EmployeeInfo where id=a.ContactUnits) ";
            strSql += "                 end) ";
            strSql += " when '1' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ";
            strSql += " when '2' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ";
            strSql += " when '3' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ";
            strSql += " when '4' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ";
            strSql += " when '5' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ";
            strSql += " when '6' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ";
            strSql += " when '7' then (select EmployeeName from officedba.EmployeeInfo where id=a.ContactUnits) ";
            strSql += " when '8' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ";
            strSql += " when '9' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ";
            strSql += " end) CustName, ";
            strSql += " (case a.ContactType when '1' then '供应商' when '2' then '客户' when '3' then '职员' end)ContactType,";
            strSql += " (case a.AcceWay when '0' then '未结算' when '1' then '已结算' when '2' then '结算中' end)AcceWay,";
            strSql += " a.SubjectsNo,f.SubjectsName,a.TotalPrice,a.Executor,b.EmployeeName ExecutorName, ";
            strSql += " (case a.ConfirmStatus when '0' then '未确认' when '1' then '已确认' end)ConfirmStatus,";
            strSql += " a.DeptID,c.DeptName,a.Confirmor,d.EmployeeName ConfirmorName,";
            strSql += " (case a.IsAccount when '0' then '未登记' when '1' then '已登记' end)IsAccount,";
            strSql += " CONVERT(varchar(100), a.ConfirmDate, 23) ConfirmDate,a.Accountor,e.EmployeeName AccountorName,";
            strSql += " CONVERT(varchar(100), a.AccountDate , 23) AccountDate ,a.SourceNo,a.CurrencyType,";
            strSql += " (case a.AccountsStatus when '0' then '未结算' when '1' then '已结算' else '结算中' end)AccountsStatus,";
            strSql += " a.CurrencyRate,g.CurrencyName,a.Note ";
            strSql += "FROM officedba.Fees a ";
            strSql += " left join officedba.EmployeeInfo b ON a.Executor = b.ID ";
            strSql += " left join officedba.DeptInfo c ON a.DeptID = c.ID ";
            strSql += " left join officedba.EmployeeInfo d ON a.Confirmor = d.ID ";
            strSql += " left join officedba.EmployeeInfo e ON a.Accountor = e.ID ";
            strSql += " left join officedba.AccountSubjects f ON a.SubjectsNo = f.SubjectsCD and f.CompanyCD = a.CompanyCD ";
            strSql += " left join officedba.CurrencyTypeSetting g ON a.CurrencyType = g.ID ";
            strSql += "WHERE a.FeesNo = @FeesNo and a.CompanyCD = @CompanyCD ";

            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@FeesNo", BillNo);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }
        #endregion

        #region 确认
        #region 确认主方法
        public static bool ConfirmFees(string OrderNO,FeesModel FeesM, List<FeesDetailModel> FeesDetailM, out string strMsg)
        {
            string  CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            //判断单据是否为制单状态，非制单状态不能确认
            if (isHandle(OrderNO, "0"))
            {
                //TransactionManager tran = new TransactionManager();
                //tran.BeginTransaction();
                try
                {                   
                    //判断是否启用自动生成凭证
                    //((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsApply;//自动审核登帐
                    //((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsVoucher;//自动生成凭证
                    if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsVoucher == true)
                    {
                        AttestBillModel Model = new AttestBillModel();//凭证主表实例
                        ArrayList DetailList = new ArrayList();//凭证明细数组
                        Model.CompanyCD = CompanyCD;
                        Model.FromName = "";
                        Model.Attachment = 1;//附件数
                        Model.AttestName = "记账凭证";//凭证名称
                        Model.VoucherDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));//凭证日期
                        Model.AttestNo = "记-" + VoucherDBHelper.GetMaxAttestNo(CompanyCD, DateTime.Now.ToString("yyyy-MM-dd"));//凭证号
                        Model.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//制单人
                        Model.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));//制单日期
                        Model.FromTbale = "officedba.Fees";//来源表名
                        Model.FromValue = FeesM.ID;//来源表主键

                        AttestBillDetailsModel DetailModel = new AttestBillDetailsModel();//凭证明细表实例
                        DetailModel.Abstract = FeesM.Note;// dt.Rows[0]["Abstract"].ToString();//摘要
                        DetailModel.CurrencyTypeID = Convert.ToInt32(FeesM.CurrencyType); //币种
                        DetailModel.ExchangeRate = FeesM.CurrencyRate;//汇率
                        DetailModel.SubjectsCD = FeesM.SubjectsNo;//科目编号
                        DetailModel.OriginalAmount = FeesM.TotalPrice;//原币金额
                        DetailModel.DebitAmount = 0;//借方金额
                        DetailModel.CreditAmount = FeesM.TotalPrice * Convert.ToDecimal(FeesM.CurrencyRate);//贷方金额

                        DetailModel.SubjectsDetails = "";
                        DetailModel.FormTBName = "";
                        DetailModel.FileName = "";
                        string Auciliary = VoucherDBHelper.GetSubjectsAuciliaryCD(FeesM.SubjectsNo, CompanyCD);

                        if (Auciliary == "供应商" || Auciliary == "客户" || Auciliary == "职员")
                        {
                            DetailModel.SubjectsDetails = FeesM.ContactUnits;//辅助核算
                            if (Auciliary == "供应商")
                            {
                                DetailModel.FormTBName = "officedba.ProviderInfo";
                                DetailModel.FileName = "CustName";
                            }
                            else if (Auciliary == "客户")
                            {
                                DetailModel.FormTBName = "officedba.CustInfo";
                                DetailModel.FileName = "CustName";
                            }
                            else
                            {
                                DetailModel.FormTBName = "officedba.EmployeeInfo";
                                DetailModel.FileName = "EmployeeName";
                            }
                        }
                        DetailList.Add(DetailModel);


                        //foreach (DataRow row in tempdt.Rows)//根据凭证模板构建凭证明细数组
                        foreach (FeesDetailModel FDetailM in FeesDetailM)
                        {
                            AttestBillDetailsModel DetailM = new AttestBillDetailsModel();//凭证明细表实例
                            DetailM.Abstract = FeesM.Note;//摘要
                            DetailM.CurrencyTypeID = Convert.ToInt32(FeesM.CurrencyType);//币种
                            DetailM.ExchangeRate = FeesM.CurrencyRate;//汇率
                            DetailM.SubjectsCD = FDetailM.SubjectsNo;//科目编号
                            DetailM.OriginalAmount = FDetailM.FeeTotal;//原币金额
                            DetailM.DebitAmount = FDetailM.FeeTotal * Convert.ToDecimal(FeesM.CurrencyRate);//借方金额
                            DetailM.CreditAmount = 0;//贷方金额

                            DetailM.SubjectsDetails = "";
                            DetailM.FormTBName = "";
                            DetailM.FileName = "";

                            DetailList.Add(DetailM);
                        }

                        //自动生成凭证并根据IsApply判断是否登帐 --生成成功
                        int BillID = 0;
                        string IsA = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsApply == true ? "1" : "0";
                        if (VoucherDBHelper.InsertIntoAttestBill(Model, DetailList, out BillID, IsA))
                        {

                            SqlParameter[] paras = new SqlParameter[3];
                            strSq = "update  officedba.Fees set ConfirmStatus='1' , Confirmor=@EmployeeID, ";
                            strSq += " ConfirmDate=getdate(),IsAccount='1',Accountor=@EmployeeID,AccountDate=getdate() ";

                            paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                            paras[1] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                            paras[2] = new SqlParameter("@FeesNo", OrderNO);

                            strSq += " WHERE FeesNo  = @FeesNo  and CompanyCD=@CompanyCD";

                            //SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);
                            SqlHelper.ExecuteTransSql(strSq.ToString(), paras);
                            if (SqlHelper.Result.OprateCount > 0)
                            {
                                isSuc = true;
                                strMsg = "确认成功！";
                            }
                            else
                            {
                                isSuc = false;
                                strMsg = "生成凭证失败！";
                            }
                        }
                    }
                    else
                    {
                        SqlParameter[] paras = new SqlParameter[3];
                        strSq = "update  officedba.Fees set ConfirmStatus='1' , Confirmor=@EmployeeID, ";
                        strSq += " ConfirmDate=getdate() ";

                        paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                        paras[1] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                        paras[2] = new SqlParameter("@FeesNo", OrderNO);

                        strSq += " WHERE FeesNo  = @FeesNo  and CompanyCD=@CompanyCD";

                        SqlHelper.ExecuteTransSql(strSq.ToString(), paras);
                        if (SqlHelper.Result.OprateCount > 0)
                        {
                            isSuc = true;
                            strMsg = "确认成功！";
                        }
                        else
                        {
                            isSuc = false;
                            strMsg = "确认失败！";
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    //tran.Rollback();

                    isSuc = false;
                    strMsg = "确认失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                isSuc = false;
                strMsg = "该单据已被其他用户确认，不可再次确认！";
            }
            return isSuc;
        }
        #endregion

        //判断确认状态
        private static bool isHandle(string FeesNo, string Status)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;

            strSql = "select count(1) from officedba.Fees where FeesNo  = @FeesNo  and CompanyCD=@CompanyCD and (ConfirmStatus=@ConfirmStatus or IsAccount = @IsAccount) ";
            ArrayList arr = new ArrayList();
            SqlParameter[] paras = new SqlParameter[4];
            paras[0] = new SqlParameter("@FeesNo", FeesNo);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            paras[2] = new SqlParameter("@ConfirmStatus", Status);
            paras[3] = new SqlParameter("@IsAccount ", "0");

            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount != 0)
            {
                isSuc = true;
            }
            return isSuc;
        }

        #endregion

        #region 导出费用票据列表
        public static DataTable ExportFeesBySearch(string CompanyCD, FeesModel FeesM, string DateBegin, string DateEnd, string PriceB, string PriceE)
        {
            try
            {
                #region 查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,a.CompanyCD                ");
                searchSql.AppendLine("      ,a.FeesNo                   ");
                searchSql.AppendLine(",(case a.InvoiceType when '1' then '增值税发票' when '2' then '普通地税' when '3' then '普通国税' else '收据' end)InvoiceType  ");
                searchSql.AppendLine(",(case a.FeesType                ");
                searchSql.AppendLine("  when '0' then (case a.ContactType ");
                searchSql.AppendLine("                  when '1' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ");
                searchSql.AppendLine("                  when '2' then (select CustName from officedba.CustInfo where id=a.ContactUnits) ");
                searchSql.AppendLine("                  when '3' then (select EmployeeName from officedba.EmployeeInfo where id=a.ContactUnits) ");
                searchSql.AppendLine("                 end) ");
                searchSql.AppendLine(" when '1' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ");
                searchSql.AppendLine(" when '2' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ");
                searchSql.AppendLine(" when '3' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ");
                searchSql.AppendLine(" when '4' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ");
                searchSql.AppendLine(" when '5' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ");
                searchSql.AppendLine(" when '6' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ");
                searchSql.AppendLine(" when '7' then (select EmployeeName from officedba.EmployeeInfo where id=a.ContactUnits) ");
                searchSql.AppendLine(" when '8' then (select CustName from officedba.CustInfo where id=a.ContactUnits)  ");
                searchSql.AppendLine(" when '9' then (select CustName from officedba.ProviderInfo where id=a.ContactUnits) ");
                searchSql.AppendLine(" end) CustName ");
                searchSql.AppendLine("      ,a.ContactUnits                   ");
                searchSql.AppendLine("      , CONVERT(varchar(100), a.CreateDate, 23) CreateDate ");
                searchSql.AppendLine("      ,a.TotalPrice,a.Confirmor,b.EmployeeName  ConfirmorName ");
                searchSql.AppendLine("      , CONVERT(varchar(100), a.ConfirmDate, 23) ConfirmDate ");
                searchSql.AppendLine("      ,(case a.IsAccount when '1' then '已登记' else '未登记' end)IsAccount  ");
                searchSql.AppendLine("  FROM officedba.Fees a ");
                searchSql.AppendLine("  left join officedba.EmployeeInfo b on b.id = a.Confirmor ");
                searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");

                #endregion
                #region 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

                //所属项目
                if (!string.IsNullOrEmpty(FeesM.ProjectID))
                {
                    searchSql.AppendLine("	AND a.ProjectID =  @ProjectID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", FeesM.ProjectID));
                }

                //编号
                if (!string.IsNullOrEmpty(FeesM.FeesNo))
                {
                    searchSql.AppendLine("	AND a.FeesNo LIKE  '%' + @FeesNo + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FeesNo", FeesM.FeesNo));
                }
                //票据类型
                if (FeesM.InvoiceType != "")
                {
                    searchSql.AppendLine("	AND a.InvoiceType =  @InvoiceType ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@InvoiceType", FeesM.InvoiceType));
                }
                //开始时间
                if (DateBegin != "")
                {
                    searchSql.AppendLine("	AND a.CreateDate >= @DateBegin  ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateBegin", DateBegin));
                }
                //结束时间
                if (DateEnd != "")
                {
                    searchSql.AppendLine("	AND a.CreateDate <= @DateEnd  ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateEnd", DateEnd));
                }
                //金额B
                if (PriceB != "")
                {
                    searchSql.AppendLine("	AND a.TotalPrice >= @PriceB  ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@PriceB", PriceB));
                }
                //金额E
                if (PriceE != "")
                {
                    searchSql.AppendLine("	AND a.TotalPrice <= @PriceE  ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@PriceE", PriceE));
                }
                //源单类型
                if (FeesM.FeesType != "")
                {
                    searchSql.AppendLine("	AND a.FeesType =  @FeesType ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FeesType", FeesM.FeesType));
                }
                //确认状态
                if (FeesM.ConfirmStatus != "")
                {
                    searchSql.AppendLine("	AND a.ConfirmStatus =  @ConfirmStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmStatus", FeesM.ConfirmStatus));
                }
                //是否登记
                if (FeesM.IsAccount != "")
                {
                    searchSql.AppendLine("	AND a.IsAccount  =  @IsAccount  ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsAccount ", FeesM.IsAccount));
                }
                #endregion

                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.ExecuteSearch(comm);
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}

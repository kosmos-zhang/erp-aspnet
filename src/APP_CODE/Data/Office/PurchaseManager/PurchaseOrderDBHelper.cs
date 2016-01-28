/**********************************************
 * 类作用：   采购订单数据库层处理
 * 建立人：   王超
 * 建立时间： 2009/04/16
  * 修改人：   王保军                          *
 * 修改时间： 2009/08/27                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using System.Data.SqlClient;
using XBase.Model.Office.PurchaseManager;
using XBase.Common;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Collections;

namespace XBase.Data.Office.PurchaseManager
{
    public class PurchaseOrderDBHelper
    {
        #region 查询采购订单
        /// <summary>
        /// 根据检索条件检索出满足条件的信息 Added By jiangym 2009-04-16
        /// </summary>
        /// <param name="OrderNo">订单号</param>
        /// <param name="Title">主题</param>
        /// <param name="CustName">客户</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns>DataTable</returns>
        public static DataTable SearchOrderByCondition(string CompanyCD, string OrderNo, string Title,
            string CustName, string StartDate, string EndDate)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from officedba.GetPurchaseOrderInfo ");
            sql.AppendLine("where CompanyCD=@CompanyCD");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //订单编号
            if (!string.IsNullOrEmpty(OrderNo))
            {
                sql.AppendLine(" AND OrderNo LIKE @OrderNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", "%" + OrderNo + "%"));
            }
            //主题
            if (!string.IsNullOrEmpty(Title))
            {
                sql.AppendLine(" AND Title LIKE @Title ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + Title + "%"));
            }
            //客户名称
            if (!string.IsNullOrEmpty(CustName))
            {
                sql.AppendLine(" AND CustID = @CustName ");
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


        #region 查询采购订单
        /// <summary>
        /// 根据检索条件检索出满足条件的信息 Added By Moshenlin 2009-04-16
        /// </summary>
        /// <param name="OrderNo">订单号</param>
        /// <param name="Title">主题</param>
        /// <param name="CustName">客户</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns>DataTable</returns>
        public static DataTable SearchOrderByCondition(string CompanyCD, string OrderNo, string Title,
            string CustName, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from officedba.GetPurchaseOrderInfo ");
            sql.AppendLine("where CompanyCD=@CompanyCD");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //订单编号
            if (!string.IsNullOrEmpty(OrderNo))
            {
                sql.AppendLine(" AND OrderNo LIKE @OrderNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", "%" + OrderNo + "%"));
            }
            //主题
            if (!string.IsNullOrEmpty(Title))
            {
                sql.AppendLine(" AND Title LIKE @Title ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + Title + "%"));
            }
            //客户名称
            if (!string.IsNullOrEmpty(CustName))
            {
                sql.AppendLine(" AND CustID = @CustName ");
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


        #region
        /// <summary>
        /// 根据检索条件检索出满足条件的信息 Added By moshenlin 2009-06-29
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable SearchOrderByCondition(string ids, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from officedba.GetPurchaseOrderInfo ");
            sql.AppendLine("where CompanyCD=@CompanyCD and ID in (" + ids + ") order by CreateDate asc ");
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


        #region 更新开票状态
        /// <summary>
        /// 更新开票状态  Added By jiangym 2009-04-22
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns></returns>
        public static bool UpdateisOpenBill(string ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Update officedba.PurchaseOrder set isOpenbill='1'");
            sql.AppendLine("where ID In( " + ID + ") ");


            SqlHelper.ExecuteTransSql(sql.ToString());
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        #region 查询采购计划
        /// <summary>
        /// 查询指定供应商下的采购计划
        /// </summary>
        /// <param name="ProviderID">供应商ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetPurPlanByProvider(string CompanyCD, int ProviderID, string ProductNo, string ProductName, string StartDate, string EndDate
            , int pageIndex, int pageCount, string OrderBy, string OrderByType, ref int totalCount)
        {
            SqlParameter[] parms = new SqlParameter[11];
            parms[0] = new SqlParameter("@CompanyCD", CompanyCD);
            parms[1] = new SqlParameter("@ProviderID", ProviderID);
            parms[2] = new SqlParameter("@ProductNo", ProductNo);
            parms[3] = new SqlParameter("@ProductName", ProductName);
            parms[4] = new SqlParameter("@StartDate", StartDate);
            parms[5] = new SqlParameter("@EndDate", EndDate);
            parms[6] = new SqlParameter("@pageIndex", pageIndex);
            parms[7] = new SqlParameter("@pageCount", pageCount);
            parms[8] = new SqlParameter("@OrderBy", OrderBy);
            parms[9] = new SqlParameter("@OrderByType", OrderByType);
            parms[10] = MakeParam("@totalCount", SqlDbType.Int, 0, ParameterDirection.Output, null);

            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[ProcgetPurPlan]", parms);
            totalCount = int.Parse(parms[10].Value.ToString());
            return dt;

        }
        /// <summary>
        /// Make stored procedure param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <param name="Direction">Parm direction.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>
        public static SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size,
            ParameterDirection Direction, object Value)
        {
            SqlParameter param;

            if (Size > 0)
                param = new SqlParameter(ParamName, DbType, Size);
            else
                param = new SqlParameter(ParamName, DbType);

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;

            return param;
        }
        #endregion


        #region 合同控件取值
        public static DataTable GetContractDetail(string CompanyCD, string ContractNo, string Title, int ProviderID, int Currency, int pageIndex, int pageSize, string OrderBy, out int totalRecord)
        {
            try
            {
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

                //查询该公司的单据状态为‘2’，订购数量小于合同数量的明细
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM (SELECT   ROW_NUMBER() OVER (ORDER BY A.ID,B.SortNo) AS Pos,A.ID,A.ContractNo, B.SortNo, B.ProductID, C.ProdNo AS ProductNo, C.ProductName, C.Specification, C.UnitID, ISNULL(D.CodeName,'') AS UnitName, Convert(numeric(14," + userInfo.SelPoint + "),isnull(B.ProductCount,0)) as ProductCount , ");
                sql.AppendLine("        Convert(numeric(14," + userInfo.SelPoint + "),isnull(B.OrderCount,0)) as OrderCount        , Convert(numeric(14," + userInfo.SelPoint + "),isnull(B.UnitPrice,0)) as UnitPrice        , Convert(numeric(22," + userInfo.SelPoint + "),isnull(B.TaxPrice,0)) as TaxPrice, Convert(numeric(12," + userInfo.SelPoint + "),isnull(B.Discount,0)) as Discount, Convert(numeric(12," + userInfo.SelPoint + "),isnull(B.TaxRate,0)) as TaxRate  ,  Convert(numeric(22," + userInfo.SelPoint + "),isnull(B.TotalFee,0)) as TotalFee ,   Convert(numeric(22," + userInfo.SelPoint + "),isnull(B.TotalPrice,0)) as TotalPrice      , Convert(numeric(22," + userInfo.SelPoint + "),isnull(B.TotalTax,0)) as TotalTax        , CONVERT(VARCHAR(10),B.RequireDate,21) AS RequireDate , B.ApplyReason   ,B.UsedUnitID     ,Convert(numeric(14," + userInfo.SelPoint + "), isnull(B.UsedUnitCount,0) ) as UsedUnitCount ,  Convert(numeric(14," + userInfo.SelPoint + "),isnull(B.UsedPrice,0)) as UsedPrice   ,ISNULL(FF.CodeName,'') AS UsedUnitName,isnull(HF.TypeName,'') as ColorName                                                                   ");
                sql.AppendLine("FROM         officedba.PurchaseContract AS A  left outer   JOIN                                                                                                                                                              ");
                sql.AppendLine("                      officedba.PurchaseContractDetail AS B ON A.CompanyCD = B.CompanyCD AND A.ContractNo = B.ContractNo AND ISNULL(B.ProductCount, 0)                                                                  ");
                sql.AppendLine("                      > ISNULL(B.OrderCount, 0) left outer   JOIN                                                                                                                                                           ");
                sql.AppendLine("                      officedba.ProductInfo AS C ON B.ProductID = C.ID left outer   JOIN                                                                                                                                     ");
                sql.AppendLine("                      officedba.CodeUnitType AS D ON C.UnitID = D.ID   left outer   JOIN                                                                                                                                             ");
                sql.AppendLine("                      officedba.CodeUnitType AS FF ON FF.ID = B.UsedUnitID      ");

                sql.AppendLine(" left join officedba.CodePublicType HF on C.ColorID=HF.ID  ");

                sql.AppendLine("WHERE     A.BillStatus = '2'  AND ISNULL(B.ProductCount, 0)  > ISNULL(B.OrderCount, 0)  AND A.CompanyCD=@CompanyCD ");

                ArrayList parmList = new ArrayList();
                if (!string.IsNullOrEmpty(ContractNo))
                {
                    sql.AppendLine(" AND A.ContractNo like @ContractNo");
                    parmList.Add(new SqlParameter("@ContractNo", "%" + ContractNo + "%"));
                }
                if (!string.IsNullOrEmpty(Title))
                {
                    sql.AppendLine(" AND A.Title like @Title");
                    parmList.Add(new SqlParameter("@Title", "%" + Title + "%"));
                }
                if (ProviderID != 0)
                {
                    sql.AppendLine(" AND A.ProviderID=@ProviderID");
                    parmList.Add(new SqlParameter("@ProviderID", ProviderID));
                }
                if (Currency != 0)
                {
                    sql.AppendLine(" AND A.CurrencyType=@CurrencyType");
                    parmList.Add(new SqlParameter("@CurrencyType", Currency));
                }

                sql.AppendLine(" ) AS FFFF WHERE Pos Between ''+CAST((@pageIndex-1)*@pageSize+1 AS int)+'' AND ''+CAST(@pageIndex*@pageSize AS int)+''");
                sql.AppendLine("SELECT @totalCount=COUNT(*) FROM officedba.PurchaseContract AS A                                                                                                                  ");
                sql.AppendLine("left outer   JOIN  officedba.PurchaseContractDetail AS B ON A.CompanyCD = B.CompanyCD AND A.ContractNo = B.ContractNo AND ISNULL(B.ProductCount, 0) > ISNULL(B.OrderCount, 0) ");
                sql.AppendLine("left outer   JOIN  officedba.ProductInfo AS C ON B.ProductID = C.ID                                                                                                           ");
                sql.AppendLine("left outer   JOIN  officedba.CodeUnitType AS D ON C.UnitID = D.ID                                                                                                             ");
                sql.AppendLine("WHERE     A.BillStatus = '2'                                                                                                                                          ");
                if (!string.IsNullOrEmpty(ContractNo))
                {
                    sql.AppendLine(" AND A.ContractNo like @ContractNo");
                }
                if (!string.IsNullOrEmpty(Title))
                {
                    sql.AppendLine(" AND A.Title like @Title");
                }
                if (ProviderID != 0)
                {
                    sql.AppendLine(" AND A.ProviderID=@ProviderID");
                }
                if (Currency != 0)
                {
                    sql.AppendLine(" AND A.CurrencyType=@CurrencyType");
                }



                SqlCommand comm = new SqlCommand();
                parmList.Add(new SqlParameter("@CompanyCD", CompanyCD));
                parmList.Add(new SqlParameter("@pageIndex", pageIndex));
                parmList.Add(new SqlParameter("@pageSize", pageSize));
                parmList.Add(new SqlParameter("@OrderBy", OrderBy));
                SqlParameter parm = MakeParam("@totalCount", SqlDbType.Int, 0, ParameterDirection.Output, null);
                parmList.Add(parm);

                //comm.CommandText = sql.ToString();
                //comm.Parameters.AddRange(
                DataTable dt = SqlHelper.ExecuteSql(sql.ToString(), parmList);
                totalRecord = int.Parse(parm.Value.ToString());
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //主表取值
        public static DataTable GetContract(string CompanyCD, int ID)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT  A.ProviderID, B.CustName AS ProviderName, A.Seller AS Purchaser, C.EmployeeName AS PurchaserName, A.TheyDelegate, A.OurDelegate,");
                sql.AppendLine("      D.EmployeeName AS OurDelegateName, A.SignAddr, A.DeptID, E.DeptName, A.TakeType, A.CarryType, A.isAddTax, A.Discount, A.TypeID,   ");
                sql.AppendLine("                      A.MoneyType, A.PayType, A.CurrencyType, A.Rate                                                                    ");
                sql.AppendLine("FROM         officedba.PurchaseContract AS A LEFT JOIN                                                                                  ");
                sql.AppendLine("                      officedba.ProviderInfo AS B ON A.ProviderID = B.ID LEFT JOIN                                                      ");
                sql.AppendLine("                      officedba.EmployeeInfo AS C ON A.Seller = C.ID LEFT JOIN                                                          ");
                sql.AppendLine("                      officedba.DeptInfo AS E ON A.DeptID = E.ID LEFT JOIN                                                              ");
                sql.AppendLine("                      officedba.EmployeeInfo AS D ON A.OurDelegate = D.ID                                                               ");
                sql.AppendLine(" WHERE A.CompanyCD=@CompanyCD AND A.ID=@ID ");

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sql.ToString();
                SqlParameter[] parms = new SqlParameter[2];
                parms[0] = new SqlParameter("@CompanyCD", CompanyCD);
                parms[1] = new SqlParameter("@ID", ID);

                comm.Parameters.AddRange(parms);
                return SqlHelper.ExecuteSearch(comm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据采购合同编号查询采购合同明细
        /// <summary>
        /// 根据采购合同编号查询采购合同明细
        /// </summary>
        /// <param name="ContractNo">采购合同编号</param>
        /// <returns>DataTable</returns>
        /// 

        public static DataTable GetPurOrderDetailByContractNo(string ContractNo)
        {
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT isnull(A.ProductID    ,0) AS ProductID                    ");
            sql.AppendLine("     , isnull(A.ProductNo    ,'') AS ProductNo                    ");
            sql.AppendLine("     , isnull(A.ProductName  ,'') AS ProductName                  ");
            sql.AppendLine("     , isnull(A.standard     ,'') AS standard                     ");
            sql.AppendLine("     , isnull(A.ProductCount ,0) AS ProductCount                 ");
            sql.AppendLine("     , isnull(A.OrderCount,0) AS OrderCount                      ");
            sql.AppendLine("     , isnull(A.UnitID       ,0) AS UnitID                       ");
            sql.AppendLine("     , isnull(C.CodeName     ,'') AS UnitName                     ");
            sql.AppendLine("     , isnull(A.UnitPrice    ,0) AS UnitPrice                    ");
            sql.AppendLine("     , isnull(A.TotalPrice   ,0) AS TotalPrice                   ");
            sql.AppendLine("     , isnull(CONVERT(varchar(100), A.RequireDate, 23)  ,'') AS RequireDate                  ");
            sql.AppendLine("     , isnull(A.TaxPrice     ,0) AS TaxPrice                     ");
            sql.AppendLine("     , isnull(A.Discount     ,0) AS Discount                     ");
            sql.AppendLine("     , isnull(A.TaxRate      ,0) AS TaxRate                      ");
            sql.AppendLine("     , isnull(A.TotalFee     ,0) AS TotalFee                     ");
            sql.AppendLine("     , isnull(A.TotalTax     ,0) AS TotalTax                     ");
            sql.AppendLine("     , isnull(B.ID           ,0) AS FromBillID                   ");
            sql.AppendLine("     , isnull(A.ContractNo   ,'') AS FromBillNo                   ");
            sql.AppendLine("     , isnull(A.SortNo   ,'') AS FromLineNo                   ");
            sql.AppendLine("     , isnull(A.Remark   ,'') AS Remark                   ");
            sql.AppendLine(",isnull(B.Seller,0) AS Purchaser              ");
            sql.AppendLine(" ,isnull(D.EmployeeName,'') AS PurchaserName  ");
            sql.AppendLine(",isnull(B.DeptID ,0) AS DeptID                ");
            sql.AppendLine(",isnull(E.DeptName ,'') AS  DeptName          ");

            sql.AppendLine("FROM officedba.PurchaseContractDetail AS A                       ");
            sql.AppendLine("  INNER JOIN officedba.PurchaseContract AS B                ");
            sql.AppendLine("ON A.CompanyCD = B.CompanyCD AND A.ContractNo = B.ContractNo AND B.BillStatus='2'");
            sql.AppendLine("  LEFT OUTER JOIN officedba.CodeUnitType AS C ON A.UnitID = C.ID ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON B.Seller=D.ID ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS E ON B.DeptID=E.ID     ");


            sql.AppendLine(" WHERE A.CompanyCD=@CompanyCD");
            sql.AppendLine(" AND A.ContractNo=@ContractNo");
            //sql.AppendLine(" AND A.BillStatus=@BillStatus");
            #endregion

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractNo", ContractNo));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 根据供应商ID查询采购合同主表信息
        /// <summary>
        /// 根据供应商ID查询采购合同主表信息
        /// </summary>
        /// <param name="ProviderID">供应商ID</param>
        /// <returns>DataTable</returns>
        /// 
        public static DataTable GetPurContract(string CompanyCD, string ProviderName, string ContractNo, string Title, string StartDate, string EndDate
            , int pageIndex, int pageCount, string OrderBy, string OrderByType, ref int totalCount)
        {
            SqlParameter[] parms = new SqlParameter[11];
            int i = 0;
            parms[i++] = new SqlParameter("@CompanyCD", CompanyCD);
            parms[i++] = new SqlParameter("@ProviderName", ProviderName);
            parms[i++] = new SqlParameter("@ContractNo", ContractNo);
            parms[i++] = new SqlParameter("@Title", Title);
            parms[i++] = new SqlParameter("@StartDate", StartDate);
            parms[i++] = new SqlParameter("@EndDate", EndDate);
            parms[i++] = new SqlParameter("@pageIndex", pageIndex);
            parms[i++] = new SqlParameter("@pageCount", pageCount);
            parms[i++] = new SqlParameter("@OrderBy", OrderBy);
            parms[i++] = new SqlParameter("@OrderByType", OrderByType);
            parms[i++] = MakeParam("@totalCount", SqlDbType.Int, 0, ParameterDirection.Output, null);

            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[ProcgetPurCon]", parms);
            totalCount = int.Parse(parms[10].Value.ToString());
            return dt;
        }
        #endregion

        #region 币种
        public static DataTable GetCurrenyType()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ID                                           ");
            sql.AppendLine("      ,isnull(CompanyCD      ,'') AS CompanyCD      ");
            sql.AppendLine("      ,isnull(CurrencyName   ,'') AS CurrencyName   ");
            sql.AppendLine("      ,isnull(CurrencySymbol ,'') AS CurrencySymbol ");
            sql.AppendLine("      ,isnull(isMaster       ,'') AS isMaster       ");
            sql.AppendLine("      ,isnull(ExchangeRate   ,1) AS ExchangeRate    ");
            sql.AppendLine("      ,(CONVERT(varchar(50),ID))+'_'+(CONVERT(varchar(50),ExchangeRate)) as hhh");
            sql.AppendLine("      ,isnull(ConvertWay     ,0) AS ConvertWay      ");
            sql.AppendLine("      ,isnull(ChangeTime     ,'') AS ChangeTime     ");
            sql.AppendLine("      ,isnull(UsedStatus     ,'') AS UsedStatus     ");
            sql.AppendLine("  FROM officedba.CurrencyTypeSetting                ");
            sql.AppendLine(" WHERE  CompanyCD=@CompanyCD              ");
            sql.AppendLine(" AND UsedStatus=@UsedStatus            ");
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", "1"));

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 采购订单操作

        #region 判断单据可不可以保存
        public static bool CanSave(List<PurchaseOrderDetailModel> PurchaseOrderDetailMList, out string Reason)
        {
            try
            {
                //判断引用源单的数量是不是超过了可用数量
                foreach (PurchaseOrderDetailModel PurchaseOrderDetailM in PurchaseOrderDetailMList)
                {
                    switch (PurchaseOrderDetailM.FromType)
                    {
                        case "2":
                            //来源计划
                            {
                                StringBuilder sql = new StringBuilder();
                                sql.AppendLine("SELECT  isnull(SUM(isnull(B.ProductCount,0)),0) AS ProductCount,isnull(SUM(isnull(B.OrderCount,0)),0) AS OrderCount  FROM officedba.PurchasePlan AS A ");
                                sql.AppendLine(" INNER JOIN officedba.PurchasePlanDetail AS B ON A.CompanyCD = B.CompanyCD AND ");
                                sql.AppendLine(" A.PlanNo = B.PlanNo AND  A.ID=@ID AND B.SortNo=@SortNo ");
                                //sql.AppendLine(" AND ISNULL(B.ProductCount, 0) - ISNULL(B.OrderCount, 0) <@ProductCount");
                                sql.AppendLine(" WHERE A.CompanyCD=@CompanyCD");

                                SqlCommand comm = new SqlCommand();
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", PurchaseOrderDetailM.CompanyCD));
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", PurchaseOrderDetailM.FromBillID));
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", PurchaseOrderDetailM.FromLineNo));
                                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount",PurchaseOrderDetailM.ProductCount));

                                comm.CommandText = sql.ToString();
                                DataTable dt = SqlHelper.ExecuteSearch(comm);
                                double ddd = Convert.ToDouble(dt.Rows[0]["ProductCount"].ToString()) - Convert.ToDouble(dt.Rows[0]["OrderCount"].ToString());
                                if (ddd - Convert.ToDouble(PurchaseOrderDetailM.ProductCount) < -0.01)
                                {
                                    Reason = "第" + PurchaseOrderDetailM.SortNo + "行的采购数量不能大于当前可用的采购数量:" + ddd.ToString() + "";
                                    return false;
                                }
                                break;
                            }
                        case "4":
                            //来源合同
                            {
                                StringBuilder sql = new StringBuilder();
                                sql.AppendLine(" SELECT isnull(SUM(ISNULL(B.ProductCount, 0)), 0) AS ProductCount, isnull(SUM(ISNULL(B.OrderCount, 0)), 0) AS OrderCount");
                                sql.AppendLine(" FROM officedba.PurchaseContract AS A INNER JOIN");
                                sql.AppendLine(" officedba.PurchaseContractDetail AS B ON A.CompanyCD = B.CompanyCD AND A.ContractNo = B.ContractNo AND  A.ID=@ID AND B.SortNo=@SortNo");
                                sql.AppendLine(" WHERE A.CompanyCD=@CompanyCD");

                                SqlCommand comm = new SqlCommand();
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", PurchaseOrderDetailM.CompanyCD));
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", PurchaseOrderDetailM.FromBillID));
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", PurchaseOrderDetailM.SortNo));

                                comm.CommandText = sql.ToString();
                                DataTable dt = SqlHelper.ExecuteSearch(comm);
                                double ff = Convert.ToDouble(dt.Rows[0]["ProductCount"].ToString()) - Convert.ToDouble(dt.Rows[0]["OrderCount"].ToString());
                                if (ff - Convert.ToDouble(PurchaseOrderDetailM.ProductCount) < -0.01)
                                {
                                    Reason = "第" + PurchaseOrderDetailM.SortNo + "行的采购数量不能大于当前可用的采购数量:" + ff.ToString() + "";
                                    return false;
                                }
                                break;
                            }
                        default:
                            break;

                    }

                }
                Reason = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region insert
        public static SqlCommand InsertPurchaseOrder(PurchaseOrderModel PurchaseOrderM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sqlPri = new StringBuilder();
            sqlPri.AppendLine("INSERT INTO officedba.PurchaseOrder ");
            sqlPri.AppendLine("           (CompanyCD               ");
            sqlPri.AppendLine("           ,OrderNo                 ");
            sqlPri.AppendLine("           ,Title                   ");
            sqlPri.AppendLine("           ,TypeID                  ");
            sqlPri.AppendLine("           ,FromType                ");
            sqlPri.AppendLine("           ,CurrencyType            ");
            sqlPri.AppendLine("           ,Rate                    ");
            sqlPri.AppendLine("           ,ProjectID                      ");
            //sqlPri.AppendLine("           ,PurchaseDate            ");
            sqlPri.AppendLine("           ,OrderDate               ");
            sqlPri.AppendLine("           ,TheyDelegate            ");
            sqlPri.AppendLine("           ,OurDelegate             ");
            sqlPri.AppendLine("           ,ProviderID              ");
            sqlPri.AppendLine("           ,DeptID                  ");
            sqlPri.AppendLine("           ,PayType                 ");
            sqlPri.AppendLine("           ,MoneyType               ");
            sqlPri.AppendLine("           ,Purchaser               ");
            sqlPri.AppendLine("           ,TakeType                ");
            sqlPri.AppendLine("           ,CarryType               ");
            sqlPri.AppendLine("           ,ProviderBillID          ");
            sqlPri.AppendLine("           ,remark                  ");
            sqlPri.AppendLine("           ,TotalPrice              ");
            sqlPri.AppendLine("           ,TotalTax                ");
            sqlPri.AppendLine("           ,TotalFee                ");
            sqlPri.AppendLine("           ,Discount                ");
            sqlPri.AppendLine("           ,DiscountTotal           ");
            sqlPri.AppendLine("           ,OtherTotal              ");
            sqlPri.AppendLine("           ,RealTotal               ");
            sqlPri.AppendLine("           ,isAddTax                ");
            sqlPri.AppendLine("           ,CountTotal              ");
            sqlPri.AppendLine("           ,isOpenbill              ");
            sqlPri.AppendLine("           ,Confirmor               ");
            sqlPri.AppendLine("           ,ConfirmDate             ");
            //sqlPri.AppendLine("           ,Closer                  ");
            //sqlPri.AppendLine("           ,CloseDate               ");
            sqlPri.AppendLine("           ,BillStatus              ");
            sqlPri.AppendLine("           ,Creator                 ");
            sqlPri.AppendLine("           ,CreateDate              ");
            sqlPri.AppendLine("           ,ModifiedDate            ");
            sqlPri.AppendLine("           ,ModifiedUserID         ");
            sqlPri.AppendLine("           ,CanViewUserName              ");
            sqlPri.AppendLine("           ,CanViewUser          ");
            sqlPri.AppendLine("           ,Attachment         ");
            sqlPri.Append(")");

            sqlPri.AppendLine("     VALUES(@CompanyCD             ");
            sqlPri.AppendLine("            ,@OrderNo               ");
            sqlPri.AppendLine("            ,@Title                 ");
            sqlPri.AppendLine("            ,@TypeID                ");
            sqlPri.AppendLine("            ,@FromType              ");
            sqlPri.AppendLine("            ,@CurrencyType          ");
            sqlPri.AppendLine("            ,@Rate                  ");
            sqlPri.AppendLine("           ,@ProjectID                      ");
            //sqlPri.AppendLine("            ,@PurchaseDate          ");
            sqlPri.AppendLine("            ,@OrderDate             ");
            sqlPri.AppendLine("            ,@TheyDelegate          ");
            sqlPri.AppendLine("            ,@OurDelegate           ");
            sqlPri.AppendLine("            ,@ProviderID            ");
            sqlPri.AppendLine("            ,@DeptID                ");
            sqlPri.AppendLine("            ,@PayType               ");
            sqlPri.AppendLine("            ,@MoneyType             ");
            sqlPri.AppendLine("            ,@Purchaser             ");
            sqlPri.AppendLine("            ,@TakeType              ");
            sqlPri.AppendLine("            ,@CarryType             ");
            sqlPri.AppendLine("            ,@ProviderBillID        ");
            sqlPri.AppendLine("            ,@remark                ");
            sqlPri.AppendLine("            ,@TotalPrice            ");
            sqlPri.AppendLine("            ,@TotalTax              ");
            sqlPri.AppendLine("            ,@TotalFee              ");
            sqlPri.AppendLine("            ,@Discount              ");
            sqlPri.AppendLine("            ,@DiscountTotal         ");
            sqlPri.AppendLine("            ,@OtherTotal            ");
            sqlPri.AppendLine("            ,@RealTotal             ");
            sqlPri.AppendLine("            ,@isAddTax              ");
            sqlPri.AppendLine("            ,@CountTotal            ");
            sqlPri.AppendLine("            ,@isOpenbill            ");
            sqlPri.AppendLine("            ,@Confirmor             ");
            sqlPri.AppendLine("            ,@ConfirmDate           ");
            //sqlPri.AppendLine("            ,@Closer                ");
            //sqlPri.AppendLine("            ,@CloseDate             ");
            sqlPri.AppendLine("            ,@BillStatus            ");
            sqlPri.AppendLine("            ,@Creator               ");
            sqlPri.AppendLine("            ,@CreateDate            ");
            sqlPri.AppendLine("            ,@ModifiedDate          ");
            sqlPri.AppendLine("            ,@ModifiedUserID       ");
            sqlPri.AppendLine("           ,@CanViewUserName              ");
            sqlPri.AppendLine("           ,@CanViewUser          ");
            sqlPri.AppendLine("            ,@Attachment");
            sqlPri.Append(")");
            sqlPri.AppendLine("set @IndexID = @@IDENTITY");
            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", PurchaseOrderM.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", PurchaseOrderM.OrderNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", PurchaseOrderM.Title));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", PurchaseOrderM.TypeID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", PurchaseOrderM.FromType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", PurchaseOrderM.ProjectID));


            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrencyType", PurchaseOrderM.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate", PurchaseOrderM.Rate));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@PurchaseDate", PurchaseOrderM.PurchaseDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderDate", PurchaseOrderM.OrderDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TheyDelegate", PurchaseOrderM.TheyDelegate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OurDelegate", PurchaseOrderM.OurDelegate));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", PurchaseOrderM.ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", PurchaseOrderM.DeptID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PayType", PurchaseOrderM.PayType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MoneyType", PurchaseOrderM.MoneyType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Purchaser", (PurchaseOrderM.Purchaser == null) ? DBNull.Value.ToString() : PurchaseOrderM.Purchaser.ToString()));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TakeType", (PurchaseOrderM.TakeType == null) ? DBNull.Value.ToString() : PurchaseOrderM.TakeType.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CarryType", (PurchaseOrderM.CarryType == null) ? DBNull.Value.ToString() : PurchaseOrderM.CarryType.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderBillID", PurchaseOrderM.ProviderBillID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@remark", PurchaseOrderM.Remark));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice", PurchaseOrderM.TotalPrice.ToString()));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalTax", PurchaseOrderM.TotalTax.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalFee", PurchaseOrderM.TotalFee.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Discount", PurchaseOrderM.Discount.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DiscountTotal", PurchaseOrderM.DiscountTotal.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OtherTotal", PurchaseOrderM.OtherTotal.ToString()));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RealTotal", PurchaseOrderM.RealTotal.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@isAddTax", PurchaseOrderM.isAddTax));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal", PurchaseOrderM.CountTotal.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@isOpenbill", PurchaseOrderM.isOpenbill));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", PurchaseOrderM.Confirmor.ToString()));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate", PurchaseOrderM.ConfirmDate.ToString()));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@Closer", PurchaseOrderM.Closer.ToString()));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", PurchaseOrderM.CloseDate.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchaseOrderM.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", PurchaseOrderM.Creator.ToString()));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", PurchaseOrderM.CreateDate.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", PurchaseOrderM.ModifiedDate.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", PurchaseOrderM.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUserName", PurchaseOrderM.CanUserName));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUser ", PurchaseOrderM.CanUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Attachment", PurchaseOrderM.Attachment));
            SqlParameter IndexID = new SqlParameter("@IndexID", SqlDbType.Int);
            IndexID.Direction = ParameterDirection.Output;
            comm.Parameters.Add(IndexID);

            #endregion

            comm.CommandText = sqlPri.ToString();

            return comm;

        }
        #endregion

        #region update

        #region 更新主表
        public static SqlCommand UpdatePurchaseOrder(PurchaseOrderModel PurchaseOrderM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseOrder    ");
            sql.AppendLine("   SET CompanyCD      = @CompanyCD      ");
            sql.AppendLine("      ,OrderNo        = @OrderNo        ");
            sql.AppendLine("      ,Title          = @Title          ");
            sql.AppendLine("      ,TypeID         = @TypeID         ");
            sql.AppendLine("      ,FromType       = @FromType       ");
            sql.AppendLine("      ,CurrencyType   = @CurrencyType   ");
            sql.AppendLine("      ,Rate           = @Rate           ");
            sql.AppendLine("      ,ProjectID           = @ProjectID           ");

            //sql.AppendLine("      ,PurchaseDate   = @PurchaseDate   ");
            sql.AppendLine("      ,OrderDate      = @OrderDate      ");
            sql.AppendLine("      ,TheyDelegate   = @TheyDelegate   ");
            sql.AppendLine("      ,OurDelegate    = @OurDelegate    ");
            sql.AppendLine("      ,ProviderID     = @ProviderID     ");
            sql.AppendLine("      ,DeptID         = @DeptID         ");
            sql.AppendLine("      ,PayType        = @PayType        ");
            sql.AppendLine("      ,MoneyType      = @MoneyType      ");
            sql.AppendLine("      ,Purchaser      = @Purchaser      ");
            sql.AppendLine("      ,TakeType       = @TakeType       ");
            sql.AppendLine("      ,CarryType      = @CarryType      ");
            sql.AppendLine("      ,ProviderBillID = @ProviderBillID ");
            sql.AppendLine("      ,remark         = @remark         ");
            sql.AppendLine("      ,TotalPrice     = @TotalPrice     ");
            sql.AppendLine("      ,TotalTax       = @TotalTax       ");
            sql.AppendLine("      ,TotalFee       = @TotalFee       ");
            sql.AppendLine("      ,Discount       = @Discount       ");
            sql.AppendLine("      ,DiscountTotal  = @DiscountTotal  ");
            sql.AppendLine("      ,OtherTotal     = @OtherTotal     ");
            sql.AppendLine("      ,RealTotal      = @RealTotal      ");
            sql.AppendLine("      ,isAddTax       = @isAddTax       ");
            sql.AppendLine("      ,CountTotal     = @CountTotal     ");
            //sql.AppendLine("      ,isOpenbill     = @isOpenbill     ");
            sql.AppendLine("      ,Confirmor      = @Confirmor      ");
            sql.AppendLine("      ,ConfirmDate    = @ConfirmDate    ");
            //sql.AppendLine("      ,Closer         = @Closer         ");
            //sql.AppendLine("      ,CloseDate      = @CloseDate      ");
            sql.AppendLine("      ,BillStatus     = @BillStatus     ");
            //sql.AppendLine("      ,Creator        = @Creator        ");
            //sql.AppendLine("      ,CreateDate     = @CreateDate     ");
            sql.AppendLine("      ,ModifiedDate   = getDate()     ");
            sql.AppendLine("      ,ModifiedUserID = @ModifiedUserID ");
            sql.AppendLine("      ,Attachment = @Attachment ");
            sql.AppendLine("           ,CanViewUserName=@UserName          ");
            sql.AppendLine("           ,CanViewUser=@CanViewUser          ");
            sql.AppendLine(" WHERE ID=@ID                           ");

            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", PurchaseOrderM.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", PurchaseOrderM.OrderNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", PurchaseOrderM.Title));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", PurchaseOrderM.TypeID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", PurchaseOrderM.FromType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", PurchaseOrderM.ProjectID));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrencyType", PurchaseOrderM.CurrencyType.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate", PurchaseOrderM.Rate.ToString()));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@PurchaseDate", PurchaseOrderM.PurchaseDate.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderDate", PurchaseOrderM.OrderDate.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TheyDelegate", PurchaseOrderM.TheyDelegate.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OurDelegate", PurchaseOrderM.OurDelegate.ToString()));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", PurchaseOrderM.ProviderID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", PurchaseOrderM.DeptID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PayType", PurchaseOrderM.PayType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MoneyType", PurchaseOrderM.MoneyType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Purchaser", PurchaseOrderM.Purchaser));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TakeType", PurchaseOrderM.TakeType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CarryType", PurchaseOrderM.CarryType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderBillID", PurchaseOrderM.ProviderBillID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@remark", PurchaseOrderM.Remark));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice", PurchaseOrderM.TotalPrice.ToString()));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalTax", PurchaseOrderM.TotalTax.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalFee", PurchaseOrderM.TotalFee.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Discount", PurchaseOrderM.Discount.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DiscountTotal", PurchaseOrderM.DiscountTotal.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OtherTotal", PurchaseOrderM.OtherTotal.ToString()));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RealTotal", PurchaseOrderM.RealTotal.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@isAddTax", PurchaseOrderM.isAddTax));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal", PurchaseOrderM.CountTotal.ToString()));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@isOpenbill", PurchaseOrderM.isOpenbill));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", PurchaseOrderM.Confirmor.ToString()));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate", PurchaseOrderM.ConfirmDate.ToString()));
            //comm[index].Parameters.Add(SqlHelper.GetParameterFromString("@Closer", PurchaseOrderM.Closer.ToString()));
            //comm[index].Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", PurchaseOrderM.CloseDate.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchaseOrderM.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", PurchaseOrderM.Creator.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UserName", PurchaseOrderM.CanUserName));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUser ", PurchaseOrderM.CanUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", PurchaseOrderM.CreateDate.ToString()));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", PurchaseOrderM.ModifiedDate.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", PurchaseOrderM.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Attachment", PurchaseOrderM.Attachment));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", PurchaseOrderM.ID.ToString()));
            #endregion

            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #endregion

        #region select
        public static DataTable GetPurchaseOrder(PurchaseOrderModel PurchaseOrderM, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT     A.ID   ");
            sql.AppendLine(" , A.CompanyCD     ");
            sql.AppendLine(" , A.OrderNo       ");
            sql.AppendLine(" , A.Title AS OrderTitle        ");
            sql.AppendLine(" , A.TypeID        ");
            sql.AppendLine(" , E.TypeName ");
            sql.AppendLine(" , A.Purchaser as  PurchaserID   ");
            sql.AppendLine(" , B.EmployeeName  AS PurchaserName ");
            sql.AppendLine(" , A.ProviderID    ");
            sql.AppendLine(" , C.CustName   AS ProviderName     ");
            sql.AppendLine(" , A.TotalPrice    ");
            sql.AppendLine(" , A.TotalTax      ");
            sql.AppendLine(" , A.TotalFee      ");
            sql.AppendLine(" , A.isOpenBill    ");
            sql.AppendLine(" , A.BillStatus    ");
            sql.AppendLine(" ,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'                         ");
            sql.AppendLine(" when '4' then '手工结单' when '5' then '自动结单' end AS BillStatusName                                   ");
            sql.AppendLine(" , D.FlowStatus                                                                                            ");
            sql.AppendLine(" ,case D.FlowStatus when '1' then '待审批' when '2' then '审批中' when '3'                                 ");
            sql.AppendLine(" then '审批通过' when '4' then '审批不通过' when '5' then '撤销审批' else '' end as FlowStatusName ,isnull( A.ModifiedDate,'') AS ModifiedDate  ");
            sql.AppendLine(" FROM officedba.PurchaseOrder AS A                                                                         ");
            sql.AppendLine(" left JOIN officedba.EmployeeInfo AS B ON A.Purchaser = B.ID                                              ");
            sql.AppendLine(" left JOIN officedba.ProviderInfo AS C ON A.ProviderID = C.ID                                             ");
            sql.AppendLine(" LEFT JOIN officedba.FlowInstance AS D ON A.ID = D.BillID AND D.BillTypeFlag = 6 AND D.BillTypeCode = 4    ");
            sql.AppendLine(" AND D.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = 6 AND F.BillTypeCode = 4 )");
            sql.AppendLine(" LEFT JOIN officedba.CodePublicType AS E ON A.TypeID=E.ID ");
            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine(" AND A.CompanyCD=@CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", PurchaseOrderM.CompanyCD));
            if (PurchaseOrderM.OrderNo != "")
            {
                sql.AppendLine(" AND A.OrderNo like @OrderNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", "%" + PurchaseOrderM.OrderNo + "%"));
            }
            if (PurchaseOrderM.Title != "")
            {
                sql.AppendLine(" AND A.Title like @Title");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + PurchaseOrderM.Title + "%"));
            }
            if (PurchaseOrderM.TypeID != "")
            {
                sql.AppendLine(" AND A.TypeID = @TypeID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", PurchaseOrderM.TypeID));
            }
            if (PurchaseOrderM.DeptID != "")
            {
                sql.AppendLine(" AND A.DeptID = @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", PurchaseOrderM.DeptID));
            }
            if (PurchaseOrderM.Purchaser != "")
            {
                sql.AppendLine(" AND A.Purchaser = @Purchaser");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Purchaser", PurchaseOrderM.Purchaser));
            }
            if (PurchaseOrderM.FromType != "10")
            {
                sql.AppendLine(" AND A.FromType = @FromType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", PurchaseOrderM.FromType));
            }
            if (PurchaseOrderM.ProviderID != "")
            {
                sql.AppendLine(" AND A.ProviderID = @ProviderID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", PurchaseOrderM.ProviderID));
            }
            if (PurchaseOrderM.BillStatus != "0")
            {
                sql.AppendLine(" AND A.BillStatus = @BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchaseOrderM.BillStatus));
            }
            if (PurchaseOrderM.FlowStatus != "0")
            {
                if (PurchaseOrderM.FlowStatus == "")
                {
                    sql.AppendLine(" AND D.FlowStatus is null");
                }
                else
                {
                    sql.AppendLine(" AND D.FlowStatus = @FlowStatus");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", PurchaseOrderM.FlowStatus));
                }


            }
            string CanViewUser = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            sql.AppendLine(" and (a.CanViewUser  like '%" + CanViewUser + "%'  or '" + CanViewUser + "' = a.Creator or a.CanViewUser  = ',,' or a.CanViewUser  is null )");

            if (!string.IsNullOrEmpty(PurchaseOrderM.EFIndex) && !string.IsNullOrEmpty(PurchaseOrderM.EFDesc))
            {
                sql.AppendLine(" and a.ExtField" + PurchaseOrderM.EFIndex + " LIKE @EFDesc");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + PurchaseOrderM.EFDesc + "%"));
            }
            if (!string.IsNullOrEmpty(PurchaseOrderM.ProjectID))
            {
                sql.AppendLine(" AND a.ProjectID = @ProjectID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", PurchaseOrderM.ProjectID));
            }
            #endregion

            comm.CommandText = sql.ToString();
            //return SqlHelper.ExecuteSearch(comm);
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }





        public static DataTable GetPurchaseOrderAnaylise(PurchaseOrderModel PurchaseOrderM, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();

            StringBuilder sql = new StringBuilder();


            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            sql.AppendLine(" select isnull(d.ProductID,e.ProductID) as productID, Convert(char(20),Convert(numeric(22," + userInfo.SelPoint + "),isnull(d.OrderCount,0)))+'&nbsp;' OrderCount,  Convert(char(20),Convert(numeric(22," + userInfo.SelPoint + "),isnull(d.OrderPrice,0)))+'&nbsp;' OrderPrice, Convert(char(20),Convert(numeric(22," + userInfo.SelPoint + "),isnull(e.RejectPrice,0)))+'&nbsp;' RejectPrice, Convert(char(20),Convert(numeric(22," + userInfo.SelPoint + "),isnull(e.RejectCount,0)))+'&nbsp;' RejectCount ");
            sql.AppendLine(",isnull(d.ProviderID,e.ProviderID) as ProviderID,ISNULL(c.ProductName,'') AS ProductName,ISNULL(c.Specification,'')as Specification,isnull(f.CodeName,'')  AS UnitName,  ");
            sql.AppendLine("isnull(g.CustName,'') as ProviderName,c.ProdNo as ProductNo ");
            sql.AppendLine(" from ");
            sql.AppendLine(" ( ");
            sql.AppendLine("  select distinct B.ProductID,a.ProviderID,sum( isnull(b.ProductCount,0))  as OrderCount, sum(isnull(b.TotalPrice,0))  as OrderPrice ");
            sql.AppendLine("  from Officedba.PurchaseOrder a    ");
            sql.AppendLine("  left outer join Officedba.PurchaseOrderDetail b on a.CompanyCD=b.CompanyCD and a.OrderNo=b.OrderNo   ");
            sql.AppendLine("  where a.BillStatus=2 and a.CompanyCD=@CompanyCD   AND b.ProductNo is not null           ");
            if (PurchaseOrderM.Title != "")
            {
                sql.AppendLine(" AND A.ConfirmDate > @Title  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", PurchaseOrderM.Title + " 00:00:00"));
            }
            if (PurchaseOrderM.TypeID != "")
            {
                sql.AppendLine(" AND A.ConfirmDate < @TypeID  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", PurchaseOrderM.TypeID + " 23:59:59"));
            }

            sql.AppendLine("  group by  b.ProductID,a.ProviderID) as d ");

            sql.AppendLine(" full   join  ");

            sql.AppendLine(" (  select distinct b.ProductID,a.ProviderID,sum(isnull(b.ProductCount,0)) as RejectCount,sum(isnull(b.TotalPrice,0)) as RejectPrice   ");
            sql.AppendLine("   from Officedba.PurchaseReject a      ");
            sql.AppendLine("  left outer join Officedba.PurchaseRejectDetail b on a.CompanyCD=b.CompanyCD and a.RejectNo=b.RejectNo             ");
            sql.AppendLine("  where a.BillStatus=2  and a.CompanyCD=@CompanyCD AND b.ProductNo is not null     ");

            if (PurchaseOrderM.Title != "")
            {
                sql.AppendLine(" AND a.ConfirmDate > @Title1  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title1", PurchaseOrderM.Title + " 00:00:00"));

            }
            if (PurchaseOrderM.TypeID != "")
            {
                sql.AppendLine(" AND a.ConfirmDate < @TypeID1  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID1", PurchaseOrderM.TypeID + " 23:59:59"));

            }

            sql.AppendLine("  group by  b.ProductID,a.ProviderID) as e on d.ProductID=e.ProductID and  d.ProviderID=e.ProviderID  ");

            sql.AppendLine("  left outer join Officedba.ProductInfo c on   isnull(d.ProductID,e.ProductID)=C.ID               ");
            sql.AppendLine("  LEFT outer JOIN officedba.CodeUnitType AS f ON   c.UnitID=f.ID              ");
            sql.AppendLine("   LEFT outer JOIN officedba.ProviderInfo AS g ON  isnull(d.ProviderID,e.ProviderID)=g.ID   ");

            sql.AppendLine("  where 1=1   ");






            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", PurchaseOrderM.CompanyCD));
            if (PurchaseOrderM.OrderNo != "")
            {
                sql.AppendLine(" AND c.ProdNo like @ProductNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", "%" + PurchaseOrderM.OrderNo + "%"));
            }

            if (PurchaseOrderM.ProviderID != "")
            {
                sql.AppendLine(" AND isnull(d.ProviderID,e.ProviderID)= @ProviderID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", PurchaseOrderM.ProviderID));
            }
            if (PurchaseOrderM.DeptID != "")
            {
                sql.AppendLine(" AND c.ProductName  like  @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", "%" + PurchaseOrderM.DeptID + "%"));
            }



            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }

        public static DataTable GetPurchaseOrder(PurchaseOrderModel PurchaseOrderM, string OrderBy)
        {
            SqlCommand comm = new SqlCommand();
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT     A.ID   ");
            sql.AppendLine(" , A.CompanyCD     ");
            sql.AppendLine(" , A.OrderNo       ");
            sql.AppendLine(" , A.Title AS OrderTitle        ");
            sql.AppendLine(" , A.TypeID        ");
            sql.AppendLine(" , E.TypeName ");
            sql.AppendLine(" , A.Purchaser as  PurchaserID   ");
            sql.AppendLine(" , B.EmployeeName  AS PurchaserName ");
            sql.AppendLine(" , A.ProviderID    ");
            sql.AppendLine(" , C.CustName   AS ProviderName     ");
            sql.AppendLine(" , A.TotalPrice    ");
            sql.AppendLine(" , A.TotalTax      ");
            sql.AppendLine(" , A.TotalFee      ");
            sql.AppendLine(" , A.isOpenBill    ");
            sql.AppendLine(" , CASE A.isOpenBill WHEN '0' THEN '未建单' WHEN '1' THEN '已建单' END AS isOpenBillName");
            sql.AppendLine(" , A.BillStatus    ");
            sql.AppendLine(" ,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'                         ");
            sql.AppendLine(" when '4' then '手工结单' when '5' then '自动结单' end AS BillStatusName                                   ");
            sql.AppendLine(" , D.FlowStatus                                                                                            ");
            sql.AppendLine(" ,case D.FlowStatus when '1' then '待审批' when '2' then '审批中' when '3'                                 ");
            sql.AppendLine(" then '审批通过' when '4' then '审批不通过' when '5' then '撤销审批' else '' end as FlowStatusName   ");
            sql.AppendLine(" FROM officedba.PurchaseOrder AS A                                                                         ");
            sql.AppendLine(" INNER JOIN officedba.EmployeeInfo AS B ON A.Purchaser = B.ID                                              ");
            sql.AppendLine(" INNER JOIN officedba.ProviderInfo AS C ON A.ProviderID = C.ID                                             ");
            sql.AppendLine(" LEFT JOIN officedba.FlowInstance AS D ON A.ID = D.BillID AND D.BillTypeFlag = 6 AND D.BillTypeCode = 4    ");
            sql.AppendLine(" AND D.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = 6 AND F.BillTypeCode = 4 )");
            sql.AppendLine(" LEFT JOIN officedba.CodePublicType AS E ON A.TypeID=E.ID ");
            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine(" AND A.CompanyCD=@CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", PurchaseOrderM.CompanyCD));
            if (PurchaseOrderM.OrderNo != "")
            {
                sql.AppendLine(" AND A.OrderNo like @OrderNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", "%" + PurchaseOrderM.OrderNo + "%"));
            }
            if (PurchaseOrderM.Title != "")
            {
                sql.AppendLine(" AND A.Title like @Title");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + PurchaseOrderM.Title + "%"));
            }
            if (PurchaseOrderM.TypeID != "")
            {
                sql.AppendLine(" AND A.TypeID = @TypeID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", PurchaseOrderM.TypeID));
            }
            if (PurchaseOrderM.DeptID != "")
            {
                sql.AppendLine(" AND A.DeptID = @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", PurchaseOrderM.DeptID));
            }
            if (PurchaseOrderM.Purchaser != "")
            {
                sql.AppendLine(" AND A.Purchaser = @Purchaser");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Purchaser", PurchaseOrderM.Purchaser));
            }
            if (PurchaseOrderM.FromType != "10")
            {
                sql.AppendLine(" AND A.FromType = @FromType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", PurchaseOrderM.FromType));
            }
            if (PurchaseOrderM.ProviderID != "")
            {
                sql.AppendLine(" AND A.ProviderID = @ProviderID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", PurchaseOrderM.ProviderID));
            }
            if (!string.IsNullOrEmpty(PurchaseOrderM.ProjectID))
            {
                sql.AppendLine(" AND a.ProjectID = @ProjectID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", PurchaseOrderM.ProjectID));
            }
            if (PurchaseOrderM.BillStatus != "0")
            {
                sql.AppendLine(" AND A.BillStatus = @BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchaseOrderM.BillStatus));
            }
            if (PurchaseOrderM.FlowStatus != "0")
            {
                if (PurchaseOrderM.FlowStatus == "")
                {
                    sql.AppendLine(" AND D.FlowStatus is null");
                }
                else
                {
                    sql.AppendLine(" AND D.FlowStatus = @FlowStatus");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", PurchaseOrderM.FlowStatus));
                }
            }
            if (!string.IsNullOrEmpty(PurchaseOrderM.EFIndex) && !string.IsNullOrEmpty(PurchaseOrderM.EFDesc))
            {
                sql.AppendLine(" and a.ExtField" + PurchaseOrderM.EFIndex + " LIKE @EFDesc");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + PurchaseOrderM.EFDesc + "%"));
            }


            sql.AppendLine(" ORDER BY " + OrderBy + "");
            #endregion

            comm.CommandText = sql.ToString();
            //return SqlHelper.ExecuteSearch(comm);
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 判断采购订单有没有被引用
        public static bool IsCite(string ID)
        {
            bool IsCite = false;
            //采购到货引用
            if (!IsCite)
            {
                string sql = "SELECT ID FROM officedba.PurchaseArriveDetail WHERE FromType=@FromType AND FromBillID=@ID";
                SqlParameter[] parameters = {
                    new SqlParameter("@FromType", SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.VarChar),};
                parameters[0].Value = "1";
                parameters[1].Value = ID;
                IsCite = SqlHelper.Exists(sql, parameters);
            }
            return IsCite;
        }
        #endregion

        #region 确认
        #region 判断可不可以确认
        public static bool CanConfirm(string CompanyCD, string OrderNo, int ID, string FromType, out string Reason)
        {
            SqlParameter[] parms = new SqlParameter[6];
            int i = 0;
            parms[i++] = new SqlParameter("@CompanyCD", CompanyCD);
            parms[i++] = new SqlParameter("@OrderNo", OrderNo);
            parms[i++] = new SqlParameter("@ID", ID);
            parms[i++] = new SqlParameter("@FromType", FromType);
            parms[i++] = MakeParam("@Reason", SqlDbType.VarChar, 30, ParameterDirection.Output, null);
            //parms[i++] = MakeParam("@ReturnValue", SqlDbType.Bit, 1, ParameterDirection.Output, null);
            SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Bit);
            returnValue.Direction = ParameterDirection.ReturnValue;
            parms[i++] = returnValue;
            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[ProcCanConfirmPurOrder]", parms);
            bool Flag = Convert.ToBoolean(returnValue.Value);
            if (Flag == false)
            {//单据已经确认或者已经撤销审批
                Reason = string.Empty;
                return Flag;
            }
            else
            {//否则，看数量可合法                
                foreach (DataRow dr in dt.Rows)
                {//循环每行明细，看数量是不是合法
                    //如果订购数量小于源单可用数量  
                    double pCount = Convert.ToDouble(dr["YProductCount"].ToString()) - Convert.ToDouble(dr["YOrderCount"].ToString());
                    if (pCount - Convert.ToDouble(dr["ProductCount"].ToString()) < -0.01)
                    {
                        //源单可用数量小于订购数量
                        Reason = "第" + dr["SortNo"].ToString() + "行的采购数量不能大于当前可用的采购数量:" + pCount.ToString() + "";
                        return false;
                    }
                    //if(Convert.ToDouble(dr["HasCount"].ToString() - Convert.ToDouble(dr["ProductCount"].ToString() < 0.01))
                    //{//库存可用数量小于当前订购量
                    //    Reason = "第" + dr["SortNo"].ToString() + "行的采购数量不能大于当前可用的采购数量" + pCount.ToString() + "";
                    //    return false;
                    //}
                }
                //如果订购数量小于可用库存
                //如果不允许负库存
            }
            //SqlHelper.ExecuteTransStoredProcedure("[officedba].[ProcCanConfirmPurOrder]", parms);
            //Reason = parms[4].Value.ToString();
            Reason = string.Empty;
            return Flag;

        }
        #endregion


        #region 写确认人，确认时间，单据状态
        public static SqlCommand ConfirmPurchaseOrder(string ID)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseOrder    ");
            sql.AppendLine("   SET Confirmor = @Confirmor     ");
            sql.AppendLine("      ,ConfirmDate = @ConfirmDate ");
            sql.AppendLine("      ,BillStatus = @BillStatus   ");
            sql.AppendLine(" WHERE ID=@ID                     ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate", DateTime.Now.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

            comm.CommandText = sql.ToString();


            return comm;
        }
        #endregion

        #region 回写
        public static SqlCommand WriteBack(string Flag, ProductModel ProductM)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            switch (Flag)
            {
                case "Contact"://回写合同表
                    sql.AppendLine("UPDATE officedba.PurchaseContractDetail    ");
                    sql.AppendLine("   SET OrderCount = (isnull(OrderCount,0)+@OrderCount) ");
                    sql.AppendLine(" WHERE ContractNo=@FromBillNo ");
                    sql.AppendLine(" AND CompanyCD=@CompanyCD");
                    sql.AppendLine(" AND SortNo=@SortNo");

                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCount", ProductM.ProductCount));
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", ProductM.FromBillNo));
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", ProductM.FromLineNo));
                    break;
                case "Plan"://回写计划表
                    sql.AppendLine("UPDATE officedba.PurchasePlanDetail    ");
                    sql.AppendLine("   SET OrderCount = (isnull(OrderCount,0)+@OrderCount) ");
                    sql.AppendLine(" WHERE PlanNo=@FromBillNo");
                    sql.AppendLine(" AND CompanyCD=@CompanyCD");
                    sql.AppendLine(" AND SortNo=@SortNo");

                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCount", ProductM.ProductCount));
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", ProductM.FromBillNo));
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", ProductM.FromLineNo));
                    break;
            }
            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region 回写合同表
        public static SqlCommand WritePurContract(ProductModel ProductM)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseContractDetail    ");
            sql.AppendLine("   SET OrderCount = (isnull(OrderCount,0)+@OrderCount) ");
            sql.AppendLine(" WHERE ContractNo=@FromBillNo ");
            sql.AppendLine(" AND CompanyCD=@CompanyCD");
            sql.AppendLine(" AND SortNo=@SortNo");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCount", ProductM.UsedUnitCount));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCount", ProductM.ProductCount));
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", ProductM.FromBillNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", ProductM.FromLineNo));
            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region 回写计划表
        public static SqlCommand WritePurPlanConfirm(ProductModel ProductM)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchasePlanDetail    ");
            sql.AppendLine("   SET OrderCount = (isnull(OrderCount,0)+@OrderCount) ");
            sql.AppendLine(" WHERE PlanNo=@FromBillNo");
            sql.AppendLine(" AND CompanyCD=@CompanyCD");
            sql.AppendLine(" AND SortNo=@SortNo");

            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCount", ProductM.UsedUnitCount));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCount", ProductM.ProductCount));
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", ProductM.FromBillNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", ProductM.FromLineNo));
            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region 回写库存分仓存量表
        public static SqlCommand WriteStorge(ProductModel ProductM)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            if (IsExistInStorge(ProductM.ProductID))
            {
                sql.AppendLine("UPDATE officedba.StorageProduct   ");
                sql.AppendLine(" SET RoadCount = isnull(RoadCount,0)+@RoadCount");
                sql.AppendLine(" WHERE ProductID = @ProductID ");
                sql.AppendLine(" AND StorageID = (SELECT StorageID FROM officedba.ProductInfo WHERE ID=@ProductID)");
                sql.AppendLine(" AND CompanyCD = @CompanyCD");

                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductM.ProductID));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RoadCount", ProductM.ProductCount));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
                comm.CommandText = sql.ToString();
            }
            else
            {
                sql.AppendLine("INSERT INTO officedba.StorageProduct ( CompanyCD,StorageID,ProductID,RoadCount)");
                sql.AppendLine(" SELECT @CompanyCD,(SELECT StorageID FROM officedba.ProductInfo WHERE ID=@ProductID),@ProductID,@RoadCount ");


                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductM.ProductID));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RoadCount", ProductM.ProductCount));
                comm.CommandText = sql.ToString();
            }
            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion
        #endregion

        #region 取消确认
        #region 判断可不可以取消确认
        public static bool CanConcel(string ID, out string Reason)
        {
            Reason = string.Empty;
            //被引用的单据不可以取消确认
            if (IsCite(ID))
            {
                Reason = "被引用的单据不能取消确认！";
                return false;
            }
            DataTable dt = isHandle(ID);
            try
            {
                if (dt.Rows.Count == 0)
                {
                    Reason = "该单据已被删除，不能取消确认！";
                    return false;
                }
                if (dt.Rows[0]["isOpenbill"].ToString() == "1")
                {
                    Reason = "已建单的单据不能取消确认！";
                    return false;
                }
                if (dt.Rows[0]["BillStatus"].ToString() == "2")
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 取消确认时更改主表的单据状态，确认人，确认时间（置空）
        public static SqlCommand ConcelConfirm(string ID)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseOrder    ");
            sql.AppendLine("   SET Confirmor = null     ");
            sql.AppendLine("      ,ConfirmDate = null ");
            sql.AppendLine("      ,BillStatus = @BillStatus   ");
            sql.AppendLine(" WHERE ID=@ID                     ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "1"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

            comm.CommandText = sql.ToString();


            return comm;
        }
        #endregion

        #region 取消确认时回写库存
        public static SqlCommand WriteStorgeDecr(ProductModel ProductM)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            if (IsExistInStorge(ProductM.ProductID))
            {
                sql.AppendLine("UPDATE officedba.StorageProduct   ");
                sql.AppendLine(" SET RoadCount = isnull(RoadCount,0)-@RoadCount");
                sql.AppendLine(" WHERE ProductID = @ProductID ");
                sql.AppendLine(" AND StorageID = (SELECT StorageID FROM officedba.ProductInfo WHERE ID=@ProductID)");
                sql.AppendLine(" AND CompanyCD = @CompanyCD");

                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductM.ProductID));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RoadCount", ProductM.ProductCount));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
                comm.CommandText = sql.ToString();
            }
            else
            {
                sql.AppendLine("INSERT INTO officedba.StorageProduct                                                                   ");
                sql.AppendLine("SELECT @CompanyCD,(SELECT StorageID FROM officedba.ProductInfo WHERE ID=@ProductID),@ProductID,null,");
                sql.AppendLine("null,null,@RoadCount,null,null                                                                         ");


                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductM.ProductID));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RoadCount", (0 - Convert.ToInt32(ProductM.ProductCount)).ToString()));
                comm.CommandText = sql.ToString();
            }
            return comm;
        }

        //判断这条物品在分仓存量表
        public static bool IsExistInStorge(string ProductID)
        {
            String sql = "SELECT A.ID, A.StorageID FROM officedba.StorageProduct AS A INNER JOIN " +
                      "officedba.ProductInfo AS B ON A.ProductID = B.ID AND A.StorageID = B.StorageID AND A.CompanyCD = B.CompanyCD " +
                      "WHERE A.CompanyCD=@CompanyCD AND A.ProductID=@ProductID";
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar),
                    new SqlParameter("@ProductID", SqlDbType.VarChar),};
            parameters[0].Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            parameters[1].Value = ProductID;

            return SqlHelper.Exists(sql, parameters);
        }
        #endregion

        #region 取消确认时回写计划表
        public static SqlCommand WritePurPlanDecr(ProductModel ProductM)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchasePlanDetail    ");
            sql.AppendLine("   SET OrderCount = (isnull(OrderCount,0)-@OrderCount) ");
            sql.AppendLine(" WHERE PlanNo=@FromBillNo");
            sql.AppendLine(" AND CompanyCD=@CompanyCD");
            sql.AppendLine(" AND SortNo=@SortNo");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCount", ProductM.UsedUnitCount));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCount", ProductM.ProductCount));
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", ProductM.FromBillNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", ProductM.FromLineNo));
            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region 取消确认时回写合同表
        public static SqlCommand WritePurContDecr(ProductModel ProductM)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseContractDetail    ");
            sql.AppendLine("   SET OrderCount = (isnull(OrderCount,0)-@OrderCount) ");
            sql.AppendLine(" WHERE ContractNo=@FromBillNo ");
            sql.AppendLine(" AND CompanyCD=@CompanyCD");
            sql.AppendLine(" AND SortNo=@SortNo");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCount", ProductM.UsedUnitCount));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCount", ProductM.ProductCount));
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", ProductM.FromBillNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", ProductM.FromLineNo));
            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion
        #endregion

        #region 结单
        public static bool CompletePurchaseOrder(string ID)
        {
            SqlCommand[] comm = new SqlCommand[1];
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseOrder  ");
            sql.AppendLine("   SET BillStatus = @BillStatus ");
            sql.AppendLine("      ,Closer = @Closer       ");
            sql.AppendLine("      ,CloseDate = @CloseDate ");
            sql.AppendLine(" WHERE ID=@ID                   ");

            comm[0] = new SqlCommand();
            comm[0].Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "4"));
            comm[0].Parameters.Add(SqlHelper.GetParameterFromString("@Closer", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString()));
            comm[0].Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", DateTime.Now.ToString()));
            comm[0].Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

            comm[0].CommandText = sql.ToString();
            return SqlHelper.ExecuteTransForList(comm);
        }
        #endregion

        #region 取消结单
        public static bool ConcelCompletePurchaseOrder(string ID)
        {
            SqlCommand[] comm = new SqlCommand[1];
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseOrder  ");
            sql.AppendLine("   SET BillStatus = @BillStatus ");
            sql.AppendLine("      ,Closer = @Closer       ");
            sql.AppendLine("      ,CloseDate = @CloseDate ");
            sql.AppendLine(" WHERE ID=@ID                   ");

            comm[0] = new SqlCommand();
            comm[0].Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
            comm[0].Parameters.Add(SqlHelper.GetParameterFromString("@Closer", DBNull.Value.ToString()));
            comm[0].Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", DBNull.Value.ToString()));
            comm[0].Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

            comm[0].CommandText = sql.ToString();
            return SqlHelper.ExecuteTransForList(comm);
        }
        #endregion

        #region 根据采购订单ID填充主表
        /// <summary>
        /// 根据采购订单ID填充主表
        /// </summary>
        /// <param name="ID">订单ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetPurchaseOrderByID(string ID)
        {
            SqlCommand comm = new SqlCommand();

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT     A.ID                                                          ");
            sql.AppendLine(", isnull(A.CompanyCD       ,'') AS CompanyCD                             ");
            sql.AppendLine(", isnull(A.OrderNo         ,'') AS OrderNo                               ");
            sql.AppendLine(", isnull(A.Title           ,'') AS Title                                 ");
            sql.AppendLine(", isnull(A.TypeID          ,0) AS TypeID                                 ");
            sql.AppendLine(", isnull(A.FromType        ,'') AS FromType                              ");
            sql.AppendLine(", isnull(A.CurrencyType    ,0) AS CurrencyType                           ");
            sql.AppendLine(", Convert(numeric(12," + userInfo.SelPoint + "),a.Rate) as Rate                                ");
            //sql.AppendLine(", isnull(CONVERT(varchar(23),A.PurchaseDate,23),'') AS PurchaseDate      ");
            sql.AppendLine(", isnull(CONVERT(varchar(23),A.OrderDate,23),'') AS OrderDate            ");
            sql.AppendLine(", isnull(A.TheyDelegate    ,'') AS TheyDelegate                          ");
            sql.AppendLine(", isnull(A.OurDelegate     ,0) AS OurDelegate                            ");
            sql.AppendLine(", isnull(B.EmployeeName    ,'') AS OurDelegateName                          ");
            sql.AppendLine(", isnull(A.ProviderID      ,0) AS ProviderID                             ");
            sql.AppendLine(", isnull(C.CustName        ,'') AS ProviderName                              ");
            sql.AppendLine(", isnull(A.DeptID          ,0) AS DeptID                                 ");
            sql.AppendLine(", isnull(D.DeptName        ,'') AS DeptName                              ");
            sql.AppendLine(", isnull(A.PayType         ,0) AS PayType                                ");
            sql.AppendLine(", isnull(A.MoneyType       ,0) AS MoneyType                              ");
            sql.AppendLine(", isnull(A.Purchaser       ,0) AS Purchaser                              ");
            sql.AppendLine(", isnull(E.EmployeeName    ,'') AS PurchaserName                          ");
            sql.AppendLine(", isnull(A.TakeType        ,0) AS TakeType                               ");
            sql.AppendLine(", isnull(A.CarryType       ,0) AS CarryType                              ");
            sql.AppendLine(", isnull(A.ProviderBillID  ,'') AS ProviderBillID                        ");
            sql.AppendLine(", isnull(A.remark          ,'') AS remark                                ");
            sql.AppendLine(", Convert(numeric(22," + userInfo.SelPoint + "),a.TotalPrice) as TotalPrice                             ");
            sql.AppendLine(", Convert(numeric(22," + userInfo.SelPoint + "),a.TotalTax) as TotalTax                              ");
            sql.AppendLine(", Convert(numeric(22," + userInfo.SelPoint + "),a.TotalFee) as TotalFee                           ");
            sql.AppendLine(",Convert(numeric(12," + userInfo.SelPoint + "),a.Discount) as Discount                   ");
            sql.AppendLine(", Convert(numeric(22," + userInfo.SelPoint + "),a.DiscountTotal) as DiscountTotal                     ");
            sql.AppendLine(", Convert(numeric(22," + userInfo.SelPoint + "),a.OtherTotal) as OtherTotal                    ");
            sql.AppendLine(", Convert(numeric(22," + userInfo.SelPoint + "),a.RealTotal) as RealTotal                        ");
            sql.AppendLine(", isnull(A.isAddTax        ,'') AS isAddTax                              ");
            sql.AppendLine(", Convert(numeric(22," + userInfo.SelPoint + "),a.CountTotal) as CountTotal                       ");
            sql.AppendLine(", isnull(A.Confirmor       ,0) AS Confirmor                              ");
            sql.AppendLine(", isnull(H.EmployeeName    ,'') AS  ConfirmorName                        ");
            sql.AppendLine(", isnull(CONVERT(varchar(23),A.ConfirmDate,23),'')AS ConfirmDate         ");
            sql.AppendLine(", isnull(A.Closer          ,0) AS Closer                                 ");
            sql.AppendLine(", isnull(I.EmployeeName    ,'') AS CloserName                            ");
            sql.AppendLine(", isnull(CONVERT(varchar(23),A.CloseDate,23)    ,'') AS CloseDate        ");
            sql.AppendLine(", isnull(A.BillStatus      ,0) AS BillStatus                             ");
            sql.AppendLine(", case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'when '4' then '手工结单'");
            sql.AppendLine("       when '5' then '自动结单' end AS BillStatusName    ");
            sql.AppendLine("   ,case A.FromType when '1' then '采购申请单' when '2' then '采购计划单' when '3' then '采购询价单' when '4' then '采购合同' else '无来源' end as FromTypeName     ");
            sql.AppendLine("   ,case A.isAddTax when '1' then '是'  else '否' end as isAddTaxName     ");
            sql.AppendLine(", isnull(A.Creator         ,0) AS Creator                                ");
            sql.AppendLine(", isnull(F.EmployeeName    ,'') AS CreatorName                           ");
            sql.AppendLine(", isnull(CONVERT(varchar(23),A.CreateDate,23)    ,'')AS CreateDate       ");
            sql.AppendLine(", isnull(CONVERT(varchar(23),A.ModifiedDate,23)    ,'') AS ModifiedDate  ");
            sql.AppendLine(", isnull(A.ModifiedUserID  ,'') AS ModifiedUserID                        ");
            sql.AppendLine(", isnull(J.FlowStatus,'') AS FlowStatus");
            sql.AppendLine(", isnull(A.Attachment,'') AS Attachment");
            sql.AppendLine(",isnull(a.ExtField1,'') as ExtField1 ");
            sql.AppendLine(",isnull(a.ExtField2,'') as ExtField2 ");
            sql.AppendLine(",isnull(a.ExtField3,'') as ExtField3 ");
            sql.AppendLine(",isnull(a.ExtField4,'') as ExtField4 ");
            sql.AppendLine(",isnull(a.ExtField5,'') as ExtField5 ");
            sql.AppendLine(",isnull(a.ExtField6,'') as ExtField6 ");
            sql.AppendLine(",isnull(a.ExtField7,'') as ExtField7 ");
            sql.AppendLine(",isnull(a.ExtField8,'') as ExtField8 ");
            sql.AppendLine(",isnull(a.ExtField9,'') as ExtField9 ");
            sql.AppendLine(",isnull(a.ExtField10,'') as ExtField10 ");

            sql.AppendLine("		 ,isnull(A.CanViewUserName ,'') AS  UserName                                                                  ");
            sql.AppendLine("		 ,isnull(A.CanViewUser  ,'') as  CanUserID                                                           ");

            sql.AppendLine("		 ,isnull( P.TypeName,'') as TypeName");
            sql.AppendLine("		  ,isnull( Q.CurrencyName,'') as CurrencyTypeName                                                                      ");
            sql.AppendLine("		 ,isnull(R.TypeName,'') AS  MoneyTypeName                                                                  ");
            sql.AppendLine("		  ,isnull( U.TypeName,'') as PayTypeName                                                                      ");
            sql.AppendLine("		  ,isnull( S.TypeName,'') as TakeTypeName                                                                      ");
            sql.AppendLine("		 ,isnull(T.TypeName,'') AS  CarryTypeName                                                                  ");
            sql.AppendLine("		 ,isnull(aak.ProjectName,'') AS  ProjectName                                                                  ");
            sql.AppendLine("		 ,isnull(A.ProjectID,0) AS  ProjectID                                                                  ");
            sql.AppendLine("FROM         officedba.PurchaseOrder AS A                                ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS B ON A.OurDelegate = B.ID            ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS C ON A.ProviderID = C.ID             ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS D ON A.DeptID = D.ID                     ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS E ON A.Purchaser = E.ID              ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS F ON A.Creator = F.ID                ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS P ON A.TypeID = P.ID                                      ");
            sql.AppendLine("LEFT JOIN officedba.CurrencyTypeSetting AS Q ON A.CurrencyType = Q.ID                           ");

            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS S ON A.CompanyCD = S.CompanyCD  AND A.TakeType=S.ID                     ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS T ON A.CompanyCD = T.CompanyCD  AND A.CarryType=T.ID                    ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS U ON A.CompanyCD = U.CompanyCD  AND    A.PayType=U.ID                     ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS R ON A.CompanyCD = R.CompanyCD  AND A.MoneyType=R.ID                    ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS H ON A.Confirmor = H.ID ");
            sql.AppendLine("LEFT JOIN officedba.ProjectInfo AS aak ON A.ProjectID = aak.ID ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS I ON A.Closer = I.ID    ");
            sql.AppendLine(" LEFT JOIN officedba.FlowInstance AS J ON A.CompanyCD=J.CompanyCD AND A.ID = J.BillID AND J.BillTypeFlag = 6 AND J.BillTypeCode = 4    ");
            sql.AppendLine(" AND J.ID=(SELECT max(ID) FROM officedba.FlowInstance AS K WHERE A.CompanyCD=K.CompanyCD AND A.ID = K.BillID AND K.BillTypeFlag = 6 AND K.BillTypeCode = 4 )");
            sql.AppendLine(" WHERE A.ID=@ID");
            #endregion

            #region
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
            #endregion

            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 根据采购订单ID，删除
        public static SqlCommand DeletePurchaseOrder(string IDs)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE FROM officedba.PurchaseOrder");
            sql.AppendLine("WHERE ID in " + IDs + "");

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region
        public static DataTable GetPurchaseOrderPrintByID(string ID)
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                StringBuilder sql = new StringBuilder();
                #region SQL
                sql.AppendLine("SELECT [ID]                               ");
                sql.AppendLine("      ,[CompanyCD]                        ");
                sql.AppendLine("      ,[OrderNo]                          ");
                sql.AppendLine("      ,[Title]                            ");
                sql.AppendLine("      ,[TypeID]                           ");
                sql.AppendLine("      ,[FromType]                         ");
                sql.AppendLine("      ,[CurrencyType]                     ");
                sql.AppendLine("      ,[Rate]                             ");
                sql.AppendLine("      ,[OrderDate]                        ");
                sql.AppendLine("      ,[TheyDelegate]                     ");
                sql.AppendLine("      ,[OurDelegate]                      ");
                sql.AppendLine("      ,[OurDelegateName]                  ");
                sql.AppendLine("      ,[ProviderID]                       ");
                sql.AppendLine("      ,[ProviderName]                     ");
                sql.AppendLine("      ,[DeptID]                           ");
                sql.AppendLine("      ,[DeptName]                         ");
                sql.AppendLine("      ,[PayType]                          ");
                sql.AppendLine("      ,[MoneyType]                        ");
                sql.AppendLine("      ,[Purchaser]                        ");
                sql.AppendLine("      ,[PurchaserName]                    ");
                sql.AppendLine("      ,[TakeType]                         ");
                sql.AppendLine("      ,[CarryType]                        ");
                sql.AppendLine("      ,[ProviderBillID]                   ");
                sql.AppendLine("      ,[remark]                           ");
                sql.AppendLine("      ,[TotalPrice]                       ");
                sql.AppendLine("      ,[TotalTax]                         ");
                sql.AppendLine("      ,[TotalFee]                         ");
                sql.AppendLine("      ,[Discount]                         ");
                sql.AppendLine("      ,[DiscountTotal]                    ");
                sql.AppendLine("      ,[OtherTotal]                       ");
                sql.AppendLine("      ,[RealTotal]                        ");
                sql.AppendLine("      ,[isAddTax]                         ");
                sql.AppendLine("      ,[CountTotal]                       ");
                sql.AppendLine("      ,[Confirmor]                        ");
                sql.AppendLine("      ,[ConfirmorName]                    ");
                sql.AppendLine("      ,[ConfirmDate]                      ");
                sql.AppendLine("      ,[Closer]                           ");
                sql.AppendLine("      ,[CloserName]                       ");
                sql.AppendLine("      ,[CloseDate]                        ");
                sql.AppendLine("      ,[BillStatus]                       ");
                sql.AppendLine("      ,[BillStatusName]                   ");
                sql.AppendLine("      ,[Creator]                          ");
                sql.AppendLine("      ,[CreatorName]                      ");
                sql.AppendLine("      ,[CreateDate]                       ");
                sql.AppendLine("      ,[ModifiedDate]                     ");
                sql.AppendLine("      ,[ModifiedUserID]                   ");
                sql.AppendLine("      ,[FlowStatus]                       ");
                sql.AppendLine("      ,[Attachment]                       ");
                sql.AppendLine("      ,[TypeName]                         ");
                sql.AppendLine("      ,[PayTypeName]                      ");
                sql.AppendLine("      ,[MoneyTypeName]                    ");
                sql.AppendLine("      ,[TakeTypeName]                     ");
                sql.AppendLine("      ,[CarryTypeName]                    ");
                sql.AppendLine("      ,[CurrencyName]                     ");
                sql.AppendLine("  FROM [officedba].[V_PurchaseOrder]      ");
                sql.AppendLine(" WHERE ID=@ID");
                #endregion

                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

                comm.CommandText = sql.ToString();
                return SqlHelper.ExecuteSearch(comm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 获取单据单据信息
        //ID：单据ID
        public static DataTable isHandle(string ID)
        {
            SqlCommand comm = new SqlCommand();

            string strSql = "select BillStatus,isOpenbill from officedba.PurchaseOrder where ID = @ID and CompanyCD=@CompanyCD";
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));

            comm.CommandText = strSql;
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        #endregion

        #region 采购订单明细表操作
        #region insert
        public static SqlCommand InsertPurchaseOrderDetail(PurchaseOrderDetailModel PurchaseOrderDetailM, string OrderNo)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.PurchaseOrderDetail ");
            sql.AppendLine("           (CompanyCD                     ");
            sql.AppendLine("           ,OrderNo                       ");
            sql.AppendLine("           ,SortNo                        ");
            sql.AppendLine("           ,FromType                      ");
            sql.AppendLine("           ,FromBillID                    ");
            sql.AppendLine("           ,FromLineNo                    ");
            sql.AppendLine("           ,ProductID                     ");
            sql.AppendLine("           ,ProductNo                     ");
            sql.AppendLine("           ,ProductName                   ");
            sql.AppendLine("           ,ProductCount                  ");
            sql.AppendLine("           ,UnitID                        ");
            sql.AppendLine("           ,UnitPrice                     ");
            sql.AppendLine("           ,TaxPrice                      ");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                sql.AppendLine("           ,UsedUnitID                  ");
                sql.AppendLine("           ,UsedUnitCount                        ");
                sql.AppendLine("           ,UsedPrice                     ");
                sql.AppendLine("           ,ExRate                      ");
            }
            //sql.AppendLine("           ,Discount                      ");
            sql.AppendLine("           ,TaxRate                       ");
            sql.AppendLine("           ,TotalFee                      ");
            sql.AppendLine("           ,TotalPrice                    ");
            sql.AppendLine("           ,TotalTax                      ");
            sql.AppendLine("           ,RequireDate                   ");
            sql.AppendLine("           ,ApplyReason                   ");
            sql.AppendLine("           ,Remark                        ");
            sql.AppendLine("           ,ArrivedCount)                  ");
            //sql.AppendLine("           ,ModifiedDate                  ");
            //sql.AppendLine("           ,ModifiedUserID)               ");
            sql.AppendLine("     VALUES                               ");
            sql.AppendLine("           (@CompanyCD                    ");
            sql.AppendLine("           ,@OrderNo                      ");
            sql.AppendLine("           ,@SortNo                       ");
            sql.AppendLine("           ,@FromType                     ");
            sql.AppendLine("           ,@FromBillID                   ");
            sql.AppendLine("           ,@FromLineNo                   ");
            sql.AppendLine("           ,@ProductID                    ");
            sql.AppendLine("           ,@ProductNo                    ");
            sql.AppendLine("           ,@ProductName                  ");
            sql.AppendLine("           ,@ProductCount                 ");
            sql.AppendLine("           ,@UnitID                       ");
            sql.AppendLine("           ,@UnitPrice                    ");
            sql.AppendLine("           ,@TaxPrice                     ");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                sql.AppendLine("           ,@UsedUnitID                  ");
                sql.AppendLine("           ,@UsedUnitCount                        ");
                sql.AppendLine("           ,@UsedPrice                     ");
                sql.AppendLine("           ,@ExRate                      ");
            }
            //sql.AppendLine("           ,@Discount                     ");
            sql.AppendLine("           ,@TaxRate                      ");
            sql.AppendLine("           ,@TotalFee                     ");
            sql.AppendLine("           ,@TotalPrice                   ");
            sql.AppendLine("           ,@TotalTax                     ");
            sql.AppendLine("           ,@RequireDate                  ");
            sql.AppendLine("           ,@ApplyReason                  ");
            sql.AppendLine("           ,@Remark                       ");
            sql.AppendLine("           ,@ArrivedCount)                ");
            //sql.AppendLine("           ,@ModifiedDate                 ");
            //sql.AppendLine("           ,@ModifiedUserID)              ");

            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", PurchaseOrderDetailM.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", OrderNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", PurchaseOrderDetailM.SortNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", PurchaseOrderDetailM.FromType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID", PurchaseOrderDetailM.FromBillID));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromLineNo", PurchaseOrderDetailM.FromLineNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", PurchaseOrderDetailM.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", PurchaseOrderDetailM.ProductNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", PurchaseOrderDetailM.ProductName));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount", PurchaseOrderDetailM.ProductCount));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitID", PurchaseOrderDetailM.UnitID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitPrice", PurchaseOrderDetailM.UnitPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaxPrice", PurchaseOrderDetailM.TaxPrice));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@Discount", PurchaseOrderDetailM.Discount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaxRate", PurchaseOrderDetailM.TaxRate));
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitID", PurchaseOrderDetailM.UsedUnitID));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitCount", PurchaseOrderDetailM.UsedUnitCount));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedPrice", PurchaseOrderDetailM.UsedPrice));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExRate", PurchaseOrderDetailM.ExRate));
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalFee", PurchaseOrderDetailM.TotalFee));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice", PurchaseOrderDetailM.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalTax", PurchaseOrderDetailM.TotalTax));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RequireDate", PurchaseOrderDetailM.RequireDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyReason", "0"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", PurchaseOrderDetailM.Remark));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ArrivedCount", "0"));


            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", PurchaseOrderDetailM.ModifiedDate));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", PurchaseOrderDetailM.ModifiedUserID));
            #endregion

            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region delete
        public static SqlCommand DeletePurchaseOrderDetailSingle(string OrderNo)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE FROM officedba.PurchaseOrderDetail");
            sql.AppendLine(" WHERE CompanyCD=@CompanyCD");
            sql.AppendLine(" AND OrderNo=@OrderNo");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", OrderNo));

            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region 根据采购订单编号填充订单明细
        public static DataTable GetPurchaseOrderDetail(string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  A.ID                                                                               ");
            sql.AppendLine("      , A.OrderNo                                                                          ");
            sql.AppendLine("      , A.CompanyCD                                                                        ");
            sql.AppendLine("      , isnull(A.SortNo     ,'') AS SortNo                                                 ");
            sql.AppendLine("      , isnull(A.FromBillID ,'') AS FromBillID                                             ");
            sql.AppendLine(",isnull(A.FromType,'') AS  FromType                                                        ");
            sql.AppendLine(",case A.FromType when '0' then ''                                                          ");
            sql.AppendLine(" when '1' then (select ApplyNo from officedba.PurchaseApply where ID=A.FromBillID)         ");
            sql.AppendLine(" when '2' then (select PlanNo from officedba.PurchasePlan where ID=A.FromBillID)           ");
            sql.AppendLine(" when '3' then (select AskNo from officedba.PurchaseAskPrice where ID=A.FromBillID)        ");
            sql.AppendLine(" when '4' then (select ContractNo from officedba.PurchaseContract where ID= A.FromBillID)  ");
            sql.AppendLine("  end AS FromBillNo                                                                        ");
            sql.AppendLine("      , isnull(A.FromLineNo ,'') AS FromLineNo                                             ");
            sql.AppendLine("      , isnull(A.ProductID  ,'') AS ProductID                                              ");
            sql.AppendLine("      , isnull(B.ProdNo  ,'') AS ProductNo                                              ");
            sql.AppendLine("      , isnull(B.ProductName,'') AS ProductName                                            ");
            sql.AppendLine("      , isnull(B.Specification,'') as Specification                                        ");
            sql.AppendLine("      , Convert(numeric(14," + userInfo.SelPoint + "),a.ProductCount) as ProductCount                               ");
            sql.AppendLine("      , isnull(A.UnitID      ,0) AS UnitID                                                 ");
            sql.AppendLine("      , isnull(E.Rate     ,0) AS Rate                                                 ");
            sql.AppendLine("      , isnull(C.CodeName    ,'') AS UnitName                                              ");
            sql.AppendLine("      , isnull(ff.CodeName    ,'') AS UsedUnitName                                              ");

            sql.AppendLine("      , Convert(numeric(14," + userInfo.SelPoint + "),a.UnitPrice) as UnitPrice                                        ");
            sql.AppendLine("      , Convert(numeric(22," + userInfo.SelPoint + "),a.TaxPrice) as TaxPrice                                            ");
            //sql.AppendLine("      , isnull(A.Discount    ,0) AS Discount                                               ");
            sql.AppendLine("      , Convert(numeric(22," + userInfo.SelPoint + "),a.TaxRate) as TaxRate                                           ");
            sql.AppendLine("      , Convert(numeric(22," + userInfo.SelPoint + "),a.TotalFee) as TotalFee                                            ");
            sql.AppendLine("      , Convert(numeric(22," + userInfo.SelPoint + "),a.TotalPrice) as TotalPrice                                     ");
            sql.AppendLine("      , Convert(numeric(22," + userInfo.SelPoint + "),a.TotalTax) as TotalTax                                    ");
            sql.AppendLine("      , Convert(numeric(12," + userInfo.SelPoint + "),a.ExRate) as ExRate                                                ");
            sql.AppendLine("      , Convert(numeric(14," + userInfo.SelPoint + "),a.UsedPrice) as UsedPrice                                   ");
            sql.AppendLine("      , Convert(numeric(14," + userInfo.SelPoint + "),a.UsedUnitCount) as UsedUnitCount                                     ");
            sql.AppendLine("      , isnull(A.UsedUnitID    ,0) AS UsedUnitID                                               ");
            sql.AppendLine("      , isnull(CONVERT(varchar(23),A.RequireDate,23),'') AS RequireDate                    ");
            sql.AppendLine("      , isnull(A.ApplyReason ,0) AS ApplyReason                                            ");
            sql.AppendLine("      , isnull(D.CodeName,'') AS ApplyReasonName                                           ");
            sql.AppendLine("      , isnull(A.Remark,'') AS  Remark                                                     ");
            sql.AppendLine("      , Convert(numeric(14," + userInfo.SelPoint + "),a.ArrivedCount) as ArrivedCount,isnull(H.TypeName,'') as ColorName ");
            //sql.AppendLine("      , isnull(A.OrderCount,0) AS OrderCount");
            sql.AppendLine("FROM  officedba.PurchaseOrderDetail AS A                                                   ");
            sql.AppendLine("INNER JOIN officedba.PurchaseOrder AS E ON A.CompanyCD=E.CompanyCD AND A.OrderNo=E.OrderNO AND E.ID=@ID");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.ProductID = B.ID                                 ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS C ON A.UnitID = C.ID                                   ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS ff  ON A.UsedUnitID = ff.ID                                   ");
            sql.AppendLine("LEFT JOIN officedba.CodeReasonType AS D ON A.ApplyReason = D.ID                            ");
            sql.AppendLine("left join officedba.CodePublicType H on B.ColorID=H.ID");
            //sql.AppendLine("LEFT JOIN officedba.PurchaseOrder AS E ON A.OrderNo=E.OrderNo AND A.CompanyCD=E.CompanyCD  ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine(" AND A.CompanyCD=@CompanyCD");
            sql.AppendLine(" order by A.SortNo ");

            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 根据采购订单编号删除订单明细
        public static SqlCommand DeletePurchaseOrderDetail(string OrderNos)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE FROM officedba.PurchaseOrderDetail");
            sql.AppendLine("WHERE OrderNo in " + OrderNos + "");
            sql.AppendLine("AND CompanyCD =@CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion
        #endregion


        #region 查询采购历史价格列表
        /// <summary>
        /// 根据检索条件检索出满足条件的信息 Added By songfei 2009-04-24
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable SelectHistoryAskPriceList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProductID, string StartPurchaseDate, string EndPurchaseDate)
        {//取最大时期的含税价，同一天(最大日期)也有多条数据，则取最大ID为最新的一条数据
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            sql.AppendLine("SELECT distinct A.ProductID,isnull(B.ProdNo,'') AS ProductNo,isnull(B.ProductName,'') AS ProductName,isnull(B.Specification,'') AS Specification,A.UnitID,C.CodeName AS UnitName ");
            sql.AppendLine(",isnull(Convert(numeric(12," + userInfo.SelPoint + "),max(DISTINCT(A.TaxPrice*D.Rate))),0)as LargeTaxPrice,isnull(Convert(numeric(12," + userInfo.SelPoint + "),min(DISTINCT(A.TaxPrice*D.Rate))),0)as SmallTaxPrice,isnull(Convert(numeric(12," + userInfo.SelPoint + "),avg(DISTINCT(A.TaxPrice*D.Rate))),0) as AverageTaxPrice");
            sql.AppendLine(",Convert(numeric(12," + userInfo.SelPoint + "),(select  top 1  e.TaxPrice*f.Rate from officedba.PurchaseOrderDetail as e,officedba.PurchaseOrder as f  ");
            sql.AppendLine(" where e.OrderNo = f.OrderNo and e.CompanyCD = f.CompanyCD  and e.ProductID=A.ProductID and  ");
            sql.AppendLine(" f.id = ( select max(g.id) from officedba.PurchaseOrder as g,officedba.PurchaseOrderDetail as h");
            sql.AppendLine("where g.CompanyCD = h.CompanyCD and g.OrderNo=h.OrderNo and h.ProductID = a.ProductID and  g.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "') and f.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' ) )AS NewTaxPrice");


            sql.AppendLine(" FROM officedba.PurchaseOrderDetail AS A                                                                ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProductID=B.ID                  ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS C ON A.CompanyCD = C.CompanyCD AND A.UnitID=C.ID                    ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseOrder AS D ON A.CompanyCD = D.CompanyCD AND A.OrderNo=D.OrderNo             ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND A.ProductID=@ProductID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            }
            if (StartPurchaseDate != null && StartPurchaseDate != "")
            {
                sql.AppendLine(" AND D.OrderDate >=@StartPurchaseDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartPurchaseDate", StartPurchaseDate));
            }
            if (EndPurchaseDate != "" && EndPurchaseDate != null)
            {
                sql.AppendLine(" AND D.OrderDate <@EndPurchaseDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndPurchaseDate", Convert.ToDateTime(EndPurchaseDate).AddDays(1).ToString("yyyy-MM-dd")));
            }




            sql.AppendLine(" GROUP BY A.ProductID,B.ProdNo,B.ProductName,B.Specification,A.UnitID,C.CodeName");

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);

        }
        public static DataTable SelectHistoryAskPriceListPrint(string orderBy, string ProductID, string StartPurchaseDate, string EndPurchaseDate)
        {//取最大时期的含税价，同一天(最大日期)也有多条数据，则取最大ID为最新的一条数据
            SqlCommand comm = new SqlCommand();

            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ProductID,isnull(B.ProdNo,'') AS ProductNo,isnull(B.ProductName,'') AS ProductName,isnull(B.Specification,'') AS Specification,A.UnitID,C.CodeName AS UnitName ");
            sql.AppendLine(",isnull(Convert(numeric(20," + jingdu + "),max(DISTINCT(A.TaxPrice*D.Rate))),0)as LargeTaxPrice,isnull(Convert(numeric(20," + jingdu + "),min(DISTINCT(A.TaxPrice*D.Rate))),0)as SmallTaxPrice,isnull(Convert(numeric(20," + jingdu + "),avg(DISTINCT(A.TaxPrice*D.Rate))),0) as AverageTaxPrice");
            sql.AppendLine(",Convert(numeric(20," + jingdu + "),(select  top 1  e.TaxPrice*f.Rate from officedba.PurchaseOrderDetail as e,officedba.PurchaseOrder as f  ");
            sql.AppendLine(" where e.OrderNo = f.OrderNo and e.CompanyCD = f.CompanyCD  and e.ProductID=A.ProductID and  ");
            sql.AppendLine(" f.id = ( select max(g.id) from officedba.PurchaseOrder as g,officedba.PurchaseOrderDetail as h");
            sql.AppendLine("where g.CompanyCD = h.CompanyCD and g.OrderNo=h.OrderNo and h.ProductID = a.ProductID and  g.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "') and f.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' ) )AS NewTaxPrice");


            sql.AppendLine(" FROM officedba.PurchaseOrderDetail AS A                                                                ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProductID=B.ID                  ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS C ON A.CompanyCD = C.CompanyCD AND A.UnitID=C.ID                    ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseOrder AS D ON A.CompanyCD = D.CompanyCD AND A.OrderNo=D.OrderNo             ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND A.ProductID=@ProductID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            }
            if (StartPurchaseDate != null && StartPurchaseDate != "")
            {
                sql.AppendLine(" AND D.OrderDate >=@StartPurchaseDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartPurchaseDate", StartPurchaseDate));
            }
            if (EndPurchaseDate != "" && EndPurchaseDate != null)
            {
                sql.AppendLine(" AND D.OrderDate <@EndPurchaseDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndPurchaseDate", Convert.ToDateTime(EndPurchaseDate).AddDays(1).ToString("yyyy-MM-dd")));
            }




            sql.AppendLine(" GROUP BY A.ProductID,B.ProdNo,B.ProductName,B.Specification,A.UnitID,C.CodeName");
            sql.AppendLine(" ORDER BY " + orderBy + "");
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderBy",orderBy));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        #region 查询采购历史价格链接页面
        /// <summary>
        /// 根据物品编号链接要显示的信息 Added By songfei 2009-04-24
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable SelectHistoryAskPriceShowList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProductID)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID, A.ProductID,B.ProdNo AS ProductNo,B.ProductName,isnull(B.Specification,'') AS Specification,A.UnitID,C.CodeName AS UnitName ");
            sql.AppendLine(",  A.OrderNo,isnull(CONVERT(varchar(100),D.CreateDate,23),'') AS PurchaseDate ,isnull(D.Purchaser,0) AS Purchaser ,isnull(E.EmployeeName,'') AS PurchaserName ,isnull(D.ProviderID,0) AS ProviderID ,isnull(F.CustName,'') AS ProviderName");
            sql.AppendLine(",  isnull(Convert(numeric(12,2),A.UnitPrice),0) AS UnitPrice,isnull(Convert(numeric(12,2),A.TaxRate),0) AS TaxRate,isnull(Convert(numeric(12,2),A.TaxPrice),0) AS TaxPrice");
            sql.AppendLine(",  isnull(Convert(numeric(12,2),A.ProductCount),0) AS ProductCount,isnull(Convert(numeric(12,2),A.TotalFee),0) AS   TotalFee  ");

            sql.AppendLine(" FROM officedba.PurchaseOrderDetail AS A                                                                ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProductID=B.ID                  ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS C ON A.CompanyCD = C.CompanyCD AND A.UnitID=C.ID                    ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseOrder AS D ON A.CompanyCD = D.CompanyCD AND A.OrderNo=D.OrderNo             ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS E ON D.CompanyCD = E.CompanyCD AND D.Purchaser=E.ID                  ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo   AS F on A.CompanyCD = F.CompanyCD  AND D.ProviderID=F.ID              ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND A.ProductID=@ProductID ");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);


        }
        #endregion

        #region 采购报表采购价格分析
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchasePriceAnalyse(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProductID, string StartOrderDate, string EndOrderDate, string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();


            sql.AppendLine("SELECT distinct A.ProductID,isnull(B.ProdNo,'') AS CompanyCD,isnull(B.ProductName,'') AS OrderNo  ");
            sql.AppendLine(",isnull(Convert(numeric(12,2),max(DISTINCT(A.TaxPrice))),0)as TotalFee,isnull(Convert(numeric(12,2),min(DISTINCT(A.TaxPrice))),0)as TotalTax ");
            sql.AppendLine(",isnull(Convert(numeric(12,2),avg(DISTINCT(A.TaxPrice))),0) as Rate ");
            sql.AppendLine(",Convert(numeric(12,2),(select e.TaxPrice from officedba.PurchaseOrderDetail as e,officedba.PurchaseOrder as f  ");
            sql.AppendLine("where e.OrderNo = f.OrderNo and e.CompanyCD = f.CompanyCD  and e.ProductID=A.ProductID and   ");
            sql.AppendLine(" f.id = ( select max(g.id) from officedba.PurchaseOrder as g,officedba.PurchaseOrderDetail as h ");
            sql.AppendLine("where g.CompanyCD = h.CompanyCD and g.OrderNo=h.OrderNo and h.ProductID = a.ProductID and   ");
            sql.AppendLine("g.CompanyCD = '" + CompanyCD + "') and f.CompanyCD = '" + CompanyCD + "' AND f.BillStatus <>1 ) )AS TotalPrice ");
            sql.AppendLine(",D.OrderNo as TheyDelegate,isnull(F.CustName,'') as Title,G.OrderNo as remark,isnull(I.CustName,'') as ProviderBillID ");
            sql.AppendLine(" FROM officedba.PurchaseOrderDetail AS A   ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProductID=B.ID  ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseOrder AS C ON A.CompanyCD = C.CompanyCD AND A.OrderNo=C.OrderNo  ");
            sql.AppendLine("left join officedba.PurchaseOrderDetail as D ON A.ProductID = D.ProductID and A.CompanyCD = D.CompanyCD and  D.TaxPrice ");
            sql.AppendLine(" = ( select min(DISTINCT(zz.TaxPrice)) from officedba.PurchaseOrderDetail as zz where zz.ProductID = A.ProductID)   ");
            sql.AppendLine("left join officedba.PurchaseOrder as E on D.OrderNo =E.OrderNo and D.CompanyCD =E.CompanyCD  ");
            sql.AppendLine("left join  officedba.ProviderInfo as F on E.ProviderID =F.id and E.CompanyCD =F.CompanyCD   ");
            sql.AppendLine("left join officedba.PurchaseOrderDetail as G ON A.ProductID = G.ProductID and A.CompanyCD = G.CompanyCD and  G.TaxPrice ");
            sql.AppendLine(" = ( select max(DISTINCT(yy.TaxPrice)) from officedba.PurchaseOrderDetail as yy where yy.ProductID = A.ProductID)  ");
            sql.AppendLine("left join officedba.PurchaseOrder as H on G.OrderNo =H.OrderNo and G.CompanyCD =H.CompanyCD   ");
            sql.AppendLine("left join  officedba.ProviderInfo as I on H.ProviderID =I.id and H.CompanyCD =I.CompanyCD  ");




            sql.AppendLine(" WHERE 1=1 AND C.BillStatus <>1 ");
            sql.AppendLine("AND A.CompanyCD = '" + CompanyCD + "'");
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND A.ProductID=@ProductID ");
            }
            if (StartOrderDate != "" && StartOrderDate != null)
            {
                sql.AppendLine(" AND C.OrderDate >=@StartOrderDate ");
            }
            if (EndOrderDate != "" && EndOrderDate != null)
            {
                sql.AppendLine(" AND C.OrderDate <@EndOrderDate ");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartOrderDate", StartOrderDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndOrderDate", Convert.ToDateTime(EndOrderDate).AddDays(1).ToString("yyyy-MM-dd")));
            sql.AppendLine("group by A.ProductID,B.ProdNo,B.ProductName,D.OrderNo,f.CustName,G.OrderNo,I.CustName");


            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion

        #region 采购报表采购价格分析打印
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchasePriceAnalysePrint(string ProductID, string StartOrderDate, string EndOrderDate, string CompanyCD, string orderBy)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();


            sql.AppendLine("SELECT distinct A.ProductID,isnull(B.ProdNo,'') AS CompanyCD,isnull(B.ProductName,'') AS OrderNo  ");
            sql.AppendLine(",isnull(Convert(numeric(12,2),max(DISTINCT(A.TaxPrice))),0)as TotalFee,isnull(Convert(numeric(12,2),min(DISTINCT(A.TaxPrice))),0)as TotalTax ");
            sql.AppendLine(",isnull(Convert(numeric(12,2),avg(DISTINCT(A.TaxPrice))),0) as Rate ");
            sql.AppendLine(",Convert(numeric(12,2),(select e.TaxPrice from officedba.PurchaseOrderDetail as e,officedba.PurchaseOrder as f  ");
            sql.AppendLine("where e.OrderNo = f.OrderNo and e.CompanyCD = f.CompanyCD  and e.ProductID=A.ProductID and   ");
            sql.AppendLine(" f.id = ( select max(g.id) from officedba.PurchaseOrder as g,officedba.PurchaseOrderDetail as h ");
            sql.AppendLine("where g.CompanyCD = h.CompanyCD and g.OrderNo=h.OrderNo and h.ProductID = a.ProductID and   ");
            sql.AppendLine("g.CompanyCD = '" + CompanyCD + "') and f.CompanyCD = '" + CompanyCD + "' AND f.BillStatus <>1  ) )AS TotalPrice ");
            sql.AppendLine(",D.OrderNo as TheyDelegate,isnull(F.CustName,'') as Title,G.OrderNo as remark,isnull(I.CustName,'') as ProviderBillID ");
            sql.AppendLine(" FROM officedba.PurchaseOrderDetail AS A   ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProductID=B.ID  ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseOrder AS C ON A.CompanyCD = C.CompanyCD AND A.OrderNo=C.OrderNo  ");
            sql.AppendLine("left join officedba.PurchaseOrderDetail as D ON A.ProductID = D.ProductID and A.CompanyCD = D.CompanyCD and  D.TaxPrice ");
            sql.AppendLine(" = ( select min(DISTINCT(zz.TaxPrice)) from officedba.PurchaseOrderDetail as zz where zz.ProductID = A.ProductID)   ");
            sql.AppendLine("left join officedba.PurchaseOrder as E on D.OrderNo =E.OrderNo and D.CompanyCD =E.CompanyCD  ");
            sql.AppendLine("left join  officedba.ProviderInfo as F on E.ProviderID =F.id and E.CompanyCD =F.CompanyCD   ");
            sql.AppendLine("left join officedba.PurchaseOrderDetail as G ON A.ProductID = G.ProductID and A.CompanyCD = G.CompanyCD and  G.TaxPrice ");
            sql.AppendLine(" = ( select max(DISTINCT(yy.TaxPrice)) from officedba.PurchaseOrderDetail as yy where yy.ProductID = A.ProductID)  ");
            sql.AppendLine("left join officedba.PurchaseOrder as H on G.OrderNo =H.OrderNo and G.CompanyCD =H.CompanyCD   ");
            sql.AppendLine("left join  officedba.ProviderInfo as I on H.ProviderID =I.id and H.CompanyCD =I.CompanyCD  ");




            sql.AppendLine(" WHERE 1=1 AND C.BillStatus <>1 ");
            sql.AppendLine("AND A.CompanyCD = '" + CompanyCD + "'");
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND A.ProductID=@ProductID ");
            }
            if (StartOrderDate != "" && StartOrderDate != null)
            {
                sql.AppendLine(" AND C.OrderDate >=@StartOrderDate ");
            }
            if (EndOrderDate != "" && EndOrderDate != null)
            {
                sql.AppendLine(" AND C.OrderDate <@EndOrderDate ");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartOrderDate", StartOrderDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndOrderDate", Convert.ToDateTime(EndOrderDate).AddDays(1).ToString("yyyy-MM-dd")));
            sql.AppendLine("group by A.ProductID,B.ProdNo,B.ProductName,D.OrderNo,f.CustName,G.OrderNo,I.CustName");
            sql.AppendLine(" ORDER BY " + orderBy + "");


            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
            //return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion

        #region 采购报表采购历史价查询
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseHisPriceQuery(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProductID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ProductID,isnull(B.ProdNo,'') AS ProductNo,isnull(B.ProductName,'') AS ProductName,isnull(B.Specification,'') AS Specification,A.UnitID,C.CodeName AS UnitName ");
            sql.AppendLine(",isnull(Convert(numeric(12,2),max(DISTINCT(A.TaxPrice*D.Rate))),0)as LargeTaxPrice,isnull(Convert(numeric(12,2),min(DISTINCT(A.TaxPrice*D.Rate))),0)as SmallTaxPrice,isnull(Convert(numeric(12,2),avg(DISTINCT(A.TaxPrice*D.Rate))),0) as AverageTaxPrice");
            sql.AppendLine(",Convert(numeric(12,2),(select e.TaxPrice from officedba.PurchaseOrderDetail as e,officedba.PurchaseOrder as f  ");
            sql.AppendLine(" where e.OrderNo = f.OrderNo and e.CompanyCD = f.CompanyCD  and e.ProductID=A.ProductID and  ");
            sql.AppendLine(" f.id = ( select max(g.id) from officedba.PurchaseOrder as g,officedba.PurchaseOrderDetail as h");
            sql.AppendLine("where g.CompanyCD = h.CompanyCD and g.OrderNo=h.OrderNo and h.ProductID = a.ProductID and  g.CompanyCD = '" + CompanyCD + "') and f.CompanyCD = '" + CompanyCD + "' ) )AS NewTaxPrice");


            sql.AppendLine(" FROM officedba.PurchaseOrderDetail AS A                                                                ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProductID=B.ID                  ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS C ON A.CompanyCD = C.CompanyCD AND A.UnitID=C.ID                    ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseOrder AS D ON A.CompanyCD = D.CompanyCD AND A.OrderNo=D.OrderNo             ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + CompanyCD + "'");
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND A.ProductID=@ProductID ");
            }
            if (StartConfirmDate != null && StartConfirmDate != "")
            {
                sql.AppendLine(" AND D.OrderDate >=@StartConfirmDate");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND D.OrderDate <@EndConfirmDate ");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", Convert.ToDateTime(EndConfirmDate).AddDays(1).ToString("yyyy-MM-dd")));
            sql.AppendLine(" GROUP BY A.ProductID,B.ProdNo,B.ProductName,B.Specification,A.UnitID,C.CodeName");

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion

        #region 采购报表采购历史价查询打印
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseHisPriceQueryPrint(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProductID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ProductID,isnull(B.ProdNo,'') AS ProductNo,isnull(B.ProductName,'') AS ProductName,isnull(B.Specification,'') AS Specification,A.UnitID,C.CodeName AS UnitName ");
            sql.AppendLine(",isnull(Convert(numeric(12,2),max(DISTINCT(A.TaxPrice*D.Rate))),0)as LargeTaxPrice,isnull(Convert(numeric(12,2),min(DISTINCT(A.TaxPrice*D.Rate))),0)as SmallTaxPrice,isnull(Convert(numeric(12,2),avg(DISTINCT(A.TaxPrice*D.Rate))),0) as AverageTaxPrice");
            sql.AppendLine(",Convert(numeric(12,2),(select e.TaxPrice from officedba.PurchaseOrderDetail as e,officedba.PurchaseOrder as f  ");
            sql.AppendLine(" where e.OrderNo = f.OrderNo and e.CompanyCD = f.CompanyCD  and e.ProductID=A.ProductID and  ");
            sql.AppendLine(" f.id = ( select max(g.id) from officedba.PurchaseOrder as g,officedba.PurchaseOrderDetail as h");
            sql.AppendLine("where g.CompanyCD = h.CompanyCD and g.OrderNo=h.OrderNo and h.ProductID = a.ProductID and  g.CompanyCD = '" + CompanyCD + "') and f.CompanyCD = '" + CompanyCD + "' ) )AS NewTaxPrice");


            sql.AppendLine(" FROM officedba.PurchaseOrderDetail AS A                                                                ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProductID=B.ID                  ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS C ON A.CompanyCD = C.CompanyCD AND A.UnitID=C.ID                    ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseOrder AS D ON A.CompanyCD = D.CompanyCD AND A.OrderNo=D.OrderNo             ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + CompanyCD + "'");
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND A.ProductID=@ProductID ");
            }
            if (StartConfirmDate != null && StartConfirmDate != "")
            {
                sql.AppendLine(" AND D.OrderDate >=@StartConfirmDate");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND D.OrderDate <@EndConfirmDate ");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", Convert.ToDateTime(EndConfirmDate).AddDays(1).ToString("yyyy-MM-dd")));
            sql.AppendLine(" GROUP BY A.ProductID,B.ProdNo,B.ProductName,B.Specification,A.UnitID,C.CodeName");

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 报表
        //采购订单查询
        public static DataTable SelectPurchaseOrder(string CompanyCD, string BillStatus, string StartDate, string EndDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  A.ID,isnull(A.OrderNo,'') as OrderNo, isnull(A.Title,'') as Title,CONVERT(varchar(23),A.OrderDate,23) AS OrderDate, isnull(A.RealTotal*A.Rate,0) AS RealTotal, isnull(A.CountTotal,0) as CountTotal, A.BillStatus                                                                  ");
            sql.AppendLine(",case A.BillStatus when 1 then '制单' when '2' then '执行'  when '3' then '变更'  when '4' then '手工结单'                                        ");
            sql.AppendLine(" when '5' then '自动结单' end AS BillStatusName                                                                                                   ");
            sql.AppendLine(", A.ProviderID,isnull(D.CustName,'') AS ProviderName, A.Purchaser,isnull(E.EmployeeName,'')  AS PurchaserName                                                            ");
            sql.AppendLine(",isnull( (SELECT SUM(isnull(B.ArrivedCount,0)) FROM officedba.PurchaseOrderDetail AS B WHERE A.CompanyCD = B.CompanyCD AND A.OrderNo = B.OrderNo ),0) AS ArrivedCount ");
            sql.AppendLine(",isnull((SELECT SUM(isnull(C.InCount,0)) FROM officedba.PurchaseArriveDetail AS C WHERE A.ID = C.FromBillID AND C.FromType = '1'),0) AS InCount                       ");
            sql.AppendLine(",isnull((SELECT SUM(isnull(C.BackCount,0)) FROM officedba.PurchaseArriveDetail AS C WHERE A.ID = C.FromBillID AND C.FromType = '1'),0) AS BackCount                   ");
            sql.AppendLine(",isnull(F.YAccounts,0) as YAccounts,isnull(isnull(A.RealTotal,0)-isnull(F.YAccounts,0),0)  AS NAccounts ");
            sql.AppendLine(" FROM officedba.PurchaseOrder AS A                                                                                                                 ");
            sql.AppendLine(" LEFT JOIN officedba.ProviderInfo AS D ON A.ProviderID=D.ID                                                                                        ");
            sql.AppendLine(" LEFT JOIN officedba.EmployeeInfo AS E ON A.Purchaser=E.ID                                                                                         ");
            sql.AppendLine(" LEFT JOIN officedba.BlendingDetails AS F ON F.BillingType=2 AND A.CompanyCD=F.CompanyCD AND A.OrderNo=F.BillCD");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine(" AND A.CompanyCD=@CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (BillStatus != "")
            {
                sql.AppendLine(" AND A.BillStatus=@BillStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", BillStatus));
            }
            if (StartDate != null && StartDate != "")
            {
                sql.AppendLine(" AND A.OrderDate >= @StartDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
            }
            if (EndDate != null)
            {
                sql.AppendLine(" AND A.OrderDate <= @EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        public static DataTable SelectPurchaseOrder(string CompanyCD, string OrderBy, string BillStatus, string StartDate, string EndDate)
        {
            SqlCommand comm = new SqlCommand();
            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  A.ID,A.OrderNo, A.Title,CONVERT(varchar(23),A.OrderDate,23) AS OrderDate, A.RealTotal, A.CountTotal, A.BillStatus                                                                  ");
            sql.AppendLine(",case A.BillStatus when 1 then '制单' when '2' then '执行'  when '3' then '变更'  when '4' then '手工结单'                                        ");
            sql.AppendLine(" when '5' then '自动结单' end AS BillStatusName                                                                                                   ");
            sql.AppendLine(", A.ProviderID,D.CustName AS ProviderName, A.Purchaser,E.EmployeeName AS PurchaserName                                                            ");
            sql.AppendLine(",Convert(decimal(22," + jingdu + "), ISNULL((SELECT SUM(ISNULL(B.ArrivedCount,0)) FROM officedba.PurchaseOrderDetail AS B WHERE A.CompanyCD = B.CompanyCD AND A.OrderNo = B.OrderNo ),0)) AS ArrivedCount ");
            sql.AppendLine(",Convert(decimal(22," + jingdu + "), ISNULL((SELECT SUM(ISNULL(C.InCount,0)) FROM officedba.PurchaseArriveDetail AS C WHERE A.ID = C.FromBillID AND C.FromType = '1'),0) )AS InCount                       ");
            sql.AppendLine(",Convert(decimal(22," + jingdu + "), ISNULL((SELECT SUM(ISNULL(C.BackCount,0)) FROM officedba.PurchaseArriveDetail AS C WHERE A.ID = C.FromBillID AND C.FromType = '1'),0)) AS BackCount                   ");
            sql.AppendLine(",Convert(decimal(22," + jingdu + "), ISNULL(F.YAccounts,0)) AS YAccounts,Convert(decimal(22," + jingdu + "), A.RealTotal-isnull(F.YAccounts,0))  AS NAccounts ");
            sql.AppendLine(" FROM officedba.PurchaseOrder AS A                                                                                                                 ");
            sql.AppendLine(" LEFT JOIN officedba.ProviderInfo AS D ON A.ProviderID=D.ID                                                                                        ");
            sql.AppendLine(" LEFT JOIN officedba.EmployeeInfo AS E ON A.Purchaser=E.ID                                                                                         ");
            sql.AppendLine(" LEFT JOIN officedba.BlendingDetails AS F ON F.BillingType=2 AND A.CompanyCD=F.CompanyCD AND A.OrderNo=F.BillCD");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine(" AND A.CompanyCD=@CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (BillStatus != "")
            {
                sql.AppendLine(" AND A.BillStatus=@BillStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", BillStatus));
            }
            if (StartDate != null && StartDate != "")
            {
                sql.AppendLine(" AND A.OrderDate >= @StartDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
            }
            if (EndDate != null)
            {
                sql.AppendLine(" AND A.OrderDate <= @EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }
            sql.AppendLine(" ORDER BY " + OrderBy + "");
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderBy",OrderBy));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }

        //采购价格查询
        public static DataTable GetPurchasePrice(string CompanyCD, string ProductID, string StartDate, string EndDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();
            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT [CompanyCD]                       ");
            sql.AppendLine("      ,isnull(OrderNo,'') as OrderNo                         ");
            sql.AppendLine("      ,[ProductID]                       ");
            sql.AppendLine("      ,isnull(ProductNo,'') as ProductNo                       ");
            sql.AppendLine("      ,isnull(ProductName ,'') as   ProductName                 ");

            sql.AppendLine("      ,Convert(decimal(22," + jingdu + "),isnull(TaxPriceAfterDisCount,0)) as  TaxPriceAfterDisCount          ");
            sql.AppendLine("      ,isnull(DisCount,0)  as DisCount         ");


            sql.AppendLine("      ,Convert(decimal(22," + jingdu + "),isnull(TaxPrice,0)) as  TaxPrice          ");

            sql.AppendLine("      ,[OrderDate]                       ");
            sql.AppendLine("      ,[ProviderID]                      ");
            sql.AppendLine("      ,isnull(ProviderName,'') as ProviderName                    ");
            sql.AppendLine("      ,[ID]                              ");
            sql.AppendLine("      ,[Purchaser]                       ");
            sql.AppendLine("      , isnull(PurchaserName,'') as PurchaserName                   ");
            sql.AppendLine("  FROM [officedba].[V_PurchasePriceQuery]");

            sql.AppendLine(" WHERE CompanyCD=@CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (!string.IsNullOrEmpty(ProductID))
            {
                sql.AppendLine(" AND ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                sql.AppendLine(" AND OrderDate>=@StartDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" AND OrderDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }

        public static DataTable GetPurchasePrice(string CompanyCD, string OrderBy, string ProductID, string StartDate, string EndDate)
        {
            SqlCommand comm = new SqlCommand();
            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT [CompanyCD]                       ");
            sql.AppendLine("      ,[OrderNo]                         ");
            sql.AppendLine("      ,[ProductID]                       ");
            sql.AppendLine("      ,[ProductNo]                       ");
            sql.AppendLine("      ,[ProductName]                     ");
            sql.AppendLine("      ,Convert(decimal(22," + jingdu + "),isnull(TaxPriceAfterDisCount,0)) as  TaxPriceAfterDisCount          ");
            sql.AppendLine("      ,    DisCount          ");
            sql.AppendLine("      ,Convert(decimal(22," + jingdu + "),isnull(TaxPrice,0)) as  TaxPrice         ");
            sql.AppendLine("      ,[OrderDate]                       ");
            sql.AppendLine("      ,[ProviderID]                      ");
            sql.AppendLine("      ,[ProviderName]                    ");
            sql.AppendLine("      ,[ID]                              ");
            sql.AppendLine("      ,[Purchaser]                       ");
            sql.AppendLine("      ,[PurchaserName]                   ");
            sql.AppendLine("  FROM [officedba].[V_PurchasePriceQuery]");

            sql.AppendLine(" WHERE CompanyCD=@CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (!string.IsNullOrEmpty(ProductID))
            {
                sql.AppendLine(" AND ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                sql.AppendLine(" AND OrderDate>=@StartDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" AND OrderDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }
            sql.AppendLine(" ORDER BY " + OrderBy + "");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }

        //按采购员统计
        public static DataTable GetPurStatByPurchaser(string CompanyCD, string Purchaser, string StartDate, string EndDate, string OrderBy)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.*,B.EmployeeName AS PurchaserName FROM ");
            sql.AppendLine("    (SELECT A.Purchaser,A.MAXNccounts,A.OwnDays,B.ProviderCount,B.YAccounts,B.NAccounts,B.RealTotal FROM ");
            sql.AppendLine("           (SELECT  A.Purchaser, MAX((A.RealTotal - ISNULL(B.YAccounts, 0)*A.Rate)) AS MAXNccounts, MAX(DATEDIFF(d, A.ConfirmDate, GETDATE()) - ISNULL(C.AllowDefaultDays,0)) AS OwnDays ");
            sql.AppendLine("             FROM officedba.PurchaseOrder AS A  ");
            sql.AppendLine("             INNER JOIN  officedba.ProviderInfo AS C ON A.ProviderID = C.ID  ");
            sql.AppendLine("             LEFT OUTER JOIN  officedba.BlendingDetails AS B ON A.CompanyCD = B.CompanyCD AND B.BillingType = 2 AND A.ID = B.SourceID  ");
            sql.AppendLine("             WHERE     (A.BillStatus <> '1') AND (A.RealTotal > ISNULL(B.YAccounts, 0)) AND A.CompanyCD=@CompanyCD   ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (!string.IsNullOrEmpty(StartDate))
            {
                sql.AppendLine(" AND A.ConfirmDate>=@StartDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" AND A.ConfirmDate<@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }
            if (!string.IsNullOrEmpty(Purchaser))
            {
                sql.AppendLine(" AND A.Purchaser=@Purchaser");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Purchaser", Purchaser));
            }

            sql.AppendLine("GROUP BY A.Purchaser) AS A     ");
            sql.AppendLine("INNER JOIN        ");
            sql.AppendLine("(SELECT     COUNT(DISTINCT A.ProviderID) AS ProviderCount, SUM(ISNULL(B.YAccounts, 0)*A.Rate) AS YAccounts, SUM(A.RealTotal*A.Rate) - SUM(ISNULL(B.YAccounts, 0)*A.Rate) AS NAccounts, A.Purchaser, SUM(A.RealTotal*A.Rate) AS RealTotal   ");
            sql.AppendLine("FROM         officedba.PurchaseOrder AS A LEFT OUTER JOIN     ");
            sql.AppendLine("                      officedba.BlendingDetails AS B ON A.CompanyCD = B.CompanyCD AND B.BillingType = 2 AND A.ID = B.SourceID ");
            sql.AppendLine("WHERE  (A.BillStatus <> '1') AND A.CompanyCD=@CompanyCD ");
            if (!string.IsNullOrEmpty(StartDate))
            {
                sql.AppendLine(" AND A.ConfirmDate>=@StartDate");
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" AND A.ConfirmDate<@EndDate");
            }
            if (!string.IsNullOrEmpty(Purchaser))
            {
                sql.AppendLine(" AND A.Purchaser=@Purchaser");
            }
            sql.AppendLine("GROUP BY A.Purchaser) AS B ON A.Purchaser=B.Purchaser) AS A   ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS B ON A.Purchaser=B.ID   ");
            sql.AppendLine(" ORDER BY " + OrderBy + "");

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        public static DataTable GetPurStatByPurchaser(string CompanyCD, string Purchaser, string StartDate, string EndDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.*,B.EmployeeName AS PurchaserName FROM ");
            sql.AppendLine("    (SELECT A.Purchaser,A.MAXNccounts,A.OwnDays,B.ProviderCount,B.YAccounts,B.NAccounts,B.RealTotal FROM ");
            sql.AppendLine("           (SELECT  A.Purchaser, MAX((A.RealTotal - ISNULL(B.YAccounts, 0)*A.Rate)) AS MAXNccounts, MAX(DATEDIFF(d, A.OrderDate, GETDATE()) - ISNULL(C.AllowDefaultDays,0)) AS OwnDays ");
            sql.AppendLine("             FROM officedba.PurchaseOrder AS A  ");
            sql.AppendLine("             INNER JOIN  officedba.ProviderInfo AS C ON A.ProviderID = C.ID  ");
            sql.AppendLine("             LEFT OUTER JOIN  officedba.BlendingDetails AS B ON A.CompanyCD = B.CompanyCD AND B.BillingType = 2 AND A.ID = B.SourceID  ");
            sql.AppendLine("             WHERE     (A.BillStatus <> '1') AND (A.RealTotal > ISNULL(B.YAccounts, 0)) AND A.CompanyCD=@CompanyCD   ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (!string.IsNullOrEmpty(StartDate))
            {
                sql.AppendLine(" AND A.OrderDate>=@StartDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" AND A.OrderDate<@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }
            if (!string.IsNullOrEmpty(Purchaser))
            {
                sql.AppendLine(" AND A.Purchaser=@Purchaser");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Purchaser", Purchaser));
            }

            sql.AppendLine("GROUP BY A.Purchaser) AS A     ");
            sql.AppendLine("INNER JOIN        ");
            sql.AppendLine("(SELECT     COUNT(DISTINCT A.ProviderID) AS ProviderCount, SUM(ISNULL(B.YAccounts, 0)*A.Rate) AS YAccounts, SUM(A.RealTotal*A.Rate) - SUM(ISNULL(B.YAccounts, 0)*A.Rate) AS NAccounts, A.Purchaser, SUM(A.RealTotal*A.Rate) AS RealTotal   ");
            sql.AppendLine("FROM         officedba.PurchaseOrder AS A LEFT OUTER JOIN     ");
            sql.AppendLine("                      officedba.BlendingDetails AS B ON A.CompanyCD = B.CompanyCD AND B.BillingType = 2 AND A.ID = B.SourceID ");
            sql.AppendLine("WHERE  (A.BillStatus <> '1') AND A.CompanyCD=@CompanyCD ");
            if (!string.IsNullOrEmpty(StartDate))
            {
                sql.AppendLine(" AND A.OrderDate>=@StartDate");
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" AND A.OrderDate<@EndDate");
            }
            if (!string.IsNullOrEmpty(Purchaser))
            {
                sql.AppendLine(" AND A.Purchaser=@Purchaser");
            }
            sql.AppendLine("GROUP BY A.Purchaser) AS B ON A.Purchaser=B.Purchaser) AS A   ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS B ON A.Purchaser=B.ID   ");


            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        //采购统计--按照物品
        public static DataTable GetPurByProduct(string CompanyCD, string ProductID, string StartDate, string EndDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();
            string sql = "SELECT ProductID,ProductNo,ProductName,SUM(ProductCount) AS ProductCount,SUM(TotalFee) AS TotalFee FROM "
                         + " (SELECT     A.ProductID,A.CompanyCD, B.OrderDate, C.ProdNo AS ProductNo, C.ProductName, A.ProductCount, A.TotalFee*B.DisCount*B.Rate AS TotalFee"
                         + " FROM  officedba.PurchaseOrderDetail AS A INNER JOIN"
                         + " officedba.PurchaseOrder AS B ON A.CompanyCD = B.CompanyCD AND A.OrderNo = B.OrderNo AND B.BillStatus<>'1' INNER JOIN"
                         + " officedba.ProductInfo AS C ON A.ProductID = C.ID ) AS ggg"
                         + " WHERE CompanyCD=@CompanyCD ";
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (!string.IsNullOrEmpty(ProductID))
            {
                sql += " AND ProductID=@ProductID";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                sql += " AND OrderDate>=@StartDate";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql += " AND OrderDate<=@EndDate";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }
            sql += " GROUP BY ProductID,ProductNo,ProductName";
            comm.CommandText = sql;
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        public static DataTable GetPurByProduct(string CompanyCD, string OrderBy, string ProductID, string StartDate, string EndDate)
        {
            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            string sql = "SELECT ProductID,ProductNo,isnull(ProductName,'') as ProductName ,Convert(decimal(22," + jingdu + "),SUM(ProductCount)) AS ProductCount,Convert(decimal(22," + jingdu + "),SUM(TotalFee)) AS TotalFee FROM "
                //+ " (SELECT     A.ProductID,A.CompanyCD, B.OrderDate, C.ProdNo AS ProductNo, C.ProductName, A.ProductCount, A.TotalFee*ISNULL(B.DisCount,100)/100*B.Rate AS TotalFee"
                         + " (SELECT     A.ProductID,A.CompanyCD, B.OrderDate, C.ProdNo AS ProductNo, C.ProductName, A.ProductCount, A.TotalFee*B.DisCount*B.Rate AS TotalFee"
                         + " FROM  officedba.PurchaseOrderDetail AS A INNER JOIN"
                         + " officedba.PurchaseOrder AS B ON A.CompanyCD = B.CompanyCD AND A.OrderNo = B.OrderNo AND B.BillStatus<>'1' INNER JOIN"
                         + " officedba.ProductInfo AS C ON A.ProductID = C.ID ) AS ggg"
                         + " WHERE CompanyCD=@CompanyCD ";
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (!string.IsNullOrEmpty(ProductID))
            {
                sql += " AND ProductID=@ProductID";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                sql += " AND OrderDate>=@StartDate";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql += " AND OrderDate<=@EndDate";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }
            sql += " GROUP BY ProductID,ProductNo,ProductName";
            sql += " ORDER BY " + OrderBy + "";
            comm.CommandText = sql;
            return SqlHelper.ExecuteSearch(comm);
        }
        //采购汇总查询
        //根据物品ID得到物品名称
        public static string GetProdName(string ID)
        {
            SqlCommand comm = new SqlCommand();
            string sql = "SELECT [ProdNo],[ProductName] FROM [officedba].[ProductInfo] WHERE ID=@ID";
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
            comm.CommandText = sql;
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            try
            {
                if (dt != null && dt.Rows.Count != 0)
                {
                    return dt.Rows[0]["ProductName"].ToString();
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static DataTable GetPurCll(string CompanyCD, string Field, string ProviderID, string ProviderName, string ProductID, string StartDate, string EndDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            #region
            //SqlCommand cmd = new SqlCommand();
            //StringBuilder sql = new StringBuilder();
            ////明早来弄
            //sql.AppendLine("SELECT [CompanyCD]                               ");
            //sql.AppendLine("      ,[OrderNo]                                 ");
            //sql.AppendLine("      ,[ProductID]                               ");
            //sql.AppendLine("      ,[ProductNo]                               ");
            //sql.AppendLine("      ,[ProductName]                             ");
            //sql.AppendLine("      ,[TaxPriceAfterDisCount]                   ");
            //sql.AppendLine("      ,[OrderDate]                               ");
            //sql.AppendLine("      ,[ProviderID]                              ");
            //sql.AppendLine("      ,[ProviderName]                            ");
            //sql.AppendLine("      ,[ID]                                      ");
            //sql.AppendLine("      ,[Purchaser]                               ");
            //sql.AppendLine("      ,[PurchaserName]                           ");
            //sql.AppendLine("  FROM [officedba].[V_PurchasePriceQuery]");

            //sql.AppendLine(" WHERE CompanyCD=@CompanyCD");
            //cmd.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //if (!string.IsNullOrEmpty(ProviderID))
            //{
            //    sql.AppendLine(" AND ProviderID=@ProviderID");
            //    cmd.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            //}
            //if (!string.IsNullOrEmpty(ProductID))
            //{
            //    sql.AppendLine(" AND ProductID=@ProductID");
            //    cmd.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            //}
            //if (!string.IsNullOrEmpty(StartDate))
            //{
            //    sql.AppendLine(" AND OrderDate>=@StartDate");
            //    cmd.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
            //}
            //if (!string.IsNullOrEmpty(EndDate))
            //{
            //    sql.AppendLine(" AND OrderDate<=@EndDate");
            //    cmd.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            //}
            //cmd.CommandText = sql.ToString();
            //return SqlHelper.PagerWithCommand(cmd, pageIndex, pageCount, OrderBy, ref totalCount);
            #endregion

            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            sql.AppendLine("SELECT     TOP (100) PERCENT A.ProviderID, B.ProductID,  Convert(numeric(22," + userInfo.SelPoint + "),SUM(ISNULL(B.ProductCount, 0))) as ProductCount       , C.CustNo AS ProviderNo, ");
            sql.AppendLine("                      C.CustName AS ProviderName, D.ProdNo AS ProductNo, D.ProductName                                          ");
            sql.AppendLine(", CONVERT(NUMERIC(20," + userInfo.SelPoint + "),SUM(ISNULL(B.TotalFee, 0)*ISNULL(A.DisCount,100)/100*isnull(A.Rate,0))) AS TotalFee, CONVERT(NUMERIC(20," + userInfo.SelPoint + "),SUM(ISNULL(B.TotalPrice, 0)*ISNULL(A.DisCount,100)/100*isnull(A.Rate,0)))AS TotalPrice  ");
            sql.AppendLine(", CONVERT(NUMERIC(20," + userInfo.SelPoint + "),SUM(ISNULL(B.TotalFee, 0)*ISNULL(A.DisCount,100)/100*isnull(A.Rate,0))/SUM(ISNULL(B.ProductCount, 0))) AS TaxPrice              ");
            sql.AppendLine(", CONVERT(NUMERIC(20," + userInfo.SelPoint + "),SUM(ISNULL(B.TotalPrice, 0)*ISNULL(A.DisCount,100)/100*isnull(A.Rate,0))/ SUM(ISNULL(B.ProductCount, 0))) AS UnitPrice          ");
            sql.AppendLine("FROM         officedba.PurchaseOrder AS A INNER JOIN                                                                            ");
            sql.AppendLine("                      officedba.PurchaseOrderDetail AS B ON A.CompanyCD = B.CompanyCD AND A.OrderNo = B.OrderNo INNER JOIN      ");
            sql.AppendLine("                      officedba.ProviderInfo AS C ON A.ProviderID = C.ID INNER JOIN                                             ");
            sql.AppendLine("                      officedba.ProductInfo AS D ON B.ProductID = D.ID                                                          ");
            sql.AppendLine("WHERE     (A.BillStatus <> '1') AND (A.CompanyCD = @CompanyCD) AND A.OrderDate>=@StartDate AND A.OrderDate<=@EndDate        ");
            sql.AppendLine("GROUP BY A.ProviderID, B.ProductID, C.CustNo, C.CustName, D.ProdNo, D.ProductName                                               ");
            sql.AppendLine("ORDER BY B.ProductID, A.ProviderID                                                                                              ");



            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            DataTable dt = SqlHelper.ExecuteSearch(comm);



            string[] ProvID = ProviderID.Split(',');
            string[] ProvName = ProviderName.Split(',');
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Product");//增加表头，即表的第一列
            foreach (string PName in ProvName)
            {//供应商为列
                dt2.Columns.Add("" + PName + "");
            }

            if (!string.IsNullOrEmpty(ProductID))
            {//物品不为空时，按照选择的物品编号的顺序为行的顺序,此时dt2的行数也是固定的

                string[] ProdID = ProductID.Split(',');
                foreach (string Pdid in ProdID)
                {//循环增加每行
                    DataRow dr = dt2.NewRow();//新增行，为某一物品对应的不同供应商的采购数量，供应商按照应该是按照预定的顺序
                    int i = 0;
                    try
                    {
                        string ooo = dt.Select("ProductID=" + Pdid)[0]["ProductName"].ToString();//第一列为物品名称
                        dr[i++] = ooo;
                    }
                    catch
                    {
                        dr[i++] = GetProdName(Pdid);
                    }

                    foreach (string Pvid in ProvID)
                    {//找出该物品对应的供应商的采购数量，然后赋值给dr的某项，然后删除dt的这行
                        try
                        {
                            string ddd = dt.Select("ProviderID=" + Pvid + " and ProductID=" + Pdid)[0]["" + Field + ""].ToString();
                            dr[i++] = ddd;
                        }
                        catch
                        {
                            dr[i++] = "0";
                        }
                        //找出dt中ProviderID为Pvid，ProductID为Pdid时ProductCount的值
                        //dt.RowDeleted(

                    }
                    dt2.Rows.Add(dr);
                }
            }
            else
            {//物品为空的时候，找出所有的物品，此时dt2的行数不固定
                SqlCommand comm1 = new SqlCommand();
                StringBuilder sql1 = new StringBuilder();
                sql1.AppendLine("SELECT DISTINCT  B.ProductID , D.ProdNo AS ProductNo, D.ProductName");
                sql1.AppendLine("FROM         officedba.PurchaseOrder AS A INNER JOIN                                                                            ");
                sql1.AppendLine("                      officedba.PurchaseOrderDetail AS B ON A.CompanyCD = B.CompanyCD AND A.OrderNo = B.OrderNo INNER JOIN      ");
                sql1.AppendLine("                      officedba.ProductInfo AS D ON B.ProductID = D.ID                                                          ");
                sql1.AppendLine("WHERE     (A.BillStatus <> '1') AND (A.CompanyCD = @CompanyCD) AND A.OrderDate>=@StartDate AND A.OrderDate<=@EndDate        ");
                comm1.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                comm1.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
                comm1.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
                comm1.CommandText = sql1.ToString();
                DataTable dt1 = SqlHelper.ExecuteSearch(comm1);

                foreach (DataRow dr in dt1.Rows)
                {
                    DataRow dr2 = dt2.NewRow();
                    int i = 0;
                    dr2[i++] = dr["ProductName"].ToString();
                    foreach (string Pvid in ProvID)
                    {//找出该物品对应的供应商的采购数量，然后赋值给dr的某项，然后删除dt的这行
                        try
                        {
                            string ddd = dt.Select("ProviderID=" + Pvid + " and ProductID=" + dr["ProductID"].ToString())[0]["" + Field + ""].ToString();
                            dr2[i++] = ddd;
                        }
                        catch
                        {
                            dr2[i++] = "0";
                        }
                        //找出dt中ProviderID为Pvid，ProductID为Pdid时ProductCount的值
                        //dt.RowDeleted(

                    }
                    dt2.Rows.Add(dr2);
                }
            }
            return dt2;
        }

        public static DataTable GetPurCll(string CompanyCD, string ProviderID, string ProductID, string StartDate, string EndDate)
        {


            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT     TOP (100) PERCENT A.ProviderID, B.ProductID, SUM(ISNULL(B.ProductCount, 0)) AS ProductCount, C.CustNo AS ProviderNo, ");
            sql.AppendLine("                      C.CustName AS ProviderName, D.ProdNo AS ProductNo, D.ProductName                                          ");
            sql.AppendLine(", SUM(ISNULL(B.TotalFee, 0)*ISNULL(A.DisCount,100)/100*isnull(A.Rate,0)) AS TotalFee, SUM(ISNULL(B.TotalPrice, 0)*ISNULL(A.DisCount,100)/100*isnull(A.Rate,0))AS TotalPrice  ");
            sql.AppendLine(", SUM(ISNULL(B.TotalFee, 0)*ISNULL(A.DisCount,100)/100*isnull(A.Rate,0))/SUM(ISNULL(B.ProductCount, 0)) AS TaxPrice              ");
            sql.AppendLine(", SUM(ISNULL(B.TotalPrice, 0)*ISNULL(A.DisCount,100)/100*isnull(A.Rate,0))/ SUM(ISNULL(B.ProductCount, 0)) AS UnitPrice          ");
            sql.AppendLine("FROM         officedba.PurchaseOrder AS A INNER JOIN                                                                            ");
            sql.AppendLine("                      officedba.PurchaseOrderDetail AS B ON A.CompanyCD = B.CompanyCD AND A.OrderNo = B.OrderNo INNER JOIN      ");
            sql.AppendLine("                      officedba.ProviderInfo AS C ON A.ProviderID = C.ID INNER JOIN                                             ");
            sql.AppendLine("                      officedba.ProductInfo AS D ON B.ProductID = D.ID                                                          ");
            sql.AppendLine("WHERE     (A.BillStatus <> '1') AND (A.CompanyCD = @CompanyCD) AND A.OrderDate>=@StartDate AND A.OrderDate<=@EndDate        ");
            if (!string.IsNullOrEmpty(ProviderID))
            {
                sql.AppendLine(" AND A.ProviderID IN (" + ProviderID + ")");
            }
            if (!string.IsNullOrEmpty(ProductID))
            {
                sql.AppendLine(" AND B.ProductID IN (" + ProductID + ")");
            }
            sql.AppendLine("GROUP BY A.ProviderID, B.ProductID, C.CustNo, C.CustName, D.ProdNo, D.ProductName                                               ");
            sql.AppendLine("ORDER BY B.ProductID, A.ProviderID                                                                                              ");



            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            return SqlHelper.ExecuteSearch(comm);
        }

        public static DataTable ProdOnRoadQry(string CompanyCD, string ProductID, string ProviderID, string StartDate, string EndDate, string OrderBy)
        {
            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            try
            {
                SqlCommand comm = new SqlCommand();
                StringBuilder sql = new StringBuilder(); //ROW_NUMBER() OVER (order by '"+OrderBy+"' ) AS Pos,
                sql.AppendLine(" SELECT [OrderNo],[OrderDate],[ProductNo],[ProviderNo],");
                sql.AppendLine("isnull(ProviderName,'') as ProviderName,");
                sql.AppendLine("isnull(UnitName,'') as UnitName,");
                sql.AppendLine("isnull(Specification,'') as Specification,");
                sql.AppendLine("isnull(ProductName,'') as ProductName,");

                sql.AppendLine("Convert(decimal(22," + jingdu + "),isnull(ProductCount,0)) as ProductCount, ");
                sql.AppendLine("Convert(decimal(22," + jingdu + "),isnull(ArrivedCount,0)) as ArrivedCount, ");
                sql.AppendLine("Convert(decimal(22," + jingdu + "),isnull(RoadCount,0)) as RoadCount, ");
                sql.AppendLine("Convert(decimal(22," + jingdu + "),isnull(TaxPrice,0)) as TaxPrice, ");
                sql.AppendLine("Convert(decimal(22," + jingdu + "),isnull(UnitPrice,0)) as UnitPrice, ");
                sql.AppendLine("Convert(decimal(22," + jingdu + "),isnull(TotalFee,0)) as TotalFee, ");

                sql.AppendLine("Convert(decimal(22," + jingdu + "),isnull(TotalFeeOnRoad,0)) as TotalFeeOnRoad, ");
                sql.AppendLine("Convert(decimal(22," + jingdu + "),isnull(TotalPrice,0)) as TotalPrice, ");
                sql.AppendLine("Convert(decimal(22," + jingdu + "),isnull(TotalPriceOnRoad,0)) as TotalPriceOnRoad   ");

                sql.AppendLine(" FROM [officedba].[V_PurRoadQry]");
                sql.AppendLine(" WHERE CompanyCD=@CompanyCD ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                if (!string.IsNullOrEmpty(ProductID))
                {
                    sql.AppendLine(" AND ProductID in (@ProductID)");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
                }
                if (!string.IsNullOrEmpty(ProviderID))
                {
                    sql.AppendLine(" AND ProviderID IN (@ProviderID)");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
                }
                if (!string.IsNullOrEmpty(StartDate))
                {
                    sql.AppendLine(" AND OrderDate>@StartDate");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
                }
                if (!string.IsNullOrEmpty(EndDate))
                {
                    sql.AppendLine(" AND OrderDate<@EndDate");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
                }
                if (!string.IsNullOrEmpty(OrderBy))
                {
                    sql.AppendLine(" ORDER BY " + OrderBy + " ");
                    //sql.AppendLine(" ORDER BY @OrderBy ");
                    //comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderBy", OrderBy));
                }
                comm.CommandText = sql.ToString();
                return SqlHelper.ExecuteSearch(comm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 新报表

        /// <summary>
        /// 采购订单数量部门分布
        /// </summary>
        public static DataTable GetOrderByTypeNum(string OrderStatus, string FlowStatus, string FromType, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT min(E.TypeName) as Name,  ");
            sb.Append(" count(1) as Counts, A.TypeID   ");
            sb.Append(" FROM officedba.PurchaseOrder AS A ");
            sb.Append(" LEFT JOIN officedba.FlowInstance AS D ON A.ID = D.BillID AND D.BillTypeFlag = 6 AND D.BillTypeCode = 4  ");
            sb.Append(" AND D.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = 6 AND F.BillTypeCode = 4 ) ");
            sb.Append(" inner JOIN officedba.CodePublicType AS E ON A.TypeID=E.ID   WHERE 1=1 ");
            sb.Append("  AND A.CompanyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");
            if (OrderStatus != "")
            {
                sb.Append("and A.BillStatus=");
                sb.Append(OrderStatus);
            }

            if (FlowStatus != "0")
            {
                if (FlowStatus == "")
                {
                    sb.AppendLine(" AND D.FlowStatus is null");
                }
                else
                {
                    sb.AppendLine(" AND D.FlowStatus =");
                    sb.Append(FlowStatus);
                }
            }
            if (FromType != "")
            {
                sb.Append("and A.FromType=");
                sb.Append(FromType);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }

            sb.Append(" group by A.TypeID ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 采购订单数量部门分布
        /// </summary>
        public static DataTable GetOrderByDeptNum(string OrderStatus, string FlowStatus, string FromType, string BeginDate, string EndDate)
        {
            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;

            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT min(E.DeptName) as Name,  ");
            sb.Append(" Convert(decimal(12," + jingdu + "),count(1)) as Counts, A.DeptID   ");
            sb.Append(" FROM officedba.PurchaseOrder AS A ");
            sb.Append(" LEFT JOIN officedba.FlowInstance AS D ON A.ID = D.BillID AND D.BillTypeFlag = 6 AND D.BillTypeCode = 4  ");
            sb.Append(" AND D.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = 6 AND F.BillTypeCode = 4 ) ");
            sb.Append(" inner JOIN officedba.DeptInfo AS E ON A.DeptID=E.ID   WHERE 1=1 ");
            sb.Append("  AND A.CompanyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");
            if (OrderStatus != "")
            {
                sb.Append("and A.BillStatus=");
                sb.Append(OrderStatus);
            }
            if (FlowStatus != "0")
            {
                if (FlowStatus == "")
                {
                    sb.AppendLine(" AND D.FlowStatus is null");
                }
                else
                {
                    sb.AppendLine(" AND D.FlowStatus =");
                    sb.Append(FlowStatus);
                }
            }
            if (FromType != "")
            {
                sb.Append("and A.FromType=");
                sb.Append(FromType);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }

            sb.Append(" group by A.DeptID ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }


        /// <summary>
        /// 采购数量供应商分布
        /// </summary>
        public static DataTable GetOrderByProviderNum(string OrderStatus, string FlowStatus, string FromType, string BeginDate, string EndDate)
        {

            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT min(E.CustName) as Name,  ");
            sb.Append(" count(1) as Counts, A.providerID   ");
            sb.Append(" FROM officedba.PurchaseOrder AS A ");
            sb.Append(" LEFT JOIN officedba.FlowInstance AS D ON A.ID = D.BillID AND D.BillTypeFlag = 6 AND D.BillTypeCode = 4  ");
            sb.Append(" AND D.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = 6 AND F.BillTypeCode = 4 ) ");
            sb.Append(" inner JOIN officedba.providerinfo AS E ON A.providerID=E.ID  WHERE 1=1 ");
            sb.Append("  AND A.CompanyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");
            if (OrderStatus != "")
            {
                sb.Append("and A.BillStatus=");
                sb.Append(OrderStatus);
            }
            if (FlowStatus != "0")
            {
                if (FlowStatus == "")
                {
                    sb.AppendLine(" AND D.FlowStatus is null");
                }
                else
                {
                    sb.AppendLine(" AND D.FlowStatus=");
                    sb.Append(FlowStatus);
                }
            }
            if (FromType != "")
            {
                sb.Append("and A.FromType=");
                sb.Append(FromType);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }

            sb.Append(" group by A.providerID ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 采购数量走势
        /// </summary>
        public static DataTable GetOrderByTrendNum(string OrderStatus, string FlowStatus, string FromType, string Type, string DateType, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string SearchField = "";
            if (DateType == "1")
            {
                SearchField = "dateName(year,a.OrderDate)+'年'";
            }
            else if (DateType == "2")
            {
                SearchField = "dateName(year,a.OrderDate)+'年-'+dateName(month,a.OrderDate)+'月'";
            }
            else
            {
                SearchField = "dateName(year,a.OrderDate)+'年-'+dateName(week,a.OrderDate)+'周'";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT  ");
            sb.Append(SearchField);
            sb.Append(" as Name,count(1) as Counts  ");
            sb.Append(" FROM officedba.PurchaseOrder AS A ");
            sb.Append(" LEFT JOIN officedba.FlowInstance AS D ON A.ID = D.BillID AND D.BillTypeFlag = 6 AND D.BillTypeCode = 4  ");
            sb.Append(" AND D.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = 6 AND F.BillTypeCode = 4 ) ");
            sb.Append(" inner JOIN officedba.providerinfo AS E ON A.providerID=E.ID  WHERE 1=1 ");
            sb.Append("  AND A.CompanyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");
            if (OrderStatus != "")
            {
                sb.Append("and A.BillStatus=");
                sb.Append(OrderStatus);
            }
            if (FlowStatus != "0")
            {
                if (FlowStatus == "")
                {
                    sb.AppendLine(" AND D.FlowStatus is null");
                }
                else
                {
                    sb.AppendLine(" AND D.FlowStatus =");
                    sb.Append(FlowStatus);
                }
            }
            if (FromType != "")
            {
                sb.Append("and A.FromType=");
                sb.Append(FromType);
            }
            if (Type != "")
            {
                sb.Append(" and  A.TypeID=");
                sb.Append(Type);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by  ");
            sb.Append(SearchField);
            return SqlHelper.ExecuteSql(sb.ToString());
        }


        /// <summary>
        /// 采购订单金额部门分布
        /// </summary>
        public static DataTable GetOrderByTypePrice(string OrderStatus, string FlowStatus, string FromType, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT min(E.TypeName) as Name,  ");
            sb.Append(" sum(isnull(realtotal,0)*isnull(rate,1)) as Counts, A.TypeID   ");
            sb.Append(" FROM officedba.PurchaseOrder AS A ");
            sb.Append(" LEFT JOIN officedba.FlowInstance AS D ON A.ID = D.BillID AND D.BillTypeFlag = 6 AND D.BillTypeCode = 4  ");
            sb.Append(" AND D.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = 6 AND F.BillTypeCode = 4 ) ");
            sb.Append(" inner JOIN officedba.CodePublicType AS E ON A.TypeID=E.ID   WHERE 1=1 ");
            sb.Append("  AND A.CompanyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");
            if (OrderStatus != "")
            {
                sb.Append("and A.BillStatus=");
                sb.Append(OrderStatus);
            }

            if (FlowStatus != "0")
            {
                if (FlowStatus == "")
                {
                    sb.AppendLine(" AND D.FlowStatus is null");
                }
                else
                {
                    sb.AppendLine(" AND D.FlowStatus =");
                    sb.Append(FlowStatus);
                }
            }
            if (FromType != "")
            {
                sb.Append("and A.FromType=");
                sb.Append(FromType);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }

            sb.Append(" group by A.TypeID ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 采购订单金额部门分布
        /// </summary>
        public static DataTable GetOrderByDeptPrice(string OrderStatus, string FlowStatus, string FromType, string BeginDate, string EndDate)
        {

            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT min(E.DeptName) as Name,  ");
            sb.Append(" sum(isnull(realtotal,0)*isnull(rate,1)) as Counts, A.DeptID   ");
            sb.Append(" FROM officedba.PurchaseOrder AS A ");
            sb.Append(" LEFT JOIN officedba.FlowInstance AS D ON A.ID = D.BillID AND D.BillTypeFlag = 6 AND D.BillTypeCode = 4  ");
            sb.Append(" AND D.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = 6 AND F.BillTypeCode = 4 ) ");
            sb.Append(" inner JOIN officedba.DeptInfo AS E ON A.DeptID=E.ID   WHERE 1=1 ");
            sb.Append("  AND A.CompanyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");
            if (OrderStatus != "")
            {
                sb.Append("and A.BillStatus=");
                sb.Append(OrderStatus);
            }
            if (FlowStatus != "0")
            {
                if (FlowStatus == "")
                {
                    sb.AppendLine(" AND D.FlowStatus is null");
                }
                else
                {
                    sb.AppendLine(" AND D.FlowStatus =");
                    sb.Append(FlowStatus);
                }
            }
            if (FromType != "")
            {
                sb.Append("and A.FromType=");
                sb.Append(FromType);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }

            sb.Append(" group by A.DeptID ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }


        /// <summary>
        /// 采购金额供应商分布
        /// </summary>
        public static DataTable GetOrderByProviderPrice(string OrderStatus, string FlowStatus, string FromType, string BeginDate, string EndDate)
        {

            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT min(E.CustName) as Name,  ");
            sb.Append(" sum(isnull(realtotal,0)*isnull(rate,1)) as Counts, A.providerID   ");
            sb.Append(" FROM officedba.PurchaseOrder AS A ");
            sb.Append(" LEFT JOIN officedba.FlowInstance AS D ON A.ID = D.BillID AND D.BillTypeFlag = 6 AND D.BillTypeCode = 4  ");
            sb.Append(" AND D.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = 6 AND F.BillTypeCode = 4 ) ");
            sb.Append(" inner JOIN officedba.providerinfo AS E ON A.providerID=E.ID  WHERE 1=1 ");
            sb.Append("  AND A.CompanyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");
            if (OrderStatus != "")
            {
                sb.Append("and A.BillStatus=");
                sb.Append(OrderStatus);
            }
            if (FlowStatus != "0")
            {
                if (FlowStatus == "")
                {
                    sb.AppendLine(" AND D.FlowStatus is null");
                }
                else
                {
                    sb.AppendLine(" AND D.FlowStatus=");
                    sb.Append(FlowStatus);
                }
            }
            if (FromType != "")
            {
                sb.Append("and A.FromType=");
                sb.Append(FromType);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }

            sb.Append(" group by A.providerID ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 采购金额走势
        /// </summary>
        public static DataTable GetOrderByTrendPrice(string OrderStatus, string FlowStatus, string FromType, string Type, string DateType, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string SearchField = "";
            if (DateType == "1")
            {
                SearchField = "dateName(year,a.OrderDate)+'年'";
            }
            else if (DateType == "2")
            {
                SearchField = "dateName(year,a.OrderDate)+'年-'+dateName(month,a.OrderDate)+'月'";
            }
            else
            {
                SearchField = "dateName(year,a.OrderDate)+'年-'+dateName(week,a.OrderDate)+'周'";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT  ");
            sb.Append(SearchField);
            sb.Append(" as Name,sum(isnull(realtotal,0)*isnull(rate,1)) as Counts  ");
            sb.Append(" FROM officedba.PurchaseOrder AS A ");
            sb.Append(" LEFT JOIN officedba.FlowInstance AS D ON A.ID = D.BillID AND D.BillTypeFlag = 6 AND D.BillTypeCode = 4  ");
            sb.Append(" AND D.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = 6 AND F.BillTypeCode = 4 ) ");
            sb.Append(" inner JOIN officedba.providerinfo AS E ON A.providerID=E.ID  WHERE 1=1 ");
            sb.Append("  AND A.CompanyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");
            if (OrderStatus != "")
            {
                sb.Append("and A.BillStatus=");
                sb.Append(OrderStatus);
            }
            if (FlowStatus != "0")
            {
                if (FlowStatus == "")
                {
                    sb.AppendLine(" AND D.FlowStatus is null");
                }
                else
                {
                    sb.AppendLine(" AND D.FlowStatus =");
                    sb.Append(FlowStatus);
                }
            }
            if (FromType != "")
            {
                sb.Append("and A.FromType=");
                sb.Append(FromType);
            }
            if (Type != "")
            {
                sb.Append(" and  A.TypeID=");
                sb.Append(Type);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by  ");
            sb.Append(SearchField);
            return SqlHelper.ExecuteSql(sb.ToString());
        }


        public static DataTable GetPurchaseOrderDetail(string OrderStatus, string FlowStatus, string FromType, string Type, string DateType, string BeginDate, string EndDate, string DeptId, string ProviderId, string DateValue, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;

            SqlCommand comm = new SqlCommand();
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT     A.ID ,A.DeptID, A.FromType ,");
            sql.AppendLine("dateName(year,a.OrderDate)+'年' as OrderYear,");
            sql.AppendLine("dateName(year,a.OrderDate)+'年-'+dateName(month,a.OrderDate)+'月' as OrderMonth,");
            sql.AppendLine("dateName(year,a.OrderDate)+'年-'+dateName(week,a.OrderDate)+'周' as OrderWeek ");
            sql.AppendLine(" , A.CompanyCD     ");
            sql.AppendLine(" , A.OrderNo       ");
            sql.AppendLine(" , A.Title AS OrderTitle        ");
            sql.AppendLine(" , A.TypeID        ");
            sql.AppendLine(" , E.TypeName ");
            sql.AppendLine(" , A.Purchaser as  PurchaserID   ");
            sql.AppendLine(" , B.EmployeeName  AS PurchaserName ");
            sql.AppendLine(" , A.ProviderID    ");
            sql.AppendLine(" , C.CustName   AS ProviderName     ");
            sql.AppendLine(" , Convert(decimal(22," + jingdu + "), isnull(A.TotalPrice,0)*isnull(a.Rate,1)) as  TotalPrice");
            sql.AppendLine(" , Convert(decimal(22," + jingdu + "), isnull(A.TotalTax,0)*isnull(a.Rate,1)) as   TotalTax    ");
            sql.AppendLine(" ,Convert(decimal(22," + jingdu + "),  isnull(A.TotalFee,0)*isnull(a.Rate,1)) as   TotalFee    ");
            sql.AppendLine(" ,case A.isOpenBill when 0 then '否' else '是' end isOpenBill");
            sql.AppendLine(" , A.BillStatus    ");
            sql.AppendLine(" ,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'                         ");
            sql.AppendLine(" when '4' then '手工结单' when '5' then '自动结单' end AS BillStatusName                                   ");
            sql.AppendLine(" , D.FlowStatus                                                                                            ");
            sql.AppendLine(" ,case D.FlowStatus when '1' then '待审批' when '2' then '审批中' when '3'                                 ");
            sql.AppendLine(" then '审批通过' when '4' then '审批不通过' when '5' then '撤销审批' else '' end as FlowStatusName ,isnull( A.ModifiedDate,'') AS ModifiedDate  ");
            sql.AppendLine(" FROM officedba.PurchaseOrder AS A                                                                         ");
            sql.AppendLine(" INNER JOIN officedba.EmployeeInfo AS B ON A.Purchaser = B.ID                                              ");
            sql.AppendLine(" INNER JOIN officedba.ProviderInfo AS C ON A.ProviderID = C.ID                                             ");
            sql.AppendLine(" LEFT JOIN officedba.FlowInstance AS D ON A.ID = D.BillID AND D.BillTypeFlag = 6 AND D.BillTypeCode = 4    ");
            sql.AppendLine(" AND D.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = 6 AND F.BillTypeCode = 4 )");
            sql.AppendLine(" LEFT JOIN officedba.CodePublicType AS E ON A.TypeID=E.ID ");
            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine(" AND A.CompanyCD=@CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", strCompanyCD));


            if (BeginDate != "")
            {
                sql.Append("and a.OrderDate>=Convert(datetime,'");
                sql.Append(BeginDate);
                sql.Append("')");

            }
            if (EndDate != "")
            {
                sql.Append("and a.OrderDate< DATEADD(day,1,Convert(datetime,'");
                sql.Append(EndDate);
                sql.Append("'))");
            }

            if (Type != "")
            {
                sql.AppendLine(" AND A.TypeID = @TypeID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", Type));
            }
            if (DeptId != "")
            {
                sql.AppendLine(" AND A.DeptID = @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptId));
            }

            if (FromType != "")
            {
                sql.AppendLine(" AND A.FromType = @FromType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", FromType));
            }
            if (ProviderId != "")
            {
                sql.AppendLine(" AND A.ProviderID = @ProviderID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderId));
            }
            if (OrderStatus != "")
            {
                sql.AppendLine(" AND A.BillStatus = @BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", OrderStatus));
            }

            if (FlowStatus != "0")
            {
                if (FlowStatus == "")
                {
                    sql.AppendLine(" AND D.FlowStatus is null");
                }
                else
                {
                    sql.AppendLine(" AND D.FlowStatus =");
                    sql.Append(FlowStatus);
                }
            }
            if (DateValue != "")
            {
                if (DateType == "1")
                {
                    sql.Append("and (dateName(year,a.OrderDate)+'年')='");
                    sql.Append(DateValue);
                    sql.Append("'");
                }
                else if (DateType == "2")
                {
                    sql.Append("and (dateName(year,a.OrderDate)+'年-'+dateName(month,a.OrderDate)+'月')='");
                    sql.Append(DateValue);
                    sql.Append("'");
                }
                else
                {
                    sql.Append("and (dateName(year,a.OrderDate)+'年-'+dateName(week,a.OrderDate)+'周')='");
                    sql.Append(DateValue);
                    sql.Append("'");
                }
            }
            #endregion

            comm.CommandText = sql.ToString();

            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }





        /// <summary>
        /// 采购物品分布
        /// </summary>
        public static DataTable GetOrderByProduct(string ProviderID, string DeptID, string BeginDate, string EndDate, string StatType)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(c.productname) as Name,");
            if (StatType == "Num")
            {
                sb.Append(" sum(isnull(productCount,0)) as counts ");
            }
            else
            {
                sb.Append(" sum(isnull(b.TotalFee,0)*isnull(a.Rate,1)) as counts ");
            }
            sb.Append(",b.productId  ");
            sb.Append(" from officedba.purchaseorder as a left join officedba.purchaseorderdetail as b  ");
            sb.Append(" on a.orderNo=b.orderNo and a.companyCD=b.companyCD left join officedba.productinfo as c on b.productId=c.Id ");
            sb.Append(" WHERE 1=1 ");
            sb.Append("  AND A.CompanyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (ProviderID != "")
            {
                sb.Append("and A.providerid=");
                sb.Append(ProviderID);
            }
            if (DeptID != "")
            {
                sb.Append(" and  A.deptId=");
                sb.Append(DeptID);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by b.productId  ");

            return SqlHelper.ExecuteSql(sb.ToString());
        }


        /// <summary>
        /// 采购物品走势
        /// </summary>
        public static DataTable GetOrderByProductTrend(string ProviderID, string DeptID, string BeginDate, string EndDate, string DateType, string StatType, string ProductID)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string SearchField = "";
            if (DateType == "1")
            {
                SearchField = "dateName(year,a.OrderDate)+'年'";
            }
            else if (DateType == "2")
            {
                SearchField = "dateName(year,a.OrderDate)+'年-'+dateName(month,a.OrderDate)+'月'";
            }
            else
            {
                SearchField = "dateName(year,a.OrderDate)+'年-'+dateName(week,a.OrderDate)+'周'";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT  ");
            sb.Append(SearchField);
            sb.Append(" as Name,");

            if (StatType == "Num")
            {
                sb.Append(" sum(isnull(productCount,0)) as counts ");
            }
            else
            {
                sb.Append(" sum(isnull(b.TotalFee,0)*isnull(a.Rate,1)) as counts ");
            }
            sb.Append(" from officedba.purchaseorder as a left join officedba.purchaseorderdetail as b ");
            sb.Append(" on a.orderNo=b.orderNo and a.companyCD=b.companyCD left join officedba.productinfo as c on b.productId=c.Id  ");
            sb.Append(" WHERE 1=1 ");
            sb.Append("  AND A.CompanyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");
            if (ProviderID != "")
            {
                sb.Append("and A.providerid=");
                sb.Append(ProviderID);
            }
            if (DeptID != "")
            {
                sb.Append(" and  A.deptId=");
                sb.Append(DeptID);
            }
            if (ProductID != "")
            {
                sb.Append(" and  b.productId=");
                sb.Append(ProductID);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by   ");
            sb.Append(SearchField);
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 采购物品分析
        /// </summary>
        public static DataTable GetPurchaseOrderProductDetail(string ProviderID, string DeptID, string BeginDate, string EndDate, string DateType, string DateValue, string ProductID, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            SqlCommand comm = new SqlCommand();
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select ");
            sql.AppendLine(" b.OrderNO,a.ProductID, ");
            sql.AppendLine("dateName(year,b.OrderDate)+'年' as OrderYear,");
            sql.AppendLine("dateName(year,b.OrderDate)+'年-'+dateName(month,b.OrderDate)+'月' as OrderMonth,");
            sql.AppendLine("dateName(year,b.OrderDate)+'年-'+dateName(week,b.OrderDate)+'周' as OrderWeek,");
            sql.AppendLine(" a.ProductNo,    ");
            sql.AppendLine(" a.ProductName,      ");
            sql.AppendLine(" a.ProductCount,       ");
            sql.AppendLine(" isnull(a.UnitPrice,0)*isnull(b.Rate,1) UnitPrice,       ");
            sql.AppendLine(" isnull(a.TotalPrice,0)*isnull(b.Rate,1) TotalPrice, ");
            sql.AppendLine(" a.TaxRate,   ");
            sql.AppendLine(" case b.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'                         ");
            sql.AppendLine(" when '4' then '手工结单' when '5' then '自动结单' end AS BillStatusName                                   ");
            sql.AppendLine("  from officedba.purchaseOrderdetail as a left join officedba.purchaseorder as b ");
            sql.AppendLine("  on a.OrderNo=b.OrderNo and a.companyCD=b.CompanyCD ");
            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine(" AND A.CompanyCD=@CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", strCompanyCD));

            if (DeptID != "")
            {
                sql.AppendLine(" AND b.DeptID = @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }

            if (ProviderID != "")
            {
                sql.AppendLine(" AND b.ProviderID = @ProviderID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            }
            if (ProductID != "")
            {
                sql.Append("and a.ProductID=");
                sql.Append(ProductID);
            }

            if (BeginDate != "")
            {
                sql.Append("and b.OrderDate>=Convert(datetime,'");
                sql.Append(BeginDate);
                sql.Append("')");

            }
            if (EndDate != "")
            {
                sql.Append("and b.OrderDate< DATEADD(day,1,Convert(datetime,'");
                sql.Append(EndDate);
                sql.Append("'))");
            }


            if (DateValue != "")
            {
                if (DateType == "1")
                {
                    sql.Append("and (dateName(year,b.OrderDate)+'年')='");
                    sql.Append(DateValue);
                    sql.Append("'");
                }
                else if (DateType == "2")
                {
                    sql.Append("and (dateName(year,b.OrderDate)+'年-'+dateName(month,b.OrderDate)+'月')='");
                    sql.Append(DateValue);
                    sql.Append("'");
                }
                else
                {
                    sql.Append("and (dateName(year,b.OrderDate)+'年-'+dateName(week,b.OrderDate)+'周')='");
                    sql.Append(DateValue);
                    sql.Append("'");
                }
            }
            #endregion

            comm.CommandText = sql.ToString();

            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }

        #endregion

        #region 在途报表
        /// <summary>
        /// 在途物品查询
        /// </summary>
        /// <param name="CompanyCD">公司CD</param>
        /// <param name="ProductID">产品ID</param>
        /// <param name="ProviderID">供应商ID</param>
        /// <param name="StartDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <param name="OrderNo">订单编号</param>
        /// <param name="pageIndex">查询页</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="OrderBy">排序</param>
        /// <param name="TotalCount">总数</param>
        /// <returns></returns>
        public static DataTable ProdOnRoadQry(string CompanyCD, string ProductID, string ProviderID
            , string StartDate, string EndDate, string OrderNo, int pageIndex
            , int pageSize, string OrderBy, ref int TotalCount)
        {
            try
            {
                SqlParameter[] parms = new SqlParameter[10];
                int i = 0;
                parms[i++] = new SqlParameter("@CompanyCD", CompanyCD);
                parms[i++] = new SqlParameter("@ProductID", ProductID);
                parms[i++] = new SqlParameter("@ProviderID", ProviderID);
                parms[i++] = new SqlParameter("@StartDate", StartDate);
                parms[i++] = new SqlParameter("@EndDate", EndDate);
                parms[i++] = new SqlParameter("@OrderNo", OrderNo);
                parms[i++] = new SqlParameter("@pageIndex", pageIndex);
                parms[i++] = new SqlParameter("@pageSize", pageSize);
                parms[i++] = new SqlParameter("@OrderBy", OrderBy);
                parms[i] = MakeParam("@TotalCount", SqlDbType.BigInt, 0, ParameterDirection.Output, null);
                DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[ProGetPurProdRoad]", parms);
                TotalCount = int.Parse(parms[i].Value.ToString());
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获得采购订单编号
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页记录数</param>
        /// <param name="orderBy">排序方法</param>
        /// <param name="TotalCount">总记录数</param>
        /// <param name="userInfo">用户信息实体类</param>
        /// <returns></returns>
        public static DataTable SelectPurchaseOrder(int pageIndex, int pageCount, string orderBy, ref int TotalCount
            , UserInfoUtil userInfo)
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = @"SELECT DISTINCT po.ID
                                      ,po.OrderNo
                                      ,po.Title
                                FROM   officedba.PurchaseOrder po
                                       INNER JOIN officedba.PurchaseOrderDetail pod
                                            ON  po.CompanyCD = pod.CompanyCD
                                                AND po.OrderNo = pod.OrderNo
                                                AND pod.ProductCount>pod.ArrivedCount
                                WHERE po.CompanyCD=@CompanyCD AND po.BillStatus<>'1'";
            comm.Parameters.Add(new SqlParameter("@CompanyCD", userInfo.CompanyCD));
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion
    }
}

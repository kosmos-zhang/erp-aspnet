using System;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using XBase.Common;
using System.Collections;
using System.Collections.Generic;
using XBase.Model.Office.SellManager;
namespace XBase.Data.Office.HumanManager
{
  public class InputPersonalRoyaltyDBHelper
    {
      public static DataTable SearchPersonTaxInfo(string CompanyCD, string OrderNo, string EmpName, string StartDate, string EndDate, int pageIndex, int pageCount, string ord, ref int totalCount)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                  ");
            searchSql.AppendLine(" 	 a.ID                                  ");
            searchSql.AppendLine(" 	,a.CompanyCD                           ");
            searchSql.AppendLine(" 	,a.Seller                          ");
            searchSql.AppendLine(" 	,a.OrderNo                             ");
            searchSql.AppendLine(" 	,a.Title                             ");
            searchSql.AppendLine(" 	,a.Rate                                ");
            searchSql.AppendLine(" 	,a.CurrencyType                        ");
            searchSql.AppendLine(" 	,(a.TotalPrice*a.Discount/100) TotalPrice  ");
            searchSql.AppendLine(" 	,a.CustID                              ");
            searchSql.AppendLine(" 	,isnull(Convert(VARCHAR(10),a.OrderDate,21),'')as OrderDate                           ");
            searchSql.AppendLine(" 	,b.CustName                            ");
            searchSql.AppendLine(" 	,c.EmployeeName                        ");
            searchSql.AppendLine(" 	,d.CurrencyName                        ");
            searchSql.AppendLine(" FROM                                    ");
            searchSql.AppendLine(" 	officedba.SellOrder a left join                                                 ");
            searchSql.AppendLine(" 	officedba.CustInfo b  on a.CustID=b.ID  left join                               ");
            searchSql.AppendLine(" 	officedba.EmployeeInfo c  on a.Seller=c.ID left join     ");
            searchSql.AppendLine(" officedba.CurrencyTypeSetting d on a.CurrencyType=d.ID          ");
            searchSql.AppendLine(" WHERE                                   ");
            searchSql.AppendLine(" 	a.CompanyCD = @CompanyCD and a.BillStatus!='1'              ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //时间
            if (!string.IsNullOrEmpty(StartDate))
            {
                searchSql.AppendLine("	AND a.OrderDate >= @starttime ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@starttime", StartDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                searchSql.AppendLine("	AND a.OrderDate<=@endtime");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@endtime", EndDate));
            }
            if (!string.IsNullOrEmpty(OrderNo))
            {
                searchSql.AppendLine("	and a.OrderNo like '%'+ @OrderNo +'%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", OrderNo));
            }
            if (!string.IsNullOrEmpty(EmpName))
            {
                searchSql.AppendLine(" and c.EmployeeName like '%'+ @EmpName + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmpName", EmpName));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
        }

      /// <summary>
      /// 获取个人提成录入数据
      /// </summary>
      /// <param name="CompanyCD"></param>
      /// <param name="OrderNo"></param>
      /// <param name="EmpName"></param>
      /// <param name="StartDate"></param>
      /// <param name="EndDate"></param>
      /// <param name="pageIndex"></param>
      /// <param name="pageCount"></param>
      /// <param name="ord"></param>
      /// <param name="totalCount"></param>
      /// <returns></returns>

      public static DataTable SearchInputPersonalRoyalty(string CompanyCD, string EmpNO, string EmpName, string StartDate, string EndDate)
      {
          #region 查询语句
          StringBuilder searchSql = new StringBuilder();
          searchSql.AppendLine(" SELECT                                    ");
          searchSql.AppendLine(" 	 a.ID                                  ");
          searchSql.AppendLine(" 	,a.CompanyCD                           ");
          searchSql.AppendLine(" 	,a.EmployeeID                          ");
          searchSql.AppendLine(" 	,a.FromBillNo                             ");
          searchSql.AppendLine(" 	,Convert(numeric(10,2),a.Rate) as Rate                   ");
          searchSql.AppendLine(" 	,a.CurrencyType                        ");
          searchSql.AppendLine(" 	,Convert(numeric(10,2),a.AfterTaxMoney) as AfterTaxMoney                      ");
          searchSql.AppendLine(" 	,a.CustID                              ");
          searchSql.AppendLine(" 	,Convert(numeric(10,2),a.NewCustomerTax) as NewCustomerTax                      ");
          searchSql.AppendLine(" 	,isnull(Convert(VARCHAR(10),a.CreateTime,21),'')as CreateTime                           ");
          searchSql.AppendLine(" 	,b.CustName                            ");
          searchSql.AppendLine(" 	,c.EmployeeName                        ");
          searchSql.AppendLine(" 	,c.EmployeeNo                          ");
          searchSql.AppendLine(" 	,d.CurrencyName                        ");
          searchSql.AppendLine(" FROM                                      ");
          searchSql.AppendLine(" 	officedba.InputPersonalRoyalty a left join               ");
          searchSql.AppendLine(" 	officedba.CustInfo b  on a.CustID=b.ID  left join        ");
          searchSql.AppendLine(" 	officedba.EmployeeInfo c  on a.EmployeeID=c.ID left join     ");
          searchSql.AppendLine(" officedba.CurrencyTypeSetting d on a.CurrencyType=d.ID      ");
          searchSql.AppendLine(" WHERE                                   ");
          searchSql.AppendLine(" 	a.CompanyCD = @CompanyCD          ");
          #endregion

          //定义查询的命令
          SqlCommand comm = new SqlCommand();
          //公司代码
          comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
          //时间
          if (!string.IsNullOrEmpty(StartDate))
          {
              searchSql.AppendLine("	AND a.CreateTime >= @starttime ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@starttime", StartDate));
          }
          if (!string.IsNullOrEmpty(EndDate))
          {
              searchSql.AppendLine("	AND a.CreateTime<=@endtime");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@endtime", EndDate));
          }
          if (!string.IsNullOrEmpty(EmpNO))
          {
              searchSql.AppendLine("	and c.EmployeeNo like '%'+ @EmpNO +'%' ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmpNO", EmpNO));
          }
          if (!string.IsNullOrEmpty(EmpName))
          {
              searchSql.AppendLine(" and c.EmployeeName like '%'+ @EmpName + '%'");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmpName", EmpName));
          }

          comm.CommandText = searchSql.ToString();
          //执行查询
          return SqlHelper.ExecuteSearch(comm);
      }
          /// <summary>
          /// 插入销售订单主表
          /// </summary>
          /// <param name="dt"></param>
          /// <param name="ModifiedUserID"></param>
          /// <returns></returns>
        public static bool UpdateIsuPersonalTaxInfo(DataTable dt, string ModifiedUserID)
        {
            if (!DeletePersonalTaxInfo(dt.Rows[0]["CompanyCD"].ToString()))
            {
                return false;
            }
            bool isSucc = false;
            foreach (DataRow row in dt.Rows)
            {
                #region 插入SQL拼写
                StringBuilder insertSql = new StringBuilder();
                insertSql.AppendLine("insert into  officedba.InputPersonalRoyalty(CompanyCD,EmployeeID,FromType,CurrencyType,Rate,AfterTaxMoney,CustID,NewCustomerTax,CreateTime ");
                insertSql.AppendLine(", FromBillNo,ModifiedDate,ModifiedUserID)");
                insertSql.AppendLine("    values(@CompanyCD,@EmployeeID,'1',@CurrencyType,@Rate,@AfterTaxMoney,@CustID,@NewCustomerTax,@CreateTime,@FromBillNo,getdate(),@ModifiedUserID) ");
                #endregion
                //定义插入基本信息的命令
                SqlCommand comm = new SqlCommand();
                comm.CommandText = insertSql.ToString();
                //设置保存的参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", row["CompanyCD"].ToString()));	//公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", row["EmployeeID"].ToString()));	//创建人
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrencyType ", row["CurrencyType"].ToString()));	//启用状态
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate", row["Rate"].ToString()));	//启用状态
                comm.Parameters.AddWithValue("@AfterTaxMoney", row["TotalPrice"].ToString());	//启用状态
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", row["CustID"].ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@NewCustomerTax", ""));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateTime", row["OrderDate"].ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", row["OrderNo"].ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", ModifiedUserID));
                //添加返回参数
                //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

                //执行插入操作
                isSucc = SqlHelper.ExecuteTransWithCommand(comm);
                if (!isSucc)
                {
                    isSucc = false;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return isSucc;

        }
        /// <summary>
        /// 插入销售订单明细表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ModifiedUserID"></param>
        /// <returns></returns>
        public static bool UpdatePieceworkSalary(DataTable dt, string ModifiedUserID,string CompancyCD)
        {
            if (!DeletePieceworkSalary(CompancyCD))
            {
                return false;
            }
           
            bool isSucc = false;
            foreach (DataRow row in dt.Select("ProdNo is not null"))
            {
                #region 插入SQL拼写
                StringBuilder insertSql = new StringBuilder();
                insertSql.AppendLine("insert into  officedba.CommissionSalary(CompanyCD,EmployeeID,CommDate,ItemNo,Amount,SalaryMoney,ModifiedDate,ModifiedUserID,Flag )");
                insertSql.AppendLine("    values(@CompanyCD,@EmployeeID,@CommDate,@ItemNo,@Amount,@SalaryMoney,getdate(),@ModifiedUserID,'1') ");
                #endregion
                //定义插入基本信息的命令
                SqlCommand comm = new SqlCommand();
                comm.CommandText = insertSql.ToString();
                //设置保存的参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",CompancyCD ));	//公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", row["EmployeeID"].ToString()));	//创建人
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CommDate ", row["OrderDate"].ToString()));	//启用状态
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemNo", row["ProdNo"].ToString()));	//启用状态
                comm.Parameters.AddWithValue("@Amount", Convert.ToDecimal(row["TotalPrice"].ToString()));	//启用状态
                comm.Parameters.AddWithValue("@SalaryMoney", Convert.ToDecimal(row["SalaryMoney"].ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", ModifiedUserID));
                //添加返回参数
                //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

                //执行插入操作
                isSucc = SqlHelper.ExecuteTransWithCommand(comm);
                if (!isSucc)
                {
                    isSucc = false;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return isSucc;

        }
       /// <summary>
       /// 删除个人提成
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
        public static bool DeletePersonalTaxInfo(string CompanyCD)
        {
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine("Delete from officedba.InputPersonalRoyalty where CompanyCD=@CompanyCD");

            //定义插入基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));	//公司代码
            //添加返回参数
            //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

            //执行插入操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
            return isSucc;


        }

        /// <summary>
        /// 删除单品提成
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeletePieceworkSalary(string CompanyCD)
        {
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine("Delete from officedba.CommissionSalary where CompanyCD=@CompanyCD and Flag='1' ");

            //定义插入基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));	//公司代码
            //添加返回参数
            //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

            //执行插入操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
            return isSucc;


        }
        public static DataTable GetSellOrderSynchronizer(string OrdrNO, string CompanyCD)
        {
            string allID = "";
            string[] IdS = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            OrdrNO = OrdrNO.Substring(1, OrdrNO.Length-1);
            IdS = OrdrNO.Split(',');

            for (int i = 0; i < IdS.Length; i++)
            {
                IdS[i] = "'" + IdS[i] + "'";
                sb.Append(IdS[i]);
            }
            allID = sb.ToString().Replace("''", "','");
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" SELECT                                    ");
            Sql.AppendLine(" 	 a.ID                                  ");
            Sql.AppendLine(" 	,a.CompanyCD                           ");
            Sql.AppendLine(" 	,a.seller as  EmployeeID                         ");
            Sql.AppendLine(" 	,a.OrderNo                             ");
            Sql.AppendLine(" 	,a.CurrencyType                        ");
            Sql.AppendLine(" 	,(a.TotalPrice*a.Discount/100) TotalPrice  ");
            Sql.AppendLine(" 	,a.CustID                              ");
            Sql.AppendLine(" 	,a.Rate                              ");
            Sql.AppendLine(" 	,a.OrderDate                           ");
            Sql.AppendLine(" 	,b.CustName                            ");
            Sql.AppendLine(" 	,c.EmployeeName                        ");
            Sql.AppendLine(" 	,d.CurrencyName                        ");
            Sql.AppendLine(" FROM                                      ");
            Sql.AppendLine(" 	officedba.SellOrder a left join        ");
            Sql.AppendLine(" 	officedba.CustInfo b  on a.CustID=b.ID  left join            ");
            Sql.AppendLine(" 	officedba.EmployeeInfo c  on a.seller=c.ID left join     ");
            Sql.AppendLine(" officedba.CurrencyTypeSetting d on a.CurrencyType=d.ID          ");
            Sql.AppendLine(" WHERE                                     ");
            Sql.AppendLine(" 	a.OrderNo  IN (" + allID + ") and a.CompanyCD = @CompanyCD               ");
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //指定命令的SQL文
            comm.CommandText = Sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);

        }
        public static DataTable GetSellOrderSynchronizerDetail(string OrdrNO, string CompanyCD)
        {
            string allID = "";
            string[] IdS = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            OrdrNO = OrdrNO.Substring(0, OrdrNO.Length);
            IdS = OrdrNO.Split(',');

            for (int i = 0; i < IdS.Length; i++)
            {
                IdS[i] = "'" + IdS[i] + "'";
                sb.Append(IdS[i]);
            }
            allID = sb.ToString().Replace("''", "','");
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" SELECT                                    ");
            Sql.AppendLine(" 	 a.ID                                  ");
            Sql.AppendLine(" 	,a.CompanyCD                           ");
            Sql.AppendLine(" 	,a.OrderNo                             ");
            Sql.AppendLine(" 	,a.ProductID                           ");
            Sql.AppendLine(" 	,a.TotalPrice                          ");
            Sql.AppendLine(" 	,d.ProductName                         ");
            Sql.AppendLine(" 	,d.ProdNO                         ");
            Sql.AppendLine(" FROM                                      ");
            Sql.AppendLine(" 	officedba.SellOrderDetail a left join        ");
            Sql.AppendLine(" officedba.ProductInfo d on a.ProductID=d.ID          ");
            Sql.AppendLine(" WHERE                                     ");
            Sql.AppendLine(" 	a.OrderNo  IN (" + allID + ") and a.CompanyCD = @CompanyCD               ");
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //指定命令的SQL文
            comm.CommandText = Sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);

        }

        public static DataTable GetSellDetail(string CompanyCD,string OrderNo)
        {
            string allID = "";
            string[] IdS = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            OrderNo = OrderNo.Substring(1, OrderNo.Length-1);
            IdS = OrderNo.Split(',');

            for (int i = 0; i < IdS.Length; i++)
            {
                IdS[i] = "'" + IdS[i] + "'";
                sb.Append(IdS[i]);
            }
            allID = sb.ToString().Replace("''", "','");
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",CompanyCD),
                new SqlParameter("@allID",allID)
            };
            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[SellOrderSynchronizer]", param);
            return dt;
        }
          /// <summary>
          /// 获取销售订单明细信息
          /// </summary>
          /// <param name="CompanyCD"></param>
          /// <param name="OrderNo"></param>
          /// <returns></returns>
        public static DataTable GetSellDetailProdNo(string CompanyCD, string OrdrNO)
        {
            string allID = "";
            string[] IdS = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            OrdrNO = OrdrNO.Substring(1, OrdrNO.Length-1);
            IdS = OrdrNO.Split(',');

            for (int i = 0; i < IdS.Length; i++)
            {
                IdS[i] = "'" + IdS[i] + "'";
                sb.Append(IdS[i]);
            }
            allID = sb.ToString().Replace("''", "','");
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" SELECT                                    ");
            Sql.AppendLine(" 	 distinct c.ProdNo                                  ");
            Sql.AppendLine(" 	from officedba. SellOrderDetail a                          ");
            Sql.AppendLine(" 	left join officedba.ProductInfo c                             ");
            Sql.AppendLine(" 	on a.ProductID=c.ID left join                           ");
            Sql.AppendLine(" 	officedba.CommissionItem d on c.prodno=Itemno                          ");
            Sql.AppendLine(" 	and c.companycd=a.companycd                         ");
            Sql.AppendLine(" 	left join officedba. SellOrder e                         ");
            Sql.AppendLine(" on e.orderno=a.orderno                                      ");
            Sql.AppendLine(" WHERE                                     ");
            Sql.AppendLine(" 	a.OrderNo  IN (" + allID + ") and a.CompanyCD = @CompanyCD               ");
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //指定命令的SQL文
            comm.CommandText = Sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
      /// <summary>
        /// 获取所有的产品单品提成设置
      /// </summary>
      /// <param name="CompanyCD"></param>
      /// <param name="OrdrNO"></param>
      /// <returns></returns>
        public static DataTable GetCommissionItemProdNo(string CompanyCD)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" SELECT                                    ");
            Sql.AppendLine(" 	 ItemNo                                  ");
            Sql.AppendLine(" 	from officedba.CommissionItem                          ");
            Sql.AppendLine(" WHERE                                     ");
            Sql.AppendLine(" 	CompanyCD = @CompanyCD  and ItemNo is not null             ");
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //指定命令的SQL文
            comm.CommandText = Sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);

        }
       
        /// <summary>
        /// 判断是否是新老客户
        /// </summary>
        /// <param name="codename"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static bool ChargeNewOrOld(string CustID, string CompanyCD, string OrderDate)
        {
            SqlParameter[] parameters = { new SqlParameter("@CompanyCD", SqlDbType.VarChar, 50),
                                          new SqlParameter("@CustID",SqlDbType.VarChar,50),
                                          new SqlParameter("@OrderDate",SqlDbType.VarChar,50)};
            parameters[0].Value = CompanyCD;
            parameters[1].Value = CustID;
            parameters[2].Value = OrderDate;
            object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.SellOrder where CustID=@CustID and CompanyCD=@CompanyCD and OrderDate>@OrderDate", parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }



        /// <summary>
        /// 判断是否有通用规则
        /// </summary>
        /// <param name="codename"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static bool ChargeHava(string CompanyCD)
        {
            SqlParameter[] parameters = { new SqlParameter("@CompanyCD", SqlDbType.VarChar, 50)};
            parameters[0].Value = CompanyCD;
            object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.CommissionItem where ItemNo is null and CompanyCD=@CompanyCD ", parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }

        /// <summary>
        /// 判断个人提成设置是否有这个客户
        /// </summary>
        /// <param name="codename"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static bool ChargeCust(string CustID, string CompanyCD)
        {
            SqlParameter[] parameters = { new SqlParameter("@CompanyCD", SqlDbType.VarChar, 50),
                                          new SqlParameter("@CustID",SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = CompanyCD;
            parameters[1].Value = CustID;
            object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.SalaryPersonalRoyaltySet where CustID=@CustID and CompanyCD=@CompanyCD", parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }
        /// <summary>
        /// 判断个人提成设置是否有这个员工
        /// </summary>
        /// <param name="codename"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static bool ChargeEmp(string EmpID, string CompanyCD)
        {
            SqlParameter[] parameters = { new SqlParameter("@CompanyCD", SqlDbType.VarChar, 50),
                                          new SqlParameter("@EmpID",SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = CompanyCD;
            parameters[1].Value = EmpID;
            object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.SalaryPersonalRoyaltySet where EmployeeID=@EmpID and CompanyCD=@CompanyCD", parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }
      /// <summary>
      /// 根据员工和客户获取整个个人提成设置
      /// </summary>
      /// <param name="OrdrNO"></param>
      /// <param name="CompanyCD"></param>
      /// <returns></returns>
        public static DataTable GetSellOrderSynchronizerDetail(string CustID,string EmpID,string CompanyCD)
        {
            DataTable dt=new DataTable ();
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" SELECT                                    ");
            Sql.AppendLine(" 	 a.ID                                  ");
            Sql.AppendLine(" 	,a.EmployeeID                           ");
            Sql.AppendLine(" 	,a.CustID                             ");
            Sql.AppendLine(" 	,a.MiniMoney                            ");
            Sql.AppendLine(" 	,a.MaxMoney                          ");
            Sql.AppendLine(" 	,a.TaxPercent                         ");
            Sql.AppendLine(" 	,a.NewCustomerTax                         ");
            Sql.AppendLine(" 	,a.OldCustomerTax                         ");
            Sql.AppendLine(" FROM                                      ");
            Sql.AppendLine(" 	officedba.SalaryPersonalRoyaltySet a ");
            Sql.AppendLine(" WHERE                                     ");
            Sql.AppendLine(" 	a.CustID=@CustID and a.EmployeeID=@EmployeeID and a.CompanyCD = @CompanyCD               ");
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", EmpID));

            //指定命令的SQL文
            comm.CommandText = Sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);

        }

       

        public static DataTable GetSynchronizerRate(string ID)
        {
            string allID = "";
            string[] IdS = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            ID = ID.Substring(1, ID.Length - 1);
            IdS = ID.Split(',');

            for (int i = 0; i < IdS.Length; i++)
            {
                IdS[i] = "'" + IdS[i] + "'";
                sb.Append(IdS[i]);
            }
            allID = sb.ToString().Replace("''", "','");
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" SELECT                                    ");
            Sql.AppendLine(" 	 a.ID                                  ");
            Sql.AppendLine(" 	,a.AfterTaxMoney  ");
            Sql.AppendLine(" 	,a.CustID                              ");
            Sql.AppendLine(" 	,a.NewCustomerTax                              ");
            Sql.AppendLine(" 	,a.CreateTime                              ");
            Sql.AppendLine(" 	,a.EmployeeID                           ");
            Sql.AppendLine(" FROM                                      ");
            Sql.AppendLine(" 	officedba.InputPersonalRoyalty a       ");
            Sql.AppendLine(" WHERE                                     ");
            Sql.AppendLine(" 	a.ID  IN (" + allID + ")                ");
            SqlCommand comm = new SqlCommand();
            //公司代码

            //指定命令的SQL文
            comm.CommandText = Sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);

        }
        /// <summary>
        /// 同步提成率
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Rate"></param>
        /// <returns></returns>
        public static bool UpdateRate(string ID, string NewCustomerTax)
        {
            List<SqlCommand> SqlList = new List<SqlCommand>();
            string[] IdS = null;
            string[] Rate = null;
            int j = 0;
            ID = ID.Substring(1, ID.Length - 1);
            IdS = ID.Split(',');
            NewCustomerTax = NewCustomerTax.Substring(0, NewCustomerTax.Length - 1);
            Rate = NewCustomerTax.Split(',');
            foreach (string Edit_ID in IdS)
            {
                j++;
                StringBuilder strSubSql = new StringBuilder();
                strSubSql.Append("update officedba.InputPersonalRoyalty  set NewCustomerTax=@NewCustomerTax where ID=@ID");
               
                SqlParameter[] subparameters = {
				new SqlParameter("@NewCustomerTax",  SqlDbType.Decimal,9),
				new SqlParameter("@ID", SqlDbType.VarChar,8)};
                subparameters[0].Value = Rate[j-1].ToString();
                subparameters[1].Value = Edit_ID;

                SqlList.Add(SqlHelper.GetNewSqlCommond(strSubSql.ToString(), subparameters));
            }
            bool res = SqlHelper.ExecuteTransWithCollections(SqlList);
            return res;
        }
       #region 查询个人业务提成信息
        /// <summary>
        /// 查询个人业务提成信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="salaryMonth">年月份</param>
        /// <returns></returns>
        public static DataTable GetMonthPersonSalary(string companyCD, string salaryMonth, string startDate, string endDate)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT 		                    ");
            searchSql.AppendLine(" 	SUM( convert(numeric(12,2),isnull(AfterTaxMoney,0)*isnull(NewCustomerTax,0)/100)  ) AS TotalSalary ");
            searchSql.AppendLine(" 	,EmployeeID AS EmployeeID       ");
            searchSql.AppendLine(" FROM                             ");
            searchSql.AppendLine(" 	officedba.InputPersonalRoyalty      ");
            searchSql.AppendLine(" WHERE                            ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD  and CreateTime  between @startDate and @endDate       ");
             
            searchSql.AppendLine(" GROUP BY                         ");
            searchSql.AppendLine(" 	EmployeeID                      ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //员工ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@startDate", startDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@endDate", endDate));
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        /// <summary>
        /// 当没有设置通用规则时查询需要同步销售订单的明细信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetNotSetSellDetail(string CompanyCD, string OrdrNO)
        {
            string allID = "";
            string[] IdS = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            OrdrNO = OrdrNO.Substring(1, OrdrNO.Length - 1);
            IdS = OrdrNO.Split(',');

            for (int i = 0; i < IdS.Length; i++)
            {
                IdS[i] = "'" + IdS[i] + "'";
                sb.Append(IdS[i]);
            }
            allID = sb.ToString().Replace("''", "','");
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" SELECT                                    ");
            Sql.AppendLine(" 	 e.seller as EmployeeID,e.OrderDate,a.orderno,a.productid,                                  ");
            Sql.AppendLine(" 	a.productCount,a.TotalPrice                         ");
            Sql.AppendLine(" 	,c.ProdNo,d.rate,(a.totalprice*d.rate/100) as SalaryMoney                              ");
            Sql.AppendLine(" 	from officedba. SellOrderDetail a left join officedba.ProductInfo c                          ");
            Sql.AppendLine(" 	on a.ProductID=c.ID left join officedba.CommissionItem d                          ");
            Sql.AppendLine(" 	on c.prodno=Itemno and c.companycd=a.companycd                        ");
            Sql.AppendLine(" 	left join officedba. SellOrder e on e.orderno=a.orderno                       ");
            Sql.AppendLine("  and e.companycd=a.companycd                                      ");
            Sql.AppendLine(" WHERE                                     ");
            Sql.AppendLine(" 	a.OrderNo  IN (" + allID + ") and a.CompanyCD = @CompanyCD               ");
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //指定命令的SQL文
            comm.CommandText = Sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
    }
}

/**********************************************
 * 类作用：   分店库存日结处理
 * 建立人：  王保军
 * 建立时间： 2009/03/30
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

namespace XBase.Data.Office.StorageManager
{
  public   class SubDayEndDBHelper
  {
      #region 从分仓存量表中获取改公司分店下的所有产品信息
      /// <summary>
        /// 从分仓存量表中获取改公司分店下的所有产品信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetCompanyProductList(string CompanyCD,string DeptID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  isnull(BatchNo,'') as BatchNo,ProductID  ");
            strSql.Append("from officedba.SubStorageProduct   ");
            strSql.Append(" where CompanyCD=@CompanyCD  and  DeptID=@DeptID");

            ArrayList parmList = new ArrayList();
            parmList.Add(new SqlParameter("@CompanyCD", CompanyCD));
            parmList.Add(new SqlParameter("@DeptID", DeptID));
            return SqlHelper.ExecuteSql(strSql.ToString(), parmList);

        }
        #endregion

        #region 获得第一天的某个物品的结存量
        /// <summary>
        /// 获得前一天的某个物品的结存量
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="StorageID"></param>
        /// <param name="DailyDate"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static decimal GetFirstDayCount(string ProductID, string BatchNo, string CompanyCD, string DeptID)
        {
            decimal TodayCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  ProductCount  as TodayCount  ");
            strSql.Append("FROM officedba.SubStorageProduct    ");
            if (string.IsNullOrEmpty(BatchNo))
            {
                strSql.Append(" where CompanyCD=@CompanyCD and DeptID=@DeptID   and (BatchNo=@BatchNo or BatchNo is Null) and ProductID=@ProductID    ");
            }
            else
            {
                strSql.Append(" where CompanyCD=@CompanyCD and DeptID=@DeptID   and BatchNo=@BatchNo and ProductID=@ProductID   ");
            }
     


            SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar), 
                    new SqlParameter("@BatchNo", SqlDbType.VarChar),
                    new SqlParameter("@ProductID", SqlDbType.Int ),
                    new SqlParameter("@DeptID", SqlDbType.Int ),
                                          };
            parameters[0].Value = CompanyCD; 
            parameters[1].Value = BatchNo;
            parameters[2].Value = ProductID;
            parameters[3].Value = DeptID;
            try
            {
                TodayCount = Convert.ToDecimal(SqlHelper.ExecuteScalar(strSql.ToString(), parameters));
            }
            catch 
            {
                TodayCount = -123456789;
            
            }

            return TodayCount;
        }
        #endregion

        #region 获得前一天的某个物品的结存量
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
            decimal TodayCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT isnull(TodayCount,0) as TodayCount  ");
            strSql.Append("FROM officedba.SubStorageDaily    ");
            strSql.Append(" where CompanyCD=@CompanyCD and DeptID=@DeptID  and DailyDate=@DailyDate  and ProductID=@ProductID   ");

            if (!string.IsNullOrEmpty(BatchNo))
            {
                strSql.Append("  and BatchNo=@BatchNo     ");
                SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar),
					new SqlParameter("@DailyDate", SqlDbType.DateTime ),
                    new SqlParameter("@BatchNo", SqlDbType.VarChar),
                    new SqlParameter("@ProductID", SqlDbType.Int ),
                    new SqlParameter("@DeptID", SqlDbType.Int ),
                                          };
                parameters[0].Value = CompanyCD;
                parameters[1].Value = DailyDate;
                parameters[2].Value = BatchNo;
                parameters[3].Value = ProductID;
                parameters[4].Value = DeptID;
                TodayCount = Convert.ToDecimal(SqlHelper.ExecuteScalar(strSql.ToString(), parameters));
            }
            else
            {
                
                SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar),
					new SqlParameter("@DailyDate", SqlDbType.DateTime ), 
                    new SqlParameter("@ProductID", SqlDbType.Int ),
                    new SqlParameter("@DeptID", SqlDbType.Int ),
                                          };
                parameters[0].Value = CompanyCD;
                parameters[1].Value = DailyDate; 
                parameters[2].Value = ProductID;
                parameters[3].Value = DeptID;
                TodayCount = Convert.ToDecimal(SqlHelper.ExecuteScalar(strSql.ToString(), parameters));
 
            }



        

            return TodayCount;
        }
        #endregion

        #region 获得库存流水账表中取得当天的某个物品的各种单据信息
        /// <summary>
        /// 获得前一天的某个物品的数量
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="StorageID"></param>
        /// <param name="DailyDate"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static decimal GetDayItemsCount(string ProductID, string BatchNo, string DeptID,  string DailyDate, string CompanyCD, int ItemsType)
        {
            decimal HappenCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT isnull(sum( isnull(HappenCount,0)),0) as HappenCount  ");
            strSql.Append("FROM officedba.SubStorageAccount   ");
            if (!string.IsNullOrEmpty(BatchNo))
            {
                strSql.Append(" where CompanyCD=@CompanyCD and BillType=@BillType and CONVERT(VARCHAR(10),HappenDate,21) =@DailyDate   and BatchNo=@BatchNo and ProductID=@ProductID and DeptID=@DeptID     ");
                SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar),
                     new SqlParameter("@BillType", SqlDbType.Int ),
					new SqlParameter("@DailyDate", SqlDbType.DateTime ),
                    new SqlParameter("@BatchNo", SqlDbType.VarChar),
                    new SqlParameter("@ProductID", SqlDbType.Int ),
                    new SqlParameter("@DeptID", SqlDbType.Int ),
                                          };
                parameters[0].Value = CompanyCD;
                parameters[1].Value = ItemsType;
                parameters[2].Value = DailyDate;
                parameters[3].Value = BatchNo;
                parameters[4].Value = ProductID;
                parameters[5].Value = DeptID;
                HappenCount = Convert.ToDecimal(SqlHelper.ExecuteScalar(strSql.ToString(), parameters));
            }
            else
            {

                strSql.Append(" where CompanyCD=@CompanyCD and BillType=@BillType and CONVERT(VARCHAR(10),HappenDate,21) =@DailyDate   and (BatchNo is null or BatchNo ='') and ProductID=@ProductID  and DeptID=@DeptID     ");
                SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar),
                     new SqlParameter("@BillType", SqlDbType.Int ),
					new SqlParameter("@DailyDate", SqlDbType.DateTime ),
                    new SqlParameter("@ProductID", SqlDbType.Int ),
                     new SqlParameter("@DeptID", SqlDbType.Int ),
                                          };
                parameters[0].Value = CompanyCD;
                parameters[1].Value = ItemsType;
                parameters[2].Value = DailyDate;
                parameters[3].Value = ProductID;
                parameters[4].Value = DeptID;
                HappenCount = Convert.ToDecimal(SqlHelper.ExecuteScalar(strSql.ToString(), parameters));
            }




            return HappenCount;
        }
        #endregion

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
        public static decimal GetDayItemsPrice(string ProductID, string BatchNo, string StorageID, string DailyDate, string CompanyCD, int ItemsType)
        {
            decimal TotalPrice = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT cast( isnull(sum( isnull(HappenCount,0)*isnull(Price,0) ),0)  as Numeric(22,6) ) TotalPrice  ");
            strSql.Append("FROM officedba.SubStorageAccount   ");
            if (!string.IsNullOrEmpty(BatchNo))
            {
                strSql.Append(" where CompanyCD=@CompanyCD and BillType=@BillType and CONVERT(VARCHAR(10),HappenDate,21) =@DailyDate AND StorageID=@StorageID   and BatchNo=@BatchNo and ProductID=@ProductID      ");
                SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar),
                     new SqlParameter("@BillType", SqlDbType.Int ),
					new SqlParameter("@DailyDate", SqlDbType.DateTime ),
                    new SqlParameter("@StorageID", SqlDbType.Int ),
                    new SqlParameter("@BatchNo", SqlDbType.VarChar),
                    new SqlParameter("@ProductID", SqlDbType.Int ),
                                          };
                parameters[0].Value = CompanyCD;
                parameters[1].Value = ItemsType;
                parameters[2].Value = DailyDate;
                parameters[3].Value = StorageID;
                parameters[4].Value = BatchNo;
                parameters[5].Value = ProductID;
                TotalPrice = Convert.ToDecimal(SqlHelper.ExecuteScalar(strSql.ToString(), parameters));
            }
            else
            {

                strSql.Append(" where CompanyCD=@CompanyCD and BillType=@BillType and CONVERT(VARCHAR(10),HappenDate,21) =@DailyDate AND StorageID=@StorageID   and (BatchNo is null or BatchNo ='')  and ProductID=@ProductID      ");
                SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar),
                     new SqlParameter("@BillType", SqlDbType.Int ),
					new SqlParameter("@DailyDate", SqlDbType.DateTime ),
                    new SqlParameter("@StorageID", SqlDbType.Int ), 
                    new SqlParameter("@ProductID", SqlDbType.Int ),
                                          };
                parameters[0].Value = CompanyCD;
                parameters[1].Value = ItemsType;
                parameters[2].Value = DailyDate;
                parameters[3].Value = StorageID;
                parameters[4].Value = ProductID;
                TotalPrice = Convert.ToDecimal(SqlHelper.ExecuteScalar(strSql.ToString(), parameters));
            }




            return TotalPrice;
        }

        /// <summary>
        /// 从分店销售订单表中分离出当天某发货模式下的信息
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="StorageID"></param>
        /// <param name="DailyDate"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSaleInfo(string ProductID, string BatchNo,string  DeptID, string DailyDate, string CompanyCD,int SendModle)
        {
            ArrayList parmList = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select cast( isnull(sum(isnull(a.ProductCount,0)),0) as Numeric(22,6)) CountTotal, ");
            strSql.Append("cast(isnull(sum(isnull(a.TotalFee,0)),0) as  Numeric(22,6))   TotalPrice  ");
            strSql.Append(" from officedba.SubSellOrderDetail a   ");
            strSql.Append(" left outer join officedba. SubSellOrder b   ");
            strSql.Append("on a.CompanyCD=b.CompanyCD and a.OrderNo=b.OrderNo ");
            if (!string.IsNullOrEmpty(BatchNo))
            {
                strSql.Append(" where a.CompanyCD=@CompanyCD and a.DeptID=@DeptID  and b.SendMode=@SendMode  and CONVERT(VARCHAR(10),b.OrderDate,21) =@OrderDate   and (b.BillStatus=2 or b.BillStatus=4 or b.BillStatus=5 ) and (b.BusiStatus=2 or b.BusiStatus=3 or b.BusiStatus=4 ) and a.BatchNo=@BatchNo and a.ProductID=@ProductID");
                parmList.Add(new SqlParameter("@BatchNo", BatchNo));
            }
            else
            {
                strSql.Append(" where a.CompanyCD=@CompanyCD and a.DeptID=@DeptID  and b.SendMode=@SendMode  and CONVERT(VARCHAR(10),b.OrderDate,21) =@OrderDate   and (b.BillStatus=2 or b.BillStatus=4 or b.BillStatus=5 ) and (b.BusiStatus=2 or b.BusiStatus=3 or b.BusiStatus=4 )  and a.ProductID=@ProductID and (a.BatchNo is null or a.BatchNo ='') ");
            }
            parmList.Add(new SqlParameter("@CompanyCD", CompanyCD));
            parmList.Add(new SqlParameter("@DeptID", DeptID));
            parmList.Add(new SqlParameter("@SendMode", SendModle));
            parmList.Add(new SqlParameter("@ProductID", ProductID));
            parmList.Add(new SqlParameter("@OrderDate", DailyDate));
            return SqlHelper.ExecuteSql(strSql.ToString(), parmList);

        }


        /// <summary>
        /// 从分店销售退货表中分离出当天某发货模式下的信息
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="StorageID"></param>
        /// <param name="DailyDate"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSaleRejectInfo(string ProductID, string BatchNo, string DeptID, string DailyDate, string CompanyCD, int SendModle)
        {
            ArrayList parmList = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select cast( isnull(sum(isnull(a.BackCount,0)),0) as Numeric(22,6)) CountTotal, ");
            strSql.Append("cast(isnull(sum(isnull(a.TotalFee,0)),0) as  Numeric(22,6))   TotalPrice  ");
            strSql.Append(" from officedba.SubSellBackDetail a   ");
            strSql.Append(" left outer join officedba. SubSellBack  b   ");
            strSql.Append("on a.CompanyCD=b.CompanyCD and a.BackNo=b.BackNo ");
            if (!string.IsNullOrEmpty(BatchNo))
            {
                strSql.Append(" where a.CompanyCD=@CompanyCD and a.DeptID=@DeptID  and b.SendMode=@SendMode  and CONVERT(VARCHAR(10),b.BackDate,21) =@OrderDate   and (b.BillStatus='2' or b.BillStatus='4' or b.BillStatus='5' ) and (b.BusiStatus='2' or b.BusiStatus='3' or b.BusiStatus='4' ) and a.BatchNo=@BatchNo and a.ProductID=@ProductID");
                parmList.Add(new SqlParameter("@BatchNo", BatchNo));
            }
            else
            {
                strSql.Append(" where a.CompanyCD=@CompanyCD and a.DeptID=@DeptID  and b.SendMode=@SendMode  and CONVERT(VARCHAR(10),b.BackDate,21) =@OrderDate   and (b.BillStatus='2' or b.BillStatus='4' or b.BillStatus='5' ) and (b.BusiStatus='2' or b.BusiStatus='3' or b.BusiStatus='4' )  and a.ProductID=@ProductID  and (a.BatchNo is null or a.BatchNo ='') ");
            }
            parmList.Add(new SqlParameter("@CompanyCD", CompanyCD));
            parmList.Add(new SqlParameter("@DeptID", DeptID));
            parmList.Add(new SqlParameter("@SendMode", SendModle));
            parmList.Add(new SqlParameter("@ProductID", ProductID));
            parmList.Add(new SqlParameter("@OrderDate", DailyDate));
            return SqlHelper.ExecuteSql(strSql.ToString(), parmList);

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
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,CONVERT(VARCHAR(10),a.DailyDate,21)  as DailyDate,a.ProductID,a.DeptID");
            sql.AppendLine(",isnull(b.ProdNo,'') as ProductNo ");
            sql.AppendLine(" ,isnull(b.ProductName,'') as ProductName ");
            sql.AppendLine(" ,isnull(c.DeptName,'') as DeptName ");
            sql.AppendLine("  ,isnull(b.Specification,'') as Specification                ");
            sql.AppendLine(" ,isnull(a.BatchNo,'') as BatchNo                         ");
            sql.AppendLine(" ,   Convert(numeric(22," + userInfo.SelPoint + "),a.InTotal) as InTotal   ");
            sql.AppendLine(" ,          Convert(numeric(22," + userInfo.SelPoint + "),a.OutTotal) as OutTotal               ");
            sql.AppendLine(" ,      Convert(numeric(22," + userInfo.SelPoint + "),a.TodayCount) as TodayCount  ");
            sql.AppendLine("from officedba.SubStorageDaily a ");
            sql.AppendLine("left outer join officedba.ProductInfo b on a.CompanyCD=b.CompanyCD and a.ProductID=b.id  ");
            sql.AppendLine("left outer join officedba.DeptInfo c on a.CompanyCD=c.CompanyCD and a.DeptID=c.id  ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD  and a.DeptID=@DeptID ");

            if (!string.IsNullOrEmpty(day))
            {
                sql.AppendLine(" AND a.DailyDate =@day");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@day", day));
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID",DeptID   ));
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
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
            #region SQL文
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("delete from  officedba.SubStorageDaily  ");
            strSql.AppendLine("where CompanyCD=@CompanyCD and DailyDate=@DailyDate and DeptID=@DeptID ");

            #endregion

            #region 参数
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            param[1] = SqlHelper.GetParameter("@DailyDate", day);
            param[2] = SqlHelper.GetParameter("@DeptID", DeptID);
            #endregion

            SqlHelper.ExecuteTransSql(strSql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }

        /// <summary>
        /// 防止重复日结某一天，出现数据冗余，先删除当天在日结表中的数据
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static bool CheckDay(string CompanyCD, string day, string DeptID)
        {
            #region SQL文
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select count(*) from   officedba.SubStorageDaily  ");
            strSql.AppendLine("where CompanyCD=@CompanyCD and DailyDate=@DailyDate  and DeptID=@DeptID  ");

            #endregion

            #region 参数
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            param[1] = SqlHelper.GetParameter("@DailyDate", day);
            param[2] = SqlHelper.GetParameter("@DeptID", DeptID);
            #endregion

            int count = (int)SqlHelper.ExecuteScalar(strSql.ToString(), param);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 判断是否是第一次日结
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static bool CheckFirstOperate(string CompanyCD,string DeptID)
        {
            #region SQL文
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select count(*) from   officedba.SubStorageDaily  ");
            strSql.AppendLine("where CompanyCD=@CompanyCD and DeptID=@DeptID  ");

            #endregion

            #region 参数
            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            param[1] = SqlHelper.GetParameter("@DeptID", DeptID );
            #endregion

            int count = (int)SqlHelper.ExecuteScalar(strSql.ToString(), param);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


      
            /// <summary>
        /// 判断是否是第一次日结
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static bool isHaveData(string CompanyCD, string DeptID,string day)
        {
            #region SQL文
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select count(*) from   officedba.SubStorageAccount   ");
            strSql.AppendLine("where CompanyCD=@CompanyCD and DeptID=@DeptID  and  CONVERT(VARCHAR(10),HappenDate,21) =@day ");

            #endregion

            #region 参数
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            param[1] = SqlHelper.GetParameter("@DeptID", DeptID );
            param[2] = SqlHelper.GetParameter("@day", day);
            #endregion

            int count = (int)SqlHelper.ExecuteScalar(strSql.ToString(), param);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 返回第一次日结的日期和最后一次做日结的日期
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static DataTable GetOperateDateInfo(string CompanyCD,string Deptid)
        {
            #region SQL文
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select min(CONVERT(varchar(100), DailyDate, 23)) as FirstDailyDate,max(CONVERT(varchar(100), DailyDate, 23)) as LastDailyDate  from officedba.SubStorageDaily ");
            strSql.AppendLine("where CompanyCD=@CompanyCD and Deptid=@Deptid ");

            #endregion

            #region 参数
            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            param[1] = SqlHelper.GetParameter("@Deptid", Deptid);
            #endregion

            return SqlHelper.ExecuteSql(strSql.ToString(), param);

        }



        public static DataTable GetSubStorageDailyInfo(string ProductID, string BatchNo, string DeptID, string DailyDate, string CompanyCD)
        {
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"]; 

            ArrayList parmList = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  ");
            strSql.Append("isnull(c.DeptName,'') as DeptName  ");
            strSql.Append(" , CONVERT(VARCHAR(10),a.DailyDate,21) as DailyDate   ");
            strSql.Append(", a.ProductID   ");
            strSql.Append(", isnull(a.BatchNo,'') as  BatchNo ");
            strSql.Append(",   Convert(numeric(22," + userInfo.SelPoint + "),a.InitInCount) as InitInCount    ");
            strSql.Append(", Convert(numeric(22," + userInfo.SelPoint + "),a.SendInCount) as SendInCount    ");
            strSql.Append(",Convert(numeric(22," + userInfo.SelPoint + "),a.SubSaleBackInCount) as SubSaleBackInCount   ");
            strSql.Append(",        Convert(numeric(22," + userInfo.SelPoint + "),a.SaleBackInCount) as SaleBackInCount   ");
            strSql.Append(",   Convert(numeric(22," + userInfo.SelPoint + "),a.DispInCont) as DispInCont   ");
            strSql.Append(",    Convert(numeric(22," + userInfo.SelPoint + "),a.SubSaleOutCount) as SubSaleOutCount   ");
            strSql.Append(",      Convert(numeric(22," + userInfo.SelPoint + "),a.SaleOutCount) as SaleOutCount   ");
            strSql.Append(",  Convert(numeric(22," + userInfo.SelPoint + "),a.DispOutCount) as DispOutCount    ");
            strSql.Append(", Convert(numeric(22," + userInfo.SelPoint + "),a.SendOutCount) as SendOutCount    ");
            strSql.Append(",   Convert(numeric(22," + userInfo.SelPoint + "),a.InTotal) as InTotal   ");
            strSql.Append(",    Convert(numeric(22," + userInfo.SelPoint + "),a.OutTotal) as OutTotal  ");
            strSql.Append(",    Convert(numeric(22," + userInfo.SelPoint + "),a.TodayCount) as TodayCount    ");
            strSql.Append(",   Convert(numeric(22," + userInfo.SelPoint + "),a.SaleFee) as SaleFee     ");
            strSql.Append(",  Convert(numeric(22," + userInfo.SelPoint + "),a.SaleBackFee) as SaleBackFee    ");
            strSql.Append(",  Convert(numeric(22," + userInfo.SelPoint + "),a.InitBatchCount) as InitBatchCount    ");
            strSql.Append(", a.CreateDate   ");
            strSql.Append(", isnull(b.EmployeeName,'') as CreatorName  ");
            strSql.Append(", isnull(d.ProductName,'') as ProductName   ");
            strSql.Append(", isnull(d.ProdNo,'') as ProductNo   ");
            strSql.Append("FROM  officedba . SubStorageDaily a   ");
            strSql.Append("left outer join officedba.EmployeeInfo b   ");
            strSql.Append("on a.CompanyCD=b.CompanyCD and a.Creator=b.id   ");
            strSql.Append("left outer join officedba.DeptInfo c  ");
            strSql.Append("on a.CompanyCD=c.CompanyCD and a.DeptID=c.id   ");
            strSql.Append("left outer join officedba.ProductInfo d  ");
            strSql.Append("on a.CompanyCD=d.CompanyCD  and a.ProductID=d.id   ");



            if (!string.IsNullOrEmpty(BatchNo.Trim ()))
            {
                strSql.Append(" where a.CompanyCD=@CompanyCD and a.DeptID=@DeptID    and CONVERT(VARCHAR(10),a.DailyDate,21) =@DailyDate   and  a.BatchNo=@BatchNo and a.ProductID=@ProductID");
                parmList.Add(new SqlParameter("@BatchNo", BatchNo));
            }
            else
            {
                strSql.Append(" where a.CompanyCD=@CompanyCD and a.DeptID=@DeptID    and CONVERT(VARCHAR(10),a.DailyDate,21) =@DailyDate   and  (a.BatchNo is null or a.BatchNo ='') and  a.ProductID=@ProductID  ");
            }
            parmList.Add(new SqlParameter("@CompanyCD", CompanyCD));
            parmList.Add(new SqlParameter("@DeptID", DeptID)); 
            parmList.Add(new SqlParameter("@ProductID", ProductID));
            parmList.Add(new SqlParameter("@DailyDate", DailyDate));
            return SqlHelper.ExecuteSql(strSql.ToString(), parmList);

        }

    }
}

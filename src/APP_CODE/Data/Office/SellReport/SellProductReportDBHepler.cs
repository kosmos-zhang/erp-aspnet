/********************
**销售汇报
*创建人：hexw
*创建时间：2010-7-6
********************/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.SellReport;

namespace XBase.Data.Office.SellReport
{
    public class SellProductReportDBHepler
    {
        #region 增、删、改
        #region 添加
        public static bool Insert(SellReportModel sellrptModel, List<SellReportDetailModel> sellRptDetailModellList, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            int billID = 0;
            strMsg = "";
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                billID = InsertSellReport(sellrptModel, tran);
                InsertSellReportDetail(sellRptDetailModellList,billID, tran);
                tran.Commit();
                isSucc = true;
                strMsg = "保存成功！|"+billID;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                strMsg = "保存失败，请联系系统管理员！";
                throw ex;
            }
           
            return isSucc;
        }
        #endregion
        #region 销售汇报 插入主表信息
        /// <summary>
        /// 销售汇报 插入主表信息
        /// </summary>
        /// <param name="sellrptModel"></param>
        /// <param name="tran"></param>
        private static int InsertSellReport(SellReportModel sellrptModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("insert into officedba.SellReport(");
            strSql.AppendLine("CompanyCD,SellDept,productID,productName,price,sellNum,sellPrice,createdate,memo)");
            strSql.AppendLine(" values (");
            strSql.AppendLine("@CompanyCD,@SellDept,@productID,@productName,@price,@sellNum,@sellPrice,@createdate,@memo)");
            strSql.AppendLine(";set @Id=@@IDENTITY");
            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",sellrptModel.CompanyCD),
                                    new SqlParameter("@SellDept",sellrptModel.SellDept),
                                    new SqlParameter("@productID",sellrptModel.ProductID),
                                    new SqlParameter("@productName",sellrptModel.ProductName),
                                    new SqlParameter("@price",sellrptModel.Price),
                                    new SqlParameter("@sellNum",sellrptModel.SellNum),
                                    new SqlParameter("@sellPrice",sellrptModel.SellPrice),
                                    new SqlParameter("@createdate",sellrptModel.CreateDate),
                                    new SqlParameter("@memo",sellrptModel.Memo),
                                    new SqlParameter("@Id",SqlDbType.Int,6)
                                   };
            param[9].Direction = ParameterDirection.Output;

            foreach (SqlParameter para in param)
            {
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }

            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
            int Id = Convert.ToInt32(param[9].Value);
            return Id;
        }
        #endregion
        #region 销售汇报 插入明细信息
        private static void InsertSellReportDetail(List<SellReportDetailModel> sellRptDetailModellList,int billID, TransactionManager tran)
        {
            foreach (SellReportDetailModel sellRptDetailModel in sellRptDetailModellList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendLine("insert into officedba.sellerrate(");
                strSql.AppendLine("CompanyCD,sellreportID,sellerID,rate)");
                strSql.AppendLine(" values (");
                strSql.AppendLine("@CompanyCD,@sellreportID,@sellerID,@rate)");
                #region 参数
                SqlParameter[] parameters = {
					                            new SqlParameter("@CompanyCD", SqlDbType.VarChar,20),
					                            new SqlParameter("@sellreportID", SqlDbType.Int,4),
					                            new SqlParameter("@sellerID", SqlDbType.Int,4),
					                            new SqlParameter("@rate", SqlDbType.Decimal,9)
                                            };
                parameters[0].Value = sellRptDetailModel.CompanyCD;
                parameters[1].Value = billID;
                parameters[2].Value = sellRptDetailModel.SellerID;
                parameters[3].Value = sellRptDetailModel.Rate;
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

        #region 删除销售汇报
        /// <summary>
        /// 删除销售汇报
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DelSellRpt(string ids, string strCompanyCD, out string strMsg, out string strFieldText)
        {
            bool isSucc = false;
            strMsg = "";
            strFieldText = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellReport WHERE id IN ( " + ids + " ) and CompanyCD='" + strCompanyCD + "'", null);
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.sellerRate WHERE sellreportID IN ( " + ids + " ) and CompanyCD='" + strCompanyCD + "'", null);

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
            return isSucc;
        }
        #endregion

        #region 更新销售汇报
        /// <summary>
        /// 更新销售发汇报
        /// </summary>
        /// <returns></returns>
        public static bool UpdateSellReport(SellReportModel sellrptModel, List<SellReportDetailModel> sellRptDetailModellList, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";

            string strSql = "delete from officedba.sellerRate where  sellreportID=@sellreportID  and CompanyCD=@CompanyCD";
            SqlParameter[] paras = { new SqlParameter("@sellreportID", sellrptModel.ID), new SqlParameter("@CompanyCD", sellrptModel.CompanyCD) };
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {

                UpdateSellReportMain(sellrptModel, tran);
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);
                InsertSellReportDetail(sellRptDetailModellList,sellrptModel.ID, tran);
                tran.Commit();
                strMsg = "保存成功！";
                isSucc = true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                strMsg = "保存失败，请联系系统管理员！";
                throw ex;
            }
            
            return isSucc;
        }
        #endregion
        #region 更新主表数据
        /// <summary>
        /// 更新主表数据
        /// </summary>
        /// <param name="sellBackModel"></param>
        /// <param name="tran"></param>
        private static void UpdateSellReportMain(SellReportModel sellrptModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.SellReport set ");
            strSql.AppendLine("SellDept=@SellDept,");
            strSql.AppendLine("productID=@productID,");
            strSql.AppendLine("productName=@productName,");
            strSql.AppendLine("price=@price,");
            strSql.AppendLine("sellNum=@sellNum,");
            strSql.AppendLine("sellPrice=@sellPrice,");
            strSql.AppendLine("createdate=@createdate,");
            strSql.AppendLine("memo=@memo ");
            strSql.AppendLine(" where CompanyCD=@CompanyCD and ID=@ID ");

            SqlParameter[] param = { 
                                    new SqlParameter("@ID",sellrptModel.ID.ToString()),
                                    new SqlParameter("@CompanyCD",sellrptModel.CompanyCD),
                                    new SqlParameter("@SellDept",sellrptModel.SellDept),
                                    new SqlParameter("@productID",sellrptModel.ProductID),
                                    new SqlParameter("@productName",sellrptModel.ProductName),
                                    new SqlParameter("@price",sellrptModel.Price),
                                    new SqlParameter("@sellNum",sellrptModel.SellNum),
                                    new SqlParameter("@sellPrice",sellrptModel.SellPrice),
                                    new SqlParameter("@createdate",sellrptModel.CreateDate),
                                    new SqlParameter("@memo",sellrptModel.Memo)
                                   };

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

        #region 获取相关信息
        #region 获取产品列表
        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="strCompanyCD">公司编号</param>
        /// <returns>ID和Name组成的Datatable</returns>
        public static DataTable GetProductList(string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select id,productName from officedba.UserProductInfo where CompanyCD=@CompanyCD");
            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 根据ID获取单据产品汇报详细信息
        /// <summary>
        /// 根据ID获取单据产品汇报详细信息
        /// </summary>
        /// <param name="id">单据ID</param>
        /// <returns></returns>
        public static DataTable GetSellReportMain(int id,string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select a.id,a.SellDept,a.productID,a.productName,a.price,a.sellNum,a.sellPrice,");
            strSql.AppendLine(" convert(varchar(10),a.createdate,120) as createdate,a.memo,d.DeptName as SellDeptName ");
            strSql.AppendLine(" from officedba.SellReport a ");
            strSql.AppendLine(" left join officedba.DeptInfo d on d.ID=a.SellDept ");
            strSql.AppendLine(" where a.id=@id");
            SqlParameter[] param = { 
                                    new SqlParameter("@id",id)
                                   };

            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 根据ID获取单据产品汇报详细信息(精度控制)
        /// <summary>
        /// 根据ID获取单据产品汇报详细信息(精度控制)
        /// </summary>
        /// <param name="id">单据ID</param>
        /// <param name="sellPointLen">精度</param>
        /// <returns></returns>
        public static DataTable GetSellReportMain(int id, string strCompanyCD, string selPointLen)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select a.SellDept,a.productID,a.productName,");
            strSql.AppendLine(" convert(decimal(22," + selPointLen + "),a.price) as price,");
            strSql.AppendLine(" convert(decimal(22," + selPointLen + "),a.sellNum) as sellNum,");
            strSql.AppendLine(" convert(decimal(22," + selPointLen + "),a.sellPrice) as sellPrice,");
            strSql.AppendLine(" convert(varchar(10),a.createdate,120) as createdate,a.memo,d.DeptName as SellDeptName ");
            strSql.AppendLine(" from officedba.SellReport a ");
            strSql.AppendLine(" left join officedba.DeptInfo d on d.ID=a.SellDept ");
            strSql.AppendLine(" where a.id=@id");
            SqlParameter[] param = { 
                                    new SqlParameter("@id",id)
                                   };

            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 获取明细
        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="id">汇报ID</param>
        /// <returns></returns>
        public static DataTable GetSellReportDetail(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select a.id,a.sellerID,a.rate,b.EmployeeName as SellerName ");
            strSql.AppendLine(" from officedba.sellerRate a ");
            strSql.AppendLine(" left join officedba.EmployeeInfo b on b.ID=a.SellerID ");
            strSql.AppendLine(" where a.sellreportID=@id ");
            SqlParameter[] param = { 
                                    new SqlParameter("@id",id)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 获取销售汇报列表
        /// <summary>
        /// 获取销售汇报列表
        /// </summary>
        /// <param name="sellrptModel">sellrptModel实体</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSellRptList(SellReportModel sellrptModel,DateTime? CreateDate1, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select a.id,a.SellDept,a.productID,a.productName,");
            strSql.AppendLine(" convert(decimal(22," + sellrptModel.SelPointLen + "),a.price) as price,");
            strSql.AppendLine(" convert(decimal(22," + sellrptModel.SelPointLen + "),a.sellNum) as sellNum,");
            strSql.AppendLine(" convert(decimal(22," + sellrptModel.SelPointLen + "),a.sellPrice) as sellPrice,");
            strSql.AppendLine(" convert(varchar(10),a.createdate,120) as createdate,dbo.getSellerList(a.id," + int.Parse(sellrptModel.SelPointLen) + ") as SellerRate, ");
            strSql.AppendLine(" d.DeptName as SellDeptName");
            strSql.AppendLine(" from officedba.SellReport a");
            strSql.AppendLine(" left join officedba.DeptInfo d on d.ID=a.SellDept");
            strSql.AppendLine(" where a.CompanyCD=@CompanyCD");

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", sellrptModel.CompanyCD));

            if (!string.IsNullOrEmpty(sellrptModel.SellDept.ToString()))
            {
                strSql.AppendLine(" and a.SellDept=@DeptID");
                arr.Add(new SqlParameter("@DeptID", sellrptModel.SellDept));
            }
            if (!string.IsNullOrEmpty(sellrptModel.CreateDate.ToString()))
            {
                strSql.AppendLine(" and a.createdate>=@CreateDate");
                arr.Add(new SqlParameter("@CreateDate", sellrptModel.CreateDate));
            }
            if (!string.IsNullOrEmpty(CreateDate1.ToString()))
            {
                strSql.AppendLine(" and a.createdate<dateadd(day,1,@CreateDate1)");
                arr.Add(new SqlParameter("@CreateDate1", CreateDate1));
            }

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }
        #endregion
        #endregion

        #region 获取产品信息
        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns></returns>
        public static DataTable GetProductInfoByID(int productID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select ID,productName,isnull(price,0) as price,bref,memo ");
            strSql.AppendLine(" from officedba.UserProductInfo where id=@ID");
            SqlParameter[] param = { 
                                    new SqlParameter("@ID",productID)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion
    }
}



using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Collections;

using XBase.Data.DBHelper;
using XBase.Common;
using XBase.Model.Office.StorageManager;

namespace XBase.Data.Office.StorageManager
{
    public class StorageBorrowDBHelper
    {

        #region 读取部门列表
        public static DataTable GetDept(string companyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT ID,DeptName FROM officedba.DeptInfo WHERE CompanyCD=@CompanyCD");
            SqlParameter[] sqlPara ={
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar)
                                   };
            sqlPara[0].Value = companyCD;
            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlPara);

        }
        #endregion

        #region 读取仓库列表
        public static DataTable GetDepot(string CompanyCD)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.Append("SELECT ID,StorageName FROM officedba.StorageInfo WHERE CompanyCD=@CompanyCD AND UsedStatus=1");
            SqlParameter[] sqlPara ={
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar)
                                   };
            sqlPara[0].Value = CompanyCD;

            return SqlHelper.ExecuteSql(sbStr.ToString(), sqlPara);

        }
        #endregion

        #region 读取物品信息 （关联查询出 物品单位 库存数量  成本售价）
        public static DataTable GetProductInfo(string CompanyCD, int BorrowDeptID, int StorageID, string ProductName, string ProdNo)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT   officedba.ProductInfo.ID AS ProductID, officedba.ProductInfo.ProdNo, officedba.ProductInfo.ProductName, officedba.CodeUnitType.CodeName,  (Isnull(officedba.StorageProduct.ProductCount,0)) as ProductCount, isnull(officedba.ProductInfo.StandardCost,'0.00') as StandardCost, officedba.ProductInfo.Specification, officedba.ProductInfo.ID,officedba.ProductInfo.UnitID AS UnitID,officedba.ProductInfo.CompanyCD FROM  officedba.ProductInfo Left JOIN officedba.CodeUnitType ON officedba.ProductInfo.UnitID = officedba.CodeUnitType.ID INNER JOIN officedba.StorageProduct ON officedba.ProductInfo.ID = officedba.StorageProduct.ProductID ");
            sbSql.Append(" WHERE officedba.ProductInfo.CompanyCD=officedba.StorageProduct.CompanyCD AND  officedba.ProductInfo.CompanyCD=@CompanyCD AND officedba.StorageProduct.StorageID=@StorageID ");

            int length = 2;
            if (!string.IsNullOrEmpty(ProductName))
                length++;
            if (!string.IsNullOrEmpty(ProdNo))
                length++;
            SqlParameter[] sqlPara = new SqlParameter[length];
            int index = 0;
            sqlPara[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            sqlPara[index++].Value = CompanyCD;

            sqlPara[index] = new SqlParameter("@StorageID", SqlDbType.Int);
            sqlPara[index++].Value = StorageID.ToString();

            if (!string.IsNullOrEmpty(ProductName))
            {
                ProductName = "%" + ProductName + "%";
                sbSql.Append(" AND officedba.ProductInfo.ProductName LIKE @ProductName ");
                sqlPara[index] = new SqlParameter("@ProductName", SqlDbType.VarChar);
                sqlPara[index++].Value = ProductName;
            }
            if (!string.IsNullOrEmpty(ProdNo))
            {
                ProdNo = "%" + ProdNo + "%";
                sbSql.Append(" AND officedba.ProductInfo.ProdNo LIKE @ProdNo ");
                sqlPara[index] = new SqlParameter("@ProdNo", SqlDbType.VarChar);
                sqlPara[index++].Value = ProdNo;
            }
            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlPara);

        }


        public static DataTable GetProductInfo(string CompanyCD, int BorrowDeptID, int StorageID, string ProductName, string ProdNo, string EFIndex, string EFDesc)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("SELECT   A.ID AS ProductID,ISNULL(A.IsBatchNo,'') IsBatchNo,ISNULL(C.BatchNo,'')BatchNo,A.ProdNo,A.ProductName,B.CodeName,(Isnull(C.ProductCount,0)) as ProductCount, ");
            sbSql.Append("isnull(A.StandardCost,'0.00') as StandardCost,A.Specification,A.ID,A.UnitID AS UnitID,A.CompanyCD  ");
            sbSql.Append("FROM  officedba.ProductInfo A Left JOIN  ");
            sbSql.Append("officedba.CodeUnitType B ON A.UnitID = B.ID  ");
            sbSql.Append("LEFT JOIN  ");
            sbSql.Append("( ");
            sbSql.Append("SELECT SUM(ProductCount) AS ProductCount,ProductID,StorageID,BatchNo FROM officedba.StorageProduct  ");
            sbSql.Append("WHERE CompanyCD=@CompanyCD  ");
            sbSql.Append("GROUP BY ProductID,StorageID,BatchNo ");
            sbSql.Append(") AS C ON c.ProductID=A.ID ");
            sbSql.Append("WHERE   A.CompanyCD=@CompanyCD AND C.StorageID=@StorageID  ");

            int length = 2;
            if (!string.IsNullOrEmpty(ProductName))
                length++;
            if (!string.IsNullOrEmpty(ProdNo))
                length++;
            if (EFIndex != "-1" && !string.IsNullOrEmpty(EFDesc))
                length++;
            SqlParameter[] sqlPara = new SqlParameter[length];
            int index = 0;
            sqlPara[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            sqlPara[index++].Value = CompanyCD;

            sqlPara[index] = new SqlParameter("@StorageID", SqlDbType.Int);
            sqlPara[index++].Value = StorageID.ToString();

            if (!string.IsNullOrEmpty(ProductName))
            {
                ProductName = "%" + ProductName + "%";
                sbSql.Append(" AND A.ProductName LIKE @ProductName ");
                sqlPara[index] = new SqlParameter("@ProductName", SqlDbType.VarChar);
                sqlPara[index++].Value = ProductName;
            }
            if (!string.IsNullOrEmpty(ProdNo))
            {
                ProdNo = "%" + ProdNo + "%";
                sbSql.Append(" AND A.ProdNo LIKE @ProdNo ");
                sqlPara[index] = new SqlParameter("@ProdNo", SqlDbType.VarChar);
                sqlPara[index++].Value = ProdNo;
            }
            if (EFIndex != "-1" && !string.IsNullOrEmpty(EFDesc))
            {
                EFDesc = "%" + EFDesc + "%";
                sbSql.Append("	AND A.ExtField" + EFIndex + " LIKE @EFDesc ");
                sqlPara[index] = new SqlParameter("@EFDesc", SqlDbType.VarChar);
                sqlPara[index++].Value = EFDesc;
            }

            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlPara);

        }

        #endregion

        #region 添加数据 （借货表 借货明明细表  插入成功返回 插入的主键）
        public static string SetBorrowStorage(StorageBorrow sborrow, List<StorageBorrowDetail> borrowlist, Hashtable ht)
        {
            List<SqlCommand> SqlList = new List<SqlCommand>();

            #region 构造查询字符串

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.StorageBorrow (");
            strSql.Append("CompanyCD,BorrowNo,Title,Borrower,DeptID,BorrowDate,ReasonType,OutDeptID,StorageID,OutDate,Transactor,TotalPrice,CountTotal,Summary,Remark,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@BorrowNo,@Title,@Borrower,@DeptID,@BorrowDate,@ReasonType,@OutDeptID,@StorageID,@OutDate,@Transactor,@TotalPrice,@CountTotal,@Summary,@Remark,@Creator,@CreateDate,@BillStatus,@ModifiedDate,@ModifiedUserID)");
            strSql.Append(";select @ID=@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@BorrowNo", SqlDbType.VarChar,50),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@Borrower", SqlDbType.Int,4),
					new SqlParameter("@DeptID", SqlDbType.Int,4),
					new SqlParameter("@BorrowDate", SqlDbType.DateTime),
					new SqlParameter("@ReasonType", SqlDbType.Int,4),
					new SqlParameter("@OutDeptID", SqlDbType.Int,4),
					new SqlParameter("@StorageID", SqlDbType.Int,4),
					new SqlParameter("@OutDate", SqlDbType.DateTime),
					new SqlParameter("@Transactor", SqlDbType.Int,4),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@CountTotal", SqlDbType.Decimal,9),
					new SqlParameter("@Summary", SqlDbType.VarChar,200),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@ID",SqlDbType.Int)};
            parameters[0].Value = sborrow.CompanyCD;
            parameters[1].Value = sborrow.BorrowNo;
            parameters[2].Value = sborrow.Title;
            parameters[3].Value = sborrow.Borrower;
            parameters[4].Value = sborrow.DeptID;
            parameters[5].Value = sborrow.BorrowDate;
            parameters[6].Value = sborrow.ReasonType;
            parameters[7].Value = sborrow.OutDeptID;
            parameters[8].Value = sborrow.StorageID;
            parameters[9].Value = sborrow.OutDate;
            parameters[10].Value = sborrow.Transactor;
            parameters[11].Value = sborrow.TotalPrice;
            parameters[12].Value = sborrow.CountTotal;
            parameters[13].Value = sborrow.Summary;
            parameters[14].Value = sborrow.Remark;
            parameters[15].Value = sborrow.Creator;
            parameters[16].Value = sborrow.CreateDate;
            parameters[17].Value = sborrow.BillStatus;
            parameters[18].Value = sborrow.ModifiedDate;
            parameters[19].Value = sborrow.ModifiedUserID;
            parameters[20].Direction = ParameterDirection.Output;

            #endregion

            SqlList.Add(SqlHelper.GetNewSqlCommond(strSql.ToString(), parameters));
            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(sborrow, ht, cmd);
            if (ht.Count > 0)
                SqlList.Add(cmd);
            #endregion

            #region 构造明细字符串
            foreach (StorageBorrowDetail borrow in borrowlist)
            {
                #region 构造SQL字符
                StringBuilder strSubSql = new StringBuilder();
                strSubSql.Append("insert into officedba.StorageBorrowDetail (");
                strSubSql.Append("CompanyCD,BorrowNo,SortNo,ProductID,UnitID,UnitPrice,ProductCount,TotalPrice,ReturnDate,ReturnCount,Remark,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSubSql.Append(" values (");
                strSubSql.Append("@CompanyCD,@BorrowNo,@SortNo,@ProductID,@UnitID,@UnitPrice,@ProductCount,@TotalPrice,@ReturnDate,@ReturnCount,@Remark,@ModifiedDate,@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSubSql.Append(" select @@IDENTITY");


                SqlParameter[] sqlParams = new SqlParameter[18];

                int index = 0;
                sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", borrow.CompanyCD);
                sqlParams[index++] = SqlHelper.GetParameter("@BorrowNo", borrow.BorrowNo);
                sqlParams[index++] = SqlHelper.GetParameter("@SortNo", borrow.SortNo);
                sqlParams[index++] = SqlHelper.GetParameter("@ProductID", borrow.ProductID);
                sqlParams[index++] = SqlHelper.GetParameter("@UnitID", borrow.UnitID);
                sqlParams[index++] = SqlHelper.GetParameter("@UnitPrice", borrow.UnitPrice);
                sqlParams[index++] = SqlHelper.GetParameter("@ProductCount", borrow.ProductCount);
                sqlParams[index++] = SqlHelper.GetParameter("@TotalPrice", borrow.TotalPrice);
                sqlParams[index++] = SqlHelper.GetParameter("@ReturnDate", borrow.ReturnDate);
                sqlParams[index++] = SqlHelper.GetParameter("@ReturnCount", borrow.ReturnCount);
                sqlParams[index++] = SqlHelper.GetParameter("@Remark", borrow.Remark);
                sqlParams[index++] = SqlHelper.GetParameter("@ModifiedDate", borrow.ModifiedDate);
                sqlParams[index++] = SqlHelper.GetParameter("@ModifiedUserID", borrow.ModifiedUserID);
                sqlParams[index++] = SqlHelper.GetParameter("@UsedUnitID", borrow.UsedUnitID);
                sqlParams[index++] = SqlHelper.GetParameter("@UsedUnitCount", borrow.UsedUnitCount);
                sqlParams[index++] = SqlHelper.GetParameter("@UsedPrice", borrow.UsedPrice);
                sqlParams[index++] = SqlHelper.GetParameter("@ExRate", borrow.ExRate);
                sqlParams[index++] = SqlHelper.GetParameter("@BatchNo", borrow.BatchNo);

                SqlList.Add(SqlHelper.GetNewSqlCommond(strSubSql.ToString(), sqlParams));
                #endregion
            }

            #endregion

            bool res = SqlHelper.ExecuteTransWithCollections(SqlList);

            if (res)
                return "1|" + SqlList[0].Parameters["@ID"].Value.ToString();
            else
                return "0|";
        }
        #endregion


        #region 读取借货原因
        public static DataTable GetBorrowReason(int Flag, string CompanyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT ID,CodeName,Description,UsedStatus,ModifiedDate,ModifiedUserID FROM officedba.CodeReasonType ");
            sbSql.Append(" WHERE Flag=@Flag AND CompanyCD=@CompanyCD");
            SqlParameter[] Para = { 
                       new SqlParameter("@Flag",SqlDbType.Int),
                       new SqlParameter("@CompanyCD",SqlDbType.VarChar)
                                  };
            Para[0].Value = Flag;
            Para[1].Value = CompanyCD;

            return SqlHelper.ExecuteSql(sbSql.ToString(), Para);
        }
        #endregion

        #region 验证单据编号唯一性
        protected static bool CheckBorrowNo(string BorrowNo, string CompanyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT TOP 1 * FROM officedba.StorageBorrow WHERE BorrowNo=@BorrowNo AND CompanyCD=@CompanyCD");
            SqlParameter[] Para = { 
                                      new SqlParameter("@BorrowNo",SqlDbType.VarChar),
                                      new SqlParameter("@CompanyCD",SqlDbType.VarChar)
                                  };
            Para[0].Value = BorrowNo;
            Para[1].Value = CompanyCD;
            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), Para);
            if (dt.Rows.Count > 0)
                return false;
            else
                return true;
        }
        #endregion

        #region 读取单据明细列表
        public static DataTable GetStorageBorrowDetail(string BorrowNo, string CompanyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT c.CodeName AS UsedUnitName,officedba.StorageBorrowDetail.BatchNo,officedba.StorageBorrowDetail.ExRate,officedba.StorageBorrowDetail.UsedPrice,officedba.StorageBorrowDetail.UsedUnitCount,officedba.StorageBorrowDetail.UsedUnitID,officedba.StorageBorrowDetail.RealReturnCount,officedba.StorageBorrowDetail.ID, officedba.StorageBorrowDetail.CompanyCD, officedba.StorageBorrowDetail.BorrowNo, officedba.StorageBorrowDetail.SortNo, officedba.StorageBorrowDetail.ProductID, officedba.StorageBorrowDetail.UnitID, officedba.StorageBorrowDetail.UnitPrice, officedba.StorageBorrowDetail.ProductCount, officedba.StorageBorrowDetail.TotalPrice,CONVERT(VARCHAR(10),officedba.StorageBorrowDetail.ReturnDate, 21) ReturnDate, officedba.StorageBorrowDetail.ReturnCount, officedba.StorageBorrowDetail.RealReturnCount, officedba.StorageBorrowDetail.Remark, officedba.StorageBorrowDetail.ModifiedDate, officedba.StorageBorrowDetail.ModifiedUserID, officedba.ProductInfo.ProdNo, officedba.ProductInfo.ProductName,officedba.CodeUnitType.CodeName,officedba.StorageProduct.ProductCount as UseCount ,  officedba.ProductInfo.Specification,officedba.CodeUnitType.ID AS CodeUnitID ,officedba.ProductInfo.MinusIs as MinusIs  FROM   officedba.StorageBorrowDetail LEFT JOIN  officedba.ProductInfo ON officedba.StorageBorrowDetail.ProductID = officedba.ProductInfo.ID  LEFT JOIN  officedba.CodeUnitType ON officedba.StorageBorrowDetail.UnitID = officedba.CodeUnitType.ID ");
            sbSql.Append(" LEFT  JOIN   officedba.StorageProduct ON officedba.StorageBorrowDetail.ProductID = officedba.StorageProduct.ProductID   and  officedba.StorageBorrowDetail.BatchNo = officedba.StorageProduct.BatchNo   and (officedba.StorageProduct.StorageID=(Select officedba.StorageBorrow.StorageID FROM officedba.StorageBorrow WHERE officedba.StorageBorrow.BorrowNo=@BorrowNo  AND officedba.StorageBorrow.Companycd=@Companycd ))");
            sbSql.Append(" LEFT JOIN officedba.CodeUnitType AS c ON c.ID=officedba.StorageBorrowDetail.UsedUnitID ");
            sbSql.Append("WHERE officedba.StorageBorrowDetail.CompanyCD=@CompanyCD  AND officedba.StorageBorrowDetail.BorrowNo=@BorrowNo");

            SqlParameter[] Para = {
                                      new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                      new SqlParameter("@BorrowNo",SqlDbType.VarChar)
                                  };
            Para[0].Value = CompanyCD;
            Para[1].Value = BorrowNo;

            return SqlHelper.ExecuteSql(sbSql.ToString(), Para);


        }

        #endregion

        #region 更新单据 及其明细
        public static string UpdateStorageBorrow(StorageBorrow sborrow, List<StorageBorrowDetail> borrowlist, Hashtable ht)
        {

            List<SqlCommand> SqlList = new List<SqlCommand>();

            #region 构造SQL字符串

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageBorrow set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("BorrowNo=@BorrowNo,");
            strSql.Append("Title=@Title,");
            strSql.Append("Borrower=@Borrower,");
            strSql.Append("DeptID=@DeptID,");
            strSql.Append("BorrowDate=@BorrowDate,");
            strSql.Append("ReasonType=@ReasonType,");
            strSql.Append("OutDeptID=@OutDeptID,");
            strSql.Append("StorageID=@StorageID,");
            strSql.Append("OutDate=@OutDate,");
            strSql.Append("Transactor=@Transactor,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("CountTotal=@CountTotal,");
            strSql.Append("Summary=@Summary,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("BillStatus=@BillStatus ");
            strSql.Append(" where ID=@ID ");
            strSql.Append(" ; select @ID ,@@ROWCOUNT as result");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@BorrowNo", SqlDbType.VarChar,50),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@Borrower", SqlDbType.Int,4),
					new SqlParameter("@DeptID", SqlDbType.Int,4),
					new SqlParameter("@BorrowDate", SqlDbType.DateTime),
					new SqlParameter("@ReasonType", SqlDbType.Int,4),
					new SqlParameter("@OutDeptID", SqlDbType.Int,4),
					new SqlParameter("@StorageID", SqlDbType.Int,4),
					new SqlParameter("@OutDate", SqlDbType.DateTime),
					new SqlParameter("@Transactor", SqlDbType.Int,4),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@CountTotal", SqlDbType.Decimal,9),
					new SqlParameter("@Summary", SqlDbType.VarChar,200),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
                                        };
            parameters[0].Value = sborrow.ID;
            parameters[1].Value = sborrow.CompanyCD;
            parameters[2].Value = sborrow.BorrowNo;
            parameters[3].Value = sborrow.Title;
            parameters[4].Value = sborrow.Borrower;
            parameters[5].Value = sborrow.DeptID;
            parameters[6].Value = sborrow.BorrowDate;
            parameters[7].Value = sborrow.ReasonType;
            parameters[8].Value = sborrow.OutDeptID;
            parameters[9].Value = sborrow.StorageID;
            parameters[10].Value = sborrow.OutDate;
            parameters[11].Value = sborrow.Transactor;
            parameters[12].Value = sborrow.TotalPrice;
            parameters[13].Value = sborrow.CountTotal;
            parameters[14].Value = sborrow.Summary;
            parameters[15].Value = sborrow.Remark;
            parameters[16].Value = sborrow.BillStatus;

            #endregion

            SqlList.Add(SqlHelper.GetNewSqlCommond(strSql.ToString(), parameters));

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(sborrow, ht, cmd);
            if (ht.Count > 0)
                SqlList.Add(cmd);
            #endregion
            #region 删除明细
            StringBuilder strDelSql = new StringBuilder();
            strDelSql.Append("delete officedba.StorageBorrowDetail ");
            strDelSql.Append(" where CompanyCD=@CompanyCD AND BorrowNo=@BorrowNo ");
            SqlParameter[] delparameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar),
                    new SqlParameter("@BorrowNo",SqlDbType.VarChar)
                                        };
            delparameters[0].Value = sborrow.CompanyCD;
            delparameters[1].Value = sborrow.BorrowNo;
            #endregion

            SqlList.Add(SqlHelper.GetNewSqlCommond(strDelSql.ToString(), delparameters));

            #region 构造明细字符串
            foreach (StorageBorrowDetail borrow in borrowlist)
            {

                StringBuilder strSubSql = new StringBuilder();
                strSubSql.Append("insert into officedba.StorageBorrowDetail (");
                strSubSql.Append("CompanyCD,BorrowNo,SortNo,ProductID,UnitID,UnitPrice,ProductCount,TotalPrice,ReturnDate,ReturnCount,Remark,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSubSql.Append(" values (");
                strSubSql.Append("@CompanyCD,@BorrowNo,@SortNo,@ProductID,@UnitID,@UnitPrice,@ProductCount,@TotalPrice,@ReturnDate,@ReturnCount,@Remark,@ModifiedDate,@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSubSql.Append(" select @@IDENTITY");
                SqlParameter[] subparameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@BorrowNo", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.VarChar,20),
					new SqlParameter("@UnitID", SqlDbType.Int,4),
					new SqlParameter("@UnitPrice", SqlDbType.Decimal,9),
					new SqlParameter("@ProductCount", SqlDbType.Decimal,9),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@ReturnDate", SqlDbType.DateTime),
					new SqlParameter("@ReturnCount", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,200),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@UsedUnitID", SqlDbType.Int,4),
					new SqlParameter("@UsedUnitCount", SqlDbType.Decimal,9),
					new SqlParameter("@UsedPrice", SqlDbType.Decimal,9),
					new SqlParameter("@ExRate", SqlDbType.Decimal,9),
                    new SqlParameter("@BatchNo",SqlDbType.VarChar,50)};
                subparameters[0].Value = borrow.CompanyCD;
                subparameters[1].Value = borrow.BorrowNo;
                subparameters[2].Value = borrow.SortNo;
                subparameters[3].Value = borrow.ProductID;
                subparameters[4].Value = borrow.UnitID;
                subparameters[5].Value = borrow.UnitPrice;
                subparameters[6].Value = borrow.ProductCount;
                subparameters[7].Value = borrow.TotalPrice;
                subparameters[8].Value = borrow.ReturnDate;
                subparameters[9].Value = borrow.ReturnCount;
                subparameters[10].Value = borrow.Remark;
                subparameters[11].Value = borrow.ModifiedDate;
                subparameters[12].Value = borrow.ModifiedUserID;
                subparameters[13].Value = borrow.UsedUnitID;
                subparameters[14].Value = borrow.UsedUnitCount;
                subparameters[15].Value = borrow.UsedPrice;
                subparameters[16].Value = borrow.ExRate;
                subparameters[17].Value = borrow.BatchNo;

                SqlList.Add(SqlHelper.GetNewSqlCommond(strSubSql.ToString(), subparameters));
            }

            #endregion

            bool res = SqlHelper.ExecuteTransWithCollections(SqlList);

            if (res)
                return "1|";
            else
                return "0|";

        }

        #endregion

        #region 更新单据状态
        public static string SetBillStatus(Model.Office.StorageManager.StorageBorrow borrow)
        {

            string data = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageBorrow set ");

            #region 根据单据状态 构造SQL语句 状态 2
            if (borrow.BillStatus == "3")
            {
                strSql.Append("BillStatus=@BillStatus,");
                strSql.Append("ModifiedDate=@ModifiedDate,");
                strSql.Append("ModifiedUserID=@ModifiedUserID ");
                strSql.Append(" where ID=@ID ");
                strSql.Append(" ; select @@ROWCOUNT as result");
                SqlParameter[] parameters = { new SqlParameter("@ID", SqlDbType.Int), new SqlParameter("@BillStatus", SqlDbType.Char, 1), new SqlParameter("@ModifiedDate", SqlDbType.DateTime), new SqlParameter("@ModifiedUserID", SqlDbType.VarChar, 50) };
                parameters[0].Value = borrow.ID;
                parameters[1].Value = borrow.BillStatus;
                parameters[2].Value = borrow.ModifiedDate;
                parameters[3].Value = borrow.ModifiedUserID;

                DataTable dt = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
                if (dt != null || dt.Rows.Count > 0)
                    data = "1|成功";
                else
                    data = "0|失败";
            }
            else if (borrow.BillStatus == "4")
            {

                strSql.Append("BillStatus=@BillStatus,");
                strSql.Append("ModifiedDate=@ModifiedDate ,");
                strSql.Append("ModifiedUserID=@ModifiedUserID,");
                strSql.Append("Closer=@Closer,");
                strSql.Append("CloseDate=@CloseDate ");
                strSql.Append(" where ID=@ID ");
                strSql.Append(" ; select @@ROWCOUNT as result");
                SqlParameter[] parameters = { 
                                                new SqlParameter("@ID", SqlDbType.Int), 
                                                new SqlParameter("@BillStatus", SqlDbType.Char, 1), 
                                                new SqlParameter("@ModifiedDate", SqlDbType.DateTime), 
                                                new SqlParameter("@ModifiedUserID", SqlDbType.VarChar, 50) ,
                                                new SqlParameter("@Closer", SqlDbType.Int,4),
				                             	new SqlParameter("@CloseDate", SqlDbType.DateTime)
                                            };
                parameters[0].Value = borrow.ID;
                parameters[1].Value = borrow.BillStatus;
                parameters[2].Value = borrow.ModifiedDate;
                parameters[3].Value = borrow.ModifiedUserID;
                parameters[4].Value = borrow.Closer;
                parameters[5].Value = borrow.CloseDate;



                DataTable dt = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
                if (dt != null || dt.Rows.Count > 0)
                    data = "1|结单成功";
                else
                    data = "0|结单失败";
            }
            else if (borrow.BillStatus == "2")
            {
                strSql.Append("BillStatus=@BillStatus,");
                strSql.Append("ModifiedDate=@ModifiedDate,");
                strSql.Append("ModifiedUserID=@ModifiedUserID,");
                strSql.Append("Confirmor=@Confirmor,");
                strSql.Append("ConfirmDate=@ConfirmDate ");
                strSql.Append(" where ID=@ID ");
                strSql.Append(" ; select @@ROWCOUNT as result");
                SqlParameter[] parameters = { 
                    new SqlParameter("@ID", SqlDbType.Int), 
                    new SqlParameter("@BillStatus", SqlDbType.Char, 1), 
                    new SqlParameter("@ModifiedDate", SqlDbType.DateTime), 
                    new SqlParameter("@ModifiedUserID", SqlDbType.VarChar, 50),
					new SqlParameter("@Confirmor", SqlDbType.Int,4),
					new SqlParameter("@ConfirmDate", SqlDbType.DateTime)
                                            };
                parameters[0].Value = borrow.ID;
                parameters[1].Value = borrow.BillStatus;
                parameters[2].Value = borrow.ModifiedDate;
                parameters[3].Value = borrow.ModifiedUserID;
                parameters[4].Value = borrow.Confirmor;
                parameters[5].Value = borrow.ConfirmDate;
                DataTable dt = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
                if (dt != null && dt.Rows.Count > 0)
                {
                    data = "1|确认单据成功";
                    try
                    {
                        SetStorageInfo(borrow);
                    }
                    catch
                    {
                        data = "0|确认单据失败失败";
                    }
                }
                else
                    data = "0|失败";
            }
            else if (borrow.BillStatus == "1")
            {
                bool res = CancelConfirm(borrow);
                if (res)
                    return "1|取消确认单据成功";
                else
                    return "0|取消确认单据失败";
            }
            else
                data = "0|失败";
            #endregion

            return data;

        }
        #endregion

        #region 库存移动
        public static void SetStorageInfo(StorageBorrow borrow)
        {
            #region 构造字符串查询明细表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM officedba.StorageBorrowDetail  ");
            strSql.Append(" WHERE CompanyCD=@CompanyCD AND BorrowNo=@BorrowNo ");
            SqlParameter[] Para ={
                                    new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                    new SqlParameter("@BorrowNo",SqlDbType.VarChar)
                                };
            Para[0].Value = borrow.CompanyCD;
            Para[1].Value = borrow.BorrowNo;
            DataTable dt = SqlHelper.ExecuteSql(strSql.ToString(), Para);
            List<SqlCommand> cmdList = new List<SqlCommand>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    #region 构造库存移动SQL字符串
                    StringBuilder sbSql = new StringBuilder();

                    //验证 明细中是否存入批次
                    bool IsBatch = false;
                    if (row["BatchNo"] != null && row["BatchNo"].ToString() != string.Empty && row["BatchNo"].ToString().ToLower() != "nobatch")
                        IsBatch = true;

                    //构造查询字符串
                    sbSql.Append("UPDATE officedba.StorageProduct SET  ");
                    sbSql.Append(" OutCount=isnull(OutCount,0)+@BorrowCount");
                    //借出量（增加）
                    sbSql.Append(" WHERE CompanyCD=@CompanyCD AND ProductID=@ProductID  AND StorageID=@StorageID " + (IsBatch ? "AND  BatchNo=@BatchNo" : string.Empty));


                    SqlParameter[] sqlParams = null;
                    sqlParams = IsBatch ? new SqlParameter[5] : new SqlParameter[4];
                    int index = 0;
                    sqlParams[index++] = SqlHelper.GetParameter("@BorrowCount", row["ProductCount"].ToString());
                    sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", row["CompanyCD"].ToString());
                    sqlParams[index++] = SqlHelper.GetParameter("@ProductID", row["ProductID"].ToString());
                    sqlParams[index++] = SqlHelper.GetParameter("@StorageID", borrow.StorageID);
                    if (IsBatch)
                        sqlParams[index++] = SqlHelper.GetParameter("@BatchNo", row["BatchNo"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sbSql.ToString();
                    cmd.Parameters.AddRange(sqlParams);
                    cmdList.Add(cmd);
                    #endregion
                }

                SqlHelper.ExecuteTransWithCollections(cmdList);
            }

            #endregion

        }
        #endregion

        #region 获取单据状态
        public static string GetBillStatus(StorageBorrow borrow)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT BillStatus FROM officedba.StorageBorrow ");
            strSql.Append(" WHERE ID=@ID");
            SqlParameter[] Para = { new SqlParameter("@ID", SqlDbType.Int) };
            Para[0].Value = borrow.ID;
            DataTable dt = SqlHelper.ExecuteSql(strSql.ToString(), Para);
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0]["BillStatus"].ToString();
            return string.Empty;

        }
        #endregion

        #region 读取借货单列表
        public static DataTable GetStorageList(string EFIndex, string EFDesc, int PageIndex, int PageSzie, string OrderBy, ref int TotalCount, StorageBorrow borrow, DateTime StartTime, DateTime EndTime, int SubmitStatus)
        {
            StringBuilder sbSql = new StringBuilder();
            string Submit = string.Empty;
            if (SubmitStatus > 0)
                Submit = "  AND  FlowStatus=" + SubmitStatus.ToString();
            else if (SubmitStatus == 0)
                Submit = "  AND FlowStatus is null ";
            sbSql.Append(" SELECT * FROM ");
            sbSql.Append(" (SELECT S.ID, S.CompanyCD, S.BorrowNo, S.Title, (select e.EmployeeName from officedba.EmployeeInfo as e where e.ID=S.Borrower  ) as Borrower, (SELECT D.DeptName FROM officedba.DeptInfo as d where d.ID=S.DeptID) AS DeptID , S.BorrowDate, S.ReasonType, (SELECT OD.DeptName FROM officedba.DeptInfo as OD where OD.ID=S.OutDeptID) AS OutDeptID, (select si.StorageName from officedba.StorageInfo as si where si.ID= S.StorageID) AS StorageID, S.OutDate, (select te.EmployeeName from officedba.EmployeeInfo as te where te.ID=S.Transactor ) AS Transactor,S.Creator, S.CreateDate,(CASE S.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,( select top 1 fi.FlowStatus  from  officedba.FlowInstance AS FI  where S.ID=FI.BillID AND fi.CompanyCD=S.CompanyCD AND fi.BillTypeCode=" + XBase.Common.ConstUtil.BILL_TYPECODE_STORAGE_BORROW.ToString() + " AND fi.BillTypeFlag=" + XBase.Common.ConstUtil.BILL_TYPEFLAG_STORAGE.ToString() + " order by FI.ModifiedDate DESC) as FlowStatus,S.Confirmor, S.ConfirmDate, S.Closer, S.CloseDate, S.ModifiedDate, S.ModifiedUserID,S.TotalPrice,S.CountTotal FROM officedba.StorageBorrow AS S  WHERE s.CompanyCD=@CompanyCD  ");

            Hashtable htPara = new Hashtable();
            if (!string.IsNullOrEmpty(borrow.BorrowNo.Trim()) && borrow.BorrowNo.Trim() != "")
                htPara.Add("BorrowNo", borrow.BorrowNo);
            if (!string.IsNullOrEmpty(borrow.Title.Trim()) && borrow.Title != "")
                htPara.Add("Title", borrow.Title);
            if (borrow.Borrower > 0)
                htPara.Add("Borrower", borrow.Borrower);
            if (borrow.DeptID > 0)
                htPara.Add("DeptID", borrow.DeptID);
            if (borrow.OutDeptID > 0)
                htPara.Add("OutDeptID", borrow.OutDeptID);
            if (borrow.StorageID > 0)
                htPara.Add("StorageID", borrow.StorageID);
            if (borrow.Transactor > 0)
                htPara.Add("Transactor", borrow.Transactor);
            if (StartTime > DateTime.MinValue)
                htPara.Add("StartTime", StartTime);
            if (EndTime > DateTime.MinValue)
                htPara.Add("EndTime", EndTime);
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                htPara.Add("ExtField", EFDesc);
                //sql += " and s.ExtField" + EFIndex + " like @EFDesc ";
                //comm.Parameters.Add(SqlHelper.GetParameter("@EFDesc", "%" + EFDesc + "%"));
            }
            if (!string.IsNullOrEmpty(borrow.BillStatus) && borrow.BillStatus.Trim() != "" && borrow.BillStatus != "-1")
                htPara.Add("BillStatus", borrow.BillStatus);
            htPara.Add("CompanyCD", borrow.CompanyCD);
            SqlParameter[] Paras = new SqlParameter[htPara.Count];
            int index = 0;
            foreach (string key in htPara.Keys)
            {
                switch (key)
                {
                    case "BorrowNo":
                        sbSql.Append(" AND S.BorrowNo like @" + key);
                        break;
                    case "Title":
                        sbSql.Append(" AND S.Title like @" + key);
                        break;
                    case "Borrower":
                        sbSql.Append(" AND  S.Borrower =@" + key);
                        break;
                    case "DeptID":
                        sbSql.Append(" AND S.DeptID=@" + key);
                        break;
                    case "OutDeptID":
                        sbSql.Append(" AND  S.OutDeptID=@" + key);
                        break;
                    case "StorageID":
                        sbSql.Append(" AND  S.StorageID=@" + key);
                        break;
                    case "Transactor":
                        sbSql.Append(" AND S.Transactor=@" + key);
                        break;
                    case "StartTime":
                        sbSql.Append(" AND S.BorrowDate>=@" + key);
                        break;
                    case "EndTime":
                        sbSql.Append(" AND  S.BorrowDate<=@" + key);
                        break;
                    case "ExtField":
                        sbSql.Append(" AND ExtField" + EFIndex + " like '" + EFDesc + "' ");
                        break;
                    case "BillStatus":
                        sbSql.Append(" AND S.BillStatus=@" + key);
                        break;
                    case "CompanyCD":
                        break;
                }
                Paras[index] = new SqlParameter("@" + key, SqlDbType.VarChar);
                Paras[index].Value = htPara[key];
                index++;
            }
            sbSql.Append(" ) AS temp");


            sbSql.Append(" WHERE 1=1 " + Submit);

            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSzie, OrderBy, Paras, ref TotalCount);
            //return SqlHelper.ExecuteSql(sbSql.ToString(),Paras);

        }
        /*不分页*/
        public static DataTable GetStorageList(string EFIndex, string EFDesc, StorageBorrow borrow, string OrderBy, DateTime StartTime, DateTime EndTime, int SubmitStatus)
        {
            StringBuilder sbSql = new StringBuilder();
            string Submit = string.Empty;
            if (SubmitStatus > 0)
                Submit = "  AND  FlowStatus=" + SubmitStatus.ToString();
            else if (SubmitStatus == 0)
                Submit = "  AND FlowStatus is null ";
            sbSql.Append(" SELECT * FROM ");
            sbSql.Append(" (SELECT S.ID, S.CompanyCD, S.BorrowNo, S.Title, (select e.EmployeeName from officedba.EmployeeInfo as e where e.ID=S.Borrower  ) as Borrower, (SELECT D.DeptName FROM officedba.DeptInfo as d where d.ID=S.DeptID) AS DeptID , S.BorrowDate, S.ReasonType, (SELECT OD.DeptName FROM officedba.DeptInfo as OD where OD.ID=S.OutDeptID) AS OutDeptID, (select si.StorageName from officedba.StorageInfo as si where si.ID= S.StorageID) AS StorageID, S.OutDate, (select te.EmployeeName from officedba.EmployeeInfo as te where te.ID=S.Transactor ) AS Transactor,S.Creator, S.CreateDate,(CASE S.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,( select top 1 fi.FlowStatus  from  officedba.FlowInstance AS FI  where S.ID=FI.BillID AND fi.CompanyCD=S.CompanyCD AND fi.BillTypeCode=" + XBase.Common.ConstUtil.BILL_TYPECODE_STORAGE_BORROW.ToString() + " AND fi.BillTypeFlag=" + XBase.Common.ConstUtil.BILL_TYPEFLAG_STORAGE.ToString() + " order by FI.ModifiedDate DESC) as FlowStatus,S.Confirmor, S.ConfirmDate, S.Closer, S.CloseDate, S.ModifiedDate, S.ModifiedUserID,S.TotalPrice,S.CountTotal FROM officedba.StorageBorrow AS S  WHERE s.CompanyCD=@CompanyCD  ");

            Hashtable htPara = new Hashtable();

            if (!string.IsNullOrEmpty(borrow.BorrowNo.Trim()) && borrow.BorrowNo.Trim() != "")
                htPara.Add("BorrowNo", borrow.BorrowNo);
            if (!string.IsNullOrEmpty(borrow.Title.Trim()) && borrow.Title != "")
                htPara.Add("Title", borrow.Title);
            if (borrow.Borrower > 0)
                htPara.Add("Borrower", borrow.Borrower);
            if (borrow.DeptID > 0)
                htPara.Add("DeptID", borrow.DeptID);
            if (borrow.OutDeptID > 0)
                htPara.Add("OutDeptID", borrow.OutDeptID);
            if (borrow.StorageID > 0)
                htPara.Add("StorageID", borrow.StorageID);
            if (borrow.Transactor > 0)
                htPara.Add("Transactor", borrow.Transactor);
            if (StartTime > DateTime.MinValue)
                htPara.Add("StartTime", StartTime);
            if (EndTime > DateTime.MinValue)
                htPara.Add("EndTime", EndTime);
            if (!string.IsNullOrEmpty(borrow.BillStatus) && borrow.BillStatus.Trim() != "" && borrow.BillStatus != "-1")
                htPara.Add("BillStatus", borrow.BillStatus);
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                htPara.Add("ExtField", EFDesc);
                //sql += " and s.ExtField" + EFIndex + " like @EFDesc ";
                //comm.Parameters.Add(SqlHelper.GetParameter("@EFDesc", "%" + EFDesc + "%"));
            }
            htPara.Add("CompanyCD", borrow.CompanyCD);

            SqlParameter[] Paras = new SqlParameter[htPara.Count];
            int index = 0;
            foreach (string key in htPara.Keys)
            {
                switch (key)
                {
                    case "BorrowNo":
                        sbSql.Append(" AND S.BorrowNo like @" + key);
                        break;
                    case "Title":
                        sbSql.Append(" AND S.Title like @" + key);
                        break;
                    case "Borrower":
                        sbSql.Append(" AND  S.Borrower =@" + key);
                        break;
                    case "DeptID":
                        sbSql.Append(" AND S.DeptID=@" + key);
                        break;
                    case "OutDeptID":
                        sbSql.Append(" AND  S.OutDeptID=@" + key);
                        break;
                    case "StorageID":
                        sbSql.Append(" AND  S.StorageID=@" + key);
                        break;
                    case "Transactor":
                        sbSql.Append(" AND S.Transactor=@" + key);
                        break;
                    case "StartTime":
                        sbSql.Append(" AND S.BorrowDate>=@" + key);
                        break;
                    case "EndTime":
                        sbSql.Append(" AND  S.BorrowDate<=@" + key);
                        break;
                    case "BillStatus":
                        sbSql.Append(" AND S.BillStatus=@" + key);
                        break;
                    case "ExtField":
                        sbSql.Append(" AND s.ExtField" + EFIndex + " like @" + EFDesc);
                        break;
                    case "CompanyCD":
                        break;
                }
                Paras[index] = new SqlParameter("@" + key, SqlDbType.VarChar);
                Paras[index].Value = htPara[key];
                index++;
            }
            sbSql.Append(" ) AS temp");


            sbSql.Append(" WHERE 1=1 " + Submit + "ORDER BY " + OrderBy);

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
            //return SqlHelper.ExecuteSql(sbSql.ToString(),Paras);

        }

        #endregion

        #region 删除借货单
        public static string DeleteStorageBorrow(string[] ID)
        {

            if (ID.Length > 0)
            {
                List<SqlCommand> SqlList = new List<SqlCommand>();
                for (int i = 0; i < ID.Length; i++)
                {
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("DELETE officedba.StorageBorrow WHERE ID=@ID");
                    SqlParameter[] Para = { new SqlParameter("@ID", SqlDbType.Int) };
                    Para[0].Value = ID[i];


                    SqlList.Add(SqlHelper.GetNewSqlCommond(sbSql.ToString(), Para));
                    //SqlHelper.ExecuteSql(sbSql.ToString(), Para);
                }
                bool res = SqlHelper.ExecuteTransWithCollections(SqlList);
                if (res)
                    return "1";
                else
                    return "0";
            }
            else
                return "0";
        }
        #endregion

        #region 获取借货单信息
        public static DataTable GetStorageBorrowByID(int ID)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT sb.ID, sb.CompanyCD, sb.BorrowNo, sb.Title, sb.Borrower, sb.DeptID, CONVERT(VARCHAR(10),sb.BorrowDate, 21)BorrowDate, sb.ReasonType, sb.OutDeptID, sb.StorageID,CONVERT(VARCHAR(10),sb.OutDate, 21) OutDate, sb.Transactor, sb.TotalPrice, sb.CountTotal, sb.Summary, sb.Remark, (SELECT ect.EmployeeName FROM  officedba.EmployeeInfo AS ect where ect.id=sb.Creator) as CreatorText, sb.CreateDate, sb.BillStatus, sb.Confirmor, sb.ConfirmDate, sb.Closer, sb.CloseDate,sb.ModifiedDate, sb.ModifiedUserID,(SELECT e.EmployeeName FROM officedba.EmployeeInfo as e where e.id=sb.Borrower ) as BorrowerText, (SELECT EI.EmployeeName FROM  officedba.EmployeeInfo AS EI where EI.ID=SB.Transactor) AS OuterText,(SELECT ec.EmployeeName FROM  officedba.EmployeeInfo AS ec where ec.id=sb.Confirmor) as ConfirmorText,(SELECT el.EmployeeName FROM officedba.EmployeeInfo AS el where el.ID=SB.Closer) AS CloserText,(SELECT di1.DeptName from officedba.DeptInfo as di1 where di1.id=sb.DeptID ) AS DeptName ,(SELECT di2.DeptName from officedba.DeptInfo as di2 where di2.ID=sb.OutDeptID ) as OutDeptName ,isnull(ExtField1,'')ExtField1,isnull(ExtField2,'')ExtField2,isnull(ExtField3,'')ExtField3,isnull(ExtField4,'')ExtField4,isnull(ExtField5,'')ExtField5,isnull(ExtField6,''),ExtField6,isnull(ExtField7,'')ExtField7,isnull(ExtField8,'')ExtField8,isnull(ExtField9,'')ExtField9,isnull(ExtField10,'')ExtField10 FROM  officedba.StorageBorrow AS sb WHERE ID=@ID");
            SqlParameter[] Para = { new SqlParameter("@ID", SqlDbType.Int) };
            Para[0].Value = ID;

            return SqlHelper.ExecuteSql(sbSql.ToString(), Para);

        }

        /*单据打印使用查询*/
        public static DataTable GetStorageBorrowInfo(int ID)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("    SELECT     ID, CompanyCD, BorrowNo, Title, Borrower, DeptID, CONVERT(VARCHAR(10),(CASE BorrowDate WHEN '1753-1-1 0:00:00' THEN NULL WHEN '1900-1-1 0:00:00' ");
            sbSql.AppendLine("THEN NULL ELSE BorrowDate END), 21) AS BorrowDate, ReasonType, OutDeptID, StorageID, CONVERT(VARCHAR(10),OutDate, 21)OutDate, Transactor, TotalPrice, CountTotal,  ");
            sbSql.AppendLine("    Summary, Remark,isnull(ExtField1,'')ExtField1,isnull(ExtField2,'')ExtField2,isnull(ExtField3,'')ExtField3,isnull(ExtField4,'')ExtField4,isnull(ExtField5,'')ExtField5,isnull(ExtField6,''),ExtField6,isnull(ExtField7,'')ExtField7,isnull(ExtField8,'')ExtField8,isnull(ExtField9,'')ExtField9,isnull(ExtField10,'')ExtField10, ");
            sbSql.AppendLine("     (SELECT     EmployeeName ");
            sbSql.AppendLine("     FROM          officedba.EmployeeInfo AS ect ");
            sbSql.AppendLine("     WHERE      (ID = sb.Creator)) AS CreatorText, CONVERT(VARCHAR(10),CreateDate, 21)CreateDate, BillStatus, Confirmor, CONVERT(VARCHAR(10),ConfirmDate, 21)ConfirmDate, Closer, CONVERT(VARCHAR(10),CloseDate, 21)CloseDate,CONVERT(VARCHAR(10),ModifiedDate, 21) ModifiedDate, ModifiedUserID, ");
            sbSql.AppendLine("     (SELECT     EmployeeName ");
            sbSql.AppendLine("     FROM          officedba.EmployeeInfo AS e ");
            sbSql.AppendLine("     WHERE      (ID = sb.Borrower)) AS BorrowerText, ");
            sbSql.AppendLine("     (SELECT     EmployeeName ");
            sbSql.AppendLine("     FROM          officedba.EmployeeInfo AS EI ");
            sbSql.AppendLine("     WHERE      (ID = sb.Transactor)) AS OuterText, ");
            sbSql.AppendLine("     (SELECT     EmployeeName ");
            sbSql.AppendLine("     FROM          officedba.EmployeeInfo AS ec ");
            sbSql.AppendLine("     WHERE      (ID = sb.Confirmor)) AS ConfirmorText, ");
            sbSql.AppendLine("     (SELECT     EmployeeName ");
            sbSql.AppendLine("     FROM          officedba.EmployeeInfo AS el ");
            sbSql.AppendLine("     WHERE      (ID = sb.Closer)) AS CloserText, ");
            sbSql.AppendLine("     (SELECT     DeptName ");
            sbSql.AppendLine("     FROM          officedba.DeptInfo AS di1 ");
            sbSql.AppendLine("     WHERE      (ID = sb.DeptID)) AS DeptName, ");
            sbSql.AppendLine("      (SELECT     DeptName ");
            sbSql.AppendLine("      FROM          officedba.DeptInfo AS di2 ");
            sbSql.AppendLine("      WHERE      (ID = sb.OutDeptID)) AS OutDeptName, ");
            sbSql.AppendLine("      (SELECT     CodeName ");
            sbSql.AppendLine("      FROM          officedba.CodeReasonType AS crt ");
            sbSql.AppendLine("      WHERE      (ID = sb.ReasonType)) AS Reason, ");
            sbSql.AppendLine("      (SELECT     StorageName ");
            sbSql.AppendLine("      FROM          officedba.StorageInfo AS si ");
            sbSql.AppendLine("      WHERE      (ID = sb.StorageID)) AS SotorageName,  ");
            sbSql.AppendLine("      (CASE sb.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '4' THEN '手工结单' WHEN '5' THEN '自动结单' END) AS BillStatusText ");
            sbSql.AppendLine("      FROM         officedba.StorageBorrow AS sb ");
            sbSql.AppendLine("      WHERE ID=@ID ");

            SqlParameter[] Para = { new SqlParameter("@ID", SqlDbType.Int) };
            Para[0].Value = ID;

            return SqlHelper.ExecuteSql(sbSql.ToString(), Para);

        }


        #endregion

        #region 取消确认 -->单据状态重置为初始 回写数据
        public static bool CancelConfirm(StorageBorrow borrow)
        {
            List<SqlCommand> SqlList = new List<SqlCommand>();

            #region 重置单据为初始状态
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageBorrow set ");
            strSql.Append("BillStatus=@BillStatus,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = { 
                    new SqlParameter("@ID", SqlDbType.Int), 
                    new SqlParameter("@BillStatus", SqlDbType.Char, 1), 
                    new SqlParameter("@ModifiedDate", SqlDbType.DateTime), 
                    new SqlParameter("@ModifiedUserID", SqlDbType.VarChar, 50),
                                            };
            parameters[0].Value = borrow.ID;
            parameters[1].Value = borrow.BillStatus;
            parameters[2].Value = borrow.ModifiedDate;
            parameters[3].Value = borrow.ModifiedUserID;
            #endregion

            SqlList.Add(SqlHelper.GetNewSqlCommond(strSql.ToString(), parameters));

            #region 查询单据明细
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("SELECT * FROM officedba.StorageBorrowDetail  ");
            sbQuery.Append(" WHERE CompanyCD=@CompanyCD AND BorrowNo=@BorrowNo ");
            SqlParameter[] Para ={
                                    new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                    new SqlParameter("@BorrowNo",SqlDbType.VarChar)
                                };
            Para[0].Value = borrow.CompanyCD;
            Para[1].Value = borrow.BorrowNo;
            DataTable dtDetail = SqlHelper.ExecuteSql(sbQuery.ToString(), Para);


            #endregion

            #region 回写库存数据
            foreach (DataRow row in dtDetail.Rows)
            {
                #region 构造库存移动SQL字符串

                bool isBatch = false;
                if (row["BatchNo"] != null || row["BatchNo"].ToString().ToLower() != "nobatch" || row["BatchNo"].ToString() != "")
                    isBatch = true;
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("UPDATE officedba.StorageProduct SET  ");
                sbSql.Append(" OutCount=OutCount-@BorrowCount");
                //借出量（减少） 
                sbSql.Append(" WHERE CompanyCD=@CompanyCD AND ProductID=@ProductID  AND StorageID=@StorageID " + (isBatch ? " AND BatchNo=@BatchNo " : string.Empty));

                SqlParameter[] sqlParams = isBatch ? new SqlParameter[5] : new SqlParameter[4];
                int index = 0;
                sqlParams[index++] = SqlHelper.GetParameter("@BorrowCount", row["ProductCount"].ToString());
                sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", row["CompanyCD"].ToString());
                sqlParams[index++] = SqlHelper.GetParameter("@ProductID", row["ProductID"].ToString());
                sqlParams[index++] = SqlHelper.GetParameter("@StorageID", borrow.StorageID);
                if (isBatch)
                    sqlParams[index++] = SqlHelper.GetParameter("@BatchNo", row["BatchNo"].ToString());


                SqlList.Add(SqlHelper.GetNewSqlCommond(sbSql.ToString(), sqlParams));
                #endregion
            }
            #endregion

            IList<SqlCommand> tempList = Data.Common.FlowDBHelper.GetCancelConfirmSqlCommond(borrow.CompanyCD, Convert.ToInt32(ConstUtil.BILL_TYPEFLAG_STORAGE), Convert.ToInt32(ConstUtil.BILL_TYPECODE_STORAGE_BORROW), borrow.ID, borrow.ModifiedUserID);
            foreach (SqlCommand scmd in tempList)
            {
                SqlList.Add(scmd);
            }

            return SqlHelper.ExecuteTransWithCollections(SqlList);



        }
        #endregion

        #region 删除明细 已不用
        public static bool DelStorageDetails(string[] IDList)
        {
            List<SqlCommand> SqlList = new List<SqlCommand>();
            foreach (string s in IDList)
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append(" delete officedba.StorageBorrowDetail where ID=@ID");
                SqlParameter[] Paras = { new SqlParameter("@ID", SqlDbType.Int) };
                Paras[0].Value = s;
                SqlList.Add(SqlHelper.GetNewSqlCommond(sbSql.ToString(), Paras));
            }

            return SqlHelper.ExecuteTransWithCollections(SqlList);

        }
        #endregion

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(StorageBorrow model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.StorageBorrow set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND BorrowNo = @BorrowNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@BorrowNo", model.BorrowNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion
    }
}

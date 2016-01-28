using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using XBase.Data.DBHelper;
using XBase.Common;
using XBase.Model.Office.StorageManager;
namespace XBase.Data.Office.StorageManager
{
    public class StorageTransferDBHelper
    {
        #region 读取产品
        public static DataTable GetProduct(string CompanyCD, int OutDeptID, int OutStorageID, string ProductName, string ProdNo)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT   isnull(officedba.ProductInfo.TransferPrice,0) as TransferPrice ,officedba.ProductInfo.ID AS ProductID, officedba.ProductInfo.ProdNo, officedba.ProductInfo.ProductName, officedba.CodeUnitType.CodeName, (isnull(officedba.StorageProduct.ProductCount,0)) as ProductCount, isnull(officedba.StorageProduct.ProductCount,0) as NowCount, isnull(officedba.ProductInfo.StandardCost,0) AS StandardCost, officedba.ProductInfo.Specification, officedba.ProductInfo.ID,officedba.CodeUnitType.ID  AS UnitID,officedba.ProductInfo.CompanyCD FROM  officedba.ProductInfo INNER JOIN officedba.CodeUnitType ON officedba.ProductInfo.UnitID = officedba.CodeUnitType.ID INNER JOIN officedba.StorageProduct ON officedba.ProductInfo.ID = officedba.StorageProduct.ProductID ");
            sbSql.Append(" WHERE officedba.ProductInfo.CompanyCD=officedba.StorageProduct.CompanyCD AND  officedba.ProductInfo.CompanyCD=@CompanyCD AND officedba.StorageProduct.StorageID=@StorageID");

            int length = 2;
            if (!string.IsNullOrEmpty(ProductName))
                length++;
            if (!string.IsNullOrEmpty(ProdNo))
                length++;
            SqlParameter[] Paras = new SqlParameter[length];
            int index = 0;
            Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[index++].Value = CompanyCD;

            Paras[index] = new SqlParameter("@StorageID", SqlDbType.Int);
            Paras[index++].Value = OutStorageID;

            if (!string.IsNullOrEmpty(ProductName))
            {
                ProductName = "%" + ProductName + "%";
                sbSql.Append(" AND officedba.ProductInfo.ProductName LIKE @ProductName ");
                Paras[index] = new SqlParameter("@ProductName", SqlDbType.VarChar);
                Paras[index++].Value = ProductName;
            }
            if (!string.IsNullOrEmpty(ProdNo))
            {
                ProdNo = "%" + ProdNo + "%";
                sbSql.Append(" AND officedba.ProductInfo.ProdNo LIKE @ProdNo ");
                Paras[index] = new SqlParameter("@ProdNo", SqlDbType.VarChar);
                Paras[index++].Value = ProdNo;
            }

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);

        }


        public static DataTable GetProduct(string CompanyCD, int OutDeptID, int OutStorageID, string ProductName, string ProdNo, string EFIndex, string EFDesc)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT   isnull(A.TransferPrice,0) as TransferPrice ,C.BatchNo,A.ID AS ProductID, A.ProdNo, A.ProductName, B.CodeName, ");
            sbSql.Append("(isnull(C.ProductCount,0)) as ProductCount, isnull(C.ProductCount,0) as NowCount, ");
            sbSql.Append("isnull(A.StandardCost,0) AS StandardCost, A.Specification,A.ID,B.ID AS UnitID,A.CompanyCD, ");
            sbSql.Append("ISNULL(A.IsBatchNo,'')IsBatchNo FROM  officedba.ProductInfo A  ");
            sbSql.Append("left JOIN officedba.CodeUnitType B ON A.UnitID = B.ID  ");
            sbSql.Append("LEFT JOIN  ");
            sbSql.Append("( ");
            sbSql.Append("SELECT SUM(ProductCount) AS ProductCount,ProductID,StorageID,BatchNo FROM officedba.StorageProduct  ");
            sbSql.Append("WHERE CompanyCD=@CompanyCD  ");
            sbSql.Append("GROUP BY ProductID,StorageID,BatchNo ");
            sbSql.Append(") AS C ON c.ProductID=A.ID  ");
            sbSql.Append("WHERE  A.CompanyCD=@CompanyCD AND C.StorageID=@StorageID ");

            int length = 2;
            if (!string.IsNullOrEmpty(ProductName))
                length++;
            if (!string.IsNullOrEmpty(ProdNo))
                length++;
            if (EFIndex != "-1" && !string.IsNullOrEmpty(EFDesc))
                length++;
            SqlParameter[] Paras = new SqlParameter[length];
            int index = 0;
            Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[index++].Value = CompanyCD;

            Paras[index] = new SqlParameter("@StorageID", SqlDbType.Int);
            Paras[index++].Value = OutStorageID;

            if (!string.IsNullOrEmpty(ProductName))
            {
                ProductName = "%" + ProductName + "%";
                sbSql.Append(" AND A.ProductName LIKE @ProductName ");
                Paras[index] = new SqlParameter("@ProductName", SqlDbType.VarChar);
                Paras[index++].Value = ProductName;
            }
            if (!string.IsNullOrEmpty(ProdNo))
            {
                ProdNo = "%" + ProdNo + "%";
                sbSql.Append(" AND A.ProdNo LIKE @ProdNo ");
                Paras[index] = new SqlParameter("@ProdNo", SqlDbType.VarChar);
                Paras[index++].Value = ProdNo;
            }
            if (EFIndex != "-1" && !string.IsNullOrEmpty(EFDesc))
            {
                EFDesc = "%" + EFDesc + "%";
                sbSql.Append("	AND A.ExtField" + EFIndex + " LIKE @EFDesc ");
                Paras[index] = new SqlParameter("@EFDesc", SqlDbType.VarChar);
                Paras[index++].Value = EFDesc;
            }
            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);

        }
        #endregion

        #region 添加调拨单 及其明细
        public static string AddStorageTransfer(StorageTransfer st, List<StorageTransferDetail> stdList, Hashtable ht)
        {

            #region 构造SQL字符串
            StringBuilder strMainSql = new StringBuilder();
            strMainSql.Append("insert into officedba.StorageTransfer(");
            strMainSql.Append("CompanyCD,TransferNo,Title,ReasonType,InStorageID,OutStorageID,ApplyUserID,ApplyDeptID,OutDeptID,RequireInDate,TransferPrice,TransferFeeSum,TransferCount,BusiStatus,Remark,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID,Summary)");
            strMainSql.Append(" values (");
            strMainSql.Append("@CompanyCD,@TransferNo,@Title,@ReasonType,@InStorageID,@OutStorageID,@ApplyUserID,@ApplyDeptID,@OutDeptID,@RequireInDate,@TransferPrice,@TransferFeeSum,@TransferCount,@BusiStatus,@Remark,@Creator,@CreateDate,@BillStatus,@ModifiedDate,@ModifiedUserID,@Summary)");
            strMainSql.Append(";select @ID=@@IDENTITY");
            SqlParameter[] MainParas = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@TransferNo", SqlDbType.VarChar,50),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@ReasonType", SqlDbType.Int,4),
					new SqlParameter("@InStorageID", SqlDbType.Int,4),
					new SqlParameter("@OutStorageID", SqlDbType.Int,4),
					new SqlParameter("@ApplyUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplyDeptID", SqlDbType.Int,4),
					new SqlParameter("@OutDeptID", SqlDbType.Int,4),
					new SqlParameter("@RequireInDate", SqlDbType.DateTime),
					new SqlParameter("@TransferPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@TransferFeeSum",SqlDbType.Decimal,9),
					new SqlParameter("@TransferCount", SqlDbType.Decimal,9),
					new SqlParameter("@BusiStatus", SqlDbType.Char,1),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@Summary",SqlDbType.VarChar),
                    new SqlParameter("@ID",SqlDbType.Int)};
            MainParas[0].Value = st.CompanyCD;
            MainParas[1].Value = st.TransferNo;
            MainParas[2].Value = st.Title;
            MainParas[3].Value = st.ReasonType;
            MainParas[4].Value = st.InStorageID;
            MainParas[5].Value = st.OutStorageID;
            MainParas[6].Value = st.ApplyUserID;
            MainParas[7].Value = st.ApplyDeptID;
            MainParas[8].Value = st.OutDeptID;
            MainParas[9].Value = st.RequireInDate;
            MainParas[10].Value = st.TransferPrice;
            MainParas[11].Value = st.TransferFeeSum;
            MainParas[12].Value = st.TransferCount;
            MainParas[13].Value = st.BusiStatus;
            MainParas[14].Value = st.Remark;
            MainParas[15].Value = st.Creator;
            MainParas[16].Value = st.CreateDate;
            MainParas[17].Value = st.BillStatus;
            MainParas[18].Value = st.ModifiedDate;
            MainParas[19].Value = st.ModifiedUserID;
            MainParas[20].Value = st.Summary;
            MainParas[21].Direction = ParameterDirection.Output;

            #endregion

            SqlCommand sqlMainCmd = new SqlCommand();
            sqlMainCmd.CommandText = strMainSql.ToString();
            sqlMainCmd.Parameters.AddRange(MainParas);
            ArrayList SqlCmdList = new ArrayList();
            SqlCmdList.Add(sqlMainCmd);
            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(st, ht, cmd);
            if (ht.Count > 0)
                SqlCmdList.Add(cmd);
            #endregion
            #region 构造明细SQL字符串
            foreach (StorageTransferDetail std in stdList)
            {
                StringBuilder strSubSql = new StringBuilder();
                strSubSql.Append("insert into officedba.StorageTransferDetail(");
                strSubSql.Append("CompanyCD,TransferNo,SortNo,ProductID,UnitID,TranCount,TranPrice,TranPriceTotal,Remark,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSubSql.Append(" values (");
                strSubSql.Append("@CompanyCD,@TransferNo,@SortNo,@ProductID,@UnitID,@TranCount,@TranPrice,@TranPriceTotal,@Remark,@ModifiedDate,@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@TransferNo", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@UnitID", SqlDbType.Int,4),
					new SqlParameter("@TranCount", SqlDbType.Decimal,9),
					new SqlParameter("@TranPrice", SqlDbType.Decimal,9),
					new SqlParameter("@TranPriceTotal", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@UsedUnitID", SqlDbType.Int,4),
					new SqlParameter("@UsedUnitCount", SqlDbType.Decimal,9),
					new SqlParameter("@UsedPrice", SqlDbType.Decimal,9),
					new SqlParameter("@ExRate", SqlDbType.Decimal,9),
                    new SqlParameter("@BatchNo",SqlDbType.VarChar)};
                parameters[0].Value = std.CompanyCD;
                parameters[1].Value = std.TransferNo;
                parameters[2].Value = std.SortNo;
                parameters[3].Value = std.ProductID;
                if (std.UnitID != null)
                    parameters[4].Value = std.UnitID;
                else
                    parameters[4].Value = DBNull.Value;
                parameters[5].Value = std.TranCount;
                parameters[6].Value = std.TranPrice;
                parameters[7].Value = std.TranPriceTotal;
                parameters[8].Value = std.Remark;
                parameters[9].Value = std.ModifiedDate;
                parameters[10].Value = std.ModifiedUserID;
                parameters[11].Value = std.UsedUnitID;
                parameters[12].Value = std.UsedUnitCount;
                parameters[13].Value = std.UsedPrice;
                parameters[14].Value = std.ExRate;
                parameters[15].Value = std.BatchNo;

                SqlCommand sqlSubCmd = new SqlCommand();
                sqlSubCmd.Parameters.AddRange(parameters);
                sqlSubCmd.CommandText = strSubSql.ToString();
                SqlCmdList.Add(sqlSubCmd);

            }
            #endregion

            bool result = SqlHelper.ExecuteTransWithArrayList(SqlCmdList);
            if (result)
            {
                return "1|" + ((SqlCommand)SqlCmdList[0]).Parameters["@ID"].Value.ToString() + "#" + st.TransferNo;
            }
            else
                return "3|保存数据失败";

        }
        #endregion

        #region 更新调拨单 及其明细
        public static string UpdateStorageTransfer(StorageTransfer st, List<StorageTransferDetail> stdList, Hashtable ht)
        {
            #region 构造调拨单SQL字符串

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageTransfer set ");
            strSql.Append("Title=@Title,");
            strSql.Append("ReasonType=@ReasonType,");
            strSql.Append("InStorageID=@InStorageID,");
            strSql.Append("OutStorageID=@OutStorageID,");
            strSql.Append("ApplyUserID=@ApplyUserID,");
            strSql.Append("ApplyDeptID=@ApplyDeptID,");
            strSql.Append("OutDeptID=@OutDeptID,");
            strSql.Append("RequireInDate=@RequireInDate,");
            strSql.Append("TransferCount=@TransferCount,");
            strSql.Append("TransferFeeSum=@TransferFeeSum,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(",Summary=@Summary ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@ReasonType", SqlDbType.Int,4),
					new SqlParameter("@InStorageID", SqlDbType.Int,4),
					new SqlParameter("@OutStorageID", SqlDbType.Int,4),
					new SqlParameter("@ApplyUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplyDeptID", SqlDbType.Int,4),
					new SqlParameter("@OutDeptID", SqlDbType.Int,4),
					new SqlParameter("@RequireInDate", SqlDbType.DateTime),
					new SqlParameter("@TransferPrice", SqlDbType.Decimal,9),
					new SqlParameter("@TransferCount", SqlDbType.Decimal,9),
                    new SqlParameter("@TransferFeeSum",SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@Summary",SqlDbType.VarChar)
                                        };
            parameters[0].Value = st.ID;
            parameters[1].Value = st.Title;
            parameters[2].Value = st.ReasonType;
            parameters[3].Value = st.InStorageID;
            parameters[4].Value = st.OutStorageID;
            parameters[5].Value = st.ApplyUserID;
            parameters[6].Value = st.ApplyDeptID;
            parameters[7].Value = st.OutDeptID;
            parameters[8].Value = st.RequireInDate;
            parameters[9].Value = st.TransferPrice;
            parameters[10].Value = st.TransferCount;
            parameters[11].Value = st.TransferFeeSum;
            parameters[12].Value = st.Remark;
            parameters[13].Value = st.ModifiedDate;
            parameters[14].Value = st.ModifiedUserID;
            parameters[15].Value = st.Summary;

            #endregion

            SqlCommand SqlMainCmd = new SqlCommand();
            SqlMainCmd.CommandText = strSql.ToString();
            SqlMainCmd.Parameters.AddRange(parameters);
            ArrayList SqlCmdList = new ArrayList();
            SqlCmdList.Add(SqlMainCmd);
            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(st, ht, cmd);
            if (ht.Count > 0)
                SqlCmdList.Add(cmd);
            #endregion
            #region 构造调拨单明细SQL字符串
            StringBuilder sbDel = new StringBuilder();
            sbDel.Append("DELETE officedba.StorageTransferDetail WHERE TransferNo=@TransferNo AND CompanyCD=@CompanyCD");
            SqlParameter[] Paras = { 
                                       new SqlParameter("@TransferNo",SqlDbType.VarChar),
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar)
                                   };
            Paras[0].Value = st.TransferNo;
            Paras[1].Value = st.CompanyCD;

            SqlCommand sqlDelCmd = new SqlCommand();
            sqlDelCmd.CommandText = sbDel.ToString();
            sqlDelCmd.Parameters.AddRange(Paras);
            SqlCmdList.Add(sqlDelCmd);

            foreach (StorageTransferDetail std in stdList)
            {
                StringBuilder strSubSql = new StringBuilder();
                strSubSql.Append("insert into officedba.StorageTransferDetail(");
                strSubSql.Append("CompanyCD,TransferNo,SortNo,ProductID,UnitID,TranCount,TranPrice,TranPriceTotal,Remark,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSubSql.Append(" values (");
                strSubSql.Append("@CompanyCD,@TransferNo,@SortNo,@ProductID,@UnitID,@TranCount,@TranPrice,@TranPriceTotal,@Remark,@ModifiedDate,@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                SqlParameter[] subparas = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@TransferNo", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@UnitID", SqlDbType.Int,4),
					new SqlParameter("@TranCount", SqlDbType.Decimal,9),
					new SqlParameter("@TranPrice", SqlDbType.Decimal,9),
					new SqlParameter("@TranPriceTotal", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@UsedUnitID", SqlDbType.Int,4),
					new SqlParameter("@UsedUnitCount", SqlDbType.Decimal,9),
					new SqlParameter("@UsedPrice", SqlDbType.Decimal,9),
					new SqlParameter("@ExRate", SqlDbType.Decimal,9),
                    new SqlParameter("@BatchNo",SqlDbType.VarChar)};
                subparas[0].Value = std.CompanyCD;
                subparas[1].Value = std.TransferNo;
                subparas[2].Value = std.SortNo;
                subparas[3].Value = std.ProductID;
                if (std.UnitID != null)
                    subparas[4].Value = std.UnitID;
                else
                    subparas[4].Value = DBNull.Value;
                subparas[5].Value = std.TranCount;
                subparas[6].Value = std.TranPrice;
                subparas[7].Value = std.TranPriceTotal;
                subparas[8].Value = std.Remark;
                subparas[9].Value = std.ModifiedDate;
                subparas[10].Value = std.ModifiedUserID;
                subparas[11].Value = std.UsedUnitID;
                subparas[12].Value = std.UsedUnitCount;
                subparas[13].Value = std.UsedPrice;
                subparas[14].Value = std.ExRate;
                subparas[15].Value = std.BatchNo;

                SqlCommand sqlSubCmd = new SqlCommand();
                sqlSubCmd.Parameters.AddRange(subparas);
                sqlSubCmd.CommandText = strSubSql.ToString();
                SqlCmdList.Add(sqlSubCmd);

            }



            #endregion

            bool result = SqlHelper.ExecuteTransWithArrayList(SqlCmdList);
            if (result)
                return "1|";
            else
                return "3|";


        }
        #endregion

        #region 更新状态
        public static string UpdateStorageTransfer(int stype, Model.Office.StorageManager.StorageTransfer st)
        {
            if (stype == 1) //确认订单 及修改订单业务状态
            {
                #region 确认单据
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("UPDATE officedba.StorageTransfer SET ");
                sbSql.Append("BusiStatus=@BusiStatus, ");
                sbSql.Append("BillStatus=@BillStatus, ");
                sbSql.Append("Confirmor=@Confirmor,");
                sbSql.Append("ConfirmDate=@ConfirmDate,");
                sbSql.Append("ModifiedDate=@ModifiedDate,");
                sbSql.Append("ModifiedUserID=@ModifiedUserID");
                sbSql.Append("  WHERE ID=@ID");
                SqlParameter[] Paras = { 
                                           new SqlParameter("@BusiStatus",SqlDbType.VarChar),
                                           new SqlParameter("@BillStatus",SqlDbType.VarChar),
                                           new SqlParameter("@Confirmor",SqlDbType.Int),
                                           new SqlParameter("@ConfirmDate",SqlDbType.DateTime),
                                           new SqlParameter("@ModifiedDate",SqlDbType.DateTime),
                                           new SqlParameter("@ModifiedUserID",SqlDbType.VarChar),
                                           new SqlParameter("@ID",SqlDbType.Int)
                                       };
                Paras[0].Value = st.BusiStatus;
                Paras[1].Value = st.BillStatus;
                Paras[2].Value = st.Confirmor;
                Paras[3].Value = st.ConfirmDate;
                Paras[4].Value = st.ModifiedDate;
                Paras[5].Value = st.ModifiedUserID;
                Paras[6].Value = st.ID;

                if (SqlHelper.ExecuteTransSql(sbSql.ToString(), Paras) > 0)
                    return "1|单据确认成功";
                else
                    return "4|单据确认失败";
                #endregion
            }
            else if (stype == 3 || stype == 4) //取消结单 和取消确认
            {
                #region 取消结单和取消确认

                List<SqlCommand> SqlCmdList = new List<SqlCommand>();
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("UPDATE officedba.StorageTransfer SET ");
                sbSql.Append("BillStatus=@BillStatus, ");
                if (stype == 4)
                    sbSql.Append("BusiStatus=@BusiStatus,");
                sbSql.Append("ModifiedDate=@ModifiedDate,");
                sbSql.Append("ModifiedUserID=@ModifiedUserID");
                sbSql.Append("  WHERE ID=@ID");

                int length = 0;
                int index = 0;
                if (stype == 4)
                    length = 5;
                else
                    length = 4;

                SqlParameter[] Paras = new SqlParameter[length];
                Paras[index] = new SqlParameter("@BillStatus", SqlDbType.VarChar);
                Paras[index++].Value = st.BillStatus;
                Paras[index] = new SqlParameter("@ModifiedDate", SqlDbType.DateTime);
                Paras[index++].Value = st.ModifiedDate;
                Paras[index] = new SqlParameter("@ModifiedUserID", SqlDbType.VarChar);
                Paras[index++].Value = st.ModifiedUserID;
                Paras[index] = new SqlParameter("@ID", SqlDbType.Int);
                Paras[index++].Value = st.ID;
                if (stype == 4)
                {
                    Paras[index] = new SqlParameter("@BusiStatus", SqlDbType.VarChar);
                    Paras[index++].Value = st.BusiStatus;
                }
                SqlCommand sqlcmd = new SqlCommand() { CommandText = sbSql.ToString() };
                sqlcmd.Parameters.AddRange(Paras);
                SqlCmdList.Add(sqlcmd);
                /*追加取消确认的SqlCommond*/
                if (stype == 4)
                {
                    IList<SqlCommand> tempList = Data.Common.FlowDBHelper.GetCancelConfirmSqlCommond(st.CompanyCD, Convert.ToInt32(ConstUtil.BILL_TYPEFLAG_STORAGE), Convert.ToInt32(ConstUtil.BILL_TYPECODE_STORAGE_TRANSFER), st.ID, st.ModifiedUserID);
                    foreach (SqlCommand scmd in tempList)
                    {
                        SqlCmdList.Add(scmd);
                    }
                }

                if (SqlHelper.ExecuteTransWithCollections(SqlCmdList))
                    return "1|";
                else
                    return "4|";
                #endregion
            }
            else if (stype == 2)
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("UPDATE officedba.StorageTransfer SET ");
                sbSql.Append("BillStatus=@BillStatus, ");
                sbSql.Append("Closer=@Closer,");
                sbSql.Append("CloseDate=@CloseDate,");
                sbSql.Append("ModifiedDate=@ModifiedDate,");
                sbSql.Append("ModifiedUserID=@ModifiedUserID");
                sbSql.Append("  WHERE ID=@ID");
                SqlParameter[] Paras = { 
                                           new SqlParameter("@BillStatus",SqlDbType.VarChar),
                                           new SqlParameter("@Closer",SqlDbType.Int),
                                           new SqlParameter("@CloseDate",SqlDbType.DateTime),
                                           new SqlParameter("@ModifiedDate",SqlDbType.DateTime),
                                           new SqlParameter("@ModifiedUserID",SqlDbType.VarChar),
                                           new SqlParameter("@ID",SqlDbType.Int)
                                       };
                Paras[0].Value = st.BillStatus;
                Paras[1].Value = st.Closer;
                Paras[2].Value = st.CloseDate;
                Paras[3].Value = st.ModifiedDate;
                Paras[4].Value = st.ModifiedUserID;
                Paras[5].Value = st.ID;
                if (SqlHelper.ExecuteTransSql(sbSql.ToString(), Paras) > 0)
                    return "1|结单成功";
                else
                    return "4|结单失败";
            }
            else
            {
                return "4|操作失败";
            }

        }
        #endregion

        #region 读取调拨单
        public static DataTable GetStorageTransferList(string EFIndex, string EFDesc, Hashtable htPara, int PageIndex, int PageSize, string OrderBy, ref int TotalCount)
        {
            string FlowStatusSql = string.Empty;
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(" SELECT * FROM ( ");
            sbSql.Append("SELECT st.ID,st.TransferNo,st.Title,");
            sbSql.Append("(SELECT ei1.EmployeeName from officedba.EmployeeInfo as ei1 where st.ApplyUserID=ei1.ID) AS ApplyUserID,");
            sbSql.Append("(SELECT di1.DeptName from officedba.DeptInfo as di1 where st.ApplyDeptID=di1.ID) AS ApplyDeptID,");
            sbSql.Append("(SELECT si1.StorageName from officedba.StorageInfo as si1 where st.InStorageID=si1.ID) AS InStorageID,");
            sbSql.Append("st.RequireInDate,");
            sbSql.Append("(SELECT di2.DeptName from officedba.DeptInfo as di2 where st.OutDeptID=di2.ID) AS OutDeptID,");
            sbSql.Append("(SELECT si2.StorageName from officedba.StorageInfo as si2 where st.OutStorageID=si2.ID) AS OutStorageID,");
            sbSql.Append("st.OutDate,st.TransferCount,st.TransferPrice,");
            // sbSql.Append("(CASE st.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,(case fi.FlowStatus when '1' then '待审批'  when '2' then '审批中'  when '3' then '审批通过'  when '4' then '审批不通过' when '' then '待审批'  end) as FlowStatus,");
            sbSql.Append("(CASE st.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,( select top 1 fi.FlowStatus   from  officedba.FlowInstance AS FI  where fi.BillID=st.ID  and fi.CompanyCD=st.CompanyCD  AND fi.BillTypeCode=" + ConstUtil.BILL_TYPECODE_STORAGE_TRANSFER + "  AND fi.BillTypeFlag=" + ConstUtil.BILL_TYPEFLAG_STORAGE + " order by FI.ModifiedDate DESC) as FlowStatus,");
            sbSql.Append("(case  st.busiStatus when '1' then '调拨申请' when '2' then '调拨出库' when '3' then  '调拨入库' when '4' then '调拨完成' end) as BusiStatus ");
            sbSql.Append("FROM officedba.StorageTransfer AS ST");
            sbSql.Append("  WHERE  st.CompanyCD=@CompanyCD ");
            string Submit = string.Empty;
            string ConfirmStatus = string.Empty;
            int index = 0;
            int length = htPara.Count;
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sbSql.AppendLine(" and st.ExtField" + EFIndex + " like '%" + EFDesc + "%' ");
            }
            if (htPara.ContainsKey("ConfirmStatus"))
            {
                int SubmitStatus = Convert.ToInt32(htPara["ConfirmStatus"].ToString());
                if (SubmitStatus > 0)
                    Submit = "  AND  FlowStatus=" + htPara["ConfirmStatus"].ToString();
                else if (SubmitStatus == 0)
                    Submit = "  AND FlowStatus is null ";
                length--;
            }


            SqlParameter[] Paras = new SqlParameter[length];

            if (htPara.ContainsKey("TransferNo"))
            {
                sbSql.Append(" AND st.TransferNo LIKE @TransferNo");
                Paras[index] = new SqlParameter("@TransferNo", SqlDbType.VarChar);
                Paras[index].Value = htPara["TransferNo"];
                index++;
            }
            if (htPara.ContainsKey("Title"))
            {
                sbSql.Append(" AND st.Title LIKE @Title");
                Paras[index] = new SqlParameter("@Title", SqlDbType.VarChar);
                Paras[index].Value = htPara["Title"];
                index++;
            }
            if (htPara.ContainsKey("ApplyUserID"))
            {
                sbSql.Append(" AND st.ApplyUserID=@ApplyUserID");
                Paras[index] = new SqlParameter("@ApplyUserID", SqlDbType.Int);
                Paras[index].Value = htPara["ApplyUserID"];
                index++;
            }
            if (htPara.ContainsKey("ApplyDeptID"))
            {
                sbSql.Append(" AND st.ApplyDeptID=@ApplyDeptID");
                Paras[index] = new SqlParameter("@ApplyDeptID", SqlDbType.Int);
                Paras[index].Value = htPara["ApplyDeptID"];
                index++;
            }
            if (htPara.ContainsKey("InStorageID"))
            {
                sbSql.Append(" AND st.InStorageID=@InStorageID");
                Paras[index] = new SqlParameter("@InStorageID", SqlDbType.Int);
                Paras[index].Value = htPara["InStorageID"];
                index++;
            }
            if (htPara.ContainsKey("RequireInDate"))
            {
                sbSql.Append(" AND st.RequireInDate=@RequireInDate");
                Paras[index] = new SqlParameter("@RequireInDate", SqlDbType.DateTime);
                Paras[index].Value = htPara["RequireInDate"];
                index++;
            }
            if (htPara.ContainsKey("OutDate"))
            {
                sbSql.Append(" AND st.OutDate=@OutDate");
                Paras[index] = new SqlParameter("@OutDate", SqlDbType.DateTime);
                Paras[index].Value = htPara["OutDate"];
                index++;
            }
            if (htPara.ContainsKey("OutDeptID"))
            {
                sbSql.Append(" AND st.OutDeptID=@OutDeptID");
                Paras[index] = new SqlParameter("@OutDeptID", SqlDbType.Int);
                Paras[index].Value = htPara["OutDeptID"];
                index++;
            }
            if (htPara.ContainsKey("OutStorageID"))
            {
                sbSql.Append(" AND st.OutStorageID=@OutStorageID");
                Paras[index] = new SqlParameter("OutStorageID", SqlDbType.Int);
                Paras[index].Value = htPara["OutStorageID"];
                index++;
            }
            if (htPara.ContainsKey("BusiStatus"))
            {
                sbSql.Append(" AND st.BusiStatus=@BusiStatus");
                Paras[index] = new SqlParameter("@BusiStatus", SqlDbType.VarChar);
                Paras[index].Value = htPara["BusiStatus"];
                index++;
            }
            if (htPara.ContainsKey("BillStatus"))
            {
                sbSql.Append(" AND st.BillStatus=@BillStatus");
                Paras[index] = new SqlParameter("@BillStatus", SqlDbType.VarChar);
                Paras[index].Value = htPara["BillStatus"];
                index++;
            }
            Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[index].Value = htPara["CompanyCD"];

            sbSql.Append(" )  as tempt  where 1=1" + Submit);

            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, Paras, ref TotalCount);

        }

        /*不分页*/
        public static DataTable GetStorageTransferList(Hashtable htPara, string OrderBy)
        {
            string FlowStatusSql = string.Empty;
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(" SELECT * FROM ( ");
            sbSql.Append("SELECT st.ID,st.TransferNo,st.Title,");
            sbSql.Append("(SELECT ei1.EmployeeName from officedba.EmployeeInfo as ei1 where st.ApplyUserID=ei1.ID) AS ApplyUserID,");
            sbSql.Append("(SELECT di1.DeptName from officedba.DeptInfo as di1 where st.ApplyDeptID=di1.ID) AS ApplyDeptID,");
            sbSql.Append("(SELECT si1.StorageName from officedba.StorageInfo as si1 where st.InStorageID=si1.ID) AS InStorageID,");
            sbSql.Append("st.RequireInDate,");
            sbSql.Append("(SELECT di2.DeptName from officedba.DeptInfo as di2 where st.OutDeptID=di2.ID) AS OutDeptID,");
            sbSql.Append("(SELECT si2.StorageName from officedba.StorageInfo as si2 where st.OutStorageID=si2.ID) AS OutStorageID,");
            sbSql.Append("st.OutDate,st.TransferCount,st.TransferPrice,");
            // sbSql.Append("(CASE st.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,(case fi.FlowStatus when '1' then '待审批'  when '2' then '审批中'  when '3' then '审批通过'  when '4' then '审批不通过' when '' then '待审批'  end) as FlowStatus,");
            sbSql.Append("(CASE st.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,( select top 1 fi.FlowStatus   from  officedba.FlowInstance AS FI  where fi.BillID=st.ID  and fi.CompanyCD=st.CompanyCD  AND fi.BillTypeCode=" + ConstUtil.BILL_TYPECODE_STORAGE_TRANSFER + "  AND fi.BillTypeFlag=" + ConstUtil.BILL_TYPEFLAG_STORAGE + " order by FI.ModifiedDate DESC) as FlowStatus,");
            sbSql.Append("(case  st.busiStatus when '1' then '调拨申请' when '2' then '调拨出库' when '3' then  '调拨入库' when '4' then '调拨完成' end) as BusiStatus ");
            sbSql.Append("FROM officedba.StorageTransfer AS ST");
            sbSql.Append("  WHERE  st.CompanyCD=@CompanyCD ");
            string Submit = string.Empty;
            string ConfirmStatus = string.Empty;
            int index = 0;
            int length = htPara.Count;
            if (htPara.ContainsKey("ConfirmStatus"))
            {
                int SubmitStatus = Convert.ToInt32(htPara["ConfirmStatus"].ToString());
                if (SubmitStatus > 0)
                    Submit = "  AND  FlowStatus=" + htPara["ConfirmStatus"].ToString();
                else if (SubmitStatus == 0)
                    Submit = "  AND FlowStatus is null ";
                length--;
            }


            SqlParameter[] Paras = new SqlParameter[length];

            if (htPara.ContainsKey("TransferNo"))
            {
                sbSql.Append(" AND st.TransferNo LIKE @TransferNo");
                Paras[index] = new SqlParameter("@TransferNo", SqlDbType.VarChar);
                Paras[index].Value = htPara["TransferNo"];
                index++;
            }
            if (htPara.ContainsKey("Title"))
            {
                sbSql.Append(" AND st.Title LIKE @Title");
                Paras[index] = new SqlParameter("@Title", SqlDbType.VarChar);
                Paras[index].Value = htPara["Title"];
                index++;
            }
            if (htPara.ContainsKey("ApplyUserID"))
            {
                sbSql.Append(" AND st.ApplyUserID=@ApplyUserID");
                Paras[index] = new SqlParameter("@ApplyUserID", SqlDbType.Int);
                Paras[index].Value = htPara["ApplyUserID"];
                index++;
            }
            if (htPara.ContainsKey("ApplyDeptID"))
            {
                sbSql.Append(" AND st.ApplyDeptID=@ApplyDeptID");
                Paras[index] = new SqlParameter("@ApplyDeptID", SqlDbType.Int);
                Paras[index].Value = htPara["ApplyDeptID"];
                index++;
            }
            if (htPara.ContainsKey("InStorageID"))
            {
                sbSql.Append(" AND st.InStorageID=@InStorageID");
                Paras[index] = new SqlParameter("@InStorageID", SqlDbType.Int);
                Paras[index].Value = htPara["InStorageID"];
                index++;
            }
            if (htPara.ContainsKey("InStorageIDS"))
            {
                sbSql.Append(" AND st.InStorageID in(" + htPara["InStorageIDS"] + ")");
                Paras[index] = new SqlParameter("@InStorageIDS", SqlDbType.VarChar);
                Paras[index].Value = htPara["InStorageIDS"];
                index++;
            }
            if (htPara.ContainsKey("RequireInDate"))
            {
                sbSql.Append(" AND st.RequireInDate=@RequireInDate");
                Paras[index] = new SqlParameter("@RequireInDate", SqlDbType.DateTime);
                Paras[index].Value = htPara["RequireInDate"];
                index++;
            }
            if (htPara.ContainsKey("OutDate"))
            {
                sbSql.Append(" AND st.OutDate=@OutDate");
                Paras[index] = new SqlParameter("@OutDate", SqlDbType.DateTime);
                Paras[index].Value = htPara["OutDate"];
                index++;
            }
            if (htPara.ContainsKey("OutDeptID"))
            {
                sbSql.Append(" AND st.OutDeptID=@OutDeptID");
                Paras[index] = new SqlParameter("@OutDeptID", SqlDbType.Int);
                Paras[index].Value = htPara["OutDeptID"];
                index++;
            }
            if (htPara.ContainsKey("OutStorageID"))
            {
                sbSql.Append(" AND st.OutStorageID=@OutStorageID");
                Paras[index] = new SqlParameter("OutStorageID", SqlDbType.Int);
                Paras[index].Value = htPara["OutStorageID"];
                index++;
            }
            if (htPara.ContainsKey("OutStorageIDS"))
            {
                sbSql.Append(" AND st.OutStorageID in(" + htPara["OutStorageIDS"] + ")");
                Paras[index] = new SqlParameter("@OutStorageIDS", SqlDbType.VarChar);
                Paras[index].Value = htPara["OutStorageIDS"];
                index++;
            }
            if (htPara.ContainsKey("BusiStatus"))
            {
                sbSql.Append(" AND st.BusiStatus=@BusiStatus");
                Paras[index] = new SqlParameter("@BusiStatus", SqlDbType.VarChar);
                Paras[index].Value = htPara["BusiStatus"];
                index++;
            }
            if (htPara.ContainsKey("BillStatus"))
            {
                sbSql.Append(" AND st.BillStatus=@BillStatus");
                Paras[index] = new SqlParameter("@BillStatus", SqlDbType.VarChar);
                Paras[index].Value = htPara["BillStatus"];
                index++;
            }
            Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[index].Value = htPara["CompanyCD"];

            sbSql.Append(" )  as tempt  where 1=1" + Submit);

            sbSql.Append("  ORDER BY " + OrderBy);
            //return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, Paras, ref TotalCount);
            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
        }

        #endregion

        #region 删除调拨单 及其明细
        public static bool DelStorageTransfer(string[] ID)
        {
            bool Flag = true;

            foreach (string tempID in ID)
            {
                StringBuilder strMainSql = new StringBuilder();
                strMainSql.Append("DELETE officedba.StorageTransferDetail WHERE CompanyCD=(SELECT st1.CompanyCD FROM  officedba.StorageTransfer as st1  where st1.ID=@ID) AND TransferNo=(SELECT st2.TransferNo FROM officedba.StorageTransfer as st2 WHERE st2.ID=@ID)");
                SqlParameter[] MainParas ={
                                              new SqlParameter("@ID",SqlDbType.Int)
                                         };
                MainParas[0].Value = Convert.ToInt32(tempID);
                SqlCommand MainSqlCmd = new SqlCommand();
                MainSqlCmd.CommandText = strMainSql.ToString();
                MainSqlCmd.Parameters.AddRange(MainParas);
                ArrayList SqlCmdList = new ArrayList();
                SqlCmdList.Add(MainSqlCmd);


                StringBuilder strSubSql = new StringBuilder();
                strSubSql.Append("DELETE officedba.StorageTransfer WHERE ID=@ID");
                SqlParameter[] SubParas = { 
                                              new SqlParameter("@ID",SqlDbType.Int)
                                          };
                SubParas[0].Value = Convert.ToInt32(tempID);
                SqlCommand SubSqlCmd = new SqlCommand();
                SubSqlCmd.Parameters.AddRange(SubParas);
                SubSqlCmd.CommandText = strSubSql.ToString();
                SqlCmdList.Add(SubSqlCmd);

                bool result = SqlHelper.ExecuteTransWithArrayList(SqlCmdList);

                if (!result)
                    Flag = false;
            }
            return Flag;
        }
        #endregion

        #region 获取调拨单信息
        public static DataTable GetStorageTransferInfo(StorageTransfer st)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select st.ID,st.TransferNo,st.Title,st.ApplyUserID,");
            sbSql.Append("(SELECT ei1.EmployeeName from officedba.EmployeeInfo as ei1 where st.ApplyUserID=ei1.ID) AS ApplyUserIDName,");
            sbSql.Append("st.ApplyDeptID,");
            sbSql.Append("(SELECT di1.DeptName from officedba.DeptInfo as di1 where st.ApplyDeptID=di1.ID) AS ApplyDeptIDName,");
            sbSql.Append("st.InStorageID,st.RequireInDate,st.ReasonType,");
            sbSql.Append("st.OutDeptID,");
            sbSql.Append("(SELECT di2.DeptName from officedba.DeptInfo as di2 where st.OutDeptID=di2.ID) AS OutDeptIDName,");
            sbSql.Append("st.OutStorageID,st.OutDate,st.BusiStatus,st.Summary,st.TransferCount,st.TransferPrice,st.TransferFeeSum,");
            sbSql.Append("st.Creator,");
            sbSql.Append("(SELECT ei2.EmployeeName from officedba.EmployeeInfo as ei2 where st.Creator=ei2.ID) as CreatorName,");
            sbSql.Append("st.CreateDate,st.BillStatus,st.Remark,");
            sbSql.Append("(SELECT ei3.EmployeeName from officedba.EmployeeInfo as ei3 where st.Confirmor=ei3.ID) as ConfirmorName,");
            sbSql.Append("st.ConfirmDate,st.Closer,");
            sbSql.Append("(SELECT ei4.EmployeeName from officedba.EmployeeInfo as ei4 where st.Closer=ei4.ID) as CloserName,");
            sbSql.Append("st.CloseDate,st.ModifiedDate,st.ModifiedUserID,st.OutFeeSum,st.InFeeSum,st.OutCount, st.InCount,st.OutUserID,");
            sbSql.Append("(SELECT ei5.EmployeeName from officedba.EmployeeInfo as ei5 where st.OutUserID=ei5.ID) as OutUserIDName,");
            sbSql.Append("st.OutDate,st.InUserID,st.InDate,");
            sbSql.Append("(SELECT ei6.EmployeeName from officedba.EmployeeInfo as ei6 where st.InUserID=ei6.ID) as InUserIDName, ");
            sbSql.Append("isnull(ExtField1,'')ExtField1,isnull(ExtField2,'')ExtField2,isnull(ExtField3,'')ExtField3,isnull(ExtField4,'')ExtField4,");
            sbSql.Append("isnull(ExtField5,'')ExtField5,isnull(ExtField6,''),ExtField6,isnull(ExtField7,'')ExtField7,isnull(ExtField8,'')ExtField8,");
            sbSql.Append("   isnull(ExtField9,'')ExtField9,isnull(ExtField10,'')ExtField10 ");
            sbSql.Append("from officedba.StorageTransfer as st ");
            sbSql.Append(" where ID=@ID");
            SqlParameter[] Paras = { 
                                       new SqlParameter("@ID",SqlDbType.Int)
                                   };
            Paras[0].Value = st.ID;

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
        }

        /*提供打印*/
        public static DataTable GetStorageTransferInfoPrint(StorageTransfer st)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("   ");
            sbSql.Append(" SELECT     ID, TransferNo, Title, ApplyUserID, ");
            sbSql.Append("  (SELECT     DeptName ");
            sbSql.Append("  FROM          officedba.DeptInfo AS di ");
            sbSql.Append("  WHERE      (ID = st.InStorageID)) AS InStorageName, ");
            sbSql.Append("  (SELECT     DeptName ");
            sbSql.Append("  FROM          officedba.DeptInfo AS di1 ");
            sbSql.Append("  WHERE      (ID = st.OutStorageID)) AS OutStorageName,  ");
            sbSql.Append("  (CASE st.BusiStatus WHEN '1' THEN '调拨申请' WHEN '2' THEN '调拨出库' WHEN '3' THEN '调拨入库' WHEN '4' THEN '调拨完成' END)  ");
            sbSql.Append("  AS BusiStatusText, ");
            sbSql.Append("  (SELECT     CodeName ");
            sbSql.Append("  FROM          officedba.CodeReasonType AS crt ");
            sbSql.Append("  WHERE      (ID = st.ReasonType)) AS Reason, ");
            sbSql.Append("  (SELECT     EmployeeName ");
            sbSql.Append("  FROM          officedba.EmployeeInfo AS ei1 ");
            sbSql.Append("  WHERE      (st.ApplyUserID = ID)) AS ApplyUserIDName, ApplyDeptID, ");
            sbSql.Append("  (SELECT     DeptName ");
            sbSql.Append("  FROM          officedba.DeptInfo AS di1 ");
            sbSql.Append("  WHERE      (st.ApplyDeptID = ID)) AS ApplyDeptIDName, InStorageID,CONVERT(VARCHAR(10),RequireInDate, 21)RequireInDate , ReasonType, OutDeptID, ");
            sbSql.Append("  (SELECT     DeptName ");
            sbSql.Append("  FROM          officedba.DeptInfo AS di2 ");
            sbSql.Append("  WHERE      (st.OutDeptID = ID)) AS OutDeptIDName, OutStorageID, CONVERT(VARCHAR(10),OutDate, 21)OutDate, BusiStatus, Summary, TransferCount, TransferPrice, TransferFeeSum,  ");
            sbSql.Append("  Creator, ");
            sbSql.Append("  (SELECT     EmployeeName ");
            sbSql.Append("  FROM          officedba.EmployeeInfo AS ei2 ");
            sbSql.Append("  WHERE      (st.Creator = ID)) AS CreatorName, CONVERT(VARCHAR(10),CreateDate, 21)CreateDate, BillStatus, Remark, ");
            sbSql.Append("  (SELECT     EmployeeName ");
            sbSql.Append("  FROM          officedba.EmployeeInfo AS ei3 ");
            sbSql.Append("  WHERE      (st.Confirmor = ID)) AS ConfirmorName, CONVERT(VARCHAR(10),ConfirmDate, 21)ConfirmDate , Closer, ");
            sbSql.Append("  (SELECT     EmployeeName ");
            sbSql.Append("  FROM          officedba.EmployeeInfo AS ei4 ");
            sbSql.Append("  WHERE      (st.Closer = ID)) AS CloserName, CONVERT(VARCHAR(10),CloseDate, 21)CloseDate , CONVERT(VARCHAR(10),ModifiedDate, 21)ModifiedDate, ModifiedUserID, OutFeeSum, InFeeSum, OutCount, InCount, OutUserID, ");
            sbSql.Append("  (SELECT     EmployeeName ");
            sbSql.Append("  FROM          officedba.EmployeeInfo AS ei5 ");
            sbSql.Append("  WHERE      (st.OutUserID = ID)) AS OutUserIDName, OutDate AS Expr1, InUserID,CONVERT(VARCHAR(10),InDate, 21)InDate , ");
            sbSql.Append("  (SELECT     EmployeeName ");
            sbSql.Append("  FROM          officedba.EmployeeInfo AS ei6 ");
            sbSql.Append("  WHERE      (st.InUserID = ID)) AS InUserIDName,  ");
            sbSql.Append("  (CASE st.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '4' THEN '手工结单' WHEN '5' THEN '自动结单' END)  ");
            sbSql.Append("  AS BillStatusText, ");
            sbSql.Append("isnull(st.ExtField1,'')ExtField1,isnull(st.ExtField2,'')ExtField2,isnull(st.ExtField3,'')ExtField3,isnull(st.ExtField4,'')ExtField4,");
            sbSql.Append("isnull(st.ExtField5,'')ExtField5,isnull(st.ExtField6,''),ExtField6,isnull(st.ExtField7,'')ExtField7,isnull(st.ExtField8,'')ExtField8,");
            sbSql.Append("   isnull(st.ExtField9,'')ExtField9,isnull(st.ExtField10,'')ExtField10 ");
            sbSql.Append("  FROM         officedba.StorageTransfer AS st ");
            sbSql.Append(" where ID=@ID");
            SqlParameter[] Paras = { 
                                       new SqlParameter("@ID",SqlDbType.Int)
                                   };
            Paras[0].Value = st.ID;

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
        }
        #endregion

        #region 读取调拨单明细
        public static DataTable GetStorageTransferDetailInfo(Model.Office.StorageManager.StorageTransferDetail std)
        {

            //bool isBatchNo = false;
            //if (std.BatchNo != null && std.BatchNo != "" && std.BatchNo != string.Empty)
            //    isBatchNo = true;
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select g.CodeName AS UsedUnitName,std.UsedUnitID,std.UsedUnitCount,std.UsedPrice,std.ExRate,std.ID,std.CompanyCD,std.TransferNo,std.SortNo,pi1.ProductName,pi1.ProdNo,pi1.Specification,std.ModifiedDate,std.ModifiedUserID,std.BatchNo,std.OutCount,std.OutPriceTotal,std.InCount,std.InPriceTotal ,");
            sbSql.Append("(SELECT u.CodeName from officedba.CodeUnitType as u where std.UnitID=u.ID) AS UnitName");
            sbSql.Append(",std.TranPrice as TranPrice ,isnull(std.TranCount,0) as TranCount, std.TranPriceTotal as TranPriceTotal,std.Remark,std.ProductID,std.UnitID,pi1.MinusIs,");
            //sbSql.Append(" (SELECT isnull(sp.ProductCount,0) from officedba.StorageProduct as sp  left  join officedba.StorageTransfer as st on sp.StorageID=st.OutStorageID  and sp.CompanyCD=st.CompanyCD  where sp.ProductID=std.ProductID and sp.CompanyCD=std.CompanyCD and st.TransferNo=std.TransferNo " + (isBatchNo ? " AND std.BatchNo=sp.BatchNo " : string.Empty) + " ) AS UseCount ");
            sbSql.Append("CONVERT(VARCHAR(10),0) AS UseCount ");
            sbSql.Append(",isnull(std.InCount,0) as InCount ,isnull(std.OutCount,0) as OutCount,std.OutPriceTotal as OutPriceTotal,std.InPriceTotal as InPriceTotal ");
            sbSql.Append("from officedba.StorageTransferDetail  as std left join officedba.ProductInfo as pi1 on std.ProductID=pi1.ID");
            sbSql.Append(" LEFT JOIN officedba.CodeUnitType AS g ON g.ID=std.UsedUnitID ");
            sbSql.Append(" WHERE std.CompanyCD=@CompanyCD AND std.TransferNo=@TransferNo ");
            SqlParameter[] Paras = { 
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@TransferNo",SqlDbType.VarChar)
                                   };
            Paras[0].Value = std.CompanyCD;
            Paras[1].Value = std.TransferNo;

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);

        }
        #endregion

        #region 调拨出库
        public static string StorageTransferOut(Model.Office.StorageManager.StorageTransfer st, DataTable dtDetail, bool isBatchNo)
        {

            isBatchNo = false;

            #region 构造调拨单SQL字符串

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageTransfer set ");
            strSql.Append(" OutDate=@OutDate,");
            strSql.Append(" OutUserID=@OutUserID,");
            strSql.Append(" ModifiedDate=@ModifiedDate,");
            strSql.Append(" ModifiedUserID=@ModifiedUserID ,");
            strSql.Append(" OutCount=@OutCount,");
            strSql.Append(" OutFeeSum=@OutFeeSum,");
            strSql.Append(" BusiStatus=@BusiStatus ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@OutDate", SqlDbType.DateTime),
					new SqlParameter("@OutUserID", SqlDbType.Int),
                    new SqlParameter("@ModifiedDate",SqlDbType.DateTime),
                    new SqlParameter("@ModifiedUserID",SqlDbType.VarChar),
                    new SqlParameter("@OutCount",SqlDbType.Decimal),
                    new SqlParameter("@OutFeeSum",SqlDbType.Decimal),
                    new SqlParameter("@BusiStatus",SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.Int),
                                        };
            parameters[0].Value = st.OutDate;
            parameters[1].Value = st.OutUserID;
            parameters[2].Value = st.ModifiedDate;
            parameters[3].Value = st.ModifiedUserID;
            parameters[4].Value = st.OutCount;
            parameters[5].Value = st.OutFeeSum;
            parameters[6].Value = st.BusiStatus;
            parameters[7].Value = st.ID;
            #endregion

            SqlCommand SqlMainCmd = new SqlCommand();
            SqlMainCmd.CommandText = strSql.ToString();
            SqlMainCmd.Parameters.AddRange(parameters);
            ArrayList SqlCmdList = new ArrayList();
            SqlCmdList.Add(SqlMainCmd);

            #region 构造调拨单明细SQL字符串
            StringBuilder sbDel = new StringBuilder();
            sbDel.Append("DELETE officedba.StorageTransferDetail WHERE TransferNo=@TransferNo AND CompanyCD=@CompanyCD");
            SqlParameter[] Paras = { 
                                       new SqlParameter("@TransferNo",SqlDbType.VarChar),
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar)
                                   };
            Paras[0].Value = st.TransferNo;
            Paras[1].Value = st.CompanyCD;

            SqlCommand sqlDelCmd = new SqlCommand();
            sqlDelCmd.CommandText = sbDel.ToString();
            sqlDelCmd.Parameters.AddRange(Paras);
            SqlCmdList.Add(sqlDelCmd);

            for (int i = 0; i < dtDetail.Rows.Count; i++)
            {
                DataRow row = dtDetail.Rows[i];


                StringBuilder strSubSql = new StringBuilder();
                strSubSql.Append("insert into officedba.StorageTransferDetail(");
                strSubSql.Append("CompanyCD,TransferNo,SortNo,ProductID,UnitID,TranCount,TranPrice,TranPriceTotal,Remark,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo,OutCount,OutPriceTotal)");
                strSubSql.Append(" values (");
                strSubSql.Append("@CompanyCD,@TransferNo,@SortNo,@ProductID,@UnitID,@TranCount,@TranPrice,@TranPriceTotal,@Remark,@ModifiedDate,@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo,@OutCount,@OutPriceTotal)");
                SqlParameter[] subparas = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@TransferNo", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@UnitID", SqlDbType.Int,4),
					new SqlParameter("@TranCount", SqlDbType.Decimal,9),
					new SqlParameter("@TranPrice", SqlDbType.Decimal,9),
					new SqlParameter("@TranPriceTotal", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@UsedUnitID", SqlDbType.Int,4),
					new SqlParameter("@UsedUnitCount", SqlDbType.Decimal,9),
					new SqlParameter("@UsedPrice", SqlDbType.Decimal,9),
					new SqlParameter("@ExRate", SqlDbType.Decimal,9),
                    new SqlParameter("@BatchNo",SqlDbType.VarChar),
                    new SqlParameter("@OutCount",SqlDbType.Decimal,22),
                    new SqlParameter("@OutPriceTotal",SqlDbType.Decimal,22)};
                subparas[0].Value = row["CompanyCD"].ToString();
                subparas[1].Value = row["TransferNo"].ToString();
                subparas[2].Value = row["SortNo"].ToString();
                subparas[3].Value = row["ProductID"].ToString();
                if (row["UnitID"] != null && row["UnitID"].ToString() != "")
                    subparas[4].Value = row["UnitID"].ToString();
                else
                    subparas[4].Value = DBNull.Value;
                subparas[5].Value = row["TranCount"].ToString();
                subparas[6].Value = row["TranPrice"].ToString();
                subparas[7].Value = row["TranPriceTotal"].ToString();
                subparas[8].Value = row["Remark"].ToString();
                subparas[9].Value = row["ModifiedDate"].ToString();
                subparas[10].Value = row["ModifiedUserID"].ToString();
                if (row["UsedUnitID"].ToString() != "")
                    subparas[11].Value = row["UsedUnitID"].ToString();
                else
                    subparas[11].Value = DBNull.Value;
                if (row["UsedUnitCount"].ToString() != "")
                    subparas[12].Value = row["UsedUnitCount"].ToString();
                else
                    subparas[12].Value = DBNull.Value;
                if (row["UsedPrice"].ToString() != "")
                    subparas[13].Value = row["UsedPrice"].ToString();
                else
                    subparas[13].Value = DBNull.Value;
                if (row["ExRate"].ToString() != "")
                    subparas[14].Value = row["ExRate"].ToString();
                else
                    subparas[14].Value = DBNull.Value;
                subparas[15].Value = row["BatchNo"].ToString();
                if (row["OutCount"].ToString() != "")
                    subparas[16].Value = row["OutCount"].ToString();
                else
                    subparas[16].Value = DBNull.Value;
                if (row["OutPriceTotal"].ToString() != "")
                    subparas[17].Value = row["OutPriceTotal"].ToString();
                else
                    subparas[17].Value = DBNull.Value;

                SqlCommand sqlSubCmd = new SqlCommand();
                sqlSubCmd.Parameters.AddRange(subparas);
                sqlSubCmd.CommandText = strSubSql.ToString();
                SqlCmdList.Add(sqlSubCmd);

                //bool isBatchNo = false;
                if (row["BatchNo"] != null && row["BatchNo"].ToString() != "" && row["BatchNo"].ToString() != string.Empty && row["BatchNo"].ToString().ToLower() != "nobatch")
                    isBatchNo = true;



                StringBuilder sbProduct = new StringBuilder();
                sbProduct.Append("UPDATE officedba.StorageProduct  SET ProductCount=ProductCount-@OutCount ");
                sbProduct.Append(" WHERE CompanyCD=@CompanyCD AND StorageID=@StorageID AND ProductID=@ProductID " + (isBatchNo ? " AND BatchNo=@BatchNo " : "AND BatchNo IS NULL"));


                SqlParameter[] pdtSqlParams = isBatchNo ? new SqlParameter[5] : new SqlParameter[4];
                int index = 0;
                pdtSqlParams[index++] = SqlHelper.GetParameter("@OutCount", row["OutCount"].ToString());
                pdtSqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", st.CompanyCD);
                pdtSqlParams[index++] = SqlHelper.GetParameter("@StorageID", st.OutStorageID);
                pdtSqlParams[index++] = SqlHelper.GetParameter("@ProductID", row["ProductID"].ToString());
                if (isBatchNo)
                    pdtSqlParams[index++] = SqlHelper.GetParameter("@BatchNo", row["BatchNo"].ToString());



                SqlCommand SqlPdtCmd = new SqlCommand();
                SqlPdtCmd.CommandText = sbProduct.ToString();
                SqlPdtCmd.Parameters.AddRange(pdtSqlParams);
                SqlCmdList.Add(SqlPdtCmd);
            }


            #region 插入流水账表
            StringBuilder accountSql = new StringBuilder();

            accountSql.AppendLine("  INSERT INTO officedba.StorageAccount ");
            accountSql.AppendLine(" (CompanyCD,BillType,ProductID,StorageID," + (isBatchNo ? "BatchNo," : string.Empty) + "BillNo,HappenDate,HappenCount,ProductCount,Creator,Price,PageUrl) ");
            //accountSql.AppendLine(" VALUES ");
            //accountSql.AppendLine(" ( ");
            accountSql.AppendLine(" SELECT a.CompanyCD,@BillType,a.ProductID,@StorageID," + (isBatchNo ? "a.BatchNo," : string.Empty) + "a.TransferNo,getdate(),a.OutCount,b.ProductCount,@Creator,a.TranPrice ");
            accountSql.AppendLine(" ,@PageUrl ");
            accountSql.AppendLine(" FROM officedba.StorageTransferDetail AS a ");
            accountSql.AppendLine(" LEFT JOIN  ");
            accountSql.AppendLine(" ( ");
            accountSql.AppendLine(" SELECT SUM(b.ProductCount) AS ProductCount,b.ProductID," + (isBatchNo ? "b.BatchNo," : string.Empty) + "b.StorageID FROM officedba.StorageProduct AS b ");
            accountSql.AppendLine(" WHERE b.CompanyCD=@CompanyCD AND b.StorageID=@StorageID  ");
            accountSql.AppendLine(" GROUP BY b.ProductID ,b.StorageID " + (isBatchNo ? ",b.BatchNo" : string.Empty));
            accountSql.AppendLine("  ) AS b ON a.ProductID=b.ProductID  " + (isBatchNo ? " AND a.BatchNo=b.BatchNo" : string.Empty));
            accountSql.AppendLine(" WHERE a.TransferNo=@TransferNo AND a.CompanyCD =@CompanyCD ");
            //  accountSql.AppendLine(" ) ");

            SqlParameter[] acctParams = new SqlParameter[6];

            int acctIndex = 0;
            acctParams[acctIndex++] = SqlHelper.GetParameter("@CompanyCD", st.CompanyCD);
            acctParams[acctIndex++] = SqlHelper.GetParameter("@BillType", 12);
            acctParams[acctIndex++] = SqlHelper.GetParameter("@StorageID", st.OutStorageID);
            acctParams[acctIndex++] = SqlHelper.GetParameter("@Creator", st.OutUserID);
            acctParams[acctIndex++] = SqlHelper.GetParameter("@PageUrl", "Pages/Office/StorageManager/StorageTransferSave.aspx?ModuleID=2051501&action=EDIT&TransferID=" + st.ID);
            //acctParams[acctIndex++] = SqlHelper.GetParameter("@ReMark", "调拨出库");
            acctParams[acctIndex++] = SqlHelper.GetParameter("@TransferNo", st.TransferNo);


            SqlCommand acctCmd = new SqlCommand();
            acctCmd.CommandText = accountSql.ToString();
            acctCmd.Parameters.AddRange(acctParams);
            SqlCmdList.Add(acctCmd);

            #endregion


            //foreach (StorageTransferDetail std in stdList)
            //{


            //    StringBuilder strSubSql = new StringBuilder();
            //    strSubSql.Append("insert into officedba.StorageTransferDetail(");
            //    strSubSql.Append("CompanyCD,TransferNo,SortNo,ProductID,UnitID,TranCount,TranPrice,TranPriceTotal,Remark,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
            //    strSubSql.Append(" values (");
            //    strSubSql.Append("@CompanyCD,@TransferNo,@SortNo,@ProductID,@UnitID,@TranCount,@TranPrice,@TranPriceTotal,@Remark,@ModifiedDate,@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
            //    SqlParameter[] subparas = {
            //    new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
            //    new SqlParameter("@TransferNo", SqlDbType.VarChar,50),
            //    new SqlParameter("@SortNo", SqlDbType.Int,4),
            //    new SqlParameter("@ProductID", SqlDbType.Int,4),
            //    new SqlParameter("@UnitID", SqlDbType.Int,4),
            //    new SqlParameter("@TranCount", SqlDbType.Decimal,9),
            //    new SqlParameter("@TranPrice", SqlDbType.Decimal,9),
            //    new SqlParameter("@TranPriceTotal", SqlDbType.Decimal,9),
            //    new SqlParameter("@Remark", SqlDbType.VarChar,800),
            //    new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
            //    new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
            //    new SqlParameter("@UsedUnitID", SqlDbType.Int,4),
            //    new SqlParameter("@UsedUnitCount", SqlDbType.Decimal,9),
            //    new SqlParameter("@UsedPrice", SqlDbType.Decimal,9),
            //    new SqlParameter("@ExRate", SqlDbType.Decimal,9),
            //    new SqlParameter("@BatchNo",SqlDbType.VarChar)};
            //    subparas[0].Value = std.CompanyCD;
            //    subparas[1].Value = std.TransferNo;
            //    subparas[2].Value = std.SortNo;
            //    subparas[3].Value = std.ProductID;
            //    if (std.UnitID != null)
            //        subparas[4].Value = std.UnitID;
            //    else
            //        subparas[4].Value = DBNull.Value;
            //    subparas[5].Value = std.TranCount;
            //    subparas[6].Value = std.TranPrice;
            //    subparas[7].Value = std.TranPriceTotal;
            //    subparas[8].Value = std.Remark;
            //    subparas[9].Value = std.ModifiedDate;
            //    subparas[10].Value = std.ModifiedUserID;
            //    subparas[11].Value = std.UsedUnitID;
            //    subparas[12].Value = std.UsedUnitCount;
            //    subparas[13].Value = std.UsedPrice;
            //    subparas[14].Value = std.ExRate;
            //    subparas[15].Value = std.BatchNo;

            //    SqlCommand sqlSubCmd = new SqlCommand();
            //    sqlSubCmd.Parameters.AddRange(subparas);
            //    sqlSubCmd.CommandText = strSubSql.ToString();
            //    SqlCmdList.Add(sqlSubCmd);

            //    bool isBatchNo = false;
            //    if (std.BatchNo != null && std.BatchNo != "" && std.BatchNo != string.Empty && std.BatchNo.ToLower() != "nobatch")
            //        isBatchNo = true;



            //    StringBuilder sbProduct = new StringBuilder();
            //    sbProduct.Append("UPDATE officedba.StorageProduct  SET ProductCount=ProductCount-@OutCount ");
            //    sbProduct.Append(" WHERE CompanyCD=@CompanyCD AND StorageID=@StorageID AND ProductID=@ProductID " + (isBatchNo ? " AND BatchNo=@BatchNo " : string.Empty));


            //    SqlParameter[] pdtSqlParams = isBatchNo ? new SqlParameter[5] : new SqlParameter[4];
            //    int index = 0;
            //    pdtSqlParams[index++] = SqlHelper.GetParameter("@OutCount", std.OutCount);
            //    pdtSqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", std.CompanyCD);
            //    pdtSqlParams[index++] = SqlHelper.GetParameter("@StorageID", st.OutStorageID);
            //    pdtSqlParams[index++] = SqlHelper.GetParameter("@ProductID", std.ProductID);
            //    if (isBatchNo)
            //        pdtSqlParams[index++] = SqlHelper.GetParameter("@BatchNo", std.BatchNo);



            //    SqlCommand SqlPdtCmd = new SqlCommand();
            //    SqlPdtCmd.CommandText = sbProduct.ToString();
            //    SqlPdtCmd.Parameters.AddRange(pdtSqlParams);
            //    SqlCmdList.Add(SqlPdtCmd);
            //}


            #endregion

            bool result = SqlHelper.ExecuteTransWithArrayList(SqlCmdList);
            if (result)
                return "1|";
            else
                return "3|";
        }
        #endregion

        #region 调拨入库
        public static string StorageTransferIn(Model.Office.StorageManager.StorageTransfer st, DataTable dtDetail, bool isBatchNo)
        {
            isBatchNo = false;

            #region 构造调拨单SQL字符串

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageTransfer set ");
            strSql.Append(" InDate=@InDate,");
            strSql.Append(" InUserID=@InUserID,");
            strSql.Append(" ModifiedDate=@ModifiedDate,");
            strSql.Append(" ModifiedUserID=@ModifiedUserID ,");
            strSql.Append(" InCount=@InCount,");
            strSql.Append(" InFeeSum=@InFeeSum,");
            strSql.Append(" BusiStatus=@BusiStatus ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@InDate", SqlDbType.DateTime),
					new SqlParameter("@InUserID", SqlDbType.Int),
                    new SqlParameter("@ModifiedDate",SqlDbType.DateTime),
                    new SqlParameter("@ModifiedUserID",SqlDbType.VarChar),
                    new SqlParameter("@InCount",SqlDbType.Decimal),
                    new SqlParameter("@InFeeSum",SqlDbType.Decimal),
                    new SqlParameter("@BusiStatus",SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.Int),
                                        };
            parameters[0].Value = st.InDate;
            parameters[1].Value = st.InUserID;
            parameters[2].Value = st.ModifiedDate;
            parameters[3].Value = st.ModifiedUserID;
            parameters[4].Value = st.InCount;
            parameters[5].Value = st.InFeeSum;
            parameters[6].Value = st.BusiStatus;
            parameters[7].Value = st.ID;
            #endregion

            SqlCommand SqlMainCmd = new SqlCommand();
            SqlMainCmd.CommandText = strSql.ToString();
            SqlMainCmd.Parameters.AddRange(parameters);
            ArrayList SqlCmdList = new ArrayList();
            SqlCmdList.Add(SqlMainCmd);

            #region 构造调拨单明细SQL字符串
            StringBuilder sbDel = new StringBuilder();
            sbDel.Append("DELETE officedba.StorageTransferDetail WHERE TransferNo=@TransferNo AND CompanyCD=@CompanyCD");
            SqlParameter[] Paras = { 
                                       new SqlParameter("@TransferNo",SqlDbType.VarChar),
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar)
                                   };
            Paras[0].Value = st.TransferNo;
            Paras[1].Value = st.CompanyCD;

            SqlCommand sqlDelCmd = new SqlCommand();
            sqlDelCmd.CommandText = sbDel.ToString();
            sqlDelCmd.Parameters.AddRange(Paras);
            SqlCmdList.Add(sqlDelCmd);

            for (int i = 0; i < dtDetail.Rows.Count; i++)
            {

                DataRow row = dtDetail.Rows[i];

                StringBuilder strSubSql = new StringBuilder();
                strSubSql.Append("insert into officedba.StorageTransferDetail(");
                strSubSql.Append("CompanyCD,TransferNo,SortNo,ProductID,UnitID,TranCount,TranPrice,TranPriceTotal,Remark,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo,OutCount,OutPriceTotal,InCount,InPriceTotal)");
                strSubSql.Append(" values (");
                strSubSql.Append("@CompanyCD,@TransferNo,@SortNo,@ProductID,@UnitID,@TranCount,@TranPrice,@TranPriceTotal,@Remark,@ModifiedDate,@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo,@OutCount,@OutPriceTotal,@InCount,@InPriceTotal)");
                SqlParameter[] subparas = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@TransferNo", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@UnitID", SqlDbType.Int,4),
					new SqlParameter("@TranCount", SqlDbType.Decimal,9),
					new SqlParameter("@TranPrice", SqlDbType.Decimal,9),
					new SqlParameter("@TranPriceTotal", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@UsedUnitID", SqlDbType.Int,4),
					new SqlParameter("@UsedUnitCount", SqlDbType.Decimal,9),
					new SqlParameter("@UsedPrice", SqlDbType.Decimal,9),
					new SqlParameter("@ExRate", SqlDbType.Decimal,9),
                    new SqlParameter("@BatchNo",SqlDbType.VarChar),
                    new SqlParameter("@OutCount",SqlDbType.Decimal,22),
                    new SqlParameter("@OutPriceTotal",SqlDbType.Decimal,22),
                    new SqlParameter("@InCount",SqlDbType.Decimal,22),
                    new SqlParameter("@InPriceTotal",SqlDbType.Decimal,22)};
                subparas[0].Value = st.CompanyCD;
                subparas[1].Value = row["TransferNo"].ToString();
                subparas[2].Value = row["SortNo"].ToString();
                subparas[3].Value = row["ProductID"].ToString();
                if (row["UnitID"] != null && row["UnitID"].ToString() != string.Empty)
                    subparas[4].Value = row["UnitID"].ToString();
                else
                    subparas[4].Value = DBNull.Value;
                subparas[5].Value = row["TranCount"].ToString();
                subparas[6].Value = row["TranPrice"].ToString();
                subparas[7].Value = row["TranPriceTotal"].ToString();
                subparas[8].Value = row["Remark"].ToString();
                subparas[9].Value = row["ModifiedDate"].ToString();
                subparas[10].Value = row["ModifiedUserID"].ToString();
                if (row["UsedUnitID"].ToString() != "")
                    subparas[11].Value = row["UsedUnitID"].ToString();
                else
                    subparas[11].Value = DBNull.Value;
                if (row["UsedUnitCount"].ToString() != "")
                    subparas[12].Value = row["UsedUnitCount"].ToString();
                else
                    subparas[12].Value = DBNull.Value;
                if (row["UsedPrice"].ToString() != "")
                    subparas[13].Value = row["UsedPrice"].ToString();
                else
                    subparas[13].Value = DBNull.Value;
                if (row["ExRate"].ToString() != "")
                    subparas[14].Value = row["ExRate"].ToString();
                else
                    subparas[14].Value = DBNull.Value;
                subparas[15].Value = row["BatchNo"].ToString();
                subparas[16].Value = row["OutCount"].ToString();
                subparas[17].Value = row["OutPriceTotal"].ToString();
                subparas[18].Value = row["InCount"].ToString();
                subparas[19].Value = row["InPriceTotal"].ToString();

                SqlCommand sqlSubCmd = new SqlCommand();
                sqlSubCmd.Parameters.AddRange(subparas);
                sqlSubCmd.CommandText = strSubSql.ToString();
                SqlCmdList.Add(sqlSubCmd);


                //bool isBatchNo = false;
                if (row["BatchNo"] != null && row["BatchNo"].ToString() != "" && row["BatchNo"].ToString() != string.Empty && row["BatchNo"].ToString().ToLower() != "nobatch")
                    isBatchNo = true;



                /*验证该分仓存量表中是否有指定产品的库存 如果没有则插入 反之 更新数据*/
                StringBuilder sbCheck = new StringBuilder();
                sbCheck.AppendLine(" SELECT TOP 1 * FROM officedba.StorageProduct where ");
                sbCheck.AppendLine(" CompanyCD=@CompanyCD AND StorageID=@StorageID AND ProductID=@ProductID " + (isBatchNo ? " AND BatchNo=@BatchNo" : " AND BatchNo IS NULL"));

                SqlParameter[] chkSqlParams = isBatchNo ? new SqlParameter[4] : new SqlParameter[3];
                int index = 0;
                chkSqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", st.CompanyCD);
                chkSqlParams[index++] = SqlHelper.GetParameter("@StorageID", st.InStorageID);
                chkSqlParams[index++] = SqlHelper.GetParameter("@ProductID", row["ProductID"].ToString());
                if (isBatchNo)
                    chkSqlParams[index++] = SqlHelper.GetParameter("@BatchNo", row["BatchNo"].ToString());



                if (SqlHelper.Exists(sbCheck.ToString(), chkSqlParams))
                {
                    StringBuilder sbProduct = new StringBuilder();
                    sbProduct.Append("UPDATE officedba.StorageProduct  SET ProductCount=isnull(ProductCount,0)+@InCount ");
                    sbProduct.Append(" WHERE CompanyCD=@CompanyCD AND StorageID=@StorageID  AND ProductID=@ProductID  " + (isBatchNo ? " AND BatchNo=@BatchNo " : "AND BatchNo IS NULL "));

                    SqlParameter[] pdtSqlParams = isBatchNo ? new SqlParameter[5] : new SqlParameter[4];
                    int m = 0;
                    pdtSqlParams[m++] = SqlHelper.GetParameter("@InCount", row["InCount"].ToString());
                    pdtSqlParams[m++] = SqlHelper.GetParameter("@CompanyCD", st.CompanyCD);
                    pdtSqlParams[m++] = SqlHelper.GetParameter("@StorageId", st.InStorageID);
                    pdtSqlParams[m++] = SqlHelper.GetParameter("@ProductID", row["ProductID"].ToString());
                    if (isBatchNo)
                        pdtSqlParams[m++] = SqlHelper.GetParameter("@BatchNo", row["BatchNo"].ToString());

                    SqlCommand SqlPdtCmd = new SqlCommand();
                    SqlPdtCmd.CommandText = sbProduct.ToString();
                    SqlPdtCmd.Parameters.AddRange(pdtSqlParams);
                    SqlCmdList.Add(SqlPdtCmd);
                }
                else
                {
                    StringBuilder sbProduct = new StringBuilder();
                    sbProduct.AppendLine(" INSERT INTO officedba.StorageProduct (CompanyCD,StorageID,ProductID,ProductCount,BatchNo) VALUES ");
                    sbProduct.AppendLine(" (@CompanyCD,@StorageID,@ProductID,@ProductCount,@BatchNo )");
                    SqlParameter[] pdtPara = { 
                                             new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                             new SqlParameter("@StorageID",SqlDbType.Int),
                                             new SqlParameter("@ProductID",SqlDbType.Int),
                                             new SqlParameter("@ProductCount",SqlDbType.Decimal),
                                             new SqlParameter("@BatchNo",SqlDbType.VarChar)};
                    pdtPara[0].Value = st.CompanyCD;
                    pdtPara[1].Value = st.InStorageID;
                    pdtPara[2].Value = row["ProductID"].ToString();
                    pdtPara[3].Value = row["InCount"].ToString();
                    pdtPara[4].Value = row["BatchNo"].ToString();

                    SqlCommand SqlPdtCmd = new SqlCommand();
                    SqlPdtCmd.CommandText = sbProduct.ToString();
                    SqlPdtCmd.Parameters.AddRange(pdtPara);
                    SqlCmdList.Add(SqlPdtCmd);
                }



         


            }

            #region 插入库存流水账表
            StringBuilder accountSql = new StringBuilder();

            accountSql.AppendLine("  INSERT INTO officedba.StorageAccount ");
            accountSql.AppendLine(" (CompanyCD,BillType,ProductID,StorageID," + (isBatchNo ? "BatchNo," : string.Empty) + "BillNo,HappenDate,HappenCount,ProductCount,Creator,Price,PageUrl) ");
            //accountSql.AppendLine(" VALUES ");
            //accountSql.AppendLine(" ( ");
            accountSql.AppendLine(" SELECT a.CompanyCD,@BillType,a.ProductID,@StorageID," + (isBatchNo ? "a.BatchNo," : string.Empty) + "a.TransferNo,getdate(),a.OutCount,b.ProductCount,@Creator,a.TranPrice ");
            accountSql.AppendLine(" ,@PageUrl ");
            accountSql.AppendLine(" FROM officedba.StorageTransferDetail AS a ");
            accountSql.AppendLine(" LEFT JOIN  ");
            accountSql.AppendLine(" ( ");
            accountSql.AppendLine(" SELECT SUM(b.ProductCount) AS ProductCount,b.ProductID,b.StorageID " + (isBatchNo ? ",b.BatchNo" : string.Empty) + " FROM officedba.StorageProduct AS b ");
            accountSql.AppendLine(" WHERE b.CompanyCD=@CompanyCD AND b.StorageID=@StorageID --对某个公司的某个物品数量进行统计");
            accountSql.AppendLine(" GROUP BY b.ProductID,b.StorageID " + (isBatchNo ? " ,b.BatchNo" : string.Empty));
            accountSql.AppendLine("  ) AS b ON a.ProductID=b.ProductID   " + (isBatchNo ? " AND a.BatchNo=b.BatchNo " : string.Empty));
            accountSql.AppendLine(" WHERE a.TransferNo=@TransferNo AND a.CompanyCD =@CompanyCD ");
            //  accountSql.AppendLine(" ) ");

            SqlParameter[] acctParams = new SqlParameter[6];

            int acctIndex = 0;
            acctParams[acctIndex++] = SqlHelper.GetParameter("@CompanyCD", st.CompanyCD);
            acctParams[acctIndex++] = SqlHelper.GetParameter("@BillType", 13);
            acctParams[acctIndex++] = SqlHelper.GetParameter("@StorageID", st.InStorageID);
            acctParams[acctIndex++] = SqlHelper.GetParameter("@Creator", st.InUserID);
            acctParams[acctIndex++] = SqlHelper.GetParameter("@PageUrl", "Pages/Office/StorageManager/StorageTransferSave.aspx?ModuleID=2051501&action=EDIT&TransferID=" + st.ID);
           // acctParams[acctIndex++] = SqlHelper.GetParameter("@ReMark", "调拨入库");
            acctParams[acctIndex++] = SqlHelper.GetParameter("@TransferNo", st.TransferNo);


            SqlCommand acctCmd = new SqlCommand();
            acctCmd.CommandText = accountSql.ToString();
            acctCmd.Parameters.AddRange(acctParams);
            SqlCmdList.Add(acctCmd);
            #endregion



            //foreach (StorageTransferDetail std in stdList)
            //{
            //    StringBuilder strSubSql = new StringBuilder();
            //    strSubSql.Append("insert into officedba.StorageTransferDetail(");
            //    strSubSql.Append("CompanyCD,TransferNo,SortNo,ProductID,UnitID,TranCount,TranPrice,TranPriceTotal,Remark,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
            //    strSubSql.Append(" values (");
            //    strSubSql.Append("@CompanyCD,@TransferNo,@SortNo,@ProductID,@UnitID,@TranCount,@TranPrice,@TranPriceTotal,@Remark,@ModifiedDate,@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
            //    SqlParameter[] subparas = {
            //    new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
            //    new SqlParameter("@TransferNo", SqlDbType.VarChar,50),
            //    new SqlParameter("@SortNo", SqlDbType.Int,4),
            //    new SqlParameter("@ProductID", SqlDbType.Int,4),
            //    new SqlParameter("@UnitID", SqlDbType.Int,4),
            //    new SqlParameter("@TranCount", SqlDbType.Decimal,9),
            //    new SqlParameter("@TranPrice", SqlDbType.Decimal,9),
            //    new SqlParameter("@TranPriceTotal", SqlDbType.Decimal,9),
            //    new SqlParameter("@Remark", SqlDbType.VarChar,800),
            //    new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
            //    new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
            //    new SqlParameter("@UsedUnitID", SqlDbType.Int,4),
            //    new SqlParameter("@UsedUnitCount", SqlDbType.Decimal,9),
            //    new SqlParameter("@UsedPrice", SqlDbType.Decimal,9),
            //    new SqlParameter("@ExRate", SqlDbType.Decimal,9),
            //    new SqlParameter("@BatchNo",SqlDbType.VarChar)};
            //    subparas[0].Value = std.CompanyCD;
            //    subparas[1].Value = std.TransferNo;
            //    subparas[2].Value = std.SortNo;
            //    subparas[3].Value = std.ProductID;
            //    if (std.UnitID != null)
            //        subparas[4].Value = std.UnitID;
            //    else
            //        subparas[4].Value = DBNull.Value;
            //    subparas[5].Value = std.TranCount;
            //    subparas[6].Value = std.TranPrice;
            //    subparas[7].Value = std.TranPriceTotal;
            //    subparas[8].Value = std.Remark;
            //    subparas[9].Value = std.ModifiedDate;
            //    subparas[10].Value = std.ModifiedUserID;
            //    subparas[11].Value = std.UsedUnitID;
            //    subparas[12].Value = std.UsedUnitCount;
            //    subparas[13].Value = std.UsedPrice;
            //    subparas[14].Value = std.ExRate;
            //    subparas[15].Value = std.BatchNo;

            //    SqlCommand sqlSubCmd = new SqlCommand();
            //    sqlSubCmd.Parameters.AddRange(subparas);
            //    sqlSubCmd.CommandText = strSubSql.ToString();
            //    SqlCmdList.Add(sqlSubCmd);


            //    bool isBatchNo = false;
            //    if (std.BatchNo != null && std.BatchNo != "" && std.BatchNo != string.Empty && std.BatchNo.ToLower() != "nobatch")
            //        isBatchNo = true;



            //    /*验证该分仓存量表中是否有指定产品的库存 如果没有则插入 反之 更新数据*/
            //    StringBuilder sbCheck = new StringBuilder();
            //    sbCheck.AppendLine(" SELECT TOP 1 * FROM officedba.StorageProduct where ");
            //    sbCheck.AppendLine(" CompanyCD=@CompanyCD AND StorageID=@StorageID AND ProductID=@ProductID " + (isBatchNo ? " AND BatchNo=@BatchNo" : string.Empty));

            //    SqlParameter[] chkSqlParams = isBatchNo ? new SqlParameter[4] : new SqlParameter[3];
            //    int index = 0;
            //    chkSqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", std.CompanyCD);
            //    chkSqlParams[index++] = SqlHelper.GetParameter("@StorageID", st.OutStorageID);
            //    chkSqlParams[index++] = SqlHelper.GetParameter("@ProductID", std.ProductID);
            //    if (isBatchNo)
            //        chkSqlParams[index++] = SqlHelper.GetParameter("@BatchNo", std.BatchNo);



            //    if (SqlHelper.Exists(sbCheck.ToString(), chkSqlParams))
            //    {
            //        StringBuilder sbProduct = new StringBuilder();
            //        sbProduct.Append("UPDATE officedba.StorageProduct  SET ProductCount=isnull(ProductCount,0)+@InCount ");
            //        sbProduct.Append(" WHERE CompanyCD=@CompanyCD AND StorageID=@StorageID  AND ProductID=@ProductID  " + (isBatchNo ? " AND BatchNo=@BatchNo " : string.Empty));

            //        SqlParameter[] pdtSqlParams = isBatchNo ? new SqlParameter[5] : new SqlParameter[4];
            //        int m = 0;
            //        pdtSqlParams[m++] = SqlHelper.GetParameter("@InCount", std.InCount);
            //        pdtSqlParams[m++] = SqlHelper.GetParameter("@CompanyCD", std.CompanyCD);
            //        pdtSqlParams[m++] = SqlHelper.GetParameter("@StorageId", st.OutStorageID);
            //        pdtSqlParams[m++] = SqlHelper.GetParameter("@ProductID", std.ProductID);
            //        if (isBatchNo)
            //            pdtSqlParams[m++] = SqlHelper.GetParameter("@BatchNo", std.BatchNo);

            //        SqlCommand SqlPdtCmd = new SqlCommand();
            //        SqlPdtCmd.CommandText = sbProduct.ToString();
            //        SqlPdtCmd.Parameters.AddRange(pdtSqlParams);
            //        SqlCmdList.Add(SqlPdtCmd);
            //    }
            //    else
            //    {
            //        StringBuilder sbProduct = new StringBuilder();
            //        sbProduct.AppendLine(" INSERT INTO officedba.StorageProduct (CompanyCD,StorageID,ProductID,ProductCount,BatchNo) VALUES ");
            //        sbProduct.AppendLine(" (@CompanyCD,@StorageID,@ProductID,@ProductCount,@BatchNo )");
            //        SqlParameter[] pdtPara = { 
            //                             new SqlParameter("@CompanyCD",SqlDbType.VarChar),
            //                             new SqlParameter("@StorageID",SqlDbType.Int),
            //                             new SqlParameter("@ProductID",SqlDbType.Int),
            //                             new SqlParameter("@ProductCount",SqlDbType.Decimal),
            //                             new SqlParameter("@BatchNo",SqlDbType.VarChar)};
            //        pdtPara[0].Value = std.CompanyCD;
            //        pdtPara[1].Value = st.InStorageID;
            //        pdtPara[2].Value = std.ProductID;
            //        pdtPara[3].Value = std.InCount;
            //        pdtPara[4].Value = std.BatchNo;

            //        SqlCommand SqlPdtCmd = new SqlCommand();
            //        SqlPdtCmd.CommandText = sbProduct.ToString();
            //        SqlPdtCmd.Parameters.AddRange(pdtPara);
            //        SqlCmdList.Add(SqlPdtCmd);
            //    }
            //}


            #endregion

            bool result = SqlHelper.ExecuteTransWithArrayList(SqlCmdList);
            if (result)
                return "1|";
            else
                return "3|";
        }
        #endregion

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(StorageTransfer model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.StorageTransfer set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND TransferNo = @TransferNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@TransferNo", model.TransferNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion
    }
}

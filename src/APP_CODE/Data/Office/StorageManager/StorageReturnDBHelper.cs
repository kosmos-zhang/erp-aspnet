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
    public class StorageReturnDBHelper
    {

        #region 读取 借货单列表
        public static DataTable GetStorageBorrow(Hashtable htPara, int PageIndex, int PageSize, string OrderBy, ref int TotalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT sum(ProductCount)as ProductCount,sum(RealReturnCount) as RealReturnCount,sb.ID, sb.CompanyCD, sb.BorrowNo, sb.Title, sb.Borrower, sb.DeptID, sb.BorrowDate, sb.ReasonType, sb.OutDeptID, sb.StorageID, sb.OutDate, sb.Transactor, sb.TotalPrice, sb.CountTotal, sb.Summary, sb.Remark, (SELECT ect.EmployeeName FROM  officedba.EmployeeInfo AS ect where ect.id=sb.Creator) as CreatorText,sb.CreateDate, sb.BillStatus, sb.Confirmor, sb.ConfirmDate, sb.Closer, sb.CloseDate,sb.ModifiedDate, sb.ModifiedUserID,(SELECT e.EmployeeName FROM officedba.EmployeeInfo as e where e.id=sb.Borrower ) as BorrowerText, (SELECT EI.EmployeeName FROM  officedba.EmployeeInfo AS EI where EI.ID=SB.Transactor) AS OuterText,(SELECT di.DeptName from officedba.DeptInfo as di where di.ID=sb.DeptID ) AS DeptIDText,(SELECT di.DeptName from officedba.DeptInfo as di where di.ID=sb.OutDeptID) AS OutDeptIDText ,(SELECT si.StorageName from officedba.StorageInfo as si where si.ID=sb.StorageID) AS StorageName FROM  officedba.StorageBorrow AS sb inner join officedba.StorageBorrowDetail as sbd on sbd.CompanyCD=sb.CompanyCD and sbd.BorrowNo=sb.BorrowNo WHERE Convert(int,sb.BillStatus)=2  and ProductCount>RealReturnCount and sb.CompanyCD=@CompanyCD ");
            SqlParameter[] Paras = new SqlParameter[htPara.Count];
            int index = 0;
            Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[index++].Value = htPara["CompanyCD"];
            if (htPara.ContainsKey("BorrowNo"))
            {
                sbSql.Append(" AND sb.BorrowNo Like @BorrowNo ");
                Paras[index] = new SqlParameter("@BorrowNo", SqlDbType.VarChar);
                Paras[index++].Value = htPara["BorrowNo"];
            }
            if (htPara.ContainsKey("Borrower"))
            {
                sbSql.Append(" AND sb.Borrower=@Borrower ");
                Paras[index] = new SqlParameter("@Borrower", SqlDbType.Int);
                Paras[index++].Value = htPara["Borrower"];
            }
            if (htPara.ContainsKey("BorrowDeptID"))
            {
                sbSql.Append(" AND sb.DeptID=@BorrowDeptID ");
                Paras[index] = new SqlParameter("@BorrowDeptID", SqlDbType.Int);
                Paras[index++].Value = htPara["BorrowDeptID"];
            }
            if (htPara.ContainsKey("OutDeptID"))
            {
                sbSql.Append(" AND sb.OutDeptID=@OutDeptID ");
                Paras[index] = new SqlParameter("@OutDeptID", SqlDbType.Int);
                Paras[index++].Value = htPara["OutDeptID"];
            }
            if (htPara.ContainsKey("OutStorageID"))
            {
                sbSql.Append(" AND sb.StorageID=@OutStorageID ");
                Paras[index] = new SqlParameter("@OutStorageID", SqlDbType.Int);
                Paras[index++].Value = htPara["OutStorageID"];
            }

            sbSql.Append(" group by sb.ID, sb.CompanyCD, sb.BorrowNo, sb.Title, sb.Borrower, sb.DeptID, sb.BorrowDate, sb.ReasonType, sb.OutDeptID, sb.StorageID, sb.OutDate, sb.Transactor, sb.TotalPrice, sb.CountTotal, sb.Summary,sb.Creator, sb.Remark,sb.CreateDate, sb.BillStatus, sb.Confirmor, sb.ConfirmDate, sb.Closer, sb.CloseDate,sb.ModifiedDate, sb.ModifiedUserID");

            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, Paras, ref TotalCount);
        }
        #endregion

        #region 读取借货单明细
        public static DataTable GetStorageBorrowDetail(string CompanyCD, string BorrowNo)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT sbd.BatchNo,sbd.UsedUnitID,sbd.UsedUnitCount,sbd.UsedPrice,sbd.ExRate,sbd.ID,sbd.CompanyCD,sbd.BorrowNo,sbd.SortNo,sbd.ProductID,sbd.UnitID,sbd.UnitPrice,sbd.ProductCount,sbd.TotalPrice,sbd.ReturnDate,sbd.ReturnCount,sbd.RealReturnCount,sbd.Remark,sbd.ModifiedDate,sbd.ModifiedUserID, officedba.ProductInfo.ProdNo, officedba.ProductInfo.ProductName, officedba.CodeUnitType.CodeName, officedba.ProductInfo.Specification,officedba.CodeUnitType.ID AS CodeUnitID ,officedba.ProductInfo.MinusIs as MinusIs  FROM  officedba.StorageBorrowDetail as sbd INNER JOIN  officedba.ProductInfo ON sbd.ProductID = officedba.ProductInfo.ID INNER JOIN  officedba.CodeUnitType ON sbd.UnitID = officedba.CodeUnitType.ID WHERE sbd.CompanyCD=@CompanyCD AND sbd.BorrowNo=@BorrowNo AND sbd.RealReturnCount<sbd.ProductCount");

            SqlParameter[] Paras = { 
                                      new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                      new SqlParameter("@BorrowNo",SqlDbType.VarChar)
                                  };
            Paras[0].Value = CompanyCD;
            Paras[1].Value = BorrowNo;
            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
        }
        #endregion

        #region 添加借货返还单
        public static string StorageReturnAdd(StorageReturn sr, List<StorageReturnDetail> srdList, Hashtable ht)
        {

            if (!StorageReturnDetail(sr.ReturnNo, sr.CompanyCD))
                return "1|该借货单货单据编号已经存在";

            #region 构造字符串
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.StorageReturn(");
            strSql.Append("CompanyCD,ReturnNo,Title,FromType,FromBillID,StorageID,DeptID,ReturnPerson,ReturnDate,TotalPrice,CountTotal,Summary,Transactor,Remark,Creator,CreateDate,BillStatus)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@ReturnNo,@Title,@FromType,@FromBillID,@StorageID,@DeptID,@ReturnPerson,@ReturnDate,@TotalPrice,@CountTotal,@Summary,@Transactor,@Remark,@Creator,@CreateDate,@BillStatus)");
            strSql.Append(";select @ID=@@IDENTITY ");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@ReturnNo", SqlDbType.VarChar,50),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@FromType", SqlDbType.Char,1),
					new SqlParameter("@FromBillID", SqlDbType.Int,4),
					new SqlParameter("@StorageID", SqlDbType.Int,4),
					new SqlParameter("@DeptID", SqlDbType.Int,4),
					new SqlParameter("@ReturnPerson", SqlDbType.Int,4),
					new SqlParameter("@ReturnDate", SqlDbType.DateTime),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@CountTotal", SqlDbType.Decimal,9),
					new SqlParameter("@Summary", SqlDbType.VarChar,200),
					new SqlParameter("@Transactor", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
                    new SqlParameter("@Creator",SqlDbType.Int),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
                    new SqlParameter("@ID",SqlDbType.Int)};
            parameters[0].Value = sr.CompanyCD;
            parameters[1].Value = sr.ReturnNo;
            parameters[2].Value = sr.Title;
            parameters[3].Value = sr.FromType;
            parameters[4].Value = sr.FromBillID;
            parameters[5].Value = sr.StorageID;
            parameters[6].Value = sr.DeptID;
            parameters[7].Value = sr.ReturnPerson;
            parameters[8].Value = sr.ReturnDate;
            parameters[9].Value = sr.TotalPrice;
            parameters[10].Value = sr.CountTotal;
            parameters[11].Value = sr.Summary;
            parameters[12].Value = sr.Transactor;
            parameters[13].Value = sr.Remark;
            parameters[14].Value = sr.Creator;
            parameters[15].Value = sr.CreateDate;
            parameters[16].Value = sr.BillStatus;
            parameters[17].Direction = ParameterDirection.Output;
            #endregion

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Parameters.AddRange(parameters);
            sqlCmd.CommandText = strSql.ToString();
            ArrayList SqlCmdList = new ArrayList();
            SqlCmdList.Add(sqlCmd);

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(sr, ht, cmd);
            if (ht.Count > 0)
                SqlCmdList.Add(cmd);
            #endregion

            #region 构造明细字符串
            if (srdList != null && srdList.Count > 0)
            {
                foreach (StorageReturnDetail model in srdList)
                {
                    #region 构造字符串
                    StringBuilder strSqlDetail = new StringBuilder();
                    strSqlDetail.Append("insert into officedba.StorageReturnDetail(");
                    strSqlDetail.Append("CompanyCD,ReturnNo,SortNo,ProductID,ProductName,UnitID,ProductCount,ReturnCount,UnitPrice,TotalPrice,FromType,FromBillID,FromLineNo,Remark,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                    strSqlDetail.Append(" values (");
                    strSqlDetail.Append("@CompanyCD,@ReturnNo,@SortNo,@ProductID,@ProductName,@UnitID,@ProductCount,@ReturnCount,@UnitPrice,@TotalPrice,@FromType,@FromBillID,@FromLineNo,@Remark,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                    strSql.Append(";select @ID= @@IDENTITY");
                    SqlParameter[] paras = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@ReturnNo", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@ProductName", SqlDbType.VarChar,100),
					new SqlParameter("@UnitID", SqlDbType.Int,4),
					new SqlParameter("@ProductCount", SqlDbType.Decimal,9),
					new SqlParameter("@ReturnCount", SqlDbType.Decimal,9),
					new SqlParameter("@UnitPrice", SqlDbType.Decimal,9),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@FromType", SqlDbType.Char,1),
					new SqlParameter("@FromBillID", SqlDbType.Int,4),
					new SqlParameter("@FromLineNo", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
                    new SqlParameter("@UsedUnitID", SqlDbType.Int,4),
					new SqlParameter("@UsedUnitCount", SqlDbType.Decimal,9),
					new SqlParameter("@UsedPrice", SqlDbType.Decimal,9),
					new SqlParameter("@ExRate", SqlDbType.Decimal,9),
                    new SqlParameter("@ID",SqlDbType.Int),
                    new SqlParameter("@BatchNo",SqlDbType.VarChar)

                                           };
                    paras[0].Value = model.CompanyCD;
                    paras[1].Value = model.ReturnNo;
                    paras[2].Value = model.SortNo;
                    paras[3].Value = model.ProductID;
                    paras[4].Value = model.ProductName;
                    if (model.UnitID != null)
                        paras[5].Value = model.UnitID;
                    else
                        paras[5].Value = DBNull.Value;
                    paras[6].Value = model.ProductCount;
                    paras[7].Value = model.ReturnCount;
                    paras[8].Value = model.UnitPrice;
                    paras[9].Value = model.TotalPrice;
                    paras[10].Value = model.FromType;
                    paras[11].Value = model.FromBillID;
                    paras[12].Value = model.FromLineNo;
                    paras[13].Value = model.Remark;
                    paras[14].Value = model.UsedUnitID;
                    paras[15].Value = model.UsedUnitCount;
                    paras[16].Value = model.UsedPrice;
                    paras[17].Value = model.ExRate;
                    paras[18].Direction = ParameterDirection.Output;
                    paras[19].Value = model.BatchNo;
                    #endregion

                    SqlCommand sqlCmdDetail = new SqlCommand();
                    sqlCmdDetail.CommandText = strSqlDetail.ToString();
                    sqlCmdDetail.Parameters.AddRange(paras);
                    SqlCmdList.Add(sqlCmdDetail);
                }
            }
            #endregion

            bool result = SqlHelper.ExecuteTransWithArrayList(SqlCmdList);
            if (result)
            {
                string ID = string.Empty;

                ID = ((SqlCommand)SqlCmdList[0]).Parameters["@ID"].Value.ToString();
                return "2|" + ID + "#" + sr.ReturnNo;

            }
            else
                return "3|保存数据失败";
        }
        #endregion

        #region 更新 借货返还单及其明细
        public static string StorageReturnUpdate(XBase.Model.Office.StorageManager.StorageReturn sr, List<StorageReturnDetail> srdList, Hashtable ht)
        {

            #region 构造借货返还单更新字符串
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageReturn set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("ReturnNo=@ReturnNo,");
            strSql.Append("Title=@Title,");
            strSql.Append("FromType=@FromType,");
            strSql.Append("FromBillID=@FromBillID,");
            strSql.Append("StorageID=@StorageID,");
            strSql.Append("DeptID=@DeptID,");
            strSql.Append("ReturnPerson=@ReturnPerson,");
            strSql.Append("ReturnDate=@ReturnDate,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("CountTotal=@CountTotal,");
            strSql.Append("Summary=@Summary,");
            strSql.Append("InDate=@InDate,");
            strSql.Append("Transactor=@Transactor,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("BillStatus=@BillStatus,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@ReturnNo", SqlDbType.VarChar,50),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@FromType", SqlDbType.Char,1),
					new SqlParameter("@FromBillID", SqlDbType.Int,4),
					new SqlParameter("@StorageID", SqlDbType.Int,4),
					new SqlParameter("@DeptID", SqlDbType.Int,4),
					new SqlParameter("@ReturnPerson", SqlDbType.Int,4),
					new SqlParameter("@ReturnDate", SqlDbType.DateTime),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@CountTotal", SqlDbType.Decimal,9),
					new SqlParameter("@Summary", SqlDbType.VarChar,200),
					new SqlParameter("@InDate", SqlDbType.DateTime),
					new SqlParameter("@Transactor", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50)};
            parameters[0].Value = sr.ID;
            parameters[1].Value = sr.CompanyCD;
            parameters[2].Value = sr.ReturnNo;
            parameters[3].Value = sr.Title;
            parameters[4].Value = sr.FromType;
            parameters[5].Value = sr.FromBillID;
            parameters[6].Value = sr.StorageID;
            parameters[7].Value = sr.DeptID;
            parameters[8].Value = sr.ReturnPerson;
            parameters[9].Value = sr.ReturnDate;
            parameters[10].Value = sr.TotalPrice;
            parameters[11].Value = sr.CountTotal;
            parameters[12].Value = sr.Summary;
            parameters[13].Value = sr.InDate;
            parameters[14].Value = sr.Transactor;
            parameters[15].Value = sr.Remark;
            parameters[16].Value = sr.BillStatus;
            parameters[17].Value = sr.ModifiedDate;
            parameters[18].Value = sr.ModifiedUserID;
            #endregion

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Parameters.AddRange(parameters);
            sqlCmd.CommandText = strSql.ToString();

            ArrayList SqlCmdList = new ArrayList();
            SqlCmdList.Add(sqlCmd);

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(sr, ht, cmd);
            if (ht.Count > 0)
                SqlCmdList.Add(cmd);
            #endregion

            #region 先删除明细 然后更新明细
            StringBuilder sbDelDetail = new StringBuilder();
            sbDelDetail.Append("DELETE  officedba.StorageReturnDetail  WHERE CompanyCD=@CompanyCD AND ReturnNo=@ReturnNo ");
            SqlParameter[] ParaDelDetail ={
                                           new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                           new SqlParameter("@ReturnNo",SqlDbType.VarChar)
                                      };
            ParaDelDetail[0].Value = sr.CompanyCD;
            ParaDelDetail[1].Value = sr.ReturnNo;
            SqlCommand sqlCmdDetail = new SqlCommand();
            sqlCmdDetail.CommandText = sbDelDetail.ToString();
            sqlCmdDetail.Parameters.AddRange(ParaDelDetail);
            SqlCmdList.Add(sqlCmdDetail);

            #region 构造明细字符串
            if (srdList != null && srdList.Count > 0)
            {
                foreach (StorageReturnDetail model in srdList)
                {
                    #region 构造字符串
                    StringBuilder strSqlDetail = new StringBuilder();
                    strSqlDetail.Append("insert into officedba.StorageReturnDetail(");
                    strSqlDetail.Append("CompanyCD,ReturnNo,SortNo,ProductID,ProductName,UnitID,ProductCount,ReturnCount,UnitPrice,TotalPrice,FromType,FromBillID,FromLineNo,Remark,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                    strSqlDetail.Append(" values (");
                    strSqlDetail.Append("@CompanyCD,@ReturnNo,@SortNo,@ProductID,@ProductName,@UnitID,@ProductCount,@ReturnCount,@UnitPrice,@TotalPrice,@FromType,@FromBillID,@FromLineNo,@Remark,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                    strSql.Append(";select @ID= @@IDENTITY");
                    SqlParameter[] paras = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@ReturnNo", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@ProductName", SqlDbType.VarChar,100),
					new SqlParameter("@UnitID", SqlDbType.Int,4),
					new SqlParameter("@ProductCount", SqlDbType.Decimal,9),
					new SqlParameter("@ReturnCount", SqlDbType.Decimal,9),
					new SqlParameter("@UnitPrice", SqlDbType.Decimal,9),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@FromType", SqlDbType.Char,1),
					new SqlParameter("@FromBillID", SqlDbType.Int,4),
					new SqlParameter("@FromLineNo", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
                    new SqlParameter("@UsedUnitID", SqlDbType.Int,4),
					new SqlParameter("@UsedUnitCount", SqlDbType.Decimal,9),
					new SqlParameter("@UsedPrice", SqlDbType.Decimal,9),
					new SqlParameter("@ExRate", SqlDbType.Decimal,9),
                    new SqlParameter("@ID",SqlDbType.Int),
                    new SqlParameter("@BatchNo",SqlDbType.VarChar)
                                           };
                    paras[0].Value = model.CompanyCD;
                    paras[1].Value = model.ReturnNo;
                    paras[2].Value = model.SortNo;
                    paras[3].Value = model.ProductID;
                    paras[4].Value = model.ProductName;
                    if (model.UnitID != null)
                        paras[5].Value = model.UnitID;
                    else
                        paras[5].Value = DBNull.Value;
                    paras[6].Value = model.ProductCount;
                    paras[7].Value = model.ReturnCount;
                    paras[8].Value = model.UnitPrice;
                    paras[9].Value = model.TotalPrice;
                    paras[10].Value = model.FromType;
                    paras[11].Value = model.FromBillID;
                    paras[12].Value = model.FromLineNo;
                    paras[13].Value = model.Remark;
                    paras[14].Value = model.UsedUnitID;
                    paras[15].Value = model.UsedUnitCount;
                    paras[16].Value = model.UsedPrice;
                    paras[17].Value = model.ExRate;
                    paras[18].Direction = ParameterDirection.Output;
                    paras[19].Value = model.BatchNo;
                    #endregion

                    SqlCommand sqlCmdDetailList = new SqlCommand();
                    sqlCmdDetailList.CommandText = strSqlDetail.ToString();
                    sqlCmdDetailList.Parameters.AddRange(paras);
                    SqlCmdList.Add(sqlCmdDetailList);
                }
            }
            #endregion

            bool result = SqlHelper.ExecuteTransWithArrayList(SqlCmdList);
            if (result)
            {
                return "1|";
            }
            else
                return "2|";

            #endregion

        }

        #endregion

        #region 验证借货返还单编号是否唯一
        public static bool StorageReturnDetail(string ReturnNo, string CommpanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Count(*) FROM officedba.StorageReturn  WHERE ReturnNo=@ReturnNo AND CompanyCD=@CompanyCD ");
            SqlParameter[] Paras = { 
                                      new SqlParameter("@ReturnNo",SqlDbType.VarChar),
                                      new SqlParameter("@CompanyCD",SqlDbType.VarChar)
                                  };
            Paras[0].Value = ReturnNo;
            Paras[1].Value = CommpanyCD;
            DataTable dt = SqlHelper.ExecuteSql(strSql.ToString(), Paras);
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region 读取借货返还单明细ID
        public static string GetDetailIDList(string CompnayCD, string ReturnNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID,FromLineNo FROM officedba.StorageReturnDetail WHERE CompanyCD=@CompanyCD AND ReturnNo=@ReturnNo");
            SqlParameter[] Paras = { 
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@ReturnNo",SqlDbType.VarChar)
                                   };
            Paras[0].Value = CompnayCD;
            Paras[1].Value = ReturnNo;


            DataTable dt = SqlHelper.ExecuteSql(strSql.ToString(), Paras);
            string temp = string.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    temp += dt.Rows[i]["FromLineNo"].ToString() + "#" + dt.Rows[i]["ID"].ToString() + ",";
            }
            return temp;
        }
        #endregion

        #region 读取借货返还单列表
        public static DataTable GetStorageReturnList(string EFIndex, string EFDesc, Hashtable htPara, string CompanyCD, int PageSize, int PageIndex, string OrderBy, ref int TotalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT sr.ID, sr.ReturnNo,sr.Title,(SELECT si.StorageName from officedba.StorageInfo as si where si.id=sr.StorageID) AS StorageName,(SELECT ei.EmployeeName from officedba.EmployeeInfo as ei where ei.id=sr.ReturnPerson ) as ReturnName,sr.ReturnDate,(CASE sr.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,sb.BorrowNo,(select di.DeptName from officedba.DeptInfo as di where sb.DeptID=di.ID ) AS BorrowDept  from officedba.StorageReturn as sr left join officedba.StorageBorrow as sb on sr.FromBillID=sb.ID  where 1=1  AND sr.CompanyCD=@CompanyCD");
            SqlParameter[] Paras = new SqlParameter[htPara.Count + 1];
            Paras[0] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[0].Value = CompanyCD;
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                strSql.AppendLine(" and sr.ExtField" + EFIndex + " like '%" + EFDesc + "%' ");
            }

            #region 构造参数
            if (htPara.Count > 0)
            {
                int index = 1;
                if (htPara.ContainsKey("@ReturnNo"))
                {
                    strSql.Append(" AND sr.ReturnNo LIKE @ReturnNo");
                    Paras[index] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                    Paras[index].Value = htPara["@ReturnNo"];
                    index++;
                }
                if (htPara.ContainsKey("@ReturnTitle"))
                {
                    strSql.Append(" AND sr.Title LIKE @ReturnTitle");
                    Paras[index] = new SqlParameter("@ReturnTitle", SqlDbType.VarChar);
                    Paras[index].Value = htPara["@ReturnTitle"];
                    index++;
                }
                if (htPara.ContainsKey("@BorrowDeptID"))
                {
                    strSql.Append(" AND sb.DeptID=@BorrowDeptID ");
                    Paras[index] = new SqlParameter("@BorrowDeptID", SqlDbType.Int);
                    Paras[index].Value = Convert.ToInt32(htPara["@BorrowDeptID"]);
                    index++;
                }
                if (htPara.ContainsKey("@OutDeptID"))
                {
                    strSql.Append(" AND sb.OutDeptID=@OutDeptID");
                    Paras[index] = new SqlParameter("@OutDeptID", SqlDbType.Int);
                    Paras[index].Value = Convert.ToInt32(htPara["@OutDeptID"]);
                    index++;
                }
                if (htPara.ContainsKey("@StorageID"))
                {
                    strSql.Append(" AND sr.StorageID=@StorageID");
                    Paras[index] = new SqlParameter("@StorageID", SqlDbType.Int);
                    Paras[index].Value = Convert.ToInt32(htPara["@StorageID"]);
                    index++;
                }
                if (htPara.ContainsKey("@ReturnPerson"))
                {
                    strSql.Append(" AND sr.ReturnPerson=@ReturnPerson");
                    Paras[index] = new SqlParameter("@ReturnPerson", SqlDbType.Int);
                    Paras[index].Value = Convert.ToInt32(htPara["@ReturnPerson"]);
                    index++;
                }
                if (htPara.ContainsKey("@StartDate"))
                {
                    strSql.Append(" AND sr.ReturnDate >= Convert(datetime,@StartDate)");
                    Paras[index] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                    Paras[index].Value = htPara["@StartDate"];
                    index++;
                }
                if (htPara.ContainsKey("@EndDate"))
                {
                    strSql.Append(" AND sr.ReturnDate<=Convert(datetime,@EndDate)");
                    Paras[index] = new SqlParameter("@EndDate", SqlDbType.DateTime);
                    Paras[index].Value = htPara["@EndDate"];
                    index++;
                }
                if (htPara.ContainsKey("@BillStatus"))
                {
                    strSql.Append(" AND sr.BillStatus=@BillStatus");
                    Paras[index] = new SqlParameter("@BillStatus", SqlDbType.VarChar);
                    Paras[index].Value = htPara["@BillStatus"];
                    index++;
                }
                if (htPara.ContainsKey("@FromBillID"))
                {
                    strSql.Append(" AND sr.FromBillID=@FromBillID");
                    Paras[index] = new SqlParameter("@FromBillID", SqlDbType.Int);
                    Paras[index].Value = htPara["@FromBillID"];
                    index++;
                }
                if (htPara.ContainsKey("@EFIndex"))
                {
                    strSql.Append(" AND sr.ExtField" + htPara["@EFIndex"] + " LIKE @EFDesc");
                    Paras[index] = new SqlParameter("@EFIndex", SqlDbType.VarChar);
                    Paras[index].Value = htPara["@EFIndex"];
                    index++;
                }
            }
            else
            { }
            #endregion


            return SqlHelper.CreateSqlByPageExcuteSql(strSql.ToString(), PageIndex, PageSize, OrderBy, Paras, ref TotalCount);

            // return        SqlHelper.ExecuteSql(strSql.ToString(), Paras);

        }


        /*不分页*/
        public static DataTable GetStorageReturnList(Hashtable htPara, string CompanyCD, string OrderBy)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT sr.ID, sr.ReturnNo,sr.Title,(SELECT si.StorageName from officedba.StorageInfo as si where si.id=sr.StorageID) AS StorageName,(SELECT ei.EmployeeName from officedba.EmployeeInfo as ei where ei.id=sr.ReturnPerson ) as ReturnName,sr.ReturnDate,(CASE sr.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,sb.BorrowNo  from officedba.StorageReturn as sr,officedba.StorageBorrow as sb where sr.FromBillID=sb.ID  AND sr.CompanyCD=@CompanyCD");
            SqlParameter[] Paras = new SqlParameter[htPara.Count + 1];
            Paras[0] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[0].Value = CompanyCD;
            #region 构造参数
            if (htPara.Count > 0)
            {
                int index = 1;
                if (htPara.ContainsKey("@ReturnNo"))
                {
                    strSql.Append(" AND sr.ReturnNo LIKE @ReturnNo");
                    Paras[index] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                    Paras[index].Value = htPara["@ReturnNo"];
                    index++;
                }
                if (htPara.ContainsKey("@ReturnTitle"))
                {
                    strSql.Append(" AND sr.Title LIKE @ReturnTitle");
                    Paras[index] = new SqlParameter("@ReturnTitle", SqlDbType.VarChar);
                    Paras[index].Value = htPara["@ReturnTitle"];
                    index++;
                }
                if (htPara.ContainsKey("@BorrowDeptID"))
                {
                    strSql.Append(" AND sb.DeptID=@BorrowDeptID ");
                    Paras[index] = new SqlParameter("@BorrowDeptID", SqlDbType.Int);
                    Paras[index].Value = Convert.ToInt32(htPara["@BorrowDeptID"]);
                    index++;
                }
                if (htPara.ContainsKey("@OutDeptID"))
                {
                    strSql.Append(" AND sb.OutDeptID=@OutDeptID");
                    Paras[index] = new SqlParameter("@OutDeptID", SqlDbType.Int);
                    Paras[index].Value = Convert.ToInt32(htPara["@OutDeptID"]);
                    index++;
                }
                if (htPara.ContainsKey("@StorageID"))
                {
                    strSql.Append(" AND sr.StorageID=@StorageID");
                    Paras[index] = new SqlParameter("@StorageID", SqlDbType.Int);
                    Paras[index].Value = Convert.ToInt32(htPara["@StorageID"]);
                    index++;
                }
                if (htPara.ContainsKey("@ReturnPerson"))
                {
                    strSql.Append(" AND sr.ReturnPerson=@ReturnPerson");
                    Paras[index] = new SqlParameter("@ReturnPerson", SqlDbType.Int);
                    Paras[index].Value = Convert.ToInt32(htPara["@ReturnPerson"]);
                    index++;
                }
                if (htPara.ContainsKey("@StartDate"))
                {
                    strSql.Append(" AND sr.ReturnDate >= Convert(datetime,@StartDate)");
                    Paras[index] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                    Paras[index].Value = htPara["@StartDate"];
                    index++;
                }
                if (htPara.ContainsKey("@EndDate"))
                {
                    strSql.Append(" AND sr.ReturnDate<=Convert(datetime,@EndDate)");
                    Paras[index] = new SqlParameter("@EndDate", SqlDbType.DateTime);
                    Paras[index].Value = htPara["@EndDate"];
                    index++;
                }
                if (htPara.ContainsKey("@BillStatus"))
                {
                    strSql.Append(" AND sr.BillStatus=@BillStatus");
                    Paras[index] = new SqlParameter("@BillStatus", SqlDbType.VarChar);
                    Paras[index].Value = htPara["@BillStatus"];
                    index++;
                }
                if (htPara.ContainsKey("@FromBillID"))
                {
                    strSql.Append(" AND sr.FromBillID=@FromBillID");
                    Paras[index] = new SqlParameter("@FromBillID", SqlDbType.Int);
                    Paras[index].Value = htPara["@FromBillID"];
                    index++;
                }
            }
            else
            { }

            strSql.Append(" ORDER BY  " + OrderBy);
            #endregion
            // return SqlHelper.CreateSqlByPageExcuteSql(strSql.ToString(), PageIndex, PageSize, OrderBy, Paras, ref TotalCount);
            return SqlHelper.ExecuteSql(strSql.ToString(), Paras);

        }
        #endregion

        #region 删除借货返还单 及其明细
        public static bool DelStorageReturn(string[] ID)
        {
            bool Flag = true;

            foreach (string tempID in ID)
            {
                StringBuilder strMainSql = new StringBuilder();
                strMainSql.Append("DELETE officedba.StorageReturnDetail WHERE CompanyCD=(SELECT sr1.CompanyCD FROM  officedba.StorageReturn as sr1  where sr1.ID=@ID) AND ReturnNo=(SELECT sr2.ReturnNo FROM officedba.StorageReturn as sr2 WHERE sr2.ID=@ID)");
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
                strSubSql.Append("DELETE officedba.StorageReturn WHERE ID=@ID");
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

        #region 读取借货返还单信息
        public static DataTable GetStorageReturnInfo(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT sr.ID,sr.Title,sr.ReturnNo,sr.FromBillID,sr.StorageID,sr.DeptID,(case sr.FromType when '1'then '借货单' end )as FromType,");
            strSql.Append(" isnull(sr.ExtField1,'')ExtField1,isnull(sr.ExtField2,'')ExtField2,isnull(sr.ExtField3,'')ExtField3,isnull(sr.ExtField4,'')ExtField4, ");
            strSql.Append("isnull(sr.ExtField5,'')ExtField5,isnull(sr.ExtField6,'')ExtField6,isnull(sr.ExtField7,'')ExtField7,isnull(sr.ExtField8,'')ExtField8, ");
            strSql.Append("isnull(sr.ExtField9,'')ExtField9,isnull(sr.ExtField10,'')ExtField10,");
            strSql.Append(" (select di.DeptName  from officedba.DeptInfo as di where di.id=sr.DeptID) AS DeptName,");
            strSql.Append(" sr.ReturnPerson,");
            strSql.Append(" (select ei1.EmployeeName FROM officedba.EmployeeInfo AS ei1 where ei1.id=sr.ReturnPerson) as ReturnPersonName,");
            strSql.Append(" CONVERT(VARCHAR(10),sr.ReturnDate, 21)ReturnDate,sr.TotalPrice,sr.CountTotal,sr.Summary,CONVERT(VARCHAR(10),sr.InDate, 21)InDate,sr.Transactor,");
            strSql.Append(" (SELECT ei2.EmployeeName FROM officedba.EmployeeInfo AS ei2 where ei2.id=sr.Transactor) as TransactorName,");
            strSql.Append(" sr.Remark,");
            strSql.Append(" (SELECT ei3.EmployeeName FROM officedba.EmployeeInfo as ei3 where ei3.id=sr.Creator) as Creator,");
            strSql.Append(" CONVERT(VARCHAR(10),sr.CreateDate, 21)CreateDate,sr.BillStatus,(CASE sr.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatusName ,");
            strSql.Append(" (SELECT ei4.EmployeeName from officedba.EmployeeInfo as ei4 where ei4.id=sr.Confirmor) as Confirmor,");
            strSql.Append(" CONVERT(VARCHAR(10),sr.ConfirmDate, 21)ConfirmDate,");
            strSql.Append(" (SELECT ei5.EmployeeName from officedba.EmployeeInfo as ei5 where ei5.id=sr.Closer) as Closer,");
            strSql.Append(" CONVERT(VARCHAR(10),sr.CloseDate, 21)CloseDate,sr.ModifiedUserID,CONVERT(VARCHAR(10),sr.ModifiedDate, 21)ModifiedDate,");
            strSql.Append(" sb.BorrowNo,sb.OutDeptID AS OutDept,");
            strSql.Append(" (SELECT di1.DeptName from officedba.deptInfo  as di1 where di1.id=sb.OutDeptID ) AS OutDeptName,");
            strSql.Append(" sb.Borrower,");
            strSql.Append(" (SELECT ei7.EmployeeName from officedba.EmployeeInfo as ei7 where ei7.id=sb.Borrower) as BorrowerName,");
            strSql.Append(" CONVERT(VARCHAR(10),sb.BorrowDate, 21)BorrowDate,");
            strSql.Append(" (SELECT si.StorageName from officedba.StorageInfo as si where si.id=sb.StorageID) AS StorageName ,");
            strSql.Append(" (SELECT di2.DeptName from officedba.DeptInfo as di2 where sb.DeptID=di2.ID) AS BorrowDeptName,sb.DeptID AS BorrowDept");
            strSql.Append(" FROM officedba.StorageReturn as sr left join officedba.StorageBorrow as sb on sr.FromBillID=sb.ID ");
            strSql.Append(" WHERE ");
            strSql.Append(" sr.ID=@ID ");
            SqlParameter[] Paras = { new SqlParameter("@ID", SqlDbType.Int) };
            Paras[0].Value = ID;
            return SqlHelper.ExecuteSql(strSql.ToString(), Paras);
        }
        #endregion

        #region 读取借货返还单明细
        public static DataTable GetStorageReturnDetailList(string CompanyCD, string ReturnNo)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select g.CodeName AS UsedUnitName,srd.BatchNo,srd.id,srd.SortNo,srd.ProductID,srd.ProductName,srd.UsedUnitID,srd.UsedUnitCount,srd.UsedPrice,srd.ExRate,");
            strSql.Append(" (SELECT pif1.ProdNo from officedba.ProductInfo as pif1 where srd.ProductID=pif1.ID) AS ProductNo,");
            strSql.Append(" (SELECT cut.CodeName from officedba.CodeUnitType AS cut where cut.id=srd.UnitID) as UnitName,");
            strSql.Append(" (SELECT pif.Specification FROM officedba.ProductInfo as pif where pif.ID=srd.ProductID) as ProductSpec,");
            strSql.Append(" srd.UnitID,srd.ReturnCount,srd.UnitPrice,srd.TotalPrice,srd.FromLineNo,srd.Remark,srd.ProductCount ,");
            strSql.Append(" (SELECT sb.BorrowNo from officedba.StorageBorrow as sb where sb.ID=srd.FromBillID) as BorrowNo ,sbd.RealReturnCount ");
            strSql.Append(" from officedba.StorageReturnDetail as srd inner join officedba.StorageBorrowDetail as sbd on srd.CompanyCD=sbd.CompanyCD AND srd.FromLineNo=sbd.SortNo and sbd.BorrowNo=(select sb1.BorrowNo from officedba.StorageBorrow as sb1 where sb1.ID=srd.FromBillID) ");
            strSql.Append(" LEFT JOIN officedba.CodeUnitType AS g ON g.ID =srd.UsedUnitID ");
            strSql.Append(" where srd.CompanyCD=@CompanyCD AND srd.ReturnNo=@ReturnNo ");
            SqlParameter[] Paras = { 
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@ReturnNo",SqlDbType.VarChar)
                                   };
            Paras[0].Value = CompanyCD;
            Paras[1].Value = ReturnNo;
            return SqlHelper.ExecuteSql(strSql.ToString(), Paras);
        }
        #endregion


        #region 更新借货返还单状态 并执行业务逻辑操作
        public static bool UpdateStatus(StorageReturn sr, string stype)
        {
            /***********************************
             * stype 1:确认 2：结单 3：取消结单
             * *********************************/
            StringBuilder sbUpdate = new StringBuilder();
            sbUpdate.Append("UPDATE officedba.StorageReturn SET ");
            if (stype == "1") //确认
            {

                #region 构造更新状态SQL
                sbUpdate.Append(" BillStatus=@BillStatus ,Confirmor=@Confirmor,ConfirmDate=@ConfirmDate ");
                sbUpdate.Append(", ModifiedDate=@ModifiedDate,ModifiedUserID=@ModifiedUserID ");
                sbUpdate.Append(" WHERE ID=@ID");

                SqlParameter[] Paras ={
                                         new SqlParameter("@BillStatus",SqlDbType.VarChar),
                                         new SqlParameter("@Confirmor",SqlDbType.Int),
                                         new SqlParameter("@ConfirmDate",SqlDbType.DateTime),
                                         new SqlParameter("@ID",SqlDbType.Int),
                                         new SqlParameter("@ModifiedDate",SqlDbType.DateTime),
                                         new SqlParameter("@ModifiedUserID",SqlDbType.VarChar)
                                     };
                Paras[0].Value = sr.BillStatus;
                Paras[1].Value = sr.Confirmor;
                Paras[2].Value = sr.ConfirmDate;
                Paras[3].Value = sr.ID;
                Paras[4].Value = sr.ModifiedDate;
                Paras[5].Value = sr.ModifiedUserID;

                #endregion


                ArrayList SqlCmdList = new ArrayList();
                SqlCommand srSqlCmd = new SqlCommand();
                srSqlCmd.CommandText = sbUpdate.ToString();
                srSqlCmd.Parameters.AddRange(Paras);
                SqlCmdList.Add(srSqlCmd);


                #region 构造 业务逻辑 实现SQL
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("SELECT srd.*,(SELECT sr.StorageID FROM officedba.StorageReturn as sr WHERE sr.ID=@ID ) AS StorageID   FROM officedba.StorageReturnDetail as srd ");
                sbSql.Append(" WHERE srd.CompanyCD=@CompanyCD AND srd.ReturnNo=@ReturnNo");
                SqlParameter[] sPara = { 
                                           new SqlParameter("@ID",SqlDbType.Int),
                                           new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                           new SqlParameter("@ReturnNo",SqlDbType.VarChar),
                                       };
                sPara[0].Value = sr.ID;
                sPara[1].Value = sr.CompanyCD;
                sPara[2].Value = sr.ReturnNo;


                DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), sPara);




                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        bool isBatchNo = false;
                        if (row["BatchNo"] != null || row["BatchNo"].ToString().ToLower() != "nobatch" || row["BatchNo"].ToString() != "")
                            isBatchNo = true;


                        /*更新被借仓库 更新 现有存量(现有库存不变)+ 借出量- */
                        StringBuilder SubSql = new StringBuilder();
                        SubSql.Append("UPDATE officedba.StorageProduct SET  OutCount=isnull(OutCount,0)-@ProductCount ");
                        SubSql.Append(" WHERE CompanyCD=@CompanyCD AND StorageID=@StorageID AND ProductID=@ProductID " + (isBatchNo ? "AND BatchNo=@BatchNo" : string.Empty));

                        SqlParameter[] subSqlParams = isBatchNo ? new SqlParameter[5] : new SqlParameter[4];
                        int index = 0;
                        subSqlParams[index++] = SqlHelper.GetParameter("@ProductCount", row["ReturnCount"].ToString());
                        subSqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", row["CompanyCD"].ToString());
                        subSqlParams[index++] = SqlHelper.GetParameter("@StorageID", row["StorageID"].ToString());
                        subSqlParams[index++] = SqlHelper.GetParameter("@ProductID", row["ProductID"].ToString());
                        if (isBatchNo)
                            subSqlParams[index++] = SqlHelper.GetParameter("@BatchNo", row["BatchNo"].ToString());

                        SqlCommand subSqlCmd = new SqlCommand();
                        subSqlCmd.CommandText = SubSql.ToString();
                        subSqlCmd.Parameters.AddRange(subSqlParams);
                        SqlCmdList.Add(subSqlCmd);
                        /*更新借货单明细中的 已返还数量+*/
                        StringBuilder sbUpdateBorrowDetail = new StringBuilder();
                        sbUpdateBorrowDetail.Append(" UPDATE officedba.StorageBorrowDetail set RealReturnCount=isnull(RealReturnCount,0)+@ReturnCount ");
                        sbUpdateBorrowDetail.Append(" WHERE CompanyCD=@CompanyCD AND SortNo=@SortNo AND BorrowNo=(SELECT sb.BorrowNo FROM officedba.StorageBorrow as sb WHERE sb.ID=@BorrowID )");
                        SqlParameter[] updateSqlParas = { 
                                                        new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                                        new SqlParameter("@SortNo",SqlDbType.Int),
                                                        new SqlParameter("@BorrowID",SqlDbType.Int),
                                                        new SqlParameter("@ReturnCount",SqlDbType.Decimal)};
                        updateSqlParas[0].Value = row["CompanyCD"].ToString();
                        updateSqlParas[1].Value = row["FromLineNo"].ToString();
                        updateSqlParas[2].Value = row["FromBillID"].ToString();
                        updateSqlParas[3].Value = row["ReturnCount"].ToString();

                        SqlCmdList.Add(SqlHelper.GetNewSqlCommond(sbUpdateBorrowDetail.ToString(), updateSqlParas));

                    }


                    bool result = SqlHelper.ExecuteTransWithArrayList(SqlCmdList);
                    return result;

                }
                else
                    return false;
                #endregion
            }
            else if (stype == "2") //结单
            {
                sbUpdate.Append("BillStatus=@BillStatus,Closer=@Closer,CloseDate=@CloseDate ");
                sbUpdate.Append(", ModifiedDate=@ModifiedDate,ModifiedUserID=@ModifiedUserID ");
                sbUpdate.Append(" WHERE ID=@ID ");
                SqlParameter[] Paras = { 
                                           new SqlParameter("@BillStatus",SqlDbType.VarChar),
                                           new SqlParameter("@Closer",SqlDbType.Int),
                                           new SqlParameter("@CloseDate",SqlDbType.DateTime),
                                           new SqlParameter("@ModifiedDate",SqlDbType.DateTime),
                                           new SqlParameter("@ModifiedUserID",SqlDbType.VarChar),
                                           new SqlParameter("@ID",SqlDbType.Int)
                                       };
                Paras[0].Value = sr.BillStatus;
                Paras[1].Value = sr.Closer;
                Paras[2].Value = sr.CloseDate;
                Paras[3].Value = sr.ModifiedDate;
                Paras[4].Value = sr.ModifiedUserID;
                Paras[5].Value = sr.ID;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Parameters.AddRange(Paras);
                sqlcmd.CommandText = sbUpdate.ToString();
                ArrayList sqlcmdlist = new ArrayList();
                sqlcmdlist.Add(sqlcmd);
                return SqlHelper.ExecuteTransWithArrayList(sqlcmdlist);
            }
            else if (stype == "3") //取消结单
            {
                sbUpdate.Append("BillStatus=@BillStatus, ModifiedDate=@ModifiedDate,ModifiedUserID=@ModifiedUserID ");
                sbUpdate.Append(" WHERE ID=@ID");
                SqlParameter[] Paras = { 
                                      new SqlParameter("@BillStatus",SqlDbType.VarChar),
                                      new  SqlParameter("@ModifiedDate",SqlDbType.DateTime),
                                      new SqlParameter("@ModifiedUserID",SqlDbType.VarChar),
                                       new SqlParameter("@ID",SqlDbType.Int)};
                Paras[0].Value = sr.BillStatus;
                Paras[1].Value = sr.ModifiedDate;
                Paras[2].Value = sr.ModifiedUserID;
                Paras[3].Value = sr.ID;

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Parameters.AddRange(Paras);
                sqlcmd.CommandText = sbUpdate.ToString();
                ArrayList sqlcmdlist = new ArrayList();
                sqlcmdlist.Add(sqlcmd);
                return SqlHelper.ExecuteTransWithArrayList(sqlcmdlist);
            }
            else
                return false;

        }
        #endregion



        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(StorageReturn model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.StorageReturn set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND ReturnNo = @ReturnNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@ReturnNo", model.ReturnNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion
    }
}

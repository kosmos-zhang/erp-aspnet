using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

using XBase.Data.DBHelper;
using XBase.Common;
using XBase.Model.Office.StorageManager;
namespace XBase.Data.Office.StorageManager
{
    public class StorageCheckDBHelper
    {
        #region 添加盘点单及其明细
        public static string StorageCheckAdd(StorageCheck sc, Hashtable htExtAttr, List<StorageCheckDetail> scdList)
        {

            #region 插入盘点单
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.StorageCheck(");
            strSql.Append("CompanyCD,CheckNo,Title,CheckStartDate,CheckEndDate,StorageID,DeptID,CheckType,Transactor,NowCount,CheckCount,DiffCount,NowMoney,CheckMoney,DiffMoney,Summary,Remark,Attachment,Creator,CreateDate,ModifiedDate,ModifiedUserID,BillStatus)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@CheckNo,@Title,@CheckStartDate,@CheckEndDate,@StorageID,@DeptID,@CheckType,@Transactor,@NowCount,@CheckCount,@DiffCount,@NowMoney,@CheckMoney,@DiffMoney,@Summary,@Remark,@Attachment,@Creator,@CreateDate,@ModifiedDate,@ModifiedUserID,@BillStatus)");
            strSql.Append(";select @ID=@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@CheckNo", SqlDbType.VarChar,50),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@CheckStartDate", SqlDbType.DateTime),
					new SqlParameter("@CheckEndDate", SqlDbType.DateTime),
					new SqlParameter("@StorageID", SqlDbType.Int,4),
					new SqlParameter("@DeptID", SqlDbType.Int,4),
					new SqlParameter("@CheckType", SqlDbType.Int,4),
					new SqlParameter("@Transactor", SqlDbType.Int,4),
					new SqlParameter("@NowCount", SqlDbType.Decimal,9),
					new SqlParameter("@CheckCount", SqlDbType.Decimal,9),
					new SqlParameter("@DiffCount", SqlDbType.Decimal,9),
					new SqlParameter("@NowMoney", SqlDbType.Decimal,9),
					new SqlParameter("@CheckMoney", SqlDbType.Decimal,9),
					new SqlParameter("@DiffMoney", SqlDbType.Decimal,9),
					new SqlParameter("@Summary", SqlDbType.VarChar,200),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@Attachment", SqlDbType.VarChar,100),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@ID",SqlDbType.Int),
                    new SqlParameter("@BillStatus",SqlDbType.VarChar)
                                       };
            parameters[0].Value = sc.CompanyCD;
            parameters[1].Value = sc.CheckNo;
            parameters[2].Value = sc.Title;
            parameters[3].Value = sc.CheckStartkDate;
            parameters[4].Value = sc.CheckEndDate;
            parameters[5].Value = sc.StorageID;
            parameters[6].Value = sc.DeptID;
            parameters[7].Value = sc.CheckType;
            parameters[8].Value = sc.Transactor;
            parameters[9].Value = sc.NowCount;
            parameters[10].Value = sc.CheckCount;
            parameters[11].Value = sc.DiffCount;
            parameters[12].Value = sc.NowMoney;
            parameters[13].Value = sc.CheckMoney;
            parameters[14].Value = sc.DiffMoney;
            parameters[15].Value = sc.Summary;
            parameters[16].Value = sc.Remark;
            parameters[17].Value = sc.Attachment;
            parameters[18].Value = sc.Creator;
            parameters[19].Value = sc.CreateDate;
            parameters[20].Value = sc.ModifiedDate;
            parameters[21].Value = sc.ModifiedUserID;
            parameters[22].Direction = ParameterDirection.Output;
            parameters[23].Value = sc.BillStatus;
            #endregion

            SqlCommand SqlMainCmd = new SqlCommand();
            SqlMainCmd.CommandText = strSql.ToString();
            SqlMainCmd.Parameters.AddRange(parameters);

            ArrayList SqlCmdList = new ArrayList();
            SqlCmdList.Add(SqlMainCmd);

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(sc, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                SqlCmdList.Add(cmd);
            #endregion

            #region 盘点单明细
            foreach (StorageCheckDetail scd in scdList)
            {
                StringBuilder strSubSql = new StringBuilder();
                strSubSql.Append("insert into officedba.StorageCheckDetail(");
                strSubSql.Append("CompanyCD,CheckNo,SortNo,ProductID,UnitID,NowCount,CheckCount,DiffCount,DiffType,Remark,ModifiedDate,ModifiedUserID,BatchNo,UsedUnitID,UsedUnitCount,UsedPrice,ExRate)");
                strSubSql.Append(" values (");
                strSubSql.Append("@CompanyCD,@CheckNo,@SortNo,@ProductID,@UnitID,@NowCount,@CheckCount,@DiffCount,@DiffType,@Remark,@ModifiedDate,@ModifiedUserID,@BatchNo,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate)");
                strSubSql.Append(";select @@IDENTITY");
                SqlParameter[] subParas = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@CheckNo", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@UnitID", SqlDbType.Int,4),//基本单位
					new SqlParameter("@NowCount", SqlDbType.Decimal,9),
					new SqlParameter("@CheckCount", SqlDbType.Decimal,9),//基本数量
					new SqlParameter("@DiffCount", SqlDbType.Decimal,9),
					new SqlParameter("@DiffType", SqlDbType.Char,1),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),

                    new SqlParameter("@BatchNo", SqlDbType.VarChar,50),//批次
                    new SqlParameter("@UsedUnitID",SqlDbType.Int,4),//实际单位
                    new SqlParameter("@UsedUnitCount", SqlDbType.Decimal,9),//实际数量
                    new SqlParameter("@UsedPrice", SqlDbType.Decimal,9),//实际单价
                    new SqlParameter("@ExRate", SqlDbType.Decimal,9)//比率                            
                                          };
                subParas[0].Value = scd.CompanyCD;
                subParas[1].Value = scd.CheckNo;
                subParas[2].Value = scd.SortNo;
                subParas[3].Value = scd.ProductID;
                if (scd.UnitID != null)
                    subParas[4].Value = scd.UnitID;
                else
                    subParas[4].Value = DBNull.Value;
                subParas[5].Value = scd.NowCount;
                subParas[6].Value = scd.CheckCount;
                subParas[7].Value = scd.DiffCount;
                subParas[8].Value = scd.DiffType;
                subParas[9].Value = scd.Remark;
                subParas[10].Value = scd.ModifiedDate;
                subParas[11].Value = scd.ModifiedUserID;
                if (scd.BatchNo != null)
                    subParas[12].Value = scd.BatchNo;
                else
                    subParas[12].Value = DBNull.Value;
                if (scd.UsedUnitID != null)
                    subParas[13].Value = scd.UsedUnitID;
                else
                    subParas[13].Value = DBNull.Value;
                if (scd.UsedUnitCount != null)
                    subParas[14].Value = scd.UsedUnitCount;
                else
                    subParas[14].Value = DBNull.Value;
                if (scd.UsedPrice != null)
                    subParas[15].Value = scd.UsedPrice;
                else
                    subParas[15].Value = DBNull.Value;
                if (scd.ExRate != null)
                    subParas[16].Value = scd.ExRate;
                else
                    subParas[16].Value = DBNull.Value;
                SqlCommand sqlSubCmd = new SqlCommand();
                sqlSubCmd.CommandText = strSubSql.ToString();
                sqlSubCmd.Parameters.AddRange(subParas);

                SqlCmdList.Add(sqlSubCmd);
            }
            #endregion

            bool result = SqlHelper.ExecuteTransWithArrayList(SqlCmdList);
            if (result)
            {
                return "1|" + ((SqlCommand)SqlCmdList[0]).Parameters["@ID"].Value.ToString() + "#" + sc.CheckNo;
            }
            else
                return "3|保存数据失败";
        }
        #endregion

        #region 更新盘点单及其明细
        public static string StorageCheckUpdate(StorageCheck sc, Hashtable htExtAttr, List<StorageCheckDetail> scdList)
        {

            #region 盘点单
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageCheck set ");
            strSql.Append("Title=@Title,");
            strSql.Append("CheckStartDate=@CheckStartDate,");
            strSql.Append("CheckEndDate=@CheckEndDate,");
            strSql.Append("StorageID=@StorageID,");
            strSql.Append("DeptID=@DeptID,");
            strSql.Append("CheckType=@CheckType,");
            strSql.Append("Transactor=@Transactor,");
            strSql.Append("NowCount=@NowCount,");
            strSql.Append("CheckCount=@CheckCount,");
            strSql.Append("DiffCount=@DiffCount,");
            strSql.Append("NowMoney=@NowMoney,");
            strSql.Append("CheckMoney=@CheckMoney,");
            strSql.Append("DiffMoney=@DiffMoney,");
            strSql.Append("Summary=@Summary,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("BillStatus=@BillStatus,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@CheckStartDate", SqlDbType.DateTime),
					new SqlParameter("@CheckEndDate", SqlDbType.DateTime),
					new SqlParameter("@StorageID", SqlDbType.Int,4),
					new SqlParameter("@DeptID", SqlDbType.Int,4),
					new SqlParameter("@CheckType", SqlDbType.Int,4),
					new SqlParameter("@Transactor", SqlDbType.Int,4),
					new SqlParameter("@NowCount", SqlDbType.Decimal,9),
					new SqlParameter("@CheckCount", SqlDbType.Decimal,9),
					new SqlParameter("@DiffCount", SqlDbType.Decimal,9),
					new SqlParameter("@NowMoney", SqlDbType.Decimal,9),
					new SqlParameter("@CheckMoney", SqlDbType.Decimal,9),
					new SqlParameter("@DiffMoney", SqlDbType.Decimal,9),
					new SqlParameter("@Summary", SqlDbType.VarChar,200),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@Attachment", SqlDbType.VarChar,100),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50)};
            parameters[0].Value = sc.ID;
            parameters[1].Value = sc.Title;
            parameters[2].Value = sc.CheckStartkDate;
            parameters[3].Value = sc.CheckEndDate;
            parameters[4].Value = sc.StorageID;
            parameters[5].Value = sc.DeptID;
            parameters[6].Value = sc.CheckType;
            parameters[7].Value = sc.Transactor;
            parameters[8].Value = sc.NowCount;
            parameters[9].Value = sc.CheckCount;
            parameters[10].Value = sc.DiffCount;
            parameters[11].Value = sc.NowMoney;
            parameters[12].Value = sc.CheckMoney;
            parameters[13].Value = sc.DiffMoney;
            parameters[14].Value = sc.Summary;
            parameters[15].Value = sc.Remark;
            parameters[16].Value = sc.Attachment;
            parameters[17].Value = sc.BillStatus;
            parameters[18].Value = sc.ModifiedDate;
            parameters[19].Value = sc.ModifiedUserID;

            #endregion

            SqlCommand sqlMainCmd = new SqlCommand();
            sqlMainCmd.CommandText = strSql.ToString();
            sqlMainCmd.Parameters.AddRange(parameters);
            ArrayList SqlCmdList = new ArrayList();
            SqlCmdList.Add(sqlMainCmd);
            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(sc, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                SqlCmdList.Add(cmd);
            #endregion

            #region  明细
            StringBuilder strDel = new StringBuilder();
            strDel.Append("DELETE officedba.StorageCheckDetail  where CompanyCD=@CompanyCD AND CheckNo=@CheckNo");
            SqlParameter[] delParas = { 
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@CheckNo",SqlDbType.VarChar)
                                   };
            delParas[0].Value = sc.CompanyCD;
            delParas[1].Value = sc.CheckNo;

            SqlCommand sqlDelCmd = new SqlCommand();
            sqlDelCmd.CommandText = strDel.ToString();
            sqlDelCmd.Parameters.AddRange(delParas);
            SqlCmdList.Add(sqlDelCmd);

            foreach (StorageCheckDetail scd in scdList)
            {
                StringBuilder strSubSql = new StringBuilder();
                strSubSql.Append("insert into officedba.StorageCheckDetail(");
                strSubSql.Append("CompanyCD,CheckNo,SortNo,ProductID,UnitID,NowCount,CheckCount,DiffCount,DiffType,Remark,ModifiedDate,ModifiedUserID,BatchNo,UsedUnitID,UsedUnitCount,UsedPrice,ExRate)");
                strSubSql.Append(" values (");
                strSubSql.Append("@CompanyCD,@CheckNo,@SortNo,@ProductID,@UnitID,@NowCount,@CheckCount,@DiffCount,@DiffType,@Remark,@ModifiedDate,@ModifiedUserID,@BatchNo,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate)");
                strSubSql.Append(";select @@IDENTITY");
                SqlParameter[] subParas = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@CheckNo", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@UnitID", SqlDbType.Int,4),//基本单位
					new SqlParameter("@NowCount", SqlDbType.Decimal,9),
					new SqlParameter("@CheckCount", SqlDbType.Decimal,9),//基本数量
					new SqlParameter("@DiffCount", SqlDbType.Decimal,9),
					new SqlParameter("@DiffType", SqlDbType.Char,1),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),

                    new SqlParameter("@BatchNo", SqlDbType.VarChar,50),//批次
                    new SqlParameter("@UsedUnitID",SqlDbType.Int,4),//实际单位
                    new SqlParameter("@UsedUnitCount", SqlDbType.Decimal,9),//实际数量
                    new SqlParameter("@UsedPrice", SqlDbType.Decimal,9),//实际单价
                    new SqlParameter("@ExRate", SqlDbType.Decimal,9)//比率                            
                                          };
                subParas[0].Value = scd.CompanyCD;
                subParas[1].Value = scd.CheckNo;
                subParas[2].Value = scd.SortNo;
                subParas[3].Value = scd.ProductID;
                if (scd.UnitID != null)
                    subParas[4].Value = scd.UnitID;
                else
                    subParas[4].Value = DBNull.Value;
                subParas[5].Value = scd.NowCount;
                subParas[6].Value = scd.CheckCount;
                subParas[7].Value = scd.DiffCount;
                subParas[8].Value = scd.DiffType;
                subParas[9].Value = scd.Remark;
                subParas[10].Value = scd.ModifiedDate;
                subParas[11].Value = scd.ModifiedUserID;
                if (scd.BatchNo != null)
                    subParas[12].Value = scd.BatchNo;
                else
                    subParas[12].Value = DBNull.Value;
                if (scd.UsedUnitID != null)
                    subParas[13].Value = scd.UsedUnitID;
                else
                    subParas[13].Value = DBNull.Value;
                if (scd.UsedUnitCount != null)
                    subParas[14].Value = scd.UsedUnitCount;
                else
                    subParas[14].Value = DBNull.Value;
                if (scd.UsedPrice != null)
                    subParas[15].Value = scd.UsedPrice;
                else
                    subParas[15].Value = DBNull.Value;
                if (scd.ExRate != null)
                    subParas[16].Value = scd.ExRate;
                else
                    subParas[16].Value = DBNull.Value;
                SqlCommand sqlSubCmd = new SqlCommand();
                sqlSubCmd.CommandText = strSubSql.ToString();
                sqlSubCmd.Parameters.AddRange(subParas);

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
        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(StorageCheck sc, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.StorageCheck set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND CheckNo = @CheckNo";
                cmd.Parameters.AddWithValue("@CompanyCD", sc.CompanyCD);
                cmd.Parameters.AddWithValue("@CheckNo", sc.CheckNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion
        #region 读取盘点单
        public static DataTable StorageCheckGet(StorageCheck sc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT sc.ID,sc.CompanyCD,sc.CheckNo,sc.Title,Convert(varchar(10),sc.CheckStartDate,120)CheckStartDate,Convert(varchar(10),sc.CheckEndDate,120)CheckEndDate,sc.CheckUserID, Convert(varchar(10),sc.CheckDate,120)CheckDate,");
            strSql.Append("sc.StorageID,ISNULL(sc.ExtField1,'')ExtField1,ISNULL(sc.ExtField2,'')ExtField2,ISNULL(sc.ExtField3,'')ExtField3,ISNULL(sc.ExtField4,'')ExtField4,ISNULL(sc.ExtField5,'')ExtField5,ISNULL(sc.ExtField6,'')ExtField6,ISNULL(sc.ExtField7,'')ExtField7,ISNULL(sc.ExtField8,'')ExtField8,ISNULL(sc.ExtField9,'')ExtField9,ISNULL(sc.ExtField10,'')ExtField10,   ");
            strSql.Append(" (select si.StorageName from officedba.StorageInfo as si where si.ID=sc.StorageID ) AS StorageName,");
            strSql.Append(" sc.DeptID,");
            strSql.Append(" (SELECT di.DeptName from officedba.DeptInfo as di where di.ID=sc.DeptID) AS DeptName,");
            strSql.Append(" sc.CheckType,sc.Transactor,");
            strSql.Append(" (select x.TypeName from officedba.CodePublicType as x where x.ID=sc.CheckType ) AS CheckTypeName,");//CheckTypeName
            strSql.Append(" (select ei.EmployeeName from officedba.EmployeeInfo as ei where ei.ID=sc.Transactor) as TransactorName,");
            strSql.Append(" sc.NowCount,sc.CheckCount,sc.DiffCount,sc.NowMoney,sc.CheckMoney,sc.DiffMoney,sc.Summary,sc.Remark,sc.Attachment,");
            strSql.Append(" sc.Creator,");
            strSql.Append(" (select ei1.EmployeeName from officedba.EmployeeInfo as ei1 where ei1.ID=sc.Creator) as CreatorName,");
            strSql.Append(" Convert(varchar(10),sc.CreateDate,120)CreateDate,sc.BillStatus,sc.CheckUserID,");
            strSql.Append("case sc.BillStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'");
            strSql.Append("when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName,   ");
            strSql.Append(" (select ei2.EmployeeName from officedba.EmployeeInfo as ei2 where ei2.ID=sc.CheckUserID) as CheckUserName,");
            strSql.Append(" Convert(varchar(10),sc.CheckDate,120)CheckDate,sc.Confirmor,");
            strSql.Append(" (select ei3.EmployeeName from officedba.EmployeeInfo as ei3 where ei3.ID=sc.Confirmor) as ConfirmorName,");
            strSql.Append(" Convert(varchar(10),sc.ConfirmDate,120)ConfirmDate,sc.Closer,");
            strSql.Append(" (select ei4.EmployeeName from officedba.EmployeeInfo as ei4 where ei4.ID=sc.Closer) as CloserName,");
            strSql.Append(" Convert(varchar(10),sc.CloseDate,120)CloseDate,Convert(varchar(10),sc.ModifiedDate,120)ModifiedDate,sc.ModifiedUserID");
            strSql.Append("  FROM officedba.StorageCheck AS sc");
            strSql.Append(" where sc.ID=@ID");
            SqlParameter[] Paras = { new SqlParameter("@ID", SqlDbType.Int) };
            Paras[0].Value = sc.ID;

            return SqlHelper.ExecuteSql(strSql.ToString(), Paras);
        }

        /*打印SQL*/
        public static DataTable StorageCheckInfoPrint(StorageCheck sc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("  SELECT     ID, CompanyCD, CheckNo, Title, CheckStartDate, CheckEndDate, CheckUserID, CheckDate, StorageID,   ");
            strSql.AppendLine("  (case sc.CheckType when 0 then '' else (select ISNULL(ct.TypeName,'') from officedba.CodePublicType ct where sc.CheckType=ct.ID) end) as CheckTypeText,--查的是公共分类表   ");
            strSql.AppendLine("  (CASE sc.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '4' THEN '手工结单' WHEN '5' THEN '自动结单' END) AS BillStatusText,  ");
            strSql.AppendLine("  (SELECT     StorageName  ");
            strSql.AppendLine("  FROM          officedba.StorageInfo AS si  ");
            strSql.AppendLine("  WHERE      (ID = sc.StorageID)) AS StorageName, DeptID,  ");
            strSql.AppendLine("  (SELECT     DeptName  ");
            strSql.AppendLine("  FROM          officedba.DeptInfo AS di  ");
            strSql.AppendLine("  WHERE      (ID = sc.DeptID)) AS DeptName, CheckType, Transactor,  ");
            strSql.AppendLine("  (SELECT     EmployeeName  ");
            strSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei  ");
            strSql.AppendLine("  WHERE      (ID = sc.Transactor)) AS TransactorName, NowCount, CheckCount, DiffCount, NowMoney, CheckMoney, DiffMoney, Summary, Remark,   ");
            strSql.AppendLine("  Attachment, Creator,  ");
            strSql.AppendLine("   (SELECT     EmployeeName  ");
            strSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei1  ");
            strSql.AppendLine("  WHERE      (ID = sc.Creator)) AS CreatorName, CreateDate, BillStatus, CheckUserID AS Expr1,  ");
            strSql.AppendLine("  (SELECT     EmployeeName  ");
            strSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei2  ");
            strSql.AppendLine("  WHERE      (ID = sc.CheckUserID)) AS CheckUserName, CheckDate AS Expr2, Confirmor,  ");
            strSql.AppendLine("  (SELECT     EmployeeName  ");
            strSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei3  ");
            strSql.AppendLine("  WHERE      (ID = sc.Confirmor)) AS ConfirmorName, ConfirmDate, Closer,  ");
            strSql.AppendLine("  (SELECT     EmployeeName  ");
            strSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei4  ");
            strSql.AppendLine("  WHERE      (ID = sc.Closer)) AS CloserName, CloseDate, ModifiedDate, ModifiedUserID  ");
            strSql.AppendLine("  FROM         officedba.StorageCheck AS sc  ");
            strSql.Append(" where sc.ID=@ID");
            SqlParameter[] Paras = { new SqlParameter("@ID", SqlDbType.Int) };
            Paras[0].Value = sc.ID;

            return SqlHelper.ExecuteSql(strSql.ToString(), Paras);
        }
        #endregion

        #region 更新状态
        public static string UpdateStorageCheckStatus(int stype, Model.Office.StorageManager.StorageCheck sc)
        {
            if (stype == 1) //确认订单 
            {
                #region 确认单据
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update officedba.StorageCheck set ");
                strSql.Append("BillStatus=@BillStatus,");
                strSql.Append("Confirmor=@Confirmor,");
                strSql.Append("ConfirmDate=@ConfirmDate,");
                strSql.Append("ModifiedDate=@ModifiedDate,");
                strSql.Append("ModifiedUserID=@ModifiedUserID");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@Confirmor", SqlDbType.Int,4),
					new SqlParameter("@ConfirmDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@ID",SqlDbType.Int)
                                            };
                parameters[0].Value = sc.BillStatus;
                parameters[1].Value = sc.Confirmor;
                parameters[2].Value = sc.ConfirmDate;
                parameters[3].Value = sc.ModifiedDate;
                parameters[4].Value = sc.ModifiedUserID;
                parameters[5].Value = sc.ID;


                if (SqlHelper.ExecuteTransSql(strSql.ToString(), parameters) > 0)
                {
                    return "1|";
                }
                else
                {
                    return "0|";
                }
                #endregion
            }
            else if (stype == 2)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update officedba.StorageCheck set ");
                strSql.Append("BillStatus=@BillStatus,");
                strSql.Append("Closer=@Closer,");
                strSql.Append("CloseDate=@CloseDate,");
                strSql.Append("ModifiedDate=@ModifiedDate,");
                strSql.Append("ModifiedUserID=@ModifiedUserID");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@Closer", SqlDbType.Int,4),
					new SqlParameter("@CloseDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@ID",SqlDbType.Int)
                                            };
                parameters[0].Value = sc.BillStatus;
                parameters[1].Value = sc.Closer;
                parameters[2].Value = sc.CloseDate;
                parameters[3].Value = sc.ModifiedDate;
                parameters[4].Value = sc.ModifiedUserID;
                parameters[5].Value = sc.ID;
                if (SqlHelper.ExecuteTransSql(strSql.ToString(), parameters) > 0)
                    return "1|";
                else
                    return "0|";
            }
            else if (stype == 3 || stype == 4)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update officedba.StorageCheck set ");
                strSql.Append("BillStatus=@BillStatus,");
                strSql.Append("ModifiedDate=@ModifiedDate,");
                strSql.Append("ModifiedUserID=@ModifiedUserID");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@ID",SqlDbType.Int)
                                            };
                parameters[0].Value = sc.BillStatus;
                parameters[1].Value = sc.ModifiedDate;
                parameters[2].Value = sc.ModifiedUserID;
                parameters[3].Value = sc.ID;



                List<SqlCommand> SqlCmdList = new List<SqlCommand>();
                SqlCommand sqlUpdate = SqlHelper.GetNewSqlCommond(strSql.ToString(), parameters);
                SqlCmdList.Add(sqlUpdate);
                if (stype == 4)
                {
                    IList<SqlCommand> tempList = Data.Common.FlowDBHelper.GetCancelConfirmSqlCommond(sc.CompanyCD, Convert.ToInt32(ConstUtil.BILL_TYPEFLAG_STORAGE), Convert.ToInt32(ConstUtil.BILL_TYPECODE_STORAGE_CHECK), sc.ID, sc.ModifiedUserID);
                    foreach (SqlCommand scmd in tempList)
                    {
                        SqlCmdList.Add(scmd);
                    }
                }
                if (SqlHelper.ExecuteTransWithCollections(SqlCmdList))
                    return "1|";
                else
                    return "0|";
            }
            else
            {
                return "0|";
            }
        }
        #endregion

        #region 库存调整
        public static string StorageCheck(StorageCheck sc)
        {
            #region 读取盘点单信息
            StringBuilder strMainSql = new StringBuilder();
            strMainSql.Append("SELECT * FROM officedba.StorageCheck  WHERE ID=@ID");
            SqlParameter[] MainParas = { new SqlParameter("@ID", SqlDbType.Int) };
            MainParas[0].Value = sc.ID;

            DataTable dtStorage = SqlHelper.ExecuteSql(strMainSql.ToString(), MainParas);
            DataRow MainRow = dtStorage.Rows[0];
            #endregion


            #region 读取明细
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,ISNULL(b.StandardCost,0)StandardCost from officedba.StorageCheckDetail  a ");
            strSql.Append("left outer join officedba.productinfo b ");
            strSql.Append("on a.ProductID=b.id where a.CompanyCD=@CompanyCD and a.CheckNo=@CheckNo ");
            SqlParameter[] Paras = { 
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@CheckNo",SqlDbType.VarChar)
                                   };
            Paras[0].Value = sc.CompanyCD;
            Paras[1].Value = MainRow["CheckNo"];

            DataTable dtDetail = SqlHelper.ExecuteSql(strSql.ToString(), Paras);

            #endregion


            ArrayList SqlCmdList = new ArrayList();

            #region 更新盘点单 库存调整人及调整时间
            StringBuilder SqlMain = new StringBuilder();
            SqlMain.Append("UPDATE officedba.StorageCheck SET CheckUserID=@CheckUserID,CheckDate=@CheckDate");
            SqlMain.Append(" WHERE ID=@ID ");
            SqlParameter[] sPara = {
                                       new SqlParameter("@CheckUserID",SqlDbType.Int),
                                       new SqlParameter("@CheckDate",SqlDbType.DateTime),
                                       new SqlParameter("@ID",SqlDbType.Int)
                                   };
            sPara[0].Value = sc.CheckUserID;
            sPara[1].Value = sc.CloseDate;
            sPara[2].Value = sc.ID;
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandText = SqlMain.ToString();
            sqlcmd.Parameters.AddRange(sPara);
            SqlCmdList.Add(sqlcmd);

            #endregion


            foreach (DataRow row in dtDetail.Rows)
            {
                StringBuilder strSubSql = new StringBuilder();
                strSubSql.Append("UPDATE officedba.StorageProduct SET");
                strSubSql.Append(" ProductCount=@ProductCount ");
                strSubSql.Append(" WHERE CompanyCD=@CompanyCD AND ");
                strSubSql.Append(" StorageID=@StorageID AND ");
                strSubSql.Append(" ProductID=@ProductID");
                if (row["BatchNo"].ToString() != "")
                    strSubSql.Append(" and BatchNo='" + row["BatchNo"].ToString().Trim() + "' ");
                else
                    strSubSql.Append(" and BatchNo is null ");

                SqlParameter[] SubParas = { 
                                              new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                              new SqlParameter("@StorageID",SqlDbType.Int),
                                              new SqlParameter("@ProductID",SqlDbType.Int),
                                              new SqlParameter("@ProductCount",SqlDbType.Decimal)
                                          };
                SubParas[0].Value = row["CompanyCD"];
                SubParas[1].Value = MainRow["StorageID"];
                SubParas[2].Value = row["ProductID"];
                SubParas[3].Value = row["CheckCount"];

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = strSubSql.ToString();
                sqlCmd.Parameters.AddRange(SubParas);
                SqlCmdList.Add(sqlCmd);

                #region 操作库存流水账
                StorageAccountModel AccountM_ = new StorageAccountModel();
                AccountM_.BatchNo = row["BatchNo"].ToString();
                AccountM_.BillNo = row["CheckNo"].ToString();
                AccountM_.BillType = 15;
                AccountM_.CompanyCD = row["CompanyCD"].ToString();
                AccountM_.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                AccountM_.HappenCount = Convert.ToDecimal(row["CheckCount"].ToString());
                AccountM_.HappenDate = System.DateTime.Now;
                AccountM_.PageUrl = "../Office/StorageManager/StorageCheckSave.aspx";
                AccountM_.Price = Convert.ToDecimal(row["StandardCost"].ToString());
                AccountM_.ProductCount = Convert.ToDecimal(row["CheckCount"].ToString());
                AccountM_.ProductID = Convert.ToInt32(row["ProductID"].ToString());
                AccountM_.StorageID = Convert.ToInt32(dtStorage.Rows[0]["StorageID"].ToString());

                #region sql
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO officedba.StorageAccount");
                sql.AppendLine("           (CompanyCD");
                sql.AppendLine("           ,BillType");
                sql.AppendLine("           ,ProductID");
                sql.AppendLine("           ,StorageID");
                sql.AppendLine("           ,BatchNo");
                sql.AppendLine("           ,BillNo");
                sql.AppendLine("           ,HappenDate");
               // sql.AppendLine("           ,HappenCount");
                sql.AppendLine("           ,ProductCount");
                sql.AppendLine("           ,Creator");
                sql.AppendLine("           ,Price");
                sql.AppendLine("           ,PageUrl)");
                sql.AppendLine("     VALUES");
                sql.AppendLine("           (@CompanyCD");
                sql.AppendLine("           ,@BillType");
                sql.AppendLine("           ,@ProductID");
                sql.AppendLine("           ,@StorageID");
                sql.AppendLine("           ,@BatchNo");
                sql.AppendLine("           ,@BillNo");
                sql.AppendLine("           ,getdate()");
                //sql.AppendLine("           ,@HappenCount");
                sql.AppendLine("           ,@ProductCount");
                sql.AppendLine("           ,@Creator");
                sql.AppendLine("           ,@Price");
                sql.AppendLine("           ,@PageUrl)");

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sql.ToString();
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", AccountM_.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillType", AccountM_.BillType.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", AccountM_.ProductID.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", AccountM_.StorageID.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", AccountM_.BatchNo));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillNo", AccountM_.BillNo));
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@HappenCount", model.HappenCount.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount", AccountM_.ProductCount.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", AccountM_.Creator.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Price", AccountM_.Price.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PageUrl", AccountM_.PageUrl));
                #endregion
                //SqlCommand AccountCom_ = StorageAccountDBHelper.InsertStorageAccountCommand(AccountM_,"1");
                SqlCmdList.Add(comm);
                #endregion
            }

            bool result = SqlHelper.ExecuteTransWithArrayList(SqlCmdList);
            if (result)
                return "0";
            else
                return "1";

        }
        #endregion

        #region 读取盘点单明细
        public static DataTable GetStorageCheckDetail(StorageCheck sc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select scd.SortNo,scd.ProductID,ISNULL(p.IsBatchNo,'')IsBatchNo,ISNULL(scd.BatchNo,'')BatchNo,ISNULL(scd.UsedUnitID,0)UsedUnitID,ISNULL(scd.UsedUnitCount,0.00)UsedUnitCount,ISNULL(scd.UsedPrice,0.00)UsedPrice,ISNULL(scd.ExRate,0.00)ExRate,");
            strSql.Append("scd.UnitID,scd.NowCount,scd.CheckCount,scd.DiffCount,");
            strSql.Append("(SELECT u.CodeName from officedba.CodeUnitType as u where scd.UnitID=u.ID) AS UnitName,");
            strSql.Append("(SELECT ii.CodeName from officedba.CodeUnitType as ii where scd.UsedUnitID=ii.ID) AS UsedUnitName,");
            strSql.Append("scd.DiffType,case scd.DiffType when '0' then '正常' when '1' then '盘盈' when '2' then '盘亏' else '' end DiffTypeName,scd.Remark,scd.ModifiedDate,scd.ModifiedUserID,");
            strSql.Append("p.ProdNo,p.ProductName,p.Specification,isnull(p.StandardCost,0) as StandardCost ");
            strSql.Append("from officedba.StorageCheckDetail as scd ");
            strSql.Append("LEFT JOIN officedba.ProductInfo AS p on scd.ProductID=P.ID ");
            strSql.Append("WHERE scd.CompanyCD=@CompanyCD AND scd.CheckNo=@CheckNo");

            SqlParameter[] Paras = { 
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@CheckNo",SqlDbType.VarChar)
                                   };
            Paras[0].Value = sc.CompanyCD;
            Paras[1].Value = sc.CheckNo;

            return SqlHelper.ExecuteSql(strSql.ToString(), Paras);

        }

        /*打印SQL*/
        public static DataTable GetStorageCheckDetailPrint(StorageCheck sc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("  SELECT     scd.SortNo, scd.ProductID, scd.UnitID, scd.NowCount, scd.CheckCount, scd.DiffCount,   ");
            strSql.AppendLine("  (CASE scd.DiffType WHEN '0' THEN '正常' WHEN '1' THEN '盘盈' WHEN '2' THEN '盘亏' END) AS DiffTypeText,  ");
            strSql.AppendLine("  (SELECT     CodeName  ");
            strSql.AppendLine("  FROM          officedba.CodeUnitType AS u  ");
            strSql.AppendLine("  WHERE      (scd.UnitID = ID)) AS UnitName, scd.DiffType, scd.Remark, scd.ModifiedDate, scd.ModifiedUserID, p.ProdNo, p.ProductName,   ");
            strSql.AppendLine("  p.Specification, p.StandardCost  ");
            strSql.AppendLine("  FROM         officedba.StorageCheckDetail AS scd LEFT OUTER JOIN  ");
            strSql.AppendLine("  officedba.ProductInfo AS p ON scd.ProductID = p.ID  ");
            strSql.AppendLine("WHERE scd.CompanyCD=@CompanyCD AND scd.CheckNo=@CheckNo");

            SqlParameter[] Paras = { 
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@CheckNo",SqlDbType.VarChar)
                                   };
            Paras[0].Value = sc.CompanyCD;
            Paras[1].Value = sc.CheckNo;

            return SqlHelper.ExecuteSql(strSql.ToString(), Paras);

        }
        #endregion


        #region 读取盘点单列表
        public static DataTable GetStorageCheckList(Hashtable htPara, string EFIndex, string EFDesc,string BatchNo, int PageIndex, int PageSize, string OrderBy, ref int TotalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct TMP.ID,TMP.CheckNo,TMP.Title,TMP.DeptID,TMP.DeptName,TMP.Transactor,");
            strSql.Append(" TMP.TransactorName,TMP.StorageID,TMP.StorageName,TMP.CheckStartDate,TMP.CheckType,TMP.DiffCount,TMP.BillStatus,TMP.FlowStatus from ( ");
            strSql.Append("select  * from (");
            strSql.Append(" select sc.ID,sc.CheckNo,sc.Title,sc.DeptID,sc.Transactor,");
            strSql.Append("(select ei.EmployeeName FROM officedba.EmployeeInfo as ei WHERE ei.ID=sc.Transactor ) as TransactorName,");
            strSql.Append("(select di.DeptName from officedba.DeptInfo as di where di.ID=sc.DeptID) AS DeptName,");
            strSql.Append("StorageID,");
            strSql.Append("(SELECT si.StorageName from officedba.StorageInfo as si where si.ID=sc.StorageID) AS StorageName,");
            strSql.Append("(SELECT x.BatchNo from officedba.StorageCheckDetail as x where x.CheckNo=sc.CheckNo and x.CompanyCD=sc.CompanyCD ");
            strSql.Append(" ) AS BatchNo,");
            strSql.Append("CheckStartDate,(case sc.CheckType when 0 then '' else (select ISNULL(ct.TypeName,'') from officedba.CodePublicType ct where sc.CheckType=ct.ID) end) as CheckType,");
            strSql.Append("DiffCount, (CASE sC.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,");
            strSql.Append(" ( select top 1 fi.FlowStatus   from  officedba.FlowInstance AS FI  where fi.BillID=sc.ID  and fi.CompanyCD=sc.CompanyCD  AND fi.BillTypeCode=" + ConstUtil.BILL_TYPECODE_STORAGE_CHECK + "  AND fi.BillTypeFlag=" + ConstUtil.BILL_TYPEFLAG_STORAGE + " order by FI.ModifiedDate DESC) as FlowStatus ");
            strSql.Append(" from officedba.StorageCheck as sc  WHERE 1=1 AND sc.CompanyCD=@CompanyCD ");

            

            #region 构造查询
            int length = htPara.Count;
            string FlowString = string.Empty;
            if (htPara.ContainsKey("FlowStatus"))
            {
                if (Convert.ToInt32(htPara["FlowStatus"].ToString()) > 0)
                    FlowString = "  AND  FlowStatus=" + htPara["FlowStatus"].ToString();
                else if (Convert.ToInt32(htPara["FlowStatus"].ToString()) == 0)
                    FlowString = "  AND FlowStatus is null ";
                length--;
            }

            if (EFIndex != "" && EFDesc != "")
            {
                strSql.Append(" AND sc.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' ");
            }
            SqlParameter[] Paras = new SqlParameter[length];
            int index = 0;

            foreach (string key in htPara.Keys)
            {
                switch (key)
                {
                    case "CheckNo":
                        strSql.Append(" AND sc.CheckNo LIKE @CheckNo ");
                        Paras[index] = new SqlParameter("@CheckNo", SqlDbType.VarChar);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "Title":
                        strSql.Append("AND sc.Title LIKE @Title ");
                        Paras[index] = new SqlParameter("@Title", SqlDbType.VarChar);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "Transactor":
                        strSql.Append(" AND sc.Transactor=@Transactor ");
                        Paras[index] = new SqlParameter("@Transactor", SqlDbType.Int);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "DeptID":
                        strSql.Append(" AND sc.DeptID=@DeptID ");
                        Paras[index] = new SqlParameter("@DeptID", SqlDbType.Int);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "StorageID":
                        strSql.Append(" AND sc.StorageID=@StorageID ");
                        Paras[index] = new SqlParameter("@StorageID", SqlDbType.Int);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "StorageIDS":
                        strSql.Append(" AND sc.StorageID in(" + htPara[key] + ") ");
                        Paras[index] = new SqlParameter("@StorageIDS", SqlDbType.VarChar);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "CheckType":
                        strSql.Append(" AND sc.CheckType=@CheckType ");
                        Paras[index] = new SqlParameter("@CheckType", SqlDbType.Int);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "CheckStartDate":
                        strSql.Append(" AND sc.CheckStartDate<=Convert(datetime,@CheckStartDate) AND sc.CheckEndDate>= Convert(datetime,@CheckStartDate) ");
                        Paras[index] = new SqlParameter("@CheckStartDate", SqlDbType.DateTime);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "DiffCountStart":
                        strSql.Append(" AND sc.DiffCount>=@DiffCountStart ");
                        Paras[index] = new SqlParameter("@DiffCountStart", SqlDbType.Decimal);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "DiffCountEnd":
                        strSql.Append(" AND sc.DiffCount <=@DiffCountEnd");
                        Paras[index] = new SqlParameter("@DiffCountEnd", SqlDbType.Decimal);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "BillStatus":
                        strSql.Append(" AND BillStatus=@BillStatus ");
                        Paras[index] = new SqlParameter("@BillStatus", SqlDbType.VarChar);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "CompanyCD":
                        Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                }

            }

            #endregion
            
            strSql.Append("  ) as tmpt  where 1=1 ");
            if (!string.IsNullOrEmpty(BatchNo))
            {
                strSql.Append(" and BatchNo = '" + BatchNo + "' ");
            }
            strSql.Append("  ) as TMP  where 1=1 ");
            strSql.Append( FlowString);
            return SqlHelper.CreateSqlByPageExcuteSql(strSql.ToString(), PageIndex, PageSize, OrderBy, Paras, ref TotalCount);
        }

        /*不分页*/
        public static DataTable GetStorageCheckList(Hashtable htPara, string IndexValue, string TxtValue,string BatchNo, string OrderBy)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct TMP.ID,TMP.CheckNo,TMP.Title,TMP.DeptID,TMP.DeptName,TMP.Transactor,");
            strSql.Append(" TMP.TransactorName,TMP.StorageID,TMP.StorageName,TMP.CheckStartDate,TMP.CheckType,TMP.DiffCount,TMP.BillStatus,TMP.FlowStatus from ( ");
            strSql.Append("select  * from (");
            strSql.Append(" select sc.ID,sc.CheckNo,sc.Title,sc.DeptID,sc.Transactor,");
            strSql.Append("(select ei.EmployeeName FROM officedba.EmployeeInfo as ei WHERE ei.ID=sc.Transactor ) as TransactorName,");
            strSql.Append("(select di.DeptName from officedba.DeptInfo as di where di.ID=sc.DeptID) AS DeptName,");
            strSql.Append("StorageID,");
            strSql.Append("(SELECT si.StorageName from officedba.StorageInfo as si where si.ID=sc.StorageID) AS StorageName,");
            strSql.Append("(SELECT x.BatchNo from officedba.StorageCheckDetail as x where x.CheckNo=sc.CheckNo and x.CompanyCD=sc.CompanyCD ");
            strSql.Append(" ) AS BatchNo,");
            strSql.Append("CheckStartDate,(case sc.CheckType when 0 then '' else (select ISNULL(ct.TypeName,'') from officedba.CodePublicType ct where sc.CheckType=ct.ID) end) as CheckType,");
            strSql.Append("DiffCount, (CASE sC.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,");
            strSql.Append(" ( select top 1 fi.FlowStatus   from  officedba.FlowInstance AS FI  where fi.BillID=sc.ID  and fi.CompanyCD=sc.CompanyCD  AND fi.BillTypeCode=" + ConstUtil.BILL_TYPECODE_STORAGE_CHECK + "  AND fi.BillTypeFlag=" + ConstUtil.BILL_TYPEFLAG_STORAGE + " order by FI.ModifiedDate DESC) as FlowStatus ");
            strSql.Append(" from officedba.StorageCheck as sc  WHERE 1=1 AND sc.CompanyCD=@CompanyCD ");



            #region 构造查询
            int length = htPara.Count;
            string FlowString = string.Empty;
            if (htPara.ContainsKey("FlowStatus"))
            {
                if (Convert.ToInt32(htPara["FlowStatus"].ToString()) > 0)
                    FlowString = "  AND  FlowStatus=" + htPara["FlowStatus"].ToString();
                else if (Convert.ToInt32(htPara["FlowStatus"].ToString()) == 0)
                    FlowString = "  AND FlowStatus is null ";
                length--;
            }

            if (IndexValue != "" && TxtValue != "")
            {
                strSql.Append(" AND sc.ExtField" + IndexValue + " LIKE '%" + TxtValue + "%' ");
            }
            SqlParameter[] Paras = new SqlParameter[length];
            int index = 0;

            foreach (string key in htPara.Keys)
            {
                switch (key)
                {
                    case "CheckNo":
                        strSql.Append(" AND sc.CheckNo LIKE @CheckNo ");
                        Paras[index] = new SqlParameter("@CheckNo", SqlDbType.VarChar);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "Title":
                        strSql.Append("AND sc.Title LIKE @Title ");
                        Paras[index] = new SqlParameter("@Title", SqlDbType.VarChar);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "Transactor":
                        strSql.Append(" AND sc.Transactor=@Transactor ");
                        Paras[index] = new SqlParameter("@Transactor", SqlDbType.Int);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "DeptID":
                        strSql.Append(" AND sc.DeptID=@DeptID ");
                        Paras[index] = new SqlParameter("@DeptID", SqlDbType.Int);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "StorageID":
                        strSql.Append(" AND sc.StorageID=@StorageID ");
                        Paras[index] = new SqlParameter("@StorageID", SqlDbType.Int);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "StorageIDS":
                        strSql.Append(" AND sc.StorageID in(" + htPara[key] + ") ");
                        Paras[index] = new SqlParameter("@StorageIDS", SqlDbType.VarChar);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "CheckType":
                        strSql.Append(" AND sc.CheckType=@CheckType ");
                        Paras[index] = new SqlParameter("@CheckType", SqlDbType.Int);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "CheckStartDate":
                        strSql.Append(" AND sc.CheckStartDate<=Convert(datetime,@CheckStartDate) AND sc.CheckEndDate>= Convert(datetime,@CheckStartDate) ");
                        Paras[index] = new SqlParameter("@CheckStartDate", SqlDbType.DateTime);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "DiffCountStart":
                        strSql.Append(" AND sc.DiffCount>=@DiffCountStart ");
                        Paras[index] = new SqlParameter("@DiffCountStart", SqlDbType.Decimal);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "DiffCountEnd":
                        strSql.Append(" AND sc.DiffCount <=@DiffCountEnd");
                        Paras[index] = new SqlParameter("@DiffCountEnd", SqlDbType.Decimal);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "BillStatus":
                        strSql.Append(" AND BillStatus=@BillStatus ");
                        Paras[index] = new SqlParameter("@BillStatus", SqlDbType.VarChar);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "CompanyCD":
                        Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                }

            }

            #endregion

            strSql.Append("  ) as tmpt  where 1=1 ");
            if (!string.IsNullOrEmpty(BatchNo))
            {
                strSql.Append(" and BatchNo = '" + BatchNo + "' ");
            }
            strSql.Append("  ) as TMP  where 1=1 ");
            strSql.Append(FlowString);
            return SqlHelper.ExecuteSql(strSql.ToString(), Paras);
            // return SqlHelper.CreateSqlByPageExcuteSql(strSql.ToString(), PageIndex, PageSize, OrderBy, Paras, ref TotalCount);
        }
        #endregion

        #region 删除盘点单 及其明细
        public static bool DelStorageCheck(string[] ID)
        {
            bool Flag = true;

            foreach (string tempID in ID)
            {
                StringBuilder strMainSql = new StringBuilder();
                strMainSql.Append("DELETE officedba.StorageCheckDetail WHERE CompanyCD=(SELECT st1.CompanyCD FROM  officedba.StorageCheck as st1  where st1.ID=@ID) AND CheckNo=(SELECT st2.CheckNo FROM officedba.StorageCheck as st2 WHERE st2.ID=@ID)");
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
                strSubSql.Append("DELETE officedba.StorageCheck WHERE ID=@ID");
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


        #region 盘点类型（add by hm）
        public static DataTable GetCheckType(string CompanyCD)
        {
            string SqlStr = "SELECT ID,TypeName FROM officedba.CodePublicType WHERE TypeFlag = 9 and UsedStatus='1' and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(SqlStr);
        }

        #endregion

    }
}

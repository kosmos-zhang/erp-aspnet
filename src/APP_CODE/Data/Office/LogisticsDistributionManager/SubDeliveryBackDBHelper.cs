using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using XBase.Data.DBHelper;
using XBase.Common;
using XBase.Model.Office.LogisticsDistributionManager;
using XBase.Model.Office.SubStoreManager;
namespace XBase.Data.Office.LogisticsDistributionManager
{
    public class SubDeliveryBackDBHelper
    {
        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(SubDeliveryBack model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.SubDeliveryBack set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND BackNo = @BackNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@BackNo", model.BackNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion

        #region 读取分店存量表
        public static DataTable GetSubStorageProduct(SubStorageProductModel model, int PageIndex, int PageSize, string OrderBy, ref int PageCount, Hashtable htPara)
        {

            int length = htPara.Count;
            string EFSql = string.Empty;
            if (htPara.ContainsKey("EFIndex") && htPara.ContainsKey("EFDesc"))
            {
                EFSql = " AND pi." + htPara["EFIndex"] + " LIKE '" + htPara["EFDesc"] + "' ";
                length = length - 2;
            }


            SqlParameter[] paras = new SqlParameter[length + 2];
            int index = 0;
            paras[index] = new SqlParameter("@DeptID", SqlDbType.Int);
            paras[index++].Value = model.DeptID;
            paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            paras[index++].Value = model.CompanyCD;
            string tmpSql = string.Empty;
            if (htPara.ContainsKey("SubStoreName"))
            {
                tmpSql += " AND di.DeptName LIKE @SubStoreName ";
                paras[index] = new SqlParameter("@SubStoreName", SqlDbType.VarChar);
                paras[index++].Value = htPara["SubStoreName"];
            }
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT ssp.*,( SELECT di.DeptName FROM officedba.DeptInfo as di  WHERE di.ID=ssp.DeptID  " + tmpSql + " )  as DetpName,  pi.SellPrice");
            sbSql.Append(" ,pi.ProductName ,pi.ProdNo,pi.Specification,pi.IsBatchNo,(select ct.CodeName from officedba.CodeUnitType as ct where ct.ID=pi.UnitID  ) as UnitName,pi.UnitID ");
            sbSql.Append(" ,(SELECT sps.SendPriceTax FROM officedba.SubProductSendPrice AS sps where sps.DeptID=ssp.DeptID AND sps.CompanyCD=ssp.CompanyCD AND sps.ProductID=ssp.ProductID) AS BackPrice ");
            sbSql.Append(@" FROM officedba.SubStorageProduct as ssp  
                            inner join officedba.ProductInfo as pi on pi.ID=ssp.ProductID AND pi.CheckStatus=1 ");
            sbSql.Append(" where ssp.DeptID=@DeptID AND ssp.CompanyCD=@CompanyCD ");
            if (htPara.ContainsKey("ProductName"))
            {
                sbSql.Append(" AND pi.ProductName LIKE @ProductName ");
                paras[index] = new SqlParameter("@ProductName", SqlDbType.VarChar);
                paras[index++].Value = htPara["ProductName"];
            }
            if (htPara.ContainsKey("ProdNo"))
            {
                sbSql.Append(" AND pi.ProdNo LIKE @ProdNo");
                paras[index] = new SqlParameter("@ProdNo", SqlDbType.VarChar);
                paras[index++].Value = htPara["ProdNo"];
            }

            if (htPara.ContainsKey("Specification"))
            {
                sbSql.Append(" AND pi.Specification LIKE @Specification");
                paras[index] = new SqlParameter("@Specification", SqlDbType.VarChar);
                paras[index++].Value = htPara["Specification"];
            }

            /*追加扩展属性查询*/
            sbSql.Append(EFSql);

            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, paras, ref PageCount);

        }

        /*条码扫描使用*/
        public static DataTable GetSubStorageProduct(SubStorageProductModel model)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT ssp.*,( SELECT di.DeptName FROM officedba.DeptInfo as di  WHERE di.ID=ssp.DeptID  )  as DetpName,  pi.SellPrice,");
            sbSql.Append(" pi.ProductName ,pi.ProdNo,pi.Specification,pi.IsBatchNo,(select ct.CodeName from officedba.CodeUnitType as ct where ct.ID=pi.UnitID  ) as UnitName, ");
            sbSql.Append(" (SELECT sps.SendPriceTax FROM officedba.SubProductSendPrice AS sps where sps.DeptID=ssp.DeptID AND sps.CompanyCD=ssp.CompanyCD AND sps.ProductID=ssp.ProductID) AS BackPrice ,(SELECT sps.SendPriceTax FROM officedba.SubProductSendPrice AS sps where sps.DeptID=0 AND sps.CompanyCD=ssp.CompanyCD AND sps.ProductID=ssp.ProductID) AS DefaultBackPrice  ");
            sbSql.Append(" FROM officedba.SubStorageProduct as ssp  inner join officedba.ProductInfo as pi on pi.ID=ssp.ProductID AND pi.CheckStatus=1 ");
            sbSql.Append(" where ssp.DeptID=@DeptID AND ssp.CompanyCD=@CompanyCD  AND pi.ID=@ProductID ");
            SqlParameter[] Paras ={
                                    SqlHelper.GetParameter("@CompanyCD",model.CompanyCD),
                                    SqlHelper.GetParameter("@DeptID",model.DeptID),
                                    SqlHelper.GetParameter("@ProductID",model.ProductID)
                                 };

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
        }



        #endregion

        #region 添加配送退货单及其明细
        public static string AddSubDeliveryBack(SubDeliveryBack model, List<SubDeliveryBackDetail> modelList, Hashtable htExtAttr)
        {
            ArrayList SqlList = new ArrayList();

            #region 构造退货单主表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SubDeliveryBack(");
            strSql.Append("CompanyCD,BackNo,SendID,Title,ApplyUserID,ApplyDeptID,OutDeptID,BackPrice,BackFeeSum,BackCount,BackReason,BusiStatus,Remark,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@BackNo,@SendID,@Title,@ApplyUserID,@ApplyDeptID,@OutDeptID,@BackPrice,@BackFeeSum,@BackCount,@BackReason,@BusiStatus,@Remark,@Creator,@CreateDate,@BillStatus,@ModifiedDate,@ModifiedUserID)");
            strSql.Append(";select @ID=@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@BackNo", SqlDbType.VarChar,50), 
					new SqlParameter("@SendID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@ApplyUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplyDeptID", SqlDbType.Int,4),
					new SqlParameter("@OutDeptID", SqlDbType.Int,4),
					new SqlParameter("@BackPrice", SqlDbType.Decimal,9),
					new SqlParameter("@BackFeeSum", SqlDbType.Decimal,9),
					new SqlParameter("@BackCount", SqlDbType.Decimal,9),
					new SqlParameter("@BackReason", SqlDbType.VarChar,1024),
					new SqlParameter("@BusiStatus", SqlDbType.Char,1),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@ID",SqlDbType.Int)};
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.BackNo;
            parameters[2].Value = model.SendID;
            parameters[3].Value = model.Title;
            parameters[4].Value = model.ApplyUserID;
            parameters[5].Value = model.ApplyDeptID;
            if (model.OutDeptID.HasValue)
            {
                parameters[6].Value = model.OutDeptID;
            }
            else
            {
                parameters[6].Value = DBNull.Value;
            }
            parameters[7].Value = model.BackPrice;
            parameters[8].Value = model.BackFeeSum;
            parameters[9].Value = model.BackCount;
            parameters[10].Value = model.BackReason;
            parameters[11].Value = model.BusiStatus;
            parameters[12].Value = model.Remark;
            parameters[13].Value = model.Creator;
            parameters[14].Value = model.CreateDate;
            parameters[15].Value = model.BillStatus;
            parameters[16].Value = model.ModifiedDate;
            parameters[17].Value = model.ModifiedUserID;
            parameters[18].Direction = ParameterDirection.Output;

            SqlCommand sqlMainCmd = new SqlCommand() { CommandText = strSql.ToString() };
            sqlMainCmd.Parameters.AddRange(parameters);
            SqlList.Add(sqlMainCmd);
            #endregion


            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                SqlList.Add(cmd);
            #endregion

            #region 构造明细表
            foreach (SubDeliveryBackDetail m in modelList)
            {
                SqlList.Add(GetSaveDetail(m));
            }
            #endregion

            bool result = SqlHelper.ExecuteTransWithArrayList(SqlList);
            if (result)
            {
                return ((SqlCommand)SqlList[0]).Parameters["@ID"].Value.ToString();
            }
            else
                return string.Empty;
        }
        #endregion

        /// <summary>
        /// 保存明细
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private static SqlCommand GetSaveDetail(SubDeliveryBackDetail m)
        {
            StringBuilder strSubSql = new StringBuilder();
            strSubSql.Append("insert into officedba.SubDeliveryBackDetail(");
            strSubSql.Append("CompanyCD,BackNo,SortNo,ProductID,BackCount,BackPrice,BackPriceTotal,UsedUnitCount,UnitID,ExRate,StorageID,SendBatchNo,BackBatchNo)");
            strSubSql.Append(" values (");
            strSubSql.Append("@CompanyCD,@BackNo,@SortNo,@ProductID,@BackCount,@BackPrice,@BackPriceTotal,@UsedUnitCount,@UnitID,@ExRate,@StorageID,@SendBatchNo,@BackBatchNo)");
            strSubSql.Append(";select @@IDENTITY");
            SqlParameter[] subParameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@BackNo", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@BackCount", SqlDbType.Decimal,9),
					new SqlParameter("@BackPrice", SqlDbType.Decimal,9),
					new SqlParameter("@BackPriceTotal", SqlDbType.Decimal,9),
					new SqlParameter("@StorageID", SqlDbType.Int,4),
					new SqlParameter("@SendBatchNo", SqlDbType.VarChar,50),
					new SqlParameter("@BackBatchNo", SqlDbType.VarChar,50)};
            subParameters[0].Value = m.CompanyCD;
            subParameters[1].Value = m.BackNo;
            subParameters[2].Value = m.SortNo;
            subParameters[3].Value = m.ProductID;
            subParameters[4].Value = m.BackCount;
            subParameters[5].Value = m.BackPrice;
            subParameters[6].Value = m.BackPriceTotal;
            if (m.StorageID.HasValue)
            {
                subParameters[7].Value = m.StorageID.Value;
            }
            else
            {
                subParameters[7].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(m.SendBatchNo))
            {
                subParameters[8].Value = m.SendBatchNo;
            }
            else
            {
                subParameters[8].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(m.BackBatchNo))
            {
                subParameters[9].Value = m.BackBatchNo;
            }
            else
            {
                subParameters[9].Value = DBNull.Value;
            }

            SqlCommand sqlSubCmd = new SqlCommand() { CommandText = strSubSql.ToString() };
            sqlSubCmd.Parameters.AddRange(subParameters);
            if (m.UnitID.HasValue)
            {
                sqlSubCmd.Parameters.Add(SqlHelper.GetParameter("@UnitID", m.UnitID.Value));
            }
            else
            {
                sqlSubCmd.Parameters.Add(SqlHelper.GetParameter("@UnitID", DBNull.Value));
            }
            if (m.UsedUnitCount.HasValue)
            {
                sqlSubCmd.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", m.UsedUnitCount.Value));
            }
            else
            {
                sqlSubCmd.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", DBNull.Value));
            }
            if (m.ExRate.HasValue)
            {
                sqlSubCmd.Parameters.Add(SqlHelper.GetParameter("@ExRate", m.ExRate.Value));
            }
            else
            {
                sqlSubCmd.Parameters.Add(SqlHelper.GetParameter("@ExRate", DBNull.Value));
            }
            return sqlSubCmd;
        }

        #region 更新状态
        public static bool UpdateStatus(SubDeliveryBack model, int stype)
        {
            if (stype == 1)
            {
                #region 确认单据
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update officedba.SubDeliveryBack set ");
                strSql.Append("BusiStatus=@BusiStatus,");
                strSql.Append("BillStatus=@BillStatus,");
                strSql.Append("Confirmor=@Confirmor,");
                strSql.Append("ConfirmDate=@ConfirmDate,");
                strSql.Append("ModifiedDate=@ModifiedDate,");
                strSql.Append("ModifiedUserID=@ModifiedUserID");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@BusiStatus", SqlDbType.Char,1),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@Confirmor", SqlDbType.Int,4),
					new SqlParameter("@ConfirmDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50)};
                parameters[0].Value = model.ID;
                parameters[1].Value = model.BusiStatus;
                parameters[2].Value = model.BillStatus;
                parameters[3].Value = model.Confirmor;
                parameters[4].Value = model.ConfirmDate;
                parameters[5].Value = model.ModifiedDate;
                parameters[6].Value = model.ModifiedUserID;

                if (SqlHelper.ExecuteTransSql(strSql.ToString(), parameters) > 0)
                    return true;
                else
                    return false;
                #endregion
            }
            else if (stype == 3 || stype == 4)
            {
                #region 取消结单
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update officedba.SubDeliveryBack set ");
                strSql.Append("BillStatus=@BillStatus,");
                strSql.Append("ModifiedDate=@ModifiedDate,");
                strSql.Append("ModifiedUserID=@ModifiedUserID,");
                strSql.Append("BusiStatus=@BusiStatus ");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("BusiStatus",SqlDbType.VarChar)};
                parameters[0].Value = model.ID;
                parameters[1].Value = model.BillStatus;
                parameters[2].Value = model.ModifiedDate;
                parameters[3].Value = model.ModifiedUserID;
                parameters[4].Value = model.BusiStatus;
                SqlCommand SqlMainCmd = new SqlCommand() { CommandText = strSql.ToString() };
                SqlMainCmd.Parameters.AddRange(parameters);

                List<SqlCommand> SqlCmdList = new List<SqlCommand>();
                SqlCmdList.Add(SqlMainCmd);


                /*追加取消确认的SqlCommond*/
                if (stype == 4)
                {
                    IList<SqlCommand> tempList = Data.Common.FlowDBHelper.GetCancelConfirmSqlCommond(model.CompanyCD, Convert.ToInt32(ConstUtil.TYPEFLAG_LogisticsDistribution_NO), Convert.ToInt32(ConstUtil.TYPECODE_SubDeliveryBack_NO), model.ID, model.ModifiedUserID);
                    foreach (SqlCommand scmd in tempList)
                    {
                        SqlCmdList.Add(scmd);
                    }
                }



                if (SqlHelper.ExecuteTransWithCollections(SqlCmdList))
                    return true;
                else
                    return false;
                #endregion
            }
            else if (stype == 2)
            {
                #region 结单
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update officedba.SubDeliveryBack set ");
                strSql.Append("BillStatus=@BillStatus,");
                strSql.Append("Closer=@Closer,");
                strSql.Append("CloseDate=@CloseDate,");
                strSql.Append("ModifiedDate=@ModifiedDate,");
                strSql.Append("ModifiedUserID=@ModifiedUserID");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@Closer", SqlDbType.Int,4),
					new SqlParameter("@CloseDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50)};
                parameters[0].Value = model.ID;
                parameters[1].Value = model.BillStatus;
                parameters[2].Value = model.Closer;
                parameters[3].Value = model.CloseDate;
                parameters[4].Value = model.ModifiedDate;
                parameters[5].Value = model.ModifiedUserID;
                if (SqlHelper.ExecuteTransSql(strSql.ToString(), parameters) > 0)
                    return true;
                else
                    return false;
                #endregion

            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 更新退货单及其明细
        public static bool UpdateSubDeliverySend(SubDeliveryBack model, List<SubDeliveryBackDetail> modelList, Hashtable htExtAttr)
        {

            ArrayList SqlList = new ArrayList();
            #region 配送单
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SubDeliveryBack set ");
            strSql.Append("SendID=@SendID,");
            strSql.Append("Title=@Title,");
            strSql.Append("ApplyUserID=@ApplyUserID,");
            strSql.Append("ApplyDeptID=@ApplyDeptID,");
            strSql.Append("BackPrice=@BackPrice,");
            strSql.Append("BackFeeSum=@BackFeeSum,");
            strSql.Append("BackCount=@BackCount,");
            strSql.Append("BackReason=@BackReason,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@SendID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@ApplyUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplyDeptID", SqlDbType.Int,4),
					new SqlParameter("@BackPrice", SqlDbType.Decimal,9),
					new SqlParameter("@BackFeeSum", SqlDbType.Decimal,9),
					new SqlParameter("@BackCount", SqlDbType.Decimal,9),
					new SqlParameter("@BackReason", SqlDbType.VarChar,1024),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.SendID;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.ApplyUserID;
            parameters[4].Value = model.ApplyDeptID;
            parameters[5].Value = model.BackPrice;
            parameters[6].Value = model.BackFeeSum;
            parameters[7].Value = model.BackCount;
            parameters[8].Value = model.BackReason;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.ModifiedDate;
            parameters[11].Value = model.ModifiedUserID;

            SqlCommand SqlCmd = new SqlCommand { CommandText = strSql.ToString() };
            SqlCmd.Parameters.AddRange(parameters);
            SqlList.Add(SqlCmd);

            #endregion

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                SqlList.Add(cmd);
            #endregion


            #region 配送明细单
            StringBuilder sbDel = new StringBuilder();
            sbDel.Append("DELETE officedba.SubDeliveryBackDetail WHERE CompanyCD=@CompanyCD AND BackNo=@BackNo");
            SqlParameter[] delParas = { 
                                          new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                          new SqlParameter("@BackNo",SqlDbType.VarChar)
                                      };
            delParas[0].Value = model.CompanyCD;
            delParas[1].Value = model.BackNo;
            SqlCommand sqlDelCmd = new SqlCommand { CommandText = sbDel.ToString() };
            sqlDelCmd.Parameters.AddRange(delParas);
            SqlList.Add(sqlDelCmd);

            foreach (SubDeliveryBackDetail m in modelList)
            {
                SqlList.Add(GetSaveDetail(m));
            }
            #endregion

            bool result = SqlHelper.ExecuteTransWithArrayList(SqlList);
            return result;

        }
        #endregion

        #region 读取退货单列表
        public static DataTable GetSubDeliveryBackList(Hashtable htPara, int PageIndex, int PageSize, string OrderBy, string EFIndex, string EFDesc, ref int TotalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(" SELECT * FROM (");
            sbSql.Append("SELECT  sds.ID,sds.BackNo,sds.Title,sds.ApplyUserID,sds.ApplyDeptID,sds.BackPrice,sds.BackCount,sds.BackFeeSum,sds.busiStatus, ");
            sbSql.Append(" (select ei1.EmployeeName from officedba.EmployeeInfo  as ei1  where ei1.ID=sds.ApplyUserID) as ApplyUserIDName,");
            sbSql.Append(" (select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=sds.ApplyDeptID) as ApplyDeptIDName,");
            sbSql.Append("(CASE sds.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,( select top 1  fi.FlowStatus   from  officedba.FlowInstance AS FI  where fi.BillID=sds.ID  and fi.CompanyCD=sds.CompanyCD  AND fi.BillTypeCode=" + XBase.Common.ConstUtil.TYPECODE_SubDeliveryBack_NO + "  AND fi.BillTypeFlag=" + XBase.Common.ConstUtil.TYPEFLAG_LogisticsDistribution_NO + "  order by FI.ModifiedDate DESC) as FlowStatus ");
            sbSql.Append("  FROM officedba.SubDeliveryBack as sds where sds.CompanyCD=@CompanyCD  ");



            string FlowStatusSql = string.Empty;
            if (htPara.ContainsKey("@FlowStatus"))
            {
                int SubmitStatus = Convert.ToInt32(htPara["@FlowStatus"].ToString());
                if (SubmitStatus > 0)
                    FlowStatusSql = "  AND  FlowStatus=" + htPara["@FlowStatus"].ToString();
                else if (SubmitStatus == 0)
                    FlowStatusSql = "  AND FlowStatus is null ";
            }

            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter Paras = null;

            #region 构造查询条件
            if (htPara.ContainsKey("@BackNo"))
            {
                sbSql.Append(" AND sds.BackNo LIKE @BackNo");
                Paras = new SqlParameter("@BackNo", SqlDbType.VarChar);
                Paras.Value = htPara["@BackNo"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@Title"))
            {
                sbSql.Append(" AND sds.Title LIKE @Title");
                Paras = new SqlParameter("@Title", SqlDbType.VarChar);
                Paras.Value = htPara["@Title"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@ApplyUserID"))
            {
                sbSql.Append(" AND sds.ApplyUserID=@ApplyUserID");
                Paras = new SqlParameter("@ApplyUserID", SqlDbType.Int);
                Paras.Value = htPara["@ApplyUserID"];
                list.Add(Paras);
            }

            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sbSql.Append(" and sds.ExtField" + EFIndex + " LIKE @EFDesc ");
                Paras = new SqlParameter("@EFDesc", SqlDbType.VarChar);
                Paras.Value = "%" + EFDesc + "%";
                list.Add(Paras);
            }

            if (htPara.ContainsKey("@ApplyDeptID"))
            {
                sbSql.Append(" AND sds.ApplyDeptID=@ApplyDeptID");
                Paras = new SqlParameter("@ApplyDeptID", SqlDbType.Int);
                Paras.Value = htPara["@ApplyDeptID"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@BusiStatus"))
            {
                sbSql.Append(" AND sds.BusiStatus=@BusiStatus");
                Paras = new SqlParameter("@BusiStatus", SqlDbType.VarChar);
                Paras.Value = htPara["@BusiStatus"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@BillStatus"))
            {
                sbSql.Append(" AND sds.BillStatus=@BillStatus");
                Paras = new SqlParameter("@BillStatus", SqlDbType.VarChar);
                Paras.Value = htPara["@BillStatus"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@CompanyCD"))
            {
                Paras = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
                Paras.Value = htPara["@CompanyCD"];
                list.Add(Paras);
            }
            #endregion

            sbSql.Append(" ) as tmpt where 1=1 " + FlowStatusSql);
            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, list.ToArray(), ref TotalCount);

        }

        /*不分页*/
        public static DataTable GetSubDeliveryBackList(Hashtable htPara, string OrderBy, string EFIndex, string EFDesc)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(" SELECT * FROM (");
            sbSql.Append("SELECT  sds.ID,sds.BackNo,sds.Title,sds.ApplyUserID,sds.ApplyDeptID,sds.BackPrice,sds.BackCount,sds.BackFeeSum,sds.busiStatus, ");
            sbSql.Append(" (select ei1.EmployeeName from officedba.EmployeeInfo  as ei1  where ei1.ID=sds.ApplyUserID) as ApplyUserIDName,");
            sbSql.Append(" (select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=sds.ApplyDeptID) as ApplyDeptIDName,");
            sbSql.Append("(CASE sds.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,( select top 1  fi.FlowStatus   from  officedba.FlowInstance AS FI  where fi.BillID=sds.ID  and fi.CompanyCD=sds.CompanyCD  AND fi.BillTypeCode=" + XBase.Common.ConstUtil.TYPECODE_SubDeliveryBack_NO + "  AND fi.BillTypeFlag=" + XBase.Common.ConstUtil.TYPEFLAG_LogisticsDistribution_NO + "  order by FI.ModifiedDate DESC) as FlowStatus ");
            sbSql.Append("  FROM officedba.SubDeliveryBack as sds where sds.CompanyCD=@CompanyCD  ");



            string FlowStatusSql = string.Empty;
            if (htPara.ContainsKey("@FlowStatus"))
            {
                int SubmitStatus = Convert.ToInt32(htPara["@FlowStatus"].ToString());
                if (SubmitStatus > 0)
                    FlowStatusSql = "  AND  FlowStatus=" + htPara["@FlowStatus"].ToString();
                else if (SubmitStatus == 0)
                    FlowStatusSql = "  AND FlowStatus is null ";
            }
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter Paras = null;
            #region 构造查询条件
            if (htPara.ContainsKey("@BackNo"))
            {
                sbSql.Append(" AND sds.BackNo LIKE @BackNo");
                Paras = new SqlParameter("@BackNo", SqlDbType.VarChar);
                Paras.Value = htPara["@BackNo"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@Title"))
            {
                sbSql.Append(" AND sds.Title LIKE @Title");
                Paras = new SqlParameter("@Title", SqlDbType.VarChar);
                Paras.Value = htPara["@Title"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@ApplyUserID"))
            {
                sbSql.Append(" AND sds.ApplyUserID=@ApplyUserID");
                Paras = new SqlParameter("@ApplyUserID", SqlDbType.Int);
                Paras.Value = htPara["@ApplyUserID"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@ApplyDeptID"))
            {
                sbSql.Append(" AND sds.ApplyDeptID=@ApplyDeptID");
                Paras = new SqlParameter("@ApplyDeptID", SqlDbType.Int);
                Paras.Value = htPara["@ApplyDeptID"];
                list.Add(Paras);
            }

            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sbSql.Append(" and sds.ExtField" + EFIndex + " LIKE @EFDesc ");
                Paras = new SqlParameter("@EFDesc", SqlDbType.VarChar);
                Paras.Value = "%" + EFDesc + "%";
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@BusiStatus"))
            {
                sbSql.Append(" AND sds.BusiStatus=@BusiStatus");
                Paras = new SqlParameter("@BusiStatus", SqlDbType.VarChar);
                Paras.Value = htPara["@BusiStatus"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@BillStatus"))
            {
                sbSql.Append(" AND sds.BillStatus=@BillStatus");
                Paras = new SqlParameter("@BillStatus", SqlDbType.VarChar);
                Paras.Value = htPara["@BillStatus"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@CompanyCD"))
            {
                Paras = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
                Paras.Value = htPara["@CompanyCD"];
                list.Add(Paras);
            }
            #endregion

            sbSql.Append(" ) as tmpt where 1=1 " + FlowStatusSql);
            sbSql.Append(" ORDER BY " + OrderBy);
            return SqlHelper.ExecuteSql(sbSql.ToString(), list.ToArray());

        }

        #endregion

        #region 删除
        public static bool DelSubDeliveryBack(string[] IDList)
        {

            ArrayList SqlList = new ArrayList();
            for (int i = 0; i < IDList.Length; i++)
            {
                StringBuilder sbSubSql = new StringBuilder();
                sbSubSql.Append("DELETE officedba.SubDeliveryBackDetail  where CompanyCD=(Select sds1.CompanyCD FROM officedba.SubDeliveryBack as sds1 where sds1.ID=@ID) ");
                sbSubSql.Append(" AND BackNo=(SELECT sds2.BackNo from officedba.SubDeliveryBack as sds2 where sds2.ID=@ID)");
                SqlParameter[] SubParas = { new SqlParameter("@ID", SqlDbType.Int) };
                SubParas[0].Value = IDList[i];

                SqlCommand SqlSubCmd = new SqlCommand();
                SqlSubCmd.CommandText = sbSubSql.ToString();
                SqlSubCmd.Parameters.AddRange(SubParas);
                SqlList.Add(SqlSubCmd);

                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("DELETE officedba.SubDeliveryBack where ID=@ID");
                SqlParameter[] Paras = { new SqlParameter("@ID", SqlDbType.Int) };
                Paras[0].Value = IDList[i];
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.CommandText = sbSql.ToString();
                SqlCmd.Parameters.AddRange(Paras);
                SqlList.Add(SqlCmd);
            }

            bool res = SqlHelper.ExecuteTransWithArrayList(SqlList);
            return res;
        }
        #endregion

        #region 读取配送退还单信息
        public static DataTable GetSubDeliveryBackInfo(SubDeliveryBack model)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT sds.* ,");
            sbSql.Append("(SELECT s.SendNo from officedba.SubDeliverySend  as s where s.ID=sds.SendID) AS SendNo ,");
            sbSql.Append(" (select ei1.EmployeeName from officedba.EmployeeInfo  as ei1  where ei1.ID=sds.ApplyUserID) as ApplyUserIDName,");
            sbSql.Append("(select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=sds.ApplyDeptID) as ApplyDeptIDName,");
            sbSql.Append("(select ei2.EmployeeName from officedba.EmployeeInfo  as ei2 where ei2.ID=sds.OutUserID) as OutUserIDName,");
            sbSql.Append("(select ei3.EmployeeName from officedba.EmployeeInfo  as ei3 where ei3.ID=sds.InUserID) as InUserIDName,");
            sbSql.Append("(select ei4.EmployeeName from officedba.EmployeeInfo  as ei4 where ei4.ID=sds.Creator) as CreatorName,");
            sbSql.Append("(select ei5.EmployeeName from officedba.EmployeeInfo  as ei5 where ei5.ID=sds.Confirmor) as ConfirmorName,");
            sbSql.Append("(select ei6.EmployeeName from officedba.EmployeeInfo  as ei6 where ei6.ID=sds.Closer) as CloserName ");
            sbSql.Append(" from officedba.SubDeliveryBack as sds where sds.CompanyCD=@CompanyCD AND sds.ID=@ID");

            SqlParameter[] Paras = { 
                                   new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                   new SqlParameter("@ID",SqlDbType.Int)};
            Paras[0].Value = model.CompanyCD;
            Paras[1].Value = model.ID;

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
        }

        /*打印使用*/
        public static DataTable GetSubDeliveryBackInfoPrint(SubDeliveryBack model)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("  SELECT     ID, CompanyCD, BackNo, SendID, Title, ApplyUserID, ApplyDeptID, OutDeptID,BackReason, BusiStatus, ");
            sbSql.AppendLine("   OutUserID, convert(varchar(10),OutDate,120)OutDate, InUserID, convert(varchar(10),InDate,120)InDate, Remark, Creator, convert(varchar(10),CreateDate,120)CreateDate, BillStatus, Confirmor, convert(varchar(10),ConfirmDate,120)ConfirmDate, Closer, convert(varchar(10),CloseDate,120)CloseDate, convert(varchar(10),ModifiedDate,120)ModifiedDate, ExtField1,");
            sbSql.AppendLine("ExtField2,ExtField3,ExtField4,ExtField5,ExtField6,ExtField7,ExtField8,ExtField9,ExtField10");
            sbSql.AppendLine("  ,CONVERT(NUMERIC(12,2),ISNULL(BackPrice,0)) BackPrice");
            sbSql.AppendLine("  ,CONVERT(NUMERIC(12,2),ISNULL(BackFeeSum,0)) BackFeeSum");
            sbSql.AppendLine("  ,CONVERT(NUMERIC(12,2),ISNULL(BackCount,0)) BackCount");
            sbSql.AppendLine("  ,ModifiedUserID,  ");
            sbSql.AppendLine("  (CASE sds.BusiStatus WHEN '1' THEN '退货申请' WHEN '2' THEN '退货出库' WHEN '3' THEN '验收入库' WHEN '4' THEN '退货完成' END) ");
            sbSql.AppendLine("  AS BusiStatusText, (CASE sds.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '4' THEN '手工结单' WHEN '5' THEN '自动结单' END)   ");
            sbSql.AppendLine("   AS BillStatusText,  ");
            sbSql.AppendLine("   (SELECT     SendNo  ");
            sbSql.AppendLine("  FROM          officedba.SubDeliverySend AS s  ");
            sbSql.AppendLine("  WHERE      (ID = sds.SendID)) AS SendNo,  ");
            sbSql.AppendLine("   (SELECT     EmployeeName  ");
            sbSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei1  ");
            sbSql.AppendLine("   WHERE      (ID = sds.ApplyUserID)) AS ApplyUserIDName,  ");
            sbSql.AppendLine("  (SELECT     DeptName  ");
            sbSql.AppendLine("  FROM          officedba.DeptInfo AS di2  ");
            sbSql.AppendLine("  WHERE      (ID = sds.ApplyDeptID)) AS ApplyDeptIDName,  ");
            sbSql.AppendLine("  (SELECT     EmployeeName  ");
            sbSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei2  ");
            sbSql.AppendLine("  WHERE      (ID = sds.OutUserID)) AS OutUserIDName,  ");
            sbSql.AppendLine("  (SELECT     EmployeeName  ");
            sbSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei3  ");
            sbSql.AppendLine("   WHERE      (ID = sds.InUserID)) AS InUserIDName,  ");
            sbSql.AppendLine("  (SELECT     EmployeeName  ");
            sbSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei4  ");
            sbSql.AppendLine("  WHERE      (ID = sds.Creator)) AS CreatorName,  ");
            sbSql.AppendLine("  (SELECT     EmployeeName  ");
            sbSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei5  ");
            sbSql.AppendLine("  WHERE      (ID = sds.Confirmor)) AS ConfirmorName,  ");
            sbSql.AppendLine("  (SELECT     EmployeeName  ");
            sbSql.AppendLine("   FROM          officedba.EmployeeInfo AS ei6  ");
            sbSql.AppendLine("   WHERE      (ID = sds.Closer)) AS CloserName  ");
            sbSql.AppendLine("  FROM         officedba.SubDeliveryBack AS sds  ");
            sbSql.AppendLine("  where sds.CompanyCD=@CompanyCD AND sds.ID=@ID ");
            SqlParameter[] Paras = { 
                                   new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                   new SqlParameter("@ID",SqlDbType.Int)};
            Paras[0].Value = model.CompanyCD;
            Paras[1].Value = model.ID;

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
        }
        #endregion

        #region 读取配送退货单明细信息
        public static DataTable GetSubDeliveryBackDetail(SubDeliveryBackDetail model)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT stsd.ID,stsd.CompanyCD,stsd.BackNo,stsd.SortNo,stsd.ProductID,stsd.StorageID,stsd.UnitID as UsedUnitID,stsd.UsedUnitCount,stsd.SendBatchNo,stsd.BackBatchNo");
            sbSql.Append(",ISNULL(stsd.BackCount,0) BackCount");
            sbSql.Append(",ISNULL(stsd.BackPrice,0) BackPrice");
            sbSql.Append(",ISNULL(stsd.BackPriceTotal,0) BackPriceTotal");
            sbSql.Append(",pi.ProdNo,pi.ProductName,pi.Specification,pi.UnitID,pi.MinusIs,pi.IsBatchNo");
            sbSql.Append(", (select SUM(ISNULL(ProductCount,0))  from officedba.SubStorageProduct as sp where sp.DeptID=(select s.ApplyDeptID FROM officedba.SubDeliveryBack AS S WHERE S.CompanyCD=@CompanyCD AND S.BackNo=@BackNo)  AND sp.ProductID=stsd.ProductID AND sp.CompanyCD=stsd.CompanyCD AND isnull(sp.BatchNo,'')=isnull(stsd.SendBatchNo,'') ) AS UseCount");
            sbSql.Append(", (SELECT ui.CodeName from officedba.CodeUnitType as ui where ui.ID=pi.UnitID) AS UnitName ");
            sbSql.Append(", (SELECT ui1.CodeName from officedba.CodeUnitType as ui1 where ui1.ID=stsd.UnitID) AS UsedUnitName ");
            sbSql.Append(" from officedba.SubDeliveryBackDetail as stsd inner join officedba.ProductInfo as pi on stsd.ProductID=pi.ID where stsd.CompanyCD=@CompanyCD AND stsd.BackNo=@BackNo");

            SqlParameter[] Paras = { 
                                   new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                   new SqlParameter("@BackNo",SqlDbType.VarChar)};

            Paras[0].Value = model.CompanyCD;
            Paras[1].Value = model.BackNo;

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
        }
        #endregion

        #region 分店出库
        public static bool SubDeliveryBackOut(SubDeliveryBack model)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT * FROM officedba.SubDeliveryBackDetail  ");
            sbSql.Append(" WHERE CompanyCD=@CompanyCD and BackNo=@BackNo");

            SqlParameter[] paras = { 
                                   new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                   new SqlParameter("@BackNo",SqlDbType.VarChar)};
            paras[0].Value = model.CompanyCD;
            paras[1].Value = model.BackNo;

            DataTable dtBack = SqlHelper.ExecuteSql(sbSql.ToString(), paras);
            ArrayList SqlList = new ArrayList();

            if (dtBack != null && dtBack.Rows.Count > 0)
            {

                #region 更新单据
                StringBuilder sbMainSql = new StringBuilder();
                sbMainSql.Append("UPDATE  officedba.SubDeliveryBack SET OutUserID=@OutUserID,OutDate=@OutDate,BusiStatus=@BusiStatus ");
                sbMainSql.Append(" WHERE ID=@ID ");
                SqlParameter[] MainParas = { 
                                           new SqlParameter("@OutUserID",SqlDbType.Int),
                                           new SqlParameter("@OutDate",SqlDbType.DateTime),
                                           new SqlParameter("@BusiStatus",SqlDbType.VarChar),
                                           new SqlParameter("@ID",SqlDbType.Int)
                                       };
                MainParas[0].Value = model.OutUserID;
                MainParas[1].Value = model.OutDate;
                MainParas[2].Value = model.BusiStatus;
                MainParas[3].Value = model.ID;

                SqlCommand sqlMainCmd = new SqlCommand() { CommandText = sbMainSql.ToString() };
                sqlMainCmd.Parameters.AddRange(MainParas);
                SqlList.Add(sqlMainCmd);
                #endregion

                int id = 0;
                decimal count = 0m;
                foreach (DataRow row in dtBack.Rows)
                {
                    #region 添加门店库存流水帐
                    SubStorageAccountModel aModel = new SubStorageAccountModel();
                    aModel.BatchNo = row["SendBatchNo"].ToString();
                    aModel.BillNo = model.BackNo;
                    aModel.BillType = 7;
                    aModel.CompanyCD = model.CompanyCD;
                    aModel.Creator = model.Creator;
                    aModel.DeptID = model.ApplyDeptID;
                    aModel.HappenDate = DateTime.Now;
                    if (int.TryParse(row["ProductID"].ToString(), out id))
                    {
                        aModel.ProductID = id;
                    }
                    if (decimal.TryParse(row["BackPrice"].ToString(), out count))
                    {
                        aModel.Price = count;
                    }
                    if (decimal.TryParse(row["BackCount"].ToString(), out count))
                    {
                        aModel.HappenCount = -count;
                    }
                    aModel.PageUrl = model.Remark;
                    SqlList.Add(XBase.Data.Office.SubStoreManager.SubStorageAccountDBHelper.GetCountAndInsertCommand(aModel));
                    #endregion
                    // 更新库存
                    SqlList.Add(XBase.Data.Office.LogisticsDistributionManager.StorageProductQueryDBHelper.UpdateProductCount(model.CompanyCD
                                , row["ProductID"].ToString(), model.ApplyDeptID.ToString(), row["SendBatchNo"].ToString(), -count));

                }

                bool res = SqlHelper.ExecuteTransWithArrayList(SqlList);
                return res;

            }
            else
                return false;

        }

        /// <summary>
        /// 获得分店库存
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="ProductID">产品代码</param>
        /// <param name="DeptID">部门代码</param>
        /// <param name="BatchNo">批次</param>
        /// <returns></returns>
        public static DataTable GetSubProductCount(string CompanyCD, string ProductID, string DeptID, string BatchNo)
        {
            SqlParameter[] subPara = { 
                                          new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                          new SqlParameter("@ProductID",SqlDbType.Int),
                                          new SqlParameter("@DeptID",SqlDbType.Int),
                                          new SqlParameter("@BatchNo",SqlDbType.VarChar),};
            subPara[0].Value = CompanyCD;
            subPara[1].Value = ProductID;
            subPara[2].Value = DeptID;
            subPara[3].Value = BatchNo;
            return SqlHelper.ExecuteSql(@"SELECT * FROM officedba.SubStorageProduct 
                                WHERE ProductID=@ProductID AND DeptID=@DeptID AND CompanyCD=@CompanyCD AND ISNULL(BatchNo,'')=@BatchNo ", subPara);

        }
        #endregion

        #region 验货入库
        public static bool SubDeliveryBackIn(SubDeliveryBack model, List<SubDeliveryBackDetail> modelList)
        {
            ArrayList SqlList = new ArrayList();

            #region 更新配送单信息
            StringBuilder sbMainSql = new StringBuilder();
            sbMainSql.Append("UPDATE officedba.SubDeliveryBack SET InUserID=@InUserID , InDate=@InDate ,BusiStatus=@BusiStatus ");
            sbMainSql.Append(" where ID=@ID");
            SqlParameter[] MainParas = { 
                                           new SqlParameter("@InUserID",SqlDbType.Int),
                                           new SqlParameter("@InDate",SqlDbType.DateTime),
                                           new SqlParameter("@BusiStatus",SqlDbType.VarChar),
                                           new SqlParameter("@ID",SqlDbType.Int)};
            MainParas[0].Value = model.InUserID;
            MainParas[1].Value = model.InDate;
            MainParas[2].Value = model.BusiStatus;
            MainParas[3].Value = model.ID;
            SqlCommand SqlMainCmd = new SqlCommand() { CommandText = sbMainSql.ToString() };
            SqlMainCmd.Parameters.AddRange(MainParas);
            SqlList.Add(SqlMainCmd);
            #endregion

            StringBuilder sbSubDelSql = new StringBuilder();
            sbSubDelSql.Append("DELETE officedba.SubDeliveryBackDetail WHERE   ");
            sbSubDelSql.Append(" BackNo=@BackNo AND CompanyCD=@CompanyCD ");
            SqlParameter[] delParas = { 
                                          new SqlParameter("@BackNo",SqlDbType.VarChar),
                                          new SqlParameter("@CompanyCD",SqlDbType.VarChar)};
            delParas[0].Value = model.BackNo;
            delParas[1].Value = model.CompanyCD;
            SqlCommand sqlDelCmd = new SqlCommand() { CommandText = sbSubDelSql.ToString() };
            sqlDelCmd.Parameters.AddRange(delParas);
            SqlList.Add(sqlDelCmd);


            /*如果单据有源单 则回写源单退货数量*/
            if (model.SendID > 0)
            {
                StringBuilder sbSetSendSql = new StringBuilder();
                sbSetSendSql.Append("UPDATE officedba.SubDeliverySend SET BackCount=(isnull(BackCount,0)+@BackCount)  ");
                sbSetSendSql.Append(" where ID=@ID");
                SqlParameter[] setSendParas = { 
                                                  new SqlParameter("@BackCount",SqlDbType.Decimal),
                                                  new SqlParameter("@ID",SqlDbType.Int)};
                setSendParas[0].Value = model.BackCount;
                setSendParas[1].Value = model.SendID;

                SqlCommand sqlSetSendCmd = new SqlCommand() { CommandText = sbSetSendSql.ToString() };
                sqlSetSendCmd.Parameters.AddRange(setSendParas);
                SqlList.Add(sqlSetSendCmd);
            }

            foreach (SubDeliveryBackDetail m in modelList)
            {
                SqlList.Add(GetSaveDetail(m));

                #region 添加门店库存流水帐
                XBase.Model.Office.StorageManager.StorageAccountModel sModel = new XBase.Model.Office.StorageManager.StorageAccountModel();
                sModel.BatchNo = m.BackBatchNo;
                sModel.BillNo = m.BackNo;
                sModel.BillType = 20;
                sModel.CompanyCD = m.CompanyCD;
                sModel.StorageID = m.StorageID.Value;
                sModel.ProductID = m.ProductID.Value;
                sModel.Creator = model.Creator.Value;
                sModel.HappenDate = DateTime.Now;
                sModel.Price = m.BackPrice.Value;
                sModel.HappenCount = m.BackCount.Value;
                sModel.PageUrl = model.Remark;
                SqlList.Add(XBase.Data.Office.StorageManager.StorageAccountDBHelper.InsertStorageAccountCommand(sModel, "0"));
                #endregion

                #region 入库操作
                SqlList.Add(XBase.Data.Office.StorageManager.StorageSearchDBHelper.UpdateProductCount(m.CompanyCD, m.ProductID.ToString(), m.StorageID.ToString(), m.BackBatchNo, m.BackCount.Value));
                #endregion

            }
            return SqlHelper.ExecuteTransWithArrayList(SqlList);
        }
        #endregion

    }
}

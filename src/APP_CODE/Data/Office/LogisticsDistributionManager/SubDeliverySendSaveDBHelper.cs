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
    /// <summary>
    /// 保存配送单
    /// </summary>
    public class SubDeliverySendSaveDBHelper
    {

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(SubDeliverySend model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.SubDeliverySend set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND SendNo = @SendNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@SendNo", model.SendNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion


        #region 保存配送单
        public static string AddSubDeliverySend(Model.Office.LogisticsDistributionManager.SubDeliverySend model, List<Model.Office.LogisticsDistributionManager.SubDeliverySendDetail> modellist, Hashtable htExtAttr)
        {

            #region 构造配送单
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SubDeliverySend(");
            strSql.Append("CompanyCD,SendNo,Title,ApplyUserID,ApplyDeptID,OutDeptID,RequireInDate,SendPrice,SendCount,SendFeeSum,BusiStatus,Remark,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID,BatchNo)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@SendNo,@Title,@ApplyUserID,@ApplyDeptID,@OutDeptID,@RequireInDate,@SendPrice,@SendCount,@SendFeeSum,@BusiStatus,@Remark,@Creator,@CreateDate,@BillStatus,@ModifiedDate,@ModifiedUserID,@BatchNo)");
            strSql.Append(";select @ID=@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@SendNo", SqlDbType.VarChar,50),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@ApplyUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplyDeptID", SqlDbType.Int,4),
					new SqlParameter("@OutDeptID", SqlDbType.Int,4),
					new SqlParameter("@RequireInDate", SqlDbType.DateTime),
					new SqlParameter("@SendPrice", SqlDbType.Decimal,9),
					new SqlParameter("@SendCount", SqlDbType.Decimal,9),
					new SqlParameter("@SendFeeSum", SqlDbType.Decimal,9),
					new SqlParameter("@BusiStatus", SqlDbType.Char,1),
					new SqlParameter("@Remark", SqlDbType.VarChar,800), 
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@ID",SqlDbType.Int),
					new SqlParameter("@BatchNo", SqlDbType.VarChar,50)};
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.SendNo;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.ApplyUserID;
            parameters[4].Value = model.ApplyDeptID;
            parameters[5].Value = model.OutDeptID;
            parameters[6].Value = model.RequireInDate;
            parameters[7].Value = model.SendPrice;
            parameters[8].Value = model.SendCount;
            parameters[9].Value = model.SendFeeSum;
            parameters[10].Value = model.BusiStatus;
            parameters[11].Value = model.Remark;
            parameters[12].Value = model.Creator;
            parameters[13].Value = model.CreateDate;
            parameters[14].Value = model.BillStatus;
            parameters[15].Value = model.ModifiedDate;
            parameters[16].Value = model.ModifiedUserID;
            parameters[17].Direction = ParameterDirection.Output;
            parameters[18].Value = model.BatchNo;
            #endregion

            ArrayList SqlList = new ArrayList();
            SqlCommand SqlCmd = new SqlCommand { CommandText = strSql.ToString() };
            SqlCmd.Parameters.AddRange(parameters);
            SqlList.Add(SqlCmd);

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                SqlList.Add(cmd);
            #endregion

            #region 构造配送单明细

            foreach (Model.Office.LogisticsDistributionManager.SubDeliverySendDetail m in modellist)
            {
                SqlList.Add(SaveDetail(m));
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

        #region  更新配送单
        public static bool UpdateSubDeliverySend(SubDeliverySend model, List<SubDeliverySendDetail> modelList, Hashtable htExtAttr)
        {
            #region 配送单
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SubDeliverySend set ");
            strSql.Append("Title=@Title,");
            strSql.Append("ApplyUserID=@ApplyUserID,");
            strSql.Append("ApplyDeptID=@ApplyDeptID,");
            strSql.Append("OutDeptID=@OutDeptID,");
            strSql.Append("RequireInDate=@RequireInDate,");
            strSql.Append("SendPrice=@SendPrice,");
            strSql.Append("SendCount=@SendCount,");
            strSql.Append("SendFeeSum=@SendFeeSum,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID,");
            strSql.Append("BatchNo=@BatchNo");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@ApplyUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplyDeptID", SqlDbType.Int,4),
					new SqlParameter("@OutDeptID", SqlDbType.Int,4),
					new SqlParameter("@RequireInDate", SqlDbType.DateTime),
					new SqlParameter("@SendPrice", SqlDbType.Decimal,9),
					new SqlParameter("@SendCount", SqlDbType.Decimal,9),
					new SqlParameter("@SendFeeSum", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
					new SqlParameter("@BatchNo", SqlDbType.VarChar,50)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.ApplyUserID;
            parameters[3].Value = model.ApplyDeptID;
            parameters[4].Value = model.OutDeptID;
            parameters[5].Value = model.RequireInDate;
            parameters[6].Value = model.SendPrice;
            parameters[7].Value = model.SendCount;
            parameters[8].Value = model.SendFeeSum;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.ModifiedDate;
            parameters[11].Value = model.ModifiedUserID;
            parameters[12].Value = model.BatchNo;

            #endregion

            ArrayList SqlList = new ArrayList();
            SqlCommand SqlCmd = new SqlCommand { CommandText = strSql.ToString() };
            SqlCmd.Parameters.AddRange(parameters);
            SqlList.Add(SqlCmd);


            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                SqlList.Add(cmd);
            #endregion


            #region 配送明细单
            StringBuilder sbDel = new StringBuilder();
            sbDel.Append("DELETE officedba.SubDeliverySendDetail WHERE CompanyCD=@CompanyCD AND SendNo=@SendNo");
            SqlParameter[] delParas = { 
                                          new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                          new SqlParameter("@SendNo",SqlDbType.VarChar)
                                      };
            delParas[0].Value = model.CompanyCD;
            delParas[1].Value = model.SendNo;
            SqlCommand sqlDelCmd = new SqlCommand { CommandText = sbDel.ToString() };
            sqlDelCmd.Parameters.AddRange(delParas);
            SqlList.Add(sqlDelCmd);

            foreach (Model.Office.LogisticsDistributionManager.SubDeliverySendDetail m in modelList)
            {
                SqlList.Add(SaveDetail(m));
            }
            #endregion

            bool result = SqlHelper.ExecuteTransWithArrayList(SqlList);
            return result;

        }

        /// <summary>
        /// 保存明细信息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private static SqlCommand SaveDetail(SubDeliverySendDetail m)
        {
            StringBuilder strSubSql = new StringBuilder();
            strSubSql.Append("insert into officedba.SubDeliverySendDetail(");
            strSubSql.Append("CompanyCD,SendNo,SortNo,ProductID,StorageID,SendCount,SendPrice,SendPriceTotal,UsedUnitCount,UnitID,ExRate,BatchNo)");
            strSubSql.Append(" values (");
            strSubSql.Append("@CompanyCD,@SendNo,@SortNo,@ProductID,@StorageID,@SendCount,@SendPrice,@SendPriceTotal,@UsedUnitCount,@UnitID,@ExRate,@BatchNo)");
            strSubSql.Append(";select @@IDENTITY");
            SqlParameter[] subParameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@SendNo", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@StorageID", SqlDbType.Int,4),
					new SqlParameter("@SendCount", SqlDbType.Decimal,9),
					new SqlParameter("@SendPrice", SqlDbType.Decimal,9),
					new SqlParameter("@SendPriceTotal", SqlDbType.Decimal,9)};
            subParameters[0].Value = m.CompanyCD;
            subParameters[1].Value = m.SendNo;
            subParameters[2].Value = m.SortNo;
            subParameters[3].Value = m.ProductID;
            subParameters[4].Value = m.StorageID;
            subParameters[5].Value = m.SendCount;
            subParameters[6].Value = m.SendPrice;
            subParameters[7].Value = m.SendPriceTotal;

            SqlCommand SqlSubCmd = new SqlCommand { CommandText = strSubSql.ToString() };
            SqlSubCmd.Parameters.AddRange(subParameters);
            if (!string.IsNullOrEmpty(m.BatchNo))
            {
                SqlSubCmd.Parameters.Add(SqlHelper.GetParameter("@BatchNo", m.BatchNo));
            }
            else
            {
                SqlSubCmd.Parameters.Add(SqlHelper.GetParameter("@BatchNo", DBNull.Value));
            }
            if (m.UnitID.HasValue)
            {
                SqlSubCmd.Parameters.Add(SqlHelper.GetParameter("@UnitID", m.UnitID.Value));
            }
            else
            {
                SqlSubCmd.Parameters.Add(SqlHelper.GetParameter("@UnitID", DBNull.Value));
            }
            if (m.UsedUnitCount.HasValue)
            {
                SqlSubCmd.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", m.UsedUnitCount.Value));
            }
            else
            {
                SqlSubCmd.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", DBNull.Value));
            }
            if (m.ExRate.HasValue)
            {
                SqlSubCmd.Parameters.Add(SqlHelper.GetParameter("@ExRate", m.ExRate.Value));
            }
            else
            {
                SqlSubCmd.Parameters.Add(SqlHelper.GetParameter("@ExRate", DBNull.Value));
            }
            return SqlSubCmd;
        }

        #endregion

        #region 更新状态
        public static bool UpdateStatus(SubDeliverySend model, int stype)
        {
            if (stype == 1)
            {
                #region 确认单据
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update officedba.SubDeliverySend set ");
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
                strSql.Append("update officedba.SubDeliverySend set ");
                strSql.Append("BillStatus=@BillStatus,");
                if (stype == 4)
                    strSql.Append("BusiStatus=@BusiStatus, ");
                strSql.Append("ModifiedDate=@ModifiedDate,");
                strSql.Append("ModifiedUserID=@ModifiedUserID");
                strSql.Append(" where ID=@ID ");


                int length = 4;
                int index = 0;
                if (stype == 4)
                    length++;
                SqlParameter[] parameters = new SqlParameter[length];
                parameters[index] = new SqlParameter("@ID", SqlDbType.Int);
                parameters[index++].Value = model.ID;
                parameters[index] = new SqlParameter("@BillStatus", SqlDbType.VarChar);
                parameters[index++].Value = model.BillStatus;
                parameters[index] = new SqlParameter("@ModifiedDate", SqlDbType.DateTime);
                parameters[index++].Value = model.ModifiedDate;
                parameters[index] = new SqlParameter("@ModifiedUserID", SqlDbType.VarChar);
                parameters[index++].Value = model.ModifiedUserID;
                if (stype == 4)
                {
                    parameters[index] = new SqlParameter("@BusiStatus", SqlDbType.VarChar);
                    parameters[index++].Value = model.BusiStatus;
                }

                SqlCommand SqlMainCmd = new SqlCommand();
                SqlMainCmd.CommandText = strSql.ToString();
                SqlMainCmd.Parameters.AddRange(parameters);

                List<SqlCommand> SqlCmdList = new List<SqlCommand>();
                SqlCmdList.Add(SqlMainCmd);


                /*追加取消确认的SqlCommond*/
                if (stype == 4)
                {
                    IList<SqlCommand> tempList = Data.Common.FlowDBHelper.GetCancelConfirmSqlCommond(model.CompanyCD, Convert.ToInt32(ConstUtil.TYPEFLAG_LogisticsDistribution_NO), Convert.ToInt32(ConstUtil.TYPECODE_SubDeliverySend_NO), model.ID, model.ModifiedUserID);
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
                strSql.Append("update officedba.SubDeliverySend set ");
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

        #region 读取配送单信息
        public static DataTable GetSubDeliverySendInfo(SubDeliverySend model)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT sds.* ,");
            sbSql.Append(" (select ei1.EmployeeName from officedba.EmployeeInfo  as ei1  where ei1.ID=sds.ApplyUserID) as ApplyUserIDName,");
            sbSql.Append("(select  di1.DeptName from officedba.DeptInfo as di1 where di1.ID=sds.OutDeptID ) as OutDeptIDName,");
            sbSql.Append("(select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=sds.ApplyDeptID) as ApplyDeptIDName,");
            sbSql.Append("(select ei2.EmployeeName from officedba.EmployeeInfo  as ei2 where ei2.ID=sds.OutUserID) as OutUserIDName,");
            sbSql.Append("(select ei3.EmployeeName from officedba.EmployeeInfo  as ei3 where ei3.ID=sds.InUserID) as InUserIDName,");
            sbSql.Append("(select ei4.EmployeeName from officedba.EmployeeInfo  as ei4 where ei4.ID=sds.Creator) as CreatorName,");
            sbSql.Append("(select ei5.EmployeeName from officedba.EmployeeInfo  as ei5 where ei5.ID=sds.Confirmor) as ConfirmorName,");
            sbSql.Append("(select ei6.EmployeeName from officedba.EmployeeInfo  as ei6 where ei6.ID=sds.Closer) as CloserName ");
            sbSql.Append(" from officedba.SubDeliverySend as sds where sds.CompanyCD=@CompanyCD AND sds.ID=@ID");

            SqlParameter[] Paras = { 
                                   new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                   new SqlParameter("@ID",SqlDbType.Int)};
            Paras[0].Value = model.CompanyCD;
            Paras[1].Value = model.ID;

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
        }

        /*打印使用*/
        public static DataTable GetSubDeliverySendInfoPrint(SubDeliverySend model)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("  SELECT     ID, CompanyCD, SendNo, Title, ApplyUserID, ApplyDeptID, OutDeptID, convert(varchar(10),RequireInDate,120)RequireInDate ,BusiStatus, OutUserID,BatchNo,   ");
            sbSql.AppendLine("  OutDate, InUserID, InDate, BackCount, Remark, Creator,convert(varchar(10),CreateDate,120) CreateDate, BillStatus, Confirmor,convert(varchar(10),ConfirmDate,120) ConfirmDate, Closer, convert(varchar(10),CloseDate,120) CloseDate,convert(varchar(10),ModifiedDate,120) ModifiedDate");
            sbSql.AppendLine("  ,CONVERT(NUMERIC(12,2),ISNULL(sds.SendPrice,0)) SendPrice");
            sbSql.AppendLine("  ,CONVERT(NUMERIC(12,2),ISNULL(sds.SendCount,0)) SendCount");
            sbSql.AppendLine("  ,CONVERT(NUMERIC(12,2),ISNULL(sds.SendFeeSum,0)) SendFeeSum");
            sbSql.AppendLine("  ,ModifiedUserID,   ExtField1,ExtField2,ExtField3,ExtField4,ExtField5,ExtField6,ExtField7,ExtField8,ExtField9,ExtField10,");
            sbSql.AppendLine("  (CASE sds.BusiStatus WHEN '1' THEN '配送申请' WHEN '2' THEN '配送出库' WHEN '3' THEN '配送入库' WHEN '4' THEN '配送完成' END)   ");
            sbSql.AppendLine("   AS BusiStatusText, (CASE sds.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '4' THEN '手工结单' WHEN '5' THEN '自动结单' END)   ");
            sbSql.AppendLine("   AS BillStatusText,  ");
            sbSql.AppendLine("  (SELECT     EmployeeName  ");
            sbSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei1  ");
            sbSql.AppendLine("  WHERE      (ID = sds.ApplyUserID)) AS ApplyUserIDName,  ");
            sbSql.AppendLine("  (SELECT     DeptName  ");
            sbSql.AppendLine("  FROM          officedba.DeptInfo AS di1  ");
            sbSql.AppendLine("   WHERE      (ID = sds.OutDeptID)) AS OutDeptIDName,  ");
            sbSql.AppendLine("   (SELECT     DeptName  ");
            sbSql.AppendLine("   FROM          officedba.DeptInfo AS di2  ");
            sbSql.AppendLine("  WHERE      (ID = sds.ApplyDeptID)) AS ApplyDeptIDName,  ");
            sbSql.AppendLine("   (SELECT     EmployeeName  ");
            sbSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei2  ");
            sbSql.AppendLine("  WHERE      (ID = sds.OutUserID)) AS OutUserIDName,  ");
            sbSql.AppendLine("   (SELECT     EmployeeName  ");
            sbSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei3  ");
            sbSql.AppendLine("  WHERE      (ID = sds.InUserID)) AS InUserIDName,  ");
            sbSql.AppendLine("  (SELECT     EmployeeName  ");
            sbSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei4  ");
            sbSql.AppendLine("  WHERE      (ID = sds.Creator)) AS CreatorName,  ");
            sbSql.AppendLine("  (SELECT     EmployeeName  ");
            sbSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei5  ");
            sbSql.AppendLine("   WHERE      (ID = sds.Confirmor)) AS ConfirmorName,  ");
            sbSql.AppendLine("  (SELECT     EmployeeName  ");
            sbSql.AppendLine("  FROM          officedba.EmployeeInfo AS ei6  ");
            sbSql.AppendLine("   WHERE      (ID = sds.Closer)) AS CloserName  ");
            sbSql.AppendLine("   FROM         officedba.SubDeliverySend AS sds  ");
            sbSql.AppendLine("  where sds.CompanyCD=@CompanyCD AND sds.ID=@ID");

            SqlParameter[] Paras = { 
                                   new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                   new SqlParameter("@ID",SqlDbType.Int)};
            Paras[0].Value = model.CompanyCD;
            Paras[1].Value = model.ID;

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
        }

        #endregion

        #region 读取配送单明细信息
        public static DataTable GetSubDeliverySendDetail(SubDeliverySendDetail model, int DeptID)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(@" SELECT sdsd.*
,CASE WHEN spsp.ID IS NULL  THEN spsp2.ID ELSE spsp.ID END AS ID
,CASE WHEN spsp.DeptID IS NULL THEN spsp2.DeptID ELSE spsp.DeptID END AS DeptID
,CASE WHEN spsp.SendPrice IS NULL THEN spsp2.SendPrice ELSE spsp.SendPrice END AS SendPrice
,CASE WHEN spsp.SendPriceTax IS NULL THEN spsp2.SendPriceTax ELSE spsp.SendPriceTax END AS SendPriceTax
,CASE WHEN spsp.SendTax IS NULL THEN spsp2.SendTax ELSE spsp.SendTax END AS SendTax
,CASE WHEN spsp.Discount IS NULL THEN spsp2.Discount ELSE spsp.Discount END AS Discount
,CASE WHEN spsp.Creator IS NULL THEN spsp2.Creator ELSE spsp.Creator END AS Creator
,CASE WHEN spsp.CreateDate IS NULL THEN spsp2.CreateDate ELSE spsp.CreateDate END AS CreateDate
,CASE WHEN spsp.ModifiedDate IS NULL THEN spsp2.ModifiedDate ELSE spsp.ModifiedDate END AS ModifiedDate
,CASE WHEN spsp.ModifiedUserID IS NULL THEN spsp2.ModifiedUserID ELSE spsp.ModifiedUserID END AS ModifiedUserID
,pi1.ProdNo,pi1.ProductName,pi1.Specification,pi1.UnitID,pi1.MinusIs,pi1.IsBatchNo
,(SELECT ui.CodeName from officedba.CodeUnitType as ui where ui.ID=pi1.UnitID) AS UnitName
, (select  (isnull(sp.ProductCount,0)+isnull(sp.InCount,0)+isnull(sp.RoadCount,0)-isnull(sp.OutCount,0)-isnull(sp.OrderCount,0))  
   from officedba.StorageProduct sp 
   WHERE sdsd.CompanyCD=sp.CompanyCD AND sdsd.StorageID=sp.StorageID AND sdsd.ProductID=sp.ProductID AND isnull(sdsd.BatchNo,'')=isnull(sp.BatchNO,'')  ) AS UseCount
,CASE WHEN pi1.IsBatchNo='1' THEN sds.BatchNo ELSE '' END AS SendBatchNo
FROM officedba.SubDeliverySendDetail sdsd
INNER JOIN officedba.SubDeliverySend sds ON sdsd.CompanyCD=sds.CompanyCD AND sdsd.SendNo=sds.SendNo
INNER JOIN officedba.ProductInfo pi1 ON sdsd.CompanyCD=pi1.CompanyCD AND sdsd.ProductID=pi1.ID
LEFT JOIN officedba.SubProductSendPrice spsp ON sdsd.CompanyCD=spsp.CompanyCD AND sdsd.ProductID=spsp.ProductID AND spsp.DeptID=@DeptID
LEFT JOIN officedba.SubProductSendPrice spsp2 ON sdsd.CompanyCD=spsp2.CompanyCD AND sdsd.ProductID=spsp2.ProductID AND spsp2.DeptID=0
where sdsd.CompanyCD=@CompanyCD AND sdsd.SendNo=@SendNo ");

            SqlParameter[] Paras = { 
                                   new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                   new SqlParameter("@SendNo",SqlDbType.VarChar),
                                   new SqlParameter("@DeptID",SqlDbType.Int)};

            Paras[0].Value = model.CompanyCD;
            Paras[1].Value = model.SendNo;
            Paras[2].Value = DeptID;


            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
            return dt;

        }

        /*打印使用*/
        public static DataTable GetSubDeliverySendDetailPrint(SubDeliverySendDetail model, int DeptID)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(@"SELECT stsd.ID, stsd.CompanyCD, stsd.SendNo, stsd.SortNo, stsd.ProductID, stsd.StorageID,stsd.BatchNo, pi.ProdNo, pi.ProductName, pi.Specification, pi.UnitID, pi.MinusIs,stsd.UnitID AS UsedUnitID,stsd.UsedUnitCount
,CONVERT(NUMERIC(12,2),ISNULL(stsd.SendPrice,0)) SendPrice
,CONVERT(NUMERIC(12,2),ISNULL(stsd.SendCount,0)) SendCount
,CONVERT(NUMERIC(12,2),ISNULL(stsd.SendPriceTotal,0)) SendPriceTotal
,CASE WHEN spsp.ID IS NULL  THEN sp.ID ELSE spsp.ID END AS Expr1
,CASE WHEN spsp.CompanyCD IS NULL  THEN sp.CompanyCD ELSE spsp.CompanyCD END AS Expr2
,CASE WHEN spsp.ProductID IS NULL  THEN sp.ProductID ELSE spsp.ProductID END AS Expr3
,CASE WHEN spsp.DeptID IS NULL  THEN sp.DeptID ELSE spsp.DeptID END AS DeptID
,CASE WHEN spsp.Creator IS NULL  THEN sp.Creator ELSE spsp.Creator END AS Creator
,CASE WHEN spsp.CreateDate IS NULL  THEN sp.CreateDate ELSE spsp.CreateDate END AS CreateDate
,CASE WHEN spsp.ModifiedUserID IS NULL  THEN sp.ModifiedUserID ELSE spsp.ModifiedUserID END AS ModifiedUserID
,CONVERT(NUMERIC(12,2),ISNULL(CASE WHEN spsp.SendPrice IS NULL  THEN sp.SendPrice ELSE spsp.SendPrice END,0)) SendPrice
,CONVERT(NUMERIC(12,2),ISNULL(CASE WHEN spsp.SendPriceTax IS NULL  THEN sp.SendPriceTax ELSE spsp.SendPriceTax END,0)) SendPriceTax
,CONVERT(NUMERIC(12,2),ISNULL(CASE WHEN spsp.SendTax IS NULL  THEN sp.SendTax ELSE spsp.SendTax END,0)) SendTax
,CONVERT(NUMERIC(12,2),ISNULL(CASE WHEN spsp.Discount IS NULL  THEN sp.Discount ELSE spsp.Discount END,0)) Discount
,(SELECT     ISNULL(ProductCount, 0) + ISNULL(InCount, 0) + ISNULL(RoadCount, 0) - ISNULL(OutCount, 0) - ISNULL(OrderCount, 0) AS Expr1  
FROM          officedba.StorageProduct AS sp  
 WHERE      (StorageID = stsd.StorageID) AND (ProductID = stsd.ProductID) AND (CompanyCD = stsd.CompanyCD) AND isnull(stsd.BatchNo,'')=isnull(sp.BatchNO,'')  ) AS UseCount,  
 (SELECT     CodeName  
FROM          officedba.CodeUnitType AS ui  
WHERE      (ID = pi.UnitID)) AS UnitName, 
(SELECT     CodeName  
FROM          officedba.CodeUnitType AS ui1  
WHERE      (ID = stsd.UnitID)) AS UsedUnitName, 
(SELECT     StorageName  
FROM          officedba.StorageInfo AS si  
WHERE      (ID = stsd.StorageID)) AS StorageName  
 FROM         officedba.SubDeliverySendDetail AS stsd 
 INNER JOIN  officedba.ProductInfo AS pi ON stsd.CompanyCD=PI.CompanyCD AND stsd.ProductID = pi.ID  
 LEFT JOIN  officedba.SubProductSendPrice AS sp ON stsd.CompanyCD = sp.CompanyCD AND stsd.ProductID = sp.ProductID AND sp.DeptID=@DeptID
 LEFT JOIN officedba.SubProductSendPrice spsp ON stsd.CompanyCD=spsp.CompanyCD AND stsd.ProductID=spsp.ProductID AND spsp.DeptID=0
WHERE stsd.CompanyCD=@CompanyCD AND stsd.SendNo=@SendNo  ");

            SqlParameter[] Paras = { 
                                   new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                   new SqlParameter("@SendNo",SqlDbType.VarChar),
                                   new SqlParameter("@DeptID",SqlDbType.Int)};

            Paras[0].Value = model.CompanyCD;
            Paras[1].Value = model.SendNo;
            Paras[2].Value = DeptID;


            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), Paras);

            if (dt != null && dt.Rows.Count > 0)
                return dt;
            else
                return GetSubDeliverySendDetailPrint(model, 0);

        }


        #endregion

        #region 实时读取库存量
        public static string GetProductUseCount(SubDeliverySendDetail model)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select  @UseCount=((isnull(sp.ProductCount,0)+isnull(sp.InCount,0)+isnull(sp.RoadCount,0)-isnull(sp.OutCount,0)-isnull(sp.OrderCount,0)))  from officedba.StorageProduct as sp where sp.StorageID=@StorageID AND sp.ProductID=@ProductID AND sp.CompanyCD=@CompanyCD ");
            SqlParameter[] Paras = { 
                                       new SqlParameter("@UseCount",SqlDbType.Int),
                                       new SqlParameter("@StorageID",SqlDbType.Int),
                                       new SqlParameter("@ProductID",SqlDbType.Int),
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar)
                                    };
            Paras[0].Direction = ParameterDirection.Output;
            Paras[1].Value = model.StorageID;
            Paras[2].Value = model.ProductID;
            Paras[3].Value = model.CompanyCD;

            SqlHelper.ExecuteSql(sbSql.ToString(), Paras);

            return Paras[0].Value.ToString();


        }
        #endregion

        #region 执行配送出库
        public static bool RunSubDeliverySendOut(SubDeliverySend model, List<SubDeliverySendDetail> modelList)
        {
            ArrayList SqlList = new ArrayList();

            #region 配送明细单
            StringBuilder sbDel = new StringBuilder();
            sbDel.Append("DELETE officedba.SubDeliverySendDetail WHERE CompanyCD=@CompanyCD AND SendNo=@SendNo");
            SqlParameter[] delParas = { 
                                          new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                          new SqlParameter("@SendNo",SqlDbType.VarChar)
                                      };
            delParas[0].Value = model.CompanyCD;
            delParas[1].Value = model.SendNo;
            SqlCommand sqlDelCmd = new SqlCommand { CommandText = sbDel.ToString() };
            sqlDelCmd.Parameters.AddRange(delParas);
            SqlList.Add(sqlDelCmd);
            foreach (Model.Office.LogisticsDistributionManager.SubDeliverySendDetail m in modelList)
            {

                SqlList.Add(SaveDetail(m));

                #region 添加门店库存流水帐

                XBase.Model.Office.StorageManager.StorageAccountModel sModel = new XBase.Model.Office.StorageManager.StorageAccountModel();
                sModel.BatchNo = m.BatchNo;
                sModel.BillNo = m.SendNo;
                sModel.BillType = 19;
                sModel.CompanyCD = m.CompanyCD;
                sModel.StorageID = m.StorageID.Value;
                sModel.ProductID = m.ProductID.Value;
                sModel.Creator = model.Creator.Value;
                sModel.HappenDate = DateTime.Now;
                sModel.Price = m.SendPrice.Value;
                sModel.HappenCount = m.SendCount.Value;
                sModel.PageUrl = model.Remark;
                SqlList.Add(XBase.Data.Office.StorageManager.StorageAccountDBHelper.InsertStorageAccountCommand(sModel, "1"));

                #endregion

                // 更新库存
                SqlList.Add(XBase.Data.Office.StorageManager.StorageSearchDBHelper.UpdateProductCount(m.CompanyCD, m.ProductID.ToString(), m.StorageID.ToString()
                    , m.BatchNo, -m.SendCount.Value));
            }
            #endregion

            #region 更新配送单
            StringBuilder sbMainSql = new StringBuilder();
            sbMainSql.Append("UPDATE  officedba.SubDeliverySend SET OutUserID=@OutUserID,OutDate=@OutDate,BusiStatus=@BusiStatus ,");
            sbMainSql.Append("SendPrice=@SendPrice,SendCount=@SendCount ");
            sbMainSql.Append(" WHERE ID=@ID ");
            SqlParameter[] MainParas = { 
                                           new SqlParameter("@OutUserID",SqlDbType.Int),
                                           new SqlParameter("@OutDate",SqlDbType.DateTime),
                                           new SqlParameter("@BusiStatus",SqlDbType.VarChar),
                                           new SqlParameter("@ID",SqlDbType.Int),
                                           new SqlParameter("@SendPrice",SqlDbType.Decimal),
                                           new SqlParameter("@SendCount",SqlDbType.Decimal)
                                       };
            MainParas[0].Value = model.OutUserID;
            MainParas[1].Value = model.OutDate;
            MainParas[2].Value = model.BusiStatus;
            MainParas[3].Value = model.ID;
            MainParas[4].Value = model.SendPrice;
            MainParas[5].Value = model.SendCount;
            SqlCommand SqlMainCmd = new SqlCommand() { CommandText = sbMainSql.ToString() };
            SqlMainCmd.Parameters.AddRange(MainParas);
            SqlList.Add(SqlMainCmd);
            #endregion

            return SqlHelper.ExecuteTransWithArrayList(SqlList);

        }
        #endregion

        #region 执行验货入库操作
        public static bool RunSubDeliverySendIn(SubDeliverySend model)
        {
            #region 读取配送单明细
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT sdsd.SendNo,sdsd.ProductID,sdsd.SendPrice,sdsd.SendCount,pi1.IsBatchNo FROM officedba.SubDeliverySendDetail sdsd ");
            sbSql.Append(" LEFT JOIN officedba.ProductInfo pi1 ON pi1.ID=sdsd.ProductID ");
            sbSql.Append(" where sdsd.CompanyCD=@CompanyCD AND sdsd.SendNo=@SendNo");

            SqlParameter[] Paras = { 
                                   new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                   new SqlParameter("@SendNo",SqlDbType.VarChar)};
            Paras[0].Value = model.CompanyCD;
            Paras[1].Value = model.SendNo;
            DataTable dtDetail = SqlHelper.ExecuteSql(sbSql.ToString(), Paras);


            ArrayList SqlList = new ArrayList();
            int id = 0;
            decimal count = 0m;
            /*实时判断明细是否存在*/
            if (dtDetail != null && dtDetail.Rows.Count > 0)
            {
                #region 更新配送单信息
                StringBuilder sbMainSql = new StringBuilder();
                sbMainSql.Append("UPDATE officedba.SubDeliverySend SET InUserID=@InUserID , InDate=@InDate ,BusiStatus=@BusiStatus ");
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
                foreach (DataRow row in dtDetail.Rows)
                {
                    string BatchNo = "";
                    #region 添加门店库存流水帐
                    if (row["IsBatchNo"].ToString() == "1")
                    {
                        BatchNo = model.BatchNo;
                    }
                    SubStorageAccountModel aModel = new SubStorageAccountModel();
                    aModel.BatchNo = BatchNo;
                    aModel.BillNo = row["SendNo"].ToString();
                    aModel.BillType = 2;
                    aModel.CompanyCD = model.CompanyCD;
                    aModel.Creator = model.Creator;
                    aModel.DeptID = model.ApplyDeptID;
                    aModel.HappenDate = DateTime.Now;
                    if (int.TryParse(row["ProductID"].ToString(), out id))
                    {
                        aModel.ProductID = id;
                    }
                    if (decimal.TryParse(row["SendPrice"].ToString(), out count))
                    {
                        aModel.Price = count;
                    }
                    if (decimal.TryParse(row["SendCount"].ToString(), out count))
                    {
                        aModel.HappenCount = count;
                    }
                    aModel.PageUrl = model.Remark;
                    SqlList.Add(XBase.Data.Office.SubStoreManager.SubStorageAccountDBHelper.GetCountAndInsertCommand(aModel));
                    #endregion

                    SqlList.Add(XBase.Data.Office.LogisticsDistributionManager.StorageProductQueryDBHelper.UpdateProductCount(model.CompanyCD
                            , row["ProductID"].ToString(), model.ApplyDeptID.ToString(), BatchNo, count));

                }
                return SqlHelper.ExecuteTransWithArrayList(SqlList);
            }
            else
                return false;
            #endregion
        }
        #endregion


        #region 门店配送统计表

        #region 分页
        public static DataTable SubDeliverySendBackStat(Hashtable htPara, int PageIndex, int PageSize, string OrderBy, ref int TotalCount)
        {
            SqlParameter[] Paras = new SqlParameter[htPara.Count];
            string sConditionSend = string.Empty;
            string sConditionBack = string.Empty;
            int index = 0;
            Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[index++].Value = htPara["CompanyCD"];
            if (htPara.ContainsKey("ApplyDeptID"))
            {
                sConditionSend += " AND t2.ApplyDeptID=@ApplyDeptID ";
                sConditionBack += " AND r2.ApplyDeptID=@ApplyDeptID ";
                Paras[index] = new SqlParameter("@ApplyDeptID", SqlDbType.Int);
                Paras[index++].Value = htPara["ApplyDeptID"];
            }
            if (htPara.ContainsKey("StartDate"))
            {
                sConditionSend += " AND t2.ConfirmDate>= Convert(datetime,@StartDate) ";
                sConditionBack += " AND r2.ConfirmDate >=Convert(datetime,@StartDate) ";
                Paras[index] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["StartDate"];
            }
            if (htPara.ContainsKey("EndDate"))
            {
                sConditionSend += " AND t2.ConfirmDate <= DATEADD(day, 1,Convert(datetime,@EndDate)) ";
                sConditionBack += " AND r2.ConfirmDate <= DATEADD(day, 1,Convert(datetime,@EndDate)) ";
                Paras[index] = new SqlParameter("@EndDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["EndDate"];
            }
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT G1.CompanyCD AS BackCompanyCD,G2.CompanyCD AS SendCompanyCD,G1.BackApplyDeptID,G1.BackProductID,G2.SendApplyDeptID,G2.SendProductID, ");
            sbSql.AppendLine("G1.TotalBackCount,g1.TotalBackPrice,g2.TotalSendCount,g2.TotalSendPrice, ");
            sbSql.AppendLine("ISNULL((Select di1.DeptName from officedba.DeptInfo as di1 where di1.ID=G1.BackApplyDeptID),(select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=G2.SendApplyDeptID)) as DeptName, ");
            sbSql.AppendLine("isnull(BackProductName,SendProductName) AS ProductName, ");
            sbSql.AppendLine("isnull(BackSpan,SendSpan) as Span ,isnull(BackUnitName,SendUnitName) as UnitName ");
            sbSql.AppendLine(" from ");
            sbSql.AppendLine("(SELECT y2.CompanyCD,y2.SendApplyDeptID,y2.TotalSendCount,y2.TotalSendPrice,pi2.ProductName as SendProductName,pi2.Specification as SendSpan,(select cu.CodeName from officedba.CodeUnitType as cu where cu.ID=pi2.UnitID) as SendUnitName,y2.SendProductID from ");
            sbSql.AppendLine("(SELECT t1.CompanyCD,t2.ApplyDeptID AS SendApplyDeptID,t1.ProductID AS SendProductID,sum(t1.SendCount) as TotalSendCount,sum(t1.SendPriceTotal) as TotalSendPrice from officedba.SubDeliverySendDetail as t1 left join officedba.SubDeliverySend as t2  ");
            sbSql.AppendLine("on t1.SendNo=t2.SendNo AND t1.CompanyCD=t2.CompanyCD  where t2.CompanyCD=@CompanyCD and Convert(int,t2.BillStatus)>=2 ");
            /*追加查询条件*/
            sbSql.AppendLine(sConditionSend);
            sbSql.AppendLine("group by t2.ApplyDeptID,t1.ProductID,t1.CompanyCD) as y2 left join officedba.ProductInfo as pi2 on pi2.ID=y2.SendProductID) AS G2  ");
            sbSql.AppendLine("FULL JOIN ");
            sbSql.AppendLine("(select y1.CompanyCD,y1.TotalBackCount,y1.TotalBackPrice,y1.BackApplyDeptID,pi1.ProductName as BackProductName,pi1.Specification as BackSpan,(select cu.CodeName from officedba.CodeUnitType as cu where cu.ID=pi1.UnitID) as BackUnitName,y1.BackProductID  ");
            sbSql.AppendLine("from (select r1.CompanyCD,Sum(r1.BackCount) as TotalBackCount,sum(r1.BackPriceTotal) as TotalBackPrice,r2.ApplyDeptID as BackApplyDeptID,r1.ProductID AS BackProductID from officedba.SubDeliveryBackDetail as r1 left join officedba.SubDeliveryBack  as r2 ");
            sbSql.AppendLine("on r1.BackNo=r2.BackNo and r1.CompanyCD=r2.CompanyCD  where r2.CompanyCD=@CompanyCD AND Convert(int,r2.BillStatus)>=2 ");
            /*追加查询条件*/
            sbSql.AppendLine(sConditionBack);
            sbSql.AppendLine("group by r2.ApplyDeptID,r1.ProductID,r1.CompanyCD) as y1 left join officedba.ProductInfo as pi1 on pi1.ID=y1.BackProductID) AS  G1 ");
            sbSql.AppendLine("ON G1.CompanyCD=G2.CompanyCD AND G1.BackProductID=G2.SendProductID AND G1.BackApplyDeptID=G2.SendApplyDeptID ");
            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, Paras, ref TotalCount);

        }
        #endregion

        #region 不分页
        public static DataTable SubDeliverySendBackStat(Hashtable htPara)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlParameter[] Paras = new SqlParameter[htPara.Count - 1];
            string sConditionSend = string.Empty;
            string sConditionBack = string.Empty;
            int index = 0;
            Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[index++].Value = htPara["CompanyCD"];
            if (htPara.ContainsKey("ApplyDeptID"))
            {
                sConditionSend += " AND t2.ApplyDeptID=@ApplyDeptID ";
                sConditionBack += " AND r2.ApplyDeptID=@ApplyDeptID ";
                Paras[index] = new SqlParameter("@ApplyDeptID", SqlDbType.Int);
                Paras[index++].Value = htPara["ApplyDeptID"];
            }
            if (htPara.ContainsKey("StartDate"))
            {
                sConditionSend += " AND t2.ConfirmDate>= Convert(datetime,@StartDate) ";
                sConditionBack += " AND r2.ConfirmDate >=Convert(datetime,@StartDate) ";
                Paras[index] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["StartDate"];
            }
            if (htPara.ContainsKey("EndDate"))
            {
                sConditionSend += " AND t2.ConfirmDate <= Convert(datetime,@EndDate) ";
                sConditionBack += " AND r2.ConfirmDate < DATEADD(day, 1,Convert(datetime,@EndDate)) ";
                Paras[index] = new SqlParameter("@EndDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["EndDate"];
            }
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT G1.CompanyCD AS BackCompanyCD,G2.CompanyCD AS SendCompanyCD,G1.BackApplyDeptID,G1.BackProductID,G2.SendApplyDeptID,G2.SendProductID, ");
            sbSql.AppendLine(" Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(G1.TotalBackCount,0)))+'&nbsp;' as TotalBackCount,");
            sbSql.AppendLine(" Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(g1.TotalBackPrice,0)))+'&nbsp;' as TotalBackPrice,");
            sbSql.AppendLine(" Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(g2.TotalSendCount,0)))+'&nbsp;' as TotalSendCount,");
            sbSql.AppendLine(" Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(g2.TotalSendPrice,0)))+'&nbsp;' as TotalSendPrice, ");
            sbSql.AppendLine("ISNULL((Select di1.DeptName from officedba.DeptInfo as di1 where di1.ID=G1.BackApplyDeptID),(select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=G2.SendApplyDeptID)) as DeptName, ");
            sbSql.AppendLine("isnull(BackProductName,SendProductName) AS ProductName, ");
            sbSql.AppendLine("isnull(BackSpan,SendSpan) as Span ,isnull(BackUnitName,SendUnitName) as UnitName ");
            sbSql.AppendLine(" from ");
            sbSql.AppendLine("(SELECT y2.CompanyCD,y2.SendApplyDeptID,y2.TotalSendCount,y2.TotalSendPrice,pi2.ProductName as SendProductName,pi2.Specification as SendSpan,(select cu.CodeName from officedba.CodeUnitType as cu where cu.ID=pi2.UnitID) as SendUnitName,y2.SendProductID from ");
            sbSql.AppendLine("(SELECT t1.CompanyCD,t2.ApplyDeptID AS SendApplyDeptID,t1.ProductID AS SendProductID,sum(t1.SendCount) as TotalSendCount,sum(t1.SendPriceTotal) as TotalSendPrice from officedba.SubDeliverySendDetail as t1 left join officedba.SubDeliverySend as t2  ");
            sbSql.AppendLine("on t1.SendNo=t2.SendNo AND t1.CompanyCD=t2.CompanyCD  where t2.CompanyCD=@CompanyCD and Convert(int,t2.BillStatus)>=2 ");
            /*追加查询条件*/
            sbSql.AppendLine(sConditionSend);
            sbSql.AppendLine("group by t2.ApplyDeptID,t1.ProductID,t1.CompanyCD) as y2 left join officedba.ProductInfo as pi2 on pi2.ID=y2.SendProductID) AS G2  ");
            sbSql.AppendLine("FULL JOIN ");
            sbSql.AppendLine("(select y1.CompanyCD,y1.TotalBackCount,y1.TotalBackPrice,y1.BackApplyDeptID,pi1.ProductName as BackProductName,pi1.Specification as BackSpan,(select cu.CodeName from officedba.CodeUnitType as cu where cu.ID=pi1.UnitID) as BackUnitName,y1.BackProductID  ");
            sbSql.AppendLine("from (select r1.CompanyCD,Sum(r1.BackCount) as TotalBackCount,sum(r1.BackPriceTotal) as TotalBackPrice,r2.ApplyDeptID as BackApplyDeptID,r1.ProductID AS BackProductID from officedba.SubDeliveryBackDetail as r1 left join officedba.SubDeliveryBack  as r2 ");
            sbSql.AppendLine("on r1.BackNo=r2.BackNo and r1.CompanyCD=r2.CompanyCD  where r2.CompanyCD=@CompanyCD AND Convert(int,r2.BillStatus)>=2 ");
            /*追加查询条件*/
            sbSql.AppendLine(sConditionBack);
            sbSql.AppendLine("group by r2.ApplyDeptID,r1.ProductID,r1.CompanyCD) as y1 left join officedba.ProductInfo as pi1 on pi1.ID=y1.BackProductID) AS  G1 ");
            sbSql.AppendLine("ON G1.CompanyCD=G2.CompanyCD AND G1.BackProductID=G2.SendProductID AND G1.BackApplyDeptID=G2.SendApplyDeptID ");
            sbSql.AppendLine(" ORDER BY " + htPara["OrderBy"]);
            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);

        }
        #endregion
        #endregion


        #region 门店配送明细表

        #region 分页
        public static DataTable SubDeliverySendDetailReport(Hashtable htPara, int PageIndex, int PageSize, string OrderBy, ref int TotalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            SqlParameter[] Paras = new SqlParameter[htPara.Count];
            int index = 0;
            string sCondition = string.Empty;
            Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[index++].Value = htPara["CompanyCD"];
            if (htPara.ContainsKey("ApplyDeptID"))
            {
                sCondition += " AND t2.ApplyDeptID=@ApplyDeptID ";
                Paras[index] = new SqlParameter("@ApplyDeptID", SqlDbType.Int);
                Paras[index++].Value = htPara["ApplyDeptID"];
            }
            if (htPara.ContainsKey("StartDate"))
            {
                sCondition += " AND t2.ConfirmDate>= Convert(datetime,@StartDate) ";
                Paras[index] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["StartDate"];
            }
            if (htPara.ContainsKey("EndDate"))
            {
                sCondition += " AND t2.ConfirmDate <DATEADD(day, 1,Convert(datetime,@EndDate)) ";
                Paras[index] = new SqlParameter("@EndDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["EndDate"];
            }
            sbSql.AppendLine("   SELECT pi1.ProductName,pi1.Specification as span,q1.OutDate,q1.TotalSendCount,q1.TotalSendPrice,q1.SendPrice,  ");
            sbSql.AppendLine("  (select cu.CodeName from officedba.CodeUnitType as cu where cu.ID=pi1.UnitID) AS UnitName, ");
            sbSql.AppendLine("   (SELECT di1.DeptName from officedba.DeptInfo as di1 where di1.ID=q1.ApplyDeptID) AS ApplyDept, ");
            sbSql.AppendLine("  (select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=q1.OutDeptID) AS OutDept  ");
            sbSql.AppendLine("   FROM ( ");
            sbSql.AppendLine("  select y1.ProductID,y1.ApplyDeptID,y1.OutDate,y1.OutDeptID,y1.SendPrice, ");
            sbSql.AppendLine("   sum(y1.SendCount) as TotalSendCount, ");
            sbSql.AppendLine("    sum(y1.SendPriceTotal) as TotalSendPrice  ");
            sbSql.AppendLine("   from  ( ");
            sbSql.AppendLine("    select t1.ProductID,t1.SendCount,t1.SendPrice,t1.SendPriceTotal ,t2.ApplyDeptID,t2.OutDate ,t2.OutDeptID  ");
            sbSql.AppendLine("   FROM ");
            sbSql.AppendLine("  officedba.SubDeliverySendDetail as t1 ");
            sbSql.AppendLine("     left join  ");
            sbSql.AppendLine("   officedba.SubDeliverySend as t2  ");
            sbSql.AppendLine("     on t1.SendNo=t2.SendNo and t1.CompanyCD=t2.CompanyCD  where t1.CompanyCD=@CompanyCD and Convert(int,t2.BillStatus)>=2 and t2.Outdate is not null  ");
            sbSql.AppendLine(sCondition);
            sbSql.AppendLine("    ) as y1   ");
            sbSql.AppendLine("   group by y1.ProductID,y1.ApplyDeptID,y1.OutDate,y1.OutDeptID,y1.SendPrice ) AS q1 ");
            sbSql.AppendLine("   left join officedba.ProductInfo as pi1 ");
            sbSql.AppendLine("   on q1.ProductID=pi1.ID  ");
            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, Paras, ref TotalCount);
        }

        #endregion

        #region 不分页
        public static DataTable SubDeliverySendDetailReport(Hashtable htPara)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sbSql = new StringBuilder();
            SqlParameter[] Paras = new SqlParameter[htPara.Count - 1];
            int index = 0;
            string sCondition = string.Empty;
            Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[index++].Value = htPara["CompanyCD"];
            if (htPara.ContainsKey("ApplyDeptID"))
            {
                sCondition += " AND t2.ApplyDeptID=@ApplyDeptID ";
                Paras[index] = new SqlParameter("@ApplyDeptID", SqlDbType.Int);
                Paras[index++].Value = htPara["ApplyDeptID"];
            }
            if (htPara.ContainsKey("StartDate"))
            {
                sCondition += " AND t2.ConfirmDate>= Convert(datetime,@StartDate) ";
                Paras[index] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["StartDate"];
            }
            if (htPara.ContainsKey("EndDate"))
            {
                sCondition += " AND t2.ConfirmDate <DATEADD(day, 1,Convert(datetime,@EndDate)) ";
                Paras[index] = new SqlParameter("@EndDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["EndDate"];
            }
            sbSql.AppendLine("   SELECT pi1.ProductName,pi1.Specification as span,q1.OutDate,");
            sbSql.AppendLine(" Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(q1.TotalSendCount,0)))+'&nbsp;' as TotalSendCount,");
            sbSql.AppendLine(" Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(q1.TotalSendPrice,0)))+'&nbsp;' as TotalSendPrice,");
            sbSql.AppendLine("  Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(q1.SendPrice,0)))+'&nbsp;' as SendPrice,  ");
            sbSql.AppendLine("  (select cu.CodeName from officedba.CodeUnitType as cu where cu.ID=pi1.UnitID) AS UnitName, ");
            sbSql.AppendLine("   (SELECT di1.DeptName from officedba.DeptInfo as di1 where di1.ID=q1.ApplyDeptID) AS ApplyDept, ");
            sbSql.AppendLine("  (select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=q1.OutDeptID) AS OutDept  ");
            sbSql.AppendLine("   FROM ( ");
            sbSql.AppendLine("  select y1.ProductID,y1.ApplyDeptID,y1.OutDate,y1.OutDeptID,y1.SendPrice, ");
            sbSql.AppendLine("   sum(y1.SendCount) as TotalSendCount, ");
            sbSql.AppendLine("    sum(y1.SendPriceTotal) as TotalSendPrice  ");
            sbSql.AppendLine("   from  ( ");
            sbSql.AppendLine("    select t1.ProductID,t1.SendCount,t1.SendPrice,t1.SendPriceTotal ,t2.ApplyDeptID,t2.OutDate ,t2.OutDeptID  ");
            sbSql.AppendLine("   FROM ");
            sbSql.AppendLine("  officedba.SubDeliverySendDetail as t1 ");
            sbSql.AppendLine("     left join  ");
            sbSql.AppendLine("   officedba.SubDeliverySend as t2  ");
            sbSql.AppendLine("     on t1.SendNo=t2.SendNo and t1.CompanyCD=t2.CompanyCD  where t1.CompanyCD=@CompanyCD and Convert(int,t2.BillStatus)>=2 and t2.Outdate is not null  ");
            sbSql.AppendLine(sCondition);
            sbSql.AppendLine("    ) as y1   ");
            sbSql.AppendLine("   group by y1.ProductID,y1.ApplyDeptID,y1.OutDate,y1.OutDeptID,y1.SendPrice ) AS q1 ");
            sbSql.AppendLine("   left join officedba.ProductInfo as pi1 ");
            sbSql.AppendLine("   on q1.ProductID=pi1.ID  ");
            sbSql.AppendLine(" ORDER BY " + htPara["OrderBy"]);

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);

        }
        #endregion
        #endregion
    }
}

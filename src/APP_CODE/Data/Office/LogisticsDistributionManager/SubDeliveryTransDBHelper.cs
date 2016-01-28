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
    public class SubDeliveryTransDBHelper
    {
        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(SubDeliveryTrans model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.SubDeliveryTrans set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND TransNo = @TransNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@TransNo", model.TransNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion

        #region 保存门店调拨单 及其明细
        public static string AddSubDeliveryTrans(SubDeliveryTrans model, List<SubDeliveryTransDetail> modellist, Hashtable htExtAttr)
        {

            #region 构造调拨单
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SubDeliveryTrans(");
            strSql.Append("CompanyCD,TransNo,Title,ApplyUserID,OutDeptID,InDeptID,TransDeptID,TransPrice,TransFeeSum,TransCount,Reason,BusiStatus,Remark,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@TransNo,@Title,@ApplyUserID,@OutDeptID,@InDeptID,@TransDeptID,@TransPrice,@TransFeeSum,@TransCount,@Reason,@BusiStatus,@Remark,@Creator,@CreateDate,@BillStatus,@ModifiedDate,@ModifiedUserID)");
            strSql.Append(";select @ID=@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@TransNo", SqlDbType.VarChar,50),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@ApplyUserID", SqlDbType.Int,4),
					new SqlParameter("@OutDeptID", SqlDbType.Int,4),
					new SqlParameter("@InDeptID", SqlDbType.Int,4),
					new SqlParameter("@TransDeptID", SqlDbType.Int,4),
					new SqlParameter("@TransPrice", SqlDbType.Decimal,9),
					new SqlParameter("@TransFeeSum", SqlDbType.Decimal,9),
					new SqlParameter("@TransCount", SqlDbType.Decimal,9),
					new SqlParameter("@Reason", SqlDbType.VarChar,1024),
					new SqlParameter("@BusiStatus", SqlDbType.Char,1),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@ID",SqlDbType.Int)};
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.TransNo;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.ApplyUserID;
            parameters[4].Value = model.OutDeptID;
            parameters[5].Value = model.InDeptID;
            if (model.TransDeptID.HasValue)
            {
                parameters[6].Value = model.TransDeptID;
            }
            else
            {
                parameters[6].Value = DBNull.Value;
            }
            parameters[7].Value = model.TransPrice;
            parameters[8].Value = model.TransFeeSum;
            parameters[9].Value = model.TransCount;
            parameters[10].Value = model.Reason;
            parameters[11].Value = model.BusiStatus;
            parameters[12].Value = model.Remark;
            parameters[13].Value = model.Creator;
            parameters[14].Value = model.CreateDate;
            parameters[15].Value = model.BillStatus;
            parameters[16].Value = model.ModifiedDate;
            parameters[17].Value = model.ModifiedUserID;
            parameters[18].Direction = ParameterDirection.Output;
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


            #region 构造调拨单明细
            foreach (SubDeliveryTransDetail m in modellist)
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

        /// <summary>
        /// 保存数据明细
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static SqlCommand SaveDetail(SubDeliveryTransDetail m)
        {
            StringBuilder strSubSql = new StringBuilder();
            strSubSql.Append("insert into officedba.SubDeliveryTransDetail(");
            strSubSql.Append("CompanyCD,TransNo,SortNo,ProductID,TransCount,TransPrice,TransPriceTotal,UsedUnitCount,UnitID,ExRate,BatchNo,Remark)");
            strSubSql.Append(" values (");
            strSubSql.Append("@CompanyCD,@TransNo,@SortNo,@ProductID,@TransCount,@TransPrice,@TransPriceTotal,@UsedUnitCount,@UnitID,@ExRate,@BatchNo,@Remark)");
            strSubSql.Append(";select @@IDENTITY");
            SqlParameter[] subParameters = {
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
                    new SqlParameter("@TransNo", SqlDbType.VarChar,50),
                    new SqlParameter("@SortNo", SqlDbType.Int,4),
                    new SqlParameter("@ProductID", SqlDbType.Int,4),
                    new SqlParameter("@TransCount", SqlDbType.Decimal,9),
                    new SqlParameter("@TransPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@TransPriceTotal", SqlDbType.Decimal,9),
                    new SqlParameter("@BatchNo", SqlDbType.VarChar,50)};
            subParameters[0].Value = m.CompanyCD;
            subParameters[1].Value = m.TransNo;
            subParameters[2].Value = m.SortNo;
            subParameters[3].Value = m.ProductID;
            subParameters[4].Value = m.TransCount;
            subParameters[5].Value = m.TransPrice;
            subParameters[6].Value = m.TransPriceTotal;
            if (String.IsNullOrEmpty(m.BatchNo))
            {
                subParameters[7].Value = DBNull.Value;
            }
            else
            {
                subParameters[7].Value = m.BatchNo;
            }

            SqlCommand SqlSubCmd = new SqlCommand { CommandText = strSubSql.ToString() };
            SqlSubCmd.Parameters.AddRange(subParameters);
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
            SqlSubCmd.Parameters.Add(SqlHelper.GetParameter("@Remark", m.Remark));
            return SqlSubCmd;
        }

        #region 更新门店调拨单 及其明细
        public static bool UpdateSubDeliveryTrans(SubDeliveryTrans model, List<SubDeliveryTransDetail> modelList, Hashtable htExtAttr)
        {

            #region 更新调拨单主表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SubDeliveryTrans set ");
            strSql.Append("Title=@Title,");
            strSql.Append("ApplyUserID=@ApplyUserID,");
            strSql.Append("OutDeptID=@OutDeptID,");
            strSql.Append("InDeptID=@InDeptID,");
            strSql.Append("TransDeptID=@TransDeptID,");
            strSql.Append("TransPrice=@TransPrice,");
            strSql.Append("TransFeeSum=@TransFeeSum,");
            strSql.Append("TransCount=@TransCount,");
            strSql.Append("Reason=@Reason,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@ApplyUserID", SqlDbType.Int,4),
					new SqlParameter("@OutDeptID", SqlDbType.Int,4),
					new SqlParameter("@InDeptID", SqlDbType.Int,4),
					new SqlParameter("@TransDeptID", SqlDbType.Int,4),
					new SqlParameter("@TransPrice", SqlDbType.Decimal,9),
					new SqlParameter("@TransFeeSum", SqlDbType.Decimal,9),
					new SqlParameter("@TransCount", SqlDbType.Decimal,9),
					new SqlParameter("@Reason", SqlDbType.VarChar,1024),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.ApplyUserID;
            parameters[3].Value = model.OutDeptID;
            parameters[4].Value = model.InDeptID;
            if (model.TransDeptID.HasValue)
            {
                parameters[5].Value = model.TransDeptID;
            }
            else
            {
                parameters[5].Value = DBNull.Value;
            }
            parameters[6].Value = model.TransPrice;
            parameters[7].Value = model.TransFeeSum;
            parameters[8].Value = model.TransCount;
            parameters[9].Value = model.Reason;
            parameters[10].Value = model.Remark;
            parameters[11].Value = model.ModifiedDate;
            parameters[12].Value = model.ModifiedUserID;

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

            #region 调拨明细单
            StringBuilder sbDel = new StringBuilder();
            sbDel.Append("DELETE officedba.SubDeliveryTransDetail WHERE CompanyCD=@CompanyCD AND TransNo=@TransNo");
            SqlParameter[] delParas = { 
                                          new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                          new SqlParameter("@TransNo",SqlDbType.VarChar)
                                      };
            delParas[0].Value = model.CompanyCD;
            delParas[1].Value = model.TransNo;
            SqlCommand sqlDelCmd = new SqlCommand { CommandText = sbDel.ToString() };
            sqlDelCmd.Parameters.AddRange(delParas);
            SqlList.Add(sqlDelCmd);

            foreach (SubDeliveryTransDetail m in modelList)
            {
                SqlList.Add(SaveDetail(m));
            }
            #endregion

            bool result = SqlHelper.ExecuteTransWithArrayList(SqlList);
            return result;

        }
        #endregion

        #region 更新状态
        public static bool UpdateStatus(SubDeliveryTrans model, int stype)
        {
            if (stype == 1)
            {
                #region 确认单据
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update officedba.SubDeliveryTrans set ");
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
                strSql.Append("update officedba.SubDeliveryTrans set ");
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
                    new SqlParameter("@BusiStatus",SqlDbType.VarChar)};
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
                    IList<SqlCommand> tempList = Data.Common.FlowDBHelper.GetCancelConfirmSqlCommond(model.CompanyCD, Convert.ToInt32(ConstUtil.TYPEFLAG_LogisticsDistribution_NO), Convert.ToInt32(ConstUtil.TYPECODE_SubDeliveryTrans_NO), model.ID, model.ModifiedUserID);
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
                strSql.Append("update officedba.SubDeliveryTrans set ");
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

        #region 读取分店调拨单列表
        public static DataTable GetSubDeliveryTransList(Hashtable htPara, int PageIndex, int PageSize, string OrderBy, string EFIndex, string EFDesc, ref int TotalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT * FROM ( ");
            sbSql.Append("SELECT  sds.ID,sds.TransNo,sds.Title,sds.ApplyUserID,sds.OutDeptID,sds.TransPrice,sds.TransCount,sds.TransFeeSum,sds.busiStatus, ");
            sbSql.Append(" (select ei1.EmployeeName from officedba.EmployeeInfo  as ei1  where ei1.ID=sds.ApplyUserID) as ApplyUserIDName,");
            sbSql.Append(" (select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=sds.OutDeptID) as OutDeptIDName,");
            sbSql.Append(" (select di3.DeptName from officedba.DeptInfo as di3 where di3.ID=sds.InDeptID) as InDeptIDName,");
            sbSql.Append(" (select di4.DeptName from officedba.DeptInfo as di4 where di4.ID=sds.TransDeptID) as TansDeptIDName,");
            sbSql.Append("(CASE sds.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,( select top 1 fi.FlowStatus   from  officedba.FlowInstance AS FI  where fi.BillID=sds.ID  and fi.CompanyCD=sds.CompanyCD  AND fi.BillTypeCode=" + XBase.Common.ConstUtil.TYPECODE_SubDeliveryTrans_NO + "  AND fi.BillTypeFlag= " + XBase.Common.ConstUtil.TYPEFLAG_LogisticsDistribution_NO + " order by FI.ModifiedDate DESC) as FlowStatus ");
            sbSql.Append("  FROM officedba.SubDeliveryTrans as sds where CompanyCD=@CompanyCD ");


            string FlowStatusSql = string.Empty;
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter Paras = null;
            if (htPara.ContainsKey("@FlowStatus"))
            {
                int SubmitStatus = Convert.ToInt32(htPara["@FlowStatus"].ToString());
                if (SubmitStatus > 0)
                    FlowStatusSql = "  AND  FlowStatus=" + htPara["@FlowStatus"].ToString();
                else if (SubmitStatus == 0)
                    FlowStatusSql = "  AND FlowStatus is null ";
            }


            #region 构造查询条件
            if (htPara.ContainsKey("@TransNo"))
            {
                sbSql.Append(" AND sds.TransNo LIKE @TransNo");
                Paras = new SqlParameter("@TransNo", SqlDbType.VarChar);
                Paras.Value = htPara["@TransNo"];
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
            if (htPara.ContainsKey("@OutDeptID"))
            {
                sbSql.Append(" AND sds.OutDeptID=@OutDeptID");
                Paras = new SqlParameter("@OutDeptID", SqlDbType.Int);
                Paras.Value = htPara["@OutDeptID"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@BusiStatus"))
            {
                sbSql.Append(" AND sds.BusiStatus=@BusiStatus");
                Paras = new SqlParameter("@BusiStatus", SqlDbType.VarChar);
                Paras.Value = htPara["@BusiStatus"];
                list.Add(Paras);
            }

            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sbSql.Append(" and sds.ExtField" + EFIndex + " LIKE @EFDesc ");
                Paras = new SqlParameter("@EFDesc", SqlDbType.VarChar);
                Paras.Value = "%" + EFDesc + "%";
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@BillStatus"))
            {
                sbSql.Append(" AND sds.BillStatus=@BillStatus");
                Paras = new SqlParameter("@BillStatus", SqlDbType.VarChar);
                Paras.Value = htPara["@BillStatus"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@InDeptID"))
            {
                sbSql.Append(" AND sds.InDeptID=@InDeptID ");
                Paras = new SqlParameter("@InDeptID", SqlDbType.Int);
                Paras.Value = htPara["@InDeptID"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@TransDeptID"))
            {
                sbSql.Append(" AND sds.TransDeptID=@TransDeptID ");
                Paras = new SqlParameter("@TransDeptID", SqlDbType.Int);
                Paras.Value = htPara["@TransDeptID"];
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
        public static DataTable GetSubDeliveryTransList(Hashtable htPara, string OrderBy, string EFIndex, string EFDesc)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT * FROM ( ");
            sbSql.Append("SELECT  sds.ID,sds.TransNo,sds.Title,sds.ApplyUserID,sds.OutDeptID,sds.TransPrice,sds.TransCount,sds.TransFeeSum,sds.busiStatus, ");
            sbSql.Append(" (select ei1.EmployeeName from officedba.EmployeeInfo  as ei1  where ei1.ID=sds.ApplyUserID) as ApplyUserIDName,");
            sbSql.Append(" (select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=sds.OutDeptID) as OutDeptIDName,");
            sbSql.Append(" (select di3.DeptName from officedba.DeptInfo as di3 where di3.ID=sds.InDeptID) as InDeptIDName,");
            sbSql.Append(" (select di4.DeptName from officedba.DeptInfo as di4 where di4.ID=sds.TransDeptID) as TansDeptIDName,");
            sbSql.Append("(CASE sds.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,( select top 1 fi.FlowStatus   from  officedba.FlowInstance AS FI  where fi.BillID=sds.ID  and fi.CompanyCD=sds.CompanyCD  AND fi.BillTypeCode=" + XBase.Common.ConstUtil.TYPECODE_SubDeliveryTrans_NO + "  AND fi.BillTypeFlag= " + XBase.Common.ConstUtil.TYPEFLAG_LogisticsDistribution_NO + " order by FI.ModifiedDate DESC) as FlowStatus ");
            sbSql.Append("  FROM officedba.SubDeliveryTrans as sds where CompanyCD=@CompanyCD ");


            string FlowStatusSql = string.Empty;
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter Paras = null;
            if (htPara.ContainsKey("@FlowStatus"))
            {
                int SubmitStatus = Convert.ToInt32(htPara["@FlowStatus"].ToString());
                if (SubmitStatus > 0)
                    FlowStatusSql = "  AND  FlowStatus=" + htPara["@FlowStatus"].ToString();
                else if (SubmitStatus == 0)
                    FlowStatusSql = "  AND FlowStatus is null ";
            }

            #region 构造查询条件
            if (htPara.ContainsKey("@TransNo"))
            {
                sbSql.Append(" AND sds.TransNo LIKE @TransNo");
                Paras = new SqlParameter("@TransNo", SqlDbType.VarChar);
                Paras.Value = htPara["@TransNo"];
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
            if (htPara.ContainsKey("@OutDeptID"))
            {
                sbSql.Append(" AND sds.OutDeptID=@OutDeptID");
                Paras = new SqlParameter("@OutDeptID", SqlDbType.Int);
                Paras.Value = htPara["@OutDeptID"];
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
            if (htPara.ContainsKey("@InDeptID"))
            {
                sbSql.Append(" AND sds.InDeptID=@InDeptID ");
                Paras = new SqlParameter("@InDeptID", SqlDbType.Int);
                Paras.Value = htPara["@InDeptID"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@TransDeptID"))
            {
                sbSql.Append(" AND sds.TransDeptID=@TransDeptID ");
                Paras = new SqlParameter("@TransDeptID", SqlDbType.Int);
                Paras.Value = htPara["@TransDeptID"];
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
                sbSubSql.Append("DELETE officedba.SubDeliveryTransDetail  where CompanyCD=(Select sds1.CompanyCD FROM officedba.SubDeliveryTrans as sds1 where sds1.ID=@ID) ");
                sbSubSql.Append(" AND TransNo=(SELECT sds2.TransNo from officedba.SubDeliveryTrans as sds2 where sds2.ID=@ID)");
                SqlParameter[] SubParas = { new SqlParameter("@ID", SqlDbType.Int) };
                SubParas[0].Value = IDList[i];

                SqlCommand SqlSubCmd = new SqlCommand();
                SqlSubCmd.CommandText = sbSubSql.ToString();
                SqlSubCmd.Parameters.AddRange(SubParas);
                SqlList.Add(SqlSubCmd);

                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("DELETE officedba.SubDeliveryTrans where ID=@ID");
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

        #region 读取门店调拨单信息
        public static DataTable GetSubDeliveryTransInfo(SubDeliveryTrans model)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT sds.* ,");
            sbSql.Append(" (select ei1.EmployeeName from officedba.EmployeeInfo  as ei1  where ei1.ID=sds.ApplyUserID) as ApplyUserIDName,");
            sbSql.Append("(select  di1.DeptName from officedba.DeptInfo as di1 where di1.ID=sds.OutDeptID ) as OutDeptIDName,");
            sbSql.Append("(select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=sds.InDeptID) as InDeptIDName,");
            sbSql.Append("(select di3.DeptName from officedba.DeptInfo as di3 where di3.ID=sds.TransDeptID ) AS TransDeptIDName ,");
            sbSql.Append("(select ei2.EmployeeName from officedba.EmployeeInfo  as ei2 where ei2.ID=sds.OutUserID) as OutUserIDName,");
            sbSql.Append("(select ei3.EmployeeName from officedba.EmployeeInfo  as ei3 where ei3.ID=sds.InUserID) as InUserIDName,");
            sbSql.Append("(select ei4.EmployeeName from officedba.EmployeeInfo  as ei4 where ei4.ID=sds.Creator) as CreatorName,");
            sbSql.Append("(select ei5.EmployeeName from officedba.EmployeeInfo  as ei5 where ei5.ID=sds.Confirmor) as ConfirmorName,");
            sbSql.Append("(select ei6.EmployeeName from officedba.EmployeeInfo  as ei6 where ei6.ID=sds.Closer) as CloserName ");
            sbSql.Append(" from officedba.SubDeliveryTrans as sds where sds.CompanyCD=@CompanyCD AND sds.ID=@ID");

            SqlParameter[] Paras = { 
                                   new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                   new SqlParameter("@ID",SqlDbType.Int)};
            Paras[0].Value = model.CompanyCD;
            Paras[1].Value = model.ID;

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);

        }

        /*打印使用*/
        public static DataTable GetSubDeliveryTransInfoPrint(SubDeliveryTrans model)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("      ");
            sbSql.AppendLine("    SELECT     ID, CompanyCD, TransNo, Title, ApplyUserID, OutDeptID, InDeptID, TransDeptID, Reason, BusiStatus,    ");
            sbSql.AppendLine("    OutUserID, OutDate, InUserID, InDate, Remark, Creator, convert(varchar(10),CreateDate,120) CreateDate, BillStatus, Confirmor, convert(varchar(10),ConfirmDate,120)ConfirmDate, Closer, convert(varchar(10),CloseDate,120) CloseDate, convert(varchar(10),ModifiedDate,120)ModifiedDate,    ");
            sbSql.AppendLine("ExtField1,ExtField2,ExtField3,ExtField4,ExtField5,ExtField6,ExtField7,ExtField8,ExtField9,ExtField10");
            sbSql.AppendLine("  ,CONVERT(NUMERIC(12,2),ISNULL(sds.TransPrice,0)) TransPrice");
            sbSql.AppendLine("  ,CONVERT(NUMERIC(12,2),ISNULL(sds.TransFeeSum,0)) TransFeeSum");
            sbSql.AppendLine("  ,CONVERT(NUMERIC(12,2),ISNULL(sds.TransCount,0)) TransCount");
            sbSql.AppendLine("  ,ModifiedUserID    ");
            sbSql.AppendLine("  ,(CASE sds.BusiStatus WHEN '1' THEN '调拨申请' WHEN '2' THEN '调拨出库' WHEN '3' THEN '调拨入库' WHEN '4' THEN '调拨完成' END)    ");
            sbSql.AppendLine("    AS BusiStatusText, (CASE sds.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '4' THEN '手工结单' WHEN '5' THEN '自动结单' END)    ");
            sbSql.AppendLine("    AS BillStatusText,  ");
            sbSql.AppendLine("    (SELECT     EmployeeName  ");
            sbSql.AppendLine("    FROM          officedba.EmployeeInfo AS ei1  ");
            sbSql.AppendLine("    WHERE      (ID = sds.ApplyUserID)) AS ApplyUserIDName,  ");
            sbSql.AppendLine("    (SELECT     DeptName  ");
            sbSql.AppendLine("    FROM          officedba.DeptInfo AS di1  ");
            sbSql.AppendLine("   WHERE      (ID = sds.OutDeptID)) AS OutDeptIDName,  ");
            sbSql.AppendLine("   (SELECT     DeptName  ");
            sbSql.AppendLine("   FROM          officedba.DeptInfo AS di2  ");
            sbSql.AppendLine("      WHERE      (ID = sds.InDeptID)) AS InDeptIDName,  ");
            sbSql.AppendLine("     (SELECT     DeptName  ");
            sbSql.AppendLine("      FROM          officedba.DeptInfo AS di3  ");
            sbSql.AppendLine("      WHERE      (ID = sds.TransDeptID)) AS TransDeptIDName,  ");
            sbSql.AppendLine("   (SELECT     EmployeeName  ");
            sbSql.AppendLine("      FROM          officedba.EmployeeInfo AS ei2  ");
            sbSql.AppendLine("      WHERE      (ID = sds.OutUserID)) AS OutUserIDName,  ");
            sbSql.AppendLine("     (SELECT     EmployeeName  ");
            sbSql.AppendLine("     FROM          officedba.EmployeeInfo AS ei3  ");
            sbSql.AppendLine("     WHERE      (ID = sds.InUserID)) AS InUserIDName,  ");
            sbSql.AppendLine("      (SELECT     EmployeeName  ");
            sbSql.AppendLine("      FROM          officedba.EmployeeInfo AS ei4  ");
            sbSql.AppendLine("     WHERE      (ID = sds.Creator)) AS CreatorName,  ");
            sbSql.AppendLine("     (SELECT     EmployeeName  ");
            sbSql.AppendLine("    FROM          officedba.EmployeeInfo AS ei5  ");
            sbSql.AppendLine("     WHERE      (ID = sds.Confirmor)) AS ConfirmorName,  ");
            sbSql.AppendLine("    (SELECT     EmployeeName  ");
            sbSql.AppendLine("     FROM          officedba.EmployeeInfo AS ei6  ");
            sbSql.AppendLine("     WHERE      (ID = sds.Closer)) AS CloserName  ");
            sbSql.AppendLine("    FROM         officedba.SubDeliveryTrans AS sds  ");
            sbSql.AppendLine("  where sds.CompanyCD=@CompanyCD AND sds.ID=@ID");

            SqlParameter[] Paras = { 
                                   new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                   new SqlParameter("@ID",SqlDbType.Int)};
            Paras[0].Value = model.CompanyCD;
            Paras[1].Value = model.ID;

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);

        }
        #endregion

        #region 读取门店掉调拨单明细信息
        public static DataTable GetSubDeliveryTransDetailInfo(SubDeliveryTransDetail model)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT stsd.ID,stsd.CompanyCD,stsd.TransNo,stsd.SortNo,stsd.ProductID,stsd.UsedUnitCount,stsd.UnitID AS UsedUnitID,stsd.BatchNo,stsd.Remark");
            sbSql.Append(" ,ISNULL(stsd.TransCount,0) AS TransCount");
            sbSql.Append(" ,ISNULL(stsd.TransPrice,0) AS TransPrice");
            sbSql.Append(" ,ISNULL(stsd.TransPriceTotal,0) AS TransPriceTotal");
            sbSql.Append(" ,pi.ProdNo,pi.ProductName,pi.Specification,pi.UnitID,pi.MinusIs,pi.IsBatchNo");
            sbSql.Append(" ,(select SUM(ISNULL(ProductCount,0))  from officedba.SubStorageProduct as sp where sp.DeptID=(select s.OutDeptID FROM officedba.SubDeliveryTrans AS S WHERE S.CompanyCD=@CompanyCD AND S.TransNo=@TransNo)  AND sp.ProductID=stsd.ProductID AND sp.CompanyCD=stsd.CompanyCD AND isnull(sp.BatchNo,'')=isnull(stsd.BatchNo,'') ) AS UseCount");
            sbSql.Append(" ,(SELECT ui.CodeName from officedba.CodeUnitType as ui where ui.ID=pi.UnitID) AS UnitName ");
            sbSql.Append(" ,(SELECT ui1.CodeName from officedba.CodeUnitType as ui1 where ui1.ID=stsd.UnitID) AS UsedUnitName ");
            sbSql.Append(" from officedba.SubDeliveryTransDetail as stsd inner join officedba.ProductInfo as pi on stsd.ProductID=pi.ID where stsd.CompanyCD=@CompanyCD AND stsd.TransNo=@TransNo");

            SqlParameter[] Paras = { 
                                   new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                   new SqlParameter("@TransNo",SqlDbType.VarChar)};

            Paras[0].Value = model.CompanyCD;
            Paras[1].Value = model.TransNo;

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
        }
        #endregion

        #region 门店出库
        public static bool RunSubDeliveryTransOut(SubDeliveryTrans model)
        {

            ArrayList SqlList = new ArrayList();
            #region 更新门店调拨单
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SubDeliveryTrans set ");
            strSql.Append("BusiStatus=@BusiStatus,");
            strSql.Append("OutUserID=@OutUserID,");
            strSql.Append("OutDate=@OutDate");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@BusiStatus", SqlDbType.Char,1),
					new SqlParameter("@OutUserID", SqlDbType.Int,4),
					new SqlParameter("@OutDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.BusiStatus;
            parameters[2].Value = model.OutUserID;
            parameters[3].Value = model.OutDate;
            #endregion

            SqlCommand sqlMainCmd = new SqlCommand() { CommandText = strSql.ToString() };
            sqlMainCmd.Parameters.AddRange(parameters);
            SqlList.Add(sqlMainCmd);

            #region 实时读取明细信息
            StringBuilder sbDetailSql = new StringBuilder();
            sbDetailSql.Append(" SELECT ProductID,TransCount,BatchNo,TransPrice FROM officedba.SubDeliveryTransDetail  ");
            sbDetailSql.Append(" WHERE CompanyCD=@CompanyCD AND TransNo=@TransNo");
            SqlParameter[] DetailParas = { 
                                        new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                        new SqlParameter("@TransNo",SqlDbType.VarChar)};
            DetailParas[0].Value = model.CompanyCD;
            DetailParas[1].Value = model.TransNo;
            DataTable dtDetails = SqlHelper.ExecuteSql(sbDetailSql.ToString(), DetailParas);
            #endregion
            int id = 0;
            decimal count = 0m;
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                foreach (DataRow row in dtDetails.Rows)
                {

                    #region 添加门店库存流水帐
                    SubStorageAccountModel aModel = new SubStorageAccountModel();
                    aModel.BatchNo = row["BatchNo"].ToString().Trim();
                    aModel.BillNo = model.TransNo;
                    aModel.BillType = 5;
                    aModel.CompanyCD = model.CompanyCD;
                    aModel.Creator = model.Creator;
                    aModel.DeptID = model.OutDeptID;
                    aModel.HappenDate = DateTime.Now;
                    if (int.TryParse(row["ProductID"].ToString(), out id))
                    {
                        aModel.ProductID = id;
                    }
                    if (decimal.TryParse(row["TransPrice"].ToString(), out count))
                    {
                        aModel.Price = count;
                    }
                    if (decimal.TryParse(row["TransCount"].ToString(), out count))
                    {
                        aModel.HappenCount = -count;
                    }
                    aModel.PageUrl = model.Remark;
                    SqlList.Add(XBase.Data.Office.SubStoreManager.SubStorageAccountDBHelper.GetCountAndInsertCommand(aModel));
                    #endregion

                    SqlList.Add(XBase.Data.Office.LogisticsDistributionManager.StorageProductQueryDBHelper.UpdateProductCount(model.CompanyCD
                        , row["ProductID"].ToString(), model.OutDeptID.ToString(), row["BatchNo"].ToString(), -count));


                }
                return SqlHelper.ExecuteTransWithArrayList(SqlList);
            }
            else
                return false;
        }

        private static SqlParameter[] GetSubPara(object TransCount, object CompanyCD, object ProductID, object DeptID, object ProductCount, object BatchNo)
        {
            SqlParameter[] para = {  
                                             new SqlParameter("@TransCount",SqlDbType.Decimal),
                                             new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                             new SqlParameter("@ProductID",SqlDbType.Int),
                                             new SqlParameter("@DeptID",SqlDbType.Int),
                                             new SqlParameter("@ProductCount",SqlDbType.Decimal),
                                             new SqlParameter("@BatchNo",SqlDbType.VarChar)};
            para[0].Value = TransCount;
            para[1].Value = CompanyCD;
            para[2].Value = ProductID;
            para[3].Value = DeptID;
            para[4].Value = ProductCount;
            para[5].Value = BatchNo;

            return para;
        }
        #endregion

        #region 门店入库
        public static bool RunSubDeliveryTransIn(SubDeliveryTrans model)
        {
            ArrayList SqlList = new ArrayList();

            #region 更新调拨单
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SubDeliveryTrans set ");
            strSql.Append("BusiStatus=@BusiStatus,");
            strSql.Append("InUserID=@InUserID,");
            strSql.Append("InDate=@InDate");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@BusiStatus", SqlDbType.Char,1),
					new SqlParameter("@InUserID", SqlDbType.Int,4),
					new SqlParameter("@InDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.BusiStatus;
            parameters[2].Value = model.InUserID;
            parameters[3].Value = model.InDate;
            #endregion

            SqlCommand sqlMainCmd = new SqlCommand() { CommandText = strSql.ToString() };
            sqlMainCmd.Parameters.AddRange(parameters);
            SqlList.Add(sqlMainCmd);

            #region 实时读取明细信息
            StringBuilder sbDetailSql = new StringBuilder();
            sbDetailSql.Append(" SELECT ProductID,TransCount,BatchNo,TransPrice FROM officedba.SubDeliveryTransDetail  ");
            sbDetailSql.Append(" WHERE CompanyCD=@CompanyCD AND TransNo=@TransNo");
            SqlParameter[] DetailParas = { 
                                        new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                        new SqlParameter("@TransNo",SqlDbType.VarChar)};
            DetailParas[0].Value = model.CompanyCD;
            DetailParas[1].Value = model.TransNo;
            DataTable dtDetails = SqlHelper.ExecuteSql(sbDetailSql.ToString(), DetailParas);
            #endregion

            int id = 0;
            decimal count = 0m;
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                foreach (DataRow row in dtDetails.Rows)
                {
                    #region 添加门店库存流水帐
                    SubStorageAccountModel aModel = new SubStorageAccountModel();
                    aModel.BatchNo = row["BatchNo"].ToString().Trim();
                    aModel.BillNo = model.TransNo;
                    aModel.BillType = 6;
                    aModel.CompanyCD = model.CompanyCD;
                    aModel.Creator = model.Creator;
                    aModel.DeptID = model.InDeptID;
                    aModel.HappenDate = DateTime.Now;
                    if (int.TryParse(row["ProductID"].ToString(), out id))
                    {
                        aModel.ProductID = id;
                    }
                    if (decimal.TryParse(row["TransPrice"].ToString(), out count))
                    {
                        aModel.Price = count;
                    }
                    if (decimal.TryParse(row["TransCount"].ToString(), out count))
                    {
                        aModel.HappenCount = count;
                    }
                    aModel.PageUrl = model.Remark;
                    SqlList.Add(XBase.Data.Office.SubStoreManager.SubStorageAccountDBHelper.GetCountAndInsertCommand(aModel));


                    #endregion

                    SqlList.Add(XBase.Data.Office.LogisticsDistributionManager.StorageProductQueryDBHelper.UpdateProductCount(model.CompanyCD
                        , row["ProductID"].ToString(), model.InDeptID.ToString(), row["BatchNo"].ToString(), count));

                }
                return SqlHelper.ExecuteTransWithArrayList(SqlList);
            }
            else
                return false;

        }
        #endregion

        #region 门店调拨统计报表
        /*分页*/
        public static DataTable GetSubDeliveryTransReport(Hashtable htPara, int PageIndex, int PageSize, string OrderBy, ref int TotalCount, string InOut)
        {
            string sCondition = string.Empty;
            int index = 0;
            SqlParameter[] Paras = new SqlParameter[htPara.Count];
            Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[index++].Value = htPara["CompanyCD"];
            if (!string.IsNullOrEmpty(InOut))
            {
                if (InOut.ToUpper() == "IN")
                {
                    sCondition += " AND t2.InDeptID=@DeptID";
                    Paras[index] = new SqlParameter("@DeptID", SqlDbType.Int);
                    Paras[index++].Value = htPara["DeptID"];
                }
                else if (InOut.ToUpper() == "OUT")
                {
                    sCondition += " AND t2.OutDeptID=@DeptID";
                    Paras[index] = new SqlParameter("@DeptID", SqlDbType.Int);
                    Paras[index++].Value = htPara["DeptID"];
                }
            }
            if (htPara.ContainsKey("StartDate"))
            {
                sCondition += " AND t2.ConfirmDate>= Convert(datetime,@StartDate) ";
                Paras[index] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["StartDate"];
            }
            if (htPara.ContainsKey("EndDate"))
            {
                sCondition += " AND t2.ConfirmDate < DATEADD(day, 1,Convert(datetime,@EndDate)) ";
                Paras[index] = new SqlParameter("@EndDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["EndDate"];
            }

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("   select pi1.ProductName,pi1.Specification as span, ");
            sbSql.AppendLine("   (select cu.CodeName from officedba.CodeUnitType as cu where cu.ID=pi1.UnitID) AS UnitName, ");
            sbSql.AppendLine("  (SELECT di1.DeptName from officedba.DeptInfo as di1 where di1.ID=q1.InDeptID) AS InDept, ");
            sbSql.AppendLine("  (select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=q1.OutDeptID) AS OutDept , ");
            sbSql.AppendLine("  q1.TotalTransCount,q1.TotalTransPrice  ");
            sbSql.AppendLine("  from ");
            sbSql.AppendLine("  ( ");
            sbSql.AppendLine("  SELECT y1.ProductID,SUM(y1.TransCount) as TotalTransCount ,SUM(y1.TransPriceTotal) as TotalTransPrice,y1.OutDeptID,y1.InDeptID  ");
            sbSql.AppendLine("  FROM ");
            sbSql.AppendLine("  ( ");
            sbSql.AppendLine("  select t1.ProductID,t1.TransCount,t1.TransPriceTotal,t2.OutDeptID,t2.InDeptID  ");
            sbSql.AppendLine("  from  ");
            sbSql.AppendLine("  officedba.SubDeliveryTransDetail as t1 ");
            sbSql.AppendLine("  left join officedba.SubDeliveryTrans as t2 ");
            sbSql.AppendLine("  on t1.TransNo=t2.TransNo and t1.CompanyCD=t2.CompanyCD WHERE t1.CompanyCD=@CompanyCD AND Convert(int,t2.BillStatus)>=2 and t2.BusiStatus='4' ");
            sbSql.AppendLine(sCondition);
            sbSql.AppendLine("  ) AS y1  ");
            sbSql.AppendLine("  GROUP BY y1.ProductID,y1.OutDeptID,y1.InDeptID ");
            sbSql.AppendLine("  ) as q1 ");
            sbSql.AppendLine("  left join officedba.ProductInfo as pi1 ");
            sbSql.AppendLine("  on q1.ProductID=pi1.ID ");


            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, Paras, ref TotalCount);
        }

        /*不分页*/
        public static DataTable GetSubDeliveryTransReport(Hashtable htPara, string OrderBy, string InOut)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            string sCondition = string.Empty;
            int index = 0;
            SqlParameter[] Paras = new SqlParameter[htPara.Count];
            Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[index++].Value = htPara["CompanyCD"];
            if (!string.IsNullOrEmpty(InOut))
            {
                if (InOut.ToUpper() == "IN")
                {
                    sCondition += " AND t2.InDeptID=@DeptID";
                    Paras[index] = new SqlParameter("@DeptID", SqlDbType.Int);
                    Paras[index++].Value = htPara["DeptID"];
                }
                else if (InOut.ToUpper() == "OUT")
                {
                    sCondition += " AND t2.OutDeptID=@DeptID";
                    Paras[index] = new SqlParameter("@DeptID", SqlDbType.Int);
                    Paras[index++].Value = htPara["DeptID"];
                }
            }
            if (htPara.ContainsKey("StartDate"))
            {
                sCondition += " AND t2.ConfirmDate>= Convert(datetime,@StartDate) ";
                Paras[index] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["StartDate"];
            }
            if (htPara.ContainsKey("EndDate"))
            {
                sCondition += " AND t2.ConfirmDate < DATEADD(day, 1,Convert(datetime,@EndDate)) ";
                Paras[index] = new SqlParameter("@EndDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["EndDate"];
            }

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("   select pi1.ProductName,pi1.Specification as span, ");
            sbSql.AppendLine("   (select cu.CodeName from officedba.CodeUnitType as cu where cu.ID=pi1.UnitID) AS UnitName, ");
            sbSql.AppendLine("  (SELECT di1.DeptName from officedba.DeptInfo as di1 where di1.ID=q1.InDeptID) AS InDept, ");
            sbSql.AppendLine("  (select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=q1.OutDeptID) AS OutDept , ");
            sbSql.AppendLine("  Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(q1.TotalTransCount,0)))+'&nbsp;' as TotalTransCount,");
            sbSql.AppendLine(" Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(q1.TotalTransPrice,0)))+'&nbsp;' as TotalTransPrice  ");
            sbSql.AppendLine("  from ");
            sbSql.AppendLine("  ( ");
            sbSql.AppendLine("  SELECT y1.ProductID,SUM(y1.TransCount) as TotalTransCount ,SUM(y1.TransPriceTotal) as TotalTransPrice,y1.OutDeptID,y1.InDeptID  ");
            sbSql.AppendLine("  FROM ");
            sbSql.AppendLine("  ( ");
            sbSql.AppendLine("  select t1.ProductID,t1.TransCount,t1.TransPriceTotal,t2.OutDeptID,t2.InDeptID  ");
            sbSql.AppendLine("  from  ");
            sbSql.AppendLine("  officedba.SubDeliveryTransDetail as t1 ");
            sbSql.AppendLine("  left join officedba.SubDeliveryTrans as t2 ");
            sbSql.AppendLine("  on t1.TransNo=t2.TransNo and t1.CompanyCD=t2.CompanyCD WHERE t1.CompanyCD=@CompanyCD AND Convert(int,t2.BillStatus)>=2 and t2.BusiStatus='4' ");
            sbSql.AppendLine(sCondition);
            sbSql.AppendLine("  ) AS y1  ");
            sbSql.AppendLine("  GROUP BY y1.ProductID,y1.OutDeptID,y1.InDeptID ");
            sbSql.AppendLine("  ) as q1 ");
            sbSql.AppendLine("  left join officedba.ProductInfo as pi1 ");
            sbSql.AppendLine("  on q1.ProductID=pi1.ID ");
            sbSql.AppendLine(" Order by " + OrderBy);

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
        }
        #endregion

        #region 门店调拨明细表
        /*分页*/
        public static DataTable GetSubDeliveryTransDetailReport(Hashtable htPara, int PageIndex, int PageSize, string OrderBy, ref int TotalCount)
        {
            string sCondition = string.Empty;
            SqlParameter[] Paras = new SqlParameter[htPara.Count];
            int index = 0;
            Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[index++].Value = htPara["CompanyCD"];
            if (htPara.ContainsKey("OutDeptID"))
            {
                sCondition += " AND t2.OutDeptID=@OutDeptID ";
                Paras[index] = new SqlParameter("@OutDeptID", SqlDbType.VarChar);
                Paras[index++].Value = htPara["OutDeptID"];
            }
            if (htPara.ContainsKey("InDeptID"))
            {
                sCondition += " AND t2.InDeptID=@InDeptID ";
                Paras[index] = new SqlParameter("@InDeptID", SqlDbType.Int);
                Paras[index++].Value = htPara["InDeptID"];
            }
            if (htPara.ContainsKey("StartDate"))
            {
                sCondition += " AND t2.ConfirmDate>= Convert(datetime,@StartDate) ";
                Paras[index] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["StartDate"];
            }
            if (htPara.ContainsKey("EndDate"))
            {
                sCondition += " AND t2.ConfirmDate < DATEADD(day, 1,Convert(datetime,@EndDate)) ";
                Paras[index] = new SqlParameter("@EndDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["EndDate"];
            }

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT  ");
            sbSql.AppendLine(" (select di1.DeptName from officedba.DeptInfo as di1 where di1.ID=q1.OutDeptID) AS OutDept, ");
            sbSql.AppendLine(" (select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=q1.InDeptID) AS InDept, ");
            sbSql.AppendLine(" q1.ConfirmDate, ");
            sbSql.AppendLine(" (select ei.EmployeeName from  officedba.EmployeeInfo as ei where ei.ID=q1.ApplyUserID) as ApplyUser, ");
            sbSql.AppendLine(" pi1.ProductName,pi1.Specification as span, ");
            sbSql.AppendLine(" (select cu.CodeName from officedba.CodeUnitType as cu where cu.ID=pi1.UnitID) AS UnitName, ");
            sbSql.AppendLine(" q1.TotalTransCount,q1.TotalTransPrice ");
            sbSql.AppendLine(" FROM ( ");
            sbSql.AppendLine(" SELECT y1.OutDeptID,y1.InDeptID,y1.ConfirmDate,y1.ApplyUserID,y1.ProductID, ");
            sbSql.AppendLine(" SUM(y1.TransCount) AS TotalTransCount,sum(y1.TransPriceTotal) as TotalTransPrice   ");
            sbSql.AppendLine(" FROM ( ");
            sbSql.AppendLine(" SELECT t1.TransPriceTotal,t1.TransCount,t2.ConfirmDate,t2.OutDeptID,t2.InDeptID,t2.ApplyUserID,t1.ProductID ");
            sbSql.AppendLine(" FROM officedba.SubDeliveryTransDetail AS t1  ");
            sbSql.AppendLine(" left join officedba.SubDeliveryTrans as t2 ");
            sbSql.AppendLine(" on t1.TransNo=t2.TransNo and t1.CompanyCD=t2.CompanyCD where t1.CompanyCD=@CompanyCD AND Convert(int,t2.BillStatus)>=2 AND t2.BusiStatus='4' ");
            /*追加参数*/
            sbSql.AppendLine(sCondition);
            sbSql.AppendLine(" ) AS y1 ");
            sbSql.AppendLine(" GROUP BY y1.OutDeptID,y1.InDeptID,y1.ConfirmDate,y1.ApplyUserID,y1.ProductID ");
            sbSql.AppendLine(" ) AS q1 ");
            sbSql.AppendLine(" left join officedba.ProductInfo as pi1  ");
            sbSql.AppendLine(" on q1.ProductID=pi1.ID  ");


            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, Paras, ref TotalCount);

        }

        /*不分页*/
        public static DataTable GetSubDeliveryTransDetailReport(Hashtable htPara, string OrderBy)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            string sCondition = string.Empty;
            SqlParameter[] Paras = new SqlParameter[htPara.Count];
            int index = 0;
            Paras[index] = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
            Paras[index++].Value = htPara["CompanyCD"];
            if (htPara.ContainsKey("OutDeptID"))
            {
                sCondition += " AND t2.OutDeptID=@OutDeptID ";
                Paras[index] = new SqlParameter("@OutDeptID", SqlDbType.VarChar);
                Paras[index++].Value = htPara["OutDeptID"];
            }
            if (htPara.ContainsKey("InDeptID"))
            {
                sCondition += " AND t2.InDeptID=@InDeptID ";
                Paras[index] = new SqlParameter("@InDeptID", SqlDbType.Int);
                Paras[index++].Value = htPara["InDeptID"];
            }
            if (htPara.ContainsKey("StartDate"))
            {
                sCondition += " AND t2.ConfirmDate>= Convert(datetime,@StartDate) ";
                Paras[index] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["StartDate"];
            }
            if (htPara.ContainsKey("EndDate"))
            {
                sCondition += " AND t2.ConfirmDate < DATEADD(day, 1,Convert(datetime,@EndDate)) ";
                Paras[index] = new SqlParameter("@EndDate", SqlDbType.DateTime);
                Paras[index++].Value = htPara["EndDate"];
            }

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT  ");
            sbSql.AppendLine(" (select di1.DeptName from officedba.DeptInfo as di1 where di1.ID=q1.OutDeptID) AS OutDept, ");
            sbSql.AppendLine(" (select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=q1.InDeptID) AS InDept, ");
            sbSql.AppendLine(" q1.ConfirmDate, ");
            sbSql.AppendLine(" (select ei.EmployeeName from  officedba.EmployeeInfo as ei where ei.ID=q1.ApplyUserID) as ApplyUser, ");
            sbSql.AppendLine(" pi1.ProductName,pi1.Specification as span, ");
            sbSql.AppendLine(" (select cu.CodeName from officedba.CodeUnitType as cu where cu.ID=pi1.UnitID) AS UnitName, ");
            sbSql.AppendLine(" Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(q1.TotalTransCount,0)))+'&nbsp;' as TotalTransCount,");
            sbSql.AppendLine(" Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(q1.TotalTransPrice,0)))+'&nbsp;' as TotalTransPrice ");
            sbSql.AppendLine(" FROM ( ");
            sbSql.AppendLine(" SELECT y1.OutDeptID,y1.InDeptID,y1.ConfirmDate,y1.ApplyUserID,y1.ProductID, ");
            sbSql.AppendLine(" SUM(y1.TransCount) AS TotalTransCount,sum(y1.TransPriceTotal) as TotalTransPrice   ");
            sbSql.AppendLine(" FROM ( ");
            sbSql.AppendLine(" SELECT t1.TransPriceTotal,t1.TransCount,t2.ConfirmDate,t2.OutDeptID,t2.InDeptID,t2.ApplyUserID,t1.ProductID ");
            sbSql.AppendLine(" FROM officedba.SubDeliveryTransDetail AS t1  ");
            sbSql.AppendLine(" left join officedba.SubDeliveryTrans as t2 ");
            sbSql.AppendLine(" on t1.TransNo=t2.TransNo and t1.CompanyCD=t2.CompanyCD where t1.CompanyCD=@CompanyCD AND Convert(int,t2.BillStatus)>=2 AND t2.BusiStatus='4' ");
            /*追加参数*/
            sbSql.AppendLine(sCondition);
            sbSql.AppendLine(" ) AS y1 ");
            sbSql.AppendLine(" GROUP BY y1.OutDeptID,y1.InDeptID,y1.ConfirmDate,y1.ApplyUserID,y1.ProductID ");
            sbSql.AppendLine(" ) AS q1 ");
            sbSql.AppendLine(" left join officedba.ProductInfo as pi1  ");
            sbSql.AppendLine(" on q1.ProductID=pi1.ID  ");
            sbSql.AppendLine(" ORDER BY " + OrderBy);

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);

        }
        #endregion

    }
}

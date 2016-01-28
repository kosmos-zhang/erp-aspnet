using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using System.Data.Sql;
using XBase.Model.Office.SellManager;
using XBase.Common;
using XBase.Data.Common;

namespace XBase.Data.Office.SellManager
{
    public class SellChannelSttlDBHelper
    {
        #region 添加、修改、删除相关操作
        /// <summary>
        /// 保存销售委托代销单
        /// </summary>
        /// <returns></returns>
        public static bool SaveOrder(Hashtable ht, SellChannelSttlModel sellChannelSttlModel, List<SellChannelSttlDetailModel> sellChannelSttlDetailModellist, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            //判断单据编号是否存在
            if (NoIsExist(sellChannelSttlModel.SttlNo))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    InsertOrderInfo(ht,sellChannelSttlModel, tran);
                    InsertOrderDetail(sellChannelSttlDetailModellist, tran);
                    tran.Commit();
                    isSucc = true;
                    strMsg = "保存成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "保存失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                isSucc = false;
                strMsg = "该编号已被使用，请输入未使用的编号！";
            }
            return isSucc;
        }

        /// <summary>
        /// 更新销售委托代销单
        /// </summary>
        /// <returns></returns>
        public static bool UpdateOrder(Hashtable ht, SellChannelSttlModel sellChannelSttlModel, List<SellChannelSttlDetailModel> sellChannelSttlDetailModellist, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            if (IsUpdate(sellChannelSttlModel.SttlNo))
            {
                string strSql = "delete from officedba.SellChannelSttlDetail where  SttlNo=@SttlNo  and CompanyCD=@CompanyCD";
                SqlParameter[] paras = { new SqlParameter("@SttlNo", sellChannelSttlModel.SttlNo), new SqlParameter("@CompanyCD", sellChannelSttlModel.CompanyCD) };
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {

                    UpdateOrderInfo(ht,sellChannelSttlModel, tran);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);
                    InsertOrderDetail(sellChannelSttlDetailModellist, tran);
                    tran.Commit();
                    isSucc = true;
                    strMsg = "保存成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "保存失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                isSucc = false;
                strMsg = "非制单状态的未提交审批、审批未通过或撤销审批单据不可修改！";
            }
            return isSucc;
        }

        #region 更新主表数据
        /// <summary>
        /// 跟新主表数据
        /// </summary>
        /// <param name="sellChannelSttlModel"></param>
        /// <param name="tran"></param>
        private static void UpdateOrderInfo(Hashtable htExtAttr, SellChannelSttlModel sellChannelSttlModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SellChannelSttl set ");
            strSql.Append("Title=@Title,");
            strSql.Append("CustID=@CustID,");
            strSql.Append("FromType=@FromType,");
            strSql.Append("PayType=@PayType,");
            strSql.Append("MoneyType=@MoneyType,");
            strSql.Append("Seller=@Seller,");
            strSql.Append("SellDeptId=@SellDeptId,");
            strSql.Append("CurrencyType=@CurrencyType,");
            strSql.Append("Rate=@Rate,");
            strSql.Append("SttlDate=@SttlDate,");
            strSql.Append("CountTotal=@CountTotal,");
            strSql.Append("TotalFee=@TotalFee,");
            strSql.Append("PushMoneyPercent=@PushMoneyPercent,");
            strSql.Append("PushMoney=@PushMoney,");
            strSql.Append("HandFeeTotal=@HandFeeTotal,");
            strSql.Append("SttlTotal=@SttlTotal,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("BillStatus=@BillStatus,");
            strSql.Append("Confirmor=@Confirmor,");
            strSql.Append("ConfirmDate=@ConfirmDate,");
            strSql.Append("Closer=@Closer,");
            strSql.Append("CloseDate=@CloseDate,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID,");
            strSql.Append("FromBillID=@FromBillID");
            strSql.Append(" where CompanyCD=@CompanyCD and SttlNo=@SttlNo ");

            SqlParameter[] param = null;
            ArrayList lcmd = new ArrayList();
            #region 参数
            lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellChannelSttlModel.CompanyCD));
            lcmd.Add(SqlHelper.GetParameterFromString("@SttlNo", sellChannelSttlModel.SttlNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@Title", sellChannelSttlModel.Title));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustID", sellChannelSttlModel.CustID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromType", sellChannelSttlModel.FromType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@PayType", sellChannelSttlModel.PayType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Seller", sellChannelSttlModel.Seller.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellDeptId", sellChannelSttlModel.SellDeptId.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CurrencyType", sellChannelSttlModel.CurrencyType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Rate", sellChannelSttlModel.Rate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SttlDate", sellChannelSttlModel.SttlDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CountTotal", sellChannelSttlModel.CountTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalFee", sellChannelSttlModel.TotalFee.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@PushMoneyPercent", sellChannelSttlModel.PushMoneyPercent.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@PushMoney", sellChannelSttlModel.PushMoney.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@HandFeeTotal", sellChannelSttlModel.HandFeeTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SttlTotal", sellChannelSttlModel.SttlTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Remark", sellChannelSttlModel.Remark));
            lcmd.Add(SqlHelper.GetParameterFromString("@BillStatus", sellChannelSttlModel.BillStatus));
            lcmd.Add(SqlHelper.GetParameterFromString("@Confirmor", sellChannelSttlModel.Confirmor.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ConfirmDate", sellChannelSttlModel.ConfirmDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Closer", sellChannelSttlModel.Closer.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CloseDate", sellChannelSttlModel.CloseDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", sellChannelSttlModel.ModifiedUserID));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromBillID", sellChannelSttlModel.FromBillID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@MoneyType", sellChannelSttlModel.MoneyType.ToString()));
            #endregion

            #region 拓展属性

            if (htExtAttr != null && htExtAttr.Count != 0)
            {
                strSql.Append(" ;UPDATE officedba.SellChannelSttl set ");
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql.Append(de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",");
                    lcmd.Add(SqlHelper.GetParameterFromString("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim()));
                }
                strSql.Append(" BillStatus=@BillStatus  where CompanyCD=@CompanyCD and SttlNo=@SttlNo ");
            }
            if (lcmd != null && lcmd.Count > 0)
            {
                param = new SqlParameter[lcmd.Count];
                for (int i = 0; i < lcmd.Count; i++)
                {
                    param[i] = (SqlParameter)lcmd[i];
                }
            }
            #endregion
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

        #region 获取当前单据的id
        /// <summary>
        /// 获取当前单据的id
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static int GetOrderID(string orderNo)
        {
            int OrderID = 0;
            string sql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号

           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            

            sql = " select ID from officedba.SellChannelSttl where CompanyCD=@CompanyCD and SttlNo=@SttlNo ";
            SqlParameter[] paras = { new SqlParameter("@CompanyCD", strCompanyCD), new SqlParameter("@SttlNo", orderNo) };
            try
            {
                OrderID = (int)SqlHelper.ExecuteScalar(sql, paras);
            }
            catch
            {
            }
            return OrderID;
        }
        #endregion

        #region 为主表插入数据
        /// <summary>
        /// 为主表插入数据
        /// </summary>
        /// <param name="sellChannelSttlModel"></param>
        /// <param name="tran"></param>
        private static void InsertOrderInfo(Hashtable htExtAttr, SellChannelSttlModel sellChannelSttlModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SellChannelSttl(");
            strSql.Append("CompanyCD,SttlNo,Title,CustID,FromType,PayType,Seller,SellDeptId,CurrencyType,Rate,SttlDate,CountTotal,TotalFee,PushMoneyPercent,PushMoney,HandFeeTotal,SttlTotal,Remark,BillStatus,Creator,CreateDate,ModifiedDate,ModifiedUserID,FromBillID,MoneyType)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@SttlNo,@Title,@CustID,@FromType,@PayType,@Seller,@SellDeptId,@CurrencyType,@Rate,@SttlDate,@CountTotal,@TotalFee,@PushMoneyPercent,@PushMoney,@HandFeeTotal,@SttlTotal,@Remark,@BillStatus,@Creator,getdate(),getdate(),@ModifiedUserID,@FromBillID,@MoneyType)");

            SqlParameter[] param = null;
            ArrayList lcmd = new ArrayList();
            #region 参数
            lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellChannelSttlModel.CompanyCD));
            lcmd.Add(SqlHelper.GetParameterFromString("@SttlNo", sellChannelSttlModel.SttlNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@Title", sellChannelSttlModel.Title));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustID", sellChannelSttlModel.CustID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromType", sellChannelSttlModel.FromType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@PayType", sellChannelSttlModel.PayType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Seller", sellChannelSttlModel.Seller.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellDeptId", sellChannelSttlModel.SellDeptId.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CurrencyType", sellChannelSttlModel.CurrencyType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Rate", sellChannelSttlModel.Rate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SttlDate", sellChannelSttlModel.SttlDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CountTotal", sellChannelSttlModel.CountTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalFee", sellChannelSttlModel.TotalFee.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@PushMoneyPercent", sellChannelSttlModel.PushMoneyPercent.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@PushMoney", sellChannelSttlModel.PushMoney.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@HandFeeTotal", sellChannelSttlModel.HandFeeTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SttlTotal", sellChannelSttlModel.SttlTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Remark", sellChannelSttlModel.Remark));
            lcmd.Add(SqlHelper.GetParameterFromString("@BillStatus", sellChannelSttlModel.BillStatus));
            lcmd.Add(SqlHelper.GetParameterFromString("@Creator", sellChannelSttlModel.Creator.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", sellChannelSttlModel.ModifiedUserID));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromBillID", sellChannelSttlModel.FromBillID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@MoneyType", sellChannelSttlModel.MoneyType.ToString()));
            #endregion

            #region 拓展属性

            if (htExtAttr != null && htExtAttr.Count != 0)
            {
                strSql.Append(" ;UPDATE officedba.SellChannelSttl set ");
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql.Append(de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",");
                    lcmd.Add(SqlHelper.GetParameterFromString("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim()));
                }
                strSql.Append(" BillStatus=@BillStatus  where CompanyCD=@CompanyCD and SttlNo=@SttlNo ");
            }
            if (lcmd != null && lcmd.Count > 0)
            {
                param = new SqlParameter[lcmd.Count];
                for (int i = 0; i < lcmd.Count; i++)
                {
                    param[i] = (SqlParameter)lcmd[i];
                }
            }
            #endregion
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
        #region 为明细表插入数据
        /// <summary>
        /// 为明细表插入数据
        /// </summary>
        /// <param name="sellChannelSttlDetailModellist"></param>
        /// <param name="tran"></param>
        private static void InsertOrderDetail(List<SellChannelSttlDetailModel> sellChannelSttlDetailModellist, TransactionManager tran)
        {
            foreach (SellChannelSttlDetailModel sellChannelSttlDetailModel in sellChannelSttlDetailModellist)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into officedba.SellChannelSttlDetail(");
                strSql.Append("CompanyCD,SttlNo,SortNo,ProductID,UnitID,ProductCount,SttlNumber,UnitPrice,totalPrice,Remark,FromType,FromBillID,FromLineNo,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate)");
                strSql.Append(" values (");
                strSql.Append("@CompanyCD,@SttlNo,@SortNo,@ProductID,@UnitID,@ProductCount,@SttlNumber,@UnitPrice,@totalPrice,@Remark,@FromType,@FromBillID,@FromLineNo,@ModifiedDate,@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate)");
                #region 参数
                SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@SttlNo", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@UnitID", SqlDbType.Int,4),
					new SqlParameter("@ProductCount", SqlDbType.Decimal,9),
					new SqlParameter("@SttlNumber", SqlDbType.Decimal,9),
					new SqlParameter("@UnitPrice", SqlDbType.Decimal,9),
					new SqlParameter("@totalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,200),
					new SqlParameter("@FromType", SqlDbType.Char,1),
					new SqlParameter("@FromBillID", SqlDbType.Int,4),
					new SqlParameter("@FromLineNo", SqlDbType.Int,4),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20),
					new SqlParameter("@UsedUnitID", SqlDbType.Int,4),
					new SqlParameter("@UsedUnitCount", SqlDbType.Decimal,9),
					new SqlParameter("@UsedPrice", SqlDbType.Decimal,9),
					new SqlParameter("@ExRate", SqlDbType.Decimal,9)};
                parameters[0].Value = sellChannelSttlDetailModel.CompanyCD;
                parameters[1].Value = sellChannelSttlDetailModel.SttlNo;
                parameters[2].Value = sellChannelSttlDetailModel.SortNo;
                parameters[3].Value = sellChannelSttlDetailModel.ProductID;
                parameters[4].Value = sellChannelSttlDetailModel.UnitID;
                parameters[5].Value = sellChannelSttlDetailModel.ProductCount;
                parameters[6].Value = sellChannelSttlDetailModel.SttlNumber;
                parameters[7].Value = sellChannelSttlDetailModel.UnitPrice;
                parameters[8].Value = sellChannelSttlDetailModel.totalPrice;
                parameters[9].Value = sellChannelSttlDetailModel.Remark;
                parameters[10].Value = sellChannelSttlDetailModel.FromType;
                parameters[11].Value = sellChannelSttlDetailModel.FromBillID;
                parameters[12].Value = sellChannelSttlDetailModel.FromLineNo;
                parameters[13].Value = sellChannelSttlDetailModel.ModifiedDate;
                parameters[14].Value = sellChannelSttlDetailModel.ModifiedUserID;
                parameters[15].Value = sellChannelSttlDetailModel.UsedUnitID;
                parameters[16].Value = sellChannelSttlDetailModel.UsedUnitCount;
                parameters[17].Value = sellChannelSttlDetailModel.UsedPrice;
                parameters[18].Value = sellChannelSttlDetailModel.ExRate;
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

        #region 删除
        /// <summary>
        /// 删除销售发货单
        /// </summary>
        /// <param name="orderNos"></param>
        /// <returns></returns>
        public static bool DelOrder(string orderNos, out string strMsg, out string strFieldText)
        {
            string strCompanyCD = string.Empty;//单位编号
            bool isSucc = false;
            string allOrderNo = "";
            strMsg = "";
            strFieldText = "";
            bool bTemp = false;//单据是否可以被删除
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            TransactionManager tran = new TransactionManager();
          
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string[] orderNoS = null;
            orderNoS = orderNos.Split(',');

            for (int i = 0; i < orderNoS.Length; i++)
            {
                if (!IsFlow(orderNoS[i]))
                {
                    strFieldText += "单据：" + orderNoS[i] + "|";
                    strMsg += "已提交审批或已确认后的单据不允许删除！|";
                    bTemp = true;
                }
                else if (!isHandle(orderNoS[i], "1"))
                {
                    strFieldText += "单据：" + orderNoS[i] + "|";
                    strMsg += "已提交审批或已确认后的单据不允许删除！|";
                    bTemp = true;
                }
                orderNoS[i] = "'" + orderNoS[i] + "'";
                sb.Append(orderNoS[i]);
            }

            allOrderNo = sb.ToString().Replace("''", "','");
            if (!bTemp)
            {
                tran.BeginTransaction();
                try
                {
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellChannelSttl WHERE SttlNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellChannelSttlDetail WHERE SttlNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);

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
            }
            else
            {
                isSucc = false;
            }
            return isSucc;
        }
        #endregion
        #endregion

        #region 获取发货单信息相关操作

        #region  获取发货单列表
        /// <summary>
        /// 获取发货单列表 
        /// </summary>
        /// <param name="sellContractModel">sellContractModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(SellChannelSttlModel sellChannelSttlModel, DateTime? dt, int? FlowStatus,string EFIndex,string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            strSql = "select ID,SttlNo,Title,SellerName,CustName,SttlDate,SttlTotal,OrderNo,BillStatusText, ";
            strSql += "FlowInstanceText,FromTypeText,RefText,ModifiedDate from (SELECT s.SttlNo, s.ID, s.ModifiedDate,s.Title, ISNULL(so.SendNo, '') AS OrderNo, ";
            strSql += "ISNULL(e1.EmployeeName, '') AS SellerName, ISNULL(c.CustName, '') ";
            strSql += "AS CustName, CASE s.FromType  WHEN 1 THEN '销售发货单' END AS FromTypeText,s.SttlTotal ,CONVERT(varchar(100), s.SttlDate , 23) AS SttlDate, ";
            strSql += "CASE s.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更' ";
            strSql += "WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText, ";
            strSql += "CASE WHEN (select top 1 FlowStatus from officedba.FlowInstance ";
            strSql += " where BillID=s.ID  AND BillTypeFlag = 5 AND BillTypeCode = 6 order by ModifiedDate desc )IS NULL THEN '' ";
            strSql += "WHEN (select top 1 FlowStatus from officedba.FlowInstance ";
            strSql += "where BillID=s.ID  AND BillTypeFlag = 5 AND BillTypeCode = 6 order by ModifiedDate desc )=1 THEN ";
            strSql += "'待审批' WHEN (select top 1 FlowStatus from officedba.FlowInstance ";
            strSql += "where BillID=s.ID  AND BillTypeFlag = 5 AND BillTypeCode = 6 order by ModifiedDate desc ) = 2 THEN '审批中' ";
            strSql += " WHEN (select top 1 FlowStatus from officedba.FlowInstance ";
            strSql += " where BillID=s.ID  AND BillTypeFlag = 5 AND BillTypeCode = 6 order by ModifiedDate desc ) = 3 THEN '审批通过' ";
            strSql += "WHEN (select top 1 FlowStatus from officedba.FlowInstance ";
            strSql += "where BillID=s.ID  AND BillTypeFlag = 5 AND BillTypeCode = 6 order by ModifiedDate desc )=4 THEN '审批不通过' ";
            strSql += " WHEN (select top 1 FlowStatus from officedba.FlowInstance ";
            strSql += " where BillID=s.ID  AND BillTypeFlag = 5 AND BillTypeCode = 6 order by ModifiedDate desc ) = 5 THEN ";
            strSql += "'撤销审批' END AS FlowInstanceText,(select top 1 FlowStatus from officedba.FlowInstance ";
            strSql += " where BillID=s.ID  AND BillTypeFlag = 5 AND BillTypeCode = 6 order by ModifiedDate desc )as FlowStatus, ";
            strSql += "isnull(CASE((SELECT count(1) FROM officedba.BlendingDetails                         ";
            strSql += "WHERE BillingType = '4' AND BillCD = s.SttlNo and CompanyCD=s.CompanyCD)    ";
            strSql += ") WHEN 0 THEN '无引用' END, '被引用') AS RefText                            ";
            strSql += "FROM officedba.SellChannelSttl AS s LEFT OUTER JOIN ";
            strSql += "officedba.SellSend AS so ON s.FromBillID = so.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e1 ON s.Seller = e1.ID LEFT OUTER JOIN ";
            strSql += "officedba.CustInfo AS c ON s.CustID = c.ID where s.CompanyCD=@CompanyCD  ";

            string strCompanyCD = string.Empty;//单位编号
             strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            //扩展属性
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                strSql += " and s.ExtField" + EFIndex + " like @EFDesc ";
                arr.Add(new SqlParameter("@EFDesc", "%" + EFDesc + "%"));
            }

            if (sellChannelSttlModel.BillStatus != null)
            {
                strSql += " and s.BillStatus= @BillStatus";
                arr.Add(new SqlParameter("@BillStatus", sellChannelSttlModel.BillStatus));
            }
            if (sellChannelSttlModel.FromBillID != null)
            {
                strSql += " and s.FromBillID=@FromBillID";
                arr.Add(new SqlParameter("@FromBillID", sellChannelSttlModel.FromBillID)); ;
            }
            if (sellChannelSttlModel.CustID != null)
            {
                strSql += " and s.CustID=@CustID";
                arr.Add(new SqlParameter("@CustID", sellChannelSttlModel.CustID));
            }
            if (sellChannelSttlModel.Seller != null)
            {
                strSql += " and s.Seller=@Seller";
                arr.Add(new SqlParameter("@Seller", sellChannelSttlModel.Seller));
            }
            if (sellChannelSttlModel.SttlNo != null)
            {
                strSql += " and s.SttlNo like @SttlNo";
                arr.Add(new SqlParameter("@SttlNo", "%" + sellChannelSttlModel.SttlNo + "%"));
            }

            if (sellChannelSttlModel.SttlDate != null)
            {
                strSql += " and s.SttlDate >= @SttlDate";
                arr.Add(new SqlParameter("@SttlDate", sellChannelSttlModel.SttlDate));
            }
            if (dt != null)
            {
                strSql += " and s.SttlDate <= @SttlDate1";
                arr.Add(new SqlParameter("@SttlDate1", dt));
            }
            if (sellChannelSttlModel.Title != null)
            {
                strSql += " and s.Title like @Title";
                arr.Add(new SqlParameter("@Title", "%" + sellChannelSttlModel.Title + "%"));
            }

            strSql += " ) as f  where 1=1 ";
            if (FlowStatus != null)
            {
                if (FlowStatus != 0)
                {
                    strSql += " and f.FlowStatus=@FlowStatus";
                    arr.Add(new SqlParameter("@FlowStatus", FlowStatus));
                }
                else
                {
                    strSql += " and f.FlowStatus is null ";
                }

            }

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }
        #endregion
        #region 获取单据明细信息
        /// <summary>
        /// 获取单据明细信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable GetOrderDetail(string orderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = "SELECT s.ProductID, s.UnitID,ssd.SendNo, s.ProductCount, s.SttlNumber, s.UnitPrice, s.totalPrice, ";
            strSql += "s.Remark, s.FromType, s.FromBillID, s.FromLineNo, p.ProdNo, p.ProductName, p.Specification,isnull(p.StandardCost,0) as StandardCost, ";
            strSql += "ct.CodeName,isnull((SELECT SttlCount FROM officedba.SellSendDetail WHERE (SendNo = ";
            strSql += "(SELECT SendNo FROM officedba.SellSend WHERE (ID = s.FromBillID))) AND ";
            strSql += "(CompanyCD = s.CompanyCD) AND (SortNo = s.FromLineNo)),0) AS SttlCount ";
            strSql += ",s.UsedUnitID,isnull(s.UsedUnitCount,0) as UsedUnitCount,isnull(s.UsedPrice,0) as UsedPrice,isnull(s.ExRate,1) as ExRate  ";//单位ID，数量，单价，换算率
            strSql += "FROM officedba.SellChannelSttlDetail AS s left JOIN ";
            strSql += "officedba.ProductInfo AS p ON s.ProductID = p.ID left JOIN ";
            strSql += "officedba.CodeUnitType AS ct ON s.UnitID = ct.ID left join ";
            strSql += "officedba.SellSend AS ssd ON s.FromBillID = ssd.ID  ";
            strSql += "WHERE (s.SttlNo = @SttlNo) AND (s.CompanyCD = @CompanyCD) ";
            strSql += "ORDER BY s.SortNo ";

            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@SttlNo", orderNo);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }
        #endregion
        #region 获取委托代销主表信息
        /// <summary>
        /// 获取委托代销主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = "SELECT sc.SttlNo, sc.Title, sc.CustID,sc.MoneyType, sc.PayType, sc.Seller, sc.SellDeptId, sc.CurrencyType,                       ";
            strSql += "sc.Rate, CONVERT(varchar(100), sc.SttlDate, 23) AS SttlDate, sc.CountTotal, sc.TotalFee,                            ";
            strSql += "sc.PushMoneyPercent, sc.PushMoney, sc.HandFeeTotal, sc.SttlTotal, sc.Remark, sc.BillStatus,                         ";
            strSql += "sc.ExtField1,sc.ExtField2,sc.ExtField3,sc.ExtField4,sc.ExtField5,";
            strSql += "sc.ExtField6,sc.ExtField7,sc.ExtField8,sc.ExtField9,sc.ExtField10, ";
            strSql += "CONVERT(varchar(100), sc.ConfirmDate, 23) AS ConfirmDate, sc.Confirmor, sc.Closer,                                  ";
            strSql += "CONVERT(varchar(100), sc.CloseDate, 23) AS CloseDate, sc.Creator,                                                   ";
            strSql += "CONVERT(varchar(100), sc.CreateDate, 23) AS CreateDate, CONVERT(varchar(100), sc.ModifiedDate, 23)                  ";
            strSql += "AS ModifiedDate, sc.ModifiedUserID, sc.FromBillID, s.SendNo, e1.EmployeeName AS SellerName,                         ";
            strSql += "e2.EmployeeName AS CreatorName, e3.EmployeeName AS ConfirmorName, c.CustName, ct.CurrencyName, d.DeptName,          ";
            strSql += "CASE sc.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更'                                         ";
            strSql += "WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText,e4.EmployeeName AS CloserName, c.Tel,           ";
            strSql += "CASE sc.isOpenbill WHEN '0' THEN '未建单' WHEN '1' THEN '已建单' END AS isOpenbillText  ,                           ";
            strSql += "ISNULL((SELECT ISNULL(SUM(YAccounts), 0) AS Expr1                                                                   ";
            strSql += "FROM officedba.BlendingDetails                                                                                              ";
            strSql += "WHERE (BillingType = '4') AND (BillCD = sc.SttlNo) AND (CompanyCD = sc.CompanyCD)), 0) AS YAccounts ,               ";
            strSql += " case when (isnull((SELECT isnull(sum(YAccounts),0) FROM officedba.BlendingDetails                                          ";
            strSql += "WHERE BillingType = '4' AND BillCD = sc.SttlNo and CompanyCD=sc.CompanyCD),0))>0                                    ";
            strSql += "and (isnull((SELECT isnull(sum(YAccounts),0) FROM officedba.BlendingDetails                                                 ";
            strSql += "WHERE BillingType = '4' AND BillCD = sc.SttlNo and CompanyCD=sc.CompanyCD),0)) <                                    ";
            strSql += "(isnull((SELECT isnull(max(TotalPrice),0) FROM officedba.BlendingDetails                                                    ";
            strSql += "WHERE BillingType = '4' AND BillCD = sc.SttlNo and CompanyCD=sc.CompanyCD),0)) then '部分结算'                      ";
            strSql += "when (isnull( (SELECT isnull(sum(YAccounts),0) FROM officedba.BlendingDetails                                               ";
            strSql += "WHERE BillingType = '4' AND BillCD = sc.SttlNo and CompanyCD=sc.CompanyCD),0)) =                                    ";
            strSql += "(isnull((SELECT isnull(max(TotalPrice),0) FROM officedba.BlendingDetails                                                    ";
            strSql += "WHERE BillingType = '4' AND BillCD = sc.SttlNo and CompanyCD=sc.CompanyCD),0)) and                                  ";
            strSql += "(isnull((SELECT isnull(sum(YAccounts),0) FROM officedba.BlendingDetails                                                     ";
            strSql += "WHERE BillingType = '4' AND BillCD = sc.SttlNo and CompanyCD=sc.CompanyCD),0))<>0 then '已结算'                     ";
            strSql += "when (isnull((SELECT isnull(sum(YAccounts),0)                                                                       ";
            strSql += "FROM officedba.BlendingDetails                                                                                              ";
            strSql += "WHERE BillingType = '4' AND BillCD = sc.SttlNo and CompanyCD=sc.CompanyCD),0))=0 then '未结算' end as IsAccText     ";
            strSql += "FROM officedba.SellChannelSttl AS sc LEFT OUTER JOIN                                                                ";
            strSql += "officedba.SellSend AS s ON sc.FromBillID = s.ID LEFT OUTER JOIN                                                     ";
            strSql += "officedba.CustInfo AS c ON sc.CustID = c.ID LEFT OUTER JOIN                                                         ";
            strSql += "officedba.EmployeeInfo AS e1 ON sc.Seller = e1.ID LEFT OUTER JOIN                                                   ";
            strSql += "officedba.CurrencyTypeSetting AS ct ON sc.CurrencyType = ct.ID LEFT OUTER JOIN                                      ";
            strSql += "officedba.DeptInfo AS d ON sc.SellDeptId = d.ID LEFT OUTER JOIN                                                     ";
            strSql += "officedba.EmployeeInfo AS e2 ON sc.Creator = e2.ID LEFT OUTER JOIN                                                  ";
            strSql += "officedba.EmployeeInfo AS e3 ON sc.Confirmor = e3.ID LEFT OUTER JOIN                                                ";
            strSql += "officedba.EmployeeInfo AS e4 ON sc.Closer = e4.ID                                                                   ";




            strSql += " WHERE (sc.ID = @ID ) AND (sc.CompanyCD = @CompanyCD)";
            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@ID", orderID);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }
        #endregion
        #endregion


        #region 确认、结单、取消确认、取消结单操作

        /// <summary>
        /// 确认单据
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool ConfirmOrder(string OrderNO, out string strMsg, out string strFieldText)
        {
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            strFieldText = "";
            //判断单据是够为制单状态，非制单状态不能确认
            if (isHandle(OrderNO, "1"))
            {
                if (IsConfirm(OrderNO, out strMsg, out strFieldText))
                {
                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {
                        SqlParameter[] paras = new SqlParameter[4];
                        strSq = "update  officedba.SellChannelSttl set BillStatus='2'  ";
                        
                            strSq += " , Confirmor=@EmployeeID, ConfirmDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                            paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                            paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                            paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                            paras[3] = new SqlParameter("@SttlNo", OrderNO);
                       
                        strSq += " WHERE SttlNo = @SttlNo and CompanyCD=@CompanyCD";

                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);
                        UpdateSellOrder(1, OrderNO, tran);
                        tran.Commit();
                        isSuc = true;
                        strMsg = "确认成功！";
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();

                        isSuc = false;
                        strMsg = "确认失败，请联系系统管理员！";
                        throw ex;
                    }
                }
            }
            else
            {
                isSuc = false;
                strMsg = "该单据已被其他用户确认，不可再次确认！";
            }
            return isSuc;
        }

        /// <summary>
        /// 更新发货单中的已退货数量
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="SendNo"></param>
        private static void UpdateSellOrder(int flag, string OrderNo, TransactionManager tran)
        {
            bool isMoreUnit = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit;//是否启用多计量单位
            string strCompanyCD = string.Empty;//单位编号
            string strSql = " SELECT FromLineNo, FromBillID,isnull(SttlNumber,0) as SttlNumber,isnull(UsedUnitCount,0) as UsedUnitCount FROM officedba.SellChannelSttlDetail ";
            strSql += " WHERE(SttlNo = @SttlNo) AND (CompanyCD = @CompanyCD) AND (FromType = '1')";
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            SqlParameter[] paras = { new SqlParameter("@SttlNo", OrderNo), new SqlParameter("@CompanyCD", strCompanyCD) };
            DataTable dt = SqlHelper.ExecuteSql(strSql, paras);
            strSql = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (flag)
                {
                    case 1://确认
                        strSql = "UPDATE officedba.SellSendDetail SET SttlCount = (isnull(SttlCount,0) + ";
                        break;
                    case 2://变更
                        strSql = "UPDATE officedba.SellSendDetail SET SttlCount = (isnull(SttlCount,0) - ";
                        break;
                    default:
                        break;
                }
                //多计量单位
                if (isMoreUnit)
                {
                    strSql += dt.Rows[i]["UsedUnitCount"].ToString();
                }
                else
                {
                    strSql += dt.Rows[i]["SttlNumber"].ToString();
                }
                
                strSql += ") where SendNo =( SELECT SendNo  FROM officedba.SellSend where  ID =@ID )  AND SortNo=@SortNo and (CompanyCD = @CompanyCD) ";
                SqlParameter[] param = { new SqlParameter("@ID", dt.Rows[i]["FromBillID"]),
                                           new SqlParameter("@SortNo", dt.Rows[i]["FromLineNo"]),
                                           new SqlParameter("@CompanyCD",strCompanyCD)
                                       };
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql, param);
            }
        }


        /// <summary>
        /// 结单
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool CloseOrder(string OrderNO, out string strMsg)
        {
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            //判断单据是否为执行状态，非执行状态不能结单
            if (isHandle(OrderNO, "2"))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    SqlParameter[] paras = new SqlParameter[4];
                    strSq = "update  officedba.SellChannelSttl set BillStatus='4'  ";
                    
                        strSq += " , Closer=@EmployeeID, CloseDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                        paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                        paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                        paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                        paras[3] = new SqlParameter("@SttlNo", OrderNO);
                    
                    strSq += " WHERE SttlNo = @SttlNo and CompanyCD=@CompanyCD";

                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);

                    tran.Commit();
                    isSuc = true;
                    strMsg = "结单成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    isSuc = false;
                    strMsg = "结单失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                isSuc = false;
                strMsg = "该单据已被其他用户结单，不可再次结单！";
            }
            return isSuc;
        }

        /// <summary>
        /// 取消结单
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool UnCloseOrder(string OrderNO, out string strMsg)
        {
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            //判断单据是否为手工结单状态，非手工结单状态不能结单
            if (isHandle(OrderNO, "4"))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    SqlParameter[] paras = new SqlParameter[5];
                    strSq = "update  officedba.SellChannelSttl set BillStatus='2'  ";
                    
                        strSq += " , Closer=@EmployeeID, CloseDate=@CloseDate, ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                        paras[0] = new SqlParameter("@EmployeeID", DBNull.Value);
                        paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                        paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                        paras[3] = new SqlParameter("@SttlNo", OrderNO);
                        paras[4] = new SqlParameter("@CloseDate", DBNull.Value);
                    
                    strSq += " WHERE SttlNo = @SttlNo and CompanyCD=@CompanyCD";

                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);

                    tran.Commit();
                    isSuc = true;
                    strMsg = "取消结单成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    isSuc = false;
                    strMsg = "取消结单失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                isSuc = false;
                strMsg = "该单据已被其他用户取消结单，不可再次取消结单！";
            }
            return isSuc;
        }

        /// <summary>
        /// 取消确认单据
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool UnConfirmOrder(string OrderNO, out string strMsg)
        {
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            int OrderId = GetOrderID(OrderNO);


            //判断单据是否为执行状态，非执行状态不能取消确认
            if (isHandle(OrderNO, "2"))
            {
                //判断单据是否被引用
                if (IsRef(OrderNO))
                {
                    int iEmployeeID = 0;//员工id
                    string strUserID = string.Empty;//用户id
                    string strCompanyCD = string.Empty;//单位编码
                    SqlParameter[] paras = new SqlParameter[5];
                    

                        iEmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                        strUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                        strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                   

                    strSq = "update  officedba.SellChannelSttl set BillStatus='1'   ";
                    strSq += " , Confirmor=@Confirmor, ConfirmDate=@ConfirmDate, ModifiedDate=getdate() ,ModifiedUserID=@UserID ";
                    strSq += " WHERE SttlNo = @SttlNo and CompanyCD=@CompanyCD";

                    paras[0] = new SqlParameter("@Confirmor", DBNull.Value);
                    paras[1] = new SqlParameter("@UserID", strUserID);
                    paras[2] = new SqlParameter("@CompanyCD", strCompanyCD);
                    paras[3] = new SqlParameter("@SttlNo", OrderNO);
                    paras[4] = new SqlParameter("@ConfirmDate", DBNull.Value);

                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {

                        FlowDBHelper.OperateCancelConfirm(strCompanyCD, 5, 6, OrderId, strUserID, tran);//撤销审批
                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);
                        UpdateSellOrder(2, OrderNO, tran);
                        tran.Commit();
                        isSuc = true;
                        strMsg = "取消确认成功！";
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();

                        isSuc = false;
                        strMsg = "取消确认失败，请联系系统管理员！";
                        throw ex;
                    }
                }
                else
                {
                    isSuc = false;
                    strMsg = "该单据已被其他单据引用，不可取消确认！";
                }
            }
            else
            {
                isSuc = false;
                strMsg = "该单据已被其他用户取消确认，不可再次取消确认！";
            }
            return isSuc;
        }

        #endregion

        #region 确认相关操作是否可以进行

        /// <summary>
        /// 根据单据状态判断该单据是否可以执行该操作
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="OrderStatus">单据状态</param>
        /// <returns>返回true时表示可以执行操作</returns>
        private static bool isHandle(string OrderNO, string OrderStatus)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;

            strSql = "select count(1) from officedba.SellChannelSttl where SttlNo = @SttlNo and CompanyCD=@CompanyCD and BillStatus=@BillStatus ";
            ArrayList arr = new ArrayList();
            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@SttlNo", OrderNO);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            paras[2] = new SqlParameter("@BillStatus", OrderStatus);

            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount != 0)
            {
                isSuc = true;
            }

            return isSuc;

        }

        /// <summary>
        /// 判断当前退货明细中的退货数量是否大于引用发货单中的未退货数量，超出不允许确认
        /// </summary>
        /// <param name="SendNo"></param>
        /// <param name="strMsg"></param>
        /// <param name="strFieldText"></param>
        /// <returns>返回true表示未超出，可以确认</returns>
        private static bool IsConfirm(string SendNo, out string strMsg, out string strFieldText)
        {
            bool isMoreUnit = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit;//是否启用多计量单位
            string strCompanyCD = string.Empty;//单位编号
            bool isSuc = true;
            strMsg = "";
            strFieldText = "";
            string strSql = "SELECT t.FromLineNo,s.SortNo, t.FromBillID,t.ProductCount,t.UsedUnitCount    ";
            strSql += "FROM officedba.SellChannelSttlDetail as s left join         ";
            strSql += "(SELECT FromLineNo, FromBillID, sum(isnull(SttlNumber,0)) as ProductCount,sum(isnull(UsedUnitCount,0)) as UsedUnitCount FROM officedba.SellChannelSttlDetail   ";
            strSql += "WHERE (SttlNo = @SttlNo) AND (CompanyCD = @CompanyCD)        ";
            strSql += "group by FromBillID,FromLineNo) as t on t.FromLineNo=s.FromLineNo and t.FromBillID=s.FromBillID   ";
            strSql += "WHERE (SttlNo = @SttlNo) AND (CompanyCD = @CompanyCD)        ";

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            SqlParameter[] paras = { new SqlParameter("@SttlNo", SendNo), new SqlParameter("@CompanyCD", strCompanyCD) };
            DataTable dt = SqlHelper.ExecuteSql(strSql, paras);
            strSql = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strSql = string.Empty;
                //多计量单位
                if (isMoreUnit)
                {
                    strSql += " select isnull((UsedUnitCount-isnull(SttlCount,0)),0) as pCount  from officedba.SellSendDetail ";
                }
                else
                {
                    strSql += " select isnull((ProductCount-isnull(SttlCount,0)),0) as pCount  from officedba.SellSendDetail ";
                }
                
                strSql += " where SendNo =( SELECT SendNo  FROM officedba.SellSend where  ID =@ID )  AND SortNo=@SortNo and (CompanyCD = @CompanyCD) ";
                SqlParameter[] param = { new SqlParameter("@ID", dt.Rows[i]["FromBillID"]),
                                           new SqlParameter("@SortNo", dt.Rows[i]["FromLineNo"]),
                                           new SqlParameter("@CompanyCD",strCompanyCD)
                                       };
                decimal pCount = Convert.ToDecimal(SqlHelper.ExecuteScalar(strSql, param));
                if (isMoreUnit)
                {
                    if (Convert.ToDecimal(dt.Rows[i]["UsedUnitCount"].ToString()) > pCount)
                    {
                        strFieldText += "明细第" + dt.Rows[i]["SortNo"].ToString() + "行：|";
                        strMsg += "本次结算数量大于源单中的未结算数量，请修改！|";
                        isSuc = false;
                    }
                }
                else
                {
                    if (Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString()) > pCount)
                    {
                        strFieldText += "明细第" + dt.Rows[i]["SortNo"].ToString() + "行：|";
                        strMsg += "本次结算数量大于源单中的未结算数量，请修改！|";
                        isSuc = false;
                    }
                }
                
            }
            return isSuc;
        }

        /// <summary>
        /// 判断单据编号是否存在
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <returns>返回true时表示不存在</returns>
        private static bool NoIsExist(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@SttlNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.SellChannelSttl ";
            strSql += " WHERE  (SttlNo = @SttlNo) AND (CompanyCD = @CompanyCD) ";
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }

        /// <summary>
        /// 单据是否被引用
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns>返回true时表示未被引用</returns>
        private static bool IsRef(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@SttlNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += "select  (                                                                                         ";
           
            strSql += "(SELECT count(1)                                                          ";
            strSql += "FROM officedba.BlendingDetails                                                    ";
            strSql += "WHERE BillingType = '4' AND BillCD = s.SttlNo and CompanyCD=s.CompanyCD) ";
            strSql += ") as tt     ";
            strSql += "FROM officedba.SellChannelSttl AS s                                                                                         ";

            strSql += "WHERE (s.SttlNo = @SttlNo) AND (s.CompanyCD = @CompanyCD)               ";

            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));


            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }

        /// <summary>
        /// 单据是否提交审批
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns>返回true时表示未提交</returns>
        private static bool IsFlow(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@SttlNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@FlowStatus",'5')
                                   };
            strSql += " SELECT COUNT(1) FROM officedba.FlowInstance AS f LEFT OUTER JOIN officedba.SellChannelSttl AS s ON f.BillID = s.ID AND f.BillTypeFlag = 5 AND f.BillTypeCode = 6 and f.CompanyCD=s.CompanyCD";
            strSql += "  WHERE (s.SttlNo = @SttlNo) AND (s.CompanyCD = @CompanyCD) and f.flowstatus!=@FlowStatus  ";
            try
            {
                iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            }
            catch
            {
                isSuc = false;
            }

            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }

        /// <summary>
        /// 根据单据状态判断单据是否可以被修改
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns>返回true时表示可以修改</returns>
        private static bool IsUpdate(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            string strStatus = string.Empty;
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@SttlNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select TOP  1  FlowStatus  from officedba.FlowInstance ";
            strSql += " WHERE (BillID = (SELECT ID FROM officedba.SellChannelSttl WHERE (SttlNo = @SttlNo) AND (CompanyCD = @CompanyCD))) AND BillTypeFlag = 5 AND BillTypeCode = 6  ";
            strSql += " ORDER BY ModifiedDate DESC ";
            object obj = SqlHelper.ExecuteScalar(strSql, paras);
            if (obj != null)
            {
                strStatus = obj.ToString();
                switch (strStatus)
                {
                    case "4":
                        isSuc = true;
                        break;
                    case "5":
                        isSuc = true;
                        break;
                    default:
                        isSuc = false;
                        break;
                }
            }
            else
            {
                isSuc = true;
            }

            return isSuc;
        }
        #endregion

        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          
            string strSql = "select * from  officedba.sellmodule_report_SellChannelSttl WHERE (SttlNo = @SttlNo) AND (CompanyCD = @CompanyCD)";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@SttlNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderDetail(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = "select  * from  officedba.sellmodule_report_SellChannelSttlDetail WHERE (SttlNo = @SttlNo) AND (CompanyCD = @CompanyCD) order by SortNo asc";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@SttlNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }
    }
}

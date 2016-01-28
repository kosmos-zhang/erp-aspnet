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
    public class SellBackDBHelper
    {
        #region 添加、修改、删除相关操作
        #region 保存销售退货单
        /// <summary>
        /// 保存销售发货单
        /// </summary>
        /// <returns></returns>
        public static bool SaveSellBack(Hashtable ht, SellBackModel sellBackModel, List<SellBackDetailModel> sellBackDetailModellList, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            TransactionManager tran = new TransactionManager();
            strMsg = "";
            //判断单据编号是否存在
            if (NoIsExist(sellBackModel.BackNo))
            {
                tran.BeginTransaction();
                try
                {
                    InsertSellBack(ht,sellBackModel, tran);
                    InsertSellBackDetail(sellBackDetailModellList, tran);
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
        #endregion

        #region 保存销退货单
        /// <summary>
        /// 保存销售发货单
        /// </summary>
        /// <returns></returns>
        public static bool UpdateSellBack(Hashtable ht, SellBackModel sellBackModel, List<SellBackDetailModel> sellBackDetailModellList, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            if (IsUpdate(sellBackModel.BackNo))
            {
                string strSql = "delete from officedba.SellBackDetail where  BackNo=@BackNo  and CompanyCD=@CompanyCD";
                SqlParameter[] paras = { new SqlParameter("@BackNo", sellBackModel.BackNo), new SqlParameter("@CompanyCD", sellBackModel.CompanyCD) };
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {

                    UpdateSellBack(ht,sellBackModel, tran);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);
                    InsertSellBackDetail(sellBackDetailModellList, tran);
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
        #endregion

        #region 更新主表数据
        /// <summary>
        /// 跟新主表数据
        /// </summary>
        /// <param name="sellBackModel"></param>
        /// <param name="tran"></param>
        private static void UpdateSellBack(Hashtable htExtAttr, SellBackModel sellBackModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SellBack set ");
            strSql.Append("CustID=@CustID,");
            strSql.Append("FromType=@FromType,");
            strSql.Append("FromBillID=@FromBillID,");
            strSql.Append("CustTel=@CustTel,");
            strSql.Append("Title=@Title,");
            strSql.Append("BackDate=@BackDate,");
            strSql.Append("Seller=@Seller,");
            strSql.Append("SellDeptId=@SellDeptId,");
            strSql.Append("CarryType=@CarryType,");
            strSql.Append("SendAddress=@SendAddress,");
            strSql.Append("ReceiveAddress=@ReceiveAddress,");
            strSql.Append("PayType=@PayType,");
            strSql.Append("MoneyType=@MoneyType,");
            strSql.Append("CurrencyType=@CurrencyType,");
            strSql.Append("Rate=@Rate,");
            strSql.Append("isAddTax=@isAddTax,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("Tax=@Tax,");
            strSql.Append("TotalFee=@TotalFee,");
            strSql.Append("Discount=@Discount,");
            strSql.Append("DiscountTotal=@DiscountTotal,");
            strSql.Append("RealTotal=@RealTotal,");
            strSql.Append("CountTotal=@CountTotal,");
            strSql.Append("NotPayTotal=@NotPayTotal,");
            strSql.Append("BackTotal=@BackTotal,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("BillStatus=@BillStatus,");
            strSql.Append("Confirmor=@Confirmor,");
            strSql.Append("ConfirmDate=@ConfirmDate,");
            strSql.Append("Closer=@Closer,");
            strSql.Append("CloseDate=@CloseDate,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID,");
            strSql.Append("BusiType=@BusiType");
            strSql.Append(",ProjectID=@ProjectID");
            strSql.Append(" where CompanyCD=@CompanyCD and BackNo=@BackNo ");

            SqlParameter[] param = null;
            ArrayList lcmd = new ArrayList();
            #region 参数
            lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellBackModel.CompanyCD));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustID", sellBackModel.CustID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustTel", sellBackModel.CustTel));
            lcmd.Add(SqlHelper.GetParameterFromString("@BackNo", sellBackModel.BackNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@Title", sellBackModel.Title));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromType", sellBackModel.FromType));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromBillID", sellBackModel.FromBillID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@BackDate", sellBackModel.BackDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Seller", sellBackModel.Seller.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellDeptId", sellBackModel.SellDeptId.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SendAddress", sellBackModel.SendAddress));
            lcmd.Add(SqlHelper.GetParameterFromString("@ReceiveAddress", sellBackModel.ReceiveAddress));
            lcmd.Add(SqlHelper.GetParameterFromString("@CarryType", sellBackModel.CarryType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@PayType", sellBackModel.PayType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@MoneyType", sellBackModel.MoneyType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CurrencyType", sellBackModel.CurrencyType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Rate", sellBackModel.Rate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalPrice", sellBackModel.TotalPrice.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@isAddTax", sellBackModel.isAddTax.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Tax", sellBackModel.Tax.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalFee", sellBackModel.TotalFee.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Discount", sellBackModel.Discount.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@DiscountTotal", sellBackModel.DiscountTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@RealTotal", sellBackModel.RealTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CountTotal", sellBackModel.CountTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@NotPayTotal", sellBackModel.NotPayTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@BackTotal", sellBackModel.BackTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Remark", sellBackModel.Remark));
            lcmd.Add(SqlHelper.GetParameterFromString("@BillStatus", sellBackModel.BillStatus));
            lcmd.Add(SqlHelper.GetParameterFromString("@Confirmor", sellBackModel.Confirmor.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ConfirmDate", sellBackModel.ConfirmDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Closer", sellBackModel.Closer.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CloseDate", sellBackModel.CloseDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", sellBackModel.ModifiedUserID));
            lcmd.Add(SqlHelper.GetParameterFromString("@BusiType", sellBackModel.BusiType));
            lcmd.Add(SqlHelper.GetParameterFromString("@ProjectID", sellBackModel.ProjectID.ToString()));
            #endregion

            #region 拓展属性

            if (htExtAttr != null && htExtAttr.Count != 0)
            {
                strSql.Append(" ;UPDATE officedba.SellBack set ");
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql.Append(de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",");
                    lcmd.Add(SqlHelper.GetParameterFromString("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim()));
                }
                strSql.Append(" BillStatus=@BillStatus  where CompanyCD=@CompanyCD and BackNo=@BackNo ");
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


            sql = " select ID from officedba.SellBack where CompanyCD=@CompanyCD and BackNo=@BackNo ";
            SqlParameter[] paras = { new SqlParameter("@CompanyCD", strCompanyCD), new SqlParameter("@BackNo", orderNo) };
            OrderID = (int)SqlHelper.ExecuteScalar(sql, paras);
            return OrderID;
        }
        #endregion 

        #region 为主表插入数据
        /// <summary>
        /// 为主表插入数据
        /// </summary>
        /// <param name="sellBackModel"></param>
        /// <param name="tran"></param>
        private static void InsertSellBack(Hashtable htExtAttr, SellBackModel sellBackModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SellBack(");
            strSql.Append("CompanyCD,BackNo,CustID,FromType,FromBillID,CustTel,Title,BackDate,Seller,SellDeptId,CarryType,SendAddress,ReceiveAddress,PayType,MoneyType,CurrencyType,Rate,isAddTax,TotalPrice,Tax,TotalFee,Discount,DiscountTotal,RealTotal,CountTotal,NotPayTotal,BackTotal,Remark,BillStatus,Creator,CreateDate,Confirmor,ConfirmDate,Closer,CloseDate,ModifiedDate,ModifiedUserID,BusiType,ProjectID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@BackNo,@CustID,@FromType,@FromBillID,@CustTel,@Title,@BackDate,@Seller,@SellDeptId,@CarryType,@SendAddress,@ReceiveAddress,@PayType,@MoneyType,@CurrencyType,@Rate,@isAddTax,@TotalPrice,@Tax,@TotalFee,@Discount,@DiscountTotal,@RealTotal,@CountTotal,@NotPayTotal,@BackTotal,@Remark,@BillStatus,@Creator,getdate(),@Confirmor,@ConfirmDate,@Closer,@CloseDate,getdate(),@ModifiedUserID,@BusiType,@ProjectID)");

            SqlParameter[] param = null;
            ArrayList lcmd = new ArrayList();
            #region 参数
            lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellBackModel.CompanyCD));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustID", sellBackModel.CustID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustTel", sellBackModel.CustTel));
            lcmd.Add(SqlHelper.GetParameterFromString("@BackNo", sellBackModel.BackNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@Title", sellBackModel.Title));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromType", sellBackModel.FromType));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromBillID", sellBackModel.FromBillID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@BackDate", sellBackModel.BackDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Seller", sellBackModel.Seller.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellDeptId", sellBackModel.SellDeptId.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SendAddress", sellBackModel.SendAddress));
            lcmd.Add(SqlHelper.GetParameterFromString("@ReceiveAddress", sellBackModel.ReceiveAddress));
            lcmd.Add(SqlHelper.GetParameterFromString("@CarryType", sellBackModel.CarryType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@PayType", sellBackModel.PayType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@MoneyType", sellBackModel.MoneyType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CurrencyType", sellBackModel.CurrencyType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Rate", sellBackModel.Rate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalPrice", sellBackModel.TotalPrice.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@isAddTax", sellBackModel.isAddTax.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Tax", sellBackModel.Tax.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalFee", sellBackModel.TotalFee.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Discount", sellBackModel.Discount.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@DiscountTotal", sellBackModel.DiscountTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@RealTotal", sellBackModel.RealTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CountTotal", sellBackModel.CountTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@NotPayTotal", sellBackModel.NotPayTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@BackTotal", sellBackModel.BackTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Remark", sellBackModel.Remark));
            lcmd.Add(SqlHelper.GetParameterFromString("@BillStatus", sellBackModel.BillStatus));
            lcmd.Add(SqlHelper.GetParameterFromString("@Creator", sellBackModel.Creator.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Confirmor", sellBackModel.Confirmor.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ConfirmDate", sellBackModel.ConfirmDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Closer", sellBackModel.Closer.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CloseDate", sellBackModel.CloseDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", sellBackModel.ModifiedUserID));
            lcmd.Add(SqlHelper.GetParameterFromString("@BusiType", sellBackModel.BusiType));
            lcmd.Add(SqlHelper.GetParameterFromString("@ProjectID", sellBackModel.ProjectID.ToString()));
            #endregion

            #region 拓展属性

            if (htExtAttr != null && htExtAttr.Count != 0)
            {
                strSql.Append(" ;UPDATE officedba.SellBack set ");
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql.Append(de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",");
                    lcmd.Add(SqlHelper.GetParameterFromString("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim()));
                }
                strSql.Append(" BillStatus=@BillStatus  where CompanyCD=@CompanyCD and BackNo=@BackNo ");
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
        /// <param name="sellBackDetailModellList"></param>
        /// <param name="tran"></param>
        private static void InsertSellBackDetail(List<SellBackDetailModel> sellBackDetailModellList, TransactionManager tran)
        {
            foreach (SellBackDetailModel sellBackDetailModel in sellBackDetailModellList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into officedba.SellBackDetail(");
                strSql.Append("CompanyCD,BackNo,SortNo,ProductID,ProductCount,UnitID,UnitPrice,TaxPrice,Discount,TaxRate,TotalFee,TotalPrice,TotalTax,Package,BackNumber,Reason,InNumber,Remark,FromType,FromBillID,FromLineNo,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate)");
                strSql.Append(" values (");
                strSql.Append("@CompanyCD,@BackNo,@SortNo,@ProductID,@ProductCount,@UnitID,@UnitPrice,@TaxPrice,@Discount,@TaxRate,@TotalFee,@TotalPrice,@TotalTax,@Package,@BackNumber,@Reason,@InNumber,@Remark,@FromType,@FromBillID,@FromLineNo,getdate(),@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate)");
                #region 参数
                SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@BackNo", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@ProductCount", SqlDbType.Decimal,9),
					new SqlParameter("@UnitID", SqlDbType.Int,4),
					new SqlParameter("@UnitPrice", SqlDbType.Decimal,9),
					new SqlParameter("@TaxPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Discount", SqlDbType.Decimal,5),
					new SqlParameter("@TaxRate", SqlDbType.Decimal,5),
					new SqlParameter("@TotalFee", SqlDbType.Decimal,9),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@TotalTax", SqlDbType.Decimal,9),
					new SqlParameter("@Package", SqlDbType.Int,4),
					new SqlParameter("@BackNumber", SqlDbType.Decimal,9),
					new SqlParameter("@Reason", SqlDbType.Int,4),
					new SqlParameter("@InNumber", SqlDbType.Decimal,9),
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
                parameters[0].Value = sellBackDetailModel.CompanyCD;
                parameters[1].Value = sellBackDetailModel.BackNo;
                parameters[2].Value = sellBackDetailModel.SortNo;
                parameters[3].Value = sellBackDetailModel.ProductID;
                parameters[4].Value = sellBackDetailModel.ProductCount;
                parameters[5].Value = sellBackDetailModel.UnitID;
                parameters[6].Value = sellBackDetailModel.UnitPrice;
                parameters[7].Value = sellBackDetailModel.TaxPrice;
                parameters[8].Value = sellBackDetailModel.Discount;
                parameters[9].Value = sellBackDetailModel.TaxRate;
                parameters[10].Value = sellBackDetailModel.TotalFee;
                parameters[11].Value = sellBackDetailModel.TotalPrice;
                parameters[12].Value = sellBackDetailModel.TotalTax;
                parameters[13].Value = sellBackDetailModel.Package;
                parameters[14].Value = sellBackDetailModel.BackNumber;
                parameters[15].Value = sellBackDetailModel.Reason;
                parameters[16].Value = sellBackDetailModel.InNumber;
                parameters[17].Value = sellBackDetailModel.Remark;
                parameters[18].Value = sellBackDetailModel.FromType;
                parameters[19].Value = sellBackDetailModel.FromBillID;
                parameters[20].Value = sellBackDetailModel.FromLineNo;
                parameters[22].Value = sellBackDetailModel.ModifiedUserID;
                parameters[23].Value = sellBackDetailModel.UsedUnitID;
                parameters[24].Value = sellBackDetailModel.UsedUnitCount;
                parameters[25].Value = sellBackDetailModel.UsedPrice;
                parameters[26].Value = sellBackDetailModel.ExRate;
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
        #region 删除退货单
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
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellBack WHERE BackNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellBackDetail WHERE BackNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);

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

        #region 获取退货单信息相关操作
        /// <summary>
        /// 获取退货原因信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetReasonType()
        {
            StringBuilder sql = new StringBuilder();
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            sql.AppendLine("SELECT [ID]");
            sql.AppendLine(",[CodeName]");
            sql.AppendLine(",[Description]");
            sql.AppendLine("FROM [officedba].[CodeReasonType] where CompanyCD='" + strCompanyCD + "' and Flag=20 and UsedStatus='1'");
            return SqlHelper.ExecuteSql(sql.ToString());
        }

        /// <summary>
        /// 获取退货单列表 
        /// </summary>
        /// <param name="sellContractModel">sellContractModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(SellBackModel sellBackModel, DateTime? dt, int? Reason, int? FlowStatus,string EFIndex,string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            strSql = "select * from (SELECT s.ID, s.BackNo, s.Title,s.ModifiedDate, CONVERT(varchar(100), s.BackDate, 23) AS BackDate, s.TotalPrice, c.CustName, e.EmployeeName, ";
            strSql += "CASE s.FromType WHEN 0 THEN '无来源' WHEN 1 THEN '销售发货单' END AS FromTypeText, ";
            strSql += "CASE s.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更' WHEN 4 THEN ";
            strSql += "'手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText, CASE WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) IS NULL THEN '' WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) = 1 THEN '待审批' WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) = 2 THEN '审批中' WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) = 3 THEN '审批通过' WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) = 4 THEN '审批不通过' WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) = 5 THEN '撤销审批' END AS FlowInstanceText, isnull(CASE ";
            strSql += "((SELECT count(1) FROM officedba.StorageInOther AS soo ";
            strSql += "WHERE soo.FromType = '1' AND soo.FromBillID = s.ID)+ ";
            strSql += "(SELECT count(1)                                                          ";
            strSql += "FROM officedba.BlendingDetails                                                    ";
            strSql += "WHERE BillingType = '3' AND BillCD = s.BackNo and CompanyCD=s.CompanyCD) ";
            strSql += " ) WHEN 0 THEN '无引用' END, '被引用') AS RefText, ";
            strSql += "(SELECT TOP 1 FlowStatus FROM officedba.FlowInstance ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) AS FlowStatus ";
            strSql += "FROM officedba.SellBack AS s LEFT JOIN ";
            strSql += "officedba.CustInfo AS c ON s.CustID = c.ID LEFT JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID  where 1=1 and s.CompanyCD=@CompanyCD";
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
            if (sellBackModel.ProjectID != null)
            {
                strSql += " and s.ProjectID=@ProjectID ";
                arr.Add(new SqlParameter("@ProjectID", sellBackModel.ProjectID));
            }
            if (sellBackModel.BillStatus != null)
            {
                strSql += " and s.BillStatus= @BillStatus";
                arr.Add(new SqlParameter("@BillStatus", sellBackModel.BillStatus));
            }

            if (sellBackModel.FromType != null)
            {
                strSql += " and s.FromType=@FromType";
                arr.Add(new SqlParameter("@FromType", sellBackModel.FromType));
            }

            if (sellBackModel.BackNo != null)
            {
                strSql += " and s.BackNo like @BackNo";
                arr.Add(new SqlParameter("@BackNo", "%" + sellBackModel.BackNo + "%"));
            }
            if (Reason != null)
            {
                strSql += " and s.BackNo in (select BackNo from officedba.SellBackDetail where CompanyCD=s.CompanyCD and Reason=@Reason) ";
                arr.Add(new SqlParameter("@Reason", Reason));
            }
            if (sellBackModel.BackDate != null)
            {
                strSql += " and s.BackDate >= @BackDate";
                arr.Add(new SqlParameter("@BackDate", sellBackModel.BackDate));
            }
            if (dt != null)
            {
                strSql += " and s.BackDate <= @BackDate1";
                arr.Add(new SqlParameter("@BackDate1", dt));
            }
            if (sellBackModel.CustID != null)
            {
                strSql += " and s.CustID=@CustID";
                arr.Add(new SqlParameter("@CustID", sellBackModel.CustID));
            }
            if (sellBackModel.Title != null)
            {
                strSql += " and s.Title like @Title";
                arr.Add(new SqlParameter("@Title", "%" + sellBackModel.Title + "%"));
            }
            if (sellBackModel.Seller != null)
            {
                strSql += " and s.Seller=@Seller";
                arr.Add(new SqlParameter("@Seller", sellBackModel.Seller));
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

        /// <summary>
        /// 获取单据明细信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable GetOrderDetail(string orderNo)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = " SELECT s.ProductID, s.ProductCount, s.UnitID, s.UnitPrice, s.TaxPrice, s.Discount,       ";
            strSql += "  s.TaxRate, s.TotalFee, s.TotalPrice, s.TotalTax, s.Package, s.BackNumber,isnull(pc.TypeName,'') as ColorName,    ";
            strSql += "  s.Reason, ISNULL(s.InNumber, 0) AS InNumber, s.Remark, s.FromType, s.FromBillID,        ";
            strSql += "  s.FromLineNo, p.ProductName,ss.SendNo, c.CodeName, ISNULL(p.SellTax,                    ";
            strSql += "  0) AS SellTax, ISNULL(p.TaxRate, 0) AS TaxRate1, p.ProdNo,isnull(p.StandardCost,0) as StandardCost,  ";
            strSql += "  CASE s.FromType WHEN '0' THEN '无来源' WHEN '1' THEN '销售发货单' END AS FromTypeText,  ";
            strSql += " isnull((select BackCount from officedba.SellSendDetail where                             ";
            strSql += "  SendNo=( select SendNo from officedba.SellSend where ID=s.FromBillID)                   ";
            strSql += " and SortNo=s.FromLineNo and CompanyCD=s.CompanyCD),0)   as BackCount                     ";
            //物品售价，单位ID，数量，单价，换算率
            strSql += ",isnull(p.StandardSell,0) as StandardSell,s.UsedUnitID,isnull(s.UsedUnitCount,0) as UsedUnitCount,isnull(s.UsedPrice,0) as UsedPrice,isnull(s.ExRate,1) as ExRate  ";
            strSql += "  FROM officedba.SellBackDetail AS s LEFT OUTER JOIN   officedba.SellSend as ss on  ss.ID=s.FromBillID LEFT OUTER JOIN ";
            strSql += "  officedba.ProductInfo AS p ON s.ProductID = p.ID LEFT OUTER JOIN                        ";
            strSql += " officedba.CodePublicType as pc on pc.ID=p.ColorID left join ";
            strSql += "  officedba.CodeUnitType AS c ON s.UnitID = c.ID                                          ";

            strSql += " WHERE (s.BackNo = @BackNo) AND (s.CompanyCD = @CompanyCD) ";
            strSql += "ORDER BY s.SortNo ";

            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@BackNo", orderNo);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }


        /// <summary>
        /// 获取退货单主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = "SELECT ct.CurrencyName, so.SendNo, c.CustName, d.DeptName, e5.EmployeeName AS SellerName,    ";
            strSql += "e2.EmployeeName AS CreatorName,e3.EmployeeName AS ConfirmorName, e4.EmployeeName AS   ";
            strSql += "CloserName, ss.CustID, ss.Title, ss.FromType, ss.FromBillID, ss.BusiType,    ";
            strSql += "ss.MoneyType, ss.CarryType, ss.CurrencyType, ss.Rate, ss.TotalPrice, ss.Tax, ";
            strSql += "ss.TotalFee, ss.Discount, ss.Remark, ss.Creator, ss.BillStatus,   ";
            strSql += "ss.ExtField1,ss.ExtField2,ss.ExtField3,ss.ExtField4,ss.ExtField5,";
            strSql += "ss.ExtField6,ss.ExtField7,ss.ExtField8,ss.ExtField9,ss.ExtField10, ";
            strSql += "CONVERT(varchar(100), ss.CreateDate, 23) AS CreateDate,  ";
            strSql += " CONVERT(varchar(100), ss.ConfirmDate, 23) AS ConfirmDate, ss.Confirmor, ss.Closer, ";
            strSql += "CONVERT(varchar(100), ss.CloseDate, 23) AS CloseDate,     ";
            strSql += "CONVERT(varchar(100), ss.ModifiedDate, 23) AS ModifiedDate, ss.ModifiedUserID,                                        ";
            strSql += "ss.SellDeptId, ss.DiscountTotal, ss.RealTotal, ss.isAddTax, ss.CountTotal, ss.Seller,                                 ";
            strSql += " ss.ProjectID,p.ProjectName ,";
            strSql += "CASE ss.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更'                                           ";
            strSql += "WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText,                                                  ";
            strSql += "ss.BackNo, ss.CustTel, CONVERT(varchar(100), ss.BackDate, 23) AS BackDate,                                            ";
            strSql += "ss.SendAddress, ss.ReceiveAddress,                         ";
            strSql += "case when (SELECT    isnull( sum(InNumber),0) as InNumber                                                             ";
            strSql += "FROM officedba.SellBackDetail                                                                                         ";
            strSql += "WHERE BackNo = ss.BackNo and CompanyCD=ss.CompanyCD) =0 then '未入库'                                                 ";
            strSql += "when (SELECT isnull( sum(InNumber),0) as InNumber                                                                     ";
            strSql += "FROM officedba.SellBackDetail                                                                                         ";
            strSql += "WHERE BackNo = ss.BackNo and CompanyCD=ss.CompanyCD) >0  and                                                          ";
            strSql += "(SELECT isnull( sum(InNumber),0) as InNumber                                                                          ";
            strSql += "FROM officedba.SellBackDetail                                                                                         ";
            strSql += "WHERE BackNo = ss.BackNo and CompanyCD=ss.CompanyCD)<                                                                 ";
            strSql += "(SELECT isnull( sum(BackNumber),0) as BackNumber                                                                      ";
            strSql += "FROM officedba.SellBackDetail                                                                                         ";
            strSql += "WHERE BackNo = ss.BackNo and CompanyCD=ss.CompanyCD)                                                                  ";
            strSql += "then '部分入库'                                                                                                       ";
            strSql += "when (SELECT isnull( sum(InNumber),0) as InNumber                                                                     ";
            strSql += "FROM officedba.SellBackDetail                                                                                         ";
            strSql += "WHERE BackNo = ss.BackNo and CompanyCD=ss.CompanyCD) <>0  and                                                         ";
            strSql += "(SELECT isnull( sum(InNumber),0) as InNumber                                                                          ";
            strSql += "FROM officedba.SellBackDetail                                                                                         ";
            strSql += "WHERE BackNo = ss.BackNo and CompanyCD=ss.CompanyCD)=                                                                 ";
            strSql += "(SELECT isnull( sum(BackNumber),0) as BackNumber                                                                      ";
            strSql += "FROM officedba.SellBackDetail                                                                                         ";
            strSql += "WHERE BackNo = ss.BackNo and CompanyCD=ss.CompanyCD)                                                                  ";
            strSql += "then '已入库'                                                                                                         ";
            strSql += "end as isSendText,                                                                                                    ";
            strSql += "ss.PayType, ss.NotPayTotal, ss.BackTotal ,                                                                            ";
            strSql += "CASE ss.isOpenbill WHEN '0' THEN '未建单' WHEN '1' THEN '已建单' END AS isOpenbillText  ,                             ";
            strSql += "ISNULL((SELECT ISNULL(SUM(YAccounts), 0) AS Expr1                                                                     ";
            strSql += "FROM officedba.BlendingDetails                                                                                                ";
            strSql += "WHERE (BillingType = '3') AND (BillCD = ss.BackNo) AND (CompanyCD = ss.CompanyCD)), 0) AS YAccounts ,                 ";
            strSql += " case when (isnull((SELECT isnull(sum(YAccounts),0) FROM officedba.BlendingDetails                                            ";
            strSql += "WHERE BillingType = '3' AND BillCD = ss.BackNo and CompanyCD=ss.CompanyCD),0))>0                                      ";
            strSql += "and (isnull((SELECT isnull(sum(YAccounts),0) FROM officedba.BlendingDetails                                                   ";
            strSql += "WHERE BillingType = '3' AND BillCD = ss.BackNo and CompanyCD=ss.CompanyCD),0)) <                                      ";
            strSql += "(isnull((SELECT isnull(max(TotalPrice),0) FROM officedba.BlendingDetails                                                      ";
            strSql += "WHERE BillingType = '3' AND BillCD = ss.BackNo and CompanyCD=ss.CompanyCD),0)) then '部分结算'                        ";
            strSql += "when (isnull( (SELECT isnull(sum(YAccounts),0) FROM officedba.BlendingDetails                                                 ";
            strSql += "WHERE BillingType = '3' AND BillCD = ss.BackNo and CompanyCD=ss.CompanyCD),0)) =                                      ";
            strSql += "(isnull((SELECT isnull(max(TotalPrice),0) FROM officedba.BlendingDetails                                                      ";
            strSql += "WHERE BillingType = '3' AND BillCD = ss.BackNo and CompanyCD=ss.CompanyCD),0)) and                                    ";
            strSql += "(isnull((SELECT isnull(sum(YAccounts),0) FROM officedba.BlendingDetails                                                       ";
            strSql += "WHERE BillingType = '3' AND BillCD = ss.BackNo and CompanyCD=ss.CompanyCD),0))<>0 then '已结算'                       ";
            strSql += "when (isnull((SELECT isnull(sum(YAccounts),0)                                                                         ";
            strSql += "FROM officedba.BlendingDetails                                                                                                ";
            strSql += "WHERE BillingType = '3' AND BillCD = ss.BackNo and CompanyCD=ss.CompanyCD),0))=0 then '未结算' end as IsAccText       ";
            strSql += "FROM officedba.SellBack AS ss LEFT OUTER JOIN                                                                         ";
            strSql += "officedba.CurrencyTypeSetting AS ct ON ss.CurrencyType = ct.ID LEFT OUTER JOIN                                        ";
            strSql += "officedba.CustInfo AS c ON ss.CustID = c.ID LEFT OUTER JOIN                                                           ";
            strSql += "officedba.DeptInfo AS d ON ss.SellDeptId = d.ID LEFT OUTER JOIN                                                       ";
            strSql += "officedba.SellSend AS so ON ss.FromBillID = so.ID LEFT OUTER JOIN                                                     ";
            strSql += "officedba.EmployeeInfo AS e2 ON ss.Creator = e2.ID LEFT OUTER JOIN                                                    ";
            strSql += "officedba.EmployeeInfo AS e3 ON ss.Confirmor = e3.ID LEFT OUTER JOIN                                                  ";
            strSql += "officedba.EmployeeInfo AS e4 ON ss.Closer = e4.ID LEFT OUTER JOIN officedba.EmployeeInfo AS e5 ON ss.Seller = e5.ID   ";
            strSql += " left join officedba.ProjectInfo p on p.ID=ss.ProjectID ";


            strSql += " WHERE (ss.ID = @ID ) AND (ss.CompanyCD = @CompanyCD)";
            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@ID", orderID);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }

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
                        strSq = "update  officedba.SellBack set BillStatus='2'  ";

                        strSq += " , Confirmor=@EmployeeID, ConfirmDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                        paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                        paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                        paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                        paras[3] = new SqlParameter("@BackNo", OrderNO);

                        strSq += " WHERE BackNo = @BackNo and CompanyCD=@CompanyCD";

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
        private static void UpdateSellOrder(int flag, string BackNo, TransactionManager tran)
        {
            bool isMoreUnit = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit;//是否启用多计量单位（true启用，false不启用）
            string strCompanyCD = string.Empty;//单位编号
            string strSql = " SELECT FromLineNo, FromBillID,isnull(BackNumber,0) as BackNumber,isnull(UsedUnitCount,0) as UsedUnitCount FROM officedba.SellBackDetail ";
            strSql += " WHERE(BackNo = @BackNo) AND (CompanyCD = @CompanyCD) AND (FromType = '1')";

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            SqlParameter[] paras = { new SqlParameter("@BackNo", BackNo), new SqlParameter("@CompanyCD", strCompanyCD) };
            DataTable dt = SqlHelper.ExecuteSql(strSql, paras);
            strSql = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (flag)
                {
                    case 1://确认
                        strSql = "UPDATE officedba.SellSendDetail SET BackCount = (isnull(BackCount,0) + ";
                        break;
                    case 2://取消确认
                        strSql = "UPDATE officedba.SellSendDetail SET BackCount = (isnull(BackCount,0) - ";
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
                    strSql += dt.Rows[i]["BackNumber"].ToString();
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
                    strSq = "update  officedba.SellBack set BillStatus='4'  ";

                    strSq += " , Closer=@EmployeeID, CloseDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                    paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                    paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                    paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    paras[3] = new SqlParameter("@BackNo", OrderNO);

                    strSq += " WHERE BackNo = @BackNo and CompanyCD=@CompanyCD";

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
                    strSq = "update  officedba.SellBack set BillStatus='2'  ";

                    strSq += " , Closer=@EmployeeID, CloseDate=@CloseDate, ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                    paras[0] = new SqlParameter("@EmployeeID", DBNull.Value);
                    paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                    paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    paras[3] = new SqlParameter("@BackNo", OrderNO);
                    paras[4] = new SqlParameter("@CloseDate", DBNull.Value);

                    strSq += " WHERE BackNo = @BackNo and CompanyCD=@CompanyCD";

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



                    strSq = "update  officedba.SellBack set BillStatus='1'   ";
                    strSq += " , Confirmor=@Confirmor, ConfirmDate=@ConfirmDate, ModifiedDate=getdate() ,ModifiedUserID=@UserID ";
                    strSq += " WHERE BackNo = @BackNo and CompanyCD=@CompanyCD";

                    paras[0] = new SqlParameter("@Confirmor", DBNull.Value);
                    paras[1] = new SqlParameter("@UserID", strUserID);
                    paras[2] = new SqlParameter("@CompanyCD", strCompanyCD);
                    paras[3] = new SqlParameter("@BackNo", OrderNO);
                    paras[4] = new SqlParameter("@ConfirmDate", DBNull.Value);

                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {

                        FlowDBHelper.OperateCancelConfirm(strCompanyCD, 5, 5, OrderId, strUserID, tran);//撤销审批
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

            strSql = "select count(1) from officedba.SellBack where BackNo = @BackNo and CompanyCD=@CompanyCD and BillStatus=@BillStatus ";
            ArrayList arr = new ArrayList();
            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@BackNo", OrderNO);
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
            strSql += "FROM officedba.SellBackDetail as s left join         ";
            strSql += "(SELECT FromLineNo, FromBillID, sum(isnull(BackNumber,0)) as ProductCount,sum(isnull(UsedUnitCount,0)) as UsedUnitCount FROM officedba.SellBackDetail   ";
            strSql += "WHERE (BackNo = @BackNo) AND (CompanyCD = @CompanyCD) AND (FromType = '1')         ";
            strSql += "group by FromBillID,FromLineNo) as t on t.FromLineNo=s.FromLineNo and t.FromBillID=s.FromBillID   ";
            strSql += "WHERE (BackNo = @BackNo) AND (CompanyCD = @CompanyCD) AND (FromType = '1')          ";

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            SqlParameter[] paras = { new SqlParameter("@BackNo", SendNo), new SqlParameter("@CompanyCD", strCompanyCD) };
            DataTable dt = SqlHelper.ExecuteSql(strSql, paras);
            strSql = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strSql = string.Empty;
                if (isMoreUnit)
                {
                    strSql += " select isnull((UsedUnitCount-isnull(BackCount,0)),0) as pCount  from officedba.SellSendDetail ";
                }
                else
                {
                    strSql += " select isnull((ProductCount-isnull(BackCount,0)),0) as pCount  from officedba.SellSendDetail ";
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
                        strMsg += "本次退货数量大于源单中的未退货数量，请修改！|";
                        isSuc = false;
                    }
                }
                else
                {
                    if (Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString()) > pCount)
                    {
                        strFieldText += "明细第" + dt.Rows[i]["SortNo"].ToString() + "行：|";
                        strMsg += "本次退货数量大于源单中的未退货数量，请修改！|";
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
                                       new SqlParameter("@BackNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.SellBack ";
            strSql += " WHERE  (BackNo = @BackNo) AND (CompanyCD = @CompanyCD) ";
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
                                       new SqlParameter("@BackNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += "select  (                                                                                         ";
            strSql += "(SELECT count(1) FROM officedba.StorageInOther AS soo ";
            strSql += "WHERE soo.FromType = '1' AND soo.FromBillID = s.ID) +                                                                 ";
            strSql += "(SELECT count(1)                                                          ";
            strSql += "FROM officedba.BlendingDetails                                                    ";
            strSql += "WHERE BillingType = '3' AND BillCD = s.BackNo and CompanyCD=s.CompanyCD) ";
            strSql += ") as tt     ";
            strSql += "FROM officedba.SellBack AS s                                                                                         ";

            strSql += "WHERE (s.BackNo = @BackNo) AND (s.CompanyCD = @CompanyCD)               ";

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
                                       new SqlParameter("@BackNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@FlowStatus",'5')
                                   };
            strSql += " SELECT COUNT(1) FROM officedba.FlowInstance AS f LEFT OUTER JOIN officedba.SellBack AS s ON f.BillID = s.ID AND f.BillTypeFlag = 5 AND f.BillTypeCode = 5 and f.CompanyCD=s.CompanyCD";
            strSql += "  WHERE (s.BackNo = @BackNo) AND (s.CompanyCD = @CompanyCD) and f.flowstatus!=@FlowStatus  ";
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
                                       new SqlParameter("@BackNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select TOP  1  FlowStatus  from officedba.FlowInstance ";
            strSql += " WHERE (BillID = (SELECT ID FROM officedba.SellBack WHERE (BackNo = @BackNo) AND (CompanyCD = @CompanyCD))) AND BillTypeFlag = 5 AND BillTypeCode = 5  ";
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

            string strSql = "select * from  officedba.sellmodule_report_SellBack WHERE (BackNo = @BackNo) AND (CompanyCD = @CompanyCD)";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@BackNo",OrderNo)
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

            string strSql = "select  * from  officedba.sellmodule_report_SellBackDetail WHERE (BackNo = @BackNo) AND (CompanyCD = @CompanyCD) order by SortNo asc";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@BackNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }

        #region 新报表

        /// <summary>
        /// 退货数量部门分布
        /// </summary>
        /// <param name="Type">退货单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetBackByDeptNum(string Type, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(b.DeptName) as Name,count(1) as Counts,a.selldeptId as DeptId  ");
            sb.Append(" from officedba.sellback as a left join officedba.DeptInfo as b on a.sellDeptId=b.Id  ");
            sb.Append(" where a.selldeptId is not null and a.BillStatus<>'1' and a.selldeptId !='' and a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (Type != "")
            {
                sb.Append("and a.BusiType=");
                sb.Append(Type);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.BackDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.BackDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }

            sb.Append(" group by a.selldeptId ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 退货数量人员分布
        /// </summary>
        /// <param name="Type">订单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetBackByPersonNum(string Type, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(b.EmployeeName) as Name,count(1) as Counts,a.Seller ");
            sb.Append(" from officedba.sellback as a left join officedba.EmployeeInfo as b on a.Seller=b.Id  ");
            sb.Append(" where a.BillStatus<>'1' and a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (Type != "")
            {
                sb.Append("and a.BusiType=");
                sb.Append(Type);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.BackDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.BackDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by a.Seller ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }



        /// <summary>
        /// 退货数量区域分布
        /// </summary>
        /// <param name="BusiType">分类</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetBackByAreaNum(string BusiType, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select count(1) as Counts,min(c.TypeName) as Name,b.AreaId from officedba.sellback as a ");
            sb.Append(" left join officedba.custinfo as b on a.CustId=b.Id ");
            sb.Append(" left join officedba.CodePublicType as c on b.AreaId=c.Id where a.BillStatus<>'1' and b.AreaId !=0 and b.AreaId is not null ");
            sb.Append(" and a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (BusiType != "")
            {
                sb.Append(" and a.BusiType=");
                sb.Append(BusiType);
            }

            if (BeginDate != "")
            {
                sb.Append("and a.BackDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }

            if (EndDate != "")
            {
                sb.Append("and a.BackDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by b.AreaId ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 退货走势数量分析
        /// </summary>
        /// <param name="FromType">销售来源</param>
        /// <param name="DeptId">所属部门</param>
        /// <param name="EmployeeId">部门人员</param>
        /// <param name="DateType">时间分类</param>
        /// <param name="State">销售订单状态</param>
        /// <param name="BusiType">销售订单类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetBackByTrendNum(string GroupType, string DeptOrEmployeeId, string DateType, string State, string BusiType, string BeginDate, string EndDate, string AreaId)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string SearchField = "";
            if (DateType == "1")
            {
                SearchField = "dateName(year,a.BackDate)+'年'";
            }
            else if (DateType == "2")
            {
                SearchField = "dateName(year,a.BackDate)+'年-'+dateName(month,a.BackDate)+'月'";
            }
            else
            {
                SearchField = "dateName(year,a.BackDate)+'年-'+dateName(week,a.BackDate)+'周'";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append(SearchField);
            sb.Append(" as Name,count(1) as Counts ");
            sb.Append(" from officedba. sellback as a ");
            sb.Append(" left join officedba.custinfo as b on a.CustId=b.Id ");
            sb.Append(" where a.BillStatus<>'1' and a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (State != "")
            {
                sb.Append(" and a.BillStatus=");
                sb.Append(State);
            }
            if (BusiType != "")
            {
                sb.Append(" and a.BusiType=");
                sb.Append(BusiType);
            }

            if (DeptOrEmployeeId != "")
            {
                if (GroupType == "Dept")
                {
                    sb.Append("and a.SellDeptId in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(")");
                }
                else
                {
                    sb.Append("and a.Seller in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(")");
                }
            }
            if (AreaId != "" && AreaId != "0")
            {
                sb.Append(" and b.AreaId=");
                sb.Append(AreaId);
            }
            if (BeginDate != "")
            {
                sb.Append(" and a.BackDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append(" and a.BackDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by  ");
            sb.Append(SearchField);
            return SqlHelper.ExecuteSql(sb.ToString());
        }


        /// <summary>
        /// 退货金额部门分布
        /// </summary>
        /// <param name="Type">退货单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetBackByDeptPrice(string Type, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(b.DeptName) as Name,sum(isnull(RealTotal,0)*isnull(rate,1)) as Counts,a.selldeptId as DeptId  ");
            sb.Append(" from officedba.sellback as a left join officedba.DeptInfo as b on a.sellDeptId=b.Id  ");
            sb.Append(" where a.BillStatus<>'1' and a.selldeptId is not null and a.selldeptId !='' and a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (Type != "")
            {
                sb.Append("and a.BusiType=");
                sb.Append(Type);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.BackDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.BackDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }

            sb.Append(" group by a.selldeptId ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 退货金额人员分布
        /// </summary>
        /// <param name="Type">订单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetBackByPersonPrice(string Type, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(b.EmployeeName) as Name,sum(isnull(RealTotal,0)*isnull(rate,1)) as Counts,a.Seller ");
            sb.Append(" from officedba.sellback as a left join officedba.EmployeeInfo as b on a.Seller=b.Id  ");
            sb.Append(" where a.BillStatus<>'1' and a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (Type != "")
            {
                sb.Append("and a.BusiType=");
                sb.Append(Type);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.BackDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.BackDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by a.Seller ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }



        /// <summary>
        /// 退货金额区域分布
        /// </summary>
        /// <param name="BusiType">分类</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetBackByAreaPrice(string BusiType, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select sum(isnull(RealTotal,0)*isnull(rate,1)) as Counts,min(c.TypeName) as Name,b.AreaId from officedba.sellback as a ");
            sb.Append(" left join officedba.custinfo as b on a.CustId=b.Id ");
            sb.Append(" left join officedba.CodePublicType as c on b.AreaId=c.Id where a.BillStatus<>'1' and b.AreaId !=0 and b.AreaId is not null ");
            sb.Append(" and a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (BusiType != "")
            {
                sb.Append(" and a.BusiType=");
                sb.Append(BusiType);
            }

            if (BeginDate != "")
            {
                sb.Append("and a.BackDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }

            if (EndDate != "")
            {
                sb.Append("and a.BackDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by b.AreaId ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 退货走势金额分析
        /// </summary>
        /// <param name="FromType">销售来源</param>
        /// <param name="DeptId">所属部门</param>
        /// <param name="EmployeeId">部门人员</param>
        /// <param name="DateType">时间分类</param>
        /// <param name="State">销售订单状态</param>
        /// <param name="BusiType">销售订单类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetBackByTrendPrice(string GroupType, string DeptOrEmployeeId, string DateType, string State, string BusiType, string BeginDate, string EndDate, string AreaId)
        {
            string strCompanyCD = string.Empty;
            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string SearchField = "";
            if (DateType == "1")
            {
                SearchField = "dateName(year,a.BackDate)+'年'";
            }
            else if (DateType == "2")
            {
                SearchField = "dateName(year,a.BackDate)+'年-'+dateName(month,a.BackDate)+'月'";
            }
            else
            {
                SearchField = "dateName(year,a.BackDate)+'年-'+dateName(week,a.BackDate)+'周'";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append(SearchField);
            sb.Append(" as Name, Convert(decimal(22," + jingdu + ") ,sum(isnull(RealTotal,0)*isnull(rate,1))) as Counts ");
            sb.Append(" from officedba. sellback as a ");
            sb.Append(" left join officedba.custinfo as b on a.CustId=b.Id ");
            sb.Append(" where a.BillStatus<>'1' and  a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (State != "")
            {
                sb.Append(" and a.BillStatus=");
                sb.Append(State);
            }
            if (BusiType != "")
            {
                sb.Append(" and a.BusiType=");
                sb.Append(BusiType);
            }

            if (DeptOrEmployeeId != "")
            {
                if (GroupType == "Dept")
                {
                    sb.Append("and a.SellDeptId in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(")");
                }
                else
                {
                    sb.Append("and a.Seller in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(")");
                }
            }
            if (AreaId != "" && AreaId != "0")
            {
                sb.Append(" and b.AreaId=");
                sb.Append(AreaId);
            }
            if (BeginDate != "")
            {
                sb.Append(" and a.BackDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append(" and a.BackDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by  ");
            sb.Append(SearchField);
            return SqlHelper.ExecuteSql(sb.ToString());
        }


        /// <summary>
        /// 图表明细列表 
        /// </summary>
        public static DataTable GetSellBackDetail(string GroupType, string DeptOrEmployeeId, string DateType, string DateValue, string State, string BusiType, string BeginDate, string EndDate, string AreaId, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            strSql += "SELECT s.ID, s.BackNo, s.Title,s.ModifiedDate, CONVERT(varchar(100), s.BackDate, 23) AS BackDate,isnull(s.RealTotal,0)*isnull(s.rate,1) TotalPrice, c.CustName, e.EmployeeName,       ";
            strSql += "CASE s.FromType WHEN 0 THEN '无来源' WHEN 1 THEN '销售发货单' END AS FromTypeText, ";
            strSql += "CASE s.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更' WHEN 4 THEN ";
            strSql += "'手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText, CASE WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) IS NULL THEN '' WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) = 1 THEN '待审批' WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) = 2 THEN '审批中' WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) = 3 THEN '审批通过' WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) = 4 THEN '审批不通过' WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) = 5 THEN '撤销审批' END AS FlowInstanceText, isnull(CASE ";
            strSql += "((SELECT count(1) FROM officedba.StorageInOther AS soo ";
            strSql += "WHERE soo.FromType = '1' AND soo.FromBillID = s.ID)+ ";
            strSql += "(SELECT count(1)                                                          ";
            strSql += "FROM officedba.BlendingDetails                                                    ";
            strSql += "WHERE BillingType = '3' AND BillCD = s.BackNo and CompanyCD=s.CompanyCD) ";
            strSql += " ) WHEN 0 THEN '无引用' END, '被引用') AS RefText, ";
            strSql += "(SELECT TOP 1 FlowStatus FROM officedba.FlowInstance ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) AS FlowStatus ";
            strSql += "FROM officedba.SellBack AS s LEFT JOIN ";
            strSql += "officedba.CustInfo AS c ON s.CustID = c.ID LEFT JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID  where 1=1 and s.CompanyCD=@CompanyCD";

            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (State != "")
            {
                strSql += " and s.BillStatus=";
                strSql += State;
            }
            if (BusiType != "")
            {
                strSql += " and s.BusiType=";
                strSql += BusiType;
            }

            if (DeptOrEmployeeId != "")
            {
                if (GroupType == "Dept")
                {
                    strSql += "and s.SellDeptId in (";
                    strSql += DeptOrEmployeeId;
                    strSql += ")";
                }
                else
                {
                    strSql += "and s.Seller in (";
                    strSql += DeptOrEmployeeId;
                    strSql += ")";
                }
            }
            if (AreaId != "" && AreaId != "0")
            {
                strSql += " and c.AreaId=";
                strSql += AreaId;
            }
            if (BeginDate != "")
            {
                strSql += "and s.BackDate>=Convert(datetime,'";
                strSql += BeginDate;
                strSql += "')";

            }
            if (EndDate != "")
            {
                strSql += "and s.BackDate< DATEADD(day,1,Convert(datetime,'";
                strSql += EndDate;
                strSql += "'))";
            }
            if (DateValue != "")
            {

                if (DateType == "1")
                {
                    strSql += "and (dateName(year,s.BackDate)+'年')='";
                    strSql += DateValue;
                    strSql += "'";
                }
                else if (DateType == "2")
                {
                    strSql += "and (dateName(year,s.BackDate)+'年-'+dateName(month,s.BackDate)+'月')='";
                    strSql += DateValue;
                    strSql += "'";
                }
                else
                {
                    strSql += "and (dateName(year,s.BackDate)+'年-'+dateName(week,s.BackDate)+'周')='";
                    strSql += DateValue;
                    strSql += "'";
                }
            }

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 图表明细列表 
        /// </summary>
        public static DataTable GetSellBackDetail(string GroupType, string DeptOrEmployeeId, string DateType, string DateValue, string State, string BusiType, string BeginDate, string EndDate, string AreaId)
        {

            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            string strSql = string.Empty;
            strSql += "SELECT s.ID, s.BackNo,s.Seller,s.SellDeptId,c.AreaId,isnull(s.Title,'') as Title ,s.ModifiedDate, CONVERT(varchar(100), s.BackDate, 23) AS BackDate, Convert(decimal(22," + jingdu + "),isnull(s.RealTotal,0)*isnull(s.rate,1) ) as TotalPrice, isnull(c.CustName,'') as CustName , isnull(e.EmployeeName,'') as EmployeeName,       ";
            strSql += "dateName(year,s.BackDate)+'年' as BackYear,";
            strSql += "dateName(year,s.BackDate)+'年-'+dateName(month,s.BackDate)+'月' as BackMonth,";
            strSql += "dateName(year,s.BackDate)+'年-'+dateName(week,s.BackDate)+'周' as BackWeek,";

            strSql += "CASE s.FromType WHEN 0 THEN '无来源' WHEN 1 THEN '销售发货单' END AS FromTypeText, ";
            strSql += "CASE s.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更' WHEN 4 THEN ";
            strSql += "'手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText, CASE WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) IS NULL THEN '' WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) = 1 THEN '待审批' WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) = 2 THEN '审批中' WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) = 3 THEN '审批通过' WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) = 4 THEN '审批不通过' WHEN (SELECT TOP 1 FlowStatus ";
            strSql += "FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) = 5 THEN '撤销审批' END AS FlowInstanceText, isnull(CASE ";
            strSql += "((SELECT count(1) FROM officedba.StorageInOther AS soo ";
            strSql += "WHERE soo.FromType = '1' AND soo.FromBillID = s.ID)+ ";
            strSql += "(SELECT count(1)                                                          ";
            strSql += "FROM officedba.BlendingDetails                                                    ";
            strSql += "WHERE BillingType = '3' AND BillCD = s.BackNo and CompanyCD=s.CompanyCD) ";
            strSql += " ) WHEN 0 THEN '无引用' END, '被引用') AS RefText, ";
            strSql += "(SELECT TOP 1 FlowStatus FROM officedba.FlowInstance ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 5 ";
            strSql += "ORDER BY ModifiedDate DESC) AS FlowStatus ";
            strSql += "FROM officedba.SellBack AS s LEFT JOIN ";
            strSql += "officedba.CustInfo AS c ON s.CustID = c.ID LEFT JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID  where 1=1 and s.CompanyCD=@CompanyCD";

            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (State != "")
            {
                strSql += " and s.BillStatus=";
                strSql += State;
            }
            if (BusiType != "")
            {
                strSql += " and s.BusiType=";
                strSql += BusiType;
            }

            if (DeptOrEmployeeId != "")
            {
                if (GroupType == "Dept")
                {
                    strSql += "and s.SellDeptId in (";
                    strSql += DeptOrEmployeeId;
                    strSql += ")";
                }
                else
                {
                    strSql += "and s.Seller in (";
                    strSql += DeptOrEmployeeId;
                    strSql += ")";
                }
            }
            if (AreaId != "" && AreaId != "0")
            {
                strSql += " and c.AreaId=";
                strSql += AreaId;
            }
            if (BeginDate != "")
            {
                strSql += "and s.BackDate>=Convert(datetime,'";
                strSql += BeginDate;
                strSql += "')";

            }
            if (EndDate != "")
            {
                strSql += "and s.BackDate< DATEADD(day,1,Convert(datetime,'";
                strSql += EndDate;
                strSql += "'))";
            }
            if (DateValue != "")
            {

                if (DateType == "1")
                {
                    strSql += "and (dateName(year,s.BackDate)+'年')='";
                    strSql += DateValue;
                    strSql += "'";
                }
                else if (DateType == "2")
                {
                    strSql += "and (dateName(year,s.BackDate)+'年-'+dateName(month,s.BackDate)+'月')='";
                    strSql += DateValue;
                    strSql += "'";
                }
                else
                {
                    strSql += "and (dateName(year,s.BackDate)+'年-'+dateName(week,s.BackDate)+'周')='";
                    strSql += DateValue;
                    strSql += "'";
                }
            }

            return SqlHelper.ExecuteSql(strSql.ToString(), arr);
        }

        #endregion
    }
}

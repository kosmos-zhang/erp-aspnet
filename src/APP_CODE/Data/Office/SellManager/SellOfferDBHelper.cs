/***********************************************************************
 * 
 * Module Name:XBase.Data.Office.SystemManager.{.cs
 * Current Version: 1.0 
 * Creator: 周军
 * Auditor:2009-01-13
 * End Date:
 * Description: 销售报价数据库层处理
 * Version History:
 ***********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using XBase.Model.Office.SellManager;
using XBase.Common;
using XBase.Data.Common;

namespace XBase.Data.Office.SellManager
{
    public class SellOfferDBHelper
    {
        #region 添加、修改、删除相关操作
        /// <summary>
        /// 添加销售报价单
        /// </summary>
        /// <param name="sellChanceModel">销售机会表实体</param>
        /// <param name="sellChancePushModel">销售阶段表实体</param>
        /// <returns>是否添加成功</returns>
        public static bool InsertOrder(Hashtable ht, SellOfferModel sellOfferModel, List<SellOfferDetailModel> SellOrderDetailModelList,
            List<SellOfferHistoryModel> sellOfferHistoryModelList, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            //判断单据编号是否存在
            if (NoIsExist(sellOfferModel.OfferNo))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    InsertSellOffer(sellOfferModel, tran);

                    //拓展属性
                    GetExtAttrCmd(sellOfferModel, ht, tran);

                    InsertSellOfferDetail(sellOfferModel, SellOrderDetailModelList, tran);
                    InsertSellOfferHistory(sellOfferModel, sellOfferHistoryModelList, tran);
                    tran.Commit();
                    isSucc = true;
                    strMsg = "保存成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    isSucc = false;
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
        /// 修改销售报价单
        /// </summary>
        /// <param name="sellChanceModel">销售机会表实体</param>
        /// <param name="sellChancePushModel">销售阶段表实体</param>
        /// <returns>是否添加成功</returns>
        public static bool UpdateOrder(Hashtable ht,SellOfferModel sellOfferModel, List<SellOfferDetailModel> SellOrderDetailModelList,
            List<SellOfferHistoryModel> sellOfferHistoryModelList, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            if (IsUpdate(sellOfferModel.OfferNo))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    UpdateSellOffer(sellOfferModel, tran);

                    //拓展属性
                    GetExtAttrCmd(sellOfferModel, ht, tran);

                    InsertSellOfferDetail(sellOfferModel, SellOrderDetailModelList, tran);
                    InsertSellOfferHistory(sellOfferModel, sellOfferHistoryModelList, tran);
                    tran.Commit();
                    isSucc = true;
                    strMsg = "保存成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    isSucc = false;
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

        /// <summary>
        /// 添加销售报价单主表信息
        /// </summary>
        /// <param name="sellOfferModel"></param>
        private static void InsertSellOffer(SellOfferModel sellOfferModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SellOffer(");
            strSql.Append("CompanyCD,OfferNo,CustID,CustTel,FromType,FromBillID,Title,Seller,SellDeptId,SellType,BusiType,PayType,MoneyType,CarryType,TakeType,ExpireDate,TotalPrice,TotalTax,TotalFee,Discount,DiscountTotal,RealTotal,isAddTax,CountTotal,CurrencyType,Rate,QuoteTime,OfferDate,PayRemark,DeliverRemark,PackTransit,Remark,BillStatus,Creator,CreateDate,ModifiedDate,ModifiedUserID,CanViewUser)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@OfferNo,@CustID,@CustTel,@FromType,@FromBillID,@Title,@Seller,@SellDeptId,@SellType,@BusiType,@PayType,@MoneyType,@CarryType,@TakeType,@ExpireDate,@TotalPrice,@TotalTax,@TotalFee,@Discount,@DiscountTotal,@RealTotal,@isAddTax,@CountTotal,@CurrencyType,@Rate,@QuoteTime,@OfferDate,@PayRemark,@DeliverRemark,@PackTransit,@Remark,@BillStatus,@Creator,getdate(),getdate(),@ModifiedUserID,@CanViewUser)");
            #region 参数
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@OfferNo", SqlDbType.VarChar,50),
					new SqlParameter("@CustID", SqlDbType.Int,4),
					new SqlParameter("@CustTel", SqlDbType.VarChar,100),
					new SqlParameter("@FromType", SqlDbType.Char,1),
					new SqlParameter("@FromBillID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@Seller", SqlDbType.Int,4),
					new SqlParameter("@SellDeptId", SqlDbType.Int,4),
					new SqlParameter("@SellType", SqlDbType.Int,4),
					new SqlParameter("@BusiType", SqlDbType.Char,1),
					new SqlParameter("@PayType", SqlDbType.Int,4),
					new SqlParameter("@MoneyType", SqlDbType.Int,4),
					new SqlParameter("@CarryType", SqlDbType.Int,4),
					new SqlParameter("@TakeType", SqlDbType.Int,4),
					new SqlParameter("@ExpireDate", SqlDbType.DateTime),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@TotalTax", SqlDbType.Decimal,9),
					new SqlParameter("@TotalFee", SqlDbType.Decimal,9),
					new SqlParameter("@Discount", SqlDbType.Decimal,9),
					new SqlParameter("@DiscountTotal", SqlDbType.Decimal,9),
					new SqlParameter("@RealTotal", SqlDbType.Decimal,9),
					new SqlParameter("@isAddTax", SqlDbType.Char,1),
					new SqlParameter("@CountTotal", SqlDbType.Decimal,9),
					new SqlParameter("@CurrencyType", SqlDbType.Int,4),
					new SqlParameter("@Rate", SqlDbType.Decimal,9),
					new SqlParameter("@QuoteTime", SqlDbType.Int,4),
					new SqlParameter("@OfferDate", SqlDbType.DateTime),
					new SqlParameter("@PayRemark", SqlDbType.VarChar,200),
					new SqlParameter("@DeliverRemark", SqlDbType.VarChar,200),
					new SqlParameter("@PackTransit", SqlDbType.VarChar,200),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20),
					new SqlParameter("@CanViewUser", SqlDbType.VarChar,2048)};
            parameters[0].Value = sellOfferModel.CompanyCD;
            parameters[1].Value = sellOfferModel.OfferNo;
            parameters[2].Value = sellOfferModel.CustID;
            parameters[3].Value = sellOfferModel.CustTel;
            parameters[4].Value = sellOfferModel.FromType;
            parameters[5].Value = sellOfferModel.FromBillID;
            parameters[6].Value = sellOfferModel.Title;
            parameters[7].Value = sellOfferModel.Seller;
            parameters[8].Value = sellOfferModel.SellDeptId;
            parameters[9].Value = sellOfferModel.SellType;
            parameters[10].Value = sellOfferModel.BusiType;
            parameters[11].Value = sellOfferModel.PayType;
            parameters[12].Value = sellOfferModel.MoneyType;
            parameters[13].Value = sellOfferModel.CarryType;
            parameters[14].Value = sellOfferModel.TakeType;
            parameters[15].Value = sellOfferModel.ExpireDate;
            parameters[16].Value = sellOfferModel.TotalPrice;
            parameters[17].Value = sellOfferModel.TotalTax;
            parameters[18].Value = sellOfferModel.TotalFee;
            parameters[19].Value = sellOfferModel.Discount;
            parameters[20].Value = sellOfferModel.DiscountTotal;
            parameters[21].Value = sellOfferModel.RealTotal;
            parameters[22].Value = sellOfferModel.isAddTax;
            parameters[23].Value = sellOfferModel.CountTotal;
            parameters[24].Value = sellOfferModel.CurrencyType;
            parameters[25].Value = sellOfferModel.Rate;
            parameters[26].Value = sellOfferModel.QuoteTime;
            parameters[27].Value = sellOfferModel.OfferDate;
            parameters[28].Value = sellOfferModel.PayRemark;
            parameters[29].Value = sellOfferModel.DeliverRemark;
            parameters[30].Value = sellOfferModel.PackTransit;
            parameters[31].Value = sellOfferModel.Remark;
            parameters[32].Value = sellOfferModel.BillStatus;
            parameters[33].Value = sellOfferModel.Creator;
            parameters[34].Value = sellOfferModel.ModifiedUserID;
            parameters[35].Value = sellOfferModel.CanViewUser;
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

        /// <summary>
        /// 修改销售报价单主表信息
        /// </summary>
        /// <param name="sellOfferModel"></param>
        /// <param name="tran"></param>
        private static void UpdateSellOffer(SellOfferModel sellOfferModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SellOffer set ");
            strSql.Append("CustID=@CustID,");
            strSql.Append("CustTel=@CustTel,");
            strSql.Append("FromType=@FromType,");
            strSql.Append("FromBillID=@FromBillID,");
            strSql.Append("Title=@Title,");
            strSql.Append("Seller=@Seller,");
            strSql.Append("SellDeptId=@SellDeptId,");
            strSql.Append("SellType=@SellType,");
            strSql.Append("BusiType=@BusiType,");
            strSql.Append("PayType=@PayType,");
            strSql.Append("MoneyType=@MoneyType,");
            strSql.Append("CarryType=@CarryType,");
            strSql.Append("TakeType=@TakeType,");
            strSql.Append("ExpireDate=@ExpireDate,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("TotalTax=@TotalTax,");
            strSql.Append("TotalFee=@TotalFee,");
            strSql.Append("Discount=@Discount,");
            strSql.Append("DiscountTotal=@DiscountTotal,");
            strSql.Append("RealTotal=@RealTotal,");
            strSql.Append("isAddTax=@isAddTax,");
            strSql.Append("CountTotal=@CountTotal,");
            strSql.Append("CurrencyType=@CurrencyType,");
            strSql.Append("Rate=@Rate,");
            strSql.Append("QuoteTime=@QuoteTime,");
            strSql.Append("OfferDate=@OfferDate,");
            strSql.Append("PayRemark=@PayRemark,");
            strSql.Append("DeliverRemark=@DeliverRemark,");
            strSql.Append("PackTransit=@PackTransit,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("BillStatus=@BillStatus,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(",CanViewUser=@CanViewUser ");
            strSql.Append(" where CompanyCD=@CompanyCD and OfferNo=@OfferNo ");
            #region
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@OfferNo", SqlDbType.VarChar,50),
					new SqlParameter("@CustID", SqlDbType.Int,4),
					new SqlParameter("@CustTel", SqlDbType.VarChar,100),
					new SqlParameter("@FromType", SqlDbType.Char,1),
					new SqlParameter("@FromBillID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@Seller", SqlDbType.Int,4),
					new SqlParameter("@SellDeptId", SqlDbType.Int,4),
					new SqlParameter("@SellType", SqlDbType.Int,4),
					new SqlParameter("@BusiType", SqlDbType.Char,1),
					new SqlParameter("@PayType", SqlDbType.Int,4),
					new SqlParameter("@MoneyType", SqlDbType.Int,4),
					new SqlParameter("@CarryType", SqlDbType.Int,4),
					new SqlParameter("@TakeType", SqlDbType.Int,4),
					new SqlParameter("@ExpireDate", SqlDbType.DateTime),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@TotalTax", SqlDbType.Decimal,9),
					new SqlParameter("@TotalFee", SqlDbType.Decimal,9),
					new SqlParameter("@Discount", SqlDbType.Decimal,9),
					new SqlParameter("@DiscountTotal", SqlDbType.Decimal,9),
					new SqlParameter("@RealTotal", SqlDbType.Decimal,9),
					new SqlParameter("@isAddTax", SqlDbType.Char,1),
					new SqlParameter("@CountTotal", SqlDbType.Decimal,9),
					new SqlParameter("@CurrencyType", SqlDbType.Int,4),
					new SqlParameter("@Rate", SqlDbType.Decimal,9),
					new SqlParameter("@QuoteTime", SqlDbType.Int,4),
					new SqlParameter("@OfferDate", SqlDbType.DateTime),
					new SqlParameter("@PayRemark", SqlDbType.VarChar,200),
					new SqlParameter("@DeliverRemark", SqlDbType.VarChar,200),
					new SqlParameter("@PackTransit", SqlDbType.VarChar,200),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20),
					new SqlParameter("@CanViewUser", SqlDbType.VarChar,2048)};
            parameters[0].Value = sellOfferModel.CompanyCD;
            parameters[1].Value = sellOfferModel.OfferNo;
            parameters[2].Value = sellOfferModel.CustID;
            parameters[3].Value = sellOfferModel.CustTel;
            parameters[4].Value = sellOfferModel.FromType;
            parameters[5].Value = sellOfferModel.FromBillID;
            parameters[6].Value = sellOfferModel.Title;
            parameters[7].Value = sellOfferModel.Seller;
            parameters[8].Value = sellOfferModel.SellDeptId;
            parameters[9].Value = sellOfferModel.SellType;
            parameters[10].Value = sellOfferModel.BusiType;
            parameters[11].Value = sellOfferModel.PayType;
            parameters[12].Value = sellOfferModel.MoneyType;
            parameters[13].Value = sellOfferModel.CarryType;
            parameters[14].Value = sellOfferModel.TakeType;
            parameters[15].Value = sellOfferModel.ExpireDate;
            parameters[16].Value = sellOfferModel.TotalPrice;
            parameters[17].Value = sellOfferModel.TotalTax;
            parameters[18].Value = sellOfferModel.TotalFee;
            parameters[19].Value = sellOfferModel.Discount;
            parameters[20].Value = sellOfferModel.DiscountTotal;
            parameters[21].Value = sellOfferModel.RealTotal;
            parameters[22].Value = sellOfferModel.isAddTax;
            parameters[23].Value = sellOfferModel.CountTotal;
            parameters[24].Value = sellOfferModel.CurrencyType;
            parameters[25].Value = sellOfferModel.Rate;
            parameters[26].Value = sellOfferModel.QuoteTime;
            parameters[27].Value = sellOfferModel.OfferDate;
            parameters[28].Value = sellOfferModel.PayRemark;
            parameters[29].Value = sellOfferModel.DeliverRemark;
            parameters[30].Value = sellOfferModel.PackTransit;
            parameters[31].Value = sellOfferModel.Remark;
            parameters[32].Value = sellOfferModel.BillStatus;
            parameters[33].Value = sellOfferModel.ModifiedUserID;
            parameters[34].Value = sellOfferModel.CanViewUser;
            foreach (SqlParameter para in parameters)
            {
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }
            #endregion
            //如果单据状态变为变更状态，回写订单中已执行数量
            if (sellOfferModel.BillStatus == "3")
            {
                SqlParameter[] p = { new SqlParameter("@CompanyCD", sellOfferModel.CompanyCD), new SqlParameter("@OfferNo", sellOfferModel.OfferNo) };
                string str = SqlHelper.ExecuteScalar("select BillStatus from officedba.SellOffer where CompanyCD=@CompanyCD and OfferNo=@OfferNo ", p).ToString();
                if (str == "2")
                {
                    UpdateSellChance(2, sellOfferModel.OfferNo, tran);
                }
            }
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 添加销售报价单子表信息
        /// </summary>
        /// <param name="sellOfferModel"></param>
        private static void InsertSellOfferDetail(SellOfferModel sellOfferModel, List<SellOfferDetailModel> SellOrderDetailModelList, TransactionManager tran)
        {

            string strSqlDel = "delete from officedba.SellOfferDetail where  OfferNo=@OfferNo  and CompanyCD=@CompanyCD ";
            SqlParameter[] paras = { new SqlParameter("@OfferNo", sellOfferModel.OfferNo), new SqlParameter("@CompanyCD", sellOfferModel.CompanyCD) };
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSqlDel.ToString(), paras);

            foreach (SellOfferDetailModel sellOfferDetailModel in SellOrderDetailModelList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into officedba.SellOfferDetail(");
                strSql.Append("CompanyCD,OfferNo,SortNo,ProductID,ProductCount,UnitID,UnitPrice,TaxPrice,Discount,TaxRate,TotalFee,TotalPrice,TotalTax,SendTime,Package,Remark,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate)");
                strSql.Append(" values (");
                strSql.Append("@CompanyCD,@OfferNo,@SortNo,@ProductID,@ProductCount,@UnitID,@UnitPrice,@TaxPrice,@Discount,@TaxRate,@TotalFee,@TotalPrice,@TotalTax,@SendTime,@Package,@Remark,getdate(),@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate)");
                #region 参数
                SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@OfferNo", SqlDbType.VarChar,50),
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
					new SqlParameter("@SendTime", SqlDbType.Int,4),
					new SqlParameter("@Package", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.VarChar,200),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20),
					new SqlParameter("@UsedUnitID", SqlDbType.Int,4),
					new SqlParameter("@UsedUnitCount", SqlDbType.Decimal,9),
					new SqlParameter("@UsedPrice", SqlDbType.Decimal,9),
					new SqlParameter("@ExRate", SqlDbType.Decimal,9)};
                parameters[0].Value = sellOfferDetailModel.CompanyCD;
                parameters[1].Value = sellOfferDetailModel.OfferNo;
                parameters[2].Value = sellOfferDetailModel.SortNo;
                parameters[3].Value = sellOfferDetailModel.ProductID;
                parameters[4].Value = sellOfferDetailModel.ProductCount;
                parameters[5].Value = sellOfferDetailModel.UnitID;
                parameters[6].Value = sellOfferDetailModel.UnitPrice;
                parameters[7].Value = sellOfferDetailModel.TaxPrice;
                parameters[8].Value = sellOfferDetailModel.Discount;
                parameters[9].Value = sellOfferDetailModel.TaxRate;
                parameters[10].Value = sellOfferDetailModel.TotalFee;
                parameters[11].Value = sellOfferDetailModel.TotalPrice;
                parameters[12].Value = sellOfferDetailModel.TotalTax;
                parameters[13].Value = sellOfferDetailModel.SendTime;
                parameters[14].Value = sellOfferDetailModel.Package;
                parameters[15].Value = sellOfferDetailModel.Remark;
                parameters[16].Value = sellOfferDetailModel.ModifiedUserID;
                parameters[17].Value = sellOfferDetailModel.UsedUnitID;
                parameters[18].Value = sellOfferDetailModel.UsedUnitCount;
                parameters[19].Value = sellOfferDetailModel.UsedPrice;
                parameters[20].Value = sellOfferDetailModel.ExRate;
                //@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate
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

        /// <summary>
        /// 添加销售报价记录
        /// </summary>
        /// <param name="sellOfferModel"></param>
        private static void InsertSellOfferHistory(SellOfferModel sellOfferModel, List<SellOfferHistoryModel> sellOfferHistoryModelList, TransactionManager tran)
        {
            if (sellOfferHistoryModelList.Count > 0)
            {
                string strSqlDel = "delete from officedba.SellOfferHistory where  OfferNo=@OfferNo  and CompanyCD=@CompanyCD and OfferTime=@OfferTime";
                SqlParameter[] paras = { new SqlParameter("@OfferNo", sellOfferHistoryModelList[0].OfferNo),
                                         new SqlParameter("@CompanyCD", sellOfferHistoryModelList[0].CompanyCD) ,
                                       new SqlParameter("@OfferTime",sellOfferHistoryModelList[0].OfferTime)};
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSqlDel.ToString(), paras);
            }
            foreach (SellOfferHistoryModel sellOfferHistoryModel in sellOfferHistoryModelList)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into officedba.SellOfferHistory(");
                strSql.Append("CompanyCD,OfferNo,ProductID,OfferDate,OfferTime,Seller,TotalPrice)");
                strSql.Append(" values (");
                strSql.Append("@CompanyCD,@OfferNo,@ProductID,getdate(),@OfferTime,@Seller,@TotalPrice)");
                #region 参数
                SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@OfferNo", SqlDbType.VarChar,50),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@OfferTime", SqlDbType.Int,4),
					new SqlParameter("@Seller", SqlDbType.Int,4),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9)};
                parameters[0].Value = sellOfferHistoryModel.CompanyCD;
                parameters[1].Value = sellOfferHistoryModel.OfferNo;
                parameters[2].Value = sellOfferHistoryModel.ProductID;
                parameters[3].Value = sellOfferHistoryModel.OfferTime;
                parameters[4].Value = sellOfferHistoryModel.Seller;
                parameters[5].Value = sellOfferHistoryModel.TotalPrice;
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
           

            sql = " select ID from officedba.SellOffer where CompanyCD=@CompanyCD and OfferNo=@OfferNo ";
            SqlParameter[] paras = { new SqlParameter("@CompanyCD", strCompanyCD), new SqlParameter("@OfferNo", orderNo) };
            OrderID = (int)SqlHelper.ExecuteScalar(sql, paras);
            return OrderID;
        }

        /// <summary>
        /// 删除销售机会
        /// </summary>
        /// <param name="orderNos">单据编号列表</param>
        /// <param name="strMsg">返回的信息</param>
        /// <param name="strFieldText">提示的字段名</param>
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
                    strMsg += "已提交审批或已确认后的单据不允许删除 ！|";
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
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellOffer WHERE OfferNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellOfferDetail WHERE OfferNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellOfferHistory WHERE OfferNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    tran.Commit();
                    isSucc = true;
                    strMsg = "删除成功！";

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    isSucc = false;
                    strMsg = "删除失败，请联系系统管理员！";
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


        #region 获取报价信息相关操作

        /// <summary>
        /// 获取报价单列表 
        /// </summary>
        /// <param name="sellOfferModel">sellOfferModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(string EFIndex, string EFDesc, SellOfferModel sellOfferModel, int? FlowStatus, DateTime? dt, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            strSql = "select * from ( SELECT so.ID,so.OfferNo, so.Title, so.TotalFee,so.ModifiedDate, isnull( e.EmployeeName,'') as EmployeeName,                 ";
            strSql += "CONVERT(varchar(100),so.OfferDate, 23) AS OfferDate , so.QuoteTime, isnull(sc.ChanceNo,'') as ChanceNo,    ";
            strSql += "isnull(case ((select count(1) from  officedba.SellContract where FromType='1' and FromBillID=so.ID)+       ";
            strSql += "(select count(1) from  officedba.SellOrder where FromType='1' and FromBillID=so.ID))                       ";
            strSql += "when 0 then '无引用'   end , '被引用')as RefText,                                                          ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                   ";
            strSql += "FROM officedba.FlowInstance                                                                                ";
            strSql += "WHERE BillID = so.ID AND BillTypeFlag = 5 AND BillTypeCode = 1                                             ";
            strSql += "ORDER BY ModifiedDate DESC) AS FlowStatus  ,                                                               ";
            strSql += "CASE so.FromType WHEN  0 THEN '无来源' WHEN 1 THEN '销售机会' END AS FromTypeText,                         ";
            strSql += "CASE so.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3                                            ";
            strSql += "THEN '变更' WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText,                           ";
            strSql += "CASE WHEN (SELECT TOP 1 FlowStatus                                                                         ";
            strSql += "FROM officedba.FlowInstance                                                                                ";
            strSql += "WHERE BillID = so.ID AND BillTypeFlag = 5 AND BillTypeCode = 1                                             ";
            strSql += "ORDER BY ModifiedDate DESC) IS NULL THEN '' WHEN                                                     ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                   ";
            strSql += "FROM officedba.FlowInstance                                                                                ";
            strSql += "WHERE BillID = so.ID AND BillTypeFlag = 5 AND BillTypeCode = 1                                             ";
            strSql += "ORDER BY ModifiedDate DESC) = 1 THEN '待审批' WHEN                                                         ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                   ";
            strSql += "FROM officedba.FlowInstance                                                                                ";
            strSql += "WHERE BillID = so.ID AND BillTypeFlag = 5 AND BillTypeCode = 1                                             ";
            strSql += "ORDER BY ModifiedDate DESC) = 2 THEN '审批中' WHEN                                                         ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                   ";
            strSql += "FROM officedba.FlowInstance                                                                                ";
            strSql += "WHERE BillID = so.ID AND BillTypeFlag = 5 AND BillTypeCode = 1                                             ";
            strSql += "ORDER BY ModifiedDate DESC) = 3 THEN '审批通过' WHEN                                                       ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                   ";
            strSql += "FROM officedba.FlowInstance                                                                                ";
            strSql += "WHERE BillID = so.ID AND BillTypeFlag = 5 AND BillTypeCode = 1                                             ";
            strSql += "ORDER BY ModifiedDate DESC) = 4 THEN '审批不通过' WHEN                                                     ";
            strSql += "(SELECT TOP 1 FlowStatus    ";
            strSql += "FROM officedba.FlowInstance                   ";
            strSql += "WHERE BillID = so.ID AND BillTypeFlag = 5 AND BillTypeCode = 1     ";
            strSql += "ORDER BY ModifiedDate DESC) = 5 THEN '撤销审批' END AS FlowInstanceText   ";
            strSql += "FROM officedba.SellOffer as so LEFT OUTER JOIN          ";
            strSql += "officedba.SellChance as sc on so.FromBillID=sc.ID LEFT OUTER JOIN    ";
            strSql += "officedba.EmployeeInfo as e ON so.Seller = e.ID     where 1=1 and so.CompanyCD=@CompanyCD  ";

            strSql += " and ( charindex('," + sellOfferModel.Creator + ",' , ','+so.CanViewUser+',')>0 or so.Creator=" + sellOfferModel.Creator + " OR so.CanViewUser='' OR so.CanViewUser is null) ";
           
            string strCompanyCD = string.Empty;//单位编号
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;           

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (sellOfferModel.BillStatus != null)
            {
                strSql += " and so.BillStatus= @BillStatus";
               
                arr.Add(new SqlParameter("@BillStatus", sellOfferModel.BillStatus));
            }
            if (sellOfferModel.FromBillID != null)
            {
                strSql += " and so.FromBillID=@FromBillID";
                arr.Add(new SqlParameter("@FromBillID", sellOfferModel.FromBillID)); ;
            }
            if (sellOfferModel.FromType != null)
            {
                strSql += " and so.FromType=@FromType";
                arr.Add(new SqlParameter("@FromType", sellOfferModel.FromType));
            }
            if (sellOfferModel.OfferDate != null)
            {
                strSql += " and so.OfferDate >= @OfferDate";
                arr.Add(new SqlParameter("@OfferDate", sellOfferModel.OfferDate));
            }
            if (dt != null)
            {
                strSql += " and so.OfferDate <= @OfferDate1";
                arr.Add(new SqlParameter("@OfferDate1", dt));
            }
            if (sellOfferModel.OfferNo != null)
            {
                strSql += " and so.OfferNo like @OfferNo";
                arr.Add(new SqlParameter("@OfferNo", "%" + sellOfferModel.OfferNo + "%"));
            }
            if (sellOfferModel.Seller != null)
            {
                strSql += " and so.Seller=@Seller";
                arr.Add(new SqlParameter("@Seller", sellOfferModel.Seller));
            }
            if (sellOfferModel.Title != null)
            {
                strSql += " and so.Title like @Title";
                arr.Add(new SqlParameter("@Title", "%" + sellOfferModel.Title + "%"));
            }
            if (sellOfferModel.CustID != null)
            {
                strSql += " and so.CustID=@CustID";
                arr.Add(new SqlParameter("@CustID", sellOfferModel.CustID));
            }

            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                strSql+= " and so.ExtField" + EFIndex + " LIKE @EFDesc";
                arr.Add(new SqlParameter("@EFDesc", "%" + EFDesc + "%"));
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
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
           
            string strSql = "SELECT so.ProductID, isnull(so.ProductCount,0) as ProductCount, so.UnitID,so.UnitPrice, ";
            strSql += "so.TaxPrice, so.Discount, so.TaxRate, so.TotalFee, so.TotalPrice, p.Specification, ";
            strSql += "so.TotalTax, so.SendTime, so.Package, so.Remark, ";
            strSql += " p.ProductName, c.CodeName,isnull(p.StandardCost,0) as StandardCost, p.SellTax, p.TaxRate AS TaxRate1 , p.ProdNo ";
            strSql += ",isnull(p.StandardSell,0) as StandardSell,so.UsedUnitID,isnull(so.UsedUnitCount,0) as UsedUnitCount,isnull(so.UsedPrice,0) as UsedPrice,isnull(so.ExRate,1) as ExRate ";
            strSql += "FROM officedba.SellOfferDetail AS so left JOIN ";
            strSql += "officedba.ProductInfo AS p ON so.ProductID = p.ID left JOIN ";
            strSql += "officedba.CodeUnitType AS c ON so.UnitID = c.ID ";
            strSql += "WHERE (so.OfferNo = @OfferNo) AND (so.CompanyCD = @CompanyCD) ";
            strSql += "ORDER BY so.SortNo ";

            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@OfferNo", orderNo);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }

        /// <summary>
        /// 获取报价记录
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataTable GetSellOfferHistory(string orderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = "SELECT soh.TotalPrice, soh.Seller, CONVERT(varchar(100), soh.OfferDate, 23) AS OfferDate,soh.OfferTime, e.EmployeeName, soh.OfferNo ";
            strSql += "FROM officedba.SellOfferHistory AS soh LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON soh.Seller = e.ID ";
            strSql += "WHERE (soh.OfferNo = @OfferNo ) AND (soh.CompanyCD = @CompanyCD) AND (soh.ProductID IS NULL) ";
            strSql += "ORDER BY soh.OfferTime  asc";

            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@OfferNo", orderNo);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }

        /// <summary>
        /// 获取报价记录详细信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="OfferTime"></param>
        /// <returns></returns>
        public static DataTable GetSellOfferHistoryDetail(string orderNo, int OfferTime)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = "SELECT s.OfferTime, CONVERT(varchar(100), s.OfferDate, 23) AS OfferDate, ";
            strSql += "s.TotalPrice, e.EmployeeName, p.ProductName ";
            strSql += "FROM officedba.SellOfferHistory AS s LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID LEFT OUTER JOIN ";
            strSql += "officedba.ProductInfo AS p ON s.ProductID = p.ID ";
            strSql += "WHERE (s.OfferNo = @OfferNo) AND (s.CompanyCD = @CompanyCD) ";
            strSql += "AND (s.ProductID IS NOT NULL) AND (s.OfferTime = @OfferTime) ";
            strSql += " ORDER BY s.ProductID ";


            SqlParameter[] paras = new SqlParameter[3];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@OfferNo", orderNo);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            paras[2] = new SqlParameter("@OfferTime", OfferTime);
            arr.Add(paras[2]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }

        /// <summary>
        /// 获取报价单主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = "SELECT s.OfferNo, s.CustID, s.CustTel, s.FromType, s.FromBillID, s.Title, s.Seller, ";
            strSql += "s.SellDeptId, s.SellType, s.BusiType, s.PayType, s.MoneyType, ";
            strSql += "s.CarryType, s.TakeType, CONVERT(varchar(100), s.ExpireDate, 23) AS ExpireDate, ";
            strSql += "s.TotalPrice, s.TotalTax, s.TotalFee, s.Discount, s.DiscountTotal, ";
            strSql += "s.RealTotal, s.CountTotal, s.isAddTax, s.CurrencyType, s.Rate, s.QuoteTime, ";
            strSql += "CONVERT(varchar(100), s.OfferDate, 23) AS OfferDate, s.PayRemark, ";
            strSql += "s.DeliverRemark, s.PackTransit, s.Remark, s.BillStatus, CONVERT(varchar(100), ";
            strSql += "s.CreateDate, 23) AS CreateDate, CONVERT(varchar(100), ";
            strSql += "s.ConfirmDate, 23) AS ConfirmDate, CONVERT(varchar(100), s.CloseDate, 23) AS CloseDate, ";
            strSql += "CONVERT(varchar(100), s.ModifiedDate, 23) ";
            strSql += "AS ModifiedDate, s.ModifiedUserID, e1.EmployeeName AS SellerName, ";
            strSql += "e2.EmployeeName AS CreatorName, e3.EmployeeName AS ConfirmorName, ";
            strSql += "d.DeptName, c.CurrencyName, sc.ChanceNo, e4.EmployeeName AS CloserName, ";
            strSql += "CASE s.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更' ";
            strSql += "WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText,ct.CustName, ";
            strSql += " s.ExtField1,s.ExtField2,s.ExtField3,s.ExtField4,s.ExtField5,s.ExtField6,s.ExtField7,s.ExtField8,s.ExtField9,s.ExtField10 ";
            strSql += ",s.CanViewUser,[dbo].[getEmployeeNameString](s.CanViewUser) as CanViewUserName ";
            strSql += "FROM officedba.SellOffer AS s LEFT OUTER JOIN officedba.CustInfo AS ct ON s.CustID = ct.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e1 ON s.Seller = e1.ID LEFT OUTER JOIN ";
            strSql += "officedba.DeptInfo AS d ON s.SellDeptId = d.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e2 ON s.Creator = e2.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e3 ON s.Confirmor = e3.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e4 ON s.Closer = e4.ID LEFT OUTER JOIN ";
            strSql += "officedba.CurrencyTypeSetting AS c ON s.CurrencyType = c.ID LEFT OUTER JOIN ";
            strSql += "officedba.SellChance AS sc ON s.FromBillID = sc.ID ";

            strSql += " WHERE (s.ID = @ID ) AND (s.CompanyCD = @CompanyCD)";
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
        public static bool ConfirmOrder(string OrderNO, out string strMsg)
        {
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            //判断单据是够为制单状态，非制单状态不能确认
            if (isHandle(OrderNO, "1"))
            {
                if (SellChanceIsOffer(OrderNO))
                {
                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {
                        SqlParameter[] paras = new SqlParameter[4];
                        strSq = "update  officedba.SellOffer set BillStatus='2'  ";
                       
                            strSq += " , Confirmor=@EmployeeID, ConfirmDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                            paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                            paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                            paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                            paras[3] = new SqlParameter("@OfferNo", OrderNO);
                       
                        strSq += " WHERE OfferNo = @OfferNo and CompanyCD=@CompanyCD";

                        UpdateSellChance(1, OrderNO, tran);//更新销售机会是否报价

                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);

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
                else
                {
                    strMsg = "源单已被其他报价单报价，请重新选择！";
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
                    strSq = "update  officedba.SellOffer set BillStatus='4'  ";
                    
                        strSq += " , Closer=@EmployeeID, CloseDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                        paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                        paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                        paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                        paras[3] = new SqlParameter("@OfferNo", OrderNO);
                    
                    strSq += " WHERE OfferNo = @OfferNo and CompanyCD=@CompanyCD";

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
                    strSq = "update  officedba.SellOffer set BillStatus='2'  ";
                  
                        strSq += " , Closer=@EmployeeID, CloseDate=@CloseDate, ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                        paras[0] = new SqlParameter("@EmployeeID", DBNull.Value);
                        paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                        paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                        paras[3] = new SqlParameter("@OfferNo", OrderNO);
                        paras[4] = new SqlParameter("@CloseDate", DBNull.Value);
                    
                    strSq += " WHERE OfferNo = @OfferNo and CompanyCD=@CompanyCD";

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

                    

                    strSq = "update  officedba.SellOffer set BillStatus='1'  ";
                    strSq += " , Confirmor=@Confirmor, ConfirmDate=@ConfirmDate, ModifiedDate=getdate() ,ModifiedUserID=@UserID ";
                    strSq += " WHERE OfferNo = @OfferNo and CompanyCD=@CompanyCD";

                    paras[0] = new SqlParameter("@Confirmor", DBNull.Value);
                    paras[1] = new SqlParameter("@UserID", strUserID);
                    paras[2] = new SqlParameter("@CompanyCD", strCompanyCD);
                    paras[3] = new SqlParameter("@OfferNo", OrderNO);
                    paras[4] = new SqlParameter("@ConfirmDate", DBNull.Value);

                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {

                        UpdateSellChance(2, OrderNO, tran);//更新销售机会是否被报价
                        FlowDBHelper.OperateCancelConfirm(strCompanyCD, 5, 1, OrderId, strUserID, tran);//撤销审批
                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);

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


        /// <summary>
        /// 更新销售就会中的是否报价字段
        /// </summary>
        /// <param name="flag">标识，1：确认操作，2：取消确认</param>
        /// <param name="OfferNo"></param>
        private static void UpdateSellChance(int flag, string OfferNo, TransactionManager tran)
        {
            string strCompanyCD = string.Empty;//单位编号

           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            

            string strSql = string.Empty;

            switch (flag)
            {
                case 1://确认操作，销售机会被更新为被报价
                    strSql = "UPDATE officedba.SellChance SET IsQuoted = '1' ";
                    break;
                case 2://取消确认，销售机会被更新为未被报价
                    strSql = "UPDATE officedba.SellChance SET IsQuoted = '0' ";
                    break;
                default:
                    break;
            }

            SqlParameter[] paras = { new SqlParameter("@OfferNo", OfferNo), new SqlParameter("@CompanyCD", strCompanyCD) };
            strSql += " WHERE (ID = (SELECT FromBillID FROM officedba.SellOffer WHERE (OfferNo = @OfferNo) AND (CompanyCD = @CompanyCD)))";
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql, paras);

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

            strSql = "select count(1) from officedba.SellOffer where OfferNo = @OfferNo and CompanyCD=@CompanyCD and BillStatus=@BillStatus ";
            ArrayList arr = new ArrayList();
            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@OfferNo", OrderNO);
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
        /// 获取销售机会是否被报价来确定报价单能否确认
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <returns>返回true时表示可以执行操作</returns>
        private static bool SellChanceIsOffer(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@OfferNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.SellChance ";
            strSql += " WHERE (ID = (SELECT FromBillID FROM officedba.SellOffer WHERE (OfferNo = @OfferNo) AND (CompanyCD = @CompanyCD) and FromType='1')) and IsQuoted<>'0' ";
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount == 0)
            {
                isSuc = true;
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
                                       new SqlParameter("@OfferNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.SellOffer ";
            strSql += " WHERE  (OfferNo = @OfferNo) AND (CompanyCD = @CompanyCD) ";
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
                                       new SqlParameter("@OfferNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.SellContract ";
            strSql += " WHERE (FromBillID = (SELECT ID FROM officedba.SellOffer WHERE (OfferNo = @OfferNo) AND (CompanyCD = @CompanyCD))) and FromType='1' ";
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            strSql = null;
            SqlParameter[] paras1 = { 
                                       new SqlParameter("@OfferNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.SellOrder ";
            strSql += " WHERE (FromBillID = (SELECT ID FROM officedba.SellOffer WHERE (OfferNo = @OfferNo) AND (CompanyCD = @CompanyCD))) and FromType='1' ";
            iCount += Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras1));

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
                                       new SqlParameter("@OfferNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("FlowStauts",'5')
                                   };
            strSql += " SELECT COUNT(1) FROM officedba.FlowInstance AS f LEFT OUTER JOIN officedba.SellOffer AS s ON f.BillID = s.ID AND f.BillTypeFlag = 5 AND f.BillTypeCode = 1 and f.CompanyCD=s.CompanyCD";
            strSql += "  WHERE (s.OfferNo = @OfferNo) AND (s.CompanyCD = @CompanyCD) and f.flowstatus!=@FlowStatus  ";
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
                                       new SqlParameter("@OfferNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select TOP  1  FlowStatus  from officedba.FlowInstance ";
            strSql += " WHERE (BillID = (SELECT ID FROM officedba.SellOffer WHERE (OfferNo = @OfferNo) AND (CompanyCD = @CompanyCD))) AND BillTypeFlag = 5 AND BillTypeCode = 1  ";
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
           
            string strSql = "select * from  officedba.sellmodule_report_SellOffer WHERE (OfferNo = @OfferNo) AND (CompanyCD = @CompanyCD)";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@OfferNo",OrderNo)
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
           
            string strSql = "select * from  officedba.sellmodule_report_SellOfferDetail WHERE (OfferNo = @OfferNo) AND (CompanyCD = @CompanyCD) order by SortNo asc";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@OfferNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(SellOfferModel model, Hashtable htExtAttr, TransactionManager tran)
        {
            try
            {
                string strSql = string.Empty;
                strSql = "UPDATE officedba.SellOffer set ";

                SqlParameter[] parameters = new SqlParameter[htExtAttr.Count + 2];
                int i = 0;

                foreach (DictionaryEntry de in htExtAttr)// de为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    parameters[i] = SqlHelper.GetParameter("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                    i++;
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND OfferNo = @OfferNo";
                parameters[i] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
                parameters[i + 1] = SqlHelper.GetParameter("@OfferNo", model.OfferNo);
                //cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                //cmd.Parameters.AddWithValue("@PlanNo", model.PlanNo);
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
                //cmd.CommandText = strSql;
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
        }
        #endregion
    }
}

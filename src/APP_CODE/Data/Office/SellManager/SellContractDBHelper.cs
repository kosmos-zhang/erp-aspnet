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
    public class SellContractDBHelper
    {
        #region 添加、修改、删除相关操作
        /// <summary>
        /// 添加销售合同
        /// </summary>
        /// <param name="sellChanceModel"></param>
        /// <param name="sellChancePushModel"></param>
        /// <returns>是否添加成功</returns>
        public static bool InsertOrder(Hashtable ht,SellContractModel sellContractModel,
            List<SellContractDetailModel> SellContractDetailModelList, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            //判断单据编号是否存在
            if (NoIsExist(sellContractModel.ContractNo))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    InsertSellContract(sellContractModel, tran);

                    //拓展属性
                    GetExtAttrCmd(sellContractModel, ht, tran);

                    InsertSellContractDetail(SellContractDetailModelList, sellContractModel, tran);

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
        /// 修改销售合同
        /// </summary>
        /// <param name="sellChanceModel">销售合同表实体</param>
        /// <param name="sellChancePushModel">销售阶段表实体</param>
        /// <returns>是否添加成功</returns>
        public static bool UpdateOrder(Hashtable ht,SellContractModel sellContractModel,
            List<SellContractDetailModel> SellContractDetailModelList, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            if (IsUpdate(sellContractModel.ContractNo))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    UpdateSellContract(sellContractModel, tran);

                    //拓展属性
                    GetExtAttrCmd(sellContractModel, ht, tran);

                    InsertSellContractDetail(SellContractDetailModelList, sellContractModel, tran);
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

        /// <summary>
        /// 添加销售合同主表信息
        /// </summary>
        /// <param name="sellContractModel"></param>
        private static void InsertSellContract(SellContractModel sellContractModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SellContract(");
            strSql.Append("CompanyCD,FromType,FromBillID,CustID,CustTel,Title,ContractNo,SellType,BusiType,CurrencyType,Rate,TotalPrice,Tax,TotalFee,Discount,DiscountTotal,RealTotal,isAddTax,CountTotal,PayType,MoneyType,CarryType,TakeType,Seller,SellDeptId,SignDate,StartDate,EndDate,SignAddr,TheyDelegate,OurDelegate,State,EndNote,TalkProcess,Remark,Attachment,BillStatus,Creator,CreateDate,ModifiedDate,ModifiedUserID,CanViewUser)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@FromType,@FromBillID,@CustID,@CustTel,@Title,@ContractNo,@SellType,@BusiType,@CurrencyType,@Rate,@TotalPrice,@Tax,@TotalFee,@Discount,@DiscountTotal,@RealTotal,@isAddTax,@CountTotal,@PayType,@MoneyType,@CarryType,@TakeType,@Seller,@SellDeptId,@SignDate,@StartDate,@EndDate,@SignAddr,@TheyDelegate,@OurDelegate,@State,@EndNote,@TalkProcess,@Remark,@Attachment,@BillStatus,@Creator,getdate(),getdate(),@ModifiedUserID,@CanViewUser)");

            #region 参数
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@FromType", SqlDbType.Char,1),
					new SqlParameter("@FromBillID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.Int,4),
					new SqlParameter("@CustTel", SqlDbType.VarChar,100),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@ContractNo", SqlDbType.VarChar,50),
					new SqlParameter("@SellType", SqlDbType.Int,4),
					new SqlParameter("@BusiType", SqlDbType.Char,1),
					new SqlParameter("@CurrencyType", SqlDbType.Int,4),
					new SqlParameter("@Rate", SqlDbType.Decimal,9),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Tax", SqlDbType.Decimal,9),
					new SqlParameter("@TotalFee", SqlDbType.Decimal,9),
					new SqlParameter("@Discount", SqlDbType.Decimal,9),
					new SqlParameter("@DiscountTotal", SqlDbType.Decimal,9),
					new SqlParameter("@RealTotal", SqlDbType.Decimal,9),
					new SqlParameter("@isAddTax", SqlDbType.Char,1),
					new SqlParameter("@CountTotal", SqlDbType.Decimal,9),
					new SqlParameter("@PayType", SqlDbType.Int,4),
					new SqlParameter("@MoneyType", SqlDbType.Int,4),
					new SqlParameter("@CarryType", SqlDbType.Int,4),
					new SqlParameter("@TakeType", SqlDbType.Int,4),
					new SqlParameter("@Seller", SqlDbType.Int,4),
					new SqlParameter("@SellDeptId", SqlDbType.Int,4),
					new SqlParameter("@SignDate", SqlDbType.DateTime),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@SignAddr", SqlDbType.VarChar,100),
					new SqlParameter("@TheyDelegate", SqlDbType.VarChar,50),
					new SqlParameter("@OurDelegate", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.Char,1),
					new SqlParameter("@EndNote", SqlDbType.VarChar,200),
					new SqlParameter("@TalkProcess", SqlDbType.VarChar,200),
					new SqlParameter("@Remark", SqlDbType.VarChar,200),
					new SqlParameter("@Attachment", SqlDbType.VarChar,150),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20),
					new SqlParameter("@CanViewUser", SqlDbType.VarChar,2048)};
            parameters[0].Value = sellContractModel.CompanyCD;
            parameters[1].Value = sellContractModel.FromType;
            parameters[2].Value = sellContractModel.FromBillID;
            parameters[3].Value = sellContractModel.CustID;
            parameters[4].Value = sellContractModel.CustTel;
            parameters[5].Value = sellContractModel.Title;
            parameters[6].Value = sellContractModel.ContractNo;
            parameters[7].Value = sellContractModel.SellType;
            parameters[8].Value = sellContractModel.BusiType;
            parameters[9].Value = sellContractModel.CurrencyType;
            parameters[10].Value = sellContractModel.Rate;
            parameters[11].Value = sellContractModel.TotalPrice;
            parameters[12].Value = sellContractModel.Tax;
            parameters[13].Value = sellContractModel.TotalFee;
            parameters[14].Value = sellContractModel.Discount;
            parameters[15].Value = sellContractModel.DiscountTotal;
            parameters[16].Value = sellContractModel.RealTotal;
            parameters[17].Value = sellContractModel.isAddTax;
            parameters[18].Value = sellContractModel.CountTotal;
            parameters[19].Value = sellContractModel.PayType;
            parameters[20].Value = sellContractModel.MoneyType;
            parameters[21].Value = sellContractModel.CarryType;
            parameters[22].Value = sellContractModel.TakeType;
            parameters[23].Value = sellContractModel.Seller;
            parameters[24].Value = sellContractModel.SellDeptId;
            parameters[25].Value = sellContractModel.SignDate;
            parameters[26].Value = sellContractModel.StartDate;
            parameters[27].Value = sellContractModel.EndDate;
            parameters[28].Value = sellContractModel.SignAddr;
            parameters[29].Value = sellContractModel.TheyDelegate;
            parameters[30].Value = sellContractModel.OurDelegate;
            parameters[31].Value = sellContractModel.State;
            parameters[32].Value = sellContractModel.EndNote;
            parameters[33].Value = sellContractModel.TalkProcess;
            parameters[34].Value = sellContractModel.Remark;
            parameters[35].Value = sellContractModel.Attachment;
            parameters[36].Value = sellContractModel.BillStatus;
            parameters[37].Value = sellContractModel.Creator;
            parameters[38].Value = sellContractModel.ModifiedUserID;
            parameters[39].Value = sellContractModel.CanViewUser;

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
        /// 修改销售合同主表信息
        /// </summary>
        /// <param name="sellContractModel"></param>
        /// <param name="tran"></param>
        private static void UpdateSellContract(SellContractModel sellContractModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SellContract set ");
            strSql.Append("FromType=@FromType,");
            strSql.Append("FromBillID=@FromBillID,");
            strSql.Append("CustID=@CustID,");
            strSql.Append("CustTel=@CustTel,");
            strSql.Append("Title=@Title,");
            strSql.Append("SellType=@SellType,");
            strSql.Append("BusiType=@BusiType,");
            strSql.Append("CurrencyType=@CurrencyType,");
            strSql.Append("Rate=@Rate,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("Tax=@Tax,");
            strSql.Append("TotalFee=@TotalFee,");
            strSql.Append("Discount=@Discount,");
            strSql.Append("DiscountTotal=@DiscountTotal,");
            strSql.Append("RealTotal=@RealTotal,");
            strSql.Append("isAddTax=@isAddTax,");
            strSql.Append("CountTotal=@CountTotal,");
            strSql.Append("PayType=@PayType,");
            strSql.Append("MoneyType=@MoneyType,");
            strSql.Append("CarryType=@CarryType,");
            strSql.Append("TakeType=@TakeType,");
            strSql.Append("Seller=@Seller,");
            strSql.Append("SellDeptId=@SellDeptId,");
            strSql.Append("SignDate=@SignDate,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("SignAddr=@SignAddr,");
            strSql.Append("TheyDelegate=@TheyDelegate,");
            strSql.Append("OurDelegate=@OurDelegate,");
            strSql.Append("State=@State,");
            strSql.Append("EndNote=@EndNote,");
            strSql.Append("TalkProcess=@TalkProcess,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("BillStatus=@BillStatus,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(",CanViewUser=@CanViewUser");
            strSql.Append(" where CompanyCD=@CompanyCD and ContractNo=@ContractNo");

            #region
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@FromType", SqlDbType.Char,1),
					new SqlParameter("@FromBillID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.Int,4),
					new SqlParameter("@CustTel", SqlDbType.VarChar,100),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@ContractNo", SqlDbType.VarChar,50),
					new SqlParameter("@SellType", SqlDbType.Int,4),
					new SqlParameter("@BusiType", SqlDbType.Char,1),
					new SqlParameter("@CurrencyType", SqlDbType.Int,4),
					new SqlParameter("@Rate", SqlDbType.Decimal,9),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Tax", SqlDbType.Decimal,9),
					new SqlParameter("@TotalFee", SqlDbType.Decimal,9),
					new SqlParameter("@Discount", SqlDbType.Decimal,9),
					new SqlParameter("@DiscountTotal", SqlDbType.Decimal,9),
					new SqlParameter("@RealTotal", SqlDbType.Decimal,9),
					new SqlParameter("@isAddTax", SqlDbType.Char,1),
					new SqlParameter("@CountTotal", SqlDbType.Decimal,9),
					new SqlParameter("@PayType", SqlDbType.Int,4),
					new SqlParameter("@MoneyType", SqlDbType.Int,4),
					new SqlParameter("@CarryType", SqlDbType.Int,4),
					new SqlParameter("@TakeType", SqlDbType.Int,4),
					new SqlParameter("@Seller", SqlDbType.Int,4),
					new SqlParameter("@SellDeptId", SqlDbType.Int,4),
					new SqlParameter("@SignDate", SqlDbType.DateTime),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@SignAddr", SqlDbType.VarChar,100),
					new SqlParameter("@TheyDelegate", SqlDbType.VarChar,50),
					new SqlParameter("@OurDelegate", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.Char,1),
					new SqlParameter("@EndNote", SqlDbType.VarChar,200),
					new SqlParameter("@TalkProcess", SqlDbType.VarChar,200),
					new SqlParameter("@Remark", SqlDbType.VarChar,200),
					new SqlParameter("@Attachment", SqlDbType.VarChar,150),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20),
					new SqlParameter("@CanViewUser", SqlDbType.VarChar,2048)};
            parameters[0].Value = sellContractModel.CompanyCD;
            parameters[1].Value = sellContractModel.FromType;
            parameters[2].Value = sellContractModel.FromBillID;
            parameters[3].Value = sellContractModel.CustID;
            parameters[4].Value = sellContractModel.CustTel;
            parameters[5].Value = sellContractModel.Title;
            parameters[6].Value = sellContractModel.ContractNo;
            parameters[7].Value = sellContractModel.SellType;
            parameters[8].Value = sellContractModel.BusiType;
            parameters[9].Value = sellContractModel.CurrencyType;
            parameters[10].Value = sellContractModel.Rate;
            parameters[11].Value = sellContractModel.TotalPrice;
            parameters[12].Value = sellContractModel.Tax;
            parameters[13].Value = sellContractModel.TotalFee;
            parameters[14].Value = sellContractModel.Discount;
            parameters[15].Value = sellContractModel.DiscountTotal;
            parameters[16].Value = sellContractModel.RealTotal;
            parameters[17].Value = sellContractModel.isAddTax;
            parameters[18].Value = sellContractModel.CountTotal;
            parameters[19].Value = sellContractModel.PayType;
            parameters[20].Value = sellContractModel.MoneyType;
            parameters[21].Value = sellContractModel.CarryType;
            parameters[22].Value = sellContractModel.TakeType;
            parameters[23].Value = sellContractModel.Seller;
            parameters[24].Value = sellContractModel.SellDeptId;
            parameters[25].Value = sellContractModel.SignDate;
            parameters[26].Value = sellContractModel.StartDate;
            parameters[27].Value = sellContractModel.EndDate;
            parameters[28].Value = sellContractModel.SignAddr;
            parameters[29].Value = sellContractModel.TheyDelegate;
            parameters[30].Value = sellContractModel.OurDelegate;
            parameters[31].Value = sellContractModel.State;
            parameters[32].Value = sellContractModel.EndNote;
            parameters[33].Value = sellContractModel.TalkProcess;
            parameters[34].Value = sellContractModel.Remark;
            parameters[35].Value = sellContractModel.Attachment;
            parameters[36].Value = sellContractModel.BillStatus;
            parameters[37].Value = sellContractModel.ModifiedUserID;
            parameters[38].Value = sellContractModel.CanViewUser;
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
        /// 添加销售合同子表信息
        /// </summary>
        /// <param name="sellContractModel"></param>
        private static void InsertSellContractDetail(List<SellContractDetailModel> SellContractDetailModelList, SellContractModel sellContractModel, TransactionManager tran)
        {

            string strSqlDel = "delete from officedba.SellContractDetail where  ContractNo=@ContractNo  and CompanyCD=@CompanyCD ";
            SqlParameter[] paras = { new SqlParameter("@ContractNo", sellContractModel.ContractNo), new SqlParameter("@CompanyCD", sellContractModel.CompanyCD) };
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSqlDel.ToString(), paras);

            foreach (SellContractDetailModel sellOfferDetailModel in SellContractDetailModelList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into officedba.SellContractDetail(");
                strSql.Append("CompanyCD,ContractNo,SortNo,ProductID,ProductCount,UnitID,UnitPrice,TaxPrice,Discount,TaxRate,TotalFee,TotalPrice,TotalTax,SendTime,Package,Remark,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate)");
                strSql.Append(" values (");
                strSql.Append("@CompanyCD,@ContractNo,@SortNo,@ProductID,@ProductCount,@UnitID,@UnitPrice,@TaxPrice,@Discount,@TaxRate,@TotalFee,@TotalPrice,@TotalTax,@SendTime,@Package,@Remark,getdate(),@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate)");
                #region
                SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@ContractNo", SqlDbType.VarChar,50),
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
                parameters[1].Value = sellOfferDetailModel.ContractNo;
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


            sql = " select ID from officedba.SellContract where CompanyCD=@CompanyCD and ContractNo=@ContractNo ";
            SqlParameter[] paras = { new SqlParameter("@CompanyCD", strCompanyCD), new SqlParameter("@ContractNo", orderNo) };
            OrderID = (int)SqlHelper.ExecuteScalar(sql, paras);
            return OrderID;
        }

        /// <summary>
        /// 删除销售合同
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
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellContract WHERE ContractNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellContractDetail WHERE ContractNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);

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
        /// 获取合同列表 
        /// </summary>
        /// <param name="sellContractModel">sellContractModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(string EFIndex,string EFDesc,SellContractModel sellContractModel, int? FlowStatus, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            strSql = "select * from ( SELECT s.ID, s.Title,s.ModifiedDate, s.ContractNo, s.TotalFee,case s.FromType when  ";
            strSql += "  '1' then  ISNULL(so.OfferNo, '') when '2' then isnull(sc.ChanceNo,'') end as OfferNo, ";
            strSql += "ISNULL(e.EmployeeName, '') AS EmployeeName,isnull( c.CustName,'') as CustName,                          ";
            strSql += "CASE s.FromType WHEN '0' THEN '无来源' WHEN '1' THEN '销售报价单'  WHEN '2' THEN '销售机会'  END AS FromTypeText,                      ";
            strSql += "CASE s.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更'                              ";
            strSql += "WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText,                                    ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                ";
            strSql += "FROM officedba.FlowInstance                                                                             ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 2                                          ";
            strSql += "ORDER BY ModifiedDate DESC) AS FlowStatus  ,                                                            ";
            strSql += "CASE WHEN (SELECT TOP 1 FlowStatus                                                                      ";
            strSql += "FROM officedba.FlowInstance                                                                             ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 2                                          ";
            strSql += "ORDER BY ModifiedDate DESC) IS NULL THEN '' WHEN                                                        ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                ";
            strSql += "FROM officedba.FlowInstance                                                                             ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 2                                          ";
            strSql += "ORDER BY ModifiedDate DESC) = 1 THEN '待审批' WHEN                                                      ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                ";
            strSql += "FROM officedba.FlowInstance                                                                             ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 2                                          ";
            strSql += "ORDER BY ModifiedDate DESC) = 2 THEN '审批中' WHEN                                                      ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                ";
            strSql += "FROM officedba.FlowInstance                                                                             ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 2                                          ";
            strSql += "ORDER BY ModifiedDate DESC) = 3 THEN '审批通过' WHEN                                                    ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                ";
            strSql += "FROM officedba.FlowInstance                                                                             ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 2                                          ";
            strSql += "ORDER BY ModifiedDate DESC) = 4 THEN '审批不通过' WHEN                                                  ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                ";
            strSql += "FROM officedba.FlowInstance                                                                             ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 2                                          ";
            strSql += "ORDER BY ModifiedDate DESC) = 5 THEN '撤销审批' END AS FlowInstanceText,                                ";
            strSql += "CASE s.State WHEN '1' THEN '执行中' WHEN '2' THEN '意外终止' WHEN '3'                                   ";
            strSql += "THEN '已执行' when '4' then '已到期' END AS stateText, isnull(CASE                                                             ";
            strSql += "(SELECT count(1) FROM officedba.SellOrder AS sod                                                        ";
            strSql += "WHERE sod.FromType = '2' AND sod.FromBillID = s.ID) WHEN 0 THEN '无引用' END, '被引用') AS RefText      ";
            strSql += "FROM officedba.SellContract AS s LEFT OUTER JOIN                                                        ";
            strSql += "officedba.SellOffer AS so ON s.FromBillID = so.ID and s.FromType='1' LEFT OUTER JOIN                                       ";
            strSql += "officedba.SellChance AS sc ON s.FromBillID = sc.ID and s.FromType='2' LEFT OUTER JOIN                                       ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID LEFT OUTER JOIN                                          ";
            strSql += "officedba.CustInfo AS c ON s.CustID = c.ID  where 1=1      and s.CompanyCD=@CompanyCD                                               ";

            strSql += " and ( charindex('," + sellContractModel.Creator + ",' , ','+s.CanViewUser+',')>0 or s.Creator=" + sellContractModel.Creator + " OR s.CanViewUser='' OR s.CanViewUser is null) ";

            string strCompanyCD = string.Empty;//单位编号
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (sellContractModel.BillStatus != null)
            {
                strSql += " and s.BillStatus= @BillStatus";
                arr.Add(new SqlParameter("@BillStatus", sellContractModel.BillStatus));
            }
            if (sellContractModel.FromBillID != null)
            {
                strSql += " and s.FromBillID=@FromBillID";
                arr.Add(new SqlParameter("@FromBillID", sellContractModel.FromBillID)); ;
            }
            if (sellContractModel.FromType != null)
            {
                strSql += " and s.FromType=@FromType";
                arr.Add(new SqlParameter("@FromType", sellContractModel.FromType));
            }

            if (sellContractModel.ContractNo != null)
            {
                strSql += " and s.ContractNo like @ContractNo";
                arr.Add(new SqlParameter("@ContractNo", "%" + sellContractModel.ContractNo + "%"));
            }
            if (sellContractModel.Seller != null)
            {
                strSql += " and s.Seller=@Seller";
                arr.Add(new SqlParameter("@Seller", sellContractModel.Seller));
            }
            if (sellContractModel.Title != null)
            {
                strSql += " and s.Title like @Title";
                arr.Add(new SqlParameter("@Title", "%" + sellContractModel.Title + "%"));
            }
            if (sellContractModel.CustID != null)
            {
                strSql += " and s.CustID=@CustID";
                arr.Add(new SqlParameter("@CustID", sellContractModel.CustID));
            }

            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                strSql += " and s.ExtField" + EFIndex + " LIKE @EFDesc";
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

            string strSql = "SELECT so.ProductID, so.ProductCount, so.UnitID,so.UnitPrice, ";
            strSql += "so.TaxPrice, so.Discount, so.TaxRate, so.TotalFee, so.TotalPrice, p.Specification, ";
            strSql += "so.TotalTax, so.SendTime, so.Package, so.Remark, p.ProductName, c.CodeName, p.SellTax, p.TaxRate AS TaxRate1 ,isnull(p.StandardCost,0) as StandardCost, p.ProdNo ";
            strSql += ",isnull(p.StandardSell,0) as StandardSell,so.UsedUnitID,isnull(so.UsedUnitCount,0) as UsedUnitCount,isnull(so.UsedPrice,0) as UsedPrice,isnull(so.ExRate,1) as ExRate ";
            strSql += "FROM officedba.SellContractDetail AS so INNER JOIN ";
            strSql += "officedba.ProductInfo AS p ON so.ProductID = p.ID INNER JOIN ";
            strSql += "officedba.CodeUnitType AS c ON so.UnitID = c.ID ";
            strSql += "WHERE (so.ContractNo = @ContractNo) AND (so.CompanyCD = @CompanyCD) ";
            strSql += "ORDER BY so.SortNo ";

            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@ContractNo", orderNo);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }


        /// <summary>
        /// 获取合同主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = "SELECT s.ContractNo, s.CustID, s.CustTel, s.FromType, s.FromBillID, ";
            strSql += "s.Title, s.Seller, s.SellDeptId, s.SellType, s.BusiType, s.PayType, s.MoneyType, ";
            strSql += "s.CarryType, s.TakeType, s.TotalPrice, s.TotalFee, s.Discount, ";
            strSql += "s.DiscountTotal, s.RealTotal, s.CountTotal, s.isAddTax, s.CurrencyType, s.Rate, ";
            strSql += "s.Remark, s.BillStatus, CONVERT(varchar(100), s.CreateDate, 23) AS CreateDate, ";
            strSql += "CONVERT(varchar(100), s.ConfirmDate, 23) AS ConfirmDate, ";
            strSql += "CONVERT(varchar(100), s.CloseDate, 23) AS CloseDate, ";
            strSql += "CONVERT(varchar(100), s.ModifiedDate, 23) AS ModifiedDate, s.ModifiedUserID, ";
            strSql += "e1.EmployeeName AS SellerName, e2.EmployeeName AS CreatorName, ";
            strSql += "e3.EmployeeName AS ConfirmorName, d.DeptName, c.CurrencyName, ";
            strSql += "case s.FromType when  '1' then  ISNULL(sc.OfferNo, '') when '2' then isnull(sc1.ChanceNo,'') end as OfferNo , e4.EmployeeName AS CloserName, ";
            strSql += "CASE s.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更' ";
            strSql += "WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText, ";
            strSql += "ct.CustName, s.Tax, CONVERT(varchar(100), s.SignDate, 23) AS SignDate, ";
            strSql += "CONVERT(varchar(100), s.StartDate, 23) AS StartDate, ";
            strSql += "CONVERT(varchar(100), s.EndDate, 23) AS EndDate, s.SignAddr, s.TheyDelegate, ";
            strSql += "s.OurDelegate, s.State, s.EndNote, s.TalkProcess, s.Attachment, ";
            strSql += "e5.EmployeeName AS OurDelegateName, ";
            strSql += " s.ExtField1,s.ExtField2,s.ExtField3,s.ExtField4,s.ExtField5,s.ExtField6,s.ExtField7,s.ExtField8,s.ExtField9,s.ExtField10  ";
            strSql += ",s.CanViewUser,[dbo].[getEmployeeNameString](s.CanViewUser) as CanViewUserName ";
            strSql += "FROM officedba.SellContract AS s LEFT OUTER JOIN ";
            strSql += "officedba.CustInfo AS ct ON s.CustID = ct.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e1 ON s.Seller = e1.ID LEFT OUTER JOIN ";
            strSql += "officedba.DeptInfo AS d ON s.SellDeptId = d.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e2 ON s.Creator = e2.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e3 ON s.Confirmor = e3.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e4 ON s.Closer = e4.ID LEFT OUTER JOIN ";
            strSql += "officedba.CurrencyTypeSetting AS c ON s.CurrencyType = c.ID LEFT OUTER JOIN ";
            strSql += "officedba.SellOffer AS sc ON s.FromBillID = sc.ID and s.FromType='1' LEFT OUTER JOIN                                       ";
            strSql += "officedba.SellChance AS sc1 ON s.FromBillID = sc1.ID and s.FromType='2' LEFT OUTER JOIN                                       ";
            strSql += "officedba.EmployeeInfo AS e5 ON s.OurDelegate = e5.ID ";

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

                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    SqlParameter[] paras = new SqlParameter[4];
                    strSq = "update  officedba.SellContract set BillStatus='2'  ";

                    strSq += " , Confirmor=@EmployeeID, ConfirmDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                    paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                    paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                    paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    paras[3] = new SqlParameter("@ContractNo", OrderNO);

                    strSq += " WHERE ContractNo = @ContractNo and CompanyCD=@CompanyCD";

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
                isSuc = false;
                strMsg = "该单据已被其他用户确认，不可再次确认！";
            }
            return isSuc;
        }

        /// <summary>
        /// 终止合同
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static bool EndOrder(string OrderNO, out string strMsg)
        {
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            //判断单据是否被引用
            if (IsRef(OrderNO))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    int iEmployeeID = 0;//员工id
                    string strUserID = string.Empty;//用户id
                    string strCompanyCD = string.Empty;//单位编码
                    int OrderId = GetOrderID(OrderNO);


                    iEmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                    strUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


                    SqlParameter[] paras = new SqlParameter[4];
                    strSq = "update  officedba.SellContract set BillStatus='4' ,State='2'  ";

                    strSq += " , Closer=@EmployeeID, CloseDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                    paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                    paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                    paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    paras[3] = new SqlParameter("@ContractNo", OrderNO);

                    strSq += " WHERE ContractNo = @ContractNo and CompanyCD=@CompanyCD";
                    FlowDBHelper.OperateCancelConfirm(strCompanyCD, 5, 2, OrderId, strUserID, tran);//撤销审批
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);

                    tran.Commit();
                    isSuc = true;
                    strMsg = "终止合同成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    isSuc = false;
                    strMsg = "终止合同失败，请联系系统管理员！";
                    throw ex;
                }

            }
            else
            {
                isSuc = false;
                strMsg = "该单据已被其他单据引用，不可终止！";
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
                    strSq = "update  officedba.SellContract set BillStatus='4' ,State='3'  ";

                    strSq += " , Closer=@EmployeeID, CloseDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                    paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                    paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                    paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    paras[3] = new SqlParameter("@ContractNo", OrderNO);

                    strSq += " WHERE ContractNo = @ContractNo and CompanyCD=@CompanyCD";

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
                    strSq = "update  officedba.SellContract set BillStatus='2'  ,State='1' ";

                    strSq += " , Closer=@EmployeeID, CloseDate=@CloseDate, ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                    paras[0] = new SqlParameter("@EmployeeID", DBNull.Value);
                    paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                    paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    paras[3] = new SqlParameter("@ContractNo", OrderNO);
                    paras[4] = new SqlParameter("@CloseDate", DBNull.Value);

                    strSq += " WHERE ContractNo = @ContractNo and CompanyCD=@CompanyCD";

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


                    strSq = "update  officedba.SellContract set BillStatus='1'   ";
                    strSq += " , Confirmor=@Confirmor, ConfirmDate=@ConfirmDate, ModifiedDate=getdate() ,ModifiedUserID=@UserID ";
                    strSq += " WHERE ContractNo = @ContractNo and CompanyCD=@CompanyCD";

                    paras[0] = new SqlParameter("@Confirmor", DBNull.Value);
                    paras[1] = new SqlParameter("@UserID", strUserID);
                    paras[2] = new SqlParameter("@CompanyCD", strCompanyCD);
                    paras[3] = new SqlParameter("@ContractNo", OrderNO);
                    paras[4] = new SqlParameter("@ConfirmDate", DBNull.Value);

                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {

                        FlowDBHelper.OperateCancelConfirm(strCompanyCD, 5, 2, OrderId, strUserID, tran);//撤销审批
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

            strSql = "select count(1) from officedba.SellContract where ContractNo = @ContractNo and CompanyCD=@CompanyCD and BillStatus=@BillStatus ";
            ArrayList arr = new ArrayList();
            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@ContractNo", OrderNO);
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
                                       new SqlParameter("@ContractNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.SellContract ";
            strSql += " WHERE  (ContractNo = @ContractNo) AND (CompanyCD = @CompanyCD) ";
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
                                       new SqlParameter("@ContractNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.SellOrder ";
            strSql += " WHERE (FromBillID = (SELECT ID FROM officedba.SellContract WHERE (ContractNo = @ContractNo) AND (CompanyCD = @CompanyCD))) and FromType='2' ";
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
                                       new SqlParameter("@ContractNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@FlowStatus",'5')
                                   };
            strSql += " SELECT COUNT(1) FROM officedba.FlowInstance AS f LEFT OUTER JOIN officedba.SellContract AS s ON f.BillID = s.ID AND f.BillTypeFlag = 5 AND f.BillTypeCode = 2 and f.CompanyCD=s.CompanyCD";
            strSql += "  WHERE (s.ContractNo = @ContractNo) AND (s.CompanyCD = @CompanyCD) and f.flowstatus!=@FlowStatus  ";
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
                                       new SqlParameter("@ContractNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select TOP  1  FlowStatus  from officedba.FlowInstance ";
            strSql += " WHERE (BillID = (SELECT ID FROM officedba.SellContract WHERE (ContractNo = @ContractNo) AND (CompanyCD = @CompanyCD))) AND BillTypeFlag = 5 AND BillTypeCode = 2  ";
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

        #region 报表相关

        #region 旧报表
        /// <summary>
        /// 合同订单状态分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <returns></returns>
        public static DataTable GetTotal(DateTime startDate, DateTime endDate, bool isCon, bool isOrd)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            endDate = endDate.AddDays(1);
            string strSql = string.Empty;
            ArrayList arr = new ArrayList();
            if (isCon)
            {
                strSql = "select count(1) as CountTotal,                  ";
                strSql += "CASE State WHEN '1' THEN '执行中（合同）' WHEN '2' THEN '意外终止（合同）' WHEN '3'   ";
                strSql += "THEN '已执行（合同）' when '4' then '已到期（合同）' END AS Remark                                                   ";
                strSql += "From officedba. SellContract                                                          ";
                strSql += "Where CompanyCD=@CompanyCD and SignDate<@endDate and  SignDate>=@startDate and BillStatus<> '1'           ";
                strSql += "Group by State                                                                        ";

            }
            if (isCon && isOrd)
            {
                strSql += " union all ";
            }
            if (isOrd)
            {
                strSql += "select count(1) as CountTotal,                 ";
                strSql += "CASE Status WHEN '1' THEN '处理中（订单）' WHEN '2' THEN '处理完（订单）' WHEN '3'  ";
                strSql += "THEN '终止（订单）' END AS Remark                                                   ";
                strSql += "from officedba. SellOrder                                                           ";
                strSql += "where  CompanyCD=@CompanyCD and OrderDate<@endDate and  OrderDate>=@startDate and   BillStatus<> '1'    ";
                strSql += "group by Status                                                                     ";

            }

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@startDate", startDate));
            arr.Add(new SqlParameter("@endDate", endDate));
            return SqlHelper.ExecuteSql(strSql, arr);
        }

        #region 合同订单状态/金额分布

        /// <summary>
        /// 合同订单状态/金额分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <returns></returns>
        public static DataTable GetTotalByState(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? CurrencyType,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            endDate = endDate.AddDays(1);
            string strSql = string.Empty;
            ArrayList arr = new ArrayList();
            if (isCon)
            {
                if (CurrencyType != null)
                {
                    strSql = "select sum(CountTotal) as CountTotal, sum(RealTotal) as TotalPrice,                  ";
                }
                if (CurrencyType == null)
                {
                    strSql = "select sum(CountTotal) as CountTotal, sum(RealTotal*Rate) as TotalPrice,                  ";
                }

                strSql += "CASE State WHEN '1' THEN '执行中（合同）' WHEN '2' THEN '意外终止（合同）' WHEN '3'   ";
                strSql += "THEN '已执行（合同）' when '4' then '已到期（合同）' END AS Remark                                                   ";
                strSql += "From officedba. SellContract                                                          ";
                strSql += "Where CompanyCD=@CompanyCD and BillStatus<> '1' and SignDate<@endDate and  SignDate>=@startDate          ";
                if (CurrencyType != null)
                {
                    strSql += " and  CurrencyType=@CurrencyType  ";

                }
                strSql += "Group by State                                                                        ";

            }
            if (isCon && isOrd)
            {
                strSql += " union all ";
            }
            if (isOrd)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(CountTotal) as CountTotal,sum(RealTotal) as TotalPrice,                 ";
                }
                if (CurrencyType == null)
                {
                    strSql += "select sum(CountTotal) as CountTotal,sum(RealTotal*Rate) as TotalPrice,                 ";
                }

                strSql += "CASE Status WHEN '1' THEN '处理中（订单）' WHEN '2' THEN '处理完（订单）' WHEN '3'  ";
                strSql += "THEN '终止（订单）' END AS Remark                                                   ";
                strSql += "from officedba. SellOrder                                                           ";
                strSql += "where  CompanyCD=@CompanyCD and   BillStatus<> '1'  and OrderDate<@endDate and  OrderDate>=@startDate    ";
                if (CurrencyType != null)
                {
                    strSql += " and  CurrencyType=@CurrencyType  ";

                }
                strSql += "group by Status                                                                     ";

            }
            if (CurrencyType != null)
            {

                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@startDate", startDate));
            arr.Add(new SqlParameter("@endDate", endDate));
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 合同订单状态/金额分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <returns></returns>
        public static DataTable GetTotalByState(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? CurrencyType)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            endDate = endDate.AddDays(1);
            string strSql = string.Empty;
            ArrayList arr = new ArrayList();
            if (isCon)
            {
                if (CurrencyType != null)
                {
                    strSql = "select sum(CountTotal) as CountTotal, sum(RealTotal) as TotalPrice,                  ";
                }
                if (CurrencyType == null)
                {
                    strSql = "select sum(CountTotal) as CountTotal, sum(RealTotal*Rate) as TotalPrice,                  ";
                }
                strSql += "CASE State WHEN '1' THEN '执行中（合同）' WHEN '2' THEN '意外终止（合同）' WHEN '3'   ";
                strSql += "THEN '已执行（合同）' when '4' then '已到期（合同）' END AS Remark                                                   ";
                strSql += "From officedba. SellContract                                                          ";
                strSql += "Where CompanyCD=@CompanyCD and BillStatus<> '1'  and SignDate<@endDate and  SignDate>=@startDate         ";
                if (CurrencyType != null)
                {
                    strSql += " and  CurrencyType=@CurrencyType  ";

                }
                strSql += "Group by State                                                                        ";

            }
            if (isCon && isOrd)
            {
                strSql += " union all ";
            }
            if (isOrd)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(CountTotal) as CountTotal,sum(RealTotal) as TotalPrice,                 ";
                }
                if (CurrencyType == null)
                {
                    strSql += "select sum(CountTotal) as CountTotal,sum(RealTotal*Rate) as TotalPrice,                 ";
                }
                strSql += "CASE Status WHEN '1' THEN '处理中（订单）' WHEN '2' THEN '处理完（订单）' WHEN '3'  ";
                strSql += "THEN '终止（订单）' END AS Remark                                                   ";
                strSql += "from officedba. SellOrder                                                           ";
                strSql += "where  CompanyCD=@CompanyCD and   BillStatus<> '1'  and OrderDate<@endDate and  OrderDate>=@startDate    ";
                if (CurrencyType != null)
                {
                    strSql += " and  CurrencyType=@CurrencyType  ";

                }
                strSql += "group by Status                                                                     ";

            }
            if (CurrencyType != null)
            {

                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@startDate", startDate));
            arr.Add(new SqlParameter("@endDate", endDate));
            return SqlHelper.ExecuteSql(strSql, arr);
        }

        #endregion

        #region 合同订单业务员/金额分布

        /// <summary>
        /// 合同订单业务员/金额分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotalBySeller(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? SellerID, int? CurrencyType,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            endDate = endDate.AddDays(1);
            string strSql = string.Empty;
            ArrayList arr = new ArrayList();
            if (isCon)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal) as TotalPrice, ISNULL(e.EmployeeName, ' ') AS Remark ";
                }
                if (CurrencyType == null)
                {
                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal*sc.Rate) as TotalPrice, ISNULL(e.EmployeeName, ' ') AS Remark ";

                }

                strSql += "from officedba. SellContract as sc LEFT OUTER JOIN                                                         ";
                strSql += "officedba.EmployeeInfo AS e ON sc.Seller = e.ID AND sc.CompanyCD = e.CompanyCD                             ";
                strSql += "where sc.CompanyCD=@CompanyCD and  sc.BillStatus<> '1' and sc.State<>2  and sc.SignDate<@endDate and  sc.SignDate>=@startDate ";

                if (SellerID != null)
                {
                    strSql += " AND (sc.Seller = @Seller) ";

                }
                if (CurrencyType != null)
                {
                    strSql += " and  sc.CurrencyType=@CurrencyType  ";

                }
                strSql += "group by e.EmployeeName ,sc.Seller                                                                                   ";

            }
            if (isCon && isOrd)
            {
                strSql += " union all ";
            }
            if (isOrd)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal,sum(so.RealTotal) as TotalPrice, ISNULL(e.EmployeeName, ' ') AS Remark   ";
                }
                if (CurrencyType == null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal,sum(so.RealTotal*so.Rate) as TotalPrice, ISNULL(e.EmployeeName, ' ') AS Remark   ";

                }

                strSql += "from officedba. SellOrder as so LEFT OUTER JOIN                                                             ";
                strSql += "officedba.EmployeeInfo AS  e ON so.Seller = e.ID AND so.CompanyCD = e.CompanyCD                             ";
                strSql += "where so.CompanyCD=@CompanyCD  and so.BillStatus<> '1' and so.Status<>3  and so.OrderDate<@endDate and  so.OrderDate>=@startDate  ";
                if (SellerID != null)
                {
                    strSql += " AND (so.Seller = @Seller) ";
                }
                if (CurrencyType != null)
                {
                    strSql += " and  so.CurrencyType=@CurrencyType  ";

                }
                strSql += "group by e.EmployeeName , so.Seller                                                                                   ";
            }

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@startDate", startDate));
            arr.Add(new SqlParameter("@endDate", endDate));
            if (SellerID != null)
            {
                arr.Add(new SqlParameter("@Seller", SellerID));

            }
            if (CurrencyType != null)
            {

                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 合同订单业务员/金额分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotalBySeller(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? SellerID, int? CurrencyType)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            endDate = endDate.AddDays(1);
            string strSql = string.Empty;
            ArrayList arr = new ArrayList();
            if (isCon)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal) as TotalPrice, ISNULL(e.EmployeeName, ' ') AS Remark ";
                }
                if (CurrencyType == null)
                {
                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal*sc.Rate) as TotalPrice, ISNULL(e.EmployeeName, ' ') AS Remark ";

                }

                strSql += "from officedba. SellContract as sc LEFT OUTER JOIN                                                         ";
                strSql += "officedba.EmployeeInfo AS e ON sc.Seller = e.ID AND sc.CompanyCD = e.CompanyCD                             ";
                strSql += "where sc.CompanyCD=@CompanyCD  and  sc.BillStatus<> '1' and sc.State<>2  and sc.SignDate<@endDate and  sc.SignDate>=@startDate ";

                if (SellerID != null)
                {
                    strSql += " AND (sc.Seller = @Seller) ";

                }
                if (CurrencyType != null)
                {
                    strSql += " and  sc.CurrencyType=@CurrencyType  ";

                }
                strSql += "group by e.EmployeeName ,sc.Seller                                                                                   ";

            }
            if (isCon && isOrd)
            {
                strSql += " union all ";
            }
            if (isOrd)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal,sum(so.RealTotal) as TotalPrice, ISNULL(e.EmployeeName, ' ') AS Remark   ";
                }
                if (CurrencyType == null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal,sum(so.RealTotal*so.Rate) as TotalPrice, ISNULL(e.EmployeeName, ' ') AS Remark   ";

                }
                strSql += "from officedba. SellOrder as so LEFT OUTER JOIN                                                             ";
                strSql += "officedba.EmployeeInfo AS  e ON so.Seller = e.ID AND so.CompanyCD = e.CompanyCD                             ";
                strSql += "where so.CompanyCD=@CompanyCD and so.BillStatus<> '1' and so.Status<>3  and so.OrderDate<@endDate and  so.OrderDate>=@startDate  ";
                if (SellerID != null)
                {
                    strSql += " AND (so.Seller = @Seller) ";
                }
                if (CurrencyType != null)
                {
                    strSql += " and  so.CurrencyType=@CurrencyType  ";

                }
                strSql += "group by e.EmployeeName , so.Seller                                                                                   ";
            }

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@startDate", startDate));
            arr.Add(new SqlParameter("@endDate", endDate));
            if (SellerID != null)
            {
                arr.Add(new SqlParameter("@Seller", SellerID));

            }
            if (CurrencyType != null)
            {

                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            return SqlHelper.ExecuteSql(strSql, arr);
        }
        #endregion

        #region 合同订单类别分布

        /// <summary>
        /// 合同订单类别分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <param name="SellType">销售类别</param>
        /// <returns></returns>
        public static DataTable GetTotalBySellerAndSellType(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? SellerID, int? SellType, int? CurrencyType,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            endDate = endDate.AddDays(1);
            string strSql = string.Empty;
            ArrayList arr = new ArrayList();
            if (isCon)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal) as TotalPrice ,   ";
                }
                if (CurrencyType == null)
                {

                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal*sc.Rate) as TotalPrice ,   ";
                }

                strSql += "(ISNULL(cpt.TypeName, '无类别')+'（合同）') AS TypeName ,                   ";
                strSql += "ISNULL(e.EmployeeName, '') AS EmployeeName                                    ";
                strSql += "From officedba. SellContract as sc  left join                                 ";
                strSql += "officedba.CodePublicType AS cpt ON sc.SellType = cpt.ID  LEFT OUTER JOIN      ";
                strSql += "officedba.EmployeeInfo AS  e ON sc.Seller = e.ID                              ";

                strSql += "where sc.CompanyCD=@CompanyCD  and sc.BillStatus<> '1' and sc.State<>'2' and sc.SignDate<@endDate and  sc.SignDate>=@startDate ";

                if (SellerID != null)
                {
                    strSql += " AND (sc.Seller = @Seller) ";

                }
                if (CurrencyType != null)
                {
                    strSql += " and  sc.CurrencyType=@CurrencyType  ";

                }
                if (SellType != null)
                {
                    if (SellType != 0)
                    {
                        strSql += " AND (sc.SellType = @SellType)";

                    }
                    else
                    {
                        strSql += " AND (sc.SellType is null ) ";
                    }
                }
                strSql += "Group by sc.SellType,cpt.TypeName ,e.EmployeeName , sc.Seller                 ";

            }
            if (isCon && isOrd)
            {
                strSql += " union all ";
            }
            if (isOrd)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal, sum(so.RealTotal) as TotalPrice ,   ";
                }
                if (CurrencyType == null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal, sum(so.RealTotal*so.Rate) as TotalPrice ,   ";

                }

                strSql += "(ISNULL(cpt.TypeName, '无类别')+'（订单）') AS TypeName ,                   ";
                strSql += "ISNULL(e.EmployeeName, '') AS EmployeeName                                    ";
                strSql += "From officedba. SellOrder as so  left join                                 ";
                strSql += "officedba.CodePublicType AS cpt ON so.SellType = cpt.ID  LEFT OUTER JOIN      ";
                strSql += "officedba.EmployeeInfo AS  e ON so.Seller = e.ID                              ";
                strSql += "where so.CompanyCD=@CompanyCD and so.OrderDate<@endDate and  so.OrderDate>=@startDate and so.BillStatus<> '1' and so.Status<>3 ";
                if (SellerID != null)
                {
                    strSql += " AND (so.Seller = @Seller) ";
                }
                if (CurrencyType != null)
                {
                    strSql += " and  so.CurrencyType=@CurrencyType  ";

                }
                if (SellType != null)
                {
                    if (SellType != 0)
                    {
                        strSql += " AND (so.SellType = @SellType)";

                    }
                    else
                    {
                        strSql += " AND (so.SellType is null ) ";
                    }
                }
                strSql += "Group by so.SellType,cpt.TypeName ,e.EmployeeName , so.Seller                 ";

            }
            if (CurrencyType != null)
            {

                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            if (SellType != null)
            {
                if (SellType != 0)
                {

                    arr.Add(new SqlParameter("@SellType", SellType));
                }
            }
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@startDate", startDate));
            arr.Add(new SqlParameter("@endDate", endDate));
            if (SellerID != null)
            {
                arr.Add(new SqlParameter("@Seller", SellerID));

            }

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 合同订单类别分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <param name="SellType">销售类别</param>
        /// <returns></returns>
        public static DataTable GetTotalBySellerAndSellType(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? SellerID, int? SellType, int? CurrencyType)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            endDate = endDate.AddDays(1);
            string strSql = string.Empty;
            ArrayList arr = new ArrayList();
            if (isCon)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal) as TotalPrice ,   ";
                }
                if (CurrencyType == null)
                {

                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal*sc.Rate) as TotalPrice ,   ";
                }
                strSql += "(ISNULL(cpt.TypeName, '无类别')+'（合同）') AS TypeName ,                   ";
                strSql += "ISNULL(e.EmployeeName, '') AS EmployeeName                                    ";
                strSql += "From officedba. SellContract as sc  left join                                 ";
                strSql += "officedba.CodePublicType AS cpt ON sc.SellType = cpt.ID  LEFT OUTER JOIN      ";
                strSql += "officedba.EmployeeInfo AS  e ON sc.Seller = e.ID                              ";

                strSql += "where sc.CompanyCD=@CompanyCD and sc.BillStatus<> '1' and sc.State<>2 and sc.SignDate<@endDate and  sc.SignDate>=@startDate ";

                if (SellerID != null)
                {
                    strSql += " AND (sc.Seller = @Seller) ";

                }
                if (CurrencyType != null)
                {
                    strSql += " and  sc.CurrencyType=@CurrencyType  ";

                }
                if (SellType != null)
                {
                    if (SellType != 0)
                    {
                        strSql += " AND (sc.SellType = @SellType)";

                    }
                    else
                    {
                        strSql += " AND (sc.SellType is null ) ";
                    }
                }
                strSql += "Group by sc.SellType,cpt.TypeName ,e.EmployeeName , sc.Seller                 ";

            }
            if (isCon && isOrd)
            {
                strSql += " union all ";
            }
            if (isOrd)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal, sum(so.RealTotal) as TotalPrice ,   ";
                }
                if (CurrencyType == null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal, sum(so.RealTotal*so.Rate) as TotalPrice ,   ";

                }
                strSql += "(ISNULL(cpt.TypeName, '无类别')+'（订单）') AS TypeName ,                   ";
                strSql += "ISNULL(e.EmployeeName, '') AS EmployeeName                                    ";
                strSql += "From officedba. SellOrder as so  left join                                 ";
                strSql += "officedba.CodePublicType AS cpt ON so.SellType = cpt.ID  LEFT OUTER JOIN      ";
                strSql += "officedba.EmployeeInfo AS  e ON so.Seller = e.ID                              ";
                strSql += "where so.CompanyCD=@CompanyCD  and so.BillStatus<> '1' and so.Status<>3  and so.OrderDate<@endDate and  so.OrderDate>=@startDate ";
                if (SellerID != null)
                {
                    strSql += " AND (so.Seller = @Seller) ";
                }
                if (CurrencyType != null)
                {
                    strSql += " and  so.CurrencyType=@CurrencyType  ";

                }
                if (SellType != null)
                {
                    if (SellType != 0)
                    {
                        strSql += " AND (so.SellType = @SellType)";

                    }
                    else
                    {
                        strSql += " AND (so.SellType is null ) ";
                    }
                }
                strSql += "Group by so.SellType,cpt.TypeName ,e.EmployeeName , so.Seller                 ";

            }
            if (SellType != null)
            {
                if (SellType != 0)
                {

                    arr.Add(new SqlParameter("@SellType", SellType));
                }
            }

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@startDate", startDate));
            arr.Add(new SqlParameter("@endDate", endDate));
            if (CurrencyType != null)
            {

                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            if (SellerID != null)
            {
                arr.Add(new SqlParameter("@Seller", SellerID));

            }

            return SqlHelper.ExecuteSql(strSql, arr);
        }

        #endregion

        #region 合同订单状态/业务员统计

        /// <summary>
        /// 合同订单状态/业务员统计
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotalBySellerAndState(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? SellerID, int? CurrencyType,
             int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            endDate = endDate.AddDays(1);
            string strSql = string.Empty;
            ArrayList arr = new ArrayList();
            if (isCon)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal) as TotalPrice           ";
                }
                if (CurrencyType == null)
                {
                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal*sc.Rate) as TotalPrice           ";

                }

                strSql += ",ISNULL(e.EmployeeName, '') AS EmployeeName,                                        ";
                strSql += "CASE sc.State WHEN '1' THEN '执行中（合同）' WHEN '2' THEN '意外终止（合同）' WHEN '3' ";
                strSql += "THEN '已执行（合同）' when '4' then '已到期（合同）' END AS StateName                                              ";
                strSql += "From officedba. SellContract as sc left join                                        ";
                strSql += "officedba.EmployeeInfo AS  e ON sc.Seller = e.ID                                    ";

                strSql += "where sc.CompanyCD=@CompanyCD  and sc.BillStatus<> '1'   and sc.SignDate<@endDate and  sc.SignDate>=@startDate               ";
                if (CurrencyType != null)
                {
                    strSql += " and  sc.CurrencyType=@CurrencyType  ";

                }
                if (SellerID != null)
                {
                    strSql += " AND (sc.Seller = @Seller) ";

                }
                strSql += "Group by sc.State,e.EmployeeName , sc.Seller                                                                                   ";

            }
            if (isCon && isOrd)
            {
                strSql += " union all ";
            }
            if (isOrd)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal, sum(so.RealTotal) as TotalPrice ,         ";
                }
                if (CurrencyType == null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal, sum(so.RealTotal*so.Rate) as TotalPrice ,         ";

                }

                strSql += "ISNULL(e.EmployeeName, '') AS EmployeeName,                                         ";
                strSql += "CASE so.Status WHEN '1' THEN '处理中（订单）' WHEN '2' THEN '处理完（订单）' WHEN '3'  ";
                strSql += "THEN '终止（订单）' END AS StateName                                                ";
                strSql += "From officedba. SellOrder as so left join                                           ";
                strSql += "officedba.EmployeeInfo AS  e ON so.Seller = e.ID                                    ";


                strSql += "where so.CompanyCD=@CompanyCD  and so.BillStatus<> '1'   and so.OrderDate<@endDate and  so.OrderDate>=@startDate               ";
                if (SellerID != null)
                {
                    strSql += " AND (so.Seller = @Seller) ";
                }
                if (CurrencyType != null)
                {
                    strSql += " and  so.CurrencyType=@CurrencyType  ";

                }
                strSql += "Group by so.Status,e.EmployeeName , so.Seller                                                                                  ";
            }

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@startDate", startDate));
            arr.Add(new SqlParameter("@endDate", endDate));
            if (SellerID != null)
            {
                arr.Add(new SqlParameter("@Seller", SellerID));

            }
            if (CurrencyType != null)
            {

                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 合同订单状态/业务员统计
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotalBySellerAndState(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? SellerID, int? CurrencyType)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            endDate = endDate.AddDays(1);
            string strSql = string.Empty;
            ArrayList arr = new ArrayList();
            if (isCon)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal) as TotalPrice           ";
                }
                if (CurrencyType == null)
                {
                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal*sc.Rate) as TotalPrice           ";

                }
                strSql += ",ISNULL(e.EmployeeName, '') AS EmployeeName,                                        ";
                strSql += "CASE sc.State WHEN '1' THEN '执行中（合同）' WHEN '2' THEN '意外终止（合同）' WHEN '3' ";
                strSql += "THEN '已执行（合同）' when '4' then '已到期（合同' END AS StateName                                              ";
                strSql += "From officedba. SellContract as sc left join                                        ";
                strSql += "officedba.EmployeeInfo AS  e ON sc.Seller = e.ID                                    ";

                strSql += "where sc.CompanyCD=@CompanyCD  and sc.BillStatus<> '1'  and sc.SignDate<@endDate and  sc.SignDate>=@startDate                ";

                if (SellerID != null)
                {
                    strSql += " AND (sc.Seller = @Seller) ";

                }
                if (CurrencyType != null)
                {
                    strSql += " and  sc.CurrencyType=@CurrencyType  ";

                }
                strSql += "Group by sc.State,e.EmployeeName , sc.Seller                                                                                   ";

            }
            if (isCon && isOrd)
            {
                strSql += " union all ";
            }
            if (isOrd)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal, sum(so.RealTotal) as TotalPrice ,         ";
                }
                if (CurrencyType == null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal, sum(so.RealTotal*so.Rate) as TotalPrice ,         ";

                }
                strSql += "ISNULL(e.EmployeeName, '') AS EmployeeName,                                         ";
                strSql += "CASE so.Status WHEN '1' THEN '处理中（订单）' WHEN '2' THEN '处理完（订单）' WHEN '3'  ";
                strSql += "THEN '终止（订单）' END AS StateName                                                ";
                strSql += "From officedba. SellOrder as so left join                                           ";
                strSql += "officedba.EmployeeInfo AS  e ON so.Seller = e.ID                                    ";


                strSql += "where so.CompanyCD=@CompanyCD and so.BillStatus<> '1'   and so.OrderDate<@endDate and  so.OrderDate>=@startDate              ";
                if (SellerID != null)
                {
                    strSql += " AND (so.Seller = @Seller) ";
                }
                if (CurrencyType != null)
                {
                    strSql += " and  so.CurrencyType=@CurrencyType  ";

                }
                strSql += "Group by so.Status,e.EmployeeName , so.Seller                                                                                  ";
            }

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@startDate", startDate));
            arr.Add(new SqlParameter("@endDate", endDate));
            if (SellerID != null)
            {
                arr.Add(new SqlParameter("@Seller", SellerID));

            }
            if (CurrencyType != null)
            {

                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            return SqlHelper.ExecuteSql(strSql, arr);
        }

        #endregion

        #region 合同订单签约月份/金额统计

        /// <summary>
        /// 合同订单签约月份/金额统计
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotalBySellerAndDate(string startDate, string endDate, bool isCon, bool isOrd, int? SellerID, int? CurrencyType,
             int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            ArrayList arr = new ArrayList();
            if (isCon)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal) as TotalPrice           ";
                }
                if (CurrencyType == null)
                {

                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal*sc.Rate) as TotalPrice           ";
                }

                strSql += ",ISNULL(e.EmployeeName, '') AS EmployeeName,                                        ";
                strSql += "LEFT(CONVERT(varchar(10), sc.SignDate, 20), 7) as OrderDate   ";

                strSql += "From officedba. SellContract as sc left join                                        ";
                strSql += "officedba.EmployeeInfo AS  e ON sc.Seller = e.ID                                  ";

                strSql += "where sc.CompanyCD=@CompanyCD  and sc.BillStatus<> '1' and sc.State<>2   and LEFT(CONVERT(varchar(10), sc.SignDate, 20), 7)<=@endDate and  LEFT(CONVERT(varchar(10), sc.SignDate, 20), 7)>=@startDate ";
                if (CurrencyType != null)
                {
                    strSql += " and  sc.CurrencyType=@CurrencyType  ";

                }
                if (SellerID != null)
                {
                    strSql += " AND (sc.Seller = @Seller) ";

                }
                strSql += "Group by e.EmployeeName , sc.Seller ,LEFT(CONVERT(varchar(10), sc.SignDate, 20), 7)                                                                                 ";

            }
            if (isCon && isOrd)
            {
                strSql += " union all ";
            }
            if (isOrd)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal, sum(so.RealTotal) as TotalPrice ,         ";
                }
                if (CurrencyType == null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal, sum(so.RealTotal*so.Rate) as TotalPrice ,         ";

                }

                strSql += "ISNULL(e.EmployeeName, '') AS EmployeeName,                                         ";
                strSql += "LEFT(CONVERT(varchar(10), so.OrderDate, 20), 7) as OrderDate   ";

                strSql += "From officedba. SellOrder as so left join                                           ";
                strSql += "officedba.EmployeeInfo AS  e ON so.Seller = e.ID                                    ";


                strSql += "where so.CompanyCD=@CompanyCD   and so.BillStatus<> '1'and so.Status<>3  and LEFT(CONVERT(varchar(10), so.OrderDate, 20), 7)<=@endDate and  LEFT(CONVERT(varchar(10), so.OrderDate, 20), 7)>=@startDate ";
                if (SellerID != null)
                {
                    strSql += " AND (so.Seller = @Seller) ";
                }
                if (CurrencyType != null)
                {
                    strSql += " and  so.CurrencyType=@CurrencyType  ";

                }
                strSql += "Group by e.EmployeeName , so.Seller, LEFT(CONVERT(varchar(10), so.OrderDate, 20), 7)                                                                                  ";
            }

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@startDate", startDate));
            arr.Add(new SqlParameter("@endDate", endDate));
            if (SellerID != null)
            {
                arr.Add(new SqlParameter("@Seller", SellerID));

            }
            if (CurrencyType != null)
            {

                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 合同订单签约月份/金额统计
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotalBySellerAndDate(string startDate, string endDate, bool isCon, bool isOrd, int? SellerID, int? CurrencyType)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            ArrayList arr = new ArrayList();
            if (isCon)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal) as TotalPrice           ";
                }
                if (CurrencyType == null)
                {

                    strSql += "select sum(sc.CountTotal) as CountTotal, sum(sc.RealTotal*sc.Rate) as TotalPrice           ";
                }
                strSql += ",ISNULL(e.EmployeeName, '') AS EmployeeName,                                        ";
                strSql += "LEFT(CONVERT(varchar(10), sc.SignDate, 20), 7) as OrderDate   ";

                strSql += "From officedba. SellContract as sc left join                                        ";
                strSql += "officedba.EmployeeInfo AS  e ON sc.Seller = e.ID                                  ";

                strSql += "where sc.CompanyCD=@CompanyCD  and sc.BillStatus<> '1' and sc.State<>2  and LEFT(CONVERT(varchar(10), sc.SignDate, 20), 7)<=@endDate and  LEFT(CONVERT(varchar(10), sc.SignDate, 20), 7)>=@startDate ";
                if (CurrencyType != null)
                {
                    strSql += " and  sc.CurrencyType=@CurrencyType  ";

                }
                if (SellerID != null)
                {
                    strSql += " AND (sc.Seller = @Seller) ";

                }
                strSql += "Group by e.EmployeeName , sc.Seller ,LEFT(CONVERT(varchar(10), sc.SignDate, 20), 7)                                                                                 ";

            }
            if (isCon && isOrd)
            {
                strSql += " union all ";
            }
            if (isOrd)
            {
                if (CurrencyType != null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal, sum(so.RealTotal) as TotalPrice ,         ";
                }
                if (CurrencyType == null)
                {
                    strSql += "select sum(so.CountTotal) as CountTotal, sum(so.RealTotal*so.Rate) as TotalPrice ,         ";

                }
                strSql += "ISNULL(e.EmployeeName, '') AS EmployeeName,                                         ";
                strSql += "LEFT(CONVERT(varchar(10), so.OrderDate, 20), 7) as OrderDate   ";

                strSql += "From officedba. SellOrder as so left join                                           ";
                strSql += "officedba.EmployeeInfo AS  e ON so.Seller = e.ID                                    ";


                strSql += "where so.CompanyCD=@CompanyCD  and so.BillStatus<> '1'and so.Status<>3  and LEFT(CONVERT(varchar(10), so.OrderDate, 20), 7)<=@endDate and  LEFT(CONVERT(varchar(10), so.OrderDate, 20), 7)>=@startDate  ";
                if (SellerID != null)
                {
                    strSql += " AND (so.Seller = @Seller) ";
                }
                if (CurrencyType != null)
                {
                    strSql += " and  so.CurrencyType=@CurrencyType  ";

                }
                strSql += "Group by e.EmployeeName , so.Seller, LEFT(CONVERT(varchar(10), so.OrderDate, 20), 7)                                                                                  ";
            }

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@startDate", startDate));
            arr.Add(new SqlParameter("@endDate", endDate));
            if (SellerID != null)
            {
                arr.Add(new SqlParameter("@Seller", SellerID));

            }
            if (CurrencyType != null)
            {

                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            return SqlHelper.ExecuteSql(strSql, arr);
        }


        #endregion

        #region 合同订单未尽收款金额按签约月份统计

        /// <summary>
        /// 合同订单未尽收款金额按签约月份统计
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// 
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotal(string startDate, string endDate, int? SellerID, int? CurrencyType,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            ArrayList arr = new ArrayList();

            strSql += "select EmployeeName,isnull(sum(OrderCount),0) as CountTotal,isnull(sum(TotalFee),0) as TotalPrice ,                      ";
            strSql += "LEFT(CONVERT(varchar(10), OrderDate, 20), 7) as OrderDate                                          ";
            strSql += "from ( select isnull(e.EmployeeName,'') as EmployeeName ,(isnull(sd.ProductCount,0)-isnull((                                  ";
            strSql += "select sum(ssd.OutCount) from officedba.SellSendDetail as ssd                                      ";
            strSql += "where FromType='1' and FromBillID=so.id and FromLineNo=sd.SortNo                                   ";
            if (CurrencyType != null)
            {
                strSql += "),0))*sd.TaxPrice*sd.Discount*so.Discount*0.0001 as TotalFee,(isnull(sd.ProductCount,0)-isnull((   ";
            }
            if (CurrencyType == null)
            {

                strSql += "),0))*sd.TaxPrice*sd.Discount*so.Discount*0.0001*so.Rate as TotalFee,(isnull(sd.ProductCount,0)-isnull((   ";
            }

            strSql += "select sum(ssd.OutCount) from officedba.SellSendDetail as ssd                                      ";
            strSql += "where FromType='1' and FromBillID=so.id and FromLineNo=sd.SortNo                                   ";
            strSql += "),0))as OrderCount, so.Seller,so.OrderDate                                                         ";
            strSql += "from officedba.SellOrder as so left  join                                                          ";
            strSql += "officedba.EmployeeInfo as e on so.seller = e.id left join                                          ";
            strSql += "officedba.SellOrderDetail as sd on so.OrderNo=sd.OrderNo and so.CompanyCD=sd.CompanyCD             ";
            strSql += "where so.CompanyCD=@CompanyCD and so.BillStatus<> '1' and so.Status<>3  and LEFT(CONVERT(varchar(10), so.OrderDate, 20), 7) <= @endDate and  LEFT(CONVERT(varchar(10), so.OrderDate, 20),7) >= @startDate ";

            if (SellerID != null)
            {
                strSql += " AND (so.Seller = @Seller) ";
                arr.Add(new SqlParameter("@Seller", SellerID));
            }
            if (CurrencyType != null)
            {
                strSql += " and  so.CurrencyType=@CurrencyType  ";

            }
            strSql += " ) as tt   group by Seller,EmployeeName,LEFT(CONVERT(varchar(10), OrderDate, 20), 7) ";

            if (CurrencyType != null)
            {

                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@startDate", startDate));
            arr.Add(new SqlParameter("@endDate", endDate));

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 合同订单未尽收款金额按签约月份统计
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// 
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotal(string startDate, string endDate, int? SellerID, int? CurrencyType)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            ArrayList arr = new ArrayList();

            strSql += "select EmployeeName,isnull(sum(OrderCount),0) as CountTotal,isnull(sum(TotalFee),0) as TotalPrice ,                      ";
            strSql += "LEFT(CONVERT(varchar(10), OrderDate, 20), 7) as OrderDate                                          ";
            strSql += "from ( select isnull(e.EmployeeName,'') as EmployeeName ,(isnull(sd.ProductCount,0)-isnull((                                  ";
            strSql += "select sum(ssd.OutCount) from officedba.SellSendDetail as ssd                                      ";
            strSql += "where FromType='1' and FromBillID=so.id and FromLineNo=sd.SortNo                                   ";
            if (CurrencyType != null)
            {
                strSql += "),0))*sd.TaxPrice*sd.Discount*so.Discount*0.0001 as TotalFee,(isnull(sd.ProductCount,0)-isnull((   ";
            }
            if (CurrencyType == null)
            {

                strSql += "),0))*sd.TaxPrice*sd.Discount*so.Discount*0.0001*so.Rate as TotalFee,(isnull(sd.ProductCount,0)-isnull((   ";
            }
            strSql += "select sum(ssd.OutCount) from officedba.SellSendDetail as ssd                                      ";
            strSql += "where FromType='1' and FromBillID=so.id and FromLineNo=sd.SortNo                                   ";
            strSql += "),0))as OrderCount, so.Seller,so.OrderDate                                                         ";
            strSql += "from officedba.SellOrder as so left  join                                                          ";
            strSql += "officedba.EmployeeInfo as e on so.seller = e.id left join                                          ";
            strSql += "officedba.SellOrderDetail as sd on so.OrderNo=sd.OrderNo and so.CompanyCD=sd.CompanyCD             ";
            strSql += "where so.CompanyCD=@CompanyCD and so.BillStatus<> '1'  and so.Status<>3  and LEFT(CONVERT(varchar(10), so.OrderDate, 20), 7) <= @endDate and  LEFT(CONVERT(varchar(10), so.OrderDate, 20),7) >= @startDate ";
            if (CurrencyType != null)
            {
                strSql += " and  so.CurrencyType=@CurrencyType  ";

            }
            if (SellerID != null)
            {
                strSql += " AND (so.Seller = @Seller) ";
                arr.Add(new SqlParameter("@Seller", SellerID));
            }
            strSql += " ) as tt   group by Seller,EmployeeName,LEFT(CONVERT(varchar(10), OrderDate, 20), 7) ";

            if (CurrencyType != null)
            {

                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@startDate", startDate));
            arr.Add(new SqlParameter("@endDate", endDate));

            return SqlHelper.ExecuteSql(strSql, arr);
        }

        #endregion

        #region 开票月度统计

        /// <summary>
        /// 开票月度统计
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="strType">发票类型</param>
        /// <returns></returns>
        public static DataTable GetTotalBybill(string startDate, string endDate, string strType, int? CurrencyType,
             int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            ArrayList arr = new ArrayList();

            if (CurrencyType != null)
            {
                strSql += "SELECT isnull(sum(b.YAccounts),0) as TotalPrice,                                                   ";
            }
            if (CurrencyType == null)
            {

                strSql += "SELECT isnull(sum(b.YAccounts*s.Rate),0) as TotalPrice,                                                   ";
            }

            strSql += "LEFT(CONVERT(varchar(10), b.CreateDate, 20),7) as OrderDate,                                              ";
            strSql += "case b.invoiceType  when '1' then '增值税发票'  when '2' then '普通地税'                                  ";
            strSql += "when '3' then '普通国税'   when '4' then '收据'  end as TypeName                                          ";
            strSql += "FROM officedba.BlendingDetails as b   left  join                                                          ";
            strSql += "officedba.SellOrder as s on  b.BillingType = '1'  and b.BillCD=s.OrderNo and b.CompanyCD=s.CompanyCD      ";


            strSql += "WHERE b.CompanyCD=@CompanyCD and LEFT(CONVERT(varchar(10), b.CreateDate, 20),7) <=@endDate and LEFT(CONVERT(varchar(10), b.CreateDate, 20),7) >=@startDate    ";
            if (strType != null)
            {
                strSql += " and  b.InvoiceType = @InvoiceType ";
                arr.Add(new SqlParameter("@InvoiceType", strType));
            }
            if (CurrencyType != null)
            {
                strSql += " and  s.CurrencyType=@CurrencyType  ";
                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            strSql += "group by b.invoiceType,LEFT(CONVERT(varchar(10), b.CreateDate, 20),7)                                   ";


            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@startDate", startDate));
            arr.Add(new SqlParameter("@endDate", endDate));

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 开票月度统计
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="strType">发票类型</param>
        /// <returns></returns>
        public static DataTable GetTotalBybill(string startDate, string endDate, string strType, int? CurrencyType)
        {
            string strCompanyCD = string.Empty;//单位编号
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            ArrayList arr = new ArrayList();

            if (CurrencyType != null)
            {
                strSql += "SELECT isnull(sum(b.YAccounts),0) as TotalPrice,                                                   ";
            }
            if (CurrencyType == null)
            {

                strSql += "SELECT isnull(sum(b.YAccounts*s.Rate),0) as TotalPrice,                                                   ";
            }

            strSql += "LEFT(CONVERT(varchar(10), b.CreateDate, 20),7) as OrderDate,                                              ";
            strSql += "case b.invoiceType  when '1' then '增值税发票'  when '2' then '普通地税'                                  ";
            strSql += "when '3' then '普通国税'   when '4' then '收据'  end as TypeName                                          ";
            strSql += "FROM officedba.BlendingDetails as b   left  join                                                          ";
            strSql += "officedba.SellOrder as s on  b.BillingType = '1'  and b.BillCD=s.OrderNo and b.CompanyCD=s.CompanyCD      ";


            strSql += "WHERE b.CompanyCD=@CompanyCD and LEFT(CONVERT(varchar(10), b.CreateDate, 20),7) <=@endDate and LEFT(CONVERT(varchar(10), b.CreateDate, 20),7) >=@startDate    ";
            if (strType != null)
            {
                strSql += " and  b.InvoiceType = @InvoiceType ";
                arr.Add(new SqlParameter("@InvoiceType", strType));
            }
            if (CurrencyType != null)
            {
                strSql += " and  s.CurrencyType=@CurrencyType  ";
                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            strSql += "group by b.invoiceType,LEFT(CONVERT(varchar(10), b.CreateDate, 20),7)                                   ";

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@startDate", startDate));
            arr.Add(new SqlParameter("@endDate", endDate));

            return SqlHelper.ExecuteSql(strSql, arr);
        }

        #endregion



        #endregion

        #region 新报表

        /// <summary>
        /// 销售合同数量部门分布
        /// </summary>
        /// <param name="Status">销售合同状态</param>
        /// <param name="Type">合同业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByDeptNum(string Status, string Type, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(b.DeptName) as Name,count(1) as Counts,a.selldeptId as DeptId  ");
            sb.Append(" from officedba. SellContract as a left join officedba.DeptInfo as b on a.sellDeptId=b.Id  ");
            sb.Append(" where a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");
            if (Status != "")
            {
                sb.Append("and a.State=");
                sb.Append(Status);
            }
            if (Type != "")
            {
                sb.Append("and a.BusiType=");
                sb.Append(Type);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.SignDate>Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.SignDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }

            sb.Append(" group by a.selldeptId ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售合同数量人员分布
        /// </summary>
        /// <param name="Status">销售合同状态</param>
        /// <param name="Type">合同业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByPersonNum(string Status, string Type, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(b.EmployeeName) as Name,count(1) as Counts,a.Seller ");
            sb.Append(" from officedba. SellContract as a left join officedba.EmployeeInfo as b on a.Seller=b.Id  ");
            sb.Append(" where a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (Status != "")
            {
                sb.Append("and a.State=");
                sb.Append(Status);
            }
            if (Type != "")
            {
                sb.Append("and a.BusiType=");
                sb.Append(Type);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.SignDate>Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.SignDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by a.Seller ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售合同数量状态分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByStateNum(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select CASE State WHEN '1' THEN '执行中' WHEN '2' THEN '意外终止' WHEN '3'  THEN '已执行' when '4' then '已到期' END as Name,count(1) as Counts,State ");
            sb.Append(" from officedba. SellContract ");
            sb.Append(" where companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (DeptOrEmployeeId != "")
            {
                if (GroupType == "Dept")
                {
                    sb.Append("and SellDeptId in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(") ");
                }
                else
                {
                    sb.Append("and Seller in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(")");
                }
            }
            if (BeginDate != "")
            {
                sb.Append(" and SignDate>Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and SignDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by  State ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售合同数量类型分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByTypeNum(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select CASE BusiType WHEN '1' THEN '普通销售' WHEN '2' THEN '委托代销' WHEN '3'  THEN '直运' WHEN '4'  THEN '零售' WHEN '5'  THEN '销售调拨'  END as Name,count(1) as Counts,BusiType ");
            sb.Append(" from officedba. SellContract ");
            sb.Append(" where companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (DeptOrEmployeeId != "")
            {
                if (GroupType == "Dept")
                {
                    sb.Append("and SellDeptId in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(")");
                }
                else
                {
                    sb.Append("and Seller in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(")");
                }
            }
            if (BeginDate != "")
            {
                sb.Append("and SignDate>Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and SignDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by BusiType ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售合同走势数量分析
        /// </summary>
        /// <param name="FromType">销售来源</param>
        /// <param name="DeptId">所属部门</param>
        /// <param name="EmployeeId">部门人员</param>
        /// <param name="DateType">时间分类</param>
        /// <param name="State">销售合同状态</param>
        /// <param name="BusiType">销售合同类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByTypeTrend(string GroupType, string DeptOrEmployeeId, string DateType, string State, string BusiType, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string SearchField = "";
            if (DateType == "1")
            {
                SearchField = "dateName(year,signdate)+'年'";
            }
            else if (DateType == "2")
            {
                SearchField = "dateName(year,signdate)+'年-'+dateName(month,signdate)+'月'";
            }
            else
            {
                SearchField = "dateName(year,signdate)+'年-'+dateName(week,signdate)+'周'";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append(SearchField);
            sb.Append(" as Name,count(1) as Counts ");
            sb.Append(" from officedba. SellContract ");
            sb.Append(" where companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (State != "")
            {
                sb.Append(" and State=");
                sb.Append(State);
            }
            if (BusiType != "")
            {
                sb.Append(" and BusiType=");
                sb.Append(BusiType);
            }

            if (DeptOrEmployeeId != "")
            {
                if (GroupType == "Dept")
                {
                    sb.Append("and SellDeptId in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(")");
                }
                else
                {
                    sb.Append("and Seller in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(")");
                }
            }

            if (BeginDate != "")
            {
                sb.Append("and SignDate>Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and SignDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by  ");
            sb.Append(SearchField);
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售合同金额部门分布
        /// </summary>
        /// <param name="Status">销售合同状态</param>
        /// <param name="Type">合同业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByDeptPrice(string Status, string Type, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(b.DeptName) as Name,sum(isnull(RealTotal,0)*isnull(rate,1)) as Counts,a.selldeptId as DeptId  ");
            sb.Append(" from officedba. SellContract as a left join officedba.DeptInfo as b on a.sellDeptId=b.Id  ");
            sb.Append(" where a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (Status != "")
            {
                sb.Append("and a.State=");
                sb.Append(Status);
            }
            if (Type != "")
            {
                sb.Append("and a.BusiType=");
                sb.Append(Type);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.SignDate>Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");
            }
            if (EndDate != "")
            {
                sb.Append("and a.SignDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }


            sb.Append(" group by a.selldeptId ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售合同金额人员分布
        /// </summary>
        /// <param name="Status">销售合同状态</param>
        /// <param name="Type">合同业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByPersonPrice(string Status, string Type, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(b.EmployeeName) as Name,sum(isnull(RealTotal,0)*isnull(rate,1)) as Counts,a.Seller ");
            sb.Append(" from officedba. SellContract as a left join officedba.EmployeeInfo as b on a.Seller=b.Id  ");
            sb.Append(" where a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (Status != "")
            {
                sb.Append("and a.State=");
                sb.Append(Status);
            }
            if (Type != "")
            {
                sb.Append("and a.BusiType=");
                sb.Append(Type);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.SignDate>Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.SignDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by a.Seller ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }


        /// <summary>
        /// 销售合同数量状态分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByStatePrice(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select CASE State WHEN '1' THEN '执行中' WHEN '2' THEN '意外终止' WHEN '3'  THEN '已执行' when '4' then '已到期' END as Name,sum(isnull(RealTotal,0)*isnull(rate,1)) as Counts,State ");
            sb.Append(" from officedba. SellContract ");
            sb.Append(" where companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (DeptOrEmployeeId != "")
            {
                if (GroupType == "Dept")
                {
                    sb.Append("and SellDeptId in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(") ");
                }
                else
                {
                    sb.Append("and Seller in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(")");
                }
            }
            if (BeginDate != "")
            {
                sb.Append(" and SignDate>Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and SignDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by  State ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售合同数量类型分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByTypePrice(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select CASE BusiType WHEN '1' THEN '普通销售' WHEN '2' THEN '委托代销' WHEN '3'  THEN '直运' WHEN '4'  THEN '零售' WHEN '5'  THEN '销售调拨'  END as Name,sum(isnull(RealTotal,0)*isnull(rate,1)) as Counts,BusiType ");
            sb.Append(" from officedba. SellContract ");
            sb.Append(" where companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (DeptOrEmployeeId != "")
            {
                if (GroupType == "Dept")
                {
                    sb.Append("and SellDeptId in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(")");
                }
                else
                {
                    sb.Append("and Seller in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(")");
                }
            }
            if (BeginDate != "")
            {
                sb.Append("and SignDate>Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and SignDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by BusiType ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售合同走势数量分析
        /// </summary>
        /// <param name="FromType">销售来源</param>
        /// <param name="DeptId">所属部门</param>
        /// <param name="EmployeeId">部门人员</param>
        /// <param name="DateType">时间分类</param>
        /// <param name="State">销售合同状态</param>
        /// <param name="BusiType">销售合同类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByTrendPrice(string GroupType, string DeptOrEmployeeId, string DateType, string State, string BusiType, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string SearchField = "";
            if (DateType == "1")
            {
                SearchField = "dateName(year,signdate)+'年'";
            }
            else if (DateType == "2")
            {
                SearchField = "dateName(year,signdate)+'年-'+dateName(month,signdate)+'月'";
            }
            else
            {
                SearchField = "dateName(year,signdate)+'年-'+dateName(week,signdate)+'周'";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append(SearchField);
            sb.Append(" as Name,sum(isnull(RealTotal,0)*isnull(rate,1)) as Counts ");
            sb.Append(" from officedba. SellContract ");
            sb.Append(" where companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (State != "")
            {
                sb.Append(" and State=");
                sb.Append(State);
            }
            if (BusiType != "")
            {
                sb.Append(" and BusiType=");
                sb.Append(BusiType);
            }

            if (DeptOrEmployeeId != "")
            {
                if (GroupType == "Dept")
                {
                    sb.Append("and SellDeptId in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(")");
                }
                else
                {
                    sb.Append("and Seller in (");
                    sb.Append(DeptOrEmployeeId);
                    sb.Append(")");
                }
            }

            if (BeginDate != "")
            {
                sb.Append("and SignDate>Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and SignDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by  ");
            sb.Append(SearchField);
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 获取合同列表 
        /// </summary>
        /// <param name="sellContractModel">sellContractModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetSellContractDetail(int Status, int Type, string Name, string DeptAndPerson, string BeginDate, string EndDate, string DataType, string GroupType, string DeptOrEmployeeId, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {

            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT s.Id,s.Title, s.ContractNo,CASE s.FromType WHEN '0' THEN '无来源' WHEN '1' THEN '销售报价单' WHEN '2' THEN '销售机会' END AS FromTypeText, ");/*Id、 合同编号、合同主题、源单类型 */
            sb.Append(" case s.FromType when '1' then  ISNULL(so.OfferNo, '') when '2' then isnull(sc.ChanceNo,'') end as OfferNo, "); /* 源单编号 */
            sb.Append(" ISNULL(e.EmployeeName, '') AS EmployeeName,isnull( c.CustName,'') as CustName ,Convert(decimal(22," + jingdu + "),isnull(s.TotalFee,0)*isnull(s.rate,1))  as TotalFee, "); /* 业务员、客户、合同金额 */
            sb.Append(" CASE s.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更' WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText,  ");/*状态*/

            sb.Append(" CASE s.State WHEN '1' THEN '执行中' WHEN '2' THEN '意外终止' WHEN '3'  THEN '已执行' when '4' then '已到期' END AS stateText, ");/*合同状态*/

            sb.Append(" case (SELECT TOP 1 FlowStatus  FROM officedba.FlowInstance  WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 2 ");
            sb.Append(" ORDER BY ModifiedDate DESC) when null then '' when 1 THEN '待审批' when 2 THEN '审批中' when 3 THEN '审批通过' when 4 THEN '审批不通过' ");

            sb.Append(" when 5 THEN '撤销审批' end AS FlowInstanceText,fc.DeptName "); /*审批状态*/
            sb.Append(" FROM officedba.SellContract AS s LEFT OUTER JOIN ");
            sb.Append(" officedba.SellOffer AS so ON s.FromBillID = so.ID and s.FromType='1' LEFT OUTER JOIN  ");
            sb.Append(" officedba.SellChance AS sc ON s.FromBillID = sc.ID and s.FromType='2' LEFT OUTER JOIN ");
            sb.Append(" officedba.EmployeeInfo AS e ON s.Seller = e.ID LEFT OUTER JOIN ");
            sb.Append(" officedba.deptinfo as fc on fc.Id=s.SellDeptId  LEFT OUTER JOIN ");
            sb.Append(" officedba.CustInfo AS c ON s.CustID = c.ID  where 1=1  and s.CompanyCD=@CompanyCD  ");

            string strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            /*合同状态*/

            if (Status != 0)
            {
                sb.Append("  and s.State= @State ");
                arr.Add(new SqlParameter("@State", Status));
            }

            /*业务类型*/
            if (Type != 0)
            {
                sb.Append("  and s.BusiType= @BusiType ");
                arr.Add(new SqlParameter("@BusiType", Type));
            }

            /*部门*/
            if (GroupType == "Dept")
            {
                if (DeptOrEmployeeId != "")
                {
                    sb.Append("  and fc.Id= @DeptName ");
                    arr.Add(new SqlParameter("@DeptName", DeptOrEmployeeId));
                }
            }
            /*人员*/
            if (GroupType == "Person")
            {
                if (DeptOrEmployeeId != "")
                {
                    sb.Append("  and e.Id= @EmployeeName ");
                    arr.Add(new SqlParameter("@EmployeeName", DeptOrEmployeeId));
                }
            }

            /*开始时间*/
            if (BeginDate != "")
            {
                sb.Append("and s.SignDate>Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            /*结束时间*/
            if (EndDate != "")
            {
                sb.Append("and s.SignDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }

            if (DataType == "1")
            {
                sb.Append("and (dateName(year,s.SignDate)+'年')='");
                sb.Append(Name);
                sb.Append("'");
            }
            else if (DataType == "2")
            {
                sb.Append("and (dateName(year,s.signdate)+'年-'+dateName(month,s.signdate)+'月')='");
                sb.Append(Name);
                sb.Append("'");
            }
            else
            {
                sb.Append("and (dateName(year,s.signdate)+'年-'+dateName(week,s.signdate)+'周')='");
                sb.Append(Name);
                sb.Append("'");
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(sb.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }


        /// <summary>
        /// 获取合同列表 
        /// </summary>
        /// <param name="sellContractModel">sellContractModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetSellContractDetail(int Status, int Type, string Name, string DeptAndPerson, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT s.Id,s.Title, s.ContractNo,CASE s.FromType WHEN '0' THEN '无来源' WHEN '1' THEN '销售报价单' WHEN '2' THEN '销售机会' END AS FromTypeText, ");/*Id、 合同编号、合同主题、源单类型 */
            sb.Append(" case s.FromType when '1' then  ISNULL(so.OfferNo, '') when '2' then isnull(sc.ChanceNo,'') end as OfferNo, "); /* 源单编号 */
            sb.Append(" ISNULL(e.EmployeeName, '') AS EmployeeName,isnull( c.CustName,'') as CustName, isnull(s.TotalFee,0)*isnull(s.rate,1) TotalFee, "); /* 业务员、客户、合同金额 */
            sb.Append(" CASE s.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更' WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText,  ");/*状态*/

            sb.Append(" CASE s.State WHEN '1' THEN '执行中' WHEN '2' THEN '意外终止' WHEN '3'  THEN '已执行' when '4' then '已到期' END AS stateText, ");/*合同状态*/

            sb.Append(" case (SELECT TOP 1 FlowStatus  FROM officedba.FlowInstance  WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 2 ");
            sb.Append(" ORDER BY ModifiedDate DESC) when null then '' when 1 THEN '待审批' when 2 THEN '审批中' when 3 THEN '审批通过' when 4 THEN '审批不通过' ");

            sb.Append(" when 5 THEN '撤销审批' end AS FlowInstanceText,fc.DeptName "); /*审批状态*/
            sb.Append(" FROM officedba.SellContract AS s LEFT OUTER JOIN ");
            sb.Append(" officedba.SellOffer AS so ON s.FromBillID = so.ID and s.FromType='1' LEFT OUTER JOIN  ");
            sb.Append(" officedba.SellChance AS sc ON s.FromBillID = sc.ID and s.FromType='2' LEFT OUTER JOIN ");
            sb.Append(" officedba.EmployeeInfo AS e ON s.Seller = e.ID LEFT OUTER JOIN ");
            sb.Append(" officedba.deptinfo as fc on fc.Id=s.SellDeptId  LEFT OUTER JOIN ");
            sb.Append(" officedba.CustInfo AS c ON s.CustID = c.ID  where 1=1  and s.CompanyCD=@CompanyCD  ");

            string strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            /*合同状态*/

            if (Status != 0)
            {
                sb.Append("  and s.State= @State ");
                arr.Add(new SqlParameter("@State", Status));
            }

            /*业务类型*/
            if (Type != 0)
            {
                sb.Append("  and s.BusiType= @BusiType ");
                arr.Add(new SqlParameter("@BusiType", Type));
            }

            /*部门*/
            if (DeptAndPerson == "Dept")
            {
                if (Name != "")
                {
                    sb.Append("  and fc.Id= @DeptName ");
                    arr.Add(new SqlParameter("@DeptName", Name));
                }
            }
            /*人员*/
            if (DeptAndPerson == "Person")
            {
                if (Name != "")
                {
                    sb.Append("  and e.Id= @EmployeeName ");
                    arr.Add(new SqlParameter("@EmployeeName", Name));
                }
            }

            /*开始时间*/
            if (BeginDate != "")
            {
                sb.Append("and s.SignDate>Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            /*结束时间*/
            if (EndDate != "")
            {
                sb.Append("and s.SignDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }

            return SqlHelper.CreateSqlByPageExcuteSqlArr(sb.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }
        /// <summary>
        /// 获取合同列表 
        /// </summary>
        /// <param name="sellContractModel">sellContractModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetSellContractDetail(int Status, int Type, string Name, string DeptAndPerson, string BeginDate, string EndDate)
        {


            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT s.Id,s.Title, s.ContractNo,");/*Id、 合同编号、合同主题、*/
            sb.Append("dateName(year,s.SignDate)+'年' as SignYear,");/*年*/
            sb.Append("dateName(year,s.signdate)+'年-'+dateName(month,s.signdate)+'月' as SignMonth,");/*月*/
            sb.Append("dateName(year,s.signdate)+'年-'+dateName(week,s.signdate)+'周' as SignWeek,");/*周*/
            sb.Append(" CASE s.BusiType WHEN '1' THEN '普通销售' WHEN '2' THEN '委托代销' WHEN '3'  THEN '直运' WHEN '4'  THEN '零售' WHEN '5'  THEN '销售调拨'  END as BusiType, ");/*类型*/
            sb.Append(" CASE s.FromType WHEN '0' THEN '无来源' WHEN '1' THEN '销售报价单' WHEN '2' THEN '销售机会' END AS FromTypeText,");/*源单类型 */
            sb.Append(" case s.FromType when '1' then  ISNULL(so.OfferNo, '') when '2' then isnull(sc.ChanceNo,'') end as OfferNo, "); /* 源单编号 */
            sb.Append(" ISNULL(e.EmployeeName, '') AS EmployeeName,isnull( c.CustName,'') as CustName,Convert(decimal(22," + jingdu + "),isnull( s.TotalFee,0)) TotalFee ,"); /* 业务员、客户、合同金额 */
            sb.Append(" CASE s.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更' WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText,  ");/*状态*/

            sb.Append(" CASE s.State WHEN '1' THEN '执行中' WHEN '2' THEN '意外终止' WHEN '3'  THEN '已执行' when '4' then '已到期' END AS stateText, ");/*合同状态*/

            sb.Append(" case (SELECT TOP 1 FlowStatus  FROM officedba.FlowInstance  WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 2 ");
            sb.Append(" ORDER BY ModifiedDate DESC) when null then '' when 1 THEN '待审批' when 2 THEN '审批中' when 3 THEN '审批通过' when 4 THEN '审批不通过' ");

            sb.Append(" when 5 THEN '撤销审批' end AS FlowInstanceText,fc.DeptName "); /*审批状态*/
            sb.Append(" FROM officedba.SellContract AS s LEFT OUTER JOIN ");
            sb.Append(" officedba.SellOffer AS so ON s.FromBillID = so.ID and s.FromType='1' LEFT OUTER JOIN  ");
            sb.Append(" officedba.SellChance AS sc ON s.FromBillID = sc.ID and s.FromType='2' LEFT OUTER JOIN ");
            sb.Append(" officedba.EmployeeInfo AS e ON s.Seller = e.ID LEFT OUTER JOIN ");
            sb.Append(" officedba.deptinfo as fc on fc.Id=s.SellDeptId  LEFT OUTER JOIN ");
            sb.Append(" officedba.CustInfo AS c ON s.CustID = c.ID  where 1=1  and s.CompanyCD=@CompanyCD  ");

            string strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            /*合同状态*/

            if (Status != 0)
            {
                sb.Append("  and s.State= @State ");
                arr.Add(new SqlParameter("@State", Status));
            }

            /*业务类型*/
            if (Type != 0)
            {

                sb.Append("  and s.BusiType= @BusiType ");
                arr.Add(new SqlParameter("@BusiType", Type));
            }

            /*部门*/
            if (DeptAndPerson == "Dept")
            {
                if (Name != "")
                {
                    sb.Append("  and fc.Id= @DeptName ");
                    arr.Add(new SqlParameter("@DeptName", Name));
                }
            }
            /*人员*/
            if (DeptAndPerson == "Person")
            {
                if (Name != "")
                {
                    sb.Append("  and e.Id= @EmployeeName ");
                    arr.Add(new SqlParameter("@EmployeeName", Name));
                }
            }
            /*开始时间*/
            if (BeginDate != "")
            {
                sb.Append("and s.SignDate>Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            /*结束时间*/
            if (EndDate != "")
            {
                sb.Append("and s.SignDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }

            return SqlHelper.ExecuteSql(sb.ToString(), arr);
        }



        #endregion

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

            string strSql = "select * from  officedba.sellmodule_report_SellContract WHERE (ContractNo = @ContractNo) AND (CompanyCD = @CompanyCD)";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@ContractNo",OrderNo)
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

            string strSql = "select  * from  officedba.sellmodule_report_SellContractDetail WHERE (ContractNo = @ContractNo) AND (CompanyCD = @CompanyCD) order by SortNo asc";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@ContractNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(SellContractModel model, Hashtable htExtAttr, TransactionManager tran)
        {
            try
            {
                string strSql = string.Empty;
                strSql = "UPDATE officedba.SellContract set ";

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
                strSql += " where CompanyCD = @CompanyCD  AND ContractNo = @ContractNo";
                parameters[i] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
                parameters[i + 1] = SqlHelper.GetParameter("@ContractNo", model.ContractNo);
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


        #region 若是执行状态的销售合同已超过限期时，更新销售合同状态为“已到期”
        /// <summary>
        /// 若是执行状态的销售合同已超过限期时，更新销售合同状态为“已到期”
        /// 只对单据状态为执行或结单，合同状态为执行中 的单据进行处理
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        public static bool UpDateStatus(string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            bool retVal = false;
            strSql.AppendLine(" update officedba.SellContract set State=@State");
            strSql.AppendLine("where CompanyCD=@CompanyCD and EndDate<@EndDate ");
            strSql.AppendLine("and State='1' and (BillStatus='2'or BillStatus='4' or BillStatus='5') ");

            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD),
                                    new SqlParameter("@EndDate",System.DateTime.Now.ToString()),
                                    new SqlParameter("@State",'4')
                                   };

            int iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql.ToString(), param));
            if (iCount > 0)
            {
                retVal = true;
            }
            return retVal;
        }
        #endregion
    }
}

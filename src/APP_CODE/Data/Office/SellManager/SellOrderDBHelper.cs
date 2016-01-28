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
    public class SellOrderDBHelper
    {
        #region 添加、修改、删除相关操作
        #region 添加新单据(销售订单)
        /// <summary>
        /// 添加新单据
        /// </summary>
        /// <returns></returns>
        public static bool Insert(Hashtable ht, SellOrderModel sellOrderModel, List<SellOrderDetailModel> sellOrderDetailModellList,
            List<SellOrderFeeDetailModel> sellOrderFeeDetailModelList, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            //判断单据编号是否存在
            if (NoIsExist(sellOrderModel.OrderNo))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    InsertSellOrder(ht,sellOrderModel, tran);
                    InsertSellOrderDetail(sellOrderDetailModellList, tran);
                    InsertSellOrderFeeDetail(sellOrderFeeDetailModelList, tran);
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

        #region 修改销售订单
        /// <summary>
        /// 修改销售订单
        /// </summary>
        /// <returns></returns>
        public static bool Update(Hashtable ht, SellOrderModel sellOrderModel, List<SellOrderDetailModel> sellOrderDetailModellList,
            List<SellOrderFeeDetailModel> sellOrderFeeDetailModelList, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            if (IsUpdate(sellOrderModel.OrderNo))
            {
                string strSql = "delete from officedba.SellOrderDetail where  OrderNo=@OrderNo  and CompanyCD=@CompanyCD";
                SqlParameter[] paras = { new SqlParameter("@OrderNo", sellOrderModel.OrderNo), new SqlParameter("@CompanyCD", sellOrderModel.CompanyCD) };
                string strSql1 = "delete from officedba.SellOrderFeeDetail where  OrderNo=@OrderNo  and CompanyCD=@CompanyCD";
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {

                    UpdateSellOrder(ht, sellOrderModel, tran);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql1.ToString(), paras);
                    InsertSellOrderDetail(sellOrderDetailModellList, tran);
                    InsertSellOrderFeeDetail(sellOrderFeeDetailModelList, tran);
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
        /// <param name="sellOrderModel"></param>
        /// <param name="tran"></param>
        private static void UpdateSellOrder(Hashtable htExtAttr, SellOrderModel sellOrderModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            #region sql语句
            strSql.Append("update officedba.SellOrder set ");
            strSql.Append("CustID=@CustID,");
            strSql.Append("CustTel=@CustTel,");
            strSql.Append("Title=@Title,");
            strSql.Append("FromType=@FromType,");
            strSql.Append("FromBillID=@FromBillID,");
            strSql.Append("Seller=@Seller,");
            strSql.Append("SellDeptId=@SellDeptId,");
            strSql.Append("SellType=@SellType,");
            strSql.Append("BusiType=@BusiType,");
            strSql.Append("OrderMethod=@OrderMethod,");
            strSql.Append("PayType=@PayType,");
            strSql.Append("MoneyType=@MoneyType,");
            strSql.Append("CarryType=@CarryType,");
            strSql.Append("TakeType=@TakeType,");
            strSql.Append("CurrencyType=@CurrencyType,");
            strSql.Append("Rate=@Rate,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("Tax=@Tax,");
            strSql.Append("TotalFee=@TotalFee,");
            strSql.Append("Discount=@Discount,");
            strSql.Append("SaleFeeTotal=@SaleFeeTotal,");
            strSql.Append("DiscountTotal=@DiscountTotal,");
            strSql.Append("RealTotal=@RealTotal,");
            strSql.Append("isAddTax=@isAddTax,");
            strSql.Append("CountTotal=@CountTotal,");
            strSql.Append("SendDate=@SendDate,");
            strSql.Append("OrderDate=@OrderDate,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("TheyDelegate=@TheyDelegate,");
            strSql.Append("OurDelegate=@OurDelegate,");
            strSql.Append("Status=@Status,");
            strSql.Append("PayRemark=@PayRemark,");
            strSql.Append("DeliverRemark=@DeliverRemark,");
            strSql.Append("PackTransit=@PackTransit,");
            strSql.Append("StatusNote=@StatusNote,");
            strSql.Append("CustOrderNo=@CustOrderNo,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("BillStatus=@BillStatus,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(",CanViewUser=@CanViewUser");
            strSql.Append(",ProjectID=@ProjectID");
            strSql.Append(" where CompanyCD=@CompanyCD and OrderNo=@OrderNo ");
            #endregion
            #region 参数
            //SqlParameter[] parameters = {
            //        new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
            //        new SqlParameter("@CustID", SqlDbType.Int,4),
            //        new SqlParameter("@CustTel", SqlDbType.VarChar,100),
            //        new SqlParameter("@OrderNo", SqlDbType.VarChar,50),
            //        new SqlParameter("@Title", SqlDbType.VarChar,100),
            //        new SqlParameter("@FromType", SqlDbType.Char,1),
            //        new SqlParameter("@FromBillID", SqlDbType.Int,4),
            //        new SqlParameter("@Seller", SqlDbType.Int,4),
            //        new SqlParameter("@SellDeptId", SqlDbType.Int,4),
            //        new SqlParameter("@SellType", SqlDbType.Int,4),
            //        new SqlParameter("@BusiType", SqlDbType.Char,1),
            //        new SqlParameter("@OrderMethod", SqlDbType.Int,4),
            //        new SqlParameter("@PayType", SqlDbType.Int,4),
            //        new SqlParameter("@MoneyType", SqlDbType.Int,4),
            //        new SqlParameter("@CarryType", SqlDbType.Int,4),
            //        new SqlParameter("@TakeType", SqlDbType.Int,4),
            //        new SqlParameter("@CurrencyType", SqlDbType.Int,4),
            //        new SqlParameter("@Rate", SqlDbType.Decimal,9),
            //        new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
            //        new SqlParameter("@Tax", SqlDbType.Decimal,9),
            //        new SqlParameter("@TotalFee", SqlDbType.Decimal,9),
            //        new SqlParameter("@Discount", SqlDbType.Decimal,9),
            //        new SqlParameter("@SaleFeeTotal", SqlDbType.Decimal,9),
            //        new SqlParameter("@DiscountTotal", SqlDbType.Decimal,9),
            //        new SqlParameter("@RealTotal", SqlDbType.Decimal,9),
            //        new SqlParameter("@isAddTax", SqlDbType.Char,1),
            //        new SqlParameter("@CountTotal", SqlDbType.Decimal,9),
            //        new SqlParameter("@SendDate", SqlDbType.DateTime),
            //        new SqlParameter("@OrderDate", SqlDbType.DateTime),
            //        new SqlParameter("@StartDate", SqlDbType.DateTime),
            //        new SqlParameter("@EndDate", SqlDbType.DateTime),
            //        new SqlParameter("@TheyDelegate", SqlDbType.VarChar,50),
            //        new SqlParameter("@OurDelegate", SqlDbType.Int,4),
            //        new SqlParameter("@Status", SqlDbType.Char,1),
            //        new SqlParameter("@PayRemark", SqlDbType.VarChar,200),
            //        new SqlParameter("@DeliverRemark", SqlDbType.VarChar,200),
            //        new SqlParameter("@PackTransit", SqlDbType.VarChar,200),
            //        new SqlParameter("@StatusNote", SqlDbType.VarChar,100),
            //        new SqlParameter("@CustOrderNo", SqlDbType.VarChar,100),
            //        new SqlParameter("@Remark", SqlDbType.VarChar,200),
            //        new SqlParameter("@Attachment", SqlDbType.VarChar,150),
            //        new SqlParameter("@BillStatus", SqlDbType.Char,1),
            //        new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20)};
            //parameters[0].Value = sellOrderModel.CompanyCD;
            //parameters[1].Value = sellOrderModel.CustID;
            //parameters[2].Value = sellOrderModel.CustTel;
            //parameters[3].Value = sellOrderModel.OrderNo;
            //parameters[4].Value = sellOrderModel.Title;
            //parameters[5].Value = sellOrderModel.FromType;
            //parameters[6].Value = sellOrderModel.FromBillID;
            //parameters[7].Value = sellOrderModel.Seller;
            //parameters[8].Value = sellOrderModel.SellDeptId;
            //parameters[9].Value = sellOrderModel.SellType;
            //parameters[10].Value = sellOrderModel.BusiType;
            //parameters[11].Value = sellOrderModel.OrderMethod;
            //parameters[12].Value = sellOrderModel.PayType;
            //parameters[13].Value = sellOrderModel.MoneyType;
            //parameters[14].Value = sellOrderModel.CarryType;
            //parameters[15].Value = sellOrderModel.TakeType;
            //parameters[16].Value = sellOrderModel.CurrencyType;
            //parameters[17].Value = sellOrderModel.Rate;
            //parameters[18].Value = sellOrderModel.TotalPrice;
            //parameters[19].Value = sellOrderModel.Tax;
            //parameters[20].Value = sellOrderModel.TotalFee;
            //parameters[21].Value = sellOrderModel.Discount;
            //parameters[22].Value = sellOrderModel.SaleFeeTotal;
            //parameters[23].Value = sellOrderModel.DiscountTotal;
            //parameters[24].Value = sellOrderModel.RealTotal;
            //parameters[25].Value = sellOrderModel.isAddTax;
            //parameters[26].Value = sellOrderModel.CountTotal;
            //parameters[27].Value = sellOrderModel.SendDate;
            //parameters[28].Value = sellOrderModel.OrderDate;
            //parameters[29].Value = sellOrderModel.StartDate;
            //parameters[30].Value = sellOrderModel.EndDate;
            //parameters[31].Value = sellOrderModel.TheyDelegate;
            //parameters[32].Value = sellOrderModel.OurDelegate;
            //parameters[33].Value = sellOrderModel.Status;
            //parameters[34].Value = sellOrderModel.PayRemark;
            //parameters[35].Value = sellOrderModel.DeliverRemark;
            //parameters[36].Value = sellOrderModel.PackTransit;
            //parameters[37].Value = sellOrderModel.StatusNote;
            //parameters[38].Value = sellOrderModel.CustOrderNo;
            //parameters[39].Value = sellOrderModel.Remark;
            //parameters[40].Value = sellOrderModel.Attachment;
            //parameters[41].Value = sellOrderModel.BillStatus;
            //parameters[42].Value = sellOrderModel.ModifiedUserID;

            //foreach (SqlParameter para in parameters)
            //{
            //    if (para.Value == null)
            //    {
            //        para.Value = DBNull.Value;
            //    }
            //}
            #endregion
            SqlParameter[] param = null;
            ArrayList lcmd = new ArrayList();
            #region 参数
            lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellOrderModel.CompanyCD));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustID", sellOrderModel.CustID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustTel", sellOrderModel.CustTel));
            lcmd.Add(SqlHelper.GetParameterFromString("@OrderNo", sellOrderModel.OrderNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@Title", sellOrderModel.Title));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromType", sellOrderModel.FromType));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromBillID", sellOrderModel.FromBillID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Seller", sellOrderModel.Seller.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellDeptId", sellOrderModel.SellDeptId.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellType", sellOrderModel.SellType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@BusiType", sellOrderModel.BusiType));
            lcmd.Add(SqlHelper.GetParameterFromString("@OrderMethod", sellOrderModel.OrderMethod.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@PayType", sellOrderModel.PayType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@MoneyType", sellOrderModel.MoneyType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CarryType", sellOrderModel.CarryType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TakeType", sellOrderModel.TakeType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CurrencyType", sellOrderModel.CurrencyType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Rate", sellOrderModel.Rate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalPrice", sellOrderModel.TotalPrice.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Tax", sellOrderModel.Tax.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalFee", sellOrderModel.TotalFee.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Discount", sellOrderModel.Discount.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SaleFeeTotal", sellOrderModel.SaleFeeTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@DiscountTotal", sellOrderModel.DiscountTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@RealTotal", sellOrderModel.RealTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@isAddTax", sellOrderModel.isAddTax.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CountTotal", sellOrderModel.CountTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SendDate", sellOrderModel.SendDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@OrderDate", sellOrderModel.OrderDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@StartDate", sellOrderModel.StartDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@EndDate", sellOrderModel.EndDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TheyDelegate", sellOrderModel.TheyDelegate));
            lcmd.Add(SqlHelper.GetParameterFromString("@OurDelegate", sellOrderModel.OurDelegate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Status", sellOrderModel.Status));
            lcmd.Add(SqlHelper.GetParameterFromString("@PayRemark", sellOrderModel.PayRemark));
            lcmd.Add(SqlHelper.GetParameterFromString("@DeliverRemark", sellOrderModel.DeliverRemark));
            lcmd.Add(SqlHelper.GetParameterFromString("@PackTransit", sellOrderModel.PackTransit));
            lcmd.Add(SqlHelper.GetParameterFromString("@StatusNote", sellOrderModel.StatusNote));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustOrderNo", sellOrderModel.CustOrderNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@Remark", sellOrderModel.Remark));
            lcmd.Add(SqlHelper.GetParameterFromString("@Attachment", sellOrderModel.Attachment));
            lcmd.Add(SqlHelper.GetParameterFromString("@BillStatus", sellOrderModel.BillStatus));
            lcmd.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", sellOrderModel.ModifiedUserID));
            lcmd.Add(SqlHelper.GetParameterFromString("@CanViewUser", sellOrderModel.CanViewUser));
            lcmd.Add(SqlHelper.GetParameterFromString("@ProjectID", sellOrderModel.ProjectID.ToString()));
            #endregion

            #region 拓展属性

            if (htExtAttr != null && htExtAttr.Count != 0)
            {
                strSql.Append(" ;UPDATE officedba.SellOrder set ");
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql.Append(de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",");
                    lcmd.Add(SqlHelper.GetParameterFromString("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim()));
                }
                strSql.Append(" BillStatus=@BillStatus  where CompanyCD=@CompanyCD and OrderNo=@OrderNo ");
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


            sql = " select ID from officedba.SellOrder where CompanyCD=@CompanyCD and OrderNo=@OrderNo ";
            SqlParameter[] paras = { new SqlParameter("@CompanyCD", strCompanyCD), new SqlParameter("@OrderNo", orderNo) };
            OrderID = (int)SqlHelper.ExecuteScalar(sql, paras);
            return OrderID;
        }
        #endregion

        #region 为主表插入数据(销售订单)
        /// <summary>
        /// 为主表插入数据
        /// </summary>
        /// <param name="sellOrderModel"></param>
        /// <param name="tran"></param>
        private static void InsertSellOrder(Hashtable htExtAttr, SellOrderModel sellOrderModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SellOrder(");
            strSql.Append("CompanyCD,CustID,CustTel,OrderNo,Title,FromType,FromBillID,Seller,SellDeptId,SellType,BusiType,OrderMethod,PayType,MoneyType,CarryType,TakeType,CurrencyType,Rate,TotalPrice,Tax,TotalFee,Discount,SaleFeeTotal,DiscountTotal,RealTotal,isAddTax,CountTotal,SendDate,OrderDate,StartDate,EndDate,TheyDelegate,OurDelegate,Status,PayRemark,DeliverRemark,PackTransit,StatusNote,CustOrderNo,Remark,Attachment,BillStatus,Creator,CreateDate,ModifiedDate,ModifiedUserID,CanViewUser,ProjectID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@CustID,@CustTel,@OrderNo,@Title,@FromType,@FromBillID,@Seller,@SellDeptId,@SellType,@BusiType,@OrderMethod,@PayType,@MoneyType,@CarryType,@TakeType,@CurrencyType,@Rate,@TotalPrice,@Tax,@TotalFee,@Discount,@SaleFeeTotal,@DiscountTotal,@RealTotal,@isAddTax,@CountTotal,@SendDate,@OrderDate,@StartDate,@EndDate,@TheyDelegate,@OurDelegate,@Status,@PayRemark,@DeliverRemark,@PackTransit,@StatusNote,@CustOrderNo,@Remark,@Attachment,@BillStatus,@Creator,getdate(),getdate(),@ModifiedUserID,@CanViewUser,@ProjectID)");

            SqlParameter[] param = null;
            ArrayList lcmd = new ArrayList();
            #region 参数
            lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellOrderModel.CompanyCD));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustID", sellOrderModel.CustID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustTel", sellOrderModel.CustTel));
            lcmd.Add(SqlHelper.GetParameterFromString("@OrderNo", sellOrderModel.OrderNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@Title", sellOrderModel.Title));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromType", sellOrderModel.FromType));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromBillID", sellOrderModel.FromBillID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Seller", sellOrderModel.Seller.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellDeptId", sellOrderModel.SellDeptId.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellType", sellOrderModel.SellType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@BusiType", sellOrderModel.BusiType));
            lcmd.Add(SqlHelper.GetParameterFromString("@OrderMethod", sellOrderModel.OrderMethod.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@PayType", sellOrderModel.PayType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@MoneyType", sellOrderModel.MoneyType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CarryType", sellOrderModel.CarryType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TakeType", sellOrderModel.TakeType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CurrencyType", sellOrderModel.CurrencyType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Rate", sellOrderModel.Rate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalPrice", sellOrderModel.TotalPrice.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Tax", sellOrderModel.Tax.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalFee", sellOrderModel.TotalFee.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Discount", sellOrderModel.Discount.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SaleFeeTotal", sellOrderModel.SaleFeeTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@DiscountTotal", sellOrderModel.DiscountTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@RealTotal", sellOrderModel.RealTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@isAddTax", sellOrderModel.isAddTax.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CountTotal", sellOrderModel.CountTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SendDate", sellOrderModel.SendDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@OrderDate", sellOrderModel.OrderDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@StartDate", sellOrderModel.StartDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@EndDate", sellOrderModel.EndDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TheyDelegate", sellOrderModel.TheyDelegate));
            lcmd.Add(SqlHelper.GetParameterFromString("@OurDelegate", sellOrderModel.OurDelegate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Status", sellOrderModel.Status));
            lcmd.Add(SqlHelper.GetParameterFromString("@PayRemark", sellOrderModel.PayRemark));
            lcmd.Add(SqlHelper.GetParameterFromString("@DeliverRemark", sellOrderModel.DeliverRemark));
            lcmd.Add(SqlHelper.GetParameterFromString("@PackTransit", sellOrderModel.PackTransit));
            lcmd.Add(SqlHelper.GetParameterFromString("@StatusNote", sellOrderModel.StatusNote));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustOrderNo", sellOrderModel.CustOrderNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@Remark", sellOrderModel.Remark));
            lcmd.Add(SqlHelper.GetParameterFromString("@Attachment", sellOrderModel.Attachment));
            lcmd.Add(SqlHelper.GetParameterFromString("@BillStatus", sellOrderModel.BillStatus));
            lcmd.Add(SqlHelper.GetParameterFromString("@Creator", sellOrderModel.Creator.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", sellOrderModel.ModifiedUserID));
            lcmd.Add(SqlHelper.GetParameterFromString("@CanViewUser", sellOrderModel.CanViewUser));
            lcmd.Add(SqlHelper.GetParameterFromString("@ProjectID", sellOrderModel.ProjectID.ToString()));
            #endregion

            #region 拓展属性

            if (htExtAttr != null && htExtAttr.Count != 0)
            {
                strSql.Append(" ;UPDATE officedba.SellOrder set ");
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql.Append(de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",");
                    lcmd.Add(SqlHelper.GetParameterFromString("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim()));
                }
                strSql.Append(" BillStatus=@BillStatus  where CompanyCD=@CompanyCD and OrderNo=@OrderNo ");
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
        /// <param name="sellOrderDetailModellList"></param>
        /// <param name="tran"></param>
        private static void InsertSellOrderDetail(List<SellOrderDetailModel> sellOrderDetailModellList, TransactionManager tran)
        {
            foreach (SellOrderDetailModel sellOrderDetailModel in sellOrderDetailModellList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into officedba.SellOrderDetail(");
                strSql.Append("CompanyCD,OrderNo,SortNo,ProductID,ProductCount,UnitID,UnitPrice,TaxPrice,Discount,TaxRate,TotalFee,TotalPrice,TotalTax,SendTime,Package,Remark,UsedUnitID,UsedUnitCount,UsedPrice,ExRate)");
                strSql.Append(" values (");
                strSql.Append("@CompanyCD,@OrderNo,@SortNo,@ProductID,@ProductCount,@UnitID,@UnitPrice,@TaxPrice,@Discount,@TaxRate,@TotalFee,@TotalPrice,@TotalTax,@SendTime,@Package,@Remark,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate)");
                #region 参数
                SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@OrderNo", SqlDbType.VarChar,50),
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
					new SqlParameter("@UsedUnitID", SqlDbType.Int,4),
					new SqlParameter("@UsedUnitCount", SqlDbType.Decimal,9),
					new SqlParameter("@UsedPrice", SqlDbType.Decimal,9),
					new SqlParameter("@ExRate", SqlDbType.Decimal,9)};
                parameters[0].Value = sellOrderDetailModel.CompanyCD;
                parameters[1].Value = sellOrderDetailModel.OrderNo;
                parameters[2].Value = sellOrderDetailModel.SortNo;
                parameters[3].Value = sellOrderDetailModel.ProductID;
                parameters[4].Value = sellOrderDetailModel.ProductCount;
                parameters[5].Value = sellOrderDetailModel.UnitID;
                parameters[6].Value = sellOrderDetailModel.UnitPrice;
                parameters[7].Value = sellOrderDetailModel.TaxPrice;
                parameters[8].Value = sellOrderDetailModel.Discount;
                parameters[9].Value = sellOrderDetailModel.TaxRate;
                parameters[10].Value = sellOrderDetailModel.TotalFee;
                parameters[11].Value = sellOrderDetailModel.TotalPrice;
                parameters[12].Value = sellOrderDetailModel.TotalTax;
                parameters[13].Value = sellOrderDetailModel.SendTime;
                parameters[14].Value = sellOrderDetailModel.Package;
                parameters[15].Value = sellOrderDetailModel.Remark;
                parameters[16].Value = sellOrderDetailModel.UsedUnitID;
                parameters[17].Value = sellOrderDetailModel.UsedUnitCount;
                parameters[18].Value = sellOrderDetailModel.UsedPrice;
                parameters[19].Value = sellOrderDetailModel.ExRate;
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
        #region 为费用表插入数据
        /// <summary>
        /// 为费用表插入数据
        /// </summary>
        /// <param name="sellOrderFeeDetailModelList"></param>
        /// <param name="tran"></param>
        private static void InsertSellOrderFeeDetail(List<SellOrderFeeDetailModel> sellOrderFeeDetailModelList, TransactionManager tran)
        {
            foreach (SellOrderFeeDetailModel sellOrderFeeDetailModel in sellOrderFeeDetailModelList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into officedba.SellOrderFeeDetail(");
                strSql.Append("OrderNo,SortNo,FeeID,FeeTotal,Remark,CompanyCD)");
                strSql.Append(" values (");
                strSql.Append("@OrderNo,@SortNo,@FeeID,@FeeTotal,@Remark,@CompanyCD)");
                #region 参数
                SqlParameter[] parameters = {
					new SqlParameter("@OrderNo", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@FeeID", SqlDbType.Int,4),
					new SqlParameter("@FeeTotal", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8)};
                parameters[0].Value = sellOrderFeeDetailModel.OrderNo;
                parameters[1].Value = sellOrderFeeDetailModel.SortNo;
                parameters[2].Value = sellOrderFeeDetailModel.FeeID;
                parameters[3].Value = sellOrderFeeDetailModel.FeeTotal;
                parameters[4].Value = sellOrderFeeDetailModel.Remark;
                parameters[5].Value = sellOrderFeeDetailModel.CompanyCD;
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

        #region 删除销售订单
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
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellOrder WHERE OrderNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellOrderDetail WHERE OrderNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellOrderFeeDetail WHERE OrderNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);

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

        #region 获取报价信息相关操作
        #region 获取销售订单列表
        /// <summary>
        /// 获取销售订单列表 
        /// </summary>
        /// <param name="sellContractModel">sellContractModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(SellOrderModel sellOrderModel, decimal? TotalPrice1, string SendPro, int? FlowStatus,string EFIndex,string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            bool isMoreUnit = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit;//多计量单位

            strSql += "select * from ( SELECT s.ID, s.OrderNo, s.Title,s.ModifiedDate, CONVERT(varchar(100), s.OrderDate, 23) AS OrderDate,                                          ";
            strSql += "s.RealTotal, c.CustName, e.EmployeeName,                                                                                      ";
            strSql += "CASE s.FromType WHEN 0 THEN '无来源' WHEN 1 THEN '销售报价单' WHEN 2 THEN '销售合同'  WHEN 3 THEN '销售机会'  END AS FromTypeText,                      ";
            strSql += "CASE s.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更'                                                     ";
            strSql += "WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText,                                                           ";
            strSql += "CASE WHEN (SELECT TOP 1 FlowStatus                                                                                             ";
            strSql += "FROM officedba.FlowInstance                                                                                                    ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 3                                                                  ";
            strSql += "ORDER BY ModifiedDate DESC) IS NULL THEN '' WHEN                                                                         ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                                       ";
            strSql += "FROM officedba.FlowInstance                                                                                                    ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 3                                                                  ";
            strSql += "ORDER BY ModifiedDate DESC) = 1 THEN '待审批' WHEN                                                                             ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                                       ";
            strSql += "FROM officedba.FlowInstance                                                                                                    ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 3                                                                  ";
            strSql += "ORDER BY ModifiedDate DESC) = 2 THEN '审批中' WHEN                                                                             ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                                       ";
            strSql += "FROM officedba.FlowInstance                                                                                                    ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 3                                                                  ";
            strSql += "ORDER BY ModifiedDate DESC) = 3 THEN '审批通过' WHEN                                                                           ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                                       ";
            strSql += "FROM officedba.FlowInstance                                                                                                    ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 3                                                                  ";
            strSql += "ORDER BY ModifiedDate DESC) = 4 THEN '审批不通过' WHEN                                                                         ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                                       ";
            strSql += "FROM officedba.FlowInstance                                                                                                    ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 3                                                                  ";
            strSql += "ORDER BY ModifiedDate DESC) = 5 THEN '撤销审批' END AS FlowInstanceText, isnull(CASE                                           ";
            strSql += "(((SELECT count(1)                                                                                                             ";
            strSql += "FROM officedba.SellSend AS soo                                                                                                 ";
            strSql += "WHERE soo.FromType = '1' AND soo.FromBillID = s.ID)+                                                                           ";
            strSql += "(SELECT count(1)                                                                                                               ";
            strSql += "FROM officedba.SellSendDetail                                                                                                  ";
            strSql += "WHERE FromType = '1' AND FromBillID = s.ID)+                                                                                   ";
            strSql += "(SELECT count(1)                                                                                                               ";
            strSql += "FROM officedba.PurchaseApplyDetailSource                                                                                       ";
            strSql += "WHERE FromType = '1' AND FromBillID = s.ID)+                                                                                   ";
            strSql += "(SELECT count(1)                                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD)                                                      ";
            strSql += "+(SELECT count(1)                                                                                                              ";
            strSql += "FROM officedba.MasterProductScheduleDetail                                                                                     ";
            strSql += "WHERE FromBillID = s.ID and CompanyCD=s.CompanyCD)                                                                             ";
            strSql += ")) WHEN 0 THEN '无引用' END, '被引用') AS RefText,                                                                             ";
            //发货状态Start
            strSql += "case when (SELECT    isnull( sum(SendCount),0) as SendCount                                                                    ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD) =0 then '未发货'                                                          ";
            strSql += "when (SELECT isnull( sum(SendCount),0) as SendCount                                                                            ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD) >0  and                                                                   ";
            strSql += "(SELECT isnull( sum(SendCount),0) as SendCount                                                                                 ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)<                                                                          ";
            if (isMoreUnit)
            {
                strSql += "(SELECT isnull( sum(UsedUnitCount),0) as SendCount   ";
            }
            else
            {
                strSql += "(SELECT isnull( sum(ProductCount),0) as SendCount   ";
            }
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)                                                                           ";
            strSql += "then '部分发货'                                                                                                                ";
            strSql += "when (SELECT isnull( sum(SendCount),0) as SendCount                                                                            ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD) <>0  and                                                                  ";
            strSql += "(SELECT isnull( sum(SendCount),0) as SendCount                                                                                 ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)>=                                                                          ";
            if (isMoreUnit)
            {
                strSql += "(SELECT isnull( sum(UsedUnitCount),0) as SendCount    ";
            }
            else
            {
                strSql += "(SELECT isnull( sum(ProductCount),0) as SendCount    ";
            }
            
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)                                                                           ";
            strSql += "then '已发货'                                                                                                                  ";
            strSql += "end as isSendText,                                                                                                     ";
            //发货状态End
            strSql += "isnull( (SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0) as YAccounts,                                  ";
            strSql += " case when (isnull(                                                                                                            ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))>0                                             ";
            strSql += "and (isnull(                                                                                                                   ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0)) <                                             ";
            strSql += "(isnull(                                                                                                                       ";
            strSql += "(SELECT isnull(max(TotalPrice),0)                                                                                              ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))                                               ";
            strSql += " then '部分回款'                                                                                                               ";
            strSql += "when                                                                                                                           ";
            strSql += "(isnull(                                                                                                                       ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0)) =                                             ";
            strSql += "(isnull(                                                                                                                       ";
            strSql += "(SELECT isnull(max(TotalPrice),0)                                                                                              ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0)) and                                           ";
            strSql += "(isnull(                                                                                                                       ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))<>0                                            ";
            strSql += "  then '已回款'                                                                                                                ";
            strSql += "when (isnull(                                                                                                                  ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))=0 then '未回款' end as IsAccText ,            ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                                       ";
            strSql += "FROM officedba.FlowInstance                                                                                                    ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 3                                                                  ";
            strSql += "ORDER BY ModifiedDate DESC) AS FlowStatus                                                                                      ";
            strSql += ",case s.isOpenbill when '0' then '未建单' when '1' then '已建单' end as isOpenbillText                                         ";
            strSql += ", CASE s.FromType WHEN 0 THEN '' WHEN 1 THEN (select isnull(OfferNo,'') as                                                     ";
            strSql += " OfferNo from  officedba.SellOffer where ID=s.FromBillID)                                                                      ";
            strSql += "WHEN 2 THEN (select isnull(ContractNo,'') as  ContractNo  from  officedba.SellContract                                         ";
            strSql += "where ID=s.FromBillID)                                                                                      ";
            strSql += "WHEN 3 THEN (select isnull(ChanceNo,'') as  ChanceNo  from  officedba.SellChance                                         ";
            strSql += "where ID=s.FromBillID) END AS FromBillNo    ";
            strSql += "FROM officedba.SellOrder AS s LEFT JOIN     ";
            strSql += "officedba.CustInfo AS c ON s.CustID = c.ID LEFT JOIN      ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID where 1=1   and s.CompanyCD=@CompanyCD  ";
           //过滤单据：显示当前用户拥有权限查看的单据
            int empid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            strSql += " and ( charindex('," + empid + ",' , ','+s.CanViewUser+',')>0 or s.Creator=" + empid + " OR s.CanViewUser='' OR s.CanViewUser is null) ";
            
            string strCompanyCD = string.Empty;//单位编号
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            ArrayList arr = new ArrayList();
            //扩展属性
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                strSql += " and s.ExtField"+EFIndex+" like @EFDesc ";
                arr.Add(new SqlParameter("@EFDesc", "%" + EFDesc + "%"));
            }

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (sellOrderModel.ProjectID != null)
            {
                strSql += " and s.ProjectID=@ProjectID ";
                arr.Add(new SqlParameter("@ProjectID", sellOrderModel.ProjectID));
            }
            if (sellOrderModel.BillStatus != null)
            {
                strSql += " and s.BillStatus= @BillStatus";
                arr.Add(new SqlParameter("@BillStatus", sellOrderModel.BillStatus));
            }

            if (sellOrderModel.FromType != null)
            {
                strSql += " and s.FromType=@FromType";
                arr.Add(new SqlParameter("@FromType", sellOrderModel.FromType));
            }
            if (sellOrderModel.FromBillID != null)
            {
                strSql += " and s.FromBillID=@FromBillID";
                arr.Add(new SqlParameter("@FromBillID", sellOrderModel.FromBillID));
            }

            if (sellOrderModel.OrderNo != null)
            {
                strSql += " and s.OrderNo like @OrderNo";
                arr.Add(new SqlParameter("@OrderNo", "%" + sellOrderModel.OrderNo + "%"));
            }

            if (sellOrderModel.TotalPrice != null)
            {
                strSql += " and s.TotalPrice >= @TotalPrice";
                arr.Add(new SqlParameter("@TotalPrice", sellOrderModel.TotalPrice));
            }
            if (TotalPrice1 != null)
            {
                strSql += " and s.TotalPrice <= @TotalPrice1";
                arr.Add(new SqlParameter("@TotalPrice1", TotalPrice1));
            }
            if (sellOrderModel.CustID != null)
            {
                strSql += " and s.CustID=@CustID";
                arr.Add(new SqlParameter("@CustID", sellOrderModel.CustID));
            }
            if (sellOrderModel.Title != null)
            {
                strSql += " and s.Title like @Title";
                arr.Add(new SqlParameter("@Title", "%" + sellOrderModel.Title + "%"));
            }
            if (sellOrderModel.Seller != null)
            {
                strSql += " and s.Seller=@Seller";
                arr.Add(new SqlParameter("@Seller", sellOrderModel.Seller));
            }
            if (sellOrderModel.isOpenbill != null)
            {
                strSql += " and s.isOpenbill=@isOpenbill";
                arr.Add(new SqlParameter("@isOpenbill", sellOrderModel.isOpenbill));
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
            if (SendPro != null)
            {

                strSql += " and f.isSendText=@isSendText";
                arr.Add(new SqlParameter("@isSendText", SendPro));

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

            string strSql = "SELECT s.ProductID, s.ProductCount, s.UnitID, s.UnitPrice, s.Discount, s.TaxRate,p.Specification, ";
            strSql += "s.TotalFee, s.TotalPrice, s.TotalTax, s.Package, s.Remark, p.ProductName, isnull(pc.TypeName,'') as ColorName, ";
            strSql += "c.CodeName, ISNULL(p.SellTax, 0) AS SellTax, ISNULL(p.TaxRate, 0) AS TaxRate1, p.ProdNo, isnull(p.StandardCost,0) as StandardCost,";
            strSql += "s.TaxPrice, s.SendTime, ISNULL(s.SendCount, 0) AS SendCount, ISNULL(s.PlanProductCount, 0) ";
            strSql += "AS PlanProductCount, ISNULL(s.UseStockCount, 0) AS UseStockCount ";
            strSql += ",isnull(p.StandardSell,0) as StandardSell,s.UsedUnitID,isnull(s.UsedUnitCount,0) as UsedUnitCount,isnull(s.UsedPrice,0) as UsedPrice,isnull(s.ExRate,1) as ExRate ";
            strSql += "FROM officedba.SellOrderDetail AS s LEFT OUTER JOIN ";
            strSql += "officedba.ProductInfo AS p ON s.ProductID = p.ID LEFT OUTER JOIN ";
            strSql += " officedba.CodePublicType as pc on pc.ID=p.ColorID left join ";
            strSql += "officedba.CodeUnitType AS c ON s.UnitID = c.ID ";
            strSql += " WHERE (s.OrderNo = @OrderNo) AND (s.CompanyCD = @CompanyCD) ";
            strSql += "ORDER BY s.SortNo ";

            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@OrderNo", orderNo);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }
        #endregion

        #region 获取销售订单主表信息
        /// <summary>
        /// 获取销售订单主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = "SELECT e2.EmployeeName AS CreatorName, e3.EmployeeName AS ConfirmorName, ";
            strSql += "e1.EmployeeName AS OurDelegateName, ct.CurrencyName, ";
            strSql += "e.EmployeeName AS SellerName, e4.EmployeeName AS CloserName, d.DeptName, ";
            strSql += "s.ExtField1,s.ExtField2,s.ExtField3,s.ExtField4,s.ExtField5,";
            strSql += "s.ExtField6,s.ExtField7,s.ExtField8,s.ExtField9,s.ExtField10, ";
            strSql += "s.CustID, s.CustTel, s.OrderNo, s.Title, s.FromType, s.FromBillID, ";
            strSql += "s.Seller, s.SellDeptId, s.SellType, s.BusiType, s.OrderMethod, s.PayType, ";
            strSql += "s.MoneyType, s.CarryType, s.TakeType, s.CurrencyType, s.Rate, ";
            strSql += "s.TotalPrice, s.Tax, s.TotalFee, s.Discount, s.SaleFeeTotal, s.DiscountTotal, ";
            strSql += " s.RealTotal, s.isAddTax, s.CountTotal, s.isOpenbill, ";
            strSql += "CONVERT(varchar(100), s.SendDate, 23) AS SendDate, CONVERT(varchar(100), s.OrderDate, 23) ";
            strSql += " AS OrderDate, CONVERT(varchar(100), s.StartDate, 23) ";
            strSql += "AS StartDate, CONVERT(varchar(100), s.EndDate, 23) AS EndDate, s.TheyDelegate, ";
            strSql += "s.OurDelegate, s.Status, s.PayRemark, s.DeliverRemark, ";
            strSql += "s.PackTransit, s.StatusNote, s.CustOrderNo, s.Remark, s.Attachment, s.BillStatus,s.Status as State, ";
            strSql += "CONVERT(varchar(100), s.CreateDate, 23) AS CreateDate, ";
            strSql += "CONVERT(varchar(100), s.ConfirmDate, 23) AS ConfirmDate, CONVERT(varchar(100), s.CloseDate, 23) ";
            strSql += "AS CloseDate,CASE s.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更' WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText , CONVERT(varchar(100), ";
            strSql += "s.ModifiedDate, 23) AS ModifiedDate, s.ModifiedUserID,  ";
            strSql += " s.ProjectID,p.ProjectName,   ";
            strSql += "s.CanViewUser,[dbo].[getEmployeeNameString](s.CanViewUser) as CanViewUserName,";//可查看此订单人员
            strSql += "case when (SELECT    isnull( sum(SendCount),0) as SendCount     ";
            strSql += "FROM officedba.SellOrderDetail    ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD) =0 then '未发货'  ";
            strSql += "when (SELECT isnull( sum(SendCount),0) as SendCount    ";
            strSql += "FROM officedba.SellOrderDetail   ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD) >0  and  ";
            strSql += "(SELECT isnull( sum(SendCount),0) as SendCount  ";
            strSql += "FROM officedba.SellOrderDetail      ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)<   ";
            strSql += "(SELECT isnull( sum(ProductCount),0) as SendCount   ";
            strSql += "FROM officedba.SellOrderDetail         ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)   ";
            strSql += "then '部分发货'        ";
            strSql += "when (SELECT isnull( sum(SendCount),0) as SendCount   ";
            strSql += "FROM officedba.SellOrderDetail     ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD) <>0  and  ";
            strSql += "(SELECT isnull( sum(SendCount),0) as SendCount  ";
            strSql += "FROM officedba.SellOrderDetail    ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)=  ";
            strSql += "(SELECT isnull( sum(ProductCount),0) as SendCount   ";
            strSql += "FROM officedba.SellOrderDetail   ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)  ";
            strSql += "then '已发货'    ";
            strSql += "end as isSendText, ISNULL ";
            strSql += "((SELECT ISNULL(SUM(YAccounts), 0) AS Expr1 ";
            strSql += "FROM officedba.BlendingDetails ";
            strSql += "WHERE (BillingType = '1') AND (BillCD = s.OrderNo) AND (CompanyCD = s.CompanyCD)), 0) ";
            strSql += " AS YAccounts, ";
            strSql += "CASE s.isOpenbill WHEN '0' THEN '未建单' WHEN '1' THEN '已建单' END AS isOpenbillText, ";
            strSql += "CASE s.FromType WHEN 0 THEN '' WHEN 1 THEN ";
            strSql += "(SELECT isnull(OfferNo, '') AS OfferNo ";
            strSql += "FROM officedba.SellOffer ";
            strSql += "WHERE ID = s.FromBillID) WHEN 2 THEN ";
            strSql += "(SELECT isnull(ContractNo, '') AS ContractNo ";
            strSql += "FROM officedba.SellContract ";
            strSql += "WHERE ID = s.FromBillID) when 3 then ";
            strSql += "(SELECT isnull(ChanceNo, '') AS ChanceNo ";
            strSql += "FROM officedba.SellChance ";
            strSql += "WHERE ID = s.FromBillID)  ";
            strSql += " END AS FromBillNo, c.CustName, ";
            strSql += " case when (isnull(   ";
            strSql += "(SELECT isnull(sum(YAccounts),0)    ";
            strSql += "FROM officedba.BlendingDetails    ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))>0      ";
            strSql += "and (isnull(     ";
            strSql += "(SELECT isnull(sum(YAccounts),0)   ";
            strSql += "FROM officedba.BlendingDetails    ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0)) <   ";
            strSql += "(isnull(     ";
            strSql += "(SELECT isnull(max(TotalPrice),0)    ";
            strSql += "FROM officedba.BlendingDetails    ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))  ";
            strSql += " then '部分回款'        ";
            strSql += "when       ";
            strSql += "(isnull(   ";
            strSql += "(SELECT isnull(sum(YAccounts),0)  ";
            strSql += "FROM officedba.BlendingDetails  ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0)) =  ";
            strSql += "(isnull(   ";
            strSql += "(SELECT isnull(max(TotalPrice),0)  ";
            strSql += "FROM officedba.BlendingDetails   ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0)) and  ";
            strSql += "(isnull(   ";
            strSql += "(SELECT isnull(sum(YAccounts),0)   ";
            strSql += "FROM officedba.BlendingDetails   ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))<>0  ";
            strSql += "  then '已回款'    ";
            strSql += "when (isnull(   ";
            strSql += "(SELECT isnull(sum(YAccounts),0)   ";
            strSql += "FROM officedba.BlendingDetails    ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))=0 then '未回款' end as IsAccText  ";
            strSql += "FROM officedba.SellOrder AS s LEFT OUTER JOIN ";
            strSql += "officedba.CustInfo AS c ON s.CustID = c.ID LEFT OUTER JOIN ";
            strSql += "officedba.DeptInfo AS d ON s.SellDeptId = d.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID LEFT OUTER JOIN ";
            strSql += "officedba.CurrencyTypeSetting AS ct ON s.CurrencyType = ct.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e1 ON s.OurDelegate = e1.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e2 ON s.Creator = e2.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e3 ON s.Confirmor = e3.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e4 ON s.Closer = e4.ID ";
            strSql += " left join officedba.ProjectInfo p on p.ID=s.ProjectID ";

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

        #region 获取销售费用明细
        /// <summary>
        /// 获取销售费用明细
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable GetFee(string orderNo)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = "SELECT s.FeeID, s.FeeTotal, s.Remark, c.CodeName    ";
            strSql += "FROM officedba.SellOrderFeeDetail AS s left JOIN   ";
            strSql += "officedba.CodeFeeType AS c ON s.FeeID = c.ID        ";
            strSql += " WHERE (s.OrderNo = @OrderNo) AND (s.CompanyCD = @CompanyCD) ";
            strSql += "ORDER BY s.SortNo ";

            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@OrderNo", orderNo);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }
        #endregion

        #endregion

        #region 财务模块相关
        /// <summary>
        /// 更新建单状态  Added By jiangym 2009-04-22
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns></returns>
        public static bool UpdateisOpenBill(string ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Update officedba.SellOrder set isOpenbill='1'");
            sql.AppendLine("where ID In( " + ID + ") ");
            SqlHelper.ExecuteTransSql(sql.ToString());
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }

        /// <summary>
        /// 根据检索条件检索出满足条件的信息 Added By jiangym 2009-04-16
        /// </summary>
        /// <param name="OrderNo">订单号</param>
        /// <param name="Title">主题</param>
        /// <param name="CustName">客户</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns>DataTable</returns>
        public static DataTable SearchOrderByCondition(string CompanyCD, string OrderNo, string Title,
            string CustName, string StartDate, string EndDate)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from officedba.GetSellOrderInfo ");
            sql.AppendLine("where CompanyCD=@CompanyCD");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //订单编号
            if (!string.IsNullOrEmpty(OrderNo))
            {
                sql.AppendLine(" AND OrderNo LIKE @OrderNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", "%" + OrderNo + "%"));
            }
            //主题
            if (!string.IsNullOrEmpty(Title))
            {
                sql.AppendLine(" AND Title LIKE @Title ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + Title + "%"));
            }
            //客户名称
            if (!string.IsNullOrEmpty(CustName))
            {
                sql.AppendLine(" AND CustID = @CustID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustName));
            }
            //开始和结束时间
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" AND CreateDate BetWeen  @StartDate and @EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);

        }






        /// <summary>
        /// 根据检索条件检索出满足条件的信息Added By Moshenlin 2010-03-11
        /// </summary>
        /// <param name="OrderNo">订单号</param>
        /// <param name="Title">主题</param>
        /// <param name="CustName">客户</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns>DataTable</returns>
        public static DataTable SearchOrderByCondition(string CompanyCD, string OrderNo, string Title,
            string CustName, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from officedba.GetSellOrderInfo ");
            sql.AppendLine("where CompanyCD=@CompanyCD");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //订单编号
            if (!string.IsNullOrEmpty(OrderNo))
            {
                sql.AppendLine(" AND OrderNo LIKE @OrderNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", "%" + OrderNo + "%"));
            }
            //主题
            if (!string.IsNullOrEmpty(Title))
            {
                sql.AppendLine(" AND Title LIKE @Title ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + Title + "%"));
            }
            //客户名称
            if (!string.IsNullOrEmpty(CustName))
            {
                sql.AppendLine(" AND CustID = @CustID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustName));
            }
            //开始和结束时间
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" AND CreateDate BetWeen  @StartDate and @EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);

        }




        /// <summary>
        /// 根据检索条件检索出满足条件的信息 Added By moshenlin 2009-06-29
        /// </summary>
        /// <param name="ids">主表ID集</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable SearchOrderByCondition(string ids, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from officedba.GetSellOrderInfo ");
            sql.AppendLine("where CompanyCD=@CompanyCD and id in ( " + ids + " ) order by CreateDate asc ");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));


            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);

        }

        #endregion

        #region 报表相关
        #region 旧报表

        #region 部门业绩周对比

        /// <summary>
        /// 部门业绩周对比
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="strDate">分析的月份的第一天（格式：2009-05-01）</param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndWeek(int? DeptID, string strDate, int? CurrencyType,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


            strSql += "select DeptName,OneRealTotal,TwoRealTotal,ThreeRealTotal,FiveRealTotal,OneCountTotal,TwoCountTotal                      ";
            strSql += ",ThreeCountTotal,ForuCountTotal,FiveCountTotal,ForuRealTotal ,sixRealTotal,sixCountTotal,                                                          ";
            strSql += "(OneRealTotal+TwoRealTotal+ThreeRealTotal+FiveRealTotal+ForuRealTotal+sixRealTotal) as PriceTotal,                                   ";
            strSql += "(OneCountTotal+TwoCountTotal+ThreeCountTotal+ForuCountTotal+FiveCountTotal+sixCountTotal) as CountTotal                               ";
            strSql += "from (select SellDeptId,isnull(DeptName,'未知') as DeptName,isnull(sum(OneRealTotal),0) as OneRealTotal                                            ";
            strSql += ",isnull(sum(TwoRealTotal),0) as TwoRealTotal,isnull(sum(ThreeRealTotal),0) as ThreeRealTotal                            ";
            strSql += ",isnull(sum(FiveRealTotal),0) as FiveRealTotal,isnull(sum(OneCountTotal),0) as OneCountTotal                            ";
            strSql += ",isnull(sum(TwoCountTotal),0) as TwoCountTotal,isnull(sum(ThreeCountTotal),0) as ThreeCountTotal                        ";
            strSql += ",isnull(sum(ForuCountTotal),0) as ForuCountTotal,isnull(sum(FiveCountTotal),0) as FiveCountTotal                        ";
            strSql += ",isnull(sum(ForuRealTotal),0) as ForuRealTotal,isnull(sum(sixCountTotal),0) as sixCountTotal                            ";
            strSql += " ,isnull(sum(sixRealTotal),0) as sixRealTotal  from (SELECT *FROM (                                                                                                    ";
            strSql += "select SellDeptId,sum(RealTotal) as RealTotal,weeknum,weeknum1,sum(CountTotal) as CountTotal,d.DeptName from (          ";
            if (CurrencyType != null)
            {
                strSql += "select SellDeptId,RealTotal,CountTotal,                                                                                 ";
            }
            if (CurrencyType == null)
            {
                strSql += "select SellDeptId,(RealTotal*Rate) as RealTotal,CountTotal,                                                                                 ";
            }

            strSql += "case when (datepart(week,OrderDate) - datepart(week,@SearchDate))=0 then 'OneRealTotal'                                 ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=1 then 'TwoRealTotal'                                      ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=2 then 'ThreeRealTotal'                                    ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=3 then 'ForuRealTotal'                                     ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=4 then 'FiveRealTotal'                                     ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=5 then 'sixRealTotal'                                    ";
            strSql += "end as weeknum ,                                                                                                        ";
            strSql += "case when (datepart(week,OrderDate) - datepart(week,@SearchDate))=0 then 'OneCountTotal'                                ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=1 then 'TwoCountTotal'                                     ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=2 then 'ThreeCountTotal'                                   ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=3 then 'ForuCountTotal'                                    ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=4 then 'FiveCountTotal'                                    ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=5 then 'sixCountTotal'                                    ";
            strSql += "end as weeknum1 from officedba.SellOrder                                                                                ";
            strSql += "where OrderDate>=@SearchDate and OrderDate <dateadd(month,1,@SearchDate) and CompanyCD=@CompanyCD  and BillStatus<>'1' and Status<>3  ";
            ArrayList arr = new ArrayList();
            if (DeptID != null)
            {
                strSql += " and SellDeptId=@SellDeptId  ";
                arr.Add(new SqlParameter("@SellDeptId", DeptID));
            }
            if (CurrencyType != null)
            {
                strSql += " and  CurrencyType=@CurrencyType  ";
                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            strSql += " ) as tt left join                                                                            ";
            strSql += "officedba.DeptInfo AS d ON tt.SellDeptId = d.ID                                                                         ";
            strSql += "group by SellDeptId,weeknum ,weeknum1,d.DeptName                                                                        ";
            strSql += ") s  PIVOT (SUM (RealTotal) FOR weeknum IN                                                                              ";
            strSql += "([OneRealTotal],[TwoRealTotal],[ThreeRealTotal],[ForuRealTotal],[FiveRealTotal],[sixRealTotal])) as pvt1  PIVOT (                      ";
            strSql += "SUM (CountTotal) FOR weeknum1 IN                                                                                        ";
            strSql += "([OneCountTotal],[TwoCountTotal],[ThreeCountTotal],[ForuCountTotal],[FiveCountTotal],[sixCountTotal])                                   ";
            strSql += ") AS pvt1) as tt  group by SellDeptId,DeptName ) as ttt                                                                 ";


            arr.Add(new SqlParameter("@SearchDate", strDate));
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 部门业绩周对比
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="strDate">分析的月份的第一天（格式：2009-05-01）</param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndWeek(int? DeptID, string strDate, int? CurrencyType)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


            strSql += "select DeptName,OneRealTotal,TwoRealTotal,ThreeRealTotal,FiveRealTotal,OneCountTotal,TwoCountTotal                      ";
            strSql += ",ThreeCountTotal,ForuCountTotal,FiveCountTotal,ForuRealTotal ,sixRealTotal,sixCountTotal,                                                          ";
            strSql += "(OneRealTotal+TwoRealTotal+ThreeRealTotal+FiveRealTotal+ForuRealTotal+sixRealTotal) as PriceTotal,                                   ";
            strSql += "(OneCountTotal+TwoCountTotal+ThreeCountTotal+ForuCountTotal+FiveCountTotal+sixCountTotal) as CountTotal                               ";
            strSql += "from (select SellDeptId,isnull(DeptName,'未知') as DeptName,isnull(sum(OneRealTotal),0) as OneRealTotal                                            ";
            strSql += ",isnull(sum(TwoRealTotal),0) as TwoRealTotal,isnull(sum(ThreeRealTotal),0) as ThreeRealTotal                            ";
            strSql += ",isnull(sum(FiveRealTotal),0) as FiveRealTotal,isnull(sum(OneCountTotal),0) as OneCountTotal                            ";
            strSql += ",isnull(sum(TwoCountTotal),0) as TwoCountTotal,isnull(sum(ThreeCountTotal),0) as ThreeCountTotal                        ";
            strSql += ",isnull(sum(ForuCountTotal),0) as ForuCountTotal,isnull(sum(FiveCountTotal),0) as FiveCountTotal                        ";
            strSql += ",isnull(sum(ForuRealTotal),0) as ForuRealTotal,isnull(sum(sixCountTotal),0) as sixCountTotal                            ";
            strSql += " ,isnull(sum(sixRealTotal),0) as sixRealTotal  from (SELECT *FROM (                                                                                                    ";
            strSql += "select SellDeptId,sum(RealTotal) as RealTotal,weeknum,weeknum1,sum(CountTotal) as CountTotal,d.DeptName from (          ";
            if (CurrencyType != null)
            {
                strSql += "select SellDeptId,RealTotal,CountTotal,                                                                                 ";
            }
            if (CurrencyType == null)
            {
                strSql += "select SellDeptId,(RealTotal*Rate) as RealTotal,CountTotal,                                                                                 ";
            }

            strSql += "case when (datepart(week,OrderDate) - datepart(week,@SearchDate))=0 then 'OneRealTotal'                                 ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=1 then 'TwoRealTotal'                                      ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=2 then 'ThreeRealTotal'                                    ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=3 then 'ForuRealTotal'                                     ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=4 then 'FiveRealTotal'                                     ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=5 then 'sixRealTotal'                                    ";
            strSql += "end as weeknum ,                                                                                                        ";
            strSql += "case when (datepart(week,OrderDate) - datepart(week,@SearchDate))=0 then 'OneCountTotal'                                ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=1 then 'TwoCountTotal'                                     ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=2 then 'ThreeCountTotal'                                   ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=3 then 'ForuCountTotal'                                    ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=4 then 'FiveCountTotal'                                    ";
            strSql += "when (datepart(week,OrderDate) - datepart(week,@SearchDate))=5 then 'sixCountTotal'                                    ";
            strSql += "end as weeknum1 from officedba.SellOrder                                                                                ";
            strSql += "where OrderDate>=@SearchDate and OrderDate <dateadd(month,1,@SearchDate) and CompanyCD=@CompanyCD  and BillStatus<>'1' and Status<>3  ";
            ArrayList arr = new ArrayList();
            if (DeptID != null)
            {
                strSql += " and SellDeptId=@SellDeptId  ";
                arr.Add(new SqlParameter("@SellDeptId", DeptID));
            }
            if (CurrencyType != null)
            {
                strSql += " and  CurrencyType=@CurrencyType  ";
                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            strSql += " ) as tt left join                                                                            ";
            strSql += "officedba.DeptInfo AS d ON tt.SellDeptId = d.ID                                                                         ";
            strSql += "group by SellDeptId,weeknum ,weeknum1,d.DeptName                                                                        ";
            strSql += ") s  PIVOT (SUM (RealTotal) FOR weeknum IN                                                                              ";
            strSql += "([OneRealTotal],[TwoRealTotal],[ThreeRealTotal],[ForuRealTotal],[FiveRealTotal],[sixRealTotal])) as pvt1  PIVOT (                      ";
            strSql += "SUM (CountTotal) FOR weeknum1 IN                                                                                        ";
            strSql += "([OneCountTotal],[TwoCountTotal],[ThreeCountTotal],[ForuCountTotal],[FiveCountTotal],[sixCountTotal])                                   ";
            strSql += ") AS pvt1) as tt  group by SellDeptId,DeptName ) as ttt                                                                 ";


            arr.Add(new SqlParameter("@SearchDate", strDate));
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            return SqlHelper.ExecuteSql(strSql, arr);
        }

        #endregion

        #region 部门业绩月度对比
        /// <summary>
        /// 部门业绩月度对比
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="Year">分析年份</param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndMonth(int? DeptID, string Year, int? CurrencyType,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


            strSql += "select DeptName,MR1,MR2,MR3,MR4,MR5,MR6,MR7,MR8,MR9,MR10,MR11,MR12,                                                 ";
            strSql += "MC1,MC2,MC3,MC4,MC5,MC6, MC7,MC8,MC9,MC10,MC11,MC12,                                                                ";
            strSql += "(MR1+MR2+MR3+MR5+MR4+MR6+MR7+MR8+MR9+MR10+MR11+MR12) as PriceTotal,                                                 ";
            strSql += "(MC1+MC2+MC3+MC4+MC5+MC6+MC7+MC8+MC9+MC10+MC11+MC12) as CountTotal                                                  ";
            strSql += "from (select SellDeptId,isnull(DeptName ,'未知') as DeptName,isnull(sum(MR1),0) as MR1                                                         ";
            strSql += ",isnull(sum(MR2),0) as MR2,isnull(sum(MR3),0) as MR3,isnull(sum(MR4),0) as MR4                                      ";
            strSql += ",isnull(sum(MR5),0) as MR5,isnull(sum(MR6),0) as MR6,isnull(sum(MR7),0) as MR7                                      ";
            strSql += ",isnull(sum(MR8),0) as MR8,isnull(sum(MR9),0) as MR9,isnull(sum(MR10),0) as MR10                                    ";
            strSql += ",isnull(sum(MR11),0) as MR11,isnull(sum(MR12),0) as MR12 ,isnull(sum(MC1),0) as MC1                                 ";
            strSql += ",isnull(sum(MC2),0) as MC2,isnull(sum(MC3),0) as MC3,isnull(sum(MC4),0) as MC4                                      ";
            strSql += ",isnull(sum(MC5),0) as MC5,isnull(sum(MC6),0) as MC6,isnull(sum(MC7),0) as MC7                                      ";
            strSql += ",isnull(sum(MC8),0) as MC8,isnull(sum(MC9),0) as MC9,isnull(sum(MC10),0) as MC10                                    ";
            strSql += ",isnull(sum(MC11),0) as MC11,isnull(sum(MC12),0) as MC12 from (SELECT *FROM (                                       ";
            strSql += "select SellDeptId,sum(RealTotal) as RealTotal,weeknum,weeknum1,sum(CountTotal) as CountTotal,d.DeptName             ";
            if (CurrencyType != null)
            {
                strSql += "from ( select SellDeptId,RealTotal,CountTotal,case when month(OrderDate) =1 then 'MR1' ";

            }
            if (CurrencyType == null)
            {
                strSql += "from ( select SellDeptId,(RealTotal*Rate) as RealTotal,CountTotal,case when month(OrderDate) =1 then 'MR1' ";

            }

            strSql += "when month(OrderDate) =2 then 'MR2' when month(OrderDate) =3 then 'MR3' when month(OrderDate) =4 then 'MR4'         ";
            strSql += "when month(OrderDate) =5 then 'MR5' when month(OrderDate) =6 then 'MR6' when month(OrderDate) =7 then 'MR7'         ";
            strSql += "when month(OrderDate) =8 then 'MR8' when month(OrderDate) =9 then 'MR9' when month(OrderDate) =10 then 'MR10'       ";
            strSql += "when month(OrderDate) =11 then 'MR11' when month(OrderDate) =12 then 'MR12' end as weeknum ,                        ";
            strSql += "case when month(OrderDate) =1 then 'MC1' when month(OrderDate) =2 then 'MC2' when month(OrderDate) =3 then 'MC3'    ";
            strSql += "when month(OrderDate) =4 then 'MC4' when month(OrderDate) =5 then 'MC5' when month(OrderDate) =6 then 'MC6'         ";
            strSql += "when month(OrderDate) =7 then 'MC7' when month(OrderDate) =8 then 'MC8' when month(OrderDate) =9 then 'MC9'         ";
            strSql += "when month(OrderDate) =10 then 'MC10' when month(OrderDate) =11 then 'MC11' when month(OrderDate) =12 then 'MC12'   ";
            strSql += "end as weeknum1 from officedba.SellOrder                                                                            ";
            strSql += "where YEAR(OrderDate)=@OrderDate and CompanyCD=@CompanyCD and BillStatus<>'1'   and Status<>3                                     ";

            ArrayList arr = new ArrayList();
            if (DeptID != null)
            {
                strSql += " and SellDeptId=@SellDeptId  ";
                arr.Add(new SqlParameter("@SellDeptId", DeptID));
            }
            if (CurrencyType != null)
            {
                strSql += " and  CurrencyType=@CurrencyType  ";
                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            strSql += ") as tt left join officedba.DeptInfo AS d ON tt.SellDeptId = d.ID                                                   ";
            strSql += "group by SellDeptId,weeknum ,weeknum1,d.DeptName                                                                    ";
            strSql += ") s  PIVOT (SUM (RealTotal) FOR weeknum IN                                                                          ";
            strSql += "([MR1],[MR2],[MR3],[MR4],[MR5],[MR6],[MR7],[MR8],[MR9],[MR10],[MR11],[MR12])) as pvt1  PIVOT (                      ";
            strSql += "SUM (CountTotal) FOR weeknum1 IN                                                                                    ";
            strSql += "([MC1],[MC2],[MC3],[MC4],[MC5],[MC6],[MC7],[MC8],[MC9],[MC10],[MC11],[MC12])                                        ";
            strSql += ") AS pvt1) as tt  group by SellDeptId,DeptName ) as ttt                                                             ";


            arr.Add(new SqlParameter("@OrderDate", Year));
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 部门业绩月度对比
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="Year">分析年份</param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndMonth(int? DeptID, string Year, int? CurrencyType)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


            strSql += "select DeptName,MR1,MR2,MR3,MR4,MR5,MR6,MR7,MR8,MR9,MR10,MR11,MR12,                                                 ";
            strSql += "MC1,MC2,MC3,MC4,MC5,MC6, MC7,MC8,MC9,MC10,MC11,MC12,                                                                ";
            strSql += "(MR1+MR2+MR3+MR5+MR4+MR6+MR7+MR8+MR9+MR10+MR11+MR12) as PriceTotal,                                                 ";
            strSql += "(MC1+MC2+MC3+MC4+MC5+MC6+MC7+MC8+MC9+MC10+MC11+MC12) as CountTotal                                                  ";
            strSql += "from (select SellDeptId,isnull(DeptName ,'未知') as DeptName,isnull(sum(MR1),0) as MR1                                                         ";
            strSql += ",isnull(sum(MR2),0) as MR2,isnull(sum(MR3),0) as MR3,isnull(sum(MR4),0) as MR4                                      ";
            strSql += ",isnull(sum(MR5),0) as MR5,isnull(sum(MR6),0) as MR6,isnull(sum(MR7),0) as MR7                                      ";
            strSql += ",isnull(sum(MR8),0) as MR8,isnull(sum(MR9),0) as MR9,isnull(sum(MR10),0) as MR10                                    ";
            strSql += ",isnull(sum(MR11),0) as MR11,isnull(sum(MR12),0) as MR12 ,isnull(sum(MC1),0) as MC1                                 ";
            strSql += ",isnull(sum(MC2),0) as MC2,isnull(sum(MC3),0) as MC3,isnull(sum(MC4),0) as MC4                                      ";
            strSql += ",isnull(sum(MC5),0) as MC5,isnull(sum(MC6),0) as MC6,isnull(sum(MC7),0) as MC7                                      ";
            strSql += ",isnull(sum(MC8),0) as MC8,isnull(sum(MC9),0) as MC9,isnull(sum(MC10),0) as MC10                                    ";
            strSql += ",isnull(sum(MC11),0) as MC11,isnull(sum(MC12),0) as MC12 from (SELECT *FROM (                                       ";
            strSql += "select SellDeptId,sum(RealTotal) as RealTotal,weeknum,weeknum1,sum(CountTotal) as CountTotal,d.DeptName             ";
            if (CurrencyType != null)
            {
                strSql += "from ( select SellDeptId,RealTotal,CountTotal,case when month(OrderDate) =1 then 'MR1' ";

            }
            if (CurrencyType == null)
            {
                strSql += "from ( select SellDeptId,(RealTotal*Rate) as RealTotal,CountTotal,case when month(OrderDate) =1 then 'MR1' ";

            }

            strSql += "when month(OrderDate) =2 then 'MR2' when month(OrderDate) =3 then 'MR3' when month(OrderDate) =4 then 'MR4'         ";
            strSql += "when month(OrderDate) =5 then 'MR5' when month(OrderDate) =6 then 'MR6' when month(OrderDate) =7 then 'MR7'         ";
            strSql += "when month(OrderDate) =8 then 'MR8' when month(OrderDate) =9 then 'MR9' when month(OrderDate) =10 then 'MR10'       ";
            strSql += "when month(OrderDate) =11 then 'MR11' when month(OrderDate) =12 then 'MR12' end as weeknum ,                        ";
            strSql += "case when month(OrderDate) =1 then 'MC1' when month(OrderDate) =2 then 'MC2' when month(OrderDate) =3 then 'MC3'    ";
            strSql += "when month(OrderDate) =4 then 'MC4' when month(OrderDate) =5 then 'MC5' when month(OrderDate) =6 then 'MC6'         ";
            strSql += "when month(OrderDate) =7 then 'MC7' when month(OrderDate) =8 then 'MC8' when month(OrderDate) =9 then 'MC9'         ";
            strSql += "when month(OrderDate) =10 then 'MC10' when month(OrderDate) =11 then 'MC11' when month(OrderDate) =12 then 'MC12'   ";
            strSql += "end as weeknum1 from officedba.SellOrder                                                                            ";
            strSql += "where YEAR(OrderDate)=@OrderDate and CompanyCD=@CompanyCD and BillStatus<>'1'  and Status<>3                                      ";

            ArrayList arr = new ArrayList();
            if (DeptID != null)
            {
                strSql += " and SellDeptId=@SellDeptId  ";
                arr.Add(new SqlParameter("@SellDeptId", DeptID));
            }
            if (CurrencyType != null)
            {
                strSql += " and  CurrencyType=@CurrencyType  ";
                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            strSql += ") as tt left join officedba.DeptInfo AS d ON tt.SellDeptId = d.ID                                                   ";
            strSql += "group by SellDeptId,weeknum ,weeknum1,d.DeptName                                                                    ";
            strSql += ") s  PIVOT (SUM (RealTotal) FOR weeknum IN                                                                          ";
            strSql += "([MR1],[MR2],[MR3],[MR4],[MR5],[MR6],[MR7],[MR8],[MR9],[MR10],[MR11],[MR12])) as pvt1  PIVOT (                      ";
            strSql += "SUM (CountTotal) FOR weeknum1 IN                                                                                    ";
            strSql += "([MC1],[MC2],[MC3],[MC4],[MC5],[MC6],[MC7],[MC8],[MC9],[MC10],[MC11],[MC12])                                        ";
            strSql += ") AS pvt1) as tt  group by SellDeptId,DeptName ) as ttt                                                             ";


            arr.Add(new SqlParameter("@OrderDate", Year));
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            return SqlHelper.ExecuteSql(strSql, arr);
        }

        #endregion

        #region 部门业绩年度对比

        /// <summary>
        /// 部门业绩年度对比
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="Year">分析年份</param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndYear(int? DeptID, string Year, int? CurrencyType,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ArrayList arr = new ArrayList();
            strSql += "select DeptName,sum(isnull(NowR,0)) as NowR,sum(isnull(NowC,0)) as NowC,                     ";
            strSql += "sum(isnull(UpR,0)) as UpR,sum(isnull(UpC,0)) as UpC                                          ";
            if (CurrencyType != null)
            {
                strSql += "from (select sum(s.RealTotal) as RealTotal,sum(s.CountTotal) as CountTotal,                  ";

            }
            if (CurrencyType == null)
            {
                strSql += "from (select sum(s.RealTotal*Rate) as RealTotal,sum(s.CountTotal) as CountTotal,                  ";
            }

            strSql += "'NowR' AS OYR,'NowC' AS OYC,isnull(d.DeptName ,'未知') as DeptName,s.SellDeptId              ";
            strSql += "from officedba.SellOrder as s left join                                                      ";
            strSql += "officedba.DeptInfo AS d ON s.SellDeptId = d.ID                                               ";
            strSql += "where YEAR(OrderDate)=@OrderYear and s.CompanyCD=@CompanyCD and BillStatus<>'1'   and Status<>3          ";
            if (DeptID != null)
            {
                strSql += " and SellDeptId=@SellDeptId  ";

            }
            if (CurrencyType != null)
            {
                strSql += " and  CurrencyType=@CurrencyType  ";

            }
            strSql += "group by s.SellDeptId,d.DeptName                                                             ";
            strSql += "union all                                                                                    ";
            if (CurrencyType != null)
            {
                strSql += "select  sum(s.RealTotal) as RealTotal,sum(s.CountTotal) as CountTotal ,                      ";

            }
            if (CurrencyType == null)
            {
                strSql += "select  sum(s.RealTotal*Rate) as RealTotal,sum(s.CountTotal) as CountTotal ,                      ";

            }

            strSql += "'UpR' AS OYR,'UpC' AS OYC,isnull(d.DeptName ,'未知') as DeptName,s.SellDeptId                ";
            strSql += "from officedba.SellOrder as s left join                                                      ";
            strSql += "officedba.DeptInfo AS d ON s.SellDeptId = d.ID                                               ";
            strSql += "where YEAR(OrderDate)=(@OrderYear-1) and s.CompanyCD=@CompanyCD and BillStatus<>'1'  and Status<>3      ";
            if (DeptID != null)
            {
                strSql += " and SellDeptId=@SellDeptId  ";

            }
            if (CurrencyType != null)
            {
                strSql += " and  CurrencyType=@CurrencyType  ";

            }
            strSql += "group by s.SellDeptId,d.DeptName ) as tt                                                     ";
            strSql += "PIVOT (SUM (RealTotal) FOR OYR IN ([NowR],[UpR])) as pvt1                                    ";
            strSql += "PIVOT (SUM (CountTotal) FOR OYC IN ([NowC],[UpC])) as pvt                                    ";
            strSql += "group by SellDeptId,DeptName                                                                 ";

            if (DeptID != null)
            {
                arr.Add(new SqlParameter("@SellDeptId", DeptID));
            }
            if (CurrencyType != null)
            {

                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            arr.Add(new SqlParameter("@OrderYear", Year));
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);

        }

        /// <summary>
        /// 部门业绩年度对比
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="Year">分析年份</param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndYear(int? DeptID, string Year, int? CurrencyType)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ArrayList arr = new ArrayList();
            strSql += "select DeptName,sum(isnull(NowR,0)) as NowR,sum(isnull(NowC,0)) as NowC,                     ";
            strSql += "sum(isnull(UpR,0)) as UpR,sum(isnull(UpC,0)) as UpC                                          ";
            if (CurrencyType != null)
            {
                strSql += "from (select sum(s.RealTotal) as RealTotal,sum(s.CountTotal) as CountTotal,                  ";

            }
            if (CurrencyType == null)
            {
                strSql += "from (select sum(s.RealTotal*Rate) as RealTotal,sum(s.CountTotal) as CountTotal,                  ";
            }

            strSql += "'NowR' AS OYR,'NowC' AS OYC,isnull(d.DeptName ,'未知') as DeptName,s.SellDeptId              ";
            strSql += "from officedba.SellOrder as s left join                                                      ";
            strSql += "officedba.DeptInfo AS d ON s.SellDeptId = d.ID                                               ";
            strSql += "where YEAR(OrderDate)=@OrderYear and s.CompanyCD=@CompanyCD and BillStatus<>'1'   and Status<>3          ";
            if (DeptID != null)
            {
                strSql += " and SellDeptId=@SellDeptId  ";

            }
            if (CurrencyType != null)
            {
                strSql += " and  CurrencyType=@CurrencyType  ";

            }
            strSql += "group by s.SellDeptId,d.DeptName                                                             ";
            strSql += "union all                                                                                    ";
            if (CurrencyType != null)
            {
                strSql += "select  sum(s.RealTotal) as RealTotal,sum(s.CountTotal) as CountTotal ,                      ";

            }
            if (CurrencyType == null)
            {
                strSql += "select  sum(s.RealTotal*Rate) as RealTotal,sum(s.CountTotal) as CountTotal ,                      ";

            }

            strSql += "'UpR' AS OYR,'UpC' AS OYC,isnull(d.DeptName ,'未知') as DeptName,s.SellDeptId                ";
            strSql += "from officedba.SellOrder as s left join                                                      ";
            strSql += "officedba.DeptInfo AS d ON s.SellDeptId = d.ID                                               ";
            strSql += "where YEAR(OrderDate)=(@OrderYear-1) and s.CompanyCD=@CompanyCD and BillStatus<>'1'  and Status<>3      ";
            if (DeptID != null)
            {
                strSql += " and SellDeptId=@SellDeptId  ";

            }
            if (CurrencyType != null)
            {
                strSql += " and  CurrencyType=@CurrencyType  ";

            }
            strSql += "group by s.SellDeptId,d.DeptName ) as tt                                                     ";
            strSql += "PIVOT (SUM (RealTotal) FOR OYR IN ([NowR],[UpR])) as pvt1                                    ";
            strSql += "PIVOT (SUM (CountTotal) FOR OYC IN ([NowC],[UpC])) as pvt                                    ";
            strSql += "group by SellDeptId,DeptName                                                                 ";

            if (DeptID != null)
            {
                arr.Add(new SqlParameter("@SellDeptId", DeptID));
            }
            if (CurrencyType != null)
            {

                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            arr.Add(new SqlParameter("@OrderYear", Year));
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            return SqlHelper.ExecuteSql(strSql, arr);
        }

        #endregion

        /// <summary>
        /// 部门销售实绩分析表
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="SearchModel">查询的模式:1表示所有数据，2表示分页查处部分数据</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndDate(int? DeptID, DateTime StartDate, DateTime EndDate, int CurrencyType, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号
            EndDate = EndDate.AddDays(1);
            DataTable dt = new DataTable();

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ArrayList arr = new ArrayList();

            strSql += "select SellDeptId,DeptName,count(CustID) as CustCount,sum(CountTotal) as CountTotal,sum(TotalPrice) as TotalPrice,             ";
            strSql += "sum(BackCountTotal) as BackCountTotal,sum(BackTotalPrice) as BackTotalPrice,                                                   ";
            strSql += "sum(DiscountTotal) as DiscountTotal,sum(SaleFeeTotal) as SaleFeeTotal,                                                         ";
            strSql += "sum(BillTotalPrice) as BillTotalPrice,sum(YAccounts) as YAccounts,                                                             ";
            strSql += "sum(NAccounts) as NAccounts,max(MaxCreditDate) as MaxCreditDate,max(NAccounts) as MaxNAccounts,                                ";
            strSql += "(sum(TotalPrice)-sum(DiscountTotal)-sum(BackTotalPrice)-sum(SaleFeeTotal)) as SellRealTotal                                    ";
            strSql += "from                                                                                                                           ";
            strSql += "(select SellDeptId,DeptName,CustID,sum(CountTotal) as CountTotal,sum(TotalPrice) as TotalPrice,                                ";
            strSql += "sum(BackCountTotal) as BackCountTotal,sum(BackTotalPrice) as BackTotalPrice,                                                   ";
            strSql += "sum(DiscountTotal) as DiscountTotal,sum(SaleFeeTotal) as SaleFeeTotal,                                                         ";
            strSql += "sum(BillTotalPrice) as BillTotalPrice,sum(YAccounts) as YAccounts,                                                             ";
            strSql += "sum(NAccounts) as NAccounts,max(MaxCreditDate) as MaxCreditDate,max(NAccounts) as MaxNAccounts,                                ";
            strSql += "(sum(TotalPrice)-sum(DiscountTotal)-sum(BackTotalPrice)) as SellRealTotal                                                      ";
            strSql += "from (select s.SellDeptId,s.CustID,s.CountTotal,(s.TotalPrice*s.Discount*0.01) as TotalPrice,0 as BackCountTotal,0 as BackTotalPrice,                          ";
            strSql += "s.DiscountTotal,s.SaleFeeTotal,isnull(s.RealTotal,0) as BillTotalPrice,                                                        ";
            strSql += "isnull(b.YAccounts,0) as YAccounts,(isnull(s.RealTotal,0)-isnull(b.YAccounts,0)) as NAccounts,d.DeptName,                      ";
            strSql += "(DateDiff(d,s.OrderDate,getdate())-isnull(c.MaxCreditDate,0)) as MaxCreditDate                                                 ";
            strSql += "from officedba. SellOrder as s left join                                                                                       ";
            strSql += "Officedba.BlendingDetails as b on s.CompanyCD=b.CompanyCD and s.OrderNo=b.BillCD and b.BillingType='1' left join                       ";
            strSql += "officedba.CustInfo as c on s.CustID=c.ID left join                                                                             ";
            strSql += "officedba.DeptInfo as d on s.SellDeptId=d.ID                                                                                   ";
            strSql += "where s.Status<>'3' and s.BillStatus<>'1' and s.CompanyCD = @CompanyCD and s.OrderDate >=@StartDate and s.OrderDate<@EndDate  and s.CurrencyType=@CurrencyType   ";
            if (DeptID != null)
            {
                strSql += "  and s.SellDeptId=@SellDeptId  ";
            }
            strSql += "union all                                                                                                                      ";
            strSql += "select s.SellDeptId,s.CustID,0 as CountTotal,0 as TotalPrice,s.CountTotal as BackCountTotal,(s.TotalPrice*s.Discount*0.01) as BackTotalPrice,    ";
            strSql += "0 as DiscountTotal,0 as SaleFeeTotal,(0-isnull(s.RealTotal,0)) as BillTotalPrice,                                                 ";
            strSql += "(0-isnull(b.YAccounts,0)) as YAccounts,(0-(isnull(s.RealTotal,0)-isnull(b.YAccounts,0))) as NAccounts,d.DeptName,                                              ";
            strSql += "0 as MaxCreditDate                                                                                                             ";
            strSql += "from officedba. SellBack as s left join                                                                                        ";
            strSql += "Officedba.BlendingDetails as b on s.CompanyCD=b.CompanyCD and s.BackNo=b.BillCD and b.BillingType='3' left join                        ";
            strSql += "officedba.DeptInfo as d on s.SellDeptId=d.ID                                                                                   ";
            strSql += "where  s.BillStatus<>'1' and  s.CompanyCD = @CompanyCD   and s.BackDate >=@StartDate and s.BackDate<@EndDate     and s.CurrencyType=@CurrencyType              ";
            if (DeptID != null)
            {
                strSql += " and  s.SellDeptId=@SellDeptId  ";
            }
            strSql += " ) as t group by SellDeptId,DeptName,CustID) as t                                                                              ";
            strSql += "group by SellDeptId,DeptName                                                                                                   ";
            if (DeptID != null)
            {
                arr.Add(new SqlParameter("@SellDeptId", DeptID));
            }
            arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@StartDate", StartDate));
            arr.Add(new SqlParameter("@EndDate", EndDate));
            if (SearchModel == "1")
            {
                strSql += " order by " + ord;

                dt = SqlHelper.ExecuteSql(strSql, arr);
            }
            else if (SearchModel == "2")
            {
                dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            }
            return dt;
        }

        /// <summary>
        /// 部门销售实绩分析表
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="SearchModel">查询的模式:1表示所有数据，2表示分页查处部分数据</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndDate(int? DeptID, DateTime StartDate, DateTime EndDate, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号
            EndDate = EndDate.AddDays(1);
            DataTable dt = new DataTable();

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ArrayList arr = new ArrayList();

            strSql += "select SellDeptId,DeptName,count(CustID) as CustCount,sum(CountTotal) as CountTotal,sum(TotalPrice) as TotalPrice,      ";
            strSql += "sum(BackCountTotal) as BackCountTotal,sum(BackTotalPrice) as BackTotalPrice,                                            ";
            strSql += "sum(DiscountTotal) as DiscountTotal,sum(SaleFeeTotal) as SaleFeeTotal,                                                  ";
            strSql += "sum(BillTotalPrice) as BillTotalPrice,sum(YAccounts) as YAccounts,                                                      ";
            strSql += "sum(NAccounts) as NAccounts,max(MaxCreditDate) as MaxCreditDate,max(NAccounts) as MaxNAccounts,                         ";
            strSql += "(sum(TotalPrice)-sum(DiscountTotal)-sum(BackTotalPrice)-sum(SaleFeeTotal)) as SellRealTotal                             ";
            strSql += "from                                                                                                                    ";
            strSql += "(select SellDeptId,DeptName,CustID,sum(CountTotal) as CountTotal,sum(TotalPrice) as TotalPrice,                         ";
            strSql += "sum(BackCountTotal) as BackCountTotal,sum(BackTotalPrice) as BackTotalPrice,                                            ";
            strSql += "sum(DiscountTotal) as DiscountTotal,sum(SaleFeeTotal) as SaleFeeTotal,                                                  ";
            strSql += "sum(BillTotalPrice) as BillTotalPrice,sum(YAccounts) as YAccounts,                                                      ";
            strSql += "sum(NAccounts) as NAccounts,max(MaxCreditDate) as MaxCreditDate,max(NAccounts) as MaxNAccounts,                         ";
            strSql += "(sum(TotalPrice)-sum(DiscountTotal)-sum(BackTotalPrice)) as SellRealTotal                                               ";
            strSql += "from (                                                                                                                  ";
            strSql += "select s.SellDeptId,s.CustID,s.CountTotal,isnull((s.TotalPrice*s.Discount*0.01),0)*s.Rate as  TotalPrice,                                 ";
            strSql += "0 as BackCountTotal,0 as BackTotalPrice,isnull(s.DiscountTotal,0)*s.Rate as DiscountTotal,                              ";
            strSql += "isnull(s.SaleFeeTotal,0)*s.Rate as SaleFeeTotal,isnull(s.RealTotal,0)*s.Rate as BillTotalPrice,                         ";
            strSql += "isnull(b.YAccounts,0)*s.Rate as YAccounts,(isnull(s.RealTotal,0)-isnull(b.YAccounts,0))*s.Rate as NAccounts,            ";
            strSql += "d.DeptName,(DateDiff(d,s.OrderDate,getdate())-isnull(c.MaxCreditDate,0)) as MaxCreditDate                               ";
            strSql += "from officedba. SellOrder as s left join                                                                                ";
            strSql += "Officedba.BlendingDetails as b on s.CompanyCD=b.CompanyCD and s.OrderNo=b.BillCD and b.BillingType='1' left join                ";
            strSql += "officedba.CustInfo as c on s.CustID=c.ID left join                                                                      ";
            strSql += "officedba.DeptInfo as d on s.SellDeptId=d.ID                                                                            ";
            strSql += "where s.Status<>'3' and s.BillStatus<>'1'  and s.CompanyCD = @CompanyCD and s.OrderDate >=@StartDate and s.OrderDate<@EndDate    ";

            if (DeptID != null)
            {
                strSql += "  and s.SellDeptId=@SellDeptId ";
            }
            strSql += "union all                                                                                                               ";
            strSql += "select s.SellDeptId,s.CustID,0 as CountTotal,0 as TotalPrice,s.CountTotal as BackCountTotal,                            ";
            strSql += "isnull((s.TotalPrice*s.Discount*0.01),0)*s.Rate as BackTotalPrice,                                                                        ";
            strSql += "0 as DiscountTotal,0 as SaleFeeTotal,(0-isnull(s.RealTotal,0)*s.Rate) as BillTotalPrice,                                ";
            strSql += "(0-isnull(b.YAccounts,0)*s.Rate) as YAccounts,                                                                          ";
            strSql += "(0-(isnull(s.RealTotal,0)-isnull(b.YAccounts,0))*s.Rate) as NAccounts,d.DeptName,                                       ";
            strSql += "0 as MaxCreditDate                                                                                                      ";
            strSql += "from officedba. SellBack as s left join                                                                                 ";
            strSql += "Officedba.BlendingDetails as b on s.CompanyCD=b.CompanyCD and s.BackNo=b.BillCD and b.BillingType='3' left join                 ";
            strSql += "officedba.DeptInfo as d on s.SellDeptId=d.ID                                                                            ";
            strSql += "where  s.BillStatus<>'1'  and  s.CompanyCD = @CompanyCD   and s.BackDate >=@StartDate and s.BackDate<@EndDate           ";

            if (DeptID != null)
            {
                strSql += " and  s.SellDeptId=@SellDeptId  ";
            }
            strSql += ") as t                                                                                                                  ";
            strSql += "group by SellDeptId,DeptName,CustID) as t                                                                               ";
            strSql += "group by SellDeptId,DeptName                                                                                            ";
            if (DeptID != null)
            {
                arr.Add(new SqlParameter("@SellDeptId", DeptID));
            }

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@StartDate", StartDate));
            arr.Add(new SqlParameter("@EndDate", EndDate));
            if (SearchModel == "1")
            {
                strSql += " order by " + ord;

                dt = SqlHelper.ExecuteSql(strSql, arr);
            }
            else if (SearchModel == "2")
            {
                dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            }
            return dt;
        }

        /// <summary>
        /// 业务员销售实绩分析表
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="Seller">业务员</param>
        /// <param name="CurrencyType">币种</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="SearchModel">查询的模式:1表示所有数据，2表示分页查处部分数据</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndSellerAndDate(int? DeptID, int? Seller, int CurrencyType, DateTime StartDate, DateTime EndDate, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号
            EndDate = EndDate.AddDays(1);
            DataTable dt = new DataTable();

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ArrayList arr = new ArrayList();

            strSql += "select SellDeptId,DeptName,count(CustID) as CustCount,sum(CountTotal) as CountTotal,sum(TotalPrice) as TotalPrice,                ";
            strSql += "sum(BackCountTotal) as BackCountTotal,sum(BackTotalPrice) as BackTotalPrice,                                                      ";
            strSql += "sum(DiscountTotal) as DiscountTotal,sum(SaleFeeTotal) as SaleFeeTotal,                                                            ";
            strSql += "sum(BillTotalPrice) as BillTotalPrice,sum(YAccounts) as YAccounts,Seller,EmployeeName ,                                           ";
            strSql += "sum(NAccounts) as NAccounts,max(MaxCreditDate) as MaxCreditDate,max(NAccounts) as MaxNAccounts,                                   ";
            strSql += "(sum(TotalPrice)-sum(DiscountTotal)-sum(BackTotalPrice)-sum(SaleFeeTotal)) as SellRealTotal                                       ";
            strSql += "from                                                                                                                              ";
            strSql += "(select SellDeptId,DeptName,CustID,sum(CountTotal) as CountTotal,sum(TotalPrice) as TotalPrice,                                   ";
            strSql += "sum(BackCountTotal) as BackCountTotal,sum(BackTotalPrice) as BackTotalPrice,                                                      ";
            strSql += "sum(DiscountTotal) as DiscountTotal,sum(SaleFeeTotal) as SaleFeeTotal,                                                            ";
            strSql += "sum(BillTotalPrice) as BillTotalPrice,sum(YAccounts) as YAccounts,Seller,EmployeeName ,                                           ";
            strSql += "sum(NAccounts) as NAccounts,max(MaxCreditDate) as MaxCreditDate,max(NAccounts) as MaxNAccounts,                                   ";
            strSql += "(sum(TotalPrice)-sum(DiscountTotal)-sum(BackTotalPrice)) as SellRealTotal                                                         ";
            strSql += "from (                                                                                                                            ";
            strSql += "select s.Seller,s.SellDeptId,s.CustID,s.CountTotal,isnull((s.TotalPrice*s.Discount*0.01),0) as  TotalPrice,                                  ";
            strSql += "0 as BackCountTotal,0 as BackTotalPrice,isnull(s.DiscountTotal,0) as DiscountTotal,                                        ";
            strSql += "isnull(s.SaleFeeTotal,0) as SaleFeeTotal,isnull(s.RealTotal,0) as BillTotalPrice,                                   ";
            strSql += "isnull(b.YAccounts,0) as YAccounts,(isnull(s.RealTotal,0)-isnull(b.YAccounts,0)) as NAccounts,                      ";
            strSql += "d.DeptName,(DateDiff(d,s.OrderDate,getdate())-isnull(c.MaxCreditDate,0)) as MaxCreditDate ,e.EmployeeName                         ";
            strSql += "from officedba. SellOrder as s left join                                                                                          ";
            strSql += "Officedba.BlendingDetails as b on s.CompanyCD=b.CompanyCD and s.OrderNo=b.BillCD and b.BillingType='1' left join                          ";
            strSql += "officedba.CustInfo as c on s.CustID=c.ID left join                                                                                ";
            strSql += "officedba.DeptInfo as d on s.SellDeptId=d.ID   left join                                                                          ";
            strSql += "officedba.EmployeeInfo as e on s.Seller=e.ID                                                                                      ";
            strSql += "where s.Status<>'3' and s.BillStatus<>'1' and s.CompanyCD = @CompanyCD and s.OrderDate >=@StartDate and s.OrderDate<@EndDate and s.CurrencyType=@CurrencyType ";

            if (DeptID != null)
            {
                strSql += "  and s.SellDeptId=@SellDeptId ";
            }
            if (Seller != null)
            {
                strSql += " and  s.Seller=@Seller  ";
            }
            strSql += "union all                                                                                                                         ";
            strSql += "select s.Seller,s.SellDeptId,s.CustID,0 as CountTotal,0 as TotalPrice,s.CountTotal as BackCountTotal,                             ";
            strSql += "isnull((s.TotalPrice*s.Discount*0.01),0) as BackTotalPrice,                                                                                  ";
            strSql += "0 as DiscountTotal,0 as SaleFeeTotal,(0-isnull(s.RealTotal,0)) as BillTotalPrice,                                          ";
            strSql += "(0-isnull(b.YAccounts,0)) as YAccounts,                                                                                    ";
            strSql += "(0-(isnull(s.RealTotal,0)-isnull(b.YAccounts,0))) as NAccounts,d.DeptName,                                                 ";
            strSql += "0 as MaxCreditDate,e.EmployeeName                                                                                                 ";
            strSql += "from officedba. SellBack as s left join                                                                                           ";
            strSql += "Officedba.BlendingDetails as b on s.CompanyCD=b.CompanyCD and s.BackNo=b.BillCD and b.BillingType='3' left join                           ";
            strSql += "officedba.DeptInfo as d on s.SellDeptId=d.ID left join                                                                            ";
            strSql += "officedba.EmployeeInfo as e on s.Seller=e.ID                                                                                      ";
            strSql += "where  s.BillStatus<>'1' and  s.CompanyCD = @CompanyCD   and s.BackDate >=@StartDate and s.BackDate<@EndDate and s.CurrencyType=@CurrencyType ";

            if (DeptID != null)
            {
                strSql += " and  s.SellDeptId=@SellDeptId  ";
            }
            if (Seller != null)
            {
                strSql += " and  s.Seller=@Seller  ";
            }
            strSql += ") as t                                                                                                                            ";
            strSql += "group by SellDeptId,DeptName,CustID,Seller,EmployeeName) as t                                                                     ";
            strSql += "group by SellDeptId,DeptName ,Seller,EmployeeName                                                                                 ";
            if (DeptID != null)
            {
                arr.Add(new SqlParameter("@SellDeptId", DeptID));
            }
            if (Seller != null)
            {
                arr.Add(new SqlParameter("@Seller", Seller));
            }
            arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@StartDate", StartDate));
            arr.Add(new SqlParameter("@EndDate", EndDate));
            if (SearchModel == "1")
            {
                strSql += " order by " + ord;

                dt = SqlHelper.ExecuteSql(strSql, arr);
            }
            else if (SearchModel == "2")
            {
                dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            }
            return dt;
        }

        /// <summary>
        /// 业务员销售实绩分析表
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="Seller">业务员</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="SearchModel">查询的模式:1表示所有数据，2表示分页查处部分数据</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndSellerAndDate(int? DeptID, int? Seller, DateTime StartDate, DateTime EndDate, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号
            EndDate = EndDate.AddDays(1);
            DataTable dt = new DataTable();

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ArrayList arr = new ArrayList();

            strSql += "select SellDeptId,DeptName,count(CustID) as CustCount,sum(CountTotal) as CountTotal,sum(TotalPrice) as TotalPrice,                ";
            strSql += "sum(BackCountTotal) as BackCountTotal,sum(BackTotalPrice) as BackTotalPrice,                                                      ";
            strSql += "sum(DiscountTotal) as DiscountTotal,sum(SaleFeeTotal) as SaleFeeTotal,                                                            ";
            strSql += "sum(BillTotalPrice) as BillTotalPrice,sum(YAccounts) as YAccounts,Seller,EmployeeName ,                                           ";
            strSql += "sum(NAccounts) as NAccounts,max(MaxCreditDate) as MaxCreditDate,max(NAccounts) as MaxNAccounts,                                   ";
            strSql += "(sum(TotalPrice)-sum(DiscountTotal)-sum(BackTotalPrice)-sum(SaleFeeTotal)) as SellRealTotal                                       ";
            strSql += "from                                                                                                                              ";
            strSql += "(select SellDeptId,DeptName,CustID,sum(CountTotal) as CountTotal,sum(TotalPrice) as TotalPrice,                                   ";
            strSql += "sum(BackCountTotal) as BackCountTotal,sum(BackTotalPrice) as BackTotalPrice,                                                      ";
            strSql += "sum(DiscountTotal) as DiscountTotal,sum(SaleFeeTotal) as SaleFeeTotal,                                                            ";
            strSql += "sum(BillTotalPrice) as BillTotalPrice,sum(YAccounts) as YAccounts,Seller,EmployeeName ,                                           ";
            strSql += "sum(NAccounts) as NAccounts,max(MaxCreditDate) as MaxCreditDate,max(NAccounts) as MaxNAccounts,                                   ";
            strSql += "(sum(TotalPrice)-sum(DiscountTotal)-sum(BackTotalPrice)) as SellRealTotal                                                         ";
            strSql += "from (                                                                                                                            ";
            strSql += "select s.Seller,s.SellDeptId,s.CustID,s.CountTotal,isnull((s.TotalPrice*s.Discount*0.01),0)*s.Rate as  TotalPrice,                                  ";
            strSql += "0 as BackCountTotal,0 as BackTotalPrice,isnull(s.DiscountTotal,0)*s.Rate as DiscountTotal,                                        ";
            strSql += "isnull(s.SaleFeeTotal,0)*s.Rate as SaleFeeTotal,isnull(s.RealTotal,0)*s.Rate as BillTotalPrice,                                   ";
            strSql += "isnull(b.YAccounts,0)*s.Rate as YAccounts,(isnull(s.RealTotal,0)-isnull(b.YAccounts,0))*s.Rate as NAccounts,                      ";
            strSql += "d.DeptName,(DateDiff(d,s.OrderDate,getdate())-isnull(c.MaxCreditDate,0)) as MaxCreditDate ,e.EmployeeName                         ";
            strSql += "from officedba. SellOrder as s left join                                                                                          ";
            strSql += "Officedba.BlendingDetails as b on s.CompanyCD=b.CompanyCD and s.OrderNo=b.BillCD and b.BillingType='1' left join                          ";
            strSql += "officedba.CustInfo as c on s.CustID=c.ID left join                                                                                ";
            strSql += "officedba.DeptInfo as d on s.SellDeptId=d.ID   left join                                                                          ";
            strSql += "officedba.EmployeeInfo as e on s.Seller=e.ID                                                                                      ";
            strSql += "where s.Status<>'3' and s.BillStatus<>'1' and s.CompanyCD = @CompanyCD and s.OrderDate >=@StartDate and s.OrderDate<@EndDate      ";

            if (DeptID != null)
            {
                strSql += "  and s.SellDeptId=@SellDeptId ";
            }
            if (Seller != null)
            {
                strSql += " and  s.Seller=@Seller  ";
            }
            strSql += "union all                                                                                                                         ";
            strSql += "select s.Seller,s.SellDeptId,s.CustID,0 as CountTotal,0 as TotalPrice,s.CountTotal as BackCountTotal,                             ";
            strSql += "isnull((s.TotalPrice*s.Discount*0.01),0)*s.Rate as BackTotalPrice,                                                                                  ";
            strSql += "0 as DiscountTotal,0 as SaleFeeTotal,(0-isnull(s.RealTotal,0)*s.Rate) as BillTotalPrice,                                          ";
            strSql += "(0-isnull(b.YAccounts,0)*s.Rate) as YAccounts,                                                                                    ";
            strSql += "(0-(isnull(s.RealTotal,0)-isnull(b.YAccounts,0))*s.Rate) as NAccounts,d.DeptName,                                                 ";
            strSql += "0 as MaxCreditDate,e.EmployeeName                                                                                                 ";
            strSql += "from officedba. SellBack as s left join                                                                                           ";
            strSql += "Officedba.BlendingDetails as b on s.CompanyCD=b.CompanyCD and s.BackNo=b.BillCD and b.BillingType='3' left join                           ";
            strSql += "officedba.DeptInfo as d on s.SellDeptId=d.ID left join                                                                            ";
            strSql += "officedba.EmployeeInfo as e on s.Seller=e.ID                                                                                      ";
            strSql += "where  s.BillStatus<>'1' and  s.CompanyCD = @CompanyCD   and s.BackDate >=@StartDate and s.BackDate<@EndDate                      ";

            if (DeptID != null)
            {
                strSql += " and  s.SellDeptId=@SellDeptId  ";
            }
            if (Seller != null)
            {
                strSql += " and  s.Seller=@Seller  ";
            }
            strSql += ") as t                                                                                                                            ";
            strSql += "group by SellDeptId,DeptName,CustID,Seller,EmployeeName) as t                                                                     ";
            strSql += "group by SellDeptId,DeptName ,Seller,EmployeeName                                                                                 ";
            if (DeptID != null)
            {
                arr.Add(new SqlParameter("@SellDeptId", DeptID));
            }
            if (Seller != null)
            {
                arr.Add(new SqlParameter("@Seller", Seller));
            }
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@StartDate", StartDate));
            arr.Add(new SqlParameter("@EndDate", EndDate));
            if (SearchModel == "1")
            {
                strSql += " order by " + ord;

                dt = SqlHelper.ExecuteSql(strSql, arr);
            }
            else if (SearchModel == "2")
            {
                dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            }
            return dt;
        }

        /// <summary>
        /// 部门业绩与成长率分析
        /// </summary>
        /// <param name="DeptID">部门</param>
        /// <param name="iCount">时间的跨度</param>
        /// <param name="CurrencyType">币种</param>
        /// <param name="EndYear">截止的年份</param>
        /// <param name="SearchModel">查询的模式:1表示所有数据，2表示分页查处部分数据</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable ReportByDeptGetGrow(int? DeptID, int iCount, int? CurrencyType, int EndYear, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            DataTable dt = new DataTable();
            if (iCount == 3)
            {
                dt = ReportByDeptGetGrowInThreeYear(DeptID, CurrencyType, EndYear, SearchModel, pageIndex, pageCount, ord, ref  TotalCount);
            }
            else if (iCount == 5)
            {
                dt = ReportByDeptGetGrowInFiveYear(DeptID, CurrencyType, EndYear, SearchModel, pageIndex, pageCount, ord, ref  TotalCount);
            }
            return dt;
        }

        /// <summary>
        /// 部门业绩与成长率分析(跨度三年)
        /// </summary>
        /// <param name="DeptID">部门</param>
        /// <param name="CurrencyType">币种</param>
        /// <param name="EndYear">截止的年份</param>
        /// <param name="SearchModel">查询的模式:1表示所有数据，2表示分页查处部分数据</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        private static DataTable ReportByDeptGetGrowInThreeYear(int? DeptID, int? CurrencyType, int EndYear, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号

            DataTable dt = new DataTable();

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ArrayList arr = new ArrayList();

            strSql += "select DeptName,SellDeptId, Y1,Y2,Y3,'' as Grow1, case  when Y1 =0 and Y2=0 then ''            ";
            strSql += "when  (Y1 <>0) then CAST(((Y2-Y1)/Y1*100) as varchar) when (Y2 <>0) and ((Y1=0) )              ";
            strSql += "then '100.00' end as Grow2,case  when Y2 =0 and Y3=0 then '' when  (Y2 <>0) then               ";
            strSql += "CAST(((Y3-Y2)/Y2*100) as varchar) when (Y3 <>0) and ((Y2=0) ) then '100.00' end as Grow3       ";
            strSql += "from( select DeptName,SellDeptId,                                                              ";
            strSql += "isnull([" + (EndYear - 2).ToString() + "],0) as Y1,isnull([" + (EndYear - 1).ToString() + "],0) as Y2, ";
            strSql += "isnull([" + EndYear.ToString() + "],0) as Y3 from (                                            ";
            if (CurrencyType == null)
            {
                strSql += "select  sum((s.TotalPrice*s.Discount*s.Rate/100)) as TotalPrice,                               ";
            }
            else
            {
                strSql += "select  sum((s.TotalPrice*s.Discount/100)) as TotalPrice,                               ";
            }
            strSql += "isnull(d.DeptName ,'未知') as DeptName,s.SellDeptId ,YEAR(s.OrderDate) as Orderyear              ";
            strSql += "from officedba.SellOrder as s left join                                                        ";
            strSql += "officedba.DeptInfo AS d ON s.SellDeptId = d.ID                                                 ";
            strSql += "where  s.Status<>'3' and s.BillStatus<>'1' and s.CompanyCD=@CompanyCD and YEAR(s.OrderDate)<=" + EndYear;
            strSql += "  and YEAR(s.OrderDate) >=" + (EndYear - 2).ToString();
            if (DeptID != null)
            {
                strSql += " and  s.SellDeptId=@SellDeptId  ";
            }
            if (CurrencyType != null)
            {
                strSql += " and  s.CurrencyType=@CurrencyType  ";
            }
            strSql += "group by s.SellDeptId,d.DeptName,YEAR(OrderDate)                                               ";
            strSql += ") as tt PIVOT (SUM (TotalPrice) FOR Orderyear IN ";
            strSql += "([" + (EndYear - 2).ToString() + "],[" + (EndYear - 1).ToString() + "],[" + EndYear.ToString() + "])) as pvt1  ) as tt ";

            if (DeptID != null)
            {
                arr.Add(new SqlParameter("@SellDeptId", DeptID));
            }
            if (CurrencyType != null)
            {
                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            if (SearchModel == "1")
            {
                strSql += " order by " + ord;

                dt = SqlHelper.ExecuteSql(strSql, arr);
            }
            else if (SearchModel == "2")
            {
                dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            }

            return dt;
        }

        /// <summary>
        /// 部门业绩与成长率分析(跨度五年)
        /// </summary>
        /// <param name="DeptID">部门</param>
        /// <param name="CurrencyType">币种</param>
        /// <param name="EndYear">截止的年份</param>
        /// <param name="SearchModel">查询的模式:1表示所有数据，2表示分页查处部分数据</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        private static DataTable ReportByDeptGetGrowInFiveYear(int? DeptID, int? CurrencyType, int EndYear, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号

            DataTable dt = new DataTable();

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ArrayList arr = new ArrayList();

            strSql += "select DeptName,SellDeptId, Y1,Y2,Y3,Y4,Y5,'' as Grow1, case  when Y1 =0 and Y2=0 then ''                 ";
            strSql += "when  (Y1 <>0) then CAST(((Y2-Y1)/Y1*100) as varchar) when (Y2 <>0) and ((Y1=0) )                         ";
            strSql += "then '100.00' end as Grow2,case  when Y2 =0 and Y3=0 then '' when  (Y2 <>0) then                          ";
            strSql += "CAST(((Y3-Y2)/Y2*100) as varchar) when (Y3 <>0) and ((Y2=0) ) then '100.00' end as Grow3,                 ";
            strSql += "case  when Y3 =0 and Y4=0 then '' when  (Y3 <>0) then                                                     ";
            strSql += "CAST(((Y4-Y3)/Y3*100) as varchar) when (Y4 <>0) and ((Y3=0) ) then '100.00' end as Grow4,                 ";
            strSql += "case  when Y4 =0 and Y5=0 then '' when  (Y4 <>0) then                                                     ";
            strSql += "CAST(((Y5-Y4)/Y4*100) as varchar) when (Y5 <>0) and ((Y4=0) ) then '100.00' end as Grow5                  ";
            strSql += "from(select DeptName,SellDeptId,isnull([" + (EndYear - 4).ToString() + "],0) as Y1,isnull([" + (EndYear - 3).ToString() + "],0) as Y2, ";
            strSql += "isnull([" + (EndYear - 2).ToString() + "],0) as Y3,isnull([" + (EndYear - 1).ToString() + "],0) as Y4, ";
            strSql += "isnull([" + EndYear.ToString() + "],0) as Y5 from ( ";

            if (CurrencyType == null)
            {
                strSql += "select  sum((s.TotalPrice*s.Discount*s.Rate/100)) as TotalPrice,                               ";
            }
            else
            {
                strSql += "select  sum((s.TotalPrice*s.Discount/100)) as TotalPrice,                               ";
            }
            strSql += "isnull(d.DeptName ,'未知') as DeptName,s.SellDeptId ,YEAR(OrderDate) as Orderyear                         ";
            strSql += "from officedba.SellOrder as s left join                                                                   ";
            strSql += "officedba.DeptInfo AS d ON s.SellDeptId = d.ID                                                            ";
            strSql += "where  s.Status<>'3' and s.BillStatus<>'1' and s.CompanyCD=@CompanyCD and YEAR(s.OrderDate)<=" + EndYear;

            strSql += "  and YEAR(s.OrderDate) >=" + (EndYear - 4).ToString();
            if (DeptID != null)
            {
                strSql += " and  s.SellDeptId=@SellDeptId  ";
            }
            if (CurrencyType != null)
            {
                strSql += " and  s.CurrencyType=@CurrencyType  ";
            }
            strSql += "group by s.SellDeptId,d.DeptName,YEAR(OrderDate)                                                          ";


            strSql += ") as tt PIVOT (SUM (TotalPrice) FOR Orderyear IN(  ";
            strSql += "[" + (EndYear - 4).ToString() + "],[" + (EndYear - 3).ToString() + "], ";
            strSql += "[" + (EndYear - 2).ToString() + "],[" + (EndYear - 1).ToString() + "],[" + EndYear.ToString() + "])) as pvt1  ) as tt ";

            if (DeptID != null)
            {
                arr.Add(new SqlParameter("@SellDeptId", DeptID));
            }
            if (CurrencyType != null)
            {
                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            if (SearchModel == "1")
            {
                strSql += " order by " + ord;

                dt = SqlHelper.ExecuteSql(strSql, arr);
            }
            else if (SearchModel == "2")
            {
                dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            }

            return dt;
        }


        /// <summary>
        /// 业务员业绩与成长率分析
        /// </summary>
        /// <param name="DeptID">部门</param>
        /// <param name="iCount">时间的跨度</param>
        /// <param name="CurrencyType">币种</param>
        /// <param name="EndYear">截止的年份</param>
        /// <param name="SearchModel">查询的模式:1表示所有数据，2表示分页查处部分数据</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndSellerGetGrow(int? DeptID, int? Seller, int iCount, int? CurrencyType, int EndYear, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            DataTable dt = new DataTable();
            if (iCount == 3)
            {
                dt = ReportByDeptAndSellerGetGrowInThreeYear(DeptID, Seller, CurrencyType, EndYear, SearchModel, pageIndex, pageCount, ord, ref  TotalCount);
            }
            else if (iCount == 5)
            {
                dt = ReportByDeptAndSellerGetGrowInFiveYear(DeptID, Seller, CurrencyType, EndYear, SearchModel, pageIndex, pageCount, ord, ref  TotalCount);
            }
            return dt;
        }

        /// <summary>
        /// 业务员业绩与成长率分析(跨度三年)
        /// </summary>
        /// <param name="DeptID">部门</param>
        /// <param name="CurrencyType">币种</param>
        /// <param name="EndYear">截止的年份</param>
        /// <param name="SearchModel">查询的模式:1表示所有数据，2表示分页查处部分数据</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        private static DataTable ReportByDeptAndSellerGetGrowInThreeYear(int? DeptID, int? Seller, int? CurrencyType, int EndYear, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号

            DataTable dt = new DataTable();

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ArrayList arr = new ArrayList();

            strSql += "select DeptName,SellDeptId, Y1,Y2,Y3,'' as Grow1, case  when Y1 =0 and Y2=0 then ''            ";
            strSql += "when  (Y1 <>0) then CAST(((Y2-Y1)/Y1*100) as varchar) when (Y2 <>0) and ((Y1=0) )              ";
            strSql += "then '100.00' end as Grow2,case  when Y2 =0 and Y3=0 then '' when  (Y2 <>0) then               ";
            strSql += "CAST(((Y3-Y2)/Y2*100) as varchar) when (Y3 <>0) and ((Y2=0) ) then '100.00' end as Grow3 ,Seller,EmployeeName      ";
            strSql += "from( select DeptName,SellDeptId,                                                              ";
            strSql += "isnull([" + (EndYear - 2).ToString() + "],0) as Y1,isnull([" + (EndYear - 1).ToString() + "],0) as Y2, ";
            strSql += "isnull([" + EndYear.ToString() + "],0) as Y3,Seller,EmployeeName from (                                            ";
            if (CurrencyType == null)
            {
                strSql += "select  sum((s.TotalPrice*s.Discount*s.Rate/100)) as TotalPrice,                               ";
            }
            else
            {
                strSql += "select  sum((s.TotalPrice*s.Discount/100)) as TotalPrice,                               ";
            }
            strSql += "isnull(d.DeptName ,'未知') as DeptName,s.SellDeptId ,YEAR(s.OrderDate) as Orderyear,s.Seller,e.EmployeeName ";
            strSql += "from officedba.SellOrder as s left join                                                        ";
            strSql += "officedba.DeptInfo AS d ON s.SellDeptId = d.ID   left join officedba.EmployeeInfo as e on s.Seller=e.ID ";
            strSql += "where  s.Status<>'3' and s.BillStatus<>'1' and s.CompanyCD=@CompanyCD and YEAR(s.OrderDate)<=" + EndYear;
            strSql += "  and YEAR(s.OrderDate) >=" + (EndYear - 2).ToString();
            if (DeptID != null)
            {
                strSql += " and  s.SellDeptId=@SellDeptId  ";
                arr.Add(new SqlParameter("@SellDeptId", DeptID));
            }
            if (CurrencyType != null)
            {
                strSql += " and  s.CurrencyType=@CurrencyType  ";
                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            if (Seller != null)
            {
                strSql += " and  s.Seller=@Seller  ";
                arr.Add(new SqlParameter("@Seller", Seller));
            }
            strSql += "group by s.SellDeptId,d.DeptName,YEAR(OrderDate),s.Seller,e.EmployeeName ";
            strSql += ") as tt PIVOT (SUM (TotalPrice) FOR Orderyear IN ";
            strSql += "([" + (EndYear - 2).ToString() + "],[" + (EndYear - 1).ToString() + "],[" + EndYear.ToString() + "])) as pvt1  ) as tt ";


            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            if (SearchModel == "1")
            {
                strSql += " order by " + ord;

                dt = SqlHelper.ExecuteSql(strSql, arr);
            }
            else if (SearchModel == "2")
            {
                dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            }

            return dt;
        }

        /// <summary>
        /// 业务员业绩与成长率分析(跨度五年)
        /// </summary>
        /// <param name="DeptID">部门</param>
        /// <param name="CurrencyType">币种</param>
        /// <param name="EndYear">截止的年份</param>
        /// <param name="SearchModel">查询的模式:1表示所有数据，2表示分页查处部分数据</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        private static DataTable ReportByDeptAndSellerGetGrowInFiveYear(int? DeptID, int? Seller, int? CurrencyType, int EndYear, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号

            DataTable dt = new DataTable();

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ArrayList arr = new ArrayList();

            strSql += "select DeptName,SellDeptId, Y1,Y2,Y3,Y4,Y5,'' as Grow1, case  when Y1 =0 and Y2=0 then ''                 ";
            strSql += "when  (Y1 <>0) then CAST(((Y2-Y1)/Y1*100) as varchar) when (Y2 <>0) and ((Y1=0) )                         ";
            strSql += "then '100.00' end as Grow2,case  when Y2 =0 and Y3=0 then '' when  (Y2 <>0) then                          ";
            strSql += "CAST(((Y3-Y2)/Y2*100) as varchar) when (Y3 <>0) and ((Y2=0) ) then '100.00' end as Grow3,                 ";
            strSql += "case  when Y3 =0 and Y4=0 then '' when  (Y3 <>0) then                                                     ";
            strSql += "CAST(((Y4-Y3)/Y3*100) as varchar) when (Y4 <>0) and ((Y3=0) ) then '100.00' end as Grow4,                 ";
            strSql += "case  when Y4 =0 and Y5=0 then '' when  (Y4 <>0) then                                                     ";
            strSql += "CAST(((Y5-Y4)/Y4*100) as varchar) when (Y5 <>0) and ((Y4=0) ) then '100.00' end as Grow5 ,Seller,EmployeeName                 ";
            strSql += "from(select DeptName,SellDeptId,isnull([" + (EndYear - 4).ToString() + "],0) as Y1,isnull([" + (EndYear - 3).ToString() + "],0) as Y2, ";
            strSql += "isnull([" + (EndYear - 2).ToString() + "],0) as Y3,isnull([" + (EndYear - 1).ToString() + "],0) as Y4, ";
            strSql += "isnull([" + EndYear.ToString() + "],0) as Y5,Seller,EmployeeName from ( ";

            if (CurrencyType == null)
            {
                strSql += "select  sum((s.TotalPrice*s.Discount*s.Rate/100)) as TotalPrice,                               ";
            }
            else
            {
                strSql += "select  sum((s.TotalPrice*s.Discount/100)) as TotalPrice,                               ";
            }
            strSql += "isnull(d.DeptName ,'未知') as DeptName,s.SellDeptId ,YEAR(OrderDate) as Orderyear  ,s.Seller,e.EmployeeName ";
            strSql += "from officedba.SellOrder as s left join                                                                   ";
            strSql += "officedba.DeptInfo AS d ON s.SellDeptId = d.ID   left join officedba.EmployeeInfo as e on s.Seller=e.ID ";
            strSql += "where  s.Status<>'3' and s.BillStatus<>'1' and s.CompanyCD=@CompanyCD and YEAR(s.OrderDate)<=" + EndYear;

            strSql += "  and YEAR(s.OrderDate) >=" + (EndYear - 4).ToString();
            if (DeptID != null)
            {
                strSql += " and  s.SellDeptId=@SellDeptId  ";
                arr.Add(new SqlParameter("@SellDeptId", DeptID));
            }
            if (CurrencyType != null)
            {
                strSql += " and  s.CurrencyType=@CurrencyType  ";
                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            if (Seller != null)
            {
                strSql += " and  s.Seller=@Seller  ";
                arr.Add(new SqlParameter("@Seller", Seller));
            }
            strSql += "group by s.SellDeptId,d.DeptName,YEAR(OrderDate) ,s.Seller,e.EmployeeName                                                         ";


            strSql += ") as tt PIVOT (SUM (TotalPrice) FOR Orderyear IN(  ";
            strSql += "[" + (EndYear - 4).ToString() + "],[" + (EndYear - 3).ToString() + "], ";
            strSql += "[" + (EndYear - 2).ToString() + "],[" + (EndYear - 1).ToString() + "],[" + EndYear.ToString() + "])) as pvt1  ) as tt ";


            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            if (SearchModel == "1")
            {
                strSql += " order by " + ord;

                dt = SqlHelper.ExecuteSql(strSql, arr);
            }
            else if (SearchModel == "2")
            {
                dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            }

            return dt;
        }
        #endregion

        #region 新报表

        /// <summary>
        /// 销售订单数量部门分布
        /// </summary>
        /// <param name="Status">销售订单状态</param>
        /// <param name="Type">订单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByDeptNum(string Status, string Type, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(b.DeptName) as Name,count(1) as Counts,a.selldeptId as DeptId  ");
            sb.Append(" from officedba. SellOrder as a left join officedba.DeptInfo as b on a.sellDeptId=b.Id  ");
            sb.Append(" where a.selldeptId is not null and a.selldeptId !='' and a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");
            if (Status != "")
            {
                sb.Append("and a.Status=");
                sb.Append(Status);
            }
            if (Type != "")
            {
                sb.Append("and a.BusiType=");
                sb.Append(Type);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }

            sb.Append(" group by a.selldeptId ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售订单数量人员分布
        /// </summary>
        /// <param name="Status">销售订单状态</param>
        /// <param name="Type">订单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByPersonNum(string Status, string Type, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(b.EmployeeName) as Name,count(1) as Counts,a.Seller ");
            sb.Append(" from officedba. SellOrder as a left join officedba.EmployeeInfo as b on a.Seller=b.Id  ");
            sb.Append(" where a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (Status != "")
            {
                sb.Append("and a.Status=");
                sb.Append(Status);
            }
            if (Type != "")
            {
                sb.Append("and a.BusiType=");
                sb.Append(Type);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by a.Seller ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售订单数量状态分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByStateNum(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select CASE Status WHEN '1' THEN '处理中' WHEN '2' THEN '处理完' WHEN '3'  THEN '终止' END as Name,count(1) as Counts,Status ");
            sb.Append(" from officedba. SellOrder ");
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
                sb.Append(" and OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and OrderDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by  Status ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售订单数量类型分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByTypeNum(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select CASE BusiType WHEN '1' THEN '普通销售' WHEN '2' THEN '委托代销' WHEN '3'  THEN '直运' WHEN '4'  THEN '零售' WHEN '5'  THEN '销售调拨'  END as Name,count(1) as Counts,BusiType ");
            sb.Append(" from officedba. SellOrder ");
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
                sb.Append("and OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and OrderDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by BusiType ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售订单数量区域分布
        /// </summary>
        /// <param name="GroupType">部门或人员</param>
        /// <param name="DeptOrEmployeeId">部门或人员ID</param>
        /// <param name="State">状态</param>
        /// <param name="BusiType">分类</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByAreaNum(string GroupType, string DeptOrEmployeeId, string Status, string BusiType, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(c.TypeName) as Name,count(1) as Counts,b.AreaId from officedba.sellorder as a ");
            sb.Append(" left join officedba.custinfo as b on a.CustId=b.Id ");
            sb.Append(" left join officedba.CodePublicType as c on b.AreaId=c.Id where b.AreaId !=0 and b.AreaId is not null ");
            sb.Append(" and a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

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

            if (Status != "")
            {
                sb.Append("and a.Status=");
                sb.Append(Status);
            }

            if (BusiType != "")
            {
                sb.Append(" and a.BusiType=");
                sb.Append(BusiType);
            }

            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }

            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by b.AreaId ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售订单走势数量分析
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
        public static DataTable GetOrderByTrendNum(string GroupType, string DeptOrEmployeeId, string DateType, string State, string BusiType, string BeginDate, string EndDate, string AreaId)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string SearchField = "";
            if (DateType == "1")
            {
                SearchField = "dateName(year,a.OrderDate)+'年'";
            }
            else if (DateType == "2")
            {
                SearchField = "dateName(year,a.OrderDate)+'年-'+dateName(month,a.OrderDate)+'月'";
            }
            else
            {
                SearchField = "dateName(year,a.OrderDate)+'年-'+dateName(week,a.OrderDate)+'周'";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append(SearchField);
            sb.Append(" as Name,count(1) as Counts ");
            sb.Append(" from officedba. SellOrder as a ");
            sb.Append(" left join officedba.custinfo as b on a.CustId=b.Id ");
            sb.Append(" where  a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (State != "")
            {
                sb.Append(" and a.Status=");
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
                sb.Append(" and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append(" and a.OrderDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by  ");
            sb.Append(SearchField);
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售订单金额部门分布
        /// </summary>
        /// <param name="Status">销售订单状态</param>
        /// <param name="Type">订单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByDeptPrice(string Status, string Type, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(b.DeptName) as Name,sum(isnull(RealTotal,0)*isnull(Rate,1)) as Counts,a.selldeptId as DeptId  ");
            sb.Append(" from officedba. SellOrder as a left join officedba.DeptInfo as b on a.sellDeptId=b.Id  ");
            sb.Append(" where a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (Status != "")
            {
                sb.Append("and a.Status=");
                sb.Append(Status);
            }
            if (Type != "")
            {
                sb.Append("and a.BusiType=");
                sb.Append(Type);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");
            }
            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }


            sb.Append(" group by a.selldeptId ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售订单金额人员分布
        /// </summary>
        /// <param name="Status">销售订单状态</param>
        /// <param name="Type">订单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByPersonPrice(string Status, string Type, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(b.EmployeeName) as Name,sum(RealTotal*Rate) as Counts,a.Seller ");
            sb.Append(" from officedba. SellOrder as a left join officedba.EmployeeInfo as b on a.Seller=b.Id  ");
            sb.Append(" where a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (Status != "")
            {
                sb.Append("and a.Status=");
                sb.Append(Status);
            }
            if (Type != "")
            {
                sb.Append("and a.BusiType=");
                sb.Append(Type);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by a.Seller ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }


        /// <summary>
        /// 销售订单金额状态分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByStatePrice(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select  CASE Status WHEN '1' THEN '处理中' WHEN '2' THEN '处理完' WHEN '3'  THEN '终止' END as Name,sum(RealTotal*Rate) as Counts,Status ");
            sb.Append(" from officedba. SellOrder ");
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
                sb.Append(" and OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and OrderDate< DATEADD(day, 2,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by  Status ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售订单金额类型分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByTypePrice(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select CASE BusiType WHEN '1' THEN '普通销售' WHEN '2' THEN '委托代销' WHEN '3'  THEN '直运' WHEN '4'  THEN '零售' WHEN '5'  THEN '销售调拨'  END as Name,sum(RealTotal*Rate) as Counts,BusiType ");
            sb.Append(" from officedba.SellOrder ");
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
                sb.Append("and OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and OrderDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by BusiType ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售订单金额区域分布
        /// </summary>
        /// <param name="GroupType">部门或人员</param>
        /// <param name="DeptOrEmployeeId">部门或人员ID</param>
        /// <param name="State">状态</param>
        /// <param name="BusiType">分类</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByAreaPrice(string GroupType, string DeptOrEmployeeId, string Status, string BusiType, string BeginDate, string EndDate)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(c.TypeName) as Name,sum(a.RealTotal*a.Rate) as Counts,b.AreaId from officedba.sellorder as a ");
            sb.Append(" left join officedba.custinfo as b on a.CustId=b.Id ");
            sb.Append(" left join officedba.CodePublicType as c on b.AreaId=c.Id where b.AreaId !=0 and b.AreaId is not null ");
            sb.Append(" and a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

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

            if (Status != "")
            {
                sb.Append("and a.Status=");
                sb.Append(Status);
            }

            if (BusiType != "")
            {
                sb.Append("and a.BusiType=");
                sb.Append(BusiType);
            }

            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }

            if (BeginDate != "")
            {
                sb.Append("and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.OrderDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by b.AreaId ");
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        /// <summary>
        /// 销售订单走势金额分析
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
        public static DataTable GetOrderByTrendPrice(string GroupType, string DeptOrEmployeeId, string DateType, string State, string BusiType, string BeginDate, string EndDate, string AreaId)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string SearchField = "";
            if (DateType == "1")
            {
                SearchField = "dateName(year,a.OrderDate)+'年'";
            }
            else if (DateType == "2")
            {
                SearchField = "dateName(year,a.OrderDate)+'年-'+dateName(month,a.OrderDate)+'月'";
            }
            else
            {
                SearchField = "dateName(year,a.OrderDate)+'年-'+dateName(week,a.OrderDate)+'周'";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append(SearchField);
            sb.Append(" as Name,sum(a.RealTotal*a.Rate) as Counts ");
            sb.Append(" from officedba. SellOrder as a ");
            sb.Append(" left join officedba.custinfo as b on a.CustId=b.Id ");
            sb.Append(" where  a.companyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (State != "")
            {
                sb.Append(" and a.Status=");
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
                sb.Append(" and a.OrderDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append(" and a.OrderDate< DATEADD(day, 1,Convert(datetime,'");
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
        public static DataTable GetSellOrderDetail(string GroupType, string DeptOrEmployeeId, string DateType, string DateValue, string State, string BusiType, string BeginDate, string EndDate, string AreaId, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;

            strSql += "SELECT s.ID, s.OrderNo, s.Title,s.ModifiedDate, CONVERT(varchar(100), s.OrderDate, 23) AS OrderDate,                                          ";
            strSql += "isnull(s.RealTotal,0)*isnull(s.Rate,1) RealTotal, c.CustName, e.EmployeeName,                                                                                      ";
            strSql += "CASE s.FromType WHEN 0 THEN '无来源' WHEN 1 THEN '销售报价单' WHEN 2 THEN '销售合同'  WHEN 3 THEN '销售机会'  END AS FromTypeText,                      ";
            strSql += "CASE s.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更'                                                     ";
            strSql += "WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText,                                                           ";
            strSql += "CASE (SELECT TOP 1 FlowStatus                                                                                             ";
            strSql += "FROM officedba.FlowInstance                                                                                                    ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 3                                                                  ";
            strSql += "ORDER BY ModifiedDate DESC) when NULL THEN '' when 1 THEN '待审批' WHEN  2 THEN '审批中' WHEN   3 THEN '审批通过' WHEN  4 THEN '审批不通过' WHEN  5 THEN '撤销审批' END AS FlowInstanceText, isnull(CASE ";
            strSql += "(((SELECT count(1)                                                                                                             ";
            strSql += "FROM officedba.SellSend AS soo                                                                                                 ";
            strSql += "WHERE soo.FromType = '1' AND soo.FromBillID = s.ID)+                                                                           ";
            strSql += "(SELECT count(1)                                                                                                               ";
            strSql += "FROM officedba.SellSendDetail                                                                                                  ";
            strSql += "WHERE FromType = '1' AND FromBillID = s.ID)+                                                                                   ";
            strSql += "(SELECT count(1)                                                                                                               ";
            strSql += "FROM officedba.PurchaseApplyDetailSource                                                                                       ";
            strSql += "WHERE FromType = '1' AND FromBillID = s.ID)+                                                                                   ";
            strSql += "(SELECT count(1)                                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD)                                                      ";
            strSql += "+(SELECT count(1)                                                                                                              ";
            strSql += "FROM officedba.MasterProductScheduleDetail                                                                                     ";
            strSql += "WHERE FromBillID = s.ID and CompanyCD=s.CompanyCD)                                                                             ";
            strSql += ")) WHEN 0 THEN '无引用' END, '被引用') AS RefText,                                                                             ";
            strSql += "case when (SELECT    isnull( sum(SendCount),0) as SendCount                                                                    ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD) =0 then '未发货'                                                          ";
            strSql += "when (SELECT isnull( sum(SendCount),0) as SendCount                                                                            ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD) >0  and                                                                   ";
            strSql += "(SELECT isnull( sum(SendCount),0) as SendCount                                                                                 ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)<                                                                          ";
            strSql += "(SELECT isnull( sum(ProductCount),0) as SendCount                                                                              ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)                                                                           ";
            strSql += "then '部分发货'                                                                                                                ";
            strSql += "when (SELECT isnull( sum(SendCount),0) as SendCount                                                                            ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD) <>0  and                                                                  ";
            strSql += "(SELECT isnull( sum(SendCount),0) as SendCount                                                                                 ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)=                                                                          ";
            strSql += "(SELECT isnull( sum(ProductCount),0) as SendCount                                                                              ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)                                                                           ";
            strSql += "then '已发货'                                                                                                                  ";
            strSql += "end as isSendText,isnull(                                                                                                      ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0) as YAccounts,                                  ";
            strSql += " case when (isnull(                                                                                                            ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))>0                                             ";
            strSql += "and (isnull(                                                                                                                   ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0)) <                                             ";
            strSql += "(isnull(                                                                                                                       ";
            strSql += "(SELECT isnull(max(TotalPrice),0)                                                                                              ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))                                               ";
            strSql += " then '部分回款'                                                                                                               ";
            strSql += "when                                                                                                                           ";
            strSql += "(isnull(                                                                                                                       ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0)) =                                             ";
            strSql += "(isnull(                                                                                                                       ";
            strSql += "(SELECT isnull(max(TotalPrice),0)                                                                                              ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0)) and                                           ";
            strSql += "(isnull(                                                                                                                       ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))<>0                                            ";
            strSql += "  then '已回款'                                                                                                                ";
            strSql += "when (isnull(                                                                                                                  ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))=0 then '未回款' end as IsAccText ,            ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                                       ";
            strSql += "FROM officedba.FlowInstance                                                                                                    ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 3                                                                  ";
            strSql += "ORDER BY ModifiedDate DESC) AS FlowStatus                                                                                      ";
            strSql += ",case s.isOpenbill when '0' then '未建单' when '1' then '已建单' end as isOpenbillText                                         ";
            strSql += ", CASE s.FromType WHEN 0 THEN '' WHEN 1 THEN (select isnull(OfferNo,'') as                                                     ";
            strSql += " OfferNo from  officedba.SellOffer where ID=s.FromBillID)                                                                      ";
            strSql += "WHEN 2 THEN (select isnull(ContractNo,'') as  ContractNo  from  officedba.SellContract                                         ";
            strSql += "where ID=s.FromBillID)                                                                                      ";
            strSql += "WHEN 3 THEN (select isnull(ChanceNo,'') as  ChanceNo  from  officedba.SellChance                                         ";
            strSql += "where ID=s.FromBillID) END AS FromBillNo                                                                                       ";
            strSql += "FROM officedba.SellOrder AS s LEFT JOIN                                                                                        ";
            strSql += "officedba.CustInfo AS c ON s.CustID = c.ID LEFT JOIN                                                                           ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID where 1=1   and s.CompanyCD=@CompanyCD                                        ";

            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (State != "")
            {
                strSql += " and s.Status=";
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
                strSql += "and s.OrderDate>=Convert(datetime,'";
                strSql += BeginDate;
                strSql += "')";

            }
            if (EndDate != "")
            {
                strSql += "and s.OrderDate< DATEADD(day,1,Convert(datetime,'";
                strSql += EndDate;
                strSql += "'))";
            }
            if (DateValue != "")
            {

                if (DateType == "1")
                {
                    strSql += "and (dateName(year,s.OrderDate)+'年')='";
                    strSql += DateValue;
                    strSql += "'";
                }
                else if (DateType == "2")
                {
                    strSql += "and (dateName(year,s.OrderDate)+'年-'+dateName(month,s.OrderDate)+'月')='";
                    strSql += DateValue;
                    strSql += "'";
                }
                else
                {
                    strSql += "and (dateName(year,s.OrderDate)+'年-'+dateName(week,s.OrderDate)+'周')='";
                    strSql += DateValue;
                    strSql += "'";
                }
            }

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 图表明细列表 
        /// </summary>
        public static DataTable GetSellOrderDetail(string GroupType, string DeptOrEmployeeId, string DateType, string DateValue, string State, string BusiType, string BeginDate, string EndDate, string AreaId)
        {
            string strSql = string.Empty;
            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            strSql += "SELECT s.ID, s.OrderNo,s.Seller,s.SellDeptId,s.Status,s.BusiType,c.AreaId, s.Title,s.ModifiedDate, CONVERT(varchar(100), s.OrderDate, 23) AS OrderDate,                                          ";
            strSql += "dateName(year,s.OrderDate)+'年' as OrderYear,";
            strSql += "dateName(year,s.OrderDate)+'年-'+dateName(month,s.OrderDate)+'月' as OrderMonth,";
            strSql += "dateName(year,s.OrderDate)+'年-'+dateName(week,s.OrderDate)+'周' as OrderWeek,";
            strSql += "  Convert(decimal(22," + jingdu + "), isnull(s.RealTotal,0)*isnull(s.Rate,1)) RealTotal, isnull(c.CustName,'') as CustName , isnull(e.EmployeeName,'') as EmployeeName,                                                                                      ";
            strSql += "CASE s.FromType WHEN 0 THEN '无来源' WHEN 1 THEN '销售报价单' WHEN 2 THEN '销售合同'  WHEN 3 THEN '销售机会'  END AS FromTypeText,                      ";
            strSql += "CASE s.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更'                                                     ";
            strSql += "WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText,                                                           ";
            strSql += "CASE (SELECT TOP 1 FlowStatus                                                                                             ";
            strSql += "FROM officedba.FlowInstance                                                                                                    ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 3                                                                  ";
            strSql += "ORDER BY ModifiedDate DESC) when NULL THEN '' when 1 THEN '待审批' WHEN  2 THEN '审批中' WHEN   3 THEN '审批通过' WHEN  4 THEN '审批不通过' WHEN  5 THEN '撤销审批' END AS FlowInstanceText, isnull(CASE ";
            strSql += "(((SELECT count(1)                                                                                                             ";
            strSql += "FROM officedba.SellSend AS soo                                                                                                 ";
            strSql += "WHERE soo.FromType = '1' AND soo.FromBillID = s.ID)+                                                                           ";
            strSql += "(SELECT count(1)                                                                                                               ";
            strSql += "FROM officedba.SellSendDetail                                                                                                  ";
            strSql += "WHERE FromType = '1' AND FromBillID = s.ID)+                                                                                   ";
            strSql += "(SELECT count(1)                                                                                                               ";
            strSql += "FROM officedba.PurchaseApplyDetailSource                                                                                       ";
            strSql += "WHERE FromType = '1' AND FromBillID = s.ID)+                                                                                   ";
            strSql += "(SELECT count(1)                                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD)                                                      ";
            strSql += "+(SELECT count(1)                                                                                                              ";
            strSql += "FROM officedba.MasterProductScheduleDetail                                                                                     ";
            strSql += "WHERE FromBillID = s.ID and CompanyCD=s.CompanyCD)                                                                             ";
            strSql += ")) WHEN 0 THEN '无引用' END, '被引用') AS RefText,                                                                             ";
            strSql += "case when (SELECT    isnull( sum(SendCount),0) as SendCount                                                                    ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD) =0 then '未发货'                                                          ";
            strSql += "when (SELECT isnull( sum(SendCount),0) as SendCount                                                                            ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD) >0  and                                                                   ";
            strSql += "(SELECT      isnull( sum(SendCount),0) as SendCount                                                                                 ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)<                                                                          ";
            strSql += "(SELECT isnull( sum(ProductCount),0) as SendCount                                                                              ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)                                                                           ";
            strSql += "then '部分发货'                                                                                                                ";
            strSql += "when (SELECT isnull( sum(SendCount),0) as SendCount                                                                            ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD) <>0  and                                                                  ";
            strSql += "(SELECT isnull( sum(SendCount),0) as SendCount                                                                                 ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)=                                                                          ";
            strSql += "(SELECT isnull( sum(ProductCount),0) as SendCount                                                                              ";
            strSql += "FROM officedba.SellOrderDetail                                                                                                 ";
            strSql += "WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)                                                                           ";
            strSql += "then '已发货'                                                                                                                  ";
            strSql += "end as isSendText,                                                                                                     ";
            strSql += " Convert(decimal(20,"+jingdu+"),   isnull( (SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0)) as YAccounts,                                  ";
            strSql += " case when (isnull(                                                                                                            ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))>0                                             ";
            strSql += "and (isnull(                                                                                                                   ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0)) <                                             ";
            strSql += "(isnull(                                                                                                                       ";
            strSql += "(SELECT isnull(max(TotalPrice),0)                                                                                              ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))                                               ";
            strSql += " then '部分回款'                                                                                                               ";
            strSql += "when                                                                                                                           ";
            strSql += "(isnull(                                                                                                                       ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0)) =                                             ";
            strSql += "(isnull(                                                                                                                       ";
            strSql += "(SELECT isnull(max(TotalPrice),0)                                                                                              ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0)) and                                           ";
            strSql += "(isnull(                                                                                                                       ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))<>0                                            ";
            strSql += "  then '已回款'                                                                                                                ";
            strSql += "when (isnull(                                                                                                                  ";
            strSql += "(SELECT isnull(sum(YAccounts),0)                                                                                               ";
            strSql += "FROM officedba.BlendingDetails                                                                                                         ";
            strSql += "WHERE    BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD),0))=0 then '未回款' end as IsAccText ,            ";
            strSql += "(SELECT TOP 1 FlowStatus                                                                                                       ";
            strSql += "FROM officedba.FlowInstance                                                                                                    ";
            strSql += "WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 3                                                                  ";
            strSql += "ORDER BY ModifiedDate DESC) AS FlowStatus                                                                                      ";
            strSql += ",case s.isOpenbill when '0' then '未建单' when '1' then '已建单' end as isOpenbillText                                         ";
            strSql += ", CASE s.FromType WHEN 0 THEN '' WHEN 1 THEN (select isnull(OfferNo,'') as                                                     ";
            strSql += " OfferNo from  officedba.SellOffer where ID=s.FromBillID)                                                                      ";
            strSql += "WHEN 2 THEN (select isnull(ContractNo,'') as  ContractNo  from  officedba.SellContract                                         ";
            strSql += "where ID=s.FromBillID)                                                                                      ";
            strSql += "WHEN 3 THEN (select isnull(ChanceNo,'') as  ChanceNo  from  officedba.SellChance                                         ";
            strSql += "where ID=s.FromBillID) END AS FromBillNo                                                                                       ";
            strSql += "FROM officedba.SellOrder AS s LEFT JOIN                                                                                        ";
            strSql += "officedba.CustInfo AS c ON s.CustID = c.ID LEFT JOIN                                                                           ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID where 1=1   and s.CompanyCD=@CompanyCD                                        ";

            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (State != "")
            {
                strSql += " and s.Status=";
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
                strSql += "and s.OrderDate>=Convert(datetime,'";
                strSql += BeginDate;
                strSql += "')";

            }
            if (EndDate != "")
            {
                strSql += "and s.OrderDate< DATEADD(day, 1,Convert(datetime,'";
                strSql += EndDate;
                strSql += "'))";
            }
            if (DateValue != "")
            {
                if (DateType == "1")
                {
                    strSql += "and (dateName(year,s.OrderDate)+'年')='";
                    strSql += DateValue;
                    strSql += "'";
                }
                else if (DateType == "2")
                {
                    strSql += "and (dateName(year,s.OrderDate)+'年-'+dateName(month,s.OrderDate)+'月')='";
                    strSql += DateValue;
                    strSql += "'";
                }
                else
                {
                    strSql += "and (dateName(year,s.OrderDate)+'年-'+dateName(week,s.OrderDate)+'周')='";
                    strSql += DateValue;
                    strSql += "'";
                }
            }
            return SqlHelper.ExecuteSql(strSql.ToString(), arr);
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
                    strSq = "update  officedba.SellOrder set BillStatus='2'  ";

                    strSq += " , Confirmor=@EmployeeID, ConfirmDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                    paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                    paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                    paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    paras[3] = new SqlParameter("@OrderNo", OrderNO);

                    strSq += " WHERE OrderNo = @OrderNo and CompanyCD=@CompanyCD";


                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);
                    UpdateSellOrder(1, OrderNO);
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
        /// 终止订单
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
                int iEmployeeID = 0;//员工id
                string strUserID = string.Empty;//用户id
                string strCompanyCD = string.Empty;//单位编码
                int OrderId = GetOrderID(OrderNO);


                iEmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                strUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    SqlParameter[] paras = new SqlParameter[4];
                    strSq = "update  officedba.SellOrder set BillStatus='4' ,Status='3'  ";

                    strSq += " , Closer=@EmployeeID, CloseDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                    paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                    paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                    paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    paras[3] = new SqlParameter("@OrderNo", OrderNO);

                    strSq += " WHERE OrderNo = @OrderNo and CompanyCD=@CompanyCD";

                    FlowDBHelper.OperateCancelConfirm(strCompanyCD, 5, 3, OrderId, strUserID, tran);//撤销审批
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);

                    tran.Commit();
                    isSuc = true;
                    strMsg = "终止订单成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    isSuc = false;
                    strMsg = "终止订单失败，请联系系统管理员！";
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
                    strSq = "update  officedba.SellOrder set BillStatus='4' ,Status='2'  ";

                    strSq += " , Closer=@EmployeeID, CloseDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                    paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                    paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                    paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    paras[3] = new SqlParameter("@OrderNo", OrderNO);

                    strSq += " WHERE OrderNo = @OrderNo and CompanyCD=@CompanyCD";

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
                    strSq = "update  officedba.SellOrder set BillStatus='2'  ,Status='1' ";

                    strSq += " , Closer=@EmployeeID, CloseDate=@CloseDate, ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                    paras[0] = new SqlParameter("@EmployeeID", DBNull.Value);
                    paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                    paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    paras[3] = new SqlParameter("@OrderNo", OrderNO);
                    paras[4] = new SqlParameter("@CloseDate", DBNull.Value);

                    strSq += " WHERE OrderNo = @OrderNo and CompanyCD=@CompanyCD";

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



                    strSq = "update  officedba.SellOrder set BillStatus='1'   ";
                    strSq += " , Confirmor=@Confirmor, ConfirmDate=@ConfirmDate, ModifiedDate=getdate() ,ModifiedUserID=@UserID ";
                    strSq += " WHERE OrderNo = @OrderNo and CompanyCD=@CompanyCD";

                    paras[0] = new SqlParameter("@Confirmor", DBNull.Value);
                    paras[1] = new SqlParameter("@UserID", strUserID);
                    paras[2] = new SqlParameter("@CompanyCD", strCompanyCD);
                    paras[3] = new SqlParameter("@OrderNo", OrderNO);
                    paras[4] = new SqlParameter("@ConfirmDate", DBNull.Value);

                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {

                        FlowDBHelper.OperateCancelConfirm(strCompanyCD, 5, 3, OrderId, strUserID, tran);//撤销审批
                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);
                        UpdateSellOrder(2, OrderNO);
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
        /// 更新对应的物品在分仓存量表
        /// </summary>
        /// <param name="flag">flag=1表示确认单据，等于2表示取消确认</param>
        /// <param name="SendNo"></param>
        private static void UpdateSellOrder(int flag, string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            string strSql = " SELECT ProductID,ProductCount FROM officedba.SellOrderDetail ";
            strSql += " WHERE(OrderNo = @OrderNo) AND (CompanyCD = @CompanyCD) ";

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            SqlParameter[] paras = { new SqlParameter("@OrderNo", OrderNo), new SqlParameter("@CompanyCD", strCompanyCD) };
            DataTable dt = SqlHelper.ExecuteSql(strSql, paras);
            strSql = string.Empty;
            SqlParameter[] param = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (flag)
                {
                    case 1://确认
                        if (Exists(dt.Rows[i]["ProductID"].ToString(), strCompanyCD))
                        {
                            strSql = "UPDATE officedba.StorageProduct SET OrderCount = (isnull(OrderCount,0) + ";
                            strSql += dt.Rows[i]["ProductCount"].ToString();
                            strSql += ") where StorageID =( SELECT StorageID  FROM officedba.ProductInfo where  ID =@ProductID ) and ProductID=@ProductID and CompanyCD=@CompanyCD   ";
                            param = new SqlParameter[] { new SqlParameter("@ProductID", dt.Rows[i]["ProductID"]), new SqlParameter("@CompanyCD", strCompanyCD) };
                            SqlHelper.ExecuteSql(strSql, param);
                        }
                        else
                        {
                            InsertStorageProduct(dt.Rows[i]["ProductID"].ToString(), dt.Rows[i]["ProductCount"].ToString(), strCompanyCD);
                        }
                        break;
                    case 2://取消确认
                        strSql = "UPDATE officedba.StorageProduct SET OrderCount = (isnull(OrderCount,0) - ";
                        strSql += dt.Rows[i]["ProductCount"].ToString();
                        strSql += ") where StorageID =( SELECT StorageID  FROM officedba.ProductInfo where  ID =@ProductID ) and ProductID=@ProductID and CompanyCD=@CompanyCD   ";
                        param = new SqlParameter[] { new SqlParameter("@ProductID", dt.Rows[i]["ProductID"]), new SqlParameter("@CompanyCD", strCompanyCD) };
                        SqlHelper.ExecuteSql(strSql, param);
                        break;
                    default:
                        break;
                }



            }
        }

        /// <summary>
        /// 判断物品记录在分仓存量表中是否存在，不存在就插入新数据
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        private static bool Exists(string ProductID, string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from officedba.StorageProduct");
            strSql.Append(" where storageID=( SELECT StorageID  FROM officedba.ProductInfo where  ID =@ProductID ) and ProductID=@ProductID and CompanyCD=@CompanyCD ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductID", ProductID),
                    new SqlParameter("@CompanyCD",CompanyCD)};
            return SqlHelper.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 分仓存量表插入数据
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="ProductNum"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        private static void InsertStorageProduct(string ProductID, string ProductNum, string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            string strSql2 = string.Empty;
            string strStorageID = string.Empty;
            strSql2 = "SELECT isnull(StorageID,0) as StorageID  FROM officedba.ProductInfo where  ID =@ProductID ";
            SqlParameter[] para = { new SqlParameter("@ProductID", ProductID) };
            strStorageID = SqlHelper.ExecuteScalar(strSql2, para).ToString();
            strSql.Append("insert into officedba.StorageProduct(");
            strSql.Append("CompanyCD,StorageID,ProductID,OrderCount)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@StorageID,@ProductID,@OrderCount)");

            SqlParameter[] parameters = {
                    new SqlParameter("@ProductID", ProductID),
                    new SqlParameter("@CompanyCD",CompanyCD),
                      new SqlParameter("@OrderCount", ProductNum),new SqlParameter("@StorageID", strStorageID)                    };
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

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

            strSql = "select count(1) from officedba.SellOrder where OrderNo = @OrderNo and CompanyCD=@CompanyCD and BillStatus=@BillStatus ";
            ArrayList arr = new ArrayList();
            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@OrderNo", OrderNO);
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
                                       new SqlParameter("@OrderNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.SellOrder ";
            strSql += " WHERE  (OrderNo = @OrderNo) AND (CompanyCD = @CompanyCD) ";
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
                                       new SqlParameter("@OrderNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += "select  ((SELECT count(1)                                                 ";
            strSql += "FROM officedba.SellSend AS soo                                            ";
            strSql += "WHERE soo.FromType = '1' AND soo.FromBillID = s.ID)+                      ";
            strSql += "(SELECT count(1)                                                          ";
            strSql += "FROM officedba.SellSendDetail                                             ";
            strSql += "WHERE FromType = '1' AND FromBillID = s.ID)+                              ";
            strSql += "(SELECT count(1)                                                          ";
            strSql += "FROM officedba.PurchaseApplyDetailSource                                  ";
            strSql += "WHERE FromType = '1' AND FromBillID = s.ID)+                              ";
            strSql += "(SELECT count(1)                                                          ";
            strSql += "FROM officedba.BlendingDetails                                                    ";
            strSql += "WHERE BillingType = '1' AND BillCD = s.OrderNo and CompanyCD=s.CompanyCD) ";
            strSql += "+(SELECT count(1)                                                         ";
            strSql += "FROM officedba.MasterProductScheduleDetail                                ";
            strSql += "WHERE FromBillID = s.ID and CompanyCD=s.CompanyCD)                        ";
            strSql += ") as tt                                                                   ";
            strSql += "FROM officedba.SellOrder AS s                                             ";
            strSql += "WHERE (s.OrderNo = @OrderNo) AND (s.CompanyCD = @CompanyCD)               ";

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
                                       new SqlParameter("@OrderNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@FlowStatus",'5')
                                   };
            strSql += " SELECT COUNT(1) FROM officedba.FlowInstance AS f LEFT OUTER JOIN officedba.SellOrder AS s ON f.BillID = s.ID AND f.BillTypeFlag = 5 AND f.BillTypeCode = 3 and f.CompanyCD=s.CompanyCD";
            strSql += "  WHERE (s.OrderNo = @OrderNo) AND (s.CompanyCD = @CompanyCD) and f.flowstatus!=@FlowStatus  ";
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
                                       new SqlParameter("@OrderNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select TOP  1  FlowStatus  from officedba.FlowInstance ";
            strSql += " WHERE (BillID = (SELECT ID FROM officedba.SellOrder WHERE (OrderNo = @OrderNo) AND (CompanyCD = @CompanyCD))) AND BillTypeFlag = 5 AND BillTypeCode = 3  ";
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

        #region 打印
        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = "select * from  officedba.sellmodule_report_SellOrder WHERE (OrderNo = @OrderNo) AND (CompanyCD = @CompanyCD)";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@OrderNo",OrderNo)
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

            string strSql = "select  * from  officedba.sellmodule_report_SellOrderDetail WHERE (OrderNo = @OrderNo) AND (CompanyCD = @CompanyCD) order by SortNo asc";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@OrderNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderFee(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = "select  * from  officedba.sellmodule_report_SellOrderFeeDetail WHERE (OrderNo = @OrderNo) AND (CompanyCD = @CompanyCD) order by FeeID asc";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@OrderNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }

        #endregion

    }
}

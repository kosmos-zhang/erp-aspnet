/***********************************************
 * 类作用：   采购管理事务层处理               *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/04/21                       *
 * 修改人：   王保军                          *
 * 修改时间： 2009/08/27                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using System.Data.SqlClient;
using XBase.Model.Office.PurchaseManager;
using XBase.Common;
using System.Data.SqlTypes;
using System.Collections;
using XBase.Data.Common;

namespace XBase.Data.Office.PurchaseManager
{
    /// <summary>
    /// 类名：PurchaseRejectDBHelper
    /// 描述：采购退货数据库层处理
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/21
    /// 最后修改时间：2009/04/23
    /// </summary>
    ///
    public class PurchaseRejectDBHelper
    {
        #region 绑定采购类别
        public static DataTable GetddlTypeID()
        {
            string sql = "select ID,TypeName from officedba.codepublictype where typeflag =7 and typecode =5 and usedstatus=1  AND CompanyCD='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' ";
            DataTable data = SqlHelper.ExecuteSql(sql);
            return data;
        }
        #endregion

        #region 绑定采购交货方式
        public static DataTable GetDrpTakeType()
        {
            string sql = "select ID,TypeName from officedba.codepublictype where typeflag =6 and typecode =7 and usedstatus=1  AND CompanyCD='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' ";
            DataTable data = SqlHelper.ExecuteSql(sql);
            return data;
        }
        #endregion

        #region 绑定采购运送方式
        public static DataTable GetDrpCarryType()
        {
            string sql = "select ID,TypeName from officedba.codepublictype where typeflag =6 and typecode =8 and usedstatus=1  AND CompanyCD='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' ";
            DataTable data = SqlHelper.ExecuteSql(sql);
            return data;
        }
        #endregion

        #region 绑定采购结算方式
        public static DataTable GetDrpPayType()
        {
            string sql = "select ID,TypeName from officedba.codepublictype where typeflag =4 and typecode =11 and usedstatus=1  AND CompanyCD='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' ";
            DataTable data = SqlHelper.ExecuteSql(sql);
            return data;
        }
        #endregion

        #region 绑定采购支付方式
        public static DataTable GetDrpMoneyType()
        {
            string sql = "select ID,TypeName from officedba.codepublictype where typeflag =4 and typecode =14 and usedstatus=1  AND CompanyCD='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' ";
            DataTable data = SqlHelper.ExecuteSql(sql);
            return data;
        }
        #endregion

        #region 绑定原因
        public static DataTable GetDrpApplyReason()
        {
            string sql = "select ID,CodeName from officedba.CodeReasonType where CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'AND Flag='" + ConstUtil.PurchaseReject_ApplyReason_Flag + "' AND UsedStatus = '1'  AND CompanyCD='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'  ";
            DataTable data = SqlHelper.ExecuteSql(sql);
            return data;
        }
        #endregion


        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(PurchaseRejectModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.PurchaseReject set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND RejectNo = @RejectNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@RejectNo", model.RejectNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion
        #region 插入退货单
        public static bool InsertPurchaseReject(PurchaseRejectModel model, string DetailProductID
            , string DetailProductNo, string DetailProductName, string DetailUnitID
            , string DetailProductCount, string DetailBackCount, string DetailApplyReason
            , string DetailUnitPrice, string DetailTaxPrice, string DetailDiscount
            , string DetailTaxRate, string DetailTotalPrice, string DetailTotalFee
            , string DetailTotalTax, string DetailRemark, string DetailFromBillID
            , string DetailFromLineNo, string DetailUsedUnitCount, string DetailUsedUnitID
            , string DetailUsedPrice, string length, out string ID, Hashtable htExtAttr)
        {

            ArrayList listADD = new ArrayList();
            bool result = false;
            ID = "0";

            #region  采购退货单添加SQL语句
            StringBuilder sqlArrive = new StringBuilder();


            sqlArrive.AppendLine("INSERT INTO officedba.PurchaseReject");
            sqlArrive.AppendLine("(CompanyCD,RejectNo,Title,FromType,RejectDate,Purchaser,DeptID,CurrencyType,Rate,PayType,");
            sqlArrive.AppendLine("MoneyType,SendAddress,ReceiveOverAddress,ReceiveMan,ReceiveTel,remark,TotalPrice,");
            sqlArrive.AppendLine("TotalTax,TotalFee,Discount,DiscountTotal,RealTotal,isAddTax,CountTotal,BillStatus,");
            sqlArrive.AppendLine("Creator,CreateDate,Confirmor,ConfirmDate,Closer,CloseDate,ModifiedDate,");
            sqlArrive.AppendLine("ModifiedUserID,TypeID,ProviderID,TakeType,CarryType,TotalDyfzk,TotalYthkhj,ProjectID)");
            sqlArrive.AppendLine("VALUES (@CompanyCD,@RejectNo,@Title,@FromType,@RejectDate,@Purchaser,@DeptID,@CurrencyType,@Rate,@PayType,");
            sqlArrive.AppendLine("@MoneyType,@SendAddress,@ReceiveOverAddress,@ReceiveMan,@ReceiveTel,@remark,@TotalPrice,");
            sqlArrive.AppendLine("@TotalTax,@TotalFee,@Discount,@DiscountTotal,@RealTotal,@isAddTax,@CountTotal,@BillStatus,");
            sqlArrive.AppendLine("@Creator,@CreateDate,@Confirmor,@ConfirmDate,@Closer,@CloseDate,getdate(),");
            sqlArrive.AppendLine("@ModifiedUserID,@TypeID,@ProviderID,@TakeType,@CarryType,@TotalDyfzk,@TotalYthkhj,@ProjectID)");
            sqlArrive.AppendLine("set @ID=@@IDENTITY");

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@RejectNo", model.RejectNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            comm.Parameters.Add(SqlHelper.GetParameter("@RejectDate", model.RejectDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.RejectDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Purchaser", model.Purchaser));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", model.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@Rate", model.Rate));
            comm.Parameters.Add(SqlHelper.GetParameter("@PayType", model.PayType));
            comm.Parameters.Add(SqlHelper.GetParameter("@MoneyType", model.MoneyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@SendAddress", model.SendAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReceiveOverAddress", model.ReceiveOverAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReceiveMan", model.ReceiveMan));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReceiveTel", model.ReceiveTel));
            comm.Parameters.Add(SqlHelper.GetParameter("@remark", model.remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", model.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalTax", model.TotalTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalFee", model.TotalFee));
            comm.Parameters.Add(SqlHelper.GetParameter("@Discount", model.Discount));
            comm.Parameters.Add(SqlHelper.GetParameter("@DiscountTotal", model.DiscountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@RealTotal", model.RealTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@isAddTax", model.isAddTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.CreateDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.ConfirmDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Closer", model.Closer));
            comm.Parameters.Add(SqlHelper.GetParameter("@CloseDate", model.CloseDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.CloseDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@TypeID", model.TypeID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProviderID", model.ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameter("@TakeType", model.TakeType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CarryType", model.CarryType));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalDyfzk", model.TotalDyfzk));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalYthkhj", model.TotalYthkhj));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProjectID", model.ProjectID.HasValue
                                                        ? SqlInt32.Parse(model.ProjectID.Value.ToString())
                                                        : SqlInt32.Null));
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
            comm.CommandText = sqlArrive.ToString();


            listADD.Add(comm);
            #endregion

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                listADD.Add(cmd);
            #endregion

            try
            {
                #region 采购退货单明细
                int lengs = Convert.ToInt32(length);
                if (lengs > 0)
                {
                    string[] ProductID = DetailProductID.Split(',');
                    string[] ProductNo = DetailProductNo.Split(',');
                    string[] ProductName = DetailProductName.Split(',');
                    string[] UnitID = DetailUnitID.Split(',');
                    string[] ProductCount = DetailProductCount.Split(',');
                    string[] BackCount = DetailBackCount.Split(',');
                    string[] ApplyReason = DetailApplyReason.Split(',');
                    string[] UnitPrice = DetailUnitPrice.Split(',');//单价
                    string[] TaxPrice = DetailTaxPrice.Split(',');//含税价
                    string[] TaxRate = DetailTaxRate.Split(',');//税率
                    string[] TotalPrice = DetailTotalPrice.Split(',');//金额
                    string[] TotalFee = DetailTotalFee.Split(',');//含税金额
                    string[] TotalTax = DetailTotalTax.Split(',');//税额
                    string[] Remark = DetailRemark.Split(',');
                    string[] FromBillID = DetailFromBillID.Split(',');
                    string[] FromLineNo = DetailFromLineNo.Split(',');
                    string[] UsedUnitCount = DetailUsedUnitCount.Split(',');
                    string[] UsedUnitID = DetailUsedUnitID.Split(',');
                    string[] UsedPrice = DetailUsedPrice.Split(',');
                    for (int i = 0; i < lengs; i++)
                    {
                        System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                        SqlCommand comms = new SqlCommand();
                        cmdsql.AppendLine("INSERT INTO officedba.PurchaseRejectDetail");
                        cmdsql.AppendLine("(CompanyCD,");
                        cmdsql.AppendLine("RejectNo,");
                        cmdsql.AppendLine("FromType,");
                        cmdsql.AppendLine("SortNo,");
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("FromBillID,");
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("FromLineNo,");
                            }
                        }
                        cmdsql.AppendLine("ProductID,");
                        cmdsql.AppendLine("ProductNo,");
                        cmdsql.AppendLine("ProductName,");
                        if (ProductCount[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(ProductCount[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("ProductCount,");
                            }
                        }
                        cmdsql.AppendLine("BackCount,");
                        cmdsql.AppendLine("UnitID,");
                        cmdsql.AppendLine("TaxPrice,");
                        cmdsql.AppendLine("TaxRate,");
                        cmdsql.AppendLine("TotalFee,");
                        cmdsql.AppendLine("TotalTax,");
                        cmdsql.AppendLine("UnitPrice,");
                        cmdsql.AppendLine("TotalPrice,");
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("Remark,");
                            }
                        }
                        cmdsql.AppendLine("ApplyReason,UsedUnitCount,UsedUnitID,ExRate,UsedPrice)");
                        cmdsql.AppendLine(" Values(@CompanyCD");
                        cmdsql.AppendLine("            ,@RejectNo");
                        cmdsql.AppendLine("            ,@FromType");
                        cmdsql.AppendLine("            ,@SortNo");
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@FromBillID");
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@FromLineNo");
                            }
                        }
                        cmdsql.AppendLine("            ,@ProductID");
                        cmdsql.AppendLine("            ,@ProductNo");
                        cmdsql.AppendLine("            ,@ProductName");
                        if (ProductCount[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(ProductCount[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@ProductCount");
                            }
                        }
                        cmdsql.AppendLine("            ,@BackCount");
                        cmdsql.AppendLine("            ,@UnitID");
                        cmdsql.AppendLine("            ,@TaxPrice");
                        cmdsql.AppendLine("            ,@TaxRate");
                        cmdsql.AppendLine("            ,@TotalFee");
                        cmdsql.AppendLine("            ,@TotalTax");
                        cmdsql.AppendLine("            ,@UnitPrice");
                        cmdsql.AppendLine("            ,@TotalPrice");
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@Remark2");
                            }
                        }
                        cmdsql.AppendLine("            ,@ApplyReason,@UsedUnitCount,@UsedUnitID,@ExRate,@UsedPrice)");


                        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        comms.Parameters.Add(SqlHelper.GetParameter("@RejectNo", model.RejectNo));
                        comms.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
                        comms.Parameters.Add(SqlHelper.GetParameter("@SortNo", i + 1));
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@FromBillID", FromBillID[i].ToString()));
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", FromLineNo[i].ToString()));
                            }
                        }
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductNo", ProductNo[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductName", ProductName[i].ToString()));
                        if (ProductCount[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(ProductCount[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@ProductCount", ProductCount[i].ToString()));
                            }
                        }
                        comms.Parameters.Add(SqlHelper.GetParameter("@BackCount", BackCount[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UnitID", UnitID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxPrice", TaxPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxRate", TaxRate[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalFee", TotalFee[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalTax", TotalTax[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UnitPrice", UnitPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", TotalPrice[i].ToString()));
                        if (UsedUnitCount.Length == lengs && !string.IsNullOrEmpty(UsedUnitCount[i]))
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", UsedUnitCount[i]));
                        }
                        else
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", DBNull.Value));
                        }
                        if (UsedPrice.Length == lengs && !string.IsNullOrEmpty(UsedPrice[i]))
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", UsedPrice[i]));
                        }
                        else
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", DBNull.Value));
                        }
                        if (UsedUnitID.Length == lengs && !string.IsNullOrEmpty(UsedUnitID[i].Split('|')[0]))
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", UsedUnitID[i].Split('|')[0]));
                        }
                        else
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", DBNull.Value));
                        }
                        if (UsedUnitID.Length == lengs && UsedUnitID[i].Split('|').Length == 2 && !string.IsNullOrEmpty(UsedUnitID[i].Split('|')[1]))
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@ExRate", UsedUnitID[i].Split('|')[1]));
                        }
                        else
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@ExRate", DBNull.Value));
                        }
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@Remark2", Remark[i].ToString()));
                            }
                        }
                        comms.Parameters.Add(SqlHelper.GetParameter("@ApplyReason", ApplyReason[i].ToString()));
                        comms.CommandText = cmdsql.ToString();
                        listADD.Add(comms);
                    }
                }
                #endregion


                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    ID = comm.Parameters["@ID"].Value.ToString();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region 修改退货单
        public static bool UpdatePurchaseReject(PurchaseRejectModel model, string DetailProductID
            , string DetailProductNo, string DetailProductName, string DetailUnitID
            , string DetailProductCount, string DetailBackCount, string DetailApplyReason
            , string DetailUnitPrice, string DetailTaxPrice, string DetailDiscount
            , string DetailTaxRate, string DetailTotalPrice, string DetailTotalFee
            , string DetailTotalTax, string DetailRemark, string DetailFromBillID
            , string DetailFromBillNo, string DetailFromLineNo, string DetailUsedUnitCount, string DetailUsedUnitID
            , string DetailUsedPrice, string length, string fflag2, string no, Hashtable htExtAttr)
        {
            if (model.ID <= 0)
            {
                return false;
            }
            ArrayList listADD = new ArrayList();
            bool result = false;

            #region  修改采购退货单
            StringBuilder sqlArrive = new StringBuilder();

            sqlArrive.AppendLine("Update  Officedba.PurchaseReject set CompanyCD=@CompanyCD,");
            sqlArrive.AppendLine("Title=@Title,FromType=@FromType,RejectDate=@RejectDate,Purchaser=@Purchaser,DeptID=@DeptID,");
            sqlArrive.AppendLine("CurrencyType=@CurrencyType,Rate=@Rate,PayType=@PayType,MoneyType=@MoneyType,SendAddress=@SendAddress,");
            sqlArrive.AppendLine("ReceiveOverAddress=@ReceiveOverAddress,ReceiveMan=@ReceiveMan,ReceiveTel=@ReceiveTel,remark=@remark,TotalPrice=@TotalPrice,");
            sqlArrive.AppendLine("TotalTax=@TotalTax,TotalFee=@TotalFee,Discount=@Discount,DiscountTotal=@DiscountTotal,RealTotal=@RealTotal,");
            sqlArrive.AppendLine("isAddTax=@isAddTax,CountTotal=@CountTotal,BillStatus=@BillStatus,Creator=@Creator,CreateDate=@CreateDate,");
            sqlArrive.AppendLine("Confirmor=@Confirmor,ConfirmDate=@ConfirmDate,Closer=@Closer,CloseDate=@CloseDate,");
            sqlArrive.AppendLine("ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID,TypeID=@TypeID,ProviderID=@ProviderID,");
            sqlArrive.AppendLine("TakeType=@TakeType,CarryType=@CarryType,TotalDyfzk=@TotalDyfzk,TotalYthkhj=@TotalYthkhj,ProjectID=@ProjectID where CompanyCD=@CompanyCD and RejectNo=@RejectNo and ID=@ID");


            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            comm.Parameters.Add(SqlHelper.GetParameter("@RejectDate", model.RejectDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.RejectDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Purchaser", model.Purchaser));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", model.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@Rate", model.Rate));
            comm.Parameters.Add(SqlHelper.GetParameter("@PayType", model.PayType));
            comm.Parameters.Add(SqlHelper.GetParameter("@MoneyType", model.MoneyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@SendAddress", model.SendAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReceiveOverAddress", model.ReceiveOverAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReceiveMan", model.ReceiveMan));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReceiveTel", model.ReceiveTel));
            comm.Parameters.Add(SqlHelper.GetParameter("@remark", model.remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", model.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalTax", model.TotalTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalFee", model.TotalFee));
            comm.Parameters.Add(SqlHelper.GetParameter("@Discount", model.Discount));
            comm.Parameters.Add(SqlHelper.GetParameter("@DiscountTotal", model.DiscountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@RealTotal", model.RealTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@isAddTax", model.isAddTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.CreateDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.ConfirmDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Closer", model.Closer));
            comm.Parameters.Add(SqlHelper.GetParameter("@CloseDate", model.CloseDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.CloseDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@TypeID", model.TypeID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProviderID", model.ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameter("@TakeType", model.TakeType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CarryType", model.CarryType));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalDyfzk", model.TotalDyfzk));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalYthkhj", model.TotalYthkhj));
            comm.Parameters.Add(SqlHelper.GetParameter("@RejectNo", model.RejectNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProjectID", model.ProjectID.HasValue
                                                        ? SqlInt32.Parse(model.ProjectID.Value.ToString())
                                                        : SqlInt32.Null));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.CommandText = sqlArrive.ToString();


            listADD.Add(comm);
            #endregion
            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                listADD.Add(cmd);
            #endregion

            #region 保存修改时，判断是否由变更到执行，即确认后再修改的，则对采购到货明细字段回写减操作
            if (fflag2 == "1")
            {
                int lengs = Convert.ToInt32(length);
                if (lengs > 0)
                {
                    string[] BackCount = DetailBackCount.Split(',');
                    string[] FromBillNo = DetailFromBillNo.Split(',');
                    string[] FromLineNo = DetailFromLineNo.Split(',');
                    string[] ProductTempID = DetailProductID.Split(',');
                    for (int i = 0; i < lengs; i++)
                    {
                        System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                        SqlCommand commp = new SqlCommand();
                        cmdsql.AppendLine("Update  Officedba.PurchaseArriveDetail set BackCount=isnull(BackCount,0)-@BackCount");
                        cmdsql.AppendLine(" where CompanyCD=@CompanyCD and ArriveNo=@FromBillNo and SortNo=@FromLineNo");

                        commp.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        commp.Parameters.Add(SqlHelper.GetParameter("@BackCount", BackCount[i].ToString()));
                        commp.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", FromBillNo[i].ToString()));
                        commp.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", FromLineNo[i].ToString()));
                        commp.CommandText = cmdsql.ToString();
                        listADD.Add(commp);
                        if (model.FromType == "1")
                        {
                            if (FromBillNo[i].ToString() != "")
                            {
                                listADD.Add(WriteAddStorge(ProductTempID[i].ToString(), BackCount[i].ToString()));
                            }
                        }
                    }
                }
            }
            #endregion

            #region 删除退货单明细
            System.Text.StringBuilder cmdddetail = new System.Text.StringBuilder();
            cmdddetail.AppendLine("DELETE  FROM officedba.PurchaseRejectDetail WHERE  CompanyCD=@CompanyCD and RejectNo=@RejectNo");
            SqlCommand comn = new SqlCommand();
            comn.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comn.Parameters.Add(SqlHelper.GetParameter("@RejectNo", model.RejectNo));
            comn.CommandText = cmdddetail.ToString();
            listADD.Add(comn);
            #endregion



            try
            {
                #region 重新插入退货单明细
                int lengs = Convert.ToInt32(length);
                if (lengs > 0)
                {
                    string[] ProductID = DetailProductID.Split(',');
                    string[] ProductNo = DetailProductNo.Split(',');
                    string[] ProductName = DetailProductName.Split(',');
                    string[] UnitID = DetailUnitID.Split(',');
                    string[] ProductCount = DetailProductCount.Split(',');
                    string[] BackCount = DetailBackCount.Split(',');
                    string[] ApplyReason = DetailApplyReason.Split(',');
                    string[] UnitPrice = DetailUnitPrice.Split(',');//单价
                    string[] TaxPrice = DetailTaxPrice.Split(',');//含税价
                    string[] TaxRate = DetailTaxRate.Split(',');//税率
                    string[] TotalPrice = DetailTotalPrice.Split(',');//金额
                    string[] TotalFee = DetailTotalFee.Split(',');//含税金额
                    string[] TotalTax = DetailTotalTax.Split(',');//税额
                    string[] Remark = DetailRemark.Split(',');
                    string[] FromBillID = DetailFromBillID.Split(',');
                    string[] FromLineNo = DetailFromLineNo.Split(',');
                    string[] UsedUnitCount = DetailUsedUnitCount.Split(',');
                    string[] UsedUnitID = DetailUsedUnitID.Split(',');
                    string[] UsedPrice = DetailUsedPrice.Split(',');
                    for (int i = 0; i < lengs; i++)
                    {
                        System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                        SqlCommand comms = new SqlCommand();
                        cmdsql.AppendLine("INSERT INTO officedba.PurchaseRejectDetail");
                        cmdsql.AppendLine("(CompanyCD,");
                        cmdsql.AppendLine("RejectNo,");
                        cmdsql.AppendLine("FromType,");
                        cmdsql.AppendLine("SortNo,");
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("FromBillID,");
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("FromLineNo,");
                            }
                        }
                        cmdsql.AppendLine("ProductID,");
                        cmdsql.AppendLine("ProductNo,");
                        cmdsql.AppendLine("ProductName,");
                        if (ProductCount[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(ProductCount[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("ProductCount,");
                            }
                        }
                        cmdsql.AppendLine("BackCount,");
                        cmdsql.AppendLine("UnitID,");
                        cmdsql.AppendLine("TaxPrice,");
                        cmdsql.AppendLine("TaxRate,");
                        cmdsql.AppendLine("TotalFee,");
                        cmdsql.AppendLine("TotalTax,");
                        cmdsql.AppendLine("UnitPrice,");
                        cmdsql.AppendLine("TotalPrice,");
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("Remark,");
                            }
                        }
                        cmdsql.AppendLine("ApplyReason,UsedUnitCount,UsedUnitID,ExRate,UsedPrice)");
                        cmdsql.AppendLine(" Values(@CompanyCD");
                        cmdsql.AppendLine("            ,@RejectNo");
                        cmdsql.AppendLine("            ,@FromType");
                        cmdsql.AppendLine("            ,@SortNo");
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@FromBillID");
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@FromLineNo");
                            }
                        }
                        cmdsql.AppendLine("            ,@ProductID");
                        cmdsql.AppendLine("            ,@ProductNo");
                        cmdsql.AppendLine("            ,@ProductName");
                        if (ProductCount[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(ProductCount[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@ProductCount");
                            }
                        }
                        cmdsql.AppendLine("            ,@BackCount");
                        cmdsql.AppendLine("            ,@UnitID");
                        cmdsql.AppendLine("            ,@TaxPrice");
                        cmdsql.AppendLine("            ,@TaxRate");
                        cmdsql.AppendLine("            ,@TotalFee");
                        cmdsql.AppendLine("            ,@TotalTax");
                        cmdsql.AppendLine("            ,@UnitPrice");
                        cmdsql.AppendLine("            ,@TotalPrice");
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@Remark2");
                            }
                        }
                        cmdsql.AppendLine("            ,@ApplyReason,@UsedUnitCount,@UsedUnitID,@ExRate,@UsedPrice)");


                        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        comms.Parameters.Add(SqlHelper.GetParameter("@RejectNo", model.RejectNo));
                        comms.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
                        comms.Parameters.Add(SqlHelper.GetParameter("@SortNo", i + 1));
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@FromBillID", FromBillID[i].ToString()));
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", FromLineNo[i].ToString()));
                            }
                        }
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductNo", ProductNo[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductName", ProductName[i].ToString()));
                        if (ProductCount[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(ProductCount[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@ProductCount", ProductCount[i].ToString()));
                            }
                        }
                        comms.Parameters.Add(SqlHelper.GetParameter("@BackCount", BackCount[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UnitID", UnitID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxPrice", TaxPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxRate", TaxRate[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalFee", TotalFee[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalTax", TotalTax[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UnitPrice", UnitPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", TotalPrice[i].ToString()));
                        if (UsedUnitCount.Length == lengs && !string.IsNullOrEmpty(UsedUnitCount[i]))
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", UsedUnitCount[i]));
                        }
                        else
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", DBNull.Value));
                        }
                        if (UsedPrice.Length == lengs && !string.IsNullOrEmpty(UsedPrice[i]))
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", UsedPrice[i]));
                        }
                        else
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", DBNull.Value));
                        }
                        if (UsedUnitID.Length == lengs && !string.IsNullOrEmpty(UsedUnitID[i].Split('|')[0]))
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", UsedUnitID[i].Split('|')[0]));
                        }
                        else
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", DBNull.Value));
                        }

                        if (UsedUnitID.Length == lengs && UsedUnitID[i].Split('|').Length == 2 && !string.IsNullOrEmpty(UsedUnitID[i].Split('|')[1]))
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@ExRate", UsedUnitID[i].Split('|')[1]));
                        }
                        else
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@ExRate", DBNull.Value));
                        }
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@Remark2", Remark[i].ToString()));
                            }
                        }
                        comms.Parameters.Add(SqlHelper.GetParameter("@ApplyReason", ApplyReason[i].ToString()));
                        comms.CommandText = cmdsql.ToString();
                        listADD.Add(comms);
                    }
                }
                #endregion


                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region 选择到货来源
        public static DataTable GetPurchaseRejectDetail(string ProductNo, string ProductName, string FromBillNo, string CompanyCD, int ProviderID, int CurrencyTypeID, string Rate, string PurchaseArriveEFIndex, string PurchaseArriveEFDesc)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ID                                                                     ");
            sql.AppendLine("      ,A.ProductID                                                                       ");
            sql.AppendLine("      ,isnull(B.ProdNo,'') AS   ProductNo                                                 ");
            sql.AppendLine("      ,isnull(A.ProductName ,'') as   ProductName                                                                    ");
            sql.AppendLine("      ,isnull(B.Specification,'') AS standard                                             ");
            sql.AppendLine("      ,A.UnitID                                                                           ");
            sql.AppendLine("      ,isnull(C.CodeName,'') AS UnitName                                                             ");
            sql.AppendLine("      ,isnull(A.ProductCount,0) AS ProductCount                                           ");
            sql.AppendLine("      ,isnull(A.InCount,0) AS InCount                                           ");
            sql.AppendLine("      ,isnull(A.UnitPrice,0) AS UnitPrice                          ");
            sql.AppendLine("      ,isnull(A.TaxPrice,0) AS TaxPrice                              ");
            sql.AppendLine("      ,isnull(B.Discount,0) AS Discount                             ");
            sql.AppendLine("      ,isnull(A.TaxRate,0)  AS TaxRate                              ");
            sql.AppendLine("      ,isnull(A.TotalPrice,0) AS TotalPrice                        ");
            sql.AppendLine("      ,isnull(A.TotalFee,0) AS TotalFee                             ");
            sql.AppendLine("      ,isnull(A.TotalTax,0)  AS TotalTax                           ");
            sql.AppendLine("      ,isnull(A.Remark,'') AS Remark  ,D.ID AS FromBillID                                  ");
            sql.AppendLine("      ,D.ArriveNo AS FromBillNo                                                             ");
            sql.AppendLine("      ,A.SortNo AS FromLineNo                                                               ");
            sql.AppendLine("      ,isnull(A.BackCount,0) AS BackCount                                                   ");
            sql.AppendLine("      ,D.ProviderID,isnull(E.CustName,'') AS ProviderName                                   ");
            sql.AppendLine("      ,isnull(D.CurrencyType,0) AS CurrencyType,isnull(F.CurrencyName,'') AS CurrencyTypeName");
            sql.AppendLine("      ,isnull(D.Rate,0) AS Rate                                                             ");
            sql.AppendLine("      ,ISNULL(cpt.TypeName,'') AS ColorName ");
            sql.AppendLine("      ,isnull(A.UsedUnitCount,0) AS UsedUnitCount,A.UsedUnitID,A.UsedPrice,isnull(CU.CodeName,'') AS UsedUnitName ");
            sql.AppendLine("FROM officedba.PurchaseArriveDetail AS A                                                    ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS C ON A.CompanyCD = C.CompanyCD AND A.UnitID = C.ID      ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS CU ON A.CompanyCD = CU.CompanyCD AND A.UsedUnitID = CU.ID      ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProductID = B.ID    ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType cpt ON B.ColorID=cpt.ID   ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseArrive AS D ON A.CompanyCD = D.CompanyCD AND A.ArriveNo = D.ArriveNo ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS E ON D.CompanyCD = E.CompanyCD AND D.ProviderID = E.ID   ");
            sql.AppendLine("LEFT JOIN officedba.CurrencyTypeSetting AS F ON D.CompanyCD = F.CompanyCD AND D.CurrencyType = F.ID ");

            sql.AppendLine("WHERE D.BillStatus = 2 AND D.CompanyCD = '" + CompanyCD + "' ");
            if (ProductNo != null && ProductNo != "")
            {
                sql.AppendLine(" AND A.ProductNo like '%" + ProductNo + "%'    ");
            }
            if (ProductName != null && ProductName != "")
            {
                sql.AppendLine(" AND A.ProductName like '%" + ProductName + "%'    ");
            }
            if (FromBillNo != null && FromBillNo != "")
            {
                sql.AppendLine(" AND D.ArriveNo like '%" + FromBillNo + "%'    ");
            }
            if (ProviderID != 0)
            {
                sql.AppendLine(" AND D.ProviderID =" + ProviderID + " ");
            }
            sql.AppendLine(" AND D.CurrencyType = " + CurrencyTypeID + " ");
            if (Rate != "0")
            {
                sql.AppendLine(" AND D.Rate = " + Rate + "   ");
            }
            if (!string.IsNullOrEmpty(PurchaseArriveEFIndex) && !string.IsNullOrEmpty(PurchaseArriveEFDesc))
            {
                sql.AppendLine("	AND B.ExtField" + PurchaseArriveEFIndex + " LIKE '%" + PurchaseArriveEFDesc + "%' ");
            }
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion
        #region 选择采购入库来源
        public static DataTable GetPurchaseStorageDetail(string ProductNo, string ProductName, string FromBillNo, string CompanyCD, int ProviderID, int CurrencyTypeID, string Rate, string StorageEFIndex, string StorageEFDesc)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ID                                                                     ");
            sql.AppendLine("      ,A.ProductID                                                                       ");
            sql.AppendLine("      ,isnull(B.ProdNo,'') AS   ProductNo                                                 ");
            sql.AppendLine("      ,isnull(B.ProductName,'') AS   ProductName                                                                    ");
            sql.AppendLine("      ,isnull(B.Specification,'') AS standard                                             ");
            sql.AppendLine("      ,H.UnitID                                                                           ");
            sql.AppendLine("      ,C.CodeName AS UnitName                                                             ");
            sql.AppendLine("      ,isnull(A.ProductCount,0) AS ProductCount                                           ");//实收数量
            sql.AppendLine("      , isnull(A.UnitPrice,0)  AS UnitPrice                          ");
            sql.AppendLine("      , isnull(B.StandardBuy,0)  AS TaxPrice                              ");
            sql.AppendLine("      , isnull(B.Discount,0)  AS Discount                             ");//销售折扣
            sql.AppendLine("      , isnull(B.TaxRate,0)   AS TaxRate                              ");//销售税率
            sql.AppendLine("      , isnull(A.TotalPrice,0)   AS TotalPrice                        ");//入库金额合计
            sql.AppendLine("      ,isnull(A.Remark,'') AS Remark  ,D.ID AS FromBillID                                  ");
            sql.AppendLine("      ,D.InNo AS FromBillNo                                                             ");
            sql.AppendLine("      ,A.SortNo AS FromLineNo                                                               ");
            sql.AppendLine("      ,isnull(A.BackCount,0) AS BackCount                                             "); //已退货数量
            sql.AppendLine("      ,G.ProviderID,isnull(E.CustName,'') AS ProviderName                                   ");
            sql.AppendLine("      ,isnull(G.CurrencyType,0) AS CurrencyType,isnull(F.CurrencyName,'') AS CurrencyTypeName ");
            sql.AppendLine("      ,isnull(G.Rate,0) AS Rate                                                             ");
            sql.AppendLine("      ,ISNULL(cpt.TypeName,'') AS ColorName ");
            sql.AppendLine("      ,isnull(A.UsedPrice,0) AS UsedPrice,A.UsedUnitID,A.UsedUnitCount,CU.CodeName AS UsedUnitName ");
            sql.AppendLine("FROM officedba.StorageInPurchaseDetail AS A                                                    ");
            sql.AppendLine("LEFT outer JOIN officedba.StorageInPurchase AS D ON A.CompanyCD = D.CompanyCD AND A.InNo = D.InNo       ");
            sql.AppendLine("LEFT outer  JOIN officedba.PurchaseArrive AS G ON G.CompanyCD = D.CompanyCD AND G.ID =D.FromBillID     ");
            sql.AppendLine("LEFT  outer JOIN officedba.PurchaseArriveDetail  AS H ON H.CompanyCD = G.CompanyCD AND H.ArriveNo=G.ArriveNo and H.SortNo=A.FromLineNo  ");
            sql.AppendLine("LEFT  outer JOIN officedba.ProductInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProductID = B.ID ");
            sql.AppendLine("LEFT OUTER JOIN officedba.CodePublicType cpt ON B.ColorID=cpt.ID   ");
            sql.AppendLine("LEFT  outer JOIN officedba.CodeUnitType AS C ON A.CompanyCD = C.CompanyCD AND C.ID= H.UnitID  ");
            sql.AppendLine("LEFT  outer JOIN officedba.CodeUnitType AS CU ON A.CompanyCD = CU.CompanyCD AND CU.ID= A.UsedUnitID  ");
            sql.AppendLine("LEFT  outer JOIN officedba.ProviderInfo AS E ON D.CompanyCD = E.CompanyCD AND G.ProviderID = E.ID    ");
            sql.AppendLine("LEFT  outer JOIN officedba.CurrencyTypeSetting AS F ON D.CompanyCD = F.CompanyCD AND G.CurrencyType = F.ID  ");


            sql.AppendLine("WHERE D.BillStatus = 2 AND D.CompanyCD = '" + CompanyCD + "' ");
            if (ProductNo != null && ProductNo != "")
            {
                sql.AppendLine(" AND B.ProdNo like '%" + ProductNo + "%'    ");
            }
            if (ProductName != null && ProductName != "")
            {
                sql.AppendLine(" AND B.ProductName like '%" + ProductName + "%'    ");
            }
            if (FromBillNo != null && FromBillNo != "")
            {
                sql.AppendLine(" AND D.InNo  like '%" + FromBillNo + "%'    ");
            }
            if (ProviderID != 0)
            {
                sql.AppendLine(" AND G.ProviderID =" + ProviderID + " ");
            }
            sql.AppendLine(" AND G.CurrencyType = " + CurrencyTypeID + " ");
            if (Rate != "0")
            {
                sql.AppendLine(" AND G.Rate = " + Rate + "   ");
            }
            if (!string.IsNullOrEmpty(StorageEFIndex) && !string.IsNullOrEmpty(StorageEFDesc))
            {
                sql.AppendLine("	AND B.ExtField" + StorageEFIndex + " LIKE '%" + StorageEFDesc + "%' ");
            }
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion
        #region 确认退货单
        public static bool ConfirmPurchaseReject(PurchaseRejectModel Model, string DetailBackCount, string DetailFromBillNo, string DetailFromLineNo, string DetailProductNo, string length, out string strMsg)
        {
            strMsg = "";
            bool result = true;
            if (isCanDo(Model.ID, "1"))
            {

                #region 确认
                ArrayList listADD = new ArrayList();
                StringBuilder strSql = new StringBuilder();
                strSql.AppendLine("update officedba.PurchaseReject set ");
                strSql.AppendLine(" Confirmor=@Confirmor");
                strSql.AppendLine(" ,BillStatus=2");
                strSql.AppendLine(" ,ConfirmDate=getdate()");
                strSql.AppendLine(" where");
                strSql.AppendLine(" CompanyCD=@CompanyCD");
                strSql.AppendLine(" and ID=@ID");
                SqlCommand comm = new SqlCommand();
                comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", Model.Confirmor));
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@ID", Model.ID));
                comm.CommandText = strSql.ToString();

                listADD.Add(comm);
                #endregion

                #region 确认时回填到货中的退货数量
                try
                {

                    int lengs = Convert.ToInt32(length);
                    if (lengs > 0)
                    {
                        string[] BackCount = DetailBackCount.Split(',');
                        string[] FromBillNo = DetailFromBillNo.Split(',');
                        string[] FromLineNo = DetailFromLineNo.Split(',');
                        string[] ProductID = DetailProductNo.Split(',');
                        string pp = "";

                        for (int i = 0; i < lengs; i++)
                        {
                            if (FromLineNo[i].ToString() != "")
                            {
                                if (IsCanConfirm(FromBillNo[i].ToString(), FromLineNo[i].ToString(), Model.CompanyCD, BackCount[i].ToString(), Model.FromType))
                                {

                                    System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                                    SqlCommand comms = new SqlCommand();
                                    if (Model.FromType == "1")
                                    {
                                        cmdsql.AppendLine("Update  Officedba.PurchaseArriveDetail set BackCount=isnull(BackCount,0)+@BackCount");
                                        cmdsql.AppendLine(" where CompanyCD=@CompanyCD and ArriveNo=@FromBillNo and SortNo=@FromLineNo");
                                    }
                                    else if (Model.FromType == "2")
                                    {
                                        cmdsql.AppendLine("Update  Officedba.StorageInPurchaseDetail set BackCount=isnull(BackCount,0)+@BackCount");
                                        cmdsql.AppendLine(" where CompanyCD=@CompanyCD and InNo=@FromBillNo and SortNo=@FromLineNo");
                                    }

                                    comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD));
                                    comms.Parameters.Add(SqlHelper.GetParameter("@BackCount", BackCount[i].ToString()));
                                    comms.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", FromBillNo[i].ToString()));
                                    comms.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", FromLineNo[i].ToString()));

                                    comms.CommandText = cmdsql.ToString();


                                    listADD.Add(comms);
                                    if (Model.FromType == "1")
                                    {
                                        if (getOriginalType(Model.CompanyCD, FromBillNo[i].ToString(), FromLineNo[i].ToString()))
                                        {
                                            listADD.Add(WriteStorge(ProductID[i].ToString(), BackCount[i].ToString()));
                                        }
                                    }
                                }
                                else
                                {
                                    if (pp != "")
                                    {
                                        pp += "," + (i + 1);
                                    }
                                    else
                                    {
                                        pp = "" + (i + 1);
                                    }
                                    decimal temp = GetEnableCount(FromBillNo[i].ToString(), FromLineNo[i].ToString(), Model.CompanyCD, BackCount[i].ToString(), Model.FromType);
                                    strMsg += "第" + pp + "行的退货数量不能大于当前可用的退货数量" + Convert.ToString(temp) + ",确认失败！";
                                    //strMsg = "数据溢出，确认失败！";
                                    result = false;
                                }
                            }
                        }
                    }


                    if (result)
                    {
                        if (SqlHelper.ExecuteTransWithArrayList(listADD))
                        {
                            //ID = comm.Parameters["@ID"].Value.ToString();
                            strMsg = "确认成功！";
                            result = true;
                        }
                        else
                        {
                            strMsg = "确认失败！";
                            result = false;
                        }
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    strMsg = "确认失败！";
                    result = false;
                    return result;
                    throw ex;
                }
                #endregion
            }
            else
            {//已经被其他人确认
                strMsg = "已经确认的单据不可再次确认！";
                result = false;
                return result;

            }

        }
        #endregion
        /// <summary>
        /// 判断原单类型为采购到货明细里类型是否为采购订单
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool getOriginalType(string CompanyCD, string FromBillNo, string FromLineNo)
        {
            String sql = "select FromType from officedba.PurchaseArriveDetail " +
                 "where CompanyCD=@CompanyCD and ArriveNo=@ArriveNo and SortNo=@SortNo";
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar),
                    new SqlParameter("@ArriveNo", SqlDbType.VarChar),
                       new SqlParameter("@SortNo", SqlDbType.VarChar),                    };
            parameters[0].Value = CompanyCD;
            parameters[1].Value = FromBillNo;
            parameters[2].Value = FromLineNo;

            int i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql, parameters));
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        /// <summary>
        /// 判断这条物品在分仓存量表
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static bool IsExistInStorge(string ProductID)
        {
            String sql = "SELECT A.ID, A.StorageID FROM officedba.StorageProduct AS A INNER JOIN " +
                      "officedba.ProductInfo AS B ON A.ProductID = B.ID AND A.StorageID = B.StorageID AND A.CompanyCD = B.CompanyCD " +
                      "WHERE A.CompanyCD=@CompanyCD AND A.ProductID=@ProductID";
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar),
                    new SqlParameter("@ProductID", SqlDbType.VarChar),};
            parameters[0].Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            parameters[1].Value = ProductID;

            return SqlHelper.Exists(sql, parameters);
        }
        public static SqlCommand WriteAddStorge(string ProductID, string ProductCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            if (IsExistInStorge(ProductID))
            {
                sql.AppendLine("UPDATE officedba.StorageProduct   ");
                sql.AppendLine(" SET RoadCount = isnull(RoadCount,0)+@RoadCount");
                sql.AppendLine(" WHERE ProductID = @ProductID ");
                sql.AppendLine(" AND StorageID = (SELECT StorageID FROM officedba.ProductInfo WHERE ID=@ProductID)");
                sql.AppendLine(" AND CompanyCD = @CompanyCD");

                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RoadCount", ProductCount));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
                comm.CommandText = sql.ToString();
            }
            else
            {
                //sql.AppendLine("INSERT INTO officedba.StorageProduct ( CompanyCD,StorageID,ProductID,RoadCount)");
                //sql.AppendLine(" SELECT @CompanyCD,(SELECT StorageID FROM officedba.ProductInfo WHERE ID=@ProductID),@ProductID,@RoadCount ");


                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductM.ProductID));
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@RoadCount", ProductM.ProductCount));
                //comm.CommandText = sql.ToString();
            }
            comm.CommandText = sql.ToString();

            return comm;
        }
        /// <summary>
        /// 回写库存分仓存量表
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="ProductCount"></param>
        /// <returns></returns>
        public static SqlCommand WriteStorge(string ProductID, string ProductCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            if (IsExistInStorge(ProductID))
            {
                sql.AppendLine("UPDATE officedba.StorageProduct   ");
                sql.AppendLine(" SET RoadCount = isnull(RoadCount,0)-@RoadCount");
                sql.AppendLine(" WHERE ProductID = @ProductID ");
                sql.AppendLine(" AND StorageID = (SELECT StorageID FROM officedba.ProductInfo WHERE ID=@ProductID)");
                sql.AppendLine(" AND CompanyCD = @CompanyCD");

                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RoadCount", ProductCount));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
                comm.CommandText = sql.ToString();
            }
            else
            {
                //sql.AppendLine("INSERT INTO officedba.StorageProduct ( CompanyCD,StorageID,ProductID,RoadCount)");
                //sql.AppendLine(" SELECT @CompanyCD,(SELECT StorageID FROM officedba.ProductInfo WHERE ID=@ProductID),@ProductID,@RoadCount ");


                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductM.ProductID));
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@RoadCount", ProductM.ProductCount));
                //comm.CommandText = sql.ToString();
            }
            comm.CommandText = sql.ToString();

            return comm;
        }
        #region 取消确认退货单
        public static bool CancelConfirmPurchaseReject(PurchaseRejectModel Model, string DetailBackCount, string DetailFromBillNo, string DetailFromLineNo, string DetailProductNo, string length, out string strMsg)
        {
            bool isSuc = false;
            strMsg = "";
            //判断单据是否为执行状态，非执行状态不能取消确认
            SqlCommand comm = new SqlCommand();
            String sql = "SELECT [isOpenbill]  FROM [officedba].[PurchaseReject] WHERE ID=@ID AND CompanyCD=@CompanyCD AND isOpenBill='1'";
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", Model.ID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", Model.CompanyCD.ToString()));
            comm.CommandText = sql;
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            if (dt != null && dt.Rows.Count != 0)
            {
                strMsg = "已建单的单据不能取消确认！";
                return false;
            }
            if (isCanDo(Model.ID, "2"))
            {

                if (!IsCitePurchaseReject(Model.ID))
                {
                    string strSq = string.Empty;
                    //bool isSuc = false;
                    int iEmployeeID = 0;//员工id
                    string strUserID = string.Empty;//用户id
                    string strCompanyCD = string.Empty;//单位编码
                    SqlParameter[] paras = new SqlParameter[5];
                    iEmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                    strUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                    strSq = "update  officedba.PurchaseReject set BillStatus='1'  ";
                    strSq += " , Confirmor=@Confirmor, ConfirmDate=@ConfirmDate, ModifiedDate=getdate() ,ModifiedUserID=@UserID ";
                    strSq += " WHERE ID = @ID and CompanyCD=@CompanyCD";

                    paras[0] = new SqlParameter("@Confirmor", DBNull.Value);
                    paras[1] = new SqlParameter("@UserID", strUserID);
                    paras[2] = new SqlParameter("@CompanyCD", Model.CompanyCD);
                    paras[3] = new SqlParameter("@ID", Model.ID);
                    paras[4] = new SqlParameter("@ConfirmDate", DBNull.Value);
                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {
                        int lengs = Convert.ToInt32(length);
                        if (lengs > 0)
                        {
                            string[] BackCount = DetailBackCount.Split(',');
                            string[] FromBillNo = DetailFromBillNo.Split(',');
                            string[] FromLineNo = DetailFromLineNo.Split(',');
                            string[] ProductID = DetailProductNo.Split(',');
                            for (int i = 0; i < lengs; i++)
                            {
                                StringBuilder cmdsql = new StringBuilder();
                                if (Model.FromType == "1")
                                {
                                    cmdsql.Append("Update  Officedba.PurchaseArriveDetail set BackCount=isnull(BackCount,0)-@BackCount");
                                    cmdsql.Append(" where CompanyCD=@CompanyCD and ArriveNo=@FromBillNo and SortNo=@FromLineNo");
                                }
                                else if (Model.FromType == "2")
                                {
                                    cmdsql.AppendLine("Update  Officedba.StorageInPurchaseDetail set BackCount=isnull(BackCount,0)-@BackCount");
                                    cmdsql.AppendLine(" where CompanyCD=@CompanyCD and InNo=@FromBillNo and SortNo=@FromLineNo");
                                }

                                SqlParameter[] param = { 
                                                new SqlParameter("@CompanyCD", Model.CompanyCD),
                                                new SqlParameter("@BackCount", BackCount[i].ToString()),
                                                new SqlParameter("@FromBillNo", FromBillNo[i].ToString()),
                                                new SqlParameter("@FromLineNo", FromLineNo[i].ToString())
                                           };
                                if (cmdsql.Length > 0)
                                {
                                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, cmdsql.ToString(), param);
                                }
                                if (Model.FromType == "1")
                                {
                                    if (FromBillNo[i].ToString() != "")
                                    {
                                        StringBuilder sqlComand = new StringBuilder();
                                        string TempStr = ProductID[i].ToString();
                                        if (IsExistInStorge(TempStr))
                                        {
                                            sqlComand.AppendLine("UPDATE officedba.StorageProduct   ");
                                            sqlComand.AppendLine(" SET RoadCount = isnull(RoadCount,0)+@RoadCount");
                                            sqlComand.AppendLine(" WHERE ProductID = @ProductID ");
                                            sqlComand.AppendLine(" AND StorageID = (SELECT StorageID FROM officedba.ProductInfo WHERE ID=@ProductID)");
                                            sqlComand.AppendLine(" AND CompanyCD = @CompanyCD");

                                            SqlParameter[] param1 = { 
                                                    new SqlParameter("@ProductID", ProductID[i].ToString ()),
                                                    new SqlParameter("@RoadCount", BackCount[i].ToString()), 
                                                    new SqlParameter("@CompanyCD",  ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD)
                                                                    };
                                            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlComand.ToString(), param1);
                                        }
                                    }

                                }
                            }
                        }


                        FlowDBHelper.OperateCancelConfirm(Model.CompanyCD, Convert.ToInt32(ConstUtil.CODING_RULE_PURCHASE), Convert.ToInt32(ConstUtil.BILL_TYPEFLAG_PURCHASE_REJECT), Model.ID, strUserID, tran);//撤销审批
                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);

                        tran.Commit();
                        isSuc = true;
                        strMsg = "取消确认成功！";

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        strMsg = "取消确认失败！";

                        isSuc = false;

                        throw ex;
                    }
                }
                else
                {//单据被调用了,不可以取消确认
                    isSuc = false;
                    strMsg = "该单据已被其它单据调用了，不允许取消确认！";
                }
            }
            else
            {//单据被别人抢先取消确认了
                isSuc = false;
                strMsg = "该单据已被其他用户取消确认，不可再次取消确认！";
            }


            return isSuc;

        }
        #endregion

        #region 结单退货单
        public static bool ClosePurchaseReject(PurchaseRejectModel Model)
        {
            #region SQL文
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.PurchaseReject set ");
            strSql.AppendLine(" Closer =  @Closer");
            strSql.AppendLine(" ,BillStatus=4");
            strSql.AppendLine(" ,CloseDate=getdate()");
            strSql.AppendLine(" where");
            strSql.AppendLine(" CompanyCD= @CompanyCD");
            strSql.AppendLine(" and ID= @ID");
            #endregion

            #region 参数
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@Closer", Model.Closer);
            param[1] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
            param[2] = SqlHelper.GetParameter("@ID", Model.ID);
            #endregion

            SqlHelper.ExecuteTransSql(strSql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        #region 取消结单退货单
        public static bool CancelClosePurchaseReject(PurchaseRejectModel Model)
        {
            #region SQL文
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.PurchaseReject set ");
            strSql.AppendLine(" Closer =  @Closer");
            strSql.AppendLine(" ,BillStatus=2");
            strSql.AppendLine(" ,CloseDate=getdate()");
            strSql.AppendLine(" where");
            strSql.AppendLine(" CompanyCD= @CompanyCD");
            strSql.AppendLine(" and ID= @ID");
            #endregion

            #region 参数
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@Closer", Model.Closer);
            param[1] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
            param[2] = SqlHelper.GetParameter("@ID", Model.ID);
            #endregion

            SqlHelper.ExecuteTransSql(strSql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        #region 查询退货列表所需数据
        public static DataTable SelectRejectList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string RejectNo, string Title, string TypeID, string Purchaser, string FromType, string ProviderID, string BillStatus, string UsedStatus, string DeptID, string ProjectID, string EFIndex, string EFDesc)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ID ,A.RejectNo ,isnull(A.Title,'') AS Title ,A.TypeID,isnull(B.TypeName,'') AS TypeName,A.Purchaser,isnull(C.EmployeeName,'')  AS PurchaserName ");
            sql.AppendLine("      ,A.FromType,case A.FromType when '0' then '无来源' when '1' then '采购到货单' when '2' then '采购入库' end AS FromTypeName,A.BillStatus,A.Rate  ");
            sql.AppendLine("   ,A.ProviderID ,isnull(D.CustName,'') AS  ProviderName ,isnull(A.DeptID,0 ) AS DeptID,isnull(E.DeptName,'') AS DeptName, isnull(A.TotalYthkhj,0) AS TotalYthkhj ,isnull( A.ModifiedDate,'') AS ModifiedDate");
            sql.AppendLine("      ,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'                ");
            sql.AppendLine("       when '4' then '手工结单' when '5' then '自动结单' end AS BillStatusName                              ");
            sql.AppendLine("      ,isnull(case F.FlowStatus when '0' then '请选择' when '1' then '待审批' when '2' then '审批中'    ");
            sql.AppendLine("      when '3'  then '审批通过' when '4' then '审批不通过' when '5' then '撤消审批' end,'')  AS  UsedStatus ");
            sql.AppendLine(" ,isnull(case(select count(1) from officedba.StorageOutOther AS G where G.CompanyCD=A.CompanyCD and G.FromBillID = A.ID  and G.FromType='1' ) when 0 then 'False' end , 'True') AS Isyinyong");
            sql.AppendLine(" FROM officedba.PurchaseReject AS A                                                                     ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS B ON A.CompanyCD = B.CompanyCD AND B.TypeFlag=7 AND B.TypeCode=5 AND A.TypeID=B.ID");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Purchaser=C.ID                   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS D  ON  A.CompanyCD = D.CompanyCD AND A.ProviderID = D.ID              ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS E ON A.CompanyCD = E.CompanyCD AND A.DeptID=E.ID                  ");
            sql.AppendLine("LEFT JOIN officedba.FlowInstance AS F ON  A.CompanyCD = F.CompanyCD  AND F.BillTypeFlag = '" + ConstUtil.BILL_TYPEFLAG_PURCHASE + "' ");
            sql.AppendLine("AND F.BillTypeCode = '" + ConstUtil.BILL_TYPEFLAG_PURCHASE_REJECT + "' AND F.BillNo = A.RejectNo          ");
            sql.AppendLine(" AND F.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = '" + ConstUtil.BILL_TYPEFLAG_PURCHASE + "' AND F.BillTypeCode = '" + ConstUtil.BILL_TYPEFLAG_PURCHASE_REJECT + "' )");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
            if (RejectNo != "" && RejectNo != null)
            {
                sql.AppendLine(" AND A.RejectNo like'%" + @RejectNo + "%' ");
            }
            if (Title != null && Title != "")
            {
                sql.AppendLine(" AND A.Title like'%" + @Title + "%'  ");
            }
            if (TypeID != null && TypeID != "" && TypeID != "null")
            {
                sql.AppendLine(" AND A.TypeID =@TypeID");
            }
            if (Purchaser != "" && Purchaser != null)
            {
                sql.AppendLine(" AND A.Purchaser=@Purchaser ");
            }
            if (FromType != null && FromType != "")
            {
                sql.AppendLine(" AND A.FromType =@FromType");
            }
            if (ProviderID != "" && ProviderID != "")
            {
                sql.AppendLine(" AND A.ProviderID=@ProviderID ");
            }
            if (BillStatus != null && BillStatus != "")
            {
                sql.AppendLine(" AND A.BillStatus = @BillStatus");
            }
            if (UsedStatus != null && UsedStatus != "")
            {
                if (UsedStatus == "-1")
                {
                    sql.AppendLine(" AND F.FlowStatus is null ");
                }
                else
                {
                    sql.AppendLine(" AND F.FlowStatus = @UsedStatus");
                }
            }
            if (DeptID != null && DeptID != "")
            {
                sql.AppendLine(" AND A.DeptID = @DeptID");
            }
            if (ProjectID != null && ProjectID != "")
            {
                sql.AppendLine(" AND A.ProjectID = @ProjectID");
            }

            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine(" and a.ExtField" + EFIndex + " LIKE @EFDesc");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RejectNo", RejectNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", Title));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", TypeID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Purchaser", Purchaser));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", FromType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", UsedStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", ProjectID));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion

        #region 查找加载单个退货单记录
        public static DataTable SelectReject(int ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.RejectNo ,A.Title ,A.ProviderID,isnull(B.CustName,'') AS ProviderName ,A.FromType,case A.FromType when '0' then '无来源' when '1' then '采购到货单' end AS FromTypeName,A.PayType,isnull(C.TypeName,'') AS PayTypeName,A.MoneyType,isnull(M.TypeName,'') AS  MoneyTypeName ");
            sql.AppendLine("     ,A.Purchaser,isnull(D.EmployeeName,'') AS PurchaserName, A.DeptID,isnull(E.DeptName,'') AS DeptName            ");
            sql.AppendLine("     ,A.TakeType, isnull(G.TypeName,'') AS TakeTypeName,A.BillStatus,Convert(varchar(100),A.RejectDate,23) AS RejectDate         ");
            sql.AppendLine("     ,A.CarryType,isnull(H.TypeName,'') AS CarryTypeName ,Convert(numeric(12,2),A.TotalPrice) AS TotalPrice,Convert(numeric(12,2),A.TotalTax) AS TotalTax,Convert(numeric(12,2),A.TotalFee) AS TotalFee,Convert(numeric(12,2),A.Discount) AS Discount,isnull(A.DiscountTotal,0) AS DiscountTotal,isnull(A.RealTotal,0 ) AS  RealTotal");
            sql.AppendLine("     ,A.isAddTax, case A.isAddTax when '0' then '否' when '1' then '是' end AS isAddTaxName,A.CountTotal,A.remark,CONVERT(varchar(100),A.CreateDate,23) AS  CreateDate                               ");
            sql.AppendLine("      ,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'when '4' then '手工结单'                              ");
            sql.AppendLine("       when '5' then '自动结单' end AS BillStatusName  , A.Creator,isnull(P.EmployeeName,'') AS CreatorName                                  ");
            sql.AppendLine("     ,A.ModifiedUserID, A.Confirmor,isnull(J.EmployeeName,'') AS ConfirmorName,isnull(CONVERT(varchar(100),A.ConfirmDate,23),'') AS  ConfirmDate   ");
            sql.AppendLine("     ,A.Closer,isnull(K.EmployeeName,'') AS CloserName, isnull(CONVERT(varchar(100),A.CloseDate,23),'') AS  CloseDate, A.TypeID,isnull(L.TypeName,'') AS TypeName");
            sql.AppendLine("      ,case A.FromType when '0' then '无来源' when '1' then '采购订单'  when '2' then '采购入库'  end as FromTypeName  ");
            sql.AppendLine("     ,A.SendAddress,A.ReceiveOverAddress,A.CurrencyType,A.Rate");
            sql.AppendLine("     ,A.ReceiveMan,A.ReceiveTel,A.TotalDyfzk,A.TotalYthkhj ,N.CurrencyName AS  CurrencyTypeName,CONVERT(varchar(100),A.ModifiedDate,23) AS  ModifiedDate");
            sql.AppendLine("      ,isnull(case O.FlowStatus when '0' then '请选择' when '1' then '待审批' when '2' then '审批中'    ");
            sql.AppendLine("      when '3'  then '审批通过' when '4' then '审批不通过' when '5' then '撤消审批' end,'')  AS  UsedStatus ");
            sql.AppendLine("     ,isnull(A.isOpenbill,0) AS isOpenbill,case A.isOpenbill when '0' then '否' when '1' then '是' end AS isOpenbillName ");
            sql.AppendLine(",isnull(a.ExtField1,'') as ExtField1 ");
            sql.AppendLine(",isnull(a.ExtField2,'') as ExtField2 ");
            sql.AppendLine(",isnull(a.ExtField3,'') as ExtField3 ");
            sql.AppendLine(",isnull(a.ExtField4,'') as ExtField4 ");
            sql.AppendLine(",isnull(a.ExtField5,'') as ExtField5 ");
            sql.AppendLine(",isnull(a.ExtField6,'') as ExtField6 ");
            sql.AppendLine(",isnull(a.ExtField7,'') as ExtField7 ");
            sql.AppendLine(",isnull(a.ExtField8,'') as ExtField8 ");
            sql.AppendLine(",isnull(a.ExtField9,'') as ExtField9 ");
            sql.AppendLine(",isnull(a.ExtField10,'') as ExtField10 ");
            sql.AppendLine(",A.ProjectID,pi1.ProjectName ");

            sql.AppendLine(" FROM officedba.PurchaseReject AS A                                                                           ");
            sql.AppendLine("LEFT JOIN officedba.ProjectInfo pi1 ON pi1.ID=A.ProjectID                  ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo   AS B on A.CompanyCD = B.CompanyCD  AND A.ProviderID=B.ID                   ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS C ON A.CompanyCD = C.CompanyCD  AND A.PayType=C.ID                      ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON A.CompanyCD = D.CompanyCD AND A.Purchaser=D.ID                        ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS E ON A.CompanyCD = E.CompanyCD AND A.DeptID=E.ID                               ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS G ON A.CompanyCD = G.CompanyCD  AND A.TakeType=G.ID                      ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS H ON A.CompanyCD = H.CompanyCD  AND A.CarryType=H.ID                     ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS J ON A.CompanyCD = J.CompanyCD AND A.Confirmor=J.ID                         ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS K ON A.CompanyCD = K.CompanyCD AND A.Closer=K.ID                            ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS L ON A.CompanyCD = L.CompanyCD  AND A.TypeID=L.ID                         ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS M ON A.CompanyCD = M.CompanyCD  AND A.MoneyType=M.ID                      ");
            sql.AppendLine("LEFT JOIN officedba.CurrencyTypeSetting AS N ON A.CompanyCD = N.CompanyCD  AND A.CurrencyType=N.ID              ");
            sql.AppendLine("LEFT JOIN officedba.FlowInstance AS O ON  A.CompanyCD = O.CompanyCD  AND O.BillTypeFlag = 6 ");
            sql.AppendLine("AND O.BillTypeCode = 6 AND O.BillNo = A.RejectNo          ");
            sql.AppendLine(" AND O.ID=(SELECT max(ID) FROM officedba.FlowInstance AS O WHERE A.ID = O.BillID AND O.BillTypeFlag = 6 AND O.BillTypeCode = 6 )");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS P ON A.CompanyCD = P.CompanyCD AND A.Creator=P.ID                           ");

            sql.AppendLine("WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
            sql.AppendLine("AND A.ID = " + ID + "");



            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 查找加载退货单明细
        public static DataTable Details(int ID, string FormType)
        {
            StringBuilder sql = new StringBuilder();
            if (FormType == "1" || FormType == "0")
            {
                sql.AppendLine("SELECT   A.ID,A.CompanyCD,A.RejectNo ,isnull(A.SortNo,0) AS SortNo ,isnull(A.FromBillID,0) AS FromBillID,A.ProductID ");
                sql.AppendLine(",A.ProductNo ,A.ProductName,A.UnitID,isnull(A.ProductCount,0) AS ProductCount    ");
                sql.AppendLine(", B.Specification AS Specification ,isnull(A.BackCount,0) AS BackCount,A.ApplyReason,C.CodeName AS ApplyReasonName ");
                sql.AppendLine(",case when A.UnitID is null then '' when A.UnitID ='' then ''                         ");
                sql.AppendLine("else (select CodeName from officedba.CodeUnitType where id=A.UnitID)end as UnitName   ");
                sql.AppendLine(",isnull(A.FromType,'') AS  FromType ,isnull(B.TaxRate,0) AS HidTaxRate                           ");
                sql.AppendLine(",isnull(case A.FromType when '0' then ''                                                          ");
                sql.AppendLine(" when '1' then (select ArriveNo from officedba.PurchaseArrive where ID=A.FromBillID)         ");
                sql.AppendLine(" when '2' then (select InNo from officedba.StorageInPurchase where ID=A.FromBillID)         ");
                sql.AppendLine("  end,'') AS FromBillNo                                                                        ");
                sql.AppendLine(",ISNULL(cpt.TypeName,'') AS ColorName ");
                sql.AppendLine(",isnull(A.UnitPrice,0) AS UnitPrice ,isnull(A.TaxPrice,0) AS TaxPrice,isnull(A.Discount,0) AS Discount,isnull(A.TaxRate,0) AS TaxRate,isnull(A.TotalFee,0) AS TotalFee,isnull(A.TotalPrice,0) AS TotalPrice,isnull(A.TotalTax,0) AS TotalTax");
                sql.AppendLine(",isnull(case when A.FromBillID is null then 0 when A.FromBillID = '' then 0 ");
                sql.AppendLine(" else (select top(1) BackCount from officedba.PurchaseArriveDetail where CompanyCD=A.CompanyCD AND ArriveNo=D.ArriveNo and SortNo=A.FromLineNo)end,0) AS YBackCount");
                sql.AppendLine(",(select CodeName from officedba.CodeUnitType where id=A.UsedUnitID) as UsedUnitName   ");
                sql.AppendLine(",isnull(A.Remark,'') AS Remark,isnull(A.FromLineNo,0) AS FromLineNo,isnull(A.OutedTotal,0) AS OutedTotal,A.UsedUnitID,A.UsedUnitCount,A.UsedPrice  ,isnull(F.CodeName,'') as UsedUnitName    FROM officedba.PurchaseRejectDetail AS A     ");

                sql.AppendLine("LEFT JOIN officedba.ProductInfo   AS B on A.CompanyCD = B.CompanyCD  AND A.ProductID=B.ID    ");
                sql.AppendLine("LEFT JOIN officedba.CodePublicType cpt ON B.ColorID=cpt.ID   ");
                sql.AppendLine("LEFT JOIN officedba.CodeReasonType   AS C on A.CompanyCD = C.CompanyCD  AND A.ApplyReason=C.ID    ");
                sql.AppendLine("LEFT outer JOIN officedba.CodeUnitType   AS F on A.CompanyCD = F.CompanyCD  AND A.UsedUnitID=F.ID    ");
                sql.AppendLine("LEFT JOIN officedba.PurchaseArrive   AS D on A.CompanyCD = D.CompanyCD  AND A.FromBillID=D.ID     ");
                sql.AppendLine("LEFT JOIN officedba.PurchaseReject   AS E on A.CompanyCD = E.CompanyCD  AND A.RejectNo=E.RejectNo   ");
            }
            else if (FormType == "2")
            {
                sql.AppendLine("SELECT   A.ID,A.CompanyCD,A.RejectNo ,isnull(A.SortNo,0) AS SortNo ,isnull(A.FromBillID,0) AS FromBillID,A.ProductID ");
                sql.AppendLine(",A.ProductNo ,A.ProductName,A.UnitID,isnull(A.ProductCount,0) AS ProductCount    ");
                sql.AppendLine(", B.Specification AS Specification ,isnull(A.BackCount,0) AS BackCount,A.ApplyReason,C.CodeName AS ApplyReasonName ");
                sql.AppendLine(",case when A.UnitID is null then '' when A.UnitID ='' then ''                         ");
                sql.AppendLine("else (select CodeName from officedba.CodeUnitType where id=A.UnitID)end as UnitName   ");
                sql.AppendLine(",isnull(A.FromType,'') AS  FromType ,isnull(B.TaxRate,0) AS HidTaxRate          ");
                sql.AppendLine(",isnull(case A.FromType when '0' then ''                                         ");
                sql.AppendLine(" when '1' then (select ArriveNo from officedba.PurchaseArrive where ID=A.FromBillID)         ");
                sql.AppendLine(" when '2' then (select InNo from officedba.StorageInPurchase where ID=A.FromBillID)         ");
                sql.AppendLine(" end,'') AS FromBillNo                                                           ");
                sql.AppendLine(",ISNULL(cpt.TypeName,'') AS ColorName ");
                sql.AppendLine(",isnull(A.UnitPrice,0) AS UnitPrice ,isnull(A.TaxPrice,0) AS TaxPrice,isnull(A.Discount,0) AS Discount,isnull(A.TaxRate,0)");
                sql.AppendLine("AS TaxRate,isnull(A.TotalFee,0) AS TotalFee,isnull(A.TotalPrice,0) AS TotalPrice,isnull(A.TotalTax,0) AS TotalTax");
                sql.AppendLine(", isnull(case when A.FromBillID is null then 0 when A.FromBillID = '' then 0 ");
                sql.AppendLine("else (select BackCount from officedba.StorageInPurchaseDetail where InNo=D.InNo and SortNo=A.FromLineNo)end,0) AS YBackCount");
                sql.AppendLine(",isnull(A.Remark,'') AS Remark,isnull(A.FromLineNo,0) AS FromLineNo,isnull(A.OutedTotal,0) AS OutedTotal,A.UsedUnitID,A.UsedUnitCount,A.UsedPrice,isnull(F.CodeName,'') as UsedUnitName ");
                sql.AppendLine("FROM officedba.PurchaseRejectDetail AS A     ");
                sql.AppendLine("LEFT outer JOIN officedba.ProductInfo   AS B on A.CompanyCD = B.CompanyCD  AND A.ProductID=B.ID    ");
                sql.AppendLine("LEFT outer JOIN officedba.CodePublicType cpt ON B.ColorID=cpt.ID   ");
                sql.AppendLine("LEFT outer JOIN officedba.CodeReasonType   AS C on A.CompanyCD = C.CompanyCD  AND A.ApplyReason=C.ID    ");
                sql.AppendLine("LEFT outer JOIN officedba.CodeUnitType   AS F on A.CompanyCD = F.CompanyCD  AND A.UsedUnitID=F.ID    ");
                sql.AppendLine("LEFT outer JOIN officedba.StorageInPurchase   AS D on A.CompanyCD = D.CompanyCD  AND A.FromBillID=D.ID     ");
                sql.AppendLine("LEFT outer JOIN officedba.PurchaseReject   AS E on A.CompanyCD = E.CompanyCD  AND A.RejectNo=E.RejectNo  ");
            }
            sql.AppendLine("where A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' AND E.ID=" + ID + "  ");

            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 删除采购退货主表及明细
        public static bool DeletePurchaseRejectPrimary(string DetailNo)
        {
            //#region SQL文
            //string strSql = "delete officedba.PurchaseReject where CompanyCD='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and RejectNo='" + RejectNo + "'";
            //#endregion

            //sql[i] = strSql;

            string strCompanyCD = string.Empty;//单位编号
            bool isSucc = false;
            string AllDetailNo = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            TransactionManager tran = new TransactionManager();
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            string[] DetailNoS = null;
            DetailNoS = DetailNo.Split(',');

            for (int i = 0; i < DetailNoS.Length; i++)
            {
                DetailNoS[i] = "'" + DetailNoS[i] + "'";
                sb.Append(DetailNoS[i]);
            }

            AllDetailNo = sb.ToString().Replace("''", "','");
            tran.BeginTransaction();
            try
            {
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.PurchaseReject WHERE RejectNo IN ( " + AllDetailNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.PurchaseRejectDetail WHERE RejectNo IN ( " + AllDetailNo + " ) and CompanyCD='" + strCompanyCD + "'", null);

                tran.Commit();
                isSucc = true;

            }
            catch
            {
                tran.Rollback();
                isSucc = false;
            }
            return isSucc;

        }
        #endregion

        #region 删除采购退货通知明细
        //public static void DeletePurchaseRejectDetail(string RejectNo, ref string[] sql, int i)
        //{
        //    string strSql = "delete officedba.PurchaseRejectDetail where CompanyCD='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and RejectNo='" + RejectNo + "'";

        //    sql[i] = strSql;
        //}
        #endregion

        #region 判断单据是否可以执行
        /// <summary>
        /// 根据单据状态判断该单据是否可以执行该操作
        /// </summary>
        /// <param name="ID">单据ID</param>
        /// <param name="BillStatus">单据状态</param>
        /// <returns>返回true时表示可以执行操作</returns>
        private static bool isCanDo(int ID, string BillStatus)
        {
            bool isSuc = false;
            int iCount = 0;
            string strSql = string.Empty;

            strSql = "select count(1) from officedba.PurchaseReject where ID = @ID and BillStatus=@BillStatus ";
            ArrayList arr = new ArrayList();
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@ID", ID);
            paras[1] = new SqlParameter("@BillStatus", BillStatus);

            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount != 0)
            {
                isSuc = true;
            }

            return isSuc;

        }
        #endregion

        #region 判断采购退货有没有被引用
        public static bool IsCitePurchaseReject(int ID)
        {
            bool IsCite = false;
            //出库引用
            if (!IsCite)
            {
                string sql = "SELECT A.ID FROM officedba.StorageOutOther AS A WHERE A.FromType=@FromType AND A.FromBillID=@ID";
                SqlParameter[] parameters = {
                    new SqlParameter("@FromType", SqlDbType.VarChar),
					new SqlParameter("@ID", Convert.ToString(SqlDbType.Int)),};
                parameters[0].Value = "1";
                parameters[1].Value = ID;
                IsCite = SqlHelper.Exists(sql, parameters);
            }
            //财务引用
            if (!IsCite)
            {
                string sql = "SELECT E.ID FROM officedba.Billing AS E,officedba.PurchaseReject AS G WHERE E.BillingType=@FromType AND E.BillCD=G.RejectNo AND G.ID=@ID ";
                SqlParameter[] parameters = {
                    new SqlParameter("@FromType", SqlDbType.VarChar),
					new SqlParameter("@ID", Convert.ToString(SqlDbType.Int)),};
                parameters[0].Value = "5";
                parameters[1].Value = ID;
                IsCite = SqlHelper.Exists(sql, parameters);
            }
            return IsCite;


        }
        #endregion

        #region 确认时判断数量可否进行确认
        public static bool IsCanConfirm(string FromBillNo, string FromLineNo, string CompanyCD, string YOrderCount, string FromType)
        {


            try
            {

                StringBuilder sql = new StringBuilder();
                if (FromType == "1")
                {
                    sql.AppendLine("SELECT isnull(A.ProductCount,0) as ProductCount    ,isnull(A.BackCount,0) as  BackCount ,isnull(A.InCount,0) as  InCount FROM officedba.PurchaseArriveDetail AS A where CompanyCD=@CompanyCD");

                    sql.AppendLine("and ArriveNo=@ArriveNo  and SortNo=@SortNo ");
                }
                else if (FromType == "2")
                {
                    sql.AppendLine("SELECT isnull(A.ProductCount,0) as ProductCount    ,isnull(A.BackCount,0) as  BackCount    FROM officedba.StorageInPurchaseDetail  AS A where CompanyCD=@CompanyCD");

                    sql.AppendLine("and InNo=@ArriveNo  and SortNo=@SortNo ");
                }
                SqlParameter[] param = new SqlParameter[3];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@ArriveNo", FromBillNo);
                param[2] = SqlHelper.GetParameter("@SortNo", FromLineNo);
                DataTable dt = SqlHelper.ExecuteSql(sql.ToString(), param);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        decimal dlProductCount = Convert.ToDecimal(dt.Rows[0]["ProductCount"].ToString());
                        decimal dlArrivedCount = Convert.ToDecimal(dt.Rows[0]["BackCount"].ToString());
                        Decimal tmp = 0;
                        if (FromType == "1")
                        {
                            decimal dlInCount = Convert.ToDecimal(dt.Rows[0]["InCount"].ToString());
                            tmp = dlProductCount - dlArrivedCount - dlInCount;
                        }
                        else
                        {
                            tmp = dlProductCount - dlArrivedCount;
                        }
                        if (tmp >= Convert.ToDecimal(YOrderCount))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {

                return false;

                throw ex;
            }


        }
        #endregion
        public static decimal GetEnableCount(string FromBillNo, string FromLineNo, string CompanyCD, string YOrderCount, string FromType)
        {
            try
            {

                StringBuilder sql = new StringBuilder();
                if (FromType == "1")
                {
                    sql.AppendLine("SELECT isnull(A.ProductCount,0) as ProductCount    ,isnull(A.BackCount,0) as  BackCount ,isnull(A.InCount,0) as  InCount FROM officedba.PurchaseArriveDetail AS A where CompanyCD=@CompanyCD");

                    sql.AppendLine("and ArriveNo=@ArriveNo  and SortNo=@SortNo ");
                }
                else if (FromType == "2")
                {
                    sql.AppendLine("SELECT isnull(A.ProductCount,0) as ProductCount    ,isnull(A.BackCount,0) as  BackCount    FROM officedba.StorageInPurchaseDetail  AS A where CompanyCD=@CompanyCD");

                    sql.AppendLine("and InNo=@ArriveNo  and SortNo=@SortNo ");
                }
                SqlParameter[] param = new SqlParameter[3];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@ArriveNo", FromBillNo);
                param[2] = SqlHelper.GetParameter("@SortNo", FromLineNo);
                DataTable dt = SqlHelper.ExecuteSql(sql.ToString(), param);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        decimal dlProductCount = Convert.ToDecimal(dt.Rows[0]["ProductCount"].ToString());
                        decimal dlArrivedCount = Convert.ToDecimal(dt.Rows[0]["BackCount"].ToString());
                        Decimal tmp = 0;
                        if (FromType == "1")
                        {
                            decimal dlInCount = Convert.ToDecimal(dt.Rows[0]["InCount"].ToString());
                            tmp = dlProductCount - dlArrivedCount - dlInCount;
                        }
                        else
                        {
                            tmp = dlProductCount - dlArrivedCount;
                        }
                        return tmp;
                    }
                }
                return 0;
            }
            catch
            {
                return 0;
            }


        }
        #region 查询采购退货汇总报表
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable SelectPurchaseRejectOverview(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProductID, string Reason, string StartRejectDate, string EndRejectDate)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY " + orderBy + ") AS ROWNUMBER,A.* FROM ( ");
            sql.AppendLine("SELECT A.ProductID,isnull(B.ProdNo,'') AS ProductNo,isnull(B.ProductName,'') AS ProductName,SUM(isnull(A.BackCount,0)) AS BackCount");
            sql.AppendLine(" ,SUM(isnull(A.OutedTotal,0)) AS OutedTotal,isnull(CONVERT(varchar(100),C.RejectDate,23),'') AS  RejectDate  ");
            sql.AppendLine(" ,SUM(isnull(A.TotalFee,0)*isnull(C.Rate,1)) AS TotalFee");

            sql.AppendLine(" FROM officedba.PurchaseRejectDetail AS A                                                               ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProductID=B.ID                  ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseReject AS C ON A.CompanyCD = C.CompanyCD AND A.RejectNo=C.RejectNo          ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' AND C.BillStatus <>1 ");
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND A.ProductID=@ProductID ");
            }
            if (Reason != null && Reason != "")
            {
                sql.AppendLine(" AND A.ApplyReason =@Reason");
            }
            if (StartRejectDate != "" && StartRejectDate != null)
            {
                sql.AppendLine(" AND C.RejectDate >=@StartRejectDate ");
            }
            if (EndRejectDate != "" && EndRejectDate != null)
            {
                sql.AppendLine(" AND C.RejectDate <@EndRejectDate ");
            }
            sql.AppendLine(" GROUP BY A.ProductID,B.ProdNo,B.ProductName,C.RejectDate");
            sql.AppendLine(" ) AS A)AS A WHERE A.ROWNUMBER BETWEEN " + (pageIndex - 1) * pageCount + 1 + " AND " + pageIndex * pageCount + "");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Reason", Reason));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartRejectDate", StartRejectDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndRejectDate", Convert.ToDateTime(EndRejectDate).AddDays(1).ToString("yyyy-MM-dd")));

            comm.CommandText = sql.ToString();


            SqlCommand comm1 = new SqlCommand();
            StringBuilder sql1 = new StringBuilder();
            sql1.AppendLine("SELECT COUNT(*) FROM (SELECT A.ProductID,isnull(B.ProdNo,'') AS ProductNo,isnull(B.ProductName,'') AS ProductName,SUM(isnull(A.BackCount,0)) AS BackCount ");
            sql1.AppendLine(" ,SUM(isnull(A.OutedTotal,0)) AS OutedTotal,isnull(CONVERT(varchar(100),C.RejectDate,23),'') AS  RejectDate                                               ");
            sql1.AppendLine(" ,SUM(isnull(A.TotalFee,0)*isnull(C.Rate,1)) AS TotalFee                                                                                                  ");
            sql1.AppendLine(" FROM officedba.PurchaseRejectDetail AS A                                                                                                                                        ");
            sql1.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProductID=B.ID                                                                                           ");
            sql1.AppendLine("LEFT JOIN officedba.PurchaseReject AS C ON A.CompanyCD = C.CompanyCD AND A.RejectNo=C.RejectNo                                                                                   ");
            sql1.AppendLine(" WHERE 1=1                                                                                                                                                                       ");
            sql1.AppendLine("AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' AND C.BillStatus <>1                                                                                                                                   ");
            if (ProductID != "" && ProductID != null)
            {
                sql1.AppendLine(" AND A.ProductID=@ProductID ");
                comm1.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            }
            if (Reason != null && Reason != "")
            {
                sql1.AppendLine(" AND A.ApplyReason =@Reason");
                comm1.Parameters.Add(SqlHelper.GetParameterFromString("@Reason", Reason));
            }
            if (StartRejectDate != "" && StartRejectDate != null)
            {
                sql1.AppendLine(" AND C.RejectDate >=@StartRejectDate ");
                comm1.Parameters.Add(SqlHelper.GetParameterFromString("@StartRejectDate", StartRejectDate));
            }
            if (EndRejectDate != "" && EndRejectDate != null)
            {
                sql1.AppendLine(" AND C.RejectDate <@EndRejectDate ");
                comm1.Parameters.Add(SqlHelper.GetParameterFromString("@EndRejectDate", Convert.ToDateTime(EndRejectDate).AddDays(1).ToString("yyyy-MM-dd")));
            }
            sql1.AppendLine(" GROUP BY A.ProductID,B.ProdNo,B.ProductName,C.RejectDate) AS A                                                                                                                  ");
            comm1.CommandText = sql1.ToString();
            DataTable dt2 = SqlHelper.ExecuteSearch(comm1);
            TotalCount = int.Parse(dt2.Rows[0][0].ToString());

            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        #region 查询采购退货汇总报表打印
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable SelectPurchaseRejectOverviewPrint(string ProductID, string Reason, string StartRejectDate, string EndRejectDate, string orderBy)
        {
            SqlCommand comm = new SqlCommand();

            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ProductID,isnull(B.ProdNo,'') AS ProductNo,isnull(B.ProductName,'') AS ProductName,Convert(numeric(20," + jingdu + "),SUM(isnull(A.BackCount,0))) AS BackCount");
            sql.AppendLine(" ,Convert(numeric(20," + jingdu + "),SUM(isnull(A.OutedTotal,0))) AS OutedTotal,isnull(CONVERT(varchar(100),C.RejectDate,23),'') AS  RejectDate  ");
            sql.AppendLine(" ,Convert(numeric(20," + jingdu + "),SUM(isnull(A.TotalFee,0)*isnull(C.Rate,1))) AS TotalFee");

            sql.AppendLine(" FROM officedba.PurchaseRejectDetail AS A                                                               ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProductID=B.ID                  ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseReject AS C ON A.CompanyCD = C.CompanyCD AND A.RejectNo=C.RejectNo          ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' AND C.BillStatus <>1 ");
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND A.ProductID=@ProductID ");
            }
            if (Reason != null && Reason != "")
            {
                sql.AppendLine(" AND A.ApplyReason =@Reason");
            }
            if (StartRejectDate != "" && StartRejectDate != null)
            {
                sql.AppendLine(" AND C.RejectDate >=@StartRejectDate ");
            }
            if (EndRejectDate != "" && EndRejectDate != null)
            {
                sql.AppendLine(" AND C.RejectDate <@EndRejectDate ");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Reason", Reason));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartRejectDate", StartRejectDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndRejectDate", Convert.ToDateTime(EndRejectDate).AddDays(1).ToString("yyyy-MM-dd")));
            sql.AppendLine(" GROUP BY A.ProductID,B.ProdNo,B.ProductName,C.RejectDate ");
            sql.AppendLine(" ORDER BY " + orderBy + "");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 采购报表采购退货查询
        public static DataTable PurchaseRejectQuery(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProviderID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ID ,A.RejectNo ,isnull(A.Title,'') AS Title ,A.TypeID,isnull(B.TypeName,'') AS TypeName,A.Purchaser,isnull(C.EmployeeName,'') AS EmployeeName ");
            sql.AppendLine(" ,isnull(A.ProviderID,0) AS ProviderID, isnull(D.CustName,'') AS ProviderName, Convert(numeric(20,2),isnull(A.TotalYthkhj,0)*isnull(A.Rate,1)) AS TotalYthkhj                    ");

            sql.AppendLine(" FROM officedba.PurchaseReject AS A                                                                     ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS B ON A.CompanyCD = B.CompanyCD AND B.TypeFlag=7 AND B.TypeCode=5 AND A.TypeID=B.ID");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Purchaser=C.ID                   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS D  ON  A.CompanyCD = D.CompanyCD AND A.ProviderID = D.ID              ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.BillStatus <>1 AND A.CompanyCD = '" + CompanyCD + "'");
            if (ProviderID != "" && ProviderID != "")
            {
                sql.AppendLine(" AND A.ProviderID=@ProviderID ");
            }
            if (StartConfirmDate != null && StartConfirmDate != "")
            {
                sql.AppendLine(" AND A.RejectDate >= @StartConfirmDate");
            }
            if (EndConfirmDate != null && EndConfirmDate != "")
            {
                sql.AppendLine(" AND A.RejectDate <= @EndConfirmDate ");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", EndConfirmDate));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);

        }
        #endregion

        #region 采购报表采购退货查询打印
        public static DataTable PurchaseRejectQueryPrint(string ProviderID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ID ,A.RejectNo AS CompanyCD ,isnull(A.Title,'') AS RejectNo ,A.TypeID,isnull(B.TypeName,'') AS Title,A.Purchaser,isnull(C.EmployeeName,'') AS SendAddress ");
            sql.AppendLine(" ,isnull(A.ProviderID,0) AS ProviderID, isnull(D.CustName,'') AS ReceiveOverAddress, Convert(numeric(20,2),isnull(A.TotalYthkhj,0)*isnull(A.Rate,1)) AS RealTotal                    ");

            sql.AppendLine(" FROM officedba.PurchaseReject AS A                                                                     ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS B ON A.CompanyCD = B.CompanyCD AND B.TypeFlag=7 AND B.TypeCode=5 AND A.TypeID=B.ID");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Purchaser=C.ID                   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS D  ON  A.CompanyCD = D.CompanyCD AND A.ProviderID = D.ID              ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.BillStatus <>1 AND A.CompanyCD = '" + CompanyCD + "'");
            if (ProviderID != "" && ProviderID != "")
            {
                sql.AppendLine(" AND A.ProviderID=@ProviderID ");
            }
            if (StartConfirmDate != null && StartConfirmDate != "")
            {
                sql.AppendLine(" AND A.RejectDate >= @StartConfirmDate");
            }
            if (EndConfirmDate != null && EndConfirmDate != "")
            {
                sql.AppendLine(" AND A.RejectDate <= @EndConfirmDate ");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", EndConfirmDate));

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        /// <summary>
        /// 采购退货按类别统计
        /// zxb by 2009-11-05
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="billStatus"></param>
        /// <param name="FromType"></param>
        /// <param name="shState"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static DataTable GetPurchaseRejectType(string companycd, string billStatus, string FromType, int shState, string begindate, string enddate)
        {
            //@companyCD varchar(50),
            //@billStatus char(1),
            //@FromType char(1),
            //@shState int,-- -1 表示全部，0表示待提交，1：待审批, 2：审批中, 3：审批通过, 4：审批不通过, 5：撤销审批
            //@begindate varchar(10),
            //@enddate varchar(10)
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@billStatus",billStatus),
                new SqlParameter("@FromType",FromType),
                new SqlParameter("@shState",shState),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetPurchaseRejectNum]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 采购退货按类别统计明细
        /// zxb by 2009-11-05
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="billstatus"></param>
        /// <param name="fromtype"></param>
        /// <param name="shstate"></param>
        /// <param name="typeid"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetPurchaseRejectTypeDetails(string companycd, string billstatus, string fromtype, int shstate, int typeid, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companycd",companycd),
                new SqlParameter("@billstatus",billstatus),
                new SqlParameter("@fromtype",fromtype),
                new SqlParameter("@shstate",shstate),
                new SqlParameter("@typeid",typeid),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetPurchaseRejectNumDetails]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 采购退货按部门分布
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="billStatus"></param>
        /// <param name="FromType"></param>
        /// <param name="shState"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static DataTable GetPurchaseRejectTypeDept(string companycd, string billStatus, string FromType, int shState, string begindate, string enddate)
        {
            //@companyCD varchar(50),
            //@billStatus char(1),
            //@FromType char(1),
            //@shState int,-- -1 表示全部，0表示待提交，1：待审批, 2：审批中, 3：审批通过, 4：审批不通过, 5：撤销审批
            //@begindate varchar(10),
            //@enddate varchar(10)
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@billStatus",billStatus),
                new SqlParameter("@FromType",FromType),
                new SqlParameter("@shState",shState),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetPurchaseRejectNumDept]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 采购退货按部门分布
        /// zxb
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="billstatus"></param>
        /// <param name="fromtype"></param>
        /// <param name="shstate"></param>
        /// <param name="deptid"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetPurchaseRejectDeptDetails(string companycd, string billstatus, string fromtype, int shstate, int deptid, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companycd",companycd),
                new SqlParameter("@billstatus",billstatus),
                new SqlParameter("@fromtype",fromtype),
                new SqlParameter("@shstate",shstate),
                new SqlParameter("@deptID",deptid),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetPurchaseRejectNumDetailsDept]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 根据供应商统计
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="billStatus"></param>
        /// <param name="FromType"></param>
        /// <param name="shState"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static DataTable GetPurchaseRejectProvider(string companycd, string billStatus, string FromType, int shState, string begindate, string enddate)
        {
            //@companyCD varchar(50),
            //@billStatus char(1),
            //@FromType char(1),
            //@shState int,-- -1 表示全部，0表示待提交，1：待审批, 2：审批中, 3：审批通过, 4：审批不通过, 5：撤销审批
            //@begindate varchar(10),
            //@enddate varchar(10)
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@billStatus",billStatus),
                new SqlParameter("@FromType",FromType),
                new SqlParameter("@shState",shState),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetPurchaseRejectNumProvider]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 供应商统计明细
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="billstatus"></param>
        /// <param name="fromtype"></param>
        /// <param name="shstate"></param>
        /// <param name="providerID"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetPurchaseRejectProviderDetails(string companycd, string billstatus, string fromtype, int shstate, int providerID, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companycd",companycd),
                new SqlParameter("@billstatus",billstatus),
                new SqlParameter("@fromtype",fromtype),
                new SqlParameter("@shstate",shstate),
                new SqlParameter("@ProviderID",providerID),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetPurchaseRejectNumDetailsProvider]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 退货原因统计
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="billStatus"></param>
        /// <param name="FromType"></param>
        /// <param name="shState"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static DataTable GetPurchaseRejectReason(string companycd, string billStatus, string FromType, int shState, string begindate, string enddate)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@billStatus",billStatus),
                new SqlParameter("@FromType",FromType),
                new SqlParameter("@shState",shState),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetPurchaseRejectNumReason]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 退货原因明细
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="billstatus"></param>
        /// <param name="fromtype"></param>
        /// <param name="shstate"></param>
        /// <param name="reasonid"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetPurchaseReasonDetails(string companycd, string billstatus, string fromtype, int shstate, int reasonid, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companycd",companycd),
                new SqlParameter("@billstatus",billstatus),
                new SqlParameter("@fromtype",fromtype),
                new SqlParameter("@shstate",shstate),
                new SqlParameter("@ReasonID",reasonid),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetPurchaseRejectReasonDetails]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 采购退货走势
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="billStatus"></param>
        /// <param name="FromType"></param>
        /// <param name="shState"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static DataTable GetPurchaseRejectSetUp(string companycd, string billStatus, string FromType, int shState, string begindate, string enddate, int timeType)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@billStatus",billStatus),
                new SqlParameter("@FromType",FromType),
                new SqlParameter("@shState",shState),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@timeType",timeType)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetPurchaseRejectSetup]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 退货走势明细
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="billstatus"></param>
        /// <param name="fromtype"></param>
        /// <param name="shstate"></param>
        /// <param name="deptid"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="timeType"></param>
        /// <param name="timestr"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetPurchaseRejectSetUpDetails(string companycd, string billstatus, string fromtype, int shstate, string begindate, string enddate, string order, int pageindex, int pagesize, int timeType, string timestr, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companycd",companycd),
                new SqlParameter("@billstatus",billstatus),
                new SqlParameter("@fromtype",fromtype),
                new SqlParameter("@shstate",shstate),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@timeType",timeType),
                new SqlParameter("@timestr",timestr)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetPurchaseRejectSetupDetails]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }


        #region 新报表

        /// <summary>
        /// 退货物品分布
        /// </summary>
        public static DataTable GetRejectByProduct(string ProviderID, string DeptID, string BeginDate, string EndDate, string StatType)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            StringBuilder sb = new StringBuilder();
            sb.Append(" select min(c.productname) as Name,");
            if (StatType == "Num")
            {
                sb.Append(" sum(isnull(BackCount,0)) as counts ");
            }
            else
            {
                sb.Append(" sum(isnull(b.TotalFee,0)*isnull(a.Rate,1)) as counts ");
            }
            sb.Append(",b.productId  ");
            sb.Append(" from officedba.PurchaseRejectDetail as b left join officedba.PurchaseReject as a  ");
            sb.Append(" on a.RejectNO=b.RejectNO and a.companyCD=b.companyCD left join officedba.productinfo as c on b.productId=c.Id ");
            sb.Append(" WHERE 1=1 ");
            sb.Append("  AND A.CompanyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");

            if (ProviderID != "")
            {
                sb.Append("and A.providerid=");
                sb.Append(ProviderID);
            }
            if (DeptID != "")
            {
                sb.Append(" and  A.deptId=");
                sb.Append(DeptID);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.RejectDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.RejectDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by b.productId  ");

            return SqlHelper.ExecuteSql(sb.ToString());
        }

        public static DataTable GetPurchaseRejectProductDetail(string ProviderID, string DeptID, string BeginDate, string EndDate, string DateType, string DateValue, string ProductID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" select ");
            sb.AppendLine("  a.RejectNO as OrderNo,b.ProductID, ");
            sb.AppendLine("dateName(year,a.RejectDate)+'年' as OrderYear,");
            sb.AppendLine("dateName(year,a.RejectDate)+'年-'+dateName(month,a.RejectDate)+'月' as OrderMonth,");
            sb.AppendLine("dateName(year,a.RejectDate)+'年-'+dateName(week,a.RejectDate)+'周' as OrderWeek,");
            sb.AppendLine("  b.ProductNo,     ");
            sb.AppendLine("  b.ProductName,      ");
            sb.AppendLine("Convert(decimal(22," + jingdu + "), isnull(b.BackCount,0) ) as ProductCount,       ");
            sb.AppendLine(" Convert(decimal(22," + jingdu + "), isnull(b.UnitPrice,0)*isnull(a.Rate,1)) as UnitPrice,       ");
            sb.AppendLine("Convert(decimal(22," + jingdu + "),  isnull(b.TotalPrice,0)*isnull(a.Rate,1)) as TotalPrice, ");
            sb.AppendLine("Convert(decimal(22," + jingdu + "), isnull(b.TaxRate,0))  as TaxRate,   ");
            sb.AppendLine(" case a.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'                         ");
            sb.AppendLine(" when '4' then '手工结单' when '5' then '自动结单' end AS BillStatusName                                   ");
            sb.Append(" from officedba.PurchaseRejectDetail as b left join officedba.PurchaseReject as a  ");
            sb.Append(" on a.RejectNO=b.RejectNO and a.companyCD=b.companyCD left join officedba.productinfo as c on b.productId=c.Id  ");
            sb.Append(" WHERE 1=1 ");
            sb.Append("  AND A.CompanyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");
            if (ProviderID != "")
            {
                sb.Append("and A.providerid=");
                sb.Append(ProviderID);
            }
            if (DeptID != "")
            {
                sb.Append(" and  A.deptId=");
                sb.Append(DeptID);
            }
            if (ProductID != "")
            {
                sb.Append(" and  b.productId=");
                sb.Append(ProductID);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.RejectDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append(" and a.RejectDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }

            if (DateValue != "")
            {

                if (DateType == "1")
                {
                    sb.Append(" and (dateName(year,a.RejectDate)+'年')='");
                    sb.Append(DateValue);
                    sb.Append(" '");
                }
                else if (DateType == "2")
                {
                    sb.Append(" and (dateName(year,a.RejectDate)+'年-'+dateName(month,a.RejectDate)+'月')='");
                    sb.Append(DateValue);
                    sb.Append("'");
                }
                else
                {
                    sb.Append("and (dateName(year,a.RejectDate)+'年-'+dateName(week,a.RejectDate)+'周')='");
                    sb.Append(DateValue);
                    sb.Append("'");
                }
            }


            return SqlHelper.CreateSqlByPageExcuteSqlArr(sb.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
        }

        /// <summary>
        /// 退货物品走势
        /// </summary>
        public static DataTable GetRejectByProductTrend(string ProviderID, string DeptID, string BeginDate, string EndDate, string DateType, string StatType, string ProductID)
        {
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string SearchField = "";
            if (DateType == "1")
            {
                SearchField = "dateName(year,a.RejectDate)+'年'";
            }
            else if (DateType == "2")
            {
                SearchField = "dateName(year,a.RejectDate)+'年-'+dateName(month,a.RejectDate)+'月'";
            }
            else
            {
                SearchField = "dateName(year,a.RejectDate)+'年-'+dateName(week,a.RejectDate)+'周'";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT  ");
            sb.Append(SearchField);
            sb.Append(" as Name,");

            if (StatType == "Num")
            {
                sb.Append(" sum(isnull(BackCount,0)) as counts ");
            }
            else
            {
                sb.Append(" sum(isnull(b.TotalFee,0)*isnull(a.Rate,1)) as counts ");
            }
            sb.Append(" from officedba.PurchaseRejectDetail as b left join officedba.PurchaseReject as a  ");
            sb.Append(" on a.RejectNO=b.RejectNO and a.companyCD=b.companyCD left join officedba.productinfo as c on b.productId=c.Id  ");
            sb.Append(" WHERE 1=1 ");
            sb.Append("  AND A.CompanyCD='");
            sb.Append(strCompanyCD);
            sb.Append("' ");
            if (ProviderID != "")
            {
                sb.Append("and A.providerid=");
                sb.Append(ProviderID);
            }
            if (DeptID != "")
            {
                sb.Append(" and  A.deptId=");
                sb.Append(DeptID);
            }
            if (ProductID != "")
            {
                sb.Append(" and  b.productId=");
                sb.Append(ProductID);
            }
            if (BeginDate != "")
            {
                sb.Append("and a.RejectDate>=Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append("and a.RejectDate< DATEADD(day,1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by   ");
            sb.Append(SearchField);
            return SqlHelper.ExecuteSql(sb.ToString());
        }



        #endregion

    }
}

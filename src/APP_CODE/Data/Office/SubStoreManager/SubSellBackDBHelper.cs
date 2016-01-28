/***********************************************
 * 类作用：   门店管理事务层处理               *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/05/23                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using System.Data.SqlClient;
using XBase.Model.Office.SubStoreManager;
using XBase.Common;
using System.Data.SqlTypes;
using System.Collections;

namespace XBase.Data.Office.SubStoreManager
{
    /// <summary>
    /// 类名：SubSellBackDBHelper
    /// 描述：门店销售退货数据库层处理
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/05/23
    /// 最后修改时间：2009/06/04
    /// </summary>
    ///
    public class SubSellBackDBHelper
    {
        #region 绑定门店销售退货仓库
        public static DataTable GetdrpStorageID()
        {
            string sql = "select ID,StorageName from officedba.StorageInfo where CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and usedstatus=1";
            DataTable data = SqlHelper.ExecuteSql(sql);
            return data;
        }
        #endregion

        #region 插入门店销售退货单
        public static bool InsertSubSellBack(SubSellBackModel model, Hashtable htExtAttr, string DetailProductID, string DetailProductNo, string DetailProductName, string DetailUnitID, string DetailProductCount, string DetailBackCount, string DetailUnitPrice, string DetailTaxPrice, string DetailDiscount, string DetailTaxRate, string DetailTotalPrice, string DetailTotalFee, string DetailTotalTax, string DetailStorageID, string DetailFromBillID, string DetailFromLineNo, string DetailRemark, string DetailUsedUnitID, string DetailUsedUnitCount, string DetailUsedPrice, string DetailExRate, string DetailBatchNo, string length, out string ID)
        {

            ArrayList listADD = new ArrayList();
            bool result = false;
            ID = "0";

            #region  门店销售退货单添加SQL语句
            StringBuilder sqlArrive = new StringBuilder();


            sqlArrive.AppendLine("INSERT INTO officedba.SubSellBack");
            sqlArrive.AppendLine("(CompanyCD,DeptID,BackNo,Title,OrderID,SendMode,Seller,CustName,CustTel,CustMobile,CustAddr,BackDate,BackReason,");
            sqlArrive.AppendLine("BusiStatus,CurrencyType,Rate,TotalPrice,Tax,TotalFee,Discount,DiscountTotal,RealTotal,PayedTotal,WairPayTotal,");
            sqlArrive.AppendLine("isAddTax,CountTotal,Remark,Attachment,BillStatus,Creator,CreateDate,Confirmor,ConfirmDate,OutUserID,OutDate,");
            sqlArrive.AppendLine("ModifiedDate,ModifiedUserID,FromType)");
            sqlArrive.AppendLine("VALUES (@CompanyCD,@DeptID,@BackNo,@Title,@OrderID,@SendMode,@Seller,@CustName,@CustTel,@CustMobile,@CustAddr,@BackDate,@BackReason,");
            sqlArrive.AppendLine("@BusiStatus,@CurrencyType,@Rate,@TotalPrice,@Tax,@TotalFee,@Discount,@DiscountTotal,@RealTotal,@PayedTotal,@WairPayTotal,");
            sqlArrive.AppendLine("@isAddTax,@CountTotal,@Remark,@Attachment,@BillStatus,@Creator,@CreateDate,@Confirmor,@ConfirmDate,@OutUserID,@OutDate,");
            sqlArrive.AppendLine("getdate(),@ModifiedUserID,@FromType)");
            sqlArrive.AppendLine("set @ID=@@IDENTITY");

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@BackNo", model.BackNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@OrderID", model.OrderID));
            comm.Parameters.Add(SqlHelper.GetParameter("@SendMode", model.SendMode));
            comm.Parameters.Add(SqlHelper.GetParameter("@Seller", model.Seller));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustName", model.CustName));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustTel", model.CustTel));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustMobile", model.CustMobile));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustAddr", model.CustAddr));
            comm.Parameters.Add(SqlHelper.GetParameter("@BackDate", model.BackDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.BackDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@BackReason", model.BackReason));
            comm.Parameters.Add(SqlHelper.GetParameter("@BusiStatus", model.BusiStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", model.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@Rate", model.Rate));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", model.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@Tax", model.Tax));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalFee", model.TotalFee));
            comm.Parameters.Add(SqlHelper.GetParameter("@Discount", model.Discount));
            comm.Parameters.Add(SqlHelper.GetParameter("@DiscountTotal", model.DiscountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@RealTotal", model.RealTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@PayedTotal", model.PayedTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@WairPayTotal", model.WairPayTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@isAddTax", model.isAddTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@Attachment", model.Attachment));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.CreateDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.ConfirmDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@OutUserID", model.OutUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@OutDate", model.OutDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.OutDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@SttlUserID", model.SttlUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
            comm.CommandText = sqlArrive.ToString();


            listADD.Add(comm);

            // 更新扩展属性
            SqlCommand commExtAttr = UpdateExtAttr(model, htExtAttr);
            if (commExtAttr != null)
            {
                listADD.Add(commExtAttr);
            }
            #endregion


            try
            {
                #region 门店销售退货单明细
                int lengs = Convert.ToInt32(length);
                if (lengs > 0)
                {
                    string[] ProductID = DetailProductID.Split(',');
                    string[] ProductNo = DetailProductNo.Split(',');
                    string[] ProductName = DetailProductName.Split(',');
                    string[] UnitID = DetailUnitID.Split(',');
                    string[] ProductCount = DetailProductCount.Split(',');
                    string[] BackCount = DetailBackCount.Split(',');
                    string[] UnitPrice = DetailUnitPrice.Split(',');
                    string[] TaxPrice = DetailTaxPrice.Split(',');
                    string[] Discount = DetailDiscount.Split(',');
                    string[] TaxRate = DetailTaxRate.Split(',');
                    string[] TotalPrice = DetailTotalPrice.Split(',');
                    string[] TotalFee = DetailTotalFee.Split(',');
                    string[] TotalTax = DetailTotalTax.Split(',');
                    string[] StorageID = DetailStorageID.Split(',');
                    string[] FromBillID = DetailFromBillID.Split(',');
                    string[] FromLineNo = DetailFromLineNo.Split(',');
                    string[] Remark = DetailRemark.Split(',');
                    string[] UsedUnitID = DetailUsedUnitID.Split(',');
                    string[] UsedUnitCount = DetailUsedUnitCount.Split(',');
                    string[] UsedPrice = DetailUsedPrice.Split(',');
                    string[] ExRate = DetailExRate.Split(',');
                    string[] BatchNo = DetailBatchNo.Split(',');
                    for (int i = 0; i < lengs; i++)
                    {
                        System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                        SqlCommand comms = new SqlCommand();
                        cmdsql.AppendLine("INSERT INTO officedba.SubSellBackDetail");
                        cmdsql.AppendLine("(CompanyCD,");
                        cmdsql.AppendLine("DeptID,");
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("OrderID,");
                            }
                        }
                        cmdsql.AppendLine("SortNo,");
                        cmdsql.AppendLine("BackNo,");
                        cmdsql.AppendLine("ProductID,");
                        if (StorageID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(StorageID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("StorageID,");
                            }
                        }
                        cmdsql.AppendLine("BackCount,");
                        cmdsql.AppendLine("UnitID,");
                        cmdsql.AppendLine("UnitPrice,");
                        cmdsql.AppendLine("TaxPrice,");
                        cmdsql.AppendLine("Discount,");
                        cmdsql.AppendLine("TaxRate,");
                        cmdsql.AppendLine("TotalFee,");
                        cmdsql.AppendLine("TotalPrice,");
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("Remark,");
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("FromLineNo,");
                            }
                        }
                        cmdsql.AppendLine("UsedUnitID,");
                        cmdsql.AppendLine("UsedUnitCount,");
                        cmdsql.AppendLine("UsedPrice,");
                        cmdsql.AppendLine("ExRate,");
                        cmdsql.AppendLine("BatchNo,");
                        cmdsql.AppendLine("TotalTax)");


                        cmdsql.AppendLine(" Values(@CompanyCD");
                        cmdsql.AppendLine("            ,@DeptID");
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@OrderID");
                            }
                        }
                        cmdsql.AppendLine("            ,@SortNo");
                        cmdsql.AppendLine("            ,@BackNo");
                        cmdsql.AppendLine("            ,@ProductID");
                        if (StorageID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(StorageID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@StorageID");
                            }
                        }
                        cmdsql.AppendLine("            ,@BackCount");
                        cmdsql.AppendLine("            ,@UnitID");
                        cmdsql.AppendLine("            ,@UnitPrice");
                        cmdsql.AppendLine("            ,@TaxPrice");
                        cmdsql.AppendLine("            ,@Discount");
                        cmdsql.AppendLine("            ,@TaxRate");
                        cmdsql.AppendLine("            ,@TotalFee");
                        cmdsql.AppendLine("            ,@TotalPrice");
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@Remark");
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@FromLineNo");
                            }
                        }
                        cmdsql.AppendLine("            ,@UsedUnitID");
                        cmdsql.AppendLine("            ,@UsedUnitCount");
                        cmdsql.AppendLine("            ,@UsedPrice");
                        cmdsql.AppendLine("            ,@ExRate");
                        cmdsql.AppendLine("            ,@BatchNo");
                        cmdsql.AppendLine("            ,@TotalTax)");


                        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        comms.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@OrderID", FromBillID[i].ToString()));
                            }
                        }
                        comms.Parameters.Add(SqlHelper.GetParameter("@SortNo", i + 1));
                        comms.Parameters.Add(SqlHelper.GetParameter("@BackNo", model.BackNo));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID[i].ToString()));
                        if (StorageID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(StorageID[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@StorageID", StorageID[i].ToString()));
                            }
                        }
                        comms.Parameters.Add(SqlHelper.GetParameter("@BackCount", BackCount[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UnitID", UnitID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UnitPrice", UnitPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxPrice", TaxPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@Discount", Discount[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxRate", TaxRate[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalFee", TotalFee[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", TotalPrice[i].ToString()));
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@Remark", Remark[i].ToString()));
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", FromLineNo[i].ToString()));
                            }
                        }
                        comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", UsedUnitID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", UsedUnitCount[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", UsedPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ExRate", ExRate[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@BatchNo", BatchNo[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalTax", TotalTax[i].ToString()));
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

        /// <summary>
        /// 扩展属性更新命令
        /// </summary>
        /// <param name="model">分店销售退货单</param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static SqlCommand UpdateExtAttr(SubSellBackModel model, Hashtable htExtAttr)
        {
            SqlCommand sqlcomm = new SqlCommand();
            if (htExtAttr == null || htExtAttr.Count < 1)
            {// 没有属性需要修改
                return null;
            }

            StringBuilder sb = new StringBuilder(" UPDATE officedba.SubSellBack SET ");
            foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
            {
                sb.AppendFormat(" {0}=@{0},", de.Key.ToString());
                sqlcomm.Parameters.Add(SqlHelper.GetParameter(String.Format("@{0}", de.Key.ToString()), de.Value));
            }
            string strSql = sb.ToString();
            strSql = strSql.TrimEnd(',');
            strSql += " WHERE CompanyCD = @CompanyCD  AND BackNo = @BackNo ";
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@BackNo", model.BackNo));
            sqlcomm.CommandText = strSql;

            return sqlcomm;
        }
        #endregion

        #region 修改门店销售退货单
        public static bool UpdateSubSellBack(SubSellBackModel model, Hashtable htExtAttr, string DetailProductID, string DetailProductNo, string DetailProductName, string DetailUnitID, string DetailProductCount, string DetailBackCount, string DetailUnitPrice, string DetailTaxPrice, string DetailDiscount, string DetailTaxRate, string DetailTotalPrice, string DetailTotalFee, string DetailTotalTax, string DetailStorageID, string DetailFromBillID, string DetailFromLineNo, string DetailRemark, string DetailUsedUnitID, string DetailUsedUnitCount, string DetailUsedPrice, string DetailExRate, string DetailBatchNo, string length, string no)
        {
            if (model.ID <= 0)
            {
                return false;
            }
            ArrayList listADD = new ArrayList();
            bool result = false;

            #region  修改门店销售退货单
            StringBuilder sqlArrive = new StringBuilder();

            sqlArrive.AppendLine("Update  Officedba.SubSellBack set CompanyCD=@CompanyCD,");
            sqlArrive.AppendLine("DeptID=@DeptID,BackNo=@BackNo,Title=@Title,OrderID=@OrderID,SendMode=@SendMode,Seller=@Seller,CustName=@CustName,");
            sqlArrive.AppendLine("CustTel=@CustTel,CustMobile=@CustMobile,CustAddr=@CustAddr,BackDate=@BackDate,BackReason=@BackReason,");
            sqlArrive.AppendLine("BusiStatus=@BusiStatus,CurrencyType=@CurrencyType,Rate=@Rate,TotalPrice=@TotalPrice,Tax=@Tax,TotalFee=@TotalFee,");
            sqlArrive.AppendLine("Discount=@Discount,DiscountTotal=@DiscountTotal,RealTotal=@RealTotal,PayedTotal=@PayedTotal,WairPayTotal=@WairPayTotal,");
            sqlArrive.AppendLine("isAddTax=@isAddTax,CountTotal=@CountTotal,Remark=@Remark,Attachment=@Attachment,BillStatus=@BillStatus,OutUserID=@OutUserID,");
            sqlArrive.AppendLine("OutDate=@OutDate,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID,FromType=@FromType where CompanyCD=@CompanyCD and ID=@ID");


            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@BackNo", model.BackNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@OrderID", model.OrderID));
            comm.Parameters.Add(SqlHelper.GetParameter("@SendMode", model.SendMode));
            comm.Parameters.Add(SqlHelper.GetParameter("@Seller", model.Seller));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustName", model.CustName));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustTel", model.CustTel));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustMobile", model.CustMobile));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustAddr", model.CustAddr));
            comm.Parameters.Add(SqlHelper.GetParameter("@BackDate", model.BackDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.BackDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@BackReason", model.BackReason));
            comm.Parameters.Add(SqlHelper.GetParameter("@BusiStatus", model.BusiStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", model.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@Rate", model.Rate));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", model.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@Tax", model.Tax));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalFee", model.TotalFee));
            comm.Parameters.Add(SqlHelper.GetParameter("@Discount", model.Discount));
            comm.Parameters.Add(SqlHelper.GetParameter("@DiscountTotal", model.DiscountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@RealTotal", model.RealTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@PayedTotal", model.PayedTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@WairPayTotal", model.WairPayTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@isAddTax", model.isAddTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@Attachment", model.Attachment));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@OutUserID", model.OutUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@OutDate", model.OutDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.OutDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@SttlUserID", model.SttlUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.CommandText = sqlArrive.ToString();


            listADD.Add(comm);

            // 更新扩展属性
            SqlCommand commExtAttr = UpdateExtAttr(model, htExtAttr);
            if (commExtAttr != null)
            {
                listADD.Add(commExtAttr);
            }
            #endregion

            #region 删除门店销售退货单明细
            System.Text.StringBuilder cmdddetail = new System.Text.StringBuilder();
            cmdddetail.AppendLine("DELETE  FROM officedba.SubSellBackDetail WHERE  CompanyCD=@CompanyCD and BackNo=@BackNo");
            SqlCommand comn = new SqlCommand();
            comn.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comn.Parameters.Add(SqlHelper.GetParameter("@BackNo", model.BackNo));
            comn.CommandText = cmdddetail.ToString();
            listADD.Add(comn);
            #endregion

            try
            {
                #region 门店销售退货单明细
                int lengs = Convert.ToInt32(length);
                if (lengs > 0)
                {
                    string[] ProductID = DetailProductID.Split(',');
                    string[] ProductNo = DetailProductNo.Split(',');
                    string[] ProductName = DetailProductName.Split(',');
                    string[] UnitID = DetailUnitID.Split(',');
                    string[] ProductCount = DetailProductCount.Split(',');
                    string[] BackCount = DetailBackCount.Split(',');
                    string[] UnitPrice = DetailUnitPrice.Split(',');
                    string[] TaxPrice = DetailTaxPrice.Split(',');
                    string[] Discount = DetailDiscount.Split(',');
                    string[] TaxRate = DetailTaxRate.Split(',');
                    string[] TotalPrice = DetailTotalPrice.Split(',');
                    string[] TotalFee = DetailTotalFee.Split(',');
                    string[] TotalTax = DetailTotalTax.Split(',');
                    string[] StorageID = DetailStorageID.Split(',');
                    string[] FromBillID = DetailFromBillID.Split(',');
                    string[] FromLineNo = DetailFromLineNo.Split(',');
                    string[] Remark = DetailRemark.Split(',');
                    string[] UsedUnitID = DetailUsedUnitID.Split(',');
                    string[] UsedUnitCount = DetailUsedUnitCount.Split(',');
                    string[] UsedPrice = DetailUsedPrice.Split(',');
                    string[] ExRate = DetailExRate.Split(',');
                    string[] BatchNo = DetailBatchNo.Split(',');

                    for (int i = 0; i < lengs; i++)
                    {
                        System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                        SqlCommand comms = new SqlCommand();
                        cmdsql.AppendLine("INSERT INTO officedba.SubSellBackDetail");
                        cmdsql.AppendLine("(CompanyCD,");
                        cmdsql.AppendLine("DeptID,");
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("OrderID,");
                            }
                        }
                        cmdsql.AppendLine("SortNo,");
                        cmdsql.AppendLine("BackNo,");
                        cmdsql.AppendLine("ProductID,");
                        if (StorageID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(StorageID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("StorageID,");
                            }
                        }
                        cmdsql.AppendLine("BackCount,");
                        cmdsql.AppendLine("UnitID,");
                        cmdsql.AppendLine("UnitPrice,");
                        cmdsql.AppendLine("TaxPrice,");
                        cmdsql.AppendLine("Discount,");
                        cmdsql.AppendLine("TaxRate,");
                        cmdsql.AppendLine("TotalFee,");
                        cmdsql.AppendLine("TotalPrice,");
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("Remark,");
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("FromLineNo,");
                            }
                        }
                        cmdsql.AppendLine("UsedUnitID,");
                        cmdsql.AppendLine("UsedUnitCount,");
                        cmdsql.AppendLine("UsedPrice,");
                        cmdsql.AppendLine("ExRate,");
                        cmdsql.AppendLine("BatchNo,");
                        cmdsql.AppendLine("TotalTax)");


                        cmdsql.AppendLine(" Values(@CompanyCD");
                        cmdsql.AppendLine("            ,@DeptID");
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@OrderID");
                            }
                        }
                        cmdsql.AppendLine("            ,@SortNo");
                        cmdsql.AppendLine("            ,@BackNo");
                        cmdsql.AppendLine("            ,@ProductID");
                        if (StorageID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(StorageID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@StorageID");
                            }
                        }
                        cmdsql.AppendLine("            ,@BackCount");
                        cmdsql.AppendLine("            ,@UnitID");
                        cmdsql.AppendLine("            ,@UnitPrice");
                        cmdsql.AppendLine("            ,@TaxPrice");
                        cmdsql.AppendLine("            ,@Discount");
                        cmdsql.AppendLine("            ,@TaxRate");
                        cmdsql.AppendLine("            ,@TotalFee");
                        cmdsql.AppendLine("            ,@TotalPrice");
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@Remark");
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@FromLineNo");
                            }
                        }
                        cmdsql.AppendLine("            ,@UsedUnitID");
                        cmdsql.AppendLine("            ,@UsedUnitCount");
                        cmdsql.AppendLine("            ,@UsedPrice");
                        cmdsql.AppendLine("            ,@ExRate");
                        cmdsql.AppendLine("            ,@BatchNo");
                        cmdsql.AppendLine("            ,@TotalTax)");


                        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        comms.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@OrderID", FromBillID[i].ToString()));
                            }
                        }
                        comms.Parameters.Add(SqlHelper.GetParameter("@SortNo", i + 1));
                        comms.Parameters.Add(SqlHelper.GetParameter("@BackNo", model.BackNo));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID[i].ToString()));
                        if (StorageID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(StorageID[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@StorageID", StorageID[i].ToString()));
                            }
                        }
                        comms.Parameters.Add(SqlHelper.GetParameter("@BackCount", BackCount[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UnitID", UnitID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UnitPrice", UnitPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxPrice", TaxPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@Discount", Discount[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxRate", TaxRate[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalFee", TotalFee[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", TotalPrice[i].ToString()));
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@Remark", Remark[i].ToString()));
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", FromLineNo[i].ToString()));
                            }
                        }
                        comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", UsedUnitID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", UsedUnitCount[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", UsedPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ExRate", ExRate[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@BatchNo", BatchNo[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalTax", TotalTax[i].ToString()));
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

        #region 确认门店销售退货单
        public static bool ConfirmSubSellBack(SubSellBackModel model, string DetailBackCount, string DetailFromBillNo, string DetailFromLineNo, string length)
        {
            #region 确认单据
            ArrayList listADD = new ArrayList();
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.SubSellBack set ");
            strSql.AppendLine(" Confirmor=@Confirmor");
            strSql.AppendLine(" ,BillStatus=2");
            strSql.AppendLine(" ,BusiStatus=2");
            strSql.AppendLine(" ,ConfirmDate=getdate()");
            strSql.AppendLine(" where");
            strSql.AppendLine(" CompanyCD=@CompanyCD");
            strSql.AppendLine(" and ID=@ID");
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.CommandText = strSql.ToString();

            listADD.Add(comm);
            #endregion

            #region 确认时回填门店销售订单中的退货数量
            try
            {

                int lengs = Convert.ToInt32(length);
                if (lengs > 0)
                {
                    string[] BackCount = DetailBackCount.Split(',');
                    string[] FromBillNo = DetailFromBillNo.Split(',');
                    string[] FromLineNo = DetailFromLineNo.Split(',');
                    //SqlCommand comms = null;
                    //System.Text.StringBuilder cmdsql=null;
                    for (int i = 0; i < lengs; i++)
                    {
                        System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                        SqlCommand comms = new SqlCommand();
                        cmdsql.AppendLine("Update  Officedba.SubSellOrderDetail set BackCount=isnull(BackCount,0)+@BackCount");
                        cmdsql.AppendLine(" where CompanyCD=@CompanyCD and OrderNo=@FromBillNo and SortNo=@FromLineNo");

                        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        comms.Parameters.Add(SqlHelper.GetParameter("@BackCount", BackCount[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", FromBillNo[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", FromLineNo[i].ToString()));
                        comms.CommandText = cmdsql.ToString();
                        listADD.Add(comms);
                    }
                }



                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    //ID = comm.Parameters["@ID"].Value.ToString();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion

        }
        #endregion

        #region 取消确认门店销售退货单
        public static bool QxConfirmSubSellBack(SubSellBackModel model, string DetailBackCount, string DetailFromBillNo, string DetailFromLineNo, string length)
        {
            #region 确认单据
            ArrayList listADD = new ArrayList();
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.SubSellBack set ");
            strSql.AppendLine(" Confirmor=@Confirmor");
            strSql.AppendLine(" ,BillStatus=1");
            strSql.AppendLine(" ,BusiStatus=1");
            strSql.AppendLine(" ,ConfirmDate=@ConfirmDate");
            strSql.AppendLine(" where");
            strSql.AppendLine(" CompanyCD=@CompanyCD");
            strSql.AppendLine(" and ID=@ID");
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.ConfirmDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.CommandText = strSql.ToString();

            listADD.Add(comm);
            #endregion

            #region 确认时回填门店销售订单中的退货数量
            try
            {

                int lengs = Convert.ToInt32(length);
                if (lengs > 0)
                {
                    string[] BackCount = DetailBackCount.Split(',');
                    string[] FromBillNo = DetailFromBillNo.Split(',');
                    string[] FromLineNo = DetailFromLineNo.Split(',');
                    for (int i = 0; i < lengs; i++)
                    {
                        System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                        SqlCommand comms = new SqlCommand();
                        cmdsql.AppendLine("Update  Officedba.SubSellOrderDetail set BackCount=isnull(BackCount,0)-@BackCount");
                        cmdsql.AppendLine(" where CompanyCD=@CompanyCD and OrderNo=@FromBillNo and SortNo=@FromLineNo");

                        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        comms.Parameters.Add(SqlHelper.GetParameter("@BackCount", BackCount[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", FromBillNo[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", FromLineNo[i].ToString()));
                        comms.CommandText = cmdsql.ToString();
                        listADD.Add(comms);
                    }
                }



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
            #endregion

        }
        #endregion

        #region 入库门店销售退货单
        public static bool RukuSubSellBack(SubSellBackModel model, string DetailBackCount, string DetailStorageID, string DetailProductID, string DetailUnitPrice, string DetailBatchNo, string length)
        {
            #region 入库单据
            ArrayList listADD = new ArrayList();
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            if (model.PayedTotal != model.WairPayTotal)
            {
                strSql.AppendLine("update officedba.SubSellBack set ");
                strSql.AppendLine(" InUserID=@InUserID");
                strSql.AppendLine(" ,InDate=@InDate");
                strSql.AppendLine(" ,BillStatus=@BillStatus");
                strSql.AppendLine(" ,BusiStatus=@BusiStatus");
                strSql.AppendLine(" ,PayedTotal=@PayedTotal");
                strSql.AppendLine(" ,WairPayTotal=@WairPayTotal");
                strSql.AppendLine(" where");
                strSql.AppendLine(" CompanyCD=@CompanyCD");
                strSql.AppendLine(" and ID=@ID");
                SqlCommand comm = new SqlCommand();
                comm.Parameters.Add(SqlHelper.GetParameter("@InUserID", model.InUserID));
                comm.Parameters.Add(SqlHelper.GetParameter("@InDate", model.InDate == null
                                                            ? SqlDateTime.Null
                                                            : SqlDateTime.Parse(model.InDate.ToString())));
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
                comm.Parameters.Add(SqlHelper.GetParameter("@BusiStatus", model.BusiStatus));
                comm.Parameters.Add(SqlHelper.GetParameter("@PayedTotal", model.PayedTotal));
                comm.Parameters.Add(SqlHelper.GetParameter("@WairPayTotal", model.WairPayTotal));
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                comm.CommandText = strSql.ToString();

                listADD.Add(comm);
            }
            else
            {
                strSql.AppendLine("update officedba.SubSellBack set ");
                strSql.AppendLine(" InUserID=@InUserID");
                strSql.AppendLine(" ,InDate=@InDate");
                strSql.AppendLine(" ,Closer=@Closer");
                strSql.AppendLine(" ,CloseDate=getdate()");
                strSql.AppendLine(" ,BillStatus=@BillStatus");
                strSql.AppendLine(" ,BusiStatus=@BusiStatus");
                strSql.AppendLine(" ,PayedTotal=@PayedTotal");
                strSql.AppendLine(" ,WairPayTotal=@WairPayTotal");
                strSql.AppendLine(" where");
                strSql.AppendLine(" CompanyCD=@CompanyCD");
                strSql.AppendLine(" and ID=@ID");
                SqlCommand comm = new SqlCommand();
                comm.Parameters.Add(SqlHelper.GetParameter("@InUserID", model.InUserID));
                comm.Parameters.Add(SqlHelper.GetParameter("@InDate", model.InDate == null
                                                            ? SqlDateTime.Null
                                                            : SqlDateTime.Parse(model.InDate.ToString())));
                comm.Parameters.Add(SqlHelper.GetParameter("@Closer", model.Closer));
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
                comm.Parameters.Add(SqlHelper.GetParameter("@BusiStatus", model.BusiStatus));
                comm.Parameters.Add(SqlHelper.GetParameter("@PayedTotal", model.PayedTotal));
                comm.Parameters.Add(SqlHelper.GetParameter("@WairPayTotal", model.WairPayTotal));
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                comm.CommandText = strSql.ToString();

                listADD.Add(comm);
            }
            #endregion

            #region 入库时回写响应模块的退货数量
            try
            {

                int id = 0;
                decimal count = 0m;
                if (model.SendMode == "1")
                {//分店发货，更新分店的存量表中的现有存量,增加相应物品的退货数量
                    int lengs = Convert.ToInt32(length);
                    if (lengs > 0)
                    {
                        string[] BackCount = DetailBackCount.Split(',');
                        string[] ProductID = DetailProductID.Split(',');
                        string[] UnitPrice = DetailUnitPrice.Split(',');
                        string[] BatchNo = DetailBatchNo.Split(',');
                        for (int i = 0; i < lengs; i++)
                        {
                            #region 添加门店库存流水帐
                            SubStorageAccountModel aModel = new SubStorageAccountModel();
                            aModel.BatchNo = BatchNo[i];
                            aModel.BillNo = model.BackNo;
                            aModel.BillType = 4;
                            aModel.CompanyCD = model.CompanyCD;
                            aModel.Creator = model.Closer;
                            aModel.DeptID = model.DeptID;
                            aModel.HappenDate = DateTime.Now;
                            if (int.TryParse(ProductID[i], out id))
                            {
                                aModel.ProductID = id;
                            }
                            if (decimal.TryParse(UnitPrice[i], out count))
                            {
                                aModel.Price = count;
                            }
                            if (decimal.TryParse(BackCount[i], out count))
                            {
                                aModel.HappenCount = count;
                            }
                            aModel.PageUrl = model.Remark;
                            listADD.Add(XBase.Data.Office.SubStoreManager.SubStorageAccountDBHelper.GetCountAndInsertCommand(aModel));
                            #endregion

                            // 更新库存
                            listADD.Add(XBase.Data.Office.LogisticsDistributionManager.StorageProductQueryDBHelper.UpdateProductCount(model.CompanyCD
                                        , ProductID[i], model.DeptID.ToString(), BatchNo[i], count));
                        }
                    }
                }
                else
                {//总部发货，更新总部的分仓存量表中的现有存量,增加相应物品的退货数量
                    int lengs = Convert.ToInt32(length);
                    if (lengs > 0)
                    {
                        string[] BackCount = DetailBackCount.Split(',');
                        string[] StorageID = DetailStorageID.Split(',');
                        string[] ProductID = DetailProductID.Split(',');
                        string[] UnitPrice = DetailUnitPrice.Split(',');
                        string[] BatchNo = DetailBatchNo.Split(',');
                        for (int i = 0; i < lengs; i++)
                        {
                            #region 添加库存流水帐

                            XBase.Model.Office.StorageManager.StorageAccountModel sModel = new XBase.Model.Office.StorageManager.StorageAccountModel();
                            sModel.BatchNo = BatchNo[i];
                            sModel.BillNo = model.BackNo;
                            sModel.BillType = 22;
                            sModel.CompanyCD = model.CompanyCD;
                            sModel.Creator = model.Closer;
                            if (int.TryParse(StorageID[i], out id))
                            {
                                sModel.StorageID = id;
                            }
                            if (int.TryParse(ProductID[i], out id))
                            {
                                sModel.ProductID = id;
                            }
                            if (decimal.TryParse(UnitPrice[i], out count))
                            {
                                sModel.Price = count;
                            }
                            if (decimal.TryParse(BackCount[i], out count))
                            {
                                sModel.HappenCount = count;
                            }
                            sModel.HappenDate = DateTime.Now;
                            sModel.PageUrl = model.Remark;
                            listADD.Add(XBase.Data.Office.StorageManager.StorageAccountDBHelper.InsertStorageAccountCommand(sModel, "0"));

                            #endregion

                            // 更新库存
                            listADD.Add(XBase.Data.Office.StorageManager.StorageSearchDBHelper.UpdateProductCount(model.CompanyCD, ProductID[i], StorageID[i], BatchNo[i], count));

                        }
                    }
                }

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
            #endregion

        }
        #endregion

        #region 结算门店销售退货单
        public static bool JiesuanubSellBack(SubSellBackModel model)
        {
            #region 结算单据
            ArrayList listADD = new ArrayList();
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            if (model.PayedTotal != model.WairPayTotal)
            {
                strSql.AppendLine("update officedba.SubSellBack set ");
                strSql.AppendLine(" SttlUserID=@SttlUserID");
                strSql.AppendLine(" ,SttlDate=@SttlDate");
                strSql.AppendLine(" ,BillStatus=@BillStatus");
                strSql.AppendLine(" ,BusiStatus=@BusiStatus");
                strSql.AppendLine(" ,PayedTotal=@PayedTotal");
                strSql.AppendLine(" ,WairPayTotal=@WairPayTotal");
                strSql.AppendLine(" where");
                strSql.AppendLine(" CompanyCD=@CompanyCD");
                strSql.AppendLine(" and ID=@ID");
                SqlCommand comm = new SqlCommand();
                comm.Parameters.Add(SqlHelper.GetParameter("@SttlUserID", model.SttlUserID));
                comm.Parameters.Add(SqlHelper.GetParameter("@SttlDate", model.SttlDate == null
                                                            ? SqlDateTime.Null
                                                            : SqlDateTime.Parse(model.SttlDate.ToString())));
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
                comm.Parameters.Add(SqlHelper.GetParameter("@BusiStatus", model.BusiStatus));
                comm.Parameters.Add(SqlHelper.GetParameter("@PayedTotal", model.PayedTotal));
                comm.Parameters.Add(SqlHelper.GetParameter("@WairPayTotal", model.WairPayTotal));
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                comm.CommandText = strSql.ToString();

                listADD.Add(comm);
            }
            else
            {
                strSql.AppendLine("update officedba.SubSellBack set ");
                strSql.AppendLine(" SttlUserID=@SttlUserID");
                strSql.AppendLine(" ,SttlDate=@SttlDate");
                strSql.AppendLine(" ,BillStatus=@BillStatus");
                strSql.AppendLine(" ,BusiStatus=@BusiStatus");
                strSql.AppendLine(" ,PayedTotal=@PayedTotal");
                strSql.AppendLine(" ,WairPayTotal=@WairPayTotal");
                strSql.AppendLine(" ,Closer=@Closer");
                strSql.AppendLine(" ,CloseDate=getdate()");
                strSql.AppendLine(" where");
                strSql.AppendLine(" CompanyCD=@CompanyCD");
                strSql.AppendLine(" and ID=@ID");
                SqlCommand comm = new SqlCommand();
                comm.Parameters.Add(SqlHelper.GetParameter("@SttlUserID", model.SttlUserID));
                comm.Parameters.Add(SqlHelper.GetParameter("@SttlDate", model.SttlDate == null
                                                            ? SqlDateTime.Null
                                                            : SqlDateTime.Parse(model.SttlDate.ToString())));
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
                comm.Parameters.Add(SqlHelper.GetParameter("@BusiStatus", model.BusiStatus));
                comm.Parameters.Add(SqlHelper.GetParameter("@PayedTotal", model.PayedTotal));
                comm.Parameters.Add(SqlHelper.GetParameter("@WairPayTotal", model.WairPayTotal));
                comm.Parameters.Add(SqlHelper.GetParameter("@Closer", model.Closer));
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                comm.CommandText = strSql.ToString();

                listADD.Add(comm);
            }
            #endregion


            try
            {
                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    //ID = comm.Parameters["@ID"].Value.ToString();
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

        #region 选择门店销售订单
        public static DataTable GetSubSellBackDetail(int DeptID, string OrderNo, string SendMode, string CompanyCD, int CurrencyTypeID, string Rate)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID                                                                              ");
            sql.AppendLine("      ,isnull(A.OrderNo,'') AS   OrderNo                                                 ");
            sql.AppendLine("      ,A.SendMode                                                                       ");
            sql.AppendLine("      ,case A.SendMode when '1' then '分店发货' when '2' then '总部发货' end AS SendModeName ");
            sql.AppendLine("      ,isnull(CONVERT(varchar(100),A.OutDate,120),'') AS OutDate                           ");
            sql.AppendLine("      ,isnull(A.OutUserID,0) AS  OutUserID                                                 ");
            sql.AppendLine("      ,isnull(B.EmployeeName,'') AS OutUserName                                            ");
            sql.AppendLine("      ,isnull(A.CustName,'') AS CustName                                                   ");
            sql.AppendLine("      ,isnull(A.CustTel,'') AS CustTel                                                     ");
            sql.AppendLine("      ,isnull(A.CustMobile,'') AS CustMobile                                                ");
            sql.AppendLine("      ,isnull(A.CustAddr,'') AS CustAddr                                                    ");
            sql.AppendLine("      ,A.CurrencyType,isnull(C.CurrencyName,'') AS  CurrencyTypeName,A.Rate                ");
            sql.AppendLine("FROM officedba.SubSellOrder AS A                                                            ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS B ON A.CompanyCD = B.CompanyCD AND A.OutUserID = B.ID    ");
            sql.AppendLine("LEFT JOIN officedba.CurrencyTypeSetting AS C ON A.CompanyCD = C.CompanyCD AND A.CurrencyType = C.ID ");

            sql.AppendLine("WHERE A.BillStatus <> 1 AND A.DeptID =" + DeptID + " AND A.CompanyCD ='" + CompanyCD + "' ");
            if (OrderNo != null && OrderNo != "")
            {
                sql.AppendLine(" AND A.OrderNo like '%" + OrderNo + "%'    ");
            }
            if (SendMode != null && SendMode != "")
            {
                sql.AppendLine(" AND A.SendMode like '%" + SendMode + "%'    ");
            }
            if (CurrencyTypeID > 0)
            {
                sql.AppendLine(" AND A.CurrencyType = " + CurrencyTypeID + " ");
            }
            if (Rate != "0")
            {
                sql.AppendLine(" AND A.Rate = " + Rate + "   ");
            }
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 选择门店销售订单明细来源
        public static DataTable GetSubSellBackDetailUC(string OrderNo, int CurrencyTypeID, string Rate, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID,A.UsedUnitID,A.UsedUnitCount,A.UsedPrice,A.ExRate                                                                              ");
            sql.AppendLine("      ,A.DeptID,isnull(B.DeptName,'') AS DeptName,A.OrderNo,isnull(A.SortNo,0) AS FromLineNo");
            sql.AppendLine("      ,isnull(A.OrderNo,'') AS FromBillNo, isnull(C.ID,0) AS  FromBillID,A.ProductID  ");
            sql.AppendLine("      ,isnull(D.ProdNo,'') AS ProductNo,isnull(D.ProductName,'')AS  ProductName,isnull(D.Specification,'') AS standard");
            sql.AppendLine("      ,A.UnitID, isnull(E.CodeName,'') AS UnitName,isnull(A.UnitPrice,0) AS UnitPrice ");
            sql.AppendLine("      ,isnull(A.TaxPrice,0) AS  TaxPrice,isnull(A.Discount,0) AS Discount");
            sql.AppendLine("      ,isnull(A.TaxRate,0) AS  TaxRate,isnull(A.StorageID,0) AS StorageID");
            sql.AppendLine("      ,C.CurrencyType,C.Rate, isnull(CU.CodeName,'') AS UsedUnitName  ");
            sql.AppendLine("      ,isnull(F.StorageName,'') AS StorageName ,isnull(A.ProductCount,0) AS ProductCount,isnull(A.Remark,'') AS Remark,isnull(A.BackCount,0) AS YBackCount,ISNULL(A.BatchNo,'') AS BatchNo ");

            sql.AppendLine("FROM officedba.SubSellOrderDetail AS A                                                      ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS B ON A.CompanyCD = B.CompanyCD AND A.DeptID = B.ID          ");
            sql.AppendLine("LEFT JOIN officedba.SubSellOrder AS C ON A.CompanyCD = C.CompanyCD AND A.OrderNo = C.OrderNo ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS D ON A.CompanyCD = D.CompanyCD AND A.ProductID = D.ID    ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS E ON A.CompanyCD = E.CompanyCD AND A.UnitID = E.ID      ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS CU ON A.CompanyCD = CU.CompanyCD AND A.UsedUnitID = CU.ID      ");
            sql.AppendLine("LEFT JOIN officedba.StorageInfo AS F ON A.CompanyCD = F.CompanyCD AND A.StorageID = F.ID      ");
            sql.AppendLine("LEFT JOIN officedba.SubSellOrder AS G ON A.CompanyCD = G.CompanyCD AND A.OrderNo = G.OrderNo      ");

            sql.AppendLine("WHERE G.BillStatus <> 1 AND A.OrderNo='" + OrderNo + "' AND A.CompanyCD ='" + CompanyCD + "'   ");
            sql.AppendLine(" AND C.CurrencyType = " + CurrencyTypeID + " AND C.Rate = " + Rate + "  ");
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 查询门店退货列表所需数据
        public static DataTable SelectSubSellBack(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string BackNo, string Title, string OrderID, string CustName, string CustTel, string DeptID, string Seller, string BusiStatus, string BillStatus, string CustAddr, string EFIndex, string EFDesc)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.BackNo ,isnull(A.Title,'') AS Title ,isnull(A.OrderID,0) AS OrderID,isnull(B.OrderNo,'') AS OrderNo,isnull(A.CustName,'') AS CustName ");
            sql.AppendLine("      ,isnull(A.CustTel,'') AS CustTel,isnull(A.CustAddr,'') AS CustAddr, isnull(A.DeptID,0) AS DeptID,isnull(C.DeptName,'') AS DeptName  ");
            sql.AppendLine("      ,A.Seller ,isnull(D.EmployeeName,'') AS  SellerName,A.BusiStatus,A.BillStatus AS  BillStatusSSS    ");
            sql.AppendLine("      ,case A.BusiStatus when '1' then '退单' when '2' then '入库' when '3' then '结算'                  ");
            sql.AppendLine("       when '4' then '完成' end AS BusiStatusName                                                        ");
            sql.AppendLine("      ,case A.BillStatus  when '1' then '制单' when '2' then '执行'                                      ");
            sql.AppendLine("      when '3'  then '变更' when '4' then '手工结单' when '5' then '自动结单' end  AS  BillStatusName    ");
            sql.AppendLine("      ,A.SendMode,A.FromType,A.CurrencyType  ");

            sql.AppendLine(" FROM officedba.SubSellBack AS A                                                                          ");
            sql.AppendLine("LEFT JOIN officedba.SubSellOrder AS B ON A.CompanyCD = B.CompanyCD AND A.OrderID=B.ID                     ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS C ON A.CompanyCD = C.CompanyCD AND A.DeptID=C.ID                          ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON A.CompanyCD = D.CompanyCD AND A.Seller=D.ID                      ");


            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
            if (BackNo != "" && BackNo != null)
            {
                sql.AppendLine(" AND A.BackNo like'%" + @BackNo + "%' ");
            }
            if (Title != null && Title != "")
            {
                sql.AppendLine(" AND A.Title like'%" + @Title + "%'  ");
            }
            if (OrderID != null && OrderID != "")
            {
                sql.AppendLine(" AND B.ID =@OrderID");
            }
            if (CustName != null && CustName != "")
            {
                sql.AppendLine(" AND A.CustName like'%" + @CustName + "%'  ");
            }
            if (CustTel != null && CustTel != "")
            {
                sql.AppendLine(" AND A.CustTel like'%" + @CustTel + "%'  ");
            }
            if (DeptID != null && DeptID != "")
            {
                sql.AppendLine(" AND A.DeptID =@DeptID");
            }
            if (Seller != "" && Seller != "")
            {
                sql.AppendLine(" AND D.ID=@Seller ");
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine(" and A.ExtField" + EFIndex + " LIKE @EFDesc ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }
            if (BusiStatus != null && BusiStatus != "")
            {
                sql.AppendLine(" AND A.BusiStatus = @BusiStatus");
            }
            if (BillStatus != null && BillStatus != "")
            {
                sql.AppendLine(" AND A.BillStatus = @BillStatus");
            }
            if (CustAddr != null && CustAddr != "")
            {
                sql.AppendLine(" AND A.CustAddr = @CustAddr");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BackNo", BackNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", Title));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderID", OrderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", CustName));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustTel", CustTel));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Seller", Seller));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BusiStatus", BusiStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustAddr", CustAddr));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);

            //sql.AppendLine("WHERE A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
            //if (!string.IsNullOrEmpty(str[0]))
            //{
            //    sql.AppendLine("     AND A.BackNo = '" + str[0].ToString().Trim() + "'         ");
            //}
            //if (!string.IsNullOrEmpty(str[1]))
            //{
            //    sql.AppendLine("	 AND A.Title like '%" + str[1].ToString().Trim() + "%'           ");
            //}
            //if (!string.IsNullOrEmpty(str[2]))
            //{
            //    sql.AppendLine("	 AND B.ID = " + Convert.ToInt32(str[2].ToString().Trim()) + "       ");
            //}
            //if (!string.IsNullOrEmpty(str[3]))
            //{
            //    sql.AppendLine("	 AND A.CustName like '%" + str[3].ToString().Trim() + "%'          ");
            //}
            //if (!string.IsNullOrEmpty(str[4]))
            //{
            //    sql.AppendLine("	 AND A.CustTel like '%" + str[4].ToString().Trim() + "%'           ");
            //}
            //if (!string.IsNullOrEmpty(str[5]))
            //{
            //    sql.AppendLine("	 AND A.DeptID = " + Convert.ToInt32(str[5].ToString().Trim()) + "      ");
            //}
            //if (!string.IsNullOrEmpty(str[6]))
            //{
            //    sql.AppendLine("	 AND D.ID = '" + Convert.ToInt32(str[6].ToString().Trim()) + " '    ");
            //}
            //if (!string.IsNullOrEmpty(str[7]))
            //{
            //    sql.AppendLine("     AND A.BusiStatus    =  '" + str[7].ToString().Trim() + "'   ");
            //}
            //if (!string.IsNullOrEmpty(str[8]))
            //{
            //    sql.AppendLine("	 AND A.BillStatus  =  '" + str[8].ToString().Trim() + "'    ");
            //}
            //if (!string.IsNullOrEmpty(str[9]))
            //{
            //    sql.AppendLine("	 AND A.CustAddr like '%" + str[9].ToString().Trim() + "%'   ");
            //}

            //return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 查找加载单个门店销售退货单记录
        public static DataTable SubSellBack(int ID)
        {
            StringBuilder sql = new StringBuilder();
            //创建视图
            //sql.AppendLine("SELECT A.ID ,A.DeptID ,isnull(B.DeptName,'') AS DeptName,isnull(A.BackNo,'') AS  BackNo, isnull(A.Title,'') AS Title");
            //sql.AppendLine("     ,isnull(A.OrderID,0) AS OrderID,isnull(C.OrderNo,'') AS OrderNo, A.SendMode,case A.SendMode when '1' then '分店发货' ");
            //sql.AppendLine("     when '2' then '总部发货' end AS SendModeName,isnull(A.Seller,0) AS Seller,isnull(D.EmployeeName,'') AS  SellerName   ");
            //sql.AppendLine("     ,isnull(A.CustName,'') AS CustName,isnull(A.CustTel,'') AS CustTel,isnull(A.CustMobile,'') AS CustMobile             ");
            //sql.AppendLine("     ,isnull(A.CustAddr,'') AS CustAddr,isnull(CONVERT(varchar(100),A.BackDate,120),'') AS BackDate                      ");
            //sql.AppendLine("     ,isnull(A.BackReason,'') AS BackReason,A.BusiStatus,case A.BusiStatus when '1' then '退单' when '2' then '入库' when '3'");
            //sql.AppendLine("     then '结算' when '4' then '完成' end AS BusiStatusName,A.CurrencyType, E.CurrencyName AS CurrencyTypeName, A.Rate    ");
            //sql.AppendLine("     ,Convert(numeric(12,2),A.TotalPrice) AS TotalPrice,Convert(numeric(12,2),A.Tax) AS Tax,Convert(numeric(12,2),A.TotalFee) AS TotalFee");
            //sql.AppendLine("     ,Convert(numeric(12,2),A.Discount) AS Discount,Convert(numeric(12,2),A.DiscountTotal) AS DiscountTotal,Convert(numeric(12,2),A.RealTotal) AS RealTotal");
            //sql.AppendLine("     ,Convert(numeric(12,2),A.PayedTotal) AS PayedTotal,Convert(numeric(12,2),A.WairPayTotal) AS WairPayTotal,Convert(numeric(12,2),(A.WairPayTotal-A.PayedTotal)) AS WairPayTotalOverage,A.isAddTax  ");
            //sql.AppendLine("     ,Convert(numeric(12,2),A.CountTotal) AS CountTotal,isnull(A.Remark,'') AS Remark,isnull(A.Attachment,'') AS Attachment");
            //sql.AppendLine("     ,A.BillStatus,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更' when '4' then '手工结单'");
            //sql.AppendLine("     when '5' then '自动结单' end AS BillStatusName,A.Creator,isnull(CONVERT(varchar(100),A.CreateDate,23),'') AS CreateDate");
            //sql.AppendLine("     ,isnull(F.EmployeeName,'') AS CreatorName,isnull(A.Confirmor,0) AS Confirmor,isnull(G.EmployeeName,'') AS ConfirmorName ");
            //sql.AppendLine("     ,isnull(CONVERT(varchar(100),A.ConfirmDate,23),'') AS ConfirmDate,isnull(A.OutUserID,0) AS OutUserID,isnull(H.EmployeeName,'') AS OutUserIDName ");
            //sql.AppendLine("     ,isnull(CONVERT(varchar(100),A.OutDate,120),'') AS OutDate,isnull(A.SttlUserID,0) AS SttlUserID,isnull(I.EmployeeName,'') AS SttlUserIDName ");
            //sql.AppendLine("     ,isnull(CONVERT(varchar(100),A.SttlDate,120),'') AS SttlDate,isnull(A.Closer,0) AS Closer,isnull(CONVERT(varchar(100),A.CloseDate,23),'') AS CloseDate");
            //sql.AppendLine("     ,CONVERT(varchar(100),A.ModifiedDate,23) AS  ModifiedDate,isnull(A.ModifiedUserID,'') AS ModifiedUserID,A.FromType         ");
            //sql.AppendLine("     ,isnull(A.InUserID,0) AS InUserID,isnull(J.EmployeeName,'') AS InUserIDName,isnull(CONVERT(varchar(100),A.InDate,120),'') AS InDate");
            //sql.AppendLine("       ,isnull(K.EmployeeName,'') AS CloserName");

            //sql.AppendLine(" FROM officedba.SubSellBack AS A                                                                              ");
            //sql.AppendLine("LEFT JOIN officedba.DeptInfo AS B ON A.CompanyCD = B.CompanyCD AND A.DeptID=B.ID                              ");
            //sql.AppendLine("LEFT JOIN officedba.SubSellOrder AS C on A.CompanyCD = C.CompanyCD  AND A.OrderID=C.ID                        ");
            //sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON A.CompanyCD = D.CompanyCD AND A.Seller=D.ID                          ");
            //sql.AppendLine("LEFT JOIN officedba.CurrencyTypeSetting AS E ON A.CompanyCD = E.CompanyCD  AND A.CurrencyType=E.ID            ");
            //sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS F ON A.CompanyCD = F.CompanyCD AND A.Creator=F.ID                         ");
            //sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS G ON A.CompanyCD = G.CompanyCD AND A.Confirmor=G.ID                       ");
            //sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS H ON A.CompanyCD = H.CompanyCD AND A.OutUserID=H.ID                       ");
            //sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS I ON A.CompanyCD = I.CompanyCD AND A.SttlUserID=I.ID                      ");
            //sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS J ON A.CompanyCD = J.CompanyCD AND A.InUserID=J.ID                        ");
            //sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS K ON A.CompanyCD = K.CompanyCD AND A.Closer=K.ID                          ");



            sql.AppendLine("SELECT A.ID ,A.DeptID ,A.DeptName,A.BackNo, A.Title,A.OrderID,A.OrderNo, A.SendMode,A.SellerName,A.Seller       ");
            sql.AppendLine("     ,A.CustName,A.CustTel,A.CustMobile,A.CustAddr,A.BackDate,A.BackReason,A.BusiStatus,A.BusiStatusName        ");
            sql.AppendLine("     ,A.CurrencyType, A.CurrencyTypeName, A.Rate,A.TotalPrice,A.Tax,A.TotalFee,A.Discount,A.DiscountTotal       ");
            sql.AppendLine("     ,A.RealTotal,A.PayedTotal,A.WairPayTotal,A.isAddTax,A.CountTotal,A.Remark,A.Attachment,A.BillStatus        ");
            sql.AppendLine("     ,A.BillStatusName,A.Creator,A.CreateDate,A.CreatorName,A.Confirmor,A.ConfirmorName,A.ConfirmDate           ");
            sql.AppendLine("     ,A.OutUserID,A.OutUserIDName,A.OutDate,A.SttlUserID,A.SttlUserIDName,A.SttlDate,A.Closer,A.CloseDate       ");
            sql.AppendLine("     ,A.ModifiedDate,A.ModifiedUserID,A.FromType,A.InUserID,A.InUserIDName,A.InDate,A.CloserName,A.WairPayTotalOverage,A.SendModeName");
            sql.AppendLine("     ,A.ExtField1,A.ExtField2,A.ExtField3,A.ExtField4,A.ExtField5,A.ExtField6,A.ExtField7,A.ExtField8,A.ExtField9,A.ExtField10");
            sql.AppendLine(" FROM officedba.V_SubStoreSubSellBack AS A  ");
            sql.AppendLine("WHERE 1=1                                   ");
            sql.AppendLine("AND A.ID = " + ID + "                       ");



            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 查找加载门店销售退货单明细
        public static DataTable Details(int ID, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine("SELECT distinct A.ID,A.CompanyCD,A.DeptID ,isnull(B.DeptName,'') AS DeptName,A.BackNo,isnull(A.OrderID,0) AS FromBillID  ");
            sql.AppendLine(" ,isnull(D.OrderNo,'') AS FromBillNo,isnull(A.FromLineNo,0) AS FromLineNo,A.SortNo,isnull(A.ProductID,0) AS ProductID");
            sql.AppendLine(" ,isnull(F.ProdNo,'') AS ProductNo, isnull(F.ProductName,'') AS ProductName,isnull(F.Specification,'') AS standard ");
            sql.AppendLine(" ,isnull(A.StorageID,0) AS StorageID,isnull(C.StorageName,'') AS StorageName,isnull(Convert(numeric(12,2),A.BackCount),0) AS BackCount ");
            sql.AppendLine(" ,isnull(A.UnitID,0) AS UnitID,isnull(Convert(numeric(12,2),A.UnitPrice),0) AS UnitPrice,isnull(F.TaxRate,0) AS HidTaxRate ");
            sql.AppendLine(" ,case when A.UnitID is null then '' when A.UnitID ='' then ''                                                  ");
            sql.AppendLine(" else (select CodeName from officedba.CodeUnitType where id=A.UnitID)end as UnitName                            ");
            sql.AppendLine(" ,isnull(Convert(numeric(12,2),A.TaxPrice),0) AS TaxPrice,isnull(Convert(numeric(8,2),A.Discount),0) AS Discount");
            sql.AppendLine(" ,isnull(Convert(numeric(8,2),A.TaxRate),0) AS TaxRate,isnull(Convert(numeric(12,2),A.TotalFee),0) AS TotalFee  ");
            sql.AppendLine(" ,isnull(Convert(numeric(12,2),A.TotalPrice),0) AS TotalPrice,isnull(Convert(numeric(12,2),A.TotalTax),0) AS TotalTax");
            sql.AppendLine(" ,isnull(A.Remark,'') AS Remark,Convert(numeric(12,2), isnull(case when A.OrderID is null then 0 when A.OrderID = '' then 0 ");
            sql.AppendLine(" else (select BackCount from officedba.SubSellOrderDetail where OrderNo=D.OrderNo and SortNo=A.FromLineNo)end,0)) AS YBackCount");
            sql.AppendLine(" ,Convert(numeric(14,2),isnull(G.ProductCount,0)) AS ProductCount,A.UsedUnitID,A.UsedUnitCount,A.UsedPrice,A.ExRate,A.BatchNo,F.IsBatchNo ");
            sql.AppendLine(" ,(select CodeName from officedba.CodeUnitType cu where cu.id=A.UsedUnitID) AS UsedUnitName                            ");



            sql.AppendLine("FROM officedba.SubSellBackDetail AS A                                                         ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo     AS B ON A.CompanyCD = B.CompanyCD AND A.DeptID=B.ID          ");
            sql.AppendLine("LEFT JOIN officedba.StorageInfo  AS C on A.CompanyCD = C.CompanyCD  AND A.StorageID=C.ID      ");
            sql.AppendLine("LEFT JOIN officedba.SubSellOrder AS D on A.CompanyCD = D.CompanyCD  AND A.OrderID=D.ID        ");
            sql.AppendLine("LEFT JOIN officedba.SubSellBack  AS E on A.CompanyCD = E.CompanyCD  AND A.BackNo=E.BackNo     ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo  AS F on A.CompanyCD = F.CompanyCD  AND A.ProductID=F.ID      ");
            sql.AppendLine("LEFT JOIN officedba.SubSellOrderDetail AS G on D.CompanyCD = G.CompanyCD  AND D.OrderNo=G.OrderNo AND A.FromLineNo = G.SortNo");
            sql.AppendLine("WHERE 1=1  and A.CompanyCD=@CompanyCD          ");
            sql.AppendLine("AND E.ID = @ID ");
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 删除门店销售退货单主表
        public static SqlCommand DeleteSubSellBack(string IDs)
        {
            SqlCommand comm = new SqlCommand();

            String sql = "DELETE FROM officedba.SubSellBack WHERE ID in (" + IDs + ")";

            comm.CommandText = sql;

            return comm;
        }
        #endregion

        #region 删除门店销售退货单明细
        public static SqlCommand DeleteSubSellBackDetail(string BackNos, string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE FROM officedba.SubSellBackDetail");
            sql.AppendLine(" WHERE CompanyCD=@CompanyCD");
            sql.AppendLine(" AND BackNo in (" + BackNos + ")");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion
    }
}

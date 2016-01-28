/***********************************************
 * 类作用：   采购管理事务层处理               *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/03/26                       *
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
    /// 类名：PurchaseContractDBHelper
    /// 描述：采购合同数据库层处理
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/03/25
    /// 最后修改时间：2009/03/25
    /// </summary>
    ///
    public class PurchaseContractDBHelper
    {
        //#region 新增：合同记录
        ///// <summary>
        ///// 添加合同记录
        ///// </summary>
        ///// <returns></returns>
        //public static bool InsertPurchaseContract(string[] sql)
        //{
        //    //StringBuilder sqlc = new StringBuilder();
        //    //sqlc.AppendLine("Insert into officedba.PurchaseContract");
        //    //sqlc.AppendLine("(CompanyCD,ContractNo,Title,ProviderID,FromType,FromBillID,");
        //    //sqlc.AppendLine("TotalPrice,CurrencyType,Rate,PayType,SignDate,");
        //    //sqlc.AppendLine("Seller,OppositerUserID,OurUserID,Status,Note,");
        //    //sqlc.AppendLine("TakeType,CarryType,Attachment,remark,BillStatus,Creator,");
        //    //sqlc.AppendLine("CreateDate,ModifiedDate,ModifiedUserID,Confirmor,ConfirmDate,Closer,CloseDate)");
        //    //sqlc.AppendLine("values(@CompanyCD,@ContractNo,@Title,@ProviderID,@FromType,@FromBillID,@TotalPrice,@CurrencyType,@Rate,@PayType,@SignDate,@Seller,@OppositerUserID");
        //    //sqlc.AppendLine(",@OurUserID,@Status,@Note,@TakeType,@CarryType,@Attachment,@remark,@BillStatus,@Creator,getdate(),getdate(),");
        //    //sqlc.AppendLine("@ModifiedUserID,@Confirmor,@ConfirmDate,@Closer,@CloseDate)");

        //    //SqlParameter[] parms = new SqlParameter[27];
        //    //parms[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
        //    //parms[1] = SqlHelper.GetParameter("ContractNo", model.ContractNo);
        //    //parms[2] = SqlHelper.GetParameter("@Title", model.Title);
        //    //parms[3] = SqlHelper.GetParameter("@ProviderID", model.ProviderID);
        //    //parms[4] = SqlHelper.GetParameter("@FromType", model.FromType);
        //    //parms[5] = SqlHelper.GetParameter("@FromBillID", model.FromBillID);
        //    //parms[6] = SqlHelper.GetParameter("@TotalPrice", model.TotalPrice);
        //    //parms[7] = SqlHelper.GetParameter("@CurrencyType", model.CurrencyType);
        //    //parms[8] = SqlHelper.GetParameter("@Rate", model.Rate);
        //    //parms[9] = SqlHelper.GetParameter("@PayType", model.PayType);
        //    //parms[10] = SqlHelper.GetParameter("@SignDate", model.SignDate == null
        //    //                        ? SqlDateTime.Null
        //    //                        : SqlDateTime.Parse(model.SignDate.ToString()));
        //    //parms[11] = SqlHelper.GetParameter("@Seller", model.Seller);
        //    //parms[12] = SqlHelper.GetParameter("@OppositerUserID", model.OppositerUserID);
        //    //parms[13] = SqlHelper.GetParameter("@OurUserID", model.OurUserID);
        //    //parms[14] = SqlHelper.GetParameter("@Status", "1");//新建合同时默认状态为执行中
        //    //parms[15] = SqlHelper.GetParameter("@Note", model.Note);
        //    //parms[16] = SqlHelper.GetParameter("@TakeType", model.TakeType);
        //    //parms[17] = SqlHelper.GetParameter("@CarryType", model.CarryType);
        //    //parms[18] = SqlHelper.GetParameter("@Attachment", model.Attachment);
        //    //parms[19] = SqlHelper.GetParameter("@remark", model.remark);
        //    //parms[20] = SqlHelper.GetParameter("@BillStatus", model.BillStatus);
        //    //parms[21] = SqlHelper.GetParameter("@Creator", model.Creator);
        //    //parms[22] = SqlHelper.GetParameter("@ModifiedUserID", (11));
        //    //parms[23] = SqlHelper.GetParameter("@Confirmor", model.Confirmor);
        //    //parms[24] = SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate == null
        //    //                        ? SqlDateTime.Null
        //    //                        : SqlDateTime.Parse(model.ConfirmDate.ToString()));
        //    //parms[25] = SqlHelper.GetParameter("@Closer", model.Closer);
        //    //parms[26] = SqlHelper.GetParameter("@CloseDate", model.CloseDate == null
        //    //                        ? SqlDateTime.Null
        //    //                        : SqlDateTime.Parse(model.CloseDate.ToString()));
        //    //SqlHelper.ExecuteTransSql(sqlc.ToString(), parms);
        //    //return SqlHelper.Result.OprateCount > 0 ? true : false;

        //    return SqlHelper.ExecuteTransForListWithSQL(sql);


        //}
        //#endregion


        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(PurchaseContractModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.PurchaseContract set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND ContractNo = @ContractNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@ContractNo", model.ContractNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion
        #region 插入采购合同
        public static bool InsertPurchaseContract(PurchaseContractModel model, string DetailProductID, string DetailProductNo, string DetailProductName, string DetailUnitID, string DetailProductCount, string DetailUnitPrice, string DetailTaxPrice, string DetailDiscount, string DetailTaxRate, string DetailTotalPrice, string DetailTotalFee, string DetailTotalTax, string DetailRequireDate, string DetailApplyReason, string DetailRemark, string DetailFromBillID, string DetailFromLineNo, string DetailUsedUnitID, string DetailUsedUnitCount, string DetailUsedPrice, string DetailExRate, string length, out string ID, Hashtable htExtAttr)
        {

            ArrayList listADD = new ArrayList();
            bool result = false;
            ID = "0";

            #region  采购合同添加SQL语句
            StringBuilder sqlArrive = new StringBuilder();


            sqlArrive.AppendLine("INSERT INTO officedba.PurchaseContract");
            sqlArrive.AppendLine("(CompanyCD,ContractNo,Title,ProviderID,FromType,FromBillID,CurrencyType,Rate,PayType,");
            sqlArrive.AppendLine("MoneyType,SignDate,Seller,DeptID,TheyDelegate,OurDelegate,SignAddr,");
            sqlArrive.AppendLine("Note,TakeType,CarryType,TotalPrice,TotalTax,TotalFee,Discount,DiscountTotal,");
            sqlArrive.AppendLine("RealTotal,isAddTax,CountTotal,Attachment,remark,BillStatus,Creator");
            sqlArrive.AppendLine(",CreateDate,ModifiedDate,ModifiedUserID,Confirmor,ConfirmDate,Closer,CloseDate,TypeID)");
            sqlArrive.AppendLine("VALUES (@CompanyCD,@ContractNo,@Title,@ProviderID,@FromType,@FromBillID,@CurrencyType,@Rate,@PayType,");
            sqlArrive.AppendLine("@MoneyType,@SignDate,@Seller,@DeptID,@TheyDelegate,@OurDelegate,@SignAddr,");
            sqlArrive.AppendLine("@Note,@TakeType,@CarryType,@TotalPrice,@TotalTax,@TotalFee,@Discount,@DiscountTotal,");
            sqlArrive.AppendLine("@RealTotal,@isAddTax,@CountTotal,@Attachment,@remark,@BillStatus,@Creator");
            sqlArrive.AppendLine(",@CreateDate,getdate(),@ModifiedUserID,@Confirmor,@ConfirmDate,@Closer,@CloseDate,@TypeID)");
            sqlArrive.AppendLine("set @ID=@@IDENTITY");

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ContractNo", model.ContractNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProviderID", model.ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromBillID", model.FromBillID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", model.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@Rate", model.Rate));
            comm.Parameters.Add(SqlHelper.GetParameter("@PayType", model.PayType));
            comm.Parameters.Add(SqlHelper.GetParameter("@MoneyType", model.MoneyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@SignDate", model.SignDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.SignDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Seller", model.Seller));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@TheyDelegate", model.TheyDelegate));
            comm.Parameters.Add(SqlHelper.GetParameter("@OurDelegate", model.OurDelegate));
            comm.Parameters.Add(SqlHelper.GetParameter("@SignAddr", model.SignAddr));
            comm.Parameters.Add(SqlHelper.GetParameter("@Note", model.Note));
            comm.Parameters.Add(SqlHelper.GetParameter("@TakeType", model.TakeType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CarryType", model.CarryType));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", model.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalTax", model.TotalTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalFee", model.TotalFee));
            comm.Parameters.Add(SqlHelper.GetParameter("@Discount", model.Discount));
            comm.Parameters.Add(SqlHelper.GetParameter("@DiscountTotal", model.DiscountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@RealTotal", model.RealTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@isAddTax", model.isAddTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@Attachment", model.Attachment));
            comm.Parameters.Add(SqlHelper.GetParameter("@remark", model.remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.CreateDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.ConfirmDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Closer", model.Closer));
            comm.Parameters.Add(SqlHelper.GetParameter("@CloseDate", model.CloseDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.CloseDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@TypeID", model.TypeID));
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
                #region 采购合同明细
                int lengs = Convert.ToInt32(length);
                if (lengs > 0)
                {
                    string[] ProductID = DetailProductID.Split(',');
                    string[] ProductNo = DetailProductNo.Split(',');
                    string[] ProductName = DetailProductName.Split(',');
                    string[] UnitID = DetailUnitID.Split(',');
                    string[] ProductCount = DetailProductCount.Split(',');
                    string[] UnitPrice = DetailUnitPrice.Split(',');//单价
                    string[] TaxPrice = DetailTaxPrice.Split(',');//含税价
                    //string[] Discount = DetailDiscount.Split(',');//折扣
                    string[] TaxRate = DetailTaxRate.Split(',');//税率
                    string[] TotalPrice = DetailTotalPrice.Split(',');//金额
                    string[] TotalFee = DetailTotalFee.Split(',');//含税金额
                    string[] TotalTax = DetailTotalTax.Split(',');//税额
                    string[] RequireDate = DetailRequireDate.Split(',');
                    string[] ApplyReason = DetailApplyReason.Split(',');
                    string[] Remark = DetailRemark.Split(',');
                    string[] FromBillID = DetailFromBillID.Split(',');
                    string[] FromLineNo = DetailFromLineNo.Split(',');

                    string[] UsedUnitID = DetailUsedUnitID.Split(',');
                    string[] UsedUnitCount = DetailUsedUnitCount.Split(',');
                    string[] UsedPrice = DetailUsedPrice.Split(',');
                    string[] ExRate = DetailExRate.Split(',');
                    //SqlCommand comms = null;
                    //System.Text.StringBuilder cmdsql=null;
                    for (int i = 0; i < lengs; i++)
                    {
                        System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                        SqlCommand comms = new SqlCommand();
                        cmdsql.AppendLine("INSERT INTO officedba.PurchaseContractDetail");
                        cmdsql.AppendLine("(CompanyCD,");
                        cmdsql.AppendLine("ContractNo,");
                        cmdsql.AppendLine("SortNo,");
                        cmdsql.AppendLine("FromType,");
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("FromBillID,");
                            }
                        }
                        if (RequireDate[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(RequireDate[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("RequireDate,");
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("FromLineNo,");
                            }
                        }
                        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                        {
                            if (UsedUnitID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedUnitID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           UsedUnitID,                        ");
                                }
                            }
                            if (UsedUnitCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedUnitCount[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           UsedUnitCount ,                        ");
                                }
                            }
                            if (UsedPrice[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedPrice[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           UsedPrice,                        ");
                                }
                            }
                            if (ExRate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(ExRate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ExRate,                        ");
                                }
                            }
                        }








                        cmdsql.AppendLine("ProductID,");
                        cmdsql.AppendLine("ProductNo,");
                        cmdsql.AppendLine("ProductName,");
                        cmdsql.AppendLine("ProductCount,");
                        cmdsql.AppendLine("UnitID,");
                        cmdsql.AppendLine("UnitPrice,");
                        cmdsql.AppendLine("TaxPrice,");
                        //cmdsql.AppendLine("Discount,");
                        cmdsql.AppendLine("TaxRate,");
                        cmdsql.AppendLine("TotalFee,");
                        cmdsql.AppendLine("TotalPrice,");
                        cmdsql.AppendLine("TotalTax,");
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("Remark,");
                            }
                        }
                        cmdsql.AppendLine("ApplyReason)");
                        cmdsql.AppendLine(" Values(@CompanyCD");
                        cmdsql.AppendLine("            ,@ContractNo");
                        cmdsql.AppendLine("            ,@SortNo");
                        cmdsql.AppendLine("            ,@FromType");
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@FromBillID");
                            }
                        }
                        if (RequireDate[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(RequireDate[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@RequireDate");
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@FromLineNo");
                            }
                        }
                        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                        {
                            if (UsedUnitID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedUnitID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@UsedUnitID                        ");
                                }
                            }
                            if (UsedUnitCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedUnitCount[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@UsedUnitCount                         ");
                                }
                            }
                            if (UsedPrice[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedPrice[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@UsedPrice                        ");
                                }
                            }
                            if (ExRate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(ExRate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@ExRate                        ");
                                }
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
                        cmdsql.AppendLine("            ,@UnitID");
                        cmdsql.AppendLine("            ,@UnitPrice");
                        cmdsql.AppendLine("            ,@TaxPrice");
                        //cmdsql.AppendLine("            ,@Discount");
                        cmdsql.AppendLine("            ,@TaxRate");
                        cmdsql.AppendLine("            ,@TotalFee");
                        cmdsql.AppendLine("            ,@TotalPrice");
                        cmdsql.AppendLine("            ,@TotalTax");
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@Remark2");
                            }
                        }
                        cmdsql.AppendLine("            ,@ApplyReason)");


                        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ContractNo", model.ContractNo));
                        comms.Parameters.Add(SqlHelper.GetParameter("@SortNo", i + 1));
                        comms.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
                        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                        {
                            if (UsedUnitID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedUnitID[i].ToString().Trim()))
                                { 
                                    comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", UsedUnitID[i].ToString()));
                                }
                            }
                            if (UsedUnitCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedUnitCount[i].ToString().Trim()))
                                { 
                                    comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", UsedUnitCount[i].ToString()));
                                }
                            }
                            if (UsedPrice[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedPrice[i].ToString().Trim()))
                                { 
                                    comms.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", UsedPrice[i].ToString()));
                                }
                            }
                            if (ExRate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(ExRate[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@ExRate", ExRate[i].ToString()));
                                }
                            }
                        }

                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@FromBillID", FromBillID[i].ToString()));
                            }
                        }
                        if (RequireDate[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(RequireDate[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@RequireDate", RequireDate[i].ToString()));
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
                        comms.Parameters.Add(SqlHelper.GetParameter("@UnitID", UnitID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UnitPrice", UnitPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxPrice", TaxPrice[i].ToString()));
                        //comms.Parameters.Add(SqlHelper.GetParameter("@Discount", Discount[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxRate", TaxRate[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalFee", TotalFee[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", TotalPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalTax", TotalTax[i].ToString()));
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

        //#region 合同明细插入操作
        ///// <summary>
        ///// 合同明细insert
        ///// </summary>
        ///// <param name="PurchaseApplyM">主表</param>
        ///// <param name="str">明细来源</param>
        ///// <param name="str2">明细信息</param>
        ///// <param name="ContractNo"></param>
        ///// <param name="dtlslen">原明细来源的长度</param>
        ///// <returns></returns>
        //public static void InsertDtlS(PurchaseContractModel model, string DetailID, string DetailProductNo, string DetailProductName, string DetailProductCount, string DetailUnitID, string DetailRequireDate, string DetailUnitPrice, string DetailTotalPrice, string DetailApplyReason, string DetailRemark, string DetailFromBillID, string DetailFromLineNo, string length)
        //{
        //    //string[] inseritems = null;
        //    //inseritems = str.Split(',');
        //    //StringBuilder dtlssql = new StringBuilder();
        //    //dtlssql.AppendLine("INSERT INTO officedba.PurchaseContractDetail");
        //    //dtlssql.AppendLine("           (CompanyCD                          ");
        //    //dtlssql.AppendLine("            ,ContractNo                        ");
        //    //dtlssql.AppendLine("            ,ProductNo                         ");
        //    //dtlssql.AppendLine("            ,ProductName                       ");
        //    //dtlssql.AppendLine("            ,ProductCount                      ");
        //    //dtlssql.AppendLine("            ,UnitID                            ");
        //    //dtlssql.AppendLine("            ,RequireDate                       ");
        //    //dtlssql.AppendLine("            ,UnitPrice                         ");
        //    //dtlssql.AppendLine("            ,TotalPrice                        ");
        //    //dtlssql.AppendLine("            ,ApplyReason                       ");
        //    //dtlssql.AppendLine("            ,Remark                            ");
        //    //dtlssql.AppendLine("            ,FromBillID                        ");
        //    //dtlssql.AppendLine("            ,FromLineNo)                       ");
        //    //dtlssql.AppendLine("values ");

        //    //dtlssql.AppendLine("           ( '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
        //    //dtlssql.AppendLine("            ,'" + ContractNo.ToString() + "' ");
        //    //dtlssql.AppendLine("            ," + inseritems[0].ToString() + " ");
        //    //dtlssql.AppendLine("            ," + inseritems[1].ToString() + " ");
        //    //dtlssql.AppendLine("            ,'" + Convert.ToDecimal(inseritems[2].ToString()) + "' ");
        //    //dtlssql.AppendLine("            ,'" + Convert.ToDecimal(inseritems[3].ToString()) + "' ");
        //    //dtlssql.AppendLine("            ," + Convert.ToDateTime(inseritems[4].ToString()) + " ");
        //    //dtlssql.AppendLine("            ," + Convert.ToDecimal(inseritems[5].ToString()) + " ");
        //    //dtlssql.AppendLine("            ," + Convert.ToDecimal(inseritems[6].ToString()) + "");
        //    //dtlssql.AppendLine("            ," + Convert.ToDecimal(inseritems[7].ToString()) + "");
        //    //dtlssql.AppendLine("            ," + inseritems[8].ToString() + " ");
        //    //dtlssql.AppendLine("            ,'" + Convert.ToDecimal(inseritems[9].ToString()) + " '");
        //    //dtlssql.AppendLine("            ,'" + Convert.ToDecimal(inseritems[10].ToString()) + "') ");
        //    //sql[i] = dtlssql.ToString();
        //}
        //#endregion

        #region 修改：合同记录
        ///<summary>
        /// 修改合同记录
        /// <param name="CompanyCD"></param>
        /// <param name="CompanyNo"></param>
        /// <returns></returns>
        //public static bool UpdatePurchaseContract(string[] sql)
        //{
        //    //StringBuilder sql = new StringBuilder();
        //    //sql.AppendLine("UPDATE officedba.PurchaseContract       ");
        //    //sql.AppendLine("   SET,Title            = @Title ");
        //    //sql.AppendLine("      ,ProviderID       = @ProviderID");
        //    //sql.AppendLine("      ,FromType         = @FromType");
        //    //sql.AppendLine("      ,FromBillID       = @FromBillID");
        //    //sql.AppendLine("      ,TotalPrice       = @TotalPrice");
        //    //sql.AppendLine("      ,CurrencyType     = @CurrencyType");
        //    //sql.AppendLine("      ,Rate             = @Rate");
        //    //sql.AppendLine("      ,PayType          = @PayType");
        //    //sql.AppendLine("      ,SignDate         = @SignDate ");
        //    //sql.AppendLine("      ,Seller           = @Seller");
        //    //sql.AppendLine("      ,OppositerUserID  = @OppositerUserID");
        //    //sql.AppendLine("      ,OurUserID        = @OurUserID");
        //    //sql.AppendLine("      ,Status           = @Status ");
        //    //sql.AppendLine("      ,Note             = @Note");
        //    //sql.AppendLine("      ,TakeType         = @TakeType");
        //    //sql.AppendLine("      ,CarryType        = @CarryType");
        //    //sql.AppendLine("      ,Attachment       = @Attachment");
        //    //sql.AppendLine("      ,remark           = @remark");
        //    //sql.AppendLine("      ,BillStatus       = @BillStatus");
        //    //sql.AppendLine("      ,Creator          = @Creator ");
        //    //sql.AppendLine("      ,CreateDate       = @CreateDate");
        //    //sql.AppendLine("      ,ModifiedDate     = getdate()");
        //    //sql.AppendLine("      ,ModifiedUserID   = @ModifiedUserID");
        //    //sql.AppendLine("      ,Confirmor        = @Confirmor");
        //    //sql.AppendLine("      ,ConfirmDate      = @ConfirmDate");
        //    //sql.AppendLine("      ,Closer           = @Closer");
        //    //sql.AppendLine("      ,CloseDate        = @CloseDate");
        //    //sql.AppendLine(" Where  CompanyCD=@CompanyCD and ContractNo=@ContractNo");
        //    //SqlParameter[] parms;
        //    //parms = new SqlParameter[27];
        //    //parms[0] = SqlHelper.GetParameter("@Title", model.Title);
        //    //parms[1] = SqlHelper.GetParameter("@ProviderID", model.ProviderID);
        //    //parms[2] = SqlHelper.GetParameter("@FromType", model.FromType);
        //    //parms[3] = SqlHelper.GetParameter("@FromBillID", model.FromBillID);
        //    //parms[4] = SqlHelper.GetParameter("@TotalPrice", model.TotalPrice);
        //    //parms[5] = SqlHelper.GetParameter("@CurrencyType", model.CurrencyType);
        //    //parms[6] = SqlHelper.GetParameter("@Rate", model.Rate);
        //    //parms[7] = SqlHelper.GetParameter("@PayType", model.PayType);
        //    //parms[8] = SqlHelper.GetParameter("@SignDate", model.SignDate);
        //    //parms[9] = SqlHelper.GetParameter("@Seller", model.Seller);
        //    //parms[10] = SqlHelper.GetParameter("@OppositerUserID", model.OppositerUserID);
        //    //parms[11] = SqlHelper.GetParameter("@OurUserID", model.OurUserID);
        //    //parms[12] = SqlHelper.GetParameter("@Status", model.Status);
        //    //parms[13] = SqlHelper.GetParameter("@Note", model.Note);
        //    //parms[14] = SqlHelper.GetParameter("@TakeType", model.TakeType);
        //    //parms[15] = SqlHelper.GetParameter("@CarryType", model.CarryType);
        //    //parms[16] = SqlHelper.GetParameter("@Attachment", model.Attachment);
        //    //parms[17] = SqlHelper.GetParameter("@remark", model.remark);
        //    //parms[18] = SqlHelper.GetParameter("@BillStatus", model.BillStatus);
        //    //parms[19] = SqlHelper.GetParameter("@Creator", model.Creator);
        //    //parms[20] = SqlHelper.GetParameter("@CreateDate", model.CreateDate);
        //    //parms[21] = SqlHelper.GetParameter("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
        //    //parms[22] = SqlHelper.GetParameter("@Confirmor", model.Confirmor);
        //    //parms[23] = SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate);
        //    //parms[24] = SqlHelper.GetParameter("@Closer", model.Closer);
        //    //parms[25] = SqlHelper.GetParameter("@CloseDate", model.CloseDate);
        //    //parms[26] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
        //    //parms[27] = SqlHelper.GetParameter("@ContractNo", model.ContractNo);
        //    //SqlHelper.ExecuteSql(sql.ToString(), parms);
        //    //return SqlHelper.Result.OprateCount > 0 ? true : false;

        //    return SqlHelper.ExecuteTransForListWithSQL(sql);
        //}
        #endregion


        #region 修改采购合同
        public static bool UpdatePurchaseContract(PurchaseContractModel model, string DetailProductID, string DetailProductNo, string DetailProductName, string DetailUnitID, string DetailProductCount, string DetailUnitPrice, string DetailTaxPrice, string DetailDiscount, string DetailTaxRate, string DetailTotalPrice, string DetailTotalFee, string DetailTotalTax, string DetailRequireDate, string DetailApplyReason, string DetailRemark, string DetailFromBillID, string DetailFromBillNo, string DetailFromLineNo, string DetailUsedUnitID, string DetailUsedUnitCount, string DetailUsedPrice, string DetailExRate, string length, string fflag2, string no, Hashtable htExtAttr)
        {
            if (model.ID <= 0)
            {
                return false;
            }
            ArrayList listADD = new ArrayList();
            bool result = false;

            #region  修改采购合同
            StringBuilder sqlArrive = new StringBuilder();

            sqlArrive.AppendLine("Update  Officedba.PurchaseContract set CompanyCD=@CompanyCD,");
            sqlArrive.AppendLine("ContractNo=@ContractNo,Title=@Title,ProviderID=@ProviderID,FromType=@FromType,FromBillID=@FromBillID,");
            sqlArrive.AppendLine("CurrencyType=@CurrencyType,Rate=@Rate,PayType=@PayType,MoneyType=@MoneyType,SignDate=@SignDate,Seller=@Seller,");
            sqlArrive.AppendLine("DeptID=@DeptID,TheyDelegate=@TheyDelegate,OurDelegate=@OurDelegate,SignAddr=@SignAddr,");
            sqlArrive.AppendLine("Note=@Note,TakeType=@TakeType,CarryType=@CarryType,TotalPrice=@TotalPrice,TotalTax=@TotalTax,TotalFee=@TotalFee,");
            sqlArrive.AppendLine("Discount=@Discount,DiscountTotal=@DiscountTotal,RealTotal=@RealTotal,isAddTax=@isAddTax,CountTotal=@CountTotal,");
            sqlArrive.AppendLine("Attachment=@Attachment,remark=@remark,BillStatus=@BillStatus,CreateDate=@CreateDate,");
            sqlArrive.AppendLine("ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID,Confirmor=@Confirmor,ConfirmDate=@ConfirmDate,");
            sqlArrive.AppendLine("Closer=@Closer,CloseDate=@CloseDate,TypeID=@TypeID where CompanyCD=@CompanyCD and ContractNo=@ContractNo and ID=@ID");


            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ContractNo", no));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProviderID", model.ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromBillID", model.FromBillID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", model.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@Rate", model.Rate));
            comm.Parameters.Add(SqlHelper.GetParameter("@PayType", model.PayType));
            comm.Parameters.Add(SqlHelper.GetParameter("@MoneyType", model.MoneyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@SignDate", model.SignDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.SignDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Seller", model.Seller));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@TheyDelegate", model.TheyDelegate));
            comm.Parameters.Add(SqlHelper.GetParameter("@OurDelegate", model.OurDelegate));
            comm.Parameters.Add(SqlHelper.GetParameter("@SignAddr", model.SignAddr));
            comm.Parameters.Add(SqlHelper.GetParameter("@Note", model.Note));
            comm.Parameters.Add(SqlHelper.GetParameter("@TakeType", model.TakeType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CarryType", model.CarryType));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", model.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalTax", model.TotalTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalFee", model.TotalFee));
            comm.Parameters.Add(SqlHelper.GetParameter("@Discount", model.Discount));
            comm.Parameters.Add(SqlHelper.GetParameter("@DiscountTotal", model.DiscountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@RealTotal", model.RealTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@isAddTax", model.isAddTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@Attachment", model.Attachment));
            comm.Parameters.Add(SqlHelper.GetParameter("@remark", model.remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.CreateDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.ConfirmDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Closer", model.Closer));
            comm.Parameters.Add(SqlHelper.GetParameter("@CloseDate", model.CloseDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.CloseDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@TypeID", model.TypeID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.CommandText = sqlArrive.ToString();


            listADD.Add(comm);
            model.ContractNo = no;
            #endregion
              #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                listADD.Add(cmd);
            #endregion

            #region 保存修改时，判断源单类型是否为采购计划，如果是的话，再判断是否由变更到执行，即确认后再修改的，则对采购计划明细字段回写减操作
            //#region 原先没有取消确认，撤消被回写的字段在确认后再次修改时撤消的。增加了取消确认，撤消回写的写在取消确认的方法里。
            //#endregion
            //if (model.FromType.Trim() == "2")
            //{
            //    if (fflag2 == "1")
            //    {
            //        int lengs = Convert.ToInt32(length);
            //        if (lengs > 0)
            //        {
            //            string[] ProductCount = DetailProductCount.Split(',');
            //            string[] FromBillNo = DetailFromBillNo.Split(',');
            //            string[] FromLineNo = DetailFromLineNo.Split(',');

            //            for (int i = 0; i < lengs; i++)
            //            {
            //                System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
            //                SqlCommand commp = new SqlCommand();
            //                cmdsql.AppendLine("Update  Officedba.PurchasePlanDetail set OrderCount=isnull(OrderCount,0)-@ProductCount");
            //                cmdsql.AppendLine(" where CompanyCD=@CompanyCD and PlanNo=@FromBillNo and SortNo=@FromLineNo");

            //                commp.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            //                commp.Parameters.Add(SqlHelper.GetParameter("@ProductCount", ProductCount[i].ToString()));
            //                commp.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", FromBillNo[i].ToString()));
            //                commp.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", FromLineNo[i].ToString()));
            //                commp.CommandText = cmdsql.ToString();
            //                listADD.Add(commp);
            //            }
            //        }
            //    }
            //}
            #endregion

            #region 删除采购合同明细
            System.Text.StringBuilder cmdddetail = new System.Text.StringBuilder();
            cmdddetail.AppendLine("DELETE  FROM officedba.PurchaseContractDetail WHERE  CompanyCD=@CompanyCD and ContractNo=@ContractNo");
            SqlCommand comn = new SqlCommand();
            comn.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comn.Parameters.Add(SqlHelper.GetParameter("@ContractNo", no));
            comn.CommandText = cmdddetail.ToString();
            listADD.Add(comn);
            #endregion



            try
            {
                #region 重新插入采购合同明细
                int lengs = Convert.ToInt32(length);
                if (lengs > 0)
                {
                    string[] ProductID = DetailProductID.Split(',');
                    string[] ProductNo = DetailProductNo.Split(',');
                    string[] ProductName = DetailProductName.Split(',');
                    string[] UnitID = DetailUnitID.Split(',');
                    string[] ProductCount = DetailProductCount.Split(',');
                    string[] UnitPrice = DetailUnitPrice.Split(',');//单价
                    string[] TaxPrice = DetailTaxPrice.Split(',');//含税价
                    string[] Discount = DetailDiscount.Split(',');//折扣
                    string[] TaxRate = DetailTaxRate.Split(',');//税率
                    string[] TotalPrice = DetailTotalPrice.Split(',');//金额
                    string[] TotalFee = DetailTotalFee.Split(',');//含税金额
                    string[] TotalTax = DetailTotalTax.Split(',');//税额
                    string[] RequireDate = DetailRequireDate.Split(',');
                    string[] ApplyReason = DetailApplyReason.Split(',');
                    string[] Remark = DetailRemark.Split(',');
                    string[] FromBillID = DetailFromBillID.Split(',');
                    string[] FromLineNo = DetailFromLineNo.Split(',');
                    string[] UsedUnitID = DetailUsedUnitID.Split(',');
                    string[] UsedUnitCount = DetailUsedUnitCount.Split(',');
                    string[] UsedPrice = DetailUsedPrice.Split(',');
                    string[] ExRate = DetailExRate.Split(',');




                    //SqlCommand comms = null;
                    //System.Text.StringBuilder cmdsql=null;
                    for (int i = 0; i < lengs; i++)
                    {
                        System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                        SqlCommand comms = new SqlCommand();
                        cmdsql.AppendLine("INSERT INTO officedba.PurchaseContractDetail");
                        cmdsql.AppendLine("(CompanyCD,");
                        cmdsql.AppendLine("ContractNo,");
                        cmdsql.AppendLine("SortNo,");
                        cmdsql.AppendLine("FromType,");
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("FromBillID,");
                            }
                        }
                        if (RequireDate[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(RequireDate[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("RequireDate,");
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("FromLineNo,");
                            }
                        }

                        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                        {
                            if (UsedUnitID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedUnitID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           UsedUnitID,                        ");
                                }
                            }
                            if (UsedUnitCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedUnitCount[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           UsedUnitCount ,                        ");
                                }
                            }
                            if (UsedPrice[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedPrice[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           UsedPrice,                        ");
                                }
                            }
                            if (ExRate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(ExRate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ExRate,                        ");
                                }
                            }
                        }


                        cmdsql.AppendLine("ProductID,");
                        cmdsql.AppendLine("ProductNo,");
                        cmdsql.AppendLine("ProductName,");
                        cmdsql.AppendLine("ProductCount,");
                        cmdsql.AppendLine("UnitID,");
                        cmdsql.AppendLine("UnitPrice,");
                        cmdsql.AppendLine("TaxPrice,");
                        //cmdsql.AppendLine("Discount,");
                        cmdsql.AppendLine("TaxRate,");
                        cmdsql.AppendLine("TotalFee,");
                        cmdsql.AppendLine("TotalPrice,");
                        cmdsql.AppendLine("TotalTax,");
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("Remark,");
                            }
                        }
                        cmdsql.AppendLine("ApplyReason)");
                        cmdsql.AppendLine(" Values(@CompanyCD");
                        cmdsql.AppendLine("            ,@ContractNo");
                        cmdsql.AppendLine("            ,@SortNo");
                        cmdsql.AppendLine("            ,@FromType"); 
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@FromBillID");
                            }
                        }
                        if (RequireDate[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(RequireDate[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@RequireDate");
                            }
                        }
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@FromLineNo");
                            }
                        }
                        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                        {
                            if (UsedUnitID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedUnitID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@UsedUnitID                        ");
                                }
                            }
                            if (UsedUnitCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedUnitCount[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@UsedUnitCount                         ");
                                }
                            }
                            if (UsedPrice[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedPrice[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@UsedPrice                        ");
                                }
                            }
                            if (ExRate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(ExRate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@ExRate                        ");
                                }
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
                        cmdsql.AppendLine("            ,@UnitID");
                        cmdsql.AppendLine("            ,@UnitPrice");
                        cmdsql.AppendLine("            ,@TaxPrice");
                        //cmdsql.AppendLine("            ,@Discount");
                        cmdsql.AppendLine("            ,@TaxRate");
                        cmdsql.AppendLine("            ,@TotalFee");
                        cmdsql.AppendLine("            ,@TotalPrice");
                        cmdsql.AppendLine("            ,@TotalTax");
                        if (Remark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(Remark[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@Remark2");
                            }
                        }
                        cmdsql.AppendLine("            ,@ApplyReason)");


                        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ContractNo", no));
                        comms.Parameters.Add(SqlHelper.GetParameter("@SortNo", i + 1));
                        comms.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
                        if (FromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromBillID[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@FromBillID", FromBillID[i].ToString()));
                            }
                        }
                        if (RequireDate[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(RequireDate[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@RequireDate", RequireDate[i].ToString()));
                            }
                        }

                        
                        if (FromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(FromLineNo[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", FromLineNo[i].ToString()));
                            }
                        }
                        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                        {
                            if (UsedUnitID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedUnitID[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", UsedUnitID[i].ToString()));
                                }
                            }
                            if (UsedUnitCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedUnitCount[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", UsedUnitCount[i].ToString()));
                                }
                            }
                            if (UsedPrice[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(UsedPrice[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", UsedPrice[i].ToString()));
                                }
                            }
                            if (ExRate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(ExRate[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@ExRate", ExRate[i].ToString()));
                                }
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
                        comms.Parameters.Add(SqlHelper.GetParameter("@UnitID", UnitID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UnitPrice", UnitPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxPrice", TaxPrice[i].ToString()));
                        //comms.Parameters.Add(SqlHelper.GetParameter("@Discount", Discount[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxRate", TaxRate[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalFee", TotalFee[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", TotalPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalTax", TotalTax[i].ToString()));
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



        #region 查询：合同记录
        ///<summary>
        /// 查询合同记录
        ///</summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="employeeNo">人员编号</param>
        ///<return>DataTable</return>
        private static DataTable GetHistoryInfo(string companyCD, string contractNo)
        {

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ID                       ");
            sql.AppendLine("      ,CompanyCD                ");
            sql.AppendLine("      ,ContractNo               ");
            sql.AppendLine("      ,Title                    ");
            sql.AppendLine("      ,ProviderID               ");
            sql.AppendLine("      ,FromType                 ");
            sql.AppendLine("      ,FromBillID               ");
            sql.AppendLine("      ,TotalPrice               ");
            sql.AppendLine("      ,CurrencyType             ");
            sql.AppendLine("      ,Rate                     ");
            sql.AppendLine("      ,PayType                  ");
            sql.AppendLine("      ,SignDate                 ");
            sql.AppendLine("      ,Seller                   ");
            sql.AppendLine("      ,TheyDelegate          ");
            sql.AppendLine("      ,OurDelegate                ");
            sql.AppendLine("      ,Status                   ");
            sql.AppendLine("      ,Note                     ");
            sql.AppendLine("      ,TakeType                 ");
            sql.AppendLine("      ,CarryType                ");
            sql.AppendLine("      ,Attachment               ");
            sql.AppendLine("      ,remark                   ");
            sql.AppendLine("      ,BillStatus               ");
            sql.AppendLine("      ,Creator                  ");
            sql.AppendLine("      ,CreateDate               ");
            sql.AppendLine("      ,ModifiedDate             ");
            sql.AppendLine("      ,ModifiedUserID           ");
            sql.AppendLine("      ,Confirmor                ");
            sql.AppendLine("      ,ConfirmDate              ");
            sql.AppendLine("      ,Closer                   ");
            sql.AppendLine("      ,CloseDate                ");
            sql.AppendLine("  FROM officedba.PurchaseContract");
            sql.AppendLine(" WHERE CompanyCD = @CompanyCD   ");
            sql.AppendLine("   AND ContractNo = @ContractNo ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameterFromString("@CompanyCD", companyCD);
            //员工编号
            param[1] = SqlHelper.GetParameterFromString("@contractNo", contractNo);
            //执行查询
            DataTable data = SqlHelper.ExecuteSql(sql.ToString(), param);

            return data;
        }
        #endregion


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

        #region 绑定原因
        public static DataTable GetDrpApplyReason()
        {
            string sql = "select ID,CodeName from officedba.CodeReasonType where CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'AND Flag = 22 AND UsedStatus = '1'";
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

        #region 删除：合同记录
        /// <summary>
        /// 删除合同记录
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeletePurchaseContract(string ID, string CompanyCD)
        {
            string[] sql = new string[2];
            sql[0] = "delete from officedba.PurchaseContract where CompanyCD='" + CompanyCD + "' and ContractNo=(select ContractNo from officedba.PurchaseContract where ID=" + ID + ")";
            sql[1] = "delete from officedba.PurchaseContract where CompanyCD='" + CompanyCD + "' and ID=" + ID;
            return SqlHelper.ExecuteTransForListWithSQL(sql);
        }
        #endregion


        #region 选择申请来源
        public static DataTable GetApplySelectDetail(int Provider00ID, string ProductNo, string ProductName, string FromBillNo, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID                                                                                  ");
            sql.AppendLine("      ,A.ProductID                                                                          ");
            sql.AppendLine("      ,isnull(E.ProdNo,'') AS   ProductNo                                                    ");
            sql.AppendLine("      ,A.ProductName                                                                         ");
            sql.AppendLine("      ,isnull(E.Specification,'') AS standard                                                 ");
            sql.AppendLine("      ,A.UnitID                                                                               ");
            sql.AppendLine("      ,ISNULL(C.CodeName,'') AS Unit                                                          ");
            sql.AppendLine("      ,A.ProductCount                                                                         ");
            sql.AppendLine("      ,Convert(numeric(12,2),isnull(E.TaxBuy,0)) AS   UnitPrice                                ");
            sql.AppendLine("      ,Convert(numeric(12,2),isnull(E.StandardBuy,0) )AS TaxPrice                                 ");
            sql.AppendLine("      ,isnull(E.Discount,0) AS  Discount                                                        ");
            sql.AppendLine("      ,Convert(numeric(12,2),isnull(E.InTaxRate ,0) )  AS  TaxRate                                 ");
            sql.AppendLine("      , isnull(CONVERT(varchar(100), A.RequireDate, 23),'') as RequireDate                       ");
            sql.AppendLine("      ,isnull(A.ApplyReason ,0) AS ApplyReasonID                                                 ");
            sql.AppendLine("      ,isnull(D.CodeName,'') AS   ApplyReason                                                    ");
            sql.AppendLine("      ,isnull(F.ID,0) AS FromBillID                                ");
            sql.AppendLine("      ,isnull(A.ApplyNo,'') AS FromBillNo                                                          ");
            sql.AppendLine("      ,A.SortNo  AS  FromLineNo                                                                   ");
            sql.AppendLine("      ,isnull(F.TypeID,0)  AS  TypeID                                                             ");
            sql.AppendLine("      ,isnull(F.ApplyUserID,0)  AS  Seller                                                         "); 
            sql.AppendLine("      ,isnull(G.EmployeeName,'')  AS  SellerName                                                    ");
            sql.AppendLine("      ,isnull(F.ApplyDeptID,0)  AS  DeptID                                                          ");
            sql.AppendLine("      ,isnull(H.DeptName,'')  AS  DeptIDName                                                         ");

            sql.AppendLine("      ,A.UsedUnitID, ISNULL(A.UsedUnitCount, 0) AS UsedUnitCount, ISNULL(ff.CodeName, '')   AS UsedUnitName ,isnull(HH.TypeName,'') as ColorName                                               ");

            sql.AppendLine("FROM officedba.PurchaseApplyDetail AS A                                                              ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS C ON A.CompanyCD = C.CompanyCD AND A.UnitID = C.ID    ");
            sql.AppendLine("LEFT JOIN officedba.CodeReasonType AS D ON D.CompanyCD = A.CompanyCD AND A.ApplyReason = D.ID    ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS E ON A.CompanyCD = E.CompanyCD AND A.ProductID = E.ID    ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseApply AS F ON A.CompanyCD = F.CompanyCD AND A.ApplyNo = F.ApplyNo    ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS G ON F.ApplyUserID = G.ID ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS H ON F.ApplyDeptID = H.ID ");
            sql.AppendLine("LEFT OUTER JOIN officedba.CodeUnitType AS ff ON A.UsedUnitID = ff.ID   ");
            sql.AppendLine("left join officedba.CodePublicType HH on E.ColorID=HH.ID");
            
            sql.AppendLine("WHERE F.BillStatus = 2 AND F.CompanyCD = '"+CompanyCD+"'   ");
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
                sql.AppendLine(" AND F.ApplyNo like '%" + FromBillNo + "%'    ");
            }
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 选择计划来源
        public static DataTable GetSellOrderDetail(string ProductNo, string ProductName, string FromBillNo, string CompanyCD,int ProviderID)
        {
                        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ID                                                                      ");
            sql.AppendLine("      ,A.ProductID                                                                        ");
            sql.AppendLine("      ,isnull(E.ProdNo,'') AS   ProductNo                                                 ");
            sql.AppendLine("      ,isnull(E.ProductName,'') AS   ProductName                                          ");
            sql.AppendLine("      ,isnull(E.Specification,'') AS standard                                             ");
            sql.AppendLine("      ,A.UnitID                                                                           ");
            sql.AppendLine("      ,isnull(C.CodeName,'') AS Unit                                                      ");
            sql.AppendLine("      ,isnull(FF.CodeName,'') AS UsedUnitName                                                      ");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.ProductCount,0)) as ProductCount                                 ");

            sql.AppendLine("      , Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.UnitPrice,0)) as UnitPrice             ");
            sql.AppendLine("      , Convert(numeric(14," + userInfo.SelPoint + "),isnull(E.StandardBuy,0)) as TaxPrice                               ");
            sql.AppendLine("      ,  Convert(numeric(12," + userInfo.SelPoint + "),isnull(E.Discount,0)) as Discount                                         ");
            sql.AppendLine("      , Convert(numeric(12," + userInfo.SelPoint + "),isnull(E.TaxRate,0)) as TaxRate                           ");
            sql.AppendLine("      , Convert(numeric(12," + userInfo.SelPoint + "),isnull(A.TotalPrice,0)) as TotalPrice                      ");
            sql.AppendLine("      ,isnull( CONVERT(varchar(100), A.RequireDate, 23),'') as RequireDate                 ");
            sql.AppendLine("      ,isnull(B.ApplyReason , 0)  AS ApplyReasonID                                         ");
            sql.AppendLine("      ,isnull(D.CodeName ,'') AS   ApplyReason                                              ");
            sql.AppendLine("      ,isnull(F.ID,0) AS FromBillID                                                        ");
            sql.AppendLine("      ,isnull(A.PlanNo,'') AS FromBillNo                                                     ");
            sql.AppendLine("      ,isnull(A.SortNo,0)  AS  FromLineNo                                                    ");
            sql.AppendLine("      ,isnull(F.TypeID,0)  AS  TypeID                                                       ");
            sql.AppendLine("      ,isnull(F.Purchaser,0)  AS  Seller                                                    ");
            sql.AppendLine("      ,isnull(G.EmployeeName,'')  AS  SellerName                                             ");
            sql.AppendLine("      ,isnull(F.PlanDeptID,0)  AS  DeptID                                                    ");
            sql.AppendLine("      ,isnull(H.DeptName,'')  AS  DeptIDName                                                 ");
            sql.AppendLine("      ,     Convert(numeric(14," + userInfo.SelPoint + "),isnull(A.OrderCount,0)) as OrderCount                                                  ");
            sql.AppendLine("      ,     Convert(numeric(14," + userInfo.SelPoint + "),isnull(A.UsedUnitCount,0)) as UsedUnitCount                                                  ");
            sql.AppendLine("      ,     Convert(numeric(14," + userInfo.SelPoint + "),isnull(A.UsedPrice,0)) as UsedPrice                                                  ");
            sql.AppendLine("      ,  isnull(A.UsedUnitID,0)        as UsedUnitID                                       ");

            sql.AppendLine("      ,isnull(I.CustName,'') AS ProviderName ,B.ProviderID,isnull(HH.TypeName,'') as ColorName                                    ");
            sql.AppendLine("FROM officedba.PurchasePlanDetail AS A                                                        ");
            sql.AppendLine("LEFT JOIN officedba.PurchasePlanSource AS B ON A.CompanyCD = B.CompanyCD AND A.PlanNo = B.PlanNo  and a.SortNo=b.sortNo ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS C ON A.CompanyCD = C.CompanyCD AND A.UnitID = C.ID        ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS FF ON A.CompanyCD = FF.CompanyCD AND A.UsedUnitID = FF.ID        ");
            sql.AppendLine("LEFT JOIN officedba.CodeReasonType AS D ON D.CompanyCD = B.CompanyCD AND B.ApplyReason = D.ID ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS E ON A.CompanyCD = E.CompanyCD AND A.ProductID = E.ID      ");
            sql.AppendLine("LEFT JOIN officedba.PurchasePlan AS F ON A.CompanyCD = F.CompanyCD AND A.PlanNo = F.PlanNo    ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS G ON F.Purchaser = G.ID                                   ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS H ON F.PlanDeptID = H.ID                                      ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS I ON B.CompanyCD = I.CompanyCD AND B.ProviderID = I.ID    ");
            sql.AppendLine("left join officedba.CodePublicType HH on E.ColorID=HH.ID    ");
            sql.AppendLine("WHERE F.BillStatus = 2  AND F.CompanyCD = '" + CompanyCD + "' ");
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
                sql.AppendLine(" AND F.PlanNo like '%" + FromBillNo + "%'    ");
            }
            if (ProviderID != 0)
            {
                sql.AppendLine(" AND B.ProviderID =" + ProviderID + " ");
            }
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion


        #region 选择询价来源
        public static DataTable GetAskPriceOrderDetail(string ProductNo, string ProductName, string FromBillNo, string CompanyCD, int ProviderID,int CurrencyTypeID, string Rate)
        {
                       
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ID                                                                          ");
            sql.AppendLine("      ,A.ProductID                                                                            ");
            sql.AppendLine("      ,isnull(E.ProdNo,'') AS   ProductNo                                                     ");
            sql.AppendLine("      ,A.ProductName                                                                           ");
            sql.AppendLine("      ,isnull(E.Specification,'') AS standard                                                  ");
            sql.AppendLine("      ,A.UnitID                                                                                ");
            sql.AppendLine("      ,isnull(C.CodeName,'') AS Unit                                                           ");
            sql.AppendLine("      ,A.ProductCount                                                                          ");
            sql.AppendLine("      ,     Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.UnitPrice,0)) as UnitPrice                     ");
            sql.AppendLine("      ,       Convert(numeric(22," + userInfo.SelPoint + "),isnull(E.StandardBuy,0)) as TaxPrice                                ");
            sql.AppendLine("      ,              Convert(numeric(12," + userInfo.SelPoint + "),isnull(E.InTaxRate,0)) as TaxRate                            ");
            sql.AppendLine("      ,      Convert(numeric(22," + userInfo.SelPoint + "),isnull(A.TotalPrice,0)) as TotalPrice");
            sql.AppendLine("      ,     Convert(numeric(22," + userInfo.SelPoint + "),isnull(A.TotalFee,0)) as TotalFee                        ");
            sql.AppendLine("      ,      Convert(numeric(22," + userInfo.SelPoint + "),isnull(A.TotalTax,0)) as TotalTax                             ");
            sql.AppendLine("      , isnull(CONVERT(varchar(100), A.RequireDate, 23),'') as RequireDate                     ");
            sql.AppendLine("      ,isnull(A.ApplyReason ,0)  AS ApplyReasonID                                               ");
            sql.AppendLine("      ,isnull(D.CodeName,'') AS   ApplyReason                                                   ");
            sql.AppendLine("      ,isnull(A.Remark,'') AS  Remark ,isnull(F.ID,0) AS FromBillID                             ");
            sql.AppendLine("      ,isnull(A.AskNo,'') AS FromBillNo                                                         ");
            sql.AppendLine("      ,A.SortNo  AS  FromLineNo                                                                 ");
            sql.AppendLine("      ,isnull(F.AskUserID,0)  AS  Seller                                                        ");
            sql.AppendLine("      ,isnull(G.EmployeeName,'')  AS  SellerName                                                 ");
            sql.AppendLine("      ,isnull(F.DeptID,0)  AS  DeptID                                                            ");
            sql.AppendLine("      ,isnull(H.DeptName,'')  AS  DeptIDName                                                     ");
            sql.AppendLine("      ,isnull(F.isAddTax,0) AS isAddTax                                                           ");
            sql.AppendLine("      ,isnull(F.ProviderID,0) AS ProviderID,isnull(I.CustName,'') AS ProviderName                 ");
            sql.AppendLine("      ,isnull(F.CurrencyType,0) AS CurrencyType,isnull(J.CurrencyName,'') AS  CurrencyTypeName    ");
            sql.AppendLine("      ,isnull(F.Rate,0) AS Rate                                                                      ");

            sql.AppendLine("      ,A.UsedUnitID                                                        ");
            sql.AppendLine("      ,           Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.UsedUnitCount,0)) as UsedUnitCount                                                         ");
            sql.AppendLine("      ,              Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.UsedPrice,0)) as UsedPrice                                                      ");
            sql.AppendLine("      ,          Convert(numeric(12," + userInfo.SelPoint + "),isnull(a.ExRate,0)) as ExRate                                                      ");
            sql.AppendLine("      ,isnull(FF.CodeName,'') AS UsedUnitName,isnull(HH.TypeName,'') as ColorName                                                           ");

            sql.AppendLine("FROM officedba.PurchaseAskPriceDetail AS A                                                        ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS C ON A.CompanyCD = C.CompanyCD AND A.UnitID = C.ID            ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS FF ON A.CompanyCD = FF.CompanyCD AND A.UsedUnitID =FF.ID            ");
            sql.AppendLine("LEFT JOIN officedba.CodeReasonType AS D ON D.CompanyCD = A.CompanyCD AND A.ApplyReason = D.ID    ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS E ON A.CompanyCD = E.CompanyCD AND A.ProductID = E.ID         ");
            sql.AppendLine("left join officedba.CodePublicType HH on E.ColorID=HH.ID ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseAskPrice AS F ON A.CompanyCD = F.CompanyCD AND A.AskNo = F.AskNo     ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS G ON F.AskUserID = G.ID                                      ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS H ON F.DeptID = H.ID                                             ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS I ON F.CompanyCD = I.CompanyCD AND F.ProviderID = I.ID       ");
            sql.AppendLine("LEFT JOIN officedba.CurrencyTypeSetting AS J ON F.CompanyCD = J.CompanyCD AND F.CurrencyType = J.ID ");
            sql.AppendLine("WHERE F.BillStatus = 2  AND F.CompanyCD = '" + CompanyCD + "'     ");
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
                sql.AppendLine(" AND F.AskNo like '%" + FromBillNo + "%'    ");
            }
            if(ProviderID != 0)
            {
                sql.AppendLine(" AND F.ProviderID =" + ProviderID + " ");
            }
            sql.AppendLine(" AND F.CurrencyType = " + CurrencyTypeID + "  ");
            if (Rate != "0")
            {
                sql.AppendLine(" AND F.Rate = " + Rate + "   ");
            }
            return SqlHelper.ExecuteSql(sql.ToString());

        }
        #endregion


        #region 查询合同列表所需数据
        public static DataTable SelectContractList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ContractNo1, string Title, string TypeID, string DeptID, string Seller, string FromType, string ProviderID, string BillStatus, string UsedStatus,string  EFIndex ,string EFDesc )
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ID ,A.ContractNo ,isnull(A.Title,'') AS Title ,A.TypeID,isnull(B.TypeName,'') AS TypeName,A.DeptID ,isnull(D.DeptName,'') AS DeptName,A.Seller,isnull(C.EmployeeName,'')  AS EmployeeName,isnull( A.ModifiedDate,'') AS ModifiedDate ");
            sql.AppendLine("      ,case A.FromType when '0' then '无来源' when '1' then '采购申请'                                 ");
            sql.AppendLine("       when '2' then '采购计划' when '3' then '采购询价单' end as FromTypeName,A.ProviderID ,isnull(E.CustName,'') AS  CustName   ");
            sql.AppendLine("      ,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'                ");
            sql.AppendLine("       when '4' then '手工结单' when '5' then '自动结单' end AS BillStatusName ,A.Rate                        ");
            sql.AppendLine("      ,Convert(numeric(12,2),A.TotalPrice) AS TotalPrice,Convert(numeric(12,2),A.TotalTax) AS TotalTax,Convert(numeric(12,2),A.TotalFee) AS TotalFee,A.BillStatus   ");
            sql.AppendLine("      ,isnull(case F.FlowStatus when '0' then '请选择' when '1' then '待审批' when '2' then '审批中'    ");
            sql.AppendLine("      when '3'  then '审批通过' when '4' then '审批不通过' when '5' then '撤消审批' end,'')  AS  UsedStatus                ");
            sql.AppendLine(" ,isnull(case(select count(1) from officedba.PurchaseOrderDetail AS G,officedba.PurchaseOrder AS H where G.CompanyCD=A.CompanyCD and G.FromBillID = A.ID  and G.FromType='4' and G.OrderNo = H.OrderNo ) when 0 then 'False' end , 'True') AS Isyinyong");
            sql.AppendLine(" FROM officedba.PurchaseContract AS A                                                                   ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS B ON A.CompanyCD = B.CompanyCD AND B.TypeFlag=7 AND B.TypeCode=5 AND A.TypeID=B.ID");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Seller=C.ID          ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS D ON A.CompanyCD = D.CompanyCD AND A.DeptID=D.ID                  ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS E  ON  A.CompanyCD = E.CompanyCD AND A.ProviderID = E.ID              ");
            sql.AppendLine("LEFT JOIN officedba.FlowInstance AS F ON  A.CompanyCD = F.CompanyCD  AND F.BillTypeFlag = 6            ");
            sql.AppendLine("AND F.BillTypeCode = 8 AND F.BillNo = A.ContractNo                                                         ");
            sql.AppendLine(" AND F.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = 6 AND F.BillTypeCode = 8 )");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
            if (ContractNo1 != "" && ContractNo1 != null)
            {
                sql.AppendLine(" AND A.ContractNo like '%"+  @ContractNo1 +"%' ");
            }
            if (Title != null && Title != "")
            {
                sql.AppendLine(" AND A.Title like'%" + @Title + "%'  ");
            }
            if (TypeID != null && TypeID != "")
            {
                sql.AppendLine(" AND A.TypeID =@TypeID");
            }
            if (DeptID != "" && DeptID != null)
            {
                sql.AppendLine(" AND A.DeptID=@DeptID ");
            }
            if (Seller != null && Seller != "")
            {
                sql.AppendLine(" AND A.Seller =@Seller");
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

            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine(" and a.ExtField" + EFIndex + " LIKE @EFDesc");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractNo1", ContractNo1));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", Title));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", TypeID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Seller", Seller));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", FromType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", UsedStatus));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);

            //sql.AppendLine("WHERE A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
            //if (!string.IsNullOrEmpty(str[0]))
            //{
            //    sql.AppendLine("     AND A.ContractNo = '" + str[0].ToString().Trim() + "'              ");
            //}
            //if (!string.IsNullOrEmpty(str[1]))
            //{
            //    sql.AppendLine("	 AND A.Title like '%" + str[1].ToString().Trim() + "%'                    ");
            //}
            //if (!string.IsNullOrEmpty(str[2]))
            //{
            //    sql.AppendLine("	 AND A.TypeID = " + Convert.ToInt32(str[2].ToString().Trim()) + "     ");
            //}
            //if (!string.IsNullOrEmpty(str[3]))
            //{
            //    sql.AppendLine("	 AND A.DeptID = " + Convert.ToInt32(str[3].ToString().Trim()) + "     ");
            //}
            //if (!string.IsNullOrEmpty(str[4]))
            //{
            //    sql.AppendLine("	 AND A.Seller = " + Convert.ToInt32(str[4].ToString().Trim()) + "      ");
            //}
            //if (!string.IsNullOrEmpty(str[5]))
            //{
            //    sql.AppendLine("	 AND A.FromType = '" + str[5].ToString().Trim() + "'                  ");
            //}
            //if (!string.IsNullOrEmpty(str[6]))
            //{
            //    sql.AppendLine("	 AND A.ProviderID = " + Convert.ToInt32(str[6].ToString().Trim()) + " ");
            //}
            //if (!string.IsNullOrEmpty(str[7]))
            //{
            //    sql.AppendLine("	 AND A.BillStatus = '" + str[7].ToString().Trim() + "'                   ");
            //}
            //if (!string.IsNullOrEmpty(str[8]))
            //    sql.AppendLine("	 AND F.FlowStatus = '" + str[8].ToString().Trim() + " '                 ");

            //return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion


        #region 查找加载单个合同记录
        public static DataTable SelectContract(int ID)
        {
 

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.ContractNo ,isnull(A.Title,'') as Title  ,A.ProviderID,isnull(B.CustName,'') AS ProviderName ,A.FromType,A.PayType,isnull(C.TypeName,'') AS PayTypeName,A.MoneyType,isnull(M.TypeName,'') AS  MoneyTypeName ");
            sql.AppendLine("     ,isnull(CONVERT(varchar(100),A.SignDate,23),'') AS  SignDate,A.Seller,isnull(D.EmployeeName,'') AS SellerName, A.DeptID,isnull(E.DeptName,'') AS DeptName            ");
            sql.AppendLine("     ,A.TheyDelegate,A.OurDelegate AS OurUserID,isnull(F.EmployeeName,'') AS OurUserName ,A.TakeType, isnull(G.TypeName,'') AS TakeTypeName,A.BillStatus         ");
            sql.AppendLine("     ,A.CarryType,isnull(H.TypeName,'') AS CarryTypeName ,  Convert(numeric(22," + userInfo.SelPoint + "),a.TotalPrice) as TotalPrice, Convert(numeric(22," + userInfo.SelPoint + "),a.TotalTax) as TotalTax,         Convert(numeric(22," + userInfo.SelPoint + "),a.TotalFee) as TotalFee ,Convert(numeric(12," + userInfo.SelPoint + "),a.Discount) as Discount,Convert(numeric(22," + userInfo.SelPoint + "),a.DiscountTotal) as DiscountTotal,Convert(numeric(22," + userInfo.SelPoint + "),a.RealTotal) as RealTotal,A.SignAddr ");
            sql.AppendLine("     ,A.isAddTax,case A.isAddTax when '0' then '否' when '1' then '是'end AS isAddTaxName ,Convert(numeric(22," + userInfo.SelPoint + "),a.CountTotal) as CountTotal,A.Attachment,isnull(A.remark,'') as remark,CONVERT(varchar(100),A.CreateDate,23) AS  CreateDate,CONVERT(varchar(100),A.ModifiedDate,23) AS  ModifiedDate");
            sql.AppendLine("      ,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'when '4' then '手工结单'                              ");
            sql.AppendLine("       when '5' then '自动结单' end AS BillStatusName , A.Creator AS CreatorID,isnull(P.EmployeeName,'') AS  CreatorName ");
            sql.AppendLine("     ,A.ModifiedUserID, A.Confirmor,J.EmployeeName AS ConfirmorName,isnull(CONVERT(varchar(100),A.ConfirmDate,23),'') AS  ConfirmDate ,isnull(A.CurrencyType,0) AS CurrencyType, isnull(N.CurrencyName,'') AS  CurrencyTypeName,isnull(A.Rate,0) AS Rate");
            sql.AppendLine("     ,A.Closer,isnull(K.EmployeeName,'') AS CloserName, isnull(CONVERT(varchar(100),A.CloseDate,23),'') AS  CloseDate, A.TypeID,isnull(L.TypeName,'') AS TypeIDName");
            sql.AppendLine("      ,case A.FromType when '0' then '无来源' when '1' then '采购申请'when '2' then '采购计划' when '3' then '采购询价单' end as FromTypeName  ");
            sql.AppendLine("      ,isnull(case O.FlowStatus when '0' then '请选择' when '1' then '待审批' when '2' then '审批中'    ");
            sql.AppendLine("      when '3'  then '审批通过' when '4' then '审批不通过' when '5' then '撤消审批' end,'')  AS  UsedStatus    ");
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
            sql.AppendLine(" FROM officedba.PurchaseContract AS A                                                                                                        ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo   AS B on A.CompanyCD = B.CompanyCD  AND A.ProviderID=B.ID                  ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS C ON A.CompanyCD = C.CompanyCD  AND A.PayType=C.ID                     ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON A.CompanyCD = D.CompanyCD AND A.Seller=D.ID                          ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS E ON A.CompanyCD = E.CompanyCD AND A.DeptID=E.ID                              ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS F ON A.CompanyCD = F.CompanyCD AND A.OurDelegate=F.ID                     ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS G ON A.CompanyCD = G.CompanyCD  AND A.TakeType=G.ID                     ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS H ON A.CompanyCD = H.CompanyCD  AND A.CarryType=H.ID                    ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS J ON A.CompanyCD = J.CompanyCD AND A.Confirmor=J.ID                       ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS K ON A.CompanyCD = K.CompanyCD AND A.Closer=K.ID                          ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS L ON A.CompanyCD = L.CompanyCD  AND A.TypeID=L.ID                       ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS M ON A.CompanyCD = M.CompanyCD  AND A.MoneyType=M.ID                    ");
            sql.AppendLine("LEFT JOIN officedba.CurrencyTypeSetting AS N ON A.CompanyCD = N.CompanyCD  AND A.CurrencyType=N.ID            ");
            sql.AppendLine("LEFT JOIN officedba.FlowInstance AS O ON  A.CompanyCD = O.CompanyCD  AND O.BillTypeFlag = 6                   ");
            sql.AppendLine("AND O.BillTypeCode = 8 AND O.BillNo = A.ContractNo                                                            ");
            sql.AppendLine(" AND O.ID=(SELECT max(ID) FROM officedba.FlowInstance AS O WHERE A.ID = O.BillID AND O.BillTypeFlag = 6 AND O.BillTypeCode = 8 )");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS P ON A.CompanyCD = P.CompanyCD AND A.Creator=P.ID                         ");

            sql.AppendLine("WHERE 1=1");
            sql.AppendLine(" AND A.ID =" + ID + "");



            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 查找加载合同明细
        public static DataTable Details(int ID)
        {
       

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  distinct  A.ID,A.CompanyCD,A.ContractNo ,isnull(A.FromBillID,0) AS FromBillID,isnull(A.FromLineNo,0) AS FromLineNo,A.ProductID ");
            sql.AppendLine(",A.ProductNo ,A.ProductName, Convert(numeric(14," + userInfo.SelPoint + "),a.ProductCount) as ProductCount,A.UnitID                     ");
            sql.AppendLine(",isnull(A.FromType,'') AS  FromType                                                        ");
            sql.AppendLine(",isnull(case A.FromType  when '2' then (select (g.productcount-g.ordercount) from officedba.PurchasePlan e left outer join officedba.PurchasePlanDetail  g  on e.id=A.FromBillID and e.planno=g.planno where  g.sortno=A.FromLineNo)   end,'0') as hiddProductCount                                                      ");
            sql.AppendLine(",         Convert(numeric(12," + userInfo.SelPoint + "),a.ExRate) as ExRate                                                ");
            sql.AppendLine(",Convert(numeric(14," + userInfo.SelPoint + "),a.UsedPrice) as UsedPrice                                              ");
            sql.AppendLine(",  Convert(numeric(14," + userInfo.SelPoint + "),a.UsedUnitCount) as UsedUnitCount                                                 ");
            sql.AppendLine(",isnull(A.UsedUnitID,0) AS  UsedUnitID                                                        ");
                    sql.AppendLine(",isnull(ff.CodeName,0) AS  UsedUnitName                                                        ");
         
            sql.AppendLine(",isnull(case A.FromType when '0' then ''                                                          ");
            sql.AppendLine(" when '1' then (select ApplyNo from officedba.PurchaseApply where ID=A.FromBillID)         ");
            sql.AppendLine(" when '2' then (select PlanNo from officedba.PurchasePlan where ID=A.FromBillID)           ");
            sql.AppendLine(" when '3' then (select AskNo from officedba.PurchaseAskPrice where ID=A.FromBillID)        ");
            sql.AppendLine("  end,'') AS FromBillNo ,A.SortNo,isnull(C.TaxRate,0) AS HidTaxRate                        ");
            sql.AppendLine(",case when A.UnitID is null then '' when A.UnitID ='' then ''                         ");
            sql.AppendLine("else (select CodeName from officedba.CodeUnitType where id=A.UnitID)end as UnitName   ");
            sql.AppendLine(",Convert(numeric(12,2),A.UnitPrice) AS UnitPrice ,Convert(numeric(12,2),A.TaxPrice) AS TaxPrice,Convert(numeric(12,2),A.Discount) AS Discount ,Convert(numeric(12,2),A.TaxRate) AS TaxRate,Convert(numeric(12,2),A.TotalFee) AS TotalFee,Convert(numeric(12,2),A.TotalPrice) AS TotalPrice,Convert(numeric(12,2),A.TotalTax ) AS  TotalTax ");
            sql.AppendLine(",isnull(CONVERT(varchar(100), A.RequireDate, 23),'') AS RequireDate,A.ApplyReason ,Convert(numeric(12,2),isnull(A.OrderCount,0)) AS OrderCount");
            sql.AppendLine(",case when A.ApplyReason is null then '' when A.ApplyReason ='' then ''               ");
            sql.AppendLine("else (select CodeName from officedba.CodeReasonType  where id=A.ApplyReason)end as ApplyReasonName ");
            sql.AppendLine(",isnull(A.Remark,'') AS Remark ,isnull(A.standard,'') AS standard,isnull(H.TypeName,'') as ColorName   FROM officedba.PurchaseContractDetail AS A     ");
            sql.AppendLine(" LEFT JOIN officedba.PurchaseContract AS B ON A.ContractNo = B.ContractNo AND A.CompanyCD = B.CompanyCD ");
            sql.AppendLine(" LEFT JOIN officedba.ProductInfo AS C ON A.CompanyCD = C.CompanyCD AND A.ProductID = C.ID ");
            sql.AppendLine("left join officedba.CodePublicType H on C.ColorID=H.ID");
                  sql.AppendLine(" LEFT JOIN officedba.CodeUnitType AS ff ON A.CompanyCD = ff.CompanyCD AND A.UsedUnitID = ff.ID ");
            sql.AppendLine("where 1=1 AND B.ID =" + ID + " ");
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion


        #region 删除采购合同主表及明细
        public static bool DeletePurchasePlanPrimary(string DetailNo)
        {
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
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.PurchaseContract WHERE ContractNo IN ( " + AllDetailNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.PurchaseContractDetail WHERE ContractNo IN ( " + AllDetailNo + " ) and CompanyCD='" + strCompanyCD + "'", null);

                tran.Commit();
                isSucc = true;

            }
            catch 
            {
                tran.Rollback();
                isSucc = false;
            }
            return isSucc;

            //#region SQL文
            //string strSql = "delete officedba.PurchaseContract where CompanyCD='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and ContractNo='" + ContractNo + "'";
            //#endregion

            //sql[i] = strSql;
        }
        #endregion

        #region 删除采购合同明细
        public static void DeletePurchasePlanDetail(string ContractNo, ref string[] sql, int i)
        {
            string strSql = "delete officedba.PurchaseContractDetail where CompanyCD='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and ContractNo='" + ContractNo + "'";

            sql[i] = strSql;
        }
        #endregion


        #region 确认合同
        public static bool ConfirmPurchaseContract(PurchaseContractModel Model, string DetailProductCount, string DetailFromBillNo, string DetailFromLineNo, string length, out string strMsg)
        {
            strMsg = "";
            bool result = true;
            if (isCanDo(Model.ID, "1"))
            {
                try
                {
                    ArrayList listADD = new ArrayList();
                    
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendLine("update officedba.PurchaseContract set ");
                    strSql.AppendLine(" Confirmor =  @Confirmor");
                    strSql.AppendLine(" ,BillStatus=@BillStatus");
                    strSql.AppendLine(" ,ConfirmDate=getdate()");
                    strSql.AppendLine(" where");
                    strSql.AppendLine(" CompanyCD= @CompanyCD");
                    strSql.AppendLine(" and ID= @ID");
                    SqlCommand comm = new SqlCommand();
                    comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", Model.Confirmor));
                    comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", "2"));
                    comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD));
                    comm.Parameters.Add(SqlHelper.GetParameter("@ID", Model.ID));
                    comm.CommandText = strSql.ToString();
                    listADD.Add(comm);

                    if (Model.FromType.Trim() == "2")
                    {
                        int lengs = Convert.ToInt32(length);
                        if (lengs > 0)
                        {
                            string[] ProductCount = DetailProductCount.Split(',');
                            string[] FromBillNo = DetailFromBillNo.Split(',');
                            string[] FromLineNo = DetailFromLineNo.Split(',');
                            string[]  UsedUnitCount={};
                             if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit){
                             UsedUnitCount=Model.Attachment.Split(',');
                             }
                            string pp = "";

                            for (int i = 0; i < lengs; i++)
                            {
                                if (FromLineNo[i].ToString() != "")
                                {
                                    if (IsCanConfirm(FromBillNo[i].ToString(), FromLineNo[i].ToString(), Model.CompanyCD, ProductCount[i].ToString()))
                                    {
                                        System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                                        SqlCommand comms = new SqlCommand();
                                        cmdsql.AppendLine("Update  Officedba.PurchasePlanDetail set OrderCount=isnull(OrderCount,0)+@ProductCount");
                                        cmdsql.AppendLine(" where CompanyCD=@CompanyCD and PlanNo=@FromBillNo and SortNo=@FromLineNo");

                                        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD));
                                        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                                        {
                                            comms.Parameters.Add(SqlHelper.GetParameter("@ProductCount", UsedUnitCount[i].ToString()));
                                        }
                                        else
                                        {
                                            comms.Parameters.Add(SqlHelper.GetParameter("@ProductCount", ProductCount[i].ToString()));
                                        }

                                    
                                        comms.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", FromBillNo[i].ToString()));
                                        comms.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", FromLineNo[i].ToString()));
                                        comms.CommandText = cmdsql.ToString();
                                        listADD.Add(comms);
                                    }
                                    else
                                    {
                                        if (pp != "")
                                        {
                                            pp += "," + (i + 1);
                                        }
                                        else {
                                            pp = "" + (i + 1);
                                        }
                                        strMsg += "第" + pp + "行的采购数量不能大于当前可用的采购数量,确认失败！";
                                        result = false;
                                    }
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
            }
            else
            {//已经被其他人确认
                strMsg = "已经确认的单据不可再次确认！";
                result = false;
                return result;
            }

        }
        #endregion

        #region 取消确认合同
        public static bool CancelConfirmPurchaseContract(PurchaseContractModel Model, string DetailProductCount, string DetailFromBillNo, string DetailFromLineNo, string length, out string strMsg)
        {
           
            bool isSuc = false;
            strMsg = "";
            //判断单据是否为执行状态，非执行状态不能取消确认
            if (isCanDo(Model.ID, "2"))
            {

                if (!IsCitePurchaseContract(Model.ID))
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

                    strSq = "update  officedba.PurchaseContract set BillStatus='1'  ";
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


                        if (Model.FromType.Trim() == "2")
                        {
                            int lengs = Convert.ToInt32(length);
                            if (lengs > 0)
                            {
                                string[] ProductCount = DetailProductCount.Split(',');
                                string[] FromBillNo = DetailFromBillNo.Split(',');
                                string[] FromLineNo = DetailFromLineNo.Split(',');
                                string[] UsedUnitCount = { };
                                if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                                {
                                    UsedUnitCount = Model.Attachment.Split(',');
                                }
                                for (int i = 0; i < lengs; i++)
                                {

                                    StringBuilder cmdsql = new StringBuilder();
                                    cmdsql.Append("Update  Officedba.PurchasePlanDetail set OrderCount=isnull(OrderCount,0)-@ProductCount");
                                    cmdsql.Append(" where CompanyCD=@CompanyCD and PlanNo=@FromBillNo and SortNo=@FromLineNo");
                                    if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                                    {
                                        SqlParameter[] param = { 
                                               new SqlParameter("@CompanyCD", Model.CompanyCD),
                                                new SqlParameter("@ProductCount", UsedUnitCount[i].ToString()),
                                                 new SqlParameter("@FromBillNo", FromBillNo[i].ToString()),
                                                  new SqlParameter("@FromLineNo", FromLineNo[i].ToString())
                                               };
                                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, cmdsql.ToString(), param);
                                    }
                                    else
                                    {
                                        SqlParameter[] param = { 
                                               new SqlParameter("@CompanyCD", Model.CompanyCD),
                                                new SqlParameter("@ProductCount", ProductCount[i].ToString()),
                                                 new SqlParameter("@FromBillNo", FromBillNo[i].ToString()),
                                                  new SqlParameter("@FromLineNo", FromLineNo[i].ToString())
                                               };
                                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, cmdsql.ToString(), param);
                                    }
                                 

                                }
                            }
                        }


                        FlowDBHelper.OperateCancelConfirm(Model.CompanyCD, Convert.ToInt32(ConstUtil.CODING_RULE_PURCHASE), Convert.ToInt32(ConstUtil.BILL_TYPEFLAG_PURCHASE_CONTRACT), Model.ID, strUserID, tran);//撤销审批
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

        #region 结单
        public static bool ClosePurchaseContract(PurchaseContractModel Model)
        {
            try
            {
                ArrayList listADD = new ArrayList();
                bool result = false;
                StringBuilder strSql = new StringBuilder();
                strSql.AppendLine("update officedba.PurchaseContract set ");
                strSql.AppendLine(" Closer =  @Closer");
                strSql.AppendLine(" ,BillStatus=@BillStatus");
                strSql.AppendLine(" ,CloseDate=getdate()");
                strSql.AppendLine(" where");
                strSql.AppendLine(" CompanyCD= @CompanyCD");
                strSql.AppendLine(" and ID= @ID");

                SqlCommand comm = new SqlCommand();
                comm.Parameters.Add(SqlHelper.GetParameter("@Closer", Model.Closer));
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", "4"));
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@ID", Model.ID));
                comm.CommandText = strSql.ToString();
                listADD.Add(comm);

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

        #region 取消结单
        public static bool CancelClosePurchaseContract(PurchaseContractModel Model)
        {
            try
            {
                ArrayList listADD = new ArrayList();
                bool result = false;
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("update officedba.PurchaseContract set ");
                sql.AppendLine("  Closer = null");
                sql.AppendLine(" ,CloseDate = null");
                sql.AppendLine(" ,BillStatus = @BillStatus");
                sql.AppendLine(" where CompanyCD= @CompanyCD");
                sql.AppendLine(" and ID= @ID");

                SqlCommand comm = new SqlCommand();
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", "2"));
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@ID", Model.ID));

                comm.CommandText = sql.ToString();
                listADD.Add(comm);

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

            strSql = "select count(1) from officedba.PurchaseContract where ID = @ID and BillStatus=@BillStatus ";
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

        #region 判断采购合同有没有被引用
        public static bool IsCitePurchaseContract(int ID)
        {
            bool IsCite = false;
            //采购订单引用
            if (!IsCite)
            {
                string sql = "SELECT A.ID FROM officedba.PurchaseOrderDetail AS A,officedba.PurchaseOrder AS B WHERE A.FromType=@FromType AND A.FromBillID=@ID AND A.OrderNo=B.OrderNo  ";
                SqlParameter[] parameters = {
                    new SqlParameter("@FromType", SqlDbType.VarChar),
					new SqlParameter("@ID", Convert.ToString(SqlDbType.Int)),};
                parameters[0].Value = "4";
                parameters[1].Value = ID;
                IsCite = SqlHelper.Exists(sql, parameters);
            }
            return IsCite;
        }
        #endregion

        #region 确认时判断数量可否进行确认
        public static bool IsCanConfirm(string FromBillNo, string FromLineNo, string CompanyCD, string YOrderCount)
        {


            try
            {
                StringBuilder sql = new StringBuilder();
                //string sql = "SELECT A.ProductCount FROM officedba.PurchaseOrderDetail AS A where CompanyCD=@CompanyCD and PlanNo=@FromBillNo and SortNo=@FromLineNo  ";


                sql.AppendLine("SELECT @Tmp=(Convert(Decimal,isnull(A.ProductCount,0))-Convert(Decimal,isnull(A.OrderCount,0))) FROM officedba.PurchasePlanDetail AS A where CompanyCD= '" + CompanyCD + "'");
                sql.AppendLine("and PlanNo='" + FromBillNo + "' and SortNo='" + FromLineNo + "' ");

                SqlParameter[] Paras = { 
                                   new SqlParameter("@Tmp",SqlDbType.Decimal)};
                Paras[0].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteSql(sql.ToString(), Paras);

                Decimal tmp = Convert.ToDecimal(Paras[0].Value);
                if (tmp >= Convert.ToDecimal(YOrderCount))
                {
                    return true;
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

        #region 采购报表采购合同查询
        public static DataTable PurchaseContractQuery(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProviderID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
                  
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            sql.AppendLine("SELECT distinct A.ID ,A.ContractNo ,isnull(A.Title,'') AS Title ,A.TypeID,isnull(B.TypeName,'') AS TypeName,A.Seller,isnull(C.EmployeeName,'')  AS EmployeeName ");
            sql.AppendLine(" ,isnull(A.ProviderID,0) AS ProviderID ,isnull(D.CustName,'') AS ProviderName, Convert(numeric(20," + userInfo.SelPoint + "),isnull(A.RealTotal,0)*isnull(A.Rate,0)) AS RealTotal,A.BillStatus           ");

            sql.AppendLine(" FROM officedba.PurchaseContract AS A                                                                   ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS B ON A.CompanyCD = B.CompanyCD AND B.TypeFlag=7 AND B.TypeCode=5 AND A.TypeID=B.ID");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Seller=C.ID          ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS D  ON  A.CompanyCD = D.CompanyCD AND A.ProviderID = D.ID              ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.BillStatus <>1 AND A.CompanyCD = '" + CompanyCD + "'");
            if (ProviderID != "" && ProviderID != "")
            {
                sql.AppendLine(" AND A.ProviderID=@ProviderID ");
            }
            if (StartConfirmDate != null && StartConfirmDate != "")
            {
                sql.AppendLine(" AND A.SignDate >= @StartConfirmDate");
            }
            if (EndConfirmDate != null && EndConfirmDate != "")
            {
                sql.AppendLine(" AND A.SignDate <= @EndConfirmDate ");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", EndConfirmDate));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);

        }
        #endregion

        #region 采购报表采购合同查询打印
        public static DataTable PurchaseContractQueryPrint(string ProviderID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ID ,A.ContractNo ,isnull(A.Title,'') AS Title ,A.TypeID,isnull(B.TypeName,'') AS TheyDelegate,A.Seller,isnull(C.EmployeeName,'')  AS SignAddr ");
            sql.AppendLine(" ,isnull(A.ProviderID,0) AS ProviderID ,isnull(D.CustName,'') AS remark, Convert(numeric(20,2),isnull(A.RealTotal,0)*isnull(A.Rate,0)) AS RealTotal,A.BillStatus           ");

            sql.AppendLine(" FROM officedba.PurchaseContract AS A                                                                   ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS B ON A.CompanyCD = B.CompanyCD AND B.TypeFlag=7 AND B.TypeCode=5 AND A.TypeID=B.ID");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Seller=C.ID          ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS D  ON  A.CompanyCD = D.CompanyCD AND A.ProviderID = D.ID              ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.BillStatus <>1 AND A.CompanyCD = '" + CompanyCD + "'");
            if (ProviderID != "" && ProviderID != "")
            {
                sql.AppendLine(" AND A.ProviderID=@ProviderID ");
            }
            if (StartConfirmDate != null && StartConfirmDate != "")
            {
                sql.AppendLine(" AND A.SignDate >= @StartConfirmDate");
            }
            if (EndConfirmDate != null && EndConfirmDate != "")
            {
                sql.AppendLine(" AND A.SignDate <= @EndConfirmDate ");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", EndConfirmDate));

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

    }
}

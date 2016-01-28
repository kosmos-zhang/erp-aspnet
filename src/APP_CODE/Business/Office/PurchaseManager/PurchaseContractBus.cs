/***********************************************
 * 类作用：   采购管理事务层处理               *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/03/25                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Model.Office.PurchaseManager;
using XBase.Data.Office.PurchaseManager;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;
using System.Data.SqlTypes;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Data.Office.FinanceManager;
using System.Collections ;
namespace XBase.Business.Office.PurchaseManager
{
    // <summary>
    /// 类名：PurchaseContractBus
    /// 描述：采购管理事务层处理
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/03/26
    /// </summary>
    public class PurchaseContractBus
    {
        #region 新增：合同记录
        /// <summary>
        /// 新增合同
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CompanyNo"></param>
        /// <returns></returns>
        public static bool InsertPurchaseContract(PurchaseContractModel model, string DetailProductID, string DetailProductNo, string DetailProductName, string DetailUnitID, string DetailProductCount, string DetailUnitPrice, string DetailTaxPrice, string DetailDiscount, string DetailTaxRate, string DetailTotalPrice, string DetailTotalFee, string DetailTotalTax, string DetailRequireDate, string DetailApplyReason, string DetailRemark, string DetailFromBillID, string DetailFromLineNo,string DetailUsedUnitID ,string DetailUsedUnitCount ,string DetailUsedPrice ,string DetailExRate,  string length, out string ID,Hashtable  ht)
        {
            try
            {
                ////获取登陆用户ID
                //string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                //return PurchaseContractDBHelper.InsertPurchaseContract(model, DetailProductID, DetailProductNo, DetailProductName, DetailUnitID, DetailProductCount, DetailUnitPrice, DetailTaxPrice, DetailDiscount, DetailTaxRate, DetailTotalPrice, DetailTotalFee, DetailTotalTax, DetailRequireDate, DetailApplyReason, DetailRemark, DetailFromBillID, DetailFromLineNo, length, out ID);

                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.ContractNo);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                //设置模块ID 模块ID请在ConstUtil中定义，以便维护
                logModel.ModuleID = ConstUtil.MODULE_ID_PurchaseContract_Add;
                succ = PurchaseContractDBHelper.InsertPurchaseContract(model, DetailProductID, DetailProductNo, DetailProductName, DetailUnitID, DetailProductCount, DetailUnitPrice, DetailTaxPrice, DetailDiscount, DetailTaxRate, DetailTotalPrice, DetailTotalFee, DetailTotalTax, DetailRequireDate, DetailApplyReason, DetailRemark, DetailFromBillID, DetailFromLineNo, DetailUsedUnitID, DetailUsedUnitCount, DetailUsedPrice, DetailExRate, length, out ID, ht);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            #region
            //string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            ////model.CompanyCD = CompanyCD;
            ////string CompanyCD = "AAAAAA";
            //try
            //{
            //    ////int length = 3 + str.Length + str2.Length;//SQL语句长度
            //    ////string[] sql = new string[length];
            //    //return PurchaseContractDBHelper.InsertPurchaseContract(model);
                
            //    ////for (int i = 0; i < str.Length; ++i)
            //    ////{
            //    ////    PurchaseContractDBHelper.InsertDtlS(model,DetailID, DetailProductNo, DetailProductName, DetailProductCount, DetailUnitID, DetailRequireDate, DetailUnitPrice, DetailTotalPrice, DetailApplyReason, DetailRemark, DetailFromBillID, DetailFromLineNo, length);
            //    ////}
            //    ////SqlHelper.ExecuteTransForListWithSQL(sql);
            //    ////return SqlHelper.Result.OprateCount > 0 ? true : false;
            //    int Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            //    bool result = false;

             

            //    #region 合同添加SQL语句
            //    //SQL拼写
            //    StringBuilder sqlc = new StringBuilder();
            //    SqlDateTime signdate = model.SignDate == null ? SqlDateTime.Null : SqlDateTime.Parse(model.SignDate.ToString());
            //    SqlDateTime confirmdate = model.ConfirmDate == null ? SqlDateTime.Null : SqlDateTime.Parse(model.ConfirmDate.ToString());
            //    SqlDateTime closedate = model.CloseDate == null ? SqlDateTime.Null : SqlDateTime.Parse(model.CloseDate.ToString());
            //    SqlDateTime createdate = model.CreateDate == null ? SqlDateTime.Null : SqlDateTime.Parse(model.CreateDate.ToString());
            //    sqlc.AppendLine("Insert into officedba.PurchaseContract");
            //    sqlc.AppendLine("(CompanyCD,ContractNo,Title,ProviderID,FromType,FromBillID,");
            //    sqlc.AppendLine("TotalPrice,CurrencyType,Rate,PayType,SignDate,");
            //    sqlc.AppendLine("Seller,TheyDelegate,OurDelegate,Status,Note,");
            //    sqlc.AppendLine("TakeType,CarryType,Attachment,remark,BillStatus,Creator,");
            //    sqlc.AppendLine("CreateDate,ModifiedDate,ModifiedUserID,Confirmor,ConfirmDate,Closer,CloseDate,");
            //    sqlc.AppendLine("DeptID,TotalTax,TotalFee,Discount,DiscountTotal,RealTotal,isAddTax,CountTotal,TypeID,SignAddr,MoneyType)");
            //    sqlc.AppendLine("values('" + model.CompanyCD + "','" + model.ContractNo + "','" + model.Title + "'," + model.ProviderID + ",'" + model.FromType + "'");
            //    sqlc.AppendLine("," + model.FromBillID + "," + model.TotalPrice + "," + model.CurrencyType + "," + model.Rate + "," + model.PayType + "");
            //    if (signdate.IsNull)
            //    {
            //        sqlc.AppendLine("," + signdate + "");
            //    }
            //    else
            //    {
            //        sqlc.AppendLine(",'"+signdate+"'");
            //    }
            //    sqlc.AppendLine("," + model.Seller + ",'" + model.TheyDelegate + "'," + model.OurDelegate + ",'" + model.Status + "'");
            //    sqlc.AppendLine(",'" + model.Note + "'," + model.TakeType + "," + model.CarryType + ",'" + model.Attachment + "','" + model.remark + "'");
            //    sqlc.AppendLine(", '" + model.BillStatus + "'," + Creator+ "");
            //    if (createdate.IsNull)
            //    {
            //        sqlc.AppendLine(","+createdate+"");
            //    }
            //    else
            //    {
            //        sqlc.AppendLine(",'" + createdate + "'");
            //    }
            //    sqlc.AppendLine(",getdate(),'" + model.ModifiedUserID + "'," + model.Confirmor + "");
            //    if (confirmdate.IsNull)
            //    {
            //        sqlc.AppendLine("," + confirmdate + "");
            //    }
            //    else
            //    {
            //        sqlc.AppendLine(",'"+confirmdate+"'");
            //    }
            //    sqlc.AppendLine("," + model.Closer + "");
            //    if (closedate.IsNull)
            //    {
            //        sqlc.AppendLine(","+closedate+"");
            //    }
            //    else
            //    {
            //        sqlc.AppendLine(",'"+closedate+"'");
            //    }
            //    sqlc.AppendLine("," + model.DeptID + "," + model.TotalTax + "," + model.TotalFee + "," + model.Discount + "," + model.DiscountTotal + "," + model.RealTotal + ",'" + model.isAddTax + "'," + model.CountTotal + "," + model.TypeID + ",'" + model.SignAddr + "','" + model.MoneyType + "')");

            //   // sqlc.AppendLine(",'" + model.BillStatus + "'," + model.Creator + ", '"+createdate+"',getdate(),'" + model.ModifiedUserID + "'");
            //   // sqlc.AppendLine("," + model.Confirmor + "," +confirmdate+ "," + model.Closer + ","+closedate+" )");
            //    #endregion
            //    string[] sql = null;
            //    int lengs = Convert.ToInt32(length);
            //    if (lengs > 0)
            //    {
            //        sql = new string[lengs+1];
            //        sql[0] = sqlc.ToString();
            //        string[] ProductID = DetailProductID.Split(',');
            //        string[] ProductNo = DetailProductNo.Split(',');
            //        string[] ProductName = DetailProductName.Split(',');
            //        string[] Standard = DetailStandard.Split(',');//规格
            //        string[] UnitID = DetailUnitID.Split(',');
            //        string[] ProductCount = DetailProductCount.Split(',');
            //        string[] UnitPrice = DetailUnitPrice.Split(',');//单价
            //        string[] TaxPrice = DetailTaxPrice.Split(',');//含税价
            //        string[] Discount = DetailDiscount.Split(',');//折扣
            //        string[] TaxRate = DetailTaxRate.Split(',');//税率
            //        string[] TotalPrice = DetailTotalPrice.Split(',');//金额
            //        string[] TotalFee = DetailTotalFee.Split(',');//含税金额
            //        string[] TotalTax = DetailTotalTax.Split(',');//税额
            //        string[] RequireDate = DetailRequireDate.Split(',');
            //        string[] ApplyReason = DetailApplyReason.Split(',');
            //        string[] Remark = DetailRemark.Split(',');
            //        string[] FromBillID = DetailFromBillID.Split(',');
            //        string[] FromLineNo = DetailFromLineNo.Split(',');
            //        for (int i = 1; i <=lengs; i++)
            //        {
            //            SqlDateTime mRequireDate;
            //            decimal mTotalPrice, mUnitID;
            //            decimal mUnitPrice, mProductCount, mTaxPrice, mDiscount, mTaxRate, mTotalFee, mTotalTax;
            //            //if (RequireDate[i-1].ToString() == "")
            //            //{
            //            //    mRequireDate = SqlDateTime.Null;
            //            //}
            //            //else
            //            //{
            //            //    mRequireDate = SqlDateTime.Parse(RequireDate[i-1].ToString());
            //            //}
            //            if (UnitPrice[i - 1].ToString() == "")
            //            {
            //                mUnitPrice = 0;
            //            }
            //            else
            //            {
            //                mUnitPrice = Convert.ToDecimal(UnitPrice[i - 1].ToString());
            //            }
            //            if (ProductCount[i - 1].ToString() == "")
            //            {
            //                mProductCount = 0;
            //            }
            //            else
            //            {
            //                mProductCount = Convert.ToDecimal(ProductCount[i - 1].ToString());
            //            }
            //            if (TotalPrice[i - 1].ToString() == "")
            //            {
            //                mTotalPrice = 0;
            //            }
            //            else
            //            {
            //                mTotalPrice = Convert.ToDecimal(TotalPrice[i - 1].ToString());
            //            }
            //            if (TaxPrice[i - 1].ToString() == "")
            //            {
            //                mTaxPrice = 0;
            //            }
            //            else
            //            {
            //                mTaxPrice = Convert.ToDecimal(TaxPrice[i - 1].ToString());
            //            }
            //            if (Discount[i - 1].ToString() == "")
            //            {
            //                mDiscount = 0;
            //            }
            //            else
            //            {
            //                mDiscount = Convert.ToDecimal(Discount[i - 1].ToString());
            //            }
            //            if (TaxRate[i - 1].ToString() == "")
            //            {
            //                mTaxRate = 0;
            //            }
            //            else
            //            {
            //                mTaxRate = Convert.ToDecimal(TaxRate[i - 1].ToString());
            //            }
            //            if (TotalFee[i - 1].ToString() == "")
            //            {
            //                mTotalFee = 0;
            //            }
            //            else
            //            {
            //                mTotalFee = Convert.ToDecimal(TotalFee[i - 1].ToString());
            //            }
            //            if (TotalTax[i - 1].ToString() == "")
            //            {
            //                mTotalTax = 0;
            //            }
            //            else
            //            {
            //                mTotalTax = Convert.ToDecimal(TotalTax[i - 1].ToString());
            //            }
            //            if (UnitID[i - 1].ToString() == "")
            //            {
            //                mUnitID = 0;
            //            }
            //            else
            //            {
            //                mUnitID = Convert.ToDecimal(UnitID[i - 1].ToString());
            //            }
            //            System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
            //            cmdsql.AppendLine(" Insert into  officedba.PurchaseContractDetail(CompanyCD,ContractNo,ProductID,ProductNo,ProductName,standard,UnitID,ProductCount,");
            //            cmdsql.AppendLine("UnitPrice,TaxPrice,Discount,TaxRate,TotalPrice,TotalFee,TotalTax,RequireDate,ApplyReason,Remark,FromBillID,FromLineNo)");
            //            cmdsql.AppendLine(" Values('" + model.CompanyCD + "','" + model.ContractNo + "','" + ProductID[i - 1].ToString() + "','" + ProductNo[i - 1].ToString() + "','" + ProductName[i - 1].ToString() + "','" + Standard[i - 1].ToString() + "'");
            //            cmdsql.AppendLine(",'" + mUnitID + "','" + mProductCount + "','" + mUnitPrice + "','" + mTaxPrice + "','" + mDiscount + "','" + mTaxRate + "','" + mTotalPrice + "','" + mTotalFee + "','" + mTotalTax + "'");
            //            if (RequireDate[i - 1].ToString() == "")
            //            {
            //                mRequireDate = SqlDateTime.Null;
            //                cmdsql.AppendLine(","+mRequireDate+"");
            //            }
            //            else
            //            {
            //                mRequireDate = SqlDateTime.Parse(RequireDate[i - 1].ToString());
            //                cmdsql.AppendLine(",'"+mRequireDate+"'");
            //            }
            //            cmdsql.AppendLine(",'" + ApplyReason[i - 1].ToString() + "','" + Remark[i - 1].ToString() + "','" + FromBillID[i - 1].ToString() + "','" + FromLineNo[i - 1].ToString() + "')");
            //            sql[i] = cmdsql.ToString();
                       
            //        } 
            //        if (PurchaseContractDBHelper.InsertPurchaseContract(sql))
            //            {
            //                //model.ID = IDIdentityUtil.GetIDIdentity("officedba.PurchaseContract");
            //                result = true;
            //            }
            //    }
            //    else
            //    {
            //        sql = new string[1];
            //        sql[0] = sqlc.ToString();
            //        if (PurchaseContractDBHelper.InsertPurchaseContract(sql))
            //        {
            //            model.ID = IDIdentityUtil.GetIDIdentity("officedba.PurchaseContract");
            //            result = true;
            //        }
            //    }
            //    return result;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            #endregion
        }
        #endregion
     
        #region 修改：合同记录
        /// <summary>
        /// 修改合同记录
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CompanyNo"></param>
        /// <returns></returns>
        public static bool UpdatePurchaseContract(PurchaseContractModel model, string DetailProductID, string DetailProductNo, string DetailProductName, string DetailUnitID, string DetailProductCount, string DetailUnitPrice, string DetailTaxPrice, string DetailDiscount, string DetailTaxRate, string DetailTotalPrice, string DetailTotalFee, string DetailTotalTax, string DetailRequireDate, string DetailApplyReason, string DetailRemark, string DetailFromBillID, string DetailFromBillNo, string DetailFromLineNo, string DetailUsedUnitID, string DetailUsedUnitCount, string DetailUsedPrice, string DetailExRate, string length, string fflag2, string no, Hashtable ht)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            if (model.ID <= 0)
            {
                return false;
            }
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(no);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                //设置模块ID 模块ID请在ConstUtil中定义，以便维护
                logModel.ModuleID = ConstUtil.MODULE_ID_PurchaseContract_Add;
                succ = PurchaseContractDBHelper.UpdatePurchaseContract(model, DetailProductID, DetailProductNo, DetailProductName, DetailUnitID, DetailProductCount, DetailUnitPrice, DetailTaxPrice, DetailDiscount, DetailTaxRate, DetailTotalPrice, DetailTotalFee, DetailTotalTax, DetailRequireDate, DetailApplyReason, DetailRemark, DetailFromBillID, DetailFromBillNo, DetailFromLineNo, DetailUsedUnitID, DetailUsedUnitCount, DetailUsedPrice, DetailExRate, length, fflag2, no, ht);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            #region
            //if (model.CompanyCD == null) model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            ////string CompanyCD = "AAAAAA";
            ////try
            ////{
            ////    return PurchaseContractDBHelper.UpdatePurchaseContract(sql);
            ////}
            ////catch (Exception ex)
            ////{
            ////    throw ex;
            ////}

            //try
            //{
            //    SqlDateTime signdate = model.SignDate == null ? SqlDateTime.Null : SqlDateTime.Parse(model.SignDate.ToString());
            //    SqlDateTime confirmdate = model.ConfirmDate == null ? SqlDateTime.Null : SqlDateTime.Parse(model.ConfirmDate.ToString());
            //    SqlDateTime closedate = model.CloseDate == null ? SqlDateTime.Null : SqlDateTime.Parse(model.CloseDate.ToString());
            //    SqlDateTime createdate = model.CreateDate == null ? SqlDateTime.Null : SqlDateTime.Parse(model.CreateDate.ToString());
            //    int Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            //    #region 采购合同更新SQL语句
            //    StringBuilder sqlc = new StringBuilder();
            //    sqlc.AppendLine(" UPDATE officedba.PurchaseContract SET ");
            //    sqlc.AppendLine(" CompanyCD          ='" + model.CompanyCD + "',");
            //    sqlc.AppendLine(" ContractNo         ='" + no + "',");
            //    sqlc.AppendLine(" Title              ='" + model.Title + "',");
            //    sqlc.AppendLine(" ProviderID         =" + model.ProviderID + ",");
            //    sqlc.AppendLine(" FromType           ='" + model.FromType + "',");
            //    sqlc.AppendLine(" FromBillID         =" + model.FromBillID + ",");
            //    sqlc.AppendLine(" TotalPrice         =" + model.TotalPrice + ",");
            //    sqlc.AppendLine(" CurrencyType       =" + model.CurrencyType + ",");
            //    sqlc.AppendLine(" Rate               =" + model.Rate + ",");
            //    sqlc.AppendLine(" PayType            =" + model.PayType + ",");
            //    if (signdate.IsNull)
            //    {
            //        sqlc.AppendLine("SignDate        = " + signdate + ",");
            //    }
            //    else
            //    {
            //        sqlc.AppendLine("SignDate       = '" + signdate + "',");
            //    }
            //    sqlc.AppendLine(" Seller            =" + model.Seller + ",");
            //    sqlc.AppendLine(" TheyDelegate   ='" + model.TheyDelegate + "',");
            //    sqlc.AppendLine(" OurDelegate         =" + model.OurDelegate + ",");
            //    sqlc.AppendLine(" Status            ='" + model.Status + "',");
            //    sqlc.AppendLine(" Note              ='" + model.Note + "',");
            //    sqlc.AppendLine(" TakeType          =" + model.TakeType + ",");
            //    sqlc.AppendLine(" CarryType         =" + model.CarryType + ",");
            //    sqlc.AppendLine(" Attachment        ='" + model.Attachment + "',");
            //    sqlc.AppendLine(" remark            ='" + model.remark + "',");
            //    sqlc.AppendLine(" BillStatus        ='" + model.BillStatus + "',");
            //    sqlc.AppendLine(" Creator           =" + model.Creator + ",");
            //    if (createdate.IsNull)
            //    {
            //        sqlc.AppendLine("CreateDate     = " + createdate + ",");
            //    }
            //    else
            //    {
            //        sqlc.AppendLine("CreateDate     = '" + createdate + "',");
            //    }
            //    sqlc.AppendLine(" ModifiedDate      =getdate(),");
            //    sqlc.AppendLine(" ModifiedUserID    ='" + model.ModifiedUserID + "',");
            //    sqlc.AppendLine(" Confirmor         =" + model.Confirmor + ",");
            //    if (confirmdate.IsNull)
            //    {
            //        sqlc.AppendLine("Confirmdate     = " + confirmdate + ",");
            //    }
            //    else
            //    {
            //        sqlc.AppendLine("Confirmdate     = '" + confirmdate + "',");
            //    }
            //    sqlc.AppendLine(" Closer            =" + Creator + ",");
            //    if (closedate.IsNull)
            //    {
            //        sqlc.AppendLine("CloseDate      =" + closedate + ",");
            //    }
            //    else
            //    {
            //        sqlc.AppendLine("CloseDate      ='" + closedate + "',");
            //    }
            //    sqlc.AppendLine("DeptID             =" + model.DeptID + ",");
            //    sqlc.AppendLine("TotalTax             =" + model.TotalTax + ",");
            //    sqlc.AppendLine("TotalFee             =" + model.TotalFee + ",");
            //    sqlc.AppendLine("Discount             =" + model.Discount + ",");
            //    sqlc.AppendLine("DiscountTotal             =" + model.DiscountTotal + ",");
            //    sqlc.AppendLine("RealTotal             =" + model.RealTotal + ",");
            //    sqlc.AppendLine("isAddTax             ='" + model.isAddTax + "',");
            //    sqlc.AppendLine("CountTotal             =" + model.CountTotal + ",");
            //    sqlc.AppendLine("TypeID             =" + model.TypeID + ",");
            //    sqlc.AppendLine("SignAddr             ='" + model.SignAddr + "',");
            //    sqlc.AppendLine("MoneyType             =" + model.MoneyType + "");
            //    sqlc.AppendLine(" Where  CompanyCD='" + model.CompanyCD + "' and ContractNo='" + no+"' ");

            //    #endregion

            //    //如果未别引用，明细操作
            //    string[] sql = null;
            //    int lengs = Convert.ToInt32(length);
            //    if (lengs > 0)
            //    {
            //        sql = new string[lengs + 2];
            //        sql[0] = sqlc.ToString();
            //        sql[1] = "delete from officedba.PurchaseContractDetail where CompanyCD='" + model.CompanyCD + "' and ContractNo='" + no + "' ";
            //        string[] ProductID = DetailProductID.Split(',');
            //        string[] ProductNo = DetailProductNo.Split(',');
            //        string[] ProductName = DetailProductName.Split(',');
            //        string[] Standard = DetailStandard.Split(',');//规格
            //        string[] UnitID = DetailUnitID.Split(',');
            //        string[] ProductCount = DetailProductCount.Split(',');
            //        string[] UnitPrice = DetailUnitPrice.Split(',');//单价
            //        string[] TaxPrice = DetailTaxPrice.Split(',');//含税价
            //        string[] Discount = DetailDiscount.Split(',');//折扣
            //        string[] TaxRate = DetailTaxRate.Split(',');//税率
            //        string[] TotalPrice = DetailTotalPrice.Split(',');//金额
            //        string[] TotalFee = DetailTotalFee.Split(',');//含税金额
            //        string[] TotalTax = DetailTotalTax.Split(',');//税额
            //        string[] RequireDate = DetailRequireDate.Split(',');
            //        string[] ApplyReason = DetailApplyReason.Split(',');
            //        string[] Remark = DetailRemark.Split(',');
            //        string[] FromBillID = DetailFromBillID.Split(',');
            //        string[] FromLineNo = DetailFromLineNo.Split(',');
            //        for (int i = 2; i <= lengs + 1; i++)
            //        {
            //            SqlDateTime mRequireDate;
            //            decimal mTotalPrice, mUnitID;
            //            decimal mUnitPrice, mProductCount, mTaxPrice, mDiscount, mTaxRate, mTotalFee, mTotalTax;
            //            if (UnitPrice[i - 2].ToString() == "")
            //            {
            //                mUnitPrice = 0;
            //            }
            //            else
            //            {
            //                mUnitPrice = Convert.ToDecimal(UnitPrice[i - 2].ToString());
            //            }
            //            if (ProductCount[i - 2].ToString() == "")
            //            {
            //                mProductCount = 0;
            //            }
            //            else
            //            {
            //                mProductCount = Convert.ToDecimal(ProductCount[i - 2].ToString());
            //            }
            //            if (TotalPrice[i - 2].ToString() == "")
            //            {
            //                mTotalPrice = 0;
            //            }
            //            else
            //            {
            //                mTotalPrice = Convert.ToDecimal(TotalPrice[i - 2].ToString());
            //            }
            //            if (TaxPrice[i - 2].ToString() == "")
            //            {
            //                mTaxPrice = 0;
            //            }
            //            else
            //            {
            //                mTaxPrice = Convert.ToDecimal(TaxPrice[i - 2].ToString());
            //            }
            //            if (Discount[i - 2].ToString() == "")
            //            {
            //                mDiscount = 0;
            //            }
            //            else
            //            {
            //                mDiscount = Convert.ToDecimal(Discount[i - 2].ToString());
            //            }
            //            if (TaxRate[i - 2].ToString() == "")
            //            {
            //                mTaxRate = 0;
            //            }
            //            else
            //            {
            //                mTaxRate = Convert.ToDecimal(TaxRate[i - 2].ToString());
            //            }
            //            if (TotalFee[i - 2].ToString() == "")
            //            {
            //                mTotalFee = 0;
            //            }
            //            else
            //            {
            //                mTotalFee = Convert.ToDecimal(TotalFee[i - 2].ToString());
            //            }
            //            if (TotalTax[i - 2].ToString() == "")
            //            {
            //                mTotalTax = 0;
            //            }
            //            else
            //            {
            //                mTotalTax = Convert.ToDecimal(TotalTax[i - 2].ToString());
            //            }
            //            if (UnitID[i - 2].ToString() == "")
            //            {
            //                mUnitID = 0;
            //            }
            //            else
            //            {
            //                mUnitID = Convert.ToDecimal(UnitID[i - 2].ToString());
            //            }
            //            System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
            //            //cmdsql.AppendLine(" Insert into  officedba.PurchaseContractDetail(CompanyCD,ContractNo,ProductID,ProductNo,ProductName,ProductCount,");
            //            //cmdsql.AppendLine("UnitID,RequireDate,UnitPrice,TotalPrice,ApplyReason,Remark,FromBillID,FromLineNo)");
            //            cmdsql.AppendLine(" Insert into  officedba.PurchaseContractDetail(CompanyCD,ContractNo,ProductID,ProductNo,ProductName,standard,UnitID,ProductCount,");
            //            cmdsql.AppendLine("UnitPrice,TaxPrice,Discount,TaxRate,TotalPrice,TotalFee,TotalTax,RequireDate,ApplyReason,Remark,FromBillID,FromLineNo)");
            //            cmdsql.AppendLine(" Values('" + model.CompanyCD + "','" + no + "','" + ProductID[i - 2].ToString() + "','" + ProductNo[i - 2].ToString() + "','" + ProductName[i - 2].ToString() + "','" + Standard[i - 2].ToString() + "','" + mUnitID + "','" + mProductCount + "'");
            //            cmdsql.AppendLine(",'" + mUnitPrice + "','" + mTaxPrice + "','" + mDiscount + "','" + mTaxRate + "','" + mTotalPrice + "','" + mTotalFee + "','" + mTotalTax + "'");
            //            if (RequireDate[i - 2].ToString() == "")
            //            {
            //                mRequireDate = SqlDateTime.Null;
            //                cmdsql.AppendLine("," + mRequireDate + "");
            //            }
            //            else
            //            {
            //                mRequireDate = SqlDateTime.Parse(RequireDate[i - 2].ToString());
            //                cmdsql.AppendLine(",'" + mRequireDate + "'");
            //            }
            //            cmdsql.AppendLine(",'" + ApplyReason[i - 2].ToString() + "','" + Remark[i - 2].ToString() + "','" + FromBillID[i - 2].ToString() + "','" + FromLineNo[i - 2].ToString() + "')");
            //            sql[i] = cmdsql.ToString();
            //        }
            //        return PurchaseContractDBHelper.UpdatePurchaseContract(sql);
            //    }
            //    else
            //    {
            //        sql = new string[2];
            //        sql[0] = sqlc.ToString();
            //        sql[1] = "delete from officedba.PurchaseContractDetail where CompanyCD='" + model.CompanyCD + "' and ContractNo='" + no + "' ";
            //        return PurchaseContractDBHelper.UpdatePurchaseContract(sql);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            #endregion

        }
        #endregion

        #region 删除：合同删除//？
        /// <summary>
        /// 删除合同
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="ID">主键</param>
        /// <returns>true 成功,false 失败</returns>
        public static bool DeletePurchaseContract(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string CompanyCD = userInfo.CompanyCD;
            if (string.IsNullOrEmpty(ID) && string.IsNullOrEmpty(CompanyCD)) return false;
            try
            {
                bool isSucc = PurchaseContractDBHelper.DeletePurchaseContract(ID, CompanyCD);
                string remark;
                //成功时
                if (isSucc)
                {
                    //设置操作成功标识
                    remark = ConstUtil.LOG_PROCESS_SUCCESS;
                }
                else
                {
                    //设置操作成功标识 
                    remark = ConstUtil.LOG_PROCESS_FAILED;
                }
                //获取删除的编号列表
                string[] noList = ID.Split(',');
                //遍历所有编号，登陆操作日志
                for (int i = 0; i < noList.Length; i++)
                {
                    //获取编号
                    string no = noList[i];
                    //替换两边的 '
                    no = no.Replace("'", string.Empty);

                    //操作日志
                    LogInfoModel logModel = InitLogInfo("变更单ID：" + no);
                    //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                    logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                    //设置操作成功标识
                    logModel.Remark = remark;

                    //登陆日志
                    LogDBHelper.InsertLog(logModel);
                }
                return isSucc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 绑定采购类别
        /// <summary>
        /// 绑定采购类别
        /// </summary>
        /// <returns>DataTable</returns>
        
        public static DataTable GetddlTypeID()
        {
            DataTable dt = PurchaseContractDBHelper.GetddlTypeID();
            return dt;
        }
        #endregion

        #region 原因
        public static DataTable GetDrpApplyReason()
        {
            DataTable dt = PurchaseContractDBHelper.GetDrpApplyReason();
            return dt;
        }
        #endregion

        #region 绑定采购交货方式
        /// <summary>
        /// 绑定采购交货方式
        /// </summary>
        /// <returns>DataTable</returns>
        
        public static DataTable GetDrpTakeType()
        {
            DataTable dt = PurchaseContractDBHelper.GetDrpTakeType();
            return dt;
        }
        #endregion
        
        #region 绑定采购运送方式
        /// <summary>
        /// 绑定采购运送方式
        /// </summary>
        /// <returns>DataTable</returns>
        
        public static DataTable GetDrpCarryType()
        {
            DataTable dt = PurchaseContractDBHelper.GetDrpCarryType();
            return dt;
        }
        #endregion

        #region 绑定采购结算方式
        /// <summary>
        /// 绑定采购结算方式
        /// </summary>
        /// <returns>DataTable</returns>
        
        public static DataTable GetDrpPayType()
        {
            DataTable dt = PurchaseContractDBHelper.GetDrpPayType();
            return dt;
        }
        #endregion

        #region 绑定采购支付方式
        /// <summary>
        /// 绑定采购结算方式
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetDrpMoneyType()
        {
            DataTable dt = PurchaseContractDBHelper.GetDrpMoneyType();
            return dt;
        }
        #endregion

        #region 绑定采购合同币种
        /// <summary>
        /// 绑定采购合同币种
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetCurrenyType()
        {
            DataTable dt = CurrTypeSettingDBHelper.GetCurrenyType();
            return dt;
        }
        #endregion

        #region 查询采购合同列表所需数据
        public static DataTable SelectPurchaseContract(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ContractNo1, string Title, string TypeID, string DeptID, string Seller, string FromType, string ProviderID, string BillStatus, string UsedStatus, string EFIndex ,string EFDesc )
        {
            try
            {
                return PurchaseContractDBHelper.SelectContractList(pageIndex, pageCount, orderBy, ref TotalCount, ContractNo1, Title, TypeID, DeptID, Seller, FromType, ProviderID, BillStatus, UsedStatus, EFIndex, EFDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 选择采购申请源单数据
        public static DataTable GetApplySelectDetail(int Provider00ID,string ProductNo, string ProductName, string FromBillNo, string CompanyCD)
        {
            try
            {
                return PurchaseContractDBHelper.GetApplySelectDetail(Provider00ID,ProductNo, ProductName, FromBillNo, CompanyCD);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 选择采购计划源单数据
        public static DataTable GetSellOrderDetail(string ProductNo, string ProductName, string FromBillNo, string CompanyCD,int ProviderID)
        {
            try
            {
                return PurchaseContractDBHelper.GetSellOrderDetail(ProductNo, ProductName, FromBillNo, CompanyCD, ProviderID);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 选择采购询价源单数据
        public static DataTable GetAskPriceOrderDetail(string ProductNo, string ProductName, string FromBillNo, string CompanyCD, int ProviderID,int CurrencyTypeID,string Rate)
        {
            try
            {
                return PurchaseContractDBHelper.GetAskPriceOrderDetail(ProductNo, ProductName, FromBillNo, CompanyCD, ProviderID, CurrencyTypeID, Rate);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 删除采购合同
        public static bool DeletePurchaseContractAll(string DetailNo)
        {
            LogInfoModel logModel = InitLogInfo(DetailNo);
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_PurchaseContractInfo;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string CompanyCD = userInfo.CompanyCD;

            //string[] sql = new string[2];
            //int index = 0;
            //PurchaseContractDBHelper.DeletePurchasePlanPrimary(ContractNo, ref sql, index++);
            //PurchaseContractDBHelper.DeletePurchasePlanDetail(ContractNo, ref sql, index++);

            //SqlHelper.ExecuteTransForListWithSQL(sql);
            //bool isSucc = SqlHelper.Result.OprateCount > 0 ? true : false;

            bool isSucc = PurchaseContractDBHelper.DeletePurchasePlanPrimary(DetailNo);
            string remark;
            //成功时
            if (isSucc)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
                logModel.Remark = remark;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
                logModel.Remark = remark;
            }
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }
        #endregion

        #region 确认采购合同
        public static bool ConfirmPurchaseContract(PurchaseContractModel Model, string DetailProductCount, string DetailFromBillNo, string DetailFromLineNo, string length, out string strMsg)
        {
            return PurchaseContractDBHelper.ConfirmPurchaseContract(Model, DetailProductCount, DetailFromBillNo, DetailFromLineNo, length, out strMsg);
        }
        #endregion

        #region 取消确认采购合同
        public static bool CancelConfirmPurchaseContract(PurchaseContractModel Model, string DetailProductCount, string DetailFromBillNo, string DetailFromLineNo, string length, out string strMsg)
        {
            return PurchaseContractDBHelper.CancelConfirmPurchaseContract(Model, DetailProductCount, DetailFromBillNo, DetailFromLineNo, length, out strMsg);
        }
        #endregion

        #region 手工结单
        public static bool ClosePurchaseContract(PurchaseContractModel Model)
        {
            return PurchaseContractDBHelper.ClosePurchaseContract(Model);
        }
        #endregion

        #region 取消结单
        public static bool CancelClosePurchaseContract(PurchaseContractModel Model)
        {
            return PurchaseContractDBHelper.CancelClosePurchaseContract(Model);
        }
        #endregion

        #region 获取单个合同记录
        public static DataTable SelectContract(int ID)
        {
            try
            {
                return PurchaseContractDBHelper.SelectContract(ID);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 获取合同明细
        public static DataTable Details(int ID)
        {
            try
            {
                return PurchaseContractDBHelper.Details(ID);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <returns></returns>
        private static LogInfoModel InitLogInfo(string ContractNo)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            ////设置模块ID 模块ID请在ConstUtil中定义，以便维护
            //logModel.ModuleID = ConstUtil.MODULE_ID_PurchaseContract_Add;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PURCHASECONTRACT;
            //操作对象
            logModel.ObjectID = ContractNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        #region 确定单据有没有被引用
        public static bool IsCitePurchaseContract(int ID)
        {
            try
            {
                return PurchaseContractDBHelper.IsCitePurchaseContract(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购报表采购合同查询
        public static DataTable PurchaseContractQuery(int pageIndex,int pageCount,string orderBy, ref int TotalCount,string ProviderID,string StartConfirmDate,string EndConfirmDate,string CompanyCD)
        {
            try
            {
                return PurchaseContractDBHelper.PurchaseContractQuery(pageIndex, pageCount, orderBy, ref TotalCount, ProviderID, StartConfirmDate, EndConfirmDate, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购报表采购合同查询打印
        public static DataTable PurchaseContractQueryPrint(string ProviderID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            try
            {
                return PurchaseContractDBHelper.PurchaseContractQueryPrint(ProviderID, StartConfirmDate, EndConfirmDate, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}

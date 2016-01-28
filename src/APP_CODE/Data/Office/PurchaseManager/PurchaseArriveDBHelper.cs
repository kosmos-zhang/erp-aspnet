/***********************************************
 * 类作用：   采购管理事务层处理               *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/04/16                       *
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
    /// 类名：PurchaseArriveDBHelper
    /// 描述：采购到货数据库层处理
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/16
    /// 最后修改时间：2009/04/16
    /// </summary>
    ///
    public class PurchaseArriveDBHelper
    {
        #region 绑定采购类别
        public static DataTable GetddlTypeID()
        {
            string sql = "select ID,TypeName from officedba.codepublictype where typeflag =7 and typecode =5 and usedstatus=1  AND CompanyCD=@CompanyCD   ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql, param);
            return data;
        }
        #endregion

        #region 绑定采购交货方式
        public static DataTable GetDrpTakeType()
        {
            string sql = "select ID,TypeName from officedba.codepublictype where typeflag =6 and typecode =7 and usedstatus=1  AND CompanyCD=@CompanyCD ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql, param);
            return data;
        }
        #endregion

        #region 绑定采购运送方式
        public static DataTable GetDrpCarryType()
        {
            string sql = "select ID,TypeName from officedba.codepublictype where typeflag =6 and typecode =8 and usedstatus=1  AND CompanyCD=@CompanyCD";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql, param);
            return data;
        }
        #endregion

        #region 绑定采购结算方式
        public static DataTable GetDrpPayType()
        {
            string sql = "select ID,TypeName from officedba.codepublictype where typeflag =4 and typecode =11 and usedstatus=1  AND CompanyCD=@CompanyCD";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql, param);
            return data;
        }
        #endregion

        #region 绑定采购支付方式
        public static DataTable GetDrpMoneyType()
        {
            string sql = "select ID,TypeName from officedba.codepublictype where typeflag =4 and typecode =14 and usedstatus=1  AND CompanyCD=@CompanyCD";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql, param);
            return data;
        }
        #endregion

        #region 绑定币种
        public static DataTable GetDrpCurrencyType()
        {
            string sql = "select ID,CurrencyName from officedba.CurrencyTypeSetting where  usedstatus=1  AND CompanyCD=@CompanyCD";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql, param);
            return data;
        }
        #endregion

        #region 插入到货通知单
        public static bool InsertPurchaseArrive(PurchaseArriveModel model, string DetailProductID, string DetailProductNo
            , string DetailProductName, string DetailUnitID, string DetailProductCount
            , string DetailRequireDate, string DetailUnitPrice, string DetailTaxPrice
            , string DetailDiscount, string DetailTaxRate, string DetailTotalPrice
            , string DetailTotalFee, string DetailTotalTax, string DetailRemark
            , string DetailFromBillID, string DetailFromLineNo
            , string DetailUsedUnitCount, string DetailUsedUnitID, string DetailUsedPrice
            , string length, out string ID, Hashtable htExtAttr)
        {

            ArrayList listADD = new ArrayList();
            bool result = false;
            ID = "0";

            #region  采购合同到货通知单添加SQL语句
            StringBuilder sqlArrive = new StringBuilder();


            sqlArrive.AppendLine("INSERT INTO officedba.PurchaseArrive");
            sqlArrive.AppendLine("(CompanyCD,ArriveNo,Title,FromType,ProviderID,Purchaser,DeptID,PayType,TypeID,TakeType,");
            sqlArrive.AppendLine("CarryType,SendAddress,ReceiveOverAddress,CheckUserID,CheckDate,CurrencyType,Rate,");
            sqlArrive.AppendLine("TotalMoney,TotalTax,TotalFee,Discount,DiscountTotal,RealTotal,isAddTax,CountTotal,");
            sqlArrive.AppendLine("remark,BillStatus,Creator,CreateDate,ModifiedDate,ModifiedUserID,Confirmor,");
            sqlArrive.AppendLine("ConfirmDate,Closer,CloseDate,OtherTotal,Attachment,MoneyType,ArriveDate,ProjectID,CanViewUserName ,CanViewUser)");
            sqlArrive.AppendLine("VALUES (@CompanyCD,@ArriveNo,@Title,@FromType,@ProviderID,@Purchaser,@DeptID,@PayType,@TypeID,@TakeType,");
            sqlArrive.AppendLine("@CarryType,@SendAddress,@ReceiveOverAddress,@CheckUserID,@CheckDate,@CurrencyType,@Rate,");
            sqlArrive.AppendLine("@TotalMoney,@TotalTax,@TotalFee,@Discount,@DiscountTotal,@RealTotal,@isAddTax,@CountTotal,");
            sqlArrive.AppendLine("@remark,@BillStatus,@Creator,@CreateDate,getdate(),@ModifiedUserID,@Confirmor,");
            sqlArrive.AppendLine("@ConfirmDate,@Closer,@CloseDate,@OtherTotal,@Attachment,@MoneyType,@ArriveDate,@ProjectID,@UserName,@CanUserID)");
            sqlArrive.AppendLine("set @ID=@@IDENTITY");

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@UserName", model.CanUserName));
            comm.Parameters.Add(SqlHelper.GetParameter("@CanUserID", model.CanUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ArriveNo", model.ArriveNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProviderID", model.ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Purchaser", model.Purchaser));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@PayType", model.PayType));
            comm.Parameters.Add(SqlHelper.GetParameter("@TypeID", model.TypeID));
            comm.Parameters.Add(SqlHelper.GetParameter("@TakeType", model.TakeType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CarryType", model.CarryType));
            comm.Parameters.Add(SqlHelper.GetParameter("@SendAddress", model.SendAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReceiveOverAddress", model.ReceiveOverAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckUserID", model.CheckUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckDate", model.CheckDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.CheckDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", model.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@Rate", model.Rate));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalMoney", model.TotalMoney));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalTax", model.TotalTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalFee", model.TotalFee));
            comm.Parameters.Add(SqlHelper.GetParameter("@Discount", model.Discount));
            comm.Parameters.Add(SqlHelper.GetParameter("@DiscountTotal", model.DiscountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@RealTotal", model.RealTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@isAddTax", model.isAddTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
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
            comm.Parameters.Add(SqlHelper.GetParameter("@OtherTotal", model.OtherTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@Attachment", model.Attachment));
            comm.Parameters.Add(SqlHelper.GetParameter("@MoneyType", model.MoneyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@ArriveDate", model.ArriveDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.ArriveDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProjectID", model.ProjectID.HasValue
                                                        ? SqlInt32.Parse(model.ProjectID.Value.ToString())
                                                        : SqlInt32.Null));
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
            comm.CommandText = sqlArrive.ToString();


            listADD.Add(comm);
            #endregion

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd, string.Empty);
            if (htExtAttr.Count > 0)
                listADD.Add(cmd);
            #endregion
            try
            {
                #region 采购合同到货通知单明细
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
                        cmdsql.AppendLine("INSERT INTO officedba.PurchaseArriveDetail");
                        cmdsql.AppendLine("(CompanyCD,ArriveNo,SortNo,FromType,FromBillID,FromLineNo,ProductID,ProductNo,ProductName,ProductCount,UnitID,UnitPrice,");
                        cmdsql.AppendLine("TaxPrice,TaxRate,TotalFee,TotalPrice,TotalTax,Remark,UsedUnitCount,UsedUnitID,ExRate,UsedPrice)");
                        cmdsql.AppendLine("VALUES (@CompanyCD,@ArriveNo,@SortNo,@FromType,@FromBillID,@FromLineNo,@ProductID,@ProductNo,@ProductName,@ProductCount,@UnitID,@UnitPrice,");
                        cmdsql.AppendLine("@TaxPrice,@TaxRate,@TotalFee,@TotalPrice,@TotalTax,@Remark2,@UsedUnitCount,@UsedUnitID,@ExRate,@UsedPrice)");

                        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ArriveNo", model.ArriveNo));
                        comms.Parameters.Add(SqlHelper.GetParameter("@SortNo", i + 1));
                        comms.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
                        comms.Parameters.Add(SqlHelper.GetParameter("@FromBillID", FromBillID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", FromLineNo[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductNo", ProductNo[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductName", ProductName[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductCount", ProductCount[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UnitID", UnitID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxPrice", TaxPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxRate", TaxRate[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalFee", TotalFee[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", TotalPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalTax", TotalTax[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@Remark2", Remark[i].ToString()));
                        if (UsedUnitCount.Length == lengs && !string.IsNullOrEmpty(UsedUnitCount[i]))
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", UsedUnitCount[i]));
                        }
                        else
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", DBNull.Value));
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
                            // 多计量单位时重新计算单价
                            comms.Parameters.Add(SqlHelper.GetParameter("@UnitPrice", (decimal.Parse(UsedPrice[i])) / decimal.Parse(UsedUnitID[i].Split('|')[1])));
                        }
                        else
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@ExRate", DBNull.Value));
                            // 单价
                            comms.Parameters.Add(SqlHelper.GetParameter("@UnitPrice", UnitPrice[i].ToString()));
                        }
                        if (UsedPrice.Length == lengs && !string.IsNullOrEmpty(UsedPrice[i]))
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", UsedPrice[i]));
                        }
                        else
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", DBNull.Value));
                        }
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

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(PurchaseArriveModel model, Hashtable htExtAttr, SqlCommand cmd, string no)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.PurchaseArrive set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND ArriveNo = @ArriveNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                if (!string.IsNullOrEmpty(no))
                {
                    cmd.Parameters.AddWithValue("@ArriveNo", no);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ArriveNo", model.ArriveNo);
                }
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion

        #region 修改到货通知单
        public static bool UpdatePurchaseArrive(PurchaseArriveModel model, string DetailProductID
            , string DetailProductNo, string DetailProductName, string DetailUnitID
            , string DetailProductCount, string DetailRequireDate, string DetailUnitPrice
            , string DetailTaxPrice, string DetailDiscount, string DetailTaxRate, string DetailTotalPrice
            , string DetailTotalFee, string DetailTotalTax, string DetailRemark, string DetailFromBillID
            , string DetailFromBillNo, string DetailFromLineNo, string DetailUsedUnitCount
            , string DetailUsedUnitID, string DetailUsedPrice, string length, string fflag2
            , string no, Hashtable htExtAttr)
        {
            if (model.ID <= 0)
            {
                return false;
            }
            ArrayList listADD = new ArrayList();
            bool result = false;

            #region  修改采购合同到货通知单
            StringBuilder sqlArrive = new StringBuilder();

            sqlArrive.AppendLine("Update  Officedba.PurchaseArrive set CompanyCD=@CompanyCD,");
            sqlArrive.AppendLine("Title=@Title,FromType=@FromType,ProviderID=@ProviderID,Purchaser=@Purchaser,DeptID=@DeptID,");
            sqlArrive.AppendLine("PayType=@PayType,TypeID=@TypeID,TakeType=@TakeType,CarryType=@CarryType,SendAddress=@SendAddress,");
            sqlArrive.AppendLine("ReceiveOverAddress=@ReceiveOverAddress,CheckUserID=@CheckUserID,CheckDate=@CheckDate,CurrencyType=@CurrencyType,Rate=@Rate,");
            sqlArrive.AppendLine("TotalMoney=@TotalMoney,TotalTax=@TotalTax,TotalFee=@TotalFee,Discount=@Discount,DiscountTotal=@DiscountTotal,");
            sqlArrive.AppendLine("RealTotal=@RealTotal,isAddTax=@isAddTax,CountTotal=@CountTotal,remark=@remark,BillStatus=@BillStatus,");
            sqlArrive.AppendLine("CreateDate=@CreateDate,ModifiedUserID=@ModifiedUserID,Confirmor=@Confirmor,");
            sqlArrive.AppendLine("ConfirmDate=@ConfirmDate,Closer=@Closer,CloseDate=@CloseDate,OtherTotal=@OtherTotal,");
            sqlArrive.AppendLine("Attachment=@Attachment,MoneyType=@MoneyType,ArriveDate=@ArriveDate,ProjectID=@ProjectID ,ModifiedDate=getDate(),CanViewUser =@CanUserID,CanViewUserName =@UserName   where CompanyCD=@CompanyCD and ArriveNo='" + no + "'and ID=" + model.ID);


            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@UserName", model.CanUserName));
            comm.Parameters.Add(SqlHelper.GetParameter("@CanUserID", model.CanUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProviderID", model.ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Purchaser", model.Purchaser));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@PayType", model.PayType));
            comm.Parameters.Add(SqlHelper.GetParameter("@TypeID", model.TypeID));
            comm.Parameters.Add(SqlHelper.GetParameter("@TakeType", model.TakeType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CarryType", model.CarryType));
            comm.Parameters.Add(SqlHelper.GetParameter("@SendAddress", model.SendAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReceiveOverAddress", model.ReceiveOverAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckUserID", model.CheckUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckDate", model.CheckDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.CheckDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", model.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@Rate", model.Rate));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalMoney", model.TotalMoney));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalTax", model.TotalTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalFee", model.TotalFee));
            comm.Parameters.Add(SqlHelper.GetParameter("@Discount", model.Discount));
            comm.Parameters.Add(SqlHelper.GetParameter("@DiscountTotal", model.DiscountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@RealTotal", model.RealTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@isAddTax", model.isAddTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
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
            comm.Parameters.Add(SqlHelper.GetParameter("@OtherTotal", model.OtherTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@Attachment", model.Attachment));
            comm.Parameters.Add(SqlHelper.GetParameter("@MoneyType", model.MoneyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@ArriveDate", model.ArriveDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.ArriveDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProjectID", model.ProjectID.HasValue
                                                        ? SqlInt32.Parse(model.ProjectID.Value.ToString())
                                                        : SqlInt32.Null));
            comm.CommandText = sqlArrive.ToString();


            listADD.Add(comm);

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd, no);
            if (htExtAttr.Count > 0)
                listADD.Add(cmd);
            #endregion

            #region 删除到货通知单明细
            System.Text.StringBuilder cmdddetail = new System.Text.StringBuilder();
            cmdddetail.AppendLine("DELETE  FROM officedba.PurchaseArriveDetail WHERE  CompanyCD=@CompanyCD and ArriveNo=@ArriveNo");
            SqlCommand comn = new SqlCommand();
            comn.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comn.Parameters.Add(SqlHelper.GetParameter("@ArriveNo", no));
            comn.CommandText = cmdddetail.ToString();
            listADD.Add(comn);
            #endregion


            try
            {
                #region 重新插入到货通知单明细
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
                        cmdsql.AppendLine("INSERT INTO officedba.PurchaseArriveDetail");
                        cmdsql.AppendLine("(CompanyCD,ArriveNo,SortNo,FromType,FromBillID,FromLineNo,ProductID,ProductNo,ProductName,ProductCount,UnitID,UnitPrice,");
                        cmdsql.AppendLine("TaxPrice,TaxRate,TotalFee,TotalPrice,TotalTax,Remark,UsedUnitCount,UsedUnitID,ExRate,UsedPrice)");
                        cmdsql.AppendLine("VALUES (@CompanyCD,@ArriveNo,@SortNo,@FromType,@FromBillID,@FromLineNo,@ProductID,@ProductNo,@ProductName,@ProductCount,@UnitID,@UnitPrice,");
                        cmdsql.AppendLine("@TaxPrice,@TaxRate,@TotalFee,@TotalPrice,@TotalTax,@Remark2,@UsedUnitCount,@UsedUnitID,@ExRate,@UsedPrice)");


                        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ArriveNo", no));
                        comms.Parameters.Add(SqlHelper.GetParameter("@SortNo", i + 1));
                        comms.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
                        comms.Parameters.Add(SqlHelper.GetParameter("@FromBillID", FromBillID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", FromLineNo[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductNo", ProductNo[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductName", ProductName[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductCount", ProductCount[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UnitID", UnitID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@UnitPrice", UnitPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxPrice", TaxPrice[i].ToString()));

                        comms.Parameters.Add(SqlHelper.GetParameter("@TaxRate", TaxRate[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalFee", TotalFee[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", TotalPrice[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalTax", TotalTax[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@Remark2", Remark[i].ToString()));
                        if (UsedUnitCount.Length == lengs && !string.IsNullOrEmpty(UsedUnitCount[i]))
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", UsedUnitCount[i]));
                        }
                        else
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", DBNull.Value));
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
                        if (UsedPrice.Length == lengs && !string.IsNullOrEmpty(UsedPrice[i]))
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", UsedPrice[i]));
                        }
                        else
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", DBNull.Value));
                        }
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

        #endregion

        #region 选择订单来源
        public static DataTable GetPurchaseOrderDetail(string ProductNo, string ProductName, string FromBillNo, string CompanyCD, int ProviderID, int CurrencyTypeID, string Rate, string PurchaseArriveEFIndex, string PurchaseArriveEFDesc)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine("SELECT distinct A.ID                                                                       ");
            sql.AppendLine("      ,A.ProductID                                                                         ");
            sql.AppendLine("      ,isnull( B.ProdNo,'') AS   ProductNo                                                             ");
            sql.AppendLine("      ,A.ProductName                                                                       ");
            sql.AppendLine("      ,isnull(B.Specification,'') AS standard                                              ");
            sql.AppendLine("      ,A.UnitID                                                                            ");
            sql.AppendLine("      ,isnull(C.CodeName,'') AS UnitName                                                   ");
            sql.AppendLine("      ,isnull(A.ProductCount,0) AS ProductOrder                                            ");
            sql.AppendLine("      ,isnull(CONVERT(varchar(100), A.RequireDate, 23),'') as RequireDate                 ");
            sql.AppendLine("      ,isnull(A.UnitPrice,0) AS   UnitPrice                         ");
            sql.AppendLine("      ,isnull(A.TaxPrice,0) AS TaxPrice                              ");
            sql.AppendLine("      ,isnull(A.Discount,0) AS Discount                             ");
            sql.AppendLine("      ,isnull(A.TaxRate,0)  AS TaxRate                              ");
            sql.AppendLine("      ,isnull(A.TotalPrice,0) AS TotalPrice                        ");
            sql.AppendLine("      ,isnull(A.TotalFee,0) AS TotalFee                             ");
            sql.AppendLine("      ,isnull(A.TotalTax,0)  AS TotalTax                           ");
            sql.AppendLine("      ,isnull(A.Remark,'') AS Remark  ,D.ID AS FromBillID                                  ");
            sql.AppendLine("      ,A.SortNo AS FromLineNo,A.OrderNo  AS FromBillNo                                      ");
            sql.AppendLine("      ,isnull(A.ArrivedCount,0) AS  ArrivedCount                                            ");
            sql.AppendLine("      ,D.ProviderID,isnull(E.CustName,'') AS ProviderName                                   ");
            sql.AppendLine("      ,D.Purchaser,isnull(ei.EmployeeName,'') AS PurchaserName                                   ");
            sql.AppendLine("      ,isnull(D.CurrencyType,0) AS CurrencyType,isnull(F.CurrencyName,'') AS CurrencyTypeName");
            sql.AppendLine("      ,isnull(D.Rate,0) AS Rate                                                             ");
            sql.AppendLine("      ,ISNULL(cpt.TypeName,'') AS ColorName ");
            sql.AppendLine("      ,CU.CodeName AS UsedUnitName,A.UsedUnitID,A.UsedUnitCount,isnull(A.UsedPrice,0) AS UsedPrice ");
            sql.AppendLine("FROM officedba.PurchaseOrderDetail AS A                                                     ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS C ON A.CompanyCD = C.CompanyCD AND A.UnitID = C.ID      ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS CU ON A.CompanyCD = CU.CompanyCD AND A.UsedUnitID = CU.ID      ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProductID = B.ID    ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType cpt ON B.ColorID=cpt.ID   ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseOrder AS D ON A.CompanyCD = D.CompanyCD AND A.OrderNo = D.OrderNo");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS E ON D.CompanyCD = E.CompanyCD AND D.ProviderID = E.ID   ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS ei ON ei.CompanyCD = D.CompanyCD AND D.Purchaser = ei.ID   ");
            sql.AppendLine("LEFT JOIN officedba.CurrencyTypeSetting AS F ON D.CompanyCD = F.CompanyCD AND D.CurrencyType = F.ID ");
            sql.AppendLine("WHERE D.BillStatus = 2  AND D.CompanyCD =@CompanyCD ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (ProductNo != null && ProductNo != "")
            {
                sql.AppendLine(" AND A.ProductNo like '%'+ @ProductNo +'%'   ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", ProductNo));
            }
            if (ProductName != null && ProductName != "")
            {
                sql.AppendLine(" AND A.ProductName like '%'+ @ProductName +'%'   ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", ProductName));
            }
            if (FromBillNo != null && FromBillNo != "")
            {
                sql.AppendLine(" AND D.OrderNo like  '%'+ @FromBillNo +'%'   ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", FromBillNo));
            }
            if (ProviderID != 0)
            {
                sql.AppendLine(" AND D.ProviderID =@ProviderID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", Convert.ToString(ProviderID)));
            }
            sql.AppendLine(" AND D.CurrencyType = @CurrencyTypeID ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrencyTypeID", Convert.ToString(CurrencyTypeID)));
            if (Rate != "0")
            {
                sql.AppendLine(" AND D.Rate = @Rate  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate", Rate));
            }
            if (!string.IsNullOrEmpty(PurchaseArriveEFIndex) && !string.IsNullOrEmpty(PurchaseArriveEFDesc))
            {
                sql.AppendLine("	AND B.ExtField" + PurchaseArriveEFIndex + " LIKE '%" + PurchaseArriveEFDesc + "%' ");
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 确认
        public static bool ConfirmPurchaseArrive(PurchaseArriveModel Model, string DetailProductCount, string DetailFromBillNo, string DetailFromLineNo, string length, out string strMsg)
        {
            strMsg = "";
            bool result = true;
            if (isCanDo(Model.ID, "1"))
            {

                #region 确认
                ArrayList listADD = new ArrayList();
                StringBuilder strSql = new StringBuilder();
                strSql.AppendLine("update officedba.PurchaseArrive set ");
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
                UserInfoUtil userInfo = SessionUtil.Session["UserInfo"] as UserInfoUtil;

                #region 确认时回填订单中的到货数量
                try
                {

                    int lengs = Convert.ToInt32(length);
                    if (lengs > 0)
                    {
                        string[] ProductCount = DetailProductCount.Split(',');
                        string[] FromBillNo = DetailFromBillNo.Split(',');
                        string[] FromLineNo = DetailFromLineNo.Split(',');
                        string pp = "";

                        for (int i = 0; i < lengs; i++)
                        {
                            if (FromLineNo[i].ToString() != "")
                            {
                                if (userInfo.IsOverOrder || IsCanConfirm(FromBillNo[i].ToString(), FromLineNo[i].ToString(), Model.CompanyCD, ProductCount[i].ToString()))
                                {
                                    System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                                    SqlCommand comms = new SqlCommand();
                                    cmdsql.AppendLine("Update  Officedba.PurchaseOrderDetail set ArrivedCount=isnull(ArrivedCount,0)+@ProductCount");
                                    cmdsql.AppendLine(" where CompanyCD=@CompanyCD and OrderNo=@FromBillNo and SortNo=@FromLineNo");

                                    comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD));
                                    comms.Parameters.Add(SqlHelper.GetParameter("@ProductCount", ProductCount[i].ToString()));
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
                                    else
                                    {
                                        pp = "" + (i + 1);
                                    }
                                    strMsg += "第" + pp + "行的到货数量不能大于当前可用的到货数量,确认失败！";
                                    result = false;
                                }
                            }
                        }
                    }


                    if (result)
                    {
                        if (SqlHelper.ExecuteTransWithArrayList(listADD))
                        {
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

        #region 取消确认
        public static bool CancelConfirmPurchaseArrive(PurchaseArriveModel Model, string DetailProductCount, string DetailFromBillNo, string DetailFromLineNo, string length, out string strMsg)
        {

            bool isSuc = false;
            strMsg = "";
            //被财务模块调用不能取消确认


            //判断单据是否为执行状态，非执行状态不能取消确认
            if (isCanDo(Model.ID, "2"))
            {

                if (!IsCitePurchaseArrive(Model.ID))
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

                    strSq = "update  officedba.PurchaseArrive set BillStatus='1'  ";
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
                            string[] ProductCount = DetailProductCount.Split(',');
                            string[] FromBillNo = DetailFromBillNo.Split(',');
                            string[] FromLineNo = DetailFromLineNo.Split(',');

                            for (int i = 0; i < lengs; i++)
                            {

                                StringBuilder cmdsql = new StringBuilder();
                                cmdsql.Append("Update  Officedba.PurchaseOrderDetail set ArrivedCount=isnull(ArrivedCount,0)-@ProductCount");
                                cmdsql.Append(" where CompanyCD=@CompanyCD and OrderNo=@FromBillNo and SortNo=@FromLineNo");

                                SqlParameter[] param = { 
                                           new SqlParameter("@CompanyCD", Model.CompanyCD),
                                            new SqlParameter("@ProductCount", ProductCount[i].ToString()),
                                             new SqlParameter("@FromBillNo", FromBillNo[i].ToString()),
                                              new SqlParameter("@FromLineNo", FromLineNo[i].ToString())
                                           };
                                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, cmdsql.ToString(), param);

                            }
                        }


                        FlowDBHelper.OperateCancelConfirm(Model.CompanyCD, Convert.ToInt32(ConstUtil.CODING_RULE_PURCHASE), Convert.ToInt32(ConstUtil.BILL_TYPEFLAG_PURCHASE_ARRIVE), Model.ID, strUserID, tran);//撤销审批
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
        public static bool ClosePurchaseArrive(PurchaseArriveModel Model)
        {
            #region SQL文
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.PurchaseArrive set ");
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

        #region 取消结单
        public static bool CancelClosePurchaseArrive(PurchaseArriveModel Model)
        {
            #region SQL文
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.PurchaseArrive set ");
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

        #region 查询到货通知列表所需数据
        public static DataTable SelectArriveList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ArriveNo, string Title, string TypeID, string Purchaser, string FromType, string ProviderID, string BillStatus, string UsedStatus, string ProjectID, string EFIndex, string EFDesc)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ID ,A.ArriveNo ,isnull(A.Title,'') AS Title,A.Rate ,A.TypeID,isnull(B.TypeName,'') AS TypeName,A.Purchaser,isnull(C.EmployeeName,'')  AS PurchaserName ");
            sql.AppendLine("      ,A.FromType,case A.FromType when '0' then '无来源' when '1' then '采购订单'end AS FromTypeName ,isnull(G.FromBillID,0) AS FromBillID ,A.BillStatus");
            sql.AppendLine("   ,A.ProviderID ,isnull(D.CustName,'') AS  ProviderName ,isnull(Convert(numeric(12,2),A.TotalMoney),0) AS TotalMoney,isnull(E.OrderNo,'') AS OrderNo,isnull(E.Title,'') AS OrderTitle ,isnull( A.ModifiedDate,'') AS ModifiedDate ");
            sql.AppendLine("      ,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'                ");
            sql.AppendLine("       when '4' then '手工结单' when '5' then '自动结单' end AS BillStatusName                              ");
            sql.AppendLine("      ,isnull(case F.FlowStatus when '0' then '请选择' when '1' then '待审批' when '2' then '审批中'    ");
            sql.AppendLine("      when '3'  then '审批通过' when '4' then '审批不通过' when '5' then '撤消审批' end,'')  AS  UsedStatus                      ");
            sql.AppendLine(" ,isnull(case(select count(1) from officedba.PurchaseRejectDetail AS H,officedba.PurchaseReject AS L where H.CompanyCD=A.CompanyCD and H.FromBillID = A.ID  and H.FromType='1' and H.RejectNo = L.RejectNo and L.BillStatus <> 1)+ ");
            sql.AppendLine("(select count(1) from officedba.StorageInPurchase AS I where I.CompanyCD=A.CompanyCD and I.FromBillID = A.ID  and I.FromType='1' )+");
            sql.AppendLine("(select count(1) from officedba.QualityCheckApplyDetail AS J,officedba.QualityCheckApplay AS M where J.CompanyCD=A.CompanyCD and J.FromBillID = A.ID  and J.FromType='1' and J.ApplyNo = M.ApplyNo )+");
            sql.AppendLine("(select count(1) from officedba.QualityCheckReport AS K where K.CompanyCD=A.CompanyCD and K.ReportID = A.ID  and K.FromType='4' ) when 0 then 'False' end , 'True') AS Isyinyong");
            sql.AppendLine(" FROM officedba.PurchaseArrive AS A                                                                     ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS B ON A.CompanyCD = B.CompanyCD AND B.TypeFlag=7 AND B.TypeCode=5 AND A.TypeID=B.ID");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Purchaser=C.ID                   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS D  ON  A.CompanyCD = D.CompanyCD AND A.ProviderID = D.ID              ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseArriveDetail AS G ON A.CompanyCD = G.CompanyCD AND A.ArriveNo=G.ArriveNo        ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseOrder AS E ON G.CompanyCD = E.CompanyCD   AND G.FromBillID=E.ID                 ");
            sql.AppendLine("LEFT JOIN officedba.FlowInstance AS F ON  A.CompanyCD = F.CompanyCD  AND F.BillTypeFlag = 6                ");
            sql.AppendLine("AND F.BillTypeCode = 5 AND F.BillNo = A.ArriveNo                                                         ");
            sql.AppendLine(" AND F.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = 6 AND F.BillTypeCode = 5 )");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD =@CompanyCD");
            if (ArriveNo != "" && ArriveNo != null)
            {
                sql.AppendLine(" AND A.ArriveNo like'%" + @ArriveNo + "%' ");
            }
            if (Title != null && Title != "")
            {
                sql.AppendLine(" AND A.Title like'%" + @Title + "%'  ");
            }
            if (TypeID != null && TypeID != "" && TypeID != "null")
            {
                sql.AppendLine(" AND A.TypeID =@TypeID");
            }
            if (ProjectID != null && ProjectID != "" && ProjectID != "null")
            {
                sql.AppendLine(" AND A.ProjectID =@ProjectID");
            }
            if (Purchaser != "" && Purchaser != null)
            {
                sql.AppendLine(" AND A.Purchaser=@Purchaser ");
            }
            if (!string.IsNullOrEmpty(FromType))
            {
                if (FromType != "-1")
                {
                    sql.AppendLine(" AND A.FromType =@FromType");
                }
            }

            if (ProviderID != "" && ProviderID != "")
            {
                sql.AppendLine(" AND A.ProviderID=@ProviderID ");
            }
            if (!string.IsNullOrEmpty(BillStatus))
            {
                if (BillStatus != "0")
                {
                    sql.AppendLine(" AND A.BillStatus = @BillStatus");
                }
            }

            if (UsedStatus != null && UsedStatus != "")
            {
                if (UsedStatus == "0")
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
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ArriveNo", ArriveNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", Title));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", TypeID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", ProjectID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Purchaser", Purchaser));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", FromType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", UsedStatus));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion

        #region 查找加载单个到货通知记录
        public static DataTable SelectArrive(int ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.ArriveNo ,A.Title ,A.ProviderID,isnull(B.CustName,'') AS ProviderName ,A.FromType,case A.FromType when '0' then '无来源' when '1' then '采购订单' end AS FromTypeName ,A.PayType,isnull(C.TypeName,'') AS PayTypeName,A.MoneyType,isnull(M.TypeName,'') AS  MoneyTypeName ");
            sql.AppendLine("     ,A.Purchaser,D.EmployeeName AS PurchaserName, A.DeptID,isnull(E.DeptName,'') AS DeptName ,isnull(CONVERT(varchar(100),A.ArriveDate,23),'') AS ArriveDate  ");
            sql.AppendLine("     ,A.TakeType, isnull(G.TypeName,'') AS TakeTypeName,A.BillStatus,CONVERT(varchar(100),A.ModifiedDate,23) AS  ModifiedDate");
            sql.AppendLine("     ,A.CarryType,isnull(H.TypeName,'') AS CarryTypeName ,Convert(numeric(12,2),A.TotalMoney) AS TotalMoney,Convert(numeric(12,2),A.TotalTax) AS TotalTax,Convert(numeric(12,2),A.TotalFee) AS TotalFee,Convert(numeric(12,2),A.Discount) AS Discount,Convert(numeric(12,2),A.DiscountTotal) AS DiscountTotal,Convert(numeric(12,2),A.RealTotal) AS RealTotal");
            sql.AppendLine("     ,A.isAddTax, case A.isAddTax when '0' then '否' when '1' then '是' end AS isAddTaxName ,A.CountTotal,A.Attachment,A.remark,isnull(CONVERT(varchar(100),A.CreateDate,23),'') AS  CreateDate                               ");
            sql.AppendLine("      ,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'when '4' then '手工结单'                              ");
            sql.AppendLine("       when '5' then '自动结单' end AS BillStatusName ,Convert(numeric(12,2),A.OtherTotal) AS OtherTotal                                     ");
            sql.AppendLine("     ,A.ModifiedUserID, A.Creator,isnull(Q.EmployeeName,'') AS CreatorName,A.Confirmor,isnull(J.EmployeeName,'') AS ConfirmorName,isnull(CONVERT(varchar(100),A.ConfirmDate,23),'') AS  ConfirmDate   ");
            sql.AppendLine("     ,A.Closer,isnull(K.EmployeeName,'') AS CloserName, isnull(CONVERT(varchar(100),A.CloseDate,23),'') AS  CloseDate, A.TypeID,isnull(L.TypeName,'') AS TypeIDName");
            sql.AppendLine("      ,case A.FromType when '0' then '无来源' when '1' then '采购订单' end as FromTypeName  ");
            sql.AppendLine("     ,isnull(A.SendAddress,'') AS SendAddress,isnull(A.ReceiveOverAddress,'') AS ReceiveOverAddress,A.CheckUserID,isnull(N.EmployeeName,'') AS CheckUserName,isnull(CONVERT(varchar(100),A.CheckDate,23),'') AS CheckDate,A.CurrencyType,A.Rate,O.CurrencyName AS  CurrencyTypeName");
            sql.AppendLine("      ,isnull(case P.FlowStatus when '0' then '请选择' when '1' then '待审批' when '2' then '审批中'    ");
            sql.AppendLine("      when '3'  then '审批通过' when '4' then '审批不通过' when '5' then '撤消审批' end,'')  AS  UsedStatus                ");
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
            sql.AppendLine(",isnull(a.CanViewUserName ,'') as UserName ");
            sql.AppendLine(",isnull(a.CanViewUser  ,'') as CanUserID  ");
            sql.AppendLine(",A.ProjectID,pi1.ProjectName ");
            sql.AppendLine(",A.isOpenbill ");

            sql.AppendLine(" FROM officedba.PurchaseArrive AS A                                                                           ");
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
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS N ON A.CompanyCD = N.CompanyCD  AND A.CheckUserID=N.ID                     ");
            sql.AppendLine("LEFT JOIN officedba.CurrencyTypeSetting AS O ON A.CompanyCD = O.CompanyCD  AND A.CurrencyType=O.ID              ");
            sql.AppendLine("LEFT JOIN officedba.FlowInstance AS P ON  A.CompanyCD = P.CompanyCD  AND P.BillTypeFlag = 6            ");
            sql.AppendLine("AND P.BillTypeCode = 5 AND P.BillNo = A.ArriveNo                                                         ");
            sql.AppendLine(" AND P.ID=(SELECT max(ID) FROM officedba.FlowInstance AS P WHERE A.ID = P.BillID AND P.BillTypeFlag = 6 AND P.BillTypeCode = 5 )");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS Q ON A.CompanyCD = Q.CompanyCD AND A.Creator=Q.ID                           ");



            sql.AppendLine("WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = @CompanyCD");
            sql.AppendLine(" AND A.ID = @ID");

            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@ID", Convert.ToString(ID));

            return SqlHelper.ExecuteSql(sql.ToString(), param);
        }
        #endregion

        #region 查找加载到货通知明细
        public static DataTable Details(int ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  distinct  A.ID,A.CompanyCD,A.ArriveNo ,isnull(A.SortNo,0) AS SortNo ,A.FromBillID,A.ProductID ");
            sql.AppendLine(",A.ProductNo ,A.ProductName,Convert(numeric(12,2),isnull(E.ProductCount,0)) AS ProductOrder,A.UnitID,Convert(numeric(12,2),isnull(A.ProductCount,0)) AS ProductCount    ");
            sql.AppendLine(", B.Specification AS Specification,Convert(numeric(12,2),isnull(A.ApplyCheckCount,0)) AS ApplyCheckCount,Convert(numeric(12,2),isnull(A.CheckedCount,0)) AS  CheckedCount");
            sql.AppendLine(", Convert(numeric(12,2),isnull(A.PassCount,0)) AS PassCount,Convert(numeric(12,2),isnull(A.NotPassCount,0)) AS NotPassCount,Convert(numeric(12,2),isnull(A.InCount,0)) AS InCount");
            sql.AppendLine(", Convert(numeric(12,2),isnull(A.RejectCount,0)) AS RejectCount,Convert(numeric(12,2),isnull(A.BackCount,0)) AS BackCount");
            sql.AppendLine(",ISNULL(cpt.TypeName,'') AS ColorName ");
            sql.AppendLine(",isnull(A.FromType,'') AS  FromType ,isnull(B.TaxRate,0) AS HidTaxRate                     ");
            sql.AppendLine(",case A.FromType when '0' then ''                                                          ");
            sql.AppendLine(" when '1' then (select OrderNo from officedba.PurchaseOrder where ID=A.FromBillID)         ");
            sql.AppendLine("  end AS FromBillNo                                                                        ");
            sql.AppendLine(",case when A.UnitID is null then '' when A.UnitID ='' then ''                         ");
            sql.AppendLine("else (select CodeName from officedba.CodeUnitType where id=A.UnitID)end as UnitName   ");
            sql.AppendLine(",Convert(numeric(12,2),A.UnitPrice) AS UnitPrice ,Convert(numeric(12,2),A.TaxPrice) AS TaxPrice,Convert(numeric(12,2),A.Discount) AS Discount,Convert(numeric(12,2),A.TaxRate) AS TaxRate,Convert(numeric(12,2),A.TotalFee) AS TotalFee,Convert(numeric(12,2),A.TotalPrice) AS TotalPrice,Convert(numeric(12,2),A.TotalTax) AS  TotalTax     ");
            sql.AppendLine(" ,isnull(case when A.FromBillID is null then 0 when A.FromBillID = '' then 0                ");
            sql.AppendLine(" else (select ArrivedCount from officedba.PurchaseOrderDetail where OrderNo=C.OrderNo and SortNo=A.FromLineNo) end,0) AS ArrivedCount");
            sql.AppendLine(",(select CodeName from officedba.CodeUnitType where id=A.UsedUnitID) as UsedUnitName   ");
            sql.AppendLine(",A.Remark,isnull(C.OrderNo,'') AS FromBillNo,A.FromLineNo,A.UsedUnitID,A.UsedUnitCount,A.UsedPrice    FROM officedba.PurchaseArriveDetail AS A     ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo   AS B on A.CompanyCD = B.CompanyCD  AND A.ProductID=B.ID    ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType cpt ON B.ColorID=cpt.ID                  ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseOrder   AS C on A.CompanyCD = C.CompanyCD  AND A.FromBillID=C.ID    ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseArrive   AS D on A.CompanyCD = D.CompanyCD  AND A.ArriveNo=D.ArriveNo   ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseOrderDetail   AS E on D.CompanyCD = E.CompanyCD  AND C.OrderNo=E.OrderNo  and E.sortNo=A.FromLineNo   ");

            sql.AppendLine("where A.CompanyCD =@CompanyCD AND D.ID =@ID ");

            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@ID", Convert.ToString(ID));

            return SqlHelper.ExecuteSql(sql.ToString(), param);
        }
        #endregion

        #region 删除采购到货通知主表及明细
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
                return false;
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
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.PurchaseArrive WHERE ArriveNo IN ( " + AllDetailNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.PurchaseArriveDetail WHERE ArriveNo IN ( " + AllDetailNo + " ) and CompanyCD='" + strCompanyCD + "'", null);

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

        #region 删除采购到货通知明细
        public static void DeletePurchasePlanDetail(string ArriveNo, ref string[] sql, int i)
        {
            string strSql = "delete officedba.PurchaseArriveDetail where CompanyCD='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and ArriveNo='" + ArriveNo + "'";

            sql[i] = strSql;
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

            strSql = "select count(1) from officedba.PurchaseArrive where ID = @ID and BillStatus=@BillStatus ";
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

        #region 判断采购到货有没有被引用
        public static bool IsCitePurchaseArrive(int ID)
        {
            UserInfoUtil userInfo = SessionUtil.Session["UserInfo"] as UserInfoUtil;
            bool IsCite = false;
            //入库引用
            if (!IsCite)
            {
                string sql = "SELECT A.ID FROM officedba.StorageInPurchase AS A WHERE A.FromType=@FromType AND A.FromBillID=@ID AND A.CompanyCD=@CompanyCD  ";
                SqlParameter[] parameters = {
                    new SqlParameter("@FromType", SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.Int),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar)
                                            };
                parameters[0].Value = "1";
                parameters[1].Value = ID;
                parameters[2].Value = userInfo.CompanyCD;
                IsCite = SqlHelper.Exists(sql, parameters);
            }
            //质量检测引用
            if (!IsCite)
            {
                string sql = "SELECT C.ID FROM officedba.QualityCheckApplyDetail AS C,officedba.QualityCheckApplay AS D WHERE C.FromType=@FromType AND C.FromBillID=@ID AND C.ApplyNo=D.ApplyNo  AND D.CompanyCD=@CompanyCD ";
                SqlParameter[] parameters = {
                    new SqlParameter("@FromType", SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.Int),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar)
                                            };
                parameters[0].Value = "1";
                parameters[1].Value = ID;
                parameters[2].Value = userInfo.CompanyCD;
                IsCite = SqlHelper.Exists(sql, parameters);
            }
            //质量检测引用
            if (!IsCite)
            {
                string sql = "SELECT E.ID FROM officedba.QualityCheckReport AS E WHERE E.FromType=@FromType AND E.ReportID=@ID  AND E.CompanyCD=@CompanyCD ";
                SqlParameter[] parameters = {
                    new SqlParameter("@FromType", SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.Int),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar)
                                            };
                parameters[0].Value = "4";
                parameters[1].Value = ID;
                parameters[2].Value = userInfo.CompanyCD;
                IsCite = SqlHelper.Exists(sql, parameters);
            }
            //采购退货引用
            if (!IsCite)
            {
                string sql = "SELECT F.ID FROM officedba.PurchaseRejectDetail AS F WHERE F.FromType=@FromType AND F.FromBillID=@ID AND F.CompanyCD=@CompanyCD ";
                SqlParameter[] parameters = {
                    new SqlParameter("@FromType", SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.Int),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar)
                                            };
                parameters[0].Value = "1";
                parameters[1].Value = ID;
                parameters[2].Value = userInfo.CompanyCD;
                IsCite = SqlHelper.Exists(sql, parameters);
            }
            //开票
            if (!IsCite)
            {
                string sql = "SELECT pa.ID FROM officedba.PurchaseArrive pa WHERE pa.id=@ID AND pa.isOpenbill=@FromType  AND pa.CompanyCD=@CompanyCD ";
                SqlParameter[] parameters = {
                    new SqlParameter("@FromType", SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.Int),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar)
                                            };
                parameters[0].Value = "1";
                parameters[1].Value = ID;
                parameters[2].Value = userInfo.CompanyCD;
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


                sql.AppendLine("SELECT isnull(A.ProductCount,0) as ProductCount ,isnull(A.ArrivedCount,0) as ArrivedCount FROM officedba.PurchaseOrderDetail AS A where CompanyCD= @CompanyCD ");
                sql.AppendLine("and OrderNo=@OrderNo  and SortNo=@SortNo ");

                SqlParameter[] param = new SqlParameter[3];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@OrderNo", FromBillNo);
                param[2] = SqlHelper.GetParameter("@SortNo", FromLineNo);
                DataTable dt = SqlHelper.ExecuteSql(sql.ToString(), param);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        decimal dlProductCount = Convert.ToDecimal(dt.Rows[0]["ProductCount"].ToString());
                        decimal dlArrivedCount = Convert.ToDecimal(dt.Rows[0]["ArrivedCount"].ToString());
                        Decimal tmp = dlProductCount - dlArrivedCount;
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

        #region 采购报表到货汇总查询
        /// <summary>
        /// 获得查询命令
        /// </summary>
        /// <param name="ProviderID"></param>
        /// <param name="ProductID"></param>
        /// <param name="StartConfirmDate"></param>
        /// <param name="EndConfirmDate"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="isDate"></param>
        /// <returns></returns>
        private static SqlCommand GetPurchaseArrive(string ProviderID, string ProductID, string StartConfirmDate
            , string EndConfirmDate, string CompanyCD, string orderBy, bool isDate, bool isPrint)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            sql.AppendLine("SELECT [ProductID]                                                     ");
            sql.AppendLine("      ,[ProviderID]                                                    ");
            sql.AppendLine("      ,[ProviderNo]                                                    ");
            sql.AppendLine("      ,ISNULL([ProviderName],'') AS ProviderName ");
            sql.AppendLine("      ,[ProductNo]                                                     ");
            sql.AppendLine("      ,[ProductName]                                                   ");
            sql.AppendLine("      ,[Specification]                                                 ");
            sql.AppendLine("      ,[UnitID]                                                        ");
            sql.AppendLine("      ,ISNULL([UnitName],'') AS UnitName ");
            if (isDate)
            {
                sql.AppendLine("      ,CONVERT(VARCHAR(10),ConfirmDate,120) AS ConfirmDate                                                    ");
                sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),[UnitPrice]) AS UnitPrice    ");
            }
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),SUM([ProductCount])) AS [ProductCount]    ");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),SUM([TotalFee])) AS [TotalFee]            ");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),SUM([TotalPrice])) AS [TotalPrice]        ");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),SUM(isnull([BackCount],0))) AS [BackCount]");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),SUM([BackTotalPrice])) AS [BackTotalPrice]");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),SUM([BackTotalFee])) AS [BackTotalFee]    ");
            sql.AppendLine("  FROM [officedba].[V_PurchaseArriveCollect]                   ");


            sql.AppendLine(" WHERE BillStatus<>'1' AND CompanyCD=@CompanyCD ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND ProviderID= @ProviderID   ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            }
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND ProductID =@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND ConfirmDate >= @StartConfirmDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND ConfirmDate <@EndConfirmDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", Convert.ToDateTime(EndConfirmDate).AddDays(1).ToString("yyyy-MM-dd")));
            }

            sql.AppendLine("GROUP BY [ProductID]                                                   ");
            sql.AppendLine("      ,[ProviderID]                                                    ");
            sql.AppendLine("      ,[ProviderNo]                                                    ");
            sql.AppendLine("      ,[ProviderName]                                                  ");
            sql.AppendLine("      ,[ProductNo]                                                     ");
            sql.AppendLine("      ,[ProductName]                                                   ");
            sql.AppendLine("      ,[Specification]                                                 ");
            sql.AppendLine("      ,[UnitID]                                                        ");
            sql.AppendLine("      ,[UnitName]                                                      ");
            if (isDate)
            {
                sql.AppendLine("      ,[ConfirmDate] ");
                sql.AppendLine("      ,[UnitPrice]   ");
            }
            if (isPrint)
            {
                sql.AppendLine(" ORDER BY " + orderBy + "");
            }

            comm.CommandText = sql.ToString();
            return comm;
        }

        /// <summary>
        /// 采购报表到货汇总查询(包括打印功能)
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="orderBy"></param>
        /// <param name="TotalCount"></param>
        /// <param name="ProviderID"></param>
        /// <param name="ProductID"></param>
        /// <param name="StartConfirmDate"></param>
        /// <param name="EndConfirmDate"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="isDate"></param>
        /// <param name="isPrint"></param>
        /// <returns></returns>
        public static DataTable PurchaseArriveCollect(int pageIndex, int pageCount, string orderBy
            , ref int TotalCount, string ProviderID, string ProductID, string StartConfirmDate
            , string EndConfirmDate, string CompanyCD, bool isDate, bool isPrint)
        {
            SqlCommand comm = GetPurchaseArrive(ProviderID, ProductID, StartConfirmDate, EndConfirmDate, CompanyCD, orderBy, isDate, isPrint);
            if (isPrint)
            {
                return SqlHelper.ExecuteSearch(comm);
            }
            else
            {
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
            }

        }

        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseArriveCollect(int pageIndex, int pageCount, string orderBy
            , ref int TotalCount, string ProviderID, string ProductID, string StartConfirmDate
            , string EndConfirmDate, string CompanyCD)
        {

            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            sql.AppendLine("SELECT [ProductID]                                                     ");
            sql.AppendLine("      ,[ProviderID]                                                    ");
            sql.AppendLine("      ,[ProviderNo]                                                    ");
            sql.AppendLine("      ,[ProviderName]                                                  ");
            sql.AppendLine("      ,[ProductNo]                                                     ");
            sql.AppendLine("      ,[ProductName]                                                   ");
            sql.AppendLine("      ,[Specification]                                                 ");
            sql.AppendLine("      ,[UnitID]                                                        ");
            sql.AppendLine("      ,[UnitName]                                                      ");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),SUM([ProductCount])) AS [ProductCount]    ");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),SUM([TotalFee])) AS [TotalFee]            ");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),SUM([TotalPrice])) AS [TotalPrice]        ");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),SUM(isnull([BackCount],0))) AS [BackCount]");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),SUM([BackTotalPrice])) AS [BackTotalPrice]");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),SUM([BackTotalFee])) AS [BackTotalFee]    ");
            sql.AppendLine("  FROM [officedba].[V_PurchaseArriveCollect]                   ");


            sql.AppendLine(" WHERE BillStatus<>'1' AND CompanyCD=@CompanyCD ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND ProviderID= @ProviderID   ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            }
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND ProductID =@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND ConfirmDate >= @StartConfirmDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND ConfirmDate <@EndConfirmDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", Convert.ToDateTime(EndConfirmDate).AddDays(1).ToString("yyyy-MM-dd")));
            }

            sql.AppendLine("GROUP BY [ProductID]                                                   ");
            sql.AppendLine("      ,[ProviderID]                                                    ");
            sql.AppendLine("      ,[ProviderNo]                                                    ");
            sql.AppendLine("      ,[ProviderName]                                                  ");
            sql.AppendLine("      ,[ProductNo]                                                     ");
            sql.AppendLine("      ,[ProductName]                                                   ");
            sql.AppendLine("      ,[Specification]                                                 ");
            sql.AppendLine("      ,[UnitID]                                                        ");
            sql.AppendLine("      ,[UnitName]                                                      ");

            //sql.AppendLine(" ORDER BY " + orderBy + "");
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);

        }


        public static DataTable PurchaseOrderCollectList(int pageIndex, int pageCount, string orderBy
            , ref int TotalCount, string ProviderID, string ProductID, string StartConfirmDate
            , string EndConfirmDate, string CompanyCD)
        {

            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            sql.AppendLine("SELECT [ProductID]                                                     ");
            sql.AppendLine("      ,[ProviderID]                                                    ");
            sql.AppendLine("      ,[ProviderNo]                                                    ");
            sql.AppendLine("      ,[ProviderName]                                                  ");
            sql.AppendLine("      ,[ProductNo]                                                     ");
            sql.AppendLine("      ,[ProductName]                                                   ");
            sql.AppendLine("      ,[Specification]                                                 ");
            sql.AppendLine("      ,[UnitID]                                                        ");
            sql.AppendLine("      ,[UnitName],PurchaseName    ,BillNo                                                  ");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "), ProductCount) AS [ProductCount]    ");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "), UnitPrice) AS [UnitPrice] ,Convert(varchar,ConfirmDate,23)  AS ConfirmDate   ,ColorName        "); 
            sql.AppendLine("  FROM [officedba].[View_PurchaseOrderCollectList]                   ");


            sql.AppendLine(" WHERE BillStatus<>'1' AND CompanyCD=@CompanyCD ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND ProviderID= @ProviderID   ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            }
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND ProductID =@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND ConfirmDate >= @StartConfirmDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND ConfirmDate <@EndConfirmDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", Convert.ToDateTime(EndConfirmDate).AddDays(1).ToString("yyyy-MM-dd")));
            }

            //sql.AppendLine("GROUP BY [ProductID]                                                   ");
            //sql.AppendLine("      ,[ProviderID]                                                    ");
            //sql.AppendLine("      ,[ProviderNo]                                                    ");
            //sql.AppendLine("      ,[ProviderName]                                                  ");
            //sql.AppendLine("      ,[ProductNo]                                                     ");
            //sql.AppendLine("      ,[ProductName]                                                   ");
            //sql.AppendLine("      ,[Specification]                                                 ");
            //sql.AppendLine("      ,[UnitID]                                                        ");
            //sql.AppendLine("      ,[UnitName]                                                      ");

            //sql.AppendLine(" ORDER BY " + orderBy + "");
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);

        }
        public static DataTable PurchaseOrderCollectListPrint(  string ProviderID, string ProductID, string StartConfirmDate
      , string EndConfirmDate, string CompanyCD, string orderBy)
        {

            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            sql.AppendLine("SELECT [ProductID]                                                     ");
            sql.AppendLine("      ,[ProviderID]                                                    ");
            sql.AppendLine("      ,[ProviderNo]                                                    ");
            sql.AppendLine("      ,[ProviderName]                                                  ");
            sql.AppendLine("      ,[ProductNo]                                                     ");
            sql.AppendLine("      ,[ProductName]                                                   ");
            sql.AppendLine("      ,[Specification]                                                 ");
            sql.AppendLine("      ,[UnitID]                                                        ");
            sql.AppendLine("      ,[UnitName],PurchaseName    ,BillNo                                                  ");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "), ProductCount) AS [ProductCount]    ");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "), UnitPrice) AS [UnitPrice] ,Convert(varchar,ConfirmDate,23)  AS ConfirmDate   ,ColorName        ");
            sql.AppendLine("  FROM [officedba].[View_PurchaseOrderCollectList]                   ");


            sql.AppendLine(" WHERE BillStatus<>'1' AND CompanyCD=@CompanyCD ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND ProviderID= @ProviderID   ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            }
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND ProductID =@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND ConfirmDate >= @StartConfirmDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND ConfirmDate <@EndConfirmDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", Convert.ToDateTime(EndConfirmDate).AddDays(1).ToString("yyyy-MM-dd")));
            }

            sql.AppendLine("   ORDER BY " + orderBy + "");
            //sql.AppendLine("GROUP BY [ProductID]                                                   ");
            //sql.AppendLine("      ,[ProviderID]                                                    ");
            //sql.AppendLine("      ,[ProviderNo]                                                    ");
            //sql.AppendLine("      ,[ProviderName]                                                  ");
            //sql.AppendLine("      ,[ProductNo]                                                     ");
            //sql.AppendLine("      ,[ProductName]                                                   ");
            //sql.AppendLine("      ,[Specification]                                                 ");
            //sql.AppendLine("      ,[UnitID]                                                        ");
            //sql.AppendLine("      ,[UnitName]                                                      ");

            //sql.AppendLine(" ORDER BY " + orderBy + "");
            comm.CommandText = sql.ToString();
            // return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
            return SqlHelper.ExecuteSearch(comm);

        }

        public static DataTable PurchaseArriveCollectList(int pageIndex, int pageCount, string orderBy
     , ref int TotalCount, string ProviderID, string ProductID, string StartConfirmDate
     , string EndConfirmDate, string CompanyCD)
        {

            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            sql.AppendLine("SELECT [ProductID]                                                     ");
            sql.AppendLine("      ,[ProviderID]                                                    ");
            sql.AppendLine("      ,[ProviderNo]                                                    ");
            sql.AppendLine("      ,[ProviderName]                                                  ");
            sql.AppendLine("      ,[ProductNo]                                                     ");
            sql.AppendLine("      ,[ProductName]                                                   ");
            sql.AppendLine("      ,[Specification]                                                 ");
            sql.AppendLine("      ,[UnitID]                                                        ");
            sql.AppendLine("      ,[UnitName]   ,PurchaseName    ,BillNo                                                      ");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),ProductCount) AS [ProductCount]    ");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),UnitPrice) AS [UnitPrice] ,Convert(varchar,ConfirmDate,23)  AS ConfirmDate,ColorName          "); 
            sql.AppendLine("  FROM [officedba].[View_PurchaseArriveCollectList]                   ");


            sql.AppendLine(" WHERE BillStatus<>'1' AND CompanyCD=@CompanyCD ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND ProviderID= @ProviderID   ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            }
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND ProductID =@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND ConfirmDate >= @StartConfirmDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND ConfirmDate <@EndConfirmDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", Convert.ToDateTime(EndConfirmDate).AddDays(1).ToString("yyyy-MM-dd")));
            }

            //sql.AppendLine("GROUP BY [ProductID]                                                   ");
            //sql.AppendLine("      ,[ProviderID]                                                    ");
            //sql.AppendLine("      ,[ProviderNo]                                                    ");
            //sql.AppendLine("      ,[ProviderName]                                                  ");
            //sql.AppendLine("      ,[ProductNo]                                                     ");
            //sql.AppendLine("      ,[ProductName]                                                   ");
            //sql.AppendLine("      ,[Specification]                                                 ");
            //sql.AppendLine("      ,[UnitID]                                                        ");
            //sql.AppendLine("      ,[UnitName]                                                      ");

            //sql.AppendLine(" ORDER BY " + orderBy + "");
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);

        }
        
        #endregion

        #region 采购报表到货汇总查询打印
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseArriveCollectPrint(string ProviderID, string ProductID
            , string StartConfirmDate, string EndConfirmDate, string CompanyCD, string orderBy)
        {

            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT [ProductID]                                                     ");
            sql.AppendLine("      ,[ProviderID]                                                    ");
            sql.AppendLine("      ,[ProviderNo]                                                    ");
            sql.AppendLine("      ,isnull( ProviderName,'') as ProviderName                                                  ");
            sql.AppendLine("      ,[ProductNo]                                                     ");
            sql.AppendLine("      ,isnull(ProductName,'') as ProductName                                               ");
            sql.AppendLine("      ,isnull(Specification,'') as Specification                                                 ");
            sql.AppendLine("      ,[UnitID]                                                        ");
            sql.AppendLine("      ,isnull(UnitName,'') as UnitName                                                      ");
            sql.AppendLine("      ,CONVERT(numeric(20," + jingdu + "),SUM([ProductCount])) AS [ProductCount]    ");
            sql.AppendLine("      ,CONVERT(numeric(22," + jingdu + "),SUM([TotalFee])) AS [TotalFee]            ");
            sql.AppendLine("      ,CONVERT(numeric(22," + jingdu + "),SUM([TotalPrice])) AS [TotalPrice]        ");
            sql.AppendLine("      ,CONVERT(numeric(20," + jingdu + "),SUM(isnull([BackCount],0))) AS [BackCount]");
            sql.AppendLine("      ,CONVERT(numeric(20," + jingdu + "),SUM([BackTotalPrice])) AS [BackTotalPrice]");
            sql.AppendLine("      ,CONVERT(numeric(20," + jingdu + "),SUM([BackTotalFee])) AS [BackTotalFee]    ");
            sql.AppendLine("  FROM [officedba].[V_PurchaseArriveCollect]                   ");


            sql.AppendLine(" WHERE BillStatus<>'1' AND CompanyCD=@CompanyCD ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND ProviderID= @ProviderID    ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            }
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND ProductID =@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND ConfirmDate >=@StartConfirmDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND ConfirmDate <@EndConfirmDate  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", Convert.ToDateTime(EndConfirmDate).AddDays(1).ToString("yyyy-MM-dd")));
            }

            sql.AppendLine("GROUP BY [ProductID]                                                   ");
            sql.AppendLine("      ,[ProviderID]                                                    ");
            sql.AppendLine("      ,[ProviderNo]                                                    ");
            sql.AppendLine("      ,[ProviderName]                                                  ");
            sql.AppendLine("      ,[ProductNo]                                                     ");
            sql.AppendLine("      ,[ProductName]                                                   ");
            sql.AppendLine("      ,[Specification]                                                 ");
            sql.AppendLine("      ,[UnitID]                                                        ");
            sql.AppendLine("      ,[UnitName]                                                      ");

            sql.AppendLine(" ORDER BY " + orderBy + "");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

            #endregion
        }



        public static DataTable PurchaseArriveCollectListPrint(  string ProviderID, string ProductID, string StartConfirmDate
 , string EndConfirmDate, string CompanyCD, string orderBy)
        {

            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            sql.AppendLine("SELECT [ProductID]                                                     ");
            sql.AppendLine("      ,[ProviderID]                                                    ");
            sql.AppendLine("      ,[ProviderNo]                                                    ");
            sql.AppendLine("      ,[ProviderName]                                                  ");
            sql.AppendLine("      ,[ProductNo]                                                     ");
            sql.AppendLine("      ,[ProductName]                                                   ");
            sql.AppendLine("      ,[Specification]                                                 ");
            sql.AppendLine("      ,[UnitID]                                                        ");
            sql.AppendLine("      ,[UnitName]   ,PurchaseName    ,BillNo                                                      ");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),ProductCount) AS [ProductCount]    ");
            sql.AppendLine("      ,CONVERT(numeric(12," + userInfo.SelPoint + "),UnitPrice) AS [UnitPrice] ,Convert(varchar,ConfirmDate,23)  AS ConfirmDate,ColorName          ");
            sql.AppendLine("  FROM [officedba].[View_PurchaseArriveCollectList]                   ");


            sql.AppendLine(" WHERE BillStatus<>'1' AND CompanyCD=@CompanyCD ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND ProviderID= @ProviderID   ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            }
            if (ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" AND ProductID =@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND ConfirmDate >= @StartConfirmDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND ConfirmDate <@EndConfirmDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", Convert.ToDateTime(EndConfirmDate).AddDays(1).ToString("yyyy-MM-dd")));
            }



            sql.AppendLine("   ORDER BY " + orderBy + "");
            //sql.AppendLine("GROUP BY [ProductID]                                                   ");
            //sql.AppendLine("      ,[ProviderID]                                                    ");
            //sql.AppendLine("      ,[ProviderNo]                                                    ");
            //sql.AppendLine("      ,[ProviderName]                                                  ");
            //sql.AppendLine("      ,[ProductNo]                                                     ");
            //sql.AppendLine("      ,[ProductName]                                                   ");
            //sql.AppendLine("      ,[Specification]                                                 ");
            //sql.AppendLine("      ,[UnitID]                                                        ");
            //sql.AppendLine("      ,[UnitName]                                                      ");

            //sql.AppendLine(" ORDER BY " + orderBy + "");
            comm.CommandText = sql.ToString();
           // return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        #region 采购报表采购到货查询
        public static DataTable PurchaseArriveQuery(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProviderID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ID ,A.ArriveNo ,isnull(A.Title,'') AS Title ,A.TypeID,isnull(B.TypeName,'') AS TypeName,A.Purchaser,isnull(C.EmployeeName,'') AS EmployeeName ");
            sql.AppendLine(" ,isnull(A.ProviderID,0) AS ProviderID, isnull(D.CustName,'') AS ProviderName, Convert(numeric(20,2),isnull(A.RealTotal,0)*isnull(A.Rate,0)) AS RealTotal                        ");
            sql.AppendLine(" FROM officedba.PurchaseArrive AS A                                                                     ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS B ON A.CompanyCD = B.CompanyCD AND B.TypeFlag=7 AND B.TypeCode=5 AND A.TypeID=B.ID");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Purchaser=C.ID                   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS D  ON  A.CompanyCD = D.CompanyCD AND A.ProviderID = D.ID              ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.BillStatus <>1 AND A.CompanyCD =@CompanyCD");
            if (ProviderID != "" && ProviderID != "")
            {
                sql.AppendLine(" AND A.ProviderID=@ProviderID ");
            }
            if (StartConfirmDate != null && StartConfirmDate != "")
            {
                sql.AppendLine(" AND A.ArriveDate >= @StartConfirmDate");
            }
            if (EndConfirmDate != null && EndConfirmDate != "")
            {
                sql.AppendLine(" AND A.ArriveDate <= @EndConfirmDate ");
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", EndConfirmDate));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion

        #region 采购报表采购到货查询打印
        public static DataTable PurchaseArriveQueryPrint(string ProviderID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ID ,A.ArriveNo AS CompanyCD ,isnull(A.Title,'') AS ArriveNo ,A.TypeID,isnull(B.TypeName,'') AS Title,A.Purchaser,isnull(C.EmployeeName,'') AS Attachment ");
            sql.AppendLine(" ,isnull(A.ProviderID,0) AS ProviderID, isnull(D.CustName,'') AS remark, Convert(numeric(20,2),isnull(A.RealTotal,0)) AS RealTotal                        ");
            sql.AppendLine(" FROM officedba.PurchaseArrive AS A                                                                     ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS B ON A.CompanyCD = B.CompanyCD AND B.TypeFlag=7 AND B.TypeCode=5 AND A.TypeID=B.ID");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Purchaser=C.ID                   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS D  ON  A.CompanyCD = D.CompanyCD AND A.ProviderID = D.ID              ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.BillStatus <>1 AND A.CompanyCD = @CompanyCD");
            if (ProviderID != "" && ProviderID != "")
            {
                sql.AppendLine(" AND A.ProviderID=@ProviderID ");
            }
            if (StartConfirmDate != null && StartConfirmDate != "")
            {
                sql.AppendLine(" AND A.ArriveDate >= @StartConfirmDate");
            }
            if (EndConfirmDate != null && EndConfirmDate != "")
            {
                sql.AppendLine(" AND A.ArriveDate <= @EndConfirmDate ");
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", EndConfirmDate));

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

    }
}

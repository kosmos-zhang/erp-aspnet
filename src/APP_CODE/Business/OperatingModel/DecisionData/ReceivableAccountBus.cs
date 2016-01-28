/**********************************************
 * 类作用：   决策支持分析
 * 建立人：   王玉贞
 * 建立时间： 2010/05/28
 ***********************************************/

using System;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;
using XBase.Model.Office.SupplyChain;
using XBase.Data.OperatingModel.DecisionData;

namespace XBase.Business.OperatingModel.DecisionData
{
    public class ReceivableAccountBus
    {
        #region 单个客户应收账款余额查询
        /// <summary>
        /// 单个客户应收账款余额查询
        /// </summary>
        /// <param name="CustID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetReceivableAccount(string CustID, string CompanyCD)
        {
            try
            {
                return ReceivableAccountDBHelper.GetReceivableAccount(CustID,CompanyCD);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 获取应收账款余额列表
        /// <summary>
        /// 获取应收账款余额列表
        /// </summary>
        /// <param name="CustID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetReceivaleAccountList(string CustID,string CompanyCD)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            /*1.创建一个新的表*/
            DataTable dbNew = new DataTable();
            dbNew.Columns.Add("CustID");
            dbNew.Columns.Add("CustName");
            dbNew.Columns.Add("MaxCredit");
            dbNew.Columns.Add("ReciverFee");
            dbNew.Columns.Add("UnPayFee");
            dbNew.Columns.Add("AdvanceChargeFee");
            dbNew.Columns.Add("OverMaxCredit");

            if (string.IsNullOrEmpty(CustID))
            {
                /*2.循环客户档案*/
                DataTable dtCust = ReceivableAccountBus.GetCustInfo(CompanyCD);
                if (dtCust.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCust.Rows.Count; i++)
                    {
                        DataTable dt = ReceivableAccountBus.GetReceivableAccount(dtCust.Rows[i]["ID"].ToString(), CompanyCD);
                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dbNew.NewRow();
                            dr["CustID"] = dtCust.Rows[i]["ID"].ToString();
                            dr["CustName"] = dtCust.Rows[i]["CustName"].ToString();
                            dr["MaxCredit"] = dtCust.Rows[i]["MaxCredit"].ToString();
                            dr["ReciverFee"] = dt.Rows[0]["OrderFee"];
                            dr["UnPayFee"] = dt.Rows[0]["UnPayFee"];
                            dr["AdvanceChargeFee"] = dt.Rows[0]["AdvanceChargeFee"];
                            decimal advChargeFee = Decimal.Parse(dt.Rows[0]["AdvanceChargeFee"].ToString());
                            decimal unPayFee = Decimal.Parse(dt.Rows[0]["UnPayFee"].ToString());
                            decimal maxCredit = Decimal.Parse(dtCust.Rows[i]["MaxCredit"].ToString());
                            decimal unRealPayFee = Math.Abs(advChargeFee - unPayFee);
                            dr["OverMaxCredit"] = Decimal.Parse("0.000000");
                            
                            /*1.信用额度为0时
                             *      预收小于未收,超信用额度=预收 - 未收
                             *2.信用额度大于0时
                             *      实际未收等于0时，超信用额度=0
                             *      实际未收 >信用额,则超信用额  = 实际未收的绝对值  -超信用额 
                             */
                            if (maxCredit == 0)
                            {
                                if (advChargeFee < unPayFee)
                                {
                                    dr["OverMaxCredit"] = unRealPayFee;
                                }
                            }
                            else
                            {
                                if (unRealPayFee == 0)
                                {
                                    /*若实际未付的等于零,则超信用额度=0*/
                                }
                                else
                                {
                                    if (unRealPayFee > maxCredit)
                                    {
                                        dr["OverMaxCredit"] = unRealPayFee - maxCredit;
                                    }

                                }
                            }
                            dbNew.Rows.Add(dr);
                        }

                    }
                }
            }
            else
            {
                DataTable dt = ReceivableAccountBus.GetReceivableAccount(CustID, CompanyCD);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dbNew.NewRow();
                    dr["CustID"] = CustID;
                    dr["CustName"] = dt.Rows[0]["CustName"];//待修改
                    dr["MaxCredit"] = dt.Rows[0]["MaxCredit"];
                    dr["ReciverFee"] = dt.Rows[0]["OrderFee"];
                    dr["UnPayFee"] = dt.Rows[0]["UnPayFee"];
                    dr["AdvanceChargeFee"] = dt.Rows[0]["AdvanceChargeFee"];
                    decimal advChargeFee = Decimal.Parse(dt.Rows[0]["AdvanceChargeFee"].ToString());
                    decimal unPayFee = Decimal.Parse(dt.Rows[0]["UnPayFee"].ToString());
                    decimal maxCredit = Decimal.Parse(dt.Rows[0]["MaxCredit"].ToString());
                    decimal unRealPayFee = Math.Abs(advChargeFee - unPayFee);
                    dr["OverMaxCredit"] = Decimal.Parse("0.000000");
                    /*1.信用额度为0时
                     *      预收小于未收,超信用额度=预收 - 未收
                     *2.信用额度大于0时
                     *      实际未收等于0时，超信用额度=0
                     *      实际未收 >信用额,则超信用额  = 实际未收的绝对值  -超信用额 
                     */
                    if (maxCredit == 0)
                    {
                        if (advChargeFee < unPayFee)
                        {
                            dr["OverMaxCredit"] = unRealPayFee;
                        }
                    }
                    else
                    {
                        if (unRealPayFee == 0)
                        {
                            /*若实际未付的等于零,则超信用额度=0*/
                        }
                        else
                        {
                            if (unRealPayFee > maxCredit)
                            {
                                dr["OverMaxCredit"] = unRealPayFee - maxCredit;
                            }

                        }
                    }
                    dbNew.Rows.Add(dr);
                }
            }
            return dbNew;
        }

        #endregion


        #region 所有客户
        /// <summary>
        /// 所有客户
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetCustInfo(string CompanyCD)
        {
            try
            {
                return ReceivableAccountDBHelper.GetCustInfo(CompanyCD);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

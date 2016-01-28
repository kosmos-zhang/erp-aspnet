using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Common;
using XBase.Data.Common;
using System.Collections;
using XBase.Data.Office.FinanceManager;
namespace XBase.Business.Office.FinanceManager
{
    public class FinanceReportBus
    {
     
        #region  销货成本明细表_报表
        /// <summary>
        /// 销货成本明细表
        /// </summary>
        /// <param name="Year">年</param>
        /// <param name="Month">月</param>
        /// <returns></returns>
        public static DataTable GetSellCostDetail(string Year, string Month, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return FinanceReportDBHelper.GetSellCostDetail(Year,Month,CompanyCD,pageIndex,pageCount,ord,ref TotalCount);
        }

        /// <summary>
        /// 销货成本明细表
        /// </summary>
        /// <param name="Year">年</param>
        /// <param name="Month">月</param>
        /// <returns></returns>
        public static DataTable GetSellCostDetailPrint(string Year, string Month, string CompanyCD, string ord)
        {
            return FinanceReportDBHelper.GetSellCostDetailPrint(Year, Month, CompanyCD,ord);

        }
        #endregion

        #region  销货收入月报表_报表
        /// <summary>
        /// 销货收入月报表
        /// </summary>
        /// <param name="Year">年</param>
        /// <param name="Month">月</param>
        /// <returns></returns>
        public static DataTable GetSellMonthIncome(string Year, string Month, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return FinanceReportDBHelper.GetSellMonthIncome(Year, Month, CompanyCD, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 销货成本明细表
        /// </summary>
        /// <param name="Year">年</param>
        /// <param name="Month">月</param>
        /// <returns></returns>
        public static DataTable GetSellMonthIncomePrint(string Year, string Month, string CompanyCD, string ord)
        {
            return FinanceReportDBHelper.GetSellMonthIncomePrint(Year, Month, CompanyCD, ord);

        }
        #endregion

        #region 获取账簿中不同的客户--应收帐款
        /// <summary>
        /// 获取账簿中不同的客户--应收帐款
        /// </summary>
        /// <param name="CurryType">币种</param>
        /// <param name="SubjectsCD">会计科目</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetDistinctCusFromAcountBook(string CurryType, string SubjectsCD)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                return FinanceReportDBHelper.GetDistinctCusFromAcountBook(CurryType, SubjectsCD, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 获取某会计期间某科目某币种对应的详细信息
        /// <summary>
        /// 获取某会计期间某科目某币种对应的详细信息
        /// </summary>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="CurryType">币种</param>
        /// <param name="SubjectsCD">会计科目</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="SubjectsDetails">辅助核算主键</param>
        /// <param name="FormTBName">来源表</param>
        /// <param name="FileName">来源表字段名</param>
        /// <returns></returns>
        public static DataTable GetAccountBookInfo(string StartDate, string EndDate, string CurryType, string SubjectsCD,string SubjectsDetails, string FormTBName, string FileName)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                return FinanceReportDBHelper.GetAccountBookInfo(StartDate, EndDate, CurryType, SubjectsCD, CompanyCD, SubjectsDetails, FormTBName, FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 应收账款月报表
        /// <summary>
        /// 应收账款月报表
        /// </summary>
        /// <param name="AccountDate">会计期间</param>
        /// <param name="CurryType">币种</param>
        /// <param name="SubjectsCD">会计科目</param>
        /// <param name="SubjectsDetails">辅助核算主键</param>
        /// <param name="FormTBName">来源表</param>
        /// <param name="FileName">来源表字段</param>
        /// <returns></returns>
        public static DataTable GetReceivableMothRepter(string AccountDate, string CurryType, string SubjectsCD, string SubjectsDetails, string FormTBName, string FileName)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码

                #region 对输入的资料日期进行处理开始

                string startDate = string.Empty;
                string endDate = string.Empty;


                int MothString = Convert.ToInt32(AccountDate.Split('-')[1].ToString());
                int yearString = Convert.ToInt32(AccountDate.Split('-')[0].ToString());
                string daycount = string.Empty;
                if (MothString == 4 || MothString == 6 || MothString == 9 || MothString == 11)
                {
                    daycount = "-30";
                }
                else if (MothString == 2)
                {
                    if (((yearString % 4 == 0 && yearString % 100 == 0)) || (yearString % 400 == 0))
                    {
                        daycount = "-29";
                    }
                    else
                    {
                        daycount = "-28";
                    }
                }
                else
                {
                    daycount = "-31";
                }

                startDate = AccountDate + "-01";
                endDate = AccountDate + daycount;

                #endregion

                #region 若查询条件中带出具体客户，清空获取账簿中不同的客户记录，添加查询条件中的客户信息到SubjectsDetailsDT

                DataTable SubjectsDetailsDT = FinanceReportDBHelper.GetDistinctCusFromAcountBook(CurryType, SubjectsCD, CompanyCD);//获取账簿中不同的客户--应收帐款
                /* 若查询条件中带出具体客户，清空获取账簿中不同的客户记录，添加查询条件中的客户信息到SubjectsDetailsDT  开始*/
                if (SubjectsDetails.Trim().Length > 0)
                {
                    SubjectsDetailsDT.Clear();
                    DataRow row = SubjectsDetailsDT.NewRow();
                    row["SubjectsDetails"] = SubjectsDetails;
                    row["FormTBName"] = FormTBName;
                    row["FileName"] = FileName;
                    SubjectsDetailsDT.Rows.Add(row);
                }

                /* 若查询条件中带出具体客户，清空获取账簿中不同的客户记录，添加查询条件中的客户信息到SubjectsDetailsDT  结束*/

                #endregion

                #region 定义数据源Table

                DataTable dt = new DataTable();
                dt.Columns.Add("VoucherDate");//日期
                dt.Columns.Add("CusOrPro");//客户
                dt.Columns.Add("Abstract");//摘要
                dt.Columns.Add("BeginAmount");//期初金额
                dt.Columns.Add("ThisDebit");//本期借方金额
                dt.Columns.Add("ThisCredit");//本期贷方金额
                dt.Columns.Add("Direction");//余额方向
                dt.Columns.Add("EndAmount");//期末余额
                dt.Columns.Add("SourceCode");//源单编码
                dt.Columns.Add("Type");//源单类别
                dt.Columns.Add("SourceDate");//源单日期
                dt.Columns.Add("TotalAmount");//总金额
                dt.Columns.Add("BlendingAmount");//回款金额
                dt.Columns.Add("Scale");//回款比例
                dt.Columns.Add("ByOrder");//排序

                #endregion

                foreach (DataRow dr in SubjectsDetailsDT.Rows)
                {
                    DataTable AccountBookInfo = GetAccountBookInfo(startDate, endDate, CurryType, SubjectsCD, dr["SubjectsDetails"].ToString(), dr["FormTBName"].ToString(), dr["FileName"].ToString());
                    decimal BeginAmount=AcountBookDBHelper.GetSubjectsBeginDetailAmount(SubjectsCD,CurryType,CompanyCD,dr["SubjectsDetails"].ToString(), dr["FormTBName"].ToString(), dr["FileName"].ToString());
                    foreach (DataRow drr in AccountBookInfo.Rows)
                    {
                        if (drr["FromTbale"].ToString().Trim().Length > 0 && (drr["FromTbale"].ToString().Equals("officedba.IncomeBill") || drr["FromTbale"].ToString().Equals("officedba.PayBill")))//根据凭证主表的值对应获取勾兑明细
                        {
                            BlendingDetailsBus bus = new BlendingDetailsBus();
                            //根据凭证主表的来源表及来源表主键__获取对应的勾兑明细信息 Start
                            DataTable BlendingSourceDT = bus.GetBlendingSoureByTB(drr["FromTbale"].ToString(), drr["FromValue"].ToString());
                            //根据凭证主表的来源表及来源表主键__获取对应的勾兑明细信息 End
                            if (BlendingSourceDT.Rows.Count > 1)//判断是否含有勾兑明细
                            {
                                for (int i = 0; i < BlendingSourceDT.Rows.Count; i++)
                                {
                                    DataRow row = dt.NewRow();
                                    if (i == 0)
                                    {
                                        row["VoucherDate"] = drr["VoucherDate"].ToString();//日期
                                        row["CusOrPro"] = VoucherDBHelper.GetAssistantName(dr["SubjectsDetails"].ToString(), dr["FormTBName"].ToString(), dr["FileName"].ToString());//客户
                                        row["Abstract"] = drr["Abstract"].ToString();//摘要
                                        row["BeginAmount"] = BeginAmount.ToString("#,###0.#0");//期初金额
                                        if (CurryType.LastIndexOf(",") == -1)
                                        {
                                            row["ThisDebit"] = Math.Round(Convert.ToDecimal(drr["ForeignThisDebit"].ToString()),2).ToString("#,###0.#0");//本期借方金额
                                            row["ThisCredit"] = Math.Round(Convert.ToDecimal(drr["ForeignThisCredit"].ToString()),2).ToString("#,###0.#0");//本期贷方金额
                                        }
                                        else
                                        {
                                            row["ThisDebit"] = Math.Round(Convert.ToDecimal(drr["ThisDebit"].ToString()),2).ToString("#,###0.#0");//本期借方金额
                                            row["ThisCredit"] = Math.Round(Convert.ToDecimal(drr["ThisCredit"].ToString()),2).ToString("#,###0.#0");//本期贷方金额
                                        }

                                        decimal m=BeginAmount + Convert.ToDecimal(row["ThisDebit"].ToString()) - Convert.ToDecimal(row["ThisCredit"].ToString());
                                        row["EndAmount"] = Math.Round(m,2).ToString("#,###0.#0");//期末余额
                                        row["Direction"] = AcountBookDBHelper.DirectionSource(SubjectsCD, Convert.ToDecimal(row["EndAmount"].ToString()));//余额方向
                                        row["SourceCode"] = BlendingSourceDT.Rows[i]["BillCD"].ToString();
                                        row["Type"] = GetBillType(BlendingSourceDT.Rows[i]["BillingType"].ToString());
                                        row["SourceDate"] = BlendingSourceDT.Rows[i]["CreateDate"].ToString();
                                        row["TotalAmount"] = Math.Round(Convert.ToDecimal(BlendingSourceDT.Rows[i]["TotalPrice"].ToString()),2).ToString("#,###0.#0");
                                        row["BlendingAmount"] = Math.Round(Convert.ToDecimal(BlendingSourceDT.Rows[i]["BlendingAmount"].ToString()), 2).ToString("#,###0.#0");
                                        row["Scale"] = Convert.ToString(Math.Round(Convert.ToDecimal(BlendingSourceDT.Rows[i]["BlendingAmount"].ToString()) / Convert.ToDecimal(BlendingSourceDT.Rows[i]["TotalPrice"].ToString()) * 100, 2)) + "%";
                                        row["ByOrder"] = "1";


                                    }
                                    else
                                    {
                                        row["VoucherDate"] = "";//日期
                                        row["CusOrPro"] = "";//客户
                                        row["Abstract"] = "";//摘要
                                        row["BeginAmount"] = "";//期初金额
                                        row["ThisDebit"] = "";//本期借方金额
                                        row["ThisCredit"] = "";//本期贷方金额
                                        row["EndAmount"] = "";//期末余额
                                        row["Direction"] = "";//余额方向
                                        row["SourceCode"] = BlendingSourceDT.Rows[i]["BillCD"].ToString();
                                        row["Type"] = GetBillType(BlendingSourceDT.Rows[i]["BillingType"].ToString());
                                        row["SourceDate"] = BlendingSourceDT.Rows[i]["CreateDate"].ToString();
                                        row["TotalAmount"] = Math.Round(Convert.ToDecimal(BlendingSourceDT.Rows[i]["TotalPrice"].ToString()),2).ToString("#,###0.#0");
                                        row["BlendingAmount"] = Math.Round(Convert.ToDecimal(BlendingSourceDT.Rows[i]["BlendingAmount"].ToString()), 2).ToString("#,###0.#0");
                                        row["Scale"] = Convert.ToString(Math.Round(Convert.ToDecimal(BlendingSourceDT.Rows[i]["BlendingAmount"].ToString()) / Convert.ToDecimal(BlendingSourceDT.Rows[i]["TotalPrice"].ToString()) * 100, 2)) + "%";
                                        row["ByOrder"] = "1";
                                    }
                                    dt.Rows.Add(row);
                                }
                            }
                            else
                            {
                                DataRow row = dt.NewRow();
                                row["VoucherDate"] = drr["VoucherDate"].ToString();//日期
                                row["CusOrPro"] = VoucherDBHelper.GetAssistantName(dr["SubjectsDetails"].ToString(), dr["FormTBName"].ToString(), dr["FileName"].ToString());//客户
                                row["Abstract"] = drr["Abstract"].ToString();//摘要
                                row["BeginAmount"] = BeginAmount.ToString("#,###0.#0");//期初金额
                                if (CurryType.LastIndexOf(",") == -1)
                                {
                                    row["ThisDebit"] = Math.Round(Convert.ToDecimal(drr["ForeignThisDebit"].ToString()),2).ToString("#,###0.#0");//本期借方金额
                                    row["ThisCredit"] = Math.Round(Convert.ToDecimal(drr["ForeignThisCredit"].ToString()),2).ToString("#,###0.#0");//本期贷方金额
                                }
                                else
                                {
                                    row["ThisDebit"] = Math.Round(Convert.ToDecimal(drr["ThisDebit"].ToString()),2).ToString("#,###0.#0");//本期借方金额
                                    row["ThisCredit"] = Math.Round(Convert.ToDecimal(drr["ThisCredit"].ToString()),2).ToString("#,###0.#0");//本期贷方金额
                                }

                                decimal n=BeginAmount + Convert.ToDecimal(row["ThisDebit"].ToString()) - Convert.ToDecimal(row["ThisCredit"].ToString());
                                row["EndAmount"] =Math.Round(n,2).ToString("#,###0.#0");//期末余额
                                row["Direction"] = AcountBookDBHelper.DirectionSource(SubjectsCD, Convert.ToDecimal(row["EndAmount"].ToString()));//余额方向
                                row["SourceCode"] = "";
                                row["Type"] = "";
                                row["SourceDate"] = "";
                                row["TotalAmount"] = "";
                                row["BlendingAmount"] = "";
                                row["Scale"] = "";
                                row["ByOrder"] = "1";
                                dt.Rows.Add(row);
                            }
                        }
                        else
                        {
                            DataRow row = dt.NewRow();
                            row["VoucherDate"] = drr["VoucherDate"].ToString();//日期
                            row["CusOrPro"] = VoucherDBHelper.GetAssistantName(dr["SubjectsDetails"].ToString(), dr["FormTBName"].ToString(), dr["FileName"].ToString());//客户
                            row["Abstract"] = drr["Abstract"].ToString();//摘要
                            row["BeginAmount"] = BeginAmount.ToString("#,###0.#0");//期初金额
                            if (CurryType.LastIndexOf(",") == -1)
                            {
                                row["ThisDebit"] = Math.Round(Convert.ToDecimal(drr["ForeignThisDebit"].ToString()),2).ToString("#,###0.#0");//本期借方金额
                                row["ThisCredit"] = Math.Round(Convert.ToDecimal(drr["ForeignThisCredit"].ToString()),2).ToString("#,###0.#0");//本期贷方金额
                            }
                            else
                            {
                                row["ThisDebit"] = Math.Round(Convert.ToDecimal(drr["ThisDebit"].ToString()),2).ToString("#,###0.#0");//本期借方金额
                                row["ThisCredit"] = Math.Round(Convert.ToDecimal(drr["ThisCredit"].ToString()),2).ToString("#,###0.#0");//本期贷方金额
                            }

                            decimal p = BeginAmount + Convert.ToDecimal(row["ThisDebit"].ToString()) - Convert.ToDecimal(row["ThisCredit"].ToString());
                            row["EndAmount"] = Math.Round(p, 2).ToString("#,###0.#0");//期末余额
                            row["Direction"] = AcountBookDBHelper.DirectionSource(SubjectsCD, Convert.ToDecimal(row["EndAmount"].ToString()));//余额方向
                            row["SourceCode"] = "";
                            row["Type"] = "";
                            row["SourceDate"] = "";
                            row["TotalAmount"] = "";
                            row["BlendingAmount"] = "";
                            row["Scale"] = "";
                            row["ByOrder"] = "1";
                            dt.Rows.Add(row);
                        }

                       
                    }


                    decimal ThisDebitSum = 0;
                    decimal ThisCreditSum = 0;
                    decimal TotalSum = 0;
                    decimal BlendingSum = 0;
                    decimal Amount=0;
                    foreach (DataRow rw in dt.Rows)
                    {
                        if (rw["EndAmount"].ToString().Trim().Length > 0)
                        {
                            ThisCreditSum+=Convert.ToDecimal(rw["ThisCredit"].ToString());
                            ThisDebitSum+=Convert.ToDecimal(rw["ThisDebit"].ToString());
                            Amount=BeginAmount + ThisDebitSum - ThisCreditSum;
                            rw["EndAmount"] = Math.Abs(Amount);
                            rw["Direction"] = AcountBookDBHelper.DirectionSource(SubjectsCD, Amount);//余额方向
                        }
                        if (rw["SourceCode"].ToString().Trim().Equals("合计"))
                        {
                            TotalSum += Convert.ToDecimal(rw["TotalAmount"].ToString());
                            BlendingSum += Convert.ToDecimal(rw["BlendingAmount"].ToString());
                        }
                    }

                    DataRow rp = dt.NewRow();

                    rp["VoucherDate"] = "";//日期
                    rp["CusOrPro"] = "";//客户
                    rp["Abstract"] = "本月合计";//摘要
                    rp["BeginAmount"] = "";//期初金额
                    rp["ThisDebit"] = ThisDebitSum.ToString("#,###0.#0");//本期借方金额
                    rp["ThisCredit"] = ThisCreditSum.ToString("#,###0.#0");//本期贷方金额
                    rp["EndAmount"] = Amount.ToString("#,###0.#0");//期末余额
                    rp["Direction"] = "";//余额方向
                    rp["SourceCode"] = "";
                    rp["Type"] = "";
                    rp["SourceDate"] = "";
                    rp["TotalAmount"] = TotalSum.ToString("#,###0.#0");
                    rp["BlendingAmount"] = BlendingSum.ToString("#,###0.#0");
                    rp["ByOrder"] = "1";
                    if (TotalSum == 0)
                    {
                        rp["Scale"] = "";
                    }
                    else
                    {
                        rp["Scale"] = Convert.ToString(Math.Round((BlendingSum / TotalSum) * 100, 2)) + "%";
                    }
                    
                    dt.Rows.Add(rp);



                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 客户账龄分析表读取视图
        /// <summary>
        /// 客户账龄分析表读取视图
        /// </summary>
        /// <param name="CustID">客户主键</param>
        /// <param name="CurryTypeID">币种</param>
        /// <returns></returns>
        public static DataTable GetCusSalesAging(string CustID, string CurryTypeID)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                return FinanceReportDBHelper.GetCusSalesAging(CustID, CurryTypeID, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 客户账龄分析表数据源
        /// <summary>
        /// 客户账龄分析表数据源
        /// </summary>
        /// <param name="CustID">客户主键</param>
        /// <param name="CurryTypeID">币种</param>
        /// <param name="EndDate">截至日期</param>
        /// <returns></returns>
        public static DataTable GetCustSaleAgingReport(string CustID, string CurryTypeID, string EndDate)
        {
            try
            {
                /*
                 * 获取不同客户 Start
                 */
                ArrayList MyList = new ArrayList();
                if (CustID.Trim().Length > 0)
                    MyList.Add(CustID);
                else
                {
                    DataTable CustDT = GetDistinctCustFromView(CurryTypeID);
                    foreach (DataRow dr in CustDT.Rows)
                    {
                        MyList.Add(dr["CustID"].ToString());
                    }
                }
                /*
                 * 获取不同客户 End
                 */
                DataTable SourceDT = new DataTable();
                SourceDT.Columns.Add("CustNo");//客户编码
                SourceDT.Columns.Add("CustName");//客户名称
                SourceDT.Columns.Add("SellType");//源单类别
                SourceDT.Columns.Add("OrderNo");//订单编码
                SourceDT.Columns.Add("ResultDate");//订单日期
                SourceDT.Columns.Add("MaxCreditDate");//结账期
                SourceDT.Columns.Add("CurrExg");//币种(汇率)
                SourceDT.Columns.Add("RealTotal");//源单总金额
                SourceDT.Columns.Add("YAccounts");//已结金额
                SourceDT.Columns.Add("NAccounts");//未结金额
                SourceDT.Columns.Add("ForeignRealTotal");//外币总金额
                SourceDT.Columns.Add("ForeignYAccounts");//外币已结金额
                SourceDT.Columns.Add("ForeignNAccounts");//外币未结金额
                SourceDT.Columns.Add("SaleAging");//账龄
                SourceDT.Columns.Add("ExtendedCount");//超期天数
                SourceDT.Columns.Add("Status");//状态：平|余|欠
                SourceDT.Columns.Add("ByOrder");//排序
                for (int i = 0; i < MyList.Count; i++)
                {
                    DataTable dt = GetCusSalesAging(MyList[i].ToString(), CurryTypeID);
                    decimal SumRealTotal = 0;
                    decimal SumYAccounts = 0;
                    decimal SumNAccounts = 0;
                    decimal SumForeignRealTotal = 0;
                    decimal SumForeignYAccounts = 0;
                    decimal SumForeignNAccounts = 0;
                    string EndResultDate = string.Empty;
                    int j = 0;
                    int MaxCreditDate=0;
                    foreach (DataRow dr in dt.Rows)
                    {

                        DataRow row = SourceDT.NewRow();
                        row["CustNo"] = dr["CustNo"].ToString();
                        row["CustName"] = dr["CustName"].ToString();
                        row["SellType"] = dr["SellTypeName"].ToString();
                        row["OrderNo"] = dr["OrderNo"].ToString();
                        row["ResultDate"] = dr["ResultDate"].ToString();
                        row["MaxCreditDate"] = dr["MaxCreditDate"].ToString();
                        row["ByOrder"] = "1";
                        MaxCreditDate=int.Parse(row["MaxCreditDate"].ToString());
                        if (CurryTypeID.LastIndexOf(",") == -1)
                        {
                            row["CurrExg"] = dr["CurrencyName"].ToString() + "(" + Math.Round(Convert.ToDecimal(dr["Rate"].ToString() == "" ? "0" : dr["Rate"].ToString()), 2).ToString() + ")";
                           
                        }
                        else
                        {
                            row["CurrExg"] = "综合本位币";
                        }
                        if (dr["SellType"].ToString() == "3")
                        {
                            row["ForeignRealTotal"] = Convert.ToDecimal(-Convert.ToDecimal(dr["RealTotal"].ToString())).ToString("#,###0.#0");
                            row["ForeignYAccounts"] = Convert.ToDecimal(-Convert.ToDecimal(dr["YAccounts"].ToString())).ToString("#,###0.#0");
                            row["ForeignNAccounts"] = Convert.ToDecimal(-Convert.ToDecimal(dr["NAccounts"].ToString())).ToString("#,###0.#0");


                            row["RealTotal"] =  Convert.ToDecimal(-Convert.ToDecimal(dr["RealTotal"].ToString()) * Convert.ToDecimal(dr["Rate"].ToString())).ToString("#,###0.#0");
                            row["YAccounts"] =  Convert.ToDecimal(-Convert.ToDecimal(dr["YAccounts"].ToString()) * Convert.ToDecimal(dr["Rate"].ToString())).ToString("#,###0.#0");
                            row["NAccounts"] =  Convert.ToDecimal(-Convert.ToDecimal(dr["NAccounts"].ToString()) * Convert.ToDecimal(dr["Rate"].ToString())).ToString("#,###0.#0");
                            row["SaleAging"] = "";
                            row["ExtendedCount"] = "";
                            row["Status"] = "";
                        }
                        else
                        {
                            row["ForeignRealTotal"] = Convert.ToDecimal(dr["RealTotal"].ToString()).ToString("#,###0.#0");
                            row["ForeignYAccounts"] = Convert.ToDecimal(dr["YAccounts"].ToString()).ToString("#,###0.#0");
                            row["ForeignNAccounts"] = Convert.ToDecimal(dr["NAccounts"].ToString()).ToString("#,###0.#0");


                            row["RealTotal"] = Convert.ToDecimal(Convert.ToDecimal(dr["RealTotal"].ToString()) * Convert.ToDecimal(dr["Rate"].ToString())).ToString("#,###0.#0");
                            row["YAccounts"] =Convert.ToDecimal(Convert.ToDecimal(dr["YAccounts"].ToString()) * Convert.ToDecimal(dr["Rate"].ToString())).ToString("#,###0.#0");
                            row["NAccounts"] = Convert.ToDecimal(Convert.ToDecimal(dr["NAccounts"].ToString()) * Convert.ToDecimal(dr["Rate"].ToString())).ToString("#,###0.#0");
                            if (Convert.ToDecimal(row["NAccounts"].ToString()) > 0)
                            {
                                TimeSpan span = Convert.ToDateTime(EndDate).Subtract(Convert.ToDateTime(dr["ResultDate"].ToString()));
                                row["SaleAging"] = span.Days;
                                row["ExtendedCount"] = span.Days - int.Parse(row["MaxCreditDate"].ToString());
                                row["Status"] = "欠";
                                if (j == 0)
                                {
                                    EndResultDate = dr["ResultDate"].ToString();
                                }
                                j++;
                            }
                            else if (Convert.ToDecimal(row["NAccounts"].ToString()) < 0)
                            {
                                row["SaleAging"] = "";
                                row["ExtendedCount"] = "";
                                row["Status"] = "余";
                            }
                            else
                            {
                                row["SaleAging"] = "";
                                row["ExtendedCount"] = "";
                                row["Status"] = "平";
                            }
                        }

                        SumRealTotal += Convert.ToDecimal(row["RealTotal"].ToString());
                        SumYAccounts += Convert.ToDecimal(row["YAccounts"].ToString());
                        SumNAccounts += Convert.ToDecimal(row["NAccounts"].ToString());
                        SumForeignRealTotal += Convert.ToDecimal(row["ForeignRealTotal"].ToString());
                        SumForeignYAccounts += Convert.ToDecimal(row["ForeignYAccounts"].ToString());
                        SumForeignNAccounts += Convert.ToDecimal(row["ForeignNAccounts"].ToString());
                        SourceDT.Rows.Add(row);

                    }

                    DataRow roww = SourceDT.NewRow();
                    roww["CustNo"] = "";
                    roww["CustName"] = "";
                    roww["SellType"] = "";
                    roww["OrderNo"] = "";
                    roww["ByOrder"] = "1";
                    roww["ResultDate"] ="";
                    roww["MaxCreditDate"] = "";
                    roww["CurrExg"] = "合计";
                    roww["ForeignRealTotal"] = SumForeignRealTotal.ToString("#,###0.#0");
                    roww["ForeignYAccounts"] = SumForeignYAccounts.ToString("#,###0.#0");
                    roww["ForeignNAccounts"] = SumForeignNAccounts.ToString("#,###0.#0");
                    roww["RealTotal"] = SumRealTotal.ToString("#,###0.#0");
                    roww["YAccounts"] = SumYAccounts.ToString("#,###0.#0");
                    roww["NAccounts"] = SumNAccounts.ToString("#,###0.#0");
                    if (SumNAccounts > 0)
                    {
                        TimeSpan spann = Convert.ToDateTime(EndDate).Subtract(Convert.ToDateTime(EndResultDate));
                        roww["SaleAging"] = spann.Days;
                        roww["ExtendedCount"] = spann.Days - MaxCreditDate;
                        roww["Status"] = "欠";
                    }
                    else if (SumNAccounts < 0)
                    {
                        roww["SaleAging"] = "";
                        roww["ExtendedCount"] = "";
                        roww["Status"] = "余";
                    }
                    else
                    {
                        roww["SaleAging"] = "";
                        roww["ExtendedCount"] = "";
                        roww["Status"] = "平";
                    }

                    SourceDT.Rows.Add(roww);


                }
                return SourceDT;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 订单类型转换
        /// <summary>
        /// 订单类型转换
        /// </summary>
        /// <param name="type">订单类型</param>
        /// <returns></returns>
        public static string GetBillType(string type)
        {
            string rev = string.Empty;
            switch (type)
            {
                case "1":
                    rev = "销售订单";
                    break;
                case "2":
                    rev = "采购订单";
                    break;
                case "3":
                    rev = "销售退货单";
                    break;
                case "4":
                    rev = "代销结算单";
                    break;
                case "5":
                    rev = "采购退货单";
                    break;
                default:
                    break;
            }
            return rev;
        }
        #endregion


        #region 获取客户账龄视图不同客户
        /// <summary>
        /// 获取客户账龄视图不同客户
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetDistinctCustFromView(string CurryTypeID)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                return FinanceReportDBHelper.GetDistinctCustFromView(CompanyCD,CurryTypeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

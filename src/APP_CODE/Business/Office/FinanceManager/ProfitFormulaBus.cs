/**********************************************
 * 描述：     利润表计算公式明细业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/05/25
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;
using System.Collections;
namespace XBase.Business.Office.FinanceManager
{
    public  class ProfitFormulaBus
    {
        #region singleton
        private static ProfitFormulaBus _Instance=null;
        public static ProfitFormulaBus GetInstance()
        {
            if (_Instance == null)
            {
                lock (typeof(ProfitFormulaBus))
                {
                    if (_Instance == null)
                    {
                        _Instance = new ProfitFormulaBus();
                    }
                }
            }
            return _Instance;
        }
        #endregion

        #region
        public DataTable GetProfitFormulaDetails(int FormulaID)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return ProfitFormulaDBHelper.GetInstance().GetProfitFormulaDetails(CompanyCD,FormulaID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取利润表项目信息
        public static DataTable GetProfitFormulaInfo()
        {
            try
            {
                return ProfitFormulaDBHelper.GetInstance().GetProfitFormulaInfo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 修改公式明细信息
        public bool UpdateProfitFormulaDetails(string ID,
            string SubjectsCD, string SubjectsName, string Direction, string Operator)
        {
            try
            {
                return ProfitFormulaDBHelper.GetInstance().UpdateProfitFormulaDetails(ID,
                    SubjectsCD,SubjectsName,Direction,Operator);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       #endregion

         #region 删除利润表明细信息
        public bool DeleteProfitFormulaInfo(string ID)
        {
            try
            {
                return ProfitFormulaDBHelper.GetInstance().DeleteProfitFormulaInfo(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 添加利润公式明细信息
        public bool InsertProfitFormulaDetails(int ForumalID,string SubjectsCD,
            string SubjectsName,string Dirt,string Operator)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return ProfitFormulaDBHelper.GetInstance().InsertProfitFormulaDetails(CompanyCD, ForumalID, SubjectsCD, SubjectsName,
                  Dirt, Operator);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取利润表计算信息
        public DataTable GetProfitProcessInfo(string DuringDate,string TypeID)
        {
            DataTable Profitdt = null; //定义利润表对象
          
            #region 变量的定义
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
            string CurrentstartDate = string.Empty;//本期开始时间
            string CurrentendDate = string.Empty;//本期结束时间
            string BeforestartDate = string.Empty;//上期开始时间
            string BeforeendDate = string.Empty;//上期结束时间
            string CurrencyTypeID = string.Empty;//币种ID
            string DayEnd = string.Empty;//月末数
            string _DayEnd = string.Empty;//月末数
            decimal CurrBusinessProfit = 0;//本年营业利润
            decimal BeforeBusinessProfit = 0;//上一年营业利润
            #endregion

            try
            {
                if (TypeID == "0")//显示本年累计数、上年累计数
                {
                    //对输入的资料日期进行处理开始
                    int MothString = Convert.ToInt32(DuringDate.Split('-')[1].ToString());
                    int yearString = Convert.ToInt32(DuringDate.Split('-')[0].ToString());
                    int Year = Convert.ToInt32(DuringDate.Split('-')[0].ToString());
                    Year = Year - 1;
                    if (MothString == 4 || MothString == 6 || MothString == 9 || MothString == 11)
                    {
                        DayEnd = "-30";
                       _DayEnd = "-30";
                    }
                    else if (MothString == 2)
                    {
                        if (((yearString % 4 == 0 && yearString % 100 == 0)) || (yearString % 400 == 0))
                        {
                            DayEnd = "-29";
                        }
                        else
                        {
                            DayEnd = "-28";
                        }

                        if (((Year % 4 == 0 && Year % 100 == 0)) || (Year % 400 == 0))
                        {
                            _DayEnd = "-29";
                        }
                        else
                        {
                            _DayEnd = "-28";
                        }
                    }
                    else
                    {
                        DayEnd = "-31";
                        _DayEnd = "-31";
                    }
                    CurrentstartDate = yearString.ToString() + "-01-01";
                    CurrentendDate = DuringDate + DayEnd;
                    BeforestartDate = Year.ToString() + "-01-01";
                    if (MothString.ToString().Length == 1)
                    {
                        BeforeendDate = Year.ToString() + "-" + "0" + MothString.ToString() + _DayEnd;
                    }
                    else
                    {
                        BeforeendDate = Year.ToString() + "-"+ MothString.ToString() + _DayEnd;
                    }
                    //获取币种表币种ID
                    DataTable Currencydt = CurrTypeSettingDBHelper.GetCurrTypeByCompanyCD(CompanyCD);
                    if (Currencydt != null && Currencydt.Rows.Count > 0)
                    {
                        foreach (DataRow rows in Currencydt.Rows)
                        {
                            CurrencyTypeID += rows["ID"].ToString() + ",";
                        }
                        CurrencyTypeID = CurrencyTypeID.TrimEnd(new char[] { ',' });//所有币种ID集
                    }
                    //获取公式项目信息
                    DataTable ForumlaItemDt = ProfitFormulaDBHelper.GetInstance().GetProfitFormulaInfo();

                    /*
                    * Add by Moshenlin 去除统计期末处理损益结转生成的凭证登帐的账簿信息
                    */
                    string CurrNotIDS = VoucherDBHelper.GetProfitandLossAttestIDS(CurrentstartDate, CurrentendDate, CompanyCD);
                    string BeforeNotIDS = VoucherDBHelper.GetProfitandLossAttestIDS(BeforestartDate, BeforeendDate, CompanyCD);

                    string CQueryStr = string.Empty;
                    string BQueryStr = string.Empty;

                    if (CurrNotIDS.Trim().Length > 0)
                    {
                        CQueryStr = " and AttestBillID not in ( " + CurrNotIDS + " ) ";
                    }
                    if (BeforeNotIDS.Trim().Length > 0)
                    {
                        BQueryStr = " and AttestBillID not in ( " + BeforeNotIDS + " ) ";
                    }

                    //执行本期数SQL语句
                    string CurrSql = " VoucherDate>='" + CurrentstartDate + "' and VoucherDate<='" + CurrentendDate + "'  and CompanyCD='" + CompanyCD + "' " + CQueryStr + " ";
                    //执行上期数SQL语句
                     string BeforeSql = " VoucherDate>='" + BeforestartDate + "' and VoucherDate<='" + BeforeendDate + "'  and CompanyCD='" + CompanyCD + "' " + BQueryStr + " ";
                    if (ForumlaItemDt != null && ForumlaItemDt.Rows.Count > 0)
                    {
                        Profitdt = new DataTable();//构造利润表对象
       
                        Profitdt.Columns.Add("itemName");
                        Profitdt.Columns.Add("line"); 
                        Profitdt.Columns.Add("currMoney");
                        Profitdt.Columns.Add("agoMoney");
                        foreach (DataRow rows in ForumlaItemDt.Rows)
                        {
                            DataRow Newrows = Profitdt.NewRow();
                            Newrows["itemName"] = rows["Name"].ToString();//项目名称
                            Newrows["line"] = rows["Line"].ToString();//行号
                            if (Convert.ToInt32(rows["Line"]) != 10 && Convert.ToInt32(rows["Line"]) != 13 &&
                               Convert.ToInt32(rows["Line"]) != 15)
                            {
                                //获取本期数
                                Newrows["currMoney"] = Math.Round(GetAmount(Convert.ToInt32(rows["ID"]), CurrSql, rows["Line"].ToString(), CurrencyTypeID), 2).ToString("#,###0.#0");
                                //获取上期数
                                Newrows["agoMoney"] = Math.Round(GetAmount(Convert.ToInt32(rows["ID"]), BeforeSql, rows["Line"].ToString(), CurrencyTypeID), 2).ToString("#,###0.#0");
                            }
                            //营业利润
                            if (Convert.ToInt32(rows["Line"]) == 10)
                            {
                                if (Profitdt != null && Profitdt.Rows.Count > 0)
                                {
                                    for (int i = 0; i < Profitdt.Rows.Count; i++)
                                    {
                                        if (i == 0)
                                        {
                                            //获取当年营业利润
                                            CurrBusinessProfit = Convert.ToDecimal(Profitdt.Rows[i]["currMoney"]);
                                            //获取上一年营业利润
                                            BeforeBusinessProfit = Convert.ToDecimal(Profitdt.Rows[i]["agoMoney"]);
                                        }
                                        else
                                        {
                                            if (i <= 6)
                                            {
                                                //减：当年营业成本
                                                CurrBusinessProfit = CurrBusinessProfit - Convert.ToDecimal(Profitdt.Rows[i]["currMoney"]);
                                                //减：上一年营业成本
                                                BeforeBusinessProfit = BeforeBusinessProfit - Convert.ToDecimal(Profitdt.Rows[i]["agoMoney"]);
                                            }
                                            else
                                            {
                                                if (i == 7)
                                                {
                                                    //加：当年 投资收益
                                                    CurrBusinessProfit = CurrBusinessProfit + Convert.ToDecimal(Profitdt.Rows[i]["currMoney"]);

                                                    //加：当年 投资收益
                                                    BeforeBusinessProfit = BeforeBusinessProfit + Convert.ToDecimal(Profitdt.Rows[i]["agoMoney"]);
                                                }
                                                if (i == 8)
                                                {
                                                    //加： 投资收益
                                                    CurrBusinessProfit = CurrBusinessProfit + Convert.ToDecimal(Profitdt.Rows[i]["currMoney"]);
                                                    //加：上一年 投资收益
                                                    BeforeBusinessProfit = BeforeBusinessProfit + Convert.ToDecimal(Profitdt.Rows[i]["agoMoney"]);
                                                }
                                            }
                                        }
                                    }
                                    //当年本营业利润
                                    Newrows["currMoney"] = Math.Round(CurrBusinessProfit, 2).ToString("#,###0.#0");
                                    //上一年本营业利润
                                    Newrows["agoMoney"] = Math.Round(BeforeBusinessProfit, 2).ToString("#,###0.#0");
                                }

                            }//利润总额
                            else  if (Convert.ToInt32(rows["Line"]) == 13)
                            {
                                if (Profitdt != null && Profitdt.Rows.Count > 0)
                                {
                                    //当年利润总额
                                    Newrows["currMoney"] = Math.Round(Convert.ToDecimal(Profitdt.Rows[9]["currMoney"]) + Convert.ToDecimal(Profitdt.Rows[10]["currMoney"]) - Convert.ToDecimal(Profitdt.Rows[11]["currMoney"]), 2).ToString("#,###0.#0");
                                    //上一年利润总额
                                    Newrows["agoMoney"] = Math.Round(Convert.ToDecimal(Profitdt.Rows[9]["agoMoney"]) + Convert.ToDecimal(Profitdt.Rows[10]["agoMoney"]) - Convert.ToDecimal(Profitdt.Rows[11]["agoMoney"]), 2).ToString("#,###0.#0");
                                }
                            }//净利润
                            else if (Convert.ToInt32(rows["Line"]) == 15)
                            {
                                //当年净利润
                                Newrows["currMoney"] = Math.Round(Convert.ToDecimal(Profitdt.Rows[12]["currMoney"]) - Convert.ToDecimal(Profitdt.Rows[13]["currMoney"]), 2).ToString("#,###0.#0");
                                //上一年净利润
                                Newrows["agoMoney"] = Math.Round(Convert.ToDecimal(Profitdt.Rows[12]["agoMoney"]) - Convert.ToDecimal(Profitdt.Rows[13]["agoMoney"]), 2).ToString("#,###0.#0");
                            }
                            Profitdt.Rows.Add(Newrows);
                        }
                    }
                }
                else if (TypeID == "1")//显示本期数、本年累计数   
                {
                    //对输入的资料日期进行处理开始
                    int MothString = Convert.ToInt32(DuringDate.Split('-')[1].ToString());
                    int yearString = Convert.ToInt32(DuringDate.Split('-')[0].ToString());
               
                    if (MothString == 4 || MothString == 6 || MothString == 9 || MothString == 11)
                    {
                        DayEnd = "-30";
                    }
                    else if (MothString == 2)
                    {
                        if (((yearString % 4 == 0 && yearString % 100 == 0)) || (yearString % 400 == 0))
                        {
                            DayEnd = "-29";
                        }
                        else
                        {
                            DayEnd = "-28";
                        }
                    }
                    else
                    {
                        DayEnd = "-31";
                    }
                    if (MothString.ToString().Length == 1)
                    {
                        CurrentstartDate = yearString.ToString() + "-0" + MothString + "-01";
                        CurrentendDate = yearString.ToString() + "-0" + MothString + DayEnd;
                        BeforeendDate = yearString.ToString() + "-0" + MothString + DayEnd;
                    }
                    else
                    {
                        CurrentstartDate = yearString.ToString()+ MothString + "-01";
                        CurrentendDate = yearString.ToString()+ MothString + DayEnd;
                        BeforeendDate = yearString.ToString()+ MothString + DayEnd;
                    }
                    BeforestartDate = yearString.ToString() + "-01-01";
                    //获取币种表币种ID
                    DataTable Currencydt = CurrTypeSettingDBHelper.GetCurrTypeByCompanyCD(CompanyCD);
                    if (Currencydt != null && Currencydt.Rows.Count > 0)
                    {
                        foreach (DataRow rows in Currencydt.Rows)
                        {
                            CurrencyTypeID += rows["ID"].ToString() + ",";
                        }
                        CurrencyTypeID = CurrencyTypeID.TrimEnd(new char[] { ',' });//所有币种ID集
                    }
                    //获取公式项目信息
                    DataTable ForumlaItemDt = ProfitFormulaDBHelper.GetInstance().GetProfitFormulaInfo();

                    /*
                     * Add by Moshenlin 去除统计期末处理损益结转生成的凭证登帐的账簿信息
                     */
                    string CurrNotIDS = VoucherDBHelper.GetProfitandLossAttestIDS(CurrentstartDate, CurrentendDate, CompanyCD);
                    string BeforeNotIDS = VoucherDBHelper.GetProfitandLossAttestIDS(BeforestartDate, BeforeendDate, CompanyCD);

                    string CQueryStr = string.Empty;
                    string BQueryStr = string.Empty;

                    if (CurrNotIDS.Trim().Length > 0)
                    {
                        CQueryStr = " and AttestBillID not in ( "+CurrNotIDS+" ) ";
                    }
                    if (BeforeNotIDS.Trim().Length > 0)
                    {
                        BQueryStr = " and AttestBillID not in ( " + BeforeNotIDS + " ) ";
                    }
                    //执行本期数SQL语句
                    string CurrSql = " VoucherDate>='" + CurrentstartDate + "' and VoucherDate<='" + CurrentendDate + "'  and CompanyCD='" + CompanyCD + "' " + CQueryStr + " ";
                    //执行本年累计数SQL语句
                    string BeforeSql = " VoucherDate>='" + BeforestartDate + "' and VoucherDate<='" + BeforeendDate + "'  and CompanyCD='" + CompanyCD + "' " + BQueryStr + " ";
                    if (ForumlaItemDt != null && ForumlaItemDt.Rows.Count > 0)
                    {
                        Profitdt = new DataTable();//构造利润表对象

                        Profitdt.Columns.Add("itemName");
                        Profitdt.Columns.Add("line");
                        Profitdt.Columns.Add("currMoney");
                        Profitdt.Columns.Add("agoMoney");
                        foreach (DataRow rows in ForumlaItemDt.Rows)
                        {
                            DataRow Newrows = Profitdt.NewRow();
                            Newrows["itemName"] = rows["Name"].ToString();//项目名称
                            Newrows["line"] = rows["Line"].ToString();//行号
                            if (Convert.ToInt32(rows["Line"]) != 10 && Convert.ToInt32(rows["Line"]) != 13 &&
                               Convert.ToInt32(rows["Line"]) != 15)
                            {
                                //获取本期数
                                Newrows["currMoney"] = Math.Round(GetAmount(Convert.ToInt32(rows["ID"]), CurrSql, rows["Line"].ToString(), CurrencyTypeID), 2).ToString("#,###0.#0");
                                //获取上期数
                                Newrows["agoMoney"] = Math.Round(GetAmount(Convert.ToInt32(rows["ID"]), BeforeSql, rows["Line"].ToString(), CurrencyTypeID), 2).ToString("#,###0.#0");
                            }
                            //营业利润
                            if (Convert.ToInt32(rows["Line"]) == 10)
                            {
                                if (Profitdt != null && Profitdt.Rows.Count > 0)
                                {
                                    for (int i = 0; i < Profitdt.Rows.Count; i++)
                                    {
                                        if (i == 0)
                                        {
                                            //获取当年营业利润
                                            CurrBusinessProfit = Convert.ToDecimal(Profitdt.Rows[i]["currMoney"]);
                                            //获取上一年营业利润
                                            BeforeBusinessProfit = Convert.ToDecimal(Profitdt.Rows[i]["agoMoney"]);
                                        }
                                        else
                                        {
                                            if (i <= 6)
                                            {
                                                //减：当年营业成本
                                                CurrBusinessProfit = CurrBusinessProfit - Convert.ToDecimal(Profitdt.Rows[i]["currMoney"]);
                                                //减：上一年营业成本
                                                BeforeBusinessProfit = BeforeBusinessProfit - Convert.ToDecimal(Profitdt.Rows[i]["agoMoney"]);
                                            }
                                            else
                                            {
                                                if (i == 7)
                                                {
                                                    //加：当年 投资收益
                                                    CurrBusinessProfit = CurrBusinessProfit + Convert.ToDecimal(Profitdt.Rows[i]["currMoney"]);

                                                    //加：当年 投资收益
                                                    BeforeBusinessProfit = BeforeBusinessProfit + Convert.ToDecimal(Profitdt.Rows[i]["agoMoney"]);
                                                }
                                                if (i == 8)
                                                {
                                                    //加： 投资收益
                                                    CurrBusinessProfit = CurrBusinessProfit + Convert.ToDecimal(Profitdt.Rows[i]["currMoney"]);
                                                    //加：上一年 投资收益
                                                    BeforeBusinessProfit = BeforeBusinessProfit + Convert.ToDecimal(Profitdt.Rows[i]["agoMoney"]);
                                                }
                                            }
                                        }
                                    }
                                    //当年本营业利润
                                    Newrows["currMoney"] = Math.Round(CurrBusinessProfit, 2).ToString("#,###0.#0");
                                    //上一年本营业利润
                                    Newrows["agoMoney"] = Math.Round(BeforeBusinessProfit, 2).ToString("#,###0.#0");
                                }

                            }//利润总额
                            else if (Convert.ToInt32(rows["Line"]) == 13)
                            {
                                if (Profitdt != null && Profitdt.Rows.Count > 0)
                                {
                                    //当年利润总额
                                    Newrows["currMoney"] = Math.Round(Convert.ToDecimal(Profitdt.Rows[9]["currMoney"]) + Convert.ToDecimal(Profitdt.Rows[10]["currMoney"]) - Convert.ToDecimal(Profitdt.Rows[11]["currMoney"]), 2).ToString("#,###0.#0");
                                    //上一年利润总额
                                    Newrows["agoMoney"] = Math.Round(Convert.ToDecimal(Profitdt.Rows[9]["agoMoney"]) + Convert.ToDecimal(Profitdt.Rows[10]["agoMoney"]) - Convert.ToDecimal(Profitdt.Rows[11]["agoMoney"]), 2).ToString("#,###0.#0");
                                }
                            }//净利润
                            else if (Convert.ToInt32(rows["Line"]) == 15)
                            {
                                //当年净利润
                                Newrows["currMoney"] = Math.Round(Convert.ToDecimal(Profitdt.Rows[12]["currMoney"]) - Convert.ToDecimal(Profitdt.Rows[13]["currMoney"]), 2).ToString("#,###0.#0");
                                //上一年净利润
                                Newrows["agoMoney"] = Math.Round(Convert.ToDecimal(Profitdt.Rows[12]["agoMoney"]) - Convert.ToDecimal(Profitdt.Rows[13]["agoMoney"]), 2).ToString("#,###0.#0");
                            }
                            Profitdt.Rows.Add(Newrows);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Profitdt;
        }
        #endregion

        #region  获取项目本期数金额
        private decimal GetAmount(int FormulaID, string sql, string Line, string CurrencyTypeID)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
            if (Convert.ToInt32(Line) == 10 || Convert.ToInt32(Line) == 13
                || Convert.ToInt32(Line) == 15) return 0;
            decimal YInitAmount = 0;//年初始值
            decimal CurrentAmount = 0;//本期数
            decimal TotalAmount = 0;//总金额
            decimal nev = 0;
            try
            {
                DataTable dt = ProfitFormulaDBHelper.GetInstance().GetProfitFormulaDetails(CompanyCD, FormulaID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow rows in dt.Rows)
                    {
                        YInitAmount = AcountBookDBHelper.GetBeginCurryTypeAmount(rows["SubjectsCD"].ToString(), CurrencyTypeID,CompanyCD);
                        CurrentAmount = VoucherDBHelper.GetBeginAmount(sql,rows["SubjectsCD"].ToString(), rows["Direction"].ToString());
                        TotalAmount = YInitAmount + CurrentAmount;
                        if (rows["Operator"].ToString().Trim()== "+")
                        {
                            nev = nev + TotalAmount;
                        }
                        else if (rows["Operator"].ToString().Trim() == "-")
                        {
                            nev = nev - TotalAmount;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return nev;
        }
        #endregion
    }
}

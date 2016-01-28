/**********************************************
 * 描述：     期末项目处理业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/05/11
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Collections;
using System.Data;
using XBase.Common;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
namespace XBase.Business.Office.FinanceManager
{
    public class EndItemProcessingBus
    {
        #region 常量定义
        const string SUBJECT_CD_DNLY_NAME = "本年利润";
        const string SUBJECT_CD_DNLY_CODE = "4103";

        const string SUBJECT_CD_KCXJ_NAME = "库存现金";
        const string SUBJECT_CD_KCXJ_CODE = "1001";

        const string SUBJECT_CD_HDSY_CODE = "6603001";
        const string SUBJECT_CD_HDSY_NAME = "汇兑损益";




        const string DIRECTION_J = "0"; //借方向
        const string DIRECTION_D = "1";//贷方向

        #endregion

        #region 结转到下一年
        public static bool JZtoNextYear()
        {
            //bool result = false;
            //获取当前企业代码
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                //获取结转下一年的期末余额的记录ID
                DataTable dtid = EndItemProcessing.GetJZNextYearRecordId(CompanyCD);
                //登帐凭证期末余额ID
                string AccountID = string.Empty;
                //实例存放科目实体的数组
                ArrayList modelList = new ArrayList();

                if (dtid != null && dtid.Rows.Count > 0)
                {
                    foreach (DataRow rows in dtid.Rows)
                    {
                        AccountID += rows["ID"].ToString() + ",";
                    }
                    AccountID = AccountID.Remove(AccountID.Length - 1);
                    //获取科目年末余额
                    DataTable dtEndAmount = EndItemProcessing.GetJZNextYearEndAmount(AccountID);
                    //定义会计科目实体
                    AccountSubjectsModel model = null;
                    if (dtEndAmount != null && dtEndAmount.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtEndAmount.Rows)
                        {
                            //实例科目实体
                            model = new AccountSubjectsModel();
                            model.SubjectsCD = row["SubjectsCD"].ToString();
                            model.CompanyCD = CompanyCD;
                            model.YInitialValue = Convert.ToDecimal(row["EndAmount"]);
                            modelList.Add(model);
                        }
                    }
                }
                //更新会计科目年初始值
                return EndItemProcessing.UpdateSubjectsYInitialValue(modelList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 期末调汇
        public static bool EndExchangeRate(string[] CurrTypeRate,
            string ItemID, string PeriodNum, ref string AttestNo, ref int AttestID,string DuringDate)
        {
            if (CurrTypeRate == null) return false;
            bool result = false;
            //获取当前企业代码
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                #region 凭证实体
                //实例凭证单据实体对象
                AttestBillModel Attestmodel = new AttestBillModel();
                //凭证明细实体定义
                AttestBillDetailsModel Detailmodel = null;
                #endregion
                //汇总总金额
                decimal SumTotalPrice = 0;
                //登帐凭证ID号
                string EndAccountID = string.Empty;
                //凭证单据ID
                int ID;
                //会计科目代码
                string SubjectsCD = string.Empty;
                //定义数组存储实体对象
                ArrayList modelList = new ArrayList();

                //获取本币币种ID
                int MasterCurrencyID = Convert.ToInt32(CurrTypeSettingDBHelper.GetMasterCurrency(CompanyCD).Rows[0]["ID"]);
                //获取结转汇兑损益摘要ID

                /*修改于20090604 摘要直接存名称 by 莫申林  start*/
                //int AbstractID;
                //AbstractID = Convert.ToInt32(SummarySettingBus.GetSummaryIDByName(ConstUtil.SUMMARY_JZHDSY_NAME));
                string AbstractID;
                AbstractID = ConstUtil.SUMMARY_JZHDSY_NAME;
                /*修改于20090604 摘要直接存名称 by 莫申林  end*/

                //对输入的资料日期进行处理开始
                //开始时间
                string StartDate = string.Empty;
                //结束时间
                string EndDate = string.Empty;
                string DayEnd = string.Empty;//月末数
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
                StartDate = yearString.ToString() + "-" + MothString.ToString() + "-01";
                EndDate = yearString.ToString() + "-" + MothString.ToString() + DayEnd;
                foreach (string array in CurrTypeRate)
                {
                    decimal TempPrice = 0;
                    string[] Currency = array.Split(',');
                    DataTable dt = EndItemProcessing.GetAccountBookEndAmount(CompanyCD, MasterCurrencyID, Convert.ToInt32(Currency[0]),StartDate,EndDate);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            //外币本币期末余额
                            decimal WbEndAmount, BbEndAmount = 0;
                            //读取外币期末余额
                            WbEndAmount = Convert.ToDecimal(rows["wbEndAmount"]);
                            //读取本币期末余额
                            BbEndAmount = Convert.ToDecimal(rows["EndAmount"]);
                            //计算利率后的金额
                            decimal TotalPrice = WbEndAmount * (Convert.ToDecimal(Currency[1])) - BbEndAmount;
                            SumTotalPrice += TotalPrice;
                            TempPrice += TotalPrice;

                            #region 科目凭证明细赋值
                            if (TotalPrice != 0)
                            {
                                Detailmodel = new AttestBillDetailsModel();//实例凭证明细实体对象
                                // Detailmodel.AttestBillID = ID;
                                Detailmodel.Abstract = AbstractID; //摘要ID
                                Detailmodel.SubjectsCD = rows["SubjectsCD"].ToString();//库存现金科目
                                if (TotalPrice > 0)
                                {
                                    Detailmodel.DebitAmount = TotalPrice;//借方金额
                                    Detailmodel.CreditAmount = 0;//贷方金额
                                }
                                else
                                {
                                    Detailmodel.DebitAmount = 0;//借方金额为0
                                    //Modified 2009-06-26
                                    TotalPrice = Convert.ToDecimal(TotalPrice.ToString().Substring(TotalPrice.ToString().IndexOf("-") + 1, TotalPrice.ToString().Length - TotalPrice.ToString().IndexOf("-") - 1));
                                    Detailmodel.CreditAmount = TotalPrice;//贷方金额
                                }
                                Detailmodel.CurrencyTypeID = MasterCurrencyID;
                                //Detailmodel.CurrencyTypeID = Convert.ToInt32(Currency[0]);//币种
                                Detailmodel.OriginalAmount = TotalPrice;//原币金额
                                //Modified 2009-06-26
                                //  Detailmodel.ExchangeRate = Convert.ToDecimal(Currency[1]);//汇率
                                Detailmodel.ExchangeRate = 1;
                                //明细实体对象添加到数组中
                                modelList.Add(Detailmodel);
                            }
                           #endregion
                        }
                        #region 2009-06-07 Modified by jiangym
                        //if (TempPrice != 0)
                        //{
                        //    #region 本年利润凭证明细赋值
                        //    Detailmodel = new AttestBillDetailsModel();//实例本年利润凭证明细实体对象
                        //  //  Detailmodel.AttestBillID = ID;
                        //    Detailmodel.Abstract = AbstractID; //摘要ID
                        //    Detailmodel.SubjectsCD = SUBJECT_CD_KCXJ_CODE;//库存现金科目
                        //    if (TempPrice > 0)
                        //    {
                        //        Detailmodel.DebitAmount = TempPrice;//借方金额
                        //        Detailmodel.CreditAmount = 0;//贷方金额
                        //    }
                        //    else
                        //    {
                        //        Detailmodel.DebitAmount = 0;//借方金额为0
                        //        //Modified 2009-06-26
                        //        TempPrice = Convert.ToDecimal(TempPrice.ToString().Substring(TempPrice.ToString().IndexOf("-") + 1, TempPrice.ToString().Length - TempPrice.ToString().IndexOf("-") - 1));

                        //        Detailmodel.CreditAmount = TempPrice;//贷方金额
                        //    }
                        //    Detailmodel.CurrencyTypeID = MasterCurrencyID;

                        //  //  Detailmodel.CurrencyTypeID = Convert.ToInt32(Currency[0]);//币种
                        //    Detailmodel.OriginalAmount = TempPrice;//原币金额

                        //    //Modified 2009-06-26

                        //    Detailmodel.ExchangeRate = 1;
                        //   // Detailmodel.ExchangeRate = Convert.ToDecimal(Currency[1]);//汇率


                        //    //明细实体对象添加到数组中
                        //    modelList.Add(Detailmodel);
                        //    #endregion
                        //}
                        #endregion
                    }


                }

                #region 生成汇兑损益明细
                if (SumTotalPrice != 0)
                {
                    Detailmodel = new AttestBillDetailsModel();
                    //  Detailmodel.AttestBillID = ID;
                    Detailmodel.Abstract = AbstractID; //摘要ID
                    Detailmodel.SubjectsCD = SUBJECT_CD_HDSY_CODE;//汇兑损益科目
                    if (SumTotalPrice > 0)
                    {
                        Detailmodel.DebitAmount = 0;//借方金额为0
                        Detailmodel.CreditAmount = SumTotalPrice;//贷方金额
                    }
                    else
                    {
                        SumTotalPrice = Convert.ToDecimal(SumTotalPrice.ToString().Substring(SumTotalPrice.ToString().IndexOf("-") + 1, SumTotalPrice.ToString().Length - SumTotalPrice.ToString().IndexOf("-") - 1));
                        Detailmodel.DebitAmount = SumTotalPrice;//借方金额

                        //Modified 2009-06-26
                    
                        Detailmodel.CreditAmount = 0;//贷方金额为0
                    }
                    Detailmodel.CurrencyTypeID = MasterCurrencyID;//本币类别
                    Detailmodel.OriginalAmount = SumTotalPrice;//原币金额
                    Detailmodel.ExchangeRate = 1;//汇率
                    //明细实体对象添加到数组中
                    modelList.Add(Detailmodel);
                }
                #endregion
                //判断是否有凭证明细
                if (modelList.Count > 0)
                {
                    #region 凭证单据实体赋值
                    int PeriodRecordID=0;

                    result = EndItemProcessing.InsertPeriodProced(CompanyCD, ItemID,PeriodNum,"1",out   PeriodRecordID);
                    if (result)
                    {
                        Attestmodel.CompanyCD = CompanyCD;
                        //获取生成的凭证号
                        Attestmodel.AttestNo = "记-" + VoucherDBHelper.GetMaxAttestNo(CompanyCD, DateTime.Now.ToString());
                        AttestNo = Attestmodel.AttestNo;
                        Attestmodel.VoucherDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                        Attestmodel.AttestName = "记账凭证";
                        Attestmodel.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                        Attestmodel.status = 0;
                        Attestmodel.FromTbale = "officedba.EndItemProcessedRecord";
                        if (PeriodRecordID > 0)
                        {
                            Attestmodel.FromValue = PeriodRecordID.ToString();
                        }
                        else
                        {
                            Attestmodel.FromValue = "";
                        }
                    }
                    //添加凭证单据信息
                    result = EndItemProcessing.BuildAttestBill(Attestmodel, out ID);
                    AttestID = ID;
                    if (result)
                    {
                        //遍历明细实体给单据编号赋值
                        for (int j = 0; j < modelList.Count; j++)
                        {
                            (modelList[j] as AttestBillDetailsModel).AttestBillID = ID;
                        }
                    }
                    #endregion
                }

                result = EndItemProcessing.BuildEndRatechangeDetailInfo(modelList);
                if (modelList.Count > 0)
                {
                    if (result)
                    {
                        VoucherBus.SetStatus(AttestID.ToString(), "1", "status", 0);
                        VoucherBus.InsertAccount(AttestID);
                    }
                }
                //凭证自动登帐及自动审核
           
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询当期项目是否进行期末处理
        public static bool CheckCurrentPeriodIsProced(string PeriodNum, int ItemID)
        {

            try
            {
                return EndItemProcessing.CheckCurrentPeriodIsProced(PeriodNum, ItemID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 损益科目期末结转
        public static bool ProfitandLossChange(string ItemID, string PeriodNum,
            ref string getAttestNo, ref int AttestID, string DuringDate)
        {
            bool result = false;
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                #region 凭证实体
                AttestBillModel Attestmodel = new AttestBillModel(); //实例凭证单据实体对象
                AttestBillDetailsModel Detailmodel = null;//凭证明细实体定义
                #endregion
                #region 凭证单据实体赋值
                Attestmodel.CompanyCD = CompanyCD;
                Attestmodel.AttestNo = "记-" + VoucherDBHelper.GetMaxAttestNo(CompanyCD, DateTime.Now.ToString());
                getAttestNo = Attestmodel.AttestNo;
                Attestmodel.AttestName = "记账凭证";
                Attestmodel.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                Attestmodel.status = 0;
                Attestmodel.FromTbale = "officedba.EndItemProcessedRecord";
             //   Attestmodel.FromValue = ItemID;
                Attestmodel.VoucherDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

                #endregion
                int ID;
                ArrayList modelList = new ArrayList();//定义数组存储实体对象
                //添加凭证单据信息

                //开始时间
                string StartDate = string.Empty;
                //结束时间
                string EndDate = string.Empty;
                //月末数
                string DayEnd = string.Empty;
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
                StartDate = yearString.ToString() + "-" + MothString.ToString() + "-01";
                EndDate = yearString.ToString() + "-" + MothString.ToString() + DayEnd;
                if (!CheckProfitEndAmount(StartDate,EndDate))
                {
                    #region 凭证单据实体赋值
                    int PeriodRecordID = 0;

                    result = EndItemProcessing.InsertPeriodProced(CompanyCD, ItemID, PeriodNum, "1", out  PeriodRecordID);
                    if (PeriodRecordID > 0)
                    {
                        Attestmodel.FromValue = PeriodRecordID.ToString();
                    }
                    else
                    {
                        Attestmodel.FromValue = "";
                    }


                    result = EndItemProcessing.BuildAttestBill(Attestmodel, out ID);
                    AttestID = ID;
                    //损益类方向为借的ID
                    string AccountIDj = string.Empty;
                    //损益类方向为贷的ID
                    string AccountIDd = string.Empty;
                    //获取本币币种ID
                    int CurrencyID = Convert.ToInt32(CurrTypeSettingDBHelper.GetMasterCurrency(CompanyCD).Rows[0]["ID"]);
                    //获取结转本期损益摘要ID
                    #region 修改于20090604 摘要直接存名称 by 莫申林
                    /*修改于20090604 摘要直接存名称 by 莫申林  start*/
                    //int AbstractID;
                    //AbstractID = Convert.ToInt32(SummarySettingBus.GetSummaryIDByName(ConstUtil.SUMMARY_JZBQSY_NAMEA));
                    string AbstractID;
                    AbstractID = ConstUtil.SUMMARY_JZBQSY_NAMEA;
                    /*修改于20090604 摘要直接存名称 by 莫申林  end*/

               

                    #endregion
                    if (result && ID > 0)
                    {
                        //取出损益类科目方向为借的信息
                        DataTable dtJ = EndItemProcessing.GetSYSubjectsEndAmount(CompanyCD, DIRECTION_J, StartDate,EndDate);
                        if (dtJ != null && dtJ.Rows.Count > 0)
                        {
                            //读取AccountIDj
                            foreach (DataRow rowj in dtJ.Rows)
                            {
                                AccountIDj += rowj["ID"].ToString() + ",";
                            }
                            AccountIDj = AccountIDj.Remove(AccountIDj.Length - 1);
                            //取出损益类方向为借的期末余额
                            DataTable dtJEndAmount = EndItemProcessing.GetSYEndAmountByID(AccountIDj);
                            if (dtJEndAmount != null && dtJEndAmount.Rows.Count > 0)
                            {
                                //读取方向为借的期末余额
                                foreach (DataRow jendrow in dtJEndAmount.Rows)
                                {
                                    if (Convert.ToDecimal(jendrow["EndAmount"]) != 0)
                                    {

                                        #region 明细实体赋值
                                        Detailmodel = new AttestBillDetailsModel();//实例凭证明细实体对象
                                        Detailmodel.AttestBillID = ID;
                                        Detailmodel.CurrencyTypeID = CurrencyID;
                                        Detailmodel.Abstract = AbstractID;//摘要ID
                                        Detailmodel.SubjectsCD = jendrow["SubjectsCD"].ToString();
                                        //判断损益科目方向为借的金额统计是否大于0
                                        //大于则为贷否则为借方

                                        if (Convert.ToDecimal(jendrow["EndAmount"]) > 0)
                                        {
                                            Detailmodel.OriginalAmount = Convert.ToDecimal(jendrow["EndAmount"]);//外币金额
                                            Detailmodel.CreditAmount = Convert.ToDecimal(jendrow["EndAmount"]);//贷方金额
                                            Detailmodel.DebitAmount = 0;
                                            //将损益类科目方向为借的逆向生成的实体添加到数组当中
                                            modelList.Add(Detailmodel);//实体对象添加到数组中

                                            #region 本年利润凭证明细赋值
                                            Detailmodel = new AttestBillDetailsModel();//实例本年利润凭证明细实体对象
                                            Detailmodel.AttestBillID = ID;
                                            Detailmodel.CurrencyTypeID = CurrencyID;
                                            Detailmodel.Abstract = AbstractID;//摘要ID
                                            Detailmodel.SubjectsCD = SUBJECT_CD_DNLY_CODE;//本年利润科目

                                            Detailmodel.OriginalAmount = Convert.ToDecimal(jendrow["EndAmount"]);//外币金额
                                            Detailmodel.DebitAmount = Convert.ToDecimal(jendrow["EndAmount"]);//借方金额
                                            Detailmodel.CreditAmount = 0;//贷方金额为0

                                            //本年利润实体对象添加到数组中
                                            modelList.Add(Detailmodel);
                                            #endregion

                                        }
                                        else
                                        {
                                            decimal EndAmount = Convert.ToDecimal(jendrow["EndAmount"]);
                                            EndAmount = Convert.ToDecimal(EndAmount.ToString().Substring(EndAmount.ToString().IndexOf("-") + 1, EndAmount.ToString().Length - EndAmount.ToString().IndexOf("-") - 1));

                                            //Detailmodel.OriginalAmount = Convert.ToDecimal(jendrow["EndAmount"]);//外币金额
                                            //Detailmodel.DebitAmount = Convert.ToDecimal(jendrow["EndAmount"]);//借方金额
                                            //Detailmodel.CreditAmount = 0;//贷方金额为0
                                            //modelList.Add(Detailmodel);//实体对象添加到数组中

                                            //#region 本年利润凭证明细赋值
                                            //Detailmodel = new AttestBillDetailsModel();//实例本年利润凭证明细实体对象
                                            //Detailmodel.AttestBillID = ID;
                                            //Detailmodel.Abstract = AbstractID;//摘要ID
                                            //Detailmodel.CurrencyTypeID = CurrencyID;
                                            //Detailmodel.SubjectsCD = SUBJECT_CD_DNLY_CODE;//本年利润科目
                                            //Detailmodel.CreditAmount = Convert.ToDecimal(jendrow["EndAmount"]);//贷方金额
                                            //Detailmodel.OriginalAmount = Convert.ToDecimal(jendrow["EndAmount"]);//外币金额
                                            //Detailmodel.DebitAmount = 0;//借方金额为0


                                            Detailmodel.OriginalAmount = EndAmount;//外币金额
                                            Detailmodel.DebitAmount = EndAmount;//借方金额
                                            Detailmodel.CreditAmount = 0;//贷方金额为0
                                            modelList.Add(Detailmodel);//实体对象添加到数组中

                                            #region 本年利润凭证明细赋值
                                            Detailmodel = new AttestBillDetailsModel();//实例本年利润凭证明细实体对象
                                            Detailmodel.AttestBillID = ID;
                                            Detailmodel.Abstract = AbstractID;//摘要ID
                                            Detailmodel.CurrencyTypeID = CurrencyID;
                                            Detailmodel.SubjectsCD = SUBJECT_CD_DNLY_CODE;//本年利润科目
                                            Detailmodel.CreditAmount = EndAmount;//贷方金额
                                            Detailmodel.OriginalAmount = EndAmount; //外币金额
                                            Detailmodel.DebitAmount = 0;//借方金额为0


                                            //本年利润实体对象添加到数组中
                                            modelList.Add(Detailmodel);
                                            #endregion
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                        //取出损益类科目方向为贷的信息
                        DataTable dtD = EndItemProcessing.GetSYSubjectsEndAmount(CompanyCD, DIRECTION_D,StartDate,EndDate);
                        if (dtD != null && dtD.Rows.Count > 0)
                        {
                            //读取AccountIDd
                            foreach (DataRow rowd in dtD.Rows)
                            {
                                AccountIDd += rowd["ID"].ToString() + ",";
                            }
                            AccountIDd = AccountIDd.Remove(AccountIDd.Length - 1);
                            //取出损益类方向为贷的期末余额
                            DataTable dtDEndAmount = EndItemProcessing.GetSYEndAmountByID(AccountIDd);
                            if (dtDEndAmount != null && dtDEndAmount.Rows.Count > 0)
                            {
                                //读取方向为贷的期末余额
                                foreach (DataRow Dendrow in dtDEndAmount.Rows)
                                {
                                    #region 明细实体赋值
                                    if (Convert.ToDecimal(Dendrow["EndAmount"]) != 0)
                                    {
                                        Detailmodel = new AttestBillDetailsModel();//实例凭证明细实体对象
                                        Detailmodel.AttestBillID = ID;
                                        Detailmodel.Abstract = AbstractID;//摘要ID
                                        Detailmodel.SubjectsCD = Dendrow["SubjectsCD"].ToString();
                                        Detailmodel.CurrencyTypeID = CurrencyID;

                                        //判断损益科目方向为贷的金额统计是否大于0
                                        //大于则为借否则为贷方
                                        if (Convert.ToDecimal(Dendrow["EndAmount"]) > 0)
                                        {
                                            Detailmodel.CreditAmount = 0; //贷方金额
                                            Detailmodel.OriginalAmount = Convert.ToDecimal(Dendrow["EndAmount"]);//外币金额
                                            Detailmodel.DebitAmount = Convert.ToDecimal(Dendrow["EndAmount"]);//借方金额

                                            //将损益类科目方向为贷的逆向生成的实体添加到数组当中
                                            modelList.Add(Detailmodel);//实体对象添加到数组中

                                            #region 本年利润凭证明细赋值
                                            Detailmodel = new AttestBillDetailsModel();//实例本年利润凭证明细实体对象
                                            Detailmodel.AttestBillID = ID;
                                            Detailmodel.Abstract = AbstractID;//摘要ID
                                            Detailmodel.SubjectsCD = SUBJECT_CD_DNLY_CODE;//本年利润科目
                                            Detailmodel.DebitAmount = 0;//借方金额为0
                                            Detailmodel.CurrencyTypeID = CurrencyID;
                                            Detailmodel.CreditAmount = Convert.ToDecimal(Dendrow["EndAmount"]); ;//贷方金额
                                            Detailmodel.OriginalAmount = Convert.ToDecimal(Dendrow["EndAmount"]);//外币金额
                                            //本年利润实体对象添加到数组中
                                            modelList.Add(Detailmodel);
                                            #endregion

                                        }
                                        else
                                        {

                                            decimal EndAmount = Convert.ToDecimal(Dendrow["EndAmount"]);
                                            EndAmount = Convert.ToDecimal(EndAmount.ToString().Substring(EndAmount.ToString().IndexOf("-") + 1, EndAmount.ToString().Length - EndAmount.ToString().IndexOf("-") - 1));


                                            //Detailmodel.DebitAmount = 0;//借方金额为0
                                            //Detailmodel.OriginalAmount = Convert.ToDecimal(Dendrow["EndAmount"]);//外币金额
                                            //Detailmodel.CreditAmount = Convert.ToDecimal(Dendrow["EndAmount"]); ;//贷方金额
                                            //modelList.Add(Detailmodel);//实体对象添加到数组中

                                            //#region 本年利润凭证明细赋值
                                            //Detailmodel = new AttestBillDetailsModel();//实例本年利润凭证明细实体对象
                                            //Detailmodel.AttestBillID = ID;
                                            //Detailmodel.Abstract = AbstractID;//摘要ID
                                            //Detailmodel.CurrencyTypeID = CurrencyID;
                                            //Detailmodel.OriginalAmount = Convert.ToDecimal(Dendrow["EndAmount"]);//外币金额
                                            //Detailmodel.SubjectsCD = SUBJECT_CD_DNLY_CODE;//本年利润科目
                                            //Detailmodel.CreditAmount = 0;//贷方金额为0
                                            //Detailmodel.DebitAmount = Convert.ToDecimal(Dendrow["EndAmount"]); ;//借方金额


                                            Detailmodel.DebitAmount = 0;//借方金额为0
                                            Detailmodel.OriginalAmount = EndAmount;//外币金额
                                            Detailmodel.CreditAmount = EndAmount;//贷方金额
                                            modelList.Add(Detailmodel);//实体对象添加到数组中

                                            #region 本年利润凭证明细赋值
                                            Detailmodel = new AttestBillDetailsModel();//实例本年利润凭证明细实体对象
                                            Detailmodel.AttestBillID = ID;
                                            Detailmodel.Abstract = AbstractID;//摘要ID
                                            Detailmodel.CurrencyTypeID = CurrencyID;
                                            Detailmodel.OriginalAmount = EndAmount;//外币金额
                                            Detailmodel.SubjectsCD = SUBJECT_CD_DNLY_CODE;//本年利润科目
                                            Detailmodel.CreditAmount = 0;//贷方金额为0
                                            Detailmodel.DebitAmount = EndAmount;//借方金额

                                            //本年利润实体对象添加到数组中
                                            modelList.Add(Detailmodel);
                                            #endregion
                                        }
                                    #endregion
                                    }
                                }
                            }
                        }
                    }
                    //组合更新ID
                    string UpdateID = string.Empty;
                    if (AccountIDd.Length > 0 && AccountIDj.Length > 0)
                    {
                        UpdateID = AccountIDd + "," + AccountIDj;
                    }
                    result = EndItemProcessing.BuildJZDetailInfo(modelList, UpdateID);
                    if (result)
                    {
                         //凭证自动登帐及自动审核
                         VoucherBus.SetStatus(ID.ToString(), "1", "status", 0);
                         VoucherBus.InsertAccount(AttestID);
                    }
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

        #region 检查是否有损益类科目期末信息
        private static bool CheckProfitEndAmount(string StartDate,string EndDate)
        {
            bool result = false;
                    //损益类方向为借的ID
                    string AccountIDj = string.Empty;
                    //损益类方向为贷的ID
                    string AccountIDd = string.Empty;

            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
  
            //借方期末余额
            decimal dtJEndAmount = 0;
            //贷方期末余额
            decimal dtDEndAmount = 0;
            //取出损益类科目方向为借的信息
            DataTable dtJ = EndItemProcessing.GetSYSubjectsEndAmount(CompanyCD, DIRECTION_J,StartDate,EndDate);
            if (dtJ != null && dtJ.Rows.Count > 0)
            {
                //读取AccountIDj
                foreach (DataRow rowj in dtJ.Rows)
                {
                    AccountIDj += rowj["ID"].ToString() + ",";
                }
                AccountIDj = AccountIDj.Remove(AccountIDj.Length - 1);
                //取出损益类方向为借的期末余额
                dtJEndAmount = EndItemProcessing.GetSYCountAmountByID(AccountIDj);
            }
            DataTable dtD = EndItemProcessing.GetSYSubjectsEndAmount(CompanyCD, DIRECTION_D, StartDate, EndDate);
            if (dtD != null && dtD.Rows.Count > 0)
            {
                //读取AccountIDd
                foreach (DataRow rowd in dtD.Rows)
                {
                    AccountIDd += rowd["ID"].ToString() + ",";
                }
                AccountIDd = AccountIDd.Remove(AccountIDd.Length - 1);
                //取出损益类方向为贷的期末余额
                dtDEndAmount = EndItemProcessing.GetSYCountAmountByID(AccountIDd);
            }
            if (dtJEndAmount == 0 && dtDEndAmount == 0)
            {
                result = true;
            }
            return result;

        }
        #endregion

        #region 获取账簿中所有损益类科目的期末余额 add by Moshenlin 2009-08-11
        /// <summary>
        /// 获取账簿中所有损益类科目的期末余额
        /// </summary>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns></returns>
        public static DataTable GetProfitandLossInfo(string StartDate, string EndDate)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return EndItemProcessing.GetProfitandLossInfo(CompanyCD, StartDate, EndDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 损益类科目数据集转换 add by Moshenlin 2009-08-11
        public static DataTable GetChangeDt(DataTable dt, int Direction)
        {
            try
            {
                DataTable sourcedt = dt.Clone();
                sourcedt.Clear();
                switch (Direction)
                {
                    case 0://损益类科目期末余额为借方的数据集
                        foreach (DataRow dr in dt.Rows)
                        {
                            if ((dr["Direction"].ToString() == "0" && Convert.ToDecimal(dr["EndM"].ToString()) > 0) || (dr["Direction"].ToString() == "1" && Convert.ToDecimal(dr["EndM"].ToString()) < 0))
                            {
                                DataRow row = sourcedt.NewRow();
                                row["SubjectsCD"] = dr["SubjectsCD"].ToString();
                                row["Direction"] = Direction.ToString();
                                row["EndM"] = Math.Abs(Convert.ToDecimal(dr["EndM"].ToString()));
                                sourcedt.Rows.Add(row);
                            }
                        }
                        break;
                    case 1://损益类科目期末余额为贷方的数据集
                        foreach (DataRow dr in dt.Rows)
                        {
                            if ((dr["Direction"].ToString() == "0" && Convert.ToDecimal(dr["EndM"].ToString()) < 0) || (dr["Direction"].ToString() == "1" && Convert.ToDecimal(dr["EndM"].ToString()) > 0))
                            {
                                DataRow row = sourcedt.NewRow();
                                row["SubjectsCD"] = dr["SubjectsCD"].ToString();
                                row["Direction"] = Direction.ToString();
                                row["EndM"] = Math.Abs(Convert.ToDecimal(dr["EndM"].ToString()));
                                sourcedt.Rows.Add(row);
                            }
                        }
                        break;
                }
                return sourcedt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  损益科目期末结转  add by Moshenlin 2009-08-11
        /// <summary>
        /// 损益科目期末结转
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="PeriodNum"></param>
        /// <param name="getAttestNo"></param>
        /// <param name="AttestID"></param>
        /// <param name="DuringDate"></param>
        /// <returns></returns>
        public static bool ProfitandLossMade(string ItemID, string PeriodNum, ref string getAttestNo, ref string AttestID, string DuringDate)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                bool result = false;

                 //开始时间
                string StartDate = string.Empty;
                //结束时间
                string EndDate = string.Empty;
                //月末数
                string DayEnd = string.Empty;
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
                StartDate = yearString.ToString() + "-" + MothString.ToString() + "-01";
                EndDate = yearString.ToString() + "-" + MothString.ToString() + DayEnd;
                if (!CheckProfitEndAmount(StartDate, EndDate))
                {

                    //获取本币币种ID
                    int CurrencyID = Convert.ToInt32(CurrTypeSettingDBHelper.GetMasterCurrency(CompanyCD).Rows[0]["ID"]);
                    //获取结转本期损益摘要ID
                    // 修改于20090604 摘要直接存名称 by 莫申林
                    /*修改于20090604 摘要直接存名称 by 莫申林  start*/
                    //int AbstractID;
                    //AbstractID = Convert.ToInt32(SummarySettingBus.GetSummaryIDByName(ConstUtil.SUMMARY_JZBQSY_NAMEA));
                    string AbstractID;
                    AbstractID = ConstUtil.SUMMARY_JZBQSY_NAMEA;
                    /*修改于20090604 摘要直接存名称 by 莫申林  end*/


                    AttestBillModel Attestmodel = new AttestBillModel(); //实例凭证单据实体对象
                    #region 凭证单据实体赋值
                    Attestmodel.CompanyCD = CompanyCD;
                   
                    Attestmodel.AttestName = "记账凭证";
                    Attestmodel.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                    Attestmodel.status = 0;
                    Attestmodel.FromTbale = "officedba.EndItemProcessedRecord";
                    //   Attestmodel.FromValue = ItemID;
                    Attestmodel.VoucherDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    Attestmodel.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    Attestmodel.Attachment = 0;
                    Attestmodel.FromName = "ProfitandLoss";
                    #region 更新期末处理结转损益状态
                    int PeriodRecordID = 0;
                    result = EndItemProcessing.InsertPeriodProced(CompanyCD, ItemID, PeriodNum, "1", out  PeriodRecordID);
                    #endregion

                    if (PeriodRecordID > 0)
                    {
                        Attestmodel.FromValue = PeriodRecordID.ToString();
                    }
                    else
                    {
                        Attestmodel.FromValue = "";
                    }
                    #endregion

                    DataTable sourcedt = GetProfitandLossInfo(StartDate, EndDate);//获取账簿中所有损益类科目的期末余额
                    DataTable debitdt = GetChangeDt(sourcedt, 0);//期末余额为借方的损益类科目信息数据集
                    DataTable creditdt = GetChangeDt(sourcedt, 1);//期末余额为贷方的损益类科目信息数据集

                    
                    if (debitdt != null && debitdt.Rows.Count > 0)
                    {
                        decimal Money = 0;
                        int VouchreID = 0;
                        ArrayList MyList = new ArrayList();
                        Attestmodel.AttestNo = "记-" + VoucherDBHelper.GetMaxAttestNo(CompanyCD, DateTime.Now.ToString("yyyy-MM-dd"));
                        getAttestNo += Attestmodel.AttestNo + ",";


                        foreach (DataRow dr in debitdt.Rows)
                        {
                            AttestBillDetailsModel Detailmodel = new AttestBillDetailsModel();//凭证明细实体定义
                            Detailmodel.Abstract = AbstractID;//摘要ID
                            Detailmodel.SubjectsCD = dr["SubjectsCD"].ToString();
                            Detailmodel.CurrencyTypeID = CurrencyID;
                            Detailmodel.ExchangeRate = 1;
                            Detailmodel.CreditAmount = Convert.ToDecimal(dr["EndM"].ToString()); //贷方金额
                            Detailmodel.OriginalAmount = Convert.ToDecimal(dr["EndM"].ToString());//外币金额
                            Detailmodel.DebitAmount = 0; //借方金额
                            Detailmodel.SubjectsDetails = "";
                            Detailmodel.FormTBName = "";
                            Detailmodel.FileName = "";
                            Money += Convert.ToDecimal(dr["EndM"].ToString());
                            MyList.Add(Detailmodel);

                        }

                        #region 本年利润凭证明细赋值
                        AttestBillDetailsModel AttestDetailmodel = new AttestBillDetailsModel();//凭证明细实体定义
                        AttestDetailmodel.Abstract = AbstractID;//摘要ID
                        AttestDetailmodel.SubjectsCD = SUBJECT_CD_DNLY_CODE;//本年利润科目
                        AttestDetailmodel.DebitAmount = Money;//借方金额
                        AttestDetailmodel.CurrencyTypeID = CurrencyID;
                        AttestDetailmodel.ExchangeRate = 1;
                        AttestDetailmodel.CreditAmount = 0;//贷方金额
                        AttestDetailmodel.OriginalAmount = Money;//外币金额
                        AttestDetailmodel.SubjectsDetails = "";
                        AttestDetailmodel.FormTBName = "";
                        AttestDetailmodel.FileName = "";
                        //本年利润实体对象添加到数组中
                        MyList.Add(AttestDetailmodel);
                        #endregion

                        result=VoucherBus.InsertIntoAttestBill(Attestmodel, MyList, out VouchreID, "1");
                        AttestID += VouchreID + ",";
                    }


                    if (creditdt != null && creditdt.Rows.Count > 0)
                    {
                        decimal Money = 0;
                        int VouchreID = 0;
                        ArrayList MyList = new ArrayList();
                        Attestmodel.AttestNo = "记-" + VoucherDBHelper.GetMaxAttestNo(CompanyCD, DateTime.Now.ToString("yyyy-MM-dd"));
                        getAttestNo += Attestmodel.AttestNo + ",";


                        foreach (DataRow dr in creditdt.Rows)
                        {
                            AttestBillDetailsModel Detailmodel = new AttestBillDetailsModel();//凭证明细实体定义
                            Detailmodel.Abstract = AbstractID;//摘要ID
                            Detailmodel.SubjectsCD = dr["SubjectsCD"].ToString();
                            Detailmodel.CurrencyTypeID = CurrencyID;
                            Detailmodel.ExchangeRate = 1;
                            Detailmodel.CreditAmount = 0; //贷方金额
                            Detailmodel.OriginalAmount = Convert.ToDecimal(dr["EndM"].ToString());//外币金额
                            Detailmodel.DebitAmount = Convert.ToDecimal(dr["EndM"].ToString()); //借方金额
                            Detailmodel.SubjectsDetails = "";
                            Detailmodel.FormTBName = "";
                            Detailmodel.FileName = "";
                            Money += Convert.ToDecimal(dr["EndM"].ToString());
                            MyList.Add(Detailmodel);

                        }

                        #region 本年利润凭证明细赋值
                        AttestBillDetailsModel AttestDetailmodel = new AttestBillDetailsModel();//凭证明细实体定义
                        AttestDetailmodel.Abstract = AbstractID;//摘要ID
                        AttestDetailmodel.SubjectsCD = SUBJECT_CD_DNLY_CODE;//本年利润科目
                        AttestDetailmodel.DebitAmount = 0;//借方金额
                        AttestDetailmodel.CurrencyTypeID = CurrencyID;
                        AttestDetailmodel.ExchangeRate = 1;
                        AttestDetailmodel.CreditAmount = Money;//贷方金额
                        AttestDetailmodel.OriginalAmount = Money;//外币金额
                        AttestDetailmodel.SubjectsDetails = "";
                        AttestDetailmodel.FormTBName = "";
                        AttestDetailmodel.FileName = "";
                        //本年利润实体对象添加到数组中
                        MyList.Add(AttestDetailmodel);
                        #endregion

                        result=VoucherBus.InsertIntoAttestBill(Attestmodel, MyList, out VouchreID, "1");
                        AttestID += VouchreID + ",";
                    }
                }
                AttestID = AttestID.TrimEnd(new char[] { ',' });
                getAttestNo = getAttestNo.TrimEnd(new char[] { ',' });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取账簿中所有资产类及损益类科目的本位币余额与外币余额乘以期末调汇后汇率之差 --add by Moshenlin 2009-08-12
      /// <summary>
      /// 获取账簿中所有资产类及损益类科目的本位币余额与外币余额乘以期末调汇后汇率之差
      /// </summary>
      /// <param name="RateStr">期末处理调整后的汇率集</param>
      /// <param name="CurrencyTypeStr">期末处理调整后的币种主键集</param>
      /// <param name="CompanyCD">公司编码</param>
      /// <param name="StartDate">凭证开始日期</param>
      /// <param name="EndDate">凭证结束日期</param>
      /// <param name="MasterCurrencyType">本位币币种主键</param>
      /// <returns></returns>
        public static DataTable GetTermEndAdjustmentSource(string RateStr, string CurrencyTypeStr, string StartDate, string EndDate, string MasterCurrencyType)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return EndItemProcessing.GetTermEndAdjustmentSource(RateStr, CurrencyTypeStr, CompanyCD, StartDate, EndDate, MasterCurrencyType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 期末调汇业务处理 -add by Moshenlin 2009-08-13
        /// <summary>
        /// 期末调汇业务处理
        /// </summary>
        /// <param name="RateStr"></param>
        /// <param name="CurrencyStr"></param>
        /// <param name="ItemID"></param>
        /// <param name="PeriodNum"></param>
        /// <param name="AttestNo"></param>
        /// <param name="AttestID"></param>
        /// <param name="DuringDate"></param>
        /// <returns></returns>
        public static bool TermEndAdjustment(string RateStr,string CurrencyStr,
           string ItemID, string PeriodNum, ref string AttestNo, ref string AttestID, string DuringDate)
        {
            if (RateStr.Trim().Length<=0) return false;
            bool result = false;
            //获取当前企业代码
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                //获取本币币种ID
                int MasterCurrencyID = Convert.ToInt32(CurrTypeSettingDBHelper.GetMasterCurrency(CompanyCD).Rows[0]["ID"]);
                //获取结转汇兑损益摘要ID

                /*修改于20090604 摘要直接存名称 by 莫申林  start*/
                //int AbstractID;
                //AbstractID = Convert.ToInt32(SummarySettingBus.GetSummaryIDByName(ConstUtil.SUMMARY_JZHDSY_NAME));
                string AbstractID;
                AbstractID = ConstUtil.SUMMARY_JZHDSY_NAME;
                /*修改于20090604 摘要直接存名称 by 莫申林  end*/

                //对输入的资料日期进行处理开始
                //开始时间
                string StartDate = string.Empty;
                //结束时间
                string EndDate = string.Empty;
                string DayEnd = string.Empty;//月末数
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
                StartDate = yearString.ToString() + "-" + MothString.ToString() + "-01";
                EndDate = yearString.ToString() + "-" + MothString.ToString() + DayEnd;



                AttestBillModel Attestmodel = new AttestBillModel(); //实例凭证单据实体对象
                #region 凭证单据实体赋值
                Attestmodel.CompanyCD = CompanyCD;

                Attestmodel.AttestName = "记账凭证";
                Attestmodel.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                Attestmodel.status = 0;
                Attestmodel.FromTbale = "officedba.EndItemProcessedRecord";
                //   Attestmodel.FromValue = ItemID;
                Attestmodel.VoucherDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                Attestmodel.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                Attestmodel.Attachment = 0;
                Attestmodel.FromName = "";
                #region 更新期末处理结转损益状态
                int PeriodRecordID = 0;
                result = EndItemProcessing.InsertPeriodProced(CompanyCD, ItemID, PeriodNum, "1", out  PeriodRecordID);
                #endregion

                if (PeriodRecordID > 0)
                {
                    Attestmodel.FromValue = PeriodRecordID.ToString();
                }
                else
                {
                    Attestmodel.FromValue = "";
                }
                #endregion

                DataTable sourcedt = EndItemProcessing.GetTermEndAdjustmentSource(RateStr, CurrencyStr, CompanyCD, StartDate, EndDate, MasterCurrencyID.ToString());
                DataTable debitdt = ChangTermEndAdjustmentSource(sourcedt, 0);
                DataTable creditdt = ChangTermEndAdjustmentSource(sourcedt,1);





                  if (debitdt != null && debitdt.Rows.Count > 0)
                    {
                        decimal Money = 0;
                        int VouchreID = 0;
                        ArrayList MyList = new ArrayList();
                        Attestmodel.AttestNo = "记-" + VoucherDBHelper.GetMaxAttestNo(CompanyCD, DateTime.Now.ToString("yyyy-MM-dd"));
                        AttestNo += Attestmodel.AttestNo + ",";


                        foreach (DataRow dr in debitdt.Rows)
                        {
                            AttestBillDetailsModel Detailmodel = new AttestBillDetailsModel();//凭证明细实体定义
                            Detailmodel.Abstract = AbstractID;//摘要ID
                            Detailmodel.SubjectsCD = dr["SubjectsCD"].ToString();
                            Detailmodel.CurrencyTypeID = MasterCurrencyID;
                            Detailmodel.ExchangeRate = 1;
                            Detailmodel.CreditAmount =0; //贷方金额
                            Detailmodel.OriginalAmount = Convert.ToDecimal(dr["EndChange"].ToString());//外币金额
                            Detailmodel.DebitAmount =  Convert.ToDecimal(dr["EndChange"].ToString()); //借方金额
                            Detailmodel.SubjectsDetails = "";
                            Detailmodel.FormTBName = "";
                            Detailmodel.FileName = "";
                            Money += Convert.ToDecimal(dr["EndChange"].ToString());
                            MyList.Add(Detailmodel);

                        }

                        #region 结转汇兑损益凭证明细赋值
                        AttestBillDetailsModel AttestDetailmodel = new AttestBillDetailsModel();//凭证明细实体定义
                        AttestDetailmodel.Abstract = AbstractID;//摘要ID
                        AttestDetailmodel.SubjectsCD = SUBJECT_CD_HDSY_CODE;//结转汇兑损益科目
                        AttestDetailmodel.DebitAmount = 0;//借方金额
                        AttestDetailmodel.CurrencyTypeID = MasterCurrencyID;
                        AttestDetailmodel.ExchangeRate = 1;
                        AttestDetailmodel.CreditAmount = Money;//贷方金额
                        AttestDetailmodel.OriginalAmount = Money;//外币金额
                        AttestDetailmodel.SubjectsDetails = "";
                        AttestDetailmodel.FormTBName = "";
                        AttestDetailmodel.FileName = "";
                        //本年利润实体对象添加到数组中
                        MyList.Add(AttestDetailmodel);
                        #endregion

                        result=VoucherBus.InsertIntoAttestBill(Attestmodel, MyList, out VouchreID, "1");
                        AttestID += VouchreID + ",";
                    }


                    if (creditdt != null && creditdt.Rows.Count > 0)
                    {
                        decimal Money = 0;
                        int VouchreID = 0;
                        ArrayList MyList = new ArrayList();
                        Attestmodel.AttestNo = "记-" + VoucherDBHelper.GetMaxAttestNo(CompanyCD, DateTime.Now.ToString("yyyy-MM-dd"));
                        AttestNo += Attestmodel.AttestNo + ",";


                        foreach (DataRow dr in creditdt.Rows)
                        {
                            AttestBillDetailsModel Detailmodel = new AttestBillDetailsModel();//凭证明细实体定义
                            Detailmodel.Abstract = AbstractID;//摘要ID
                            Detailmodel.SubjectsCD = dr["SubjectsCD"].ToString();
                            Detailmodel.CurrencyTypeID = MasterCurrencyID;
                            Detailmodel.ExchangeRate = 1;
                            Detailmodel.CreditAmount = Convert.ToDecimal(dr["EndChange"].ToString()); //贷方金额
                            Detailmodel.OriginalAmount = Convert.ToDecimal(dr["EndChange"].ToString());//外币金额
                            Detailmodel.DebitAmount = 0; //借方金额
                            Detailmodel.SubjectsDetails = "";
                            Detailmodel.FormTBName = "";
                            Detailmodel.FileName = "";
                            Money += Convert.ToDecimal(dr["EndChange"].ToString());
                            MyList.Add(Detailmodel);

                        }

                        #region 结转汇兑损益凭证明细赋值
                        AttestBillDetailsModel AttestDetailmodel = new AttestBillDetailsModel();//凭证明细实体定义
                        AttestDetailmodel.Abstract = AbstractID;//摘要ID
                        AttestDetailmodel.SubjectsCD = SUBJECT_CD_HDSY_CODE;//结转汇兑损益科目
                        AttestDetailmodel.DebitAmount = Money;//借方金额
                        AttestDetailmodel.CurrencyTypeID = MasterCurrencyID;
                        AttestDetailmodel.ExchangeRate = 1;
                        AttestDetailmodel.CreditAmount =0 ;//贷方金额
                        AttestDetailmodel.OriginalAmount = Money;//外币金额
                        AttestDetailmodel.SubjectsDetails = "";
                        AttestDetailmodel.FormTBName = "";
                        AttestDetailmodel.FileName = "";
                        //本年利润实体对象添加到数组中
                        MyList.Add(AttestDetailmodel);
                        #endregion

                        result=VoucherBus.InsertIntoAttestBill(Attestmodel, MyList, out VouchreID, "1");
                        AttestID += VouchreID + ",";
                    }
                AttestID = AttestID.TrimEnd(new char[] { ',' });
                AttestNo = AttestNo.TrimEnd(new char[] { ',' });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  期末调汇数据集转换 -add by Moshenlin 2009-08-13
        /// <summary>
        /// 期末调汇数据集转换
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Direction"></param>
        /// <returns></returns>
        public static DataTable ChangTermEndAdjustmentSource(DataTable dt , int Direction)
        {
            try
            {
                DataTable sourcedt = dt.Clone();
                sourcedt.Clear();
                switch (Direction)
                {
                    case 0://资产类:本位币期末余额减去外币的期末余额乘以调整后的汇率小于0的金额放借方，负债类：本位币期末余额减去外币的期末余额乘以调整后的汇率大于0的金额放借方
                        foreach (DataRow dr in dt.Rows)
                        {
                            if ((dr["Direction"].ToString() == "0" && Convert.ToDecimal(dr["EndChange"].ToString()) < 0) || (dr["Direction"].ToString() == "1" && Convert.ToDecimal(dr["EndChange"].ToString()) > 0))
                            {
                                DataRow row = sourcedt.NewRow();
                                row["SubjectsCD"] = dr["SubjectsCD"].ToString();
                                row["Direction"] = Direction.ToString();
                                row["EndChange"] = Math.Abs(Convert.ToDecimal(dr["EndChange"].ToString()));
                                sourcedt.Rows.Add(row);
                            }
                        }
                        break;
                    case 1://资产类:本位币期末余额减去外币的期末余额乘以调整后的汇率大于0的金额放贷方，负债类：本位币期末余额减去外币的期末余额乘以调整后的汇率小于0的金额放贷方
                        foreach (DataRow dr in dt.Rows)
                        {
                            if ((dr["Direction"].ToString() == "0" && Convert.ToDecimal(dr["EndChange"].ToString()) > 0) || (dr["Direction"].ToString() == "1" && Convert.ToDecimal(dr["EndChange"].ToString()) < 0))
                            {
                                DataRow row = sourcedt.NewRow();
                                row["SubjectsCD"] = dr["SubjectsCD"].ToString();
                                row["Direction"] = Direction.ToString();
                                row["EndChange"] = Math.Abs(Convert.ToDecimal(dr["EndChange"].ToString()));
                                sourcedt.Rows.Add(row);
                            }
                        }
                        break;
                }
                return sourcedt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

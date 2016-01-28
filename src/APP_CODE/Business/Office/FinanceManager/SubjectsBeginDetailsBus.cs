/**********************************************
 * 描述：     科目期初设置业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/06/19
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
namespace XBase.Business.Office.FinanceManager
{
    public class SubjectsBeginDetailsBus
    {
        #region 常量定义
        const string SUBJECT_INIT_CODE_ZERO = "0";//未初始化
        const string SUBJECT_INIT_CODE_ONE = "1";//已初始化
        #endregion

        #region 判断是否有期初值
        public static bool IsPeriodMoney()
        {
            bool result = false;
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                result = SubjectsBeginDetailsDBHelper.IsPeriodMoney(CompanyCD);


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 检索科目是否已存在
        public static bool SubjectsCDisExist(string SubjectsCD, string CurrencyType,string SubjectsDetails,string FormTBName,string FileName)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


                return SubjectsBeginDetailsDBHelper.SubjectsCDisExist(CompanyCD, SubjectsCD, CurrencyType,SubjectsDetails,FormTBName,FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 检索科目期初明细信息
        public static DataTable SearchSubjectDetatilInfo(string CurrencyType,string SubjectsType)
        {
            try
            {
                 string CompanyCD=((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                  DataTable dt =SubjectsBeginDetailsDBHelper.SearchSubjectDetatilInfo(CurrencyType, CompanyCD, SubjectsType);
                  DataTable sumdt = GetSumInfo(SubjectsType, CurrencyType);
                  if (sumdt != null && sumdt.Rows.Count > 0 && dt.Rows.Count > 0)
                  {
                      DataRow Rows = dt.NewRow();
                      Rows["ID"] ="1000";
                      Rows["Dirt"] = "";
                      Rows["SubjectsCD"] = "合计";
                      Rows["YTotalDebit"] = sumdt.Rows[0]["YTotalDebit"].ToString();
                      Rows["YTotalDebitY"] = sumdt.Rows[0]["YTotalDebitY"].ToString();
                      Rows["YTotalLendersY"] = sumdt.Rows[0]["YTotalLendersY"].ToString();
                      Rows["YTotalLenders"] = sumdt.Rows[0]["YTotalLenders"].ToString();
                      Rows["OriginalCurrency"] = sumdt.Rows[0]["OriginalCurrency"].ToString();
                      Rows["StandardCurrency"] = sumdt.Rows[0]["StandardCurrency"].ToString();
                      Rows["BeginMoney"] = sumdt.Rows[0]["BeginMoney"].ToString();
                      Rows["SumOriginalCurrency"] = sumdt.Rows[0]["SumOriginalCurrency"].ToString();
                      Rows["ByOrder"] = "1";
                      dt.Rows.Add(Rows);
                  }
              return dt;   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 综合本位币汇总
        public static DataTable  SearchSumStandardCurrency( string SubjectTypeID)
        {
            try
            {
                //string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                //DataSet ds = SubjectsBeginDetailsDBHelper.SearchSumStandardCurrency(CompanyCD,SubjectTypeID);
                //DataTable dt = null;
                //dt = ds.Tables[0].Copy();
                //if (ds != null && ds.Tables[1].Rows.Count > 0 && ds.Tables[0].Rows.Count>0)
                //{
                //    DataRow Rows = dt.NewRow();
                //    Rows["Dirt"] = "";
                //    Rows["YTotalDebit"] = ds.Tables[1].Rows[0]["YTotalDebit"].ToString();
                //    Rows["YTotalLenders"] = ds.Tables[1].Rows[0]["YTotalLenders"].ToString();
                //    Rows["StandardCurrency"] = ds.Tables[1].Rows[0]["StandardCurrency"].ToString();
                //    Rows["SubjectsName"] = "合计";
                //    Rows["OriginalCurrency"] = Convert.ToDecimal(0);
                //    Rows["BeginMoney"] = ds.Tables[1].Rows[0]["BeginMoney"].ToString();
                //    Rows["SumOriginalCurrency"] = ds.Tables[1].Rows[0]["SumOriginalCurrency"].ToString();

                //    Rows["YTotalDebitY"] = ds.Tables[1].Rows[0]["YTotalDebitY"].ToString();
                //    Rows["YTotalLendersY"] = ds.Tables[1].Rows[0]["YTotalLendersY"].ToString();

                //    dt.Rows.Add(Rows);
                //    ds.Tables.Clear();
                //}
                //dt.Columns.Add("ID");
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    int index=0;
                //    foreach (DataRow rows in dt.Rows)
                //    {
                //        rows["ID"] = ++index;
                //    }
                //}
                //return dt;

                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                DataTable dt = SubjectsBeginDetailsDBHelper.SearchSumStandardCurrency(CompanyCD, SubjectTypeID);
                DataTable sumdt = GetSumInfo(SubjectTypeID, "0");
                if (sumdt != null && sumdt.Rows.Count > 0 && dt.Rows.Count > 0)
                {
                    DataRow Rows = dt.NewRow();
                    Rows["ID"] = "1000";
                    Rows["Dirt"] = "";
                    Rows["SubjectsCD"] = "合计";
                    Rows["YTotalDebit"] = sumdt.Rows[0]["YTotalDebit"].ToString();
                    Rows["YTotalDebitY"] = sumdt.Rows[0]["YTotalDebitY"].ToString();
                    Rows["YTotalLendersY"] = sumdt.Rows[0]["YTotalLendersY"].ToString();
                    Rows["YTotalLenders"] = sumdt.Rows[0]["YTotalLenders"].ToString();
                    Rows["OriginalCurrency"] = sumdt.Rows[0]["OriginalCurrency"].ToString();
                    Rows["StandardCurrency"] = sumdt.Rows[0]["StandardCurrency"].ToString();
                    Rows["BeginMoney"] = sumdt.Rows[0]["BeginMoney"].ToString();
                    Rows["SumOriginalCurrency"] = sumdt.Rows[0]["SumOriginalCurrency"].ToString();
                    Rows["ByOrder"] = "1";
                    dt.Rows.Add(Rows);
                }
                return dt;   

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  获取企业期初值信息
        public static string GetPeriodNum()
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return SubjectsBeginDetailsDBHelper.GetPeriodNum(CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       #endregion

        #region 删除科目期初值期初
        public static bool DeleteDetailsPeriod()
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return SubjectsBeginDetailsDBHelper.DeleteDetailsPeriod(CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 检查是否存在期初值
        public static bool IsexistDetails()
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                return SubjectsBeginDetailsDBHelper.IsexistDetails(CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 检查期初信息是否存在
        public static bool PeriodIsexist()
        {
            bool result = false;

            try
            {
                 string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                 result = SubjectsBeginDetailsDBHelper.PeriodIsexist(CompanyCD);

                 return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 添加科目期初明细信息
        public static bool InsertSubjectsBeginDetails(SubjectsBeginDetailsModel model,string PeriodNum)
        {
            try
            {
                bool result = false;
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                if (model.CompanyCD == null) model.CompanyCD = CompanyCD;
                if (SubjectsBeginDetailsDBHelper.InsertSubjectsBeginDetails(model))
                {
                    result = SubjectsBeginDetailsDBHelper.InsertPeriodNum(CompanyCD, PeriodNum);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 修改初始化信息
        public static bool UpdateSubjectsInitRecord( string Flag)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                return SubjectsBeginDetailsDBHelper.UpdateSubjectsInitRecord(CompanyCD,Flag);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region  是否存在初始化记录
        public static bool IsInit()
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                return SubjectsBeginDetailsDBHelper.IsInit(CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 添加初始化信息
        public static bool InsertSubjectsInitRecord(string Flag)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                return SubjectsBeginDetailsDBHelper.InsertSubjectsInitRecord(CompanyCD,Flag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询初始化信息
        public static bool GetSubjectsInit()
        {
            bool result = false;
            
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                DataTable dt = SubjectsBeginDetailsDBHelper.GetSubjectsInit(CompanyCD);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["IsInitFlag"].ToString() == SUBJECT_INIT_CODE_ONE)
                    {
                        result = true;
                    }
                    else if (dt.Rows[0]["IsInitFlag"].ToString() == SUBJECT_INIT_CODE_ZERO)
                    {
                        result = false;
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


        #region 获取试算平衡列表
        public static DataTable GetsubjectsBalanceInfo(int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {

            try
            {
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                DataTable dt = SubjectsBeginDetailsDBHelper.GetsubjectsBalanceInfo(companyCD, pageIndex, pageSize, OrderBy, ref totalCount);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  判断期初余额是否平衡
        public static bool PeriodIsBalance()
        {
            bool result = false;

            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            DataTable dt = SubjectsBeginDetailsDBHelper.PeriodIsBalance(CompanyCD);
            if (dt != null && dt.Rows.Count == 2)
            {
                decimal JPeriodMoney = Convert.ToDecimal(dt.Rows[0][0]);
                decimal DPeriodMoney = Convert.ToDecimal(dt.Rows[1][0]);
                if (JPeriodMoney == DPeriodMoney)
                {
                    result = true;
                }
            }
            return result;
        }
        #endregion

       #region 试算平衡
        public static bool SubjectsBalance()
        {
            try
            {
                bool result = false;

                decimal YearCount = 0;//本年累计
                decimal PeriodBegin = 0;//期初余额
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                DataTable dt = SubjectsBeginDetailsDBHelper.SubjectsBalance(CompanyCD);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            PeriodBegin = Convert.ToDecimal(dt.Rows[i]["d"]) - Convert.ToDecimal(dt.Rows[i]["j"]);
                        }
                        else
                        {
                            YearCount = Convert.ToDecimal(dt.Rows[i]["d"]) - Convert.ToDecimal(dt.Rows[i]["j"]);
                        } 
                    }
                }

                if (PeriodBegin == 0 && YearCount == 0)
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
        
       #region 修改科目期初明细信息
        public static bool UpdateSubjectsBeginDetails(SubjectsBeginDetailsModel model, string PeriodNum)
        {
            try
            {

                bool result = false;
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                if (model.CompanyCD == null) model.CompanyCD = CompanyCD;
                if (SubjectsBeginDetailsDBHelper.UpdateSubjectsBeginDetails(model))
                {
                    result = SubjectsBeginDetailsDBHelper.InsertPeriodNum(CompanyCD, PeriodNum);
                }
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       #endregion

       #region 删除科目期初明细信息
        public static bool DeleteInfo(string ID)
        {
            try
            {
                return SubjectsBeginDetailsDBHelper.DeleteInfo(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取不相同的科目编码的顶级科目编码
        /// <summary>
        /// 获取不相同的科目编码的顶级科目编码
        /// </summary>
        /// <returns></returns>
        public static string GetDistinctSubjectsCD()
        {
            string str = string.Empty;
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                if (!string.IsNullOrEmpty(CompanyCD))
                {
                    DataTable dt = SubjectsBeginDetailsDBHelper.GetDistinctSubjectCD(CompanyCD);
                    foreach (DataRow dr in dt.Rows)
                    {
                        string PresubjectsCD = VoucherDBHelper.GetSubjectsPerCD(dr["SubjectsCD"].ToString()).ToString().Split(',')[0].ToString();
                        if (!str.Contains(PresubjectsCD))
                        {
                            str += PresubjectsCD + ",";
                        }
                    }
                    str = str.TrimEnd(new char[] { ',' });
                    str = str.Replace("'", "");
                }
                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 获取科目初始化个科目类别的汇总信息
        /// <summary>
        /// 获取科目初始化个科目类别的汇总信息
        /// </summary>
        /// <param name="SubjectTypeID">会计科目类别</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetSumInfo(string SubjectTypeID,string CurryTypeID)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                DataTable dt=SubjectsBeginDetailsDBHelper.GetSumInfo(SubjectTypeID, CompanyCD,CurryTypeID);
                DataTable SourceDT = dt.Clone();
                //DataTable ESourceDT = dt.Clone();
                SourceDT.Clear();
                //ESourceDT.Clear();

                //DataTable ret = new DataTable(); 

                decimal YTotalDebit = 0;
                decimal YTotalLenders = 0;
                decimal StandardCurrency = 0;
                decimal BeginMoney = 0;
                decimal SumOriginalCurrency = 0;
                decimal YTotalDebitY = 0;
                decimal YTotalLendersY = 0;
                decimal OriginalCurrency = 0;



                //decimal EYTotalDebit = 0;
                //decimal EYTotalLenders = 0;
                //decimal EStandardCurrency = 0;
                //decimal EBeginMoney = 0;
                //decimal ESumOriginalCurrency = 0;
                //decimal EYTotalDebitY = 0;
                //decimal EYTotalLendersY = 0;
                //decimal EOriginalCurrency = 0;

                foreach (DataRow dr in dt.Rows)
                {

                    //if (dr["Dirt"].ToString()=="0")
                    //{
                        YTotalDebit += Convert.ToDecimal(dr["YTotalDebit"].ToString());
                        StandardCurrency += Convert.ToDecimal(dr["StandardCurrency"].ToString());
                        YTotalLenders += Convert.ToDecimal(dr["YTotalLenders"].ToString());
                        BeginMoney += Convert.ToDecimal(dr["BeginMoney"].ToString());
                        SumOriginalCurrency += Convert.ToDecimal(dr["SumOriginalCurrency"].ToString());
                        YTotalDebitY += Convert.ToDecimal(dr["YTotalDebitY"].ToString());
                        YTotalLendersY += Convert.ToDecimal(dr["YTotalLendersY"].ToString());
                        OriginalCurrency += Convert.ToDecimal(dr["OriginalCurrency"].ToString());

                    //    EYTotalDebit -= Convert.ToDecimal(dr["YTotalDebit"].ToString());
                    //    EStandardCurrency -= Convert.ToDecimal(dr["StandardCurrency"].ToString());
                    //    EYTotalLenders -= Convert.ToDecimal(dr["YTotalLenders"].ToString());
                    //    EBeginMoney -= Convert.ToDecimal(dr["BeginMoney"].ToString());
                    //    ESumOriginalCurrency -= Convert.ToDecimal(dr["SumOriginalCurrency"].ToString());
                    //    EYTotalDebitY -= Convert.ToDecimal(dr["YTotalDebitY"].ToString());
                    //    EYTotalLendersY -= Convert.ToDecimal(dr["YTotalLendersY"].ToString());
                    //    EOriginalCurrency -= Convert.ToDecimal(dr["OriginalCurrency"].ToString());
                    //}
                    //else
                    //{
                    //    YTotalDebit -= Convert.ToDecimal(dr["YTotalDebit"].ToString());
                    //    StandardCurrency -= Convert.ToDecimal(dr["StandardCurrency"].ToString());
                    //    YTotalLenders -= Convert.ToDecimal(dr["YTotalLenders"].ToString());
                    //    BeginMoney -= Convert.ToDecimal(dr["BeginMoney"].ToString());
                    //    SumOriginalCurrency -= Convert.ToDecimal(dr["SumOriginalCurrency"].ToString());
                    //    YTotalDebitY -= Convert.ToDecimal(dr["YTotalDebitY"].ToString());
                    //    YTotalLendersY -= Convert.ToDecimal(dr["YTotalLendersY"].ToString());
                    //    OriginalCurrency -= Convert.ToDecimal(dr["OriginalCurrency"].ToString());

                    //    EYTotalDebit += Convert.ToDecimal(dr["YTotalDebit"].ToString());
                    //    EStandardCurrency += Convert.ToDecimal(dr["StandardCurrency"].ToString());
                    //    EYTotalLenders += Convert.ToDecimal(dr["YTotalLenders"].ToString());
                    //    EBeginMoney += Convert.ToDecimal(dr["BeginMoney"].ToString());
                    //    ESumOriginalCurrency += Convert.ToDecimal(dr["SumOriginalCurrency"].ToString());
                    //    EYTotalDebitY += Convert.ToDecimal(dr["YTotalDebitY"].ToString());
                    //    EYTotalLendersY += Convert.ToDecimal(dr["YTotalLendersY"].ToString());
                    //    EOriginalCurrency += Convert.ToDecimal(dr["OriginalCurrency"].ToString());

                    //}
                }

                DataRow row = SourceDT.NewRow();
                row["YTotalDebit"] = YTotalDebit.ToString();
                row["StandardCurrency"] = StandardCurrency.ToString();
                row["YTotalLenders"] = YTotalLenders.ToString();
                row["BeginMoney"] = BeginMoney.ToString();
                row["SumOriginalCurrency"] = SumOriginalCurrency.ToString();
                row["YTotalDebitY"] = YTotalDebitY.ToString();
                row["YTotalLendersY"] = YTotalLendersY.ToString();
                row["OriginalCurrency"] = OriginalCurrency.ToString();
                row["Dirt"] = "1000";
                SourceDT.Rows.Add(row);


                //DataRow Erow = ESourceDT.NewRow();
                //Erow["YTotalDebit"] = EYTotalDebit.ToString();
                //Erow["StandardCurrency"] = EStandardCurrency.ToString();
                //Erow["YTotalLenders"] = EYTotalLenders.ToString();
                //Erow["BeginMoney"] = EBeginMoney.ToString();
                //Erow["SumOriginalCurrency"] = ESumOriginalCurrency.ToString();
                //Erow["YTotalDebitY"] = EYTotalDebitY.ToString();
                //Erow["YTotalLendersY"] = EYTotalLendersY.ToString();
                //Erow["OriginalCurrency"] = EOriginalCurrency.ToString();
                //Erow["Dirt"] = "1000";
                //ESourceDT.Rows.Add(Erow);

                //switch (int.Parse(SubjectTypeID))
                //{
                //    case 1:
                //        ret= SourceDT;
                //        break;
                //    case 2:
                //        ret= ESourceDT;
                //        break;
                //    case 3:
                //        ret= SourceDT;
                //        break;
                //    case 4:
                //        ret= ESourceDT;
                //        break;
                //    case 5:
                //        ret= SourceDT;
                //        break;
                //    case 6:
                //        ret= ESourceDT;
                //        break;
                //}
                return SourceDT;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 判断企业科目期初值是否结束初始化 add by Moshenlin 2009-08-06
        /// <summary>
        /// 判断企业科目期初值是否结束初始化
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static bool IsEndInit()
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                return SubjectsBeginDetailsDBHelper.IsEndInit(CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

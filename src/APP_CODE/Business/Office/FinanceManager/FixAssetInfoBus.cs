/**********************************************
 * 描述：     固定资产业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/04/03
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;
using XBase.Business.Common;
using System.Collections;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
namespace XBase.Business.Office.FinanceManager
{
    public class FixAssetInfoBus
    {
        #region  检索企业是否有固定资产
        public static bool FixAssestIsexist()
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return FixAssetInfoDBHelper.FixAssestIsexist(CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 判断固定资产编号是否存在
        public static bool FixNoIsExist(string FixNo)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return FixAssetInfoDBHelper.FixNoIsExist(CompanyCD,FixNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        
        #region 计算固定资产折旧
        public static bool CountFixAssetDepreciation(string ItemID, string PeriodNum,
            ref string getAttestNo, ref int AttestID)
        {
            //获取当前操作用户企业编码
            bool result = false;//执行返回结果
            try
            {
                int PeriodID=0;
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                DataTable dt = FixAssetInfoDBHelper.GetAssetInfoByCompanyCD(CompanyCD,PeriodNum);
                if (dt != null && dt.Rows.Count > 0)
                {
                    #region 固定资产计提折旧计算
                    //计算方法
                    string CountMethod = string.Empty;
                    //预计使用年限
                    int EstimateUseYear;
                    //预计净残值率
                    decimal EstiResiValue;
                    //原值
                    decimal OriginalValue;
                    //月折旧率
                    decimal MonthDeprRate;
                    //期末净值
                    decimal EndNetValue;
                    //本期净值
                    decimal CurrentNetValue;
                    //本期减值准备
                    decimal CurrValueRe;
                    //月折旧额
                    decimal MonthDeprPrice = 0;
                    //累计月折旧额
                    decimal CountTotalDeprPrice = 0;

                    ArrayList DeprDetailList=new ArrayList();

                    ArrayList DeprPeriodList=new ArrayList();

                    //计提前固定资产实体数组
                    ArrayList FixDeprAfterList = new ArrayList();

                    //定义固定资产计提明细实体
                    FixAssetDeprDetailModel DeprDetailmodel = null;
                    //资产计提
                    FixAssetPeriodDeprModel DeprPeriodmodel = null;
                    //资产计提前实体
                    FixAssetDeprAfterModel FixDeprAftermodel = null;

                    foreach (DataRow rows in dt.Rows)
                    {
                        //读取期末净值
                        EndNetValue = Convert.ToDecimal(rows["EndNetValue"]);
                        //读取计算方法
                        CountMethod = rows["CountMethod"].ToString();
                        //读取预计净残值率
                        EstiResiValue = Convert.ToDecimal(rows["EstiResiValue"]);
                        //读取原价
                        OriginalValue = Convert.ToDecimal(rows["OriginalValue"]);
                        //读取本期减值准备
                        CurrValueRe = Convert.ToDecimal(rows["CurrValueRe"]);
                        //读取月折旧率
                        MonthDeprRate = Convert.ToDecimal(rows["AmorDeprRate"]);



                        //实例固定资产计提明细实体
                        DeprDetailmodel = new FixAssetDeprDetailModel();
                        #region 固定资产计提明细赋值
                        //给固定资产计提明细赋值
                        DeprDetailmodel.CompanyCD = CompanyCD;
                        DeprDetailmodel.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                        //固定资产名称
                        DeprDetailmodel.FixName = rows["FixName"].ToString();
                        //固定资产编号
                        DeprDetailmodel.FixNo = rows["FixNo"].ToString();
                        //资产类别
                        DeprDetailmodel.FixType = Convert.ToInt32(rows["FixType"]);
                        //资产数量
                        DeprDetailmodel.Number = Convert.ToInt32(rows["FixNumber"]);
                        //资产原值
                        DeprDetailmodel.OriginalValue = Convert.ToDecimal(rows["OriginalValue"]);
                        //使用日期
                        DeprDetailmodel.UsedDate = Convert.ToDateTime(rows["UseDate"]);
                        //使用年限
                        DeprDetailmodel.UsedYears = Convert.ToDecimal(rows["UsedYear"]);
                        //预计使用年限
                        DeprDetailmodel.EstimateUse = Convert.ToDecimal(rows["EstimateUse"]);


                        //计算预计净残值
                        decimal Yjcz = OriginalValue * (EstiResiValue / 100);
                        #endregion
                        //判断期末净值大于计净残值则进行折旧处理
                        if (EndNetValue > Yjcz)
                        {
                            //年限平均法
                            if (CountMethod == ConstUtil.ASSETCOUNT_METHOD_NXPJF_CODE)
                            {
                                //读取期初减值准备
                                decimal ReduValueRe = Convert.ToDecimal(rows["ReduValueRe"]);
                                //读取预计使用年限
                                EstimateUseYear = Convert.ToInt32(rows["EstimateUse"]);
                                //MonthDeprPrice = Math.Round(MonthDeprRate * OriginalValue, 2);//计算月折旧额
                                //读取月折旧额
                                MonthDeprPrice = Convert.ToDecimal(rows["AmorDeprM"]);
                                //计算本期期末净值
                                CurrentNetValue = EndNetValue - MonthDeprPrice - CurrValueRe;
                                //本期期末净值
                                DeprDetailmodel.EndNetValue = CurrentNetValue;
                                //月折旧额
                                DeprDetailmodel.MDeprPrice = MonthDeprPrice;
                                //计算累计折旧额
                                CountTotalDeprPrice = Convert.ToDecimal(rows["TotalDeprPrice"]) + MonthDeprPrice;
                                DeprDetailmodel.TotalDeprPrice = CountTotalDeprPrice;
                                //累计减值
                                DeprDetailmodel.TotalImpairment = (ReduValueRe + CurrValueRe);
                                //计算月折旧额
                                //如果本期净值大于净残值则更新
                                if (CurrentNetValue > Yjcz)
                                {
                                    #region 添加固定资产折旧前信息实体
                                    FixDeprAftermodel = new FixAssetDeprAfterModel();
                                    FixDeprAftermodel.AmorDeprM = rows["AmorDeprM"] != DBNull.Value ? Convert.ToDecimal(rows["AmorDeprM"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.AmorDeprRate = Convert.ToDecimal(rows["AmorDeprRate"]); ;
                                    FixDeprAftermodel.CompanyCD = CompanyCD;
                                    FixDeprAftermodel.EndNetValue = rows["EndNetValue"] != DBNull.Value ? Convert.ToDecimal(rows["EndNetValue"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.FixNo = rows["FixNo"].ToString();
                                    FixDeprAftermodel.PeriodNum = Convert.ToInt32(PeriodNum);
                                    FixDeprAftermodel.TotalDeprPrice = rows["TotalDeprPrice"] != DBNull.Value ? Convert.ToDecimal(rows["TotalDeprPrice"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAfterList.Add(FixDeprAftermodel);
                                    #endregion


                                    DeprPeriodmodel = new FixAssetPeriodDeprModel();
                                    DeprPeriodmodel.CompanyCD = CompanyCD;
                                    DeprPeriodmodel.EndNetValue = Math.Round(CurrentNetValue, 2);
                                    DeprPeriodmodel.FixNo = rows["FixNo"].ToString();
                                    DeprPeriodmodel.AmorDeprRate = 0;
                                    DeprPeriodmodel.AmorDeprM = MonthDeprPrice;
                                    DeprPeriodmodel.TotalDeprPrice = CountTotalDeprPrice;

                                    DeprDetailList.Add(DeprDetailmodel);
                                    DeprPeriodList.Add(DeprPeriodmodel);
                                }
                                else
                                {

                                    #region 添加固定资产折旧前信息实体
                                    FixDeprAftermodel = new FixAssetDeprAfterModel();
                                    FixDeprAftermodel.AmorDeprM = rows["AmorDeprM"] != DBNull.Value ? Convert.ToDecimal(rows["AmorDeprM"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.AmorDeprRate = Convert.ToDecimal(rows["AmorDeprRate"]); 
                                    FixDeprAftermodel.CompanyCD = CompanyCD;
                                    FixDeprAftermodel.EndNetValue = rows["EndNetValue"] != DBNull.Value ? Convert.ToDecimal(rows["EndNetValue"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.FixNo = rows["FixNo"].ToString();
                                    FixDeprAftermodel.PeriodNum = Convert.ToInt32(PeriodNum);
                                    FixDeprAftermodel.TotalDeprPrice = rows["TotalDeprPrice"] != DBNull.Value ? Convert.ToDecimal(rows["TotalDeprPrice"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAfterList.Add(FixDeprAftermodel);
                                    #endregion


                                    DeprPeriodmodel = new FixAssetPeriodDeprModel();
                                    DeprPeriodmodel.CompanyCD = CompanyCD;
                                    DeprPeriodmodel.EndNetValue = Yjcz;
                                    DeprPeriodmodel.FixNo = rows["FixNo"].ToString();
                                    DeprPeriodmodel.AmorDeprRate = 0;
                                    DeprPeriodmodel.AmorDeprM = MonthDeprPrice;
                                    DeprPeriodmodel.TotalDeprPrice = Convert.ToDecimal(rows["EndNetValue"]) - Yjcz; 

                                    DeprDetailList.Add(DeprDetailmodel);
                                    DeprPeriodList.Add(DeprPeriodmodel);
                                }
                            }
                            //工作量法
                            else if (CountMethod == ConstUtil.ASSETCOUNT_METHOD_GZLF_CODE)
                            {
                                //读取月折旧额
                                MonthDeprPrice = Convert.ToDecimal(rows["AmorDeprM"]);
                                //计算本期期末净值
                                CurrentNetValue = EndNetValue - MonthDeprPrice - CurrValueRe;
                                //计算累计折旧额
                                CountTotalDeprPrice = Convert.ToDecimal(rows["TotalDeprPrice"]) + MonthDeprPrice;
                                //如果本期净值大于原净值则不更新
                                if (CurrentNetValue > Yjcz)
                                {
                                    #region 添加固定资产折旧前信息实体
                                    FixDeprAftermodel = new FixAssetDeprAfterModel();
                                    FixDeprAftermodel.AmorDeprM = rows["AmorDeprM"] != DBNull.Value ? Convert.ToDecimal(rows["AmorDeprM"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.AmorDeprRate = Convert.ToDecimal(rows["AmorDeprRate"]); ;
                                    FixDeprAftermodel.CompanyCD = CompanyCD;
                                    FixDeprAftermodel.EndNetValue = rows["EndNetValue"] != DBNull.Value ? Convert.ToDecimal(rows["EndNetValue"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.FixNo = rows["FixNo"].ToString();
                                    FixDeprAftermodel.PeriodNum = Convert.ToInt32(PeriodNum);
                                    FixDeprAftermodel.TotalDeprPrice = rows["TotalDeprPrice"] != DBNull.Value ? Convert.ToDecimal(rows["TotalDeprPrice"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAfterList.Add(FixDeprAftermodel);
                                    #endregion


                                    DeprPeriodmodel = new FixAssetPeriodDeprModel();
                                    DeprPeriodmodel.CompanyCD = CompanyCD;
                                    DeprPeriodmodel.EndNetValue = Math.Round(CurrentNetValue, 2);
                                    DeprPeriodmodel.FixNo = rows["FixNo"].ToString();
                                    DeprPeriodmodel.AmorDeprRate = 0;
                                    DeprPeriodmodel.AmorDeprM = MonthDeprPrice;
                                    DeprPeriodmodel.TotalDeprPrice = CountTotalDeprPrice;

                                    DeprDetailList.Add(DeprDetailmodel);
                                    DeprPeriodList.Add(DeprPeriodmodel);

                                }
                                else
                                {
                                    #region 添加固定资产折旧前信息实体
                                    FixDeprAftermodel = new FixAssetDeprAfterModel();
                                    FixDeprAftermodel.AmorDeprM = rows["AmorDeprM"] != DBNull.Value ? Convert.ToDecimal(rows["AmorDeprM"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.AmorDeprRate = Convert.ToDecimal(rows["AmorDeprRate"]); ;
                                    FixDeprAftermodel.CompanyCD = CompanyCD;
                                    FixDeprAftermodel.EndNetValue = rows["EndNetValue"] != DBNull.Value ? Convert.ToDecimal(rows["EndNetValue"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.FixNo = rows["FixNo"].ToString();
                                    FixDeprAftermodel.PeriodNum = Convert.ToInt32(PeriodNum);
                                    FixDeprAftermodel.TotalDeprPrice = rows["TotalDeprPrice"] != DBNull.Value ? Convert.ToDecimal(rows["TotalDeprPrice"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAfterList.Add(FixDeprAftermodel);
                                    #endregion


                                    DeprPeriodmodel = new FixAssetPeriodDeprModel();
                                    DeprPeriodmodel.CompanyCD = CompanyCD;
                                    DeprPeriodmodel.EndNetValue = Yjcz;
                                    DeprPeriodmodel.FixNo = rows["FixNo"].ToString();
                                    DeprPeriodmodel.AmorDeprRate = 0;
                                    DeprPeriodmodel.AmorDeprM = MonthDeprPrice;
                                    DeprPeriodmodel.TotalDeprPrice = Convert.ToDecimal(rows["EndNetValue"]) - Yjcz;

                                    DeprDetailList.Add(DeprDetailmodel);
                                    DeprPeriodList.Add(DeprPeriodmodel);

                                }

                            }//年限总和法
                            else if (CountMethod == ConstUtil.ASSETCOUNT_METHOD_NXZHF_CODE)
                            {
                                //年折旧率
                                decimal YearDeprRate = 0;
                                //读取预计使用年限
                                EstimateUseYear = Convert.ToInt32(rows["EstimateUse"]);
                                //读取已使用年限
                                int UsedYears = Convert.ToInt32(rows["UsedYear"]);
                                //计算年折旧率
                                YearDeprRate = Convert.ToDecimal((EstimateUseYear - UsedYears)) / (Convert.ToDecimal(EstimateUseYear) * (Convert.ToDecimal(EstimateUseYear + 1)) / 2) * (100 / 100);
                                //计算月折旧率
                                MonthDeprRate = YearDeprRate / 12;
                                //计算月折旧额
                                MonthDeprPrice = (OriginalValue - (OriginalValue * (EstiResiValue/100))) * MonthDeprRate;
                                //计算本期期末净值
                                CurrentNetValue = EndNetValue - MonthDeprPrice - CurrValueRe;
                                //计算累计折旧额
                                CountTotalDeprPrice = Convert.ToDecimal(rows["TotalDeprPrice"]) + MonthDeprPrice;
                                if (CurrentNetValue > Yjcz)
                                {
                                    #region 添加固定资产折旧前信息实体
                                    FixDeprAftermodel = new FixAssetDeprAfterModel();
                                    FixDeprAftermodel.AmorDeprM = rows["AmorDeprM"] != DBNull.Value ? Convert.ToDecimal(rows["AmorDeprM"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.AmorDeprRate = Convert.ToDecimal(rows["AmorDeprRate"]); ;
                                    FixDeprAftermodel.CompanyCD = CompanyCD;
                                    FixDeprAftermodel.EndNetValue = rows["EndNetValue"] != DBNull.Value ? Convert.ToDecimal(rows["EndNetValue"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.FixNo = rows["FixNo"].ToString();
                                    FixDeprAftermodel.PeriodNum = Convert.ToInt32(PeriodNum);
                                    FixDeprAftermodel.TotalDeprPrice = rows["TotalDeprPrice"] != DBNull.Value ? Convert.ToDecimal(rows["TotalDeprPrice"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAfterList.Add(FixDeprAftermodel);
                                    #endregion


                                    DeprPeriodmodel = new FixAssetPeriodDeprModel();
                                    DeprPeriodmodel.CompanyCD = CompanyCD;
                                    DeprPeriodmodel.EndNetValue = Math.Round(CurrentNetValue, 2);
                                    DeprPeriodmodel.FixNo = rows["FixNo"].ToString();
                                    DeprPeriodmodel.AmorDeprRate = Math.Round(MonthDeprRate, 8);
                                    DeprPeriodmodel.AmorDeprM = Math.Round(MonthDeprPrice, 2);
                                    DeprPeriodmodel.TotalDeprPrice = CountTotalDeprPrice;

                                    DeprDetailList.Add(DeprDetailmodel);
                                    DeprPeriodList.Add(DeprPeriodmodel);
                                }
                                else
                                {

                                    #region 添加固定资产折旧前信息实体
                                    FixDeprAftermodel = new FixAssetDeprAfterModel();
                                    FixDeprAftermodel.AmorDeprM = rows["AmorDeprM"] != DBNull.Value ? Convert.ToDecimal(rows["AmorDeprM"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.AmorDeprRate = Convert.ToDecimal(rows["AmorDeprRate"]); ;
                                    FixDeprAftermodel.CompanyCD = CompanyCD;
                                    FixDeprAftermodel.EndNetValue = rows["EndNetValue"] != DBNull.Value ? Convert.ToDecimal(rows["EndNetValue"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.FixNo = rows["FixNo"].ToString();
                                    FixDeprAftermodel.PeriodNum = Convert.ToInt32(PeriodNum);
                                    FixDeprAftermodel.TotalDeprPrice = rows["TotalDeprPrice"] != DBNull.Value ? Convert.ToDecimal(rows["TotalDeprPrice"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAfterList.Add(FixDeprAftermodel);
                                    #endregion



                                    DeprPeriodmodel = new FixAssetPeriodDeprModel();
                                    DeprPeriodmodel.CompanyCD = CompanyCD;
                                    DeprPeriodmodel.EndNetValue = Yjcz;
                                    DeprPeriodmodel.FixNo = rows["FixNo"].ToString();
                                    DeprPeriodmodel.AmorDeprRate = Math.Round(MonthDeprRate, 8);
                                    DeprPeriodmodel.AmorDeprM = MonthDeprPrice;
                                    DeprPeriodmodel.TotalDeprPrice = Convert.ToDecimal(rows["EndNetValue"]) - Yjcz;

                                    DeprDetailList.Add(DeprDetailmodel);
                                    DeprPeriodList.Add(DeprPeriodmodel);


                                }

                            }//双倍余额递减法
                            else if (CountMethod == ConstUtil.ASSETCOUNT_METHOD_SBYETJS_CODE)
                            {
                                //读取预计使用年限
                                EstimateUseYear = Convert.ToInt32(rows["EstimateUse"]);
                                //年折旧率
                                decimal YearDeprRate = 0;
                                //计算年折旧率
                                YearDeprRate = 2 / Convert.ToDecimal(EstimateUseYear) * (100 / 100);
                                //计算月折旧率
                                MonthDeprRate = YearDeprRate / 12;
                                //计算月折旧额
                                MonthDeprPrice = EndNetValue * MonthDeprRate;
                                //计算本期期末净值
                                CurrentNetValue = EndNetValue - MonthDeprPrice - CurrValueRe;
                                //计算累计折旧额
                                CountTotalDeprPrice = Convert.ToDecimal(rows["TotalDeprPrice"]) + MonthDeprPrice;
                                if (CurrentNetValue > Yjcz)
                                {
                                    #region 添加固定资产折旧前信息实体
                                    FixDeprAftermodel = new FixAssetDeprAfterModel();
                                    FixDeprAftermodel.AmorDeprM = rows["AmorDeprM"] != DBNull.Value ? Convert.ToDecimal(rows["AmorDeprM"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.AmorDeprRate = Convert.ToDecimal(rows["AmorDeprRate"]); ;
                                    FixDeprAftermodel.CompanyCD = CompanyCD;
                                    FixDeprAftermodel.EndNetValue = rows["EndNetValue"] != DBNull.Value ? Convert.ToDecimal(rows["EndNetValue"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.FixNo = rows["FixNo"].ToString();
                                    FixDeprAftermodel.PeriodNum = Convert.ToInt32(PeriodNum);
                                    FixDeprAftermodel.TotalDeprPrice = rows["TotalDeprPrice"] != DBNull.Value ? Convert.ToDecimal(rows["TotalDeprPrice"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAfterList.Add(FixDeprAftermodel);
                                    #endregion


                                    DeprPeriodmodel = new FixAssetPeriodDeprModel();
                                    DeprPeriodmodel.CompanyCD = CompanyCD;
                                    DeprPeriodmodel.EndNetValue = Math.Round(CurrentNetValue, 2);
                                    DeprPeriodmodel.FixNo = rows["FixNo"].ToString();
                                    DeprPeriodmodel.AmorDeprRate = Math.Round(MonthDeprRate, 8);
                                    DeprPeriodmodel.AmorDeprM = Math.Round(MonthDeprPrice, 2);
                                    DeprPeriodmodel.TotalDeprPrice = CountTotalDeprPrice;
                                    DeprDetailList.Add(DeprDetailmodel);
                                    DeprPeriodList.Add(DeprPeriodmodel);
                                }
                                else
                                {

                                    #region 添加固定资产折旧前信息实体
                                    FixDeprAftermodel = new FixAssetDeprAfterModel();
                                    FixDeprAftermodel.AmorDeprM = rows["AmorDeprM"] != DBNull.Value ? Convert.ToDecimal(rows["AmorDeprM"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.AmorDeprRate = Convert.ToDecimal(rows["AmorDeprRate"]); ;
                                    FixDeprAftermodel.CompanyCD = CompanyCD;
                                    FixDeprAftermodel.EndNetValue = rows["EndNetValue"] != DBNull.Value ? Convert.ToDecimal(rows["EndNetValue"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAftermodel.FixNo = rows["FixNo"].ToString();
                                    FixDeprAftermodel.PeriodNum = Convert.ToInt32(PeriodNum);
                                    FixDeprAftermodel.TotalDeprPrice = rows["TotalDeprPrice"] != DBNull.Value ? Convert.ToDecimal(rows["TotalDeprPrice"]) : Convert.ToDecimal(DBNull.Value);
                                    FixDeprAfterList.Add(FixDeprAftermodel);
                                    #endregion



                                    DeprPeriodmodel = new FixAssetPeriodDeprModel();
                                    DeprPeriodmodel.CompanyCD = CompanyCD;
                                    DeprPeriodmodel.EndNetValue = Yjcz;
                                    DeprPeriodmodel.FixNo = rows["FixNo"].ToString();
                                    DeprPeriodmodel.AmorDeprRate = Math.Round(MonthDeprRate, 8);
                                    DeprPeriodmodel.AmorDeprM = MonthDeprPrice;
                                    DeprPeriodmodel.TotalDeprPrice = Convert.ToDecimal(rows["EndNetValue"]) - Yjcz;

                                    DeprDetailList.Add(DeprDetailmodel);
                                    DeprPeriodList.Add(DeprPeriodmodel);
                                }
                            }
                        }
                    }
                    #endregion
                    //折旧固定资产明细
                    result = FixAssetInfoDBHelper.UpdateEndFixAssetInfo(DeprDetailList, DeprPeriodList, CompanyCD, PeriodNum, 
                        ItemID, ref PeriodID, FixDeprAfterList);
                }
                if (result)
                {
                    #region 凭证单据明细实体
                    //凭证单据实体
                    AttestBillModel Attestmodel = new AttestBillModel();
                    //凭证明细实体定义
                    AttestBillDetailsModel Detailmodel = null;
                    #endregion

                    #region 凭证单据实体赋值
                    Attestmodel.CompanyCD = CompanyCD;
                    Attestmodel.AttestNo = "记-" + VoucherDBHelper.GetMaxAttestNo(CompanyCD, DateTime.Now.ToString());
                    getAttestNo = Attestmodel.AttestNo;
                    Attestmodel.AttestName = "记账凭证";
                    Attestmodel.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                    Attestmodel.status = 0;
                    Attestmodel.FromTbale = "EndItemProcessedRecord,FixWithInfo,FixPeriodDeprDetails";
                    Attestmodel.FromValue=PeriodID.ToString()+","+PeriodNum.ToString();
                    Attestmodel.VoucherDate =Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    #endregion
                    int ID;
                    ArrayList modelList = new ArrayList();//定义数组存储实体对象
                    //添加凭证单据信息
                    result = FixAssetInfoDBHelper.BuildDepreAttestBill(Attestmodel, out ID, ItemID, PeriodNum);
                    AttestID = ID;
                    if (result && ID > 0)
                    {
                        //获取固定资产科目汇总金额信息
                        DataTable AttestDetail = FixAssetInfoDBHelper.GetBuildAttestInfo(CompanyCD);
                        //获取折旧凭证摘要ID
                        string AbstractID;
                        AbstractID = ConstUtil.SUMMARY_ZCJZ_NAME;
                        #endregion
                        //获取本币币种ID
                        int MasterCurrencyID = Convert.ToInt32(CurrTypeSettingDBHelper.GetMasterCurrency(CompanyCD).Rows[0]["ID"]);
                        if (AttestDetail != null && AttestDetail.Rows.Count > 0)
                        {
                            foreach (DataRow drows in AttestDetail.Rows)
                            {
                                #region 凭证明细实体赋值
                                Detailmodel = new AttestBillDetailsModel();
                                Detailmodel.AttestBillID = ID;
                                Detailmodel.Abstract = AbstractID;// 摘要
                                Detailmodel.SubjectsCD = drows["AccuDeprSubjeCD"].ToString();//累计折旧科目 贷
                                Detailmodel.CreditAmount = Convert.ToDecimal(drows["CountSum"]);//贷方金额
                                Detailmodel.DebitAmount = 0;
                                Detailmodel.OriginalAmount = Convert.ToDecimal(drows["CountSum"]);//外币金额
                                Detailmodel.CurrencyTypeID = MasterCurrencyID;//本币ID
                                Detailmodel.ExchangeRate = 1;//本币汇率
                                modelList.Add(Detailmodel);


                                Detailmodel = new AttestBillDetailsModel();
                                Detailmodel.AttestBillID = ID;
                                Detailmodel.Abstract = AbstractID;// 摘要
                                Detailmodel.CreditAmount = 0;
                                Detailmodel.SubjectsCD = drows["DeprCostSubjeCD"].ToString();//折旧费用科目 借
                                Detailmodel.DebitAmount = Convert.ToDecimal(drows["CountSum"]);//借方金额
                                Detailmodel.OriginalAmount = Convert.ToDecimal(drows["CountSum"]);//外币金额
                                Detailmodel.CurrencyTypeID = MasterCurrencyID;//本币ID
                                Detailmodel.ExchangeRate = 1;//本币汇率
                                modelList.Add(Detailmodel);
                                #endregion
                            }
                            //凭证自动登帐及自动审核
                            result = FixAssetInfoDBHelper.BuildDepreDetailInfo(modelList);
                            if (result)
                            {
                                 VoucherBus.SetStatus(ID.ToString(), "1", "status", 0);
                                 VoucherBus.InsertAccount(AttestID);
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 

        #region  添加固定资产信息及计提信息
        /// <summary>
        /// 添加固定资产信息及计提信息
        /// </summary>
        /// <param name="FixInfoModel">固定资产实体</param>
        /// <param name="FixWithModel">固定资产计提实体</param>
        /// <returns>true 成功，false 失败</returns>
        public static bool InsertFixAssetInfo(FixAssetInfoModel FixInfoModel, FixWithInfoModel FixWithModel)
        {
            if (FixInfoModel == null && FixWithModel == null) return false;
            if (FixInfoModel.CompanyCD == null) FixInfoModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            if (FixInfoModel.ModifiedUserID == null) FixInfoModel.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            try
            {
                return FixAssetInfoDBHelper.InsertFixAssetInfo(FixInfoModel, FixWithModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 修改固定资产信息及计提信息
        /// <summary>
        /// 修改固定资产信息及计提信息
        /// </summary>
        /// <param name="FixInfoModel">固定资产实体</param>
        /// <param name="FixWithModel">固定资产计提实体</param>
        /// <returns>true 成功，false 失败</returns>
        public static bool UpdateFixAssetInfo(FixAssetInfoModel FixInfoModel, FixWithInfoModel FixWithModel)
        {
            if (FixInfoModel == null && FixWithModel == null) return false;
            if (FixInfoModel.CompanyCD == null) FixInfoModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            if (FixInfoModel.ModifiedUserID == null) FixInfoModel.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            try
            {
                return FixAssetInfoDBHelper.UpdateFixAssetInfo(FixInfoModel, FixWithModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除固定资产信息
        /// <summary>
        /// 删除固定资产信息
        /// </summary>
        /// <param name="MID">资产信息主键</param>
        /// <param name="DID">计提信息主键</param>
        /// <returns>true 成功，false 失败</returns>
        public static bool DeleteFixAssetInfo(string FixNo)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {

                //执行删除操作
                bool isSucc = FixAssetInfoDBHelper.DeleteFixAssetInfo(CompanyCD, FixNo);

                ////定义变量
                //string remark;
                ////成功时
                //if (isSucc)
                //{
                //    //设置操作成功标识
                //    remark = ConstUtil.LOG_PROCESS_SUCCESS;
                //}
                //else
                //{
                //    //设置操作成功标识 
                //    remark = ConstUtil.LOG_PROCESS_FAILED;
                //}
                ////获取删除的编号列表
                //string[] noList = FixNo.Split(',');
                ////遍历所有编号，登陆操作日志
                //for (int i = 0; i < noList.Length; i++)
                //{
                //    //获取编号
                //    string no = noList[i];
                //    //替换两边的 '
                //    no = no.Replace("'", string.Empty);

                //    //操作日志
                //    LogInfoModel logModel = InitLogInfo(no);
                //    //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                //    logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                //    //设置操作成功标识
                //    logModel.Remark = remark;

                //    //登陆日志
                //    LogDBHelper.InsertLog(logModel);
                //}
                return isSucc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <returns></returns>
        private static LogInfoModel InitLogInfo(string no)
        {
            LogInfoModel logModel = new LogInfoModel();

            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID
            logModel.ModuleID = ConstUtil.MODULE_ID_FINANCEMANAGER_FIX_LIST;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_FIX + ConstUtil.CODING_RULE_TABLE_FIXWITHINFO;
            //操作单据编号
            logModel.ObjectID = no;

            return logModel;

        }
        #endregion


        #region 获取固定资产信息及计提信息_打印
        /// <summary>
        /// 获取固定资产信息及计提信息_打印
        /// </summary>
        /// <returns>DataSet</returns>
        public static DataTable GetFixiInfoByNO(string CompanyCD, string FixNO)
        {
            if (string.IsNullOrEmpty(FixNO)) return null;
            try
            {
                return FixAssetInfoDBHelper.GetFixiInfoByNO(CompanyCD,FixNO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 获取固定资产信息及计提信息
        /// <summary>
        /// 获取固定资产信息及计提信息
        /// </summary>
        /// <returns>DataSet</returns>
        public static DataSet GetFixiInfo(string ID)
        {
            if (string.IsNullOrEmpty(ID)) return null;
            try
            {
                return FixAssetInfoDBHelper.GetFixiInfo(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据企业编码获取企业固定资产信息
        /// <summary>
        /// 根据企业编码获取企业固定资产信息
        /// </summary>
        /// <param name="CompanyCD">企业编码</param>
        /// <returns>DataTable</returns>
        public static DataTable SearchFixAssetInfo(string FixNo,
        string FixName, string Type, string FixStatus, string ZJSubjectsCD, string DeptID,
        string StartDate, string EndDate)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    string DayEnd = "";
                    StartDate +="-01";
                    //对输入的资料日期进行处理开始

                    int yearString = Convert.ToInt32(EndDate.Split('-')[0].ToString());
                    int MothString = Convert.ToInt32(EndDate.Split('-')[1].ToString());
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
                    EndDate += DayEnd;
                }
                //获取固定资产的查询结果集
                DataTable dt= FixAssetInfoDBHelper.SearchFixAssetInfo(CompanyCD, FixNo, FixName, 
                    Type, FixStatus, ZJSubjectsCD, DeptID, StartDate, EndDate);
                //获取资产累计折旧汇总结果集
                DataTable Hzdt = FixAssetInfoDBHelper.CountFixTotalZJE(CompanyCD);
                //创建折旧总金额列
                dt.Columns.Add("TotalDeprPrice");

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow rows in dt.Rows)
                    {
                        string expression;
                        expression = "FixNo= '" + rows["FixNo"].ToString() + "'";
                        DataRow[] FoundRows;
                        // Use the Select method to find all rows matching the filter.
                        FoundRows = Hzdt.Select(expression);
                        if (FoundRows.Length > 0)
                        {
                            rows["TotalDeprPrice"] = FoundRows[0]["TotalDeprPrice"].ToString() == "" || FoundRows[0]["TotalDeprPrice"].ToString() == null ? "" : Convert.ToDecimal(FoundRows[0]["TotalDeprPrice"].ToString()).ToString("#,###0.#0");
                        }
                        else
                        {
                             rows["TotalDeprPrice"] = "";
                        }
                    } 
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询车辆信息列表
        /// <summary>
        /// 查询车辆信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCarInfoList(string CarNo, string CarName, string CarMark, string CarType, string CompanyID)
        {
            try
            {
                return FixAssetInfoDBHelper.GetCarInfoList(CarNo, CarName, CarMark, CarType, CompanyID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取设备信息
        public static DataTable GetEquipmentInfo(string EquipmentCD, string EquipmentName,
            string EquipmentType, string UsedStatus, string StartDate,string EndDate)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return FixAssetInfoDBHelper.GetEquipmentInfo(CompanyCD, EquipmentCD, EquipmentName,
                    EquipmentType, UsedStatus, StartDate, EndDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static DataSet ReadEexcel(string FilePath, string companycd)
        {
            return FixAssetInfoDBHelper.ReadEexcel(FilePath, companycd);
        }

        public static bool ChargeAssetTypeInfo(string codename, string compid)
        {
            return FixAssetInfoDBHelper.ChargeAssetTypeInfo(codename, compid);
        }

        public static bool ChargeFixAssetInfo(string codename, string compid)
        {
            return FixAssetInfoDBHelper.ChargeFixAssetInfo(codename, compid);
        }

        public static int GetExcelToFixAssetInfo(string companycd, string usercode)
        {
            return FixAssetInfoDBHelper.GetExcelToFixAssetInfo(companycd, usercode);
        }

        public static DataSet GetNullFixAssetList(string companycd)
        {
            return FixAssetInfoDBHelper.GetNullFixAssetList(companycd);
        }

        public static int GetCodeRuleID(string companycd)
        {
            return FixAssetInfoDBHelper.GetCodeRuleID(companycd);
        }

        public static void UpdateFixAssetInfo(string companycd, string FixType, string ID)
        {
            FixAssetInfoDBHelper.UpdateFixAssetInfo(companycd, FixType, ID);
        }

        public static void InsertFixWithInfo()
        {
            FixAssetInfoDBHelper.InsertFixWithInfo();
        }

    }
}

  

    


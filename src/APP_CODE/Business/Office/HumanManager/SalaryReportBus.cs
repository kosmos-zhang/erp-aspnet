/**********************************************
 * 类作用：   工资报表
 * 建立人：   吴志强
 * 建立时间： 2009/05/20
 ***********************************************/
using System;
using System.Text;
using XBase.Model.Office.HumanManager;
using XBase.Data.Office.HumanManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;
using System.Collections;
using XBase.Business.Office.HumanManager;

namespace XBase.Business.Office.HumanManager
{
    /// <summary>
    /// 类名：SalaryReportBus
    /// 描述：工资报表
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/20
    /// 最后修改时间：2009/05/20
    /// </summary>
    ///
    public class SalaryReportBus
    {

        #region 校验所属月份的报表是否已经生成
        /// <summary>
        /// 校验所属月份的报表是否已经生成
        /// </summary>
        /// <param name="belongMonth">所属月份</param>
        /// <returns></returns>
        public static bool IsExsistReport(string belongMonth)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //执行查询
            return SalaryReportDBHelper.IsExsistReport(userInfo.CompanyCD, belongMonth);
        }
        #endregion

        #region 校验所属月份的报表是否已经生成
        /// <summary>
        /// 校验所属月份的报表是否已经生成
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="reprotNo">报表编号</param>
        /// <returns></returns>
        public static bool UpdateReportStatus(string status, string reprotNo)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //执行查询
            return SalaryReportDBHelper.UpdateReportStatus(status, userInfo.CompanyCD, reprotNo, userInfo.UserID);
        }
        #endregion
        
        #region 生成报表处理
        /// <summary>
        /// 生成报表处理
        /// </summary>
        /// <param name="belongMonth">所属月份</param>
        /// <returns></returns>
        public static string CreateSalaryReport(SalaryReportModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            string userID = userInfo.UserID;
            model.CompanyCD = companyCD;
            model.ModifiedUserID = userID;
            //获取人员信息
            EmployeeSearchModel searchEmplModel = new EmployeeSearchModel();
            //设置公司代码
            searchEmplModel.CompanyCD = companyCD;
            searchEmplModel.StartDate = model.StartDate;
            searchEmplModel.EndDate = model.EndDate;
            //查询人员信息
            DataTable dtEmplInfo = EmployeeInfoDBHelper.GetWorkEmplInfo(searchEmplModel);
            DataTable dtNew = SalaryItemBus.GetOutEmployeeInfo(searchEmplModel);
            dtEmplInfo.Merge(dtNew);
            //获取员工固定工资
            DataTable dtEmplSalary = SalaryEmployeeDBHelper.GetSalaryEmployeeInfo(companyCD);
            DataTable dtFixedSalary = new DataTable();
            dtFixedSalary = dtEmplSalary.Clone();
            //dtFixedSalary.Columns["SalaryMoney"].DataType.GetType() = "System.Decimal";
            //获取员工计件工资
            DataTable dtPeiceSalary = PieceworkSalaryDBHelper.GetMonthPieceworkSalary(companyCD, model.ReportMonth,model .StartDate ,model.EndDate );

            string spMonth = model.ReportMonth.Substring(0, 4) + "-" + model.ReportMonth.Substring(4);
            //获取员工出勤率
            DataTable dtAttendance = PieceworkSalaryDBHelper.GetMonthAttendance(companyCD,  spMonth );
            //获取员工计时工资
            DataTable dtTimeSalary = TimeSalaryDBHelper.GetMonthTimeSalary(companyCD, model.ReportMonth, model.StartDate, model.EndDate);
            //获取员工产品单品提成工资
            DataTable dtCommSalary = CommissionSalaryDBHelper.GetMonthCommSalary(companyCD, model.ReportMonth, model.StartDate, model.EndDate);

            //获取公司业务提成
            DataTable dtComanySalary = InputCompanyRoyaltyDBHelper.GetMonthCompanySalary(companyCD, model.ReportMonth, model.StartDate, model.EndDate);
            //获取公司业务提成设置
            DataTable dtCompanySet = InputCompanyRoyaltyDBHelper.GetCompanySetInfo(companyCD);
            //获取个人业务信息
            DataTable dtPersonCommSalary = InputPersonalRoyaltyDBHelper.GetMonthPersonSalary(companyCD, model.ReportMonth, model.StartDate, model.EndDate);
            

            //获取部门业务提成
            DataTable dtDeptSalary = InputDepatmentRoyaltyDBHelper.GetMonthDeptSalary (companyCD, model.ReportMonth, model.StartDate, model.EndDate);
            //获取部门业务提成设置
            DataTable dtDeptSet = InputDepatmentRoyaltyDBHelper.GetDeptSetInfo(companyCD); 


            //获取员工整体薪资结构
            DataTable dtStructure = SalaryEmployeeStructureSetDBHelper.GetSalaryStructure(companyCD);
            //获取绩效工资
            DataTable dtPerformanceSalary = InputPerformanceRoyaltyDBHelper.GetMonthPerformanceSalary (companyCD, model.ReportMonth, model.StartDate, model.EndDate);

            

            //获取社会保险信息
            DataTable dtInsuSalary = InsuEmployeeDBHelper.GetInsuEmployeeInf(companyCD,model.ReportMonth );
            //获取个人所得税信息
            DataTable dtPersonSalary = InputPersonTrueIncomeTaxDBHelper.SearchPersonTax (companyCD, model.ReportMonth);
            //变量定义
            ArrayList lstSummary = new ArrayList();

            //遍历所有的员工信息，获取工资总额
            for (int i = 0; i < dtEmplInfo.Rows.Count; i++)
            {
                //变量定义
                SalaryReportSummaryModel summaryModel = new SalaryReportSummaryModel();
                //变量定义
                decimal totalSalary = 0;
                decimal salaryMoney = 0;
                //公司代码
                summaryModel.CompanyCD = companyCD;
                //报表编号
                summaryModel.ReprotNo = model.ReprotNo;
                //获取员工ID
                string employeeID = GetSafeData.GetStringFromInt(dtEmplInfo.Rows[i], "ID");
                summaryModel.EmployeeID = employeeID;
                summaryModel.AdminLevelName = GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "AdminLevelName");
                summaryModel.DeptName = GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "DeptName");
                summaryModel.EmployeeName = GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "EmployeeName");
                summaryModel.EmployeeNo = GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "EmployeeNo");
                summaryModel.QuarterName = GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "QuarterName");
                string deptID = GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "DeptID");
                //获取员工薪资结构
                DataRow[] drStructure = dtStructure.Select("EmployeeID=" + employeeID);

                if (drStructure.Length > 0)
                {

                }
                else
                {
                    lstSummary.Add(summaryModel);
                    continue;
                }
                #region 判断是否设置固定工资
                if (GetSafeData.ValidateDataRow_String(drStructure[0], "IsFixSalarySet") == "1")//判断是否设置固定工资
                {
                    //固定工资
                    DataRow[] drFixedSalary = dtEmplSalary.Select("EmployeeID=" + employeeID);
                   
                    //遍历所有固定工资项，计算工资额
                    for (int j = 0; j < drFixedSalary.Length; j++)
                    {
                        //导入工资记录
                        dtFixedSalary.ImportRow(drFixedSalary[j]);
                        //该项工资额为DBNull时，增加该工资项
                        if (drFixedSalary[j]["SalaryMoney"] != DBNull.Value)
                        {
                            //获取是否扣款
                            string flag = GetSafeData.ValidateDataRow_String(drFixedSalary[j], "PayFlag");

                            string cal = GetSafeData.ValidateDataRow_String(drFixedSalary[j], "Calculate");  
                            decimal fina=0;
                            if (string.IsNullOrEmpty(cal))
                            {
                                fina =Convert.ToDecimal(drFixedSalary[j]["SalaryMoney"]);
                            }
                            else
                            {
                                while (cal.IndexOf('A') != -1)
                                {
                                    cal = cal.Replace('A', '+');
                                }
                                string numlist = GetSafeData.ValidateDataRow_String(drFixedSalary[j], "ParamsList");
                                string[] numberlist = numlist.Split(',');
                                string temp = cal;

                                for (int inde = 0; inde < numberlist.Length; inde++)
                                {
                                    if (numberlist[inde] == "@")
                                    {
               
                                      DataRow[] drtem = dtAttendance.Select("EmployeeID=" + employeeID);
                                      decimal atten=0;
                                        if (drtem .Length >0)
                                        {
                                           atten  = Math.Round(Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drtem[0], "AttendanceRate")) / 100, 2);
                                        }
                                       if (atten < 1)
                                       {

                                         
                                           cal = cal.Replace("{" + numberlist[inde] + "}", Convert.ToString(atten));
                                       }
                                       else
                                       {
                                           cal = cal.Replace("{" + numberlist[inde] + "}",  "0");
                                       }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(numberlist[inde]))
                                        {
                                            DataRow[] drtem = dtEmplSalary.Select("EmployeeID=" + employeeID + "  and  ItemNo=" + numberlist[inde]);


                                            if (drtem.Length > 0)
                                            {
                                                cal = cal.Replace("{" + numberlist[inde] + "}", GetSafeData.ValidateDataRow_String(drtem[0], "SalaryMoney"));
                                            }
                                            else
                                            {
                                                cal = cal.Replace("{" + numberlist[inde] + "}", "0");
                                            }
                                        }
                                    }



                                }

                                try
                                {
                      
                                 //   string result = CustomEval.eval(cal).ToString();
                              //      fina = Convert.ToDecimal(result);
                                
                                }
                                catch
                                {

                                    fina = 0;
                                }
                            }


                            if ("0".Equals(flag))
                            {



                                //增加工资额
                                totalSalary += fina;
                            }
                            else
                            {
                                //减少工资额
                                // totalSalary -= Convert.ToDecimal(drFixedSalary[j]["SalaryMoney"]);
                                salaryMoney += fina ;
                            }
                         
                            for (int a = 0; a < dtFixedSalary.Rows.Count; a++)
                            {
                                string empl=dtFixedSalary .Rows [a]["EmployeeID"]==null ?"":dtFixedSalary .Rows [a]["EmployeeID"].ToString ();
                                string iten=dtFixedSalary .Rows [a]["ItemNo"]==null ?"":dtFixedSalary .Rows [a]["ItemNo"].ToString ();
                                string organItem=drFixedSalary[j]["ItemNo"] ==null ?"":drFixedSalary[j]["ItemNo"] .ToString ();
                                if ((empl == employeeID) && ( iten == organItem))
                                {
                                    dtFixedSalary.Rows[a]["SalaryMoney"] = fina.ToString();
                                }
                            }


                        }
                    }


                    //固定工资
                    summaryModel.FixedMoney = totalSalary.ToString();
                }
                else
                {
                    //固定工资
                    summaryModel.FixedMoney =  "0";
                }

#endregion
                #region 判断是否设置计件
                if (GetSafeData.ValidateDataRow_String(drStructure[0], "IsPieceWorkSet") == "1")//判断是否设置计件
                {
                    //计件工资
                    DataRow[] drPeiceSalary = dtPeiceSalary.Select("EmployeeID=" + employeeID);
                    if (drPeiceSalary.Length > 0)
                    {
                        //设置计件工资
                        summaryModel.WorkMoney = GetSafeData.GetStringFromDecimal(drPeiceSalary[0], "TotalSalary");
                        //加到总工资
                        totalSalary += Convert.ToDecimal(drPeiceSalary[0]["TotalSalary"]);

                    }
                }
                else
                {
                    //设置计件工资
                    summaryModel.WorkMoney = "0";
                    //加到总工资
                    totalSalary +=   0;
                }
                #endregion
                #region 判断是否设置计时
                if (GetSafeData.ValidateDataRow_String(drStructure[0], "IsTimeWorkSet") == "1")//判断是否设置计时
                {
                    //计时工资
                    DataRow[] drTimeSalary = dtTimeSalary.Select("EmployeeID=" + employeeID);
                    if (drTimeSalary.Length > 0)
                    {
                        //设置计时工资
                        summaryModel.TimeMoney = GetSafeData.GetStringFromDecimal(drTimeSalary[0], "TotalSalary");
                        totalSalary += Convert.ToDecimal(drTimeSalary[0]["TotalSalary"]);
                    }
                }
                else
                {
                    //设置计时工资
                    summaryModel.TimeMoney =  "0";
                    totalSalary +=  0;


                }
                #endregion
                
             
                #region 判断是否设置产品单品提成
                if (GetSafeData.ValidateDataRow_String(drStructure[0], "IsProductRoyaltySet") == "1")//判断是否设置产品单品提成
                {
                    //提成工资  dtPersonSalary
                    DataRow[] drCommSalary = dtCommSalary.Select("EmployeeID=" + employeeID);
                    if (drCommSalary.Length > 0)
                    {
                        //设置产品单品提成工资
                        //summaryModel.CommissionMoney = GetSafeData.GetStringFromDecimal(drCommSalary[0], "TotalSalary");Convert.ToDecimal(drCommSalary[0]["TotalSalary"]);
                        summaryModel.CommissionMoney = GetSafeData.GetStringFromDecimal(drCommSalary[0], "TotalSalary");   
                        totalSalary += Convert.ToDecimal(drCommSalary[0]["TotalSalary"]);
                    }
                }
                else
                {
                    //设置产品单品提成工资
                    summaryModel.CommissionMoney =  "0";
                    //commisionMoney = 0;
                    totalSalary +=  0;
                }
                #endregion

                 #region 判断是否设置个人业务提成
                if (GetSafeData.ValidateDataRow_String(drStructure[0], "IsPersonalRoyaltySet") == "1")//判断是否设置个人业务提成
                {
                    // 
                    DataRow[] drPersonCommSalary = dtPersonCommSalary.Select("EmployeeID=" + employeeID);
                    if (drPersonCommSalary.Length > 0)
                    {
                        //设置个人业务提成
                        summaryModel.PersonComMoney = GetSafeData.GetStringFromDecimal(drPersonCommSalary[0], "TotalSalary");
                       
                        totalSalary += Convert.ToDecimal(drPersonCommSalary[0]["TotalSalary"]);
                    }
                }
                else
                {
                    //设置个人业务提成
                    //summaryModel.CommissionMoney =  "0";
                    summaryModel.PersonComMoney ="0";
                    totalSalary +=  0;
                }
                #endregion
                #region 判断是否设置公司业务提成
                if (GetSafeData.ValidateDataRow_String(drStructure[0], "IsCompanyRoyaltySet") == "1")//判断是否设置公司业务提成
                {
                    decimal rate = 0;
                    //提成工资  dtPersonSalary dtComanySalary
                    DataRow[] drCompanyalary = dtComanySalary.Select("DeptID=" + deptID);
                    DataRow[] drCompanyalarySet = dtCompanySet.Select("DeptID=" + deptID, "MiniMoney  asc");
                    DataRow[] drCompanyalarySetCommon = dtCompanySet.Select("DeptID=0", "MiniMoney  asc");//获取通用规则
                    if (drCompanyalary.Length > 0)
                    {
                             string tem=   GetSafeData.ValidateDataRow_String(drCompanyalary[0], "TotalSalary");
                        decimal temp=Convert .ToDecimal (tem );
                        bool isCheck = false;
                             if (drCompanyalarySet.Length > 0)
                             {  
                                 for (int a = 0; a < drCompanyalarySet.Length; a++)
                                 {
                                     decimal minMoney = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drCompanyalarySet[a], "MiniMoney"));
                                     decimal maxMoney = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drCompanyalarySet[a], "MaxMoney"));
                                      if (temp >= minMoney && temp <maxMoney)
                                      {
                                          rate = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drCompanyalarySet[a], "TaxPercent"));
                                          isCheck = true;
                                          break;
                                      }
                                 }

                                 if (!isCheck)
                                 {
                                     rate = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drCompanyalarySet[(drCompanyalarySet.Length - 1)], "TaxPercent"));
                                 }
                             }
                             else if (drCompanyalarySetCommon.Length > 0)
                             {
                                 bool isHave = false;
                                 int len=drCompanyalarySetCommon.Length;
                                 for (int a = 0; a <len  ; a++)
                                 {
                                     decimal minMoney = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drCompanyalarySetCommon[a], "MiniMoney"));
                                     decimal maxMoney = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drCompanyalarySetCommon[a], "MaxMoney"));
                                     if (temp >= minMoney && temp < maxMoney)
                                     {
                                         rate = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drCompanyalarySetCommon[a], "TaxPercent"));
                                         isHave = true;
                                         break;
                                     }
                                 }
                                 if (!isHave)
                                 {
                                     rate = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drCompanyalarySetCommon[(len -1)], "TaxPercent"));
                                 }

                             }

                             summaryModel .CompanyComMoney  = Convert .ToString ( decimal.Round(temp * rate * (Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drStructure[0], "CompanyRatePercent"))) / 10000, 2));
                             totalSalary += decimal.Round(temp * rate * (Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drStructure[0], "CompanyRatePercent"))) / 10000, 2);
                    }
                }
                else
                {

                    summaryModel.CompanyComMoney = "0";
                    totalSalary += 0;
                }
                #endregion
                   
                #region 判断是否设置部门业务提成
                if (GetSafeData.ValidateDataRow_String(drStructure[0], "IsDeptRoyaltySet") == "1")//判断是否设置部门业务提成
                {
                    decimal rate = 0;
                    //提成工资  dtPersonSalary dtComanySalary
                    DataRow[] drDeptalary = dtDeptSalary.Select("DeptID=" + deptID);
                    DataRow[] drDeptalarySet = dtDeptSet.Select("DeptID=" + deptID, "MiniMoney  asc");
                    DataRow[] drDeptalarySetCommon = dtDeptSet.Select("DeptID=0", "MiniMoney  asc");//获取通用规则
                    if (drDeptalary.Length > 0)
                    {
                        string tem = GetSafeData.ValidateDataRow_String(drDeptalary[0], "TotalSalary");
                        decimal temp = Convert.ToDecimal(tem);
                        bool isCheck = false;
                        if (drDeptalarySet.Length > 0)
                        {
                            for (int a = 0; a < drDeptalarySet.Length; a++)
                            {
                                decimal minMoney = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drDeptalarySet[a], "MiniMoney"));
                                decimal maxMoney = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drDeptalarySet[a], "MaxMoney"));
                                if (temp >= minMoney && temp < maxMoney)
                                {
                                    rate = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drDeptalarySet[a], "TaxPercent"));
                                    break;
                                }
                            }
                            if (!isCheck)
                            {
                                rate = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drDeptalarySet[(drDeptalarySet.Length - 1)], "TaxPercent"));
                            }
                        }
                        else if (drDeptalarySetCommon.Length > 0)
                        {
                            bool isHave = false;
                            int len = drDeptalarySetCommon.Length;
                            for (int a = 0; a < len; a++)
                            {
                                decimal minMoney = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drDeptalarySetCommon[a], "MiniMoney"));
                                decimal maxMoney = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drDeptalarySetCommon[a], "MaxMoney"));
                                if (temp >= minMoney && temp < maxMoney)
                                {
                                    rate = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drDeptalarySetCommon[a], "TaxPercent"));
                                    isHave = true;
                                    break;
                                }
                            }
                            if (!isHave)
                            {
                                rate = Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drDeptalarySetCommon[(len- 1)], "TaxPercent"));
                            }

                        }

                        summaryModel .DeptComMoney  = Convert .ToString ( decimal.Round(temp * rate * (Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drStructure[0], "DeptRatePercent"))) / 10000, 2));
                        totalSalary += decimal.Round(temp * rate * (Convert.ToDecimal(GetSafeData.ValidateDataRow_String(drStructure[0], "DeptRatePercent"))) / 10000, 2);
                    }
                }
                else
                {

                    summaryModel.DeptComMoney = "0";
                    totalSalary += 0;
                }
                #endregion
                #region 判断是否设置绩效
                if (GetSafeData.ValidateDataRow_String(drStructure[0], "IsPerformanceSet") == "1")//判断是否设置绩效
                {
                    //计时工资
                    DataRow[] drPerformanceSalary = dtPerformanceSalary.Select("EmployeeID=" + employeeID);
                    if (drPerformanceSalary.Length > 0)
                    {
                        //设置绩效工资
                        //summaryModel.TimeMoney = GetSafeData.GetStringFromDecimal(drPerformanceSalary[0], "TotalPerformanceMoney");
                        summaryModel.PerformanceMoney = GetSafeData.GetStringFromDecimal(drPerformanceSalary[0], "TotalPerformanceMoney");//待新建一项绩效工资栏目
                        totalSalary += Convert.ToDecimal(drPerformanceSalary[0]["TotalPerformanceMoney"]);
                    }
                }
                else
                {
                    //设置绩效工资
                    summaryModel.PerformanceMoney = "0";
                    totalSalary += 0;


                }
                #endregion
                //工资总额
                summaryModel.AllGetMoney = totalSalary.ToString();
                decimal insuMoney = 0;
                #region 判断是否设置社会保险
                if (GetSafeData.ValidateDataRow_String(drStructure[0], "IsInsurenceSet") == "1")//判断是否设置社会保险
                {
                    //社会保险
                    DataRow[] drInsuSalary = dtInsuSalary.Select("EmployeeID=" + employeeID);

                    for (int x = 0; x < drInsuSalary.Length; x++)
                    {
                        //数据不为空时，计算社会保险总额
                        if (drInsuSalary[x]["InsuranceBase"] != DBNull.Value
                                    && drInsuSalary[x]["PersonPayRate"] != DBNull.Value)
                        {
                            //保险基数
                            decimal insuBase = Convert.ToDecimal(drInsuSalary[x]["InsuranceBase"]);
                            //个人缴纳百分比
                            decimal insuPercent = Convert.ToDecimal(drInsuSalary[x]["PersonPayRate"]);
                            //增加社会保险额
                            insuMoney += insuBase * insuPercent / 100;
                        }
                    }
                    //社会保险
                    summaryModel.Insurance = StringUtil.TrimZero(insuMoney.ToString());
                }
                else
                {
                    summaryModel.Insurance = "0";

                }
                #endregion
                decimal minusMoney = 0;
                decimal PersonMoney = 0;

                #region 判断是否设置个人所得税
                if (GetSafeData.ValidateDataRow_String(drStructure[0], "IsPerIncomeTaxSet") == "1")//判断是否设置个人所得税
                {
                    //个人所得税  dtPersonSalary
                    DataRow[] drPersonSalary = dtPersonSalary.Select("EmployeeID=" + employeeID);
                    if (drPersonSalary.Length > 0)
                    {
                        decimal salryCount = drPersonSalary[0]["SalaryCount"] == null ? 0 : Convert.ToDecimal(drPersonSalary[0]["SalaryCount"]);
                        decimal TaxPercent = drPersonSalary[0]["TaxPercent"] == null ? 0 : Convert.ToDecimal(drPersonSalary[0]["TaxPercent"]);
                        decimal TaxCount = drPersonSalary[0]["TaxCount"] == null ? 0 : Convert.ToDecimal(drPersonSalary[0]["TaxCount"]);
                          decimal te=0;
                          if ((totalSalary - salryCount - insuMoney) > 0)
                          {
                              te = (totalSalary - salryCount - insuMoney) * TaxPercent / 100 - TaxCount;
                          }
                        //个人所得税
                        summaryModel.IncomeTax = te.ToString ();
                        //  totalSalary -= Convert.ToDecimal(drPersonSalary[0]["TaxCount"]);
                        PersonMoney = te;
                    }
                }
                else
                {
                    //个人所得税
                    summaryModel.IncomeTax = "0" ;
                    //  totalSalary -= Convert.ToDecimal(drPersonSalary[0]["TaxCount"]);
                    PersonMoney =  0;
                }
                #endregion
          
              //  decimal[] taxInfo = TaxCalculateBus.CalculateTax(totalSalary);
                //税额
            //    decimal minusMoney = taxInfo[1];
                //    summaryModel.IncomeTax = StringUtil.TrimZero(minusMoney.ToString());
               

               
                minusMoney = insuMoney + PersonMoney + salaryMoney;
                //应扣款合计
                //minusMoney =minusMoney +salaryMoney;
                summaryModel.AllKillMoney = minusMoney.ToString()  ;
                //实发工资
              
                summaryModel.SalaryMoney = (totalSalary - minusMoney).ToString();
                //
                lstSummary.Add(summaryModel);

            }

            //登陆报表信息
            bool isSucc = SalaryReportDBHelper.CreateSalaryReport(model, lstSummary, dtFixedSalary);

            if (!isSucc) return null;
            else return PageDisplayInfo(dtEmplInfo, dtFixedSalary, lstSummary, companyCD);
        }
        #endregion
      

        #region 生成页面显示信息
        /// <summary>
        /// 生成页面显示信息
        /// </summary>
        /// <param name="dtEmplInfo">员工信息</param>
        /// <param name="dtFixedSalary">固定工资信息</param>
        /// <param name="lstSummary">工资合计信息</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        private static string PageDisplayInfo(DataTable dtEmplInfo, DataTable dtFixedSalary
                                                            , ArrayList lstSummary, string companyCD)
        {
            //变量定义
            StringBuilder sbPageInfo = new StringBuilder(); //返回的页面信息

            //表格开始
            sbPageInfo.AppendLine("<table width='99%' border='0' align='center' cellpadding='0' cellspacing='0' bgcolor='#F0f0f0'>");
            //标题
            sbPageInfo.AppendLine("<tr><td class='Title' height='30' align='center'>员工工资列表</td></tr>");
            sbPageInfo.AppendLine("<tr><td height='5px'></td></tr>");

            if (dtEmplInfo != null && dtEmplInfo.Rows.Count > 0)
            {
                //行列开始
                sbPageInfo.AppendLine("<tr><td>");
                //表格开始
                sbPageInfo.AppendLine("<table  width='100%' border='0' id='tblSalaryDetail'  align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
                //固定工资项总数
                int salaryItemCount = 0;
                int userCount = 0;
                //获取所有使用中的固定工资项
                DataTable dtSalaryItem = SalaryItemDBHelper.SearchSalaryItemInfo(companyCD, true);

                //设置表格标题
                sbPageInfo.AppendLine(CreateEmplSalaryTableTime(dtSalaryItem));

                //固定工资项总数
                if (dtSalaryItem != null && dtSalaryItem.Rows.Count > 0) salaryItemCount = dtSalaryItem.Rows.Count;
                //员工总数
                userCount = dtEmplInfo.Rows.Count;
                //设置表格标题
                //遍历所有员工 设置工资
                for (int i = 0; i < userCount; i++)
                {
                    //行背景色定义
                    string backColor = i % 2 == 0 ? "#FFFFFF" : "#E7E7E7";
                    //插入行开始标识
                    sbPageInfo.AppendLine("<tr style='background-color:" + backColor + ";' onmouseover='this.style.backgroundColor=\"#cfc\";'"
                                                    + " onmouseout='this.style.backgroundColor=\"" + backColor + "\";'>");

                    #region 设置员工信息
                    //员工ID
                    string emplyID = GetSafeData.GetStringFromInt(dtEmplInfo.Rows[i], "ID");

                    //员工编号
                    sbPageInfo.AppendLine("<td align='center'>"
                                + "<input type='hidden' id='txtEmplID_" + (i + 1).ToString() + "' value='" + emplyID + "' />"
                                + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "EmployeeNo") + "</td>");
                    //员工姓名
                    sbPageInfo.AppendLine("<td align='center' id='tdEmployeeName_" + (i + 1).ToString() + "'>"
                                + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "EmployeeName") + "</td>");
                    //所在部门
                    sbPageInfo.AppendLine("<td align='center'>"
                                + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "DeptName") + "</td>");
                    //所在岗位
                    sbPageInfo.AppendLine("<td align='center'>"
                                + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "QuarterName") + "</td>");
                    //岗位职等
                    sbPageInfo.AppendLine("<td align='center'>"
                                + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "AdminLevelName") + "</td>");
                    #endregion
                    ////#region 判断是否设置固定工资
                    ////if (GetSafeData.ValidateDataRow_String(drStructure[0], "IsFixSalarySet") == "1")//判断是否设置固定工资
                    ////{
                    ////    //固定工资
                    ////    DataRow[] drFixedSalary = dtEmplSalary.Select("EmployeeID=" + employeeID);

                    ////    //遍历所有固定工资项，计算工资额
                    ////    for (int j = 0; j < drFixedSalary.Length; j++)
                    ////    {
                    ////        //导入工资记录
                    ////        dtFixedSalary.ImportRow(drFixedSalary[j]);
                    ////        //该项工资额为DBNull时，增加该工资项
                    ////        if (drFixedSalary[j]["SalaryMoney"] != DBNull.Value)
                    ////        {
                    ////            //获取是否扣款
                    ////            string flag = GetSafeData.ValidateDataRow_String(drFixedSalary[j], "PayFlag");

                    ////            string cal = GetSafeData.ValidateDataRow_String(drFixedSalary[j], "Calculate");
                    ////            decimal fina = 0;
                    ////            if (string.IsNullOrEmpty(cal))
                    ////            {
                    ////                fina = Convert.ToDecimal(drFixedSalary[j]["SalaryMoney"]);
                    ////            }
                    ////            else
                    ////            {
                    ////                while (cal.IndexOf('A') != -1)
                    ////                {
                    ////                    cal = cal.Replace('A', '+');
                    ////                }
                    ////                string numlist = GetSafeData.ValidateDataRow_String(drFixedSalary[j], "ParamsList");
                    ////                string[] numberlist = numlist.Split(',');
                    ////                string temp = cal;

                    ////                for (int inde = 0; inde < numberlist.Length; inde++)
                    ////                {
                    ////                    DataRow[] drtem = dtEmplSalary.Select("EmployeeID=" + employeeID + "  and  ItemNo=" + numberlist[inde]);
                    ////                    if (drtem.Length > 0)
                    ////                    {
                    ////                        cal = cal.Replace("{" + numberlist[inde] + "}", GetSafeData.ValidateDataRow_String(drtem[0], "SalaryMoney"));
                    ////                    }
                    ////                    else
                    ////                    {
                    ////                        cal = cal.Replace("{" + numberlist[inde] + "}", "0");
                    ////                    }



                    ////                }

                    ////                try
                    ////                {

                    ////                    string result = CustomEval.eval(cal).ToString();
                    ////                    fina = Convert.ToDecimal(result);

                    ////                }
                    ////                catch (Exception e)
                    ////                {

                    ////                    fina = 0;
                    ////                }
                    ////            }


                    ////            if ("0".Equals(flag))
                    ////            {



                    ////                //增加工资额
                    ////                totalSalary += fina;
                    ////            }
                    ////            else
                    ////            {
                    ////                //减少工资额
                    ////                // totalSalary -= Convert.ToDecimal(drFixedSalary[j]["SalaryMoney"]);
                    ////                salaryMoney += fina;
                    ////            }




                    ////        }
                    ////    }


                    ////    //固定工资
                    ////    summaryModel.FixedMoney = totalSalary.ToString();
                    ////}
                    ////else
                    ////{
                    ////    //固定工资
                    ////    summaryModel.FixedMoney = "0";
                    ////}

                    ////#endregion
                    #region 设置工资固定工资信息
                    //工资信息数据存在时
                    if (dtSalaryItem != null && salaryItemCount > 0)
                    {
                        for (int j = 0; j < salaryItemCount; j++)
                        {

                            //获取工资项ID 
                            string salaryID = GetSafeData.GetStringFromInt(dtSalaryItem.Rows[j], "ItemNo");
                            //获取工资固定Flag
                            string changeFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "ChangeFlag");
                            string PayFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "PayFlag");
                            if (PayFlag == "0")
                            {
                                //是否设置
                                DataRow[] drFixedSalary = dtFixedSalary.Select("EmployeeID = " + emplyID
                                                + " and  ItemNo = " + salaryID + " and  ItemNo = " + salaryID);

                                //获取固定工资
                                string fixedSalary = string.Empty;
                                if (drFixedSalary != null && drFixedSalary.Length > 0)
                                    fixedSalary = GetSafeData.GetStringFromDecimal(drFixedSalary[0], "SalaryMoney");
                                string disabled = string.Empty;
                                if ("1".Equals(changeFlag))
                                {
                                    disabled = "disabled";
                                }
                                sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                    + "<input type='text' maxlength = '12' style='width:75%;' value='" + StringUtil.TrimZero(fixedSalary)
                                    + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                    + " class='tdinput' id='txtFixedMoney_" + (i + 1).ToString() + "_" + (j + 1).ToString() + "' "
                                    + disabled + " /></td>");
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                     #endregion

                    #region 设置其他工资内容
                    //获取合计工资信息
                    SalaryReportSummaryModel summaryModel = (SalaryReportSummaryModel)lstSummary[i];
                    //计件工资
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='" + StringUtil.TrimZero(summaryModel.WorkMoney)
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtWorkMoney_" + (i + 1).ToString() + "' /></td>");
                    //计时工资
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='" + StringUtil.TrimZero(summaryModel.TimeMoney)
                                + "' onchange='Number_round(this,\"2\");'   onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtTimeMoney_" + (i + 1).ToString() + "' /></td>");
                    //公司提成  
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='" + StringUtil.TrimZero(summaryModel.CompanyComMoney)
                                + "' onchange='Number_round(this,\"2\");'   onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtCompanyComMoney_" + (i + 1).ToString() + "' /></td>");
                    //部门提成   
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='" + StringUtil.TrimZero(summaryModel.DeptComMoney)
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtDeptComMoney_" + (i + 1).ToString() + "' /></td>"); 
                                       // 个人业务提成 
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='" + StringUtil.TrimZero(summaryModel.PersonComMoney)
                                + "' onchange='Number_round(this,\"2\");'   onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtPersonComMoney_" + (i + 1).ToString() + "' /></td>");
                    //单品提成工资
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='" + StringUtil.TrimZero(summaryModel.CommissionMoney)
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtCommissionMoney_" + (i + 1).ToString() + "' /></td>");
                    //绩效工资
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='" + StringUtil.TrimZero(summaryModel.PerformanceMoney )
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtPerformanceMoney_" + (i + 1).ToString() + "' /></td>");
                    //其他应付工资
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='"
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtOtherPayMoney_" + (i + 1).ToString() + "' /></td>");
                    //应付工资合计
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' readonly style='width:75%;' value='" + StringUtil.TrimZero(summaryModel.AllGetMoney)
                                + "' class='tdinput' id='txtAllGetMoney_" + (i + 1).ToString() + "' /></td>");
                    if (dtSalaryItem != null && salaryItemCount > 0)
                    {
                        for (int j = 0; j < salaryItemCount; j++)
                        {

                            //获取工资项ID 
                            string salaryID = GetSafeData.GetStringFromInt(dtSalaryItem.Rows[j], "ItemNo");
                            //获取工资固定Flag
                            string changeFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "ChangeFlag");
                            string PayFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "PayFlag");
                            if (PayFlag == "1")
                            {
                                //是否设置
                                DataRow[] drFixedSalary = dtFixedSalary.Select("EmployeeID = " + emplyID
                                                + " and  ItemNo = " + salaryID + " and  ItemNo = " + salaryID);

                                //获取固定工资
                                string fixedSalary = string.Empty;
                                if (drFixedSalary != null && drFixedSalary.Length > 0)
                                    fixedSalary = GetSafeData.GetStringFromDecimal(drFixedSalary[0], "SalaryMoney");
                                string disabled = string.Empty;
                                if ("1".Equals(changeFlag))
                                {
                                    disabled = "disabled";
                                }
                                sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                    + "<input type='text' maxlength = '12' style='width:75%;' value='" + StringUtil.TrimZero(fixedSalary)
                                    + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                    + " class='tdinput' id='txtFixedMoney_" + (i + 1).ToString() + "_" + (j + 1).ToString() + "' "
                                    + disabled + " /></td>");
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    
                    //社会保险
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='" + StringUtil.TrimZero(summaryModel.Insurance)
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtInsurance_" + (i + 1).ToString() + "' /></td>");
                    //个人所得税
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='" + StringUtil.TrimZero(summaryModel.IncomeTax)
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtIncomeTax_" + (i + 1).ToString() + "' /></td>");
                    //其他应扣款
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='"
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtOtherMinusMoney_" + (i + 1).ToString() + "' /></td>");
                    //应扣款合计
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' readonly style='width:75%;' value='" + StringUtil.TrimZero(summaryModel.AllKillMoney)
                                + "' class='tdinput' id='txtAllKillMoney_" + (i + 1).ToString() + "' /></td>");
                    //实发工资额
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' readonly style='width:75%;' value='" + StringUtil.TrimZero(summaryModel.SalaryMoney)
                                + "' class='tdinput' id='txtFactSalaryMoney_" + (i + 1).ToString() + "' /></td>");
                    #endregion

                    //表格结束
                    sbPageInfo.AppendLine("</tr>");

                }
                //表格结束
                sbPageInfo.AppendLine("</table>");
                //设置固定工资项总数
                sbPageInfo.AppendLine("<input type='hidden' id='txtSalaryItemCount' value='" + salaryItemCount.ToString() + "' />");
                //设置员工总数
                sbPageInfo.AppendLine("<input type='hidden' id='txtUserCount' value='" + userCount.ToString() + "' />");
                //行列结束
                sbPageInfo.AppendLine("</td></tr>");
            }
            //表格结束
            sbPageInfo.AppendLine("</table>");

            //返回页面信息
            return sbPageInfo.ToString();
        }
        #endregion

        #region 设置员工工资列表表格标题
        /// <summary>
        /// 设置员工工资列表表格标题
        /// </summary>
        /// <param name="dtSalaryItem">固定工资项</param>
        /// <returns></returns>
        private static string CreateEmplSalaryTableTime(DataTable dtSalaryItem)
        {
            //定义表格变量
            StringBuilder tableTitle = new StringBuilder();
            //生成表格标题
            tableTitle.AppendLine("	<tr>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>员工编号</td>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>员工姓名</td>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>所在部门</td>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>所在岗位</td>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>岗位职等</td>");

            //工资项数据存在时
            int salaryCount = 0;
            if (dtSalaryItem != null && dtSalaryItem.Rows.Count > 0)
            {
            
                //获取工资项总数
                salaryCount = dtSalaryItem.Rows.Count;
                //遍历所有工资项，设置到表格中
                for (int i = 0; i < salaryCount; i++)
                {
                    //获取工资项名称
                    string PayFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "PayFlag");
                    if (PayFlag == "0")
                    {
                        string salaryName = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "ItemName");
                        //设置工资项名称
                        tableTitle.AppendLine("<td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>"
                                        + "<input type='hidden' id='txtSalaryName_" + (i + 1).ToString() + "' value='" + salaryName + "' />"
                                        + "<input type='hidden' id='txtSalaryFlag_" + (i + 1).ToString() + "' value='"
                                        + GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "PayFlag") + "' />"
                                        + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
                                        + GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "ItemNo") + "' />" + salaryName + "</td>");
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>计件工资</td>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>计时工资</td>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>公司提成</td>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>部门提成</td>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>个人业务提成</td>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>产品单品提成</td>"); 
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>绩效工资</td>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>其他应付工资</td>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>应付工资</td>");
            if (dtSalaryItem != null && dtSalaryItem.Rows.Count > 0)
            {
                //获取工资项总数
                salaryCount = dtSalaryItem.Rows.Count;
                //遍历所有工资项，设置到表格中
                for (int i = 0; i < salaryCount; i++)
                {  string PayFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "PayFlag");
                if (PayFlag == "1")
                {
                    //获取工资项名称
                    string salaryName = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "ItemName");
                    //设置工资项名称
                    tableTitle.AppendLine("<td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>"
                                    + "<input type='hidden' id='txtSalaryName_" + (i + 1).ToString() + "' value='" + salaryName + "' />"
                                    + "<input type='hidden' id='txtSalaryFlag_" + (i + 1).ToString() + "' value='"
                                    + GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "PayFlag") + "' />"
                                    + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
                                    + GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "ItemNo") + "' />" + salaryName + "</td>");
                }
                else
                {
                    continue;
                }
                }
            }
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>社会保险</td>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>个人所得税</td>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>其他应扣款</td>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>扣款合计</td>");
            tableTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/tableTitle_bg.jpg'>应发工资</td>");

            tableTitle.AppendLine("	</tr>");
            //返回表格语句
            return tableTitle.ToString();
        }
        #endregion

        #region 保存操作
        /// <summary>
        /// 保存工资报表信息
        /// </summary>
        /// <param name="lstDetail">明细信息</param>
        /// <param name="lstSummary">合计信息</param>
        /// <param name="model">基本信息</param>
        /// <returns></returns>
        public static bool SaveSalaryInfo(ArrayList lstDetail, ArrayList lstSummary, SalaryReportModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //用户ID
            model.ModifiedUserID = userInfo.UserID;

            //执行保存操作
            bool isSucc = SalaryReportDBHelper.SaveSalaryInfo(lstDetail, lstSummary, model);

            //返回
            return isSucc;
        }
        #endregion

        #region 查询工资报表信息
        /// <summary>
        /// 查询工资报表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable SearchReportInfo(SalaryReportModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询
            return SalaryReportDBHelper.SearchReportInfo(model);
        }
        #endregion

        #region 生成页面的工资报表信息
        /// <summary>
        /// 生成页面的工资报表信息
        /// </summary>
        /// <param name="reportNo">报表编号</param>
        /// <returns></returns>
        public static string InitSalaryReportInfo(string reportNo)
        {
            //获取登陆用户信息
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //变量定义
            StringBuilder sbPageInfo = new StringBuilder(); //返回的页面信息

            //表格开始
            sbPageInfo.AppendLine("<table width='99%' border='0' align='center' cellpadding='0' cellspacing='0' bgcolor='#F0f0f0'>");
            //标题
            sbPageInfo.AppendLine("<tr><td class='Title' height='30' align='center'>员工工资列表</td></tr>");
            sbPageInfo.AppendLine("<tr><td height='5px'></td></tr>");

            //工资合计信息
            DataTable dtSummaryInfo = SalaryReportDBHelper.GetReportSummaryInfoByNo(reportNo, companyCD);
            //工资详细信息
            DataTable dtFixedSalary = SalaryReportDBHelper.GetReportDetailInfoByNo(reportNo, companyCD);

            if (dtSummaryInfo != null && dtSummaryInfo.Rows.Count > 0)
            {
                //行列开始
                sbPageInfo.AppendLine("<tr><td>");
                //表格开始
                sbPageInfo.AppendLine("<table  width='100%' border='0' id='tblSalaryDetail'  align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
                //固定工资项总数
                int salaryItemCount = 0;
                int userCount = 0;
                //获取所有使用中的固定工资项
              //  DataTable dtSalaryItem = SalaryItemDBHelper.SearchSalaryItemInfo(companyCD, true);
                DataTable dtSalaryItem = SalaryReportDBHelper.GetReportSalaryDetailByNo(reportNo, companyCD);
                //设置表格标题
                sbPageInfo.AppendLine(CreateEmplSalaryTableTime(dtSalaryItem));

                //固定工资项总数
                if (dtSalaryItem != null && dtSalaryItem.Rows.Count > 0) salaryItemCount = dtSalaryItem.Rows.Count;
                //员工总数
                userCount = dtSummaryInfo.Rows.Count;
                //设置表格标题
                //遍历所有员工 设置工资
                for (int i = 0; i < userCount; i++)
                {
                    //行背景色定义
                    string backColor = i % 2 == 0 ? "#FFFFFF" : "#E7E7E7";
                    //插入行开始标识
                    sbPageInfo.AppendLine("<tr style='background-color:" + backColor + ";' onmouseover='this.style.backgroundColor=\"#cfc\";'"
                                                    + " onmouseout='this.style.backgroundColor=\"" + backColor + "\";'>");

                    #region 设置员工信息
                    //员工ID
                    string emplyID = GetSafeData.GetStringFromInt(dtSummaryInfo.Rows[i], "EmployeeID");

                    //员工编号
                    sbPageInfo.AppendLine("<td align='center'>"
                                + "<input type='hidden' id='txtEmplID_" + (i + 1).ToString() + "' value='" + emplyID + "' />"
                                + GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "EmployeeNo") + "</td>");
                    //员工姓名
                    sbPageInfo.AppendLine("<td align='center' id='tdEmployeeName_" + (i + 1).ToString() + "'>"
                                + GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "EmployeeName") + "</td>");
                    //所在部门
                    sbPageInfo.AppendLine("<td align='center'>"
                                + GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "DeptName") + "</td>");
                    //所在岗位
                    sbPageInfo.AppendLine("<td align='center'>"
                                + GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "QuarterName") + "</td>");
                    //岗位职等
                    sbPageInfo.AppendLine("<td align='center'>"
                                + GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "AdminLevelName") + "</td>");
                    #endregion

                    #region 设置工资固定工资信息
                    //工资信息数据存在时
                    if (dtSalaryItem != null && salaryItemCount > 0)
                    {
                      
                            for (int j = 0; j < salaryItemCount; j++)
                            {
                                   string PayFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "PayFlag");
                                   if (PayFlag == "0")
                              {
                                //获取工资项ID 
                                string salaryID = GetSafeData.GetStringFromInt(dtSalaryItem.Rows[j], "ItemNo");
                                //获取工资固定Flag
                                string changeFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "ChangeFlag");
                                //是否设置
                                DataRow[] drFixedSalary = dtFixedSalary.Select("EmployeeID = " + emplyID
                                                + " and  ItemNo = " + salaryID);

                                //获取固定工资
                                string fixedSalary = string.Empty;
                                if (drFixedSalary != null && drFixedSalary.Length > 0)
                                    fixedSalary = GetSafeData.GetStringFromDecimal(drFixedSalary[0], "SalaryMoney");
                                string disabled = string.Empty;
                                if ("1".Equals(changeFlag))
                                {
                                    disabled = "disabled";
                                }
                                sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                    + "<input type='text' maxlength = '12' style='width:75%;' value='" + StringUtil.TrimZero(fixedSalary)
                                    + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                    + " class='tdinput' id='txtFixedMoney_" + (i + 1).ToString() + "_" + (j + 1).ToString() + "' "
                                    + disabled + " /></td>");


                            }
                        else
                        { continue; }
                            }
                    
                    }
                    #endregion

                    #region 设置其他工资内容
                    //计件工资
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='"
                                + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "WorkMoney"))
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtWorkMoney_" + (i + 1).ToString() + "' /></td>");
                    //计时工资
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='"
                                + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "TimeMoney"))
                                + "' onchange='Number_round(this,\"2\");' onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtTimeMoney_" + (i + 1).ToString() + "' /></td>");
                    // 公司提成工资
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='"
                                + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "CompanyComMoney"))
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtCompanyComMoney_" + (i + 1).ToString() + "' /></td>");
                    // 部门提成工资
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='"
                                + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "DeptComMoney"))
                                + "' onchange='Number_round(this,\"2\");' onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtDeptComMoney_" + (i + 1).ToString() + "' /></td>");
                    // 个人业务提成工资
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='"
                                + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "PersonComMoney"))
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtPersonComMoney_" + (i + 1).ToString() + "' /></td>");
                    // 产品单品提成工资
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='"
                                + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "CommissionMoney"))
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtCommissionMoney_" + (i + 1).ToString() + "' /></td>");
                    // 绩效工资
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='"
                                + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "PerformanceMoney"))
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtPerformanceMoney_" + (i + 1).ToString() + "' /></td>");
                    //其他应付工资
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='"
                                + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "OtherGetMoney"))
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtOtherPayMoney_" + (i + 1).ToString() + "' /></td>");
                    //应付工资合计
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' readonly style='width:75%;' value='"
                                + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "AllGetMoney"))
                                + "' class='tdinput' id='txtAllGetMoney_" + (i + 1).ToString() + "' /></td>");
                    if (dtSalaryItem != null && salaryItemCount > 0)
                    {

                        for (int j = 0; j < salaryItemCount; j++)
                        {
                            string PayFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "PayFlag");
                            if (PayFlag == "1")
                            {
                                //获取工资项ID 
                                string salaryID = GetSafeData.GetStringFromInt(dtSalaryItem.Rows[j], "ItemNo");
                                //获取工资固定Flag
                                string changeFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "ChangeFlag");
                                //是否设置
                                DataRow[] drFixedSalary = dtFixedSalary.Select("EmployeeID = " + emplyID
                                                + " and  ItemNo = " + salaryID);

                                //获取固定工资
                                string fixedSalary = string.Empty;
                                if (drFixedSalary != null && drFixedSalary.Length > 0)
                                    fixedSalary = GetSafeData.GetStringFromDecimal(drFixedSalary[0], "SalaryMoney");
                                string disabled = string.Empty;
                                if ("1".Equals(changeFlag))
                                {
                                    disabled = "disabled";
                                }
                                sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                    + "<input type='text' maxlength = '12' style='width:75%;' value='" + StringUtil.TrimZero(fixedSalary)
                                    + "' onchange='Number_round(this,\"2\");' onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                    + " class='tdinput' id='txtFixedMoney_" + (i + 1).ToString() + "_" + (j + 1).ToString() + "' "
                                    + disabled + " /></td>");


                            }
                            else
                            { continue; }
                        }

                    }
                    
                    
                    //社会保险
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='"
                                + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "Insurance"))
                                + "' onchange='Number_round(this,\"2\");' onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtInsurance_" + (i + 1).ToString() + "' /></td>");

             
                    //个人所得税
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='"
                                + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "IncomeTax"))
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtIncomeTax_" + (i + 1).ToString() + "' /></td>");
                    //其他应扣款
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' style='width:75%;' value='"
                                + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "OtherKillMoney"))
                                + "' onchange='Number_round(this,\"2\");'  onblur='CalculateMinusSalary(this, \"" + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtOtherMinusMoney_" + (i + 1).ToString() + "' /></td>");
                    //应扣款合计
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' readonly style='width:75%;' value='"
                                + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "AllKillMoney"))
                                + "' class='tdinput' id='txtAllKillMoney_" + (i + 1).ToString() + "' /></td>");
                    //实发工资额
                    sbPageInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' readonly style='width:75%;' value='"
                                + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "SalaryMoney"))
                                + "' class='tdinput' id='txtFactSalaryMoney_" + (i + 1).ToString() + "' /></td>");
                    #endregion

                    //表格结束
                    sbPageInfo.AppendLine("</tr>");

                }
                //表格结束
                sbPageInfo.AppendLine("</table>");
                //设置固定工资项总数
                sbPageInfo.AppendLine("<input type='hidden' id='txtSalaryItemCount' value='" + salaryItemCount.ToString() + "' />");
                //设置员工总数
                sbPageInfo.AppendLine("<input type='hidden' id='txtUserCount' value='" + userCount.ToString() + "' />");
                //行列结束
                sbPageInfo.AppendLine("</td></tr>");
            }
            //表格结束
            sbPageInfo.AppendLine("</table>");

            //返回页面信息
            return sbPageInfo.ToString();
        }
        #endregion

        #region 通过ID获取基本信息
        /// <summary>
        /// 通过ID获取基本信息
        /// </summary>
        /// <param name="ID">报表ID</param>
        /// <returns></returns>
        public static DataTable GetReportInfoByID(string ID)
        {
                        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                        return SalaryReportDBHelper.GetReportInfoByID(ID, companyCD);
        }

        #endregion

        public static DataTable GetReportInfoByNo(string ID)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SalaryReportDBHelper.GetReportInfoByNo(ID, companyCD);
        }
        public static bool DeleteOneReport(string reportNo)
        {
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //执行删除操作
            return SalaryReportDBHelper.DeleteOneReport (reportNo, companyCD);
        }
        #region 删除工资报表信息
        /// <summary>
        /// 删除工资报表信息
        /// </summary>
        /// <param name="reportNo">报表编号</param>
        /// <returns></returns>
        public static bool DeleteReport(string reportNo)
        {
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //执行删除操作
            return SalaryReportDBHelper.DeleteReport(reportNo, companyCD);
        }
        #endregion
        public static string PrintInitSalaryReportInfo(string reportNo, int defaultCount, string Printclass)
        {
            //获取登陆用户信息
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //变量定义
            StringBuilder sbPageInfo = new StringBuilder(); //返回的页面信息

            //表格开始
        
            //标题
     

            //工资合计信息
            DataTable dtSummaryInfo = SalaryReportDBHelper.GetReportSummaryInfoByNo(reportNo, companyCD);
            //工资详细信息
            DataTable dtFixedSalary = SalaryReportDBHelper.GetReportDetailInfoByNo(reportNo, companyCD);

            if (dtSummaryInfo != null && dtSummaryInfo.Rows.Count > 0)
            {
                //行列开始
   
                //表格开始
               
                //固定工资项总数
                int salaryItemCount = 0;
                int userCount = 0;
                //获取所有使用中的固定工资项
                //  DataTable dtSalaryItem = SalaryItemDBHelper.SearchSalaryItemInfo(companyCD, true);
                DataTable dtSalaryItem = SalaryReportDBHelper.GetReportSalaryDetailByNo(reportNo, companyCD);
                //设置表格标题
                sbPageInfo.AppendLine(PrintCreateEmplSalaryTableTime(dtSalaryItem));

                //固定工资项总数
                if (dtSalaryItem != null && dtSalaryItem.Rows.Count > 0) salaryItemCount = dtSalaryItem.Rows.Count;
                //员工总数
                userCount = dtSummaryInfo.Rows.Count;
                sbPageInfo.AppendLine("\r\t     <tbody bgcolor=\"white\" id=\"show\">");
                //设置表格标题
                //遍历所有员工 设置工资
                for (int i = 0; i < userCount; i++)
                {
                 
                        string className = (i + 1) % defaultCount == 0 ? "class=td5" : "class=td4";
                        string className1 = (i + 1) % defaultCount == 0 ? "class=td3" : "class=td1";
                        string trclass = ((i % defaultCount == 0) && i > 0) ? "class=" + Printclass : "";
                        if (i < userCount - 1)
                    {   //行背景色定义
                        //string backColor = i % 2 == 0 ? "#FFFFFF" : "#E7E7E7";
                        //插入行开始标识
                        sbPageInfo.AppendLine("\r\t     <tr " + trclass + ">");
                        #region 设置员工信息
                        //员工ID
                        string emplyID = GetSafeData.GetStringFromInt(dtSummaryInfo.Rows[i], "EmployeeID");

                        //员工编号
                        sbPageInfo.AppendLine("\r\t     <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\">"
                                    + GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "EmployeeNo") + "&nbsp; </td>");
                        //员工姓名
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\"   id='tdEmployeeName_" + (i + 1).ToString() + "'>"
                                    + GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "EmployeeName") + "&nbsp; </td>");
                        //所在部门
                        sbPageInfo.AppendLine("\r\t  <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\">"
                                    + GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "DeptName") + "&nbsp; </td>");
                        //所在岗位
                        sbPageInfo.AppendLine(" \r\t  <td  width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\">"
                                    + GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "QuarterName") + "&nbsp; </td>");
                        //岗位职等
                        sbPageInfo.AppendLine("\r\t   <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\">"
                                    + GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "AdminLevelName") + "&nbsp; </td>");
                        #endregion

                        #region 设置工资固定工资信息
                        //工资信息数据存在时
                        if (dtSalaryItem != null && salaryItemCount > 0)
                        {

                            for (int j = 0; j < salaryItemCount; j++)
                            {
                                string PayFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "PayFlag");
                                if (PayFlag == "0")
                                {
                                    //获取工资项ID 
                                    string salaryID = GetSafeData.GetStringFromInt(dtSalaryItem.Rows[j], "ItemNo");
                                    //获取工资固定Flag
                                    string changeFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "ChangeFlag");
                                    //是否设置
                                    DataRow[] drFixedSalary = dtFixedSalary.Select("EmployeeID = " + emplyID
                                                    + " and  ItemNo = " + salaryID);

                                    //获取固定工资
                                    string fixedSalary = string.Empty;
                                    if (drFixedSalary != null && drFixedSalary.Length > 0)
                                        fixedSalary = GetSafeData.GetStringFromDecimal(drFixedSalary[0], "SalaryMoney");
                                    string disabled = string.Empty;
                                    if ("1".Equals(changeFlag))
                                    {
                                        disabled = "disabled";
                                    }
                                    sbPageInfo.AppendLine("\r\t  <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\">"+ StringUtil.TrimZero(fixedSalary)+"&nbsp;  </td>");


                                }
                                else
                                { continue; }
                            }

                        }
                        #endregion

                        #region 设置其他工资内容
                        //计件工资
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\" >"
                                      + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "WorkMoney"))
                                    + "&nbsp; </td>");
                        //计时工资
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\" >" 
                                    + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "TimeMoney"))
                                    + "&nbsp; </td>");
                        //公司提成
                        sbPageInfo.AppendLine("\r\t     <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\">"
                                    + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "CompanyComMoney")) + "&nbsp;  </td>");
                        //部门工资
                        sbPageInfo.AppendLine("\r\t     <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\">"
                                    + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "DeptComMoney")) + "&nbsp;  </td>");
                        //个人业务提成
                        sbPageInfo.AppendLine("\r\t     <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\">"
                                    + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "PersonComMoney")) + "&nbsp;  </td>");
                        //单品产品提成
                        sbPageInfo.AppendLine("\r\t     <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\">"
                                    + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "CommissionMoney")) + "&nbsp;  </td>");
                        //绩效工资
                        sbPageInfo.AppendLine("\r\t     <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\">"
                                    + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "PerformanceMoney")) + "&nbsp;  </td>");


                        
                        //其他应付工资
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\" >"
                                  + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "OtherGetMoney"))
                                 + "&nbsp; </td>");
                        //应付工资合计
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\" >"
                                    + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "AllGetMoney"))
                                 + "&nbsp; </td>");
                        if (dtSalaryItem != null && salaryItemCount > 0)
                        {

                            for (int j = 0; j < salaryItemCount; j++)
                            {
                                string PayFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "PayFlag");
                                if (PayFlag == "1")
                                {
                                    //获取工资项ID 
                                    string salaryID = GetSafeData.GetStringFromInt(dtSalaryItem.Rows[j], "ItemNo");
                                    //获取工资固定Flag
                                    string changeFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "ChangeFlag");
                                    //是否设置
                                    DataRow[] drFixedSalary = dtFixedSalary.Select("EmployeeID = " + emplyID
                                                    + " and  ItemNo = " + salaryID);

                                    //获取固定工资
                                    string fixedSalary = string.Empty;
                                    if (drFixedSalary != null && drFixedSalary.Length > 0)
                                        fixedSalary = GetSafeData.GetStringFromDecimal(drFixedSalary[0], "SalaryMoney");
                                    string disabled = string.Empty;
                                    if ("1".Equals(changeFlag))
                                    {
                                        disabled = "disabled";
                                    }
                                    sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\" >"
                                      + StringUtil.TrimZero(fixedSalary) + " &nbsp;    </td>");


                                }
                                else
                                { continue; }
                            }

                        }


                        //社会保险
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\" >"
                                     + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "Insurance"))
                                    + "&nbsp; </td>");


                        //个人所得税
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\" >"
                                    + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "IncomeTax"))
                                   + "&nbsp; </td>");
                        //其他应扣款
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\" >"
                                       + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "OtherKillMoney"))
                                    + "&nbsp; </td>");
                        //应扣款合计
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" " + className1 + " style=\"height: 25px\" >"
                                     + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "AllKillMoney"))
                                    + "&nbsp; </td>");
                        //实发工资额
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" " + className + " style=\"height: 25px\" >"
                                    + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "SalaryMoney"))
                                  + " &nbsp; </td>");
                        #endregion

                        //表格结束
                        sbPageInfo.AppendLine("</tr>");
                    }
                    else
                    {
                         
                        sbPageInfo.AppendLine("\r\t     <tr " + trclass + ">");
                        #region 设置员工信息
                        //员工ID
                        string emplyID = GetSafeData.GetStringFromInt(dtSummaryInfo.Rows[i], "EmployeeID");

                        //员工编号
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                 + GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "EmployeeNo") + "&nbsp; </td>");
                        //员工姓名
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\"   id='tdEmployeeName_" + (i + 1).ToString() + "'>"
                                    + GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "EmployeeName") + "&nbsp; </td>");
                        //所在部门
                        sbPageInfo.AppendLine("\r\t  <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                    + GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "DeptName") + "&nbsp; </td>");
                        //所在岗位
                        sbPageInfo.AppendLine(" \r\t  <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                    + GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "QuarterName") + "&nbsp; </td>");
                        //岗位职等
                        sbPageInfo.AppendLine("\r\t   <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                    + GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "AdminLevelName") + "&nbsp; </td>");
                        #endregion

                        #region 设置工资固定工资信息
                        //工资信息数据存在时
                        if (dtSalaryItem != null && salaryItemCount > 0)
                        {

                            for (int j = 0; j < salaryItemCount; j++)
                            {
                                string PayFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "PayFlag");
                                if (PayFlag == "0")
                                {
                                    //获取工资项ID 
                                    string salaryID = GetSafeData.GetStringFromInt(dtSalaryItem.Rows[j], "ItemNo");
                                    //获取工资固定Flag
                                    string changeFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "ChangeFlag");
                                    //是否设置
                                    DataRow[] drFixedSalary = dtFixedSalary.Select("EmployeeID = " + emplyID
                                                    + " and  ItemNo = " + salaryID);

                                    //获取固定工资
                                    string fixedSalary = string.Empty;
                                    if (drFixedSalary != null && drFixedSalary.Length > 0)
                                        fixedSalary = GetSafeData.GetStringFromDecimal(drFixedSalary[0], "SalaryMoney");
                                    string disabled = string.Empty;
                                    if ("1".Equals(changeFlag))
                                    {
                                        disabled = "disabled";
                                    }
                                    sbPageInfo.AppendLine("\r\t  <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                  + StringUtil.TrimZero(fixedSalary) + " &nbsp; </td>");


                                }
                                else
                                { continue; }
                            }

                        }
                        #endregion

                        #region 设置其他工资内容
                        //计件工资
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                      + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "WorkMoney")) + " &nbsp; </td>");
                        //计时工资
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                     + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "TimeMoney")) + "&nbsp;  </td>");
                        //公司提成
                        sbPageInfo.AppendLine("\r\t     <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                    + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "CompanyComMoney")) + "&nbsp;  </td>");
                        //部门工资
                        sbPageInfo.AppendLine("\r\t     <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                    + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "DeptComMoney")) + "&nbsp;  </td>");
                        //个人业务提成
                        sbPageInfo.AppendLine("\r\t     <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                    + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "PersonComMoney")) + "&nbsp;  </td>");
                        //单品产品提成
                        sbPageInfo.AppendLine("\r\t     <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                    + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "CommissionMoney")) + "&nbsp;  </td>");
                        //绩效工资
                        sbPageInfo.AppendLine("\r\t     <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                    + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "PerformanceMoney")) + "&nbsp;  </td>");
                        //其他应付工资
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">" + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "OtherGetMoney")) + "&nbsp;  </td>");
                        //应付工资合计
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">" + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "AllGetMoney")) + "&nbsp;  </td>");
                        if (dtSalaryItem != null && salaryItemCount > 0)
                        {

                            for (int j = 0; j < salaryItemCount; j++)
                            {
                                string PayFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "PayFlag");
                                if (PayFlag == "1")
                                {
                                    //获取工资项ID 
                                    string salaryID = GetSafeData.GetStringFromInt(dtSalaryItem.Rows[j], "ItemNo");
                                    //获取工资固定Flag
                                    string changeFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[j], "ChangeFlag");
                                    //是否设置
                                    DataRow[] drFixedSalary = dtFixedSalary.Select("EmployeeID = " + emplyID
                                                    + " and  ItemNo = " + salaryID);

                                    //获取固定工资
                                    string fixedSalary = string.Empty;
                                    if (drFixedSalary != null && drFixedSalary.Length > 0)
                                        fixedSalary = GetSafeData.GetStringFromDecimal(drFixedSalary[0], "SalaryMoney");
                                    string disabled = string.Empty;
                                    if ("1".Equals(changeFlag))
                                    {
                                        disabled = "disabled";
                                    }
                                    sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                         + StringUtil.TrimZero(fixedSalary) + " &nbsp;  </td>");


                                }
                                else
                                { continue; }
                            }

                        }


                        //社会保险
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                     + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "Insurance"))
                                      + " &nbsp; </td>");


                        //个人所得税
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                     + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "IncomeTax"))
                                     + " &nbsp; </td>");
                        //其他应扣款
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                     + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "OtherKillMoney"))
                                   + " &nbsp; </td>");
                        //应扣款合计
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" class=td3 style=\"height: 25px\">"
                                     + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "AllKillMoney"))
                                     + " &nbsp; </td>");
                        //实发工资额
                        sbPageInfo.AppendLine("\r\t    <td width=\"7%\" align=\"left\" class=td5 style=\"height: 25px\">"
                                     + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtSummaryInfo.Rows[i], "SalaryMoney"))
                                   + " &nbsp; </td>");
                        #endregion

                        //表格结束
                        sbPageInfo.AppendLine("</tr>");
                    }
                }
                //表格结束
               
                //设置固定工资项总数
                sbPageInfo.AppendLine("<input type='hidden' id='txtSalaryItemCount' value='" + salaryItemCount.ToString() + "' />");
                //设置员工总数
                sbPageInfo.AppendLine("<input type='hidden' id='txtUserCount' value='" + userCount.ToString() + "' />");
                //行列结束
 
            }
            //表格结束

            sbPageInfo.AppendLine("\r\t     </tbody>");
            //返回页面信息
            return sbPageInfo.ToString();
        }
        private static string PrintCreateEmplSalaryTableTime(DataTable dtSalaryItem)
        {
            //定义表格变量
            StringBuilder tableTitle = new StringBuilder();
            //生成表格标题
            tableTitle.AppendLine("	<tr>");
            tableTitle.AppendLine("\r\t      <td width=\"7%\" align=\"center\" class=\"td1\" style=\"height: 25px\">员工编号</td>");
            tableTitle.AppendLine("  \r\t      <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">员工姓名</td>");
            tableTitle.AppendLine(" \r\t       <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">所在部门</td>");
            tableTitle.AppendLine("\r\t      <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">所在岗位</td>");
            tableTitle.AppendLine("\r\t       <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">岗位职等</td>");

            //工资项数据存在时
            int salaryCount = 0;
            if (dtSalaryItem != null && dtSalaryItem.Rows.Count > 0)
            {

                //获取工资项总数
                salaryCount = dtSalaryItem.Rows.Count;
                //遍历所有工资项，设置到表格中
                for (int i = 0; i < salaryCount; i++)
                {
                    //获取工资项名称
                    string PayFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "PayFlag");
                    if (PayFlag == "0")
                    {
                        string salaryName = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "ItemName");
                        //设置工资项名称
                        tableTitle.AppendLine(" \r\t   <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">"
                                        + "<input type='hidden' id='txtSalaryName_" + (i + 1).ToString() + "' value='" + salaryName + "' />"
                                        + "<input type='hidden' id='txtSalaryFlag_" + (i + 1).ToString() + "' value='"
                                        + GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "PayFlag") + "' />"
                                        + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
                                        + GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "ItemNo") + "' />" + salaryName + "</td>");
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            tableTitle.AppendLine("\r\t       <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">计件工资</td>");
            tableTitle.AppendLine("  \r\t      <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">计时工资</td>");
            tableTitle.AppendLine(" \r\t      <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">公司提成</td>");
            tableTitle.AppendLine(" \r\t      <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">部门提成</td>");
            tableTitle.AppendLine(" \r\t      <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">个人业务提成</td>");
            tableTitle.AppendLine(" \r\t      <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">产品单品提成</td>");
            tableTitle.AppendLine(" \r\t      <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">绩效工资</td>");
            tableTitle.AppendLine("  \r\t      <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">其他应付工资</td>");
            tableTitle.AppendLine(" \r\t      <td width=\"5%\" align=\"center\" class=\"td1\" style=\"height: 25px\">应付工资</td>");
            if (dtSalaryItem != null && dtSalaryItem.Rows.Count > 0)
            {
                //获取工资项总数
                salaryCount = dtSalaryItem.Rows.Count;
                //遍历所有工资项，设置到表格中
                for (int i = 0; i < salaryCount; i++)
                {
                    string PayFlag = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "PayFlag");
                    if (PayFlag == "1")
                    {
                        //获取工资项名称
                        string salaryName = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "ItemName");
                        //设置工资项名称
                        tableTitle.AppendLine("\r\t    <td width=\"7%\" align=\"center\" class=\"td1\" style=\"height: 25px\">"
                                        + "<input type='hidden' id='txtSalaryName_" + (i + 1).ToString() + "' value='" + salaryName + "' />"
                                        + "<input type='hidden' id='txtSalaryFlag_" + (i + 1).ToString() + "' value='"
                                        + GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "PayFlag") + "' />"
                                        + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
                                        + GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "ItemNo") + "' />" + salaryName + "</td>");
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            tableTitle.AppendLine("\r\t       <td width=\"7%\" align=\"center\" class=\"td1\" style=\"height: 25px\">社会保险</td>");
            tableTitle.AppendLine(" \r\t      <td width=\"7%\" align=\"center\" class=\"td1\" style=\"height: 25px\">个人所得税</td>");
            tableTitle.AppendLine("\r\t       <td width=\"7%\" align=\"center\" class=\"td1\" style=\"height: 25px\">其他应扣款</td>");
            tableTitle.AppendLine("\r\t       <td width=\"7%\" align=\"center\" class=\"td1\" style=\"height: 25px\">扣款合计</td>");
            tableTitle.AppendLine("\r\t        <td width=\"7%\" align=\"center\" class=\"td4\" style=\"height: 25px\">应发工资</td>");

            tableTitle.AppendLine("	</tr></thead>");
            //返回表格语句
            return tableTitle.ToString();
        }
    }
}

/**********************************************
 * 类作用：   个人所得税
 * 建立人：   吴志强
 * 建立时间： 2009/05/18
 ***********************************************/
using System;
using XBase.Model.Office.HumanManager;
using XBase.Data.Office.HumanManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;
using System.Collections;
using System.Configuration;

namespace XBase.Business.Office.HumanManager
{
    /// <summary>
    /// 类名：TaxCalculateBus
    /// 描述：个人所得税
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/18
    /// 最后修改时间：2009/05/18
    /// </summary>
    ///
    public class TaxCalculateBus
    {

        #region 计算员工的个人所得税
        /// <summary>
        /// 计算员工的个人所得税
        /// </summary>
        /// <param name="dtEmplInfo">员工信息</param>
        /// <param name="salaryMonth">工资年月</param>
        /// <returns></returns>
        //public static DataTable CalculateEmployeeTax(DataTable dtEmplInfo, string salaryMonth, EmployeeSearchModel searchModel)
        //{
        //    //人员信息不存在时，查询员工信息
        //    if (dtEmplInfo == null || dtEmplInfo.Rows.Count < 1)
        //    {
        //        //查询员工信息
        //        dtEmplInfo = EmployeeInfoBus.SearchEmplInfo(searchModel);
        //        //员工信息仍然不存在时，返回NULL
        //        if (dtEmplInfo == null || dtEmplInfo.Rows.Count < 1)
        //        {
        //            return null;
        //        }

        //    }
        //    //获取登陆用户信息
        //    UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //    //获取员工固定工资
        //    DataTable dtFixedSalary = SalaryEmployeeDBHelper.SearchSalaryEmployeeInfo(userInfo.CompanyCD);
        //    //获取员工计件工资
        //    DataTable dtPeiceSalary = PieceworkSalaryDBHelper.GetMonthPieceworkSalary(userInfo.CompanyCD, salaryMonth);
        //    //获取员工计时工资
        //    DataTable dtTimeSalary = TimeSalaryDBHelper.GetMonthTimeSalary(userInfo.CompanyCD, salaryMonth);
        //    //获取员工提成工资
        //    DataTable dtCommSalary = CommissionSalaryDBHelper.GetMonthCommSalary(userInfo.CompanyCD, salaryMonth);

        //    //工资总额
        //    dtEmplInfo.Columns.Add(new DataColumn("TotalSalary", System.Type.GetType("System.String")));
        //    //税率
        //    dtEmplInfo.Columns.Add(new DataColumn("TaxRate", System.Type.GetType("System.String")));
        //    //税额
        //    dtEmplInfo.Columns.Add(new DataColumn("TotalTax", System.Type.GetType("System.String")));

        //    //遍历所有的员工信息，获取工资总额
        //    for (int i = 0; i < dtEmplInfo.Rows.Count; i++ )
        //    {
        //        //变量定义
        //        decimal totalSalary = 0;
        //        //获取员工ID
        //        string employeeID = GetSafeData.GetStringFromInt(dtEmplInfo.Rows[i], "ID");
        //        //固定工资
        //        DataRow[] drFixedSalary = dtFixedSalary.Select("EmployeeID=" + employeeID);
        //        //遍历所有固定工资项，计算工资额
        //        for (int j = 0; j < drFixedSalary.Length; j++)
        //        {
        //            //该项工资额为DBNull时，增加该工资项
        //            if (drFixedSalary[j]["SalaryMoney"] != DBNull.Value)
        //            {
        //                //增加工资额
        //                totalSalary += Convert.ToDecimal(drFixedSalary[j]["SalaryMoney"]); 
        //            }
        //        }

        //        //计件工资
        //        DataRow[] drPeiceSalary = dtPeiceSalary.Select("EmployeeID=" + employeeID);
        //        if (drPeiceSalary.Length > 0)
        //        {
        //            totalSalary += Convert.ToDecimal(drPeiceSalary[0]["TotalSalary"]);
        //        }
        //        //计时工资
        //        DataRow[] drTimeSalary = dtTimeSalary.Select("EmployeeID=" + employeeID);
        //        if (drTimeSalary.Length > 0)
        //        {
        //            totalSalary += Convert.ToDecimal(drTimeSalary[0]["TotalSalary"]);
        //        }
        //        //提成工资
        //        DataRow[] drCommSalary = dtCommSalary.Select("EmployeeID=" + employeeID);
        //        if (drCommSalary.Length > 0)
        //        {
        //            totalSalary += Convert.ToDecimal(drCommSalary[0]["TotalSalary"]);
        //        }

        //        //工资总额
        //        dtEmplInfo.Rows[i]["TotalSalary"] = totalSalary.ToString();
        //        decimal[] taxInfo = CalculateTax(totalSalary);
        //        //税率
        //        dtEmplInfo.Rows[i]["TaxRate"] = StringUtil.TrimZero(taxInfo[0].ToString());
        //        //税额
        //        dtEmplInfo.Rows[i]["TotalTax"] = StringUtil.TrimZero(taxInfo[1].ToString());

        //    }

        //    return dtEmplInfo;
        //}
        #endregion

        #region 计算个人所得税
        /// <summary>
        /// 计算个人所得税
        /// </summary>
        /// <param name="totalSalary">工资总额</param>
        /// <returns></returns>
        public static decimal[] CalculateTax(decimal totalSalary)
        {
            //定义税率
            decimal taxRate = 0;
            decimal totalTax = 0;
            //获取缴税起征点
            decimal taxStartMoney = Convert.ToDecimal(ConfigurationManager.AppSettings["TAX_START_MONEY"]);
            //缴税金额
            decimal calTaxMoney = totalSalary - taxStartMoney;
            //需要缴税时，获取缴税税率资料
            if (calTaxMoney > 0)
            {
                //获取缴税信息
                DataTable dtTaxInfo = IncomeTaxPercentDBHelper.GetTaxRate(calTaxMoney.ToString());
                //个人所得税信息存在时
                if (dtTaxInfo != null && dtTaxInfo.Rows.Count > 0)
                {
                    //税率
                    taxRate = GetSafeData.ValidateDataRow_Decimal(dtTaxInfo.Rows[0], "TaxPercent");
                    //速算扣除数
                    decimal minusMoney = GetSafeData.ValidateDataRow_Decimal(dtTaxInfo.Rows[0], "MinusMoney");
                    //计算税额
                    totalTax = calTaxMoney * taxRate / 100 - minusMoney;
                }
            }
            decimal[] taxInfo = { taxRate, totalTax };
            return taxInfo;
        }
        #endregion

    }
}

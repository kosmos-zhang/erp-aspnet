/**********************************************
 * 描述：     自动生成凭证业务处理
 * 建立人：   莫申林
 * 建立时间： 2010/03/29
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using System.Data.SqlClient;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;

namespace XBase.Business.Office.FinanceManager
{
   public class AutoVoucherBus
    {
        /// <summary>
        /// 业务单自动生成凭证--自动登帐--启用自动生成凭证时生成凭证后自动回写源单登记凭证状态
        /// </summary>
        /// <param name="TemplateType">模板类别（1.采购订单，2. 销售订单，3. 委托代销单，4. 销售退货单，5. 采购入库，6. 其他出库单，7. 销售出库单，8. 其他入库单，9.收款单，10.付款单）</param>
        /// <param name="BillAmount">业务单含税金额合计</param>
        /// <param name="FromTBInfo">来源表信息，格式来源表名+,+来源表主键（业务单主表名称（带上架构）,自动生成凭证的业务单主键）必填</param>
        /// <param name="CurrencyInfo">业务单币种信息，格式为（币种ID,汇率）必填，若业务单无币种汇率，则默认传本位币及汇率</param>
        /// <param name="ProOrCustID">科目辅助核算ID（默认为业务单中的供应商或客户主键）</param>
        /// <param name="returnValue">returnValue=0 业务单未设凭证模板,returnValue=1 企业不启用业务单自动生成凭证,returnValue = 2 企业不启用自动生成凭证自动登帐, returnValue = 3 自动生成凭证失败 ，returnValue = "4" 回写业务单登记凭证状态成功，returnValue = "5" 回写业务单登记凭证状态失败</param>
        /// <returns></returns>
       public static bool AutoVoucherInsert(int TemplateType, decimal BillAmount, string FromTBInfo, string CurrencyInfo, int ProOrCustID, out string returnValue)
       {
            
           try
           {

               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编号
               string IsVoucher = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsVoucher == true ? "1" : "0";//是否自动生成凭证
               string IsApply = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsApply == true ? "1" : "0";//是否自动审核登帐

               return AutoVoucherDBHelper.AutoVoucherInsert(TemplateType, CompanyCD, IsVoucher, IsApply, BillAmount, FromTBInfo, CurrencyInfo, ProOrCustID,out returnValue);
           }
           catch(Exception ex)
           {
               throw ex;
           }

       }


       #region 业务单反确认时--已登记凭证且已登帐的业务单--反登帐-->删除自动生成的凭证-->更新业务单登记凭证状态
        /// <summary>
        /// 业务单反确认时--已登记凭证且已登帐的业务单--反登帐-->删除自动生成的凭证-->更新业务单登记凭证状态
        /// </summary>
        /// <param name="FromTBInfo">业务单表及主键，格式为（表名（带上架构）,主键）</param>
       /// <param name="returnValue">returnValue = "0";//未自动生成凭证，不做处理，returnValue = "1";//反登帐成功，returnValue = "2";//反登帐失败，returnValue = "3";//删除对应凭证并更新原始业务单凭证登记状态成功，returnValue = "4";//删除对应凭证并更新原始业务单凭证登记状态失败</param>
        /// <returns></returns>
       public static bool AntiConfirmVoucher(string FromTBInfo,out string returnValue)
       {
           try
           {
               return AutoVoucherDBHelper.AntiConfirmVoucher(FromTBInfo,out returnValue);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion
    }
}

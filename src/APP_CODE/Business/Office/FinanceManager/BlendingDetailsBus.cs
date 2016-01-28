/**********************************************
 * 类作用：   勾兑明细表业务层处理
 * 建立人：   莫申林
 * 建立时间： 2009/06/27
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;
using System.Collections;

namespace XBase.Business.Office.FinanceManager
{
  
   public class BlendingDetailsBus
    {
       /// <summary>
       /// 默认构造函数
       /// </summary>
       public BlendingDetailsBus()
       {
 
       }
       private BlendingDetailsDBHelper dta=new BlendingDetailsDBHelper();

       #region  获取业务单对应的源单的详细信息
       /// <summary>
       /// 获取业务单对应的源单的详细信息
       /// </summary>
       /// <param name="BillingID">业务单主键ID</param>
       /// <returns></returns>
       public DataTable GetSourceDt(int BillingID,string PayOrIncomeType)
       {
           try
           {
              string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
              return dta.GetSourceDt(BillingID,CompanyCD,PayOrIncomeType);

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 添加勾兑明细
       /// <summary>
       /// 添加勾兑明细
       /// </summary>
       /// <param name="MyList"></param>
       /// <returns></returns>
       public bool InSertBlendingDetails(ArrayList MyList,out string ListID)
       {
           try
           {
               return dta.InSertBlendingDetails(MyList,out ListID);

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion
       #region 判读勾兑明细表中是否存在记录
       /// <summary>
       /// 判读勾兑明细表中是否存在记录
       /// </summary>
       /// <param name="BillingID">业务单ID</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns></returns>
       public bool IsExist(int BillingID, string PayOrIncomeType)
       {
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           try
           {
               return dta.IsExist(BillingID, CompanyCD,PayOrIncomeType);

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 根据勾兑明细表中是否存在BillingID记录，若存在则修改对应记录，若不存在则插入勾兑明细表
       /// <summary>
       /// 根据勾兑明细表中是否存在BillingID记录，若存在则修改对应记录，若不存在则插入勾兑明细表
       /// </summary>
       /// <param name="MyList"></param>
       /// <returns></returns>
       public bool insertOrUpateSouce(ArrayList MyList,out string listID)
       {
           try
           {
               listID = string.Empty;
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               bool ret = false;
               if (MyList.Count > 0)
               {
                   bool rev = dta.IsExist((MyList[0] as BlendingDetailsModel).BillingID, CompanyCD, (MyList[0] as BlendingDetailsModel).PayOrInComeType);
                   if (rev)
                   {
                       ret = dta.UpdateBalendingDetails(MyList,out listID);
                   }
                   else
                   {
                       ret = dta.InSertBlendingDetails(MyList,out listID);
                   }
               }
               return ret;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion


       #region 自动勾兑
       /// <summary>
       /// 自动勾兑
       /// </summary>
       /// <param name="BillingID">业务单ID</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns></returns>
       public bool AutoBlending(int BillingID, string PayOrIncomeType,decimal PayAmount,out string listID,out string OutAmount)
       {
           OutAmount = string.Empty;
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           int Excutor = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
           DataTable dt = dta.GetSourceDt(BillingID, CompanyCD,PayOrIncomeType);
           ArrayList MyList = new ArrayList();
           string rp = string.Empty;
           if (dta.IsExist(BillingID, CompanyCD,PayOrIncomeType))
           {
               foreach (DataRow dr in dt.Rows)
               {
                   BlendingDetailsModel Model = new BlendingDetailsModel();
                   Model.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

                   decimal NAmount = Convert.ToDecimal(dr["NAccounts"].ToString());
                   decimal TotalAmount=Convert.ToDecimal(dr["TotalPrice"].ToString());
                   Model.TotalPrice = TotalAmount;
                   Model.BillingID = BillingID;
                   Model.ID = int.Parse(dr["ID"].ToString());
                   Model.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                   Model.ExecutDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                   Model.Executor = Excutor;
                   Model.CompanyCD = CompanyCD;
                   Model.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                   Model.ContactUnits = dr["ContactUnits"].ToString();
                   Model.BillCD = dr["BillCD"].ToString();
                   Model.PayOrInComeType = PayOrIncomeType;
                   Model.BillingType = dr["BillingType"].ToString();
                   Model.InvoiceType = dr["InvoiceType"].ToString();
                   Model.SourceDt = dr["SourceDt"].ToString();
                   Model.SourceID = int.Parse(dr["SourceID"].ToString());
                   Model.CurrencyType = int.Parse(dr["CurrencyType"].ToString());
                   Model.CurrencyRate = Convert.ToDecimal(dr["Rate"].ToString());



                   if (PayAmount == 0)
                   {
                       Model.YAccounts = Convert.ToDecimal(dr["YAccounts"].ToString());
                       Model.NAccounts = Convert.ToDecimal(dr["NAccounts"].ToString());
                       if (Model.NAccounts > 0)
                       {
                           Model.Status = "0";
                       }
                       else
                       {
                           Model.Status = "1";
                       }
                       rp = "0";
                   }
                   else
                   {
                       if (PayAmount > NAmount)
                       {
                           Model.YAccounts = TotalAmount;
                           Model.NAccounts = 0;
                           PayAmount = PayAmount - NAmount;
                           Model.Status = "1";
                           rp = NAmount.ToString();
                       }
                       else if (PayAmount <= NAmount)
                       {
                           Model.YAccounts =Convert.ToDecimal(dr["YAccounts"].ToString())+PayAmount;
                           Model.NAccounts = Convert.ToDecimal(dr["NAccounts"].ToString()) - PayAmount;
                           if (Model.NAccounts > 0)
                           {
                               Model.Status = "0";
                           }
                           else
                           {
                               Model.Status = "1";
                           }
                           rp = PayAmount.ToString();
                           PayAmount = 0;
                           
                       }
                   }
                   MyList.Add(Model);
                   OutAmount += rp + ",";
               }
           }
           else
           {
               foreach (DataRow dr in dt.Rows)
               {
                   BlendingDetailsModel Model = new BlendingDetailsModel();
                   Model.BillingID = BillingID;
                   Model.CompanyCD = CompanyCD;
                   Model.ExecutDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                   Model.Executor = Excutor;
                   Model.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                   Model.ContactUnits = dr["ContactUnits"].ToString();
                   Model.BillCD = dr["BillCD"].ToString();
                   Model.PayOrInComeType = PayOrIncomeType;
                   Model.BillingType = dr["BillingType"].ToString();
                   Model.InvoiceType = dr["InvoiceType"].ToString();
                   Model.SourceDt = dr["SourceDt"].ToString();
                   Model.SourceID = int.Parse(dr["SourceID"].ToString());
                   Model.CurrencyType = int.Parse(dr["CurrencyType"].ToString());
                   Model.CurrencyRate = Convert.ToDecimal(dr["Rate"].ToString());
                   decimal TotalAmount = Convert.ToDecimal(dr["TotalPrice"].ToString());
                   Model.TotalPrice = TotalAmount;
                   if (PayAmount == 0)
                   {
                       Model.YAccounts = 0;
                       Model.NAccounts = TotalAmount;
                       Model.Status = "0";
                       rp = "0";
                   }
                   else
                   {
                       if (PayAmount > TotalAmount)
                       {
                           Model.YAccounts = TotalAmount;
                           Model.NAccounts = 0;
                           PayAmount = PayAmount - TotalAmount;
                           Model.Status = "1";
                           rp = TotalAmount.ToString();
                       }
                       else if (PayAmount <= TotalAmount)
                       {
                           Model.YAccounts = PayAmount;
                           Model.NAccounts = TotalAmount - PayAmount;
                           if (Model.NAccounts > 0)
                           {
                               Model.Status = "0";
                           }
                           else
                           {
                               Model.Status = "1";
                           }
                           rp = PayAmount.ToString();
                           PayAmount = 0;
                       }
                   }
                   MyList.Add(Model);
                   OutAmount += rp + ",";

               }
           }
           listID = string.Empty;
           OutAmount = OutAmount.TrimEnd(new char[] { ',' });
           bool returnValue = insertOrUpateSouce(MyList,out listID);
           return returnValue;

       }
       #endregion
       #region 修改时获取勾兑明细记录
       /// <summary>
       /// 修改时获取勾兑明细记录
       /// </summary>
       /// <param name="BillingID">业务单ID</param>
       /// <param name="PayOrIncomeType">收付款单类别</param>
       /// <param name="PayOrInComeID">收付款单ID</param>
       /// <returns></returns>
       public DataTable GetEditBlendingSource(int BillingID, string PayOrIncomeType, int PayOrInComeID)
       {
           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               return dta.GetEditBlendingSource(BillingID, PayOrIncomeType, CompanyCD,PayOrInComeID);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        #endregion

       #region 修改收付款单，回滚当前单据勾兑的金额
       /// <summary>
       /// 修改收付款单，回滚当前单据勾兑的金额
       /// </summary>
       /// <param name="BillingID">业务单ID</param>
       /// <param name="PayOrIncomeType">收付款单类别</param>
       /// <param name="PayOrInComeID">收付款单ID</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns></returns>
       public bool UpdateEditBlending(int BillingID, string PayOrIncomeType, int PayOrInComeID)
       {
           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               return dta.UpdateEditBlending(BillingID, PayOrIncomeType, CompanyCD, PayOrInComeID);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion
       
        #region 获取勾兑明细
       /// <summary>
       /// 获取勾兑明细
       /// </summary>
       /// <param name="CompanyCD">公司编码</param>
       /// <param name="PayOrInComeType">来源表类别</param>
       /// <param name="BillingID">来源表ID主键</param>
       /// <returns></returns>
       public DataTable GetBlendingSourse(string BillingID,string PayOrInComeType)
       {
           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               return dta.GetBlendingSourse(CompanyCD, BillingID, PayOrInComeType);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        #endregion
       #region 删除勾兑明细
       /// <summary>
       /// 删除勾兑明细
       /// </summary>
       /// <param name="CompanyCD">公司编码</param>
       /// <param name="PayOrInComeType">收付款类别</param>
       /// <param name="BillingID">业务单ID主键</param>
       /// <returns></returns>
       public bool DeleteBlendingDetails(string BillingID, string PayOrInComeType)
       {
           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               return dta.DeleteBlendingDetails(CompanyCD, BillingID, PayOrInComeType);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region  根据凭证主表的来源表及来源表主键
       /// <summary>
       /// 根据凭证主表的来源表及来源表主键__
       /// </summary>
       /// <param name="FormTBName">来源表</param>
       /// <param name="FileValue">来源表主键集</param>
       /// <returns></returns>
       public DataTable GetBlendingSoureByTB(string FormTBName, string FileValue)
       {
           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               return dta.GetBlendingSoureByTB(FormTBName, FileValue, CompanyCD);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion
    }

}

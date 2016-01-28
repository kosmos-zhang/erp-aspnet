 /**********************************************
 * 描述：     凭证管理业务处理
 * 建立人：   莫申林
 * 建立时间： 2009/04/09
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;
using System.Collections;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.FinanceManager
{
    public class VoucherBus
    {



        #region 输出系统日志
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">异常信息</param>
        private static void WriteSystemLog(UserInfoUtil userInfo, Exception ex)
        {
            /* 
             * 出现异常时，输出系统日志到文本文件 
             * 考虑出现异常情况比较少，尽管一个方法可能多次异常，
             * 但还是考虑将异常日志的变量定义放在catch里面
             */
            //定义变量
            LogInfo logSys = new LogInfo();
            //设置日志类型 需要指定为系统日志
            logSys.Type = LogInfo.LogType.SYSTEM;
            //指定系统日志类型 出错信息
            logSys.SystemKind = LogInfo.SystemLogKind.SYSTEM_ERROR;
            //指定登陆用户信息
            logSys.UserInfo = userInfo;
            //设定模块ID
            logSys.ModuleID = ConstUtil.MODULE_ID_VOUCHER_ADD;
            //描述
            logSys.Description = ex.ToString();
            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion

        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <returns></returns>
        private static LogInfoModel InitLogInfo(string wcno)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_VOUCHER_ADD;

           
            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;
            return logModel;
        }
        #endregion

        #region  获取科目明细信息
        /// <summary>
        /// 获取科目明细信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCompanyInfo()
        {
            try
            {
                return VoucherDBHelper.GetCompanyInfo();
            }
            catch(Exception exx)
            {
                throw exx;
            }
        }
        #endregion

        #region 凭证提交插入主,明细表信息--事务处理

        /// <summary>
        /// 凭证提交插入主,明细表信息--事务处理
        /// </summary>
        /// <param name="Model">主表Model</param>
        /// <param name="List">明细表Model加入List中</param>
        /// <returns></returns>
        public static bool InsertIntoAttestBill(AttestBillModel Model, ArrayList List, out int ID, string rblAccount)
        {
            try
            {
               UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
               string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
               LogInfoModel logModel = InitLogInfo(Model.AttestNo);
               logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
               //设置操作日志类型 修改
               logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_ATTESTBILL;

                bool suuc=VoucherDBHelper.InsertIntoAttestBill(Model,List,out ID,rblAccount);
                if (suuc)
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                LogDBHelper.InsertLog(logModel);

                for (int i = 0; i < List.Count; i++)
                {
                    LogInfoModel logModell = InitLogInfo((List[i] as AttestBillDetailsModel).Abstract);
                    logModell.ObjectName = ConstUtil.CODING_RULE_TABLE_ATTESTBILLDETIALS;
                    logModell.Element = ConstUtil.LOG_PROCESS_INSERT;
                    if (suuc)
                        logModell.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                    else
                        logModell.Remark = ConstUtil.LOG_PROCESS_FAILED;
                    LogDBHelper.InsertLog(logModell);

                }
                    return suuc;
            }
            catch (Exception exx)
            {
                throw exx;
            }
        }
        #endregion

        #region 根据公司CopanyCD和凭证日期判断当天凭证号是否重复

        /// <summary>
        /// 根据公司CopanyCD和凭证日期判断当天凭证号是否重复
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="NowDate">当前日期</param>
        /// <param name="AttestNo">凭证号</param>
        /// <returns></returns>
        public static bool IsAttestNo(string AttestNo,string CompanyCD, string NowDate)
        {
            try
            {
                return VoucherDBHelper.IsAttestNo(AttestNo,CompanyCD, NowDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  根据条件获取凭证主表信息
        /// <summary>
        /// 根据条件获取凭证主表信息
        /// </summary>
        /// <param name="queryStr">查询条件</param>
        /// <returns></returns>
        public static DataTable GetAttestBillInfo(string queryStr)
        {
            try
            {
                return VoucherDBHelper.GetAttestBillInfo(queryStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据ID获取凭证主表信息
        /// <summary>
        /// 根据ID获取凭证主表信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetVoucherInfo(int id)
        {
            //if (string.IsNullOrEmpty(CompanyCD)) return null;
            try
            {
                return VoucherDBHelper.GetVoucherInfo(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据主表ID获取凭证明细表信

        /// <summary>
        /// 根据主表ID获取凭证明细表信息
        /// </summary>
        /// <param name="AttestBillID"></param>
        /// <returns></returns>
        public static DataTable GetVoucherDetailsInfo(int AttestBillID)
        {
            try
            {
                return VoucherDBHelper.GetVoucherDetailsInfo(AttestBillID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 系统自动获取凭证号
        /// <summary>
        /// 系统自动获取凭证号
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="NowDate">凭证日期</param>
        /// <returns></returns>
        public static string GetMaxAttestNo(string CompanyCD, string NowDate)
        {
            try
            {
                return VoucherDBHelper.GetMaxAttestNo(CompanyCD,NowDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 更新凭证信息
        /// <summary>
        /// 更新凭证信息
        /// </summary>
        /// <param name="Model">主表Model信息</param>
        /// <param name="List">明细表信息</param>
        /// <returns></returns>
        public static bool UpdateAttestBillInfo(AttestBillModel Model, ArrayList List)
        {
            try
            {
                return VoucherDBHelper.UpdateAttestBillInfo(Model,List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除凭证信息
        /// <summary>
        /// 删除凭证信息
        /// </summary>
        /// <param name="deleteNos"></param>
        /// <returns></returns>
        public static bool DeleteAttestBillInfo(string deleteNos)
        {
            try
            {
                return VoucherDBHelper.DeleteAttestBillInfo(deleteNos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 更新凭证信息时根据公司CopanyCD和凭证日期判断当天凭证号是否重复

        /// <summary>
        /// 更新凭证信息时根据公司CopanyCD和凭证日期判断当天凭证号是否重复
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="NowDate">当前日期</param>
        /// <param name="AttestNo">凭证号</param>
        /// <returns></returns>
        public static bool IsAttestNo(string AttestNo, string CompanyCD, string VoucherDate, string AttestBillID)
        {
            try
            {
                return VoucherDBHelper.IsAttestNo(AttestNo,CompanyCD,VoucherDate,AttestBillID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 将金钱数字转化成对应的list数组
        /// <summary>
        /// 将金钱数字转化成对应的list数组
        /// </summary>
        /// <param name="Amount"></param>
        /// <returns></returns>
        public static ArrayList GetAmountList(string Amount)
        {
            try
            {
                return VoucherDBHelper.GetAmountList(Amount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取金额总值

        /// <summary>
        /// 获取金额总值
        /// </summary>
        /// <param name="VoucherID"></param>
        /// <returns></returns>
        public static decimal GetSumAmount(int VoucherID)
        {
            try
            {
                return VoucherDBHelper.GetSumAmount(VoucherID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 人民币转换大写

        /// <summary>
        /// 人民币转换大写
        /// </summary>
        /// <param name="num">对应数字金额</param>
        /// <returns></returns>
        public static string CmycurD(decimal num)
        {
            try
            {
                return VoucherDBHelper.CmycurD(num);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 凭证状态设置

        /// <summary>
        /// 凭证状态设置
        /// </summary>
        /// <param name="ids">主表ID集</param>
        /// <param name="value">状态值</param>
        /// <param name="Field">字段名称</param>
        /// <returns></returns>
        public static bool SetStatus(string ids, string value, string Field, int flag)
        {
            try
            {
                return VoucherDBHelper.SetStatus(ids,value,Field,flag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取账簿单据编号

        /// <summary>
        /// 获取账簿单据编号
        /// </summary>
        /// <returns>编号</returns>
        public static string GetAccountBookOrderCode()
        {
            try
            {
                return VoucherDBHelper.GetAccountBookOrderCode();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion 

        #region 凭证登帐操作--事务处理
        /// <summary>
        /// 凭证登帐操作--事务处理
        /// </summary>
        /// <param name="AttestBillID"></param>
        /// <returns></returns>
        public static bool InsertAccount(int AttestBillID)
        {
            try
            {
                return VoucherDBHelper.InsertAccount(AttestBillID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion 

        #region 反登记账簿
        /// <summary>
        /// 反登记账簿
        /// </summary>
        /// <param name="id">主表ID</param>
        /// <returns></returns>
        public static bool AntiAccount(string id)
        {
            try
            {
                return VoucherDBHelper.AntiAccount(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 判断凭证是否能删除，返回提示
        /// <summary>
        /// 判断凭证是否能删除，返回提示
        /// </summary>
        /// <param name="ids">凭证ID集</param>
        /// <returns></returns>
        public static string IsCanDel(string ids)
        {
            try
            {
                return VoucherDBHelper.IsCanDel(ids);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取人员名称
        /// <summary>
        /// 获取人员名称
        /// </summary>
        /// <param name="EmplyeeID"></param>
        /// <returns></returns>
        public static string GetEmplyeeName(string EmplyeeID)
        {
            try
            {
                return VoucherDBHelper.GetEmplyeeName(EmplyeeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 判断凭证列表是否能审核或反审核

        /// <summary>
        /// 判断凭证列表是否能审核或反审核
        /// </summary>
        /// <param name="ids">凭证ID集</param>
        /// <param name="flag">flag=0：审核1：反审核</param>
        /// <returns></returns>
        public static string IsCanAuditOrAntiAuditor(string ids, int flag)
        {
            try
            {
                return VoucherDBHelper.IsCanAuditOrAntiAuditor(ids,flag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据科目编码获取其所有下级科目

        /// <summary>
        /// 根据科目编码获取其所有下级科目
        /// </summary>
        /// <param name="SubjectsCD">科目编码</param>
        /// <returns></returns>
        public static string GetSubjectsNextCD(string SubjectsCD)
        {
            try
            {
                return VoucherDBHelper.GetSubjectsNextCD(SubjectsCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 根据科目编码获取其所有父级科目

        /// <summary>
        /// 根据科目编码获取其所有父级科目
        /// </summary>
        /// <param name="SubjectsCD">科目编码</param>
        /// <returns></returns>
        public static string GetSubjectsPerCD(string SubjectsCD)
        {
            try
            {
                return VoucherDBHelper.GetSubjectsPerCD(SubjectsCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取会计科目名称
        /// <summary>
        /// 获取会计科目名称
        /// </summary>
        /// <param name="SubjectsCD">科目编码</param>
        /// <returns></returns>
        public static string GetSubjectsName(string SubjectsCD)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                if (string.IsNullOrEmpty(CompanyCD))
                    return "";
                else
                return VoucherDBHelper.GetSubjectsName(SubjectsCD,CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取科目对应的辅助科目

        /// <summary>
        /// 获取科目对应的辅助科目
        /// </summary>
        /// <param name="SubjectsCD"></param>
        /// <returns></returns>
        public static string GetSubjectsAuciliaryCD(string SubjectsCD)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                if (string.IsNullOrEmpty(CompanyCD))
                    return "";
                else
                    return VoucherDBHelper.GetSubjectsAuciliaryCD(SubjectsCD, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 凭证列表页面显示数据源
        /// <summary>
        /// 凭证列表页面显示数据源
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static DataTable AttestBillSource(string queryStr)
        {
            try
            {
                return VoucherDBHelper.AttestBillSource(queryStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取会计科目全名称

        /// <summary>
        /// 获取会计科目全名称
        /// </summary>
        /// <param name="AttestBillDetailsID"></param>
        /// <returns></returns>
        public static string GetSubjectsNamesInfo(string AttestBillDetailsID)
        {
            try
            {
                return VoucherDBHelper.GetSubjectsNamesInfo(AttestBillDetailsID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取会计科目全名

        /// <summary>
        /// 获取会计科目全名
        /// </summary>
        /// <param name="SubjectsCD"></param>
        /// <param name="SubjectsDetails"></param>
        /// <param name="formTBName"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string GetSubJectsName(string SubjectsCD, string SubjectsDetails, string formTBName, string FileName)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码 
                if (string.IsNullOrEmpty(CompanyCD))
                    return "";
                else
                return VoucherDBHelper.GetSubJectsName(SubjectsCD, SubjectsDetails, formTBName, FileName,CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 获取出库单数据源--添加凭证

        /// <summary>
        /// 获取出库单数据源--添加凭证
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static DataTable GetV_StorangOutSource(string queryStr)
        {
            try
            {
                return VoucherDBHelper.GetV_StorangOutSource(queryStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 获取出纳数据源--添加凭证
        /// <summary>
        /// 获取出纳数据源--添加凭证
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static DataTable GetV_CashierSource(string queryStr)
        {
            try
            {
                return VoucherDBHelper.GetV_CashierSource(queryStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 更新流水帐中登记凭证后的原始单据状态
        /// <summary>
        /// 更新流水帐中登记凭证后的原始单据状态
        /// </summary>
        /// <param name="nos"></param>
        /// <param name="fromTb"></param>
        /// <returns></returns>
        public static bool UpateRunningAccountStatus(string nos, string fromTb)
        {
            try
            {
                return VoucherDBHelper.UpateRunningAccountStatus(nos,fromTb);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 根据科目名称，绑定所在科目的所有下级科目

        /// <summary>
        /// 根据科目名称，绑定所在科目的所有下级科目
        /// </summary>
        /// <param name="SubjectsName">科目名称</param>
        /// <returns></returns>
        public static DataTable GetSubjectsInfo(string SubjectsName)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                return VoucherDBHelper.GetSubjectsInfo(SubjectsName,CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 根据科目名称，绑定所在科目的所有下级科目
        /// <summary>
        /// 根据科目名称，绑定所在科目的所有下级科目
        /// </summary>
        /// <param name="SubjectsName">科目名称</param>
        /// <returns></returns>
        public static DataTable GetSubjectsCDInfo(string SubjectsName)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                return VoucherDBHelper.GetSubjectsCDInfo(SubjectsName, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取应收/应付汇总表数据源


        /// <summary>
        /// 获取应收/应付汇总表数据源
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="subjectsCD">会计科目</param>
        /// <param name="FormTBName">来源表</param>
        /// <param name="FileName">表字段</param>
        /// <param name="SubjectsDetails">辅助核算ID</param>
        /// <param name="CurryTypeID">币种ID</param>
        /// <returns></returns>
        public static DataTable GetSummarySouse(string startDate, string endDate, string subjectsCD, string FormTBName, string FileName, string SubjectsDetails, string CurryTypeID)
        {
            try
            {
                return VoucherDBHelper.GetSummarySouse(startDate, endDate, subjectsCD,FormTBName,FileName,SubjectsDetails,CurryTypeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 科目汇总表数据源

        /// <summary>
        /// 科目汇总表数据源
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public static DataTable SubjectsTotalSource(string startDate, string endDate, string CurryTypeID)
        {
            try
            {
                return VoucherDBHelper.SubjectsTotalSource(startDate, endDate,CurryTypeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 获取现金/银行日记账数据源（本币或综合本位币）
        /// <summary>
       /// 获取现金/银行日记账数据源（本币或综合本位币）
       /// </summary>
       /// <param name="startDate">开始日期</param>
       /// <param name="endDate">结束日期</param>
       /// <param name="acceWay">收付款方式</param>
       /// <param name="BankName">银行名称</param>
       /// <param name="CurryTypeID">币种</param>
       /// <returns></returns>
        public static DataTable CashAccountSource(string startDate, string endDate, string acceWay,string BankName, string CurryTypeID)
        {
            try
            {
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                DataTable dt = new DataTable();
                dt.Columns.Add("Date");//日期
                dt.Columns.Add("Abstract");//摘要
                dt.Columns.Add("DebitAmount");//借方金额
                dt.Columns.Add("CreditAmount");//贷方金额
                dt.Columns.Add("Direction");//方向
                dt.Columns.Add("EndAmount");//余额
                dt.Columns.Add("ByOrderr");//排序
                dt.Columns.Add("ID");//源单ID
                dt.Columns.Add("SourceDT");//源单来源表
                dt.Columns.Add("Code");//源单编码
                /*从现金/银行账目视图获取不重复的日期（查询条件） 开始*/
                string DistinctDateQuery = " RetDate>='" + startDate + "' and RetDate<='" + endDate + "' and AcceWay='" + acceWay + "' and CompanyCD='"+companyCD+"' ";
                if (BankName.Trim().Length > 0)
                {
                    DistinctDateQuery += " and BankName='";
                    DistinctDateQuery += BankName;
                    DistinctDateQuery += "' ";

                }

                if (CurryTypeID.LastIndexOf(",") == -1)
                {
                    DistinctDateQuery += " and CurrencyType='";
                    DistinctDateQuery += CurryTypeID;
                    DistinctDateQuery += "' ";
                }

                DataTable DisDateDt = VoucherDBHelper.GetDistinctDateView(DistinctDateQuery);
                /*从现金/银行账目视图获取不重复的日期（查询条件） 结束*/

                /*获取上日余额开始*/
                string PreQueryStr = " RetDate<'" + startDate + "' and AcceWay='" + acceWay + "'  and CompanyCD='" + companyCD + "'  ";
                if (BankName.Trim().Length > 0)
                {
                    PreQueryStr += " and BankName='";
                    PreQueryStr += BankName;
                    PreQueryStr += "' ";

                }
                if (CurryTypeID.LastIndexOf(",") == -1)
                {
                    PreQueryStr += " and CurrencyType='";
                    PreQueryStr += CurryTypeID;
                    PreQueryStr += "' ";
                }


                decimal beginAmount = GetCashOrBankBeginAmount(PreQueryStr, CurryTypeID);
                DataRow row = dt.NewRow();
                row["Date"] = "";
                row["Abstract"] = "上日余额";
                row["DebitAmount"] = "";
                row["CreditAmount"] = "";
                row["Direction"] = CashOrBankDirection(beginAmount);
                row["EndAmount"] = Math.Abs(beginAmount).ToString("#,###0.#0");
                row["ByOrderr"] = "1";
                row["ID"] = "";
                row["SourceDT"] = "";
                row["Code"] = "";
                dt.Rows.Add(row);


                /*获取上日余额结束*/


                decimal endAmount = beginAmount;
                decimal Damount = 0;
                decimal Camount = 0;

                foreach (DataRow dr in DisDateDt.Rows)
                {

                    string thisQueryStr = "  RetDate='" + dr["RetDate"].ToString() + "' and AcceWay='" + acceWay + "'  and CompanyCD='" + companyCD + "' ";
                    if (BankName.Trim().Length > 0)
                    {
                        thisQueryStr += " and BankName='";
                        thisQueryStr += BankName;
                        thisQueryStr += "' ";

                    }

                    if (CurryTypeID.LastIndexOf(",") == -1)
                    {
                        thisQueryStr += " and CurrencyType='";
                        thisQueryStr += CurryTypeID;
                        thisQueryStr += "' ";
                    }

                    DataTable ThisDT = VoucherDBHelper.GetCashOrBankView(thisQueryStr);

                    decimal thisDebitAmount = 0;
                    decimal thisCreditAmount = 0;

                    foreach(DataRow drr in ThisDT.Rows)
                    {
                        DataRow roww = dt.NewRow();
                        roww["Date"] = drr["RetDate"].ToString();
                        roww["Abstract"] = drr["Summary"].ToString();
                        switch (int.Parse(drr["Direction"].ToString()))
                        {
                            case 0:
                                if (CurryTypeID.LastIndexOf(",") == -1)
                                {
                                    roww["DebitAmount"] = Convert.ToDecimal(drr["Amount"].ToString()).ToString("#,###0.#0");
                                    roww["CreditAmount"] = "0.00";
                                   
                                }
                                else
                                {
                                    roww["DebitAmount"] = Convert.ToDecimal(drr["Comprehensive"].ToString()).ToString("#,###0.#0");
                                    roww["CreditAmount"] = "0.00";
                                }
                                break;
                            case 1:
                                if (CurryTypeID.LastIndexOf(",") == -1)
                                {
                                    roww["DebitAmount"] = "0.00";
                                    roww["CreditAmount"] = Convert.ToDecimal(drr["Amount"].ToString()).ToString("#,###0.#0");
                                }
                                else
                                {
                                    roww["DebitAmount"] = "0.00";
                                    roww["CreditAmount"] = Convert.ToDecimal(drr["Comprehensive"].ToString()).ToString("#,###0.#0");
                                }
                                break;
                            default :
                                break;
                        }
                        endAmount = endAmount + Convert.ToDecimal(roww["DebitAmount"].ToString()) - Convert.ToDecimal(roww["CreditAmount"].ToString());
                        thisDebitAmount += Convert.ToDecimal(roww["DebitAmount"].ToString());
                        thisCreditAmount += Convert.ToDecimal(roww["CreditAmount"].ToString());

                        roww["Direction"] = CashOrBankDirection(endAmount);
                        roww["EndAmount"] = Math.Abs(endAmount).ToString("#,###0.#0");
                        roww["ByOrderr"] = "1";
                        roww["ID"] = drr["ID"].ToString();
                        roww["SourceDT"] = drr["SourceTB"].ToString();
                        roww["Code"] = drr["Code"].ToString();
                        dt.Rows.Add(roww);
                    }



                    Damount += thisDebitAmount;
                    Camount += thisCreditAmount;

                    DataRow rowww = dt.NewRow();
                    rowww["Date"] = "";
                    rowww["Abstract"] = "本日合计";
                    rowww["DebitAmount"] = thisDebitAmount.ToString("#,###0.#0");
                    rowww["CreditAmount"] = thisCreditAmount.ToString("#,###0.#0");
                    rowww["Direction"] = CashOrBankDirection(endAmount);
                    rowww["EndAmount"] = Math.Abs(endAmount).ToString("#,###0.#0");
                    rowww["ByOrderr"] = "1";
                    rowww["ID"] = "";
                    rowww["SourceDT"] = "";
                    rowww["Code"] = "";
                    dt.Rows.Add(rowww);


                }

                DataRow rowwww = dt.NewRow();
                rowwww["Date"] = "";
                rowwww["Abstract"] = "本期合计";
                rowwww["DebitAmount"] = Damount.ToString("#,###0.#0");
                rowwww["CreditAmount"] = Camount.ToString("#,###0.#0");
                rowwww["Direction"] = CashOrBankDirection(endAmount);
                rowwww["EndAmount"] = Math.Abs(endAmount).ToString("#,###0.#0");
                rowwww["ByOrderr"] = "1";
                rowwww["ID"] = "";
                rowwww["SourceDT"] = "";
                rowwww["Code"] = "";
                dt.Rows.Add(rowwww);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
       #endregion

        #region 现金/银行存款日记账（其他外币数据源）
        /// <summary>
        /// 现金/银行存款日记账（其他外币数据源）
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="acceWay">现金/银行类别</param>
        /// <param name="BankName">银行名称</param>
        /// <param name="CurryTypeID">币种</param>
        /// <returns></returns>
        public static DataTable CashAccountForeignSource(string startDate, string endDate, string acceWay, string BankName, string CurryTypeID)
        {
            try
            {

                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                DataTable dt = new DataTable();
                dt.Columns.Add("Date");//日期
                dt.Columns.Add("Abstract");//摘要
                dt.Columns.Add("DebitAmount");//综合本位币借方金额
                dt.Columns.Add("CreditAmount");//综合本位币贷方金额
                dt.Columns.Add("ForeignDebitAmount");//外币借方金额
                dt.Columns.Add("ForeignCreditAmount");//外币贷方金额
                dt.Columns.Add("Direction");//方向
                dt.Columns.Add("ForeignEndAmount");//外币余额
                dt.Columns.Add("EndAmount");//综合本位币余额
                dt.Columns.Add("ByOrderr");//排序
                dt.Columns.Add("CurrExg");//汇率
                dt.Columns.Add("ID");//源单ID
                dt.Columns.Add("SourceDT");//源单来源表
                dt.Columns.Add("Code");//源单编码



                /*从现金/银行账目视图获取不重复的日期（查询条件） 开始*/
                string DistinctDateQuery = " RetDate>='" + startDate + "' and RetDate<='" + endDate + "' and AcceWay='" + acceWay + "' and CompanyCD='" + companyCD + "' ";
                if (BankName.Trim().Length > 0)
                {
                    DistinctDateQuery += " and BankName='";
                    DistinctDateQuery += BankName;
                    DistinctDateQuery += "' ";

                }

                if (CurryTypeID.LastIndexOf(",") == -1)
                {
                    DistinctDateQuery += " and CurrencyType='";
                    DistinctDateQuery += CurryTypeID;
                    DistinctDateQuery += "' ";
                }


                DataTable DisDateDt = VoucherDBHelper.GetDistinctDateView(DistinctDateQuery);
                /*从现金/银行账目视图获取不重复的日期（查询条件） 结束*/



                /*获取上日余额开始*/
                string PreQueryStr = " RetDate<'" + startDate + "' and AcceWay='" + acceWay + "'  and CompanyCD='" + companyCD + "'  ";
                if (BankName.Trim().Length > 0)
                {
                    PreQueryStr += " and BankName='";
                    PreQueryStr += BankName;
                    PreQueryStr += "' ";

                }
                if (CurryTypeID.LastIndexOf(",") == -1)
                {
                    PreQueryStr += " and CurrencyType='";
                    PreQueryStr += CurryTypeID;
                    PreQueryStr += "' ";
                }


                string ComprehensivePreQueryStr = " RetDate<'" + startDate + "' and AcceWay='" + acceWay + "'  and CompanyCD='" + companyCD + "'  ";
                if (BankName.Trim().Length > 0)
                {
                    ComprehensivePreQueryStr += " and BankName='";
                    ComprehensivePreQueryStr += BankName;
                    ComprehensivePreQueryStr += "' ";

                }


                string CurrencyTypeIDStr = string.Empty;
                DataTable CurrencyTypeDt = CurrTypeSettingDBHelper.GetCurrTypeByCompanyCD(companyCD);
                for (int p = 0; p < CurrencyTypeDt.Rows.Count; p++)
                {
                    CurrencyTypeIDStr += CurrencyTypeDt.Rows[p]["ID"].ToString() + ",";

                }
                CurrencyTypeIDStr = CurrencyTypeIDStr.TrimEnd(new char[] { ',' });//综合本位币所有币种ID集






                decimal ComprehensiveBeginAmount = GetCashOrBankBeginAmount(ComprehensivePreQueryStr, CurrencyTypeIDStr);

                decimal ForeignBeginAmount = GetCashOrBankBeginAmount(PreQueryStr, CurryTypeID);

                DataRow row = dt.NewRow();
                row["Date"] = "";
                row["Abstract"] = "上日余额";
                row["DebitAmount"] = "";
                row["CreditAmount"] = "";
                row["ForeignDebitAmount"] = "";//外币借方金额
                row["ForeignCreditAmount"] = ""; ;//外币贷方金额
                row["Direction"]=CashOrBankDirection(ForeignBeginAmount);//方向
                row["ForeignEndAmount"] = Math.Abs(ForeignBeginAmount).ToString("#,###0.#0");//外币余额
                row["EndAmount"] = Math.Abs(ComprehensiveBeginAmount).ToString("#,###0.#0");
                row["ByOrderr"] = "1";
                row["CurrExg"] = "";
                row["ID"] = "";
                row["SourceDT"] = "";
                row["Code"] = "";
                dt.Rows.Add(row);


                /*获取上日余额结束*/


                decimal ForeignEndAmount = ForeignBeginAmount;
                decimal ComprehensiveEndAmount = ComprehensiveBeginAmount;
                decimal ForeignDamount = 0;
                decimal ForeignCamount = 0;
                decimal ComprehensiveDamount = 0;
                decimal ComprehensiveCamount = 0;

                foreach (DataRow dr in DisDateDt.Rows)
                {

                    string thisQueryStr = "  RetDate='" + dr["RetDate"].ToString() + "' and AcceWay='" + acceWay + "'  and CompanyCD='" + companyCD + "' ";
                    if (BankName.Trim().Length > 0)
                    {
                        thisQueryStr += " and BankName='";
                        thisQueryStr += BankName;
                        thisQueryStr += "' ";

                    }

                    if (CurryTypeID.LastIndexOf(",") == -1)
                    {
                        thisQueryStr += " and CurrencyType='";
                        thisQueryStr += CurryTypeID;
                        thisQueryStr += "' ";
                    }

                    DataTable ThisDT = VoucherDBHelper.GetCashOrBankView(thisQueryStr);

                    decimal ForeignthisDebitAmount = 0;
                    decimal ForeignthisCreditAmount = 0;
                    decimal ComprehensivethisDebitAmount = 0;
                    decimal ComprehensivethisCreditAmount = 0;

                    foreach (DataRow drr in ThisDT.Rows)
                    {
                        DataRow roww = dt.NewRow();
                        roww["Date"] = drr["RetDate"].ToString();
                        roww["Abstract"] = drr["Summary"].ToString();
                        switch (int.Parse(drr["Direction"].ToString()))
                        {
                            case 0:

                                roww["ForeignDebitAmount"] = Convert.ToDecimal(drr["Amount"].ToString()).ToString("#,###0.#0");
                                roww["ForeignCreditAmount"] = "0.00";
                                roww["DebitAmount"] = Convert.ToDecimal(drr["Comprehensive"].ToString()).ToString("#,###0.#0");
                                roww["CreditAmount"] = "0.00";
                                break;
                            case 1:
                                roww["ForeignDebitAmount"] = "0.00";
                                roww["ForeignCreditAmount"] = Convert.ToDecimal(drr["Amount"].ToString()).ToString("#,###0.#0");
                                roww["DebitAmount"] = "0.00";
                                roww["CreditAmount"] = Convert.ToDecimal(drr["Comprehensive"].ToString()).ToString("#,###0.#0");
                                break;
                            default:
                                break;
                        }
                        ComprehensiveEndAmount = ComprehensiveEndAmount + Convert.ToDecimal(roww["DebitAmount"].ToString()) - Convert.ToDecimal(roww["CreditAmount"].ToString());

                        ForeignEndAmount = ForeignEndAmount + Convert.ToDecimal(roww["ForeignDebitAmount"].ToString()) - Convert.ToDecimal(roww["ForeignCreditAmount"].ToString());


                        ComprehensivethisDebitAmount += Convert.ToDecimal(roww["DebitAmount"].ToString());
                        ComprehensivethisCreditAmount += Convert.ToDecimal(roww["CreditAmount"].ToString());
                        ForeignthisDebitAmount += Convert.ToDecimal(roww["ForeignDebitAmount"].ToString());
                        ForeignthisCreditAmount += Convert.ToDecimal(roww["ForeignCreditAmount"].ToString());

                        roww["Direction"] = CashOrBankDirection(ForeignEndAmount);
                        roww["ForeignEndAmount"] = Math.Abs(ForeignEndAmount).ToString("#,###0.#0");//外币余额
                        roww["EndAmount"] = Math.Abs(ComprehensiveEndAmount).ToString("#,###0.#0");
                        roww["ByOrderr"] = "1";
                        roww["CurrExg"] = drr["CurrencyType"].ToString() + "(" + drr["CurrencyRate"].ToString() + ")";
                        roww["ID"] = drr["ID"].ToString();
                        roww["SourceDT"] = drr["SourceTB"].ToString();
                        roww["Code"] = drr["Code"].ToString();
                        dt.Rows.Add(roww);
                    }



                    ComprehensiveDamount += ComprehensivethisDebitAmount;
                    ComprehensiveCamount += ComprehensivethisCreditAmount;

                    ForeignDamount += ForeignthisDebitAmount;
                    ForeignCamount += ForeignthisCreditAmount;



                    DataRow rowww = dt.NewRow();
                    rowww["Date"] = "";
                    rowww["Abstract"] = "本日合计";

                    rowww["DebitAmount"] = ComprehensivethisDebitAmount.ToString("#,###0.#0");
                    rowww["CreditAmount"] = ComprehensivethisCreditAmount.ToString("#,###0.#0");
                    rowww["ForeignDebitAmount"] = ForeignthisDebitAmount.ToString("#,###0.#0");
                    rowww["ForeignCreditAmount"] = ForeignthisCreditAmount.ToString("#,###0.#0");

                    rowww["Direction"] = CashOrBankDirection(ForeignEndAmount);
                    rowww["ForeignEndAmount"] = Math.Abs(ForeignEndAmount).ToString("#,###0.#0");//外币余额
                    rowww["EndAmount"] = Math.Abs(ComprehensiveEndAmount).ToString("#,###0.#0");
                    rowww["ByOrderr"] = "1";
                    rowww["CurrExg"] = "";
                    rowww["ID"] = "";
                    rowww["SourceDT"] = "";
                    rowww["Code"] = "";
                    dt.Rows.Add(rowww);


                }



                DataRow rowwww = dt.NewRow();
                rowwww["Date"] = "";
                rowwww["Abstract"] = "本期合计";

                rowwww["DebitAmount"] = ComprehensiveDamount.ToString("#,###0.#0");
                rowwww["CreditAmount"] = ComprehensiveCamount.ToString("#,###0.#0");
                rowwww["ForeignDebitAmount"] = ForeignDamount.ToString("#,###0.#0");
                rowwww["ForeignCreditAmount"] = ForeignCamount.ToString("#,###0.#0");

                rowwww["Direction"] = CashOrBankDirection(ForeignEndAmount);
                rowwww["ForeignEndAmount"] = Math.Abs(ForeignEndAmount).ToString("#,###0.#0");//外币余额
                rowwww["EndAmount"] = Math.Abs(ComprehensiveEndAmount).ToString("#,###0.#0");
                rowwww["ByOrderr"] = "1";
                rowwww["CurrExg"] = "";
                rowwww["ID"] = "";
                rowwww["SourceDT"] = "";
                rowwww["Code"] = "";
                dt.Rows.Add(rowwww);

                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 从现金/银行账目视图中 统计金额
        /// <summary>
        /// 从现金/银行账目视图中 统计金额
        /// </summary>
        /// <param name="QueryStr">查询条件</param>
        /// <param name="CurryTypeID">币种</param>
        /// <returns></returns>
        public static decimal GetCashOrBankBeginAmount(string QueryStr, string CurryTypeID)
        {
            try
            {
                decimal nev = 0;
                DataTable dt = VoucherDBHelper.GetCashOrBankView(QueryStr);
                foreach (DataRow dr in dt.Rows)
                {
                    if (CurryTypeID.LastIndexOf(",") == -1)//原币
                    {
                        decimal ret = Convert.ToDecimal(dr["Amount"].ToString());
                        switch (int.Parse(dr["Direction"].ToString()))
                        {
                            case 0:
                                nev += ret;
                                break;
                            case 1:
                                nev = nev - ret;
                                break;
                            default:
                                break;
                        }

                    }
                    else//综合本位币
                    {
                        decimal ret = Convert.ToDecimal(dr["Comprehensive"].ToString());
                        switch (int.Parse(dr["Direction"].ToString()))
                        {
                            case 0:
                                nev += ret;
                                break;
                            case 1:
                                nev = nev - ret;
                                break;
                            default:
                                break;
                        }
                    }
                }
                return nev;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取现金/银行日记账 余额的方向
        /// <summary>
        /// 获取现金/银行日记账 余额的方向
        /// </summary>
        /// <param name="Money"></param>
        /// <returns></returns>
        public static string CashOrBankDirection(decimal Money)
        {
            try
            {
                string nev = string.Empty;
                if (Money == 0)
                {
                    nev = "平";
                }
                else if (Money > 0)
                {
                    nev = "借";
                }
                else if (Money < 0)
                {
                    nev = "贷";
                }
                return nev;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取应收/应付汇总表数据源  其他外币数据源

        /// <summary>
        /// 获取应收/应付汇总表数据源  其他外币数据源
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="subjectsCD">会计科目</param>
        /// <param name="FormTBName">来源表</param>
        /// <param name="FileName">表字段</param>
        /// <param name="SubjectsDetails">辅助核算ID</param>
        /// <param name="CurryTypeID">币种ID</param>
        /// <returns></returns>
        public static DataTable GetForeignSummarySouse(string startDate, string endDate, string subjectsCD, string FormTBName, string FileName, string SubjectsDetails, string CurryTypeID)
        {
            try
            {
                return VoucherDBHelper.GetForeignSummarySouse(startDate, endDate, subjectsCD,FormTBName,FileName,SubjectsDetails,CurryTypeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region 试算平衡表数据源

        /// <summary>
        /// 试算平衡表数据源
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="IsBalance">是否平衡</param>
        /// <param name="CurryTypeID">币种</param>
        /// <returns></returns>
        public static DataTable SpreadsheetBalanceSource(string startDate, string endDate, out int IsBalance, string CurryTypeID)
        {
            try
            {
                IsBalance = 0;
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                DataTable dt = new DataTable();
                dt.Columns.Add("SubjectsCD");//会计科目代码
                dt.Columns.Add("SubjectsCDName");//会计科目名称
                dt.Columns.Add("BeginDebitAmount");//期初借方
                dt.Columns.Add("BeginCreditAmount");//期初贷方
                dt.Columns.Add("ThisDebitAmount");//本期借方发生额
                dt.Columns.Add("ThisCreditAmount");//本期贷方发生额
                dt.Columns.Add("EndDebitAmount");//期末借方
                dt.Columns.Add("EndCreditAmount");//期末贷方
                dt.Columns.Add("ByOrderr");//排序

                if (VoucherDBHelper.IsNullSubjects(companyCD)&&CurryTypeID.Trim().Length>0)
                {
                    DataTable subjectsDT1 = VoucherDBHelper.GetSpreadsheetSubjectsCD(startDate, endDate, CurryTypeID, companyCD);

                    /*根据具体科目获取顶级科目 Start*/
                    string subjectsCDstr = string.Empty;
                    foreach (DataRow dr in subjectsDT1.Rows)
                    {
                        string PresubjectsCD = VoucherDBHelper.GetSubjectsPerCD(dr["SubjectsCD"].ToString()).ToString().Split(',')[0].ToString();
                        if (subjectsCDstr.Contains(PresubjectsCD))
                        {

                        }
                        else
                        {
                            subjectsCDstr += PresubjectsCD + ",";
                        }
                    }


                    string[] SpreechBalaBeginSubjects = SubjectsBeginDetailsBus.GetDistinctSubjectsCD().Split(',');

                    for (int p = 0; p < SpreechBalaBeginSubjects.Length; p++)
                    {
                        if (subjectsCDstr.Contains(SpreechBalaBeginSubjects[p].ToString()))
                        {

                        }
                        else
                        {
                            subjectsCDstr += SpreechBalaBeginSubjects[p].ToString() + ",";
                        }
                    }

                    subjectsCDstr = subjectsCDstr.TrimEnd(new char[] { ',' });
                    subjectsCDstr = subjectsCDstr.Replace("'", "");

                    if (subjectsCDstr.Trim().Length > 0)
                    {
                        string[] Str = subjectsCDstr.Split(',');
                        DataTable subjectsDT = new DataTable();
                        subjectsDT.Columns.Add("SubjectsCD");
                        for (int m = 0; m < Str.Length; m++)
                        {
                            DataRow drr = subjectsDT.NewRow();
                            drr["SubjectsCD"] = Str[m].ToString();
                            subjectsDT.Rows.Add(drr);
                        }
                        /*根据具体科目获取顶级科目 End*/

                        /*合计信息初始化开始*/
                        decimal BeginDebitAmountSum = 0;//期初借方合计
                        decimal BeginCreditAmountSum = 0;//期初贷方合计
                        decimal ThisDebitAmountSum = 0;//本期借方发生额合计
                        decimal ThisCreditAmountSum = 0;//本期贷方发生额合计
                        decimal EndDebitAmountSum = 0;//期末借方合计
                        decimal EndCreditAmountSum = 0;//期末贷方合计
                        /*合计信息初始化结束*/


                        for (int i = 0; i < subjectsDT.Rows.Count; i++)
                        {
                            DataRow row = dt.NewRow();

                            int Dirdt = VoucherDBHelper.GetSubjectDirection(subjectsDT.Rows[i]["SubjectsCD"].ToString(), companyCD);
                            /*根据科目编码获取该科目方向结束*/

                            row["SubjectsCD"] = subjectsDT.Rows[i]["SubjectsCD"].ToString();//科目编码
                            row["SubjectsCDName"] = GetSubjectsName(subjectsDT.Rows[i]["SubjectsCD"].ToString());//科目名称
                            row["ByOrderr"] = "1";//排序


                            /*根据方向 计算期初借方，期初贷方开始*/
                            string queryStr = " VoucherDate<'" + startDate + "' and  SubjectsCD in (" + GetSubjectsNextCD(subjectsDT.Rows[i]["SubjectsCD"].ToString()) + ")  and CompanyCD='" + companyCD + "' and  CurrencyTypeID in ( " + CurryTypeID + " ) ";
                            decimal beginAmount = 0;
                            beginAmount = AcountBookDBHelper.GetAccountBookBeginAmount(queryStr, subjectsDT.Rows[i]["SubjectsCD"].ToString(), CurryTypeID);
                            decimal DefaultAmount = AcountBookDBHelper.GetBeginCurryTypeAmount(subjectsDT.Rows[i]["SubjectsCD"].ToString(), CurryTypeID, companyCD);
                            beginAmount += DefaultAmount;
                            //GetBeginAmount(queryStr, Dirdt.ToString());
                            switch (Dirdt)
                            {
                                case 0://借
                                    row["BeginDebitAmount"] = Math.Round(beginAmount, 2).ToString("#,###0.#0");//期初借方
                                    row["BeginCreditAmount"] = "0";//期初贷方
                                    break;
                                case 1://贷
                                    row["BeginDebitAmount"] = "0"; //期初借方
                                    row["BeginCreditAmount"] = Math.Round(beginAmount, 2).ToString("#,###0.#0");//期初贷方
                                    break;
                                default:
                                    break;
                            }
                            /*根据方向 计算期初借方，期初贷方开始*/

                            /*计算本期借/贷方发生额开始*/
                            string thisQueryStr = " VoucherDate>='" + startDate + "' and VoucherDate<='" + endDate + "' and CompanyCD='" + companyCD + "' and SubjectsCD in (" + GetSubjectsNextCD(subjectsDT.Rows[i]["SubjectsCD"].ToString()) + ") and CurrencyTypeID in (" + CurryTypeID + ")   ";

                            row["ThisDebitAmount"] = "0";//本期借方发生额
                            row["ThisCreditAmount"] = "0";//本期贷方发生额

                            DataTable sumDT = AcountBookDBHelper.GetAcountSumAmount(thisQueryStr);//统计对应科目的借方金额合计和贷方金额合计
                            if (sumDT.Rows.Count > 0)
                            {
                                if (CurryTypeID.LastIndexOf(",") == -1)
                                {
                                    row["ThisDebitAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ForeignThisDebit"].ToString()), 2).ToString("#,###0.#0");//本期借方发生额
                                    row["ThisCreditAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ForeignThisCredit"].ToString()), 2).ToString("#,###0.#0");//本期贷方发生额
                                }
                                else
                                {
                                    row["ThisDebitAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ThisDebit"].ToString()), 2).ToString("#,###0.#0");//本期借方发生额
                                    row["ThisCreditAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ThisCredit"].ToString()), 2).ToString("#,###0.#0");//本期贷方发生额
                                }

                            }
                            /*计算本期借/贷方发生额结束*/
                            /*根据方向 计算期末借方，期末贷方开始*/
                            switch (Dirdt)
                            {
                                case 0://借
                                    row["EndDebitAmount"] = Math.Round(Convert.ToDecimal(row["BeginDebitAmount"].ToString()) + Convert.ToDecimal(row["ThisDebitAmount"].ToString()) - Convert.ToDecimal(row["ThisCreditAmount"].ToString()), 2).ToString("#,###0.#0");//期末借方
                                    row["EndCreditAmount"] = "0";//期末贷方
                                    break;
                                case 1://贷
                                    row["EndDebitAmount"] = "0";//期末借方
                                    row["EndCreditAmount"] = Math.Round(Convert.ToDecimal(row["BeginCreditAmount"].ToString()) + Convert.ToDecimal(row["ThisCreditAmount"].ToString()) - Convert.ToDecimal(row["ThisDebitAmount"].ToString()), 2).ToString("#,###0.#0");//期末贷方
                                    break;
                                default:
                                    break;
                            }
                            /*根据方向 计算期末借方，期末贷方结束*/

                            /*合计信息计算开始*/
                            BeginDebitAmountSum += Convert.ToDecimal(row["BeginDebitAmount"].ToString());//期初借方合计
                            BeginCreditAmountSum += Convert.ToDecimal(row["BeginCreditAmount"].ToString());//期初贷方合计
                            ThisDebitAmountSum += Convert.ToDecimal(row["ThisDebitAmount"].ToString());//本期借方发生额合计
                            ThisCreditAmountSum += Convert.ToDecimal(row["ThisCreditAmount"].ToString());//本期贷方发生额合计
                            EndDebitAmountSum += Convert.ToDecimal(row["EndDebitAmount"].ToString());//期末借方合计
                            EndCreditAmountSum += Convert.ToDecimal(row["EndCreditAmount"].ToString());//期末贷方合计
                            /*合计信息计算结束*/


                            dt.Rows.Add(row);
                        }
                        if ((BeginDebitAmountSum == BeginCreditAmountSum) && (ThisDebitAmountSum == ThisCreditAmountSum) && (EndDebitAmountSum == EndCreditAmountSum))
                        {
                            IsBalance = 1;
                        }

                        DataRow roww = dt.NewRow();
                        roww["SubjectsCD"] = "";
                        roww["SubjectsCDName"] = "合计";
                        roww["BeginDebitAmount"] = Math.Round(BeginDebitAmountSum, 2).ToString("#,###0.#0");
                        roww["BeginCreditAmount"] = Math.Round(BeginCreditAmountSum, 2).ToString("#,###0.#0");
                        roww["ThisDebitAmount"] = Math.Round(ThisDebitAmountSum, 2).ToString("#,###0.#0");
                        roww["ThisCreditAmount"] = Math.Round(ThisCreditAmountSum, 2).ToString("#,###0.#0");
                        roww["EndDebitAmount"] = Math.Round(EndDebitAmountSum, 2).ToString("#,###0.#0");
                        roww["EndCreditAmount"] = Math.Round(EndCreditAmountSum, 2).ToString("#,###0.#0");
                        roww["ByOrderr"] = "1";
                        dt.Rows.Add(roww);
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

        #region 现金流量表数据源计算
        /// <summary>
        /// 现金流量表数据源计算
        /// </summary>
        /// <param name="DuringDate">会计期间</param>
        /// <returns></returns>
        public static DataTable CashFlowSource(string DuringDate)
        {


            /*datatable构造 start*/
            DataTable dt = new DataTable();
            dt.Columns.Add("Item");//项目
            dt.Columns.Add("Line");//行次
            dt.Columns.Add("CurrentAmount");//本期金额
            dt.Columns.Add("PreAmount");//上期金额
            dt.Columns.Add("ByOrder");//排序
            /*datatable构造 end*/

            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码


            string CurrencyTypeIDStr = string.Empty;
            DataTable CurrencyTypeDt = CurrTypeSettingDBHelper.GetCurrTypeByCompanyCD(CompanyCD);
            for (int p = 0; p < CurrencyTypeDt.Rows.Count; p++)
            {
                CurrencyTypeIDStr += CurrencyTypeDt.Rows[p]["ID"].ToString() + ",";

            }
            CurrencyTypeIDStr = CurrencyTypeIDStr.TrimEnd(new char[] { ',' });//综合本位币所有币种ID集

            string queryDateStart =string.Empty;//本期限制条件
            string queryDateEnd =string.Empty;//上期限制条件


            DataTable FormulaDT = CashFlowFormulaBus.GetCashFlowFormulaInfo("");//现金流量表项目明细


            decimal CurrentAmountSUM = 0;
            decimal PreAmountSUM = 0;
            for (int i = 0; i < FormulaDT.Rows.Count; i++)
            {
                if (i == 4 || i == 9 || i == 10 || i == 17 || i == 30 || i == 31 || i == 36 || i == 40 || i == 41 || i == 67 || i == 77)
                {
                    CurrentAmountSUM = 0;
                    PreAmountSUM = 0;
                }
                if (i == 4)
                {
                    DataRow row1 = dt.NewRow();
                    row1["Item"] = FormulaDT.Rows[4]["Name"].ToString();
                    row1["Line"] = FormulaDT.Rows[4]["Line"].ToString();
                    row1["CurrentAmount"] = Math.Round(CurrentAmountSUM, 2);//本期金额

                    row1["PreAmount"] = Math.Round(PreAmountSUM, 2);//上期金额
                    row1["ByOrder"] = "1";
                    dt.Rows.Add(row1);

                }
                else if (i == 9)
                {
                    DataRow row2 = dt.NewRow();
                    row2["Item"] = FormulaDT.Rows[9]["Name"].ToString();
                    row2["Line"] = FormulaDT.Rows[9]["Line"].ToString();
                    row2["CurrentAmount"] = Math.Round(CurrentAmountSUM, 2);//本期金额

                    row2["PreAmount"] = Math.Round(PreAmountSUM, 2);//上期金额
                    row2["ByOrder"] = "1";
                    dt.Rows.Add(row2);

                }
                else if (i == 10)//未处理
                {
                    DataRow row3 = dt.NewRow();
                    row3["Item"] = FormulaDT.Rows[10]["Name"].ToString();
                    row3["Line"] = FormulaDT.Rows[10]["Line"].ToString();
                    row3["CurrentAmount"] = Math.Round(CurrentAmountSUM, 2);//本期金额

                    row3["PreAmount"] = Math.Round(PreAmountSUM, 2);//上期金额
                    row3["ByOrder"] = "1";
                    dt.Rows.Add(row3);

                }
                else if (i == 17)
                {
                    DataRow row4 = dt.NewRow();
                    row4["Item"] = FormulaDT.Rows[17]["Name"].ToString();
                    row4["Line"] = FormulaDT.Rows[17]["Line"].ToString();
                    row4["CurrentAmount"] = Math.Round(CurrentAmountSUM, 2);//本期金额

                    row4["PreAmount"] = Math.Round(PreAmountSUM, 2);//上期金额
                    row4["ByOrder"] = "1";
                    dt.Rows.Add(row4);

                }
                else if (i == 30)
                {
                    DataRow row5 = dt.NewRow();
                    row5["Item"] = FormulaDT.Rows[30]["Name"].ToString();
                    row5["Line"] = FormulaDT.Rows[30]["Line"].ToString();
                    row5["CurrentAmount"] = Math.Round(CurrentAmountSUM, 2);//本期金额

                    row5["PreAmount"] = Math.Round(PreAmountSUM, 2);//上期金额
                    row5["ByOrder"] = "1";
                    dt.Rows.Add(row5);

                }
                else if (i == 31)//未处理
                {
                    DataRow row6 = dt.NewRow();
                    row6["Item"] = FormulaDT.Rows[31]["Name"].ToString();
                    row6["Line"] = FormulaDT.Rows[31]["Line"].ToString();
                    row6["CurrentAmount"] = Math.Round(CurrentAmountSUM, 2);//本期金额

                    row6["PreAmount"] = Math.Round(PreAmountSUM, 2);//上期金额
                    row6["ByOrder"] = "1";
                    dt.Rows.Add(row6);

                }
                else if (i == 36)
                {
                    DataRow row7 = dt.NewRow();
                    row7["Item"] = FormulaDT.Rows[36]["Name"].ToString();
                    row7["Line"] = FormulaDT.Rows[36]["Line"].ToString();
                    row7["CurrentAmount"] = Math.Round(CurrentAmountSUM, 2);//本期金额
                    row7["PreAmount"] = Math.Round(PreAmountSUM, 2);//上期金额
                    row7["ByOrder"] = "1";
                    dt.Rows.Add(row7);
                }
                else if (i == 40)
                {
                    DataRow row8 = dt.NewRow();
                    row8["Item"] = FormulaDT.Rows[40]["Name"].ToString();
                    row8["Line"] = FormulaDT.Rows[40]["Line"].ToString();
                    row8["CurrentAmount"] = Math.Round(CurrentAmountSUM, 2);//本期金额
                    row8["PreAmount"] = Math.Round(PreAmountSUM, 2);//上期金额
                    row8["ByOrder"] = "1";
                    dt.Rows.Add(row8);
                }
                else if (i == 41)//未处理
                {
                    DataRow row9 = dt.NewRow();
                    row9["Item"] = FormulaDT.Rows[41]["Name"].ToString();
                    row9["Line"] = FormulaDT.Rows[41]["Line"].ToString();
                    row9["CurrentAmount"] = Math.Round(CurrentAmountSUM, 2);//本期金额
                    row9["PreAmount"] = Math.Round(PreAmountSUM, 2);//上期金额
                    row9["ByOrder"] = "1";
                    dt.Rows.Add(row9);
                }
                else
                {
                    DataRow row = dt.NewRow();
                    row["Item"] = FormulaDT.Rows[i]["Name"].ToString();
                    row["Line"] = FormulaDT.Rows[i]["Line"].ToString() == "0" ? "" : FormulaDT.Rows[i]["Line"].ToString() == "10000" ? "行次" : FormulaDT.Rows[i]["Line"].ToString();

                    row["CurrentAmount"] = Math.Round(GetCashFlowFormulaAmount(FormulaDT.Rows[i]["Line"].ToString(), FormulaDT.Rows[i]["ID"].ToString(), DuringDate, CompanyCD, 0, CurrencyTypeIDStr), 2);//本期金额

                    row["PreAmount"] = Math.Round(GetCashFlowFormulaAmount(FormulaDT.Rows[i]["Line"].ToString(), FormulaDT.Rows[i]["ID"].ToString(), DuringDate, CompanyCD, 1, CurrencyTypeIDStr), 2);//上期金额
                    row["ByOrder"] = "1";

                    CurrentAmountSUM += Convert.ToDecimal(row["CurrentAmount"].ToString());
                    PreAmountSUM += Convert.ToDecimal(row["PreAmount"].ToString());


                    dt.Rows.Add(row);
                }
            }

            return dt;


        }
        #endregion

        #region 现金流量表数据计算
        public static decimal GetCashFlowOperatorTypeAmount(string DatequeryStr, string subjectsCD, string Direction, string OperatorType, string CurrencyTypeIDStr,string CompanyCD)
        {
            decimal BeginAmount = 0;
            decimal YbeginAmount = AcountBookDBHelper.GetBeginCurryTypeAmount(subjectsCD, CurrencyTypeIDStr,CompanyCD);

            DataTable beginDT =VoucherDBHelper.GetAmount(subjectsCD, DatequeryStr);

            if (beginDT.Rows.Count > 0)
            {
                switch (int.Parse(Direction))
                {
                    case 0: BeginAmount = Convert.ToDecimal(beginDT.Rows[0]["ThisDebit"].ToString()) - Convert.ToDecimal(beginDT.Rows[0]["ThisCredit"].ToString());
                        break;
                    case 1: BeginAmount = Convert.ToDecimal(beginDT.Rows[0]["ThisCredit"].ToString()) - Convert.ToDecimal(beginDT.Rows[0]["ThisDebit"].ToString());
                        break;
                    default:
                        break;
                }

                switch (int.Parse(OperatorType))
                {
                    case 1:
                        break;
                    case 2:
                        BeginAmount = 0 - BeginAmount;
                        break;
                    case 3:
                        BeginAmount = BeginAmount + YbeginAmount;
                        break;
                    case 4:
                        BeginAmount = BeginAmount + YbeginAmount;
                        break;
                    case 5:
                        BeginAmount = 0;
                        BeginAmount = Convert.ToDecimal(beginDT.Rows[0]["ThisDebit"].ToString());
                        break;
                    case 6:
                        BeginAmount = 0;
                        BeginAmount = Convert.ToDecimal(beginDT.Rows[0]["ThisCredit"].ToString());
                        break;
                }

            }
            return BeginAmount;
        }

        #endregion

        #region 根据公式计算现金流量表某一项目的金额
        /// <summary>
        /// 根据公式计算现金流量表某一项目的金额
        /// </summary>
        /// <param name="Line">行次</param>
        /// <param name="formulaID">项目ID</param>
        /// <param name="DatequeryStr">限制条件</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="flag">flag 0:本期金额，flag 1：上期金额</param>
        /// <returns></returns>
        public static decimal GetCashFlowFormulaAmount(string Line, string formulaID, string DuringDate, string CompanyCD, int flag, string CurrencyTypeIDStr)
        {
            string StartDate = string.Empty;
            string EndDate = string.Empty;

            //对输入的资料日期进行处理开始
            int MothString = Convert.ToInt32(DuringDate.Split('-')[1].ToString());
            int yearString = Convert.ToInt32(DuringDate.Split('-')[0].ToString());
            int PreYearString = yearString - 1;
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

            switch (flag)
            {
                case 0:
                    StartDate = yearString.ToString() + "-01-01";
                    EndDate = DuringDate + daycount;
                    break;
                case 1:
                    StartDate = PreYearString.ToString() + "-01-01";
                    EndDate = PreYearString.ToString() + "-" + MothString.ToString() + daycount;
                    break;
                default:
                    break;
            }
           
            

            decimal nev = 0;

            if (Line != "" && Line != "10000")
            {
                decimal BeginAmount = 0;
                DataTable FormulaDetailsdt = CashFlowFormulaDetailsDBHelper.GetCashFlowFormulaDetails("", CompanyCD,formulaID);
                string queryStr = string.Empty;

                foreach (DataRow dr in FormulaDetailsdt.Rows)
                {

                    switch (int.Parse(dr["OperatorType"].ToString()))
                    {
                        case 1:
                            queryStr = " VoucherDate>='" + StartDate + "' and VoucherDate<='" + EndDate + "'  and CompanyCD='" + CompanyCD + "' ";
                            break;
                        case 2:
                            queryStr = " VoucherDate>='" + StartDate + "' and VoucherDate<='" + EndDate + "'  and CompanyCD='" + CompanyCD + "' ";
                            break;
                        case 3:
                            queryStr = " VoucherDate<='" + EndDate + "'  and CompanyCD='" + CompanyCD + "' ";
                            break;
                        case 4:
                            queryStr = " VoucherDate<='" + StartDate + "'  and CompanyCD='" + CompanyCD + "' ";
                            break;
                        case 5:
                            queryStr = " VoucherDate>='" + StartDate + "' and VoucherDate<='" + EndDate + "'  and CompanyCD='" + CompanyCD + "' ";
                            break;
                        case 6:
                            queryStr = " VoucherDate>='" + StartDate + "' and VoucherDate<='" + EndDate + "'  and CompanyCD='" + CompanyCD + "' ";
                            break;
                        default:
                            break;
                    }
                    BeginAmount = GetCashFlowOperatorTypeAmount(queryStr, dr["SubjectsCD"].ToString(), dr["Direction"].ToString(), dr["OperatorType"].ToString(), CurrencyTypeIDStr,CompanyCD);
                    switch (dr["Operator"].ToString())
                    {
                        case "+":
                            nev = nev + BeginAmount;
                            break;
                        case "-":
                            nev = nev - BeginAmount;
                            break;
                        default:

                            break;
                    }
                }
            }

            return nev;
        }
        #endregion

        #region 科目期初资产负债表公式计算
        public static DataTable GetBalanceInfo()
        {
            DataTable Sourcedt = new DataTable();
            Sourcedt.Columns.Add("Asset");//资产
            Sourcedt.Columns.Add("ALine");//资产行次
            Sourcedt.Columns.Add("AEndAmount");//资产期末数
            Sourcedt.Columns.Add("AYearBeginAmount");//资产年初数
            Sourcedt.Columns.Add("Debt");//负债
            Sourcedt.Columns.Add("DLine");//负债行次
            Sourcedt.Columns.Add("DEndAmount");//负债期末数
            Sourcedt.Columns.Add("DYearBeginAmount");//负债年初数
            Sourcedt.Columns.Add("ByOrder");//排序
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码

            string CurrencyTypeIDStr = string.Empty;
            DataTable CurrencyTypeDt = CurrTypeSettingDBHelper.GetCurrTypeByCompanyCD(CompanyCD);
            for (int p = 0; p < CurrencyTypeDt.Rows.Count; p++)
            {
                CurrencyTypeIDStr += CurrencyTypeDt.Rows[p]["ID"].ToString() + ",";

            }
            CurrencyTypeIDStr = CurrencyTypeIDStr.TrimEnd(new char[] { ',' });//综合本位币所有币种ID集

            /*datatable构造 start*/
            DataTable dt = new DataTable();
            dt.Columns.Add("Asset");//资产
            dt.Columns.Add("ALine");//资产行次
            dt.Columns.Add("AEndAmount");//资产期末数
            dt.Columns.Add("AYearBeginAmount");//资产年初数

               if (CurrencyTypeIDStr.Trim().Length > 0)
                {
                    DataTable FormulaDt = BalanceFormulaDBHelper.GetBalanceFormulaInfo("");
                    decimal Ybg = 0;
                    decimal Ebg = 0;
                    for (int i = 0; i < FormulaDt.Rows.Count; i++)
                    {
                        if (i == 13 || i == 30 || i == 32 || i == 46 || i == 56 || i == 63)
                        {
                            Ybg = 0;
                            Ebg = 0;
                        }
                        if (i == 12)
                        {
                            DataRow row1 = dt.NewRow();
                            row1["Asset"] = FormulaDt.Rows[12]["Name"].ToString();
                            row1["ALine"] = FormulaDt.Rows[12]["Line"].ToString();
                            row1["AEndAmount"] = Math.Round(Ebg, 2).ToString("#,###0.#0");
                            row1["AYearBeginAmount"] = Math.Round(Ybg, 2).ToString("#,###0.#0");
                            dt.Rows.Add(row1);
                        }
                        else if (i == 29)
                        {
                            DataRow row2 = dt.NewRow();
                            row2["Asset"] = FormulaDt.Rows[29]["Name"].ToString();
                            row2["ALine"] = FormulaDt.Rows[29]["Line"].ToString();
                            row2["AEndAmount"] = Math.Round(Ebg, 2).ToString("#,###0.#0");
                            row2["AYearBeginAmount"] = Math.Round(Ybg, 2).ToString("#,###0.#0");
                            dt.Rows.Add(row2);
                        }
                        else if (i == 31)
                        {
                            DataRow row3 = dt.NewRow();
                            row3["Asset"] = FormulaDt.Rows[31]["Name"].ToString();
                            row3["ALine"] = FormulaDt.Rows[31]["Line"].ToString();
                            row3["AEndAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[12]["AEndAmount"].ToString()) + Convert.ToDecimal(dt.Rows[29]["AEndAmount"].ToString()), 2).ToString("#,###0.#0");
                            row3["AYearBeginAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[12]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(dt.Rows[29]["AYearBeginAmount"].ToString()), 2).ToString("#,###0.#0");
                            dt.Rows.Add(row3);
                        }
                        else if (i == 45)
                        {
                            DataRow row4 = dt.NewRow();
                            row4["Asset"] = FormulaDt.Rows[45]["Name"].ToString();
                            row4["ALine"] = FormulaDt.Rows[45]["Line"].ToString();
                            row4["AEndAmount"] = Math.Round(Ebg, 2).ToString("#,###0.#0");
                            row4["AYearBeginAmount"] = Math.Round(Ybg, 2).ToString("#,###0.#0");
                            dt.Rows.Add(row4);
                        }
                        else if (i == 55)
                        {
                            DataRow row5 = dt.NewRow();
                            row5["Asset"] = FormulaDt.Rows[55]["Name"].ToString();
                            row5["ALine"] = FormulaDt.Rows[55]["Line"].ToString();
                            row5["AEndAmount"] = Math.Round(Ebg, 2).ToString("#,###0.#0");
                            row5["AYearBeginAmount"] = Math.Round(Ybg, 2).ToString("#,###0.#0");
                            dt.Rows.Add(row5);
                        }
                        else if (i == 62)
                        {
                            DataRow row6 = dt.NewRow();
                            row6["Asset"] = FormulaDt.Rows[62]["Name"].ToString();
                            row6["ALine"] = FormulaDt.Rows[62]["Line"].ToString();
                            row6["AEndAmount"] = Math.Round(Ebg, 2).ToString("#,###0.#0");
                            row6["AYearBeginAmount"] = Math.Round(Ybg, 2).ToString("#,###0.#0");
                            dt.Rows.Add(row6);
                        }
                        else if (i == 63)
                        {
                            DataRow row7 = dt.NewRow();
                            row7["Asset"] = FormulaDt.Rows[63]["Name"].ToString();
                            row7["ALine"] = FormulaDt.Rows[63]["Line"].ToString();
                            row7["AEndAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[45]["AEndAmount"].ToString()) + Convert.ToDecimal(dt.Rows[55]["AEndAmount"].ToString()) + Convert.ToDecimal(dt.Rows[62]["AEndAmount"].ToString()), 2).ToString("#,###0.#0");
                            row7["AYearBeginAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[45]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(dt.Rows[55]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(dt.Rows[62]["AYearBeginAmount"].ToString()), 2).ToString("#,###0.#0");
                            dt.Rows.Add(row7);
                        }
                        else
                        {
                            DataRow row = dt.NewRow();
                            row["Asset"] = FormulaDt.Rows[i]["Name"].ToString();
                            row["ALine"] = FormulaDt.Rows[i]["Line"].ToString() == "0" ? "" : FormulaDt.Rows[i]["Line"].ToString();
                            row["AEndAmount"] = FormulaDt.Rows[i]["Line"].ToString() == "0" ? "0" : Math.Round(GetBalaAmountinfo(FormulaDt.Rows[i]["Line"].ToString(), FormulaDt.Rows[i]["ID"].ToString(), 0, CurrencyTypeIDStr), 2).ToString("#,###0.#0");
                            row["AYearBeginAmount"] = FormulaDt.Rows[i]["Line"].ToString() == "0" ? "0" : Math.Round(GetBalaAmountinfo(FormulaDt.Rows[i]["Line"].ToString(), FormulaDt.Rows[i]["ID"].ToString(), 1, CurrencyTypeIDStr), 2).ToString("#,###0.#0");
                            Ybg += Convert.ToDecimal(row["AYearBeginAmount"].ToString());
                            Ebg += Convert.ToDecimal(row["AEndAmount"]);
                            dt.Rows.Add(row);
                        }
                    }
                    for (int j = 0; j < 32; j++)
                    {
                        DataRow r = Sourcedt.NewRow();
                        r["Asset"] = dt.Rows[j]["Asset"].ToString();
                        r["ALine"] = dt.Rows[j]["ALine"].ToString();
                        r["AEndAmount"] = dt.Rows[j]["AEndAmount"].ToString() == "0" || dt.Rows[j]["AEndAmount"].ToString() == "0.00" ? "" : dt.Rows[j]["AEndAmount"].ToString();
                        r["AYearBeginAmount"] = dt.Rows[j]["AYearBeginAmount"].ToString() == "0" || dt.Rows[j]["AYearBeginAmount"].ToString() == "0.00" ? "" : dt.Rows[j]["AYearBeginAmount"].ToString();
                        r["Debt"] = dt.Rows[j + 32]["Asset"].ToString();
                        r["DLine"] = dt.Rows[j + 32]["ALine"].ToString();
                        r["DEndAmount"] = dt.Rows[j + 32]["AEndAmount"].ToString() == "0" || dt.Rows[j + 32]["AEndAmount"].ToString() == "0.00" ? "" : dt.Rows[j + 32]["AEndAmount"].ToString();
                        r["DYearBeginAmount"] = dt.Rows[j + 32]["AYearBeginAmount"].ToString() == "0" || dt.Rows[j + 32]["AYearBeginAmount"].ToString() == "0.00" ? "" : dt.Rows[j + 32]["AYearBeginAmount"].ToString();
                        r["ByOrder"] = "1";
                        Sourcedt.Rows.Add(r);
                    }
                }

               return Sourcedt;

        }
        #endregion

        #region 资产负债表公式计算
        /// <summary>
        /// 资产负债表计算
        /// </summary>
        /// <param name="DuringDate">会计期间</param>
        /// <returns></returns>
        public static DataTable BalanceSheetSource(string DuringDate)
        {

            /*datatable构造 start*/
            DataTable Sourcedt = new DataTable();
            Sourcedt.Columns.Add("Asset");//资产
            Sourcedt.Columns.Add("ALine");//资产行次
            Sourcedt.Columns.Add("AEndAmount");//资产期末数
            Sourcedt.Columns.Add("AYearBeginAmount");//资产年初数
            Sourcedt.Columns.Add("Debt");//负债
            Sourcedt.Columns.Add("DLine");//负债行次
            Sourcedt.Columns.Add("DEndAmount");//负债期末数
            Sourcedt.Columns.Add("DYearBeginAmount");//负债年初数
            Sourcedt.Columns.Add("ByOrder");//排序
            /*datatable构造 end*/
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
            if (IsNullSubjects())
            {
                string startDate = string.Empty;
                string endDate = string.Empty;

                //对输入的资料日期进行处理开始
                int MothString = Convert.ToInt32(DuringDate.Split('-')[1].ToString());
                int yearString = Convert.ToInt32(DuringDate.Split('-')[0].ToString());
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

                startDate = yearString.ToString() + "-01-01";
                endDate = DuringDate + daycount;





                string CurrencyTypeIDStr = string.Empty;
                DataTable CurrencyTypeDt = CurrTypeSettingDBHelper.GetCurrTypeByCompanyCD(CompanyCD);
                for (int p = 0; p < CurrencyTypeDt.Rows.Count; p++)
                {
                    CurrencyTypeIDStr += CurrencyTypeDt.Rows[p]["ID"].ToString() + ",";

                }
                CurrencyTypeIDStr = CurrencyTypeIDStr.TrimEnd(new char[] { ',' });//综合本位币所有币种ID集



                /*datatable构造 start*/
                DataTable dt = new DataTable();
                dt.Columns.Add("Asset");//资产
                dt.Columns.Add("ALine");//资产行次
                dt.Columns.Add("AEndAmount");//资产期末数
                dt.Columns.Add("AYearBeginAmount");//资产年初数
                /*datatable构造 end*/
                if (CurrencyTypeIDStr.Trim().Length > 0)
                {
                    DataTable FormulaDt = BalanceFormulaDBHelper.GetBalanceFormulaInfo("");
                    decimal Ybg = 0;
                    decimal Ebg = 0;
                    for (int i = 0; i < FormulaDt.Rows.Count; i++)
                    {
                        if (i == 13 || i == 30 || i == 32 || i == 46 || i == 56 || i == 63)
                        {
                            Ybg = 0;
                            Ebg = 0;
                        }
                        string queryDateStart = " VoucherDate<'" + startDate + "' and CompanyCD='" + CompanyCD + "' ";
                        string queryDateEnd = " VoucherDate<='" + endDate + "'  and CompanyCD='" + CompanyCD + "' ";
                        if (i == 12)
                        {
                            DataRow row1 = dt.NewRow();
                            row1["Asset"] = FormulaDt.Rows[12]["Name"].ToString();
                            row1["ALine"] = FormulaDt.Rows[12]["Line"].ToString();
                            row1["AEndAmount"] = Math.Round(Ebg, 2).ToString("#,###0.#0");
                            row1["AYearBeginAmount"] = Math.Round(Ybg, 2).ToString("#,###0.#0");
                            dt.Rows.Add(row1);
                        }
                        else if (i == 29)
                        {
                            DataRow row2 = dt.NewRow();
                            row2["Asset"] = FormulaDt.Rows[29]["Name"].ToString();
                            row2["ALine"] = FormulaDt.Rows[29]["Line"].ToString();
                            row2["AEndAmount"] = Math.Round(Ebg, 2).ToString("#,###0.#0");
                            row2["AYearBeginAmount"] = Math.Round(Ybg, 2).ToString("#,###0.#0");
                            dt.Rows.Add(row2);
                        }
                        else if (i == 31)
                        {
                            DataRow row3 = dt.NewRow();
                            row3["Asset"] = FormulaDt.Rows[31]["Name"].ToString();
                            row3["ALine"] = FormulaDt.Rows[31]["Line"].ToString();
                            row3["AEndAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[12]["AEndAmount"].ToString()) + Convert.ToDecimal(dt.Rows[29]["AEndAmount"].ToString()), 2).ToString("#,###0.#0");
                            row3["AYearBeginAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[12]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(dt.Rows[29]["AYearBeginAmount"].ToString()), 2).ToString("#,###0.#0");
                            dt.Rows.Add(row3);
                        }
                        else if (i == 45)
                        {
                            DataRow row4 = dt.NewRow();
                            row4["Asset"] = FormulaDt.Rows[45]["Name"].ToString();
                            row4["ALine"] = FormulaDt.Rows[45]["Line"].ToString();
                            row4["AEndAmount"] = Math.Round(Ebg, 2).ToString("#,###0.#0");
                            row4["AYearBeginAmount"] = Math.Round(Ybg, 2).ToString("#,###0.#0");
                            dt.Rows.Add(row4);
                        }
                        else if (i == 55)
                        {
                            DataRow row5 = dt.NewRow();
                            row5["Asset"] = FormulaDt.Rows[55]["Name"].ToString();
                            row5["ALine"] = FormulaDt.Rows[55]["Line"].ToString();
                            row5["AEndAmount"] = Math.Round(Ebg, 2).ToString("#,###0.#0");
                            row5["AYearBeginAmount"] = Math.Round(Ybg, 2).ToString("#,###0.#0");
                            dt.Rows.Add(row5);
                        }
                        else if (i == 62)
                        {
                            DataRow row6 = dt.NewRow();
                            row6["Asset"] = FormulaDt.Rows[62]["Name"].ToString();
                            row6["ALine"] = FormulaDt.Rows[62]["Line"].ToString();
                            row6["AEndAmount"] = Math.Round(Ebg, 2).ToString("#,###0.#0");
                            row6["AYearBeginAmount"] = Math.Round(Ybg, 2).ToString("#,###0.#0");
                            dt.Rows.Add(row6);
                        }
                        else if (i == 63)
                        {
                            DataRow row7 = dt.NewRow();
                            row7["Asset"] = FormulaDt.Rows[63]["Name"].ToString();
                            row7["ALine"] = FormulaDt.Rows[63]["Line"].ToString();
                            row7["AEndAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[45]["AEndAmount"].ToString()) + Convert.ToDecimal(dt.Rows[55]["AEndAmount"].ToString()) + Convert.ToDecimal(dt.Rows[62]["AEndAmount"].ToString()), 2).ToString("#,###0.#0");
                            row7["AYearBeginAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[45]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(dt.Rows[55]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(dt.Rows[62]["AYearBeginAmount"].ToString()), 2).ToString("#,###0.#0");
                            dt.Rows.Add(row7);
                        }
                        else
                        {
                            DataRow row = dt.NewRow();
                            row["Asset"] = FormulaDt.Rows[i]["Name"].ToString();
                            row["ALine"] = FormulaDt.Rows[i]["Line"].ToString() == "0" ? "" : FormulaDt.Rows[i]["Line"].ToString();
                            row["AEndAmount"] = FormulaDt.Rows[i]["Line"].ToString() == "0" ? "0" : Math.Round(GetBalaAmount(FormulaDt.Rows[i]["Line"].ToString(), FormulaDt.Rows[i]["ID"].ToString(), queryDateEnd, 0, CurrencyTypeIDStr), 2).ToString("#,###0.#0");
                            row["AYearBeginAmount"] = FormulaDt.Rows[i]["Line"].ToString() == "0" ? "0" : Math.Round(GetBalaAmount(FormulaDt.Rows[i]["Line"].ToString(), FormulaDt.Rows[i]["ID"].ToString(), queryDateStart, 1, CurrencyTypeIDStr), 2).ToString("#,###0.#0");
                            Ybg += Convert.ToDecimal(row["AYearBeginAmount"].ToString());
                            Ebg += Convert.ToDecimal(row["AEndAmount"]);
                            dt.Rows.Add(row);
                        }
                    }
                    for (int j = 0; j < 32; j++)
                    {
                        DataRow r = Sourcedt.NewRow();
                        r["Asset"] = dt.Rows[j]["Asset"].ToString();
                        r["ALine"] = dt.Rows[j]["ALine"].ToString();
                        r["AEndAmount"] = dt.Rows[j]["AEndAmount"].ToString() == "0" || dt.Rows[j]["AEndAmount"].ToString() == "0.00" ? "" : dt.Rows[j]["AEndAmount"].ToString();
                        r["AYearBeginAmount"] = dt.Rows[j]["AYearBeginAmount"].ToString() == "0" || dt.Rows[j]["AYearBeginAmount"].ToString() == "0.00" ? "" : dt.Rows[j]["AYearBeginAmount"].ToString();
                        r["Debt"] = dt.Rows[j + 32]["Asset"].ToString();
                        r["DLine"] = dt.Rows[j + 32]["ALine"].ToString();
                        r["DEndAmount"] = dt.Rows[j + 32]["AEndAmount"].ToString() == "0" || dt.Rows[j + 32]["AEndAmount"].ToString() == "0.00" ? "" : dt.Rows[j + 32]["AEndAmount"].ToString();
                        r["DYearBeginAmount"] = dt.Rows[j + 32]["AYearBeginAmount"].ToString() == "0" || dt.Rows[j + 32]["AYearBeginAmount"].ToString() == "0.00" ? "" : dt.Rows[j + 32]["AYearBeginAmount"].ToString();
                        r["ByOrder"] = "1";
                        Sourcedt.Rows.Add(r);
                    }
                }
            }
            return Sourcedt;
        }
        #endregion
         
        #region 按公式计算资产负债表中各项的值 added by jiangym
        /// <summary>
        /// 按公式计算资产负债表中各项的值
        /// </summary>
        /// <param name="Line">行次</param>
        /// <param name="formulaID">资产负债表项ID</param>
        /// <param name="DatequeryStr">日期限制条件</param>
        /// <param name="flag">flag 0:年初数，flag 1:期末数</param>
        /// <returns></returns>
        public static decimal GetBalaAmountinfo(string Line, string formulaID, int flag, string CurrencyTypeIDStr)
        {
            decimal nev = 0;
            if (Line != "0")
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                DataTable dt = FormulaDetailsDBHelper.GetBalanceFormulaDetails("", CompanyCD, formulaID);

                foreach (DataRow dr in dt.Rows)
                {
                    decimal YbeginAmount = 0;
            
                    decimal total = 0;
                    switch (flag)
                    {
                        case 0:
                            YbeginAmount = AcountBookDBHelper.GetBeginCurryTypeAmount(dr["SubjectsCD"].ToString(), CurrencyTypeIDStr, CompanyCD);
                            break;
                        case 1:
                            YbeginAmount = AcountBookDBHelper.GetYeaeBeginAmount(dr["SubjectsCD"].ToString(), CurrencyTypeIDStr, CompanyCD);
                            break;
                        default:
                            break;
                    }
                    total = YbeginAmount;
                    switch (dr["Operator"].ToString())
                    {
                        case "+":
                            nev = nev + total;
                            break;
                        case "-":
                            nev = nev - total;
                            break;
                        default:

                            break;
                    }
                }
            }
            return nev;
        }
        #endregion

        #region 按公式计算资产负债表中各项的值
        /// <summary>
        /// 按公式计算资产负债表中各项的值
        /// </summary>
        /// <param name="Line">行次</param>
        /// <param name="formulaID">资产负债表项ID</param>
        /// <param name="DatequeryStr">日期限制条件</param>
        /// <param name="flag">flag 0:年初数，flag 1:期末数</param>
        /// <returns></returns>
        public static decimal GetBalaAmount(string Line, string formulaID, string DatequeryStr, int flag, string CurrencyTypeIDStr)
        {
            decimal nev = 0;
            if (Line != "0")
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                DataTable dt = FormulaDetailsDBHelper.GetBalanceFormulaDetails("", CompanyCD, formulaID);

                foreach (DataRow dr in dt.Rows)
                {
                    decimal YbeginAmount = 0;
                    decimal BeginAmount = 0;
                    decimal total = 0;
                    switch (flag)
                    {
                        case 0:
                            YbeginAmount = AcountBookDBHelper.GetBeginCurryTypeAmount(dr["SubjectsCD"].ToString(), CurrencyTypeIDStr, CompanyCD);
                            break;
                        case 1:
                            YbeginAmount =AcountBookDBHelper.GetYeaeBeginAmount(dr["SubjectsCD"].ToString(),CurrencyTypeIDStr,CompanyCD);
                            break;
                        default:
                            break;
                    }
                    BeginAmount = GetAccountBookBeginAmount(DatequeryStr, dr["SubjectsCD"].ToString(), dr["Direction"].ToString());
                    total = YbeginAmount + BeginAmount;
                    switch (dr["Operator"].ToString())
                    {
                        case "+":
                            nev = nev + total;
                            break;
                        case "-":
                            nev = nev - total;
                            break;
                        default:

                            break;
                    }
                }

            }
            return nev;
        }
        #endregion

        #region 统计账簿金额期初金额
        /// <summary>
        /// 统计账簿金额期初金额
        /// </summary>
        /// <param name="DatequeryStr">查询条件</param>
        /// <param name="subjectsCD">科目</param>
        /// <param name="Direction">方向</param>
        /// <returns></returns>
        public static decimal GetAccountBookBeginAmount(string DatequeryStr, string subjectsCD, string Direction)
        {

            decimal BeginAmount = 0;
            //科目方向
            //string subjectCDList = VoucherDBHelper.GetSubjectsNextCD(subjectsCD);

            DataTable beginDT = VoucherDBHelper.GetAmount(subjectsCD, DatequeryStr);

            if (beginDT.Rows.Count > 0)
            {
                switch (int.Parse(Direction))
                {
                    case 0: BeginAmount = Convert.ToDecimal(beginDT.Rows[0]["ThisDebit"].ToString()) - Convert.ToDecimal(beginDT.Rows[0]["ThisCredit"].ToString());
                        break;
                    case 1: BeginAmount = Convert.ToDecimal(beginDT.Rows[0]["ThisCredit"].ToString()) - Convert.ToDecimal(beginDT.Rows[0]["ThisDebit"].ToString());
                        break;
                    default:
                        break;
                }
            }
            return BeginAmount;
        }
        #endregion

        #region 资产负债表带上年数数据源
        /// <summary>
        /// 资产负债表带上年数数据源
        /// </summary>
        /// <param name="DuringDate">会计期间</param>
        /// <returns></returns>
        public static DataTable BalanceSheetNowPreSource(string DuringDate)
        {

            /*datatable构造 start*/
            DataTable dt = new DataTable();
            dt.Columns.Add("Asset");//资产
            dt.Columns.Add("ALine");//资产行次
            dt.Columns.Add("AEndAmount");//资产期末数
            dt.Columns.Add("AYearBeginAmount");//资产年初数

            dt.Columns.Add("PreAEndAmount");//上年资产期末数
            dt.Columns.Add("PreAYearBeginAmount");//上年资产年初数
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码

            if (VoucherDBHelper.IsNullSubjects(CompanyCD))
            {
                string StartDate = string.Empty;
                string EndDate = string.Empty;
                string PreStartDate = string.Empty;
                string PreEndDate = string.Empty;

                //对输入的资料日期进行处理开始
                int MothString = Convert.ToInt32(DuringDate.Split('-')[1].ToString());
                int yearString = Convert.ToInt32(DuringDate.Split('-')[0].ToString());
                int PreYearString = yearString - 1;
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

                StartDate = yearString.ToString() + "-01-01";
                EndDate = DuringDate + daycount;


                PreStartDate = PreYearString.ToString() + "-01-01";
                PreEndDate = PreYearString.ToString() + "-" + MothString.ToString() + daycount;





                string CurrencyTypeIDStr = string.Empty;
                DataTable CurrencyTypeDt = CurrTypeSettingDBHelper.GetCurrTypeByCompanyCD(CompanyCD);
                for (int p = 0; p < CurrencyTypeDt.Rows.Count; p++)
                {
                    CurrencyTypeIDStr += CurrencyTypeDt.Rows[p]["ID"].ToString() + ",";

                }
                CurrencyTypeIDStr = CurrencyTypeIDStr.TrimEnd(new char[] { ',' });//综合本位币所有币种ID集




                if (CurrencyTypeIDStr.Trim().Length > 0)
                {
                    DataTable FormulaDt = BalanceFormulaDBHelper.GetBalanceFormulaInfo("");
                    decimal Ybg = 0;
                    decimal Ebg = 0;
                    decimal PreYbg = 0;
                    decimal PreEbg = 0;
                    for (int i = 0; i < FormulaDt.Rows.Count; i++)
                    {
                        if (i == 13 || i == 30 || i == 32 || i == 46 || i == 56 || i == 63)
                        {
                            Ybg = 0;
                            Ebg = 0;
                            PreYbg = 0;
                            PreEbg = 0;
                        }
                        string queryDateStart = " VoucherDate<'" + StartDate + "' and CompanyCD='" + CompanyCD + "' ";
                        string queryDateEnd = " VoucherDate<='" + EndDate + "'  and CompanyCD='" + CompanyCD + "' ";
                        string PreQueryDateStart = " VoucherDate<'" + PreStartDate + "' and CompanyCD='" + CompanyCD + "' ";
                        string PreQueryDateEnd = " VoucherDate<='" + PreEndDate + "'  and CompanyCD='" + CompanyCD + "' ";


                        if (i == 12)
                        {
                            DataRow row1 = dt.NewRow();
                            row1["Asset"] = FormulaDt.Rows[12]["Name"].ToString();
                            row1["ALine"] = FormulaDt.Rows[12]["Line"].ToString();
                            row1["AEndAmount"] = Math.Round(Ebg, 2);
                            row1["AYearBeginAmount"] = Math.Round(Ybg, 2);
                            row1["PreAEndAmount"] = Math.Round(PreEbg, 2);
                            row1["PreAYearBeginAmount"] = Math.Round(PreYbg, 2);
                            dt.Rows.Add(row1);
                        }
                        else if (i == 29)
                        {
                            DataRow row2 = dt.NewRow();
                            row2["Asset"] = FormulaDt.Rows[29]["Name"].ToString();
                            row2["ALine"] = FormulaDt.Rows[29]["Line"].ToString();
                            row2["AEndAmount"] = Math.Round(Ebg, 2);
                            row2["AYearBeginAmount"] = Math.Round(Ybg, 2);
                            row2["PreAEndAmount"] = Math.Round(PreEbg, 2);
                            row2["PreAYearBeginAmount"] = Math.Round(PreYbg, 2);
                            dt.Rows.Add(row2);
                        }
                        else if (i == 31)
                        {
                            DataRow row3 = dt.NewRow();
                            row3["Asset"] = FormulaDt.Rows[31]["Name"].ToString();
                            row3["ALine"] = FormulaDt.Rows[31]["Line"].ToString();
                            row3["AEndAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[12]["AEndAmount"].ToString()) + Convert.ToDecimal(dt.Rows[29]["AEndAmount"].ToString()), 2);
                            row3["AYearBeginAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[12]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(dt.Rows[29]["AYearBeginAmount"].ToString()), 2);

                            row3["PreAEndAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[12]["PreAEndAmount"].ToString()) + Convert.ToDecimal(dt.Rows[29]["PreAEndAmount"].ToString()), 2);
                            row3["PreAYearBeginAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[12]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(dt.Rows[29]["PreAYearBeginAmount"].ToString()), 2);


                            dt.Rows.Add(row3);
                        }
                        else if (i == 45)
                        {
                            DataRow row4 = dt.NewRow();
                            row4["Asset"] = FormulaDt.Rows[45]["Name"].ToString();
                            row4["ALine"] = FormulaDt.Rows[45]["Line"].ToString();
                            row4["AEndAmount"] = Math.Round(Ebg, 2);
                            row4["AYearBeginAmount"] = Math.Round(Ybg, 2);
                            row4["PreAEndAmount"] = Math.Round(PreEbg, 2);
                            row4["PreAYearBeginAmount"] = Math.Round(PreYbg, 2);
                            dt.Rows.Add(row4);
                        }
                        else if (i == 55)
                        {
                            DataRow row5 = dt.NewRow();
                            row5["Asset"] = FormulaDt.Rows[55]["Name"].ToString();
                            row5["ALine"] = FormulaDt.Rows[55]["Line"].ToString();
                            row5["AEndAmount"] = Math.Round(Ebg, 2);
                            row5["AYearBeginAmount"] = Math.Round(Ybg, 2);
                            row5["PreAEndAmount"] = Math.Round(PreEbg, 2);
                            row5["PreAYearBeginAmount"] = Math.Round(PreYbg, 2);
                            dt.Rows.Add(row5);
                        }
                        else if (i == 62)
                        {
                            DataRow row6 = dt.NewRow();
                            row6["Asset"] = FormulaDt.Rows[62]["Name"].ToString();
                            row6["ALine"] = FormulaDt.Rows[62]["Line"].ToString();
                            row6["AEndAmount"] = Math.Round(Ebg, 2);
                            row6["AYearBeginAmount"] = Math.Round(Ybg, 2);
                            row6["PreAEndAmount"] = Math.Round(PreEbg, 2);
                            row6["PreAYearBeginAmount"] = Math.Round(PreYbg, 2);
                            dt.Rows.Add(row6);
                        }
                        else if (i == 63)
                        {
                            DataRow row7 = dt.NewRow();
                            row7["Asset"] = FormulaDt.Rows[63]["Name"].ToString();
                            row7["ALine"] = FormulaDt.Rows[63]["Line"].ToString();
                            row7["AEndAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[45]["AEndAmount"].ToString()) + Convert.ToDecimal(dt.Rows[55]["AEndAmount"].ToString()) + Convert.ToDecimal(dt.Rows[62]["AEndAmount"].ToString()), 2);
                            row7["AYearBeginAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[45]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(dt.Rows[55]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(dt.Rows[62]["AYearBeginAmount"].ToString()), 2);


                            row7["PreAEndAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[45]["PreAEndAmount"].ToString()) + Convert.ToDecimal(dt.Rows[55]["PreAEndAmount"].ToString()) + Convert.ToDecimal(dt.Rows[62]["PreAEndAmount"].ToString()), 2);
                            row7["PreAYearBeginAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[45]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(dt.Rows[55]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(dt.Rows[62]["PreAYearBeginAmount"].ToString()), 2);

                            dt.Rows.Add(row7);
                        }
                        else
                        {
                            DataRow row = dt.NewRow();
                            row["Asset"] = FormulaDt.Rows[i]["Name"].ToString();
                            row["ALine"] = FormulaDt.Rows[i]["Line"].ToString() == "0" ? "" : FormulaDt.Rows[i]["Line"].ToString();
                            row["AEndAmount"] = FormulaDt.Rows[i]["Line"].ToString() == "0" ? 0 : Math.Round(GetBalaAmount(FormulaDt.Rows[i]["Line"].ToString(), FormulaDt.Rows[i]["ID"].ToString(), queryDateEnd, 0, CurrencyTypeIDStr), 2);
                            row["AYearBeginAmount"] = FormulaDt.Rows[i]["Line"].ToString() == "0" ? 0 : Math.Round(GetBalaAmount(FormulaDt.Rows[i]["Line"].ToString(), FormulaDt.Rows[i]["ID"].ToString(), queryDateStart, 1, CurrencyTypeIDStr), 2);



                            row["PreAEndAmount"] = FormulaDt.Rows[i]["Line"].ToString() == "0" ? 0 : Math.Round(GetBalaAmount(FormulaDt.Rows[i]["Line"].ToString(), FormulaDt.Rows[i]["ID"].ToString(), PreQueryDateEnd, 0, CurrencyTypeIDStr), 2);
                            row["PreAYearBeginAmount"] = FormulaDt.Rows[i]["Line"].ToString() == "0" ? 0 : Math.Round(GetBalaAmount(FormulaDt.Rows[i]["Line"].ToString(), FormulaDt.Rows[i]["ID"].ToString(), PreQueryDateStart, 1, CurrencyTypeIDStr), 2);


                            Ybg += Convert.ToDecimal(row["AYearBeginAmount"].ToString());
                            Ebg += Convert.ToDecimal(row["AEndAmount"]);
                            PreYbg += Convert.ToDecimal(row["PreAYearBeginAmount"].ToString());
                            PreEbg += Convert.ToDecimal(row["PreAEndAmount"]);

                            dt.Rows.Add(row);
                        }


                    }
                }
            }


            return dt;

        }
        #endregion

        #region 现金流量表数据源（约定公式计算）
        /// <summary>
        /// 现金流量表数据源（约定公式计算）
        /// </summary>
        /// <param name="DuringDate">会计期间</param>
        /// <returns>DataTable</returns>
        public static DataTable GetCashFlowInfo(string DuringDate)
        {
            DataTable dt = new DataTable();
            AdditionalItemBus bus=new AdditionalItemBus();

            DataTable BalanceSheetDT = BalanceSheetNowPreSource(DuringDate);//资产负债表数据源
            DataTable AdditionalItemDT = bus.GetAdditionalItemInfo(DuringDate);//辅助数据项数据源
            DataTable ProfitDT=ProfitFormulaBus.GetInstance().GetProfitProcessInfo(DuringDate, "0");//利润表数据源

            dt.Columns.Add("ItemName");
            dt.Columns.Add("Line");
            dt.Columns.Add("CurryAmount");
            dt.Columns.Add("PreAmount");
            dt.Columns.Add("ByOrder");

            DataRow D5 = dt.NewRow();
            D5["ItemName"] = ConstUtil.ItemName1;
            D5["Line"] = "";
            D5["CurryAmount"] = "0";
            D5["PreAmount"] = "0";
            D5["ByOrder"] = "01";
            dt.Rows.Add(D5);


            DataRow D6 = dt.NewRow();
            D6["ItemName"] = ConstUtil.ItemName2;
            D6["Line"] = "1";
            D6["CurryAmount"] =Math.Round(Convert.ToDecimal(ProfitDT.Rows[0]["currMoney"].ToString()) * Convert.ToDecimal(1.17) + Convert.ToDecimal(BalanceSheetDT.Rows[3]["AYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[3]["AEndAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[4]["AYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[4]["AEndAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[37]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[37]["AYearBeginAmount"].ToString()),2);
            D6["PreAmount"] = Math.Round(Convert.ToDecimal(ProfitDT.Rows[0]["agoMoney"].ToString()) * Convert.ToDecimal(1.17) + Convert.ToDecimal(BalanceSheetDT.Rows[3]["PreAYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[3]["PreAEndAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[4]["PreAYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[4]["PreAEndAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[37]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[37]["PreAYearBeginAmount"].ToString()),2);
            D6["ByOrder"] = "02";
            dt.Rows.Add(D6);

            DataRow D7 = dt.NewRow();
            D7["ItemName"] = ConstUtil.ItemName3;
            D7["Line"] = "3";
            D7["CurryAmount"] = "0";
            D7["PreAmount"] = "0";
            D7["ByOrder"] = "03";
            dt.Rows.Add(D7);


            DataRow D10 = dt.NewRow();
            D10["ItemName"] = ConstUtil.ItemName6;
            D10["Line"] = "10";
            D10["CurryAmount"] = Math.Round((Convert.ToDecimal(ProfitDT.Rows[1]["currMoney"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[9]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[9]["AYearBeginAmount"].ToString())) * Convert.ToDecimal(1.17) + Convert.ToDecimal(BalanceSheetDT.Rows[35]["AYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[35]["AEndAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[36]["AYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[36]["AEndAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[5]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[5]["AYearBeginAmount"].ToString()),2);
            D10["PreAmount"] = Math.Round((Convert.ToDecimal(ProfitDT.Rows[1]["agoMoney"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[9]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[9]["PreAYearBeginAmount"].ToString())) * Convert.ToDecimal(1.17) + Convert.ToDecimal(BalanceSheetDT.Rows[35]["PreAYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[35]["PreAEndAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[36]["PreAYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[36]["PreAEndAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[5]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[5]["PreAYearBeginAmount"].ToString()),2);
            D10["ByOrder"] = "06";
            dt.Rows.Add(D10);


            DataRow D11 = dt.NewRow();
            D11["ItemName"] = ConstUtil.ItemName7;
            D11["Line"] = "12";
            D11["CurryAmount"] =Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[3]["CurrentAmount"].ToString()),2);
            D11["PreAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[3]["PreAmount"].ToString()),2);
            D11["ByOrder"] = "07";
            dt.Rows.Add(D11);


            DataRow D12 = dt.NewRow();
            D12["ItemName"] = ConstUtil.ItemName8;
            D12["Line"] = "13";
            D12["CurryAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[11]["CurrentAmount"].ToString()),2);
            D12["PreAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[11]["PreAmount"].ToString()),2);
            D12["ByOrder"] = "08";
            dt.Rows.Add(D12);



            DataRow D33 = dt.NewRow();
            D33["ItemName"] = ConstUtil.ItemName29;
            D33["Line"] = "46";
            D33["CurryAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[16]["CurrentAmount"].ToString()),2);
            D33["PreAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[16]["PreAmount"].ToString()),2);
            D33["ByOrder"] = "29";
            dt.Rows.Add(D33);


            DataRow D13 = dt.NewRow();

            D13["ItemName"] = ConstUtil.ItemName9;
            D13["Line"] = "18";

            D13["CurryAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[8]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[8]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[42]["AYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[42]["AEndAmount"].ToString()) + Convert.ToDecimal(ProfitDT.Rows[11]["currMoney"].ToString()) + Convert.ToDecimal(ProfitDT.Rows[4]["currMoney"].ToString()) + Convert.ToDecimal(ProfitDT.Rows[3]["currMoney"].ToString()) - Convert.ToDecimal(D11["CurryAmount"].ToString()) -  Convert.ToDecimal(AdditionalItemDT.Rows[12]["CurrentAmount"].ToString()) -  Convert.ToDecimal(AdditionalItemDT.Rows[9]["CurrentAmount"].ToString()) + Convert.ToDecimal(D33["CurryAmount"].ToString()) - Convert.ToDecimal(ProfitDT.Rows[5]["currMoney"].ToString()),2);

            D13["PreAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[8]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[8]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[42]["PreAYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[42]["PreAEndAmount"].ToString()) + Convert.ToDecimal(ProfitDT.Rows[11]["agoMoney"].ToString()) + Convert.ToDecimal(ProfitDT.Rows[4]["agoMoney"].ToString()) + Convert.ToDecimal(ProfitDT.Rows[3]["agoMoney"].ToString()) - Convert.ToDecimal(D11["PreAmount"].ToString()) -  Convert.ToDecimal(AdditionalItemDT.Rows[12]["PreAmount"].ToString()) -  Convert.ToDecimal(AdditionalItemDT.Rows[9]["PreAmount"].ToString()) + Convert.ToDecimal(D33["PreAmount"].ToString()) - Convert.ToDecimal(ProfitDT.Rows[5]["agoMoney"].ToString()),2);

            D13["ByOrder"] = "09";
            dt.Rows.Add(D13);


            DataRow D14 = dt.NewRow();
            D14["ItemName"] = ConstUtil.ItemName10;
            D14["Line"] = "20";
            D14["CurryAmount"] = Math.Round(Convert.ToDecimal(D10["CurryAmount"].ToString()) + Convert.ToDecimal(D11["CurryAmount"].ToString()) + Convert.ToDecimal(D12["CurryAmount"].ToString()) + Convert.ToDecimal(D13["CurryAmount"].ToString()),2);
            D14["PreAmount"] = Math.Round(Convert.ToDecimal(D10["PreAmount"].ToString()) + Convert.ToDecimal(D11["PreAmount"].ToString()) + Convert.ToDecimal(D12["PreAmount"].ToString()) + Convert.ToDecimal(D13["PreAmount"].ToString()),2);
            D14["ByOrder"] = "10";
            dt.Rows.Add(D14);


            DataRow D16 = dt.NewRow();
            D16["ItemName"] = ConstUtil.ItemName12;
            D16["Line"] = "";
            D16["CurryAmount"] = "0";
            D16["PreAmount"] = "0";
            D16["ByOrder"] = "12";
            dt.Rows.Add(D16);

            DataRow D17 = dt.NewRow();
            D17["ItemName"] = ConstUtil.ItemName13;
            D17["Line"] = "22";
            D17["CurryAmount"] = "0";
            D17["PreAmount"] = "0";
            D17["ByOrder"] = "13";
            dt.Rows.Add(D17);

            DataRow D18 = dt.NewRow();
            D18["ItemName"] = ConstUtil.ItemName14;
            D18["Line"] = "23";
            D18["CurryAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[21]["CurrentAmount"].ToString()),2);
            D18["PreAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[21]["PreAmount"].ToString()),2);
            D18["ByOrder"] = "14";
            dt.Rows.Add(D18);

            DataRow D19 = dt.NewRow();
            D19["ItemName"] = ConstUtil.ItemName15;
            D19["Line"] = "25";
            D19["CurryAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[22]["CurrentAmount"].ToString()),2);
            D19["PreAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[22]["PreAmount"].ToString()),2);
            D19["ByOrder"] = "15";
            dt.Rows.Add(D19);


            DataRow D20 = dt.NewRow();
            D20["ItemName"] = ConstUtil.ItemName16;
            D20["Line"] = "28";
            D20["CurryAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[23]["CurrentAmount"].ToString()),2);
            D20["PreAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[23]["PreAmount"].ToString()),2);
            D20["ByOrder"] = "16";
            dt.Rows.Add(D20);


            DataRow D21 = dt.NewRow();
            D21["ItemName"] = ConstUtil.ItemName17;
            D21["Line"] = "29";
            D21["CurryAmount"] = Math.Round(Convert.ToDecimal(D17["CurryAmount"].ToString()) + Convert.ToDecimal(D18["CurryAmount"].ToString()) + Convert.ToDecimal(D19["CurryAmount"].ToString()) + Convert.ToDecimal(D20["CurryAmount"].ToString()),2);
            D21["PreAmount"] = Math.Round(Convert.ToDecimal(D17["PreAmount"].ToString()) + Convert.ToDecimal(D18["PreAmount"].ToString()) + Convert.ToDecimal(D19["PreAmount"].ToString()) + Convert.ToDecimal(D20["PreAmount"].ToString()),2);
            D21["ByOrder"] = "17";
            dt.Rows.Add(D21);


            DataRow D22 = dt.NewRow();
            D22["ItemName"] = ConstUtil.ItemName18;
            D22["Line"] = "30";
            D22["CurryAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[20]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[20]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[19]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[19]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[24]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[24]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(AdditionalItemDT.Rows[12]["CurrentAmount"].ToString()),2);

            D22["PreAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[20]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[20]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[19]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[19]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[24]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[24]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(AdditionalItemDT.Rows[12]["PreAmount"].ToString()),2);
            D22["ByOrder"] = "18";
            dt.Rows.Add(D22);


            DataRow D23 = dt.NewRow();
            D23["ItemName"] = ConstUtil.ItemName19;
            D23["Line"] = "31";
            D23["CurryAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[2]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[2]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[16]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[16]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[14]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[14]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[15]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[15]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[17]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[17]["AYearBeginAmount"].ToString()),2);
            D23["PreAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[2]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[2]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[16]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[16]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[14]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[14]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[15]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[15]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[17]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[17]["PreAYearBeginAmount"].ToString()),2);
            D23["ByOrder"] = "19";
            dt.Rows.Add(D23);


            DataRow D24 = dt.NewRow();
            D24["ItemName"] = ConstUtil.ItemName20;
            D24["Line"] = "35";
            D24["CurryAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[24]["CurrentAmount"].ToString()),2);
            D24["PreAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[24]["PreAmount"].ToString()),2);
            D24["ByOrder"] = "20";
            dt.Rows.Add(D24);



            DataRow D25 = dt.NewRow();
            D25["ItemName"] = ConstUtil.ItemName21;
            D25["Line"] = "36";
            D25["CurryAmount"] = Math.Round(Convert.ToDecimal(D22["CurryAmount"].ToString()) + Convert.ToDecimal(D23["CurryAmount"].ToString()) + Convert.ToDecimal(D24["CurryAmount"].ToString()),2) ;
            D25["PreAmount"] = Math.Round(Convert.ToDecimal(D22["PreAmount"].ToString()) + Convert.ToDecimal(D23["PreAmount"].ToString()) + Convert.ToDecimal(D24["PreAmount"].ToString()),2);
            D25["ByOrder"] = "21";
            dt.Rows.Add(D25);


            DataRow D26 = dt.NewRow();
            D26["ItemName"] = ConstUtil.ItemName22;
            D26["Line"] = "37";
            D26["CurryAmount"] = Math.Round(Convert.ToDecimal(D21["CurryAmount"].ToString()) - Convert.ToDecimal(D25["CurryAmount"].ToString()),2) ;
            D26["PreAmount"] = Math.Round(Convert.ToDecimal(D21["PreAmount"].ToString()) - Convert.ToDecimal(D25["PreAmount"].ToString()),2);
            D26["ByOrder"] = "22";
            dt.Rows.Add(D26);

            DataRow D27 = dt.NewRow();
            D27["ItemName"] = ConstUtil.ItemName23;
            D27["Line"] = "";
            D27["CurryAmount"] = "0";
            D27["PreAmount"] = "0";
            D27["ByOrder"] = "23";
            dt.Rows.Add(D27);


            DataRow D28 = dt.NewRow();
            D28["ItemName"] = ConstUtil.ItemName24;
            D28["Line"] = "38";
            D28["CurryAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[57]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[57]["AYearBeginAmount"].ToString()),2);
            D28["PreAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[57]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[57]["PreAYearBeginAmount"].ToString()),2);
            D28["ByOrder"] = "24";
            dt.Rows.Add(D28);


            DataRow D29 = dt.NewRow();
            D29["ItemName"] = ConstUtil.ItemName25;
            D29["Line"] = "40";
            D29["CurryAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[33]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[33]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[47]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[47]["AYearBeginAmount"].ToString()),2);
            D29["PreAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[33]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[33]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[47]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[47]["PreAYearBeginAmount"].ToString()),2);
            D29["ByOrder"] = "25";
            dt.Rows.Add(D29);


            DataRow D30 = dt.NewRow();
            D30["ItemName"] = ConstUtil.ItemName26;
            D30["Line"] = "43";
            D30["CurryAmount"] = "0";
            D30["PreAmount"] = "0";
            D30["ByOrder"] = "26";
            dt.Rows.Add(D30);

            DataRow D31 = dt.NewRow();
            D31["ItemName"] = ConstUtil.ItemName27;
            D31["Line"] = "44";
            D31["CurryAmount"] = Math.Round(Convert.ToDecimal(D28["CurryAmount"].ToString()) + Convert.ToDecimal(D29["CurryAmount"].ToString()) + Convert.ToDecimal(D30["CurryAmount"].ToString()),2);
            D31["PreAmount"] = Math.Round(Convert.ToDecimal(D28["PreAmount"].ToString()) + Convert.ToDecimal(D29["PreAmount"].ToString()) + Convert.ToDecimal(D30["PreAmount"].ToString()),2);
            D31["ByOrder"] = "27";
            dt.Rows.Add(D31);


            DataRow D32 = dt.NewRow();
            D32["ItemName"] = ConstUtil.ItemName28;
            D32["Line"] = "45";
            D32["CurryAmount"] = "0";
            D32["PreAmount"] = "0";
            D32["ByOrder"] = "28";
            dt.Rows.Add(D32);


            


            DataRow D34 = dt.NewRow();
            D34["ItemName"] = ConstUtil.ItemName30;
            D34["Line"] = "52";
            D34["CurryAmount"] = "0";
            D34["PreAmount"] = "0";
            D34["ByOrder"] = "30";
            dt.Rows.Add(D34);

            DataRow D35 = dt.NewRow();
            D35["ItemName"] = ConstUtil.ItemName31;
            D35["Line"] = "53";
            D35["CurryAmount"] = Math.Round(Convert.ToDecimal(D32["CurryAmount"].ToString()) + Convert.ToDecimal(D33["CurryAmount"].ToString()) + Convert.ToDecimal(D34["CurryAmount"].ToString()),2);
            D35["PreAmount"] = Math.Round(Convert.ToDecimal(D32["PreAmount"].ToString()) + Convert.ToDecimal(D33["PreAmount"].ToString()) + Convert.ToDecimal(D34["PreAmount"].ToString()),2);
            D35["ByOrder"] = "31";
            dt.Rows.Add(D35);

            DataRow D36 = dt.NewRow();
            D36["ItemName"] = ConstUtil.ItemName32;
            D36["Line"] = "54";
            D36["CurryAmount"] = Math.Round(Convert.ToDecimal(D31["CurryAmount"].ToString()) - Convert.ToDecimal(D35["CurryAmount"].ToString()),2);
            D36["PreAmount"] = Math.Round(Convert.ToDecimal(D31["PreAmount"].ToString()) - Convert.ToDecimal(D35["PreAmount"].ToString()),2);
            D36["ByOrder"] = "32";
            dt.Rows.Add(D36);


            DataRow D37 = dt.NewRow();
            D37["ItemName"] = ConstUtil.ItemName33;
            D37["Line"] = "55";
            D37["CurryAmount"] = "0";
            D37["PreAmount"] = "0";
            D37["ByOrder"] = "33";
            dt.Rows.Add(D37);

            DataRow H30 = dt.NewRow();
            H30["ItemName"] = ConstUtil.ItemName60;
            H30["Line"] = "";
            H30["CurryAmount"] = "0";
            H30["PreAmount"] = "0";
            H30["ByOrder"] = "60";
            dt.Rows.Add(H30);

            DataRow H31 = dt.NewRow();
            H31["ItemName"] = ConstUtil.ItemName61;
            H31["Line"] = "";
            H31["CurryAmount"] = "0";
            H31["PreAmount"] = "0";
            H31["ByOrder"] = "61";
            dt.Rows.Add(H31);

            DataRow H32 = dt.NewRow();
            H32["ItemName"] = ConstUtil.ItemName62;
            H32["Line"] = "";
            H32["CurryAmount"] = "0";
            H32["PreAmount"] = "0";
            H32["ByOrder"] = "62";
            dt.Rows.Add(H32);

            DataRow H33 = dt.NewRow();
            H33["ItemName"] = ConstUtil.ItemName63;
            H33["Line"] = "";
            H33["CurryAmount"] = "0";
            H33["PreAmount"] = "0";
            H33["ByOrder"] = "63";
            dt.Rows.Add(H33);

            DataRow H34 = dt.NewRow();
            H34["ItemName"] = ConstUtil.ItemName64;
            H34["Line"] = "79";
            H34["CurryAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[1]["AEndAmount"].ToString()),2);
            H34["PreAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[1]["PreAEndAmount"].ToString()),2);
            H34["ByOrder"] = "64";
            dt.Rows.Add(H34);

            DataRow H35 = dt.NewRow();
            H35["ItemName"] = ConstUtil.ItemName65;
            H35["Line"] = "80";
            H35["CurryAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[1]["AYearBeginAmount"].ToString()),2);
            H35["PreAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[1]["PreAYearBeginAmount"].ToString()),2);
            H35["ByOrder"] = "65";
            dt.Rows.Add(H35);


            DataRow H36 = dt.NewRow();
            H36["ItemName"] = ConstUtil.ItemName66;
            H36["Line"] = "81";
            H36["CurryAmount"] = "0";
            H36["PreAmount"] = "0";
            H36["ByOrder"] = "66";
            dt.Rows.Add(H36);



            DataRow H37 = dt.NewRow();
            H37["ItemName"] = ConstUtil.ItemName67;
            H37["Line"] = "82";
            H37["CurryAmount"] = "0";
            H37["PreAmount"] = "0";
            H37["ByOrder"] = "67";
            dt.Rows.Add(H37);

            DataRow H38 = dt.NewRow();
            H38["ItemName"] = ConstUtil.ItemName68;
            H38["Line"] = "83";
            H38["CurryAmount"] = Math.Round(Convert.ToDecimal(H34["CurryAmount"].ToString()) - Convert.ToDecimal(H35["CurryAmount"].ToString()) + Convert.ToDecimal(H36["CurryAmount"].ToString()) - Convert.ToDecimal(H37["CurryAmount"].ToString()),2);
            H38["PreAmount"] = Math.Round(Convert.ToDecimal(H34["PreAmount"].ToString()) - Convert.ToDecimal(H35["PreAmount"].ToString()) + Convert.ToDecimal(H36["PreAmount"].ToString()) - Convert.ToDecimal(H37["PreAmount"].ToString()),2);
            H38["ByOrder"] = "68";
            dt.Rows.Add(H38);


            /*未核算项D8,D9,D15*/


            DataRow D8 = dt.NewRow();
            D8["ItemName"] = ConstUtil.ItemName4;
            D8["Line"] = "8";
            D8["CurryAmount"] = Math.Round(Convert.ToDecimal(H38["CurryAmount"].ToString()) + Convert.ToDecimal(D14["CurryAmount"].ToString()) - Convert.ToDecimal(D6["CurryAmount"].ToString()) - Convert.ToDecimal(D7["CurryAmount"].ToString()) - Convert.ToDecimal(D26["CurryAmount"].ToString()) - Convert.ToDecimal(D36["CurryAmount"].ToString()) - Convert.ToDecimal(D37["CurryAmount"].ToString()),2);
            D8["PreAmount"] = Math.Round(Convert.ToDecimal(H38["PreAmount"].ToString()) + Convert.ToDecimal(D14["PreAmount"].ToString()) - Convert.ToDecimal(D6["PreAmount"].ToString()) - Convert.ToDecimal(D7["PreAmount"].ToString()) - Convert.ToDecimal(D26["PreAmount"].ToString()) - Convert.ToDecimal(D36["PreAmount"].ToString()) - Convert.ToDecimal(D37["PreAmount"].ToString()),2);
            D8["ByOrder"] = "04";
            dt.Rows.Add(D8);


            DataRow D9 = dt.NewRow();
            D9["ItemName"] = ConstUtil.ItemName5;
            D9["Line"] = "9";
            D9["CurryAmount"] =Math.Round(Convert.ToDecimal(D6["CurryAmount"].ToString()) + Convert.ToDecimal(D7["CurryAmount"].ToString()) + Convert.ToDecimal(D8["CurryAmount"].ToString()),2) ;
            D9["PreAmount"] = Math.Round(Convert.ToDecimal(D6["PreAmount"].ToString()) + Convert.ToDecimal(D7["PreAmount"].ToString()) + Convert.ToDecimal(D8["PreAmount"].ToString()),2);
            D9["ByOrder"] = "05";
            dt.Rows.Add(D9);


            DataRow D15 = dt.NewRow();
            D15["ItemName"] = ConstUtil.ItemName11;
            D15["Line"] = "21";
            D15["CurryAmount"] = Math.Round(Convert.ToDecimal(D9["CurryAmount"].ToString()) - Convert.ToDecimal(D14["CurryAmount"].ToString()),2) ;
            D15["PreAmount"] = Math.Round(Convert.ToDecimal(D9["PreAmount"].ToString()) - Convert.ToDecimal(D14["PreAmount"].ToString()),2) ;
            D15["ByOrder"] = "11";
            dt.Rows.Add(D15);



            DataRow D38 = dt.NewRow();
            D38["ItemName"] = ConstUtil.ItemName34;
            D38["Line"] = "56";
            D38["CurryAmount"] = Math.Round(Convert.ToDecimal(D15["CurryAmount"].ToString()) + Convert.ToDecimal(D26["CurryAmount"].ToString()) + Convert.ToDecimal(D36["CurryAmount"].ToString()) + Convert.ToDecimal(D37["CurryAmount"].ToString()),2);
            D38["PreAmount"] = Math.Round(Convert.ToDecimal(D15["PreAmount"].ToString()) + Convert.ToDecimal(D26["PreAmount"].ToString()) + Convert.ToDecimal(D36["PreAmount"].ToString()) + Convert.ToDecimal(D37["PreAmount"].ToString()),2);
            D38["ByOrder"] = "34";
            dt.Rows.Add(D38);



            /*D8,D9,D15已核算完成*/


            DataRow H5 = dt.NewRow();
            H5["ItemName"] = ConstUtil.ItemName35;
            H5["Line"] = "";
            H5["CurryAmount"] = "0";
            H5["PreAmount"] = "0";
            H5["ByOrder"] = "35";
            dt.Rows.Add(H5);

            DataRow H6 = dt.NewRow();
            H6["ItemName"] = ConstUtil.ItemName36;
            H6["Line"] = "57";
            H6["CurryAmount"] = Math.Round(Convert.ToDecimal(ProfitDT.Rows[14]["currMoney"].ToString()),2);
            H6["PreAmount"] = Math.Round(Convert.ToDecimal(ProfitDT.Rows[14]["agoMoney"].ToString()),2);
            H6["ByOrder"] = "36";
            dt.Rows.Add(H6);


            DataRow H7 = dt.NewRow();
            H7["ItemName"] = ConstUtil.ItemName37;
            H7["Line"] = "58";
            H7["CurryAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[17]["CurrentAmount"].ToString()),2);
            H7["PreAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[17]["PreAmount"].ToString()),2);
            H7["ByOrder"] = "37";
            dt.Rows.Add(H7);

            DataRow H8 = dt.NewRow();
            H8["ItemName"] = ConstUtil.ItemName38;
            H8["Line"] = "59";
            H8["CurryAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[12]["CurrentAmount"].ToString()),2);
            H8["PreAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[12]["PreAmount"].ToString()),2);
            H8["ByOrder"] = "38";
            dt.Rows.Add(H8);



            DataRow H9 = dt.NewRow();
            H9["ItemName"] = ConstUtil.ItemName39;
            H9["Line"] = "60";
            H9["CurryAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[18]["CurrentAmount"].ToString()),2);
            H9["PreAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[18]["PreAmount"].ToString()),2);
            H9["ByOrder"] = "39";
            dt.Rows.Add(H9);



            DataRow H10 = dt.NewRow();
            H10["ItemName"] = ConstUtil.ItemName40;
            H10["Line"] = "61";
            H10["CurryAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[19]["CurrentAmount"].ToString()),2);
            H10["PreAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[19]["PreAmount"].ToString()),2);
            H10["ByOrder"] = "40";
            dt.Rows.Add(H10);


            DataRow H11 = dt.NewRow();
            H11["ItemName"] = ConstUtil.ItemName41;
            H11["Line"] = "66";
            H11["CurryAmount"] = Math.Round(Convert.ToDecimal(ProfitDT.Rows[11]["currMoney"].ToString()),2);
            H11["PreAmount"] = Math.Round(Convert.ToDecimal(ProfitDT.Rows[11]["agoMoney"].ToString()),2);
            H11["ByOrder"] = "41";
            dt.Rows.Add(H11);


            DataRow H12 = dt.NewRow();
            H12["ItemName"] = ConstUtil.ItemName42;
            H12["Line"] = "67";
            H12["CurryAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[20]["CurrentAmount"].ToString()),2);
            H12["PreAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[20]["PreAmount"].ToString()),2);
            H12["ByOrder"] = "42";
            dt.Rows.Add(H12);



            DataRow H13 = dt.NewRow();
            H13["ItemName"] = ConstUtil.ItemName43;
            H13["Line"] = "";
            H13["CurryAmount"] = "0";
            H13["PreAmount"] = "0";
            H13["ByOrder"] = "43";
            dt.Rows.Add(H13);


            DataRow H14 = dt.NewRow();
            H14["ItemName"] = ConstUtil.ItemName44;
            H14["Line"] = "68";
            H14["CurryAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[15]["CurrentAmount"].ToString()),2);
            H14["PreAmount"] = Math.Round(Convert.ToDecimal(AdditionalItemDT.Rows[15]["PreAmount"].ToString()),2);
            H14["ByOrder"] = "44";
            dt.Rows.Add(H14);

            DataRow H15 = dt.NewRow();
            H15["ItemName"] = ConstUtil.ItemName45;
            H15["Line"] = "69";
            H15["CurryAmount"] = Math.Round(Convert.ToDecimal(0-Convert.ToDecimal(ProfitDT.Rows[7]["currMoney"].ToString())),2);
            H15["PreAmount"] = Math.Round(Convert.ToDecimal(0-Convert.ToDecimal(ProfitDT.Rows[7]["agoMoney"].ToString())),2);
            H15["ByOrder"] = "45";
            dt.Rows.Add(H15);


            DataRow H16 = dt.NewRow();
            H16["ItemName"] = ConstUtil.ItemName46;
            H16["Line"] = "";
            H16["CurryAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[27]["AYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[27]["AEndAmount"].ToString()),2);
            H16["PreAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[27]["PreAYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[27]["PreAEndAmount"].ToString()),2);
            H16["ByOrder"] = "46";
            dt.Rows.Add(H16);


            DataRow H17 = dt.NewRow();
            H17["ItemName"] = ConstUtil.ItemName47;
            H17["Line"] = "70";
            H17["CurryAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[52]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[52]["AYearBeginAmount"].ToString()),2);
            H17["PreAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[52]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[52]["PreAYearBeginAmount"].ToString()),2);
            H17["ByOrder"] = "47";
            dt.Rows.Add(H17);


            DataRow H18 = dt.NewRow();
            H18["ItemName"] = ConstUtil.ItemName48;
            H18["Line"] = "71";
            H18["CurryAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[9]["AYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[9]["AEndAmount"].ToString()),2);
            H18["PreAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[9]["PreAYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[9]["PreAEndAmount"].ToString()),2);
            H18["ByOrder"] = "48";
            dt.Rows.Add(H18);


            DataRow H19 = dt.NewRow();
            H19["ItemName"] = ConstUtil.ItemName49;
            H19["Line"] = "72";
            H19["CurryAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[3]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[4]["AYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[4]["AEndAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[5]["AYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[5]["AEndAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[8]["AYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[8]["AEndAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[26]["AYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[26]["AEndAmount"].ToString()),2);
            H19["PreAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[3]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[4]["PreAYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[4]["PreAEndAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[5]["PreAYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[5]["PreAEndAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[8]["PreAYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[8]["PreAEndAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[26]["PreAYearBeginAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[26]["PreAEndAmount"].ToString()),2);
            H19["ByOrder"] = "49";
            dt.Rows.Add(H19);



            DataRow H20 = dt.NewRow();
            H20["ItemName"] = ConstUtil.ItemName50;
            H20["Line"] = "73";
            H20["CurryAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[34]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[34]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[35]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[35]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[36]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[36]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[37]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[37]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[38]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[38]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[39]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[39]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[40]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[40]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[41]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[41]["AYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[42]["AEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[42]["AYearBeginAmount"].ToString()),2);
            H20["PreAmount"] = Math.Round(Convert.ToDecimal(BalanceSheetDT.Rows[34]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[34]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[35]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[35]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[36]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[36]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[37]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[37]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[38]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[38]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[39]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[39]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[40]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[40]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[41]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[41]["PreAYearBeginAmount"].ToString()) + Convert.ToDecimal(BalanceSheetDT.Rows[42]["PreAEndAmount"].ToString()) - Convert.ToDecimal(BalanceSheetDT.Rows[42]["PreAYearBeginAmount"].ToString()),2);
            H20["ByOrder"] = "50";
            dt.Rows.Add(H20);



            DataRow H21 = dt.NewRow();
            H21["ItemName"] = ConstUtil.ItemName51;
            H21["Line"] = "74";
            H21["CurryAmount"] = Math.Round(Convert.ToDecimal(D15["CurryAmount"].ToString()) - Convert.ToDecimal(H6["CurryAmount"].ToString()) - Convert.ToDecimal(H7["CurryAmount"].ToString()) - Convert.ToDecimal(H8["CurryAmount"].ToString()) - Convert.ToDecimal(H9["CurryAmount"].ToString()) - Convert.ToDecimal(H10["CurryAmount"].ToString()) - Convert.ToDecimal(H11["CurryAmount"].ToString()) - Convert.ToDecimal(H12["CurryAmount"].ToString()) - Convert.ToDecimal(H13["CurryAmount"].ToString()) - Convert.ToDecimal(H14["CurryAmount"].ToString()) - Convert.ToDecimal(H15["CurryAmount"].ToString()) - Convert.ToDecimal(H16["CurryAmount"].ToString()) - Convert.ToDecimal(H17["CurryAmount"].ToString()) - Convert.ToDecimal(H18["CurryAmount"].ToString()) - Convert.ToDecimal(H19["CurryAmount"].ToString()) - Convert.ToDecimal(H20["CurryAmount"].ToString()),2);
            H21["PreAmount"] =Math.Round(Convert.ToDecimal(D15["PreAmount"].ToString()) - Convert.ToDecimal(H6["PreAmount"].ToString()) - Convert.ToDecimal(H7["PreAmount"].ToString()) - Convert.ToDecimal(H8["PreAmount"].ToString()) - Convert.ToDecimal(H9["PreAmount"].ToString()) - Convert.ToDecimal(H10["PreAmount"].ToString()) - Convert.ToDecimal(H11["PreAmount"].ToString()) - Convert.ToDecimal(H12["PreAmount"].ToString()) - Convert.ToDecimal(H13["PreAmount"].ToString()) - Convert.ToDecimal(H14["PreAmount"].ToString()) - Convert.ToDecimal(H15["PreAmount"].ToString()) - Convert.ToDecimal(H16["PreAmount"].ToString()) - Convert.ToDecimal(H17["PreAmount"].ToString()) - Convert.ToDecimal(H18["PreAmount"].ToString()) - Convert.ToDecimal(H19["PreAmount"].ToString()) - Convert.ToDecimal(H20["PreAmount"].ToString()),2);
            H21["ByOrder"] = "51";
            dt.Rows.Add(H21);



            DataRow H22 = dt.NewRow();
            H22["ItemName"] = ConstUtil.ItemName52;
            H22["Line"] = "75";
            H22["CurryAmount"] = Math.Round(Convert.ToDecimal(D15["CurryAmount"].ToString()),2);
            H22["PreAmount"] = Math.Round(Convert.ToDecimal(D15["PreAmount"].ToString()),2);
            H22["ByOrder"] = "52";
            dt.Rows.Add(H22);


            DataRow H23 = dt.NewRow();
            H23["ItemName"] = ConstUtil.ItemName53;
            H23["Line"] = "";
            H23["CurryAmount"] = "0";
            H23["PreAmount"] = "0";
            H23["ByOrder"] = "53";
            dt.Rows.Add(H23);

            DataRow H24 = dt.NewRow();
            H24["ItemName"] = ConstUtil.ItemName54;
            H24["Line"] = "";
            H24["CurryAmount"] = "0";
            H24["PreAmount"] = "0";
            H24["ByOrder"] = "54";
            dt.Rows.Add(H24);

            DataRow H25 = dt.NewRow();
            H25["ItemName"] = ConstUtil.ItemName55;
            H25["Line"] = "";
            H25["CurryAmount"] = "0";
            H25["PreAmount"] = "0";
            H25["ByOrder"] = "55";
            dt.Rows.Add(H25);

            DataRow H26 = dt.NewRow();
            H26["ItemName"] = ConstUtil.ItemName56;
            H26["Line"] = "";
            H26["CurryAmount"] = "0";
            H26["PreAmount"] = "0";
            H26["ByOrder"] = "56";
            dt.Rows.Add(H26);


            DataRow H27 = dt.NewRow();
            H27["ItemName"] = ConstUtil.ItemName57;
            H27["Line"] = "76";
            H27["CurryAmount"] = "0";
            H27["PreAmount"] = "0";
            H27["ByOrder"] = "57";
            dt.Rows.Add(H27);


            DataRow H28 = dt.NewRow();
            H28["ItemName"] = ConstUtil.ItemName58;
            H28["Line"] = "77";
            H28["CurryAmount"] = "0";
            H28["PreAmount"] = "0";
            H28["ByOrder"] = "58";
            dt.Rows.Add(H28);


            DataRow H29 = dt.NewRow();
            H29["ItemName"] = ConstUtil.ItemName59;
            H29["Line"] = "78";
            H29["CurryAmount"] = "0";
            H29["PreAmount"] = "0";
            H29["ByOrder"] = "59";
            dt.Rows.Add(H29);
            /*按ByOrder升序重新排列DataTable开始*/
            DataRow[] rows = dt.Select("", " ByOrder asc");

            DataTable dt2 = dt.Clone();
            dt2.Rows.Clear();

            foreach (DataRow row in rows)
                dt2.ImportRow(row);
            /*按ByOrder升序重新排列DataTable结束*/

            /*重组datatable格式开始*/
            DataTable tmpdt = new DataTable();
            tmpdt.Columns.Add("AItemName");
            tmpdt.Columns.Add("ALine");
            tmpdt.Columns.Add("ACurryAmount");
            tmpdt.Columns.Add("APreAmount");
            tmpdt.Columns.Add("BItemName");
            tmpdt.Columns.Add("BLine");
            tmpdt.Columns.Add("BCurryAmount");
            tmpdt.Columns.Add("BPreAmount");
            tmpdt.Columns.Add("ByOrder");

            for (int i = 0; i < 34; i++)
            {
                DataRow dr = tmpdt.NewRow();
                dr["AItemName"] = dt2.Rows[i]["ItemName"].ToString();
                dr["ALine"] = dt2.Rows[i]["Line"].ToString();
                dr["ACurryAmount"] = dt2.Rows[i]["CurryAmount"].ToString() == "0" ? "" : dt2.Rows[i]["CurryAmount"].ToString() == "0.00" ? "" : dt2.Rows[i]["CurryAmount"].ToString();
                dr["APreAmount"] = dt2.Rows[i]["PreAmount"].ToString() == "0" ? "" : dt2.Rows[i]["PreAmount"].ToString() == "0.00" ? "" : dt2.Rows[i]["PreAmount"].ToString();
                dr["BItemName"] = dt2.Rows[i + 34]["ItemName"].ToString();
                dr["BLine"] = dt2.Rows[i + 34]["Line"].ToString();
                dr["BCurryAmount"] = dt2.Rows[i + 34]["CurryAmount"].ToString() == "0" ? "" : dt2.Rows[i + 34]["CurryAmount"].ToString() == "0.00" ? "" : dt2.Rows[i + 34]["CurryAmount"].ToString();
                dr["BPreAmount"] = dt2.Rows[i + 34]["PreAmount"].ToString() == "0" ? "" : dt2.Rows[i + 34]["PreAmount"].ToString() == "0.00" ? "" : dt2.Rows[i + 34]["PreAmount"].ToString();
                dr["ByOrder"] = "1";
                tmpdt.Rows.Add(dr);
            }
            /*重组datatable格式结束*/
            return tmpdt;
        }

        #endregion

        #region 上一张，下一张 凭证修改页面功能
        /// <summary>
        /// 上一张，下一张 凭证修改页面功能
        /// </summary>
        /// <param name="ID">凭证ID</param>
        /// <param name="type">1：上一张；2：下一张</param>
        /// <returns></returns>
        public static int GetPreOrNextAttestBillID(int ID, int type)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
            try
            {
                if (string.IsNullOrEmpty(CompanyCD))
                    return 0;
                else
                {
                    return VoucherDBHelper.GetPreOrNextAttestBillID(ID, type, CompanyCD);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
#endregion

        #region 科目余额表数据源绑定(本位币或综合本位币)
        /// <summary>
        /// 科目余额表数据源绑定(本位币或综合本位币)
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="CurryTypeID">币种</param>
        /// <returns></returns>
        public static DataTable GetAccountReportSourse(string startDate, string endDate, string CurryTypeID,string SubjectsCD)
        {
            try
            {
                string YearBeginDate = startDate.Split('-')[0].ToString() + "-01-01";
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                DataTable dt = new DataTable();
                dt.Columns.Add("SubjectsCD");//会计科目代码
                dt.Columns.Add("SubjectsCDName");//会计科目名称
                dt.Columns.Add("BeginDebitAmount");//期初借方
                dt.Columns.Add("BeginCreditAmount");//期初贷方
                dt.Columns.Add("ThisDebitAmount");//本期借方发生额
                dt.Columns.Add("ThisCreditAmount");//本期贷方发生额
                dt.Columns.Add("YearDebit");//本年累计发生额（借方）
                dt.Columns.Add("YearCredit");//本年累计发生额（贷方）
                dt.Columns.Add("EndDebitAmount");//期末借方
                dt.Columns.Add("EndCreditAmount");//期末贷方
                dt.Columns.Add("ByOrderr");//排序

                if (VoucherDBHelper.IsNullSubjects(companyCD) && CurryTypeID.Trim().Length > 0)
                {
                    string subjectsCDstr = string.Empty;
                    if (SubjectsCD.Trim().Length > 0)
                        subjectsCDstr += SubjectsCD + ",";
                    else
                    {

                        DataTable subjectsDT1 = VoucherDBHelper.GetSpreadsheetSubjectsCD(startDate, endDate, CurryTypeID, companyCD);

                        /*根据具体科目获取顶级科目 Start*/

                        foreach (DataRow dr in subjectsDT1.Rows)
                        {
                            string PresubjectsCD = VoucherDBHelper.GetSubjectsPerCD(dr["SubjectsCD"].ToString()).ToString().Split(',')[0].ToString();
                            if (subjectsCDstr.Contains(PresubjectsCD))
                            {

                            }
                            else
                            {
                                subjectsCDstr += PresubjectsCD + ",";
                            }
                        }
                    }
                    subjectsCDstr = subjectsCDstr.TrimEnd(new char[] { ',' });
                    subjectsCDstr = subjectsCDstr.Replace("'", "");
                    if (subjectsCDstr.Trim().Length > 0)
                    {
                        string[] Str = subjectsCDstr.Split(',');
                        DataTable subjectsDT = new DataTable();
                        subjectsDT.Columns.Add("SubjectsCD");
                        for (int m = 0; m < Str.Length; m++)
                        {
                            DataRow drr = subjectsDT.NewRow();
                            drr["SubjectsCD"] = Str[m].ToString();
                            subjectsDT.Rows.Add(drr);
                        }
                        /*根据具体科目获取顶级科目 End*/

                        /*合计信息初始化开始*/
                        decimal BeginDebitAmountSum = 0;//期初借方合计
                        decimal BeginCreditAmountSum = 0;//期初贷方合计
                        decimal ThisDebitAmountSum = 0;//本期借方发生额合计
                        decimal ThisCreditAmountSum = 0;//本期贷方发生额合计
                        decimal YearDebit = 0;//本年累计发生额（借方)
                        decimal YearCredit = 0;//本年累计发生额（贷方)
                        decimal EndDebitAmountSum = 0;//期末借方合计
                        decimal EndCreditAmountSum = 0;//期末贷方合计
                        /*合计信息初始化结束*/


                        for (int i = 0; i < subjectsDT.Rows.Count; i++)
                        {
                            DataRow row = dt.NewRow();

                            int Dirdt = VoucherDBHelper.GetSubjectDirection(subjectsDT.Rows[i]["SubjectsCD"].ToString(), companyCD);
                            /*根据科目编码获取该科目方向结束*/

                            row["SubjectsCD"] = subjectsDT.Rows[i]["SubjectsCD"].ToString();//科目编码
                            row["SubjectsCDName"] = GetSubjectsName(subjectsDT.Rows[i]["SubjectsCD"].ToString());//科目名称
                            row["ByOrderr"] = "1";//排序


                            /*根据方向 计算期初借方，期初贷方开始*/
                            string queryStr = " VoucherDate<'" + startDate + "' and  SubjectsCD in (" + GetSubjectsNextCD(subjectsDT.Rows[i]["SubjectsCD"].ToString()) + ")  and CompanyCD='" + companyCD + "' and  CurrencyTypeID in ( " + CurryTypeID + " ) ";
                            decimal beginAmount = 0;
                            beginAmount = AcountBookDBHelper.GetAccountBookBeginAmount(queryStr, subjectsDT.Rows[i]["SubjectsCD"].ToString(), CurryTypeID);

                            decimal defaultAmount = AcountBookDBHelper.GetBeginCurryTypeAmount(subjectsDT.Rows[i]["SubjectsCD"].ToString(), CurryTypeID, companyCD);
                            beginAmount += defaultAmount;
                            switch (Dirdt)
                            {
                                case 0://借
                                    row["BeginDebitAmount"] = Math.Round(beginAmount, 2).ToString("#,###0.#0");//期初借方
                                    row["BeginCreditAmount"] = "0";//期初贷方
                                    break;
                                case 1://贷
                                    row["BeginDebitAmount"] = "0"; //期初借方
                                    row["BeginCreditAmount"] = Math.Round(beginAmount, 2).ToString("#,###0.#0");//期初贷方
                                    break;
                                default:
                                    break;
                            }
                            /*根据方向 计算期初借方，期初贷方开始*/

                            /*计算本期借/贷方发生额开始*/
                            string thisQueryStr = " VoucherDate>='" + startDate + "' and VoucherDate<='" + endDate + "' and CompanyCD='" + companyCD + "' and SubjectsCD in (" + GetSubjectsNextCD(subjectsDT.Rows[i]["SubjectsCD"].ToString()) + ") and CurrencyTypeID in (" + CurryTypeID + ")   ";

                            string YearQueryStr = "VoucherDate>='" + YearBeginDate + "' and VoucherDate<='" + endDate + "' and CompanyCD='" + companyCD + "' and SubjectsCD in (" + GetSubjectsNextCD(subjectsDT.Rows[i]["SubjectsCD"].ToString()) + ") and CurrencyTypeID in (" + CurryTypeID + ")   ";

                            row["ThisDebitAmount"] = "0";//本期借方发生额
                            row["ThisCreditAmount"] = "0";//本期贷方发生额
                            row["YearDebit"] = "0";//本年累计发生额（借方)
                            row["YearCredit"] = "0";//本年累计发生额（贷方)

                            DataTable sumDT = AcountBookDBHelper.GetAcountSumAmount(thisQueryStr);//统计对应科目的借方金额合计和贷方金额合计(本期)
                            DataTable YearSumDT = AcountBookBus.GetAcountSumAmount(YearQueryStr);//统计对应科目的借方金额合计和贷方金额合计(本年)
                            if (sumDT.Rows.Count > 0)
                            {
                                if (CurryTypeID.LastIndexOf(",") == -1)
                                {
                                    row["ThisDebitAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ForeignThisDebit"].ToString()), 2).ToString("#,###0.#0");//本期借方发生额
                                    row["ThisCreditAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ForeignThisCredit"].ToString()), 2).ToString("#,###0.#0");//本期贷方发生额
                                }
                                else
                                {
                                    row["ThisDebitAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ThisDebit"].ToString()), 2).ToString("#,###0.#0");//本期借方发生额
                                    row["ThisCreditAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ThisCredit"].ToString()), 2).ToString("#,###0.#0");//本期贷方发生额
                                }

                            }

                            if (YearSumDT.Rows.Count > 0)
                            {
                                if (CurryTypeID.LastIndexOf(",") == -1)
                                {
                                    row["YearDebit"] = Math.Round(Convert.ToDecimal(YearSumDT.Rows[0]["ForeignThisDebit"].ToString()), 2).ToString("#,###0.#0");//本期借方发生额
                                    row["YearCredit"] = Math.Round(Convert.ToDecimal(YearSumDT.Rows[0]["ForeignThisCredit"].ToString()), 2).ToString("#,###0.#0");//本期贷方发生额
                                }
                                else
                                {
                                    row["YearDebit"] = Math.Round(Convert.ToDecimal(YearSumDT.Rows[0]["ThisDebit"].ToString()), 2).ToString("#,###0.#0");//本年借方发生额
                                    row["YearCredit"] = Math.Round(Convert.ToDecimal(YearSumDT.Rows[0]["ThisCredit"].ToString()), 2).ToString("#,###0.#0");//本年贷方发生额
                                }

                            }

                            /*计算本期借/贷方发生额结束*/
                            /*根据方向 计算期末借方，期末贷方开始*/
                            switch (Dirdt)
                            {
                                case 0://借
                                    row["EndDebitAmount"] = Math.Round(Convert.ToDecimal(row["BeginDebitAmount"].ToString()) + Convert.ToDecimal(row["ThisDebitAmount"].ToString()) - Convert.ToDecimal(row["ThisCreditAmount"].ToString()), 2).ToString("#,###0.#0");//期末借方
                                    row["EndCreditAmount"] = "0";//期末贷方
                                    break;
                                case 1://贷
                                    row["EndDebitAmount"] = "0";//期末借方
                                    row["EndCreditAmount"] = Math.Round(Convert.ToDecimal(row["BeginCreditAmount"].ToString()) + Convert.ToDecimal(row["ThisCreditAmount"].ToString()) - Convert.ToDecimal(row["ThisDebitAmount"].ToString()), 2).ToString("#,###0.#0");//期末贷方
                                    break;
                                default:
                                    break;
                            }
                            /*根据方向 计算期末借方，期末贷方结束*/

                            /*合计信息计算开始*/
                            BeginDebitAmountSum += Convert.ToDecimal(row["BeginDebitAmount"].ToString());//期初借方合计
                            BeginCreditAmountSum += Convert.ToDecimal(row["BeginCreditAmount"].ToString());//期初贷方合计
                            ThisDebitAmountSum += Convert.ToDecimal(row["ThisDebitAmount"].ToString());//本期借方发生额合计
                            ThisCreditAmountSum += Convert.ToDecimal(row["ThisCreditAmount"].ToString());//本期贷方发生额合计
                            YearDebit += Convert.ToDecimal(row["YearDebit"].ToString());//本期借方发生额合计
                            YearCredit += Convert.ToDecimal(row["YearCredit"].ToString());//本期贷方发生额合计
                            EndDebitAmountSum += Convert.ToDecimal(row["EndDebitAmount"].ToString());//期末借方合计
                            EndCreditAmountSum += Convert.ToDecimal(row["EndCreditAmount"].ToString());//期末贷方合计
                            /*合计信息计算结束*/


                            dt.Rows.Add(row);
                        }

                        DataRow roww = dt.NewRow();
                        roww["SubjectsCD"] = "";
                        roww["SubjectsCDName"] = "合计";
                        roww["BeginDebitAmount"] = Math.Round(BeginDebitAmountSum, 2).ToString("#,###0.#0");
                        roww["BeginCreditAmount"] = Math.Round(BeginCreditAmountSum, 2).ToString("#,###0.#0");
                        roww["ThisDebitAmount"] = Math.Round(ThisDebitAmountSum, 2).ToString("#,###0.#0");
                        roww["ThisCreditAmount"] = Math.Round(ThisCreditAmountSum, 2).ToString("#,###0.#0");
                        roww["YearDebit"] = Math.Round(YearDebit, 2).ToString("#,###0.#0");
                        roww["YearCredit"] = Math.Round(YearCredit, 2).ToString("#,###0.#0");
                        roww["EndDebitAmount"] = Math.Round(EndDebitAmountSum, 2).ToString("#,###0.#0");
                        roww["EndCreditAmount"] = Math.Round(EndCreditAmountSum, 2).ToString("#,###0.#0");
                        roww["ByOrderr"] = "1";
                        dt.Rows.Add(roww);
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

        #region 科目余额表数据源绑定(其他外币统计)
        /// <summary>
        /// 科目余额表数据源绑定(其他外币统计)
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="CurryTypeID">币种</param>
        /// <returns></returns>
        public static DataTable GetForeignAccountReportSourse(string startDate, string endDate, string CurryTypeID, string SubjectsCD)
        {
            try
            {
                string YearBeginDate = startDate.Split('-')[0].ToString() + "-01-01";
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                DataTable dt = new DataTable();
                dt.Columns.Add("SubjectsCD");//会计科目代码
                dt.Columns.Add("SubjectsCDName");//会计科目名称

                dt.Columns.Add("BeginDebitAmount");//期初借方（本位币）
                dt.Columns.Add("BeginCreditAmount");//期初贷方（本位币）
                dt.Columns.Add("ThisDebitAmount");//本期借方发生额（本位币）
                dt.Columns.Add("ThisCreditAmount");//本期贷方发生额（本位币）
                dt.Columns.Add("YearDebit");//本年累计发生额（借方）（本位币）
                dt.Columns.Add("YearCredit");//本年累计发生额（贷方）（本位币）
                dt.Columns.Add("EndDebitAmount");//期末借方（本位币）
                dt.Columns.Add("EndCreditAmount");//期末贷方（本位币）


                dt.Columns.Add("ForeignBeginDebitAmount");//期初借方（原币）
                dt.Columns.Add("ForeignBeginCreditAmount");//期初贷方（原币）
                dt.Columns.Add("ForeignThisDebitAmount");//本期借方发生额（原币）
                dt.Columns.Add("ForeignThisCreditAmount");//本期贷方发生额（原币）
                dt.Columns.Add("ForeignYearDebit");//本年累计发生额（借方）（原币）
                dt.Columns.Add("ForeignYearCredit");//本年累计发生额（贷方）（原币）
                dt.Columns.Add("ForeignEndDebitAmount");//期末借方（原币）
                dt.Columns.Add("ForeignEndCreditAmount");//期末贷方（原币）


                dt.Columns.Add("ByOrderr");//排序

                if (VoucherDBHelper.IsNullSubjects(companyCD) && CurryTypeID.Trim().Length > 0)
                {
                    string subjectsCDstr = string.Empty;
                    if (SubjectsCD.Trim().Length > 0)
                        subjectsCDstr += SubjectsCD + ",";
                    else
                    {

                        DataTable subjectsDT1 = VoucherDBHelper.GetSpreadsheetSubjectsCD(startDate, endDate, CurryTypeID, companyCD);

                        /*根据具体科目获取顶级科目 Start*/

                        foreach (DataRow dr in subjectsDT1.Rows)
                        {
                            string PresubjectsCD = VoucherDBHelper.GetSubjectsPerCD(dr["SubjectsCD"].ToString()).ToString().Split(',')[0].ToString();
                            if (subjectsCDstr.Contains(PresubjectsCD))
                            {

                            }
                            else
                            {
                                subjectsCDstr += PresubjectsCD + ",";
                            }
                        }
                    }
                    subjectsCDstr = subjectsCDstr.TrimEnd(new char[] { ',' });
                    subjectsCDstr = subjectsCDstr.Replace("'", "");
                    if (subjectsCDstr.Trim().Length > 0)
                    {
                        DataTable subjectsDT = new DataTable();
                        if (subjectsCDstr.Trim().Length > 0 || subjectsCDstr != "")
                        {
                            string[] Str = subjectsCDstr.Split(',');

                            subjectsDT.Columns.Add("SubjectsCD");
                            for (int m = 0; m < Str.Length; m++)
                            {
                                DataRow drr = subjectsDT.NewRow();
                                drr["SubjectsCD"] = Str[m].ToString();
                                subjectsDT.Rows.Add(drr);
                            }
                        }
                        /*根据具体科目获取顶级科目 End*/


                        string CurrencyTypeIDStr = string.Empty;
                        DataTable CurrencyTypeDt = CurrTypeSettingDBHelper.GetCurrTypeByCompanyCD(companyCD);
                        for (int p = 0; p < CurrencyTypeDt.Rows.Count; p++)
                        {
                            CurrencyTypeIDStr += CurrencyTypeDt.Rows[p]["ID"].ToString() + ",";

                        }
                        CurrencyTypeIDStr = CurrencyTypeIDStr.TrimEnd(new char[] { ',' });//综合本位币所有币种ID集


                        /*合计信息初始化开始*/
                        decimal BeginDebitAmountSum = 0;//期初借方合计（本位币）
                        decimal BeginCreditAmountSum = 0;//期初贷方合计（本位币）
                        decimal ThisDebitAmountSum = 0;//本期借方发生额合计（本位币）
                        decimal ThisCreditAmountSum = 0;//本期贷方发生额合计（本位币）
                        decimal YearDebit = 0;//本年累计发生额（借方)（本位币）
                        decimal YearCredit = 0;//本年累计发生额（贷方)（本位币）
                        decimal EndDebitAmountSum = 0;//期末借方合计（本位币）
                        decimal EndCreditAmountSum = 0;//期末贷方合计（本位币）


                        decimal ForeignBeginDebitAmountSum = 0;//期初借方合计（原币）
                        decimal ForeignBeginCreditAmountSum = 0;//期初贷方合计（原币）
                        decimal ForeignThisDebitAmountSum = 0;//本期借方发生额合计（原币）
                        decimal ForeignThisCreditAmountSum = 0;//本期贷方发生额合计（原币）
                        decimal ForeignYearDebit = 0;//本年累计发生额（借方)（原币）
                        decimal ForeignYearCredit = 0;//本年累计发生额（贷方)（原币）
                        decimal ForeignEndDebitAmountSum = 0;//期末借方合计（原币）
                        decimal ForeignEndCreditAmountSum = 0;//期末贷方合计（原币）


                        /*合计信息初始化结束*/


                        for (int i = 0; i < subjectsDT.Rows.Count; i++)
                        {
                            DataRow row = dt.NewRow();

                            int Dirdt = VoucherDBHelper.GetSubjectDirection(subjectsDT.Rows[i]["SubjectsCD"].ToString(), companyCD);
                            /*根据科目编码获取该科目方向结束*/

                            row["SubjectsCD"] = subjectsDT.Rows[i]["SubjectsCD"].ToString();//科目编码
                            row["SubjectsCDName"] = GetSubjectsName(subjectsDT.Rows[i]["SubjectsCD"].ToString());//科目名称
                            row["ByOrderr"] = "1";//排序


                            /*根据方向 计算期初借方，期初贷方开始*/
                            string queryStr = " VoucherDate<'" + startDate + "' and  SubjectsCD in (" + GetSubjectsNextCD(subjectsDT.Rows[i]["SubjectsCD"].ToString()) + ")  and CompanyCD='" + companyCD + "' and  CurrencyTypeID in ( " + CurrencyTypeIDStr + " ) ";

                            string ForeignQuerStr = " VoucherDate<'" + startDate + "' and  SubjectsCD in (" + GetSubjectsNextCD(subjectsDT.Rows[i]["SubjectsCD"].ToString()) + ")  and CompanyCD='" + companyCD + "' and  CurrencyTypeID in ( " + CurryTypeID + " ) ";

                            decimal beginAmount = 0;
                            decimal ForeignBeginAmount = 0;
                            beginAmount = AcountBookDBHelper.GetAccountBookBeginAmount(queryStr, subjectsDT.Rows[i]["SubjectsCD"].ToString(), CurrencyTypeIDStr);
                            ForeignBeginAmount = AcountBookDBHelper.GetAccountBookBeginAmount(ForeignQuerStr, subjectsDT.Rows[i]["SubjectsCD"].ToString(), CurryTypeID);


                            decimal defaultAmount = AcountBookDBHelper.GetBeginCurryTypeAmount(subjectsDT.Rows[i]["SubjectsCD"].ToString(), CurrencyTypeIDStr, companyCD);
                            decimal ForeignDefalutAmount = AcountBookDBHelper.GetBeginCurryTypeAmount(subjectsDT.Rows[i]["SubjectsCD"].ToString(), CurryTypeID, companyCD);

                            beginAmount += defaultAmount;
                            ForeignBeginAmount += ForeignDefalutAmount;
                            //GetBeginAmount(queryStr, Dirdt.ToString());
                            switch (Dirdt)
                            {
                                case 0://借
                                    row["BeginDebitAmount"] = Math.Round(beginAmount, 2).ToString("#,###0.#0");//期初借方
                                    row["BeginCreditAmount"] = "0";//期初贷方
                                    row["ForeignBeginDebitAmount"] = Math.Round(ForeignBeginAmount, 2).ToString("#,###0.#0");//期初借方
                                    row["ForeignBeginCreditAmount"] = "0";//期初贷方
                                    break;
                                case 1://贷
                                    row["BeginDebitAmount"] = "0"; //期初借方
                                    row["BeginCreditAmount"] = Math.Round(beginAmount, 2).ToString("#,###0.#0");//期初贷方
                                    row["ForeignBeginDebitAmount"] = "0"; //期初借方
                                    row["ForeignBeginCreditAmount"] = Math.Round(ForeignBeginAmount, 2).ToString("#,###0.#0");//期初贷方
                                    break;
                                default:
                                    break;
                            }
                            /*根据方向 计算期初借方，期初贷方开始*/

                            /*计算本期借/贷方发生额开始*/
                            string thisQueryStr = " VoucherDate>='" + startDate + "' and VoucherDate<='" + endDate + "' and CompanyCD='" + companyCD + "' and SubjectsCD in (" + GetSubjectsNextCD(subjectsDT.Rows[i]["SubjectsCD"].ToString()) + ") and CurrencyTypeID in (" + CurryTypeID + ")   ";

                            string YearQueryStr = "VoucherDate>='" + YearBeginDate + "' and VoucherDate<='" + endDate + "' and CompanyCD='" + companyCD + "' and SubjectsCD in (" + GetSubjectsNextCD(subjectsDT.Rows[i]["SubjectsCD"].ToString()) + ") and CurrencyTypeID in (" + CurryTypeID + ")   ";





                            row["ThisDebitAmount"] = "0";//本期借方发生额
                            row["ThisCreditAmount"] = "0";//本期贷方发生额
                            row["YearDebit"] = "0";//本年累计发生额（借方)
                            row["YearCredit"] = "0";//本年累计发生额（贷方)


                            row["ForeignThisDebitAmount"] = "0";//本期借方发生额
                            row["ForeignThisCreditAmount"] = "0";//本期贷方发生额
                            row["ForeignYearDebit"] = "0";//本年累计发生额（借方)
                            row["ForeignYearCredit"] = "0";//本年累计发生额（贷方)

                            DataTable sumDT = AcountBookDBHelper.GetAcountSumAmount(thisQueryStr);//统计对应科目的借方金额合计和贷方金额合计(本期)
                            DataTable YearSumDT = AcountBookBus.GetAcountSumAmount(YearQueryStr);//统计对应科目的借方金额合计和贷方金额合计(本年)



                            if (sumDT.Rows.Count > 0)
                            {
                                row["ThisDebitAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ThisDebit"].ToString()), 2).ToString("#,###0.#0");//本期借方发生额
                                row["ThisCreditAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ThisCredit"].ToString()), 2).ToString("#,###0.#0");//本期贷方发生额

                                row["ForeignThisDebitAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ForeignThisDebit"].ToString()), 2).ToString("#,###0.#0");//本期借方发生额
                                row["ForeignThisCreditAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ForeignThisCredit"].ToString()), 2).ToString("#,###0.#0");//本期贷方发生额

                            }




                            if (YearSumDT.Rows.Count > 0)
                            {
                                row["YearDebit"] = Math.Round(Convert.ToDecimal(YearSumDT.Rows[0]["ThisDebit"].ToString()), 2).ToString("#,###0.#0");//本期借方发生额
                                row["YearCredit"] = Math.Round(Convert.ToDecimal(YearSumDT.Rows[0]["ThisCredit"].ToString()), 2).ToString("#,###0.#0");//本期贷方发生额

                                row["ForeignYearDebit"] = Math.Round(Convert.ToDecimal(YearSumDT.Rows[0]["ForeignThisDebit"].ToString()), 2).ToString("#,###0.#0");//本期借方发生额
                                row["ForeignYearCredit"] = Math.Round(Convert.ToDecimal(YearSumDT.Rows[0]["ForeignThisCredit"].ToString()), 2).ToString("#,###0.#0");//本期贷方发生额
                            }


                            /*计算本期借/贷方发生额结束*/
                            /*根据方向 计算期末借方，期末贷方开始*/
                            switch (Dirdt)
                            {
                                case 0://借
                                    row["EndDebitAmount"] = Math.Round(Convert.ToDecimal(row["BeginDebitAmount"].ToString()) + Convert.ToDecimal(row["ThisDebitAmount"].ToString()) - Convert.ToDecimal(row["ThisCreditAmount"].ToString()), 2).ToString("#,###0.#0");//期末借方
                                    row["EndCreditAmount"] = "0";//期末贷方



                                    row["ForeignEndDebitAmount"] = Math.Round(Convert.ToDecimal(row["ForeignBeginDebitAmount"].ToString()) + Convert.ToDecimal(row["ForeignThisDebitAmount"].ToString()) - Convert.ToDecimal(row["ForeignThisCreditAmount"].ToString()), 2).ToString("#,###0.#0");//期末借方
                                    row["ForeignEndCreditAmount"] = "0";//期末贷方



                                    break;
                                case 1://贷
                                    row["EndDebitAmount"] = "0";//期末借方
                                    row["EndCreditAmount"] = Math.Round(Convert.ToDecimal(row["BeginCreditAmount"].ToString()) + Convert.ToDecimal(row["ThisCreditAmount"].ToString()) - Convert.ToDecimal(row["ThisDebitAmount"].ToString()), 2).ToString("#,###0.#0");//期末贷方


                                    row["ForeignEndDebitAmount"] = "0";//期末借方
                                    row["ForeignEndCreditAmount"] = Math.Round(Convert.ToDecimal(row["ForeignBeginCreditAmount"].ToString()) + Convert.ToDecimal(row["ForeignThisCreditAmount"].ToString()) - Convert.ToDecimal(row["ForeignThisDebitAmount"].ToString()), 2).ToString("#,###0.#0");//期末贷方


                                    break;
                                default:
                                    break;
                            }
                            /*根据方向 计算期末借方，期末贷方结束*/

                            /*合计信息计算开始*/
                            BeginDebitAmountSum += Convert.ToDecimal(row["BeginDebitAmount"].ToString());//期初借方合计
                            BeginCreditAmountSum += Convert.ToDecimal(row["BeginCreditAmount"].ToString());//期初贷方合计
                            ThisDebitAmountSum += Convert.ToDecimal(row["ThisDebitAmount"].ToString());//本期借方发生额合计
                            ThisCreditAmountSum += Convert.ToDecimal(row["ThisCreditAmount"].ToString());//本期贷方发生额合计
                            YearDebit += Convert.ToDecimal(row["YearDebit"].ToString());//本期借方发生额合计
                            YearCredit += Convert.ToDecimal(row["YearCredit"].ToString());//本期贷方发生额合计
                            EndDebitAmountSum += Convert.ToDecimal(row["EndDebitAmount"].ToString());//期末借方合计
                            EndCreditAmountSum += Convert.ToDecimal(row["EndCreditAmount"].ToString());//期末贷方合计



                            ForeignBeginDebitAmountSum += Convert.ToDecimal(row["ForeignBeginDebitAmount"].ToString());//期初借方合计
                            ForeignBeginCreditAmountSum += Convert.ToDecimal(row["ForeignBeginCreditAmount"].ToString());//期初贷方合计
                            ForeignThisDebitAmountSum += Convert.ToDecimal(row["ForeignThisDebitAmount"].ToString());//本期借方发生额合计
                            ForeignThisCreditAmountSum += Convert.ToDecimal(row["ForeignThisCreditAmount"].ToString());//本期贷方发生额合计
                            ForeignYearDebit += Convert.ToDecimal(row["ForeignYearDebit"].ToString());//本期借方发生额合计
                            ForeignYearCredit += Convert.ToDecimal(row["ForeignYearCredit"].ToString());//本期贷方发生额合计
                            ForeignEndDebitAmountSum += Convert.ToDecimal(row["ForeignEndDebitAmount"].ToString());//期末借方合计
                            ForeignEndCreditAmountSum += Convert.ToDecimal(row["ForeignEndCreditAmount"].ToString());//期末贷方合计



                            /*合计信息计算结束*/


                            dt.Rows.Add(row);
                        }

                        DataRow roww = dt.NewRow();
                        roww["SubjectsCD"] = "";
                        roww["SubjectsCDName"] = "合计";
                        roww["BeginDebitAmount"] = Math.Round(BeginDebitAmountSum, 2).ToString("#,###0.#0");
                        roww["BeginCreditAmount"] = Math.Round(BeginCreditAmountSum, 2).ToString("#,###0.#0");
                        roww["ThisDebitAmount"] = Math.Round(ThisDebitAmountSum, 2).ToString("#,###0.#0");
                        roww["ThisCreditAmount"] = Math.Round(ThisCreditAmountSum, 2).ToString("#,###0.#0");
                        roww["YearDebit"] = Math.Round(YearDebit, 2).ToString("#,###0.#0");
                        roww["YearCredit"] = Math.Round(YearCredit, 2).ToString("#,###0.#0");
                        roww["EndDebitAmount"] = Math.Round(EndDebitAmountSum, 2).ToString("#,###0.#0");
                        roww["EndCreditAmount"] = Math.Round(EndCreditAmountSum, 2).ToString("#,###0.#0");


                        roww["ForeignBeginDebitAmount"] = Math.Round(ForeignBeginDebitAmountSum, 2).ToString("#,###0.#0");
                        roww["ForeignBeginCreditAmount"] = Math.Round(ForeignBeginCreditAmountSum, 2).ToString("#,###0.#0");
                        roww["ForeignThisDebitAmount"] = Math.Round(ForeignThisDebitAmountSum, 2).ToString("#,###0.#0");
                        roww["ForeignThisCreditAmount"] = Math.Round(ForeignThisCreditAmountSum, 2).ToString("#,###0.#0");
                        roww["ForeignYearDebit"] = Math.Round(ForeignYearDebit, 2).ToString("#,###0.#0");
                        roww["ForeignYearCredit"] = Math.Round(ForeignYearCredit, 2).ToString("#,###0.#0");
                        roww["ForeignEndDebitAmount"] = Math.Round(ForeignEndDebitAmountSum, 2).ToString("#,###0.#0");
                        roww["ForeignEndCreditAmount"] = Math.Round(ForeignEndCreditAmountSum, 2).ToString("#,###0.#0");


                        roww["ByOrderr"] = "1";
                        dt.Rows.Add(roww);
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

        #region 判断科目表是否为空
        /// <summary>
        /// 判断科目表是否为空
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static bool IsNullSubjects()
        {
            try
            {
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                return VoucherDBHelper.IsNullSubjects(companyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
          #endregion

        #region 新建凭证时若数据源从流水帐中读取数据，插入流水账明细表记录
        /// <summary>
        /// 新建凭证时若数据源从流水帐中读取数据，插入流水账明细表记录
        /// </summary>
        /// <param name="Codes">源单编码</param>
        /// <param name="AttestBillID">凭证主键</param>
        /// <returns></returns>
        public static bool InsertRunningAccoutDetails(string Codes, int AttestBillID)
        {
            try
            {
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                return VoucherDBHelper.InsertRunningAccoutDetails(Codes, AttestBillID, companyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 资产负债表--只统计期末数 Server 资产负债分析表
        /// <summary>
        /// 资产负债表--只统计期末数 Server 资产负债分析表
        /// </summary>
        /// <param name="DuringDate">会计期间</param>
        /// <param name="CompareDate">比较期间</param>
        /// <returns></returns>
        public static DataTable BalanceSheetByServer(string DuringDate, string CompareDate)
        {

            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码

            /*datatable构造 start*/
            DataTable dt = new DataTable();
            dt.Columns.Add("Asset");//资产
            dt.Columns.Add("ALine");//资产行次
            dt.Columns.Add("AEndAmount");//资产期末数
            dt.Columns.Add("CompareEndAmout");
            /*datatable构造 end*/

            if (IsNullSubjects())
            {
                string endDate = string.Empty;
                string endCompareDate = string.Empty;


                //对输入的资料日期进行处理开始
                int Moth = Convert.ToInt32(CompareDate.Split('-')[1].ToString());
                int year = Convert.ToInt32(CompareDate.Split('-')[0].ToString());
                string daycountString = string.Empty;
                if (Moth == 4 || Moth == 6 || Moth == 9 || Moth == 11)
                {
                    daycountString = "-30";
                }
                else if (Moth == 2)
                {
                    if (((year % 4 == 0 && year % 100 == 0)) || (year % 400 == 0))
                    {
                        daycountString = "-29";
                    }
                    else
                    {
                        daycountString = "-28";
                    }
                }
                else
                {
                    daycountString = "-31";
                }

                endCompareDate = CompareDate + daycountString;



                //对输入的资料日期进行处理开始
                int MothString = Convert.ToInt32(DuringDate.Split('-')[1].ToString());
                int yearString = Convert.ToInt32(DuringDate.Split('-')[0].ToString());
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
                endDate = DuringDate + daycount;





                string CurrencyTypeIDStr = string.Empty;
                DataTable CurrencyTypeDt = CurrTypeSettingDBHelper.GetCurrTypeByCompanyCD(CompanyCD);
                for (int p = 0; p < CurrencyTypeDt.Rows.Count; p++)
                {
                    CurrencyTypeIDStr += CurrencyTypeDt.Rows[p]["ID"].ToString() + ",";

                }
                CurrencyTypeIDStr = CurrencyTypeIDStr.TrimEnd(new char[] { ',' });//综合本位币所有币种ID集
               
                if (CurrencyTypeIDStr.Trim().Length > 0)
                {
                    DataTable FormulaDt = BalanceFormulaDBHelper.GetBalanceFormulaInfo("");
                    decimal Ebg = 0;
                    decimal CompareEbg = 0;
                    for (int i = 0; i < FormulaDt.Rows.Count; i++)
                    {
                        if (i == 13 || i == 30 || i == 32 || i == 46 || i == 56 || i == 63)
                        {
                            Ebg = 0;
                            CompareEbg = 0;
                        }
                        string queryDateEnd = " VoucherDate<='" + endDate + "'  and CompanyCD='" + CompanyCD + "' ";
                        string ComparequeryDateEnd = " VoucherDate<='" + endCompareDate + "'  and CompanyCD='" + CompanyCD + "' ";
                        if (i == 12)
                        {
                            DataRow row1 = dt.NewRow();
                            row1["Asset"] = FormulaDt.Rows[12]["Name"].ToString();
                            row1["ALine"] = FormulaDt.Rows[12]["Line"].ToString();
                            row1["AEndAmount"] = Math.Round(Ebg, 2);
                            row1["CompareEndAmout"] = Math.Round(CompareEbg, 2);
                            dt.Rows.Add(row1);
                        }
                        else if (i == 29)
                        {
                            DataRow row2 = dt.NewRow();
                            row2["Asset"] = FormulaDt.Rows[29]["Name"].ToString();
                            row2["ALine"] = FormulaDt.Rows[29]["Line"].ToString();
                            row2["AEndAmount"] = Math.Round(Ebg, 2);
                            row2["CompareEndAmout"] = Math.Round(CompareEbg, 2);
                            dt.Rows.Add(row2);
                        }
                        else if (i == 31)
                        {
                            DataRow row3 = dt.NewRow();
                            row3["Asset"] = FormulaDt.Rows[31]["Name"].ToString();
                            row3["ALine"] = FormulaDt.Rows[31]["Line"].ToString();
                            row3["AEndAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[12]["AEndAmount"].ToString()) + Convert.ToDecimal(dt.Rows[29]["AEndAmount"].ToString()), 2);

                            row3["CompareEndAmout"] = Math.Round(Convert.ToDecimal(dt.Rows[12]["CompareEndAmout"].ToString()) + Convert.ToDecimal(dt.Rows[29]["CompareEndAmout"].ToString()), 2);
                            dt.Rows.Add(row3);
                        }
                        else if (i == 45)
                        {
                            DataRow row4 = dt.NewRow();
                            row4["Asset"] = FormulaDt.Rows[45]["Name"].ToString();
                            row4["ALine"] = FormulaDt.Rows[45]["Line"].ToString();
                            row4["AEndAmount"] = Math.Round(Ebg, 2);
                            row4["CompareEndAmout"] = Math.Round(CompareEbg, 2);
                            dt.Rows.Add(row4);
                        }
                        else if (i == 55)
                        {
                            DataRow row5 = dt.NewRow();
                            row5["Asset"] = FormulaDt.Rows[55]["Name"].ToString();
                            row5["ALine"] = FormulaDt.Rows[55]["Line"].ToString();
                            row5["AEndAmount"] = Math.Round(Ebg, 2);
                            row5["CompareEndAmout"] = Math.Round(CompareEbg, 2);
                            dt.Rows.Add(row5);
                        }
                        else if (i == 62)
                        {
                            DataRow row6 = dt.NewRow();
                            row6["Asset"] = FormulaDt.Rows[62]["Name"].ToString();
                            row6["ALine"] = FormulaDt.Rows[62]["Line"].ToString();
                            row6["AEndAmount"] = Math.Round(Ebg, 2);
                            row6["CompareEndAmout"] = Math.Round(CompareEbg, 2);
                            dt.Rows.Add(row6);
                        }
                        else if (i == 63)
                        {
                            DataRow row7 = dt.NewRow();
                            row7["Asset"] = FormulaDt.Rows[63]["Name"].ToString();
                            row7["ALine"] = FormulaDt.Rows[63]["Line"].ToString();
                            row7["AEndAmount"] = Math.Round(Convert.ToDecimal(dt.Rows[45]["AEndAmount"].ToString()) + Convert.ToDecimal(dt.Rows[55]["AEndAmount"].ToString()) + Convert.ToDecimal(dt.Rows[62]["AEndAmount"].ToString()), 2);

                            row7["CompareEndAmout"] = Math.Round(Convert.ToDecimal(dt.Rows[45]["CompareEndAmout"].ToString()) + Convert.ToDecimal(dt.Rows[55]["CompareEndAmout"].ToString()) + Convert.ToDecimal(dt.Rows[62]["CompareEndAmout"].ToString()), 2);
                            dt.Rows.Add(row7);
                        }
                        else
                        {
                            DataRow row = dt.NewRow();
                            row["Asset"] = FormulaDt.Rows[i]["Name"].ToString();
                            row["ALine"] = FormulaDt.Rows[i]["Line"].ToString() == "0" ? "" : FormulaDt.Rows[i]["Line"].ToString();
                            row["AEndAmount"] = FormulaDt.Rows[i]["Line"].ToString() == "0" ? 0 : Math.Round(GetBalaAmount(FormulaDt.Rows[i]["Line"].ToString(), FormulaDt.Rows[i]["ID"].ToString(), queryDateEnd, 0, CurrencyTypeIDStr), 2);

                            row["CompareEndAmout"] = FormulaDt.Rows[i]["Line"].ToString() == "0" ? 0 : Math.Round(GetBalaAmount(FormulaDt.Rows[i]["Line"].ToString(), FormulaDt.Rows[i]["ID"].ToString(), ComparequeryDateEnd, 0, CurrencyTypeIDStr), 2);


                            Ebg += Convert.ToDecimal(row["AEndAmount"]);
                            CompareEbg += Convert.ToDecimal(row["CompareEndAmout"]);
                            dt.Rows.Add(row);
                        }


                    }
                }
            }
            return dt;

        }
        #endregion

        #region 资产负债分析表数据源
        /// <summary>
        /// 资产负债分析表数据源
        /// </summary>
        /// <param name="AccountDate">会计期间</param>
        /// <param name="AccountDate">比较期间</param>
        /// <returns></returns>
        public static DataTable GetBalanceSheetAnalysis(string AccountDate, string CompareDate)
        {
            try
            {
                /*datatable构造 start*/
                DataTable Sourcedt = new DataTable();
                Sourcedt.Columns.Add("Asset");//资产
                Sourcedt.Columns.Add("ALine");//行次
                Sourcedt.Columns.Add("AEndAmount");//期末数
                Sourcedt.Columns.Add("ACompareEndAmout");//比较期末数
                Sourcedt.Columns.Add("AUpOrDownAmount");//增加或减少金额
                Sourcedt.Columns.Add("APercentage");//增加或减少金额

                Sourcedt.Columns.Add("DAsset");//负债和所有者（或股东）权益
                Sourcedt.Columns.Add("DLine");//行次
                Sourcedt.Columns.Add("DEndAmount");//期末数
                Sourcedt.Columns.Add("DCompareEndAmout");//比较期末数
                Sourcedt.Columns.Add("DUpOrDownAmount");//增加或减少金额
                Sourcedt.Columns.Add("DPercentage");//增加或减少金额

                Sourcedt.Columns.Add("ByOrder");//排序
                /*datatable构造 end*/


                DataTable dt = BalanceSheetByServer(AccountDate, CompareDate);
                dt.Columns.Add("UpOrDownAmount");//增加或减少金额
                dt.Columns.Add("Percentage");//增加或减少百分比
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["ALine"].ToString() != "")
                        {
                            decimal AEndAmount = Convert.ToDecimal(dr["AEndAmount"].ToString() == "" ? "0" : dr["AEndAmount"].ToString());
                            decimal AYearBeginAmount = Convert.ToDecimal(dr["CompareEndAmout"].ToString() == "" ? "0" : dr["CompareEndAmout"].ToString());
                            dr["UpOrDownAmount"] = AEndAmount - AYearBeginAmount;
                            if (Math.Abs(AEndAmount) > 0 && Math.Abs(AYearBeginAmount) == 0)
                            {
                                if (AEndAmount > 0)
                                {
                                    dr["Percentage"] = "100%";
                                }
                                else
                                {
                                    dr["Percentage"] = "-100%";
                                }
                            }
                            else if (Math.Abs(AEndAmount) == 0 && Math.Abs(AYearBeginAmount) == 0)
                            {
                                dr["Percentage"] = "";
                            }
                            else if (Math.Abs(AEndAmount) > 0 && Math.Abs(AYearBeginAmount) > 0)
                            {
                                dr["Percentage"] = Math.Round((AEndAmount / AYearBeginAmount - 1) * 100, 2).ToString() + "%";
                            }
                        }
                        else
                        {
                            dr["UpOrDownAmount"] = "";
                            dr["Percentage"] = "";
                        }
                    }

                    for (int j = 0; j < 32; j++)
                    {
                        DataRow r = Sourcedt.NewRow();
                        r["Asset"] = dt.Rows[j]["Asset"].ToString();
                        r["ALine"] = dt.Rows[j]["ALine"].ToString();
                        r["AEndAmount"] = dt.Rows[j]["AEndAmount"].ToString() == "0" || dt.Rows[j]["AEndAmount"].ToString() == "0.00" ? "" : dt.Rows[j]["AEndAmount"].ToString();
                        r["ACompareEndAmout"] = dt.Rows[j]["CompareEndAmout"].ToString() == "0" || dt.Rows[j]["CompareEndAmout"].ToString() == "0.00" ? "" : dt.Rows[j]["CompareEndAmout"].ToString();

                        r["AUpOrDownAmount"] = dt.Rows[j]["UpOrDownAmount"].ToString() == "0" || dt.Rows[j]["UpOrDownAmount"].ToString() == "0.00" ? "" : dt.Rows[j]["UpOrDownAmount"].ToString();
                        r["APercentage"] = dt.Rows[j]["Percentage"].ToString() == "0" || dt.Rows[j]["Percentage"].ToString() == "0.00" ? "" : dt.Rows[j]["Percentage"].ToString();

                        r["DAsset"] = dt.Rows[j + 32]["Asset"].ToString();
                        r["DLine"] = dt.Rows[j + 32]["ALine"].ToString();
                        r["DEndAmount"] = dt.Rows[j + 32]["AEndAmount"].ToString() == "0" || dt.Rows[j + 32]["AEndAmount"].ToString() == "0.00" ? "" : dt.Rows[j + 32]["AEndAmount"].ToString();
                        r["DCompareEndAmout"] = dt.Rows[j + 32]["CompareEndAmout"].ToString() == "0" || dt.Rows[j + 32]["CompareEndAmout"].ToString() == "0.00" ? "" : dt.Rows[j + 32]["CompareEndAmout"].ToString();
                        r["DUpOrDownAmount"] = dt.Rows[j + 32]["UpOrDownAmount"].ToString() == "0" || dt.Rows[j + 32]["UpOrDownAmount"].ToString() == "0.00" ? "" : dt.Rows[j + 32]["UpOrDownAmount"].ToString();
                        r["DPercentage"] = dt.Rows[j + 32]["Percentage"].ToString() == "0" || dt.Rows[j + 32]["Percentage"].ToString() == "0.00" ? "" : dt.Rows[j + 32]["Percentage"].ToString();

                        r["ByOrder"] = "1";
                        Sourcedt.Rows.Add(r);
                    }
                }
                return Sourcedt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        #endregion

        #region 利润分析表数据源
        /// <summary>
        /// 利润分析表数据源
        /// </summary>
        /// <param name="AccountDate">会计期间</param>
        /// <returns></returns>
        public static DataTable GetProfitProcessAnalysis(string AccountDate)
        {

            try
            {
                DataTable dt = ProfitFormulaBus.GetInstance().GetProfitProcessInfo(AccountDate, "0");
                dt.Columns.Add("UpOrDownAmount");//增加或减少金额
                dt.Columns.Add("Percentage");//增加或减少百分比

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        decimal AEndAmount = Convert.ToDecimal(dr["currMoney"].ToString() == "" ? "0" : dr["currMoney"].ToString());
                        decimal AYearBeginAmount = Convert.ToDecimal(dr["agoMoney"].ToString() == "" ? "0" : dr["agoMoney"].ToString());
                        dr["UpOrDownAmount"] = AEndAmount - AYearBeginAmount;
                        if (Math.Abs(AEndAmount) > 0 && Math.Abs(AYearBeginAmount) == 0)
                        {
                            if (AEndAmount > 0)
                            {
                                dr["Percentage"] = "100%";
                            }
                            else
                            {
                                dr["Percentage"] = "-100%";
                            }
                        }
                        else if (Math.Abs(AEndAmount) == 0 && Math.Abs(AYearBeginAmount) == 0)
                        {
                            dr["Percentage"] = "";
                        }
                        else if (Math.Abs(AEndAmount) > 0 && Math.Abs(AYearBeginAmount) > 0)
                        {
                            dr["Percentage"] = Math.Round((AEndAmount / AYearBeginAmount - 1) * 100, 2).ToString() + "%";
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

        #region 获取辅助核算名称
        /// <summary>
        /// 获取辅助核算名称
        /// </summary>
        /// <param name="SubjectsDetails">辅助核算主键</param>
        /// <param name="FormTBName">辅助核算来源表</param>
        /// <param name="FileName">辅助核算字段名</param>
        /// <returns></returns>
        public static string GetAssistantName(string SubjectsDetails, string FormTBName, string FileName)
        {
            try
            {
                return VoucherDBHelper.GetAssistantName(SubjectsDetails, FormTBName, FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取一级科目名称
        /// <summary>
        /// 获取一级科目名称
        /// </summary>
        /// <param name="SubjectsCD">科目编码</param>
        /// <returns></returns>
        public static string GetFirstSubjectsName(string SubjectsCD)
        {
            try
            {
                string Name = string.Empty;
                string FirstCD = string.Empty;
                string PreSubjectsCDList = GetSubjectsPerCD(SubjectsCD);
                PreSubjectsCDList = PreSubjectsCDList.Replace("'", "");
                string[] CD = PreSubjectsCDList.Split(',');
                if (CD.Length > 0)
                {
                    FirstCD = CD[0].ToString();
                    DataTable dt = AccountSubjectsBus.GetSubjectsInfoByCD(FirstCD);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Name = dt.Rows[0]["SubjectsName"].ToString();
                    }
                }
                return Name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取明细科目名称
        /// <summary>
        /// 获取明细科目名称
        /// </summary>
        /// <param name="SubjectsCD">科目编码</param>
        /// <param name="subjectsDetails">辅助核算主键</param>
        /// <param name="FromTBName">辅助核算来源表</param>
        /// <param name="FileName">辅助核算表字段名</param>
        /// <returns></returns>
        public static string GetDetailsSubjectsName(string SubjectsCD, string subjectsDetails, string FromTBName, string FileName)
        {
            try
            {
                string Name = string.Empty;
                string FirstCD = string.Empty;
                string PreSubjectsCDList = GetSubjectsPerCD(SubjectsCD).Replace("'", "");
                string[] CD = PreSubjectsCDList.Split(',');
                if (CD.Length > 0)
                {
                    FirstCD = CD[0].ToString();
                    if (!FirstCD.Equals(SubjectsCD))
                    {
                        for (int i = 1; i < CD.Length; i++)
                        {
                            DataTable dt = AccountSubjectsBus.GetSubjectsInfoByCD(CD[i].ToString());
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                Name += dt.Rows[0]["SubjectsName"].ToString() + "→";
                            }
                        }
                    }
                }

                if (subjectsDetails.Trim().Length > 0 && FromTBName.Trim().Length > 0 && FileName.Trim().Length > 0)
                {
                    if (Name.Trim().Length > 0)
                    {
                        Name += VoucherDBHelper.GetAssistantName(subjectsDetails, FromTBName, FileName);
                    }
                    else
                    {
                        Name = VoucherDBHelper.GetAssistantName(subjectsDetails,FromTBName,FileName);
                    }
                }

                Name = Name.TrimEnd(new char[] { '→' });
                return Name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 生成资产负债表时判断对应期间的期末处理是否处理过
        /// <summary>
        /// 生成资产负债表时判断对应期间的期末处理是否处理过
        /// </summary>
        /// <param name="PerioNum">会计期间</param>
        /// <returns></returns>
        public static string IsEndBalanceSheet(string PerioNum)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                return VoucherDBHelper.IsEndBalanceSheet(CompanyCD, PerioNum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

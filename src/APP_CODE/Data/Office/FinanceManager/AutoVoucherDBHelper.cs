/**********************************************
 * 类作用：   自动生成凭理数据库层处理
 * 建立人：   莫申林
 * 建立时间： 2010/03/25
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
using System.Collections;
using XBase.Common;

namespace XBase.Data.Office.FinanceManager
{
    public  class AutoVoucherDBHelper
    {

        #region 业务单确认时自动生成凭证--自动登帐--启用自动生成凭证时生成凭证后自动回写源单登记凭证状态
        /// <summary>
        /// 业务单确认时自动生成凭证--自动登帐--启用自动生成凭证时生成凭证后自动回写源单登记凭证状态
        /// </summary>
        /// <param name="TemplateType">模板类别（1.采购订单，2. 销售订单，3. 委托代销单，4. 销售退货单，5. 采购入库，6. 其他出库单，7. 销售出库单，8. 其他入库单，9.收款单，10.付款单）</param>
        /// <param name="CompanyCD">公司编号</param>
        /// <param name="IsVoucher">是否自动生成凭证 0 不生成 1 启用自动生成</param>
        /// <param name="IsApply">凭证是否自动登帐 0 不登帐 1 启用自动登帐</param>
        /// <param name="BillAmount">业务单含税金额合计</param>
        /// <param name="FromTBInfo">来源表信息，格式来源表名+,+来源表主键（业务单主表名称（带上架构）,自动生成凭证的业务单主键）必填</param>
        /// <param name="CurrencyInfo">业务单币种信息，格式为（币种ID,汇率）必填，若业务单无币种汇率，则默认传本位币及汇率</param>
        /// <param name="ProOrCustID">科目辅助核算ID（默认为业务单中的供应商或客户主键）</param>
        /// <param name="returnV">返回提示信息</param>
        /// <returns></returns>
        public static bool AutoVoucherInsert(int TemplateType, string CompanyCD, string IsVoucher, string IsApply, decimal BillAmount, string FromTBInfo, string CurrencyInfo, int ProOrCustID, out string returnV) 
        {
            bool rev = true;
            string returnValue =string.Empty;
            int BillID = 0;//生成凭证的凭证主键
            if (IsVoucher == "1")//自动生成凭证
            {
                /*判断某业务单是否配置生成凭证模板--start*/
                StringBuilder SelectTemplateSQL = new StringBuilder();
                SelectTemplateSQL.AppendLine("select Abstract,TemNo from officedba.VoucherTemplate ");
                SelectTemplateSQL.AppendLine(" where CompanyCD=@CompanyCD and TemType=@TemType and UsedStatus='1' ");
                SqlCommand comm = new SqlCommand();
                comm.CommandText = SelectTemplateSQL.ToString();
                comm.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar).Value = CompanyCD;
                comm.Parameters.AddWithValue("@TemType", SqlDbType.Int).Value = TemplateType;

                DataTable dt = SqlHelper.ExecuteSearch(comm);
                /*判断某业务单是否配置生成凭证模板--End*/
                if (dt != null && dt.Rows.Count > 0)
                {
                    /*获取业务单对应的凭证模板明细--start*/
                    StringBuilder TemplateDetailSQL = new StringBuilder();
                    TemplateDetailSQL.AppendLine(" select SubjectsNo,Direction,Scale  ");
                    TemplateDetailSQL.AppendLine(" from officedba.VoucherTemplateDetail ");
                    TemplateDetailSQL.AppendLine(" where  CompanyCD=@CompanyCD ");
                    TemplateDetailSQL.AppendLine(" and TemNo=@TemNo");

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = TemplateDetailSQL.ToString();
                    cmd.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar).Value = CompanyCD;
                    cmd.Parameters.AddWithValue("@TemNo", SqlDbType.VarChar).Value = dt.Rows[0]["TemNo"].ToString();

                    DataTable tempdt = SqlHelper.ExecuteSearch(cmd);
                    /*获取业务单对应的凭证模板明细--End*/
                    if (tempdt != null && tempdt.Rows.Count > 0)
                    {


                        AttestBillModel Model = new AttestBillModel();//凭证主表实例
                        ArrayList DetailList = new ArrayList();//凭证明细数组
                        Model.Attachment = 1;//附件数
                        Model.AttestName = "记账凭证";//凭证名称
                        Model.VoucherDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));//凭证日期
                        Model.AttestNo = "记-" + VoucherDBHelper.GetMaxAttestNo(CompanyCD, DateTime.Now.ToString("yyyy-MM-dd"));//凭证号
                        Model.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//制单人
                        Model.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));//制单日期
                        Model.FromTbale = FromTBInfo.Split(',')[0].ToString();//来源表名
                        Model.FromValue = FromTBInfo.Split(',')[1].ToString();//来源表主键
                        Model.CompanyCD = CompanyCD;
                        Model.FromName = "";
                        foreach (DataRow row in tempdt.Rows)//根据凭证模板构建凭证明细数组
                        {
                            AttestBillDetailsModel DetailModel = new AttestBillDetailsModel();//凭证明细表实例
                            DetailModel.Abstract = dt.Rows[0]["Abstract"].ToString();//摘要
                            DetailModel.CurrencyTypeID = int.Parse(CurrencyInfo.Split(',')[0].ToString());//币种
                            DetailModel.ExchangeRate = Convert.ToDecimal(CurrencyInfo.Split(',')[1].ToString());//汇率
                            DetailModel.SubjectsCD = row["SubjectsNo"].ToString();//科目编号
                            decimal orgAmount = BillAmount * Convert.ToDecimal(row["Scale"].ToString()) / 100;
                            DetailModel.OriginalAmount = orgAmount;//原币金额

                            if (row["Direction"].ToString() == "0")
                            {
                                DetailModel.DebitAmount = orgAmount * Convert.ToDecimal(CurrencyInfo.Split(',')[1].ToString());//借方金额
                                DetailModel.CreditAmount = 0;//贷方金额
                            }
                            else
                            {
                                DetailModel.DebitAmount = 0;//借方金额
                                DetailModel.CreditAmount = orgAmount * Convert.ToDecimal(CurrencyInfo.Split(',')[1].ToString());//贷方金额
                            }


                            string Auciliary = VoucherDBHelper.GetSubjectsAuciliaryCD(row["SubjectsNo"].ToString(), CompanyCD);
                            DetailModel.SubjectsDetails = "";
                            DetailModel.FormTBName = "";
                            DetailModel.FileName = "";
                            if (Auciliary == "供应商" || Auciliary == "客户")
                            {
                                DetailModel.SubjectsDetails = ProOrCustID.ToString();//辅助核算
                                if (Auciliary == "供应商")
                                {
                                    DetailModel.FormTBName = "officedba.ProviderInfo";
                                    DetailModel.FileName = "CustName";
                                }
                                else
                                {
                                    DetailModel.FormTBName = "officedba.CustInfo";
                                    DetailModel.FileName = "CustName";
                                }
                            }
                            DetailList.Add(DetailModel);
                        }
                        if (VoucherDBHelper.InsertIntoAttestBill(Model, DetailList, out BillID, "0"))//自动生成凭证并根据IsApply判断是否登帐 --生成成功
                        {


                            /*更新原始业务单登记凭证状态 start*/
                            StringBuilder UpdateSourceTB = new StringBuilder();
                            UpdateSourceTB.AppendLine("update {0} set IsAccount=@IsAccount,AccountDate=@AccountDate, ");
                            UpdateSourceTB.AppendLine("Accountor=@Accountor , AttestBillID=@AttestBillID ");
                            UpdateSourceTB.AppendLine(" where ID=@ID ");

                            string UpdateSourceTBSQL = string.Format(UpdateSourceTB.ToString(), FromTBInfo.Split(',')[0].ToString());

                            SqlParameter[] parms = 
                           {
                               new SqlParameter("@IsAccount","1"),
                               new SqlParameter("@AccountDate",DateTime.Now.ToString("yyyy-MM-dd")),
                               new SqlParameter("@Accountor",((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString()),
                               new SqlParameter("@AttestBillID",BillID.ToString()),
                               new SqlParameter("@ID",FromTBInfo.Split(',')[1].ToString())
                           };

                            if (SqlHelper.ExecuteTransSql(UpdateSourceTBSQL, parms) > 0)//更新业务单成功
                            {
                                //
                            }
                            else
                            {
                                returnValue = " 警告：自动生成凭证失败！";
                                rev = false;
                                //删除凭证操作开始
                                VoucherDBHelper.DeleteAttestBillInfo(BillID.ToString());
                                //删除凭证操作结束
                            }
                            /*更新原始业务单登记凭证状态 End*/



                            if (IsApply == "1")//自动生成凭证是否自动登帐 --否
                            {
                                if (!VoucherDBHelper.InsertAccount(BillID))
                                {

                                    returnValue = " 警告：自动生成凭证失败！";
                                    rev = false;
                                    //删除凭证操作开始
                                    VoucherDBHelper.DeleteAttestBillInfo(BillID.ToString());
                                    //删除凭证操作结束


                                    /*更新原始业务单登记凭证状态 start*/
                                    StringBuilder AntiUpdateSourceTB = new StringBuilder();
                                    AntiUpdateSourceTB.AppendLine("update {0} set IsAccount=@IsAccount,AccountDate=@AccountDate, ");
                                    AntiUpdateSourceTB.AppendLine("Accountor=@Accountor , AttestBillID=@AttestBillID ");
                                    AntiUpdateSourceTB.AppendLine(" where ID=@ID ");

                                    string AntiUpdateSourceTBSQL = string.Format(UpdateSourceTB.ToString(), FromTBInfo.Split(',')[0].ToString());

                                    SqlParameter[] Antiparms = 
                                   {
                                       new SqlParameter("@IsAccount","0"),
                                       new SqlParameter("@AccountDate",DateTime.Now.ToString("yyyy-MM-dd")),
                                       new SqlParameter("@Accountor",((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString()),
                                       new SqlParameter("@AttestBillID",BillID.ToString()),
                                       new SqlParameter("@ID",FromTBInfo.Split(',')[1].ToString())
                                   };

                                    SqlHelper.ExecuteTransSql(AntiUpdateSourceTBSQL, Antiparms);//更新业务单成功

                                    /*更新原始业务单登记凭证状态 End*/
                                }
                            }

                        }
                        else
                        {


                            returnValue = " 警告：自动生成凭证失败！";
                            rev = false;
                        }



                    }


                }
                else
                {
                    //未设置模板
                    returnValue = "警告：自动生成凭证失败，请在“财务管理-初始设置”中设置或启用对应的凭证模板！";
                    rev = false;
                }
            }
            returnV= returnValue;
            return rev;
        }
        #endregion


        #region 业务单反确认时--已登记凭证且已登帐的业务单--反登帐-->删除自动生成的凭证-->更新业务单登记凭证状态
        /// <summary>
        /// 业务单反确认时--已登记凭证且已登帐的业务单--反登帐-->删除自动生成的凭证-->更新业务单登记凭证状态
        /// </summary>
        /// <param name="FromTBInfo">业务单表及主键，格式为（表名（带上架构）,主键）</param>
        /// <param name="returnV">returnValue = "0";//未自动生成凭证，不做处理，returnValue = "1";//反登帐成功，returnValue = "2";//反登帐失败，returnValue = "3";//删除对应凭证并更新原始业务单凭证登记状态成功，returnValue = "4";//删除对应凭证并更新原始业务单凭证登记状态失败</param>
        /// <returns></returns>
        public static bool AntiConfirmVoucher(string FromTBInfo,out string returnV)
        {
            bool rev = true;
            string[] TBinfo = FromTBInfo.Split(',');


            string returnValue = "0";//未自动生成凭证


            StringBuilder SelectBill = new StringBuilder();//获取业务单生成的凭证的主键
            SelectBill.AppendLine("Select AttestBillID from {0} ");
            SelectBill.AppendLine(" where ID=@ID ");

            string selectBillSQL = string.Format(SelectBill.ToString(), TBinfo[0].ToString());


            SqlParameter[] parms = 
                           {
                               new SqlParameter("@ID",TBinfo[1].ToString())
                           };

            object obj = SqlHelper.ExecuteScalar(selectBillSQL, parms);

            if (obj != null)
            {
                string objValue = Convert.ToString(obj);

                if (!string.IsNullOrEmpty(objValue))
                {
                    DataTable rowDt = VoucherDBHelper.GetVoucherInfo(Convert.ToInt32(obj));
                    if (rowDt != null && rowDt.Rows.Count > 0)
                    {
                        if (VoucherDBHelper.AntiAccount(objValue))//反登帐成功
                        {
                            returnValue = "1";//反登帐成功
                            if (VoucherDBHelper.DeleteAttestBillInfo(objValue))//删除凭证并更新业务单登记凭证状态
                            {
                                returnValue = "3";//删除对应凭证并更新原始业务单凭证登记状态成功
                            }
                            else
                            {
                                returnValue = "4";//删除对应凭证并更新原始业务单凭证登记状态失败
                                rev = false;
                            }
                        }
                        else
                        {
                            returnValue = "2";//反登帐失败
                            rev = false;
                        }
                    }
                }


            }
            returnV = returnValue;

            return rev;
        }
        #endregion

    }
}

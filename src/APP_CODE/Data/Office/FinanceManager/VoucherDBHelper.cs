/**********************************************
 * 类作用：   凭证管理数据库层处理
 * 建立人：   莫申林
 * 建立时间： 2009/04/09
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
    public class VoucherDBHelper
    {
        #region 获取科目明细信息
        /// <summary>
        /// 获取科目明细信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCompanyInfo()
        {
            string sql = "select CompanyCD,NameCn from company";
            return SqlHelper.ExecuteSql(sql);
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
            StringBuilder sql = new StringBuilder();//插入凭证主表SQL语句
            sql.AppendLine("Insert into officedba.AttestBill ( VoucherDate, ");
            sql.AppendLine("AttestNo,Attachment,AttestName,Creator,");
            sql.AppendLine("CreateDate,status");
            sql.AppendLine(",FromTbale,FromName,");
            sql.AppendLine("FromValue,AccountStatus,Note,CompanyCD ) ");
            sql.AppendLine(" Values ( ");
            sql.AppendLine("@VoucherDate,@AttestNo,@Attachment,@AttestName,@Creator,");
            sql.AppendLine("@CreateDate,@status");
            sql.AppendLine(",@FromTbale,@FromName,");
            sql.AppendLine("@FromValue,@AccountStatus,@Note,@CompanyCD ) ");
            sql.AppendLine("set @IntID= @@IDENTITY");

            SqlParameter[] parms = new SqlParameter[14];
            parms[0] = SqlHelper.GetParameter("@VoucherDate", Model.VoucherDate);
		    parms[1] = SqlHelper.GetParameter("@AttestNo", Model.AttestNo);
		    parms[2] = SqlHelper.GetParameter("@Attachment",Model.Attachment );
		    parms[3] = SqlHelper.GetParameter("@AttestName",Model.AttestName );
		    parms[4] = SqlHelper.GetParameter("@Creator",Model.Creator );
		    parms[5] = SqlHelper.GetParameter("@CreateDate",Model.CreateDate );
		    parms[6] = SqlHelper.GetParameter("@status",Model.status );
		    parms[7] = SqlHelper.GetParameter("@FromTbale",Model.FromTbale );
		    parms[8] = SqlHelper.GetParameter("@FromName", Model.FromName);
		    parms[9] = SqlHelper.GetParameter("@FromValue", Model.FromValue);
		    parms[10] = SqlHelper.GetParameter("@AccountStatus",Model.AccountStatus );
		    parms[11] = SqlHelper.GetParameter("@Note", Model.Note==null?"":Model.Note);
            parms[12] = SqlHelper.GetParameter("@CompanyCD",Model.CompanyCD);
            parms[13] = SqlHelper.GetOutputParameter("@IntID", SqlDbType.Int);

            SqlConnection conn = new SqlConnection(SqlHelper._connectionStringStr);
            conn.Open();
            SqlTransaction mytran = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.Transaction = mytran;
            try
            {
                if (parms != null && parms.Length > 0)
                {
                    foreach (SqlParameter item in parms)
                        cmd.Parameters.Add(item);
                }
                cmd.CommandText = sql.ToString();
                cmd.ExecuteNonQuery();
                ID = Convert.ToInt32(parms[13].Value);
                string KeyID = string.Empty;//主表关联明细表，明细表对应的主表ID
                //string GetInsertIDSql = string.Format(@"select ID from officedba.AttestBill where AttestNo='{0}' and CompanyCD='{1}' and VoucherDate='{2}' ", Model.AttestNo, Model.CompanyCD, Model.VoucherDate);//获取上面插入主表信息的数据库主键ID
                //cmd.CommandText = GetInsertIDSql;
                KeyID =ID.ToString();
                if (KeyID.Trim().Length > 0)
                {
                    for (int i = 0; i < List.Count; i++)
                    {
                        StringBuilder DetailsSql = new StringBuilder();//定义插入凭证明细表SQL语句
                        DetailsSql.AppendLine("Insert into Officedba.AttestBillDetails ( AttestBillID, ");
                        DetailsSql.AppendLine("Abstract,SubjectsCD,SubjectsDetails,DebitAmount,");
                        DetailsSql.AppendLine("CreditAmount,ExchangeRate,FormTBName,FileName,CurrencyTypeID,OriginalAmount )");
                        DetailsSql.AppendLine(" Values ( ");
                        DetailsSql.AppendLine(" @AttestBillID" + i + ",@Abstract" + i + ",@SubjectsCD" + i + ",@SubjectsDetails" + i + ",@DebitAmount" + i + ",");
                        DetailsSql.AppendLine(" @CreditAmount" + i + ",@ExchangeRate" + i + ",@FormTBName" + i + ",@FileName" + i + ",@CurrencyTypeID" + i + ",@OriginalAmount" + i + " )");

                        SqlParameter[] parmss = new SqlParameter[11];
                        parmss[0] = SqlHelper.GetParameter("@AttestBillID" + i + "", KeyID);
                        parmss[1] = SqlHelper.GetParameter("@Abstract" + i + "", (List[i] as AttestBillDetailsModel).Abstract);
                        parmss[2] = SqlHelper.GetParameter("@SubjectsCD" + i + "", (List[i] as AttestBillDetailsModel).SubjectsCD);
                        parmss[3] = SqlHelper.GetParameter("@SubjectsDetails" + i + "", (List[i] as AttestBillDetailsModel).SubjectsDetails);
                        parmss[4] = SqlHelper.GetParameter("@DebitAmount" + i + "", (List[i] as AttestBillDetailsModel).DebitAmount);
                        parmss[5] = SqlHelper.GetParameter("@CreditAmount" + i + "", (List[i] as AttestBillDetailsModel).CreditAmount);
                        parmss[6] = SqlHelper.GetParameter("@ExchangeRate" + i + "", (List[i] as AttestBillDetailsModel).ExchangeRate);
                        parmss[7] = SqlHelper.GetParameter("@FormTBName" + i + "", (List[i] as AttestBillDetailsModel).FormTBName);
                        parmss[8] = SqlHelper.GetParameter("@FileName" + i + "", (List[i] as AttestBillDetailsModel).FileName);
                        parmss[9] = SqlHelper.GetParameter("@CurrencyTypeID" + i + "", (List[i] as AttestBillDetailsModel).CurrencyTypeID);
                        parmss[10] = SqlHelper.GetParameter("@OriginalAmount" + i + "", (List[i] as AttestBillDetailsModel).OriginalAmount);

                        if (parmss != null && parmss.Length > 0)
                        {
                            foreach (SqlParameter item in parmss)
                                cmd.Parameters.Add(item);
                        }
                        cmd.CommandText = DetailsSql.ToString();
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    mytran.Rollback();
                    return false;
                }
                mytran.Commit();//事务提交 

                if (rblAccount == "1")
                {
                    InsertAccount(ID);
                }
                return true;

            }
            catch
            {
                mytran.Rollback();//事务回滚
                ID = 0;
                return false;
            }
            finally
            {
                conn.Close();
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
        public static bool IsAttestNo(string AttestNo, string CompanyCD, string NowDate)
        {
            string s = Convert.ToDateTime(NowDate).ToString("yyyy-MM-dd");
            int dayString = Convert.ToInt32(s.Split('-')[1].ToString());
            int yearString = Convert.ToInt32(s.Split('-')[0].ToString());
            string daycount = string.Empty;
            if (dayString == 4 || dayString == 6 || dayString == 9 || dayString == 11)
            {
                daycount = "-30";
            }
            else
                if (dayString == 2)
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
            string dayStr = dayString.ToString();
            if (Convert.ToString(dayString).Length < 2)
            {
                dayStr = "0" + dayString.ToString();
            }

            string mothbegin = yearString.ToString() + "-" + dayStr.ToString() + "-01";
            string mothend = yearString.ToString() + "-" + dayStr.ToString() + daycount;

            string sql = string.Format(@"select count(ID) from officedba.AttestBill where AttestNo='{0}' and CompanyCD='{1}' and ( VoucherDate between '{2}' and '{3}') ",AttestNo,CompanyCD,mothbegin,mothend);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, null)) > 0 ? true : false;
        }
        #endregion

        #region 根据条件获取凭证主表信息
        /// <summary>
        /// 根据条件获取凭证主表信息
        /// </summary>
        /// <param name="queryStr">查询条件</param>
        /// <returns></returns>
        public static DataTable GetAttestBillInfo(string queryStr)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ID,CONVERT(VARCHAR(10),VoucherDate,21) as VoucherDate, ");
            sql.AppendLine("AttestNo,Attachment,AttestName,Creator,CONVERT(VARCHAR(10),CreateDate,21) as CreateDate, ");
            sql.AppendLine("case when Accountor is null then '' when Accountor is not null then Accountor end as Accountor , ");
            sql.AppendLine("case when AccountDate is not null then CONVERT(VARCHAR(10),AccountDate,21) when AccountDate is null then '' end  as AccountDate , ");
            sql.AppendLine("case when Auditor is null  then '' when Auditor is not null then Auditor end as Auditor, ");
            sql.AppendLine("case when AuditorDate is not null then CONVERT(VARCHAR(10),AuditorDate,21) when  AuditorDate is null  then ''  end as AuditorDate, ");
            sql.AppendLine("case(status) when 0 then '已制表' when 1 then '已审批' when 2 then '作废' when 3 then '已登帐' when 4 then '冲销' end as status, ");
            sql.AppendLine("FromTbale,FromName,FromValue,AccountStatus,Note,CompanyCD ");
            sql.AppendLine("from officedba.AttestBill   {0} ");

            string sql1 = string.Format(sql.ToString(), queryStr.Trim().Length > 0 ? " where " + queryStr : "");
            try
            {
                return SqlHelper.ExecuteSql(sql1);
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
            string sql = "select * from officedba.AttestBill  where ID=@ID ";
            SqlParameter[] parmss = new SqlParameter[1];
            parmss[0] = SqlHelper.GetParameter("@ID", id);

            return SqlHelper.ExecuteSql(sql,parmss);

        }
        #endregion

        #region 根据主表ID获取凭证明细表信息
        /// <summary>
        /// 根据主表ID获取凭证明细表信息
        /// </summary>
        /// <param name="AttestBillID"></param>
        /// <returns></returns>
        public static DataTable GetVoucherDetailsInfo(int AttestBillID)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
            string sql = "select a.ID, a.AttestBillID,a.FormTBName,a.FileName,a.Abstract,a.Abstract as AbstractName,a.SubjectsCD,a.SubjectsDetails,a.DebitAmount,a.CreditAmount,isnull(a.ExchangeRate,1) as ExchangeRate,a.FormTBName,a.FileName,a.OriginalAmount,a.CurrencyTypeID,a.SubjectsDetails as SubjectsDetailsName,d.SubjectsName as SubjectsCDName,isnull(e.CurrencyName,'') as ExchangeRateName  from officedba.AttestBillDetails a left outer join officedba.AccountSubjects d on a.SubjectsCD=d.SubjectsCD left outer join officedba.CurrencyTypeSetting e on a.CurrencyTypeID=e.ID  where a.AttestBillID=@AttestBillID and d.CompanyCD=@CompanyCD and e.CompanyCD=@CompanyCDD order by a.ID asc";
            SqlParameter[] parmss = new SqlParameter[3];
            parmss[0] = SqlHelper.GetParameter("@AttestBillID", AttestBillID);
            parmss[1] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            parmss[2] = SqlHelper.GetParameter("@CompanyCDD", companyCD);

            return SqlHelper.ExecuteSql(sql, parmss);
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

            string s = Convert.ToDateTime(NowDate).ToString("yyyy-MM-dd");
            int dayString = Convert.ToInt32(s.Split('-')[1].ToString());
            int yearString = Convert.ToInt32(s.Split('-')[0].ToString());
            string daycount = string.Empty;
            if (dayString == 4 || dayString == 6 || dayString == 9 || dayString == 11)
            {
                daycount = "-30";
            }
            else
                if (dayString == 2)
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
            string dayStr = string.Empty;
            if (dayString < 10)
            {
                dayStr = "0" + dayString.ToString();
            }
            else
            {
                dayStr = dayString.ToString();
            }
            string mothbegin = yearString.ToString() + "-" + dayStr.ToString() + "-01";
            string mothend = yearString.ToString() + "-" + dayStr.ToString() + daycount;

            string AttestNO = string.Empty;
            string sql = string.Format(@"select count(ID) from officedba.AttestBill where CompanyCD='{0}' and (     VoucherDate between ' {1} ' and ' {2} ' ) ",CompanyCD,mothbegin,mothend);
            //SqlParameter[] parmss = new SqlParameter[3];
            //parmss[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //parmss[1] = SqlHelper.GetParameter("@VoucherDate", mothbegin);
            //parmss[2] = SqlHelper.GetParameter("@VoucherDate1", mothend);
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(sql,null));
            string stringSQL = string.Format(@"select max(AttestNo) from  officedba.AttestBill where CompanyCD='{0}' and ( VoucherDate between '{1}' and '{2}' ) ",CompanyCD,mothbegin,mothend);
            if (count > 0)
            {
                //SqlParameter[] parms = new SqlParameter[3];
                //parms[0] = SqlHelper.GetParameter("@CompanyCD1", CompanyCD);
                //parms[1] = SqlHelper.GetParameter("@VoucherDate2", mothbegin);
                //parms[2] = SqlHelper.GetParameter("@VoucherDate3", mothend);
                string[] NowStr = Convert.ToString(SqlHelper.ExecuteScalar(stringSQL, null)).Split('-');
                string AttestNOO =(Convert.ToInt32(NowStr[1].ToString()) + 1).ToString();
                switch (AttestNOO.Length)
                {
                    case 1: AttestNO = "0" + AttestNOO; 
                        break;
                    default:
                        AttestNO = AttestNOO;
                        break;
                }
                
            }
            else
            {
                AttestNO = "01";
            }
            return AttestNO;
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
            StringBuilder sql = new StringBuilder();//更新凭证主表SQL语句
            sql.AppendLine("Update officedba.AttestBill set VoucherDate=@VoucherDate, ");
            sql.AppendLine("AttestNo=@AttestNo,Attachment=@Attachment,AttestName=@AttestName, ");
            sql.AppendLine("Creator=@Creator ");
            sql.AppendLine(" where ID=@ID ");

            SqlParameter[] parms = new SqlParameter[6];
            parms[0] = SqlHelper.GetParameter("@VoucherDate", Model.VoucherDate);
            parms[1] = SqlHelper.GetParameter("@AttestNo", Model.AttestNo);
            parms[2] = SqlHelper.GetParameter("@Attachment", Model.Attachment);
            parms[3] = SqlHelper.GetParameter("@AttestName", Model.AttestName);
            parms[4] = SqlHelper.GetParameter("@Creator", Model.Creator);
            parms[5] = SqlHelper.GetParameter("@ID", Model.ID);

            SqlConnection conn = new SqlConnection(SqlHelper._connectionStringStr);
            conn.Open();
            SqlTransaction mytran = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.Transaction = mytran;
            try
            {
                if (parms != null && parms.Length > 0)
                {
                    foreach (SqlParameter item in parms)
                        cmd.Parameters.Add(item);
                }
                cmd.CommandText = sql.ToString();
                cmd.ExecuteNonQuery();
                string DeletAttestBillDetailsSql = string.Format(@"delete from officedba.AttestBillDetails where AttestBillID='{0}'", Model.ID);//删除主表对应的明细表信息
                cmd.CommandText = DeletAttestBillDetailsSql;
                cmd.ExecuteNonQuery();
                for (int i = 0; i < List.Count; i++)
                {
                    StringBuilder DetailsSql = new StringBuilder();//定义插入凭证明细表SQL语句
                    DetailsSql.AppendLine("Insert into Officedba.AttestBillDetails ( AttestBillID, ");
                    DetailsSql.AppendLine("Abstract,SubjectsCD,SubjectsDetails,DebitAmount,");
                    DetailsSql.AppendLine("CreditAmount,ExchangeRate,FormTBName,FileName,CurrencyTypeID,OriginalAmount )");
                    DetailsSql.AppendLine(" Values ( ");
                    DetailsSql.AppendLine(" @AttestBillID" + i + ",@Abstract" + i + ",@SubjectsCD" + i + ",@SubjectsDetails" + i + ",@DebitAmount" + i + ",");
                    DetailsSql.AppendLine(" @CreditAmount" + i + ",@ExchangeRate" + i + ",@FormTBName" + i + ",@FileName" + i + ",@CurrencyTypeID" + i + ",@OriginalAmount" + i + " )");

                    SqlParameter[] parmss = new SqlParameter[11];
                    parmss[0] = SqlHelper.GetParameter("@AttestBillID" + i + "", Model.ID);
                    parmss[1] = SqlHelper.GetParameter("@Abstract" + i + "", (List[i] as AttestBillDetailsModel).Abstract);
                    parmss[2] = SqlHelper.GetParameter("@SubjectsCD" + i + "", (List[i] as AttestBillDetailsModel).SubjectsCD);
                    parmss[3] = SqlHelper.GetParameter("@SubjectsDetails" + i + "", (List[i] as AttestBillDetailsModel).SubjectsDetails);
                    parmss[4] = SqlHelper.GetParameter("@DebitAmount" + i + "", (List[i] as AttestBillDetailsModel).DebitAmount);
                    parmss[5] = SqlHelper.GetParameter("@CreditAmount" + i + "", (List[i] as AttestBillDetailsModel).CreditAmount);
                    parmss[6] = SqlHelper.GetParameter("@ExchangeRate" + i + "", (List[i] as AttestBillDetailsModel).ExchangeRate);
                    parmss[7] = SqlHelper.GetParameter("@FormTBName" + i + "", (List[i] as AttestBillDetailsModel).FormTBName);
                    parmss[8] = SqlHelper.GetParameter("@FileName" + i + "", (List[i] as AttestBillDetailsModel).FileName);
                    parmss[9] = SqlHelper.GetParameter("@CurrencyTypeID" + i + "", (List[i] as AttestBillDetailsModel).CurrencyTypeID);
                    parmss[10] = SqlHelper.GetParameter("@OriginalAmount" + i + "", (List[i] as AttestBillDetailsModel).OriginalAmount);

                    if (parmss != null && parmss.Length > 0)
                    {
                        foreach (SqlParameter item in parmss)
                            cmd.Parameters.Add(item);
                    }
                    cmd.CommandText = DetailsSql.ToString();
                    cmd.ExecuteNonQuery();
                }
                mytran.Commit();//事务提交
                return true;

            }
            catch
            {
                mytran.Rollback();//事务回滚
                return false;
            }
            finally
            {
                conn.Close();
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
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
            string detailsSql = string.Format(@"delete from officedba.AttestBillDetails where AttestBillID in ( {0} )", deleteNos);
            string sql = string.Format("delete from officedba.AttestBill where ID in ( {0} )", deleteNos);
            string DeleteRunningBillDetailSQL = string.Format("delete from officedba.RunningBillDetail where AttestID in ( {0} ) and CompanyCD='{1}'",deleteNos,CompanyCD);
            string[] delStr = deleteNos.Split(',');
            SqlConnection conn = new SqlConnection(SqlHelper._connectionStringStr);
            conn.Open();
            SqlTransaction mytran = conn.BeginTransaction();
            try
            {
                string updateSql = @"update {0} set IsAccount='0' where ID in ( {1} ) ";
                for (int i = 0; i < delStr.Length; i++)//循环更新对应生成该凭证的状态
                {
                    string selectSql = string.Format(@"select isnull(FromTbale,'') as FromTbale,isnull(FromValue,'') as FromValue from officedba.AttestBill where ID in ( {0} )", delStr[i].ToString());
                    DataTable dt = SqlHelper.ExecuteSql(selectSql);
                    string FromTbale = dt.Rows[0]["FromTbale"].ToString();
                    string FromValue = dt.Rows[0]["FromValue"].ToString();
                    if (FromTbale.Trim().Length > 0)
                    {
                        if (FromTbale.Trim().Equals("officedba.EndItemProcessedRecord"))//期末处理
                        {
                            string ExecSql = " delete from officedba.EndItemProcessedRecord where id=@RecordID  ";
                            SqlParameter[] parmas = 
                            { 
                                new SqlParameter("@RecordID",FromValue)
                            };
                            SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, ExecSql, parmas);
                        }
                        else if (FromTbale.Trim().LastIndexOf(",") != -1)//固定资产计提折旧凭证删除处理
                        {
                            string[] FromTbaleStr = FromTbale.Split(',');
                            string[] FromValueStr = FromValue.Split(',');
                            if (FromValueStr.Length >= 2&&FromTbaleStr.Length>=3)
                            {
                                /*删除已处理的期末处理项目信息officedba.EndItemProcessedRecord Start */
                                string ExecDeleteRecordSQL = string.Format(@" delete from {0} where ID=@ID", "officedba." + FromTbaleStr[0].ToString());
                                SqlParameter[] parmas = 
                                { 
                                    new SqlParameter("@ID",FromValueStr[0].ToString())
                                };
                                SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, ExecDeleteRecordSQL, parmas);
                                /*删除已处理的期末处理项目信息 End */

                                /*获取固定资产折旧明细officedba.FixPeriodDeprDetails by 公司编码，折旧期数 Start */
                                string FixAssetDeprDetailSQL = string.Format(@"select * from {0} where CompanyCD=@CompanyCD and PeriodNum=@PeriodNum", "officedba." + FromTbaleStr[2].ToString());
                                SqlParameter[] DeprDetailParmas = 
                                { 
                                    new SqlParameter("@CompanyCD",CompanyCD),
                                    new SqlParameter("@PeriodNum",FromValueStr[1].ToString()),
                                };
                                DataTable FixAssetDeprDetaildt = SqlHelper.ExecuteSql(FixAssetDeprDetailSQL, DeprDetailParmas);
                                /*获取固定资产折旧明细 by 公司编码，折旧期数 End */


                                /*根据固定资产折旧明细，更新回滚固定资产计提信息表officedba.FixWithInfo by 公司编码，固定资产编码 Start */
                                foreach (DataRow dr in FixAssetDeprDetaildt.Rows)
                                {
                                    StringBuilder UpdateFixWithInfoSQL = new StringBuilder();
                                    UpdateFixWithInfoSQL.AppendLine(" update  " + "officedba." + FromTbaleStr[1].ToString() + " set  ");
                                    UpdateFixWithInfoSQL.AppendLine(" EndNetValue=@EndNetValue, ");
                                    UpdateFixWithInfoSQL.AppendLine("AmorDeprRate=@AmorDeprRate,AmorDeprM=@AmorDeprM,");
                                    UpdateFixWithInfoSQL.AppendLine("TotalDeprPrice=@TotalDeprPrice ");
                                    UpdateFixWithInfoSQL.AppendLine("where CompanyCD=@CompanyCD and FixNo=@FixNo");
                                    SqlParameter[] FixWithInfoParmas = 
                                    { 
                                        new SqlParameter("@EndNetValue",dr["EndNetValue"].ToString()),
                                        new SqlParameter("@AmorDeprRate",dr["AmorDeprRate"].ToString()),
                                        new SqlParameter("@AmorDeprM",dr["AmorDeprM"].ToString()),
                                        new SqlParameter("@TotalDeprPrice",dr["TotalDeprPrice"].ToString()),
                                        new SqlParameter("@CompanyCD",CompanyCD),
                                        new SqlParameter("@FixNo",dr["FixNo"].ToString()),

                                    };

                                    SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, UpdateFixWithInfoSQL.ToString(), FixWithInfoParmas);

 
                                }
                                /*根据固定资产折旧明细，更新回滚固定资产计提信息表officedba.FixWithInfo by 公司编码，固定资产编码 End */

                                /*删除固定资产折旧明细记录officedba.FixPeriodDeprDetails BY 公司编码，折旧期数 Start*/

                                string DeleteFixPeriodDeprDetailsSQL = string.Format(@" delete from {0} where CompanyCD=@CompanyCDD and PeriodNum=@PeriodNumm", "officedba." + FromTbaleStr[2].ToString());
                                SqlParameter[] DeleteFixPeriodDeprDetailsParmas = 
                                { 
                                    new SqlParameter("@CompanyCDD",CompanyCD),
                                    new SqlParameter("@PeriodNumm",FromValueStr[1].ToString()),
                                };

                                SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, DeleteFixPeriodDeprDetailsSQL, DeleteFixPeriodDeprDetailsParmas);

                                /*删除固定资产折旧明细记录officedba.FixPeriodDeprDetails BY 公司编码，折旧期数 End*/


                                /*
                                 * 删除固定资产折旧明细 Start
                                 */
                                string DeprDate = FromValueStr[1].ToString();
                                string DeprDateYear = DeprDate.Substring(0, 4);
                                string DeprDateMoth = DeprDate.Substring(4, DeprDate.Length - 4);
                                if (int.Parse(DeprDateMoth) <= 9)
                                {
                                    DeprDateMoth = "0" + DeprDateMoth;
                                }
                                string DeprDateStr = DeprDateYear + "-" + DeprDateMoth;

                                string DeprDeleteSQL = "delete from officedba.FixAssetDeprDetail where substring(CONVERT(VARCHAR(10),DeprDate,21),0,8) like '%" + DeprDateStr + "%' and CompanyCD=@DeprCompanyCD";

                                SqlParameter[] DeprParmas = 
                                { 
                                    new SqlParameter("@DeprCompanyCD",CompanyCD)
                                };

                                SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, DeprDeleteSQL, DeprParmas);


                                /*
                                 * 删除固定资产折旧明细 End
                                 */


                            }
                        }
                        else
                        {
                            /*更新关联表登记凭证状态 start*/
                            string ExecSQL = string.Format(updateSql, FromTbale, FromValue);
                            SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, ExecSQL, null);
                            /*更新关联表登记凭证状态 end*/
                        }
                    }
 
                }
                
                SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, DeleteRunningBillDetailSQL, null);//删除流水账明细记录
                /*删除凭证主表及明细表信息 Start*/
                SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, detailsSql, null);
                SqlHelper.ExecuteNonQuery(mytran,CommandType.Text,sql,null);
                /*删除凭证主表及明细表信息 End*/
                mytran.Commit();
                return true;
            }
            catch 
            {
                mytran.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region 更新凭证信息时根据公司CopanyCD和凭证日期判断当月凭证号是否重复
        /// <summary>
        /// 更新凭证信息时根据公司CopanyCD和凭证日期判断当月凭证号是否重复
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="NowDate">当前日期</param>
        /// <param name="AttestNo">凭证号</param>
        /// <returns></returns>
        public static bool IsAttestNo(string AttestNo, string CompanyCD, string VoucherDate,string AttestBillID)
        {
            string s = Convert.ToDateTime(VoucherDate).ToString("yyyy-MM-dd");
            int dayString = Convert.ToInt32(s.Split('-')[1].ToString());
            int yearString = Convert.ToInt32(s.Split('-')[0].ToString());
            string daycount = string.Empty;
            if (dayString == 4 || dayString == 6 || dayString == 9 || dayString == 11)
            {
                daycount = "-30";
            }
            else
                if (dayString == 2)
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
            string dayStr = string.Empty;
            if (Convert.ToString(dayString).Length < 2)
            {
                dayStr = "0" + dayString.ToString();
            }

            string mothbegin = yearString.ToString() + "-" + dayStr.ToString() + "-01";
            string mothend = yearString.ToString() + "-" + dayStr.ToString() + daycount;

            string sql = string.Format(@"select count(ID) from officedba.AttestBill where AttestNo='{0}' and CompanyCD='{1}' and （ VoucherDate between '{2}' and '{3}' )  and ID not in ( {4} )", AttestNo, CompanyCD,mothbegin,mothend, AttestBillID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, null)) > 0 ? true : false;
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
            ArrayList list1 = new ArrayList();
            ArrayList list2 = new ArrayList();
            string[] str = Amount.Split('.');
            if (int.Parse(str[1]) == 0)
            {
                if (int.Parse(str[0]) == 0)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        list2.Add(-1);
                    }
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        list2.Add(-2);
                    }
                }
            }
            else
            {
                string DecimalInfo = str[1].ToString();
                if (DecimalInfo.Length > 2)
                {
                    DecimalInfo = DecimalInfo.Substring(0, 2);
                }

                int t = int.Parse(DecimalInfo) % 10;
                int count2 = int.Parse(DecimalInfo) / 10;
                int t2 = count2 % 10;
                list2.Add(t);
                list2.Add(t2);
            }
            if (int.Parse(str[0]) == 0)
            {
                for (int j = 0; j < 8; j++)
                {
                    list2.Add(-1);
                }
            }
            else
            {
                int IntegerInfo = int.Parse(str[0]);
                int IntegerCount = 0;
                while (IntegerInfo != 0)
                {
                    IntegerCount = IntegerInfo % 10;
                    IntegerInfo = IntegerInfo / 10;
                    list2.Add(IntegerCount);
                }
                if (list2.Count < 10)
                {
                    for (int k = list2.Count; k < 10; k++)
                    {
                        list2.Add(-1);
                    }
                }
            }
            return list2;
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
            string sql = string.Format(@"select SUM(DebitAmount) from Officedba.AttestBillDetails where AttestBillID={0}", VoucherID);
            decimal nov = 0;
            nov = Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, null));
            return nov;
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
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string str3 = "";    //从原num值中取出的值 
            string str4 = "";    //数字的字符串形式 
            string str5 = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int j;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3);      //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
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
        public static bool SetStatus(string ids, string value, string Field,int flag)
        {
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = string.Format(@"update officedba.AttestBill set {0}={1},Auditor={2},AuditorDate='{3}' where ID in ( {4} )", Field, value, ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID, DateTime.Now.ToString("yyyy-MM-dd"), ids);
                   
                    break;
                case 1:
                    sql = string.Format(@"update officedba.AttestBill set {0}={1} where ID in ( {2} )", Field, value, ids);
                   
                    break;
                default:
                    break;
            }
            try
            {
                SqlParameter[] parmss = new SqlParameter[1];
                parmss = null;
                return SqlHelper.ExecuteTransSql(sql, parmss) > 0 ? true : false;
            }
            catch 
            {
                return false;
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
            SqlConnection conn = new SqlConnection(SqlHelper._connectionStringStr);
            conn.Open();
            SqlTransaction mytran = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.Transaction = mytran;
            try
            {
                
                string AccountDate = DateTime.Now.ToString("yyyy-MM-dd");
                string voucherDate = string.Empty;
                string SubjectsCD = string.Empty;
                string SubjectsDetails = string.Empty;
                string Direction = string.Empty;
                string Abstract = string.Empty;

                /**2009.05.13 by moshenlin 新增字段 start **/

                decimal ForeignBeginAmount = 0;//外币期初余额
                decimal ForeignThisDebit = 0;//外币本期借方
                decimal ForeignThisCredit = 0;//外币本期贷方
                decimal ForeignEndAmount = 0;//外币期末余额
                string  CurrencyTypeID = string.Empty;//币种ID
                decimal ExchangeRate = 1;//汇率
                decimal OriginalAmount = 0;//外币金额

                /**2009.05.13 by moshenlin 新增字段 end **/

                decimal BeginAmount = 0;//综合本位币期初余额
                decimal ThisDebit = 0;//综合本位币本期借方
                decimal ThisCredit = 0;//综合本位币本期贷方
                decimal EndAmount = 0;//综合本位币期末余额


                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                string AccoutBookNo = string.Empty;
                string AttestBillNo = string.Empty;
                string FormTBName = string.Empty;
                string FileName = string.Empty;
                int AttestBillDetailsID = 0;


                string CurrencyTypeIDStr = string.Empty;
                DataTable CurrencyTypeDt = CurrTypeSettingDBHelper.GetCurrTypeByCompanyCD(CompanyCD);
                for (int p = 0; p < CurrencyTypeDt.Rows.Count; p++)
                {
                    CurrencyTypeIDStr += CurrencyTypeDt.Rows[p]["ID"].ToString() + ",";

                }
                CurrencyTypeIDStr = CurrencyTypeIDStr.TrimEnd(new char[] {','});//综合本位币所有币种ID集


                string NoSQL = string.Format(@"select AttestNo from Officedba.AttestBill where ID={0}",AttestBillID);
                AttestBillNo =Convert.ToString(SqlHelper.ExecuteScalar(NoSQL,null));
                string AttestBillDetailsSQL = string.Format(@"select a.ID,a.Abstract,a.CurrencyTypeID,a.OriginalAmount,a.SubjectsCD,a.SubjectsDetails,a.DebitAmount,a.CreditAmount,isnull(a.ExchangeRate,1) as ExchangeRate,b.BlanceDire,a.FormTBName,a.FileName,case when a.DebitAmount>0 then OriginalAmount else  a.DebitAmount end as ForeignDebitAmount,case when a.CreditAmount>0 then a.OriginalAmount else a.CreditAmount end as ForeignCreditAmount,c.VoucherDate  from officedba.AttestBillDetails a left outer join officedba.AccountSubjects b on a.SubjectsCD=b.SubjectsCD left outer join Officedba.AttestBill c on a.AttestBillID=c.ID where AttestBillID=@AttestBillID and b.CompanyCD=@CompanyCD and c.CompanyCD=@CompanyCDD");//获取凭证明细表信息|BlanceDire:0借1贷
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = SqlHelper.GetParameter("@AttestBillID", AttestBillID);
                parms[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                parms[2] = SqlHelper.GetParameter("@CompanyCDD", CompanyCD);
               
                DataTable AttestBillDetailsDT = SqlHelper.ExecuteSql(AttestBillDetailsSQL, parms);

                for (int i = 0; i < AttestBillDetailsDT.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        AccoutBookNo = GetAccountBookOrderCode();
                        AttestBillDetailsID = Convert.ToInt32(AttestBillDetailsDT.Rows[i]["ID"].ToString());
                        SubjectsCD = AttestBillDetailsDT.Rows[i]["SubjectsCD"].ToString();
                        SubjectsDetails = AttestBillDetailsDT.Rows[i]["SubjectsDetails"].ToString();
                        Direction = AttestBillDetailsDT.Rows[i]["BlanceDire"].ToString();
                        Abstract = AttestBillDetailsDT.Rows[i]["Abstract"].ToString();
                        FormTBName = AttestBillDetailsDT.Rows[i]["FormTBName"].ToString();
                        FileName = AttestBillDetailsDT.Rows[i]["FileName"].ToString();
                        voucherDate = AttestBillDetailsDT.Rows[i]["VoucherDate"].ToString();
                        CurrencyTypeID= AttestBillDetailsDT.Rows[i]["CurrencyTypeID"].ToString();
                        ExchangeRate =Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["ExchangeRate"].ToString());
                        OriginalAmount =Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["OriginalAmount"].ToString());
                        ForeignBeginAmount = GetBeginAmount(SubjectsCD, AttestBillDetailsDT.Rows[i]["BlanceDire"].ToString(), " a.SubjectsCD='" + AttestBillDetailsDT.Rows[i]["SubjectsCD"].ToString() + "' and a.CurrencyTypeID=" + AttestBillDetailsDT.Rows[i]["CurrencyTypeID"].ToString() + " ", AttestBillDetailsDT.Rows[i]["CurrencyTypeID"].ToString()); 
                        ForeignThisDebit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["ForeignDebitAmount"].ToString());//外币本期借方
                        ForeignThisCredit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["ForeignCreditAmount"].ToString());//外币本期贷方



                        BeginAmount = GetBeginAmount(SubjectsCD, AttestBillDetailsDT.Rows[i]["BlanceDire"].ToString(), " a.SubjectsCD='" + AttestBillDetailsDT.Rows[i]["SubjectsCD"].ToString() + "' and a.CurrencyTypeID in ( " + CurrencyTypeIDStr + " ) ", CurrencyTypeIDStr);
                        ThisDebit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["DebitAmount"].ToString());
                        ThisCredit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["CreditAmount"].ToString());
                        switch (int.Parse(AttestBillDetailsDT.Rows[i]["BlanceDire"].ToString()))
                        {
                            case 0:
                                EndAmount = BeginAmount + ThisDebit - ThisCredit;
                                ForeignEndAmount = ForeignBeginAmount + ForeignThisDebit - ForeignThisCredit;
                                break;
                            case 1:
                                EndAmount = BeginAmount + ThisCredit - ThisDebit;
                                ForeignEndAmount = ForeignBeginAmount + ForeignThisCredit - ForeignThisDebit;
                                break;
                            default:
                                break;
                        }

                    }
                    else
                    {
                        if (AttestBillDetailsDT.Rows[i]["SubjectsCD"].ToString() == SubjectsCD.ToString() && AttestBillDetailsDT.Rows[i]["SubjectsCD"].ToString()==CurrencyTypeID)//科目和币种都一样
                        {
                            string code1 = AccoutBookNo.Substring(0, 8);
                            int code2 = Convert.ToInt32(AccoutBookNo.Remove(0, 8));
                            code2 += i;
                            string code2String = string.Empty;    
                            switch (code2.ToString().Length)
                            {
                                case 1: code2String = "000" + code2.ToString();
                                    break;
                                case 2: code2String = "00" + code2.ToString();
                                    break;
                                case 3: code2String = "0" + code2.ToString();
                                    break;
                            }
                            AccoutBookNo = code1 + code2String;
                            AttestBillDetailsID = Convert.ToInt32(AttestBillDetailsDT.Rows[i]["ID"].ToString());
                            SubjectsCD = AttestBillDetailsDT.Rows[i]["SubjectsCD"].ToString();
                            SubjectsDetails = AttestBillDetailsDT.Rows[i]["SubjectsDetails"].ToString();
                            Direction = AttestBillDetailsDT.Rows[i]["BlanceDire"].ToString();
                            Abstract = AttestBillDetailsDT.Rows[i]["Abstract"].ToString();
                            FormTBName = AttestBillDetailsDT.Rows[i]["FormTBName"].ToString();
                            FileName = AttestBillDetailsDT.Rows[i]["FileName"].ToString();
                            voucherDate = AttestBillDetailsDT.Rows[i]["VoucherDate"].ToString();
                            CurrencyTypeID = AttestBillDetailsDT.Rows[i]["CurrencyTypeID"].ToString();
                            ExchangeRate = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["ExchangeRate"].ToString());
                            OriginalAmount = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["OriginalAmount"].ToString());
                            ForeignBeginAmount = ForeignEndAmount;
                            ForeignThisDebit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["ForeignDebitAmount"].ToString());//外币本期借方
                            ForeignThisCredit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["ForeignCreditAmount"].ToString());//外币本期贷方

                            BeginAmount = EndAmount;
                            ThisDebit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["DebitAmount"].ToString());
                            ThisCredit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["CreditAmount"].ToString());
                            switch (int.Parse(AttestBillDetailsDT.Rows[i]["BlanceDire"].ToString()))
                            {
                                case 0:
                                    EndAmount = BeginAmount + ThisDebit - ThisCredit;
                                    ForeignEndAmount = ForeignBeginAmount + ForeignThisDebit - ForeignThisCredit;
                                    break;
                                case 1:
                                    EndAmount = BeginAmount + ThisCredit - ThisDebit;
                                    ForeignEndAmount = ForeignBeginAmount + ForeignThisCredit - ForeignThisDebit;
                                    break;
                                default:
                                    break;
                            }

                        }
                        else if (AttestBillDetailsDT.Rows[i]["SubjectsCD"].ToString() == SubjectsCD.ToString() && AttestBillDetailsDT.Rows[i]["SubjectsCD"].ToString() != CurrencyTypeID) 
                        {
                            string codee = AccoutBookNo.Substring(0, 8);
                            int coded = Convert.ToInt32(AccoutBookNo.Remove(0, 8));
                            coded += i;
                            string codedString = string.Empty;
                            switch (coded.ToString().Length)
                            {
                                case 1: codedString = "000" + coded.ToString();
                                    break;
                                case 2: codedString = "00" + coded.ToString();
                                    break;
                                case 3: codedString = "0" + coded.ToString();
                                    break;
                            }
                            AccoutBookNo = codee + codedString;
                            AttestBillDetailsID = Convert.ToInt32(AttestBillDetailsDT.Rows[i]["ID"].ToString());
                            SubjectsCD = AttestBillDetailsDT.Rows[i]["SubjectsCD"].ToString();
                            SubjectsDetails = AttestBillDetailsDT.Rows[i]["SubjectsDetails"].ToString();
                            Direction = AttestBillDetailsDT.Rows[i]["BlanceDire"].ToString();
                            Abstract = AttestBillDetailsDT.Rows[i]["Abstract"].ToString();
                            FormTBName = AttestBillDetailsDT.Rows[i]["FormTBName"].ToString();
                            FileName = AttestBillDetailsDT.Rows[i]["FileName"].ToString();
                            voucherDate = AttestBillDetailsDT.Rows[i]["VoucherDate"].ToString();
                            CurrencyTypeID = AttestBillDetailsDT.Rows[i]["CurrencyTypeID"].ToString();
                            ExchangeRate = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["ExchangeRate"].ToString());
                            OriginalAmount = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["OriginalAmount"].ToString());
                            ForeignBeginAmount = GetBeginAmount(SubjectsCD, AttestBillDetailsDT.Rows[i]["BlanceDire"].ToString(), " a.SubjectsCD='" + AttestBillDetailsDT.Rows[i]["SubjectsCD"].ToString() + "' and a.CurrencyTypeID=" + AttestBillDetailsDT.Rows[i]["CurrencyTypeID"].ToString() + " ", AttestBillDetailsDT.Rows[i]["CurrencyTypeID"].ToString());
                            ForeignThisDebit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["ForeignDebitAmount"].ToString());//外币本期借方
                            ForeignThisCredit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["ForeignCreditAmount"].ToString());//外币本期贷方

                            BeginAmount = EndAmount;
                            ThisDebit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["DebitAmount"].ToString());
                            ThisCredit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["CreditAmount"].ToString());
                            switch (int.Parse(AttestBillDetailsDT.Rows[i]["BlanceDire"].ToString()))
                            {
                                case 0:
                                    EndAmount = BeginAmount + ThisDebit - ThisCredit;
                                    ForeignEndAmount = ForeignBeginAmount + ForeignThisDebit - ForeignThisCredit;
                                    break;
                                case 1:
                                    EndAmount = BeginAmount + ThisCredit - ThisDebit;
                                    ForeignEndAmount = ForeignBeginAmount + ForeignThisCredit - ForeignThisDebit;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            string code1 = AccoutBookNo.Substring(0, 8);
                            int code2 = Convert.ToInt32(AccoutBookNo.Remove(0, 8));
                            code2 += i;
                            string code2String = string.Empty;
                            switch (code2.ToString().Length)
                            {
                                case 1: code2String = "000" + code2.ToString();
                                    break;
                                case 2: code2String = "00" + code2.ToString();
                                    break;
                                case 3: code2String = "0" + code2.ToString();
                                    break;
                            }
                            AccoutBookNo = code1 + code2String;
                            AttestBillDetailsID = Convert.ToInt32(AttestBillDetailsDT.Rows[i]["ID"].ToString());
                            SubjectsCD = AttestBillDetailsDT.Rows[i]["SubjectsCD"].ToString();
                            SubjectsDetails = AttestBillDetailsDT.Rows[i]["SubjectsDetails"].ToString();
                            Direction = AttestBillDetailsDT.Rows[i]["BlanceDire"].ToString();
                            Abstract = AttestBillDetailsDT.Rows[i]["Abstract"].ToString();
                            FormTBName = AttestBillDetailsDT.Rows[i]["FormTBName"].ToString();
                            FileName = AttestBillDetailsDT.Rows[i]["FileName"].ToString();
                            voucherDate = AttestBillDetailsDT.Rows[i]["VoucherDate"].ToString();
                            CurrencyTypeID = AttestBillDetailsDT.Rows[i]["CurrencyTypeID"].ToString();
                            ExchangeRate = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["ExchangeRate"].ToString());
                            OriginalAmount = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["OriginalAmount"].ToString());
                            ForeignBeginAmount = GetBeginAmount(SubjectsCD, AttestBillDetailsDT.Rows[i]["BlanceDire"].ToString(), " a.SubjectsCD='" + AttestBillDetailsDT.Rows[i]["SubjectsCD"].ToString() + "' and a.CurrencyTypeID=" + AttestBillDetailsDT.Rows[i]["CurrencyTypeID"].ToString() + " ", AttestBillDetailsDT.Rows[i]["CurrencyTypeID"].ToString());
                            ForeignThisDebit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["ForeignDebitAmount"].ToString());//外币本期借方
                            ForeignThisCredit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["ForeignCreditAmount"].ToString());//外币本期贷方



                            BeginAmount = GetBeginAmount(SubjectsCD, AttestBillDetailsDT.Rows[i]["BlanceDire"].ToString(), " a.SubjectsCD='" + AttestBillDetailsDT.Rows[i]["SubjectsCD"].ToString() + "' and a.CurrencyTypeID in ( " + CurrencyTypeIDStr + " ) ", CurrencyTypeIDStr);
                            ThisDebit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["DebitAmount"].ToString());
                            ThisCredit = Convert.ToDecimal(AttestBillDetailsDT.Rows[i]["CreditAmount"].ToString());
                            switch (int.Parse(AttestBillDetailsDT.Rows[i]["BlanceDire"].ToString()))
                            {
                                case 0:
                                    EndAmount = BeginAmount + ThisDebit - ThisCredit;
                                    ForeignEndAmount = ForeignBeginAmount + ForeignThisDebit - ForeignThisCredit;
                                    break;
                                case 1:
                                    EndAmount = BeginAmount + ThisCredit - ThisDebit;
                                    ForeignEndAmount = ForeignBeginAmount + ForeignThisCredit - ForeignThisDebit;
                                    break;
                                default:
                                    break;
                            }
                            //if (Direction.Trim().Length > 0)
                            //{
                            //    switch (int.Parse(AttestBillDetailsDT.Rows[i]["BlanceDire"].ToString()))
                            //    {
                            //        case 0:
                            //            EndAmount = BeginAmount + ThisDebit - ThisCredit;
                            //            break;
                            //        case 1:
                            //            EndAmount = BeginAmount + ThisCredit - ThisDebit;
                            //            break;
                            //        default:
                            //            break;
                            //    }
                            //}
                            //else
                            //{
                            //    EndAmount =Math.Abs(ThisDebit - ThisCredit);
                            //}

                        }
                    }


                    StringBuilder insertAcountBook = new StringBuilder();//插入账簿表SQL语句
                    insertAcountBook.AppendLine("insert into Officedba.AcountBook (AccoutBookNo, ");
                    insertAcountBook.AppendLine("AccountDate,SubjectsCD,SubjectsDetails,");
                    insertAcountBook.AppendLine("Direction,AttestBillID,BeginAmount,");
                    insertAcountBook.AppendLine("ThisDebit,ThisCredit,EndAmount,CompanyCD,Abstract,AttestBillNo,  ");
                    insertAcountBook.AppendLine("FormTBName,FileName,CurrencyTypeID,ExchangeRate,OriginalAmount,  ");
                    insertAcountBook.AppendLine("ForeignBeginAmount,ForeignThisDebit,ForeignThisCredit,ForeignEndAmount,VoucherDate,AttestBillDetailsID ) ");
                    insertAcountBook.AppendLine(" Values ( ");
                    insertAcountBook.AppendLine("@AccoutBookNo" + i + ",@AccountDate" + i + ",@SubjectsCD" + i + ",");
                    insertAcountBook.AppendLine("@SubjectsDetails" + i + ",@Direction" + i + ",@AttestBillID" + i + ",");
                    insertAcountBook.AppendLine("@BeginAmount" + i + ",@ThisDebit" + i + ",@ThisCredit" + i + ",");
                    insertAcountBook.AppendLine("@EndAmount" + i + ",@CompanyCD" + i + ",@Abstract" + i + ",@AttestBillNo" + i + ",@FormTBName" + i + ",@FileName" + i + ", ");
                    insertAcountBook.AppendLine("@CurrencyTypeID" + i + ",@ExchangeRate" + i + ",@OriginalAmount" + i + ",");
                    insertAcountBook.AppendLine("@ForeignBeginAmount" + i + ",@ForeignThisDebit" + i + ",@ForeignThisCredit" + i + ",@ForeignEndAmount" + i + ",@VoucherDate" + i + ",@AttestBillDetailsID"+i+"  ) ");


                    SqlParameter[] parmss = new SqlParameter[24];
                    parmss[0] = SqlHelper.GetParameter("@AccoutBookNo" + i + "", AccoutBookNo);
                    parmss[1] = SqlHelper.GetParameter("@AccountDate" + i + "", AccountDate);
                    parmss[2] = SqlHelper.GetParameter("@SubjectsCD" + i + "", SubjectsCD);
                    parmss[3] = SqlHelper.GetParameter("@SubjectsDetails" + i + "", SubjectsDetails);
                    parmss[4] = SqlHelper.GetParameter("@Direction" + i + "", Direction);
                    parmss[5] = SqlHelper.GetParameter("@AttestBillID" + i + "", AttestBillID);
                    parmss[6] = SqlHelper.GetParameter("@BeginAmount" + i + "", BeginAmount);
                    parmss[7] = SqlHelper.GetParameter("@ThisDebit" + i + "", ThisDebit);
                    parmss[8] = SqlHelper.GetParameter("@ThisCredit" + i + "", ThisCredit);
                    parmss[9] = SqlHelper.GetParameter("@EndAmount" + i + "", EndAmount);
                    parmss[10] = SqlHelper.GetParameter("@CompanyCD" + i + "", CompanyCD);
                    parmss[11] = SqlHelper.GetParameter("@Abstract" + i + "", Abstract);
                    parmss[12] = SqlHelper.GetParameter("@AttestBillNo" + i + "", AttestBillNo);
                    parmss[13] = SqlHelper.GetParameter("@FormTBName" + i + "", FormTBName);
                    parmss[14] = SqlHelper.GetParameter("@FileName" + i + "", FileName);
                    parmss[15] = SqlHelper.GetParameter("@CurrencyTypeID" + i + "", CurrencyTypeID);
                    parmss[16] = SqlHelper.GetParameter("@ExchangeRate" + i + "", ExchangeRate);
                    parmss[17] = SqlHelper.GetParameter("@OriginalAmount" + i + "", OriginalAmount);
                    parmss[18] = SqlHelper.GetParameter("@ForeignBeginAmount" + i + "", ForeignBeginAmount);
                    parmss[19] = SqlHelper.GetParameter("@ForeignThisDebit" + i + "", ForeignThisDebit);
                    parmss[20] = SqlHelper.GetParameter("@ForeignThisCredit" + i + "", ForeignThisCredit);
                    parmss[21] = SqlHelper.GetParameter("@ForeignEndAmount" + i + "", ForeignEndAmount);
                    parmss[22] = SqlHelper.GetParameter("@VoucherDate" + i + "", voucherDate);
                    parmss[23] = SqlHelper.GetParameter("@AttestBillDetailsID" + i + "", AttestBillDetailsID);

                    SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, insertAcountBook.ToString(), parmss);

                }

                string UpdateStausSQL = "update officedba.AttestBill set status=3,Accountor=@Accountorr,AccountDate=@AccountDatee where ID=@ID";
                SqlParameter[] parmsss = new SqlParameter[3];
                parmsss[0] = SqlHelper.GetParameter("@Accountorr", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
                parmsss[1] = SqlHelper.GetParameter("@AccountDatee", DateTime.Now.ToString("yyyy-MM-dd"));
                parmsss[2] = SqlHelper.GetParameter("@ID", AttestBillID);

                SqlHelper.ExecuteNonQuery(mytran,CommandType.Text,UpdateStausSQL,parmsss);//更新主表状态为已登帐
               
                mytran.Commit();
                return true;
            }
            catch 
            {
                mytran.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
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
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string sRev = string.Empty;
            string sZero = string.Empty;
            int iNum = 0;
            object oData = null;
            string sPrefix = "AB";
            string dDate = string.Format("{0:yy-MM-dd}", DateTime.Now).Replace("-", "");
            string sSuffix = "0001";
            string sql = string.Format("select top 1 AccoutBookNo from Officedba.AcountBook where AccoutBookNo like '{0}{1}%' and CompanyCD='"+CompanyCD+"'  order by ID desc", sPrefix, dDate);
            oData = SqlHelper.ExecuteScalar(sql,null);
            if (oData != null)
            {
                sRev = (string)oData;
                sRev = sRev.Substring(sPrefix.Length + dDate.Length);
                iNum = int.Parse(sRev);
                ++iNum;
                for (int i = 1; i <= sSuffix.Length - iNum.ToString().Length; i++)
                {
                    sZero += "0";
                }
                sZero += iNum.ToString();
                sRev = sRev = sPrefix + dDate + sZero;
            }
            else
            {
                sRev = sPrefix + dDate + sSuffix;
            }
            return sRev;
        }
        #endregion

        #region 获取已登记凭证的期初余额
        /// <summary>
        /// 获取已登记凭证的期初余额
        /// </summary>
        /// <param name="subjectsCD">科目编码</param>
        /// <param name="direction">科目对应方向</param>
        /// <param name="queryStr">包含科目编码，币种类别及日期限制的所有查询条件总和</param>
        /// <param name="cuerryTypeID">币种类别</param>
        /// <returns></returns>
        public static decimal GetBeginAmount(string subjectsCD, string direction, string queryStr, string cuerryTypeID)
        {
            try
            {



                decimal nev = 0;//年初始值
                ////根据币种ID获取年初始值开始
                //string defaultSql = string.Format(@"select isnull(YInitialValue,0) as YInitialValue,ForCurrencyAcc from officedba.AccountSubjects where SubjectsCD='{0}' and ForCurrencyAcc in ( {1} )", subjectsCD, cuerryTypeID);//获取会计科目初始化金额
                //DataTable defaultDt = SqlHelper.ExecuteSql(defaultSql);
                //if (defaultDt.Rows.Count > 0)
                //{
                //    if (cuerryTypeID.LastIndexOf(",") == -1)
                //    {
                //        nev += Convert.ToDecimal(defaultDt.Rows[0]["YInitialValue"].ToString());
                //    }
                //    else
                //    {   //综合本位币的cuerryTypeID为（所有币种ID用，号隔开）
                //        nev += Convert.ToDecimal(defaultDt.Rows[0]["YInitialValue"].ToString()) * (CurrTypeSettingDBHelper.GetCuerryTypeExchangeRate(int.Parse(defaultDt.Rows[0]["ForCurrencyAcc"].ToString())));
                //    }
                //}
                //根据币种ID获取年初始值结束
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
                if (cuerryTypeID.LastIndexOf(",") == -1)
                {
                    if (direction.Trim().Length > 0)
                    {
                        string sqlStr = string.Format(@"select  case when a.DebitAmount>0 then OriginalAmount else  a.DebitAmount end as DebitAmount,case when a.CreditAmount>0 then a.OriginalAmount else a.CreditAmount end as CreditAmount  from officedba.AttestBillDetails a left outer join officedba.AttestBill b on a.AttestBillID=b.ID where b.status=3 and b.CompanyCD='"+companyCD+"' {0}", queryStr.Trim().Length > 0 ? " and " + queryStr : "");
                        DataTable dt = SqlHelper.ExecuteSql(sqlStr);
                        switch (int.Parse(direction))
                        {
                            case 0:
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    nev = nev + (Convert.ToDecimal(dt.Rows[i]["DebitAmount"].ToString()) - Convert.ToDecimal(dt.Rows[i]["CreditAmount"].ToString()));
                                }
                                break;
                            case 1:
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    nev = nev + (Convert.ToDecimal(dt.Rows[i]["CreditAmount"].ToString()) - Convert.ToDecimal(dt.Rows[i]["DebitAmount"].ToString()));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    if (direction.Trim().Length > 0)
                    {
                        string sql = string.Empty;
                        string limit = string.Empty;
                        switch (int.Parse(direction))
                        {
                            case 0:
                                sql = string.Format(@"select ISNULL(SUM(ISNULL(a.DebitAmount,0))-SUM(ISNULL(a.CreditAmount,0)),0) AS beginAmount from officedba.AttestBillDetails a left outer join officedba.AttestBill b on a.AttestBillID=b.ID where b.status=3  and b.CompanyCD='" + companyCD + "' {0}", queryStr.Trim().Length > 0 ? " and " + queryStr : "");
                                nev += Convert.ToDecimal(SqlHelper.ExecuteScalar(sql));
                                break;
                            case 1:
                                sql = string.Format(@"select ISNULL(SUM(ISNULL(a.CreditAmount,0))-SUM(ISNULL(a.DebitAmount,0)),0) AS beginAmount from officedba.AttestBillDetails a left outer join officedba.AttestBill b on a.AttestBillID=b.ID where b.status=3  and b.CompanyCD='" + companyCD + "' {0}", queryStr.Trim().Length > 0 ? " and " + queryStr : "");
                                nev += Convert.ToDecimal(SqlHelper.ExecuteScalar(sql));
                                break;
                            default:
                                break;
                        }
                    }
                }
                return nev;
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

            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码

            string sql = string.Format(@"select ID,SubjectsCD,Direction,CurrencyTypeID,OriginalAmount,ForeignBeginAmount,ForeignThisDebit,ForeignThisCredit,ForeignEndAmount,AttestBillID,BeginAmount,ThisDebit,ThisCredit,EndAmount from officedba.AcountBook where AttestBillID={0}", id);//查询需要反登帐凭证的账簿信息
            SqlConnection conn = new SqlConnection(SqlHelper._connectionStringStr);
            conn.Open();
            try
            {
                DataTable AcountBookDT = SqlHelper.ExecuteSql(sql);
                for (int i = 0; i < AcountBookDT.Rows.Count; i++)
                {
                    string SelectSql = string.Format(@"select ID from officedba.AcountBook where AttestBillID  not in ( {0} ) and SubjectsCD='{1}' and ID>{2} and CompanyCD='{3}' order by ID asc", id, AcountBookDT.Rows[i]["SubjectsCD"].ToString(), AcountBookDT.Rows[i]["ID"].ToString(),companyCD);//查询反登帐对应科目的账簿信息（查询在这科目之后登帐的记录）综合本位币查询条件
                    string ForeignSelectSQL = string.Format(@"select ID from officedba.AcountBook where AttestBillID  not in ( {0} ) and SubjectsCD='{1}' and ID>{2} and CurrencyTypeID={3} and CompanyCD='{4}' order by ID asc", id, AcountBookDT.Rows[i]["SubjectsCD"].ToString(), AcountBookDT.Rows[i]["ID"].ToString(), AcountBookDT.Rows[i]["CurrencyTypeID"].ToString(),companyCD);
                    DataTable SelectDT = SqlHelper.ExecuteSql(SelectSql);
                    DataTable foreignSelectDT = SqlHelper.ExecuteSql(ForeignSelectSQL);
                    string Str = string.Empty;
                    string foreignStr = string.Empty;
                    for (int k = 0; k < foreignSelectDT.Rows.Count; k++)
                    {
                        foreignStr += foreignSelectDT.Rows[k]["ID"].ToString() + ",";
                    }
                    foreignStr = foreignStr.TrimEnd(new char[] { ',' });

                    for (int j = 0; j < SelectDT.Rows.Count; j++)
                    {
                        Str += SelectDT.Rows[j]["ID"].ToString() + ",";
                    }
                    Str = Str.TrimEnd(new char[] {','});

                    if (Str.Trim().Length > 0)
                    {
                        string UpdateAcontBookSQL = string.Empty;
                        switch (int.Parse(AcountBookDT.Rows[i]["Direction"].ToString()))
                        {
                            case 0:
                                UpdateAcontBookSQL = string.Format(@"update officedba.AcountBook set BeginAmount=BeginAmount+({0}),EndAmount=EndAmount+({0}) where ID in ( {1} )", Convert.ToDecimal(AcountBookDT.Rows[i]["ThisCredit"].ToString()) - Convert.ToDecimal(AcountBookDT.Rows[i]["ThisDebit"].ToString()), Str);//综合本位币期初期末金额修改
                                break;
                            case 1:
                                UpdateAcontBookSQL = string.Format(@"update officedba.AcountBook set BeginAmount=BeginAmount+({0}),EndAmount=EndAmount+({0}) where ID in ( {1} )", Convert.ToDecimal(AcountBookDT.Rows[i]["ThisDebit"].ToString()) - Convert.ToDecimal(AcountBookDT.Rows[i]["ThisCredit"].ToString()), Str);//综合本位币期初期末金额修改
                                break;
                            default:
                                break;
                        }
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = UpdateAcontBookSQL;
                        cmd.ExecuteNonQuery();


                    }

                    if(foreignStr.Trim().Length>0)
                    {
                        string UpdateForeignSQL = string.Empty;
                        switch (int.Parse(AcountBookDT.Rows[i]["Direction"].ToString()))
                        {
                            case 0:
                                UpdateForeignSQL = string.Format(@"update officedba.AcountBook set ForeignBeginAmount=ForeignBeginAmount+({0}),ForeignEndAmount=ForeignEndAmount+({0}) where ID in ( {1} ) and CurrencyTypeID={2}", Convert.ToDecimal(AcountBookDT.Rows[i]["ForeignThisCredit"].ToString()) - Convert.ToDecimal(AcountBookDT.Rows[i]["ForeignThisDebit"].ToString()), foreignStr, AcountBookDT.Rows[i]["CurrencyTypeID"].ToString());//外币期初期末金额修改
                                break;
                            case 1:
                                UpdateForeignSQL = string.Format(@"update officedba.AcountBook set ForeignBeginAmount=ForeignBeginAmount+({0}),ForeignEndAmount=ForeignEndAmount+({0}) where ID in ( {1} ) and CurrencyTypeID={2}", Convert.ToDecimal(AcountBookDT.Rows[i]["ForeignThisDebit"].ToString()) - Convert.ToDecimal(AcountBookDT.Rows[i]["ForeignThisCredit"].ToString()), foreignStr, AcountBookDT.Rows[i]["CurrencyTypeID"].ToString());//外币期初期末金额修改
                                break;
                            default:
                                break;
                        }
                        SqlCommand cmdd = new SqlCommand();
                        cmdd.Connection = conn;
                        cmdd.CommandText = UpdateForeignSQL;
                        cmdd.ExecuteNonQuery();


                    }

                }
                string updateAttestBillStatusSQL = string.Format(@"update officedba.AttestBill set status=1 where ID={0}",id);
                string deleteAcountBookSQL = string.Format(@"delete officedba.AcountBook where AttestBillID={0} and CompanyCD='{1}'", id,companyCD);
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = conn;
                cmd1.CommandText = deleteAcountBookSQL;
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = updateAttestBillStatusSQL;
                cmd2.ExecuteNonQuery();
                //SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection("officedba"),deleteAcountBookSQL);
                //SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection("officedba"),updateAttestBillStatusSQL);
                //mytran.Commit();
                return true;
            }
            catch 
            {
                //mytran.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
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
            string nev = string.Empty;
            string sql = string.Format(@"select AttestNo,status,FromTbale,FromValue from Officedba.AttestBill where ID in ( {0} )", ids);
            DataTable dt = SqlHelper.ExecuteSql(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (int.Parse(dt.Rows[i]["status"].ToString()))
                {
                    case 1:
                        nev += "'" + dt.Rows[i]["AttestNo"].ToString() + "'  已审批|";
                        break;
                    case 3:
                        nev += "'" + dt.Rows[i]["AttestNo"].ToString() + "'  已登帐|";
                        break;
                    case 4:
                        nev += "'" + dt.Rows[i]["AttestNo"].ToString() + "'  已冲销|";
                        break;
                }
                /*  固定资产计提折旧凭证，只能删除当期的折旧凭证 Start */
                string TableName = dt.Rows[i]["FromTbale"].ToString();
                string FromValue = dt.Rows[i]["FromValue"].ToString();
                string time=DateTime.Now.ToString("yyyy-MM-dd");
                int MothNmu = Convert.ToInt32(time.Split('-')[1].ToString());
                string NowNum = time.Split('-')[0].ToString() + MothNmu.ToString();
                if (TableName.Trim().LastIndexOf(',') != -1)
                {
                    string[] FormValueStr = FromValue.Split(',');
                    if (FormValueStr.Length >= 2)
                    {
                        if (!FormValueStr[1].ToString().Equals(NowNum))
                        {
                            nev += "'" + dt.Rows[i]["AttestNo"].ToString() + "'  为固定资产计提折旧凭证，只能删除当期的折旧凭证|";
                        }
                    }
                }

                /*  固定资产计提折旧凭证，只能删除当期的折旧凭证 End */



            }
            return nev.TrimEnd(new char[] {'|'});
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
            string sql = string.Format("select EmployeeName from Officedba.EmployeeInfo where ID={0}",EmplyeeID);
            return Convert.ToString(SqlHelper.ExecuteScalar(sql,null));
        }
        #endregion

        #region 判断凭证列表是否能审核或反审核
        /// <summary>
        /// 判断凭证列表是否能审核或反审核
        /// </summary>
        /// <param name="ids">凭证ID集</param>
        /// <param name="flag">flag=0：审核1：反审核</param>
        /// <returns></returns>
        public static string IsCanAuditOrAntiAuditor(string ids,int flag)
        {
            string nev = string.Empty;
            string sql = string.Format(@"select AttestNo,status from Officedba.AttestBill where ID in ( {0} )", ids);
            DataTable dt = SqlHelper.ExecuteSql(sql);
            switch (flag)
            {
                case 0:
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        switch (int.Parse(dt.Rows[i]["status"].ToString()))
                        {
                            case 1:
                                nev += "'" + dt.Rows[i]["AttestNo"].ToString() + "'  已审批|";
                                break;
                            case 3:
                                nev += "'" + dt.Rows[i]["AttestNo"].ToString() + "'  已登帐|";
                                break;
                            case 4:
                                nev += "'" + dt.Rows[i]["AttestNo"].ToString() + "'  已冲销|";
                                break;
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        switch (int.Parse(dt.Rows[i]["status"].ToString()))
                        {
                            case 0:
                                nev += "'" + dt.Rows[i]["AttestNo"].ToString() + "'  未审批|";
                                break;
                            case 3:
                                nev += "'" + dt.Rows[i]["AttestNo"].ToString() + "'  已登帐|";
                                break;
                            case 4:
                                nev += "'" + dt.Rows[i]["AttestNo"].ToString() + "'  已冲销|";
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }

            return nev.TrimEnd(new char[] { '|' });
        }
        #endregion

        #region 根据科目编码获取其所有下级科目
        /// <summary>
        /// 根据科目编码获取其所有下级科目
        /// </summary>
        /// <param name="SubjectsCD">科目编码</param>
        /// <returns>string</returns>
        public static string GetSubjectsNextCD(string SubjectsCD)
        {
            string nev = string.Empty;
            string companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string sql = string.Format(" exec officedba.GetSubjectsNextCD '{0}','{1}' ",SubjectsCD,companycd);
            DataTable dt = SqlHelper.ExecuteSql(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow rows in dt.Rows)
                {
                    nev += "'"+rows["SubjectsCD"].ToString() + "',";
                }
            }
            return nev.TrimEnd(new char[] { ',' });
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
            string nev = string.Empty;
            string companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string sql = string.Format(" exec officedba.GetSubjectsPerCD '{0}','{1}' ", SubjectsCD,companycd);
            DataTable dt = SqlHelper.ExecuteSql(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                nev += "'"+dt.Rows[i]["SubjectsCD"].ToString() + "',";
            }
            return nev.TrimEnd(new char[] { ',' });
        }
        #endregion

        #region 获取会计科目名称
        /// <summary>
        /// 获取会计科目名称
        /// </summary>
        /// <param name="SubjectsCD">科目编码</param>
        /// <returns></returns>
        public static string GetSubjectsName(string SubjectsCD,string CompanyCD)
        {
            string nev = string.Empty;
            string ids = GetSubjectsPerCD(SubjectsCD);
            if (ids.Trim().Length > 0)
            {
                string sql =string.Format(@"select SubjectsCD,SubjectsName from officedba.AccountSubjects where SubjectsCD in ( {0} ) and CompanyCD='{1}' order by SubjectsCD asc",ids,CompanyCD);
                DataTable dt = SqlHelper.ExecuteSql(sql);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    nev += dt.Rows[i]["SubjectsName"].ToString() + "→";
                }
            }

            return nev.TrimEnd(new char[] { '→' });
        }
        #endregion

        #region 获取科目对应的辅助科目
        /// <summary>
        /// 获取科目对应的辅助科目
        /// </summary>
        /// <param name="SubjectsCD"></param>
        /// <returns></returns>
        public static string GetSubjectsAuciliaryCD(string SubjectsCD, string CompanyCD)
        {
            string sql = "select b.Name from officedba.AccountSubjects a left outer join officedba.AssistantType b  on a.AuciliaryCD=b.ID  where SubjectsCD='" + SubjectsCD + "' and a.CompanyCD='" + CompanyCD + "' and b.CompanyCD='" + CompanyCD + "' ";
            return Convert.ToString(SqlHelper.ExecuteScalar(sql,null));
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
            DataTable sourceDt = new DataTable();
            sourceDt.Columns.Add("ID");
            sourceDt.Columns.Add("VoucherDate");
            sourceDt.Columns.Add("AttestNo");
            sourceDt.Columns.Add("Creator");
            sourceDt.Columns.Add("CreateDate");
            sourceDt.Columns.Add("Auditor");
            sourceDt.Columns.Add("AuditorDate");
            sourceDt.Columns.Add("status");
            sourceDt.Columns.Add("Accountor");
            sourceDt.Columns.Add("AccountDate");
            sourceDt.Columns.Add("Abstract");
            sourceDt.Columns.Add("SubjectsCD");
            sourceDt.Columns.Add("DebitAmount");
            sourceDt.Columns.Add("CreditAmount");
            sourceDt.Columns.Add("byorder");

            string SelectSql = string.Format(" select distinct a.ID from officedba.AttestBill a left outer join officedba.AttestBillDetails b on a.ID=b.AttestBillID {0}  ",queryStr.Trim().Length>0?" where  "+queryStr:"");
            DataTable IDdt = SqlHelper.ExecuteSql(SelectSql);
            string Str = string.Empty;
            foreach (DataRow dr in IDdt.Rows)
            {
                Str += dr["ID"].ToString() + ",";
            }
            Str = Str.TrimEnd(new char[] { ',' });
            if (Str.Trim().Length > 0)
            {
                DataTable dt = GetAttestBillInfo(" ID in ( " + Str + " ) ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable detailsDt = GetVoucherDetailsInfo(int.Parse(dt.Rows[i]["ID"].ToString()));
                    DataRow row = sourceDt.NewRow();
                    row["ID"] = dt.Rows[i]["ID"].ToString();
                    row["VoucherDate"] = dt.Rows[i]["VoucherDate"].ToString();
                    row["AttestNo"] = dt.Rows[i]["AttestNo"].ToString();
                    row["Creator"] = dt.Rows[i]["Creator"].ToString();
                    row["CreateDate"] = dt.Rows[i]["CreateDate"].ToString();
                    row["Auditor"] = dt.Rows[i]["Auditor"].ToString();
                    row["AuditorDate"] = dt.Rows[i]["AuditorDate"].ToString();
                    row["status"] = dt.Rows[i]["status"].ToString();
                    row["Accountor"] = dt.Rows[i]["Accountor"].ToString();
                    row["AccountDate"] = dt.Rows[i]["AccountDate"].ToString();

                    row["Abstract"] = "";
                    row["SubjectsCD"] = "";
                    row["DebitAmount"] = "";
                    row["CreditAmount"] = "";
                    if (detailsDt.Rows.Count > 0)
                    {
                        row["Abstract"] = detailsDt.Rows[0]["AbstractName"].ToString();
                        row["SubjectsCD"] = GetSubjectsNamesInfo(detailsDt.Rows[0]["ID"].ToString());
                        row["DebitAmount"] = Convert.ToDecimal(detailsDt.Rows[0]["DebitAmount"].ToString()).ToString("#,###0.#0");
                        row["CreditAmount"] = Convert.ToDecimal(detailsDt.Rows[0]["CreditAmount"].ToString()).ToString("#,###0.#0");
                    }
                    row["byorder"] = "1";

                    sourceDt.Rows.Add(row);
                    if (detailsDt.Rows.Count > 1)
                    {
                        for (int j = 1; j < detailsDt.Rows.Count; j++)
                        {
                            DataRow roww = sourceDt.NewRow();
                            roww["ID"] = dt.Rows[i]["ID"].ToString()+"^0";
                            roww["VoucherDate"] = "";
                            roww["AttestNo"] = "";
                            roww["Creator"] = "";
                            roww["CreateDate"] = "";
                            roww["Auditor"] = "";
                            roww["AuditorDate"] = "";
                            roww["status"] = "";
                            roww["Accountor"] = "";
                            roww["AccountDate"] = "";

                            roww["Abstract"] = detailsDt.Rows[j]["AbstractName"].ToString();
                            roww["SubjectsCD"] = GetSubjectsNamesInfo(detailsDt.Rows[j]["ID"].ToString());
                            roww["DebitAmount"] = Convert.ToDecimal(detailsDt.Rows[j]["DebitAmount"].ToString()).ToString("#,###0.#0");
                            roww["CreditAmount"] = Convert.ToDecimal(detailsDt.Rows[j]["CreditAmount"].ToString()).ToString("#,###0.#0");
                            roww["byorder"] = "1";
                            sourceDt.Rows.Add(roww);

                        }
                    }

                }
            }

            return sourceDt;
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
            string companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string detailsSql = string.Format(@"select SubjectsCD,SubjectsDetails,FormTBName,FileName from Officedba.AttestBillDetails where ID={0}",AttestBillDetailsID);
            DataTable dt = SqlHelper.ExecuteSql(detailsSql);
            string name = GetSubjectsName(dt.Rows[0]["SubjectsCD"].ToString(),companycd);
            string sql = string.Empty;
            if (dt.Rows[0]["SubjectsDetails"].ToString().Trim().Length > 0)
            {
                if (dt.Rows[0]["FormTBName"].ToString() == "company")
                {
                    sql = string.Format(@"select {0} from {1} where CompanyCD='{2}'", dt.Rows[0]["FileName"].ToString(), dt.Rows[0]["FormTBName"].ToString(), dt.Rows[0]["SubjectsDetails"].ToString());
                    name += "→" + Convert.ToString(SqlHelper.ExecuteScalar(sql, null));
                }
                else
                {
                    sql = string.Format(@"select {0} from {1} where id={2}", dt.Rows[0]["FileName"].ToString(), dt.Rows[0]["FormTBName"].ToString(), dt.Rows[0]["SubjectsDetails"].ToString());
                    name += "→" + Convert.ToString(SqlHelper.ExecuteScalar(sql, null));
                }
            }
            return name;
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
        public static string GetSubJectsName(string SubjectsCD, string SubjectsDetails,string formTBName,string FileName,string CompanyCD)
        {
            string nev = string.Empty;
            string sql = string.Empty;
            if (SubjectsCD.Trim().Length > 0)
            {
                nev = GetSubjectsName(SubjectsCD,CompanyCD);
            }
            if (SubjectsDetails.Trim().Length > 0)
            {
                if (formTBName == "company")
                {
                    sql = string.Format(@"select {0} from {1} where CompanyCD='{2}'", FileName, formTBName, SubjectsDetails);
                    nev += "→" + Convert.ToString(SqlHelper.ExecuteScalar(sql, null));
                }
                else
                {
                    sql = string.Format(@"select {0} from {1} where id={2}", FileName, formTBName, SubjectsDetails);
                    nev += "→" + Convert.ToString(SqlHelper.ExecuteScalar(sql, null));
                }
            }
            return nev;
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
            string sql = string.Format("select ID,companycd,OutNo,OutDate,CountTotal,TotalPrice,DeptID,type,fromTBName from officedba.V_StorangeOut {0}",queryStr.Trim().Length>0?" where "+queryStr:"");
            return SqlHelper.ExecuteSql(sql);

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
            string sql = string.Format("select * from officedba.V_Cashier {0}", queryStr.Trim().Length > 0 ? " where " + queryStr : "");
            return SqlHelper.ExecuteSql(sql);

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
            string sql = string.Format("update {0} set IsAccount='1' where ID in ( {1} )",fromTb,nos);
            SqlParameter[] parms = new SqlParameter[0];
            return SqlHelper.ExecuteTransSql(sql, parms) > 0 ? true : false;
        }
        #endregion

        #region 根据科目名称，绑定所在科目的所有下级科目
        /// <summary>
        /// 根据科目名称，绑定所在科目的所有下级科目
        /// </summary>
        /// <param name="SubjectsName">科目名称</param>
        /// <returns></returns>
        public static DataTable GetSubjectsInfo(string SubjectsName,string CompanyCD)
        {

            string sql = "select SubjectsCD,SubjectsName from officedba.AccountSubjects where SubjectsName=@SubjectsName and CompanyCD=@CompanyCD";
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = SqlHelper.GetParameter("@SubjectsName", SubjectsName);
            parms[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            DataTable dt = SqlHelper.ExecuteSql(sql, parms);
            DataTable retDt = new DataTable();
            if (dt.Rows.Count == 1)
            {
                //string nextCD = GetSubjectsNextCD(dt.Rows[0]["SubjectsCD"].ToString());
                //string[] str = nextCD.Split(',');
                //int i = nextCD.IndexOf(',')+1;
                //if (str.Length > 1)
                //{
                //    nextCD = nextCD.Remove(0, i);
                //}
                string rsql = string.Format(@"select SubjectsCD,SubjectsName from officedba.AccountSubjects where ParentID='{0}' and CompanyCD='{1}' order by SubjectsCD asc ", dt.Rows[0]["SubjectsCD"].ToString(), CompanyCD);
                retDt = SqlHelper.ExecuteSql(rsql);


            }

            return retDt;

        }
        #endregion

        #region 根据科目名称，绑定所在科目的所有下级科目
        /// <summary>
        /// 根据科目名称，绑定所在科目的所有下级科目
        /// </summary>
        /// <param name="SubjectsName">科目名称</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetSubjectsCDInfo(string SubjectsName, string CompanyCD)
        {
            string sql = "select SubjectsCD,SubjectsName from officedba.AccountSubjects where SubjectsName=@SubjectsName and CompanyCD=@CompanyCD";
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = SqlHelper.GetParameter("@SubjectsName", SubjectsName);
            parms[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            DataTable dt = SqlHelper.ExecuteSql(sql, parms);
            DataTable retDt = new DataTable();
            if (dt.Rows.Count == 1)
            {
                string nextCD = GetSubjectsNextCD(dt.Rows[0]["SubjectsCD"].ToString());
                string[] str = nextCD.Split(',');
                int i = nextCD.IndexOf(',') + 1;
                if (str.Length > 1)
                {
                    nextCD = nextCD.Remove(0, i);
                }
                string rsql = string.Format(@"select SubjectsCD,SubjectsName from officedba.AccountSubjects where SubjectsCD in ( {0} ) and CompanyCD='{1}' order by SubjectsCD asc ", nextCD, CompanyCD);
                retDt = SqlHelper.ExecuteSql(rsql);


            }

            return retDt;
        }
        #endregion
        #region 获取对方科目名称
        /// <summary>
        /// 获取对方科目名称
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetAntiSubjectsName(DataTable dt)
        {
            string nev = string.Empty;
            string companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                nev += GetSubJectsName(dt.Rows[i]["SubjectsCD"].ToString(), dt.Rows[i]["SubjectsDetails"].ToString(), dt.Rows[i]["FormTBName"].ToString(), dt.Rows[i]["FileName"].ToString(),companycd)+",";
            }
            return nev.TrimEnd(new char[] {','});
        }
        #endregion

        #region 获取应收/应付汇总表数据源 本币或综合本位币数据源
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
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
            DataTable dt = new DataTable();
            dt.Columns.Add("Date");//日期
            dt.Columns.Add("AttestNo");//凭证编号
            dt.Columns.Add("Abstract");//摘要
            dt.Columns.Add("CusOrPro");//辅助核算名称
            dt.Columns.Add("AntiSubjectsCD");//对方科目
            dt.Columns.Add("DebitAmount");//借方金额
            dt.Columns.Add("CreditAmount");//贷方金额
            dt.Columns.Add("Direction");//方向
            dt.Columns.Add("EndAmount");//余额
            dt.Columns.Add("ByOrderr");//排序
            dt.Columns.Add("AttestBillID");//凭证主表ID

            //string CusOrPro = GetAssistantName(SubjectsDetails, FormTBName, FileName);

            if (IsNullSubjects(companyCD)&&CurryTypeID.Trim().Length>0)
            {
                string sql = @"select BlanceDire from officedba.AccountSubjects where SubjectsCD=@SubjectsCD and CompanyCD=@CompanyCD";
                SqlParameter[] parms = new SqlParameter[2];
                parms[0] = SqlHelper.GetParameter("@SubjectsCD", subjectsCD);
                parms[1] = SqlHelper.GetParameter("@CompanyCD", companyCD);
                object obj = SqlHelper.ExecuteScalar(sql, parms);
                int Dirdt = 0;
                if (obj != null)
                {
                    Dirdt = Convert.ToInt32(obj);
                }

                string subjectCDList = GetSubjectsNextCD(subjectsCD);//获取该科目对应的所有下级科目ID集
                /*期初设置START*/
                string queryStr = string.Empty;
                if (SubjectsDetails.Trim().Length <= 0)
                {
                    queryStr = "  VoucherDate<'" + startDate + "' and SubjectsCD in ( " + subjectCDList + " ) and CompanyCD='" + companyCD + "' ";//获取上日余额的查询条件
                }
                else
                {
                    queryStr = "  VoucherDate<'" + startDate + "' and SubjectsCD in ( " + subjectCDList + " )  and SubjectsDetails='" + SubjectsDetails + "' and FormTBName='" + FormTBName + "' and FileName='" + FileName + "' and CompanyCD='" + companyCD + "' ";//获取上日余额的查询条件
                }

                if (CurryTypeID.LastIndexOf(",") == -1&&CurryTypeID.Trim().Length>0)
                {
                    queryStr += " and CurrencyTypeID=" + CurryTypeID + " ";
                }
                else
                {
                    //综合本位币查询条件不做处理
                }

                decimal beginAmountt = AcountBookDBHelper.GetAccountBookBeginAmount(queryStr, subjectsCD, CurryTypeID); //GetBeginAmount(queryStr, Dirdt.ToString());//上日余额

                decimal DefaultAmount = 0;
                if (SubjectsDetails.Trim().Length <= 0)
                {
                    DefaultAmount = AcountBookDBHelper.GetBeginCurryTypeAmount(subjectsCD, CurryTypeID, companyCD);//年初始化金额
                }
                else
                {
                    DefaultAmount = AcountBookDBHelper.GetSubjectsBeginDetailAmount(subjectsCD, CurryTypeID, companyCD, SubjectsDetails, FormTBName, FileName);
                }
                decimal beginAmount = beginAmountt + DefaultAmount;

                string Dir = AcountBookDBHelper.DirectionSource(subjectsCD, beginAmount);//余额方向
                DataRow row = dt.NewRow();
                row["Date"] = "";
                row["AttestNo"] = "";
                row["Abstract"] = "期初余额";
                row["AntiSubjectsCD"] = "";
                row["CusOrPro"] = "";
                row["DebitAmount"] = "";
                row["CreditAmount"] = "";
                row["Direction"] = Dir;
                row["EndAmount"] = Math.Round(beginAmount, 2).ToString("#,###0.#0");
                row["ByOrderr"] = "1";
                row["AttestBillID"] = "";
                dt.Rows.Add(row);
                /*期初设置END*/

                /*本日应收/应付明细 Start*/
                string thisQueryStr = string.Empty;
                string SelectDistinctDateSQL = string.Empty;
                string YearQueryStr = string.Empty;
                if (SubjectsDetails.Trim().Length <= 0)
                {
                    thisQueryStr = " VoucherDate>='" + startDate + "' and VoucherDate<='" + endDate + "'  and SubjectsCD in ( " + subjectCDList + " ) and CompanyCD='" + companyCD + "' ";

                    YearQueryStr = " VoucherDate<='" + endDate + "'  and SubjectsCD in ( " + subjectCDList + " ) and CompanyCD='" + companyCD + "' ";

                }
                else
                {
                    thisQueryStr = " VoucherDate>='" + startDate + "' and VoucherDate<='" + endDate + "'  and SubjectsCD in ( " + subjectCDList + " )and SubjectsDetails='" + SubjectsDetails + "' and FormTBName='" + FormTBName + "' and FileName='" + FileName + "' and CompanyCD='" + companyCD + "'  ";

                    YearQueryStr = " VoucherDate<='" + endDate + "'  and SubjectsCD in ( " + subjectCDList + " )and SubjectsDetails='" + SubjectsDetails + "' and FormTBName='" + FormTBName + "' and FileName='" + FileName + "' and CompanyCD='" + companyCD + "'  ";
                }

                if (CurryTypeID.LastIndexOf(",") == -1)
                {
                    thisQueryStr += " and CurrencyTypeID=" + CurryTypeID + " ";
                    YearQueryStr += " and CurrencyTypeID=" + CurryTypeID + " ";
                }
                else
                {
                    //综合本位币查询条件不做处理
                }


                SelectDistinctDateSQL = string.Format(@"select distinct CONVERT(VARCHAR(10),VoucherDate,21) as VoucherDate  from Officedba.AcountBook  {0} ", thisQueryStr.Trim().Length > 0 ? " where " + thisQueryStr : "");

                DataTable dateDt = SqlHelper.ExecuteSql(SelectDistinctDateSQL);
                decimal endAmount = beginAmount;
                decimal Damount = 0;
                decimal Camount = 0;
                for (int i = 0; i < dateDt.Rows.Count; i++)
                {
                    string thisDateQueryStr = string.Empty;
                    if (SubjectsDetails.Trim().Length <= 0)
                    {
                        thisDateQueryStr = " a.VoucherDate='" + dateDt.Rows[i]["VoucherDate"].ToString() + "' and a.SubjectsCD in ( " + subjectCDList + " ) and a.CompanyCD='" + companyCD + "' ";
                    }
                    else
                    {
                        thisDateQueryStr = " a.VoucherDate='" + dateDt.Rows[i]["VoucherDate"].ToString() + "' and a.SubjectsCD in ( " + subjectCDList + " ) and a.SubjectsDetails='" + SubjectsDetails + "' and a.FormTBName='" + FormTBName + "' and a.FileName='" + FileName + "'  and a.CompanyCD='" + companyCD + "'  ";
                    }

                    if (CurryTypeID.LastIndexOf(",") == -1 && CurryTypeID.Trim().Length > 0)
                    {
                        thisDateQueryStr += " and a.CurrencyTypeID=" + CurryTypeID + " ";
                    }
                    else
                    {
                        //综合本位币查询条件不做处理
                    }



                    string InfoSql = string.Format(@"select a.ID,a.AccountDate,a.SubjectsCD,a.SubjectsDetails,a.Abstract,a.AttestBillID,a.AttestBillDetailsID,a.VoucherDate,a.CurrencyTypeID,a.ExchangeRate,a.OriginalAmount,a.ForeignBeginAmount,a.ForeignThisDebit,a.ForeignThisCredit,a.ForeignEndAmount,a.BeginAmount,a.ThisDebit,a.ThisCredit,a.EndAmount,a.CompanyCD,a.AttestBillNo,a.FormTBName,a.FileName from Officedba.AcountBook a  {0} ", thisDateQueryStr.Trim().Length > 0 ? " where " + thisDateQueryStr : "");
                    DataTable InfoDT = SqlHelper.ExecuteSql(InfoSql);//获取当日应收/应付信息

                    decimal thisDebitAmount = 0;
                    decimal thisCreditAmount = 0;
                    for (int j = 0; j < InfoDT.Rows.Count; j++)
                    {
                        /*对方科目 start*/
                        string AntiSubjectsCDsql = string.Format("select SubjectsCD,SubjectsDetails,FormTBName,FileName from  Officedba.AttestBillDetails where AttestBillID={0} and ID not in ( {1} )  ", InfoDT.Rows[j]["AttestBillID"].ToString(), InfoDT.Rows[j]["AttestBillDetailsID"].ToString());

                        DataTable s = SqlHelper.ExecuteSql(AntiSubjectsCDsql);
                        /*对方科目 end*/

                        if (CurryTypeID.LastIndexOf(",") == -1)
                        {
                            switch (Dirdt)
                            {
                                case 0:
                                    endAmount = endAmount + Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisDebit"].ToString()) - Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisCredit"].ToString());
                                    break;
                                case 1:
                                    endAmount = endAmount + Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisCredit"].ToString()) - Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisDebit"].ToString());
                                    break;
                                default:
                                    break;
                            }

                            thisDebitAmount += Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisDebit"].ToString());
                            thisCreditAmount += Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisCredit"].ToString());
                        }
                        else
                        {
                            switch (Dirdt)
                            {
                                case 0:
                                    endAmount = endAmount + Convert.ToDecimal(InfoDT.Rows[j]["ThisDebit"].ToString()) - Convert.ToDecimal(InfoDT.Rows[j]["ThisCredit"].ToString());
                                    break;
                                case 1:
                                    endAmount = endAmount + Convert.ToDecimal(InfoDT.Rows[j]["ThisCredit"].ToString()) - Convert.ToDecimal(InfoDT.Rows[j]["ThisDebit"].ToString());
                                    break;
                                default:
                                    break;
                            }
                            thisDebitAmount += Convert.ToDecimal(InfoDT.Rows[j]["ThisDebit"].ToString());
                            thisCreditAmount += Convert.ToDecimal(InfoDT.Rows[j]["ThisCredit"].ToString());
                        }

                        DataRow rowww = dt.NewRow();
                        rowww["Date"] = dateDt.Rows[i]["VoucherDate"].ToString();
                        rowww["AttestNo"] = InfoDT.Rows[j]["AttestBillNo"].ToString();
                        rowww["Abstract"] = InfoDT.Rows[j]["Abstract"].ToString();
                        rowww["AntiSubjectsCD"] = GetAntiSubjectsName(s);
                        rowww["CusOrPro"] = GetAssistantName(InfoDT.Rows[j]["SubjectsDetails"].ToString(), InfoDT.Rows[j]["FormTBName"].ToString(), InfoDT.Rows[j]["FileName"].ToString());

                        if (CurryTypeID.LastIndexOf(",") == -1)
                        {
                            rowww["DebitAmount"] = Math.Round(Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisDebit"].ToString()), 2).ToString("#,###0.#0");
                            rowww["CreditAmount"] = Math.Round(Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisCredit"].ToString()), 2).ToString("#,###0.#0");
                        }
                        else
                        {
                            rowww["DebitAmount"] = Math.Round(Convert.ToDecimal(InfoDT.Rows[j]["ThisDebit"].ToString()), 2).ToString("#,###0.#0");
                            rowww["CreditAmount"] = Math.Round(Convert.ToDecimal(InfoDT.Rows[j]["ThisCredit"].ToString()), 2).ToString("#,###0.#0");
                        }
                        rowww["Direction"] = AcountBookDBHelper.DirectionSource(subjectsCD, endAmount);
                        rowww["EndAmount"] = Math.Round(endAmount, 2).ToString("#,###0.#0");
                        rowww["ByOrderr"] = "1";
                        rowww["AttestBillID"] = InfoDT.Rows[j]["AttestBillID"].ToString();
                        dt.Rows.Add(rowww);
                    }

                    Damount += thisDebitAmount;
                    Camount += thisCreditAmount;
                }
                DataRow rowwww = dt.NewRow();
                rowwww["Date"] = "";
                rowwww["AttestNo"] = "";
                rowwww["Abstract"] = "本期合计";
                rowwww["CusOrPro"] = "";
                rowwww["AntiSubjectsCD"] = "";
                rowwww["DebitAmount"] = Math.Round(Damount, 2).ToString("#,###0.#0");
                rowwww["CreditAmount"] = Math.Round(Camount, 2).ToString("#,###0.#0");
                rowwww["Direction"] = AcountBookDBHelper.DirectionSource(subjectsCD, endAmount);
                rowwww["EndAmount"] = Math.Round(endAmount, 2).ToString("#,###0.#0");
                rowwww["ByOrderr"] = "1";
                rowwww["AttestBillID"] = "";
                dt.Rows.Add(rowwww);


                decimal YearDebit=0;
                decimal YearCredit=0;
                decimal YearEnd = 0;
                DataTable YearEndDT = AcountBookDBHelper.GetAcountSumAmount(YearQueryStr);
                if (YearEndDT.Rows.Count > 0)
                {
                    if (CurryTypeID.LastIndexOf(",") == -1)
                    {
                        YearDebit = Convert.ToDecimal(YearEndDT.Rows[0]["ForeignThisDebit"].ToString());
                        YearCredit = Convert.ToDecimal(YearEndDT.Rows[0]["ForeignThisCredit"].ToString());
                    }
                    else
                    {
                        YearDebit = Convert.ToDecimal(YearEndDT.Rows[0]["ThisDebit"].ToString());
                        YearCredit = Convert.ToDecimal(YearEndDT.Rows[0]["ThisCredit"].ToString());
                    }
                }

                if (Dirdt == 0)
                {
                    YearEnd = DefaultAmount + YearDebit - YearCredit;
                }
                else
                {
                    YearEnd = DefaultAmount + YearCredit - YearDebit;
                }
                DataRow roww = dt.NewRow();
                roww["Date"] = "";
                roww["AttestNo"] = "";
                roww["Abstract"] = "本年累计";
                roww["CusOrPro"] = "";
                roww["AntiSubjectsCD"] = "";
                roww["DebitAmount"] = Math.Round(YearDebit, 2).ToString("#,###0.#0");
                roww["CreditAmount"] = Math.Round(YearCredit, 2).ToString("#,###0.#0");
                roww["Direction"] = AcountBookDBHelper.DirectionSource(subjectsCD, YearEnd);
                roww["EndAmount"] = Math.Abs(Math.Round(YearEnd, 2)).ToString("#,###0.#0");
                roww["ByOrderr"] = "1";
                roww["AttestBillID"] = "";
                dt.Rows.Add(roww);


            }
            return dt;
        }
        #endregion

        #region 获取应收/应付汇总表数据源 其他外币数据源
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
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
            DataTable dt = new DataTable();
            dt.Columns.Add("Date");//日期
            dt.Columns.Add("AttestNo");//凭证编号
            dt.Columns.Add("Abstract");//摘要
            dt.Columns.Add("AntiSubjectsCD");//对方科目
            dt.Columns.Add("CusOrPro");//辅助核算名称
            dt.Columns.Add("CurrExg");//币种/汇率
            dt.Columns.Add("ForeignDebitAmount");//外币借方金额
            dt.Columns.Add("DebitAmount");//综合本位币借方金额
            dt.Columns.Add("ForeignCreditAmount");//外币贷方金额
            dt.Columns.Add("CreditAmount");//综合本位币贷方金额
            dt.Columns.Add("Direction");//方向
            dt.Columns.Add("ForeignEndAmount");//外币余额
            dt.Columns.Add("EndAmount");//综合本位币余额
            dt.Columns.Add("ByOrderr");//排序
            dt.Columns.Add("AttestBillID");//凭证主表ID

            //string CusOrPro = GetAssistantName(SubjectsDetails, FormTBName, FileName);

            if (IsNullSubjects(companyCD)&&CurryTypeID.Trim().Length>0)
            {
                string sql = @"select BlanceDire from officedba.AccountSubjects where SubjectsCD=@SubjectsCD and CompanyCD=@CompanyCD";
                SqlParameter[] parms = new SqlParameter[2];
                parms[0] = SqlHelper.GetParameter("@SubjectsCD", subjectsCD);
                parms[1] = SqlHelper.GetParameter("@CompanyCD", companyCD);
                object obj = SqlHelper.ExecuteScalar(sql, parms);
                int Dirdt = 0;
                if (obj != null)
                {
                    Dirdt = Convert.ToInt32(obj);
                }

                string subjectCDList = GetSubjectsNextCD(subjectsCD);//获取该科目对应的所有下级科目ID集


                string CurrencyTypeIDStr = string.Empty;
                DataTable CurrencyTypeDt = CurrTypeSettingDBHelper.GetCurrTypeByCompanyCD(companyCD);
                for (int p = 0; p < CurrencyTypeDt.Rows.Count; p++)
                {
                    CurrencyTypeIDStr += CurrencyTypeDt.Rows[p]["ID"].ToString() + ",";

                }
                CurrencyTypeIDStr = CurrencyTypeIDStr.TrimEnd(new char[] { ',' });//综合本位币所有币种ID集



                /*期初设置START*/
                string queryStr = string.Empty;
                string benweibiqueryStr = string.Empty;
                if (SubjectsDetails.Trim().Length <= 0)
                {
                    queryStr = "  VoucherDate<'" + startDate + "' and SubjectsCD in ( " + subjectCDList + " ) and CurrencyTypeID=" + CurryTypeID + " and CompanyCD='"+companyCD+"' ";//获取上日余额的查询条件（外币）
                    benweibiqueryStr = "  VoucherDate<'" + startDate + "' and SubjectsCD in ( " + subjectCDList + " )  and CompanyCD='" + companyCD + "' ";//获取上日余额的查询条件（综合本位币）
                }
                else
                {
                    queryStr = "  VoucherDate<'" + startDate + "' and SubjectsCD in ( " + subjectCDList + " )  and SubjectsDetails='" + SubjectsDetails + "' and FormTBName='" + FormTBName + "' and FileName='" + FileName + "' and CurrencyTypeID=" + CurryTypeID + " and CompanyCD='" + companyCD + "'  ";//获取上日余额的查询条件（外币）
                    benweibiqueryStr = "  VoucherDate<'" + startDate + "' and SubjectsCD in ( " + subjectCDList + " )  and SubjectsDetails='" + SubjectsDetails + "' and FormTBName='" + FormTBName + "' and FileName='" + FileName + "' and CompanyCD='" + companyCD + "'  ";//获取上日余额的查询条件（综合本位币）
                }


                decimal beginAmountt = AcountBookDBHelper.GetAccountBookBeginAmount(queryStr, subjectsCD, CurryTypeID); //GetBeginAmount(queryStr, Dirdt.ToString());//上日余额(外币)
                decimal benbeginAmountt = AcountBookDBHelper.GetAccountBookBeginAmount(benweibiqueryStr, subjectsCD, CurrencyTypeIDStr);//上日余额(综合本位币)


               decimal DefaultAmount = 0;
               decimal benDefaultAmount = 0;

               if (SubjectsDetails.Trim().Length <= 0)
               {
                   DefaultAmount = AcountBookDBHelper.GetBeginCurryTypeAmount(subjectsCD, CurryTypeID, companyCD);//年初始化金额(外币)
                   benDefaultAmount = AcountBookDBHelper.GetBeginCurryTypeAmount(subjectsCD, CurrencyTypeIDStr, companyCD);//年初始化金额(综合本位币)
               }
               else
               {
                   DefaultAmount = AcountBookDBHelper.GetSubjectsBeginDetailAmount(subjectsCD, CurryTypeID, companyCD,SubjectsDetails,FormTBName,FileName);//年初始化金额(外币)
                   benDefaultAmount = AcountBookDBHelper.GetSubjectsBeginDetailAmount(subjectsCD, CurrencyTypeIDStr, companyCD,SubjectsDetails,FormTBName,FileName);//年初始化金额(综合本位币)
               }

                decimal ForeignbeginAmount = beginAmountt + DefaultAmount;
                decimal beginAmount = benbeginAmountt + benDefaultAmount;

                string Dir = AcountBookDBHelper.DirectionSource(subjectsCD, beginAmount);//余额方向
                DataRow row = dt.NewRow();
                row["Date"] = "";
                row["AttestNo"] = "";
                row["Abstract"] = "期初余额";
                row["AntiSubjectsCD"] = "";
                row["CusOrPro"] = "";
                row["DebitAmount"] = "";
                row["CreditAmount"] = "";
                row["ForeignDebitAmount"] = "";
                row["ForeignCreditAmount"] = "";
                row["Direction"] = Dir;
                row["EndAmount"] = Math.Round(beginAmount, 2).ToString("#,###0.#0");
                row["ForeignEndAmount"] = Math.Round(ForeignbeginAmount, 2).ToString("#,###0.#0");
                row["ByOrderr"] = "1";
                row["AttestBillID"] = "";
                row["CurrExg"] = "";
                dt.Rows.Add(row);
                /*期初设置END*/

                /*本日应收/应付明细 Start*/
                string thisQueryStr = string.Empty;
                string SelectDistinctDateSQL = string.Empty;
                string YearQueryStr = string.Empty;
                if (SubjectsDetails.Trim().Length <= 0)
                {
                    thisQueryStr = " VoucherDate>='" + startDate + "' and VoucherDate<='" + endDate + "'  and SubjectsCD in ( " + subjectCDList + " ) and CurrencyTypeID=" + CurryTypeID + " and CompanyCD='" + companyCD + "' ";

                    YearQueryStr = " VoucherDate<='" + endDate + "'  and SubjectsCD in ( " + subjectCDList + " ) and CurrencyTypeID=" + CurryTypeID + " and CompanyCD='" + companyCD + "' ";
                }
                else
                {
                    thisQueryStr = " VoucherDate>='" + startDate + "' and VoucherDate<='" + endDate + "'  and SubjectsCD in ( " + subjectCDList + " )and SubjectsDetails='" + SubjectsDetails + "' and FormTBName='" + FormTBName + "' and FileName='" + FileName + "' and CurrencyTypeID=" + CurryTypeID + " and CompanyCD='" + companyCD + "'  ";

                    YearQueryStr = " VoucherDate<='" + endDate + "'  and SubjectsCD in ( " + subjectCDList + " )and SubjectsDetails='" + SubjectsDetails + "' and FormTBName='" + FormTBName + "' and FileName='" + FileName + "' and CurrencyTypeID=" + CurryTypeID + " and CompanyCD='" + companyCD + "'  ";
                }


                SelectDistinctDateSQL = string.Format(@"select distinct CONVERT(VARCHAR(10),VoucherDate,21) as VoucherDate  from Officedba.AcountBook  {0} ", thisQueryStr.Trim().Length > 0 ? " where " + thisQueryStr : "");

                DataTable dateDt = SqlHelper.ExecuteSql(SelectDistinctDateSQL);
                decimal endAmount = beginAmount;
                decimal Damount = 0;
                decimal Camount = 0;
                decimal ForeignendAmount = ForeignbeginAmount;
                decimal ForeignDamount = 0;
                decimal ForeignCamount = 0;

                for (int i = 0; i < dateDt.Rows.Count; i++)
                {
                    string thisDateQueryStr = string.Empty;
                    
                    if (SubjectsDetails.Trim().Length <= 0)
                    {
                        thisDateQueryStr = " a.VoucherDate='" + dateDt.Rows[i]["VoucherDate"].ToString() + "' and a.SubjectsCD in ( " + subjectCDList + " ) and a.CurrencyTypeID=" + CurryTypeID + " and a.CompanyCD='" + companyCD + "' ";
                    }
                    else
                    {
                        thisDateQueryStr = " a.VoucherDate='" + dateDt.Rows[i]["VoucherDate"].ToString() + "' and a.SubjectsCD in ( " + subjectCDList + " ) and a.SubjectsDetails='" + SubjectsDetails + "' and a.FormTBName='" + FormTBName + "' and a.FileName='" + FileName + "' and a.CurrencyTypeID=" + CurryTypeID + "  and a.CompanyCD='" + companyCD + "'  ";
                    }


                    string InfoSql = string.Format(@"select a.ID,a.AccountDate,a.SubjectsCD,a.SubjectsDetails,a.Abstract,a.AttestBillID,a.AttestBillDetailsID,a.VoucherDate,a.CurrencyTypeID,c.CurrencyName,a.ExchangeRate,a.OriginalAmount,a.ForeignBeginAmount,a.ForeignThisDebit,a.ForeignThisCredit,a.ForeignEndAmount,a.BeginAmount,a.ThisDebit,a.ThisCredit,a.EndAmount,a.CompanyCD,a.AttestBillNo,a.FormTBName,a.FileName from Officedba.AcountBook a  left outer join officedba.CurrencyTypeSetting c on a.CurrencyTypeID=c.ID {0} ", thisDateQueryStr.Trim().Length > 0 ? " where " + thisDateQueryStr : "");
                    DataTable InfoDT = SqlHelper.ExecuteSql(InfoSql);//获取当日应收/应付信息

                    decimal thisDebitAmount = 0;
                    decimal thisCreditAmount = 0;
                    decimal ForeignthisDebitAmount = 0;
                    decimal ForeignthisCreditAmount = 0;
                    for (int j = 0; j < InfoDT.Rows.Count; j++)
                    {
                        /*对方科目 start*/
                        string AntiSubjectsCDsql = string.Format("select SubjectsCD,SubjectsDetails,FormTBName,FileName from  Officedba.AttestBillDetails where AttestBillID={0} and ID not in ( {1} )  ", InfoDT.Rows[j]["AttestBillID"].ToString(), InfoDT.Rows[j]["AttestBillDetailsID"].ToString());

                        DataTable s = SqlHelper.ExecuteSql(AntiSubjectsCDsql);
                        /*对方科目 end*/


                        switch (Dirdt)
                        {
                            case 0:
                                endAmount = endAmount + Convert.ToDecimal(InfoDT.Rows[j]["ThisDebit"].ToString()) - Convert.ToDecimal(InfoDT.Rows[j]["ThisCredit"].ToString());
                                ForeignendAmount = ForeignendAmount + Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisDebit"].ToString()) - Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisCredit"].ToString());
                                break;
                            case 1:
                                endAmount = endAmount + Convert.ToDecimal(InfoDT.Rows[j]["ThisCredit"].ToString()) - Convert.ToDecimal(InfoDT.Rows[j]["ThisDebit"].ToString());
                                ForeignendAmount = ForeignendAmount + Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisCredit"].ToString()) - Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisDebit"].ToString());
                                break;
                            default:
                                break;
                        }

                        thisDebitAmount += Convert.ToDecimal(InfoDT.Rows[j]["ThisDebit"].ToString());
                        thisCreditAmount += Convert.ToDecimal(InfoDT.Rows[j]["ThisCredit"].ToString());
                        ForeignthisDebitAmount += Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisDebit"].ToString());
                        ForeignthisCreditAmount += Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisCredit"].ToString());


                        DataRow rowww = dt.NewRow();
                        rowww["Date"] = dateDt.Rows[i]["VoucherDate"].ToString();
                        rowww["AttestNo"] = InfoDT.Rows[j]["AttestBillNo"].ToString();
                        rowww["Abstract"] = InfoDT.Rows[j]["Abstract"].ToString();
                        rowww["AntiSubjectsCD"] = GetAntiSubjectsName(s);
                        rowww["ForeignDebitAmount"] = Math.Round(Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisDebit"].ToString()), 2).ToString("#,###0.#0");
                        rowww["ForeignCreditAmount"] = Math.Round(Convert.ToDecimal(InfoDT.Rows[j]["ForeignThisCredit"].ToString()), 2).ToString("#,###0.#0");
                        rowww["DebitAmount"] = Math.Round(Convert.ToDecimal(InfoDT.Rows[j]["ThisDebit"].ToString()), 2).ToString("#,###0.#0");
                        rowww["CreditAmount"] = Math.Round(Convert.ToDecimal(InfoDT.Rows[j]["ThisCredit"].ToString()), 2).ToString("#,###0.#0");
                        rowww["Direction"] = AcountBookDBHelper.DirectionSource(subjectsCD, endAmount);
                        rowww["EndAmount"] = Math.Round(endAmount, 2).ToString("#,###0.#0");
                        rowww["ForeignEndAmount"] = Math.Round(ForeignendAmount, 2).ToString("#,###0.#0");
                        rowww["ByOrderr"] = "1";
                        rowww["AttestBillID"] = InfoDT.Rows[j]["AttestBillID"].ToString();
                        rowww["CusOrPro"] = GetAssistantName(InfoDT.Rows[j]["SubjectsDetails"].ToString(), InfoDT.Rows[j]["FormTBName"].ToString(), InfoDT.Rows[j]["FileName"].ToString());

                        rowww["CurrExg"] = InfoDT.Rows[j]["CurrencyName"].ToString() + "(" + Math.Round(Convert.ToDecimal(InfoDT.Rows[j]["ExchangeRate"].ToString()), 2) + ")";
                        dt.Rows.Add(rowww);
                    }

                    Damount += thisDebitAmount;
                    Camount += thisCreditAmount;
                    ForeignDamount += ForeignthisDebitAmount;
                    ForeignCamount += ForeignthisCreditAmount;
                   
                }
                DataRow rowwww = dt.NewRow();
                rowwww["Date"] = "";
                rowwww["AttestNo"] = "";
                rowwww["Abstract"] = "本期合计";
                rowwww["CusOrPro"] = "";
                rowwww["AntiSubjectsCD"] = "";
                rowwww["DebitAmount"] = Math.Round(Damount, 2).ToString("#,###0.#0");
                rowwww["CreditAmount"] = Math.Round(Camount, 2).ToString("#,###0.#0");
                rowwww["Direction"] = AcountBookDBHelper.DirectionSource(subjectsCD, ForeignendAmount);
                rowwww["EndAmount"] = Math.Round(endAmount, 2).ToString("#,###0.#0");
                rowwww["ByOrderr"] = "1";
                rowwww["AttestBillID"] = "";
                rowwww["ForeignDebitAmount"] = Math.Round(ForeignDamount, 2).ToString("#,###0.#0");
                rowwww["ForeignCreditAmount"] = Math.Round(ForeignCamount, 2).ToString("#,###0.#0");
                rowwww["ForeignEndAmount"] = Math.Round(ForeignendAmount, 2).ToString("#,###0.#0");
                rowwww["CurrExg"] = "";


                dt.Rows.Add(rowwww);



                decimal YearDebit = 0;
                decimal YearCredit = 0;
                decimal YearEnd = 0;
                decimal ForeignYearDebit = 0;
                decimal ForeignYearCredit = 0;
                decimal ForeignYearEnd = 0;
                DataTable YearEndDT = AcountBookDBHelper.GetAcountSumAmount(YearQueryStr);
                if (YearEndDT.Rows.Count > 0)
                {
                    ForeignYearDebit = Convert.ToDecimal(YearEndDT.Rows[0]["ForeignThisDebit"].ToString());
                    ForeignYearCredit = Convert.ToDecimal(YearEndDT.Rows[0]["ForeignThisCredit"].ToString());
                    YearDebit = Convert.ToDecimal(YearEndDT.Rows[0]["ThisDebit"].ToString());
                    YearCredit = Convert.ToDecimal(YearEndDT.Rows[0]["ThisCredit"].ToString());
                }

                if (Dirdt == 0)
                {
                    YearEnd = benDefaultAmount + YearDebit - YearCredit;
                    ForeignYearEnd = DefaultAmount + ForeignYearDebit - ForeignYearCredit;
                }
                else
                {
                    YearEnd = benDefaultAmount - YearDebit + YearCredit;
                    ForeignYearEnd = DefaultAmount - ForeignYearDebit + ForeignYearCredit;
                }

                DataRow roww = dt.NewRow();
                roww["Date"] = "";
                roww["AttestNo"] = "";
                roww["Abstract"] = "本年累计";
                roww["AntiSubjectsCD"] = "";
                roww["DebitAmount"] = Math.Round(YearDebit, 2).ToString("#,###0.#0");
                roww["CreditAmount"] = Math.Round(YearCredit, 2).ToString("#,###0.#0");
                roww["Direction"] = AcountBookDBHelper.DirectionSource(subjectsCD, ForeignYearEnd);
                roww["EndAmount"] = Math.Round(YearEnd, 2).ToString("#,###0.#0");
                roww["ByOrderr"] = "1";
                roww["AttestBillID"] = "";
                roww["CusOrPro"] = "";
                roww["ForeignDebitAmount"] = Math.Round(ForeignYearDebit, 2).ToString("#,###0.#0");
                roww["ForeignCreditAmount"] = Math.Round(ForeignYearCredit, 2).ToString("#,###0.#0");
                roww["ForeignEndAmount"] = Math.Round(ForeignYearEnd, 2).ToString("#,###0.#0");
                roww["CurrExg"] = "";


                dt.Rows.Add(roww);



            }
            return dt;
        }
        #endregion

        #region 科目汇总表数据源--已调整
        /// <summary>
        /// 科目汇总表数据源
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="CurryTypeID">币种</param>
        /// <returns></returns>
        public static DataTable SubjectsTotalSource(string startDate, string endDate, string CurryTypeID)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码

            //string MasterCurrency = string.Empty;//获取本位币ID 
            //DataTable CurrTypeDT = CurrTypeSettingDBHelper.GetMasterCurrency(companyCD);
            //if (CurrTypeDT.Rows.Count > 0)
            //{
            //    MasterCurrency = CurrTypeDT.Rows[0]["ID"].ToString();
            //}

            DataTable dt = new DataTable();

            dt.Columns.Add("SubjectsCD");//会计科目代码
            dt.Columns.Add("SubjectsCDName");//会计科目名称
            dt.Columns.Add("DebitAmount");//借方金额
            dt.Columns.Add("CreditAmount");//贷方金额
            dt.Columns.Add("ByOrderr");//排序

            if (IsNullSubjects(companyCD)&&CurryTypeID.Trim().Length>0)
            {

                string sql = @"select distinct SubjectsCD from Officedba.AcountBook where VoucherDate>=@startDate and VoucherDate<=@endDate and CompanyCD=@CompanyCD and CurrencyTypeID in (" + CurryTypeID + ")   order by SubjectsCD asc";
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = SqlHelper.GetParameter("@startDate", startDate);
                parms[1] = SqlHelper.GetParameter("@endDate", endDate);
                parms[2] = SqlHelper.GetParameter("@CompanyCD", companyCD);
                DataTable subjectsDT = SqlHelper.ExecuteSql(sql, parms);//统计从startDate到endDate的不重复的会计科目

                decimal DebitAmountSUM = 0;
                decimal CreditAmountSUM = 0;

                for (int i = 0; i < subjectsDT.Rows.Count; i++)
                {
                    DataRow row = dt.NewRow();
                    row["SubjectsCD"] = subjectsDT.Rows[i]["SubjectsCD"].ToString();
                    row["SubjectsCDName"] = GetSubjectsName(subjectsDT.Rows[i]["SubjectsCD"].ToString(), companyCD);
                    row["DebitAmount"] = "0";
                    row["CreditAmount"] = "0";
                    row["ByOrderr"] = "1";
                    string queryStr = " VoucherDate>='" + startDate + "' and VoucherDate<='" + endDate + "' and CompanyCD='" + companyCD + "'and SubjectsCD in( " + GetSubjectsNextCD(subjectsDT.Rows[i]["SubjectsCD"].ToString()) + ") and CurrencyTypeID in ( " + CurryTypeID + " )  ";

                    DataTable sumDT = AcountBookDBHelper.GetAcountSumAmount(queryStr);//统计对应科目的借方金额合计和贷方金额合计

                    if (sumDT.Rows.Count > 0)
                    {
                        if (CurryTypeID.LastIndexOf(",") == -1)//外币
                        {
                            row["DebitAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ForeignThisDebit"].ToString()), 2).ToString("#,###0.#0");
                            row["CreditAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ForeignThisCredit"].ToString()), 2).ToString("#,###0.#0");
                            DebitAmountSUM += Convert.ToDecimal(sumDT.Rows[0]["ForeignThisDebit"].ToString());
                            CreditAmountSUM += Convert.ToDecimal(sumDT.Rows[0]["ForeignThisCredit"].ToString());
                        }
                        else//综合本位币
                        {
                            row["DebitAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ThisDebit"].ToString()), 2).ToString("#,###0.#0");
                            row["CreditAmount"] = Math.Round(Convert.ToDecimal(sumDT.Rows[0]["ThisCredit"].ToString()), 2).ToString("#,###0.#0");
                            DebitAmountSUM += Convert.ToDecimal(sumDT.Rows[0]["ThisDebit"].ToString());
                            CreditAmountSUM += Convert.ToDecimal(sumDT.Rows[0]["ThisCredit"].ToString());
                        }

                    }
                    dt.Rows.Add(row);
                }
                DataRow roww = dt.NewRow();
                roww["SubjectsCD"] = "合计";
                roww["SubjectsCDName"] = "";
                roww["DebitAmount"] = Math.Round(DebitAmountSUM, 2).ToString("#,###0.#0");
                roww["CreditAmount"] = Math.Round(CreditAmountSUM, 2).ToString("#,###0.#0");
                roww["ByOrderr"] = "1";
                dt.Rows.Add(roww);
            }
            return dt;
        }
        #endregion

        #region 根据查询条件统计对应科目的借方金额合计和贷方金额合计
        /// <summary>
        /// 根据查询条件统计对应科目的借方金额合计和贷方金额合计
        /// </summary>
        /// <param name="querySty"></param>
        /// <returns></returns>
        public static DataTable GetSumAmount(string querySty)
        {
            string sumSQL = string.Format(@"select isnull(DebitAmount,0) as DebitAmount, isnull(CreditAmount,0) as CreditAmount,isnull(ExchangeRate,1) as ExchangeRate  from Officedba.AttestBillDetails a left outer join Officedba.AttestBill b on a.AttestBillID=b.ID {0}", querySty.Trim().Length > 0 ? " where " + querySty : "");

            DataTable dt = new DataTable();
            dt.Columns.Add("DebitAmount");//借方金额合计
            dt.Columns.Add("CreditAmount");//贷方金额合计
            decimal debitAmount = 0;
            decimal creditAmount = 0;
            DataTable sumDT = SqlHelper.ExecuteSql(sumSQL);
            for (int i = 0; i < sumDT.Rows.Count; i++)
            {
                debitAmount += Convert.ToDecimal(sumDT.Rows[i]["DebitAmount"].ToString()) * Convert.ToDecimal(sumDT.Rows[i]["ExchangeRate"].ToString());
                creditAmount += Convert.ToDecimal(sumDT.Rows[i]["CreditAmount"].ToString()) * Convert.ToDecimal(sumDT.Rows[i]["ExchangeRate"].ToString());
            }
            DataRow row = dt.NewRow();
            row["DebitAmount"] = debitAmount.ToString();
            row["CreditAmount"] = creditAmount.ToString();
            dt.Rows.Add(row);
            return dt;

        }
        #endregion

        #region 获取试算平衡表不相同的科目
        public static DataTable GetSpreadsheetSubjectsCD(string startDate, string endDate, string CurryTypeID,string companyCD)
        {
            DataTable subjectsDT = new DataTable();
            if (CurryTypeID.Trim().Length > 0)
            {
                string sql = @"select distinct SubjectsCD from Officedba.AcountBook where VoucherDate>=@startDate and VoucherDate<=@endDate and CompanyCD=@CompanyCD and CurrencyTypeID in (" + CurryTypeID + ")  order by SubjectsCD asc";
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = SqlHelper.GetParameter("@startDate", startDate);
                parms[1] = SqlHelper.GetParameter("@endDate", endDate);
                parms[2] = SqlHelper.GetParameter("@CompanyCD", companyCD);
                subjectsDT = SqlHelper.ExecuteSql(sql, parms);//统计从startDate到endDate的不重复的会计科目
            }
            return subjectsDT;
        }
        #endregion

        #region 获取会计科目方向
        public static int GetSubjectDirection(string SubjectsCD, string CompanyCD)
        {
            /*根据科目编码获取该科目方向开始*/
            string Balancesql = @"select BlanceDire from officedba.AccountSubjects where SubjectsCD=@SubjectsCD and CompanyCD=@CompanyCD";
            SqlParameter[] parmss = new SqlParameter[2];
            parmss[0] = SqlHelper.GetParameter("@SubjectsCD",SubjectsCD);
            parmss[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            object obj = SqlHelper.ExecuteScalar(Balancesql, parmss);
            int Dirdt = 0;
            if (obj != null)
            {
                Dirdt = Convert.ToInt32(obj);
            }

            return Dirdt;

        }
        #endregion

        #region 账簿金额统计
        /// <summary>
        /// 获取借贷方金额总和
        /// </summary>
        /// <param name="subjectsCD">科目编码</param>
        /// <param name="DatequeryStr">日期限制条件</param>
        /// <returns></returns>
        public static DataTable GetAmount(string subjectsCD, string DatequeryStr)
        {
            //科目方向
            string subjectCDList = VoucherDBHelper.GetSubjectsNextCD(subjectsCD);
            //金额统计SQL
            string sql = string.Format("select ISNULL(SUM(ThisDebit),0) as ThisDebit, ISNULL(SUM(ThisCredit),0) as ThisCredit from Officedba.AcountBook  where subjectsCD in ( {0} ) {1} ", subjectCDList, DatequeryStr.Trim().Length > 0 ? " and " + DatequeryStr : "");
            DataTable beginDT = SqlHelper.ExecuteSql(sql);
            return beginDT;
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
        public static decimal GetBeginAmount(string DatequeryStr, string subjectsCD, string Direction)
        {

            decimal BeginAmount = 0;
            //科目方向
            string subjectCDList = VoucherDBHelper.GetSubjectsNextCD(subjectsCD);
            //金额统计SQL
            string sql = string.Format("select ISNULL(SUM(ThisDebit),0) as ThisDebit, ISNULL(SUM(ThisCredit),0) as ThisCredit from Officedba.AcountBook  where subjectsCD in ( {0} ) {1} ", subjectCDList, DatequeryStr.Trim().Length > 0 ? " and " + DatequeryStr : "");
            DataTable beginDT = SqlHelper.ExecuteSql(sql);

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

        #region 上一张，下一张 凭证修改页面功能
        /// <summary>
        /// 上一张，下一张 凭证修改页面功能
        /// </summary>
        /// <param name="ID">凭证ID</param>
        /// <param name="type">1：上一张；2：下一张</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static int GetPreOrNextAttestBillID(int ID,int type,string CompanyCD)
        {
            string SelectSQL = string.Empty;
            switch (type)
            {
                case 1:
                    SelectSQL = "Select top 1 ID from Officedba.AttestBill where ID<@ID and CompanyCD=@CompanyCD order by ID desc ";
                    break;
                case 2:
                    SelectSQL = "Select top 1 ID from Officedba.AttestBill where ID>@ID and CompanyCD=@CompanyCD order by ID asc ";
                    break;
                default:
                    break;
            }

            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = SqlHelper.GetParameter("@ID", ID);
            parms[1]=SqlHelper.GetParameter("@CompanyCD",CompanyCD);

            int nev = 0;
            object obj = SqlHelper.ExecuteScalar(SelectSQL, parms);
            if (obj != null)
            {
                nev = Convert.ToInt32(obj);
            }

            return nev;


        }
        #endregion

        #region 获取从出纳模块统计的现金/银行账目视图数据集
        /// <summary>
        /// 获取从出纳模块统计的现金/银行账目视图数据集
        /// </summary>
        /// <param name="QueryStr">查询条件</param>
        /// <returns></returns>
        public static DataTable GetCashOrBankView(string QueryStr)
        {
            try
            {
                string sql = @" select [ID],[CompanyCD],[ConfirmDate],[Code],[Amount],[RetDate],[AcceWay],[BankName] ,[BankNo],[Direction],[Summary],[CurrencyType],[CurrencyRate],[Comprehensive],[SourceTB] from officedba.CashierSource {0} ";
                string ExecSql = string.Format(sql, QueryStr.Trim().Length > 0 ? " where  " + QueryStr : "");
                return SqlHelper.ExecuteSql(ExecSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 从现金/银行账目视图获取不重复的日期（查询条件）
        /// <summary>
        /// 从现金/银行账目视图获取不重复的日期（查询条件）
        /// </summary>
        /// <param name="QueryStr">查询条件</param>
        /// <returns></returns>
        public static DataTable GetDistinctDateView(string QueryStr)
        {
            try
            {
                string sql = @" select distinct RetDate from officedba.CashierSource {0} order by RetDate asc ";
                string ExecSql = string.Format(sql, QueryStr.Trim().Length > 0 ? " where " + QueryStr : "");
                return SqlHelper.ExecuteSql(ExecSql);
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
        public static bool IsNullSubjects(string CompanyCD)
        {
            string sql = "select count(ID) from officedba.AccountSubjects where CompanyCD=@CompanyCD";
            SqlParameter[] parms = 
                    {
                        
                         new SqlParameter("@CompanyCD",CompanyCD)
                        
                    };
            int nev = 0;
            object obj = SqlHelper.ExecuteScalar(sql, parms);
            if (obj != null)
            {
                nev = Convert.ToInt32(obj);
            }
            return nev > 0 ? true : false;

        }

        #endregion

        #region 新建凭证时若数据源从流水帐中读取数据，插入流水账明细表记录
        /// <summary>
        /// 新建凭证时若数据源从流水帐中读取数据，插入流水账明细表记录
        /// </summary>
        /// <param name="Codes">源单编码</param>
        /// <param name="AttestBillID">凭证主键</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static bool InsertRunningAccoutDetails(string Codes, int AttestBillID,string CompanyCD)
        {
            try
            {
                int nev = 0;
                string[] CodeStr = Codes.Split(',');
                for (int i = 0; i < CodeStr.Length; i++)
                {
                    string sql = "insert into officedba.RunningBillDetail (AttestID,RunningBillCD,CompanyCD) values ( @AttestID,@RunningBillCD,@CompanyCD )";
                    SqlParameter[] parms = 
                    {
                        
                         new SqlParameter("@AttestID",AttestBillID.ToString()),
                         new SqlParameter("@RunningBillCD",CodeStr[i].ToString()),
                         new SqlParameter("@CompanyCD",CompanyCD)
                        
                    };

                    nev += SqlHelper.ExecuteTransSql(sql, parms);
                }
                return nev > 0 ? true : false;
            }
            catch(Exception ex)
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
            string nev = string.Empty;
            if (SubjectsDetails.Trim().Length > 0 && FormTBName.Trim().Length > 0 && FileName.Trim().Length > 0)
            {
                string sql = string.Format(@"select {0} from {1} where ID='{2}'",FileName,FormTBName,SubjectsDetails);
                object obj = SqlHelper.ExecuteScalar(sql,null);
                if (obj != null)
                {
                    nev = Convert.ToString(obj);
                }
            }

            return nev;
        }
        #endregion

        #region 获取期末处理损益结转过来的凭证主键集
        /// <summary>
        /// 获取期末处理损益结转过来的凭证主键集
        /// </summary>
        /// <param name="StartDate">凭证开始日期</param>
        /// <param name="EndDate">凭证结束日期</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static string GetProfitandLossAttestIDS(string StartDate, string EndDate, string CompanyCD)
        {
            string sql = "Select ID from officedba.AttestBill where VoucherDate>=@StartDate and VoucherDate<=@EndDate and CompanyCD=@CompanyCD and FromTbale='officedba.EndItemProcessedRecord' and FromName='ProfitandLoss'";
            SqlParameter[] parms = 
           {
               
               new SqlParameter("@StartDate",StartDate),
               new SqlParameter("@EndDate",EndDate),
               new SqlParameter("@CompanyCD",CompanyCD)
           };
            DataTable dt = SqlHelper.ExecuteSql(sql.ToString(), parms);
            string rev = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                rev += dr["ID"].ToString()+",";

            }

            return rev.TrimEnd(new char[] { ',' });
        }
        #endregion

        #region 生成资产负债表时判断对应期间的期末处理是否处理过
        /// <summary>
        /// 生成资产负债表时判断对应期间的期末处理是否处理过
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="PerioNum">会计期间</param>
        /// <returns></returns>
        public static string IsEndBalanceSheet(string CompanyCD, string PerioNum)
        {
            string ReturnStr = string.Empty;
            string ItemSql = "select ID,ItemName from officedba.EndItemProcSetting where CompanyCD=@CompanyCD and UsedStatus='1' and ItemName='损益结转' ";
            SqlParameter[] parms = 
            {
                 new SqlParameter("@CompanyCD",CompanyCD)
            };

            DataTable ItemDT = SqlHelper.ExecuteSql(ItemSql, parms);

            foreach (DataRow row in ItemDT.Rows)
            {
                string SelectSQL = " select count(ID) from officedba.EndItemProcessedRecord where PeriodNum=@PeriodNum and ItemID=@ItemID and CompanyCD=@CompanyCD  ";

                SqlParameter[] parmst = 
                {
                     new SqlParameter("@PeriodNum",PerioNum),
                     new SqlParameter("@ItemID",row["ID"].ToString()),
                     new SqlParameter("@CompanyCD",CompanyCD)
                };
                object obj=SqlHelper.ExecuteScalar(SelectSQL,parmst);

                if (obj != null)
                {
                    if (Convert.ToInt32(obj) <= 0)
                    {
                        ReturnStr += row["ItemName"].ToString() + "|";
                    }
                }
            }

            return ReturnStr.TrimEnd(new char[] { '|' });

        }
        #endregion
    }
}

/**********************************************
 * 类作用：   账簿表数据库层处理
 * 建立人：   莫申林
 * 建立时间： 2009/04/16
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
using XBase.Common;
using System.Collections;
namespace XBase.Data.Office.FinanceManager
{
    public class AcountBookDBHelper
    {

        #region 根据查询条件获取账簿信息
        /// <summary>
        /// 根据查询条件获取账簿信息
        /// </summary>
        /// <param name="queryStr">查询条件</param>
        /// <returns></returns>
        public static DataTable GetAcountBookInfo(string queryStr)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.AccoutBookNo,CONVERT(VARCHAR(10),AccountDate,21) as AccountDate ,a.SubjectsCD,b.SubjectsName,");
            sql.AppendLine("a.SubjectsDetails,case when c.NameCn is null then '' when c.NameCn is not null then c.NameCn end  as SubjectsDetailsName,case(a.Direction) when 0 then '借' when 1 then '贷' end as Direction,a.AttestBillID,a.BeginAmount,");
            sql.AppendLine("a.ThisDebit,a.ThisCredit,a.EndAmount,a.CompanyCD,a.Abstract,case when d.Name is null then '' when d.Name is not null then d.Name end  as AbstractName ");
            sql.AppendLine(" from Officedba.AcountBook a left outer join Officedba.AccountSubjects b ");
            sql.AppendLine(" on a.SubjectsCD=b.SubjectsCD ");
            sql.AppendLine(" left outer join company c ");
            sql.AppendLine(" on a.SubjectsDetails=c.CompanyCD ");
            sql.AppendLine(" left outer join officedba.summarySetting d  ");
            sql.AppendLine(" on a.Abstract=d.ID {0} ");

            string selectSql = string.Format(sql.ToString(), queryStr.Trim().Length > 0 ? " where " + queryStr : "");
            return SqlHelper.ExecuteSql(selectSql);
        }
        #endregion

        #region 明细帐数据源
        /// <summary>
        /// 明细帐数据源
        /// </summary>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="subjectsCD"></param>
        /// <returns></returns>
        public static DataTable GetAccountBookListSource(string begindate, string enddate, string subjectsCD, string CurryTypeID, string CompanyCD, string EndSubjectsCD, string FromTBName, string FileName, string subjectsDetails)
        {

            DataTable sourceDT = new DataTable();
            sourceDT.Columns.Add("AttestBillID");
            sourceDT.Columns.Add("AccountDate");
            sourceDT.Columns.Add("AttestBillNo");
            sourceDT.Columns.Add("Subjects");
            sourceDT.Columns.Add("SubjectsCD");
            sourceDT.Columns.Add("Abstract");
            sourceDT.Columns.Add("ThisDebit");
            sourceDT.Columns.Add("ThisCredit");
            sourceDT.Columns.Add("Direction");
            sourceDT.Columns.Add("ENDAmount");
            sourceDT.Columns.Add("ByOrder");
            string AssistantTypeQueryStr = string.Empty;//辅助核算限制条件
            string AssistantTypeQueryStrBya = string.Empty;

            if (subjectsDetails.Trim().Length > 0 && FromTBName.Trim().Length > 0 && FileName.Trim().Length > 0)
            {
                AssistantTypeQueryStr = " and  SubjectsDetails='" + subjectsDetails + "' and FormTBName='" + FromTBName + "' and FileName='" + FileName + "'";
                AssistantTypeQueryStrBya = " and  a.SubjectsDetails='" + subjectsDetails + "' and a.FormTBName='" + FromTBName + "' and a.FileName='" + FileName + "'";
            }
            if (VoucherDBHelper.IsNullSubjects(CompanyCD))
            {
                string distinctsql = string.Empty;


                string beginCDDT, endCDDT = "";
                beginCDDT = VoucherDBHelper.GetSubjectsNextCD(subjectsCD);
                endCDDT = VoucherDBHelper.GetSubjectsNextCD(EndSubjectsCD);
                beginCDDT = beginCDDT.Replace("'", "").Split(',')[0].ToString();
                string[] endStr = endCDDT.Replace("'", "").Split(',');
                int endCount = endStr.Length;
                endCDDT = endStr[endCount - 1].ToString();

                ArrayList MyList = new ArrayList();
                if (subjectsCD.Trim().Length > 0 && EndSubjectsCD.Trim().Length <= 0)
                    MyList.Add(subjectsCD);
                else if (subjectsCD.Trim().Length <= 0 && EndSubjectsCD.Trim().Length > 0)
                {
                    distinctsql = string.Format("select distinct SubjectsCD from Officedba.AcountBook  where VoucherDate>='{0}' and VoucherDate<='{1}' and SubjectsCD<='{2}' and CompanyCD='{3}' {4} order by SubjectsCD asc", begindate, enddate, endCDDT, CompanyCD, AssistantTypeQueryStr);
                }
                else if (subjectsCD.Trim().Length > 0 && EndSubjectsCD.Trim().Length > 0)
                {

                    distinctsql = string.Format("select distinct SubjectsCD from Officedba.AcountBook  where VoucherDate>='{0}' and VoucherDate<='{1}' and ( SubjectsCD between '{2}' and '{3}' ) and CompanyCD='{4}' {5}  order by SubjectsCD asc", begindate, enddate, beginCDDT, endCDDT, CompanyCD, AssistantTypeQueryStr);
                }
                else
                {
                    distinctsql = string.Format("select distinct SubjectsCD from Officedba.AcountBook  where VoucherDate>='{0}' and VoucherDate<='{1}' and CompanyCD='{2}' {3} order by SubjectsCD asc", begindate, enddate, CompanyCD, AssistantTypeQueryStr);

                }
                DataTable distinctSubjectsDT = new DataTable();
                if (distinctsql.Trim().Length > 0)
                {
                    distinctSubjectsDT = SqlHelper.ExecuteSql(distinctsql);
                }
                foreach (DataRow dr in distinctSubjectsDT.Rows)
                {
                    MyList.Add(dr["SubjectsCD"].ToString());
                }





                for (int j = 0; j < MyList.Count; j++)
                {
                    string beginAmountSQL = string.Empty;
                    string queryStr = string.Empty;
                    string queryString = string.Empty;
                    string totalQueryStr = string.Empty;
                    string subjectCDList = VoucherDBHelper.GetSubjectsNextCD(MyList[j].ToString());
                    if (CurryTypeID.LastIndexOf(",") == -1)
                    {
                        //人民币期初金额
                        beginAmountSQL = "VoucherDate<'" + begindate + "'  and SubjectsCD in ( " + subjectCDList + " ) and CurrencyTypeID=" + CurryTypeID + " and CompanyCD='" + CompanyCD + "' " + AssistantTypeQueryStr + "";
                        queryStr = " ( a.VoucherDate between '" + begindate + "' and '" + enddate + "' ) and a.SubjectsCD in ( " + subjectCDList + " ) and a.CurrencyTypeID=" + CurryTypeID + " and a.CompanyCD='" + CompanyCD + "' " + AssistantTypeQueryStrBya + "";
                        queryString = " ( VoucherDate between '" + begindate + "' and '" + enddate + "' ) and SubjectsCD in ( " + subjectCDList + " )  and CurrencyTypeID=" + CurryTypeID + " and CompanyCD='" + CompanyCD + "' " + AssistantTypeQueryStr + "";
                        totalQueryStr = " VoucherDate<='" + enddate + "'  and SubjectsCD in ( " + subjectCDList + " ) and CurrencyTypeID=" + CurryTypeID + " and CompanyCD='" + CompanyCD + "' " + AssistantTypeQueryStr + " ";
                    }
                    else
                    {
                        //综合本位币期初金额
                        beginAmountSQL = "VoucherDate<'" + begindate + "'  and SubjectsCD in ( " + subjectCDList + " ) and CompanyCD='" + CompanyCD + "' " + AssistantTypeQueryStr + "";

                        queryStr = " ( a.VoucherDate between '" + begindate + "' and '" + enddate + "' ) and a.SubjectsCD in ( " + subjectCDList + " )  and CompanyCD='" + CompanyCD + "' " + AssistantTypeQueryStrBya + "";
                        queryString = " ( VoucherDate between '" + begindate + "' and '" + enddate + "' ) and SubjectsCD in ( " + subjectCDList + " ) and CompanyCD='" + CompanyCD + "' " + AssistantTypeQueryStr + " ";
                        totalQueryStr = " VoucherDate<='" + enddate + "'  and SubjectsCD in ( " + subjectCDList + " ) and CompanyCD='" + CompanyCD + "' " + AssistantTypeQueryStr + "";
                    }
                    string directionSql = string.Format(@"select BlanceDire from officedba.AccountSubjects where SubjectsCD='{0}' and CompanyCD='" + CompanyCD + "' ", MyList[j].ToString());
                    int direction = Convert.ToInt32(SqlHelper.ExecuteScalar(directionSql, null));



                    decimal BeginAmount = 0;

                    BeginAmount = GetAccountBookBeginAmount(beginAmountSQL, MyList[j].ToString(), CurryTypeID);// GetAcountSumAmount(beginAmountSQL);

                    decimal nev = 0;//年初始值

                    if (subjectsDetails.Trim().Length > 0 && FromTBName.Trim().Length > 0 && FileName.Trim().Length > 0)
                    {
                        nev = GetSubjectsBeginDetailAmount(MyList[j].ToString(), CurryTypeID, CompanyCD, subjectsDetails, FromTBName, FileName);
                    }
                    else
                    {
                        nev = GetBeginCurryTypeAmount(MyList[j].ToString(), CurryTypeID, CompanyCD);
                    }
                    BeginAmount += nev;

                    DataRow roww = sourceDT.NewRow();
                    roww["AttestBillID"] = "";
                    roww["AccountDate"] = "";
                    roww["AttestBillNo"] = "";
                    roww["Subjects"] = MyList[j].ToString();
                    roww["SubjectsCD"] = VoucherDBHelper.GetSubjectsName(MyList[j].ToString(), CompanyCD);
                    roww["Abstract"] = "上期结余";
                    roww["ThisDebit"] = "";
                    roww["ThisCredit"] = "";
                    roww["Direction"] = DirectionSource(MyList[j].ToString(), BeginAmount);
                    roww["ENDAmount"] = Math.Round(BeginAmount, 2).ToString("#,###0.#0");
                    roww["ByOrder"] = "1";
                    sourceDT.Rows.Add(roww);



                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select CONVERT(VARCHAR(10),AccountDate,21) as AccountDate ,a.SubjectsCD,");
                    sql.AppendLine("a.SubjectsDetails,a.AttestBillID,a.BeginAmount,a.ForeignBeginAmount,");
                    sql.AppendLine("a.ThisDebit,a.ThisCredit,a.EndAmount,a.ForeignThisDebit,a.ForeignThisCredit,a.ForeignEndAmount,case when a.Abstract is null then '' when a.Abstract is not null then a.Abstract end  as AbstractName,  ");
                    sql.AppendLine("a.FormTBName,a.FileName,a.AttestBillNo ");
                    sql.AppendLine(" from Officedba.AcountBook a {0}");


                    string selectSQL = string.Format(sql.ToString(), queryStr.Trim().Length > 0 ? " where " + queryStr : "");

                    DataTable dt = SqlHelper.ExecuteSql(selectSQL);

                    decimal nowDebit = 0;
                    decimal nowCredit = 0;
                    decimal nowEndAmount = 0;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (CurryTypeID.LastIndexOf(",") == -1)
                        {
                            nowDebit += Convert.ToDecimal(dt.Rows[i]["ForeignThisDebit"].ToString());
                            nowCredit += Convert.ToDecimal(dt.Rows[i]["ForeignThisCredit"].ToString());
                        }
                        else
                        {
                            nowDebit += Convert.ToDecimal(dt.Rows[i]["ThisDebit"].ToString());
                            nowCredit += Convert.ToDecimal(dt.Rows[i]["ThisCredit"].ToString());
                        }

                        switch (direction)
                        {
                            case 0:
                                nowEndAmount = BeginAmount + nowDebit - nowCredit;
                                break;
                            case 1:
                                nowEndAmount = BeginAmount - nowDebit + nowCredit;
                                break;
                            default:
                                break;
                        }

                        DataRow row = sourceDT.NewRow();
                        row["AttestBillID"] = dt.Rows[i]["AttestBillID"].ToString();
                        row["AccountDate"] = dt.Rows[i]["AccountDate"].ToString();
                        row["AttestBillNo"] = dt.Rows[i]["AttestBillNo"].ToString();
                        row["Subjects"] = "";
                        row["SubjectsCD"] = VoucherDBHelper.GetSubJectsName(dt.Rows[i]["SubjectsCD"].ToString(), dt.Rows[i]["SubjectsDetails"].ToString(), dt.Rows[i]["FormTBName"].ToString(), dt.Rows[i]["FileName"].ToString(), CompanyCD);
                        row["Abstract"] = dt.Rows[i]["AbstractName"].ToString();
                        if (CurryTypeID.LastIndexOf(",") == -1)
                        {
                            row["ThisDebit"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["ForeignThisDebit"].ToString()), 2).ToString("#,###0.#0");
                            row["ThisCredit"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["ForeignThisCredit"].ToString()), 2).ToString("#,###0.#0");
                        }
                        else
                        {
                            row["ThisDebit"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["ThisDebit"].ToString()), 2).ToString("#,###0.#0");
                            row["ThisCredit"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["ThisCredit"].ToString()), 2).ToString("#,###0.#0");
                        }
                        row["Direction"] = DirectionSource(MyList[j].ToString(), nowEndAmount);
                        row["ENDAmount"] = Math.Round(nowEndAmount, 2).ToString("#,###0.#0");
                        row["ByOrder"] = "1";
                        sourceDT.Rows.Add(row);
                    }

                    decimal amount = 0;
                    decimal ThisDebitTotal = 0;
                    decimal ThisCreditTotal = 0;




                    DataTable totalDT = GetAcountSumAmount(queryString);
                    DataRow row1 = sourceDT.NewRow();
                    row1["AttestBillID"] = "";
                    row1["AccountDate"] = "";
                    row1["AttestBillNo"] = "";
                    row1["Subjects"] = "";
                    row1["SubjectsCD"] = "";
                    row1["Abstract"] = "本期合计";
                    ThisDebitTotal = Convert.ToDecimal(totalDT.Rows[0]["ThisDebit"].ToString());
                    ThisCreditTotal = Convert.ToDecimal(totalDT.Rows[0]["ThisCredit"].ToString());
                    row1["ThisDebit"] = Math.Round(ThisDebitTotal, 2).ToString("#,###0.#0");
                    row1["ThisCredit"] = Math.Round(ThisCreditTotal, 2).ToString("#,###0.#0");

                    switch (direction)
                    {
                        case 0:
                            amount = BeginAmount + ThisDebitTotal - ThisCreditTotal;
                            break;
                        case 1:
                            amount = BeginAmount - ThisDebitTotal + ThisCreditTotal;
                            break;
                        default:
                            break;
                    }
                    row1["Direction"] = DirectionSource(MyList[j].ToString(), amount);
                    row1["ENDAmount"] = Math.Round(amount, 2).ToString("#,###0.#0");
                    row1["ByOrder"] = "1";
                    sourceDT.Rows.Add(row1);




                    DataTable YearTotal = GetAcountSumAmount(totalQueryStr);
                    decimal YearDebit = 0;
                    decimal YearCredit = 0;
                    YearDebit = Convert.ToDecimal(YearTotal.Rows[0]["ThisDebit"].ToString());
                    YearCredit = Convert.ToDecimal(YearTotal.Rows[0]["ThisCredit"].ToString());

                    DataRow row2 = sourceDT.NewRow();
                    row2["AttestBillID"] = "";
                    row2["AccountDate"] = "";
                    row2["AttestBillNo"] = "";
                    row2["Subjects"] = "";
                    row2["SubjectsCD"] = "";
                    row2["Abstract"] = "当前累计";
                    row2["ThisDebit"] = Math.Round(YearDebit, 2).ToString("#,###0.#0");
                    row2["ThisCredit"] = Math.Round(YearCredit, 2).ToString("#,###0.#0");
                    row2["Direction"] = DirectionSource(MyList[j].ToString(), amount);
                    row2["ENDAmount"] = Math.Round(amount, 2).ToString("#,###0.#0");
                    row2["ByOrder"] = "1";
                    sourceDT.Rows.Add(row2);
                }
            }
            return sourceDT;

        }

        #endregion

        #region 外币明细帐数据源
        /// <summary>
        /// 外币明细帐数据源
        /// </summary>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="subjectsCD"></param>
        /// <returns></returns>
        public static DataTable GetForeignAccountBookListSource(string begindate, string enddate, string subjectsCD, string CurryTypeID, string EndSubjectsCD, string FromTBName, string FileName, string subjectsDetails)
        {


            DataTable sourceDT = new DataTable();
            sourceDT.Columns.Add("AttestBillID");
            sourceDT.Columns.Add("AccountDate");
            sourceDT.Columns.Add("AttestBillNo");
            sourceDT.Columns.Add("Subjects");
            sourceDT.Columns.Add("SubjectsCD");
            sourceDT.Columns.Add("Abstract");

            sourceDT.Columns.Add("ThisDebit");
            sourceDT.Columns.Add("ThisCredit");
            sourceDT.Columns.Add("Direction");
            sourceDT.Columns.Add("ENDAmount");
            sourceDT.Columns.Add("CurryEx");
            sourceDT.Columns.Add("ForeignThisDebit");
            sourceDT.Columns.Add("ForeignThisCredit");
            sourceDT.Columns.Add("ForeignENDAmount");
            sourceDT.Columns.Add("ByOrder");



            string AssistantTypeQueryStr = string.Empty;//辅助核算限制条件
            string AssistantTypeQueryStrBya = string.Empty;

            if (subjectsDetails.Trim().Length > 0 && FromTBName.Trim().Length > 0 && FileName.Trim().Length > 0)
            {
                AssistantTypeQueryStr = " and  SubjectsDetails='" + subjectsDetails + "' and FormTBName='" + FromTBName + "' and FileName='" + FileName + "'";
                AssistantTypeQueryStrBya = " and  a.SubjectsDetails='" + subjectsDetails + "' and a.FormTBName='" + FromTBName + "' and a.FileName='" + FileName + "'";
            }

            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
            if (VoucherDBHelper.IsNullSubjects(companyCD))
            {

                string beginCDDT, endCDDT = "";
                beginCDDT = VoucherDBHelper.GetSubjectsNextCD(subjectsCD);
                endCDDT = VoucherDBHelper.GetSubjectsNextCD(EndSubjectsCD);
                beginCDDT = beginCDDT.Replace("'", "").Split(',')[0].ToString();
                string[] endStr = endCDDT.Replace("'", "").Split(',');
                int endCount = endStr.Length;
                endCDDT = endStr[endCount - 1].ToString();

                string distinctsql = string.Empty;
                ArrayList MyList = new ArrayList();
                if (subjectsCD.Trim().Length > 0 && EndSubjectsCD.Trim().Length <= 0)
                    MyList.Add(subjectsCD);
                else if (subjectsCD.Trim().Length <= 0 && EndSubjectsCD.Trim().Length > 0)
                {
                    distinctsql = string.Format("select distinct SubjectsCD from Officedba.AcountBook  where VoucherDate>='{0}' and VoucherDate<='{1}' and SubjectsCD<='{2}' and CompanyCD='{3}' {4} order by SubjectsCD asc", begindate, enddate, endCDDT, companyCD, AssistantTypeQueryStr);
                }
                else if (subjectsCD.Trim().Length > 0 && EndSubjectsCD.Trim().Length > 0)
                {
                    distinctsql = string.Format("select distinct SubjectsCD from Officedba.AcountBook  where VoucherDate>='{0}' and VoucherDate<='{1}' and ( SubjectsCD between '{2}' and '{3}' ) and CompanyCD='{4}' {5} order by SubjectsCD asc", begindate, enddate, beginCDDT, endCDDT, companyCD, AssistantTypeQueryStr);
                }
                else
                {
                    distinctsql = string.Format("select distinct SubjectsCD from Officedba.AcountBook  where VoucherDate>='{0}' and VoucherDate<='{1}' and CompanyCD='{2}' {3}  order by SubjectsCD asc", begindate, enddate, companyCD, AssistantTypeQueryStr);

                }
                DataTable distinctSubjectsDT = new DataTable();
                if (distinctsql.Trim().Length > 0)
                {
                    distinctSubjectsDT = SqlHelper.ExecuteSql(distinctsql);
                }
                foreach (DataRow dr in distinctSubjectsDT.Rows)
                {
                    MyList.Add(dr["SubjectsCD"].ToString());
                }






                for (int j = 0; j < MyList.Count; j++)
                {
                    string beginAmountSQL = string.Empty;
                    string queryStr = string.Empty;
                    string queryString = string.Empty;
                    string totalQueryStr = string.Empty;


                    string companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                    string CurrencyTypeIDStr = string.Empty;
                    DataTable CurrencyTypeDt = CurrTypeSettingDBHelper.GetCurrTypeByCompanyCD(companycd);
                    for (int p = 0; p < CurrencyTypeDt.Rows.Count; p++)
                    {
                        CurrencyTypeIDStr += CurrencyTypeDt.Rows[p]["ID"].ToString() + ",";

                    }
                    CurrencyTypeIDStr = CurrencyTypeIDStr.TrimEnd(new char[] { ',' });//综合本位币所有币种ID集
                    string subjectCDList = VoucherDBHelper.GetSubjectsNextCD(MyList[j].ToString());

                    //外币期初金额
                    string ForeignbeginAmountSQL = string.Format(@" VoucherDate<'{0}'  and SubjectsCD in( {1} ) and CurrencyTypeID={2} and CompanyCD='{3}' {4} ", begindate, subjectCDList, CurryTypeID, companyCD, AssistantTypeQueryStr);

                    string benweibiBeginAmountSQL = string.Format(@" VoucherDate<'{0}'  and SubjectsCD in( {1} ) and CompanyCD='{2}' {3} ", begindate, subjectCDList, companyCD, AssistantTypeQueryStr);



                    queryStr = " ( a.VoucherDate between '" + begindate + "' and '" + enddate + "' ) and a.SubjectsCD in ( " + subjectCDList + " ) and a.CurrencyTypeID=" + CurryTypeID + " and a.CompanyCD='" + companyCD + "' " + AssistantTypeQueryStrBya + "";


                    queryString = " ( VoucherDate between '" + begindate + "' and '" + enddate + "' ) and SubjectsCD in ( " + subjectCDList + " )  and CurrencyTypeID=" + CurryTypeID + "  and CompanyCD='" + companyCD + "' " + AssistantTypeQueryStr + "";


                    totalQueryStr = " VoucherDate<='" + enddate + "'  and SubjectsCD in ( " + subjectCDList + " ) and CurrencyTypeID=" + CurryTypeID + "  and CompanyCD='" + companyCD + "' " + AssistantTypeQueryStr + " ";

                    string directionSql = string.Format(@"select BlanceDire from officedba.AccountSubjects where SubjectsCD='{0}'  and CompanyCD='" + companyCD + "' ", MyList[j].ToString());
                    int direction = Convert.ToInt32(SqlHelper.ExecuteScalar(directionSql, null));



                    decimal BeginAmount = 0;//综合本位币期初金额
                    decimal ForeignBeginAmount = 0;//外币币期初金额

                    BeginAmount += GetAccountBookBeginAmount(benweibiBeginAmountSQL, MyList[j].ToString(), CurrencyTypeIDStr);
                    ForeignBeginAmount += GetAccountBookBeginAmount(ForeignbeginAmountSQL, MyList[j].ToString(), CurryTypeID);

                    decimal nev = 0;//外币年初始值
                    decimal nev1 = 0;//综合本位币年初始化金额

                    if (subjectsDetails.Trim().Length > 0 && FromTBName.Trim().Length > 0 && FileName.Trim().Length > 0)
                    {
                        nev = GetSubjectsBeginDetailAmount(MyList[j].ToString(), CurryTypeID, companycd, subjectsDetails, FromTBName, FileName);
                        nev1 = GetSubjectsBeginDetailAmount(MyList[j].ToString(), CurrencyTypeIDStr, companycd, subjectsDetails, FromTBName, FileName);
                    }
                    else
                    {
                        nev = GetBeginCurryTypeAmount(MyList[j].ToString(), CurryTypeID, companycd);
                        nev1 = GetBeginCurryTypeAmount(MyList[j].ToString(), CurrencyTypeIDStr, companycd);
                    }

                    BeginAmount += nev1;
                    ForeignBeginAmount += nev;

                    DataRow roww = sourceDT.NewRow();
                    roww["AttestBillID"] = "";
                    roww["AccountDate"] = "";
                    roww["AttestBillNo"] = "";
                    roww["Subjects"] = MyList[j].ToString();
                    roww["SubjectsCD"] = VoucherDBHelper.GetSubjectsName(MyList[j].ToString(), companycd);
                    roww["CurryEx"] = "";
                    roww["Abstract"] = "上期结余";
                    roww["ForeignThisDebit"] = "";
                    roww["ThisDebit"] = "";
                    roww["ForeignThisCredit"] = "";
                    roww["ThisCredit"] = "";
                    roww["Direction"] = DirectionSource(MyList[j].ToString(), ForeignBeginAmount);
                    roww["ForeignENDAmount"] = Math.Round(ForeignBeginAmount, 2).ToString("#,###0.#0");
                    roww["ENDAmount"] = Math.Round(BeginAmount, 2).ToString("#,###0.#0");
                    roww["ByOrder"] = "1";
                    sourceDT.Rows.Add(roww);



                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select CONVERT(VARCHAR(10),AccountDate,21) as AccountDate ,a.SubjectsCD,");
                    sql.AppendLine("a.SubjectsDetails,a.AttestBillID,a.BeginAmount,a.ForeignBeginAmount,");
                    sql.AppendLine("a.ThisDebit,a.ThisCredit,a.EndAmount,a.ForeignThisDebit,a.ForeignThisCredit,a.ForeignEndAmount,case when a.Abstract is null then '' when a.Abstract is not null then a.Abstract end  as AbstractName,  ");
                    sql.AppendLine("a.FormTBName,a.FileName,a.AttestBillNo,b.CurrencyName,a.ExchangeRate ");
                    sql.AppendLine(" from Officedba.AcountBook a ");
                    sql.AppendLine(" left outer join officedba.CurrencyTypeSetting b on a.CurrencyTypeID=b.ID {0} ");


                    string selectSQL = string.Format(sql.ToString(), queryStr.Trim().Length > 0 ? " where " + queryStr : "");

                    DataTable dt = SqlHelper.ExecuteSql(selectSQL);

                    decimal nowDebit = 0;
                    decimal nowCredit = 0;
                    decimal nowEndAmount = 0;
                    decimal ForeignnowDebit = 0;
                    decimal ForeignnowCredit = 0;
                    decimal ForeignnowEndAmount = 0;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {


                        ForeignnowDebit += Convert.ToDecimal(dt.Rows[i]["ForeignThisDebit"].ToString());
                        ForeignnowCredit += Convert.ToDecimal(dt.Rows[i]["ForeignThisCredit"].ToString());

                        nowDebit += Convert.ToDecimal(dt.Rows[i]["ThisDebit"].ToString());
                        nowCredit += Convert.ToDecimal(dt.Rows[i]["ThisCredit"].ToString());

                        switch (direction)
                        {
                            case 0:
                                nowEndAmount = BeginAmount + nowDebit - nowCredit;
                                ForeignnowEndAmount = ForeignBeginAmount + ForeignnowDebit - ForeignnowCredit;
                                break;
                            case 1:
                                nowEndAmount = BeginAmount - nowDebit + nowCredit;
                                ForeignnowEndAmount = ForeignBeginAmount - ForeignnowDebit + ForeignnowCredit;
                                break;
                            default:
                                break;
                        }

                        DataRow row = sourceDT.NewRow();
                        row["AttestBillID"] = dt.Rows[i]["AttestBillID"].ToString();
                        row["AccountDate"] = dt.Rows[i]["AccountDate"].ToString();
                        row["AttestBillNo"] = dt.Rows[i]["AttestBillNo"].ToString();
                        row["CurryEx"] = dt.Rows[i]["CurrencyName"].ToString() + "(" + dt.Rows[i]["ExchangeRate"].ToString() + ")";
                        row["Subjects"] = "";
                        row["SubjectsCD"] = VoucherDBHelper.GetSubJectsName(dt.Rows[i]["SubjectsCD"].ToString(), dt.Rows[i]["SubjectsDetails"].ToString(), dt.Rows[i]["FormTBName"].ToString(), dt.Rows[i]["FileName"].ToString(), companycd);
                        row["Abstract"] = dt.Rows[i]["AbstractName"].ToString();

                        row["ForeignThisDebit"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["ForeignThisDebit"].ToString()), 2).ToString("#,###0.#0");
                        row["ForeignThisCredit"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["ForeignThisCredit"].ToString()), 2).ToString("#,###0.#0");
                        row["ThisDebit"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["ThisDebit"].ToString()), 2).ToString("#,###0.#0");
                        row["ThisCredit"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["ThisCredit"].ToString()), 2).ToString("#,###0.#0");
                        row["Direction"] = DirectionSource(MyList[j].ToString(), ForeignnowEndAmount);
                        row["ENDAmount"] = Math.Round(nowEndAmount, 2).ToString("#,###0.#0");
                        row["ForeignENDAmount"] = Math.Round(ForeignnowEndAmount, 2).ToString("#,###0.#0");
                        row["ByOrder"] = "1";
                        sourceDT.Rows.Add(row);
                    }

                    /**2009-05-14 end tomorrow start**/

                    decimal amount = 0;
                    decimal ThisDebitTotal = 0;
                    decimal ThisCreditTotal = 0;
                    decimal Foreignamount = 0;
                    decimal ForeignThisDebitTotal = 0;
                    decimal ForeignThisCreditTotal = 0;






                    DataTable totalDT = GetAcountSumAmount(queryString);
                    DataRow row1 = sourceDT.NewRow();
                    row1["AttestBillID"] = "";
                    row1["AccountDate"] = "";
                    row1["AttestBillNo"] = "";
                    row1["Subjects"] = "";
                    row1["SubjectsCD"] = "";
                    row1["Abstract"] = "本期合计";
                    ThisDebitTotal = Convert.ToDecimal(totalDT.Rows[0]["ThisDebit"].ToString());
                    ThisCreditTotal = Convert.ToDecimal(totalDT.Rows[0]["ThisCredit"].ToString());
                    ForeignThisDebitTotal = Convert.ToDecimal(totalDT.Rows[0]["ForeignThisDebit"].ToString());
                    ForeignThisCreditTotal = Convert.ToDecimal(totalDT.Rows[0]["ForeignThisCredit"].ToString());
                    row1["ThisDebit"] = Math.Round(ThisDebitTotal, 2).ToString("#,###0.#0");
                    row1["ThisCredit"] = Math.Round(ThisCreditTotal, 2).ToString("#,###0.#0");
                    row1["CurryEx"] = "";
                    row1["ForeignThisDebit"] = Math.Round(ForeignThisDebitTotal, 2).ToString("#,###0.#0");
                    row1["ForeignThisCredit"] = Math.Round(ForeignThisCreditTotal, 2).ToString("#,###0.#0");

                    switch (direction)
                    {
                        case 0:
                            amount = BeginAmount + ThisDebitTotal - ThisCreditTotal;
                            Foreignamount = ForeignBeginAmount + ForeignThisDebitTotal - ForeignThisCreditTotal;
                            break;
                        case 1:
                            amount = BeginAmount - ThisDebitTotal + ThisCreditTotal;
                            Foreignamount = ForeignBeginAmount - ForeignThisDebitTotal + ForeignThisCreditTotal;
                            break;
                        default:
                            break;
                    }
                    row1["Direction"] = DirectionSource(MyList[j].ToString(), Foreignamount);
                    row1["ENDAmount"] = Math.Round(amount, 2).ToString("#,###0.#0");
                    row1["ForeignENDAmount"] = Math.Round(Foreignamount, 2).ToString("#,###0.#0");
                    row1["ByOrder"] = "1";
                    sourceDT.Rows.Add(row1);




                    DataTable YearTotal = GetAcountSumAmount(totalQueryStr);
                    decimal YearDebit = 0;
                    decimal YearCredit = 0;
                    decimal ForeignYearDebit = 0;
                    decimal ForeignYearCredit = 0;
                    YearDebit = Convert.ToDecimal(YearTotal.Rows[0]["ThisDebit"].ToString());
                    YearCredit = Convert.ToDecimal(YearTotal.Rows[0]["ThisCredit"].ToString());
                    ForeignYearDebit = Convert.ToDecimal(YearTotal.Rows[0]["ForeignThisDebit"].ToString());
                    ForeignYearCredit = Convert.ToDecimal(YearTotal.Rows[0]["ForeignThisCredit"].ToString());

                    DataRow row2 = sourceDT.NewRow();
                    row2["AttestBillID"] = "";
                    row2["AccountDate"] = "";
                    row2["AttestBillNo"] = "";
                    row2["Subjects"] = "";
                    row2["SubjectsCD"] = "";
                    row2["CurryEx"] = "";
                    row2["Abstract"] = "当前累计";
                    row2["ThisDebit"] = Math.Round(YearDebit, 2).ToString("#,###0.#0");
                    row2["ThisCredit"] = Math.Round(YearCredit, 2).ToString("#,###0.#0");
                    row2["ForeignThisDebit"] = Math.Round(ForeignYearDebit, 2).ToString("#,###0.#0");
                    row2["ForeignThisCredit"] = Math.Round(ForeignYearCredit, 2).ToString("#,###0.#0");
                    row2["Direction"] = DirectionSource(MyList[j].ToString(), Foreignamount);
                    row2["ENDAmount"] = Math.Round(amount, 2).ToString("#,###0.#0");
                    row2["ForeignENDAmount"] = Math.Round(Foreignamount, 2).ToString("#,###0.#0");
                    row2["ByOrder"] = "1";
                    sourceDT.Rows.Add(row2);
                }
            }

            return sourceDT;

        }

        #endregion

        #region 根据期末余额和会计科目来判断期末余额方向
        /// <summary>
        /// 根据期末余额和会计科目来判断期末余额方向
        /// </summary>
        /// <param name="subjectsCD"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string DirectionSource(string subjectsCD, decimal money)
        {
            string DIREC = string.Empty;
            string directionSql = string.Format(@"select BlanceDire from officedba.AccountSubjects where SubjectsCD='{0}'", subjectsCD);
            int direction = Convert.ToInt32(SqlHelper.ExecuteScalar(directionSql, null));
            switch (direction)
            {
                case 0:
                    if (money > 0)
                    {
                        DIREC = "借";
                    }
                    else if (money == 0)
                    {
                        DIREC = "平";
                    }
                    else
                    {
                        DIREC = "贷";
                    }
                    break;
                case 1:
                    if (money > 0)
                    {
                        DIREC = "贷";
                    }
                    else if (money == 0)
                    {
                        DIREC = "平";
                    }
                    else
                    {
                        DIREC = "借";
                    }
                    break;
                default:
                    break;
            }
            return DIREC;
        }
        #endregion

        #region 总分类帐数据源(人民币或综合本位币)
        /// <summary>
        /// 总分类帐数据源(人民币或综合本位币)
        /// </summary>
        /// <param name="beginTime">开始日期</param>
        /// <param name="EndTime">结束日期</param>
        /// <param name="SubjectsCD">科目编码</param>
        /// <returns></returns>
        public static DataTable GetAcountBookTotalSource(string beginTime, string EndTime, string SubjectsCD, string CurryTypeID, string CompanyCD, string EndSubjectsCD, string FromTBName, string FileName, string subjectsDetails)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Date");
            dt.Columns.Add("SubjectsCD");
            dt.Columns.Add("SubjectsName");
            dt.Columns.Add("Abstract");
            dt.Columns.Add("SumDebit");
            dt.Columns.Add("SumCredit");
            dt.Columns.Add("Direction");
            dt.Columns.Add("EndAmount");

            string AssistantTypeQueryStr = string.Empty;//辅助核算限制条件
            if (subjectsDetails.Trim().Length > 0 && FromTBName.Trim().Length > 0 && FileName.Trim().Length > 0)
            {
                AssistantTypeQueryStr = " and  SubjectsDetails='" + subjectsDetails + "' and FormTBName='" + FromTBName + "' and FileName='" + FileName + "'";
            }

            if (VoucherDBHelper.IsNullSubjects(CompanyCD))
            {
                string[] start = beginTime.Split('-');
                string[] end = EndTime.Split('-');
                string value = string.Empty;
                if (start[1].ToString() == end[1].ToString())
                {
                    value = start[0].ToString() + "年" + start[1].ToString() + "期";
                }
                if (start[1].ToString() != end[1].ToString())
                {
                    value = start[0].ToString() + "年" + start[1].ToString() + "至" + end[0].ToString() + "年" + end[1].ToString() + "期";
                }



                string distinctsql = string.Empty;

                string beginCDDT, endCDDT = "";
                beginCDDT = VoucherDBHelper.GetSubjectsNextCD(SubjectsCD);
                endCDDT = VoucherDBHelper.GetSubjectsNextCD(EndSubjectsCD);
                beginCDDT = beginCDDT.Replace("'", "").Split(',')[0].ToString();
                string[] endStr = endCDDT.Replace("'", "").Split(',');
                int endCount = endStr.Length;
                endCDDT = endStr[endCount - 1].ToString();


                ArrayList MyList = new ArrayList();
                if ((SubjectsCD.Trim().Length > 0 && EndSubjectsCD.Trim().Length <= 0) || (SubjectsCD.Equals(EndSubjectsCD) && !string.IsNullOrEmpty(SubjectsCD) && !string.IsNullOrEmpty(EndSubjectsCD)))
                    MyList.Add(SubjectsCD);
                else if (SubjectsCD.Trim().Length <= 0 && EndSubjectsCD.Trim().Length > 0)
                {
                    distinctsql = string.Format("select distinct SubjectsCD from Officedba.AcountBook  where VoucherDate>='{0}' and VoucherDate<='{1}' and SubjectsCD<='{2}' and CompanyCD='{3}' {4}  order by SubjectsCD asc", beginTime, EndTime, endCDDT, CompanyCD, AssistantTypeQueryStr);
                }
                else if (SubjectsCD.Trim().Length > 0 && EndSubjectsCD.Trim().Length > 0 && !SubjectsCD.Equals(EndSubjectsCD))
                {
                    distinctsql = string.Format("select distinct SubjectsCD from Officedba.AcountBook  where VoucherDate>='{0}' and VoucherDate<='{1}' and ( SubjectsCD between '{2}' and '{3}' ) and CompanyCD='{4}' {5} order by SubjectsCD asc", beginTime, EndTime, beginCDDT, endCDDT, CompanyCD, AssistantTypeQueryStr);
                }
                else
                {
                    distinctsql = string.Format("select distinct SubjectsCD from Officedba.AcountBook  where VoucherDate>='{0}' and VoucherDate<='{1}'  and CompanyCD='{2}' {3} order by SubjectsCD asc", beginTime, EndTime, CompanyCD, AssistantTypeQueryStr);

                }
                DataTable distinctSubjectsDT = new DataTable();
                if (distinctsql.Trim().Length > 0)
                {
                    distinctSubjectsDT = SqlHelper.ExecuteSql(distinctsql);
                }
                foreach (DataRow dr in distinctSubjectsDT.Rows)
                {
                    MyList.Add(dr["SubjectsCD"].ToString());
                }

                for (int i = 0; i < MyList.Count; i++)
                {
                    string directionSql = string.Format(@"select BlanceDire from officedba.AccountSubjects where SubjectsCD='{0}' and CompanyCD='{1}' ", MyList[i].ToString(), CompanyCD);
                    int direction = Convert.ToInt32(SqlHelper.ExecuteScalar(directionSql, null));
                    string subjectCDList = VoucherDBHelper.GetSubjectsNextCD(MyList[i].ToString());

                    string beginQueryStr = string.Empty;
                    string thisQueryStr = string.Empty;
                    string EndQueryStr = string.Empty;

                    if (CurryTypeID.LastIndexOf(",") == -1)//人民币
                    {
                        beginQueryStr = string.Format(@"VoucherDate<'{0}'  and SubjectsCD in ( {1} ) and CurrencyTypeID={2}  and CompanyCD='" + CompanyCD + "' " + AssistantTypeQueryStr + " ", beginTime, subjectCDList, CurryTypeID);
                        //人民币期初金额
                        thisQueryStr = " ( VoucherDate between '" + beginTime + "' and '" + EndTime + "' ) and SubjectsCD in ( " + subjectCDList + " )  and CurrencyTypeID=" + CurryTypeID + " and CompanyCD='" + CompanyCD + "' " + AssistantTypeQueryStr + " ";
                        EndQueryStr = " VoucherDate<='" + EndTime + "' and SubjectsCD in ( " + subjectCDList + " ) and CurrencyTypeID=" + CurryTypeID + " and CompanyCD='" + CompanyCD + "' " + AssistantTypeQueryStr + " ";
                    }
                    else//综合本位币
                    {
                        beginQueryStr = string.Format(@" VoucherDate<'{0}'  and SubjectsCD in ( {1} ) and CompanyCD='" + CompanyCD + "' " + AssistantTypeQueryStr + " ", beginTime, subjectCDList);
                        thisQueryStr = " ( VoucherDate between '" + beginTime + "' and '" + EndTime + "' ) and SubjectsCD in ( " + subjectCDList + " )  and CompanyCD='" + CompanyCD + "' " + AssistantTypeQueryStr + " ";
                        EndQueryStr = " VoucherDate<='" + EndTime + "' and SubjectsCD in ( " + subjectCDList + " ) and CompanyCD='" + CompanyCD + "' " + AssistantTypeQueryStr + " ";
                    }

                    decimal begingAmount = 0;
                    if (subjectsDetails.Trim().Length > 0 && FromTBName.Trim().Length > 0 && FileName.Trim().Length > 0)
                    {
                        begingAmount += GetSubjectsBeginDetailAmount(MyList[i].ToString(), CurryTypeID, CompanyCD, subjectsDetails, FromTBName, FileName);
                    }
                    else
                    {
                        begingAmount += GetBeginCurryTypeAmount(MyList[i].ToString(), CurryTypeID, CompanyCD);
                    }
                    begingAmount += GetAccountBookBeginAmount(beginQueryStr, MyList[i].ToString(), CurryTypeID);

                    DataRow row = dt.NewRow();
                    row["ID"] = "1";
                    row["Date"] = value;
                    row["SubjectsCD"] = MyList[i].ToString();
                    row["SubjectsName"] = VoucherDBHelper.GetSubjectsName(MyList[i].ToString(), CompanyCD);
                    row["Abstract"] = "上期结余";
                    row["SumDebit"] = "";
                    row["SumCredit"] = "";
                    row["Direction"] = DirectionSource(MyList[i].ToString(), begingAmount);
                    row["EndAmount"] = Math.Round(begingAmount, 2).ToString("#,###0.#0");//GetAcountBookSumAmount(beginQueryStr, SubjectsCD, direction).ToString();
                    dt.Rows.Add(row);

                    DataRow row1 = dt.NewRow();
                    row1["ID"] = "1";
                    row1["Date"] = "";
                    row1["SubjectsCD"] = "";
                    row1["SubjectsName"] = "";
                    row1["Abstract"] = "本期合计";
                    DataTable thisDT = GetAcountSumAmount(thisQueryStr);
                    if (CurryTypeID.LastIndexOf(",") == -1)
                    {
                        row1["SumDebit"] = Math.Round(Convert.ToDecimal(thisDT.Rows[0]["ForeignThisDebit"].ToString()), 2).ToString("#,###0.#0");
                        row1["SumCredit"] = Math.Round(Convert.ToDecimal(thisDT.Rows[0]["ForeignThisCredit"].ToString()), 2).ToString("#,###0.#0");

                    }
                    else
                    {
                        row1["SumDebit"] = Math.Round(Convert.ToDecimal(thisDT.Rows[0]["ThisDebit"].ToString()), 2).ToString("#,###0.#0");
                        row1["SumCredit"] = Math.Round(Convert.ToDecimal(thisDT.Rows[0]["ThisCredit"].ToString()), 2).ToString("#,###0.#0");
                    }
                    decimal money = 0;
                    if (subjectsDetails.Trim().Length > 0 && FromTBName.Trim().Length > 0 && FileName.Trim().Length > 0)
                    {
                        money = GetSubjectsBeginDetailAmount(MyList[i].ToString(), CurryTypeID, CompanyCD, subjectsDetails, FromTBName, FileName) + GetAccountBookBeginAmount(EndQueryStr, MyList[i].ToString(), CurryTypeID);
                    }
                    else
                    {
                        money = GetBeginCurryTypeAmount(MyList[i].ToString(), CurryTypeID, CompanyCD) + GetAccountBookBeginAmount(EndQueryStr, MyList[i].ToString(), CurryTypeID);
                    }
                    row1["EndAmount"] = Math.Round(money, 2).ToString("#,###0.#0");
                    row1["Direction"] = DirectionSource(MyList[i].ToString(), money);
                    dt.Rows.Add(row1);

                    DataRow row3 = dt.NewRow();
                    row3["ID"] = "1";
                    row3["Date"] = "";
                    row3["SubjectsCD"] = "";
                    row3["SubjectsName"] = "";
                    row3["Abstract"] = "当前累计";
                    row3["SumDebit"] = "";
                    row3["SumCredit"] = "";
                    row3["Direction"] = DirectionSource(MyList[i].ToString(), money);
                    row3["EndAmount"] = Math.Round(money, 2).ToString("#,###0.#0");
                    dt.Rows.Add(row3);
                }
            }
            return dt;

        }

        #endregion

        #region 总分类帐数据源(不是本币或本位币的外币总帐)
        /// <summary>
        /// 总分类帐数据源(不是本币或本位币的外币总帐)
        /// </summary>
        /// <param name="beginTime">开始日期</param>
        /// <param name="EndTime">结束日期</param>
        /// <param name="SubjectsCD">科目编码</param>
        /// <returns></returns>
        public static DataTable GetForeignAcountBookTotalSource(string beginTime, string EndTime, string SubjectsCD, string CurryTypeID, string EndSubjectsCD, string FromTBName, string FileName, string subjectsDetails)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Date");
            dt.Columns.Add("SubjectsCD");
            dt.Columns.Add("SubjectsName");
            dt.Columns.Add("Abstract");
            dt.Columns.Add("SumDebit");
            dt.Columns.Add("SumCredit");
            dt.Columns.Add("Direction");
            dt.Columns.Add("EndAmount");
            dt.Columns.Add("ForeignSumDebit");
            dt.Columns.Add("ForeignSumCredit");
            dt.Columns.Add("ForeignEndAmount");


            string AssistantTypeQueryStr = string.Empty;//辅助核算限制条件
            if (subjectsDetails.Trim().Length > 0 && FromTBName.Trim().Length > 0 && FileName.Trim().Length > 0)
            {
                AssistantTypeQueryStr = " and  SubjectsDetails='" + subjectsDetails + "' and FormTBName='" + FromTBName + "' and FileName='" + FileName + "'";
            }


            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
            if (VoucherDBHelper.IsNullSubjects(companyCD))
            {


                string[] start = beginTime.Split('-');
                string[] end = EndTime.Split('-');
                string value = string.Empty;
                if (start[1].ToString() == end[1].ToString())
                {
                    value = start[0].ToString() + "年" + start[1].ToString() + "期";
                }
                if (start[1].ToString() != end[1].ToString())
                {
                    value = start[0].ToString() + "年" + start[1].ToString() + "至" + end[0].ToString() + "年" + end[1].ToString() + "期";
                }






                string distinctsql = string.Empty;
                string beginCDDT, endCDDT = "";
                beginCDDT = VoucherDBHelper.GetSubjectsNextCD(SubjectsCD);
                endCDDT = VoucherDBHelper.GetSubjectsNextCD(EndSubjectsCD);
                beginCDDT = beginCDDT.Replace("'", "").Split(',')[0].ToString();
                string[] endStr = endCDDT.Replace("'", "").Split(',');
                int endCount = endStr.Length;
                endCDDT = endStr[endCount - 1].ToString();

                ArrayList MyList = new ArrayList();
                if ((SubjectsCD.Trim().Length > 0 && EndSubjectsCD.Trim().Length <= 0) || (SubjectsCD.Equals(EndSubjectsCD) && !string.IsNullOrEmpty(SubjectsCD) && !string.IsNullOrEmpty(EndSubjectsCD)))
                    MyList.Add(SubjectsCD);
                else if (SubjectsCD.Trim().Length <= 0 && EndSubjectsCD.Trim().Length > 0)
                {
                    distinctsql = string.Format("select distinct SubjectsCD from Officedba.AcountBook  where VoucherDate>='{0}' and VoucherDate<='{1}' and SubjectsCD<='{2}' and CompanyCD='" + companyCD + "' {3}  order by SubjectsCD asc", beginTime, EndTime, endCDDT, AssistantTypeQueryStr);
                }
                else if (SubjectsCD.Trim().Length > 0 && EndSubjectsCD.Trim().Length > 0 && !SubjectsCD.Equals(EndSubjectsCD))
                {
                    distinctsql = string.Format("select distinct SubjectsCD from Officedba.AcountBook  where VoucherDate>='{0}' and VoucherDate<='{1}' and ( SubjectsCD between '{2}' and '{3}' )  and CompanyCD='" + companyCD + "' {4} order by SubjectsCD asc", beginTime, EndTime, beginCDDT, endCDDT, AssistantTypeQueryStr);
                }
                else
                {
                    distinctsql = string.Format("select distinct SubjectsCD from Officedba.AcountBook  where VoucherDate>='{0}' and VoucherDate<='{1}'and CompanyCD='" + companyCD + "'  {2} order by SubjectsCD asc", beginTime, EndTime, AssistantTypeQueryStr);

                }
                DataTable distinctSubjectsDT = new DataTable();
                if (distinctsql.Trim().Length > 0)
                {
                    distinctSubjectsDT = SqlHelper.ExecuteSql(distinctsql);
                }
                foreach (DataRow dr in distinctSubjectsDT.Rows)
                {
                    MyList.Add(dr["SubjectsCD"].ToString());
                }

                for (int i = 0; i < MyList.Count; i++)
                {

                    string directionSql = string.Format(@"select BlanceDire from officedba.AccountSubjects where SubjectsCD='{0}' and CompanyCD='" + companyCD + "' ", MyList[i].ToString());
                    int direction = Convert.ToInt32(SqlHelper.ExecuteScalar(directionSql, null));
                    string subjectCDList = VoucherDBHelper.GetSubjectsNextCD(MyList[i].ToString());


                    string companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                    string CurrencyTypeIDStr = string.Empty;
                    DataTable CurrencyTypeDt = CurrTypeSettingDBHelper.GetCurrTypeByCompanyCD(companycd);
                    for (int p = 0; p < CurrencyTypeDt.Rows.Count; p++)
                    {
                        CurrencyTypeIDStr += CurrencyTypeDt.Rows[p]["ID"].ToString() + ",";

                    }
                    CurrencyTypeIDStr = CurrencyTypeIDStr.TrimEnd(new char[] { ',' });//综合本位币所有币种ID集



                    string beginQueryStr = string.Empty;
                    string benweibiBeginQueryStr = string.Empty;
                    string thisQueryStr = string.Empty;
                    string EndQueryStr = string.Empty;

                    beginQueryStr = string.Format(@"VoucherDate<'{0}'  and SubjectsCD in ( {1} ) and CurrencyTypeID={2} and CompanyCD='" + companycd + "' {3} ", beginTime, subjectCDList, CurryTypeID, AssistantTypeQueryStr);//外币期初金额

                    benweibiBeginQueryStr = string.Format(@"VoucherDate<'{0}'  and SubjectsCD in ( {1} ) and CompanyCD='" + companycd + "' {2} ", beginTime, subjectCDList, AssistantTypeQueryStr);//本位币期初金额




                    thisQueryStr = " ( VoucherDate between '" + beginTime + "' and '" + EndTime + "' ) and SubjectsCD in ( " + subjectCDList + " )  and CurrencyTypeID=" + CurryTypeID + " and CompanyCD='" + companycd + "'  " + AssistantTypeQueryStr + " ";

                    EndQueryStr = " VoucherDate<='" + EndTime + "' and SubjectsCD in ( " + subjectCDList + " ) and CurrencyTypeID=" + CurryTypeID + "  and CompanyCD='" + companycd + "' " + AssistantTypeQueryStr + " ";


                    decimal BeginAmount = 0;
                    decimal ForeignBeginAmount = 0;
                    decimal nev = 0;//外币年初始值
                    decimal nev1 = 0;//综合本位币年初始化金额

                    if (subjectsDetails.Trim().Length > 0 && FromTBName.Trim().Length > 0 && FileName.Trim().Length > 0)
                    {
                        BeginAmount += GetSubjectsBeginDetailAmount(MyList[i].ToString(), CurrencyTypeIDStr, companycd, subjectsDetails, FromTBName, FileName);//综合本位币期初初始化金额

                        ForeignBeginAmount += GetSubjectsBeginDetailAmount(MyList[i].ToString(), CurryTypeID, companycd, subjectsDetails, FromTBName, FileName);//外币期初初始化金额

                    }
                    else
                    {
                        BeginAmount += GetBeginCurryTypeAmount(MyList[i].ToString(), CurrencyTypeIDStr, companycd);//综合本位币期初初始化金额

                        ForeignBeginAmount += GetBeginCurryTypeAmount(MyList[i].ToString(), CurryTypeID, companycd);//外币期初初始化金额
                    }

                    nev += GetAccountBookBeginAmount(beginQueryStr, MyList[i].ToString(), CurryTypeID);

                    nev1 += GetAccountBookBeginAmount(benweibiBeginQueryStr, MyList[i].ToString(), CurrencyTypeIDStr);


                    BeginAmount += nev1;
                    ForeignBeginAmount += nev;

                    DataRow row = dt.NewRow();
                    row["ID"] = "1";
                    row["Date"] = value;
                    row["SubjectsCD"] = MyList[i].ToString();
                    row["SubjectsName"] = VoucherDBHelper.GetSubjectsName(MyList[i].ToString(), companycd);
                    row["Abstract"] = "上期结余";
                    row["ForeignSumDebit"] = "";
                    row["SumDebit"] = "";
                    row["ForeignSumCredit"] = "";
                    row["SumCredit"] = "";
                    row["Direction"] = DirectionSource(MyList[i].ToString(), ForeignBeginAmount);
                    row["ForeignEndAmount"] = Math.Round(ForeignBeginAmount, 2).ToString("#,###0.#0");
                    row["EndAmount"] = Math.Round(BeginAmount, 2).ToString("#,###0.#0");//GetAcountBookSumAmount(beginQueryStr, SubjectsCD, direction).ToString();
                    dt.Rows.Add(row);

                    DataRow row1 = dt.NewRow();
                    row1["ID"] = "1";
                    row1["Date"] = "";
                    row1["SubjectsCD"] = "";
                    row1["SubjectsName"] = "";
                    row1["Abstract"] = "本期合计";
                    DataTable thisDT = GetAcountSumAmount(thisQueryStr);

                    row1["ForeignSumDebit"] = Math.Round(Convert.ToDecimal(thisDT.Rows[0]["ForeignThisDebit"].ToString()), 2).ToString("#,###0.#0");
                    row1["ForeignSumCredit"] = Math.Round(Convert.ToDecimal(thisDT.Rows[0]["ForeignThisCredit"].ToString()), 2).ToString("#,###0.#0");
                    row1["SumDebit"] = Math.Round(Convert.ToDecimal(thisDT.Rows[0]["ThisDebit"].ToString()), 2).ToString("#,###0.#0");
                    row1["SumCredit"] = Math.Round(Convert.ToDecimal(thisDT.Rows[0]["ThisCredit"].ToString()), 2).ToString("#,###0.#0");

                    decimal EndAmount = 0;
                    decimal ForeignEndAmount = 0;
                    decimal Endnev = 0;//外币年初始值
                    decimal Endnev1 = 0;//综合本位币年初始化金额


                    if (subjectsDetails.Trim().Length > 0 && FromTBName.Trim().Length > 0 && FileName.Trim().Length > 0)
                    {
                        EndAmount += GetSubjectsBeginDetailAmount(MyList[i].ToString(), CurrencyTypeIDStr, companycd, subjectsDetails, FromTBName, FileName);//综合本位币期初初始化金额

                        ForeignEndAmount += GetSubjectsBeginDetailAmount(MyList[i].ToString(), CurryTypeID, companycd, subjectsDetails, FromTBName, FileName);//外币期初初始化金额
                    }
                    else
                    {
                        EndAmount += GetBeginCurryTypeAmount(MyList[i].ToString(), CurrencyTypeIDStr, companycd);//综合本位币期初初始化金额

                        ForeignEndAmount += GetBeginCurryTypeAmount(MyList[i].ToString(), CurryTypeID, companycd);//外币期初初始化金额
                    }

                    Endnev += GetAccountBookBeginAmount(EndQueryStr, MyList[i].ToString(), CurryTypeID);

                    Endnev1 += GetAccountBookBeginAmount(EndQueryStr, MyList[i].ToString(), CurrencyTypeIDStr);

                    EndAmount += Endnev1;
                    ForeignEndAmount += Endnev;




                    row1["ForeignEndAmount"] = Math.Round(ForeignEndAmount, 2).ToString("#,###0.#0");
                    row1["EndAmount"] = Math.Round(EndAmount, 2).ToString("#,###0.#0");

                    row1["Direction"] = DirectionSource(MyList[i].ToString(), ForeignEndAmount);
                    dt.Rows.Add(row1);


                    DataRow row3 = dt.NewRow();
                    row3["ID"] = "1";
                    row3["Date"] = "";
                    row3["SubjectsCD"] = "";
                    row3["SubjectsName"] = "";
                    row3["Abstract"] = "当前累计";
                    row3["ForeignSumDebit"] = "";
                    row3["SumDebit"] = "";
                    row3["ForeignSumCredit"] = "";
                    row3["SumCredit"] = "";
                    row3["Direction"] = DirectionSource(MyList[i].ToString(), ForeignEndAmount);
                    row3["ForeignEndAmount"] = Math.Round(ForeignEndAmount, 2).ToString("#,###0.#0");
                    row3["EndAmount"] = Math.Round(EndAmount, 2).ToString("#,###0.#0");
                    dt.Rows.Add(row3);
                }
            }
            return dt;

        }

        #endregion

        #region 根据查询条件获取本期借贷方发生的金额总和
        /// <summary>
        /// 根据查询条件获取本期借贷方发生的金额总和
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static DataTable GetAcountSumAmount(string queryStr)
        {

            string sql = string.Format("select ISNULL(SUM(ThisDebit),0) as ThisDebit, ISNULL(SUM(ThisCredit),0) as ThisCredit,ISNULL(SUM(ForeignThisDebit),0) as ForeignThisDebit, ISNULL(SUM(ForeignThisCredit),0) as ForeignThisCredit from Officedba.AcountBook {0} ", queryStr.Trim().Length > 0 ? " where " + queryStr : "");

            return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region 统计账簿金额期初金额
        /// <summary>
        /// 统计账簿金额期初金额
        /// </summary>
        /// <param name="queryStr">查询条件</param>
        /// <param name="subjectsCD">科目</param>
        /// <param name="CurryTypeID">币种</param>
        /// <returns></returns>
        public static decimal GetAccountBookBeginAmount(string queryStr, string subjectsCD, string CurryTypeID)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
            decimal BeginAmount = 0;
            //科目方向
            string directionSql = string.Format(@"select BlanceDire from officedba.AccountSubjects where SubjectsCD='{0}' and CompanyCD='{1}'", subjectsCD, companyCD);
            int direction = Convert.ToInt32(SqlHelper.ExecuteScalar(directionSql, null));
            //金额统计SQL
            string sql = string.Format("select ISNULL(SUM(ThisDebit),0) as ThisDebit, ISNULL(SUM(ThisCredit),0) as ThisCredit,ISNULL(SUM(ForeignThisDebit),0) as ForeignThisDebit, ISNULL(SUM(ForeignThisCredit),0) as ForeignThisCredit from Officedba.AcountBook {0} ", queryStr.Trim().Length > 0 ? " where " + queryStr : "");
            DataTable beginDT = SqlHelper.ExecuteSql(sql);

            if (beginDT.Rows.Count > 0)
            {
                for (int i = 0; i < beginDT.Rows.Count; i++)
                {
                    if (CurryTypeID.LastIndexOf(",") == -1)//外币合计
                    {
                        switch (direction)
                        {
                            case 0: BeginAmount = Convert.ToDecimal(beginDT.Rows[i]["ForeignThisDebit"].ToString()) - Convert.ToDecimal(beginDT.Rows[i]["ForeignThisCredit"].ToString());
                                break;
                            case 1: BeginAmount = Convert.ToDecimal(beginDT.Rows[i]["ForeignThisCredit"].ToString()) - Convert.ToDecimal(beginDT.Rows[i]["ForeignThisDebit"].ToString());
                                break;
                            default:
                                break;
                        }
                    }
                    else//综合本位币合计
                    {
                        switch (direction)
                        {
                            case 0: BeginAmount = Convert.ToDecimal(beginDT.Rows[i]["ThisDebit"].ToString()) - Convert.ToDecimal(beginDT.Rows[i]["ThisCredit"].ToString());
                                break;
                            case 1: BeginAmount = Convert.ToDecimal(beginDT.Rows[i]["ThisCredit"].ToString()) - Convert.ToDecimal(beginDT.Rows[i]["ThisDebit"].ToString());
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return BeginAmount;
        }

        #endregion

        #region 获取会计科目的初始化金额
        public static decimal GetBeginCurryTypeAmount(string subjectsCD, string CurryTypeID, string CompanyCD)
        {
            decimal nev = 0;//年初始值
            string subjectCDList = VoucherDBHelper.GetSubjectsNextCD(subjectsCD);
            //根据币种ID获取年初始值开始
            //  string Sql = string.Empty;
            StringBuilder sql = null;
            if (CurryTypeID.LastIndexOf(",") == -1)
            {
                sql = new StringBuilder();
                sql.AppendLine("select isnull(sum(isnull(BeginMoney,0)),0) as BeginAmount");
                sql.AppendLine(" from officedba.SubjectsBeginDetails ");
                sql.AppendLine(" where SubjectsCD in(" + subjectCDList + ") ");
                sql.AppendLine(" and CurrencyType =" + CurryTypeID + " ");
                sql.AppendLine(" and CompanyCD=@CompanyCD ");
            }
            else
            {
                sql = new StringBuilder();
                sql.AppendLine("select isnull(sum(isnull(SumOriginalCurrency,0)),0) as BeginAmount");
                sql.AppendLine("from officedba.SubjectsBeginDetails where SubjectsCD in (" + subjectCDList + ") ");
                sql.AppendLine(" and CompanyCD=@CompanyCD");
            }
            SqlParameter[] parms = 
           {
               new SqlParameter("@CompanyCD",CompanyCD)
           };
            DataTable defaultDt = SqlHelper.ExecuteSql(sql.ToString(), parms);
            foreach (DataRow dr in defaultDt.Rows)
            {
                nev += Convert.ToDecimal(dr["BeginAmount"].ToString());
            }

            return nev;
        }
        #endregion

        #region 获取会计科目的年初金额_资产负债表
        /// <summary>
        /// 获取会计科目的年初金额
        /// </summary>
        /// <param name="subjectsCD">科目编码</param>
        /// <param name="CurryTypeID">币种</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static decimal GetYeaeBeginAmount(string subjectsCD, string CurryTypeID, string CompanyCD)
        {
            decimal nev = 0;//年初始值
            string subjectCDList = VoucherDBHelper.GetSubjectsNextCD(subjectsCD);
            //根据币种ID获取年初始值开始
            //  string Sql = string.Empty;
            StringBuilder sql = null;
            if (CurryTypeID.LastIndexOf(",") == -1)
            {
                sql = new StringBuilder();
                sql.AppendLine("select isnull(sum(isnull(OriginalCurrency,0)),0) as BeginAmount");
                sql.AppendLine(" from officedba.SubjectsBeginDetails ");
                sql.AppendLine(" where SubjectsCD in(" + subjectCDList + ") ");
                sql.AppendLine(" and CurrencyType =" + CurryTypeID + " ");
                sql.AppendLine(" and CompanyCD=@CompanyCD ");
            }
            else
            {
                sql = new StringBuilder();
                sql.AppendLine("select isnull(sum(isnull(StandardCurrency,0)),0) as BeginAmount");
                sql.AppendLine("from officedba.SubjectsBeginDetails where SubjectsCD in (" + subjectCDList + ") ");
                sql.AppendLine(" and CompanyCD=@CompanyCD");
            }
            SqlParameter[] parms = 
           {
               new SqlParameter("@CompanyCD",CompanyCD)
           };
            DataTable defaultDt = SqlHelper.ExecuteSql(sql.ToString(), parms);
            foreach (DataRow dr in defaultDt.Rows)
            {
                nev += Convert.ToDecimal(dr["BeginAmount"].ToString());
            }

            return nev;
        }
        #endregion

        #region 获取辅助核算科目不同核算方式的科目初始化金额
        /// <summary>
        /// 获取辅助核算科目不同核算方式的科目初始化金额
        /// </summary>
        /// <param name="subjectsCD">科目编码</param>
        /// <param name="CurryTypeID">币种</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="SubjectsDetails">辅助核算项目主键</param>
        /// <param name="FromTB">辅助核算来源表</param>
        /// <param name="FileVale">辅助核算字段名</param>
        /// <returns></returns>
        public static decimal GetSubjectsBeginDetailAmount(string subjectsCD, string CurryTypeID, string CompanyCD, string SubjectsDetails, string FormTBName, string FileName)
        {
            decimal nev = 0;//年初始值
            if (SubjectsDetails.Trim().Length > 0 && FormTBName.Trim().Length > 0 && FileName.Trim().Length > 0)
            {
                string subjectCDList = VoucherDBHelper.GetSubjectsNextCD(subjectsCD);
                //根据币种ID获取年初始值开始
                //  string Sql = string.Empty;
                StringBuilder sql = null;
                if (CurryTypeID.LastIndexOf(",") == -1)
                {
                    sql = new StringBuilder();
                    sql.AppendLine("select isnull(sum(isnull(BeginMoney,0)),0) as BeginAmount");
                    sql.AppendLine(" from officedba.SubjectsBeginDetails ");
                    sql.AppendLine(" where SubjectsCD in(" + subjectCDList + ") ");
                    sql.AppendLine(" and CurrencyType =" + CurryTypeID + " ");
                    sql.AppendLine(" and CompanyCD=@CompanyCD ");
                    sql.AppendLine(" and SubjectsDetails=@SubjectsDetails ");
                    sql.AppendLine(" and FormTBName=@FormTBName ");
                    sql.AppendLine(" and FileName=@FileName ");
                }
                else
                {
                    sql = new StringBuilder();
                    sql.AppendLine("select isnull(sum(isnull(SumOriginalCurrency,0)),0) as BeginAmount");
                    sql.AppendLine("from officedba.SubjectsBeginDetails where SubjectsCD in (" + subjectCDList + ") ");
                    sql.AppendLine(" and CompanyCD=@CompanyCD");
                    sql.AppendLine(" and SubjectsDetails=@SubjectsDetails ");
                    sql.AppendLine(" and FormTBName=@FormTBName ");
                    sql.AppendLine(" and FileName=@FileName ");
                }
                SqlParameter[] parms = 
               {
                   new SqlParameter("@CompanyCD",CompanyCD),
                   new SqlParameter("@SubjectsDetails",SubjectsDetails),
                   new SqlParameter("@FormTBName",FormTBName),
                   new SqlParameter("@FileName",FileName)

               };
                DataTable defaultDt = SqlHelper.ExecuteSql(sql.ToString(), parms);
                foreach (DataRow dr in defaultDt.Rows)
                {
                    nev += Convert.ToDecimal(dr["BeginAmount"].ToString());
                }
            }
            return nev;
        }
        #endregion

    }
}

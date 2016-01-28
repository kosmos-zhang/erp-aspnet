/**********************************************
 * 类作用：    科目期初值明细数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/06/19
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
namespace XBase.Data.Office.FinanceManager
{
    public class SubjectsBeginDetailsDBHelper
    {


        #region 添加科目期初明细信息
        public static bool InsertSubjectsBeginDetails(SubjectsBeginDetailsModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Insert into officedba.SubjectsBeginDetails");
            sql.AppendLine("(CompanyCD,SubjectsCD,Dirt,YTotalDebit,");
            sql.AppendLine("YTotalLenders,OriginalCurrency,StandardCurrency,BeginMoney,CurrencyType,SumOriginalCurrency,YTotalDebitY,YTotalLendersY,")
;
            sql.AppendLine(" SubjectsDetails,FormTBName,FileName)");
            sql.AppendLine("values(@CompanyCD,@SubjectsCD,@Dirt,@YTotalDebit,");
            sql.AppendLine("@YTotalLenders,@OriginalCurrency,@StandardCurrency,@BeginMoney,@CurrencyType,@SumOriginalCurrency,@YTotalDebitY,@YTotalLendersY,@SubjectsDetails,@FormTBName,@FileName)");

            SqlParameter[] parms = 
            {
                 new SqlParameter("@CompanyCD",model.CompanyCD),
                 new SqlParameter("@SubjectsCD",model.SubjectsCD),
                 new SqlParameter("@Dirt",model.Dirt), 
                 new SqlParameter("@YTotalDebit",model.YTotalDebit),
                 new SqlParameter("@YTotalLenders",model.YTotalLenders),
                 new SqlParameter("@OriginalCurrency",model.OriginalCurrency),
                 new SqlParameter("@StandardCurrency",model.StandardCurrency),
                 new SqlParameter("@BeginMoney",model.BeginMoney),
                 new SqlParameter("@CurrencyType",model.CurrencyType),
                 new SqlParameter("@SumOriginalCurrency",model.SumOriginalCurrency),
                 new SqlParameter("@YTotalDebitY",model.YTotalDebitY),
                 new SqlParameter("@YTotalLendersY",model.YTotalLendersY),
                 new SqlParameter("@SubjectsDetails",model.SubjectsDetails),
                 new SqlParameter("@FormTBName",model.FormTBName),
                 new SqlParameter("@FileName",model.FileName)
            };



            SqlHelper.ExecuteTransSql(sql.ToString(), parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        #region 修改科目期初明细信息
        public static bool UpdateSubjectsBeginDetails(SubjectsBeginDetailsModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Update officedba.SubjectsBeginDetails");
            sql.AppendLine("set SubjectsCD=@SubjectsCD,Dirt=@Dirt,");
            sql.AppendLine("YTotalDebit=@YTotalDebit,");
            sql.AppendLine("OriginalCurrency=@OriginalCurrency,");
            sql.AppendLine("StandardCurrency=@StandardCurrency,BeginMoney=@BeginMoney,SumOriginalCurrency=@SumOriginalCurrency ");
            sql.AppendLine(",YTotalDebitY=@YTotalDebitY,YTotalLendersY=@YTotalLendersY,");
            sql.AppendLine("SubjectsDetails=@SubjectsDetails,FormTBName=@FormTBName,FileName=@FileName");
            sql.AppendLine("where ID=@ID");

            SqlParameter[] parms = 
            {
                 new SqlParameter("@ID",model.ID),
                 new SqlParameter("@SubjectsCD",model.SubjectsCD),
                 new SqlParameter("@Dirt",model.Dirt),
                 new SqlParameter("@YTotalDebit",model.YTotalDebit),
                 new SqlParameter("@YTotalLenders",model.YTotalLenders),
                 new SqlParameter("@OriginalCurrency",model.OriginalCurrency),
                 new SqlParameter("@StandardCurrency",model.StandardCurrency),
                 new SqlParameter("@BeginMoney",model.BeginMoney),
                 new SqlParameter("@SumOriginalCurrency",model.SumOriginalCurrency),
                 new SqlParameter("@YTotalDebitY",model.YTotalDebitY),
                 new SqlParameter("@YTotalLendersY",model.YTotalLendersY),
                 new SqlParameter("@SubjectsDetails",model.SubjectsDetails),
                 new SqlParameter("@FormTBName",model.FormTBName),
                 new SqlParameter("@FileName",model.FileName)
            };
            SqlHelper.ExecuteTransSql(sql.ToString(), parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        #region 删除科目期初明细信息
        public static bool DeleteInfo(string ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Delete from  officedba.SubjectsBeginDetails ");
            sql.AppendLine("where ID in("+ID+")");

            SqlHelper.ExecuteTransSql(sql.ToString());
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }

        #endregion

        #region 修改初始化信息
        public static bool UpdateSubjectsInitRecord(string CompanyCD, string Flag)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Update  officedba.SubjectsInitRecord");
            sql.AppendLine(" set IsInitFlag=@IsInitFlag  where CompanyCD=@CompanyCD");

            SqlParameter[] parms = 
            {
                 new SqlParameter("@CompanyCD",CompanyCD),
                 new SqlParameter("@IsInitFlag",Flag)
            };

            SqlHelper.ExecuteTransSql(sql.ToString(), parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }
        #endregion

        #region 添加初始化信息
        public static bool InsertSubjectsInitRecord(string CompanyCD, string Flag)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Insert into officedba.SubjectsInitRecord");
            sql.AppendLine("(CompanyCD,IsInitFlag)");
            sql.AppendLine("values(@CompanyCD,@IsInitFlag)");

            SqlParameter[] parms = 
            {
                 new SqlParameter("@CompanyCD",CompanyCD),
                 new SqlParameter("@IsInitFlag",Flag)
            };


            SqlHelper.ExecuteTransSql(sql.ToString(),parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        #region  是否存在初始化记录
        public static bool IsInit(string CompanyCD)
        {
            bool result = false;

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select count(CompanyCD) from  officedba.SubjectsInitRecord");
            sql.AppendLine("where CompanyCD=@CompanyCD");


            SqlParameter[] parms = 
            {
                new SqlParameter("@CompanyCD",CompanyCD)
            };

            object objs = SqlHelper.ExecuteScalar(sql.ToString(),parms);
            if (Convert.ToInt32(objs) > 0)
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region  判断期初余额是否平衡
        static public DataTable PeriodIsBalance(string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select isnull(sum(BeginMoney),0) BeginMoney from officedba.SubjectsBeginDetails");
            sql.AppendLine("where CompanyCD=@CompanyCD group by Dirt ");

            SqlParameter[] parm = 
            {
                 new SqlParameter("@CompanyCD",SqlDbType.VarChar,8)
            };
            parm[0].Value = CompanyCD;

            return SqlHelper.ExecuteSql(sql.ToString(), parm);

        }
        #endregion


        #region 添加科目期初值期数
        public static bool InsertPeriodNum(string CompanyCD, string PeriodNum)
        {
            bool result = PeriodIsexist(CompanyCD);
            StringBuilder sql = new StringBuilder();
            if (!result)
            {
                sql.AppendLine("Insert into officedba.SubjectsPeriod");
                sql.AppendLine("(CompanyCD,PeriodNum)");
                sql.AppendLine("values(@CompanyCD,@PeriodNum)");

                SqlParameter[] parms = 
                {
                     new SqlParameter("@CompanyCD",CompanyCD),
                     new SqlParameter("@PeriodNum",PeriodNum)
                };
                SqlHelper.ExecuteTransSql(sql.ToString(), parms);
            }
            else
            {
                sql.AppendLine(" update officedba.SubjectsPeriod ");
                sql.AppendLine(" set PeriodNum=@PeriodNum ");
                sql.AppendLine(" where CompanyCD=@CompanyCD ");

                SqlParameter[] parms = 
                {
                     new SqlParameter("@PeriodNum",PeriodNum),
                     new SqlParameter("@CompanyCD",CompanyCD)
                    
                };
                SqlHelper.ExecuteTransSql(sql.ToString(), parms);
            }

            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        #region 删除科目期初值期初数
        public static bool DeleteDetailsPeriod(string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Delete from officedba.SubjectsPeriod");
            sql.AppendLine("where CompanyCD=@CompanyCD");

            SqlParameter[] parms = 
            {
                new SqlParameter("@CompanyCD",CompanyCD)
            };

            SqlHelper.ExecuteTransSql(sql.ToString(),parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }
        #endregion

        #region 检查是否存在期初值
        public static bool IsexistDetails(string CompanyCD)
        {
            bool result = false;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select count(*) from officedba.SubjectsBeginDetails");
            sql.AppendLine("where CompanyCD=@CompanyCD");

            SqlParameter[] parms = 
            {
                new SqlParameter("@CompanyCD",CompanyCD)
            };

            object objs = SqlHelper.ExecuteScalar(sql.ToString(), parms);
            if (Convert.ToInt32(objs) > 0)
            {
                result = true;
            }
            return result;
        }
        #endregion
 

        #region 检查期初信息是否存在
        public static bool PeriodIsexist(string CompanyCD)
        {
            bool result = false; 

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select count(CompanyCD) from  officedba.SubjectsPeriod");
            sql.AppendLine("where CompanyCD=@CompanyCD");


            SqlParameter[] parms = 
            {
                new SqlParameter("@CompanyCD",CompanyCD)
            };

            object objs = SqlHelper.ExecuteScalar(sql.ToString(), parms);
            if (Convert.ToInt32(objs) > 0)
            {
                result = true;
            }
            return result;
        }
        #endregion


        #region 查询初始化信息
        public static DataTable GetSubjectsInit(string CompanyCD)
        { 
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select IsInitFlag from  officedba.SubjectsInitRecord");
            sql.AppendLine("where CompanyCD=@CompanyCD ");

            SqlParameter[] parms = 
            {
                 new SqlParameter("@CompanyCD",CompanyCD)
            };

            return SqlHelper.ExecuteSql(sql.ToString(), parms);
        }
        #endregion


        #region 获取试算平衡列表
        public static DataTable GetsubjectsBalanceInfo(string companyCD, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID, b.SubjectsName, a.SubjectsCD,case when a.Dirt='0' then '借' when a.Dirt='1' then '贷'");
            sql.AppendLine("end as Dirt,a.OriginalCurrency,a.StandardCurrency,");
            sql.AppendLine("a.BeginMoney,a.SumOriginalCurrency from officedba.SubjectsBeginDetails as a ");
            sql.AppendLine("left join officedba.AccountSubjects as b on a.SubjectsCD=b.SubjectsCD and b.CompanyCD=@CompanyCD");
            sql.AppendLine("where a.CompanyCD=@CompanyCD ");

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);

        }
        #endregion


        #region 试算平衡
        public static DataTable SubjectsBalance(string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select   isnull(sum (StandardCurrency),0) as j,");
            sql.AppendLine("(select   isnull(sum(StandardCurrency),0) as d from officedba.SubjectsBeginDetails");
            sql.AppendLine("where CompanyCD=@CompanyCD and Dirt='1') as d ");
            sql.AppendLine("from officedba.SubjectsBeginDetails");
            sql.AppendLine("where CompanyCD=@CompanyCD and Dirt='0'");
            sql.AppendLine("union ");
            sql.AppendLine("select   isnull(sum(YTotalDebit),0) as bj,");
            sql.AppendLine("(select   isnull(sum(YTotalLenders),0) as bd from officedba.SubjectsBeginDetails");
            sql.AppendLine("where CompanyCD=@CompanyCD and Dirt='1') as d from officedba.SubjectsBeginDetails");
            sql.AppendLine("where CompanyCD=@CompanyCD  and Dirt='0'");

            SqlParameter[] parms = {
                                       new SqlParameter("@CompanyCD",CompanyCD)
                                   };

            return SqlHelper.ExecuteSql(sql.ToString(),parms);
        }
        #endregion


        //#region 综合本位币汇总
        //public static DataTable SearchSumStandardCurrency(string CompanyCD,string SubjectTypeID)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    sql.AppendLine("select   case when a.Dirt='0' then '借'");
        //    sql.AppendLine("    when a.Dirt='1' then '贷' end as Dirt,");
        //    sql.AppendLine("    sum (isnull (a.YTotalDebit,0)) as YTotalDebit,");
        //    sql.AppendLine("   sum (isnull(a.YTotalLenders,0))");
        //    sql.AppendLine("    as YTotalLenders, ");
        //    sql.AppendLine("       sum(isnull (a.StandardCurrency,0)) as StandardCurrency,");
        //    sql.AppendLine("    a.SubjectsCD+'-'+ b.SubjectsName as  SubjectsName");
        //    sql.AppendLine(",sum (isnull(a.BeginMoney,0)) as BeginMoney ,sum (isnull(a.SumOriginalCurrency,0)) as SumOriginalCurrency     ");
        //    sql.AppendLine("      from officedba.SubjectsBeginDetails as  a");
        //    sql.AppendLine("    left join officedba.AccountSubjects as b");
        //    sql.AppendLine("    on  a.SubjectsCD=b.SubjectsCD   ");
        //    sql.AppendLine("     where a.CompanyCD=@CompanyCD ");
          
        //    //定义查询的命令
        //    SqlCommand comm = new SqlCommand();
        //    //添加公司代码参数
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
        //    //币种类别
        //    if (!string.IsNullOrEmpty(SubjectTypeID))
        //    {
        //        sql.AppendLine(" AND left(a.SubjectsCD,1)=@SubjectTypeID ");
        //        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SubjectTypeID", SubjectTypeID));
        //    }
        //    sql.AppendLine("   group by a.SubjectsCD,a.Dirt,b.SubjectsName");

        //    //指定命令的SQL文
        //    comm.CommandText = sql.ToString();
        //    //执行查询
        //    return SqlHelper.ExecuteSearch(comm);

        //}
        //#endregion


        #region 检索科目是否已存在
        public static bool SubjectsCDisExist(string CompanyCD, string SubjectsCD, string CurrencyType, string SubjectsDetails, string FormTBName, string FileName)
        {
            bool result = false;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select count(*) from  officedba.SubjectsBeginDetails");
            sql.AppendLine("where CompanyCD=@CompanyCD and SubjectsCD=@SubjectsCD");
            sql.AppendLine("and CurrencyType=@CurrencyType ");
            sql.AppendLine("and SubjectsDetails=@SubjectsDetails ");
            sql.AppendLine("and FormTBName=@FormTBName ");
            sql.AppendLine("and FileName=@FileName");

            SqlParameter[] parsm = {
                                       new SqlParameter("@CompanyCD",CompanyCD),
                                       new SqlParameter("@SubjectsCD",SubjectsCD),
                                       new SqlParameter("@CurrencyType",CurrencyType),
                                       new SqlParameter("@SubjectsDetails",SubjectsDetails),
                                       new SqlParameter("@FormTBName",FormTBName),
                                       new SqlParameter("@FileName",FileName)
                                   };
            object objs = SqlHelper.ExecuteScalar(sql.ToString(),parsm);
            if (Convert.ToInt32(objs) > 0)
            {
                result = true;
            }

            return result;
        }
        #endregion


        #region  获取企业期初值信息
        public static string GetPeriodNum(string CompanyCD)
        {
            string  result=string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select PeriodNum from officedba.SubjectsPeriod");
            sql.AppendLine("where CompanyCD=@CompanyCD");

            SqlParameter[] parms = 
            {
                 new SqlParameter("@CompanyCD",CompanyCD)
            };

            DataTable dt = SqlHelper.ExecuteSql(sql.ToString(), parms);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["PeriodNum"].ToString();
            }
            return result;

        }
        #endregion


        #region 判断是否有期初值
        public static bool IsPeriodMoney(string CompanyCD)
        {

            bool result = false;

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select count(CompanyCD) from officedba.SubjectsBeginDetails");
            sql.AppendLine("where CompanyCD=@CompanyCD");

            SqlParameter[] parms = 
            {
                new SqlParameter("@CompanyCD",CompanyCD)
            };

            object objs = SqlHelper.ExecuteScalar(sql.ToString(), parms);
            if (Convert.ToInt32(objs) > 0)
            {
                result = true;
            }

            return result;

        }
        #endregion

        #region 检索科目期初明细信息
        public static DataTable SearchSubjectDetatilInfo(string CurrencyType,string CompanyCD,string SubjectsType)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.SubjectsCD,  ");
            sql.AppendLine("case when a.Dirt='0' then '借'");
            sql.AppendLine("when a.Dirt='1' then '贷' end as Dirt,");
            sql.AppendLine("isnull (a.YTotalDebit,0) as YTotalDebit,isnull(a.YTotalLenders,0)");
            sql.AppendLine(" as YTotalLenders,");
            sql.AppendLine("isnull(a.OriginalCurrency,0) as OriginalCurrency ,");
            sql.AppendLine("isnull (a.StandardCurrency,0) as StandardCurrency");
            sql.AppendLine(",isnull (a.YTotalDebitY,0) as YTotalDebitY, ");
            sql.AppendLine("isnull (a.YTotalLendersY,0) as YTotalLendersY,");
            sql.AppendLine("isnull (a.SubjectsDetails,'') as SubjectsDetails, ");
            sql.AppendLine("isnull (a.FormTBName,'') as FormTBName, ");
            sql.AppendLine("isnull (a.FileName,'') as FileName,case when a.ID is not null then '1' end as ByOrder ");
            sql.AppendLine(",isnull(a.BeginMoney,0) as BeginMoney,isnull(a.SumOriginalCurrency,0) as SumOriginalCurrency  from officedba.SubjectsBeginDetails as  a");
            sql.AppendLine("left join officedba.AccountSubjects as b on  a.SubjectsCD=b.SubjectsCD   and a.CompanyCD=b.CompanyCD");
            sql.AppendLine("where a.CompanyCD=@CompanyCD   ");

            //StringBuilder sqlcount = new StringBuilder();
            //sqlcount.AppendLine("select sum(isnull (a.YTotalDebit,0)) as YTotalDebit,sum (isnull(a.YTotalLenders,0))");
            //sqlcount.AppendLine(" as YTotalLenders,");
            //sqlcount.AppendLine("sum(isnull(a.OriginalCurrency,0)) as OriginalCurrency ,");
            //sqlcount.AppendLine("sum(isnull (a.StandardCurrency,0)) as StandardCurrency");
            //sqlcount.AppendLine(",sum(isnull (a.YTotalDebitY,0))  as YTotalDebitY, ");
            //sqlcount.AppendLine("sum(isnull (a.YTotalLendersY,0)) as YTotalLendersY");
            //sqlcount.AppendLine(",sum(isnull(a.BeginMoney,0)) as BeginMoney,sum (isnull(a.SumOriginalCurrency,0)) as ");
            //sqlcount.AppendLine("SumOriginalCurrency  from officedba.SubjectsBeginDetails as  a");
            //sqlcount.AppendLine("left join officedba.AccountSubjects as b on  a.SubjectsCD=b.SubjectsCD and a.CompanyCD=b.CompanyCD ");
            //sqlcount.AppendLine("where a.CompanyCD=@CompanyCD");

            //定义查询的命令
            SqlCommand comm = new SqlCommand();

            //SqlCommand comm1 = new SqlCommand();


            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //comm1.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //科目类别
            if (!string.IsNullOrEmpty(SubjectsType))
            {
                sql.AppendLine(" AND left(a.SubjectsCD,1)=@SubjectsType ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SubjectsType", SubjectsType));

                //sqlcount.AppendLine(" AND left(a.SubjectsCD,1)=@SubjectsType");
                //comm1.Parameters.Add(SqlHelper.GetParameterFromString("@SubjectsType", SubjectsType));


            }
            //币种类别
            if (!string.IsNullOrEmpty(CurrencyType) && CurrencyType.Trim() != "0")
            {
                sql.AppendLine(" AND a.CurrencyType=@CurrencyType ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrencyType",CurrencyType));


                //sqlcount.AppendLine(" AND a.CurrencyType=@CurrencyType ");
                //comm1.Parameters.Add(SqlHelper.GetParameterFromString("@CurrencyType", CurrencyType));
            }

            sql.AppendLine(" order by a.SubjectsCD asc ");
            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
           DataTable dt= SqlHelper.ExecuteSearch(comm);

           foreach (DataRow dr in dt.Rows)
           {
               dr["SubjectsCD"] = dr["SubjectsCD"].ToString()+"-"+VoucherDBHelper.GetSubJectsName(dr["SubjectsCD"].ToString(), dr["SubjectsDetails"].ToString(), dr["FormTBName"].ToString(), dr["FileName"].ToString(), CompanyCD);
           }
           //ds.Tables.Add(dt);


           //comm1.CommandText = sqlcount.ToString();
           //DataTable dt1 = SqlHelper.ExecuteSearch(comm1);
           //ds.Tables.Add(dt1);

           return dt;
        }
        #endregion


        #region 综合本位币汇总
        public static DataTable  SearchSumStandardCurrency(string CompanyCD, string SubjectTypeID)
        {
            //DataSet ds = new DataSet();

            //StringBuilder sql = new StringBuilder();
            //sql.AppendLine("select   case when a.Dirt='0' then '借'");
            //sql.AppendLine("    when a.Dirt='1' then '贷' end as Dirt,");
            //sql.AppendLine("    sum (isnull (a.YTotalDebit,0)) as YTotalDebit,");
            //sql.AppendLine("   sum (isnull(a.YTotalLenders,0))");
            //sql.AppendLine("    as YTotalLenders, sum(isnull(a.OriginalCurrency,0)) as  OriginalCurrency,");
            //sql.AppendLine("       sum(isnull (a.StandardCurrency,0)) as StandardCurrency,");
            //sql.AppendLine("    a.SubjectsCD+'-'+ b.SubjectsName as  SubjectsName");
            //sql.AppendLine(",sum(isnull(a.BeginMoney,0)) as BeginMoney ,sum(isnull(a.SumOriginalCurrency,0)) as SumOriginalCurrency,");
            //sql.AppendLine("sum(isnull(a.YTotalDebitY,0)) as YTotalDebitY,sum(isnull(a.YTotalLendersY,0)) as YTotalLendersY");
            //sql.AppendLine("      from officedba.SubjectsBeginDetails as  a");
            //sql.AppendLine("    left join officedba.AccountSubjects as b");
            //sql.AppendLine("    on  a.SubjectsCD=b.SubjectsCD  and a.CompanyCD=b.CompanyCD  ");
            //sql.AppendLine("     where a.CompanyCD=@CompanyCD ");


            ////StringBuilder sqlcount = new StringBuilder();
            ////sqlcount.AppendLine("select sum (isnull (a.YTotalDebit,0)) as YTotalDebit,");
            ////sqlcount.AppendLine("sum (isnull(a.YTotalLenders,0))   as YTotalLenders, ");
            ////sqlcount.AppendLine("sum(isnull (a.StandardCurrency,0)) as StandardCurrency");
            ////sqlcount.AppendLine(",sum (isnull(a.BeginMoney,0)) as BeginMoney ,sum (isnull(a.SumOriginalCurrency,0)) as SumOriginalCurrency, ");

            ////sqlcount.AppendLine("sum(isnull(a.YTotalDebitY,0)) as YTotalDebitY,sum(isnull(a.YTotalLendersY,0)) as YTotalLendersY");

            ////sqlcount.AppendLine("from officedba.SubjectsBeginDetails as  a");
            ////sqlcount.AppendLine(" left join officedba.AccountSubjects as b");
            ////sqlcount.AppendLine("on  a.SubjectsCD=b.SubjectsCD  and a.CompanyCD=b.CompanyCD ");
            ////sqlcount.AppendLine(" where a.CompanyCD=@CompanyCD ");

            ////定义查询的命令
            //SqlCommand comm = new SqlCommand();
            ////SqlCommand cmd = new SqlCommand();
            ////添加公司代码参数
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            ////cmd.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            ////币种类别
            //if (!string.IsNullOrEmpty(SubjectTypeID))
            //{
            //    sql.AppendLine(" AND left(a.SubjectsCD,1)=@SubjectTypeID ");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@SubjectTypeID", SubjectTypeID));

            //    //sqlcount.AppendLine(" AND left(a.SubjectsCD,1)=@SubjectTypeID ");
            //    //cmd.Parameters.Add(SqlHelper.GetParameterFromString("@SubjectTypeID", SubjectTypeID));
            //}
            //sql.AppendLine("   group by a.SubjectsCD,a.Dirt,b.SubjectsName");

            ////指定命令的SQL文
            //comm.CommandText = sql.ToString();
            ////执行查询
            //cmd.CommandText = sqlcount.ToString();

            //DataTable dt=  SqlHelper.ExecuteSearch(comm);
            //DataTable CountDt = SqlHelper.ExecuteSearch(cmd);
            //ds.Tables.Add(dt);
            //ds.Tables.Add(CountDt);

            //return ds;


            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.SubjectsCD,  ");
            sql.AppendLine("case when a.Dirt='0' then '借'");
            sql.AppendLine("when a.Dirt='1' then '贷' end as Dirt,");
            sql.AppendLine("isnull (a.YTotalDebit,0) as YTotalDebit,isnull(a.YTotalLenders,0)");
            sql.AppendLine(" as YTotalLenders,");
            sql.AppendLine("isnull(a.OriginalCurrency,0) as OriginalCurrency ,");
            sql.AppendLine("isnull (a.StandardCurrency,0) as StandardCurrency");
            sql.AppendLine(",isnull (a.YTotalDebitY,0) as YTotalDebitY, ");
            sql.AppendLine("isnull (a.YTotalLendersY,0) as YTotalLendersY,");
            sql.AppendLine("isnull (a.SubjectsDetails,'') as SubjectsDetails, ");
            sql.AppendLine("isnull (a.FormTBName,'') as FormTBName, ");
            sql.AppendLine("isnull (a.FileName,'') as FileName,case when a.ID is not null then '1' end as ByOrder ");
            sql.AppendLine(",isnull(a.BeginMoney,0) as BeginMoney,isnull(a.SumOriginalCurrency,0) as SumOriginalCurrency  from officedba.SubjectsBeginDetails as  a");
            sql.AppendLine("left join officedba.AccountSubjects as b on  a.SubjectsCD=b.SubjectsCD   and a.CompanyCD=b.CompanyCD");
            sql.AppendLine("where a.CompanyCD=@CompanyCD   ");

            //StringBuilder sqlcount = new StringBuilder();
            //sqlcount.AppendLine("select sum(isnull (a.YTotalDebit,0)) as YTotalDebit,sum (isnull(a.YTotalLenders,0))");
            //sqlcount.AppendLine(" as YTotalLenders,");
            //sqlcount.AppendLine("sum(isnull(a.OriginalCurrency,0)) as OriginalCurrency ,");
            //sqlcount.AppendLine("sum(isnull (a.StandardCurrency,0)) as StandardCurrency");
            //sqlcount.AppendLine(",sum(isnull (a.YTotalDebitY,0))  as YTotalDebitY, ");
            //sqlcount.AppendLine("sum(isnull (a.YTotalLendersY,0)) as YTotalLendersY");
            //sqlcount.AppendLine(",sum(isnull(a.BeginMoney,0)) as BeginMoney,sum (isnull(a.SumOriginalCurrency,0)) as ");
            //sqlcount.AppendLine("SumOriginalCurrency  from officedba.SubjectsBeginDetails as  a");
            //sqlcount.AppendLine("left join officedba.AccountSubjects as b on  a.SubjectsCD=b.SubjectsCD and a.CompanyCD=b.CompanyCD ");
            //sqlcount.AppendLine("where a.CompanyCD=@CompanyCD");

            //定义查询的命令
            SqlCommand comm = new SqlCommand();

            //SqlCommand comm1 = new SqlCommand();


            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //comm1.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //科目类别
            if (!string.IsNullOrEmpty(SubjectTypeID))
            {
                sql.AppendLine(" AND left(a.SubjectsCD,1)=@SubjectsType ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SubjectsType", SubjectTypeID));

                //sqlcount.AppendLine(" AND left(a.SubjectsCD,1)=@SubjectsType");
                //comm1.Parameters.Add(SqlHelper.GetParameterFromString("@SubjectsType", SubjectsType));


            }

            sql.AppendLine(" order by a.SubjectsCD asc ");
            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            DataTable dt = SqlHelper.ExecuteSearch(comm);

            foreach (DataRow dr in dt.Rows)
            {
                dr["SubjectsCD"] = dr["SubjectsCD"].ToString() + "-" + VoucherDBHelper.GetSubJectsName(dr["SubjectsCD"].ToString(), dr["SubjectsDetails"].ToString(), dr["FormTBName"].ToString(), dr["FileName"].ToString(), CompanyCD);
            }

            return dt;



        }
        #endregion


        #region 获取不相同的科目编码的顶级科目编码
        /// <summary>
        /// 获取不相同的科目编码的顶级科目编码
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetDistinctSubjectCD(string CompanyCD)
        {
            string sql = "select distinct SubjectsCD from officedba.SubjectsBeginDetails where CompanyCD=@CompanyCD order by SubjectsCD asc";
            SqlParameter[] parms = 
            {
                 new SqlParameter("@CompanyCD",CompanyCD)
            };

            return SqlHelper.ExecuteSql(sql, parms);
        }
        #endregion

        #region 获取科目初始化个科目类别的汇总信息
        /// <summary>
        /// 获取科目初始化个科目类别的汇总信息
        /// </summary>
        /// <param name="SubjectTypeID">会计科目类别</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetSumInfo(string SubjectTypeID, string CompanyCD, string CurryTypeID)
        {
            StringBuilder sqlcount = new StringBuilder();
            sqlcount.AppendLine("select Dirt,sum (isnull (YTotalDebit,0)) as YTotalDebit,");
            sqlcount.AppendLine("sum (isnull(YTotalLenders,0))   as YTotalLenders,sum(isnull(OriginalCurrency,0)) as OriginalCurrency, ");
            sqlcount.AppendLine("sum(isnull (StandardCurrency,0)) as StandardCurrency");
            sqlcount.AppendLine(",sum(isnull(BeginMoney,0)) as BeginMoney ,sum(isnull(SumOriginalCurrency,0)) as SumOriginalCurrency,");

            sqlcount.AppendLine("sum(isnull(YTotalDebitY,0)) as YTotalDebitY,sum(isnull(YTotalLendersY,0)) as YTotalLendersY");

            sqlcount.AppendLine("from officedba.SubjectsBeginDetails");
            sqlcount.AppendLine(" where CompanyCD=@CompanyCD ");


            //定义查询的命令
            SqlCommand cmd = new SqlCommand();
            //添加公司代码参数
            cmd.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //币种类别
            if (!string.IsNullOrEmpty(SubjectTypeID))
            {
                sqlcount.AppendLine(" AND left(SubjectsCD,1)=@SubjectTypeID ");
                cmd.Parameters.Add(SqlHelper.GetParameterFromString("@SubjectTypeID", SubjectTypeID));
            }
            //币种类别
            if (!string.IsNullOrEmpty(CurryTypeID)&&CurryTypeID.Trim()!="0")
            {
                sqlcount.AppendLine(" AND CurrencyType=@CurrencyType ");
                cmd.Parameters.Add(SqlHelper.GetParameterFromString("@CurrencyType", CurryTypeID));
            }

            sqlcount.AppendLine("group by Dirt");
            //执行查询
            cmd.CommandText = sqlcount.ToString();
            return SqlHelper.ExecuteSearch(cmd); 
        }
        #endregion

        #region 判断企业科目期初值是否结束初始化
        /// <summary>
        /// 判断企业科目期初值是否结束初始化
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static bool IsEndInit(string CompanyCD)
        {
            bool result = false;

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select IsInitFlag from  officedba.SubjectsInitRecord");
            sql.AppendLine("where CompanyCD=@CompanyCD");


            SqlParameter[] parms = 
            {
                new SqlParameter("@CompanyCD",CompanyCD)
            };

            object objs = SqlHelper.ExecuteScalar(sql.ToString(), parms);
            if (objs != null)
            {
                if (Convert.ToInt32(objs) > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        #endregion

    }
}

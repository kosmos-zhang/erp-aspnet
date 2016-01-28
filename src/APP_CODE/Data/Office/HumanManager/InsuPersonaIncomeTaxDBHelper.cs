/**********************************************
 * 类作用：   个人所得税设置
 * 建立人：   王保军
 * 建立时间： 2009/06/19
 *  修改人：   王保军
 * 建立时间： 2009/08/27
 ***********************************************/
using System;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using XBase.Common;
using System.Collections;
using System.Collections.Generic;
namespace XBase.Data.Office.HumanManager
{
    public  class InsuPersonaIncomeTaxDBHelper
    {
        public static DataTable SearchInsuPersonalTaxInfo(string companyCD)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                  ");
            searchSql.AppendLine(" 	 ID                    ");
            searchSql.AppendLine(" 	,MinMoney         ");
            searchSql.AppendLine(" 	,MaxMoney          ");
            searchSql.AppendLine(" 	,TaxPercent        ");
            searchSql.AppendLine(" 	,MinusMoney         ");
            searchSql.AppendLine(" FROM                    ");
            searchSql.AppendLine(" 	officedba.IncomeTaxPercent   ");
            searchSql.AppendLine(" WHERE                   ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
    

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        public static bool UpdateIsuPersonalTaxInfo(IList<PercentIncomeTaxModel> modeList)
        {
            if (!DeletePersonalTaxInfo(modeList[0].CompanyCD))
            {
                return false;
            }
            bool isSucc = false;
            foreach (PercentIncomeTaxModel model in modeList)
            {
                #region 插入SQL拼写
                StringBuilder insertSql = new StringBuilder();
                insertSql.AppendLine("insert into  officedba.IncomeTaxPercent(CompanyCD,MinMoney,MaxMoney,TaxPercent,MinusMoney,TaxLevel) ");
                insertSql.AppendLine("          values(@CompanyCD,@MinMoney,@MaxMoney,@TaxPercent,@MinusMoney,@TaxLevel) ");
                #endregion
                //定义插入基本信息的命令
                SqlCommand comm = new SqlCommand();
                comm.CommandText = insertSql.ToString();
                //设置保存的参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@MinMoney", model.MinMoney ));	//创建人
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaxMoney ", model.MaxMoney  ));	//启用状态
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaxPercent", model.TaxPercent));	//启用状态
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@MinusMoney", model.MinusMoney ));	//启用状态
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaxLevel", model.TaxLevel ));
                //添加返回参数
                //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

                //执行插入操作
                isSucc = SqlHelper.ExecuteTransWithCommand(comm);
                if (!isSucc)
                {
                    isSucc = false;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return isSucc;

        }

        public static bool DeletePersonalTaxInfo(string CompanyCD)
        {
                StringBuilder insertSql = new StringBuilder();
                insertSql.AppendLine("Delete from officedba.IncomeTaxPercent where CompanyCD=@CompanyCD");
              
                //定义插入基本信息的命令
                SqlCommand comm = new SqlCommand();
                comm.CommandText = insertSql.ToString();
                //设置保存的参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));	//公司代码
                //添加返回参数
                //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

                //执行插入操作
                bool      isSucc = SqlHelper.ExecuteTransWithCommand(comm);
                return isSucc;


        }
    }
}

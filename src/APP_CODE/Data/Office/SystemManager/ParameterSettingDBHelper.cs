using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;

namespace XBase.Data.Office.SystemManager
{
    public class ParameterSettingDBHelper
    {

        #region 读取参数配置
        public static DataTable Get(string CompanyCD, string FunctionType)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT * ");
            sbSql.AppendLine(" FROM officedba.ParameterSetting ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND FunctionType=@FunctionType ");
            SqlParameter[] Params = new SqlParameter[2];

            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            Params[index++] = SqlHelper.GetParameter("@FunctionType", FunctionType);

            return SqlHelper.ExecuteSql(sbSql.ToString(), Params);
        }
        #endregion


        #region 设置指定的参数配置
        public static bool Set(string CompanyCD, string FuntionType, string  Status)
        {
            StringBuilder sbSql = new StringBuilder();
           //验证是否存在指定配置项
            if (ValidateKey(CompanyCD, FuntionType))
            {
                //不存在 则按照指定状态插入一条数据
                sbSql.AppendLine(" INSERT INTO officedba.ParameterSetting ");
                sbSql.AppendLine(" (CompanyCD,FunctionType,UsedStatus) ");
                sbSql.AppendLine(" VALUES ");
                sbSql.AppendLine(" (@CompanyCD,@FunctionType,@UsedStatus )");
            }
            else
            {
                //已经存在 则更新指定项的状态
                sbSql.AppendLine(" UPDATE officedba.ParameterSetting SET ");
                sbSql.AppendLine(" UsedStatus=@UsedStatus ");
                sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND FunctionType=@FunctionType ");
            }

            //构造参数
            SqlParameter[] Params =new  SqlParameter[3];
            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@UsedStatus", Status);
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            Params[index++] = SqlHelper.GetParameter("@FunctionType", FuntionType);

            //执行SQL
            if (SqlHelper.ExecuteTransSql(sbSql.ToString(), Params) > 0)
                return true;
            else
                return false;
        }

        #endregion

        #region 设置小数位
        public static bool SetPoint(string CompanyCD, string FuntionType, string SelPoint)
        {
            StringBuilder sbSql = new StringBuilder();
            //验证是否存在指定配置项
            if (ValidateKey(CompanyCD, FuntionType))
            {
                //不存在 则按照指定状态插入一条数据
                sbSql.AppendLine(" INSERT INTO officedba.ParameterSetting ");
                sbSql.AppendLine(" (CompanyCD,FunctionType,SelPoint) ");
                sbSql.AppendLine(" VALUES ");
                sbSql.AppendLine(" (@CompanyCD,@FunctionType,@SelPoint )");
            }
            else
            {
                //已经存在 则更新指定项的状态
                sbSql.AppendLine(" UPDATE officedba.ParameterSetting SET ");
                sbSql.AppendLine(" SelPoint=@SelPoint ");
                sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND FunctionType=@FunctionType ");
            }

            //构造参数
            SqlParameter[] Params = new SqlParameter[3];
            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@SelPoint", SelPoint);
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            Params[index++] = SqlHelper.GetParameter("@FunctionType", FuntionType);

            //执行SQL
            if (SqlHelper.ExecuteTransSql(sbSql.ToString(), Params) > 0)
                return true;
            else
                return false;
        }

        #endregion

        #region 验证是否已经存在指定配置项
        /// <summary>
        /// 验证是否已经存在指定配置项
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="FunctionType"></param>
        /// <returns></returns>
        private static bool ValidateKey(string CompanyCD, string FunctionType)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT  TOP 1 * ");
            sbSql.AppendLine(" FROM officedba.ParameterSetting ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD  AND FunctionType=@FunctionType ");

            SqlParameter[] Params = new SqlParameter[2];
            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            Params[index++] = SqlHelper.GetParameter("@FunctionType", FunctionType);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), Params);

            if (dt == null || dt.Rows.Count <= 0)
                return true;
            else
                return false;
        }
        #endregion

        #region 验证是否启用辅助核算
        /// <summary>
        /// 验证是否启用辅助核算
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool IfUsedAssistant(string CompanyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT  TOP 1 * ");
            sbSql.AppendLine(" FROM officedba.AccountSubjects ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD  AND AuciliaryCD!=0 and (SubjectsCD='1122' or SubjectsCD='2202') ");

            SqlParameter[] Params = new SqlParameter[1];
            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), Params);

            if (dt == null || dt.Rows.Count <= 0)
                return false;
            else
                return true;
        }
        #endregion

        #region 验证是否使用过多计量单位组
        /// <summary>
        /// 验证是否使用过多计量单位组
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool IfUsedUnitGroup(string CompanyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select top 1 *  from officedba.productInfo where  CompanyCD=@CompanyCD and GroupUnitNo is not null");

            SqlParameter[] Params = new SqlParameter[1];
            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), Params);

            if (dt == null || dt.Rows.Count <= 0)
                return true;
            else
                return false;
        }
        #endregion

    }
}

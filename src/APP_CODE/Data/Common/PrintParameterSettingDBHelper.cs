/**********************************************
 * 类作用：  打印模板设置数据库层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/03
 ***********************************************/

using System;
using XBase.Model.Common;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace XBase.Data.Common
{
    public class PrintParameterSettingDBHelper
    {

        #region 获取打印模板设置详细信息
        /// <summary>
        /// 获取打印模板设置详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetPrintParameterSettingInfo(PrintParameterSettingModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from officedba.PrintParameterSetting where BillTypeFlag=@BillTypeFlag and PrintTypeFlag=@PrintTypeFlag and CompanyCD=@CompanyCD");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", model.BillTypeFlag.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PrintTypeFlag", model.PrintTypeFlag.ToString()));


            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 打印模板参数设置编辑
        /// <summary>
        /// 打印模板参数设置编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool EditPrintParameterSetting(PrintParameterSettingModel model)
        {
            ArrayList listADD = new ArrayList();
            if (CountPrintSets(model) > 0)
            {
                #region  打印参数设置修改SQL语句
                StringBuilder sqlEdit = new StringBuilder();
                sqlEdit.AppendLine("UPDATE officedba.PrintParameterSetting");
                sqlEdit.AppendLine("   SET BaseFields = @BaseFields");
                sqlEdit.AppendLine("      ,DetailFields = @DetailFields");
                sqlEdit.AppendLine("      ,DetailSecondFields = @DetailSecondFields");
                sqlEdit.AppendLine("      ,DetailThreeFields = @DetailThreeFields");
                sqlEdit.AppendLine("      ,DetailFourFields = @DetailFourFields");
                sqlEdit.AppendLine(" WHERE BillTypeFlag=@BillTypeFlag and PrintTypeFlag=@PrintTypeFlag and CompanyCD=@CompanyCD");


                SqlCommand comm = new SqlCommand();
                comm.CommandText = sqlEdit.ToString();
                comm.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", model.BillTypeFlag.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@PrintTypeFlag", model.PrintTypeFlag.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@BaseFields", model.BaseFields));
                comm.Parameters.Add(SqlHelper.GetParameter("@DetailFields", model.DetailFields));
                comm.Parameters.Add(SqlHelper.GetParameter("@DetailSecondFields", model.DetailSecondFields));
                comm.Parameters.Add(SqlHelper.GetParameter("@DetailThreeFields", model.DetailThreeFields));
                comm.Parameters.Add(SqlHelper.GetParameter("@DetailFourFields", model.DetailFourFields));
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));

                listADD.Add(comm);
                #endregion
            }
            else
            {
                #region 打印参数设置添加SQL语句
                StringBuilder sqlPrint = new StringBuilder();
                sqlPrint.AppendLine("INSERT INTO officedba.PrintParameterSetting");
                sqlPrint.AppendLine("           (CompanyCD");
                sqlPrint.AppendLine("           ,BillTypeFlag");
                sqlPrint.AppendLine("           ,PrintTypeFlag");
                sqlPrint.AppendLine("           ,BaseFields");
                sqlPrint.AppendLine("           ,DetailFields");
                sqlPrint.AppendLine("           ,DetailSecondFields");
                sqlPrint.AppendLine("           ,DetailThreeFields");
                sqlPrint.AppendLine("           ,DetailFourFields");
                sqlPrint.AppendLine("           ,ModifiedDate)");
                sqlPrint.AppendLine("     VALUES");
                sqlPrint.AppendLine("           (@CompanyCD");
                sqlPrint.AppendLine("           ,@BillTypeFlag");
                sqlPrint.AppendLine("           ,@PrintTypeFlag");
                sqlPrint.AppendLine("           ,@BaseFields");
                sqlPrint.AppendLine("           ,@DetailFields");
                sqlPrint.AppendLine("           ,@DetailSecondFields");
                sqlPrint.AppendLine("           ,@DetailThreeFields");
                sqlPrint.AppendLine("           ,@DetailFourFields");
                sqlPrint.AppendLine("           ,getdate())");

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sqlPrint.ToString();
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", model.BillTypeFlag.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@PrintTypeFlag", model.PrintTypeFlag.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@BaseFields", model.BaseFields));
                comm.Parameters.Add(SqlHelper.GetParameter("@DetailFields", model.DetailFields));
                comm.Parameters.Add(SqlHelper.GetParameter("@DetailSecondFields", model.DetailSecondFields));
                comm.Parameters.Add(SqlHelper.GetParameter("@DetailThreeFields", model.DetailThreeFields));
                comm.Parameters.Add(SqlHelper.GetParameter("@DetailFourFields", model.DetailFourFields));
                listADD.Add(comm);
                #endregion
            }
            
            try
            {
                return SqlHelper.ExecuteTransWithArrayList(listADD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 该单据是否设置过打印模板
        /// <summary>
        /// 该单据是否设置过打印模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int CountPrintSets(PrintParameterSettingModel model)
        {

            string sql = "select count(*) as countPrints from officedba.PrintParameterSetting where BillTypeFlag=@BillTypeFlag and PrintTypeFlag=@PrintTypeFlag and CompanyCD=@CompanyCD";
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            parms[1] = SqlHelper.GetParameter("@BillTypeFlag", model.BillTypeFlag);
            parms[2] = SqlHelper.GetParameter("@PrintTypeFlag", model.PrintTypeFlag);
            object obj = SqlHelper.ExecuteScalar(sql, parms);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion
    }
}

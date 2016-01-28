/**********************************************
 * 类作用：   绩效工资基数录入
 * 建立人：   肖合明
 * 建立时间： 2009/09/11
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
    public class PerformanceRoyaltyBaseDBHelper
    {
        #region 查询：绩效工资基数录入信息
        /// <summary>
        /// 绩效工资基数录入信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetInfo(PerformanceRoyaltyBaseModel model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("SELECT a.ID                                          ");
            strSql.AppendLine(",a.EmployeeID                                        ");
            strSql.AppendLine(",case a.EmployeeID when '0' then '默认' else b.EmployeeName end as EmployeeName ");
            strSql.AppendLine(",a.BaseMoney                                         ");
            strSql.AppendLine(",a.TaskFlag");
            strSql.AppendLine(",case a.TaskFlag when '1' then '月考核系数' when '2' then '季度考核系数' when '3' then '半年考核系数' when '4' then '年考核系数' end as TaskFlagName     ");
            strSql.AppendLine("FROM officedba.PerformanceRoyaltyBase a              ");
            strSql.AppendLine("left join officedba.EmployeeInfo b on a.EmployeeID=b.ID ");
            strSql.AppendLine("where a.CompanyCD=@CompanyCD						");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.EmployeeID))
            {
                strSql.AppendLine(" and a.EmployeeID = @EmployeeID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));
            }
            if (!string.IsNullOrEmpty(model.TaskFlag))
            {
                strSql.AppendLine(" and a.TaskFlag = @TaskFlag");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag", model.TaskFlag));
            }
            comm.CommandText = strSql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);

        }
        #endregion

        #region 插入
        /// <summary>
        /// 插入一条新的数据，如果成功给Model.ID赋值主键，否则Model.ID="0"
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool InSert(PerformanceRoyaltyBaseModel Model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("insert into officedba.PerformanceRoyaltyBase(");
            strSql.AppendLine("EmployeeID,CompanyCD,BaseMoney,TaskFlag,ModifiedUserID,ModifiedDate)");
            strSql.AppendLine(" values (");
            strSql.AppendLine("@EmployeeID,@CompanyCD,@BaseMoney,@TaskFlag,@ModifiedUserID,getdate())");
            strSql.AppendLine(";set @IndexID= @@IDENTITY");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();
            SqlParameter IndexID = new SqlParameter("@IndexID", SqlDbType.Int);
            IndexID.Direction = ParameterDirection.Output;
            comm.Parameters.Add(IndexID);
            SetSaveParameter(comm, Model);
            bool result = SqlHelper.ExecuteTransWithCommand(comm);
            if (result)
            {
                Model.ID = comm.Parameters["@IndexID"].Value.ToString();
            }
            else
            {
                Model.ID = "0";
            }
            return result;
        }

        #endregion


        #region 判断插入之前，改设置是否已经存在
        /// <summary>
        /// 存在则返回true
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool ifEitst(PerformanceRoyaltyBaseModel Model)
        {
            string strSql = "select count(*) from officedba.PerformanceRoyaltyBase where CompanyCD='" + Model.CompanyCD + "' and EmployeeID=" + Model.EmployeeID + " and TaskFlag='" + Model.TaskFlag + "'";
            return SqlHelper.Exists(strSql, null);
        }

        #endregion

        #region 更新
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool Update(PerformanceRoyaltyBaseModel Model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.PerformanceRoyaltyBase set ");
            strSql.AppendLine("EmployeeID=@EmployeeID,");
            strSql.AppendLine("CompanyCD=@CompanyCD,");
            strSql.AppendLine("BaseMoney=@BaseMoney,");
            strSql.AppendLine("TaskFlag=@TaskFlag,");
            strSql.AppendLine("ModifiedUserID=@ModifiedUserID,");
            strSql.AppendLine("ModifiedDate=getdate()");
            strSql.AppendLine(" where ID=@ID ");
            SqlCommand comm = new SqlCommand(strSql.ToString());
            SetSaveParameter(comm, Model);
            return SqlHelper.ExecuteTransWithCommand(comm);

        }
        #endregion


        #region 保存时参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void SetSaveParameter(SqlCommand comm, PerformanceRoyaltyBaseModel model)
        {
            if (model.ID != "0")
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID ", model.ID));//自动生成
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID ", model.EmployeeID));//分公司ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BaseMoney ", model.BaseMoney));//数量
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag ", model.TaskFlag));//考核类型
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新人

        }
        #endregion


        #region 删除
        /// <summary>
        /// 删除事件（通过ID数组删除）
        /// </summary>
        /// <param name="StrID"></param>
        /// <returns></returns>
        public static bool DoDelete(string StrID, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.PerformanceRoyaltyBase ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" ID In( " + StrID + ")");
            deleteSql.AppendLine(" AND CompanyCD = @CompanyCD ");
            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            //comm.Parameters.Add(SqlHelper.GetParameter("@ID", StrID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));

            return SqlHelper.ExecuteTransWithCommand(comm);
        }

        #endregion
    }
}

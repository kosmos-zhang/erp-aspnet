/**********************************************
 * 类作用：   社会保险录入
 * 建立人：   吴志强
 * 建立时间： 2009/05/12
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

namespace XBase.Data.Office.HumanManager
{
   
    public class InsuEmployeeDBHelper
    {
        #region 查询社会保险录入信息
        /// <summary>
        /// 查询社会保险录入信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable SearchInsuEmployeeInfo(string companyCD)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                  ");
            searchSql.AppendLine(" 	 ID                    ");
            searchSql.AppendLine(" 	,CompanyCD             ");
            searchSql.AppendLine(" 	,EmployeeID            ");
            searchSql.AppendLine(" 	,StartDate             ");
            searchSql.AppendLine(" 	,Addr                  ");
            searchSql.AppendLine(" 	,InsuranceID           ");
            searchSql.AppendLine(" 	,InsuranceBase         ");
            searchSql.AppendLine(" FROM                    ");
            searchSql.AppendLine(" 	officedba.InsuEmployee ");
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
        #endregion


        #region 查询社会保险录入信息
        /// <summary>
        /// 查询社会保险录入信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetInsuEmployeeInfo(string companyCD)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                            ");
            searchSql.AppendLine(" 	 A.ID                            ");
            searchSql.AppendLine(" 	,A.CompanyCD                     ");
            searchSql.AppendLine(" 	,A.EmployeeID                    ");
            searchSql.AppendLine(" 	,A.StartDate                     ");
            searchSql.AppendLine(" 	,A.Addr                          ");
            searchSql.AppendLine(" 	,A.InsuranceID                   ");
            searchSql.AppendLine(" 	,A.InsuranceBase                 ");
            searchSql.AppendLine(" 	,B.CompanyPayRate                ");
            searchSql.AppendLine(" 	,B.PersonPayRate                 ");
            searchSql.AppendLine(" FROM                              ");
            searchSql.AppendLine(" 	officedba.InsuEmployee A         ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.InsuSocial B ");
            searchSql.AppendLine(" 	  ON B.CompanyCD=A.CompanyCD  and B.ID = A.InsuranceID        ");
            searchSql.AppendLine(" WHERE                             ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD         ");
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
        #endregion

        #region 保存社会保险录入信息
        /// <summary>
        /// 保存社会保险录入信息
        /// </summary>
        /// <param name="lstEdit">社会保险录入信息</param>
        /// <param name="modifyUserID">最后修改人</param>
        /// <returns></returns>
        public static bool SaveInsuEmployeeInfo(ArrayList lstEdit, string companyCD, string modifyUserID)
        {
            //定义返回变量
            bool isSucc = true;
            //信息存在时，进行操作
            if (lstEdit != null && lstEdit.Count > 0)
            {
                //保存库列表
                ArrayList lstSave = new ArrayList();
                //遍历所有社会保险录入，进行增删改操作
                for (int i = 0; i < lstEdit.Count; i++)
                {
                    //获取值
                    InsuEmployeeModel model = (InsuEmployeeModel)lstEdit[i];
                    //设置最后修改人
                    model.ModifiedUserID = modifyUserID;
                    //设置公司代码
                    model.CompanyCD = companyCD;
                    //更新
                    if ("1".Equals(model.EditFlag))
                    {
                        //执行更新操作
                        lstSave.Add(UpdateInsuEmployeeInfo(model));
                    }
                    //插入
                    else if ("0".Equals(model.EditFlag))
                    {
                        //执行插入操作
                        lstSave.Add(InsertInsuEmployeeInfo(model));
                    }
                    else
                    {
                        //执行删除操作
                        lstSave.Add(DeleteInsuEmployeeInfo(model.CompanyCD, model.EmployeeID, model.InsuranceID));
                    }
                }
                //执行保存操作
                isSucc = SqlHelper.ExecuteTransWithArrayList(lstSave);
            }

            return isSucc;
        }
        #endregion

        #region 新建社会保险录入信息
        /// <summary>
        /// 新建社会保险录入信息 
        /// </summary>
        /// <param name="model">社会保险录入信息</param>
        /// <returns></returns>
        private static SqlCommand InsertInsuEmployeeInfo(InsuEmployeeModel model)
        {
            #region 登陆SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO            ");
            insertSql.AppendLine(" officedba.InsuEmployee ");
            insertSql.AppendLine(" 	(CompanyCD            ");
            insertSql.AppendLine(" 	,EmployeeID           ");
            insertSql.AppendLine(" 	,StartDate            ");
            insertSql.AppendLine(" 	,Addr                 ");
            insertSql.AppendLine(" 	,InsuranceID          ");
            insertSql.AppendLine(" 	,InsuranceBase        ");
            insertSql.AppendLine(" 	,ModifiedDate         ");
            insertSql.AppendLine(" 	,ModifiedUserID)      ");
            insertSql.AppendLine(" VALUES                 ");
            insertSql.AppendLine(" 	(@CompanyCD           ");
            insertSql.AppendLine(" 	,@EmployeeID          ");
            insertSql.AppendLine(" 	,@StartDate           ");
            insertSql.AppendLine(" 	,@Addr                ");
            insertSql.AppendLine(" 	,@InsuranceID         ");
            insertSql.AppendLine(" 	,@InsuranceBase       ");
            insertSql.AppendLine(" 	,getdate()            ");
            insertSql.AppendLine(" 	,@ModifiedUserID)     ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //执行插入并返回插入结果
            return comm;
        }
        #endregion

        #region 更新社会保险录入信息
        /// <summary>
        /// 更新社会保险录入信息
        /// </summary>
        /// <param name="model">社会保险录入信息</param>
        /// <returns></returns>
        private static SqlCommand UpdateInsuEmployeeInfo(InsuEmployeeModel model)
        {

            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE                             ");
            updateSql.AppendLine(" officedba.InsuEmployee             ");
            updateSql.AppendLine(" SET  StartDate = @StartDate        ");
            updateSql.AppendLine(" 	,Addr = @Addr                     ");
            updateSql.AppendLine(" 	,InsuranceBase = @InsuranceBase   ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");   
            updateSql.AppendLine(" 	AND InsuranceID = @InsuranceID    ");
            updateSql.AppendLine(" 	AND EmployeeID = @EmployeeID      ");
         
            #endregion

            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //其他参数
            SetSaveParameter(comm, model);
            //执行更新
            return comm;
        }
        #endregion

        #region 保存时参数设置
        /// <summary>
        /// 保存时参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">员工社会保险信息</param>
        private static void SetSaveParameter(SqlCommand comm, InsuEmployeeModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));//员工ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InsuranceID", model.InsuranceID));//保险ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));//参保时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Addr", model.Addr));//参保地
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InsuranceBase", model.InsuranceBase));//金额 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//更新用户ID
        }
        #endregion

        #region 删除社会保险录入信息
        /// <summary>
        /// 删除社会保险录入信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="emplID">员工ID</param>
        /// <param name="insuranceID">社会保险ID</param>
        /// <returns></returns>
        private static SqlCommand DeleteInsuEmployeeInfo(string companyCD, string emplID, string insuranceID)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.InsuEmployee ");
            deleteSql.AppendLine(" WHERE                              ");
            deleteSql.AppendLine(" 	CompanyCD = @CompanyCD            ");       
            deleteSql.AppendLine(" 	AND InsuranceID = @InsuranceID    ");
            deleteSql.AppendLine(" 	AND EmployeeID = @EmployeeID      ");
  

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //员工ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", emplID));
            //社会保险项编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InsuranceID", insuranceID));
            //设置SQL语句
            comm.CommandText = deleteSql.ToString();

            //执行删除并返回
            return comm;
        }
        #endregion
        public static DataTable GetInsuEmployeeInf(string companyCD,string reportMonth)
        {
            int year = Convert.ToInt32(reportMonth.Substring(0, 4));
            int month = Convert.ToInt32(reportMonth.Substring(4, 2));
            int day = 0;
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                day = 31;
            }
            if (month == 2)
            {
                day = 28;
            }
            else
            {
                day = 30;
            }

            DateTime reportM = new DateTime(year, month, day);
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                            ");
            searchSql.AppendLine(" 	 A.ID                            ");
            searchSql.AppendLine(" 	,A.CompanyCD                     ");
            searchSql.AppendLine(" 	,A.EmployeeID                    ");
            searchSql.AppendLine(" 	,A.StartDate                     ");
            searchSql.AppendLine(" 	,A.Addr                          ");
            searchSql.AppendLine(" 	,A.InsuranceID                   ");
            searchSql.AppendLine(" 	,A.InsuranceBase                 ");
            searchSql.AppendLine(" 	,B.CompanyPayRate                ");
            searchSql.AppendLine(" 	,B.PersonPayRate                 ");
            searchSql.AppendLine(" FROM                              ");
            searchSql.AppendLine(" 	officedba.InsuEmployee A         ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.InsuSocial B ");
            searchSql.AppendLine(" 	  ON B.ID = A.InsuranceID        ");
            searchSql.AppendLine(" WHERE                             ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD         ");
            searchSql.AppendLine("  and 	@StartDate >= A.StartDate ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", reportM.ToShortDateString()));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
    }
}

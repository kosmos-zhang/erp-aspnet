/**********************************************
 * 类作用：   工资录入
 * 建立人：   吴志强
 * 建立时间： 2009/05/08
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
    /// <summary>
    /// 类名：SalaryEmployeeDBHelper
    /// 描述：工资录入
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/08
    /// 最后修改时间：2009/05/08
    /// </summary>
    ///
    public class SalaryEmployeeDBHelper
    {

        #region 查询工资录入信息
        /// <summary>
        /// 查询工资录入信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable SearchSalaryEmployeeInfo(string companyCD)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                    ");
            searchSql.AppendLine(" 	 ID                      ");
            searchSql.AppendLine(" 	,EmployeeID              ");
            searchSql.AppendLine(" 	,CompanyCD               ");
            searchSql.AppendLine(" 	,Flag                    ");
            searchSql.AppendLine(" 	,ItemNo                  ");
            searchSql.AppendLine(" 	,SalaryMoney             ");
            searchSql.AppendLine(" FROM                      ");
            searchSql.AppendLine(" 	officedba.SalaryEmployee ");
            searchSql.AppendLine(" WHERE                     ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD   ");
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


        #region 查询工资录入信息
        /// <summary>
        /// 查询工资录入信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetSalaryEmployeeInfo(string companyCD)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                      ");
            searchSql.AppendLine(" 	 A.ID                      ");
            searchSql.AppendLine(" 	,A.EmployeeID              ");
            searchSql.AppendLine(" 	,A.CompanyCD               ");
            searchSql.AppendLine(" 	,A.Flag                    ");
            searchSql.AppendLine(" 	,A.ItemNo                  ");
            searchSql.AppendLine(" 	,A.SalaryMoney             ");
            searchSql.AppendLine(" 	,B.ItemName                ");
            searchSql.AppendLine(" 	,B.ItemOrder               ");
            searchSql.AppendLine(" 	,B.PayFlag                 "); 
            searchSql.AppendLine(" 	,B.ChangeFlag ,isnull( B.Calculate,'') as  Calculate               ");
            searchSql.AppendLine(" 	,B.ChangeFlag ,isnull( B.ParamsList,'') as  ParamsList               ");
            searchSql.AppendLine(" FROM                        ");
            searchSql.AppendLine(" 	officedba.SalaryEmployee A ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.SalaryItem B ");
            searchSql.AppendLine(" 	ON   B.CompanyCD=A.CompanyCD  AND  A.ItemNo = B.ItemNo    ");
            searchSql.AppendLine(" WHERE                       ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD and B.PayFlag is not null  ");
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

        #region 保存工资录入信息
        /// <summary>
        /// 保存工资录入信息
        /// </summary>
        /// <param name="lstEdit">工资录入信息</param>
        /// <param name="modifyUserID">最后修改人</param>
        /// <returns></returns>
        public static bool SaveSalaryEmployeeInfo(ArrayList lstEdit, string companyCD, string modifyUserID)
        {
            //定义返回变量
            bool isSucc = true;
            //信息存在时，进行操作
            if (lstEdit != null && lstEdit.Count > 0)
            {
                //保存库列表
                ArrayList lstSave = new ArrayList();
                //遍历所有工资录入，进行增删改操作
                for (int i = 0; i < lstEdit.Count; i++)
                {
                    //获取值
                    SalaryEmployeeModel model = (SalaryEmployeeModel)lstEdit[i];
                    //设置最后修改人
                    model.ModifiedUserID = modifyUserID;
                    //设置公司代码
                    model.CompanyCD = companyCD;
                    //更新
                    if ("1".Equals(model.EditFlag))
                    {
                        //执行更新操作
                        lstSave.Add(UpdateSalaryEmployeeInfo(model));
                    }
                    //插入
                    else if ("0".Equals(model.EditFlag))
                    {
                        //执行插入操作
                        lstSave.Add(InsertSalaryEmployeeInfo(model));
                    }
                    else
                    {
                        //执行删除操作
                        lstSave.Add(DeleteSalaryEmployeeInfo(model.CompanyCD, model.EmployeeID, model.ItemNo));
                    }
                }
                //执行保存操作
                isSucc = SqlHelper.ExecuteTransWithArrayList(lstSave);
                //获取插入数据的ID
                for (int j = 0; j < lstEdit.Count; j++)
                {
                    //获取值
                    SalaryEmployeeModel model = (SalaryEmployeeModel)lstEdit[j];
                    //插入时
                    if ("0".Equals(model.EditFlag))
                    {
                        //获取插入的命令
                        SqlCommand comm = (SqlCommand)lstSave[j];
                        //设置工资录入编号
                        model.ID = comm.Parameters["@SalaryEmployeeID"].Value.ToString();
                    }
                }
            }

            return isSucc;
        }
        #endregion

        #region 新建工资录入信息
        /// <summary>
        /// 新建工资录入申请信息 
        /// </summary>
        /// <param name="model">工资录入申请信息</param>
        /// <returns></returns>
        private static SqlCommand InsertSalaryEmployeeInfo(SalaryEmployeeModel model)
        {
            #region 登陆SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO               ");
            insertSql.AppendLine(" 	officedba.SalaryEmployee ");
            insertSql.AppendLine(" 	(CompanyCD               ");
            insertSql.AppendLine(" 	,EmployeeID              ");
            insertSql.AppendLine(" 	,Flag                    ");
            insertSql.AppendLine(" 	,ItemNo                  ");
            insertSql.AppendLine(" 	,SalaryMoney             ");
            insertSql.AppendLine(" 	,ModifiedDate            ");
            insertSql.AppendLine(" 	,ModifiedUserID)         ");
            insertSql.AppendLine(" VALUES                    ");
            insertSql.AppendLine(" 	(@CompanyCD              ");
            insertSql.AppendLine(" 	,@EmployeeID             ");
            insertSql.AppendLine(" 	,@Flag                   ");
            insertSql.AppendLine(" 	,@ItemNo                 ");
            insertSql.AppendLine(" 	,@SalaryMoney            ");
            insertSql.AppendLine(" 	,getdate()               ");
            insertSql.AppendLine(" 	,@ModifiedUserID)        ");
            insertSql.AppendLine("   SET @SalaryEmployeeID = @@IDENTITY  ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@SalaryEmployeeID", SqlDbType.Int));

            //执行插入并返回插入结果
            return comm;
        }
        #endregion

        #region 更新工资录入申请信息
        /// <summary>
        /// 更新工资录入申请信息
        /// </summary>
        /// <param name="model">工资录入申请信息</param>
        /// <returns></returns>
        private static SqlCommand UpdateSalaryEmployeeInfo(SalaryEmployeeModel model)
        {

            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.SalaryEmployee    ");
            updateSql.AppendLine(" SET                                ");
            updateSql.AppendLine(" 	 Flag = @Flag                     ");
            updateSql.AppendLine(" 	,SalaryMoney = @SalaryMoney       ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");   
            updateSql.AppendLine(" 	AND ItemNo = @ItemNo              ");
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
        /// <param name="model">员工工资信息</param>
        private static void SetSaveParameter(SqlCommand comm, SalaryEmployeeModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));//员工ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", model.Flag));//区分
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemNo", model.ItemNo));//工资项编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SalaryMoney", model.SalaryMoney));//金额
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//更新用户ID
        }
        #endregion

        #region 删除工资录入信息
        /// <summary>
        /// 删除工资录入信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="emplID">员工ID</param>
        /// <param name="itemno">工资项编号</param>
        /// <returns></returns>
        private static SqlCommand DeleteSalaryEmployeeInfo(string companyCD, string emplID, string itemno)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.SalaryEmployee ");
            deleteSql.AppendLine(" WHERE                              ");
            deleteSql.AppendLine(" 	CompanyCD = @CompanyCD            ");     
            deleteSql.AppendLine(" 	AND ItemNo = @ItemNo              ");
            deleteSql.AppendLine(" 	AND EmployeeID = @EmployeeID      ");
       

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //员工ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", emplID));
            //工资项编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemNo", itemno));
            //设置SQL语句
            comm.CommandText = deleteSql.ToString();

            //执行删除并返回
            return comm;
        }
        #endregion
    }
}

/**********************************************
 * 类作用：   组织机构
 * 建立人：   吴志强
 * 建立时间： 2009/04/09
  * 修改人：   王保军
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

namespace XBase.Data.Office.HumanManager
{
 
    public class DeptInfoDBHelper
    {
        
            #region 根据主键获取部门名称
        /// <summary>
        /// 根据主键获取部门名称
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>部门名称</returns>
        public static DataTable GetDeptinfoByNo(string DeptNo)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select DeptName,superdeptID,id from officedba.DeptInfo");
            sql.AppendLine("where CompanyCD=@CompanyCD and  DeptNo=@DeptNo");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //主键参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptNo", DeptNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
          
        }
        #endregion
        #region 根据主键获取部门名称
        /// <summary>
        /// 根据主键获取部门名称
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>部门名称</returns>
        public static DataTable GetDeptNameByID(string ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select DeptName from officedba.DeptInfo");
            sql.AppendLine("where ID=@ID");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //主键参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
         
            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
          
        }
        #endregion

        #region 添加组织机构信息
        /// <summary>
        /// 添加组织机构信息
        /// </summary>
        /// <param name="model">组织机构信息</param>
        /// <returns></returns>
        public static bool InsertDeptInfo(DeptModel model)
        {

            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO officedba.DeptInfo ");
            insertSql.AppendLine("            (CompanyCD          ");
            insertSql.AppendLine("            ,DeptNO             ");
            insertSql.AppendLine("            ,SuperDeptID        ");
            insertSql.AppendLine("            ,PYShort            ");
            insertSql.AppendLine("            ,DeptName           ");
            insertSql.AppendLine("            ,AccountFlag        ");
            insertSql.AppendLine("            ,SaleFlag           ");
            insertSql.AppendLine("            ,SubFlag            ");
            insertSql.AppendLine("            ,Address            ");
            insertSql.AppendLine("            ,Post               ");
            insertSql.AppendLine("            ,LinkMan            ");
            insertSql.AppendLine("            ,Tel                ");
            insertSql.AppendLine("            ,Fax                ");
            insertSql.AppendLine("            ,Email              ");
            insertSql.AppendLine("            ,Duty               ");
            insertSql.AppendLine("            ,UsedStatus         ");
            insertSql.AppendLine("            ,Description        ");
            insertSql.AppendLine("            ,ModifiedDate       ");
            insertSql.AppendLine("            ,ModifiedUserID)    ");
            insertSql.AppendLine("      VALUES                    ");
            insertSql.AppendLine("            (@CompanyCD         ");
            insertSql.AppendLine("            ,@DeptNO            ");
            insertSql.AppendLine("            ,@SuperDeptID       ");
            insertSql.AppendLine("            ,@PYShort           ");
            insertSql.AppendLine("            ,@DeptName          ");
            insertSql.AppendLine("            ,@AccountFlag       ");
            insertSql.AppendLine("            ,@SaleFlag          ");
            insertSql.AppendLine("            ,@SubFlag           ");
            insertSql.AppendLine("            ,@Address           ");
            insertSql.AppendLine("            ,@Post              ");
            insertSql.AppendLine("            ,@LinkMan           ");
            insertSql.AppendLine("            ,@Tel               ");
            insertSql.AppendLine("            ,@Fax               ");
            insertSql.AppendLine("            ,@Email             ");
            insertSql.AppendLine("            ,@Duty              ");
            insertSql.AppendLine("            ,@UsedStatus        ");
            insertSql.AppendLine("            ,@Description       ");
            insertSql.AppendLine("            ,getdate()          ");
            insertSql.AppendLine("            ,@ModifiedUserID)   ");
            #endregion

            //定义插入基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);

        }
        #endregion

        #region 更新组织机构信息
        /// <summary>
        /// 更新组织机构信息
        /// </summary>
        /// <param name="model">保存信息</param>
        /// <returns></returns>
        public static bool UpdateDeptInfo(DeptModel model)
        {

            #region 更新SQL拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.DeptInfo          ");
            updateSql.AppendLine(" SET                                ");
            updateSql.AppendLine(" 	 PYShort = @PYShort               ");
            updateSql.AppendLine(" 	,DeptName = @DeptName             ");
            updateSql.AppendLine(" 	,AccountFlag = @AccountFlag       ");
            updateSql.AppendLine(" 	,SaleFlag = @SaleFlag             ");
            updateSql.AppendLine(" 	,SubFlag = @SubFlag               ");
            updateSql.AppendLine(" 	,Address = @Address               ");
            updateSql.AppendLine(" 	,Post = @Post                     ");
            updateSql.AppendLine(" 	,LinkMan = @LinkMan               ");
            updateSql.AppendLine(" 	,Tel = @Tel                       ");
            updateSql.AppendLine(" 	,Fax = @Fax                       ");
            updateSql.AppendLine(" 	,Email = @Email                   ");
            updateSql.AppendLine(" 	,Duty = @Duty ,SuperDeptID=@SuperDeptID                    ");
            updateSql.AppendLine(" 	,UsedStatus = @UsedStatus         ");
            updateSql.AppendLine(" 	,Description = @Description       ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            updateSql.AppendLine(" 	AND DeptNO = @DeptNO              ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">保存信息</param>
        private static void SetSaveParameter(SqlCommand comm, DeptModel model)
        {
            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptNO", model.DeptNO));//机构编号
            //插入时，设置上级机构ID
            if (ConstUtil.EDIT_FLAG_INSERT.Equals(model.EditFlag))
            {
               
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SuperDeptID", model.SuperDeptID));//上级机构ID（对应本表的ID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", model.PYShort));//机构拼音代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptName", model.DeptName));//机构名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AccountFlag", model.AccountFlag));//是否独立核算(0否，1是)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SaleFlag", model.SaleFlag));//是否为零售店（0否，1是）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SubFlag", model.SubFlag));//是否为分公司（0否，1是）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Address", model.Address));//地址
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Post", model.Post));//邮编
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@LinkMan", model.LinkMan));//联系人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel", model.Tel));//电话
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Fax", model.Fax));//传真
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Email", model.Email));//Email
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Duty", model.Duty));//职责
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));//启用状态(0停用，1启用)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Description", model.Description));//描述
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//最后更新用户ID
        }
        #endregion

        #region 通过检索条件查询组织机构信息
        /// <summary>
        /// 查询组织机构信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="deptID">组织机构ID</param>
        /// <returns></returns>
        public static DataTable SearchDeptInfo(string companyCD, string deptID)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                    ");
            searchSql.AppendLine(" 	 A.ID                    ");
            searchSql.AppendLine(" 	,A.DeptNO                ");
            searchSql.AppendLine(" 	,A.SuperDeptID           ");
            searchSql.AppendLine(" 	,A.DeptName              ");
            searchSql.AppendLine(" 	,(SELECT COUNT(ID)       ");
            searchSql.AppendLine(" 		FROM                 ");
            searchSql.AppendLine(" 		officedba.DeptInfo B ");
            searchSql.AppendLine(" 		WHERE                ");
            searchSql.AppendLine(" 		B.CompanyCD=A.CompanyCD and B.SuperDeptID = A.ID  )");
            searchSql.AppendLine(" 	AS SubCount              ");
            searchSql.AppendLine(" FROM                      ");
            searchSql.AppendLine(" 	officedba.DeptInfo A     ");
            searchSql.AppendLine(" WHERE                     ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));

            //组织机构ID未输入时，查询
            if (string.IsNullOrEmpty(deptID))
            {
                searchSql.AppendLine(" AND A.SuperDeptID IS NULL ");
            }
            //获取子组织机构
            else
            {
                searchSql.AppendLine(" AND A.SuperDeptID = @SuperDeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SuperDeptID", deptID));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        public static DataTable ISDequter(string companyCD, string deptID)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                    ");
            searchSql.AppendLine(" 	 *                  ");

            searchSql.AppendLine(" FROM                      ");
            searchSql.AppendLine(" 	officedba.DeptQuarter       ");
            searchSql.AppendLine(" WHERE                     ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD AND  DeptID = @DeptID ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", deptID));
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #region 通过ID查询组织机构信息
        /// <summary>
        /// 查询组织机构信息
        /// </summary>
        /// <param name="deptID">组织机构ID</param>
        /// <returns></returns>
        public static DataTable GetDeptInfoWithID(string deptID, string companyCD)
        {
            #region 查询考试信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                          ");
            searchSql.AppendLine(" 	 A.DeptNO                      ");
            searchSql.AppendLine(" 	,A.SuperDeptID                 ");
            searchSql.AppendLine(" 	,B.DeptName AS SuperDeptName   ");
            searchSql.AppendLine(" 	,A.PYShort                     ");
            searchSql.AppendLine(" 	,A.DeptName                    ");
            searchSql.AppendLine(" 	,A.AccountFlag                 ");
            searchSql.AppendLine(" 	,A.SaleFlag                    ");
            searchSql.AppendLine(" 	,A.SubFlag                     ");
            searchSql.AppendLine(" 	,A.Address                     ");
            searchSql.AppendLine(" 	,A.Post                        ");
            searchSql.AppendLine(" 	,A.LinkMan                     ");
            searchSql.AppendLine(" 	,A.Tel                         ");
            searchSql.AppendLine(" 	,A.Fax                         ");
            searchSql.AppendLine(" 	,A.Email                       ");
            searchSql.AppendLine(" 	,A.Duty                        ");
            searchSql.AppendLine(" 	,A.UsedStatus                  ");
            searchSql.AppendLine(" 	,A.Description                 ");
            searchSql.AppendLine(" FROM                            ");
            searchSql.AppendLine(" 	officedba.DeptInfo A           ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo B ");
            searchSql.AppendLine(" 		ON   A.companyCD=B.companyCD and A.SuperDeptID = B.ID   ");
            searchSql.AppendLine(" WHERE                           ");
            searchSql.AppendLine(" 	 A.companyCD=@companyCD     and    A.ID = @DeptID        ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //组织机构ID
            param[0] = SqlHelper.GetParameter("@DeptID", deptID);
            param[1] = SqlHelper.GetParameter("@companyCD", companyCD );
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }

        #endregion

        #region 删除组织机构信息
        /// <summary>
        /// 删除组织机构信息
        /// </summary>
        /// <param name="deptID">组织机构ID</param>
        /// <returns></returns>
        public static bool DeleteDeptInfo(string deptID, string CompanyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.DeptInfo ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" CompanyCD=@CompanyCD and  ID = @DeptID");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", deptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        public static int  ISHavePerson(string companyCD, string deptID)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select count(*) as EmpCount from  officedba.EmployeeInfo where  CompanyCD = @CompanyCD AND  DeptID = @DeptID                 ");
             
            #endregion
            SqlParameter[] param = new SqlParameter[2];
            //招聘活动ID
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            param[1] = SqlHelper.GetParameter("@DeptID", deptID);


         
         
            //执行查询
            return Convert .ToInt32 ( SqlHelper.ExecuteScalar (searchSql.ToString () ,param ));
        }
        #region 根据主键获取部门名称,人员名称
        /// <summary>
        /// 根据主键获取部门名称
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>部门名称</returns>
        public static DataTable GetEmployeeByID(string employeeID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select isnull(b.DeptName,'') as DeptName, isnull(a.EmployeeName,'') as EmployeeName  from officedba.EmployeeInfo as a");
            sql.AppendLine("left outer join officedba.DeptInfo as b on b.id=a.deptid");
            sql.AppendLine("where a.ID=@ID");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //主键参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", employeeID));

            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion
    

 
    }
}

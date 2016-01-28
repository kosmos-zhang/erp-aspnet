/**********************************************
 * 类作用   站点用户数据处理层
 * 创建人   xz
 * 创建时间 2010-3-15 16:11:04 
 ***********************************************/

using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using XBase.Data.DBHelper;

using XBase.Model.CustomAPI.CustomWebSite;


namespace XBase.Data.CustomAPI.CustomWebSite
{
    /// <summary>
    /// 站点用户数据处理类
    /// </summary>
    public class WebSiteCustomInfoDBHelper
    {
        #region 字段

        //sql语句
        private const string C_SELECT_ALL =
                                " SELECT ID,CustomID,LoginUserName,LoginPassword,Status,IsMember" +
                                " FROM websitedba.WebSiteCustomInfo";
        private const string C_SELECT_ID =
                                " SELECT ID,CustomID,LoginUserName,LoginPassword,Status,IsMember" +
                                " FROM websitedba.WebSiteCustomInfo" +
                                " WHERE ID=@ID";
        private const string C_SELECT =
                                " SELECT ID,CustomID,LoginUserName,LoginPassword,Status,IsMember" +
                                " FROM websitedba.WebSiteCustomInfo" +
                                " WHERE CustomID=@CustomID AND LoginUserName=@LoginUserName ";
        private const string C_INSERT =
                                " INSERT websitedba.WebSiteCustomInfo(" +
                                "    CustomID,LoginUserName,LoginPassword,Status,IsMember )" +
                                " VALUES (" +
                                "    @CustomID,@LoginUserName,@LoginPassword,@Status,@IsMember )";
        private const string C_UPDATE =
                                " UPDATE websitedba.WebSiteCustomInfo SET" +
                                "    CustomID=@CustomID,LoginUserName=@LoginUserName,LoginPassword=@LoginPassword,Status=@Status" +
                                "    ,IsMember=@IsMember" +
                                " WHERE ID=@ID";
        private const string C_DELETE =
                                " DELETE FROM websitedba.WebSiteCustomInfo WHERE CustomID=@CustomID AND LoginUserName=@LoginUserName";

        private const string C_DELETE_ID =
                                " DELETE FROM websitedba.WebSiteCustomInfo WHERE ID IN ({0}) ";


        //字段顺序变量定义
        private const byte m_iDCol = 0; // 主键，自动生成列
        private const byte m_customIDCol = 1; // 往来单位ID列
        private const byte m_loginUserNameCol = 2; // 站点登陆用户名列
        private const byte m_loginPasswordCol = 3; // 站点登陆密码列
        private const byte m_statusCol = 4; // 状态1 启用0 禁用列
        private const byte m_isMemberCol = 5; // 是否会员1 是0 否列
        #endregion

        #region 方法

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD"></param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int iD)
        {
            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_SELECT_ID);

            // 参数设置
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.Int, 4)
                        };
            parameters[0].Value = iD; // 

            // 执行
            return SqlHelper.ExecuteSql(sqlSentence.ToString(), parameters);
        }

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="customID">往来单位ID</param>
        /// <param name="loginUserName">站点登陆用户名</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int customID, string loginUserName)
        {
            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_SELECT);

            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, customID, loginUserName);

            // 执行
            return SqlHelper.ExecuteSql(sqlSentence.ToString(), parameters);
        }

        /// <summary>
        /// 列表界面查询方法
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页记录数</param>
        /// <param name="orderBy">排序方法</param>
        /// <param name="TotalCount">总记录数</param>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public static DataTable SelectListData(int pageIndex, int pageCount, string orderBy, ref int TotalCount
            , WebSiteCustomInfoModel model, string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder(@"SELECT wsci.ID,wsci.CustomID,wsci.LoginUserName,wsci.LoginPassword,wsci.Status,wsci.IsMember,ci.CustName
                                                    FROM websitedba.WebSiteCustomInfo wsci
                                                    INNER JOIN officedba.CustInfo ci ON wsci.CustomID=ci.ID AND ci.CompanyCD='" + CompanyCD + "'");
            sql.Append(" WHERE 1=1 ");
            if (!String.IsNullOrEmpty(model.LoginUserName))
            {
                sql.AppendFormat(" AND wsci.LoginUserName LIKE '%{0}%'", model.LoginUserName);
            }
            if (!String.IsNullOrEmpty(model.LoginPassword))
            {
                sql.AppendFormat(" AND ci.CustName LIKE '%{0}%'", model.LoginPassword);
            }
            if (!String.IsNullOrEmpty(model.IsMember))
            {
                sql.AppendFormat(" AND wsci.IsMember='{0}'", model.IsMember);
            }
            if (!String.IsNullOrEmpty(model.Status))
            {
                sql.AppendFormat(" AND wsci.Status='{0}'", model.Status);
            }

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }

        /// <summary>
        /// 插入操作的执行命令
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>插入操作的执行命令</returns>
        public static SqlCommand InsertCommand(WebSiteCustomInfoModel model)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_INSERT + " SET @IndexID = @@IDENTITY ";
            // 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);

            comm.Parameters.AddRange(parameters);
            SqlParameter IndexID = new SqlParameter("@IndexID", SqlDbType.Int);
            IndexID.Direction = ParameterDirection.Output;
            comm.Parameters.Add(IndexID);

            return comm;
        }

        /// <summary>
        /// 修改数据记录
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>修改操作的执行命令</returns>
        public static SqlCommand UpdateCommand(WebSiteCustomInfoModel model)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_UPDATE;
            // 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);

            comm.Parameters.AddRange(parameters);

            return comm;
        }

        /// <summary>
        /// 修改数据记录
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>修改数据是否成功:true成功,false不成功</returns>
        public static bool Update(WebSiteCustomInfoModel model)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_UPDATE);

            // 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);

            //执行SQL
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;

            return returnValue;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">ID集合</param>
        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommand(string iD)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();

            comm.CommandText = string.Format(C_DELETE_ID, iD);

            return comm;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">ID集合</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(string iD)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(string.Format(C_DELETE_ID, iD));


            // 执行
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), new SqlParameter[] { }) > 0;

            return returnValue;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="customID">往来单位ID</param>
        ///<param name="loginUserName">站点登陆用户名</param>
        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommand(int customID, string loginUserName)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_DELETE;
            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, customID, loginUserName);

            comm.Parameters.AddRange(parameters);

            return comm;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="customID">往来单位ID</param>
        ///<param name="loginUserName">站点登陆用户名</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(int customID, string loginUserName)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_DELETE);

            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, customID, loginUserName);


            // 执行
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;

            return returnValue;
        }


        /// <summary>
        /// 设置查询和删除的参数数组
        /// </summary>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetSelectAndDeleteParameters()
        {
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@CustomID", SqlDbType.Int, 4), // 往来单位ID
							new SqlParameter("@LoginUserName", SqlDbType.VarChar, 100) // 站点登陆用户名
                        };

            return parameters;
        }


        /// <summary>
        /// 设置新增和修改的参数数组
        /// </summary>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetInsertAndUpdateParameters()
        {
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.Int,4), // 主键，自动生成
                            new SqlParameter("@CustomID", SqlDbType.Int,4), // 往来单位ID
                            new SqlParameter("@LoginUserName", SqlDbType.VarChar,100), // 站点登陆用户名
                            new SqlParameter("@LoginPassword", SqlDbType.VarChar,100), // 站点登陆密码
                            new SqlParameter("@Status", SqlDbType.VarChar,1), // 状态1 启用0 禁用
                            new SqlParameter("@IsMember", SqlDbType.VarChar,1)  // 是否会员1 是0 否
                        };

            return parameters;
        }


        /// <summary>
        /// 设置查询和删除的参数数组的值，此方法适用于两个字段作为主键的情况
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="customID">往来单位ID的值</param>
        /// <param name="loginUserName">站点登陆用户名的值</param>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetSelectAndDeleteParametersValue(SqlParameter[] parameters, int customID, string loginUserName)
        {
            parameters[0].Value = customID; // 往来单位ID
            parameters[1].Value = loginUserName; // 站点登陆用户名

            return parameters;
        }


        /// <summary>
        /// 设置新增和修改的参数数组的值
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="model">实体类</param>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetInsertAndUpdateParametersValue(SqlParameter[] parameters, WebSiteCustomInfoModel model)
        {
            if (!model.ID.HasValue) parameters[m_iDCol].Value = System.DBNull.Value; else parameters[m_iDCol].Value = model.ID; // 主键，自动生成
            if (!model.CustomID.HasValue) parameters[m_customIDCol].Value = System.DBNull.Value; else parameters[m_customIDCol].Value = model.CustomID; // 往来单位ID
            parameters[m_loginUserNameCol].Value = model.LoginUserName; // 站点登陆用户名
            parameters[m_loginPasswordCol].Value = model.LoginPassword; // 站点登陆密码
            parameters[m_statusCol].Value = model.Status; // 状态1 启用0 禁用
            parameters[m_isMemberCol].Value = model.IsMember; // 是否会员1 是0 否

            return parameters;
        }


        #endregion

        #region 自定义
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>修改数据是否成功:true成功,false不成功</returns>
        public static bool UpdatePassWord(WebSiteCustomInfoModel model)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            string sqlStr = "UPDATE websitedba.WebSiteCustomInfo SET LoginPassword=@LoginPassword WHERE ID=@ID";

            // 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);

            //执行SQL
            returnValue = SqlHelper.ExecuteTransSql(sqlStr, parameters) > 0;

            return returnValue;
        }


        //public static bool SetPassword(WebSiteCustomInfoModel mode)
        //{
        //    StringBuilder sbSql = new StringBuilder();
        //    sbSql.AppendLine(" UPDATE websitedba.WebSiteCustomInfo SET LoginPassword=@LoginPassword WHERE ID=@ID");
        //}


        /// <summary>
        /// 根据用户名和密码查询用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public static DataTable Login(string userName, string passWord)
        {
            //string sqlStr = " SELECT * FROM websitedba.WebSiteCustomInfo WHERE LoginUserName='{0}' AND LoginPassword='{1}'";
            //sqlStr = String.Format(sqlStr, userName, passWord);
            //return SqlHelper.ExecuteSql(sqlStr);

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT a.*,b.CompanyCD,b.CustName,c.NameCn AS CompanyName");
            sbSql.AppendLine(" FROM websitedba.WebSiteCustomInfo AS a ");
            sbSql.AppendLine(" LEFT JOIN officedba.CustInfo AS b ON a.CustomID=b.ID");
            sbSql.AppendLine(" LEFT JOIN pubdba.company AS c ON c.CompanyCD=b.CompanyCD ");
            sbSql.AppendLine(" WHERE a.LoginUserName=@LoginUserName AND a.LoginPassword=@LoginPassword");

            SqlParameter[] paras = new SqlParameter[2];
            int index = 0;
            paras[index++] = SqlHelper.GetParameter("@LoginUserName", userName);
            paras[index++] = SqlHelper.GetParameter("@LoginPassword", passWord);

            return SqlHelper.ExecuteSql(sbSql.ToString(), paras);
        }

        /// <summary>
        /// 读取用户信息
        /// </summary>
        /// <param name="userName">登陆用户名</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetUserInfo(string userName, string CompanyCD)
        {
            string sqlStr = " SELECT * FROM websitedba.WebSiteCustomInfo WHERE LoginUserName='{0}' AND CustomID={1}";

            sqlStr = String.Format(sqlStr, userName, CompanyCD);

            return SqlHelper.ExecuteSql(sqlStr);
        }

        /// <summary>
        /// 判断用户名是否已经存在
        /// </summary>
        /// <param name="Name">用户名</param>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        public static bool ExisitName(string Name, int? ID)
        {
            string sqlStr = " SELECT ID FROM websitedba.WebSiteCustomInfo  WHERE LoginUserName='" + Name + "'";
            if (ID.HasValue)
            {
                sqlStr += " AND ID<>" + ID.Value.ToString();
            }
            return SqlHelper.Exists(sqlStr, null);
        }
        #endregion
    }
}
/**********************************************
 * 类作用：   角色管理数据库层处理
 * 建立人：   陶春
 * 建立时间： 2009/01/10
 ***********************************************/

using System;
using XBase.Model.Office.SystemManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
namespace XBase.Data.Office.SystemManager
{
    public class RoleInfoDBHelper
    {
        /// <summary>
        /// 获取角色信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetRoleInfo(string CompanyCD)
        {
            string sql = "select RoleID,CompanyCD,RoleName,ModifiedDate,ModifiedUserID,remark from officedba.RoleInfo where CompanyCD=@CompanyCD and (IsRoot!='1'or IsRoot is null)";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            return SqlHelper.ExecuteSql(sql,param);
        }
        /// <summary>
        /// 根据角色ID获取信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetRoleInfoByID(int ID)
        {
            string sql = "select RoleID,CompanyCD,RoleName,ModifiedDate,remark from officedba.RoleInfo where RoleID=@ID";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@ID", ID);
            return SqlHelper.ExecuteSql(sql,param);
        }

        /// <summary>
        /// 获取角色信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetRoleInfo(string CompanyCD, string RoleName, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string sql = "select RoleID,CompanyCD,RoleName,isnull( CONVERT(CHAR(19), ModifiedDate, 120),'') as ModifiedDate,ModifiedUserID,remark from officedba.RoleInfo where RoleID>0 and (IsRoot!='1'or IsRoot is null) ";
            //企业代码不为空
            if (CompanyCD != "" && CompanyCD != null)
            {
                sql += " and CompanyCD='" + CompanyCD + "'";
            }
            //角色名称条件不为空
            if (RoleName != null && RoleName != "")
            {
                sql += " and RoleName like '%" + RoleName + "%'";
            }
            //return SqlHelper.ExecuteSql(sql);
            DataTable dt = SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, OrderBy, null, ref totalCount);
            return dt;
        }

        /// <summary>
        /// 获取角色对应功能信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetRoleFunction(string CompanyCD, string RoleID, string Mod, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string allmod = "";
            string[] Idsm = null;
            Mod = Mod.Substring(0, Mod.Length);
            Idsm = Mod.Split(',');
            StringBuilder sbm = new System.Text.StringBuilder();
            for (int i = 0; i < Idsm.Length; i++)
            {
                Idsm[i] = "'" + Idsm[i] + "'";
                sbm.Append(Idsm[i]);
            }
            allmod = sbm.ToString().Replace("''", "','");
            string sql = "SELECT id, isnull(FunctionID,'')as FunctionID,isnull(FunctionCD,'')as FunctionCD,";
            sql+="isnull(FunctionName,'')as FunctionName, ";
            sql+="isnull(CompanyCD,'')as CompanyCD, ";
            sql+="isnull(ModuleID,'')as ModuleID, ";
            sql+="isnull(ModuleName,'')as ModuleName," ;
            sql+="isnull(RoleID,'')as RoleID," ;
            sql+="isnull(RoleName,'')as RoleName,";
            sql += "isnull( CONVERT(CHAR(10), ModifiedDate, 23),'') as ModifiedDate,";
            sql+="isnull(ModifiedUserID,'')as ModifiedUserID ";
            sql += "from officedba.V_RoleFunction where RoleID>0   and  (IsRoot!='1'or IsRoot is null)";
            if (CompanyCD != "")
            {
                sql += " and CompanyCD=@CompanyCD";
            }
            if (RoleID != "")
            {
                sql += " and RoleID=@RoleID";
            }
            if (!string.IsNullOrEmpty(Mod))
            {
                sql += " AND ModuleName in(" + allmod + ")";
            }
            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            param[1] = SqlHelper.GetParameter("@RoleID", RoleID);
            DataTable dt_Role = SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, OrderBy, param, ref totalCount);
            return dt_Role;
        }

        /// <summary>
        /// 获取公司最大的角色数
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetMaxRoleCount(string CompanyCD)
        {
            String sql = "select officedba.IsRoleInfoOverflow('" + CompanyCD + "');";
            //执行sql语句
            DataTable DataTable = SqlHelper.ExecuteSql(sql);
            return DataTable;
          
        }

        ///// <summary>
        ///// 删除角色记录
        ///// </summary>
        ///// <returns>DataTable</returns>
        //public static void DeleteRoleInfo(string RoleIDArray)
        //{
        //    string sql = "delete from officedba.RoleInfo where RoleID in(" + RoleIDArray + ")";
        //    SqlHelper.ExecuteTransSql(sql);
        //}


        /// <summary>
        /// 删除角色记录
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool DeleteRoleInfo(string RoleId, string CompanyCD)
        {

            ArrayList listADD = new ArrayList();
            string allRoleId = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] Delsql = new string[1];
            try
            {
                string[] RoleIdS = null;
                RoleId = RoleId.Substring(0, RoleId.Length);
                RoleIdS = RoleId.Split(',');

                for (int i = 0; i < RoleIdS.Length; i++)
                {
                    RoleIdS[i] = "'" + RoleIdS[i] + "'";
                    sb.Append(RoleIdS[i]);
                }
                //allUserID = sb.ToString();
                allRoleId = sb.ToString().Replace("''", "','");
                Delsql[0] = "delete from officedba.RoleInfo where RoleID IN (" + allRoleId + ") and CompanyCD = '" + CompanyCD + "'";
                SqlCommand comm = new SqlCommand();
                comm.CommandText = Delsql[0].ToString();
                listADD.Add(comm);
                SqlCommand com = new SqlCommand();
                com.CommandText = "delete from officedba.RoleFunction where RoleID in(" + allRoleId + ") and CompanyCD = '" + CompanyCD + "'";
                listADD.Add(com);

                SqlHelper.ExecuteTransWithArrayList(listADD);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加角色记录
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool InsertRoleInfo(RoleInfoModel model, string loginUserID, out string ID)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.RoleInfo");
            sql.AppendLine("		(CompanyCD      ");
            sql.AppendLine("		,RoleName         ");
            sql.AppendLine("		,remark         ");
            sql.AppendLine("		,ModifiedUserID)        ");
            sql.AppendLine("VALUES                  ");
            sql.AppendLine("		(@CompanyCD     ");
            sql.AppendLine("		,@RoleName        ");
            sql.AppendLine("		,@remark       ");
            sql.AppendLine("		,'" + loginUserID + "')       ");
            sql.AppendLine("set @ID= @@IDENTITY");

            //设置参数
            SqlParameter[] param = new SqlParameter[4];
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[1] = SqlHelper.GetParameter("@remark", model.Remark);
            param[2] = SqlHelper.GetParameter("@RoleName", model.RoleName);
            param[3] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            ID = param[3].Value.ToString();
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }

        /// <summary>
        /// 修改角色记录
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool UpdateRoleInfo(RoleInfoModel model, string loginUserID, int RoleID)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.RoleInfo			     ");
            sql.AppendLine("SET                                      ");
            sql.AppendLine("		RoleName = @RoleName,             ");
            sql.AppendLine("		remark = @remark ,            ");
            sql.AppendLine("		ModifiedDate = @ModifiedDate             ");
            sql.AppendLine("		,ModifiedUserID = '" + loginUserID + "'                ");
            sql.AppendLine("WHERE                                    ");
            sql.AppendLine("		RoleID = " + RoleID + " and CompanyCD=@CompanyCD        ");

            //设置参数
            SqlParameter[] param = new SqlParameter[4];
            param[0] = SqlHelper.GetParameter("@RoleName", model.RoleName);
            param[1] = SqlHelper.GetParameter("@remark", model.Remark);
            param[2] = SqlHelper.GetParameter("@CompanyCD",model.CompanyCD);
            param[3] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);

            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }
        /// <summary>
        /// 根据角色名称获取角色ID
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        //public static int GetRoleInfoId(string CompanyCD, string RoleName)
        //{
        //    string sql = "select RoleID from officedba.RoleInfo where CompanyCD=@CompanyCD and RoleName=@RoleName";
        //    SqlParameter[] param = new SqlParameter[2];
        //    param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
        //    param[1] = SqlHelper.GetParameter("@RoleName", RoleName);
        //    DataTable dt_roleid = SqlHelper.ExecuteSql(sql, param);
        //    return Convert.ToInt32(dt_roleid.Rows[0]["RoleID"]);
        //}


        public static DataTable GetMaxRoleId()
        {
            string sql = "select max(RoleID) as roleid from officedba.RoleInfo";
            return SqlHelper.ExecuteSql(sql);
        }


        #region 通过登录帐号，获取当前用户的角色,通过角色获取所赋予的菜单
        /// <summary>
        /// 通过登录帐号，获取当前用户的角色,通过角色获取所赋予的菜单
        /// </summary>
        /// <param name="companyCD"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetRoleModuleByUser(string companyCD, string userID)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("Select distinct ModuleID From officedba.RoleFunction ");
            searchSql.AppendLine("Where CompanyCD=@CompanyCD and RoleID in(");
            searchSql.AppendLine("	Select RoleID From officedba.UserRole Where UserID=@UserID and CompanyCD=@CompanyCD");
            searchSql.AppendLine(")");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UserID", userID));


            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
       
    }
}

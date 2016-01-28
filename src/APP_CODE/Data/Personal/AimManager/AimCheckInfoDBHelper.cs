/**********************************************
 * 类作用：   个人桌面目标检查数据层处理
 * 建立人：   王乾睿
 * 建立时间： 2009.4.18
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using XBase.Common;
using XBase.Data.DBHelper;
using XBase.Model.Personal.AimManager;


namespace XBase.Data.Personal.AimManager
{
    public class AimCheckInfoDBHelper : System.Web.SessionState.IRequiresSessionState
    {
        /// <summary>
        /// 数据访问类AimCheckInfoModel。
        /// </summary>
        ///   /// 
        /// 作者：王乾睿
        /// 创建时间：2009.4.20
        /// 最后修改时间：2009.4.20
        /// </summary>
        ///
        public static DataTable SelectAimCheckList(int pageindex, int pagesize, Hashtable parm, out int RecordCount)
        {
            #region SQL语句拼写
            string whereStr = SetSearchAimCheckWhereString(parm);
            string orderbyStr = "  ORDER BY o.id DESC   ";
            if (parm.ContainsKey("OrderBy"))
            {
                orderbyStr = parm["OrderBy"].ToString();
                if (orderbyStr.Contains("AddOrCut"))
                {
                    orderbyStr = orderbyStr.Replace("AddOrCut", "o.AddOrCut");
                }

                if (orderbyStr.Contains("CheckDate"))
                {
                    orderbyStr = orderbyStr.Replace("CheckDate", "o.CheckDate");
                }

                if (orderbyStr.Contains("Remark"))
                {
                    orderbyStr = orderbyStr.Replace("Remark", "p.Remark");
                }

            }
            StringBuilder SelectAimCheckListSqlString = new StringBuilder();
            SelectAimCheckListSqlString.Append(" SELECT TOP   " + pagesize + " AimNo,AimTitle ,p.id as Cid ,officedba.getEmployeeNameByID(p.Checkor) as CheckorName,  ");
            SelectAimCheckListSqlString.Append("      AimID,AimTypeID,AimDate,AimStandard,Status,CheckMethod , p.CheckDate,CheckResult,p.AddOrCut,p.Remark ");
            SelectAimCheckListSqlString.Append("          FROM  officedba.PlanAim o join  officedba.PlanCheck p on o.ID = p.AimID  ");
            SelectAimCheckListSqlString.Append("       WHERE ( p.ID NOT IN   ");
            SelectAimCheckListSqlString.Append("         (SELECT TOP  (" + pagesize + "*" + pageindex + ")  p.id   ");
            SelectAimCheckListSqlString.Append("       FROM    officedba.PlanCheck p left join    officedba.PlanAim o on o.ID = p.AimID  ");
            SelectAimCheckListSqlString.Append(whereStr);
            SelectAimCheckListSqlString.Append("        " + orderbyStr + " ))    ");
            SelectAimCheckListSqlString.Append(whereStr.Replace("WHERE", " AND  "));
            SelectAimCheckListSqlString.Append(orderbyStr);
            SelectAimCheckListSqlString.Append(";select  @RecordCount=count(*)   from officedba.PlanAim o join  officedba.PlanCheck p on o.ID = p.AimID  ");
            SelectAimCheckListSqlString.Append(whereStr);
            #endregion
            //定义更新基本信息的命令 
            SqlCommand comm = new SqlCommand();
            comm.CommandText = SelectAimCheckListSqlString.ToString();
            //设置保存的参数
            SetAimCheckSearchParm(comm, parm);
            // 执行查找操作
          
            DataTable dt =   SqlHelper.ExecuteSearch(comm); 
            RecordCount =Convert.ToInt32( comm.Parameters["@RecordCount"].Value.ToString());
            return dt;
        }

        public static DataTable GetChooseAimList(char aimflag,string CompanyCD)
        {
             UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region SQL语句拼写
             string sqlstr = "Select  AimStandard,ID,AimTypeID,AimTitle, dbo.getEmployeeName ( PrincipalID)  as PrincipalName  ,StartDate,EndDate   from  officedba.PlanAim Where status = '1'   And  AimFlag =  '" + aimflag + "'   And  CompanyCD= '" + CompanyCD + "'  And  Checkor=" + userInfo.EmployeeID+ "  ";
            #endregion

            //定义更新基本信息的命令 
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlstr.ToString();
            // 执行查找操作
            return SqlHelper.ExecuteSearch(comm);
        }

        public static bool DelAimInfoByIdArray(string[] IdArray)
        {
            string sqlStr = "DELETE   officedba.PlanCheck  WHERE  " + SetDeleteWhereString(IdArray) + "    ";

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置两个参数
            comm.CommandText = sqlStr;
            //返回操作结果  
            return SqlHelper.ExecuteTransWithCommand(comm);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int InsertAimCheckInfo(AimCheckInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.PlanCheck(");
            strSql.Append("CompanyCD,AimID,CheckDate,Checkor,CheckMethod,CheckContent,CheckResult,remark,ModifiedUserID,AddOrCut)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@AimID,@CheckDate,@Checkor,@CheckMethod,@CheckContent,@CheckResult,@remark,@ModifiedUserID,@AddOrCut)");
            strSql.Append(";select @ID =@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@AimID", SqlDbType.Int,4),
					new SqlParameter("@CheckDate", SqlDbType.DateTime),
					new SqlParameter("@Checkor", SqlDbType.Int,4),
					new SqlParameter("@CheckMethod", SqlDbType.VarChar,50),
					new SqlParameter("@CheckContent", SqlDbType.VarChar,1024),
					new SqlParameter("@CheckResult", SqlDbType.VarChar,1024),
					new SqlParameter("@remark", SqlDbType.VarChar,1024),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10),
					new SqlParameter("@AddOrCut", SqlDbType.Char,1),
                    new SqlParameter("@ID", SqlDbType.Int,0) };
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.AimID;
            parameters[2].Value = model.CheckDate;
            parameters[3].Value = model.Checkor;
            parameters[4].Value = model.CheckMethod;
            parameters[5].Value = model.CheckContent;
            parameters[6].Value = model.CheckResult;
            parameters[7].Value = model.remark;
            parameters[8].Value = model.ModifiedUserID;
            parameters[9].Value = model.AddOrCut;
            parameters[10].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
            return Convert.ToInt32((parameters[10].Value.ToString()));

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int UpdateAimCheckInfo(AimCheckInfoModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.PlanCheck set ");
            strSql.Append("CheckDate=@CheckDate,");
            strSql.Append("Checkor=@Checkor,");
            strSql.Append("CheckMethod=@CheckMethod,");
            strSql.Append("CheckContent=@CheckContent,");
            strSql.Append("CheckResult=@CheckResult,");
            strSql.Append("remark=@remark,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID,");
            strSql.Append("AddOrCut=@AddOrCut");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CheckDate", SqlDbType.DateTime),
					new SqlParameter("@Checkor", SqlDbType.Int,4),
					new SqlParameter("@CheckMethod", SqlDbType.VarChar,50),
					new SqlParameter("@CheckContent", SqlDbType.VarChar,1024),
					new SqlParameter("@CheckResult", SqlDbType.VarChar,1024),
					new SqlParameter("@remark", SqlDbType.VarChar,1024),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10),
					new SqlParameter("@AddOrCut", SqlDbType.Char,1)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.CheckDate;
            parameters[2].Value = model.Checkor;
            parameters[3].Value = model.CheckMethod;
            parameters[4].Value = model.CheckContent;
            parameters[5].Value = model.CheckResult;
            parameters[6].Value = model.remark;
            parameters[7].Value = model.ModifiedDate;
            parameters[8].Value = model.ModifiedUserID;
            parameters[9].Value = model.AddOrCut;

            return SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);

        }

        private static void SetAimCheckSearchParm(SqlCommand comm, Hashtable hs)
        {
            if (hs.ContainsKey("AimTypeID") && hs["AimTypeID"].ToString() != "")
            {
                comm.Parameters.AddWithValue("@AimTypeID", SqlDbType.Int);
                try { comm.Parameters["@AimTypeID"].Value = Convert.ToInt32(hs["AimTypeID"]); }
                catch { comm.Parameters["@AimTypeID"].Value = DBNull.Value; }
            }


            if (hs.ContainsKey("CompanyCD") && hs["CompanyCD"].ToString() != "")
            {
                comm.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar);
                try { comm.Parameters["@CompanyCD"].Value = hs["CompanyCD"]; }
                catch { comm.Parameters["@CompanyCD"].Value = DBNull.Value; }
            }

            if (hs.ContainsKey("AimFlag"))
            {
                comm.Parameters.AddWithValue("@AimFlag", SqlDbType.VarChar);
                try { comm.Parameters["@AimFlag"].Value = hs["AimFlag"].ToString(); }
                catch { comm.Parameters["@AimFlag"].Value = DBNull.Value; }
            }

            if (hs.ContainsKey("AimNo") && hs["AimNo"].ToString() != "")
            {
                comm.Parameters.AddWithValue("@AimNo", SqlDbType.VarChar);
                try { comm.Parameters["@AimNo"].Value = "%" + hs["AimNo"].ToString() + "%"; }
                catch { comm.Parameters["@AimNo"].Value = DBNull.Value; }
            }

            if (hs.ContainsKey("PrincipalID") && hs["PrincipalID"].ToString() != "")
            {
                comm.Parameters.AddWithValue("@PrincipalID", SqlDbType.Int);
                try { comm.Parameters["@PrincipalID"].Value = Convert.ToInt32(hs["PrincipalID"].ToString()); }
                catch { comm.Parameters["@PrincipalID"].Value = DBNull.Value; }
            }
            if (hs.ContainsKey("Checkor") && hs["Checkor"].ToString() != "")
            {
                comm.Parameters.AddWithValue("@Checkor", SqlDbType.Int);
                try { comm.Parameters["@Checkor"].Value = Convert.ToInt32(hs["Checkor"].ToString()); }
                catch { comm.Parameters["@Checkor"].Value = DBNull.Value; }
            }
            if (hs.ContainsKey("AimTitle") && hs["AimTitle"].ToString() != "")
            {
                comm.Parameters.AddWithValue("@AimTitle", SqlDbType.VarChar);
                try { comm.Parameters["@AimTitle"].Value = "%" + hs["AimTitle"].ToString() + "%"; }
                catch { comm.Parameters["@AimTitle"].Value = DBNull.Value; }
            }
            if (hs.ContainsKey("CheckTime") && hs["CheckTime"].ToString() != "")
            {
                comm.Parameters.AddWithValue("@CheckTime", SqlDbType.DateTime);
                try { comm.Parameters["@CheckTime"].Value = Convert.ToDateTime(hs["CheckTime"]); }
                catch { comm.Parameters["@CheckTime"].Value = DBNull.Value; }
            }
            if (hs.ContainsKey("Status") && hs["Status"].ToString() != "")
            {
                comm.Parameters.AddWithValue("@Status", SqlDbType.Char);
                try { comm.Parameters["@Status"].Value = hs["Status"]; }
                catch { comm.Parameters["@Status"].Value = DBNull.Value; }
            }
            if (hs.ContainsKey("AimDate") && hs["AimDate"].ToString() != "")
            {
                comm.Parameters.AddWithValue("@AimDate", SqlDbType.VarChar);
                try { comm.Parameters["@AimDate"].Value = (Convert.ToDateTime(hs["AimDate"]).AddDays(-1)).ToString().Split(' ')[0]; }
                catch { comm.Parameters["@AimDate"].Value = hs["AimDate"]; }
            }
            if (hs.ContainsKey("AimNum") && hs["AimNum"].ToString() != "")
            {
                comm.Parameters.AddWithValue("@AimNum", SqlDbType.Int);
                try { comm.Parameters["@AimNum"].Value = Convert.ToInt32(hs["AimNum"]); }
                catch { comm.Parameters["@AimNum"].Value = DBNull.Value; }
            }
            comm.Parameters.AddWithValue("@RecordCount", SqlDbType.Int);
            comm.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

        }
        private static string SetSearchAimCheckWhereString(Hashtable hs)
        {
            int paramCount = 0;
            StringBuilder whereStr = new StringBuilder();

            if (hs.Keys.Count == 1 && hs.ContainsKey("OrderBy"))
                return "";

            if (hs.Keys.Count > 0)
            {
                whereStr.Append("  WHERE ");

                whereStr.Append("  p.CompanyCD=@CompanyCD ");
                paramCount++;

                if (hs.ContainsKey("AimTypeID") && hs["AimTypeID"].ToString() != "")
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimTypeID=@AimTypeID ");
                    paramCount++;
                }
                if (hs.ContainsKey("AimFlag"))
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimFlag=@AimFlag ");
                    paramCount++;
                }

                if (hs.ContainsKey("AimNo") && hs["AimNo"].ToString() != "")
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimNo  Like  @AimNo  ");
                    paramCount++;
                }

                if (hs.ContainsKey("PrincipalID") && hs["PrincipalID"].ToString() != "")
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  PrincipalID=@PrincipalID ");
                    paramCount++;
                }
                if (hs.ContainsKey("Checkor") && hs["Checkor"].ToString() != "")
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  p.Checkor=@Checkor ");
                    paramCount++;
                }
                if (hs.ContainsKey("AimTitle") && hs["AimTitle"].ToString() != "")
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimTitle LIKE  @AimTitle ");
                    paramCount++;
                }

                if (hs.ContainsKey("CheckTime") && hs["CheckTime"].ToString() != "")
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  CheckTime = @CheckTime ");
                    paramCount++;
                }

                if (hs.ContainsKey("Status") && hs["Status"].ToString() != "")
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  Status = @Status ");
                    paramCount++;
                }

                if (hs.ContainsKey("AimDate") && hs["AimDate"].ToString() != "")
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimDate = @AimDate ");
                    paramCount++;
                }
                if (hs.ContainsKey("AimNum") && hs["AimNum"].ToString() != "")
                {
                    if (paramCount > 0) whereStr.Append("  AND  ");
                    whereStr.Append("  AimNum  = @AimNum ");
                    paramCount++;
                }


            }
            return whereStr.ToString();
        }

        /// <summary>
        /// 得到一个 检查的结果
        /// </summary>
        public static DataTable GetAimCheckRecord(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   p.Checkor,AimNo,AimTitle ,p.id as Cid ,officedba.getEmployeeNameByID(p.Checkor) as CheckorName,CheckContent , AimID,AimTypeID,AimDate,AimStandard,Status,p.CheckMethod , p.CheckDate,p.CheckResult,p.AddOrCut,p.Remark from  ");
            strSql.Append("  officedba.PlanAim o join  officedba.PlanCheck p on o.ID = p.AimID  ");
            strSql.Append(" where p.ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            AimCheckInfoModel model = new AimCheckInfoModel();

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(parameters[0]);
            comm.CommandText = strSql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }


        private static string SetDeleteWhereString(string[] idarray)
        {
            int paramCount = 0;
            StringBuilder whereStr = new StringBuilder();
            whereStr.Append("  (  ");

            foreach (string s in idarray)
            {
                if (paramCount > 0)
                    whereStr.Append("   OR   id=" + s.ToString());
                else
                    whereStr.Append("    id=" + s.ToString());
                paramCount++;
            }

            whereStr.Append("  )  ");

            return whereStr.ToString();
        }
    }
}
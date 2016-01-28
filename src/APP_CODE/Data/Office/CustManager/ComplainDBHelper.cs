using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.CustManager;
using System.Data.SqlTypes;

namespace XBase.Data.Office.CustManager
{
    public class ComplainDBHelper
    {
        #region 添加客户投诉信息的方法
        /// <summary>
        /// 添加客户投诉信息的方法
        /// </summary>
        /// <param name="CustComplainM">客户投诉信息</param>
        /// <returns>投诉信息ID</returns>
        public static int CustComplainAdd(CustComplainModel CustComplainM)
        {
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[23];
                param[0] = SqlHelper.GetParameter("@CompanyCD      ", CustComplainM.CompanyCD);
                param[1] = SqlHelper.GetParameter("@CustID         ", CustComplainM.CustID);
                param[2] = SqlHelper.GetParameter("@CustLinkMan    ", CustComplainM.CustLinkMan);
                param[3] = SqlHelper.GetParameter("@CustLinkTel    ", CustComplainM.CustLinkTel);
                param[4] = SqlHelper.GetParameter("@ComplainNo     ", CustComplainM.ComplainNo);
                param[5] = SqlHelper.GetParameter("@Title          ", CustComplainM.Title);
                param[6] = SqlHelper.GetParameter("@ComplainType   ", CustComplainM.ComplainType);
                param[7] = SqlHelper.GetParameter("@DestClerk      ", CustComplainM.DestClerk);
                param[8] = SqlHelper.GetParameter("@ComplainDate   ", CustComplainM.ComplainDate);
                param[9] = SqlHelper.GetParameter("@Critical       ", CustComplainM.Critical);
                param[10] = SqlHelper.GetParameter("@State          ", CustComplainM.State);
                param[11] = SqlHelper.GetParameter("@DateUnit       ", CustComplainM.DateUnit);
                param[12] = SqlHelper.GetParameter("@SpendTime      ", CustComplainM.SpendTime);
                param[13] = SqlHelper.GetParameter("@Contents       ", CustComplainM.Contents);
                param[14] = SqlHelper.GetParameter("@DisposalProcess", CustComplainM.DisposalProcess);
                param[15] = SqlHelper.GetParameter("@Feedback       ", CustComplainM.Feedback);
                param[16] = SqlHelper.GetParameter("@Remark         ", CustComplainM.Remark);
                param[17] = SqlHelper.GetParameter("@ModifiedDate   ", CustComplainM.ModifiedDate);
                param[18] = SqlHelper.GetParameter("@ModifiedUserID ", CustComplainM.ModifiedUserID);
                param[19] = SqlHelper.GetParameter("@ComplainedMan ", CustComplainM.ComplainedMan);
                param[20] = SqlHelper.GetParameter("@CanViewUser  ", CustComplainM.CanViewUser);
                param[21] = SqlHelper.GetParameter("@CanViewUserName", CustComplainM.CanViewUserName);

                SqlParameter paramid = new SqlParameter("@id", SqlDbType.Int);
                paramid.Direction = ParameterDirection.Output;
                param[22] = paramid;
                #endregion

                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.insertCustComplain", comm, param);
                int Complainid = Convert.ToInt32(comm.Parameters["@id"].Value);

                return Complainid;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 修改客户投诉信息的方法
        /// <summary>
        /// 修改客户投诉信息的方法
        /// </summary>
        /// <param name="CustComplainM">客户投诉信息</param>
        /// <returns>操作结果</returns>
        public static bool UpdateComplain(CustComplainModel CustComplainM)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.CustComplain set ");
                sql.AppendLine("CompanyCD = @CompanyCD, ");
                sql.AppendLine("CustID          = @CustID         ,");
                sql.AppendLine("CustLinkMan     = @CustLinkMan    ,");
                sql.AppendLine("CustLinkTel     = @CustLinkTel    ,");
                //sql.AppendLine("ComplainNo      = @ComplainNo     ,");
                sql.AppendLine("Title           = @Title          ,");
                sql.AppendLine("ComplainType    = @ComplainType   ,");
                sql.AppendLine("DestClerk       = @DestClerk      ,");
                sql.AppendLine("ComplainDate    = @ComplainDate   ,");
                sql.AppendLine("Critical        = @Critical       ,");
                sql.AppendLine("State           = @State          ,");
                sql.AppendLine("DateUnit        = @DateUnit       ,");
                sql.AppendLine("SpendTime       = @SpendTime      ,");
                sql.AppendLine("Contents        = @Contents       ,");
                sql.AppendLine("ComplainedMan   = @ComplainedMan  ,");
                sql.AppendLine("DisposalProcess = @DisposalProcess,");
                sql.AppendLine("Feedback        = @Feedback       ,");
                sql.AppendLine("Remark          = @Remark         ,");
                sql.AppendLine("CanViewUser = @CanViewUser,    ");
                sql.AppendLine("CanViewUserName = @CanViewUserName, ");
                sql.AppendLine("ModifiedDate    = @ModifiedDate   ,");
                sql.AppendLine("ModifiedUserID  = @ModifiedUserID  ");
                sql.AppendLine(" WHERE ");
                sql.AppendLine("ID = @ID ");

                SqlParameter[] param = new SqlParameter[22];
                param[0] = SqlHelper.GetParameter("@ID      ", CustComplainM.ID);
                param[1] = SqlHelper.GetParameter("@CompanyCD      ", CustComplainM.CompanyCD);
                param[2] = SqlHelper.GetParameter("@CustID         ", CustComplainM.CustID);
                param[3] = SqlHelper.GetParameter("@CustLinkMan    ", CustComplainM.CustLinkMan);
                param[4] = SqlHelper.GetParameter("@CustLinkTel    ", CustComplainM.CustLinkTel);
                //param[] = SqlHelper.GetParameter("@ComplainNo     ",CustComplainM.ComplainNo     );
                param[5] = SqlHelper.GetParameter("@Title          ", CustComplainM.Title);
                param[6] = SqlHelper.GetParameter("@ComplainType   ", CustComplainM.ComplainType);
                param[7] = SqlHelper.GetParameter("@DestClerk      ", CustComplainM.DestClerk);
                param[8] = SqlHelper.GetParameter("@ComplainDate", CustComplainM.ComplainDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(CustComplainM.ComplainDate.ToString()));

                param[9] = SqlHelper.GetParameter("@Critical       ", CustComplainM.Critical);
                param[10] = SqlHelper.GetParameter("@State          ", CustComplainM.State);
                param[11] = SqlHelper.GetParameter("@DateUnit       ", CustComplainM.DateUnit);
                param[12] = SqlHelper.GetParameter("@SpendTime      ", CustComplainM.SpendTime);
                param[13] = SqlHelper.GetParameter("@Contents       ", CustComplainM.Contents);
                param[14] = SqlHelper.GetParameter("@DisposalProcess", CustComplainM.DisposalProcess);
                param[15] = SqlHelper.GetParameter("@Feedback       ", CustComplainM.Feedback);
                param[16] = SqlHelper.GetParameter("@Remark         ", CustComplainM.Remark);                
                param[17] = SqlHelper.GetParameter("@ModifiedDate", CustComplainM.ModifiedDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(CustComplainM.ModifiedDate.ToString()));
                param[18] = SqlHelper.GetParameter("@ModifiedUserID ", CustComplainM.ModifiedUserID);
                param[19] = SqlHelper.GetParameter("@ComplainedMan ", CustComplainM.ComplainedMan);
                param[20] = SqlHelper.GetParameter("@CanViewUser", CustComplainM.CanViewUser);
                param[21] = SqlHelper.GetParameter("@CanViewUserName", CustComplainM.CanViewUserName);
                

                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;

            }
            catch
            {
                return false;
            }        
        }
        #endregion

        #region 根据条件检索客户
        /// <summary>
        /// 根据条件检索客户
        /// </summary>
        /// <param name="CustName">投诉客户</param>
        /// <param name="CustComplainM">投诉信息</param>
        /// <param name="ComplainBegin">投诉开始时间</param>
        /// <param name="ComplainEnd">投诉结束时间</param>
        /// <param name="CustLinkMan">客户联系人</param>
        /// <param name="DestClerk">接待人</param>
        /// <returns></returns>
        
        public static DataTable GetComplainInfoBycondition(string CanUserID,string CustName, CustComplainModel CustComplainM, string ComplainBegin, string ComplainEnd, string CustLinkMan, string DestClerk, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select "+
                                   " cc.id,cc.ComplainNo,CONVERT(varchar(100), cc.ComplainDate, 20) ComplainDate,cc.custid,ci.custName custNam ,cc.title,cp.typename,ei.EmployeeName,cc.Critical,cc.state " +
                                   " ,ci.CustNo,ci.CustBig,ci.CanViewUser,ci.Manager,ci.Creator from " +
	                               " officedba.CustComplain cc,"+
	                               " officedba.CustInfo ci,"+
	                               " officedba.CodePublicType cp,"+
	                               " officedba.EmployeeInfo ei "+
                               " where  "+
	                               " cc.custid = ci.id "+
                               " and cc.ComplainType = cp.id "+
                               " and cc.DestClerk = ei.id "+
                               " and cc.CompanyCD = '" + CustComplainM.CompanyCD + "'"+
                " and (cc.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = cc.DestClerk or '" + CanUserID + "' = cc.ComplainedMan or cc.CanViewUser = ',,' or cc.CanViewUser is null )";

                if (CustName != "")
                    sql += " and ci.id = '" + CustName + "'";
                if (CustComplainM.ComplainType != 0)
                    sql += " and cc.ComplainType = " + CustComplainM.ComplainType + "";
                if (CustComplainM.Critical != "0")//紧急程度
                    sql += " and cc.Critical = " + CustComplainM.Critical + "";
                if (ComplainBegin != "")
                    sql += " and cc.ComplainDate >= '" + ComplainBegin + "'";
                if (ComplainEnd != "")
                    sql += " and cc.ComplainDate <= '" + ComplainEnd + "'";
                if (CustComplainM.Title != "")
                    sql += " and cc.title like '%" + CustComplainM.Title + "%'";
                if (CustLinkMan != "")
                    sql += " and cl.LinkManName like '%" + CustLinkMan + "%'"; 
                if (DestClerk != "")
                    sql += " and ei.EmployeeName like '%" + DestClerk + "%'";
                if (CustComplainM.State != "0")
                    sql += " and cc.state = '" + CustComplainM.State + "'";
                #endregion

                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetComplainInfoComplainType(string ComplainType, string CustName, CustComplainModel CustComplainM, string ComplainBegin, string ComplainEnd, string CustLinkMan, string DestClerk, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select " +
                                   " cc.id,cc.ComplainNo,CONVERT(varchar(100), cc.ComplainDate, 20) ComplainDate,cc.custid,ci.custName custNam ,cc.title,cp.typename,ei.EmployeeName,cc.Critical,cc.state " +
                               " from " +
                                   " officedba.CustComplain cc," +
                                   " officedba.CustInfo ci," +
                                   " officedba.CodePublicType cp," +
                                   " officedba.EmployeeInfo ei  " +
                               " where  " +
                                   " cc.custid = ci.id " +
                               " and cc.ComplainType = cp.id " +
                               " and cc.DestClerk = ei.id " +
                               " and cc.CompanyCD = '" + CustComplainM.CompanyCD + "'";
                if (ComplainType != "" && ComplainType !="0" )
                    sql += " and cc.ComplainType = " + ComplainType + " ";
                if (CustName != "")
                    sql += " and ci.id = '" + CustName + "'";
                if (CustComplainM.ComplainType != 0)
                    sql += " and cc.ComplainType = " + CustComplainM.ComplainType + "";
                if (CustComplainM.Critical != "0")//紧急程度
                    sql += " and cc.Critical = " + CustComplainM.Critical + "";
                if (ComplainBegin != "")
                    sql += " and cc.ComplainDate >= '" + ComplainBegin + "'";
                if (ComplainEnd != "")
                    sql += " and cc.ComplainDate <= '" + ComplainEnd + "'";
                if (CustComplainM.Title != "")
                    sql += " and cc.title like '%" + CustComplainM.Title + "%'";
                if (DestClerk != "")
                    sql += " and ei.EmployeeName like '%" + DestClerk + "%'";
                if (CustComplainM.State != "0")
                    sql += " and cc.state = '" + CustComplainM.State + "'";
                #endregion

                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetComplainInfoByComplainPerson(string ComplainPerson,string CustName, CustComplainModel CustComplainM, string ComplainBegin, string ComplainEnd, string CustLinkMan, string DestClerk, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select " +
                                   " cc.id,cc.ComplainNo,CONVERT(varchar(100), cc.ComplainDate, 20) ComplainDate,cc.custid,ci.custName custNam ,cc.title,cp.typename,ei.EmployeeName,cc.Critical,cc.state " +
                               " from " +
                                   " officedba.CustComplain cc," +
                                   " officedba.CustInfo ci," +
                                   " officedba.CodePublicType cp," +
                                   " officedba.EmployeeInfo ei " +

                               " where  " +
                                   " cc.custid = ci.id " +
                               " and cc.ComplainType = cp.id " +
                               " and cc.DestClerk = ei.id " +
                               " and cc.CompanyCD = '" + CustComplainM.CompanyCD + "'";
                if (ComplainPerson != "")
                    sql += " and cc.ComplainedMan = " + ComplainPerson + " ";
                if (CustName != "")
                    sql += " and ci.id = '" + CustName + "'";
                if (CustComplainM.ComplainType != 0)
                    sql += " and cc.ComplainType = " + CustComplainM.ComplainType + "";
                if (CustComplainM.Critical != "0")//紧急程度
                    sql += " and cc.Critical = " + CustComplainM.Critical + "";
                if (ComplainBegin != "")
                    sql += " and cc.ComplainDate >= '" + ComplainBegin + "'";
                if (ComplainEnd != "")
                    sql += " and cc.ComplainDate <= '" + ComplainEnd + "'";
                if (CustComplainM.Title != "")
                    sql += " and cc.title like '%" + CustComplainM.Title + "%'";
                if (DestClerk != "")
                    sql += " and ei.EmployeeName like '%" + DestClerk + "%'";
                if (CustComplainM.State != "0")
                    sql += " and cc.state = '" + CustComplainM.State + "'";
                #endregion

                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
       

        #endregion

        #region 根据投诉ID获取此条投诉信息
        /// <summary>
        /// 根据投诉ID获取此条投诉信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="ComplainID">投诉ID</param>
        /// <returns>返回记录</returns>
        public static DataTable GetComplainInfoByID(string CompanyCD, int ComplainID)
        {
            try
            {
                string sql = "select cc.ID " +
                                    ",cc.CompanyCD,cc.CustID,ci.CustName CustNam,ci.CustNo,cc.CustLinkMan,cl.LinkManName " +
                                    ",cc.CustLinkTel,cc.ComplainNo,cc.Title " +
                                    ",cc.ComplainType ,cc.ComplainedMan,ei2.EmployeeName ComplainedManName" +
                                    ",cc.DestClerk,ei.EmployeeName " +
                                    ",CONVERT(varchar(100), cc.ComplainDate, 20) ComplainDate " +
                                    ",cc.Critical,cc.State,cc.DateUnit,cc.SpendTime " +
                                    ",cc.Contents,cc.DisposalProcess,cc.Feedback " +
                                    ",cc.Remark,CONVERT(varchar(100), cc.ModifiedDate, 23) ModifiedDate,cc.ModifiedUserID " +
                                    " ,cc.CanViewUser,cc.CanViewUserName " +
                               " from " +
                                       " officedba.CustComplain cc  " +
                                       " left join officedba.CustInfo ci on  cc.custid = ci.id  " +
                                       " left join officedba.CustLinkMan cl  on cc.CustLinkMan = cl.id " +
                                       " left join officedba.EmployeeInfo ei on cc.DestClerk = ei.id " +
                                       " left join officedba.EmployeeInfo ei2 on cc.ComplainedMan = ei2.id " +
                               " where " +
                                    " cc.id= @id " +
                               " and cc.CompanyCD = @CompanyCD ";
                              
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", ComplainID);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 投诉一览表_报表
        /// <summary>
        /// 投诉一览表_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">投诉分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetComplainList(string CustName, string TypeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT    b.CustNo, b.CustName, d.EmployeeName, a.ComplainNo, a.Title, ");
                sql.Append(" substring(a.Contents,0,10) Contects, c.TypeName,(e.EmployeeName) ComplainedMan,a.ComplainDate,  ");
                sql.Append(" (case a.State when 1 then '处理中' when 2 then '未处理' when 3 then '已处理' else '' end) State ");
                sql.Append(" FROM  officedba.CustComplain AS a INNER JOIN ");
                sql.Append(" officedba.CustInfo AS b ON a.CustID = b.ID Left JOIN ");
                sql.Append(" officedba.CodePublicType AS c ON a.ComplainType = c.ID Left JOIN ");
                sql.Append(" officedba.EmployeeInfo AS d ON b.Manager = d.ID  left join");
                sql.Append("  officedba.EmployeeInfo As e ON a.ComplainedMan=e.Id  ");
                sql.Append(" where 1=1");
                if (CompanyCD != "")
                {
                    sql.Append(" and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }
                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (TypeId != "")
                {
                    sql.Append(" and a.ComplainType=");
                    sql.Append(TypeId);
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and  a.ComplainDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.ComplainDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }


                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetComplainListPrint(string CustName, string TypeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT    b.CustNo, b.CustName, d.EmployeeName, a.ComplainNo, a.Title, ");
                sql.Append(" substring(a.Contents,0,10) Contects, c.TypeName,(e.EmployeeName) ComplainedMan,a.ComplainDate,  ");
                sql.Append(" (case a.State when 1 then '处理中' when 2 then '未处理' when 3 then '已处理' else '' end) State ");
                sql.Append(" FROM      officedba.CustComplain AS a INNER JOIN ");
                sql.Append(" officedba.CustInfo AS b ON a.CustID = b.ID Left JOIN ");
                sql.Append(" officedba.CodePublicType AS c ON a.ComplainType = c.ID Left JOIN ");
                sql.Append(" officedba.EmployeeInfo AS d ON b.Manager = d.ID  left join");
                sql.Append("  officedba.EmployeeInfo As e ON a.ComplainedMan=e.Id  ");
                sql.Append(" where 1=1");
                if (CompanyCD != "")
                {
                    sql.Append(" and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }
                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (TypeId != "")
                {
                    sql.Append(" and a.ComplainType=");
                    sql.Append(TypeId);
                }
                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and  a.ComplainDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.ComplainDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 投诉次数统计_报表
        /// <summary>
        /// 投诉次数统计_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">投诉分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetComplainCount(string CustName, string TypeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT   b.CustNo, b.CustName, c.TypeName, a.ComplainNum ");
                sql.Append("FROM (SELECT COUNT(1) AS ComplainNum, ComplainType, CustID ");
                sql.Append(" FROM  officedba.CustComplain where 1=1 ");

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and  ComplainDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and ComplainDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                sql.Append(" GROUP BY CustID, ComplainType) AS a INNER JOIN officedba.CustInfo AS b ON a.CustID = b.ID ");
                sql.Append(" INNER JOIN officedba.CodePublicType AS c ON a.ComplainType = c.ID ");

                sql.Append(" where b.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");

                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (TypeId != "")
                {
                    sql.Append(" and a.ComplainType=");
                    sql.Append(TypeId);
                }


                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetComplainCountPrint(string CustName, string TypeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT   b.CustNo, b.CustName, c.TypeName, a.ComplainNum ");
                sql.Append("FROM (SELECT COUNT(1) AS ComplainNum, ComplainType, CustID ");
                sql.Append(" FROM  officedba.CustComplain where 1=1 ");

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and  ComplainDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and ComplainDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                sql.Append(" GROUP BY CustID, ComplainType) AS a INNER JOIN officedba.CustInfo AS b ON a.CustID = b.ID ");
                sql.Append(" INNER JOIN officedba.CodePublicType AS c ON a.ComplainType = c.ID ");

                sql.Append(" where b.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");

                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (TypeId != "")
                {
                    sql.Append(" and a.ComplainType=");
                    sql.Append(TypeId);
                }

                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 投诉分类统计_报表
        /// <summary>
        /// 投诉分类统计_报表
        /// </summary>
        /// <param name="TypeId">投诉分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetComplainByType(string TypeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT  b.TypeName, isnull(a.ComplainNum,'0') ComplainNum, a.ComplainType");
                sql.Append(" FROM  (select * from officedba.CodePublicType where TypeFlag=4 and TypeCode=10 and CompanyCD='" + CompanyCD + "')AS b  Left JOIN (SELECT COUNT(1) AS ComplainNum, ComplainType ");
                sql.Append(" FROM  officedba.CustComplain where 1=1 ");

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and  ComplainDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and ComplainDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                sql.Append(" GROUP BY ComplainType) As a ");
                sql.Append("  ON a.ComplainType = b.ID ");

                sql.Append(" where ComplainNum !=0 ");
                if (CompanyCD != "")
                {
                    sql.Append(" and  b.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }

                if (TypeId != "")
                {
                    sql.Append(" and a.ComplainType=");
                    sql.Append(TypeId);
                }


                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetComplainByTypePrint(string TypeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {

             try
            {
                #region sql语句
                string sql = "select " +
                                   " cc.ComplainType,cc.id,cc.ComplainNo,CONVERT(varchar(100), cc.ComplainDate, 20) ComplainDate,cc.custid,ci.custName custNam ,cc.title,cp.typename,ei.EmployeeName,cc.Critical,cc.state " +
                               " from " +
                                   " officedba.CustComplain cc," +
                                   " officedba.CustInfo ci," +
                                   " officedba.CodePublicType cp," +
                                   " officedba.EmployeeInfo ei " +
                               " where  " +
                                   " cc.custid = ci.id " +
                               " and cc.ComplainType = cp.id " +
                               " and cc.DestClerk = ei.id " +
                               " and cc.CompanyCD = '" + CompanyCD + "'";
                if (TypeId != "" && TypeId != "0")
                    sql += " and cc.ComplainType = " + TypeId + " ";
                if (LinkDateBegin != "")
                    sql += " and cc.ComplainDate >= '" + LinkDateBegin + "'";
                if (LinkDateEnd != "")
                    sql += " and cc.ComplainDate <= '" + LinkDateEnd + "'";
                #endregion

                int TotalCount = 0;
                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), 1, 99999, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
           
        }
        #endregion

        #region 被投诉人统计_报表
        /// <summary>
        /// 被投诉人统计_报表
        /// </summary>
        /// <param name="TypeId">投诉人ID</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetComplainByMan(string ComplainedMan, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT  b.CustNO,b.CustName,c.EmployeeName, a.ComplainNum,a.ComplainedMan ");
                sql.Append(" FROM (SELECT COUNT(1) AS ComplainNum, ComplainedMan, CustID ");
                sql.Append(" FROM  officedba.CustComplain where 1=1 ");

                if (ComplainedMan != "")
                {
                    sql.Append(" and ComplainedMan=");
                    sql.Append(ComplainedMan.ToString());
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and  ComplainDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and ComplainDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                sql.Append(" GROUP BY CustID,ComplainedMan)As a INNER JOIN officedba.CustInfo AS b ON a.CustId=b.Id ");
                sql.Append(" INNER JOIN officedba.EmployeeInfo AS c ON a.ComplainedMan = c.ID ");

                sql.Append(" where b.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 被投诉人统计_报表
        /// </summary>
        /// <param name="ComplainedMan">ComplainedManID</param>
        /// <param name="EmployeeName">EmployeeNameID</param>
        /// <param name="GroupBy">GroupByID</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetComplainByTimeBehaviour(string CustName, string ComplainedManName, string GroupBy,string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();

                 if(GroupBy =="1" ){
                     sql.Append(@"SELECT   ComplainedMan, CustID,ComplainDate,DATEPART(week,ComplainDate) as TimeIndex into #temptable
                                   FROM  officedba.CustComplain
                                   where 1=1  and  ComplainDate >= '" + LinkDateBegin + "' and ComplainDate <dateadd(dd,1,'" + LinkDateEnd + "')" +
                                 "GROUP BY CustID,ComplainedMan,ComplainDate ");
                     sql.Append(" SELECT   count(TimeIndex) as ComplainNum , '第'+convert(varchar(10),TimeIndex)+'周'  as TimeIndex ,convert(varchar(10),TimeIndex) as Tindex ");
                 }
                 else if (GroupBy == "2")
                 {
                     sql.Append(@"SELECT  ComplainedMan, CustID,ComplainDate,DATEPART(month,ComplainDate) as TimeIndex into #temptable
                                   FROM  officedba.CustComplain
                                   where 1=1  and  ComplainDate >= '" + LinkDateBegin + "' and ComplainDate <dateadd(dd,1,'" + LinkDateEnd + "')" +
                                 "GROUP BY CustID,ComplainedMan,ComplainDate ");
                     sql.Append(" SELECT   count(TimeIndex) as ComplainNum , convert(varchar(10),TimeIndex)+'月份'  as TimeIndex,convert(varchar(10),TimeIndex) as Tindex ");
                 }else{
                     sql.Append(@"SELECT   ComplainedMan, CustID,ComplainDate,DATEPART(year,ComplainDate) as TimeIndex into #temptable
                                   FROM  officedba.CustComplain
                                   where 1=1  and  ComplainDate >= '" + LinkDateBegin + "' and ComplainDate <dateadd(dd,1,'" + LinkDateEnd + "')" +
                                 "GROUP BY CustID,ComplainedMan,ComplainDate   ");
                     sql.Append(" SELECT   count(TimeIndex) as ComplainNum , convert(varchar(10),TimeIndex)+'年'  as TimeIndex,convert(varchar(10),TimeIndex) as Tindex ");
                 }
               
                sql.Append(@" FROM #temptable as a INNER JOIN officedba.CustInfo AS b ON a.CustId=b.Id
                                        where b.CompanyCD='" + CompanyCD + "'  ");

                if (CustName != "")
                {
                    sql.Append(" and ComplainedMan = '");
                    sql.Append(CustName.ToString() +"'");
                }
                if (ComplainedManName != "")
                {
                    sql.Append(" and ComplainedManName='");
                    sql.Append(ComplainedManName.ToString()+"'");
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and  ComplainDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and ComplainDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                }

                sql.Append("')  Group by  TimeIndex  ");

                sql.Append(" drop table #temptable  ");

             

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sql.ToString();
                return   SqlHelper.ExecuteSearch(comm);
               // return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetComplainByTimeBehaviour(string CustName, string ComplainedManName, string GroupBy, string TimeIndex, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select ";


                if (GroupBy == "1")
                {
                    sql += " DATEPART(week,ComplainDate) as Tindex , dbo.getEmployeeName( cc.ComplainedMan)  as ComplainedManName,cc.ComplainType,cc.id,cc.ComplainNo,CONVERT(varchar(100), cc.ComplainDate, 20) ComplainDate,cc.custid,ci.custName custNam ,cc.title,cp.typename,ei.EmployeeName,cc.Critical,cc.state ";
                }
                else if (GroupBy == "2")
                {
                    sql += " DATEPART(month,ComplainDate) as Tindex, dbo.getEmployeeName( cc.ComplainedMan)  as ComplainedManName,cc.ComplainType,cc.id,cc.ComplainNo,CONVERT(varchar(100), cc.ComplainDate, 20) ComplainDate,cc.custid,ci.custName custNam ,cc.title,cp.typename,ei.EmployeeName,cc.Critical,cc.state ";
                }
                else
                {
                    sql += " DATEPART(year,ComplainDate) as Tindex, dbo.getEmployeeName( cc.ComplainedMan)  as ComplainedManName,cc.ComplainType,cc.id,cc.ComplainNo,CONVERT(varchar(100), cc.ComplainDate, 20) ComplainDate,cc.custid,ci.custName custNam ,cc.title,cp.typename,ei.EmployeeName,cc.Critical,cc.state ";
                }


                sql += " from " +
                                    " officedba.CustComplain cc," +
                                    " officedba.CustInfo ci," +
                                    " officedba.CodePublicType cp," +
                                    " officedba.EmployeeInfo ei " +
                                " where  " +
                                    " cc.custid = ci.id " +
                                " and cc.ComplainType = cp.id " +
                                " and cc.DestClerk = ei.id " +
                                " and cc.CompanyCD = '" + CompanyCD + "'";

                if (CustName != "")
                    sql += " and ci.custName  like  '%" + CustName + "%'";
                if (ComplainedManName != "")
                    sql += " and ComplainedManName like '%" + ComplainedManName + "%'";
                if (LinkDateBegin != "")
                    sql += " and cc.ComplainDate >= '" + LinkDateBegin + "'";
                if (LinkDateEnd != "")
                    sql += " and cc.ComplainDate <= '" + LinkDateEnd + "'";
                if (TimeIndex != "")
                    sql += " and DATEPART(week,ComplainDate) = '" + TimeIndex + "'";


                #endregion

                TotalCount = 0;
                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }



        public static DataTable GetComplainByTimeBehaviourPrint(string CustName, string ComplainedManName, string GroupBy, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                #region sql语句
                string sql = "select ";


                if (GroupBy == "1")
                {
                    sql += " DATEPART(week,ComplainDate) as Tindex , dbo.getEmployeeName( cc.ComplainedMan)  as ComplainedManName,cc.ComplainType,cc.id,cc.ComplainNo,CONVERT(varchar(100), cc.ComplainDate, 20) ComplainDate,cc.custid,ci.custName custNam ,cc.title,cp.typename,ei.EmployeeName,cc.Critical,cc.state ";
                }
                else if (GroupBy == "2")
                {
                    sql += " DATEPART(month,ComplainDate) as Tindex, dbo.getEmployeeName( cc.ComplainedMan)  as ComplainedManName,cc.ComplainType,cc.id,cc.ComplainNo,CONVERT(varchar(100), cc.ComplainDate, 20) ComplainDate,cc.custid,ci.custName custNam ,cc.title,cp.typename,ei.EmployeeName,cc.Critical,cc.state ";
                }
                else
                {
                    sql += " DATEPART(year,ComplainDate) as Tindex, dbo.getEmployeeName( cc.ComplainedMan)  as ComplainedManName,cc.ComplainType,cc.id,cc.ComplainNo,CONVERT(varchar(100), cc.ComplainDate, 20) ComplainDate,cc.custid,ci.custName custNam ,cc.title,cp.typename,ei.EmployeeName,cc.Critical,cc.state ";
                }
         
               
               sql +=         " from " +
                                   " officedba.CustComplain cc," +
                                   " officedba.CustInfo ci," +
                                   " officedba.CodePublicType cp," +
                                   " officedba.EmployeeInfo ei " +
                               " where  " +
                                   " cc.custid = ci.id " +
                               " and cc.ComplainType = cp.id " +
                               " and cc.DestClerk = ei.id " +
                               " and cc.CompanyCD = '" + CompanyCD + "'";

                if (CustName != "")
                    sql += " and ci.custName  like  '%" + CustName + "%'";
                if (ComplainedManName != "")
                    sql += " and ComplainedManName like '%" + ComplainedManName + "%'";
                if (LinkDateBegin != "")
                    sql += " and cc.ComplainDate >= '" + LinkDateBegin + "'";
                if (LinkDateEnd != "")
                    sql += " and cc.ComplainDate <= '" + LinkDateEnd + "'";

               


                #endregion

                int TotalCount = 0;
                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), 1, 99999, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }


        public static DataTable GetComplainByManPrint(string ComplainedMan, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                #region sql语句
                string sql = "select " +
                                   " cc.ComplainedMan, dbo.getEmployeeName( cc.ComplainedMan)  as ComplainedManName,cc.ComplainType,cc.id,cc.ComplainNo,CONVERT(varchar(100), cc.ComplainDate, 20) ComplainDate,cc.custid,ci.custName custNam ,cc.title,cp.typename,ei.EmployeeName,cc.Critical,cc.state " +
                               " from " +
                                   " officedba.CustComplain cc," +
                                   " officedba.CustInfo ci," +
                                   " officedba.CodePublicType cp," +
                                   " officedba.EmployeeInfo ei, " +
                                   " officedba.CustLinkMan cl " +
                               " where  " +
                                   " cc.custid = ci.id " +
                               " and cc.ComplainType = cp.id " +
                               " and cc.DestClerk = ei.id " +
                               " and cc.CustLinkMan = cl.id " +
                               " and cc.CompanyCD = '" + CompanyCD + "'";
                if (ComplainedMan != "" && ComplainedMan != "0")
                    sql += " and cc.ComplainedMan = " + ComplainedMan + " ";
                if (LinkDateBegin != "")
                    sql += " and cc.ComplainDate >= '" + LinkDateBegin + "'";
                if (LinkDateEnd != "")
                    sql += " and cc.ComplainDate <= '" + LinkDateEnd + "'";
                #endregion

                int TotalCount = 0;
                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), 1, 99999, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 零投诉客户统计_报表
        /// <summary>
        /// 零投诉客户统计_报表
        /// </summary>
        /// <param name="Days">天数</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <returns>记录集</returns>
        public static DataTable GetComplainByDays(string Days, string CompanyCD,int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNo,a.CustName,b.EmployeeName from officedba.CustInfo a,officedba.EmployeeInfo b where a.Manager=b.Id ");
                sql.Append(" and b.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");
                sql.Append(" and a.Id not in(Select CustId from officedba.CustComplain where 1=1 ");
                if (Days != "")
                {
                    sql.Append(" and ComplainDate >=dateadd(dd,-" + Days.ToString() + ",getdate()) ");
                }
                sql.Append(" ) ");

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetComplainByDaysPrint(string Days, string CompanyCD,string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNo,a.CustName,b.EmployeeName from officedba.CustInfo a,officedba.EmployeeInfo b where a.Manager=b.Id ");
                sql.Append(" and b.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");
                sql.Append(" and a.Id not in(Select CustId from officedba.CustComplain where 1=1 ");
                if (Days != "")
                {
                    sql.Append(" and ComplainDate >=dateadd(dd,-" + Days.ToString() + ",getdate()) ");
                }
                sql.Append(" ) ");

                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 客户投诉信息打印
        /// <summary>
        /// 客户投诉信息打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ComplainID"></param>
        /// <returns></returns>
        public static DataTable PrintComplain(string CompanyCD, string ComplainID)
        {
            try
            {
                string sql = "select cc.ID " +
                                    ",cc.CompanyCD,cc.CustID,ci.CustName CustNam,ci.CustNo,cc.CustLinkMan,cl.LinkManName " +
                                    ",cc.CustLinkTel,cc.ComplainNo,cc.Title " +
                                    ",cc.ComplainType ,cp.typename ComplainTypeNm,cc.ComplainedMan,ei2.EmployeeName ComplainedManName" +
                                    ",cc.DestClerk,ei.EmployeeName " +
                                    ",CONVERT(varchar(100), cc.ComplainDate, 20) ComplainDate " +
                                    ",(case cc.Critical when '1' then '宽松' when '2' then '一般' when '3' then '较急,' when '4' then '紧急' when '5' then '特急' end)Critical" +
                                    ",(case cc.State when '1' then '处理中' when '2' then '未处理' when '3' then '已处理' end)State" +
                                    ",(case cc.DateUnit when '1' then '小时' when '2' then '天' when '3' then '月' end)DateUnit,cc.SpendTime " +
                                    ",cc.Contents,cc.DisposalProcess,cc.Feedback " +
                                    ",cc.Remark,CONVERT(varchar(100), cc.ModifiedDate, 23) ModifiedDate,cc.ModifiedUserID,cc.CanViewUserName " +
                               " from " +
                                       " officedba.CustComplain cc  " +
                                       " left join officedba.codepublictype cp on cp.id = cc.ComplainType" +
                                       " left join officedba.CustInfo ci on  cc.custid = ci.id  " +
                                       " left join officedba.CustLinkMan cl  on cc.CustLinkMan = cl.id " +
                                       " left join officedba.EmployeeInfo ei on cc.DestClerk = ei.id " +
                                       " left join officedba.EmployeeInfo ei2 on cc.ComplainedMan = ei2.id " +
                               " where " +
                                    " cc.id= @id " +
                               " and cc.CompanyCD = @CompanyCD ";

                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", ComplainID);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 导出客户投诉信息
        /// <summary>
        /// 导出客户投诉信息
        /// </summary>
        /// <param name="CustID"></param>
        /// <param name="CustComplainM"></param>
        /// <param name="ComplainBegin"></param>
        /// <param name="ComplainEnd"></param>
        /// <param name="CustLinkMan"></param>
        /// <param name="DestClerk"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportComplainInfo(string CanUserID,string CustID, CustComplainModel CustComplainM, string ComplainBegin, string ComplainEnd, string CustLinkMan, string DestClerk, string ord)
        {
            try
            {
                #region sql语句
                string sql = "select " +
                                   " cc.id,cc.ComplainNo,CONVERT(varchar(100), cc.ComplainDate, 20) ComplainDate,cc.custid," +
                                   "ci.custName custNam ,cc.title,cp.typename,ei.EmployeeName,(case cc.Critical when '1' then '宽松' when '2' then '一般' when '3' then '较急' when '4' then '紧急' when '5' then '特急' end)Critical," +
                                   "(case cc.state when '1' then '处理中' when '2' then '未处理' when '3' then '已处理' end)state " +
                               " from " +
                                   " officedba.CustComplain cc," +
                                   " officedba.CustInfo ci," +
                                   " officedba.CodePublicType cp," +
                                   " officedba.EmployeeInfo ei, " +
                                   " officedba.CustLinkMan cl " +
                               " where  " +
                                   " cc.custid = ci.id " +
                               " and cc.ComplainType = cp.id " +
                               " and cc.DestClerk = ei.id " +
                               " and cc.CustLinkMan = cl.id " +
                               " and cc.CompanyCD = '" + CustComplainM.CompanyCD + "'" +
                                " and (cc.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = cc.DestClerk or '" + CanUserID + "' = cc.ComplainedMan or cc.CanViewUser = ',,' or cc.CanViewUser is null )";
                if (CustID != "")
                    sql += " and ci.id = '" + CustID + "'";
                if (CustComplainM.ComplainType != 0)
                    sql += " and cc.ComplainType = " + CustComplainM.ComplainType + "";
                if (CustComplainM.Critical != "0")//紧急程度
                    sql += " and cc.Critical = " + CustComplainM.Critical + "";
                if (ComplainBegin.ToString() != "")
                    sql += " and cc.ComplainDate >= '" + ComplainBegin.ToString() + "'";
                if (ComplainEnd.ToString() != "")
                    sql += " and cc.ComplainDate <= '" + ComplainEnd.ToString() + "'";
                if (CustComplainM.Title != "")
                    sql += " and cc.title like '%" + CustComplainM.Title + "%'";
                if (CustLinkMan != "")
                    sql += " and cl.LinkManName like '%" + CustLinkMan + "%'";
                if (DestClerk != "")
                    sql += " and ei.EmployeeName like '%" + DestClerk + "%'";
                if (CustComplainM.State != "0")
                    sql += " and cc.state = '" + CustComplainM.State + "'";
                #endregion

                return SqlHelper.ExecuteSql(sql);
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}

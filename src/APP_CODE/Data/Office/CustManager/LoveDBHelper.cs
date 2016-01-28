using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.CustManager;
using System.Data.SqlTypes;

namespace XBase.Data.Office.CustManager
{
    public class LoveDBHelper
    {
        #region 添加客户关怀信息的方法
        /// <summary>
        /// 添加客户关怀信息的方法
        /// </summary>
        /// <param name="CustLoveM">客户关怀信息</param>
        /// <returns>操作记录数</returns>
        public static int CustLoveAdd(CustLoveModel CustLoveM)
        {
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[16];
                param[0] = SqlHelper.GetParameter("@CompanyCD     ", CustLoveM.CompanyCD     );
                param[1] = SqlHelper.GetParameter("@CustID        ", CustLoveM.CustID        );
                param[2] = SqlHelper.GetParameter("@CustLinkMan   ", CustLoveM.CustLinkMan   );
                param[3] = SqlHelper.GetParameter("@LoveNo        ", CustLoveM.LoveNo        );
                param[4] = SqlHelper.GetParameter("@Title         ", CustLoveM.Title         );
                param[5] = SqlHelper.GetParameter("@LoveType      ", CustLoveM.LoveType      );
                param[6] = SqlHelper.GetParameter("@Contents      ", CustLoveM.Contents      );
                param[7] = SqlHelper.GetParameter("@Linker        ", CustLoveM.Linker        );
                param[8] = SqlHelper.GetParameter("@LoveDate      ", CustLoveM.LoveDate      );
                param[9] = SqlHelper.GetParameter("@Feedback      ", CustLoveM.Feedback      );
                param[10] = SqlHelper.GetParameter("@remarks       ", CustLoveM.remarks       );
                param[11] = SqlHelper.GetParameter("@ModifiedDate  ", CustLoveM.ModifiedDate  );
                param[12] = SqlHelper.GetParameter("@ModifiedUserID", CustLoveM.ModifiedUserID);
                param[13] = SqlHelper.GetParameter("@CanViewUser", CustLoveM.CanViewUser);
                param[14] = SqlHelper.GetParameter("@CanViewUserName", CustLoveM.CanViewUserName);
                
                SqlParameter paramid = new SqlParameter("@id", SqlDbType.Int);
                paramid.Direction = ParameterDirection.Output;
                param[15] = paramid;
                #endregion

                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.insertCustLove", comm, param);
                int Loveid = Convert.ToInt32(comm.Parameters["@id"].Value);

                return Loveid;
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return 0;
            }
        }
        #endregion

        #region 根据客户关怀ID修改关怀信息
        /// <summary>
        /// 根据客户关怀ID修改关怀信息
        /// </summary>
        /// <param name="CustLoveM">客户关怀信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateLove(CustLoveModel CustLoveM)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.CustLove set ");
                sql.AppendLine("CompanyCD     =@CompanyCD     ,");
                sql.AppendLine("CustID        =@CustID        ,");
                sql.AppendLine("CustLinkMan   =@CustLinkMan   ,");
                //sql.AppendLine("LoveNo        =@LoveNo        ,");
                sql.AppendLine("Title         =@Title         ,");
                sql.AppendLine("LoveType      =@LoveType      ,");
                sql.AppendLine("Contents      =@Contents      ,");
                sql.AppendLine("Linker        =@Linker        ,");
                sql.AppendLine("LoveDate      =@LoveDate      ,");
                sql.AppendLine("Feedback      =@Feedback      ,");
                sql.AppendLine("remarks       =@remarks       ,");
                sql.AppendLine("CanViewUser = @CanViewUser,    ");
                sql.AppendLine("CanViewUserName = @CanViewUserName, ");
                sql.AppendLine("ModifiedDate  =@ModifiedDate  ,");
                sql.AppendLine("ModifiedUserID=@ModifiedUserID");
                sql.AppendLine(" WHERE ");
                sql.AppendLine("ID = @ID ");

                SqlParameter[] param = new SqlParameter[15];
                param[0] = SqlHelper.GetParameter("@ID      ", CustLoveM.ID);
                param[1] = SqlHelper.GetParameter("@CompanyCD     ",CustLoveM.CompanyCD     );
                param[2] = SqlHelper.GetParameter("@CustID        ",CustLoveM.CustID        );
                param[3] = SqlHelper.GetParameter("@CustLinkMan   ",CustLoveM.CustLinkMan   );
                //param[4] = SqlHelper.GetParameter("@LoveNo        ",CustLoveM.LoveNo        );
                param[4] = SqlHelper.GetParameter("@Title         ",CustLoveM.Title         );
                param[5] = SqlHelper.GetParameter("@LoveType      ",CustLoveM.LoveType      );
                param[6] = SqlHelper.GetParameter("@Contents      ",CustLoveM.Contents      );
                param[7] = SqlHelper.GetParameter("@Linker        ",CustLoveM.Linker        );                
                param[8] = SqlHelper.GetParameter("@LoveDate", CustLoveM.LoveDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(CustLoveM.LoveDate.ToString()));
                param[9] = SqlHelper.GetParameter("@Feedback      ",CustLoveM.Feedback      );
                param[10] = SqlHelper.GetParameter("@remarks       ",CustLoveM.remarks       );
                param[11] = SqlHelper.GetParameter("@ModifiedDate", CustLoveM.ModifiedDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(CustLoveM.ModifiedDate.ToString()));
                param[12] = SqlHelper.GetParameter("@ModifiedUserID",CustLoveM.ModifiedUserID);
                param[13] = SqlHelper.GetParameter("@CanViewUser", CustLoveM.CanViewUser);
                param[14] = SqlHelper.GetParameter("@CanViewUserName", CustLoveM.CanViewUserName);

                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion

        #region 根据条件查询客户关怀
        /// <summary>
        /// 根据条件查询客户关怀
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="CustLoveM">关怀信息</param>
        /// <param name="LoveBegin">开始时间</param>
        /// <param name="LoveEnd">结束时间</param>
        /// <param name="CustLinkMan">客户联系人</param>
        /// <returns>查询结果</returns>
        public static DataTable GetLoveInfoBycondition(string CanUserID,string CustName, CustLoveModel CustLoveM, string LoveBegin, string LoveEnd, string CustLinkMan, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select " +
                                   " cl.id,cl.LoveNo,cl.Title,CONVERT(varchar(100), cl.LoveDate, 20) LoveDate,ci.CustNo,ci.CustBig,ci.CanViewUser,ci.Manager,ci.Creator," +
                                   " clm.LinkManName,isnull(cp.TypeName,'') LoveType,cl.CustID,ci.CustName CustNam,el.EmployeeName" +
                               " from " +
                                   " officedba.custlove cl" +
                                   " left join officedba.CustInfo ci on ci.id =  cl.CustID " +
                                   " left join officedba.EmployeeInfo el on el.id = cl.Linker" +
                                   " left join officedba.CustLinkMan clm on clm.id = cl.CustLinkMan " +
                                   " left join officedba.CodePublicType cp on cp.id = cl.LoveType" +
                               " where" +                                  
                               " cl.CompanyCD = '" + CustLoveM.CompanyCD + "'"+
                " and (cl.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = cl.Linker or cl.CanViewUser = ',,' or cl.CanViewUser is null )";
                if (CustName != "")
                    sql += " and ci.id = '" + CustName + "'";
                if (CustLoveM.LoveType != 0)
                    sql += " and cl.LoveType = " + CustLoveM.LoveType + "";
                if (LoveBegin != "")
                    sql += " and cl.LoveDate >= '" + LoveBegin + "'";
                if (LoveEnd != "")
                    sql += " and cl.LoveDate <= '" + LoveEnd + "'";
                if (CustLoveM.Title != "")
                    sql += " and cl.title like '%" + CustLoveM.Title + "%'";
                if (CustLinkMan != "")
                    sql += " and clm.LinkManName like '%" + CustLinkMan + "%'";
                
                #endregion

                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 根据关怀ID获取此条关怀信息
        /// <summary>
        /// 根据关怀ID获取此条关怀信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="LoveID">关怀ID</param>
        /// <returns></returns>
        public static DataTable GetLoveInfoByID(string CompanyCD, int LoveID)
        {
            try
            {
                string sql = "select " +
                                    " cl.ID,cl.CompanyCD,cl.CustID,ci.CustNo,ci.custname custnam " +
                                    " ,cl.CustLinkMan,clm.linkmanname " +
                                    " ,cl.LoveNo,cl.Title,cl.LoveType,cl.Contents " +
                                    " ,cl.Linker,ei.employeename " +
                                    " ,CONVERT(varchar(100),cl.LoveDate, 20) LoveDate " +
                                    " ,cl.Feedback,cl.remarks " +
                                    " ,CONVERT(varchar(100),cl.ModifiedDate, 23) ModifiedDate " +
                                    " ,cl.ModifiedUserID " +
                                    " ,cl.CanViewUser,cl.CanViewUserName " +
                                " from " +
                                    " officedba.custlove cl " +
                                    " left join officedba.custinfo ci on ci.id = cl.custid " +
                                    " left join officedba.custlinkman clm on clm.id = cl.custlinkman" +
                                    " left join officedba.EmployeeInfo ei on ei.id = cl.linker" +
                                " where " +
                                     " cl.id= @id " +
                                 " and cl.CompanyCD = @CompanyCD ";     
                               
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", LoveID);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 关怀一览表_报表
        /// <summary>
        /// 关怀一览表_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">关怀分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetLoveList(string CustName, string LoveType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select b.CustNO,b.CustName,c.EmployeeName,a.LoveNO,a.Title,e.TypeName,substring(a.Contents,0,10) Contents , ");
                sql.Append("  d.EmployeeName as Linker,a.LoveDate");
                sql.Append("  from officedba. CustLove as a inner join ");
                sql.Append(" officedba.CustInfo as b on a.CustId=b.Id left join ");
                sql.Append(" officedba.EmployeeInfo as c on b.Manager=c.Id left join ");
                sql.Append(" officedba.EmployeeInfo as d on a.Linker=d.Id left join ");
                sql.Append(" officedba.CodePublicType as e on a.LoveType=e.Id ");
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
                if (LoveType != "")
                {
                    sql.Append(" and a.LoveType= ");
                    sql.Append(LoveType);
                }
                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and a.LoveDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.LoveDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        public static DataTable GetLoveListPrint(string CustName, string LoveType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select b.CustNO,b.CustName,c.EmployeeName,a.LoveNO,a.Title,e.TypeName,substring(a.Contents,0,10) Contents , ");
                sql.Append("  d.EmployeeName as Linker,a.LoveDate ");
                sql.Append("  from officedba. CustLove as a inner join ");
                sql.Append(" officedba.CustInfo as b on a.CustId=b.Id left join ");
                sql.Append(" officedba.EmployeeInfo as c on b.Manager=c.Id left join ");
                sql.Append(" officedba.EmployeeInfo as d on a.Linker=d.Id left join ");
                sql.Append(" officedba.CodePublicType as e on a.LoveType=e.Id ");
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
                if (LoveType != "")
                {
                    sql.Append(" and a.LoveType=");
                    sql.Append(LoveType);
                }
                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and a.LoveDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.LoveDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 关怀次数统计_报表
        /// <summary>
        /// 关怀次数统计_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">关怀分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetCustLoveCount(string CustName, string LoveType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.CustName, isnull(c.TypeName,'') TypeName,isnull(b.LoveCount,'0') LoveCount  ");
                sql.Append(" from officedba.CustInfo a inner join  ");
                sql.Append(" (select count(1) LoveCount,CustId,LoveType from officedba.CustLove where 1=1 ");

                if (LoveType != "")
                {
                    sql.Append(" and LoveType=");
                    sql.Append(LoveType);
                }
                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and LoveDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and LoveDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                sql.Append(" group by CustId,LoveType) b on a.Id=b.CustId left join ");
                sql.Append(" officedba.CodePublicType c on b.LoveType=c.Id ");

                sql.Append(" where 1=1");
                if (CompanyCD != "")
                {
                    sql.Append(" and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }
                if (CustName != "")
                {
                    sql.Append(" and a.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }


                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        public static DataTable GetCustLoveCountPrint(string CustName, string LoveType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.CustName, isnull(c.TypeName,'') TypeName,isnull(b.LoveCount,'0') LoveCount  ");
                sql.Append(" from officedba.CustInfo a inner join  ");
                sql.Append(" (select count(1) LoveCount,CustId,LoveType from officedba.CustLove where 1=1 ");

                if (LoveType != "")
                {
                    sql.Append(" and LoveType=");
                    sql.Append(LoveType);
                }
                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and LoveDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and LoveDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                sql.Append(" group by CustId,LoveType) b on a.Id=b.CustId left join ");
                sql.Append(" officedba.CodePublicType c on b.LoveType=c.Id ");

                sql.Append(" where 1=1");
                if (CompanyCD != "")
                {
                    sql.Append(" and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }
                if (CustName != "")
                {
                    sql.Append(" and a.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }

                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 按关怀分类统计_报表
        /// <summary>
        /// 按关怀分类统计_报表
        /// </summary>
        /// <param name="TypeId">关怀分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetCustLoveByType(string LoveType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select  a.TypeName,isnull(b.LoveCount,'0') LoveCount from ");
                sql.Append(" (select ID,TypeName from officedba.codepublictype where  TypeFlag=4 and TypeCode=13  and CompanyCD='" + CompanyCD + "') a ");
                sql.Append(" left join (select count(1) LoveCount,LoveType from officedba.CustLove where 1=1 ");

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and LoveDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and LoveDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                if (CompanyCD != "")
                {
                    sql.Append(" and CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }



                sql.Append(" group by LoveType ) b ");
                sql.Append(" on a.Id=b.LoveType where 1=1 ");

                if (LoveType != "")
                {
                    sql.Append(" and a.Id=");
                    sql.Append(LoveType);
                }


                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        public static DataTable GetCustLoveByTypePrint(string LoveType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select  a.TypeName,isnull(b.LoveCount,'0') LoveCount from ");
                sql.Append(" (select ID,TypeName from officedba.codepublictype where  TypeFlag=4 and TypeCode=13  and CompanyCD='" + CompanyCD + "') a ");
                sql.Append(" left join (select count(1) LoveCount,LoveType from officedba.CustLove where 1=1 ");

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and LoveDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and LoveDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                if (CompanyCD != "")
                {
                    sql.Append(" and CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }

                sql.Append(" group by LoveType ) b ");
                sql.Append(" on a.Id=b.LoveType where 1=1 ");

                if (LoveType != "")
                {
                    sql.Append(" and a.Id=");
                    sql.Append(LoveType);
                }


                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 按关怀执行人统计_报表
        /// <summary>
        /// 按关怀执行人统计_报表
        /// </summary>
        /// <param name="TypeId">执行人ID</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetLoveByMan(string Linker, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select c.CustNO,c.CustName,b.EmployeeName,a.LoveCount from  ");
                sql.Append(" (select count(*) LoveCount,Linker,CustId from officedba.CustLove where 1=1 ");

                if (CompanyCD != "")
                {
                    sql.Append(" and CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }

                if (Linker != "")
                {
                    sql.Append(" and Linker=");
                    sql.Append(Linker.ToString());
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and LoveDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and LoveDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                sql.Append(" group by Linker,CustId) a Inner join");
                sql.Append(" officedba.EmployeeInfo b on a.Linker=b.Id Inner join  ");
                sql.Append(" officedba.CustInfo c on a.CustId=c.Id ");

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        public static DataTable GetLoveByManPrint(string Linker, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select c.CustNO,c.CustName,b.EmployeeName,a.LoveCount from  ");
                sql.Append(" (select count(*) LoveCount,Linker,CustId from officedba.CustLove where 1=1 ");

                if (CompanyCD != "")
                {
                    sql.Append(" and CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }

                if (Linker != "")
                {
                    sql.Append(" and Linker=");
                    sql.Append(Linker.ToString());
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and LoveDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and LoveDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                sql.Append(" group by Linker,CustId) a Inner join");
                sql.Append(" officedba.EmployeeInfo b on a.Linker=b.Id Inner join  ");
                sql.Append(" officedba.CustInfo c on a.CustId=c.Id ");

                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 未关怀客户统计_报表
        /// <summary>
        /// 未关怀客户统计_报表
        /// </summary>
        /// <param name="Days">天数</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <returns>记录集</returns>
        public static DataTable GetLoveByDays(string Days, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.CustName,isnull(b.EmployeeName,'') EmployeeName from ");
                sql.Append(" officedba.CustInfo a left join officedba.EmployeeInfo b on a.Manager=b.Id ");
                sql.Append(" where a.Id not in(select CustId from officedba.CustLove where 1=1 ");

                if (Days != "")
                {
                    sql.Append(" and LoveDate >=dateadd(dd,-" + Days.ToString() + ",getdate()) ");
                }
                sql.Append(" ) ");


                sql.Append(" and a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        public static DataTable GetLoveByDaysPrint(string Days, string CompanyCD, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.CustName,isnull(b.EmployeeName,'') EmployeeName from ");
                sql.Append(" officedba.CustInfo a left join officedba.EmployeeInfo b on a.Manager=b.Id ");
                sql.Append(" where a.Id not in(select CustId from officedba.CustLove where 1=1 ");

                if (Days != "")
                {
                    sql.Append(" and LoveDate >=dateadd(dd,-" + Days.ToString() + ",getdate()) ");
                }
                sql.Append(" ) ");


                sql.Append(" and a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");

                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 客户关怀信息打印
        /// <summary>
        /// 客户关怀信息打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="LoveID"></param>
        /// <returns></returns>
        public static DataTable PrintLove(string CompanyCD, string LoveID)
        {
            try
            {
                string sql = "select " +
                                   " cl.ID,cl.CompanyCD,cl.CustID,ci.CustNo,ci.custname custnam " +
                                   " ,cl.CustLinkMan,clm.linkmanname " +
                                   " ,cl.LoveNo,cl.Title,cl.LoveType,cp.TypeName LoveTypeNm,cl.Contents " +
                                   " ,cl.Linker,ei.employeename " +
                                   " ,CONVERT(varchar(100),cl.LoveDate, 20) LoveDate " +
                                   " ,cl.Feedback,cl.remarks " +
                                   " ,CONVERT(varchar(100),cl.ModifiedDate, 23) ModifiedDate ,cl.CanViewUserName" +
                                   " ,cl.ModifiedUserID " +
                               " from " +
                                   " officedba.custlove cl " +
                                   " left join officedba.custinfo ci on ci.id = cl.custid " +
                                   " left join officedba.custlinkman clm on clm.id = cl.custlinkman" +
                                   " left join officedba.EmployeeInfo ei on ei.id = cl.linker" +
                                   " left join officedba.CodePublicType cp on cp.id = cl.LoveType" +
                               " where " +
                                    " cl.id= @id " +
                                " and cl.CompanyCD = @CompanyCD ";                             
                                

                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", LoveID);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 导出客户关怀信息列表
        /// <summary>
        /// 导出客户关怀信息列表
        /// </summary>
        /// <param name="CustName"></param>
        /// <param name="CustLoveM"></param>
        /// <param name="LoveBegin"></param>
        /// <param name="LoveEnd"></param>
        /// <param name="CustLinkMan"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportLoveInfo(string CanUserID,string CustID, CustLoveModel CustLoveM, string LoveBegin, string LoveEnd, string CustLinkMan, string ord)
        {
            try
            {
                #region sql语句
                string sql = "select " +
                                   " cl.id,cl.LoveNo,cl.Title,CONVERT(varchar(100), cl.LoveDate, 20) LoveDate," +
                                   " clm.LinkManName,isnull(cp.TypeName,'') LoveType,cl.CustID,ci.CustName CustNam,el.EmployeeName,cl.CanViewUserName " +
                               " from " +
                                   " officedba.custlove cl" +
                                   " left join officedba.CustInfo ci on ci.id =  cl.CustID " +
                                   " left join officedba.EmployeeInfo el on el.id = cl.Linker" +
                                   " left join officedba.CustLinkMan clm on clm.id = cl.CustLinkMan " +
                                   " left join officedba.CodePublicType cp on cp.id = cl.LoveType" +
                               " where" +
                               " cl.CompanyCD = '" + CustLoveM.CompanyCD + "'" +
                               " and (cl.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = cl.Linker or cl.CanViewUser = ',,' or cl.CanViewUser is null )";
                if (CustID != "")
                    sql += " and ci.id = '" + CustID + "'";
                if (CustLoveM.LoveType != 0)
                    sql += " and cl.LoveType = " + CustLoveM.LoveType + "";
                if (LoveBegin != "")
                    sql += " and cl.LoveDate >= '" + LoveBegin + "'";
                if (LoveEnd != "")
                    sql += " and cl.LoveDate <= '" + LoveEnd + "'";
                if (CustLoveM.Title != "")
                    sql += " and cl.title like '%" + CustLoveM.Title + "%'";
                if (CustLinkMan != "")
                    sql += " and clm.LinkManName like '%" + CustLinkMan + "%'";

                #endregion

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion
    }
}

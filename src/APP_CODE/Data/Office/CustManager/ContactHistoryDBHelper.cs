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
    public class ContactHistoryDBHelper
    {

        #region 添加客户联络信息的方法
        /// <summary>
        /// 添加客户联络信息的方法
        /// </summary>
        /// <param name="ContactHistoryM">客户联络信息</param>
        /// <returns>返回操作记录值</returns>
        public static int ContactHistoryAdd(ContactHistoryModel ContactHistoryM)
        {
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[15];
                param[0] = SqlHelper.GetParameter("@CompanyCD", ContactHistoryM.CompanyCD);
                param[1] = SqlHelper.GetParameter("@CustID", ContactHistoryM.CustID);
                param[2] = SqlHelper.GetParameter("@CustLinkMan", ContactHistoryM.CustLinkMan);
                param[3] = SqlHelper.GetParameter("@ContactNo", ContactHistoryM.ContactNo);
                param[4] = SqlHelper.GetParameter("@Title", ContactHistoryM.Title);
                param[5] = SqlHelper.GetParameter("@LinkReasonID", ContactHistoryM.LinkReasonID);
                param[6] = SqlHelper.GetParameter("@LinkMode", ContactHistoryM.LinkMode);
                param[7] = SqlHelper.GetParameter("@LinkDate", ContactHistoryM.LinkDate == null
                                      ? SqlDateTime.Null
                                      : SqlDateTime.Parse(ContactHistoryM.LinkDate.ToString()));
                param[8] = SqlHelper.GetParameter("@Linker", ContactHistoryM.Linker);
                param[9] = SqlHelper.GetParameter("@Contents", ContactHistoryM.Contents);

                param[10] = SqlHelper.GetParameter("@CanViewUser", ContactHistoryM.CanViewUser);
                param[11] = SqlHelper.GetParameter("@CanViewUserName", ContactHistoryM.CanViewUserName);

                param[12] = SqlHelper.GetParameter("@ModifiedDate", ContactHistoryM.ModifiedDate);
                param[13] = SqlHelper.GetParameter("@ModifiedUserID", ContactHistoryM.ModifiedUserID);
                SqlParameter paramid = new SqlParameter("@id", SqlDbType.Int);
                paramid.Direction = ParameterDirection.Output;
                param[14] = paramid;
                #endregion
                
                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.insertCustContact", comm, param);
                int contantid = Convert.ToInt32(comm.Parameters["@id"].Value);

                return contantid;
                         
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 根据联络信息ID修改联络信息
        /// <summary>
        /// 根据联络信息ID修改联络信息
        /// </summary>
        /// <param name="ContactM">联络信息</param>
        /// <returns>操作记录数</returns>
        public static bool UpdateContact(ContactHistoryModel ContactM)
        {
            try
            {
                #region 拼写修改联系人信息SQL语句
                StringBuilder sqlcontact = new StringBuilder();
                sqlcontact.AppendLine("UPDATE officedba.CustContact set ");
                sqlcontact.AppendLine("CompanyCD = @CompanyCD, ");
                sqlcontact.AppendLine("CustID = @CustID, ");
                sqlcontact.AppendLine("CustLinkMan = @CustLinkMan, ");
                sqlcontact.AppendLine("Title = @Title, ");
                sqlcontact.AppendLine("LinkReasonID = @LinkReasonID, ");
                sqlcontact.AppendLine("LinkMode = @LinkMode, ");
                sqlcontact.AppendLine("LinkDate = @LinkDate, ");
                sqlcontact.AppendLine("Linker = @Linker, ");
                sqlcontact.AppendLine("Contents = @Contents, ");
                sqlcontact.AppendLine("CanViewUser = @CanViewUser, ");
                sqlcontact.AppendLine("CanViewUserName = @CanViewUserName, ");
                sqlcontact.AppendLine("ModifiedDate = @ModifiedDate, ");
                sqlcontact.AppendLine("ModifiedUserID = @ModifiedUserID ");
                sqlcontact.AppendLine(" WHERE ");
                sqlcontact.AppendLine("ID = @ID ");
                #endregion

                #region 设置修改联系人信息参数
                SqlParameter[] param = new SqlParameter[14];
                param[0] = SqlHelper.GetParameter("@ID", ContactM.ID);
                param[1] = SqlHelper.GetParameter("@CompanyCD", ContactM.CompanyCD);
                param[2] = SqlHelper.GetParameter("@CustID", ContactM.CustID);
                param[3] = SqlHelper.GetParameter("@CustLinkMan", ContactM.CustLinkMan);
                param[4] = SqlHelper.GetParameter("@Title", ContactM.Title);
                param[5] = SqlHelper.GetParameter("@LinkReasonID", ContactM.LinkReasonID);
                param[6] = SqlHelper.GetParameter("@LinkMode", ContactM.LinkMode);
                param[7] = SqlHelper.GetParameter("@LinkDate", ContactM.LinkDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(ContactM.LinkDate.ToString()));

                param[8] = SqlHelper.GetParameter("@Linker", ContactM.Linker);
                param[9] = SqlHelper.GetParameter("@Contents", ContactM.Contents);
                param[10] = SqlHelper.GetParameter("@ModifiedDate", ContactM.ModifiedDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(ContactM.ModifiedDate.ToString()));

                param[11] = SqlHelper.GetParameter("@ModifiedUserID", ContactM.ModifiedUserID);
                param[12] = SqlHelper.GetParameter("@CanViewUser", ContactM.CanViewUser);
                param[13] = SqlHelper.GetParameter("@CanViewUserName", ContactM.CanViewUserName);
                #endregion

                SqlHelper.ExecuteTransSql(sqlcontact.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;

            }
            catch
            {
                return false;
            }
        }
        #endregion       

        #region 根据查询条件获取客户联络列表信息的方法
        /// <summary>
        /// 根据查询条件获取客户联络列表信息的方法
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="CustClass">客户类型</param>
        /// <param name="ContactM">联络model</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="txtLinkDateEnd">结束时间</param>
        /// <returns>结果集</returns>
        public static DataTable GetContactInfoBycondition(string CanUserID, string CustName, string CustLinkMan, ContactHistoryModel ContactM, string LinkDateBegin, string txtLinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "select  c.ID,i.id custid,c.ContactNo,SubString(c.Title,1,12) Title,i.CustName CustNam," +
                               " isnull(e.EmployeeName,'') Linker,i.CustNo,i.CustBig," +
                               " i.CanViewUser,i.Manager,i.Creator, " +
                               " isnull(CONVERT(varchar(100), c.LinkDate, 23),'') LinkDate,isnull(l.LinkManName,'') LinkManName " +
                               " from  " +
                               " officedba.CustContact c " +
                               " left join officedba.CustInfo i on c.custId = i.id" +
                               " left join officedba.CustLinkMan l on l.id = c.CustLinkMan " +
                    //" left join officedba.CodeCompanyType t on i.CustClass = t.id "+
                               " left join officedba.EmployeeInfo e on e.id = c.Linker " +
                               " where " +
                               " c.CompanyCD = '" + ContactM.CompanyCD + "'" +
                " and (c.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = c.Linker or c.CanViewUser = ',,' or c.CanViewUser is null )";
                if (CustName != "")
                    sql += " and i.id = '" + CustName + "'";                
                if (CustLinkMan != "")
                    sql += " and e.id = '" + CustLinkMan + "'";
                if (LinkDateBegin != "")
                    sql += " and c.linkDate >= '" + LinkDateBegin.ToString() + "'";
                if (txtLinkDateEnd != "")
                    sql += " and c.linkDate <= '" + txtLinkDateEnd.ToString() + "'";

                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 根据查询条件获取客户联络列表信息的方法
        /// <summary>
        /// 根据查询条件获取客户联络列表信息的方法
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="CustClass">客户类型</param>
        /// <param name="ContactM">联络model</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="txtLinkDateEnd">结束时间</param>
        /// <returns>结果集</returns>
        public static DataTable GetContactInfoBycondition(string CustName, string CustLinkMan, ContactHistoryModel ContactM, string LinkDateBegin, string txtLinkDateEnd, string ReasonId, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "select  c.ID,i.id custid,c.ContactNo,SubString(c.Title,1,12) Title,i.CustName CustNam," +
                               " isnull(e.EmployeeName,'') Linker," +
                               " isnull(CONVERT(varchar(100), c.LinkDate, 23),'') LinkDate,isnull(l.LinkManName,'') LinkManName " +
                               " from  " +
                               " officedba.CustContact c " +
                               " left join officedba.CustInfo i on c.custId = i.id" +
                               " left join officedba.CustLinkMan l on l.id = c.CustLinkMan " +
                    //" left join officedba.CodeCompanyType t on i.CustClass = t.id "+
                               " left join officedba.EmployeeInfo e on e.id = c.Linker " +
                               " where " +
                                   " c.CompanyCD = '" + ContactM.CompanyCD + "'";
                if (CustName != "")
                    sql += " and i.id = '" + CustName + "'";
                if (CustLinkMan != "")
                    sql += " and e.id = '" + CustLinkMan + "'";
                if (LinkDateBegin != "")
                    sql += " and c.linkDate >= '" + LinkDateBegin.ToString() + "'";
                if (txtLinkDateEnd != "")
                    sql += " and c.linkDate <= '" + txtLinkDateEnd.ToString() + "'";
                 if (txtLinkDateEnd != "")
                     sql += " and c.LinkReasonID = " + ReasonId.ToString() + "  ";
                
                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 获取所有延期未联络客户信息的方法
        /// <summary>
        /// 获取所有延期未联络的
        /// </summary>
        /// <returns></returns>
        public static DataTable GetContactDefer(string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region 拼写SQL语句
                string sql = "select " +
                                   " def.id,def.custno,def.custname,def.typename custtype,def.linkcycle,x.days, CONVERT(varchar(100), x.linkdate, 23) linkdate,e.EmployeeName " +
                               " from 	" +
                                   " (select " +
                                           " c.id,c.custno,c.custname,p2.typename,p.typename linkcycle " +
                                       " from " +
                                           " officedba.custinfo c " +
                                       " left join officedba.CodePublicType p on c.linkcycle = p.id " +
                                       " left join officedba.CodePublicType p2 on c.CustType = p2.id" +
                                       " where " +
                                           " c.CompanyCD = '" + CompanyCD + "' " +
                                       " and " +
                                           " c.linkcycle <> 0) def," +
                                   " (select " +
                                       " a.id,a.custid,c.days,a.linkdate,a.Linker " +
                                   " from " +
                                       " officedba.custcontact a" +
                                   " left join " +
                                       " (select b.custid,b.linkdate,datediff(dd,b.linkdate,getdate()) days from " +
                                           " (select custid,max(linkdate) linkdate from officedba.custcontact group by custid) b) c " +
                                   " on a.custid = c.custid and a.linkdate = c.linkdate) x," +
                                   " officedba.EmployeeInfo e " +
                               " where " +
                                   " def.id = x.custid " +
                               " and x.days > def.linkcycle " +
                               " and e.id = x.linker " +
                               " union " +
                               " select " +
                                   " d.id,d.custno,d.custname,d.custtype,d.linkcycle,d.days,CONVERT(varchar(100), d.linkdate, 23) linkdate," +
                                   " e.EmployeeName " +
                                " from " +
                                   " (" +
                                       " select " +
                                          "  c.id,c.custno,c.custname,p2.typename custtype,p.typename linkcycle," +
                                           " datediff(dd,c.CreatedDate,getdate()) days,c.CreatedDate linkdate,c.Manager linker " +
                                       " from " +
                                           " officedba.custinfo c " +
                                       " left join officedba.CodePublicType p on c.linkcycle = p.id " +
                                       " left join officedba.CodePublicType p2 on c.CustType = p2.id" +
                                       " where " +
                                           " c.LinkCycle<>0 " +
                                      "  and datediff(dd,c.CreatedDate,getdate()) > p.typename" +
                                      "  and c.CompanyCD = '" + CompanyCD + "' " +
                                      "  and c.id not in(select custid from officedba.custcontact group by custid)" +
                                   " ) d " +
                               " left join officedba.EmployeeInfo e on e.id = d.linker";
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

        #region 根据联络ID获得联络信息
        /// <summary>
        /// 根据联络ID获得联络信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="contactid">联络单ID</param>
        /// <returns>查询记录结果</returns>
        public static DataTable GetContactInfoByID(string CompanyCD, int contactid)
        {
            try
            {
                string sql = "select c.ID , " +
                                  " c.CompanyCD," +
                                  " c.CustID,i.CustNo,i.CustName CustNam," +
                                  " c.CustLinkMan,l.LinkManName," +
                                  " c.ContactNO," +
                                  " c.Title," +
                                  " c.LinkReasonID  ," +
                                  " c.LinkMode ," +
                                  " CONVERT(varchar(100), c.LinkDate, 23)LinkDate," +
                                  " c.Linker,e.EmployeeName LinkerName," +
                                  " c.Contents," +
                                  " CONVERT(varchar(100), c.ModifiedDate, 23) ModifiedDate ," +
                                  " c.ModifiedUserID,c.CanViewUser,c.CanViewUserName " +
                          " from  officedba.CustContact c " +
                               " left join officedba.CustInfo i on i.id = c.custid" +
                               " left join officedba.custlinkman l on l.id = c.custlinkman" +
                               " left join officedba.EmployeeInfo e on e.id = c.Linker" +
                           " where " +
                              " c.id = @id and c.CompanyCD = @CompanyCD ";

                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", contactid);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 客户联络一览表__报表

        /// <summary>
        /// 根据查询条件获取客户联络一览表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns>结果集</returns>
        public static DataTable GetContactInfoBycondition(string CustName,string CompanyCD , string  LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select  a.ID,b.id CustId,b.CustNo,b.CustName,isnull(d.EmployeeName,'') Linker,a.ContactNo,SubString(a.Title,1,12) Title,");
                sql.Append("e.TypeName,(case a.LinkMode when '1' then '电话' when '2' then '传真' when '3' then '邮件' when '4' then '远程在线'");
                sql.Append(" when '5' then '会晤拜访' when '6' then '综合' else '' end) LinkMode,isnull(c.LinkManName,'') LinkManName,isnull(CONVERT(varchar(100),a.LinkDate, 23),'') LinkDate ");
                sql.Append("from officedba.CustContact a ");
                sql.Append("inner join officedba.CustInfo b on b.id= a.custId ");
                sql.Append("left join officedba.CustLinkMan c on c.id = a.CustLinkMan ");
                sql.Append("left join officedba.EmployeeInfo d on d.id = b.Manager ");
                sql.Append("left join officedba.CodePublicType e on e.id = a.LinkReasonID where 1=1 and a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");

                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and a.linkDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.linkDate  <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetContactInfoByconditionPrint(string CustName, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select  a.ID,b.id CustId,b.CustNo,b.CustName,isnull(d.EmployeeName,'') Linker,a.ContactNo,SubString(a.Title,1,12) Title,");
                sql.Append("e.TypeName,(case a.LinkMode when '1' then '电话' when '2' then '传真' when '3' then '邮件' when '4' then '远程在线'");
                sql.Append(" when '5' then '会晤拜访' when '6' then '综合' else '' end) LinkMode,isnull(c.LinkManName,'') LinkManName,isnull(CONVERT(varchar(100),a.LinkDate, 23),'') LinkDate ");
                sql.Append("from officedba.CustContact a ");
                sql.Append("inner join officedba.CustInfo b on b.id= a.custId ");
                sql.Append("left join officedba.CustLinkMan c on c.id = a.CustLinkMan ");
                sql.Append("left join officedba.EmployeeInfo d on d.id = b.Manager ");
                sql.Append("left join officedba.CodePublicType e on e.id = a.LinkReasonID where 1=1 and a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");

                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and a.linkDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.linkDate  <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
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

        #region  客户联络次数统计_报表
        /// <summary>
        /// 客户联络次数统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCust(string CustName, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select b.CustId,b.ContactNum,a.CustNo,a.CustName from officedba.custInfo a,");
                sql.Append("(select count(1) ContactNum,CustId from officedba.CustContact c,officedba.custInfo d  where d.Id=c.CustId and d.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");
                if (CustName != "")
                {
                    sql.Append(" and d.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and c.linkDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and c.linkDate  <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                sql.Append(" group by c.CustId) b where a.Id=b.CustId ");
               
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 客户联络次数统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCustPrint(string CustName, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select b.CustId,b.ContactNum,a.CustNo,a.CustName from officedba.custInfo a,");
                sql.Append("(select count(1) ContactNum,CustId from officedba.CustContact c,officedba.custInfo d  where d.Id=c.CustId and d.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");
                if (CustName != "")
                {
                    sql.Append(" and d.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and c.linkDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and c.linkDate  <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }
                sql.Append(" group by CustId) b where a.Id=b.CustId ");
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

        #region  客户联络事由统计_报表
        /// <summary>
        /// 客户联络事由统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkReasonId">联络事由ID</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCustAndReason(string CustName, string LinkReasonId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select b.CustId,b.ContactNum,a.CustNo,a.CustName,b.TypeName,b.ReasonId from officedba.custInfo a,");
                sql.Append("(select count(1) ContactNum ,a.CustId, c.ID ReasonId,c.TypeName from officedba.CustContact a,");
                sql.Append("officedba.CustInfo b,officedba.CodePublicType c where a.linkReasonID=c.Id and a.custId=b.ID and a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");
                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }

                if (LinkReasonId != "") 
                {
                    sql.Append(" and c.Id=");
                    sql.Append(LinkReasonId);
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and a.linkDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.linkDate  <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                sql.Append(" group by c.ID,c.typeName,a.CustId) b where a.Id=b.CustId ");

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            }
            catch
            {
                return null;
            }

        }

        public static DataTable GetStatContactNumByCustAndReasonDatails(string CustName, string LinkReasonId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select b.CustId,b.ContactNum,a.CustNo,a.CustName,b.TypeName,b.ReasonId from officedba.custInfo a,");
                sql.Append("(select count(1) ContactNum ,a.CustId, c.ID ReasonId,c.TypeName from officedba.CustContact a,");
                sql.Append("officedba.CustInfo b,officedba.CodePublicType c where a.linkReasonID=c.Id and a.custId=b.ID and a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");
                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }

                if (LinkReasonId != "")
                {
                    sql.Append(" and c.Id=");
                    sql.Append(LinkReasonId);
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and a.linkDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.linkDate  <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                sql.Append(" group by c.ID,c.typeName,a.CustId) b where a.Id=b.CustId ");

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// 客户联络事由统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkReasonId">联络事由ID</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCustAndReasonPrint(string CustName, string LinkReasonId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select b.CustId,b.ContactNum,a.CustNo,a.CustName,b.TypeName,b.ReasonId from officedba.custInfo a,");
                sql.Append("(select count(1) ContactNum ,a.CustId, c.ID ReasonId,c.TypeName from officedba.CustContact a,");
                sql.Append("officedba.CustInfo b,officedba.CodePublicType c where a.linkReasonID=c.Id and a.custId=b.ID and a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");
                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }

                if (LinkReasonId != "")
                {
                    sql.Append(" and c.Id=");
                    sql.Append(LinkReasonId);
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and a.linkDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.linkDate  <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                sql.Append(" group by c.ID,c.typeName,a.CustId) b where a.Id=b.CustId ");
                sql.Append("Order by ");
                sql.Append(ord);
                return SqlHelper.ExecuteSql(sql.ToString());

            }
            catch
            {
                return null;
            }

        }


         public static DataTable GetStatContactNumByCustAndReasonPrint(string CustName, string LinkReasonId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord,bool reson)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                //sql.Append("select b.TypeName,b.ReasonId   from officedba.custInfo a,");
                //sql.Append("(select count(1)  c.ID ReasonId,c.TypeName from officedba.CustContact a,");
                //sql.Append("officedba.CustInfo b,officedba.CodePublicType c where a.linkReasonID=c.Id and a.custId=b.ID and a.CompanyCD='");

                sql.Append("select TypeName, isnull(a.ContactNum,'0')  as   ContactNum , linkReasonID from ");
                 sql.Append("(  ( SELECT COUNT(1) AS ContactNum, linkReasonID  FROM  officedba.CustContact   where  ");
                 sql.Append("CompanyCD='" + CompanyCD + "'    ");


                 if (LinkReasonId != "")
                 {
                     sql.Append(" and  LinkReasonId=");
                     sql.Append(LinkReasonId);
                 }

                 if (LinkDateBegin.ToString() != "")
                 {
                     sql.Append(" and  linkDate >= '");
                     sql.Append(LinkDateBegin.ToString());
                     sql.Append("' ");
                 }
                 if (LinkDateEnd.ToString() != "")
                 {
                     sql.Append(" and  linkDate  <dateadd(dd,1,'");
                     sql.Append(LinkDateEnd.ToString());
                     sql.Append("')");
                 }

                 sql.Append("     group by linkReasonID )  a  left join ");
                 sql.Append("( select * from officedba.CodePublicType where TypeFlag=4 and TypeCode=5 and CompanyCD='" + CompanyCD + "') as  b on a.linkReasonID = b.Id  )   ");


                //sql.Append(" SELECT  b.TypeName, isnull(a.ContactNum,'0') ContactNum, a.ComplainType");
                //sql.Append(" FROM  (select * from officedba.CodePublicType where TypeFlag=4 and TypeCode=5 and CompanyCD='" + CompanyCD + "')AS b  Left JOIN (SELECT COUNT(1) AS ContactNum, ComplainType ");
                //sql.Append(" FROM  officedba.CustContact where   a.linkReasonID=c.Id and a.custId=b.ID and a.CompanyCD=' ");

                //sql.Append(CompanyCD);
                //sql.Append("' ");
                //if (CustName != "")
                //{
                //    sql.Append(" and b.CustName like '%");
                //    sql.Append(CustName);
                //    sql.Append("%'");
                //}

                //if (LinkReasonId != "")
                //{
                //    sql.Append(" and c.Id=");
                //    sql.Append(LinkReasonId);
                //}

                //if (LinkDateBegin.ToString() != "")
                //{
                //    sql.Append(" and a.linkDate >= '");
                //    sql.Append(LinkDateBegin.ToString());
                //    sql.Append("' ");
                //}
                //if (LinkDateEnd.ToString() != "")
                //{
                //    sql.Append(" and a.linkDate  <dateadd(dd,1,'");
                //    sql.Append(LinkDateEnd.ToString());
                //    sql.Append("')");
                //}

                //sql.Append(" group by c.ID,c.typeName) b where a.Id=b.CustId ");
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

        #region  客户联络方式统计_报表
        /// <summary>
        /// 客户联络方式统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkReasonId">联络事由ID</param>
        /// <param name="LinkModeId">联系方式ID</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCustAndLinkMode(string CustName, string LinkReasonId, string LinkModeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select b.CustId,b.ContactNum,a.CustNo,a.CustName,(case b.LinkMode when '1' then '电话' when '2' then '传真' when '3' then '邮件' when '4' then '远程在线'" );
                sql.Append(" when '5' then '会晤拜访' when '6' then '综合' else '' end) LinkMode,b.TypeName,b.ReasonId from officedba.custInfo a,");
                sql.Append("(select count(1) ContactNum ,a.CustId, c.ID ReasonId,c.TypeName,a.LinkMode from officedba.CustContact a,");
                sql.Append("officedba.CustInfo b,officedba.CodePublicType c where a.linkReasonID=c.Id and a.custId=b.ID  and a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");
                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (LinkModeId != "")
                {
                    sql.Append(" and a.LinkMode=");
                    sql.Append(LinkModeId);
                }
                if (LinkReasonId != "")
                {
                    sql.Append(" and c.Id=");
                    sql.Append(LinkReasonId);
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and a.linkDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.linkDate  <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                sql.Append(" group by c.ID,c.typeName,a.CustId,a.LinkMode) b where a.Id=b.CustId");

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 客户联络方式统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkReasonId">联络事由ID</param>
        /// <param name="LinkModeId">联系方式ID</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCustAndLinkModePrint(string CustName, string LinkReasonId, string LinkModeId, string CompanyCD, string LinkDateBegin, string LinkDateEnd,string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select b.CustId,b.ContactNum,a.CustNo,a.CustName,(case b.LinkMode when '1' then '电话' when '2' then '传真' when '3' then '邮件' when '4' then '远程在线'");
                sql.Append(" when '5' then '会晤拜访' when '6' then '综合' else '' end) LinkMode,b.TypeName,b.ReasonId from officedba.custInfo a,");
                sql.Append("(select count(1) ContactNum ,a.CustId, c.ID ReasonId,c.TypeName,a.LinkMode from officedba.CustContact a,");
                sql.Append("officedba.CustInfo b,officedba.CodePublicType c where a.linkReasonID=c.Id and a.custId=b.ID  and a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");
                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (LinkModeId != "")
                {
                    sql.Append(" and a.LinkMode=");
                    sql.Append(LinkModeId);
                }
                if (LinkReasonId != "")
                {
                    sql.Append(" and c.Id=");
                    sql.Append(LinkReasonId);
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and a.linkDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.linkDate  <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                sql.Append(" group by c.ID,c.typeName,a.CustId,a.LinkMode) b where a.Id=b.CustId ");
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

        #region  客户联络人统计_报表
        /// <summary>
        /// 客户联络人统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkReasonId">联络人ID</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCustAndLinkMan(string CustName, string Linker, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select b.CustId,b.ContactNum,a.CustNo,a.CustName,c.EmployeeName from officedba.custInfo a, ");
                sql.Append("(select count(1) ContactNum,CustId,a.linker from officedba.CustContact a,officedba.custInfo b , ");
                sql.Append("officedba.EmployeeInfo c where b.Id=a.CustId and a.linker=c.Id and a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");
                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (Linker != "")
                {
                    sql.Append(" and a.Linker=");
                    sql.Append(Linker);
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and a.linkDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.linkDate  <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                sql.Append(" group by a.CustId,a.linker) b,officedba.EmployeeInfo c where a.Id=b.CustId and c.Id=b.linker ");

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 客户联络人统计
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="LinkReasonId">联络人ID</param>
        /// <param name="LinkDateBegin">联络开始时间</param>
        /// <param name="LinkDateEnd">联络结束时间</param>
        /// <returns></returns>
        public static DataTable GetStatContactNumByCustAndLinkManPrint(string CustName, string Linker, string CompanyCD, string LinkDateBegin, string LinkDateEnd,string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select b.CustId,b.ContactNum,a.CustNo,a.CustName,c.EmployeeName from officedba.custInfo a, ");
                sql.Append("(select count(1) ContactNum,CustId,a.linker from officedba.CustContact a,officedba.custInfo b , ");
                sql.Append("officedba.EmployeeInfo c where b.Id=a.CustId and a.linker=c.Id and a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");
                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (Linker != "")
                {
                    sql.Append(" and a.Linker=");
                    sql.Append(Linker);
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and a.linkDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.linkDate  <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                sql.Append(" group by a.CustId,a.linker) b,officedba.EmployeeInfo c where a.Id=b.CustId and c.Id=b.linker ");
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

        #region  未联络客户统计_报表
        /// <summary>
        /// 未联络客户统计
        /// </summary>
        /// <param name="CustName">公司ID</param>
        /// <param name="LinkReasonId">天数</param>
        /// <returns></returns>
        public static DataTable GetStatCustByDays(string CompanyCD, int Days,int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.CustName,f.TypeName,isnull(b.EmployeeName,'') EmployeeName from ");
                sql.Append(" officedba.CustInfo a left join officedba.EmployeeInfo b on a.Manager=b.Id ");
                sql.Append(" left join officedba.CodePublicType f on a.LinkCycle=f.Id ");
                sql.Append(" where a.Id not in(select CustId from officedba.CustContact where 1=1 ");
                if (Days.ToString()!= "")
                {
                    sql.Append(" and linkdate >dateadd(dd,-");
                    sql.Append(Days);
                    sql.Append(",getdate())");
                }
                sql.Append("  ) ");

                if (CompanyCD != "")
                {
                    sql.Append("and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("'");
                }

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 未联络客户统计
        /// </summary>
        /// <param name="CustName">公司ID</param>
        /// <param name="LinkReasonId">天数</param>
        /// <returns></returns>
        public static DataTable GetStatCustByDaysPrint(string CompanyCD, int Days,string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.CustName,f.TypeName,isnull(b.EmployeeName,'') EmployeeName from ");
                sql.Append(" officedba.CustInfo a left join officedba.EmployeeInfo b on a.Manager=b.Id ");
                sql.Append(" left join officedba.CodePublicType f on a.LinkCycle=f.Id ");
                sql.Append(" where a.Id not in(select CustId from officedba.CustContact where 1=1 ");
                if (Days.ToString() != "")
                {
                    sql.Append(" and linkdate >dateadd(dd,-");
                    sql.Append(Days);
                    sql.Append(",getdate())");
                }
                sql.Append("  ) ");

                if (CompanyCD != "")
                {
                    sql.Append("and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("'");
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

        #region 客户联络信息打印
        /// <summary>
        /// 客户联络信息打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="contactid"></param>
        /// <returns></returns>
        public static DataTable PrintContactInfoByID(string CompanyCD, string contactid)
        {
            try
            {
                string sql = "select c.ID ,c.CompanyCD,c.CustID,i.CustNo,i.CustName, " +
                              " c.CustLinkMan,l.LinkManName,c.ContactNO,c.Title, " +
                              " c.LinkReasonID, cp.TypeName LinkReasonNm," +
                              " (case c.LinkMode when '1' then '电话' when '2' then '传真' when '3' then '邮件' " +
                               " when '4' then '远程在线' when '5' then '会晤拜访' when '6' then '综合' end) LinkMode, " +
                              " CONVERT(varchar(100), c.LinkDate, 23)LinkDate, " +
                              " c.Linker,e.EmployeeName LinkerName,c.Contents, " +
                              " CONVERT(varchar(100), c.ModifiedDate, 23) ModifiedDate ,c.ModifiedUserID,c.CanViewUserName  " +
                      " from  officedba.CustContact c  " +
                           " left join officedba.CustInfo i on i.id = c.custid " +
                           " left join officedba.custlinkman l on l.id = c.custlinkman " +
                           " left join officedba.EmployeeInfo e on e.id = c.Linker " +
                           " left join officedba.CodePublicType cp on cp.id = c.LinkReasonID" +
                           " where " +
                              " c.id = @id and c.CompanyCD = @CompanyCD ";

                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", contactid);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 导出客户联络列表信息
        /// <summary>
        /// 导出客户联络列表信息
        /// </summary>
        /// <param name="CustName"></param>
        /// <param name="CustLinkMan"></param>
        /// <param name="ContactM"></param>
        /// <param name="LinkDateBegin"></param>
        /// <param name="txtLinkDateEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportContactInfo(string CanUserID,string CustID, string CustLinkMan, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                string sql = "select  c.ID,i.id custid,c.ContactNo,SubString(c.Title,1,12) Title,i.CustName CustNam," +
                               " isnull(e.EmployeeName,'') Linker," +
                               " isnull(CONVERT(varchar(100), c.LinkDate, 23),'') LinkDate,isnull(l.LinkManName,'') LinkManName " +
                               " from  " +
                               " officedba.CustContact c " +
                               " left join officedba.CustInfo i on c.custId = i.id" +
                               " left join officedba.CustLinkMan l on l.id = c.CustLinkMan " +                    
                               " left join officedba.EmployeeInfo e on e.id = c.Linker " +
                               " where " +
                                   " c.CompanyCD = '" + CompanyCD + "'"+
                " and (c.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = c.Linker or c.CanViewUser = ',,' or c.CanViewUser is null )";
                if (CustID != "")
                    sql += " and i.id = '" + CustID + "'";
                if (CustLinkMan != "")
                    sql += " and e.id = '" + CustLinkMan + "'";
                if (LinkDateBegin.ToString() != "")
                    sql += " and c.linkDate >= '" + LinkDateBegin.ToString() + "'";
                if (LinkDateEnd.ToString() != "")
                    sql += " and c.linkDate <= '" + LinkDateEnd.ToString() + "'";

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

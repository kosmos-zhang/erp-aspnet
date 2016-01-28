using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Model.Office.CustManager;
using XBase.Data.DBHelper;
using XBase.Common;

namespace XBase.Data.Office.CustManager
{
    public class CustCallDBHelper
    {
        #region 客户来电
        #region 根据来电号码获取来电记录列表
        /// <summary>
        /// 根据来电号码获取来电记录列表
        /// </summary>
        /// <param name="CustCallM"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetCustCallByTel(CustCallModel CustCallM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.CompanyCD,a.CustID,a.Tel,a.Callor,Convert(varchar(20), ");
                strSql.Append("     a.CallTime,120)CallTime,a.CallContents ,c.EmployeeName  ");
                strSql.Append(" from officedba.CustCall a   ");
                strSql.Append(" left join officedba.EmployeeInfo c on c.id = a.Creator ");
                strSql.Append("  where a.CompanyCD = @CompanyCD ");
                strSql.Append(" 	and ((a.CustID = (select b.id from officedba.custinfo b where CompanyCD = @CompanyCD ");
                strSql.Append(" 						and( b.Tel=@Tel or b.Mobile=@Tel or b.Fax =@Tel)))");
                strSql.Append(" 		or (a.CustID = (select d.ID from officedba.CustInfo d");
                strSql.Append(" 					left join officedba.CustLinkMan e on e.CustNo = d.CustNo and e.CompanyCD = d.CompanyCD  and e.CompanyCD = @CompanyCD");
                strSql.Append(" 						where e.WorkTel=@Tel or e.Fax = @Tel or e.Handset=@Tel");
                strSql.Append(" 							or e.HomeTel=@Tel))");
                strSql.Append(" 		) ");
                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustCallM.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel", CustCallM.Tel));

                #endregion

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 根据来电号码自动记录
        public static bool AddCustCallByTel(CustCallModel model)
        {
            //根据来电号码取所属客户ID

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.CustCall");
            sql.AppendLine("           (CustID,CompanyCD,Tel,Creator,ModifiedDate,ModifiedUserID)");
            sql.AppendLine("    values (@CustID,@CompanyCD,@Tel,@Creator,@ModifiedDate,@ModifiedUserID)");
            
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel", model.Tel));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", model.CustID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", model.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.Creator));

            return SqlHelper.ExecuteTransWithCommand(comm);

        }
        #endregion

        #region 根据来电号码判断是否有对应客户
        public static DataTable GetCustInfoByTel(string CompanyCD,string Tel)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT a.ID ");
            sql.AppendLine(",a.CustNO ");
            sql.AppendLine("FROM officedba.CustInfo a ");
            sql.AppendLine("left join officedba.CustLinkMan b on a.CustNo=b.CustNo and a.CompanyCD=b.CompanyCD ");
            sql.AppendLine(" where a.CompanyCD='" + CompanyCD + "' and (a.Tel='" + Tel + "' or a.Mobile = '" + Tel + "' or a.Fax = '" + Tel + "' ");
            sql.AppendLine("                                             or b.WorkTel='" + Tel + "' or b.Fax = '" + Tel + "' or b.Handset='" + Tel + "') ");

           
            return SqlHelper.ExecuteSql(sql.ToString());

        }
        #endregion

        #region 根据客户id获取客户信息
        //客户编号、客户名称、客户大类、管理分类、营销分类、时间分类、优质级别、客户细分、客户类别、
        //国家地区、分管业务员、
        public static DataTable GetCustInfoByID(string CustID, string Tel, string CompanyCD)
        {
            DataTable dt = GetLinkManByTel(Tel, CompanyCD);
            if (dt.Rows.Count == 0)
            {
                #region sql语句
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT '' LinkManName,''Age,''Sex,''MailAddress,''QQ,a.ID,a.CompanyCD,a.CustNo,a.CustName,(case a.CustBig when '1' then '企业' ");
                sql.AppendLine("                                                            else '会员' end) CustBig,");

                sql.AppendLine(" (case a.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' ");
                sql.AppendLine("                        when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage,");

                sql.AppendLine(" (case a.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' ");
                sql.AppendLine("                        when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell,");

                sql.AppendLine(" (case a.CustTypeTime when '1' then '老客户' when '2' then '新客户' ");
                sql.AppendLine("                        when '3' then '潜在客户' end) CustTypeTime,");

                sql.AppendLine(" a.CreditGrade,c.TypeName CreditGradeName,a.CustClass,d.CodeName CustClassName,");
                sql.AppendLine(" a.CustType,e.TypeName CustTypeName,a.CountryID,f.TypeName CountryIDName,");
                sql.AppendLine(" a.Manager,g.EmployeeName ManagerName ");

                sql.AppendLine("FROM officedba.CustInfo a ");
                //sql.AppendLine("left join officedba.CustLinkMan b on a.CustNo=b.CustNo and a.CompanyCD=b.CompanyCD ");
                sql.AppendLine("left join officedba.CodePublicType c on c.ID=a.CreditGrade ");
                sql.AppendLine("left join officedba.CodeCompanyType d on d.id = a.CustClass");
                sql.AppendLine("left join officedba.CodePublicType e on e.ID=a.CustType ");
                sql.AppendLine("left join officedba.CodePublicType f on f.ID=a.CountryID ");
                sql.AppendLine("left join officedba.EmployeeInfo g on g.ID=a.Manager ");
                sql.AppendLine(" where a.ID = '" + CustID + "'");
                #endregion

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            else
            {
                return dt;
            }
        }

        //根据号码判断是否有客户联系人
        public static DataTable GetLinkManByTel( string Tel, string CompanyCD)
        {            
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT a.ID,a.CompanyCD,a.CustNo,a.CustName,(case a.CustBig when '1' then '企业' ");
            sql.AppendLine("                                                            else '会员' end) CustBig,");

            sql.AppendLine(" (case a.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户' ");
            sql.AppendLine("                        when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage,");

            sql.AppendLine(" (case a.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' ");
            sql.AppendLine("                        when '3' then '方便型客户' when '4' then '道德型客户' end) CustTypeSell,");

            sql.AppendLine(" (case a.CustTypeTime when '1' then '老客户' when '2' then '新客户' ");
            sql.AppendLine("                        when '3' then '潜在客户' end) CustTypeTime,");

            sql.AppendLine(" a.CreditGrade,c.TypeName CreditGradeName,a.CustClass,d.CodeName CustClassName,");
            sql.AppendLine(" a.CustType,e.TypeName CustTypeName,a.CountryID,f.TypeName CountryIDName,");
            sql.AppendLine(" a.Manager,g.EmployeeName ManagerName,b.LinkManName,");
            sql.AppendLine(" (case b.Sex when '1' then '男' else '女' end)Sex,b.Age,b.MailAddress,b.QQ ");

            sql.AppendLine("FROM officedba.CustLinkMan b ");
            sql.AppendLine("left join officedba.CustInfo a on a.CustNo=b.CustNo and a.CompanyCD=b.CompanyCD ");
            sql.AppendLine("left join officedba.CodePublicType c on c.ID=a.CreditGrade ");
            sql.AppendLine("left join officedba.CodeCompanyType d on d.id = a.CustClass");
            sql.AppendLine("left join officedba.CodePublicType e on e.ID=a.CustType ");
            sql.AppendLine("left join officedba.CodePublicType f on f.ID=a.CountryID ");
            sql.AppendLine("left join officedba.EmployeeInfo g on g.ID=a.Manager ");
            sql.AppendLine(" where b.CompanyCD = '" + CompanyCD + "' and b.Handset = '" + Tel + "'");

            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 根据客户ID获取客户投诉信息
        public static DataTable GetCustComplainByCustID(CustComplainModel CustComplainM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.CompanyCD,a.CustID,a.Title, ");
                strSql.Append("    Convert(varchar(20),a.ComplainDate,120) ComplainDate,  ");
                strSql.Append(" 	(case a.State when '1' then '处理中'  ");
                strSql.Append("				  when '2' then '未处理' ");
                strSql.Append("				  when '3' then '已处理' end) State ");
                strSql.Append(" from officedba.CustComplain a ");
                strSql.Append("  where a.CustID = @CustID ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustComplainM.CustID.ToString()));
                #endregion

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 根据客户ID获取客户服务信息
        public static DataTable GetCustServiceByCustID(CustServiceModel CustServiceM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.CompanyCD,a.CustID,a.Title Title_fw, ");
                strSql.Append("    Convert(varchar(20),a.BeginDate,120) BeginDate,  ");
                strSql.Append(" 	(case a.Fashion when '1' then '远程支持'  ");
                strSql.Append("				  when '2' then '现场服务' ");
                strSql.Append("				  when '3' then '综合服务' end) Fashion,b.LinkManName ");
                strSql.Append(" from officedba.CustService a ");
                strSql.Append(" left join officedba.CustLinkMan b on b.ID = a.CustLinkMan ");
                strSql.Append("  where a.CustID = @CustID ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustServiceM.CustID.ToString()));
                #endregion

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 根据客户ID获取客户联络信息
        public static DataTable GetCustContactByCustID(ContactHistoryModel ContactHistoryM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.CompanyCD,a.CustID,a.Title Title_ll, ");
                strSql.Append("    Convert(varchar(20),a.LinkDate,23) LinkDate,  ");
                strSql.Append(" 	b.LinkManName ");
                strSql.Append(" from officedba.CustContact a ");
                strSql.Append(" left join officedba.CustLinkMan b on b.ID = a.CustLinkMan ");
                strSql.Append("  where a.CustID = @CustID ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", ContactHistoryM.CustID.ToString()));
                #endregion

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 根据记录id获取来电记录信息
        public static DataTable GetCallInfoByID(string CallID)
        {
            #region sql语句
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(" select a.ID,a.CompanyCD,a.CustID,a.Tel,a.Callor,Convert(varchar(20),a.CallTime,120)CallTime,a.CallContents,a.Creator ");
            sql.AppendLine(" 		,b.CustName,c.EmployeeName,Convert(varchar(20),a.ModifiedDate,120)ModifiedDate,  ");
            sql.AppendLine("        d.EmployeeName ModifiedUserName,a.Title ");
            sql.AppendLine("  from officedba.CustCall a ");
            sql.AppendLine(" left join officedba.CustInfo b on b.id = a.CustID ");
            sql.AppendLine(" left join officedba.EmployeeInfo c on c.id = a.Creator ");
            sql.AppendLine(" left join officedba.EmployeeInfo d on c.id = a.ModifiedUserID ");
            sql.AppendLine(" where a.ID='" + CallID + "' ");
            #endregion

            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 根据来电号码自动记录
        public static bool UpdateCallBuID(CustCallModel model)
        {
            //根据来电号码取所属客户ID
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update officedba.CustCall set ");
            sql.AppendLine("    Callor = @Callor,CallContents = @CallContents,ModifiedDate = @ModifiedDate ");
            sql.AppendLine("    ,ModifiedUserID = @ModifiedUserID,Title = @Title where ID = @ID ");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CallContents", model.CallContents));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Callor", model.Callor));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", model.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));

            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        #region 页面根据条件获取来电记录列表
        public static DataTable GetCustCallByCon(CustCallModel CustCallM,string DateBegin,string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.CompanyCD,a.CustID,a.Tel,a.Title,a.Callor,Convert(varchar(20), ");
                strSql.Append("     a.CallTime,120)CallTime,a.CallContents ,c.EmployeeName,b.CustName,a.ModifiedDate ");
                strSql.Append(" from officedba.CustCall a   ");
                strSql.Append(" left join officedba.CustInfo b on b.id = a.CustID ");
                strSql.Append(" left join officedba.EmployeeInfo c on c.id = a.Creator ");                
                strSql.Append("  where a.CompanyCD = @CompanyCD ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustCallM.CompanyCD));
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel", CustCallM.Tel));
                #endregion

                if (CustCallM.CustID != 0)
                {
                    strSql.Append(" and a.CustID = @CustID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustCallM.CustID.ToString()));
                }
                if (!string.IsNullOrEmpty(CustCallM.Tel))
                {
                    strSql.Append(" and a.Tel like @Tel ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel","%" + CustCallM.Tel + "%"));
                }

                if (!string.IsNullOrEmpty(DateBegin))
                {
                    strSql.AppendLine(" and a.CallTime >= @DateBegin");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateBegin", DateBegin));
                }

                if (!string.IsNullOrEmpty(DateEnd))
                {
                    strSql.AppendLine(" and a.CallTime <= @DateEnd");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateEnd", DateEnd));
                }

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 导出
        public static DataTable GetCustCallByCon(CustCallModel CustCallM, string DateBegin, string DateEnd,  string ord)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.CompanyCD,a.CustID,a.Tel,a.Title,a.Callor,Convert(varchar(20), ");
                strSql.Append("     a.CallTime,120)CallTime,a.CallContents ,c.EmployeeName,b.CustName,a.ModifiedDate ");
                strSql.Append(" from officedba.CustCall a   ");
                strSql.Append(" left join officedba.CustInfo b on b.id = a.CustID ");
                strSql.Append(" left join officedba.EmployeeInfo c on c.id = a.Creator ");
                strSql.Append("  where a.CompanyCD = @CompanyCD ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustCallM.CompanyCD));               
                #endregion

                if (CustCallM.CustID != 0)
                {
                    strSql.Append(" and a.CustID = @CustID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustCallM.CustID.ToString()));
                }
                if (!string.IsNullOrEmpty(CustCallM.Tel))
                {
                    strSql.Append(" and a.Tel like @Tel ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel", "%" + CustCallM.Tel + "%"));
                }

                if (!string.IsNullOrEmpty(DateBegin))
                {
                    strSql.AppendLine(" and a.CallTime >= @DateBegin");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateBegin", DateBegin));
                }

                if (!string.IsNullOrEmpty(DateEnd))
                {
                    strSql.AppendLine(" and a.CallTime <= @DateEnd");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateEnd", DateEnd));
                }
                if (!string.IsNullOrEmpty(ord))
                {
                    strSql.AppendLine(ord);
                }

                comm.CommandText = strSql.ToString();
                return SqlHelper.ExecuteSearch(comm);
                //return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion
        #endregion

        #region 综合查询

        #region 客户档案
        public static DataTable GetCust_daByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.CustNo,a.CustName,a.CustClass,b.CodeName CustClassName,a.ModifiedDate, ");
                strSql.Append(" 	a.CustType,c.TypeName CustTypeName,a.Manager,d.EmployeeName ManagerName, ");
                strSql.Append(" 	a.CreditGrade,e.TypeName CreditGradeName,(case a.RelaGrade when '1' then '密切' ");
                strSql.Append(" 											when '2' then '较好' when '3' then '一般'  ");
                strSql.Append("												when '4' then '较差' end) RelaGradeName,");
                strSql.Append("	    a.Creator,f.EmployeeName CreatorName,CONVERT(varchar(100), a.CreatedDate, 23) CreatedDate,");
                strSql.Append(" 	(case a.UsedStatus when '1' then '启用' when '0' then '停用' end) UsedStatusName");
                strSql.Append(" from officedba.CustInfo a");
                strSql.Append(" left join officedba.CodeCompanyType b on b.id = a.CustClass");
                strSql.Append(" left join officedba.CodePublicType c on c.id = a.CustType");
                strSql.Append(" left join officedba.EmployeeInfo d on d.id = a.Manager");
                strSql.Append(" left join officedba.CodePublicType e on e.id = a.CreditGrade");
                strSql.Append(" left join officedba.EmployeeInfo f on f.id = a.Creator");
                strSql.Append(" where a.CompanyCD = @CompanyCD ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustInfoM.CompanyCD));
                #endregion

                if (CustInfoM.ID != 0)
                {
                    strSql.Append(" and a.ID = @CustID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustInfoM.ID.ToString()));
                }
                if (!string.IsNullOrEmpty(CustInfoM.Tel))
                {
                    strSql.Append(" 	and ((a.Tel like @Tel or a.Mobile like @Tel or a.Fax like @Tel)");
                    strSql.Append(" 		or (a.ID in (select y.ID from officedba.CustInfo y");
                    strSql.Append(" 					left join officedba.CustLinkMan x on y.CustNo = x.CustNo and y.CompanyCD = x.CompanyCD and y.CompanyCD = @CompanyCD");
                    strSql.Append(" 						where x.WorkTel like @Tel or x.Fax like @Tel or x.Handset like @Tel");
                    strSql.Append(" 							or x.HomeTel like @Tel))");
                    strSql.Append(" 		) ");

                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel", "%" + CustInfoM.Tel + "%"));
                }

                if (!string.IsNullOrEmpty(DateBegin))
                {
                    strSql.AppendLine(" and a.CreatedDate >= @DateBegin");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateBegin", DateBegin));
                }

                if (!string.IsNullOrEmpty(DateEnd))
                {
                    strSql.AppendLine(" and a.CreatedDate <= @DateEnd");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateEnd", DateEnd));
                }

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 客户联系人
        public static DataTable GetCust_lxrByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.LinkManName,b.CustName,c.TypeName LinkTypeName,a.ModifiedDate, ");
                strSql.Append("  	(case a.Important when '1' then '不重要' when '2' then '普通' ");
                strSql.Append("  		when '3' then '重要' when '4' then '关键' else '' end) Important,");
                strSql.Append("  	a.WorkTel,a.Handset,a.Fax,a.HomeTel,a.QQ,Convert(varchar(100),a.Birthday,23) Birthday");
                strSql.Append("  from officedba.CustLinkMan a");
                strSql.Append("  left join officedba.custinfo b on b.custno = a.custno and a.companycd = b.companycd");
                strSql.Append("  left join officedba.CodePublicType c on c.id = a.LinkType");                
                strSql.Append(" where a.CompanyCD = @CompanyCD ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustInfoM.CompanyCD));
                #endregion

                if (CustInfoM.CustNo != "")
                {
                    strSql.Append(" and a.CustNo = @CustNo ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustNo", CustInfoM.CustNo));
                }
                if (!string.IsNullOrEmpty(CustInfoM.Tel))
                {
                    strSql.Append(" 	and ((a.CustNo in (select z.CustNo from officedba.custinfo z where CompanyCD = @CompanyCD ");
                    strSql.Append(" 						and( z.Tel like @Tel or z.Mobile like @Tel or z.Fax like @Tel)))");
                    strSql.Append(" 		or (a.WorkTel like @Tel or a.Fax like @Tel or a.Handset like @Tel");
                    strSql.Append(" 							or a.HomeTel like @Tel)");
                    strSql.Append(" 		) ");

                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel", "%" + CustInfoM.Tel + "%"));
                }

                if (!string.IsNullOrEmpty(DateBegin))
                {
                    strSql.AppendLine(" and a.CreatedDate >= @DateBegin");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateBegin", DateBegin));
                }

                if (!string.IsNullOrEmpty(DateEnd))
                {
                    strSql.AppendLine(" and a.CreatedDate <= @DateEnd");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateEnd", DateEnd));
                }

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 客户联络
        public static DataTable GetCust_llByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.ContactNo,a.Title,b.CustName,c.LinkManName,a.ModifiedDate, ");
                strSql.Append(" 	CONVERT(varchar(100), a.LinkDate, 23) LinkDate,d.EmployeeName  ");
                strSql.Append(" from officedba.CustContact a ");
                strSql.Append(" left join officedba.CustInfo b on a.custId = b.id ");
                strSql.Append(" left join officedba.CustLinkMan c on c.id = a.CustLinkMan ");
                strSql.Append(" left join officedba.EmployeeInfo d on d.id = a.Linker ");
                strSql.Append(" where a.CompanyCD = @CompanyCD ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustInfoM.CompanyCD));
                #endregion

                if (CustInfoM.ID != 0)
                {
                    strSql.Append(" and a.CustID = @CustID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustInfoM.ID.ToString()));
                }
                if (!string.IsNullOrEmpty(CustInfoM.Tel))
                {
                    strSql.Append(" 	and ((a.CustID in (select z.id from officedba.custinfo z where CompanyCD = @CompanyCD ");
                    strSql.Append(" 						and( z.Tel like @Tel or z.Mobile like @Tel or z.Fax like @Tel)))");
                    strSql.Append(" 		or (a.CustID in (select y.ID from officedba.CustInfo y");
                    strSql.Append(" 					left join officedba.CustLinkMan x on y.CustNo = x.CustNo and y.CompanyCD = x.CompanyCD and y.CompanyCD = @CompanyCD");
                    strSql.Append(" 						where x.WorkTel like @Tel or x.Fax like @Tel or x.Handset like @Tel");
                    strSql.Append(" 							or x.HomeTel like @Tel))");
                    strSql.Append(" 		) ");

                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel", "%" + CustInfoM.Tel + "%"));
                }

                if (!string.IsNullOrEmpty(DateBegin))
                {
                    strSql.AppendLine(" and a.LinkDate >= @DateBegin");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateBegin", DateBegin));
                }

                if (!string.IsNullOrEmpty(DateEnd))
                {
                    strSql.AppendLine(" and a.LinkDate <= @DateEnd");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateEnd", DateEnd));
                }

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 客户洽谈
        public static DataTable GetCust_qtByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.TalkNo,a.Title,b.CustName,c.LinkManName,a.ModifiedDate, ");
                strSql.Append(" 	CONVERT(varchar(100), a.CompleteDate, 23) CompleteDate,d.EmployeeName, ");
                strSql.Append("  	(case a.Status when '1' then '未开始' when '2' then '进行中' else '已完成' end) Status");
                strSql.Append("  from officedba.CustTalk a");
                strSql.Append(" left join officedba.CustInfo b on a.custId = b.id  ");
                strSql.Append(" left join officedba.CustLinkMan c on c.id = a.CustLinkMan ");
                strSql.Append(" left join officedba.EmployeeInfo d on d.id = a.Linker ");
                
                strSql.Append(" where a.CompanyCD = @CompanyCD ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustInfoM.CompanyCD));
                #endregion

                if (CustInfoM.ID != 0)
                {
                    strSql.Append(" and a.CustID = @CustID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustInfoM.ID.ToString()));
                }
                if (!string.IsNullOrEmpty(CustInfoM.Tel))
                {
                    strSql.Append(" 	and ((a.CustID in (select z.id from officedba.custinfo z where CompanyCD = @CompanyCD ");
                    strSql.Append(" 						and( z.Tel like @Tel or z.Mobile like @Tel or z.Fax like @Tel)))");
                    strSql.Append(" 		or (a.CustID in (select y.ID from officedba.CustInfo y");
                    strSql.Append(" 					left join officedba.CustLinkMan x on y.CustNo = x.CustNo and y.CompanyCD = x.CompanyCD and y.CompanyCD = @CompanyCD");
                    strSql.Append(" 						where x.WorkTel like @Tel or x.Fax like @Tel or x.Handset like @Tel");
                    strSql.Append(" 							or x.HomeTel like @Tel))");
                    strSql.Append(" 		) ");

                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel", "%" + CustInfoM.Tel + "%"));
                }

                if (!string.IsNullOrEmpty(DateBegin))
                {
                    strSql.AppendLine(" and a.CompleteDate >= @DateBegin");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateBegin", DateBegin));
                }

                if (!string.IsNullOrEmpty(DateEnd))
                {
                    strSql.AppendLine(" and a.CompleteDate <= @DateEnd");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateEnd", DateEnd));
                }

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 客户关怀
        public static DataTable GetCust_ghByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.LoveNo,a.Title,b.CustName,c.LinkManName,a.ModifiedDate, ");
                strSql.Append(" 	CONVERT(varchar(100), a.LoveDate, 23) LoveDate,d.EmployeeName ");
                strSql.Append(" from officedba.CustLove a");
                strSql.Append(" left join officedba.CustInfo b on a.custId = b.id ");
                strSql.Append(" left join officedba.CustLinkMan c on c.id = a.CustLinkMan");
                strSql.Append(" left join officedba.EmployeeInfo d on d.id = a.Linker");
                strSql.Append(" where a.CompanyCD = @CompanyCD ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustInfoM.CompanyCD));
                #endregion

                if (CustInfoM.ID != 0)
                {
                    strSql.Append(" and a.CustID = @CustID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustInfoM.ID.ToString()));
                }
                if (!string.IsNullOrEmpty(CustInfoM.Tel))
                {
                    strSql.Append(" 	and ((a.CustID in (select z.id from officedba.custinfo z where CompanyCD = @CompanyCD ");
                    strSql.Append(" 						and( z.Tel like @Tel or z.Mobile like @Tel or z.Fax like @Tel)))");
                    strSql.Append(" 		or (a.CustID in (select y.ID from officedba.CustInfo y");
                    strSql.Append(" 					left join officedba.CustLinkMan x on y.CustNo = x.CustNo and y.CompanyCD = x.CompanyCD and y.CompanyCD = @CompanyCD");
                    strSql.Append(" 						where x.WorkTel like @Tel or x.Fax like @Tel or x.Handset like @Tel");
                    strSql.Append(" 							or x.HomeTel like @Tel))");
                    strSql.Append(" 		) ");

                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel", "%" + CustInfoM.Tel + "%"));
                }

                if (!string.IsNullOrEmpty(DateBegin))
                {
                    strSql.AppendLine(" and a.LoveDate >= @DateBegin");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateBegin", DateBegin));
                }

                if (!string.IsNullOrEmpty(DateEnd))
                {
                    strSql.AppendLine(" and a.LoveDate <= @DateEnd");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateEnd", DateEnd));
                }

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 客户服务
        public static DataTable GetCust_fwByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.ServeNo,a.Title,b.CustName,c.LinkManName,a.ModifiedDate, ");
                strSql.Append(" 	CONVERT(varchar(100), a.BeginDate, 120) BeginDate,d.EmployeeName");
                strSql.Append(" from officedba.CustService a ");
                strSql.Append(" left join officedba.CustInfo b on a.custId = b.id  ");
                strSql.Append(" left join officedba.CustLinkMan c on c.id = a.CustLinkMan ");
                strSql.Append(" left join officedba.EmployeeInfo d on d.id = a.Executant ");
               
                strSql.Append(" where a.CompanyCD = @CompanyCD ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustInfoM.CompanyCD));
                #endregion

                if (CustInfoM.ID != 0)
                {
                    strSql.Append(" and a.CustID = @CustID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustInfoM.ID.ToString()));
                }
                if (!string.IsNullOrEmpty(CustInfoM.Tel))
                {
                    strSql.Append(" 	and ((a.CustID in (select z.id from officedba.custinfo z where CompanyCD = @CompanyCD ");
                    strSql.Append(" 						and( z.Tel like @Tel or z.Mobile like @Tel or z.Fax like @Tel)))");
                    strSql.Append(" 		or (a.CustID in (select y.ID from officedba.CustInfo y");
                    strSql.Append(" 					left join officedba.CustLinkMan x on y.CustNo = x.CustNo and y.CompanyCD = x.CompanyCD and y.CompanyCD = @CompanyCD");
                    strSql.Append(" 						where x.WorkTel like @Tel or x.Fax like @Tel or x.Handset like @Tel");
                    strSql.Append(" 							or x.HomeTel like @Tel))");
                    strSql.Append(" 		) ");

                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel", "%" + CustInfoM.Tel + "%"));
                }

                if (!string.IsNullOrEmpty(DateBegin))
                {
                    strSql.AppendLine(" and a.BeginDate >= @DateBegin");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateBegin", DateBegin));
                }

                if (!string.IsNullOrEmpty(DateEnd))
                {
                    strSql.AppendLine(" and a.BeginDate <= @DateEnd");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateEnd", DateEnd));
                }

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 客户投诉
        public static DataTable GetCust_tsByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.ComplainNo,a.Title,b.CustName,a.ModifiedDate, ");
                strSql.Append(" 		CONVERT(varchar(100), a.ComplainDate, 120) ComplainDate, ");
                strSql.Append(" 	(case a.Critical when '1' then '宽松' when '2' then '一般' ");
                strSql.Append(" 					when '3' then '较急' when '4' then '紧急' ");
                strSql.Append(" 					when '5' then '特急' end)Critical, ");
                strSql.Append(" 	c.EmployeeName,(case a.state when '1' then '处理中'  ");
                strSql.Append(" 				when '2' then '未处理' when '3' then '已处理' end)state ");
                strSql.Append(" from officedba. CustComplain a ");
                strSql.Append(" left join officedba.CustInfo b on a.custId = b.id ");
                strSql.Append(" left join officedba.EmployeeInfo c on c.id = a.DestClerk ");

                strSql.Append(" where a.CompanyCD = @CompanyCD ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustInfoM.CompanyCD));
                #endregion

                if (CustInfoM.ID != 0)
                {
                    strSql.Append(" and a.CustID = @CustID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustInfoM.ID.ToString()));
                }
                if (!string.IsNullOrEmpty(CustInfoM.Tel))
                {
                    strSql.Append(" 	and ((a.CustID in (select z.id from officedba.custinfo z where CompanyCD = @CompanyCD ");
                    strSql.Append(" 						and( z.Tel like @Tel or z.Mobile like @Tel or z.Fax like @Tel)))");
                    strSql.Append(" 		or (a.CustID in (select y.ID from officedba.CustInfo y");
                    strSql.Append(" 					left join officedba.CustLinkMan x on y.CustNo = x.CustNo and y.CompanyCD = x.CompanyCD and y.CompanyCD = @CompanyCD");
                    strSql.Append(" 						where x.WorkTel like @Tel or x.Fax like @Tel or x.Handset like @Tel");
                    strSql.Append(" 							or x.HomeTel like @Tel))");
                    strSql.Append(" 		) ");

                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel", "%" + CustInfoM.Tel + "%"));
                }

                if (!string.IsNullOrEmpty(DateBegin))
                {
                    strSql.AppendLine(" and a.ComplainDate >= @DateBegin");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateBegin", DateBegin));
                }

                if (!string.IsNullOrEmpty(DateEnd))
                {
                    strSql.AppendLine(" and a.ComplainDate <= @DateEnd");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateEnd", DateEnd));
                }

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 客户建议
        public static DataTable GetCust_jyByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append("  select a.ID,a.AdviceNo,a.Title,b.CustName,a.ModifiedDate, ");
                strSql.Append("  		c.LinkManName,d.EmployeeName,(case a.Accept when '1' then '暂不考虑' ");
                strSql.Append("  		when '2' then '一般' when '3' then '争取改进' else '一定做到' end) Accept, ");
                strSql.Append("  		CONVERT(varchar(100), a.AdviceDate, 120) AdviceDate	 ");
                strSql.Append("  from officedba.CustAdvice a ");
                strSql.Append("  left join officedba.CustInfo b on a.custId = b.id ");
                strSql.Append("  left join officedba.CustLinkMan c on c.id = a.CustLinkMan ");
                strSql.Append("  left join officedba.EmployeeInfo d on d.id = a.DestClerk ");
                strSql.Append(" where a.CompanyCD = @CompanyCD ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustInfoM.CompanyCD));
                #endregion

                if (CustInfoM.ID != 0)
                {
                    strSql.Append(" and a.CustID = @CustID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustInfoM.ID.ToString()));
                }
                if (!string.IsNullOrEmpty(CustInfoM.Tel))
                {
                    strSql.Append(" 	and ((a.CustID in (select z.id from officedba.custinfo z where CompanyCD = @CompanyCD ");
                    strSql.Append(" 						and( z.Tel like @Tel or z.Mobile like @Tel or z.Fax like @Tel)))");
                    strSql.Append(" 		or (a.CustID in (select y.ID from officedba.CustInfo y");
                    strSql.Append(" 					left join officedba.CustLinkMan x on y.CustNo = x.CustNo and y.CompanyCD = x.CompanyCD and y.CompanyCD = @CompanyCD");
                    strSql.Append(" 						where x.WorkTel like @Tel or x.Fax like @Tel or x.Handset like @Tel");
                    strSql.Append(" 							or x.HomeTel like @Tel))");
                    strSql.Append(" 		) ");

                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel", "%" + CustInfoM.Tel + "%"));
                }

                if (!string.IsNullOrEmpty(DateBegin))
                {
                    strSql.AppendLine(" and a.AdviceDate >= @DateBegin");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateBegin", DateBegin));
                }

                if (!string.IsNullOrEmpty(DateEnd))
                {
                    strSql.AppendLine(" and a.AdviceDate <= @DateEnd");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateEnd", DateEnd));
                }

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 客户来电记录
        public static DataTable GetCust_ldByCon(CustInfoModel CustInfoM, string DateBegin, string DateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,b.CustName,a.Tel,a.Title,a.Callor,c.EmployeeName,a.ModifiedDate, ");
                strSql.Append(" CONVERT(varchar(100), a.CallTime, 120) CallTime ");
                strSql.Append(" from officedba.CustCall a ");
                strSql.Append(" left join officedba.CustInfo b on a.custId = b.id ");
                strSql.Append(" left join officedba.EmployeeInfo c on c.id = a.Creator ");
                                
                strSql.Append(" where a.CompanyCD = @CompanyCD ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustInfoM.CompanyCD));
                #endregion

                if (CustInfoM.ID != 0)
                {
                    strSql.Append(" and a.CustID = @CustID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustInfoM.ID.ToString()));
                }
                if (!string.IsNullOrEmpty(CustInfoM.Tel))
                {
                    strSql.AppendLine(" and a.Tel like @Tel");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel", "%" + CustInfoM.Tel + "%"));
                }

                if (!string.IsNullOrEmpty(DateBegin))
                {
                    strSql.AppendLine(" and a.CallTime >= @DateBegin");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateBegin", DateBegin));
                }

                if (!string.IsNullOrEmpty(DateEnd))
                {
                    strSql.AppendLine(" and a.CallTime <= @DateEnd");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateEnd", DateEnd));
                }

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region Cust树
        /// <summary>
        /// Cust树
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetCustTree(CustInfoModel model, string CanUserID)
        {
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select a.ID,a.CustNo,a.CustName from officedba.CustInfo a where a.CompanyCD=@CompanyCD and a.UsedStatus=@UsedStatus ");
            searchSql.AppendLine(" and (a.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = a.Creator or '" + CanUserID + "' = a.Manager or a.CanViewUser = ',,' or a.CanViewUser is null )");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", "1"));


            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 根据记录id获取客户信息
        public static DataTable GetCustInfoByID(string CustID)
        {
            #region sql语句
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(" select a.ID,a.CompanyCD,a.CustNo,a.CustName, ");
            sql.AppendLine(" 	(case a.CustBig when '1' then '企业' else '会员' End) CustBig, ");
            sql.AppendLine(" (case a.CustTypeManage when '1' then 'VIP客户' when '2' then '主要客户'  ");
            sql.AppendLine(" 		when '3' then '普通客户' when '4' then '临时客户' end) CustTypeManage, ");
            sql.AppendLine(" 	(case a.CustTypeSell when '1' then '经济型客户' when '2' then '个性化客户' ");
            sql.AppendLine(" 		when '3' then '方便型客户'  when '4' then '道德型客户' end)CustTypeSell, ");
            sql.AppendLine(" 	(case a.CustTypeTime when '1' then '老客户' when '2' then '新客户'  ");
            sql.AppendLine(" 		when '3' then '潜在客户' end) CustTypeTime,	b.TypeName CreditGrade, ");
            sql.AppendLine(" 	c.CodeName CustClass,d.TypeName CustType,e.EmployeeName Creator, ");
            sql.AppendLine(" 	CONVERT(varchar(100), a.CreatedDate, 23) CreatedDate, ");
            sql.AppendLine(" 	a.CustNote,f.TypeName CountryID,g.TypeName AreaID, ");
            sql.AppendLine(" 	(case a.BusiType when '1' then '普通销售' when '2' then '委托代销' ");
            sql.AppendLine(" 		when '3' then '直运' when '4' then '零售' when '5' then '销售调拨' end) BusiType, ");
            sql.AppendLine(" 	h.EmployeeName Manager,a.Tel,a.Mobile,a.Fax,i.TypeName LinkCycle, ");
            sql.AppendLine(" 	(case a.CreditManage when '1' then '否' when '2' then '是' end) CreditManage, ");
            sql.AppendLine(" 	a.ReceiveAddress,a.MaxCredit,a.MaxCreditDate,j.TypeName PayType, ");
            sql.AppendLine("  	a.CanViewUserName,k.TotalPrice ");
            sql.AppendLine(" from officedba.CustInfo a ");
            sql.AppendLine(" left join officedba.CodePublicType b on b.id = a.CreditGrade ");
            sql.AppendLine(" left join officedba.CodeCompanyType c on c.id = a.CustClass ");
            sql.AppendLine(" left join officedba.CodePublicType d on d.id = a.CustType ");
            sql.AppendLine(" left join officedba.EmployeeInfo e on e.id = a.Creator ");
            sql.AppendLine(" left join officedba.CodePublicType f on f.id = a.CountryID ");
            sql.AppendLine(" left join officedba.CodePublicType g on g.id = a.AreaID ");
            sql.AppendLine(" left join officedba.EmployeeInfo h on h.id = a.Manager ");
            sql.AppendLine(" left join officedba.CodePublicType i on i.id = a.LinkCycle ");
            sql.AppendLine(" left join officedba.CodePublicType j on j.id = a.PayType ");
            sql.AppendLine(" left join (select z.CustID,sum(z.TotalPrice)TotalPrice ");
            sql.AppendLine("            from officedba.IncomeBill z ");
            sql.AppendLine("            where z.ConfirmStatus = '1' and z.BillingID = '0' ");
            sql.AppendLine("            group by z.CustID) k on k.CustID = a.ID ");
            sql.AppendLine(" where a.ID = '" + CustID + "' ");

            #endregion

            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 客户购买记录
        public static DataTable GetCust_gmByCon(CustInfoModel CustInfoM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.CustID,b.CustName,a.OrderNo,a.RealTotal, ");
                strSql.Append("     a.ModifiedDate,convert(varchar(10),a.OrderDate,23) OrderDate ");
                strSql.Append(" from officedba.SellOrder a ");
                strSql.Append(" left join officedba.CustInfo b on a.CustID = b.id ");

                strSql.Append(" where a.CompanyCD = @CompanyCD ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustInfoM.CompanyCD));
                #endregion

                if (CustInfoM.ID != 0)
                {
                    strSql.Append(" and a.CustID = @CustID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustInfoM.ID.ToString()));
                }
               
                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 发货记录
        public static DataTable GetCust_fhByCon(CustInfoModel CustInfoM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.SendNo,convert(varchar(10),a.SendDate,23) SendDate, ");
                strSql.Append("		b.ProductName,b.Specification,c.TypeName,a.ProductCount,a.ModifiedDate ");
                strSql.Append(" from officedba.SellSendDetail a ");
                strSql.Append(" left join officedba.ProductInfo b on b.id = a.ProductID ");
                strSql.Append(" left join officedba.CodePublicType c on c.id = b.ColorID ");
                strSql.Append(" left join officedba.SellSend d on d.SendNo = a.SendNo and d.CompanyCD = a.CompanyCD ");

                strSql.Append(" where a.CompanyCD = @CompanyCD ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustInfoM.CompanyCD));
                #endregion

                if (CustInfoM.ID != 0)
                {
                    strSql.Append(" and d.CustID = @CustID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustInfoM.ID.ToString()));
                }

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 回款计划
        public static DataTable GetCust_hkByCon(CustInfoModel CustInfoM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,a.GatheringNo,convert(varchar(10),a.PlanGatherDate,23) PlanGatherDate, ");
                strSql.Append("		a.PlanPrice,convert(varchar(10),a.FactGatherDate,23) FactGatherDate, ");
                strSql.Append(" 		a.FactPrice,a.ModifiedDate,(case a.State when '1' then '已回款'  ");
                strSql.Append(" 			when '2' then '未回款' else '部分回款' end)State ");
                strSql.Append(" from officedba.SellGathering a ");

                strSql.Append(" where a.CompanyCD = @CompanyCD ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustInfoM.CompanyCD));
                #endregion

                if (CustInfoM.ID != 0)
                {
                    strSql.Append(" and a.CustID = @CustID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustInfoM.ID.ToString()));
                }

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 回款记录
        public static DataTable GetCust_jlByCon(CustInfoModel CustInfoM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.ID,convert(varchar(10),a.AcceDate,23) AcceDate,a.TotalPrice, ");
                strSql.Append(" 		(case a.AcceWay when '0' then '现金' else '银行存款' end) AcceWay, ");
                strSql.Append(" 		( case b.InvoiceType when '1' then '增值税发票' when '2' then '普通地税'  ");
                strSql.Append(" 				when '3' then '普通国税' when '4' then '收据' end)InvoiceType, ");
                strSql.Append(" 		b.BillingNum,convert(varchar(10),b.CreateDate,23) CreateDate,a.ModifiedDate ");
                strSql.Append(" from officedba.IncomeBill a ");
                strSql.Append(" left join Officedba.Billing b on b.id = a.BillingID ");

                strSql.Append(" where a.CompanyCD = @CompanyCD and a.ConfirmStatus = '1' ");

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustInfoM.CompanyCD));
                #endregion

                if (CustInfoM.ID != 0)
                {
                    strSql.Append(" and a.CustID = @CustID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustInfoM.ID.ToString()));
                }

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 销售机会
        public static DataTable GetCust_jhByCon(CustInfoModel CustInfoM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string strCompanyCD = string.Empty;//单位编号
                int eid = 0;

                //strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                eid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;

                string strSql = "SELECT s.ID, s.ChanceNo,s.ModifiedDate, s.Title, CONVERT(varchar(100), s.FindDate, 23) AS FindDate, ";
                strSql += "ISNULL(c.CustName, ' ') AS CustName, ISNULL(cpt.TypeName, ' ') ";
                strSql += "AS ChanceTypeName, ISNULL(cpt1.TypeName, ' ') AS HapSourceName, ";
                strSql += "ISNULL(e.EmployeeName, ' ') AS EmployeeName, ";
                strSql += "CASE sp.Phase WHEN '1' THEN '初期沟通' WHEN '2' THEN '立项评估' ";
                strSql += "WHEN '3' THEN '需求分析' WHEN '4' THEN '方案指定' WHEN '5' THEN '招投标/竞争' ";
                strSql += "WHEN '6' THEN '商务谈判' WHEN '7' THEN '合同签约' END AS PhaseName, ";
                strSql += "CASE sp.State WHEN '1' THEN '跟踪' WHEN '2' THEN '成功' WHEN '3' ";
                strSql += "THEN '失败' WHEN '4' THEN '搁置' WHEN '5' THEN '失效' END AS StateName  , ";
                strSql += "isnull(CASE(SELECT count(1) FROM officedba.SellOffer AS sb ";
                strSql += "WHERE  sb.FromType = '1' AND sb.FromBillID = s.ID)  WHEN 0 THEN '不存在' END, '存在') AS FromBillText ";
                strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN ";

                strSql += "(SELECT scp.ID, scp.CompanyCD, scp.ChanceNo, scp.PushDate, scp.Seller, ";
                strSql += "scp.Phase, scp.State, scp.Feasibility, scp.DelayDate, scp.Remark, ";
                strSql += "scp.ModifiedDate, scp.ModifiedUserID ";

                strSql += " from (SELECT MAX(Phase) AS Phase, ChanceNo,CompanyCD FROM officedba.SellChancePush ";
                strSql += " GROUP BY ChanceNo,CompanyCD) AS scp1 ";
                strSql += " left join ( SELECT ID, CompanyCD, ChanceNo, PushDate, Seller,";
                strSql += " Phase, State, Feasibility, DelayDate, Remark, ModifiedDate, ModifiedUserID ";
                strSql += " from officedba.SellChancePush ) as scp ";
                strSql += " ON scp.ChanceNo  = scp1.ChanceNo and scp.CompanyCD  = scp1.CompanyCD AND scp.Phase = scp1.Phase ";
                strSql += " WHERE scp1.CompanyCD = @CompanyCD ";
             
                strSql += ") AS sp ON s.ChanceNo = sp.ChanceNo AND s.CompanyCD = sp.CompanyCD LEFT OUTER JOIN ";
                strSql += "officedba.CustInfo AS c ON s.CustID = c.ID AND s.CompanyCD = c.CompanyCD LEFT OUTER JOIN ";
                strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID AND s.CompanyCD = e.CompanyCD LEFT OUTER JOIN ";
                strSql += "officedba.CodePublicType AS cpt ON s.ChanceType = cpt.ID AND s.CompanyCD = cpt.CompanyCD LEFT OUTER JOIN ";
                strSql += "officedba.CodePublicType AS cpt1 ON s.HapSource = cpt1.ID AND s.CompanyCD = cpt1.CompanyCD ";

                strSql += " WHERE (s.CompanyCD = @CompanyCD) ";
                strSql += " AND (CHARINDEX('," + eid + ",',','+s.CanViewUser+',')>0  OR s.CanViewUser='' or s.CanViewUser is null OR  s.Creator=" + eid + ") ";

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CustInfoM.CompanyCD));
                #endregion

                if (CustInfoM.ID != 0)
                {
                    strSql += " and s.CustID = @CustID ";
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustInfoM.ID.ToString()));
                }

                comm.CommandText = strSql.ToString();
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #endregion
        
       

    }
}

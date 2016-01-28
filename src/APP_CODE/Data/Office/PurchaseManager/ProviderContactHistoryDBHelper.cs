/***********************************************
 * 类作用：   采购管理事务层处理               *
 * 建立人：   宋飞                          *
 * 修改人：   王保军                          *
 * 建立时间： 2009/04/27                       *
 * 修改时间： 2009/08/27                       *
 ***********************************************/
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using XBase.Model.Office.PurchaseManager;
using XBase.Model.Office.ProductionManager;
using XBase.Model.Office.SellManager;
using System.Collections;
using XBase.Common;
using System.Data.SqlTypes;

namespace XBase.Data.Office.PurchaseManager
{
    /// <summary>
    /// 类名：ProviderContactHistoryDBHelper
    /// 描述：采购供应商联络数据库层处理
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/29
    /// 最后修改时间：2009/04/29
    /// </summary>
    ///
    public class ProviderContactHistoryDBHelper
    {
        #region 绑定采购供应商联络事由
        public static DataTable GetdrpLinkReasonID()
        {
            string sql = "select ID,TypeName from officedba.CodePublicType where CompanyCD=@CompanyCD AND typeflag =7 and typecode =4 and usedstatus=1   ";
            SqlCommand comm = new SqlCommand();
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD ));
            comm.CommandText = sql.ToString();
            DataTable data = SqlHelper.ExecuteSearch(comm);
            return data;
        }
        #endregion

        #region 绑定采购供应商联络方式
        public static DataTable GetdrpLinkMode()
        {
            string sql = "select ID,TypeName from officedba.CodePublicType where CompanyCD= @CompanyCD AND typeflag =4 and typecode =6 and usedstatus=1   ";
            SqlCommand comm = new SqlCommand();
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.CommandText = sql.ToString();
            DataTable data = SqlHelper.ExecuteSearch(comm);
            return data;
        }
        #endregion


        #region 插入供应商联络
        public static bool InsertProviderContactHistory(ProviderContactHistoryModel model, out string ID)
        {

            ArrayList listADD = new ArrayList();
            bool result = false;
            ID = "0";

            #region  采购供应商联络添加SQL语句
            StringBuilder sqlArrive = new StringBuilder();


            sqlArrive.AppendLine("INSERT INTO officedba.ProviderContactHistory");
            sqlArrive.AppendLine("(CompanyCD,CustID,ContactNo,Title,LinkManID,LinkManName,LinkReasonID,LinkMode,LinkDate,Linker,Contents)");

            sqlArrive.AppendLine("VALUES (@CompanyCD,@CustID,@ContactNo,@Title,@LinkManID,@LinkManName,@LinkReasonID,@LinkMode,@LinkDate,@Linker,@Contents)");
            sqlArrive.AppendLine("set @ID=@@IDENTITY");

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ContactNo", model.ContactNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkManID", model.LinkManID));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkManName", model.LinkManName));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkReasonID", model.LinkReasonID));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkMode", model.LinkMode));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkDate", model.LinkDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.LinkDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Linker", model.Linker));
            comm.Parameters.Add(SqlHelper.GetParameter("@Contents", model.Contents));
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
            comm.CommandText = sqlArrive.ToString();


            listADD.Add(comm);
            #endregion


            try
            {
                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    ID = comm.Parameters["@ID"].Value.ToString();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 更新供应商联络
        public static bool UpdateProviderContactHistory(ProviderContactHistoryModel model)
        {
            if (model.ID <= 0)
            {
                return false;
            }
            ArrayList listADD = new ArrayList();
            bool result = false;

            #region  修改供应商联络
            StringBuilder sqlArrive = new StringBuilder();

            sqlArrive.AppendLine("Update  Officedba.ProviderContactHistory set CompanyCD=@CompanyCD,");
            sqlArrive.AppendLine("CustID=@CustID,ContactNo=@ContactNo,Title=@Title,LinkManID=@LinkManID,LinkManName=@LinkManName,LinkReasonID=@LinkReasonID,");
            sqlArrive.AppendLine("LinkMode=@LinkMode,LinkDate=@LinkDate,Linker=@Linker,Contents=@Contents where CompanyCD=@CompanyCD and ID=@ID");


            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ContactNo", model.ContactNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkManID", model.LinkManID));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkManName", model.LinkManName));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkReasonID", model.LinkReasonID));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkMode", model.LinkMode));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkDate", model.LinkDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.LinkDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Linker", model.Linker));
            comm.Parameters.Add(SqlHelper.GetParameter("@Contents", model.Contents));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.CommandText = sqlArrive.ToString();


            listADD.Add(comm);
            #endregion

            try
            {
                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 查询采购供应商联络列表所需数据
        public static DataTable SelectProviderContactHistoryList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string CustID, string Linker, string StartLinkDate, string EndLinkDate)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.CustID ,isnull(B.CustName,'') AS CustName,A.ContactNo,isnull(A.Linker,0) AS Linker ");
            sql.AppendLine("      ,isnull(C.EmployeeName,'') AS LinkerName ,isnull( Convert(varchar(100),A.LinkDate,23),'') AS LinkDate");
            sql.AppendLine("      ,A.LinkManName,isnull(D.TypeName,'') AS CustTypeName    ");
            sql.AppendLine(" FROM officedba.ProviderContactHistory AS A                                   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND  A.CustID=B.ID");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND  A.Linker=C.ID");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS D ON A.CompanyCD = D.CompanyCD AND  B.CustType=D.ID");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = @CompanyCD");
            if (CustID != "" && CustID != null)
            {
                sql.AppendLine(" AND A.CustID=@CustID ");
            }
            if (Linker != null && Linker != "")
            {
                sql.AppendLine(" AND A.Linker =@Linker");
            }
            if (StartLinkDate != "" && StartLinkDate != null)
            {
                sql.AppendLine(" AND A.LinkDate >=@StartLinkDate ");
            }
            if (EndLinkDate != null && EndLinkDate != "")
            {
                sql.AppendLine(" AND A.LinkDate <=@EndLinkDate");
            }
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Linker", Linker));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartLinkDate", StartLinkDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndLinkDate", EndLinkDate));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion

        #region 查找加载单个供应商联络
        public static DataTable SelectProviderContactHistory(int ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.CustID ,B.CustNo AS CustNo, B.CustName AS CustName,A.ContactNo,isnull(A.Title,'') AS Title ");
            sql.AppendLine("     ,A.LinkManID,A.LinkManName,A.LinkReasonID,isnull(D.TypeName,'') AS LinkReasonName, A.LinkMode ,isnull(Convert(varchar(100),A.LinkDate,23),'') AS LinkDate ");
            sql.AppendLine("     ,isnull(A.Linker,0) AS Linker, isnull(C.EmployeeName,'') AS LinkerName,A.Contents ");
            sql.AppendLine("     ,case A.LinkMode  when '0' then '' when '1' then '电话'  when '2' then '传真'  when '3' then '邮件' when '4' then '远程在线' when '5' then '会晤拜访'  when '6' then '综合'  end AS LinkModeName");

            sql.AppendLine(" FROM officedba.ProviderContactHistory AS A                                            ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.CustID=B.ID   ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND  A.Linker=C.ID  ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS D ON A.CompanyCD = D.CompanyCD AND  A.LinkReasonID=D.ID  ");

            sql.AppendLine("WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
            sql.AppendLine(" AND A.ID = " + ID + "");
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 删除供应商联络
        public static bool DeleteProviderContactHistory(string ID, string CompanyCD)
        {
            string allID = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] Delsql = new string[1];
            try
            {
                string[] IdS = null;
                ID = ID.Substring(0, ID.Length);
                IdS = ID.Split(',');

                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                //allUserID = sb.ToString();
                allID = sb.ToString().Replace("''", "','");
                Delsql[0] = "delete from  officedba.ProviderContactHistory where ID IN (" + allID + ") and CompanyCD = @CompanyCD ";
                SqlCommand comm = new SqlCommand();
                comm.CommandText = Delsql[0].ToString();

                //设置参数
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                ArrayList lstDelete = new ArrayList();
                comm.CommandText = Delsql[0].ToString();
                //添加基本信息更新命令
                lstDelete.Add(comm);
                return SqlHelper.ExecuteTransWithArrayList(lstDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取供应商联络延期警告的信息
        /// <summary>
        /// 获取供应商联络延期警告的信息
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectProviderContactDelay(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string CompanyCD)
        {
           
            try
            {
                SqlCommand comm = new SqlCommand();
                #region 拼写SQL语句
                string sql = "select " +
                                   " def.ID,def.CustName,def.LinkCycleName,x.DelayDays-def.LinkCycleName AS DelayDays, CONVERT(varchar(100), x.LinkDate, 23) LinkDate,isnull(e.EmployeeName,'') as LinkerName" +
                               " from " +
                                   " (select " +
                                       " c.id,c.CustName,p.typename linkcyclename" +
                                   " from " +
                                       " officedba.ProviderInfo c," +
                                       " officedba.CodePublicType p" +
                                   " where " +
                                       " c.CompanyCD = @CompanyCD" +
                                   " and " +
                                       " c.linkcycle <> 0 " +
                                   " and " +
                                       " c.linkcycle = p.id)  def," +
                                   " (select " +
                                       " a.id,a.custid,c.delaydays,a.linkdate,a.Linker " +
                                   " from " +
                                       " officedba.ProviderContactHistory a," +
                                       " (select b.custid,b.linkdate,datediff(dd,b.linkdate,getdate()) delaydays from " +
                                           " (select custid,max(linkdate) linkdate from officedba.ProviderContactHistory group by custid) b) c " +
                                   " where " +
                                       " a.custid = c.custid " +
                                   " and " +
                                       " a.linkdate = c.linkdate) x," +
                                   " officedba.EmployeeInfo e " +
                               " where " +
                                   " def.id = x.custid " +
                               " and x.delaydays > def.linkcyclename " +
                               " and e.id = x.linker " +
                               " union " +
                               " select " +
                                   " d.id,d.CustName,d.LinkCycleName,d.DelayDays,CONVERT(varchar(100), d.LinkDate, 23) LinkDate," +
                                   " e.EmployeeName LinkerName" +
                               " from " +
                                   " (select " +
                                       " c.id,c.custname,p.typename linkcyclename," +
                                       " datediff(dd,c.CreateDate,getdate()) delaydays,c.CreateDate linkdate,c.Manager linker " +
                                   " from " +
                                       " officedba.ProviderInfo c," +
                                       " officedba.CodePublicType p" +
                                   " where " +
                                       " c.LinkCycle<>0 " +
                                   " and c.CompanyCD = @CompanyCD" +
                                   " and c.id not in(select custid from officedba.ProviderContactHistory group by custid) " +
                                   " and c.linkcycle = p.id) d " +
                                   " ,officedba.EmployeeInfo e " +
                               " where " +
                                   " d.delaydays > d.linkcyclename " +
                               " and e.id = d.linker";
                #endregion

                //return SqlHelper.ExecuteSql(sql);
                comm.CommandText = sql.ToString();
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}

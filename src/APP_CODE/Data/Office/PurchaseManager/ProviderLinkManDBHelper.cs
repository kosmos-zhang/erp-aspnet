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
     
    public class ProviderLinkManDBHelper
    {
        #region 绑定采购供应商联系人类型
        public static DataTable GetdrpLinkType()
        {
            string sql = "select ID,TypeName from officedba.CodePublicType where typeflag =7 and typecode =6 and usedstatus=1  AND CompanyCD= @CompanyCD  ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql,param );
            return data;
        }
        #endregion


        #region 插入供应商联系人
        public static bool InsertProviderLinkMan(ProviderLinkManModel model, out string ID)
        {
            ArrayList listADD = new ArrayList();
            bool result = false;
            ID = "0";
            #region  采购供应商联系人添加SQL语句
            StringBuilder sqlArrive = new StringBuilder();
            sqlArrive.AppendLine("INSERT INTO officedba.ProviderLinkMan");
            sqlArrive.AppendLine("(CustNo,CompanyCD,LinkManName,Sex,Important,Company,Appellation,Department,Position,Operation,");
            sqlArrive.AppendLine("WorkTel,Fax,Handset,MailAddress,HomeTel,MSN,QQ,Post,HomeAddress,Remark,");
            sqlArrive.AppendLine("Age,Likes,LinkType,Birthday,PaperType,PaperNum,Photo,ModifiedDate,ModifiedUserID)");

            sqlArrive.AppendLine("VALUES (@CustNo,@CompanyCD,@LinkManName,@Sex,@Important,@Company,@Appellation,@Department,@Position,@Operation,");
            sqlArrive.AppendLine("@WorkTel,@Fax,@Handset,@MailAddress,@HomeTel,@MSN,@QQ,@Post,@HomeAddress,@Remark,");
            sqlArrive.AppendLine("@Age,@Likes,@LinkType,@Birthday,@PaperType,@PaperNum,@Photo,getdate(),@ModifiedUserID)");
            sqlArrive.AppendLine("set @ID=@@IDENTITY");

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CustNo", model.CustNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkManName", model.LinkManName));
            comm.Parameters.Add(SqlHelper.GetParameter("@Sex", model.Sex));
            comm.Parameters.Add(SqlHelper.GetParameter("@Important", model.Important));
            comm.Parameters.Add(SqlHelper.GetParameter("@Company", model.Company));
            comm.Parameters.Add(SqlHelper.GetParameter("@Appellation", model.Appellation));
            comm.Parameters.Add(SqlHelper.GetParameter("@Department", model.Department));
            comm.Parameters.Add(SqlHelper.GetParameter("@Position", model.Position));
            comm.Parameters.Add(SqlHelper.GetParameter("@Operation", model.Operation));
            comm.Parameters.Add(SqlHelper.GetParameter("@WorkTel", model.WorkTel));
            comm.Parameters.Add(SqlHelper.GetParameter("@Fax", model.Fax));
            comm.Parameters.Add(SqlHelper.GetParameter("@Handset", model.Handset));
            comm.Parameters.Add(SqlHelper.GetParameter("@MailAddress", model.MailAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@HomeTel", model.HomeTel));
            comm.Parameters.Add(SqlHelper.GetParameter("@MSN", model.MSN));
            comm.Parameters.Add(SqlHelper.GetParameter("@QQ", model.QQ));
            comm.Parameters.Add(SqlHelper.GetParameter("@Post", model.Post));
            comm.Parameters.Add(SqlHelper.GetParameter("@HomeAddress", model.HomeAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@Age", model.Age));
            comm.Parameters.Add(SqlHelper.GetParameter("@Likes", model.Likes));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkType", model.LinkType));
            comm.Parameters.Add(SqlHelper.GetParameter("@Birthday", model.Birthday == null  ? SqlDateTime.Null :SqlDateTime.Parse(model.Birthday.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@PaperType", model.PaperType));
            comm.Parameters.Add(SqlHelper.GetParameter("@PaperNum", model.PaperNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@Photo", model.Photo));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
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

        #region 更新供应商联系人
        public static bool UpdateProviderLinkMan(ProviderLinkManModel model)
        {
            if (model.ID <= 0)
            {
                return false;
            }
            ArrayList listADD = new ArrayList();
            bool result = false;

            #region  修改供应商联系人
            StringBuilder sqlArrive = new StringBuilder();

            sqlArrive.AppendLine("Update  Officedba.ProviderLinkMan set CustNo=@CustNo,");
            sqlArrive.AppendLine("CompanyCD=@CompanyCD,LinkManName=@LinkManName,Sex=@Sex,Important=@Important,Company=@Company,");
            sqlArrive.AppendLine("Appellation=@Appellation,Department=@Department,Position=@Position,Operation=@Operation,WorkTel=@WorkTel,");
            sqlArrive.AppendLine("Fax=@Fax,Handset=@Handset,MailAddress=@MailAddress,HomeTel=@HomeTel,MSN=@MSN,");
            sqlArrive.AppendLine("QQ=@QQ,Post=@Post,HomeAddress=@HomeAddress,Remark=@Remark,Age=@Age,");
            sqlArrive.AppendLine("Likes=@Likes,LinkType=@LinkType,Birthday=@Birthday,PaperType=@PaperType,PaperNum=@PaperNum,");
            sqlArrive.AppendLine("Photo=@Photo,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID where CompanyCD=@CompanyCD and ID=@ID");


            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CustNo", model.CustNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkManName", model.LinkManName));
            comm.Parameters.Add(SqlHelper.GetParameter("@Sex", model.Sex));
            comm.Parameters.Add(SqlHelper.GetParameter("@Important", model.Important));
            comm.Parameters.Add(SqlHelper.GetParameter("@Company", model.Company));
            comm.Parameters.Add(SqlHelper.GetParameter("@Appellation", model.Appellation));
            comm.Parameters.Add(SqlHelper.GetParameter("@Department", model.Department));
            comm.Parameters.Add(SqlHelper.GetParameter("@Position", model.Position));
            comm.Parameters.Add(SqlHelper.GetParameter("@Operation", model.Operation));
            comm.Parameters.Add(SqlHelper.GetParameter("@WorkTel", model.WorkTel));
            comm.Parameters.Add(SqlHelper.GetParameter("@Fax", model.Fax));
            comm.Parameters.Add(SqlHelper.GetParameter("@Handset", model.Handset));
            comm.Parameters.Add(SqlHelper.GetParameter("@MailAddress", model.MailAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@HomeTel", model.HomeTel));
            comm.Parameters.Add(SqlHelper.GetParameter("@MSN", model.MSN));
            comm.Parameters.Add(SqlHelper.GetParameter("@QQ", model.QQ));
            comm.Parameters.Add(SqlHelper.GetParameter("@Post", model.Post));
            comm.Parameters.Add(SqlHelper.GetParameter("@HomeAddress", model.HomeAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@Age", model.Age));
            comm.Parameters.Add(SqlHelper.GetParameter("@Likes", model.Likes));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkType", model.LinkType));
            comm.Parameters.Add(SqlHelper.GetParameter("@Birthday", model.Birthday == null    ? SqlDateTime.Null  : SqlDateTime.Parse(model.Birthday.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@PaperType", model.PaperType));
            comm.Parameters.Add(SqlHelper.GetParameter("@PaperNum", model.PaperNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@Photo", model.Photo));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
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


        #region 查询采购供应商联系人列表所需数据
        public static DataTable SelectProviderLinkManList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string CustNo, string LinkManName, string Handset, string Important, string LinkType, string StartBirthday, string EndBirthday)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.CustNo ,isnull(B.CustName,'') AS CustName,A.LinkManName,isnull(CONVERT(varchar(100),A.Birthday,23),'') AS  Birthday");
            sql.AppendLine("      ,case A.Important when '1' then '不重要' when '2' then '普通' ");
            sql.AppendLine("      when '3' then '重要'when '4' then '关键' end AS  Important");
            sql.AppendLine("   ,A.Appellation ,A.WorkTel,A.Handset,A.MailAddress,A.MSN,A.QQ,A.LinkType,isnull(C.TypeName,'') AS LinkTypeName,isnull( Convert(varchar(100),A.ModifiedDate,23),'') AS ModifiedDate");
            sql.AppendLine(" FROM officedba.ProviderLinkMan AS A                                   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND  A.CustNo=B.CustNo");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS C ON A.CompanyCD = C.CompanyCD AND  A.LinkType=C.ID");


            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD =@CompanyCD");
            if (CustNo != "" && CustNo != null)
            {
                sql.AppendLine(" AND A.CustNo=@CustNo ");
            }
            if (LinkManName != null && LinkManName != "")
            {
                sql.AppendLine(" AND A.LinkManName like '%"+LinkManName+"%' ");
            }
            if (Handset != null && Handset != "")
            {
                sql.AppendLine(" AND A.Handset like '%"+Handset+"%'  ");
            }
            if (Important != null && Important != "")
            {
                sql.AppendLine(" AND A.Important =@Important");
            }
            if (LinkType != "" && LinkType != null)
            {
                sql.AppendLine(" AND A.LinkType=@LinkType ");
            }
            if (StartBirthday != null && StartBirthday != "")
            {
                sql.AppendLine(" AND A.Birthday >=@StartBirthday");
            }
            if (EndBirthday != null && EndBirthday != "")
            {
                sql.AppendLine(" AND A.Birthday <=@EndBirthday");
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustNo", CustNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Important", Important));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@LinkType", LinkType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartBirthday", StartBirthday));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndBirthday", EndBirthday));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion

        #region 查找加载单个供应商联系人
        public static DataTable SelectProviderLinkMan(int ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.CustNo ,A.LinkManName ,A.Sex ,A.Important,A.Company,A.Appellation ,A.Department,A.Position,isnull(A.ModifiedUserID,'') AS ModifiedUserID  ");
            sql.AppendLine(",case A.Sex  when '1' then '男' when '2' then '女' end AS SexName,case A.Important  when '1' then '不重要' when '2' then '普通'  when '3' then '重要' when '4' then '关键' end AS ImportantName");
            sql.AppendLine("     ,A.Operation, A.WorkTel,A.Fax,A.Handset,A.MailAddress,A.HomeTel,A.MSN,A.QQ,A.Post,A.HomeAddress,isnull(B.CustName,'') AS CustName,CONVERT(varchar(100),A.ModifiedDate,23) AS  ModifiedDate");
            sql.AppendLine("     ,A.Remark,A.Age,A.Likes,A.LinkType,isnull(C.TypeName,'') AS LinkTypeName,isnull(Convert(varchar(100),A.Birthday,23),'') AS Birthday,A.PaperType,A.PaperNum,A.Photo");

            sql.AppendLine(" FROM officedba.ProviderLinkMan AS A                                                                           ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.CustNo=B.CustNo                        ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS C ON A.CompanyCD = C.CompanyCD AND A.LinkType=C.ID                        ");

            sql.AppendLine("WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
            sql.AppendLine(" AND A.ID =@ID");
            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@ID",  ID );

            return SqlHelper.ExecuteSql(sql.ToString(),param );
        }
        #endregion

        #region 删除供应商联系人
        public static bool DeleteProviderLinkMan(string ID, string CompanyCD)
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
                Delsql[0] = "delete from  officedba.ProviderLinkMan where ID IN (" + allID + ") and CompanyCD = @CompanyCD ";
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


        #region 供应商联络模块取供应商联系人
        public static DataTable GetProviderLinkManname(string CustNo, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.LinkManName                     ");

            sql.AppendLine(" FROM officedba.ProviderLinkMan AS A           ");

            sql.AppendLine("WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD =@CompanyCD         ");
            sql.AppendLine(" AND A.CustNo = @CustNo              ");
            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD );
            param[1] = SqlHelper.GetParameter("@CustNo", CustNo );
            return SqlHelper.ExecuteSql(sql.ToString(),param );
        }
        #endregion

    }
}

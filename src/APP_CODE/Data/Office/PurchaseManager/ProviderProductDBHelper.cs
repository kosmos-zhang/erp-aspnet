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
    /// 类名：ProviderProductDBHelper
    /// 描述：采购供应商物品推荐数据库层处理
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/28
    /// 最后修改时间：2009/04/28
    /// </summary>
    ///
    public class ProviderProductDBHelper
    {
        #region 插入供应商物品推荐
        public static bool InsertProviderProduct(ProviderProductModel model, out string ID)
        {

            ArrayList listADD = new ArrayList();
            bool result = false;
            ID = "0";

            #region  采购供应商联系人添加SQL语句
            StringBuilder sqlArrive = new StringBuilder();


            sqlArrive.AppendLine("INSERT INTO officedba.ProviderProduct");
            sqlArrive.AppendLine("(CompanyCD,CustNo,ProductID,Grade,Remark,JoinDate,Joiner)");
            sqlArrive.AppendLine("VALUES (@CompanyCD,@CustNo,@ProductID,@Grade,@Remark,@JoinDate,@Joiner)");
            sqlArrive.AppendLine("set @ID=@@IDENTITY");

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustNo", model.CustNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", model.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Grade", model.Grade));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@JoinDate", model.JoinDate == null
                                                        ? SqlDateTime.Null
                                                        :SqlDateTime.Parse(model.JoinDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Joiner", model.Joiner));
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
            catch 
            {
                return false;
            }
        }
        #endregion

        #region 更新供应商联系人
        public static bool UpdateProviderProduct(ProviderProductModel model)
        {
            if (model.ID <= 0)
            {
                return false;
            }
            ArrayList listADD = new ArrayList();
            bool result = false;

            #region  修改供应商联系人
            StringBuilder sqlArrive = new StringBuilder();

            sqlArrive.AppendLine("Update  Officedba.ProviderProduct set CompanyCD=@CompanyCD,");
            sqlArrive.AppendLine("CustNo=@CustNo,ProductID=@ProductID,Grade=@Grade,Remark=@Remark,");
            sqlArrive.AppendLine("JoinDate=@JoinDate,Joiner=@Joiner where CompanyCD=@CompanyCD and ID=@ID");
            

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustNo", model.CustNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", model.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Grade", model.Grade));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@JoinDate", model.JoinDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.JoinDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Joiner", model.Joiner));
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
            catch 
            {
                return false;
            }
        }
        #endregion


        #region 查询采购供应商物品推荐列表所需数据
        public static DataTable SelectProviderProductList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string CustNo, string ProductID, string Grade, string Joiner, string StartJoinDate, string EndJoinDate)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.CustNo ,isnull(B.CustName,'') AS CustName,A.ProductID,isnull(C.ProductName,'') AS  ProductName,A.Grade ");
            sql.AppendLine("      ,case A.Grade when '1' then '低' when '2' then '中' ");
            sql.AppendLine("      when '3' then '高' end AS  GradeName");
            sql.AppendLine("   ,isnull(A.Joiner,0) AS Joiner ,isnull(D.EmployeeName,'') AS JoinerName,isnull(Convert(varchar(100),A.JoinDate,23),'')  AS JoinDate ");
            sql.AppendLine(" FROM officedba.ProviderProduct AS A                                   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND  A.CustNo=B.CustNo");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS C ON A.CompanyCD = C.CompanyCD AND  A.ProductID=C.ID");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON A.CompanyCD = D.CompanyCD AND  A.Joiner=D.ID");


            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD =@CompanyCD ");
            if (CustNo != "" && CustNo != null)
            {
                sql.AppendLine(" AND A.CustNo=@CustNo ");
            }
            if (ProductID != null && ProductID != "")
            {
                sql.AppendLine(" AND A.ProductID =@ProductID");
            }
            if (Grade != "" && Grade != null)
            {
                sql.AppendLine(" AND A.Grade=@Grade ");
            }
            if (Joiner != null && Joiner != "")
            {
                sql.AppendLine(" AND A.Joiner =@Joiner");
            }
            if (StartJoinDate != null && StartJoinDate != "")
            {
                sql.AppendLine(" AND A.JoinDate >= @StartJoinDate");
            }
            if (EndJoinDate != "" && EndJoinDate != "")
            {
                sql.AppendLine(" AND A.JoinDate <= @EndJoinDate ");
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustNo", CustNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Grade", Grade));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Joiner", Joiner));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartJoinDate", StartJoinDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndJoinDate", EndJoinDate));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion

        #region 查找加载单个供应商物品推荐
        public static DataTable SelectProviderProduct(int ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.CustNo ,B.CustName AS CustName ,A.ProductID ,C.ProductName AS  ProductName,A.Grade  ");
            sql.AppendLine("     ,A.Remark, isnull(A.Joiner,0) AS Joiner,isnull(D.EmployeeName,'') AS JoinerName,Convert(varchar(100),A.JoinDate,23) AS JoinDate ");

            sql.AppendLine(" FROM officedba.ProviderProduct AS A                                                                  ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.CustNo=B.CustNo              ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS C ON A.CompanyCD = C.CompanyCD AND  A.ProductID=C.ID"               );
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON A.CompanyCD = D.CompanyCD AND  A.Joiner=D.ID");

            sql.AppendLine("WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = @CompanyCD");
            sql.AppendLine(" AND A.ID =@ID");

            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@ID", Convert .ToString (ID ));
            return SqlHelper.ExecuteSql(sql.ToString(),param );
        }
        #endregion

        #region 删除供应商物品推荐
        public static bool DeleteProviderProduct(string ID, string CompanyCD)
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
                Delsql[0] = "delete from  officedba.ProviderProduct where ID IN (" + allID + ") and CompanyCD = @CompanyCD ";
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

        #region 查找加载合同明细
        public static DataTable ISCunzaiProviderProduct(int ProductID, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID");
            sql.AppendLine(" FROM officedba.ProviderProduct AS A     ");
            sql.AppendLine("where A.CompanyCD =@CompanyCD AND A.ProductID =@ProductID ");
            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@ProductID", Convert .ToString ( ProductID ));
            return SqlHelper.ExecuteSql(sql.ToString(),param );
        }
        #endregion

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using System.Data.Sql;
using XBase.Model.Office.SellManager;
using XBase.Common;

namespace XBase.Data.Office.SellManager
{
    public class AdversarySellDBHelper
    {
        /// <summary>
        /// 选择竞争对手
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetAdversaryInfo(int? id)
        {

            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = "SELECT ISNULL(c.TypeName, '') AS TypeName, ISNULL(e.EmployeeName, '') AS EmployeeName, ";
            strSql += "a.CustNo, a.CustName, CONVERT(varchar(100), a.CreatDate, 23) AS CreatDate, a.ID, ";
            strSql += "a.Project, a.Power, a.disadvantage, a.Policy ";
            strSql += "FROM officedba.AdversaryInfo AS a LEFT OUTER JOIN ";
            strSql += "officedba.CodePublicType AS c ON a.CustType = c.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON a.Creator = e.ID  where  a.UsedStatus='1' and a.CompanyCD=@CompanyCD  ";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (id != null)
            {
                strSql += "  and a.ID=@ID";
                arr.Add(new SqlParameter("@ID", id));
            }

            return SqlHelper.ExecuteSql(strSql, arr); ;
        }

        /// <summary>
        /// 选择竞争对手控件列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetAdversaryInfo(string Title, string OrderNO, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {

            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = "SELECT ISNULL(c.TypeName, '') AS TypeName, ISNULL(e.EmployeeName, '') AS EmployeeName, ";
            strSql += "a.CustNo, a.CustName, CONVERT(varchar(100), a.CreatDate, 23) AS CreatDate, a.ID, ";
            strSql += "a.Project, a.Power, a.disadvantage, a.Policy ";
            strSql += "FROM officedba.AdversaryInfo AS a LEFT OUTER JOIN ";
            strSql += "officedba.CodePublicType AS c ON a.CustType = c.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON a.Creator = e.ID  where a.CompanyCD=@CompanyCD  and a.UsedStatus='1'";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (Title != null)
            {
                strSql += "  and a.CustName like @Title";
                arr.Add(new SqlParameter("@Title", "%"+Title+"%"));
            }
            if (OrderNO != null)
            {
                strSql += "  and a.CustNo like @OrderNO";
                arr.Add(new SqlParameter("@OrderNO", "%"+OrderNO+"%"));
            }

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="adversarySellModel"></param>
        /// <returns></returns>
        public static int? Insert(AdversarySellModel adversarySellModel)
        {
            int? id = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.AdversarySell(");
            strSql.Append("CompanyCD,CustNo,ChanceID,CustID,Project,Price,Power,Advantage,disadvantage,Policy,Remark,Creator,CreatDate,ModifiedDate,ModifiedUserID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@CustNo,@ChanceID,@CustID,@Project,@Price,@Power,@Advantage,@disadvantage,@Policy,@Remark,@Creator,getdate(),getdate(),@ModifiedUserID)");
            strSql.Append(";select @@IDENTITY");
            #region 参数
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@CustNo", SqlDbType.VarChar,50),
					new SqlParameter("@ChanceID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.Int,4),
					new SqlParameter("@Project", SqlDbType.VarChar,2000),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Power", SqlDbType.VarChar,2000),
					new SqlParameter("@Advantage", SqlDbType.VarChar,2000),
					new SqlParameter("@disadvantage", SqlDbType.VarChar,2000),
					new SqlParameter("@Policy", SqlDbType.VarChar,2000),
					new SqlParameter("@Remark", SqlDbType.VarChar,2000),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10)};
            parameters[0].Value = adversarySellModel.CompanyCD;
            parameters[1].Value = adversarySellModel.CustNo;
            parameters[2].Value = adversarySellModel.ChanceID;
            parameters[3].Value = adversarySellModel.CustID;
            parameters[4].Value = adversarySellModel.Project;
            parameters[5].Value = adversarySellModel.Price;
            parameters[6].Value = adversarySellModel.Power;
            parameters[7].Value = adversarySellModel.Advantage;
            parameters[8].Value = adversarySellModel.disadvantage;
            parameters[9].Value = adversarySellModel.Policy;
            parameters[10].Value = adversarySellModel.Remark;
            parameters[11].Value = adversarySellModel.Creator;
            parameters[12].Value = adversarySellModel.ModifiedUserID;
            foreach (SqlParameter para in parameters)
            {
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }
            #endregion

            object obj = SqlHelper.ExecuteScalar(strSql.ToString(), parameters);
            if (obj != null)
            {
                id = Convert.ToInt32(obj);
            }
            return id;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="adversarySellModel"></param>
        public static bool Update(AdversarySellModel adversarySellModel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.AdversarySell set ");
            strSql.Append("CustNo=@CustNo,");
            strSql.Append("ChanceID=@ChanceID,");
            strSql.Append("CustID=@CustID,");
            strSql.Append("Project=@Project,");
            strSql.Append("Price=@Price,");
            strSql.Append("Power=@Power,");
            strSql.Append("Advantage=@Advantage,");
            strSql.Append("disadvantage=@disadvantage,");
            strSql.Append("Policy=@Policy,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CustNo", SqlDbType.VarChar,50),
					new SqlParameter("@ChanceID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.Int,4),
					new SqlParameter("@Project", SqlDbType.VarChar,2000),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Power", SqlDbType.VarChar,2000),
					new SqlParameter("@Advantage", SqlDbType.VarChar,2000),
					new SqlParameter("@disadvantage", SqlDbType.VarChar,2000),
					new SqlParameter("@Policy", SqlDbType.VarChar,2000),
					new SqlParameter("@Remark", SqlDbType.VarChar,2000),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10)};
            parameters[0].Value = adversarySellModel.ID;
            parameters[1].Value = adversarySellModel.CustNo;
            parameters[2].Value = adversarySellModel.ChanceID;
            parameters[3].Value = adversarySellModel.CustID;
            parameters[4].Value = adversarySellModel.Project;
            parameters[5].Value = adversarySellModel.Price;
            parameters[6].Value = adversarySellModel.Power;
            parameters[7].Value = adversarySellModel.Advantage;
            parameters[8].Value = adversarySellModel.disadvantage;
            parameters[9].Value = adversarySellModel.Policy;
            parameters[10].Value = adversarySellModel.Remark;
            parameters[11].Value = adversarySellModel.ModifiedUserID;
            foreach (SqlParameter para in parameters)
            {
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }
            return SqlHelper.ExecuteTransSql(strSql.ToString(), parameters) == 1 ? true : false;
        }

        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="orderNos"></param>
        /// <returns></returns>
        public static bool DelOrder(string orderIDs)
        {
            string strCompanyCD = string.Empty;//单位编号
            bool isSucc = false;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            TransactionManager tran = new TransactionManager();
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            
            tran.BeginTransaction();
            try
            {
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.AdversarySell WHERE ID IN ( " + orderIDs + " ) and CompanyCD='" + strCompanyCD + "'", null);
             
                tran.Commit();
                isSucc = true;

            }
            catch (Exception ex)
            {
                tran.Rollback();
                isSucc = false;
                throw ex;
            }
            return isSucc;
        }


        /// <summary>
        /// 获取单据列表 
        /// </summary>
        /// <param name="sellContractModel">sellContractModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(AdversarySellModel adversarySellModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            strSql = "SELECT a.CustNo,a.ModifiedDate, CONVERT(varchar(100), a.CreatDate, 23) AS CreatDate, ";
            strSql += "c.CustNo AS CustNo1,isnull(c.CustName,'') as CustName1, ISNULL(e.EmployeeName, '') AS EmployeeName, s.ChanceNo, ";
            strSql += "ad.CustName, a.ID FROM officedba.AdversarySell AS a LEFT OUTER JOIN ";
            strSql += "officedba.AdversaryInfo AS ad ON a.CustNo = ad.CustNo AND a.CompanyCD = ad.CompanyCD ";
            strSql += "LEFT OUTER JOIN officedba.CustInfo AS c ON a.CustID = c.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON a.Creator = e.ID LEFT OUTER JOIN ";
            strSql += "officedba.SellChance AS s ON a.ChanceID = s.ID  where  a.CompanyCD=@CompanyCD";                                        

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            if (adversarySellModel.ChanceID != null)
            {
                strSql += " and a.ChanceID= @ChanceID ";
                arr.Add(new SqlParameter("@ChanceID", adversarySellModel.ChanceID));
            }
            if (adversarySellModel.CustID != null)
            {
                strSql += " and a.CustID= @CustID ";
                arr.Add(new SqlParameter("@CustID", adversarySellModel.CustID));
            }
            if (adversarySellModel.CustNo != null)
            {
                strSql += " and a.CustNo like @CustNo ";
                arr.Add(new SqlParameter("@CustNo", "%"+adversarySellModel.CustNo+"%"));
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = "select * from  officedba.sellmodule_report_AdversarySell WHERE (ID = @ID) AND (CompanyCD = @CompanyCD)";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@ID",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }


        /// <summary>
        /// 获取单据主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = "SELECT a.CustNo, a.ChanceID, a.CustID, a.Project,isnull( a.Price,0) as Price, a.Power, a.Advantage, ";
            strSql += "a.disadvantage, a.Policy, a.Remark, a.Creator, CONVERT(varchar(100),a.CreatDate, 23) ";
            strSql += "AS CreatDate, c.CustNo AS CustNo1,c.CustName AS CustName1,  ISNULL(e.EmployeeName, '') AS EmployeeName, ";
            strSql += "s.ChanceNo, ad.CustName , CONVERT(varchar(100), a.ModifiedDate, 23) AS ModifiedDate, a.ModifiedUserID FROM officedba.AdversarySell AS a LEFT OUTER JOIN ";
            strSql += "officedba.AdversaryInfo AS ad ON a.CustNo = ad.CustNo AND a.CompanyCD = ad.CompanyCD LEFT OUTER JOIN ";
            strSql += "officedba.CustInfo AS c ON a.CustID = c.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON a.Creator = e.ID LEFT OUTER JOIN ";
            strSql += "officedba.SellChance AS s ON a.ChanceID = s.ID ";                                                        


            strSql += " WHERE (a.ID = @ID ) AND (a.CompanyCD = @CompanyCD)";
            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@ID", orderID);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }
    }
}

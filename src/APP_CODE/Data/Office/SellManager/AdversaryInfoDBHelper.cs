/***********************************************************************
 * 
 * Module Name:XBase.Data.Office.SystemManager.AdversaryInfoDBHelper.cs
 * Current Version: 1.0 
 * Creator: 周军
 * Auditor:2009-01-12
 * End Date:
 * Description: 竞争对手数据库层处理
 * Version History:
 ***********************************************************************/
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
    /// <summary>
    /// 竞争对手数据层
    /// </summary>
    public class AdversaryInfoDBHelper
    {
        /// <summary>
        /// 添加单据
        /// </summary>
        /// <param name="sellChanceModel">销售机会表实体</param>
        /// <param name="sellChancePushModel">销售阶段表实体</param>
        /// <returns>是否添加成功</returns>
        public static bool InsertOrder(AdversaryInfoModel adversaryInfoModel, List<AdversaryDynamicModel> adversaryDynamicModelList)
        {
            bool isSucc = false;//是否添加成功

            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                InsertAdversaryInfo( adversaryInfoModel, tran);
                InsertAdversaryDynamic(adversaryDynamicModelList, tran);
                tran.Commit();
                isSucc = true;
            }
            catch(Exception ex)
            {
                tran.Rollback();
                isSucc = false;
                throw ex;
            }


            return isSucc;
        }

        /// <summary>
        /// 添加zhu表信息
        /// </summary>
        /// <param name="adversaryInfoModel"></param>
        /// <param name="tran"></param>
        private static void InsertAdversaryInfo(AdversaryInfoModel adversaryInfoModel,TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.AdversaryInfo(");
            strSql.Append("CompanyCD,BigType,CustType,CustClass,CustNo,CustName,ShortNam,PYShort,AreaID,SetupDate,ArtiPerson,CompanyType,StaffCount,SetupMoney,SetupAddress,website,CapitalScale,SellArea,SaleroomY,ProfitY,TaxCD,BusiNumber,IsTax,Address,Post,ContactName,Tel,Mobile,email,CustNote,Product,Market,SellMode,Project,Power,Advantage,disadvantage,Policy,Remark,UsedStatus,Creator,CreatDate,ModifiedDate,ModifiedUserID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@BigType,@CustType,@CustClass,@CustNo,@CustName,@ShortNam,@PYShort,@AreaID,@SetupDate,@ArtiPerson,@CompanyType,@StaffCount,@SetupMoney,@SetupAddress,@website,@CapitalScale,@SellArea,@SaleroomY,@ProfitY,@TaxCD,@BusiNumber,@IsTax,@Address,@Post,@ContactName,@Tel,@Mobile,@email,@CustNote,@Product,@Market,@SellMode,@Project,@Power,@Advantage,@disadvantage,@Policy,@Remark,@UsedStatus,@Creator,getdate(),getdate(),@ModifiedUserID)");
            #region 参数
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@BigType", SqlDbType.Char,1),
					new SqlParameter("@CustType", SqlDbType.Int,4),
					new SqlParameter("@CustClass", SqlDbType.Int,4),
					new SqlParameter("@CustNo", SqlDbType.VarChar,50),
					new SqlParameter("@CustName", SqlDbType.VarChar,100),
					new SqlParameter("@ShortNam", SqlDbType.VarChar,50),
					new SqlParameter("@PYShort", SqlDbType.VarChar,50),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@SetupDate", SqlDbType.DateTime),
					new SqlParameter("@ArtiPerson", SqlDbType.VarChar,20),
					new SqlParameter("@CompanyType", SqlDbType.Char,1),
					new SqlParameter("@StaffCount", SqlDbType.Int,4),
					new SqlParameter("@SetupMoney", SqlDbType.Decimal,9),
					new SqlParameter("@SetupAddress", SqlDbType.VarChar,100),
					new SqlParameter("@website", SqlDbType.VarChar,100),
					new SqlParameter("@CapitalScale", SqlDbType.Decimal,9),
					new SqlParameter("@SellArea", SqlDbType.VarChar,200),
					new SqlParameter("@SaleroomY", SqlDbType.Decimal,9),
					new SqlParameter("@ProfitY", SqlDbType.Decimal,9),
					new SqlParameter("@TaxCD", SqlDbType.VarChar,50),
					new SqlParameter("@BusiNumber", SqlDbType.VarChar,50),
					new SqlParameter("@IsTax", SqlDbType.Char,1),
					new SqlParameter("@Address", SqlDbType.VarChar,100),
					new SqlParameter("@Post", SqlDbType.VarChar,10),
					new SqlParameter("@ContactName", SqlDbType.VarChar,50),
					new SqlParameter("@Tel", SqlDbType.VarChar,50),
					new SqlParameter("@Mobile", SqlDbType.VarChar,50),
					new SqlParameter("@email", SqlDbType.VarChar,50),
					new SqlParameter("@CustNote", SqlDbType.VarChar,1024),
					new SqlParameter("@Product", SqlDbType.VarChar,800),
					new SqlParameter("@Market", SqlDbType.VarChar,50),
					new SqlParameter("@SellMode", SqlDbType.VarChar,50),
					new SqlParameter("@Project", SqlDbType.VarChar,1024),
					new SqlParameter("@Power", SqlDbType.VarChar,1024),
					new SqlParameter("@Advantage", SqlDbType.VarChar,1024),
					new SqlParameter("@disadvantage", SqlDbType.VarChar,1024),
					new SqlParameter("@Policy", SqlDbType.VarChar,1024),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
					new SqlParameter("@UsedStatus", SqlDbType.Char,1),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10)};
            parameters[0].Value = adversaryInfoModel.CompanyCD;
            parameters[1].Value = adversaryInfoModel.BigType;
            parameters[2].Value = adversaryInfoModel.CustType;
            parameters[3].Value = adversaryInfoModel.CustClass;
            parameters[4].Value = adversaryInfoModel.CustNo;
            parameters[5].Value = adversaryInfoModel.CustName;
            parameters[6].Value = adversaryInfoModel.ShortNam;
            parameters[7].Value = adversaryInfoModel.PYShort;
            parameters[8].Value = adversaryInfoModel.AreaID;
            parameters[9].Value = adversaryInfoModel.SetupDate;
            parameters[10].Value = adversaryInfoModel.ArtiPerson;
            parameters[11].Value = adversaryInfoModel.CompanyType;
            parameters[12].Value = adversaryInfoModel.StaffCount;
            parameters[13].Value = adversaryInfoModel.SetupMoney;
            parameters[14].Value = adversaryInfoModel.SetupAddress;
            parameters[15].Value = adversaryInfoModel.website;
            parameters[16].Value = adversaryInfoModel.CapitalScale;
            parameters[17].Value = adversaryInfoModel.SellArea;
            parameters[18].Value = adversaryInfoModel.SaleroomY;
            parameters[19].Value = adversaryInfoModel.ProfitY;
            parameters[20].Value = adversaryInfoModel.TaxCD;
            parameters[21].Value = adversaryInfoModel.BusiNumber;
            parameters[22].Value = adversaryInfoModel.IsTax;
            parameters[23].Value = adversaryInfoModel.Address;
            parameters[24].Value = adversaryInfoModel.Post;
            parameters[25].Value = adversaryInfoModel.ContactName;
            parameters[26].Value = adversaryInfoModel.Tel;
            parameters[27].Value = adversaryInfoModel.Mobile;
            parameters[28].Value = adversaryInfoModel.email;
            parameters[29].Value = adversaryInfoModel.CustNote;
            parameters[30].Value = adversaryInfoModel.Product;
            parameters[31].Value = adversaryInfoModel.Market;
            parameters[32].Value = adversaryInfoModel.SellMode;
            parameters[33].Value = adversaryInfoModel.Project;
            parameters[34].Value = adversaryInfoModel.Power;
            parameters[35].Value = adversaryInfoModel.Advantage;
            parameters[36].Value = adversaryInfoModel.disadvantage;
            parameters[37].Value = adversaryInfoModel.Policy;
            parameters[38].Value = adversaryInfoModel.Remark;
            parameters[39].Value = adversaryInfoModel.UsedStatus;
            parameters[40].Value = adversaryInfoModel.Creator;
            parameters[41].Value = adversaryInfoModel.ModifiedUserID;
            foreach (SqlParameter para in parameters)
            {
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }
            #endregion
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 添加子表信息
        /// </summary>
        /// <param name="adversaryDynamicModelList"></param>
        /// <param name="tran"></param>
        private static void InsertAdversaryDynamic(List<AdversaryDynamicModel> adversaryDynamicModelList, TransactionManager tran)
        {
            foreach (AdversaryDynamicModel adversaryDynamicModel in adversaryDynamicModelList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into officedba.AdversaryDynamic(");
                strSql.Append("CompanyCD,CustNo,Dynamic,InputDate,InputUserID)");
                strSql.Append(" values (");
                strSql.Append("@CompanyCD,@CustNo,@Dynamic,@InputDate,@InputUserID)");
                SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@CustNo", SqlDbType.VarChar,50),
					new SqlParameter("@Dynamic", SqlDbType.VarChar,200),
					new SqlParameter("@InputDate", SqlDbType.DateTime),
					new SqlParameter("@InputUserID", SqlDbType.Int,4)};
                parameters[0].Value = adversaryDynamicModel.CompanyCD;
                parameters[1].Value = adversaryDynamicModel.CustNo;
                parameters[2].Value = adversaryDynamicModel.Dynamic;
                parameters[3].Value = adversaryDynamicModel.InputDate;
                parameters[4].Value = adversaryDynamicModel.InputUserID;
                foreach (SqlParameter para in parameters)
                {
                    if (para.Value == null)
                    {
                        para.Value = DBNull.Value;
                    }
                }
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
            }
 
        }

        /// <summary>
        /// 跟新单据单据
        /// </summary>
        /// <param name="sellChanceModel">销售机会表实体</param>
        /// <param name="sellChancePushModel">销售阶段表实体</param>
        /// <returns>是否添加成功</returns>
        public static bool UpdateOrder(AdversaryInfoModel adversaryInfoModel, List<AdversaryDynamicModel> adversaryDynamicModelList)
        {
            bool isSucc = false;//是否添加成功
            string strSql = "delete from officedba.AdversaryDynamic where  CustNo=@CustNo  and CompanyCD=@CompanyCD";
            SqlParameter[] paras = { new SqlParameter("@CustNo", adversaryInfoModel.CustNo), new SqlParameter("@CompanyCD", adversaryInfoModel.CompanyCD) };
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                UpdateAdversaryInfo( adversaryInfoModel,  tran);
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);
                InsertAdversaryDynamic(adversaryDynamicModelList, tran);
                tran.Commit();
                isSucc = true;
            }
            catch(Exception ex)
            {
                tran.Rollback();
                isSucc = false;
                throw ex;
            }


            return isSucc;
        }

        /// <summary>
        /// 跟新主表信息
        /// </summary>
        /// <param name="adversaryInfoModel"></param>
        /// <param name="tran"></param>
        private static void UpdateAdversaryInfo(AdversaryInfoModel adversaryInfoModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            #region sql语句
            strSql.Append("update officedba.AdversaryInfo set ");
            strSql.Append("BigType=@BigType,");
            strSql.Append("CustType=@CustType,");
            strSql.Append("CustClass=@CustClass,");   
            strSql.Append("CustName=@CustName,");
            strSql.Append("ShortNam=@ShortNam,");
            strSql.Append("PYShort=@PYShort,");
            strSql.Append("AreaID=@AreaID,");
            strSql.Append("SetupDate=@SetupDate,");
            strSql.Append("ArtiPerson=@ArtiPerson,");
            strSql.Append("CompanyType=@CompanyType,");
            strSql.Append("StaffCount=@StaffCount,");
            strSql.Append("SetupMoney=@SetupMoney,");
            strSql.Append("SetupAddress=@SetupAddress,");
            strSql.Append("website=@website,");
            strSql.Append("CapitalScale=@CapitalScale,");
            strSql.Append("SellArea=@SellArea,");
            strSql.Append("SaleroomY=@SaleroomY,");
            strSql.Append("ProfitY=@ProfitY,");
            strSql.Append("TaxCD=@TaxCD,");
            strSql.Append("BusiNumber=@BusiNumber,");
            strSql.Append("IsTax=@IsTax,");
            strSql.Append("Address=@Address,");
            strSql.Append("Post=@Post,");
            strSql.Append("ContactName=@ContactName,");
            strSql.Append("Tel=@Tel,");
            strSql.Append("Mobile=@Mobile,");
            strSql.Append("email=@email,");
            strSql.Append("CustNote=@CustNote,");
            strSql.Append("Product=@Product,");
            strSql.Append("Market=@Market,");
            strSql.Append("SellMode=@SellMode,");
            strSql.Append("Project=@Project,");
            strSql.Append("Power=@Power,");
            strSql.Append("Advantage=@Advantage,");
            strSql.Append("disadvantage=@disadvantage,");
            strSql.Append("Policy=@Policy,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("UsedStatus=@UsedStatus,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(" where CompanyCD=@CompanyCD and  CustNo=@CustNo");
            #endregion
            #region 参数
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@BigType", SqlDbType.Char,1),
					new SqlParameter("@CustType", SqlDbType.Int,4),
					new SqlParameter("@CustClass", SqlDbType.Int,4),
					new SqlParameter("@CustNo", SqlDbType.VarChar,50),
					new SqlParameter("@CustName", SqlDbType.VarChar,100),
					new SqlParameter("@ShortNam", SqlDbType.VarChar,50),
					new SqlParameter("@PYShort", SqlDbType.VarChar,50),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@SetupDate", SqlDbType.DateTime),
					new SqlParameter("@ArtiPerson", SqlDbType.VarChar,20),
					new SqlParameter("@CompanyType", SqlDbType.Char,1),
					new SqlParameter("@StaffCount", SqlDbType.Int,4),
					new SqlParameter("@SetupMoney", SqlDbType.Decimal,9),
					new SqlParameter("@SetupAddress", SqlDbType.VarChar,100),
					new SqlParameter("@website", SqlDbType.VarChar,100),
					new SqlParameter("@CapitalScale", SqlDbType.Decimal,9),
					new SqlParameter("@SellArea", SqlDbType.VarChar,200),
					new SqlParameter("@SaleroomY", SqlDbType.Decimal,9),
					new SqlParameter("@ProfitY", SqlDbType.Decimal,9),
					new SqlParameter("@TaxCD", SqlDbType.VarChar,50),
					new SqlParameter("@BusiNumber", SqlDbType.VarChar,50),
					new SqlParameter("@IsTax", SqlDbType.Char,1),
					new SqlParameter("@Address", SqlDbType.VarChar,100),
					new SqlParameter("@Post", SqlDbType.VarChar,10),
					new SqlParameter("@ContactName", SqlDbType.VarChar,50),
					new SqlParameter("@Tel", SqlDbType.VarChar,50),
					new SqlParameter("@Mobile", SqlDbType.VarChar,50),
					new SqlParameter("@email", SqlDbType.VarChar,50),
					new SqlParameter("@CustNote", SqlDbType.VarChar,1024),
					new SqlParameter("@Product", SqlDbType.VarChar,800),
					new SqlParameter("@Market", SqlDbType.VarChar,50),
					new SqlParameter("@SellMode", SqlDbType.VarChar,50),
					new SqlParameter("@Project", SqlDbType.VarChar,1024),
					new SqlParameter("@Power", SqlDbType.VarChar,1024),
					new SqlParameter("@Advantage", SqlDbType.VarChar,1024),
					new SqlParameter("@disadvantage", SqlDbType.VarChar,1024),
					new SqlParameter("@Policy", SqlDbType.VarChar,1024),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
					new SqlParameter("@UsedStatus", SqlDbType.Char,1),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10)};
            parameters[0].Value = adversaryInfoModel.CompanyCD;
            parameters[1].Value = adversaryInfoModel.BigType;
            parameters[2].Value = adversaryInfoModel.CustType;
            parameters[3].Value = adversaryInfoModel.CustClass;
            parameters[4].Value = adversaryInfoModel.CustNo;
            parameters[5].Value = adversaryInfoModel.CustName;
            parameters[6].Value = adversaryInfoModel.ShortNam;
            parameters[7].Value = adversaryInfoModel.PYShort;
            parameters[8].Value = adversaryInfoModel.AreaID;
            parameters[9].Value = adversaryInfoModel.SetupDate;
            parameters[10].Value = adversaryInfoModel.ArtiPerson;
            parameters[11].Value = adversaryInfoModel.CompanyType;
            parameters[12].Value = adversaryInfoModel.StaffCount;
            parameters[13].Value = adversaryInfoModel.SetupMoney;
            parameters[14].Value = adversaryInfoModel.SetupAddress;
            parameters[15].Value = adversaryInfoModel.website;
            parameters[16].Value = adversaryInfoModel.CapitalScale;
            parameters[17].Value = adversaryInfoModel.SellArea;
            parameters[18].Value = adversaryInfoModel.SaleroomY;
            parameters[19].Value = adversaryInfoModel.ProfitY;
            parameters[20].Value = adversaryInfoModel.TaxCD;
            parameters[21].Value = adversaryInfoModel.BusiNumber;
            parameters[22].Value = adversaryInfoModel.IsTax;
            parameters[23].Value = adversaryInfoModel.Address;
            parameters[24].Value = adversaryInfoModel.Post;
            parameters[25].Value = adversaryInfoModel.ContactName;
            parameters[26].Value = adversaryInfoModel.Tel;
            parameters[27].Value = adversaryInfoModel.Mobile;
            parameters[28].Value = adversaryInfoModel.email;
            parameters[29].Value = adversaryInfoModel.CustNote;
            parameters[30].Value = adversaryInfoModel.Product;
            parameters[31].Value = adversaryInfoModel.Market;
            parameters[32].Value = adversaryInfoModel.SellMode;
            parameters[33].Value = adversaryInfoModel.Project;
            parameters[34].Value = adversaryInfoModel.Power;
            parameters[35].Value = adversaryInfoModel.Advantage;
            parameters[36].Value = adversaryInfoModel.disadvantage;
            parameters[37].Value = adversaryInfoModel.Policy;
            parameters[38].Value = adversaryInfoModel.Remark;
            parameters[39].Value = adversaryInfoModel.UsedStatus;
            parameters[40].Value = adversaryInfoModel.ModifiedUserID;
            foreach (SqlParameter para in parameters)
            {
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }
            #endregion
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="orderNos"></param>
        /// <returns></returns>
        public static bool DelOrder(string orderNos)
        {
            string strCompanyCD = string.Empty;//单位编号
            bool isSucc = false;
            string allOrderNo = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            TransactionManager tran = new TransactionManager();
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string[] orderNoS = null;
            orderNoS = orderNos.Split(',');

            for (int i = 0; i < orderNoS.Length; i++)
            {
                orderNoS[i] = "'" + orderNoS[i] + "'";
                sb.Append(orderNoS[i]);
            }

            allOrderNo = sb.ToString().Replace("''", "','");
            tran.BeginTransaction();
            try
            {
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.AdversaryInfo WHERE CustNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.AdversaryDynamic WHERE CustNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);

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
        public static DataTable GetOrderList(AdversaryInfoModel adversaryInfoModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
             string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            strSql = "SELECT ISNULL(c.TypeName, '') AS TypeName, ISNULL(e.EmployeeName, '') AS EmployeeName, ";
            strSql += "a.CustNo, a.CustName, a.PYShort, a.Tel, a.ContactName,a.ModifiedDate, ";
            strSql += "CONVERT(varchar(100), a.CreatDate, 23) AS CreatDate, isnull(CASE ";
            strSql += "(SELECT count(1) FROM officedba.AdversarySell AS ab ";
            strSql += "WHERE ab.CustNo = a.CustNo and ab.CompanyCD=a.CompanyCD ) WHEN 0 THEN '不存在' END, '存在') AS FromBillText , a.ID ";
            strSql += "FROM officedba.AdversaryInfo AS a LEFT OUTER JOIN ";
            strSql += "officedba.CodePublicType AS c ON a.CustType = c.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON a.Creator = e.ID where a.CompanyCD=@CompanyCD  ";                                       
                       
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (adversaryInfoModel.CustName != null)
            {
                strSql += " and a.CustName like @CustName ";
                arr.Add(new SqlParameter("@CustName", "%" + adversaryInfoModel.CustName + "%"));
            }
            if (adversaryInfoModel.CustNo != null)
            {
                strSql += " and a.CustNo like @CustNo ";
                arr.Add(new SqlParameter("@CustNo", "%" + adversaryInfoModel.CustNo + "%"));
            }
            if (adversaryInfoModel.PYShort != null)
            {
                strSql += " and a.PYShort like @PYShort ";
                arr.Add(new SqlParameter("@PYShort", "%" + adversaryInfoModel.PYShort + "%"));
            }
            if (adversaryInfoModel.CustType != null)
            {
                strSql += " and a.CustType= @CustType ";
                arr.Add(new SqlParameter("@CustType", adversaryInfoModel.CustType));
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 获取单据明细信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable GetOrderDetail(string orderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = "SELECT Dynamic FROM officedba.AdversaryDynamic ";
            strSql += "WHERE (CompanyCD = @CompanyCD) AND (CustNo = @CustNo) ";

            strSql += "ORDER BY ID ";

            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@CustNo", orderNo);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
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
            
            string strSql = "SELECT a.BigType, a.CustType, a.CustClass, a.CustNo, a.CustName, a.ShortNam, ";
            strSql += "a.PYShort, a.AreaID, CONVERT(varchar(100), a.SetupDate, 23) AS SetupDate, ";
            strSql += "a.ArtiPerson, a.CompanyType, a.StaffCount, a.SetupMoney, a.SetupAddress, ";
            strSql += "a.website, a.CapitalScale, a.SellArea, a.SaleroomY, a.ProfitY, a.TaxCD, ";
            strSql += "a.BusiNumber, a.IsTax, a.Address, a.Post, a.ContactName, a.Tel, a.Mobile, ";
            strSql += "a.email, a.CustNote, a.Product, a.Market, a.Project, a.SellMode, a.Power, ";
            strSql += "a.disadvantage, a.Advantage, a.Policy, a.Remark, a.UsedStatus, a.Creator, ";
            strSql += "CONVERT(varchar(100), a.CreatDate, 23) AS CreatDate,c.CodeName, e.EmployeeName, ";
            strSql += "CONVERT(varchar(100), a.ModifiedDate, 23) AS ModifiedDate, a.ModifiedUserID ";
            strSql += "FROM officedba.AdversaryInfo AS a LEFT OUTER JOIN ";
            strSql += "officedba.CodeCompanyType AS c ON a.CustClass = c.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON a.Creator = e.ID ";                                 

            strSql += " WHERE (a.ID = @ID ) AND (a.CompanyCD = @CompanyCD)";
            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@ID", orderID);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }

        /// <summary>
        /// 获取竞争对手类别
        /// </summary>
        /// <param name="ComPanyCD"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static DataTable GetAdversaryType()
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string sql = "select ID,CodeName,SupperID,Description from officedba.CodeCompanyType where CompanyCD=@CompanyCD and BigType='3' and UsedStatus='1' ";
            SqlParameter[] param = { new SqlParameter("@CompanyCD", strCompanyCD) };
           
            DataTable dt = SqlHelper.ExecuteSql(sql, param);
            return dt;
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
            
            string strSql = "select * from  officedba.sellmodule_report_adversaryinfo WHERE (CustNo = @CustNo) AND (CompanyCD = @CompanyCD)";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@CustNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderDetail(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = "select * from  officedba.sellmodule_report_AdversaryDynamic WHERE (CustNo = @CustNo) AND (CompanyCD = @CompanyCD)";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@CustNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }
       
    }
}

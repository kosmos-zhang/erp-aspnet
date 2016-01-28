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
    /// 类名：PurchaseRejectDBHelper
    /// 描述：采购供应商档案数据库层处理
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/26
    /// 最后修改时间：2009/06/26
    /// </summary>
    ///
    public class ProviderInfoDBHelper
    {
        #region 绑定采购供应商类别
        public static DataTable GetdrpCustType()
        {
            string sql = "select ID,TypeName from officedba.CodePublicType where typeflag =7 and typecode =1 and usedstatus=1 AND CompanyCD=@CompanyCD";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql,param );
            return data;
        }
        #endregion

        #region 绑定采购供应商分类
        public static DataTable GetdrpCustClass()
        {
            string sql = "select ID,CodeName from officedba.CodeCompanytype where BigType =2  and usedstatus=1  AND CompanyCD=@CompanyCD ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql,param );
            return data;
        }
        #endregion

        #region 绑定采购供应商优质级别
        public static DataTable GetdrpCreditGrade()
        {
            string sql = "select ID,TypeName from officedba.CodePublicType where typeflag =7 and typecode =2 and usedstatus=1  AND CompanyCD=@CompanyCD  ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql,param );
            return data;
        }
        #endregion

        #region 绑定采购供应商所在区域
        public static DataTable GetdrpAreaID()
        {
            string sql = "select ID,TypeName from officedba.CodePublicType where typeflag =4 and typecode =12 and usedstatus=1 AND CompanyCD =@CompanyCD";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql,param );
            return data;
        }
        #endregion

        #region 绑定采购供应商联络期限
        public static DataTable GetdrpLinkCycle()
        {
            string sql = "select ID,TypeName from officedba.CodePublicType where typeflag =7 and typecode =3 and usedstatus=1  AND CompanyCD=@CompanyCD ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql,param );
            return data;
        }
        #endregion

        #region 绑定采购供应商绑定币种
        public static DataTable GetdrpCurrencyType()
        {
            string sql = "select ID,CurrencyName from officedba.CurrencyTypeSetting where usedstatus=1  AND CompanyCD=@CompanyCD ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql,param );
            return data;
        }
        #endregion

        #region 绑定国家
        public static DataTable GetdrpCountryID()
        {
            string sql = "select ID,TypeName from officedba.codepublictype where typeflag =2 and typecode =3 and usedstatus=1  AND CompanyCD=@CompanyCD ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql,param );
            return data;
        }
        #endregion

        #region 绑定采购交货方式
        public static DataTable GetDrpTakeType()
        {
            string sql = "select ID,TypeName from officedba.codepublictype where typeflag =6 and typecode =7 and usedstatus=1  AND CompanyCD=@CompanyCD";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql,param );
            return data;
        }
        #endregion

        #region 绑定采购运送方式
        public static DataTable GetDrpCarryType()
        {
            string sql = "select ID,TypeName from officedba.codepublictype where typeflag =6 and typecode =8 and usedstatus=1  AND CompanyCD=@CompanyCD ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql,param );
            return data;
        }
        #endregion

        #region 绑定采购结算方式
        public static DataTable GetDrpPayType()
        {
            string sql = "select ID,TypeName from officedba.codepublictype where typeflag =4 and typecode =11 and usedstatus=1  AND CompanyCD=@CompanyCD ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql,param );
            return data;
        }
        #endregion
        #region 插入供应商档案
        public static bool InsertProviderInfo(ProviderInfoModel model, out string ID)
        {
            ArrayList listADD = new ArrayList();
            bool result = false;
            ID = "0";
            #region  采购供应商档案添加SQL语句
            StringBuilder sqlArrive = new StringBuilder();
            sqlArrive.AppendLine("INSERT INTO officedba.ProviderInfo");
            sqlArrive.AppendLine("(CompanyCD,BigType,CustType,CustClass,CustNo,CustName,CustNam,PYShort,CreditGrade,CustNote,");
            sqlArrive.AppendLine("Manager,AreaID,LinkCycle,HotIs,HotHow,MeritGrade,CompanyType,StaffCount,SetupDate,ArtiPerson,");
            sqlArrive.AppendLine("SetupMoney,SetupAddress,CapitalScale,SaleroomY,ProfitY,TaxCD,BusiNumber,IsTax,SellArea,CountryID,");
            sqlArrive.AppendLine("Province,City,SendAddress,Post,ContactName,Tel,Fax,Mobile,email,OnLine,");
            sqlArrive.AppendLine("WebSite,TakeType,CarryType,PayType,CurrencyType,OpenBank,AccountMan,AccountNum,Remark,UsedStatus,");
            sqlArrive.AppendLine("Creator,CreateDate,ModifiedDate,ModifiedUserID,AllowDefaultDays)");
            sqlArrive.AppendLine("VALUES (@CompanyCD,@BigType,@CustType,@CustClass,@CustNo,@CustName,@CustNam,@PYShort,@CreditGrade,@CustNote,");
            sqlArrive.AppendLine("@Manager,@AreaID,@LinkCycle,@HotIs,@HotHow,@MeritGrade,@CompanyType,@StaffCount,@SetupDate,@ArtiPerson,");
            sqlArrive.AppendLine("@SetupMoney,@SetupAddress,@CapitalScale,@SaleroomY,@ProfitY,@TaxCD,@BusiNumber,@IsTax,@SellArea,@CountryID,");
            sqlArrive.AppendLine("@Province,@City,@SendAddress,@Post,@ContactName,@Tel,@Fax,@Mobile,@email,@OnLine,");
            sqlArrive.AppendLine("@WebSite,@TakeType,@CarryType,@PayType,@CurrencyType,@OpenBank,@AccountMan,@AccountNum,@Remark,@UsedStatus,");
            sqlArrive.AppendLine("@Creator,@CreateDate,getdate(),@ModifiedUserID,@AllowDefaultDays)");
            sqlArrive.AppendLine("set @ID=@@IDENTITY");
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@BigType", 2));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustType", model.CustType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustClass", model.CustClass));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustNo", model.CustNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustName", model.CustName));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustNam", model.CustNam));
            comm.Parameters.Add(SqlHelper.GetParameter("@PYShort", model.PYShort));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreditGrade", model.CreditGrade));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustNote", model.CustNote));
            comm.Parameters.Add(SqlHelper.GetParameter("@Manager", model.Manager));
            comm.Parameters.Add(SqlHelper.GetParameter("@AreaID", model.AreaID));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkCycle", model.LinkCycle));
            comm.Parameters.Add(SqlHelper.GetParameter("@HotIs", model.HotIs));
            comm.Parameters.Add(SqlHelper.GetParameter("@HotHow", model.HotHow));
            comm.Parameters.Add(SqlHelper.GetParameter("@MeritGrade", model.MeritGrade));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyType", model.CompanyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@StaffCount", model.StaffCount));
            comm.Parameters.Add(SqlHelper.GetParameter("@SetupDate", model.SetupDate == null   ? SqlDateTime.Null  : SqlDateTime.Parse(model.SetupDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@ArtiPerson", model.ArtiPerson));
            comm.Parameters.Add(SqlHelper.GetParameter("@SetupMoney", model.SetupMoney));
            comm.Parameters.Add(SqlHelper.GetParameter("@SetupAddress", model.SetupAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@CapitalScale", model.CapitalScale));
            comm.Parameters.Add(SqlHelper.GetParameter("@SaleroomY", model.SaleroomY));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProfitY", model.ProfitY));
            comm.Parameters.Add(SqlHelper.GetParameter("@TaxCD", model.TaxCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@BusiNumber", model.BusiNumber));
            comm.Parameters.Add(SqlHelper.GetParameter("@IsTax", model.isTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@SellArea", model.SellArea));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountryID", model.CountryID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Province", model.Province));
            comm.Parameters.Add(SqlHelper.GetParameter("@City", model.City));
            comm.Parameters.Add(SqlHelper.GetParameter("@SendAddress", model.SendAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@Post", model.Post));
            comm.Parameters.Add(SqlHelper.GetParameter("@ContactName", model.ContactName));
            comm.Parameters.Add(SqlHelper.GetParameter("@Tel", model.Tel));
            comm.Parameters.Add(SqlHelper.GetParameter("@Fax", model.Fax));
            comm.Parameters.Add(SqlHelper.GetParameter("@Mobile", model.Mobile));
            comm.Parameters.Add(SqlHelper.GetParameter("@email", model.email));
            comm.Parameters.Add(SqlHelper.GetParameter("@OnLine", model.OnLine));
            comm.Parameters.Add(SqlHelper.GetParameter("@WebSite", model.WebSite));
            comm.Parameters.Add(SqlHelper.GetParameter("@TakeType", model.TakeType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CarryType", model.CarryType));
            comm.Parameters.Add(SqlHelper.GetParameter("@PayType", model.PayType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", model.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@OpenBank", model.OpenBank));
            comm.Parameters.Add(SqlHelper.GetParameter("@AccountMan", model.AccountMan));
            comm.Parameters.Add(SqlHelper.GetParameter("@AccountNum", model.AccountNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", model.UsedStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate == null   ? SqlDateTime.Null  : SqlDateTime.Parse(model.CreateDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@AllowDefaultDays", model.AllowDefaultDays));
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

        #region 更新供应商档案
        public static bool UpdateProviderInfo(ProviderInfoModel model)
        {
            if (model.ID <= 0)
            {
                return false;
            }
            ArrayList listADD = new ArrayList();
            bool result = false;
            #region  修改供应商档案
            StringBuilder sqlArrive = new StringBuilder();
            sqlArrive.AppendLine("Update  Officedba.ProviderInfo set CompanyCD=@CompanyCD,");
            sqlArrive.AppendLine("BigType=@BigType,CustType=@CustType,CustClass=@CustClass,CustNo=@CustNo,CustName=@CustName,");
            sqlArrive.AppendLine("CustNam=@CustNam,PYShort=@PYShort,CreditGrade=@CreditGrade,CustNote=@CustNote,Manager=@Manager,");
            sqlArrive.AppendLine("AreaID=@AreaID,LinkCycle=@LinkCycle,HotIs=@HotIs,HotHow=@HotHow,MeritGrade=@MeritGrade,");
            sqlArrive.AppendLine("CompanyType=@CompanyType,StaffCount=@StaffCount,SetupDate=@SetupDate,ArtiPerson=@ArtiPerson,SetupMoney=@SetupMoney,");
            sqlArrive.AppendLine("SetupAddress=@SetupAddress,CapitalScale=@CapitalScale,SaleroomY=@SaleroomY,ProfitY=@ProfitY,TaxCD=@TaxCD,");
            sqlArrive.AppendLine("BusiNumber=@BusiNumber,IsTax=@IsTax,SellArea=@SellArea,CountryID=@CountryID,Province=@Province,");
            sqlArrive.AppendLine("City=@City,SendAddress=@SendAddress,Post=@Post,ContactName=@ContactName,Tel=@Tel,Fax=@Fax,");
            sqlArrive.AppendLine("Mobile=@Mobile,email=@email,OnLine=@OnLine,WebSite=@WebSite,TakeType=@TakeType,");
            sqlArrive.AppendLine("CarryType=@CarryType,PayType=@PayType,CurrencyType=@CurrencyType,OpenBank=@OpenBank,AccountMan=@AccountMan,");
            sqlArrive.AppendLine("AccountNum=@AccountNum,Remark=@Remark,UsedStatus=@UsedStatus,CreateDate=@CreateDate,");
            sqlArrive.AppendLine("ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID,AllowDefaultDays=@AllowDefaultDays where CompanyCD=@CompanyCD and ID=@ID");
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@BigType", 2));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustType", model.CustType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustClass", model.CustClass));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustNo", model.CustNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustName", model.CustName));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustNam", model.CustNam));
            comm.Parameters.Add(SqlHelper.GetParameter("@PYShort", model.PYShort));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreditGrade", model.CreditGrade));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustNote", model.CustNote));
            comm.Parameters.Add(SqlHelper.GetParameter("@Manager", model.Manager));
            comm.Parameters.Add(SqlHelper.GetParameter("@AreaID", model.AreaID));
            comm.Parameters.Add(SqlHelper.GetParameter("@LinkCycle", model.LinkCycle));
            comm.Parameters.Add(SqlHelper.GetParameter("@HotIs", model.HotIs));
            comm.Parameters.Add(SqlHelper.GetParameter("@HotHow", model.HotHow));
            comm.Parameters.Add(SqlHelper.GetParameter("@MeritGrade", model.MeritGrade));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyType", model.CompanyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@StaffCount", model.StaffCount));
            comm.Parameters.Add(SqlHelper.GetParameter("@SetupDate", model.SetupDate == null? SqlDateTime.Null : SqlDateTime.Parse(model.SetupDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@ArtiPerson", model.ArtiPerson));
            comm.Parameters.Add(SqlHelper.GetParameter("@SetupMoney", model.SetupMoney));
            comm.Parameters.Add(SqlHelper.GetParameter("@SetupAddress", model.SetupAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@CapitalScale", model.CapitalScale));
            comm.Parameters.Add(SqlHelper.GetParameter("@SaleroomY", model.SaleroomY));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProfitY", model.ProfitY));
            comm.Parameters.Add(SqlHelper.GetParameter("@TaxCD", model.TaxCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@BusiNumber", model.BusiNumber));
            comm.Parameters.Add(SqlHelper.GetParameter("@IsTax", model.isTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@SellArea", model.SellArea));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountryID", model.CountryID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Province", model.Province));
            comm.Parameters.Add(SqlHelper.GetParameter("@City", model.City));
            comm.Parameters.Add(SqlHelper.GetParameter("@SendAddress", model.SendAddress));
            comm.Parameters.Add(SqlHelper.GetParameter("@Post", model.Post));
            comm.Parameters.Add(SqlHelper.GetParameter("@ContactName", model.ContactName));
            comm.Parameters.Add(SqlHelper.GetParameter("@Tel", model.Tel));
            comm.Parameters.Add(SqlHelper.GetParameter("@Fax", model.Fax));
            comm.Parameters.Add(SqlHelper.GetParameter("@Mobile", model.Mobile));
            comm.Parameters.Add(SqlHelper.GetParameter("@email", model.email));
            comm.Parameters.Add(SqlHelper.GetParameter("@OnLine", model.OnLine));
            comm.Parameters.Add(SqlHelper.GetParameter("@WebSite", model.WebSite));
            comm.Parameters.Add(SqlHelper.GetParameter("@TakeType", model.TakeType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CarryType", model.CarryType));
            comm.Parameters.Add(SqlHelper.GetParameter("@PayType", model.PayType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", model.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameter("@OpenBank", model.OpenBank));
            comm.Parameters.Add(SqlHelper.GetParameter("@AccountMan", model.AccountMan));
            comm.Parameters.Add(SqlHelper.GetParameter("@AccountNum", model.AccountNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", model.UsedStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate == null? SqlDateTime.Null  : SqlDateTime.Parse(model.CreateDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@AllowDefaultDays", model.AllowDefaultDays));
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

        #region 查询采购供应商档案列表所需数据
        public static DataTable SelectProviderInfoList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string CustNo, string CustName, string CustNam, string PYShort, string CustType, string CustClass, string AreaID, string CreditGrade, string Manager, string StartCreateDate, string EndCreateDate)
        { 
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.CustNo ,isnull(A.CustName,'') AS CustName ,isnull(A.CustNam,'') AS CustNam  ");
            sql.AppendLine("   ,A.CustType , B.TypeName AS CustTypeName,A.PYShort,A.CreditGrade,isnull(C.TypeName,'') AS  CreditGradeName");
            sql.AppendLine("   ,isnull(A.Manager,0) AS Manager,isnull(D.EmployeeName,'') AS ManagerName,A.AreaID,isnull(E.TypeName,'') AS AreaName ");
            sql.AppendLine("   ,A.Creator,isnull(F.EmployeeName,'') AS CreatorName,Convert(varchar(100),A.CreateDate,23) AS CreateDate,A.CustClass,isnull(G.CodeName,'') AS CustClassName,isnull( A.ModifiedDate,'') AS ModifiedDate");
            sql.AppendLine(" FROM officedba.ProviderInfo AS A                                                                     ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS B ON A.CompanyCD = B.CompanyCD AND B.TypeFlag=7 AND B.TypeCode=1 AND A.CustType=B.ID");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS C ON A.CompanyCD = C.CompanyCD AND C.TypeFlag=7 AND C.TypeCode=2 AND A.CreditGrade=C.ID");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON A.CompanyCD = D.CompanyCD AND A.Manager=D.ID                   ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS E ON A.CompanyCD = E.CompanyCD AND E.TypeFlag=4 AND E.TypeCode=12 AND A.AreaID=E.ID");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS F ON A.CompanyCD = F.CompanyCD AND A.Creator=F.ID                   ");
            sql.AppendLine("LEFT JOIN officedba.CodeCompanytype AS G ON A.CompanyCD = G.CompanyCD AND A.CustClass=G.ID                   ");
            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = @CompanyCD ");
            if (CustNo != "" && CustNo != null)
            {
                sql.AppendLine(" AND A.CustNo like'%"+ @CustNo +"%'  ");
            }
            if (CustName != null && CustName != "")
            {
                sql.AppendLine(" AND A.CustName like'%" + @CustName + "%'  ");
            }
            if (CustNam != null && CustNam != "")
            {
                sql.AppendLine(" AND A.CustNam  like '%" + @CustNam+ "%'  ");
            }
            if (PYShort != null && PYShort != "")
            {
                sql.AppendLine(" AND A.PYShort  like '%" + @PYShort + "%'  ");
            }
            if (CustType != "" && CustType != null)
            {
                sql.AppendLine(" AND A.CustType=@CustType ");
            }
            if (CustClass != null && CustClass != "")
            {
                sql.AppendLine(" AND A.CustClass =@CustClass");
            }
            if (AreaID != null && AreaID != "")
            {
                sql.AppendLine(" AND A.AreaID =@AreaID");
            }
            if (CreditGrade != "" && CreditGrade != "")
            {
                sql.AppendLine(" AND A.CreditGrade=@CreditGrade ");
            }
            if (Manager != null && Manager != "")
            {
                sql.AppendLine(" AND A.Manager  = @Manager ");
            }
            if (StartCreateDate != null && StartCreateDate != "")
            {
                sql.AppendLine(" AND A.CreateDate >= @StartCreateDate");
            }
            if (EndCreateDate != null && EndCreateDate != "")
            {
                sql.AppendLine(" AND A.CreateDate <= @EndCreateDate");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustNo", CustNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", CustName));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustNam", CustNam));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", PYShort));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustType", CustType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustClass", CustClass));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AreaID", AreaID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreditGrade", CreditGrade));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manager ", Manager));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartCreateDate ", StartCreateDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndCreateDate ", EndCreateDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion

        #region 先判断采购订单中有没有符合条件的供应商相关信息
        public static bool isValue(int ID)
        {
            string sql = "select isnull(Count(1),0)  from  officedba.PurchaseOrder where CompanyCD='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and ProviderID = " + ID + " AND BillStatus<>1";
            DataTable data = SqlHelper.ExecuteSql(sql);
            if (Convert.ToInt32(data.Rows[0][0]) == 0)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region 先判断采购订单中有没有符合条件的供应商相关信息
        public static bool isValues(int ID)
        {
            string sql = "select isnull(Count(1),0)  from  officedba.PurchaseReject where CompanyCD=@CompanyCD  and ProviderID = @ProviderID  AND BillStatus<>1";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@ProviderID",  ID);
            DataTable data1 = SqlHelper.ExecuteSql(sql,param );
            if (Convert.ToInt32(data1.Rows[0][0]) == 0)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region 查找加载单个供应商档案订单中有符合条件数据
        public static DataTable SelectProviderInfo(int ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ID ,A.AllowDefaultDays,A.CustType,isnull(N.TypeName,'') AS CustTypeName ,isnull(A.CustClass,0) AS CustClass,isnull(O.CodeName,'') AS CustClassName ,isnull(F.CodeName,'') AS CustClassName ,A.CustNo ,A.CustName,A.CustNam,A.PYShort ,A.CreditGrade,isnull(G.TypeName,'') AS CreditGradeName,A.CustNote  ");
            sql.AppendLine("     ,A.Manager,isnull(B.EmployeeName,'') AS ManagerName, A.AreaID,isnull(H.TypeName,'') AS AreaName ,A.LinkCycle,isnull(P.TypeName,'') AS LinkCycleName ,A.HotIs,case A.HotIs when '0' then '' when '1' then '是' when '2' then '否' end AS HotIsName,A.HotHow,case A.HotHow when '0' then ''  when '1' then '低热' when '2' then '中热' when '3' then '高热'  end AS HotHowName,A.MeritGrade ");
            sql.AppendLine("     ,case A.MeritGrade when '0' then ''  when '1' then '高' when '2' then '中' when '3' then '低'  end AS MeritGradeName,A.CompanyType,case A.CompanyType  when '0' then '' when '1' then '事业'  when '2' then '企业'  when '3' then '社团' when '4' then '自然人' when '5' then '其他'   end AS CompanyTypeName, isnull(A.StaffCount,0) AS StaffCount,isnull(Convert(varchar(100),A.SetupDate,23),'') AS SetupDate  ");
            sql.AppendLine("     ,A.ArtiPerson, isnull(A.SetupMoney,0) AS SetupMoney,A.SetupAddress,isnull(A.CapitalScale,0) AS CapitalScale ,isnull(A.ModifiedUserID,'') AS ModifiedUserID ");
            sql.AppendLine("     ,isnull(A.SaleroomY,0) AS SaleroomY, isnull(A.ProfitY,0) AS ProfitY ,A.TaxCD,A.BusiNumber,A.IsTax,case A.IsTax  when '2' then ''  when '0' then '否' when '1' then '是' end AS IsTaxName");
            sql.AppendLine("     ,A.SellArea, A.CountryID,isnull(J.TypeName,'') AS CountryName, A.Province,A.City,A.SendAddress,A.Post,A.ContactName,A.Tel,A.Fax,A.Mobile,A.email ");
            sql.AppendLine("     ,A.OnLine, A.WebSite,A.TakeType,isnull(K.TypeName,'') AS TakeTypeName, A.CarryType,isnull(L.TypeName,'') AS CarryTypeName,A.PayType,isnull(M.TypeName,'') AS PayTypeName,A.CurrencyType,isnull(I.CurrencyName,'') AS CurrencyTypeName, A.OpenBank,A.AccountMan,A.AccountNum ");
            sql.AppendLine("     ,A.Remark, A.UsedStatus,case A.UsedStatus when '0' then '停用' when '1' then '启用' end AS UsedStatusName,A.Creator,isnull(C.EmployeeName,'') AS CreatorName, Convert(varchar(100),A.CreateDate,23) AS CreateDate,CONVERT(varchar(100),A.ModifiedDate,23) AS  ModifiedDate");
            sql.AppendLine("     ,Convert(numeric(12,2),isnull(sum(E.TotalPrice*D.Rate),0)) AS PTotalPrice,Convert(numeric(12,2),isnull(sum(E.YAccounts*D.Rate),0)) AS PYAccounts,Convert(numeric(12,2),isnull(sum(E.NAccounts*D.Rate),0)) AS PNAccounts");
            sql.AppendLine("     ,Convert(numeric(12,2),(select isnull(sum(d.CountTotal),0) from officedba.PurchaseReject as d,officedba.ProviderInfo AS a");
            sql.AppendLine("      where a.companyCD=d.CompanyCD and a.ID=d.ProviderID  AND d.BillStatus<>1 AND a.ID = @ID )) as TCountTotal");
            sql.AppendLine("     ,Convert(numeric(12,2),(select isnull(sum(e.CountTotal),0) from officedba.PurchaseOrder as e,officedba.ProviderInfo AS a");
            sql.AppendLine("     where a.companyCD=e.CompanyCD and a.ID=e.ProviderID  AND e.BillStatus<>1 AND a.ID = @ID )) as DCountTotal");
            sql.AppendLine("     ,Convert(numeric(12,2),(select isnull(sum(f.YAccounts),0) from officedba.BlendingDetails as f,officedba.ProviderInfo AS a,officedba.PurchaseReject as  g");
            sql.AppendLine("     where a.companyCD=f.CompanyCD and f.companyCD=g.CompanyCD and a.ID=10  AND f.BillCD=g.RejectNo and f.BillingType=5 AND a.ID = @ID )) as TYAccounts");
            

            sql.AppendLine(" FROM officedba.ProviderInfo AS A                                                                            ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS B ON A.CompanyCD = B.CompanyCD AND A.Manager=B.ID                        ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Creator=C.ID                        ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseOrder AS D ON A.CompanyCD = D.CompanyCD AND A.ID=D.ProviderID                    ");
            sql.AppendLine("LEFT JOIN officedba.BlendingDetails AS E ON D.CompanyCD = E.CompanyCD AND D.OrderNo=E.BillCD AND E.BillingType=2");
            sql.AppendLine("LEFT JOIN officedba.CodeCompanyType AS F ON A.CompanyCD = F.CompanyCD AND A.CustClass=F.ID                   ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS G ON A.CompanyCD = G.CompanyCD AND A.CreditGrade=G.ID                  ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS H ON A.CompanyCD = H.CompanyCD AND A.AreaID=H.ID                       ");
            sql.AppendLine("LEFT JOIN officedba.CurrencyTypeSetting AS I ON A.CompanyCD = I.CompanyCD AND A.CurrencyType=I.ID            ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS J ON A.CompanyCD = J.CompanyCD AND A.CountryID=J.ID                    ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS K ON A.CompanyCD = K.CompanyCD AND A.TakeType=K.ID                     ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS L ON A.CompanyCD = L.CompanyCD AND A.CarryType=L.ID                    ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS M ON A.CompanyCD = M.CompanyCD AND A.PayType=M.ID                      ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS N ON A.CompanyCD = N.CompanyCD AND A.CustType=N.ID                     ");
            sql.AppendLine("LEFT JOIN officedba.CodeCompanytype AS O ON A.CompanyCD = O.CompanyCD AND A.CustClass=O.ID                   ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS P ON A.CompanyCD = P.CompanyCD AND A.LinkCycle=P.ID                    ");
            

            sql.AppendLine("WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = @CompanyCD ");
            sql.AppendLine(" AND A.ID = @ID");
            sql.AppendLine(" GROUP BY A.ID ,A.AllowDefaultDays,A.CustType,N.TypeName ,A.CustClass,O.CodeName,F.CodeName ,A.CustNo ,A.CustName,A.CustNam,A.PYShort ,A.CreditGrade,G.TypeName,A.CustNote");
            sql.AppendLine(" ,A.Manager,B.EmployeeName,A.AreaID,H.TypeName,A.LinkCycle,P.TypeName,A.HotIs,A.HotHow,A.MeritGrade ");
            sql.AppendLine(" ,A.CompanyType,A.StaffCount,A.SetupDate,A.ArtiPerson,A.SetupMoney,A.SetupAddress,A.CapitalScale,A.ModifiedUserID ");
            sql.AppendLine(" ,A.SaleroomY,A.ProfitY,A.TaxCD,A.BusiNumber,A.IsTax,A.SellArea, A.CountryID,J.TypeName,A.Province,A.City ");
            sql.AppendLine(" ,A.SendAddress,A.Post,A.ContactName,A.Tel,A.Fax,A.Mobile,A.email,A.OnLine, A.WebSite,A.TakeType,K.TypeName,A.CarryType,L.TypeName ");
            sql.AppendLine(" ,A.PayType,M.TypeName,A.CurrencyType,I.CurrencyName,A.OpenBank,A.AccountMan,A.AccountNum,A.Remark, A.UsedStatus,A.Creator,C.EmployeeName,A.CreateDate,A.ModifiedDate ");
            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@ID", ID);

            return SqlHelper.ExecuteSql(sql.ToString(),param );
        }
        #endregion

        #region 查找加载单个供应商档案订单中没有符合条件数据
        public static DataTable SelectProviderInfo2(int ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.CustType ,isnull(A.CustClass,0) AS CustClass ,isnull(F.CodeName,'') AS CustClassName ,A.CustNo ,A.CustName,A.CustNam,A.PYShort ,A.CreditGrade,isnull(G.TypeName,'--请选择--') AS CreditGradeName,A.CustNote  ");
            sql.AppendLine("     ,A.Manager,isnull(B.EmployeeName,'') AS ManagerName, A.AreaID,isnull(H.TypeName,'--请选择--') AS AreaName ,A.LinkCycle,A.HotIs,A.HotHow,A.MeritGrade ");
            sql.AppendLine("     ,A.CompanyType, isnull(A.StaffCount,0) AS StaffCount,isnull(Convert(varchar(100),A.SetupDate,23),'') AS SetupDate  ");
            sql.AppendLine("     ,A.ArtiPerson, isnull(A.SetupMoney,0) AS SetupMoney,A.SetupAddress,isnull(A.CapitalScale,0) AS CapitalScale");
            sql.AppendLine("     ,isnull(A.SaleroomY,0) AS SaleroomY, isnull(A.ProfitY,0) AS ProfitY ,A.TaxCD,A.BusiNumber,A.IsTax  ");
            sql.AppendLine("     ,A.SellArea, A.CountryID,isnull(J.TypeName,'--请选择--') AS CountryName, A.Province,A.City,A.SendAddress,A.Post,A.ContactName,A.Tel,A.Fax,A.Mobile,A.email ");
            sql.AppendLine("     ,A.OnLine, A.WebSite,A.TakeType,isnull(K.TypeName,'--请选择--') AS TakeTypeName, A.CarryType,isnull(L.TypeName,'--请选择--') AS CarryTypeName,A.PayType,isnull(M.TypeName,'--请选择--') AS PayTypeName,A.CurrencyType,isnull(I.CurrencyName,'--请选择--') AS CurrencyTypeName, A.OpenBank,A.AccountMan,A.AccountNum ");
            sql.AppendLine("     ,A.Remark, A.UsedStatus,A.Creator,isnull(C.EmployeeName,'') AS CreatorName, Convert(varchar(100),A.CreateDate,23) AS CreateDate,CONVERT(varchar(100),A.ModifiedDate,23) AS  ModifiedDate");
            sql.AppendLine("     ,isnull(sum(D.RealTotal),0) AS PTotalPrice,isnull(sum(E.YAccounts),0) AS PYAccounts,isnull(sum(D.RealTotal),0)-isnull(sum(E.YAccounts),0) AS PNAccounts");


            sql.AppendLine(" FROM officedba.ProviderInfo AS A                                                                            ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS B ON A.CompanyCD = B.CompanyCD AND A.Manager=B.ID                        ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Creator=C.ID                        ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseOrder AS D ON A.CompanyCD = D.CompanyCD AND A.ID=D.ProviderID                    ");
            sql.AppendLine("LEFT JOIN officedba.Billing AS E ON D.CompanyCD = E.CompanyCD AND D.OrderNo=E.BillCD                         ");
            sql.AppendLine("LEFT JOIN officedba.CodeCompanyType AS F ON A.CompanyCD = F.CompanyCD AND A.CustClass=F.ID                   ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS G ON A.CompanyCD = G.CompanyCD AND A.CreditGrade=G.ID                  ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS H ON A.CompanyCD = H.CompanyCD AND A.AreaID=H.ID                       ");
            sql.AppendLine("LEFT JOIN officedba.CurrencyTypeSetting AS I ON A.CompanyCD = I.CompanyCD AND A.CurrencyType=I.ID            ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS J ON A.CompanyCD = J.CompanyCD AND A.CountryID=J.ID                    ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS K ON A.CompanyCD = K.CompanyCD AND A.TakeType=K.ID                     ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS L ON A.CompanyCD = L.CompanyCD AND A.CarryType=L.ID                    ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS M ON A.CompanyCD = M.CompanyCD AND A.PayType=M.ID                      ");

            sql.AppendLine("WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = @CompanyCD ");
            sql.AppendLine(" AND A.ID = @ID "); 
            sql.AppendLine(" GROUP BY A.ID ,A.CustType ,A.CustClass,F.CodeName ,A.CustNo ,A.CustName,A.CustNam,A.PYShort ,A.CreditGrade,G.TypeName,A.CustNote");
            sql.AppendLine(" ,A.Manager,B.EmployeeName,A.AreaID,H.TypeName,A.LinkCycle,A.HotIs,A.HotHow,A.MeritGrade ");
            sql.AppendLine(" ,A.CompanyType,A.StaffCount,A.SetupDate,A.ArtiPerson,A.SetupMoney,A.SetupAddress,A.CapitalScale ");
            sql.AppendLine(" ,A.SaleroomY,A.ProfitY,A.TaxCD,A.BusiNumber,A.IsTax,A.SellArea, A.CountryID,J.TypeName,A.Province,A.City ");
            sql.AppendLine(" ,A.SendAddress,A.Post,A.ContactName,A.Tel,A.Fax,A.Mobile,A.email,A.OnLine, A.WebSite,A.TakeType,K.TypeName,A.CarryType,L.TypeName ");
            sql.AppendLine(" ,A.PayType,M.TypeName,A.CurrencyType,I.CurrencyName,A.OpenBank,A.AccountMan,A.AccountNum,A.Remark, A.UsedStatus,A.Creator,C.EmployeeName,A.CreateDate,A.ModifiedDate");
            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@ID", ID);

            return SqlHelper.ExecuteSql(sql.ToString(),param );
        }
        #endregion

        #region 删除供应商
        #region 删除采购供应商信息
        //public static void DeleteProviderInfo(string CustNo,string CompanyCD, ref string[] sql, int i)
          public static SqlCommand DeleteProviderInfo(string CustNo,string CompanyCD)
        {
            #region SQL文
            SqlCommand comm = new SqlCommand();
            string strSql = "delete officedba.ProviderInfo where CompanyCD=@CompanyCD  and CustNo=@CustNo";
            comm.CommandText = strSql;
             comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD ));
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustNo",  CustNo ));
            return comm;
            #endregion

            //sql[i] = strSql;
        }
        #endregion

        #region 删除采购供应商联系人
          public static SqlCommand DeleteProviderLinkMan(string CustNo, string CompanyCD)
          {
              SqlCommand comm = new SqlCommand();
              string strSql = "delete officedba.ProviderLinkMan where CompanyCD=@CompanyCD  and CustNo=@CustNo ";
              comm.CommandText = strSql;
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustNo", CustNo));
            return comm;
            //sql[i] = strSql;
        }
        #endregion

        #region 删除采购供应商产品
          public static SqlCommand DeleteProviderProduct(string CustNo, string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            string strSql = "delete officedba.ProviderProduct where CompanyCD=@CompanyCD and CustNo=@CustNo ";
            comm.CommandText = strSql;
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustNo", CustNo));
            return comm;
            //sql[i] = strSql;
        }
        #endregion
        #endregion

        #region 供应商分类
        /// <summary>
        /// 获取供应商分类
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProviderClass()
        {
            string sql = "select ID,CodeName,SupperID from officedba.CodeCompanyType where UsedStatus='1' and BigType = '2' and CompanyCD=@CompanyCD";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD",((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD );
            DataTable dt = SqlHelper.ExecuteSql(sql, param);
            return dt;
        }
        #endregion

        #region  
        public static DataTable GetProviderInfo(ProviderInfoModel ProviderInfoM, int pageIndex, int pageCount, string OrderBy, out int totalCount)
        {
            SqlParameter[] parms = new SqlParameter[7];
            int i = 0;
            parms[i++] = new SqlParameter("@CompanyCD", ProviderInfoM.CompanyCD);
            parms[i++] = new SqlParameter("@ProviderNo", ProviderInfoM.CustNo);
            parms[i++] = new SqlParameter("@ProviderName", ProviderInfoM.CustName);
            parms[i++] = new SqlParameter("@pageIndex", pageIndex);
            parms[i++] = new SqlParameter("@pageCount", pageCount);
            parms[i++] = new SqlParameter("@OrderBy", OrderBy);
            parms[i++] = MakeParam("@totalCount", SqlDbType.Int, 0, ParameterDirection.Output, null);

            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[ProcGetProviderInfo]", parms);
            totalCount = int.Parse(parms[6].Value.ToString());
            return dt;
        }
        /// <summary>
        /// Make stored procedure param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <param name="Direction">Parm direction.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>
        public static SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size,
            ParameterDirection Direction, object Value)
        {
            SqlParameter param;

            if (Size > 0)
                param = new SqlParameter(ParamName, DbType, Size);
            else
                param = new SqlParameter(ParamName, DbType);

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;

            return param;
        }
        #endregion

        #region 供应商控件，附带交货、运送、结算
        /// <summary>
        /// 获取供应商及交货、运送、结算 
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetProviderSelect(ProviderInfoModel ProviderInfoM)
        {
            SqlCommand comm = new SqlCommand();
            string sql = "select a.ID as ProviderID ,a.CustNo as ProviderNo,a.CustName as ProviderName,a.TakeType as TakeType,isnull(b.TypeName,'') as TakeTypeName,a.CarryType as CarryType,isnull(c.TypeName,'') as CarryTypeName,a.PayType as PayType,isnull(d.TypeName,'') as PayTypeName from officedba.ProviderInfo as a  left join officedba.codepublictype as b on b.id = a.TakeType and b.CompanyCD=a.CompanyCD left join officedba.codepublictype as c on c.id = a.CarryType left join officedba.codepublictype as d on d.id = a.PayType where a.UsedStatus =1 AND a.CompanyCD=@CompanyCD1 ";

            if (!string.IsNullOrEmpty(ProviderInfoM.CustNo))
            { 
                sql += " and a.CustNo like @ProviderNo"; 
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderNo", "%" + ProviderInfoM.CustNo + "%"));
            }
            if (!string.IsNullOrEmpty(ProviderInfoM.CustName))
            { 
                sql += " and a.CustName like @ProviderName";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderName", "%" + ProviderInfoM.CustName + "%"));
            }
            if (!string.IsNullOrEmpty(ProviderInfoM.CompanyCD))
            { 
                sql += " and a.CompanyCD= @CompanyCD";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ProviderInfoM.CompanyCD));
            }
            if (!string.IsNullOrEmpty(ProviderInfoM.CompanyCD))
            { 
                sql += " and a.UsedStatus=@UsedStatus";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", ProviderInfoM.UsedStatus));
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD1", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.CommandText = sql;
            return SqlHelper.ExecuteSearch(comm); 
        }
        #endregion

        #region 获取当前企业的供应商信息
        /// <summary>
        /// 获取当前企业的供应商信息 Add by jiangym
        /// </summary>
        /// <param name="CompanyCD">企业编码</param>
        /// <returns>DataTable</returns>
        public static DataTable GetProviderInfo(string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select CustName,ID,CompanyCD,BigType ");
            sql.AppendLine("from officedba.ProviderInfo where");
            sql.AppendLine("CompanyCD=@CompanyCD and UsedStatus='1'");
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            return SqlHelper.ExecuteSql(sql.ToString(), parms);
        }
        #endregion

        #region 采购报表供应商统计表
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseProviderCount(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProviderID, string StartOrderDate, string EndOrderDate)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ProviderID,isnull(B.CustName,'') AS OrderNo");
            sql.AppendLine(" ,Convert(numeric(12,2),isnull(sum(A.CountTotal),0)) AS TotalPrice,Convert(numeric(12,2),isnull(sum(A.RealTotal*A.Rate),0)) AS TotalTax ");

            sql.AppendLine(" FROM officedba.PurchaseOrder AS A                                                                     ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProviderID=B.ID                ");

            sql.AppendLine(" WHERE 1=1 AND A.BillStatus <>1");
            sql.AppendLine("AND A.CompanyCD =@CompanyCD ");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND A.ProviderID=@ProviderID ");
            }
            if (StartOrderDate != "" && StartOrderDate != null)
            {
                sql.AppendLine(" AND A.OrderDate >=@StartOrderDate ");
            }
            if (EndOrderDate != "" && EndOrderDate != null)
            {
                sql.AppendLine(" AND A.OrderDate <@EndOrderDate ");
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartOrderDate", StartOrderDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndOrderDate", Convert.ToDateTime(EndOrderDate).AddDays(1).ToString("yyyy-MM-dd")));
            sql.AppendLine("group by A.ProviderID,B.CustName");
            

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion

        #region 采购报表供应商统计表打印
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseProviderCountPrint(string ProviderID, string StartOrderDate, string EndOrderDate, string CompanyCD, string orderBy)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct A.ProviderID,isnull(B.CustName,'') AS OrderNo");
            sql.AppendLine(" ,Convert(numeric(12,2),isnull(sum(A.CountTotal),0)) AS TotalPrice,Convert(numeric(12,2),isnull(sum(A.RealTotal*A.Rate),0)) AS TotalTax ");

            sql.AppendLine(" FROM officedba.PurchaseOrder AS A                                                                     ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProviderID=B.ID                ");

            sql.AppendLine(" WHERE 1=1 AND A.BillStatus <>1");
            sql.AppendLine("AND A.CompanyCD = @CompanyCD ");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND A.ProviderID=@ProviderID ");
            }
            if (StartOrderDate != "" && StartOrderDate != null)
            {
                sql.AppendLine(" AND A.OrderDate >=@StartOrderDate ");
            }
            if (EndOrderDate != "" && EndOrderDate != null)
            {
                sql.AppendLine(" AND A.OrderDate <@EndOrderDate ");
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@orderBy", orderBy));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartOrderDate", StartOrderDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndOrderDate", Convert.ToDateTime(EndOrderDate).AddDays(1).ToString("yyyy-MM-dd")));
            sql.AppendLine("group by A.ProviderID,B.CustName");

            sql.AppendLine(" ORDER BY  ProviderID DESC");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm); 
        }
        #endregion

        #region 采购报表供应商应付款查询
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseProviderPayment(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProviderID, string ProviderName, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            #region
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.BillNo,A.ProviderID,A.Purchaser,Convert(varchar(23),A.ConfirmDate,23) AS ConfirmDate,A.RealTotal,A.TotalPrice,A.YAccounts,A.NAccounts,A.Flag,B.CustNo AS ProviderNo,B.CustName AS ProviderName,C.EmployeeName AS PurchaserName FROM                                                                                                                            ");
            sql.AppendLine("(                                                                                                                                                                                                     ");
            sql.AppendLine("	SELECT A.OrderNo AS BillNo, A.ProviderID, A.Purchaser, A.OrderDate AS ConfirmDate, A.RealTotal*A.Rate AS RealTotal,A.RealTotal*A.Rate*A.isOpenBill  AS TotalPrice, ISNULL(B.YAccounts, 0)*A.Rate AS YAccounts, (A.RealTotal - ISNULL(B.YAccounts, 0))*A.Rate AS NAccounts,'采购订单' AS Flag        ");
            sql.AppendLine("	FROM   officedba.PurchaseOrder AS A                                                                                                                                                                 ");
            sql.AppendLine("	LEFT OUTER JOIN officedba.BlendingDetails AS B ON B.BillingType = '2' AND A.ID = B.SourceID                                                                                                        ");
            sql.AppendLine("	WHERE A.CompanyCD=@CompanyCD AND A.BillStatus <>'1'                                                                                                                                                    ");
            sql.AppendLine("	UNION                                                                                                                                                                                               ");
            sql.AppendLine("	SELECT A.RejectNo AS BillNo, A.ProviderID, A.Purchaser, A.RejectDate AS ConfirmDate, A.RealTotal*A.Rate,A.RealTotal*A.Rate*A.isOpenBill  AS TotalPrice, ISNULL(B.YAccounts, 0)*A.Rate AS YAccounts, (A.RealTotal - ISNULL(B.YAccounts, 0))*A.Rate AS NAccounts,'采购退货单' AS Flag     ");
            sql.AppendLine("	FROM  officedba.PurchaseReject AS A                                                                                                                                                                 ");
            sql.AppendLine("	LEFT OUTER  JOIN  officedba.BlendingDetails AS B ON B.BillingType = '5' AND  A.ID = B.SourceID                                                                                                     ");
            sql.AppendLine("	WHERE A.CompanyCD=@CompanyCD AND A.BillStatus <>'1'                                                                                                                                                    ");
            sql.AppendLine(") AS A                                                                                                                                                                                                ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.ProviderID=B.ID                                                                                                                                            ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.Purchaser=C.ID                                                                                                                                             ");
            sql.AppendLine(" WHERE 1=1 ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND A.ProviderID= @ProviderID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND B.CustName like @ProviderName ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderName", "%" + ProviderName + "%"));
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND A.ConfirmDate >= @StartDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartConfirmDate));
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND A.ConfirmDate < @EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndConfirmDate));
            }

            comm.CommandText = sql.ToString();

            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);

            #endregion
        }
        #endregion

        #region 采购报表供应商应付款查询打印
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseProviderPaymentPrint(string ProviderID, string ProviderName, string StartConfirmDate, string EndConfirmDate, string CompanyCD, string orderBy)
        {
            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.BillNo,A.ProviderID,A.Purchaser,Convert(varchar(23),A.ConfirmDate,23) AS ConfirmDate,Convert(decimal(22," + jingdu + "),isnull(A.RealTotal,0)) as RealTotal,Convert(decimal(22," + jingdu + "),isnull(A.TotalPrice,0)) as TotalPrice ,Convert(decimal(22," + jingdu + "),isnull(A.YAccounts,0)) as YAccounts ,Convert(decimal(22," + jingdu + "),isnull(A.NAccounts,0)) as NAccounts,A.Flag,B.CustNo AS ProviderNo,B.CustName AS ProviderName,C.EmployeeName AS PurchaserName  FROM                                                                                                                            ");
            sql.AppendLine("(                                                                                                                                                                                                     ");
            sql.AppendLine("	SELECT A.OrderNo AS BillNo, A.ProviderID, A.Purchaser, A.OrderDate AS ConfirmDate, A.RealTotal*A.Rate AS RealTotal,ISNULL(B.TotalPrice,0)*A.Rate AS TotalPrice, ISNULL(B.YAccounts, 0)*A.Rate AS YAccounts, (A.RealTotal - ISNULL(B.YAccounts, 0))*A.Rate AS NAccounts,'采购订单' AS Flag        ");
            sql.AppendLine("	FROM   officedba.PurchaseOrder AS A                                                                                                                                                                 ");
            sql.AppendLine("	LEFT OUTER JOIN officedba.BlendingDetails AS B ON B.BillingType = '2' AND A.ID = B.SourceID                                                                                                        ");
            sql.AppendLine("	WHERE A.CompanyCD=@CompanyCD AND A.BillStatus <>'1'                                                                                                                                                    ");
            sql.AppendLine("	UNION                                                                                                                                                                                               ");
            sql.AppendLine("	SELECT A.RejectNo AS BillNo, A.ProviderID, A.Purchaser, A.RejectDate AS ConfirmDate, A.RealTotal*A.Rate,ISNULL(B.TotalPrice,0)*A.Rate AS TotalPrice, ISNULL(B.YAccounts, 0)*A.Rate AS YAccounts, (A.RealTotal - ISNULL(B.YAccounts, 0))*A.Rate AS NAccounts,'采购退货单' AS Flag     ");
            sql.AppendLine("	FROM  officedba.PurchaseReject AS A                                                                                                                                                                 ");
            sql.AppendLine("	LEFT OUTER  JOIN  officedba.BlendingDetails AS B ON B.BillingType = '5' AND  A.ID = B.SourceID                                                                                                     ");
            sql.AppendLine("	WHERE A.CompanyCD=@CompanyCD AND A.BillStatus <>'1'                                                                                                                                                    ");
            sql.AppendLine(") AS A                                                                                                                                                                                                ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.ProviderID=B.ID                                                                                                                                            ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.Purchaser=C.ID                                                                                                                                             ");
            sql.AppendLine(" WHERE 1=1 ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND A.ProviderID= @ProviderID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND B.CustName like @ProviderName ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderName", "%" + ProviderName + "%"));
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND A.ConfirmDate >= @StartDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartConfirmDate));
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND A.ConfirmDate < @EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndConfirmDate));
            }
            sql.AppendLine(" ORDER BY "+orderBy+"");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 采购报表供应商台帐查询
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseProviderAccount(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProviderID, string ProviderName, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT  A.ID,A.CompanyCD, A.AskNo AS TheyDelegate ,'采购询价单' AS SignAddr,isnull(A.ProviderID,0) AS  ProviderID, isnull(A.AskUserID,0) AS AskUserID ");
            sql.AppendLine(" ,isnull(B.CustNo,'') AS ContractNo,isnull(B.CustName,'') AS Title,isnull(C.EmployeeName,'') AS Note           ");
            sql.AppendLine(" ,A.AskDate AS SignDate,Convert(numeric(20,2),isnull(A.RealTotal,0)*isnull(A.Rate,0)) AS TotalPrice "); 

            sql.AppendLine(" FROM officedba.PurchaseAskPrice AS A         ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProviderID=B.ID          ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.AskUserID=C.ID           ");

            sql.AppendLine(" WHERE 1=1 AND A.BillStatus <>1 AND A.CompanyCD = @CompanyCD ");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND A.ProviderID=@ProviderID       ");
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND B.CustName like @ProviderName ");
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND A.AskDate >= @StartConfirmDate");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND A.AskDate < @EndConfirmDate ");
            }



            sql.AppendLine(" UNION all ");

            sql.AppendLine(" SELECT  A.ID, A.CompanyCD ,A.ContractNo,'采购合同' AS Danjuleixing,isnull(A.ProviderID,0) AS ProviderID,isnull(A.Seller,0) AS Seller    ");
            sql.AppendLine(" ,isnull(B.CustNo,'') AS ProviderNo,isnull(B.CustName,'') AS ProviderName,isnull(C.EmployeeName,'') AS EmployeeName           ");
            sql.AppendLine(" ,A.SignDate,Convert(numeric(20,2),isnull(A.RealTotal,0)*isnull(A.Rate,0)) AS RealTotal ");
            

            sql.AppendLine(" FROM officedba.PurchaseContract AS A   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProviderID=B.ID          ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Seller=C.ID              ");

            sql.AppendLine(" WHERE 1=1 AND A.BillStatus <>1 AND A.CompanyCD = @CompanyCD");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND A.ProviderID= @ProviderID        ");
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND B.CustName like @ProviderName ");
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND A.SignDate >=@StartConfirmDate");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND A.SignDate < @EndConfirmDate");
            }


            sql.AppendLine(" UNION all");

            sql.AppendLine(" SELECT  A.ID, A.CompanyCD ,A.OrderNo,'采购订单' AS Danjuleixing,isnull(A.ProviderID,0) AS ProviderID,isnull(A.Purchaser,0) AS Purchaser");
            sql.AppendLine(" ,isnull(B.CustNo,'') AS ProviderNo,isnull(B.CustName,'') AS ProviderName,isnull(C.EmployeeName,'') AS EmployeeName           ");
            sql.AppendLine(" ,A.OrderDate,Convert(numeric(20,2),isnull(A.RealTotal,0)*isnull(A.Rate,0)) AS RealTotal ");


            sql.AppendLine(" FROM officedba.PurchaseOrder AS A   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProviderID=B.ID          ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Purchaser=C.ID           ");

            sql.AppendLine(" WHERE 1=1 AND A.BillStatus <>1 AND A.CompanyCD = @CompanyCD ");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND A.ProviderID= @ProviderID       ");
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND B.CustName like @ProviderName ");
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND A.OrderDate >= @StartConfirmDate");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND A.OrderDate < @EndConfirmDate ");
            }


            sql.AppendLine(" UNION all");

            sql.AppendLine(" SELECT  A.ID, A.CompanyCD ,A.ArriveNo,'采购到货' AS Danjuleixing,isnull(A.ProviderID,0) AS ProviderID,isnull(A.Purchaser,0) AS Purchaser");
            sql.AppendLine(" ,isnull(B.CustNo,'') AS ProviderNo,isnull(B.CustName,'') AS ProviderName,isnull(C.EmployeeName,'') AS EmployeeName           ");
            sql.AppendLine(" ,A.ArriveDate,Convert(numeric(20,2),isnull(A.RealTotal,0)*isnull(A.Rate,0)) AS RealTotal ");


            sql.AppendLine(" FROM officedba.PurchaseArrive AS A   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProviderID=B.ID          ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Purchaser=C.ID           ");

            sql.AppendLine(" WHERE 1=1 AND A.BillStatus <>1 AND A.CompanyCD = @CompanyCD ");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND A.ProviderID= @ProviderID       ");
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND B.CustName like @ProviderName");
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND A.ArriveDate >= @StartConfirmDate");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND A.ArriveDate < @EndConfirmDate ");
            }


            sql.AppendLine(" UNION all");

            sql.AppendLine(" SELECT  A.ID, A.CompanyCD ,A.RejectNo,'采购退货' AS Danjuleixing,isnull(A.ProviderID,0) AS ProviderID,isnull(A.Purchaser,0) AS Purchaser");
            sql.AppendLine(" ,isnull(B.CustNo,'') AS ProviderNo,isnull(B.CustName,'') AS ProviderName,isnull(C.EmployeeName,'') AS EmployeeName           ");
            sql.AppendLine(" ,A.RejectDate,Convert(numeric(20,2),isnull(A.RealTotal,0)*isnull(A.Rate,0)) AS RealTotal ");


            sql.AppendLine(" FROM officedba.PurchaseReject AS A   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProviderID=B.ID          ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Purchaser=C.ID           ");

            sql.AppendLine(" WHERE 1=1 AND A.BillStatus <>1 AND A.CompanyCD = '" + CompanyCD + "' ");

            sql.AppendLine("AND A.CompanyCD = @CompanyCD              ");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND A.ProviderID= @ProviderID       ");
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND B.CustName like @ProviderName");
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND A.RejectDate >= @StartConfirmDate");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND A.RejectDate < @EndConfirmDate ");
            }

            sql.AppendLine(" UNION all");

            sql.AppendLine(" SELECT  A.ID,A.CompanyCD , A.BillCD ,'采购订单' AS Danjuleixing,isnull(B.ProviderID,0) AS ProviderID ,isnull(B.Purchaser,0) AS Purchaser");
            sql.AppendLine(" ,isnull(C.CustNo,'') AS ProviderNo,isnull(C.CustName,'') AS ProviderName,isnull(D.EmployeeName,'') AS EmployeeName ");
            sql.AppendLine("  ,B.OrderDate,Convert(numeric(12,2),isnull(A.YAccounts,0)*isnull(B.Rate,0)) AS YAccounts ");

            sql.AppendLine(" FROM officedba.BlendingDetails AS A   ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseOrder AS B ON A.CompanyCD = B.CompanyCD AND A.BillCD=B.OrderNo        ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS C ON B.CompanyCD = C.CompanyCD AND B.ProviderID=C.ID          ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON B.CompanyCD = D.CompanyCD AND B.Purchaser=D.ID           ");

            sql.AppendLine(" WHERE 1=1 AND A.BillingType ='2' ");
            sql.AppendLine("AND A.CompanyCD =@CompanyCD");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND B.ProviderID= @ProviderID ");
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND C.CustName like @ProviderName ");
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND B.OrderDate >=@StartConfirmDate ");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND B.OrderDate < @EndConfirmDate");
            }


            sql.AppendLine(" UNION all");

            sql.AppendLine("SELECT  A.ID, A.CompanyCD ,A.BillCD ,'采购退货单' AS Danjuleixing,isnull(B.ProviderID,0) AS ProviderID ,isnull(B.Purchaser,0) AS Purchaser");
            sql.AppendLine(",isnull(C.CustNo,'') AS ProviderNo,isnull(C.CustName,'') AS ProviderName,isnull(D.EmployeeName,'') AS EmployeeName");
            sql.AppendLine(",B.RejectDate,Convert(numeric(12,2),isnull(A.YAccounts,0)*isnull(B.Rate,0)) AS YAccounts");
            sql.AppendLine("FROM officedba.BlendingDetails AS A  ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseReject AS B ON A.CompanyCD = B.CompanyCD AND A.BillCD=B.RejectNo ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS C ON B.CompanyCD = C.CompanyCD AND B.ProviderID=C.ID  ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON B.CompanyCD = D.CompanyCD AND B.Purchaser=D.ID ");
            sql.AppendLine(" WHERE 1=1 AND A.BillingType ='5' ");


            sql.AppendLine("AND A.CompanyCD = @CompanyCD");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND B.ProviderID= @ProviderID");
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND C.CustName like  @ProviderName");
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND B.RejectDate >= @StartConfirmDate");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND B.RejectDate <@EndConfirmDate ");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",  CompanyCD ));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderName", "'% "+ProviderName+" %'"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", Convert.ToDateTime(EndConfirmDate).AddDays(1).ToString("yyyy-MM-dd")));
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);

        }
        #endregion

        #region 采购报表供应商台帐查询打印
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseProviderAccountPrint(string ProviderID, string ProviderName, string StartConfirmDate, string EndConfirmDate, string CompanyCD, string orderBy)
        {
            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT  A.ID,A.CompanyCD, A.AskNo AS TheyDelegate ,'采购询价单' AS SignAddr,isnull(A.ProviderID,0) AS  ProviderID, isnull(A.AskUserID,0) AS AskUserID ");
            sql.AppendLine(" ,isnull(B.CustNo,'') AS ContractNo,isnull(B.CustName,'') AS Title,isnull(C.EmployeeName,'') AS Note           ");
            sql.AppendLine(" ,A.AskDate AS  SignDate,Convert(numeric(20,"+jingdu +"),isnull(A.RealTotal,0)*isnull(A.Rate,0)) AS TotalPrice ");

            sql.AppendLine(" FROM officedba.PurchaseAskPrice AS A         ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProviderID=B.ID          ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.AskUserID=C.ID           ");

            sql.AppendLine(" WHERE 1=1 AND A.BillStatus <>1 AND A.CompanyCD = @CompanyCD ");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND A.ProviderID=@ProviderID      ");
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND B.CustName like @ProviderName ");
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND A.AskDate >=@StartConfirmDate");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND A.AskDate < @EndConfirmDate ");
            }
            


            sql.AppendLine(" UNION all ");

            sql.AppendLine(" SELECT  A.ID, A.CompanyCD ,A.ContractNo,'采购合同' AS Danjuleixing,isnull(A.ProviderID,0) AS ProviderID,isnull(A.Seller,0) AS Seller    ");
            sql.AppendLine(" ,isnull(B.CustNo,'') AS ProviderNo,isnull(B.CustName,'') AS ProviderName,isnull(C.EmployeeName,'') AS EmployeeName           ");
            sql.AppendLine(" ,A.SignDate,Convert(numeric(20,2),isnull(A.RealTotal,0)*isnull(A.Rate,0)) AS RealTotal ");


            sql.AppendLine(" FROM officedba.PurchaseContract AS A   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProviderID=B.ID          ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Seller=C.ID              ");

            sql.AppendLine(" WHERE 1=1 AND A.BillStatus <>1 AND A.CompanyCD = @CompanyCD ");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND A.ProviderID= @ProviderID      ");
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND B.CustName like @ProviderName");
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND A.SignDate >=  @StartConfirmDate");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND A.SignDate < @EndConfirmDate ");
            }
            

            sql.AppendLine(" UNION all");

            sql.AppendLine(" SELECT  A.ID, A.CompanyCD ,A.OrderNo,'采购订单' AS Danjuleixing,isnull(A.ProviderID,0) AS ProviderID,isnull(A.Purchaser,0) AS Purchaser");
            sql.AppendLine(" ,isnull(B.CustNo,'') AS ProviderNo,isnull(B.CustName,'') AS ProviderName,isnull(C.EmployeeName,'') AS EmployeeName           ");
            sql.AppendLine(" ,A.OrderDate,Convert(numeric(20,2),isnull(A.RealTotal,0)*isnull(A.Rate,0)) AS RealTotal ");


            sql.AppendLine(" FROM officedba.PurchaseOrder AS A   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProviderID=B.ID          ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Purchaser=C.ID           ");

            sql.AppendLine(" WHERE 1=1 AND A.BillStatus <>1 AND A.CompanyCD = @CompanyCD ");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND A.ProviderID=@ProviderID      ");
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND B.CustName like @ProviderName ");
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND A.OrderDate >=@StartConfirmDate");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND A.OrderDate < @EndConfirmDate");
            }
            

            sql.AppendLine(" UNION all");

            sql.AppendLine(" SELECT  A.ID, A.CompanyCD ,A.ArriveNo,'采购到货' AS Danjuleixing,isnull(A.ProviderID,0) AS ProviderID,isnull(A.Purchaser,0) AS Purchaser");
            sql.AppendLine(" ,isnull(B.CustNo,'') AS ProviderNo,isnull(B.CustName,'') AS ProviderName,isnull(C.EmployeeName,'') AS EmployeeName           ");
            sql.AppendLine(" ,A.ArriveDate,Convert(numeric(20,2),isnull(A.RealTotal,0)*isnull(A.Rate,0)) AS RealTotal ");


            sql.AppendLine(" FROM officedba.PurchaseArrive AS A   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProviderID=B.ID          ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Purchaser=C.ID           ");

            sql.AppendLine(" WHERE 1=1 AND A.BillStatus <>1 AND A.CompanyCD =@CompanyCD");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND A.ProviderID=@ProviderID       ");
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND B.CustName like @ProviderName");
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND A.ArriveDate >= @StartConfirmDate");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND A.ArriveDate < @EndConfirmDate ");
            }
            

            sql.AppendLine(" UNION all");

            sql.AppendLine(" SELECT  A.ID, A.CompanyCD ,A.RejectNo,'采购退货' AS Danjuleixing,isnull(A.ProviderID,0) AS ProviderID,isnull(A.Purchaser,0) AS Purchaser");
            sql.AppendLine(" ,isnull(B.CustNo,'') AS ProviderNo,isnull(B.CustName,'') AS ProviderName,isnull(C.EmployeeName,'') AS EmployeeName           ");
            sql.AppendLine(" ,A.RejectDate,Convert(numeric(20,2),isnull(A.RealTotal,0)*isnull(A.Rate,0)) AS RealTotal ");


            sql.AppendLine(" FROM officedba.PurchaseReject AS A   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProviderID=B.ID          ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Purchaser=C.ID           ");

            sql.AppendLine(" WHERE 1=1 AND A.BillStatus <>1 AND A.CompanyCD = @CompanyCD ");

            sql.AppendLine("AND A.CompanyCD = @CompanyCD             ");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND A.ProviderID= @ProviderID      ");
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND B.CustName like @ProviderName ");
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND A.RejectDate >=@StartConfirmDate");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND A.RejectDate < @EndConfirmDate");
            }
            

            sql.AppendLine(" UNION all");

            sql.AppendLine(" SELECT  A.ID,A.CompanyCD , A.BillCD ,'采购订单' AS Danjuleixing,isnull(B.ProviderID,0) AS ProviderID ,isnull(B.Purchaser,0) AS Purchaser");
            sql.AppendLine(" ,isnull(C.CustNo,'') AS ProviderNo,isnull(C.CustName,'') AS ProviderName,isnull(D.EmployeeName,'') AS EmployeeName ");
            sql.AppendLine("  ,B.OrderDate,Convert(numeric(12,2),isnull(A.YAccounts,0)*isnull(B.Rate,0)) AS YAccounts ");

            sql.AppendLine(" FROM officedba.BlendingDetails AS A   ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseOrder AS B ON A.CompanyCD = B.CompanyCD AND A.BillCD=B.OrderNo        ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS C ON B.CompanyCD = C.CompanyCD AND B.ProviderID=C.ID          ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON B.CompanyCD = D.CompanyCD AND B.Purchaser=D.ID           ");

            sql.AppendLine(" WHERE 1=1 AND A.BillingType ='2' ");
            sql.AppendLine("AND A.CompanyCD =@CompanyCD");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND B.ProviderID= @ProviderID ");
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND C.CustName like @ProviderName ");
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND B.OrderDate >= @StartConfirmDate");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND B.OrderDate < @EndConfirmDate ");
            }
            

            sql.AppendLine(" UNION all");

            sql.AppendLine("SELECT  A.ID, A.CompanyCD ,A.BillCD ,'采购退货单' AS Danjuleixing,isnull(B.ProviderID,0) AS ProviderID ,isnull(B.Purchaser,0) AS Purchaser");
            sql.AppendLine(",isnull(C.CustNo,'') AS ProviderNo,isnull(C.CustName,'') AS ProviderName,isnull(D.EmployeeName,'') AS EmployeeName");
            sql.AppendLine(",B.RejectDate,Convert(numeric(12,2),isnull(A.YAccounts,0)*isnull(B.Rate,0)) AS YAccounts");
            sql.AppendLine("FROM officedba.BlendingDetails AS A  ");
            sql.AppendLine("LEFT JOIN officedba.PurchaseReject AS B ON A.CompanyCD = B.CompanyCD AND A.BillCD=B.RejectNo ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS C ON B.CompanyCD = C.CompanyCD AND B.ProviderID=C.ID  ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON B.CompanyCD = D.CompanyCD AND B.Purchaser=D.ID ");
            sql.AppendLine(" WHERE 1=1 AND A.BillingType ='5' ");


            sql.AppendLine("AND A.CompanyCD =@CompanyCD");
            if (ProviderID != "" && ProviderID != null)
            {
                sql.AppendLine(" AND B.ProviderID= @ProviderID");
            }
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" AND C.CustName like @ProviderName ");
            }
            if (StartConfirmDate != "" && StartConfirmDate != null)
            {
                sql.AppendLine(" AND B.RejectDate >= @StartConfirmDate ");
            }
            if (EndConfirmDate != "" && EndConfirmDate != null)
            {
                sql.AppendLine(" AND B.RejectDate < @EndConfirmDate ");
            }

            sql.AppendLine(" ORDER BY " + orderBy + "");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",  CompanyCD ));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderName", "'% " + ProviderName + " %'"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartConfirmDate", StartConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndConfirmDate", Convert.ToDateTime(EndConfirmDate).AddDays(1).ToString("yyyy-MM-dd")));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

    }
}

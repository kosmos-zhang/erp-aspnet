/**********************************************
 * 类作用：   采购询价单数据库层处理
 * 建立人：   王超
 * 建立时间： 2009/04/30
 * 修改人：   王保军                          *
 * 修改时间： 2009/08/27                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using System.Data.SqlClient;
using XBase.Model.Office.PurchaseManager;
using XBase.Common;
using System.Data.SqlTypes;
using System.Data.Sql;

namespace XBase.Data.Office.PurchaseManager
{
    public class PurchaseAskPriceDBHelper
    {
        #region 主表操作
        #region insert
        /// <summary>
        /// 插入采购询价主表
        /// </summary>
        /// <param name="PurchaseAskPriceM">采购询价主表model</param>
        /// <returns>sqlcommend</returns>
        /// 
        public static SqlCommand InsertPurAskPricePri(PurchaseAskPriceModel PurchaseAskPriceM)
        {
            SqlCommand comm = new SqlCommand();
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.PurchaseAskPrice");
            sql.AppendLine("           (CompanyCD                 ");
            sql.AppendLine("           ,AskNo                     ");
            sql.AppendLine("           ,AskOrder                  ");
            sql.AppendLine("           ,AskTitle                  ");
            sql.AppendLine("           ,FromType                  ");
            sql.AppendLine("           ,AskDate                   ");
            sql.AppendLine("           ,ProviderID                ");
            sql.AppendLine("           ,DeptID                    ");
            sql.AppendLine("           ,AskUserID                 ");
            sql.AppendLine("           ,TypeID                    ");
            sql.AppendLine("           ,CurrencyType              ");
            sql.AppendLine("           ,Rate                      ");
            sql.AppendLine("           ,TotalPrice                ");
            sql.AppendLine("           ,TotalTax                  ");
            sql.AppendLine("           ,TotalFee                  ");
            sql.AppendLine("           ,Discount                  ");
            sql.AppendLine("           ,DiscountTotal             ");
            sql.AppendLine("           ,RealTotal                 ");
            sql.AppendLine("           ,isAddTax                  ");
            sql.AppendLine("           ,CountTotal                ");
            sql.AppendLine("           ,remark                    ");
            sql.AppendLine("           ,BillStatus                ");
            sql.AppendLine("           ,Creator                   ");
            sql.AppendLine("           ,CreateDate                ");
            sql.AppendLine("           ,ModifiedDate              ");
            sql.AppendLine("           ,ModifiedUserID            ");
            //sql.AppendLine("           ,Confirmor                 ");
            //sql.AppendLine("           ,ConfirmDate               ");
            //sql.AppendLine("           ,Closer                    ");
            //sql.AppendLine("           ,CloseDate                ");
            sql.AppendLine(")");
            sql.AppendLine("     VALUES                           ");
            sql.AppendLine("     	   (@CompanyCD                 ");
            sql.AppendLine("           ,@AskNo                    ");
            sql.AppendLine("           ,@AskOrder                 ");
            sql.AppendLine("           ,@AskTitle                 ");
            sql.AppendLine("           ,@FromType                 ");
            sql.AppendLine("           ,@AskDate                  ");
            sql.AppendLine("           ,@ProviderID               ");
            sql.AppendLine("           ,@DeptID                   ");
            sql.AppendLine("           ,@AskUserID                ");
            sql.AppendLine("           ,@TypeID                   ");
            sql.AppendLine("           ,@CurrencyType             ");
            sql.AppendLine("           ,@Rate                     ");
            sql.AppendLine("           ,@TotalPrice               ");
            sql.AppendLine("           ,@TotalTax                 ");
            sql.AppendLine("           ,@TotalFee                 ");
            sql.AppendLine("           ,@Discount                 ");
            sql.AppendLine("           ,@DiscountTotal            ");
            sql.AppendLine("           ,@RealTotal                ");
            sql.AppendLine("           ,@isAddTax                 ");
            sql.AppendLine("           ,@CountTotal               ");
            sql.AppendLine("           ,@remark                   ");
            sql.AppendLine("           ,@BillStatus               ");
            sql.AppendLine("           ,@Creator                  ");
            sql.AppendLine("           ,@CreateDate               ");
            sql.AppendLine("           ,@ModifiedDate             ");
            sql.AppendLine("           ,@ModifiedUserID           ");
            //sql.AppendLine("           ,@Confirmor                ");
            //sql.AppendLine("           ,@ConfirmDate              ");
            //sql.AppendLine("           ,@Closer                   ");
            //sql.AppendLine("           ,@CloseDate               ");
            sql.AppendLine(")");
            sql.AppendLine("set @IndexID = @@IDENTITY");

            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskNo", PurchaseAskPriceM.AskNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskOrder", PurchaseAskPriceM.AskOrder));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskTitle", PurchaseAskPriceM.AskTitle));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", PurchaseAskPriceM.FromType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskDate", PurchaseAskPriceM.AskDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", PurchaseAskPriceM.ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", PurchaseAskPriceM.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskUserID", PurchaseAskPriceM.AskUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", PurchaseAskPriceM.TypeID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrencyType", PurchaseAskPriceM.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate", PurchaseAskPriceM.Rate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice", PurchaseAskPriceM.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalTax", PurchaseAskPriceM.TotalTax));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalFee", PurchaseAskPriceM.TotalFee));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Discount", PurchaseAskPriceM.Discount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DiscountTotal", PurchaseAskPriceM.DiscountTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RealTotal", PurchaseAskPriceM.RealTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@isAddTax", PurchaseAskPriceM.isAddTax));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal", PurchaseAskPriceM.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@remark", PurchaseAskPriceM.remark));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchaseAskPriceM.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", PurchaseAskPriceM.Creator));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", PurchaseAskPriceM.CreateDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", PurchaseAskPriceM.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", PurchaseAskPriceM.ModifiedUserID));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", PurchaseAskPriceM.Confirmor));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate", PurchaseAskPriceM.ConfirmDate));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@Closer", PurchaseAskPriceM.Closer));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", PurchaseAskPriceM.CloseDate));

            SqlParameter IndexID = new SqlParameter("@IndexID", SqlDbType.Int);
            IndexID.Direction = ParameterDirection.Output;
            comm.Parameters.Add(IndexID);
            #endregion

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion
        
        #region update
        /// <summary>
        ///更新采购询价主表
        /// </summary>
        /// <param name="PurchaseAskPriceM">采购询价主表model</param>
        /// <returns>sqlcommend</returns>
        /// 
        public static SqlCommand UpdatePurAskPricePri(PurchaseAskPriceModel PurchaseAskPriceM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseAskPrice ");
            sql.AppendLine("   SET CompanyCD      =@CompanyCD       ");
            sql.AppendLine("      ,AskNo          =@AskNo           ");
            sql.AppendLine("      ,AskOrder       =@AskOrder        ");
            sql.AppendLine("      ,AskTitle       =@AskTitle        ");
            sql.AppendLine("      ,FromType       =@FromType        ");
            sql.AppendLine("      ,AskDate        =@AskDate         ");
            sql.AppendLine("      ,ProviderID     =@ProviderID      ");
            sql.AppendLine("      ,DeptID         =@DeptID          ");
            sql.AppendLine("      ,AskUserID      =@AskUserID       ");
            sql.AppendLine("      ,TypeID         =@TypeID          ");
            sql.AppendLine("      ,CurrencyType   =@CurrencyType    ");
            sql.AppendLine("      ,Rate           =@Rate            ");
            sql.AppendLine("      ,TotalPrice     =@TotalPrice      ");
            sql.AppendLine("      ,TotalTax       =@TotalTax        ");
            sql.AppendLine("      ,TotalFee       =@TotalFee        ");
            sql.AppendLine("      ,Discount       =@Discount        ");
            sql.AppendLine("      ,DiscountTotal  =@DiscountTotal   ");
            sql.AppendLine("      ,RealTotal      =@RealTotal       ");
            sql.AppendLine("      ,isAddTax       =@isAddTax        ");
            sql.AppendLine("      ,CountTotal     =@CountTotal      ");
            sql.AppendLine("      ,BillStatus     =@BillStatus      ");
            sql.AppendLine("      ,Confirmor      =@Confirmor     ");
            sql.AppendLine("      ,ConfirmDate    =@ConfirmDate ");
            sql.AppendLine("      ,remark         =@remark          ");
            sql.AppendLine("      ,ModifiedDate   =getDate()    ");
            sql.AppendLine("      ,ModifiedUserID =@ModifiedUserID  ");
            sql.AppendLine("WHERE ID=@ID                            ");
            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskNo", PurchaseAskPriceM.AskNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskOrder", PurchaseAskPriceM.AskOrder));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskTitle", PurchaseAskPriceM.AskTitle));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", PurchaseAskPriceM.FromType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskDate", PurchaseAskPriceM.AskDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", PurchaseAskPriceM.ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", PurchaseAskPriceM.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskUserID", PurchaseAskPriceM.AskUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", PurchaseAskPriceM.TypeID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrencyType", PurchaseAskPriceM.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate", PurchaseAskPriceM.Rate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice", PurchaseAskPriceM.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalTax", PurchaseAskPriceM.TotalTax));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalFee", PurchaseAskPriceM.TotalFee));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Discount", PurchaseAskPriceM.Discount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DiscountTotal", PurchaseAskPriceM.DiscountTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RealTotal", PurchaseAskPriceM.RealTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@isAddTax", PurchaseAskPriceM.isAddTax));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal", PurchaseAskPriceM.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchaseAskPriceM.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", PurchaseAskPriceM.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate", PurchaseAskPriceM.ConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@remark", PurchaseAskPriceM.remark));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", PurchaseAskPriceM.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", PurchaseAskPriceM.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", PurchaseAskPriceM.ID));
            #endregion

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region delete
        public static SqlCommand DeletePurAskPricePri(string IDs)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE FROM officedba.PurchaseAskPrice");
            sql.AppendLine(" WHERE ID in "+IDs+"");
            sql.AppendLine(" AND CompanyCD=@CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region select
        /// <summary>
        ///检索采购询价主表
        /// </summary>
        /// <param name="PurchaseAskPriceM">采购询价主表model</param>
        /// <returns>datatable</returns>
        /// 
        public static DataTable SelectPurAskPricePri(PurchaseAskPriceModel PurchaseAskPriceM, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Select ID,ModifiedDate,CompanyCD,AskNo,AskOrder,AskTitle,FromType,FromTypeName,AskDate,AskUserID,DeptID,AskUserName,ProviderID,ProviderName,TotalPrice,TotalTax     ");
            sql.AppendLine(",TotalFee,BillStatus,BillStatusName,isnull(FlowStatus,'') AS FlowStatus                                                                         ");
            sql.AppendLine(",case FlowStatus WHEN '1' then '待审批' when '2' then '审批中' when '3' then '审批通过'                                                         ");
            sql.AppendLine("when '4' then '审批不通过' when '5' then '撤销审批' else '' end as FlowStatusName,ExtField1,ExtField2,ExtField3,ExtField4,ExtField5,ExtField6,ExtField7,ExtField8,ExtField9,ExtField10                                                        ");
            sql.AppendLine(" from (SELECT A.ID                                                                                                                              ");
            sql.AppendLine("     , isnull(A.CompanyCD  ,'') AS  CompanyCD                                                                                                   ");
            sql.AppendLine("     , isnull(A.AskNo      ,'') AS  AskNo  ,isnull(A.ModifiedDate,'') AS ModifiedDate           ");
            sql.AppendLine("     , isnull(A.AskOrder   ,0) AS  AskOrder                                                                                                     ");
            sql.AppendLine("     , isnull(A.AskTitle   ,'') AS  AskTitle                                                                                                    ");
            sql.AppendLine("     , isnull(A.FromType   ,'') AS  FromType                                                                                                    ");
            sql.AppendLine("     ,case A.FromType when '1' then '采购申请单' when '2' then '采购计划单' else '无来源' end as FromTypeName                                   ");
            sql.AppendLine("     , isnull(CONVERT(varchar(23),A.AskDate,23),'') AS  AskDate                                                                                 ");
            sql.AppendLine("     ,isnull(A.AskUserID,0) AS AskUserID   ,A.DeptID");
            sql.AppendLine("     ,isnull(D.EmployeeName,'') AS AskUserName                                                                                                  ");
            sql.AppendLine("     , isnull(A.ProviderID ,0) AS  ProviderID                                                                                                   ");
            sql.AppendLine("     , isnull(B.CustName   ,'') AS  ProviderName                                                                                                ");
            sql.AppendLine("     , isnull(A.TotalPrice ,0) AS  TotalPrice                                                                                                   ");
            sql.AppendLine("     , isnull(A.TotalTax   ,0) AS  TotalTax                                                                                                     ");
            sql.AppendLine("     , isnull(A.TotalFee   ,0) AS  TotalFee                                                                                                     ");
            sql.AppendLine("     , isnull(A.BillStatus ,'') AS  BillStatus                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField1 ,'') AS  ExtField1                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField2 ,'') AS  ExtField2                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField3 ,'') AS  ExtField3                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField4 ,'') AS  ExtField4                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField5 ,'') AS  ExtField5                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField6 ,'') AS  ExtField6                                                                                                  "); 
            sql.AppendLine("     , isnull(A.ExtField7 ,'') AS  ExtField7                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField8 ,'') AS  ExtField8                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField9 ,'') AS  ExtField9                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField10 ,'') AS  ExtField10                                                                                                  ");
            sql.AppendLine("     ,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'                                                          ");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' end AS BillStatusName                                                                         ");
            sql.AppendLine(",(SELECT top 1 FlowStatus FROM officedba.FlowInstance  WHERE BillTypeFlag=6 AND BillTypeCode=3 AND BillID= A.ID ORDER BY ID DESC ) AS FlowStatus");
            sql.AppendLine(" FROM         officedba.PurchaseAskPrice AS A                                                                                                   ");
            sql.AppendLine(" LEFT JOIN officedba.ProviderInfo AS B ON A.ProviderID = B.ID                                                                                   ");
            sql.AppendLine(" LEFT JOIN officedba.EmployeeInfo AS D ON A.AskUserID = D.ID  )AS E                                                                             ");

            sql.AppendLine(" WHERE CompanyCD=@CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));

            if (PurchaseAskPriceM.AskNo != "")
            {
                sql.AppendLine(" AND AskNo like @AskNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskNo", "%" + PurchaseAskPriceM.AskNo + "%"));
            }
            if (PurchaseAskPriceM.AskTitle != "")
            {
                sql.AppendLine(" AND AskTitle like @AskTitle");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskTitle", "%" + PurchaseAskPriceM.AskTitle + "%"));
            }
            if (PurchaseAskPriceM.FromType != "a")
            {
                sql.AppendLine(" AND FromType=@FromType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", PurchaseAskPriceM.FromType));
            }
            if (PurchaseAskPriceM.DeptID != "")
            {
                sql.AppendLine(" AND DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", PurchaseAskPriceM.DeptID));
            }
            if (PurchaseAskPriceM.AskUserID != "")
            {
                sql.AppendLine(" AND AskUserID=@AskUserID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskUserID", PurchaseAskPriceM.AskUserID));
            }
            if (PurchaseAskPriceM.ProviderID != "")
            {
                sql.AppendLine(" AND ProviderID=@ProviderID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", PurchaseAskPriceM.ProviderID));
            }
            if (PurchaseAskPriceM.BillStatus != "0")
            {
                sql.AppendLine(" AND BillStatus=@BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchaseAskPriceM.BillStatus));
            }
            if (PurchaseAskPriceM.FlowStatus != "")
            {
                if (PurchaseAskPriceM.FlowStatus == "0")
                {
                    sql.AppendLine(" AND FlowStatus is NULL ");
                }
                else
                {
                    sql.AppendLine(" AND FlowStatus=@FlowStatus");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", PurchaseAskPriceM.FlowStatus));
                }
            }
            if (PurchaseAskPriceM.AskDate != "")
            {
                sql.AppendLine(" AND AskDate >= @StartAskDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartAskDate", PurchaseAskPriceM.AskDate));

            }
            if (PurchaseAskPriceM.EndAskDate != "")
            {
                sql.AppendLine(" AND AskDate <= @EndAskDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndAskDate", PurchaseAskPriceM.EndAskDate));
            }
            #endregion
            if (!string.IsNullOrEmpty(PurchaseAskPriceM.EFIndex) && !string.IsNullOrEmpty(PurchaseAskPriceM.EFDesc))
            {
                sql.AppendLine(" and ExtField" + PurchaseAskPriceM.EFIndex + " LIKE @EFDesc");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + PurchaseAskPriceM.EFDesc + "%"));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
            //return SqlHelper.ExecuteSearch(comm);

        }


        public static DataTable SelectPurAskPricePri(PurchaseAskPriceModel PurchaseAskPriceM, string OrderBy)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Select ID,CompanyCD,AskNo,AskOrder,AskTitle,FromType,FromTypeName,AskDate,AskUserID,DeptID,AskUserName,ProviderID,ProviderName,TotalPrice,TotalTax     ");
            sql.AppendLine(",TotalFee,BillStatus,BillStatusName,isnull(FlowStatus,'') AS FlowStatus                                                                         ");
            sql.AppendLine(",case FlowStatus WHEN '1' then '待审批' when '2' then '审批中' when '3' then '审批通过'                                                         ");
            sql.AppendLine("when '4' then '审批不通过' when '5' then '撤销审批' else '' end as FlowStatusName,ExtField1,ExtField2,ExtField3,ExtField4,ExtField5,ExtField6,ExtField7,ExtField8,ExtField9,ExtField10                                                            ");
            sql.AppendLine(" from (SELECT A.ID                                                                                                                              ");
            sql.AppendLine("     , isnull(A.CompanyCD  ,'') AS  CompanyCD                                                                                                   ");
            sql.AppendLine("     , isnull(A.AskNo      ,'') AS  AskNo                                                                                                       ");
            sql.AppendLine("     , isnull(A.AskOrder   ,0) AS  AskOrder                                                                                                     ");
            sql.AppendLine("     , isnull(A.AskTitle   ,'') AS  AskTitle                                                                                                    ");
            sql.AppendLine("     , isnull(A.FromType   ,'') AS  FromType                                                                                                    ");
            sql.AppendLine("     ,case A.FromType when '1' then '采购申请单' when '2' then '采购计划单' else '无来源' end as FromTypeName                                   ");
            sql.AppendLine("     , isnull(CONVERT(varchar(23),A.AskDate,23),'') AS  AskDate                                                                                 ");
            sql.AppendLine("     ,isnull(A.AskUserID,0) AS AskUserID   ,A.DeptID");
            sql.AppendLine("     ,isnull(D.EmployeeName,'') AS AskUserName                                                                                                  ");
            sql.AppendLine("     , isnull(A.ProviderID ,0) AS  ProviderID                                                                                                   ");
            sql.AppendLine("     , isnull(B.CustName   ,'') AS  ProviderName                                                                                                ");
            sql.AppendLine("     , isnull(A.TotalPrice ,0) AS  TotalPrice                                                                                                   ");
            sql.AppendLine("     , isnull(A.TotalTax   ,0) AS  TotalTax                                                                                                     ");
            sql.AppendLine("     , isnull(A.TotalFee   ,0) AS  TotalFee                                                                                                     ");
            sql.AppendLine("     , isnull(A.BillStatus ,'') AS  BillStatus                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField1 ,'') AS  ExtField1                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField2 ,'') AS  ExtField2                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField3 ,'') AS  ExtField3                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField4 ,'') AS  ExtField4                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField5 ,'') AS  ExtField5                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField6 ,'') AS  ExtField6                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField7 ,'') AS  ExtField7                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField8 ,'') AS  ExtField8                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField9 ,'') AS  ExtField9                                                                                                  ");
            sql.AppendLine("     , isnull(A.ExtField10 ,'') AS  ExtField10                                                                                                  ");
            sql.AppendLine("     ,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'                                                          ");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' end AS BillStatusName                                                                         ");
            sql.AppendLine(",(SELECT top 1 FlowStatus FROM officedba.FlowInstance  WHERE BillTypeFlag=6 AND BillTypeCode=3 AND BillID= A.ID ORDER BY ID DESC ) AS FlowStatus");
            sql.AppendLine(" FROM         officedba.PurchaseAskPrice AS A                                                                                                   ");
            sql.AppendLine(" LEFT JOIN officedba.ProviderInfo AS B ON A.ProviderID = B.ID                                                                                   ");
            sql.AppendLine(" LEFT JOIN officedba.EmployeeInfo AS D ON A.AskUserID = D.ID  )AS E                                                                             ");

            sql.AppendLine(" WHERE CompanyCD=@CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));

            if (PurchaseAskPriceM.AskNo != "")
            {
                sql.AppendLine(" AND AskNo like @AskNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskNo", "%" + PurchaseAskPriceM.AskNo + "%"));
            }
            if (PurchaseAskPriceM.AskTitle != "")
            {
                sql.AppendLine(" AND AskTitle like @AskTitle");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskTitle", "%" + PurchaseAskPriceM.AskTitle + "%"));
            }
            if (PurchaseAskPriceM.FromType != "a")
            {
                sql.AppendLine(" AND FromType=@FromType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", PurchaseAskPriceM.FromType));
            }
            if (PurchaseAskPriceM.DeptID != "")
            {
                sql.AppendLine(" AND DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", PurchaseAskPriceM.DeptID));
            }
            if (PurchaseAskPriceM.AskUserID != "")
            {
                sql.AppendLine(" AND AskUserID=@AskUserID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskUserID", PurchaseAskPriceM.AskUserID));
            }
            if (PurchaseAskPriceM.ProviderID != "")
            {
                sql.AppendLine(" AND ProviderID=@ProviderID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", PurchaseAskPriceM.ProviderID));
            }
            if (PurchaseAskPriceM.BillStatus != "0")
            {
                sql.AppendLine(" AND BillStatus=@BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchaseAskPriceM.BillStatus));
            }
            if (PurchaseAskPriceM.FlowStatus != "")
            {
                if (PurchaseAskPriceM.FlowStatus == "0")
                {
                    sql.AppendLine(" AND FlowStatus is NULL ");
                }
                else
                {
                    sql.AppendLine(" AND FlowStatus=@FlowStatus");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", PurchaseAskPriceM.FlowStatus));
                }
            }
            if (PurchaseAskPriceM.AskDate != "")
            {
                sql.AppendLine(" AND AskDate >= @StartAskDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartAskDate", PurchaseAskPriceM.AskDate));

            }
            if (PurchaseAskPriceM.EndAskDate != "")
            {
                sql.AppendLine(" AND AskDate <= @EndAskDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndAskDate", PurchaseAskPriceM.EndAskDate));
            }
            if (!string.IsNullOrEmpty(PurchaseAskPriceM.EFIndex) && !string.IsNullOrEmpty(PurchaseAskPriceM.EFDesc))
            {
                sql.AppendLine(" and ExtField" + PurchaseAskPriceM.EFIndex + " LIKE @EFDesc");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + PurchaseAskPriceM.EFDesc + "%"));
            }
            sql.AppendLine(" ORDER BY "+OrderBy+"");
            #endregion

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 判断是否被引用
        public static bool IsCite(string ID)
        {
            bool IsCite = false;
            //采购合同引用
            if (!IsCite)  //,  A.FromType, B.FromBillID 
            {//SELECT ID FROM officedba.PurchaseContract WHERE FromType=@FromType AND FromBillID=@ID
                string sql = "SELECT   A.ID FROM  officedba.PurchaseContract AS A INNER JOIN ";
                sql += "    officedba.PurchaseContractDetail AS B ON A.CompanyCD = B.CompanyCD AND A.ContractNo = B.ContractNo";
                sql += " WHERE A.FromType=@FromType AND B.FromBillID=@ID";
                SqlParameter[] parameters = {
                    new SqlParameter("@FromType", SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.VarChar),};
                parameters[0].Value = "3";
                parameters[1].Value = ID;
                IsCite = SqlHelper.Exists(sql, parameters);
            }
            //采购订单引用
            if (!IsCite)
            {
                string sql = "SELECT ID FROM officedba.PurchaseOrderDetail WHERE FromType=@FromType AND FromBillID=@ID";
                SqlParameter[] parameters = {
                    new SqlParameter("@FromType", SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.VarChar),};
                parameters[0].Value = "3";
                parameters[1].Value = ID;
                IsCite = SqlHelper.Exists(sql, parameters);
            }
            return IsCite;
        }
        #endregion


        #region 根据主表ID得到主表相关信息
        /// <summary>
        ///根据主表ID得到主表相关信息
        /// </summary>
        /// <param name="ID">采购询价主表ID</param>
        /// <returns>datatable</returns>
        /// 
        public static DataTable GetPurAskPricePriByID(string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID                                                                                     ");
            sql.AppendLine("     , A.CompanyCD                                                                              ");
            sql.AppendLine("     , A.AskNo                                                                                  ");
            sql.AppendLine("     , A.AskOrder                                                                               ");
            sql.AppendLine("     , isnull(A.AskTitle,'') as    AskTitle                                                                           ");
            sql.AppendLine("     , A.FromType                                                                               ");
            sql.AppendLine("     ,case A.FromType when '1' then '采购申请单' when '2' then '采购计划单' else '无来源' end as FromTypeName                        ");
            sql.AppendLine("		 , CONVERT(varchar(23),A.AskDate,23) AS AskDate                                             ");
            sql.AppendLine("		 , A.ProviderID                                                                             ");
            sql.AppendLine("		 , isnull(B.CustName,'') AS ProviderName                                                               ");
            sql.AppendLine("		 , A.DeptID                                                                                 ");
            sql.AppendLine("		 , isnull(C.DeptName,'') as      DeptName                                                                          ");
            sql.AppendLine("		 , A.AskUserID                                                                              ");
            sql.AppendLine("		 , isnull(D.EmployeeName,'') AS AskUserName                                                                           ");
            sql.AppendLine("		 , A.TypeID                                                                                 ");
            sql.AppendLine("		 , isnull(E.TypeName,'') as          TypeName                                                                      ");
            sql.AppendLine("		 , A.CurrencyType                                                                           ");
            sql.AppendLine("		 , isnull(F.CurrencyName,'') as         CurrencyName                                                                   ");
            sql.AppendLine("		 ,                       Convert(numeric(12," + userInfo.SelPoint + "),a.Rate) as Rate                                                        ");
            sql.AppendLine("		 ,                    Convert(numeric(22," + userInfo.SelPoint + "),a.TotalPrice) as TotalPrice                                                          ");
            sql.AppendLine("		 ,     Convert(numeric(22," + userInfo.SelPoint + "),a.TotalTax) as TotalTax                                                                                  ");
            sql.AppendLine("		 ,              Convert(numeric(22," + userInfo.SelPoint + "),a.TotalFee) as TotalFee                                                                          ");
            sql.AppendLine("		 ,                 Convert(numeric(12," + userInfo.SelPoint + "),a.Discount) as Discount                                                                              ");
            sql.AppendLine("		 ,                Convert(numeric(22," + userInfo.SelPoint + "),a.DiscountTotal) as DiscountTotal                                                                 ");
            sql.AppendLine("		 ,                 Convert(numeric(22," + userInfo.SelPoint + "),a.RealTotal) as RealTotal                                                                ");
            sql.AppendLine("		 , A.isAddTax                                                                               ");
            sql.AppendLine("		 ,                         Convert(numeric(22," + userInfo.SelPoint + "),a.CountTotal) as CountTotal                                                                    ");
            sql.AppendLine("		 , isnull(A.remark,'') as         remark                                                                         ");
            sql.AppendLine("		 , A.BillStatus                                                                             ");
            sql.AppendLine("     ,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'                                               ");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' end AS BillStatusName                                                              ");
            sql.AppendLine("     ,case A.isAddTax when '1' then '是' when '0' then '否'   end AS ShowName                                                              ");
            sql.AppendLine("		 , A.Creator                                                                                ");
            sql.AppendLine("		 , isnull(G.EmployeeName,'')                         AS CreatorName                                    ");
            sql.AppendLine("		 , CONVERT(varchar(23),A.CreateDate,23)   AS CreateDate                                     ");
            sql.AppendLine("		 , CONVERT(varchar(23),A.ModifiedDate,23) AS ModifiedDate                                   ");
            sql.AppendLine("		 , A.ModifiedUserID                                                                         "); 
            sql.AppendLine("		 , A.Confirmor                                                                              ");
            sql.AppendLine("		 , isnull(I.EmployeeName,'')                          AS ConfirmorName                                  ");
            sql.AppendLine("		 , CONVERT(varchar(23),A.ConfirmDate,23)  AS ConfirmDate                                    ");
            sql.AppendLine("		 , A.Closer                                                                                 ");
            sql.AppendLine("		 , isnull(J.EmployeeName,'')                         AS CloserName                                     ");
            sql.AppendLine("		 , CONVERT(varchar(23),A.CloseDate,23)    AS CloseDate                                      ");
            sql.AppendLine(" ,isnull(K.FlowStatus,'') AS FlowStatus");
            sql.AppendLine(",isnull(a.ExtField1,'') as ExtField1 ");
            sql.AppendLine(",isnull(a.ExtField2,'') as ExtField2 ");
            sql.AppendLine(",isnull(a.ExtField3,'') as ExtField3 ");
            sql.AppendLine(",isnull(a.ExtField4,'') as ExtField4 ");
            sql.AppendLine(",isnull(a.ExtField5,'') as ExtField5 ");
            sql.AppendLine(",isnull(a.ExtField6,'') as ExtField6 ");
            sql.AppendLine(",isnull(a.ExtField7,'') as ExtField7 ");
            sql.AppendLine(",isnull(a.ExtField8,'') as ExtField8 ");
            sql.AppendLine(",isnull(a.ExtField9,'') as ExtField9 ");
            sql.AppendLine(",isnull(a.ExtField10,'') as ExtField10 ");

            sql.AppendLine("FROM officedba.PurchaseAskPrice AS A                                                            ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.ProviderID = B.ID                                    ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS C ON A.DeptID = C.ID                                            ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON A.AskUserID = D.ID                                     ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS E ON A.TypeID = E.ID                                      ");
            sql.AppendLine("LEFT JOIN officedba.CurrencyTypeSetting AS F ON A.CurrencyType = F.ID                           ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS G ON A.Creator = G.ID                                       ");
            sql.AppendLine("LEFT JOIN officedba.UserInfo AS H ON A.ModifiedUserID = H.UserID AND A.CompanyCD = H.CompanyCD  ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS I ON A.Confirmor = I.ID                                     ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS J ON A.Closer = J.ID                                        ");
            sql.AppendLine(" LEFT JOIN officedba.FlowInstance AS K ON A.ID = K.BillID AND K.BillTypeFlag = 6 AND K.BillTypeCode = 3    ");
            sql.AppendLine(" AND K.ID=(SELECT max(ID) FROM officedba.FlowInstance AS L WHERE A.ID = L.BillID AND L.BillTypeFlag = 6 AND L.BillTypeCode = 3 )");
            sql.AppendLine("WHERE A.ID=@ID");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 确认
        /// <summary>
        ///确认采购询价
        /// </summary>
        /// <param name="ID">采购询价主表ID</param>
        /// <returns>bool</returns>
        /// 
        public static bool ConfirmPurAskPrice(string ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseAskPrice ");
            sql.AppendLine("   SET Confirmor = @Confirmor     ");
            sql.AppendLine("      ,ConfirmDate = @ConfirmDate ");
            sql.AppendLine("  ,ModifiedDate=getdate()");
            sql.AppendLine("  ,ModifiedUserID =@ModifiedUserID");
            sql.AppendLine("      ,BillStatus = @BillStatus ");
            sql.AppendLine(" WHERE ID=@ID                     ");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor",((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate",DateTime.Now.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID",((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID",ID));

            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        public static SqlCommand CancelConfirm(string ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseAskPrice ");
            sql.AppendLine("   SET Confirmor = null     ");
            sql.AppendLine("      ,ConfirmDate = null ");
            sql.AppendLine("  ,ModifiedDate=getdate()");
            sql.AppendLine("  ,ModifiedUserID =@ModifiedUserID");
            sql.AppendLine("      ,BillStatus = @BillStatus ");
            sql.AppendLine(" WHERE ID=@ID                     ");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "1"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

            return comm;
        }

        #region 结单
        /// <summary>
        ///采购询价单结单
        /// </summary>
        /// <param name="ID">采购询价主表ID</param>
        /// <returns>bool</returns>
        ///
        public static bool CompletePurAskPrice(string ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseAskPrice ");
            sql.AppendLine("   SET Closer = @Closer     ");
            sql.AppendLine("      ,CloseDate = @CloseDate ");
            sql.AppendLine("  ,ModifiedDate=getdate()");
            sql.AppendLine("  ,ModifiedUserID =@ModifiedUserID");
            sql.AppendLine("      ,BillStatus = @BillStatus ");
            sql.AppendLine(" WHERE ID=@ID                     ");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Closer", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", DateTime.Now.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "4"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        #region 取消结单
        /// <summary>
        ///采购询价单取消结单
        /// </summary>
        /// <param name="ID">采购询价主表ID</param>
        /// <returns>bool</returns>
        ///
        public static bool ConcelCompletePurAskPrice(string ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseAskPrice ");
            sql.AppendLine("   SET Closer = @Closer     ");
            sql.AppendLine("      ,CloseDate = @CloseDate ");
            sql.AppendLine("  ,ModifiedDate=getdate()");
            sql.AppendLine("  ,ModifiedUserID =@ModifiedUserID");
            sql.AppendLine("      ,BillStatus = @BillStatus ");
            sql.AppendLine(" WHERE ID=@ID                     ");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Closer", ""));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", ""));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion
        #endregion

        #region 明细表操作
        #region insert
        /// <summary>
        ///插入采购询价明细表
        /// </summary>
        /// <param name="PurchaseAskPriceDetailM">采购询价明细表model</param>
        /// <returns>sqlcommend</returns>
        /// 
        public static SqlCommand InsertPurAskPriceDetail(PurchaseAskPriceDetailModel PurchaseAskPriceDetailM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.PurchaseAskPriceDetail");
            sql.AppendLine("           (CompanyCD                       ");
            sql.AppendLine("           ,AskNo                           ");
            sql.AppendLine("           ,SortNo                          ");
            sql.AppendLine("           ,FromType                        ");
            sql.AppendLine("           ,FromBillID                      ");
            sql.AppendLine("           ,FromLineNo                      ");
            sql.AppendLine("           ,ProductID                       ");
            sql.AppendLine("           ,ProductNo                       ");
            sql.AppendLine("           ,ProductName                     ");
            sql.AppendLine("           ,ProductCount                    ");
            sql.AppendLine("           ,UnitID                          ");
            sql.AppendLine("           ,UnitPrice                       ");
            sql.AppendLine("           ,TotalPrice                      ");
            sql.AppendLine("           ,RequireDate                     ");
            sql.AppendLine("           ,ApplyReason                     ");
            sql.AppendLine("           ,Remark                          ");
            sql.AppendLine("           ,TaxPrice                        "); 
            sql.AppendLine("           ,TaxRate                         ");
            sql.AppendLine("           ,TotalFee                        ");
            sql.AppendLine("           ,TotalTax                        ");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                sql.AppendLine("           ,UsedUnitID                        ");
                sql.AppendLine("           ,UsedUnitCount                         ");
                sql.AppendLine("           ,UsedPrice                        ");
                sql.AppendLine("           ,ExRate                        ");
            }
            sql.AppendLine("           ,AskOrder)                       ");
            sql.AppendLine("     VALUES (@CompanyCD                     ");
            sql.AppendLine("            ,@AskNo                         ");
            sql.AppendLine("            ,@SortNo                        ");
            sql.AppendLine("            ,@FromType                      ");
            sql.AppendLine("            ,@FromBillID                    ");
            sql.AppendLine("            ,@FromLineNo                    ");
            sql.AppendLine("            ,@ProductID                     ");
            sql.AppendLine("            ,@ProductNo                     ");
            sql.AppendLine("            ,@ProductName                   ");
            sql.AppendLine("            ,@ProductCount                  ");
            sql.AppendLine("            ,@UnitID                        ");
            sql.AppendLine("            ,@UnitPrice                     ");
            sql.AppendLine("            ,@TotalPrice                    ");
            sql.AppendLine("            ,@RequireDate                   ");
            sql.AppendLine("            ,@ApplyReason                   ");
            sql.AppendLine("            ,@Remark                        ");
            sql.AppendLine("            ,@TaxPrice                      "); 
            sql.AppendLine("            ,@TaxRate                       ");
            sql.AppendLine("            ,@TotalFee                      ");
            sql.AppendLine("            ,@TotalTax                      ");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                sql.AppendLine("           ,@UsedUnitID                        ");
                sql.AppendLine("           ,@UsedUnitCount                         ");
                sql.AppendLine("           ,@UsedPrice                        ");
                sql.AppendLine("           ,@ExRate                        ");
            }
            sql.AppendLine("           ,@AskOrder)                       ");

            #endregion

            #region 传参
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitID", PurchaseAskPriceDetailM.UsedUnitID));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitCount", PurchaseAskPriceDetailM.UsedUnitCount));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedPrice", PurchaseAskPriceDetailM.UsedPrice));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExRate", PurchaseAskPriceDetailM.ExRate));
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskNo",PurchaseAskPriceDetailM.AskNo ));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo",PurchaseAskPriceDetailM.SortNo ));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType",PurchaseAskPriceDetailM.FromType ));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID",(PurchaseAskPriceDetailM.FromBillID == null)?DBNull.Value.ToString():PurchaseAskPriceDetailM.FromBillID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromLineNo",(PurchaseAskPriceDetailM.FromLineNo == null)?DBNull.Value.ToString():PurchaseAskPriceDetailM.FromLineNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID",PurchaseAskPriceDetailM.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo",PurchaseAskPriceDetailM.ProductNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName",PurchaseAskPriceDetailM.ProductName));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount",PurchaseAskPriceDetailM.ProductCount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitID",PurchaseAskPriceDetailM.UnitID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitPrice",PurchaseAskPriceDetailM.UnitPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice",PurchaseAskPriceDetailM.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RequireDate",PurchaseAskPriceDetailM.RequireDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyReason",PurchaseAskPriceDetailM.ApplyReason));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark",PurchaseAskPriceDetailM.Remark));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaxPrice",PurchaseAskPriceDetailM.TaxPrice)); 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaxRate",PurchaseAskPriceDetailM.TaxRate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalFee",PurchaseAskPriceDetailM.TotalFee));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalTax",PurchaseAskPriceDetailM.TotalTax));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskOrder", PurchaseAskPriceDetailM.AskOrder));
            #endregion

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region delete
        /// <summary>
        /// 删除采购询价明细表
        /// </summary>
        /// <param name="AskNo">采购询价主表编号</param>
        /// <returns>sqlcommend</returns>
        ///
        public static SqlCommand DeletePurAskPriceDetail(string AskNo)
        {
            SqlCommand comm = new SqlCommand();

            string sql = "DELETE FROM officedba.PurchaseAskPriceDetail WHERE CompanyCD=@CompanyCD AND AskNo=@AskNo";
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskNo", AskNo));
            comm.CommandText = sql;
            return comm;
        }

        public static SqlCommand DeletePurAskPriceDetailS(string AskNos)
        {
            SqlCommand comm = new SqlCommand();

            string sql = "DELETE FROM officedba.PurchaseAskPriceDetail WHERE CompanyCD=@CompanyCD AND AskNo in "+AskNos+"";
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.CommandText = sql;
            return comm;
        }
        #endregion

        #region 根据AskNo查找明细表
        /// <summary>
        ///根据AskNo查找明细表
        /// </summary>
        /// <param name="AskNo">AskNo</param>
        /// <param name="AskOrder">AskOrder</param>
        /// <returns>datatable</returns>
        /// 
        public static DataTable GetPurAskPriceDetail(string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  A.ID      ");
            sql.AppendLine("		, A.CompanyCD ");
            sql.AppendLine("		, A.AskNo     ");
            sql.AppendLine("		, A.SortNo    ");
            sql.AppendLine("		, A.FromType  ");
            sql.AppendLine("		, A.FromBillID");
            sql.AppendLine(",case A.FromType when '1' then (select ApplyNo from officedba.PurchaseApply WHERE ID=A.FromBillID)   ");
            sql.AppendLine(" when '2' then (select PlanNo from officedba.PurchasePlan WHERE ID=A.FromBillID) end as FromBillNo ");
            sql.AppendLine("		, A.FromLineNo    ");
            sql.AppendLine("		, A.ProductID     ");
            sql.AppendLine("		, isnull(B.ProdNo,'') AS ProductNo       ");
            sql.AppendLine("		,isnull( B.ProductName,'') as ProductName   ");
            sql.AppendLine("		,isnull( B.Specification,'') as  Specification");
            sql.AppendLine("		,   Convert(numeric(14," + userInfo.SelPoint + "),a.ProductCount) as ProductCount");
            sql.AppendLine("		, A.UnitID        ");
            sql.AppendLine("		, isnull(C.CodeName,'') AS UnitName     ");
            sql.AppendLine("		, isnull(ff.CodeName,'') AS UsedUnitName     ");
            sql.AppendLine("		,   Convert(numeric(14," + userInfo.SelPoint + "),a.UnitPrice) as UnitPrice ");
            sql.AppendLine("		,     Convert(numeric(22," + userInfo.SelPoint + "),a.TotalPrice) as TotalPrice ");
            sql.AppendLine("		, CONVERT(varchar(23),A.RequireDate,23) AS RequireDate   ");
            sql.AppendLine("		, A.ProviderID    ");
            sql.AppendLine("		, isnull(D.CustName,'')  AS ProviderName    ");
            sql.AppendLine("		, A.ApplyReason   ");
            sql.AppendLine("		,isnull( A.Remark,'')        ");
            sql.AppendLine("		,  Convert(numeric(22," + userInfo.SelPoint + "),a.TaxPrice) as TaxPrice        ");
            sql.AppendLine("		,  Convert(numeric(22," + userInfo.SelPoint + "),a.TaxRate) as TaxRate      ");
            sql.AppendLine("		, Convert(numeric(22," + userInfo.SelPoint + "),a.TotalFee) as TotalFee    ");
            sql.AppendLine("		,  Convert(numeric(22," + userInfo.SelPoint + "),a.TotalTax) as TotalTax       ");
            sql.AppendLine("		, Convert(numeric(12," + userInfo.SelPoint + "),a.ExRate) as ExRate   ");
            sql.AppendLine("		, Convert(numeric(14," + userInfo.SelPoint + "),a.UsedPrice) as UsedPrice      ");
            sql.AppendLine("		, Convert(numeric(14," + userInfo.SelPoint + "),a.UsedUnitCount) as UsedUnitCount      ");
            sql.AppendLine("		, A.UsedUnitID,isnull(H.TypeName,'') as ColorName      ");
            sql.AppendLine("FROM officedba.PurchaseAskPriceDetail AS A                    ");
            sql.AppendLine("LEFT JOIN  officedba.ProductInfo AS B ON A.ProductID = B.ID   ");
            sql.AppendLine("LEFT JOIN  officedba.CodeUnitType AS C ON A.UnitID = C.ID     ");
            sql.AppendLine("LEFT JOIN  officedba.CodeUnitType AS ff ON A.UsedUnitID = ff.ID     ");
            sql.AppendLine("LEFT JOIN  officedba.ProviderInfo AS D ON A.ProviderID = D.ID ");
            sql.AppendLine("left join officedba.CodePublicType H on B.ColorID=H.ID");
            sql.AppendLine("INNER JOIN officedba.PurchaseAskPrice AS E ON A.CompanyCD=E.CompanyCD AND A.AskNo=E.AskNo AND E.ID=@ID");

            //sql.AppendLine(" WHERE A.AskNo=@AskNo");
            //sql.AppendLine(" AND A.AskOrder=@AskOrder");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID",ID));

            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        #endregion

        #region 历史表操作
        #region Insert
        //参数：询价单ID
        public static SqlCommand InsertPurchaseAskHistory(string ID)
        {
            SqlCommand comm = new SqlCommand();
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.PurchaseAskPriceHistory                                                      ");
            sql.AppendLine("           (CompanyCD                                                                              ");
            sql.AppendLine("           ,AskNo                                                                                  ");
            sql.AppendLine("           ,AskOrder                                                                               ");
            sql.AppendLine("           ,ProviderID                                                                             ");
            sql.AppendLine("           ,AskDate                                                                                ");
            sql.AppendLine("           ,AskUserID                                                                              ");
            sql.AppendLine("           ,DeptID                                                                                 ");
            sql.AppendLine("           ,CountTotal                                                                             ");
            sql.AppendLine("           ,TotalPrice                                                                             ");
            sql.AppendLine("           ,TotalTax                                                                               ");
            sql.AppendLine("           ,TotalFee                                                                               ");
            sql.AppendLine("           ,Discount                                                                               ");
            sql.AppendLine("           ,DiscountTotal                                                                          ");
            sql.AppendLine("           ,RealTotal                                                                              ");
            sql.AppendLine("           ,isAddTax                                                                               ");
            sql.AppendLine("           ,ProductID                                                                              ");
            sql.AppendLine("           ,ProductCount                                                                           ");
            sql.AppendLine("           ,UnitID                                                                                 ");
            sql.AppendLine("           ,DiscountDetail                                                                         ");
            sql.AppendLine("           ,TaxRate                                                                                ");
            sql.AppendLine("           ,TaxPrice                                                                               ");
            sql.AppendLine("           ,TotalFeeDetail                                                                         ");
            sql.AppendLine("           ,TotalPriceDetail                                                                       ");
            sql.AppendLine("           ,TotalTaxDetail                                                                         ");
            sql.AppendLine("           ,RequireDate                                                                            ");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                sql.AppendLine("           ,UsedUnitID                        ");
                sql.AppendLine("           ,UsedUnitCount                         ");
                sql.AppendLine("           ,UsedPrice                        ");
                sql.AppendLine("           ,ExRate                        ");
            }

            sql.AppendLine("           ,UnitPrice)                                                                             ");
            sql.AppendLine("SELECT A.CompanyCD                                                                                 ");
            sql.AppendLine(", A.AskNo                                                                                          ");
            sql.AppendLine(", B.AskOrder                                                                                       ");
            sql.AppendLine(", B.ProviderID                                                                                     ");
            sql.AppendLine(", B.AskDate                                                                                        ");
            sql.AppendLine(", B.AskUserID                                                                                      ");
            sql.AppendLine(", B.DeptID                                                                                         ");
            sql.AppendLine(", B.CountTotal                                                                                     ");
            sql.AppendLine(", B.TotalPrice                                                                                     ");
            sql.AppendLine(", B.TotalTax                                                                                       ");
            sql.AppendLine(", B.TotalFee                                                                                       ");
            sql.AppendLine(", B.Discount                                                                                       ");
            sql.AppendLine(", B.DiscountTotal                                                                                  ");
            sql.AppendLine(", B.RealTotal                                                                                      ");
            sql.AppendLine(", B.isAddTax                                                                                       ");
            sql.AppendLine(", A.ProductID                                                                                      ");
            sql.AppendLine(", A.ProductCount                                                                                   ");
            sql.AppendLine(", A.UnitID                                                                                         ");
            sql.AppendLine(", A.Discount AS DisCountDetail                                                                     ");
            sql.AppendLine(", A.TaxRate                                                                                        ");
            sql.AppendLine(", A.TaxPrice                                                                                       ");
            sql.AppendLine(", A.TotalPrice AS TotalPriceDetail                                                                 ");
            sql.AppendLine(", A.TotalTax  AS TotalTaxDetail                                                                    ");
            sql.AppendLine(", A.TotalFee  AS TotalFeeDetail                                                                    ");
            sql.AppendLine(", A.RequireDate                                                                                    ");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                sql.AppendLine("           ,A.UsedUnitID                        ");
                sql.AppendLine("           ,A.UsedUnitCount                         ");
                sql.AppendLine("           ,A.UsedPrice                        ");
                sql.AppendLine("           ,A.ExRate                        ");
            }

            sql.AppendLine(", A.UnitPrice                                                                                      ");
            sql.AppendLine("FROM officedba.PurchaseAskPriceDetail AS A                                                         ");
            sql.AppendLine("LEFT OUTER JOIN officedba.PurchaseAskPrice AS B ON A.CompanyCD = B.CompanyCD AND A.AskNo = B.AskNo ");
            sql.AppendLine(" WHERE A.CompanyCD=@CompanyCD AND B.ID=@ID");
            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
            #endregion
            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 判断这个询价记录有没有在历史表中
        /// <summary>
        /// 插入采购询价主表
        /// </summary>
        /// <param name="AskNo">采购询价单No</param>
        /// <param name="AskOrder">询价次数</param>
        /// <returns>bool</returns>
        /// 
        public static bool IsInHistory(string AskNo, string AskOrder)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ID                               ");
            sql.AppendLine("      ,AskNo                            ");
            sql.AppendLine("      ,AskOrder                         ");
            sql.AppendLine("  FROM officedba.PurchaseAskPriceHistory");
            sql.AppendLine(" WHERE CompanyCD=@CompanyCD");
            sql.AppendLine(" AND AskNo=@AskNo");
            sql.AppendLine(" AND AskOrder=@AskOrder");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskNo",AskNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AskOrder",AskOrder));

            return SqlHelper.ExecuteSearch(comm).Rows.Count>0?true:false;
        }
        #endregion

        #region 查询采购询价记录
        /// <summary>
        /// 插入采购询价主表
        /// </summary>
        /// <param name="AskNo">采购询价单No</param>
        /// <param name="AskOrder">询价次数</param>
        /// <returns>bool</returns>
        /// 
        public static DataTable GetPurAskPriceHistory(string CompanyCD, string AskNo, int pageIndex, int pageCount, string OrderBy, out int totalCount)
        {
            try
            {
                SqlParameter[] parms = new SqlParameter[6];
                int i = 0;
                parms[i++] = new SqlParameter("@CompanyCD", CompanyCD);
                parms[i++] = new SqlParameter("@AskNo", AskNo);
                parms[i++] = new SqlParameter("@pageIndex", pageIndex);
                parms[i++] = new SqlParameter("@pageCount", pageCount);
                parms[i++] = new SqlParameter("@OrderBy", OrderBy);
                parms[i++] = MakeParam("@totalCount", SqlDbType.Int, 0, ParameterDirection.Output, null);

                DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[getPurAskPriHis]", parms);
                totalCount = int.Parse(parms[5].Value.ToString());
                return dt;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
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
        #endregion

        #region 询价历史数据显示
        public static DataTable GetPurAskHistory(string CompanyCD, string AskNo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT [ID]                                         ");
                sql.AppendLine("      ,[CompanyCD]                                  ");
                sql.AppendLine("      ,[AskNo]                                      ");
                sql.AppendLine("      ,[AskOrder]                                   ");
                sql.AppendLine("      ,[ProviderID]                                 ");
                sql.AppendLine("      ,[AskDate]                                    ");
                sql.AppendLine("      ,[AskUserID]                                  ");
                sql.AppendLine("      ,[DeptID]                                     ");
                sql.AppendLine("      ,[CountTotal]                                 ");
                sql.AppendLine("      ,[TotalPrice]                                 ");
                sql.AppendLine("      ,[TotalTax]                                   ");
                sql.AppendLine("      ,[TotalFee]                                   ");
                sql.AppendLine("      ,[Discount]                                   ");
                sql.AppendLine("      ,[DiscountTotal]                              ");
                sql.AppendLine("      ,[RealTotal]                                  ");
                sql.AppendLine("      ,[isAddTax]                                   ");
                sql.AppendLine("      ,[ProductID]                                  ");
                sql.AppendLine("      ,[ProductCount]                               ");
                sql.AppendLine("      ,[UnitID]                                     ");
                sql.AppendLine("      ,[DiscountDetail]                             ");
                sql.AppendLine("      ,[TaxRate]                                    ");
                sql.AppendLine("      ,[TaxPrice]                                   ");
                sql.AppendLine("      ,[TotalFeeDetail]                             ");
                sql.AppendLine("      ,[TotalPriceDetail]                           ");
                sql.AppendLine("      ,[TotalTaxDetail]                             ");
                sql.AppendLine("      ,[RequireDate]                                ");
                sql.AppendLine("      ,[UnitPrice]                                  ");
                sql.AppendLine("      ,[Remark]                                     ");
                sql.AppendLine("  FROM [officedba].[PurchaseAskPriceHistory]");
                sql.AppendLine(" WHERE CompanyCD=@CompanyCD");
                cmd.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",CompanyCD));
                sql.AppendLine(" AND AskNo=@AskNo");
                cmd.Parameters.Add(SqlHelper.GetParameterFromString("@AskNo",AskNo));
                cmd.CommandText = sql.ToString();
                return SqlHelper.ExecuteSearch(cmd);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        #endregion
    }
}
